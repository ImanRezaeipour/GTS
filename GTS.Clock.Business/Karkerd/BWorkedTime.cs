using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Remoting.Messaging;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.MonthlyReport; 
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Leave;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Presentaion_Helper.Proxy;

namespace GTS.Clock.Business.WorkedTime
{
    public class BWorkedTime : MarshalByRefObject
    {
        IDataAccess accessPort = new BUser();
        const string ExceptionSrc = "GTS.Clock.Business.WorkedTime.BWorkedTime";
        ManagerRepository managerRepository = new ManagerRepository(false);
        LanguagesName sysLanguageResource;

        public string Username { get; set; }

        private Manager manager = null;

        public BWorkedTime()
        {
            if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                this.sysLanguageResource = LanguagesName.Parsi;
            }
            else if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.English)
            {
                this.sysLanguageResource = LanguagesName.English;
            }
        }

        public BWorkedTime(string username)
        {
            this.Username = username;
            if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                this.sysLanguageResource = LanguagesName.Parsi;
            }
            else if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.English)
            {
                this.sysLanguageResource = LanguagesName.English;
            }
        }

        /// <summary>
        /// اگر کاربر فعلی مدیر باشد درختی که تحت مدیریت او است را با خصیصه "پدیداری" مشخص میکند
        /// </summary>
        /// <returns></returns>
        public Department GetManagerDepartmentTree()
        {
            try
            {
                if (InitManager())
                {
                    Department root = new BDepartment().GetManagerDepartmentTree(manager.ID);                
                    return root;
                }
                else if (InitOperator()) 
                {
                    Department root = new BDepartment().GetOperatorDepartmentTree(BUser.CurrentUser.Person.ID);
                    return root;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkedTime", "GetManagerDepartmentTree");
                throw ex;
            }
        }

        /// <summary>
        /// با دریافت یک گره از درخت تحت مدیریت مدیر تعداد اشخاص تحت مدیریت را برمیگرداند
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public int GetUnderManagmentByDepartmentCount(int month, decimal departmentID)
        {
            try
            {
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    if (month <= 0)
                    {
                        month = Utility.ToPersianDateTime(DateTime.Now).Month;
                    }
                }
                else
                {
                    if (month <= 0)
                    {
                        month = DateTime.Now.Month;
                    }
                }
                if (InitManager())
                {
                    int Result = managerRepository.GetUnderManagmentByDepartmentCount(GridSearchFields.NONE, BUser.CurrentUser.Person.ID, departmentID, "", "");
                    return Result;
                }
                else if (InitOperator())
                {
                    int Result = managerRepository.GetUnderManagmentOperatorByDepartmentCount(GridSearchFields.NONE, BUser.CurrentUser.Person.ID, departmentID, "", "");
                    return Result;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkedTime", "GetUnderManagmentByDepartmentCount");
                throw ex;
            }
        }

        /// <summary>
        /// با دریافت یک گره از درخت تحت مدیریت مدیر اشخاص تحت مدیریت را برمیگرداند
        /// این افراد مرتب شده بر اساس فیلد مشخص شده هستند
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public IList<UnderManagementPerson> GetUnderManagmentByDepartment(int month, decimal departmentID, int pageIndex, int pageSize, GridOrderFields orderField, GridOrderFieldType orderType)
        {
            try
            {
                int year = 0;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    year = Utility.ToPersianDateTime(DateTime.Now).Year;
                    if (month <= 0)
                    {
                        month = Utility.ToPersianDateTime(DateTime.Now).Month;
                    }
                }
                else
                {
                    year = DateTime.Now.Year;
                    if (month <= 0)
                    {
                        month = DateTime.Now.Month;
                    }
                }

                if (orderField == GridOrderFields.NONE)
                    orderField = GridOrderFields.gridFields_BarCode;
                if (InitManager())
                {                   
                    IList<UnderManagementPerson> Result = managerRepository.GetUnderManagmentByDepartment(GridSearchFields.NONE, BUser.CurrentUser.Person.ID, departmentID, "", "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    foreach (UnderManagementPerson under in Result)
                    {
                        #region LoadBasicTraffic
                        GTS.Clock.Business.BaseInformation.BTraffic trafficBus = new BaseInformation.BTraffic();
                        IList<BasicTrafficProxy> trafficList = trafficBus.GetDayTraffics(under.PersonId, Utility.ToString(DateTime.Now));
                        int counter = 1;
                        foreach (BasicTrafficProxy trafic in trafficList)
                        {
                            switch (counter)
                            {
                                case 1:
                                    under.FirstEntrance = trafic.TheTime;
                                    break;
                                case 2:
                                    under.FirstExit = trafic.TheTime;
                                    break;
                                case 3:
                                    under.SecondEntrance = trafic.TheTime;
                                    break;
                                case 4:
                                    under.SecondExit = trafic.TheTime;
                                    break;
                                case 5:
                                    under.ThirdEntrance = trafic.TheTime;
                                    break;
                                case 6:
                                    under.ThirdExit = trafic.TheTime;
                                    break;
                                case 7:
                                    under.FourthEntrance = trafic.TheTime;
                                    break;
                                case 8:
                                    under.FourthExit = trafic.TheTime;
                                    break;
                                case 9:
                                    under.FifthEntrance = trafic.TheTime;
                                    break;
                                case 10:
                                    under.FifthExit = trafic.TheTime;
                                    break;

                            }
                            counter++;
                        }
                        under.LastExit = trafficList.LastOrDefault() != null ? trafficList.LastOrDefault().TheTime : "";
                        #endregion

                        under.RemainLeaveToYearEnd = this.GetReainLeaveToEndOfYear(under.PersonId, year, month);
                        under.RemainLeaveToMonthEnd = this.GetReaiLeaveToEndMonth(under.PersonId, year, month);
                    }
                    return Result;
                }
                else if (InitOperator())
                {                    
                    IList<UnderManagementPerson> Result = managerRepository.GetUnderManagmentOperatorByDepartment(GridSearchFields.NONE, BUser.CurrentUser.Person.ID, departmentID, "", "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    foreach (UnderManagementPerson under in Result)
                    {
                        #region LoadBasicTraffic
                        GTS.Clock.Business.BaseInformation.BTraffic trafficBus = new BaseInformation.BTraffic();
                        IList<BasicTrafficProxy> trafficList = trafficBus.GetDayTraffics(under.PersonId, Utility.ToString(DateTime.Now));
                        int counter = 1;
                        foreach (BasicTrafficProxy trafic in trafficList)
                        {
                            switch (counter)
                            {
                                case 1:
                                    under.FirstEntrance = trafic.TheTime;
                                    break;
                                case 2:
                                    under.FirstExit = trafic.TheTime;
                                    break;
                                case 3:
                                    under.SecondEntrance = trafic.TheTime;
                                    break;
                                case 4:
                                    under.SecondExit = trafic.TheTime;
                                    break;
                                case 5:
                                    under.ThirdEntrance = trafic.TheTime;
                                    break;
                                case 6:
                                    under.ThirdExit = trafic.TheTime;
                                    break;
                                case 7:
                                    under.FourthEntrance = trafic.TheTime;
                                    break;
                                case 8:
                                    under.FourthExit = trafic.TheTime;
                                    break;
                                case 9:
                                    under.FifthEntrance = trafic.TheTime;
                                    break;
                                case 10:
                                    under.FifthExit = trafic.TheTime;
                                    break;

                            }
                            counter++;
                        }
                        under.LastExit = trafficList.LastOrDefault() != null ? trafficList.LastOrDefault().TheTime : "";
                        #endregion

                        under.RemainLeaveToYearEnd = this.GetReainLeaveToEndOfYear(under.PersonId, year, month);
                        under.RemainLeaveToMonthEnd = this.GetReaiLeaveToEndMonth(under.PersonId, year, month);
                    }
                    return Result;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkedTime", "GetUnderManagmentByDepartment");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد افراد تحت مدیریت را براساس کلمه جستجوشده برمیگرداند 
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public int GetUnderManagmentBySearchCount(int month, string searchKey, GridSearchFields searchType)
        {
            try
            {
                if (InitManager())
                {
                    int Result = 0;
                    if (searchType == GridSearchFields.PersonName)
                    {
                        Result = managerRepository.GetUnderManagmentByDepartmentCount(GridSearchFields.PersonName, manager.ID, 0, searchKey, "");
                    }
                    else if (searchType == GridSearchFields.PersonCode)
                    {
                        Result = managerRepository.GetUnderManagmentByDepartmentCount(GridSearchFields.PersonCode, BUser.CurrentUser.Person.ID, 0, "", searchKey);
                    }
                    else
                    {
                        Result = managerRepository.GetUnderManagmentByDepartmentCount(GridSearchFields.PersonName, BUser.CurrentUser.Person.ID, 0, searchKey, "");
                        if (Result == 0)
                            Result = managerRepository.GetUnderManagmentByDepartmentCount(GridSearchFields.PersonCode, BUser.CurrentUser.Person.ID, 0, "", searchKey);
                    }
                    return Result;
                }
                else if (InitOperator())
                {
                    decimal oprPrsId = BUser.CurrentUser.Person.ID;
                    int Result = 0;
                    if (searchType == GridSearchFields.PersonName)
                    {
                        Result = managerRepository.GetUnderManagmentOperatorByDepartmentCount(GridSearchFields.PersonName, oprPrsId, 0, searchKey, "");
                    }
                    else if (searchType == GridSearchFields.PersonCode)
                    {
                        Result = managerRepository.GetUnderManagmentOperatorByDepartmentCount(GridSearchFields.PersonCode, oprPrsId, 0, "", searchKey);
                    }
                    else
                    {
                        Result = managerRepository.GetUnderManagmentOperatorByDepartmentCount(GridSearchFields.PersonName, oprPrsId, 0, searchKey, "");
                        if (Result == 0)
                            Result = managerRepository.GetUnderManagmentOperatorByDepartmentCount(GridSearchFields.PersonCode, oprPrsId, 0, "", searchKey);
                    }
                    return Result;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkedTime", "GetUnderManagmentBySearchCount");
                throw ex;
            }
        }

        /// <summary>
        /// افراد تحت مدیریت را براساس کلمه جستجوشده برمیگرداند
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="searchType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderField"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public IList<UnderManagementPerson> GetUnderManagmentBySearch(int month, string searchKey, GridSearchFields searchType, int pageIndex, int pageSize, GridOrderFields orderField, GridOrderFieldType orderType)
        {
            try
            {
                if (InitManager())
                {
                    if (orderField == GridOrderFields.NONE)
                        orderField = GridOrderFields.gridFields_BarCode;
                    IList<UnderManagementPerson> Result = new List<UnderManagementPerson>();
                    if (searchType == GridSearchFields.PersonName)
                    {
                        Result = managerRepository.GetUnderManagmentByDepartment(GridSearchFields.PersonName, BUser.CurrentUser.Person.ID, 0, searchKey, "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    }
                    else if (searchType == GridSearchFields.PersonCode)
                    {
                        Result = managerRepository.GetUnderManagmentByDepartment(GridSearchFields.PersonCode, BUser.CurrentUser.Person.ID, 0, "", searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    }
                    else
                    {
                        Result = managerRepository.GetUnderManagmentByDepartment(GridSearchFields.PersonName, BUser.CurrentUser.Person.ID, 0, searchKey, "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                        if (Result == null || Result.Count == 0)
                            Result = managerRepository.GetUnderManagmentByDepartment(GridSearchFields.PersonCode, BUser.CurrentUser.Person.ID, 0, "", searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    }
                    foreach (UnderManagementPerson under in Result)
                    {
                        #region LoadBasicTraffic
                        GTS.Clock.Business.BaseInformation.BTraffic trafficBus = new BaseInformation.BTraffic();
                        IList<BasicTrafficProxy> trafficList = trafficBus.GetDayTraffics(under.PersonId, Utility.ToString(DateTime.Now));
                        int counter = 1;
                        foreach (BasicTrafficProxy trafic in trafficList)
                        {
                            switch (counter)
                            {
                                case 1:
                                    under.FirstEntrance = trafic.TheTime;
                                    break;
                                case 2:
                                    under.FirstExit = trafic.TheTime;
                                    break;
                                case 3:
                                    under.SecondEntrance = trafic.TheTime;
                                    break;
                                case 4:
                                    under.SecondExit = trafic.TheTime;
                                    break;
                                case 5:
                                    under.ThirdEntrance = trafic.TheTime;
                                    break;
                                case 6:
                                    under.ThirdExit = trafic.TheTime;
                                    break;
                                case 7:
                                    under.FourthEntrance = trafic.TheTime;
                                    break;
                                case 8:
                                    under.FourthExit = trafic.TheTime;
                                    break;
                                case 9:
                                    under.FifthEntrance = trafic.TheTime;
                                    break;
                                case 10:
                                    under.FifthExit = trafic.TheTime;
                                    break;

                            }
                            counter++;
                        }
                        under.LastExit = trafficList.LastOrDefault() != null ? trafficList.LastOrDefault().TheTime : "";
                        #endregion
                    }
                    return Result;
                }
                else if (InitOperator()) 
                {
                    decimal oprPrsId = BUser.CurrentUser.Person.ID;
                    if (orderField == GridOrderFields.NONE)
                        orderField = GridOrderFields.gridFields_BarCode;
                    IList<UnderManagementPerson> Result = new List<UnderManagementPerson>();
                    if (searchType == GridSearchFields.PersonName)
                    {
                        Result = managerRepository.GetUnderManagmentOperatorByDepartment(GridSearchFields.PersonName, oprPrsId, 0, searchKey, "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    }
                    else if (searchType == GridSearchFields.PersonCode)
                    {
                        Result = managerRepository.GetUnderManagmentOperatorByDepartment(GridSearchFields.PersonCode, oprPrsId, 0, "", searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    }
                    else
                    {
                        Result = managerRepository.GetUnderManagmentOperatorByDepartment(GridSearchFields.PersonName, oprPrsId, 0, searchKey, "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                        if (Result == null || Result.Count == 0)
                            Result = managerRepository.GetUnderManagmentOperatorByDepartment(GridSearchFields.PersonCode, oprPrsId, 0, "", searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    }
                    foreach (UnderManagementPerson under in Result)
                    {
                        #region LoadBasicTraffic
                        GTS.Clock.Business.BaseInformation.BTraffic trafficBus = new BaseInformation.BTraffic();
                        IList<BasicTrafficProxy> trafficList = trafficBus.GetDayTraffics(under.PersonId, Utility.ToString(DateTime.Now));
                        int counter = 1;
                        foreach (BasicTrafficProxy trafic in trafficList)
                        {
                            switch (counter)
                            {
                                case 1:
                                    under.FirstEntrance = trafic.TheTime;
                                    break;
                                case 2:
                                    under.FirstExit = trafic.TheTime;
                                    break;
                                case 3:
                                    under.SecondEntrance = trafic.TheTime;
                                    break;
                                case 4:
                                    under.SecondExit = trafic.TheTime;
                                    break;
                                case 5:
                                    under.ThirdEntrance = trafic.TheTime;
                                    break;
                                case 6:
                                    under.ThirdExit = trafic.TheTime;
                                    break;
                                case 7:
                                    under.FourthEntrance = trafic.TheTime;
                                    break;
                                case 8:
                                    under.FourthExit = trafic.TheTime;
                                    break;
                                case 9:
                                    under.FifthEntrance = trafic.TheTime;
                                    break;
                                case 10:
                                    under.FifthExit = trafic.TheTime;
                                    break;

                            }
                            counter++;
                        }
                        under.LastExit = trafficList.LastOrDefault() != null ? trafficList.LastOrDefault().TheTime : "";
                        #endregion
                    }
                    return Result;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkedTime", "GetUnderManagmentByDepartment");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را انتهای سال برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private string GetReainLeaveToEndOfYear(decimal personId, int year, int month)
        {
            string remain = "";
            try
            {
                ILeaveInfo leaveInfo = new BRemainLeave();
                int day, minutes;
                leaveInfo.GetRemainLeaveToEndOfYear(personId, year, month, out day, out minutes);
                int hour = (minutes / 60);
                int min = minutes % 60;
                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    remain = String.Format(" {0} روز و {1} ساعت و {2} دقیقه", day, hour, min);
                }
                else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                {
                    remain = String.Format(" {0} days and {1} hours and {2} minutes", day, hour, min);
                }
            }
            catch (InvalidDatabaseStateException ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkdTime", "GetRemainLeaveToEndOfYear");
                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    remain = " محاسبه نشده";
                }
                else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                {
                    remain = "Not Calculated";
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkdTime", "GetRemainLeaveToEndOfYear");
                throw ex;
            }
            return remain;
        }

        /// <summary>
        /// مانده مرخصی را انتهای ماه برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        private string GetReaiLeaveToEndMonth(decimal personId, int year, int month)
        {
            string remain = "";
            try
            {
                ILeaveInfo leaveInfo = new BRemainLeave();
                int day, minutes;
                leaveInfo.GetRemainLeaveToEndOfMonth(personId, year, month, out day, out minutes);
                int hour = (minutes / 60);
                int min = minutes % 60;
                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    remain = String.Format(" {0} روز و {1} ساعت و {2} دقیقه", day, hour, min);
                }
                else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                {
                    remain = String.Format(" {0} days and {1} hours and {2} minutes", day, hour, min);
                }
            }
            catch (InvalidDatabaseStateException ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkdTime", "GetReaiLeaveToEndMonth");
                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    remain = " محاسبه نشده";
                }
                else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                {
                    remain = "Not Calculated";
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkdTime", "GetReaiLeaveToEndMonth");
                throw ex;
            }
            return remain;
        }

        /// <summary>
        /// سر ستون رزورو فیلدها را در گزارش کارکرد ماهانه برمیگرداند
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public string GetReservedFieldsName(ConceptReservedFields field)
        {
            try
            {
                return new BPersonMonthlyWorkedTime(0).GetReservedFieldsName(field);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, this.GetType().Name, "GetReservedFieldsName");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckManagerMasterMonthlyOperationLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPersonnelMasterMonthlyOperationLoadAccess()
        {
        }


        #region private methods

        /// <summary>
        /// بصورت بازگشتی درحت را پیمایش و شرط نمایش را بررسی میکند
        ///  اگر تشخیص داده شد که گره ای نباید نشان داده شود نیازی به پیمایش گره های فرزند نیست
        ///  زیرا این تشخیص شامل آنها نیز میشود
        /// </summary>
        /// <param name="department"></param>
        /// <param name="visibleIds"></param>
        private void SetVisibility(Department department, Flow flow, IList<Department> containsChildList)
        {
            BFlow bFlow = new BFlow();
            //IList<decimal> restrictionIds = accessPort.GetAccessibleDeparments();
            if (department.ChildList != null)
            {
                foreach (Department child in department.ChildList)
                {
                    if (!containsChildList.Contains(child))
                    {
                        child.Visible = child.Visible || false;//ممکن است در جریانهای قبلی مقدار یک گرفته باشد
                    }
                    else
                    {
                        child.Visible = true;
                        this.SetVisibility(child, flow, bFlow.GetDepartmentChilds(child.ID, flow.ID));
                    }
                }
            }
        }

        private int GetOrder(DateTime dt, SysLanguageResource sysLanguageResource)
        {
            switch (sysLanguageResource)
            {
                case SysLanguageResource.English: return dt.Month;
                default: return (new PersianDateTime(dt)).Month;
            }
        }

        /// <summary>
        /// بررسی و مقداردهی مدیر از روی شناسه کاربری
        /// </summary>
        /// <returns></returns>
        private bool InitManager()
        {
            if (manager == null)
            {
                if (Utility.IsEmpty(this.Username))
                {
                    this.Username = Security.BUser.CurrentUser.UserName;
                }
                BManager businessManager = new BManager();
                manager = businessManager.GetManagerByUsername(this.Username);
            }
            if (manager.ID == 0)// جانشین
            {
                SubstituteRepository subRep = new SubstituteRepository(false);
                if (subRep.IsSubstitute(Security.BUser.CurrentUser.Person.ID))
                {
                    IList<Substitute> sub = subRep.GetSubstitute(Security.BUser.CurrentUser.Person.ID);
                    manager = sub.First().Manager;
                }
            }
            if (manager.ID > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// بررسی و مقداردهی اپراتور از روی شناسه کاربری
        /// </summary>
        /// <returns></returns>
        private bool InitOperator()
        {
            BOperator businessOperator = new BOperator();
            return businessOperator.IsOperator();
        }

        #endregion


    }
}