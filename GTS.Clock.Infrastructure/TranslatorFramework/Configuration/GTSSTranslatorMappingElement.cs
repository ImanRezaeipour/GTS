using System;
using System.Configuration;

namespace GTS.Clock.Infrastructure.TranslatorFramework.Configuration
{
    public sealed class GTSSTranslatorElement : ConfigurationElement
    {
        [ConfigurationProperty(GTSSTranslatorMappingConstants.GTSSTranslatorPersianName,
            IsKey = true, IsRequired = true)]
        public string GTSSTranslatorPersianName
        {
            get { return (string)this[GTSSTranslatorMappingConstants.GTSSTranslatorPersianName]; }
            set { this[GTSSTranslatorMappingConstants.GTSSTranslatorPersianName] = value; }
        }

        [ConfigurationProperty(GTSSTranslatorMappingConstants.GTSSTranslatorFullTypeNameAttributeName, IsRequired = true)]
        public string GTSSTranslatorFullTypeName
        {
            get { return (string)this[GTSSTranslatorMappingConstants.GTSSTranslatorFullTypeNameAttributeName]; }
            set { this[GTSSTranslatorMappingConstants.GTSSTranslatorFullTypeNameAttributeName] = value; }
        }

        [ConfigurationProperty(GTSSTranslatorMappingConstants.GTSSTranslatorReleaseVersion, IsRequired = true)]
        public string GTSSTranslatorReleaseVersion
        {
            get { return (string)this[GTSSTranslatorMappingConstants.GTSSTranslatorReleaseVersion]; }
            set { this[GTSSTranslatorMappingConstants.GTSSTranslatorReleaseVersion] = value; }
        }

        [ConfigurationProperty(GTSSTranslatorMappingConstants.GTSSDictionaryPropertyName, 
            IsRequired = true, IsDefaultCollection = true)]
        public GTSSDictionaryCollection GTSSDictionaries
        {
            get { return (GTSSDictionaryCollection)this[GTSSTranslatorMappingConstants.GTSSDictionaryPropertyName]; }
        }

    }
}
