using System;
using System.Collections.Generic;
using GTS.Clock.Model.Business;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model.Calculator
{
    public class RuleCalculatorFactory
    {
        #region Variables

        private ObjectCalculator ObjCalc;

        #endregion

        #region Properties

        public T GetCalculator<T>(Person person,
                                    CategorisedRule CategorisedRule,
                                    DateTime CalculationDate)
            where T : ObjectCalculator
        {
            switch (typeof(T).Name)
            {
                case "RuleCalculator":
                    {
                        if (this.ObjCalc != null)
                        {
                            this.ObjCalc.Person = person;
                            this.ObjCalc.CategorisedRule = CategorisedRule;
                            this.ObjCalc.CalculateDate = new PersianDateTime(CalculationDate);
                        }
                        else
                            ObjCalc = new RuleCalculator(person, CategorisedRule, CalculationDate);
                        break;
                    }
            }
            return (T)this.ObjCalc;
        }

        #endregion
    }
}
