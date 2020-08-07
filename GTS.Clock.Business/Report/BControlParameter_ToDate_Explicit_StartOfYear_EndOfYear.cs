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
    public class BControlParameter_ToDate_Implicit_StartOfYear_EndOfYear : BaseControlParameter, IBControlParameter
    {
        const string ExceptionSrc = "GTS.Clock.Business.Reporting.BControlParameter_ToDate_Explicit_StartOfYear_EndOfYear";

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
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// پارامترها را از رشته تولید شده استخراج میکند
        /// گزارش پارامتر آخر سال دارد که باید پشت کار حساب شود
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
                DateTime fromDate = new DateTime(), toDate = new DateTime(), endOfYear = new DateTime();
                IDictionary<string, object> ParamValues = new Dictionary<string, object>();
                bool toDateOk = false, fromDateOk = false;
                string toDateName = "@toDate",fromDateName = "@fromDate", endOfYearName = "@endOfYear";
                if (actionId.ToLower() == ReportParametersActionId.ToDate_Implicit_StartOfYear_EndOfYear.ToString().ToLower())
                {
                    string[] split = Utility.Spilit(parameters, ';');
                    foreach (string s in split)
                    {
                        string p = s.ToLower();

                        if (p.Contains(toDateName.ToLower()))
                        {
                            toDate = Utility.ToMildiDateTime(p.Replace(toDateName.ToLower() + "=", ""));

                            endOfYear = Utility.GetDateOfEndYear(toDate, BLanguage.CurrentSystemLanguage);

                            toDateOk = true;
                        }
                        if (p.Contains(fromDateName.ToLower()))
                        {
                            fromDate = Utility.ToMildiDateTime(p.Replace(fromDateName.ToLower() + "=", ""));
                            fromDateOk = true;
                        }
                    }
                    if (!(fromDateOk && toDateOk))
                    {
                        throw new ReportParameterIsNotMatchException(UIFatalExceptionIdentifiers.ReportParameterParsingIsNotMatch, "پارامترهای ارسالی جهت بازگشایی کامل نیستند", ExceptionSrc);
                    }
                    else
                    {
                        ParamValues.Add(fromDateName.Replace("@",""), fromDate);
                        ParamValues.Add(toDateName.Replace("@", ""), toDate);
                        ParamValues.Add(endOfYearName.Replace("@", ""), endOfYear);
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

