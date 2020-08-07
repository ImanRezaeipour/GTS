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
    public interface IHourlyAbsenceBRequest
    {
        IList<MonthlyDetailReportProxy> GetAllHourlyAbsence(string datetime);

        IList<Request> GetAllHourlyLeaveDutyRequests(string datetime);

        IList<Precard> GetAllHourlyLeaves();

        IList<Precard> GetAllHourlyDutis();

        IList<Doctor> GetAllDoctors();

        IList<Illness> GetAllIllness();

        IList<DutyPlace> GetAllDutyPlaceRoot();

        IList<DutyPlace> GetAllDutyPlaceChild(decimal parentId);

        Request InsertRequest(Request request);

        bool DeleteRequest(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckHourlyRequestLoadAccess_onPersonnelLoadStateInGridSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckHourlyRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckHourlyRequestLoadAccess_onManagerLoadStateInGridSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckHourlyRequestLoadAccess_onManagerLoadStateInGanttChartSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        Request InsertHourlyRequest_onPersonnelLoadStateInGridSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        Request InsertHourlyRequest_onPersonnelLoadStateInGanttChartSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        Request InsertHourlyRequest_onManagerLoadStateInGridSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        Request InsertHourlyRequest_onManagerLoadStateInGanttChartSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool DeleteHourlyRequest_onPersonnelLoadStateInGridSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool DeleteHourlyRequest_onPersonnelLoadStateInGanttChartSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool DeleteHourlyRequest_onManagerLoadStateInGridSchema(Request request);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool DeleteHourlyRequest_onManagerLoadStateInGanttChartSchema(Request request);

    }
}
