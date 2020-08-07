using System;
using System.Collections.Generic;
using System.Linq;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.BaseInformation
{
    public class BDutyPlace : BaseBusiness<DutyPlace>
    {
        const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BDutyPlace";
        private DutyPlaceRepository dutyPlaceRep = new DutyPlaceRepository(false);

        /// <summary>
        /// ریشه درخت را برمیگرداند که با پیمایش بچههای آن درخت استخراج میشود
        /// </summary>
        /// <returns></returns>
        public DutyPlace GetDutyPalcesTree()
        {
            try
            {              

                IList<DutyPlace> departmentsList = dutyPlaceRep.GetDutyPlaceTree();

                if (departmentsList.Count == 1)
                {
                    return departmentsList.First();
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.DutyPlaceRootMoreThanOne, "تعداد ریشه محلهای ماموریت در دیتابیس نامعتبر است", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BDutyPlace", "GetDutyPalcesTree");
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک گره را برمیگرداند
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public IList<DutyPlace> GetDutyPlaceChilds(decimal parentId)
        {
            try
            {
                IList<DutyPlace> dutyplcList = dutyPlaceRep
                    .GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DutyPlace().ParentID), parentId));
                return dutyplcList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDutyPlace", "GetDutyPlaceChilds");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dutyPlace"></param>
        protected override void InsertValidate(DutyPlace dutyPlace)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (dutyPlace.ParentID==0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceParentRequest, "درج - والد باید مشخص شد", ExceptionSrc));
            }
            if (Utility.IsEmpty(dutyPlace.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (dutyPlaceRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dutyPlace.Name), dutyPlace.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }

            if (!Utility.IsEmpty(dutyPlace.CustomCode))
            {
                if (dutyPlaceRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dutyPlace.CustomCode), dutyPlace.CustomCode)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceCustomCodeRepeated, "درج - کد نباید تکراری باشد", ExceptionSrc));
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
        /// <param name="dutyPlace"></param>
        protected override void UpdateValidate(DutyPlace dutyPlace)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
           
            if (Utility.IsEmpty(dutyPlace.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceNameRequierd, "نام نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (dutyPlaceRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dutyPlace.Name), dutyPlace.Name),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => dutyPlace.ID), dutyPlace.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceNameRepeated, "نام نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (!Utility.IsEmpty(dutyPlace.CustomCode))
            {
                if (dutyPlaceRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dutyPlace.CustomCode), dutyPlace.CustomCode),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => dutyPlace.ID), dutyPlace.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceCustomCodeRepeated, "بروزرسانی - کد نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(DutyPlace dutyPlace)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            RequestRepository requestRep = new RequestRepository(false);

            if (requestRep.IsDutyPlaceUsed(dutyPlace.ID))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceUsedByRequest, "بدلیل استفاده در درخواست ها نباید حذف شود", ExceptionSrc));
            }
            
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMissionLocationsLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertMissionLocation(DutyPlace missionLocation , UIActionType UAT)
        {
            return base.SaveChanges(missionLocation, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateMissionLocation(DutyPlace missionLocation, UIActionType UAT)
        {
            return base.SaveChanges(missionLocation, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteMissionLocation(DutyPlace missionLocation, UIActionType UAT)
        {
            return base.SaveChanges(missionLocation, UAT);
        }

    }
}