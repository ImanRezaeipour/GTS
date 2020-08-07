using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;

namespace GTS.Clock.Model
{
    public class RuleParameter: BaseRuleParameter
    {
        #region Properties

        public virtual AssignRuleParameter AssignRuleParameter
        {
            get;
            set;
        }

        #endregion

    }
}
