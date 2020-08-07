using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Utility;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class ShiftBusiness : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_ShiftTableAdapter shiftTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ShiftTableAdapter();
        DatabaseGatewayTableAdapters.TA_ShiftPairTableAdapter shiftPairTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ShiftPairTableAdapter();
        DatabaseGatewayTableAdapters.TA_NobatKariTableAdapter nobatTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_NobatKariTableAdapter();

        BShift businessShift;
        Shift shift_testObject = new Shift();
        ShiftPair shiftpair_testObject = new ShiftPair();
        NobatKari ADONobatKari = new NobatKari();
        Shift ADOShift = new Shift();

        [SetUp]
        public void TestSetup()
        {
            shift_testObject = new Shift();
            shiftpair_testObject = new ShiftPair();
            businessShift = new BShift();

            nobatTA.Insert("NobatKari", "", "0-00");
            DatabaseGateway.TA_NobatKariDataTable table = new DatabaseGateway.TA_NobatKariDataTable();
            nobatTA.FillByCustomCode(table, "0-00");
            ADONobatKari.ID = Convert.ToInt32(table.Rows[0]["nobat_ID"]);
            ADONobatKari.Name = Convert.ToString(table.Rows[0]["nobat_Name"]);
            ADONobatKari.CustomCode = Convert.ToString(table.Rows[0]["nobat_CustomCode"]);
            ADONobatKari.Description = Convert.ToString(table.Rows[0]["nobat_Description"]);

            shiftTA.Insert("ShiftTest", 1, 11, ADONobatKari.ID, 100, false, false, false, "2","0-00");
            ADOShift = new Shift();

            DatabaseGateway.TA_ShiftDataTable shiftTable = new DatabaseGateway.TA_ShiftDataTable();
            shiftTA.FillByName(shiftTable, "ShiftTest");
            ADOShift.ID = Convert.ToInt32(shiftTable.Rows[0]["shift_ID"]);
            ADOShift.Name = Convert.ToString(shiftTable.Rows[0]["shift_Name"]);
            ADOShift.Color = Convert.ToString(shiftTable.Rows[0]["shift_Color"]);
            ADOShift.NobatKariID = Convert.ToInt32(shiftTable.Rows[0]["shift_Nobatkari"]);
            ADOShift.CustomCode = Convert.ToString(shiftTable.Rows[0]["shift_CustomCode"]);

            shiftPairTA.Insert(ADOShift.ID, 100, 200, 0, 0);
            shiftPairTA.Insert(ADOShift.ID, 300, 500, 0, 0);

            DatabaseGateway.TA_ShiftPairDataTable shiftpairTable = new DatabaseGateway.TA_ShiftPairDataTable();
            shiftPairTA.FillByShiftId(shiftpairTable, ADOShift.ID);
            
            ShiftPair pair = new ShiftPair();
            pair.ID = Convert.ToInt32(shiftpairTable.Rows[0]["shiftpair_ID"]);
            pair.From = Convert.ToInt32(shiftpairTable.Rows[0]["shiftpair_From"]);
            pair.To = Convert.ToInt32(shiftpairTable.Rows[0]["shiftpair_To"]);
            ADOShift.Pairs.Add(pair);

            pair = new ShiftPair();
            pair.ID = Convert.ToInt32(shiftpairTable.Rows[1]["shiftpair_ID"]);
            pair.From = Convert.ToInt32(shiftpairTable.Rows[1]["shiftpair_From"]);
            pair.To = Convert.ToInt32(shiftpairTable.Rows[1]["shiftpair_To"]);
            ADOShift.Pairs.Add(pair);

        }

        [TearDown]
        public void TreatDown()
        {            
            shiftTA.DeleteByCustomCode("0-00");
            shiftTA.DeleteByCustomCode("0-01");
            shiftTA.DeleteByCustomCode("0-02");

            nobatTA.DeleteByCustomCode("0-00");          
        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                businessShift.SaveChanges(ADOShift,UIActionType.DELETE);
               Shift shift= businessShift.GetByID(ADOShift.ID);
                Assert.Fail("آیتم حذف نشده است");
            }
            catch (ItemNotExists ex) 
            {
                Assert.Pass(ex.Message);
            }
            
        }

        [Test]
        public void GetByID_Test()
        {
            shift_testObject = businessShift.GetByID(ADOShift.ID);
            Assert.IsNotNull(shift_testObject);
            Assert.IsTrue(shift_testObject.ID == ADOShift.ID);
        }

        [Test]
        public void GetAll_Test()
        {
            dataAccessShiftTA.Insert(BUser.CurrentUser.ID, ADOShift.ID, false);
            IList<Shift> list = businessShift.GetAll();
            Assert.AreEqual(1, list.Count);
        }     

        [Test]
        public void Insert_NobatKariByIDTest()
        {
            shift_testObject.Color = "5";
            shift_testObject.Name = "test nobatkari shift";
            shift_testObject.CustomCode = "0-01";
            shift_testObject.NobatKariID = ADONobatKari.ID;
            shift_testObject.ShiftType = ShiftTypesEnum.WORK;
            businessShift.SaveChanges(shift_testObject,UIActionType.ADD);
            Assert.IsTrue(shift_testObject.ID > 0);
            Assert.IsNotNull(shift_testObject.NobatKari);
        }

        [Test]
        public void Insert_NullNobatKariTest()
        {
            shift_testObject.Color = "5";
            shift_testObject.Name = "test nobatkari shift";
            shift_testObject.CustomCode = "0-01";
            shift_testObject.NobatKari = null;
            shift_testObject.ShiftType = ShiftTypesEnum.WORK;
            businessShift.SaveChanges(shift_testObject, UIActionType.ADD);
            Assert.IsTrue(shift_testObject.ID > 0);
        }      

        [Test]
        public void Insert_ValidateEmptyName()
        {
            try
            {
                shift_testObject.Name = "";
                shift_testObject.Color = "1";
                shift_testObject.NobatKari = ADONobatKari;
                shift_testObject.ShiftType = ShiftTypesEnum.WORK;
                businessShift.SaveChanges(shift_testObject,UIActionType.ADD);
                Assert.Fail("نام خالی نباید درج شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.ShiftNameRequierd);
            }

        }      

        [Test]
        public void Insert_ValidationDuplicateNameTest() 
        {
            try
            {
                shift_testObject.Name = ADOShift.Name;
                shift_testObject.NobatKari = ADONobatKari;
                shift_testObject.Color = ADOShift.Color + 1;
                shift_testObject.CustomCode = ADOShift.CustomCode;
                shift_testObject.ShiftType = ShiftTypesEnum.WORK;
                businessShift.SaveChanges(shift_testObject, UIActionType.ADD);
                Assert.Fail("نام تکراری نباید درج شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.ShiftNameRepeated);
            }
        }

        [Test]
        public void Insert_ValidationDuplicateColorTest()
        {
            try
            {
                shift_testObject.Name = "Test";
                shift_testObject.Color = ADOShift.Color;
                shift_testObject.NobatKari = ADONobatKari;
                shift_testObject.ShiftType = ShiftTypesEnum.WORK;
                businessShift.SaveChanges(shift_testObject, UIActionType.ADD);
                /*shift_testObject.ID = 391;
                shift_testObject.Name = "shift12";
                shift_testObject.Color = "#9999FF";
                shift_testObject.ShiftType = ShiftTypesEnum.WORK;
                shift_testObject.MinNobatKari = 480;
                shift_testObject.CustomCode = "012";
                businessShift.SaveChanges(shift_testObject);*/
                Assert.Fail("رنگ تکراری نباید درج شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ShiftColorRepeated));
            }
        }

        [Test]
        public void Insert_ValidationShiftAndShiftPair()
        {
            shift_testObject = new Shift();
            shift_testObject.Name = "ShiftTest2";
            shift_testObject.Color = "5";
            shift_testObject.CustomCode = "0-01";
            shift_testObject.MinNobatKari = 2;
            shift_testObject.ShiftType = ShiftTypesEnum.WORK;

            businessShift.SaveChanges(shift_testObject, UIActionType.ADD);

            shiftpair_testObject.FromTime = "10:00";
            shiftpair_testObject.ToTime = "20:00";
            shiftpair_testObject.ShiftId = shift_testObject.ID;
            businessShift.SaveChangesShiftPair(shiftpair_testObject, UIActionType.ADD);

            decimal id = shift_testObject.ID;
            shift_testObject = new Shift();
            ClearSession();
            shift_testObject = businessShift.GetByID(id);
            Assert.AreEqual(shift_testObject.Pairs.Count, 1);
        }

        [Test]
        public void Insert_ValidateDublicateCustomCodeInsertTest()
        {
            try
            {
                shift_testObject.Name = ADOShift.Name + ADOShift.Name;
                shift_testObject.Color = ADOShift.Color + ADOShift.Color;
                shift_testObject.ShiftType = ShiftTypesEnum.WORK;
                shift_testObject.CustomCode = ADOShift.CustomCode;
                businessShift.SaveChanges(shift_testObject, UIActionType.ADD);
                Assert.Fail("کد تعریف شده نباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey, ExceptionResourceKeys.ShiftCustomCodeRepeated);
            }
        }


        [Test]
        public void Insert_ShiftPair_NextDay_Test()
        {
            shift_testObject.Name = "TestShift";
            shift_testObject.CustomCode = "0-02";
            shift_testObject.Color = "6454165416";
            shift_testObject.NobatKari = ADONobatKari;
            shift_testObject.ShiftType = ShiftTypesEnum.WORK;
            businessShift.SaveChanges(shift_testObject, UIActionType.ADD);

            shiftpair_testObject.FromTime = "10:00";
            shiftpair_testObject.ToTime = "20:00";
            shiftpair_testObject.ShiftId = shift_testObject.ID;
            shiftpair_testObject.NextDayContinual = true;
            businessShift.SaveChangesShiftPair(shiftpair_testObject, UIActionType.ADD);

            ClearSession();
            Shift shift = businessShift.GetByID(shift_testObject.ID);
            Assert.AreEqual(1, shift.PairCount);
            Assert.IsTrue(shift.Pairs[0].To > 1440);
        }


        [Test]
        public void Update_ShiftPair_Test()
        {
            shift_testObject.Name = "TestShift";
            shift_testObject.CustomCode = "0-02";
            shift_testObject.Color = "2134154";
            shift_testObject.NobatKari = ADONobatKari;
            shift_testObject.ShiftType = ShiftTypesEnum.WORK;
            businessShift.SaveChanges(shift_testObject, UIActionType.ADD);

            shiftpair_testObject.FromTime = "10:00";
            shiftpair_testObject.ToTime = "20:00";
            shiftpair_testObject.ShiftId = shift_testObject.ID;
            shiftpair_testObject.NextDayContinual = true;
            businessShift.SaveChangesShiftPair(shiftpair_testObject, UIActionType.ADD);

            ClearSession();
            Shift shift = businessShift.GetByID(shift_testObject.ID);

            shiftpair_testObject = new ShiftPair();
            shiftpair_testObject.ID = shift.Pairs[0].ID;
            shiftpair_testObject.FromTime = "04:00";
            shiftpair_testObject.ToTime = "03:00";
            shiftpair_testObject.AfterToleranceTime = "05:00";
            shiftpair_testObject.BeforeToleranceTime = "00:00";
            shiftpair_testObject.ShiftId = shift.ID;
            //shiftpair_testObject.TimePlusFlag = true;
            shiftpair_testObject.NextDayContinual = true;

            businessShift.SaveChangesShiftPair(shiftpair_testObject, UIActionType.EDIT);

            ClearSession();

            shift = businessShift.GetByID(shift_testObject.ID);  

            Assert.AreEqual(1, shift.PairCount);
            Assert.AreEqual(300, shift.Pairs[0].AfterTolerance);

            Assert.IsTrue(shift.Pairs[0].To > 1440);
        }

        [Test]
        public void Update_ValidateEmptyColor()
        {
            try
            {
                shift_testObject.ID = ADOShift.ID;
                shift_testObject.Name = ADOShift.Name;
                shift_testObject.CustomCode = ADOShift.CustomCode;
                shift_testObject.Color = "";
                shift_testObject.NobatKari = ADONobatKari;
                shift_testObject.ShiftType = ShiftTypesEnum.WORK;
                businessShift.SaveChanges(shift_testObject, UIActionType.EDIT);
                Assert.Fail("رنگ خالی نباید درج شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.ShiftColorRequierd);
            }

        }

        [Test]
        public void Update_ValidateEmptyShiftType()
        {
            try
            {
                shift_testObject.ID = ADOShift.ID;
                shift_testObject.Name = ADOShift.Name;
                shift_testObject.CustomCode = ADOShift.CustomCode;
                shift_testObject.Color = ADOShift.Color;
                shift_testObject.NobatKari = ADONobatKari;
                shift_testObject.ShiftType = null;
                businessShift.SaveChanges(shift_testObject, UIActionType.EDIT);
                Assert.Fail("نوع شیفت نباید خالی درج شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.ShiftTypeRequierd);
            }

        }

        [Test]
        public void Update_ValidateInvalidShiftType()
        {
            try
            {
                shift_testObject.ID = ADOShift.ID;
                shift_testObject.Name = ADOShift.Name;
                shift_testObject.CustomCode = ADOShift.CustomCode;
                shift_testObject.Color = ADOShift.Color;
                shift_testObject.NobatKari = ADONobatKari;
                shift_testObject.ShiftType = (ShiftTypesEnum)Enum.ToObject(typeof(ShiftTypesEnum), -1);
                businessShift.SaveChanges(shift_testObject, UIActionType.EDIT);
                Assert.Fail("نوع شیفت نباید خالی درج شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.ShiftTypeRequierd);
            }

        }

        [Test]
        public void Update_ValidateUpdateNameTest()
        {
            //shift_testObject = businessShift.GetByID(ADOShift.ID);
            Shift shift = new Shift();
            shift.ID = ADOShift.ID;
            shift.Color = ADOShift.Color;
            shift.ID = ADOShift.ID;
            shift.Breakfast = ADOShift.Breakfast;
            shift.Lunch = ADOShift.Lunch;
            shift.Dinner = ADOShift.Dinner;
            shift.CustomCode = ADOShift.CustomCode;
            shift.NobatKariID = ADONobatKari.ID;
            shift.ShiftType = ShiftTypesEnum.WORK;

            shift.Name = "updatedName";

            businessShift.SaveChanges(shift, UIActionType.EDIT);
            Assert.AreEqual(shift.ID, ADOShift.ID, "باید بروزرسانی شده باشد نه درج");
            Assert.AreEqual(shift.Name, "updatedName");

            ClearSession();

            businessShift.SaveChanges(shift, UIActionType.DELETE);
            Assert.Pass();
        }

        [Test]
        public void Update_ValidationDuplicateNameTest()
        {
            try
            {
                shift_testObject.ID = ADOShift.ID;
                shift_testObject.Name = ADOShift.Name;
                shift_testObject.NobatKari = ADONobatKari;
                shift_testObject.Color = ADOShift.Color + 1;
                shift_testObject.CustomCode = ADOShift.CustomCode;
                shift_testObject.ShiftType = ShiftTypesEnum.WORK;
                businessShift.SaveChanges(shift_testObject, UIActionType.EDIT);
                Assert.Pass("هنگام بروزرسانی در مقایسه نام تکراری باید توجه داشت که شناسه تکراری ها مخالف شناسه مقایسه شونده باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.ShiftNameRepeated);
            }
        }

        [Test]
        public void Update_ValidationDuplicateNameTest2()
        {
            try
            {
                Shift shift = new Shift();
                shift.Name = "XX";
                shift.Color = "50";
                shift.CustomCode = "0-01";
                shift.ShiftType = ShiftTypesEnum.WORK;
                businessShift.SaveChanges(shift, UIActionType.ADD);

                shift_testObject.ID = ADOShift.ID;
                shift_testObject.Name = shift.Name;
                shift_testObject.NobatKari = ADONobatKari;
                shift_testObject.Color = ADOShift.Color + 1;
                shift_testObject.CustomCode = ADOShift.CustomCode;
                shift_testObject.ShiftType = ShiftTypesEnum.WORK;
                businessShift.SaveChanges(shift_testObject, UIActionType.EDIT);
                Assert.Fail("نام تکراری نباید بروزرسانی شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.ShiftNameRepeated);
            }
        }      

        [Test]
        public void ValidateInsertUpdateShiftPair_LoadPairsTest() 
        {
            int? count = shiftTA.GetPairCount((int)ADOShift.ID);
            shift_testObject = businessShift.GetByID(ADOShift.ID);
            Assert.IsNotNull(shift_testObject.Pairs);
            Assert.AreEqual(shift_testObject.Pairs.Count, count);
        }

        [Test]
        public void ValidateInsertUpdateShiftPair_MidNightTest()
        {
            try
            {
                shift_testObject = businessShift.GetByID(ADOShift.ID);
                shiftpair_testObject.FromTime = "00:00";
                shiftpair_testObject.ToTime = Utility.IntTimeToTime(shift_testObject.Pairs[0].From - 10);
                shiftpair_testObject.ShiftId = shift_testObject.ID;
                businessShift.SaveChangesShiftPair(shiftpair_testObject, UIActionType.ADD);
                shift_testObject = new Shift();
                ClearSession();

                int? count = shiftTA.GetPairCount((int)ADOShift.ID);
                shift_testObject = businessShift.GetByID(ADOShift.ID);
                Assert.AreEqual(shift_testObject.Pairs.Count, count);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail("نیمه شب باید درج میشد");
            }
        }

        [Test]
        public void ValidateInsertUpdateShiftPair_timePlusTest()
        {
            try
            {
                shift_testObject = businessShift.GetByID(ADOShift.ID);
                shiftpair_testObject.FromTime = "10:00";
                shiftpair_testObject.ToTime = "02:00";
                shiftpair_testObject.NextDayContinual = true;
                shiftpair_testObject.ShiftId = shift_testObject.ID;
                businessShift.SaveChangesShiftPair(shiftpair_testObject, UIActionType.ADD);
                shift_testObject = new Shift();
                ClearSession();

                shift_testObject = businessShift.GetByID(ADOShift.ID);
                Assert.IsTrue(shift_testObject.Pairs.Where(x => x.ToTime.Contains("+")).Count() > 0);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail("نیمه شب باید درج میشد");
            }
        }

        [Test]
        public void ValidateInsertUpdateShiftPair_FromGreaterThanToTest()
        {
            try
            {
                shift_testObject = businessShift.GetByID(ADOShift.ID);
                shiftpair_testObject= shift_testObject.Pairs[0];
                shiftpair_testObject.FromTime = "10:02";
                shiftpair_testObject.ToTime = "10:01";
                shiftpair_testObject.ShiftId = shift_testObject.ID;
                businessShift.SaveChangesShiftPair(shiftpair_testObject, UIActionType.EDIT);
                Assert.Fail("مقدار ابتدا از انتها بزرگتر است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey , ExceptionResourceKeys.ShiftFromGreaterThanTo);
            }

        }

        [Test]
        public void ValidateInsertUpdateShiftPair_HasIntersectTest()
        {
            try
            {
                shift_testObject = businessShift.GetByID(ADOShift.ID);
                shiftpair_testObject.From = shift_testObject.Pairs[0].To - 10;
                shiftpair_testObject.To = shift_testObject.Pairs[0].To + 10;
                shiftpair_testObject.ShiftId = shift_testObject.ID;
                businessShift.SaveChangesShiftPair(shiftpair_testObject, UIActionType.ADD);
                Assert.Fail("بازه ها نباید اشتراک داشته باشند");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey, ExceptionResourceKeys.ShiftPairHasIntersect);
            }
        }

        [Test]
        public void ValidateInsertUpdateShiftPair_HasIntersectUpdateTestPass()
        {
            try
            {
                shiftpair_testObject.ID = ADOShift.Pairs[0].ID;
                shiftpair_testObject.From = ADOShift.Pairs[0].From;
                shiftpair_testObject.To = ADOShift.Pairs[0].To;
                shiftpair_testObject.ShiftId = ADOShift.ID;
                businessShift.SaveChangesShiftPair(shiftpair_testObject, UIActionType.EDIT);
                Assert.Pass();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail("در هنگام بروزرسانی باید شناسه بررسی شود");
            }
        }

        [Test]
        public void ValidateInsertUpdateShiftPair_HasIntersectUpdateTestFail()
        {
            try
            {
                shift_testObject = businessShift.GetByID(ADOShift.ID);
                shiftpair_testObject.ID = shift_testObject.Pairs[0].ID;
                shiftpair_testObject.From = shift_testObject.Pairs[0].From;
                shiftpair_testObject.To = shift_testObject.Pairs[1].To;
                shiftpair_testObject.ShiftId = shift_testObject.ID;
                businessShift.SaveChangesShiftPair(shiftpair_testObject, UIActionType.EDIT);
                Assert.Fail("در هنگام بروزرسانی باید شناسه بررسی شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey, ExceptionResourceKeys.ShiftPairHasIntersect);
            }
        }

        [Test]
        public void ValidateInsertUpdateShiftPair_FromToAreAqualsTest()
        {
            try
            {
                shift_testObject = businessShift.GetByID(ADOShift.ID);
                shiftpair_testObject = shift_testObject.Pairs[0];
                shiftpair_testObject.From = shiftpair_testObject.To;
                shiftpair_testObject.ShiftId = shift_testObject.ID;
                businessShift.SaveChangesShiftPair( shiftpair_testObject, UIActionType.EDIT);
                Assert.Fail("مقدار ابتدا با انتها برابر است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList.First().ResourceKey, ExceptionResourceKeys.ShiftFromAndToAreEquals);
            }
        }       

    }
}
