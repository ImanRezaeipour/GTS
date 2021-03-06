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

        /// <summary>
        /// روزانه اضافه کار با غیبت جبران شود
        /// در اسن نسخه به غیبت مجاز اضافه نمیکند تا یر جمع ماهانه درست باشد - بندر عباس
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R1005(AssignedRule MyRule)
        {
            //غيبت ساعتي 3001
            //اضافه کار ساعتي 56
            //3028 غيبت ساعتی غیرمجاز
            //4002 اضافه کارساعتي مجاز
            //2 کارکردخالص ساعتي
            //4017 اضافه کار مجاز کارتی
            ProceedTraffic proceedTraffic = this.Person.GetProceedTraficByDate(this.RuleCalculateDate);
            if (proceedTraffic.IsFilled)
            {
                GetLog(MyRule, DebugRuleState.Before,  3028, 4002, 4017, 2);
                if (this.DoConcept(4002).Value > 0 && this.DoConcept(3028).Value > 0)
                {
                    int tmp = Operation.Minimum(this.DoConcept(3028).Value,
                                                this.DoConcept(4002).Value,
                                                MyRule["First", this.RuleCalculateDate].ToInt());
                    ((PairableScndCnpValue)this.DoConcept(3028)).DecreasePairFromLast(tmp);
                    ((PairableScndCnpValue)this.DoConcept(4002)).DecreasePairFromLast(tmp);
                    this.DoConcept(2).Value += tmp;
                }

                if (this.DoConcept(4017).Value > 0 && this.DoConcept(3028).Value > 0)
                {
                    int tmp = Operation.Minimum(this.DoConcept(3028).Value,
                                                this.DoConcept(4017).Value,
                                                MyRule["First", this.RuleCalculateDate].ToInt());
                    ((PairableScndCnpValue)this.DoConcept(3028)).DecreasePairFromLast(tmp);
                    ((PairableScndCnpValue)this.DoConcept(4017)).DecreasePairFromLast(tmp);
                    this.DoConcept(2).Value += tmp;
                }
                GetLog(MyRule, DebugRuleState.After , 3028, 4002, 4017, 2);
            }
        }

        /// <summary>
        /// وظیفه اجرای مفاهیم ماهانه
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R1016(AssignedRule MyRule)
        {
            base.R1016(MyRule);
            this.DoConcept(2035);//ماموریت خارجی ماهانه
            this.DoConcept(2504);//ماموریت تعطیل ماهانه
            this.DoConcept(22);//حضور در تعطیلات خاص ماهانه
            this.DoConcept(21);//حضور تعطیلات خاص ماهانه
            this.DoConcept(1502);//رست

        }

        /// <summary>
        /// وظیفه اجرای مفاهیم روزانه
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R1017(AssignedRule MyRule)
        {
            base.R1017(MyRule);
            this.DoConcept(4501);//اضافه کارساعتی تعطیل_روز غیرکاری، تعطیل رسمی و غیررسمی
            this.DoConcept(2026);//ماموریت خارج از کشور
            this.DoConcept(1501);//رست

        }

        /// <summary>قانون متفرقه- ماهانه غیبتهای ساعتی با اضافه کار جبران شود
        /// </summary>
        /// 
        public void R1501(AssignedRule MyRule)
        {
            //غيبت ساعتی ماهانه 3034
            //اضافه کار ساعتي ماهانه 4005           
            //8 کارکردخالص ساعتي ماهانه
            //5 کارکردخالص روزانه ماهانه
            //3026 غيبت ساعتی مجاز ماهانه
            GetLog(MyRule, DebugRuleState.Before,3034, 3026,4006, 8, 4005);
            int t = this.DoConcept(3034).Value;
            if (this.DoConcept(4005).Value + this.DoConcept(4006).Value > 0 && t > 0)
            {
                int tmp = Operation.Minimum(this.DoConcept(4006).Value, t);
                this.DoConcept(3034).Value -= tmp;
                this.DoConcept(3026).Value += tmp;
                this.DoConcept(8).Value += tmp;
                this.DoConcept(4006).Value -= tmp;
                if (this.DoConcept(3034).Value <= 0)
                {
                    this.DoConcept(3034).Value = 1;
                }
                if (this.DoConcept(4006).Value <= 0)
                {
                    this.DoConcept(4006).Value = 1;
                }
            }
            t = this.DoConcept(3034).Value;
            if (this.DoConcept(4005).Value + this.DoConcept(4006).Value > 0 && t > 1)
            {
                int tmp = Operation.Minimum(this.DoConcept(4005).Value, t);
                this.DoConcept(3034).Value -= tmp;
                this.DoConcept(3026).Value += tmp;
                this.DoConcept(8).Value += tmp;
                this.DoConcept(4005).Value -= tmp;
                if (this.DoConcept(3034).Value <= 0)
                {
                    this.DoConcept(3034).Value = 1;
                }
                if (this.DoConcept(4005).Value <= 0)
                {
                    this.DoConcept(4005).Value = 1;
                }
            }
            GetLog(MyRule, DebugRuleState.After , 3034, 3026, 4006, 8, 4005);
        }

        #endregion

        #region قوانين کارکرد

        /// <summary>
        /// جهت دادن کارکرد روزانه یک بدون اعشار
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R2002(AssignedRule MyRule)
        {
            //3004 غيبت روزانه
            //7 کارکرددرروز
            //2 کارکردخالص ساعتي 
            //کارکردخالص روزانه 4
            ProceedTraffic ProceedTraffic = this.Person.GetProceedTraficAllByDate(this.RuleCalculateDate);
            if (this.DoConcept(3004).Value == 0 &&
                this.DoConcept(2).Value > 0 &&
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value >= this.DoConcept(7).Value &&
                this.Person.GetShiftByDate(this.RuleCalculateDate).ShiftType != ShiftTypesEnum.OVERTIME
                && ProceedTraffic.IsFilled)
            {
                GetLog(MyRule, DebugRuleState.Before,4);
                this.DoConcept(4).Value = 1;
                GetLog(MyRule, DebugRuleState.After , 4);
            }
        }

        public override void R2006(AssignedRule MyRule)
        {
            //3 کارکردناخالص ماهانه
            //6 کارکرد لازم
            //کارکردخالص ساعتي ماهانه 8
            //9 حضور ماهانه
            //کارکردلازم ماهانه 10
            //3034 غیبت ساعتی غیرمجاز ماهانه
            //4005 اضافه کارساعتي مجاز ماهانه
            //4006 اضافه کارساعتي غیرمجاز ماهانه
            //4010 اضافه کارساعتي تعطيل ماهانه

            //1006 مرخصي استحقاقي روزانه ماهانه
            //1011 مرخصي استحقاقي ساعتي ماهانه
            //1017 مرخصي استعلاجي روزانه ماهانه
            //1097 ممرخصی با حقوق ماهانه
            if (this.DoConcept(10).Value > 0)
            {
                GetLog(MyRule, DebugRuleState.Before,8, 10, 3034, 4005, 4006, 4010);
                int daterangeORder = base.GetDateRange(3, this.ConceptCalculateDate).DateRangeOrder;
                int hozour = this.DoConcept(9).Value;

                int monthlyLazem = MyRule[ daterangeORder.ToString() + "th", this.RuleCalculateDate].ToInt();
                this.DoConcept(10).Value = monthlyLazem * HourMin;
                if (this.DoConcept(3).Value >= monthlyLazem * HourMin)
                {
                    this.DoConcept(4005).Value = this.DoConcept(3).Value - monthlyLazem * HourMin;
                    this.DoConcept(8).Value = monthlyLazem * HourMin;
                    this.DoConcept(4006).Value = 0;
                    this.DoConcept(3034).Value = 0;                    
                }
                else
                {
                    this.DoConcept(3034).Value = (monthlyLazem * HourMin) - this.DoConcept(8).Value;
                    this.DoConcept(4005).Value = 0;
                    this.DoConcept(4006).Value = 0;
                    this.DoConcept(4010).Value = 0;
                }
                GetLog(MyRule, DebugRuleState.After , 8, 10, 3034, 4005, 4006, 4010);
            }

            GetLog(MyRule, DebugRuleState.Before ,3020,3028,4002,4007,4008,4003);
            this.DoConcept(3020).Value = 0;
            ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();
            ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();
            ((PairableScndCnpValue)this.DoConcept(4007)).ClearPairs();
            ((PairableScndCnpValue)this.DoConcept(4008)).ClearPairs();
            ((PairableScndCnpValue)this.DoConcept(4003)).ClearPairs();
            GetLog(MyRule, DebugRuleState.After , 3020, 3028, 4002, 4007, 4008, 4003);
        }

        /// <summary></summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي دویست و هشتاد و یک-280 درنظر گرفته شده است</remarks>
        ///  کارکرد روز جمعه
        ///  در نیمه اول سال تا ساعت 13(پایان شیفت) و در نیمه دوم تا ساعت 12 حضور داشت
        ///  هشت ساعت کارکرد (آقای گرامی) منظور گردد
        public void R2501(AssignedRule MyRule)
        {
            //7 کارکرد در روز
            //3028 غيبت ساعتی غیرمجاز
            //3029 تاخیر
            //3030 تعجیل
            //3031 غیبت بین وقت
            //3004 غیبت روزانه
            //4002 اضافه کارساعتي مجاز
            //4003 اضافه کارساعتي غیرمجاز
            //3020 غيبت ساعتی مجاز
            //3026 غيبت ساعتی مجاز ماهانه

            if (this.RuleCalculateDate.DayOfWeek == DayOfWeek.Friday)
            {
                GetLog(MyRule, DebugRuleState.Before, 2, 4, 3028, 3020,3034,3026,4002, 4003);
                PersianDateTime pd = Utility.ToPersianDateTime(this.RuleCalculateDate);
                if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0
                    && this.DoConcept(3004).Value == 0 && this.DoConcept(1).Value > 0)
                {
                    bool canApply = false;
                    if (pd.Month <= 6) //نیمه اول سال
                    {
                        //در نیمه اول سال حضور تا ساعت 13 یعنی حضور تا پایان شیفت
                        if (Operation.Intersect(this.DoConcept(3028), new PairableScndCnpValuePair(MyRule[ "First", this.RuleCalculateDate].ToInt() - 1, MyRule[ "First", this.RuleCalculateDate].ToInt())).Value == 0)
                        {
                            canApply = true;
                        }
                    }
                    else//نیمه دوم سال
                    {
                        if (Operation.Intersect(this.DoConcept(3028), new PairableScndCnpValuePair(MyRule[ "Second", this.RuleCalculateDate].ToInt() - 1, MyRule[ "Second", this.RuleCalculateDate].ToInt())).Value == 0)
                        {
                            canApply = true;
                        }
                    }
                    if (canApply)
                    {
                        IPair pair = this.Person.GetShiftByDate(this.RuleCalculateDate).First;//new PairableScndCnpValuePair(this.Person.GetShiftByDate(this.RuleCalculateDate).First.From, this.Person.GetShiftByDate(this.RuleCalculateDate).First.From + (8 * 60));
                        PairableScndCnpValue result = Operation.Differance(pair, this.DoConcept(3029));
                        result = Operation.Differance(result, this.DoConcept(3031));
                        result = Operation.Differance(result, this.DoConcept(3029));

                        this.DoConcept(2).Value = Operation.Intersect(result, this.Person.GetShiftByDate(this.RuleCalculateDate)).Value;
                        this.DoConcept(4).Value = 1;

                        if (Operation.Intersect(result, this.DoConcept(3028)).Value > 0)
                        {
                            this.DoConcept(3020).Value += Operation.Intersect(this.DoConcept(3030), result).Value;
                            ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(Operation.Differance(this.DoConcept(3028), result));                      
                        }
                        this.ReCalculate(13);
                        if (this.DoConcept(3034).Value > 0) 
                        {
                            this.ReCalculate(3034);
                            this.ReCalculate(3026);
                        }
                    }
                }
                GetLog(MyRule, DebugRuleState.After, 2, 4, 3028, 3020, 3034, 3026, 4002, 4003);
            }
        }

        /// <summary></summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي دویست و هشتاد و یک-280 درنظر گرفته شده است</remarks>
        ///  کارکرد در تعطیلات خاص
        ///  اگر حداقل تا 12 ظهر حضورداشت تعلق گیرد
        public void R2502(AssignedRule MyRule)
        {
            //7 کارکرد در روز
            //51 تعطیلات خاص مانند عید قربان و ... 
            //20 حضور تعطیلات خاص
            //4002 اضافه کار مجاز
            //3006 غیبت روزانه
            if (this.EngEnvironment.HasCalendar(this.RuleCalculateDate, "51"))
            {
                GetLog(MyRule, DebugRuleState.Before, 3028, 2, 4, 3004,1501,20,4002);
                if (this.DoConcept(4002).Value == 0)
                {
                    this.DoConcept(4).Value = 1;
                    this.DoConcept(2).Value = this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues; //this.DoConcept(7).Value;
                }
                this.DoConcept(3004).Value = 0;
                this.DoConcept(1501).Value = 0;
                if (this.DoConcept(1).Value > 0)
                {
                    ProceedTraffic ProceedTraffic = this.Person.GetProceedTraficByDate(this.RuleCalculateDate);
                    int aftermidDayCount = ProceedTraffic.Pairs.OrderBy(x => x.To).Where(x => x.IsFilled && x.To > 12 * HourMin).Count();
                    if (aftermidDayCount > 0)
                    {
                        this.DoConcept(20).Value = 1;
                        ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(this.DoConcept(1));
                    }
                }
                this.ReCalculate(13);
                GetLog(MyRule, DebugRuleState.After , 3028, 2, 4, 3004, 1501, 20, 4002);
            }

        }

        /// <summary>
        /// فلات - مجموع کارکرد لازم در نیمه اول سال 198:25 و در نیمه دوم 192 میباشد
        /// نیمه اول و دوم بر اساس تاریخ انتساب پارامتر مشخص میگردد
        /// </summary>
        /// <param name="MyRule"></param>
        public void R2503(AssignedRule MyRule)
        {
            //10 کارکرد لازم ماهانه           

            GetLog(MyRule, DebugRuleState.Before , 10);

            int lazem = MyRule["First", this.RuleCalculateDate].ToInt();

            this.DoConcept(10).Value = lazem;

            GetLog(MyRule, DebugRuleState.After, 10);
        }
        
        #endregion

        #region قوانين مرخصي

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
                    this.DoConcept(2).Value = this.DoConcept(6).Value; ;// this.DoConcept(1001).Value;
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
            GetLog(MyRule, DebugRuleState.Before, 2, 4,13);
        }

        /// <summary>
        /// اعمال رست
        /// </summary>
        /// <param name="MyRule"></param>
        public void R3501(AssignedRule MyRule)
        {
            //202 کد پیشکارت مجوز رست
            //1501 رست
            GetLog(MyRule, DebugRuleState.Before, 1501,13);
            this.DoConcept(1501);
            GetLog(MyRule, DebugRuleState.After , 1501, 13);
        }

        /// <summary>
        /// در صورتی که کمتر از 7 روز رست استفاده کند از کارکرد ماهانه کسرمیگردد
        /// </summary>
        /// <param name="MyRule"></param>
        public void R3502(AssignedRule MyRule)
        {
            //1501 رست
            //1502 رست ماهانه
            //3 :کارکرد ناخالص  ماهانه
            //7 کارکرد در روز
            //8 کارکردخالص ساعتي ماهانه  

            //مطمئن شویم آخر ماهه
            if (this.DoConcept(10).Value > 0 && this.DoConcept(1502).Value < 7)
            {
                GetLog(MyRule, DebugRuleState.Before, 8,3);
                int diff = 7 - this.DoConcept(1502).Value;
                this.DoConcept(8).Value -= diff * this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues; // this.DoConcept(7).Value;
                this.DoConcept(3).Value -= diff * this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues; //this.DoConcept(7).Value;
                //this.DoConcept(10).Value -= diff * this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues; //this.DoConcept(7).Value;
                GetLog(MyRule, DebugRuleState.After , 8,3);
            }
        }

        /// <summary>
        /// در صورتی که بیشتر از 7 روز رست استفاده کند به کارکرد ماهانه اضافه میگردد
        /// </summary>
        /// <param name="MyRule"></param>
        public void R3503(AssignedRule MyRule)
        {
            //1501 رست
            //1502 رست ماهانه
            //3 :کارکرد ناخالص  ماهانه     
            //7 کارکرد در روز
            //8 کارکردخالص ساعتي ماهانه
            //10 کارکردلازم ماهانه
            if (this.DoConcept(1502).Value > 7)
            {
                GetLog(MyRule, DebugRuleState.Before , 8, 3);
                int diff = this.DoConcept(1502).Value - 7;
                this.DoConcept(8).Value += diff * this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues; // this.DoConcept(7).Value;
                this.DoConcept(3).Value += diff * this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues; //this.DoConcept(7).Value;
                //this.DoConcept(10).Value += diff * this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues; //this.DoConcept(7).Value;
                GetLog(MyRule, DebugRuleState.After, 8, 3);
            }
        }

        /// <summary>
        /// اعمال خروج روز قبل و ورود روز بعد از رست  
        /// آولویت آن از 100011 به 1801  منتقل شد تا قبل قانون 13 انجام شود
        /// </summary>
        /// <param name="MyRule"></param>
        public void R3504(AssignedRule MyRule)
        {
            //1501 رست
            //1502 رست ماهانه
            //7 کارکرد در روز
            //3028 غیبت ساعنی
            //3004 غیبت روزانه
            //3029 تاخیر
            //3020 غیبت ساتی مجاز
            //4002 اضافه کار
            //3030 تعجیل ساعتی غیر مجاز
            //1005 استحقاقی روزانه
            
            //مثلا اگر روز اول رست تعطیل کاری باشد
            Permit rest = this.Person.GetPermitByDate(this.RuleCalculateDate, EngEnvironment.GetPrecard(202));
            if ((/*this.DoConcept(1501).Value == 1*/
                (rest != null && rest.Value > 0) || this.DoConcept(1005).Value == 1)
                && this.DoConcept(1501, this.RuleCalculateDate.AddDays(-1)).Value == 0
                && this.DoConcept(1005, this.RuleCalculateDate.AddDays(-1)).Value == 0)
            {
                ProceedTraffic ProceedTraffic = this.Person.GetProceedTraficByDate(this.RuleCalculateDate.AddDays(-1));
                if (ProceedTraffic != null && ProceedTraffic.PairCount > 0)
                {
                    if (Operation.Intersect(
                        this.DoConcept(3028, this.RuleCalculateDate.AddDays(-1)),
                        this.DoConcept(3030, this.RuleCalculateDate.AddDays(-1)))
                        .Value > 0)
                    {
                        IPair pair = ProceedTraffic.Pairs.OrderBy(x => x.To).Last();
                        if (pair.To >= 12 * HourMin)
                        {
                            IPair alowPair = new PairableScndCnpValuePair(12 * HourMin, this.Person.GetShiftByDate(this.RuleCalculateDate).PastedPairs.To);
                            PairableScndCnpValue allowAbsence = Operation.Intersect(alowPair, this.DoConcept(3030, this.RuleCalculateDate.AddDays(-1)));

                            this.DoConcept(2, this.RuleCalculateDate.AddDays(-1)).Value += allowAbsence.PairValues;
                            this.DoConcept(4, this.RuleCalculateDate.AddDays(-1)).Value = 1;
                            this.DoConcept(3020, this.RuleCalculateDate.AddDays(-1)).Value += allowAbsence.PairValues;
                            PairableScndCnpValue absence = Operation.Differance(this.DoConcept(3030, this.RuleCalculateDate.AddDays(-1)), allowAbsence);
                            ((PairableScndCnpValue)this.DoConcept(3030, this.RuleCalculateDate.AddDays(-1))).AddPairs(absence);
                            this.DoConcept(3004, this.RuleCalculateDate.AddDays(-1)).Value = 0;
                            this.ReCalculate(3028, this.RuleCalculateDate.AddDays(-1));
                            this.ReCalculate(4002, this.RuleCalculateDate.AddDays(-1));
                            this.ReCalculate(13, this.RuleCalculateDate.AddDays(-1));
                        }
                    }
                }
            }
            //مثلا اگر روز آخر رست تعطیل کاری باشد
            rest = this.Person.GetPermitByDate(this.RuleCalculateDate.AddDays(-1), EngEnvironment.GetPrecard(202));
            if ((/*this.DoConcept(1501, this.RuleCalculateDate.AddDays(-1)).Value == 1*/
                (rest != null && rest.Value > 0) ||
                this.DoConcept(1005, this.RuleCalculateDate.AddDays(-1)).Value == 1
                )
                && this.DoConcept(1501).Value == 0 && this.DoConcept(1005).Value == 0)
            {
                GetLog(MyRule, DebugRuleState.Before, 4028, 2, 4, 3028, 3004, 3020, 4002, 13);
                ProceedTraffic ProceedTraffic = this.Person.GetProceedTraficByDate(this.RuleCalculateDate);
                if (ProceedTraffic != null && ProceedTraffic.PairCount > 0)
                {
                    if (Operation.Intersect(
                       this.DoConcept(3028), this.DoConcept(3029)).Value > 0)
                    {
                        IPair pair = ProceedTraffic.Pairs.OrderBy(x => x.To).Last();
                        if (pair.From <= 11 * HourMin + 30)
                        {
                            IPair alowPair = new PairableScndCnpValuePair(5 * 60, 11 * HourMin + 30);
                            PairableScndCnpValue allowAbsence = Operation.Intersect(alowPair, this.DoConcept(3028));

                            this.DoConcept(2).Value += allowAbsence.PairValues;
                            this.DoConcept(4).Value = 1;

                            this.DoConcept(3020).Value += allowAbsence.PairValues;
                            PairableScndCnpValue absence = Operation.Differance(this.DoConcept(3028), allowAbsence);
                            ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(absence);
                            this.DoConcept(3004).Value = 0;
                            this.ReCalculate(4002);
                            this.ReCalculate(13);
                        }
                    }
                }
                GetLog(MyRule, DebugRuleState.After, 4028, 2, 4, 3028, 3004, 3020, 4002, 13);
            }

            
        }

        #endregion

        #region قوانين ماموريت

        /// <summary>ماموريت 3-1</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي يکصدوبيست وسه-3034 درنظر گرفته شده است</remarks>
        /// ماموريت 3-1: ماموريت به کارکرد خالص اضافه شود
        public override void R123(AssignedRule MyRule)
        {
            //2023 مجموع ماموريت ساعتي
            //2005 مجموع ماموریت روزانه
            //2026 ماموریت خارج از کشور
            //کارکردخالص ساعتي 2
            //4 کارکردخالص روزانه
            //13 کارکرد ناخالص ساعتی
            //ماموريت خالص روزانه 2003
            //348 ماموریت به کارکردخالص اضافه شود
            //ماموريت درروز 2001
            //7 کارکرد در روز
            GetLog(MyRule, DebugRuleState.Before ,  2, 4, 13, 4002);
            if (!EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2"))
            {
                if (this.DoConcept(2005).Value == 1 || this.DoConcept(2026).Value == 1)
                {
                    this.DoConcept(4).Value = 1;
                    if (this.DoConcept(6).Value > 0)
                    {
                        this.DoConcept(2).Value = this.DoConcept(6).Value;
                    }
                    //else //باید توسط قانون روز غیر کاری به کارکرد اضافه شود انجام گردد
                    //{
                    //    this.DoConcept(2).Value = this.DoConcept(7).Value;
                    //}
                    if (this.DoConcept(2).Value == 0)
                    {
                        this.DoConcept(4).Value = 0;
                    }
                }
                else
                {
                    int value = 0;
                    value = Operation.Intersect(this.Person.GetShiftByDate(this.RuleCalculateDate), this.DoConcept(2023)).Value;
                    if (value > 0)
                    {
                        if (this.Person.GetShiftByDate(this.RuleCalculateDate).ShiftType == ShiftTypesEnum.WORK)
                        {
                            this.DoConcept(2).Value += value;
                        }
                        else if (this.Person.GetShiftByDate(this.RuleCalculateDate).ShiftType == ShiftTypesEnum.OVERTIME)
                        {
                            ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(value);
                        }
                    }
                }
                this.ReCalculate(13);
            }
            GetLog(MyRule, DebugRuleState.After , 2, 4, 13,  4002);
        }

        /// <summary>
        /// ماموریت روزانه در روز پنج شنبه ... برابر شود
        /// </summary>
        /// <param name="MyRule"></param>
        public void R4501(AssignedRule MyRule)
        {
            // ماموریت روزانه در روزهای پنج شنبه با ضریب
            // First
            // محاسبه شود
            // مجموع ماموريت روزانه2005
            GetLog(MyRule, DebugRuleState.Before , 2005);

            if (
                this.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday &&
                this.DoConcept(2005).Value > 0
                )
            {
                var coEfficient = (MyRule["First", this.RuleCalculateDate].ToInt() / 100f);
                var temp = (int)Math.Round((this.DoConcept(2005).Value * coEfficient));

                this.DoConcept(2005).Value += temp;
            }

            GetLog(MyRule, DebugRuleState.After, 2005);

        }

        /// <summary>
        /// حضور اول وقت قبل از ماموریت ساعتی بعد از ساعت 10 اجباری است
        /// </summary>
        /// <param name="MyRule"></param>
        public void R4502(AssignedRule MyRule)
        {
            //2 کارکرد خالص
            //ماموريت ساعتي 2004
            //3021 تاخیر مجاز 
            //3029 تاخیر غیر مجاز
            GetLog(MyRule, DebugRuleState.Before , 2004, 2, 13, 3021,3029,3020, 3028);
       
            if (this.DoConcept(2004).Value > 0)
            {
                PairableScndCnpValue duty = ((PairableScndCnpValue)this.DoConcept(2004));
                PairableScndCnpValue present = ((PairableScndCnpValue)this.DoConcept(1));

                if (duty.Pairs.FirstOrDefault() != null && duty.Pairs.OrderBy(x => x.From).First().From > 10 * HourMin)
                {
                    if (present.Pairs.FirstOrDefault() != null && present.Pairs.OrderBy(x => x.From).First().From > 10 * HourMin)
                    {
                        duty.ClearPairs();
                        this.DoConcept(2004).Value = 0;
                    }
                }
                else
                {//و اگرتاخیر داشت مجاز است
                    ProceedTraffic proceedTraffic = this.Person.GetProceedTraficByDate(this.RuleCalculateDate);
                    if (proceedTraffic.Pairs.FirstOrDefault() == null || proceedTraffic.Pairs.OrderBy(x => x.From).First().From > 10 * HourMin)
                    {
                        this.DoConcept(3021).Value = this.DoConcept(3029).Value;
                        ((PairableScndCnpValue)this.DoConcept(3029)).ClearPairs();
                        this.DoConcept(2).Value += this.DoConcept(3021).Value;
                        this.ReCalculate(13, 3020, 3028);
                    }
                }
            }
            GetLog(MyRule, DebugRuleState.After, 2004, 2, 13, 3021, 3029, 3020, 3028);
        }

        /// <summary>
        /// اعمال مجوز تاخیر مجاز بعد از ماموریت ساعت 23
        /// </summary>
        /// <param name="MyRule"></param>
        public void R4503(AssignedRule MyRule)
        {
            //3021 تاخیر مجاز
            //تاخیر ساعتي 3029
            //201 کد پیشکارت درخواست تاخیر مجاز ماموریت
           
            /*if (this.DoConcept(3029).Value > 0)
            {
                PairableScndCnpValue late = ((PairableScndCnpValue)this.DoConcept(3029));
                Permit permit = this.Person.GetPermitByDate(this.RuleCalculateDate, EngEnvironment.GetPrecard(201));
                if (permit != null && (permit.Value > 0 || permit.PairCount > 0))
                {
                    IPair pair = new ShiftPair();
                    pair.From = this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).First().From;
                    pair.To = pair.From + MyRule["First", this.RuleCalculateDate].ToInt();
                    this.DoConcept(3021).Value = Operation.Intersect(pair, late).Value;
                    ((PairableScndCnpValue)this.DoConcept(3029)).AddPairs(Operation.Differance(late, pair));
                    this.ReCalculate(13, 3020, 3028);
                }
            }*/

            Permit permit = this.Person.GetPermitByDate(this.RuleCalculateDate, EngEnvironment.GetPrecard(201));
            if (permit != null && (permit.Value > 0 || permit.PairCount > 0))
            {
                GetLog(MyRule, DebugRuleState.Before , 4002);
                IPair pair = permit.Pairs.First();
                IPair mojaz = new ShiftPair(1, 6 * 60);
                PairableScndCnpValue overtime = Operation.Intersect(pair, mojaz);
                ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(overtime);
                GetLog(MyRule, DebugRuleState.After, 4002);
            }
        }

        #endregion

        #region قوانين کم کاري

        /// <summary>قانون کم کاري 22-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي شصت و هفت-67 درنظر گرفته شده است</remarks>
        /// کم کاري 22-2: روش تبديل ساعات غيبت به روزانه: هر .... معادل يک روز غيبت 
        public void R5501(AssignedRule MyRule)
        {
            //3004 غيبت روزانه
            //غيبت ساعتي غيرمجازي 3028
            //1 حضور

            //1002 مرخصي خالص استحقاقي ساعتي 
            //1056 مرخصی بی حقوق ساعتی 12
            //2002 ماموريت خالص ساعتي
            //581 مجموع مرخصی بی حقوق روزانه
            GetLog(MyRule, DebugRuleState.Before , 3028, 3004, 1002, 2002,1056);
            this.DoConcept(1002);
            this.DoConcept(1056);
            this.DoConcept(2002);
            if (this.DoConcept(3028).Value > 0
                && this.DoConcept(1).Value == 0 && this.DoConcept(2).Value == 0
                && this.DoConcept(1090).Value == 0 && this.DoConcept(2005).Value == 0
                && this.DoConcept(1091).Value == 0
                && this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0
                && MyRule["First", this.RuleCalculateDate].ToInt() != 0)
            {
                this.DoConcept(3004).Value += this.Person.GetShiftByDate(this.RuleCalculateDate).Value /
                                                                MyRule["First", this.RuleCalculateDate].ToInt();
                this.DoConcept(3028).Value = this.Person.GetShiftByDate(this.RuleCalculateDate).Value %
                                                                MyRule["First", this.RuleCalculateDate].ToInt();
            }
            GetLog(MyRule, DebugRuleState.After, 3028, 3004, 1002, 2002, 1056);
        }

        #endregion

        #region قوانين اضافه کاري
      
        public override void R6022(AssignedRule MyRule)
        {
            //ماموريت روزانه 2005            
            //اضافه کار ساعتي 4002
            //1 مفهوم حضور
            if (this.DoConcept(2005).Value > 0 && MyRule["First", this.RuleCalculateDate].ToInt() == 0) 
            {
                ((PairableScndCnpValue)this.DoConcept(1)).ClearPairs();
                this.DoConcept(13).Value = 0;
            }
            else if (this.DoConcept(2005).Value > 0 && this.DoConcept(1).Value > 0)
            {
                GetLog(MyRule, DebugRuleState.Before , 4002,13);

                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(((PairableScndCnpValue)this.DoConcept(1)).Pairs);

                //float coEfficient = 1 + (MyRule["First", this.RuleCalculateDate].ToInt() / 100f);
                var coEfficient = (int)Math.Round((decimal)(this.DoConcept(4002).Value * (MyRule["First", this.RuleCalculateDate].ToInt() / 100)));

                //((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue((int)Math.Round(this.DoConcept(4002).Value * coEfficient));
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(coEfficient);
                this.ReCalculate(13);

                GetLog(MyRule, DebugRuleState.After, 4002, 13);
            }
        }

        /// <summary>حداکثر سقف اضافه کار تعطیل در روز</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و پنجاه و هشت-158 درنظر گرفته شده است</remarks>
        public void R6501(AssignedRule MyRule)
        {
            
            //4015 اضافه کار با مجوز باشد
            //4501 اضافه کارساعتی تعطیل_روز غیرکاری، تعطیل رسمی و غیررسمی
            //4002 اضافه کار ساعتي مجاز
            //4003 اضافه کار ساعتي غيرمجاز

            if (this.DoConcept(4015).Value == 1)
                return;
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0
                && this.DoConcept(4501).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                GetLog(MyRule, DebugRuleState.Before , 4501, 4002, 4003, 13);
                ((PairableScndCnpValue)this.DoConcept(4003)).AssignValue(this.DoConcept(4501).Value - MyRule["First", this.RuleCalculateDate].ToInt());
                ((PairableScndCnpValue)this.DoConcept(4002)).DecreasePairFromLast(this.DoConcept(4501).Value - MyRule["First", this.RuleCalculateDate].ToInt());
                ((PairableScndCnpValue)this.DoConcept(4501)).DecreasePairFromLast(this.DoConcept(4501).Value - MyRule["First", this.RuleCalculateDate].ToInt());
                this.ReCalculate(13);
                GetLog(MyRule, DebugRuleState.After, 4501, 4002, 4003, 13);
            }
        }

        /// <summary>
        /// سقف اضافه کار مجاز در نیمه اول و دوم سال
        /// </summary>
        /// <param name="MyRule"></param>
        public void R6502(AssignedRule MyRule)
        {           
            //4005 اضافه کار مجاز ماهانه
            //اضافه کار غیر مجاز ماهانه  4006           
            //3 کارکرئناخالص ماهانه
            GetLog(MyRule, DebugRuleState.Before , 4005, 4006,3);
            int maxOverworkHalf1 = MyRule["First", this.RuleCalculateDate].ToInt();
            int maxOverworkHalf2 = MyRule["Second", this.RuleCalculateDate].ToInt();
            PersianDateTime pd = Utility.ToPersianDateTime(this.RuleCalculateDate);
            if (pd.Month <= 6)//نیمه اول سال
            {
                if (this.DoConcept(4005).Value > maxOverworkHalf1)
                {
                    this.DoConcept(4006).Value += this.DoConcept(4005).Value - maxOverworkHalf1;
                    this.DoConcept(3).Value -= this.DoConcept(4005).Value - maxOverworkHalf1;
                    this.DoConcept(4005).Value = maxOverworkHalf1;
                }
            }
            else
            {
                if (this.DoConcept(4005).Value > maxOverworkHalf2)
                {
                    this.DoConcept(4006).Value += this.DoConcept(4005).Value - maxOverworkHalf2;
                    this.DoConcept(3).Value -= this.DoConcept(4005).Value - maxOverworkHalf2;
                    this.DoConcept(4005).Value = maxOverworkHalf2;
                }
            }
            GetLog(MyRule, DebugRuleState.After, 4005, 4006, 3);

        }

        /// <summary>
        /// محاسبه اضافه کار بر مبنای حضور و کارکرد لازم
        /// </summary>
        /// <param name="MyRule"></param>
        public void R6503(AssignedRule MyRule)
        {
            //10 کارکرد لازم ماهانه
            //9 حضور ماهانه
            //4005 اضافه کار مجاز ماهانه
            //اضافه کار غیر مجاز ماهانه  4006           
            GetLog(MyRule, DebugRuleState.Before , 4005, 4006);
            this.DoConcept(4005).Value = this.DoConcept(9).Value - this.DoConcept(10).Value;
            if (this.DoConcept(4005).Value < 0)
                this.DoConcept(4005).Value = 0;
            this.DoConcept(4006).Value = 0;
            GetLog(MyRule, DebugRuleState.After, 4005, 4006);

        }

        #endregion
      

        #endregion
    }
}