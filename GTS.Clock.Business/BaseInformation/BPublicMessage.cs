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
using GTS.Clock.Model.UI;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// created at: 3/4/2012 2:34:56 PM
    /// by        : Farhad Salvati
    /// write your name here
    /// </summary>
    public class BPublicMessage : BaseBusiness<PublicMessage>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BPublicMessage";
        private EntityRepository<PublicMessage> objectRep = new EntityRepository<PublicMessage>();

        #region BaseBusiness Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(PublicMessage msg)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(msg.Message))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PublicMessageContentRequierd, "متن پیام مشخص نشده است", ExceptionSrc));
            }
            if (Utility.IsEmpty(msg.Subject))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PublicMessageSubjecttRequierd, "موضوع پیام مشخص نشده است", ExceptionSrc));
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
        protected override void UpdateValidate(PublicMessage msg)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(msg.Message))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PublicMessageContentRequierd, "متن پیام مشخص نشده است", ExceptionSrc));
            }
            if (Utility.IsEmpty(msg.Subject))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PublicMessageSubjecttRequierd, "موضوع پیام مشخص نشده است", ExceptionSrc));
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
        protected override void DeleteValidate(PublicMessage msg)
        {
            UIValidationExceptions exception = new UIValidationExceptions();


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPublicNewsLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPublicNews(PublicMessage publicNews, UIActionType UAT)
        {
            return base.SaveChanges(publicNews, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdatePublicNews(PublicMessage publicNews, UIActionType UAT)
        {
            return base.SaveChanges(publicNews, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeletePublicNews(PublicMessage publicNews, UIActionType UAT)
        {
            return base.SaveChanges(publicNews, UAT);
        }

        #endregion
    }
}
