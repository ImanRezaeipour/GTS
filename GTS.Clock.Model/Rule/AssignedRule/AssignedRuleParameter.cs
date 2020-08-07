using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;

namespace GTS.Clock.Model
{
    public class AssignedRuleParameter : BaseRuleParameter
    {
        #region Properties

        public virtual DateTime FromDate
        {
            get;
            set;
        }

        public virtual DateTime ToDate
        {
            get;
            set;
        }

        /// <summary>
        /// load in query
        /// </summary>
        public virtual decimal RuleId { get; set; }

        #endregion

    }
}
