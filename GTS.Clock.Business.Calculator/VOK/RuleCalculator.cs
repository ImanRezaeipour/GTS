using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GTS.Clock.Model;
using System.Reflection;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Concepts.Operations;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model.ELE;

namespace GTS.Clock.Business.Calculator
{
    /// <summary>
    /// وزرات امور خارجه
    /// </summary>
    public class RuleCalculator : GeneralRuleCalculator
    {
        #region Constructors
        /// <summary>
        /// ."تنها سازنده کلاس "محاسبه گر اشياء
        /// </summary>
        /// <param name="Person">پرسنلي که محاسبات براي او در حال انجام است</param>
        /// <param name="CategorisedRule">قانوني که منجر به فراخواني مفاهيم از کلاس "محاسبه گر قانون" خواهد شد</param>
        /// <param name="CalculateDate">تاريخ انجام محاسبات</param>
        public RuleCalculator(IEngineEnvironment engineEnvironment)
            : base(engineEnvironment, new ConceptCalculator(engineEnvironment))
        {
            this.logLock = !GTSAppSettings.RuleDebug;
        }
        #endregion 

        #region Defined Methods

        #region قوانين متفرقه
      

        #endregion

        #region قوانين کارکرد

        #endregion

        #region قوانين مرخصي

        #endregion

        #region قوانين ماموريت

        #endregion

        #region قوانين کم کاري

        #endregion

        #region قوانين اضافه کاري

        #endregion

        #region قوانين عمومي

        #endregion

        #region Concept Init


        #endregion


        #endregion
    }
}
