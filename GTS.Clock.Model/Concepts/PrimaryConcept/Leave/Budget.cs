using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model.Concepts
{
    public class Budget : IEntity
    {

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual DateTime Date { get; set; }

        public virtual int Day { get; set; }

        public virtual int Minute { get; set; }

        public virtual string Description { get; set; }

        public virtual bool Applyed { get; set; }

        public virtual BudgetType Type { get; set; }

        public virtual RuleCategory RuleCategory { get; set; }

        #endregion 

        public override string ToString()
        {
            string summery = "";
            summery = String.Format("بودجه مرخصی قانون :{0} تاریخ:{1} روز :{2} ساعت :{3} نوع:{4} میباشد ", this.RuleCategory.Name, Utility.ToPersianDate(this.Date), this.Day, Utility.IntTimeToRealTime(this.Minute), this.Type.ToString());
            return summery;
        }
    }
}
