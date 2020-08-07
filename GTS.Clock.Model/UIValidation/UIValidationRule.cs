using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Model.UIValidation
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
	/// 		<description>4/4/2012</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

	public class UIValidationRule
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
		/// Gets or sets the Active value.
		/// </summary>
		public virtual Boolean Active { get; set; }

        /// <summary>
        /// ترتیب
        /// </summary>
        public virtual int Order { get; set; }

        public virtual int SubSystemId { get; set; }


        public virtual IList<UIValidationGrouping> GroupingList { get; set; }

        public virtual IList<Precard> PrecardList { get; set; }
        
		#endregion		
	}
}