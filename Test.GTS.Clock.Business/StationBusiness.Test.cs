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
using GTS.Clock.Business.Security;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class StaionBusinessTest:BaseFixture
    {


        BControlStation busstaion;
        ControlStation station_testObject;

        [SetUp]
        public void TestSetup()
        {
            station_testObject = new ControlStation();
            busstaion = new BControlStation();

            }

        [TearDown]
        public void TreatDown()
        {
            sataionTA.DeleteByCustomCode("0-2Test");
            sataionTA.DeleteByCustomCode("0-3Test");
        }

        [Test]
        public void GetByIDTest()
        {
            station_testObject = busstaion.GetByID(ADOStaion1.ID);
            Assert.IsNotNull(station_testObject);
            Assert.IsTrue(station_testObject.ID == ADOStaion1.ID);

        }

        [Test]
        public void GetAllTest()
        {
            dataAccessControlStationTA.Insert(BUser.CurrentUser.ID, ADOStaion1.ID, false);
            dataAccessControlStationTA.Insert(BUser.CurrentUser.ID, ADOStaion2.ID, false);
            IList<ControlStation> list = busstaion.GetAll();
            Assert.AreEqual(2, list.Count);
        }      

        [Test]
        public void Insert_EmptyNameTest()
        {
            try
            {             
                station_testObject.CustomCode = "0-2Test";
                busstaion.SaveChanges(station_testObject, UIActionType.ADD);
                Assert.Fail("نام نباید خالی باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.StationNameRequierd);
            }
        }

        [Test]
        public void Insert_DublicateNameTest()
        {
            try
            {
                station_testObject.Name = ADOStaion1.Name;
                station_testObject.CustomCode = "0-2Test";
                busstaion.SaveChanges(station_testObject, UIActionType.ADD);
                Assert.Fail("نام گروه کاری نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.StationNameRepeated);
            }
        }

        [Test]
        public void Insert_DublicateCustomCodeTest()
        {
            try
            {
                station_testObject.Name = ADOStaion1.Name + ADOStaion1.Name;
                station_testObject.CustomCode = ADOStaion1.CustomCode;
                busstaion.SaveChanges(station_testObject, UIActionType.ADD);
                Assert.Fail("کد تعریف شده نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.StationCustomCodeRepeated);
            }
        }

        [Test]
        public void Insert_NullCustomCodeTest()
        {
            try
            {
                station_testObject.Name = "CustomeName";
                station_testObject.CustomCode = "0-2Test";
                busstaion.SaveChanges(station_testObject, UIActionType.ADD);
                ClearSession();
                busstaion.SaveChanges(station_testObject, UIActionType.DELETE);
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
                station_testObject.ID = ADOStaion1.ID;
                busstaion.SaveChanges(station_testObject, UIActionType.EDIT);
                Assert.Fail("نام نباید خالی باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.StationNameRequierd);
            }
        }

        [Test]
        public void Update_DublicateNameTest()
        {
            try
            {
                station_testObject.ID = ADOStaion1.ID;
                station_testObject.Name = ADOStaion2.Name;
                busstaion.SaveChanges(station_testObject, UIActionType.EDIT);
                Assert.Fail("نام گروه کاری نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.StationNameRepeated);
            }
        }

        [Test]
        public void Update_DublicateCustomCodeTest()
        {
            try
            {
                station_testObject.ID = ADOStaion1.ID;
                station_testObject.Name = ADOStaion1.Name;
                station_testObject.CustomCode = ADOStaion2.CustomCode;
                busstaion.SaveChanges(station_testObject, UIActionType.EDIT);
                Assert.Fail("کد تعریف شده نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists( ExceptionResourceKeys.StationCustomCodeRepeated));
            }
        }

        [Test]
        public void Update_NullCustomCodeTest()
        {
            try
            {
                station_testObject.ID = ADOStaion2.ID;
                station_testObject.Name = ADOStaion2.Name;
                busstaion.SaveChanges(station_testObject, UIActionType.EDIT);
                ClearSession();
                busstaion.SaveChanges(station_testObject, UIActionType.DELETE);
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
                busstaion.SaveChanges(ADOStaion1, UIActionType.DELETE);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.StationUsedByPerson);
            }
        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                busstaion.SaveChanges(ADOStaion2, UIActionType.DELETE);
                ClearSession();
                busstaion.GetByID(ADOStaion2.ID);
                Assert.Fail("چون نباید در دیتابیس موجود باشد و آنرا واکشی کردیم پس خطا باید پس میداد");
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass(ex.Message);
            }
        }

    }
}
