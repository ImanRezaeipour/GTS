using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;
using System.Globalization;


namespace GTS.Clock.Model.Concepts
{
    public class LeaveYear : IEntity
    {
        #region variables        
        private int hokmMinutes = -1000;
        private decimal _minutesInDay = -1;
        #endregion

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        /// <summary>
        /// سال
        /// </summary>
        public virtual int Year
        {
            get;
            set;
        }

        public virtual Person Person
        {
            get;
            set;
        }

        public virtual decimal PersonId { get; set; }

        public virtual LeaveSettings LeaveSettings
        {
            get
            {
                return Person.LeaveSettingsList.Last();
            }
        }

        public virtual IList<LeaveIncDec> LeaveIncDecList
        {
            get;
            set;
        }

        public virtual BudgetYear BudgetYear
        {
            get;
            set;
        }

        public virtual UsedBudget UsedBudget
        {
            get;
            set;
        }

        public virtual PersianDateTime CalculationDate
        {
            get;
            set;
        }

        /// <summary>
        /// طلب مرخصي ساعتي از سالهاي قبل که محاسبات رويش صورت ميگيرد
        /// </summary>
        public virtual int CalculatedDemandLeave
        {
            get;
            set;
        }

        /// <summary>
        /// طلب واقعی مرخصی سالهای قبل
        /// </summary>
        public virtual int DemandLeaveReal
        {
            get;
            set;
        }

        /// <summary>
        /// طلب تایید شده مرخصی سالهای قبل
        /// </summary>
        public virtual int DemandLeaveOK
        {
            get;
            set;
        }

        ///// <summary>
        ///// مجموع روزهاي حکم هاي روزانه قابل استفاده را بر ميگرداند
        ///// </summary>
        //public virtual int HokmDays1
        //{
        //    get
        //    {
        //        if (hokmDays == -1000)
        //        {
        //            ILeaveIncDecRepository incDecRep = LeaveIncDec.GetLeaveIncDecRepository(true);
        //            incDecRep.GetIncDec1(this.ID, ref hokmDays, ref hokmMinutes);
        //        }
        //        return hokmDays;
        //    }
        //}

        ///// <summary>
        ///// مجموع دقايق حکم هاي ساعتي قابل استفاده را بر ميگرداند
        ///// </summary>
        //public virtual int HokmMinutes1
        //{
        //    get
        //    {
        //        if (hokmMinutes == -1000)
        //        {
        //            ILeaveIncDecRepository incDecRep = LeaveIncDec.GetLeaveIncDecRepository(true);
        //            incDecRep.GetIncDec1(this.ID, ref hokmDays, ref hokmMinutes);
        //        }
        //        return hokmMinutes;
        //    }
        //}

        public virtual decimal MinutesInDay
        {
            set
            {
                _minutesInDay = value;
                BudgetYear.MinutesInDay = _minutesInDay;
                UsedBudget.MinutesInDay = _minutesInDay;
            }
            get
            {
                return _minutesInDay;
            }
        }

        /// <summary>
        /// سقف بدهکار شدن مرخصی استحقاقی بر حسب روز
        /// </summary>
        public virtual int MaxBorrow
        {
            get;
            set;
        }

        /// <summary>
        /// بدهکاری سالیانه
        /// </summary>
        public virtual int Debit
        {
            get;
            set;
        }


        /// <summary>
        ///  آخرین و بزرگترین تاریخی که مرخصی کم یا زیاد شده است
        /// </summary>
        public virtual UsedLeaveDetail MaxDate
        {
            get
            {
                return UsedBudget.DetailList.OrderBy(x => x.Date).LastOrDefault();

            }
        }

        /// <summary>
        /// نوع محدودیت مثل مرخصی استحقاقی یا غیره - TA_BudgetKind
        /// </summary>
        public virtual BudgetKind Kind
        {
            get;
            set;
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// طلب مرخصي ساليانه را بر حسب دقيقه بر ميگرداند
        /// </summary>
        /// <param name="minutesInDay"></param>
        /// <returns></returns>
        public virtual int GetDemandLeave()
        {
            int demandLeave = this.CalculatedDemandLeave;
            return demandLeave;
        }

        /// <summary>
        /// مقدار مرخصي باقيمانده با توجه به تنظيمات مرخصي
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public virtual int GetRemainLeave(DateTime date)
        {
            CalculationDate = new PersianDateTime(date);
            return GetRemainLeave();
        }

        /// <summary>
        /// مقدار مرخصي باقيمانده با توجه به تنظيمات مرخصي
        /// اگر منفی باشد هم صفر بر میگیرداند
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public virtual int GetRemainLeave()
        {
            try
            {
                PersianDateTime pdateTime = CalculationDate;
                int month = pdateTime.Month;
                int total = 0;// GetCurrentmonthRemainLeave(CalculationDate);

                total += GetDemandLeave();

                for (int i = 0; i < month ; i++)
                {
                    total += (BudgetYear[i] - UsedBudget[i]);
                }

                if (!LeaveSettings.DoNotUseFutureMounthLeave)
                {
                    for (int i = month; i < BudgetYear.Length; i++)
                    {
                        total += (BudgetYear[i] - UsedBudget[i]);
                    }
                }
                total += (this.MaxBorrow - this.Debit);
                return total < 0 ? 0 : total;
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در بخش مدیریت مرخصی ها.کد 101", ex);
            }
        }

        /// <summary>
        /// تعدادي مرخصي براي شخص در يک ماه خاص منظور ميکند
        /// اولويت:1-باقيمانده ماه جاري 2-طلب ساليانه 3-ماههاي گذشته 4-ماههاي آينده
        /// </summary>        
        /// <param name="remainMinutes">به روز و دقيقه تبديل ميشود</param>
        /// <param name="date"></param>
        public virtual void AddUsedLeave(int value, DateTime date)
        {
            CalculationDate = new PersianDateTime(date);
            AddUsedLeave(value);
        }

        /// <summary>
        /// تعدادي مرخصي براي شخص در يک ماه خاص منظور ميکند
        /// اولويت:1-باقيمانده ماه جاري 2-طلب ساليانه 3-ماههاي گذشته 4-ماههاي آينده
        /// </summary>        
        /// <param name="remainMinutes">به روز و دقيقه تبديل ميشود</param>
        /// <param name="date"></param>
        public virtual void AddUsedLeave(int value)
        {
            try
            {
                if (value != 0)
                {
                    PersianDateTime pdateTime = CalculationDate;
                    int month = pdateTime.Month;
                    int monthDayRemain = Convert.ToInt32(value / MinutesInDay);
                    decimal monthMinuteRemain = value - monthDayRemain * MinutesInDay;
                    int requestedLeave = value;


                    #region addUsedLeave
                    int allowedLeave = BudgetYear[month - 1] - UsedBudget[month - 1];
                    //کسر از مانده ماه جاري
                    if (value <= allowedLeave)
                    {
                        UsedBudget[month - 1] += value;
                        value = 0;
                    }
                    else
                    {
                        UsedBudget[month - 1] += allowedLeave;
                        value -= allowedLeave;
                    }
                    //کسر از طلب سالانه
                    if (value > 0)
                    {
                        int demandLeave = GetDemandLeave();
                        if (value <= demandLeave)
                        {
                            UsedBudget[month - 1] += value;
                            BudgetYear[month - 1] += value;
                            CalculatedDemandLeave -= value;
                            value = 0;
                        }
                        else
                        {
                            UsedBudget[month - 1] += demandLeave;
                            BudgetYear[month - 1] += demandLeave;
                            value -= CalculatedDemandLeave;
                            CalculatedDemandLeave = 0;
                        }
                    }
                    //کسر از طلب ماههاي قبل
                    if (value > 0)
                    {
                        for (int i = 0; i < month - 1; i++)
                        {
                            allowedLeave = BudgetYear[i] - UsedBudget[i];
                            if (value <= allowedLeave)
                            {
                                UsedBudget[month - 1] += value;
                                BudgetYear[i] -= value;
                                BudgetYear[month - 1] += value;
                                value = 0;
                                break;
                            }
                            else
                            {
                                UsedBudget[month - 1] += allowedLeave;
                                BudgetYear[i] -= allowedLeave;
                                BudgetYear[month - 1] += allowedLeave;
                                value -= allowedLeave;
                            }
                        }
                    }
                    //کسر از باقيمانده ماههاي آينده
                    if (value > 0 && !LeaveSettings.DoNotUseFutureMounthLeave)
                    {
                        for (int i = month; i < BudgetYear.Length; i++)
                        {
                            allowedLeave = BudgetYear[i] - UsedBudget[i];
                            if (value <= allowedLeave)
                            {
                                UsedBudget[month - 1] += value;
                                BudgetYear[i] -= value;
                                BudgetYear[month - 1] += value;
                                value = 0;
                                break;
                            }
                            else
                            {
                                UsedBudget[month - 1] += allowedLeave;
                                BudgetYear[i] -= allowedLeave;
                                BudgetYear[month - 1] += allowedLeave;
                                value -= allowedLeave;
                            }
                        }
                    }

                    //افزودن به بدهکاری سالیانه
                    if (value > 0)
                    {
                        this.Debit += value;
                        value = 0;
                    }

                    #endregion

                    #region Add UsedDetail
                    if (value == 0)
                    {
                        UsedLeaveDetail detail = new UsedLeaveDetail();
                        detail.UsedBudget = UsedBudget;
                        detail.Date = CalculationDate.GregorianDate;
                        detail.Value = requestedLeave;
                        UsedBudget.DetailList.Add(detail);

                    }
                    #endregion

                    if (value > 0)
                    {
                        throw new OutOfExpectedRangeException("0", GetRemainLeave(CalculationDate.GregorianDate).ToString(), value.ToString(),
                                   "GTS.Clock.Model.Concepts.BudgetYear.AddUsedLeave(end of method) Date:" + CalculationDate.ToString(), "The Total Remain Leave Is Less Than Value. \n the value checked at the top of the function but at the end of it value is not valid.there is a bad problem at the checking session \n rollback is needed here", ExceptionType.FATAL);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در بخش مدیریت مرخصی ها.کد 102", ex);
            }
        }

        /// <summary>
        /// افزايش باقيمانده مرخصي 
        /// </summary>        
        /// <param name="remainMinutes">به روز و دقيقه تبديل ميشود</param>
        /// <param name="date">تاريخ</param>
        public virtual void AddRemainLeaveMinute(int value, DateTime date)
        {
            CalculationDate = new PersianDateTime(date);
            AddRemainLeaveMinute(value);
        }

        /// <summary>
        /// افزايش باقيمانده مرخصي 
        /// </summary>        
        /// <param name="remainMinutes">به روز و دقيقه تبديل ميشود</param>
        /// <param name="date">تاريخ</param>
        public virtual void AddRemainLeaveMinute(int value)
        {
            try
            {
                if (value < 0)
                {
                    throw new Exception("AddRemainLeaveMinutes:Value <0");
                }
                PersianDateTime pdateTime = CalculationDate;
                int month = pdateTime.Month;

                #region Add UsedDetail

                UsedLeaveDetail detail = new UsedLeaveDetail();
                detail.UsedBudget = UsedBudget;
                detail.Date = CalculationDate.GregorianDate;
                detail.Value = value * -1;
                UsedBudget.DetailList.Add(detail);

                #endregion

                #region بدهکاری مرخصی
                if (this.Debit >= value)
                {
                    this.Debit -= value;
                    value = 0;
                }
                else
                {
                    value -= this.Debit;
                    this.Debit = 0;
                }
                #endregion

                if (UsedBudget[month - 1] >= value)
                {
                    UsedBudget[month - 1] -= value;
                    value = 0;
                }
                else
                {
                    value -= UsedBudget[month - 1];
                    UsedBudget[month - 1] = 0;
                }
                #region ماههای بعد
                if (!LeaveSettings.DoNotUseFutureMounthLeave)
                {
                    //ماههای بعد
                    for (int i = BudgetYear.Length - 1; i >= month && value > 0; i--)
                    {
                        if (UsedBudget[i] >= value)
                        {
                            UsedBudget[i] -= value;
                            value = 0;
                        }
                        else
                        {
                            value -= UsedBudget[month - 1];
                            UsedBudget[i] = 0;
                        }
                    }
                }
                #endregion

                #region ماههای قبل
                //ماههای قبل
                for (int i = month - 2; i >= 0 && value > 0; i--)
                {
                    if (UsedBudget[i] >= value)
                    {
                        UsedBudget[i] -= value;
                        value = 0;
                    }
                    else
                    {
                        value -= UsedBudget[month - 1];
                        UsedBudget[i] = 0;
                    }
                }
                #endregion

                #region طلب سالانه
                if (value > 0)
                {
                    CalculatedDemandLeave += value;
                    value = 0;
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در بخش مدیریت مرخصی ها.کد 103", ex);
            }
        }

        /// <summary>
        /// باتوجه به تنظيمات مرخصي , متغيرها را مقداردهي ميکند
        /// در بازه مشخص شده تمام مرخصی های داده شده را برمیگرداند تا دوباره محاسبه کند
        /// </summary>
        /// <param name="fromDateCalculation">تاریخ شروع محاسبات</param>
        /// <param name="toDateCalculation">تاریخ پایان محاسبات</param>
        public virtual void Initilize(DateTime fromDateCalculation, DateTime toDateCalculation)
        {
            try
            {
                if (Year > 0)
                {
                    if (this.Person.LeaveSettingsList == null || this.Person.LeaveSettingsList.Count == 0) 
                    {
                        throw new Exception(String.Format("خطا در بخش مدیریت مرخصی ها برای شخص {0}.کد 104", this.PersonId));
                    }

                    MinutesInDay = LeaveSettings.MinutesInDay;
                    BalanceIncDec(fromDateCalculation, toDateCalculation);

                    #region rollback last leave calculation for recalculation
                    //Rollback(fromDateCalculation.Date, toDateCalculation.Date);
                    //برگرداندن از تاریخ محاسبات تا آخر سال
                    Rollback(fromDateCalculation.Date, Infrastructure.Utility.PersianDateTime.GetEndOfShamsiYear(toDateCalculation.Date).GregorianDate);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("خطا در بخش مدیریت مرخصی ها برای شخص {0}.کد 104", this.PersonId), ex);
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// مقدار مرخصي باقيمانده ماه جاري را بر حسب دقيقه برميگرداند
        /// CurrentBudget-CurrentUsed
        /// </summary>
        /// <param name="date"></param>
        /// <param name="minutesInDay"></param>
        /// <returns></returns>
        private int GetCurrentmonthRemainLeave(PersianDateTime date)
        {
            PersianDateTime pdateTime = date;
            int month = pdateTime.Month;
            int remain = 0;

            remain = BudgetYear[month - 1] - UsedBudget[month - 1];
            return remain;
        }

        /// <summary>
        /// اعمال حکم هاي صادر شده باتوجه به تاريخ موعد آنها
        /// </summary>
        private void BalanceIncDec(DateTime fromDateCalculation, DateTime toDateCalculation)
        {
            try
            {
                List<LeaveIncDec> list = LeaveIncDecList.Where(x => x.Applyed == false && x.Date <= toDateCalculation.Date).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    LeaveIncDec lid = list[i];
                    PersianDateTime p = new PersianDateTime(lid.Date.Date);
                    if (lid.Value > 0)
                    {
                        BudgetYear[p.Month - 1] += lid.Value;
                    }
                    else//کسر کردن
                    {

                        int val = Math.Abs(lid.Value);
                        //ماههای آینده
                        for (int j = p.Month - 1; j < 12 && val > 0; j++)
                        {
                            if (val <= BudgetYear[j])
                            {
                                BudgetYear[j] -= val;
                                val = 0;
                            }
                            else
                            {
                                val -= BudgetYear[j];
                                BudgetYear[j] = 0;
                            }
                        }
                        //ماههای قبل
                        for (int j = p.Month - 2; j >= 0 && val > 0; j--)
                        {
                            if (val <= BudgetYear[j])
                            {
                                BudgetYear[j] -= val;
                                val = 0;
                            }
                            else
                            {
                                val -= BudgetYear[j];
                                BudgetYear[j] = 0;
                            }
                        }
                        //حتی اگر منفی شود
                        CalculatedDemandLeave -= val;
                        val = 0;

                    }
                    lid.Applyed = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("خطا در بخش مدیریت مرخصی ها.کد 106", ex);
            }
        }

        /// <summary>
        /// برگرداندن مرخصی های بین بازه محاسبه
        /// ابتدا و انتهای رولبک قبلا دریافت شده است
        /// </summary>
        /// <param name="delDtlOrAddBallance">مثلا در 
        /// AddRemainLeaveMinute 
        /// لازم است جزئیات مرخصی حذف نشود بلکه با درج مقدار منفی آنرا بالانس کرد
        /// که اگر باربر درست باشد حذف و اگر برابر غلط باشد بالانس میشود
        /// در حال حاضر فقط در مثالی که ذکر شد لازم است مقدار نادرست فرستاده شود</param>
        private void Rollback(DateTime startCalcDate, DateTime endCalcDate)
        {
            try
            {
                IList<UsedLeaveDetail> details = UsedBudget.DetailList.Where(x => x.Date >= startCalcDate && x.Date <= endCalcDate).ToList();
                IList<UsedLeaveDetail> shouldBeDelete = new List<UsedLeaveDetail>();
                if (details.Count > 0)
                {
                    foreach (UsedLeaveDetail dtl in details)
                    {
                        PersianDateTime pdateTime = new PersianDateTime(dtl.Date);
                        int month = pdateTime.Month;
                        int dtlValue = dtl.Value;
                        int borrow = BudgetYear[month - 1] - this.Person.GetStandardBudget(month);

                        if (dtlValue > 0)
                        {
                            #region ماه جاری

                            if (borrow > 0 && borrow >= dtlValue)
                            {
                                BudgetYear[month - 1] -= dtlValue;//از این میگیریم تا بعدا به مالک اصلی بدهیم
                                UsedBudget[month - 1] -= dtlValue;
                            }
                            else if (borrow > 0 && borrow < dtlValue)
                            {
                                BudgetYear[month - 1] -= borrow;//از این میگیریم تا بعدا به مالک اصلی بدهیم                        
                                UsedBudget[month - 1] -= dtlValue;
                                dtlValue = borrow;
                            }
                            //اصلا قرض نگرفته است و تنها از سهمیه خودش استفاده کرده است
                            else if (borrow == 0 && BudgetYear[month - 1] - UsedBudget[month - 1] >= 0)
                            {
                                UsedBudget[month - 1] -= dtlValue;
                                dtlValue = 0;
                            }
                            //مثلا inc dec leave باعث منفی شدن باشد
                            else if (borrow <= 0 && BudgetYear[month - 1] >= dtlValue)
                            {
                                BudgetYear[month - 1] -= dtlValue;//از این میگیریم تا بعدا به مالک اصلی بدهیم
                                UsedBudget[month - 1] -= dtlValue;
                                //throw new Exception("budget - standard budget <0");
                            }
                            else// Budget[month - 1] < dtlValue
                            {
                                throw new Exception("Budget-Rollback Error - Dtail Value Less Than budget ");
                            }

                            #endregion

                            //برگرداندن بصورت معکوس البته برگرداندن حالت عادی نیز اشکالی ایجاد نمیکند

                            #region ماههای بعد
                            if (!LeaveSettings.DoNotUseFutureMounthLeave)
                            {
                                //ماههای بعد
                                for (int i = BudgetYear.Length - 1; i >= month && dtlValue > 0; i--)
                                {
                                    borrow = BudgetYear[i] - this.Person.GetStandardBudget(i + 1);
                                    //قرض داده باشد و ... 
                                    if (borrow < 0 && Math.Abs(borrow) >= dtlValue)
                                    {
                                        BudgetYear[i] += dtlValue;
                                        dtlValue = 0;
                                    }
                                    else if (borrow < 0 && Math.Abs(borrow) < dtlValue)
                                    {
                                        BudgetYear[i] = this.Person.GetStandardBudget(i + 1);
                                        dtlValue -= Math.Abs(borrow);
                                    }
                                }
                            }
                            #endregion

                            #region ماههای قبل
                            //ماههای قبل
                            for (int i = month - 2; i >= 0 && dtlValue > 0; i--)
                            {
                                borrow = BudgetYear[i] - this.Person.GetStandardBudget(i + 1);
                                //قرض داده باشد و ... 
                                if (borrow < 0 && Math.Abs(borrow) >= dtlValue)
                                {
                                    BudgetYear[i] += dtlValue;
                                    dtlValue = 0;
                                }
                                else if (borrow < 0 && Math.Abs(borrow) < dtlValue)
                                {
                                    BudgetYear[i] = this.Person.GetStandardBudget(i + 1);
                                    dtlValue -= Math.Abs(borrow);
                                }
                            }
                            #endregion

                            #region طلب سالانه
                            if (dtlValue > 0)
                            {
                                CalculatedDemandLeave += dtlValue;
                                dtlValue = 0;
                            }
                            #endregion
                        }

                        /*اگر منفی بود مقدار سهمیه را زیاد کن
                        عدد منفی به معنای آن است که قبلا افزایش مرخصی به دلیل 
                        جبران مرخصی کسر شده یا بر اساس قوانین افزایش دهنده مرخصی از تابع 
                        AddRemainLeave

                        //صورت گرفته است
                        مثلا مرخصی گرفته شده به 8 ساعت میرسد وقانونی میگوید که اگر مرخصی ساعتی
                        به 6 ساعت رسید کل روز مرخصی شود و مرخصی کل روز برابر 7 ساعت است درنتیجه 
                        یک ساعت منفی درج میشود

                        */
                        else//dtlValue<0
                        {
                            dtlValue *= -1;
                            
                            BudgetYear[month - 1] += dtlValue;
                            dtlValue = 0;

                            shouldBeDelete.Add(dtl);
                            continue;
                        }

                        shouldBeDelete.Add(dtl);

                    }
                    foreach (UsedLeaveDetail dtl in shouldBeDelete)
                    {
                        UsedBudget.DetailList.Remove(dtl);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در بخش مدیریت مرخصی ها.کد 105", ex);
            }
        }


        #endregion


        #endregion

        #region Static Methods

        public static LeaveYear GetBudgetYearFacorty(BudgetKind kind)
        {
            switch (kind)
            {
                case BudgetKind.LEAVE: return new LeaveBudgetYear();
                default: throw new ArgumentException("نوع سهمیه بندی ناشناس است");
            }
        }
        #endregion
    }
}
