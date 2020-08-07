using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.RequestFlow
{
    /// <summary>
    /// درخواستهای ثبت شده
    /// مربوط به درخواست دهنده میباشد
    /// </summary>
    public interface IRegisteredRequests
    {
        int GetUserRequestCount(RequestState requestState, int year, int month);

        IList<KartablProxy> GetAllUserRequests(RequestState requestState, int year, int month, int pageIndex, int pageSize);
        IList<ContractKartablProxy> GetAllUserRequests(RequestState requestState, DateTime fromDate, DateTime toDate, decimal personId);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void DeleteRequest(decimal requestId);

        int InsertRequest(Request request,int year,int month);
        int InsertRequest(Request request, int year, int month,decimal personId);

        int InsertCollectiveRequest(Request request, IList<decimal> unckeckedPersons, int year, int month);
        int InsertCollectiveRequest(Request request, PersonAdvanceSearchProxy proxy, IList<decimal> unckeckedPersons, int year, int month);
        int InsertCollectiveRequest(Request request, string quickSearch, IList<decimal> unckeckedPersons, int year, int month);

        IList<Precard> GetAllHourlyRequestTypes();
        IList<Precard> GetAllDailyRequestTypes();
        IList<Precard> GetAllOverTimeRequestTypes();
        IList<Precard> GetAllImperativeRequestTypes();

        IList<Doctor> GetAllDoctors();
        IList<Illness> GetAllIllness();
        IList<DutyPlace> GetAllDutyPlaceRoot();
        IList<DutyPlace> GetAllDutyPlaceChild(decimal parentId);

        int GetFilterUserRequestsCount(UserRequestFilterProxy filter);
        IList<KartablProxy> GetFilterUserRequests(UserRequestFilterProxy filter, int pageIndex, int pageSize);

        bool IsCurrentUserOperator { get; }

        IList<Person> GetAllPerson(int pageIndex, int pageSize);
        IList<Person> QuickSearchByPage(int pageIndex, int pageSize, string searchKey);
        IList<Person> GetPersonInAdvanceSearch(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize);

        int GetPersonCount();
        int GetPersonInQuickSearchCount(string searchValue);
        int GetPersonInAdvanceSearchCount(PersonAdvanceSearchProxy proxy);

        IList<UnderManagmentInfoProxy> GetAllByPage(int pageIndex, int pageSize, int year, int month, string quickSearch);
        IList<UnderManagmentInfoProxy> GetAllByPage(int pageIndex, int pageSize, int year, int month, string quickSearch, IList<decimal> unckeckedPersons);
        IList<UnderManagmentInfoProxy> GetAllByPage(int pageIndex, int pageSize, int year, int month, PersonAdvanceSearchProxy proxy);
        IList<UnderManagmentInfoProxy> GetAllByPage(int pageIndex, int pageSize, int year, int month, PersonAdvanceSearchProxy proxy, IList<decimal> unckeckedPersons);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckRegisteredRequestsLoadAccess_onMainPage();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckRegisteredRequestsLoadAccess_onMonthlyOperationGridSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckRegisteredRequestsLoadAccess_onMonthlyOperationGanttChartSchema();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckRequestRgisterLoadAccess_onNormalUser();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckRequestRgisterLoadAccess_onOperator();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        int InsertSingleHourlyRequestByNormalUser(Request request, int year, int month);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        int InsertSingleDailyRequestByNormalUser(Request request, int year, int month);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        int InsertSingleOverTimeRequestByNormalUser(Request request, int year, int month);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        int InsertSingleHourlyRequestByOperator(Request request, int year, int month, decimal personnelID);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        int InsertSingleDailyRequestByOperator(Request request, int year, int month, decimal perssonnelID);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        int InsertSingleOverTimeRequestByOperator(Request request, int year, int month, decimal personnelID);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void InsertCollectiveHourlyRequestByOperator();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void InsertCollectiveDailyRequestByOperator();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void InsertCollectiveOverTimeRequestByOperator();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        int InsertImperativeRequestByOperator(Request request, ImperativeRequest imperativeRequest, IList<decimal> PersonIDsList);



    }
}
