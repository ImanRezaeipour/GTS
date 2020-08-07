using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GTS.Clock.Model.Concepts
{
    public class BudgetLCR : LeaveCalcResult
    {
        public BudgetLCR()
        {

        }

        public BudgetLCR(CurrentYearBudget CYB)
        {
            this.Budget = new Budget()
            {
                ID = CYB.BudgetId,
                Date = CYB.Date,
                Day = CYB.Day,
                Minute = CYB.Minutes,
                RuleCategory = CYB.RuleCategory,
                Description = CYB.Description,
                Type = CYB.Type,
                Applyed = true
            };
        }

        public virtual Budget Budget
        {
            get;
            set;
        }
    }
}
