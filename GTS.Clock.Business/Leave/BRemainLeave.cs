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
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Validation.Configuration;
using GTS.Clock.Infrastructure.Repository.Leave;

namespace GTS.Clock.Business.Leave
{
    /// <summary>
    /// created at: 2012-01-29 5:18:22 PM
    /// by        : Farhad Salavati
    /// write your name here
    /// </summary>
    public class BRemainLeave : BaseBusiness<LeaveYearRemain>,ILeaveInfo
    {
        private const string ExceptionSrc = "GTS.Clock.Business.Leave.BRemainLeave";
        private EntityRepository<LeaveYearRemain> objectRep = new EntityRepository<LeaveYearRemain>();
        private LeaveYearRemainRepository LeaveYearRemainRep = new LeaveYearRemainRepository();
        IDataAccess dataAccessPort = new BUser();
        int minutesInDay = 8 * 60;

        #region BaseBusiness Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(LeaveYearRemain obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void UpdateValidate(LeaveYearRemain obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

           // throw new IllegalServiceAccess("دسترسی به سرویس بروزرسانی مانده مرخصی غیر مجاز است",ExceptionSrc);

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(LeaveYearRemain obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            //throw new NotImplementedException();

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UIValidate(LeaveYearRemain obj, UIActionType action)
        {
            UIValidator.DoValidate(obj);
        }

        protected override void UpdateCFP(LeaveYearRemain obj, UIActionType action)
        {
            base.UpdateCFP(obj.Person.ID, obj.Date);
        }
        #endregion

        #region GetAll
      
        /// <summary>
        /// مانده مرخصی را برای سالهای مشخص شده برمیگرداند
        /// </summary>
        /// <param name="fromYear"></param>
        /// <param name="toYear"></param>
        /// <returns></returns>
        public IList<RemainLeaveProxy> GetRemainLeave(int fromYear, int toYear, int pageIndex, int pageSize)
        {
            try
            {               
                IList<LeaveYearRemain> list = new List<LeaveYearRemain>();

                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                    toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                }
                else
                {
                    fromDate = new DateTime(fromYear, 1, 1);
                    toDate = new DateTime(toYear, 1, 1);
                }
               
                list = LeaveYearRemainRep.GetAllLeaveYearRemain(BUser.CurrentUser.ID, fromDate, toDate, pageIndex, pageSize);

                if (list != null && list.Count > 0)
                {
                    list = list.OrderBy(x => x.Person.LastName).ThenBy(x => x.Date.Year).ToList();

                }
                return this.ConvertToProxy(list);
                
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeave");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را برای سالهای مشخص شده برمیگرداند
        /// </summary>
        /// <param name="fromYear"></param>
        /// <param name="toYear"></param>
        /// <returns></returns>
        public IList<RemainLeaveProxy> GetRemainLeave(decimal personId, int fromYear, int toYear, int pageIndex, int pageSize)
        {
            try
            {
                IList<decimal> underManagmentList = new List<decimal>();
                underManagmentList.Add(personId);
               
                /*
                IList<LeaveYearRemain> list = new List<LeaveYearRemain>();
                if (underManagmentList != null && underManagmentList.Count > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                        toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                    }
                    else
                    {
                        fromDate = new DateTime(fromYear, 1, 1);
                        toDate = new DateTime(toYear, 1, 1);
                    }
                    list = objectRep.GetByCriteriaByPage(pageIndex, pageSize,
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate, CriteriaOperation.GreaterEqThan),
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate, CriteriaOperation.LessEqThan),
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), underManagmentList.ToArray<decimal>(), CriteriaOperation.IN));

                }
                if (list != null && list.Count > 0)
                {
                    list = list.OrderBy(x => x.Person.LastName).ThenBy(x => x.Date.Year).ToList();

                }
                return this.ConvertToProxy(list);
                 * */

                return this.GetRemainLeave(underManagmentList, fromYear, toYear, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeave");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را برای سالها و افراد جستجو شده  برمیگرداند
        /// حد اکثر 1000 رکورد بر میگرداند
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<RemainLeaveProxy> GetRemainLeave(PersonAdvanceSearchProxy proxy, int fromYear, int toYear, int pageIndex, int pageSize)
        {
            try
            {
                IList<decimal> underManagmentList = new List<decimal>();
                ISearchPerson searchTool = new BPerson();
                int count = 1000;//searchTool.GetPersonInAdvanceSearchCount(proxy);
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
                var l = from p in list
                        select p.ID;
                underManagmentList = l.ToList<decimal>();
              
                /*
                IList<LeaveYearRemain> result = new List<LeaveYearRemain>();
                if (underManagmentList != null && underManagmentList.Count > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                        toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                    }
                    else
                    {
                       fromDate = new DateTime(fromYear, 1, 1);
                        toDate = new DateTime(toYear, 1, 1);
                    }
                    result = objectRep.GetByCriteriaByPage(pageIndex, pageSize,
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate, CriteriaOperation.GreaterEqThan),
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate, CriteriaOperation.LessEqThan),
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), underManagmentList.ToArray<decimal>(), CriteriaOperation.IN));
                }
                if (result != null && result.Count > 0)
                {
                    result = result.OrderBy(x => x.Person.LastName).ThenBy(x => x.Date.Year).ToList();

                }
                return this.ConvertToProxy(result);
                 * */

                return this.GetRemainLeave(underManagmentList, fromYear, toYear, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeave");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را برای سالها و افراد جستجو شده  برمیگرداند
        /// حد اکثر 1000 رکورد بر میگرداند
        /// </summary>
        /// <param name="quickSearchKey"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<RemainLeaveProxy> GetRemainLeave(string quickSearchKey, int fromYear, int toYear, int pageIndex, int pageSize)
        {
            try
            {
                if (Utility.IsEmpty(quickSearchKey)) 
                {
                    return this.GetRemainLeave(fromYear, toYear, pageIndex, pageSize);
                }

                IList<decimal> underManagmentList = new List<decimal>();
                ISearchPerson searchTool = new BPerson();
                int count = 1000;// searchTool.GetPersonInQuickSearchCount(quickSearchKey);
                IList<Person> list = searchTool.QuickSearchByPage(0, count, quickSearchKey);
                var l = from p in list
                        select p.ID;
                underManagmentList = l.ToList<decimal>();
              
                /*
                IList<LeaveYearRemain> result = new List<LeaveYearRemain>();
                if (underManagmentList != null && underManagmentList.Count > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                        toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                    }
                    else
                    {
                       fromDate = new DateTime(fromYear, 1, 1);
                        toDate = new DateTime(toYear, 1, 1);
                    }
                    result = objectRep.GetByCriteriaByPage(pageIndex, pageSize,
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate, CriteriaOperation.GreaterEqThan),
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate, CriteriaOperation.LessEqThan),
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), underManagmentList.ToArray<decimal>(), CriteriaOperation.IN));
                }
                if (result != null && result.Count > 0)
                {
                    result = result.OrderBy(x => x.Person.LastName).ThenBy(x => x.Date.Year).ToList();

                }
                return this.ConvertToProxy(result);
                * */

                return this.GetRemainLeave(underManagmentList, fromYear, toYear, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeave");
                throw ex;
            }
        }

        private IList<RemainLeaveProxy> GetRemainLeave(IList<decimal> underManagmentList, int fromYear, int toYear, int pageIndex, int pageSize)
        {
            try
            {
                IList<LeaveYearRemain> result = new List<LeaveYearRemain>();
                if (underManagmentList != null && underManagmentList.Count > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                        toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                    }
                    else
                    {
                        fromDate = new DateTime(fromYear, 1, 1);
                        toDate = new DateTime(toYear, 1, 1);
                    }
                    result = objectRep.GetByCriteriaByPage(pageIndex, pageSize,
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate, CriteriaOperation.GreaterEqThan),
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate, CriteriaOperation.LessEqThan),
                                                                    new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), underManagmentList.ToArray<decimal>(), CriteriaOperation.IN));
                }
                if (result != null && result.Count > 0)
                {
                    result = result.OrderBy(x => x.Person.LastName).ThenBy(x => x.Date.Year).ToList();

                }
                return this.ConvertToProxy(result);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeave");
                throw ex;
            }
        }
      
        #endregion

        #region Count

        /// <summary>
        /// مانده مرخصی را برای سالهای مشخص شده برمیگرداند
        /// </summary>
        /// <param name="fromYear"></param>
        /// <param name="toYear"></param>
        /// <returns></returns>
        public int GetRemainLeaveCount(int fromYear, int toYear)
        {
            try
            {
                /*
                int count = 0;
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                    toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                }
                else
                {
                    fromDate = new DateTime(fromYear, 1, 1);
                    toDate = new DateTime(toYear, 1, 1);
                }
        
                count = LeaveYearRemainRep.GetLeaveYearRemainCount(BUser.CurrentUser.ID, fromDate, toDate);

                return count;
                */

                return this.GetRemainLeaveCount("", fromYear, toYear);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeaveCount");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را برای سالهای مشخص شده برمیگرداند
        /// </summary>
        /// <param name="fromYear"></param>
        /// <param name="toYear"></param>
        /// <returns></returns>
        public int GetRemainLeaveCount(decimal personId, int fromYear, int toYear)
        {
            try
            {
                IList<decimal> underManagmentList = new List<decimal>();
                underManagmentList.Add(personId);              
                
                /*
                if (underManagmentList != null && underManagmentList.Count > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                        toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                    }
                    else
                    {
                        fromDate = new DateTime(fromYear, 1, 1);
                        toDate = new DateTime(toYear, 1, 1);
                    }
                    count = objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate, CriteriaOperation.GreaterEqThan),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate, CriteriaOperation.LessEqThan),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), underManagmentList.ToArray<decimal>(), CriteriaOperation.IN));
                }
                return count;
                 * */
                return this.GetRemainLeaveCount(underManagmentList, fromYear, toYear);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeaveCount");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را برای سالها و افراد جستجو شده  برمیگرداند
        /// حد اکثر 1000 برمیگردد
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public int GetRemainLeaveCount(PersonAdvanceSearchProxy proxy, int fromYear, int toYear)
        {
            try
            {
                IList<decimal> underManagmentList = new List<decimal>();
                ISearchPerson searchTool = new BPerson();
                int count = 1000;//searchTool.GetPersonInAdvanceSearchCount(proxy);
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
                var l = from p in list
                        select p.ID;
                underManagmentList = l.ToList<decimal>();
                
                /*
                if (underManagmentList != null && underManagmentList.Count > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                        toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                    }
                    else
                    {
                        fromDate = new DateTime(fromYear, 1, 1);
                        toDate = new DateTime(toYear, 1, 1);
                    }
                    count = objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate, CriteriaOperation.GreaterEqThan),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate, CriteriaOperation.LessEqThan),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), underManagmentList.ToArray<decimal>(), CriteriaOperation.IN));

                }
                return count;*/

                return this.GetRemainLeaveCount(underManagmentList, fromYear, toYear);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeaveCount");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را برای سالها و افراد جستجو شده  برمیگرداند
        /// </summary>
        /// <param name="quickSearchKey"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public int GetRemainLeaveCount(string quickSearchKey, int fromYear, int toYear)
        {
            try
            {
                IList<decimal> underManagmentList = new List<decimal>();
                ISearchPerson searchTool = new BPerson();               
                int count = 1000;// searchTool.GetPersonInQuickSearchCount(quickSearchKey);
                IList<Person> list = searchTool.QuickSearchByPage(0, count, quickSearchKey);
                var l = from p in list
                        select p.ID;
                underManagmentList = l.ToList<decimal>();
               
                /*
                if (underManagmentList != null && underManagmentList.Count > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                        toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                    }
                    else
                    {
                        fromDate = new DateTime(fromYear, 1, 1);
                        toDate = new DateTime(toYear, 1, 1);
                    }
                    count = objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate, CriteriaOperation.GreaterEqThan),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate, CriteriaOperation.LessEqThan),
                                                          new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), underManagmentList.ToArray<decimal>(), CriteriaOperation.IN));
                }
                return count;
                 * */

                return this.GetRemainLeaveCount(underManagmentList, fromYear, toYear);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeaveCount");
                throw ex;
            }
        }

        private int GetRemainLeaveCount(IList<decimal> underManagmentList, int fromYear, int toYear)
        {
            try
            {
                int count = 0;
                if (underManagmentList != null && underManagmentList.Count > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                        toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                    }
                    else
                    {
                        fromDate = new DateTime(fromYear, 1, 1);
                        toDate = new DateTime(toYear, 1, 1);
                    }
                    count = objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate, CriteriaOperation.GreaterEqThan),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate, CriteriaOperation.LessEqThan),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), underManagmentList.ToArray<decimal>(), CriteriaOperation.IN));
                }
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeaveCount");
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="personId"></param>
        /// <param name="dayOK"></param>
        /// <param name="hourOK"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertLeaveYear(int year, decimal personId, string dayOK, string hourOK) 
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                if (personId > 0)
                {
                    int count = this.GetRemainLeaveCount(personId, year, year);
                    if (count == 0)
                    {
                        LeaveYearRemain leaveyear = new LeaveYearRemain();
                        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                        {
                            leaveyear.Date = Utility.ToMildiDate(String.Format("{0}/01/01", year));
                        }
                        else
                        {
                            leaveyear.Date = new DateTime(year, 1, 1);
                        }
                        leaveyear.Person = new Person() { ID = personId };

                        leaveyear.DayRemainOK = Utility.ToInteger(dayOK);
                        leaveyear.MinuteRemainOK = Utility.RealTimeToIntTime(hourOK);

                        this.SaveChanges(leaveyear, UIActionType.ADD);
                        return leaveyear.ID;
                    }
                    else
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.RemainLeaveExists, "طلب سالانه مرخصی برای این شخص و سال مشخص شده موجود میباشد ", ExceptionSrc));
                        throw exception;
                    }
                }
                else 
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RemainLeavePersonNotSelected, "پرسنلی برای انجام عملیات انتخاب نشده است ", ExceptionSrc));
                    throw exception;
                }
            }
            catch (Exception ex) 
            {
                LogException(ex, "BRemainLeave", "InsertLeaveYear");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateLeaveYear(decimal remainLeaveId, string dayOK, string hourOK)
        {
            try
            {
                LeaveYearRemain leaveYear = this.GetByID(remainLeaveId);

                leaveYear.DayRemainOK = Utility.ToInteger(dayOK);
                leaveYear.MinuteRemainOK = Utility.RealTimeToIntTime(hourOK);

                this.SaveChanges(leaveYear, UIActionType.EDIT);
                return leaveYear.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "UpdateLeaveYear");
                throw ex;
            }
        }

        #region Transfer To Next Year
     
        /// <summary>
        /// اتقال مرخصی به سال بعد
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="fromYear"></param>
        /// <param name="toYear"></param>
        /// <returns></returns>
        public decimal TransferToNextYear(decimal personId, int fromYear, int toYear)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                    toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                }
                else
                {
                    fromDate = new DateTime(fromYear, 1, 1);
                    toDate = new DateTime(toYear, 1, 1);
                }
                return this.TransferToNextYear(personId, fromDate, toDate, fromYear, toYear);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "TransferToNextYear");
                throw ex;
            }
        }

        /// <summary>
        ///  اتقال مرخصی به سال بعد
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="fromYear"></param>
        /// <param name="toYear"></param>
        public int TransferToNextYear(PersonAdvanceSearchProxy proxy, int fromYear, int toYear)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                    toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                }
                else
                {
                    fromDate = new DateTime(fromYear, 1, 1);
                    toDate = new DateTime(toYear, 1, 1);
                }

                IList<decimal> underManagmentList = new List<decimal>();
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
                var l = from p in list
                        select p.ID;
                underManagmentList = l.ToList<decimal>();
                int counter = 0;
                foreach (decimal personId in underManagmentList)
                {
                    try
                    {
                        this.TransferToNextYear(personId, fromDate, toDate, fromYear, toYear);
                        counter++;
                    }
                    catch (UIValidationExceptions ex)
                    {
                        LogException(ex);
                    }
                }
                return counter;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "TransferToNextYear");
                throw ex;
            }
        }

        /// <summary>
        ///  اتقال مرخصی به سال بعد
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="fromYear"></param>
        /// <param name="toYear"></param>
        public int TransferToNextYear(string searchKey, int fromYear, int toYear)
        {
            DateTime fromDate, toDate;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
            }
            else
            {
                fromDate = new DateTime(fromYear, 1, 1);
                toDate = new DateTime(toYear, 1, 1);
            }

            IList<decimal> underManagmentList = new List<decimal>();
            ISearchPerson searchTool = new BPerson();
            int count = searchTool.GetPersonInQuickSearchCount(searchKey);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, searchKey);
            var l = from p in list
                    select p.ID;
            underManagmentList = l.ToList<decimal>();
            int counter = 0;
            foreach (decimal personId in underManagmentList)
            {
                try
                {
                    this.TransferToNextYear(personId, fromDate, toDate, fromYear, toYear);
                    counter++;
                }
                catch (UIValidationExceptions ex)
                {
                    LogException(ex);
                }
            }
            return counter;
        }

        private decimal TransferToNextYear(decimal personId, DateTime fromDate, DateTime toDate, int fromYear, int toYear)
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                if (toYear - fromYear != 1)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RemainTransferFromToYearDiffrenceMoreThanOne, "اختلاف سال شروع و پایان برای انتقال مانده مرخصی به سال بعد باید یک باشد", ExceptionSrc));
                }

                int fromYearCount = this.GetRemainLeaveCount(personId, fromYear, fromYear);
                LeaveYearRemain toLeaveYear = objectRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate),
                                                                      new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate),
                                                                      new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), personId)).FirstOrDefault();
                if (toLeaveYear == null)
                {
                    toLeaveYear = new LeaveYearRemain();
                }
                if (fromYearCount == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RemainTransferFromYearIsNotExists, "طلب سالانه مرخصی برای سال شروع مشخص شده موجود نمیباشد ", ExceptionSrc));
                }

                if (exception.Count > 0)
                {
                    throw exception;
                }
                GTSEngineWS.TotalWebServiceClient gtsEngineWS = new GTS.Clock.Business.GTSEngineWS.TotalWebServiceClient();
                gtsEngineWS.GTS_ExecuteByPersonID(BUser.CurrentUser.UserName, personId);

                int realDay, realMinute;

                this.GetRemainLeaveToEndOfYear(personId, fromYear, 12, out realDay, out realMinute);

                toLeaveYear.Date = toDate;
                toLeaveYear.Person = new Person() { ID = personId };

                toLeaveYear.DayRemainReal = realDay;
                toLeaveYear.MinuteRemainReal = realMinute;

                toLeaveYear.DayRemainOK = realDay;
                toLeaveYear.MinuteRemainOK = realMinute;

                decimal leaveRemainID = 0;
                if (toLeaveYear.ID != 0)
                {
                    leaveRemainID = this.SaveChanges(toLeaveYear, UIActionType.EDIT);
                }
                else
                {
                    leaveRemainID = this.SaveChanges(toLeaveYear, UIActionType.ADD);
                }
                this.ExtraLeaveYearRemainsExistanceValidate(leaveRemainID, toDate, personId);
                return toLeaveYear.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "TransferToNextYear");
                throw ex;
            }
        }
        
        #endregion
       
        private void ExtraLeaveYearRemainsExistanceValidate(decimal finalLeaveYearRemainID, DateTime targetDate, decimal personnelID)
        {
            IList<LeaveYearRemain> ExtraLeaveYearRemainsList = this.LeaveYearRemainRep.GetExtraLeaveYearRemains(finalLeaveYearRemainID, targetDate, personnelID);
            foreach (LeaveYearRemain ExtraLeaveYearRemainItem in ExtraLeaveYearRemainsList)
            {
                this.SaveChanges(ExtraLeaveYearRemainItem, UIActionType.DELETE);
            }
        }

        /// <summary>
        /// مانده مرخصی سالهای قبل را به پروکسی تبدیل میکند
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private IList<RemainLeaveProxy> ConvertToProxy(IList<LeaveYearRemain> list) 
        {
            

            IList<RemainLeaveProxy> proxyList = new List<RemainLeaveProxy>();
            foreach (LeaveYearRemain leave in list) 
            {
                RemainLeaveProxy proxy = new RemainLeaveProxy();
                
                proxy.ID = leave.ID;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.Year = Utility.ToPersianDateTime(leave.Date).Year;
                }
                else 
                {
                    proxy.Year = leave.Date.Year;
                }
               
                proxy.Person = leave.Person;

                proxy.RealDay = leave.DayRemainReal.ToString();
                proxy.RealHour = Utility.IntTimeToTime(leave.MinuteRemainReal, true);

                proxy.ConfirmedDay = leave.DayRemainOK.ToString();
                proxy.ConfirmedHour = Utility.IntTimeToTime(leave.MinuteRemainOK, true);

                proxyList.Add(proxy);
            }
            return proxyList;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckLeaveRemainsLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void TransferToNextYear()
        { 
        }


        #region ILeaveInfo Members

        /// <summary>
        /// مانده مرخصی تا انتهای ماه جاری
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="minutes"></param>
        public void GetRemainLeaveToEndOfMonth(decimal personId, int year, int month, out int day, out int minutes)
        {
            try
            {
                PersonRepository prsRep = new PersonRepository();

                DateTime endYear = new DateTime();
                DateTime startYear = new DateTime();
                DateTime endMonth = new DateTime();

                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    endMonth = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, Utility.GetEndOfPersianMonth(year, month)));
                    startYear = Utility.ToMildiDate(String.Format("{0}/1/1", year));
                    endYear = Utility.ToMildiDate(String.Format("{0}/12/{1}", year, Utility.GetEndOfPersianMonth(year, 12)));
                }
                else
                {
                    endMonth = new DateTime(year, month, Utility.GetEndOfMiladiMonth(year, month));
                    startYear = new DateTime(year, 1, 1);
                    endYear = new DateTime(year, 12, Utility.GetEndOfMiladiMonth(year, 12));
                }

                day = 0;
                minutes = 0;
                try
                {
                    Person prs = prsRep.GetById(personId, false);
                    prs.CalcDateZone = new DateRange(startYear, endYear, startYear, endYear);
                    prsRep.EnableEfectiveDateFilter(prs.ID, prs.CalcDateZone.FromDate, prs.CalcDateZone.ToDate, startYear, endYear, prs.CalcDateZone.FromDate.AddDays(-20), prs.CalcDateZone.ToDate.AddDays(+20));

                    LeaveInfo linfo = prs.GetRemainLeaveToDateUI(endMonth);
                    day = linfo.Day;
                    minutes = linfo.Minute;
                }
                catch (InvalidDatabaseStateException ex)
                {
                    if (ex.FatalExceptionIdentifier == UIFatalExceptionIdentifiers.LeaveLCRDoesNotExists)
                    {
                        day = 0;
                        minutes = 0;
                        LogException(ex);
                    }
                    else throw ex;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی سالهای قبل را به پروکسی تبدیل میکند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public void GetRemainLeaveToEndOfYear(decimal personId, int year, int month, out int day, out int minutes)
        {
            try
            {

                PersonRepository prsRep = new PersonRepository();              

                DateTime endYear = new DateTime();
                DateTime startYear = new DateTime();
                DateTime endMonth = new DateTime();

                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    endMonth = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, Utility.GetEndOfPersianMonth(year, month)));
                    startYear = Utility.ToMildiDate(String.Format("{0}/1/1", year));
                    endYear = Utility.ToMildiDate(String.Format("{0}/12/{1}", year, Utility.GetEndOfPersianMonth(year, 12)));
                }
                else
                {
                    endMonth = new DateTime(year, month, Utility.GetEndOfMiladiMonth(year, month));
                    startYear = new DateTime(year, 1, 1);
                    endYear = new DateTime(year, 12, Utility.GetEndOfMiladiMonth(year, 12));
                }
                day = 0;
                minutes = 0;
                try
                {
                    Person prs = prsRep.GetById(personId, false);
                    prs.CalcDateZone = new DateRange(startYear, endYear, startYear, endYear);
                    prsRep.EnableEfectiveDateFilter(prs.ID, prs.CalcDateZone.FromDate, prs.CalcDateZone.ToDate, startYear, endYear, prs.CalcDateZone.FromDate.AddDays(-20), prs.CalcDateZone.ToDate.AddDays(+20));

                    LeaveInfo linfo = prs.GetRemainLeaveToEndOfYearUI(endMonth, startYear, endYear);
                    day = linfo.Day;
                    minutes = linfo.Minute;
                }
                catch (InvalidDatabaseStateException ex)
                {
                    if (ex.FatalExceptionIdentifier == UIFatalExceptionIdentifiers.LeaveLCRDoesNotExists)
                    {
                        day = 0;
                        minutes = 0;
                        LogException(ex);
                    }
                    else throw ex;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        #endregion
    }
}
