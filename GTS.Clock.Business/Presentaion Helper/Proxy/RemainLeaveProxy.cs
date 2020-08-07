using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;

namespace GTS.Clock.Business.Proxy
{
    public class RemainLeaveProxy
    {
        public decimal ID { get; set; }
        public Person Person { get; set; }
        public decimal Year { get; set; }
        public string RealDay { get; set; }
        public string RealHour { get; set; }
        public string ConfirmedDay { get; set; }
        public string ConfirmedHour { get; set; }

    }
}
