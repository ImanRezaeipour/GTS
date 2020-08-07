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
	/// 		<term>Mahdi Payervand</term>
	/// 		<description>9/9/2012</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

	public class ConceptExpression : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal? Parent_ID { get; set; }

		/// <summary>
        /// Gets or sets the ScriptBeginFa value.
		/// </summary>
		public virtual String ScriptBeginFa { get; set; }

        /// Gets or sets the ScriptEndFa value.
        /// </summary>
        public virtual String ScriptEndFa { get; set; }

		/// <summary>
        /// Gets or sets the ScriptBeginEn value.
		/// </summary>
        public virtual String ScriptBeginEn { get; set; }

		/// <summary>
        /// Gets or sets the ScriptEndEn value.
		/// </summary>
        public virtual String ScriptEndEn { get; set; }

        /// <summary>
        /// Gets or sets the AddOnParentCreation value.
        /// </summary>
        public virtual Boolean AddOnParentCreation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Boolean CanAddToFinal { get; set; }
    
        /// <summary>
        /// 
        /// </summary>
        public virtual Boolean CanEditInFinal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Boolean Visible { get; set; }

        /// <summary>
        /// Gets or sets the SortOrder value.
        /// </summary>
        public virtual int SortOrder { get; set; }

	    public virtual int CustomeCategoryCode { get; set; }

        #endregion		
	}
}