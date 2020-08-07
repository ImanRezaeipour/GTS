using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Security;
using GTS.Clock.Business;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business.Security;

namespace GTSTestUnit.Clock.Business
{
    public class BaseFixture
    {
        #region Variables
        protected DatabaseGatewayTableAdapters.TA_PersonTableAdapter personTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_PersonDetailTableAdapter personDtilTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonDetailTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_SecurityUserTableAdapter userTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_SecurityUserTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_UserSettingsTableAdapter userSetTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UserSettingsTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_DepartmentTableAdapter departmentTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DepartmentTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_ControlStationTableAdapter sataionTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ControlStationTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_EmploymentTypeTableAdapter emplTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_EmploymentTypeTableAdapter();        
        protected DatabaseGatewayTableAdapters.TA_LeaveCalcResultTableAdapter leaveCalcTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_LeaveCalcResultTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_UsedLeaveDetailTableAdapter UsedLeaveDtlTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UsedLeaveDetailTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_ApplicationSettingsTableAdapter appSetupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ApplicationSettingsTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_LeaveYearRemainTableAdapter leaveRemTA = new DatabaseGatewayTableAdapters.TA_LeaveYearRemainTableAdapter();

        protected DatabaseGatewayTableAdapters.TA_DataAccessDepartmentTableAdapter dataAccessDepTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DataAccessDepartmentTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_DataAccessOrganizationUnitTableAdapter dataAccessOrganTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DataAccessOrganizationUnitTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_DataAccessManagerTableAdapter dataAccessMngTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DataAccessManagerTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_DataAccessWorkGroupTableAdapter dataAccessWorkGroupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DataAccessWorkGroupTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_DataAccessShiftTableAdapter dataAccessShiftTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DataAccessShiftTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_DataAccessPrecardTableAdapter dataAccessPrecardTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DataAccessPrecardTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_DataAccessCtrlStationTableAdapter dataAccessControlStationTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DataAccessCtrlStationTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_DataAccessDoctorTableAdapter dataAccessDoctorTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DataAccessDoctorTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_DataAccessRuleGroupTableAdapter dataAccessRuleTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DataAccessRuleGroupTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_DataAccessFlowTableAdapter dataAccessFlowTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DataAccessFlowTableAdapter();
        protected DatabaseGatewayTableAdapters.TA_DataAccessReportTableAdapter dataAccessReportTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DataAccessReportTableAdapter();
        protected DatabaseGateway.TA_UsedLeaveDetailDataTable UsedleaveTable = new DatabaseGateway.TA_UsedLeaveDetailDataTable();


        protected Person ADOPerson1 = new Person();
        protected Person ADOPerson2 = new Person();
        protected Person ADOPerson3 = new Person();
        protected Person ADOPerson4 = new Person();
        protected Person ADOPerson5 = new Person();
        protected Person ADOPerson6 = new Person();
        protected User ADOUser = new User();
        protected Department ADORoot = new Department();
        protected Department ADODepartment1 = new Department();
        protected Department ADODepartment2 = new Department();

        protected ControlStation ADOStaion1 = new ControlStation();
        protected ControlStation ADOStaion2 = new ControlStation();

        protected EmploymentType ADOEmploymentType1 = new EmploymentType();
        protected EmploymentType ADOEmploymentType2 = new EmploymentType();

        protected User ADOUser1 = new User();
        protected User ADOUser2 = new User();
        protected User ADOUser3 = new User();
        protected User ADOUser4 = new User();
        protected User ADOUser5 = new User();
        protected User ADOUser6 = new User(); 
        #endregion

        public bool NeedBaseInformation { get;set; }
        public BaseFixture()
        {
            this.NeedBaseInformation = false;
        }
        public BaseFixture(bool needBaseInformation)
        {
            this.NeedBaseInformation = needBaseInformation;
        }
        [TestFixtureSetUp]
        public void TestStartup() 
        {
            appSetupTA.TurnBusinessLogOff();
            BusinessServiceLogger.ResetConfiguaraion();
            
        }
       
        [TestFixtureTearDown]
        public void TestTearDown() 
        {
            appSetupTA.TurnBusinessLogOn();
            personTA.DeleteGarbagePersonDetails();
        }

        [SetUp]
        public void TestSetup()
        {
            return;
            try
            {
                if (NeedBaseInformation || true)
                {
                    #region ControlSration
                    sataionTA.Insert("ControlStationTest1", "0-0Test");
                    sataionTA.Insert("ControlStationTest2", "0-1Test");

                    DatabaseGateway.TA_ControlStationDataTable controltable = new DatabaseGateway.TA_ControlStationDataTable();
                    sataionTA.FillByCustomCode(controltable, "0-0Test");
                    ADOStaion1.ID = Convert.ToInt32(controltable.Rows[0]["station_ID"]);
                    ADOStaion1.Name = Convert.ToString(controltable.Rows[0]["station_Name"]);
                    ADOStaion1.CustomCode = Convert.ToString(controltable.Rows[0]["station_CustomCode"]);

                    sataionTA.FillByCustomCode(controltable, "0-1Test");
                    ADOStaion2.ID = Convert.ToInt32(controltable.Rows[0]["station_ID"]);
                    ADOStaion2.Name = Convert.ToString(controltable.Rows[0]["station_Name"]);
                    ADOStaion2.CustomCode = Convert.ToString(controltable.Rows[0]["station_CustomCode"]);

                    #endregion

                    #region EmplyeeType
                    emplTA.Insert("EmploymentTypeTest1", "0-0Test");
                    emplTA.Insert("EmploymentTypeTest2", "0-1Test");

                    DatabaseGateway.TA_EmploymentTypeDataTable emptable = new DatabaseGateway.TA_EmploymentTypeDataTable();
                    emplTA.FillByCustomCode(emptable, "0-0Test");
                    ADOEmploymentType1.ID = Convert.ToInt32(emptable.Rows[0]["emply_ID"]);
                    ADOEmploymentType1.Name = Convert.ToString(emptable.Rows[0]["emply_Name"]);
                    ADOEmploymentType1.CustomCode = Convert.ToString(emptable.Rows[0]["emply_CustomCode"]);

                    emplTA.FillByCustomCode(emptable, "0-1Test");
                    ADOEmploymentType2.ID = Convert.ToInt32(emptable.Rows[0]["emply_ID"]);
                    ADOEmploymentType2.Name = Convert.ToString(emptable.Rows[0]["emply_Name"]);
                    ADOEmploymentType2.CustomCode = Convert.ToString(emptable.Rows[0]["emply_CustomCode"]);


                    #endregion

                    #region Department
                    DatabaseGateway.TA_DepartmentDataTable depTable = new DatabaseGateway.TA_DepartmentDataTable();

                    depTable = departmentTA.GetRoot();
                    if (depTable.Rows.Count == 0)
                    {
                        departmentTA.InsertQuery("TestLevel1", "123", 0, "", "");
                        depTable = departmentTA.GetRoot();
                    }

                    ADORoot.ID = Convert.ToInt32(depTable.Rows[0]["dep_ID"]);
                    ADORoot.ParentID = Utility.ToInteger(depTable.Rows[0]["dep_ParentID"]);
                    ADORoot.Name = Convert.ToString(depTable.Rows[0]["dep_Name"]);
                    ADORoot.CustomCode = Convert.ToString(depTable.Rows[0]["dep_CustomCode"]);

                    departmentTA.Insert("TestLevel2_1", "1234", ADORoot.ID, "," + ADORoot.ID + ",", "");
                    departmentTA.Insert("TestLevel2_2", "1235", ADORoot.ID, "," + ADORoot.ID + ",", "");

                    departmentTA.GetByCustomCode(depTable, "1234");
                    ADODepartment1.ID = Convert.ToInt32(depTable.Rows[0]["dep_ID"]);
                    ADODepartment1.ParentID = Convert.ToInt32(depTable.Rows[0]["dep_ParentID"]);
                    ADODepartment1.Name = Convert.ToString(depTable.Rows[0]["dep_Name"]);
                    ADODepartment1.CustomCode = Convert.ToString(depTable.Rows[0]["dep_CustomCode"]);
                    ADODepartment1.ParentPath = Convert.ToString(depTable.Rows[0]["dep_ParentPath"]);

                    departmentTA.GetByCustomCode(depTable, "1235");
                    ADODepartment2.ID = Convert.ToInt32(depTable.Rows[0]["dep_ID"]);
                    ADODepartment2.ParentID = Convert.ToInt32(depTable.Rows[0]["dep_ParentID"]);
                    ADODepartment2.Name = Convert.ToString(depTable.Rows[0]["dep_Name"]);
                    ADODepartment2.CustomCode = Convert.ToString(depTable.Rows[0]["dep_CustomCode"]);

                    #endregion

                    #region Persons
                    personTA.Insert("00001", 0, true, "", ADODepartment1.ID, null, null, ADOStaion1.ID, null, ADOEmploymentType1.ID, true, "لیسانس", "TestAli1", (int)MaritalStatus.Mojarad, "TestAlian1", null);
                    personTA.Insert("00002", 0, true, "", ADODepartment2.ID, null, null, ADOStaion1.ID, null, ADOEmploymentType1.ID, true, "دیپلم", "TestAli2", (int)MaritalStatus.Motahel, "TestAlian2", null);
                    personTA.Insert("00003", 0, true, "", ADODepartment1.ID, null, null, null, null, null, true, "", "TestAli3", 1, "TestAlian3", null);
                    personTA.Insert("00004", 0, true, "", ADODepartment2.ID, null, null, null, null, null, true, "", "TestAli4", 1, "TestAlian4", null);
                    personTA.Insert("00005", 0, true, "", ADODepartment2.ID, null, null, null, null, null, true, "", "TestAli5", 1, "TestAlian5", null);
                    personTA.Insert("00006", 0, true, "", ADODepartment2.ID, null, null, null, null, null, true, "", "TestAli6", 1, "TestAlian6", null);


                    DatabaseGateway.TA_PersonDataTable personTable = personTA.GetDataByBarcode("00001");
                    ADOPerson1.ID = Convert.ToInt32(personTable.Rows[0]["prs_ID"]);
                    ADOPerson1.PersonCode = Convert.ToString(personTable.Rows[0]["prs_Barcode"]);
                    ADOPerson1.LastName = "TestAlian1";
                    personTable = personTA.GetDataByBarcode("00002");
                    ADOPerson2.ID = Convert.ToInt32(personTable.Rows[0]["prs_ID"]);
                    ADOPerson2.PersonCode = Convert.ToString(personTable.Rows[0]["prs_Barcode"]);
                    ADOPerson2.LastName = "TestAlian2";
                    personTable = personTA.GetDataByBarcode("00003");
                    ADOPerson3.ID = Convert.ToInt32(personTable.Rows[0]["prs_ID"]);
                    ADOPerson3.PersonCode = Convert.ToString(personTable.Rows[0]["prs_Barcode"]);
                    ADOPerson3.LastName = "TestAlian3";
                    personTable = personTA.GetDataByBarcode("00004");
                    ADOPerson4.ID = Convert.ToInt32(personTable.Rows[0]["prs_ID"]);
                    ADOPerson4.PersonCode = Convert.ToString(personTable.Rows[0]["prs_Barcode"]);
                    ADOPerson4.LastName = "TestAlian4";
                    personTable = personTA.GetDataByBarcode("00005");
                    ADOPerson5.ID = Convert.ToInt32(personTable.Rows[0]["prs_ID"]);
                    ADOPerson5.PersonCode = Convert.ToString(personTable.Rows[0]["prs_Barcode"]);
                    ADOPerson5.LastName = "TestAlian5";
                    personTable = personTA.GetDataByBarcode("00006");
                    ADOPerson6.ID = Convert.ToInt32(personTable.Rows[0]["prs_ID"]);
                    ADOPerson6.PersonCode = Convert.ToString(personTable.Rows[0]["prs_Barcode"]);
                    ADOPerson6.LastName = "TestAlian6";

                    personDtilTA.InsertQuery2(ADOPerson1.ID, "001232130", "23432", "", "", "HassanTest", (int)MilitaryStatus.HeineKhedmat, "", "", "", "", DateTime.Now);
                    personDtilTA.InsertQuery2(ADOPerson2.ID, "001434130", "11432", "", "", "HassanTest", (int)MilitaryStatus.AmadeBeKhedmat, "", "", "", "", DateTime.Now);
                    personDtilTA.InsertQuery2(ADOPerson3.ID, "001434165", "11532", "", "", "HassanTest", (int)MilitaryStatus.AmadeBeKhedmat, "", "", "", "", DateTime.Now);
                    personDtilTA.InsertQuery2(ADOPerson4.ID, "001434100", "11532", "", "", "HassanTest", (int)MilitaryStatus.AmadeBeKhedmat, "", "", "", "", DateTime.Now);
                    personDtilTA.InsertQuery2(ADOPerson5.ID, "001434101", "11532", "", "", "HassanTest", (int)MilitaryStatus.AmadeBeKhedmat, "", "", "", "", DateTime.Now);
                    personDtilTA.InsertQuery2(ADOPerson6.ID, "001434102", "11532", "", "", "HassanTest", (int)MilitaryStatus.AmadeBeKhedmat, "", "", "", "", DateTime.Now);

                    personTA.UpdateDetails(ADOPerson1.ID, "001232130");
                    personTA.UpdateDetails(ADOPerson2.ID, "001434130");
                    personTA.UpdateDetails(ADOPerson3.ID, "001434165");
                    personTA.UpdateDetails(ADOPerson4.ID, "001434100");
                    personTA.UpdateDetails(ADOPerson5.ID, "001434101");
                    personTA.UpdateDetails(ADOPerson6.ID, "001434102");

                    #endregion

                    #region user
                    userTA.InsertQuery(ADOPerson1.ID, "TestUserName1");
                    userTA.InsertQuery(ADOPerson2.ID, "TestUserName2");
                    userTA.InsertQuery(ADOPerson3.ID, "TestUserName3");
                    userTA.InsertQuery(ADOPerson4.ID, "TestUserName4");
                    userTA.InsertQuery(ADOPerson5.ID, "TestUserName5");
                    userTA.InsertQuery(ADOPerson6.ID, "TestUserName6");
                    DatabaseGateway.TA_SecurityUserDataTable userTable;
                    userTable = userTA.GetDataByUserName("TestUserName1");
                    ADOUser1.ID = Convert.ToInt32(userTable.Rows[0]["user_ID"]);
                    ADOUser1.UserName = "TestUserName1";
                    userTable = userTA.GetDataByUserName("TestUserName2");
                    ADOUser2.ID = Convert.ToInt32(userTable.Rows[0]["user_ID"]);
                    ADOUser2.UserName = "TestUserName2";
                    userTable = userTA.GetDataByUserName("TestUserName3");
                    ADOUser3.ID = Convert.ToInt32(userTable.Rows[0]["user_ID"]);
                    ADOUser3.UserName = "TestUserName3";
                    userTable = userTA.GetDataByUserName("TestUserName4");
                    ADOUser4.ID = Convert.ToInt32(userTable.Rows[0]["user_ID"]);
                    ADOUser4.UserName = "TestUserName4";
                    userTable = userTA.GetDataByUserName("TestUserName5");
                    ADOUser5.ID = Convert.ToInt32(userTable.Rows[0]["user_ID"]);
                    ADOUser5.UserName = "TestUserName5";
                    userTable = userTA.GetDataByUserName("TestUserName6");
                    ADOUser6.ID = Convert.ToInt32(userTable.Rows[0]["user_ID"]);
                    ADOUser6.UserName = "TestUserName6";
                    #endregion

                    #region DataAccess
                    dataAccessDepTA.Insert(BUser.CurrentUser.ID, ADODepartment1.ID, false);
                    dataAccessDepTA.Insert(BUser.CurrentUser.ID, ADODepartment2.ID, false);
                    //dataAccessMngTA.Insert(BUser.CurrentUser.ID, null, true);
                    #endregion

                    #region Calc Leave
                    UsedLeaveDtlTA.Insert(DateTime.Now.AddYears(-1), 0, 0, 0, ADOPerson1.ID, 0);
                    UsedleaveTable = UsedLeaveDtlTA.GetDataByValue(ADOPerson1.ID);
                    leaveCalcTA.Insert(DateTime.Now.AddYears(-1), 0, 0, ADOPerson1.ID, 0, 0, 0, 0, 0, (decimal)UsedleaveTable.Rows[0][0], "ULD");

                    UsedLeaveDtlTA.Insert(DateTime.Now.AddYears(-1), 0, 0, 0, ADOPerson2.ID, 0);
                    UsedleaveTable = UsedLeaveDtlTA.GetDataByValue(ADOPerson2.ID);
                    leaveCalcTA.Insert(DateTime.Now.AddYears(-1), 0, 0, ADOPerson2.ID, 0, 0, 0, 0, 0, (decimal)UsedleaveTable.Rows[0][0], "ULD");

                    UsedLeaveDtlTA.Insert(DateTime.Now.AddYears(-1), 0, 0, 0, ADOPerson3.ID, 0);
                    UsedleaveTable = UsedLeaveDtlTA.GetDataByValue(ADOPerson3.ID);
                    leaveCalcTA.Insert(DateTime.Now.AddYears(-1), 0, 0, ADOPerson3.ID, 0, 0, 0, 0, 0, (decimal)UsedleaveTable.Rows[0][0], "ULD");

                    UsedLeaveDtlTA.Insert(DateTime.Now.AddYears(-1), 0, 0, 0, ADOPerson4.ID, 0);
                    UsedleaveTable = UsedLeaveDtlTA.GetDataByValue(ADOPerson4.ID);
                    leaveCalcTA.Insert(DateTime.Now.AddYears(-1), 0, 0, ADOPerson4.ID, 0, 0, 0, 0, 0, (decimal)UsedleaveTable.Rows[0][0], "ULD");

                    UsedLeaveDtlTA.Insert(DateTime.Now.AddYears(-1), 0, 0, 0, ADOPerson5.ID, 0);
                    UsedleaveTable = UsedLeaveDtlTA.GetDataByValue(ADOPerson5.ID);
                    leaveCalcTA.Insert(DateTime.Now.AddYears(-1), 0, 0, ADOPerson5.ID, 0, 0, 0, 0, 0, (decimal)UsedleaveTable.Rows[0][0], "ULD");
                    #endregion
                }
            }
            catch (Exception ex) 
            {
                personTA.DeleteByBarcode("00001");
                personTA.DeleteByBarcode("00002");
                personTA.DeleteByBarcode("00003");
                personTA.DeleteByBarcode("00004");
                personTA.DeleteByBarcode("00005");
                personTA.DeleteByBarcode("00006");

                personDtilTA.DeleteByFatherName("HassanTest");

                throw ex;
            }
        }

        [TearDown]
        public void TreatDown()
        {
            dataAccessDepTA.DeleteByUSerId(BUser.CurrentUser.ID);
            dataAccessOrganTA.DeleteQuery(BUser.CurrentUser.ID);
            dataAccessShiftTA.DeleteByShiftId(BUser.CurrentUser.ID);
            dataAccessWorkGroupTA.DeleteByUserId(BUser.CurrentUser.ID);
            dataAccessPrecardTA.DeleteQuery(BUser.CurrentUser.ID);
            dataAccessControlStationTA.DeleteByUserId(BUser.CurrentUser.ID);
            dataAccessDoctorTA.DeleteByUserID(BUser.CurrentUser.ID);
            dataAccessMngTA.DeleteQuery(BUser.CurrentUser.ID);
            dataAccessRuleTA.DeleteQuery(BUser.CurrentUser.ID);
            dataAccessFlowTA.DeleteQuery(BUser.CurrentUser.ID);
            dataAccessReportTA.DeleteQuery(BUser.CurrentUser.ID);
          

            userTA.UpdateUserPerson(32688, BUser.CurrentUser.ID);//salavati

            UsedLeaveDtlTA.DeleteByPersonId(ADOPerson1.ID);
            UsedLeaveDtlTA.DeleteByPersonId(ADOPerson2.ID);
            UsedLeaveDtlTA.DeleteByPersonId(ADOPerson3.ID);
            UsedLeaveDtlTA.DeleteByPersonId(ADOPerson4.ID);
            UsedLeaveDtlTA.DeleteByPersonId(ADOPerson5.ID);

            leaveCalcTA.DeleteByPerson(ADOPerson1.ID);
            leaveCalcTA.DeleteByPerson(ADOPerson2.ID);
            leaveCalcTA.DeleteByPerson(ADOPerson3.ID);
            leaveCalcTA.DeleteByPerson(ADOPerson4.ID);
            leaveCalcTA.DeleteByPerson(ADOPerson5.ID);

            leaveRemTA.DeleteByPersonId(ADOPerson1.ID);
            leaveRemTA.DeleteByPersonId(ADOPerson2.ID);
            leaveRemTA.DeleteByPersonId(ADOPerson3.ID);
            leaveRemTA.DeleteByPersonId(ADOPerson4.ID);
            leaveRemTA.DeleteByPersonId(ADOPerson5.ID);

            personTA.DeleteByBarcode("00001");
            personTA.DeleteByBarcode("00002");
            personTA.DeleteByBarcode("00003");
            personTA.DeleteByBarcode("00004");
            personTA.DeleteByBarcode("00005");
            personTA.DeleteByBarcode("00006");
            personTA.DeleteByBarcode("00001222");

            personDtilTA.DeleteByFatherName("HassanTest");           

            departmentTA.DeleteByCustomCode("123");
            departmentTA.DeleteByCustomCode("1234");
            departmentTA.DeleteByCustomCode("1235");
            departmentTA.DeleteByCustomCode("0-0Test");
            departmentTA.DeleteByCustomCode("test_0-0");

            sataionTA.DeleteByCustomCode("0-0Test");
            sataionTA.DeleteByCustomCode("0-1Test");
            emplTA.DeleteByCustomCode("0-0Test");
            emplTA.DeleteByCustomCode("0-1Test");

            userTA.DeleteByUsername(ADOUser1.UserName);
            userTA.DeleteByUsername(ADOUser2.UserName);
            userTA.DeleteByUsername(ADOUser3.UserName);
            userTA.DeleteByUsername(ADOUser4.UserName);
            userTA.DeleteByUsername(ADOUser5.UserName);
            userTA.DeleteByUsername(ADOUser6.UserName);

            ClearSession();
        }

        protected void ClearSession() 
        {
            NHibernateSessionManager.Instance.GetSession().Clear();
        }

        protected void UpdateCurrentUserPersonId(decimal personId) 
        {
            userTA.UpdateUserPerson(personId, BUser.CurrentUser.ID);
            SessionHelper.ClearSessionValue(SessionHelper.BussinessCurentUser);
            ClearSession();
        }

        protected void UpdateCurrentUserPersonId(decimal personId,decimal userrSttingsId)
        {
            userTA.UpdateUserPerson(personId, BUser.CurrentUser.ID);
            userSetTA.UpdateUserId(BUser.CurrentUser.ID, userrSttingsId);
            SessionHelper.ClearSessionValue(SessionHelper.BussinessCurentUser);
            ClearSession();
        }
    }
}
