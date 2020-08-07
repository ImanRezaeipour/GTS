using System;

namespace GTS.Clock.Infrastructure.CalculatorSchedulerFramework.Configuration
{
    public static class CalculatorSchedulerConstants
    {
        public const string ConfigurationPropertyName = "calculatorSchedulers";
        public const string ConfigurationElementName = "calculatorScheduler";
        public const string SchedulerNameAttributeName = "schedulerName";
        public const string SchedulerFullTypeNameAttributeName = "schedulerFullTypeName";
        public const string ServiceableSchedulersAttributeName = "serviceableSchedulers";
        public const string SchedulerIntervalAttributeName = "Interval";
        public const string SchedulerFromTimeAttributeName = "FromTime";
        public const string SchedulerToTimeAttributeName = "ToTime";
        public const string GTSWebServiceAddressAttributeName = "GTSWebServiceAddress";            
        public const string SchedulerBatchFlushAttributeName = "BatchFlush";
        public const string CalculatorSchedulerConfigurationSectionName = "calculatorSchedulerConfiguration";
    }
}
