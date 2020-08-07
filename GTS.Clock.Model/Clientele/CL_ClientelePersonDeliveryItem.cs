using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Clientele
{
	public class CL_ClientelePersonDeliveryItem : IEntity
	{
		public virtual decimal ID { get; set; }

		public virtual CL_ClientelePerson ClientelePerson { get; set; }
		public virtual CL_DeliveryItem DeliveryItem { get; set; }

	}

}
