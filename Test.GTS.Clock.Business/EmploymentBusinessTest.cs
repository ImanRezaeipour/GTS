using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.NHibernateFramework;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class EmploymentBusinessTest:BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_EmploymentTypeTableAdapter emplTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_EmploymentTypeTableAdapter();
        DatabaseGatewayTableAdapters.TA_PersonTableAdapter personTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonTableAdapter();


        BEmployment busEmployment;
        EmploymentType employmentType_testObject;

        [SetUp]
        public void TestSetup()
        {
            employmentType_testObject = new EmploymentType();
            busEmployment = new BEmployment();

         
            personTA.Insert("0000", null, null, "", null, "", null, null, null, ADOEmploymentType1.ID, null, "", "", null, "", null);

        }

        [TearDown]
        public void TreatDown()
        {
            personTA.DeleteByBarcode("0000");
            emplTA.DeleteByCustomCode("0-2Test");
        }

        [Test]
        public void GetByIDTest()
        {
            employmentType_testObject = busEmployment.GetByID(ADOEmploymentType1.ID);
            Assert.IsNotNull(employmentType_testObject);
            Assert.IsTrue(employmentType_testObject.ID == ADOEmploymentType1.ID);

        }

        [Test]
        public void GetAllTest()
        {
            IList<EmploymentType> list = busEmployment.GetAll();
            Assert.AreEqual(list.Count, emplTA.GetCount());
        }      

        [Test]
        public void Insert_EmptyNameTest()
        {
            try
            {             
                employmentType_testObject.CustomCode = "0-2Test";
                busEmployment.SaveChanges(employmentType_testObject, UIActionType.ADD);
                Assert.Fail("نام نباید خالی باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.EmploymentTypeNameRequierd);
            }
        }

        [Test]
        public void Insert_DublicateNameTest()
        {
            try
            {
                employmentType_testObject.Name = ADOEmploymentType1.Name;
                employmentType_testObject.CustomCode = "0-2Test";
                busEmployment.SaveChanges(employmentType_testObject, UIActionType.ADD);
                Assert.Fail("نام گروه کاری نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.EmploymentTypeNameRepeated);
            }
        }

        [Test]
        public void Insert_DublicateCustomCodeTest()
        {
            try
            {
                employmentType_testObject.Name = ADOEmploymentType1.Name + ADOEmploymentType1.Name;
                employmentType_testObject.CustomCode = ADOEmploymentType1.CustomCode;
                busEmployment.SaveChanges(employmentType_testObject, UIActionType.ADD);
                Assert.Fail("کد تعریف شده نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.EmploymentTypeCustomCodeRepeated);
            }
        }

        [Test]
        public void Insert_NullCustomCodeTest()
        {
            try
            {
                employmentType_testObject.Name = "CustomeName";
                busEmployment.SaveChanges(employmentType_testObject, UIActionType.ADD);
                ClearSession();
                busEmployment.SaveChanges(employmentType_testObject, UIActionType.DELETE);
                Assert.Pass();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Update_EmptyNameTest()
        {
            try
            {
                employmentType_testObject.ID = ADOEmploymentType1.ID;
                busEmployment.SaveChanges(employmentType_testObject, UIActionType.EDIT);
                Assert.Fail("نام نباید خالی باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.EmploymentTypeNameRequierd);
            }
        }

        [Test]
        public void Update_DublicateNameTest()
        {
            try
            {
                employmentType_testObject.ID = ADOEmploymentType1.ID;
                employmentType_testObject.Name = ADOEmploymentType2.Name;
                busEmployment.SaveChanges(employmentType_testObject, UIActionType.EDIT);
                Assert.Fail("نام گروه کاری نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.EmploymentTypeNameRepeated);
            }
        }

        [Test]
        public void Update_DublicateCustomCodeTest()
        {
            try
            {
                employmentType_testObject.ID = ADOEmploymentType1.ID;
                employmentType_testObject.Name = ADOEmploymentType1.Name;
                employmentType_testObject.CustomCode = ADOEmploymentType2.CustomCode;
                busEmployment.SaveChanges(employmentType_testObject, UIActionType.EDIT);
                Assert.Fail("کد تعریف شده نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.EmploymentTypeCustomCodeRepeated));
            }
        }

        [Test]
        public void Update_NullCustomCodeTest()
        {
            try
            {
                employmentType_testObject.ID = ADOEmploymentType2.ID;
                employmentType_testObject.Name = ADOEmploymentType2.Name;
                busEmployment.SaveChanges(employmentType_testObject, UIActionType.EDIT);
                ClearSession();
                busEmployment.SaveChanges(employmentType_testObject, UIActionType.DELETE);
                Assert.Pass();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [Test]
        public void Delete_PersonConfilictTest() 
        {
            try
            {
                busEmployment.SaveChanges(ADOEmploymentType1, UIActionType.DELETE);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.EmploymentTypeUsedByPerson);
            }
        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                busEmployment.SaveChanges(ADOEmploymentType2, UIActionType.DELETE);
                ClearSession();
                busEmployment.GetByID(ADOEmploymentType2.ID);
                Assert.Fail("چون نباید در دیتابیس موجود باشد و آنرا واکشی کردیم پس خطا باید پس میداد");
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass(ex.Message);
            }
        }

    }
}
