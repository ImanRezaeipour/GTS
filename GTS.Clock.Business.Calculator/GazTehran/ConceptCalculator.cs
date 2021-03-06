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
        ///  تاخیر تبدیل شده به مرخصی
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C1501(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        /// <summary>
        /// تاخیر تبدیل شده به مرخصی ماهانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        /// 

        public override  void C1004(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            //طبق توافق صورت گرفته در اینجا تنها داشتن مجوز ملاک مقداردهی مرخصی استحقاقی روزانه است
            int leaveInDay = this.DoConcept(1001).Value > this.DoConcept(6).Value && this.DoConcept(6).Value > 0
                                 ? this.DoConcept(6).Value
                                 : this.DoConcept(1001).Value;

            Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.DailyEstehghaghiLeave));
            //طبق درخواست این سازمان در صورت حضور مرخصی روزانه داده نمیشود
            if (permit != null && permit.Value == 1 && this.DoConcept (1).Value ==0)
            {
                int demandLeave = this.Person.GetRemainLeave(this.ConceptCalculateDate);
                float dev = 1;

                if (this.DoConcept(1113).Value > 0)
                {
                    if (this.DoConcept(7).Value > 0)
                    {
                        dev = (float)this.DoConcept(6).Value / (float)this.DoConcept(7).Value;
                    }
                }
                dev = dev < 1 ? 1 : dev;

                if (this.Person.GetShiftByDate(this.ConceptCalculateDate).Value > 0 || this.DoConcept(1021).Value == 1)
                {
                    if (dev * leaveInDay > demandLeave && this.DoConcept(1098).Value == 0)
                    {
                        //تبدیل مرخصی استحقاقی به مرخصی بی حقوق در صورت عدم طلب
                        if (this.DoConcept(1014).Value == 1)
                        {
                            this.DoConcept(1066).Value = 1;
                        }
                    }
                    else
                    {
                        Result.Value = (int)dev;
                        if (Result.Value < dev)
                        {
                            float remain = dev - Result.Value;
                            ((PairableScndCnpValue)this.DoConcept(1002)).IncreaseValue((int)(remain * leaveInDay));
                            //((PairableScndCnpValue)this.DoConcept(3028)).DecreasePairFromFirst(this.DoConcept(1002).Value);
                            //((PairableScndCnpValue)this.DoConcept(3001)).DecreasePairFromFirst(this.DoConcept(1002).Value);
                            //this.ReCalculate(this.ConceptCalculateDate, 3008, 3010, 3014, 3029, 3030, 3031);
                        }
                        this.Person.AddUsedLeave(this.ConceptCalculateDate, (int)(dev * leaveInDay), permit);
                    }
                }
                //else
                //{
                //    //مرخصی قطعی در روزغیرکاری
                //    if (this.DoConcept(1021).Value == 1)
                //    {
                //        //حتی اگر طلب نداشته باشد باید منفی شود                        
                //        Result.Value = 1;
                //        this.Person.AddUsedLeave(this.ConceptCalculateDate, leaveInDay, permit);
                //    }
                //}
            }
        }


        public virtual void C1502(BaseScndCnpValue Result, SecondaryConcept MyConcept)
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

        #region مفاهيم غيبت

        
        #endregion

        #region مفاهيم اضافه کاري
   
    
        #endregion

        #region مفاهيم متفرقه

        #endregion

        #endregion      


    }
}
