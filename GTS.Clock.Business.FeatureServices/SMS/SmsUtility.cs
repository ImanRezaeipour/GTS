using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Business.FeatureServices.AlmasSmsSoap;

namespace GTS.Clock.Business.FeatureServices
{
    internal class SmsUtility
    {
        private GTSWinSvcLogger logger = new GTSWinSvcLogger();

        public void SendSMS(string to, string message)
        {
            AlmasSmsSoap.AlmasSmsSoapClient sms = new AlmasSmsSoap.AlmasSmsSoapClient();
            SendSmsResult result;
            try
            {
                long res1 = sms.SendSms3(out result, "nba7733", "9ykzx5", new string[] {message}, new string[] {to},
                                         null, null, null);
            }
            catch (Exception ex)
            {
                logger.Error("", ex.Message, ex);
                throw ex;
            }
        }
    }
}
