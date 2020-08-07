using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business;
using GTS.Clock.Business.Security;

  
namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 2012-01-25 11:13:00 AM
    /// write your name here
    /// </summary>
    [TestFixture]
    public class BUserInfoTest : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_LeaveCalcResultTableAdapter LCRTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_LeaveCalcResultTableAdapter();
        GTS.Clock.Business.Leave.BUserInfo busUserInfo;

        [SetUp]
        public void TestSetup()
        {
            //LCRTA.Insert(DateTime.Now.Date, 1, 1, ADOPerson1.ID, 1, 1, 0, 0, 0);

        }

        [TearDown]
        public void TreatDown()
        {
        }

        [Test]
        public void Call_Test()
        { 
            busUserInfo = new GTS.Clock.Business.Leave.BUserInfo();
            IList<string> list = busUserInfo.GetUserInfo(ADOPerson1.ID);
            Assert.IsTrue(list.Count > 0);
        }

    }
}
