using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Model
{
    public class BaseRuleParameter
    {
        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual string Value
        {
            get;
            set;
        }

        /// <summary>
        /// ادامه در روز بعد پارانترهایی از جنس زمان
        /// در پایگاه داده ذخیره نمیشود
        /// </summary>
        public virtual bool ContinueOnTomorrow { get; set; } 

        public virtual RuleParamType Type
        {
            get;
            set;
        }

        public virtual string Title
        {
            get;
            set;
        }




        #endregion

        #region Methods

        public virtual int ToInt()
        {
            int value = 0;
            if (!int.TryParse(this.Value, out value))
            {
                throw new BaseException(String.Format("Bad parameter type. Name:{0} expected {int} but is {Stirng}", this.Name), "BaseRuleParameter.ToInt()");
            }
            return value;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        public virtual DateTime ToDate()
        {
            try
            {
                return Convert.ToDateTime(this.Value);
            }
            catch
            {
                throw new BaseException(String.Format("Bad parameter type. Name:{0} expected {DateTime} but is {String}", this.Name), "BaseRuleParameter.ToDate()");
            }            
        }

        #endregion

    }
}
