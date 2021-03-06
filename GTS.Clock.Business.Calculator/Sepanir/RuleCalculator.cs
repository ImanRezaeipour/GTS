using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GTS.Clock.Model;
using System.Reflection;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Concepts.Operations;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model.ELE;

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

        public override void R1017(AssignedRule MyRule)
        {
            base.R1017(MyRule);

            this.DoConcept(1503);
        }

        #endregion

        #region قوانين کارکرد

      
        
     
        #endregion

        #region قوانين مرخصي

        /// <summary>
        /// مقداردهی بودجه مرخصی کارگاهی
        /// </summary>
        /// <param name="MyRule"></param>
        public void R3501(AssignedRule MyRule)
        {

            if (this.DoConcept(5).Value > 0)
            {
                int karkerd = this.DoConcept(5).Value;
                //به ازای هر 22 روز کارکرد , 8 روز مرخصی کارگاهی لحاظ میگردد
                int restBudget = (karkerd * 8) / 22;
                if (restBudget > 0)
                {
                    this.Person.AddRemainLeaveBudget(this.RuleCalculateDate, restBudget * this.DoConcept(1001).Value);
                }
            }
        }

        /// <summary>
        /// اعمال مرخصی کارگاهی
        /// </summary>
        /// <param name="MyRule"></param>
        public void R3502(AssignedRule MyRule)
        {
            //3005 غیبت روزانه ماهانه
            //5 کارکرد روزانه ماهانه
            //1502 مجوز مرخصی کارگاهی 
            //1504 مرخصی کارگاهی
            GetLog(MyRule, DebugRuleState.Before, 1501, 1502, 5, 3005);
            this.DoConcept(1501);
            int maxRest = MyRule["first", this.RuleCalculateDate].ToInt();
            int maxNegativ = MyRule["second", this.RuleCalculateDate].ToInt();
           /* if (this.DoConcept(1501).Value > 0)//1502 cal situation type every day
            {
                int rest = this.DoConcept(1502).Value;
                if (rest > maxRest)
                {
                    rest = maxRest;
                }
                int leaveInDay = this.DoConcept(1001).Value;

                int demandLeave = this.Person.GetRemainLeave(this.RuleCalculateDate);
                if (rest * leaveInDay > demandLeave + maxNegativ * leaveInDay)
                {
                    rest = (int)((float)demandLeave / (float)leaveInDay) + (int)((float)maxNegativ / (float)leaveInDay);
                }
                if (rest < this.DoConcept(1502).Value)
                {
                    this.DoConcept(1501).Value = 0;
                    this.DoConcept(3004).Value = 1;
                    this.ReCalculate(1502);
                }
               
            }*/
            //5    کارکردخالص روزانه ماهانه
            //3005 غيبت خالص روزانه ماهانه
            if (this.DoConcept(1502).Value > 0 && (this.DoConcept(5).Value > 0 || this.DoConcept(3005).Value > 0))
            {
                int rest = this.DoConcept(1502).Value;
                int realRest = rest;
                if (rest > maxRest) 
                {
                    rest = maxRest;
                }
                int leaveInDay = this.DoConcept(1001).Value;

                int demandLeave = this.Person.GetRemainLeave(this.RuleCalculateDate);
                if (rest * leaveInDay > demandLeave + maxNegativ * leaveInDay)
                {
                    rest = (int)((float)demandLeave / (float)leaveInDay) + (int)((float)maxNegativ / (float)leaveInDay);
                }
                this.Person.AddUsedLeave(this.RuleCalculateDate, rest * leaveInDay, null);
                this.DoConcept(1504).Value = rest;
                this.DoConcept(5).Value += rest;
                //this.DoConcept(3005).Value = this.DoConcept(3005).Value < rest ? 0 : this.DoConcept(3005).Value - rest;
                if (realRest > rest) 
                {
                    this.DoConcept(3005).Value += realRest - rest;
                }
            }
            GetLog(MyRule, DebugRuleState.After, 1501, 1502, 5, 3005);

        }

        /// <summary>
        /// اعمال تاخیر روز بعد از رست  
        /// </summary>
        /// <param name="MyRule"></param>
        public void R3503(AssignedRule MyRule)
        {
            //1501 رست
            //3028 غیبت ساعنی
            //3004 غیبت روزانه
            //3029 تاخیر
            //3020 غیبت ساتی مجاز
            //3021 تاخیر ساعتی مجاز
            //3024 تاخیر مجاز ماهانه
            //3026 غیبت مجاز ماهانه
            //3032 تاخیر ماهانه
            //3034 غیبت ساعتی ماهانه
            // 3 کارکرد ناخالص ماهانه
            //5 کارکرد روزانه ماهانه
            GetLog(MyRule, DebugRuleState.Before, 2, 4,1501, 3020, 3021, 3028, 3029, 13, 3020, 3028, 3, 5, 3024, 3026, 3032, 3034);
            if (this.DoConcept(1501).Value == 0 &&this.DoConcept(3004).Value == 0 && this.DoConcept(3029).Value > 0
                && this.DoConcept(1501, this.RuleCalculateDate.AddDays(-1)).Value > 0
                && !EngEnvironment.HasCalendar(this.ConceptCalculateDate, "3"))
            {
                int allowToTime = MyRule["first", this.RuleCalculateDate].ToInt();
                IPair alowPair = new PairableScndCnpValuePair(this.Person.GetShiftByDate(this.RuleCalculateDate).PastedPairs.From, allowToTime);
                this.DoConcept(3021).Value = Operation.Intersect(alowPair, this.DoConcept(3029)).Value;
                ((PairableScndCnpValue)this.DoConcept(3029)).AddPairs(Operation.Differance(this.DoConcept(3029), alowPair));
                this.DoConcept(2).Value += this.DoConcept(3021).Value;
                this.DoConcept(4).Value = 1;
                this.ReCalculate(13, 3020, 3028, 5, 3, 3024, 3026, 3032, 3034);
            }

            GetLog(MyRule, DebugRuleState.After, 2, 4, 1501, 3020, 3021, 3028, 3029, 13, 3020, 3028, 3, 5, 3024, 3026, 3032, 3034);
        }

     
        #endregion

        #region قوانين ماموريت

       
        #endregion

        #region قوانين کم کاري

        /// <summary>
        /// فرجه تاخیر و تعجیل
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R5032(AssignedRule MyRule)
        {
            //3020 غیبت ساعتی مجاز
            //3029 تاخیر
            //3030 تعجیل
            //3028 غیبت ساعتی
            //3021 تاخیر مجاز
            //3022 تعجیل مجاز
            //3031 غیبت بین وقت ساعتی غیرمجاز
            //3024 تاخیر ساعتی مجاز ماهانه
            //3026 غیبت ساعتی مجاز ماهانه
            //3027 غیبت بین وقت ساعتی مجاز ماهانه
            //1082 مجموع انواع مرخصی ساعتی
            //2023 مفهوم مجموع ماموريت ساعتي

            var conceptList = new[] { 2, 3020, 3022, 3024, 3026, 3031, 3020, 3027, 3028, 3040, 3026 };

            GetLog(MyRule, " Before Execute State:", conceptList);

            this.DoConcept(1082);
            this.DoConcept(2023);
            this.DoConcept(1002);
            this.DoConcept(1109);

            int forje = 0;
            int forjeSum = MyRule["First", this.RuleCalculateDate].ToInt();
            int forjeTakhir = MyRule["Second", this.RuleCalculateDate].ToInt();
            int forjeTajil = MyRule["Third", this.RuleCalculateDate].ToInt();
            int forjeBetween = MyRule["Fourth", this.RuleCalculateDate].ToInt();
            int forjeMounth = MyRule["Fifth", this.RuleCalculateDate].ToInt();
            int forjeMounthTakhir = MyRule["Sixth", this.RuleCalculateDate].ToInt();
            int forjeMounthTajil = MyRule["Seventh", this.RuleCalculateDate].ToInt();
            int forjeMounthBetween = MyRule["Eighth", this.RuleCalculateDate].ToInt();
            bool allDayAbsence = MyRule["Ninth", this.RuleCalculateDate].ToInt() > 0;
            //bool allMonthAbsence = MyRule["Tenth", this.RuleCalculateDate].ToInt() > 0;
            bool notFillAllowAbsence = (MyRule.HasParameter("Eleventh", this.RuleCalculateDate) && MyRule["Eleventh", this.RuleCalculateDate].ToInt() > 0);


            int SumHourlyAbsent = this.DoConcept(3024).Value + this.DoConcept(3025).Value + this.DoConcept(3027).Value;

            if (
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                (forjeSum > 0 || forjeTakhir > 0 || forjeTajil > 0 || forjeBetween > 0) &&
                this.DoConcept(3028).Value > 0
                )
            {

                PairableScndCnpValue takhir = (PairableScndCnpValue)this.DoConcept(3029);
                PairableScndCnpValue tajil = (PairableScndCnpValue)this.DoConcept(3030);
                if (takhir == null) takhir = new PairableScndCnpValue();
                if (tajil == null) tajil = new PairableScndCnpValue();
   
                #region چک کردن حضور قبل و بعد تاخیر و تعجیل و سقف مقدار

                if (allDayAbsence)
                {
                    foreach (PairableScndCnpValuePair takhirPair in ((PairableScndCnpValue)this.DoConcept(3029)).Pairs)
                    {
                        if ((takhirPair.Value > forjeSum && forjeSum > 0) || (takhirPair.Value > forjeTakhir && forjeTakhir > 0 && forjeSum == 0))
                        {
                            takhir = Operation.Differance(takhir, takhirPair);
                        }
                    }

                    foreach (PairableScndCnpValuePair tajilPair in ((PairableScndCnpValue)this.DoConcept(3030)).Pairs)
                    {
                        if ((tajilPair.Value > forjeSum && forjeSum > 0) || (tajilPair.Value > forjeTajil && forjeTajil > 0 && forjeSum == 0))
                        {
                            tajil = Operation.Differance(tajil, tajilPair);
                        }
                    }
                }                

                #endregion

                PairableScndCnpValue tempPairs = new PairableScndCnpValue();

                #region تاخیر مجاز
                if (forjeSum > 0)
                {
                    forje = forjeSum;
                }
                else if (forjeTakhir > 0)
                {
                    forje = forjeTakhir;
                }
                if (forje > 0 && takhir != null && takhir.Value > 0 && ((takhir.Value + tempPairs.Value + SumHourlyAbsent <= forjeMounth) && forjeMounth > 0
                    || (((forje + tempPairs.Value + SumHourlyAbsent <= forjeMounth) && forjeMounth > 0))
                    || ((forje + tempPairs.Value + this.DoConcept(3024).Value <= forjeMounthTakhir) && forjeMounth == 0 && forjeMounthTakhir > 0)
                    || ((takhir.Value + tempPairs.Value + this.DoConcept(3024).Value <= forjeMounthTakhir) && forjeMounth == 0 && forjeMounthTakhir > 0)))
                {
                  //  ((PairableScndCnpValue)tempPairs).AddPairs(takhir);
                    foreach (PairableScndCnpValuePair takhirPair in takhir.Pairs)
                    {
                        if (takhirPair.Value <= forje)
                        {
                            tempPairs.AppendPair(new BasePair(takhirPair.From, takhirPair.From + forje));
                            //forje = 0;
                        }
                        //else
                        //{
                        //    forje -= takhir.Value;
                        //}
                    }
                    
                } 
                #endregion


                #region تعجیل مجاز
                if (forjeSum == 0 && forjeTajil > 0)
                {

                    forje = forjeTajil;
                }

                if (forje > 0 && tajil != null && tajil.Value > 0 && ((tajil.Value + tempPairs.Value + SumHourlyAbsent <= forjeMounth) && forjeMounth > 0
                    || (((forje + tempPairs.Value + SumHourlyAbsent <= forjeMounth) && forjeMounth > 0))
                    || ((forje + tempPairs.Value + this.DoConcept(3025).Value <= forjeMounthTajil) && forjeMounth == 0 && forjeMounthTajil > 0)
                    || ((tajil.Value + tempPairs.Value + this.DoConcept(3025).Value <= forjeMounthTajil) && forjeMounth == 0 && forjeMounthTajil > 0)))
                {
                    foreach (PairableScndCnpValuePair tajilPair in tajil.Pairs)
                    {
                       // ((PairableScndCnpValue)tempPairs).AppendPair(tajilPair);

                        if (tajilPair.Value <= forje)
                        {
                            //  ((PairableScndCnpValue)tempPairs).AddPairs(Operation.Differance(tempPairs, tajil));
                            tempPairs.AppendPair(new BasePair(tajilPair.To - forje, tajilPair.To));
                            //forje = 0;
                        }
                        //else if (tajil.Value <= forje)
                        //{
                        //    forje -= tajil.Value;
                        //} 
                    }
                   
                } 
                #endregion

                #region غیبت بین وقت
                if (Operation.Intersect(this.DoConcept(3031), this.DoConcept(3028)).Value > 0)
                {
                    PairableScndCnpValue Between = ((PairableScndCnpValue)this.DoConcept(3031));
                    Between.AddPairs(Operation.Differance(Between, tempPairs));
                    if (allDayAbsence && ((Between.Value > forjeSum && forjeSum > 0) || (Between.Value > forjeBetween && forjeBetween > 0 && forjeSum == 0)))
                    {
                        Between = new PairableScndCnpValue();
                    }
                    if (forjeSum == 0 && forjeBetween > 0)
                    {
                        forje = forjeBetween;
                    }
                    if (forje > 0 && Between != null && Between.Value > 0 && ((Between.Value + tempPairs.Value + SumHourlyAbsent <= forjeMounth) && forjeMounth > 0
                    || (((forje + tempPairs.Value + SumHourlyAbsent <= forjeMounth) && forjeMounth > 0))
                    || ((forje + tempPairs.Value + this.DoConcept(3027).Value <= forjeMounthBetween) && forjeMounth == 0 && forjeMounthBetween > 0)
                    || ((Between.Value + tempPairs.Value + this.DoConcept(3027).Value <= forjeMounthBetween) && forjeMounth == 0 && forjeMounthBetween > 0)))
                    {
                        foreach (PairableScndCnpValuePair pair in Between.Pairs.OrderBy(x => x.From))
                        {

                            PairableScndCnpValue tempBetween = new PairableScndCnpValue();
                            ((PairableScndCnpValue)tempBetween).AppendPair(pair);

                            if (tempBetween.Value <= forje && forje > 0)
                            {
                                tempPairs.AppendPair(pair);
                                forje -= tempBetween.Value;
                            }

                            else if (tempBetween.Value > forje && forje > 0)
                            {
                                tempPairs.AppendPair(new BasePair(pair.From, pair.From + forje));
                                forje = 0;
                            }

                        }
                        this.DoConcept(3023).Value = Operation.Intersect(this.DoConcept(3031), tempPairs).Value;

                        ((PairableScndCnpValue)this.DoConcept(3031)).DecreasePairFromFirst(this.DoConcept(3023).Value);

                        this.DoConcept(2).Value += this.DoConcept(3023).Value;
                        this.ReCalculate(13, 3020, 3028);
                    }
                  
                } 
                #endregion

                if (tempPairs != null && tempPairs.Value > 0)
                {
                    if (!notFillAllowAbsence)
                    {
                        this.DoConcept(3021).Value += Operation.Intersect(tempPairs, takhir).Value;
                        this.DoConcept(3022).Value += Operation.Intersect(tempPairs, tajil).Value;                      
                        this.DoConcept(3020).Value = this.DoConcept(3021).Value + this.DoConcept(3022).Value + this.DoConcept(3023).Value;

                    }

                    ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(Operation.Differance(this.DoConcept(3028), tempPairs));
                    ((PairableScndCnpValue)this.DoConcept(3029)).AddPairs(Operation.Differance(this.DoConcept(3029), tempPairs));
                    ((PairableScndCnpValue)this.DoConcept(3030)).AddPairs(Operation.Differance(this.DoConcept(3030), tempPairs));
                    ((PairableScndCnpValue)this.DoConcept(3031)).AddPairs(Operation.Differance(this.DoConcept(3031), tempPairs));
                   

                }
            }

            GetLog(MyRule, " After Execute State:", conceptList);
        }

        #endregion

        #region قوانين اضافه کاري

        /// <summary>
        /// اعمال اضافه کار ویژه در زمان حضور در تعطیلات خاص
        /// </summary>
        /// <param name="MyRule"></param>
        public void R6501(AssignedRule MyRule)
        {
            //4032 اضافه کار ویژه
            //4033 اضافه کار ویژه ماهانه
            if (EngEnvironment.HasCalendar(this.RuleCalculateDate, "3"))
            {
                if (this.DoConcept(1).Value > 0)
                {
                    GetLog(MyRule, " Before Execute State:", 4032);

                    this.DoConcept(4032).Value += this.DoConcept(13).Value;

                    GetLog(MyRule, " After Execute State:", 4032);

                }
                //this.DoConcept(3004).Value = 0;

            }
            this.DoConcept(4033);
        }

        public void R6502(AssignedRule MyRule)
        {
            //4032 اضافه کار ویژه
            //4033 اضافه کار ویژه ماهانه
            GetLog(MyRule, " Before Execute State:", 4002);

            int start = MyRule["first", this.RuleCalculateDate].ToInt();
            int end = MyRule["second", this.RuleCalculateDate].ToInt();
            int overwork = MyRule["third", this.RuleCalculateDate].ToInt();

            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.Count > 1)
            {
                IPair pair = new PairableScndCnpValuePair(this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs[0].To, this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs[1].From);
                ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(Operation.Differance(this.DoConcept(4002), pair));
            }
            ProceedTraffic ProceedTraffic = this.Person.GetProceedTraficByDate(this.RuleCalculateDate);
            //2023 ماموریت ساعتی
            bool applyed = false;
            if (this.DoConcept(2023).Value > 0)
            {
                foreach (PairableScndCnpValuePair pair in ((PairableScndCnpValue)this.DoConcept(2023)).Pairs)
                {
                    if (pair.From >= start && pair.To <= end)
                    {
                        ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(overwork);
                        applyed = true;
                        this.DoConcept(13).Value += overwork;
                        if (this.DoConcept(9).Value > 0)
                        {
                            this.ReCalculate(4005);
                        }
                        break;
                    }
                }
            }
            if (!applyed && ProceedTraffic != null && ProceedTraffic.Pairs != null && ProceedTraffic.Pairs.Where(x => x.IsFilled).Count() > 1)
            {
                for (int i = 0; i < ProceedTraffic.Pairs.Count-1; i++) 
                {
                    if (ProceedTraffic.Pairs[i].To >= start && ProceedTraffic.Pairs[i + 1].From <= end)
                    {
                        ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(overwork);
                        this.DoConcept(13).Value += overwork;
                        if (this.DoConcept(9).Value > 0)
                        {
                            this.ReCalculate(4005);
                        }
                        break;
                    }
                }
            }
            
            GetLog(MyRule, " After Execute State:", 4002);


        }

        #endregion


        #endregion
     
    }
}
