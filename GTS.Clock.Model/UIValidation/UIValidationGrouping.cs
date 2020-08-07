using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

	public class UIValidationGrouping
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the RuleID value.
		/// </summary>
		public virtual Decimal RuleID { get; set; }

        public virtual Decimal GroupID { get; set; }

        public virtual Boolean Active { get; set; }

		/// <summary>
		/// Gets or sets the OperatorRestriction value.
		/// </summary>
		public virtual Boolean OperatorRestriction { get; set; }

        public virtual UIValidationGroup ValidationGroup { get; set; }

        public virtual UIValidationRule ValidationRule { get; set; }

        public virtual IList<UIValidationRuleParameter> RuleParameters { get; set; }

		#endregion		
	}
}