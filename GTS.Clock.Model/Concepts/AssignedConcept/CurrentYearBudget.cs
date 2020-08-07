using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.Concepts
{
    public class CurrentYearBudget : IEntity
    {
        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual DateTime Date { get; set; }

        public virtual int Day { get; set; }

        //ظاهرا جهت استفاده مانند پرکسی
        public virtual int Minute { get; set; }

        public virtual string Description { get; set; }

        public virtual bool Applyed { get; set; }

        public virtual BudgetType Type { get; set; }

        public virtual RuleCategory RuleCategory { get; set; }

        public virtual decimal PersonId { get; set; }

        public virtual int Minutes { get; set; }

        public virtual string AsgFromDate { get; set; }

        public virtual string AsgToDate { get; set; }

        public virtual decimal BudgetId { get; set; }

        /// <summary>
        /// از پارامتر قانون مربوطه بارگزاری میشود
        /// </summary>
        public virtual int MinutesInDay { get; set; }

        /// <summary>
        /// از پارامتر قانون مربوطه بارگزاری میشود
        /// </summary>
        public virtual bool UseFutureLeave { get; set; }

        #endregion
    }
}
