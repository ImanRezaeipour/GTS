using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model;
using GTS.Clock.Business.WorkFlow;

namespace GTS.Clock.Business.RequestFlow
{
    public class BSentryPermits : MarshalByRefObject
    {
        IDataAccess accessPort = new BUser();
        PersonRepository prsRep = new PersonRepository(false);
        BPermit busPermit = new BPermit();
        ISearchPerson searchTool = new BPerson();

        public int GetPermitCount(RequestType requestType, string theDate)
        {
            try
            {
                return this.GetPermitCount(null, requestType, theDate);
            }
            catch (Exception ex)
            {

                BaseBusiness<Entity>.LogException(ex, "BSentryPermits", "GetPermitCount");
                throw ex;
            }
        }

        public int GetPermitCount(string searchKey, string theDate)
        {
            try
            {
                return this.GetPermitCount(searchKey, RequestType.None, theDate);
            }
            catch (Exception ex)
            {

                BaseBusiness<Entity>.LogException(ex, "BSentryPermits", "GetPermitCount");
                throw ex;
            }
        }

        private int GetPermitCount(string searchKey,RequestType requestType, string theDate)
        {
            try
            {
                DateTime date;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(theDate);
                }
                else
                {
                    date = Utility.ToMildiDateTime(theDate);
                }

                IList<decimal> controlStationIds = accessPort.GetAccessibleControlStations();
                List<decimal> prsIds = new List<decimal>();
                //prsIds.AddRange(accessPort.GetAccessiblePersonByDepartment());
                IList<decimal> precardIds = accessPort.GetAccessiblePrecards();
                //prsIds.AddRange(prsRep.GetAllPersonByControlStaion(controlStationIds.ToArray()));

                if (searchKey != null)
                {
                    IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch(searchKey, PersonCategory.Sentry_UnderManagment);
                    var ids = from o in quciSearchInUnderManagment
                              select o.ID;
                    prsIds = ids.ToList<decimal>();
                }
                else 
                {
                    IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch("", PersonCategory.Sentry_UnderManagment);
                    var ids = from o in quciSearchInUnderManagment
                              select o.ID;
                    prsIds = ids.Take<decimal>(2000).ToList<decimal>();
                }

                int count = busPermit.GetExistingPermitCount(prsIds, precardIds, date);
                return count;
            }
            catch (Exception ex)
            {

                BaseBusiness<Entity>.LogException(ex, "BSentryPermits", "GetPermitCount");
                throw ex;
            }
        }

        public IList<KartablProxy> GetAllPermits(RequestType requestType, string theDate, int pageIndex, int pageSize, SentryPermitsOrderBy orderby)
        {
            try
            {
                return this.GetAllPermits(null, requestType, theDate, pageIndex, pageSize, orderby);
            }
            catch (Exception ex) 
            {
                BaseBusiness<Entity>.LogException(ex, "BSentryPermits", "GetAllPermits");
                throw ex;
            }           
        }

        public IList<KartablProxy> GetAllPermits(string searchKey, string theDate, int pageIndex, int pageSize, SentryPermitsOrderBy orderby)
        {
            try
            {
                return this.GetAllPermits(searchKey, RequestType.None, theDate, pageIndex, pageSize, orderby);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BSentryPermits", "GetAllPermits");
                throw ex;
            }
        }

        private IList<KartablProxy> GetAllPermits(string searchKey, RequestType requestType, string theDate, int pageIndex, int pageSize, SentryPermitsOrderBy orderby)
        {
            try
            {
                DateTime date;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(theDate);
                }
                else
                {
                    date = Utility.ToMildiDateTime(theDate);
                }

                IList<decimal> controlStationIds = accessPort.GetAccessibleControlStations();
                List<decimal> prsIds = new List<decimal>();
                IList<decimal> precardIds = accessPort.GetAccessiblePrecards();
                
                if (searchKey != null)
                {
                    IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch(searchKey, PersonCategory.Sentry_UnderManagment);
                    var ids = from o in quciSearchInUnderManagment
                              select o.ID;
                    prsIds.AddRange(ids.ToList<decimal>());
                }
                else
                {
                    IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch("", PersonCategory.Sentry_UnderManagment);
                    var ids = from o in quciSearchInUnderManagment
                              select o.ID;
                    prsIds = ids.ToList<decimal>();
                }

                IList<Permit> result = busPermit.GetExistingPermit(prsIds, precardIds, date, orderby, pageIndex, pageSize);

                IList<Permit> permitList = new List<Permit>();


                var permits = from o in result
                              group o by o.ID;


                foreach (var permit in permits)
                {
                    foreach (Permit item in permit)
                    {
                        permitList.Add(item);
                        break;
                    }
                }

                IList<KartablProxy> kartablResult = new List<KartablProxy>();
                int counter = 0;
                foreach (Permit permit in permitList)
                {
                    IList<PermitPair> permitPairs = permit.Pairs;
                    permitPairs = permitPairs.Where(x => precardIds.Contains(x.PreCardID)).ToList();
                    switch (requestType)
                    {
                        case RequestType.Daily:
                            permitPairs = permitPairs.Where(x => x.Precard.IsDaily).ToList();
                            break;
                        case RequestType.Hourly:
                            permitPairs = permitPairs.Where(x => x.Precard.IsHourly).ToList();
                            break;
                    }
                    foreach (PermitPair permitPair in permitPairs)
                    {
                        counter++;
                        KartablProxy proxy = new KartablProxy();
                        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                        {
                            proxy.TheFromDate = Utility.ToPersianDate(permit.FromDate);
                            proxy.TheToDate = Utility.ToPersianDate(permit.ToDate);
                        }
                        else
                        {
                            proxy.TheFromDate = Utility.ToString(permit.FromDate);
                            proxy.TheToDate = Utility.ToString(permit.ToDate);
                        }
                        proxy.ID = permitPair.ID;
                        proxy.RequestID = permitPair.ID; 
                        proxy.TheFromTime = Utility.IntTimeToRealTime(permitPair.From);
                        proxy.TheToTime = Utility.IntTimeToRealTime(permitPair.To);
                        proxy.Row = counter;
                        proxy.RequestTitle = permitPair.Precard.Name;
                        proxy.Applicant = permit.Person.Name;
                        proxy.PersonImage = permit.Person.PersonDetail.Image;
                        proxy.Barcode = permit.Person.PersonCode;
                        proxy.PersonId = permit.Person.ID;
                        string name = permitPair.Precard.PrecardGroup.LookupKey;
                        PrecardGroupsName groupName = (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), name);
                        if (groupName == PrecardGroupsName.overwork)
                        {
                            proxy.RequestType = RequestType.OverWork;
                        }
                        else if (permitPair.Precard.IsHourly)
                        {
                            proxy.RequestType = RequestType.Hourly;
                        }
                        else if (permitPair.Precard.IsDaily)
                        {
                            proxy.RequestType = RequestType.Daily;
                        }
                        else
                        {
                            proxy.RequestType = RequestType.None;
                        }

                        kartablResult.Add(proxy);
                    }
                }
                return kartablResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSentryLoadAccess()
        { 
        }

    }
}
