using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Infrastructure.Validation.Configuration;
using GTS.Clock.Business.Proxy;

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// created at: 4/10/2012 9:57:35 AM
    /// by        : Farhad Salavati
    /// write your name here
    /// </summary>
    public class BTraffic : BaseBusiness<BasicTraffic>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BTraffic";
        private EntityRepository<BasicTraffic> objectRep = new EntityRepository<BasicTraffic>();

        /// <summary>
        /// روزهای ماه را برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public IList<DayDateProxy> GetDayList(decimal personId, int year, int month)
        {
            try
            {
                IList<DayDateProxy> result = new List<DayDateProxy>();
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
                int counter = 1;
                for (DateTime day = fromDate; day <= toDate; day = day.AddDays(1))
                {
                    DayDateProxy proxy = new DayDateProxy();
                    proxy.RowID = counter;
                    counter++;
                    proxy.Date = Utility.ToString(day);
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.TheDate = Utility.ToPersianDate(day);
                        proxy.DayName = PersianDateTime.GetPershianDayName(day);
                    }
                    else
                    {
                        proxy.TheDate = Utility.ToString(day);
                        proxy.DayName = PersianDateTime.GetEnglishDayName(day);
                    }
                    result.Add(proxy);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "GetDayList");
                throw ex;
            }

        }

        /// <summary>
        /// ترددهای یک روز را برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<BasicTrafficProxy> GetDayTraffics(decimal personId, string miladiDate)
        {
            try
            {
                IList<BasicTrafficProxy> result = new List<BasicTrafficProxy>();
                IList<BasicTraffic> basicList = objectRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new BasicTraffic().Date), Utility.ToMildiDateTime(miladiDate)),
                                          new CriteriaStruct(Utility.GetPropertyName(() => new BasicTraffic().Active), true),
                                          new CriteriaStruct(Utility.GetPropertyName(() => new BasicTraffic().Person), new Person() { ID = personId }));
                basicList = basicList.OrderBy(x => x.Time).ToList();

                foreach (BasicTraffic t in basicList)
                {
                    BasicTrafficProxy proxy = new BasicTrafficProxy();
                    proxy.ID = t.ID;
                    proxy.ClockName = t.Clock != null ? t.Clock.Name : "";
                    proxy.TheTime = Utility.IntTimeToTime(t.Time);
                    proxy.PrecardName = t.Precard.Name;
                    proxy.OpName = t.OperatorPerson != null ? t.OperatorPerson.Name : "";
                    result.Add(proxy);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "GetDayTraffics");
                throw ex;
            }
        }

        /// <summary>
        /// غیر فعال کردن تردد
        /// </summary>
        /// <param name="trafficId"></param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void DeleteTraffic(decimal trafficId)
        {
            try
            {
                BasicTraffic basicTraffic = base.GetByID(trafficId);
                basicTraffic.Active = false;
                basicTraffic.OperatorPerson = new Person() { ID = BUser.CurrentUser.Person.ID };
                SaveChanges(basicTraffic, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "DeleteTraffic");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertTraffic(decimal personId, decimal precardId, string date, string time, string description)
        {
            try
            {
                BasicTraffic basic = new BasicTraffic();
                basic.Person = new Person() { ID = personId };
                basic.Precard = new Precard() { ID = precardId };
                basic.OperatorPerson = new Person() { ID = BUser.CurrentUser.Person.ID };
                basic.Time = Utility.RealTimeToIntTime(time);
                basic.Description = description;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    basic.Date = Utility.ToMildiDate(date);
                }
                else
                {
                    basic.Date = Utility.ToMildiDateTime(date);
                }
                basic.Active = true;
                basic.Used = false;
                basic.Manual = true;
                base.SaveChanges(basic, UIActionType.ADD);
                return basic.ID;

            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "InsertTraffic");
                throw ex;
            }
        }

        public IList<Precard> GetTrafficTypes()
        {
            try
            {
                BRequest busRequest = new BRequest();
                List<Precard> list = new List<Precard>();
                list.AddRange(busRequest.GetAllTraffics());
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "GetTrafficTypes");
                throw ex;
            }
        }

        /// <summary>
        /// گزارش کارکرد برای یک سطر رابرمیگرداند
        /// تا یک روز بعد محاسبات انجام میگیرد
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="miladiDate"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public DailyReportProxy GetPersonDailyReport(decimal personId, string miladiDate)
        {
            try
            {
                DateTime dayDate = Utility.ToMildiDateTime(miladiDate);
                BPersonMonthlyWorkedTime monthlyReport = new BPersonMonthlyWorkedTime(personId);
                PersonalMonthlyReportRow row = monthlyReport.GetPersonDailyReport(dayDate);

                DailyReportProxy proxy = new DailyReportProxy();
                proxy.AllowableOverTime = row.AllowableOverTime;
                proxy.DailyAbsence = row.DailyAbsence;
                proxy.DailyMission = row.DailyMission;
                proxy.HourlyMeritoriouslyLeave = row.HourlyMeritoriouslyLeave;
                proxy.HourlyMission = row.HourlyMission;
                proxy.HourlyPureOperation = row.HourlyPureOperation;
                proxy.HourlySickLeave = row.HourlySickLeave;
                proxy.HourlyUnallowableAbsence = row.HourlyUnallowableAbsence;
                proxy.HourlyWithoutPayLeave = row.HourlyWithoutPayLeave;
                proxy.ShiftPairs = row.ShiftPairs;

                return proxy;
            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "GetPersonDailyReport");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void TransferTrafficsByConditions(TrafficTransferMode TTM, decimal machineID, string fromDate, string toDate, string fromTime, string toTime, int fromRecord, int toRecord, decimal fromIdentifier, decimal toIdentifier, string transferDay, string transferTime, TrafficTransferType TTT, bool IsIntegralConditions)
        {
            try
            {
                GTS.Clock.Infrastructure.Repository.BasicTrafficRepository basicTrafficRepository = new Infrastructure.Repository.BasicTrafficRepository();
                BLanguage LanguageBusiness = new BLanguage();
                DateTime FromDate = DateTime.Now;
                DateTime ToDate = DateTime.Now;
                switch (LanguageBusiness.GetCurrentSysLanguage())
                {
                    case "fa-IR":
                        FromDate = Utility.ToMildiDate(fromDate);
                        ToDate = Utility.ToMildiDate(toDate);
                        break;
                    case "en-US":
                        FromDate = Utility.ToMildiDateTime(fromDate);
                        ToDate = Utility.ToMildiDateTime(toDate);
                        break;
                }
                if (DateTime.Compare(FromDate, ToDate) > 0)
                    FromDate = ToDate;
                int FromTime = Utility.RealTimeToIntTime(fromTime);
                int ToTime = Utility.RealTimeToIntTime(toTime);
                int TransferTime = Utility.RealTimeToIntTime(transferTime);
                int TransferDay = 0;
                if (transferDay != null && transferDay != string.Empty)
                    TransferDay = int.Parse(transferDay);
                int TargetTime = 0;

                switch (TTM)
                {
                    case TrafficTransferMode.RecordBase:
                        int BasicTrafficsRowCount = basicTrafficRepository.GetBasicTrfficsRowCount();
                        toRecord = toRecord > 0 ? toRecord <= BasicTrafficsRowCount ? toRecord : BasicTrafficsRowCount : 1;
                        fromRecord = fromRecord > 0 ? fromRecord <= toRecord ? fromRecord : 1 : 1;
                        break;
                    case TrafficTransferMode.IdentifierBase:
                        decimal BasicTrafficsLastRowIdentifier = basicTrafficRepository.GetBaseTrafficsLastRowIdentifier();
                        toIdentifier = toIdentifier > 0 ? toIdentifier <= BasicTrafficsLastRowIdentifier ? toIdentifier : BasicTrafficsLastRowIdentifier : 0;
                        fromIdentifier = fromIdentifier >= 0 ? fromIdentifier <= toIdentifier ? fromIdentifier : 0 : 0;
                        break;
                }

                IList<BasicTraffic> BasicTrafficsList = basicTrafficRepository.GetAllBaiscTrafficsByConditions(TTM, machineID, FromDate, ToDate, FromTime, ToTime, fromRecord, toRecord, fromIdentifier, toIdentifier, IsIntegralConditions);
                foreach (BasicTraffic BasicTrafficsListItem in BasicTrafficsList)
                {
                    switch (TTT)
                    {
                        case TrafficTransferType.Backward:
                            TargetTime = BasicTrafficsListItem.Time - TransferTime;
                            if (TargetTime >= 0)
                            {
                                BasicTrafficsListItem.Time = TargetTime;
                                BasicTrafficsListItem.Date = BasicTrafficsListItem.Date.AddDays(-TransferDay);
                                base.SaveChanges(BasicTrafficsListItem, UIActionType.EDIT);
                            }
                            else
                            {
                                this.InsertTraffic(BasicTrafficsListItem.Person.ID, BasicTrafficsListItem.Precard.ID, BasicTrafficsListItem.Date.AddDays(-(TransferDay + 1)), BasicTrafficsListItem.Time + (24 * 60 - TransferTime), "TrraficTimeTransfer");
                                this.DeleteTraffic(BasicTrafficsListItem.ID);
                            }
                            break;
                        case TrafficTransferType.Forward:
                            TargetTime = BasicTrafficsListItem.Time + TransferTime;
                            if (TargetTime < 24 * 60)
                            {
                                BasicTrafficsListItem.Time = TargetTime;
                                BasicTrafficsListItem.Date = BasicTrafficsListItem.Date.AddDays(TransferDay);
                                base.SaveChanges(BasicTrafficsListItem, UIActionType.EDIT);
                            }
                            else
                            {
                                this.InsertTraffic(BasicTrafficsListItem.Person.ID, BasicTrafficsListItem.Precard.ID, BasicTrafficsListItem.Date.AddDays(TransferDay + 1), (BasicTrafficsListItem.Time + TransferTime) - 24 * 60, "TrraficTimeTransfer");
                                this.DeleteTraffic(BasicTrafficsListItem.ID);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "TransferDayTrafficsByConditions");
                throw ex;
            }
        }

        private decimal InsertTraffic(decimal personId, decimal precardId, DateTime date, int time, string description)
        {
            try
            {
                BasicTraffic basic = new BasicTraffic();
                basic.Person = new Person() { ID = personId };
                basic.Precard = new Precard() { ID = precardId };
                basic.OperatorPerson = new Person() { ID = BUser.CurrentUser.Person.ID };
                basic.Time = time;
                basic.Description = description;
                basic.Date = date;
                basic.Active = true;
                basic.Used = false;
                basic.Manual = true;
                base.SaveChanges(basic, UIActionType.ADD);
                return basic.ID;

            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "InsertTraffic");
                throw ex;
            }
        }


        #region BaseBusiness Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(BasicTraffic basicTraffic)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if (basicTraffic.Person == null || basicTraffic.Person.ID == 0)
            {
                exception.Add(ExceptionResourceKeys.TrafficPersonRequierd, "برای تردد پرسنل مشخص نشده است", ExceptionSrc);
            }

            if (basicTraffic.Date < Utility.GTSMinStandardDateTime)
            {
                exception.Add(ExceptionResourceKeys.TrafficDateRequierd, "تاریخ تردد وارد نشده است", ExceptionSrc);
            }
            if (basicTraffic.Time <= 0)
            {
                exception.Add(ExceptionResourceKeys.TrafficTimeRequierd, "زمان تردد وارد نشده است", ExceptionSrc);
            }
            if (basicTraffic.Precard == null || basicTraffic.Precard.ID == 0)
            {
                exception.Add(ExceptionResourceKeys.TrafficPrecardRequierd, "نوع تردد وارد نشده است", ExceptionSrc);
            }
            if (exception.Count == 0)
            {
                if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Active), true),
                                            new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Person), new Person() { ID = basicTraffic.Person.ID }),
                                            new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Time), basicTraffic.Time),
                                            new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Date), basicTraffic.Date),
                                            new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Precard), new Precard() { ID = basicTraffic.PrecardID })) > 0)
                {
                    exception.Add(ExceptionResourceKeys.TrafficIsRepeated, "تردد تکراری است", ExceptionSrc);
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
        protected override void UpdateValidate(BasicTraffic basicTraffic)
        {
            //throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نمیباشد", ExceptionSrc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(BasicTraffic obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            throw new NotImplementedException();

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UIValidate(BasicTraffic obj, UIActionType action)
        {
            UIValidator.DoValidate(obj);
        }

        protected override void UpdateCFP(BasicTraffic obj, UIActionType action)
        {
            base.UpdateCFP(obj.Person.ID, obj.Date);
        }
        #endregion

        public IList<ProceedTrafficProxy> GetAllTrafic(decimal prsId, DateTime fromDate, DateTime toDate)
        {
            IList<ProceedTrafficProxy> list3;
            try
            {
                EntityRepository<ProceedTraffic> repository = new EntityRepository<ProceedTraffic>();
                List<ProceedTrafficProxy> list = new List<ProceedTrafficProxy>();
                CriteriaStruct[] structArray = new CriteriaStruct[3];
                Person person = new Person();
                person.ID = prsId;
           
                IList<ProceedTraffic> byCriteria = repository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ProceedTraffic().Person), new Person() { ID = prsId }),
                                                                            new CriteriaStruct(Utility.GetPropertyName(() => new ProceedTraffic().FromDate), fromDate, CriteriaOperation.GreaterEqThan),
                                                                            new CriteriaStruct(Utility.GetPropertyName(() => new ProceedTraffic().FromDate), toDate, CriteriaOperation.LessEqThan));
                foreach (ProceedTraffic traffic in byCriteria)
                {
                    for (int i = 0; i < traffic.Pairs.Count; i++)
                    {
                        ProceedTrafficProxy item = new ProceedTrafficProxy();
                        item.From = Utility.IntTimeToRealTime(traffic.Pairs[i].From);
                        item.To = Utility.IntTimeToRealTime(traffic.Pairs[i].To);
                        item.Pishcard = traffic.Pairs[i].Precard.Name;
                        item.Date = Utility.ToString(traffic.FromDate);
                        list.Add(item);
                    }
                }
                list3 = list;
            }
            catch (Exception exception)
            {
                BaseBusiness<BasicTraffic>.LogException(exception);
                throw exception;
            }
            return list3;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckTrafficsControlLoadAccess()
        { 
        }


    }
}
