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
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Rules;
using GTS.Clock.Business.AppSettings;

namespace GTS.Clock.Business.Assignments
{
    public class BAssignDateRange : BaseBusiness<PersonRangeAssignment>
    {
        const string ExceptionSrc = "GTS.Clock.Business.Assignments.BAssignDateRange";
        private EntityRepository<PersonRangeAssignment> asignRepository = new EntityRepository<PersonRangeAssignment>(false);
        private SysLanguageResource systemLanguage;

        public BAssignDateRange(SysLanguageResource sysLanguage)
        {
            systemLanguage = sysLanguage;
        }

        public BAssignDateRange(LanguagesName sysLanguage)
        {
            if (sysLanguage == LanguagesName.Parsi)
                systemLanguage = SysLanguageResource.Parsi;
            else
                systemLanguage = SysLanguageResource.English;
        }

        /// <summary>
        /// آیا قبلا انتساب دوره در یک تاریخ خاص داده شده
        /// جهت استفاده در فرم پرسنل برای چک کردن ثبت نشدن دوره تکراری
        /// </summary>
        /// <param name="dateRangeGroupId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public PersonRangeAssignment GetPersonRangeAssignmentByGroupAndDate(decimal dateRangeGroupId,decimal personId, DateTime date) 
        {
            IList<PersonRangeAssignment> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().CalcDateRangeGroup), new CalculationRangeGroup() { ID = dateRangeGroupId }),
                                                                              new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().Person), new Person() {ID=personId }),
                                                                              new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().FromDate), date));
            if (list.Count > 0) 
            {
                return list.Last();
            }
            return null;
        }

        public bool ExsitsForPerson(decimal personId)
        {
            try
            {
                if (asignRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().Person), new Person() { ID = personId })) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignDateRange", "ExsitsForPerson");
                throw ex;
            }
        }

        /// <summary>
        /// لیستی از دوره محاسبات را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<CalculationRangeGroup> GetAllCalculationRangeGroup()
        {
            try
            {
                BDateRange busRAnge = new BDateRange();
                return busRAnge.GetAll();
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignDateRange", "GetAllCalculationRangeGroup");
                throw ex;
            }
        }

        public IList<PersonRangeAssignment> GetAll(decimal personId)
        {
            try
            {
                if (personId > 0)
                {
                    IList<PersonRangeAssignment> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().Person), new Person() { ID = personId }));
                    list = list.OrderBy(x => x.FromDate).ToList();
                    foreach (PersonRangeAssignment assign in list)
                    {
                        assign.UIFromDate = Utility.ToPersianDate(assign.FromDate);
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
                LogException(ex, "BAssignDateRange", "GetAll");
                throw ex;
            }
        }

        public IList<PersonRangeAssignment> GetAllByRangeId(decimal groupId)
        {
            try
            {

                IList<PersonRangeAssignment> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().CalcDateRangeGroup), new Person() { ID = groupId }));
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignDateRange", "GetAllByRangeId");
                throw ex;
            }
        }

        public override PersonRangeAssignment GetByID(decimal objID)
        {
            try
            {
                PersonRangeAssignment assign = base.GetByID(objID);
                assign.UIFromDate = Utility.ToPersianDate(assign.FromDate);
                return assign;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignDateRange", "GetByID");
                throw ex;
            }
        }

        protected override void GetReadyBeforeSave(PersonRangeAssignment assignRange, UIActionType action)
        {
            if (systemLanguage == SysLanguageResource.Parsi)
            {
                assignRange.FromDate = Utility.ToMildiDate(assignRange.UIFromDate);
            }
            else if (systemLanguage == SysLanguageResource.English)
            {
                if (assignRange.UIFromDate != null)
                {
                    string[] strs = assignRange.UIFromDate.Split('/');
                    if (strs[0].Length == 4)
                    {
                        assignRange.FromDate = new DateTime(Utility.ToInteger(strs[0]), Utility.ToInteger(strs[1]), Utility.ToInteger(strs[2]));
                    }
                    else
                    {
                        assignRange.FromDate = new DateTime(Utility.ToInteger(strs[2]), Utility.ToInteger(strs[1]), Utility.ToInteger(strs[0]));
                    }
                }
            }
            if (action == UIActionType.ADD || action == UIActionType.EDIT)
            {
                //اگر اولین انتساب است اتوماتیک به ابتدای سال برود
                Person prs = new PersonRepository(false).GetById(assignRange.Person.ID, false);
                if (prs.PersonRangeAssignList == null || prs.PersonRangeAssignList.Count == 0)
                {
                    DateTime startYear;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        PersianDateTime pd = Utility.ToPersianDateTime(assignRange.FromDate);
                        startYear = Utility.ToMildiDate(String.Format("{0}/01/01", pd.Year));
                    }
                    else
                    {
                        startYear = new DateTime(assignRange.FromDate.Year, 1, 1);
                    }
                    if (assignRange.FromDate.Date > startYear.Date)
                    {
                        assignRange.FromDate = startYear.Date;
                    }
                }
            }
        }

        /// <summary>    
        /// اعتبار سنجی شناسه پرینل
        /// </summary>
        /// <param name="assignRange"></param>
        protected override void InsertValidate(PersonRangeAssignment assignRange)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            PersonRepository personRep = new PersonRepository(false);
            EntityRepository<CalculationRangeGroup> groupRep = new EntityRepository<CalculationRangeGroup>(false);
            if (assignRange.Person == null || personRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Person().ID), assignRange.Person.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangePersonIdNotExsits, "پرسنلی با این مشخصات یافت نشد", ExceptionSrc));
            }

            if (assignRange.CalcDateRangeGroup == null || groupRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new CalculationRangeGroup().ID), assignRange.CalcDateRangeGroup.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeGroupIdNotExsits, "گروه دوره محاسبات با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (assignRange.FromDate < Utility.GTSMinStandardDateTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeSmallerThanStandardValue, "مقدار تاریخ انتساب گروه محدوده محاسبات از حد مجاز کمتر میباشد", ExceptionSrc));
            }
            if (asignRepository.Find(x =>x.Person.ID==assignRange.Person.ID && x.FromDate.Date == assignRange.FromDate.Date).Count() > 0) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeDateIsRepeated, "تاریخ تکراری است", ExceptionSrc));
            }
            if (exception.Count == 0) 
            {
                Person prs = personRep.GetById(assignRange.Person.ID, false);
                if (prs.PersonRangeAssignList == null || prs.PersonRangeAssignList.Count == 0)
                {
                    DateTime startYear;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        PersianDateTime pd = Utility.ToPersianDateTime(assignRange.FromDate);
                        startYear = Utility.ToMildiDate(String.Format("{0}/01/01", pd.Year));
                    }
                    else
                    {
                        startYear = new DateTime(assignRange.FromDate.Year, 1, 1);
                    }
                    if (assignRange.FromDate.Date > startYear.Date)
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeFirstMustBeFromStartYear, "ابتدای بازه انتساب در اولین انتساب ، باید ابتدای سال باشد", ExceptionSrc));
                    }
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>    
        /// اعتبار سنجی شناسه پرینل
        /// </summary>
        /// <param name="assignRange"></param>
        protected override void UpdateValidate(PersonRangeAssignment assignRange)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            EntityRepository<CalculationRangeGroup> groupRep = new EntityRepository<CalculationRangeGroup>(false);
            PersonRepository personRep = new PersonRepository(false);
            if (assignRange.Person == null || personRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Person().ID), assignRange.Person.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangePersonIdNotExsits, "پرسنلی با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (assignRange.CalcDateRangeGroup == null || groupRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new CalculationRangeGroup().ID), assignRange.CalcDateRangeGroup.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeGroupIdNotExsits, "گروه دوره محاسبات با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (assignRange.FromDate < Utility.GTSMinStandardDateTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeSmallerThanStandardValue, "مقدار تاریخ انتساب گروه محدوده محاسبات از حد مجاز کمتر میباشد", ExceptionSrc));
            }
            if (exception.Count == 0)
            {
                Person prs = personRep.GetById(assignRange.Person.ID, false);
                if (prs.PersonRangeAssignList == null || prs.PersonRangeAssignList.Count == 1)
                {
                    DateTime startYear;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        PersianDateTime pd = Utility.ToPersianDateTime(assignRange.FromDate);
                        startYear = Utility.ToMildiDate(String.Format("{0}/01/01", pd.Year));
                    }
                    else
                    {
                        startYear = new DateTime(assignRange.FromDate.Year, 1, 1);
                    }
                    if (assignRange.FromDate.Date > startYear.Date)
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeFirstMustBeFromStartYear, "ابتدای بازه انتساب در اولین انتساب ، باید ابتدای سال باشد", ExceptionSrc));
                    }
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
        protected override void DeleteValidate(PersonRangeAssignment obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();



            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// بدون ایجاد ترانزاکشن و آماده سازی عمل درج را انجام میدهد
        /// </summary>
        /// <param name="assignRule"></param>
        /// <returns></returns>
        public decimal InsertWithoutTransaction(PersonRangeAssignment assignRange)
        {
            try
            {
                asignRepository.WithoutTransactSave(assignRange);
                return assignRange.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignDateRange", "InsertWithoutTransaction");
                throw ex;
            }
        }

        protected override void UpdateCFP(PersonRangeAssignment obj, UIActionType action)
        {
            if (action == UIActionType.ADD || action == UIActionType.EDIT)
            {
                decimal personId = obj.Person.ID;
                CFP cfp = base.GetCFP(personId);
                DateTime newCfpDate = obj.FromDate;
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
    }
}