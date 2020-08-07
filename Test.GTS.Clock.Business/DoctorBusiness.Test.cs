using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.Security;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class DoctorBusinessTest : BaseFixture
    {
        #region Table Adapters
        DatasetGatewayWorkFlowTableAdapters.TA_DoctorTableAdapter doctorTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_DoctorTableAdapter();
        #endregion

        #region ADOObjects
        BDoctor busDoctor = new BDoctor();
        Doctor ADODoctor1 = new Doctor();
        Doctor ADODoctor2 = new Doctor();
        Doctor doctor_testobject;
        #endregion

        [SetUp]
        public void TestSetup()
        {
            busDoctor = new BDoctor();
            doctor_testobject = new Doctor();
            doctorTA.Insert("nameTest1", "DcotorTest1", "", "12345", "");
            DatasetGatewayWorkFlow.TA_DoctorDataTable table = doctorTA.GetDataByName("DcotorTest1");
            ADODoctor1.ID = Convert.ToDecimal(table.Rows[0][0]);
            ADODoctor1.LastName = "DcotorTest1";
            ADODoctor1.Nezampezaeshki = "12345";

            doctorTA.Insert("nameTest3", "DcotorTest3", "", "12346", "");
            table = doctorTA.GetDataByName("DcotorTest3");
            ADODoctor2.ID = Convert.ToDecimal(table.Rows[0][0]);
            ADODoctor2.LastName = "DcotorTest3";
            ADODoctor2.Nezampezaeshki = "12346";
        }

        [TearDown]
        public void TreatDown()
        {
            doctorTA.DeleteByLastName("DcotorTest1");
            doctorTA.DeleteByLastName("DcotorTest2");
            doctorTA.DeleteByLastName("DcotorTest3");
        }

        [Test]
        public void GetByID_Test()
        {
            try 
            {
                doctor_testobject = busDoctor.GetByID(ADODoctor1.ID);
                Assert.AreEqual(doctor_testobject.Nezampezaeshki, ADODoctor1.Nezampezaeshki);
            }
            catch (Exception ex) 
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetAll_Test()
        {
            dataAccessDoctorTA.Insert(BUser.CurrentUser.ID, ADODoctor1.ID, false);
            dataAccessDoctorTA.Insert(BUser.CurrentUser.ID, ADODoctor2.ID, false);
            Assert.AreEqual(2, busDoctor.GetAll().Count);
        }

        [Test]
        public void Insert_EmptyNameTest()
        {
            try
            {
                doctor_testobject.Description = "0-2";
                busDoctor.SaveChanges(doctor_testobject, UIActionType.ADD);
                Assert.Fail("نام نباید خالی باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.DoctorLastNameRequierd);
            }
        }

        [Test]
        public void Insert_EmptyNezampezeshkiTest()
        {
            try
            {
                doctor_testobject.LastName = "DcotorTest2";
                busDoctor.SaveChanges(doctor_testobject, UIActionType.ADD);
                Assert.Greater(doctor_testobject.ID, 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [Test]
        public void Insert_DublicateNezampezeshkiTest()
        {
            try
            {
                doctor_testobject.LastName = "DcotorTest2";
                doctor_testobject.Nezampezaeshki = ADODoctor1.Nezampezaeshki;
                busDoctor.SaveChanges(doctor_testobject, UIActionType.ADD);
                Assert.Fail("نظام پزشکی نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.DoctorNezampezeshkiRepeated);
            }
        }

        [Test]
        public void Insert_Test()
        {
            try
            {
                doctor_testobject.LastName = "DcotorTest2";
                doctor_testobject.Nezampezaeshki = "125487";
                busDoctor.SaveChanges(doctor_testobject, UIActionType.ADD);
                Assert.Greater(doctor_testobject.ID, 0);
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
                doctor_testobject.ID = ADODoctor1.ID;
                busDoctor.SaveChanges(doctor_testobject, UIActionType.EDIT);
                Assert.Fail("نام نباید خالی باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.DoctorLastNameRequierd));
            }
        }

        [Test]
        public void Update_EmptyNezampezeshkiTest()
        {
            try
            {
                doctor_testobject.ID = ADODoctor1.ID;
                doctor_testobject.LastName = "DcotorTest2";
                busDoctor.SaveChanges(doctor_testobject, UIActionType.EDIT);
                ClearSession();
                doctor_testobject = busDoctor.GetByID(ADODoctor1.ID);
                Assert.AreEqual(doctor_testobject.LastName, "DcotorTest2");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Update_DublicateNezampezeshkiTest()
        {
            try
            {
                doctor_testobject.ID = ADODoctor1.ID;
                doctor_testobject.Nezampezaeshki = ADODoctor2.Nezampezaeshki;
                busDoctor.SaveChanges(doctor_testobject, UIActionType.EDIT);
                Assert.Fail("نظام پزشکی نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.DoctorNezampezeshkiRepeated));
            }
        }

        [Test]
        public void Update_Test()
        {
            try
            {
                doctor_testobject.ID = ADODoctor1.ID;
                doctor_testobject.LastName = "DcotorTest2";
                doctor_testobject.Nezampezaeshki = "1254547";
                busDoctor.SaveChanges(doctor_testobject, UIActionType.EDIT);
                ClearSession();
                doctor_testobject = busDoctor.GetByID(ADODoctor1.ID);
                Assert.AreEqual(doctor_testobject.LastName, "DcotorTest2");
                Assert.AreEqual(doctor_testobject.Nezampezaeshki, "1254547");
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
                busDoctor.SaveChanges(ADODoctor1, UIActionType.DELETE);
                ClearSession();
                busDoctor.GetByID(ADODoctor1.ID);
                Assert.Fail("چون نباید در دیتابیس موجود باشد و آنرا واکشی کردیم پس خطا باید پس میداد");
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass(ex.Message);
            }
        }
    }
}
