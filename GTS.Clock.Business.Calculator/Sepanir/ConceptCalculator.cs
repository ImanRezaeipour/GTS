using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Concepts.Operations;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model.ELE;
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
        /// مجوز مرخصی کارگاهی
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C1501(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
            Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(127));
            if (permit != null && permit.Value == 1)
            {
                if (!EngEnvironment.HasCalendar(this.ConceptCalculateDate, "3"))
                {
                    Result.Value = 1;
                }
                if (this.DoConcept(3028).Value > 0)
                {
                    ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();
                }
                if (this.DoConcept(3004).Value > 0)
                {
                    this.DoConcept(3004).Value = 0;
                }
            }
        }

        /// <summary>
        /// مجوز مرخصی کارگاهی ماهانه
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
        ///  مرخصی کارگاهی
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C1503(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = this.DoConcept(1501).Value;            
        }

        /// <summary>
        ///  مرخصی کارگاهی ماهانه
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
        }


        #endregion

        #region مفاهيم ماموريت

        #endregion


        #region غیبت
        /// <summary>مفهوم تاخير خالص ساعتي</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين مفهوم در تعاريف بعدي چهل و دو-42 درنظر گرفته شده است</remarks>
        public override void C3008(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
            PairableScndCnpValue.ClearPairsValue(Result);
            ProceedTraffic proceedTraffic = Person.GetProceedTraficByDate(this.ConceptCalculateDate);
            this.DoConcept(3038).Value = 0;
            if (this.DoConcept(1090).Value == 0 && this.DoConcept(2005).Value == 0 && this.DoConcept(1091).Value == 0)
            {
                PairableScndCnpValue absent = (PairableScndCnpValue)this.DoConcept(3001);
                BaseShift shift = this.Person.GetShiftByDate(this.ConceptCalculateDate);
                PairableScndCnpValue takhir=new PairableScndCnpValue();
                if (absent != null && shift != null && absent.PairCount != 0 && shift.PairCount != 0)
                {
                    if (proceedTraffic != null && proceedTraffic.Pairs.Where(x => x.IsFilled).Count() > 0
                        && Operation.Intersect(this.Person.GetShiftByDate(this.ConceptCalculateDate), proceedTraffic).Value > 0)
                    {
                        IPair pair = proceedTraffic.Pairs.Where(x => x.IsFilled).OrderBy(x => x.From).First();
                        //ShiftPair firstShiftPair = shift.Pairs.OrderBy(x => x.From).First();
                        foreach (ShiftPair shiftPair in shift.Pairs.OrderBy(x => x.From).ToList())
                        {
                            PairableScndCnpValue p = Operation.Intersect(shiftPair, absent);
                            if (p.PairCount > 0 && p.Pairs.OrderBy(x => x.From).First().From ==
                               shiftPair.From)
                            {
                                //PairableScndCnpValue val = Operation.Differance(p.Pairs.OrderBy(x => x.From).First(), pair);
                                PairableScndCnpValue.AppendPairToScndCnpValue(p.Pairs.First(), Result);
                                this.DoConcept(3038).Value += 1;
                            }
                        }
                    }
                    else
                    {
                        //در نظر گرفتن کل غیبت
                        foreach (ShiftPair shiftPair in shift.Pairs.OrderBy(x => x.From).ToList())
                        {
                            PairableScndCnpValue p = Operation.Intersect(shiftPair, absent);
                            if (p.PairCount > 0 && p.Pairs.OrderBy(x => x.From).First().From ==
                               shiftPair.From)
                            {
                                PairableScndCnpValue.AppendPairToScndCnpValue(p.Pairs.OrderBy(x => x.From).First(), Result);
                                this.DoConcept(3038).Value += 1;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>مفهوم تاخير خالص ساعتي</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين مفهوم در تعاريف بعدي چهل و دو-42 درنظر گرفته شده است</remarks>
        public virtual void C3010(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
            PairableScndCnpValue.ClearPairsValue(Result);
            ProceedTraffic proceedTraffic = Person.GetProceedTraficByDate(this.ConceptCalculateDate);
            this.DoConcept(3038).Value = 0;
            if (this.DoConcept(1090).Value == 0 && this.DoConcept(2005).Value == 0 && this.DoConcept(1091).Value == 0)
            {
                PairableScndCnpValue absent = (PairableScndCnpValue)this.DoConcept(3001);
                BaseShift shift = this.Person.GetShiftByDate(this.ConceptCalculateDate);

                if (absent != null && shift != null && absent.PairCount != 0 && shift.PairCount != 0)
                {
                    if (proceedTraffic != null && proceedTraffic.Pairs.Where(x => x.IsFilled).Count() > 0
                        && Operation.Intersect(this.Person.GetShiftByDate(this.ConceptCalculateDate), proceedTraffic).Value > 0)
                    {
                        IPair pair = proceedTraffic.Pairs.Where(x => x.IsFilled).OrderBy(x => x.From).First();
                        //ShiftPair firstShiftPair = shift.Pairs.OrderBy(x => x.From).First();
                        foreach (ShiftPair shiftPair in shift.Pairs.OrderBy(x => x.From).ToList())
                        {
                            PairableScndCnpValue p = Operation.Intersect(shiftPair, absent);
                            if (p.PairCount > 0 && p.Pairs.OrderBy(x => x.From).Last().To ==
                               shiftPair.To)
                            {
                                //PairableScndCnpValue val = Operation.Differance(p.Pairs.OrderBy(x => x.From).First(), pair);
                                PairableScndCnpValue.AppendPairToScndCnpValue(p.Pairs.Last(), Result);
                                this.DoConcept(3038).Value += 1;
                                //break;
                            }
                        }
                    }
                    else
                    {
                        //در نظر گرفتن کل غیبت
                        foreach (ShiftPair shiftPair in shift.Pairs.OrderBy(x => x.From).ToList())
                        {
                            PairableScndCnpValue p = Operation.Intersect(shiftPair, absent);
                            if (p.PairCount > 0 && p.Pairs.OrderBy(x => x.From).First().From ==
                               shiftPair.From)
                            {
                                PairableScndCnpValue.AppendPairToScndCnpValue(p.Pairs.OrderBy(x => x.From).First(), Result);
                                this.DoConcept(3038).Value += 1;
                            }
                        }
                    }
                }
            }
        }
        
        #endregion

        #region مفاهيم اضافه کاري
   

        #endregion

        #region مفاهيم متفرقه

        #endregion

        #endregion      


    }
}
