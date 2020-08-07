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
    /// <summary>
    /// وزرات امور خارجه
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
