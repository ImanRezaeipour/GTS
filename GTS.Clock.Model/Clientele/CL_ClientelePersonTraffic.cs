using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.Clientele
{
    public class CL_ClientelePersonTraffic : IEntity
    {
        public virtual Decimal ID { get; set; }

        public virtual DateTime? TrafficDate { get; set; }

        public virtual int? TrafficTime { get; set; }


        public virtual string Description { get; set; }
        
        public virtual CL_ClientelePerson ClientelePerson { get; set; }

        public virtual CL_Contractor Contractor { get; set; }

        public virtual CL_OffishRequest OffishRequest { get; set; }

        public virtual bool IsManual { get; set; }


        #region Not Mapped

        public virtual string TheTrafficDate { get; set; }
        public virtual string TheTrafficTime { get; set; }

        #endregion

    }
}
