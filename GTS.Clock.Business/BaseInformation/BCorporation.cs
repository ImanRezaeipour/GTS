using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.BaseInformation
{
    public class BCorporation : BaseBusiness<Corporation>
    {
        const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BCorporation";
        private EntityRepository<Corporation> CorporationRepository = new EntityRepository<Corporation>(false);

        protected override void InsertValidate(Corporation Corporation)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(Corporation.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CorporationNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (CorporationRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => Corporation.Name), Corporation.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CorporationNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(Corporation Corporation)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(Corporation.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CorporationNameRequierd, "نام نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (CorporationRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => Corporation.Name), Corporation.Name),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => Corporation.ID), Corporation.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.CorporationNameRepeated, "نام نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(Corporation Corporation)
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckCorporationsLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertCorporation(Corporation corporation, UIActionType UAT)
        {
            return base.SaveChanges(corporation, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateCorporation(Corporation corporation, UIActionType UAT)
        {
            return base.SaveChanges(corporation, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteCorporation(Corporation corporation, UIActionType UAT)
        {
            return base.SaveChanges(corporation, UAT);
        }

    }
}
