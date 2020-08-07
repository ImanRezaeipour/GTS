using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    public class RequestStatus : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

        public virtual String Description { get; set; }

        public virtual DateTime Date { get; set; }

		/// <summary>
		/// Gets or sets the MnagerFlow value.
		/// </summary>
		public virtual ManagerFlow ManagerFlow { get; set; }

		/// <summary>
		/// Gets or sets the Confirm value.
		/// </summary>
		public virtual Boolean Confirm { get; set; }

		/// <summary>
		/// Gets or sets the EndFlow value.
		/// </summary>
		public virtual Boolean EndFlow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Boolean IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the Request value.
		/// </summary>
		public virtual Request Request { get; set; }
		#endregion		
	}

    public class RequestStatusEualityComparer : IEqualityComparer<RequestStatus>
    {
        public bool Equals(RequestStatus x, RequestStatus y)
        {
            return x.ManagerFlow.Manager.ID == y.ManagerFlow.Manager.ID;
        }

        public int GetHashCode(RequestStatus obj)
        {
            return obj.ManagerFlow.Manager.ID.GetHashCode();
        }
    }

}