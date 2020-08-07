using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.BaseInformation
{
   public class Grade :IEntity
    {

        public virtual Decimal ID { get; set; }

        public virtual string   Name { get; set; }
        public virtual string Description { get; set; }
    }
}
