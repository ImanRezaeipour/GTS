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
	/// 		<description>3/17/2012</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

	public class PrivateMessage:IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the Subject value.
		/// </summary>
		public virtual String Subject { get; set; }

		/// <summary>
		/// Gets or sets the Message value.
		/// </summary>
		public virtual String Message { get; set; }

		/// <summary>
		/// Gets or sets the Date value.
		/// </summary>
		public virtual DateTime Date { get; set; }

        public virtual String TheDate { get; set; }

		/// <summary>
		/// Gets or sets the FromPersonID value.
		/// </summary>
		public virtual Decimal FromPersonID { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual Decimal ToPersonID { get; set; }

		/// <summary>
		/// Gets or sets the ToPersonID value.
		/// </summary>
		public virtual Person ToPerson { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual Person FromPerson { get; set; }

		/// <summary>
		/// Gets or sets the Status value.
		/// </summary>
		public virtual Boolean Status { get; set; }

        /// <summary>
        /// فرستنده از سنت باکس حذف نکرده است
        /// </summary>
        public virtual Boolean FromActive { get; set; }
        
        /// <summary>
        /// دریافت کننده از اینباکس حذف نکرده است
        /// </summary>
        public virtual Boolean ToActive { get; set; }
		#endregion		
	}
}