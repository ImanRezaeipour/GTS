using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using GTS.Clock.Model;
using System.Reflection;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Concepts.Operations;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model.ELE;
using GTS.Clock.Business.AppSettings;

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
        public override void R13(AssignedRule MyRule)
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
                GetLog(MyRule, " Before Execute State:", 3028, 4002, 4017, 2);
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
                GetLog(MyRule, " After Execute State:", 3028, 4002, 4017, 2);
            }
        }

        /// <summary>قانون متفرقه- ماهانه غیبتهای ساعتی با اضافه کار جبران شود
        /// </summary>
        /// 
        public void R285(AssignedRule MyRule)
        {
            //غيبت ساعتی ماهانه 3034
            //اضافه کار ساعتي ماهانه 4005           
            //8 کارکردخالص ساعتي ماهانه
            //5 کارکردخالص روزانه ماهانه
            //3026 غيبت ساعتی مجاز ماهانه
            GetLog(MyRule, " Before Execute State:", 3034, 4005, 8, 4005);
            int t = this.DoConcept(3034).Value;
            if (this.DoConcept(4005).Value > 0 && t > 0)
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
            GetLog(MyRule, " After Execute State:", 3034, 4005, 8, 4005);
        }

        #endregion

        #region قوانين کارکرد

        public override void R33(AssignedRule MyRule)
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
                GetLog(MyRule, " Before Execute State:", 8, 10, 3034, 4005, 4006, 4010);          

                int hozour = this.DoConcept(9).Value;

                int monthlyLazem = MyRule[this.RuleCalculateDate.Month.ToString() + "th", this.RuleCalculateDate].ToInt();
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
                GetLog(MyRule, " After Execute State:", 8, 10, 3034, 4005, 4006, 4010);
            }

            this.DoConcept(3020).Value = 0;
            ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();
            ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();
            ((PairableScndCnpValue)this.DoConcept(4007)).ClearPairs();
            ((PairableScndCnpValue)this.DoConcept(4008)).ClearPairs();
            ((PairableScndCnpValue)this.DoConcept(4003)).ClearPairs();
        }

        /// <summary></summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي دویست و هشتاد و یک-280 درنظر گرفته شده است</remarks>
        ///  کارکرد روز جمعه
        ///  در نیمه اول سال تا ساعت 13(پایان شیفت) و در نیمه دوم تا ساعت 12 حضور داشت
        ///  هشت ساعت کارکرد (آقای گرامی) منظور گردد
        public void R283(AssignedRule MyRule)
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
                GetLog(MyRule, " Before Execute State:", 2, 4, 3028, 4002, 4003);
                PersianDateTime pd = Utility.ToPersianDateTime(this.RuleCalculateDate);
                if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0
                    && this.DoConcept(3004).Value == 0 && this.DoConcept(1).Value > 0)
                {
                    bool canApply = false;
                    if (pd.Month <= 6) //نیمه اول سال
                    {
                        //در نیمه اول سال حضور تا ساعت 13 یعنی حضور تا پایان شیفت
                        if (Operation.Intersect(this.DoConcept(3028), new PairableScndCnpValuePair(MyRule["First"].ToInt() - 1, MyRule["First"].ToInt())).Value == 0)                            
                        {
                            canApply = true;                           
                        }                     
                    }
                    else//نیمه دوم سال
                    {
                        if (Operation.Intersect(this.DoConcept(3028), new PairableScndCnpValuePair(MyRule["Second"].ToInt() - 1, MyRule["Second"].ToInt())).Value == 0)
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
                GetLog(MyRule, " After Execute State:", 2, 4, 3028, 4002, 4003);
            }
        }

        /// <summary>
        /// فلات - مجموع کارکرد لازم در نیمه اول سال 198:25 و در نیمه دوم 192 میباشد
        /// نیمه اول و دوم بر اساس تاریخ انتساب پارامتر مشخص میگردد
        /// </summary>
        /// <param name="MyRule"></param>
        public void R294(AssignedRule MyRule)
        {
            //10 کارکرد لازم ماهانه           

            GetLog(MyRule, " Before Execute State:", 10);

            int lazem = MyRule["First", this.RuleCalculateDate].ToInt();

            this.DoConcept(10).Value = lazem;

            GetLog(MyRule, " After Execute State:", 10);
        }

        /// <summary></summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي دویست و هشتاد و یک-280 درنظر گرفته شده است</remarks>
        ///  کارکرد در تعطیلات خاص
        ///  اگر حداقل تا 12 ظهر حضورداشت تعلق گیرد
        public void R284(AssignedRule MyRule)
        {
            //7 کارکرد در روز
            //51 تعطیلات خاص مانند عید قربان و ... 
            //20 حضور تعطیلات خاص
            //4002 اضافه کار مجاز
            //3006 غیبت روزانه

            if (this.EngEnvironment.HasCalendar(this.RuleCalculateDate, "51"))
            {
                GetLog(MyRule, " Before Execute State:", 3028, 2, 4, 3004, 20);

                if (this.DoConcept(4002).Value == 0)
                {
                    this.DoConcept(4).Value = 1;
                    this.DoConcept(2).Value = this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues; //this.DoConcept(7).Value;
                }
                this.DoConcept(3004).Value = 0;
                if (this.DoConcept(1).Value > 0)
                {
                    ProceedTraffic ProceedTraffic = this.Person.GetProceedTraficByDate(this.RuleCalculateDate);
                    int aftermidDayCount = ProceedTraffic.Pairs.OrderBy(x => x.To).Where(x => x.IsFilled && x.To > 12 * HourMin).Count();
                    if (aftermidDayCount > 0)
                    {
                        this.DoConcept(20).Value = 1;
                    }
                }
                //اضافه کار باید تعلق بگیرد
                //((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();
                this.ReCalculate(13);
                GetLog(MyRule, " After Execute State:", 3028, 2, 4, 3004, 20);
            }

        }

        #endregion

        #region قوانين مرخصي

        public override void R79(AssignedRule MyRule)
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
            GetLog(MyRule, " Before Execute State:", 4, 2);

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
            GetLog(MyRule, " After Execute State:", 4, 2);
        }

        /// <summary>
        /// اعمال رست
        /// </summary>
        /// <param name="MyRule"></param>
        public void R290(AssignedRule MyRule)
        {
            //202 کد پیشکارت مجوز رست
            //1501 رست

            GetLog(MyRule, " Before Execute State:", 13);
            this.DoConcept(1501);
            this.ReCalculate(13);
            GetLog(MyRule, " After Execute State:", 13);

        }

        /// <summary>
        /// در صورتی که کمتر از 7 روز رست استفاده کند از کارکرد ماهانه کسرمیگردد
        /// </summary>
        /// <param name="MyRule"></param>
        public void R291(AssignedRule MyRule)
        {
            //1501 رست
            //1502 رست ماهانه
            //3 :کارکرد ناخالص  ماهانه
            //7 کارکرد در روز
            //8 کارکردخالص ساعتي ماهانه  

            //مطمئن شویم آخر ماهه
            if (this.DoConcept(10).Value > 0 && this.DoConcept(1502).Value < 7)
            {
                GetLog(MyRule, " Before Execute State:", 1502, 3, 7);
                int diff = 7 - this.DoConcept(1502).Value;
                this.DoConcept(8).Value -= diff * this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues; // this.DoConcept(7).Value;
                this.DoConcept(3).Value -= diff * this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues; //this.DoConcept(7).Value;
                this.DoConcept(10).Value -= diff * this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues; //this.DoConcept(7).Value;
                GetLog(MyRule, " After Execute State:", 1502, 3, 7);
            }
        }

        /// <summary>
        /// در صورتی که بیشتر از 7 روز رست استفاده کند به کارکرد ماهانه اضافه میگردد
        /// </summary>
        /// <param name="MyRule"></param>
        public void R292(AssignedRule MyRule)
        {
            //1501 رست
            //1502 رست ماهانه
            //3 :کارکرد ناخالص  ماهانه     
            //7 کارکرد در روز
            //8 کارکردخالص ساعتي ماهانه
            //10 کارکردلازم ماهانه
            if (this.DoConcept(1502).Value > 7)
            {
                GetLog(MyRule, " Before Execute State:", 1502, 3, 7);
                int diff = this.DoConcept(1502).Value - 7;
                this.DoConcept(8).Value += diff * this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues; // this.DoConcept(7).Value;
                this.DoConcept(3).Value += diff * this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues; //this.DoConcept(7).Value;
                this.DoConcept(10).Value += diff * this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues; //this.DoConcept(7).Value;
                GetLog(MyRule, " After Execute State:", 1502, 3, 7);
            }
        }

        /// <summary>
        /// اعمال خروج روز قبل و ورود روز بعد از رست  
        /// آولویت آن از 100011 به 1801  منتقل شد تا قبل قانون 13 انجام شود
        /// </summary>
        /// <param name="MyRule"></param>
        public void R293(AssignedRule MyRule)
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
            GetLog(MyRule, " Before Execute State:", 4028, 2, 4, 3028, 3004);
            if ((this.DoConcept(1501).Value == 1 || this.DoConcept(1005).Value == 1)
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

            if ((this.DoConcept(1501, this.RuleCalculateDate.AddDays(-1)).Value == 1||
                this.DoConcept(1005, this.RuleCalculateDate.AddDays(-1)).Value == 1)
                && this.DoConcept(1501).Value == 0 && this.DoConcept(1005).Value == 0)
            {
                ProceedTraffic ProceedTraffic = this.Person.GetProceedTraficByDate(this.RuleCalculateDate);
                if (ProceedTraffic != null && ProceedTraffic.PairCount > 0)
                {
                    if (Operation.Intersect(
                       this.DoConcept(3028),this.DoConcept(3029)).Value > 0)
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
            }

            GetLog(MyRule, " After Execute State:", 4028, 2, 4, 3028, 3004);
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
            GetLog(MyRule, " Before Execute State:", 2, 4, 13, 2001, 4002);
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
            GetLog(MyRule, " After Execute State:", 2, 4, 13, 2001, 4002);
        }

        /// <summary>
        /// ماموریت روزانه در روز پنج شنبه ... برابر شود
        /// </summary>
        /// <param name="MyRule"></param>
        public void R143(AssignedRule MyRule)
        {
            // ماموریت روزانه در روزهای پنج شنبه با ضریب
            // First
            // محاسبه شود

            var conceptIds = new[]
            {
                2005, // مجموع ماموريت روزانه
            };

            GetLog(MyRule, " Before Execute State:", conceptIds);

            if (
                this.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday &&
                this.DoConcept(2005).Value > 0
                )
            {
                var coEfficient = (MyRule["First", this.RuleCalculateDate].ToInt() / 100f);
                var temp = (int)Math.Round((this.DoConcept(2005).Value * coEfficient));

                this.DoConcept(2005).Value += temp;
            }

            GetLog(MyRule, " After Execute State:", conceptIds);

        }

        /// <summary>
        /// حضور اول وقت قبل از ماموریت ساعتی بعد از ساعت 10 اجباری است
        /// </summary>
        /// <param name="MyRule"></param>
        public void R286(AssignedRule MyRule)
        {
            //2 کارکرد خالص
            //ماموريت ساعتي 2004
            //3021 تاخیر مجاز 
            //3029 تاخیر غیر مجاز
            GetLog(MyRule, " Before Execute State:", 2004, 2, 13, 3020, 3028);
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
            GetLog(MyRule, " After Execute State:", 2004, 2, 13, 3020, 3028);

        }

        /// <summary>
        /// اعمال مجوز تاخیر مجاز بعد از ماموریت ساعت 23
        /// </summary>
        /// <param name="MyRule"></param>
        public void R287(AssignedRule MyRule)
        {
            //3021 تاخیر مجاز
            //تاخیر ساعتي 3029
            //201 کد پیشکارت درخواست تاخیر مجاز ماموریت
            GetLog(MyRule, " Before Execute State:", 3020, 3021, 3029);
            if (this.DoConcept(3029).Value > 0)
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
            }

            GetLog(MyRule, " After Execute State:", 3020, 3021, 3029);

        }

        #endregion

        #region قوانين کم کاري

        /// <summary>قانون کم کاري 22-2</summary>
        /// <remarks>شناسه اين قانون در تعاريف بعدي شصت و هفت-67 درنظر گرفته شده است</remarks>
        /// کم کاري 22-2: روش تبديل ساعات غيبت به روزانه: هر .... معادل يک روز غيبت 
        public void R67(AssignedRule MyRule)
        {
            //3004 غيبت روزانه
            //غيبت ساعتي غيرمجازي 3028
            //1 حضور

            //1002 مرخصي خالص استحقاقي ساعتي 
            //1056 مرخصی بی حقوق ساعتی 12
            //2002 ماموريت خالص ساعتي
            //581 مجموع مرخصی بی حقوق روزانه

            GetLog(MyRule, " Before Execute State:", 3028, 3004, 1002, 2002);
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
            GetLog(MyRule, " After Execute State:", 3028, 3004, 1002, 2002);
        }

        #endregion

        #region قوانين اضافه کاري

        /// <summary>حداکثر سقف اضافه کار تعطیل در روز</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين قانون در تعاريف يکصد و پنجاه و هشت-158 درنظر گرفته شده است</remarks>
        public void R209(AssignedRule MyRule)
        {
            GetLog(MyRule, " Before Execute State:", 4501, 4002, 4003, 13);

            //4015 اضافه کار با مجوز باشد
            //4501 اضافه کارساعتی تعطیل_روز غیرکاری، تعطیل رسمی و غیررسمی
            //4002 اضافه کار ساعتي مجاز
            //4003 اضافه کار ساعتي غيرمجاز

            if (this.DoConcept(4015).Value == 1)
                return;
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0
                && this.DoConcept(4501).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                ((PairableScndCnpValue)this.DoConcept(4003)).AssignValue(this.DoConcept(4501).Value - MyRule["First", this.RuleCalculateDate].ToInt());
                ((PairableScndCnpValue)this.DoConcept(4002)).DecreasePairFromLast(this.DoConcept(4501).Value - MyRule["First", this.RuleCalculateDate].ToInt());
                ((PairableScndCnpValue)this.DoConcept(4501)).DecreasePairFromLast(this.DoConcept(4501).Value - MyRule["First", this.RuleCalculateDate].ToInt());
                this.ReCalculate(13);
            }
            GetLog(MyRule, " After Execute State:", 4501, 4002, 4003, 13);
        }

        /// <summary>
        /// سقف اضافه کار مجاز در نیمه اول و دوم سال
        /// </summary>
        /// <param name="MyRule"></param>
        public void R288(AssignedRule MyRule)
        {           
            //4005 اضافه کار مجاز ماهانه
            //اضافه کار غیر مجاز ماهانه  4006           
            //3 کارکرئناخالص ماهانه
            GetLog(MyRule, " Before Execute State:", 4005, 4006);
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

            GetLog(MyRule, " After Execute State:", 4005, 4006);

        }

        /// <summary>
        /// محاسبه اضافه کار بر مبنای حضور و کارکرد لازم
        /// </summary>
        /// <param name="MyRule"></param>
        public void R289(AssignedRule MyRule)
        {
            //10 کارکرد لازم ماهانه
            //9 حضور ماهانه
            //4005 اضافه کار مجاز ماهانه
            //اضافه کار غیر مجاز ماهانه  4006           
            GetLog(MyRule, " Before Execute State:", 9, 10, 4005, 4006);
            this.DoConcept(4005).Value = this.DoConcept(9).Value - this.DoConcept(10).Value;
            if (this.DoConcept(4005).Value < 0)
                this.DoConcept(4005).Value = 0;
            this.DoConcept(4006).Value = 0;
            GetLog(MyRule, " After Execute State:", 9, 10, 4005, 4006);

        }

        #endregion

        #region قوانين عمومي

        /// <summary>
        /// وظیفه اجرای مفاهیم ماهانه
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R97(AssignedRule MyRule)
        {
            this.DoConcept(9);//حضورماهانه
            this.DoConcept(4005);//اضافه کارساعتي مجاز ماهانه
            this.DoConcept(4006);//اضافه کارساعتي غیرمجاز ماهانه
            this.DoConcept(4020);//اضافه کار بین وقت ماهانه

            this.DoConcept(3026);//غيبت ساعتي مجاز ماهانه
            this.DoConcept(3034);//غیبت ساعتی غیرمجاز ماهانه
            this.DoConcept(1006);//مرخصي استحقاقي روزانه ماهانه
            this.DoConcept(1011);//مرخصي استحقاقي ساعتي ماهانه
            this.DoConcept(1074);//مرخصي بي حقوق ساعتي ماهانه
            this.DoConcept(1076);//مرخصي بي حقوق روزانه ماهانه
            this.DoConcept(1016);//مرخصی استعلاجی ساعتی ماهانه
            this.DoConcept(3005);//غيبت روزانه ماهانه


            this.DoConcept(8);//کارکردخالص ساعتي ماهانه
            this.DoConcept(5);//کارکردخالص روزانه ماهانه
            this.DoConcept(2007);//ماموريت ساعتي ماهانه
            this.DoConcept(2006);//ماموريت روزانه ماهانه
            this.DoConcept(3);//کارکردناخالص ماهانه
            this.DoConcept(10);//کارکردلازم ماهانه
            this.DoConcept(1017);//مرخصی استعلاجی ماهانه
            this.DoConcept(5013);//کد وضعیت روز جهت رنگ ماهانه
            this.DoConcept(5015);//کارکرد لازم برای حق غذا ماهانه
            this.DoConcept(5016);//طول دوره محدوده محاسبات ماهانه

            this.DoConcept(2035);//ماموریت خارجی ماهانه
            this.DoConcept(1097);// مجموع مرخصی با حقوق روزانه ماهانه

            this.DoConcept(22);//حضور در تعطیلات خاص ماهانه
            this.DoConcept(21);//حضور تعطیلات خاص ماهانه
            this.DoConcept(1502);//رست

        }

        /// <summary>
        /// وظیفه اجرای مفاهیم روزانه
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R98(AssignedRule MyRule)
        {
            this.DoConcept(5008);//نوع
            this.DoConcept(1);//مفهوم حضور
            this.DoConcept(2);//کارکردخالص ساعتي
            this.DoConcept(4);//کارکردخالص روزانه
            this.DoConcept(13);//کارکردناخالص
            this.DoConcept(6);//کارکردلازم
            this.DoConcept(1003);//مرخصي استحقاقي ساعتي
            this.DoConcept(1005);//مرخصي استحقاقي روزانه
            this.DoConcept(1056);//مرخصي بی حقوق ساعتی 12
            this.DoConcept(1066);//مرخصي بی حقوق روزانه 32
            this.DoConcept(2023);//ماموريت ساعتي
            this.DoConcept(2501);//ماموریت ساعتی تعطیل_روز غیرکاری، تعطیل رسمی و غیررسمی
            this.DoConcept(2502);//ماموریت روزانه تعطیل_روز غیرکاری، تعطیل رسمی و غیررسمی
            this.DoConcept(2005);//ماموريت روزانه

            this.DoConcept(3004);//غيبت روزانه
            this.DoConcept(1008);//مرخصی استعلاجی ساعتی
            this.DoConcept(2008);//ماموريت خالص شبانه روزي
            this.DoConcept(1010);//مرخصي استعلاجي روزانه

            this.DoConcept(4007);//اضافه کارآخروقت
            this.DoConcept(4017);//اضافه کار مجاز کارتی
            this.DoConcept(4501);//اضافه کارساعتی تعطیل_روز غیرکاری، تعطیل رسمی و غیررسمی

            this.DoConcept(5010);//ثبت دستی تردد
            //this.DoConcept(164);//خنثی کردن غیبت توسط مرخصی
            //this.DoConcept(165);//تاخیر سرویس
            this.DoConcept(5009);//نوع روز
            this.DoConcept(5012);//کد وضعیت روز جهت رنگ
            this.DoConcept(5014);//کارکرد لازم برای حق غذا
            this.DoConcept(5017);//حضور منهای اضافه کار در گانت چارت استفاده می گردد

            this.DoConcept(2026);//ماموریت خارج از کشور
            this.DoConcept(1096);// مجموعه مرخصی با حقوق روزانه

            this.DoConcept(1501);//رست

        }

        #endregion

        #region Concept Init

        #endregion


        #endregion
    }
}