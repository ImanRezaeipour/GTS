using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    public class BasicTrafficProxy
    {
        public decimal ID { get; set; }

        public string TheTime { get; set; }

        /// <summary>
        /// پیشکارت
        /// </summary>
        public string PrecardName { get; set; }

        public string ClockName { get; set; }

        public string OpName { get; set; }
    }
}
