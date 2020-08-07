using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class UIValidationGroupingProxy
    {
        public decimal ID { get; set; }

        public decimal RuleID { get; set; }

        public decimal GroupID { get; set; }

        public string RuleName { get; set; }

        public bool OpratorRestriction { get; set; }

        public bool Active { get; set; }        
    }
}
