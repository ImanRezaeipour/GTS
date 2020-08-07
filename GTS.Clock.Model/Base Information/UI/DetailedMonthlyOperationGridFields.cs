using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.BaseInformation;


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

    public class DetailedMonthlyOperationGridFields : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the MasterID value.
		/// </summary>
		public virtual MasterMonthlyOperationGridFields Master { get; set; }

		/// <summary>
		/// Gets or sets the ConceptID value.
		/// </summary>
        public virtual ExistConcepts Concept { get; set; }

		/// <summary>
		/// Gets or sets the FromHour value.
		/// </summary>
		public virtual String FromHour { get; set; }

		/// <summary>
		/// Gets or sets the ToHour value.
		/// </summary>
		public virtual String ToHour { get; set; }
		#endregion		
	}
}