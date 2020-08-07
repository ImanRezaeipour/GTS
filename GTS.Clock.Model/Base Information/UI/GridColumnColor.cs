using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GTS.Clock.Model.UI
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

    public class GridColumnColor : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the PersonID value.
		/// </summary>
		public virtual Person Person { get; set; }

		/// <summary>
		/// Gets or sets the RequestID value.
		/// </summary>
		public virtual RequestFlow.Request Request { get; set; }

		/// <summary>
		/// Gets or sets the Date value.
		/// </summary>
		public virtual String Date { get; set; }

		/// <summary>
		/// Gets or sets the ColumnName value.
		/// </summary>
		public virtual String ColumnName { get; set; }

		/// <summary>
		/// Gets or sets the Color value.
		/// </summary>
		public virtual System.Drawing.Color Color { get; set; }
		#endregion		
	}
}