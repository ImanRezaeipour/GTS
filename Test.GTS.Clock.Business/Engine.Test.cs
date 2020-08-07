using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.NHibernateFramework;
using System.Collections;
using GTS.Clock.Business.Engine;
using GTS.Clock.Model.AppSetting;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class EngineTest : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_ConceptTemplateTableAdapter CnpTmpTA = new DatabaseGatewayTableAdapters.TA_ConceptTemplateTableAdapter();
        DatabaseGatewayTableAdapters.TA_CalendarTableAdapter CalTA = new DatabaseGatewayTableAdapters.TA_CalendarTableAdapter();
        //TotalWebService.TotalWebServiceClient WS = new TotalWebService.TotalWebServiceClient();

        [SetUp]
        public void TestSetup()
        {
        }

        [TearDown]
        public void TreatDown()
        {
        }

        [Test] 
        public void LoadScndCnpTest()
        {
            EngineEnvironment cnpe = new EngineEnvironment();
            Assert.AreEqual(CnpTmpTA.GetData().Count, cnpe.ConceptList.Count);
        }

        [Test]
        public void LoadCalendarTest()
        {
            EngineEnvironment cnpe = new EngineEnvironment();
            Assert.AreEqual(CalTA.GetData().Count, cnpe.CalendarList.Count);
        }

        [Test]
        public void WSExecuteEngineTestByPrsId()
        {
            //WS.GTS_FillByPersonID(32687);
        }

        [Test]
        public void WSExecuteEngineTestByPrsBarcode()
        {
            //WS.GTS_FillByPersonBarCodeAndToDate("00002162", DateTime.Now.Date.ToShortDateString());

        }
    }

}
