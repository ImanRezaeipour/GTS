using System;
using GTS.Clock.Infrastructure.CalculatorSchedulerFramework;
using GTS.Clock.Infrastructure.CalculatorSchedulerFramework.Configuration;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model
{
    public class Timely : ICalculatorScheduler
    {
        #region ICalculatorScheduler Members

        public bool IsConditionOccurenced(CalculatorSchedulerSettings settings,DateTime Now)
        {
            int nowTime = Utility.RealTimeToIntTime(String.Format("{0}:{1}", Now.Hour, Now.Minute));
            int fromTime = Utility.RealTimeToIntTime(settings.FromTime);
            int toTime = Utility.RealTimeToIntTime(settings.ToTime);

            if (fromTime < toTime)
            {
                if (fromTime < nowTime && toTime > nowTime)
                    return true;               
            }
            else 
            {
                if (fromTime < nowTime || toTime > nowTime)
                    return true;
            } 
            return false;
        }

        #endregion
    }
}
