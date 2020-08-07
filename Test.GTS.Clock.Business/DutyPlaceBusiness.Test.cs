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
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.RequestFlow;

namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 2011-12-14 10:55:37 AM
    /// write your name here
    /// </summary>
    [TestFixture]
    public class DutyPlaceBusinessTest : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_DutyPlaceTableAdapter dutyPlaceTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DutyPlaceTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_RequestTableAdapter requestTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_RequestTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_RequestDetailTableAdapter requestDetailTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_RequestDetailTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter precardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter precardGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter();

        BDutyPlace busDutyPlace;
        DutyPlace dutyPlace_testObject;
        DutyPlace ADODutyPlaceRoot = new DutyPlace();
        DutyPlace ADODutyPlace1 = new DutyPlace();
        DutyPlace ADODutyPlace2 = new DutyPlace();
        PrecardGroups ADOPrecardGroup1 = new PrecardGroups();
        Precard ADOPrecar1 = new Precard();
        Request ADORequest1 = new Request();

        [SetUp]
        public void TestSetup()
        {
            dutyPlace_testObject = new DutyPlace();
            busDutyPlace = new BDutyPlace();

            DatabaseGateway.TA_DutyPlaceDataTable dutyTable = dutyPlaceTA.GetRoot();
            DatabaseGateway.TA_DutyPlaceRow dutyPlaceRow;
            if (dutyTable.Rows.Count == 0) 
            {
                dutyPlaceTA.Insert("TestDutyPlaceRoot", "0-00Test", 0);
                dutyTable = dutyPlaceTA.GetRoot();
            }
            dutyPlaceRow=dutyTable.Rows[0] as DatabaseGateway.TA_DutyPlaceRow;
            ADODutyPlaceRoot.ID = dutyPlaceRow.dutyPlc_ID;
            ADODutyPlaceRoot.CustomCode = dutyPlaceRow.dutyPlc_CustomCode;
            ADODutyPlaceRoot.Name = dutyPlaceRow.dutyPlc_Name;
            ADODutyPlaceRoot.ParentID = 0;

            dutyPlaceTA.Insert("TestDutyPlace1", "0-00Test1", ADODutyPlaceRoot.ID);
            dutyTable = dutyPlaceTA.GetDataByCustomCode("0-00Test1");
            dutyPlaceRow = dutyTable.Rows[0] as DatabaseGateway.TA_DutyPlaceRow;
            ADODutyPlace1.ID = dutyPlaceRow.dutyPlc_ID;
            ADODutyPlace1.Name = dutyPlaceRow.dutyPlc_Name;
            ADODutyPlace1.CustomCode = dutyPlaceRow.dutyPlc_CustomCode;
            ADODutyPlace1.ParentID = dutyPlaceRow.dutyPlc_ParentID;

            dutyPlaceTA.Insert("TestDutyPlace2", "0-00Test2", ADODutyPlaceRoot.ID);
            dutyTable = dutyPlaceTA.GetDataByCustomCode("0-00Test2");
            dutyPlaceRow = dutyTable.Rows[0] as DatabaseGateway.TA_DutyPlaceRow;
            ADODutyPlace2.ID = dutyPlaceRow.dutyPlc_ID;
            ADODutyPlace2.Name = dutyPlaceRow.dutyPlc_Name;
            ADODutyPlace2.CustomCode = dutyPlaceRow.dutyPlc_CustomCode;
            ADODutyPlace2.ParentID = dutyPlaceRow.dutyPlc_ParentID;

            DatasetGatewayWorkFlow.TA_PrecardGroupsDataTable precardTable = new DatasetGatewayWorkFlow.TA_PrecardGroupsDataTable();
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.leave.ToString());
            ADOPrecardGroup1.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup1.Name = "HourlyLeave";

            precardTA.Insert("TestPrecard1", true, ADOPrecardGroup1.ID, true, false, true, "99999999", false);

            DatasetGatewayWorkFlow.TA_PrecardDataTable pTable = new DatasetGatewayWorkFlow.TA_PrecardDataTable();
            pTable = precardTA.GetDataByName("TestPrecard1");
            ADOPrecar1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecar1.Name = "TestPrecard1";

            requestTA.Insert(ADOPrecar1.ID, ADOPerson1.ID, new DateTime(2010, 5, 1), new DateTime(2010, 5, 1), 420, 600, "", DateTime.Now, ADOUser1.ID);

            DatasetGatewayWorkFlow.TA_RequestDataTable requestTable = new DatasetGatewayWorkFlow.TA_RequestDataTable();
            requestTable = requestTA.GetDataByPersonId(ADOPerson1.ID);
            ADORequest1.ID = Convert.ToInt32(requestTable.Rows[0][0]);

            requestDetailTA.Insert(ADORequest1.ID, null, null, ADODutyPlace2.ID);
        }

        [TearDown]
        public void TreatDown()
        {
            requestTA.DeleteByPerson(ADOPerson1.ID);
            precardTA.DeleteByID("99999999");
            dutyPlaceTA.DeleteByCustomCode("0-00Test");
            dutyPlaceTA.DeleteByCustomCode("0-00Test1");
            dutyPlaceTA.DeleteByCustomCode("0-00Test2");
            dutyPlaceTA.DeleteByCustomCode("0-00Test3");
        }

        [Test]
        public void GetById_Test()
        {
            dutyPlace_testObject= busDutyPlace.GetByID(ADODutyPlace1.ID);
            Assert.AreEqual(ADODutyPlace1.Name, dutyPlace_testObject.Name);

        }

        [Test]
        public void GetTree_Test() 
        {
            dutyPlace_testObject = busDutyPlace.GetDutyPalcesTree();
            Assert.AreEqual(dutyPlace_testObject.ID, ADODutyPlaceRoot.ID);
        }

        [Test]
        public void GetDutyPlaceChild_Test() 
        {
            IList<DutyPlace> list = busDutyPlace.GetDutyPlaceChilds(ADODutyPlaceRoot.ID);
            Assert.IsTrue(list.Where(x => x.ID == ADODutyPlace1.ID).Count() == 1);
            Assert.IsTrue(list.Where(x => x.ID == ADODutyPlace2.ID).Count() == 1);
        }

        [Test]
        public void Insert_Test()
        {
            try
            {
                dutyPlace_testObject.Name = "TestDutyPlace3";
                dutyPlace_testObject.CustomCode = "0-00Test3";
                dutyPlace_testObject.ParentID = ADODutyPlace1.ID;

                busDutyPlace.SaveChanges(dutyPlace_testObject, UIActionType.ADD);
                Assert.IsTrue(dutyPlace_testObject.ID > 0);
            }
            catch (Exception ex) 
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_EmptyName_CustomCodeRepeatedTest() 
        {
            try
            {
                dutyPlace_testObject.CustomCode = ADODutyPlace1.CustomCode;
                busDutyPlace.SaveChanges(dutyPlace_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.DutyPlaceNameRequierd));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.DutyPlaceCustomCodeRepeated));                
            }
        }

        [Test]
        public void Insert_RepeatedName()
        {
            try
            {
                dutyPlace_testObject.Name = ADODutyPlace1.Name;
                busDutyPlace.SaveChanges(dutyPlace_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.DutyPlaceNameRepeated));
            }
        }

        [Test]
        public void Insert_ParentIdEmptyTest() 
        {
            try
            {
                dutyPlace_testObject.CustomCode = ADODutyPlace1.CustomCode;
                busDutyPlace.SaveChanges(dutyPlace_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.DutyPlaceParentRequest));
            }
        }

        [Test]
        public void Update_Test()
        {
            try 
            {
                ADODutyPlace1.Name = "TestDutyDuty";
                busDutyPlace.SaveChanges(ADODutyPlace1, UIActionType.EDIT);
                ClearSession();
                dutyPlace_testObject = busDutyPlace.GetByID(ADODutyPlace1.ID);
                Assert.AreEqual("TestDutyDuty", dutyPlace_testObject.Name);
                Assert.AreEqual(ADODutyPlace1.CustomCode, dutyPlace_testObject.CustomCode);
            }
            catch (Exception ex) 
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Update_EmptyName_CustomCodeRepeatedTest()
        {
            try
            {
                dutyPlace_testObject.ID = ADODutyPlace1.ID;                
                dutyPlace_testObject.CustomCode = ADODutyPlace2.CustomCode;
                busDutyPlace.SaveChanges(dutyPlace_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.DutyPlaceNameRequierd));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.DutyPlaceCustomCodeRepeated));
            }
        }      

        [Test]
        public void Update_RepeatedName()
        {
            try
            {
                dutyPlace_testObject.ID = ADODutyPlace1.ID;
                dutyPlace_testObject.Name = ADODutyPlace2.Name;
                busDutyPlace.SaveChanges(dutyPlace_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.DutyPlaceNameRepeated));
            }
        }

        [Test]
        public void Update_Root() 
        {
            try
            {
                busDutyPlace.SaveChanges(ADODutyPlaceRoot, UIActionType.EDIT);
            }
            catch (Exception ex) 
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                busDutyPlace.SaveChanges(ADODutyPlace1, UIActionType.DELETE);
                ClearSession();
                dutyPlace_testObject = busDutyPlace.GetByID(ADODutyPlace1.ID);
                Assert.Fail();
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void Delete_UsedByRequestTest()
        {
            try
            {
                busDutyPlace.SaveChanges(ADODutyPlace2, UIActionType.DELETE);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.DutyPlaceUsedByRequest));
            }
        }
    }
}
