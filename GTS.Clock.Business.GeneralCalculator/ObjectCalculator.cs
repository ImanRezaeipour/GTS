using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;
using System.Collections;
using GTS.Clock.Model.ELE;

namespace GTS.Clock.Business.Calculator
{
    public class ObjectCalculator
    {
        private Person prs;
        private DateTime minAsgnDate = Utility.GTSMinStandardDateTime;

        #region Constructors
        /// <summary>
        /// ."تنها سازنده کلاس "محاسبه گر اشیاء
        /// </summary>
        public ObjectCalculator(IEngineEnvironment engineEnvironment)
        {
            this.EngEnvironment = engineEnvironment;
        }
        #endregion

        #region Properties

        public string EngineCompanyName
        {
            get { return "Ghadir Public"; }
        }

        public virtual IEngineEnvironment EngEnvironment
        {
            get;
            set;
        }

        public IList<Calendar> CalendarList
        {
            get
            {
                return this.EngEnvironment.CalendarList;
            }
        }

        public IList<SecondaryConcept> ConceptList
        {
            get
            {
                return this.EngEnvironment.ConceptList.Values.ToList<SecondaryConcept>();
            }
        }

        public virtual AssignedRule AssignedRule
        {
            get
            {
                return this.EngEnvironment.AssignedRule;
            }
            set
            {
                this.EngEnvironment.AssignedRule = value;
            }
        }

        public virtual Person Person
        {
            get
            {
                return this.EngEnvironment.Person;
            }
        }

        /// <summary>
        /// تاریخ جاری محاسبه مفاهیم را نگهداری می نماید
        /// در شرایطی که قانونی مفاهیم روزهای گذشته یا آینده را محاسبه کند
        /// این تاریخ با تاریخ اجرای قوانین برابر نخواهد بود
        /// </summary>
        public virtual DateTime ConceptCalculateDate
        {
            get
            {
                return this.EngEnvironment.ConceptCalculateDate;
            }
            set
            {
                this.EngEnvironment.ConceptCalculateDate = value;
            }
        }

        /// <summary>
        /// تاریخ جاری محاسبه قوانین را نگهداری می نماید
        /// در شرایطی که قانونی مفاهیم روزهای گذشته یا آینده را محاسبه کند
        /// این تاریخ با تاریخ اجرای مفاهیم برابر نخواهد بود
        /// </summary>
        public virtual DateTime RuleCalculateDate
        {
            get
            {
                return this.EngEnvironment.RuleCalculateDate;
            }
            set
            {
                this.EngEnvironment.RuleCalculateDate = value;
            }
        }

        public virtual DateRange CalcDateZone
        {
            get
            {
                return this.EngEnvironment.CalcDateZone;
            }
        }

        /// <summary>
        /// تاریخ اولین انتساب قانون
        /// کارایی:میتوان چک کرد که تا کجا به عقب برگردیم و محاسبه نماییم
        /// </summary>
        public virtual DateTime MinAssgnRuleDate
        {
            get
            {
                if (minAsgnDate == Utility.GTSMinStandardDateTime)
                {
                    minAsgnDate = DateTime.Now;
                    if (this.Person.PersonRuleCatAssignList != null && this.Person.PersonRuleCatAssignList.Count > 0)
                    {
                        minAsgnDate = (from n in this.Person.PersonRuleCatAssignList
                                                 select Utility.ToMildiDateTime(n.FromDate)).Min<DateTime>();                       
                    }                    
                }
                return minAsgnDate;
            }
        }

        #endregion

        public BaseRuleParameter GetRuleParameter(DateTime currentDate, decimal ruleId, string paramName) 
        {
            GTS.Clock.Infrastructure.Repository.EntityRepository<AssignRuleParameter> paramRep = new GTS.Clock.Infrastructure.Repository.EntityRepository<AssignRuleParameter>();
            IList<AssignRuleParameter> paramList = paramRep.Find(x => x.Rule.ID == ruleId).ToList();

            AssignRuleParameter asp = paramList.Where(x => x.FromDate <= currentDate && x.ToDate >= currentDate).FirstOrDefault();
            if (asp != null)
            {
                RuleParameter parameter = asp.RuleParameterList.Where(x => x.Name.ToLower().Equals(paramName.ToLower())).FirstOrDefault();

                return parameter;               
            }
            return null;
        }
    }
}
