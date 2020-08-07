using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business.Security;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class OrganizationBusiness : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_PersonTableAdapter personTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonTableAdapter();
        DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter organTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter();
        DatabaseGateway.TA_OrganizationUnitDataTable table = new DatabaseGateway.TA_OrganizationUnitDataTable();

        BOrganizationUnit busOrgan;
        OrganizationUnit ADOOrgan = new OrganizationUnit();
        OrganizationUnit ADOOrgan2 = new OrganizationUnit();
        OrganizationUnit ADOOrgan3 = new OrganizationUnit();
        OrganizationUnit ADOOrganParent = new OrganizationUnit();
        OrganizationUnit organ_testObject;

        [SetUp]
        public void TestSetup() 
        {
            Convert.ToInt32(personTA.InsertQuery("0000", "Ali", true,null));
            int personId = Convert.ToInt32(personTA.GetDataByBarcode("0000").Rows[0]["prs_ID"]);
            organ_testObject = new OrganizationUnit();
            busOrgan = new BOrganizationUnit();

            table = organTA.GetDataByParent();
            ADOOrganParent.ID = Convert.ToInt32(table.Rows[0]["organ_ID"]);
            ADOOrganParent.Name = Convert.ToString(table.Rows[0]["organ_Name"]);
            ADOOrganParent.CustomCode = Convert.ToString(table.Rows[0]["organ_CustomCode"]);

            organTA.InsertQuery("Level2_1", "0-1", personId, ADOOrganParent.ID,String.Format(",{0},",ADOOrganParent.ID));
            organTA.InsertQuery("Level2_2", "0-2", personId, ADOOrganParent.ID, String.Format(",{0},", ADOOrganParent.ID));

            table = organTA.GetDataByCustomCode("0-1");
            ADOOrgan.ID = Convert.ToInt32(table.Rows[0]["organ_ID"]);
            ADOOrgan.ParentID = Convert.ToInt32(table.Rows[0]["organ_ParentID"]);
            ADOOrgan.Name = Convert.ToString(table.Rows[0]["organ_Name"]);
            ADOOrgan.CustomCode = Convert.ToString(table.Rows[0]["organ_CustomCode"]);
            ADOOrgan.PersonID = Convert.ToInt32(table.Rows[0]["organ_PersonID"]);

            table = organTA.GetDataByCustomCode("0-2");
            ADOOrgan2.ID = Convert.ToInt32(table.Rows[0]["organ_ID"]);
            ADOOrgan2.ParentID = Convert.ToInt32(table.Rows[0]["organ_ParentID"]);
            ADOOrgan2.Name = Convert.ToString(table.Rows[0]["organ_Name"]);
            ADOOrgan2.CustomCode = Convert.ToString(table.Rows[0]["organ_CustomCode"]);
            ADOOrgan2.PersonID = Convert.ToInt32(table.Rows[0]["organ_PersonID"]);

            organTA.Insert("Level3_1", "0-3", personId, ADOOrgan.ID, String.Format(",{0},{1}", ADOOrganParent.ID, ADOOrgan.ID));

            table = organTA.GetDataByCustomCode("0-3");
            ADOOrgan3.ID = Convert.ToInt32(table.Rows[0]["organ_ID"]);
            ADOOrgan3.ParentID = Convert.ToInt32(table.Rows[0]["organ_ParentID"]);
            ADOOrgan3.Name = Convert.ToString(table.Rows[0]["organ_Name"]);
            ADOOrgan3.CustomCode = Convert.ToString(table.Rows[0]["organ_CustomCode"]);
            ADOOrgan3.PersonID = Convert.ToInt32(table.Rows[0]["organ_PersonID"]);

        }

        [TearDown]
        public void TreatDown()
        {
            organTA.DeleteByCustomCode("0-1");
            organTA.DeleteByCustomCode("0-2");
            organTA.DeleteByCustomCode("0-3");
            organTA.DeleteByCustomCode("0-4");
            personTA.DeleteByBarcode("0000");

        }      

        [Test]
        public void GetByID_Test()
        {
            organ_testObject = busOrgan.GetByID(ADOOrgan.ID);
            Assert.AreEqual(organ_testObject.ID, ADOOrgan.ID);
        }

        [Test]
        public void GetById_PersistPersonTest()
        {
            organ_testObject = busOrgan.GetByID(ADOOrgan.ID);
            Assert.IsNotNull(organ_testObject.Person);
            Assert.AreEqual(organ_testObject.Person.BarCode, "0000");
        }     

        [Test]
        public void GetByID_DependencyPersistTest()
        {
            organ_testObject = busOrgan.GetByID(ADOOrgan.ID);
            Assert.IsNotNull(organ_testObject.Parent,"After Persisting Parent also must be persist");
            Assert.AreEqual(organ_testObject.Parent.ID, ADOOrganParent.ID, "After Persisting Parent also must be persist");
            Assert.AreEqual(organ_testObject.ParentID, ADOOrganParent.ID, "After Persisting Parent also must be persist");
        }

        [Test]
        public void Update_Test()
        {
            organ_testObject.ID = ADOOrgan.ID;
            organ_testObject.Name = "Updated";
            organ_testObject.CustomCode = ADOOrgan.CustomCode;
            organ_testObject.ParentID = ADOOrgan.ParentID;
            organ_testObject.PersonID = ADOOrgan.PersonID;
            busOrgan.SaveChanges(organ_testObject,UIActionType.EDIT);
            organ_testObject = busOrgan.GetByID(ADOOrgan.ID);
            Assert.AreEqual(organ_testObject.Name, "Updated");
        }

        [Test]
        public void Update_InvalideParentValidateTest()
        {
            try
            {
                organ_testObject.ID = ADOOrgan.ID;
                organ_testObject.CustomCode = ADOOrgan.CustomCode;
                organ_testObject.Name = ADOOrgan.Name;
                busOrgan.SaveChanges(organ_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey, ExceptionResourceKeys.OrganizationUnitParentIDRequierd);
            }
        }

        [Test]
        public void Update_DublicateNameValidate()
        {
            try
            {
                organ_testObject.ID = ADOOrgan2.ID;
                organ_testObject.ParentID = ADOOrgan2.ParentID;
                organ_testObject.PersonID = ADOOrgan2.PersonID;
                organ_testObject.Name = ADOOrgan.Name;
                busOrgan.SaveChanges(organ_testObject, UIActionType.EDIT);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey, ExceptionResourceKeys.OrganizationUnitNameRepeated);
            }
        }

        [Test]
        public void Update_RootTest()
        {
            organ_testObject.ID = ADOOrganParent.ID;
            organ_testObject.CustomCode = ADOOrganParent.CustomCode;
            organ_testObject.PersonID = ADOOrganParent.PersonID;
            organ_testObject.Name = "RootUpdated";
            busOrgan.SaveChanges(organ_testObject, UIActionType.EDIT);
            organ_testObject = busOrgan.GetByID(ADOOrganParent.ID);
            Assert.AreEqual(organ_testObject.Name, "RootUpdated");
        }

        [Test]
        public void Insert_CustomCodeValidate()
        {
            try
            {
                organ_testObject.Name = "0-4";
                organ_testObject.ParentID = ADOOrgan.ParentID;
                organ_testObject.CustomCode = ADOOrgan2.CustomCode;
                busOrgan.SaveChanges(organ_testObject,UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey, ExceptionResourceKeys.OrganizationUnitCustomCodeRepeated);
            }
        }

        [Test]
        public void Insert_NameNullValidate()
        {
            try
            {
                organ_testObject.ID = ADOOrgan.ID;
                organ_testObject.CustomCode = ADOOrgan.CustomCode;
                organ_testObject.Name = null;
                organ_testObject.ParentID = ADOOrgan.ParentID;
                busOrgan.SaveChanges(organ_testObject,UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey, ExceptionResourceKeys.OrganizationUnitNameRequierd);
            }
        }

        [Test]
        public void Insert_DublicateNameValidate()
        {
            try
            {
                organ_testObject.CustomCode = "0-4";
                organ_testObject.ParentID = ADOOrgan.ParentID;
                organ_testObject.PersonID = ADOOrgan.PersonID;
                organ_testObject.Name = ADOOrgan.Name;
                busOrgan.SaveChanges(organ_testObject, UIActionType.ADD);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey, ExceptionResourceKeys.OrganizationUnitNameRepeated);
            }
        }

        [Test]
        public void Insert_DuplicateNameInOtheLevelTest()
        {
            try
            {
                organ_testObject.Name = ADOOrgan.Name;
                organ_testObject.ParentID = ADOOrgan.ID;
                organ_testObject.PersonID = ADOOrgan.PersonID;
                organ_testObject.CustomCode = "0-4";
                busOrgan.SaveChanges(organ_testObject,UIActionType.ADD);
                Assert.Greater(organ_testObject.ID, 0);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(String.Format("{0} Message:{1}", " نام تکراری پذیرفته شود که در سطوح مختلف باشد", ex.GetLogMessage()));
            }

            catch (Exception ex)
            {
                Assert.Fail(String.Format("{0} Message:{1}", "***** NHibernate Error", ex.Message));
            }
        }      

        [Test]
        public void Insert_PersistDependencyTest()
        {
            organ_testObject.Name = "Inserted";
            organ_testObject.CustomCode = "0-4";
            organ_testObject.ParentID = ADOOrgan.ID;
            organ_testObject.PersonID = ADOOrgan.PersonID;
            busOrgan.SaveChanges(organ_testObject, UIActionType.ADD);
            Assert.IsTrue(organ_testObject.ID > 0, "After insert ID must be persist");
            Assert.IsNotNull(organ_testObject.Parent, "After insert and GetbyId Parent Property must be persist");

        }

        [Test]
        public void Delete_PersonDependencyTest()
        {
            try
            {
                busOrgan.SaveChanges(ADOOrgan, UIActionType.DELETE);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.OrganizationUnitUsedByPerson);
            }
        }

        [Test]
        public void Delete_RootTest()
        {
            try
            {
                busOrgan.SaveChanges(ADOOrganParent, UIActionType.DELETE);
                Assert.Fail("ریشه نمیتواند حذف شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.OrganizationUnitRootDeleteIllegal);
            }

        }

        [Test]
        public void GetOrganizationTreeTest()
        {
            organ_testObject = busOrgan.GetOrganizationUnitTree();
            Assert.AreEqual(organ_testObject.ParentID, 0);

        }

        [Test]
        public void GetChild_Test() 
        {
            dataAccessOrganTA.Insert(BUser.CurrentUser.ID, ADOOrgan.ID, false);

            IList<OrganizationUnit> list = busOrgan.GetChilds(ADOOrganParent.ID);
            Assert.IsTrue(list.Where(x => x.ID == ADOOrgan.ID).Count() > 0);
        }
    }
}
