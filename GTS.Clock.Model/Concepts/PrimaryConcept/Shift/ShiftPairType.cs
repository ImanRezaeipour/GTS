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
	/// 		<description>5/23/2011</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

    public class ShiftPairType : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

        public virtual bool Active { get; set; }

		/// <summary>
		/// Gets or sets the Name value.
		/// </summary>
        public virtual String Title { get; set; }

		/// <summary>
		/// Gets or sets the Min value.
		/// </summary>
		public virtual string Description { get; set; }        

		/// <summary>
		/// Gets or sets the CustomCode value.
		/// </summary>
		public virtual String CustomCode { get; set; }

        public virtual IList<ShiftPair> ShiftPairList { get; set; }
		#endregion		
	}
}