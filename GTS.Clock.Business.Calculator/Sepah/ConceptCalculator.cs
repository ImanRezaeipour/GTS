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
    /// <summary>
    /// بانک سپه
    /// </summary>
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

        /// <summary>مفهوم مرخصي استحقاقي خالص روزانه</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين مفهوم در تعاريف بعدي بيست و دو-22 درنظر گرفته شده است</remarks>
        public override void C1004(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            //طبق توافق صورت گرفته در اینجا تنها داشتن مجوز ملاک مقداردهی مرخصی استحقاقی روزانه است
            int leaveInDay = this.DoConcept(1001).Value;

            Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.DailyEstehghaghiLeave));
            if (permit != null && permit.Value == 1)
            {
                if (this.Person.GetShiftByDate(this.ConceptCalculateDate).Value > 0)
                {
                    int demandLeave = this.Person.GetRemainLeave(this.ConceptCalculateDate);
                    if (leaveInDay > demandLeave && this.DoConcept(1098).Value == 0)
                    {
                        //تبدیل مرخصی استحقاقی به مرخصی بی حقوق در صورت عدم طلب
                        if (this.DoConcept(1014).Value == 1)
                        {
                            this.DoConcept(1066).Value = 1;
                        }
                    }
                    else
                    {
                        Result.Value = 1;
                        this.Person.AddUsedLeave(this.ConceptCalculateDate, leaveInDay, permit);
                    }
                }
                else
                {
                    //مرخصی قطعی در روزغیرکاری
                    if (this.DoConcept(1021).Value == 1)
                    {
                        //حتی اگر طلب نداشته باشد باید منفی شود                        
                        Result.Value = 1;
                        this.Person.AddUsedLeave(this.ConceptCalculateDate, leaveInDay, permit);
                    }
                }
            }
        }

        #endregion

        #region مفاهيم ماموريت

        /// <summary>
        /// ماموریت آموزشی 
        /// شامل اضافه کار نمیشود
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public override void C2014(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            ((PairableScndCnpValue)Result).Pairs.Clear();
            PairableScndCnpValue validMission = new PairableScndCnpValue();
            PairableScndCnpValue AbsentMission = new PairableScndCnpValue();
            Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.HourDuty2));
            BaseShift shift = this.Person.GetShiftByDate(this.ConceptCalculateDate);

            if (shift != null && shift.PairCount > 0)
            {

                #region بررسی غیبت ها

                AbsentMission.AddPairs(Operation.Intersect(permit, this.DoConcept(3028)));

                ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(Operation.Differance(this.DoConcept(3028), AbsentMission));
                ((PairableScndCnpValue)this.DoConcept(3001)).AddPairs(Operation.Differance(this.DoConcept(3001), AbsentMission));
                ((PairableScndCnpValue)Result).AppendPairs(AbsentMission);

                #endregion               
            }
            else//روز تعطیل 
            {
                ((PairableScndCnpValue)Result).AppendPairs(permit.Pairs);
            }

            this.ReCalculate(this.ConceptCalculateDate, 3008, 3010, 3014, 3029, 3030, 3031);
        }
        #endregion

        #region مفاهيم غيبت

        #endregion

        #region مفاهيم اضافه کاري

        /// <summary>مفهوم اضافه کارساعتي مجاز</summary>
        /// <param name="Result"></param>
        /// <remarks>شناسه اين مفهوم در تعاريف بعدي یکصدو بیست و پنج-125 درنظر گرفته شده است</remarks>
        public virtual void C4002(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            ((PairableScndCnpValue)Result).AddPairs(
                Operation.Differance(
                        Operation.Differance(this.DoConcept(4001), this.DoConcept(4017)),
                        this.DoConcept(4009)
                    )
                );
            ((PairableScndCnpValue)Result).AddPairs(Operation.Differance(Result, this.DoConcept(5002)));
        }

        /// <summary>مفهوم اضافه کارساعتي تعطيل</summary>
        /// <param name="Result"></param>this.ConceptCalculateDate
        /// <remarks>شناسه اين مفهوم در تعاريف بعدي شصت و چهار-64 درنظر گرفته شده است</remarks>
        public override void C4009(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            if (this.Person.GetShiftByDate(this.ConceptCalculateDate).Value < 1)
            {
                ((PairableScndCnpValue)Result).AddPairs(this.DoConcept(4001));
            }
        }

        /// <summary>
        /// اضافه کار خالص باجه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C4502(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        /// <summary>
        /// اضافه کار نا خالص باجه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C4506(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        /// <summary>
        /// اضافه کار خالص باجه ماهانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C4504(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        /// <summary>
        /// اضافه کار نا خالص باجه ماهانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C4505(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            //Result.Value = this.DoConcept(4504).Value;
            //Result.FromDate = this.DoConcept(4504).FromDate;
            //Result.ToDate = this.DoConcept(4504).ToDate;
            //Result.CalculationDate = this.ConceptCalculateDate;

            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        #region مفاهيمي_كه_صرفا_جهت_ويرايش_ايجاد_شده_اند_و_در_قوانين_بهيچ_عنوان_صدا_زده_نميشوند

        /// <summary>مفهوم عابربانك</summary>
        /// <param name="Result"></param>     
        public virtual void C4507(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }
        /// <summary>مفهوم آموزشي</summary>
        /// <param name="Result"></param>     
        public virtual void C4508(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }
        /// <summary>مفهوم ساير</summary>
        /// <param name="Result"></param>     
        public virtual void C4509(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        #endregion

        /// <summary>
        /// اضافه کار اول وقت ماهانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C4510(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        /// <summary>
        /// مغایرت که در قانون R70 مقدار دهی میشود
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public void C4512(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        /// <summary>
        /// ماهانه کاهشی
        /// روزانه ندارد
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C4513(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public void C4514(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public void C4515(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

      
        #endregion

        #region مفاهيم متفرقه


        /// <summary>
        /// مفهوم ویژه جانبازی
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public void C5501(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        /// <summary>
        /// مفهوم ماهانه ویژه جانبازی
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public void C5502(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        /// <summary>
        /// مفهوم وضعیت روز
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public void C5503(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        /// <summary>
        /// مفهوم ماهانه وضعیت روز
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public void C5504(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;          
        }

        /// <summary>مفهوم ایاب و ذهاب</summary>
        /// <param name="Result"></param>     
        public virtual void C5505(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        #endregion

        #endregion
    }
}
