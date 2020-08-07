using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    public class RuleParametersValidationFeaturesProxy
    {
        public RuleParametersValidationType ValidationType { get; set; }
        public Rule RelativeRule { get; set; }
        public PersonRuleCatAssignment RelativePersonRuleCatAssignment { get; set; }
    }
}
