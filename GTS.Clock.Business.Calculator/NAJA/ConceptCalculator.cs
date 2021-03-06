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
        public override void C1002(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            ((PairableScndCnpValue)Result).ClearPairs();
            //120 precard : مرخصی نیم روز
            Permit halfDayPermit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(120));
            Permit hourlyLeave = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.HourLeave1));
            if (halfDayPermit.ID > 0 && halfDayPermit.PairCount > 0)
            {
                PairableScndCnpValue PermitInShift = new PairableScndCnpValue();
                int demandLeave = 0;
                PairableScndCnpValue absence = (PairableScndCnpValue)this.DoConcept(3028);
                int absenceDuration = 0;
                int maxLeave = 3 * 60 + 30;
                foreach (PairableScndCnpValuePair pair in absence.Pairs)
                {
                    if (absenceDuration >= maxLeave)
                        break;
                    int pairValue = pair.Value;
                    if (absenceDuration + pair.Value > maxLeave)
                    {
                        pairValue = maxLeave - absenceDuration;
                        //absenceDuration = maxLeave;

                    }

                    demandLeave = this.Person.GetRemainLeave(this.ConceptCalculateDate);
                    if (demandLeave < pairValue && this.DoConcept(1098).Value == 0)
                        pairValue = demandLeave;

                    this.Person.AddUsedLeave(this.ConceptCalculateDate, pairValue, halfDayPermit);

                    absenceDuration += pairValue;
                }

                IList<IPair> pairList= absence.DecreasePairFromFirst(absenceDuration);
                ((PairableScndCnpValue)this.DoConcept(3001)).DecreasePairFromFirst(absenceDuration);
                foreach (IPair pair in pairList) 
                {
                    ((PairableScndCnpValue)Result).AppendPair(pair);
                }

                this.ReCalculate(this.ConceptCalculateDate, 3008, 3010, 3014, 3029, 3030, 3031);                
            }
            if (hourlyLeave.ID > 0 && hourlyLeave.PairCount > 0)
            {
                base.C1002(Result, MyConcept);
            }
        }

        /// <summary>
        /// مرخصی روزانه با توجه به مفهوم مرخصی در روز
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public override void C1004(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
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

        /// <summary>
        /// بی حقوق ساعتی - مرخصی قطعی لحاظ نگردد
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public override void C1055(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            ((PairableScndCnpValue)Result).Pairs.Clear();
            PairableScndCnpValue validLeave = new PairableScndCnpValue();
            PairableScndCnpValue AbsentLeave = new PairableScndCnpValue();
            Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(Precards.NoSallaryHourLeave1));
            BaseShift shift = this.Person.GetShiftByDate(this.ConceptCalculateDate);

            if (shift != null && shift.PairCount > 0 && permit.Value > 0)
            {
                #region بررسی غیبت ها
                PairableScndCnpValue intersect = Operation.Intersect(permit, this.DoConcept(3028));
                if (intersect != null && intersect.Value > 0)//در این حالت به میزان عدم حضور لحاظ گردد
                {      
                    AbsentLeave.AddPairs(intersect);
                }
                else
                {
                    AbsentLeave.AddPairs(permit.Pairs.ToList<IPair>());
                }
                if (AbsentLeave.Value > this.DoConcept(1501).Value) 
                {
                    int usedValue = 0;
                    for (int i = 0; i < AbsentLeave.PairCount; i++) 
                    {
                        int notAllow = (usedValue + AbsentLeave.Pairs[i].Value) - this.DoConcept(1501).Value;
                        notAllow = notAllow < 0 ? 0 : notAllow;
                        int allow = AbsentLeave.Pairs[i].Value - notAllow;
                        allow = allow < 0 ? 0 : allow;
                        usedValue += allow;
                        if (notAllow > 0)
                        {
                            AbsentLeave.Pairs[i] = new PairableScndCnpValuePair(AbsentLeave.Pairs[i].From, AbsentLeave.Pairs[i].From + allow);
                        }
                    }
                }
                ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(Operation.Differance(this.DoConcept(3028), AbsentLeave));
                ((PairableScndCnpValue)this.DoConcept(3001)).AddPairs(Operation.Differance(this.DoConcept(3001), AbsentLeave));
                ((PairableScndCnpValue)Result).AppendPairs(AbsentLeave);

                #endregion

                this.ReCalculate(this.ConceptCalculateDate, 3008, 3010, 3014, 3029, 3030, 3031);
            }
        }

        /// <summary>
        /// سقف بی حقوق 12
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="MyConcept"></param>
        public virtual void C1501(BaseScndCnpValue Result, SecondaryConcept MyConcept) 
        {
            Result.Value = 0;
        }

        /// <summary>مفهوم مرخصي با حقوق خالص روزانه_150</summary>
        /// <param name="Result"></param>
        public virtual void C1502(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(150));
            //Result.Value = permit != null && permit.Value == 1 ? 1 : 0;
            if (this.Person.GetShiftByDate(this.ConceptCalculateDate).Value > 0
                ||
                this.DoConcept(1021).Value == 1)
            {
                Result.Value = permit != null && permit.Value == 1 ? 1 : 0;
            }
        }

        /// <summary>مفهوم مرخصي باحقوق روزانه ماهانه-150</summary>
        /// <param name="Result"></param>        
        /// <param name="Result"></param>
        public virtual void C1503(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        /// <summary>مفهوم مرخصي با حقوق خالص روزانه_151</summary>
        /// <param name="Result"></param>
        public virtual void C1504(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Permit permit = this.Person.GetPermitByDate(this.ConceptCalculateDate, EngEnvironment.GetPrecard(151));
            //Result.Value = permit != null && permit.Value == 1 ? 1 : 0;
            if (this.Person.GetShiftByDate(this.ConceptCalculateDate).Value > 0
                ||
                this.DoConcept(1021).Value == 1)
            {
                Result.Value = permit != null && permit.Value == 1 ? 1 : 0;
            }
        }

        /// <summary>مفهوم مرخصي باحقوق روزانه ماهانه-151</summary>
        /// <param name="Result"></param>        
        /// <param name="Result"></param>
        public virtual void C1505(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            PersistedScndCnpPrdValue tmp = this.Person.GetPeriodicScndCnpValue(Result, this.ConceptCalculateDate);
            Result.Value = tmp.Value;
            Result.FromDate = tmp.FromDate;
            Result.ToDate = tmp.ToDate;
            Result.CalculationDate = this.ConceptCalculateDate;
        }

        /// <summary>مفهوم مجموع انواع مرخصی روزانه که به آنها حقوق تعلق میگیرد        
        /// </summary>
        /// <param name="Result"></param>        
        public override  void C1090(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            //انواع مرخصی که به آنها حقوق تعلق میگیرد
            Result.Value = 0;
            Result.Value += this.DoConcept(1005).Value;
            Result.Value += this.DoConcept(1010).Value;
            Result.Value += this.DoConcept(1029).Value;
            Result.Value += this.DoConcept(1031).Value;
            Result.Value += this.DoConcept(1033).Value;
            Result.Value += this.DoConcept(1035).Value;
            Result.Value += this.DoConcept(1037).Value;
            Result.Value += this.DoConcept(1502).Value;
            Result.Value += this.DoConcept(1504).Value;
            if (Result.Value > 0)
            {
                this.DoConcept(2).Value = this.Person.GetShiftByDate(this.RuleCalculateDate).Value;
            }
        }

        /// <summary>مفهوم انواع مرخصی روزانه شامل با حقوق و بی حقوق </summary>
        /// <param name="Result"></param> 
        public override  void C1095(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
            Result.Value += this.DoConcept(1005).Value;
            Result.Value += this.DoConcept(1010).Value;
            Result.Value += this.DoConcept(1029).Value;
            Result.Value += this.DoConcept(1031).Value;
            Result.Value += this.DoConcept(1033).Value;
            Result.Value += this.DoConcept(1035).Value;
            Result.Value += this.DoConcept(1037).Value;
            Result.Value += this.DoConcept(1502).Value;
            Result.Value += this.DoConcept(1504).Value;

            Result.Value += this.DoConcept(1064).Value;
            Result.Value += this.DoConcept(1066).Value;
            Result.Value += this.DoConcept(1068).Value;
            Result.Value += this.DoConcept(1070).Value;
            Result.Value += this.DoConcept(1072).Value;
        }

        /// <summaryمفهوم مجموع مرخصی باحقوق روزانه </summary>
        /// <param name="Result"></param> 
        public override  void C1096(BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            Result.Value = 0;
            Result.Value += this.DoConcept(1029).Value;
            Result.Value += this.DoConcept(1031).Value;
            Result.Value += this.DoConcept(1033).Value;
            Result.Value += this.DoConcept(1035).Value;
            Result.Value += this.DoConcept(1037).Value;
            Result.Value += this.DoConcept(1502).Value;
            Result.Value += this.DoConcept(1504).Value;
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
