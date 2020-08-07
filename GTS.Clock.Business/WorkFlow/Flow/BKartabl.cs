using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.AppSettings;
using System.Globalization;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business.Leave;
using GTS.Clock.Business.WorkFlow;
using GTS.Clock.Model.MonthlyReport;

namespace GTS.Clock.Business.RequestFlow
{
    public class BKartabl : MarshalByRefObject, IKartablRequests, IRegisteredRequests, IReviewedRequests
    {
        private const string ExceptionSrc = "GTS.Clock.Business.RequestFlow.BKartabl";
        private RequestRepository requestRep = new RequestRepository(false);
        private RequestStatusRepositiory requestStatusRep = new RequestStatusRepositiory(false);
        IManagerKartabl bManagerKartablUnderManagment = new BUnderManagment();
        ISubstituteKartabl bSubstituteKartablUnderManagment = new BUnderManagment();
        IManagerReviewedRequests bmanagerReviewed = new BUnderManagment();
        IUserRegisteredRequests bUserRegistered = new BUnderManagment();
        IOperatorRegisteredRequests bOpperatorRegistered = new BUnderManagment();
        ISearchPerson searchTool = new BPerson();
        private decimal workingPersonId = 0;
        private decimal workingUserId = 0;
        private string workingUsername = "";

        internal class ManagerComparer:IEqualityComparer<RegisteredRequestsFlowLevel>
        {
            public bool Equals(RegisteredRequestsFlowLevel x, RegisteredRequestsFlowLevel y)
            {
                bool isEqual = false;
                if(x.ManagerID == y.ManagerID)
                    isEqual = true;
                return isEqual;
            }
            public int GetHashCode(RegisteredRequestsFlowLevel obj)
            {
                if (Object.ReferenceEquals(obj, null)) 
                    return 0;
                return obj.ManagerID.GetHashCode();
            }
       }

        /// <summary>
        /// 
        /// </summary>
        public BKartabl()
        {
            GetCurentPersonId();
        }

        /// <summary>
        /// تنها جهت تست
        /// </summary>
        /// <param name="personId"></param>
        public BKartabl(decimal personId, decimal userId, string username)
        {
            this.workingPersonId = personId;
            this.workingUsername = username;
            this.workingUserId = userId;
        }

        #region IRegisteredRequests Members

        /// <summary>
        /// جهت اطلاع رسانی در سرویس
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="date"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        IList<ContractKartablProxy> IRegisteredRequests.GetAllUserRequests(RequestState requestState, DateTime fromDate, DateTime toDate, decimal personId)
        {
            try
            {
                IList<ContractKartablProxy> kartablResult = new List<ContractKartablProxy>();
                IList<InfoRequest> result = new List<InfoRequest>();

                result = bUserRegistered.GetAllRequests(personId, requestState, fromDate.Date, toDate.Date);

                for (int i = 0; i < result.Count; i++)
                {
                    InfoRequest req = result[i];
                    ContractKartablProxy proxy = new ContractKartablProxy();
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                        proxy.TheFromDate = Utility.ToPersianDate(req.FromDate);
                        proxy.TheToDate = Utility.ToPersianDate(req.ToDate);
                    }
                    else
                    {
                        proxy.RegistrationDate = Utility.ToString(req.RegisterDate);
                        proxy.TheFromDate = Utility.ToString(req.FromDate);
                        proxy.TheToDate = Utility.ToString(req.ToDate);
                    }
                    proxy.ID = req.ID;
                    proxy.RequestID = req.ID;
                    proxy.Description = req.Description;
                    proxy.Applicant = req.Applicant;
                    proxy.ManagerDescription = req.ManagerDescription;

                    proxy.TheFromTime = Utility.IntTimeToRealTime(req.FromTime);
                    proxy.TheToTime = Utility.IntTimeToRealTime(req.ToTime);
                    proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);

                    proxy.Row = i + 1;
                    proxy.RequestTitle = req.PrecardName;
                    proxy.Barcode = req.PersonCode;
                    proxy.RequestSource = ContractRequestSource.Undermanagment.ToString();
                    proxy.PersonId = req.PersonID;
                    if (req.Confirm == null)
                    {
                        proxy.FlowStatus = ContractRequestState.UnderReview.ToString();
                    }
                    else if (req.IsDeleted != null && (bool)req.IsDeleted)
                    {
                        proxy.FlowStatus = ContractRequestState.Deleted.ToString();
                    }
                    else if ((bool)req.Confirm)
                    {
                        proxy.FlowStatus = ContractRequestState.Confirmed.ToString();
                    }
                    else
                    {
                        proxy.FlowStatus = ContractRequestState.Unconfirmed.ToString();
                    }

                    if (req.LookupKey.Equals(RequestType.OverWork.ToString().ToLower()))
                    {
                        proxy.RequestType = ContractRequestType.OverWork.ToString();

                        //تنظیم زمان ابتدا و انتها
                        //درخواست بازه ای بدون انتدا و انتها
                        if (req.TimeDuration > 0 && req.FromTime == 1439 && req.ToTime == 1439)
                        {
                            proxy.TheFromTime = proxy.TheToTime = "";
                        }
                    }
                    else if (req.IsDaily)
                    {
                        proxy.RequestType = ContractRequestType.Daily.ToString();
                    }
                    else if (req.IsHourly)
                    {
                        proxy.RequestType = ContractRequestType.Hourly.ToString();
                    }
                    kartablResult.Add(proxy);
                }
                return kartablResult;

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "GetAllUserRequests");
                throw ex;
            }
        }

        #region Count

        /// <summary>
        /// تعداد درخواستهای ثبت شده را برمیگرداند
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int IRegisteredRequests.GetUserRequestCount(RequestState requestState, int year, int month)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }
                //کاربر
                decimal personId = this.GetCurentPersonId();
                decimal userId = this.GetCurentUserId();

                int result = 0;
                if (IsCurrentUserOperator)
                {
                    result = bOpperatorRegistered.GetRequestCount(personId, userId, requestState, fromDate, toDate);
                }
                else
                {
                    result = bUserRegistered.GetRequestCount(personId, requestState, fromDate, toDate);
                }
                return result;

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "GetUserRequestCount");
                throw ex;
            }
        }

        /// <summary>
        /// یک درخواست برای خود اپراتورتور را درج میکند
        /// </summary>
        /// <param name="request"></param>
        /// <returns>تعدا صفحات را بعد از درج برمیگرداند
        /// زیرا لازم است بعد از درج ایندکس به آخرین صفحه منتقل شود</returns>
        int IRegisteredRequests.InsertRequest(Request request, int year, int month)
        {
            try
            {
                request.IsDateSetByUser = true;
                IRegisteredRequests t = new BKartabl();
                BRequest busRequest = new BRequest();
                request = busRequest.InsertRequest(request);

                int count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertRequest");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد درخواستها همرا با فیلتر را برمیگرداند
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        int IRegisteredRequests.GetFilterUserRequestsCount(UserRequestFilterProxy filter)
        {
            try
            {
                DateTime? fromDate = null, toDate = null;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    fromDate = filter.FromDate != null ? Utility.ToMildiDate(filter.FromDate) : (DateTime?)null;
                    toDate = filter.ToDate != null ? Utility.ToMildiDate(filter.ToDate) : (DateTime?)null;
                }
                else
                {
                    fromDate = filter.FromDate != null ? Utility.ToMildiDateTime(filter.FromDate) : (DateTime?)null;
                    toDate = filter.ToDate != null ? Utility.ToMildiDateTime(filter.ToDate) : (DateTime?)null;
                }

                decimal personId = this.GetCurentPersonId();
                decimal userId = this.GetCurentUserId();
                int count = 0;

                if (IsCurrentUserOperator)
                {
                    count = bOpperatorRegistered.GetRequestCountByFilter(personId, userId, filter.UnderManagmentPersonId, filter.RequestType, filter.RequestSubmiter, fromDate, toDate);
                }
                else
                {
                    count = bUserRegistered.GetRequestCountByFilter(personId, filter.RequestType, filter.RequestSubmiter, fromDate, toDate);
                }
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "GetFilterUserRequestCount");
                throw ex;
            }

        }

        #endregion

        /// <summary>
        /// درخواستهای کاربر را برمیگرداند
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<KartablProxy> IRegisteredRequests.GetAllUserRequests(RequestState requestState, int year, int month, int pageIndex, int pageSize)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }
                //کاربر
                decimal personId = this.GetCurentPersonId();
                decimal userId = this.GetCurentUserId();

                IList<KartablProxy> kartablResult = new List<KartablProxy>();
                IList<InfoRequest> result = new List<InfoRequest>();
                if (IsCurrentUserOperator)
                {
                    result = bOpperatorRegistered.GetAllRequests(personId, userId, requestState, fromDate, toDate, pageIndex, pageSize);
                }
                else
                {
                    result = bUserRegistered.GetAllRequests(personId, requestState, fromDate, toDate, pageIndex, pageSize);
                }

                for (int i = 0; i < result.Count; i++)
                {
                    InfoRequest req = result[i];
                    KartablProxy proxy = new KartablProxy();

                    proxy = this.ConvertRegisterRequestToProxy(req);
                    proxy.Row = i + 1;

                    kartablResult.Add(proxy);
                }
                return kartablResult;

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "GetAllUserRequests");
                throw ex;
            }
        }

        /// <summary>
        /// درخواست ها با اعمال فیلتر
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<KartablProxy> IRegisteredRequests.GetFilterUserRequests(UserRequestFilterProxy filter, int pageIndex, int pageSize)
        {
            try
            {
                DateTime? fromDate = null, toDate = null;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    fromDate = filter.FromDate != null ? Utility.ToMildiDate(filter.FromDate) : (DateTime?)null;
                    toDate = filter.ToDate != null ? Utility.ToMildiDate(filter.ToDate) : (DateTime?)null;
                }
                else
                {
                    fromDate = filter.FromDate != null ? Utility.ToMildiDateTime(filter.FromDate) : (DateTime?)null;
                    toDate = filter.ToDate != null ? Utility.ToMildiDateTime(filter.ToDate) : (DateTime?)null;
                }
                //کاربر
                decimal personId = this.GetCurentPersonId();
                decimal userId = this.GetCurentUserId();

                IList<KartablProxy> kartablResult = new List<KartablProxy>();
                IList<InfoRequest> result = new List<InfoRequest>();

                if (IsCurrentUserOperator)
                {

                    result = bOpperatorRegistered.GetAllRequestsByFilter(personId, userId, filter.UnderManagmentPersonId, filter.RequestType, filter.RequestSubmiter, fromDate, toDate, pageIndex, pageSize);
                }
                else
                {
                    result = bUserRegistered.GetAllRequestsByFilter(personId, filter.RequestType, filter.RequestSubmiter, fromDate, toDate, pageIndex, pageSize);
                }


                for (int i = 0; i < result.Count; i++)
                {
                    InfoRequest req = result[i];
                    KartablProxy proxy = new KartablProxy();

                    proxy = this.ConvertRegisterRequestToProxy(req);
                    proxy.Row = i + 1;

                    kartablResult.Add(proxy);
                }
                return kartablResult;

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "GetAllUserRequests");
                throw ex;
            }
        }

        /// <summary>
        /// حذف یک درخواست
        /// </summary>
        /// <param name="requestId"></param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void IRegisteredRequests.DeleteRequest(decimal requestId)
        {
            try
            {
                BRequest bReq = new BRequest();
                bReq.SaveChanges(new Request() { ID = requestId }, UIActionType.DELETE);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "DeleteRequest");
                throw ex;
            }
        }


        #region Operator Insert
        /// <summary>
        /// ثبت درخواست توسط اپراتور
        /// </summary>
        /// <param name="request"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        int IRegisteredRequests.InsertRequest(Request request, int year, int month, decimal personId)
        {
            try
            {
                request.IsDateSetByUser = true;
                IRegisteredRequests t = new BKartabl();
                BRequest busRequest = new BRequest();
                request.Person = new Person() { ID = personId == 0 ? -1 : personId };
                request = busRequest.InsertRequest(request);

                int count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertRequest by Operator");
                throw ex;
            }
        }

        /// <summary>
        /// ثبت تردد انبوه برای همه پرسنل تحت مدیریت
        /// </summary>
        /// <param name="request"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int IRegisteredRequests.InsertCollectiveRequest(Request request, IList<decimal> unckeckedPersons, int year, int month)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInQuickSearchCount("", PersonCategory.Operator_UnderManagment);
                IList<Person> list = searchTool.QuickSearchByPage(0, count, "", PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        where unckeckedPersons.Contains(o.ID) == false
                        select o;
                list = l.ToList<Person>();
                IRegisteredRequests t = new BKartabl();
                BRequest busRequest = new BRequest();
                Request reqObject = new Request();
                foreach (Person prs in list)
                {
                    reqObject = (Request)request.Clone();
                    reqObject.IsDateSetByUser = true;
                    reqObject.Person = prs;
                    reqObject = busRequest.InsertRequest(reqObject);
                }
                count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequest by Operator");
                throw ex;
            }
        }

        /// <summary>
        /// ثبت تردد ابوده توسط اپراتور برای پرسنل انتخابی
        /// </summary>
        /// <param name="request"></param>
        /// <param name="proxy"></param>
        /// <param name="unckeckedPersons">برای این افراد نباید درخواست ثبت شود</param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int IRegisteredRequests.InsertCollectiveRequest(Request request, PersonAdvanceSearchProxy proxy, IList<decimal> unckeckedPersons, int year, int month)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInAdvanceSearchCount(proxy, PersonCategory.Operator_UnderManagment);
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        where unckeckedPersons.Contains(o.ID) == false
                        select o;
                list = l.ToList<Person>();
                IRegisteredRequests t = new BKartabl();
                BRequest busRequest = new BRequest();
                Request reqObject = new Request();
                foreach (Person prs in list)
                {
                    reqObject = (Request)request.Clone();
                    reqObject.IsDateSetByUser = true;
                    reqObject.Person = prs;
                    reqObject = busRequest.InsertRequest(reqObject);
                }
                count = t.GetUserRequestCount(RequestState.UnKnown, year, month);

                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequest by Operator");
                throw ex;
            }
        }

        /// <summary>
        /// ثبت تردد ابوده توسط اپراتور برای پرسنل انتخابی
        /// </summary>
        /// <param name="request"></param>
        /// <param name="proxy"></param>
        /// <param name="unckeckedPersons">برای این افراد نباید درخواست ثبت شود</param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int IRegisteredRequests.InsertCollectiveRequest(Request request, string quickSearch, IList<decimal> unckeckedPersons, int year, int month)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInQuickSearchCount(quickSearch, PersonCategory.Operator_UnderManagment);
                IList<Person> list = searchTool.QuickSearchByPage(0, count, quickSearch, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        where unckeckedPersons.Contains(o.ID) == false
                        select o;
                list = l.ToList<Person>();
                IRegisteredRequests t = new BKartabl();
                BRequest busRequest = new BRequest();
                Request reqObject = new Request();
                foreach (Person prs in list)
                {
                    reqObject = (Request)request.Clone();
                    reqObject.IsDateSetByUser = true;
                    reqObject.Person = prs;
                    reqObject = busRequest.InsertRequest(reqObject);
                }
                count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequest by Operator");
                throw ex;
            }
        }

        #endregion

        #region Operator UnderManagment Grid

        IList<UnderManagmentInfoProxy> IRegisteredRequests.GetAllByPage(int pageIndex, int pageSize, int year, int month, string searchValue)
        {
            try
            {
                DateTime date;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(String.Format("{0}/{1}/1", year, month));
                }
                else
                {
                    date = new DateTime(year, month, 1);
                }
                ISearchPerson searchTool = new BPerson();
                IList<Person> prsList = searchTool.QuickSearchByPage(pageIndex, pageSize, searchValue, PersonCategory.Operator_UnderManagment);
                var l = from prs in prsList
                        select prs.ID;

                IList<UnderManagementPerson> underResult = new List<UnderManagementPerson>();
                ManagerRepository managerRepository = new ManagerRepository(false);
                underResult = managerRepository.GetUnderManagment(month, month > 0 ? 0 : Utility.ToDateRangeIndex(date, BLanguage.CurrentSystemLanguage), date.ToString("yyyy/MM/dd"), l.ToList<decimal>(), pageIndex, pageSize);


                IList<UnderManagmentInfoProxy> Result = new List<UnderManagmentInfoProxy>();
                foreach (UnderManagementPerson under in underResult)
                {
                    CalcInfoProxy calcInfoProxy = new CalcInfoProxy();
                    calcInfoProxy.DailyAbsence = under.DailyAbsence;
                    calcInfoProxy.DailyLeave = under.DailyMeritoriouslyLeave;
                    calcInfoProxy.HourlyAbsence = under.HourlyUnallowableAbsence;
                    calcInfoProxy.HourlyLeave = under.HourlyMeritoriouslyLeave;
                    calcInfoProxy.OverTime = under.AllowableOverTime;

                    Result.Add(new UnderManagmentInfoProxy() { PersonID = under.PersonId, PersonName = under.PersonName, PersonCode = under.BarCode, CalcInfo = calcInfoProxy });
                }
                return Result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        IList<UnderManagmentInfoProxy> IRegisteredRequests.GetAllByPage(int pageIndex, int pageSize, int year, int month, string quickSearch, IList<decimal> unckeckedPersons)
        {
            try
            {
                DateTime date;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(String.Format("{0}/{1}/1", year, month));
                }
                else
                {
                    date = new DateTime(year, month, 1);
                }

                ISearchPerson searchTool = new BPerson();
                IList<Person> list = searchTool.QuickSearchByPage(pageIndex, pageSize, quickSearch, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        where unckeckedPersons.Contains(o.ID) == false
                        select o.ID;

                IList<UnderManagementPerson> underResult = new List<UnderManagementPerson>();
                ManagerRepository managerRepository = new ManagerRepository(false);
                underResult = managerRepository.GetUnderManagment(month, month > 0 ? 0 : Utility.ToDateRangeIndex(date, BLanguage.CurrentSystemLanguage), date.ToString("yyyy/MM/dd"), l.ToList<decimal>(), pageIndex, pageSize);


                IList<UnderManagmentInfoProxy> Result = new List<UnderManagmentInfoProxy>();
                foreach (UnderManagementPerson under in underResult)
                {
                    CalcInfoProxy calcInfoProxy = new CalcInfoProxy();
                    calcInfoProxy.DailyAbsence = under.DailyAbsence;
                    calcInfoProxy.DailyLeave = under.DailyMeritoriouslyLeave;
                    calcInfoProxy.HourlyAbsence = under.HourlyUnallowableAbsence;
                    calcInfoProxy.HourlyLeave = under.HourlyMeritoriouslyLeave;
                    calcInfoProxy.OverTime = under.AllowableOverTime;

                    Result.Add(new UnderManagmentInfoProxy() { PersonID = under.PersonId, PersonName = under.PersonName, PersonCode = under.BarCode, CalcInfo = calcInfoProxy });
                }
                return Result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        IList<UnderManagmentInfoProxy> IRegisteredRequests.GetAllByPage(int pageIndex, int pageSize, int year, int month, PersonAdvanceSearchProxy proxy)
        {
            try
            {
                DateTime date;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(String.Format("{0}/{1}/1", year, month));
                }
                else
                {
                    date = new DateTime(year, month, 1);
                }

                ISearchPerson searchTool = new BPerson();
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, pageIndex, pageSize, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        select o.ID;

                IList<UnderManagementPerson> underResult = new List<UnderManagementPerson>();
                ManagerRepository managerRepository = new ManagerRepository(false);
                underResult = managerRepository.GetUnderManagment(month, month > 0 ? 0 : Utility.ToDateRangeIndex(date, BLanguage.CurrentSystemLanguage), date.ToString("yyyy/MM/dd"), l.ToList<decimal>(), pageIndex, pageSize);


                IList<UnderManagmentInfoProxy> Result = new List<UnderManagmentInfoProxy>();
                foreach (UnderManagementPerson under in underResult)
                {
                    CalcInfoProxy calcInfoProxy = new CalcInfoProxy();
                    calcInfoProxy.DailyAbsence = under.DailyAbsence;
                    calcInfoProxy.DailyLeave = under.DailyMeritoriouslyLeave;
                    calcInfoProxy.HourlyAbsence = under.HourlyUnallowableAbsence;
                    calcInfoProxy.HourlyLeave = under.HourlyMeritoriouslyLeave;
                    calcInfoProxy.OverTime = under.AllowableOverTime;

                    Result.Add(new UnderManagmentInfoProxy() { PersonID = under.PersonId, PersonName = under.PersonName, PersonCode = under.BarCode, CalcInfo = calcInfoProxy });
                }
                return Result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        IList<UnderManagmentInfoProxy> IRegisteredRequests.GetAllByPage(int pageIndex, int pageSize, int year, int month, PersonAdvanceSearchProxy proxy, IList<decimal> unckeckedPersons)
        {
            try
            {
                DateTime date;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(String.Format("{0}/{1}/1", year, month));
                }
                else
                {
                    date = new DateTime(year, month, 1);
                }

                ISearchPerson searchTool = new BPerson();
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, pageIndex, pageSize, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        where unckeckedPersons.Contains(o.ID) == false
                        select o.ID;

                IList<UnderManagementPerson> underResult = new List<UnderManagementPerson>();
                ManagerRepository managerRepository = new ManagerRepository(false);
                underResult = managerRepository.GetUnderManagment(month, month > 0 ? 0 : Utility.ToDateRangeIndex(date, BLanguage.CurrentSystemLanguage), date.ToString("yyyy/MM/dd"), l.ToList<decimal>(), pageIndex, pageSize);


                IList<UnderManagmentInfoProxy> Result = new List<UnderManagmentInfoProxy>();
                foreach (UnderManagementPerson under in underResult)
                {
                    CalcInfoProxy calcInfoProxy = new CalcInfoProxy();
                    calcInfoProxy.DailyAbsence = under.DailyAbsence;
                    calcInfoProxy.DailyLeave = under.DailyMeritoriouslyLeave;
                    calcInfoProxy.HourlyAbsence = under.HourlyUnallowableAbsence;
                    calcInfoProxy.HourlyLeave = under.HourlyMeritoriouslyLeave;
                    calcInfoProxy.OverTime = under.AllowableOverTime;

                    Result.Add(new UnderManagmentInfoProxy() { PersonID = under.PersonId, PersonName = under.PersonName, PersonCode = under.BarCode, CalcInfo = calcInfoProxy });
                }
                return Result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        #endregion

        #region Elemets

        /// <summary>
        /// تمام پیشکارتهای ساعتی که میتوان روی آنها درخواست داد را برمیگرداند
        /// </summary>
        /// <returns></returns>
        IList<Precard> IRegisteredRequests.GetAllHourlyRequestTypes()
        {
            BRequest busRequest = new BRequest();

            List<Precard> list = new List<Precard>();
            list.AddRange(busRequest.GetAllTraffics());
            list.AddRange(busRequest.GetAllHourlyLeaves());
            list.AddRange(busRequest.GetAllHourlyDutis());
            return list;
        }

        /// <summary>
        /// تمام پیشکارتهای روزانه که میتوان روی آنها درخواست داد را برمیگرداند
        /// </summary>
        /// <returns></returns>
        IList<Precard> IRegisteredRequests.GetAllDailyRequestTypes()
        {
            BRequest busRequest = new BRequest();
            List<Precard> list = new List<Precard>();
            list.AddRange(busRequest.GetAllDailyLeaves());
            list.AddRange(busRequest.GetAllDailyDuties());
            return list;
        }

        /// <summary>
        /// تمام پیشکارتهای اضافه کاری که میتوان روی آنها درخواست داد را برمیگرداند
        /// </summary>
        /// <returns></returns>
        IList<Precard> IRegisteredRequests.GetAllOverTimeRequestTypes()
        {
            BRequest busRequest = new BRequest();
            List<Precard> list = new List<Precard>();
            list.AddRange(busRequest.GetAllOverWorks());
            return list;
        }

        IList<Precard> IRegisteredRequests.GetAllImperativeRequestTypes()
        {
            BRequest busRequest = new BRequest();
            List<Precard> list = new List<Precard>();
            list.AddRange(busRequest.GetAllImperatives());
            return list;
        }


        IList<Doctor> IRegisteredRequests.GetAllDoctors()
        {
            BRequest brequest = new BRequest();
            return brequest.GetAllDoctors();
        }

        IList<Illness> IRegisteredRequests.GetAllIllness()
        {
            BRequest brequest = new BRequest();
            return brequest.GetAllIllness();
        }

        IList<DutyPlace> IRegisteredRequests.GetAllDutyPlaceRoot()
        {
            BRequest brequest = new BRequest();
            return brequest.GetAllDutyPlaceRoot();
        }

        IList<DutyPlace> IRegisteredRequests.GetAllDutyPlaceChild(decimal parentId)
        {
            BRequest brequest = new BRequest();
            return brequest.GetAllDutyPlaceChild(parentId);
        }

        #endregion

        /// <summary>
        /// جهت اعمال دسترسی در واسط کاربر
        /// آیا کاربر فعلی اپراتور است
        /// </summary>
        bool IRegisteredRequests.IsCurrentUserOperator
        {
            get
            {
                BOperator op = new BOperator();
                IList<Operator> theOpp = op.GetOperator(this.GetCurentPersonId());
                return theOpp.Count > 0 ? true : false;
            }
        }

        #region IRegisteredRequests Search

        IList<Person> IRegisteredRequests.GetAllPerson(int pageIndex, int pageSize)
        {
            return searchTool.QuickSearchByPage(pageIndex, pageSize, String.Empty, PersonCategory.Operator_UnderManagment);
        }

        IList<Person> IRegisteredRequests.QuickSearchByPage(int pageIndex, int pageSize, string searchKey)
        {
            return searchTool.QuickSearchByPage(pageIndex, pageSize, searchKey, PersonCategory.Operator_UnderManagment);
        }

        IList<Person> IRegisteredRequests.GetPersonInAdvanceSearch(PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize)
        {
            return searchTool.GetPersonInAdvanceSearch(proxy, pageIndex, pageSize, PersonCategory.Operator_UnderManagment);
        }

        int IRegisteredRequests.GetPersonCount()
        {
            return searchTool.GetPersonInQuickSearchCount(String.Empty, PersonCategory.Operator_UnderManagment);
        }

        int IRegisteredRequests.GetPersonInQuickSearchCount(string searchValue)
        {
            return searchTool.GetPersonInQuickSearchCount(searchValue, PersonCategory.Operator_UnderManagment);
        }

        int IRegisteredRequests.GetPersonInAdvanceSearchCount(PersonAdvanceSearchProxy proxy)
        {
            return searchTool.GetPersonInAdvanceSearchCount(proxy, PersonCategory.Operator_UnderManagment);
        }

        #endregion

        #endregion

        #region IKartablRequests Members

        /// <summary>
        /// تعداد درخواستها زا با اعمال شرایط برمیگرداند
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        int IKartablRequests.GetRequestCount(RequestType requestType, int year, int month)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }
                Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                decimal managerID = manager.ID;
                //decimal substitutePersonId = GetCurenttSubstitute();

                int result = bManagerKartablUnderManagment.GetRequestCount(manager.ID, requestType, fromDate, toDate);
                return result;
                /*if (substitutePersonId > 0)
                {
                    int result = bSubstituteKartablUnderManagment.GetRequestCount(managerID, substitutePersonId, requestType, fromDate, toDate);

                    return result;
                }
                else
                {
                    int result = bManagerKartablUnderManagment.GetRequestCount(manager.ID, requestType, fromDate, toDate);
                    return result;
                }*/
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestCount");
                throw ex;
            }
        }

        int IKartablRequests.GetRequestCount(string searchKey, int year, int month)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }
                Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                decimal managerID = manager.ID;
                //decimal substitutePersonId = GetCurenttSubstitute();

                int result = bManagerKartablUnderManagment.GetRequestCount(manager.ID, searchKey, fromDate, toDate);
                return result;

               /* if (substitutePersonId > 0)
                {
                    IList<Person> quciSearchInUnderManagment = searchTool.QuickSearchByPage(0, 100, searchKey, PersonCategory.Substitute_UnderManagment);

                    int result = bSubstituteKartablUnderManagment.GetRequestCount(managerID, substitutePersonId, quciSearchInUnderManagment, fromDate, toDate);

                    return result;
                }
                else
                {
                    IList<Person> quciSearchInUnderManagment = searchTool.QuickSearchByPage(0, 100, searchKey, PersonCategory.Manager_UnderManagment);

                    int result = bManagerKartablUnderManagment.GetRequestCount(manager.ID, searchKey, fromDate, toDate);

                    return result;
                }*/
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestCount");
                throw ex;
            }
        }

        /// <summary>
        /// درخواستها را با اعمال جستجو برمیگرداند
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<KartablProxy> IKartablRequests.GetAllRequests(string searchKey, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }
                IList<KartablProxy> kartablResult = new List<KartablProxy>();
                IList<InfoRequest> result = new List<InfoRequest>();

                Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                decimal managerID = manager.ID;
               // decimal substitutePersonId = GetCurenttSubstitute();
               
                result = bManagerKartablUnderManagment.GetAllRequests(manager.ID, searchKey, fromDate, toDate, pageIndex, pageSize, orderby);

               /* if (substitutePersonId > 0)
                {
                    IList<Person> quciSearchInUnderManagment = searchTool.QuickSearchByPage(0, 100, searchKey, PersonCategory.Substitute_UnderManagment);

                    result = bSubstituteKartablUnderManagment.GetAllRequests(manager.ID, substitutePersonId, quciSearchInUnderManagment, fromDate, toDate, pageIndex, pageSize, orderby);
                }
                else
                {
                    IList<Person> quciSearchInUnderManagment = searchTool.QuickSearchByPage(0, 100, searchKey, PersonCategory.Manager_UnderManagment);

                    result = bManagerKartablUnderManagment.GetAllRequests(manager.ID, searchKey, fromDate, toDate, pageIndex, pageSize, orderby);
                }*/

                for (int i = 0; i < result.Count; i++)
                {
                    InfoRequest req = result[i];
                    KartablProxy proxy = new KartablProxy();                    

                    proxy = this.ConvertKartablRequestToProxy(req);
                    proxy.Row = i + 1;

                    kartablResult.Add(proxy);
                }
                return kartablResult;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllRequests");
                throw ex;
            }
        }

        /// <summary>
        /// درخواستها را با اعمال شرایط برمیگرداند
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<KartablProxy> IKartablRequests.GetAllRequests(RequestType requestType, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }
                IList<KartablProxy> kartablResult = new List<KartablProxy>();
                IList<InfoRequest> result = new List<InfoRequest>();

                Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                decimal managerID = manager.ID;
               /* decimal substitutePersonId = GetCurenttSubstitute();
                if (substitutePersonId > 0)
                {
                    result = bSubstituteKartablUnderManagment.GetAllRequests(manager.ID, substitutePersonId, requestType, fromDate, toDate, pageIndex, pageSize, orderby);
                }
                else
                {
                    result = bManagerKartablUnderManagment.GetAllRequests(manager.ID, requestType, fromDate, toDate, pageIndex, pageSize, orderby);
                }*/

                result = bManagerKartablUnderManagment.GetAllRequests(manager.ID, requestType, fromDate, toDate, pageIndex, pageSize, orderby);
                for (int i = 0; i < result.Count; i++)
                {
                    InfoRequest req = result[i];
                    KartablProxy proxy = new KartablProxy();                    
                    proxy = this.ConvertKartablRequestToProxy(req);
                    proxy.Row = i + 1;

                    kartablResult.Add(proxy);
                }
                return kartablResult;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllRequests");
                throw ex;
            }
        }

        /// <summary>
        /// جهت سرویس اطلاع رسانی
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IList<ContractKartablProxy> IKartablRequests.GetAllRequests(decimal personId)
        {
            try
            {
                IList<ContractKartablProxy> kartablResult = new List<ContractKartablProxy>();
                IList<InfoRequest> result = new List<InfoRequest>();

                Manager manager = new BManager().GetManager(personId);
                decimal managerID = manager.ID;
                decimal substitutePersonId = GetCurenttSubstitute(personId);
                if (substitutePersonId > 0)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    result = bManagerKartablUnderManagment.GetAllRequests(manager.ID);
                }

                for (int i = 0; i < result.Count; i++)
                {
                    InfoRequest req = result[i];
                    ContractKartablProxy proxy = new ContractKartablProxy();
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                        proxy.TheFromDate = Utility.ToPersianDate(req.FromDate);
                        proxy.TheToDate = Utility.ToPersianDate(req.ToDate);
                    }
                    else
                    {
                        proxy.RegistrationDate = Utility.ToString(req.RegisterDate);
                        proxy.TheFromDate = Utility.ToString(req.FromDate);
                        proxy.TheToDate = Utility.ToString(req.ToDate);
                    }
                    proxy.ID = req.ID;
                    proxy.RequestID = req.ID;
                    proxy.ManagerFlowID = req.mngrFlowID;
                    proxy.TheFromTime = Utility.IntTimeToRealTime(req.FromTime);
                    proxy.TheToTime = Utility.IntTimeToRealTime(req.ToTime);
                    proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);
                    proxy.Row = i + 1;
                    proxy.RequestTitle = req.PrecardName;
                    proxy.Description = req.Description;
                    proxy.Applicant = req.Applicant;
                    proxy.Barcode = req.PersonCode;
                    proxy.OperatorUser = req.OperatorUser;
                    proxy.RequestSource = ContractRequestSource.Undermanagment.ToString();
                    proxy.PersonId = req.PersonID;
                    kartablResult.Add(proxy);
                }
                return kartablResult;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllRequests");
                throw ex;
            }
        }

        /// <summary>
        /// تایید یا عدم تایید
        /// </summary>
        /// <param name="requsts"></param>
        /// <param name="status">تایید یا عدم تایید</param>
        /// <param name="description">توضیح جهت  عدم تایید درخواست</param>
        bool IKartablRequests.SetStatusOfRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description)
        {
            if (status != RequestState.Confirmed && status != RequestState.Unconfirmed)
                return false;

            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    description = Utility.IsEmpty(description) ? "" : description;
                    bool endFlow = false;
                    bool confirm = false;
                    EntityRepository<RequestStatus> rsRep = new EntityRepository<RequestStatus>(false);
                    EntityRepository<ManagerFlow> mngFlwRep = new EntityRepository<ManagerFlow>(false);
                    BRequest requestBusiness = new BRequest();

                    var mngFlowIds = from req in requests
                                     group req by req.ManagerFlowID;

                    foreach (var mngFlw in mngFlowIds)
                    {
                        decimal mngFlwId = mngFlw.Key;
                        var list = from r in requests
                                   where r.ManagerFlowID == mngFlwId
                                   select r.RequestID;
                        if (status == RequestState.Confirmed)
                        {
                            ManagerFlow mf = mngFlwRep.GetById(mngFlwId, false);
                            bool existsNextLevel = mngFlwRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => mf.Flow), mf.Flow),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => mf.Level), mf.Level + 1, CriteriaOperation.GreaterEqThan),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => mf.Active), true)) > 0;
                            endFlow = existsNextLevel == false;
                            confirm = true;
                        }
                        else if (status == RequestState.Unconfirmed)
                        {
                            endFlow = true;
                            confirm = false;
                        }
                        foreach (decimal reqId in list)
                        {
                            if (rsRep.Find().Where(x => x.EndFlow && x.Request.ID == reqId).Count() == 0)
                            {
                                RequestStatus rs = new RequestStatus();
                                rs.ManagerFlow = new ManagerFlow() { ID = mngFlwId };
                                rs.Request = new Request() { ID = reqId };
                                rs.Confirm = confirm;
                                rs.EndFlow = endFlow;
                                rs.Description = description;
                                rs.Date = DateTime.Now;
                                rsRep.WithoutTransactSave(rs);
                                if (endFlow && confirm)
                                {
                                    Request request = new BRequest().GetByID(reqId);
                                    this.SavePermit(request);
                                }
                                ///ImperativeRequest Update : IsLocked = false
                                if (!confirm)
                                {
                                    Request request = requestBusiness.GetByID(reqId);
                                    ChangeImperativeRequestLockState(request);
                                }
                            }
                        }
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return true;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfRequest");
                    throw ex;
                }
            }
        }

        private void ChangeImperativeRequestLockState(Request request)
        {
            BImperativeRequest imperativeRequestBusiness = new BImperativeRequest();
            if (request.Precard.IsMonthly && request.Precard.PrecardGroup.IntLookupKey == 6)
            {
                int year = 0;
                int month = 0;
                ImperativeRequest ImperativeRequest = new ImperativeRequest();
                switch (BLanguage.CurrentSystemLanguage)
                {
                    case LanguagesName.Parsi:
                        PersianCalendar pCal = new PersianCalendar();
                        year = pCal.GetYear(request.FromDate);
                        month = pCal.GetMonth(request.FromDate);
                        break;
                    case LanguagesName.English:
                        year = request.FromDate.Year;
                        month = request.FromDate.Month;
                        break;
                }
                ImperativeRequest imperativeRequest = new ImperativeRequest()
                {
                    Person = new Person() { ID = request.Person.ID },
                    Precard = new Precard() { ID = request.Precard.ID },
                    Year = year,
                    Month = month
                };
                ImperativeRequest impReq = imperativeRequestBusiness.GetImperativeRequest(imperativeRequest);
                if (impReq != null)
                {
                    impReq.IsLocked = false;
                    imperativeRequestBusiness.SaveChanges(impReq, UIActionType.EDIT);
                }
            }
        }

        /// <summary>
        /// پیشینه یک درخواست
        /// </summary>
        /// <param name="requestId"></param>
        KartablRequestHistoryProxy IKartablRequests.GetRequestHistory(decimal requestId)
        {
            KartablRequestHistoryProxy proxy = new KartablRequestHistoryProxy();
            Request request = requestRep.GetById(requestId, false);
            if (request == null)
            {
                throw new ItemNotExists("درخواست موردنظر در دیتابیس موجود نمیباشد", ExceptionSrc);
            }
            IList<Request> list = requestRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => request.Person), request.Person),
                                          new CriteriaStruct(Utility.GetPropertyName(() => request.Precard), request.Precard),
                                          new CriteriaStruct(Utility.GetPropertyName(() => request.ToDate), request.ToDate, CriteriaOperation.LessThan)).OrderBy(x => x.ToDate).ToList();
            #region Hourly Items
            if (request.Precard.IsHourly)
            {
                if (list.Count > 0)
                {
                    Request r = list.Last();

                    if (r.FromTime >= 0)
                        proxy.From = Utility.IntTimeToTime(r.FromTime);
                    if (r.ToTime >= 0)
                        proxy.To = Utility.IntTimeToTime(r.ToTime);
                }
                //در مورد درخواستهای تردد بی معنی است
                if (!request.Precard.PrecardGroup.LookupKey.ToLower().Equals(PrecardGroupsName.traffic.ToString().ToLower()))
                {
                    DateTime date = request.ToDate;
                    DateTime monthStart = new DateTime();
                    DateTime monthEnd = new DateTime();
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = new PersianCalendar().GetDaysInMonth(Utility.ToPersianDateTime(date).Year, Utility.ToPersianDateTime(date).Month);
                        monthStart = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, Utility.ToPersianDateTime(date).Month, 1));
                        monthEnd = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, Utility.ToPersianDateTime(date).Month, endOfMonth));
                    }
                    else
                    {
                        monthStart = new DateTime(date.Year, date.Month, 1);
                        monthEnd = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                    }
                    list = requestRep.GetAllUsedRequest(request.Person.ID, request.Precard.ID, monthStart, monthEnd);

                    var a = from o in list
                            select o.ToTime - o.FromTime;
                    int sum = a.Sum();
                    if (sum >= 0)
                        proxy.UesedInMonth = sum == 0 ? "00:00" : Utility.IntTimeToTime(sum);

                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = new PersianCalendar().GetDaysInMonth(Utility.ToPersianDateTime(date).Year, 12);
                        monthStart = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, 1, 1));
                        monthEnd = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, 12, endOfMonth));
                    }
                    else
                    {
                        monthStart = new DateTime(date.Year, 1, 1);
                        monthEnd = new DateTime(date.Year, 12, DateTime.DaysInMonth(date.Year, date.Month));
                    }
                    list = requestRep.GetAllUsedRequest(request.Person.ID, request.Precard.ID, monthStart, monthEnd);

                    var b = from o in list
                            select o.ToTime - o.FromTime;
                    sum = b.Sum();
                    if (sum >= 0)
                        proxy.UesedInYear = sum == 0 ? "00:00" : Utility.IntTimeToTime(sum);

                }
            }
            #endregion

            #region Daily Items
            else if (request.Precard.IsDaily)
            {
                if (list.Count > 0)
                {
                    Request r = list.Last();
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.From = Utility.ToPersianDate(r.FromDate);
                        proxy.To = Utility.ToPersianDate(r.ToDate);
                    }
                    else
                    {
                        proxy.From = Utility.ToString(r.FromDate);
                        proxy.To = Utility.ToString(r.ToDate);
                    }
                }

                DateTime date = request.ToDate;
                DateTime monthStart = new DateTime();
                DateTime monthEnd = new DateTime();
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = new PersianCalendar().GetDaysInMonth(Utility.ToPersianDateTime(date).Year, Utility.ToPersianDateTime(date).Month);
                    monthStart = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, Utility.ToPersianDateTime(date).Month, 1));
                    monthEnd = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, Utility.ToPersianDateTime(date).Month, endOfMonth));
                }
                else
                {
                    monthStart = new DateTime(date.Year, date.Month, 1);
                    monthEnd = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                }
                list = requestRep.GetAllUsedRequest(request.Person.ID, request.Precard.ID, monthStart, monthEnd);

                var a = from o in list
                        select (o.ToDate - o.FromDate).Days;
                int sum = a.Sum();
                if (sum >= 0)
                    proxy.UesedInMonth = sum.ToString();

                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = new PersianCalendar().GetDaysInMonth(Utility.ToPersianDateTime(date).Year, 12);
                    monthStart = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, 1, 1));
                    monthEnd = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, 12, endOfMonth));
                }
                else
                {
                    monthStart = new DateTime(date.Year, 1, 1);
                    monthEnd = new DateTime(date.Year, 12, DateTime.DaysInMonth(date.Year, date.Month));
                }
                list = requestRep.GetAllUsedRequest(request.Person.ID, request.Precard.ID, monthStart, monthEnd);

                var b = from o in list
                        select (o.ToDate - o.FromDate).Days;
                sum = b.Sum();
                if (sum >= 0)
                    proxy.UesedInYear = sum.ToString();
            }
            #endregion

            #region Leave
            if (request.Precard.PrecardGroup.LookupKey.Equals(PrecardGroupsName.leave.ToString()))
            {
                proxy.IsLeave = true;
                int year = 0, month = 0;
                ILeaveInfo leaveInfo = new BRemainLeave();

                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    year = Utility.ToPersianDateTime(request.ToDate).Year;
                    month = Utility.ToPersianDateTime(request.ToDate).Month;
                }
                else
                {
                    year = request.ToDate.Year;
                    month = request.ToDate.Month;
                }
                int mDay, mMinutes, yDay, yMinutes;
                GTSEngineWS.TotalWebServiceClient gtsEngineWS = new GTS.Clock.Business.GTSEngineWS.TotalWebServiceClient();
                gtsEngineWS.GTS_ExecuteByPersonID(BUser.CurrentUser.UserName, request.Person.ID);
                leaveInfo.GetRemainLeaveToEndOfMonth(request.Person.ID, year, month, out mDay, out mMinutes);
                leaveInfo.GetRemainLeaveToEndOfYear(request.Person.ID, year, month, out yDay, out yMinutes);

                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.RemainLeaveInMonth = String.Format(" {0} روز و {1} ساعت", mDay.ToString(), mMinutes == 0 ? "00:00" : Utility.IntTimeToTime(mMinutes));
                    proxy.RemainLeaveInYear = String.Format(" {0} روز و {1} ساعت", yDay.ToString(), mMinutes == 0 ? "00:00" : Utility.IntTimeToTime(yMinutes));
                }
                else
                {
                    proxy.RemainLeaveInMonth = String.Format(" {0} day and {1} hours", mDay.ToString(), mMinutes == 0 ? "00:00" : Utility.IntTimeToTime(mMinutes));
                    proxy.RemainLeaveInYear = String.Format(" {0} day and {1} hours", yDay.ToString(), mMinutes == 0 ? "00:00" : Utility.IntTimeToTime(yMinutes));
                }
            }
            #endregion
            return proxy;
        }

        /// <summary>
        ///   مراحل جریان و وضعیت درخواست در هریک را نشان میدهد
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="managerFlowId"></param>
        /// <returns></returns>
        IList<KartablFlowLevelProxy> IKartablRequests.GetRequestLevelsByManagerFlowID(decimal requestId, decimal managerFlowId)
        {
            IList<KartablFlowLevelProxy> KartablFlowLevelProxyList = new List<KartablFlowLevelProxy>();
            try
            {
                KartablFlowLevelProxyList = this.GetRequestLevelsByOperationFlow(requestId, managerFlowId);
                return KartablFlowLevelProxyList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestLevels");
                throw ex;
            }
        }

        IList<KartablFlowLevelProxy> IKartablRequests.GetRequestLevelsByPersonnelID(decimal requestId, decimal personnelID)
        {
            IList<KartablFlowLevelProxy> KartablFlowLevelProxyList = new List<KartablFlowLevelProxy>();
            try
            {
                KartablFlowLevelProxyList = this.GetRequestLevelsByPendingFlow(requestId, personnelID);
                return KartablFlowLevelProxyList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestLevels");
                throw ex;
            }
        }

        private IList<KartablFlowLevelProxy> GetRequestLevelsByOperationFlow(decimal requestId, decimal managerFlowId)
        {
            IList<KartablFlowLevelProxy> result = new List<KartablFlowLevelProxy>();
            EntityRepository<ManagerFlow> mngFlwRep = new EntityRepository<ManagerFlow>(false);
            EntityRepository<RequestStatus> rsRep = new EntityRepository<RequestStatus>(false);
            ManagerFlow mf = mngFlwRep.GetById(managerFlowId, false);
            IList<ManagerFlow> managerFlowList = mngFlwRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => mf.Flow), mf.Flow));
            managerFlowList = managerFlowList.Where(x => x.Active).OrderBy(x => x.Level).ToList();

            /*  var deletedList = from k in managerFlowList
                                from stat in k.StatusList
                                where stat.Request.ID == requestId && stat.IsDeleted
                                select stat.IsDeleted;
*/
            /*
                            int deletedCount = rsRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new RequestStatus().IsDeleted), true),
                                                                        new CriteriaStruct(Utility.GetPropertyName(() => new RequestStatus().Request), new Request() { ID = requestId }));

                            bool isDeleted = deletedCount > 0;*/
            foreach (ManagerFlow mngf in managerFlowList)
            {
                KartablFlowLevelProxy proxy = new KartablFlowLevelProxy();
                proxy.ManagerName = mngf.Manager.ThePerson.Name;
                IList<RequestStatus> rsList = rsRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new RequestStatus().ManagerFlow), mngf),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => new RequestStatus().Request), new Request() { ID = requestId }));
                if (rsList.Count > 0)
                {
                    RequestStatus rs = rsList.First();
                    proxy.Description = rs.Description;
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        proxy.TheDate = Utility.ToPersianDate(rs.Date);
                    }
                    else
                    {
                        proxy.TheDate = Utility.ToString(rs.Date);
                    }
                    if (rs.IsDeleted)
                        proxy.RequestStatus = RequestState.Deleted;
                    else if (rs.Confirm)
                        proxy.RequestStatus = RequestState.Confirmed;
                    else
                        proxy.RequestStatus = RequestState.Unconfirmed;
                }
                else
                {
                    proxy.RequestStatus = RequestState.UnderReview;
                }
                result.Add(proxy);
            }
            return result;
        }

        private IList<KartablFlowLevelProxy> GetRequestLevelsByPendingFlow(decimal requestId, decimal personnelId)
        {
            IList<KartablFlowLevelProxy> KartablFlowLevelProxyList = new List<KartablFlowLevelProxy>();
            IList<RegisteredRequestsFlowLevel> RegisteredRequestsFlowLevelList = this.requestStatusRep.GetRequestLevelsByPendingFlow(requestId, personnelId);
            RegisteredRequestsFlowLevelList = RegisteredRequestsFlowLevelList.Distinct(new ManagerComparer()).ToList();
            foreach (RegisteredRequestsFlowLevel registeredRequestsFlowLevelItem in RegisteredRequestsFlowLevelList)
            {
                KartablFlowLevelProxyList.Add(new KartablFlowLevelProxy() { ManagerName = registeredRequestsFlowLevelItem.ManagerName });
            }
            return KartablFlowLevelProxyList;
        }

        /// <summary>
        /// تعداد فلتر کارتابل
        /// </summary>
        /// <param name="fliters"></param>      
        int IKartablRequests.GetRequestsByFilterCount(IList<RequestFliterProxy> fliters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// فلتر کارتابل
        /// </summary>
        /// <param name="fliters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        IList<KartablProxy> IKartablRequests.GetAllRequestsByFilter(IList<RequestFliterProxy> fliters, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region IReviewedRequests Members

        /// <summary>
        /// تعداد درخواستها را با اعمال شرایط برمیگرداند
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        int IReviewedRequests.GetRequestCount(RequestState requestState, int year, int month)
        {
            try
            {
                if (GetCurentPersonId() > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                        fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                        toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                    }
                    else
                    {
                        int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                        fromDate = new DateTime(year, month, 1);
                        toDate = new DateTime(year, month, endOfMonth);
                    }
                    //مدیر
                    Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                    decimal managerID = manager.ID;
                    int result = bmanagerReviewed.GetRequestCount(managerID, requestState, fromDate, toDate);
                    return result;
               
                    #region comment
                    /*  decimal substitutePersonId = GetCurenttSubstitute();
                    if (substitutePersonId > 0 && new BSubstitute().GetSubstituteManager(substitutePersonId) > 0)
                    {
                        managerID = new BSubstitute().GetSubstituteManager(substitutePersonId);
                        int result = bSubstituteReviewedRequest.GetRequestCount(managerID, substitutePersonId, requestState, fromDate, toDate);
                        return result;
                    }
                    else if (managerID > 0)
                    {
                        int result = bmanagerReviewed.GetRequestCount(managerID, requestState, fromDate, toDate);
                        return result;
                    }
                    else
                    {
                        throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد1", this.workingUsername), ExceptionSrc);
                    }*/
                    
                    #endregion
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد2", this.workingUsername), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestCount");
                throw ex;
            }
        }

        int IReviewedRequests.GetRequestCount(string searchKey, int year, int month)
        {
            try
            {
                if (GetCurentPersonId() > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                        fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                        toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                    }
                    else
                    {
                        int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                        fromDate = new DateTime(year, month, 1);
                        toDate = new DateTime(year, month, endOfMonth);
                    }
                    //مدیر

                    Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                    decimal managerID = manager.ID;
                    int result = bmanagerReviewed.GetRequestCount(managerID, searchKey, fromDate, toDate);
                    return result;
                    #region comment
                    /*  decimal substitutePersonId = GetCurenttSubstitute();

                    if (substitutePersonId > 0 && new BSubstitute().GetSubstituteManager(substitutePersonId) > 0)
                    {
                        //int count = searchTool.GetPersonInQuickSearchCount(searchKey, PersonCategory.Substitute_UnderManagment);
                        //if (count == 0)
                        //    return 0;
                        //IList<Person> quciSearchInUnderManagment = searchTool.QuickSearchByPage(0, count, searchKey, PersonCategory.Substitute_UnderManagment);

                        IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch(searchKey, PersonCategory.Substitute_UnderManagment);
                        if (quciSearchInUnderManagment == null || quciSearchInUnderManagment.Count == 0)
                            return 0;

                        managerID = new BSubstitute().GetSubstituteManager(substitutePersonId);
                        int result = bSubstituteReviewedRequest.GetRequestCount(managerID, substitutePersonId, quciSearchInUnderManagment, fromDate, toDate);
                        return result;
                    }
                    else if (managerID > 0)
                    {
                        IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch(searchKey, PersonCategory.Manager_UnderManagment);
                        if (quciSearchInUnderManagment == null || quciSearchInUnderManagment.Count == 0)
                            return 0;
                        int result = bmanagerReviewed.GetRequestCount(managerID, quciSearchInUnderManagment, fromDate, toDate);
                        return result;
                    }
                    else
                    {
                        throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.workingUsername), ExceptionSrc);
                    }*/
                    
                    #endregion
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.workingUsername), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestCount");
                throw ex;
            }
        }

        IList<KartablProxy> IReviewedRequests.GetAllRequests(string searchKey, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            try
            {
                if (GetCurentPersonId() > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                        fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                        toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                    }
                    else
                    {
                        int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                        fromDate = new DateTime(year, month, 1);
                        toDate = new DateTime(year, month, endOfMonth);
                    }

                    //مدیر

                    IList<KartablProxy> kartablResult = new List<KartablProxy>();
                    IList<InfoRequest> result = new List<InfoRequest>();
                    Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                    decimal managerID = manager.ID;
                    result = bmanagerReviewed.GetAllRequests(managerID, searchKey, fromDate, toDate, pageIndex, pageSize, orderby);
                    #region comment
                    /*  decimal substitutePersonId = GetCurenttSubstitute();

                    if (substitutePersonId > 0 && new BSubstitute().GetSubstituteManager(substitutePersonId) > 0)
                    {
                        //int count = searchTool.GetPersonInQuickSearchCount(searchKey, PersonCategory.Substitute_UnderManagment);
                        //if (count == 0)
                        //{
                        //    return new List<KartablProxy>();
                        //}
                        IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch(searchKey, PersonCategory.Substitute_UnderManagment);
                        if (quciSearchInUnderManagment == null || quciSearchInUnderManagment.Count == 0) 
                        {
                            return new List<KartablProxy>();
                        }
                        managerID = new BSubstitute().GetSubstituteManager(substitutePersonId);
                        result = bSubstituteReviewedRequest.GetAllRequests(managerID, substitutePersonId, quciSearchInUnderManagment, fromDate, toDate, pageIndex, pageSize, orderby);
                    }
                    else if (managerID > 0)
                    {
                        //int count = searchTool.GetPersonInQuickSearchCount(searchKey, PersonCategory.Manager_UnderManagment);
                        //if (count == 0)
                        //{
                        //    return new List<KartablProxy>();
                        //}
                        IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch(searchKey, PersonCategory.Manager_UnderManagment);
                        if (quciSearchInUnderManagment == null || quciSearchInUnderManagment.Count == 0)
                        {
                            return new List<KartablProxy>();
                        }
                        result = bmanagerReviewed.GetAllRequests(managerID, quciSearchInUnderManagment, fromDate, toDate, pageIndex, pageSize, orderby);

                    }
                    else
                    {
                        throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.workingUsername), ExceptionSrc);
                    }*/
                    
                    #endregion

                    for (int i = 0; i < result.Count; i++)
                    {
                        InfoRequest req = result[i];
                        KartablProxy proxy = new KartablProxy();                        
                        proxy = this.ConvertReviewdRequestToProxy(req);
                        proxy.Row = i + 1;

                        kartablResult.Add(proxy);
                    }
                    return kartablResult;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.workingUsername), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllRequests");
                throw ex;
            }
        }

        /// <summary>
        /// جهت نمایش در درخواستهای بررسی شده
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<KartablProxy> IReviewedRequests.GetAllRequests(RequestState requestState, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            try
            {
                if (GetCurentPersonId() > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                        fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                        toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                    }
                    else
                    {
                        int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                        fromDate = new DateTime(year, month, 1);
                        toDate = new DateTime(year, month, endOfMonth);
                    }

                    //مدیر                   
                    IList<KartablProxy> kartablResult = new List<KartablProxy>();
                    IList<InfoRequest> result = new List<InfoRequest>();
                    Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                    decimal managerID = manager.ID;
                    result = bmanagerReviewed.GetAllRequests(managerID, requestState, fromDate, toDate, pageIndex, pageSize, orderby);

                    #region Comment
                    /*  decimal substitutePersonId = GetCurenttSubstitute();

                    if (substitutePersonId > 0 && new BSubstitute().GetSubstituteManager(substitutePersonId) > 0)
                    {
                        managerID = new BSubstitute().GetSubstituteManager(substitutePersonId);
                        result = bSubstituteReviewedRequest.GetAllRequests(managerID, substitutePersonId, requestState, fromDate, toDate, pageIndex, pageSize, orderby);
                    }
                    else if (managerID > 0)
                    {
                        result = bmanagerReviewed.GetAllRequests(managerID, requestState, fromDate, toDate, pageIndex, pageSize, orderby);
                    }
                    else
                    {
                        throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.workingUsername), ExceptionSrc);
                    }*/
                    
                    #endregion

                    for (int i = 0; i < result.Count; i++)
                    {
                        InfoRequest req = result[i];
                        KartablProxy proxy = new KartablProxy();
  
                        proxy = this.ConvertReviewdRequestToProxy(req);
                        proxy.Row = i + 1;

                        kartablResult.Add(proxy);
                    }
                    return kartablResult;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.workingUsername), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllRequests");
                throw ex;
            }
        }   

        /// <summary>
        /// حذف یک درخواست توسط مدیر
        /// </summary>
        /// <param name="requestId"></param>
        void IReviewedRequests.DeleteRequst(decimal requestId, string managerDescription)
        {
            try
            {
                BRequest busRequest = new BRequest();
                Request request = busRequest.GetByID(requestId);
                Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                RequestStatus status = request.RequestStatusList.Where(x => x.ManagerFlow.Manager.ID == manager.ID).ToList().FirstOrDefault();
                if (status == null)
                {
                    decimal substitutePersonId = GetCurenttSubstitute();
                    if (substitutePersonId > 0)
                    {
                        decimal managerID = new BSubstitute().GetSubstituteManager(substitutePersonId);
                        status = request.RequestStatusList.Where(x => x.ManagerFlow.Manager.ID == managerID).ToList().FirstOrDefault();
                    }
                }
                if (status != null)
                {
                    status.IsDeleted = true;
                    status.Date = DateTime.Now;
                    status.Description = managerDescription;
                    busRequest.SaveChanges(request, UIActionType.EDIT);

                    new BPermit().DeleteByRequestId(requestId, request.Person.ID, request.FromDate);

                    ///ImperativeRequest Update : IsLocked = false
                    ChangeImperativeRequestLockState(request);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "DeleteRequst");
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// تعداد درخواستها را با اعمال شرایط برمیگرداند
        /// اگر شخص مدیر نباشد خالی برمیگردد
        /// جهت استفاده در خلاصه وضعیت
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public int GetManagerKartablRequestCount(int year)
        {
            try
            {
                int fromMonth = 1;
                int toMonth = 12;
                if (GetCurentPersonId() > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = Utility.GetEndOfPersianMonth(year, toMonth);
                        fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, fromMonth, 1));
                        toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, toMonth, endOfMonth));
                    }
                    else
                    {
                        int endOfMonth = Utility.GetEndOfMiladiMonth(year, toMonth);
                        fromDate = new DateTime(year, fromMonth, 1);
                        toDate = new DateTime(year, toMonth, endOfMonth);
                    }
                    //نمایش آیتم سالهای پیش
                    fromDate = fromDate.AddYears(-1);

                    //مدیر
                    Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                    if (manager.ID > 0)
                    {
                        int result = bManagerKartablUnderManagment.GetRequestCount(manager.ID, RequestType.None, fromDate, toDate);

                        return result;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetManagerKartablRequestCount");
                throw ex;
            }

        }

        /// <summary>
        /// تعداد درخواستها را با اعمال شرایط برمیگرداند
        /// اگر شخص مدیر نباشد خالی برمیگردد
        /// جهت استفاده در خلاصه وضعیت
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public int GetSubstituteKartablRequestCount(int year)
        {
            try
            {
                int fromMonth = 1;
                int toMonth = 12;
                if (GetCurentPersonId() > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = Utility.GetEndOfPersianMonth(year, toMonth);
                        fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, fromMonth, 1));
                        toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, toMonth, endOfMonth));
                    }
                    else
                    {
                        int endOfMonth = Utility.GetEndOfMiladiMonth(year, toMonth);
                        fromDate = new DateTime(year, fromMonth, 1);
                        toDate = new DateTime(year, toMonth, endOfMonth);
                    }

                    //نمایش آیتم سالهای پیش
                    fromDate = fromDate.AddYears(-1);

                    //جانشین
                    decimal substitutePersonId = GetCurenttSubstitute();
                    if (substitutePersonId > 0)
                    {
                        SubstituteRepository rep = new SubstituteRepository(false);
                        int result = bSubstituteKartablUnderManagment.GetRequestCount(rep.GetSubstitute(substitutePersonId).First().Manager.ID, substitutePersonId, fromDate, toDate);
                        return result;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetSubstituteKartablRequestCount");
                throw ex;
            }

        }


        /// <summary>
        /// کاربر فعلی 
        /// </summary>
        /// <returns></returns>
        private decimal GetCurentPersonId()
        {
            if (workingPersonId == 0)
            {
                Model.Security.User user = BUser.CurrentUser;
                if (user != null)
                {
                    this.workingPersonId = user.Person.ID;
                    this.workingUsername = user.UserName;
                }
            }
            return workingPersonId;
        }

        /// <summary>
        /// کاربر فعلی 
        /// </summary>
        /// <returns></returns>
        private decimal GetCurentUserId()
        {
            if (workingUserId == 0)
            {
                Model.Security.User user = BUser.CurrentUser;
                if (user != null)
                {
                    this.workingUserId = user.ID;
                    this.workingUsername = user.UserName;
                }
            }
            return workingUserId;
        }

        /// <summary>
        /// آیا کاربر فعلی جانشین است
        /// </summary>
        /// <returns></returns>
        private decimal GetCurenttSubstitute()
        {
            SubstituteRepository rep = new SubstituteRepository(false);
            if (rep.IsSubstitute(this.workingPersonId))
                return this.workingPersonId;
            else
                return 0;
        }

        /// <summary>
        /// آیا کاربر فعلی جانشین است
        /// </summary>
        /// <returns></returns>
        public decimal GetCurenttSubstitute(decimal personId)
        {
            SubstituteRepository rep = new SubstituteRepository(false);
            if (rep.IsSubstitute(personId))
                return this.workingPersonId;
            else
                return 0;
        }

        /// <summary>
        /// ثبت درخواست بعنوان مجوز
        /// اگر درخواست اضافه کار و یا ساعتی باشد و مجوزی با همان پیشکارت و تاریخ موجود باشد به همان زوج مرتب اضافه میکند
        /// </summary>
        /// <param name="request"></param>
        private void SavePermit(Request request)
        {
            BPermit busPermit = new BPermit();
            Permit permit = new Permit();
            PermitPair permitPair = new PermitPair();
            Precard precard = new PrecardRepository().GetById(request.Precard.ID, false);
            if (precard.PrecardGroup == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.PrecardGroupIsNull, "گروه پیشکارت دستوری خالی است", ExceptionSrc);
            }
            string name = precard.PrecardGroup.LookupKey;
            PrecardGroupsName groupName = (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), name);

            IList<Permit> list = busPermit.GetExistingPermit(request.Person.ID, request.Precard.ID, request.FromDate, request.ToDate);
            if (list.Count > 0 && (groupName == PrecardGroupsName.overwork || request.Precard.IsHourly))
            {
                permit = list.First();
                if (permit.Pairs == null)
                    permit.Pairs = new List<PermitPair>();
                if (permit.Pairs.Where(x => x.RequestID == request.ID).Count() > 0)
                    return;
                permitPair.Permit = permit;
                permitPair.RequestID = request.ID;
                permitPair.From = request.FromTime;
                permitPair.To = request.ToTime;
                permitPair.Value = request.TimeDuration;
                permitPair.PreCardID = request.Precard.ID;
                permitPair.IsFilled = true;
                permit.Pairs.Add(permitPair);

                busPermit.SaveChanges(permit, UIActionType.EDIT);
            }
            else
            {
                if (groupName == PrecardGroupsName.overwork)
                {
                    permit.IsPairly = false;
                    if (request.ToTime - request.FromTime == request.TimeDuration)
                    {
                        permit.IsPairly = true;
                    }
                }
                else if (request.Precard.IsHourly)
                {
                    permit.IsPairly = true;
                }
                else
                {
                    permit.IsPairly = false;
                }

                permit.FromDate = request.FromDate;
                permit.ToDate = request.ToDate;
                permit.Pairs = new List<PermitPair>() { permitPair };
                permit.Person = request.Person;

                permitPair.Permit = permit;
                permitPair.RequestID = request.ID;
                permitPair.From = request.FromTime;
                permitPair.To = request.ToTime;
                permitPair.Value = request.TimeDuration;
                permitPair.PreCardID = request.Precard.ID;
                permitPair.IsFilled = true;

                busPermit.SaveChanges(permit, UIActionType.ADD);
            }

        }

        /// <summary>
        /// جهت اعمال دسترسی در واسط کاربر
        /// آیا کاربر فعلی اپراتور است
        /// بعلت مشکل در تست کردن دوبار نوشته شده است
        /// </summary>
        private bool IsCurrentUserOperator
        {
            get
            {
                BOperator op = new BOperator();
                IList<Operator> opList = op.GetOperator(this.GetCurentPersonId());
                return opList.Count > 0 ? true : false;
            }
        }

        public void GetAllKartablData(DateTime fromDate, DateTime toDate, RequestType requestType, decimal managerId, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            this.requestRep.GetAllKartablData(fromDate, toDate, RequestType.None, managerId, pageIndex, pageSize, orderby);
        }

        private KartablProxy ConvertKartablRequestToProxy(InfoRequest req)
        {
            KartablProxy proxy = new KartablProxy();
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                proxy.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                proxy.TheFromDate = Utility.ToPersianDate(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.Parsi);
                proxy.TheToDate = Utility.ToPersianDate(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.Parsi);
            }
            else
            {
                proxy.RegistrationDate = Utility.ToString(req.RegisterDate);
                proxy.TheFromDate = Utility.ToString(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.English);
                proxy.TheToDate = Utility.ToString(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.English);
            }
            proxy.ID = req.ID;
            proxy.RequestID = req.ID;
            proxy.ManagerFlowID = req.mngrFlowID;

            proxy.TheFromTime = Utility.IntTimeToRealTime(req.FromTime);
            proxy.TheToTime = Utility.IntTimeToRealTime(req.ToTime);
            if (!req.IsMonthly)
                proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);
            else
                proxy.TheDuration = req.TimeDuration.ToString();

            proxy.RequestTitle = req.PrecardName;
            proxy.Description = req.Description;
            proxy.Applicant = req.ApplicantFirstName + " " + req.ApplicantLastName;
            if (Utility.IsEmpty(proxy.Applicant))
            {
                proxy.Applicant = req.Applicant;
            }
            proxy.Barcode = req.PersonCode;
            proxy.OperatorUser = req.OperatorUser;
            proxy.RequestSource = RequestSource.Undermanagment;
            proxy.PersonId = req.PersonID;
            string name = req.LookupKey;
            PrecardGroupsName groupName = (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), name);
            if (groupName == PrecardGroupsName.overwork)
            {
                proxy.RequestType = RequestType.OverWork;

                //تنظیم زمان ابتدا و انتها
                //درخواست بازه ای بدون انتدا و انتها
                if (req.TimeDuration > 0 && req.FromTime == 1439 && req.ToTime == 1439)
                {
                    proxy.TheFromTime = proxy.TheToTime = "";
                }
            }
            else if (groupName == PrecardGroupsName.imperative)
            {
                proxy.RequestType = RequestType.Imperative;
            }
            else if (req.IsHourly)
            {
                proxy.RequestType = RequestType.Hourly;
            }
            else if (req.IsDaily)
            {
                proxy.RequestType = RequestType.Daily;
            }
            else if (req.IsMonthly)
            {
                proxy.RequestType = RequestType.Monthly;
            }
            else
            {
                proxy.RequestType = RequestType.None;
            }
            proxy.AttachmentFile = req.AttachmentFile;
            proxy.PersonImage = req.PersonImage;
            return proxy;
        }

        private KartablProxy ConvertRegisterRequestToProxy(InfoRequest req)
        {
            KartablProxy proxy = new KartablProxy();
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                proxy.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                proxy.TheFromDate = Utility.ToPersianDate(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.Parsi);
                proxy.TheToDate = Utility.ToPersianDate(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.Parsi);
            }
            else
            {
                proxy.RegistrationDate = Utility.ToString(req.RegisterDate);
                proxy.TheFromDate = Utility.ToString(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.English);
                proxy.TheToDate = Utility.ToString(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.English);
            }
            proxy.ID = req.ID;
            proxy.RequestID = req.ID;
            proxy.ManagerFlowID = req.mngrFlowID;

            proxy.TheFromTime = req.FromTime == 0 ? "" : Utility.IntTimeToRealTime(req.FromTime);
            proxy.TheToTime = Utility.IntTimeToRealTime(req.ToTime);
            proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);

            proxy.RequestTitle = req.PrecardName;
            proxy.Description = req.Description;
            proxy.ManagerDescription = req.ManagerDescription;
            proxy.Applicant = req.Applicant;
            if (Utility.IsEmpty(proxy.Applicant))
            {
                proxy.Applicant = req.Applicant;
            }
            proxy.Barcode = req.PersonCode;
            proxy.OperatorUser = req.OperatorUser;
            proxy.RequestSource = RequestSource.Undermanagment;
            proxy.PersonId = req.PersonID;
            if (req.Confirm == null)
            {
                proxy.FlowStatus = RequestState.UnderReview;
            }
            else if (req.IsDeleted != null && (bool)req.IsDeleted)
            {
                proxy.FlowStatus = RequestState.Deleted;
            }
            else if ((bool)req.Confirm)
            {
                proxy.FlowStatus = RequestState.Confirmed;
            }
            else
            {
                proxy.FlowStatus = RequestState.Unconfirmed;
            }

            if (req.LookupKey.Equals(RequestType.OverWork.ToString().ToLower()))
            {
                proxy.RequestType = RequestType.OverWork;

                //تنظیم زمان ابتدا و انتها
                //درخواست بازه ای بدون انتدا و انتها
                if (req.TimeDuration > 0 && req.FromTime == 1439 && req.ToTime == 1439)
                {
                    proxy.TheFromTime = proxy.TheToTime = "";
                }
            }
            else if (req.IsDaily)
            {
                proxy.RequestType = RequestType.Daily;
            }
            else if (req.IsHourly)
            {
                proxy.RequestType = RequestType.Hourly;
            }
            proxy.AttachmentFile = req.AttachmentFile;
            return proxy;
        }

        private KartablProxy ConvertReviewdRequestToProxy(InfoRequest req)
        {
            KartablProxy proxy = new KartablProxy();
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)

            {
                proxy.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                proxy.TheFromDate = Utility.ToPersianDate(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.Parsi);
                proxy.TheToDate = Utility.ToPersianDate(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.Parsi);
            }
            else
            {
                proxy.RegistrationDate = Utility.ToString(req.RegisterDate);
                proxy.TheFromDate = Utility.ToString(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.English);
                proxy.TheToDate = Utility.ToString(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.English);
            }
            proxy.ID = req.ID;
            proxy.RequestID = req.ID;
            proxy.ManagerFlowID = req.mngrFlowID;


            proxy.TheFromTime = req.FromTime == 0 ? "" : Utility.IntTimeToRealTime(req.FromTime);
            proxy.TheToTime = Utility.IntTimeToRealTime(req.ToTime);
            if (!req.IsMonthly)
                proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);
            else
                proxy.TheDuration = req.TimeDuration.ToString();

            proxy.RequestTitle = req.PrecardName;
            proxy.Description = req.Description;
            proxy.Applicant = req.Applicant;
            proxy.Barcode = req.PersonCode;
            proxy.OperatorUser = req.OperatorUser;
            proxy.RequestSource = RequestSource.Undermanagment;
            proxy.PersonId = req.PersonID;
            if (req.IsDeleted != null && (bool)req.IsDeleted)
            {
                proxy.FlowStatus = RequestState.Deleted;
            }
            else if (req.Confirm == null)
            {
                proxy.FlowStatus = RequestState.UnderReview;
            }
            else if (req.Confirm != null && (bool)req.Confirm)
            {
                proxy.FlowStatus = RequestState.Confirmed;
            }
            else if (req.Confirm != null)
            {
                proxy.FlowStatus = RequestState.Unconfirmed;
            }

            if (req.LookupKey.Equals(RequestType.OverWork.ToString().ToLower()))
            {
                proxy.RequestType = RequestType.OverWork;
            }
            else if (req.LookupKey.Equals(RequestType.Imperative.ToString().ToLower()))
            {
                proxy.RequestType = RequestType.Imperative;
            }
            else if (req.IsDaily)
            {
                proxy.RequestType = RequestType.Daily;
            }
            else if (req.IsHourly)
            {
                proxy.RequestType = RequestType.Hourly;
            }
            else if (req.IsMonthly)
            {
                proxy.RequestType = RequestType.Monthly;
            }
            proxy.AttachmentFile = req.AttachmentFile;
            proxy.PersonImage = req.PersonImage;
            return proxy;
        }  

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRegisteredRequestsLoadAccess_onMainPage()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRegisteredRequestsLoadAccess_onMonthlyOperationGridSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRegisteredRequestsLoadAccess_onMonthlyOperationGanttChartSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRequestRgisterLoadAccess_onNormalUser()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRequestRgisterLoadAccess_onOperator()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleHourlyRequestByNormalUser(Request request, int year, int month)
        {
            return ((IRegisteredRequests)(new BKartabl())).InsertRequest(request, year, month);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleDailyRequestByNormalUser(Request request, int year, int month)
        {
            return ((IRegisteredRequests)(new BKartabl())).InsertRequest(request, year, month);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleOverTimeRequestByNormalUser(Request request, int year, int month)
        {
            return ((IRegisteredRequests)(new BKartabl())).InsertRequest(request, year, month);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleHourlyRequestByOperator(Request request, int year, int month, decimal personnelID)
        {
            return ((IRegisteredRequests)(new BKartabl())).InsertRequest(request, year, month, personnelID);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleDailyRequestByOperator(Request request, int year, int month, decimal personnelID)
        {
            return ((IRegisteredRequests)(new BKartabl())).InsertRequest(request, year, month, personnelID);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleOverTimeRequestByOperator(Request request, int year, int month, decimal personnelID)
        {
            return ((IRegisteredRequests)(new BKartabl())).InsertRequest(request, year, month, personnelID);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void InsertCollectiveHourlyRequestByOperator()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void InsertCollectiveDailyRequestByOperator()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void InsertCollectiveOverTimeRequestByOperator()
        {
        }

        //[ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        int IRegisteredRequests.InsertImperativeRequestByOperator(Request request, ImperativeRequest imperativeRequest, IList<decimal> PersonIDsList)
        {
            try
            {
                IRegisteredRequests kartableBusiness = new BKartabl();
                BRequest requestBusiness = new BRequest();
                BImperativeRequest imperativeRequestBusiness = new BImperativeRequest();

                foreach (decimal personID in PersonIDsList)
                {
                    imperativeRequest.Person = new Person() { ID = personID };
                    ImperativeRequest impReq = imperativeRequestBusiness.GetImperativeRequest(imperativeRequest);
                    if (impReq != null && !impReq.IsLocked)
                    {
                        Request req = (Request)request.Clone();
                        req.IsDateSetByUser = true;
                        req.Person = new Person() { ID = personID };
                        requestBusiness.InsertRequest(req);
                    }
                }

                int count = kartableBusiness.GetUserRequestCount(RequestState.UnKnown, imperativeRequest.Year, imperativeRequest.Month);
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertImperativeRequestByOperator");
                throw ex;
            }

        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckKartableLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool ConfirmRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description)
        {
            return ((IKartablRequests)(new BKartabl())).SetStatusOfRequest(requests, status, description);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool UnconfirmRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description)
        {
            return ((IKartablRequests)(new BKartabl())).SetStatusOfRequest(requests, status, description);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSurveyedRequestsLoadAccess()
        {
        }

    }
}
