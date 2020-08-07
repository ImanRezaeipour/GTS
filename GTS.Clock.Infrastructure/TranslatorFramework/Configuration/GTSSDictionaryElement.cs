using System;
using System.Configuration;

namespace GTS.Clock.Infrastructure.TranslatorFramework.Configuration
{
    public class GTSSDictionaryElement:ConfigurationElement
    {

        [ConfigurationProperty(GTSSTranslatorMappingConstants.GTSSDictionaryPersinaName, IsRequired = true,
            IsKey = true)]
        public string GTSSDictionaryPersinaName
        {
            get { return (string)this[GTSSTranslatorMappingConstants.GTSSDictionaryPersinaName]; }
            set { this[GTSSTranslatorMappingConstants.GTSSDictionaryPersinaName] = value; }
        }

        [ConfigurationProperty(GTSSTranslatorMappingConstants.GTSSDictionaryFullTypeName, IsRequired = true)]
        public string GTSSDictionaryFullTypeName
        {
            get { return (string)this[GTSSTranslatorMappingConstants.GTSSDictionaryFullTypeName]; }
            set { this[GTSSTranslatorMappingConstants.GTSSDictionaryFullTypeName] = value; }
        }

        [ConfigurationProperty(GTSSTranslatorMappingConstants.GTSSDictionaryReleaseVersionName, IsRequired = true,
            IsKey = true)]
        public string GTSSDictionaryReleaseVersion
        {
            get { return (string)this[GTSSTranslatorMappingConstants.GTSSDictionaryReleaseVersionName]; }
            set { this[GTSSTranslatorMappingConstants.GTSSDictionaryReleaseVersionName] = value; }
        }

    }
}
