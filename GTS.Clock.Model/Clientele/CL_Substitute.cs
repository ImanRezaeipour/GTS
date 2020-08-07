using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Clientele
{
    public class CL_Substitute : IEntity
    {

        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the Manager value.
        /// </summary>
        public virtual CL_Manager Manager { get; set; }

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
        public virtual bool OffishTypeAccessIsSet { get; set; }

        public virtual IList<CL_OffishType> OffishTypeList { get; set; }
        #endregion

    }
}
