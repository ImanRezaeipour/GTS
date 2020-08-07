using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;

namespace GTS.Clock.Model
{
    public class AssignRuleParameter : IEntity
    {
        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

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

        public virtual String TheFromDate { get; set; }

        public virtual String TheToDate { get; set; }

        public virtual IList<RuleParameter> RuleParameterList
        {
            get;
            set;
        }

        public virtual Rule Rule
        {
            get;
            set;
        }

        #endregion

    }
}
