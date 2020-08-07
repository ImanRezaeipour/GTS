using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class PersonInfoProxy
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the Order value.
        /// </summary>
        public virtual Int32 Order { get; set; }

        /// <summary>
        /// Gets or sets the Active value.
        /// </summary>
        public virtual Boolean Active { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual String Title { get; set; }


        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual String Value { get; set; }
        #endregion

    }
}
