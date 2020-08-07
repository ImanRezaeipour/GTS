using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Clientele
{
    public class BaseClienteleEnity : IClienteleEntity
    {
        public virtual Decimal ID { get; set; }

        public virtual bool IsDeleted { get; set; }
    }
}
