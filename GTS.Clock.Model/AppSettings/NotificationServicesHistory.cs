using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.AppSetting
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
    /// 		<description>2012/07/28</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class NotificationServicesHistory
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the NotificationServiceID value.
        /// </summary>
        public virtual Int32 NotificationServiceID { get; set; }

        /// <summary>
        /// item id + service Id
        /// </summary>
        public virtual String ItemID { get; set; }

        public virtual DateTime Date { get; set; }
        #endregion
    }
}