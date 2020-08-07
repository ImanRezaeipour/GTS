using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;

namespace GTS.Clock.Model
{
    public class RuleTemplateParameter: BaseRuleParameter, IEntity
    {
        private string initValue = "";
        
        #region Properties

        public virtual Decimal RuleTemplateId
        {
            get;
            set;
        }

        public virtual Rule rule
        {
            get;
            set;
        }

        /// <summary>
        /// مقدار اولیه پارامتر قالب که در دیتابیس موجود نیست
        /// و تنها جهت سهولت کار در واسط کاربر ایجاد شده است
        /// </summary>
        public virtual string Value
        {
            get
            {
                if (initValue.Length == 0)
                {
                    if (this.Type == RuleParamType.numeric || this.Type == RuleParamType.Time)
                    {
                        initValue = "0";
                    }
                    else
                    {
                        initValue = String.Format("{0:yyyy/mm/dd}", Utility.GTSMinStandardDateTime);
                    }
                }
                return initValue;
            }
            set
            {
                initValue = value;
            }
        }
   

        #endregion

    }
}
