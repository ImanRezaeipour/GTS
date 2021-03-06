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

        public override void R2005(AssignedRule MyRule)
        {
            //11 مفهوم تعداد روز
            //کارکردلازم ماهانه 10
            //کارکردخالص ساعتي ماهانه 8
            //3 کارکردناخالص ماهانه

            //غیبت ساعتی مجاز ماهانه 3026
            //3034 غیبت ساعتی غیرمجاز ماهانه
            //3005 غيبت خالص روزانه ماهانه
            //3019 غيبت درروز

            //4005 اضافه کارساعتي مجاز ماهانه
            //4006 اضافه کارساعتي غیرمجاز ماهانه
            //4018  حداکثر اضافه کار مجاز ماهانه

            //9 حضورماهانه           

            GetLog(MyRule, DebugRuleState.Before ,10,3034, 4005, 8, 3);
            int daterangeORder = this.GetDateRange(3, this.ConceptCalculateDate).DateRangeOrder;

            int lazem = MyRule[daterangeORder.ToString() + "th", this.RuleCalculateDate].ToInt() * HourMin;
            this.DoConcept(10).Value = lazem;
            this.DoConcept(3034).Value = 0;
            this.DoConcept(4005).Value = 0;
            if (this.DoConcept(3005).Value > 0)
            {
                lazem -= this.DoConcept(3005).Value * this.DoConcept(3019).Value;
            }
            if (lazem > this.DoConcept(3).Value)
            {
                int tmp = lazem - this.DoConcept(3).Value;
                this.DoConcept(3034).Value = tmp;
            }
            else
            {
                int tmp = this.DoConcept(3).Value - lazem;
                this.DoConcept(4005).Value = tmp;
            }

            GetLog(MyRule, DebugRuleState.After, 10, 3034, 4005, 8, 3);
        }

        /// <summary>
        /// محاسبه کارکرد پرسنل اقماری
        /// </summary>
        /// <param name="MyRule"></param>
        public void R2501(AssignedRule MyRule)
        {
            //5 کارکرد خالص روانه ماهانه
            //7  کارکرددرروز
            //8  کارکردخالص ساعتي ماهانه 
            //3 کارکرد ناخالص ماهانه
            //9 حضور ماهانه
            //3005 غیبت ماهانه
            //4005 اضافه کار ماهانه
            //3034 غیبت ساعتی ماهانه
            //10 کارکرد لازم ماهانه
            if (this.DoConcept(5021).Value > 0)
            {
                GetLog(MyRule, DebugRuleState.Before , 3, 4005, 5, 3034, 3005, 8, 10, 4002);
                int karkerd = this.DoConcept(5).Value;
                DateRange dateRange = this.GetDateRange(5, this.RuleCalculateDate);
                if (dateRange != null)
                {
                    int monthDays = (dateRange.ToDate - dateRange.FromDate).Days + 1;

                    int maxKarkerd = 23;
                    bool calcHouryAbsence = true;
                    if (maxKarkerd - karkerd > 0) //غیبت روزانه
                    {
                        this.DoConcept(10).Value = karkerd * this.DoConcept(7).Value;
                    }
                    else
                    {
                        this.DoConcept(10).Value = maxKarkerd * this.DoConcept(7).Value;
                    }
                    float resultKarkerd = 0;
                    if (this.DoConcept(3).Value < this.DoConcept(10).Value)//به لحاظ مقدار حضور کسری دارد
                    {
                        //(karkerd na khales / 230) * month days
                        resultKarkerd = (((float)this.DoConcept(3).Value / (float)(maxKarkerd * this.DoConcept(7).Value)) * monthDays);
                        calcHouryAbsence = false;
                    }
                    else
                    {
                        //(karkerd ruzane / 23) * month days
                        resultKarkerd = (((float)karkerd / (float)maxKarkerd) * monthDays);
                        if (resultKarkerd > monthDays)
                        {
                            resultKarkerd = monthDays;
                        }
                        calcHouryAbsence = true;
                    }

                    if (resultKarkerd > (int)resultKarkerd + 0.5)
                    {
                        resultKarkerd = (int)resultKarkerd + 1;
                    }

                    this.DoConcept(8).Value = 0;
                    this.DoConcept(5).Value = (int)resultKarkerd;
                    this.DoConcept(3005).Value = ((dateRange.ToDate - dateRange.FromDate).Days + 1) - (int)resultKarkerd;
                    this.DoConcept(4005).Value = this.DoConcept(3).Value - this.DoConcept(10).Value;
                    if (this.DoConcept(4005).Value < 0 && calcHouryAbsence)
                    {
                        int absence = this.DoConcept(4005).Value * -1;
                        this.DoConcept(3005).Value += (int)((float)absence / (float)this.DoConcept(7).Value);
                        this.DoConcept(3034).Value = absence % this.DoConcept(7).Value;
                        this.DoConcept(5).Value -= (int)((float)absence / (float)this.DoConcept(7).Value);
                        this.DoConcept(4005).Value = 0;
                    }
                    else if (!calcHouryAbsence && this.DoConcept(4005).Value < 0)
                    {
                        this.DoConcept(4005).Value = 0;
                    }
                }
            }
            ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();
            GetLog(MyRule, DebugRuleState.After, 3, 4005, 5, 3034, 3005, 8, 10, 4002);
        }

        /// <summary>
        /// اگر کارکرد به .... رسید , کارکرد روزانه لحاظ گردد
        /// </summary>
        /// <param name="MyRule"></param>
        public void R2502(AssignedRule MyRule)
        {
            //5 کارکرد روانه ماهانه
            //7  کارکرددرروز
            //8 کارکرد ناخالص ماهانه
            //9 حضور ماهانه

            if (this.DoConcept(13).Value >= MyRule["first", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, DebugRuleState.Before , 4);
                this.DoConcept(4).Value = 1;
                GetLog(MyRule, DebugRuleState.After,4);
            }
        }

        /// <summary>
        /// اگر کارکرد به .... رسید , کارکرد روزانه لحاظ گردد
        /// </summary>
        /// <param name="MyRule"></param>
        public void R2503(AssignedRule MyRule)
        {
            //5 کارکرد روانه ماهانه
            //7  کارکرددرروز
            //8 کارکرد ناخالص ماهانه
            //9 حضور ماهانه

            GetLog(MyRule, DebugRuleState.Before , 4);
            if (this.DoConcept(13).Value >= MyRule["first", this.RuleCalculateDate].ToInt())
            {
                this.DoConcept(4).Value = 1;
            }

            GetLog(MyRule, DebugRuleState.After, 4);

        }

        /// <summary>
        /// کارکرد پرسنل 12 ساعته
        /// </summary>
        /// <param name="MyRule"></param>
        public void R2504(AssignedRule MyRule)
        {
            //5 کارکرد روانه ماهانه
            //7  کارکرددرروز
            //8 کارکرد ناخالص ماهانه
            //9 حضور ماهانه

            GetLog(MyRule, DebugRuleState.Before , 5);
            if (this.DoConcept(5021).Value > 0)
            {
                int karkerd = this.DoConcept(5).Value;
                float zarib = karkerd * 1.5f;
                this.DoConcept(5).Value = (int)zarib;
                if (zarib > this.DoConcept(5).Value + 0.5)
                {
                    this.DoConcept(5).Value = this.DoConcept(5).Value + 1;
                }
            }
            GetLog(MyRule, DebugRuleState.After, 5);
        }


        #endregion

        #region قوانين مرخصي

        /// <summary>
        /// مرخصی به کارکرد خالص اضافه شود با توجه به مرخصی در روز
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R3007(AssignedRule MyRule)
        {
            //تقويم تعطيل رسمي 1
            //تعطيل غير رسمي 2
            //تعطيل نوروز 4

            //2 مفهوم کارکردخالص ساعتي
            //4 مفهوم کارکردخالص روزانه
            //مرخصي درروز 1001
            //مرخصي استحقاقي روزانه 1005
            //مرخصي استحقاقي ساعتي 1003

            //1008 مرخصي استعلاجي ساعتي

            //1038,1039,1040,1041,1042 مرخصی باحقوق ساعتی
            //1043,1044,1045,1046,1047 مرخصی باحقوق روزانه

            //1090 مفهوم مجموع انواع مرخصی روزانه
            //1088 مفهوم مرخصی به کارکرد خالص اضافه شود
            GetLog(MyRule, DebugRuleState.Before , 2,4,13);

            this.DoConcept(1088).Value = 1;
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).PairCount > 0
                 && this.Person.GetShiftByDate(this.RuleCalculateDate).ShiftType != ShiftTypesEnum.OVERTIME)
            {
                if (this.DoConcept(1090).Value > 0)
                {
                    this.DoConcept(4).Value = 1;
                    this.DoConcept(2).Value = this.DoConcept(1001).Value;
                    this.ReCalculate(13);
                }
                else
                {
                    int value = 0;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1003)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1008)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1038)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1039)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1040)).Value;
                    value += Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(1042)).Value;
                    if (value > 0)
                    {
                        this.DoConcept(4).Value = 1;
                        this.DoConcept(2).Value += value;
                        this.ReCalculate(13);
                    }
                }
            }
            GetLog(MyRule, DebugRuleState.After, 2, 4, 13);
        }


        #endregion

        #region قوانين ماموريت
        public override void R4001(AssignedRule MyRule)
        {
            base.R4001(MyRule);
            if (this.DoConcept(2005).Value >= 1)
            {
                GetLog(MyRule, DebugRuleState.Before , 2, 4, 13);
                this.DoConcept(4).Value = 1;
                this.DoConcept(2).Value = this.DoConcept(2001).Value;
                this.ReCalculate(13);
                GetLog(MyRule, DebugRuleState.After, 2, 4, 13);
            }
        }
        #endregion

        #region قوانين کم کاري

        #endregion

        #region قوانين اضافه کاري

        /// <summary>
        /// محاسبه اضافه کار خالص شده
        /// غیبت تا 2 ساعت بخشوده و مازاد آن با ضریب دو از اضافه کار کسر گردد
        /// مقادیر اولیه غیبت و اضافه کار تغییر نکند
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6501(AssignedRule MyRule)
        {
            //4005 اضافه کارساعتي مجاز ماهانه
            //3034 غیبت ساعتی غیرمجاز ماهانه
            //4502 اضافه کار خالص شده ماهانه
            int zarib = MyRule["first", this.RuleCalculateDate].ToInt();
            int maxHour = MyRule["second", this.RuleCalculateDate].ToInt();
            if (this.DoConcept(9).Value > 0)
                GetLog(MyRule, DebugRuleState.Before ,4502);
            {
                int overwork = this.DoConcept(4005).Value;
                int absecns = this.DoConcept(3034).Value;
                if (absecns > maxHour)
                {
                    absecns -= maxHour;
                    absecns *= zarib;
                    overwork -= absecns;
                    this.DoConcept(4502).Value = overwork;
                }
                else
                {
                   // this.DoConcept(4502).Value = 0;
                    this.DoConcept(4502).Value = this.DoConcept (4005).Value ;
                }
                GetLog(MyRule, DebugRuleState.After, 4502);
            }
        }
       
        /// <summary>
        /// محدوده مجاز اضافه کار ماموریت 
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6510(AssignedRule MyRule)
        {
            //اضافه کار خاص ساعتي 4001            
            //4002 اضافه کار ساعتی
            //4003 اضافه کار ساعتی عیر مجاز
            Permit permit = Person.GetPermitByDate(ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.OverTime));
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value != 0 && this.DoConcept(2004).Value>0)
            {
                GetLog(MyRule, DebugRuleState.Before , 4003,4002);
               
                var temp = Operation.Differance(Operation.Intersect(this.DoConcept(4002), this.DoConcept(2004)), new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", RuleCalculateDate].ToInt()));
                ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(temp);
                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), temp));
                             
		            }

            else if (this.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday && this.DoConcept(2004).Value > 0)
            {
                 var temp = Operation.Differance(Operation.Intersect(this.DoConcept(4002), this.DoConcept(2004)), new PairableScndCnpValuePair(MyRule["Third", this.RuleCalculateDate].ToInt(), MyRule["Forth", RuleCalculateDate].ToInt()));
                ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(temp);


                ((PairableScndCnpValue)this.DoConcept(4002))
                                                .AddPairs(Operation.Differance(this.DoConcept(4002), temp));
                             
               
            }
            else if (this.RuleCalculateDate.DayOfWeek != DayOfWeek.Thursday && EngEnvironment.HasCalendar(this.RuleCalculateDate, "1") && this.DoConcept(2004).Value > 0)
            {
                 var temp = this.DoConcept(4002);
                ((PairableScndCnpValue)DoConcept(4003)).AddPairs(temp);
                ((PairableScndCnpValue)DoConcept(4002)).ClearPairs();
                
            }
            GetLog(MyRule, DebugRuleState.After, 4003, 4002);
        }
    
        /// <summary>
        /// محدوده مجاز اضافه کار ماموریت برای پرسنل بدون شیفت
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6511(AssignedRule MyRule)
        {
            //اضافه کار خاص ساعتي 4001            
            //4002 اضافه کار ساعتی
            //4003 اضافه کار ساعتی عیر مجاز
            Permit permit = Person.GetPermitByDate(ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.OverTime));
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0 && this.RuleCalculateDate.DayOfWeek != DayOfWeek.Thursday && !EngEnvironment.HasCalendar(this.RuleCalculateDate, "1") && this.DoConcept(2004).Value > 0)
            {
                GetLog(MyRule, DebugRuleState.Before , 4003, 4002);
                var temp = Operation.Differance(Operation.Intersect(this.DoConcept(4002), this.DoConcept(2004)), new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", RuleCalculateDate].ToInt()));
                ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(temp);


                ((PairableScndCnpValue)this.DoConcept(4002))
                                                .AddPairs(Operation.Differance(this.DoConcept(4002), temp));
              
            }

            else if (this.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday && this.DoConcept(2004).Value > 0)
            {
                
                var temp = Operation.Differance(Operation.Intersect(this.DoConcept(4002), this.DoConcept(2004)), new PairableScndCnpValuePair(MyRule["Third", this.RuleCalculateDate].ToInt(), MyRule["Forth", RuleCalculateDate].ToInt()));
                ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(temp);


                ((PairableScndCnpValue)this.DoConcept(4002))
                                                .AddPairs(Operation.Differance(this.DoConcept(4002), temp));
               
             }
            else if (this.RuleCalculateDate.DayOfWeek != DayOfWeek.Thursday && EngEnvironment.HasCalendar(this.RuleCalculateDate, "1") && this.DoConcept(2004).Value > 0)
            {
                var temp=Operation.Intersect(this.DoConcept(4002), this.DoConcept(2004));
                ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(temp);
               ( (PairableScndCnpValue)DoConcept(4002)).ClearPairs();
              
            }
            GetLog(MyRule, DebugRuleState.After, 4003, 4002);
        }
        //این قانون به صورت موقت اضافه شده
        public virtual void R6512(AssignedRule MyRule)
        {
            //اضافه کار خاص ساعتي 4001            
            //4002 اضافه کار ساعتی
            //4003 اضافه کار ساعتی عیر مجاز
       
         if (this.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday && this.DoConcept(2004).Value == 0)
           
            {
                GetLog(MyRule, DebugRuleState.Before , 4003, 4002);
                this.ReCalculate(4002);
               //  var temp=Operation.Intersect(this.DoConcept(1), new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", RuleCalculateDate].ToInt()));
                var temp = Operation.Differance(Operation.Intersect(this.DoConcept(4002), this.DoConcept(1)), new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", RuleCalculateDate].ToInt()));
                ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(temp);


                ((PairableScndCnpValue)this.DoConcept(4002))
                                                .AddPairs(Operation.Differance(this.DoConcept(4002), temp));

            }
         GetLog(MyRule, DebugRuleState.After, 4003, 4002);
        }

        #endregion


        #endregion        

    }
}