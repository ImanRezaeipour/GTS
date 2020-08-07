using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business.Proxy
{
    public class LeaveIncDecProxy
    {
        public decimal ID { get; set; }       
        public string Day { get; set; }
        public string Hour { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public LeaveIncDecAction Action { get; set; }
        public string ActionTitle { get; set; }

    }
}
