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

        public override void R1014(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4002);

            ((PairableScndCnpValue)this.DoConcept(4002))
                                            .AddPairs(Operation.Differance(this.DoConcept(4002),
                                                        new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt())));
            GetLog(MyRule, DebugRuleState.After, 4002);

        }

        public override void R1017(AssignedRule MyRule)
        {
            base.R1017(MyRule);

            this.DoConcept(1501);//مرخصی نیم روز
            this.DoConcept(1503); ;//آف دکتر
            this.DoConcept(2062);//ورزشی
            this.DoConcept(2064);//سرویس
            this.DoConcept(6502);
            this.DoConcept(6504);
            this.DoConcept(6506);
        }

        public override void R1016(AssignedRule MyRule)
        {
            base.R1016(MyRule);

            this.DoConcept(1502);//مرخصی نیم روز
            this.DoConcept(1504);//آف دکتر
            this.DoConcept(2021);//ورزشی
            this.DoConcept(2019);//سرویس
            this.DoConcept(6501);
            this.DoConcept(6503);
            this.DoConcept(6505);
        }

        #endregion

        #region قوانين کارکرد

        #endregion

        #region قوانين مرخصي

        /// اعمال مرخصی استحقاقی در روزها تعطیل بین مرخصی استحقاقی
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R3023(AssignedRule MyRule)
        {
            //1029 مفهوم مرخصي تشویقی
            //1035 مفهوم اعطام


            #region تشویقی
            GetLog(MyRule, DebugRuleState.Before, 4,1029);
            if (this.DoConcept(1029).Value > 0
                && this.DoConcept(1029, this.RuleCalculateDate.AddDays(-1)).Value == 0)
            {
                bool x1 = Utility.ToBoolean(MyRule["Rasmi", this.RuleCalculateDate].ToInt());
                bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0;

                bool y1 = Utility.ToBoolean(MyRule["GheireRasmi", this.RuleCalculateDate].ToInt());
                bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0;

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
                if (this.DoConcept(1029, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                {

                    foreach (DateTime calcDate in dateList)
                    {
                        this.DoConcept(1029, calcDate).Value = 1;
                        this.DoConcept(4, calcDate).Value = 1;
                    }
                }
            }

            GetLog(MyRule, DebugRuleState.After,4, 1029);
            
            #endregion

            #region اعزام
            GetLog(MyRule, DebugRuleState.Before,4, 1035);
            if (this.DoConcept(1035).Value > 0
                && this.DoConcept(1035, this.RuleCalculateDate.AddDays(-1)).Value == 0)
            {
                bool x1 = Utility.ToBoolean(MyRule["Rasmi", this.RuleCalculateDate].ToInt());
                bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0;

                bool y1 = Utility.ToBoolean(MyRule["GheireRasmi", this.RuleCalculateDate].ToInt());
                bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0;

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
                if (this.DoConcept(1035, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                {

                    foreach (DateTime calcDate in dateList)
                    {
                        this.DoConcept(1035, calcDate).Value = 1;
                        this.DoConcept(4, calcDate).Value = 1;
                    }
                }
            }

            GetLog(MyRule, DebugRuleState.After,4, 1035);

            #endregion

            #region نوزاد
            GetLog(MyRule, DebugRuleState.Before,4, 1037);
            if (this.DoConcept(1037).Value > 0
                && this.DoConcept(1037, this.RuleCalculateDate.AddDays(-1)).Value == 0)
            {
                bool x1 = Utility.ToBoolean(MyRule["Rasmi", this.RuleCalculateDate].ToInt());
                bool x2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "1") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0;

                bool y1 = Utility.ToBoolean(MyRule["GheireRasmi", this.RuleCalculateDate].ToInt());
                bool y2 = EngEnvironment.HasCalendar(this.RuleCalculateDate.AddDays(-1), "2") && this.Person.GetShiftByDate(this.RuleCalculateDate.AddDays(-1)).Value == 0;

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
                if (this.DoConcept(1037, this.RuleCalculateDate.AddDays(-beforeDays)).Value > 0)
                {

                    foreach (DateTime calcDate in dateList)
                    {
                        this.DoConcept(1037, calcDate).Value = 1;
                        this.DoConcept(4, calcDate).Value = 1;
                    }
                }
            }

            GetLog(MyRule, DebugRuleState.After,4, 1037);

            #endregion

        }
        #endregion

        #region قوانين ماموريت

        #endregion

        #region قوانين کم کاري

        /// <summary>
        /// تبدیل به غیبت روزانه
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R5009(AssignedRule MyRule)
        {
            this.DoConcept(1501);//نیم روز
            base.R5009(MyRule);
            return;
            //base.R5009(MyRule);

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

          
            GetLog(MyRule, DebugRuleState.Before, 2, 4, 3020, 3028,3029,3030,3031,13, 4002, 3004, 1002, 2002,1109);

            this.DoConcept(1002);
            this.DoConcept(1109);
            this.DoConcept(2002);
            if (this.DoConcept(3028).Value >= MyRule["First", this.RuleCalculateDate].ToInt())
            {
                if (this.DoConcept(3048).Value == 0)
                {
                    this.DoConcept(3004).Value = 1;

                    this.DoConcept(3020).Value = 0;
                    //this.DoConcept(3028).Value = 0;
                    ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();
                    ((PairableScndCnpValue)this.DoConcept(3029)).ClearPairs();
                    ((PairableScndCnpValue)this.DoConcept(3030)).ClearPairs();
                    ((PairableScndCnpValue)this.DoConcept(3031)).ClearPairs();
                }
                else
                {
                    this.DoConcept(3004).Value = ((int)((float)this.DoConcept(3028).Value / (float)this.DoConcept(7).Value));
                    int remainAbsence = ((int)((float)this.DoConcept(3028).Value % (float)this.DoConcept(7).Value));
                    int karkerd = this.DoConcept(6).Value - this.DoConcept(3028).Value;

                    this.DoConcept(3020).Value = 0;


                    ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();
                    ((PairableScndCnpValue)this.DoConcept(3029)).ClearPairs();
                    ((PairableScndCnpValue)this.DoConcept(3030)).ClearPairs();
                    ((PairableScndCnpValue)this.DoConcept(3031)).ClearPairs();
                    if (remainAbsence > 0)
                    {
                        ((PairableScndCnpValue)this.DoConcept(3028)).IncreaseValue(remainAbsence);
                    }
                    if (karkerd > 0)
                    {
                        this.DoConcept(4).Value = ((int)((float)karkerd / (float)this.DoConcept(7).Value));
                        this.DoConcept(2).Value = karkerd;
                        this.DoConcept(13).Value = karkerd;
                    }
                }

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
            }
            GetLog(MyRule, DebugRuleState.After, 2, 4, 3020, 3028,,3029,3030,3031,13, 4002, 3004, 1002, 2002,1109);
        }

        /// <summary>
        /// تبدیل غیبت ساعتی ماهانه به روزانه
        /// در صورتی که در خواست تبدیل غیبت به مرخصی ثبت بوشد , اعمال میگردد
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R5018(AssignedRule MyRule)
        {
            //3506 کسر کار تبدیل شده به روزانه ماهانه
            //3502 کسر کار تبدیل شده به مرخصی
            //پیشکارت 153 , مرخصی تبدیل کسر کار بیش از 8 ساعت - حتما باید روز آخر ماه ثبت گردد
            //1006 مرخصی روازانه ماهانه
            if (this.DoConcept(9).Value > 0)
            {
                GetLog(MyRule, DebugRuleState.Before, 3034,3502,3506, 1006, 5);
                int absence = this.DoConcept(3034).Value;
                int dailyAbsence = this.DoConcept(3005).Value;
                Permit permit = this.Person.GetPermitByDate(this.RuleCalculateDate, EngEnvironment.GetPrecard(153));
                if (permit != null && permit.Value > 0)
                {
                    if (this.DoConcept(3019).Value > 0 && this.DoConcept(3034).Value > 0)
                    {
                        int tmp = this.DoConcept(3034).Value / this.DoConcept(3019).Value;
                        int tmp2 = this.DoConcept(3034).Value % this.DoConcept(3019).Value;
                        this.Person.AddUsedLeave(this.ConceptCalculateDate, tmp * this.DoConcept(3019).Value, permit);
                        this.DoConcept(3502).Value += (tmp * this.DoConcept(3019).Value);
                        this.DoConcept(1006).Value += tmp;
                        this.DoConcept(5).Value -= tmp;
                        this.DoConcept(3034).Value = tmp2;
                    }
                }
                else
                {
                    base.R5018(MyRule);
                    if (dailyAbsence < this.DoConcept(3005).Value)
                    {
                        this.DoConcept(3506).Value = absence - this.DoConcept(3034).Value;
                    }
                }
                GetLog(MyRule, DebugRuleState.After, 3034, 3502,3506, 1006, 5);
            }

        }

        /// <summary>
        /// انتقال کسر کار برج قبل به برج جاری
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R5036(AssignedRule MyRule)
        {
            //3504 کسر کار برج قبل ماهانه
            if (this.DoConcept(9).Value > 0)
            {
                int absence = this.DoConcept(3034).Value;
                base.R5036(MyRule);
                GetLog(MyRule, DebugRuleState.Before, 3504);
                this.DoConcept(3504).Value = this.DoConcept(3034).Value - absence;
                GetLog(MyRule, DebugRuleState.After , 3504);
            }
        }

        /// <summary>
        /// تا .... کسر کار ماهانه به مرخصی تبدیل گردد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R5501(AssignedRule MyRule)
        {
            //3502 کسر کار تبدیل شده به مرخصی ماهانه
            //1011 مرخصی ماهانه ساعتی
            int monthlyAbsence = this.DoConcept(3034).Value;
            if (monthlyAbsence > 0 && monthlyAbsence <= MyRule["first", this.RuleCalculateDate].ToInt())
            {
                var conceptList = new List<int>();
                conceptList.AddRange(new[] { 3502, 3034, 3,1011 });
                GetLog(MyRule, DebugRuleState.Before, conceptList.Distinct().ToArray());

                int demandLeave = this.Person.GetRemainLeave(this.ConceptCalculateDate);
                if (demandLeave >= monthlyAbsence)
                {
                    this.DoConcept(3502).Value += this.DoConcept(3034).Value;
                    this.Person.AddUsedLeave(this.RuleCalculateDate, monthlyAbsence, null);
                    this.DoConcept(1011).Value += monthlyAbsence;
                    this.DoConcept(3).Value += monthlyAbsence;
                    this.DoConcept(3034).Value = 1;
                }
                GetLog(MyRule, DebugRuleState.After, conceptList.Distinct().ToArray());

            }
        }

        #endregion

        #region قوانين اضافه کاري

        /// <summary>
        /// حداقل اضافه کار قبل وقت
        /// مجوز نتوان گرفت
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R6004(AssignedRule MyRule)
        {
            //اضافه کار خالص ساعتي 56
            //4002 اضافه کار ساعتي مجاز
            //4003 اضافه کار ساعتي غيرمجاز
            //4008 اضافه کارساعتي قبل ازوقت

            GetLog(MyRule, DebugRuleState.Before, 4002, 4003, 4008, 13);
            if (this.DoConcept(4008).Value < MyRule["First", this.RuleCalculateDate].ToInt())
            {
                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), this.DoConcept(4008)));
                ((PairableScndCnpValue)this.DoConcept(4008)).ClearPairs();
                this.ReCalculate(13);
            }
            GetLog(MyRule, DebugRuleState.After, 4002, 4003, 4008, 13);
            
        }

        /// <summary>
        /// حداقل اضافه کار بعد وقت
        /// مجوز نتوان گرفت
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R6005(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4002, 4003, 4007, 13);

            //اضافه کار خالص ساعتي 56
            //4002 اضافه کار ساعتي مجاز
            //4003 اضافه کار ساعتي غيرمجاز
            //4007 اضافه کارساعتي بعد ازوقت

            if (this.DoConcept(4007).Value < MyRule["First", this.RuleCalculateDate].ToInt())
            {
                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), this.DoConcept(4007)));
                ((PairableScndCnpValue)this.DoConcept(4007)).ClearPairs();
                this.ReCalculate(13);
            }
            GetLog(MyRule, DebugRuleState.After, 4002, 4003, 4007, 13);
        }

        /// <summary>
        /// اعمال اضافه کار پنجشنبه با کد 151
        /// درخواست اضافه کار عادی منجر به مجاز شدن میشود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6501(AssignedRule MyRule)
        {
            var conceptList = new List<int>();
            conceptList.AddRange(new[] { 4002, 4003, 13,6501 });
            GetLog(MyRule, DebugRuleState.Before, conceptList.Distinct().ToArray());

            if (this.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday)
            {
                Permit permit = this.Person.GetPermitByDate(this.RuleCalculateDate, EngEnvironment.GetPrecard(151));
                if (permit==null || permit.Value==0)
                {
                    ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(Operation.Differance( this.DoConcept(4002),this.DoConcept(2023)));
                    ((PairableScndCnpValue)this.DoConcept(6501)).AddPairs(Operation.Differance(this.DoConcept(4002), this.DoConcept(2023)));
                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Intersect(this.DoConcept(4002), this.DoConcept(2023)));
                    this.DoConcept(13).Value = this.DoConcept(4002).Value;
                    ((PairableScndCnpValue)this.DoConcept(6501)).AddPairs(this.DoConcept(4002));
                }
                else
                {
                    ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(this.DoConcept(4003));
                    ((PairableScndCnpValue)this.DoConcept(4003)).ClearPairs();
                    this.DoConcept(13).Value = this.DoConcept(4002).Value;
                    ((PairableScndCnpValue)this.DoConcept(6501)).AddPairs(this.DoConcept(4002));
                }
            }
            GetLog(MyRule, DebugRuleState.After, conceptList.Distinct().ToArray());
        }

        /// <summary>
        /// اعمال اضافه کار شبکاری با کد 152
        /// درخواست اضافه کار عادی منجر به مجاز شدن نمیشود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6502(AssignedRule MyRule)
        {
            var conceptList = new List<int>();
            conceptList.AddRange(new[] { 4002, 4003, 13,6503 });
            GetLog(MyRule, DebugRuleState.Before, conceptList.Distinct().ToArray());
            PairableScndCnpValuePair pair = new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt());

            Permit permit = this.Person.GetPermitByDate(this.RuleCalculateDate, EngEnvironment.GetPrecard(152));
            if (permit != null && permit.Value > 0
                && Operation.Intersect(this.DoConcept(4003), pair).Value > 0)
            {
                this.DoConcept(13).Value += Operation.Intersect(this.DoConcept(4003), pair).Value;

                ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(Operation.Intersect(this.DoConcept(4003), pair));
                ((PairableScndCnpValue)this.DoConcept(6503)).AppendPairs(Operation.Intersect(this.DoConcept(4003), pair));
                ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(Operation.Differance(this.DoConcept(4003), pair));

            }
           /* else if ((permit != null || permit.Value > 0)
                && Operation.Intersect(this.DoConcept(4002), pair).Value > 0)
            {
                this.DoConcept(13).Value -= Operation.Intersect(this.DoConcept(4002), pair).Value;

                ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(Operation.Intersect(this.DoConcept(4002), pair));
                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), pair));
            
            }*/

            GetLog(MyRule, DebugRuleState.After, conceptList.Distinct().ToArray());
        }

       /// <summary>
        /// به ازای سه روز مرخصی به پرسنل شیفت مقدار 24 ساعت اضافه کار لحاظ گردد
       /// </summary>
       /// <param name="MyRule"></param>
        public virtual void R6503(AssignedRule MyRule)
        {
            var conceptList = new List<int>();
            conceptList.AddRange(new[] { 4002 });
            GetLog(MyRule, DebugRuleState.Before, conceptList.Distinct().ToArray());
            if (this.DoConcept(1004).Value == 3)
            {
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(24 * 60);
            }
            GetLog(MyRule, DebugRuleState.After, conceptList.Distinct().ToArray());
        }

        /// <summary>
        /// اعمال اضافه کار بسیج با کد 155
        /// درخواست اضافه کار عادی منجر به مجاز شدن نمیشود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6504(AssignedRule MyRule)
        {
            var conceptList = new List<int>();
            conceptList.AddRange(new[] { 4002, 4003, 13,6505 });
            GetLog(MyRule, DebugRuleState.Before, conceptList.Distinct().ToArray());
            PairableScndCnpValuePair pair = new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt());

            Permit permit = this.Person.GetPermitByDate(this.RuleCalculateDate, EngEnvironment.GetPrecard(155));
            if (permit != null && permit.Value > 0
                && Operation.Intersect(this.DoConcept(4003), pair).Value > 0)
            {
                this.DoConcept(13).Value += Operation.Intersect(this.DoConcept(4003), pair).Value;

                ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(Operation.Intersect(this.DoConcept(4003), pair));
                ((PairableScndCnpValue)this.DoConcept(6505)).AppendPairs(Operation.Intersect(this.DoConcept(4003), pair));
                ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(Operation.Differance(this.DoConcept(4003), pair));

            }
           
            GetLog(MyRule, DebugRuleState.After, conceptList.Distinct().ToArray());
        }

        /// <summary>
        /// به ازای هر تطیل رسمی به غیر از جمعه , ..... اضافه کار لحاظ گردد
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R6034(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 13, 4002);
            int overwork = MyRule["first", this.RuleCalculateDate].ToInt();
            if (EngEnvironment.HasCalendar(this.RuleCalculateDate, "1") && this.RuleCalculateDate.DayOfWeek != DayOfWeek.Friday && this.RuleCalculateDate.DayOfWeek != DayOfWeek.Thursday)
            {
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(overwork);
                if (this.DoConcept(13).Value < 24 * 60 + overwork)
                {
                    this.DoConcept(13).Value += overwork;
                }
            }
            GetLog(MyRule, DebugRuleState.After, 13, 4002);
        }
        #endregion

        #region قوانين عمومي

     
        #endregion

        #region Concept Init


        #endregion


        #endregion

    }
}

