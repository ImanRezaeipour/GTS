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
    public class BEmployment : BaseBusiness<EmploymentType>
    {
        public BEmployment() { }
        const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BEmployment";
        private EntityRepository<EmploymentType> emplRepository = new EntityRepository<EmploymentType>(false);

        /// <summary>
        /// «اعتبارسنجی
        /// «نام نباید خالی باشد
        /// «نام نوع استخدام تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="empl"></param>
        protected override void InsertValidate(EmploymentType empl)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(empl.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (emplRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => empl.Name), empl.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }

            if (!Utility.IsEmpty(empl.CustomCode))
            {
                if (emplRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => empl.CustomCode), empl.CustomCode)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeCustomCodeRepeated, "درج - کد نوع استخدام نباید تکراری باشد", ExceptionSrc));
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
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="empl"></param>
        protected override void UpdateValidate(EmploymentType empl)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(empl.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeNameRequierd, "نام نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (emplRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => empl.Name), empl.Name),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => empl.ID), empl.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeNameRepeated, "نام نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (!Utility.IsEmpty(empl.CustomCode))
            {
                if (emplRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => empl.CustomCode), empl.CustomCode),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => empl.ID), empl.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeCustomCodeRepeated, "بروزرسانی - کد نوع استخدام نباید تکراری باشد", ExceptionSrc));
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
        protected override void DeleteValidate(EmploymentType empl)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            PersonRepository rep = new PersonRepository(false);

            if (rep.GetCountByCriteria(new CriteriaStruct(/*Utility.GetPropertyName(() => new Person().EmploymentType)*/"employmentType", empl)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeUsedByPerson, "بدلیل استفاده در پرسنل نباید حذف شود", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckEmployTypesLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertEmploymentType(EmploymentType employmentType, UIActionType UAT)
        {
            return base.SaveChanges(employmentType, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateEmploymentType(EmploymentType employmentType, UIActionType UAT)
        {
            return base.SaveChanges(employmentType, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteEmploymentType(EmploymentType employmentType, UIActionType UAT)
        {
            return base.SaveChanges(employmentType, UAT);
        }

    }
}