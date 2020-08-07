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
    public class AssignDateRangeBusinessTest : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_PersonRangeAssignmentTableAdapter assingTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonRangeAssignmentTableAdapter();
        DatabaseGatewayTableAdapters.TA_CalculationRangeGroupTableAdapter groupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_CalculationRangeGroupTableAdapter();

        BAssignDateRange bussAssign;
        CalculationRangeGroup ADOGroup1 = new CalculationRangeGroup();
        PersonRangeAssignment ADOAssign = new PersonRangeAssignment();
        PersonRangeAssignment assign_testObject;

        [SetUp]
        public void TestSetup()
        {
            assign_testObject = new PersonRangeAssignment();
            bussAssign = new BAssignDateRange(SysLanguageResource.Parsi);         

            groupTA.Insert("RangeGroup1", "",1);
            DatabaseGateway.TA_CalculationRangeGroupDataTable groupTable = new DatabaseGateway.TA_CalculationRangeGroupDataTable();
            groupTA.FillByGroupName(groupTable, "RangeGroup1");

            ADOGroup1.ID = Convert.ToDecimal(groupTable.Rows[0]["CalcRangeGroup_ID"]);
            ADOGroup1.Name = Convert.ToString(groupTable.Rows[0]["CalcRangeGroup_Name"]);
            ADOGroup1.Description = Convert.ToString(groupTable.Rows[0]["CalcRangeGroup_Des"]);

            assingTA.Insert(ADOPerson1.ID, ADOGroup1.ID, new DateTime(2010, 2, 14));
            assingTA.Insert(ADOPerson1.ID, ADOGroup1.ID, new DateTime(2005, 5, 14));
            assingTA.Insert(ADOPerson1.ID, ADOGroup1.ID, new DateTime(2007, 11, 5));
            assingTA.Insert(ADOPerson1.ID, ADOGroup1.ID, new DateTime(2010, 9, 14));
            assingTA.Insert(ADOPerson1.ID, ADOGroup1.ID, new DateTime(2012, 9, 14));
            assingTA.Insert(ADOPerson1.ID, ADOGroup1.ID, new DateTime(2008, 3, 1));
            
            DatabaseGateway.TA_PersonRangeAssignmentDataTable table = new DatabaseGateway.TA_PersonRangeAssignmentDataTable();
            assingTA.FillByFilter(table, ADOPerson1.ID, ADOGroup1.ID);

            ADOAssign.ID = (decimal)table.Rows[0]["PrsRangeAsg_ID"];
            ADOAssign.FromDate = (DateTime)table.Rows[0]["PrsRangeAsg_FromDate"];

        }

        [TearDown]
        public void TreatDown()
        {
            groupTA.DeleteByName(ADOGroup1.Name);
        }

        [Test]
        public void Update_ValidatePersonTest()
        {
            try
            {
                assign_testObject.ID = ADOAssign.ID;
                assign_testObject.FromDate = DateTime.Now.Date;
                bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRangePersonIdNotExsits));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRangeGroupIdNotExsits));
            }
        }

        [Test]
        public void Update_ValidateSmallDateTest1()
        {
            try
            {
                assign_testObject.ID = ADOAssign.ID;
                assign_testObject.UIFromDate = Utility.ToPersianDate(Utility.GTSMinStandardDateTime.AddYears(-1));
                bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRangeSmallerThanStandardValue));
            }
        }      

        [Test]
        public void Update_Test()
        {
            assign_testObject.ID = ADOAssign.ID;
            assign_testObject.UIFromDate = Utility.ToPersianDate(DateTime.Now);
            assign_testObject.Person = ADOPerson1;
            assign_testObject.CalcDateRangeGroup = ADOGroup1;
            bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);

            ClearSession();
            assign_testObject = new PersonRangeAssignment();
            assign_testObject = bussAssign.GetByID(ADOAssign.ID);
            Assert.AreEqual(assign_testObject.FromDate, DateTime.Now.Date);
        }

        [Test]
        public void Insert_ValidatePersonTest()
        {
            try
            {
                assign_testObject.FromDate = DateTime.Now.Date;
                bussAssign.SaveChanges(assign_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRangePersonIdNotExsits));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRangeGroupIdNotExsits));
            }
        }

        [Test]
        public void Insert_Test()
        {
            assign_testObject.UIFromDate = Utility.ToPersianDate(DateTime.Now.Date);
            assign_testObject.Person = ADOPerson1;
            assign_testObject.CalcDateRangeGroup = ADOGroup1;
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
                assign_testObject.UIFromDate = Utility.ToPersianDate(new DateTime(1800, 5, 5));
                bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRangeSmallerThanStandardValue));
            }
        }

        [Test]
        public void Insert_ValidateSmallDateTest2()
        {
            try
            {
                bussAssign = new BAssignDateRange(SysLanguageResource.English);
                assign_testObject.UIFromDate = Utility.ToString(new DateTime(1800, 1, 1));
                bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRangeSmallerThanStandardValue));
            }
        }

        [Test]
        public void Insert_UIFromDateTest()
        {
            assign_testObject.UIFromDate = "1390/4/5";
            assign_testObject.Person = ADOPerson1;
            assign_testObject.CalcDateRangeGroup = ADOGroup1;
            bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);

            ClearSession();
            assign_testObject = bussAssign.GetByID(assign_testObject.ID);

            Assert.AreEqual(assign_testObject.FromDate.Date, Utility.ToMildiDate("1390/04/05"));
        }

        [Test]
        public void Insert_UIFromDateEnglishTest()
        {
            BAssignDateRange bassign = new BAssignDateRange(SysLanguageResource.English);
            assign_testObject.UIFromDate = "2010/4/05";
            assign_testObject.Person = ADOPerson1;
            assign_testObject.CalcDateRangeGroup = ADOGroup1;
            bassign.SaveChanges(assign_testObject, UIActionType.ADD);

            ClearSession();
            assign_testObject = bassign.GetByID(assign_testObject.ID);

            Assert.AreEqual(assign_testObject.FromDate.Date, new DateTime(2010, 4, 5));
        }

        [Test]
        public void Insert_FirstStartOfYear_Fail_Test() 
        {
            try
            {
                assign_testObject.UIFromDate = "1391/02/01";
                assign_testObject.Person = ADOPerson2;
                assign_testObject.CalcDateRangeGroup = ADOGroup1;
                bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AssignRangeFirstMustBeFromStartYear));
            }
        }

        [Test]
        public void Insert_FirstStartOfYear_Pass_Test()
        {
            try
            {
                assign_testObject.UIFromDate = "1391/01/01";
                assign_testObject.Person = ADOPerson2;
                assign_testObject.CalcDateRangeGroup = ADOGroup1;
                bussAssign.SaveChanges(assign_testObject, UIActionType.ADD);
               
                ClearSession();
                assign_testObject = bussAssign.GetByID(assign_testObject.ID);
                Assert.IsTrue(assign_testObject.ID > 0);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
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
