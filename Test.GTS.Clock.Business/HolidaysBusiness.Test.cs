using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business.Proxy;



namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 2011-12-03 8:03:34 AM
    /// write your name here
    /// </summary>
    [TestFixture]
    public class HolidaysBusinessTest : BaseFixture
    {
        #region TAble Adapters
        DatabaseGatewayTableAdapters.TA_CalendarTableAdapter calendarTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_CalendarTableAdapter();
        DatabaseGatewayTableAdapters.TA_CalendarTypeTableAdapter calanderTypeTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_CalendarTypeTableAdapter();

        #endregion

        #region Properties
        BCalendarType busCalendarType;
        CalendarType calendarType_testObject;
        Calendar ADOCalendar1 = new Calendar();
        Calendar ADOCalendar2 = new Calendar();
        Calendar ADOCalendar3 = new Calendar();
        CalendarType ADOCalendarType1 = new CalendarType();
        CalendarType ADOCalendarType2 = new CalendarType();
        CalendarType ADOCalendarType_Rasmi = new CalendarType();
        #endregion

        [SetUp]
        public void TestSetup()
        {
            calendarType_testObject = new CalendarType();
            busCalendarType = new BCalendarType();
            busCalendarType.InTestCasePersonContext = new List<decimal>() { ADOPerson1.ID };


            calanderTypeTA.Insert("TestCalendarType1", "test-1");
            DatabaseGateway.TA_CalendarTypeDataTable calendarTypeTable = calanderTypeTA.GetDataByName("TestCalendarType1");
            DatabaseGateway.TA_CalendarTypeRow calendarTypeRow = calendarTypeTable.Rows[0] as DatabaseGateway.TA_CalendarTypeRow;
            ADOCalendarType1.ID = calendarTypeRow.CalendarType_ID;
            ADOCalendarType1.Name = calendarTypeRow.CalendarType_Name;
            ADOCalendarType1.CustomCode = calendarTypeRow.CalendarType_CustomCode;

            calanderTypeTA.Insert("TestCalendarType2", "test-2");
            calendarTypeTable = calanderTypeTA.GetDataByName("TestCalendarType2");
            calendarTypeRow = calendarTypeTable.Rows[0] as DatabaseGateway.TA_CalendarTypeRow;
            ADOCalendarType2.ID = calendarTypeRow.CalendarType_ID;
            ADOCalendarType2.Name = calendarTypeRow.CalendarType_Name;
            ADOCalendarType2.CustomCode = calendarTypeRow.CalendarType_CustomCode;

            calendarTypeTable = calanderTypeTA.GetDataByCustomCode("1");
            calendarTypeRow = calendarTypeTable.Rows[0] as DatabaseGateway.TA_CalendarTypeRow;
            ADOCalendarType_Rasmi.ID = calendarTypeRow.CalendarType_ID;
            ADOCalendarType_Rasmi.Name = calendarTypeRow.CalendarType_Name;
            ADOCalendarType_Rasmi.CustomCode = calendarTypeRow.CalendarType_CustomCode;


            calendarTA.Insert(new DateTime(2010, 5, 1), ADOCalendarType1.ID);
            calendarTA.Insert(new DateTime(2010, 5, 3), ADOCalendarType1.ID);
            calendarTA.Insert(new DateTime(2010, 5, 5), ADOCalendarType1.ID);

            calendarTypeTable = calanderTypeTA.GetDataByName("TestCalendarType1");
            DatabaseGateway.TA_CalendarDataTable calendarRows = calendarTA.GetDataByTypeId(ADOCalendarType1.ID);

            ADOCalendar1.ID = ((DatabaseGateway.TA_CalendarRow)calendarRows.Rows[0]).Calendar_ID;
            ADOCalendar2.ID = ((DatabaseGateway.TA_CalendarRow)calendarRows.Rows[1]).Calendar_ID;
            ADOCalendar3.ID = ((DatabaseGateway.TA_CalendarRow)calendarRows.Rows[2]).Calendar_ID;
        }

        [TearDown]
        public void TreatDown()
        {
            calanderTypeTA.DeleteByName("TestCalendarType1");
            calanderTypeTA.DeleteByName("TestCalendarType2");
            calanderTypeTA.DeleteByName("TestCalendarType111");
            calanderTypeTA.DeleteByName("TestType2");
        }

        [Test]
        public void GetById_Test()
        {
            calendarType_testObject = busCalendarType.GetByID(ADOCalendarType1.ID);
            Assert.AreEqual(ADOCalendarType1.ID, calendarType_testObject.ID);
            Assert.AreEqual(3, calendarType_testObject.CalanderList.Count);
        }

        [Test]
        public void GetCalendarList_Test() 
        {
            IList<CalendarCellInfo> list = busCalendarType.GetCalendarList(1389, ADOCalendarType1.ID);
            Assert.AreEqual(3, list.Count);

        }

        [Test]
        public void Insert_Test()
        {
            try
            {
                calendarType_testObject.CustomCode = "test_2";
                calendarType_testObject.Name = "TestType2";
                decimal id = busCalendarType.SaveChanges(calendarType_testObject, UIActionType.ADD);
                ClearSession();
                calendarType_testObject = new CalendarType();
                calendarType_testObject = busCalendarType.GetByID(id);
                Assert.AreEqual("TestType2", calendarType_testObject.Name);
            }
            catch (Exception ex) 
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_EmtyNameTest() 
        {
            try
            {
                busCalendarType.SaveChanges(calendarType_testObject, UIActionType.ADD);
                Assert.Fail("نام خالی است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.CalendarNameRequierd));
            }
        }

        [Test]
        public void Insert_RepeatNameTest()
        {
            try
            {
                calendarType_testObject.Name = ADOCalendarType1.Name;
                busCalendarType.SaveChanges(calendarType_testObject, UIActionType.ADD);
                Assert.Fail("نام تکراری است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.CalendarNameRepeated));
            }
        }

        [Test]
        public void Insert_EmtyCustomCodeTest()
        {
            try
            {
                busCalendarType.SaveChanges(calendarType_testObject, UIActionType.ADD);
                Assert.Fail("نام خالی است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.CalendarCustomCodeRequierd));
            }
        }

        [Test]
        public void Insert_RepeatCustomCodeTest()
        {
            try
            {
                calendarType_testObject.CustomCode = ADOCalendarType1.CustomCode;
                busCalendarType.SaveChanges(calendarType_testObject, UIActionType.ADD);
                Assert.Fail("نام تکراری است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.CalendarCustomCodeRepeated));
            }
        }

        [Test]
        public void Insert_CalnendarTest()
        {
            IList<CalendarCellInfo> list = new List<CalendarCellInfo>();
            list.Add(new CalendarCellInfo() { Day = 1, Month = 3 });
            list.Add(new CalendarCellInfo() { Day = 2, Month = 3 });
            list.Add(new CalendarCellInfo() { Day = 3, Month = 3 });
            list.Add(new CalendarCellInfo() { Day = 4, Month = 3 });

            busCalendarType.InsertCalendars(ADOCalendarType1.ID, 1389, list);

            ClearSession();
            list = new List<CalendarCellInfo>();
            list = busCalendarType.GetCalendarList(1389, ADOCalendarType1.ID);
            Assert.AreEqual(4, list.Count, "تقویم در دیتابیس ثبت نشده است");
            Assert.IsTrue(list.Where(x => x.Month == 2).Count() == 0, "تقویم قبلی پاک نشده است");
            Assert.IsTrue(list.Where(x => x.Month == 3).Count() == 4, "تعداد تقویم جدید نادرست است");

        }

        [Test]
        public void Insert_CalnendarYearTest()
        {
            IList<CalendarCellInfo> list = new List<CalendarCellInfo>();
            list.Add(new CalendarCellInfo() { Day = 1, Month = 3 });
            list.Add(new CalendarCellInfo() { Day = 2, Month = 3 });
            list.Add(new CalendarCellInfo() { Day = 3, Month = 3 });
            list.Add(new CalendarCellInfo() { Day = 4, Month = 3 });

            busCalendarType.InsertCalendars(ADOCalendarType1.ID, 1390, list);

            ClearSession();
            list = new List<CalendarCellInfo>();

            list = busCalendarType.GetCalendarList(1389, ADOCalendarType1.ID);
            Assert.AreEqual(3, list.Count, "تقویم پارسال نباید تغییر میکرد");
            Assert.IsTrue(list.Where(x => x.Month == 2).Count() == 3, "تقئیم پارسال نباید تغییر میکرد");

            list = busCalendarType.GetCalendarList(1390, ADOCalendarType1.ID);
            Assert.AreEqual(4, list.Count, "تقویم امسال اعمال نشده است");
            Assert.IsTrue(list.Where(x => x.Month == 2).Count() == 0);
            Assert.IsTrue(list.Where(x => x.Month == 3).Count() == 4, "تقویم امسال تعدادش نادرست است");

        }

        [Test]
        public void Update_Test()
        {
            try
            {
                calendarType_testObject.ID = ADOCalendarType1.ID;
                calendarType_testObject.Name = ADOCalendarType1.Name + "11";
                calendarType_testObject.CustomCode = ADOCalendarType1.CustomCode;
                busCalendarType.SaveChanges(calendarType_testObject, UIActionType.EDIT);
                calendarType_testObject = new CalendarType();
                ClearSession();
                calendarType_testObject = busCalendarType.GetByID(ADOCalendarType1.ID);
                Assert.AreEqual(ADOCalendarType1.Name + "11", calendarType_testObject.Name);
            }
            catch (Exception ex) 
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Update_EmtyNameTest()
        {
            try
            {
                calendarType_testObject.ID = ADOCalendarType1.ID;
                busCalendarType.SaveChanges(calendarType_testObject, UIActionType.EDIT);
                Assert.Fail("نام خالی است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.CalendarNameRequierd));
            }
        }

        [Test]
        public void Update_RepeatNameTest()
        {
            try
            {
                calendarType_testObject.ID = ADOCalendarType1.ID;
                calendarType_testObject.Name = ADOCalendarType2.Name;
                busCalendarType.SaveChanges(calendarType_testObject, UIActionType.EDIT);
                Assert.Fail("نام تکراری است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.CalendarNameRepeated));
            }
        }

        [Test]
        public void Update_EmtyCustomCodeTest()
        {
            try
            {
                calendarType_testObject.ID = ADOCalendarType1.ID;
                busCalendarType.SaveChanges(calendarType_testObject, UIActionType.EDIT);
                Assert.Fail("نام خالی است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.CalendarCustomCodeRequierd));
            }
        }

        [Test]
        public void Update_RepeatCustomCodeTest()
        {
            try
            {
                calendarType_testObject.ID = ADOCalendarType1.ID;
                calendarType_testObject.CustomCode = ADOCalendarType2.CustomCode;
                busCalendarType.SaveChanges(calendarType_testObject, UIActionType.EDIT);
                Assert.Fail("نام تکراری است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.CalendarCustomCodeRepeated));
            }
        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                busCalendarType.SaveChanges(ADOCalendarType1, UIActionType.DELETE);
                ClearSession();
                calendarType_testObject = busCalendarType.GetByID(ADOCalendarType1.ID);
            }
            catch (ItemNotExists ex) 
            {
                Assert.Pass("");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Delete_HolidayTEmplateExists() 
        {
            try
            {
                busCalendarType.SaveChanges(ADOCalendarType_Rasmi, UIActionType.DELETE);
                Assert.Fail("نباید بتوان این آیتم را خذف کرد");
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.CalendarTypeUsedInHolidayTemplates));
            }
        }

       
    }
}
