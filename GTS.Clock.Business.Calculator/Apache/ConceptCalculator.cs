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

        /// <summary>
        /// حضور در روز
        /// اگر حضور داشته باشد , یک میشود
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C501(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
            if (this.DoConcept(1).Value > 0)
            {
                Result.Value = 1;
            }
        }
        /// <summary>
        /// تعداد روز حضور
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C502(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;

        }

        /// <summary>
        /// حداقل 3:30 ساعت حضور داشته
        /// اگر این حداقل را داشته باشد ,آخر ماه از اضافه کار به کارکرد تبدیل میشود
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C503(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        /// <summary>
        /// تعداد روز که حداقل 3:30 حضور داشته
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C504(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;

        }

        /// <summary>
        /// تعداد روز آف
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C505(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        /// <summary>
        /// تعداد روز آف ماهانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C506(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;

        }

        /// <summary>
        /// تعداد روز تبدیل کارکرد
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C507(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        /// <summary>
        /// تعداد روز تبدیل کارکر ماهانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C508(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;

        }

        /// <summary>
        /// اضافه کار خالص
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C509(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
        }

        /// <summary>
        /// hاضافه کار خالص ماهانه
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C510(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;

        }

        #endregion

        #region مفاهيم مرخصي
       
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
