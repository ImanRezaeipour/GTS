using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.Assignments;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business.Rules;
using BusinessProxy = GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model.UIValidation;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class PersonBusinessTest:BaseFixture
    {
        #region variables
        DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter workgrpTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter assignWorkGroupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_PersonRangeAssignmentTableAdapter assinTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonRangeAssignmentTableAdapter();
        DatabaseGatewayTableAdapters.TA_CalculationRangeGroupTableAdapter groupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_CalculationRangeGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_RuleCategoryTableAdapter ruleCatTA = new DatabaseGatewayTableAdapters.TA_RuleCategoryTableAdapter();
        DatabaseGatewayTableAdapters.TA_RuleCategoryPartTableAdapter ruleCAtPart = new DatabaseGatewayTableAdapters.TA_RuleCategoryPartTableAdapter(); 
        DatabaseGatewayTableAdapters.TA_PersonRuleCategoryAssignmentTableAdapter prsRleCatAsgTA = new DatabaseGatewayTableAdapters.TA_PersonRuleCategoryAssignmentTableAdapter();
        DatabaseGatewayTableAdapters.QueriesTableAdapter queris = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.QueriesTableAdapter();
        DatabaseGatewayTableAdapters.TA_UIValidationGroupTableAdapter validationGroupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationGroupTableAdapter();

        BPerson busPerson;
        ISearchPerson searchTool;
        WorkGroup ADOWorkGroup1 = new WorkGroup();
        WorkGroup ADOWorkGroup2 = new WorkGroup();
        RuleCategory ADORuleCat1 = new RuleCategory();
        RuleCategory ADORuleCat2 = new RuleCategory();
        CalculationRangeGroup ADOGroup1 = new CalculationRangeGroup();
        CalculationRangeGroup ADOGroup2 = new CalculationRangeGroup();
        Person person_testObject;
        UIValidationGroup ADOUIValidationGroup1 = new UIValidationGroup();
        #endregion

        [SetUp]
        public void TestSetup()
        {
            return;
            busPerson = new BPerson(SysLanguageResource.English, LocalLanguageResource.English);
            searchTool = new BPerson();
            person_testObject = new Person();            

            workgrpTA.Insert("WorkGroupTest1", "0-0", 0);
            workgrpTA.Insert("WorkGroupTest2", "0-1", 0);
            DatabaseGateway.TA_WorkGroupDataTable table = new DatabaseGateway.TA_WorkGroupDataTable();
            workgrpTA.FillByName(table, "WorkGroupTest1");
            ADOWorkGroup1.ID = Convert.ToInt32(table.Rows[0]["workgroup_ID"]);
            ADOWorkGroup1.Name = Convert.ToString(table.Rows[0]["workgroup_Name"]);
            ADOWorkGroup1.CustomCode = Convert.ToString(table.Rows[0]["workgroup_CustomCode"]);

            workgrpTA.FillByName(table, "WorkGroupTest2");
            ADOWorkGroup2.ID = Convert.ToInt32(table.Rows[0]["workgroup_ID"]);
            ADOWorkGroup2.Name = Convert.ToString(table.Rows[0]["workgroup_Name"]);
            ADOWorkGroup2.CustomCode = Convert.ToString(table.Rows[0]["workgroup_CustomCode"]);

            assignWorkGroupTA.Insert(ADOWorkGroup1.ID, ADOPerson2.ID,new DateTime(2007,4,5));
            assignWorkGroupTA.Insert(ADOWorkGroup1.ID, ADOPerson2.ID, new DateTime(2012, 5, 5));
            assignWorkGroupTA.Insert(ADOWorkGroup2.ID, ADOPerson2.ID, new DateTime(2010, 11, 6));         
            assignWorkGroupTA.Insert(ADOWorkGroup1.ID, ADOPerson2.ID, new DateTime(2009, 6, 15));
            assignWorkGroupTA.Insert(ADOWorkGroup1.ID, ADOPerson2.ID, new DateTime(2010, 8, 5));

            groupTA.Insert("RangeGroup1", "",1);
            groupTA.Insert("RangeGroup2", "",1);
            DatabaseGateway.TA_CalculationRangeGroupDataTable groupTable = new DatabaseGateway.TA_CalculationRangeGroupDataTable();
            groupTA.FillByGroupName(groupTable, "RangeGroup1");

            ADOGroup1.ID = Convert.ToDecimal(groupTable.Rows[0]["CalcRangeGroup_ID"]);
            ADOGroup1.Name = Convert.ToString(groupTable.Rows[0]["CalcRangeGroup_Name"]);
            ADOGroup1.Description = Convert.ToString(groupTable.Rows[0]["CalcRangeGroup_Des"]);

            groupTA.FillByGroupName(groupTable, "RangeGroup2");

            ADOGroup2.ID = Convert.ToDecimal(groupTable.Rows[0]["CalcRangeGroup_ID"]);
            ADOGroup2.Name = Convert.ToString(groupTable.Rows[0]["CalcRangeGroup_Name"]);
            ADOGroup2.Description = Convert.ToString(groupTable.Rows[0]["CalcRangeGroup_Des"]);

            assinTA.Insert(ADOPerson2.ID, ADOGroup1.ID, new DateTime(2010, 2, 14));
            assinTA.Insert(ADOPerson2.ID, ADOGroup1.ID, new DateTime(2005,5,14));
            assinTA.Insert(ADOPerson2.ID, ADOGroup1.ID, new DateTime(2007, 11, 5));
            assinTA.Insert(ADOPerson2.ID, ADOGroup2.ID, new DateTime(2010, 9, 14));
            assinTA.Insert(ADOPerson2.ID, ADOGroup1.ID, new DateTime(2012, 9, 14));
            assinTA.Insert(ADOPerson2.ID, ADOGroup1.ID, new DateTime(2008, 3, 1));

            ruleCatTA.Insert("RuleGroupTest1", "0000", false, "00-00test1");
            ruleCatTA.Insert("RuleGroupTest2", "0000", false, "00-00test1");

            DatabaseGateway.TA_RuleCategoryDataTable ruleTable = ruleCatTA.GetDataByName("RuleGroupTest1");
            ADORuleCat1.ID = (Decimal)ruleTable[0]["RuleCat_ID"];
            ADORuleCat1.Name = (String)ruleTable[0]["RuleCat_Name"];

            ruleTable = ruleCatTA.GetDataByName("RuleGroupTest2");
            ADORuleCat2.ID = (Decimal)ruleTable[0]["RuleCat_ID"];
            ADORuleCat2.Name = (String)ruleTable[0]["RuleCat_Name"];

            prsRleCatAsgTA.Insert(ADOPerson2.ID, ADORuleCat1.ID, "2005/05/15", "2007/05/08", null);
            prsRleCatAsgTA.Insert(ADOPerson2.ID, ADORuleCat1.ID, "2008/01/01", "2010/01/01", null);
            prsRleCatAsgTA.Insert(ADOPerson2.ID, ADORuleCat1.ID, "2010/01/02", "2010/12/01", null);
            prsRleCatAsgTA.Insert(ADOPerson2.ID, ADORuleCat1.ID, "2010/12/02", "2011/03/01", null);
            prsRleCatAsgTA.Insert(ADOPerson2.ID, ADORuleCat2.ID, "2011/03/02", "2015/03/01", null);

            validationGroupTA.InsertQuery("TestGroup00");
            DatabaseGateway.TA_UIValidationGroupDataTable uiValGroupTable = validationGroupTA.GetDataByName("TestGroup00");
            ADOUIValidationGroup1.ID = (uiValGroupTable.Rows[0] as DatabaseGateway.TA_UIValidationGroupRow).UIValGrp_ID;

        }

        [TearDown]
        public void TreatDown()
        {
            workgrpTA.DeleteByCustomCode("0-0");
            workgrpTA.DeleteByCustomCode("0-1");
            groupTA.DeleteByName("RangeGroup1");
            groupTA.DeleteByName("RangeGroup2");

            ruleCAtPart.DeleteByRuleCategory("00-00test1");
            prsRleCatAsgTA.DeleteByRuleCategory("00-00test1");
            ruleCatTA.DeleteByCustomCode("00-00test1");

            ruleCAtPart.DeleteByRuleCategory("00-00test2");
            prsRleCatAsgTA.DeleteByRuleCategory("00-00test2");
            ruleCatTA.DeleteByCustomCode("00-00test2");
            validationGroupTA.DeleteByName("TestGroup00");
           
            sataionTA.DeleteByCustomCode("0-1Test");
        }

        [Test]
        public void Insert_AccessDeniedTest() 
        {
            try
            {
                busPerson.SaveChanges(person_testObject, UIActionType.ADD);
                Assert.Fail("Access Denied");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is IllegalServiceAccess);
            }
        }

        #region Update Test
        
        [Test]
        public void Update_EmptyValidation()
        {
            try
            {
                person_testObject.ID = ADOPerson1.ID;
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonNameRequied), "نام خالی");
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonLastNameRequierd), "نام خانوادگی خالی");
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonMarriedRequierd), "تاهل");
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonBarcodeRequierd), "بارکد");
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonEmploymenttypeRequierd), "نوع استخدام");
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonDepartmentRequierd), "بخش");
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonWorkGroupRequierd), "گروه کاری");
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonRuleGroupRequierd), "گروه قوانین");
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonDateRangeRequierd), "رینج محاسبات");

            }
        }

        [Test]
        public void Update_RepeatValidation()
        {
            try
            {
                person_testObject.ID = ADOPerson1.ID;
                person_testObject.BarCode = ADOPerson1.BarCode;
                person_testObject.CardNum = ADOPerson1.CardNum;
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonNameRequied), "بارکد تکراری");
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonLastNameRequierd), "شماره کارت تکراری");

            }
        }

        [Test]
        public void Update_MelicodeNumberValidate()
        {
            person_testObject.PersonDetail = new PersonDetail();
            person_testObject.ID = ADOPerson1.ID;

            try
            {
                person_testObject.PersonDetail.MeliCode = "";
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.PersonMeliCodeInValid));
            }

            try
            {
                person_testObject.PersonDetail.MeliCode = "12344433a3";
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                Assert.Fail("کد پرسنلی باید عددی باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonMeliCodeInValid));
            }

            try
            {
                person_testObject.PersonDetail.MeliCode = "88994454";
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.PersonMeliCodeInValid));
            }
        }

        [Test]
        public void Update_CartNumValidate()
        {
            person_testObject.PersonDetail = new PersonDetail();
            person_testObject.ID = ADOPerson1.ID;

            try
            {
                person_testObject.CardNum = "";
                person_testObject.BarCode = "2155";
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(person_testObject.CardNum, person_testObject.BarCode);
            }

            try
            {
                person_testObject.CardNum = "12344-4333";
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                Assert.Fail("شماره کارت باید عددی باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonCartNumInValid));
            }

            try
            {
                person_testObject.CardNum = "88994454";
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.PersonCartNumInValid));
            }
        }

        [Test]
        public void Update_ShenasnameAndBarCodeValidate()
        {
            person_testObject.PersonDetail = new PersonDetail();
            person_testObject.ID = ADOPerson1.ID;

            try
            {
                person_testObject.PersonDetail.ShomareShenasname = "1312312-34";
                person_testObject.BarCode = "2155 5";
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonShenasnameCodeInValid));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonBarcodeInValid));
            }
            try
            {
                person_testObject.PersonDetail.ShomareShenasname = "131231234";
                person_testObject.BarCode = "21555";
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.PersonShenasnameCodeInValid));
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.PersonBarcodeInValid));
            }
        }

        [Test]
        public void Update_EmploymentDateValidate()
        {
            try
            {
                person_testObject.ID = ADOPerson1.ID;
                person_testObject.EmploymentDate = DateTime.Now;
                Assert.AreEqual(person_testObject.EndEmploymentDate.Year, 1900, "تاریخ پایان اگر مقداردهی نشود میتواند تهی باشد");
                person_testObject.EmploymentDate = DateTime.Now.AddYears(-1);
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.PersonEmploymentFromDateGreaterThanToDate));
            }
        }

        [Test]
        public void Update_InsertParsi_Assign_Barcode_Test()
        {
            try
            {
                person_testObject = this.GetReadyForUpdate(0, SysLanguageResource.Parsi, LocalLanguageResource.Parsi);
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                ClearSession();
                decimal personId = person_testObject.ID;
                person_testObject = new Person();
                person_testObject = busPerson.GetByID(personId);
                Assert.AreEqual(person_testObject.PersonCode, "00001222");
                Assert.IsNotNull(person_testObject.PersonRangeAssignList);
                Assert.IsTrue(person_testObject.PersonRangeAssignList.Count > 0);
                Assert.AreEqual(busPerson.GetCurrentActiveWorkGroup(person_testObject.ID), "WorkGroupTest3");
                Assert.AreEqual(busPerson.GetCurrentActiveRuleGroup(person_testObject.ID), "");
                PersonRangeAssignment PRAsg = busPerson.GetCurrentRangeAssignment(person_testObject.ID);
                Assert.AreEqual(PRAsg.CalcDateRangeGroup.Name, "CalcGroup3");

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                queris.DeletePersonTest();
            }
        }

        [Test]
        public void Update_InsertParsiDateTest()
        {
            try
            {
                person_testObject = this.GetReadyForUpdate(0,SysLanguageResource.Parsi, LocalLanguageResource.Parsi);
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                ClearSession();
                decimal personId = person_testObject.ID;
                person_testObject = new Person();
                person_testObject = busPerson.GetByID(personId);

                Assert.AreEqual(person_testObject.PersonDetail.UIBirthDate, "1390/05/03");
                Assert.AreEqual(person_testObject.UIEmploymentDate, "1380/05/03");
                Assert.AreEqual(person_testObject.UIEndEmploymentDate, "1390/05/03");

                Assert.AreEqual(person_testObject.PersonDetail.BirthDate, Utility.ToMildiDate(person_testObject.PersonDetail.UIBirthDate));
                Assert.AreEqual(person_testObject.EmploymentDate, Utility.ToMildiDate(person_testObject.UIEmploymentDate));
                Assert.AreEqual(person_testObject.EndEmploymentDate, Utility.ToMildiDate(person_testObject.UIEndEmploymentDate));

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                queris.DeletePersonTest();
            }
        }

        [Test]
        public void Update_InsertUIValidationTest()
        {
            try
            {
                person_testObject = this.GetReadyForUpdate(0, SysLanguageResource.Parsi, LocalLanguageResource.Parsi);
                person_testObject.UIValidationGroup = new UIValidationGroup() { ID = ADOUIValidationGroup1.ID };

                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                ClearSession();
                decimal personId = person_testObject.ID;
                person_testObject = new Person();
                person_testObject = busPerson.GetByID(personId);
                Assert.IsNotNull(ADOUIValidationGroup1);
                Assert.AreEqual(ADOUIValidationGroup1.ID, person_testObject.UIValidationGroup.ID);
               

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                queris.DeletePersonTest();
            }
        }

        [Test]
        public void Update_InsertEnglishDateTest()
        {
            try
            {
                person_testObject = this.GetReadyForUpdate(0,SysLanguageResource.English, LocalLanguageResource.Parsi);
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                ClearSession();
                decimal personId = person_testObject.ID;
                person_testObject = new Person();
                person_testObject = busPerson.GetByID(personId);

                Assert.AreEqual(person_testObject.PersonDetail.UIBirthDate, Utility.ToString(Utility.ToMildiDate("1390/05/03")));
                Assert.AreEqual(person_testObject.UIEmploymentDate,  Utility.ToString(Utility.ToMildiDate("1380/05/03")));
                Assert.AreEqual(person_testObject.UIEndEmploymentDate, Utility.ToString(Utility.ToMildiDate("1390/05/03")));

                Assert.AreEqual(person_testObject.PersonDetail.BirthDate, Utility.ToMildiDateTime(person_testObject.PersonDetail.UIBirthDate));
                Assert.AreEqual(person_testObject.EmploymentDate, Utility.ToMildiDateTime(person_testObject.UIEmploymentDate));
                Assert.AreEqual(person_testObject.EndEmploymentDate, Utility.ToMildiDateTime(person_testObject.UIEndEmploymentDate));

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally 
            {
                queris.DeletePersonTest();
            }
        }

        [Test]
        public void Update_Insert_UIValidation_Test()
        {
            try
            {
                person_testObject = this.GetReadyForUpdate(0, SysLanguageResource.English, LocalLanguageResource.Parsi);
                person_testObject.UIValidationGroup = null;
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PersonUIValidationRequierd));
            }
            finally
            {
                queris.DeletePersonTest();
            }
        }

        [Test]
        public void Update_InsertEnglishTest()
        {
            try
            {
                person_testObject = this.GetReadyForUpdate(0,SysLanguageResource.English, LocalLanguageResource.English);
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                ClearSession();
                decimal id = person_testObject.ID;
                person_testObject = new Person();
                person_testObject = busPerson.GetByID(id);

                Assert.AreEqual(person_testObject.PersonCode, "00001222");
                Assert.IsNotNull(person_testObject.PersonRangeAssignList);
                Assert.IsTrue(person_testObject.PersonRangeAssignList.Count > 0);
                Assert.AreEqual(busPerson.GetCurrentActiveWorkGroup(person_testObject.ID), "WorkGroupTest3");
                Assert.AreEqual(busPerson.GetCurrentActiveRuleGroup(person_testObject.ID), "");
                PersonRangeAssignment PRAsg = busPerson.GetCurrentRangeAssignment(person_testObject.ID);
                Assert.AreEqual("CalcGroup3", PRAsg.CalcDateRangeGroup.Name);


            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                queris.DeletePersonTest();
            }
        }

        [Test]
        public void Update_UpdateParsiTest()
        {
            try
            {
                person_testObject = this.GetReadyForUpdate(ADOPerson2.ID, SysLanguageResource.Parsi, LocalLanguageResource.Parsi);
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                ClearSession();
                decimal id = person_testObject.ID;
                person_testObject = new Person();
                person_testObject = busPerson.GetByID(id);

                Assert.AreEqual(person_testObject.PersonCode, "00001222");
                Assert.IsNotNull(person_testObject.PersonRangeAssignList);
                Assert.AreEqual(person_testObject.PersonRangeAssignList.Count , 7);
                Assert.AreEqual(busPerson.GetCurrentActiveWorkGroup(person_testObject.ID), "WorkGroupTest3");
                Assert.AreEqual(busPerson.GetCurrentActiveRuleGroup(person_testObject.ID), "RuleGroupTest2");
                PersonRangeAssignment PRAsg = busPerson.GetCurrentRangeAssignment(person_testObject.ID);
                Assert.AreEqual(PRAsg.CalcDateRangeGroup.Name, "RangeGroup1");

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                queris.DeletePersonTest();
            }
        }

        [Test]
        public void Update_UpdateEnglishTest()
        {
            try
            {
                person_testObject = this.GetReadyForUpdate(ADOPerson2.ID, SysLanguageResource.English, LocalLanguageResource.English);
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                ClearSession();
                decimal id = person_testObject.ID;
                person_testObject = new Person();
                person_testObject = busPerson.GetByID(id);

                Assert.AreEqual(person_testObject.PersonCode, "00001222");
                Assert.IsNotNull(person_testObject.PersonRangeAssignList);
                Assert.AreEqual(person_testObject.PersonRangeAssignList.Count, 7);
                Assert.AreEqual(busPerson.GetCurrentActiveWorkGroup(person_testObject.ID), "WorkGroupTest3");
                Assert.AreEqual(busPerson.GetCurrentActiveRuleGroup(person_testObject.ID), "RuleGroupTest2");
                PersonRangeAssignment PRAsg = busPerson.GetCurrentRangeAssignment(person_testObject.ID);
                Assert.AreEqual(PRAsg.CalcDateRangeGroup.Name, "RangeGroup1");

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                queris.DeletePersonTest();
            }
        }

        [Test]
        public void Update_InsertOrganizationUnitTest()
        {
            try
            {
                person_testObject = this.GetReadyForUpdate(0,SysLanguageResource.Parsi, LocalLanguageResource.Parsi);
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);
                ClearSession();
                decimal personId = person_testObject.ID;
                person_testObject = new Person();
                person_testObject = busPerson.GetByID(personId);

                Assert.IsNotNull(person_testObject.OrganizationUnit);
                Assert.AreEqual(person_testObject.OrganizationUnit.Name, "OrganTestLevel1");

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                queris.DeletePersonTest();
            }
        }

        [Test]
        public void Update_InsertNullOrganizationUnitTest()
        {
            try
            {
                person_testObject = this.GetReadyForUpdate(ADOPerson1.ID, SysLanguageResource.Parsi, LocalLanguageResource.Parsi);
                person_testObject.OrganizationUnit = null;
                busPerson.SaveChanges(person_testObject, UIActionType.EDIT);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                queris.DeletePersonTest();
            }
        }

        #endregion

        [Test]
        public void CreateWorkingPersonTest() 
        {
            try
            {
                decimal id = busPerson.CreateWorkingPerson2();
                ClearSession();
                person_testObject = busPerson.GetByID(id);
                ClearSession();
                busPerson.SaveChanges(person_testObject, UIActionType.DELETE);
                Assert.Pass();
            }
            catch (ItemNotExists ex) 
            {
                Assert.Fail("شخص درج نشده است");
            }
        }

        #region Current Active Assignments
      
        [Test]
        public void ActiveCurentWorkGroupTest()
        {
            person_testObject = busPerson.GetByID(ADOPerson2.ID);
            Assert.AreEqual(busPerson.GetCurrentActiveWorkGroup(person_testObject.ID), ADOWorkGroup1.Name);
        }

        [Test]
        public void ActiveCurentDateRangeTest()
        {
            person_testObject = busPerson.GetByID(ADOPerson2.ID);
            Assert.AreEqual(busPerson.GetCurrentRangeAssignment(person_testObject.ID).CalcDateRangeGroup.Name, ADOGroup1.Name);
        }

        [Test]
        public void ActiveCurentRuleGroupTest()
        {
            person_testObject = busPerson.GetByID(ADOPerson2.ID);
            Assert.AreEqual(busPerson.GetCurrentActiveRuleGroup(person_testObject.ID), ADORuleCat2.Name);
        } 
        #endregion

        #region Quick Search
        [Test]
        public void QuickSearchOnFirstNameTest()
        {
            ISearchPerson searchTool = new BPerson();
            string key = ADOPerson1.BarCode.Remove(0, 2);
            int count = searchTool.GetPersonInQuickSearchCount(key);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, key);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() == 1);
        }

        [Test]
        public void QuickSearchOnFirstNameByPageTest()
        {
            ISearchPerson searchTool = new BPerson();
            string key = ADOPerson1.BarCode.Remove(0, 2);
            int count = searchTool.GetPersonInQuickSearchCount(key);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, key);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() == 1);
        }

        [Test]
        public void QuickSearchOnFirstNameByPage_OutOfRangeTest()
        {
            try
            {
                string key = ADOPerson1.BarCode.Remove(0, 2);
                int count = searchTool.GetPersonInQuickSearchCount(key);
                IList<Person> list = searchTool.QuickSearchByPage(count, 10, key);
                Assert.Fail("رینح خارج از محدوده");
            }
            catch (OutOfExpectedRangeException ex)
            {
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public void QuickSearchOnBarcodeTest()
        {
            string key = ADOPerson1.BarCode.Remove(0, 2);
            int count = searchTool.GetPersonInQuickSearchCount(key);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, key);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() == 1);
        }

        [Test]
        public void QuickSearchOnEmptyTextTest()
        {
            int count = busPerson.GetPersonCount();
            IList<Person> list = searchTool.QuickSearchByPage(0, count, "");
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() == 1);
        }

        [Test]
        public void QuickSearchOnNullTextTest()
        {
            int count = busPerson.GetPersonCount();
            IList<Person> list = searchTool.QuickSearchByPage(0, count, null);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() == 1);
        }

        [Test]
        public void QuickSearchCountOnEmptyTextTest()
        {
            int count1 = busPerson.GetPersonCount();
            int count2 = searchTool.GetPersonInQuickSearchCount(String.Empty);
            Assert.AreEqual(count1, count2);
        }

        [Test]
        public void QuickSearchCountOnNullTextTest()
        {
            int count1 = busPerson.GetPersonCount();
            int count2 = searchTool.GetPersonInQuickSearchCount(null);
            Assert.AreEqual(count1, count2);
        }

        [Test]
        public void QuickSearchOnFirstNameLastName() 
        {
            string key = ADOPerson1.FirstName + ' ' + ADOPerson1.LastName;
            int count = searchTool.GetPersonInQuickSearchCount(key);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, key);
            Assert.IsTrue(list.Where(x => x.ID == ADOPerson1.ID).Count() == 1);
        }
        #endregion

        [Test]
        public void GetCountTest()
        {
            int count = busPerson.GetPersonCount();
            Assert.IsTrue(count > 0);
        }

        [Test]
        public void GetAllByPage_OutOfRangeTest() 
        {
            try
            {
                int count = busPerson.GetPersonCount();
                busPerson.GetAllByPage(count, 10);
                Assert.Fail("رینح خارج از محدوده");
            }
            catch (OutOfExpectedRangeException ex)
            {
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public void GetAllByPageTest() 
        {
            int count = busPerson.GetPersonCount();
            IList<Person> list = busPerson.GetAllByPage(count, 0);
            Assert.IsTrue(list.Where(x => x.PersonCode == ADOPerson1.PersonCode).Count() == 1);
        }

        [Test]
        public void GetAllByPage_FaTest()
        {
            busPerson = new BPerson(SysLanguageResource.Parsi, LocalLanguageResource.Parsi);

            int count = busPerson.GetPersonCount();
            IList<Person> list = busPerson.GetAllByPage(count, 0);
            Assert.IsTrue(list.Where(x => x.PersonCode == ADOPerson1.PersonCode).Count() == 1);
        }

        [Test]
        public void PersonDetailTest() 
        {
            Person p= busPerson.GetByID(ADOPerson1.ID);
            string s = p.PersonDetail.MeliCode;
            Assert.IsNotNullOrEmpty(s);
        }

        [Test]
        public void GetAll_UIValidationNotNull()
        {
            IList<Person> list = busPerson.GetAllByPage(10, 0);
            Assert.IsTrue(list.Where(x => x.UIValidationGroup == null).Count() == 0);
        }

        private Person GetReadyForUpdate(decimal personid, SysLanguageResource sys, LocalLanguageResource local) 
        {
            try
            {
                busPerson = new BPerson(sys, local);
                if (personid == 0)
                {
                    person_testObject.ID = busPerson.CreateWorkingPerson2();
                    ClearSession();
                }
                else 
                {
                    person_testObject.ID = personid;
                }
                person_testObject.PersonDetail = new PersonDetail();

                #region Assigns

                BAssignWorkGroup bAssginWorkGroup = new BAssignWorkGroup(SysLanguageResource.Parsi);
                BWorkgroup workgroup = new BWorkgroup();
                decimal wID = workgroup.SaveChanges(new WorkGroup() { Name = "WorkGroupTest3", CustomCode = "0-3" }, UIActionType.ADD);
                AssignWorkGroup aw = new AssignWorkGroup();
                aw.UIFromDate = Utility.ToPersianDate(DateTime.Now);
                aw.WorkGroup = new WorkGroup() { ID = wID, Name = "WorkGroupTest3" };
                aw.Person = new Person() { ID = person_testObject.ID };
                bAssginWorkGroup.SaveChanges(aw, UIActionType.ADD);

                BAssignRule bAssginRule = new BAssignRule(SysLanguageResource.Parsi);
                BRuleCategory bruleCat = new BRuleCategory();
                bruleCat.EnableInsertValidate = false;
                decimal rID = bruleCat.SaveChanges(new RuleCategory() { Name = "RuleCatTest3", CustomCode = "00-00test2" }, UIActionType.ADD);
                PersonRuleCatAssignment pa = new PersonRuleCatAssignment();
                pa.UIFromDate = Utility.ToPersianDate(new DateTime(2016,1,1));
                pa.UIToDate = Utility.ToPersianDate(new DateTime(2017, 1, 1));
                pa.RuleCategory = new RuleCategory() { ID = rID, Name = "RuleCatTest3" };
                pa.Person = new Person() { ID = person_testObject.ID };
                bAssginRule.SaveChanges(pa, UIActionType.ADD);

                BAssignDateRange bDateRange = new BAssignDateRange(SysLanguageResource.Parsi);
                PersonRangeAssignment rangeAssign = new PersonRangeAssignment();
                BDateRange bdate = new BDateRange();

                decimal rangeId = bdate.SaveChanges(new CalculationRangeGroup() { Name = "CalcGroup3" }, UIActionType.ADD);
               
                ClearSession();
               
                rangeAssign.CalcDateRangeGroup = new CalculationRangeGroup() { ID = rangeId };
                if (sys == SysLanguageResource.Parsi)
                {
                    rangeAssign.UIFromDate = "1390/01/01";
                }
                else 
                {
                    rangeAssign.UIFromDate = "1390/01/01"; //Utility.ToString(Utility.ToMildiDate("1390/01/01"));
                }
                rangeAssign.Person = new Person() { ID = person_testObject.ID };
                bDateRange.SaveChanges(rangeAssign, UIActionType.ADD);
                //جهت درج
                //person_testObject.PersonRangeAssignList = new List<PersonRangeAssignment>();
                //person_testObject.PersonRangeAssignList.Add(rangeAssign);
              
                #endregion

                #region Dep
                DatabaseGatewayTableAdapters.TA_DepartmentTableAdapter departmentTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DepartmentTableAdapter();
                decimal departmentId = Convert.ToDecimal(departmentTA.InsertQuery("Level1", "123", 1, ",1,", ""));

                DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter organTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter();
                organTA.InsertQuery("OrganTestLevel1", "0-0Test", null, 1, String.Format(",{0},", 1));
                decimal organId = Convert.ToDecimal(organTA.GetDataByCustomCode("0-0Test")[0]["organ_ID"]);

                DatabaseGatewayTableAdapters.TA_ControlStationTableAdapter sataionTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ControlStationTableAdapter();
                sataionTA.Insert("StationTest1", "0-0Test");
                decimal stationId = Convert.ToDecimal(sataionTA.GetDataByCustomCode("0-0Test")[0]["station_ID"]);

                DatabaseGatewayTableAdapters.TA_EmploymentTypeTableAdapter emplTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_EmploymentTypeTableAdapter();
                emplTA.Insert("EmploymentTypeTest1", "0-0Test");
                decimal employeeId = Convert.ToDecimal(emplTA.GetDataByCustomCode("0-0Test")[0]["emply_ID"]);
                #endregion

                #region UIValidatinGroup
                DatabaseGatewayTableAdapters.TA_UIValidationGroupTableAdapter validationGroupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationGroupTableAdapter();
                UIValidationGroup ADOUIValidationGroupEmpty = new UIValidationGroup();
                validationGroupTA.InsertQuery("TestGroup00");
                DatabaseGateway.TA_UIValidationGroupDataTable groupTable = validationGroupTA.GetDataByName("TestGroup00");
                ADOUIValidationGroupEmpty.ID = (groupTable.Rows[0] as DatabaseGateway.TA_UIValidationGroupRow).UIValGrp_ID;
                person_testObject.UIValidationGroup = ADOUIValidationGroupEmpty;
                #endregion

                person_testObject.FirstName = "Iraj";
                person_testObject.LastName = "Bahadori";
                person_testObject.PersonDetail.FatherName = "Gholzoom";
                person_testObject.PersonDetail.FatherName = "0082111098";               
                person_testObject.PersonDetail.BirthCertificate = "22777";
                person_testObject.PersonDetail.BirthPlace = "Sorhe";
                person_testObject.Education = "لیسانس";
                person_testObject.PersonDetail.Status = "رو هوا";
                person_testObject.PersonDetail.Tel = "23444343";
                person_testObject.PersonDetail.Address = "";
                person_testObject.PersonCode = "00001222";
                person_testObject.CardNum = "4345554";
                person_testObject.EmploymentNum = "123A342-ad";
                person_testObject.Sex = PersonSex.Male;
                person_testObject.MaritalStatus = MaritalStatus.Motaleghe;
                person_testObject.PersonDetail.MilitaryStatus = MilitaryStatus.HeineKhedmat;
                person_testObject.Department = new global::GTS.Clock.Model.Charts.Department() { ID = departmentId };
                person_testObject.OrganizationUnit = new global::GTS.Clock.Model.Charts.OrganizationUnit() { ID = organId, PersonID = person_testObject.ID, Name = "OrganTestLevel1", CustomCode = "0-0",ParentID =1};
                person_testObject.ControlStation = new global::GTS.Clock.Model.BaseInformation.ControlStation() { ID = stationId };
                person_testObject.EmploymentType = new global::GTS.Clock.Model.BaseInformation.EmploymentType() { ID = employeeId };
                if (sys == SysLanguageResource.Parsi)
                {
                    person_testObject.UIEmploymentDate = "1380/05/03";
                    person_testObject.UIEndEmploymentDate = "1390/05/03";
                    person_testObject.PersonDetail.UIBirthDate = "1390/05/03";
                }
                else
                {
                    person_testObject.UIEmploymentDate = Utility.ToString(Utility.ToMildiDate("1380/05/03"));
                    person_testObject.UIEndEmploymentDate = Utility.ToString(Utility.ToMildiDate("1390/05/03"));
                    person_testObject.PersonDetail.UIBirthDate = Utility.ToString(Utility.ToMildiDate("1390/05/03"));
                }

                ClearSession();
                               
                return person_testObject;

            }
            catch (Exception ex)
            {
                throw ex;
            }
      
        }

       

        [Test]
        public void Test222222222() 
        {
            PersonRepository prsRep = new PersonRepository(false);
            prsRep.TEST(1);
        }
    }
}
