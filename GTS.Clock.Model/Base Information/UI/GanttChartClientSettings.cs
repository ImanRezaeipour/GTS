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
    /// 		<description>2012/05/27</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class GanttChartClientSettings : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the PresenceDuration value.
        /// </summary>
        public virtual Boolean PresenceDuration { get; set; }

        /// <summary>
        /// Gets or sets the AllowedOverTime value.
        /// </summary>
        public virtual Boolean AllowedOverTime { get; set; }

        /// <summary>
        /// Gets or sets the UnallowableOverTime value.
        /// </summary>
        public virtual Boolean UnallowableOverTime { get; set; }

        /// <summary>
        /// Gets or sets the HourlyAllowableAbsence value.
        /// </summary>
        public virtual Boolean HourlyAllowableAbsence { get; set; }

        /// <summary>
        /// Gets or sets the HourlyUnallowableAbsence value.
        /// </summary>
        public virtual Boolean HourlyUnallowableAbsence { get; set; }

        /// <summary>
        /// Gets or sets the DailyAbsence value.
        /// </summary>
        public virtual Boolean DailyAbsence { get; set; }

        /// <summary>
        /// Gets or sets the HourlyMission value.
        /// </summary>
        public virtual Boolean HourlyMission { get; set; }

        /// <summary>
        /// Gets or sets the DailyMission value.
        /// </summary>
        public virtual Boolean DailyMission { get; set; }

        /// <summary>
        /// Gets or sets the DailyNightMission value.
        /// </summary>
        public virtual Boolean DailyNightMission { get; set; }

        /// <summary>
        /// Gets or sets the HourlySickLeave value.
        /// </summary>
        public virtual Boolean HourlySickLeave { get; set; }

        /// <summary>
        /// Gets or sets the DailySickLeave value.
        /// </summary>
        public virtual Boolean DailySickLeave { get; set; }

        /// <summary>
        /// Gets or sets the HourlyMeritoriouslyLeave value.
        /// </summary>
        public virtual Boolean HourlyMeritoriouslyLeave { get; set; }

        /// <summary>
        /// Gets or sets the DailyMeritoriouslyLeave value.
        /// </summary>
        public virtual Boolean DailyMeritoriouslyLeave { get; set; }

        /// <summary>
        /// Gets or sets the HourlyWithoutPayLeave value.
        /// </summary>
        public virtual Boolean HourlyWithoutPayLeave { get; set; }

        /// <summary>
        /// Gets or sets the DailyWithoutPayLeave value.
        /// </summary>
        public virtual Boolean DailyWithoutPayLeave { get; set; }

        /// <summary>
        /// Gets or sets the HourlyWithPayLeave value.
        /// </summary>
        public virtual Boolean HourlyWithPayLeave { get; set; }

        /// <summary>
        /// Gets or sets the DailyWithPayLeave value.
        /// </summary>
        public virtual Boolean DailyWithPayLeave { get; set; }

        public virtual AppSetting.UserSettings UserSetting { get; set; }

        #endregion
    }
}