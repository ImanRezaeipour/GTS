using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class KartablRequestHistoryProxy
    {
        public KartablRequestHistoryProxy() 
        {
            From = To = UesedInMonth = UesedInYear = RemainLeaveInMonth = RemainLeaveInYear = "";
        }

        public string From { get; set; }

        public string To { get; set; }

        public string UesedInMonth { get; set; }

        public string UesedInYear { get; set; }

        public string RemainLeaveInMonth { get; set; }

        public string RemainLeaveInYear { get; set; }

        public bool IsLeave { get; set; }
    }
}
