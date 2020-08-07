using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business;
using GTS.Clock.Business.Proxy;

namespace GTSTestUnit.Clock.Business
{    
    [TestFixture]
    public class WorkGroupDetailCalendarBusiness : BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_WorkGroupDetailTableAdapter wgdTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupDetailTableAdapter();
        DatabaseGatewayTableAdapters.TA_ShiftTableAdapter shiftTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ShiftTableAdapter();
        DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter wgTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter();

        BWorkGroupCalendar busCalendar;
        WorkGroupDetail ADOWorkGroupDetail = new WorkGroupDetail();
        Person ADOPerson = new Person();
        Shift ADOShift1 = new Shift();
        Shift ADOShift2 = new Shift();
        WorkGroup ADOWorkGroup = new WorkGroup();
        WorkGroupDetail wgd_testObject;
        DateTime date1, date2, date3, date4, date5, date6, date7, date8, date9, date10, date11;

        [SetUp]
        public void TestSetup()
        {          
            wgd_testObject = new WorkGroupDetail();
            busCalendar = new BWorkGroupCalendar(SysLanguageResource.Parsi);

            #region insert workgroup,shift,person
            BPerson bperson = new BPerson(SysLanguageResource.Parsi, LocalLanguageResource.Parsi);
            ADOPerson = new Person() { ID = bperson.CreateWorkingPerson2() };

            BWorkgroup bworkGroup = new BWorkgroup();
            ADOWorkGroup.CustomCode = "55-55";
            ADOWorkGroup.Name = "ClanderWorkGroupTest";
            bworkGroup.SaveChanges(ADOWorkGroup, UIActionType.ADD);

            BShift bshift = new BShift();
            ADOShift1.Name = "ClanderShiftTest1";
            ADOShift1.Person = ADOPerson;
            ADOShift1.Color = "0xff6512";
            ADOShift1.ShiftType = ShiftTypesEnum.WORK;
            bshift.SaveChanges(ADOShift1, UIActionType.ADD);

            ADOShift2.Name = "ClanderShiftTest2";
            ADOShift2.Color = "0xbbccaa";
            ADOShift2.Person = ADOPerson;
            ADOShift2.ShiftType = ShiftTypesEnum.WORK;
            bshift.SaveChanges(ADOShift2, UIActionType.ADD); 
            #endregion

            #region date inti
            date1 = Utility.ToMildiDate("1390/5/1");
            date2 = Utility.ToMildiDate("1390/5/2");
            date3 = Utility.ToMildiDate("1390/5/3");
            date4 = Utility.ToMildiDate("1390/5/5");
            date5 = Utility.ToMildiDate("1390/5/6");
            date6 = Utility.ToMildiDate("1390/5/7");
            date7 = Utility.ToMildiDate("1390/5/9");
            date8 = Utility.ToMildiDate("1390/5/10");
            date9 = Utility.ToMildiDate("1390/5/11");
            date10 = Utility.ToMildiDate("1389/1/1");
            date11 = Utility.ToMildiDate("1391/1/1");
            #endregion

            wgdTA.Insert(ADOWorkGroup.ID, ADOShift1.ID, date1);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift1.ID, date2);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift1.ID, date3);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift1.ID, date4);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift1.ID, date5);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift2.ID, date6);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift2.ID, date7);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift2.ID, date8);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift2.ID, date9);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift2.ID, date10);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift2.ID, date11);
            ClearSession();
        }

        [TearDown]
        public void TreatDown()
        {
            BPerson bperson = new BPerson(SysLanguageResource.Parsi, LocalLanguageResource.Parsi);
            BWorkgroup bworkGroup = new BWorkgroup();
            BShift bshift = new BShift();
            ClearSession();
            bworkGroup.SaveChanges(ADOWorkGroup, UIActionType.DELETE);
            bshift.SaveChanges(ADOShift1, UIActionType.DELETE);
            bshift.SaveChanges(ADOShift2, UIActionType.DELETE);
            bperson.SaveChanges(ADOPerson, UIActionType.DELETE);

        }

        [Test]
        public void GetAll_CountTest() 
        {
            IList<CalendarCellInfo> list = busCalendar.GetAll(ADOWorkGroup.ID, 1390);
            Assert.AreEqual(9, list.Count);
        }

        [Test]
        public void GetAll_DateTest()
        {
            IList<CalendarCellInfo> list = busCalendar.GetAll(ADOWorkGroup.ID, 1390);
            Assert.IsTrue(list.Where(x => x.Day == new PersianDateTime(date1).Day && x.Month == new PersianDateTime(date1).Month).Count() == 1);
        }

        [Test]
        public void Update_DublicateTest() 
        {
            try
            {
                IList<CalendarCellInfo> list = busCalendar.GetAll(ADOWorkGroup.ID, 1390);
                list[1].Day = list[0].Day;
                list[1].Month = list[0].Month;
                busCalendar.SaveChanges(list, ADOWorkGroup.ID, 1390);
                Assert.Fail("تاریخ تکراری نباید درج شود");
            }
            catch (UIValidationExceptions ex) 
            {
                ex.Exists(ExceptionResourceKeys.WorkGroupCalendarDublicateDate);
            }
        }

        [Test]
        public void Update_Test()
        {
            try
            {
                IList<CalendarCellInfo> list = busCalendar.GetAll(ADOWorkGroup.ID, 1390);
                list[1].Day = 15;
                list[1].Month = list[0].Month;
                busCalendar.SaveChanges(list, ADOWorkGroup.ID, 1390);
                ClearSession();
                list = busCalendar.GetAll(ADOWorkGroup.ID, 1390);
                Assert.IsTrue(list.Where(x => x.Day == 15 && x.Month == list[0].Month).Count() == 1);
                Assert.AreEqual(list.Count, 9);
                list = busCalendar.GetAll(ADOWorkGroup.ID, 1389);
                Assert.AreEqual(list.Count, 1);
                list = busCalendar.GetAll(ADOWorkGroup.ID, 1391);
                Assert.AreEqual(list.Count, 1);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void RepeatTest() 
        {
            IList<CalendarCellInfo> list = busCalendar.GetAll(ADOWorkGroup.ID, 1390);
            ClearSession();
            IList<decimal> holiday = new List<decimal>();
            holiday.Add(1); holiday.Add(180);
            IList<CalendarCellInfo> list2 = busCalendar.RepetitionPeriod(1390, 5, 1, 5, 5, null, list);
            Assert.AreEqual(0, list2.Where(x => x.Month == 5 && x.Day == 4).Count());
            Assert.AreEqual(0, list2.Where(x => x.Month == 5 && x.Day == 9).Count());
            Assert.AreEqual(1, list2.Where(x => x.Month == 5 && x.Day == 8).Count());
            Assert.AreEqual(1, list2.Where(x => x.Month == 5 && x.Day == 10).Count());

        }

        [Test]
        public void Test_2222222() 
        {
           
        }
    }
}
