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
	/// 		<description>5/23/2011</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

    public class MasterMonthlyOperationGridFields : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the Date value.
		/// </summary>
		public virtual String Date { get; set; }

		/// <summary>
		/// Gets or sets the Day value.
		/// </summary>
		public virtual String Day { get; set; }

		/// <summary>
		/// Gets or sets the FirstEntrance value.
		/// </summary>
		public virtual String FirstEntrance { get; set; }

		/// <summary>
		/// Gets or sets the FirstExit value.
		/// </summary>
		public virtual String FirstExit { get; set; }

		/// <summary>
		/// Gets or sets the SecondEntrance value.
		/// </summary>
		public virtual String SecondEntrance { get; set; }

		/// <summary>
		/// Gets or sets the SecondExit value.
		/// </summary>
		public virtual String SecondExit { get; set; }

		/// <summary>
		/// Gets or sets the ThirdEntrance value.
		/// </summary>
		public virtual String ThirdEntrance { get; set; }

		/// <summary>
		/// Gets or sets the ThirdExit value.
		/// </summary>
		public virtual String ThirdExit { get; set; }

		/// <summary>
		/// Gets or sets the LastExit value.
		/// </summary>
		public virtual String LastExit { get; set; }

		/// <summary>
		/// Gets or sets the NecessaryOperation value.
		/// </summary>
		public virtual String NecessaryOperation { get; set; }

		/// <summary>
		/// Gets or sets the HourlyPureOperation value.
		/// </summary>
		public virtual String HourlyPureOperation { get; set; }

		/// <summary>
		/// Gets or sets the DailyPureOperation value.
		/// </summary>
		public virtual String DailyPureOperation { get; set; }

		/// <summary>
		/// Gets or sets the ImpureOperation value.
		/// </summary>
		public virtual String ImpureOperation { get; set; }

		/// <summary>
		/// Gets or sets the AllowableOverTime value.
		/// </summary>
		public virtual String AllowableOverTime { get; set; }

		/// <summary>
		/// Gets or sets the UnallowableOverTime value.
		/// </summary>
		public virtual String UnallowableOverTime { get; set; }

		/// <summary>
		/// Gets or sets the HourlyAllowableAbsence value.
		/// </summary>
		public virtual String HourlyAllowableAbsence { get; set; }

		/// <summary>
		/// Gets or sets the HourlyUnallowableAbsence value.
		/// </summary>
		public virtual String HourlyUnallowableAbsence { get; set; }

		/// <summary>
		/// Gets or sets the DailyAbsence value.
		/// </summary>
		public virtual String DailyAbsence { get; set; }

		/// <summary>
		/// Gets or sets the HourlyMission value.
		/// </summary>
		public virtual String HourlyMission { get; set; }

		/// <summary>
		/// Gets or sets the DailyMission value.
		/// </summary>
		public virtual String DailyMission { get; set; }

		/// <summary>
		/// Gets or sets the HostelryMission value.
		/// </summary>
		public virtual String HostelryMission { get; set; }

		/// <summary>
		/// Gets or sets the HourlySickLeave value.
		/// </summary>
		public virtual String HourlySickLeave { get; set; }

		/// <summary>
		/// Gets or sets the DailySickLeave value.
		/// </summary>
		public virtual String DailySickLeave { get; set; }

		/// <summary>
		/// Gets or sets the HourlyMeritoriouslyLeave value.
		/// </summary>
		public virtual String HourlyMeritoriouslyLeave { get; set; }

		/// <summary>
		/// Gets or sets the DailyMeritoriouslyLeave value.
		/// </summary>
		public virtual String DailyMeritoriouslyLeave { get; set; }

		/// <summary>
		/// Gets or sets the HourlyWithoutPayLeave value.
		/// </summary>
		public virtual String HourlyWithoutPayLeave { get; set; }

		/// <summary>
		/// Gets or sets the FirstEntrance_BCID value.
		/// </summary>
		public virtual Int32 FirstEntrance_BCID { get; set; }

		/// <summary>
		/// Gets or sets the FirstExit_BCID value.
		/// </summary>
		public virtual Int32 FirstExit_BCID { get; set; }

		/// <summary>
		/// Gets or sets the SecondEntrance_BCID value.
		/// </summary>
		public virtual Int32 SecondEntrance_BCID { get; set; }

		/// <summary>
		/// Gets or sets the SecondExit_BCID value.
		/// </summary>
		public virtual Int32 SecondExit_BCID { get; set; }

		/// <summary>
		/// Gets or sets the ThirdEntrance_BCID value.
		/// </summary>
		public virtual Int32 ThirdEntrance_BCID { get; set; }

		/// <summary>
		/// Gets or sets the ThirdExit_BCID value.
		/// </summary>
		public virtual Int32 ThirdExit_BCID { get; set; }

		/// <summary>
		/// Gets or sets the FourthEntrance_BCID value.
		/// </summary>
		public virtual Int32 FourthEntrance_BCID { get; set; }

		/// <summary>
		/// Gets or sets the FourthExit_BCID value.
		/// </summary>
		public virtual Int32 FourthExit_BCID { get; set; }

		/// <summary>
		/// Gets or sets the FifthEntrance_BCID value.
		/// </summary>
		public virtual Int32 FifthEntrance_BCID { get; set; }

		/// <summary>
		/// Gets or sets the FifthExit_BCID value.
		/// </summary>
		public virtual Int32 FifthExit_BCID { get; set; }

		/// <summary>
		/// Gets or sets the LastExit_BCID value.
		/// </summary>
		public virtual Int32 LastExit_BCID { get; set; }

		/// <summary>
		/// Gets or sets the HourlyUnallowableAbsence_BCID value.
		/// </summary>
		public virtual Int32 HourlyUnallowableAbsence_BCID { get; set; }

		/// <summary>
		/// Gets or sets the DailyAbsence_BCID value.
		/// </summary>
		public virtual Int32 DailyAbsence_BCID { get; set; }

		/// <summary>
		/// Gets or sets the FourthEntrance value.
		/// </summary>
		public virtual String FourthEntrance { get; set; }

		/// <summary>
		/// Gets or sets the FourthExit value.
		/// </summary>
		public virtual String FourthExit { get; set; }

		/// <summary>
		/// Gets or sets the FifthEntrance value.
		/// </summary>
		public virtual String FifthEntrance { get; set; }

		/// <summary>
		/// Gets or sets the FifthExit value.
		/// </summary>
		public virtual String FifthExit { get; set; }

		/// <summary>
		/// Gets or sets the PresenceDuration value.
		/// </summary>
		public virtual String PresenceDuration { get; set; }

		/// <summary>
		/// Gets or sets the DailyWithoutPayLeave value.
		/// </summary>
		public virtual String DailyWithoutPayLeave { get; set; }

		/// <summary>
		/// Gets or sets the HourlyWithPayLeave value.
		/// </summary>
		public virtual String HourlyWithPayLeave { get; set; }

		/// <summary>
		/// Gets or sets the DailyWithPayLeave value.
		/// </summary>
		public virtual String DailyWithPayLeave { get; set; }

		/// <summary>
		/// Gets or sets the Shift value.
		/// </summary>
		public virtual String Shift { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField1 value.
		/// </summary>
		public virtual String ReserveField1 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField2 value.
		/// </summary>
		public virtual String ReserveField2 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField3 value.
		/// </summary>
		public virtual String ReserveField3 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField4 value.
		/// </summary>
		public virtual String ReserveField4 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField5 value.
		/// </summary>
		public virtual String ReserveField5 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField6 value.
		/// </summary>
		public virtual String ReserveField6 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField7 value.
		/// </summary>
		public virtual String ReserveField7 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField8 value.
		/// </summary>
		public virtual String ReserveField8 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField9 value.
		/// </summary>
		public virtual String ReserveField9 { get; set; }

		/// <summary>
		/// Gets or sets the ReserveField10 value.
		/// </summary>
		public virtual String ReserveField10 { get; set; }

        public virtual IList<DetailedMonthlyOperationGridFields> DetailList { get; set; }
		#endregion		
	}
}