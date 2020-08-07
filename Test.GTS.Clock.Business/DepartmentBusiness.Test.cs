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

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class BDepartmentTest : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_PersonTableAdapter personTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonTableAdapter();
        DatabaseGateway.TA_DepartmentDataTable table = new DatabaseGateway.TA_DepartmentDataTable();

        BDepartment busDep;
        Department ADOdepartmentWithoutPerson = new Department();
        Department department_testObject;

        [SetUp]
        public void TestSetup()
        {
            department_testObject = new Department();
            busDep = new BDepartment();

            departmentTA.InsertQuery("Without Person", "test_0", ADORoot.ID, "," + ADORoot.ID.ToString() + ",", "");
            departmentTA.GetByCustomCode(table, "test_0");
            ADOdepartmentWithoutPerson.ID = Convert.ToDecimal(table.Rows[0][0]);
 
        }

        [TearDown]
        public void TreatDown()
        {        
            departmentTA.DeleteByCustomCode("0-0");
            departmentTA.DeleteByCustomCode("test_0");
            departmentTA.DeleteByCustomCode("0-0Test");
        }

        /// <summary>
        /// آیا گره های والد را درست برمیگرداند
        /// </summary>
        [Test]
        [ExpectedException(typeof(ItemNotExists))]
        public void GetByID_ExceptionTest()
        {
            department_testObject = busDep.GetByID(125);
        }

        [Test]
        public void GetByID_Test()
        {
            department_testObject = busDep.GetByID(ADODepartment1.ID);
            Assert.AreEqual(department_testObject.ID, ADODepartment1.ID);
        }

        [Test]
        public void GetByID_DependencyPersistTest()
        {
            department_testObject = busDep.GetByID(ADODepartment1.ID);
            Assert.IsNotNull(department_testObject.Parent, "After PErsist Parent must be persist");
        }

        [Test]
        public void GetById_PersistPersonListTest()
        {
            department_testObject = busDep.GetByID(ADODepartment1.ID);
            Assert.IsNotNull(department_testObject.PersonList);
        }

        [Test]
        public void GetDepartmentTreeMoreRootFailTest()
        {
            try
            {
                departmentTA.InsertQuery("root", "0-root", 0, "", "");
                busDep.GetDepartmentsTree();
                Assert.Fail("Two root must be exception caused");
            }
            catch (InvalidDatabaseStateException ex)
            {
                Assert.AreEqual(ex.ExceptionType, UIExceptionTypes.Fatal);
                Assert.AreEqual(ex.FatalExceptionIdentifier, UIFatalExceptionIdentifiers.DepartmentRootMoreThanOne);
            }
            finally 
            {
                departmentTA.DeleteByCustomCode("0-root");
            }
        }

        [Test]
        public void GetDepartmentChild_Test() 
        {
            try 
            {
                IList<Department> list = busDep.GetDepartmentChilds(ADORoot.ID);
                Assert.IsTrue(list.Where(x => x.ID == ADODepartment1.ID).Count() > 0);
            }
            catch (Exception ex) 
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetDepartmentChildByParentPath_Test()
        {
            try
            {
                IList<Department> list = busDep.GetDepartmentChildsByParentPath(ADORoot.ID);
                Assert.IsTrue(list.Where(x => x.ID == ADODepartment1.ID).Count() > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_PersistDependencyTest()
        {
            department_testObject.Name = "Inserted";
            department_testObject.CustomCode = "0-0";
            department_testObject.ParentID = ADODepartment1.ID;
            busDep.SaveChanges(department_testObject, UIActionType.ADD);
            Assert.IsTrue(department_testObject.ID > 0, "After insert ID must be persist");
            NHibernateSessionManager.Instance.GetSession().Refresh(department_testObject);
            // ClearSession();
            //department_testObject = busDep.GetByID(department_testObject.ID);
            Assert.IsNotNull(department_testObject.Parent.CustomCode, "After insert and GetbyId Parent Property must be persist");
            Assert.AreEqual(department_testObject.Parent.CustomCode, ADODepartment1.CustomCode, "After insert and GetbyId Parent Property must be persist");

        }

        [Test]
        public void Insert_DuplicateNameTest()
        {
            try
            {
                department_testObject.Name = ADODepartment1.Name;
                department_testObject.ParentID = ADODepartment1.ParentID;
                department_testObject.CustomCode = "0-0";
                busDep.SaveChanges(department_testObject, UIActionType.ADD);
                Assert.Fail("نباید نام تکراری پذیرفته شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Count == 1, String.Format("با توجه به مقداردهی انتظار میرود تنها یک خطا ایجاد شود و تعدا برابر {0} است", ex.Count));
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.DepartmentRepeatedName, "Vlaue:" + ex.ExceptionList[0].ResourceKey.ToString("G"));
            }
        }

        [Test]
        public void Insert_DuplicateNameInOtheLevelTest()
        {
            try
            {
                department_testObject.Name = ADODepartment1.Name;
                department_testObject.ParentID = ADODepartment1.ID;
                department_testObject.CustomCode = "0-0";
                busDep.SaveChanges(department_testObject, UIActionType.ADD);
                Assert.Greater(department_testObject.ID, 0);
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
        [ExpectedException(typeof(UIValidationExceptions))]
        public void Insert_NameValidate()
        {
            department_testObject.CustomCode = "0-0";
            department_testObject.ParentID = ADODepartment1.ParentID;
            busDep.SaveChanges(department_testObject, UIActionType.ADD);
        }

        [Test]
        [ExpectedException(typeof(UIValidationExceptions))]
        public void Insert_NameNullValidate()
        {
            department_testObject.Name = null;
            department_testObject.ParentID = ADODepartment1.ParentID;
            busDep.SaveChanges(department_testObject, UIActionType.ADD);
        }

        [Test]
        [ExpectedException(typeof(UIValidationExceptions))]
        public void Insert_InvalideParentValidate()
        {
            department_testObject.Name = "Ali";
            department_testObject.ParentID = -1;
            busDep.SaveChanges(department_testObject, UIActionType.ADD);
        }

        [Test]
        [ExpectedException(typeof(UIValidationExceptions))]
        public void Insert_InvalideParentValidateTest()
        {
            department_testObject.Name = "Ali";
            department_testObject.ParentID = -1;
            busDep.SaveChanges(department_testObject, UIActionType.ADD);
        }

        [Test]
        public void Insert_ParentPathTest() 
        {
            department_testObject.ParentID = ADODepartment1.ID;
            department_testObject.CustomCode = "0-0";
            department_testObject.Name = "asdasd";
            busDep.SaveChanges(department_testObject, UIActionType.ADD);
            ClearSession();
            department_testObject = busDep.GetByID(department_testObject.ID);
            Assert.AreEqual(String.Format(",{0},,{1},", ADORoot.ID, ADODepartment1.ID), department_testObject.ParentPath);
        }

        [Test]
        public void Delete_DependencyTest()
        {
            try
            {
                busDep.SaveChanges(ADODepartment1, UIActionType.DELETE);
                Assert.Fail("چون این بخش توسط پرسنل استفده شده نباید حذف شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.DepUsedByPersons);
            }
        }

        [Test]       
        public void Delete_Test()
        {
            try
            {
                busDep.SaveChanges(new Department() { ID = ADOdepartmentWithoutPerson.ID }, UIActionType.DELETE);
                ClearSession();
                department_testObject = busDep.GetByID(ADOdepartmentWithoutPerson.ID);
                Assert.Fail("Item is not deleted");
            }
            catch (ItemNotExists ex) 
            {
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public void Delete_RootTest()
        {
            try
            {
                busDep.SaveChanges(ADORoot, UIActionType.DELETE);
                Assert.Fail("ریشه نمیتواند حذف شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.DepartmentRootDeleteIllegal);
            }

        }

        [Test]
        public void DeleteHirenchicaly_Test1() 
        {
            try
            {
                department_testObject.Name = ADOdepartmentWithoutPerson.Name + "0";
                department_testObject.ParentID = ADOdepartmentWithoutPerson.ID;
                department_testObject.CustomCode = "0-0Test";
                busDep.SaveChanges(department_testObject, UIActionType.ADD);
                decimal childId = department_testObject.ID;
                ClearSession();

                department_testObject = new Department();
                department_testObject.ID = ADOdepartmentWithoutPerson.ID;
                busDep.DeleteDepartment(department_testObject, UIActionType.DELETE);

                ClearSession();

                busDep.GetByID(childId);
                Assert.Fail("Child doesnot deleted");
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void DeleteHirenchicaly_Test2()
        {
            try
            {
                department_testObject.Name = ADOdepartmentWithoutPerson.Name + "0";
                department_testObject.ParentID = ADOdepartmentWithoutPerson.ID;
                department_testObject.CustomCode = "0-0Test";
                busDep.SaveChanges(department_testObject, UIActionType.ADD);
                decimal childId = department_testObject.ID;
                ClearSession();

                department_testObject = new Department();
                department_testObject.ID = ADOdepartmentWithoutPerson.ID;
                busDep.DeleteDepartment(department_testObject, UIActionType.DELETE);

                ClearSession();

                busDep.GetByID(ADOdepartmentWithoutPerson.ID);
                Assert.Fail("Item doesnot deleted");
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void DeleteHirenchicaly_ChildUsedValidation_Test()
        {
            try
            {
                departmentTA.UpdateParent(ADOdepartmentWithoutPerson.ID, ",162," + ADOdepartmentWithoutPerson.ID + ",", ADODepartment1.ID);
              
                ClearSession();

                department_testObject = new Department();
                department_testObject.ID = ADOdepartmentWithoutPerson.ID;
                busDep.DeleteDepartment(department_testObject, UIActionType.DELETE);

                Assert.Fail("چون این بخش توسط پرسنل استفده شده نباید حذف شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.DepUsedByPersons));
            }
        }

        [Test]
        public void Update_CustomCodeTest()
        {
            try
            {
                department_testObject.ID = ADODepartment1.ID;
                department_testObject.Name = ADODepartment1.Name;
                department_testObject.ParentID = ADODepartment1.ParentID;
                department_testObject.CustomCode = "0-0";
                busDep.SaveChanges(department_testObject, UIActionType.EDIT);
                department_testObject = busDep.GetByID(ADODepartment1.ID);
                Assert.AreEqual(department_testObject.CustomCode, "0-0");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Update_ParentPathTest() 
        {
            department_testObject.ID = ADODepartment1.ID;
            department_testObject.Name = ADODepartment1.Name;
            department_testObject.CustomCode = ADODepartment1.CustomCode;
            busDep.SaveChanges(department_testObject, UIActionType.EDIT);
            ClearSession();
            department_testObject = busDep.GetByID(ADODepartment1.ID);
            Assert.AreEqual(ADODepartment1.ParentPath, department_testObject.ParentPath);
        }

        [Test]
        public void Update_Test()
        {
            department_testObject.ID = ADODepartment1.ID;
            department_testObject.CustomCode = ADODepartment1.CustomCode;
            department_testObject.ParentID = ADODepartment1.ParentID;
            department_testObject.Name = "Updated";
            busDep.SaveChanges(department_testObject, UIActionType.EDIT);
            department_testObject = busDep.GetByID(ADODepartment1.ID);
            Assert.AreEqual(department_testObject.Name, "Updated");
        }

        [Test]
        public void Update_Root()
        {
            department_testObject.ID = ADORoot.ID;
            department_testObject.CustomCode = ADORoot.CustomCode;
            department_testObject.Name = "RootUpdated";
            busDep.SaveChanges(department_testObject, UIActionType.EDIT);
            department_testObject = busDep.GetByID(ADORoot.ID);
            Assert.AreEqual(department_testObject.Name, "RootUpdated");
        }
    }
}
