using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit;
using System.Web;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Rules;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class PersonMonthlyWorkedTimeTest : BaseFixture
    {
        #region date range variables
        DatabaseGatewayTableAdapters.TA_PersonRangeAssignmentTableAdapter assinTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonRangeAssignmentTableAdapter();
        DatabaseGatewayTableAdapters.TA_CalculationRangeGroupTableAdapter groupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_CalculationRangeGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_CalculationDateRangeTableAdapter dateRangeTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_CalculationDateRangeTableAdapter();
        DatabaseGatewayTableAdapters.TA_ConceptTemplateTableAdapter conceptTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ConceptTemplateTableAdapter();


        CalculationRangeGroup ADOGroup = new CalculationRangeGroup();
        CalculationDateRange ADODateRange1 = new CalculationDateRange();
        CalculationDateRange ADODateRange2 = new CalculationDateRange();     
        SecondaryConcept ADOConcept1 = new SecondaryConcept();
        SecondaryConcept ADOConcept2 = new SecondaryConcept();
        SecondaryConcept ADOConcept3 = new SecondaryConcept();
        CalculationDateRange dateRange_testObject;
        CalculationRangeGroup group_testObject;
        IList<CalculationDateRange> dateRangList_testObject;
        BDateRange businessDateRange;
        IList<CalculationDateRange> defaultDateRanges = new List<CalculationDateRange>() 
            {
                new CalculationDateRange(){FromDay=1,FromMonth=1,ToDay=31,ToMonth=1},
                new CalculationDateRange(){FromDay=1,FromMonth=2,ToDay=31,ToMonth=2},
                new CalculationDateRange(){FromDay=1,FromMonth=3,ToDay=31,ToMonth=3},
                new CalculationDateRange(){FromDay=1,FromMonth=4,ToDay=31,ToMonth=4},
                new CalculationDateRange(){FromDay=1,FromMonth=5,ToDay=31,ToMonth=5},
                new CalculationDateRange(){FromDay=1,FromMonth=6,ToDay=31,ToMonth=6},
                new CalculationDateRange(){FromDay=1,FromMonth=7,ToDay=30,ToMonth=7},
                new CalculationDateRange(){FromDay=1,FromMonth=8,ToDay=30,ToMonth=8},
                new CalculationDateRange(){FromDay=1,FromMonth=9,ToDay=30,ToMonth=9},
                new CalculationDateRange(){FromDay=1,FromMonth=10,ToDay=30,ToMonth=10},
                new CalculationDateRange(){FromDay=1,FromMonth=11,ToDay=30,ToMonth=11},
                new CalculationDateRange(){FromDay=1,FromMonth=12,ToDay=29,ToMonth=12},
            };
        #endregion

        #region variables
        DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter organTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter managerTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter flowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_UnderManagmentTableAdapter undermanagmentTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_UnderManagmentTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter precardAccessGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_SecurityUserTableAdapter userTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_SecurityUserTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_ManagerFlowTableAdapter managerFlowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_ManagerFlowTableAdapter();

        BWorkedTime bussWorkTime;
        BPersonMonthlyWorkedTime busPersonWorkedTime;
        Manager ADOManager1 = new Manager();
        Manager ADOManager2 = new Manager();
        Manager ADOManager3 = new Manager();
        Flow ADOFlow1 = new Flow();
        Flow ADOFlow2 = new Flow();
        Flow ADOFlow3 = new Flow();
        UnderManagment ADOUnderManagment = new UnderManagment();
        OrganizationUnit ADOOrganRoot = new OrganizationUnit();
        OrganizationUnit ADOOrgan = new OrganizationUnit();
        PrecardAccessGroup ADOaccessGroup = new PrecardAccessGroup();

        #endregion

        [SetUp]
        public void TestSetup()
        {
            

            #region organization unit
            DatabaseGateway.TA_OrganizationUnitDataTable organTable = new DatabaseGateway.TA_OrganizationUnitDataTable();
            organTable = organTA.GetDataByParent();
            ADOOrganRoot.ID = Convert.ToInt32(organTable.Rows[0]["organ_ID"]);
            ADOOrganRoot.Name = Convert.ToString(organTable.Rows[0]["organ_Name"]);
            ADOOrganRoot.CustomCode = Convert.ToString(organTable.Rows[0]["organ_CustomCode"]);

            organTA.Insert("Level2_1", "2020_11", ADOPerson2.ID, ADOOrganRoot.ID, String.Format(",{0},", ADOOrganRoot.ID));
            organTable = organTA.GetDataByCustomCode("2020_11");
            ADOOrgan.ID = Convert.ToInt32(organTable.Rows[0]["organ_ID"]);
            ADOOrgan.Name = Convert.ToString(organTable.Rows[0]["organ_Name"]);
            ADOOrgan.CustomCode = Convert.ToString(organTable.Rows[0]["organ_CustomCode"]); 
            #endregion

            #region managers
            managerTA.Insert(ADOPerson1.ID, null);
            DatasetGatewayWorkFlow.TA_ManagerDataTable masterTable = managerTA.GetDataByPersonID(ADOPerson1.ID);
            ADOManager1.ID = Convert.ToInt32(masterTable.Rows[0]["MasterMng_ID"]);
            ADOManager1.Person = ADOPerson1;
            ADOManager1.OrganizationUnit = null;

            managerTA.Insert(null, ADOOrgan.ID);
            masterTable = managerTA.GetDataByOrganID(ADOOrgan.ID);
            ADOManager2.ID = Convert.ToInt32(masterTable.Rows[0]["MasterMng_ID"]);
            ADOManager2.Person = null;
            ADOManager2.OrganizationUnit = ADOOrgan;

            managerTA.Insert(ADOPerson3.ID, null);
            masterTable = managerTA.GetDataByPersonID(ADOPerson3.ID);
            ADOManager3.ID = Convert.ToInt32(masterTable.Rows[0]["MasterMng_ID"]);
            ADOManager3.Person = ADOPerson3;
            ADOManager3.OrganizationUnit = null; 
            #endregion

            #region pishcart access group
            precardAccessGroupTA.Insert("PrecardAccessGroupTest");
            DatasetGatewayWorkFlow.TA_PrecardAccessGroupDataTable accessTable = precardAccessGroupTA.GetDataBy1("PrecardAccessGroupTest");
            ADOaccessGroup.ID = Convert.ToInt32(accessTable.Rows[0]["accessGrp_ID"]);
            ADOaccessGroup.Name = Convert.ToString(accessTable.Rows[0]["accessGrp_Name"]); 
            #endregion

            #region Flow
            flowTA.Insert(ADOaccessGroup.ID, false, false, "FlowTest1");
            DatasetGatewayWorkFlow.TA_FlowDataTable flowTable = flowTA.GetDataByName("FlowTest1");
            ADOFlow1.ID = Convert.ToInt32(flowTable.Rows[0]["flow_ID"]);
            ADOFlow1.AccessGroup = ADOaccessGroup;
            ADOFlow1.ActiveFlow = false;
            ADOFlow1.WorkFlow = false;
            ADOFlow1.FlowName = "FlowTest1";

            flowTA.Insert(ADOaccessGroup.ID, false, false, "FlowTest2");
            flowTable = flowTA.GetDataByName("FlowTest2");
            ADOFlow2.ID = Convert.ToInt32(flowTable.Rows[0]["flow_ID"]);
            ADOFlow2.AccessGroup = ADOaccessGroup;
            ADOFlow2.ActiveFlow = false;
            ADOFlow2.WorkFlow = false;
            ADOFlow2.FlowName = "FlowTest2";

            flowTA.Insert(ADOaccessGroup.ID, false, false, "FlowTest3");
            flowTable = flowTA.GetDataByName("FlowTest3");
            ADOFlow3.ID = Convert.ToInt32(flowTable.Rows[0]["flow_ID"]);
            ADOFlow3.AccessGroup = ADOaccessGroup;
            ADOFlow3.ActiveFlow = false;
            ADOFlow3.WorkFlow = false;
            ADOFlow3.FlowName = "FlowTest3";
            #endregion

            #region manager Flow
            managerFlowTA.Insert(ADOManager1.ID, 1, ADOFlow1.ID, true);
            managerFlowTA.Insert(ADOManager1.ID, 2, ADOFlow2.ID, true);
            managerFlowTA.Insert(ADOManager3.ID, 2, ADOFlow3.ID, true);
            #endregion                      

            #region under managment
            undermanagmentTA.Insert(ADOFlow1.ID, ADOPerson2.ID, ADODepartment1.ID, false, true);
            undermanagmentTA.Insert(ADOFlow1.ID, ADOPerson5.ID, ADODepartment1.ID, false, true);
            DatasetGatewayWorkFlow.TA_UnderManagmentDataTable underManagmentTable = new DatasetGatewayWorkFlow.TA_UnderManagmentDataTable();
            underManagmentTable = undermanagmentTA.GetDataByManagmentID(ADOFlow1.ID);
            ADOUnderManagment.ID = Convert.ToInt32(underManagmentTable.Rows[0]["underMng_ID"]);
            ADOUnderManagment.Contains = true;
            ADOUnderManagment.ContainInnerChilds = false;
            ADOUnderManagment.Person = ADOPerson2;
            ADOUnderManagment.Flow = ADOFlow1;

            undermanagmentTA.Insert(ADOFlow2.ID, ADOPerson4.ID, ADODepartment1.ID, false, true);
            undermanagmentTA.Insert(ADOFlow3.ID, ADOPerson5.ID, null, false, true);
           
            #endregion       

            bussWorkTime = new BWorkedTime(ADOUser1.UserName);

            #region date range init
            businessDateRange = new BDateRange();
            dateRange_testObject = new CalculationDateRange();
            group_testObject = new CalculationRangeGroup();
            dateRangList_testObject = new List<CalculationDateRange>();

            groupTA.Insert("TestRangeGroup", "", 1);
            DatabaseGateway.TA_CalculationRangeGroupDataTable groupTable = new DatabaseGateway.TA_CalculationRangeGroupDataTable();
            groupTA.FillByGroupName(groupTable, "TestRangeGroup");

            ADOGroup.ID = Convert.ToDecimal(groupTable.Rows[0]["CalcRangeGroup_ID"]);
            ADOGroup.Name = Convert.ToString(groupTable.Rows[0]["CalcRangeGroup_Name"]);
            ADOGroup.Description = Convert.ToString(groupTable.Rows[0]["CalcRangeGroup_Des"]);

            assinTA.Insert(ADOPerson3.ID, ADOGroup.ID, Utility.ToMildiDate("1390/05/01"));
            assinTA.Insert(ADOPerson4.ID, ADOGroup.ID, Utility.ToMildiDate("1389/01/01"));

            DatabaseGateway.TA_ConceptTemplateDataTable concepts = new DatabaseGateway.TA_ConceptTemplateDataTable();
            concepts = conceptTA.GetDataByyRanglyConceptsNotNullKeys();

            ADOConcept1.ID = Convert.ToDecimal(concepts.Rows[0]["concepttmp_ID"]);
            ADOConcept2.ID = Convert.ToDecimal(concepts.Rows[1]["concepttmp_ID"]);
            ADOConcept3.ID = Convert.ToDecimal(concepts.Rows[2]["concepttmp_ID"]);


            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 1, 14, 2, 1);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 2, 14, 3, 2);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 3, 14, 4, 3);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 4, 14, 5, 4);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 5, 14, 6, 5);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 6, 14, 7, 6);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 7, 14, 8, 7);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 8, 14, 9, 8);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 9, 14, 10, 9);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 10, 14, 11, 10);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 11, 14, 12, 11);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 12, 14, 1, 12);

            DatabaseGateway.TA_CalculationDateRangeDataTable rangeTable = new DatabaseGateway.TA_CalculationDateRangeDataTable();
            dateRangeTA.FillByGroup(rangeTable, ADOGroup.ID);
            ADODateRange1.ID = Convert.ToDecimal(rangeTable.Rows[0]["CalcDateRange_ID"]);
            ADODateRange2.ID = Convert.ToDecimal(rangeTable.Rows[1]["CalcDateRange_ID"]);           

            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 1, ToDay = 31, ToMonth = 1, Order = CalculationDateRangeOrder.Month1 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 2, ToDay = 31, ToMonth = 2, Order = CalculationDateRangeOrder.Month2 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 3, ToDay = 31, ToMonth = 3, Order = CalculationDateRangeOrder.Month3 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 4, ToDay = 31, ToMonth = 4, Order = CalculationDateRangeOrder.Month4 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 5, ToDay = 31, ToMonth = 5, Order = CalculationDateRangeOrder.Month5 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 6, ToDay = 31, ToMonth = 6, Order = CalculationDateRangeOrder.Month6 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 7, ToDay = 31, ToMonth = 7, Order = CalculationDateRangeOrder.Month7 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 8, ToDay = 31, ToMonth = 8, Order = CalculationDateRangeOrder.Month8 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 9, ToDay = 31, ToMonth = 9, Order = CalculationDateRangeOrder.Month9 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 10, ToDay = 31, ToMonth = 10, Order = CalculationDateRangeOrder.Month10 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 11, ToDay = 31, ToMonth = 11, Order = CalculationDateRangeOrder.Month11 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 12, ToDay = 29, ToMonth = 12, Order = CalculationDateRangeOrder.Month12 });
            #endregion
        }

        [TearDown]
        public void TreatDown()
        {
            managerTA.DeleteByBarcode(ADOPerson1.PersonCode);
            managerTA.DeleteByBarcode(ADOPerson3.PersonCode);
            managerTA.DeleteByOrganCustomCode(ADOOrgan.CustomCode);
            organTA.DeleteByCustomCode("2020_11");
            managerFlowTA.DeleteQuery(ADOFlow1.ID);
            managerFlowTA.DeleteQuery(ADOFlow2.ID);
            flowTA.DeleteByName("FlowTest1");
            flowTA.DeleteByName("FlowTest2");
            flowTA.DeleteByName("FlowTest3");
           
            precardAccessGroupTA.DeleteByName(ADOaccessGroup.Name);


            groupTA.DeleteByName(ADOGroup.Name);

            groupTA.DeleteByName("Group Test 2");

            personTA.DeleteByBarcode("0000");

            conceptTA.DeleteQuery(20001);
        }

        /// <summary>
        /// دوره محاسبات ندارد پس باید خصا پرتاب شود
        /// </summary>
        [Test]
        public void GetReport_EmptyDateRangeValidate()
        {
            try
            {
                BPersonMonthlyWorkedTime bpWorkTime = new BPersonMonthlyWorkedTime(ADOPerson1.ID);
                IList<PersonalMonthlyReportRow> DailyRows = null;
                PersonalMonthlyReportRow MonthlyRow = null;
                bpWorkTime.GetPersonMonthlyReport(2000, 2, "", "", out DailyRows, out MonthlyRow);
                PersonalMonthlyReportRow pmrr = DailyRows[0];
                Assert.Fail("دوره محاسبات ندارد");
            }
            catch (InvalidDatabaseStateException ex) 
            {
                Assert.Pass(ex.Message);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GetReport_PersonalMonthlyReportTest()
        {
            try
            {               
                PersianDateTime dt = new PersianDateTime(DateTime.Now);
                BPersonMonthlyWorkedTime pb = new BPersonMonthlyWorkedTime(32688);
                IList<PersonalMonthlyReportRow> list1;
                PersonalMonthlyReportRow monthlyRow;
                pb.GetPersonMonthlyReport(1390, 6, "1390/06/01", "1390/06/31", out list1, out monthlyRow);
                            
                string value = monthlyRow.PeriodicDailyPureOperation;

            }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }

        [Test]
        public void GetReport_PersonalDailyReportTest()
        {
            try
            {
                PersianDateTime dt = new PersianDateTime(DateTime.Now);
                BPersonMonthlyWorkedTime pb = new BPersonMonthlyWorkedTime(32688);
                ClearSession();
                PersonalMonthlyReportRow row = pb.GetPersonDailyReport(Utility.ToMildiDate("1390/06/03"));

                string value = row.PeriodicDailyPureOperation;

            }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }
        

        [Test]
        public void GetReport_PersonalMonthlyReportPropertyTest()
        {
            try
            {
                BPersonMonthlyWorkedTime bpWorkTime = new BPersonMonthlyWorkedTime(32688);
                IList<PersonalMonthlyReportRow> DailyRows = null;
                PersonalMonthlyReportRow MonthlyRow = null;
                bpWorkTime.GetPersonMonthlyReport(1390, 6, "1390/05/10", "1390/06/15", out DailyRows, out MonthlyRow);
                string value = DailyRows[0].DailySickLeave;
                value = DailyRows[1].DailySickLeave;
            }
            catch (Exception Exception)
            {
                Assert.Fail(Exception.Message);
            }
        }

        [Test]
        public void GetReport_PriodicValuesTest_Dependent() 
        {
            BPersonMonthlyWorkedTime bpWorkTime = new BPersonMonthlyWorkedTime(32687);
            IList<PersonalMonthlyReportRow> DailyRows = null;
            PersonalMonthlyReportRow MonthlyRow = null;
            bpWorkTime.GetPersonMonthlyReport(1390, 7, "1390/06/16", "1390/07/15", out DailyRows, out MonthlyRow);
            Assert.IsTrue(MonthlyRow.PeriodicPresenceDuration != "00:00" && MonthlyRow.PeriodicPresenceDuration != "");
        }

        [Test]
        public void GetReportDetail_Test_Dependent() 
        {
            try
            {
                 BPersonMonthlyWorkedTime bpWorkTime = new BPersonMonthlyWorkedTime(32688);
                 IList<MonthlyDetailReportProxy> df = bpWorkTime.GetPersonMonthlyRowDetail(DateTime.Now.AddDays(-6));

            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// تاریخ انتساب بعد از چند برج اول سال 90 است
        /// </summary>
        [Test]
        public void GetDateRangeOrder_SomeMonthNullTest() 
        {
            BPersonMonthlyWorkedTime bpWorkTime = new BPersonMonthlyWorkedTime(ADOPerson3.ID);
            IList<DateRangeOrderProxy> list = bpWorkTime.GetDateRangeOrder(1390);
            Assert.Less(list.Count, 12);
        }

        [Test]
        public void GetDateRangeOrder_Test()
        {
            BPersonMonthlyWorkedTime bpWorkTime = new BPersonMonthlyWorkedTime(ADOPerson4.ID);
            IList<DateRangeOrderProxy> list = bpWorkTime.GetDateRangeOrder(1390);
            Assert.AreEqual(12, list.Count);
            Assert.AreEqual(1, list.Where(x => x.Selected).Count());
        }

        [Test]
        public void GetUnderManagmentBySearch_BarcodeTest22222()
        {
            try
            {
                BPersonMonthlyWorkedTime bpWorkTime = new BPersonMonthlyWorkedTime(32687);
                IList<PersonalMonthlyReportRow> DailyRows = null;
                PersonalMonthlyReportRow MonthlyRow = null;
                bpWorkTime.GetPersonMonthlyReport(1390, 7, "1390/08/01", "1390/08/30", out DailyRows, out MonthlyRow);

                string a = DailyRows[0].UnallowableOverTime;
                IList<PersonalMonthlyReportRow> xxx = DailyRows.Where(x => x.HourlyPureOperation != "00:00").ToList();
                PersonalMonthlyReportRow pmrr = DailyRows[2];
                string x0 = pmrr.ImpureOperation;
                string x1 = pmrr.PeriodicDailyAbsence;
                string x2 = pmrr.DailyAbsence;
                User user= BUser.CurrentUser;


                IList<MonthlyDetailReportProxy> list2 = bpWorkTime.GetPersonMonthlyRowDetail(new DateTime(2011, 9, 7));

            }
            catch (Exception ex) 
            {

            }

        }


        [Test]
        public void test_2222222() 
        {
           /* try
            {
                BPersonMonthlyWorkedTime bpWorkTime = new BPersonMonthlyWorkedTime("salavati");
                IList<PersonalMonthlyReportRow> DailyRows = null;
                PersonalMonthlyReportRow MonthlyRow = null;
                bpWorkTime.GetPersonMonthlyReport(1390, 6, "1391/02/10", "1391/02/25", out DailyRows, out MonthlyRow);
                string value = DailyRows[0].DailySickLeave;
                value = DailyRows[1].DailySickLeave;
            }
            catch (Exception Exception)
            {
                Assert.Fail(Exception.Message);
            }
            */
        }
        
    }
}
