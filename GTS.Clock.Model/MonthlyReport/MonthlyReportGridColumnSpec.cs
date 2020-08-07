using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.MonthlyReport
{
    public enum MonthlyReportGridColumnSpec
    {
        NONE = 0, YELLOW = 1, GREEN = 2
    }

    public enum GridSearchFields
    {
        NONE = 1, PersonCode = 2, PersonName = 3, UnderList = 4, Complex = 5
    }

    public enum GridOrderFieldType
    {
        desc, asc
    }

    public enum GridOrderFields
    {
        NONE, /*Date, Day, FirstEntrance, FirstExit, SecondEntrance, SecondExit, ThirdEntrance, ThirdExit,
        FourthEntrance, FourthExit, FifthEntrance, FifthExit, LastExit, NecessaryOperation, HourlyPureOperation,
        DailyPureOperation, ImpureOperation, AllowableOverTime, UnallowableOverTime, HourlyAllowableAbsence,
        HourlyUnallowableAbsence, DailyAbsence, HourlyMission, DailyMission, HostelryMission, HourlySickLeave,
        DailySickLeave, HourlyMeritoriouslyLeave, DailyMeritoriouslyLeave, HourlyWithoutPayLeave, PresenceDuration,
        DailyWithoutPayLeave, HourlyWithPayLeave, DailyWithPayLeave,*/
        gridFields_DepartmentName, gridFields_Family, gridFields_BarCode
    }
}