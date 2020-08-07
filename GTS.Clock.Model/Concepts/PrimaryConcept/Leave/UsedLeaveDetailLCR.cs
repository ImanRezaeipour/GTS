using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public class UsedLeaveDetailLCR : LeaveCalcResult
    {
        public virtual UsedLeaveDetail UsedLeaveDetail
        {
            get;
            set;
        }
    }
}
