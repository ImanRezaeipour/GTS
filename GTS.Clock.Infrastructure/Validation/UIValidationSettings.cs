using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GTS.Clock.Infrastructure.Validation.Configuration
{
    public class UIValidationSettings : ConfigurationSection
    {
        [ConfigurationProperty("UIValidationMappings",
            IsDefaultCollection = true)]
        public UIValidationMappingCollection UIValidationMappings
        {
            get { return (UIValidationMappingCollection)base["UIValidationMappings"]; }
        }
    }
}
