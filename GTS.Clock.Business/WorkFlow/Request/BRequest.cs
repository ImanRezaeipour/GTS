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
using GTS.Clock.Model.Security;
using GTS.Clock.Infrastructure.Validation.Configuration;

namespace GTS.Clock.Business.RequestFlow
{
    public class BRequest : BaseBusiness<Request>, IHourlyAbsenceBRequest, IDailyAbsenceBRequest, ITrafficBRequest, IOverTimeBRequest, IDashboardBRequest
    {
        private const string ExceptionSrc = "GTS.Clock.Business.RequestFlow.BRequest";
        private RequestRepository requestRep = new RequestRepository(false);
        private decimal workingPersonId = 0;//جهت تست
        public BRequest() { }

        /// <summary>
        /// تنها جهت تست
        /// </summary>
        /// <param name="personId"></param>
        public BRequest(decimal personId) 
        {
            this.workingPersonId = personId;
        }

        #region IDashboardBRequest Members

        /// <summary>
        /// درخواستها را برمیگرداند
        /// اگر اپراتور باشد درخواستهای افراد تحت مدیریت هم به آن اضافه میشود
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int IDashboardBRequest.GetAllRequestCount(int year, int month)
        {
            IDashboardBRequest ip = new BRequest();
            int count = ip.GetAllRequestCount(year, month, RequestState.UnKnown);
            return count;
        }

        int IDashboardBRequest.GetAllRequestCount(int year, int month, RequestState state)
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

                IList<Request> result = new List<Request>();
                IList<Request> list = requestRep.GetAllRequest(GetCurrentPersonId(), fromDate, toDate, state);
                var groupedList = from o in list
                                  group o by o;
                foreach (var found in groupedList)
                {
                    Request req = found.Key;
                    result.Add(req);
                }
               
                return result.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        int IDashboardBRequest.GetAllRequestCount(int year, RequestState state)
        {
            try
            {
                int fromMonth = 1;
                int toMonth = 12;
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, toMonth);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", state == RequestState.UnderReview ? year - 1 : year, fromMonth, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, toMonth, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, toMonth);
                    fromDate = new DateTime(state == RequestState.UnderReview ? year - 1 : year, fromMonth, 1);
                    toDate = new DateTime(year, toMonth, endOfMonth);
                }
                

                IList<Request> result = new List<Request>();
                IList<Request> list = requestRep.GetAllRequest(GetCurrentPersonId(), fromDate, toDate, state);
                var groupedList = from o in list
                                  group o by o;
                foreach (var found in groupedList)
                {
                    Request req = found.Key;
                    result.Add(req);
                }

                return result.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// کاربرانی که تاحالا برای این شخص درخواست ثبت کرده اند را برمیگرداند
        /// </summary>
        /// <returns></returns>
        int IDashboardBRequest.GetAllUserInRequestCount()
        {
            throw new NotImplementedException();
        }

       

        #endregion

        #region IDailyAbsenceBRequest Members

        /// <summary>
        /// 
        /// درخواستهای مرخصی ساعتی و ماموریت ساعتی را برمیگرداند 
        /// همچنین وضعیت درخواست را جهت نمایش در واسط کاربر معین میکند
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<Request> GetAllDailyLeaveDutyRequests(string datetime)
        {
            try
            {
                DateTime date = Utility.ToMildiDateTime(datetime);
                IList<Request> list = requestRep.GetAllDailyRequestByType(GetCurrentPersonId(), date.Date, PrecardGroupsName.duty, PrecardGroupsName.leave, PrecardGroupsName.leaveestelajy);

                foreach (Request req in list)
                {
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        req.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                        req.TheFromDate = Utility.ToPersianDate(req.FromDate);
                        req.TheToDate = Utility.ToPersianDate(req.ToDate);
                    }
                    else
                    {
                        req.RegistrationDate = Utility.ToString(req.RegisterDate);
                        req.TheFromDate = Utility.ToString(req.FromDate);
                        req.TheToDate = Utility.ToString(req.ToDate);
                    }
                    req.Status = CheckStatus(req);

                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequest", "GetAllDailyLeaveDutyRequests");
                throw ex;
            }
        }

        /// <summary>
        /// انواع پیشکارت مرخصی استحقاقی و استعلاجی روزانه را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<Precard> GetAllDailyLeaves()
        {
            try
            {
                List<Precard> list = new List<Precard>();
                EntityRepository<PrecardGroups> groupRep = new EntityRepository<PrecardGroups>(false);
                PrecardGroups group = groupRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrecardGroups().LookupKey), PrecardGroupsName.leave.ToString())).FirstOrDefault();
                if (group != null)
                {
                    list.AddRange(group.PrecardList.Where(x => x.IsDaily && x.IsPermit && x.Active).ToList());
                }
                group = groupRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrecardGroups().LookupKey), PrecardGroupsName.leaveestelajy.ToString())).FirstOrDefault();
                if (group != null)
                {
                    list.AddRange(group.PrecardList.Where(x => x.IsDaily && x.IsPermit && x.Active).ToList());
                }
                list = list.Where(x => x.AccessRoleList.Where(y => y.ID == BUser.CurrentUser.Role.ID).Count() > 0).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequest", "GetAllDailyLeaves");
                throw ex;
            }
        }

        /// <summary>
        /// انواع پیشکارت ماموریت روزانه را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<Precard> GetAllDailyDuties()
        {
            try
            {
                List<Precard> list = new List<Precard>();
                EntityRepository<PrecardGroups> groupRep = new EntityRepository<PrecardGroups>(false);
                PrecardGroups group = groupRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrecardGroups().LookupKey), PrecardGroupsName.duty.ToString())).FirstOrDefault();
                if (group != null)
                {
                    list = group.PrecardList.Where(x => x.IsDaily && x.IsPermit && x.Active).ToList();
                }
                list = list.Where(x => x.AccessRoleList.Where(y => y.ID == BUser.CurrentUser.Role.ID).Count() > 0).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequest", "GetAllDailyDuties");
                throw ex;
            }
        }

        public IList<Doctor> GetAllDoctors()
        {
            BaseInformation.BDoctor bdoctor = new GTS.Clock.Business.BaseInformation.BDoctor();
            IList<Doctor> list = bdoctor.GetAll();
            return list;
        }

        public IList<Illness> GetAllIllness()
        {
            BaseInformation.BIllness billness = new GTS.Clock.Business.BaseInformation.BIllness();
            IList<Illness> list = billness.GetAll();
            return list;
        }

        public IList<DutyPlace> GetAllDutyPlaceRoot()
        {
            EntityRepository<DutyPlace> rep = new EntityRepository<DutyPlace>();
            IList<DutyPlace> list = rep.GetByCriteria(ConditionOperations.OR, new CriteriaStruct(Utility.GetPropertyName(() => new DutyPlace().ParentID), (decimal)0),
                                                        new CriteriaStruct(Utility.GetPropertyName(() => new DutyPlace().ParentID), null));
            return list;
        }

        public IList<DutyPlace> GetAllDutyPlaceChild(decimal parentId)
        {
            EntityRepository<DutyPlace> rep = new EntityRepository<DutyPlace>();
            IList<DutyPlace> list = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DutyPlace().ParentID), parentId));
            return list;
        }

        #endregion

        #region IHourlyAbsenceBRequest Members
       
        /// <summary>
        /// غیبتهای غیرمجاز ساعتی را برمیگرداند
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public IList<MonthlyDetailReportProxy> GetAllHourlyAbsence(string datetime)
        {
            try
            {
                DateTime date = Utility.ToMildiDateTime(datetime);
                PrsMonthlyRptRepository prsMonthlyRep = new PrsMonthlyRptRepository(false);
                List<MonthlyDetailReportProxy> detailList = new List<MonthlyDetailReportProxy>();
                IList<PersonalMonthlyReportRowDetail> list = prsMonthlyRep.LoadPairableScndcnpValue(GetCurrentPersonId(), date.Date, ConceptsKeys.gridFields_HourlyUnallowableAbsence);
                foreach (PersonalMonthlyReportRowDetail detail in list)
                {
                    for (int i = 0; i < detail.Pairs.Count; i++)
                    {
                        //غیبتهایی با جفتهای خالی را نشان ندهد
                        if (detail.Pairs[i].From > 0 && detail.Pairs[i].To > 0)
                        {
                            MonthlyDetailReportProxy proxy = new MonthlyDetailReportProxy();
                            proxy.From = Utility.IntTimeToRealTime(detail.Pairs[i].From).ToString();
                            proxy.To = Utility.IntTimeToRealTime(detail.Pairs[i].To);
                            proxy.Name = detail.ScndCnpName;
                            proxy.Color = detail.Color;
                            detailList.Add(proxy);
                        }
                    }
                }
                return detailList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequest", "GetAllHourlyAbsence");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// درخواستهای مرخصی ساعتی و ماموریت ساعتی را برمیگرداند 
        /// همچنین وضعیت درخواست را جهت نمایش در واسط کاربر معین میکند
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<Request> GetAllHourlyLeaveDutyRequests(string datetime)
        {
            try
            {
                DateTime date = Utility.ToMildiDateTime(datetime);
                IList<Request> list = requestRep.GetAllHourlyRequestByType(GetCurrentPersonId(), date.Date, PrecardGroupsName.duty, PrecardGroupsName.leave, PrecardGroupsName.leaveestelajy);

                foreach (Request req in list)
                {
                    req.TheFromTime = Utility.IntTimeToRealTime(req.FromTime);
                    req.TheToTime = Utility.IntTimeToRealTime(req.ToTime);
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        req.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                    }
                    else
                    {
                        req.RegistrationDate = Utility.ToString(req.RegisterDate);
                    }
                    req.Status = CheckStatus(req);
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequest", "GetAllHourlyLeaveDutyRequests");
                throw ex;
            }
        }

        /// <summary>
        /// انواع پیشکارت مرخصی ساعتی  و استعلاجی ساعتی را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<Precard> GetAllHourlyLeaves()
        {
            try
            {
                List<Precard> list = new List<Precard>();
                EntityRepository<PrecardGroups> groupRep = new EntityRepository<PrecardGroups>(false);
                PrecardGroups group = groupRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrecardGroups().LookupKey), PrecardGroupsName.leave.ToString())).FirstOrDefault();
                if (group != null)
                {
                    list.AddRange(group.PrecardList.Where(x => x.IsHourly && x.IsPermit && x.Active).ToList());
                }
                group = groupRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrecardGroups().LookupKey), PrecardGroupsName.leaveestelajy.ToString())).FirstOrDefault();
                if (group != null)
                {
                    list.AddRange(group.PrecardList.Where(x => x.IsHourly && x.IsPermit && x.Active).ToList());
                }
                list = list.Where(x => x.AccessRoleList.Where(y => y.ID == BUser.CurrentUser.Role.ID).Count() > 0).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequest", "GetAllHourlyLeaves");
                throw ex;
            }

        }

        /// <summary>
        /// انواع پیشکارت ماموریت ساعتی را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<Precard> GetAllHourlyDutis()
        {
            try
            {
                List<Precard> list = new List<Precard>();
                EntityRepository<PrecardGroups> groupRep = new EntityRepository<PrecardGroups>(false);
                PrecardGroups group = groupRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrecardGroups().LookupKey), PrecardGroupsName.duty.ToString())).FirstOrDefault();
                if (group != null)
                {
                    return group.PrecardList.Where(x => x.IsHourly && x.IsPermit && x.Active).ToList();
                }
                list = list.Where(x => x.AccessRoleList.Where(y => y.ID == BUser.CurrentUser.Role.ID).Count() > 0).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequest", "GetAllHourlyDutis");
                throw ex;
            }
        }
        

        #endregion

        #region IOverTimeBRequest Members

        /// <summary>
        /// اضافه کاری های غیر مجاز را برمیگرداند
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public IList<MonthlyDetailReportProxy> GetAllUnallowedOverworks(string datetime)
        {
            try
            {
                DateTime date = Utility.ToMildiDateTime(datetime);
                PrsMonthlyRptRepository prsMonthlyRep = new PrsMonthlyRptRepository(false);
                List<MonthlyDetailReportProxy> detailList = new List<MonthlyDetailReportProxy>();
                IList<PersonalMonthlyReportRowDetail> list = prsMonthlyRep.LoadPairableScndcnpValue(GetCurrentPersonId(), date.Date, ConceptsKeys.gridFields_UnallowableOverTime);
                foreach (PersonalMonthlyReportRowDetail detail in list)
                {
                    for (int i = 0; i < detail.Pairs.Count; i++)
                    {
                        MonthlyDetailReportProxy proxy = new MonthlyDetailReportProxy();
                        proxy.From = Utility.IntTimeToRealTime(detail.Pairs[i].From).ToString();
                        proxy.To = Utility.IntTimeToRealTime(detail.Pairs[i].To);
                        proxy.Name = detail.ScndCnpName;
                        proxy.Color = detail.Color;
                        detailList.Add(proxy);
                    }
                }
                return detailList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequest", "GetAllUnallowedOverworks");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// درخواستهای اضافه کاری را برمیگرداند 
        /// همچنین وضعیت درخواست را جهت نمایش در واسط کاربر معین میکند
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<Request> GetAllOverTimeRequests(string miladiDatetime)
        {
            try
            {
                DateTime date = Utility.ToMildiDateTime(miladiDatetime);
                IList<Request> list = requestRep.GetAllHourlyRequestByType(GetCurrentPersonId(), date.Date, PrecardGroupsName.overwork);
                EntityRepository<RequestStatus> statusRep = new EntityRepository<RequestStatus>(false);

                foreach (Request req in list)
                {
                    req.TheFromTime = Utility.IntTimeToRealTime(req.FromTime);
                    req.TheToTime = Utility.IntTimeToRealTime(req.ToTime);
                    req.TheTimeDuration = Utility.IntTimeToRealTime(req.TimeDuration);
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        req.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                        req.TheFromDate = Utility.ToPersianDate(req.FromDate);
                        req.TheToDate = Utility.ToPersianDate(req.ToDate);
                    }
                    else
                    {
                        req.RegistrationDate = Utility.ToString(req.RegisterDate);
                        req.TheFromDate = Utility.ToString(req.FromDate);
                        req.TheToDate = Utility.ToString(req.ToDate);
                    }
                    req.Status = CheckStatus(req);

                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequest", "GetAllOverTimeRequests");
                throw ex;
            }
        }

        /// <summary>
        /// انواع پیشکارت اضافه کار ساعتی را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<Precard> GetAllOverWorks()
        {
            try
            {
                List<Precard> list = new List<Precard>();
                EntityRepository<PrecardGroups> groupRep = new EntityRepository<PrecardGroups>(false);
                PrecardGroups group = groupRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrecardGroups().LookupKey), PrecardGroupsName.overwork.ToString())).FirstOrDefault();
                if (group != null)
                {
                    return group.PrecardList.Where(x => x.IsHourly && x.IsPermit && x.Active).ToList();
                }
                list = list.Where(x => x.AccessRoleList.Where(y => y.ID == BUser.CurrentUser.Role.ID).Count() > 0).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequest", "GetAllOverWorks");
                throw ex;
            }
        }

        /// <summary>
        /// انواع پیشکارت دستوری را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<Precard> GetAllImperatives()
        {
            try
            {
                List<Precard> list = new List<Precard>();
                EntityRepository<PrecardGroups> groupRep = new EntityRepository<PrecardGroups>(false);
                PrecardGroups group = groupRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrecardGroups().LookupKey), PrecardGroupsName.imperative.ToString())).FirstOrDefault();
                if (group != null)
                {
                    return group.PrecardList.Where(x => x.IsPermit && x.Active).ToList();
                }
                list = list.Where(x => x.AccessRoleList.Where(y => y.ID == BUser.CurrentUser.Role.ID).Count() > 0).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequest", "GetAllImperatives");
                throw ex;
            }
        }


        /// <summary>
        /// لیست شیفتها را از سه روز قبل تا سه روز بعد برمیگرداند
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public IList<ShiftProxy> GetAllShifts(DateTime datetime) 
        {
            decimal personId = GetCurrentPersonId();
            LanguagesName localLang= BLanguage.CurrentLocalLanguage;
            LanguagesName sysLang = BLanguage.CurrentSystemLanguage;
            PersonRepository personRep = new PersonRepository(false);
            personRep.EnableEfectiveDateFilter(personId, datetime.AddDays(-3), datetime.AddDays(3), DateTime.Now, DateTime.Now, datetime.AddDays(-3), datetime.AddDays(3));
            Model.Person person = personRep.GetById(personId, false);
            IList<ShiftProxy> result = new List<ShiftProxy>();

            for (int i = -3; i <= 3; i++)
            {
                BaseShift shift = person.GetShiftByDate(datetime.AddDays(i));
                if (shift.ID > 0)
                {
                    ShiftProxy proxy = new ShiftProxy();
                    if (localLang == LanguagesName.Parsi)
                    {
                        proxy.DayName = PersianDateTime.GetPershianDayName(datetime.AddDays(i));
                    }
                    else
                    {
                        proxy.DayName = datetime.AddDays(i).DayOfWeek.ToString("G");
                    }
                    if (sysLang == LanguagesName.Parsi)
                    {
                        proxy.Date = Utility.ToPersianDate(datetime.AddDays(i));
                    }
                    else
                    {
                        proxy.Date = Utility.ToString(datetime.AddDays(i));
                    }
                    proxy.ID = i;
                    proxy.ShiftID = shift.MyShiftId;
                    proxy.ShiftName = shift.Name;

                    result.Add(proxy);
                }
            }
            return result;
        }

        public IList<ShiftPairProxy> GetShiftDetail(decimal shiftId) 
        {
            Business.Shifts.BShift bshift = new GTS.Clock.Business.Shifts.BShift();
            Shift shift = bshift.GetByID(shiftId);
            IList<ShiftPair> pairs = shift.Pairs;
            IList<ShiftPairProxy> result = new List<ShiftPairProxy>();
            foreach (ShiftPair pair in pairs) 
            {
                ShiftPairProxy proxy = new ShiftPairProxy();
                proxy.From = Utility.IntTimeToRealTime(pair.From);
                proxy.To = Utility.IntTimeToRealTime(pair.To);
                result.Add(proxy);
            }
            return result;
        }

        #endregion

        #region ITrafficBRequest Members

        /// <summary>
        /// ترددهای صورت گرفته در یک روز خاص را برمیگرداند
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public IList<MonthlyDetailReportProxy> GetAllTrafic(string datetime)
        {
            try
            {
                DateTime date = Utility.ToMildiDateTime(datetime);
                EntityRepository<ProceedTraffic> trafficRep = new EntityRepository<ProceedTraffic>();
                List<MonthlyDetailReportProxy> detailList = new List<MonthlyDetailReportProxy>();
                IList<ProceedTraffic> list = trafficRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ProceedTraffic().Person), new Person() { ID = GetCurrentPersonId() }),
                                                                      new CriteriaStruct(Utility.GetPropertyName(() => new ProceedTraffic().FromDate), date),
                                                                      new CriteriaStruct(Utility.GetPropertyName(() => new ProceedTraffic().ToDate), date));
                foreach (ProceedTraffic detail in list)
                {
                    for (int i = 0; i < detail.Pairs.Count; i++)
                    {
                        MonthlyDetailReportProxy proxy = new MonthlyDetailReportProxy();
                        proxy.From = Utility.IntTimeToRealTime(detail.Pairs[i].From);
                        proxy.To = Utility.IntTimeToRealTime(detail.Pairs[i].To);
                        detailList.Add(proxy);
                    }
                }
                return detailList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequest", "GetAllTrafic");
                throw ex;
            }
        }

        /// <summary>
        /// جهت اطلاع رسانی ظاهرا
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<ProceedTrafficProxy> GetAllTrafic1(decimal personId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                EntityRepository<ProceedTraffic> trafficRep = new EntityRepository<ProceedTraffic>();
                List<ProceedTrafficProxy> detailList = new List<ProceedTrafficProxy>();
                IList<ProceedTraffic> list = trafficRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ProceedTraffic().Person), new Person() { ID = GetCurrentPersonId() }),
                                                                      new CriteriaStruct(Utility.GetPropertyName(() => new ProceedTraffic().FromDate), fromDate, CriteriaOperation.GreaterEqThan),
                                                                      new CriteriaStruct(Utility.GetPropertyName(() => new ProceedTraffic().FromDate), toDate, CriteriaOperation.LessEqThan));
                foreach (ProceedTraffic detail in list)
                {
                    for (int i = 0; i < detail.Pairs.Count; i++)
                    {
                        ProceedTrafficProxy proxy = new ProceedTrafficProxy();
                        proxy.From = Utility.IntTimeToRealTime(detail.Pairs[i].From);
                        proxy.To = Utility.IntTimeToRealTime(detail.Pairs[i].To);
                        proxy.Pishcard = detail.Pairs[i].Precard.Name;
                        detailList.Add(proxy);
                    }
                }
                return detailList;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// درخواستهای تردد را برمیگرداند 
        /// همچنین وضعیت درخواست را جهت نمایش در واسط کاربر معین میکند
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<Request> GetAllTrafficRequests(string datetime)
        {
            try
            {
                DateTime date = Utility.ToMildiDateTime(datetime);
                IList<Request> list = requestRep.GetAllHourlyRequestByType(GetCurrentPersonId(), date.Date, PrecardGroupsName.traffic);

                foreach (Request req in list)
                {
                    req.TheFromTime = Utility.IntTimeToRealTime(req.FromTime);
                    req.TheToTime = Utility.IntTimeToRealTime(req.ToTime);
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        req.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                    }
                    else
                    {
                        req.RegistrationDate = Utility.ToString(req.RegisterDate);
                    }
                    req.Status = CheckStatus(req);

                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequest", "GetAllHourlyTrafficRequests");
                throw ex;
            }
        }

        public IList<Precard> GetAllTraffics()
        {
            try
            {
                EntityRepository<PrecardGroups> groupRep = new EntityRepository<PrecardGroups>(false);
                PrecardGroups group = groupRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrecardGroups().LookupKey), PrecardGroupsName.traffic.ToString())).FirstOrDefault();
                List<Precard> list = new List<Precard>();
                if (group != null)
                {
                    list= group.PrecardList.Where(x => x.IsHourly && x.IsPermit && x.Active).ToList();
                    foreach (Precard p in list)
                        p.IsTraffic = true;
                }

                list = list.Where(x => x.AccessRoleList.Where(y => y.ID == BUser.CurrentUser.Role.ID).Count() > 0).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequest", "GetAllTraffics");
                throw ex;
            }
        }

        #endregion

        #region Insert Update Delete

        /// <summary>
        /// قبل از درج تاریخ ثبت را مشخص میکند
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Request InsertRequestByUIValidate(Request request)
        {
            try
            {
                Request r = InsertRequestWithoutUIValidate(request);
                return r;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// قبل از درج تاریخ ثبت را مشخص میکند
        /// بدون اعمال اعتبار سنجی واسط کاربر
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Request InsertRequestWithoutUIValidate(Request request)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {

                    //از واسط کاربر مقداردهی میشود 
                    //این خصیصه دومنظوره استفاده شده است
                    DateTime clickDate = request.RegisterDate;
                    base.SaveChanges(request, UIActionType.ADD);

                    if (!Utility.IsEmpty(clickDate) && request.FromDate <= clickDate && clickDate <= request.ToDate)
                    {
                        request.AddClientSide = true;
                    }

                    NHibernateSessionManager.Instance.CommitTransactionOn();

                    return request;

                }

                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// قبل از درج تاریخ ثبت را مشخص میکند
        /// با اعمال اعتبار سنجی واسط کاربر
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Request InsertRequest(Request request) 
        {
            return this.InsertRequestByUIValidate(request);
        }

        /// <summary>
        /// قبل از درج تاریخ ثبت را مشخص میکند
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool DeleteRequest(Request request)
        {
            try
            {
                decimal id = base.SaveChanges(request, UIActionType.DELETE);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// درخواست تکراری نباشد
        /// جهت آیتم های ساعتی انتدا و انتهای تاریخ یکسان است
        /// اعتبارسنجی بر روی:
        /// »تاریخ خالی نباشد
        /// »اگر ساعتی است زمان خالی نباشد
        /// زمان ابتدا از انتها بزرگتر نباشد
        /// تاریخ ابتدا از انتها بزرگتر نباشد
        /// »پیشکارت خالی نباشد
        /// »تکراری نباشد
        /// </summary>
        /// <param name="request"></param>
        protected override void InsertValidate(Request request)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            
            Precard p;
            if (Utility.IsEmpty(request.FromDate) || Utility.IsEmpty(request.ToDate)) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RequestDateShouldNotEmpty, "تاریخ نباید خالی باشد", ExceptionSrc));
            }
            if (request.Person == null || request.Person.ID <= 0) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RequestPersonRequierd, "نام شخص نباید خالی باشد", ExceptionSrc));
            }
            if (request.Precard == null || request.Precard.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RequestPrecardIsEmpty, "پیشکارت نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                p = new BPrecard().GetByID(request.Precard.ID);
                if (!request.IsDateSetByUser && request.FromDate != request.ToDate)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestFromToDateNotEqual, "تاریخ ابتدا و انتهای باید برابر باشد", ExceptionSrc));
                }
                else if (request.IsDateSetByUser && request.FromDate > request.ToDate)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestFromDateGreaterThanToDate, "تاریخ ابتدا و انتهای درخواست ساعتی باید یکسان باشد", ExceptionSrc));
                }
                if (p.IsHourly && request.FromTime == request.ToTime && request.TimeDuration <= 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestTimeIsNotValid, "زمان مربوط به درخواست معتبر نمیباشد", ExceptionSrc));
                }
                else if (!IsTrafficRequest(request) && p.IsHourly && (request.FromTime < 0 || request.ToTime < 0))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestTimeIsNotValid, "زمان مربوط به درخواست معتبر نمیباشد", ExceptionSrc));
                }
                else if (p.IsHourly && request.FromTime > request.ToTime && !Utility.IsEmpty(request.TheToTime))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestFromTimeGreaterThanToTime, "زمان ابتدا از انتها بزرگتر است", ExceptionSrc));
                }
            }

            if (exception.Count == 0)
            {
                Precard precard = new PrecardRepository().GetById(request.Precard.ID, false);
                if (precard.PrecardGroup == null)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.PrecardGroupIsNull, "گروه پیشکارت دستوری خالی است", ExceptionSrc);
                }
                EntityRepository<Request> rep = new EntityRepository<Request>(false);

                //اعتبار سنجی دستوری
                if (precard.PrecardGroup.IntLookupKey == (int)PrecardGroupsName.imperative)
                {
                    IList<Request> list = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => request.Precard), request.Precard),
                                               new CriteriaStruct(Utility.GetPropertyName(() => request.Person), request.Person),
                                               new CriteriaStruct(Utility.GetPropertyName(() => request.FromDate), request.FromDate),
                                               new CriteriaStruct(Utility.GetPropertyName(() => request.ToDate), request.ToDate));
                    var result = from o in list
                                 where o.RequestStatusList.Where(x => x.Confirm == false && x.EndFlow).Count() == 0
                                 select o;
                    var deleted = from o in result
                                  where o.RequestStatusList.Where(x => x.IsDeleted == true).Count() > 0
                                  from s in o.RequestStatusList
                                  select s;
                    if (deleted != null)
                    {
                        foreach (RequestStatus status in deleted.ToList())
                        {
                            result = result.Where(x => x.ID != status.Request.ID);
                        }
                    }
                    if (result.Count() > 0)
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.RequestImperativeRepeated, "این درخواست تکراری میباشد", ExceptionSrc));
                    }
                }
                else
                {
                    IList<Request> list = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => request.Precard), request.Precard),
                                                       new CriteriaStruct(Utility.GetPropertyName(() => request.Person), request.Person),
                                                       new CriteriaStruct(Utility.GetPropertyName(() => request.FromDate), request.FromDate),
                                                       new CriteriaStruct(Utility.GetPropertyName(() => request.ToDate), request.ToDate),
                                                       new CriteriaStruct(Utility.GetPropertyName(() => request.FromTime), request.FromTime),
                                                       new CriteriaStruct(Utility.GetPropertyName(() => request.ToTime), request.ToTime));
                    var result = from o in list
                                 where o.RequestStatusList.Where(x => x.Confirm == false && x.EndFlow).Count() == 0
                                 select o;
                    var deleted = from o in result
                                  where o.RequestStatusList.Where(x => x.IsDeleted == true).Count() > 0
                                  from s in o.RequestStatusList
                                  select s;
                    if (deleted != null)
                    {
                        foreach (RequestStatus status in deleted.ToList())
                        {
                            result = result.Where(x => x.ID != status.Request.ID);
                        }
                    }
                    if (result.Count() > 0)
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.RequestRepeated, "این درخواست تکراری میباشد", ExceptionSrc));
                    }
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void UpdateValidate(Request obj)
        {
            //throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نیمباشد", ExceptionSrc);        
        }

        /// <summary>
        /// درخواست به پایان جریان نرسیده باشد
        /// </summary>
        /// <param name="request"></param>
        protected override void DeleteValidate(Request request)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            EntityRepository<RequestStatus> rep = new EntityRepository<RequestStatus>(false);
            int count = rep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new RequestStatus().Request), request),
                                               new CriteriaStruct(Utility.GetPropertyName(() => new RequestStatus().Confirm), true));

            if (count > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RequestUsedByFlow, "این درخواست بدلیل استفاده در جریان کاری قابل حذف نیست", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// پرکردن محل ماموریت نام بیماری نام دکتر
        /// </summary>
        /// <param name="request"></param>
        /// <param name="action"></param>
        protected override void GetReadyBeforeSave(Request request, UIActionType action)
        {
            if (action == UIActionType.ADD)
            {
                BDoctor bdoctor = new BDoctor();
                BIllness billness = new BIllness();
                BDutyPlace bdutyPlace = new BDutyPlace();

                #region Set Description and precard

                if (IsEstelajiLeaveRequest(request))
                {
                    string doctorName = "";
                    string illnessName = "";
                    if (request.DoctorID > 0)
                    {
                        request.DoctorList = new List<Doctor>();
                        request.DoctorList.Add(bdoctor.GetByID(request.DoctorID));
                        doctorName = request.DoctorList[0].Name;
                    }
                    if (request.IllnessID > 0)
                    {
                        request.IllnessList = new List<Illness>();
                        request.IllnessList.Add(billness.GetByID(request.IllnessID));
                        illnessName = request.IllnessList[0].Name;
                    }
                    if (!Utility.IsEmpty(doctorName))
                    {
                        request.Description += String.Format("  {0} ", doctorName);
                    }
                    if (!Utility.IsEmpty(illnessName))
                    {
                        request.Description += String.Format("  {0} ", illnessName);
                    }
                }
                else if (IsDutyRequest(request))
                {
                    if (request.DutyPositionID > 0)
                    {
                        request.DutyPlaceList = new List<DutyPlace>();
                        request.DutyPlaceList.Add(bdutyPlace.GetByID(request.DutyPositionID));
                        IList<string> list = new List<string>();
                        GetDutyPalces(request.DutyPositionID, list);
                        foreach (string str in list)
                        {
                            request.Description += String.Format("  {0} -  ", str);
                        }
                    }
                }
                else if (IsTrafficRequest(request) || (request.Precard != null && request.Precard.ID == -1))
                {
                    if (request.Precard.ID == -1)
                    {
                        PrecardRepository precardRep = new PrecardRepository(false);
                        Precard p = precardRep.GetUsualPrecard();
                        if (p != null)
                        {
                            if (p.AccessRoleList.Where(x => x.ID == BUser.CurrentUser.Role.ID).Count() == 0)
                            {
                                UIValidationExceptions exception = new UIValidationExceptions();

                                exception.Add(new ValidationException(ExceptionResourceKeys.RequestIsNotAllowed, "ثبت این درخواست مجاز نمیباشد", ExceptionSrc));
                                throw exception;
                            }
                            request.Precard = new Precard() { ID = p.ID };
                        }
                        else
                        {
                            throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UsualPrecardIsNotExistsInDatabase, "پیشکارت عادی با کد صفر در دیتابیس موجود نیست", ExceptionSrc);
                        }
                    }
                }
                #endregion

                //mybe set by operator when he wants insert request
                if (request.Person == null || request.Person.ID == 0)
                {
                    decimal id = GetCurrentPersonId();
                    request.Person = new Person() { ID = id };
                }
                request.User = BUser.CurrentUser;
                request.OperatorUser = BUser.CurrentUser.Person.Name;
                if (request.IsDateSetByUser)
                {
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        request.FromDate = Utility.ToMildiDate(request.TheFromDate);
                        request.ToDate = Utility.ToMildiDate(request.TheToDate);
                    }
                    else
                    {
                        request.FromDate = Utility.ToMildiDateTime(request.TheFromDate);
                        request.ToDate = Utility.ToMildiDateTime(request.TheToDate);
                    }
                }
                else
                {
                    request.FromDate = Utility.ToMildiDateTime(request.TheFromDate);
                    request.ToDate = Utility.ToMildiDateTime(request.TheToDate);
                }
                request.FromTime = Utility.RealTimeToIntTime(request.TheFromTime);
                request.ToTime = Utility.RealTimeToIntTime(request.TheToTime);
                request.ToTime += request.TimePlusFlag ? 1440 : 0;
                request.TimeDuration = !request.Precard.IsMonthly ? Utility.RealTimeToIntTime(request.TheTimeDuration) : int.Parse(request.TheTimeDuration);
                request.RegisterDate = DateTime.Now.Date;
                request.Status = RequestState.UnderReview;
            }
            //اگر ساعت صفر وارد شود 11:59 ذخیره میگردد
            if (action == UIActionType.ADD || action == UIActionType.EDIT)
            {
                if (request.FromTime == 0)
                {
                    request.FromTime = 1439;
                }
                if (request.ToTime == 0)
                {
                    request.ToTime = 1439;
                }
            }
            if ((request.TimeDuration == -1000 || request.TimeDuration == 0) && request.FromTime > 0 && request.ToTime > 0) //کلیه درخواست ها دارای مدت باشند مگر درخواستهای اضافه کار که خود آنها مدت دارند
            {
                request.TimeDuration = request.ToTime - request.FromTime;
                if (request.ContinueOnTomorrow)
                {
                    request.TimeDuration = (request.ToTime + 1440) - request.FromTime;
                }
            }
            if (request.ContinueOnTomorrow && request.ToTime > 0)
            {
                request.ToTime += 1440;
            }
   
        }

        /// <summary>
        /// تاریخ ثبت را باتوجه به فرهنگ ثبت میکند
        /// </summary>
        /// <param name="request"></param>
        /// <param name="action"></param>
        protected override void OnSaveChangesSuccess(Request request, UIActionType action)
        {
            if (action == UIActionType.ADD)
            {
                int year = 0;
                int month = 0;
                DateTime dateTime = DateTime.Now;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    request.RegistrationDate = Utility.ToPersianDate(request.RegisterDate);
                    dateTime = Utility.ToMildiDate(request.TheFromDate);
                    PersianCalendar pCal = new PersianCalendar();
                    year = pCal.GetYear(dateTime);
                    month = pCal.GetMonth(dateTime);
                }
                else
                {
                    request.RegistrationDate = Utility.ToString(request.RegisterDate);
                    dateTime = Utility.ToMildiDateTime(request.TheFromDate);
                    year = dateTime.Year;
                    month = dateTime.Month;
                }

                //تایید مدیر
                Precard precard = new PrecardRepository().GetById(request.Precard.ID, false);
                if (precard.PrecardGroup == null) 
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.PrecardGroupIsNull, "گروه پیشکارت دستوری خالی است", ExceptionSrc);
                }
                if (precard.PrecardGroup.IntLookupKey == (int)PrecardGroupsName.imperative) 
                {
                    Manager mng = new BManager().GetManager(BUser.CurrentUser.Person.ID);
                    if (mng.ID > 0) 
                    {
                        IKartablRequests bkartabl = new BKartabl();
                        IManagerKartabl mngKartabl = new BUnderManagment();
                        IList<InfoRequest> list = mngKartabl.GetAllRequests(mng.ID);
                        if (list != null)
                        {
                            InfoRequest info = list.Where(x => x.ID == request.ID).FirstOrDefault();
                            if (info != null)
                            {
                                KartableSetStatusProxy proxy = new KartableSetStatusProxy(request.ID,info.mngrFlowID);
                                bkartabl.SetStatusOfRequest(new List<KartableSetStatusProxy>() { proxy }, RequestState.Confirmed, "");
                            }
                        }
                    }
                }

                //تغییر وضعیت درخواست دستوری
                BImperativeRequest imperativeRequestBusiness = new BImperativeRequest();
                ImperativeRequest imperativeRequest = new ImperativeRequest()
                {
                    Precard = new Precard() { ID = request.Precard.ID },
                    Person = new Person() { ID = request.Person.ID },
                    Year = year,
                    Month = month
                };
                ImperativeRequest impReq = imperativeRequestBusiness.GetImperativeRequest(imperativeRequest);
                if (impReq != null)
                {
                    impReq.IsLocked = true;
                    imperativeRequestBusiness.SaveChanges(impReq, UIActionType.EDIT);
                }

            }
        }

        /// <summary>
        /// بدون ترانزاکشن
        /// </summary>
        /// <param name="request"></param>
        protected override void Insert(Request request)
        {
            requestRep.WithoutTransactSave(request);
        }

        protected override void UIValidate(Request request, UIActionType action)
        {
            if (action == UIActionType.ADD) 
            {
                CallUIValidator(request);
            }
        }

        #endregion

        #region Distinguish Request Type

        /// <summary>
        /// آیا درخواست تردد است
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool IsTrafficRequest(Request request) 
        {
            if (request == null || request.Precard == null || request.Precard.ID == 0)
                return false;
            PrecardRepository precardRep = new PrecardRepository(false);
            PrecardGroups group = precardRep.GetPrecardGroup(request.Precard.ID);
            if (group != null && !Utility.IsEmpty(group.LookupKey))
            {
                string groupName = group.LookupKey.ToLower();
                if (groupName.Equals(PrecardGroupsName.traffic.ToString().ToLower())) 
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// آیا درخواست مرخصی ساعتی است
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool IsEstelajiLeaveRequest(Request request)
        {
            if (request == null || request.Precard == null || request.Precard.ID == 0)
                return false;
            PrecardRepository precardRep = new PrecardRepository(false);
            PrecardGroups group = precardRep.GetPrecardGroup(request.Precard.ID);
            if (group != null && !Utility.IsEmpty(group.LookupKey))
            {
                string groupName = group.LookupKey.ToLower();
                if (groupName.Equals(PrecardGroupsName.leaveestelajy.ToString().ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// آیا درخواست ماموریت ساعتی است
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool IsDutyRequest(Request request)
        {
            if (request == null || request.Precard == null || request.Precard.ID == 0)
                return false;
            PrecardRepository precardRep = new PrecardRepository(false);
            PrecardGroups group = precardRep.GetPrecardGroup(request.Precard.ID);
            if (group != null && !Utility.IsEmpty(group.LookupKey))
            {
                string groupName = group.LookupKey.ToLower();
                if (groupName.Equals(PrecardGroupsName.duty.ToString().ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// آیا درخواست اضافه کاری است
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool IsOverWorkRequest(Request request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// آیا درخواست مرخصی روزانه است
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool IsDailyLeaveRequest(Request request)
        {
            if (request == null || request.Precard == null || request.Precard.ID == 0)
                return false;
            PrecardRepository precardRep = new PrecardRepository(false);
            PrecardGroups group = precardRep.GetPrecardGroup(request.Precard.ID);
            request.Precard = new BPrecard().GetByID(request.Precard.ID);
            if (group != null && request.Precard.IsDaily && !Utility.IsEmpty(group.LookupKey))
            {
                string groupName = group.LookupKey.ToLower();
                if (groupName.Equals(PrecardGroupsName.leave.ToString().ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// آیا درخواست مرخصی ساعتی است
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool IsHourlyLeaveRequest(Request request)
        {
            if (request == null || request.Precard == null || request.Precard.ID == 0)
                return false;
            PrecardRepository precardRep = new PrecardRepository(false);
            PrecardGroups group = precardRep.GetPrecardGroup(request.Precard.ID);
            request.Precard = new BPrecard().GetByID(request.Precard.ID);
            if (group != null && request.Precard.IsHourly && !Utility.IsEmpty(group.LookupKey))
            {
                string groupName = group.LookupKey.ToLower();
                if (groupName.Equals(PrecardGroupsName.leave.ToString().ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// آیا درخواست ماموریت روزانه است
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool IsDailyDutyRequest(Request request)
        {
            throw new NotImplementedException();
        }

        private bool IsDaily(Request request) 
        {
            if (request == null || request.Precard == null || request.Precard.ID == 0)
                return false;
            PrecardRepository precardRep = new PrecardRepository(false);
            int count = precardRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Precard().ID), request.Precard.ID),
                                          new CriteriaStruct(Utility.GetPropertyName(() => new Precard().IsDaily), true));
            if (count == 1) 
            {
                return true;
            }

            return false;
        }

        #endregion

        private decimal GetCurrentPersonId() 
        {
            if (workingPersonId == 0)
            {
                Model.Security.User user = BUser.CurrentUser;
                if (user != null)
                {
                    workingPersonId = user.Person.ID;
                }
            }
            return workingPersonId;
        }

        private void GetDutyPalces(decimal childId,IList<string> result) 
        {
            if (childId > 0)
            {
                DutyPlace duty = new BDutyPlace().GetByID(childId);
                result.Add(duty.Name);
                GetDutyPalces(duty.ParentID, result);
            }
        }

        private RequestState CheckStatus(Request request) 
        {
            EntityRepository<RequestStatus> statusRep = new EntityRepository<RequestStatus>(false);
            IList<RequestStatus> reqStates = statusRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new RequestStatus().Request), request));

            if (reqStates.Count == 0 || reqStates.Where(x => x.EndFlow).Count() == 0)
            {
                return RequestState.UnderReview;
            }
            else if (reqStates.Where(x => x.IsDeleted).Count() > 0)
            {
                return RequestState.Deleted;
            }
            else if (reqStates.Where(x => x.EndFlow && !x.Confirm).Count() > 0)
            {
                return RequestState.Unconfirmed;
            }
            else if (reqStates.Where(x => x.EndFlow && x.Confirm).Count() > 0)
            {
                return RequestState.Confirmed;
            }
            else
            {
                return RequestState.UnKnown;
            }
        }

        private void CallUIValidator(Request request)
        {
            IRequestUIValidation validator = UIValidationFactory.GetRepository<IRequestUIValidation>();
            if (validator != null)
            {
                validator.DoValidate(request);
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckTrafficRequestLoadAccess_onPersonnelLoadStateInGridSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckTrafficRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckTrafficRequestLoadAccess_onManagerLoadStateInGridSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckTrafficRequestLoadAccess_onManagerLoadStateInGanttChartSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertTrafficRequest_onPersonnelLoadStateInGridSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertTrafficRequest_onPersonnelLoadStateInGanttChartSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertTrafficRequest_onManagerLoadStateInGridSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertTrafficRequest_onManagerLoadStateInGanttChartSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteTrafficRequest_onPersonnelLoadStateInGridSchema(Request request)
        {
            return this.DeleteRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteTrafficRequest_onPersonnelLoadStateInGanttChartSchema(Request request)
        {
            return this.DeleteRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteTrafficRequest_onManagerLoadStateInGridSchema(Request request)
        {
            return this.DeleteRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteTrafficRequest_onManagerLoadStateInGanttChartSchema(Request request)
        {
            return this.DeleteRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckHourlyRequestLoadAccess_onPersonnelLoadStateInGridSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckHourlyRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckHourlyRequestLoadAccess_onManagerLoadStateInGridSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckHourlyRequestLoadAccess_onManagerLoadStateInGanttChartSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertHourlyRequest_onPersonnelLoadStateInGridSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertHourlyRequest_onPersonnelLoadStateInGanttChartSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertHourlyRequest_onManagerLoadStateInGridSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertHourlyRequest_onManagerLoadStateInGanttChartSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteHourlyRequest_onPersonnelLoadStateInGridSchema(Request request)
        {
            return this.DeleteRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteHourlyRequest_onPersonnelLoadStateInGanttChartSchema(Request request)
        {
            return this.DeleteRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteHourlyRequest_onManagerLoadStateInGridSchema(Request request)
        {
            return this.DeleteRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteHourlyRequest_onManagerLoadStateInGanttChartSchema(Request request)
        {
            return this.DeleteRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckDailyRequestLoadAccess_onPersonnelLoadStateInGridSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckDailyRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckDailyRequestLoadAccess_onManagerLoadStateInGridSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckDailyRequestLoadAccess_onManagerLoadStateInGanttChartSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertDailyRequest_onPersonnelLoadStateInGridSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertDailyRequest_onPersonnelLoadStateInGanttChartSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertDailyRequest_onManagerLoadStateInGridSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertDailyRequest_onManagerLoadStateInGanttChartSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteDailyRequest_onPersonnelLoadStateInGridSchema(Request request)
        {
            return this.DeleteRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteDailyRequest_onPersonnelLoadStateInGanttChartSchema(Request request)
        {
            return this.DeleteRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteDailyRequest_onManagerLoadStateInGridSchema(Request request)
        {
            return this.DeleteRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteDailyRequest_onManagerLoadStateInGanttChartSchema(Request request)
        {
            return this.DeleteRequest(request);
        }


        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckOverTimeRequestLoadAccess_onPersonnelLoadStateInGridSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckOverTimeRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckOverTimeRequestLoadAccess_onManagerLoadStateInGridSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckOverTimeRequestLoadAccess_onManagerLoadStateInGanttChartSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertOverTimeRequest_onPersonnelLoadStateInGridSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertOverTimeRequest_onPersonnelLoadStateInGanttChartSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertOverTimeRequest_onManagerLoadStateInGridSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Request InsertOverTimeRequest_onManagerLoadStateInGanttChartSchema(Request request)
        {
            return this.InsertRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteOverTimeRequest_onPersonnelLoadStateInGridSchema(Request request)
        {
            return this.DeleteRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteOverTimeRequest_onPersonnelLoadStateInGanttChartSchema(Request request)
        {
            return this.DeleteRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteOverTimeRequest_onManagerLoadStateInGridSchema(Request request)
        {
            return this.DeleteRequest(request);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool DeleteOverTimeRequest_onManagerLoadStateInGanttChartSchema(Request request)
        {
            return this.DeleteRequest(request);
        }


        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckShiftsViewLoadAccess_onPersonnelLoadStateInGridSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckShiftsViewLoadAccess_onPersonnelLoadStateInGanttChartSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckShiftsViewLoadAccess_onManagerLoadStateInGridSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckShiftsViewLoadAccess_onManagerLoadStateInGanttChartSchema()
        {
        }

        public void DeleteRequestAttachment(string path)
        {
            try
            {
                this.requestRep.DeleteRequestAttachment(path);
            }
            catch (Exception ex)
            {                
                LogException(ex, "BRequest", "DeleteRequestAttachment");
                throw ex;
            }
        }

        public Request GetRequestByID(decimal requestID)
        {
            try
            {
               return this.requestRep.GetRequestAttachmentByID(requestID);

            }
            catch (Exception ex)
            {                
                LogException(ex, "BRequest", "GetRequestAttachmentByID");
                throw ex;
            }
        }




        
    }
}
