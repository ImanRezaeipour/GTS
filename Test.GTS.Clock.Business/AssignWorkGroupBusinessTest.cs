using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business.Assignments;
using GTS.Clock.Infrastructure.Utility;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class AssignWorkGroupBusinessTest : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter assingTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter workGroupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter();

        BAssignWorkGroup bussAssign;
        WorkGroup ADOworkGroup1 = new WorkGroup();
        AssignWorkGroup ADOAssign = new AssignWorkGroup();
        AssignWorkGroup assign_testObject;

        [SetUp]
        public void TestSetup()
        {
            assign_testObject = new AssignWorkGroup();
            bussAssign = new BAssignWorkGroup(SysLanguageResource.Parsi);
           
            DatabaseGateway.TA_PersonDataTable personTable = personTA.GetDataByBarcode("00001");
            ADOPerson1.ID = Convert.ToInt32(personTable.Rows[0]["prs_ID"]);
            ADOPerson1.BarCode = Convert.ToString(personTable.Rows[0]["prs_Barcode"]);

            workGroupTA.Insert("WorkGroup1", "0-0", null);
            DatabaseGateway.TA_WorkGroupDataTable workGroupTAble = new DatabaseGateway.TA_WorkGroupDataTable();
            workGroupTA.FillByName(workGroupTAble, "WorkGroup1");

            ADOworkGroup1.ID = Convert.ToDecimal(workGroupTAble.Rows[0]["WorkGroup_ID"]);
            ADOworkGroup1.Name = Convert.ToString(workGroupTAble.Rows[0]["WorkGroup_Name"]);
            ADOworkGroup1.CustomCode = Convert.ToString(workGroupTAble.Rows[0]["WorkGroup_CustomCode"]);

            assingTA.Insert(ADOworkGroup1.ID, ADOPerson1.ID, new DateTime(2010, 2, 14));
            assingTA.Insert(ADOworkGroup1.ID, ADOPerson1.ID, new DateTime(2005, 5, 14));
            assingTA.Insert(ADOworkGroup1.ID, ADOPerson1.ID, new DateTime(2007, 11, 5));
            assingTA.Insert(ADOworkGroup1.ID, ADOPerson1.ID, new DateTime(2010, 9, 14));
            assingTA.Insert(ADOworkGroup1.ID, ADOPerson1.ID, new DateTime(2012, 9, 14));
            assingTA.Insert(ADOworkGroup1.ID, ADOPerson1.ID, new DateTime(2008, 3, 1));
            DatabaseGateway.TA_AssignWorkGroupDataTable table=new DatabaseGateway.TA_AssignWorkGroupDataTable();
            assingTA.FillByFilter(table, ADOworkGroup1.ID, ADOPerson1.ID);

            ADOAssign.ID = (decimal)table.Rows[0]["AsgWorkGroup_ID"];
            ADOAssign.FromDate = (DateTime)table.Rows[0]["AsgWorkGroup_FromDate"];

        }

        [TearDown]
        public void TreatDown()
        {
            workGroupTA.DeleteByCustomCode(ADOworkGroup1.CustomCode);
        }

        [Test]
        public void Update_ValidatePersonTest()
        {
            try
            {
                assign_testObject.ID = ADOAssign.ID;
                assign_testObject.UIFromDate = Utility.ToPersianDate(DateTime.Now);
                bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignWorkGroupPersonIdNotExsits));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignWorkGroupIdNotExsits));
            }
        }

        [Test]
        public void Update_Test() 
        {
            assign_testObject.ID = ADOAssign.ID;
            assign_testObject.UIFromDate = Utility.ToPersianDate(DateTime.Now);
            assign_testObject.Person = ADOPerson1;
            assign_testObject.WorkGroup = ADOworkGroup1;
            bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);

            ClearSession();
            assign_testObject = new AssignWorkGroup();
            assign_testObject = bussAssign.GetByID(ADOAssign.ID);
            Assert.AreEqual(assign_testObject.FromDate, DateTime.Now.Date);
        }

        [Test]
        public void Update_ValidateSmallDateTest1()
        {
            try
            {
                assign_testObject.ID = ADOAssign.ID;
                bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(assign_testObject.FromDate, Utility.GTSMinStandardDateTime);
            }
        }

        [Test]
        public void Update_ValidateSmallDateTest2()
        {
            try
            {
                assign_testObject.ID = ADOAssign.ID;
                assign_testObject.UIFromDate = Utility.ToPersianDate(new DateTime(1800, 1, 1));
                bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignWorkGroupSmallerThanStandardValue));
            }
        }

        [Test]
        public void Insert_ValidatePersonTest()
        {
            try
            {
                assign_testObject.UIFromDate = Utility.ToPersianDate(DateTime.Now);
                bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignWorkGroupPersonIdNotExsits));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignWorkGroupIdNotExsits));
            }
        }

        [Test]
        public void Insert_Test()
        {
            assign_testObject.UIFromDate = Utility.ToPersianDate(DateTime.Now);
            assign_testObject.Person = ADOPerson1;
            assign_testObject.WorkGroup = ADOworkGroup1;
            bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);

            ClearSession();
            assign_testObject = bussAssign.GetByID(assign_testObject.ID);
            Assert.IsTrue(assign_testObject.ID > 0);
        }

        [Test]
        public void Insert_ValidateSmallDateTest1()
        {
            try
            {
                bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(assign_testObject.FromDate, Utility.GTSMinStandardDateTime);
            }
        }

        [Test]
        public void Insert_ValidateSmallDateTest2()
        {
            try
            {
                assign_testObject.UIFromDate = Utility.ToPersianDate(new DateTime(1800, 1, 1));
                bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignWorkGroupSmallerThanStandardValue));
            }
        }

        [Test]
        public void Insert_ValidateSmallEnglishDateTest1()
        {
            try
            {
                bussAssign = new BAssignWorkGroup(SysLanguageResource.English);
                bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(assign_testObject.FromDate, Utility.GTSMinStandardDateTime);
            }
        }

        [Test]
        public void Insert_ValidateSmallEnglishDateTest2()
        {
            try
            {
                bussAssign = new BAssignWorkGroup(SysLanguageResource.English);
                assign_testObject.UIFromDate = Utility.ToString(new DateTime(1800, 1, 1));
                bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignWorkGroupSmallerThanStandardValue));
            }
        }

        [Test]
        public void Insert_UIFromDateTest() 
        {
            //assign_testObject.UIFromDate = "2/5/1390";//Utility.ToPersianDate(DateTime.Now.Date);
            assign_testObject.UIFromDate = PersianDateTime.MiladiToShamsi(DateTime.Now.ToShortDateString());
            assign_testObject.Person = ADOPerson1;
            assign_testObject.WorkGroup = ADOworkGroup1;
            bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);

            ClearSession();
            assign_testObject = bussAssign.GetByID(assign_testObject.ID);

            Assert.AreEqual(assign_testObject.FromDate.Date.ToShortDateString(), DateTime.Now.ToShortDateString());
        }

        [Test]
        public void Insert_ByCreateWorkingPersonTest()
        {
            BPerson personBus = new BPerson(SysLanguageResource.Parsi, LocalLanguageResource.Parsi);
            decimal id = personBus.CreateWorkingPerson2();           

            assign_testObject.UIFromDate = Utility.ToPersianDate(DateTime.Now);
            assign_testObject.Person = new Person() { ID = id };
            assign_testObject.WorkGroup = new WorkGroup() { ID = ADOworkGroup1.ID };
            bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);

            ClearSession();
            assign_testObject = bussAssign.GetByID(assign_testObject.ID);
            Assert.IsTrue(assign_testObject.ID > 0);
        }

        [Test]
        public void Insert_UIFromDateEnglishTest()
        {
            BAssignWorkGroup bassign = new BAssignWorkGroup(SysLanguageResource.English);
            assign_testObject.UIFromDate = "2010/4/05";
            assign_testObject.Person = ADOPerson1;
            assign_testObject.WorkGroup = ADOworkGroup1;
            bassign.SaveChanges(assign_testObject, UIActionType.ADD);

            ClearSession();
            assign_testObject = bassign.GetByID(assign_testObject.ID);

            Assert.AreEqual(assign_testObject.FromDate.Date, new DateTime(2010, 4, 5));
        }

        [Test]
        public void Delete_Test() 
        {
            try
            {
                bussAssign.SaveChanges(ADOAssign, UIActionType.DELETE);
                assign_testObject = bussAssign.GetByID(ADOAssign.ID);
                Assert.IsTrue(ADOAssign.ID == 0);
            }
            catch (ItemNotExists ex) 
            {
                Assert.Pass(ex.Message);
            }
        }
    }
}
