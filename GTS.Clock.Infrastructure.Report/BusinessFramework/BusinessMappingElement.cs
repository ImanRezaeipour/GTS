using System;
using System.Configuration;

namespace GTS.Clock.Infrastructure.Report.BusinessFramework
{
    public sealed class BusinessMappingElement : ConfigurationElement
    {
        [ConfigurationProperty(BusinessMappingConstants.InterfaceKeyAttributeName, 
            IsKey = true, IsRequired = true)]
        public string InterfaceKey
        {
            get 
            {
                return (string)this[BusinessMappingConstants.InterfaceKeyAttributeName]; 
            }
            set 
            {
                this[BusinessMappingConstants.InterfaceKeyAttributeName] = value; 
            }
        }

        [ConfigurationProperty(BusinessMappingConstants.BusinessFullTypeNameAttributeName, 
            IsRequired = true)]
        public string BusinessFullTypeName
        {
            get 
            { 
                return (string)this[BusinessMappingConstants.BusinessFullTypeNameAttributeName]; 
            }
            set 
            { 
                this[BusinessMappingConstants.BusinessFullTypeNameAttributeName] = value; 
            }
        }
    }
}
