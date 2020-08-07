using GTS.Clock.Model.Concepts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.RequestFlow
{
    public class ImperativeRequest:IEntity, ICloneable
    {
        public virtual Decimal ID { get; set; }
        public virtual Person Person { get; set; }
        public virtual Precard Precard { get; set; }
        public virtual int Year { get; set; }
        public virtual int Month { get; set; }
        public virtual decimal Value { get; set; }
        public virtual bool IsLocked { get; set; }
        public virtual string Description { get; set; }

        public virtual object Clone()
        {
            ImperativeRequest imperativeRequest = new ImperativeRequest()
            {
                Precard = this.Precard,
                Person = this.Person,
                Year = this.Year,
                Month = this.Month,
                Value = this.Value,
                Description = this.Description
            };
            return imperativeRequest;
        }
    }
}
