using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts.UI
{
    public enum GridColumnColor
    {
        NONE = 0, YELLOW = 1, GREEN = 2
    }

    public enum GridSearchFields
    {
        NONE = 1, PersonCode = 2, PersonName = 3
    }
    public enum GridOrderFieldType
    {
        Descent = 1, Ascendant = 2
    }

    public enum GridOrderFields
    {
        NONE, Date, Day, FirstEntrance, FirstExit, SecondEntrance, SecondExit, ThirdEntrance, ThirdExit,
        FourthEntrance, FourthExit, FifthEntrance, FifthExit, LastExit, NecessaryOperation, HourlyPureOperation,
        DailyPureOperation, ImpureOperation, AllowableOverTime, UnallowableOverTime, HourlyAllowableAbsence,
        HourlyUnallowableAbsence, DailyAbsence, HourlyMission, DailyMission, HostelryMission, HourlySickLeave,
        DailySickLeave, HourlyMeritoriouslyLeave, DailyMeritoriouslyLeave, HourlyWithoutPayLeave, PresenceDuration,
        DailyWithoutPayLeave, HourlyWithPayLeave, DailyWithPayLeave, PersonName, PersonCode

    }
}