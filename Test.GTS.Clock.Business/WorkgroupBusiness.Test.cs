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
using GTS.Clock.Business.Security;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class WorkgroupBusiness : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_PersonTableAdapter personTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonTableAdapter();
        DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter workgrpTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_WorkGroupDetailTableAdapter workgrpDtlTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupDetailTableAdapter();
        DatabaseGatewayTableAdapters.TA_ShiftTableAdapter shiftTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ShiftTableAdapter();
        DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter assignWorkGroupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_DataAccessWorkGroupTableAdapter dataAccessWorkGrpTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DataAccessWorkGroupTableAdapter();

        BWorkgroup busWorkgroup;
        WorkGroup ADOWorkGroup = new WorkGroup();
        WorkGroup ADOWorkGroup2 = new WorkGroup();
        Shift ADOShift = new Shift();
        Person ADOPerson = new Person();
        WorkGroup workgroup_testObject;


        [SetUp] 
        public void TestSetup()
        {
            workgroup_testObject = new WorkGroup();
            busWorkgroup = new BWorkgroup();

            personTA.InsertQuery("0000", "ali", true, null);
            int personId = Convert.ToInt32(personTA.GetDataByBarcode("0000")[0][0]);

            workgrpTA.Insert("WorkGroupTest", "0-0", 0);
            workgrpTA.Insert("WorkGroupTest1", "0-1", 0);

            DatabaseGateway.TA_WorkGroupDataTable table = new DatabaseGateway.TA_WorkGroupDataTable();
            workgrpTA.FillByName(table, "WorkGroupTest");
            ADOWorkGroup.ID = Convert.ToInt32(table.Rows[0]["workgroup_ID"]);
            ADOWorkGroup.Name = Convert.ToString(table.Rows[0]["workgroup_Name"]);
            ADOWorkGroup.CustomCode = Convert.ToString(table.Rows[0]["workgroup_CustomCode"]);

            shiftTA.Insert("ShiftTest", 1, 11, null, 100, false, false, false, "2", "0-0");

            DatabaseGateway.TA_ShiftDataTable shiftTable = new DatabaseGateway.TA_ShiftDataTable();
            shiftTA.FillByName(shiftTable, "ShiftTest");
            ADOShift.ID = Convert.ToInt32(shiftTable.Rows[0]["shift_ID"]);

            workgrpDtlTA.Insert(ADOWorkGroup.ID, ADOShift.ID, DateTime.Now);

            assignWorkGroupTA.Insert(ADOWorkGroup.ID, personId, DateTime.Now);
        }

        [TearDown]
        public void TreatDown()
        {
            shiftTA.DeleteByCustomCode("0-0");
            personTA.DeleteByBarcode("0000");
            workgrpTA.DeleteByCustomCode("0-0");
            workgrpTA.DeleteByCustomCode("0-1");
            dataAccessWorkGrpTA.DeleteByUserId(BUser.CurrentUser.ID);
        }

        [Test]
        public void GetByIDTest()
        {
            workgroup_testObject = busWorkgroup.GetByID(ADOWorkGroup.ID);
            Assert.IsNotNull(workgroup_testObject);
            Assert.IsTrue(workgroup_testObject.ID == ADOWorkGroup.ID);

        }

        [Test]
        public void GetAllTest()
        {
            dataAccessWorkGrpTA.Insert(BUser.CurrentUser.ID, null, true);
            IList<WorkGroup> list = busWorkgroup.GetAll();
            Assert.AreEqual(list.Count, workgrpTA.GetCount());
        }

        [Test]
        public void LoadWorkgroupDetailListTest()
        {
            workgroup_testObject = busWorkgroup.GetByID(ADOWorkGroup.ID);
            Assert.IsNotNull(workgroup_testObject.DetailList, "Detail List not null");
            Assert.IsTrue(workgroup_testObject.DetailList.Count == 1, "Loaded Shift Count");
        }

        [Test]
        public void Insert_EmptyNameTest()
        {
            try
            {
                workgroup_testObject._grpsCode = 1;
                workgroup_testObject.CustomCode = "0-0";
                busWorkgroup.SaveChanges(workgroup_testObject, UIActionType.ADD);
                Assert.Fail("نام نباید خالی باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey, ExceptionResourceKeys.WorkGroupNameRequierd);
            }
        }

        [Test]
        public void Insert_DublicateNameTest()
        {
            try
            {
                workgroup_testObject.Name = ADOWorkGroup.Name;
                workgroup_testObject.CustomCode = ADOWorkGroup.CustomCode;
                busWorkgroup.SaveChanges(workgroup_testObject, UIActionType.ADD);
                Assert.Fail("نام گروه کاری نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey, ExceptionResourceKeys.WorkGroupNameRepeated);
            }
        }

        [Test]
        public void Insert_DublicateCustomCodeTest()
        {
            try
            {
                workgroup_testObject.Name = ADOWorkGroup.Name + ADOWorkGroup.Name;
                workgroup_testObject.CustomCode = ADOWorkGroup.CustomCode;
                busWorkgroup.SaveChanges(workgroup_testObject, UIActionType.ADD);
                Assert.Fail("کد تعریف شده نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey, ExceptionResourceKeys.WorkGroupCustomCodeRepeated);
            }
        }

        [Test]
        public void Update_EmptyNameTest()
        {
            try
            {
                workgroup_testObject.ID = ADOWorkGroup.ID;
                workgroup_testObject._grpsCode = 1;
                workgroup_testObject.CustomCode = "0-0";
                busWorkgroup.SaveChanges(workgroup_testObject, UIActionType.EDIT);
                Assert.Fail("نام نباید خالی باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey, ExceptionResourceKeys.WorkGroupNameRequierd);
            }
        }

        [Test]
        public void Update_Test()
        {
            try
            {
                ADOWorkGroup.Name = ADOWorkGroup.Name + ADOWorkGroup.Name;
                
                busWorkgroup.SaveChanges(ADOWorkGroup, UIActionType.EDIT);
                ClearSession();
                workgroup_testObject = busWorkgroup.GetByID(ADOWorkGroup.ID);
                Assert.AreEqual(workgroup_testObject.Name, ADOWorkGroup.Name);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Delete_UsedByPersonTest() 
        {
            try 
            {
                busWorkgroup.SaveChanges(ADOWorkGroup, UIActionType.DELETE);
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.WorkGroupUsedByPerson);
            }
        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                personTA.DeleteByBarcode("0000");
                busWorkgroup.SaveChanges(ADOWorkGroup, UIActionType.DELETE);
                ClearSession();
                workgroup_testObject = busWorkgroup.GetByID(ADOWorkGroup.ID);
                Assert.Fail("آیتم حذف نشده است");
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass();
            }
        }


    }
}
