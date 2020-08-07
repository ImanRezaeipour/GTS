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

        #endregion
    }
}
