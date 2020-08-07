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
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.Leave
{


    /// <summary>
    /// created at: 2012-01-16 9:39:02 AM
    /// by        : $GTSDeveloper$
    /// write your name here
    /// </summary>
    public class BUserInfo:MarshalByRefObject
    {
        public delegate void UserInfoMessage(decimal personId, IList<string> result);
        private UserInfoMessage infoProviders;

        public BUserInfo()
        {
            infoProviders += new UserInfoMessage(this.GetRemainMonthLeave);
            infoProviders += new UserInfoMessage(this.GetRemainYearLeave);

        }

        public IList<string> GetUserInfo(decimal personId)
        {
            try
            {
                IList<string> list = new List<string>();

                infoProviders.Invoke(personId, list);

                return list;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BUserInfo", "GetUserInfo");
                throw ex;
            }
        }

        private void GetRemainMonthLeave(decimal personId, IList<string> result)
        {
            try
            {
                string remain = "";
                int year = 0, month = 0, day, minutes, hour, min;
                bool negative = false;
                PersonRepository prsRep = new PersonRepository();
                ILeaveInfo leaveInfo = new BRemainLeave();               
               
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    year = Utility.ToPersianDateTime(DateTime.Now).Year;
                    month = Utility.ToPersianDateTime(DateTime.Now).Month;
                }
                else
                {
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                }
              
                leaveInfo.GetRemainLeaveToEndOfMonth(personId, year, month, out day, out minutes);
                hour = (minutes / 60);
                min = minutes % 60;

                string dayValue = day >= 0 ? day.ToString() : String.Format("-({0})", Math.Abs(day));
                string hourValue = hour >= 0 ? hour.ToString() : String.Format("-({0})", Math.Abs(hour));
                string minValue = min >= 0 ? min.ToString() : String.Format("-({0})", Math.Abs(min));

                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    remain = String.Format("مانده مرخصی تا انتهای ماه جاری {0} روز و {1} ساعت و {2} دقیقه", dayValue, hourValue, minValue);
                }
                else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                {
                    remain = String.Format("Remains off until the end of this Month, {0} days and {1} hours and {2} minutes", dayValue, hourValue, minValue);
                }
                result.Add(remain);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BUserInfo", "GetRemainMonthLeave");
                throw ex;
            }
        }

        private void GetRemainYearLeave(decimal personId, IList<string> result)
        {
            try
            {
                string remain = "";
                int day, minutes, hour, min,year = 0, month = 0;
                ILeaveInfo leaveInfo = new BRemainLeave();
                PersonRepository prsRep = new PersonRepository();
                Person prs = prsRep.GetById(personId, false);
              
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    year = Utility.ToPersianDateTime(DateTime.Now).Year;
                    month = Utility.ToPersianDateTime(DateTime.Now).Month;
                }
                else
                {
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                }
               
                leaveInfo.GetRemainLeaveToEndOfYear(personId, year, month, out day, out minutes);
                hour = (minutes / 60);
                min = minutes % 60;

                string dayValue = day >= 0 ? day.ToString() : String.Format("-({0})", Math.Abs(day));
                string hourValue = hour >= 0 ? hour.ToString() : String.Format("-({0})", Math.Abs(hour));
                string minValue = min >= 0 ? min.ToString() : String.Format("-({0})", Math.Abs(min));

                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    remain = String.Format("مانده مرخصی تا انتهای سال جاری {0} روز و {1} ساعت و {2} دقیقه", dayValue, hourValue, minValue);
                }
                else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                {
                    remain = String.Format("Remains off until the end of this Year, {0} days and {1} hours and {2} minutes", dayValue, hourValue, minValue);
                }
                result.Add(remain);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUserInformationLoadAccess_onPersonnelLoadStateInGridSchema()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUserInformationLoadAccess_onPersonnelLoadStateInGanttChartSchema()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUserInformationLoadAccess_onManagerLoadStateInGridSchema()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUserInformationLoadAccess_onManagerLoadStateInGanttChartSchema()
        { 
        }


    }
}
