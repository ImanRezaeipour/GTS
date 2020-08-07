using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Model;

namespace GTS.Clock.Business.AppSettings
{
    public class BApplicationSettings
    {
        private static ApplicationSettings appSetting;
        private static string GTSLicenseKey = "AtlasLicenseKey";
        private static string GTSLicenseSplitor = "*-*";
        private static int LicenseValue = 0;
        public static ApplicationSettings CurrentApplicationSettings
        {
            get
            {
                if (!SessionHelper.HasSessionValue(SessionHelper.GTSApplicationSettings))
                {
                    EntityRepository<ApplicationSettings> appRep = new EntityRepository<ApplicationSettings>(false);
                    IList<ApplicationSettings> appSetList = appRep.GetAll();
                    appSetting = appSetList.FirstOrDefault();
                    if (appSetting == null)
                    {
                        appSetting = new ApplicationSettings();
                    }

                    SessionHelper.SaveSessionValue(SessionHelper.GTSApplicationSettings, appSetting);
                    //Utility.CatchWrite("SettingLoaded", 1, 5);                    
                }
                object obj = SessionHelper.GetSessionValue(SessionHelper.GTSApplicationSettings);
                if (obj != null)
                {
                    return (ApplicationSettings)obj;
                }
                return appSetting;
            }
        }

        public static void Update(ApplicationSettings appSet) 
        {
            if (appSet.ID > 0) 
            {
                EntityRepository<ApplicationSettings> rep = new EntityRepository<ApplicationSettings>(false);
                rep.Update(appSet);
                SessionHelper.ClearSessionValue(SessionHelper.GTSApplicationSettings);
            }
        }

        /// <summary>
        /// بررسی تعداد لایسنس خریداری شده
        /// </summary>
        public static void CheckGTSLicense()
        {
            try
            {
                if (LicenseValue == 0)
                    LicenseValue = GetLicense();

                int personCount = 0;
                if (!SessionHelper.HasSessionValue(SessionHelper.LicensePersonCount))
                {
                    personCount = new BPerson().GetActivePersonCount();
                    SessionHelper.SaveSessionValue(SessionHelper.LicensePersonCount, personCount);
                }
                else 
                {
                    personCount = Utility.ToInteger(SessionHelper.GetSessionValue(SessionHelper.LicensePersonCount));
                }

                if (personCount > LicenseValue) 
                {
                    throw new BaseException("تعداد پرسنل داخل پایگاه داده بیشتر از مقدار لاسنس میباشد.لطفا نسبت به تهیه لاسنس اقدام نمایید", "اطلس حضور و غیاب");
                }
            }
            catch (Exception ex) 
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// با توجه به کلید لایسنس ,پس از بررسی فرمت آن تعداد را برمیگرداند
        /// </summary>
        /// <returns></returns>
        private static int GetLicense() 
        {
            if (!Utility.IsEmpty(Utility.ReadAppSetting(GTSLicenseKey)))
            {
                string license = Utility.ReadAppSetting(GTSLicenseKey);
                if (license.Contains(GTSLicenseSplitor))
                {
                    string[] parts = Utility.Spilit(license, GTSLicenseSplitor);
                    if (parts != null && parts.Length == 2)
                    {
                        int userCount = Utility.ToInteger(parts[1]);

                        string machineKey = Utility.ServerFingerPrint + "-" + userCount.ToString();
                        if (Utility.VerifyHashCode(machineKey, parts[0]))
                        {
                            return userCount;
                        }
                    }
                }
            }
            throw new Exception("لایسنس نامعتبر است");
        }
    }
}
