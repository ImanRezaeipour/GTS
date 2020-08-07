using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.AppSettings;
using System.Globalization;

namespace GTS.Clock.Business.RequestFlow
{
    public interface IOverTimeBRequest
    {
        IList<MonthlyDetailReportProxy> GetAllUnallowedOverworks(string datetime);

        IList<Request> GetAllOverTimeRequests(string datetime);

        IList<Precard> GetAllOverWorks();

        Request InsertRequest(Request request);

        bool DeleteRequest(Request request);

        IList<ShiftProxy> GetAllShifts(DateTime datetime);

        IList<ShiftPairProxy> GetShiftDetail(decimal shiftId);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckOverTimeRequestLoadAccess_onPersonnelLoadStateInGridSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckOverTimeRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckOverTimeRequestLoadAccess_onManagerLoadStateInGridSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckOverTimeRequestLoadAccess_onManagerLoadStateInGanttChartSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        Request InsertOverTimeRequest_onPersonnelLoadStateInGridSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        Request InsertOverTimeRequest_onPersonnelLoadStateInGanttChartSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        Request InsertOverTimeRequest_onManagerLoadStateInGridSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        Request InsertOverTimeRequest_onManagerLoadStateInGanttChartSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool DeleteOverTimeRequest_onPersonnelLoadStateInGridSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool DeleteOverTimeRequest_onPersonnelLoadStateInGanttChartSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool DeleteOverTimeRequest_onManagerLoadStateInGridSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool DeleteOverTimeRequest_onManagerLoadStateInGanttChartSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckShiftsViewLoadAccess_onPersonnelLoadStateInGridSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckShiftsViewLoadAccess_onPersonnelLoadStateInGanttChartSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckShiftsViewLoadAccess_onManagerLoadStateInGridSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckShiftsViewLoadAccess_onManagerLoadStateInGanttChartSchema();



    }
}
