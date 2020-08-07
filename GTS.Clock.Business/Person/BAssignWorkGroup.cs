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
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure.Validation.Configuration;
using GTS.Clock.Business.Shifts;

namespace GTS.Clock.Business.Assignments
{
    public class BAssignWorkGroup : BaseBusiness<AssignWorkGroup>
    {
        const string ExceptionSrc = "GTS.Clock.Business.Assignments.BAssignWorkGroup";
        private EntityRepository<AssignWorkGroup> asignRepository = new EntityRepository<AssignWorkGroup>(false);
        private SysLanguageResource systemLanguage;
       
        public BAssignWorkGroup(SysLanguageResource sysLanguage) 
        {
            systemLanguage = sysLanguage;
        }

        public BAssignWorkGroup(LanguagesName sysLanguage)
        {
            if (sysLanguage == LanguagesName.Parsi)
                systemLanguage = SysLanguageResource.Parsi;
            else
                systemLanguage = SysLanguageResource.English;
        }

        public bool ExsitsForPerson(decimal personId)
        {
            if (asignRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new AssignWorkGroup().Person), new Person() { ID = personId })) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// لیستی از گروههای کاری را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<WorkGroup> GetAllWorkGroup()
        {
            try
            {
                BWorkgroup busWorkGroup = new BWorkgroup();
                return busWorkGroup.GetAll();
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignWorkGroup", "GetAllWorkGroup");
                throw ex;
            }
        }

        public override IList<AssignWorkGroup> GetAll()
        {
            try
            {
                throw new IllegalServiceAccess("استفاده از این متد بی معنا میباشد", ExceptionSrc);
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignWorkGroup", "GetAll");
                throw ex;
            }
        }

        public IList<AssignWorkGroup> GetAll(decimal personId)
        {
            try
            {
                if (personId > 0)
                {
                    IList<AssignWorkGroup> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new AssignWorkGroup().Person), new Person() { ID = personId }));
                    list = list.OrderBy(x => x.FromDate).ToList();
                    foreach (AssignWorkGroup assign in list)
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
                LogException(ex, "BAssignWorkGroup", "GetAll");
                throw ex;
            }
        }

        public IList<AssignWorkGroup> GetAllByWorkGroupId(decimal workGroupId)
        {
            try
            {
                IList<AssignWorkGroup> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new AssignWorkGroup().WorkGroup), new WorkGroup() { ID = workGroupId }));
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignWorkGroup", "GetAllByWorkGroupId");
                throw ex;
            }
        }

        /// <summary>
        /// بدون ایجاد ترانزاکشن و آماده سازی عمل درج را انجام میدهد
        /// </summary>
        /// <param name="assignRule"></param>
        /// <returns></returns>
        public decimal InsertWithoutTransaction(AssignWorkGroup assignWorkGroup)
        {
            try
            {
                asignRepository.WithoutTransactSave(assignWorkGroup);
                return assignWorkGroup.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignWorkGroup", "InsertWithoutTransaction");
                throw ex;
            }
        }

    

        public override AssignWorkGroup GetByID(decimal objID)
        {
            try
            {
                AssignWorkGroup assign = base.GetByID(objID);
                assign.UIFromDate = Utility.ToPersianDate(assign.FromDate);
                return assign;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignWorkGroup", "GetByID");
                throw ex;
            }
        }

        #region override methods
       
        protected override void GetReadyBeforeSave(AssignWorkGroup obj, UIActionType action)
        {
            if (systemLanguage == SysLanguageResource.Parsi)
            {
                obj.FromDate = Utility.ToMildiDate(obj.UIFromDate);
            }
            else if (systemLanguage == SysLanguageResource.English)
            {
                obj.FromDate = Utility.ToMildiDateTime(obj.UIFromDate);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="empl"></param>
        protected override void InsertValidate(AssignWorkGroup assignWorkGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            PersonRepository personRep = new PersonRepository(false);
            WorkGroupRepository workRep = new WorkGroupRepository(false);

            if (assignWorkGroup.Person == null || personRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Person().ID), assignWorkGroup.Person.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignWorkGroupPersonIdNotExsits, "پرسنلی با این مشخصات یافت نشد", ExceptionSrc));
            }

            if (assignWorkGroup.WorkGroup == null || workRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new WorkGroup().ID), assignWorkGroup.WorkGroup.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignWorkGroupIdNotExsits, "گروه کاری با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (assignWorkGroup.FromDate < Utility.GTSMinStandardDateTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignWorkGroupSmallerThanStandardValue, "مقدار تاریخ انتساب گروه محدوده محاسبات از حد مجاز کمتر میباشد", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>        
        /// </summary>
        /// <param name="empl"></param>
        protected override void UpdateValidate(AssignWorkGroup assignWorkGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            WorkGroupRepository workRep = new WorkGroupRepository(false);

            PersonRepository personRep = new PersonRepository(false);
            if (assignWorkGroup.Person == null || personRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Person().ID), assignWorkGroup.Person.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignWorkGroupPersonIdNotExsits, "پرسنلی با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (assignWorkGroup.WorkGroup == null || workRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new WorkGroup().ID), assignWorkGroup.WorkGroup.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignWorkGroupIdNotExsits, "گروه کاری با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (assignWorkGroup.FromDate < Utility.GTSMinStandardDateTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignWorkGroupSmallerThanStandardValue, "مقدار تاریخ انتساب گروه محدوده محاسبات از حد مجاز کمتر میباشد", ExceptionSrc));
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
        protected override void DeleteValidate(AssignWorkGroup obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();



            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateCFP(AssignWorkGroup assignWorkGroup, UIActionType action)
        {
            if (action == UIActionType.ADD || action == UIActionType.EDIT)
            {
                decimal personId = assignWorkGroup.Person.ID;
                CFP cfp = base.GetCFP(personId);
                DateTime newCfpDate = assignWorkGroup.FromDate;
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

       
        
        #endregion

    }
}