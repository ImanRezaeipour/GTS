using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Clientele;

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
    /// 		<description>5/23/2011</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class ManagerFlow : IEntity
    {
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
        /// Gets or sets the Level value.
        /// </summary>
        public virtual Int32 Level { get; set; }

        /// <summary>
        /// بجای حذف ، غیر فعال میشود
        /// </summary>
        public virtual Boolean Active { get; set; }

        /// <summary>
        /// Gets or sets the Flow value.
        /// </summary>
        public virtual Flow Flow { get; set; }

        public virtual IList<CL_OffishRequestStatus> StatusList { get; set; }
        #endregion
    }
}