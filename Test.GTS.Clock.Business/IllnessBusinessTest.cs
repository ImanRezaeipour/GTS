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
    public class IllnessBusinessTest:BaseFixture
    {
        DatasetGatewayWorkFlowTableAdapters.TA_IllnessTableAdapter illnessTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_IllnessTableAdapter();
        DatabaseGatewayTableAdapters.TA_PersonTableAdapter personTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonTableAdapter();


        BIllness busstaion;
        Illness ADOIllness1 = new Illness();
        Illness ADOIllness2 = new Illness();
        Illness illness_testObject;

        [SetUp]
        public void TestSetup()
        {
            illness_testObject = new Illness();
            busstaion = new BIllness();

            illnessTA.Insert("illnessTest1", "0-0");
            illnessTA.Insert("illnessTest2", "0-1");

            DatasetGatewayWorkFlow.TA_IllnessDataTable table = new DatasetGatewayWorkFlow.TA_IllnessDataTable();
            illnessTA.FillByName(table, "illnessTest1");
            ADOIllness1.ID = Convert.ToInt32(table.Rows[0]["illness_ID"]);
            ADOIllness1.Name = "illnessTest1";
            ADOIllness1.Description = "0-1";

            illnessTA.FillByName(table, "illnessTest2");
            ADOIllness2.ID = Convert.ToInt32(table.Rows[0]["illness_ID"]);
            ADOIllness2.Name = "illnessTest2";
            ADOIllness2.Description = "0-2";

            //personTA.Insert("0000", null, null, "", null, "", null, ADOIllness1.ID, null, null, null, "", "", null, "", null);

        }

        [TearDown]
        public void TreatDown()
        {
            personTA.DeleteByBarcode("0000");
            illnessTA.DeleteByName("illnessTest1");
            illnessTA.DeleteByName("illnessTest2");
            illnessTA.DeleteByName("illnessTest3");
        }

        [Test]
        public void GetByIDTest()
        {
            illness_testObject = busstaion.GetByID(ADOIllness1.ID);
            Assert.IsNotNull(illness_testObject);
            Assert.IsTrue(illness_testObject.ID == ADOIllness1.ID);

        }

        [Test]
        public void GetAllTest()
        {
            IList<Illness> list = busstaion.GetAll();
            Assert.AreEqual(list.Count, illnessTA.GetCount());
        }      

        [Test]
        public void Insert_EmptyNameTest()
        {
            try
            {             
                illness_testObject.Description = "0-2";
                busstaion.SaveChanges(illness_testObject, UIActionType.ADD);
                Assert.Fail("نام نباید خالی باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.IllnessNameRequierd);
            }
        }

        [Test]
        public void Insert_DublicateNameTest()
        {
            try
            {
                illness_testObject.Name = ADOIllness1.Name;
                illness_testObject.Description = "0-2";
                busstaion.SaveChanges(illness_testObject, UIActionType.ADD);
                Assert.Fail("نام نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.IllnessNameRepeated);
            }
        }

        [Test]
        public void Insert_Test()
        {
            try
            {
                illness_testObject.Name = "IllnessTest3";
                illness_testObject.Description = "0-3";
                busstaion.SaveChanges(illness_testObject, UIActionType.ADD);
                ClearSession();
                Assert.IsTrue(illness_testObject.ID > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
   
        [Test]
        public void Update_EmptyNameTest()
        {
            try
            {
                illness_testObject.ID = ADOIllness1.ID;
                busstaion.SaveChanges(illness_testObject, UIActionType.EDIT);
                Assert.Fail("نام نباید خالی باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.IllnessNameRequierd);
            }
        }

        [Test]
        public void Update_DublicateNameTest()
        {
            try
            {
                illness_testObject.ID = ADOIllness1.ID;
                illness_testObject.Name = ADOIllness2.Name;
                busstaion.SaveChanges(illness_testObject, UIActionType.EDIT);
                Assert.Fail("نام نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.IllnessNameRepeated);
            }
        }

        [Test]
        public void Update_Test()
        {
            try
            {
                illness_testObject.ID = ADOIllness1.ID;
                illness_testObject.Name = "IllnessTest3";
                busstaion.SaveChanges(illness_testObject, UIActionType.EDIT);
                ClearSession();
                illness_testObject = busstaion.GetByID(ADOIllness1.ID);
                Assert.AreEqual(illness_testObject.Name, "IllnessTest3");
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
                busstaion.SaveChanges(ADOIllness2, UIActionType.DELETE);
                ClearSession();
                busstaion.GetByID(ADOIllness2.ID);
                Assert.Fail("چون نباید در دیتابیس موجود باشد و آنرا واکشی کردیم پس خطا باید پس میداد");
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass(ex.Message);
            }
        }

    }
}
