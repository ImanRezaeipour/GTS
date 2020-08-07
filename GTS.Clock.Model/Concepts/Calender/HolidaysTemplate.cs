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
	/// 		<description>2011-12-01</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

	public class HolidaysTemplate
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
        /// Gets or sets the CalendarTypeId value.
        /// </summary>
        public virtual decimal CalendarTypeId { get; set; }
		#endregion		
	}
}