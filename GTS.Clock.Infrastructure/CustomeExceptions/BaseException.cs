using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Log;

namespace GTS.Clock.Infrastructure.Exceptions
{
    public enum ExceptionType
    {
        ALARM, CRASH, REDIRECT,FATAL
    };
    public class BaseException : Exception
    {
        #region Variables
        ExceptionType exceptionType;
        string excptionSource = "";
        #endregion

        #region Constructors
        /// <summary>
        /// سازنده کلاس پایه برای خطاها
        /// </summary>
        /// <param name="msg">پیغام خطا</param>
        /// <param name="exceptionSrc">مکانی که در آن خطا پرتاب شده است</param>
        public BaseException(string msg, string exceptionSrc)
            : base(msg)
        {
            excptionSource = exceptionSrc;
        }

        /// <summary>
        /// سازنده کلاس پایه برای خطاها
        /// </summary>
        /// <param name="msg">پیغام خطا</param>
        /// <param name="exceptionSrc">مکانی که در آن خطا پرتاب شده است</param>
        /// <param name="InnerException">خطای مسبب این خطا</param>
        public BaseException(string msg, string exceptionSrc, Exception InnerException)
            : base(msg, InnerException)
        {
            excptionSource = exceptionSrc;
        }

        #endregion

        #region Properties

        /// <summary>
        /// مکانی که در آن این خطا پرتاب شده است
        /// </summary>
        public string ExceptionSource
        {
            get { return excptionSource; }
        }

        public ExceptionType ExceptionTypeActivity
        {
            get { return (ExceptionType)exceptionType; }
            set { exceptionType = value; }
        }

        /// <summary>
        /// آیا این خطا در لاگ ذخیره شده است
        /// </summary>
        public bool InsertedLog { get; set; }


        #endregion

        #region Methods


        /// <summary>
        /// باید تمامی خصیصه ها که لازم است در هنگام لاگ گرفتن بیاید در اینجا بصورت بهمچسبیده
        /// Concat
        /// برگردانده شود
        /// </summary>
        /// <returns></returns>
        public virtual string GetLogMessage()
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendLine("Exception Source: " + excptionSource);
            msg.AppendLine(" Message: " + Message + GetExceptionMessage(base.InnerException));
            return msg.ToString();
        }

        private string GetExceptionMessage(Exception ex)
        {
            string msg = " \n ";
            if (ex != null && ex.Message != null && ex.Message.Length > 0)
            {
                msg +=" --> "+ ex.Message;
            }
            if (ex != null && ex.InnerException != null)
            {
                msg += " \n " + GetExceptionMessage(ex.InnerException);
            }

            return msg;
        }

        public static void GetLog(GTSEngineLogger gtsRuleLogger, string personCode, Exception ex)
        {
            if (ex is BaseException)
            {
                if (!((BaseException)ex).InsertedLog)
                {
                    ((BaseException)ex).InsertedLog = true;
                    gtsRuleLogger.Error(personCode, ex.Message, ex);
                    gtsRuleLogger.Flush();
                }
            }
            else 
            {
                gtsRuleLogger.Error(personCode, ex.Message, ex);
                gtsRuleLogger.Flush();
            }
        }

        public static void GetLog(GTSEngineLogger gtsRuleLogger, string personCode, Exception ex, string message)
        {          
            message = personCode + " " + message;
            if (ex is BaseException)
            {               
                if (!((BaseException)ex).InsertedLog)
                {
                    ((BaseException)ex).InsertedLog = true;
                    gtsRuleLogger.Logger.Error(message, ex);   
                    gtsRuleLogger.Flush();
                }
            }
            else
            {
                gtsRuleLogger.Logger.Error(message, ex);               
                gtsRuleLogger.Flush();
            }
        }
        #endregion


    }
}
