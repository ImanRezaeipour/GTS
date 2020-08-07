using System;
using System.Configuration;

namespace GTS.Clock.Infrastructure.Report.BusinessFramework
{
    public class BusinessSettings : ConfigurationSection
    {
        [ConfigurationProperty(BusinessMappingConstants.ConfigurationPropertyName, 
            IsDefaultCollection = true)]
        public BusinessMappingCollection BusinessMappings
        {
            get { return (BusinessMappingCollection)base[BusinessMappingConstants.ConfigurationPropertyName]; }
        }
    }
}
