using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business.Security;
using GTS.Clock.Model;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business
{
    public class BHelp : BaseBusiness<Help>
    {
        const string ExceptionSrc = "GTS.Clock.Business.BHelp";
        private HelpRepository helpRepository = new HelpRepository(false);

        public override IList<Help> GetAll()
        {
            try
            {
                throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نمیباشد", ExceptionSrc);
            }
            catch (Exception ex)
            {
                LogException(ex, "BHelp", "GetAll");
                throw ex;
            }

        }

        /// <summary>
        /// برگرداندن ریشه درخت
        /// </summary>
        /// <returns></returns>
        public HelpProxy GetHelpRoot()
        {
            try
            {
                IList<Help> helpList = helpRepository.GetHelpTree();
                if (helpList.Count > 1)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.HelpRootIsMorThanOne, "Help Root more than one", ExceptionSrc);
                }
                Help help = helpList.FirstOrDefault();
                HelpProxy proxy = new HelpProxy();

                if (helpList != null)
                {
                    proxy.ID = help.ID;
                    proxy.FormKey = help.FormKey;
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        proxy.Name = help.FaName;
                        proxy.HtmlCotent = help.FaHTMLContent;
                    }
                    else
                    {
                        proxy.Name = help.EnName;
                        proxy.HtmlCotent = help.EnHTMLContent;
                    }
                }
                return proxy;
            }
            catch (Exception ex)
            {
                LogException(ex, "BHelp", "GetHelpRoot");
                throw ex;
            }
        }

        /// <summary>
        /// برگرداندن گرهای یک والد
        /// </summary>
        /// <param name="helpId"></param>
        /// <returns></returns>
        public IList<HelpProxy> GetHelpChilds(decimal helpId)
        {
            try
            {
                IList<Help> helpList = helpRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Help().Parent), new Help() { ID = helpId }));
                IList<HelpProxy> proxyList = new List<HelpProxy>();

                foreach (Help child in helpList.OrderBy(x=>x.LevelOrder).ToList())
                {

                    HelpProxy proxy = new HelpProxy();

                    if (helpList != null)
                    {
                        proxy.ID = child.ID;
                        proxy.FormKey = child.FormKey;
                        proxy.ParentId = helpId;
                        if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                        {
                            proxy.Name = child.FaName;
                            proxy.HtmlCotent = child.FaHTMLContent;
                        }
                        else
                        {
                            proxy.Name = child.EnName;
                            proxy.HtmlCotent = child.EnHTMLContent;
                        }

                    }
                    proxyList.Add(proxy);
                }
                return proxyList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BHelp", "GetHelpChilds");
                throw ex;
            }
        }

        /// <summary>
        /// یک کلید دریافت میکند و محتوا را برمیگرداند
        /// </summary>
        /// <param name="FormKey"></param>
        /// <returns></returns>
        public HelpProxy GetHelpByFormKey(string FormKey)
        {
            try
            {
                HelpProxy proxy = new HelpProxy();
                Help hlp = null;

                hlp = helpRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName((() => new Help().FormKey)), FormKey)).FirstOrDefault();
                if (hlp == null)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.HelpFormKeyDoesNotExists, String.Format("راهنمایی برای فرم با کلید {0} موجود نیست", FormKey), "GTS.Clock.Business.BHelp");
                }
                proxy.ID = hlp.ID;
                proxy.FormKey = hlp.FormKey;
                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    proxy.Name = hlp.FaName;
                    proxy.HtmlCotent = hlp.FaHTMLContent;
                }
                else
                {
                    proxy.Name = hlp.EnName;
                    proxy.HtmlCotent = hlp.EnHTMLContent;
                }
                return proxy;

            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }
     

        public void UpdateHelp(HelpProxy proxy) 
        {
            try
            {
                Help help = base.GetByID(proxy.ID);

                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    help.FaHTMLContent = proxy.HtmlCotent;
                }
                else 
                {
                    help.EnHTMLContent = proxy.HtmlCotent;
                }
                this.SaveChanges(help, UIActionType.EDIT);
            }
            catch (Exception ex) 
            {
                LogException(ex, "BHelp", "UpdateHelp");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(Help obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نمیباشد", ExceptionSrc);

            if (exception.Count > 0)
            {
                throw exception;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="help"></param>
        protected override void UpdateValidate(Help help)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (help.ID == 0) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.HelpIdNotSpecified, "شناسه مشخص نشده است", ExceptionSrc));
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
        protected override void DeleteValidate(Help obj)
        {

            UIValidationExceptions exception = new UIValidationExceptions();

            throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نمیباشد", ExceptionSrc);

            if (exception.Count > 0)
            {
                throw exception;
            }
        }
    }
}

