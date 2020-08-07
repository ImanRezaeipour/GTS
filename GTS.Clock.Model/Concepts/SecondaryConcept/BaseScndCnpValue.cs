using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.Concepts
{
    public abstract class BaseScndCnpValue : IConceptValue, IEntity
    {

        #region IConceptValue Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual int Value
        {
            get;
            set;
        }

        public virtual string FromPairs { get; set; }

        public virtual string ToPairs { get; set; }

        public virtual DateTime FromDate
        {
            get;
            set;
        }

        public virtual DateTime ToDate
        {
            get;
            set;
        }

        public virtual DateTime CalculationDate
        {
            get;
            set;
        }

        public virtual bool IsValid
        {
            get;
            set;
        }

        public virtual string ExValue
        {
            get { return Utility.IntTimeToRealTime(this.Value); }
        }

        public virtual SecondaryConcept Concept
        {
            get;
            set;
        }

        public virtual string Index
        {
            get;
            set;
        }

        public virtual Person Person
        {
            get;
            set;
        }

        public virtual decimal CalcRangeGrpId { get; set; }

        public virtual decimal CalcDateRangeId { get; set; }

        #endregion
 
        #region Methods

        /// <summary>
        /// یک شناسه منحصر به فرد برای درج در "هشت تی بل" ایجاد می نماید
        /// این شناسه براساس "شماره پرسنلی"، "شناسه مفهوم" و تاریخ مقداردهی مفهوم ایجاد می شود
        /// <remarks>اگر این تابع را قبل از مقداردهی به خصوصیات "شخص" و "مفهوم" فراخوانی نمایید با خطا مواجه خواهید شد</remarks>
        /// </summary>
        /// <param name="CalculationDate">تاریخ مقداردهی مفهوم</param>
        /// <returns>شناسه منحصر به فرد</returns>
        public virtual string GetIndex(PersianDateTime CalculationDate)
        {
            if (this.Person == null)
                throw new BaseException("خصوصیت شخص مقداردهی نشده است", "BaseScndCnpValue.GetIndex()");
            if (this.Concept == null)
                throw new BaseException("خصوصیت مفهوم مقداردهی نشده است", "BaseScndCnpValue.GetIndex()");
            return BaseScndCnpValue.GetIndex(this.Person.ID, this.Concept.IdentifierCode, CalculationDate);
        }

        /// <summary>
        /// یک شناسه منحصر به فرد برای درج در "هشت تی بل" ایجاد می نماید
        /// این شناسه براساس "شماره پرسنلی"، "شناسه مفهوم" و تاریخ مقداردهی مفهوم ایجاد می شود
        /// <remarks>اگر این تابع را قبل از مقداردهی به خصوصیات "شخص" و "مفهوم" فراخوانی نمایید با خطا مواجه خواهید شد</remarks>
        /// </summary>
        /// <param name="CalculationDate">تاریخ مقداردهی مفهوم</param>
        /// <returns>شناسه منحصر به فرد</returns>
        public virtual string GetIndex(DateTime CalculationDate)
        {
            if (this.Person == null)
                throw new BaseException("خصوصیت شخص مقداردهی نشده است", "BaseScndCnpValue.GetIndex()");
            if (this.Concept == null)
                throw new BaseException("خصوصیت مفهوم مقداردهی نشده است", "BaseScndCnpValue.GetIndex()");
            return BaseScndCnpValue.GetIndex(this.Person.ID, this.Concept.IdentifierCode, CalculationDate);            
        }

        public override string ToString()
        {
            string str = " ";
            str += this.Concept.Name;
            str += " : " + this.ExValue.ToString();
            if (this.Concept.Type == ScndCnpPairableType.PSC)
            {               
                str += " [";
                foreach (IPair pv in ((PairableScndCnpValue)this).Pairs)
                {
                    str += "(" + Utility.IntTimeToRealTime(pv.From) + "," + Utility.IntTimeToRealTime(pv.To) + ")";
                }
                str += "] ";
            }
        
            return str;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// بر اساس نوع مفهوم ثانویه یک نمونه از کلاس "مقدار مفهوم ثانویه" ایجاد می نماید 
        /// مقدار مفهوم ثانویه بر دو نوع "زوج مرتبی" و "غیر زوج مرتبی" می باشد
        /// </summary>
        /// <param name="scndCnp">مفهوم ثانویه ای که باید بر اساس آن مقدار مفهوم ثانویه ایجاد شود</param>
        /// <returns>یک نمونه از کلاس مقدار مفهوم ثانویه پایه</returns>
        public static BaseScndCnpValue GetScndCnpValueFacorty(SecondaryConcept scndCnp)
        {
            switch (scndCnp.Type)
            {
                case ScndCnpPairableType.PSC: return new PairableScndCnpValue();
                case ScndCnpPairableType.NPSC: return new NonePairableScndCnpValue();
                default: throw new ArgumentException("نوع مفهوم ثانویه ناشناس است");
            }
        }

        /// <summary>
        /// یک شناسه منحصر به فرد برای درج در "هشت تی بل" ایجاد می نماید
        /// این شناسه براساس "شماره پرسنلی"، "شناسه مفهوم" و تاریخ مقداردهی مفهوم ایجاد می شود
        /// </summary>
        /// <param name="PersonId">شماره پرسنلی</param>
        /// <param name="ScndCnpID">شناسه مفهوم</param>
        /// <param name="CalculationDate">تاریخ مقداردهی مفهوم</param>
        /// <returns>شناسه منحصر به فرد</returns>
        public static string GetIndex(Decimal PersonId, decimal ScndCnpID, PersianDateTime CalculationDate)
        {
            return BaseScndCnpValue.GetIndex(PersonId, ScndCnpID, CalculationDate.GregorianDate);
        }

        /// <summary>
        /// یک شناسه منحصر به فرد برای درج در "هشت تی بل" ایجاد می نماید
        /// این شناسه براساس "شماره پرسنلی"، "شناسه مفهوم" و تاریخ مقداردهی مفهوم ایجاد می شود
        /// </summary>
        /// <param name="PersonId">شماره پرسنلی</param>
        /// <param name="ScndCnpID">شناسه مفهوم</param>
        /// <param name="CalculationDate">تاریخ مقداردهی مفهوم</param>
        /// <returns>شناسه منحصر به فرد</returns>
        public static string GetIndex(Decimal PersonId, decimal ScndCnpID, DateTime CalculationDate)
        {
            return PersonId.ToString() + ScndCnpID.ToString() + CalculationDate.ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// یک شناسه منحصر به فرد برای مفهوم ماهانه و درج در "هشت تی بل" ایجاد می نماید
        /// این شناسه براساس "شماره پرسنلی"، "شناسه مفهوم" و محدوده محاسبه مفهوم ماهانه ایجاد می شود
        /// </summary>
        /// <param name="PersonId">شماره پرسنلی</param>
        /// <param name="ScndCnpID">شناسه مفهوم</param>
        /// <param name="CalculationDate">تاریخ مقداردهی مفهوم</param>
        /// <returns>شناسه منحصر به فرد</returns>
        public static string GetIndex(Decimal PersonId, decimal ScndCnpID, DateRange CalculationDateRange)
        {
            return PersonId.ToString() + ScndCnpID.ToString() + CalculationDateRange.FromDate.ToString("yyyy/MM/dd") + CalculationDateRange.ToDate.ToString("yyyy/MM/dd");
        }

        //public static BaseScndCnpValue operator++(BaseScndCnpValue Source1, BaseScndCnpValue Source2)
        //{
        //    if(Source2 == null)
        //        throw new NotImplementedException();
        //    if(Source1 == null)
        //        Source1 = 
        //}

        #endregion

    }
}
