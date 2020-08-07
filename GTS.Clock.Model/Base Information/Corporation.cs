using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Security;

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
    /// 		<term>Kamran Pahlevani</term>
    /// 		<description>10/07/2012</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class Corporation : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual String Code { get; set; }

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public virtual String Name { get; set; }

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public virtual String Address { get; set; }

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public virtual String Phone { get; set; }

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public virtual String Fax { get; set; }

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public virtual String EconomicCode { get; set; }

        /// <summary>
        /// Gets or sets the Description value.
        /// </summary>
        public virtual String Description { get; set; }


        #endregion

    }
}
