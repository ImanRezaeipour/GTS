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

    public class ExistConcepts : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the ConceptColorName value.
		/// </summary>
		public virtual String ConceptColorName { get; set; }

		/// <summary>
		/// Gets or sets the ConceptName value.
		/// </summary>
		public virtual String ConceptName { get; set; }

		/// <summary>
		/// Gets or sets the LookupKey value.
		/// </summary>
		public virtual String LookupKey { get; set; }
		#endregion		
	}
}