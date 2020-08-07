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
using GTS.Clock.Business.Presentaion_Helper.Proxy;


namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 4/11/2012 11:00:04 AM
    /// write your name here
    /// </summary>
    [TestFixture]
    public class BasicTrafficTest : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_BaseTrafficTableAdapter basicTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_BaseTrafficTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter precardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter groupPrecardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter();

        Precard ADOPrecard1 = new Precard();
        Precard ADOPrecard2 = new Precard();
        PrecardGroups ADOGroup = new PrecardGroups();
        BasicTraffic basic_testObject;
        BasicTraffic ADOBasic1 = new BasicTraffic();
        BTraffic busTraffic = new BTraffic();
        [SetUp]
        public void TestSetup()
        {
            busTraffic = new BTraffic();
            basic_testObject = new BasicTraffic();

            groupPrecardTA.Insert("TestPrecardGroup", "TestGroup1");
            DatasetGatewayWorkFlow.TA_PrecardGroupsDataTable groupTable;
            groupTable = groupPrecardTA.GetDataByName("TestGroup1");
            ADOGroup.ID = Convert.ToDecimal(groupTable.Rows[0][0]);

            precardTA.Insert("TestPish1", true, ADOGroup.ID, true, false, false, "1001", false);

            DatasetGatewayWorkFlow.TA_PrecardDataTable precardTable = new DatasetGatewayWorkFlow.TA_PrecardDataTable();
            precardTable = precardTA.GetDataByName("TestPish1");
            ADOPrecard1.ID = Convert.ToDecimal(precardTable.Rows[0][0]);

            basicTA.InsertQuery(ADOPrecard1.ID, ADOPerson1.ID, DateTime.Now, 100, false, true);
            DatabaseGateway.TA_BaseTrafficDataTable baseTable = basicTA.GetDataByPerson(ADOPerson1.ID);
            ADOBasic1.ID = Convert.ToDecimal(baseTable.Rows[0][0]);


        }

        [TearDown]
        public void TreatDown()
        {
            basicTA.DeleteByyPerson(ADOPerson1.ID);
            precardTA.DeleteByID("1001");
            groupPrecardTA.DeleteByName("TestPrecardGroup");
        }

        [Test]
        public void GetById_Test()
        {
            basic_testObject = busTraffic.GetByID(ADOBasic1.ID);
            Assert.AreEqual(ADOBasic1.ID, basic_testObject.ID);
        }

        [Test]
        public void Insert_Test()
        {
            decimal id = busTraffic.InsertTraffic(ADOPerson1.ID, ADOPrecard1.ID, Utility.ToPersianDate(DateTime.Now.AddDays(1)), "08:00", "");
            ClearSession();
            basic_testObject = busTraffic.GetByID(id);
            Assert.AreEqual(480, basic_testObject.Time);
        }       

        [Test]
        public void Delete_Test()
        {
            busTraffic.DeleteTraffic(ADOBasic1.ID);
            ClearSession();
            basic_testObject = busTraffic.GetByID(ADOBasic1.ID);
            Assert.IsFalse(basic_testObject.Active);
        }

        [Test]
        public void Test_2222() 
        {
  
        }
    }
}
