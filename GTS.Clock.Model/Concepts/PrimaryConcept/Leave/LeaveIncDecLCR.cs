using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public class LeaveIncDecLCR : LeaveCalcResult
    {
        public virtual LeaveIncDec LeaveIncDec
        {
            get;
            set;
        }
    }
}
