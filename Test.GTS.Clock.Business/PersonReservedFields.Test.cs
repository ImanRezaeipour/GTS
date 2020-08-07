using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Business.PersonInfo;
using GTS.Clock.Model.PersonInfo;
using GTS.Clock.Infrastructure;
using System.Collections;
using GTS.Clock.Infrastructure.Exceptions.UI;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class PersonReservedFieldsTest : BaseFixture
    {
        #region Variables
        DatabaseGateway2TableAdapters.TA_PersonReserveFieldTableAdapter prsRsvTA = new DatabaseGateway2TableAdapters.TA_PersonReserveFieldTableAdapter();
        DatabaseGateway2TableAdapters.TA_PersonReserveFieldComboValueTableAdapter prsRsvComboTA = new DatabaseGateway2TableAdapters.TA_PersonReserveFieldComboValueTableAdapter();


        BPersonReservedField busReservedFied;
        PersonReserveField prsRsvField_testObject;
        PersonReserveField ADORsvField1 = new PersonReserveField();
        #endregion
        [SetUp]
        public void TestSetup()
        {
            busReservedFied = new BPersonReservedField();
            prsRsvField_testObject = new PersonReserveField();

            prsRsvTA.Insert((decimal)prsRsvTA.GetMaxID() + 1000, "TestPrsRsvField", "TestLable1", 2);
            DatabaseGateway2.TA_PersonReserveFieldDataTable table =
                prsRsvTA.GetDataByOrginName("TestPrsRsvField");
            ADORsvField1.ID = ((DatabaseGateway2.TA_PersonReserveFieldRow)table.Rows[0]).ReserveField_ID;
        }

        [TearDown]
        public void TreatDown()
        {
            prsRsvComboTA.DeleteByReserved(ADORsvField1.ID);
            prsRsvTA.DeleteByOrginName("TestPrsRsvField");
            
        }

        [Test]
        public void GetAllReservedFiles_TextCount_Test()
        {
            prsRsvTA.DeleteByOrginName("TestPrsRsvField");
            IList<PersonReserveField> list = busReservedFied.GetAllReservedFields();
            Assert.AreEqual(15, list.Where(x => x.ControlType == PersonReservedFieldsType.TextValue).Count());
        }

        [Test]
        public void GetAllReservedFiles_ComboCount_Test()
        {
            prsRsvTA.DeleteByOrginName("TestPrsRsvField");
            IList<PersonReserveField> list = busReservedFied.GetAllReservedFields();
            Assert.AreEqual(5 , list.Where(x => x.ControlType == PersonReservedFieldsType.ComboValue).Count());
        }

        [Test]
        public void GetComboItems_Test()
        {
            prsRsvTA.DeleteByOrginName("TestPrsRsvField");
            IList<PersonReserveFieldComboValue> list = busReservedFied.GetComboItemsByOrginalName(PersonReservedFieldComboItems.R16);
            Assert.IsTrue(list!=null);
        }

        [Test]
        public void UpdateReservedFiles_LableEmpty_Test()
        {
            busReservedFied.UpdateReservedFieldLable(ADORsvField1.ID, "updated Lable Test");
            ClearSession();
            prsRsvField_testObject = busReservedFied.GetByID(ADORsvField1.ID);
            Assert.AreEqual("updated Lable Test", prsRsvField_testObject.Lable);
        }

        [Test]
        public void InsertComboValue_Test()
        {
            busReservedFied.InsertComboItem(ADORsvField1.ID, "TestComboTitle2", "TestComboValue");
            ClearSession();
            prsRsvField_testObject = busReservedFied.GetByID(ADORsvField1.ID);
            Assert.AreEqual(1, prsRsvField_testObject.ComboItems.Count);
        }

        [Test]
        public void InsertComboValue_Dublicate_Test()
        {
            try
            {
                busReservedFied.InsertComboItem(ADORsvField1.ID, "TestComboTitle", "TestComboValue");
                ClearSession();
                busReservedFied.InsertComboItem(ADORsvField1.ID, "TestComboTitle", "TestComboValue");
                Assert.Fail();
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.PrsRsvFldComboValueRepeated));
            }
        }

        [Test]
        public void EditComboValue_Test()
        {
            busReservedFied.InsertComboItem(ADORsvField1.ID, "TestComboTitle", "TestComboValue");
            ClearSession();
            prsRsvField_testObject = busReservedFied.GetByID(ADORsvField1.ID);
            PersonReserveFieldComboValue combo = prsRsvField_testObject.ComboItems.First();
            ClearSession();
            busReservedFied.EditComboItem(ADORsvField1.ID, combo.ID, "TestComboTitle1", "TestComboValue1");
            ClearSession();
            prsRsvField_testObject = busReservedFied.GetByID(ADORsvField1.ID);
            combo = prsRsvField_testObject.ComboItems.First();
            Assert.AreEqual("TestComboTitle1", combo.ComboText);
            Assert.AreEqual("TestComboValue1", combo.ComboValue);
        }

        [Test]
        public void DeleteComboValue_Test()
        {
     
                busReservedFied.InsertComboItem(ADORsvField1.ID, "TestComboTitle2", "TestComboValue");
                ClearSession();
                prsRsvField_testObject = busReservedFied.GetByID(ADORsvField1.ID);
                decimal combiId = prsRsvField_testObject.ComboItems.First().ID;
                ClearSession();
                busReservedFied.DeleteComboItem(ADORsvField1.ID, combiId);
                ClearSession();
                prsRsvField_testObject = busReservedFied.GetByID(ADORsvField1.ID);
                Assert.AreEqual(0,prsRsvField_testObject.ComboItems.Count);           

        }
    }
}
