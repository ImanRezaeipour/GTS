using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class DateRangeOrderProxy
    {       
        public int Order { get; set; }
        
        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public bool Selected { get; set; }
    }
}
