
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

        /// <summary>مفهوم رست </summary>
        /// <param name="Result"></param> 
        public void C1501(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            //Result.Value = 0;
            if (this.DoConcept(3004).Value == 1 || this.DoConcept(3028).Value > 5 * HourMin)
            {
                Permit rest = this.Person.GetPermitByDate(this.RuleCalculateDate, EngEnvironment.GetPrecard(202));
                if (rest != null && rest.Value == 1)
                {
                    this.DoConcept(1501).Value = 1;
                    this.DoConcept(4).Value = 1;
                    this.DoConcept(2).Value = 0;// this.Person.GetShiftByDate(this.RuleCalculateDate).PairValues;
                    this.DoConcept(6).Value = 0;
                    this.DoConcept(3004).Value = 0;
                    ((PairableScndCnpValue)this.DoConcept(3028)).ClearPairs();
                }
            }
          
        }

        /// <summary>مفهوم رست ماهانه </summary>
        /// <param name="Result"></param> 
        public void C1502(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        #endregion 

        #region مفاهيم ماموريت

        /// <summary>مفهوم مجموع ماموريت روزانه</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين مفهوم در تعاريف بعدي سي و چهار-4 درنظر گرفته شده است</remarks>
        public override void C2005(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
            Result.Value = this.DoConcept(2031).Value;
            Result.Value += this.DoConcept(2032).Value;
            Result.Value += this.DoConcept(2033).Value;
            Result.Value += this.DoConcept(2034).Value;
            //Result.Value += this.DoConcept(2026).Value;
        }

        /// <summary>مفهوم ماموریت ماهانه_65</summary>
        /// <param name="Result"></param>        
        public override void C2035(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        /// <summary>مفهوم ماموریت ساعتی تعطیل_روز غیرکاری، تعطیل رسمی و غیررسمی</summary>
        /// <param name="Result"></param>     
        public void C2501(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            if (EngEnvironment.HasCalendar(this.ConceptCalculateDate, "1", "2") ||
                this.Person.GetShiftByDate(this.ConceptCalculateDate).Value == 0)
            {
                ((PairableScndCnpValue)Result).AddPairs(((PairableScndCnpValue)this.DoConcept(2002)).Pairs);
            }
        }

        /// <summary>مفهوم ماموریت روزانه تعطیل_روز غیرکاری، تعطیل رسمی و غیررسمی</summary>
        /// <param name="Result"></param>     
        public void C2502(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            if (EngEnvironment.HasCalendar(this.ConceptCalculateDate, "1", "2") ||
                this.Person.GetShiftByDate(this.ConceptCalculateDate).Value == 0)
            {
                Result.Value = this.DoConcept(2003).Value;
            }
        }

        #endregion

        #region مفاهيم غيبت

        /// <summary>مفهوم غيبت خالص ساعتي</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين مفهوم در تعاريف بعدي سي و شش-36 درنظر گرفته شده است</remarks>
        public override void C3001(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
            PairableScndCnpValue.ClearPairsValue(Result);
            ProceedTraffic proceedTraffic = this.Person.GetProceedTraficByDate(this.ConceptCalculateDate);
            if (proceedTraffic.IsFilled && this.DoConcept(1090).Value == 0 && this.DoConcept(2005).Value == 0 && this.DoConcept(1091).Value == 0 &&
                this.DoConcept(2026).Value == 0 )
            {
                if (this.Person.GetShiftByDate(this.ConceptCalculateDate).ShiftType != ShiftTypesEnum.OVERTIME)
                {
                    if (this.Person.GetShiftByDate(this.ConceptCalculateDate).Value > 0 && proceedTraffic != null && (proceedTraffic.HasHourlyItem || (!proceedTraffic.HasHourlyItem && !proceedTraffic.HasDailyItem)))
                    {
                        ((PairableScndCnpValue)Result).AddPairs(Operation.Differance(this.Person.GetShiftByDate(this.ConceptCalculateDate), proceedTraffic));
                    }
                    else if (this.Person.GetShiftByDate(this.ConceptCalculateDate).Value > 0 && proceedTraffic == null)
                    {
                        ((PairableScndCnpValue)Result).AppendPairs(this.Person.GetShiftByDate(this.ConceptCalculateDate).Pairs);
                    }
                    else
                    {
                        Result.Value = 0;
                    }
                }
                else
                {
                    Result.Value = 0;
                }
            }
        }

        #endregion

        #region مفاهيم اضافه کاري

        /// <summary>مفهوم اضافه کارساعتی تعطیل_روز غیرکاری، تعطیل رسمی و غیررسمی</summary>
        /// <param name="Result"></param>     
        public void C4501(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            if (EngEnvironment.HasCalendar(this.ConceptCalculateDate, "1", "2") ||
                this.Person.GetShiftByDate(this.ConceptCalculateDate).Value == 0)
            {
                ((PairableScndCnpValue)Result).AddPairs(((PairableScndCnpValue)this.DoConcept(4001)).Pairs);
            }
        }

        #endregion

        #region مفاهيم متفرقه     

        #endregion

        #endregion
    }
}
