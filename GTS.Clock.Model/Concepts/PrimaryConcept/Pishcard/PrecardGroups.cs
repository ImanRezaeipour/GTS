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

    public class PrecardGroups : IEntity
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
		/// Gets or sets the LookupKey value.
		/// </summary>
		public virtual String LookupKey { get; set; }

        /// <summary>
        /// Gets or sets the LookupKey value.
        /// </summary>
        public virtual int IntLookupKey { get; set; }

        /// <summary>
        /// جهت نمایش درخت همراه با چک باکس در واسط کاربر
        /// </summary>
        public virtual bool ContainInPrecardAccessGroup
        {
            get;
            set;
        }

        public virtual IList<Precard> PrecardList { get; set; }
		#endregion		
	}
}