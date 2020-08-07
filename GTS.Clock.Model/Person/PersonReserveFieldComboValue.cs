using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.PersonInfo
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
	/// 		<description>2012/08/01</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

	public class PersonReserveFieldComboValue
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the ReserveFieldID value.
		/// </summary>
		public virtual decimal ReserveFieldID { get; set; }

		/// <summary>
		/// Gets or sets the ComboValue value.
		/// </summary>
		public virtual String ComboValue { get; set; }

		/// <summary>
		/// Gets or sets the ComboText value.
		/// </summary>
		public virtual String ComboText { get; set; }
		#endregion		
	}
}