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
using GTS.Clock.Model.UI;
using GTS.Clock.Model.Security;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.BoxService;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.BaseInformation;

namespace GTS.Clock.Business.BoxService
{
    /// <summary>
    /// created at: 2011-11-24 2:18:29 PM
    /// write your name here
    /// </summary>
    public class BMainPageBox 
    {
        const string ExceptionSrc = "GTS.Clock.Business.BoxService.BMainPageBox";
        EntityRepository<PublicMessage> pblMesgRep = new EntityRepository<PublicMessage>();
        EntityRepository<KartablSummary> kartablRep = new EntityRepository<KartablSummary>();

        /// <summary>
        /// پیغامهای عمومی را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<PublicMessage> GetPublicMessages()
        {
            try
            {
                IList<PublicMessage> list = pblMesgRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PublicMessage().Active), true))
                                                                     .OrderBy(x => x.Date).ToList();

                return list;             
            }
            catch (Exception ex) 
            {
                BaseBusiness<PublicMessage>.LogException(ex);
                throw ex;
            }
        }

        public IList<KartablSummary> GetKartablSummary() 
        {
            try
            {
                BKartabl bKartabl = new BKartabl();                
                IList<KartablSummary> list = kartablRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new KartablSummary().Active), true))
                                                                     .OrderBy(x => x.Order).ToList();
                IDashboardBRequest busRequest = new BRequest();
                int year = 0, month = 0;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    year = new PersianDateTime(DateTime.Now).Year;
                    month = new PersianDateTime(DateTime.Now).Month;
                }
                else
                {
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                }
                foreach (KartablSummary ks in list) 
                {
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        ks.Title = ks.FnTitle;
                    }
                    else 
                    {
                        ks.Title = ks.EnTitle;
                    }
                    KartablSummaryItems item = (KartablSummaryItems)Enum.Parse(typeof(KartablSummaryItems), ks.Key);

                    switch (item) 
                    {
                        case KartablSummaryItems.ConfirmedRequestCount:
                            int count = busRequest.GetAllRequestCount(year, month, RequestState.Confirmed);
                            ks.Value = count.ToString();
                            break;
                        case KartablSummaryItems.NotConfirmedRequestCount:
                            count = busRequest.GetAllRequestCount(year, month, RequestState.Unconfirmed);
                            ks.Value = count.ToString();
                            break;
                        case KartablSummaryItems.MainRecievedRequestCount:
                            count = bKartabl.GetManagerKartablRequestCount(year);
                            ks.Value = count == -1 ? "" : count.ToString();
                            break;
                        case KartablSummaryItems.SubstituteRecievedRequestCount:
                             count = bKartabl.GetSubstituteKartablRequestCount(year);
                            ks.Value = count == -1 ? "" : count.ToString();
                            break;
                        case KartablSummaryItems.InFlowRequestCount:
                            count = busRequest.GetAllRequestCount(year, RequestState.UnderReview);
                            ks.Value = count.ToString();
                            break;
                        case  KartablSummaryItems.PrivateMessageCount:
                            BPrivateMessage busMsg = new BPrivateMessage();
                            ks.Value = Utility.ToString(busMsg.GetAllUnReadRecievedMessagesCount());
                            break;
                    }
                }
                
                return list;
            }
            catch (Exception ex)
            {
                BaseBusiness<PublicMessage>.LogException(ex);
                throw ex;
            }
        }
    }
}
