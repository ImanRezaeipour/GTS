using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GTS.Clock.ModelEngine;
using System.Reflection;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.ModelEngine.Concepts;
using GTS.Clock.ModelEngine.Concepts.Operations;
using GTS.Clock.ModelEngine.AppSetting;
using GTS.Clock.ModelEngine.ELE;

namespace GTS.Clock.Business.Calculator
{
    public class RuleCalculator : GeneralRuleCalculator
    {

        #region Constructors
        /// <summary>
        /// ."تنها سازنده کلاس "محاسبه گر اشياء
        /// </summary>
        /// <param name="Person">پرسنلي که محاسبات براي او در حال انجام است</param>
        /// <param name="CategorisedRule">قانوني که منجر به فراخواني مفاهيم از کلاس "محاسبه گر قانون" خواهد شد</param>
        /// <param name="CalculateDate">تاريخ انجام محاسبات</param>
        public RuleCalculator(IEngineEnvironment engineEnvironment)
            : base(engineEnvironment, new ConceptCalculator(engineEnvironment))
        {
            this.logLock = !GTSAppSettings.RuleDebug;
        }
        #endregion 

        #region Defined Methods

        #region قوانين متفرقه
      

        #endregion

        #region قوانين کارکرد
        /// <summary>قانون کارکرد 4-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي بيست و هشت-28 درنظر گرفته شده است</remarks>
        /// کارکرد 4-1: روزهاي کاري بيشتر از (ساعت کارکرد در روز) يک روز کاري حساب شود
        public override void R2003(AssignedRule MyRule)
        {
            //6 کارکردلازم
            //7 کارکرددرروز
            //4 کارکردخالص روزانه
            //3004 غيبت روزانه
            //2023 مجموع ماموریت ساعتی
            //1082 مجموع انواع مرخصی ساعتی
            GetLog(MyRule, DebugRuleState.Before , 4);

            // به مرخصی بی حقوق , کارکرد روزانه و بیمه تعلق نمیگیرد
            if (this.DoConcept(6).Value > this.DoConcept(7).Value && this.DoConcept(1091).Value == 0)
            {
                this.DoConcept(2023);
                this.DoConcept(1082);
                //غیبت روزانه نداشت و همه روز غیبت نداشت
                //قانونی که غیبت ساعتی را به روزانه تبدیل میکند اولویت پایین تری دارد
                if (this.DoConcept(3004).Value == 0
                    &&
                    Operation.Differance(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(3028)).Value > 0)
                {
                    int karkerd = this.DoConcept(13).Value > this.DoConcept(6).Value ? this.DoConcept(6).Value : this.DoConcept(13).Value;

                    if (this.DoConcept(1090).Value >= 1)
                    {
                        this.DoConcept(4).Value = 1;
                    }
                    else
                    {
                        this.DoConcept(4).Value = 1;
                    }
                }
            }
            GetLog(MyRule, DebugRuleState.After, 4);
        }

        public override void R2002(AssignedRule MyRule)
        {
            //3004 غيبت روزانه
            //7 کارکرددرروز
            //2 کارکردخالص ساعتي 
            //کارکردخالص روزانه 4

            if (this.DoConcept(3004).Value == 0 && this.DoConcept(1091).Value !=0 &&
                
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value >0 &&
                this.Person.GetShiftByDate(this.RuleCalculateDate).ShiftType == ShiftTypesEnum.WORK)
            {
                GetLog(MyRule, DebugRuleState.Before ,4);
                this.DoConcept(4).Value = 1;
                GetLog(MyRule, DebugRuleState.After, 4);
            }
        }

        /// <summary>
        /// اعمال کارکرد در تعطیلات
        /// </summary>
        /// <param name="MyRule"></param>
        //public override  void R2013(AssignedRule MyRule)
        //{
        //    //23 مفهوم تعطیلات جزو کارکرد حساب شود
        //    //24 مفهوم تعطیلات رسمی جزو کارکرد حساب شود 
        //    //25 مفهوم روزهای غیر کاری جزو کارکرد حساب شود   
        //    //4 کارکرد خالص روزانه
        //    //6 مفهوم کارکردلازم
        //    //3004 غيبت روزانه
        //    //7 کارکرددرروز    
        //    //MyRule.Memory = 123;
        //    bool hourlyWork = true, dailyWork = true;
        //    if (MyRule.HasParameter("HourlyWork", this.RuleCalculateDate))
        //    {
        //        hourlyWork = MyRule["HourlyWork", this.RuleCalculateDate].ToInt() > 0;
        //    }
        //    if (MyRule.HasParameter("DailyWork", this.RuleCalculateDate))
        //    {
        //        dailyWork = MyRule["DailyWork", this.RuleCalculateDate].ToInt() > 0;
        //    }

        //    this.DoConcept(23).Value = MyRule["TatilGheirRasmi", this.RuleCalculateDate].ToInt();
        //    this.DoConcept(24).Value = MyRule["TatilRasmi", this.RuleCalculateDate].ToInt();
        //    this.DoConcept(25).Value = MyRule["GheirKari", this.RuleCalculateDate].ToInt();
        //    int normWork = this.DoConcept(7).Value;
        //    bool finish = false;
        //    //اگر روز قبل تعطیلی کارکرد داشتیم پس امروز که تعطیل است هم داریم و تمام
        //    if ((EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2") && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
        //        || this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
        //    {
        //        bool workYesterday = this.DoConcept(4, this.RuleCalculateDate.AddDays(-1)).Value > 0;
        //        if (workYesterday)
        //        {
        //            GetLog(MyRule, DebugRuleState.Before, 2, 4);
        //            if (hourlyWork)
        //                this.DoConcept(2).Value = normWork;
        //            if (dailyWork)
        //                this.DoConcept(4).Value = 1;
        //            finish = true;
        //            GetLog(MyRule, DebugRuleState.After, 2, 4);
        //        }
        //    }
        //    if (!finish && ((EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2")
        //        || this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0))
        //        )
        //    {

        //        GetLog(MyRule, DebugRuleState.Before, 2, 4, 13, 6, 3004);

        //        bool x1 = this.DoConcept(23).Value > 0 ? true : false;
        //        bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0;

        //        bool y1 = this.DoConcept(24).Value > 0 ? true : false;
        //        bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0;

        //        bool z1 = this.DoConcept(25).Value > 0 ? true : false;
        //        bool z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0 ? true : false;

        //        int beforeDays = 0;
        //        int bearkCounter = 10;
        //        IList<DateTime> dateList = new List<DateTime>();
        //        while (x2 | y2 | z2)
        //        {

        //            bearkCounter--;
        //            if (bearkCounter == 0) { break; }

        //            beforeDays++;

        //            if (this.Person.EmploymentDate > this.RuleCalculateDate.AddDays(-beforeDays))
        //            {
        //                break;
        //            }

        //            x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "2") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0;

        //            y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "1") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0;

        //            z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0 ? true : false;

        //            if ((x1 & x2) | (y1 & y2) | (z1 & z2))
        //            {
        //                dateList.Add(this.RuleCalculateDate.AddDays(-beforeDays));
        //            }
        //        }
        //        if (dateList.Count > 0)
        //        {
        //            bool work = false, absent = false, leaveBihoghogh = false;

        //            //روز قبل یا روز بعد تعطیلات
        //            work = this.DoConcept(4, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0
        //                &&
        //                this.DoConcept(4).Value > 0;

        //            absent = this.DoConcept(3004, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0
        //                &&
        //                this.DoConcept(3004).Value > 0;

        //            leaveBihoghogh = this.DoConcept(1091, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0
        //               &&
        //               this.DoConcept(1091).Value > 0;



        //            foreach (DateTime calcDate in dateList)
        //            {
        //                if (calcDate < this.MinAssgnRuleDate)
        //                {
        //                    break;
        //                }
        //                //تعطیل شیفت دار نباید حساب شود
        //                if (this.Person.GetShiftByDate(calcDate).Value > 0
        //                    && !EngEnvironment.HasCalendar(calcDate, "1", "2"))
        //                    continue;

        //                if (work)
        //                {
        //                    if (hourlyWork)
        //                        this.DoConcept(2, calcDate).Value = normWork;
        //                    if (dailyWork)
        //                        this.DoConcept(4, calcDate).Value = 1;
        //                }
        //                else if (absent)
        //                {
        //                    if (hourlyWork)
        //                        this.DoConcept(2, calcDate).Value = 0;
        //                    if (dailyWork)
        //                        this.DoConcept(4, calcDate).Value = 0;
        //                }
        //                else if (leaveBihoghogh)
        //                {
        //                    if (hourlyWork)
        //                        this.DoConcept(2, calcDate).Value = 0;
        //                    if (dailyWork)
        //                        this.DoConcept(4, calcDate).Value = 0;
        //                }
        //                else //این روز احتمالا بدلیل نداشتن قانون محاسبه نشده است
        //                {
        //                    if (hourlyWork)
        //                        this.DoConcept(2, calcDate).Value = normWork;
        //                    if (dailyWork)
        //                        this.DoConcept(4, calcDate).Value = 1;
        //                }
        //                //اگر شخص حضور و اضافه کار داشت نباید اعمال گردد
        //                if (this.DoConcept(13, calcDate).Value == 0)
        //                {
        //                    this.ReCalculate(13, calcDate);
        //                }
        //                if (this.Person.GetShiftByDate(calcDate).Value > 0)
        //                    this.DoConcept(6, calcDate).Value
        //                        = this.Person.GetShiftByDate(calcDate).Value;
        //            }
        //            //محاسبه دوباره کارکرد ماهانه

        //            for (int i = beforeDays - 1; i >= 0 && this.RuleCalculateDate.AddDays(-i) >= this.MinAssgnRuleDate; i--)
        //            {
        //                if (!this.CalcDateZone.IsContain(this.RuleCalculateDate.AddDays(-i)))
        //                    continue;
        //                this.ReCalculate(5, this.RuleCalculateDate.AddDays(-i));
        //            }
        //        }

        //        GetLog(MyRule, DebugRuleState.After, 2, 4, 13, 6, 3004);
        //    }
        //    if (this.DoConcept(1091).Value > 0)
        //    {
        //        this.DoConcept(4).Value = 0;
        //        // this.DoConcept(2).Value = this.Person.GetShiftByDate(this.RuleCalculateDate).Value;
        //        this.DoConcept(13).Value = this.DoConcept(2).Value + this.DoConcept(4002).Value;
        //    }
        //}

        /// <summary>
        /// اعمال کارکرد در تعطیلات
        /// طبق نامه داده شده روزهای تعطیل همیشه کارکرد حساب شود
        /// </summary>
        /// <param name="MyRule"></param>
        public override  void R2013(AssignedRule MyRule)
        {
            //23 مفهوم تعطیلات جزو کارکرد حساب شود
            //24 مفهوم تعطیلات رسمی جزو کارکرد حساب شود 
            //25 مفهوم روزهای غیر کاری جزو کارکرد حساب شود   
            //4 کارکرد خالص روزانه
            //6 مفهوم کارکردلازم
            //3004 غيبت روزانه
            //7 کارکرددرروز    
            //MyRule.Memory = 123;
            GetLog(MyRule, DebugRuleState.Before , 2, 4, 13, 6, 5, 3004);
            bool hourlyWork = true, dailyWork = true;
            if (MyRule.HasParameter("HourlyWork", this.RuleCalculateDate))
            {
                hourlyWork = MyRule["HourlyWork", this.RuleCalculateDate].ToInt() > 0;
            }
            if (MyRule.HasParameter("DailyWork", this.RuleCalculateDate))
            {
                dailyWork = MyRule["DailyWork", this.RuleCalculateDate].ToInt() > 0;
            }

            this.DoConcept(23).Value = MyRule["TatilGheirRasmi", this.RuleCalculateDate].ToInt();
            this.DoConcept(24).Value = MyRule["TatilRasmi", this.RuleCalculateDate].ToInt();
            this.DoConcept(25).Value = MyRule["GheirKari", this.RuleCalculateDate].ToInt();
            int normWork = this.DoConcept(7).Value;
            bool finish = false;
            //اگر روز قبل تعطیلی کارکرد داشتیم پس امروز که تعطیل است هم داریم و تمام
            if ((EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2") && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
                || this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
            {
                bool workYesterday = this.DoConcept(4, this.RuleCalculateDate.AddDays(-1)).Value > 0;
                if (workYesterday)
                {
                    GetLog(MyRule, DebugRuleState.Before, 2, 4);
                    if (hourlyWork)
                        this.DoConcept(2).Value = normWork;
                    if (dailyWork)
                        this.DoConcept(4).Value = 1;
                    finish = true;
                    GetLog(MyRule, DebugRuleState.After, 2, 4);
                }
            }
            if (!finish && ((EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2")
                || this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0))
                )
            {

                

                bool x1 = this.DoConcept(23).Value > 0 ? true : false;
                bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate, "2") && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0;

                bool y1 = this.DoConcept(24).Value > 0 ? true : false;
                bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate, "1") && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0;

                bool z1 = this.DoConcept(25).Value > 0 ? true : false;
                bool z2 = this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0 ? true : false;

                int beforeDays = 0;
                int bearkCounter = 10;
                IList<DateTime> dateList = new List<DateTime>();
                while (x2 | y2 | z2)
                {

                    bearkCounter--;
                    if (bearkCounter == 0) { break; }

                    beforeDays++;

                    if (this.Person.EmploymentDate > this.RuleCalculateDate.AddDays(-beforeDays))
                    {
                        break;
                    }

                    x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate, "2") && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0;

                    y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate, "1") && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0;

                    z2 = this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0 ? true : false;

                    if ((x1 & x2) | (y1 & y2) | (z1 & z2))
                    {
                        dateList.Add(this.RuleCalculateDate);
                    }
                }
                if (dateList.Count > 0)
                {
                    bool work = false, absent = false, leaveBihoghogh = false;

                    //روز قبل یا روز بعد تعطیلات
                    work = this.DoConcept(4, this.RuleCalculateDate).Value > 0
                        &&
                        this.DoConcept(4).Value > 0;

                    absent = this.DoConcept(3004, this.RuleCalculateDate).Value > 0
                        &&
                        this.DoConcept(3004).Value > 0;

                    leaveBihoghogh = this.DoConcept(1091, this.RuleCalculateDate).Value > 0
                       &&
                       this.DoConcept(1091).Value > 0;



                    foreach (DateTime calcDate in dateList)
                    {
                        if (calcDate < this.MinAssgnRuleDate)
                        {
                            break;
                        }
                        //تعطیل شیفت دار نباید حساب شود
                        if (this.Person.GetShiftByDate(calcDate).Value > 0
                            && !EngEnvironment.HasCalendar(calcDate, "1", "2"))
                            continue;

                        if (work)
                        {
                            if (hourlyWork)
                                this.DoConcept(2, calcDate).Value = normWork;
                            if (dailyWork)
                                this.DoConcept(4, calcDate).Value = 1;
                        }
                        else if (absent)
                        {
                            if (hourlyWork)
                                this.DoConcept(2, calcDate).Value = 0;
                            if (dailyWork)
                                this.DoConcept(4, calcDate).Value = 0;
                        }
                        else if (leaveBihoghogh)
                        {
                            if (hourlyWork)
                                this.DoConcept(2, calcDate).Value = 0;
                            if (dailyWork)
                                this.DoConcept(4, calcDate).Value = 0;
                        }
                        else //این روز احتمالا بدلیل نداشتن قانون محاسبه نشده است
                        {
                            if (hourlyWork)
                                this.DoConcept(2, calcDate).Value = normWork;
                            if (dailyWork)
                                this.DoConcept(4, calcDate).Value = 1;
                        }
                        //اگر شخص حضور و اضافه کار داشت نباید اعمال گردد
                        if (this.DoConcept(13, calcDate).Value == 0)
                        {
                            this.ReCalculate(13, calcDate);
                        }
                        if (this.Person.GetShiftByDate(calcDate).Value > 0)
                            this.DoConcept(6, calcDate).Value
                                = this.Person.GetShiftByDate(calcDate).Value;
                    }
                    //محاسبه دوباره کارکرد ماهانه

                    for (int i = beforeDays - 1; i >= 0 && this.RuleCalculateDate >= this.MinAssgnRuleDate; i--)
                    {
                        if (!this.CalcDateZone.IsContain(this.RuleCalculateDate))
                            continue;
                        this.ReCalculate(5, this.RuleCalculateDate);
                    }
                }

               
            }
            if (this.DoConcept(1091).Value > 0)
            {
                this.DoConcept(4).Value = 0;
                // this.DoConcept(2).Value = this.Person.GetShiftByDate(this.RuleCalculateDate).Value;
                this.DoConcept(13).Value = this.DoConcept(2).Value + this.DoConcept(4002).Value;
            }
            GetLog(MyRule, DebugRuleState.After, 2, 4, 13, 6, 5, 3004);
        }



        /// <summary>
        /// کارکرد در صورت تردد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R2501(AssignedRule MyRule)
        {
            //4002 اضافه کار
            //4003 اضافه کار غیر مجاز
            //4023 زمان ناهار
            ProceedTraffic proceedTraffic = this.Person.GetProceedTraficAllByDate(this.RuleCalculateDate);
            if (proceedTraffic != null && proceedTraffic.Pairs != null && proceedTraffic.Pairs.Count > 0 )
            {
                GetLog(MyRule, DebugRuleState.Before, 4, 3028, 3004);
                this.DoConcept(4).Value = 1;
                ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();
                this.DoConcept(3004).Value = 0;
                GetLog(MyRule, DebugRuleState.After , 4, 3028, 3004);
            }
        }

        /// <summary>
        /// تلورانس قبل شیفت جزو موظفی لحاظ گردد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R2502(AssignedRule MyRule)
        {
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0)
            {
                int beforeOrderWork = MyRule["first", this.RuleCalculateDate].ToInt();
                string shiftCode = MyRule["second", this.RuleCalculateDate].ToString();
                if (beforeOrderWork > 0 && this.Person.GetShiftByDate(this.RuleCalculateDate).CustomCode == shiftCode)
                {
                    GetLog(MyRule, DebugRuleState.Before, 2, 13, 4002, 4003, 3028);
                    int shiftStart = this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).First().From;
                    PairableScndCnpValuePair pair = new PairableScndCnpValuePair(shiftStart - beforeOrderWork, shiftStart);
                    this.DoConcept(6).Value += beforeOrderWork;

                    if (this.DoConcept(1).Value > 0)
                    {
                        ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), pair));
                        ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(Operation.Differance(this.DoConcept(4003), pair));
                        ((PairableScndCnpValue)this.DoConcept(2)).AddPairs(Operation.Differance(this.DoConcept(2), pair));
                        PairableScndCnpValue absent = Operation.Differance(pair, this.DoConcept(1));
                        if (absent.Value > 0)
                        {
                            ((PairableScndCnpValue)this.DoConcept(3028)).AppendPairs(absent);
                        }
                        else
                        {
                            ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);
                            this.DoConcept(13).Value += pair.Value;
                        }
                    }
                    GetLog(MyRule, DebugRuleState.After , 2, 13, 4002, 4003, 3028);

                }
            }
        }

        //public override void R2013(AssignedRule MyRule)
        //{
        //    //23 مفهوم تعطیلات جزو کارکرد حساب شود
        //    //24 مفهوم تعطیلات رسمی جزو کارکرد حساب شود 
        //    //25 مفهوم روزهای غیر کاری جزو کارکرد حساب شود   
        //    //4 کارکرد خالص روزانه
        //    //6 مفهوم کارکردلازم
        //    //3004 غيبت روزانه
        //    //7 کارکرددرروز    
        //    //MyRule.Memory = 123;
        //    bool hourlyWork = true, dailyWork = true;
        //    if (MyRule.HasParameter("HourlyWork", this.RuleCalculateDate))
        //    {
        //        hourlyWork = MyRule["HourlyWork", this.RuleCalculateDate].ToInt() > 0;
        //    }
        //    if (MyRule.HasParameter("DailyWork", this.RuleCalculateDate))
        //    {
        //        dailyWork = MyRule["DailyWork", this.RuleCalculateDate].ToInt() > 0;
        //    }

        //    this.DoConcept(23).Value = MyRule["TatilGheirRasmi", this.RuleCalculateDate].ToInt();
        //    this.DoConcept(24).Value = MyRule["TatilRasmi", this.RuleCalculateDate].ToInt();
        //    this.DoConcept(25).Value = MyRule["GheirKari", this.RuleCalculateDate].ToInt();
        //    if ((this.DoConcept(6).Value > 0
        //        && (EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1", "2")&& this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
        //        || this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0))
                
        //    {

        //        GetLog(MyRule, " Before Execute State:", 2, 4, 13, 6, 3004);

        //        bool x1 = this.DoConcept(23).Value > 0 ? true : false;
        //        bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0;

        //        bool y1 = this.DoConcept(24).Value > 0 ? true : false;
        //        bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0;

        //        bool z1 = this.DoConcept(25).Value > 0 ? true : false;
        //        bool z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0 ? true : false;

        //        int normWork = this.DoConcept(7).Value;
        //        int beforeDays = 0;
        //        int bearkCounter = 10;
        //        IList<DateTime> dateList = new List<DateTime>();
        //        while (x2 | y2 | z2)
        //        {

        //            bearkCounter--;
        //            if (bearkCounter == 0) { break; }

        //            beforeDays++;

        //            if (this.Person.EmploymentDate > this.RuleCalculateDate.AddDays(-beforeDays))
        //            {
        //                break;
        //            }

        //            x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0;
        //            y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0;
        //            z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0 ? true : false;

        //            if ((x1 & x2) | (y1 & y2) | (z1 & z2))
        //            {
        //                dateList.Add(this.RuleCalculateDate.AddDays(-beforeDays));
        //            }
        //        }
        //        if (dateList.Count > 0)
        //        {
        //            bool work = false, absent = false;

        //            //روز قبل یا روز بعد تعطیلات
        //            work = this.DoConcept(4, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0
        //                &&
        //                this.DoConcept(4).Value > 0;

        //            absent = this.DoConcept(3004, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0
        //                &&
        //                this.DoConcept(3004).Value > 0;


        //            foreach (DateTime calcDate in dateList)
        //            {
        //                if (calcDate < this.MinAssgnRuleDate)
        //                {
        //                    break;
        //                }
        //                //تعطیل شیفت دار نباید حساب شود
        //                if (this.Person.GetShiftByDate(calcDate).Value > 0)
        //                    continue;

        //                if (work)
        //                {
        //                    if (hourlyWork)
        //                        this.DoConcept(2, calcDate).Value = normWork;
        //                    if (dailyWork)
        //                        this.DoConcept(4, calcDate).Value = 1;
        //                }
        //                else if (absent)
        //                {
        //                    if (hourlyWork)
        //                        this.DoConcept(2, calcDate).Value = 0;
        //                    if (dailyWork)
        //                        this.DoConcept(4, calcDate).Value = 0;
        //                }
        //                else//این روز احتمالا بدلیل نداشتن قانون محاسبه نشده است
        //                {
        //                    //Permit permit = this.Person.GetPermitByDate(calcDate, EngEnvironment.GetPrecard(Precards.DailyNoSallaryLeave1));
        //                    //if (permit == null || permit.Value == 0)
        //                    //{
        //                    //    if (hourlyWork)
        //                    //        this.DoConcept(2, calcDate).Value = normWork;
        //                    //    if (dailyWork)
        //                    //        this.DoConcept(4, calcDate).Value = 1;
        //                    //}
        //                }
        //                //اگر شخص حضور و اضافه کار داشت نباید اعمال گردد
        //                if (this.DoConcept(13, calcDate).Value == 0)
        //                {
        //                    this.ReCalculate(13, calcDate);
        //                }
        //                if (this.Person.GetShiftByDate(calcDate).Value > 0)
        //                    this.DoConcept(6, calcDate).Value
        //                        = this.Person.GetShiftByDate(calcDate).Value;
        //            }
        //            //محاسبه دوباره کارکرد ماهانه

        //            for (int i = beforeDays - 1; i >= 0 && this.RuleCalculateDate.AddDays(-i) >= this.MinAssgnRuleDate; i--)
        //            {
        //                if (!this.CalcDateZone.IsContain(this.RuleCalculateDate.AddDays(-i)))
        //                    continue;
        //                this.ReCalculate(5, this.RuleCalculateDate.AddDays(-i));
        //            }
        //        }
                
        //        GetLog(MyRule, " After Execute State:", 2, 4, 13, 6, 3004);
        //    }
          
        //}

        
        #endregion

        #region قوانين مرخصي
        /// <summary>قانون مرخصي 14-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي هشتاد-80 درنظر گرفته شده است</remarks>
        public override  void R3008(AssignedRule MyRule)
        {
            //13 کارکردناخالص
            //1005 مفهوم مرخصي استحقاقي روزانه
            //1003 مفهوم مرخصي استحقاقي ساعتي
            //110 مفهوم مرخصي بايد به کارکرد خالص اضافه شود
            //1001 مرخصي درروز

            //مرخصی ساعتی با حقوق-دادگاه 1025
            //مرخصی ساعتی با حقوق 1027

            //مرخصی با حقوق روزانه_فوت بستگان 1029
            //مرخصی با حقوق روزانه_جبرانی ماموریت 1031
            //مرخصی با حقوق روزانه_زایمان 1033
            //مرخصی با حقوق روزانه 1035
            //مرخصی با حقوق روزانه-دادگاه ورزشی 1037
            //1010  مرخصی استعلاجی روزانه
            //1090 مفهوم مجموع انواع مرخصی روزانه
            //1008مرخصی استعلاجی ساعتی
            GetLog(MyRule, DebugRuleState.Before, 13, 3028, 1090, 1095, 1005, 1003, 4);
            if (!EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2", "4") ||
               this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.Count > 0)
            {
                if (this.DoConcept(1090).Value >= 1)
                {
                    this.DoConcept(13).Value += this.DoConcept(1090).Value * this.DoConcept(7).Value;
                }

                //به دلیل اینکه در اضافه کار مرخصی های خارج از شیفت
                //لحاظ شده است و در تعریف کارکردناخالص به آن اضافه گشته
                //در این جا اشتراک مرخصی و شیفت را به کارکردناخالص اضافه میکنیم

                int value = 0;
                value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1003)).Value;
                value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1008)).Value;
                value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1038)).Value;
                value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1025)).Value;
                value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1027)).Value;
                value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1008)).Value;

                this.DoConcept(13).Value += value;

            }
            else //مرخصی به کارکرد خالص اضافه شده و حالا کافی است کارکرد ناخالص دوباره محاسبه شود
            {
                this.DoConcept(13).Value = this.DoConcept(2).Value + this.DoConcept(4002).Value;
            }
            if (this.DoConcept(1090).Value >= 1 && this.DoConcept(4).Value == 0)
            {
                this.DoConcept(4).Value = 1;//مانا آرد
            }
            //طبق نامه داده شده مرخصی ساعتی بی حقوق 12 کارکرد میگیرد
            if (this.DoConcept(1056).Value > 0 || this.DoConcept(1100).Value > 0 || this.DoConcept(1003).Value > 0)
            {
                this.DoConcept(4).Value = 1;
            }
            if (this.DoConcept(1054).Value > 0)
            {
                this.DoConcept(4).Value = 0;
            }

           
              //این کد جهت محاسبه مرخصی بیش از 5 ساعت است چون قانون 3011 حذف شده از قوانین و در حال حاضر uiVal.. در ورژن1.4.5 موجود نیست که کنترل کند
            if (this.DoConcept(1003).Value > 299)
            {
                this.DoConcept(1005).Value = 1;

                ((PairableScndCnpValue)this.DoConcept(1003)).ClearPairs();
                this.DoConcept(1003).Value = 0;
                this.Person.AddUsedLeave(this.RuleCalculateDate, -this.DoConcept(1003).Value, null);
                this.Person.AddUsedLeave(this.RuleCalculateDate, this.DoConcept(1005).Value * this.DoConcept(6).Value, null);
                this.ReCalculate(1090, 1095);
                this.DoConcept(3028).Value = 0;
                ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();

            }

           
            GetLog(MyRule, DebugRuleState.After , 13,3028,1090,1095,1005,1003,4);

                     
        }

        /// <summary>قانون مرخصي 28-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي نود و پنج-3024 درنظر گرفته شده است</remarks>
        public override  void R3011(AssignedRule MyRule)
        {
            //7 مفهوم کارکرددرروز
            //1003 مفهوم مرخصي استحقاقي ساعتي
            //1005 مفهوم مرخصي استحقاقي روزانه
            //308 جمع مرخصي استحقاقي ساعتي
            //6 کارکرد لازم
            this.DoConcept(1109);
            // this.DoConcept(1058);
            #region استحقاقی
            // در کد قبلی (کامنت شده) مقدار مرجع برای مقایسه پارامتر دوم بوده
            // در صورتیکه باید مرخصی در روز C20 در نظر گرفته میشده
            GetLog(MyRule, DebugRuleState.Before, 1003, 1005);
            // bool mustPeresentEndOfShift;
            // mustPeresentEndOfShift = MyRule["Third", this.RuleCalculateDate].ToInt() > 0;


            if (MyRule.HasParameter("Third", this.RuleCalculateDate) && this.DoConcept(6).Value > 0 && this.DoConcept(1005).Value > 0 && this.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday )
            {

                if (MyRule["Third", this.RuleCalculateDate].ToInt() > 0)
                {
                   this.Person.AddUsedLeave(this.RuleCalculateDate, -(this.DoConcept(1005).Value * this.DoConcept(1001).Value), null);
                   this.DoConcept(1005).Value = 0;
                   this.DoConcept(1003).Value = this.DoConcept(6).Value;
                   this.Person.AddUsedLeave(this.RuleCalculateDate, this.DoConcept(1003).Value, null);
                   
                }
            }

             if (this.DoConcept(6).Value > 0 && this.DoConcept(1003).Value > 0)
            {
                this.DoConcept(1003).Value = (this.DoConcept(1005).Value * this.DoConcept(6).Value) + this.DoConcept(1003).Value;
                this.DoConcept(1005).Value = 0;

                if (
                    this.DoConcept(1003).Value >=
                        MyRule["First", this.RuleCalculateDate].ToInt() &&
                    this.DoConcept(1003).Value <=
                        MyRule["Second", this.RuleCalculateDate].ToInt()
                    )
                {
                    this.DoConcept(1005).Value = 1;

                    if (this.DoConcept(1003).Value >= this.DoConcept(1001).Value)
                        this.DoConcept(1003).Value = this.DoConcept(1003).Value % this.DoConcept(1001).Value;
                    else ((PairableScndCnpValue)this.DoConcept(1003)).ClearPairs();
                    // this.DoConcept(1003).Value = 0;

                    this.ReCalculate(1090, 1095);
                }

                else
                {
                    this.DoConcept(1005).Value = this.DoConcept(1003).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1003).Value = this.DoConcept(1003).Value % this.DoConcept(1001).Value;
                }


            }


            GetLog(MyRule, DebugRuleState.After, 1003, 1005);
            //if (this.DoConcept(4008).Value > 0)
            //{
            //    this.DoConcept(4008).Value = 0;
            //}
            #endregion

            #region استعلاجی
            if (this.DoConcept(1008).Value >= 1)
            {
                GetLog(MyRule, DebugRuleState.Before, 1010, 1008);
                if (this.DoConcept(6).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    if (this.DoConcept(6).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
                    {
                        this.DoConcept(1010).Value = 1;
                    }
                    else
                    {
                        this.DoConcept(1010).Value = 1;
                        this.DoConcept(1008).Value = this.DoConcept(6).Value - MyRule["Second", this.RuleCalculateDate].ToInt();
                    }
                }
                else
                {
                    this.DoConcept(1008).Value = this.DoConcept(6).Value;
                }

                GetLog(MyRule, DebugRuleState.After, 1010, 1008);
            }
            #endregion

            #region با حقوق 43
            if (this.DoConcept(1031).Value >= 1)
            {
                GetLog(MyRule, DebugRuleState.Before, 1031, 1038);
                if (this.DoConcept(6).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    if (this.DoConcept(6).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
                    {
                        this.DoConcept(1031).Value = 1;
                    }
                    else
                    {
                        this.DoConcept(1031).Value = 1;
                        this.DoConcept(1038).Value = this.DoConcept(6).Value - MyRule["Second", this.RuleCalculateDate].ToInt();
                    }
                }
                else
                {
                    this.DoConcept(1038).Value = this.DoConcept(6).Value;
                }

                GetLog(MyRule, DebugRuleState.After, 1031, 1038);
            }
            #endregion

            #region با حقوق 44
            if (this.DoConcept(1029).Value >= 1)
            {
                GetLog(MyRule, DebugRuleState.Before, 1029, 1039);
                if (this.DoConcept(6).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    if (this.DoConcept(6).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
                    {
                        this.DoConcept(1029).Value = 1;
                    }
                    else
                    {
                        this.DoConcept(1029).Value = 1;
                        this.DoConcept(1039).Value = this.DoConcept(6).Value - MyRule["Second", this.RuleCalculateDate].ToInt();
                    }
                }
                else
                {
                    this.DoConcept(1039).Value = this.DoConcept(6).Value;
                }

                GetLog(MyRule, DebugRuleState.After, 1029, 1039);
            }
            #endregion

            #region با حقوق 45
            if (this.DoConcept(1037).Value >= 1)
            {
                GetLog(MyRule, DebugRuleState.Before, 1040, 1037);
                if (this.DoConcept(6).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    if (this.DoConcept(6).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
                    {
                        this.DoConcept(1037).Value = 1;
                    }
                    else
                    {
                        this.DoConcept(1037).Value = 1;
                        this.DoConcept(1040).Value = this.DoConcept(6).Value - MyRule["Second", this.RuleCalculateDate].ToInt();
                    }
                }
                else
                {
                    this.DoConcept(1040).Value = this.DoConcept(6).Value;
                }

                GetLog(MyRule, DebugRuleState.After, 1040, 1037);
            }
            #endregion

            #region با حقوق 46
            if (this.DoConcept(1033).Value >= 1)
            {
                GetLog(MyRule, DebugRuleState.Before, 1041, 1033);
                if (this.DoConcept(6).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    if (this.DoConcept(6).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
                    {
                        this.DoConcept(1033).Value = 1;
                    }
                    else
                    {
                        this.DoConcept(1033).Value = 1;
                        this.DoConcept(1041).Value = this.DoConcept(6).Value - MyRule["Second", this.RuleCalculateDate].ToInt();
                    }
                }
                else
                {
                    this.DoConcept(1041).Value = this.DoConcept(6).Value;
                }

                GetLog(MyRule, DebugRuleState.After, 1041, 1033);
            }
            #endregion

            #region با حقوق 47
            if (this.DoConcept(1035).Value >= 1)
            {
                GetLog(MyRule, DebugRuleState.Before, 1035, 1042);
                if (this.DoConcept(6).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
                {
                    if (this.DoConcept(6).Value <= MyRule["Second", this.RuleCalculateDate].ToInt())
                    {
                        this.DoConcept(1035).Value = 1;
                    }
                    else
                    {
                        this.DoConcept(1035).Value = 1;
                        this.DoConcept(1042).Value = this.DoConcept(6).Value - MyRule["Second", this.RuleCalculateDate].ToInt();
                    }
                }
                else
                {
                    this.DoConcept(1042).Value = this.DoConcept(6).Value;
                }

                GetLog(MyRule, DebugRuleState.After, 1035, 1042);
            }
            #endregion

            #region بی حقوق 31
            GetLog(MyRule, DebugRuleState.Before, 1054, 1064);

            if (MyRule.HasParameter("Third", this.RuleCalculateDate) && (this.DoConcept(6).Value > 0 && this.DoConcept(1064).Value > 0))
            {

                if (MyRule["Third", this.RuleCalculateDate].ToInt() > 0)
                {
                    this.DoConcept(1064).Value = 0;
                    this.DoConcept(1054).Value = this.DoConcept(6).Value;
                    this.DoConcept(3028).Value = 0;



                }
            }

            if (this.DoConcept(1054).Value > 0)
            {
                this.DoConcept(1054).Value = (this.DoConcept(1064).Value * this.DoConcept(6).Value) + this.DoConcept(1054).Value;
                this.DoConcept(1064).Value = 0;

                if (
                    this.DoConcept(1054).Value >=
                        MyRule["First", this.RuleCalculateDate].ToInt() &&
                    this.DoConcept(1054).Value <=
                        MyRule["Second", this.RuleCalculateDate].ToInt()
                    )
                {
                    this.DoConcept(1064).Value = 1;

                    if (this.DoConcept(1054).Value >= this.DoConcept(1001).Value)
                        this.DoConcept(1054).Value = this.DoConcept(1054).Value % this.DoConcept(1001).Value;
                    else this.DoConcept(1054).Value = 0;
                }
                else
                {
                    this.DoConcept(1064).Value = this.DoConcept(1054).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1054).Value = this.DoConcept(1054).Value % this.DoConcept(1001).Value;
                }
            }
            GetLog(MyRule, DebugRuleState.After, 1054, 1064);

            #endregion

            #region بی حقوق 32
            // در کد قبلی (کامنت شده) مقدار مرجع برای مقایسه پارامتر دوم بوده
            // در صورتیکه باید مرخصی در روز C20 در نظر گرفته میشده
            GetLog(MyRule, DebugRuleState.Before, 1056, 1066);
            if (MyRule.HasParameter("Third", this.RuleCalculateDate) && (this.DoConcept(6).Value > 0 && this.DoConcept(1066).Value > 0))
            {

                if (MyRule["Third", this.RuleCalculateDate].ToInt() > 0)
                {
                    this.DoConcept(1066).Value = 0;
                    this.DoConcept(1056).Value = this.DoConcept(6).Value;
                    this.DoConcept(3028).Value = 0;

                }
            }

            if (this.DoConcept(1056).Value > 0)
            {
                this.DoConcept(1056).Value = (this.DoConcept(1066).Value * this.DoConcept(6).Value) + this.DoConcept(1056).Value;
                this.DoConcept(1066).Value = 0;

                if (
                    this.DoConcept(1056).Value >=
                        MyRule["First", this.RuleCalculateDate].ToInt() &&
                    this.DoConcept(1056).Value <=
                        MyRule["Second", this.RuleCalculateDate].ToInt()
                    )
                {
                    this.DoConcept(1066).Value = 1;

                    if (this.DoConcept(1056).Value >= this.DoConcept(1001).Value)
                        this.DoConcept(1056).Value = this.DoConcept(1056).Value % this.DoConcept(1001).Value;
                    else this.DoConcept(1056).Value = 0;
                }
                else
                {
                    this.DoConcept(1066).Value = this.DoConcept(1056).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1056).Value = this.DoConcept(1056).Value % this.DoConcept(1001).Value;
                }
            }
            GetLog(MyRule, DebugRuleState.After, 1056, 1066);
            #endregion

            #region بی حقوق 33
            GetLog(MyRule, DebugRuleState.Before, 1058, 1068);
            if (MyRule.HasParameter("Third", this.RuleCalculateDate) && (this.DoConcept(6).Value > 0 && this.DoConcept(1068).Value > 0))
            {

                if (MyRule["Third", this.RuleCalculateDate].ToInt() > 0)
                {
                    this.DoConcept(1068).Value = 0;
                    this.DoConcept(1058).Value = this.DoConcept(6).Value;
                    this.DoConcept(3028).Value = 0;

                }
            }

            if (this.DoConcept(1058).Value > 0)
            {
                this.DoConcept(1058).Value = (this.DoConcept(1068).Value * this.DoConcept(6).Value) + this.DoConcept(1058).Value;
                this.DoConcept(1068).Value = 0;

                if (
                    this.DoConcept(1058).Value >=
                        MyRule["First", this.RuleCalculateDate].ToInt() &&
                    this.DoConcept(1058).Value <=
                        MyRule["Second", this.RuleCalculateDate].ToInt()
                    )
                {
                    this.DoConcept(1068).Value = 1;

                    if (this.DoConcept(1058).Value >= this.DoConcept(1001).Value)
                        this.DoConcept(1058).Value = this.DoConcept(1058).Value % this.DoConcept(1001).Value;
                    else this.DoConcept(1058).Value = 0;
                }
                else
                {
                    this.DoConcept(1068).Value = this.DoConcept(1058).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1058).Value = this.DoConcept(1058).Value % this.DoConcept(1001).Value;
                }
            }
            GetLog(MyRule, DebugRuleState.After, 1058, 1068);
            #endregion

            #region بی حقوق 34
            GetLog(MyRule, DebugRuleState.Before, 1060, 1070);
            if (MyRule.HasParameter("Third", this.RuleCalculateDate) && (this.DoConcept(6).Value > 0 && this.DoConcept(1070).Value > 0))
            {

                if (MyRule["Third", this.RuleCalculateDate].ToInt() > 0)
                {
                    this.DoConcept(1070).Value = 0;
                    this.DoConcept(1060).Value = this.DoConcept(6).Value;
                    this.DoConcept(3028).Value = 0;

                }
            }

            if (this.DoConcept(1060).Value > 0)
            {
                this.DoConcept(1060).Value = (this.DoConcept(1070).Value * this.DoConcept(6).Value) + this.DoConcept(1060).Value;
                this.DoConcept(1070).Value = 0;

                if (
                    this.DoConcept(1060).Value >=
                        MyRule["First", this.RuleCalculateDate].ToInt() &&
                    this.DoConcept(1060).Value <=
                        MyRule["Second", this.RuleCalculateDate].ToInt()
                    )
                {
                    this.DoConcept(1070).Value = 1;

                    if (this.DoConcept(1060).Value >= this.DoConcept(1001).Value)
                        this.DoConcept(1060).Value = this.DoConcept(1060).Value % this.DoConcept(1001).Value;
                    else this.DoConcept(1060).Value = 0;
                }
                else
                {
                    this.DoConcept(1070).Value = this.DoConcept(1060).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1060).Value = this.DoConcept(1060).Value % this.DoConcept(1001).Value;
                }
            }
            GetLog(MyRule, DebugRuleState.After, 1060, 1070);
            #endregion

            #region بی حقوق 35
            GetLog(MyRule, DebugRuleState.Before, 1062, 1072);
            if (MyRule.HasParameter("Third", this.RuleCalculateDate) && (this.DoConcept(6).Value > 0 && this.DoConcept(1072).Value > 0))
            {

                if (MyRule["Third", this.RuleCalculateDate].ToInt() > 0)
                {
                    this.DoConcept(1072).Value = 0;
                    this.DoConcept(1062).Value = this.DoConcept(6).Value;
                    this.DoConcept(3028).Value = 0;
                }
            }

            if (this.DoConcept(1062).Value > 0)
            {
                this.DoConcept(1062).Value = (this.DoConcept(1072).Value * this.DoConcept(6).Value) + this.DoConcept(1062).Value;
                this.DoConcept(1072).Value = 0;

                if (
                    this.DoConcept(1062).Value >=
                        MyRule["First", this.RuleCalculateDate].ToInt() &&
                    this.DoConcept(1062).Value <=
                        MyRule["Second", this.RuleCalculateDate].ToInt()
                    )
                {
                    this.DoConcept(1072).Value = 1;

                    if (this.DoConcept(1062).Value >= this.DoConcept(1001).Value)
                        this.DoConcept(1062).Value = this.DoConcept(1062).Value % this.DoConcept(1001).Value;
                    else this.DoConcept(1062).Value = 0;
                }
                else
                {
                    this.DoConcept(1072).Value = this.DoConcept(1062).Value / this.DoConcept(1001).Value;
                    this.DoConcept(1062).Value = this.DoConcept(1062).Value % this.DoConcept(1001).Value;
                }
            }

            this.ReCalculate(1109, 1110);
            GetLog(MyRule, DebugRuleState.After, 1062, 1072);
            #endregion


        }

        /// <summary>
        /// اعمال مرخصی استحقاقی در روزها تعطیل بین مرخصی استحقاقی
        /// </summary>
        /// <param name="MyRule"></param>
        public override  void R3023(AssignedRule MyRule)
        {
            //1005 مفهوم مرخصي استحقاقي روزانه
            //114 مفهوم اعمال تعطيلات نوروز در مرخصي 
            //324 تعطیلات رسمی بین مرخصی,مرخصی محسوب شود
            //325 تعطیلات غیر رسمی بین مرخصی,مرخصی محسوب شود
            //326 روزهای غیر کاری بین مرخصی,مرخصی محسوب شود


            GetLog(MyRule, DebugRuleState.Before, 1003, 1005,1091);
            #region مرخصی استحقاقی
            //if (this.DoConcept(1005).Value > 0
            //    && this.DoConcept(1005, this.RuleCalculateDate.AddDays(-1)).Value == 0)
            //{
            //    bool x1 = Utility.ToBoolean(MyRule["Rasmi", this.RuleCalculateDate].ToInt());
            //    bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1") && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0;

            //    bool y1 = Utility.ToBoolean(MyRule["GheireRasmi", this.RuleCalculateDate].ToInt());
            //    bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2") && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0;

            //    bool z1 = Utility.ToBoolean(MyRule["GheireKari", this.RuleCalculateDate].ToInt());
            //    bool z2 = (this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0 ? true : false) & !x2 & !y2;

            //    int beforeDays = 0;
            //    int maxLoopcounter = 5;
            //    IList<DateTime> dateList = new List<DateTime>();
            //    while (x2 | y2 | z2)
            //    {
            //        beforeDays++;
            //        maxLoopcounter--;
            //        if (maxLoopcounter < 1)
            //        {
            //            break;
            //        }
            //        if (this.Person.EmploymentDate > this.RuleCalculateDate.AddDays(-beforeDays))
            //        {
            //            break;
            //        }

            //        x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "1") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0;

            //        y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "2") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0;

            //        z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0 ? true & !x2 & !y2 : false;

            //        if ((x1 & x2) | (y1 & y2) | (z1 & z2))
            //        {
            //            dateList.Add(this.RuleCalculateDate.AddDays(-beforeDays));
            //        }
            //    }
            //    if (this.DoConcept(1005, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
            //    {
            //        int leaveInDay = this.DoConcept(1001, this.RuleCalculateDate).Value;
            //        this.DoConcept(1003).Value = 0;

            //        foreach (DateTime calcDate in dateList)
            //        {
            //            this.DoConcept(1005, calcDate).Value = 1;
            //            this.Person.AddUsedLeave(calcDate, leaveInDay, null);
            //        }
            //    }
            //}
            GetLog(MyRule, DebugRuleState.After, 1003, 1005);

            #endregion

            #region مرخصی بی حقوق

            if (this.DoConcept(1091).Value > 0
               && this.DoConcept(1091, this.RuleCalculateDate.AddDays(-1)).Value == 0)
            {
                bool x1 = Utility.ToBoolean(MyRule["Rasmi", this.RuleCalculateDate].ToInt());
                bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1") && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0;

                bool y1 = Utility.ToBoolean(MyRule["GheireRasmi", this.RuleCalculateDate].ToInt());
                bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2") && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0;

                bool z1 = Utility.ToBoolean(MyRule["GheireKari", this.RuleCalculateDate].ToInt());
                bool z2 = (this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0 ? true : false) & !x2 & !y2;

                int beforeDays = 0;
                int maxLoopcounter = 5;
                IList<DateTime> dateList = new List<DateTime>();
                while (x2 | y2 | z2)
                {
                    beforeDays++;
                    maxLoopcounter--;
                    if (maxLoopcounter < 1)
                    {
                        break;
                    }
                    if (this.Person.EmploymentDate > this.RuleCalculateDate.AddDays(-beforeDays))
                    {
                        break;
                    }

                    x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "1") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0;

                    y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "2") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0;

                    z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0 ? true & !x2 & !y2 : false;

                    if ((x1 & x2) | (y1 & y2) | (z1 & z2))
                    {
                        dateList.Add(this.RuleCalculateDate.AddDays(-beforeDays));
                    }
                }
                if (this.DoConcept(1091, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                {
                   
                    foreach (DateTime calcDate in dateList)
                    {
                        this.DoConcept(1091, calcDate).Value = 1;
                        //this.Person.AddUsedLeave(calcDate, leaveInDay, null);
                        this.ReCalculate(1091, calcDate);
                    }
                }
            }
            #endregion  
            
            #region مرخصی باحقوق
            //if (this.DoConcept(1037).Value > 0
            //  && this.DoConcept(1037, this.RuleCalculateDate.AddDays(-1)).Value == 0)
            //{
            //    bool x1 = Utility.ToBoolean(MyRule["Rasmi", this.RuleCalculateDate].ToInt());
            //    bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1") && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0;

            //    bool y1 = Utility.ToBoolean(MyRule["GheireRasmi", this.RuleCalculateDate].ToInt());
            //    bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2") && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0;

            //    bool z1 = Utility.ToBoolean(MyRule["GheireKari", this.RuleCalculateDate].ToInt());
            //    bool z2 = (this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0 ? true : false) & !x2 & !y2;

            //    int beforeDays = 0;
            //    int maxLoopcounter = 5;
            //    IList<DateTime> dateList = new List<DateTime>();
            //    while (x2 | y2 | z2)
            //    {
            //        beforeDays++;
            //        maxLoopcounter--;
            //        if (maxLoopcounter < 1)
            //        {
            //            break;
            //        }
            //        if (this.Person.EmploymentDate > this.RuleCalculateDate.AddDays(-beforeDays))
            //        {
            //            break;
            //        }

            //        x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "1") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0;

            //        y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-beforeDays), "2") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0;

            //        z2 = this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-beforeDays)).Value == 0 ? true & !x2 & !y2 : false;

            //        if ((x1 & x2) | (y1 & y2) | (z1 & z2))
            //        {
            //            dateList.Add(this.RuleCalculateDate.AddDays(-beforeDays));
            //        }
            //    }
            //    if (this.DoConcept(1037, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
            //    {

            //        foreach (DateTime calcDate in dateList)
            //        {
            //            this.DoConcept(1037, calcDate).Value = 1;
            //            //this.Person.AddUsedLeave(calcDate, leaveInDay, null);
            //            this.ReCalculate(1037, calcDate);
            //        }
            //    }
            //}
            #endregion

         }


        #endregion

        #region قوانين ماموريت

        #endregion

        #region قوانين کم کاري
        /// <summary>قانون کم کاري 6-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي چهل و هشت-48 درنظر گرفته شده است</remarks>
        public override  void R5009(AssignedRule MyRule)
        {
            //غيبت ساعتي مجاز 3020
            //3028 غيبت ساعتي غيرمجاز
            //3004 غيبت روزانه

            //کارکردخالص ساعتي 2
            //4 کارکردخالص روزانه
            //13 کارکردناخالص ساعتی
            //اضافه کار ساعتي 4002

            //1003 مرخصي استحقاقي ساعتي 
            //1056 مرخصی بی حقوق ساعتی 12
            //2002 ماموريت خالص ساعتي


            GetLog(MyRule, DebugRuleState.Before, 2, 4, 3020, 3028, 4002, 3004, 1002, 2002);

            this.DoConcept(1002);
            this.DoConcept(1109);
            this.DoConcept(2002);
            //  شرط دوم زیر به این دلیل است که روزهایی که مرخصی بی حقوق31 دارند غیبت نیز حساب میشه
            if (this.DoConcept(3028).Value >= MyRule["First", this.RuleCalculateDate].ToInt() && this .DoConcept (1091).Value ==0)
            {
                if (this.DoConcept(3048).Value == 0)
                {
                    this.DoConcept(3004).Value = 1;
                }
                else
                {
                    this.DoConcept(3004).Value = ((int)((float)this.DoConcept(6).Value / (float)this.DoConcept(7).Value));
                }
                this.DoConcept(3020).Value = 0;
                //this.DoConcept(3028).Value = 0;
                ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();
                ((PairableScndCnpValue)this.DoConcept(3029)).ClearPairs();
                ((PairableScndCnpValue)this.DoConcept(3030)).ClearPairs();
                ((PairableScndCnpValue)this.DoConcept(3031)).ClearPairs();

                if (MyRule.HasParameter("Second", this.RuleCalculateDate))
                {
                    if (MyRule["Second", this.RuleCalculateDate].ToInt() == 1)
                    {
                        ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(((PairableScndCnpValue)this.DoConcept(2)).Pairs);
                    }
                }
                else
                {
                    ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(((PairableScndCnpValue)this.DoConcept(2)).Pairs);
                }
                this.DoConcept(4).Value = 0;
                this.DoConcept(2).Value = 0;
                this.DoConcept(13).Value = 0;
            }
            else if (this.DoConcept(1091).Value> 0)
            {
                this.DoConcept(3020).Value = 0;
                //this.DoConcept(3028).Value = 0;
                ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();
                ((PairableScndCnpValue)this.DoConcept(3029)).ClearPairs();
                ((PairableScndCnpValue)this.DoConcept(3030)).ClearPairs();
                ((PairableScndCnpValue)this.DoConcept(3031)).ClearPairs();
            }
            GetLog(MyRule, DebugRuleState.After, 2, 4, 3020, 3028, 4002, 3004, 1002, 2002);
        }

      

        #endregion

        #region قوانين اضافه کاري
        /// <summary>اعمال اضافه کار 24 ساعته</summary>
        /// <param name="Result"></param>
        /// <remarks></remarks>
        public virtual void R6501(AssignedRule MyRule)
        {

            //4002 اضافه کار
            //4003 اضافه کار غیر مجاز
            //4023 زمان ناهار
            ProceedTraffic proceedTraffic = this.Person.GetProceedTraficByDate(this.RuleCalculateDate);
            if (proceedTraffic != null && proceedTraffic.Pairs != null && proceedTraffic.Pairs.Count > 0 &&
                proceedTraffic.Pairs.Any(x => x.To > 1440))
            {
                GetLog(MyRule, DebugRuleState.Before, 4002,4003);
                if (this.DoConcept(4002).Value + this.DoConcept(4003).Value > 0)
                {                    
                    int parameter = 0;
                    int maxOverworkLen = 0;
                    if (this.DoConcept(6).Value > 0)
                    {
                        parameter = MyRule["First", this.RuleCalculateDate].ToInt();
                        PairableScndCnpValue notAllow = new PairableScndCnpValue();
                        //اضافه کار قبل وقت
                        foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(4003)).Pairs) 
                        {
                            if (pair.From > this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.First().From) 
                            {
                                ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(pair);
                            }
                            else
                            {
                                notAllow.AppendPair(pair);
                            }

                        }
                        ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(notAllow);                       

                        maxOverworkLen = (24 * 60) - this.Person.GetShiftByDate(this.RuleCalculateDate).PastedPairs.Value;
                    }
                    else
                    {
                        //  یک ساعت برای ساعت ناهار کسر گردیده است - قانون 112
                        parameter = MyRule["Second", this.RuleCalculateDate].ToInt();
                        ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(this.DoConcept(4003));
                        ((PairableScndCnpValue)this.DoConcept(4003)).ClearPairs();

                        ((PairableScndCnpValue)this.DoConcept(4003))
                                          .AppendPairs(Operation.Intersect(this.DoConcept(4002), this.DoConcept(4023)));
                        ((PairableScndCnpValue)this.DoConcept(4002))
                                          .AddPairs(Operation.Differance(this.DoConcept(4002), this.DoConcept(4023)));
                        maxOverworkLen = 23 * 60;
                    }                   
                    int overWork = this.DoConcept(4002).Value;
                    if (overWork > maxOverworkLen)
                        overWork = maxOverworkLen;
                    int newOverWork = (overWork * parameter) / maxOverworkLen;
                    int startOfOverwork = ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.First().From;
                    ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPair(new PairableScndCnpValuePair(startOfOverwork, startOfOverwork + newOverWork));
                }
                GetLog(MyRule, DebugRuleState.After , 4002, 4003);
            }
        }

        /// <summary>
        /// اضافه کار اجباری اعمال شود است
        /// </summary>
        /// <param name="MyRule"></param>
        public override  void R6031(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 3012, 4002, 3028, 3030, 1003, 1002, 4003, 4008, 13);

            this.DoConcept(1082);
            this.DoConcept(2023);
            this.DoConcept(1002);
            this.DoConcept(1109);

            int tajilMojaz = this.DoConcept(3012).Value;
            var obligatoryOvertimePairs =
                this.Person.GetShiftByDate(this.RuleCalculateDate, "1")
                .Pairs.Where(x =>
                    x.ShiftPairType != null &&
                    x.ShiftPairType.CustomCode.Equals("1")
                    );

            // شیفت اضافه کار اجباری وجود ندارد
            // با این فرض که غیبت روزانه نخورده است
            if (obligatoryOvertimePairs != null && obligatoryOvertimePairs.Any() && this.DoConcept(1).Value > 0
                && this.DoConcept(2005).Value == 0 && this.DoConcept(1095).Value == 0)
            {
                foreach (var obligatoryOvertimePair in obligatoryOvertimePairs.ToList())
                {
                    var absentOnMandatoryOvertime =
                        Operation.Differance(
                            obligatoryOvertimePair, this.DoConcept(1));
                    absentOnMandatoryOvertime =
                        Operation.Differance(
                            absentOnMandatoryOvertime, this.DoConcept(2023));

                    PairableScndCnpValue difToAppend;

                    if (((PairableScndCnpValue)this.DoConcept(3028)).Pairs.Any())
                    {
                        difToAppend = Operation.Union(this.DoConcept(3028), absentOnMandatoryOvertime);
                        ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(difToAppend);
                        if (absentOnMandatoryOvertime != null && absentOnMandatoryOvertime.Pairs != null && absentOnMandatoryOvertime.Pairs.Any())
                        {
                            if (absentOnMandatoryOvertime.Pairs.Last().To == obligatoryOvertimePair.To)
                            {
                                ((PairableScndCnpValue)this.DoConcept(3030)).AddPairs(difToAppend);
                            }
                        }
                    }
                    else
                    {
                        ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(absentOnMandatoryOvertime);
                        if (absentOnMandatoryOvertime != null && absentOnMandatoryOvertime.Pairs != null && absentOnMandatoryOvertime.Pairs.Any())
                        {
                            if (absentOnMandatoryOvertime.Pairs.Last().To == obligatoryOvertimePair.To)
                            {
                                ((PairableScndCnpValue)this.DoConcept(3030)).AddPairs(absentOnMandatoryOvertime);
                            }
                        }
                    }
                    ((PairableScndCnpValue)this.DoConcept(3001)).AddPairs(this.DoConcept(3028));
                    if (this.DoConcept(1003).Value > 0)
                    {
                        ((PairableScndCnpValue)this.DoConcept(3028)).AppendPairs(this.DoConcept(1003));
                        this.Person.AddUsedLeave(this.RuleCalculateDate, -1 * this.DoConcept(1003).Value, null);
                        if (this.Person.LeaveCalcResultList != null && this.Person.LeaveCalcResultList.Count > 0)
                        {
                            LeaveCalcResult CurrentLCR = this.Person.LeaveCalcResultList.Where(x => x.Date == this.RuleCalculateDate).LastOrDefault();
                            if (CurrentLCR != null)
                            {
                                CurrentLCR.MinuteUsed -= this.DoConcept(1003).Value;
                                if (CurrentLCR.MinuteUsed < 0)
                                {
                                    CurrentLCR.DayUsed--;
                                    CurrentLCR.MinuteUsed += CurrentLCR.LeaveMinuteInDay;
                                }
                                CurrentLCR.DoAdequate(CurrentLCR.LeaveMinuteInDay);
                            }
                        }
                    }
                    this.ReCalculate(1002, 1003);
                    var leaveIn = Operation.Intersect(obligatoryOvertimePair, this.DoConcept(1003));
                    // در زمانی که مرخصی در اضافه کار اجباری باشدباید اختلاف آنها حساب شود
                    ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(Operation.Differance(leaveIn, this.DoConcept(1003)));
                    //((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(leaveIn);
                }
            }

            GetLog(MyRule, DebugRuleState.After, 3012, 4002,3028,3030,1003,1002, 4003, 4008, 13);
        }


        #endregion

        #endregion
     
    }
}
