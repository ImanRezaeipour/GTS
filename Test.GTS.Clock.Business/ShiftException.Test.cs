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


namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 4/14/2012 5:16:05 PM
    /// write your name here
    /// </summary>
    [TestFixture]
    public class ShiftExceptionTest : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_ExceptionShiftTableAdapter exShiftTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ExceptionShiftTableAdapter();
        DatabaseGatewayTableAdapters.TA_ShiftTableAdapter shiftTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ShiftTableAdapter();
        DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter wgTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_WorkGroupDetailTableAdapter wgdTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupDetailTableAdapter();
        DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter asgWrgTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter();

        Shift ADOShift1 = new Shift();
        Shift ADOShift2 = new Shift();
        WorkGroup ADOWorkGroup = new WorkGroup();
        WorkGroup ADOWorkGroup2 = new WorkGroup();
        ShiftException ADOExShift = new ShiftException();
        BExceptionShift busExceptionShift;
        ShiftException shiftEx_testobject;
        [SetUp]
        public void TestSetup()
        {
            busExceptionShift = new BExceptionShift();
            shiftEx_testobject = new ShiftException();

            #region Shift
            shiftTA.Insert("ShiftTest1", 1, 11, null, 100, false, false, false, "2", "0-00");
            shiftTA.Insert("ShiftTest2", 1, 11, null, 100, false, false, false, "2", "0-00");
            ADOShift1 = new Shift();

            DatabaseGateway.TA_ShiftDataTable shiftTable = new DatabaseGateway.TA_ShiftDataTable();
            shiftTA.FillByName(shiftTable, "ShiftTest1");
            ADOShift1.ID = Convert.ToInt32(shiftTable.Rows[0]["shift_ID"]);
            shiftTA.FillByName(shiftTable, "ShiftTest2");
            ADOShift2.ID = Convert.ToInt32(shiftTable.Rows[0]["shift_ID"]);
            #endregion

            #region WorkGroup & WorkGroup Detail & Assignment
            BWorkgroup bworkGroup = new BWorkgroup();
            ADOWorkGroup.CustomCode = "00-00";
            ADOWorkGroup.Name = "TestWorkGroup";
            bworkGroup.SaveChanges(ADOWorkGroup, UIActionType.ADD);
            
            ADOWorkGroup2.CustomCode = "00-01";
            ADOWorkGroup2.Name = "TestWorkGroup2";
            bworkGroup.SaveChanges(ADOWorkGroup2, UIActionType.ADD);

            wgdTA.Insert(ADOWorkGroup.ID, ADOShift1.ID, Utility.ToMildiDate("1390/10/01"));
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift2.ID, Utility.ToMildiDate("1390/10/03"));
            wgdTA.Insert(ADOWorkGroup2.ID, ADOShift2.ID, Utility.ToMildiDate("1390/10/01"));

            asgWrgTA.Insert(ADOWorkGroup.ID, ADOPerson2.ID, Utility.ToMildiDate("1390/09/15"));
            asgWrgTA.Insert(ADOWorkGroup2.ID, ADOPerson1.ID, Utility.ToMildiDate("1390/09/15"));
            #endregion
            exShiftTA.Insert(ADOPerson1.ID, ADOShift1.ID, Utility.ToMildiDate("1390/10/10"), ADOUser1.ID, DateTime.Now, "");
            DatabaseGateway.TA_ExceptionShiftDataTable exShiftTable = exShiftTA.GetDataByPersonId(ADOPerson1.ID);
            ADOExShift.ID = Convert.ToDecimal(exShiftTable.Rows[0]["ExceptionShift_ID"]);
            

        }

        [TearDown]
        public void TreatDown()
        {
            wgTA.DeleteByCustomCode("00-00");
            wgTA.DeleteByCustomCode("00-01");
            shiftTA.DeleteByCustomCode("0-00");
            exShiftTA.DeleteByPersonId(ADOPerson1.ID);
            exShiftTA.DeleteByPersonId(ADOPerson2.ID);
        }

        [Test]
        public void GetById_Test()
        {
            shiftEx_testobject = busExceptionShift.GetByID(ADOExShift.ID);
            Assert.AreEqual(ADOExShift.ID, shiftEx_testobject.ID);
        }

        [Test]
        public void Insert_Test()
        {
            shiftEx_testobject.Person = new Person() { ID = ADOPerson2.ID };
            shiftEx_testobject.Date = DateTime.Now;
            shiftEx_testobject.Shift = new Shift() { ID = ADOShift1.ID };
            shiftEx_testobject.UserID = ADOUser1.ID;
            busExceptionShift.SaveChanges(shiftEx_testobject, UIActionType.ADD);
            ClearSession();
            Assert.IsTrue(shiftEx_testobject.ID > 0);
        }

        [Test]
        public void Update_Test()
        {
            shiftEx_testobject.ID = ADOExShift.ID;
            shiftEx_testobject.Person = new Person() { ID = ADOPerson2.ID };
            shiftEx_testobject.Date = DateTime.Now;
            shiftEx_testobject.Shift = new Shift() { ID = ADOShift1.ID };
            shiftEx_testobject.UserID = ADOUser1.ID;
            busExceptionShift.SaveChanges(shiftEx_testobject, UIActionType.EDIT);
            ClearSession();
            shiftEx_testobject = busExceptionShift.GetByID(ADOExShift.ID);
            Assert.AreEqual(ADOPerson2.ID, shiftEx_testobject.Person.ID);            
        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                busExceptionShift.DeleteExceptionShift(ADOExShift.ID);
                ClearSession();
                shiftEx_testobject = busExceptionShift.GetByID(ADOExShift.ID);
                Assert.Fail();
            }
            catch (ItemNotExists ex) 
            {
                Assert.Pass();
            }
        }

        [Test]
        public void GetShiftList_Test()
        {
            IList<ShiftException> l = busExceptionShift.GetExceptionShiftList(ADOPerson1.ID, "1390/10/05", "1390/10/11");
            Assert.IsTrue(l.Where(x => x.ID == ADOExShift.ID).Count() == 1);
        }

        [Test]
        public void InsertByPerson_Test() 
        {
            busExceptionShift.InsertByPerson(ADOPerson1.ID, ADOShift1.ID, "1390/10/08", "1390/10/12");
            ClearSession();
            IList<ShiftException> l = busExceptionShift.GetExceptionShiftList(ADOPerson1.ID, "1390/10/05", "1390/10/15");
            Assert.AreEqual(5, l.Count);
            Assert.IsTrue(l.Where(x => x.Date == Utility.ToMildiDate("1390/10/08")).Count() == 1);
            Assert.IsTrue(l.Where(x => x.Date == Utility.ToMildiDate("1390/10/09")).Count() == 1);
            Assert.IsTrue(l.Where(x => x.Date == Utility.ToMildiDate("1390/10/10")).Count() == 1);
            Assert.IsTrue(l.Where(x => x.Date == Utility.ToMildiDate("1390/10/11")).Count() == 1);
            Assert.IsTrue(l.Where(x => x.Date == Utility.ToMildiDate("1390/10/12")).Count() == 1);
        }

        #region ExchangeDayByPerson
      
        [Test]
        public void ExchangeDayByPerson_NullShiftTest()
        {
            busExceptionShift.ExchangeDayByPerson(ADOPerson2.ID, "1390/10/02", "1390/10/03");
            ClearSession();
            IList<ShiftException> l = busExceptionShift.GetExceptionShiftList(ADOPerson2.ID, "1390/10/01", "1390/10/03");
            Assert.AreEqual(2, l.Count);
            Assert.AreEqual(0, l.Where(x => x.TheDate == "1390/10/03").First().Shift.ID);
            Assert.AreEqual(ADOShift2.ID, l.Where(x => x.TheDate == "1390/10/02").First().Shift.ID);
        }

        [Test]
        public void ExchangeDayByPerson_Test()
        {
            busExceptionShift.ExchangeDayByPerson(ADOPerson2.ID, "1390/10/01", "1390/10/03");
            ClearSession();
            IList<ShiftException> l = busExceptionShift.GetExceptionShiftList(ADOPerson2.ID, "1390/10/01", "1390/10/03");
            Assert.AreEqual(2, l.Count);
            Assert.AreEqual(ADOShift2.ID, l.Where(x => x.TheDate == "1390/10/01").First().Shift.ID);
            Assert.AreEqual(ADOShift1.ID, l.Where(x => x.TheDate == "1390/10/03").First().Shift.ID);
        }

        [Test]
        public void ExchangeDayByPerson_DeleteExsitingExShiftTest()
        {
            busExceptionShift.ExchangeDayByPerson(ADOPerson1.ID, "1390/10/03", "1390/10/10");
            ClearSession();
            IList<ShiftException> l = busExceptionShift.GetExceptionShiftList(ADOPerson1.ID, "1390/10/03", "1390/10/10");
            Assert.AreEqual(2, l.Count);
            Assert.IsTrue(l.Where(x => x.ID == ADOExShift.ID).Count() == 0);
        }
        
        #endregion

        #region ExchangeDayByWorkGroup
       
        [Test]
        public void ExchangeDayByWorkGroup_Test()
        {
            busExceptionShift.ExchangeDayByWorkGroup(ADOWorkGroup.ID, "1390/10/01", "1390/10/03");
            ClearSession();
            IList<ShiftException> l = busExceptionShift.GetExceptionShiftList(ADOPerson2.ID, "1390/10/01", "1390/10/03");
            Assert.AreEqual(2, l.Count);
            Assert.AreEqual(ADOShift2.ID, l.Where(x => x.TheDate == "1390/10/01").First().Shift.ID);
            Assert.AreEqual(ADOShift1.ID, l.Where(x => x.TheDate == "1390/10/03").First().Shift.ID);
        }
       
        #endregion

        #region ExchangePerson
        [Test]
        public void ExchangePerson_Test1()
        {
            busExceptionShift.ExchangePerson(ADOPerson1.ID, ADOPerson2.ID, "1390/10/01", "1390/10/01");
            ClearSession();
            IList<ShiftException> l = busExceptionShift.GetExceptionShiftList(ADOPerson1.ID, "1390/10/01", "1390/10/02");
            Assert.AreEqual(1, l.Count);
            Assert.AreEqual(ADOShift1.ID, l[0].Shift.ID);
            ClearSession();
            l = busExceptionShift.GetExceptionShiftList(ADOPerson2.ID, "1390/10/01", "1390/10/02");
            Assert.AreEqual(1, l.Count);
            Assert.AreEqual(ADOShift2.ID, l[0].Shift.ID);
        }

        [Test]
        public void ExchangePerson_Test2()
        {
            busExceptionShift.ExchangePerson(ADOPerson1.ID, ADOPerson2.ID, "1390/10/01", "1390/10/03");
            ClearSession();
            IList<ShiftException> l = busExceptionShift.GetExceptionShiftList(ADOPerson1.ID, "1390/10/01", "1390/10/03");
            Assert.AreEqual(1, l.Count);
            Assert.AreEqual(ADOShift2.ID, l[0].Shift.ID);
            ClearSession();
            l = busExceptionShift.GetExceptionShiftList(ADOPerson2.ID, "1390/10/01", "1390/10/03");
            Assert.AreEqual(1, l.Count);
            Assert.AreEqual(ADOShift2.ID, l[0].Shift.ID);
        } 
        #endregion

        #region ExchangeWorkGroup

        [Test]
        public void ExchangeWorkGroup_Test()
        {
            busExceptionShift.ExchangeWorkGroup(ADOWorkGroup2.ID, ADOWorkGroup.ID, "1390/10/01", "1390/10/01");
            ClearSession();
            IList<ShiftException> l = busExceptionShift.GetExceptionShiftList(ADOPerson1.ID, "1390/10/01", "1390/10/02");
            Assert.AreEqual(1, l.Count);
            Assert.AreEqual(ADOShift1.ID, l[0].Shift.ID);
            ClearSession();
            l = busExceptionShift.GetExceptionShiftList(ADOPerson2.ID, "1390/10/01", "1390/10/02");
            Assert.AreEqual(1, l.Count);
            Assert.AreEqual(ADOShift2.ID, l[0].Shift.ID);
        }

        #endregion

        [Test]
        public void Test_22222222() 
        {
            busExceptionShift.ExchangeWorkGroup(14973, 1047, "1390/01/01", "1390/01/20");
            ClearSession();
            busExceptionShift.GetDayShiftByWorkGroup(14973, "1390/01/01");
        }
    }
}
