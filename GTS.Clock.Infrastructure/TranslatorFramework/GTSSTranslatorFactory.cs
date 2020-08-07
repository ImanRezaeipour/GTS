using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.TranslatorFramework.Configuration;
using System.Configuration;

namespace GTS.Clock.Infrastructure.TranslatorFramework
{
    public static class GTSSTranslatorFactory
    {
        /// <summary>
        /// .یک نمونه مترجم از نوع و نسخه خواسته شده ایجاد می نماید 
        /// .همچنین براساس نسخه دیکشنری درخواست شده دیکشنری مورد نظر را به مترجم ارسال می نماید
        /// </summary>
        /// <typeparam name="TGTSSTranslator">.نوع مترجم که خروجی عملیات است</typeparam>
        /// <typeparam name="TGTSSDictionary">.نوع دیکشنری که در حین عملیات به مترجم تخصیص داده می شود</typeparam>
        /// <param name="TranslatorReleaseVer">.نسخه ی مترجم مورد نظر</param>
        /// <param name="DictionaryReleaseVer">.نسخه دیکشنری مورد نظر</param>
        /// <returns>یک نمونه از مترجم درخواست شده</returns>
        public static TGTSSTranslator GetGTSSTranslator<TGTSSTranslator, TGTSSDictionary>(string TranslatorReleaseVer, string DictionaryReleaseVer)
            where TGTSSTranslator : BaseGTSSTranslator<TGTSSDictionary>  
            where TGTSSDictionary : BaseGTSSDictionary
        {
            TGTSSTranslator GTSSTranslator = default(TGTSSTranslator);
            string interfaceShortName = typeof(TGTSSTranslator).Name;

            GTSSTranslatorSettings settings = (GTSSTranslatorSettings)ConfigurationManager.GetSection(GTSSTranslatorMappingConstants.GTSSTranslatorConfigurationSectionName);

            Type GTSSTranslatorType = null;
            Type GTSSDictionaryType = null;

            foreach (GTSSTranslatorElement translateElement in settings.GTSSTranslatorMappings)
            {
                if (translateElement.GTSSTranslatorReleaseVersion == TranslatorReleaseVer)
                {
                    GTSSTranslatorType = Type.GetType(settings.GTSSTranslatorMappings[translateElement.GTSSTranslatorPersianName].GTSSTranslatorFullTypeName);
                    foreach (GTSSDictionaryElement dicElement in translateElement.GTSSDictionaries)
                    {
                        if (dicElement.GTSSDictionaryReleaseVersion.Contains(DictionaryReleaseVer))
                        {
                            GTSSDictionaryType = Type.GetType(dicElement.GTSSDictionaryFullTypeName);
                            break;
                        }
                    }
                    break;
                }
            }

            if (GTSSTranslatorType == null)
            {
                throw new ArgumentNullException("Cannot create the GTSSTranslator.  There was one or more invalid GTSSTranslator configuration settings.");
            }
            if(GTSSDictionaryType == null)
            {
                throw new ArgumentNullException("Cannot create the GTSSDictionary.  There was one or more invalid GTSSDictionary configuration settings.");
            }

            GTSSTranslator = Activator.CreateInstance(GTSSTranslatorType, Activator.CreateInstance(GTSSDictionaryType) as TGTSSDictionary) as TGTSSTranslator;
            return GTSSTranslator;
        }

    }
}
