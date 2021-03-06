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
        /// 
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        //public override void C1002(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        //{
        //    ((PairableScndCnpValue)Result).ClearPairs();
        //    Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.HourLeave1));
        //    PairableScndCnpValue permitPairs = new PairableScndCnpValue();
        //    int demandLeave = 0;
        //    int leavePieceCount = this.DoConcept(1092).Value;
           
        //    #region محاسبه مجوزهای داخل شیفتی که محدودیت های تعداد و مقدار برای آنها اعمال شده باشد

        //    BaseShift shift = this.Person.GetShiftByDate(this.ConceptCalculateDate);
        //    foreach (PermitPair permitPair in permit.Pairs)
        //    {
        //        PairableScndCnpValue pairsInShift = Operation.Intersect(permitPair, shift);

        //        permitPairs.AppendPair(permitPair);
        //        //if (this.DoConcept(1099).Value == 0)
        //        //{
        //        //    PermitInShift.AppendPairs(pairsInShift.Pairs);
        //        //}
        //        //else
        //        //{
        //        //    PermitInShift.AppendPair(permitPair);
        //        //}
        //    }

        //    #endregion

        //    #region بررسی مقدار طلب بمنظور تبدیل مجوز مرخصی به مرخصی

        //    demandLeave = this.Person.GetRemainLeave(this.ConceptCalculateDate);
        //    if (permitPairs.Value > 0 && (permitPairs.Value > demandLeave && this.DoConcept(1098).Value == 0))
        //    {
        //        PairableScndCnpValue removedPairs = permitPairs.DecreasePairFromLast(permitPairs.Value - demandLeave);
        //        //تبدیل مرخصی استحقاقی به مرخصی بی حقوق در صورت عدم طلب
        //        if (this.DoConcept(1014).Value == 1)
        //        {
        //            ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(Operation.Differance(this.DoConcept(3028), removedPairs));
        //            ((PairableScndCnpValue)this.DoConcept(3001)).AddPairs(Operation.Differance(this.DoConcept(3001), removedPairs));

        //            this.DoConcept(1056).Value += removedPairs.PairValues;
        //        }
        //    }

        //    #endregion

        //    if (permitPairs.Value > 0)
        //    {
        //        if (this.DoConcept(1099).Value == 0)
        //        {
        //            ((PairableScndCnpValue)Result).AppendPairs(Operation.Intersect(this.DoConcept(3028), permitPairs));
        //            this.Person.AddUsedLeave(this.ConceptCalculateDate,
        //                                     Operation.Intersect(this.DoConcept(3028), permitPairs).Value, permit);
        //        }
        //        else
        //        {
        //            ((PairableScndCnpValue)Result).AppendPairs(permitPairs);
        //            this.Person.AddUsedLeave(this.ConceptCalculateDate,
        //                                     Operation.Intersect(permitPairs, permitPairs).Value, permit);
        //        }
        //        ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(Operation.Differance(this.DoConcept(3028), permitPairs));
        //        ((PairableScndCnpValue)this.DoConcept(3001)).AddPairs(Operation.Differance(this.DoConcept(3001), permitPairs));
        //        this.DoConcept(1092).Value = leavePieceCount;

        //        this.ReCalculate(this.ConceptCalculateDate, 3008, 3010, 3014, 3029, 3030, 3031);
        //    }
        //}

        /// <summary>
        /// مرخصی روزانه با توجه به مفهوم مرخصی در روز
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        //public override void C1004(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        //{
        //    int leaveInDay = this.DoConcept(1001).Value;

        //    Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.DailyEstehghaghiLeave));
        //    if (permit != null && permit.Value == 1)
        //    {
        //        if (this.Person.GetShiftByDate(this.ConceptCalculateDate).Value > 0)
        //        {
        //            int demandLeave = this.Person.GetRemainLeave(this.ConceptCalculateDate);
        //            if (leaveInDay > demandLeave && this.DoConcept(1098).Value == 0)
        //            {
        //                //تبدیل مرخصی استحقاقی به مرخصی بی حقوق در صورت عدم طلب
        //                if (this.DoConcept(1014).Value == 1)
        //                {
        //                    this.DoConcept(1066).Value = 1;
        //                }
        //            }
        //            else
        //            {
        //                Result.Value = 1;
        //                this.Person.AddUsedLeave(this.ConceptCalculateDate, leaveInDay, permit);
        //            }
        //        }
        //        else
        //        {
        //            //مرخصی قطعی در روزغیرکاری
        //            if (this.DoConcept(1021).Value == 1)
        //            {
        //                //حتی اگر طلب نداشته باشد باید منفی شود                        
        //                Result.Value = 1;
        //                this.Person.AddUsedLeave(this.ConceptCalculateDate, leaveInDay, permit);
        //            }
        //        }
        //    }
        //}

        /// <summary>مفهوم مرخصی بیماری بی حقوق خالص روزانه_31 </summary>
        /// <param name="Result"></param>
        public override void C1063(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            //طبق درخواست مانا مرخصی روزانه ای که در روزهای 5 شنبه گرفته میشود باید روزانه شود نه ساعتی
            Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.DailyNoSallaryIllnessLeave1));
            if (permit != null && permit.Value == 1)
            {
                Result.Value = 1;
                this.DoConcept(3003).Value = 0;
                //Result.FromDate =
                //Result.ToDate = this.ConceptCalculateDate;
            }
            else
            {
                Result.Value = 0;
            }
        }
       

        #endregion

        #region مفاهيم ماموريت

        #endregion

        #region مفاهيم غيبت

        #endregion

        #region مفاهيم اضافه کاري

        #endregion

        #region مفاهيم متفرقه

        #endregion

        #endregion      


    }
}
