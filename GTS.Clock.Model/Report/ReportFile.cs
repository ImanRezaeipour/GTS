using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.Report
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
    /// 		<description>2011-11-19</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class ReportFile : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public virtual String Name { get; set; }

        /// <summary>
        /// Gets or sets the File value.
        /// </summary>
        public virtual String File { get; set; }

        public virtual String Description { get; set; }

        public virtual SubSystemIdentifier SubSystemId { get; set; }

        #endregion
    }
}