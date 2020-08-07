using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public class LeaveBudgetYear : LeaveYear
    {
        public LeaveBudgetYear()
        {
            this.UsedBudget = new UsedBudget();
            this.BudgetYear = new BudgetYear();
        }

    }
}
