using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Diagnostics;
using System.Threading;
using GTS.Clock.Business.Engine;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.FeatureServices;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model.RequestFlow;

namespace GTS.Clock.Business.FeatureServices
{

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode =
        AspNetCompatibilityRequirementsMode.Required)]
    public class EmailWebService : IEmailWebService
    {
        GTSWinSvcLogger logger = new GTSWinSvcLogger();
        EmailUtility emailUtility = new EmailUtility();
        BUserSettings busUserSettings = new BUserSettings();
        NotificationServicesHistoryRepository historyRep = new NotificationServicesHistoryRepository(false);


        #region IEmailWebService Members

        /// <summary>
        /// آیتم های جدید کارتابل را ارسال میکند
        /// </summary>
        /// <param name="readyForSendEmail"></param>
        public void SendKartablEmails(IList<InfoServiceProxy> readyForSendEmail)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                var ids = from o in readyForSendEmail
                          select o.PersonId;
                IList<decimal> managerList = new ManagerRepository(false).GetAllManagerPersons(ids.ToList<decimal>());

                var readyPerson = from o in readyForSendEmail
                                  where managerList.Contains(o.PersonId)
                                  select o;
                IKartablRequests bus = new BKartabl();
                List<decimal> itemIds = new List<decimal>();
                foreach (InfoServiceProxy proxy in readyPerson.ToList<InfoServiceProxy>())
                {
                    try
                    {
                        IList<ContractKartablProxy> pendingList = bus.GetAllRequests(proxy.PersonId);
                        IList<decimal> kartableIds = new List<decimal>();
                        IList<decimal> notConfirmedids = new List<decimal>();

                        if (pendingList.Count > 0)
                        {
                            var list = from o in pendingList
                                       select o.ID;
                            kartableIds = list.ToList<decimal>();
                            kartableIds = this.GetNewNotifications(NotificationsServices.EMAILKartabl, kartableIds);
                            if (kartableIds.Count > 0)
                            {
                                string message = this.BuildKartablEmailString(kartableIds, proxy);
                                string subject = String.Format("گزارش وضعیت کارتابل در ساعت {0}:{1} - مخصوص مدیران", DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes);
                                emailUtility.SendEmail(proxy.EmailAddress, subject, message);
                                itemIds.AddRange(kartableIds);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(proxy.PersonCode, " EmailWebService :SendKartablEmails ->" + ex.Message, ex);
                        logger.Flush();
                    }
                }
                historyRep.InsertHistory(NotificationsServices.EMAILKartabl, DateTime.Now.Date, itemIds);
            }
            catch (Exception ex)
            {
                logger.Error("", " EmailWebService :SendKartablEmails ->" + ex.Message, ex);
                logger.Flush();
            }
        }

        /// <summary>
        /// تایید یا عدم تایید درخواستها را ارسال میکند
        /// </summary>
        public void SendRequestEmails(IList<InfoServiceProxy> readyForSendEmail)
        {
            try
            {
                IRegisteredRequests bus = new BKartabl();
                List<decimal> itemIds = new List<decimal>();
                foreach (InfoServiceProxy proxy in readyForSendEmail)
                {
                    try
                    {
                        IList<ContractKartablProxy> confirrmedList = bus.GetAllUserRequests(RequestState.Confirmed, DateTime.Now, DateTime.Now, proxy.PersonId);
                        IList<ContractKartablProxy> notConfirrmedList = bus.GetAllUserRequests(RequestState.Unconfirmed, DateTime.Now, DateTime.Now, proxy.PersonId);
                        IList<decimal> confirmedids = new List<decimal>();
                        IList<decimal> notConfirmedids = new List<decimal>();

                        if (confirrmedList.Count > 0)
                        {
                            var list = from o in confirrmedList
                                       select o.ID;
                            confirmedids = list.ToList<decimal>();
                            confirmedids = this.GetNewNotifications(NotificationsServices.EMAILRequestStatus, confirmedids);
                            if (confirmedids.Count > 0)
                            {
                                string message = this.BuildConfirmedRequestEmailString(confirmedids, proxy);
                                string subject = String.Format("گزارش وضعیت درخواستها در ساعت {0}:{1} - مخصوص کاربران", DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes);
                                emailUtility.SendEmail(proxy.EmailAddress, subject, message);
                                itemIds.AddRange(confirmedids);
                            }
                        }
                        if (notConfirrmedList.Count > 0)
                        {
                            var list = from o in notConfirrmedList
                                       select o.ID;
                            notConfirmedids = list.ToList<decimal>();
                            notConfirmedids = this.GetNewNotifications(NotificationsServices.EMAILRequestStatus, confirmedids);
                            if (notConfirmedids.Count > 0)
                            {
                                string message = this.BuildUnConfirmedRequestEmailString(notConfirmedids, proxy);
                                string subject = String.Format("گزارش وضعیت درخواستها در ساعت {0}:{1} - مخصوص کاربران", DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes);
                                emailUtility.SendEmail(proxy.EmailAddress, subject, message);
                                logger.Info(String.Format("Kartabl summery was sent to {0} at {1} - {2}", proxy.PersonName, Utility.ToPersianDate(DateTime.Now), DateTime.Now.TimeOfDay));
                                itemIds.AddRange(notConfirmedids);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(proxy.PersonCode, " EmailWebService :SendRequestEmails ->" + ex.Message, ex);
                        logger.Flush();
                    }
                }
                historyRep.InsertHistory(NotificationsServices.EMAILRequestStatus, DateTime.Now.Date, itemIds);
            }
            catch (Exception ex)
            {
                logger.Error("", " EmailWebService : SendRequestEmails ->" + ex.Message, ex);
                logger.Flush();
            }
        }

        public void SendTrafficEmails(IList<InfoServiceProxy> readyForSendEmail)
        {
            
        }    

        /// <summary>
        /// تنظیمات ایمیل کل سازمان را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<InfoServiceProxy> GetAllEmailSettings()
        {
            IList<InfoServiceProxy> list = busUserSettings.GetAllEmailSettings();
            return list;
        }

        public void RunEmailServices(IList<InfoServiceProxy> readyForSendEmail) 
        {
            this.SendKartablEmails(readyForSendEmail);
            this.SendRequestEmails(readyForSendEmail);
            this.SendTrafficEmails(readyForSendEmail);
        }

      
        #endregion


        #region private

        /// <summary>
        /// شناسه های جدید که برای آنها اطلاع رسانی نکرده ایم را برمیگرداند
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        private IList<decimal> GetNewNotifications(NotificationsServices serviceType, IList<decimal> ids)
        {

            IList<NotificationServicesHistory> list = historyRep.GetHistory(serviceType, DateTime.Now);

            var result = from o in ids
                         where !list.Select(x => x.ItemID).Contains(String.Format("{0}_{1}", o, serviceType))
                         select o;

            IList<decimal> newIds = result.ToList<decimal>();
            return newIds;
        }

        private string BuildUnConfirmedRequestEmailString(IList<decimal> items, InfoServiceProxy proxy)
        {
            string message = "";
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                message = String.Format("همکار گرامی {0} {1} شما {2} عدم تایید شده دارید", proxy.Sex == PersonSex.Male ? "جناب آقای " : "سرکار خانم", proxy.PersonName, items.Count);
            }
            else
            {
                message = String.Format("Dear colleague , {0} {1} , Your have {3} Unconfirmed Request", proxy.Sex == PersonSex.Male ? "Mr. " : "Mis.", proxy.PersonName, items.Count);
            }
            return message;
        }

        private string BuildConfirmedRequestEmailString(IList<decimal> items, InfoServiceProxy proxy)
        {
            string message = "";
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                message = String.Format("همکار گرامی {0} {1} شما {2} درخواست تایید شده دارید", proxy.Sex == PersonSex.Male ? "جناب آقای " : "سرکار خانم", proxy.PersonName, items.Count);
            }
            else
            {
                message = String.Format("Dear colleague , {0} {1} , Your have {3} confirmed Request", proxy.Sex == PersonSex.Male ? "Mr. " : "Mis.", proxy.PersonName, items.Count);
            }
            return message;
        }

        private string BuildKartablEmailString(IList<decimal> items, InfoServiceProxy proxy)
        {
            string message = "";
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                message = String.Format("همکار گرامی {0} {1} شما {2} آیتم بررسی نشده در کارتابل خود دارید", proxy.Sex == PersonSex.Male ? "جناب آقای " : "سرکار خانم", proxy.PersonName, items.Count);
            }
            else
            {
                message = String.Format("Dear colleague , {0} {1} , Your have {3} Under Review Items in your kartabl", proxy.Sex == PersonSex.Male ? "Mr. " : "Mis.", proxy.PersonName, items.Count);
            }
            return message;
        }

        #endregion

     
    }
}
