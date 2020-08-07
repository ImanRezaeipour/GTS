using System;
using System.Configuration;

namespace GTS.Clock.Infrastructure.RepositoryFramework.Configuration
{
    public class RepositorySettings : ConfigurationSection
    {
        [ConfigurationProperty(RepositoryMappingConstants.ConfigurationPropertyName, 
            IsDefaultCollection = true)]
        public RepositoryMappingCollection RepositoryMappings
        {
            get { return (RepositoryMappingCollection)base[RepositoryMappingConstants.ConfigurationPropertyName]; }
        }
    }
}
