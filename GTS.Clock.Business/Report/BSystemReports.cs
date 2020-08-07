using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Reporting
{
    public class BSystemReports : MarshalByRefObject
    {
        const string ExceptionSrc = "GTS.Clock.Business.Reporting.BSystemReports";

        public SystemReportsRepository systemReportsRepository
        {
            get
            {
                return new SystemReportsRepository();
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSystemReportsLoadAccess()
        { 
        }

        public int GetSystemReportTypeCount(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemReportTypeCount(SRT, SrtFilterConditions);

            }
            catch (Exception ex)
            {
                BaseBusiness<SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemReportTypeCount");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<SystemBusinessReport> GetSystemBusinessReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemBusinessReportList(SRT, PageSize, PageIndex, SrtFilterConditions);
            }
            catch (Exception ex)
            {
                BaseBusiness<SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemBusinessReportList");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<SystemEngineReport> GetSystemEngineReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemEngineReportList(SRT, PageSize, PageIndex, SrtFilterConditions);
            }
            catch (Exception ex)
            {
                BaseBusiness<SystemReportTypesDataContext>.LogException(ex , "BSystemReports", "GetSystemBusinessReportList");                
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<SystemWindowsServiceReport> GetSystemWindowsServiceReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemWindowsServiceReportList(SRT, PageSize, PageIndex, SrtFilterConditions);
            }
            catch (Exception ex)
            {                
                BaseBusiness<SystemReportTypesDataContext>.LogException(ex , "BSystemReports", "GetSystemWindowsServiceReportList");                
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<SystemUserActionReport> GetSystemUserActionReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemUserActionReportList(SRT, PageSize, PageIndex, SrtFilterConditions);
            }
            catch (Exception ex)
            {                
                BaseBusiness<SystemReportTypesDataContext>.LogException(ex , "BSystemReports", "GetSystemUserActionReportList");                
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void DeleteAllSystemBusinessReport()
        {
            this.DeleteAllSystemReportType<SystemBusinessReport>("BSystemReports", "DeleteAllSystemBusinessReport");
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void DeleteAllSystemEngineReport()
        {
            this.DeleteAllSystemReportType<SystemEngineReport>("BSystemReports", "DeleteAllSystemEngineReport");
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void DeleteAllSystemWindowsServiceReport()
        {
            this.DeleteAllSystemReportType<SystemWindowsServiceReport>("BSystemReports", "DeleteAllSystemWindowsServiceReport");
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void DeleteAllSystemUserActionReport()
        {
            this.DeleteAllSystemReportType<SystemUserActionReport>("BSystemReports", "DeleteAllSystemUserActionReport");
        }

        private void DeleteAllSystemReportType<T>(string ClassName, string MethodName) where T : class
        {
            try
            {
                this.systemReportsRepository.DeleteAllSystemReportType<T>();
            }
            catch (Exception ex)
            {
                BaseBusiness<SystemReportTypesDataContext>.LogException(ex, ClassName, MethodName);
                throw ex;
            }
        }

    }
}
