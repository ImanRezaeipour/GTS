using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.CalculatorSchedulerFramework.Configuration;
using System.Configuration;

namespace GTS.Clock.Infrastructure.CalculatorSchedulerFramework
{
    public static class CalculatorSchedulerFactory
    {
        /// <summary>
        /// زمابندی که نام آن مشخص شده است را در تنظیمات پیدا نموده و یک نمونه از آن برمی گرداند
        /// </summary>
        /// <param name="Setting">فایل تنظیمات که مشخصات زمانبند در آن قرار دارد</param>
        /// <param name="SchedulerName">نام زمانبند که باید یک نمونه از آن ساخته شود</param>
        /// <returns></returns>
        public static ICalculatorScheduler GetCalculatorScheduler_Exculded(CalculatorSchedulerSettings Setting, string SchedulerName)
        {
            Type CalculatorSchedulerType = null;
            if (Setting.CalculatorSchedulers.ContainsKey(SchedulerName))
            {
                CalculatorSchedulerType = Type.GetType(Setting.CalculatorSchedulers[SchedulerName].SchedulerFullTypeName);
            }

            if (CalculatorSchedulerType == null)
            {
                throw new ArgumentNullException("خطا در ایجاد زمانبند محاسبه گر. نام زمانبند محاسبه گر درخواست شده در فایل تنظیمات یافت نشد" + " Requested CalculatorScheduler Name: " + SchedulerName);
            }

            return Activator.CreateInstance(CalculatorSchedulerType) as ICalculatorScheduler;
        }

        /// <summary>
        /// تنظیمات زمانبند محاسبه گر را برمی گرداند
        /// </summary>
        /// <returns></returns>
        public static CalculatorSchedulerSettings GetSetting()
        {
            return (CalculatorSchedulerSettings)ConfigurationManager.GetSection(CalculatorSchedulerConstants.CalculatorSchedulerConfigurationSectionName);
        }

        /// <summary>
        /// تابع بررسی وقوع شرط زمانبند هریک از زمانبدهای تعریف شده در فایل تنظیمات
        /// را فراخوانی می نماید
        /// </summary>
        /// <param name="Setting">تنظیماتی که نام زمانبندها در آن قرار دارد</param>
        /// <param name="Now">تاریخ/ساعتی که به عنوان پارامتر به توابع زمانبندها ارسال می گردد</param>
        /// <returns></returns>
        public static bool IsConditionOccurenced(CalculatorSchedulerSettings Setting, DateTime Now)
        {
            //بجای درگیر کردن مدل , همینجا یک مقایسه ساده انجام میدهیم

            int nowTime =Utility.Utility.RealTimeToIntTime(String.Format("{0}:{1}", Now.Hour, Now.Minute));
            int fromTime = Utility.Utility.RealTimeToIntTime(Setting.FromTime);
            int toTime = Utility.Utility.RealTimeToIntTime(Setting.ToTime);

            if (fromTime < toTime)
            {
                if (fromTime < nowTime && toTime > nowTime)
                    return true;
            }
            else
            {
                if (fromTime < nowTime || toTime > nowTime)
                    return true;
            }
            return false;
            //foreach (string SchedulerName in Setting.ServiceableSchedulers.Split('|'))
            //{
            //    if (CalculatorSchedulerFactory.GetCalculatorScheduler(Setting, SchedulerName).IsConditionOccurenced(Setting, Now))
            //        return true;
            //}
            //return false;
        }
    }
}
