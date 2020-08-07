using System;
using System.Collections.Generic;
using GTS.Clock.Model.Concepts;
using System.Globalization;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.AppSetting;

namespace GTS.Clock.Model
{

    /// <summary>
    /// از این کلاس به منظور بازیابی محدوده محاسبات در هنگام اجرای قوانین استفاده می شود 
    /// </summary>
    public class AssignedScndCnpRange : IEntity
    {
        #region Properties

        public virtual decimal ID { get; set; }

        public virtual decimal PrsRangeAsgID { get; set; }

        public virtual DateTime AsgFromDate { get; set; }

        public virtual decimal CalcRangeGrpId { get; set; }

        public virtual int FromDay { get; set; }

        public virtual int FromMonth { get; set; }

        public virtual int ToDay { get; set; }

        public virtual int ToMonth { get; set; }

        public virtual int ConceptIdentifier { get; set; }

        public virtual int Order { get; set; }

        public virtual LanguagesName UsedCulture { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// بر اساس سال ورودی و مقادیر ماه و روزی که در کلاس وجود دارد مقدار "از تاریخ" را ساخته و برمی گرداند
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        public virtual DateTime FromDate(DateTime dt, LanguagesName culture)
        {
            if (culture == LanguagesName.Parsi)
            {
                //محدوده محاسبات مفهوم شمسی ذخیره شده
                return GetFromDate(dt, new PersianCalendar());
            }
            else
            {
                return GetFromDate(dt, new GregorianCalendar());
            }
        }

        /// <summary>
        /// بر اساس سال ورودی و مقادیر ماه و روزی که در کلاس وجود دارد مقدار "تا تاریخ" را ساخته و برمی گرداند
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        public virtual DateTime ToDate(DateTime dt, LanguagesName culture)
        {
            if(culture == LanguagesName.Parsi)
            {
                //محدوده محاسبات مفهوم شمسی ذخیره شده
                return this.GetToDate(dt, new PersianCalendar());
            }
            else
            {
                return this.GetToDate(dt, new GregorianCalendar());
            }
        }

        /// <summary>
        /// returns daterange order
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        public virtual int GetOrder(DateTime dt, LanguagesName culture)
        {
            return this.Order;
        }

        public virtual DateTime GetFromDate(DateTime dt, System.Globalization.Calendar calendar)
        {      
            if (this.Order == 1 && this.FromMonth > this.ToMonth)
            {
                if (calendar.GetMonth(dt) == 1)
                {
                    //در اولین بازه محدوده، ماه شروع در سال قبل قرار گرفته
                    dt = calendar.AddYears(dt, -1);
                }
            }
            else if (this.Order == 0 && this.FromMonth >= this.ToMonth)
            {
                //بازه سالانه است و شروع در سال قبل قرار گرفته
                dt = calendar.AddYears(dt, -1);
            }
            return calendar.ToDateTime(calendar.GetYear(dt), this.FromMonth, this.FromDay, 0, 0, 0, 0);
        }

        public virtual DateTime GetToDate(DateTime dt, System.Globalization.Calendar calendar)
        {
            if (this.Order == 1 && this.FromMonth > this.ToMonth)
            {
                if (calendar.GetMonth(dt) == 12)
                {
                    //در اولین بازه محدوده، ماه شروع در سال قبل قرار گرفته
                    dt = calendar.AddYears(dt, 1);
                }
            }
            if (this.Order == 12 && this.FromMonth > this.ToMonth)
            {
                if (calendar.GetMonth(dt) == 12)
                {
                    //در آخرین بازه محدوده، ماه پایان در سال بعد قرار گرفته
                    dt = calendar.AddYears(dt, 1);
                }
            }
            else if (this.Order == 0 && this.FromMonth >= this.ToMonth)
            {
                //بازه سالانه است و پایان در سال بعد قرار گرفته
                dt = calendar.AddYears(dt, 1);
            }

            if (calendar is PersianCalendar)
            {
                if (calendar.IsLeapYear(calendar.GetYear(dt)) && this.ToMonth == 12 && this.ToDay == 29)
                {
                    return calendar.ToDateTime(calendar.GetYear(dt), this.ToMonth, 30, 0, 0, 0, 0);
                }
                else
                {
                    return calendar.ToDateTime(calendar.GetYear(dt), this.ToMonth, this.ToDay, 0, 0, 0, 0);
                }
            }
            else
            {
                if (calendar.IsLeapYear(dt.Year) && this.ToMonth == 2 && this.ToDay == 28)
                {
                    //اگر سال کبسه بود و برای ماه فوریه روز 28 انتخاب شده بود
                    //به صورت خودکار با روز 29 جایگزین می شود
                    return calendar.ToDateTime(dt.Year, this.ToMonth, 29, 0, 0, 0, 0);
                }
                else
                {
                    return calendar.ToDateTime(dt.Year, this.ToMonth, this.ToDay, 0, 0, 0, 0);
                }
            }

        }

        public override bool Equals(object obj)
        {

            return (this.ID == ((AssignedScndCnpRange)obj).ID && this.PrsRangeAsgID == ((AssignedScndCnpRange)obj).PrsRangeAsgID);
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode() + this.PrsRangeAsgID.GetHashCode();
        }

        #endregion


    }
}
