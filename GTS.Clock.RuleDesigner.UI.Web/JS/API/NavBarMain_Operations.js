
function NavBarMain_onItemSelect(sender, e) {
    NavBarMain_onItemSelect_Operations(e.get_item());
}

function NavBarMain_onItemSelect_Operations(nvbItem) {
    if (nvbItem.get_parentItem() != null) {
        var NavBarItemID = nvbItem.get_id();
        var NavBarItemText = nvbItem.get_text();
        var nvbItemBaseText = NavBarItemID.substring(7, NavBarItemID.length - 11);

        if (CheckException_nvbItem_NavBarMain(NavBarItemID))
            return;

        if (TabStripMenus.get_tabs().getTabById("tb" + nvbItemBaseText + "_TabStripMenus") == null) {

            TabStripMenus.beginUpdate();
            var NewTab = new ComponentArt.Web.UI.TabStripTab();
            var NewTabID = "tb" + nvbItemBaseText + "_TabStripMenus";
            //var NewTabText = NavBarItemText + '&nbsp;&nbsp;&nbsp;<span><img alt="" src="Images/TabStrip/tabBeforeClose.png" onclick=imgClose_TabStripTab_TabStripMenus_onclick("' + NewTabID + '") onmouseover=this.src="Images/TabStrip/tabClose.png" onmouseout=this.src="Images/TabStrip/tabBeforeClose.png" /></span>';
            var NewTabText = NavBarItemText;

            NewTab.set_text(NewTabText);
            NewTab.set_id(NewTabID);
            var pgvID = "pgv" + nvbItemBaseText;
            NewTab.set_pageViewId(pgvID);

            PageView_onBeforeShow(NewTab);
            MultiPageMenus.findPageById(pgvID).Show();

            TabStripMenus.get_tabs().add(NewTab);
            for (var i = 0; i < TabStripMenus.get_tabs().get_length() ; i++) {
                if (TabStripMenus.get_tabs().getTab(i).get_id() == NewTab.get_id()) {
                    TabStripMenus.get_tabs().getTab(i).select();
                    break;
                }
            }
            TabStripMenus.endUpdate();
        }
        else
            TabStripMenus.get_tabs().getTabById("tb" + nvbItemBaseText + "_TabStripMenus").select();
    }
}

function PageView_onBeforeShow(NewTab) {
    DialogLoading.Show();

    switch (NewTab.get_id()) {
        case 'tbWelcome_TabStripMenus':
            break;
        case 'tbShiftIntroduction_TabStripMenus':
            document.getElementById('pgvShiftIntroduction_iFrame').src = "Shifts.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbDepartmentsIntroduction_TabStripMenus':
            document.getElementById('pgvDepartmentsIntroduction_iFrame').src = "Departments.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbPostsIntroduction_TabStripMenus':
            document.getElementById('pgvPostsIntroduction_iFrame').src = "OrganizationPosts.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbEmployTypesIntroduction_TabStripMenus':
            document.getElementById('pgvEmployTypesIntroduction_iFrame').src = "EmployTypes.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbMissionLocationsIntroduction_TabStripMenus':
            document.getElementById('pgvMissionLocationsIntroduction_iFrame').src = "MissionLocations.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbWorkGroupsIntroduction_TabStripMenus':
            document.getElementById('pgvWorkGroupsIntroduction_iFrame').src = "WorkGroups.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbExceptionShiftsIntroduction_TabStripMenus':
            document.getElementById('pgvExceptionShiftsIntroduction_iFrame').src = "MasterExceptionShifts.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbPersonnelIntroduction_TabStripMenus':
            document.getElementById('pgvPersonnelIntroduction_iFrame').src = "MasterPersonnel.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbWorkHeatIntroduction_TabStripMenus':
            document.getElementById('pgvWorkHeatIntroduction_iFrame').src = "WorkHeat.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbPreCardIntroduction_TabStripMenus':
            document.getElementById('pgvPreCardIntroduction_iFrame').src = "PreCard.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbControlStationIntroduction_TabStripMenus':
            document.getElementById('pgvControlStationIntroduction_iFrame').src = "ControlStations.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbMachineIntroduction_TabStripMenus':
            document.getElementById('pgvMachineIntroduction_iFrame').src = "Machines.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbMasterLeaveRemains_TabStripMenus':
            document.getElementById('pgvMasterLeaveRemains_iFrame').src = "MasterLeaveRemains.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbPersonnelOrganizationFeaturesChange_TabStripMenus':
            document.getElementById('pgvPersonnelOrganizationFeaturesChange_iFrame').src = "PersonnelOrganizationFeaturesChange.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbUsersIntroduction_TabStripMenus':
            document.getElementById('pgvUsersIntroduction_iFrame').src = "Users.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbRolesIntroduction_TabStripMenus':
            document.getElementById('pgvRolesIntroduction_iFrame').src = "Roles.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbPasswordChange_TabStripMenus':
            document.getElementById('pgvPasswordChange_iFrame').src = "PasswordChange.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbManagerMasterMonthlyOperationReport_TabStripMenus':
            document.getElementById('pgvManagerMasterMonthlyOperationReport_iFrame').src = "ManagerMasterMonthlyOperation.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbTrafficOperationByOperator_TabStripMenus':
            document.getElementById('pgvTrafficOperationByOperator_iFrame').src = "TrafficOperationByOperator.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbMasterManagersIntroduction_TabStripMenus':
            document.getElementById('pgvMasterManagersIntroduction_iFrame').src = "MasterManagers.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbWorkFlowsView_TabStripMenus':
            document.getElementById('pgvWorkFlowsView_iFrame').src = "OrganizationWorkFlow.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbPhysicianIntroduction_TabStripMenus':
            document.getElementById('pgvPhysicianIntroduction_iFrame').src = "Physicians.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbIllnessIntroduction_TabStripMenus':
            document.getElementById('pgvIllnessIntroduction_iFrame').src = "Illnesses.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbSubstituteIntroduction_TabStripMenus':
            document.getElementById('pgvSubstituteIntroduction_iFrame').src = "Substitute.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbCalculationRangeDefinition_TabStripMenus':
            document.getElementById('pgvCalculationRangeDefinition_iFrame').src = "MasterCalculationRange.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbYearlyHolidaysIntroduction_TabStripMenus':
            document.getElementById('pgvYearlyHolidaysIntroduction_iFrame').src = "YearlyHolidays.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbRulesGroupIntroduction_TabStripMenus':
            document.getElementById('pgvRulesGroupIntroduction_iFrame').src = "RulesGroup.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbPersonnelMasterMonthlyOperationReport_TabStripMenus':
            document.getElementById('pgvPersonnelMasterMonthlyOperationReport_iFrame').src = "PersonnelMasterMonthlyOperation.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbReportsIntroduction_TabStripMenus':
            document.getElementById('pgvReportsIntroduction_iFrame').src = "Reports.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbTrafficsControl_TabStripMenus':
            document.getElementById('pgvTrafficsControl_iFrame').src = "MasterTrafficsControl.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbMasterPublicNews_TabStripMenus':
            document.getElementById('pgvMasterPublicNews_iFrame').src = "MasterPublicNews.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbPrivateMessage_TabStripMenus':
            document.getElementById('pgvPrivateMessage_iFrame').src = "PrivateMessage.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbUiValidation_TabStripMenus':
            document.getElementById('pgvUiValidation_iFrame').src = "UiValidation.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbCalculations_TabStripMenus':
            document.getElementById('pgvCalculations_iFrame').src = "Calculations.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbCorporationsIntroduction_TabStripMenus':
            document.getElementById('pgvCorporationsIntroduction_iFrame').src = "Corporations.aspx?reload=" + (new Date()).getTime();
            break;
        case 'tbConceptRuleMasterOperation_TabStripMenus':
            document.getElementById('pgvConceptRuleMasterOperation_iFrame').src = "ConceptRuleMasterOperation.aspx?reload=" + (new Date()).getTime();
            break;
    }
}

function SelectNavBarItem_onTabSelect(TabID) {
    var NvbItemID = "nvbItem" + TabID.substring(2, TabID.length - 14) + "_NavBarMain";
    var NavBarMain_ItemArray = NavBarMain.get_items().get_itemArray();
    for (var i = 0; i < NavBarMain_ItemArray.length; i++) {
        NavBarMain_SubItemArray = NavBarMain_ItemArray[i].get_items().get_itemArray();
        for (var j = 0; j < NavBarMain_SubItemArray.length; j++) {
            if (NavBarMain_SubItemArray[j].get_id() == NvbItemID) {
                NavBarMain.selectItemById(NvbItemID);
                if (!NavBarMain_ItemArray[i].get_expanded()) {
                    NavBarMain_ItemArray[i].set_expanded(true);
                    for (var k = 0; k < NavBarMain_ItemArray.length; k++) {
                        if (i != k && NavBarMain_ItemArray[k].get_expanded()) {
                            NavBarMain_ItemArray[k].set_expanded(false);
                        }
                    }
                }
                return;
            }
        }
    }
}

var BaseContentUrl_DialogKartable_NavBarMain = null;
function CheckException_nvbItem_NavBarMain(NavBarItemID) {
    var ExceptionOccured = false;
    var ObjDialogKartable = new Object();
    switch (NavBarItemID) {
        case 'nvbItemKartable_NavBarMain':
            ExceptionOccured = true;
            ObjDialogKartable.RequestCaller = 'Kartable';
            DialogKartable.set_value(ObjDialogKartable);
            DialogKartable.Show();
            break;
        case 'nvbItemSurveyedRequests_NavBarMain':
            ExceptionOccured = true;
            ObjDialogKartable.RequestCaller = 'Survey';
            DialogKartable.set_value(ObjDialogKartable);
            DialogKartable.Show();
            break;
        case 'nvbItemSentry_NavBarMain':
            ExceptionOccured = true;
            ObjDialogKartable.RequestCaller = 'Sentry';
            DialogKartable.set_value(ObjDialogKartable);
            DialogKartable.Show();
            break;
        case 'nvbItemRegisteredRequests_NavBarMain':
            ExceptionOccured = true;
            var ObjDialogRegisteredRequests = new Object();
            ObjDialogRegisteredRequests.Caller = 'MainPage';
            DialogRegisteredRequests.set_value(ObjDialogRegisteredRequests);
            DialogRegisteredRequests.Show();
            break;
        case 'nvbItemPersonalUserSettings_NavBarMain':
            ExceptionOccured = true;
            var ObjDialogUserSettings = new Object();
            ObjDialogUserSettings.Caller = 'Personal';
            DialogUserSettings.set_value(ObjDialogUserSettings);
            DialogUserSettings.Show();
            break;
        case 'nvbItemManagementUserSettings_NavBarMain':
            ExceptionOccured = true;
            var ObjDialogUserSettings = new Object();
            ObjDialogUserSettings.Caller = 'Management';
            DialogUserSettings.set_value(ObjDialogUserSettings);
            DialogUserSettings.Show();
            break;
        case 'nvbItemSystemReports_NavBarMain':
            ExceptionOccured = true;
            DialogSystemReports.Show();
            break;
        case 'nvbItemExpressionsOperation_NavBarMain':
            ExceptionOccured = true;
            DialogExpressionsManagement.Show();
            break;
    }
    return ExceptionOccured;
}

function NavBarMain_onItemLoad(sender, e) {
    //InitializeQuickLaunch_MainForm();
}
