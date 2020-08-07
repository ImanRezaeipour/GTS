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

namespace GTS.Clock.Business.Reporting
{
    public abstract class BaseControlParameter 
    {
        const string ExceptionSrc = "GTS.Clock.Business.Reporting.BControlParameter_YearMonth";

        //protected static void GetLog(Exception ex,string className,string methodName,string exceptionSource)
        //{
        //    BaseBusiness<Report>.LogException(ex, className, methodName, exceptionSource);
        //}

        public static IDictionary<string, object> ParsParameter(string parameters) 
        {
            try
            {
                if (Utility.IsEmpty(parameters))
                    return null;
                DateTime toDate = new DateTime();
                if (!parameters.Contains("@") || !parameters.Contains("=") || !parameters.Contains(";")
                    || Utility.CharOccuranceCount(parameters, "@") != Utility.CharOccuranceCount(parameters, "=")
                    || Utility.CharOccuranceCount(parameters, "@") != Utility.CharOccuranceCount(parameters, ";"))
                {
                    throw new ReportParameterIsNotMatchException(UIFatalExceptionIdentifiers.ReportParameterParsingSplitSign, "فرمت پارامتر های ارسالی جهت ترجمه نادرست است", ExceptionSrc);
                }

                string[] parts = Utility.Spilit(parameters, ';');
                IDictionary<string, object> ParamValues = new Dictionary<string, object>();
                foreach (string part in parts.Where(x=>x.Length>0).ToArray())
                {
                    string[] parmValues = Utility.Spilit(part, '=');
                    if (parmValues.Length == 2 && parmValues[0].Contains('@'))
                    {
                        string paramName = parmValues[0].Replace("@", "");
                        string paramvalue = parmValues[1];
                        ParamValues.Add(paramName, paramvalue);
                    }
                    else
                    {
                        throw new ReportParameterIsNotMatchException(UIFatalExceptionIdentifiers.ReportParameterParsingIsNotMatch, "پارامترهای ارسالی جهت بازگشایی کامل نیستند", ExceptionSrc);
                    }
                }
                return ParamValues;
            }

            catch (Exception ex)
            {
                BaseBusiness<Report>.LogException(ex);
                throw ex;
            }
        }
    }
}
