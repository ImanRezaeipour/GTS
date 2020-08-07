using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public enum Precards
    {
        UnKnown = -1,
        Usual = 0,
        //TakhirMovajah = 1,
        Enter = 4, Exit = 5, /*WithoutCart = 7,*/ ForgotCard = 10,
        NoSallaryIllnessHourLeave = 11,
        NoSallaryHourLeave1 = 12, NoSallaryHourLeave2 = 13,
        NoSallaryHourLeave3 = 14, NoSallaryHourLeave4 = 15,
        HourLeave1 = 21, HourLeave2 = 22, HourLeave3 = 23, HourLeave4 = 24, HourLeave5 = 25, HourLeave6 = 26, HourLeave7 = 27,
        ProtractOverTime = 28,
        ProtractedTraffic = 29,
        HourDuty1 = 51, HourDuty2 = 52, HourDuty3 = 53, HourDuty4 = 54, HourDuty5 = 55,
        DailyNoSallaryIllnessLeave1 = 31,
        DailyNoSallaryLeave1 = 32, DailyNoSallaryLeave2 = 33, DailyNoSallaryLeave3 = 34, DailyNoSallaryLeave4 = 35, DailyNoSallaryLeave5 = 36,
        DailyEstehghaghiLeave = 41, DailyEstelagiLeave = 42,
        DailyLeave1 = 43, DailyLeave2 = 44, DailyLeave3 = 45, DailyLeave4 = 46, DailyLeave5 = 47, DailyLeave6 = 48,
        OutOfficeWork = 50,
        DailyDuty1 = 61, DailyDuty2 = 62, DailyDuty3 = 63, DailyDuty4 = 64, DailyDuty5 = 65,
        DailyDuty24Hour1 = 71, DailyDuty24Hour2 = 72, DailyDuty24Hour3 = 73, DailyDuty24Hour4 = 74, DailyDuty24Hour5 = 75,
        ServiceDelay1 = 81, ServiceDelay2 = 82, ServiceDelay3 = 83, ServiceDelay4 = 84, ServiceDelay5 = 85,
        //DailyAbsence = 91,
        OverTime = 125, OrderedOverTime = 126

    };
}
