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
    public class BusinessActivityLogger : BaseLog
    {
        const string UserName = "UserName";
        const string ClassName = "ClassName";
        const string MethodName = "MethodName";
        const string Action = "Action";
        const string ClientIPAddress = "ClientIPAddress";
        const string PageId = "PageId";
        const string ObjectInfo = "ObjectInfo";


        public BusinessActivityLogger()
            : base(LogSource.UserActivityLog)
        {

        }
        
        public void Info(string username, string className, string methodName,  string action,string pageId,string clientIP,string objectInfo)
        {
            
            ILog m_Log = base.GetLogFactory();
            log4net.GlobalContext.Properties[UserName] = username;
            log4net.GlobalContext.Properties[ClassName] = className;
            log4net.GlobalContext.Properties[MethodName] = methodName;
            log4net.GlobalContext.Properties[Action] = action;
            log4net.GlobalContext.Properties[ClientIPAddress] = clientIP;
            log4net.GlobalContext.Properties[PageId] = pageId;
            log4net.GlobalContext.Properties[ObjectInfo] = objectInfo;
            m_Log.Info("");
            base.Flush();
            log4net.GlobalContext.Properties[UserName] = "";
            log4net.GlobalContext.Properties[ClassName] = "";
            log4net.GlobalContext.Properties[MethodName] = "";
            log4net.GlobalContext.Properties[Action] = "";
            log4net.GlobalContext.Properties[ClientIPAddress] = "";
            log4net.GlobalContext.Properties[PageId] = "";
            log4net.GlobalContext.Properties[ObjectInfo] = "";
        }
    }
}