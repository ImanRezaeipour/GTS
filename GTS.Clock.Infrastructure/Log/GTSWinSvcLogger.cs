using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;



/// <summary>
/// Summary description for Logger
/// </summary>
/// 

namespace GTS.Clock.Infrastructure.Log
{
    public class GTSWinSvcLogger : BaseLog
    {
        /// <summary>
        /// فقط برای لاگ گرفتن ویندوز سرویس انجام محاسبات
        /// </summary>
        public GTSWinSvcLogger()
            : base(LogSource.WinSvcLogToDB)
        {

        }


        public void Info(object message)
        {
            ILog m_Log = GetLogFactory();            
            m_Log.Info(message);
            base.Flush();
        }
    }
}