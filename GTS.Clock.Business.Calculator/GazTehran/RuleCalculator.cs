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
        /// درخواست مرخصی و ماموریت بین وقت قطعی است
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R1501(AssignedRule MyRule)
        {
            //1003 مرخصی ساعتی
            //1100 مجموع مرخصی با حقوق ساعتی
            //2023 مجموع ماموریت ساعتی
            if (this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0)
            {
                GetLog(MyRule, DebugRuleState.Before , 2023, 3028, 1003,1100);
                bool duty = false;
                Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.HourDuty1));
                if (permit != null && permit.Value > 0)
                {
                    duty = true;
                }
                else
                {
                    permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.HourDuty2));
                    if (permit != null && permit.Value > 0)
                    {
                        duty = true;
                    }
                    else
                    {
                        permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.HourDuty3));
                        if (permit != null && permit.Value > 0)
                        {
                            duty = true;
                        }
                        else
                        {
                            permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.HourDuty4));
                            if (permit != null && permit.Value > 0)
                            {
                                duty = true;
                            }
                            else
                            {
                                permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.HourDuty5));
                                if (permit != null && permit.Value > 0)
                                {
                                    duty = true;
                                }
                            }
                        }
                    }
                }
                if (duty)
                {
                    int shiftStart = this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).First().From;
                    foreach (PermitPair pair in permit.Pairs.Where(x => x.From > shiftStart))
                    {
                        if (Operation.Intersect(pair, this.DoConcept(2023)).Value == 0)
                        {
                            ((PairableScndCnpValue)this.DoConcept(2023)).AppendPair(pair);
                            ((PairableScndCnpValue)this.DoConcept(3028)).AppendPair(pair);
                        }
                    }
                }
              
                permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.HourLeave1));
                if (permit != null && permit.Value > 0)
                {
                    int shiftStart = this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).First().From;
                    foreach (PermitPair pair in permit.Pairs.Where(x => x.From > shiftStart))
                    {
                        if (Operation.Intersect(pair, this.DoConcept(1003)).Value == 0)
                        {
                            this.Person.AddUsedLeave(this.RuleCalculateDate, pair.value, permit);
                            ((PairableScndCnpValue)this.DoConcept(1003)).AppendPair(pair);
                            ((PairableScndCnpValue)this.DoConcept(3028)).AppendPair(pair);
                        }
                    }
                }
                bool leave = false;
                permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.HourLeave2));
                if (permit != null && permit.Value > 0)
                {
                    leave = true;
                }
                else 
                {
                    permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.HourLeave3));
                    if (permit != null && permit.Value > 0)
                    {
                        leave = true;
                    }
                    else
                    {
                        permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.HourLeave4));
                        if (permit != null && permit.Value > 0)
                        {
                            leave = true;
                        }
                        else
                        {
                            permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.HourLeave5));
                            if (permit != null && permit.Value > 0)
                            {
                                leave = true;
                            }
                        }
                    }
                }

                if (leave) 
                {
                    int shiftStart = this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).First().From;
                    foreach (PermitPair pair in permit.Pairs.Where(x => x.From > shiftStart))
                    {
                        if (this.DoConcept(1100).Value == 0)
                        {
                            this.DoConcept(1100).Value += pair.value;
                            ((PairableScndCnpValue)this.DoConcept(3028)).AppendPair(pair);
                        }
                    }
                }
                GetLog(MyRule, DebugRuleState.After, 2023, 3028, 1003, 1100);
            }
           
        }

        #endregion

        #region قوانين کارکرد

       
        
     
        #endregion

        #region قوانين مرخصي

        /// <summary>
        /// تا ---- دقیقه تاخیر تا سقف ---- ساعت در ماه به مرخصی استحقاقی تبدیل شود 
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3501(AssignedRule MyRule)
        {
            //3029 تاخیر 
            //3028 غیبت ساعتی
            //1501 تاخیر تبدیل شده به مرخصی
            //1502 تاخیر تبدیل شده به مرخصی ماهانه

            int maxTakhir = MyRule["first", this.RuleCalculateDate].ToInt();
            int maxInMonth = MyRule["second", this.RuleCalculateDate].ToInt();
            int demandLeave = this.Person.GetRemainLeave(this.ConceptCalculateDate);
       
            //چک کردن عدم تردد ناقص وحداقل یک تردد
            ProceedTraffic ProceedTraffic = this.Person.GetProceedTraficAllByDate(this.RuleCalculateDate);
            if ((ProceedTraffic.PairCount > 0 && !ProceedTraffic.IsFilled) || ProceedTraffic.PairCount<=0)
            
            {
              //  this.DoConcept(3029).Value = 0;
                return;
            }
              
   
            if (this.DoConcept(3029).Value > 0 &&
                demandLeave > 0 &&
                this.DoConcept(1502).Value < maxInMonth )
            {
                GetLog(MyRule, DebugRuleState.Before , 1003, 3029, 3028, 2, 13, 1501, 1502);
                int takhir = this.DoConcept(3029).Value;
                if (this.DoConcept(3029).Value > maxTakhir)
                {
                    takhir = maxTakhir;
                }
                int mojaz = this.DoConcept(1502).Value + takhir < maxInMonth ? takhir : maxInMonth - this.DoConcept(1502).Value;
                int toLeave = demandLeave >= mojaz ? mojaz : demandLeave;
                int startOfShift = this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).First().From;

                IPair pair = new PairableScndCnpValuePair(startOfShift, startOfShift + toLeave);
                this.Person.AddUsedLeave(this.RuleCalculateDate, toLeave, null);
                ((PairableScndCnpValue)this.DoConcept(3029)).AddPairs(Operation.Differance(this.DoConcept(3029), pair));
                ((PairableScndCnpValue)this.DoConcept(1003)).AppendPair(pair);
                this.DoConcept(1501).Value += toLeave;
                this.ReCalculate(3028, 2, 13);
                GetLog(MyRule, DebugRuleState.After, 1003, 3029, 3028, 2, 13, 1501, 1502);

            }
        }

        

     
        #endregion

        #region قوانين ماموريت
       
        #endregion

        #region قوانين کم کاري

        #endregion

        #region قوانين اضافه کاري

       
        #endregion


        #endregion
     
    }
}
