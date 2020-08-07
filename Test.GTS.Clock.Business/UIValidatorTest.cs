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

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class UIValidatorTest : BaseFixture
    {
        #region Definition

        DatabaseGatewayTableAdapters.TA_UIValidationGroupTableAdapter validationGroupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_UIValidationGroupingTableAdapter groupingTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationGroupingTableAdapter();
        DatabaseGatewayTableAdapters.TA_UIValidationRuleTableAdapter ruleTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationRuleTableAdapter();
        DatabaseGatewayTableAdapters.TA_UIValidationRuleParameterTableAdapter parmTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationRuleParameterTableAdapter();
        DatabaseGatewayTableAdapters.TA_UIValidationRuleTempPatameterTableAdapter paramTmpTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UIValidationRuleTempPatameterTableAdapter();
        DatabaseGatewayTableAdapters.TA_BaseTrafficTableAdapter basicTA = new DatabaseGatewayTableAdapters.TA_BaseTrafficTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter precardGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter precardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_RequestTableAdapter requestTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_RequestTableAdapter();


        BRequest busRequest = new BRequest();
        Request request_testObject;
        UIValidationRule ADORule4 = new UIValidationRule();
        UIValidationRule ADORule5 = new UIValidationRule();
        UIValidationRule ADORule6 = new UIValidationRule();
        UIValidationRule ADORule7 = new UIValidationRule();
        UIValidationRule ADORule8 = new UIValidationRule();
        UIValidationRule ADORule9 = new UIValidationRule();
        UIValidationRule ADORule10 = new UIValidationRule();
        UIValidationRule ADORule11 = new UIValidationRule();
        UIValidationRule ADORule12 = new UIValidationRule();
        UIValidationRule ADORule13 = new UIValidationRule();
        UIValidationRule ADORule14 = new UIValidationRule();
        UIValidationRule ADORule15 = new UIValidationRule();
        UIValidationRule ADORule16 = new UIValidationRule();
        UIValidationRule ADORule17 = new UIValidationRule();
        UIValidationRule ADORule18 = new UIValidationRule();
        UIValidationRule ADORule19 = new UIValidationRule();
        UIValidationRule ADORule20 = new UIValidationRule();
        UIValidationRule ADORule21 = new UIValidationRule();
        UIValidationRule ADORule22 = new UIValidationRule();
        UIValidationGrouping ADOGrouing4 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing5 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing6 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing7 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing8 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing9 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing10 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing11 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing12 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing13 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing14 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing15 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing16 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing17 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing18 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing19 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing20 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing21 = new UIValidationGrouping();
        UIValidationGrouping ADOGrouing22 = new UIValidationGrouping();
        UIValidationGroup ADOGroup1 = new UIValidationGroup();
        UIValidationGroup ADOGroup2 = new UIValidationGroup();

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
        #endregion

        [SetUp]
        public void TestSetup()
        {
            request_testObject = new Request();
            DatabaseGateway.TA_UIValidationRuleDataTable ruleTable = new DatabaseGateway.TA_UIValidationRuleDataTable();

            #region Insert UIValidation For ADOPerson

            validationGroupTA.InsertQuery("TestGroup00");
            DatabaseGateway.TA_UIValidationGroupDataTable groupTable = validationGroupTA.GetDataByName("TestGroup00");
            ADOGroup1.ID = (groupTable.Rows[0] as DatabaseGateway.TA_UIValidationGroupRow).UIValGrp_ID;

            validationGroupTA.InsertQuery("TestGroup01");
            groupTable = validationGroupTA.GetDataByName("TestGroup01");
            ADOGroup2.ID = (groupTable.Rows[0] as DatabaseGateway.TA_UIValidationGroupRow).UIValGrp_ID;

            #region R4
            ruleTable = ruleTA.GetDataByCode("4");
            ADORule4.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule4.ID, ADOGroup1.ID, false, true);

            DatabaseGateway.TA_UIValidationGroupingDataTable gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup1.ID, ADORule4.ID);
            ADOGrouing4.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing4.ID, "10", 0, "", "LockCalculationFromCurrentMonth");

            #endregion

            #region R5
            ruleTable = ruleTA.GetDataByCode("5");
            ADORule5.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule5.ID, ADOGroup1.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup1.ID, ADORule5.ID);
            ADOGrouing5.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing5.ID, "10", 0, "", "LockCalculationFromBeforeMonth");

            #endregion

            #region R6
            ruleTable = ruleTA.GetDataByCode("6");
            ADORule6.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule6.ID, ADOGroup1.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup1.ID, ADORule6.ID);
            ADOGrouing6.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing6.ID, "10", 0, "", "LockBaseInformationFromCurrentMonth");

            #endregion

            #region R7
            ruleTable = ruleTA.GetDataByCode("7");
            ADORule7.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule7.ID, ADOGroup1.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup1.ID, ADORule7.ID);
            ADOGrouing7.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing7.ID, "1", 0, "", "MaxTrafficRequestInMonth");

            #endregion

            #region R8
            ruleTable = ruleTA.GetDataByCode("8");
            ADORule8.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule8.ID, ADOGroup1.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup1.ID, ADORule8.ID);
            ADOGrouing8.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing8.ID, "2", 0, "", "AllowedTrafficRequestAfterTime");

            #endregion

            #region R9
            ruleTable = ruleTA.GetDataByCode("9");
            ADORule9.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule9.ID, ADOGroup1.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup1.ID, ADORule9.ID);
            ADOGrouing9.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing9.ID, "1", 0, "", "BeforeDayCount");
            parmTA.InsertQuery(ADOGrouing9.ID, "1", 0, "", "AfterDayCount");

            #endregion

            #region R10
            ruleTable = ruleTA.GetDataByCode("10");
            ADORule10.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule10.ID, ADOGroup1.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup1.ID, ADORule10.ID);
            ADOGrouing10.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing10.ID, "1", 0, "", "BeforeDayCount");
            parmTA.InsertQuery(ADOGrouing10.ID, "1", 0, "", "AfterDayCount");

            #endregion

            #region R11
            ruleTable = ruleTA.GetDataByCode("11");
            ADORule11.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule11.ID, ADOGroup1.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup1.ID, ADORule11.ID);
            ADOGrouing11.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing11.ID, "1", 0, "", "BeforeDayCount");
            parmTA.InsertQuery(ADOGrouing11.ID, "1", 0, "", "AfterDayCount");

            #endregion

            #region R12
            ruleTable = ruleTA.GetDataByCode("12");
            ADORule12.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule12.ID, ADOGroup1.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup1.ID, ADORule12.ID);
            ADOGrouing12.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing12.ID, "1", 0, "", "BeforeDayCount");
            parmTA.InsertQuery(ADOGrouing12.ID, "1", 0, "", "AfterDayCount");

            #endregion

            #region R13
            ruleTable = ruleTA.GetDataByCode("13");
            ADORule13.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule13.ID, ADOGroup1.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup1.ID, ADORule13.ID);
            ADOGrouing13.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing13.ID, "1", 0, "", "BeforeDayCount");
            parmTA.InsertQuery(ADOGrouing13.ID, "1", 0, "", "AfterDayCount");

            #endregion

            #region R14
            ruleTable = ruleTA.GetDataByCode("14");
            ADORule14.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule14.ID, ADOGroup1.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup1.ID, ADORule14.ID);
            ADOGrouing14.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing14.ID, "1", 0, "", "BeforeDayCount");
            parmTA.InsertQuery(ADOGrouing14.ID, "1", 0, "", "AfterDayCount");

            #endregion

            #region R15
            ruleTable = ruleTA.GetDataByCode("15");
            ADORule15.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule15.ID, ADOGroup1.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup1.ID, ADORule15.ID);
            ADOGrouing15.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing15.ID, "1", 0, "", "BeforeDayCount");
            parmTA.InsertQuery(ADOGrouing15.ID, "1", 0, "", "AfterDayCount");

            #endregion

            #region R16
            ruleTable = ruleTA.GetDataByCode("16");
            ADORule16.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule16.ID, ADOGroup2.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup2.ID, ADORule16.ID);
            ADOGrouing16.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing16.ID, "1", 0, "", "MaxCount");

            #endregion

            #region R17
            ruleTable = ruleTA.GetDataByCode("17");
            ADORule17.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule17.ID, ADOGroup2.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup2.ID, ADORule17.ID);
            ADOGrouing17.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing17.ID, "1", 0, "", "MaxCount");

            #endregion

            #region R18
            ruleTable = ruleTA.GetDataByCode("18");
            ADORule18.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule18.ID, ADOGroup2.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup2.ID, ADORule18.ID);
            ADOGrouing18.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing18.ID, "1", 0, "", "MaxCount");

            #endregion

            #region R19
            ruleTable = ruleTA.GetDataByCode("19");
            ADORule19.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule19.ID, ADOGroup2.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup2.ID, ADORule19.ID);
            ADOGrouing19.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing19.ID, "1", 0, "", "MaxCount");

            #endregion

            #region R20
            ruleTable = ruleTA.GetDataByCode("20");
            ADORule20.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule20.ID, ADOGroup2.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup2.ID, ADORule20.ID);
            ADOGrouing20.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing20.ID, "1", 0, "", "MaxCount");

            #endregion

            #region R21
            ruleTable = ruleTA.GetDataByCode("21");
            ADORule21.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule21.ID, ADOGroup2.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup2.ID, ADORule21.ID);
            ADOGrouing21.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing21.ID, "1", 0, "", "MaxCount");

            #endregion

            #region R22
            ruleTable = ruleTA.GetDataByCode("22");
            ADORule22.ID = (ruleTable.Rows[0] as DatabaseGateway.TA_UIValidationRuleRow).UIRle_ID;

            groupingTA.Insert(ADORule22.ID, ADOGroup2.ID, false, true);

            gropingTable = groupingTA.GetDataByGroupIdAndRuleId(ADOGroup2.ID, ADORule22.ID);
            ADOGrouing22.ID = Convert.ToDecimal(gropingTable.Rows[0][0]);

            parmTA.InsertQuery(ADOGrouing22.ID, "1", 0, "", "MaxHrours");

            #endregion


            UpdateCurrentUserPersonId(ADOPerson2.ID);
            personTA.UpdateValidationGroup(ADOGroup2.ID, BUser.CurrentUser.Person.ID);

            UpdateCurrentUserPersonId(ADOPerson1.ID);
            personTA.UpdateValidationGroup(ADOGroup1.ID, BUser.CurrentUser.Person.ID);



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
        }

        [TearDown]
        public void TreatDown()
        {
            requestTA.DeleteByUserId(BUser.CurrentUser.ID);
            validationGroupTA.DeleteByName("TestGroup00");
            validationGroupTA.DeleteByName("TestGroup01");
            requestTA.DeleteByPrecardCode("99999999");
            basicTA.DeleteByyPerson(ADOPerson1.ID);
            precardTA.DeleteByID("99999999");
        }

        #region R4
        #region Permit
        [Test]
        public void R4_LockCalculationFromCurrentMonth_Permit_Test()
        {
            try
            {
                BPermit busPermit = new BPermit();
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 9));

                Permit permit = new Permit();
                PermitPair permitPair = new PermitPair();
                permit.IsPairly = true;
                permit.FromDate = date;
                permit.ToDate = date;
                permit.Pairs = new List<PermitPair>() { permitPair };
                permit.Person = ADOPerson1;
                permitPair.Permit = permit;
                //permitPair.RequestID = request.ID;
                permitPair.From = 100;
                permitPair.To = 200;
                permitPair.Value = 100;
                permitPair.PreCardID = ADOPrecardHourlyDuty.ID;
                permitPair.IsFilled = true;
                busPermit.SaveChanges(permit, UIActionType.ADD);

                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R4_LockCalculationFromCurrentMonth));
            }
        }

        [Test]
        public void R4_LockCalculationFromCurrentMonth__PermitPass_Test()
        {
            try
            {
                AssignWorkGroup aw = new AssignWorkGroup();
                aw.FromDate = DateTime.Now;
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                aw.UIFromDate = String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11);
                aw.WorkGroup = new WorkGroup() { ID = 1047 };
                aw.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                BAssignWorkGroup bus = new BAssignWorkGroup(BLanguage.CurrentSystemLanguage);
                bus.SaveChanges(aw, UIActionType.ADD);
                Assert.IsTrue(aw.ID > 0);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.UIValidation_R4_LockCalculationFromCurrentMonth));
            }
        }
        #endregion

        #region Shift Exception
        [Test]
        public void R4_LockCalculationFromCurrentMonth_ShiftException_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                string date = String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 9);

                BExceptionShift busExShift = new BExceptionShift();
                busExShift.ExchangeDayByPerson(ADOPerson1.ID, Utility.ToPersianDate(DateTime.Now), date);

                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R4_LockCalculationFromCurrentMonth));
            }
        }

        [Test]
        public void R4_LockCalculationFromCurrentMonth__ShiftException_Pass_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                string date1 = String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11);
                string date2 = String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 15);

                BExceptionShift busExShift = new BExceptionShift();
                busExShift.ExchangeDayByPerson(ADOPerson1.ID, date1, date2);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.UIValidation_R4_LockCalculationFromCurrentMonth));
            }
        }
        #endregion

        #region LeaveIncDec
        [Test]
        public void R4_LockCalculationFromCurrentMonth_LeaveIncDec_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                string date = String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 9);

                BLeaveIncDec busLeave = new BLeaveIncDec();
                busLeave.InsertLeaveIncDec(ADOPerson1.ID, "2", "00:00", LeaveIncDecAction.Increase, "", date);

                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R4_LockCalculationFromCurrentMonth));
            }
        }

        [Test]
        public void R4_LockCalculationFromCurrentMonth__LeaveIncDec_Pass_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                string date = String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11);

                BLeaveIncDec busLeave = new BLeaveIncDec();

                busLeave.InsertLeaveIncDec(ADOPerson1.ID, "2", "00:00", LeaveIncDecAction.Increase, "", date);

            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.UIValidation_R4_LockCalculationFromCurrentMonth));
            }
        }

        #endregion

        #region LeaveYearRemain
        [Test]
        public void R4_LockCalculationFromCurrentMonth_LeaveYearRemain_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

                BRemainLeave busLeave = new BRemainLeave();

                busLeave.InsertLeaveYear(pd.Year, ADOPerson1.ID, "2", "00:00");

                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R4_LockCalculationFromCurrentMonth));
            }
        }

        [Test]
        public void R4_LockCalculationFromCurrentMonth__LeaveYearRemain_Pass_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

                BRemainLeave busLeave = new BRemainLeave();

                busLeave.InsertLeaveYear(pd.Year + 1, ADOPerson1.ID, "2", "00:00");

            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.UIValidation_R4_LockCalculationFromCurrentMonth));
            }
        }

        #endregion

        #region BasicTraffic
        [Test]
        public void R4_LockCalculationFromCurrentMonth_BasicTraffic_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                string date = String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 9);

                BTraffic busTraffic = new BTraffic();
                busTraffic.InsertTraffic(ADOPerson1.ID, ADOPrecardTraffic.ID, date, "01:00", "");

                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R4_LockCalculationFromCurrentMonth));
            }
        }

        [Test]
        public void R4_LockCalculationFromCurrentMonth__BasicTraffic_Pass_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                string date = String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 11);

                BTraffic busTraffic = new BTraffic();
                busTraffic.InsertTraffic(ADOPerson1.ID, ADOPrecardTraffic.ID, date, "01:00", "");

            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.UIValidation_R4_LockCalculationFromCurrentMonth));
            }
        }

        #endregion
        #endregion

        #region R5
        #region Permit
        [Test]
        public void R5_LockCalculationFromBeforeMonth_Permit_Test()
        {
            try
            {
                BPermit busPermit = new BPermit();
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                DateTime date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, 9));

                Permit permit = new Permit();
                PermitPair permitPair = new PermitPair();
                permit.IsPairly = true;
                permit.FromDate = date;
                permit.ToDate = date;
                permit.Pairs = new List<PermitPair>() { permitPair };
                permit.Person = ADOPerson1;
                permitPair.Permit = permit;
                //permitPair.RequestID = request.ID;
                permitPair.From = 100;
                permitPair.To = 200;
                permitPair.Value = 100;
                permitPair.PreCardID = ADOPrecardHourlyDuty.ID;
                permitPair.IsFilled = true;
                busPermit.SaveChanges(permit, UIActionType.ADD);

                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R5_LockCalculationFromBeforeMonth));
            }
        }

        [Test]
        public void R5_LockCalculationFromBeforeMonth__PermitPass_Test()
        {
            try
            {
                AssignWorkGroup aw = new AssignWorkGroup();
                aw.FromDate = DateTime.Now;
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                aw.UIFromDate = String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, 11);
                aw.WorkGroup = new WorkGroup() { ID = 1047 };
                aw.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                BAssignWorkGroup bus = new BAssignWorkGroup(BLanguage.CurrentSystemLanguage);
                bus.SaveChanges(aw, UIActionType.ADD);
                Assert.IsTrue(aw.ID > 0);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.UIValidation_R5_LockCalculationFromBeforeMonth));
            }
        }
        #endregion

        #region Shift Exception
        [Test]
        public void R5_LockCalculationFromBeforeMonth_ShiftException_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                string date = String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, 9);

                BExceptionShift busExShift = new BExceptionShift();
                busExShift.ExchangeDayByPerson(ADOPerson1.ID, Utility.ToPersianDate(DateTime.Now), date);

                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R5_LockCalculationFromBeforeMonth));
            }
        }

        [Test]
        public void R5_LockCalculationFromBeforeMonth__ShiftException_Pass_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                string date = String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, 11);

                BExceptionShift busExShift = new BExceptionShift();
                busExShift.ExchangeDayByPerson(ADOPerson1.ID, Utility.ToPersianDate(DateTime.Now), date);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.UIValidation_R5_LockCalculationFromBeforeMonth));
            }
        }
        #endregion

        #region LeaveIncDec
        [Test]
        public void R5_LockCalculationFromBeforeMonth_LeaveIncDec_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                string date = String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, 9);

                BLeaveIncDec busLeave = new BLeaveIncDec();
                busLeave.InsertLeaveIncDec(ADOPerson1.ID, "2", "00:00", LeaveIncDecAction.Increase, "", date);

                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R5_LockCalculationFromBeforeMonth));
            }
        }

        [Test]
        public void R5_LockCalculationFromBeforeMonth__LeaveIncDec_Pass_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                string date = String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, 11);

                BLeaveIncDec busLeave = new BLeaveIncDec();

                busLeave.InsertLeaveIncDec(ADOPerson1.ID, "2", "00:00", LeaveIncDecAction.Increase, "", date);

            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.UIValidation_R5_LockCalculationFromBeforeMonth));
            }
        }

        #endregion

        #region LeaveYearRemain
        [Test]
        public void R5_LockCalculationFromBeforeMonth_LeaveYearRemain_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

                BRemainLeave busLeave = new BRemainLeave();

                busLeave.InsertLeaveYear(pd.Year, ADOPerson1.ID, "2", "00:00");

                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R5_LockCalculationFromBeforeMonth));
            }
        }

        [Test]
        public void R5_LockCalculationFromBeforeMonth__LeaveYearRemain_Pass_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);

                BRemainLeave busLeave = new BRemainLeave();

                busLeave.InsertLeaveYear(pd.Year + 1, ADOPerson1.ID, "2", "00:00");

            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.UIValidation_R5_LockCalculationFromBeforeMonth));
            }
        }

        #endregion

        #region BasicTraffic
        [Test]
        public void R5_LockCalculationFromBeforeMonth_BasicTraffic_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                string date = String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, 9);

                BTraffic busTraffic = new BTraffic();
                busTraffic.InsertTraffic(ADOPerson1.ID, ADOPrecardTraffic.ID, date, "01:00", "");

                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R5_LockCalculationFromBeforeMonth));
            }
        }

        [Test]
        public void R5_LockCalculationFromBeforeMonth__BasicTraffic_Pass_Test()
        {
            try
            {
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
                string date = String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, 11);

                BTraffic busTraffic = new BTraffic();
                busTraffic.InsertTraffic(ADOPerson1.ID, ADOPrecardTraffic.ID, date, "01:00", "");

            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsFalse(ex.Exists(ExceptionResourceKeys.UIValidation_R5_LockCalculationFromBeforeMonth));
            }
        }

        #endregion
        #endregion

        #region R6
        public void R6_LockBaseInformationFromCurrentMonth_Test()
        {
            try
            {
                AssignWorkGroup aw = new AssignWorkGroup();
                aw.FromDate = DateTime.Now;
                PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now.AddDays(-11));
                aw.UIFromDate = String.Format("{0}/{1}/{2}", pd.Year, pd.Month, pd.Day);
                aw.WorkGroup = new WorkGroup() { ID = 1047 };
                aw.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                BAssignWorkGroup bus = new BAssignWorkGroup(BLanguage.CurrentSystemLanguage);
                bus.SaveChanges(aw, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R6_LockCalculationFromDate));
            }
        } 
        #endregion

        #region R7
        [Test]
        public void R7_LeaveRequest_Test()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now);
                request_testObject.TheToDate = Utility.ToString(DateTime.Now);
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("7", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم باید جلوگیری شود
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "14:00";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("7", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R7TrafficRequestMaxCount));
            }
        } 
        #endregion

        #region R8
        [Test]
        public void R8_TrafficRequest_Test()
        {
            try
            {
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";
                request_testObject.TheFromDate = Utility.ToString(new DateTime(2010, 5, 1));
                request_testObject.TheToDate = Utility.ToString(new DateTime(2010, 5, 1));
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("8", 0) };
                request_testObject.RegisterDate = DateTime.Now;

                busRequest.InsertRequest(request_testObject);
                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R8TrafficRequestDayTimeFinished));
            }
        } 
        #endregion

        #region R9
        [Test]
        public void R9_LeaveRequest_Before_Test()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("9", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم با موفقیت
                request_testObject.TheFromTime = "11:00";
                request_testObject.TheToTime = "12:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("9", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم و جلوگیری
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "15:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("9", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(2));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(2));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R9_Before));
            }
        }

        [Test]
        public void R9_LeaveRequest_AfterTest()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("9", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم با موفقیت
                request_testObject.TheFromTime = "11:00";
                request_testObject.TheToTime = "12:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("9", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم و جلوگیری
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "15:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("9", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-2));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-2));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);
                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R9_After));
            }
        } 
        #endregion

        #region R10
        [Test]
        public void R10_LeaveRequest_Before_Test()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("10", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم با موفقیت
                request_testObject.TheFromTime = "11:00";
                request_testObject.TheToTime = "12:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("10", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم و جلوگیری
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "15:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("10", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(2));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(2));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R10_Before));
            }
        }

        [Test]
        public void R10_LeaveRequest_AfterTest()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("10", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم با موفقیت
                request_testObject.TheFromTime = "11:00";
                request_testObject.TheToTime = "12:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("10", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم و جلوگیری
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "15:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("10", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-2));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-2));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);
                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R10_After));
            }
        }
        
        #endregion
      
        #region R11

        [Test]
        public void R11_WithoutPayLeaveRequest_Before_Test()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("11", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم با موفقیت
                request_testObject.TheFromTime = "11:00";
                request_testObject.TheToTime = "12:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("11", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم و جلوگیری
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "15:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("11", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(2));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(2));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Working!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R11_Before));
            }
        }

        [Test]
        public void R11_WithoutPayLeaveRequest_AfterTest()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("11", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم با موفقیت
                request_testObject.TheFromTime = "11:00";
                request_testObject.TheToTime = "12:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("11", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم و جلوگیری
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "15:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("11", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-2));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-2));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);
                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R11_After));
            }
        }

        #endregion

        #region R12
        [Test]
        public void R12_WithoutPayLeaveRequest_Before_Test()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("12", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم با موفقیت
                request_testObject.TheFromTime = "11:00";
                request_testObject.TheToTime = "12:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("12", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم و جلوگیری
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "15:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("12", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(2));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(2));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R12_Before));
            }
        }

        [Test]
        public void R12_WithoutPayLeaveRequest_AfterTest()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("12", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم با موفقیت
                request_testObject.TheFromTime = "11:00";
                request_testObject.TheToTime = "12:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("12", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم و جلوگیری
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "15:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("12", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-2));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-2));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);
                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R12_After));
            }
        }
        #endregion

        #region R13

        [Test]
        public void R13_EstelajiLeaveRequest_Before_Test()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("13", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم با موفقیت
                request_testObject.TheFromTime = "11:00";
                request_testObject.TheToTime = "12:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("13", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم و جلوگیری
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "15:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("13", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(2));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(2));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Working!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R13_Before));
            }
        }

        [Test]
        public void R13_EstelajiLeaveRequest_AfterTest()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("13", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم با موفقیت
                request_testObject.TheFromTime = "11:00";
                request_testObject.TheToTime = "12:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("13", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم و جلوگیری
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "15:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("13", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-2));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-2));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);
                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R13_After));
            }
        }

        #endregion

        #region R14
        [Test]
        public void R14_DailyEstelajiLeaveRequest_Before_Test()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("14", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم با موفقیت
                request_testObject.TheFromTime = "11:00";
                request_testObject.TheToTime = "12:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("14", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم و جلوگیری
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "15:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("14", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(2));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(2));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R14_Before));
            }
        }

        [Test]
        public void R14_DailyEstelajiLeaveRequest_AfterTest()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("14", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم با موفقیت
                request_testObject.TheFromTime = "11:00";
                request_testObject.TheToTime = "12:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("14", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم و جلوگیری
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "15:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("14", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-2));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-2));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);
                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R14_After));
            }
        }
        #endregion

        #region R15

        [Test]
        public void R15_OverWorkRequest_Before_Test()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("15", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم با موفقیت
                request_testObject.TheFromTime = "11:00";
                request_testObject.TheToTime = "12:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("15", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم و جلوگیری
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "15:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("15", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(2));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(2));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Working!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R15_Before));
            }
        }

        [Test]
        public void R15_OverWorkRequest_AfterTest()
        {
            try
            {
                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("15", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم با موفقیت
                request_testObject.TheFromTime = "11:00";
                request_testObject.TheToTime = "12:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("15", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم و جلوگیری
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "15:00";

                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("15", 0) };

                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-2));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-2));

                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);
                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R15_After));
            }
        }

        #endregion

        #region R16
        [Test]
        public void R16_LeaveRequestcount_Test()
        {
            try
            {
                UpdateCurrentUserPersonId(ADOPerson2.ID);

                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now);
                request_testObject.TheToDate = Utility.ToString(DateTime.Now);
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("16", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم باید جلوگیری شود
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "14:00";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("16", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R16RequestMaxCount));
            }
        }
        #endregion

        #region R17
        [Test]
        public void R17_LeaveRequestcount_Test()
        {
            try
            {
                UpdateCurrentUserPersonId(ADOPerson2.ID);

                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now);
                request_testObject.TheToDate = Utility.ToString(DateTime.Now);
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("17", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم باید جلوگیری شود
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "14:00";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("17", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R17RequestMaxCount));
            }
        }
        #endregion

        #region R18
        [Test]
        public void R18_WithoutPayRequestcount_Test()
        {
            try
            {
                UpdateCurrentUserPersonId(ADOPerson2.ID);

                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now);
                request_testObject.TheToDate = Utility.ToString(DateTime.Now);
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("18", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم باید جلوگیری شود
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "14:00";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("18", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R18RequestMaxCount));
            }
        }
        #endregion

        #region R19
        [Test]
        public void R19_WithoutPayRequestcount_Test()
        {
            try
            {
                UpdateCurrentUserPersonId(ADOPerson2.ID);

                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now);
                request_testObject.TheToDate = Utility.ToString(DateTime.Now);
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("19", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم باید جلوگیری شود
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "14:00";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("19", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R19RequestMaxCount));
            }
        }
        #endregion

        #region R20
        [Test]
        public void R20_EstelajiRequestcount_Test()
        {
            try
            {
                UpdateCurrentUserPersonId(ADOPerson2.ID);

                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now);
                request_testObject.TheToDate = Utility.ToString(DateTime.Now);
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("20", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم باید جلوگیری شود
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "14:00";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("20", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R20RequestMaxCount));
            }
        }
        #endregion

        #region R21
        [Test]
        public void R21_EstelajiRequestcount_Test()
        {
            try
            {
                UpdateCurrentUserPersonId(ADOPerson2.ID);

                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "7:00";
                request_testObject.TheToTime = "10:00";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now);
                request_testObject.TheToDate = Utility.ToString(DateTime.Now);
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("21", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست دوم باید جلوگیری شود
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "14:00";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("21", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R21RequestMaxCount));
            }
        }
        #endregion

        #region R22
        [Test]
        public void R22_EstelajiRequestcount_Test()
        {
            try
            {
                UpdateCurrentUserPersonId(ADOPerson2.ID);

                //درخواست اول با موفقیت
                request_testObject.TheTimeDuration = "00:30";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now);
                request_testObject.TheFromTime = "00:00";
                request_testObject.TheToTime = "00:00";
                request_testObject.TheToDate = Utility.ToString(DateTime.Now);
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("22", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست اول با موفقیت
                request_testObject.TheFromTime = "13:00";
                request_testObject.TheToTime = "13:25";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("22", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                request_testObject = new Request();
                ClearSession();

                //درخواست سوم باید جلوگیری شود
                request_testObject.TheFromTime = "17:00";
                request_testObject.TheToTime = "17:25";
                request_testObject.TheFromDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.TheToDate = Utility.ToString(DateTime.Now.AddDays(-1));
                request_testObject.Person = new Person() { ID = BUser.CurrentUser.Person.ID };
                request_testObject.Precard = new Precard() { ID = this.GetPrecardID("22", 0) };
                request_testObject.RegisterDate = DateTime.Now;
                busRequest.InsertRequest(request_testObject);

                Assert.Fail("UIValidation Not Workin!");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UIValidation_R22RequestMaxHrour));
            }
        }
        #endregion

        private decimal GetPrecardID(string ruleCode, int order)
        {
            DatabaseGatewayTableAdapters.TA_PrecardTableAdapter pTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PrecardTableAdapter();
            DatabaseGateway.TA_PrecardDataTable table = pTA.GetDataByUIValidationRuleCode(ruleCode);
            if (table.Rows.Count == 0)
            {
                throw new Exception(string.Format("پیشکارت مربوطه به قانون واسط کاربر با کد {0} تخصیص داده نشده است", ruleCode));
            }
            return Convert.ToDecimal(table.Rows[order][0]);
        }
    }
}
