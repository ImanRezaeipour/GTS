using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.RequestFlow;
namespace GTS.Clock.Model.RequestFlow
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

    public class Flow : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the AccessGroup value.
		/// </summary>
		public virtual PrecardAccessGroup AccessGroup { get; set; }

		/// <summary>
		/// Gets or sets the WorkFlow value.
		/// </summary>
		public virtual Boolean WorkFlow { get; set; }

		/// <summary>
		/// Gets or sets the ActiveFlow value.
		/// </summary>
		public virtual Boolean ActiveFlow { get; set; }

        public virtual Boolean MainFlow { get; set; }

        public virtual String FlowName { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual int PersonCount
        {
            get;
            set;
        }
        public virtual FlowGroup FlowGroup
        {
            get;
            set;
        }
        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual int DepartmentCount
        { get; set; }

        /// <summary>
        /// جهت استفاده در واسط کاربر
        /// </summary>
        public virtual bool IsAssignedToSubstitute { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual IList<ManagerFlow> ManagerFlowList { get; set; }

        public virtual IList<UnderManagment> UnderManagmentList { get; set; }

        public virtual IList<Operator> OperatorList { get; set; }

		#endregion		

        public override string ToString()
        {
            return String.Format("جریان کاری با شناسه {0} و نام {1}", this.ID, this.FlowName);
        }
	}
}