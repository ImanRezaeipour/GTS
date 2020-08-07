using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.BaseInformation;

namespace GTS.Clock.Model.Clientele
{
    public class CL_DataAccessCorporation
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
        /// Gets or sets the DoctorID value.
        /// </summary>
        public virtual Decimal? CorporationID { get; set; }

        public virtual Corporation Corporation { get; set; }
        
        #endregion		

    }
}
