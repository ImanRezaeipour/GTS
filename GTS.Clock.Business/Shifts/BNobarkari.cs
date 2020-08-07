using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.Shifts
{
    public class BNobatkari : BaseBusiness<NobatKari>
    {
        const string ExceptionSrc = "GTS.Clock.Business.Shifts.BNobatkari";
        private EntityRepository<NobatKari> nobatKariRepository
            = new EntityRepository<NobatKari>(false);             

        /// <summary>
        /// اعتبارسنجی
        /// نام نباید خالی باشد
        /// نام نوبت کاری تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="nobatKari"></param>
        protected override void InsertValidate(NobatKari nobatKari)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(nobatKari.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.NobatKariNameEmpty, "نام نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (nobatKariRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => nobatKari.Name), nobatKari.Name)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.NobatKariRepeated, "نام نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (!Utility.IsEmpty(nobatKari.CustomCode))
            {
                if (nobatKariRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => nobatKari.CustomCode), nobatKari.CustomCode)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.NobatKariCustomCodeRepeated, "بروزرسانی - کد تعریف شده در نوبت کاری نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی
        /// نام نباید خالی باشد
        /// نام نوبت کاری تکراری نباشد
        /// کد تعریف شده باید تکراری باشد
        /// </summary>
        /// <param name="nobatKari"></param>
        protected override void UpdateValidate(NobatKari nobatKari)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(nobatKari.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.NobatKariNameEmpty, "نام نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (nobatKariRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => nobatKari.Name), nobatKari.Name),
                                                                new CriteriaStruct(Utility.GetPropertyName(() => nobatKari.ID), nobatKari.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.NobatKariRepeated, "نام نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (!Utility.IsEmpty(nobatKari.CustomCode))
            {
                if (nobatKariRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => nobatKari.CustomCode), nobatKari.CustomCode),
                                                                new CriteriaStruct(Utility.GetPropertyName(() => nobatKari.ID), nobatKari.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.NobatKariCustomCodeRepeated, "بروزرسانی - کد تعریف شده در نوبت کاری نباید تکراری باشد", ExceptionSrc));
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
        protected override void DeleteValidate(NobatKari nobatKari)
        {
            ShiftRepository shiftRep = new ShiftRepository(false);
            int count = shiftRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Shift().NobatKari), nobatKari));

            if (count > 0)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
                UIValidationExceptions exception = new UIValidationExceptions();
                exception.Add(ExceptionResourceKeys.NobatKariUsedByShift, "این نوبت کاری به شیفت انتساب داده شده است", ExceptionSrc);
                throw exception;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckWorkHeatsLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertWorkHeat(NobatKari workHeat, UIActionType UAT)
        {
            return base.SaveChanges(workHeat, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateWorkHeat(NobatKari workHeat, UIActionType UAT)
        {
            return base.SaveChanges(workHeat, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteWorkHeat(NobatKari workHeat, UIActionType UAT)
        {
            return base.SaveChanges(workHeat, UAT);
        }

    }
}
