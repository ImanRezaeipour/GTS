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
    /// 		<description>2012/07/10</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class SMSSettings
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the Active value.
        /// </summary>
        public virtual Boolean Active { get; set; }

        /// <summary>
        /// Gets or sets the SendByDay value.
        /// </summary>
        public virtual Boolean SendByDay { get; set; }

        /// <summary>
        /// Gets or sets the DayNum value.
        /// </summary>
        public virtual Int32 DayCount { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual String TheDayHour { get; set; }

        /// <summary>
        /// Gets or sets the DayHour value.
        /// </summary>
        public virtual Int32 DayHour { get; set; }

        /// <summary>
        /// Gets or sets the Hour value.
        /// </summary>
        public virtual Int32 Hour { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual String TheHour { get; set; }

        public virtual UserSettings UserSetting { get; set; }

        #endregion
    }
}