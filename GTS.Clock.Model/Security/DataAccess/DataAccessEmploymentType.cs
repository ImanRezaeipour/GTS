using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Security
{
    public class DAEmploymentType
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the UserID value.
        /// </summary>
        public virtual Decimal UserID { get; set; }

        /// <summary>
        /// Gets or sets the EmploymentTypeID value.
        /// </summary>
        public virtual Decimal? EmploymentTypeID { get; set; }

        /// <summary>
        /// Gets or sets the All value.
        /// </summary>
        public virtual Boolean All { get; set; }
        #endregion		

    }
}
