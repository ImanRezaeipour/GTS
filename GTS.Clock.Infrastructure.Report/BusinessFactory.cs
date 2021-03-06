using System;
using System.Configuration;
using GTS.Clock.Infrastructure.Report.BusinessFramework;

namespace GTS.Clock.Infrastructure.Report
{
    public static class BusinessFactory
    {
        public static TBusiness GetBusiness<TBusiness>(string key)
            where TBusiness: class
        {
            // Initialize the provider's default value
            string keyName = key;

            // Get the repositoryMappingsConfiguration config section
            BusinessSettings settings = (BusinessSettings)ConfigurationManager.GetSection(BusinessMappingConstants.BusinessMappingsConfigurationSectionName);

            // Get the type to be created
            Type businessType = null;

            // See if a valid interfaceShortName was passed in
            if (settings.BusinessMappings.ContainsKey(keyName))
            {
                businessType = Type.GetType(settings.BusinessMappings[keyName].BusinessFullTypeName);
            }

            // Throw an exception if the right Repository 
            // Mapping Element could not be found and the resulting 
            // Repository Type could not be created
            if (businessType == null)
            {
                throw new ArgumentNullException("خطا در ایجاد نوع . نوع انباره ی درخواست شده در فایل تنظیمات یافت نشد" + " Requested Repository Name: " + keyName);
            }

            // Create the repository, and cast it to the interface specified

            return Activator.CreateInstance(businessType, new object[] { }) as TBusiness;
        }

        public static bool Exists(string key)
          
        {
            // Initialize the provider's default value
            string keyName = key;

            // Get the repositoryMappingsConfiguration config section
            BusinessSettings settings = (BusinessSettings)ConfigurationManager.GetSection(BusinessMappingConstants.BusinessMappingsConfigurationSectionName);           

            // See if a valid interfaceShortName was passed in
            if (settings.BusinessMappings.ContainsKey(keyName))
            {
                return true;
            }

            return false;
        }

    }
}
