using System;
using System.Configuration;

namespace GTS.Clock.Infrastructure.TranslatorFramework.Configuration
{
    public class GTSSTranslatorSettings : ConfigurationSection
    {
        [ConfigurationProperty(GTSSTranslatorMappingConstants.ConfigurationPropertyName,
            IsDefaultCollection = true)]
        public GTSSTranslatorCollection GTSSTranslatorMappings
        {
            get { return (GTSSTranslatorCollection)base[GTSSTranslatorMappingConstants.ConfigurationPropertyName]; }
        }
    }
}
