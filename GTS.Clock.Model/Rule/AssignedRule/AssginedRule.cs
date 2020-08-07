using System;
using System.Collections.Generic;
using System.Linq;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model
{
    public class AssignedRule : BaseRule<AssignedRuleParameter>
    {
        #region Properties
        public virtual decimal RuleId { get; set; }
        public virtual DateTime FromDate
        {
            get;
            set;
        }

        public virtual DateTime ToDate
        {
            get;
            set;
        }

        //public virtual object Memory { get; set; }

        /// <summary>
        /// مقدار پارامتری که نام آن ارسال شده و در تاریخ در حال محاسبه به قانون انتساب داده شده است را برمی گرداند
        /// </summary>
        /// <param name="ParameterName">نام پارامتر</param>
        /// <returns>مقدار پارامتر درخواست شده</returns>
        public virtual BaseRuleParameter this[Func<DateTime, decimal, string, BaseRuleParameter> GetParameter, string ParameterName, DateTime CalcuatoinDate]
        {
            get
            {
                //BaseRuleParameter tmp = GetParameter(CalcuatoinDate, this.RuleId, ParameterName);
                AssignedRuleParameter tmp = this.RuleParameterList
                                                .Where(x => x.Name.ToUpper() == ParameterName.ToUpper() &&
                                                            x.FromDate <= CalcuatoinDate &&
                                                            x.ToDate >= CalcuatoinDate)
                                                .FirstOrDefault();
                if (tmp == null)
                    throw new ArgumentException(String.Format("پارمتری با نام {0} در تاریخ محاسبات {1} به قانون با کد {2} انتساب داده نشده است", ParameterName, Utility.ToPersianDate(CalcuatoinDate.Date), this.IdentifierCode));
                return tmp;
            }
        }

        /// <summary>
        /// مقدار پارامتری که نام آن ارسال شده و در تاریخ در حال محاسبه به قانون انتساب داده شده است را برمی گرداند
        /// </summary>
        /// <param name="ParameterName">نام پارامتر</param>
        /// <returns>مقدار پارامتر درخواست شده</returns>
        public virtual BaseRuleParameter this[ string ParameterName, DateTime CalcuatoinDate]
        {
            get
            {               
                //BaseRuleParameter tmp = GetParameter(CalcuatoinDate, this.RuleId, ParameterName);
                AssignedRuleParameter tmp = this.RuleParameterList
                                                .Where(x => x.Name.ToUpper() == ParameterName.ToUpper() &&
                                                            x.RuleId == this.RuleId &&
                                                            x.FromDate <= CalcuatoinDate &&
                                                            x.ToDate >= CalcuatoinDate)
                                                .FirstOrDefault();
                if (tmp == null)
                    throw new ArgumentException(String.Format("پارمتری با نام {0} در تاریخ محاسبات {1} به قانون با کد {2} انتساب داده نشده است", ParameterName, Utility.ToPersianDate(CalcuatoinDate.Date), this.IdentifierCode));
                return tmp;
            }
        }

      /*
        public virtual BaseRuleParameter this[ string ParameterName, DateTime CalcuatoinDate]
        {
            get
            {
                //BaseRuleParameter tmp = GetParameter(CalcuatoinDate, this.RuleId, ParameterName);
                AssignedRuleParameter tmp = this.RuleParameterList
                                                .Where(x => x.Name.ToUpper() == ParameterName.ToUpper() &&
                                                            x.FromDate <= CalcuatoinDate &&
                                                            x.ToDate >= CalcuatoinDate)

                                             .FirstOrDefault();
                if (tmp == null)
                    throw new ArgumentException(String.Format("پارمتری با نام {0} در تاریخ محاسبات {1} به قانون با کد {2} انتساب داده نشده است", ParameterName, Utility.ToPersianDate(CalcuatoinDate.Date), this.IdentifierCode));
                return tmp;
            }
        }
        */
        /// <summary>
        /// مقدار پارامتری که نام آن ارسال شده و در تاریخ در حال محاسبه به قانون انتساب داده شده است را برمی گرداند
        /// </summary>
        /// <param name="ParameterName">نام پارامتر</param>
        /// <returns>مقدار پارامتر درخواست شده</returns>
        public virtual bool HasParameter(Func<DateTime, decimal, string, BaseRuleParameter> GetParameter,string ParameterName, DateTime CalcuatoinDate)
        {
            //BaseRuleParameter tmp = GetParameter(CalcuatoinDate, this.RuleId, ParameterName);
            AssignedRuleParameter tmp = this.RuleParameterList
                                            .Where(x => x.Name.ToUpper() == ParameterName.ToUpper() &&
                                                        x.FromDate <= CalcuatoinDate &&
                                                        x.ToDate >= CalcuatoinDate)

                                         .FirstOrDefault();
            if (tmp == null)
                return false;
            return true;

        }

        /// <summary>
        /// مقدار پارامتری که نام آن ارسال شده و در تاریخ در حال محاسبه به قانون انتساب داده شده است را برمی گرداند
        /// </summary>
        /// <param name="ParameterName">نام پارامتر</param>
        /// <returns>مقدار پارامتر درخواست شده</returns>
        public virtual bool HasParameter( string ParameterName, DateTime CalcuatoinDate)
        {
            //BaseRuleParameter tmp = GetParameter(CalcuatoinDate, this.RuleId, ParameterName);
            AssignedRuleParameter tmp = this.RuleParameterList
                                            .Where(x => x.Name.ToUpper() == ParameterName.ToUpper() &&
                                                        x.RuleId == this.RuleId &&
                                                        x.FromDate <= CalcuatoinDate &&
                                                        x.ToDate >= CalcuatoinDate)

                                         .FirstOrDefault();
            if (tmp == null)
                return false;
            return true;

        }



        #endregion

        public override string ToString()
        {
            return String.Format("identifier:{0} - {1} -> {2}", IdentifierCode, FromDate, ToDate);
        }
    }
}
