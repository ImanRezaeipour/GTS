using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GTS.Clock.Model.Charts
{
	
    public class DepartmentPosition : IEntity
	{
		#region Properties
	
		public virtual Decimal ID { get; set; }

        public virtual String UnitName { get; set; }

        public virtual String Location { get; set; }

		public virtual Department Department { get; set; }

		#endregion		
	}
}