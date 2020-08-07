using GTS.Clock.Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure.Utility;


namespace GTS.Clock.Infrastructure.Repository
{
    public class SystemReportsRepository
    {
        private SystemReportTypesDataContext srtDataContext = null;
        public SystemReportTypesDataContext SrtDataContext
        {
            get
            {
                if (this.srtDataContext == null)
                    this.srtDataContext = new SystemReportTypesDataContext();
                return this.srtDataContext;
            }
        }

        public int GetSystemReportTypeCount(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            int SystemReportTypeCount = 0;
            switch (SRT)
            {
                case SystemReportType.SystemBusinessReport:
                    System.Linq.Expressions.Expression<Func<SystemBusinessReport, bool>> SystemBusinessReportExpression = (System.Linq.Expressions.Expression<Func<SystemBusinessReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions);
                    SystemReportTypeCount = this.SrtDataContext.SystemBusinessReports.Where(SystemBusinessReportExpression).Count();
                    break;
                case SystemReportType.SystemEngineReport:
                    System.Linq.Expressions.Expression<Func<SystemEngineReport, bool>> SystemEngineReportExpression = (System.Linq.Expressions.Expression<Func<SystemEngineReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions);
                    SystemReportTypeCount = this.SrtDataContext.SystemEngineReports.Where(SystemEngineReportExpression).Count();
                    break;
                case SystemReportType.SystemWindowsServiceReport:
                    System.Linq.Expressions.Expression<Func<SystemWindowsServiceReport, bool>> SystemWindowsServiceReportExpression = (System.Linq.Expressions.Expression<Func<SystemWindowsServiceReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions);
                    SystemReportTypeCount = this.SrtDataContext.SystemWindowsServiceReports.Where(SystemWindowsServiceReportExpression).Count();
                    break;
                case SystemReportType.SystemUserActionReport:
                    System.Linq.Expressions.Expression<Func<SystemUserActionReport, bool>> SystemUserActionReportExpression = (System.Linq.Expressions.Expression<Func<SystemUserActionReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions);
                    SystemReportTypeCount = this.SrtDataContext.SystemUserActionReports.Where(SystemUserActionReportExpression).Count();
                    break;
            }
            return SystemReportTypeCount;
        }

        private object GetSystemReportTypeFilterConditions(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            object expresionObj = null;
            Dictionary<string, DateTime> SystemReportTypeConditionsDatesDic = this.GetSystemReportTypeConditionsDates(SrtFilterConditions);
            DateTime FromDate = SystemReportTypeConditionsDatesDic["FromDate"];
            DateTime ToDate = SystemReportTypeConditionsDatesDic["ToDate"];
            string SearchTerm = SrtFilterConditions.SearchTerm;

            switch (SRT)
            {
                case SystemReportType.SystemBusinessReport:
                    System.Linq.Expressions.Expression<Func<SystemBusinessReport, bool>> SystemBusinessReportExpresion = x => (x.Username.Contains(SearchTerm) ||
                                                                                                                               x.IPAddress.Contains(SearchTerm) ||
                                                                                                                               x.ClassName.Contains(SearchTerm) ||
                                                                                                                               x.MethodName.Contains(SearchTerm) ||
                                                                                                                               x.Message.Contains(SearchTerm) ||
                                                                                                                               x.Level.Contains(SearchTerm) ||
                                                                                                                               x.Exception.Contains(SearchTerm) ||
                                                                                                                               x.ExceptionSource.Contains(SearchTerm)) &&
                                                                                                                               x.Date.Date >= FromDate &&
                                                                                                                               x.Date.Date <= ToDate;
                    expresionObj = (object)SystemBusinessReportExpresion;
                    break;
                case SystemReportType.SystemEngineReport:
                    System.Linq.Expressions.Expression<Func<SystemEngineReport, bool>> SystemEngineReportExpression = x => (x.PersonBarcode.Contains(SearchTerm) ||
                                                                                                                            x.Level.Contains(SearchTerm) ||
                                                                                                                            x.Message.Contains(SearchTerm) ||
                                                                                                                            x.Exception.Contains(SearchTerm)) &&
                                                                                                                            x.Date.Date >= FromDate &&
                                                                                                                            x.Date.Date <= ToDate;
                    expresionObj = (object)SystemEngineReportExpression;
                    break;
                case SystemReportType.SystemWindowsServiceReport:
                    System.Linq.Expressions.Expression<Func<SystemWindowsServiceReport, bool>> SystemWindowsServiceReportExpression = x => (x.Level.Contains(SearchTerm) ||
                                                                                                                                            x.Message.Contains(SearchTerm) ||
                                                                                                                                            x.Exception.Contains(SearchTerm)) &&
                                                                                                                                            x.Date.Date >= FromDate &&
                                                                                                                                            x.Date.Date <= ToDate;
                    expresionObj = (object)SystemWindowsServiceReportExpression;
                    break;
                case SystemReportType.SystemUserActionReport:
                    System.Linq.Expressions.Expression<Func<SystemUserActionReport, bool>> SystemUserActionReportExpression = x => (x.Username.Contains(SearchTerm) ||
                                                                                                                                    x.IPAddress.Contains(SearchTerm) ||
                                                                                                                                    x.PageID.Contains(SearchTerm) ||
                                                                                                                                    x.ClassName.Contains(SearchTerm) ||
                                                                                                                                    x.MethodName.Contains(SearchTerm) ||
                                                                                                                                    x.Action.Contains(SearchTerm) ||
                                                                                                                                    x.ObjectInformation.Contains(SearchTerm)) &&
                                                                                                                                    x.Date.Value.Date >= FromDate &&
                                                                                                                                    x.Date.Value.Date <= ToDate;
                    expresionObj = (object)SystemUserActionReportExpression;
                    break;
            }
            return expresionObj;
        }

        public IList<SystemBusinessReport> GetSystemBusinessReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemBusinessReport, bool>> SystemBusinessReportExpression = (System.Linq.Expressions.Expression<Func<SystemBusinessReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions);
            IList<SystemBusinessReport> SystemBusinessReportList = this.SrtDataContext.SystemBusinessReports.Where(SystemBusinessReportExpression)
                                                                                                            .OrderByDescending(x => x.Date)
                                                                                                            .Skip(PageIndex * PageSize)
                                                                                                            .Take(PageSize)
                                                                                                            .AsEnumerable()
                                                                                                            .Select(x => { x.UIDate = AppLanguageSettings.Language.Name == LanguagesName.Parsi ? Utility.Utility.ToPersianDate(x.Date) : x.Date.ToShortDateString(); return x; })
                                                                                                            .ToList();
            return SystemBusinessReportList;
        }

        public IList<SystemEngineReport> GetSystemEngineReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemEngineReport, bool>> SystemEngineReportExpression = (System.Linq.Expressions.Expression<Func<SystemEngineReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions);
            IList<SystemEngineReport> SystemEngineReportList = this.SrtDataContext.SystemEngineReports.Where(SystemEngineReportExpression)
                                                                                                      .OrderByDescending(x => x.Date)
                                                                                                      .Skip(PageIndex * PageSize)
                                                                                                      .Take(PageSize)
                                                                                                      .AsEnumerable()                                                                                                                 
                                                                                                      .Select(x => { x.UIDate = AppLanguageSettings.Language.Name == LanguagesName.Parsi ? Utility.Utility.ToPersianDate(x.Date) : x.Date.ToShortDateString(); return x; })
                                                                                                      .ToList();
            return SystemEngineReportList;
        }

        public IList<SystemWindowsServiceReport> GetSystemWindowsServiceReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemWindowsServiceReport, bool>> SystemWindowsServiceReportExpression = (System.Linq.Expressions.Expression<Func<SystemWindowsServiceReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions);
            IList<SystemWindowsServiceReport> SystemWindowsServiceReportList = this.SrtDataContext.SystemWindowsServiceReports.Where(SystemWindowsServiceReportExpression)
                                                                                                                              .OrderByDescending(x => x.Date)
                                                                                                                              .Skip(PageIndex * PageSize)
                                                                                                                              .Take(PageSize)
                                                                                                                              .AsEnumerable()
                                                                                                                              .Select(x => { x.UIDate = AppLanguageSettings.Language.Name == LanguagesName.Parsi ? Utility.Utility.ToPersianDate(x.Date) : x.Date.ToShortDateString(); return x; })
                                                                                                                              .ToList();
            return SystemWindowsServiceReportList;
        }

        public IList<SystemUserActionReport> GetSystemUserActionReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemUserActionReport, bool>> SystemUserActionReportExpression = (System.Linq.Expressions.Expression<Func<SystemUserActionReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions);
            IList<SystemUserActionReport> SystemUserActionReportList = this.SrtDataContext.SystemUserActionReports.Where(SystemUserActionReportExpression)
                                                                                                                  .OrderByDescending(x => x.Date)
                                                                                                                  .Skip(PageIndex * PageSize)
                                                                                                                  .Take(PageSize)
                                                                                                                  .AsEnumerable()
                                                                                                                  .Select(x => { x.UIDate = AppLanguageSettings.Language.Name == LanguagesName.Parsi ? x.Date != null ? Utility.Utility.ToPersianDate(x.Date ?? DateTime.MinValue) : string.Empty : x.Date != null ? x.Date.Value.ToShortDateString() : string.Empty; return x; })
                                                                                                                  .ToList();
            return SystemUserActionReportList;
        }       

        public void DeleteAllSystemReportType<T>() where T : class
        {
            this.SrtDataContext.ExecuteCommand("TRUNCATE TABLE " + this.SrtDataContext.Mapping.GetTable(typeof(T)).TableName);
            //this.SrtDataContext.GetTable<T>().DeleteAllOnSubmit<T>(this.SrtDataContext.GetTable<T>());
            //this.SrtDataContext.SubmitChanges();
        }

        private Dictionary<string, DateTime> GetSystemReportTypeConditionsDates(SystemReportTypeFilterConditions SrtFilterConditions)
        {
            Dictionary<string, DateTime> SystemReportTypeConditionsDatesDic = new Dictionary<string, DateTime>();
            ApplicationLanguageSettings appLangSet = this.GetCurrentApplicationLanguageSettings();
            switch (appLangSet.Language.Name)
            {
                case LanguagesName.Parsi:
                    SystemReportTypeConditionsDatesDic.Add("FromDate", SrtFilterConditions.FromDate != string.Empty ? Utility.Utility.ToMildiDate(SrtFilterConditions.FromDate) : (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue);
                    SystemReportTypeConditionsDatesDic.Add("ToDate", SrtFilterConditions.ToDate != string.Empty ? Utility.Utility.ToMildiDate(SrtFilterConditions.ToDate) : (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue);
                    break;
                case LanguagesName.English:
                    SystemReportTypeConditionsDatesDic.Add("FromDate", SrtFilterConditions.FromDate != string.Empty ? Utility.Utility.ToMildiDateTime(SrtFilterConditions.FromDate) : (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue);
                    SystemReportTypeConditionsDatesDic.Add("ToDate", SrtFilterConditions.ToDate != string.Empty ? Utility.Utility.ToMildiDateTime(SrtFilterConditions.ToDate) : (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue);
                    break;
            }
            return SystemReportTypeConditionsDatesDic;
        }

        private ApplicationLanguageSettings GetCurrentApplicationLanguageSettings()
        {
            EntityRepository<ApplicationLanguageSettings> appRep = new EntityRepository<ApplicationLanguageSettings>(false);
            ApplicationLanguageSettings appLangSet = appRep.GetByCriteria(new CriteriaStruct(GTS.Clock.Infrastructure.Utility.Utility.GetPropertyName(() => new ApplicationLanguageSettings().IsActive), true)).FirstOrDefault();
            return appLangSet;
        }
    }


}
