using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GTS.Clock.Infrastructure.Validation.Configuration
{
    public sealed class UIValidationMappingElement : ConfigurationElement
    {
        [ConfigurationProperty("interfaceShortTypeName",
            IsKey = true, IsRequired = true)]
        public string InterfaceShortTypeName
        {
            get
            {
                return (string)this["interfaceShortTypeName"];
            }
            set
            {
                this["interfaceShortTypeName"] = value;
            }
        }

        [ConfigurationProperty("validatorFullTypeName",
            IsRequired = true)]
        public string UIValidationFullTypeName
        {
            get
            {
                return (string)this["validatorFullTypeName"];
            }
            set
            {
                this["validatorFullTypeName"] = value;
            }
        }
    }
}
