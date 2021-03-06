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

        /// <summary>
        /// اعمال کسر تقلیل برای برخی از پرسنل
        /// </summary>
        /// <param name="MyRule"></param>
        public override  void R5021(AssignedRule MyRule)
        {
            //1082 مجموع انواع مرخصی ساعتی
            //2023 مفهوم مجموع ماموريت ساعتي
            var conceptList = new[] { 2, 3, 13, 3020, 3028, 3040, 4002, 4005, 4006, 4007, 1119 };

            GetLog(MyRule, DebugRuleState.Before, conceptList);

            this.DoConcept(1082);
            this.DoConcept(2023);
            var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_shirdehi", this.RuleCalculateDate);

            if (
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                // مفدار غیبت مجاز برای تقلیل
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                personParam != null
                )
            {
                var minutes = Utility.ToInteger(personParam.Value);

                if (MyRule["Second", this.RuleCalculateDate].ToInt() == 1)
                {
                    #region اعمال روی غیبت
                    if (this.DoConcept(3028).Value > 0)
                    {
                        while (this.DoConcept(3028).Value > 0 && minutes > 0)
                        {
                            foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From))
                            {
                                if (pair.Value > minutes)
                                {
                                    IPair tempPair = new BasePair(pair.To - minutes, pair.To);

                                    this.DoConcept(3020).Value += tempPair.Value;

                                    var PairableScndCnpValue = Operation.Differance(this.DoConcept(3028), tempPair);

                                    ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(PairableScndCnpValue.Pairs);

                                    // غيبت ساعتي مجاز تقليل
                                    ((PairableScndCnpValue)this.DoConcept(3040)).AppendPair(tempPair);

                                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(tempPair);

                                    pair.To -= minutes;

                                    minutes = 0;

                                    break;
                                }

                                if (pair.Value == minutes)
                                {
                                    this.DoConcept(3020).Value += pair.Value;

                                    minutes -= pair.Value;

                                    ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);

                                    // غيبت ساعتي مجاز تقليل
                                    ((PairableScndCnpValue)this.DoConcept(3040)).AppendPair(pair);

                                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                                    pair.From = pair.To = 0;

                                    break;
                                }

                                if (pair.Value < minutes)
                                {
                                    this.DoConcept(3020).Value += pair.Value;

                                    minutes -= pair.Value;

                                    ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);

                                    // غيبت ساعتي مجاز تقليل
                                    ((PairableScndCnpValue)this.DoConcept(3040)).AddPair(pair);

                                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                                    pair.From = pair.To = 0;
                                }
                            }
                        }
                    }
                    #endregion
                }

                if (MyRule["First", this.RuleCalculateDate].ToInt() == 1)
                {
                    #region اعمال روی اضافه کار
                    if (minutes > 0)
                    {
                        // اعمال روی اضافه کار
                        ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValueEx(minutes);

                        var basePair = new BasePair(
                              ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).Last().To - minutes,
                              ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).Last().To
                              );

                        // غيبت ساعتي مجاز تقليل
                        ((PairableScndCnpValue)this.DoConcept(3040)).AddPair(basePair);

                        this.ReCalculate(3, 13, 4005, 4006, 4007);
                    }
                    #endregion
                }


                if (MyRule.HasParameter("Third", this.RuleCalculateDate))
                {
                    if (MyRule["Third", this.RuleCalculateDate].ToInt() == 1)
                    {
                        #region اعمال روی مرخصی
                        if (minutes > 0)
                        {
                            // اعمال روی مرخصی
                            this.Person.AddBudgetLeave( this.RuleCalculateDate,minutes );

                            this.DoConcept(1119).Value = minutes;
                        }
                        #endregion
                    }
                }

            }
            GetLog(MyRule, DebugRuleState.After, conceptList);
        }

        /// <summary> جانبازی</summary>
        /// <remarks></remarks>
        //اضافه کار مجاز4002
        public virtual void R6501(AssignedRule MyRule)
        {

            var conceptList = new[] { 2, 3, 13, 4005, 4006, 4007, 5501, 3020, 3028, 4002, 3029, 3030, 3031 };
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
                int allBodje,bodje = 0;
                
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
                   
                     allBodje = minutes;
                    bool doOnOvertime = this.Person.PersonTASpec.R16Value.ComboValue.Substring(2, 1) == "1";

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
                                    bodje += minutes;

                                    break;
                                }

                                if (pair.Value == minutes)
                                {
                                    this.DoConcept(3020).Value += pair.Value;

                                    ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);
                                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);

                                    pair.From = pair.To = 0;
                                    bodje += minutes;
                                    break;
                                }

                                this.DoConcept(3020).Value += pair.Value;

                                minutes -= pair.Value;

                                ((PairableScndCnpValue)this.DoConcept(3028)).RemovePair(pair);
                                ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(pair);
                                bodje += minutes;

                                pair.From = pair.To = 0;
                            }
                        }
                        if (doOnOvertime)
                        {
                            allBodje -= bodje;
                            if (allBodje > 0)
                            {
                                BasePair basePair = null;
                                if (this.DoConcept(4002).Value > 0)
                                {
                                    basePair = new BasePair(
                                      ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).First().To,
                                      ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).First().To + allBodje
                                      );
                                }
                                else
                                {
                                    basePair = new BasePair(
                                      this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.To).First().To,
                                      this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.To).First().To + allBodje
                                      );
                                }

                                ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(basePair);
                            }


                        }

                        else
                        {
                            allBodje -= bodje;
                            if (allBodje > 0)
                            {
                                this.Person.AddBudgetLeave(this.RuleCalculateDate, allBodje);
                                this.DoConcept(1119).Value = allBodje;
                            }
                        }
                }

               
                }
                ((PairableScndCnpValue)this.DoConcept(3029)).AddPairs(Operation.Intersect(this.DoConcept(3029), this.DoConcept(3028)));
                ((PairableScndCnpValue)this.DoConcept(3030)).AddPairs(Operation.Intersect(this.DoConcept(3030), this.DoConcept(3028)));
                ((PairableScndCnpValue)this.DoConcept(3031)).AddPairs(Operation.Intersect(this.DoConcept(3031), this.DoConcept(3028)));

               

            

            GetLog(MyRule, DebugRuleState.Before, conceptList);

        }

        #endregion

        #region قوانين اضافه کاري

        #endregion

        #region قوانين عمومي

        #endregion

        #region Concept Init


        #endregion


        #endregion

    }
}

