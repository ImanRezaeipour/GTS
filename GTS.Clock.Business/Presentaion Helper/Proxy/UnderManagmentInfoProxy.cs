using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class CalcInfoProxy 
    {
        public string OverTime { get; set; }
        public string HourlyAbsence { get; set; }
        public string DailyAbsence { get; set; }
        public string HourlyLeave{ get; set; }
        public string DailyLeave{ get; set; }


    }
    public class UnderManagmentInfoProxy
    {
        public decimal PersonID { get; set; }

        public string PersonName { get; set; }

        public string PersonCode { get; set; }

        public CalcInfoProxy CalcInfo { get; set; }

    }
}
