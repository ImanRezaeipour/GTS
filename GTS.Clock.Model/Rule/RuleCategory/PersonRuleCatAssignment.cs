using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model
{
    public class PersonRuleCatAssignment : IEntity
    {
        #region Property

        public virtual Decimal ID
        {
            get;
            set;
        }

        public virtual string FromDate
        {
            get;
            set;
        }

        public virtual string ToDate
        {
            get;
            set;
        }


        public virtual decimal PersonId { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual string UIFromDate { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual string UIToDate { get; set; }

        public virtual RuleCategory RuleCategory
        {
            get;
            set;
        }
        
        public virtual Person Person
        {
            get;
            set;
        }

        #endregion

        public override string ToString()
        {
            return String.Format("تخصیص گروه کاری با گروه کاری {0} وبه شخص {1} در تاریخ {2} تا {3} میباشد", this.RuleCategory != null ? this.RuleCategory.Name : "0", this.Person != null ? this.Person.Name : "0", Utility.ToPersianDate(this.FromDate), Utility.ToPersianDate(this.ToDate));
        }
    }
}
