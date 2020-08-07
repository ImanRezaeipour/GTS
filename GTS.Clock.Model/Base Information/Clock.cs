using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GTS.Clock.Model.BaseInformation
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

    public class Clock : IEntity
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

		/// <summary>
		/// Gets or sets the CustomCode value.
		/// </summary>
		public virtual String CustomCode { get; set; }

		/// <summary>
		/// Gets or sets the Clocktype value.
		/// </summary>
		public virtual ClockType Clocktype { get; set; }

		/// <summary>
		/// Gets or sets the Station value.
		/// </summary>
		public virtual ControlStation Station { get; set; }

		/// <summary>
		/// Gets or sets the Tel value.
		/// </summary>
		public virtual String Tel { get; set; }

		/// <summary>
		/// Gets or sets the Active value.
		/// </summary>
		public virtual Boolean Active { get; set; }
		#endregion		
	}
}