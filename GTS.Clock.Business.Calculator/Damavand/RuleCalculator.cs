using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GTS.Clock.ModelEngine;
using System.Reflection;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.ModelEngine.Concepts;
using GTS.Clock.ModelEngine.Concepts.Operations;
using GTS.Clock.ModelEngine.AppSetting;
using GTS.Clock.ModelEngine.ELE;

namespace GTS.Clock.Business.Calculator
{
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

        /// <summary>
        /// رند شدن اضافه کاری و غیبت به 30 دقیقه یا 0 
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R1501(AssignedRule MyRule)
        {
           
            GetLog(MyRule, DebugRuleState.Before, 3028,4002);
            if (this.DoConcept(3028).Value >0)
            {
                int gheybat = Utility.ToInteger(this.DoConcept(3028).Value / 60);
            }

            GetLog(MyRule, DebugRuleState.After, 3028, 4002);

        }
        #endregion

        #region قوانين کارکرد

        /// <summary>قانون مقداردهی به شبکاری و ضریب دادن به شبکاری C14</summary>        
        public override  void R2001(AssignedRule MyRule)
        {
            //15 شبکاری
            //16 تعداد شبکاری
            GetLog(MyRule, DebugRuleState.Before, 16, 15);
            PairableScndCnpValue.AppendPairToScndCnpValue(new PairableScndCnpValuePair(MyRule["First", this.RuleCalculateDate].ToInt(), MyRule["Second", this.RuleCalculateDate].ToInt()), this.DoConcept(14));
            this.DoConcept(16).Value = 1;
            this.DoConcept(15);
           
            if (this.DoConcept(15).Value >0)
            {
               
                float coEfficient = MyRule["Third", this.RuleCalculateDate].ToInt() / 100f;
                float i = this.DoConcept(15).Value * coEfficient;
                ((PairableScndCnpValue)this.DoConcept(15)).IncreaseValue((int)Math.Round(i));
                             
            }
           
            GetLog(MyRule, DebugRuleState.After, 16, 15);

        }

         /// <summary>
        /// به ازای هر روز تعطیل رسمی و غیر رسمی مقدار ----ساعت کسر کار اضافه شود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R2501(AssignedRule MyRule)
        {
            //var conceptList = new[] { 3028, 4002,4003,1003,1056,1008,3008,3029,3010,3030,2004,2013, 
            //                          5002,17,1053,1023,1038,1024,1039,1041,1026,2016,2017,2018,2019,
            //                          2020,2021,2022,2023,2014,4007,4009,3015,4008,1025,1027,1040,1042,
            //                          1054,4014,4012,1057,1058,1059,1060,1061,1062,1078,4011,4023,3040,
            //                          3042,3044,4030,4034,4035,4038,4040,4042,4044,4046,5038,4048,2060};
            //foreach (int calcDate in conceptList)
            //{
            //    if (this.DoConcept(calcDate).Value > 0)
            //    {
            //        int tt3028_rem;
            //        Decimal tt3028 = Math.Truncate(this.DoConcept(calcDate).Value / 60m);
            //        if (tt3028 > 0)
            //        {
            //            tt3028 = tt3028 * 60;
            //        }
            //        tt3028_rem = this.DoConcept(calcDate).Value;
            //        if (this.DoConcept(calcDate).Value > 60)
            //        {
            //            tt3028_rem = this.DoConcept(calcDate).Value % 60;
            //        }

            //        if (tt3028_rem >= 16 && tt3028_rem <= 45)
            //        {
            //            tt3028 += 30;
            //        }
            //        else if (tt3028_rem >= 46 && tt3028_rem <= 59)
            //        {
            //            tt3028 += 60;
            //        }
            //        this.DoConcept(calcDate).Value = Convert.ToInt32(tt3028);
            //    }
            //}
        }
        #endregion

        #region قوانين مرخصي
        /// <summary>
        /// مرخصی استعلاجی بیش از 15 روز
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3501(AssignedRule MyRule)
        {
            //1018 مرخصی استعلاجی سالانه
            GetLog(MyRule, DebugRuleState.Before, 1018);
            if (this.DoConcept (1018).Value > MyRule["First", this.RuleCalculateDate].ToInt())
            {
                  this.Person.AddUsedLeave(this.RuleCalculateDate, MyRule["First", this.RuleCalculateDate].ToInt(), null);
                this.DoConcept (1018).Value -=MyRule["First", this.RuleCalculateDate].ToInt();
            }     

            GetLog(MyRule, DebugRuleState.After, 1018);

        }

        /// <summary>
        /// مرخصی استحقاقی کارکنان نوبت کار
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3502(AssignedRule MyRule)
        {
           
            GetLog(MyRule, DebugRuleState.Before, 1003,1005);

            string s1 = MyRule["First", this.RuleCalculateDate].ToString ();
            string s2 = MyRule["Second", this.RuleCalculateDate].ToString();
            string s3 = MyRule["Third", this.RuleCalculateDate].ToString();
            string s4 = MyRule["Fourth", this.RuleCalculateDate].ToString();
            string s5 = MyRule["Fifth", this.RuleCalculateDate].ToString();
            string s6 = MyRule["Sixth", this.RuleCalculateDate].ToString();
            bool z1 = (this.Person.GetShiftByDate(this.RuleCalculateDate,s1).Value == 0 ? true : false) ;
            if (z1 && this .DoConcept (1005).Value >0)
            {
                this.DoConcept(1005).Value = 1;
               
            }

            bool z2 = (this.Person.GetShiftByDate(this.RuleCalculateDate, s2).Value == 0 ? true : false);
            if (z2 && this.DoConcept(1005).Value > 0)
            {
                this.DoConcept(1005).Value = 1;
            }

            bool z3 = (this.Person.GetShiftByDate(this.RuleCalculateDate, s3).Value == 0 ? true : false);
            if (z3 && this.DoConcept(1005).Value > 0)
            {
                this.DoConcept(1005).Value = 1;
                this.DoConcept(1003).Value = 240;
                this.Person.AddUsedLeave(this.RuleCalculateDate, 240, null);
            }

            bool z4 = (this.Person.GetShiftByDate(this.RuleCalculateDate, s4).Value == 0 ? true : false);
            if (z4 && this.DoConcept(1005).Value > 0)
            {
                this.DoConcept(1005).Value = 4;
                this.DoConcept(1003).Value = 240;
                this.Person.AddUsedLeave(this.RuleCalculateDate, 240, null);
            }

            bool z5 = (this.Person.GetShiftByDate(this.RuleCalculateDate, s5).Value == 0 ? true : false);
            if (z5 && this.DoConcept(1005).Value > 0)
            {
                this.DoConcept(1005).Value = 3;
                this.DoConcept(1003).Value = 240;
                this.Person.AddUsedLeave(this.RuleCalculateDate, 240, null);
            }

            bool z6 = (this.Person.GetShiftByDate(this.RuleCalculateDate, s6).Value == 0 ? true : false);
            if (z6 && this.DoConcept(1005).Value > 0)
            {
                this.DoConcept(1005).Value = 3;
            }
            GetLog(MyRule, DebugRuleState.After, 1003,1005);

        }
      
        #endregion

        #region قوانين ماموريت

        #endregion

        #region قوانين کم کاري
        /// <summary>
        /// به ازای هر روز تعطیل رسمی و غیر رسمی مقدار ----ساعت کسر کار اضافه شود
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R5501(AssignedRule MyRule)
        {

            GetLog(MyRule, DebugRuleState.Before, 3028);

            if (MyRule["Second", this.RuleCalculateDate].ToInt() > 0 && this.DoConcept(1).Value > 0 && (EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2")))
            {
                int OverTime = MyRule["First", this.RuleCalculateDate].ToInt();
                ((PairableScndCnpValue)this.DoConcept(3028)).IncreaseValue(OverTime);
            }
            else if ((EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2") || this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0))
            {
                int OverTime = MyRule["First", this.RuleCalculateDate].ToInt();
                ((PairableScndCnpValue)this.DoConcept(3028)).IncreaseValue(OverTime);
            }

            GetLog(MyRule, DebugRuleState.After, 3028);

        }


        #endregion

        #region قوانين اضافه کاري

        /// <summary>  در صورت حضور در شیفت مقدار --- ساعت اضافه کار لحاظ شود</summary>
        /// <param name="Result"></param>
        /// <remarks></remarks>
        public virtual   void R6501(AssignedRule MyRule)
        {
            // 1 حضور
            // 4002 اضافه کار مجاز
            GetLog(MyRule, DebugRuleState.After, 4002);

            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 && this.DoConcept(1).Value > 0 && MyRule["First", this.RuleCalculateDate].ToInt()>0)
            {
                int OverTime = MyRule["First", this.RuleCalculateDate].ToInt();
                this.DoConcept(4002).Value += OverTime;
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(OverTime );
                this.ReCalculate(13);

            }
            GetLog(MyRule, DebugRuleState.Before , 4002);
        }

        /// <summary>
        /// تعویض شیفت و اضافه کار تعطیل رسمی
        /// </summary>
        /// <param name="MyRule"></param>u
        public virtual void R6502(AssignedRule MyRule)
        {
            //4041 اضافه کار خالص ساعتي تعطيل رسمي و غير رسمي ماهانه
            GetLog(MyRule, DebugRuleState.After, 4041);

            if ( MyRule["First", this.RuleCalculateDate].ToInt() > 0)
            {
                int OverTime = MyRule["First", this.RuleCalculateDate].ToInt();
                this.DoConcept(6503).Value = OverTime +this.DoConcept(4041).Value;
               // ((PairableScndCnpValue)this.DoConcept(4041)).IncreaseValue(OverTime);
               // this.ReCalculate(13);

            }
            GetLog(MyRule, DebugRuleState.Before, 4041);
        }


        /// <summary>
        /// اضافه کار آنکالی
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6503(AssignedRule MyRule)
        {
            //2035 ماموریت با کد 65
            //6501 آنکالی
            GetLog(MyRule, DebugRuleState.Before, 6501 );
            if (this.DoConcept (2035).Value >0)
            {
                Permit permit = this.Person.GetPermitByDate(this.RuleCalculateDate, EngEnvironment.GetPrecard(Precards.DailyLeave6));
                int OverTime = MyRule["First", this.RuleCalculateDate].ToInt();
                if (OverTime >0)
                {
                    this.DoConcept(6501).Value = OverTime * this.DoConcept(2035).Value;
                }
               
            }
            GetLog(MyRule, DebugRuleState.After, 6501 );

        }

        /// <summary>
        /// اضافه کاردر روزهای تعطیل رسمی و غیر رسمی
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R6504(AssignedRule MyRule)
        {
            // 1 حضور
            // 4002 اضافه کار مجاز
            GetLog(MyRule, DebugRuleState.Before, 4002);

            if (MyRule["Second", this.RuleCalculateDate].ToInt() > 0 && this.DoConcept(1).Value > 0 && ((EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2") || this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0)))
            {
                int OverTime = MyRule["First", this.RuleCalculateDate].ToInt();
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(OverTime);
            }

            else if ((MyRule["Second", this.RuleCalculateDate].ToInt() == 0 && this.DoConcept(1).Value > 0 && EngEnvironment.HasCalendar(this.RuleCalculateDate, "1", "2") && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0) || (MyRule["Second", this.RuleCalculateDate].ToInt() == 0 && this.DoConcept(1).Value > 0 && this.Person.GetShiftByDate(this.RuleCalculateDate).Value == 0))
             {
                int OverTime = MyRule["First", this.RuleCalculateDate].ToInt();
                ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(OverTime);
             }
            this.ReCalculate(13);
            GetLog(MyRule, DebugRuleState.After, 4002);

        }
       

        #endregion

        #region Concept Init


        #endregion


        #endregion

    }
}

