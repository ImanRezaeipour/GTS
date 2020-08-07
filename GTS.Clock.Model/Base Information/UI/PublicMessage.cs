using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.UI
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
    /// 		<description>2011-11-24</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class PublicMessage : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the PersonId value.
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// Gets or sets the Date value.
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the Active value.
        /// </summary>
        public virtual bool Active { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual String Subject { get; set; }

        /// <summary>
        /// Gets or sets the Message value.
        /// </summary>
        public virtual String Message { get; set; }

        #endregion
    }
}