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
    public class GTSEngineLogger : BaseLog
    {
        /// <summary>
        /// فقط برای لاگ گرفتن قوانین
        /// </summary>
        public GTSEngineLogger()
            : base(LogSource.RuleLoggerToDB)
        {

        }
    }
}