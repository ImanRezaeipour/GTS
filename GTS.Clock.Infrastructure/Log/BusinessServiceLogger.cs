using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;


/// <summary>
/// این لاگر بمنظور ثبت خطاها در سرویس های بیزینس مربوط به واسط کاربر
/// استفاده میشود
/// </summary>
/// 

namespace GTS.Clock.Infrastructure.Log
{  
    public class BusinessServiceLogger:BaseLog
    {
        const string UserName = "UserName";
        const string ClassName = "ClassName";
        const string MethodName = "MethodName";
        const string ExceptionSource = "ExceptionSource";
        const string ClientIPAddress = "ClientIPAddress";

        /// <summary>
        /// فقط برای لاگ گرفتن خطاهای بیزینس
        /// </summary>
        public BusinessServiceLogger()
            : base(LogSource.BusinessServiceErrorLog)
        {
          
        }

        public void Error(string username, string className, string methodName, string exceptionSource, object message, Exception exception)
        {
            string clientIPAddress = "";

            if (System.Web.HttpContext.Current != null &&
                System.Web.HttpContext.Current.Request != null)
            {
                if (System.Web.HttpContext.Current.Request.UserHostAddress != null)
                {
                    clientIPAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
            }

            ILog m_Log = base.GetLogFactory();
            log4net.GlobalContext.Properties[UserName] = username;
            log4net.GlobalContext.Properties[ClassName] = className;
            log4net.GlobalContext.Properties[MethodName] = methodName;
            log4net.GlobalContext.Properties[ExceptionSource] = exceptionSource;
            log4net.GlobalContext.Properties[ClientIPAddress] = clientIPAddress;
            m_Log.Error(message, exception);
            base.Flush();
            log4net.GlobalContext.Properties[UserName] = "";
            log4net.GlobalContext.Properties[ClassName] = "";
            log4net.GlobalContext.Properties[MethodName] = "";
            log4net.GlobalContext.Properties[ExceptionSource] = "";
            log4net.GlobalContext.Properties[ClientIPAddress] = "";
        }

        public void Info(string username, string className, string methodName, string exceptionSource, object message, Exception exception)
        {
            string clientIPAddress = "";

            if (System.Web.HttpContext.Current != null &&
                System.Web.HttpContext.Current.Request != null)
            {
                if (System.Web.HttpContext.Current.Request.UserHostAddress != null)
                {
                    clientIPAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
            }

            ILog m_Log = base.GetLogFactory();
            log4net.GlobalContext.Properties[UserName] = username;
            log4net.GlobalContext.Properties[ClassName] = className;
            log4net.GlobalContext.Properties[MethodName] = methodName;
            log4net.GlobalContext.Properties[ExceptionSource] = exceptionSource;
            log4net.GlobalContext.Properties[ClientIPAddress] = clientIPAddress;
            m_Log.Info(message, exception);
            base.Flush();
            log4net.GlobalContext.Properties[UserName] = "";
            log4net.GlobalContext.Properties[ClassName] = "";
            log4net.GlobalContext.Properties[MethodName] = "";
            log4net.GlobalContext.Properties[ExceptionSource] = "";
            log4net.GlobalContext.Properties[ClientIPAddress] = "";
        }
    }
}