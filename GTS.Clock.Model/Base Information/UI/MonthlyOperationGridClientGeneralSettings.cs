using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.AppSetting;

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
	/// 		<description>5/23/2011</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

    public class MonthlyOperationGridClientGeneralSettings : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the LangID value.
		/// </summary>
		public virtual Languages Language { get; set; }

		/// <summary>
		/// Gets or sets the Day value.
		/// </summary>
		public virtual Int32 DayName { get; set; }

		/// <summary>
		/// Gets or sets the Date value.
		/// </summary>
		public virtual Int32 TheDate { get; set; }

		/// <summary>
		/// Gets or sets the FirstEntrance value.
		/// </summary>
		public virtual Int32 FirstEntrance { get; set; }

		/// <summary>
		/// Gets or sets the FirstExit value.
		/// </summary>
		public virtual Int32 FirstExit { get; set; }

		/// <summary>
		/// Gets or sets the SecondEntrance value.
		/// </summary>
		public virtual Int32 SecondEntrance { get; set; }

		/// <summary>
		/// Gets or sets the SecondExit value.
		/// </summary>
		public virtual Int32 SecondExit { get; set; }

		/// <summary>
		/// Gets or sets the ThirdEntrance value.
		/// </summary>
		public virtual Int32 ThirdEntrance { get; set; }

		/// <summary>
		/// Gets or sets the ThirdExit value.
		/// </summary>
		public virtual Int32 ThirdExit { get; set; }

		/// <summary>
		/// Gets or sets the FourthEntrance value.
		/// </summary>
		public virtual Int32 FourthEntrance { get; set; }

		/// <summary>
		/// Gets or sets the FourthExit value.
		/// </summary>
		public virtual Int32 FourthExit { get; set; }

		/// <summary>
		/// Gets or sets the FifthEntrance value.
		/// </summary>
		public virtual Int32 FifthEntrance { get; set; }

		/// <summary>
		/// Gets or sets the FifthExit value.
		/// </summary>
		public virtual Int32 FifthExit { get; set; }

		/// <summary>
		/// Gets or sets the LastExit value.
		/// </summary>
		public virtual Int32 LastExit { get; set; }

		/// <summary>
		/// Gets or sets the NecessaryOperation value.
		/// </summary>
		public virtual Int32 NecessaryOperation { get; set; }

		/// <summary>
		/// Gets or sets the PresenceDuration value.
		/// </summary>
		public virtual Int32 PresenceDuration { get; set; }

		/// <summary>
		/// Gets or sets the HourlyPureOperation value.
		/// </summary>
		public virtual Int32 HourlyPureOperation { get; set; }

		/// <summary>
		/// Gets or sets the DailyPureOperation value.
		/// </summary>
		public virtual Int32 DailyPureOperation { get; set; }

		/// <summary>
		/// Gets or sets the ImpureOperation value.
		/// </summary>
		public virtual Int32 ImpureOperation { get; set; }

		/// <summary>
		/// Gets or sets the AllowableOverTime value.
		/// </summary>
		public virtual Int32 AllowableOverTime { get; set; }

		/// <summary>
		/// Gets or sets the UnallowableOverTime value.
		/// </summary>
		public virtual Int32 UnallowableOverTime { get; set; }

		/// <summary>
		/// Gets or sets the HourlyAllowableAbsence value.
		/// </summary>
		public virtual Int32 HourlyAllowableAbsence { get; set; }

		/// <summary>
		/// Gets or sets the HourlyUnallowableAbsence value.
		/// </summary>
		public virtual Int32 HourlyUnallowableAbsence { get; set; }

		/// <summary>
		/// Gets or sets the DailyAbsence value.
		/// </summary>
		public virtual Int32 DailyAbsence { get; set; }

		/// <summary>
		/// Gets or sets the HourlyMission value.
		/// </summary>
		public virtual Int32 HourlyMission { get; set; }

		/// <summary>
		/// Gets or sets the DailyMission value.
		/// </summary>
		public virtual Int32 DailyMission { get; set; }

		/// <summary>
		/// Gets or sets the HostelryMission value.
		/// </summary>
		public virtual Int32 HostelryMission { get; set; }

		/// <summary>
		/// Gets or sets the HourlySickLeave value.
		/// </summary>
		public virtual Int32 HourlySickLeave { get; set; }

		/// <summary>
		/// Gets or sets the DailySickLeave value.
		/// </summary>
		public virtual Int32 DailySickLeave { get; set; }

		/// <summary>
		/// Gets or sets the HourlyMeritoriouslyLeave value.
		/// </summary>
		public virtual Int32 HourlyMeritoriouslyLeave { get; set; }

		/// <summary>
		/// Gets or sets the DailyMeritoriouslyLeave value.
		/// </summary>
		public virtual Int32 DailyMeritoriouslyLeave { get; set; }

		/// <summary>
		/// Gets or sets the HourlyWithoutPayLeave value.
		/// </summary>
		public virtual Int32 HourlyWithoutPayLeave { get; set; }

		/// <summary>
		/// Gets or sets the DailyWithoutPayLeave value.
		/// </summary>
		public virtual Int32 DailyWithoutPayLeave { get; set; }

		/// <summary>
		/// Gets or sets the HourlyWithPayLeave value.
		/// </summary>
		public virtual Int32 HourlyWithPayLeave { get; set; }

		/// <summary>
		/// Gets or sets the DailyWithPayLeave value.
		/// </summary>
		public virtual Int32 DailyWithPayLeave { get; set; }

		/// <summary>
		/// Gets or sets the Shift value.
		/// </summary>
		public virtual Int32 Shift { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField1 value.
		/// </summary>
		public virtual Int32 ReserveField1 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField2 value.
		/// </summary>
		public virtual Int32 ReserveField2 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField3 value.
		/// </summary>
		public virtual Int32 ReserveField3 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField4 value.
		/// </summary>
		public virtual Int32 ReserveField4 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField5 value.
		/// </summary>
		public virtual Int32 ReserveField5 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField6 value.
		/// </summary>
		public virtual Int32 ReserveField6 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField7 value.
		/// </summary>
		public virtual Int32 ReserveField7 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField8 value.
		/// </summary>
		public virtual Int32 ReserveField8 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField9 value.
		/// </summary>
		public virtual Int32 ReserveField9 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField10 value.
		/// </summary>
		public virtual Int32 ReserveField10 { get; set; }
		#endregion		
	}
}