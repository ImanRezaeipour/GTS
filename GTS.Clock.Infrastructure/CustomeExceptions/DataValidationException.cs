using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions.UI;

namespace GTS.Clock.Infrastructure.Exceptions
{
    public class DataValidationException : BaseException
    {
        string className = "";
        public string EntiryName
        {
            get { return className; }
        }

        /// <summary>
        /// کد برای شناسایی پیغام به زبانهای مختلف در واسط کاربر
        /// </summary>
        public ExceptionKeyCodes ExceptionKeyCode
        {
            get;
            set;
        }

        public DataValidationException(ExceptionType exType, string exceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.DataValidationException", exceptionSource)
        {
            base.ExceptionTypeActivity = exType;
        }

        public DataValidationException(string msg, ExceptionType exType, string exceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.DataValidationException:" + msg, exceptionSource)
        {
            base.ExceptionTypeActivity = exType;
        }

        public DataValidationException(string msg, string entityName, ExceptionType exType, string exceptionSource)
            : base("GTS.Clock.Infrastructure.Exceptions.DataValidationException", exceptionSource)
        { 
            base.ExceptionTypeActivity = exType;
            className = entityName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exceptionKeyCode">کد برای شناسایی پیغام به زبانهای مختلف در واسط کاربر</param>        
        public DataValidationException(string msg, ExceptionKeyCodes exceptionKeyCode)
            : base("GTS.Clock.Infrastructure.Exceptions.DataValidationException", "")
        {
            ExceptionKeyCode = exceptionKeyCode;
            base.ExceptionTypeActivity = ExceptionType.ALARM;
            className = "";
        }

        public override string GetLogMessage()
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendLine(base.GetLogMessage());
            msg.AppendLine(" Entity Information : " + EntiryName);
            return msg.ToString();
        }
    }
}