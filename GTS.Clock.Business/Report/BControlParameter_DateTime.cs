using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model;

namespace GTS.Clock.Business.Reporting
{
    public class BControlParameter_DateTime
    {
        const string ExceptionSrc = "GTS.Clock.Business.Reporting.BControlParameter_DateTime";

        public string GetParameterValue(decimal fileId, decimal uiParamerId, string actionId, int year, int month, int hour, int minute)
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

                int dateRangeOrder = month;
                DateTime toDate = endDate;
                int TimeMinutes = hour * 60 + minute;

                string result = String.Format("@Order={0};@ToDate={1};@Value={2};", dateRangeOrder, Utility.ToString(toDate), TimeMinutes);
                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

    }
}
