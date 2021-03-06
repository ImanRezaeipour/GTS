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

        #endregion

        #region قوانين مرخصي

        #endregion

        #region قوانين ماموريت

        #endregion

        #region قوانين کم کاري

       
        #endregion

        #region قوانين اضافه کاري

        /// <summary>  نیم ساعت آخر</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6565(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4002);
           //ProceedTrafficPair proceedtrafficPair = this.Person.GetProceedTraficAllByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).LastOrDefault();
           // int p = 0;
           // if (proceedtrafficPair!= null)
           //{
           //     p = proceedtrafficPair.To;
           //}
           
           // if (p  != null && p >= 980 && p  <= 1020)
           // {
           //     ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(30);
           //     if (p > 990 && p <= 1020)
           //     {
           //         int s = p - 990;
           //         this.DoConcept(4002).Value  -= s;
           //     }
           // }
            ProceedTrafficPair proceedtrafficPair = this.Person.GetProceedTraficAllByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).LastOrDefault();
            int p = 0;
            if (proceedtrafficPair != null)
            {
                p = proceedtrafficPair.To;
            }
            int fromTime = MyRule["First", this.RuleCalculateDate].ToInt();
            int toTime = MyRule["Second", this.RuleCalculateDate].ToInt();
            int telorance = MyRule["Third", this.RuleCalculateDate].ToInt();

            if (p != null && p >= fromTime && p <= toTime)
            {

                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(telorance);
                if (p > fromTime && p <= toTime)
                {
                    int s = p - fromTime;
                    this.DoConcept(4002).Value -= s;
                }
            }

            GetLog(MyRule, DebugRuleState.After, 4002);

            }
        
        /// <summary>اضافه کار و یا ماموریت در صورتی که تا ساعت 15 باشد</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6570(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4002,13);
            IPair Hozor = ((PairableScndCnpValue)this.DoConcept(1)).Pairs.OrderBy(x => x.To).LastOrDefault();
            IPair Mamoriat = ((PairableScndCnpValue)this.DoConcept(2004)).Pairs.OrderBy(x => x.To).LastOrDefault();
            int shiftStart = this.Person.GetShiftByDate(this.RuleCalculateDate).PastedPairs.From;
            int shiftEnd = this.Person.GetShiftByDate(this.RuleCalculateDate).PastedPairs.To;
            int jam = this.DoConcept(1).Value + this.DoConcept(2004).Value;
            int Telorance = MyRule["first", this.RuleCalculateDate].ToInt();
            int TeloranceEnd = MyRule["Second", this.RuleCalculateDate].ToInt();
           
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).PairCount > 0)
            {
                if (Hozor != null && this.DoConcept (1090).Value ==0)
                {
                    if ((Hozor.To >= (shiftEnd - Telorance) && Hozor.To <= TeloranceEnd) && jam >= MyRule["Third", this.RuleCalculateDate].ToInt())
                    {
                        int kol = TeloranceEnd - Hozor.To;
                        if (kol > TeloranceEnd - shiftEnd)
                        {
                            kol = TeloranceEnd - shiftEnd;
                        }
                        ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(kol);
                    }
                }


                else if (Mamoriat != null &&this.DoConcept(1090).Value == 0)
                {
                    if ((Mamoriat.To >= (shiftEnd - Telorance) && Mamoriat.To <= TeloranceEnd) && jam >= MyRule["Third", this.RuleCalculateDate].ToInt())
                    {
                        int kol = TeloranceEnd - Mamoriat.To;
                        if (kol > TeloranceEnd-shiftEnd )
                        {
                            kol = TeloranceEnd - shiftEnd;
                        }
                        ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(kol);

                    }
                }
               
                this.DoConcept(13).Value = this.DoConcept(2).Value + this.DoConcept(4002).Value;
                GetLog(MyRule, DebugRuleState.After, 4002,13);
        
            }
           }


        /// <summary>سر ریز اضافه کار</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6571(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 6510);
            if (this.DoConcept(4006).Value > 0)
            {
                int kol = this.DoConcept(4006).Value - (this.DoConcept(4036).Value + this.DoConcept(4037).Value) ;
               
                this.DoConcept(6510).Value = kol;
            }
            GetLog(MyRule, DebugRuleState.After, 6510);

        }

        /// <summary> جانبازی</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6599(AssignedRule MyRule)
        {

            var conceptList = new[] { 2,3,13,4005,4006,4007,5501, 3020, 3028, 4002, 3029,3030,3031};
            GetLog(MyRule, DebugRuleState.Before, conceptList);
            //4002   اضافه کارساعتي مجاز
            //3028    غیبت ساعتی غیر مجاز
            //3029    غیبت ساعتی مجاز

            if (
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                this.Person.PersonTASpec.R16 > 0 &&
                !Utility.IsEmpty(this.Person.PersonTASpec.R16Value.ComboValue) &&
                Utility.ToInteger(this.Person.PersonTASpec.R16Value.ComboValue) > 0 &&
                this.Person.PersonTASpec.R16Value.ComboValue.Length.Equals(3)
                )
            {

                var percentMinute = new Dictionary<string, int>
                    {
                        {"25", 45},
                        {"30", 60},
                        {"40", 90},
                        {"50", 120},
                        {"60", 150},
                        {"70", 210}
                    };

                var percent = this.Person.PersonTASpec.R16Value.ComboValue.Substring(0, 2);

                if (percentMinute.ContainsKey(percent))
                {
                    var minutes = percentMinute.FirstOrDefault(x => x.Key == percent).Value;

                    bool doOnOvertime = this.Person.PersonTASpec.R16Value.ComboValue.Substring(2, 1) == "1";

                    if (doOnOvertime)
                    {
                        #region OLD
                        // اعمال روی اضافه کار
                        //((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValueEx(minutes);

                        //var basePair = new BasePair(
                        //      ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).Last().To - minutes,
                        //      ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).Last().To
                        //      ); 
                        #endregion

                        BasePair basePair = null;

                        if (this.DoConcept(4002).Value > 0)
                        {
                            basePair = new BasePair(
                              ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).First().To,
                              ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).First().To + minutes
                              );
                        }
                        else
                        {
                            basePair = new BasePair(
                              this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.To).First().To,
                              this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.To).First().To + minutes
                              );
                        }

                        // قرار شد در غيبت نمايش داده نشود
                        //this.DoConcept(5501).Value += minutes;
                        ((PairableScndCnpValue)this.DoConcept(5501)).AddPair(basePair);

                        this.ReCalculate(3, 13, 4005, 4006, 4007);
                    }
                    else
                    {
                        // اعمال روی غیبت
                        if (this.DoConcept(3028).Value > 0)
                        {
                            foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderByDescending(x => x.From))
                            {
                                if (pair.Value > minutes)
                                {
                                    IPair tempPair = new BasePair(pair.To - minutes, pair.To);

                                    this.DoConcept(3020).Value += tempPair.Value;

                                    var pairableScndCnpValue = Operation.Differance(this.DoConcept(3028), tempPair);

                                    ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(pairableScndCnpValue.Pairs);
                                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(tempPair);

                                    pair.To -= minutes;

                                    break;
                                }

                                if (pair.Value == minutes)
                                {
                                    this.DoConcept(3020).Value += pair.Value;

                                    ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);
                                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                                    pair.From = pair.To = 0;
                                    break;
                                }

                                this.DoConcept(3020).Value += pair.Value;

                                minutes -= pair.Value;

                                ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);
                                ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                                pair.From = pair.To = 0;
                            }
                        }
                    }
                }
                ((PairableScndCnpValue)this.DoConcept(3029)).AddPairs(Operation.Intersect(this.DoConcept(3029), this.DoConcept(3028)));
                ((PairableScndCnpValue)this.DoConcept(3030)).AddPairs(Operation.Intersect(this.DoConcept(3030), this.DoConcept(3028)));
                ((PairableScndCnpValue)this.DoConcept(3031)).AddPairs(Operation.Intersect(this.DoConcept(3031), this.DoConcept(3028)));

            }

            GetLog(MyRule, DebugRuleState.Before, conceptList);
           
        }

        /// <summary> حداقل اضافه کاری قبل از وقت - پارامتر پویا</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6504(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4002,4003,4008, 13);
            var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "MINEGAV", this.RuleCalculateDate);
            if (personParam != null && this.DoConcept(4008).Value < System.Convert.ToInt32(personParam.Value))
            {
                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), this.DoConcept(4008)));
                ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(this.DoConcept(4008));
                ((PairableScndCnpValue)this.DoConcept(4008)).ClearPairs();
                this.ReCalculate(13);
            }

            GetLog(MyRule, DebugRuleState.After, 4002, 4003, 4008, 13);
        }

        /// <summary>  حداقل اضافه کاری بعد از وقت - پارامتر پویا</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6505(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4002,4003,4007, 13);
            var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "MINEBAV", this.RuleCalculateDate);
            if (personParam != null && this.DoConcept(4007).Value < System.Convert.ToInt32(personParam.Value))
            {
                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), this.DoConcept(4007)));
                ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(this.DoConcept(4007));
                ((PairableScndCnpValue)this.DoConcept(4007)).ClearPairs();
                this.ReCalculate(13);
            }


            GetLog(MyRule, DebugRuleState.After, 4002, 4003, 4007, 13);
        }

        /// <summary>  حداکثر اضافه کاری قبل از وقت - پارامتر پویا</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6506(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4002,4003,4008, 13);
            var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "MAXEGAV", this.RuleCalculateDate);
            if (personParam != null && this.DoConcept(4008).Value > System.Convert.ToInt32(personParam.Value))
            {
                int notAllowValue = this.DoConcept(4008).Value - System.Convert.ToInt32(personParam.Value);
                PairableScndCnpValue notAllow = new PairableScndCnpValue();
                foreach (PairableScndCnpValuePair pair in ((PairableScndCnpValue)this.DoConcept(4008)).Pairs.OrderBy(x => x.From))
                {
                    if (pair.Value >= notAllowValue)
                    {
                        IPair notAloowPair = new PairableScndCnpValuePair(pair.From, pair.From + notAllowValue);
                        notAllow.Pairs.Add(notAloowPair);
                        notAllowValue = 0;
                        break;
                    }
                    else
                    {
                        notAllowValue -= pair.Value;
                        notAllow.Pairs.Add(pair);
                    }
                }
                ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(Operation.Intersect(this.DoConcept(4002), notAllow));
                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), notAllow));
                this.ReCalculate(13, 4008);
            }



            GetLog(MyRule, DebugRuleState.After, 4002,4003,4008, 13);
        }

        /// <summary>  کد  حداکثر اضافه کاری بعد از وقت - پارامتر پویا</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6507(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4002, 4003, 4007, 13);
            var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "MAXEBAV", this.RuleCalculateDate);
            if (personParam != null && this.DoConcept(4007).Value > System.Convert.ToInt32(personParam.Value))
            {
                int notAllowValue = this.DoConcept(4007).Value - System.Convert.ToInt32(personParam.Value);
                PairableScndCnpValue notAllow = new PairableScndCnpValue();
                foreach (PairableScndCnpValuePair pair in ((PairableScndCnpValue)this.DoConcept(4007)).Pairs.OrderByDescending(x => x.From))
                {
                    if (pair.Value >= notAllowValue)
                    {
                        IPair notAloowPair = new PairableScndCnpValuePair(pair.To - notAllowValue, pair.To);
                        notAllow.Pairs.Add(notAloowPair);
                        notAllowValue = 0;
                        break;
                    }
                    else
                    {
                        notAllowValue -= pair.Value;
                        notAllow.Pairs.Add(pair);
                    }
                }
                ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(Operation.Intersect(this.DoConcept(4002), notAllow));
                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), notAllow));
                this.ReCalculate(13, 4007);
            }

            GetLog(MyRule, DebugRuleState.After, 4002, 4003, 4007, 13);
        }

        /// <summary>   حداکثر اضافه کاری روز عادی - پارامتر پویا</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6508(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4002, 4003,  13);
            var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "MAXERA", this.RuleCalculateDate);
            if (personParam != null && this.DoConcept(4015).Value == 1)
                return;
            if (personParam != null && this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0
                 && this.DoConcept(4002).Value > System.Convert.ToInt32(personParam.Value))
            {
                int notAllowedValue = this.DoConcept(4002).Value - System.Convert.ToInt32(personParam.Value);
                foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(4002)).Pairs)
                {
                    if (pair.Value - notAllowedValue > 0)
                    {
                        IPair notAllowedPair = new PairableScndCnpValuePair(pair.To - notAllowedValue, pair.To);
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(notAllowedPair);
                        pair.To -= notAllowedValue;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;
                        break;
                    }
                    else if (pair.Value - notAllowedValue == 0)
                    {
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(pair);
                        pair.From = pair.To = 0;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;
                        break;
                    }
                    else
                    {
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(pair);
                        notAllowedValue -= pair.Value;
                        pair.From = pair.To = 0;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;
                    }
                }
                this.ReCalculate(13);
            }

            GetLog(MyRule, DebugRuleState.After, 4002, 4003, 13);
        }

        /// <summary>   حداکثر اضافه کاری روز غیر کاری - پارامتر پویا</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6509(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4002, 4003, 13);
            var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "MAXERG", this.RuleCalculateDate);
            if (personParam != null && this.DoConcept(4015).Value == 1)
                return;
            if (personParam != null && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0
                && this.DoConcept(4002).Value > System.Convert.ToInt32(personParam.Value))
            {
                int notAllowedValue = this.DoConcept(4002).Value - System.Convert.ToInt32(personParam.Value);
                foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(4002)).Pairs)
                {
                    if (pair.Value - notAllowedValue > 0)
                    {
                        IPair notAllowedPair = new PairableScndCnpValuePair(pair.To - notAllowedValue, pair.To);
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(notAllowedPair);
                        pair.To -= notAllowedValue;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;
                        break;
                    }
                    else if (pair.Value - notAllowedValue == 0)
                    {
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(pair);
                        pair.From = pair.To = 0;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;
                        break;
                    }
                    else
                    {
                        ((PairableScndCnpValue)this.DoConcept(4003)).AppendPair(pair);
                        notAllowedValue -= pair.Value;
                        pair.From = pair.To = 0;
                        this.DoConcept(4002).Value = ((PairableScndCnpValue)this.DoConcept(4002)).PairValues;
                    }
                }
                this.ReCalculate(13);
            }

            GetLog(MyRule, DebugRuleState.After, 4002, 4003, 13);
        }

        /// <summary>   حداکثر اضافه کاری ماهانه ساعتی - پارامتر پویا</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6510(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4018,4005,4006, 3);
            var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "MAXEMS", this.RuleCalculateDate);
            this.DoConcept(4018).Value = System.Convert.ToInt32(personParam.Value);
            if (this.DoConcept(4005).Value > System.Convert.ToInt32(personParam.Value))
            {
                this.DoConcept(3).Value -= this.DoConcept(4005).Value - System.Convert.ToInt32(personParam.Value);
                this.DoConcept(4006).Value += this.DoConcept(4005).Value - System.Convert.ToInt32(personParam.Value);
                this.DoConcept(4005).Value = System.Convert.ToInt32(personParam.Value);
            }

            GetLog(MyRule, DebugRuleState.After, 4018, 4005, 4006, 3);
        }

        /// <summary> پنجشنبه اضافه کاری غیر مجاز</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6566(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4002, 4003);
            if (this.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday)
            {
                this.DoConcept(4003).Value = this.DoConcept(4002).Value;
                ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(this.DoConcept(4002));
                this.DoConcept(4002).Value = 0;
                ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();

            }

            GetLog(MyRule, DebugRuleState.After, 4002, 4003);
        }

        /// <summary>   اضافه کاری 1</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6598(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4002);
            if (this.DoConcept(2031).Value == 1) { this.DoConcept(4002).Value = 0; }
            GetLog(MyRule, DebugRuleState.After, 4002);
        }

        /// <summary>  اضافه کاری 2</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6597(AssignedRule MyRule)
        {
            GetLog(MyRule, DebugRuleState.Before, 4003);
            if (this.DoConcept(1005).Value == 1) { ((PairableScndCnpValue)this.DoConcept(4003)).AppendPairs(((PairableScndCnpValue)this.DoConcept(1))); }
            GetLog(MyRule, DebugRuleState.After,4003);
        }

       
       
        #endregion

        #region قوانين عمومي

        #endregion

        #region Concept Init


        #endregion


        #endregion

    }
}

