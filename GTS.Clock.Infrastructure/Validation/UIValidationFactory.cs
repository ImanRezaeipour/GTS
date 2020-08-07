using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GTS.Clock.Infrastructure.Validation.Configuration
{
    public static class UIValidationFactory
    {
        /// <summary>
        /// creates an instance of the requested interface.
        /// </summary>
        /// <typeparam name="TRepository">The interface of the repository 
        /// to create.</typeparam>
        /// <typeparam name="TEntity">The type of the Entity that the 
        /// repository is for.</typeparam>
        /// <returns>An instance of the interface requested.</returns>
        public static TInterface GetRepository<TInterface>()
            where TInterface : class
        {
            // Initialize the provider's default value
            TInterface repository = default(TInterface);

            string interfaceShortName = typeof(TInterface).Name;

            // Get the UIValidationMappingsConfiguration config section
            UIValidationSettings settings = (UIValidationSettings)ConfigurationManager.GetSection("ValidationConfiguration");

            // Get the type to be created
            Type repositoryType = null;

            // See if a valid interfaceShortName was passed in
            if (settings!=null && settings.UIValidationMappings.ContainsKey(interfaceShortName))
            {
                repositoryType = Type.GetType(settings.UIValidationMappings[interfaceShortName].UIValidationFullTypeName);
                if (repositoryType == null)
                {
                    throw new ArgumentNullException("خطا در ایجاد انباره. فایل باینری نوع انباره ی درخواست شده یافت نشد" + " Requested Repository Name: " + interfaceShortName);
                }               
            }

            // Throw an exception if the right Repository 
            // Mapping Element could not be found and the resulting 
            // Repository Type could not be created
            if (repositoryType == null)
            {
                throw new ArgumentNullException("خطا در ایجاد انباره. نوع انباره ی درخواست شده در فایل تنظیمات یافت نشد" + " Requested Repository Name: " + interfaceShortName);
            }

            // Create the repository, and cast it to the interface specified
            repository = Activator.CreateInstance(repositoryType) as TInterface;

            return repository;
        }

    }
}
