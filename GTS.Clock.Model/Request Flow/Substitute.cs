using System;
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
	/// 		<description>5/24/2011</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

    public class Substitute : IEntity,ICloneable
	{
        public Substitute()
        {
            TheFromDate = "";
            TheToDate = "";
        }

		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the Manager value.
		/// </summary>
		public virtual Manager Manager { get; set; }

		/// <summary>
		/// Gets or sets the Person value.
		/// </summary>
		public virtual Person Person { get; set; }

        /// <summary>
        /// جهت استفاده در واسط کاربر
        /// وقتی در واسط کاربر بر روی مدیران جستجو میشود تنها کد پرسنل برمیگردد
        /// در جستجوی مدیر پرسنل برمیگردد و واسط کاربر این آیتم را جای شناسه مدیر مقداردهی میکند
        /// </summary>
        public virtual decimal ManagerPersonId { get; set; }       

		/// <summary>
		/// Gets or sets the FromDate value.
		/// </summary>
		public virtual DateTime FromDate { get; set; }

		/// <summary>
		/// Gets or sets the ToDate value.
		/// </summary>
		public virtual DateTime ToDate { get; set; }

        /// <summary>
        /// جهت راحتی واسط کاربر
        /// </summary>
        public virtual string TheFromDate { get; set; }

        /// <summary>
        /// جهت راحتی واسط کاربر
        /// </summary>
        public virtual string TheToDate { get; set; }

		/// <summary>
		/// Gets or sets the Active value.
		/// </summary>
		public virtual Boolean Active { get; set; }

        /// <summary>
        /// سطوح دسترسی برای ویرایش ست شده است 
        /// </summary>
        public virtual bool PrecardAccessIsSet { get; set; }

        public virtual IList<Concepts.Precard> PrecardList { get; set; }
		#endregion		
	
        #region ICloneable Members

        public virtual object Clone()
        {
            Substitute sub = new Substitute();
            sub.Active = this.Active;
            sub.FromDate = this.FromDate;
            sub.ToDate = this.ToDate;
            if (this.Manager != null)
                sub.Manager = new Manager() { ID = this.Manager.ID };
            if (this.Person != null)
                sub.Person = new Person() { ID = this.Person.ID };
            if (this.PrecardList != null)
                sub.PrecardList = this.PrecardList.ToList();
            return sub;
        }

        #endregion
    }
}