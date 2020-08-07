using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Exceptions
{
    public class ConfigNotProvided : BaseException
    {
        private string configKey = "";
        /// <summary>
        /// returns Config Key which was expected but is not provided
        /// </summary>
        public string ConfigKey { get { return configKey; } }

        public ConfigNotProvided(ExceptionType exType, string exceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.ConfigNotProvided", exceptionSource)
        {
            base.ExceptionTypeActivity = exType;
        }
        public ConfigNotProvided(string msg, ExceptionType exType, string exceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.ConfigNotProvided:" + msg, exceptionSource)
        {
            base.ExceptionTypeActivity = exType;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="cnfgKey">AppSetting Key for example which was expected</param>
        /// <param name="exType"></param>
        /// <param name="exceptionSource">مکان فیزیکی که خطا پرتاب شده است</param>
        public ConfigNotProvided(string msg, string cnfgKey, ExceptionType exType, string exceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.ConfigNotProvided:" + msg, exceptionSource)
        {
            base.ExceptionTypeActivity = exType;

            configKey = cnfgKey;
        }

        public override string GetLogMessage()
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendLine(base.GetLogMessage());
            msg.AppendLine(" Expected Config Key : " + ConfigKey);
            return msg.ToString();
        }
    }
}
