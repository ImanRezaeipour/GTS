using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business.BoxService;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Leave;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business.GridSettings;
using GTS.Clock.Model.UI;
using GTS.Clock.Business.Engine;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model;
using GTS.Clock.Business.Assignments;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Business.Rules;
using GTS.Clock.Model.Security;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.RequestFlow;

namespace TestWebApplication
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {      
        }

        protected void userSettingSaveBtn_Click(object sender, EventArgs e)
        {
            BUserSettings bus = new BUserSettings();
            EmailSettings setting = new EmailSettings() { Active = true, SendByDay = false, TheHour = "10:00" };
            SMSSettings setting1 = new SMSSettings() { Active = true, SendByDay = true, TheDayHour = "10:00" };
            bus.SaveEmailSetting(setting);
            bus.SaveSMSSetting(setting1);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BUserSettings bus = new BUserSettings();
            bus.GetEmailSetting();
            bus.GetSMSSetting();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BPersonelInfoBoxService bus = new  BPersonelInfoBoxService();
            bus.GetPersonInfo();

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            LanguagesName l = GTS.Clock.Business.AppSettings.BLanguage.CurrentLocalLanguage;
            PersianDateTime dt = new PersianDateTime(DateTime.Now);
            BPersonMonthlyWorkedTime pb = 
                new BPersonMonthlyWorkedTime(BUser.CurrentUser.Person.ID);
            IList<PersonalMonthlyReportRow> list1;
            PersonalMonthlyReportRow monthlyRow;
            pb.GetPersonMonthlyReport(1391, 5, "1391/07/20", "1391/08/19", out list1, out monthlyRow);

            string value = monthlyRow.HourlyPureOperation;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            BLeaveBudget bus = new BLeaveBudget();
            LeaveBudgetProxy p = new LeaveBudgetProxy();
            p.BudgetType = BudgetType.Usual;
            p.TotoalDay = "26"; ;
            p.TotoalTime = "00:00";
            bus.SaveBudget(210, 1391, p);

         
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            BWorkGroupCalendar bus = new BWorkGroupCalendar();
            IList<CalendarCellInfo> list=new List<CalendarCellInfo>();
            IList<decimal> holidayTypes = new List<decimal>();
            list.Add(new CalendarCellInfo() { Month = 1, ShiftID = 6060, Title = "", Color = null, Day = 5 });
            list.Add(new CalendarCellInfo() { Month = 1, ShiftID = 6060, Title = "", Color = null, Day = 6 });
            list.Add(    new CalendarCellInfo() { Month = 1, ShiftID = 6060, Title = "", Color = null, Day = 7 });
            list.Add(new CalendarCellInfo() { Month = 1, ShiftID = 6060, Title = "", Color = null, Day = 8 });
            list.Add(new CalendarCellInfo() { Month = 1, ShiftID = 6060, Title = "", Color = null, Day = 9 });
            list.Add(new CalendarCellInfo() { Month = 1, ShiftID = 6060, Title = "", Color = null, Day = 10 });
            list.Add(new CalendarCellInfo() { Month = 1, ShiftID = 6060, Title = "", Color = null, Day = 1 });
            list.Add(new CalendarCellInfo() { Month = 1, ShiftID = 6060, Title = "", Color = null, Day = 12 });
            list.Add(new CalendarCellInfo() { Month = 1, ShiftID = 6060, Title = "", Color = null, Day = 13 });
            bus.RepetitionPeriod(1391, 1, 7, 1, 1, holidayTypes, list);


        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            BGridMonthlyOperationClientSettings bus = new BGridMonthlyOperationClientSettings();
            bus.GetMonthlyOperationGridClientSettings();

            bus.GetMonthlyOperationGridGeneralClientSettings();
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            BGridMonthlyOperationClientSettings bus = new BGridMonthlyOperationClientSettings();
            //MonthlyOperationGridClientSettings settting = bus.GetMonthlyOperationGridClientSettings();
            //NHibernateSessionManager.Instance.ClearSession();
            MonthlyOperationGridClientSettings settting = new MonthlyOperationGridClientSettings();
            settting.ID = 555;
            settting.ThirdExit = true;
            bus.SaveChanges(settting, GTS.Clock.Business.UIActionType.EDIT);
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            PersianDateTime dt = new PersianDateTime(DateTime.Now);
            BEngineCalculator pb = new BEngineCalculator();
            pb.Calculate(32687, "1391/06/01","1392/01/01", false);
          
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            BEngineCalculator pb = new BEngineCalculator();
            PersonRepository pr = new PersonRepository(false);
            Person p = pr.GetByBarcode(barcodeTB.Text);
            pb.Calculate(p.ID, Utility.ToPersianDate(DateTime.Now), "1393/01/01", false);
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            BAssignDateRange bus = new BAssignDateRange(LanguagesName.Parsi);
            PersonRangeAssignment p = new PersonRangeAssignment() { ID = 85916, UIFromDate = "1388/01/01", Person = new Person() { ID = 32700 }, CalcDateRangeGroup = new CalculationRangeGroup() { ID = 26017 } };
            bus.SaveChanges(p, GTS.Clock.Business.UIActionType.EDIT);
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            BWorkedTime bus = new BWorkedTime();
            bus.GetUnderManagmentByDepartment(2, 57, 0, 30, GridOrderFields.NONE, GridOrderFieldType.asc);
            bus.GetUnderManagmentBySearch(2, "3551329", GridSearchFields.PersonCode, 0, 30, GridOrderFields.NONE, GridOrderFieldType.asc);
            bus.GetUnderManagmentBySearchCount(2, "3551329", GridSearchFields.PersonCode);
        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            BUserInfo info = new BUserInfo();
            IList<string> list = info.GetUserInfo(BUser.CurrentUser.Person.ID);

        }

        protected void Button13_Click(object sender, EventArgs e)
        {
            BUser bus = new BUser();
            bus.GetActiveDirectoryUsers(1);
            bus.InsertUser(new UserProxy() { PersonID = 32688, RoleID = 1, UserName = "asdasd", Password = "123", ConfirmPassword = "123", ActiveDirectoryAuthenticate = false, Active = true });
        }

        protected void Button14_Click(object sender, EventArgs e)
        {
            BUserInfo bus = new BUserInfo();
            IList<string> s = bus.GetUserInfo(226950);
        }

        protected void Button15_Click(object sender, EventArgs e)
        {
            BWorkedTime bus = new BWorkedTime();
            IList<UnderManagementPerson> list = bus.GetUnderManagmentBySearch(0, "2197", GridSearchFields.NONE, 0, 10, GridOrderFields.NONE, GridOrderFieldType.asc);

        }

        protected void Button16_Click(object sender, EventArgs e)
        {
            BMainPageBox bus = new BMainPageBox();
            bus.GetKartablSummary();
        }

        protected void Button17_Click(object sender, EventArgs e)
        {
            IRegisteredRequests reg = new BKartabl();
            GTS.Clock.Model.RequestFlow.Request req = new GTS.Clock.Model.RequestFlow.Request();
            req.TheFromDate = req.TheToDate = "1391/07/18";
            req.TheFromTime = "13:00";
            req.TheToTime = "17:00";
            req.Precard = new Precard() { ID = 51 };
            req.IsDateSetByUser = true;
            reg.InsertCollectiveRequest(req, new PersonAdvanceSearchProxy() {DepartmentId=221 }, new List<decimal>() { 32660, 234101 }, 1391, 7);
        }

        protected void Button18_Click(object sender, EventArgs e)
        {
            IKartablRequests reg = new BKartabl();
            reg.GetRequestHistory(43606);
        }

        protected void Button19_Click1(object sender, EventArgs e)
        {
            IReviewedRequests reg = new BKartabl();
            int a = reg.GetRequestCount("", 1392, 2);
            a = reg.GetRequestCount(RequestState.UnKnown, 1392, 2);
            reg.GetAllRequests("", 1392, 3, 0, 13, KartablOrderBy.RequestDate);
            reg.GetAllRequests("مهدی", 1392, 3, 0, 13, KartablOrderBy.RequestDate);
            reg.GetAllRequests(RequestState.UnKnown, 1392, 2, 0, 15, KartablOrderBy.RequestDate);
        }

        protected void Button20_Click(object sender, EventArgs e)
        {
            BDataAccess bus = new BDataAccess();
            bus.GetOrganizationOfUser(40468, 0);
        }

        protected void Button21_Click(object sender, EventArgs e)
        {
           
        }

        protected void Button22_Click1(object sender, EventArgs e)
        {
           
        }

        protected void Button22_Click(object sender, EventArgs e)
        {
            IKartablRequests reg = new BKartabl();
            reg.GetRequestCount("123", 1392, 2);
            reg.GetRequestCount(RequestType.None, 1392, 2);
            reg.GetAllRequests(RequestType.None, 1392, 2, 0, 20, KartablOrderBy.RequestDate);
            reg.GetAllRequests("123", 1392, 2, 0, 20, KartablOrderBy.RequestDate);
        }

        protected void Button23_Click(object sender, EventArgs e)
        {
            ISearchPerson PersonnelSearchBusiness = new BPerson();
            //IList<Person> list = PersonnelSearchBusiness.QuickSearchByPage(0, 20, "62620");
            //IList<Department> departmentsList = PersonnelSearchBusiness.GetAllDepartments();
            //Department rootDep = PersonnelSearchBusiness.GetDepartmentRoot();
            IList<Person> prsList = PersonnelSearchBusiness.QuickSearchByPage(0, 1000, "");
        }

        protected void Button24_Click(object sender, EventArgs e)
        {
            IKartablRequests bus = new BKartabl();
            int countr = bus.GetRequestCount("2111", 1391, 8);
            IList<KartablProxy> list = bus.GetAllRequests(RequestType.None, 1391, 8, 0, 10, KartablOrderBy.PersonCode);
            IList<KartablProxy> list2 = bus.GetAllRequests("2111", 1391, 8, 0, 10, KartablOrderBy.PersonCode);
        }

        protected void Button25_Click(object sender, EventArgs e)
        {
            BWorkedTime s = new BWorkedTime();
            s.GetUnderManagmentBySearch(8, "کریمی", GridSearchFields.NONE, 0, 10, GridOrderFields.NONE, GridOrderFieldType.asc);

        }

        protected void Button26_Click(object sender, EventArgs e)
        {
            IRegisteredRequests bus = new BKartabl();
            GTS.Clock.Model.RequestFlow.Request req = new GTS.Clock.Model.RequestFlow.Request();
            req.Precard = new Precard() { ID = 8832 };
            req.RegisterDate = DateTime.Now;
            req.TheFromDate = "1391/09/01";
            req.TheToDate = "1391/09/01";
            req.TheFromTime = "09:00";
            req.TheToTime = "10:00";
            bus.InsertRequest(req, 1391, 8, 32688);
        }

        protected void Button27_Click(object sender, EventArgs e)
        {
            BAssignRule bus = new BAssignRule(BLanguage.CurrentSystemLanguage);
            bus.GetAll(0);
        }

        protected void Button28_Click(object sender, EventArgs e)
        {
            IRegisteredRequests req = new BKartabl();
            GTS.Clock.Model.RequestFlow.Request request=new GTS.Clock.Model.RequestFlow.Request();
            request.AddClientSide=true;
            request.TheFromDate = "1392/02/30";
            request.TheToDate = "1392/02/30";
            request.TheFromTime = "";
            request.TheToTime = "10:00";
            request.TheTimeDuration = "00:00";
            request.Precard = new Precard() { ID = 8832 };
            request.ContinueOnTomorrow = true;
            req.InsertRequest(request, 1392, 2);
        }

        protected void Button29_Click(object sender, EventArgs e)
        {
            //IKartablRequests kartabl = new BKartabl();
            //kartabl.GetRequestLevels(43675, 43062);
        }

        protected void Button30_Click(object sender, EventArgs e)
        {
            BDepartment bus = new BDepartment();
            Department rootDep = bus.GetDepartmentsTree();
            //IList<Department> list = bus.GetAll();
            GetDepChilds(rootDep/*, list*/);
        }

        private void GetDepChilds(Department parentDepartment/*, IList<Department> allNodes*/) 
        {
            BDepartment bus = new BDepartment();
            foreach (Department childDep in bus.GetDepartmentChilds(parentDepartment.ID))
            {
                if (bus.GetDepartmentChilds(childDep.ID).Count > 0)
                    this.GetDepChilds(childDep);

            }
/*
            foreach (Department childDep in bus.GetDepartmentChilds(parentDepartment.ID,allNodes))
            {
                if (bus.GetDepartmentChilds(childDep.ID, allNodes).Count > 0)
                    this.GetDepChilds(childDep, allNodes);

            }*/
        }

        protected void Button31_Click(object sender, EventArgs e)
        {
            IRegisteredRequests bus = new BKartabl();
            bus.GetAllByPage(0, 10, 1391, 10, "");
            IList<ContractKartablProxy> confirrmedList = bus.GetAllUserRequests(RequestState.Confirmed, DateTime.Now, DateTime.Now, 32682);
            IList<ContractKartablProxy> notConfirrmedList = bus.GetAllUserRequests(RequestState.Unconfirmed, DateTime.Now, DateTime.Now, 32682);
        }

        protected void Button32_Click(object sender, EventArgs e)
        {
            BFlow busFlow = new BFlow();
            busFlow.SearchFlow(FlowSearchFields.AccessGroupName, "بدون");
        }

        protected void Button34_Click(object sender, EventArgs e)
        {
            ISearchPerson search = new BPerson();
            PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy();
            proxy.RuleGroupId = 41467;
            proxy.RuleGroupFromDate = "2012/12/15";
            proxy.RuleGroupToDate = "2012/12/20";
            int count = search.GetPersonInAdvanceSearchCount(proxy);
            IList<Person> list = search.GetPersonInAdvanceSearch(proxy,0,10);
        }

        protected void Button35_Click(object sender, EventArgs e)
        {
            BWorkedTime bus = new BWorkedTime();
            bus.GetUnderManagmentBySearchCount(10, "رضایی", GridSearchFields.NONE);
        }

        protected void Button36_Click(object sender, EventArgs e)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();
                resultTB.Text = "OK";
            }
            catch
            {
                resultTB.Text = "Error!";
            }
        }

        protected void Button37_Click(object sender, EventArgs e)
        {
            BPersonMonthlyWorkedTime bus = new BPersonMonthlyWorkedTime(BUser.CurrentUser.Person.ID);
            bus.GetPersonGanttChart(1392, 1, "1391/12/21", "1392/01/20");
        }

        protected void Button38_Click(object sender, EventArgs e)
        {
            IRegisteredRequests req = new BKartabl();
            GTS.Clock.Model.RequestFlow.Request request = new GTS.Clock.Model.RequestFlow.Request();
            request.AddClientSide = true;
            request.TheFromDate = "1391/10/01";
            request.TheToDate = "1391/10/30";
            request.TheFromTime = "00:00";
            request.TheToTime = "00:00";
            request.TheTimeDuration = "10:00";
            request.Precard = new Precard() { ID = 62985 };
            req.InsertRequest(request, 1391, 10, 32688);
        }

        protected void Button39_Click(object sender, EventArgs e)
        {
            BPerson bus = new BPerson();
            bus.GetAllByPage(100, 0);
        }

        protected void Button40_Click(object sender, EventArgs e)
        {
            BGanttChartClientSettings bus = new BGanttChartClientSettings("asdasd");
            bus.GetGanttChartClientSettings();
        }

        protected void Button41_Click(object sender, EventArgs e)
        {
            BUser bus = new BUser();
            GTS.Clock.Model.Security.User user=bus.GetByID(15);
            GTS.Clock.Model.AppSetting.UserSettings set = user.UserSetting;
        }

        protected void Button42_Click(object sender, EventArgs e)
        {

            #region SetGridColumnsSize_MasterMonthlyOperation
            BGridMonthlyOperationClientSettings MonthlyOperationGridClientSettingsBusiness = new BGridMonthlyOperationClientSettings();
            MonthlyOperationGridClientGeneralSettings monthlyOperationGridClientGeneralSettings = MonthlyOperationGridClientSettingsBusiness.GetMonthlyOperationGridGeneralClientSettings();
            #endregion

            #region SetVisibleColumns_MasterMonthlyOperation
            MonthlyOperationGridClientSettings monthlyOperationGridClientSettings = MonthlyOperationGridClientSettingsBusiness.GetMonthlyOperationGridClientSettings();
            #endregion

            #region SetReserveFieldsHeaderColumnsCaption_MasterMonthlyOperation
            BPersonMonthlyWorkedTime MonthlyOperationBusiness = new BPersonMonthlyWorkedTime(0);
            IDictionary<ConceptReservedFields, string> dic = MonthlyOperationBusiness.GetReservedFieldsNames();
           
            /*foreach (string conceptReservedFieldName in Enum.GetNames(typeof(ConceptReservedFields)))
            {
                string HeadingText = MonthlyOperationBusiness.GetReservedFieldsName((ConceptReservedFields)Enum.Parse(typeof(ConceptReservedFields), conceptReservedFieldName));
            }*/
            #endregion

            #region Fill_GridMasterMonthlyOperation_MasterMonthlyOperation
            IList<PersonalMonthlyReportRow> PersonnelMonthlyOperationList = null;
            PersonalMonthlyReportRow PersonnelSummaryMonthlyOperation = null;
            try
            {
                decimal PersonnelID = BUser.CurrentUser.Person.ID;

                BPersonMonthlyWorkedTime MonthlyOperationBusiness1 = new BPersonMonthlyWorkedTime(PersonnelID);
                MonthlyOperationBusiness.GetPersonMonthlyReport(1391, 12, "1391/12/01", "1391/12/29", out PersonnelMonthlyOperationList, out PersonnelSummaryMonthlyOperation);

                Dictionary<string, object> MonthlyOperationSourceDic = new Dictionary<string, object>();
                MonthlyOperationSourceDic.Add("Details", PersonnelMonthlyOperationList);
                MonthlyOperationSourceDic.Add("Summary", PersonnelSummaryMonthlyOperation);
            }
            catch { }

            #endregion

        }

        protected void Button43_Click(object sender, EventArgs e)
        {
            BRuleCategory bus = new BRuleCategory();
            RuleCategory cat = new RuleCategory();
            cat.Name = "sadfdsfsd";
            cat.InsertedTemplateIDs = new decimal[] { 1 };
            bus.SaveChanges(cat, UIActionType.ADD);

        }

        protected void Button44_Click(object sender, EventArgs e)
        {
            BRemainLeave bus = new BRemainLeave();
            //bus.GetRemainLeave(1392, 1393, 0, 10);
            bus.TransferToNextYear("", 1391, 1392);

        }

        protected void Button45_Click(object sender, EventArgs e)
        {
            BPerson bus = new BPerson();
            bus.CreateWorkingPerson();

            Person prs= bus.GetByID(870);
            //NHibernateSessionManager.Instance.ClearSession();

            prs.Sex = PersonSex.Male;
            bus.UpdatePerson(prs, UIActionType.EDIT);

        }
       
        protected void Button46_Click(object sender, EventArgs e)
        {
            BPerson bus = new BPerson();
            Person prs = new Person();
            prs.ID = 516;
            bus.DeletePerson(prs, UIActionType.DELETE);

        }

        protected void Button47_Click(object sender, EventArgs e)
        {
            BPrecard bus = new BPrecard();
            IList<Role> roleList = new List<Role>();
            roleList.Add(new Role() { ID = 2 });
            roleList.Add(new Role() { ID = 4 });
            roleList.Add(new Role() { ID = 5 });
            roleList.Add(new Role() { ID = 6 });

            bus.UpdateRoleAccess(2, roleList);
            NHibernateSessionManager.Instance.ClearSession();
            BRole bus2 = new BRole();
            Role r = bus2.GetByID(2);

            NHibernateSessionManager.Instance.ClearSession();
            BRequest u2 = new BRequest();
            u2.GetAllDailyLeaves();
        }

        protected void Button48_Click(object sender, EventArgs e)
        {
            BWorkedTime bus = new BWorkedTime();
            bus.GetManagerDepartmentTree();
        }

        protected void Button49_Click(object sender, EventArgs e)
        {
            PersonAdvanceSearchProxy pas = new PersonAdvanceSearchProxy() { RuleGroupId = 26, PersonActivateState = true };
            IList<ChangeInfoErrorProxy> msg = new List<ChangeInfoErrorProxy>() ;
            OrganicInfoProxy info = new OrganicInfoProxy() { RuleGroupID = 27, RuleGroupFromDate = "1392/07/01"/*, RuleGroupToDate = "1392/08/01"*/ };
            BChangeOrganicInfo bus = new BChangeOrganicInfo();
            bus.ChangeInfo(pas, info, out msg);
        }

        protected void Button50_Click(object sender, EventArgs e)
        {
            BSubstitute bus = new BSubstitute();
            Substitute sub = new Substitute();
            sub.Person = new Person() { ID = 1436 };
            sub.ManagerPersonId = 556;
            sub.TheFromDate = "1392/03/01";
            sub.TheToDate = "1392/05/01";
            sub.Active = true;
            bus.SaveChanges(sub, UIActionType.ADD);
        }
        protected void Button51_Click(object sender, EventArgs e)
        {
            BPrivateMessage bus = new BPrivateMessage();
            IList<PersonDepartmentProxy> list=new List<PersonDepartmentProxy>();
            list.Add(new PersonDepartmentProxy() { ContainsInnerchilds = false, PersonId = 870, DepartmentId = 54 });
            bus.NewMessage("asdsad", "asdsad", list);
        }
    }
}