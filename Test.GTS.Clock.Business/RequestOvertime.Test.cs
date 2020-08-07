using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business.RequestFlow;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class RequestOvertimeTest : BaseFixture
    {
        #region Table Adapters
        DatasetGatewayWorkFlowTableAdapters.TA_RequestTableAdapter requestTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_RequestTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter precardGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter precardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_DoctorTableAdapter doctorTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_DoctorTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_IllnessTableAdapter illnessTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_IllnessTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_DutyPlaceTableAdapter dutyPlcTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_DutyPlaceTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_RequestDetailTableAdapter requestDetailTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_RequestDetailTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_RequestStatusTableAdapter requestStatusTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_RequestStatusTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter managerTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter flowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_ManagerFlowTableAdapter mangFlowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerFlowTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter accessGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_WorkGroupDetailTableAdapter wgdTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupDetailTableAdapter();
        DatabaseGatewayTableAdapters.TA_ShiftTableAdapter shiftTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ShiftTableAdapter();
        DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter wgTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter assingWorkGrouTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter();
        #endregion

        #region ADOObjects
        IOverTimeBRequest busOverTime = new BRequest();
        Request request_testObject = new Request();
        Request ADORequestOvertime1 = new Request();
    
        PrecardGroups ADOPrecardGroup1 = new PrecardGroups();
        Precard ADOPrecardOverTime1 = new Precard();
        Precard ADOPrecardDasturyOverTime = new Precard();
        Manager ADOManager1 = new Manager();
        Flow ADOFlow1 = new Flow();
        ManagerFlow ADOManagerFlow1 = new ManagerFlow();
        PrecardAccessGroup ADOAccessGroup1 = new PrecardAccessGroup();

        WorkGroupDetail ADOWorkGroupDetail = new WorkGroupDetail();      
        Shift ADOShift1 = new Shift();
        Shift ADOShift2 = new Shift();
        WorkGroup ADOWorkGroup = new WorkGroup();
        WorkGroupDetail wgd_testObject;
        DateTime date1, date2, date3, date4, date5, date6, date7;
        #endregion

        [SetUp]
        public void TestSetup()
        {
           

            #region precards

            DatasetGatewayWorkFlow.TA_PrecardGroupsDataTable precardTable = new DatasetGatewayWorkFlow.TA_PrecardGroupsDataTable();
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.overwork.ToString());
            ADOPrecardGroup1.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup1.Name = "OwerWork";


            precardTA.Insert("TestPrecard1", true, ADOPrecardGroup1.ID, true, false, true, "99999999", false);

            DatasetGatewayWorkFlow.TA_PrecardDataTable pTable = new DatasetGatewayWorkFlow.TA_PrecardDataTable();
            pTable = precardTA.GetDataByName("TestPrecard1");
            ADOPrecardOverTime1.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardOverTime1.Name = "TestPrecard1";

            pTable = precardTA.GetDataByCode(126.ToString());
            ADOPrecardDasturyOverTime.ID = Convert.ToInt32(pTable.Rows[0][0]);
            #endregion

            requestTA.Insert(ADOPrecardOverTime1.ID, ADOPerson1.ID, new DateTime(2010, 5, 1), new DateTime(2010, 5, 1), 420, 600, "", DateTime.Now, ADOUser1.ID);

            DatasetGatewayWorkFlow.TA_RequestDataTable requestTable = new DatasetGatewayWorkFlow.TA_RequestDataTable();
            requestTable = requestTA.GetDataByPersonId(ADOPerson1.ID);
            ADORequestOvertime1.ID = Convert.ToInt32(requestTable.Rows[0][0]);


            #region Manager Flow

            managerTA.Insert(ADOPerson1.ID, null);

            DatasetGatewayWorkFlow.TA_ManagerDataTable managetTable = new DatasetGatewayWorkFlow.TA_ManagerDataTable();
            managerTA.FillByPersonID(managetTable, ADOPerson1.ID);
            ADOManager1.ID = Convert.ToInt32(managetTable.Rows[0]["MasterMng_ID"]);
            ADOManager1.Person = ADOPerson1;

            accessGroupTA.Insert("AccessGroup1_2");
            DatasetGatewayWorkFlow.TA_PrecardAccessGroupDataTable accessTable = accessGroupTA.GetDataBy1("AccessGroup1_2");
            ADOAccessGroup1.ID = Convert.ToInt32(accessTable.Rows[0][0]);
            ADOAccessGroup1.Name = "AccessGroup1_2";

            flowTA.Insert(ADOAccessGroup1.ID, false, false, "FlowTest");
            DatasetGatewayWorkFlow.TA_FlowDataTable mangTAble = flowTA.GetDataByName("FlowTest");
            ADOFlow1.ID = Convert.ToInt32(mangTAble.Rows[0][0]);
            ADOFlow1.FlowName = "FlowTest";
            ADOFlow1.ActiveFlow = false;
            ADOFlow1.WorkFlow = false;

            mangFlowTA.Insert(ADOManager1.ID, 1, ADOFlow1.ID, true);

            DatasetGatewayWorkFlow.TA_ManagerFlowDataTable nbgFlowTable = mangFlowTA.GetDataByFlowID(ADOFlow1.ID);
            ADOManagerFlow1.ID = Convert.ToInt32(nbgFlowTable.Rows[0]["mngrFlow_ID"]);
            ADOManagerFlow1.Level = Convert.ToInt32(nbgFlowTable.Rows[0]["mngrFlow_LEVEL"]);

            #endregion

            requestStatusTA.Insert(ADOManagerFlow1.ID, ADORequestOvertime1.ID, true, false, "", DateTime.Now, false);

            request_testObject = new Request();
            busOverTime = new BRequest(ADOPerson1.ID);

            #region insert workgroup,shift
            BWorkgroup bworkGroup = new BWorkgroup();
            ADOWorkGroup.CustomCode = "55-55";
            ADOWorkGroup.Name = "ClanderWorkGroupTest";
            bworkGroup.SaveChanges(ADOWorkGroup, UIActionType.ADD);

            assingWorkGrouTA.Insert(ADOWorkGroup.ID, ADOPerson1.ID, new DateTime(2000, 1, 1));

            BShift bshift = new BShift();
            ADOShift1.Name = "ClanderShiftTest1";
            ADOShift1.Person = ADOPerson1;
            ADOShift1.Color = "0xff6512";
            ADOShift1.ShiftType = ShiftTypesEnum.WORK;
            ADOShift1.CustomCode = "55-54";
            bshift.SaveChanges(ADOShift1, UIActionType.ADD);
            ShiftPair pair1 = new ShiftPair(100, 200) { ShiftId = ADOShift1.ID };
            ShiftPair pair2= new ShiftPair(200, 400) { ShiftId = ADOShift1.ID };
            bshift.SaveChangesShiftPair(pair1, UIActionType.ADD);
            bshift.SaveChangesShiftPair(pair2, UIActionType.ADD);

            ADOShift2.Name = "ClanderShiftTest2";
            ADOShift2.Color = "0xbbccaa";
            ADOShift2.Person = ADOPerson1;
            ADOShift2.ShiftType = ShiftTypesEnum.WORK;
            ADOShift2.CustomCode = "55-55";
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
         
            #endregion

            #region WorkGroup Detail
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift1.ID, date1);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift1.ID, date2);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift1.ID, date3);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift1.ID, date4);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift1.ID, date5);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift2.ID, date6);
            wgdTA.Insert(ADOWorkGroup.ID, ADOShift2.ID, date7); 
            #endregion

            ClearSession();
        }

        [TearDown]
        public void TreatDown()
        {
            wgTA.DeleteByCustomCode("55-55");
            shiftTA.DeleteByCustomCode("55-54");
            shiftTA.DeleteByCustomCode("55-55");
            flowTA.DeleteByName("FlowTest");
            managerTA.DeleteByBarcode("00001");
            accessGroupTA.DeleteByName("AccessGroup1_2");
            doctorTA.DeleteByLastName("TestDoctorLastName1");
            illnessTA.DeleteByName("TestIllness1");
            requestTA.DeleteByPerson(ADOPerson1.ID);
            requestTA.DeleteByPerson(ADOPerson2.ID);
            precardTA.DeleteByID("99999999");         
            dutyPlcTA.DeleteByName("TestDutyPlc1");
            dutyPlcTA.DeleteByName("TestDutyPlc2");

            BPerson bperson = new BPerson(SysLanguageResource.Parsi, LocalLanguageResource.Parsi);
            BWorkgroup bworkGroup = new BWorkgroup();
            BShift bshift = new BShift();
            ClearSession();
            assingWorkGrouTA.DeleteByPerson(ADOPerson1.ID);
            //bshift.SaveChanges(ADOShift1, UIActionType.DELETE);
            //bshift.SaveChanges(ADOShift2, UIActionType.DELETE);
        }

        [Test]
        public void GetAllOverWorkPrecard_Test()
        {
            IList<Precard> list = busOverTime.GetAllOverWorks();
            Assert.IsTrue(list.Where(x => x.ID == ADOPrecardOverTime1.ID).Count() > 0);
        }

        [Test]
        public void GetAllOverworkRequests_Test()
        {            
            try
            {
                request_testObject.TheFromTime = "15:00";
                request_testObject.TheToTime = "17:00";
                request_testObject.TheTimeDuration = "05:00";
                request_testObject.TheFromDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.TheToDate = Utility.ToPersianDate(new DateTime(2010, 5, 3));
                request_testObject.IsDateSetByUser = true;
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardOverTime1.ID };
                request_testObject.RegisterDate = DateTime.Now;

                busOverTime.InsertRequest(request_testObject);
                
                ClearSession();
                decimal id = request_testObject.ID;
                busOverTime = new BRequest(ADOPerson1.ID);
                IList<Request> list = busOverTime.GetAllOverTimeRequests("2010/5/1");
                Assert.IsTrue(list.Count > 0, "2010/5/1 Count");
                request_testObject = list.Where(x => x.ID == id).First();
                Assert.IsNotNullOrEmpty(request_testObject.TheFromDate, "TheFromDate");
                Assert.IsNotNullOrEmpty(request_testObject.TheToDate, "TheToDate");
                Assert.IsNotNullOrEmpty(request_testObject.TheToTime, "TheToTime");
                Assert.IsNotNullOrEmpty(request_testObject.TheFromTime, "TheFromTime");
                Assert.IsNotNullOrEmpty(request_testObject.TheTimeDuration, "TheTimeDuration");

                list = busOverTime.GetAllOverTimeRequests("2010/5/2");
                Assert.IsTrue(list.Count > 0, "2010/5/2 Count");
                request_testObject = list.Where(x => x.ID == id).First();
                Assert.IsNotNullOrEmpty(request_testObject.TheFromDate, "TheFromDate");
                Assert.IsNotNullOrEmpty(request_testObject.TheToDate, "TheToDate");
                Assert.IsNotNullOrEmpty(request_testObject.TheToTime, "TheToTime");
                Assert.IsNotNullOrEmpty(request_testObject.TheFromTime, "TheFromTime");
                Assert.IsNotNullOrEmpty(request_testObject.TheTimeDuration, "TheTimeDuration");

                list = busOverTime.GetAllOverTimeRequests("2010/5/3");
                Assert.IsTrue(list.Count > 0, "2010/5/3 Count");
                request_testObject = list.Where(x => x.ID == id).First();
                Assert.IsNotNullOrEmpty(request_testObject.TheFromDate, "TheFromDate");
                Assert.IsNotNullOrEmpty(request_testObject.TheToDate, "TheToDate");
                Assert.IsNotNullOrEmpty(request_testObject.TheToTime, "TheToTime");
                Assert.IsNotNullOrEmpty(request_testObject.TheFromTime, "TheFromTime");
                Assert.IsNotNullOrEmpty(request_testObject.TheTimeDuration, "TheTimeDuration");

            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetAllShifts_Test()
        {
            IList<ShiftProxy> list = busOverTime.GetAllShifts(Utility.ToMildiDate("1390/5/5"));
            Assert.AreEqual(5, list.Count);
            Assert.AreEqual("1390/05/02", list[0].Date);
            IList<ShiftPairProxy> pairs = busOverTime.GetShiftDetail(list[0].ShiftID);
            Assert.AreEqual(2, pairs.Count);

        }

        [Test]
        public void Insert_TimeValidateGreaterTest()
        {
            try
            {
                request_testObject.TheFromTime = "15:00";
                request_testObject.TheToTime = "10:00";
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 1));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 1));
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardOverTime1.ID };
                request_testObject.RegisterDate = DateTime.Now;
                //request_testObject.User = new User() { ID = ADOUser1.ID };

                busOverTime.InsertRequest(request_testObject);
                Assert.Fail("زمان نامعتبر است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RequestFromTimeGreaterThanToTime));
            }
        }

        [Test]
        public void Insert_PrecardValidateTest()
        {
            try
            {
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 2));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 1));
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard();
                request_testObject.RegisterDate = DateTime.Now;
                //request_testObject.User = new User() { ID = ADOUser1.ID };

                busOverTime.InsertRequest(request_testObject);
                Assert.Fail("پیشکارت نامعتبر است");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RequestPrecardIsEmpty));
            }
        }

        [Test]
        public void Insert_EmptyPrecardTest()
        {
            try
            {
                busOverTime = new BRequest(ADOPerson2.ID);
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 12));
                request_testObject.TheFromTime = "08:00";
                request_testObject.TheToTime = "10:00";
                request_testObject.Precard = new Precard() { ID = -1 };

                busOverTime.InsertRequest(request_testObject);
                ClearSession();

                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.AreEqual("0", request_testObject.Precard.Code);
                Assert.IsTrue(request_testObject.User.ID > 0);

                busOverTime.DeleteRequest(request_testObject);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_TimeDurationTest() 
        {
            try
            {
                request_testObject.TheFromTime = "15:00";
                request_testObject.TheToTime = "17:00";
                request_testObject.TheTimeDuration = "05:00";
                request_testObject.TheFromDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.TheToDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.IsDateSetByUser = true;
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardOverTime1.ID };
                request_testObject.RegisterDate = DateTime.Now;

                busOverTime.InsertRequest(request_testObject);
                ClearSession();
                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.AreEqual(300, request_testObject.TimeDuration);          
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_TimeDurationZeroTest()
        {
            try
            {
                request_testObject.TheFromTime = "15:00";
                request_testObject.TheToTime = "17:00";
                request_testObject.TheTimeDuration = "00:00";
                request_testObject.TheFromDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.TheToDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.IsDateSetByUser = true;                
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardOverTime1.ID };
                request_testObject.RegisterDate = DateTime.Now;

                busOverTime.InsertRequest(request_testObject);
                ClearSession();
                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.AreEqual(0, request_testObject.TimeDuration);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_EmptyFromToNotTimeDurationTest()
        {
            try
            {
                request_testObject.TheFromTime = "00:00";
                request_testObject.TheToTime = "00:00";
                request_testObject.TheTimeDuration = "05:00";
                request_testObject.TheFromDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.TheToDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.IsDateSetByUser = true;
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardOverTime1.ID };
                request_testObject.RegisterDate = DateTime.Now;

                busOverTime.InsertRequest(request_testObject);
                ClearSession();
                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.AreEqual(300, request_testObject.TimeDuration);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_EmptyTimeZeroTest1()
        {
            try
            {
                request_testObject.TheFromTime = "00:00";
                request_testObject.TheToTime = "00:00";
                request_testObject.TheTimeDuration = "00:00";
                request_testObject.TheFromDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.TheToDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.IsDateSetByUser = true;
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardOverTime1.ID };
                request_testObject.RegisterDate = DateTime.Now;

                busOverTime.InsertRequest(request_testObject);
                ClearSession();
                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.Fail("زمان وارد نشده");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RequestTimeIsNotValid));
            }
        }

        [Test]
        public void Insert_EmptyTimeZeroTest2()
        {
            try
            {
                request_testObject.TheFromTime = "00:00";
                request_testObject.TheToTime = "00:00";
                request_testObject.TheFromDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.TheToDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.IsDateSetByUser = true;
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardOverTime1.ID };
                request_testObject.RegisterDate = DateTime.Now;

                busOverTime.InsertRequest(request_testObject);
                ClearSession();
                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.Fail("زمان وارد نشده");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RequestTimeIsNotValid));
            }
        }

        [Test]
        public void Insert_Test()
        {
            try
            {
                request_testObject.TheFromTime = "15:00";
                request_testObject.TheToTime = "17:00";
                request_testObject.TheTimeDuration = "05:00";
                request_testObject.TheFromDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.TheToDate = Utility.ToPersianDate(new DateTime(2010, 5, 2));
                request_testObject.IsDateSetByUser = true;
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardOverTime1.ID };
                request_testObject.RegisterDate = DateTime.Now;

                busOverTime.InsertRequest(request_testObject);
                ClearSession();
                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.AreEqual(new DateTime(2010, 5, 1), request_testObject.FromDate, "FromDate");
                Assert.AreEqual(new DateTime(2010, 5, 2), request_testObject.ToDate, "ToDate");
                Assert.AreEqual(1020, request_testObject.ToTime, "ToTime");
                Assert.AreEqual(900, request_testObject.FromTime, "FromTime");
                Assert.AreEqual(300, request_testObject.TimeDuration, "TimeDuration");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_DasturyTest()
        {
            try
            {
                request_testObject.TheFromTime = "00:00";
                request_testObject.TheToTime = "00:00";
                request_testObject.TheTimeDuration = "05:00";
                request_testObject.TheFromDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.TheToDate = Utility.ToPersianDate(new DateTime(2010, 5, 1));
                request_testObject.IsDateSetByUser = true;
                request_testObject.Person = new Person() { ID = ADOPerson1.ID };
                request_testObject.Precard = new Precard() { ID = ADOPrecardDasturyOverTime.ID };
                request_testObject.RegisterDate = DateTime.Now;

                busOverTime.InsertRequest(request_testObject);
                ClearSession();
                request_testObject = new BRequest().GetByID(request_testObject.ID);
                Assert.AreEqual(new DateTime(2010, 5, 1), request_testObject.FromDate, "FromDate");
                Assert.AreEqual(new DateTime(2010, 5, 1), request_testObject.ToDate, "ToDate");              
                Assert.AreEqual(300, request_testObject.TimeDuration, "TimeDuration");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
        }

    }
}
