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
    public class SmsWebService : ISmsWebService
    {
        GTSWinSvcLogger logger = new GTSWinSvcLogger();
        SmsUtility SmsUtility = new SmsUtility();
        BUserSettings busUserSettings = new BUserSettings();
        NotificationServicesHistoryRepository historyRep = new NotificationServicesHistoryRepository(false);


        #region ISmsWebService Members

        /// <summary>
        /// آیتم های جدید کارتابل را ارسال میکند
        /// </summary>
        /// <param name="readyForSendSms"></param>
        public void SendKartablSmss(IList<InfoServiceProxy> readyForSendSms)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                var ids = from o in readyForSendSms
                          select o.PersonId;
                IList<decimal> managerList = new ManagerRepository(false).GetAllManagerPersons(ids.ToList<decimal>());

                var readyPerson = from o in readyForSendSms
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
                            kartableIds = this.GetNewNotifications(NotificationsServices.SmsKartabl, kartableIds);
                            if (kartableIds.Count > 0)
                            {
                                string message = this.BuildKartablSmsString(kartableIds, proxy);
                                string subject = String.Format("گزارش وضعیت کارتابل در ساعت {0}:{1} - مخصوص مدیران", DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes);
                                SmsUtility.SendSMS(proxy.SmsNumber, message);
                                itemIds.AddRange(kartableIds);
                                logger.Info(String.Format("SMS webservice : person {0} had Sent kartabl sms {1} - {2}", proxy.PersonName, Utility.ToPersianDate(DateTime.Now), DateTime.Now.TimeOfDay));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(proxy.PersonCode, " SmsWebService :SendKartablSmss ->" + ex.Message, ex);
                        logger.Flush();
                    }
                }
                historyRep.InsertHistory(NotificationsServices.SmsKartabl, DateTime.Now.Date, itemIds);
            }
            catch (Exception ex)
            {
                logger.Error("", " SmsWebService :SendKartablSmss ->" + ex.Message, ex);
                logger.Flush();
            }
        }

        /// <summary>
        /// تایید یا عدم تایید درخواستها را ارسال میکند
        /// </summary>
        public void SendRequestSmss(IList<InfoServiceProxy> readyForSendSms)
        {
            try
            {
                IRegisteredRequests bus = new BKartabl();
                List<decimal> itemIds = new List<decimal>();
                foreach (InfoServiceProxy proxy in readyForSendSms)
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
                            confirmedids = this.GetNewNotifications(NotificationsServices.SmsRequestStatus, confirmedids);
                            if (confirmedids.Count > 0)
                            {
                                string message = this.BuildConfirmedRequestSmsString(confirmedids, proxy);
                                string subject = String.Format("گزارش وضعیت درخواستها در ساعت {0}:{1} - مخصوص کاربران", DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes);
                                SmsUtility.SendSMS(proxy.SmsNumber, message);
                                itemIds.AddRange(confirmedids);
                                logger.Info(String.Format("SMS webservice : person {0} had Sent confirmed request sms {1} - {2}", proxy.PersonName, Utility.ToPersianDate(DateTime.Now), DateTime.Now.TimeOfDay));
                            }
                        }
                        if (notConfirrmedList.Count > 0)
                        {
                            var list = from o in notConfirrmedList
                                       select o.ID;
                            notConfirmedids = list.ToList<decimal>();
                            notConfirmedids = this.GetNewNotifications(NotificationsServices.SmsRequestStatus, confirmedids);
                            if (notConfirmedids.Count > 0)
                            {
                                string message = this.BuildUnConfirmedRequestSmsString(notConfirmedids, proxy);
                                string subject = String.Format("گزارش وضعیت درخواستها در ساعت {0}:{1} - مخصوص کاربران", DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes);
                                SmsUtility.SendSMS(proxy.SmsNumber, message);                                
                                itemIds.AddRange(notConfirmedids);
                                logger.Info(String.Format("SMS webservice : person {0} had Sent not confirmed request sms {1} - {2}", proxy.PersonName, Utility.ToPersianDate(DateTime.Now), DateTime.Now.TimeOfDay));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(proxy.PersonCode, " SmsWebService :SendRequestSmss ->" + ex.Message, ex);
                        logger.Flush();
                    }
                }
                historyRep.InsertHistory(NotificationsServices.SmsRequestStatus, DateTime.Now.Date, itemIds);
            }
            catch (Exception ex)
            {
                logger.Error("", " SmsWebService : SendRequestSmss ->" + ex.Message, ex);
                logger.Flush();
            }
        }

        public void SendTrafficSmss(IList<InfoServiceProxy> readyForSendSms)
        {
            
        }    

        public IList<InfoServiceProxy> GetAllSmsSettings()
        {
            IList<InfoServiceProxy> list = busUserSettings.GetAllSmsSettings();
            return list;
        }

        public void RunSmsServices(IList<InfoServiceProxy> readyForSendSms) 
        {            
            this.SendKartablSmss(readyForSendSms);
            this.SendRequestSmss(readyForSendSms);
            this.SendTrafficSmss(readyForSendSms);
            logger.Flush();
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

        private string BuildUnConfirmedRequestSmsString(IList<decimal> items, InfoServiceProxy proxy)
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

        private string BuildConfirmedRequestSmsString(IList<decimal> items, InfoServiceProxy proxy)
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

        private string BuildKartablSmsString(IList<decimal> items, InfoServiceProxy proxy)
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
