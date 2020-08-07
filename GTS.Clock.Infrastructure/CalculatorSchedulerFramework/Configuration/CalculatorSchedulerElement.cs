using System;
using System.Configuration;

namespace GTS.Clock.Infrastructure.CalculatorSchedulerFramework.Configuration
{
    public sealed class CalculatorSchedulerElement : ConfigurationElement
    {
        [ConfigurationProperty(CalculatorSchedulerConstants.SchedulerNameAttributeName, 
            IsKey = true, IsRequired = true)]
        public string SchedulerName
        {
            get 
            {
                return (string)this[CalculatorSchedulerConstants.SchedulerNameAttributeName]; 
            }
            set 
            {
                this[CalculatorSchedulerConstants.SchedulerNameAttributeName] = value; 
            }
        }

        [ConfigurationProperty(CalculatorSchedulerConstants.SchedulerFullTypeNameAttributeName)]
        public string SchedulerFullTypeName
        {
            get 
            { 
                return (string)this[CalculatorSchedulerConstants.SchedulerFullTypeNameAttributeName]; 
            }
            set 
            { 
                this[CalculatorSchedulerConstants.SchedulerFullTypeNameAttributeName] = value; 
            }
        }
    }
}
