using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public class LeaveYearRemainLCR : LeaveCalcResult
    {
        public virtual LeaveYearRemain LeaveYearRemain
        {
            get;
            set;
        }
    }
}
