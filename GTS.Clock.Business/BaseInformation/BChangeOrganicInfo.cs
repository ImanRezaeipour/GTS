using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Assignments;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// created at: 3/12/2012 9:17:27 AM
    /// by        : 
    /// write your name here
    /// </summary>
    public class BChangeOrganicInfo:MarshalByRefObject
    {
        private const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BChangeOrganicInfo";
        private EntityRepository<ApplicationLanguageSettings> objectRep = new EntityRepository<ApplicationLanguageSettings>();
        ISearchPerson searchTool = new BPerson();
        BAssignDateRange bussDateRange = new BAssignDateRange(BLanguage.CurrentSystemLanguage);
        BAssignRule busRule = new BAssignRule(BLanguage.CurrentSystemLanguage);
        BAssignWorkGroup busWorkGroup = new BAssignWorkGroup(BLanguage.CurrentSystemLanguage);
        PersonRepository prsRepository = new PersonRepository(false);

        #region Fill Controls
        public Department GetDepartmentRoot() 
        {
            return searchTool.GetDepartmentRoot();
        }
        public IList<Department> GetDepartmentChild(decimal parentId) 
        {
            return searchTool.GetDepartmentChild(parentId);
        }
        public IList<EmploymentType> GetAllEmploymentTypes() 
        {
            return searchTool.GetAllEmploymentTypes();
        }
        public IList<WorkGroup> GetAllWorkGroup() 
        {
            return searchTool.GetAllWorkGroup();
        }
        public IList<RuleCategory> GetAllRuleGroup() 
        {
            return searchTool.GetAllRuleGroup();
        }
        public IList<CalculationRangeGroup> GetAllDateRanges() 
        {
            return searchTool.GetAllDateRanges();
        }
        #endregion

        /// <summary>
        /// تغییر مشخصات سازمانی
        /// </summary>
        /// <param name="proxy"></param>
        ///
        public bool ChangeInfo(PersonAdvanceSearchProxy proxy, OrganicInfoProxy infoProxy, out IList<ChangeInfoErrorProxy> errorList)
        {
            int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);

            bool result= this.ChangeInfo(list, infoProxy, out errorList);
            if(!result)
                foreach (ChangeInfoErrorProxy error in errorList) 
                {
                    BaseBusiness<Entity>.LogException(new Exception(error.ToString()));
                }
            return result;
        }

        /// <summary>
        /// تغییر مشخصات سازمانی
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="infoProxy"></param>
        /// <param name="errorList"></param>
        /// <returns></returns>
        public bool ChangeInfo(string searchKey, OrganicInfoProxy infoProxy, out IList<ChangeInfoErrorProxy> errorList)
        {
            int count = searchTool.GetPersonInQuickSearchCount(searchKey);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, searchKey);

            bool result = this.ChangeInfo(list, infoProxy, out errorList);
            if (!result)
                foreach (ChangeInfoErrorProxy error in errorList)
                {
                    BaseBusiness<Entity>.LogException(new Exception(error.ToString()));
                }
            return result;
        }

        public bool ChangeInfo(decimal personId, OrganicInfoProxy infoProxy, out IList<ChangeInfoErrorProxy> errorList)
        {
            Person prs = new BPerson().GetByID(personId);
            IList<Person> list = new List<Person>();
            list.Add(prs);
            bool result = this.ChangeInfo(list, infoProxy, out errorList);
            if (!result)
                foreach (ChangeInfoErrorProxy error in errorList)
                {
                    BaseBusiness<Entity>.LogException(new Exception(error.ToString()));
                }
            return result;
        }

        private bool ChangeInfo(IList<Person> personList, OrganicInfoProxy infoProxy, out IList<ChangeInfoErrorProxy> errorList)
        {
            IList<PersonRuleCatAssignment> ruleAssgnList = new List<PersonRuleCatAssignment>();
            IList<PersonRangeAssignment> rangeAssgnList = new List<PersonRangeAssignment>();
            IList<AssignWorkGroup> workGroupAssgnList = new List<AssignWorkGroup>();



            #region Validate
            DateTime workGroupFromDate, ruleGroupFromDate, ruleGroupToDate, DateRangeFromDate;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                workGroupFromDate = Utility.ToMildiDate(infoProxy.WorkGroupFromDate);
                ruleGroupFromDate = Utility.ToMildiDate(infoProxy.RuleGroupFromDate);
                ruleGroupToDate = Utility.ToMildiDate(infoProxy.RuleGroupToDate);
                DateRangeFromDate = Utility.ToMildiDate(infoProxy.DateRangeFromDate);
            }
            else
            {
                workGroupFromDate = Utility.ToMildiDateTime(infoProxy.WorkGroupFromDate);
                ruleGroupFromDate = Utility.ToMildiDateTime(infoProxy.RuleGroupFromDate);
                ruleGroupToDate = Utility.ToMildiDateTime(infoProxy.RuleGroupToDate);
                DateRangeFromDate = Utility.ToMildiDateTime(infoProxy.DateRangeFromDate);
            }


            errorList = new List<ChangeInfoErrorProxy>();
            foreach (Person prs in personList)
            {
                string errorMessage = "";
                if (infoProxy.WorkGroupID > 0)
                {
                    workGroupAssgnList = new BAssignWorkGroup(BLanguage.CurrentSystemLanguage).GetAllByWorkGroupId(infoProxy.WorkGroupID);
                    ValidateWorkGroupAssignment(prs, workGroupFromDate, ref errorMessage);
                }
                if (infoProxy.RuleGroupID > 0)
                {
                    ruleAssgnList = new BAssignRule(BLanguage.CurrentSystemLanguage).GetAllByRuleGroupId(infoProxy.RuleGroupID);
                    ValidateRuleGroupAssignment(prs, ruleGroupFromDate, ruleGroupToDate, ref errorMessage);
                }
                if (infoProxy.DateRangeID > 0)
                {
                    rangeAssgnList = new BAssignDateRange(BLanguage.CurrentSystemLanguage).GetAllByRangeId(infoProxy.DateRangeID);
                    ValidateDateRangeAssignment(prs, DateRangeFromDate, ref errorMessage);
                }
                if (!Utility.IsEmpty(errorMessage))
                {
                    errorList.Add(new ChangeInfoErrorProxy() { ErrorMessage = errorMessage, PersonCode = prs.PersonCode, PersonName = prs.Name });
                    if (errorList.Count > 50)
                        break;
                }
            }
            if (errorList.Count > 0)
            {
                return false;
            } 
            #endregion
          
            using (NHibernateSessionManager.Instance.BeginTransactionOn()) 
            {
                try
                {
                    bool cfpUpdateRequierd = false;
                    DateTime minDate = DateTime.Now;
                    int counter = 0;
                    foreach (Person prs in personList)
                    {
                        counter++;                        
                        bool updatePrs = false;
                   
                        #region Department
                        if (infoProxy.DepartmentID > 0)
                        {
                            prs.Department = new GTS.Clock.Model.Charts.Department() { ID = infoProxy.DepartmentID };
                            updatePrs = true;
                        } 
                        #endregion

                        #region Employment Type
                        if (infoProxy.EmploymentTypeID > 0)
                        {
                            prs.EmploymentType = new GTS.Clock.Model.BaseInformation.EmploymentType() { ID = infoProxy.EmploymentTypeID };
                            updatePrs = true;
                        } 
                        #endregion

                        if (updatePrs)
                        {
                            prsRepository.WithoutTransactUpdate(prs);
                        }

                        #region Rule Category
                        if (infoProxy.RuleGroupID > 0)
                        {
                            cfpUpdateRequierd = true;
                            PersonRuleCatAssignment ruleAssign = new PersonRuleCatAssignment();
                            BAssignRule ruleAsgnBus = new BAssignRule();
                            ruleAssgnList = ruleAsgnBus.GetAll(prs.ID);
                            IList<PersonRuleCatAssignment> confilictList =
                            ruleAssgnList.Where(x => ((Utility.ToMildiDateTime(x.FromDate) <= ruleGroupToDate && Utility.ToMildiDateTime(x.ToDate) >= ruleGroupToDate))
                                                                        ||
                                                     ((Utility.ToMildiDateTime(x.FromDate) <= ruleGroupFromDate && Utility.ToMildiDateTime(x.ToDate) >= ruleGroupFromDate))
                                                                        ||
                                                     ((Utility.ToMildiDateTime(x.FromDate) >= ruleGroupFromDate && Utility.ToMildiDateTime(x.FromDate) <= ruleGroupToDate))
                                    ).ToList();
                            if (confilictList != null && confilictList.Count > 0)
                            {
                                Range range = new Range() { From = ruleGroupFromDate, To = ruleGroupToDate, AditionalField = 0 };
                                var confilictRanges = from o in confilictList
                                                      select new Range() { From = Utility.ToMildiDateTime(o.FromDate), To = Utility.ToMildiDateTime(o.ToDate), AditionalField = o.RuleCategory.ID };
                                IList<Range> breakedList = Utility.Differance(confilictRanges.ToList(), range);

                                #region Delete
                                foreach (PersonRuleCatAssignment asgn in ruleAssgnList)
                                {
                                    ruleAsgnBus.SaveChanges(asgn, UIActionType.DELETE);
                                }
                                #endregion

                                #region add first
                                ruleAssign.FromDate = Utility.ToString(ruleGroupFromDate);
                                ruleAssign.ToDate = Utility.ToString(ruleGroupToDate);
                                ruleAssign.Person = prs;
                                ruleAssign.RuleCategory = new RuleCategory() { ID = infoProxy.RuleGroupID };
                                busRule.InsertWithoutTransaction(ruleAssign);
                                #endregion

                                #region add breaked List
                                foreach (Range r in breakedList)
                                {
                                    if (r.From == range.To)
                                        r.From = r.From.AddDays(1);
                                    if (r.To == range.From)
                                        r.To = r.To.AddDays(-1);
                                    ruleAssign = new PersonRuleCatAssignment();                                    
                                    ruleAssign.FromDate = Utility.ToString(r.From);
                                    ruleAssign.ToDate = Utility.ToString(r.To);
                                    ruleAssign.Person = prs;
                                    ruleAssign.RuleCategory = new RuleCategory() { ID = r.AditionalField };
                                    busRule.InsertWithoutTransaction(ruleAssign);
                                }
                                #endregion
                            }
                            else
                            {

                                ruleAssign.FromDate = Utility.ToString(ruleGroupFromDate);
                                ruleAssign.ToDate = Utility.ToString(ruleGroupToDate);
                                ruleAssign.Person = prs;
                                ruleAssign.RuleCategory = new RuleCategory() { ID = infoProxy.RuleGroupID };
                                busRule.InsertWithoutTransaction(ruleAssign);
                            }
                            if (minDate > ruleGroupFromDate)
                            {
                                minDate = ruleGroupFromDate;
                            }
                        } 
                        #endregion

                        #region Date Range
                        if (infoProxy.DateRangeID > 0)
                        {
                            cfpUpdateRequierd = true;
                            PersonRangeAssignment prsRangeAssignment = new PersonRangeAssignment();
                            rangeAssgnList = new BAssignDateRange(BLanguage.CurrentSystemLanguage).GetAll(prs.ID);
                            prsRangeAssignment = rangeAssgnList.Where(x => x.FromDate == DateRangeFromDate).FirstOrDefault();
                            if (prsRangeAssignment != null)
                            {
                                prsRangeAssignment.FromDate = DateRangeFromDate;
                                prsRangeAssignment.CalcDateRangeGroup = new CalculationRangeGroup() { ID = infoProxy.DateRangeID };
                            }
                            else
                            {
                                prsRangeAssignment = new PersonRangeAssignment();
                                prsRangeAssignment.Person = prs;
                                prsRangeAssignment.FromDate = DateRangeFromDate;
                                prsRangeAssignment.CalcDateRangeGroup = new CalculationRangeGroup() { ID = infoProxy.DateRangeID };
                            }
                            bussDateRange.InsertWithoutTransaction(prsRangeAssignment);

                            if (minDate > DateRangeFromDate)
                            {
                                minDate = DateRangeFromDate;
                            }
                        } 
                        #endregion

                        #region Work Group
                        if (infoProxy.WorkGroupID > 0)
                        {
                            cfpUpdateRequierd = true;
                            AssignWorkGroup prsWorkGroupAssignment = new AssignWorkGroup();
                            workGroupAssgnList = new BAssignWorkGroup(BLanguage.CurrentSystemLanguage).GetAll(prs.ID);
                            prsWorkGroupAssignment = workGroupAssgnList.Where(x => x.FromDate == workGroupFromDate).FirstOrDefault();

                            if (prsWorkGroupAssignment != null)
                            {
                                prsWorkGroupAssignment.Person = prs;
                                prsWorkGroupAssignment.FromDate = workGroupFromDate;
                                prsWorkGroupAssignment.WorkGroup = new WorkGroup() { ID = infoProxy.WorkGroupID };
                            }
                            else
                            {
                                prsWorkGroupAssignment = new AssignWorkGroup();
                                prsWorkGroupAssignment.Person = prs;
                                prsWorkGroupAssignment.FromDate = workGroupFromDate;
                                prsWorkGroupAssignment.WorkGroup = new WorkGroup() { ID = infoProxy.WorkGroupID };
                            }
                            busWorkGroup.InsertWithoutTransaction(prsWorkGroupAssignment);
                            if (minDate > workGroupFromDate)
                            {
                                minDate = workGroupFromDate;
                            }
                        } 
                        #endregion
                    } 
                    if (cfpUpdateRequierd)
                        this.UpdateCFP(personList, minDate);

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex) 
                {
                    BaseBusiness<Entity>.LogException(ex, "BChangeOrganicInfo", "ChangeInfo");
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    throw ex;
                }
            }


            return true;
        }

        /// <summary>
        /// بررسی حداقل تاریخ
        /// بررسی تکراری نبودن تاریخ
        /// </summary>
        /// <param name="person"></param>
        /// <param name="fromDate"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool ValidateWorkGroupAssignment(Person person, DateTime fromDate, ref string message)
        {
            PersonRepository personRep = new PersonRepository(false);
            WorkGroupRepository workRep = new WorkGroupRepository(false);
            EntityRepository<AssignWorkGroup> asignRepository = new EntityRepository<AssignWorkGroup>(false);

            if (fromDate <= Utility.GTSMinStandardDateTime)
            {
                message += "مقدار تاریخ انتساب گروه کاری از حد مجاز کمتر میباشد" + " --- ";
            }
            //else if (asignRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new AssignWorkGroup().Person), person),
            //    new CriteriaStruct(Utility.GetPropertyName(() => new AssignWorkGroup().FromDate), fromDate, CriteriaOperation.Equal)) > 0)
            //{
            //    message += "قبلا در این تاریخ گروه کاری دیگری انتساب داده شده است" + " --- ";
            //}

            return Utility.IsEmpty(message);
        }

        /// <summary>
        /// بررسی حداقل تاریخ
        /// بررسی بزرگتری تاریخ انها از ابتدا
        /// بررسی همپوشانی تاریخ ها
        /// </summary>
        /// <param name="person"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool ValidateRuleGroupAssignment(Person person, DateTime fromDate, DateTime toDate, ref string message)
        {
            EntityRepository<PersonRuleCatAssignment> asignRepository = new EntityRepository<PersonRuleCatAssignment>(false);
            PersonRepository personRep = new PersonRepository(false);

            if (fromDate <= Utility.GTSMinStandardDateTime || toDate <= Utility.GTSMinStandardDateTime)
            {
                message += "مقدار تاریخ انتساب گروه قانون از حد مجاز کمتر میباشد" + " --- ";
            }
            else if (toDate != Utility.GTSMinStandardDateTime && fromDate >= toDate)
            {
                message += "تاریخ انتساب گروه قانون ابتدا از انتها بزرگتر است" + " --- ";
            }
           /* else
            {
                PersonRuleCatAssignment assignRule = new PersonRuleCatAssignment();
                IList<PersonRuleCatAssignment> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => assignRule.PersonId), person.ID));
                if (list.Where(x => Utility.ToMildiDateTime(x.FromDate) <= toDate && Utility.ToMildiDateTime(x.ToDate) >= toDate).Count() > 0
                    ||
                    list.Where(x => Utility.ToMildiDateTime(x.FromDate) <= fromDate && Utility.ToMildiDateTime(x.ToDate) >= fromDate).Count() > 0
                    ||
                    list.Where(x => Utility.ToMildiDateTime(x.FromDate) >= fromDate && Utility.ToMildiDateTime(x.FromDate) <= toDate).Count() > 0
                    )
                {
                    message += "تاریخ انتساب گروه قانون با تاریخ های قبلی همپوشانی دارد" + " --- ";
                }
            }*/
            return Utility.IsEmpty(message);
        }

        /// <summary>
        /// بررسی حداقل تاریخ
        /// بررسی تکراری بودن تاریخ انتساب
        /// </summary>
        /// <param name="person"></param>
        /// <param name="fromDate"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool ValidateDateRangeAssignment(Person person, DateTime fromDate, ref string message)
        {
            PersonRepository personRep = new PersonRepository(false);
            EntityRepository<PersonRangeAssignment> asignRepository = new EntityRepository<PersonRangeAssignment>(false);

            if (fromDate <= Utility.GTSMinStandardDateTime)
            {
                message += "مقدار تاریخ انتساب دوره محاسبات از حد مجاز کمتر میباشد" + " --- ";
            }
            //else if (asignRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().Person), person),
            //    new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().FromDate), fromDate, CriteriaOperation.Equal)) > 0)
            //{
            //    message += "قبلا در این تاریخ محدوده محاسبات دیگری انتساب داده شده است" + " --- ";
            //}
            else 
            {
                DateTime startMonth, endMonth;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    PersianDateTime pd = Utility.ToPersianDateTime(fromDate);
                    int endOfMonth = Utility.GetEndOfPersianMonth(pd.Year, pd.Month);
                    startMonth = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 1));
                    endMonth = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(fromDate.Year, fromDate.Month);
                    startMonth = new DateTime(fromDate.Year, fromDate.Month, 1);
                    endMonth = new DateTime(fromDate.Year, fromDate.Month, endOfMonth);
                }

                if (asignRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().Person), person),
                                                       new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().FromDate), startMonth, CriteriaOperation.GreaterEqThan),
                                                       new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().FromDate), endMonth, CriteriaOperation.LessEqThan)) > 0) 
                {
                    message += "انتساب دوره محاسبات در یک ماه نباید دوبار صورت بگیرد" + " --- ";
                }
            }

            return Utility.IsEmpty(message);
        }

        private void UpdateCFP(IList<Person> personList, DateTime minChangeDate)
        {
            BusinessEntity baseBus = new BusinessEntity();
            IList<CFP> cfpList = new List<CFP>();
            Dictionary<decimal, DateTime> lockDates = new Dictionary<decimal, DateTime>();            
            
            foreach (Person prs in personList)
            {
                DateTime calculationLockDate = DateTime.Now;
                if (prs.PersonTASpec.UIValidationGroup != null)
                {
                    decimal uiValidateionGrpId = prs.PersonTASpec.UIValidationGroup.ID;
                    if (!lockDates.ContainsKey(uiValidateionGrpId))
                    {
                        calculationLockDate = baseBus.UIValidator.GetCalculationLockDateByGroup(prs.PersonTASpec.UIValidationGroup.ID);
                        lockDates.Add(uiValidateionGrpId, calculationLockDate);
                    }
                    else
                    {
                        calculationLockDate = lockDates[uiValidateionGrpId];
                    }
                }
                CFP cfp = baseBus.GetCFP(prs.ID);
                DateTime newCfpDate = minChangeDate;


                //بسته بودن محاسبات 
                if (calculationLockDate > Utility.GTSMinStandardDateTime && calculationLockDate > newCfpDate)
                {
                    newCfpDate = calculationLockDate.AddDays(1);
                }
                if (cfp.ID == 0 || cfp.Date > newCfpDate)
                {
                    cfp.Date = newCfpDate.Date;
                    cfp.PrsId = prs.ID;
                    cfpList.Add(cfp);
                }
            }
            baseBus.UpdateCFP(cfpList, false);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPersonnelOrganizationFeaturesChangeLoadAccess()
        {
        }


    }
}
