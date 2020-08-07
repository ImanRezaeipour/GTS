using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Charts;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Business.Proxy;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class HelptTest : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_HelpTableAdapter helpTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_HelpTableAdapter();
        BHelp busHlp = new BHelp();

        [SetUp]
        public void TestSetup()
        {
             
        }

        [TearDown]
        public void TreatDown()
        {        
        }


        [Test]
        [ExpectedException(typeof(InvalidDatabaseStateException))]
        public void GetHelpByFormKey_ExceptionTest()
        {
            busHlp.GetHelpByFormKey("ThisIsATest");
        }

        [Test]
        public void GetHelpByFormKey()
        {
            HelpProxy hlp = busHlp.GetHelpByFormKey("tlbItemHelp_TlbShift");
            Assert.IsNotNull(hlp);
        }

        [Test]
        public void GetRoot_Test() 
        {
            HelpProxy proxy = busHlp.GetHelpRoot();
            Assert.IsTrue(proxy.ID > 0);
        }

        [Test]
        public void GetChild_Test() 
        {
            IList<HelpProxy> proxyList = busHlp.GetHelpChilds(busHlp.GetHelpRoot().ID);
            Assert.IsTrue(proxyList.Count > 0);
        }

       

        [Test]
        public void test_22222() 
        {
            IList<HelpProxy> l= busHlp.GetHelpChilds(5);
        }
    }
}
