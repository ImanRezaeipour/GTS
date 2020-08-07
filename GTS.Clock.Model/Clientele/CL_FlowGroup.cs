using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Clientele
{
  public  class CL_FlowGroup :IEntity
    {
        public virtual Decimal ID { get; set; }
        public virtual String Name { get; set; }
        public virtual String Description { get; set; }
    }
}
