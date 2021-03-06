using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using GTS.Clock.ModelEngine;
using GTS.Clock.ModelEngine.Concepts;
using GTS.Clock.ModelEngine.Concepts.Operations;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.ModelEngine.ELE;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business.Calculator
{
    public class ConceptCalculator : GeneralConceptCalculator
    {
        #region Constructors

        /// <summary>
        /// ."تنها سازنده کلاس "محاسبه گر مفهوم
        /// </summary>
        /// <param name="Person">پرسنلي که محاسبات براي او در حال انجام است</param>
        /// <param name="CategorisedRule">قانوني که مفاهيم موجود در آن در صورت نياز محاسبه خواهند شد</param>
        /// <param name="CalculateDate">تاريخ انجام محاسبات</param>
        public ConceptCalculator(IEngineEnvironment engineEnvironment)
            : base(engineEnvironment)
        {

        }

        #endregion     

        #region Defined Method

        #region مفاهيم کارکرد
        
        #endregion

        #region مفاهيم مرخصي

        /// <summary>
        /// مرخصی روزانه در صورتی اعمال شود که شخص زیر 4 ساعت حضور داشته باشد
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        //public override void C1004(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        //{
        //    if (this.DoConcept(1).Value <= 4 * 60)
        //    {
        //        base.C1004(Result, MyConcept);
        //    }
        //}

        /// <summary>
        /// مرخصی استعلاجی روزانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public override void C1009(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.DailyEstelagiLeave));
            if (permit != null && permit.Value == 1)
            {
                PairableScndCnpValue tmpResult = new PairableScndCnpValue();
                PairableScndCnpValue LeaveShift = ((PairableScndCnpValue)this.DoConcept(1116));
                int hour = 0;
                if (LeaveShift.PairCount == 1 && LeaveShift.Pairs.First().To >= LeaveShift.Pairs.First().From)
                {
                    if (this.DoConcept(6).Value > 0)
                    {
                        if (this.DoConcept(6).Value < LeaveShift.Pairs.First().From)
                        {
                            tmpResult.AddPairs(this.Person.GetShiftByDate(this.ConceptCalculateDate).Pairs.ToList<IPair>());
                        }
                        else if (this.DoConcept(6).Value >= LeaveShift.Pairs.First().From && this.DoConcept(6).Value <= LeaveShift.Pairs.First().To)
                        {
                            tmpResult.Value = 1;
                        }
                        else//greater
                        {
                            int dev1 = 0;
                            if (this.DoConcept(3028).Value > 0)
                            {
                                dev1 = this.DoConcept(3028).Value / LeaveShift.Pairs.First().To;
                                hour = this.DoConcept(3028).Value % LeaveShift.Pairs.First().To;
                            }
                            else
                            {
                                dev1 = this.DoConcept(6).Value / LeaveShift.Pairs.First().To;
                                hour = this.DoConcept(6).Value % LeaveShift.Pairs.First().To;
                            }

                            if (hour >= LeaveShift.Pairs.First().From)
                            {
                                dev1++;
                                hour = 0;
                            }
                            tmpResult.Value = (int)dev1;
                        }
                    }
                    else//استعلاجی در روز تعطیل هم لحاظ میگردد 
                    {
                        tmpResult.Value = 1;
                    }
                }
                else
                {

                    float dev = 1;

                    if (this.DoConcept(1113).Value > 0)
                    {
                        if (this.DoConcept(7).Value > 0)
                        {
                            dev = (float)this.DoConcept(6).Value / (float)this.DoConcept(7).Value;
                        }
                    }
                    dev = dev < 1 ? 1 : dev;


                    tmpResult.Value = (int)dev;
                    if (Result.Value < dev)
                    {
                        hour = (int)dev - tmpResult.Value;
                    }

                }


                Result.Value = tmpResult.Value;
                if (Result.Value > 0)
                {
                    ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();
                    ((PairableScndCnpValue)this.DoConcept(3029)).ClearPairs();
                    ((PairableScndCnpValue)this.DoConcept(3030)).ClearPairs();
                    ((PairableScndCnpValue)this.DoConcept(3031)).ClearPairs();
                }
            }
        }

       /// <summary>
       /// مرخصی نیم روز
       /// مرخصی روزانه ای که تا حد مشخصی از غیبت را پوشش میدهد و د هر صورت به میزان مشخص از 
       /// مانده مرخصی کسر میگردد
       /// </summary>
       /// <param name="Result"></param>
       /// <param name="MyConcept"></param>
        public virtual void C1501(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            ((PairableScndCnpValue)Result).ClearPairs();
            PairableScndCnpValue validLeave = new PairableScndCnpValue();
            PairableScndCnpValue AbsentLeave = new PairableScndCnpValue();
            Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(150));
            BaseShift shift = this.Person.GetShiftByDate(this.ConceptCalculateDate);
            
            if (shift != null && shift.PairCount > 0 && permit.Value > 0)
            {
                // ساعات نیم روز سوال گردد

                IPair firstHalf = new PairableScndCnpValuePair(shift.PastedPairs.From, 11 * 60 + 30);
                IPair secondHalf = new PairableScndCnpValuePair(10 * 60 + 45, shift.PastedPairs.To);

                if (Operation.Intersect(firstHalf, this.DoConcept(3028)).Value > Operation.Intersect(secondHalf, this.DoConcept(3028)).Value)
                {                    
                    this.Person.AddUsedLeave(this.ConceptCalculateDate, firstHalf.Value, permit);
                    //((PairableScndCnpValue)Result).AddPair(firstHalf);
                    Result.Value = 1;
                    ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(Operation.Differance(this.DoConcept(3028), firstHalf));
                    ((PairableScndCnpValue)this.DoConcept(3001)).AddPairs(this.DoConcept(3028));
                }
                else
                {
                    this.Person.AddUsedLeave(this.ConceptCalculateDate, secondHalf.Value, permit);
                    //((PairableScndCnpValue)Result).AddPair(secondHalf);
                    Result.Value = 1;
                    ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(Operation.Differance(this.DoConcept(3028), secondHalf));
                    ((PairableScndCnpValue)this.DoConcept(3001)).AddPairs(this.DoConcept(3028));
                }
                              
                this.ReCalculate(this.ConceptCalculateDate, 3008, 3010, 3014, 3029, 3030, 3031);
            }
        }

       /// <summary>
       /// مرخصی نیم روز ماهانه
       /// </summary>
       /// <param name="Result"></param>
       /// <param name="MyConcept"></param>
        public virtual void C1502(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

       /// <summary>
       /// آف دکتر 
       /// کسر از اضافه کار
       /// </summary>
       /// <param name="Result"></param>
       /// <param name="MyConcept"></param>
        public virtual void C1503(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            ((PairableScndCnpValue)Result).ClearPairs();
           
            Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(154));
           
            if (permit.Value > 0)
            {
                ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();
                ((PairableScndCnpValue)this.DoConcept(3029)).ClearPairs();
                ((PairableScndCnpValue)this.DoConcept(3030)).ClearPairs();
                ((PairableScndCnpValue)this.DoConcept(3031)).ClearPairs();
                this.DoConcept(3004).Value = 0;
                Result.Value = 1;
              
            }
        }

       /// <summary>
       /// دکتر صنعت ماهانه
       /// </summary>
       /// <param name="Result"></param>
       /// <param name="MyConcept"></param>
        public virtual void C1504(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
            if (Result.Value > 0)
            {
                if (this.DoConcept(4005).Value > 0)
                {
                    this.DoConcept(4005).Value -= (Result.Value * 5 * 60);
                    if (this.DoConcept(4005).Value <= 0)
                    {
                        this.DoConcept(4005).Value = 1;
                    }
                }
            }
        }

        #endregion

        #region مفاهيم ماموريت

        /// <summary>مفهوم مجموع ماموريت ساعتي</summary>
        /// جدا کردن ماموریت 2019 و 2021
        /// <param name="Result"></param>
        public override void C2023(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PairableScndCnpValue.AddPairsToScndCnpValue(this.DoConcept(2004), Result);
            //PairableScndCnpValue.AppendPairsToScndCnpValue(this.DoConcept(2019), Result);
            PairableScndCnpValue.AppendPairsToScndCnpValue(this.DoConcept(2020), Result);
            //PairableScndCnpValue.AppendPairsToScndCnpValue(this.DoConcept(2021), Result);
            PairableScndCnpValue.AppendPairsToScndCnpValue(this.DoConcept(2022), Result);
            this.DoConcept(13);//در قانون مربوطه باید اضافه گردد
            this.DoConcept(2).Value += Operation.Intersect(this.Person.GetShiftByDate(this.ConceptCalculateDate), Result).Value;
        }
        #endregion

        #region مفاهيم غيبت


        /// <summary>
        ///کسر کار تبدیل شده به مرخصی
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C3501(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            ((PairableScndCnpValue)Result).ClearPairs();
            
        }

        /// <summary>
        /// کسر کار تبدیل شده به مرخصی ماهانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C3502(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        /// <summary>
        ///کسر کار برج قبل
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C3503(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;

        }

        /// <summary>
        /// کسر کار برج قبل ماهانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C3504(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        /// <summary>
        ///کسر کار تبدیل شده به روزانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C3505(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            ((PairableScndCnpValue)Result).ClearPairs();

        }

        /// <summary>
        /// کسر کار تبدیل شده به روزانه ماهانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C3506(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }


        #endregion

        #region مفاهيم اضافه کاري
    
        /// <summary>
        /// اضافه کار پنج شنبه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C6501(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            ((PairableScndCnpValue)Result).ClearPairs();               
        }

        /// <summary>
        /// اضافه کار پنجشنبه ماهانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C6502(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        /// <summary>
        /// اضافه کار شبکاری
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C6503(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            ((PairableScndCnpValue)Result).ClearPairs();
        }

        /// <summary>
        /// اضافه کار شبکاری ماهانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C6504(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        /// <summary>
        /// اضافه کار بسیج
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C6505(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            ((PairableScndCnpValue)Result).ClearPairs();
        }

        /// <summary>
        /// اضافه کار بسیج ماهانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C6506(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }
        #endregion

        #region مفاهيم متفرقه

        #endregion

        #endregion
    }
}
