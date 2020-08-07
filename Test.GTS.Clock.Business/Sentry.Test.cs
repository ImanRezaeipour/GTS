using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Security;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class SentryTest : BaseFixture
    {
        #region Table Adapters
        DatabaseGatewayTableAdapters.TA_DataAccessDepartmentTableAdapter accessDepTA = new DatabaseGatewayTableAdapters.TA_DataAccessDepartmentTableAdapter();
        DatabaseGatewayTableAdapters.TA_DataAccessPrecardTableAdapter accessPrecardTA = new DatabaseGatewayTableAdapters.TA_DataAccessPrecardTableAdapter();
        DatabaseGatewayTableAdapters.TA_DataAccessCtrlStationTableAdapter accessCtrlStationTA = new DatabaseGatewayTableAdapters.TA_DataAccessCtrlStationTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter precardGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter precardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter accessGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupDetailTableAdapter accessGroupDtlTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupDetailTableAdapter();
        DatabaseGateway2TableAdapters.TA_PermitTableAdapter permitTA = new DatabaseGateway2TableAdapters.TA_PermitTableAdapter();
        DatabaseGateway2TableAdapters.TA_PermitPairTableAdapter permitPairTA = new DatabaseGateway2TableAdapters.TA_PermitPairTableAdapter();
        
        #endregion

        #region Permit ADOObjects
       
        PrecardGroups ADOPrecardGroup1 = new PrecardGroups();
        PrecardGroups ADOPrecardGroup2 = new PrecardGroups();
        PrecardGroups ADOPrecardGroup3 = new PrecardGroups();
        PrecardGroups ADOPrecardGroup4 = new PrecardGroups();
        PrecardGroups ADOPrecardGroup5 = new PrecardGroups();
        Precard ADOPrecardHourlyLeave1 = new Precard();
        Precard ADOPrecardHourlyLeave2 = new Precard();
        Precard ADOPrecardHourlyDuty1 = new Precard();
        Precard ADOPrecardHourlyEstelji1 = new Precard();
        Precard ADOPrecardTraffic1 = new Precard();
        Precard ADOPrecardDailyLeave1 = new Precard();
        Precard ADOPrecardDailyDuty1 = new Precard();
        Precard ADOPrecardOverTime1 = new Precard();      
        Permit ADOPermit1 = new Permit();
        Permit ADOPermit2 = new Permit();
        Permit ADOPermit3 = new Permit();
        PermitPair ADOPermitPair1 = new PermitPair();
        PermitPair ADOPermitPair2 = new PermitPair();
        PermitPair ADOPermitPair3 = new PermitPair();
        PermitPair ADOPermitPair4 = new PermitPair();
        PermitPair ADOPermitPair5 = new PermitPair();
        PermitPair ADOPermitPair6 = new PermitPair();
        #endregion

        BSentryPermits busSentry;


        [SetUp]
        public void TestSetup()
        {
            base.UpdateCurrentUserPersonId(ADOPerson1.ID);

            busSentry = new BSentryPermits();

            #region precards

            DatasetGatewayWorkFlow.TA_PrecardGroupsDataTable precardTable = new DatasetGatewayWorkFlow.TA_PrecardGroupsDataTable();
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.leave.ToString());
            ADOPrecardGroup1.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup1.Name = "HourlyLeave";
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.duty.ToString());
            ADOPrecardGroup2.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup2.Name = "HourlyDuty";
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.leaveestelajy.ToString());
            ADOPrecardGroup3.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup3.Name = "Estelaji";
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.traffic.ToString());
            ADOPrecardGroup4.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup4.Name = "Traffic";
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.overwork.ToString());
            ADOPrecardGroup5.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup5.Name = "OwerWork";


            precardTA.Insert("TestPrecard1", true, ADOPrecardGroup1.ID, true, false, true, "99999999", false);
            precardTA.Insert("TestPrecard4", true, ADOPrecardGroup1.ID, true, false, true, "99999999", false);
            precardTA.Insert("TestPrecard6", true, ADOPrecardGroup1.ID, false, true, true, "99999999", false);
            precardTA.Insert("TestPrecard7", true, ADOPrecardGroup2.ID, false, true, true, "99999999", false);
            precardTA.Insert("TestPrecard2", true, ADOPrecardGroup2.ID, true, false, true, "99999999", false);
            precardTA.Insert("TestPrecard3", true, ADOPrecardGroup3.ID, true, false, true, "99999999", false);
            precardTA.Insert("TestPrecard5", true, ADOPrecardGroup4.ID, true, false, true, "99999999", false);
            precardTA.Insert("TestPrecard8", true, ADOPrecardGroup5.ID, true, false, true, "99999999", false);

            DatasetGatewayWorkFlow.TA_PrecardDataTable pTable = new DatasetGatewayWorkFlow.TA_PrecardDataTable();
            pTable = precardTA.GetDataByName("TestPrecard1");
            ADOPrecardHourlyLeave1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardHourlyLeave1.Name = "TestPrecard1";
            pTable = precardTA.GetDataByName("TestPrecard2");
            ADOPrecardHourlyDuty1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardHourlyDuty1.Name = "TestPrecard2";
            pTable = precardTA.GetDataByName("TestPrecard3");
            ADOPrecardHourlyEstelji1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardHourlyEstelji1.Name = "TestPrecard3";
            pTable = precardTA.GetDataByName("TestPrecard4");
            ADOPrecardHourlyLeave2.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardHourlyLeave2.Name = "TestPrecard4";
            pTable = precardTA.GetDataByName("TestPrecard5");
            ADOPrecardTraffic1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardTraffic1.Name = "TestPrecard6";
            pTable = precardTA.GetDataByName("TestPrecard6");
            ADOPrecardDailyLeave1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardDailyLeave1.Name = "TestPrecard7";
            pTable = precardTA.GetDataByName("TestPrecard7");
            ADOPrecardDailyDuty1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardDailyDuty1.Name = "TestPrecard7";
            pTable = precardTA.GetDataByName("TestPrecard8");
            ADOPrecardOverTime1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardOverTime1.Name = "TestPrecard8";
            #endregion

            #region Data Access
            personTA.UpdateDepartmentId(ADODepartment2.ID, ADOPerson1.ID);
            personTA.UpdateDepartmentId(ADODepartment2.ID, ADOPerson2.ID);
            personTA.UpdateDepartmentId(ADODepartment1.ID, ADOPerson3.ID);
            personTA.UpdateDepartmentId(ADODepartment1.ID, ADOPerson4.ID);
            personTA.UpdateDepartmentId(ADODepartment2.ID, ADOPerson5.ID);
            personTA.UpdateDepartmentId(ADODepartment2.ID, ADOPerson6.ID);

            personTA.UpdateControlStation(ADOStaion1.ID, ADOPerson2.ID);

            accessDepTA.DeleteByUSerId(BUser.CurrentUser.ID);
            accessDepTA.Insert(BUser.CurrentUser.ID, ADODepartment1.ID, false);//ADOPerson3,4
            accessCtrlStationTA.Insert(BUser.CurrentUser.ID, ADOStaion1.ID, false);//ADOPerson2
            accessPrecardTA.Insert(BUser.CurrentUser.ID, ADOPrecardTraffic1.ID, false);
            accessPrecardTA.Insert(BUser.CurrentUser.ID, ADOPrecardDailyLeave1.ID, false);
            accessPrecardTA.Insert(BUser.CurrentUser.ID, ADOPrecardHourlyLeave2.ID, false);
            #endregion


            permitTA.Insert(ADOPerson2.ID, new DateTime(2012, 5, 1), new DateTime(2012, 5, 1), true);
            permitTA.Insert(ADOPerson2.ID, new DateTime(2012, 5, 2), new DateTime(2012, 5, 1), true);
            DatabaseGateway2.TA_PermitDataTable permitTable = permitTA.GetDataByPersonId(ADOPerson2.ID);
            ADOPermit1.ID = (permitTable.Rows[0] as DatabaseGateway2.TA_PermitRow).Permit_ID;
            ADOPermit2.ID = (permitTable.Rows[1] as DatabaseGateway2.TA_PermitRow).Permit_ID;

            /*ADOPrecardHourlyLeave1 -->*/
            permitPairTA.Insert(ADOPermit1.ID, null, ADOPrecardHourlyLeave1.ID, 100, 200, true, null, 0, false);
            /*ADOPrecardHourlyLeave2 -->*/
            permitPairTA.Insert(ADOPermit1.ID, null, ADOPrecardHourlyLeave2.ID, 10, 20, true, null, 0, false);
            /*ADOPrecardTraffic1 -->*/
            permitPairTA.Insert(ADOPermit1.ID, null, ADOPrecardTraffic1.ID, 500, 550, true, null, 0, false);
            /*ADOPrecardTraffic1 -->*/
            permitPairTA.Insert(ADOPermit2.ID, null, ADOPrecardTraffic1.ID, 500, 550, true, null, 0, false);

            DatabaseGateway2.TA_PermitPairDataTable permitPairTable = permitPairTA.GetDataByPermitId(ADOPermit1.ID);
            ADOPermitPair1.ID = (permitPairTable.Rows[0] as DatabaseGateway2.TA_PermitPairRow).PermitPair_ID;
            ADOPermitPair1.Permit = ADOPermit1;
            ADOPermitPair2.ID = (permitPairTable.Rows[1] as DatabaseGateway2.TA_PermitPairRow).PermitPair_ID;
            ADOPermitPair2.Permit = ADOPermit1;
            ADOPermitPair3.ID = (permitPairTable.Rows[2] as DatabaseGateway2.TA_PermitPairRow).PermitPair_ID;
            ADOPermitPair3.Permit = ADOPermit1;

            permitPairTable = permitPairTA.GetDataByPermitId(ADOPermit2.ID);
            ADOPermitPair4.ID = (permitPairTable.Rows[0] as DatabaseGateway2.TA_PermitPairRow).PermitPair_ID;
            ADOPermitPair4.Permit = ADOPermit2;

            permitTA.Insert(ADOPerson3.ID, new DateTime(2012, 5, 1), new DateTime(2012, 5, 1), true);
            permitTable = permitTA.GetDataByPersonId(ADOPerson3.ID);
            ADOPermit3.ID = (permitTable.Rows[0] as DatabaseGateway2.TA_PermitRow).Permit_ID;

            /*ADOPrecardDailyLeave1 -->*/
            permitPairTA.Insert(ADOPermit3.ID, null, ADOPrecardDailyLeave1.ID, null, null, true, null, 1, false);

            permitPairTable = permitPairTA.GetDataByPermitId(ADOPermit3.ID);
            ADOPermitPair5.ID = (permitPairTable.Rows[0] as DatabaseGateway2.TA_PermitPairRow).PermitPair_ID;
            ADOPermitPair5.Permit = ADOPermit3;

        }

        [TearDown]
        public void TreatDown()
        {
            try
            {
                permitTA.DeleteByPersonId(ADOPerson2.ID);
                permitTA.DeleteByPersonId(ADOPerson3.ID);
               
                userTA.DeleteByUsername("ADOPerson1");
                userTA.DeleteByUsername("TestUserName1");
                userTA.DeleteByUsername("TestUserName2");
                precardTA.DeleteByID("99999999");

            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        [Test]
        public void GetAllCountTest() 
        {
            int count = busSentry.GetPermitCount(RequestType.None, Utility.ToPersianDate(new DateTime(2012, 5, 1)));
            Assert.AreEqual(3, count);
        }

        [Test]
        public void GetAll_CountTest() 
        {
            IList<KartablProxy> list = busSentry.GetAllPermits(RequestType.None, Utility.ToPersianDate(new DateTime(2012, 5, 1)), 0, 10, SentryPermitsOrderBy.PersonCode);
            int count = busSentry.GetPermitCount(RequestType.None, Utility.ToPersianDate(new DateTime(2012, 5, 1)));
            Assert.AreEqual(count, list.Count);
        }

        [Test]
        public void GetAll_NotAllowedPrecardTest()
        {
            IList<KartablProxy> list = busSentry.GetAllPermits(RequestType.None, Utility.ToPersianDate(new DateTime(2012, 5, 1)), 0, 10, SentryPermitsOrderBy.PersonCode);

            Assert.IsTrue(list.Where(x => x.ID == ADOPermitPair1.ID).Count() == 0);           
        }

        [Test]
        public void GetAll_AllowedPrecardTest()
        {
            IList<KartablProxy> list = busSentry.GetAllPermits(RequestType.None, Utility.ToPersianDate(new DateTime(2012, 5, 1)), 0, 10, SentryPermitsOrderBy.PersonCode);

            Assert.IsTrue(list.Where(x => x.ID == ADOPermitPair2.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.ID == ADOPermitPair3.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.ID == ADOPermitPair5.ID).Count() == 1);

        }

        [Test]
        public void Filter_AllowedHourlyPrecardTest()
        {
            IList<KartablProxy> list = busSentry.GetAllPermits(RequestType.Hourly, Utility.ToPersianDate(new DateTime(2012, 5, 1)), 0, 10, SentryPermitsOrderBy.PersonCode);

            Assert.IsTrue(list.Where(x => x.ID == ADOPermitPair2.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.ID == ADOPermitPair3.ID).Count() == 1);
            Assert.IsFalse(list.Where(x => x.ID == ADOPermitPair5.ID).Count() == 1);

        }

        [Test]
        public void Filter_AllowedDailyPrecardTest()
        {
            IList<KartablProxy> list = busSentry.GetAllPermits(RequestType.Daily, Utility.ToPersianDate(new DateTime(2012, 5, 1)), 0, 10, SentryPermitsOrderBy.PersonCode);

            Assert.IsFalse(list.Where(x => x.ID == ADOPermitPair2.ID).Count() == 1);
            Assert.IsFalse(list.Where(x => x.ID == ADOPermitPair3.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.ID == ADOPermitPair5.ID).Count() == 1);

        }

        [Test]
        public void GetAll_Test1() 
        {
            IList<KartablProxy> list = busSentry.GetAllPermits(RequestType.None, Utility.ToPersianDate(new DateTime(2012, 5, 2)), 0, 10, SentryPermitsOrderBy.PersonCode);

            Assert.IsTrue(list.Where(x => x.ID == ADOPermitPair4.ID).Count() == 1);
        }

        [Test]
        public void GetAll_OrderBy_PersonName_Test() 
        {
            IList<KartablProxy> list = busSentry.GetAllPermits(RequestType.None, Utility.ToPersianDate(new DateTime(2012, 5, 1)), 0, 10, SentryPermitsOrderBy.PersonName);
            Assert.AreEqual(ADOPerson2.PersonCode, list[0].Barcode);
        }

        [Test]
        public void GetAll_OrderBy_PersonCode_Test()
        {
            IList<KartablProxy> list = busSentry.GetAllPermits(RequestType.None, Utility.ToPersianDate(new DateTime(2012, 5, 1)), 0, 10, SentryPermitsOrderBy.PersonCode);
            Assert.AreEqual(ADOPerson2.PersonCode, list[0].Barcode);
        }

        [Test]
        public void GetAll_OrderBy_PermitSubject_Test()
        {
            IList<KartablProxy> list = busSentry.GetAllPermits(RequestType.None, Utility.ToPersianDate(new DateTime(2012, 5, 1)), 0, 10, SentryPermitsOrderBy.PermitSubject);
            Assert.AreEqual(ADOPerson2.PersonCode, list[0].Barcode);
        }

        [Test]
        public void Search_AllowedPrecardTest()
        {
            IList<KartablProxy> list = busSentry.GetAllPermits("03", Utility.ToPersianDate(new DateTime(2012, 5, 1)), 0, 10, SentryPermitsOrderBy.PersonCode);
           
            Assert.IsTrue(list.Where(x => x.ID == ADOPermitPair5.ID).Count() == 1);
        }

        [Test]
        public void Search_CountTest()
        {
            IList<KartablProxy> list = busSentry.GetAllPermits("03", Utility.ToPersianDate(new DateTime(2012, 5, 1)), 0, 10, SentryPermitsOrderBy.PersonCode);
            int count = busSentry.GetPermitCount("03", Utility.ToPersianDate(new DateTime(2012, 5, 1)));
            Assert.AreEqual(count, list.Count);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void Test222222222()
        {
            IList<KartablProxy> list = busSentry.GetAllPermits(RequestType.None, Utility.ToPersianDate(new DateTime(2012, 5, 1)), 0, 10, SentryPermitsOrderBy.PermitSubject);

          
            
        }
  
    }
}
