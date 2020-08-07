using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Assignments;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business;
using GTS.Clock.Business.WorkFlow;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business.Leave;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.Rules;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Business.Proxy;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    class CFPTest : BaseFixture
    {
        #region Definition
        DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter organTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_OrganizationUnitTableAdapter();
        DatabaseGateway2TableAdapters.TA_Calculation_Flag_PersonsTableAdapter cfpTA = new DatabaseGateway2TableAdapters.TA_Calculation_Flag_PersonsTableAdapter();
        DatabaseGatewayTableAdapters.TA_UIValidationGroupTableAdapter validationGroupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_UIValidationGroupingTableAdapter groupingTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationGroupingTableAdapter();
        DatabaseGatewayTableAdapters.TA_UIValidationRuleTableAdapter ruleTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationRuleTableAdapter();
        DatabaseGatewayTableAdapters.TA_UIValidationRuleParameterTableAdapter parmTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationRuleParameterTableAdapter();
        DatabaseGatewayTableAdapters.TA_UIValidationRuleTempPatameterTableAdapter paramTmpTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationRuleTempPatameterTableAdapter();
        DatabaseGatewayTableAdapters.TA_BaseTrafficTableAdapter basicTA = new DatabaseGatewayTableAdapters.TA_BaseTrafficTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter precardGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter precardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_RequestTableAdapter requestTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_RequestTableAdapter();
        DatabaseGatewayTableAdapters.TA_WorkGroupDetailTableAdapter wgdTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupDetailTableAdapter();
        DatabaseGatewayTableAdapters.TA_ShiftTableAdapter shiftTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ShiftTableAdapter();
        DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter workgrpTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_WorkGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter assignWorkGroupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_AssignWorkGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_PersonRangeAssignmentTableAdapter assinTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonRangeAssignmentTableAdapter();
        DatabaseGatewayTableAdapters.TA_CalculationRangeGroupTableAdapter calculationDateRangeGroupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_CalculationRangeGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_RuleCategoryTableAdapter ruleCatTA = new DatabaseGatewayTableAdapters.TA_RuleCategoryTableAdapter();
        DatabaseGatewayTableAdapters.TA_RuleCategoryPartTableAdapter ruleCatPartTA = new DatabaseGatewayTableAdapters.TA_RuleCategoryPartTableAdapter();
        DatabaseGatewayTableAdapters.TA_PersonRuleCategoryAssignmentTableAdapter prsRleCatAsgTA = new DatabaseGatewayTableAdapters.TA_PersonRuleCategoryAssignmentTableAdapter();
        DatabaseGatewayTableAdapters.QueriesTableAdapter queris = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.QueriesTableAdapter();
        DatabaseGatewayTableAdapters.TA_CalculationDateRangeTableAdapter dateRangeTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_CalculationDateRangeTableAdapter();
        DatabaseGatewayTableAdapters.TA_ConceptTemplateTableAdapter conceptTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ConceptTemplateTableAdapter();
        DatabaseGatewayTableAdapters.TA_BudgetTableAdapter budgetTA = new DatabaseGatewayTableAdapters.TA_BudgetTableAdapter();
        DatabaseGatewayTableAdapters.TA_CalendarTypeTableAdapter calanderTypeTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_CalendarTypeTableAdapter();
        #endregion

        #region variables
        BPerson busPerson;
        BRequest busRequest = new BRequest();
        Request request_testObject;
      
        UIValidationRule ADOUIRule4 = new UIValidationRule();
        UIValidationRule ADOUIRule5 = new UIValidationRule();
        UIValidationGrouping ADOGrouingR4 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouingR5 = new UIValidationGrouping();
        UIValidationGroup ADOUIValidationGroupR4 = new UIValidationGroup();
        UIValidationGroup ADOUIValidationGroupR5 = new UIValidationGroup();
        UIValidationGroup ADOUIValidationGroupEmpty = new UIValidationGroup();

        PrecardGroups ADOPrecardGroup1 = new PrecardGroups();
        PrecardGroups ADOPrecardGroup2 = new PrecardGroups();
        PrecardGroups ADOPrecardGroup3 = new PrecardGroups();
        PrecardGroups ADOPrecardGroup4 = new PrecardGroups();
        PrecardGroups ADOPrecardGroup5 = new PrecardGroups();
        Precard ADOPrecardHourlyLeave = new Precard();
        Precard ADOPrecardHourlyDuty = new Precard();
        Precard ADOPrecardHourlyEstelji = new Precard();
        Precard ADOPrecardTraffic = new Precard();
        Precard ADOPrecardDailyLeave = new Precard();
        Precard ADOPrecardDailyDuty = new Precard();
        Precard ADOPrecardOverTime = new Precard();

        CalendarType ADOCalendarType1 = new CalendarType();

        ISearchPerson searchTool;
        WorkGroup ADOWorkGroup1 = new WorkGroup();
        WorkGroup ADOWorkGroup_PersonUpdate = new WorkGroup();  
        RuleCategory ADORuleCat1 = new RuleCategory();
        RuleCategory ADORuleCat_PersonUpdate = new RuleCategory();
        CalculationRangeGroup ADODateRangeGroup1 = new CalculationRangeGroup();
        CalculationRangeGroup ADODateRangeGroup_PersonUpdate = new CalculationRangeGroup();
        Person ADOPersonUIValidationR4;
        Person ADOPersonUIValidationR5;
        Person ADOPersonUIValidation_Without;
        Shift ADOShift1 = new Shift();
        Shift ADOShift2 = new Shift();
        Shift ADOShift3 = new Shift();
        DateTime date1, date2, date3, date4, date5, date6, date7, date8, date9, date10, date11, date12, date13, date14;

        CalculationDateRange ADODateRange1 = new CalculationDateRange();
        SecondaryConcept ADOConcept1 = new SecondaryConcept();
        IList<CalculationDateRange> dateRangList_testObject = new List<CalculationDateRange>();
        
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

        [SetUp]
        public void TestSetup()
        {
            ADOPersonUIValidationR4 = new Person();
            busPerson = new BPerson();
            request_testObject = new Request();
            DatabaseGateway.TA_UIValidationRuleDataTable uiValidationRuleTable = new DatabaseGateway.TA_UIValidationRuleDataTable();
            
            #region Insert UIValidation For ADOPerson

            validationGroupTA.InsertQuery("TestGroup00");
            validationGroupTA.InsertQuery("TestGroup01");
            validationGroupTA.InsertQuery("TestGroup02");
            DatabaseGateway.TA_UIValidationGroupDataTable groupTable = validationGroupTA.GetDataByName("TestGroup00");
            ADOUIValidationGroupR4.ID = (groupTable.Rows[0] as DatabaseGateway.TA_UIValidationGroupRow).UIValGrp_ID;
            groupTable = validationGroupTA.GetDataByName("TestGroup01");
            ADOUIValidationGroupR5.ID = (groupTable.Rows[0] as DatabaseGateway.TA_UIValidationGroupRow).UIValGrp_ID;
            groupTable = validationGroupTA.GetDataByName("TestGroup02");
            ADOUIValidationGroupEmpty.ID = (groupTable.Rows[0] as DatabaseGateway.TA_UIValidationGroupRow).UIValGrp_ID;

            #region R4 close from 10th current month
            uiValidationRuleTable = ruleTA.GetDataByCode("4");
            ADOUIRule4.ID = (uiValidationRuleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADOUIRule4.ID, ADOUIValidationGroupR4.ID, false, true);

            DatabaseGateway.TA_UIValidationGroupingDataTable gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOUIValidationGroupR4.ID, ADOUIRule4.ID);
            ADOGrouingR4.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouingR4.ID, "10", 0, "", "LockCalculationFromCurrentMonth");

            #endregion

            #region R5 close from 15th before month
            uiValidationRuleTable = ruleTA.GetDataByCode("5");
            ADOUIRule5.ID = (uiValidationRuleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADOUIRule5.ID, ADOUIValidationGroupR5.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOUIValidationGroupR5.ID, ADOUIRule5.ID);
            ADOGrouingR5.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouingR5.ID, "15", 0, "", "LockCalculationFromBeforeMonth");

            #endregion

            #endregion

            #region precards

            DatasetGatewayWorkFlow.TA_PrecardGroupsDataTable precardTable = new DatasetGatewayWorkFlow.TA_PrecardGroupsDataTable();
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.leave.ToString());
            ADOPrecardGroup1.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup1.Name = "HourlyLeave";
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.duty.ToString());
            ADOPrecardGroup2.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup2.Name = "HourlyDuty";
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.leaveestelajy.ToString());
            ADOPrecardGroup3.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup3.Name = "Estelaji";
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.traffic.ToString());
            ADOPrecardGroup4.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup4.Name = "Traffic";
            precardGroupTA.FillByName(precardTable, PrecardGroupsName.overwork.ToString());
            ADOPrecardGroup5.ID = Convert.ToInt32(precardTable.Rows[0][0]);
            ADOPrecardGroup5.Name = "OwerWork";


            precardTA.Insert("TestPrecard1", true, ADOPrecardGroup1.ID, true, false, true, "99999999", false);
            precardTA.Insert("TestPrecard6", true, ADOPrecardGroup1.ID, false, true, true, "99999999", false);
            precardTA.Insert("TestPrecard7", true, ADOPrecardGroup2.ID, false, true, true, "99999999", false);
            precardTA.Insert("TestPrecard2", true, ADOPrecardGroup2.ID, true, false, true, "99999999", false);
            precardTA.Insert("TestPrecard3", true, ADOPrecardGroup3.ID, true, false, true, "99999999", false);
            precardTA.Insert("TestPrecard5", true, ADOPrecardGroup4.ID, true, false, true, "99999999", false);
            precardTA.Insert("TestPrecard8", true, ADOPrecardGroup5.ID, true, false, true, "99999999", false);

            DatasetGatewayWorkFlow.TA_PrecardDataTable pTable = new DatasetGatewayWorkFlow.TA_PrecardDataTable();
            pTable = precardTA.GetDataByName("TestPrecard1");
            ADOPrecardHourlyLeave.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardHourlyLeave.Name = "TestPrecard1";

            pTable = precardTA.GetDataByName("TestPrecard2");
            ADOPrecardHourlyDuty.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardHourlyDuty.Name = "TestPrecard2";

            pTable = precardTA.GetDataByName("TestPrecard3");
            ADOPrecardHourlyEstelji.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardHourlyEstelji.Name = "TestPrecard3";

            pTable = precardTA.GetDataByName("TestPrecard5");
            ADOPrecardTraffic.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardTraffic.Name = "TestPrecard6";

            pTable = precardTA.GetDataByName("TestPrecard6");
            ADOPrecardDailyLeave.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardDailyLeave.Name = "TestPrecard7";

            pTable = precardTA.GetDataByName("TestPrecard7");
            ADOPrecardDailyDuty.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardDailyDuty.Name = "TestPrecard7";

            pTable = precardTA.GetDataByName("TestPrecard8");
            ADOPrecardOverTime.ID = Convert.ToInt32(pTable.Rows[0][0]);
            ADOPrecardOverTime.Name = "TestPrecard8";
            #endregion

            #region Data
            workgrpTA.Insert("WorkGroupTest1", "0-0Test", 0);
            calculationDateRangeGroupTA.Insert("RangeGroup1", "", 1);
            ruleCatTA.Insert("RuleGroupTest1", "0000", false, "00-00test1");
          
            DatabaseGateway.TA_WorkGroupDataTable table = new DatabaseGateway.TA_WorkGroupDataTable();
            workgrpTA.FillByName(table, "WorkGroupTest1");
            ADOWorkGroup1.ID = Convert.ToInt32(table.Rows[0]["workgroup_ID"]);
            ADOWorkGroup1.CustomCode = Convert.ToString(table.Rows[0]["workgroup_CustomCode"]);

            DatabaseGateway.TA_CalculationRangeGroupDataTable rangeGroupTable = new DatabaseGateway.TA_CalculationRangeGroupDataTable();
            calculationDateRangeGroupTA.FillByGroupName(rangeGroupTable, "RangeGroup1");
            ADODateRangeGroup1.ID = Convert.ToDecimal(rangeGroupTable.Rows[0]["CalcRangeGroup_ID"]);

            DatabaseGateway.TA_RuleCategoryDataTable ruleCatTable = ruleCatTA.GetDataByName("RuleGroupTest1");
            ADORuleCat1.ID = (Decimal)ruleCatTable[0]["RuleCat_ID"];

            #endregion

            InsertReadyData();

            #region Shift
            BShift busshift = new BShift();
            
            ADOShift1.Name = "ClanderShiftTest1";            
            ADOShift1.Color = "0xff6512";
            ADOShift1.CustomCode = "01-00Test";
            ADOShift1.ShiftType = ShiftTypesEnum.WORK;
            busshift.SaveChanges(ADOShift1, UIActionType.ADD);

            ADOShift2.Name = "ClanderShiftTest2";
            ADOShift2.Color = "0xff65121";
            ADOShift2.CustomCode = "02-00Test";
            ADOShift2.ShiftType = ShiftTypesEnum.WORK;
            busshift.SaveChanges(ADOShift2, UIActionType.ADD);

            ADOShift3.Name = "ClanderShiftTest3";
            ADOShift3.Color = "0xff65122";
            ADOShift3.CustomCode = "03-00Test";
            ADOShift3.ShiftType = ShiftTypesEnum.WORK;
            busshift.SaveChanges(ADOShift3, UIActionType.ADD);

            ShiftPair pair = new ShiftPair(100, 200);
            pair.ShiftId = ADOShift1.ID;
            busshift.SaveChangesShiftPair(pair, UIActionType.ADD);
            pair = new ShiftPair(300, 500);
            pair.ShiftId = ADOShift1.ID;
            busshift.SaveChangesShiftPair(pair, UIActionType.ADD);
            pair = new ShiftPair(100, 200);
            pair.ShiftId = ADOShift2.ID;
            busshift.SaveChangesShiftPair(pair, UIActionType.ADD);
            pair = new ShiftPair(100, 200);
            pair.ShiftId = ADOShift3.ID;
            busshift.SaveChangesShiftPair(pair, UIActionType.ADD);
            #region date inti
            ///برخی قبل از تاریخ نشانه و برخی بعد از آن
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 8));

                     
            date1 = date;
            date2 = date.AddDays(1);
            date3 = date.AddDays(2);
            date4 = date.AddDays(3);
            date5 = date.AddDays(4);
            date6 = date.AddDays(5);
            date7 = date.AddDays(6);
            date8 = date.AddDays(7);
            date9 = date.AddDays(8);
            date10 = date.AddDays(9);
            //R5
            if (pd.Month > 1)
            {
                date11 = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, 14));
                date12 = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, 15));
                date13 = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, 16));
            }
            else
            {
                date11 = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, 12, 14));
                date12 = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, 12, 15));
                date13 = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, 12, 16));
            }
            date14 = DateTime.Now.AddDays(10);
            #endregion

            wgdTA.Insert(ADOWorkGroup_PersonUpdate.ID, ADOShift1.ID, date1);
            wgdTA.Insert(ADOWorkGroup_PersonUpdate.ID, ADOShift1.ID, date2);
            wgdTA.Insert(ADOWorkGroup_PersonUpdate.ID, ADOShift1.ID, date3);
            wgdTA.Insert(ADOWorkGroup_PersonUpdate.ID, ADOShift1.ID, date4);
            wgdTA.Insert(ADOWorkGroup_PersonUpdate.ID, ADOShift1.ID, date5);
            wgdTA.Insert(ADOWorkGroup_PersonUpdate.ID, ADOShift1.ID, date6);
            wgdTA.Insert(ADOWorkGroup_PersonUpdate.ID, ADOShift1.ID, date7);
            wgdTA.Insert(ADOWorkGroup_PersonUpdate.ID, ADOShift1.ID, date8);
            wgdTA.Insert(ADOWorkGroup_PersonUpdate.ID, ADOShift1.ID, date9);
            wgdTA.Insert(ADOWorkGroup_PersonUpdate.ID, ADOShift1.ID, date10);
            wgdTA.Insert(ADOWorkGroup_PersonUpdate.ID, ADOShift2.ID, date11);
            wgdTA.Insert(ADOWorkGroup_PersonUpdate.ID, ADOShift2.ID, date12);
            wgdTA.Insert(ADOWorkGroup_PersonUpdate.ID, ADOShift2.ID, date13);
            wgdTA.Insert(ADOWorkGroup_PersonUpdate.ID, ADOShift3.ID, date14);
            ClearSession();
            #endregion           

            #region Calendar Holiday

            calanderTypeTA.Insert("TestCalendarType1", "test-1");
            DatabaseGateway.TA_CalendarTypeDataTable calendarTypeTable = calanderTypeTA.GetDataByName("TestCalendarType1");
            DatabaseGateway.TA_CalendarTypeRow calendarTypeRow = calendarTypeTable.Rows[0] as DatabaseGateway.TA_CalendarTypeRow;
            ADOCalendarType1.ID = calendarTypeRow.CalendarType_ID;
            ADOCalendarType1.Name = calendarTypeRow.CalendarType_Name;
            ADOCalendarType1.CustomCode = calendarTypeRow.CalendarType_CustomCode;

            #endregion
              
            decimal id=busPerson.CreateWorkingPerson();                
            ADOPersonUIValidationR4 = this.GetReadyForUpdate(id,"00007", SysLanguageResource.Parsi, LocalLanguageResource.Parsi);
            ADOPersonUIValidationR4.UIValidationGroup = ADOUIValidationGroupR4;
            busPerson.SaveChanges(ADOPersonUIValidationR4, UIActionType.EDIT);
          
            ClearSession();
           
            id = busPerson.CreateWorkingPerson();
            ADOPersonUIValidationR5 = this.GetReadyForUpdate(id,"00008", SysLanguageResource.Parsi, LocalLanguageResource.Parsi);
            ADOPersonUIValidationR5.UIValidationGroup = ADOUIValidationGroupR5;
            busPerson.SaveChanges(ADOPersonUIValidationR5, UIActionType.EDIT);

            ClearSession();

            id = busPerson.CreateWorkingPerson();
            ADOPersonUIValidation_Without = this.GetReadyForUpdate(id, "00009", SysLanguageResource.Parsi, LocalLanguageResource.Parsi);
            ADOPersonUIValidation_Without.UIValidationGroup = ADOUIValidationGroupEmpty;
            busPerson.SaveChanges(ADOPersonUIValidation_Without, UIActionType.EDIT);

            //UpdateCurrentUserPersonId(ADOPersonUIValidationR4.ID);
            //personTA.UpdateValidationGroup(ADOUIValidationGroupR4.ID, ADOPersonUIValidationR4.ID);
            //personTA.UpdateValidationGroup(ADOUIValidationGroupR5.ID, ADOPersonUIValidationR5.ID);


            #region Daterange          

            DatabaseGateway.TA_ConceptTemplateDataTable concepts = new DatabaseGateway.TA_ConceptTemplateDataTable();
            concepts = conceptTA.GetDataRangly();

            ADOConcept1.ID = Convert.ToDecimal(concepts.Rows[0]["concepttmp_ID"]);

            dateRangeTA.Insert(ADOConcept1.ID, ADODateRangeGroup_PersonUpdate.ID, 15, 1, 14, 2, 1);
            dateRangeTA.Insert(ADOConcept1.ID, ADODateRangeGroup_PersonUpdate.ID, 15, 2, 14, 3, 2);
            dateRangeTA.Insert(ADOConcept1.ID, ADODateRangeGroup_PersonUpdate.ID, 15, 3, 14, 4, 3);
            dateRangeTA.Insert(ADOConcept1.ID, ADODateRangeGroup_PersonUpdate.ID, 15, 4, 14, 5, 4);
            dateRangeTA.Insert(ADOConcept1.ID, ADODateRangeGroup_PersonUpdate.ID, 15, 5, 14, 6, 5);

            DatabaseGateway.TA_CalculationDateRangeDataTable rangeTable = new DatabaseGateway.TA_CalculationDateRangeDataTable();
            dateRangeTA.FillByGroup(rangeTable, ADODateRangeGroup_PersonUpdate.ID);
            ADODateRange1.ID = Convert.ToDecimal(rangeTable.Rows[0]["CalcDateRange_ID"]);

            dateRangList_testObject = new List<CalculationDateRange>();
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

            #region Leave Budget

            budgetTA.Insert(DateTime.Now, 26, 0, ADORuleCat_PersonUpdate.ID, (int)BudgetType.Usual, "");

            #endregion

            cfpTA.UpdateCFP(DateTime.Now.AddYears(1).Date, ADOPersonUIValidationR4.ID);
            cfpTA.UpdateCFP(DateTime.Now.AddYears(1).Date, ADOPersonUIValidationR5.ID);
            cfpTA.UpdateCFP(DateTime.Now.AddYears(1).Date, ADOPersonUIValidation_Without.ID);
        }

        [TearDown]
        public void TreatDown()
        {
            requestTA.DeleteByUserId(BUser.CurrentUser.ID);
            validationGroupTA.DeleteByName("TestGroup00");
            validationGroupTA.DeleteByName("TestGroup01");
            validationGroupTA.DeleteByName("TestGroup02");
            requestTA.DeleteByPrecardCode("99999999");
            basicTA.DeleteByyPerson(ADOPerson1.ID);
            precardTA.DeleteByID("99999999");
           
            workgrpTA.DeleteByCustomCode("0-3Test");
            workgrpTA.DeleteByCustomCode("0-0Test");

            shiftTA.DeleteByCustomCode("01-00Test");
            shiftTA.DeleteByCustomCode("02-00Test");
            shiftTA.DeleteByCustomCode("03-00Test");
           
            calculationDateRangeGroupTA.DeleteByName("RangeGroup1");
            calculationDateRangeGroupTA.DeleteByName("TestCalcGroup3");

            budgetTA.DeleteByRuleCAtegoryCustomCode("0-3Test");
            budgetTA.DeleteByRuleCAtegoryCustomCode("00-00test1");
            ruleCatPartTA.DeleteByRuleCategory("0-3Test");
            ruleCatPartTA.DeleteByRuleCategory("00-00test1");
            prsRleCatAsgTA.DeleteByRuleCategory("0-3Test");
            prsRleCatAsgTA.DeleteByRuleCategory("00-00test1");
            ruleCatTA.DeleteByCustomCode("0-3Test");
            ruleCatTA.DeleteByCustomCode("00-00test1");
           
            validationGroupTA.DeleteByName("TestGroup00");

            personTA.DeleteByBarcode("00007");
            personTA.DeleteByBarcode("00008");
            personTA.DeleteByBarcode("00009");

            conceptTA.DeleteQuery(20001);

            calanderTypeTA.DeleteByName("TestCalendarType1");

            organTA.DeleteByCustomCode("0-0Test");
          
        }

        [Test]
        public void CheckCFP_PersonUpdate_Test() 
        {
            decimal id = busPerson.CreateWorkingPerson();

            Assert.AreEqual(GetCFP(id).Date.Date, DateTime.Now.Date);
        }

        #region AssignWorkGroup
      
        [Test]
        public void CheckCFP_AssignWorkGroup_MoveCFPDate_R4()
        {

            BAssignWorkGroup busAssWg = new BAssignWorkGroup(BLanguage.CurrentSystemLanguage);
            AssignWorkGroup asWg = new AssignWorkGroup();
            asWg.UIFromDate = Utility.ToPersianDate(DateTime.Now.AddMonths(-2));
            asWg.WorkGroup = new WorkGroup() { ID = ADOWorkGroup1.ID };
            asWg.Person = new Person() { ID = ADOPersonUIValidationR4.ID };
            busAssWg.SaveChanges(asWg, UIActionType.ADD);
            ClearSession();
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11));


            if (pd.Day < 10)
            {
                Assert.AreEqual(date.AddMonths(-1), GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
            else
            {
                Assert.AreEqual(date, GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
        }

        [Test]
        public void CheckCFP_AssignWorkGroup_MoveCFPDate_R5()
        {

            BAssignWorkGroup busAssWg = new BAssignWorkGroup(BLanguage.CurrentSystemLanguage);
            AssignWorkGroup asWg = new AssignWorkGroup();
            asWg.UIFromDate = Utility.ToPersianDate(DateTime.Now.AddMonths(-2));
            asWg.WorkGroup = new WorkGroup() { ID = ADOWorkGroup1.ID };
            asWg.Person = new Person() { ID = ADOPersonUIValidationR5.ID };
            busAssWg.SaveChanges(asWg, UIActionType.ADD);
            ClearSession();
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            DateTime lockDate;

            if (pd.Day < 15)
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-2));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16));
            }
            else
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-1));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16)); ;
            }
            


            Assert.AreEqual(lockDate, GetCFP(ADOPersonUIValidationR5.ID).Date);
        }

        [Test]
        public void CheckCFP_AssignWorkGroup_MoveCFPDate_WithoutLockCFP()
        {

            BAssignWorkGroup busAssWg = new BAssignWorkGroup(BLanguage.CurrentSystemLanguage);
            AssignWorkGroup asWg = new AssignWorkGroup();
            asWg.UIFromDate = Utility.ToPersianDate(DateTime.Now.AddMonths(-2));
            asWg.WorkGroup = new WorkGroup() { ID = ADOWorkGroup1.ID };
            asWg.Person = new Person() { ID = ADOPersonUIValidation_Without.ID };
            busAssWg.SaveChanges(asWg, UIActionType.ADD);
            ClearSession();

            Assert.AreEqual(DateTime.Now.AddMonths(-2).Date, GetCFP(ADOPersonUIValidation_Without.ID).Date);
        }

        [Test]
        public void CheckCFP_AssignWorkGroup_NotMoveCFPDate()
        {
            cfpTA.UpdateCFP(DateTime.Now, ADOPersonUIValidationR4.ID);
            BAssignWorkGroup busAssWg = new BAssignWorkGroup(BLanguage.CurrentSystemLanguage);
            AssignWorkGroup asWg = new AssignWorkGroup();
            asWg.UIFromDate = Utility.ToPersianDate(DateTime.Now.AddDays(10));
            asWg.WorkGroup = new WorkGroup() { ID = ADOWorkGroup1.ID };
            asWg.Person = new Person() { ID = ADOPersonUIValidationR4.ID };
            busAssWg.SaveChanges(asWg, UIActionType.ADD);
            ClearSession();
            Assert.AreEqual(DateTime.Now.Date, GetCFP(ADOPersonUIValidationR4.ID).Date.Date);
        }

        [Test]
        public void ME_CheckCFP_AssignWorkGroup_MoveCFPDate_R4()
        {

            BAssignWorkGroup busAssWg = new BAssignWorkGroup(BLanguage.CurrentSystemLanguage);
            AssignWorkGroup asWg = new AssignWorkGroup();
            asWg.ID = 50979;
            asWg.UIFromDate = "1387/05/03";
            asWg.WorkGroup = new WorkGroup() { ID = 1047 };
            asWg.Person = new Person() { ID = 32682 };
         //   busAssWg.SaveChanges(asWg, UIActionType.EDIT);
            ClearSession();
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11));


        }
      
        #endregion

        #region AssignRuleGroup

        [Test]
        public void CheckCFP_AssignRuleGroup_MoveCFPDate_R4()
        {

            BAssignRule busAssWg = new BAssignRule(BLanguage.CurrentSystemLanguage);
            PersonRuleCatAssignment asRg = new PersonRuleCatAssignment();
            asRg.UIFromDate = Utility.ToPersianDate(DateTime.Now.AddMonths(-2));
            asRg.RuleCategory = new RuleCategory() { ID = ADORuleCat1.ID };
            asRg.Person = new Person() { ID = ADOPersonUIValidationR4.ID };
            busAssWg.SaveChanges(asRg, UIActionType.ADD);
            ClearSession();
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11));

            if (pd.Day < 10)
            {
                Assert.AreEqual(date.AddMonths(-1), GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
            else
            {
                Assert.AreEqual(date, GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
        }

        [Test]
        public void CheckCFP_AssignRuleGroup_MoveCFPDate_R5()
        {
            BAssignRule busAssWg = new BAssignRule(BLanguage.CurrentSystemLanguage);
            PersonRuleCatAssignment asRg = new PersonRuleCatAssignment();
            asRg.UIFromDate = Utility.ToPersianDate(DateTime.Now.AddMonths(-2));
            asRg.RuleCategory = new RuleCategory() { ID = ADORuleCat1.ID };
            asRg.Person = new Person() { ID = ADOPersonUIValidationR5.ID };
            busAssWg.SaveChanges(asRg, UIActionType.ADD);
            ClearSession();
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            DateTime lockDate;
            if (pd.Day < 15)
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-2));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16));
            }
            else
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-1));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16)); ;
            }

            Assert.AreEqual(lockDate, GetCFP(ADOPersonUIValidationR5.ID).Date);
        }

        [Test]
        public void CheckCFP_AssignRuleGroup_MoveCFPDate_WithoutLockCFP()
        {

            BAssignRule busAssWg = new BAssignRule(BLanguage.CurrentSystemLanguage);
            PersonRuleCatAssignment asRg = new PersonRuleCatAssignment();
            asRg.UIFromDate = Utility.ToPersianDate(DateTime.Now.AddMonths(-2));
            asRg.RuleCategory = new RuleCategory() { ID = ADORuleCat1.ID };
            asRg.Person = new Person() { ID = ADOPersonUIValidation_Without.ID };
            busAssWg.SaveChanges(asRg, UIActionType.ADD);
            ClearSession();

            Assert.AreEqual(DateTime.Now.AddMonths(-2).Date, GetCFP(ADOPersonUIValidation_Without.ID).Date);
        }

        [Test]
        public void CheckCFP_AssignRuleGroup_NotMoveCFPDate()
        {
            cfpTA.UpdateCFP(DateTime.Now, ADOPersonUIValidationR4.ID);

            BAssignRule busAssWg = new BAssignRule(BLanguage.CurrentSystemLanguage);
            PersonRuleCatAssignment asRg = new PersonRuleCatAssignment();
            asRg.UIFromDate = Utility.ToPersianDate(DateTime.Now.AddDays(10));
            asRg.UIToDate = "";
            asRg.RuleCategory = new RuleCategory() { ID = ADORuleCat1.ID };
            asRg.Person = new Person() { ID = ADOPersonUIValidationR4.ID };
            busAssWg.SaveChanges(asRg, UIActionType.ADD);
            ClearSession();
            Assert.AreEqual(DateTime.Now.Date, GetCFP(ADOPersonUIValidationR4.ID).Date.Date);
        } 

        #endregion

        #region AssignDateRangeGroup

        [Test]
        public void CheckCFP_AssignDateRangeGroup_MoveCFPDate_R4()
        {

            BAssignDateRange busDateRange = new BAssignDateRange(BLanguage.CurrentSystemLanguage);
            PersonRangeAssignment asdr = new PersonRangeAssignment();
            asdr.UIFromDate = Utility.ToPersianDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 9).AddMonths(-2));
            asdr.CalcDateRangeGroup = new CalculationRangeGroup() { ID = ADODateRangeGroup1.ID };
            asdr.Person = new Person() { ID = ADOPersonUIValidationR4.ID };
            busDateRange.SaveChanges(asdr, UIActionType.ADD);
            ClearSession();
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11)).Date;
            if (pd.Day < 10)
            {
                Assert.AreEqual(date.AddMonths(-1), GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
            else 
            {
                Assert.AreEqual(date, GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
        }    

        [Test]
        public void CheckCFP_AssignDateRangeGroup_MoveCFPDate_R5()
        {
            BAssignDateRange busDateRange = new BAssignDateRange(BLanguage.CurrentSystemLanguage);
            PersonRangeAssignment asdr = new PersonRangeAssignment();
            asdr.UIFromDate = Utility.ToPersianDate(DateTime.Now.AddMonths(-2));
            asdr.CalcDateRangeGroup = new CalculationRangeGroup() { ID = ADODateRangeGroup1.ID };
            asdr.Person = new Person() { ID = ADOPersonUIValidationR5.ID };
            busDateRange.SaveChanges(asdr, UIActionType.ADD);
            ClearSession();
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            DateTime lockDate;
            if (pd.Day < 15)
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-2));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16));
            }
            else
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-1));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16)); ;
            }

            Assert.AreEqual(lockDate, GetCFP(ADOPersonUIValidationR5.ID).Date);
        }

        [Test]
        public void CheckCFP_AssignDateRangeGroup_MoveCFPDate_WithoutLockCFP()
        {

            BAssignDateRange busDateRange = new BAssignDateRange(BLanguage.CurrentSystemLanguage);
            PersonRangeAssignment asdr = new PersonRangeAssignment();
            asdr.UIFromDate = Utility.ToPersianDate(DateTime.Now.AddMonths(-2));
            asdr.CalcDateRangeGroup = new CalculationRangeGroup() { ID = ADODateRangeGroup1.ID };
            asdr.Person = new Person() { ID = ADOPersonUIValidation_Without.ID };
            busDateRange.SaveChanges(asdr, UIActionType.ADD);
            ClearSession();

            Assert.AreEqual(DateTime.Now.AddMonths(-2).Date, GetCFP(ADOPersonUIValidation_Without.ID).Date);
        }

        [Test]
        public void CheckCFP_AssignDateRangeGroup_NotMoveCFPDate()
        {
            cfpTA.UpdateCFP(DateTime.Now, ADOPersonUIValidationR4.ID);

            BAssignDateRange busDateRange = new BAssignDateRange(BLanguage.CurrentSystemLanguage);
            PersonRangeAssignment asdr = new PersonRangeAssignment();
            asdr.UIFromDate = Utility.ToPersianDate(DateTime.Now.AddDays(10));
            asdr.CalcDateRangeGroup = new CalculationRangeGroup() { ID = ADODateRangeGroup1.ID };
            asdr.Person = new Person() { ID = ADOPersonUIValidationR4.ID };
            busDateRange.SaveChanges(asdr, UIActionType.ADD);
            ClearSession();
            Assert.AreEqual(DateTime.Now.Date, GetCFP(ADOPersonUIValidationR4.ID).Date.Date);
        }

        [Test]
        public void ME_CheckCFP_AssignDateRangeGroup_MoveCFPDate_R4()
        {

            BAssignDateRange busDateRange = new BAssignDateRange(BLanguage.CurrentSystemLanguage);
            PersonRangeAssignment asdr = new PersonRangeAssignment();
            asdr.ID = 108602;
            asdr.UIFromDate = "1390/01/02";
            asdr.CalcDateRangeGroup = new CalculationRangeGroup() { ID = 26017 };
            asdr.Person = new Person() { ID = 32682 };
            busDateRange.SaveChanges(asdr, UIActionType.EDIT);
            ClearSession();
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11));

        }

        #endregion

        #region RuleCategory

        [Test]
        public void CheckCFP_RuleCategory_MoveCFPDate_R4()
        {
            cfpTA.UpdateCFP(DateTime.Now.Date, ADOPersonUIValidationR4.ID);

            BRuleCategory busRuleCat = new BRuleCategory();
            RuleCategory ruleCat = busRuleCat.GetByID(ADORuleCat_PersonUpdate.ID);
            ruleCat.InsertedTemplateIDs = new List<decimal>().ToArray<decimal>();
            ruleCat.Name = "UpdatedName!";
            busRuleCat.SaveChanges(ruleCat, UIActionType.EDIT);
            ClearSession();
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11));

            if (pd.Day < 10)
            {
                Assert.AreEqual(date.AddMonths(-1), GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
            else
            {
                Assert.AreEqual(date, GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
        }

        [Test]
        public void CheckCFP_RuleCategory_MoveCFPDate_R5()
        {
            cfpTA.UpdateCFP(DateTime.Now.Date, ADOPersonUIValidationR5.ID);

            BRuleCategory busRuleCat = new BRuleCategory();
            RuleCategory ruleCat = busRuleCat.GetByID(ADORuleCat_PersonUpdate.ID);
            ruleCat.InsertedTemplateIDs = new List<decimal>().ToArray<decimal>();
            ruleCat.Name = "UpdatedName!";
            busRuleCat.SaveChanges(ruleCat, UIActionType.EDIT);
            ClearSession();
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            DateTime lockDate;
            if (pd.Day < 15)
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-2));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16));
            }
            else
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-1));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16)); ;
            }

            Assert.AreEqual(lockDate, GetCFP(ADOPersonUIValidationR5.ID).Date);
        }

        [Test]
        public void CheckCFP_RuleCategory_MoveCFPDate_WithoutLockCFP()
        {
            cfpTA.UpdateCFP(DateTime.Now.Date, ADOPersonUIValidation_Without.ID);

            BRuleCategory busRuleCat = new BRuleCategory();
            RuleCategory ruleCat = busRuleCat.GetByID(ADORuleCat_PersonUpdate.ID);
            ruleCat.InsertedTemplateIDs = new List<decimal>().ToArray<decimal>();
            ruleCat.Name = "UpdatedName!";
            busRuleCat.SaveChanges(ruleCat, UIActionType.EDIT);
            ClearSession();
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            DateTime assgnDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 1));

            Assert.AreEqual(assgnDate, GetCFP(ADOPersonUIValidation_Without.ID).Date);
        }

        [Test]
        public void CheckCFP_RuleCategory_NotMoveCFPDate()
        {
            cfpTA.UpdateCFP(DateTime.Now.AddMonths(-3).Date, ADOPersonUIValidationR4.ID);

            BRuleCategory busRuleCat = new BRuleCategory();
            RuleCategory ruleCat = busRuleCat.GetByID(ADORuleCat_PersonUpdate.ID);
            ruleCat.InsertedTemplateIDs = new List<decimal>().ToArray<decimal>();
            ruleCat.Name = "UpdatedName!";
            busRuleCat.SaveChanges(ruleCat, UIActionType.EDIT);
            ClearSession();
            //PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            //DateTime assgnDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 1));

            Assert.AreEqual(DateTime.Now.AddMonths(-3).Date, GetCFP(ADOPersonUIValidationR4.ID).Date);
        }
        
        #endregion

        #region WorkGroup

        [Test]
        public void CheckCFP_WorkGroup_MoveCFPDate_R4()
        {
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidationR4.ID);

            BWorkGroupCalendar busWorkGroup = new BWorkGroupCalendar(SysLanguageResource.Parsi);
            //WorkGroup workGroup = busWorkGroup.GetByID(ADOWorkGroup_PersonUpdate.ID);
            
            ClearSession();
            busWorkGroup.SaveChanges(new List<CalendarCellInfo>(), ADOWorkGroup_PersonUpdate.ID, pd.Year);

            DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11));

            if (pd.Day < 10)
            {
                Assert.AreEqual(date.AddMonths(-1), GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
            else
            {
                Assert.AreEqual(date, GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
        }

        [Test]
        public void CheckCFP_WorkGroup_MoveCFPDate_R5()
        {
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidationR5.ID);

            BWorkGroupCalendar busWorkGroup = new BWorkGroupCalendar(SysLanguageResource.Parsi);

            busWorkGroup.SaveChanges(new List<CalendarCellInfo>(), ADOWorkGroup_PersonUpdate.ID, pd.Year);
            ClearSession();
            DateTime lockDate;
            if (pd.Day < 15)
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-2));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16));
            }
            else
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-1));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16)); ;
            }

            Assert.AreEqual(lockDate, GetCFP(ADOPersonUIValidationR5.ID).Date);
        }

        [Test]
        public void CheckCFP_WorkGroup_MoveCFPDate_WithoutLockCFP()
        {
           PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidation_Without.ID);

            BWorkGroupCalendar busWorkGroup = new BWorkGroupCalendar(SysLanguageResource.Parsi);

            busWorkGroup.SaveChanges(new List<CalendarCellInfo>(), ADOWorkGroup_PersonUpdate.ID, pd.Year);
            ClearSession();
            DateTime assgnDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 9));

            Assert.AreEqual(assgnDate, GetCFP(ADOPersonUIValidation_Without.ID).Date);
        }

        [Test]
        public void CheckCFP_WorkGroup_NotMoveCFPDate()
        {
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            cfpTA.UpdateCFP(DateTime.Now.AddMonths(-3).Date, ADOPersonUIValidationR4.ID);

            BWorkGroupCalendar busWorkGroup = new BWorkGroupCalendar(SysLanguageResource.Parsi);

            busWorkGroup.SaveChanges(new List<CalendarCellInfo>(), ADOWorkGroup_PersonUpdate.ID, pd.Year);
            ClearSession();
          

            Assert.AreEqual(DateTime.Now.AddMonths(-3).Date, GetCFP(ADOPersonUIValidationR4.ID).Date);
        }

        #endregion

        #region Shift

        [Test]
        public void CheckCFP_Shift_MoveCFPDate_R4()
        {
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidationR4.ID);

            BShift busShift = new BShift();
            ShiftPair pair = new ShiftPair(700, 800);
            pair.ShiftId = ADOShift1.ID;
            busShift.SaveChangesShiftPair(pair, UIActionType.ADD);        
                                                                                                                                                                                                                                                                                                                                                                                                                            
            DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11));

            if (pd.Day < 10)
            {
                Assert.AreEqual(date.AddMonths(-1), GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
            else
            {
                Assert.AreEqual(date, GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
        }

        [Test]
        public void CheckCFP_Shift_MoveCFPDate_R5()
        {
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidationR5.ID);

            BShift busShift = new BShift();
            ShiftPair pair = new ShiftPair(700, 800);
            pair.ShiftId = ADOShift2.ID;
            busShift.SaveChangesShiftPair(pair, UIActionType.ADD);    

            DateTime lockDate;
            if (pd.Day < 15)
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-2));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16));
            }
            else
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-1));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16)); ;
            }

            Assert.AreEqual(lockDate, GetCFP(ADOPersonUIValidationR5.ID).Date);
        }

        [Test]
        public void CheckCFP_Shift_MoveCFPDate_WithoutLockCFP()
        {
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidation_Without.ID);

            BShift busShift = new BShift();
            ShiftPair pair = new ShiftPair(700, 800);
            pair.ShiftId = ADOShift1.ID;
            busShift.SaveChangesShiftPair(pair, UIActionType.ADD);        
            ClearSession();
            DateTime assgnDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 9));
            assgnDate = assgnDate < date1 ? date1.Date : assgnDate;

            Assert.AreEqual(assgnDate, GetCFP(ADOPersonUIValidation_Without.ID).Date);
        }

        [Test]
        public void CheckCFP_Shift_NotMoveCFPDate()
        {
            //PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(DateTime.Now, ADOPersonUIValidationR4.ID);
            //pd = new PersianDateTime(DateTime.Now.AddDays(10));
            BShift busShift = new BShift();
            ShiftPair pair = new ShiftPair(700, 800);
            pair.ShiftId = ADOShift3.ID;
            busShift.SaveChangesShiftPair(pair, UIActionType.ADD);
            ClearSession();

            Assert.AreEqual(DateTime.Now.Date, GetCFP(ADOPersonUIValidationR4.ID).Date.Date);
        } 

        #endregion

        #region Date Range

        [Test]
        public void CheckCFP_DateRange_MoveCFPDate_R4()
        {
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidationR4.ID);

            BDateRange businessDateRange = new BDateRange();
            ADODateRangeGroup1.ID = ADODateRangeGroup_PersonUpdate.ID;
            ADODateRangeGroup1.Name = "TestCalcGroup3";
            businessDateRange.UpdateDateRange(ADODateRangeGroup1, dateRangList_testObject, new List<decimal>() { ADOConcept1.ID });

            ClearSession();
         
            DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11));

            if (pd.Day < 10)
            {
                Assert.AreEqual(date.AddMonths(-1), GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
            else
            {
                Assert.AreEqual(date, GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
        }

        [Test]
        public void CheckCFP_DateRange_MoveCFPDate_R5()
        {
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidationR5.ID);

            BDateRange businessDateRange = new BDateRange();

            ADODateRangeGroup1.ID = ADODateRangeGroup_PersonUpdate.ID;
            ADODateRangeGroup1.Name = "TestCalcGroup3";
            businessDateRange.UpdateDateRange(ADODateRangeGroup1, dateRangList_testObject, new List<decimal>() { ADOConcept1.ID });
            ClearSession();
            DateTime lockDate;
            if (pd.Day < 15)
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-2));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16));
            }
            else
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-1));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16)); ;
            }

            Assert.AreEqual(lockDate, GetCFP(ADOPersonUIValidationR5.ID).Date);
        }

        [Test]
        public void CheckCFP_DateRange_MoveCFPDate_WithoutLockCFP()
        {
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidation_Without.ID);

            BDateRange businessDateRange = new BDateRange();

            ADODateRangeGroup1.ID = ADODateRangeGroup_PersonUpdate.ID;
            ADODateRangeGroup1.Name = "TestCalcGroup3";
            businessDateRange.UpdateDateRange(ADODateRangeGroup1, dateRangList_testObject, new List<decimal>() { ADOConcept1.ID });
            ClearSession();
            DateTime assgnDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, 1, 1));

            Assert.AreEqual(assgnDate, GetCFP(ADOPersonUIValidation_Without.ID).Date);
        }

        [Test]
        public void CheckCFP_DateRange_NotMoveCFPDate()
        {
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            cfpTA.UpdateCFP(DateTime.Now.AddMonths(-3).Date, ADOPersonUIValidationR4.ID);

            BDateRange businessDateRange = new BDateRange();

            ADODateRangeGroup1.ID = ADODateRangeGroup_PersonUpdate.ID;
            ADODateRangeGroup1.Name = "TestCalcGroup3";
            businessDateRange.UpdateDateRange(ADODateRangeGroup1, dateRangList_testObject, new List<decimal>() { ADOConcept1.ID });
            ClearSession();


            Assert.AreEqual(DateTime.Now.AddMonths(-3).Date, GetCFP(ADOPersonUIValidationR4.ID).Date);
        }

        #endregion

        #region Leave Budget

        [Test]
        public void CheckCFP_LeaveBudget_MoveCFPDate_R4()
        {
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidationR4.ID);

            BLeaveBudget businessLeaveBudget = new BLeaveBudget();
            LeaveBudgetProxy proxy = new LeaveBudgetProxy();
            proxy.BudgetType = BudgetType.Usual;
            proxy.TotoalDay = "26";
            proxy.TotoalTime = "00:00";

            businessLeaveBudget.SaveBudget(ADORuleCat_PersonUpdate.ID, pd.Year, proxy);

            ClearSession();

            DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11));

            if (pd.Day < 10)
            {
                Assert.AreEqual(date.AddMonths(-1), GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
            else
            {
                Assert.AreEqual(date, GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
        }

        [Test]
        public void CheckCFP_LeaveBudget_MoveCFPDate_R5()
        {
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidationR5.ID);

            BLeaveBudget businessLeaveBudget = new BLeaveBudget();
            LeaveBudgetProxy proxy = new LeaveBudgetProxy();
            proxy.BudgetType = BudgetType.Usual;
            proxy.TotoalDay = "26";
            proxy.TotoalTime = "00:00";

            businessLeaveBudget.SaveBudget(ADORuleCat_PersonUpdate.ID, pd.Year, proxy);
            ClearSession();
            DateTime lockDate;
            if (pd.Day < 15)
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-2));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16));
            }
            else
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-1));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16)); ;
            }

            Assert.AreEqual(lockDate, GetCFP(ADOPersonUIValidationR5.ID).Date);
        }

        [Test]
        public void CheckCFP_LeaveBudget_MoveCFPDate_WithoutLockCFP()
        {
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidation_Without.ID);

            BLeaveBudget businessLeaveBudget = new BLeaveBudget();
            LeaveBudgetProxy proxy = new LeaveBudgetProxy();
            proxy.BudgetType = BudgetType.Usual;
            proxy.TotoalDay = "26";
            proxy.TotoalTime = "00:00";

            businessLeaveBudget.SaveBudget(ADORuleCat_PersonUpdate.ID, pd.Year, proxy);
            ClearSession();
            DateTime assgnDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year,1, 1));

            Assert.AreEqual(assgnDate, GetCFP(ADOPersonUIValidation_Without.ID).Date);
        }

        [Test]
        public void CheckCFP_LeaveBudget_NotMoveCFPDate()
        {
            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            cfpTA.UpdateCFP(DateTime.Now.AddYears(-2).Date, ADOPersonUIValidationR4.ID);

            BLeaveBudget businessLeaveBudget = new BLeaveBudget();
            LeaveBudgetProxy proxy = new LeaveBudgetProxy();
            proxy.BudgetType = BudgetType.Usual;
            proxy.TotoalDay = "26";
            proxy.TotoalTime = "00:00";

            businessLeaveBudget.SaveBudget(ADORuleCat_PersonUpdate.ID, pd.Year, proxy);
            ClearSession();


            Assert.AreEqual(DateTime.Now.AddYears(-2).Date, GetCFP(ADOPersonUIValidationR4.ID).Date);
        }

        #endregion

        #region Calendar Type

        [Test]
        public void CheckCFP_Calendar_checkRepositoryMethod_Test()
        {
            PersonRepository prsRep = new PersonRepository(false);
            IList<decimal> prsIds = prsRep.GetAllActivePersonIds();
            Assert.IsTrue(prsIds.Contains(ADOPerson1.ID));
            Assert.IsTrue(prsIds.Contains(ADOPerson5.ID));
            Assert.IsFalse(prsIds.Contains(ADOPersonUIValidationR4.ID));
        }

        [Test]
        public void CheckCFP_Calendar_R4() 
        {
            BCalendarType busCalendarType = new BCalendarType();

            busCalendarType.InTestCasePersonContext = new List<decimal>() { ADOPersonUIValidationR4.ID, ADOPersonUIValidationR5.ID, ADOPersonUIValidation_Without.ID };

            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidationR4.ID);

            IList<CalendarCellInfo> list = new List<CalendarCellInfo>();
            list.Add(new CalendarCellInfo() { Day = 1, Month = pd.Month });
            list.Add(new CalendarCellInfo() { Day = 2, Month = pd.Month });
            list.Add(new CalendarCellInfo() { Day = 3, Month = pd.Month });
            list.Add(new CalendarCellInfo() { Day = 4, Month = pd.Month });

            busCalendarType.InsertCalendars(ADOCalendarType1.ID, pd.Year, list);

            ClearSession();

            DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11));

            if (pd.Day < 10)
            {
                Assert.AreEqual(date.AddMonths(-1), GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
            else
            {
                Assert.AreEqual(date, GetCFP(ADOPersonUIValidationR4.ID).Date);
            }
        }

        [Test]
        public void CheckCFP_Calendar_R5()
        {
            BCalendarType busCalendarType = new BCalendarType();

            busCalendarType.InTestCasePersonContext = new List<decimal>() { ADOPersonUIValidationR4.ID, ADOPersonUIValidationR5.ID, ADOPersonUIValidation_Without.ID };

            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidationR5.ID);

            IList<CalendarCellInfo> list = new List<CalendarCellInfo>();
            list.Add(new CalendarCellInfo() { Day = 1, Month = pd.Month });
            list.Add(new CalendarCellInfo() { Day = 2, Month = pd.Month });
            list.Add(new CalendarCellInfo() { Day = 3, Month = pd.Month });
            list.Add(new CalendarCellInfo() { Day = 4, Month = pd.Month });

            busCalendarType.InsertCalendars(ADOCalendarType1.ID, pd.Year, list);

            ClearSession();

            DateTime lockDate;
            if (pd.Day < 15)
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-2));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16));
            }
            else
            {
                pd = Utility.ToPersianDateTime(DateTime.Now.AddMonths(-1));
                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 16)); ;
            }


            Assert.AreEqual(lockDate, GetCFP(ADOPersonUIValidationR5.ID).Date);
        }

        [Test]
        public void CheckCFP_Calendar_WithoutLockCFP()
        {
            BCalendarType busCalendarType = new BCalendarType();

            busCalendarType.InTestCasePersonContext = new List<decimal>() { ADOPersonUIValidationR4.ID, ADOPersonUIValidationR5.ID, ADOPersonUIValidation_Without.ID };

            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 29)), ADOPersonUIValidation_Without.ID);


            IList<CalendarCellInfo> list = new List<CalendarCellInfo>();
            list.Add(new CalendarCellInfo() { Day = 1, Month = pd.Month });
            list.Add(new CalendarCellInfo() { Day = 2, Month = pd.Month });
            list.Add(new CalendarCellInfo() { Day = 3, Month = pd.Month });
            list.Add(new CalendarCellInfo() { Day = 4, Month = pd.Month });

            busCalendarType.InsertCalendars(ADOCalendarType1.ID, pd.Year, list);

            ClearSession();

            DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 1));

            Assert.AreEqual(date, GetCFP(ADOPersonUIValidation_Without.ID).Date);
        }

        [Test]
        public void CheckCFP_Calendar_NotMoveCFPDate()
        {
            BCalendarType busCalendarType = new BCalendarType();

            busCalendarType.InTestCasePersonContext = new List<decimal>() { ADOPersonUIValidationR4.ID, ADOPersonUIValidationR5.ID, ADOPersonUIValidation_Without.ID };

            PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

            cfpTA.UpdateCFP(DateTime.Now.Date, ADOPersonUIValidationR4.ID);

            IList<CalendarCellInfo> list = new List<CalendarCellInfo>();
            list.Add(new CalendarCellInfo() { Day = 1, Month = pd.Month });
            list.Add(new CalendarCellInfo() { Day = 2, Month = pd.Month });
            list.Add(new CalendarCellInfo() { Day = 3, Month = pd.Month });
            list.Add(new CalendarCellInfo() { Day = 4, Month = pd.Month });

            busCalendarType.InsertCalendars(ADOCalendarType1.ID, pd.Year + 1, list);

            ClearSession();

            DateTime date = DateTime.Now.Date;

            Assert.AreEqual(date, GetCFP(ADOPersonUIValidationR4.ID).Date);
        }

        #endregion
        
        private void InsertReadyData() 
        {
            BWorkgroup workgroup = new BWorkgroup();
            decimal wID = workgroup.SaveChanges(new WorkGroup() { Name = "WorkGroupTest3", CustomCode = "0-3Test" }, UIActionType.ADD);
            ADOWorkGroup_PersonUpdate = new WorkGroup() { ID = wID };

            BRuleCategory bruleCat = new BRuleCategory();
            bruleCat.EnableInsertValidate = false;
            decimal rID = bruleCat.SaveChanges(new RuleCategory() { Name = "RuleCatTest3", CustomCode = "0-3Test" }, UIActionType.ADD);
            ADORuleCat_PersonUpdate = new RuleCategory() { ID = rID };
            
            BDateRange bdate = new BDateRange();
            decimal rangeId = bdate.SaveChanges(new CalculationRangeGroup() { Name = "TestCalcGroup3" }, UIActionType.ADD);
            ADODateRangeGroup_PersonUpdate = new CalculationRangeGroup() { ID = rangeId };
        }
     
        private Person GetReadyForUpdate(decimal personid,string barCode, SysLanguageResource sys, LocalLanguageResource local)
        {
            try
            {
                PersianDateTime today = Utility.ToPersianDateTime(DateTime.Now);
                Person prs = new Person();
                busPerson = new BPerson(sys, local);
                if (personid == 0)
                {
                    prs.ID = busPerson.CreateWorkingPerson2();
                    ClearSession();
                }
                else
                {
                    prs.ID = personid;
                }
                prs.PersonDetail = new PersonDetail();

                #region Assigns

                #region workgroup
                BAssignWorkGroup bAssginWorkGroup = new BAssignWorkGroup(SysLanguageResource.Parsi);
                
                AssignWorkGroup aw = new AssignWorkGroup();
                aw.UIFromDate = String.Format("{0}/{1}/{2}", today.Year, today.Month, 9);
                aw.WorkGroup = ADOWorkGroup_PersonUpdate;
                aw.Person = new Person() { ID = prs.ID };
                bAssginWorkGroup.SaveChanges(aw, UIActionType.ADD); 
                #endregion

                #region RuleCategory
                BAssignRule bAssginRule = new BAssignRule(SysLanguageResource.Parsi);

                PersonRuleCatAssignment pa = new PersonRuleCatAssignment();
                pa.UIFromDate = String.Format("{0}/{1}/{2}", today.Year, today.Month, 1);
                pa.UIToDate = String.Format("{0}/{1}/{2}", today.Year, today.Month, 9);
                pa.RuleCategory = ADORuleCat_PersonUpdate;
                pa.Person = new Person() { ID = prs.ID };
                bAssginRule.SaveChanges(pa, UIActionType.ADD); 
                #endregion
              
                #region DateRange

                BAssignDateRange bDateRange = new BAssignDateRange(SysLanguageResource.Parsi);
                PersonRangeAssignment rangeAssign = new PersonRangeAssignment();


                ClearSession();

                rangeAssign.CalcDateRangeGroup = ADODateRangeGroup_PersonUpdate;
                if (sys == SysLanguageResource.Parsi)
                {
                    rangeAssign.UIFromDate = String.Format("{0}/{1}/{2}", today.Year, 1, 1);// today.Month, 9);
                }
                else
                {
                    rangeAssign.UIFromDate = Utility.ToString(Utility.ToMildiDate(String.Format("{0}/{1}/{2}", today.Year, today.Month, 9)));
                }

                rangeAssign.Person = new Person() { ID = prs.ID };

                bDateRange.SaveChanges(rangeAssign, UIActionType.ADD);
                #endregion
              
                //جهت درج
                prs.PersonRangeAssignList = new List<PersonRangeAssignment>();
                prs.PersonRangeAssignList.Add(rangeAssign);

                #endregion

                #region Dep
                DatabaseGatewayTableAdapters.TA_DepartmentTableAdapter departmentTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DepartmentTableAdapter();
                DatabaseGateway.TA_DepartmentDataTable depTAble = new DatabaseGateway.TA_DepartmentDataTable();
                departmentTA.InsertQuery("Level1", "test_0-0", 1, ",1,", "");
                departmentTA.GetByCustomCode(depTAble, "test_0-0");
                decimal departmentId = Convert.ToDecimal(depTAble.Rows[0][0]);

                organTA.InsertQuery("OrganTestLevel1", "0-0Test", null, 1, String.Format(",{0},", 1));
                decimal organId = Convert.ToDecimal(organTA.GetDataByCustomCode("0-0Test")[0]["organ_ID"]);

                DatabaseGatewayTableAdapters.TA_ControlStationTableAdapter sataionTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ControlStationTableAdapter();
                sataionTA.Insert("StationTest1", "0-0Test");
                decimal stationId = Convert.ToDecimal(sataionTA.GetDataByCustomCode("0-0Test")[0]["station_ID"]);

                DatabaseGatewayTableAdapters.TA_EmploymentTypeTableAdapter emplTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_EmploymentTypeTableAdapter();
                emplTA.Insert("EmploymentTypeTest1", "0-0Test");
                decimal employeeId = Convert.ToDecimal(emplTA.GetDataByCustomCode("0-0Test")[0]["emply_ID"]);
                #endregion

                prs.FirstName = "Iraj";
                prs.LastName = "Bahadori";
                prs.PersonDetail.FatherName = "Gholzoom";
                prs.PersonDetail.FatherName = "0082111098";
                prs.PersonDetail.BirthCertificate = "22777";
                prs.PersonDetail.BirthPlace = "Sorhe";
                prs.Education = "لیسانس";
                prs.PersonDetail.Status = "رو هوا";
                prs.PersonDetail.Tel = "23444343";
                prs.PersonDetail.Address = "";
                prs.PersonCode = barCode;
                prs.CardNum = "4345554" + DateTime.Now.Millisecond.ToString();
                prs.EmploymentNum = "123A342-ad" + DateTime.Now.Millisecond.ToString();
                prs.Sex = PersonSex.Male;
                prs.MaritalStatus = MaritalStatus.Motaleghe;
                prs.PersonDetail.MilitaryStatus = MilitaryStatus.HeineKhedmat;
                prs.Department = new global::GTS.Clock.Model.Charts.Department() { ID = departmentId };
                prs.OrganizationUnit = new global::GTS.Clock.Model.Charts.OrganizationUnit() { ID = organId, PersonID = prs.ID, Name = "OrganTestLevel1", CustomCode = "0-0", ParentID = 1 };
                prs.ControlStation = new global::GTS.Clock.Model.BaseInformation.ControlStation() { ID = stationId };
                prs.EmploymentType = new global::GTS.Clock.Model.BaseInformation.EmploymentType() { ID = employeeId };
                if (sys == SysLanguageResource.Parsi)
                {
                    prs.UIEmploymentDate = "1380/05/03";
                    prs.UIEndEmploymentDate = "1390/05/03";
                    prs.PersonDetail.UIBirthDate = "1390/05/03";
                }
                else
                {
                    prs.UIEmploymentDate = Utility.ToString(Utility.ToMildiDate("1380/05/03"));
                    prs.UIEndEmploymentDate = Utility.ToString(Utility.ToMildiDate("1390/05/03"));
                    prs.PersonDetail.UIBirthDate = Utility.ToString(Utility.ToMildiDate("1390/05/03"));
                }

                ClearSession();

                return prs;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private CFP GetCFP(decimal personId) 
        {
            CFP currentCFP = new CFP();
            DatabaseGateway2.TA_Calculation_Flag_PersonsDataTable table = cfpTA.GetDataByPersonId(personId);
            currentCFP = new CFP();
            currentCFP.ID = (table.Rows[0] as DatabaseGateway2.TA_Calculation_Flag_PersonsRow).CFP_ID;
            currentCFP.Date = (table.Rows[0] as DatabaseGateway2.TA_Calculation_Flag_PersonsRow).CFP_Date;
            currentCFP.PrsId = (table.Rows[0] as DatabaseGateway2.TA_Calculation_Flag_PersonsRow).CFP_PrsId;
            return currentCFP;
        }

    }
}
