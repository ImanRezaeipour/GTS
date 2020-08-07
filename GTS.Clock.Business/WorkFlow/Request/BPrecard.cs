using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.Security;

namespace GTS.Clock.Business.RequestFlow
{
    public class BPrecard : BaseBusiness<Precard>
    {
        IDataAccess accessPort = new BUser();
        const string ExceptionSrc = "GTS.Clock.Business.RequestFlow.BPrecard";
        PrecardRepository precardRep = new PrecardRepository(false);

        public override IList<Precard> GetAll()
        {
            IList<decimal> ids = accessPort.GetAccessiblePrecards();
            IList<Precard> list = precardRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Precard().ID), ids.ToArray(), CriteriaOperation.IN));
            return list;
        }

        public IList<Precard> GetAllWithoutAthoriZe()
        {
            IList<Precard> list = base.GetAll();
            return list;
        }

        public IList<Precard> GetAllByPrecardGroup(decimal precadGrpId) 
        {
            IList<decimal> ids = accessPort.GetAccessiblePrecards();
            IList<Precard> list = precardRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Precard().PrecardGroup), new PrecardGroups() { ID = precadGrpId }),
                                                           new CriteriaStruct(Utility.GetPropertyName(() => new Precard().ID), ids.ToArray(), CriteriaOperation.IN));
            return list;
        }

        /// <summary>
        /// لیست همه گروهای پیشکارت را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<PrecardGroups> GetAllPrecardGroups()
        {
            try
            {
                EntityRepository<PrecardGroups> rep = new EntityRepository<PrecardGroups>(false);
                IList<PrecardGroups> list = rep.GetAll();
                if (list != null && list.Count > 0)
                {
                    list = list.OrderBy(x => x.Name).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrecard", "GetAllPrecardGroups");
                throw ex;
            }
        }

        /// <summary>
        /// کد پیشکارت نباید خالی باشد
        /// کد پیشکارت نباید تکراری باشد
        /// نام نباید خالی باشد
        /// نام تکراری نباشد
        /// دسته پیشکارت انتخاب شده باشد
        /// حتما یکی از سه وضعیت روزانه ساعتی مجوز باید انتخاب شده باشد
        /// </summary>
        /// <param name="precard"></param>
        protected override void InsertValidate(Precard precard)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if (Utility.IsEmpty(precard.Code))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardCodeRequierd, "کد پیشکارت نباید خالی باشد", ExceptionSrc));
            }
            else if (precardRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Precard().Code), precard.Code)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardCodeRepeated, "کد پیشکارت نباید تکراری باشد", ExceptionSrc));
            }
            if (Utility.IsEmpty(precard.Name)) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardNameRequierd, "نام پیشکارت نباید خالی باشد", ExceptionSrc));
            }
            else if (precardRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Precard().Name), precard.Name)) > 0) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardNameRepeated, "نام پیشکارت نباید تکراری باشد", ExceptionSrc));
            }
            if (precard.PrecardGroup == null || precard.PrecardGroup.ID == 0) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardGroupRequierd, "گروه پیشکارت باید مشخص شود", ExceptionSrc));
            }
            if (!(precard.IsHourly ^ precard.IsDaily ^ precard.IsMonthly))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardInvalidStatus, "وضعیت روزانه - ساعتی - مجوز نامعتبر است", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// کد پیشکارت نباید خالی باشد
        /// کد پیشکارت نباید تکراری باشد
        /// نام نباید خالی باشد
        /// نام تکراری نباشد
        /// دسته پیشکارت انتخاب شده باشد
        /// حتما یکی از سه وضعیت روزانه ساعتی مجوز باید انتخاب شده باشد
        /// </summary>
        /// <param name="precard"></param>
        protected override void UpdateValidate(Precard precard)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            //precard = base.GetByID(precard.ID);
            if (precardRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Precard().IsLock), true),
                                                   new CriteriaStruct(Utility.GetPropertyName(() => new Precard().ID), precard.ID)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardIsLock, "این پیشکارت قابل ویرایش نیست", ExceptionSrc));
            }
            if (Utility.IsEmpty(precard.Code))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardCodeRequierd, "کد پیشکارت نباید خالی باشد", ExceptionSrc));
            }
            else if (precardRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Precard().Code), precard.Code),
                                                   new CriteriaStruct(Utility.GetPropertyName(() => new Precard().ID), precard.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardCodeRepeated, "کد پیشکارت نباید تکراری باشد", ExceptionSrc));
            }
            if (Utility.IsEmpty(precard.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardNameRequierd, "نام پیشکارت نباید خالی باشد", ExceptionSrc));
            }
            else if (precardRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Precard().Name), precard.Name), 
                                                   new CriteriaStruct(Utility.GetPropertyName(() => new Precard().ID), precard.ID,CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardNameRepeated, "نام پیشکارت نباید تکراری باشد", ExceptionSrc));
            }
            if (precard.PrecardGroup == null || precard.PrecardGroup.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardGroupRequierd, "گروه پیشکارت باید مشخص شود", ExceptionSrc));
            }
            if (!(precard.IsHourly ^ precard.IsDaily ^ precard.IsMonthly))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardInvalidStatus, "وضعیت روزانه - ساعتی - مجوز نامعتبر است", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// در ترددها استفاده نشده باشد
        /// در جانشین استفاده نشده باشد
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(Precard precard)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            EntityRepository<BasicTraffic> baseTrafficRep = new EntityRepository<BasicTraffic>(false);
            if (precardRep.DoesUsedBySubestitute(precard.ID)) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardUsedBySubestitute, "این پیشکارت توسط جانشین مدیر استفاده شده است", ExceptionSrc));
            }
            if (baseTrafficRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new BasicTraffic().Precard), precard)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrecardUsedByBasicTraffic, "این پیشکارت توسط ترددها استفاده شده است", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPrecardsLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPrecard(Precard precard, UIActionType UAT)
        {
            return base.SaveChanges(precard, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdatePrecard(Precard precard, UIActionType UAT)
        {
            return base.SaveChanges(precard, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeletePrecard(Precard precard, UIActionType UAT)
        {
            return base.SaveChanges(precard, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateRoleAccess(decimal precardId, IList<Role> roleList)
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                if (precardId > 0)
                {
                    Precard precard = base.GetByID(precardId);
                    if (precard.AccessRoleList == null)
                    {
                        precard.AccessRoleList = new List<Role>();
                    }
                    precard.AccessRoleList.Clear();
                    foreach (Role role in roleList)
                    {
                        precard.AccessRoleList.Add(role);
                    }
                    this.SaveChanges(precard, UIActionType.EDIT);
                }
                else
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PrecardNotSpec, "پیشکارت مشخص نشده است", ExceptionSrc));
                }
            }
            catch (Exception ex) 
            {
                LogException(ex);
                throw ex;
            }
        }

    }
}
