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

        /// <summary>
        /// کد 2005 : مجموع کارکرد لازم هر ماه در داخل شیفت ها  طبق جدول محاسبه شود
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R2005(AssignedRule MyRule)
        {
           //11 مفهوم تعداد روز
            //کارکردلازم ماهانه 10
            //کارکردخالص ساعتي ماهانه 8
            //3 کارکردناخالص ماهانه
            //غیبت ساعتی مجاز ماهانه 3026
            //3034 غیبت ساعتی غیرمجاز ماهانه
            //3005 غيبت خالص روزانه ماهانه
            //4005 اضافه کارساعتي مجاز ماهانه
            //4006 اضافه کارساعتي غیرمجاز ماهانه
            //4018  حداکثر اضافه کار مجاز ماهانه
            //9 حضورماهانه           
            if (this.DoConcept(9).Value > 0)
            {
                GetLog(MyRule, DebugRuleState.Before, 3034, 4005,10);
                int daterangeORder = this.GetDateRange(3, this.ConceptCalculateDate).DateRangeOrder;
                int lazem = MyRule[daterangeORder.ToString() + "th", this.RuleCalculateDate].ToInt() * HourMin;
                this.DoConcept(10).Value = lazem;
                this.DoConcept(3034).Value = 1;
                this.DoConcept(4005).Value = 1;

                if (lazem < this.DoConcept(3).Value)
                {
                    this.DoConcept(4005).Value = this.DoConcept(3).Value - lazem;
                    this.DoConcept(3034).Value = 1;
                    this.DoConcept(10).Value = lazem;
                }

                else
                {
                    this.DoConcept(3034).Value = 0;
                    this.DoConcept(3034).Value = lazem - this.DoConcept(3).Value;
                    this.DoConcept(10).Value = lazem;
                    this.DoConcept(4005).Value = 1;
                }

                GetLog(MyRule, DebugRuleState.After , 3034, 4005,10);
            }
        
        }

        /// <summary>
        /// کد 2007 : مجموع کارکرد ماهانه در داخل شیفتها معادل با .... ساعت باشد و ایام کاری مهم است
        /// </summary>
        /// <param name="MyRule"></param>
        public override  void R2007(AssignedRule MyRule)
        {
            //11 مفهوم تعداد روز
            //کارکردلازم ماهانه 10
            //کارکردخالص ساعتي ماهانه 8
            //3 کارکردناخالص ماهانه

            //غیبت ساعتی مجاز ماهانه 3026
            //3034 غیبت ساعتی غیرمجاز ماهانه
            //3005 غيبت خالص روزانه ماهانه

            //4005 اضافه کارساعتي مجاز ماهانه
            //4006 اضافه کارساعتي غیرمجاز ماهانه
            //4018  حداکثر اضافه کار مجاز ماهانه

            //9 حضورماهانه

            GetLog(MyRule, DebugRuleState.Before, 10, 4005);

            int t2 = MyRule["First", this.RuleCalculateDate].ToInt();

            //ذوالفقاری
            if (t2 < this.DoConcept(3).Value)
            {
                this.DoConcept(4005).Value = this.DoConcept(3).Value - t2;
            }
            this.DoConcept(10).Value = t2;

            //نجاری
            //if (t2 > this.DoConcept(10).Value)
            //{
            //    int tmp = t2 - this.DoConcept(10).Value;
            //    if (this.DoConcept(4005).Value > tmp)
            //    {
            //        this.DoConcept(4005).Value -= tmp;
            //        this.DoConcept(8).Value += tmp;
            //    }
            //    else
            //    {
            //        this.DoConcept(8).Value += this.DoConcept(4005).Value;
            //        this.DoConcept(3034).Value += tmp - this.DoConcept(4005).Value;
            //        this.DoConcept(4005).Value = 1;
            //    }
            //}
            //else
            //{
            //    if (this.DoConcept(8).Value > t2)
            //    {
            //        this.DoConcept(3034).Value = this.DoConcept(10).Value - this.DoConcept(3034).Value - this.DoConcept(8).Value;
            //        if (this.DoConcept(3034).Value < 0)
            //        {
            //            this.DoConcept(3034).Value = 1;
            //        }
            //        this.DoConcept(4005).Value =this.DoConcept(4005).Value+ this.DoConcept(8).Value - t2;
            //        this.DoConcept(8).Value = t2;

            //    }
            //    else
            //    {
            //        this.DoConcept(3034).Value = this.DoConcept(10).Value - this.DoConcept(3034).Value - t2;
            //        if (this.DoConcept(3034).Value < 0)
            //        {
            //            this.DoConcept(3034).Value = 1;
            //        }
            //        this.DoConcept(4005).Value = this.DoConcept(3).Value - this.DoConcept(8).Value;
            //    }
            //}
            //this.DoConcept(10).Value = t2;


            //منصوری
            //if (this.DoConcept(4005).Value > this.DoConcept(4018).Value)
            //{
            //    this.DoConcept(3).Value -= this.DoConcept(4005).Value + this.DoConcept(4018).Value;
            //    this.DoConcept(4006).Value += this.DoConcept(4005).Value - this.DoConcept(4018).Value;
            //    this.DoConcept(4005).Value = this.DoConcept(4018).Value;
            //}
            //int movazafi = this.DoConcept(10).Value;
            //this.DoConcept(10).Value = MyRule["First", this.RuleCalculateDate].ToInt(); ;
            //if (movazafi > this.DoConcept(10).Value)
            //{
            //    int tmp = movazafi - this.DoConcept(10).Value;
            //    if (this.DoConcept(3034).Value > tmp)
            //    {
            //        this.DoConcept(3034).Value -= tmp;
            //        this.DoConcept(3).Value = this.DoConcept(4005).Value + this.DoConcept(3).Value;
            //    }
            //    else
            //    {
            //        this.DoConcept(3034).Value = 1;
            //        this.DoConcept(4005).Value = this.DoConcept(3).Value - this.DoConcept(10).Value;
            //        this.DoConcept(8).Value = this.DoConcept(10).Value;
            //        this.DoConcept(3).Value += this.DoConcept(4005).Value;
            //    }

            //}
            //else
            //{
            //    if ((this.DoConcept(3034).Value > 0 || (this.DoConcept(3034).Value == 0 && this.DoConcept(4005).Value > 0)) && this.DoConcept(10).Value > this.DoConcept(3).Value)
            //    {
            //        this.DoConcept(3034).Value = this.DoConcept(10).Value - this.DoConcept(3).Value;
            //        this.DoConcept(4005).Value = 1;
            //        //this.DoConcept(8).Value = t2;
            //        this.DoConcept(8).Value = this.DoConcept(3).Value;

            //    }

            //    else if (this.DoConcept(4005).Value > 0 && this.DoConcept(3034).Value == 0 && this.DoConcept(10).Value > this.DoConcept(3).Value)
            //    {
            //        this.DoConcept(3034).Value = this.DoConcept(10).Value - this.DoConcept(3).Value;
            //    }

            //    else if (this.DoConcept(4005).Value > 0 && this.DoConcept(3034).Value == 0 && this.DoConcept(10).Value < this.DoConcept(3).Value)
            //    {
            //        this.DoConcept(3034).Value = 1;
            //        this.DoConcept(4005).Value = this.DoConcept(3).Value - this.DoConcept(10).Value;
            //        this.DoConcept(8).Value = this.DoConcept(10).Value;
            //    }

            //}


            GetLog(MyRule, DebugRuleState.After, 10, 4005);
        }

        /// <summary>
        /// اعمال کارکرد در روزهایی که شیفت ندارد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual  void R2501(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4,5);
            if (
               this.Person.GetShiftByDate(this.RuleCalculateDate).Value== 0)
            {
                this.DoConcept(4).Value = 1;
                this.ReCalculate(5);
               
            }
            GetLog(MyRule, DebugRuleState.After , 4,5);
        }

        #endregion

        #region قوانين مرخصي
        /// <summary>
        ///  مرخصی استعلاجی بیش از سه روز
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3501(AssignedRule MyRule)
        {

            //1072 مرخصی بی حقوق 35
            //ماموریت شبانه روز 75
            //1091 مجموع مرخصی بی حقوق روزانه
            //1110مجموع انواع مرخصی بی حقوق روزانه در روز
            // مرخصی استعلاجی بیش از سه روز
            GetLog(MyRule, DebugRuleState.Before, 1072, 2045, 1091, 1110);
            if (this.DoConcept(1072).Value > 0)
            {
                this.DoConcept(2045).Value = this.DoConcept(1072).Value;
                this.DoConcept(1091).Value -= this.DoConcept(1072).Value;
                this.DoConcept(1110).Value -= this.DoConcept(1072).Value;
                this.DoConcept(1072).Value = 0;


            }

            GetLog(MyRule, DebugRuleState.After , 1072, 2045, 1091, 1110);
        }

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

            if (MyRule.HasParameter("First", this.RuleCalculateDate))
            {
                if (MyRule["First", this.RuleCalculateDate].ToInt() ==1)
                {
                    GetLog(MyRule, DebugRuleState.Before,2,4, 13);
                    if (!EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2", "4") ||
                       this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.Count > 0)
                    {
                        // پرسنل شیفتی ،روزی که مرخصی رفتن به اندازه کارکرد خالص به کارکرد ناخالص اضافه شود
                       
                        if (this.DoConcept(1090).Value >= 1)
                        {
                            this.DoConcept(13).Value += this.DoConcept(1090).Value * this.DoConcept(2).Value;
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

                        //this.DoConcept(13).Value += value;

                    }
                    else //مرخصی به کارکرد خالص اضافه شده و حالا کافی است کارکرد ناخالص دوباره محاسبه شود
                    {
                        this.DoConcept(13).Value = this.DoConcept(2).Value + this.DoConcept(4002).Value;
                    }
                    if (this.DoConcept(1090).Value >= 1 && this.DoConcept(4).Value == 0)
                    {
                        this.DoConcept(4).Value = 1;//مانا آرد
                    }
                   
                }

                else if (MyRule["First", this.RuleCalculateDate].ToInt() > 0)
                {
                    if ((!EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2", "4") ||
                      this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.Count > 0)&& this.DoConcept (1090).Value >0)
                    {
                        this.DoConcept(2).Value = MyRule["First", this.RuleCalculateDate].ToInt();
                        this.DoConcept(13).Value = this.DoConcept(2).Value;
                    }
                }

                GetLog(MyRule, DebugRuleState.Before, 2, 4, 13);
            }


        }


        #endregion

        #region قوانين ماموريت

        #endregion

        #region قوانين کم کاري

        #endregion

        #region قوانين اضافه کاري

        /// <summary>
        /// کد 6501 :  افزودن مقدار.....ساعت به اضافه کار مجاز با توجه به پیش کارت
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6501(AssignedRule MyRule)
        {
          
            // 4002اضافه کارساعتی مجاز
            // افزودن اضافه کار به اندازه..... به اضافه کار مجاز با توجه به پیش کارت 
            GetLog(MyRule, DebugRuleState.Before,   4002,4005,13);
            Permit permit= this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(152));
            if (permit != null && permit.Value > 0)
            {
                if (MyRule["First", this.RuleCalculateDate].ToInt() >0)
                {
                    ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(MyRule["First", this.RuleCalculateDate].ToInt());
                    this.DoConcept(13).Value = this.DoConcept(2).Value + this.DoConcept(4002).Value;
                }
            }
            this.ReCalculate(4005);
            GetLog(MyRule, DebugRuleState.After , 4002,4005,13);
        }

        /// <summary>
        /// کد 6502 :  اضافه کار بین شیفت
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6502(AssignedRule MyRule)
        {
            // 4002اضافه کارساعتی مجاز
          
            GetLog(MyRule, DebugRuleState.Before, 4002);
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0
                && this.DoConcept(4002).Value > 0)
            {
                this.DoConcept(4002).Value = 0;
                ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();
            }
            //var conceptList = new[] { 3028, 4002,4003,1003,1056,1008,3008,3029,3010,3030,2004,2013, 
            //                          5002,17,1053,1023,1038,1024,1039,1041,1026,2016,2017,2018,2019,
            //                          2020,2021,2022,2023,2014,4007,4009,3015,4008,1025,1027,1040,1042,
            //                          1054,4014,4012,1057,1058,1059,1060,1061,1062,1078,4011,4023,3040,
            //                          3042,3044,4030,4034,4035,4038,4040,4042,4044,4046,5038,4048,2060};
            //foreach (int calcDate in conceptList)
            //{
            //    if (this.DoConcept(calcDate).Value > 0)
            //    {
            //        int tt3028_rem;
            //        Decimal tt3028 = Math.Truncate(this.DoConcept(calcDate).Value / 60m);
            //        if (tt3028 > 0)
            //        {
            //            tt3028 = tt3028 * 60;
            //        }
            //        tt3028_rem = this.DoConcept(calcDate).Value;
            //        if (this.DoConcept(calcDate).Value > 60)
            //        {
            //            tt3028_rem = this.DoConcept(calcDate).Value % 60;
            //        }
                   
            //        if (tt3028_rem >= 16 && tt3028_rem <= 45)
            //        {
            //            tt3028 += 30;
            //        }
            //        else if (tt3028_rem >= 46 && tt3028_rem <= 59)
            //        {
            //            tt3028 += 60;
            //        }
            //        this.DoConcept(calcDate).Value = Convert.ToInt32(tt3028);
            //    }
            //}

                    GetLog(MyRule, DebugRuleState.After, 4002);

        }

        /// <summary>
        /// اضافه کار روزهای غیر جمعه
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6503(AssignedRule MyRule)
        {
            //4002 اضافه کارساعتی 
            //4046 اضافه کار ساعتی جمعه 
            GetLog(MyRule, DebugRuleState.Before,4046, 4002);
            if (this.DoConcept(4002).Value > 0 && (this.DoConcept(4046).Value > 0 || this.DoConcept(4048).Value > 0))
            {
                this.DoConcept(4002).Value = 0;
                ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();
            }
            GetLog(MyRule, DebugRuleState.After, 4046, 4002);

        }

        /// <summary>
        /// کد 6032 :   اعمال مجوز اضافه کاری
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R6032(AssignedRule MyRule)
        {
            //4002 اضافه کارساعتي مجاز
            //4003 اضافه کارساعتي غیرمجاز       
            //4015 اضافه کار با مجوز باشد
            //4016 مفهوم اضافه کار بعد از وقت مجوزی است
            //4029 مفهوم مجوز اضافه کاری
            //4007 مفهوم اضافه کارآخر وقت            
            //4026 سقف اضافه کار که ضریب تعلق میگیرد

            Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate , EngEnvironment.GetPrecard(Precards.OverTime));
            Permit permitFriday = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(151));
            GetLog(MyRule, DebugRuleState.Before, 4007, 4002, 4005, 4003, 13);

            #region Apply Parameters
            int withoutPermitAlow = 0;
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)
                withoutPermitAlow = MyRule["MojazTatil", this.RuleCalculateDate].ToInt();
            else
                withoutPermitAlow = MyRule["MojazAadi", this.RuleCalculateDate].ToInt();
            if (withoutPermitAlow > 0)
            {
                foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(4003)).Pairs)
                {
                    if (pair.Value - withoutPermitAlow > 0)
                    {
                        IPair allowedPair = new PairableScndCnpValuePair(pair.From, pair.From + withoutPermitAlow);

                        ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(allowedPair);
                        this.DoConcept(13).Value += allowedPair.Value; ;

                        pair.From += withoutPermitAlow;
                        this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;

                        break;
                    }
                    else if (pair.Value - withoutPermitAlow == 0)
                    {
                        ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(pair);
                        this.DoConcept(13).Value += pair.Value;

                        pair.From = pair.To = 0;
                        this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;

                        break;
                    }
                    else
                    {
                        ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(pair);
                        this.DoConcept(13).Value += pair.Value;

                        withoutPermitAlow -= pair.Value;

                        pair.From = pair.To = 0;
                        this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;
                    }
                }
            }
            #endregion

            if (permit != null && permit.Value > 0)
            {
                if (this.DoConcept(4003).Value > 0)
                {
                    foreach (PermitPair permitPair in permit.Pairs)
                    {
                        //مجوز مقداری و بازه ای - در حال حاضر نمیتوان روی خصیصه جفت بودن در مجوز حساب کرد
                        //لذا از روش زیر جهت شناسایی استفاده میگردد
                        #region Pairly Permit
                        if (permitPair.To - permitPair.From == permitPair.Value)
                        {
                            PairableScndCnpValue allowedOverWork = Operation.Intersect(permitPair, (PairableScndCnpValue)this.DoConcept(4003));
                            PairableScndCnpValue notAllowedOverWork = Operation.Differance(this.DoConcept(4003), allowedOverWork);
                            ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(allowedOverWork);
                            ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(notAllowedOverWork);
                            this.ReCalculate(13);

                            permitPair.IsApplyedOnTraffic = true;//اعمال شد
                        }
                        #endregion

                        #region Value Permit
                        else if ((permitPair.From == 1439 && permitPair.To == 1439 && permitPair.Value > 0) || permitPair.To - permitPair.From != permitPair.Value)
                        {
                            int permitOverWork = permitPair.Value;

                            foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(4003)).Pairs)
                            {
                                if (pair.Value - permitOverWork > 0)
                                {
                                    IPair allowedPair = new PairableScndCnpValuePair(pair.From, pair.From + permitOverWork);

                                    ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(allowedPair);

                                    pair.From += permitOverWork;
                                    this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;

                                    break;
                                }
                                else if (pair.Value - permitOverWork == 0)
                                {
                                    ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(pair);

                                    pair.From = pair.To = 0;
                                    this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;

                                    break;
                                }
                                else
                                {
                                    ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(pair);

                                    permitOverWork -= pair.Value;

                                    pair.From = pair.To = 0;
                                    this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;
                                }
                            }
                            permitPair.IsApplyedOnTraffic = true;//اعمال شد
                        }
                        #endregion
                    }
                   
                }
               
                    
            }
                      
            if (permitFriday != null && permitFriday.Value > 0)
            {
                if (this.DoConcept(4003).Value > 0)
                {
                    foreach (PermitPair permitPair in permitFriday.Pairs)
                    {
                        //مجوز مقداری و بازه ای - در حال حاضر نمیتوان روی خصیصه جفت بودن در مجوز حساب کرد
                        //لذا از روش زیر جهت شناسایی استفاده میگردد
                        #region Pairly permitFriday
                        if (permitPair.To - permitPair.From == permitPair.Value)
                        {
                            PairableScndCnpValue allowedOverWork = Operation.Intersect(permitPair, (PairableScndCnpValue)this.DoConcept(4003));
                            PairableScndCnpValue notAllowedOverWork = Operation.Differance(this.DoConcept(4003), allowedOverWork);
                            ((PairableScndCnpValue)this.DoConcept(4002)).AppendPairs(allowedOverWork);
                            ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(notAllowedOverWork);

                            permitPair.IsApplyedOnTraffic = true;//اعمال شد
                        }
                        #endregion

                        #region Value permitFriday
                        else if ((permitPair.From == 1439 && permitPair.To == 1439 && permitPair.Value > 0) || permitPair.To - permitPair.From != permitPair.Value)
                        {
                            int permitOverWork = permitPair.Value;

                            foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(4003)).Pairs)
                            {
                                if (pair.Value - permitOverWork > 0)
                                {
                                    IPair allowedPair = new PairableScndCnpValuePair(pair.From, pair.From + permitOverWork);

                                    ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(allowedPair);

                                    pair.From += permitOverWork;
                                    this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;

                                    break;
                                }
                                else if (pair.Value - permitOverWork == 0)
                                {
                                    ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(pair);

                                    pair.From = pair.To = 0;
                                    this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;

                                    break;
                                }
                                else
                                {
                                    ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(pair);

                                    permitOverWork -= pair.Value;

                                    pair.From = pair.To = 0;
                                    this.DoConcept(4003).Value = ((PairableScndCnpValue)this.DoConcept(4003)).PairValues;
                                }
                            }
                            permitPair.IsApplyedOnTraffic = true;//اعمال شد
                        }
                        #endregion
                    }
                   
                }
               
            }

            this.DoConcept(13).Value = this.DoConcept(2).Value + this.DoConcept(4002).Value;
            DateRange dateRange1 = this.GetDateRange(4005, this.RuleCalculateDate);
            if (this.RuleCalculateDate == dateRange1.ToDate || CalcDateZone.ToDate == this.RuleCalculateDate)
            {
                this.ReCalculate(4005);
            }
            this.ReCalculate( 4007);
            GetLog(MyRule, DebugRuleState.After , 4007, 4002, 4005, 4003, 13);
        }

        /// <summary>
        /// چنانچه اضافه کار ماهانه به --- ساعت رسید ، --- ساعت به آن اضافه شود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6504(AssignedRule MyRule)
        {
            //4002 اضافه کارساعتی 
            //4046 اضافه کار ساعتی جمعه 
            GetLog(MyRule, DebugRuleState.Before, 4005);
            int t1 = MyRule["First", this.RuleCalculateDate].ToInt();
            int t2 = MyRule["Second", this.RuleCalculateDate].ToInt();
            if (this.DoConcept (4005).Value >=t1)
            {
                this.DoConcept(4005).Value += t2;
            }
            GetLog(MyRule, DebugRuleState.After, 4005);

        }

        #endregion

        #region قوانين عمومي

        /// <summary>
        /// همپوشانی درخواست روزانه و ساعتی مجاز نیست
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R1501(AssignedRule MyRule)
        {
            if (this.DoConcept(2005).Value > 0 || this.DoConcept(2032).Value > 0 || this.DoConcept(2033).Value > 0 || this.DoConcept(2034).Value > 0 || this.DoConcept(2035).Value > 0 || this.DoConcept(1010).Value > 0)

            {
                if (this.DoConcept(2004).Value > 0)
                {
                    GetLog(MyRule, DebugRuleState.Before, 2004,2023,2028);
                    this.DoConcept(2004).Value = 0;
                    ((PairableScndCnpValue)this.DoConcept(2004)).ClearPairs();
                    this.DoConcept(2023).Value = 0;
                    ((PairableScndCnpValue)this.DoConcept(2023)).ClearPairs();
                    this.DoConcept(2028).Value = 0;
                    ((PairableScndCnpValue)this.DoConcept(2028)).ClearPairs();
                    GetLog(MyRule, DebugRuleState.After , 2004, 2023, 2028);
                }

                if (this.DoConcept(2019).Value > 0)
                {
                    GetLog(MyRule, DebugRuleState.Before, 2019,2023,2028);
                    this.DoConcept(2019).Value = 0;
                    ((PairableScndCnpValue)this.DoConcept(2019)).ClearPairs();
                    this.DoConcept(2023).Value = 0;
                    ((PairableScndCnpValue)this.DoConcept(2023)).ClearPairs();
                    this.DoConcept(2028).Value = 0;
                    ((PairableScndCnpValue)this.DoConcept(2028)).ClearPairs();
                    GetLog(MyRule, DebugRuleState.After , 2019, 2023, 2028);
                }
                if (this.DoConcept(1008).Value > 0)
                {
                    GetLog(MyRule, DebugRuleState.Before, 1008);
                    this.DoConcept(1008).Value = 0;
                    ((PairableScndCnpValue)this.DoConcept(1008)).ClearPairs();
                    GetLog(MyRule, DebugRuleState.After , 1008);
                }
                GetLog(MyRule, DebugRuleState.Before, 4002,4007,4008);
                this.DoConcept(4002).Value = 0;
                ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();
                this.DoConcept(4007).Value = 0;
                ((PairableScndCnpValue)this.DoConcept(4007)).ClearPairs();
                this.DoConcept(4008).Value = 0;
                ((PairableScndCnpValue)this.DoConcept(4008)).ClearPairs();
                GetLog(MyRule, DebugRuleState.After , 4002, 4007, 4008);

                if (this.DoConcept(1003).Value > 0)
                {
                    GetLog(MyRule, DebugRuleState.Before, 1003);
                    this.Person.AddUsedLeave(this.RuleCalculateDate, -this.DoConcept(1003).Value, null);
                    ((PairableScndCnpValue)this.DoConcept(1003)).ClearPairs();
                    this.DoConcept(1003).Value = 0;
                    GetLog(MyRule, DebugRuleState.After , 1003);
                }
            }

           
            
        }
        #endregion

        #region Concept Init


        #endregion


        #endregion

    }
}

