using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model.Report;
using GTS.Clock.Business.AppSettings;
using System.Globalization;
using GTS.Clock.Model;

namespace GTS.Clock.Business.Reporting
{
    public class BControlParameter_YearMonth : BaseControlParameter, IBControlParameter
    {
        const string ExceptionSrc = "GTS.Clock.Business.Reporting.BControlParameter_YearMonth";

        /// <summary>
        /// پارامتر های واسط کاربر را از کنترلها دریافت میکند و پارامترهای فایل گزارش را میسازد
        /// </summary>
        /// <param name="actionId">برای استفاده دوباره در شرایط دیگر از این کنترل , استفاده میشود</param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public string GetParameterValue(decimal fileId,decimal uiParamerId, string actionId, int year, int month)
        {
            try
            {
                DateTime endDate = new DateTime();
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    endDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    endDate = new DateTime(year, month, endOfMonth);
                }
                if (actionId.ToLower().Equals(ReportParametersActionId.PersonDateRange.ToString().ToLower()))
                {
                    int dateRangeOrder = month;
                    DateTime toDate = endDate;

                    string result = String.Format("@Order={0};@ToDate={1};", dateRangeOrder, Utility.ToString(toDate));
                    return result;
                }
                return String.Empty;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
               // GetLog(ex, "BControlParameter_YearMonth", "GetParameterValue", ExceptionSrc);
                throw ex;
            }
        }

        /// <summary>
        /// پارامترها را از رشته تولید شده استخراج میکند
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public IDictionary<string, object> ParsParameter(string parameters, string actionId)
        {
            try
            {
                if (Utility.IsEmpty(parameters))
                    return null;
                int monthOrder = 0;
                DateTime toDate = new DateTime();
                IDictionary<string, object> ParamValues = new Dictionary<string, object>();
                bool monthOrderOk = false, toDateOk = false;
                string toDateName = "@ToDate", dateRangeOrderName = "@Order";
                if (actionId.ToLower() == ReportParametersActionId.PersonDateRange.ToString().ToLower())
                {
                    string[] split = Utility.Spilit(parameters, ';');
                    foreach (string s in split)
                    {
                        string p = s;

                        if (p.Contains(toDateName))
                        {
                            toDate = Utility.ToMildiDateTime(p.Replace(toDateName + "=", ""));
                            toDateOk = true;
                        }
                        if (p.Contains(dateRangeOrderName))
                        {
                            monthOrder = Utility.ToInteger(p.Replace(dateRangeOrderName + "=", ""));
                            monthOrderOk = true;
                        }
                    }
                    if (!(monthOrderOk && toDateOk))
                    {
                        throw new ReportParameterIsNotMatchException(UIFatalExceptionIdentifiers.ReportParameterParsingIsNotMatch, "پارامترهای ارسالی جهت بازگشایی کامل نیستند", ExceptionSrc);
                    }
                    else
                    {
                        ParamValues.Add(dateRangeOrderName, monthOrder);
                        ParamValues.Add(toDateName, toDate);
                    }
                }
                return ParamValues;
            }

            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                //GetLog(ex, "BControlParameter_YearMonth", "ParsParameter", ExceptionSrc);
                throw ex;
            }

        }
    }
}

