using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.Engine
{
    public class BEngineCalculator : MarshalByRefObject
    {
        private GTSEngineWS.TotalWebServiceClient gtsEngineWS = new GTS.Clock.Business.GTSEngineWS.TotalWebServiceClient();


        public bool Calculate(decimal personId, string fromDate, string toDate, bool forceCalculate)
        {
            try
            {
                DateTime from, to;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    from = Utility.ToMildiDate(fromDate);
                    to = Utility.ToMildiDate(toDate);
                }
                else
                {
                    from = Utility.ToMildiDateTime(fromDate);
                    to = Utility.ToMildiDateTime(toDate);
                }
                decimal[] ids = new decimal[1];
                ids[0] = personId;
                if (forceCalculate)
                {
                    BusinessEntity entity = new BusinessEntity(); 
                    entity.UpdateCFP(new List<Person>() { new Person() { ID = personId } }, from, true);
                }
                gtsEngineWS.GTS_ExecutePersonsByToDate(BUser.CurrentUser.UserName, ids, to);
                BaseBusiness<Entity>.LogUserAction(String.Format("CalculateAll -> personId: {0} -->Calculate(personId,toDate)", personId));
                return true;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BEngineCalculator", "Calculate(personId,toDate)");
                throw ex;
            }
        }

        public bool Calculate(PersonAdvanceSearchProxy proxy, string fromDate ,string toDate, bool forceCalculate)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
                IList<Person> personList = searchTool.GetPersonInAdvanceSearch(proxy, 0, count)
                    .Where(x => x.Active).ToList();
                var ids = from o in personList
                          select o.ID;

                DateTime from, to;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    from = Utility.ToMildiDate(fromDate);
                    to = Utility.ToMildiDate(toDate);
                }
                else
                {
                    from = Utility.ToMildiDateTime(fromDate);
                    to = Utility.ToMildiDateTime(toDate);
                }
                if (forceCalculate)
                {
                    BusinessEntity entity = new BusinessEntity();
                    entity.UpdateCFP(personList, from, true);
                }
                gtsEngineWS.GTS_ExecutePersonsByToDate(BUser.CurrentUser.UserName, ids.ToArray<decimal>(), to);
                BaseBusiness<Entity>.LogUserAction(String.Format("CalculateAll -> Count: {0} -->Calculate(AdvanceSearch,toDate)", personList.Count));
                return true;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BEngineCalculator", "Calculate(AdvanceSearch,toDate)");
                throw ex;
            }
        }

        public bool Calculate(string searchKey, string fromDate, string toDate, bool forceCalculate)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> personList = searchTool.QuickSearch(searchKey, PersonCategory.Public)
                    .Where(x => x.Active).ToList();
                var ids = from o in personList
                          select o.ID;
                DateTime from, to;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    from = Utility.ToMildiDate(fromDate);
                    to = Utility.ToMildiDate(toDate);
                }
                else
                {
                    from = Utility.ToMildiDateTime(fromDate);
                    to = Utility.ToMildiDateTime(toDate);
                }
                if (forceCalculate)
                {
                    BusinessEntity entity = new BusinessEntity();
                    entity.UpdateCFP(personList, from, true);
                }
                gtsEngineWS.GTS_ExecutePersonsByToDate(BUser.CurrentUser.UserName, ids.ToArray<decimal>(), to);
                BaseBusiness<Entity>.LogUserAction(String.Format("CalculateAll -> searchKey: {0} And Count: {1} -->Calculate(searchKey, toDate)", searchKey, personList.Count));
                return true;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BEngineCalculator", "Calculate(searchKey, toDate)");
                throw ex;
            }
        }

        public bool Calculate(string fromDate,string toDate, bool forceCalculate)
        {
            try
            {
                return this.Calculate(String.Empty, fromDate, toDate, forceCalculate);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BEngineCalculator", "Calculate(toDate)");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد افرادی که برای محاسبه فرستاده شده و نیاز به محاسبه داشتند
        /// </summary>
        /// <returns></returns>
        public int GetTotalCountInCalculating()
        {
            try
            {
                int total = 0;
                if (SessionHelper.HasSessionValue(SessionHelper.BusinessTotalCalculateCount))
                {
                    total = Utility.ToInteger(SessionHelper.GetSessionValue(SessionHelper.BusinessTotalCalculateCount));
                }
                int newTotal = gtsEngineWS.GTS_GETTotalExecuting(BUser.CurrentUser.UserName);
                if (newTotal > total)
                {
                    SessionHelper.SaveSessionValue(SessionHelper.BusinessTotalCalculateCount, newTotal);
                    return newTotal;
                }
                return total;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BEngineCalculator", "GetTotalCountInCalculating");
                throw ex;
            }
        }

        public void ClearTotalCountInCalculating()
        {
            SessionHelper.ClearSessionValue(SessionHelper.BusinessTotalCalculateCount);
        }

        /// <summary>
        /// تعداد افراد باقی مانده برای محاسبه
        /// </summary>
        /// <returns></returns>
        public int GetRemainCountInCalculating()
        {
            try
            {
                return gtsEngineWS.GTS_GETRemainExecuting(BUser.CurrentUser.UserName);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BEngineCalculator", "GetRemainCountInCalculating");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckCalculationsLoadAccess()
        {

        }
    }
}
