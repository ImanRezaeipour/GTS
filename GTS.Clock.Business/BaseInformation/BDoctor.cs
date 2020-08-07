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
    public class BDoctor : BaseBusiness<Doctor>
    {
        IDataAccess accessPort = new BUser();
        public BDoctor() { }
        const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BDoctor";
        private EntityRepository<Doctor> staionRepository = new EntityRepository<Doctor>(false);

        public override IList<Doctor> GetAll()
        {
            IList<decimal> ids = accessPort.GetAccessibleDoctors();
            IList<Doctor> list = staionRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Doctor().ID), ids.ToArray(), CriteriaOperation.IN));
            return list;
        }

        /// <summary>
        /// «اعتبارسنجی
        /// «نام نباید خالی باشد
        /// «نام  تکراری نباشد
        /// نظام پزشکی در صورت تهی نبودن باید یکتا باشد
        /// </summary>
        /// <param name="doctor"></param>
        protected override void InsertValidate(Doctor doctor)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(doctor.LastName))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DoctorLastNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            if (!Utility.IsEmpty(doctor.Nezampezaeshki))
            {
                if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => doctor.Nezampezaeshki), doctor.Nezampezaeshki)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DoctorNezampezeshkiRepeated, "درج - کد نظام پزشکی نباید تکراری باشد", ExceptionSrc));
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
        /// «نام نوع استخدام تکراری نباشد
        /// </summary>
        /// <param name="station"></param>
        protected override void UpdateValidate(Doctor doctor)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(doctor.LastName))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DoctorLastNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            if (!Utility.IsEmpty(doctor.Nezampezaeshki))
            {
                if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => doctor.Nezampezaeshki), doctor.Nezampezaeshki),
                                                        new CriteriaStruct(Utility.GetPropertyName(() => doctor.ID), doctor.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DoctorNezampezeshkiRepeated, "درج - کد نظام پزشکی نباید تکراری باشد", ExceptionSrc));
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
        protected override void DeleteValidate(Doctor doctor)
        {
           
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPhysiciansLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhyician(Doctor phyician, UIActionType UAT)
        {
            return base.SaveChanges(phyician, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdatePhyician(Doctor phyician, UIActionType UAT)
        {
            return base.SaveChanges(phyician, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeletePhyician(Doctor phyician, UIActionType UAT)
        {
            return base.SaveChanges(phyician, UAT);
        }

    }
}