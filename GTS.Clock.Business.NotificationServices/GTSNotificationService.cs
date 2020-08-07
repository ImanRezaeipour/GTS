using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using GTS.Clock.Infrastructure.Log;
//using GTS.Clock.Business.NotificationServices.WSEmailServices;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business.NotificationServices
{
    public partial class GTSNotificationService : ServiceBase
    {
        private static WSEmailWebService.EmailWebServiceClient wsEmailService =
            new WSEmailWebService.EmailWebServiceClient();

        private static WSSmsWebService.SmsWebServiceClient wsSmsService = new WSSmsWebService.SmsWebServiceClient();
        private static int Telorance = 1;
        private static GTSWinSvcLogger logger = new GTSWinSvcLogger();
        private static System.Timers.Timer schadulerTimer;
        private static bool firstTime = true;

        public GTSNotificationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            SessionHelper.SessionWorkSpace = SessionWorkSpace.WinService;

            schadulerTimer = new System.Timers.Timer();

            // Hook up the Elapsed event for the timer.
            schadulerTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            schadulerTimer.Interval = Telorance * 60000;
            schadulerTimer.Enabled = true;

            logger.Info("", "GTSNotification Service Starteded");
            schadulerTimer.Start();
        }

        protected override void OnStop()
        {
            schadulerTimer.Stop();
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {

            if (!firstTime)
            {
                logger.Info(String.Format("GTS Notification Service : OnTimer {0} - {1}", Utility.ToPersianDate(DateTime.Now), DateTime.Now.TimeOfDay));

                #region Email

                //InfoServiceProxy[] settings = wsEmailService.GetAllEmailSettings();
                var settings = wsEmailService.GetAllEmailSettings();
                IList<InfoServiceProxy> readyForSendInfo = new List<InfoServiceProxy>();

                foreach (InfoServiceProxy proxy in settings)
                {
                    if (Utility.IsEmpty(proxy.EmailAddress))
                        continue;
                    TimeSpan span = GetLastTimeEmailService(proxy.PersonId);

                    if (proxy.SendByDay)
                    {
                        //اولین بار است که ایمیل میفرستیم
                        if (span == ZeroSpan)
                        {
                            if (DateTime.Now.TimeOfDay == proxy.RepeatePeriod)
                            {
                                readyForSendInfo.Add(proxy);
                                SaveEmailServiceEvent(proxy.PersonId);
                            }
                        }
                        else if (proxy.RepeatePeriod.Add(new TimeSpan(0, 0, -1 * Telorance, 0)) <= span &&
                                 span <= proxy.RepeatePeriod.Add(new TimeSpan(0, 0, Telorance, 0)))
                        {
                            readyForSendInfo.Add(proxy);
                            SaveEmailServiceEvent(proxy.PersonId);
                        }
                    }
                    else
                    {
                        //اولین بار است که ایمیل میفرستیم
                        if (span == ZeroSpan)
                        {
                            readyForSendInfo.Add(proxy);
                            SaveEmailServiceEvent(proxy.PersonId);
                        }
                        else if (proxy.RepeatePeriod.Add(new TimeSpan(0, 0, -1 * Telorance, 0)) <= span
                                 &&
                                 proxy.RepeatePeriod.Add(new TimeSpan(0, 0, 1 * Telorance, 0)) >= span)
                        {
                            readyForSendInfo.Add(proxy);
                            SaveEmailServiceEvent(proxy.PersonId);
                        }
                    }
                }
                if (readyForSendInfo.Count > 0)
                {
                    //logger.Info(String.Format("{0} persons are emailing now {1} - {2}", readyForSendInfo.Count,
                    //                          Utility.ToPersianDate(DateTime.Now), DateTime.Now.TimeOfDay));
                    wsEmailService.RunEmailServices(readyForSendInfo.ToArray());
                    logger.Info(String.Format("{0} persons have Sent Email {1} - {2}", readyForSendInfo.Count,
                                              Utility.ToPersianDate(DateTime.Now), DateTime.Now.TimeOfDay));
                }               

                #endregion

                #region SMS

                //changed.
                var smsSettings = wsSmsService.GetAllSmsSettings();
                readyForSendInfo = new List<InfoServiceProxy>();

                foreach (InfoServiceProxy proxy in smsSettings)
                {
                    if (Utility.IsEmpty(proxy.SmsNumber))
                        continue;
                    TimeSpan span = GetLastTimeSmsService(proxy.PersonId);

                    if (proxy.SendByDay)
                    {
                        //اولین بار است که اس ام اس میفرستیم
                        if (span == ZeroSpan)
                        {
                            if (DateTime.Now.TimeOfDay == proxy.RepeatePeriod)
                            {
                                readyForSendInfo.Add(proxy);
                                SaveSmsServiceEvent(proxy.PersonId);
                            }
                        }
                        else if (proxy.RepeatePeriod.Add(new TimeSpan(0, 0, -1 * Telorance, 0)) <= span &&
                                 span <= proxy.RepeatePeriod.Add(new TimeSpan(0, 0, Telorance, 0)))
                        {
                            readyForSendInfo.Add(proxy);
                            SaveSmsServiceEvent(proxy.PersonId);
                        }
                    }
                    else
                    {
                        //اولین بار است که اس ام اس میفرستیم
                        if (span == ZeroSpan)
                        {
                            readyForSendInfo.Add(proxy);
                            SaveSmsServiceEvent(proxy.PersonId);
                        }
                        else if (proxy.RepeatePeriod.Add(new TimeSpan(0, 0, -1 * Telorance, 0)) <= span
                                 &&
                                 proxy.RepeatePeriod.Add(new TimeSpan(0, 0, 1 * Telorance, 0)) >= span)
                        {
                            readyForSendInfo.Add(proxy);
                            SaveSmsServiceEvent(proxy.PersonId);
                        }
                    }
                }
                if (readyForSendInfo.Count > 0)
                {
                    //logger.Info(String.Format("GTS Notification Service : {0} persons are sending sms now {1} - {2}", readyForSendInfo.Count,
                    //                          Utility.ToPersianDate(DateTime.Now), DateTime.Now.TimeOfDay));
                    wsSmsService.RunSmsServices(readyForSendInfo.ToArray());
                    logger.Info(String.Format("GTS Notification Service : {0} persons have Sent sms {1} - {2}", readyForSendInfo.Count,
                                              Utility.ToPersianDate(DateTime.Now), DateTime.Now.TimeOfDay));
                }               

                #endregion
            }
            else
            {
                firstTime = false;
            }
            logger.Flush();
        }

        private static void SaveEmailServiceEvent(decimal p)
        {
            SessionHelper.SaveSessionValue(SessionHelper.GTSNotificationServiceHistory + "EMAIL" + p.ToString(),
                                           DateTime.Now);
        }

        private static void SaveSmsServiceEvent(decimal p)
        {
            SessionHelper.SaveSessionValue(SessionHelper.GTSNotificationServiceHistory + "SMS" + p.ToString(),
                                           DateTime.Now);
        }

        private static TimeSpan GetLastTimeEmailService(decimal p)
        {
            object obj =
                SessionHelper.GetSessionValue(SessionHelper.GTSNotificationServiceHistory + "EMAIL" + p.ToString());
            if (obj == null)
                return ZeroSpan;
            return DateTime.Now - Convert.ToDateTime(obj);
        }

        private static TimeSpan GetLastTimeSmsService(decimal p)
        {
            object obj =
                SessionHelper.GetSessionValue(SessionHelper.GTSNotificationServiceHistory + "SMS" + p.ToString());
            if (obj == null)
                return ZeroSpan;
            return DateTime.Now - Convert.ToDateTime(obj);
        }

        public static TimeSpan ZeroSpan
        {
            get { return DateTime.Now - DateTime.Now; }
        }
    }
}
