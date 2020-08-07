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
using BaseInfo = GTS.Clock.Model.BaseInformation;

namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 2011-12-14 3:31:05 PM
    /// write your name here
    /// </summary>
    [TestFixture]
    public class ClockBusinessTest : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_ClockTableAdapter clockTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ClockTableAdapter();
        DatabaseGatewayTableAdapters.TA_ClockTypeTableAdapter clockTypeTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ClockTypeTableAdapter();

        BClock busClock;
        BaseInfo.Clock clock_testObject;
        BaseInfo.Clock ADOClock1 = new BaseInfo.Clock();
        BaseInfo.Clock ADOClock2 = new BaseInfo.Clock();

        [SetUp]
        public void TestSetup()
        {
            busClock = new BClock();
            clock_testObject = new global::GTS.Clock.Model.BaseInformation.Clock();

            clockTypeTA.Insert("00-0Test1", "TestClockType1");
            decimal? clockTypeId = clockTypeTA.GetLastId();

            decimal? stationId = ADOStaion1.ID;

            clockTA.Insert("TestClock1", "00-0Test1", clockTypeId, stationId, "", true);
            DatabaseGateway.TA_ClockDataTable clockTable = clockTA.GetDataByCode("00-0Test1");
            DatabaseGateway.TA_ClockRow row = clockTable.Rows[0] as DatabaseGateway.TA_ClockRow;
            ADOClock1.ID = row.clock_ID;
            ADOClock1.Name = row.clock_Name;
            ADOClock1.Tel = row.clock_Tel;
            ADOClock1.CustomCode = row.clock_CustomCode;
            ADOClock1.Station = new global::GTS.Clock.Model.BaseInformation.ControlStation() { ID = (decimal)stationId };
            ADOClock1.Clocktype = new global::GTS.Clock.Model.BaseInformation.ClockType() { ID = (decimal)clockTypeId };

            clockTA.Insert("TestClock2", "00-0Test2", clockTypeId, stationId, "", true);
            clockTable = clockTA.GetDataByCode("00-0Test2");
            row = clockTable.Rows[0] as DatabaseGateway.TA_ClockRow;
            ADOClock2.ID = row.clock_ID;
            ADOClock2.Name = row.clock_Name;
            ADOClock2.Tel = row.clock_Tel;
            ADOClock2.CustomCode = row.clock_CustomCode;
            ADOClock2.Station = new global::GTS.Clock.Model.BaseInformation.ControlStation() { ID = (decimal)stationId };
            ADOClock2.Clocktype = new global::GTS.Clock.Model.BaseInformation.ClockType() { ID = (decimal)clockTypeId };
        }

        [TearDown]
        public void TreatDown()
        {            
            clockTA.DeleteByCode("00-0Test1");
            clockTA.DeleteByCode("00-0Test2");
            clockTA.DeleteByCode("00-0Test3");
            clockTypeTA.DeleteByCode("00-0Test1");
        }

        [Test]
        public void GetById_Test()
        {
            clock_testObject = busClock.GetByID(ADOClock1.ID);
            ClearSession();
            Assert.AreEqual(ADOClock1.Name, clock_testObject.Name);
        }

        [Test]
        public void Insert_Test()
        {
            clock_testObject.Name = "TestClock3";
            clock_testObject.CustomCode = "00-0Test3";
            clock_testObject.Station = ADOClock1.Station;
            clock_testObject.Clocktype = ADOClock1.Clocktype;
            busClock.SaveChanges(clock_testObject, UIActionType.ADD);
            ClearSession();
            Assert.IsTrue(clock_testObject.ID > 0);

        }

        [Test]
        public void Insert_EmptyNameTest() 
        {
            try
            {
                busClock.SaveChanges(clock_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ClockNameRequierd));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ClockControStationRequierd));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ClockTypeRequierd));
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.ClockCustomCodeRepeated));
            }
        }

        [Test]
        public void Insert_RepeatNameTest()
        {
            try
            {
                clock_testObject.Name = ADOClock1.Name;
                busClock.SaveChanges(clock_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ClockNameRepeated));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ClockControStationRequierd));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ClockTypeRequierd));
            }
        }

        [Test]
        public void Update_Test()
        {
            busClock.SaveChanges(ADOClock1, UIActionType.EDIT);
        }

        [Test]
        public void Update_EmptyNameTest()
        {
            try
            {
                clock_testObject.ID = ADOClock1.ID;
                busClock.SaveChanges(clock_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ClockNameRequierd));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ClockControStationRequierd));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ClockTypeRequierd));
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.ClockCustomCodeRepeated));
            }
        }

        [Test]
        public void UpdateRepeatNameTest()
        {
            try
            {
                clock_testObject.ID = ADOClock1.ID;
                clock_testObject.Name = ADOClock1.Name;
                busClock.SaveChanges(clock_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ClockNameRepeated));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ClockControStationRequierd));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.ClockTypeRequierd));
            }
        }


        [Test]
        public void Delete_Test()
        {
            try
            {
                busClock.SaveChanges(ADOClock1, UIActionType.DELETE);
                ClearSession();
                clock_testObject = busClock.GetByID(ADOClock1.ID);
                Assert.Fail();
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass();
            }
        }
    }
}
