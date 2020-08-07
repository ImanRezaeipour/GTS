using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model;
using System.Reflection;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.BaseInformation
{
    public class BIllness : BaseBusiness<Illness>
    {
        public BIllness() { }
        const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BIllness";
        private EntityRepository<Illness> staionRepository = new EntityRepository<Illness>(false);

        /// <summary>
        /// «اعتبارسنجی
        /// «نام نباید خالی باشد
        /// «نام  تکراری نباشد
        /// </summary>
        /// <param name="illness"></param>
        protected override void InsertValidate(Illness illness)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(illness.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.IllnessNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => illness.Name), illness.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.IllnessNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }

            
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// «اعتبارسنجی
        /// «نام نباید خالی باشد
        /// «نام نوع استخدام تکراری نباشد
        /// </summary>
        /// <param name="station"></param>
        protected override void UpdateValidate(Illness illness)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(illness.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.IllnessNameRequierd, "نام نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => illness.Name), illness.Name),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => illness.ID), illness.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.IllnessNameRepeated, "نام نباید تکراری باشد", ExceptionSrc));
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
        /// <param name="station"></param>
        protected override void DeleteValidate(Illness illness)
        {
           
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckIllnessesLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness(Illness illness, UIActionType UAT)
        {
            return base.SaveChanges(illness, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateIllness(Illness illness, UIActionType UAT)
        {
            return base.SaveChanges(illness, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteIllness(Illness illness, UIActionType UAT)
        {
            return base.SaveChanges(illness, UAT);
        }


    }
}