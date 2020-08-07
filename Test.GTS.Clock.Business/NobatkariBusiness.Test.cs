using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.NHibernateFramework;

namespace GTSTestUnit.Clock.Business
{    
    [TestFixture]
    public class NobatkariBusiness:BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_NobatKariTableAdapter nobatTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_NobatKariTableAdapter();
        DatabaseGatewayTableAdapters.TA_ShiftTableAdapter shiftTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ShiftTableAdapter();
        
        BNobatkari busNobatkari;
        NobatKari ADONobatKari = new NobatKari();
        NobatKari nobatKari_testObject;

        [SetUp]
        public void TestSetup()
        {          
            nobatKari_testObject = new NobatKari();
            busNobatkari = new BNobatkari();
            nobatTA.Insert("NobatKari", "", "0-0");
            DatabaseGateway.TA_NobatKariDataTable table = new DatabaseGateway.TA_NobatKariDataTable();
            nobatTA.FillByCustomCode(table, "0-0");
            ADONobatKari.ID = Convert.ToInt32(table.Rows[0]["nobat_ID"]);
            ADONobatKari.Name = Convert.ToString(table.Rows[0]["nobat_Name"]);
            ADONobatKari.CustomCode = Convert.ToString(table.Rows[0]["nobat_CustomCode"]);
            ADONobatKari.Description = Convert.ToString(table.Rows[0]["nobat_Description"]);
            shiftTA.Insert("ShiftTest", 1, 11, ADONobatKari.ID, 100, false, false, false, "2", "0-0");
        }

        [TearDown]
        public void TreatDown()
        {
            shiftTA.DeleteByCustomCode("0-0");
            nobatTA.DeleteByCustomCode("0-0");           
        }

        [Test]
        public void GetByIDTest()
        {
            nobatKari_testObject = busNobatkari.GetByID(ADONobatKari.ID);
            Assert.IsNotNull(nobatKari_testObject);
            Assert.IsTrue(nobatKari_testObject.ID > 0);

        }

        [Test]
        public void GetAllTest()
        {
            IList<NobatKari> list = busNobatkari.GetAll();
            Assert.AreEqual(list.Count, nobatTA.GetCount());
        }

        [Test]
        public void LoadShiftListTest()
        {
            nobatKari_testObject = busNobatkari.GetByID(ADONobatKari.ID);
            Assert.IsNotNull(nobatKari_testObject.ShiftList,"Shift List not null");
            Assert.IsTrue(nobatKari_testObject.ShiftList.Count > 0,"Loaded Shift Count");
        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                busNobatkari.SaveChanges(ADONobatKari, UIActionType.DELETE);
                Assert.Fail("چون این نوبت توسط شیفت استفده شده نباید حذف شود");
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.NobatKariUsedByShift);
            }
        }

        [Test]
        [ExpectedException(typeof(UIValidationExceptions))]
        public void Insert_EmptyNameTest()
        {
            nobatKari_testObject.Description = "نوبت کاری اول";
            nobatKari_testObject.CustomCode = "0-0";
            busNobatkari.SaveChanges(nobatKari_testObject,UIActionType.ADD);
        }

        [Test]
        public void Insert_DublicateCustomCodeTest()
        {
            try
            {
                nobatKari_testObject.Name = ADONobatKari.Name + ADONobatKari.Name;
                nobatKari_testObject.CustomCode = ADONobatKari.CustomCode;
                busNobatkari.SaveChanges(nobatKari_testObject,UIActionType.ADD);
                Assert.Fail("کد تعریف شده نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey, ExceptionResourceKeys.NobatKariCustomCodeRepeated);
            }
        }        



    }
}
