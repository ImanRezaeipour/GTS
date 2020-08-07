using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.UI;
using GTS.Clock.Model.Security;
using GTS.Clock.Business;
using GTS.Clock.Business.BoxService;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.BoxService;
using GTS.Clock.Infrastructure;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class MainPageServiceBoxBusinessTest : BaseFixture
    {
        #region Table Adapters
        DatabaseGatewayTableAdapters.TA_PersonTableAdapter personTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonTableAdapter();
        DatabaseGatewayTableAdapters.TA_PublicMessageTableAdapter publicTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PublicMessageTableAdapter();
        DatabaseGatewayTableAdapters.TA_KartablSummaryTableAdapter kartablSmyTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_KartablSummaryTableAdapter();
        #endregion

        #region ADOObjects
        PublicMessage publicMessage_testObject = new PublicMessage();
        BMainPageBox busMessage = new BMainPageBox();
        PublicMessage ADOMessage1 = new PublicMessage();
        PublicMessage ADOMessage2 = new PublicMessage();
        PublicMessage ADOMessage3 = new PublicMessage();

        KartablSummary ADOKartablSmy1 = new KartablSummary();
        KartablSummary ADOKartablSmy2 = new KartablSummary();
        KartablSummary ADOKartablSmy3 = new KartablSummary();
        #endregion

        [SetUp]
        public void TestSetup()
        {
            DatabaseGateway.TA_PublicMessageDataTable publicTable = new DatabaseGateway.TA_PublicMessageDataTable();
            publicTA.Insert(ADOPerson1.ID, true, DateTime.Now, "Message Test1", DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1));
            publicTable = publicTA.GetDataByMesage("Message Test1");
            ADOMessage1.ID = Convert.ToDecimal(publicTable.Rows[0]["pblMsg_ID"]);

            publicTA.Insert(ADOPerson1.ID, true, DateTime.Now, "Message Test2", DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            publicTable = publicTA.GetDataByMesage("Message Test2");
            ADOMessage2.ID = Convert.ToDecimal(publicTable.Rows[0]["pblMsg_ID"]);

            publicTA.Insert(ADOPerson1.ID, false, DateTime.Now, "Message Test3", DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1));
            publicTable = publicTA.GetDataByMesage("Message Test3");
            ADOMessage3.ID = Convert.ToDecimal(publicTable.Rows[0]["pblMsg_ID"]);

            DatabaseGateway.TA_KartablSummaryDataTable kartablTable = new DatabaseGateway.TA_KartablSummaryDataTable();
            kartablTable = kartablSmyTA.GetDataByKey(KartablSummaryItems.ConfirmedRequestCount.ToString());
            if (kartablTable.Rows.Count == 0)
            {
                kartablSmyTA.Insert(KartablSummaryItems.ConfirmedRequestCount.ToString(), "تعداد درخواستهای تایید شده", "Confirmed Request Count", 1, true);
                kartablTable = kartablSmyTA.GetDataByKey(KartablSummaryItems.ConfirmedRequestCount.ToString());
            }
            ADOKartablSmy1.ID = Convert.ToDecimal(kartablTable.Rows[0]["kartablsmry_ID"]);

            kartablTable = kartablSmyTA.GetDataByKey(KartablSummaryItems.NotConfirmedRequestCount.ToString());
            if (kartablTable.Rows.Count == 0)
            {
                kartablSmyTA.Insert(KartablSummaryItems.NotConfirmedRequestCount.ToString(), "تعداد درخواستهای تایید نشده", "Not Confirmed Request Count", 1, true);                
                kartablTable = kartablSmyTA.GetDataByKey(KartablSummaryItems.NotConfirmedRequestCount.ToString());
            }
            ADOKartablSmy2.ID = Convert.ToDecimal(kartablTable.Rows[0]["kartablsmry_ID"]);

            kartablTable = kartablSmyTA.GetDataByKey(KartablSummaryItems.InFlowRequestCount.ToString());
            if (kartablTable.Rows.Count == 0)
            {
                kartablSmyTA.Insert(KartablSummaryItems.InFlowRequestCount.ToString(), "تعداد درخواستهای درحال بررسی", "UnderReview Request Count", 1, true);
                kartablTable = kartablSmyTA.GetDataByKey(KartablSummaryItems.InFlowRequestCount.ToString());
            }
            ADOKartablSmy3.ID = Convert.ToDecimal(kartablTable.Rows[0]["kartablsmry_ID"]);

            
        }

        [TearDown]
        public void TreatDown()
        {
            publicTA.DeleteByMessage("Message Test1");
            publicTA.DeleteByMessage("Message Test2");
            publicTA.DeleteByMessage("Message Test3");
        }

        [Test]
        public void GetPublicMessage_Test()
        {
            IList<PublicMessage> list = busMessage.GetPublicMessages();
            Assert.IsTrue(list.Where(x => x.ID == ADOMessage1.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.ID == ADOMessage2.ID).Count() == 1);
            Assert.IsFalse(list.Where(x => x.ID == ADOMessage3.ID).Count() == 1);
        }

        [Test]
        public void GetKartablSummary_Test() 
        {
            IList<KartablSummary> list = busMessage.GetKartablSummary();
            Assert.IsTrue(list.Where(x => x.ID == ADOKartablSmy1.ID).Count() > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOKartablSmy2.ID).Count() > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOKartablSmy3.ID).Count() > 0);
        }

        [Test]
        public void Test2222222() 
        {
            IList<KartablSummary> list = busMessage.GetKartablSummary();
        }
    }
}
