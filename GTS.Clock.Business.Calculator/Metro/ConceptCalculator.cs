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

        #endregion

        #region مفاهيم ماموريت

        #endregion

        #region مفاهيم غيبت
        
        #endregion

        #region مفاهيم اضافه کاري

        /// <summary>مفهوم اضافه کاراول وقت</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين مفهوم در تعاريف بعدي شصت و دو-62 درنظر گرفته شده است</remarks>
        //public override void C4008(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        //{

        //    int i = 0;
        //    ((PairableScndCnpValue)Result).ClearPairs();
        //    PairableScndCnpValue unionResult = (PairableScndCnpValue)this.DoConcept(1);
        //    PairableScndCnpValue validLeave = new PairableScndCnpValue();
        //    ((PairableScndCnpValue)validLeave ).AddPairs(Operation.Differance (unionResult,
        //                                                 this.Person.GetShiftByDate(this.ConceptCalculateDate)).Pairs);
        //    PairableScndCnpValue overTime = (PairableScndCnpValue)this.DoConcept(4002);
        //    BaseShift shift = this.Person.GetShiftByDate(this.ConceptCalculateDate);

        //    if (shift != null && shift.PairCount > 0)
        //    {
        //        IPair shiftPair = shift.Pairs.OrderBy(x => x.From).First();
        //        if (shift.PastedPairs.From == shiftPair.From)
        //        {

        //            while (i <= overTime.PairCount - 1)
        //            {
        //                if (shiftPair.From > overTime.PairPart(i).From)
        //                {
        //                    ((PairableScndCnpValue)Result).AppendPair(overTime.PairPart(i));
        //                }
        //                i++;
        //            }
        //        }
        //    }


        //}


        /// <summary>مفهوم اضافه کار  ساعتی جمعه کاری </summary>
        /// <param name="Result"></param>        
        public override void C4046(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            ((PairableScndCnpValue)Result).ClearPairs();
            if (this.ConceptCalculateDate.DayOfWeek == DayOfWeek.Friday)
            {
                PairableScndCnpValue unionResult = new PairableScndCnpValue();
                PairableScndCnpValuePair unionResult2 = new PairableScndCnpValuePair();
                PairableScndCnpValuePair unionResult3 = new PairableScndCnpValuePair();
                unionResult2.From = 1440;
                unionResult2.To = 2880;
                unionResult3.From = 1;
                unionResult3.To = 1439;

                ((PairableScndCnpValue)Result).AddPairs(Operation.Intersect(
                                                       Operation.Differance(this.DoConcept(1),
                                                                            this.Person.GetShiftByDate(this.ConceptCalculateDate)
                                                                           ), Operation.Intersect(this.DoConcept(4002), unionResult3)));

                ((PairableScndCnpValue)Result).AppendPairs(Operation.Intersect(this.DoConcept(4002), this.DoConcept(2004)));
                //((PairableScndCnpValue)Result).AppendPairs();

                if (this.DoConcept(4002, this.RuleCalculateDate.AddDays(-1)).Value > 0)
                {
                    unionResult = (PairableScndCnpValue)this.DoConcept(4002, this.RuleCalculateDate.AddDays(-1));
                    ((PairableScndCnpValue)Result).AppendPairs(Operation.Intersect(unionResult, unionResult2));
                }
               
                    

                
            }


        }

        #endregion

        #region مفاهيم متفرقه

        #endregion

        #endregion
    }
}
