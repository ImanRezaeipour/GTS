using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Concepts.Operations;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.Shifts
{
    public class BWorkgroup : BaseBusiness<WorkGroup>
    {
        IDataAccess accessPort = new BUser();
        const string ExceptionSrc = "GTS.Clock.Business.Shifts.Business.BWorkgroup";
        private WorkGroupRepository workGroupRep = new WorkGroupRepository(false);

        public override IList<WorkGroup> GetAll()
        {
            try
            {
                IList<decimal> ids = accessPort.GetAccessibleWorkGroups();
                IList<WorkGroup> list = new List<WorkGroup>();
                list = workGroupRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new WorkGroup().ID), ids.ToArray(), CriteriaOperation.IN));
                return list;
            }
            catch (Exception ex) 
            {
                LogException(ex, "BWorkgroup", "GetAll");
                throw ex;
            }
        }

        

        /// <summary>
        /// «اعتبارسنجی
        /// «نام نباید خالی باشد
        /// «نام گروه کاری تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="workgroup"></param>
        protected override void InsertValidate(WorkGroup workgroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(workgroup.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.WorkGroupNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (workGroupRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => workgroup.Name), workgroup.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.WorkGroupNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }

            if (!Utility.IsEmpty(workgroup.CustomCode))
            {
                if (workGroupRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => workgroup.CustomCode), workgroup.CustomCode)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.WorkGroupCustomCodeRepeated, "درج - کد گروه کاری نباید تکراری باشد", ExceptionSrc));
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// «اعتبارسنجی
        /// «نام نباید خالی باشد
        /// «نام گروه کاری تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="workgroup"></param>
        protected override void UpdateValidate(WorkGroup workgroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(workgroup.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.WorkGroupNameRequierd, "نام نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (workGroupRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => workgroup.Name), workgroup.Name),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => workgroup.ID), workgroup.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.WorkGroupNameRepeated, "نام نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (!Utility.IsEmpty(workgroup.CustomCode))
            {
                if (workGroupRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => workgroup.CustomCode), workgroup.CustomCode),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => workgroup.ID), workgroup.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.WorkGroupCustomCodeRepeated, "بروزرسانی - کد گروه کاری نباید تکراری باشد", ExceptionSrc));
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
        protected override void DeleteValidate(WorkGroup obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (workGroupRep.UsedByPerson(obj.ID)) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.WorkGroupUsedByPerson, "حذف - این گروه کاری توسط اشخاص استفاده شده است", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckWorkGroupsLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertWorkGroup(WorkGroup workGroup, UIActionType UAT)
        {
            return base.SaveChanges(workGroup, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateWorkGroup(WorkGroup workGroup, UIActionType UAT)
        {
            return base.SaveChanges(workGroup, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteWorkGroup(WorkGroup workGroup, UIActionType UAT)
        {
            return base.SaveChanges(workGroup, UAT);
        }

    }
}
