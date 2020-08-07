using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model;
using System.Reflection;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Rules;

namespace GTS.Clock.Business.Assignments
{
    public class BAssignRule : BaseBusiness<PersonRuleCatAssignment>
    {
        const string ExceptionSrc = "GTS.Clock.Business.Assignments.BAssignRule";
        private EntityRepository<PersonRuleCatAssignment> asignRepository = new EntityRepository<PersonRuleCatAssignment>(false);
        private SysLanguageResource systemLanguage;

        #region Constructor

        public BAssignRule()
        {
            systemLanguage = SysLanguageResource.Parsi;
        }

        public BAssignRule(SysLanguageResource sysLanguageResource)
        {
            systemLanguage = sysLanguageResource;
        }

        public BAssignRule(LanguagesName sysLanguage)
        {
            if (sysLanguage == LanguagesName.Parsi)
                systemLanguage = SysLanguageResource.Parsi;
            else
                systemLanguage = SysLanguageResource.English;
        }
        
        #endregion
         

        #region GetAll

        public override IList<PersonRuleCatAssignment> GetAll()
        {
            try
            {
                throw new IllegalServiceAccess("استفاده از این متد بی معنا میباشد", ExceptionSrc);
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignRule", "GetAll");
                throw ex;
            }
        }

        public IList<PersonRuleCatAssignment> GetAll(decimal personId)
        {
            try
            {
                if (personId > 0)
                {
                    IList<PersonRuleCatAssignment> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRuleCatAssignment().Person), new Person() { ID = personId }));
                    list = list.OrderBy(x => Utility.ToMildiDateTime(x.FromDate)).ToList();
                    foreach (PersonRuleCatAssignment assign in list)
                    {
                        assign.UIFromDate = Utility.ToPersianDate(assign.FromDate);
                        assign.UIToDate = Utility.ToPersianDate(assign.ToDate);
                    }
                    return list;
                }
                else
                {
                    throw new ItemNotExists("پرسنل مشخص نشده است - خطا در مرورگر", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignRule", "GetAll");
                throw ex;
            }
        }

        public IList<PersonRuleCatAssignment> GetAllByRuleGroupId(decimal groupId)
        {
            try
            {
                IList<PersonRuleCatAssignment> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRuleCatAssignment().RuleCategory), new RuleCategory() { ID = groupId }));
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignRule", "GetAllByRuleGroupId");
                throw ex;
            }
        }

        public override PersonRuleCatAssignment GetByID(decimal objID)
        {
            try
            {
                if (objID > 0)
                {
                    PersonRuleCatAssignment assign = base.GetByID(objID);
                    assign.UIFromDate = Utility.ToPersianDate(assign.FromDate);
                    assign.UIToDate = Utility.ToPersianDate(assign.ToDate);
                    return assign;
                }
                else 
                {
                    throw new ItemNotExists("پرسنل مشخص نشده است - خطا در مرورگر", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignRule", "GetByID");
                throw ex;
            }
        }

        public bool ExsitsForPerson(decimal personId)
        {
            if (asignRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRuleCatAssignment().Person), new Person() { ID = personId })) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// لیستی از گروههای قوانین را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<RuleCategory> GetAllRuleCategories()
        {
            try
            {
                BRuleCategory busRule = new BRuleCategory();
                IList<RuleCategory> list= busRule.GetAll();
                if (list != null && list.Count > 0) 
                {
                    list = list.Where(x => !x.IsRoot).OrderBy(x => x.Name).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignRule", "GetAllRuleCategories");
                throw ex;
            }
        }

        #endregion
      
        protected override void GetReadyBeforeSave(PersonRuleCatAssignment obj, UIActionType action)
        {
            if (systemLanguage == SysLanguageResource.Parsi)
            {
                obj.FromDate = Utility.ToString(Utility.ToMildiDate(obj.UIFromDate));
                obj.ToDate = Utility.ToString(Utility.ToMildiDate(obj.UIToDate));
            }
            else if (systemLanguage == SysLanguageResource.English)
            {
                obj.FromDate = Utility.ToString(Utility.ToMildiDateTime(obj.UIFromDate));
                obj.ToDate = Utility.ToString(Utility.ToMildiDateTime(obj.UIToDate));
            }
        }

        /// <summary>       
        /// </summary>
        /// <param name="empl"></param>
        protected override void InsertValidate(PersonRuleCatAssignment assignRule)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            RuleCategoryRepository catRep = new RuleCategoryRepository(false);
            PersonRepository personRep = new PersonRepository(false);
            DateTime fromDate,toDate;
            if (assignRule.Person==null || personRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Person().ID), assignRule.Person.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRulePersonIdNotExsits, "پرسنلی با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (assignRule.RuleCategory == null || catRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new RuleCategory().ID), assignRule.RuleCategory.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRuleIdNotExsits, "گروه قانون با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (!DateTime.TryParse(assignRule.FromDate, out fromDate) || !DateTime.TryParse(assignRule.ToDate, out toDate)) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRuleDateFormatProblem, "فرمت تاریخ انتساب گروه قانون نادرست میباشد", ExceptionSrc));
            }
            else if (fromDate < Utility.GTSMinStandardDateTime || toDate < Utility.GTSMinStandardDateTime) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRuleDateSmallerThanStandardValue, "مقدار تاریخ انتساب گروه قانون از حد مجاز کمتر میباشد", ExceptionSrc));
            }
            else if (toDate != Utility.GTSMinStandardDateTime && fromDate >= toDate)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRuleFromDateGreaterThanToDate, "تاریخ انتساب گروه قانون ابتدا از انتها بزرگتر است", ExceptionSrc));
            }
            else if(exception.Count==0)
            {
                IList<PersonRuleCatAssignment> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRuleCatAssignment().PersonId), assignRule.Person.ID));
                if (list.Where(x => Utility.ToMildiDateTime(x.FromDate) <= Utility.ToMildiDateTime(assignRule.ToDate) && Utility.ToMildiDateTime(x.ToDate) >= Utility.ToMildiDateTime(assignRule.ToDate)).Count() > 0
                    ||
                    list.Where(x => Utility.ToMildiDateTime(x.FromDate) <= Utility.ToMildiDateTime(assignRule.FromDate) && Utility.ToMildiDateTime(x.ToDate) >= Utility.ToMildiDateTime(assignRule.FromDate)).Count() > 0
                    ||
                    list.Where(x => Utility.ToMildiDateTime(x.FromDate) >= Utility.ToMildiDateTime(assignRule.FromDate) && Utility.ToMildiDateTime(x.FromDate) <= Utility.ToMildiDateTime(assignRule.ToDate)).Count() > 0
                    )
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.AssignRuleDateHasConfilict, "تاریخ انتساب گروه قانون با تاریخ های قبلی همپوشانی دارد", ExceptionSrc));
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>        
        /// </summary>
        /// <param name="empl"></param>
        protected override void UpdateValidate(PersonRuleCatAssignment assignRule)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            RuleCategoryRepository catRep = new RuleCategoryRepository(false);
            DateTime fromDate, toDate;
            PersonRepository personRep = new PersonRepository(false);
            if (assignRule.Person == null || personRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Person().ID), assignRule.Person.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRulePersonIdNotExsits, "پرسنلی با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (assignRule.RuleCategory == null || catRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new RuleCategory().ID), assignRule.RuleCategory.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRuleIdNotExsits, "گروه قانون با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (!DateTime.TryParse(assignRule.FromDate, out fromDate) || !DateTime.TryParse(assignRule.ToDate, out toDate))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRuleDateFormatProblem, "فرمت تاریخ انتساب گروه قانون نادرست میباشد", ExceptionSrc));
            }
            else if (fromDate < Utility.GTSMinStandardDateTime || toDate < Utility.GTSMinStandardDateTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRuleDateSmallerThanStandardValue, "مقدار تاریخ انتساب گروه قانون از حد مجاز کمتر میباشد", ExceptionSrc));
            }
            else if (toDate != Utility.GTSMinStandardDateTime && fromDate >= toDate)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRuleFromDateGreaterThanToDate, "تاریخ انتساب گروه قانون ابتدا از انتها بزرگتر است", ExceptionSrc));
            }
            else if(exception.Count==0)
            {
                IList<PersonRuleCatAssignment> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRuleCatAssignment().PersonId), assignRule.Person.ID),
                                                                                    new CriteriaStruct(Utility.GetPropertyName(() => new PersonRuleCatAssignment().ID), assignRule.ID, CriteriaOperation.NotEqual));
                if (list.Where(x => Utility.ToMildiDateTime(x.FromDate) <= Utility.ToMildiDateTime(assignRule.ToDate) && Utility.ToMildiDateTime(x.ToDate) >= Utility.ToMildiDateTime(assignRule.ToDate)).Count() > 0
                    ||
                    list.Where(x => Utility.ToMildiDateTime(x.FromDate) <= Utility.ToMildiDateTime(assignRule.FromDate) && Utility.ToMildiDateTime(x.ToDate) >= Utility.ToMildiDateTime(assignRule.FromDate)).Count() > 0
                    ||
                    list.Where(x => Utility.ToMildiDateTime(x.FromDate) >= Utility.ToMildiDateTime(assignRule.FromDate) && Utility.ToMildiDateTime(x.FromDate) <= Utility.ToMildiDateTime(assignRule.ToDate)).Count() > 0
                    )
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.AssignRuleDateHasConfilict, "تاریخ انتساب گروه قانون با تاریخ های قبلب همپوشانی دارد", ExceptionSrc));
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(PersonRuleCatAssignment obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateCFP(PersonRuleCatAssignment obj, UIActionType action)
        {
            if (action == UIActionType.ADD || action == UIActionType.EDIT)
            {
                decimal personId = obj.Person.ID;
                CFP cfp = base.GetCFP(personId);
                DateTime newCfpDate = Utility.ToMildiDateTime(obj.FromDate);
                if (cfp.ID == 0 || cfp.Date > newCfpDate)
                {
                    DateTime calculationLockDate = base.UIValidator.GetCalculationLockDate(personId);

                    //بسته بودن محاسبات 
                    if (calculationLockDate > Utility.GTSMinStandardDateTime && calculationLockDate > newCfpDate)
                    {
                        newCfpDate = calculationLockDate.AddDays(1);
                    }

                    base.UpdateCFP(personId, newCfpDate);
                }
            }
        }

        /// <summary>
        /// بدون ایجاد ترانزاکشن و آماده سازی عمل درج را انجام میدهد
        /// </summary>
        /// <param name="assignRule"></param>
        /// <returns></returns>
        public decimal InsertWithoutTransaction(PersonRuleCatAssignment assignRule) 
        {
            try
            {
                asignRepository.WithoutTransactSave(assignRule);
                return assignRule.ID;
            }
            catch (Exception ex) 
            {
                LogException(ex, "BAssignRule", "InsertWithoutTransaction");
                throw ex;
            }
        }
    }
}