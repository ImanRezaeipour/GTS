using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.CalculatorSchedulerFramework.Configuration;

namespace GTS.Clock.Infrastructure.CalculatorSchedulerFramework
{
    public interface ICalculatorScheduler
    {
        bool IsConditionOccurenced(CalculatorSchedulerSettings settings, DateTime Now);
    }
}
