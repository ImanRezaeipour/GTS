//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Globalization;
//using GTS.Clock.Infrastructure;
//using GTS.Clock.Infrastructure.Exceptions.UI;
//using GTS.Clock.Business.Security;
//using GTS.Clock.Business.AppSettings;
//using GTS.Clock.Business;
//using GTS.Clock.Business.GTSEngineWS;
//using GTS.Clock.Infrastructure.Repository;

//namespace GTS.Clock.RuleDesigner.UI.Web.Classes
//{
//    ///// <summary>
//    ///// Summary description for DateRangeHelper
//    ///// </summary>
//    //public class DateRangeHelper
//    //{
//    //    private TotalWebServiceClient gtsEngineWS;
//    //    private decimal workingPersonId;
//    //    string Username;


//    //    public class DateRangeOrderProxy
//    //    {
//    //        public string FromDate { get; set; }
//    //        public int Order { get; set; }
//    //        public bool Selected { get; set; }
//    //        public string ToDate { get; set; }
//    //    }

//    //    public DateRangeHelper()
//    //    {
//    //        this.gtsEngineWS = new TotalWebServiceClient();
//    //        this.workingPersonId = 0M;
//    //        this.Username = HttpContext.Current.User.Identity.Name;
//    //    }


//    //    public IList<DateRangeOrderProxy> GetDateRangeOrder(int year)
//    //    {
//    //        IList<DateRangeOrderProxy> DateRangeOrderProxyList = new List<DateRangeOrderProxy>();

//    //        for (int i = 1; i <= 12; i++)
//    //        {
//    //            DateRangeOrderProxy dateRangeOrderProxy = new DateRangeOrderProxy();
//    //            dateRangeOrderProxy.Order = i;
//    //            dateRangeOrderProxy.Selected = false;
//    //            dateRangeOrderProxy.FromDate = year.ToString() + "/" + i.ToString() + "/1";
//    //            string toDateDay = string.Empty;
//    //            if (i <= 6)
//    //                toDateDay = "31";
//    //            else
//    //                if (i < 12)
//    //                    toDateDay = "30";
//    //                else
//    //                {
//    //                    dateRangeOrderProxy.Selected = true;
//    //                    if (new PersianCalendar().IsLeapYear(year))
//    //                        toDateDay = "30";
//    //                    else
//    //                        toDateDay = "29";
//    //                }

//    //            dateRangeOrderProxy.ToDate = year.ToString() + "/" + i.ToString() + "/" + toDateDay;
//    //            DateRangeOrderProxyList.Add(dateRangeOrderProxy);
//    //        }

//    //        return DateRangeOrderProxyList;
//    //    }

//    //    public void GetPersonMonthlyReport(int year, int month, string fromDate, string toDate, out IList<PersonalMonthlyReportRow> DailyRows, out PersonalMonthlyReportRow MonthlyRow)
//    //    {
//    //        try
//    //        {
//    //            DateTime time2;
//    //            DateTime time3;
//    //            if (GTS.Clock.Infrastructure.Utility.Utility.IsEmpty(fromDate) || GTS.Clock.Infrastructure.Utility.Utility.IsEmpty(toDate))
//    //            {
//    //                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.PersonDateRangeIsNotDefiend, string.Format("برای شخص {0} رینج محاسبات تعریف نشده است", this.workingPersonId), "GTS.Clock.Business.WorkedTime.BPersonMonthlyWorkedTime");
//    //            }
//    //            if (!this.IsValidPeson())
//    //            {
//    //                throw new IllegalServiceAccess(string.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.Username), "GTS.Clock.Business.WorkedTime.BPersonMonthlyWorkedTime");
//    //            }
//    //            DateTime date = new DateTime(year, month, GTS.Clock.Infrastructure.Utility.Utility.GetEndOfMiladiMonth(year, month));
//    //            if (this.sysLanguageResource == LanguagesName.Parsi)
//    //            {
//    //                date = GTS.Clock.Infrastructure.Utility.Utility.ToMildiDate(string.Format("{0}/{1}/{2}", year, month, GTS.Clock.Infrastructure.Utility.Utility.GetEndOfPersianMonth(year, month)));
//    //            }
//    //            if (this.sysLanguageResource == LanguagesName.Parsi)
//    //            {
//    //                time2 = GTS.Clock.Infrastructure.Utility.Utility.ToMildiDate(fromDate);
//    //                time3 = GTS.Clock.Infrastructure.Utility.Utility.ToMildiDate(toDate);
//    //            }
//    //            else
//    //            {
//    //                time2 = GTS.Clock.Infrastructure.Utility.Utility.ToMildiDateTime(fromDate);
//    //                time3 = GTS.Clock.Infrastructure.Utility.Utility.ToMildiDateTime(toDate);
//    //            }
//    //            this.gtsEngineWS.GTS_ExecuteByPersonID(BUser.CurrentUser.UserName, this.workingPersonId);
//    //            PersonalMonthlyReport report = new PersonalMonthlyReport(this.workingPersonId, date, month, time2, time3);
//    //            report.LanguageName = this.sysLanguageResource;
//    //            DailyRows = report.PersonalMonthlyReportRows;
//    //            MonthlyRow = DailyRows.FirstOrDefault<PersonalMonthlyReportRow>();
//    //            foreach (PersonalMonthlyReportRow row in DailyRows)
//    //            {
//    //                if (!GTS.Clock.Infrastructure.Utility.Utility.IsEmpty(row.DayStateTitle) && row.DayStateTitle.Contains<char>(';'))
//    //                {
//    //                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
//    //                    {
//    //                        row.DayStateTitle = GTS.Clock.Infrastructure.Utility.Utility.Spilit(row.DayStateTitle, ';')[0].Replace("fa:", "");
//    //                    }
//    //                    else
//    //                    {
//    //                        row.DayStateTitle = GTS.Clock.Infrastructure.Utility.Utility.Spilit(row.DayStateTitle, ';')[1].Replace("en:", "");
//    //                    }
//    //                }
//    //                else
//    //                {
//    //                    row.DayStateTitle = "";
//    //                }
//    //            }
//    //        }
//    //        catch (Exception exception)
//    //        {
//    //            BaseBusiness<Entity>.LogException(exception, "BPersonMonthlyWorkedTime", "GetPersonMonthlyReport");
//    //            throw exception;
//    //        }
//    //    }

//    //    private bool IsValidPeson()
//    //    {
//    //        if (this.workingPersonId > 0M)
//    //        {
//    //            return true;
//    //        }
//    //        if (GTS.Clock.Infrastructure.Utility.Utility.IsEmpty(this.Username))
//    //        {
//    //            this.Username = BUser.CurrentUser.UserName;
//    //        }
//    //        User byUserName = new UserRepository(false).GetByUserName(this.Username);
//    //        if (((byUserName != null) && (byUserName.Person != null)) && (byUserName.Person.ID > 0M))
//    //        {
//    //            this.workingPersonId = byUserName.Person.ID;
//    //            return true;
//    //        }
//    //        return false;
//    //    }


//    //    public LanguagesName sysLanguageResource { get; set; }
//    //}
//}