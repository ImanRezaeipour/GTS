using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GTS.Clock.Model.Charts
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

    public class Unit : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the ChartId value.
		/// </summary>
		public virtual Decimal ChartId { get; set; }

		/// <summary>
		/// Gets or sets the Name value.
		/// </summary>
		public virtual String Name { get; set; }

		/// <summary>
		/// Gets or sets the ParentId value.
		/// </summary>
		public virtual Decimal ParentId { get; set; }

		/// <summary>
		/// Gets or sets the Path value.
		/// </summary>
		public virtual String Path { get; set; }

		/// <summary>
		/// Gets or sets the CustomCode value.
		/// </summary>
		public virtual String CustomCode { get; set; }

        public virtual Unit Parent
        {
            get;
            set;
        }
        public virtual IList<Unit> ChildList
        {
            get;
            set;
        }
        public virtual IList<Person> PersonList
        {
            get;
            set;
        }

		#endregion		
	}
}