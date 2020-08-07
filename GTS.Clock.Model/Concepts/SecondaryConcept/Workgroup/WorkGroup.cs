using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
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
    /// 		<description>6/6/2011</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class WorkGroup : IEntity
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
        /// Gets or sets the CustomCode value.
        /// </summary>
        public virtual String CustomCode { get; set; }

        /// <summary>
        /// Gets or sets the _grpsCode value.
        /// </summary>
        public virtual Int32 _grpsCode { get; set; }

        public virtual IList<WorkGroupDetail> DetailList { get; set; }

        public virtual IList<AssignWorkGroup> AssignWorkGroupList { get; set; }
        #endregion
    }
}