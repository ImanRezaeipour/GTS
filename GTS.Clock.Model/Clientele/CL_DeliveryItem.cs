using System;
using System.Collections.Generic;

namespace GTS.Clock.Model.Clientele
{
	public class CL_DeliveryItem : IEntity
	{
		public virtual decimal ID { get; set; }
		public virtual string Title { get; set; }
		public virtual string CustomCode { get; set; }
        public virtual string Description { get; set; }
        public virtual string Image { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual bool IsReturn { get; set; }

		public virtual CL_Contractor Contractor { get; set; }
		public virtual CL_OffishRequest Offish { get; set; }

        public virtual List<decimal> ClDeliveryItemIdList { get; set; }
        public virtual List<decimal> ClClientelePersonIdList { get; set; }
        public virtual bool IsReturnAll { get; set; }
    }
}
