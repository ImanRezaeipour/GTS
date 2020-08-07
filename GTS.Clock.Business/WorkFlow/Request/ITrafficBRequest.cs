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
    public interface ITrafficBRequest
    {
        IList<MonthlyDetailReportProxy> GetAllTrafic(string datetime);

        IList<Request> GetAllTrafficRequests(string datetime);

        IList<Precard> GetAllTraffics();

        Request InsertRequest(Request request);

        bool DeleteRequest(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckTrafficRequestLoadAccess_onPersonnelLoadStateInGridSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckTrafficRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckTrafficRequestLoadAccess_onManagerLoadStateInGridSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckTrafficRequestLoadAccess_onManagerLoadStateInGanttChartSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        Request InsertTrafficRequest_onPersonnelLoadStateInGridSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        Request InsertTrafficRequest_onPersonnelLoadStateInGanttChartSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        Request InsertTrafficRequest_onManagerLoadStateInGridSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        Request InsertTrafficRequest_onManagerLoadStateInGanttChartSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool DeleteTrafficRequest_onPersonnelLoadStateInGridSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool DeleteTrafficRequest_onPersonnelLoadStateInGanttChartSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool DeleteTrafficRequest_onManagerLoadStateInGridSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool DeleteTrafficRequest_onManagerLoadStateInGanttChartSchema(Request request);



    }
}
