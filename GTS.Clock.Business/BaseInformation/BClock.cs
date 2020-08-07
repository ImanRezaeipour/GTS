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
using BaseInfo = GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// created at: 2011-12-14 1:50:17 PM
    /// by        : Farhad Salavati
    /// write your name here
    /// </summary>
    public class BClock : BaseBusiness<BaseInfo.Clock>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.BaseInformationBClock";
        private EntityRepository<BaseInfo.Clock> objectRep = new EntityRepository<BaseInfo.Clock>();

        #region BaseBusiness Implementation

        /// <summary>
        /// تمامی انواع ساعت را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<ClockType> GetAllClockTypes() 
        {
            EntityRepository<ClockType> rep = new EntityRepository<ClockType>();
            IList<ClockType> list = rep.GetAll();
            return list;
        }

        /// <summary>
        /// تمامی ایستگاه کنترل را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<ControlStation> GetAllControlStations() 
        {
            EntityRepository<ControlStation> rep = new EntityRepository<ControlStation>();
            IList<ControlStation> list = rep.GetAll();
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(BaseInfo.Clock clock)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(clock.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ClockNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => clock.Name), clock.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ClockNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }
            if (!Utility.IsEmpty(clock.CustomCode))
            {
                if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => clock.CustomCode), clock.CustomCode)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ClockCustomCodeRepeated, "درج - کد نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (clock.Clocktype == null) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ClockTypeRequierd, "درج - نوع ساعت نباید خالی باشد", ExceptionSrc));
            }

            if (clock.Station == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ClockControStationRequierd, "درج - ایستگاه کنترل نباید خالی باشد", ExceptionSrc));
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
        protected override void UpdateValidate(BaseInfo.Clock clock)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(clock.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ClockNameRequierd, "بروزرسانی - نام نباید خالی باشد", ExceptionSrc));
            }
            else 
                if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => clock.Name), clock.Name),
                                                 new CriteriaStruct(Utility.GetPropertyName(() => clock.ID), clock.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ClockNameRepeated, "بروزرسانی - نام نباید تکراری باشد", ExceptionSrc));
            }
            if (!Utility.IsEmpty(clock.CustomCode))
            {
                if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => clock.CustomCode), clock.CustomCode),
                                                 new CriteriaStruct(Utility.GetPropertyName(() => clock.ID), clock.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ClockCustomCodeRepeated, "بروزرسانی - کد نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (clock.Clocktype == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ClockTypeRequierd, "بروزرسانی - نوع ساعت نباید خالی باشد", ExceptionSrc));
            }

            if (clock.Station == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ClockControStationRequierd, "بروزرسانی - ایستگاه کنترل نباید خالی باشد", ExceptionSrc));
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
        protected override void DeleteValidate(BaseInfo.Clock obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMachinesLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertMachine(BaseInfo.Clock machine, UIActionType UAT)
        {
            return base.SaveChanges(machine, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateMachine(BaseInfo.Clock machine, UIActionType UAT)
        {
            return base.SaveChanges(machine, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteMachine(BaseInfo.Clock machine, UIActionType UAT)
        {
            return base.SaveChanges(machine, UAT);
        }


        #endregion
    }
}
