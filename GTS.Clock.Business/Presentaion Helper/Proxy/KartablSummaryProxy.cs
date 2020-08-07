using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    public class KartablSummaryProxy
    {
        public String MainRecievedRequestCount { get; set; }

        public String SubstituteRecievedRequestCount { get; set; }

        public String ConfirmedRequestCount { get; set; }

        public String NotConfirmedRequestCount { get; set; }

        public String InFlowRequestCount { get; set; }

        public String PrivateMessageCount { get; set; }
    }
}
