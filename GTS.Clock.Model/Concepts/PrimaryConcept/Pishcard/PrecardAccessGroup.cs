using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.RequestFlow;

namespace GTS.Clock.Model.Concepts
{
	#region Comments	
	/// <h3>Changes</h3>
	/// 	<listheader>
	/// 		<th>Author</th>
	/// 		<th>Date</th>
	/// 		<th>Details</th>
	/// 	</listheader>
	/// 	<item>
	/// 		<term>Farhad Salavati</term>
	/// 		<description>5/23/2011</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

    public class PrecardAccessGroup : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the Name value.
		/// </summary>
		public virtual String Name { get; set; }

        public virtual String Description { get; set; }

        public virtual IList<Precard> PrecardList { get; set; }

        public virtual IList<Flow> FlowList { get; set; }
		#endregion		
	}
}