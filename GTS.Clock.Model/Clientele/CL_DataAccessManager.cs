using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Clientele
{
    public class CL_DataAccessManager :IEntity
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
        /// Gets or sets the ManagerID value.
        /// </summary>
        public virtual Decimal? ManagerID { get; set; }

        /// <summary>
        /// Gets or sets the All value.
        /// </summary>
        public virtual Boolean All { get; set; }
        #endregion
    }
}
