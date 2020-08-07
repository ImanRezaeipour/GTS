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
    /// 		<description>2011-10-02</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class MonthlyOperationGridMasterSettings : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the UserSettingId value.
        /// </summary>
        public virtual AppSetting.UserSettings UserSetting { get; set; }

        /// <summary>
        /// Gets or sets the FirstEntrance value.
        /// </summary>
        public virtual Boolean FirstEntrance { get; set; }

        /// <summary>
        /// Gets or sets the FirstExit value.
        /// </summary>
        public virtual Boolean FirstExit { get; set; }

        /// <summary>
        /// Gets or sets the SecondEntrance value.
        /// </summary>
        public virtual Boolean SecondEntrance { get; set; }

        /// <summary>
        /// Gets or sets the SecondExit value.
        /// </summary>
        public virtual Boolean SecondExit { get; set; }

        /// <summary>
        /// Gets or sets the ThirdEntrance value.
        /// </summary>
        public virtual Boolean ThirdEntrance { get; set; }

        /// <summary>
        /// Gets or sets the ThirdExit value.
        /// </summary>
        public virtual Boolean ThirdExit { get; set; }

        /// <summary>
        /// Gets or sets the FourthEntrance value.
        /// </summary>
        public virtual Boolean FourthEntrance { get; set; }

        /// <summary>
        /// Gets or sets the FourthExit value.
        /// </summary>
        public virtual Boolean FourthExit { get; set; }

        /// <summary>
        /// Gets or sets the FifthEntrance value.
        /// </summary>
        public virtual Boolean FifthEntrance { get; set; }

        /// <summary>
        /// Gets or sets the FifthExit value.
        /// </summary>
        public virtual Boolean FifthExit { get; set; }

        /// <summary>
        /// Gets or sets the LastExit value.
        /// </summary>
        public virtual Boolean LastExit { get; set; }

        /// <summary>
        /// Gets or sets the NecessaryOperation value.
        /// </summary>
        public virtual Boolean NecessaryOperation { get; set; }

        /// <summary>
        /// Gets or sets the PresenceDuration value.
        /// </summary>
        public virtual Boolean PresenceDuration { get; set; }

        /// <summary>
        /// Gets or sets the HourlyPureOperation value.
        /// </summary>
        public virtual Boolean HourlyPureOperation { get; set; }

        /// <summary>
        /// Gets or sets the DailyPureOperation value.
        /// </summary>
        public virtual Boolean DailyPureOperation { get; set; }

        /// <summary>
        /// Gets or sets the ImpureOperation value.
        /// </summary>
        public virtual Boolean ImpureOperation { get; set; }

        /// <summary>
        /// Gets or sets the AllowableOverTime value.
        /// </summary>
        public virtual Boolean AllowableOverTime { get; set; }

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
        /// Gets or sets the HostelryMission value.
        /// </summary>
        public virtual Boolean HostelryMission { get; set; }

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

        /// <summary>
        /// Gets or sets the ReserveField1 value.
        /// </summary>
        public virtual Boolean ReserveField1 { get; set; }

        /// <summary>
        /// Gets or sets the ReserveField2 value.
        /// </summary>
        public virtual Boolean ReserveField2 { get; set; }

        /// <summary>
        /// Gets or sets the ReserveField3 value.
        /// </summary>
        public virtual Boolean ReserveField3 { get; set; }

        /// <summary>
        /// Gets or sets the ReserveField4 value.
        /// </summary>
        public virtual Boolean ReserveField4 { get; set; }

        /// <summary>
        /// Gets or sets the ReserveField5 value.
        /// </summary>
        public virtual Boolean ReserveField5 { get; set; }

        /// <summary>
        /// Gets or sets the ReserveField6 value.
        /// </summary>
        public virtual Boolean ReserveField6 { get; set; }

        /// <summary>
        /// Gets or sets the ReserveField7 value.
        /// </summary>
        public virtual Boolean ReserveField7 { get; set; }

        /// <summary>
        /// Gets or sets the ReserveField8 value.
        /// </summary>
        public virtual Boolean ReserveField8 { get; set; }

        /// <summary>
        /// Gets or sets the ReserveField9 value.
        /// </summary>
        public virtual Boolean ReserveField9 { get; set; }

        /// <summary>
        /// Gets or sets the ReserveField10 value.
        /// </summary>
        public virtual Boolean ReserveField10 { get; set; }

        /// <summary>
        /// Gets or sets the RemainLeaveToYearEnd value.
        /// </summary>
        public virtual Boolean RemainLeaveToYearEnd { get; set; }

        /// <summary>
        /// Gets or sets the RemainLeaveToMonthEnd value.
        /// </summary>
        public virtual Boolean RemainLeaveToMonthEnd { get; set; }

        /// <summary>
        /// Gets or sets the BarCode value.
        /// </summary>
        public virtual Boolean BarCode { get; set; }

        /// <summary>
        /// Gets or sets the PersonName value.
        /// </summary>
        public virtual Boolean PersonName { get; set; }

        /// <summary>
        /// Gets or sets the DepartmentName value.
        /// </summary>
        public virtual Boolean DepartmentName { get; set; }
        #endregion
    }
}