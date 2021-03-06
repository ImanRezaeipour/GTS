using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Concepts.Operations;
using GTS.Clock.Model.ELE;

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
            int leaveInDay = this.DoConcept(1001).Value ;

            Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.DailyEstehghaghiLeave));
            if (permit != null && permit.Value == 1)
            {
                if (this.Person.GetShiftByDate(this.ConceptCalculateDate).Value > 0)
                {
                    int demandLeave = this.Person.GetRemainLeave(this.ConceptCalculateDate);
                    if (leaveInDay > demandLeave)
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

        #endregion

        #region مفاهيم غيبت

        /// <summary>
        /// مفهوم غیبت مجاز شیردهی
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public void C3501(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        /// <summary>
        /// مفهوم ماهانه غیبت مجاز شیردهی
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public void C3502(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        /// <summary>
        /// غیبت مجاز مهد
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public void C3503(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        /// <summary>
        /// کارکرد خالص شب ماهانه 
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public void C3504(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        /// <summary>
        /// غیبت مجاز تقليل رفاه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public void C3505(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        /// <summary>
        /// مفهوم ماهانه غیبت مجاز تقليل رفاه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public void C3506(BaseScndCnpValue Result, SecondaryConcept MyConcept)
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

        #endregion

        #endregion
    }
}
