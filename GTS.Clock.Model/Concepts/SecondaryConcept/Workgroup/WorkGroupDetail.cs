using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	/// 		<description>6/6/2011</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

    public class WorkGroupDetail : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }	

		/// <summary>
		/// Gets or sets the Date value.
		/// </summary>
		public virtual DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the WorkGroupID value.
        /// </summary>
        public virtual WorkGroup WorkGroup { get; set; }

        /// <summary>
        /// Gets or sets the ShiftID value.
        /// </summary>
        public virtual Shift Shift { get; set; }

		#endregion		
	}
}