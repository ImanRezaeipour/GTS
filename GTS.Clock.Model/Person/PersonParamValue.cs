using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Model.PersonInfo;


namespace GTS.Clock.Model.Rules
{
    public class PersonParamValue : IEntity
    {       
        #region Properties

        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        public virtual Person Person{get; set; }

        public virtual PersonParamField ParamField { get; set; }

        public virtual string Value { get; set; }

        public virtual DateTime FromDate { get; set; }

        public virtual DateTime ToDate { get; set; }

        public virtual string TheFromDate { get; set; }

        public virtual string TheToDate { get; set; }

        public override string ToString()
        {
            return String.Format("مقدار پارامتر با مشخصات شخص {0} و فیلد {1} , مقدار {2} تاریخ شروع{3} و تاریخ پایان {4} میباشد", this.Person.ID, this.ParamField.Key, this.Value, this.FromDate, this.ToDate);
        }

        #endregion
    }
}