using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    public class DailyReportProxy
    {
        public String ShiftPairs { get; set; }

        /// <summary>
        /// کارکرد خالص
        /// </summary>
        public String HourlyPureOperation { get; set; }

        /// <summary>
        /// اضافه کار مجاز
        /// </summary>
        public String AllowableOverTime { get; set; }

        /// <summary>
        /// اضافه کار ساعتی غیر مجاز
        /// </summary>
        public String HourlyUnallowableAbsence { get; set; }

        /// <summary>
        /// غیبت غیر مجاز روزانه
        /// </summary>
        public String DailyAbsence { get; set; }

        /// <summary>
        /// مرخصی ساعتی استعلاجی
        /// </summary>
        public String HourlySickLeave { get; set; }

        /// <summary>
        /// مرخصی ساعتی بدون حقوق
        /// </summary>
        public String HourlyWithoutPayLeave { get; set; }

        /// <summary>
        /// مرخصی ساعتی استحقاقی
        /// </summary>
        public String HourlyMeritoriouslyLeave { get; set; }

        /// <summary>
        /// ماموریت ساعتی
        /// </summary>
        public String HourlyMission { get; set; }

        /// <summary>
        /// ماموریت روزانه
        /// </summary>
        public String DailyMission { get; set; }
    }
}
