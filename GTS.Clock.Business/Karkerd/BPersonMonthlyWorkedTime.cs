using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Remoting.Messaging;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Business.Proxy;
using System.Globalization;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.Concepts;
using Microsoft.Practices.Unity;

namespace GTS.Clock.Business.WorkedTime
{
    public class BPersonMonthlyWorkedTime:MarshalByRefObject
    {
        private const string ExceptionSrc = "GTS.Clock.Business.WorkedTime.BPersonMonthlyWorkedTime";
        private PersonRepository personRepository = new PersonRepository(false);
        private LanguagesName sysLanguageResource;
        private LanguagesName localLanguageResource;
        private string Username { get; set; }
        private decimal workingPersonId = 0;
        private Manager manager = new Manager();
        private GTSEngineWS.TotalWebServiceClient gtsEngineWS = new GTS.Clock.Business.GTSEngineWS.TotalWebServiceClient();

        #region Constructor

        [InjectionConstructor]
        public BPersonMonthlyWorkedTime(decimal personId)
        {
            this.sysLanguageResource = AppSettings.BLanguage.CurrentSystemLanguage;
            this.localLanguageResource = AppSettings.BLanguage.CurrentLocalLanguage;
            workingPersonId = personId;
        }

        /// <summary>
        /// برای تست استفاده میشود
        /// </summary>
        /// <param name="username"></param>
        public BPersonMonthlyWorkedTime(string username)
        {
            this.Username = username;
            if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                this.sysLanguageResource = LanguagesName.Parsi;
            }
            else if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.English)
            {
                this.sysLanguageResource = LanguagesName.English;
            }
        } 
        
        #endregion

        /// <summary>
        /// سطرهای گزارش کارکرد ماهانه را برای یک دوره یک ماهه برمیگرداند
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public void GetPersonMonthlyReport(int year, int month, string fromDate, string toDate, out IList<PersonalMonthlyReportRow> DailyRows, out PersonalMonthlyReportRow MonthlyRow)
        {
            try
            {
                if (Utility.IsEmpty(fromDate) || Utility.IsEmpty(toDate))
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.PersonDateRangeIsNotDefiend, String.Format("برای شخص {0} رینج محاسبات تعریف نشده است", workingPersonId), ExceptionSrc);
                }
                if (IsValidPeson())
                {
                    DateTime date = new DateTime(year, month, Utility.GetEndOfMiladiMonth(year, month));
                    if (sysLanguageResource == LanguagesName.Parsi)
                    {
                        date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, Utility.GetEndOfPersianMonth(year, month)));
                    }
                    DateTime from, to;
                    if (sysLanguageResource == LanguagesName.Parsi)
                    {
                        from = Utility.ToMildiDate(fromDate);
                        to = Utility.ToMildiDate(toDate);
                    }
                    else
                    {
                        from = Utility.ToMildiDateTime(fromDate);
                        to = Utility.ToMildiDateTime(toDate);
                    } 
                    gtsEngineWS.GTS_ExecuteByPersonID(BUser.CurrentUser.UserName, this.workingPersonId);
                    PersonalMonthlyReport Result = new PersonalMonthlyReport(this.workingPersonId, date, month, from, to);
                    Result.LanguageName = sysLanguageResource;

                    DailyRows = Result.PersonalMonthlyReportRows;
                    MonthlyRow = DailyRows.FirstOrDefault();

                    foreach (PersonalMonthlyReportRow row in DailyRows)
                    {
                        //Day State Title
                        //fa:{0};en:{1}
                        if (!Utility.IsEmpty(row.DayStateTitle) && row.DayStateTitle.Contains(';'))
                        {
                            if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                            {
                                row.DayStateTitle = Utility.Spilit(row.DayStateTitle, ';')[0].Replace("fa:", "");
                            }
                            else
                            {
                                row.DayStateTitle = Utility.Spilit(row.DayStateTitle, ';')[1].Replace("en:", "");
                            }
                        }
                        else
                        {
                            row.DayStateTitle = "";
                        }                      
                    }
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BPersonMonthlyWorkedTime", "GetPersonMonthlyReport");
                throw ex;
            }
        }

        public IList<PersonalMonthlyReportRow> GetPersonGanttChart(int year, int month, string fromDate, string toDate) 
        {
            IList<PersonalMonthlyReportRow> DailyRows=new List<PersonalMonthlyReportRow>();            

            try
            {
                if (Utility.IsEmpty(fromDate) || Utility.IsEmpty(toDate))
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.PersonDateRangeIsNotDefiend, String.Format("برای شخص {0} رینج محاسبات تعریف نشده است", workingPersonId), ExceptionSrc);
                }
                if (IsValidPeson())
                {                    
                    DateTime from, to;
                    if (sysLanguageResource == LanguagesName.Parsi)
                    {
                        from = Utility.ToMildiDate(fromDate);
                        to = Utility.ToMildiDate(toDate);
                    }
                    else
                    {
                        from = Utility.ToMildiDateTime(fromDate);
                        to = Utility.ToMildiDateTime(toDate);
                    }
                    gtsEngineWS.GTS_ExecuteByPersonID(BUser.CurrentUser.UserName, this.workingPersonId);
                    PersonalMonthlyReport Result = new PersonalMonthlyReport(this.workingPersonId, from, to);
                    Result.LanguageName = sysLanguageResource;

                    DailyRows = Result.PersonalGanttChartRows;                   

                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
                return DailyRows;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BPersonMonthlyWorkedTime", "GetPersonMonthlyReport");
                throw ex;
            }
        }

        /// <summary>
        /// بازه ساعت شبانه روز که در گانت چارت نمایش داده میشود
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void GetMinMaxHourForGanttChart(out int min, out int max) 
        {
            min = 7 * 60;
            max = 23 * 60;
        }

        public PersonalMonthlyReportRow GetPersonDailyReport(DateTime dayDate)
        {
            try
            {
                IList<PersonalMonthlyReportRow> DailyRows = new List<PersonalMonthlyReportRow>();
                
                if (IsValidPeson())
                {
                    PersianDateTime p = new PersianDateTime(dayDate);
                    DateTime endOfMonth;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        endOfMonth = PersianDateTime.GetEndOfShamsiMonth(p.Year, p.Month).GregorianDate;
                    }
                    else 
                    {
                        endOfMonth = new DateTime(dayDate.Year, dayDate.Month, DateTime.DaysInMonth(dayDate.Year, dayDate.Month));
                    }

                    gtsEngineWS.GTS_ExecuteByPersonIdAndToDate(BUser.CurrentUser.UserName, this.workingPersonId, dayDate.AddDays(1));
                    PersonalMonthlyReport Result = new PersonalMonthlyReport(this.workingPersonId, endOfMonth, new PersianDateTime(endOfMonth).Month, dayDate, dayDate);
                    Result.LanguageName = sysLanguageResource;

                    DailyRows = Result.PersonalMonthlyReportRows;

                    return DailyRows.First();
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BPersonMonthlyWorkedTime", "GetPersonDailyReport");
                throw ex;
            }
        }


        /// <summary>
        /// جزیات یک روز را برمیگرداند
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<MonthlyDetailReportProxy> GetPersonMonthlyRowDetail(DateTime date)
        {
            try
            {
                if (IsValidPeson())
                {
                    PrsMonthlyRptRepository prsMonthlyRep = new PrsMonthlyRptRepository(false);
                    List<MonthlyDetailReportProxy> detailList = new List<MonthlyDetailReportProxy>();
                    IList<PersonalMonthlyReportRowDetail> list = prsMonthlyRep.LoadPairableScndcnpValue(this.workingPersonId, date.Date);
                    foreach (PersonalMonthlyReportRowDetail detail in list)
                    {
                        for (int i = 0; i < detail.Pairs.Count; i++)
                        {
                            if (!detail.ScndCnpName.Contains("خالص") && !detail.ScndCnpName.Contains("چارت"))
                            {
                                MonthlyDetailReportProxy proxy = new MonthlyDetailReportProxy();
                                proxy.From = Utility.IntTimeToRealTime(detail.Pairs[i].From);
                                proxy.To = Utility.IntTimeToRealTime(detail.Pairs[i].To);
                                proxy.Name = detail.ScndCnpName;
                                proxy.Color = detail.Color;
                                if (detail.Pairs[i].From > 0 && detail.Pairs[i].To == 0)
                                {
                                    if (sysLanguageResource == LanguagesName.Parsi)
                                    {
                                        proxy.Description = "عدم وجود زوج مرتب بعلت خطای احتمالی در تعریف قوانین";
                                    }
                                    else
                                    {
                                        proxy.Description = "No pair is available maybe caused by rules definition";
                                    }
                                }
                                detailList.Add(proxy);
                            }
                        }
                    }
                    return detailList;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BPersonMonthlyWorkedTime", "GetPersonMonthlyRowDetail");
                throw ex;
            }
        }

        /// <summary>
        /// یک سال را دریافت میکند و 12 ماه را همراه با شروع و پایان آن برمیگرداند
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public IList<DateRangeOrderProxy> GetDateRangeOrder(int year)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    if (IsValidPeson())
                    {
                        IList<DateRangeOrderProxy> list = new List<DateRangeOrderProxy>();


                        for (int i = 1; i <= 12; i++)
                        {
                            DateRangeOrderProxy proxy = new DateRangeOrderProxy();
                            DateTime date = new DateTime(year, i, DateTime.DaysInMonth(year, i));
                            if (sysLanguageResource == LanguagesName.Parsi)
                            {
                                int endOfMonth = new PersianCalendar().GetDaysInMonth(year, i);
                                date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, i, endOfMonth));
                            }
                            PersonalMonthlyReport report = personRepository.GetPersonalMonthlyReport(this.workingPersonId, date, i);
                            if (report.DataRangeIsValid)
                            {
                                proxy.Order = i;
                                proxy.Selected = false;
                                if (sysLanguageResource == LanguagesName.Parsi)
                                {
                                    proxy.FromDate = Utility.ToPersianDate(report.FromDate);
                                    proxy.ToDate = Utility.ToPersianDate(report.ToDate);
                                }
                                else
                                {
                                    proxy.FromDate = report.FromDate;
                                    proxy.ToDate = report.ToDate;
                                }
                                if (report.MinDate.Date <= DateTime.Now && DateTime.Now.Date <= report.MaxDate)
                                {
                                    proxy.Selected = true;
                                }
                                list.Add(proxy);
                            }
                        }
                        list=list.OrderBy(x=>x.Order).ToList();
                        if (list.Where(x => x.Selected).Count() == 0) 
                        {
                            if (Utility.ToPersianDateTime(DateTime.Now).Year == year + 1)//سال قبل
                            {
                                list[list.Count - 1].Selected = true;
                            }
                            else
                            {
                                list[0].Selected = true;
                            }
                        }
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                        return list;
                    }
                    else
                    {
                        throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                    }
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BPersonMonthlyWorkedTime", "GetDateRangeOrder");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// سر ستون رزورو فیلدها را در گزارش کارکرد ماهانه برمیگرداند
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public string GetReservedFieldsName (ConceptReservedFields field)
        {
            try
            {
                string keyColumn = "";
                switch (field)
                {
                    case ConceptReservedFields.ReserveField1:
                        keyColumn = "gridFields_ReserveField1";
                        break;
                    case ConceptReservedFields.ReserveField2:
                        keyColumn = "gridFields_ReserveField2";
                        break;
                    case ConceptReservedFields.ReserveField3:
                        keyColumn = "gridFields_ReserveField3";
                        break;
                    case ConceptReservedFields.ReserveField4:
                        keyColumn = "gridFields_ReserveField4";
                        break;
                    case ConceptReservedFields.ReserveField5:
                        keyColumn = "gridFields_ReserveField5";
                        break;
                    case ConceptReservedFields.ReserveField6:
                        keyColumn = "gridFields_ReserveField6";
                        break;
                    case ConceptReservedFields.ReserveField7:
                        keyColumn = "gridFields_ReserveField7";
                        break;
                    case ConceptReservedFields.ReserveField8:
                        keyColumn = "gridFields_ReserveField8";
                        break;
                    case ConceptReservedFields.ReserveField9:
                        keyColumn = "gridFields_ReserveField9";
                        break;
                    case ConceptReservedFields.ReserveField10:
                        keyColumn = "gridFields_ReserveField10";
                        break;
                }
                SecondaryConceptRepository rep = new SecondaryConceptRepository(false);
                IList<SecondaryConcept> list = rep.Find().
                    Where(x => x.KeyColumnName != null && x.KeyColumnName != ""
                        && x.KeyColumnName == keyColumn).ToList<SecondaryConcept>();
                SecondaryConcept concept = list.FirstOrDefault();
                if (concept != null)
                {
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        return concept.FnName;
                    }
                    else
                    {
                        return concept.EnName;
                    }
                }
                return String.Empty;
            }
            catch (Exception ex) 
            {
                BaseBusiness<Entity>.LogException(ex, this.GetType().Name, "GetReservedFieldsName");
                throw ex;
            }

        }

        /// <summary>
        /// سر ستون رزورو فیلدها را در گزارش کارکرد ماهانه برمیگرداند
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public IDictionary<ConceptReservedFields, string> GetReservedFieldsNames()
        {
            try
            {
                IList<string> reserveFields = new List<string>() { "gridFields_ReserveField1","gridFields_ReserveField2","gridFields_ReserveField3"
                                                                    ,"gridFields_ReserveField4","gridFields_ReserveField5","gridFields_ReserveField6"
                                                                    ,"gridFields_ReserveField7","gridFields_ReserveField8","gridFields_ReserveField9"
                                                                    ,"gridFields_ReserveField10"};


                SecondaryConceptRepository rep = new SecondaryConceptRepository(false);
                IList<SecondaryConcept> list = rep.GetAllByKeyNames(reserveFields);
                list = list == null ? new List<SecondaryConcept>() : list;
                 
                IDictionary<ConceptReservedFields, string> dic = new Dictionary<ConceptReservedFields, string>();
                foreach (string field in reserveFields)
                {
                    ConceptReservedFields keyColumn = new ConceptReservedFields();
                    switch (field)
                    {
                        case "gridFields_ReserveField1":
                            keyColumn = ConceptReservedFields.ReserveField1;
                            break;
                        case "gridFields_ReserveField2":
                            keyColumn = ConceptReservedFields.ReserveField2;
                            break;
                        case "gridFields_ReserveField3":
                            keyColumn = ConceptReservedFields.ReserveField3;
                            break;
                        case "gridFields_ReserveField4":
                            keyColumn = ConceptReservedFields.ReserveField4;
                            break;
                        case "gridFields_ReserveField5":
                            keyColumn = ConceptReservedFields.ReserveField5;
                            break;
                        case "gridFields_ReserveField6":
                            keyColumn = ConceptReservedFields.ReserveField6;
                            break;
                        case "gridFields_ReserveField7":
                            keyColumn = ConceptReservedFields.ReserveField7;
                            break;
                        case "gridFields_ReserveField8":
                            keyColumn = ConceptReservedFields.ReserveField8;
                            break;
                        case "gridFields_ReserveField9":
                            keyColumn = ConceptReservedFields.ReserveField9;
                            break;
                        case "gridFields_ReserveField10":
                            keyColumn = ConceptReservedFields.ReserveField10;
                            break;
                    }

                    SecondaryConcept cnp = list.Where(x => x.KeyColumnName == field).FirstOrDefault();

                    if (cnp != null)
                    {
                        if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                        {
                            dic.Add(keyColumn, cnp.FnName);
                        }
                        else
                        {
                            dic.Add(keyColumn, cnp.EnName);
                        }
                    }
                    else
                    {
                        dic.Add(keyColumn, String.Empty);
                    }
                }
                return dic;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, this.GetType().Name, "GetReservedFieldsNames");
                throw ex;
            }

        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMonthlyOperationGridSchemaLoadAccess_onManagerState()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMonthlyOperationGanttChartSchemaLoadAccess_onManagerState()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMonthlyOperationGridSchemaLoadAccess_onPersonnelState()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMonthlyOperationGanttChartSchemaLoadAccess_onPersonnelState()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMonthlyOperationGridSchemaDetailsRowsAccess_onPersonnelState()
        { 
        }



        #region private methods

        private int GetOrder(DateTime dt, SysLanguageResource sysLanguageResource)
        {
            switch (sysLanguageResource)
            {
                case SysLanguageResource.English: return dt.Month;
                default: return (new PersianDateTime(dt)).Month;
            }
        }

        /// <summary>
        /// اگر شناسه پرسنل صفر باشد باید از شناسه کاربری بازیابی شود
        /// اگر شناسه پرسنل صفر نباشد معنایش این است که از برم مدیریتی گزارش کارکرد وارد شده ایم
        /// و میتوان آنها را اعتبارسنجی کرد
        /// </summary>
        /// <returns></returns>
        private bool IsValidPeson()
        {
            if (workingPersonId > 0)
            {
                return true;       
            }
            else //for testing
            {
                if (Utility.IsEmpty(this.Username)) 
                {
                    this.Username = Security.BUser.CurrentUser.UserName;
                }
                UserRepository userRep = new UserRepository(false);
                Model.Security.User user = userRep.GetByUserName(this.Username);
                if (user != null && user.Person != null && user.Person.ID > 0)
                {
                    this.workingPersonId = user.Person.ID;
                    NHibernateSessionManager.Instance.ClearSession();
                    return true;
                }
            }
            return false;
        }
       

        #endregion

    }
}