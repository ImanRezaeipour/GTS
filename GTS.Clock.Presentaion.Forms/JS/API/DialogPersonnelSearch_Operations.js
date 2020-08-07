
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjexpandingOrgPostNode_PersonnelSearch = null;

function ViewCurrentLangCalendars_PersonnelSearch() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpFromDate_DualCalendars_PersonnelSearch").parentNode.removeChild(document.getElementById("pdpFromDate_DualCalendars_PersonnelSearch"));
            document.getElementById("pdpFromDate_DualCalendars_PersonnelSearchimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_DualCalendars_PersonnelSearchimgbt"));
            document.getElementById("pdpToDate_DualCalendars_PersonnelSearch").parentNode.removeChild(document.getElementById("pdpToDate_DualCalendars_PersonnelSearch"));
            document.getElementById("pdpToDate_DualCalendars_PersonnelSearchimgbt").parentNode.removeChild(document.getElementById("pdpToDate_DualCalendars_PersonnelSearchimgbt"));
            document.getElementById("pdpFromDate_SingleCalendar_PersonnelSearch").parentNode.removeChild(document.getElementById("pdpFromDate_SingleCalendar_PersonnelSearch"));
            document.getElementById("pdpFromDate_SingleCalendar_PersonnelSearchimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_SingleCalendar_PersonnelSearchimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_FromDateCalendars_DualCalendars_PersonnelSearch").removeChild(document.getElementById("Container_gCalFromDate_DualCalendars_PersonnelSearch"));
            document.getElementById("Container_ToDateCalendars_DualCalendars_PersonnelSearch").removeChild(document.getElementById("Container_gCalToDate_DualCalendars_PersonnelSearch"));
            document.getElementById("Container_FromDateCalendars_SingleCalendar_PersonnelSearch").removeChild(document.getElementById("Container_gCalFromDate_SingleCalendar_PersonnelSearch"));
            break;
    }
}

function ChangeComboTreeDirection_PersonnelSearch() {
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            document.getElementById('trvDepartment_PersonnelSearch').style.direction = 'ltr';
            document.getElementById('trvOrganizationPost_PersonnelSearch').style.direction = 'ltr';
            break;
        case 'en-US':
            document.getElementById('trvDepartment_PersonnelSearch').style.direction = 'rtl';
            document.getElementById('trvOrganizationPost_PersonnelSearch').style.direction = 'rtl';
            break;
    }
}


function gdpFromDate_DualCalendars_PersonnelSearch_OnDateChange(sender, eventArgs) {
    var FromDate = gdpFromDate_DualCalendars_PersonnelSearch.getSelectedDate();
    gCalFromDate_DualCalendars_PersonnelSearch.setSelectedDate(FromDate);
}
function gCalFromDate_DualCalendars_PersonnelSearch_OnChange(sender, eventArgs) {
    var FromDate = gCalFromDate_DualCalendars_PersonnelSearch.getSelectedDate();
    gdpFromDate_DualCalendars_PersonnelSearch.setSelectedDate(FromDate);
}
function btn_gdpFromDate_DualCalendars_PersonnelSearch_OnClick(event) {
    if (gCalFromDate_DualCalendars_PersonnelSearch.get_popUpShowing()) {
        gCalFromDate_DualCalendars_PersonnelSearch.hide();
    }
    else {
        gCalFromDate_DualCalendars_PersonnelSearch.setSelectedDate(gdpFromDate_DualCalendars_PersonnelSearch.getSelectedDate());
        gCalFromDate_DualCalendars_PersonnelSearch.show();
    }
}
function btn_gdpFromDate_DualCalendars_PersonnelSearch_OnMouseUp(event) {
    if (gCalFromDate_DualCalendars_PersonnelSearch.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalFromDate_DualCalendars_PersonnelSearch_onLoad(sender, e) {
    window.gCalFromDate_DualCalendars_PersonnelSearch.PopUpObject.z = 25000000;
}

function gdpToDate_DualCalendars_PersonnelSearch_OnDateChange(sender, eventArgs) {
    var ToDate = gdpToDate_DualCalendars_PersonnelSearch.getSelectedDate();
    gCalToDate_DualCalendars_PersonnelSearch.setSelectedDate(ToDate);
}
function gCalToDate_DualCalendars_PersonnelSearch_OnChange(sender, eventArgs) {
    var ToDate = gCalToDate_DualCalendars_PersonnelSearch.getSelectedDate();
    gdpToDate_DualCalendars_PersonnelSearch.setSelectedDate(ToDate);
}
function btn_gdpToDate_DualCalendars_PersonnelSearch_OnClick(event) {
    if (gCalToDate_DualCalendars_PersonnelSearch.get_popUpShowing()) {
        gCalToDate_DualCalendars_PersonnelSearch.hide();
    }
    else {
        gCalToDate_DualCalendars_PersonnelSearch.setSelectedDate(gdpToDate_DualCalendars_PersonnelSearch.getSelectedDate());
        gCalToDate_DualCalendars_PersonnelSearch.show();
    }
}
function btn_gdpToDate_DualCalendars_PersonnelSearch_OnMouseUp(event) {
    if (gCalToDate_DualCalendars_PersonnelSearch.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalToDate_DualCalendars_PersonnelSearch_onLoad(sender, e) {
    window.gCalToDate_DualCalendars_PersonnelSearch.PopUpObject.z = 25000000;
}

function gdpFromDate_SingleCalendar_PersonnelSearch_OnDateChange(sender, eventArgs) {
    var FromDate = gdpFromDate_SingleCalendar_PersonnelSearch.getSelectedDate();
    gCalFromDate_SingleCalendar_PersonnelSearch.setSelectedDate(FromDate);
}
function gCalFromDate_SingleCalendar_PersonnelSearch_OnChange(sender, eventArgs) {
    var FromDate = gCalFromDate_SingleCalendar_PersonnelSearch.getSelectedDate();
    gdpFromDate_SingleCalendar_PersonnelSearch.setSelectedDate(FromDate);
}
function btn_gdpFromDate_SingleCalendar_PersonnelSearch_OnClick(event) {
    if (gCalFromDate_SingleCalendar_PersonnelSearch.get_popUpShowing()) {
        gCalFromDate_SingleCalendar_PersonnelSearch.hide();
    }
    else {
        gCalFromDate_SingleCalendar_PersonnelSearch.setSelectedDate(gdpFromDate_SingleCalendar_PersonnelSearch.getSelectedDate());
        gCalFromDate_SingleCalendar_PersonnelSearch.show();
    }
}
function btn_gdpFromDate_SingleCalendar_PersonnelSearch_OnMouseUp(event) {
    if (gCalFromDate_SingleCalendar_PersonnelSearch.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalFromDate_SingleCalendar_PersonnelSearch_onLoad(sender, e) {
    window.gCalFromDate_SingleCalendar_PersonnelSearch.PopUpObject.z = 25000000;
}

function GetBoxesHeaders_Substitute() {
    parent.document.getElementById('Title_DialogPersonnelSearch').innerHTML = document.getElementById('hfTitle_DialogPersonnelSearch').value;
}

function tlbItemSave_TlbPersonnelSearch_onClick() {
    CollapseControls_PersonnelSearch();
    CloseDialogPersonnelSearch();
}

function PersonnelSearch_onSave() {
    var StrObjPersonnelSearch = '';
    var Active = '';
    var Sex = '-1';
    var FatherName = '';
    var MarriageState = '-1';
    var MilitaryState = '-1';
    var Education = '';
    var BirthLocation = '';
    var CardNumber = '0';
    var EmployNumber = '';
    var DepartmentID = '0';
    var IsContainsSubDepartment = false;
    var OrganizationPostID = '0';
    var EmployTypeID = '0';
    var EmployFromDate = '';
    var EmployToDate = '';
    var ControlStationID = '0';
    var FromBirthDate = '';
    var ToBirthDate = '';
    var WorkGroupID = '0';
    var WorkGroupFromDate = '';
    var RuleGroupID = '0';
    var RuleGroupFromDate = '';
    var RuleGroupToDate = '';
    var CalculationRangeID = '0';
    var CalculationRangeFromDate = '';

    if (document.getElementById('rdbAllPersonnel_PersonnelSearch').checked)
        Active = '';
    else
        if (document.getElementById('rdbActive_PersonnelSearch').checked)
            Active = true;
        else
            if (document.getElementById('rdbDeactive_PersonnelSearch').checked)
                Active = false;
    if (cmbSex_PersonnelSearch.getSelectedItem() != undefined)
        Sex = cmbSex_PersonnelSearch.getSelectedItem().get_value();
    FatherName = document.getElementById('txtFatherName_PersonnelSearch').value;
    if (cmbMarriageState_PersonnelSearch.getSelectedItem() != undefined)
        MarriageState = cmbMarriageState_PersonnelSearch.getSelectedItem().get_value();
    if (cmbMilitaryState_PersonnelSearch.getSelectedItem() != undefined)
        MilitaryState = cmbMilitaryState_PersonnelSearch.getSelectedItem().get_value();
    Education = document.getElementById('txtEducation_PersonnelSearch').value;
    BirthLocation = document.getElementById('txtBirthLocation_PersonnelSearch').value;
    CardNumber = document.getElementById('txtCardNumber_PersonnelSearch').value;
    EmployNumber = document.getElementById('txtEmployNumber_PersonnelSearch').value;
    if (trvDepartment_PersonnelSearch.get_selectedNode() != undefined)
        DepartmentID = trvDepartment_PersonnelSearch.get_selectedNode().get_id();
    IsContainsSubDepartment = document.getElementById('chbSubDepartment_PersonnelSearch').checked;
    if (trvOrganizationPost_PersonnelSearch.get_selectedNode() != undefined)
        OrganizationPostID = trvOrganizationPost_PersonnelSearch.get_selectedNode().get_id();
    if (cmbEmployType_PersonnelSearch.getSelectedItem() != undefined)
        EmployTypeID = cmbEmployType_PersonnelSearch.getSelectedItem().get_value();
    EmployFromDate = document.getElementById('txtFromDate_EmployDate_PersonnelSearch').value;
    EmployToDate = document.getElementById('txtToDate_EmployDate_PersonnelSearch').value;
    if (cmbControlStation_PersonnelSearch.getSelectedItem() != undefined)
        ControlStationID = cmbControlStation_PersonnelSearch.getSelectedItem().get_value();
    FromBirthDate = document.getElementById('txtFromDate_BirthDate_PersonnelSearch').value;
    ToBirthDate = document.getElementById('txtToDate_BirthDate_PersonnelSearch').value;
    if (cmbWorkGroups_PersonnelSearch.getSelectedItem() != undefined)
        WorkGroupID = cmbWorkGroups_PersonnelSearch.getSelectedItem().get_value();
    WorkGroupFromDate = document.getElementById('txtFromDate_WorkGroups_PersonnelSearch').value;
    if (cmbRuleGroups_PersonnelSearch.getSelectedItem() != undefined)
        RuleGroupID = cmbRuleGroups_PersonnelSearch.getSelectedItem().get_value();
    RuleGroupFromDate = document.getElementById('txtFromDate_RuleGroups_PersonnelSearch').value;
    RuleGroupToDate = document.getElementById('txtToDate_RuleGroups_PersonnelSearch').value;
    if (cmbCalculationRange_PersonnelSearch.getSelectedItem() != undefined)
        CalculationRangeID = cmbCalculationRange_PersonnelSearch.getSelectedItem().get_value();
    CalculationRangeFromDate = document.getElementById('txtFromDate_CalculationDateRange_PersonnelSearch').value;

    StrObjPersonnelSearch = '{"Active":"' + Active + '","Sex":"' + Sex + '","FatherName":"' + FatherName + '","MarriageState":"' + MarriageState + '","MilitaryState":"' + MilitaryState + '","Education":"' + Education + '","BirthLocation":"' + BirthLocation + '","CardNumber":"' + CardNumber + '","EmployNumber":"' + EmployNumber + '","DepartmentID":"' + DepartmentID + '","IsContainsSubDepartment":"' + IsContainsSubDepartment.toString() + '","OrganizationPostID":"' + OrganizationPostID + '","EmployTypeID":"' + EmployTypeID + '","EmployFromDate":"' + EmployFromDate + '","EmployToDate":"' + EmployToDate + '","ControlStationID":"' + ControlStationID + '","FromBirthDate":"' + FromBirthDate + '","ToBirthDate":"' + ToBirthDate + '","WorkGroupID":"' + WorkGroupID + '","WorkGroupFromDate":"' + WorkGroupFromDate + '","RuleGroupID":"' + RuleGroupID + '","RuleGroupFromDate":"' + RuleGroupFromDate + '","RuleGroupToDate":"' + RuleGroupToDate + '","CalculationRangeID":"' + CalculationRangeID + '","CalculationRangeFromDate":"' + CalculationRangeFromDate + '"}';

    FetchRelativeOperation_PersonnelSearch(StrObjPersonnelSearch);
}

function FetchRelativeOperation_PersonnelSearch(StrObjPersonnelSearch) {
    var caller = parent.DialogPersonnelSearch.get_value().Caller;
    switch (caller) {
        case 'Manager_Substitute':
            parent.document.getElementById('pgvSubstituteIntroduction_iFrame').contentWindow.Substitute_onAfterPersonnelAdvancedSearch('Manager', StrObjPersonnelSearch);
            break;
        case 'Personnel_Substitute':
            parent.document.getElementById('pgvSubstituteIntroduction_iFrame').contentWindow.Substitute_onAfterPersonnelAdvancedSearch('Substitute', StrObjPersonnelSearch);
            break;
        case 'Operators':
            parent.document.getElementById('DialogOperators_IFrame').contentWindow.Operators_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'RegisteredRequests':
            parent.document.getElementById('DialogRegisteredRequests_IFrame').contentWindow.RegisteredRequests_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'RequestRegister':
            parent.document.getElementById('DialogRegisteredRequests_IFrame').contentWindow.document.getElementById('DialogRequestRegister_IFrame').contentWindow.RequestRegister_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'LeaveRemains':
            parent.document.getElementById('pgvMasterLeaveRemains_iFrame').contentWindow.LeaveRemains_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'MasterPersonnel':
            parent.document.getElementById('pgvPersonnelIntroduction_iFrame').contentWindow.MasterPersonnelMainInformation_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'Users':
            parent.document.getElementById('pgvUsersIntroduction_iFrame').contentWindow.Users_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'UnderManagementPersonnel':
            parent.document.getElementById('DialogUnderManagementPersonnel_IFrame').contentWindow.UnderManagementPersonnel_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'PersonnelOrganizationFeaturesChange':
            parent.document.getElementById('pgvPersonnelOrganizationFeaturesChange_iFrame').contentWindow.PersonnelOrganizationFeaturesChange_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'MasterTrafficsControl':
            parent.document.getElementById('pgvTrafficsControl_iFrame').contentWindow.MasterTrafficsControl_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'MasterExceptionShifts':
            parent.document.getElementById('pgvExceptionShiftsIntroduction_iFrame').contentWindow.MasterExceptionShifts_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'ExceptionShifts':
            parent.document.getElementById('DialogExceptionShifts_IFrame').contentWindow.ExceptionShifts_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'Calculations':
            parent.document.getElementById('pgvCalculations_iFrame').contentWindow.Calculations_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'UserSettings':
            parent.document.getElementById('DialogUserSettings_IFrame').contentWindow.UserSettings_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'ManagersWorkFlow':
            parent.document.getElementById('DialogUnderManagementPersonnel_IFrame').contentWindow.document.getElementById('DialogManagersWorkFlow_IFrame').contentWindow.ManagersWorkFlow_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
    }
}

function tlbItemFormReconstruction_TlbPersonnelSearch_onClick() {
    CloseDialogPersonnelSearch();
    var caller = parent.DialogPersonnelSearch.get_value().Caller;
    switch (caller) {
        case 'Manager_Substitute':
            parent.document.getElementById('pgvSubstituteIntroduction_iFrame').contentWindow.ShowDialogPersonnelSearch('Manager_Substitute');
            break;
        case 'Personnel_Substitute':
            parent.document.getElementById('pgvSubstituteIntroduction_iFrame').contentWindow.ShowDialogPersonnelSearch('Personnel_Substitute');
            break;
        case 'Operators':
            parent.document.getElementById('DialogOperators_IFrame').contentWindow.ShowDialogPersonnelSearch('Operators');
            break;
        case 'RegisteredRequests':
            parent.document.getElementById('DialogRegisteredRequests_IFrame').contentWindow.ShowDialogPersonnelSearch('RegisteredRequests');
            break;
        case 'RequestRegister':
            parent.document.getElementById('DialogRegisteredRequests_IFrame').contentWindow.document.getElementById('DialogRequestRegister_IFrame').contentWindow.ShowDialogPersonnelSearch('RequestRegister');
            break;
        case 'LeaveRemains':
            parent.document.getElementById('pgvMasterLeaveRemains_iFrame').contentWindow.ShowDialogPersonnelSearch('LeaveRemains');
            break;
        case 'MasterPersonnel':
            parent.document.getElementById('pgvPersonnelIntroduction_iFrame').contentWindow.ShowDialogPersonnelSearch('MasterPersonnel');
            break;
        case 'Users':
            parent.document.getElementById('pgvUsersIntroduction_iFrame').contentWindow.ShowDialogPersonnelSearch('Users');
            break;
        case 'UnderManagementPersonnel':
            parent.document.getElementById('DialogUnderManagementPersonnel_IFrame').contentWindow.ShowDialogPersonnelSearch('UnderManagementPersonnel');
            break;
        case 'PersonnelOrganizationFeaturesChange':
            parent.document.getElementById('pgvPersonnelOrganizationFeaturesChange_iFrame').contentWindow.ShowDialogPersonnelSearch('PersonnelOrganizationFeaturesChange');
            break;
        case 'MasterTrafficsControl':
            parent.document.getElementById('pgvTrafficsControl_iFrame').contentWindow.ShowDialogPersonnelSearch('MasterTrafficsControl');
            break;
        case 'MasterExceptionShifts':
            parent.document.getElementById('pgvExceptionShiftsIntroduction_iFrame').contentWindow.ShowDialogPersonnelSearch('MasterExceptionShifts');
            break;
        case 'ExceptionShifts':
            parent.document.getElementById('DialogExceptionShifts_IFrame').contentWindow.ShowDialogPersonnelSearch('ExceptionShifts');
            break;
        case 'Calculations':
            parent.document.getElementById('pgvCalculations_iFrame').contentWindow.ShowDialogPersonnelSearch('Calculations');
            break;
        case 'UserSettings':
            parent.document.getElementById('DialogUserSetings_IFrame').contentWindow.ShowDialogPersonnelSearch('UserSettings');
            break;
        case 'ManagersWorkFlow':
            parent.document.getElementById('DialogManagersWorkFlow_IFrame').contentWindow.ShowDialogPersonnelSearch('ManagersWorkFlow');
            break;
    }
}


function tlbItemExit_TlbPersonnelSearch_onClick() {
    ShowDialogConfirm();
}

function cmbSex_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbSex_PersonnelSearch);
    if (cmbSex_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSex_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSex_PersonnelSearch = true;
        Fill_cmbSex_PersonnelSearch();
    }
}

function Fill_cmbSex_PersonnelSearch() {
    CallBack_cmbSex_PersonnelSearch.callback();
}

function CallBack_cmbSex_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbSex_PersonnelSearch.dispose();
}

function CallBack_cmbSex_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Sex_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbSex_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbSex_PersonnelSearch_DropImage').mousedown();
        cmbSex_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbSex_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbSex_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_Sex_PersonnelSearch_onClick() {
    document.getElementById('cmbSex_PersonnelSearch_Input').value = '';
    cmbSex_PersonnelSearch.unSelect();
}

function cmbMarriageState_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbMarriageState_PersonnelSearch);
    if (cmbMarriageState_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMarriageState_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMarriageState_PersonnelSearch = true;
        Fill_cmbMarriageState_PersonnelSearch();
    }
}

function Fill_cmbMarriageState_PersonnelSearch() {
    CallBack_cmbMarriageState_PersonnelSearch.callback();
}

function CallBack_cmbMarriageState_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbMarriageState_PersonnelSearch.dispose();
}

function CallBack_cmbMarriageState_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MarriageState_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbMarriageState_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMarriageState_PersonnelSearch_DropImage').mousedown();
        cmbMarriageState_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbMarriageState_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbMarriageState_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_MarriageState_PersonnelSearch_onClick() {
    document.getElementById('cmbMarriageState_PersonnelSearch_Input').value = '';
    cmbMarriageState_PersonnelSearch.unSelect();
}

function cmbMilitaryState_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbMilitaryState_PersonnelSearch);
    if (cmbMilitaryState_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMilitaryState_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMilitaryState_PersonnelSearch = true;
        Fill_cmbMilitaryState_PersonnelSearch();
    }
}

function Fill_cmbMilitaryState_PersonnelSearch() {
    CallBack_cmbMilitaryState_PersonnelSearch.callback();
}

function CallBack_cmbMilitaryState_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbMilitaryState_PersonnelSearch.dispose();
}

function CallBack_cmbMilitaryState_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MilitaryState_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbMilitaryState_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMilitaryState_PersonnelSearch_DropImage').mousedown();
        cmbMilitaryState_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbMilitaryState_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbMilitaryState_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_MilitaryState_PersonnelSearch_onClick() {
    document.getElementById('cmbMilitaryState_PersonnelSearch_Input').value = '';
    cmbMilitaryState_PersonnelSearch.unSelect();
}

function trvDepartment_PersonnelSearch_onNodeSelect(sender, e) {
    document.getElementById('cmbDepartment_PersonnelSearch_TextBox').innerHTML = e.get_node().get_text();
    cmbDepartment_PersonnelSearch.collapse();
}

function cmbDepartment_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbDepartment_PersonnelSearch);
    if (trvDepartment_PersonnelSearch.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDepartment_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDepartment_PersonnelSearch = true;
        Fill_cmbDepartment_PersonnelSearch();
    }
}

function Fill_cmbDepartment_PersonnelSearch() {
    CallBack_cmbDepartment_PersonnelSearch.callback();
}

function CallBack_cmbDepartment_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbDepartment_PersonnelSearch.dispose();
}

function CallBack_cmbDepartment_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Department_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbDepartment_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDepartment_PersonnelSearch_DropImage').mousedown();
        cmbDepartment_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbDepartment_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbDepartment_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_Department_PersonnelSearch_onClick() {
    document.getElementById('cmbDepartment_PersonnelSearch_Input').value = '';
    cmbDepartment_PersonnelSearch.unSelect();
    trvDepartment_PersonnelSearch.SelectedNode = undefined;
}

function Refresh_cmbDepartment_PersonnelSearch() {
    Fill_cmbDepartment_PersonnelSearch();
}

function trvOrganizationPost_PersonnelSearch_onNodeSelect(sender, e) {
    document.getElementById('cmbOrganizationPost_PersonnelSearch_TextBox').innerHTML = e.get_node().get_text();
    cmbOrganizationPost_PersonnelSearch.collapse();
}

function trvOrganizationPost_PersonnelSearch_onCallbackComplete(sender, e) {
    if (ObjexpandingOrgPostNode_PersonnelSearch != null) {
        if (ObjexpandingOrgPostNode_PersonnelSearch.Node.get_nodes().get_length() == 0 && ObjexpandingOrgPostNode_PersonnelSearch.HasChild) {
            ObjexpandingOrgPostNode_PersonnelSearch = null;
            GetLoadonDemandError_PersonnelSearchPage();
        }
        else
            ObjexpandingOrgPostNode_PersonnelSearch = null;
    }
}

function GetLoadonDemandError_PersonnelSearchPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function trvOrganizationPost_PersonnelSearch_onNodeBeforeExpand(sender, e) {
    if (ObjexpandingOrgPostNode_PersonnelSearch != null)
        ObjexpandingOrgPostNode_PersonnelSearch = null;
    ObjexpandingOrgPostNode_PersonnelSearch = new Object();
    ObjexpandingOrgPostNode_PersonnelSearch.Node = e.get_node();
    if (e.get_node().get_nodes().get_length() == 1 && (e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined || e.get_node().get_nodes().get_nodeArray()[0].get_id() == '')) {
        ObjexpandingOrgPostNode_PersonnelSearch.HasChild = true;
        trvOrganizationPost_PersonnelSearch.beginUpdate();
        ObjexpandingOrgPostNode_PersonnelSearch.Node.get_nodes().remove(0);
        trvOrganizationPost_PersonnelSearch.endUpdate();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            ObjexpandingOrgPostNode_PersonnelSearch.HasChild = false;
        else
            ObjexpandingOrgPostNode_PersonnelSearch.HasChild = true;
    }
}

function cmbOrganizationPost_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbOrganizationPost_PersonnelSearch);
    if (trvOrganizationPost_PersonnelSearch.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbOrganizationPost_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbOrganizationPost_PersonnelSearch = true;
        ObjexpandingOrgPostNode_PersonnelSearch = null;
        Fill_cmbOrganizationPost_PersonnelSearch();
    }
}

function Fill_cmbOrganizationPost_PersonnelSearch() {
    CallBack_cmbOrganizationPost_PersonnelSearch.callback();
}

function CallBack_cmbOrganizationPost_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_OrganizationPost_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbOrganizationPost_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbOrganizationPost_PersonnelSearch_DropImage').mousedown();
        cmbOrganizationPost_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbOrganizationPost_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbOrganizationPost_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_OrganizationPost_PersonnelSearch_onClick() {
    document.getElementById('cmbOrganizationPost_PersonnelSearch_Input').value = '';
    cmbOrganizationPost_PersonnelSearch.unSelect();
    trvOrganizationPost_PersonnelSearch.SelectedNode = undefined;
}

function Refresh_cmbOrganizationPost_PersonnelSearch() {
    Fill_cmbOrganizationPost_PersonnelSearch();
}

function cmbControlStation_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbControlStation_PersonnelSearch);
    if (cmbControlStation_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbControlStation_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbControlStation_PersonnelSearch = true;
        Fill_cmbControlStation_PersonnelSearch();
    }
}

function Fill_cmbControlStation_PersonnelSearch() {
    CallBack_cmbControlStation_PersonnelSearch.callback();
}

function CallBack_cmbControlStation_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbControlStation_PersonnelSearch.dispose();
}

function CallBack_cmbControlStation_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ControlStation_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbControlStation_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbControlStation_PersonnelSearch_DropImage').mousedown();
        cmbControlStation_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbControlStation_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbControlStation_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_ControlStation_PersonnelSearch_onClick() {
    document.getElementById('cmbControlStation_PersonnelSearch_Input').value = '';
    cmbControlStation_PersonnelSearch.unSelect();
}

function Refresh_cmbControlStation_PersonnelSearch() {
    Fill_cmbControlStation_PersonnelSearch();
}

function tlbItemSetDate_TlbSetDate_BirthDate_PersonnelSearch() {
    ShowDialogDualCalendars('BirthDate');
}

function cmbWorkGroups_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbWorkGroups_PersonnelSearch);
    if (cmbWorkGroups_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbWorkGroups_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbWorkGroups_PersonnelSearch = true;
        Fill_cmbWorkGroups_PersonnelSearch();
    }
}

function Fill_cmbWorkGroups_PersonnelSearch() {
    CallBack_cmbWorkGroups_PersonnelSearch.callback();
}

function CallBack_cmbWorkGroups_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbWorkGroups_PersonnelSearch.dispose();
}

function CallBack_cmbWorkGroups_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkGroups_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbWorkGroups_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbWorkGroups_PersonnelSearch_DropImage').mousedown();
        cmbWorkGroups_PersonnelSearch.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbWorkGroups_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbWorkGroups_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function Refresh_cmbWorkGroups_PersonnelSearch() {
    Fill_cmbWorkGroups_PersonnelSearch();
}

function tlbItemClear_TlbClear_WorkGroups_PersonnelSearch_onClick() {
    document.getElementById('cmbWorkGroups_PersonnelSearch_Input').value = '';
    cmbWorkGroups_PersonnelSearch.unSelect();
}

function tlbItemSetDate_TlbSetDate_WorkGroups_PersonnelSearch() {
    ShowDialogSingleCalendar('WorkGroup');
}

function cmbRuleGroups_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbRuleGroups_PersonnelSearch);
    if (cmbRuleGroups_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRuleGroups_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRuleGroups_PersonnelSearch = true;
        Fill_cmbRuleGroups_PersonnelSearch();
    }
}

function Fill_cmbRuleGroups_PersonnelSearch() {
    CallBack_cmbRuleGroups_PersonnelSearch.callback();
}

function CallBack_cmbRuleGroups_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbRuleGroups_PersonnelSearch.dispose();
}

function CallBack_cmbRuleGroups_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RuleGroups_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbRuleGroups_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbRuleGroups_PersonnelSearch_DropImage').mousedown();
        cmbRuleGroups_PersonnelSearch.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbRuleGroups_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbRuleGroups_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function Refresh_cmbRuleGroups_PersonnelSearch() {
    Fill_cmbRuleGroups_PersonnelSearch();
}

function tlbItemClear_TlbClear_RuleGroups_PersonnelSearch_onClick() {
    document.getElementById('cmbRuleGroups_PersonnelSearch_Input').value = '';
    cmbRuleGroups_PersonnelSearch.unSelect();
}

function tlbItemSetDate_TlbSetDate_RuleGroups_PersonnelSearch() {
    ShowDialogDualCalendars('RuleGroup');
}

function cmbCalculationRange_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbCalculationRange_PersonnelSearch);
    if (cmbCalculationRange_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbCalculationRange_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbCalculationRange_PersonnelSearch = true;
        Fill_cmbCalculationRange_PersonnelSearch();
    }
}

function Fill_cmbCalculationRange_PersonnelSearch() {
    CallBack_cmbCalculationRange_PersonnelSearch.callback();
}

function CallBack_cmbCalculationRange_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbCalculationRange_PersonnelSearch.dispose();
}

function CallBack_cmbCalculationRange_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_CalculationRange_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbCalculationRange_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbCalculationRange_PersonnelSearch_DropImage').mousedown();
        cmbCalculationRange_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbCalculationRange_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbCalculationRange_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_CalculationRange_PersonnelSearch_onClick() {
    document.getElementById('cmbCalculationRange_PersonnelSearch_Input').value = '';
    cmbCalculationRange_PersonnelSearch.unSelect();
}

function Refresh_cmbCalculationRange_PersonnelSearch() {
    Fill_cmbCalculationRange_PersonnelSearch();
}

function tlbItemSetDate_TlbSetDate_CalculationRange_PersonnelSearch() {
    ShowDialogSingleCalendar('CalculationRange');
}

function cmbEmployType_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbEmployType_PersonnelSearch);
    if (cmbEmployType_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbEmployType_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbEmployType_PersonnelSearch = true;
        Fill_cmbEmployType_PersonnelSearch();
    }
}

function Fill_cmbEmployType_PersonnelSearch() {
    CallBack_cmbEmployType_PersonnelSearch.callback();
}

function CallBack_cmbEmployType_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbEmployType_PersonnelSearch.dispose();
}

function CallBack_cmbEmployType_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_EmployType_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbEmployType_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbEmployType_PersonnelSearch_DropImage').mousedown();
        cmbEmployType_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbEmployType_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbEmployType_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_EmployDate_PersonnelSearch_onClick() {
    document.getElementById('cmbEmployType_PersonnelSearch_Input').value = '';
    cmbEmployType_PersonnelSearch.unSelect();
}

function Refresh_cmbEmployType_PersonnelSearch() {
    Fill_cmbEmployType_PersonnelSearch();
}

function tlbItemSetDate_TlbSetDate_EmployDate_PersonnelSearch() {
    ShowDialogDualCalendars('EmployType');
}

function tlbItemSave_TlbDualCalendars_PersonnelSearch_onClick() {
    FetchRelativeDates_DualCalendars_PersonnelSearch();
    DialogDualCalendars.Close();
}

function FetchRelativeDates_DualCalendars_PersonnelSearch() {
    var caller = DialogDualCalendars.get_value();
    var txtFromDateID = null;
    var txtToDateID = null;
    var targetFromCalID = null;
    var targetToCalID = null;
    switch (caller) {
        case 'BirthDate':
            txtFromDateID = 'txtFromDate_BirthDate_PersonnelSearch';
            txtToDateID = 'txtToDate_BirthDate_PersonnelSearch';
            break;
        case 'RuleGroup':
            txtFromDateID = 'txtFromDate_RuleGroups_PersonnelSearch';
            txtToDateID = 'txtToDate_RuleGroups_PersonnelSearch';
            break;
        case 'EmployType':
            txtFromDateID = 'txtFromDate_EmployDate_PersonnelSearch';
            txtToDateID = 'txtToDate_EmployDate_PersonnelSearch';
            break;
    }
    switch (parent.SysLangID) {
        case 'fa-IR':
            targetFromCalID = 'pdpFromDate_DualCalendars_PersonnelSearch';
            targetToCalID = 'pdpToDate_DualCalendars_PersonnelSearch';
            break;
        case 'en-US':
            targetFromCalID = 'gdpFromDate_DualCalendars_PersonnelSearch_picker';
            targetToCalID = 'gdpToDate_DualCalendars_PersonnelSearch_picker';
            break;
    }
    document.getElementById(txtFromDateID).value = document.getElementById(targetFromCalID).value;
    document.getElementById(txtToDateID).value = document.getElementById(targetToCalID).value;
}

function tlbItemExit_TlbDualCalendars_PersonnelSearch_onClick() {
    DialogDualCalendars.Close();
}

function tlbItemSave_TlbSingleCalendar_PersonnelSearch_onClick() {
    FetchRelativeDate_SingleCalendars_PersonnelSearch();
    DialogSingleCalendar.Close();
}

function FetchRelativeDate_SingleCalendars_PersonnelSearch() {
    var caller = DialogSingleCalendar.get_value();
    var txtDateID = null;
    var targetCalID = null;
    switch (caller) {
        case 'WorkGroup':
            txtDateID = 'txtFromDate_WorkGroups_PersonnelSearch';
            break;
        case 'CalculationRange':
            txtDateID = 'txtFromDate_CalculationDateRange_PersonnelSearch';
            break;
    }
    switch (parent.SysLangID) {
        case 'fa-IR':
            targetCalID = 'pdpFromDate_SingleCalendar_PersonnelSearch';
            break;
        case 'en-US':
            targetCalID = 'gdpFromDate_SingleCalendar_PersonnelSearch_picker';
            break;
    }
    document.getElementById(txtDateID).value = document.getElementById(targetCalID).value;
}

function tlbItemExit_TlbSingleCalendar_PersonnelSearch_onClick() {
    DialogSingleCalendar.Close();
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function ShowConnectionError_PersonnelSearch() {
    var error = document.getElementById('hfErrorType_PersonnelSearch').value;
    var errorBody = document.getElementById('hfConnectionError_PersonnelSearch').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDialogPersonnelSearch();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PersonnelSearch').value;
    DialogConfirm.Show();
}

function CloseDialogPersonnelSearch() {
    PersonnelSearch_onSave();
    parent.document.getElementById('DialogPersonnelSearch_IFrame').src = 'WhitePage.aspx';
    parent.DialogPersonnelSearch.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function ShowDialogDualCalendars(caller) {
    var ObjDatesOfCaller = GetDatesOfCaller_PersonnelSearch(caller);
    SetDate_FromDateCalendars_DualCalendars_PersonnelSearch(ObjDatesOfCaller.FromDate);
    SetDate_ToDateCalendars_DualCalendars_PersonnelSearch(ObjDatesOfCaller.ToDate);
    DialogDualCalendars.set_value(caller);
    DialogDualCalendars.Show();
}

function GetDateOfCaller_PersonnelSearch(caller) {
    var date = '';
    switch (caller) {
        case 'WorkGroup':
            date = document.getElementById('txtFromDate_WorkGroups_PersonnelSearch').value;
            break;
        case 'CalculationRange':
            date = document.getElementById('txtFromDate_CalculationDateRange_PersonnelSearch').value;
            break;
    }
    return date;
}

function GetDatesOfCaller_PersonnelSearch(caller) {
    var ObjDateOfCaller = new Object();
    ObjDateOfCaller.FromDate = '';
    ObjDateOfCaller.ToDate = '';
    switch (caller) {
        case 'BirthDate':
            ObjDateOfCaller.FromDate = document.getElementById('txtFromDate_BirthDate_PersonnelSearch').value;
            ObjDateOfCaller.ToDate = document.getElementById('txtToDate_BirthDate_PersonnelSearch').value;
            break;
        case 'RuleGroup':
            ObjDateOfCaller.FromDate = document.getElementById('txtFromDate_RuleGroups_PersonnelSearch').value;
            ObjDateOfCaller.ToDate = document.getElementById('txtToDate_RuleGroups_PersonnelSearch').value;
            break;
        case 'EmployType':
            ObjDateOfCaller.FromDate = document.getElementById('txtFromDate_EmployDate_PersonnelSearch').value;
            ObjDateOfCaller.ToDate = document.getElementById('txtToDate_EmployDate_PersonnelSearch').value;
            break;
    }
    return ObjDateOfCaller;
}

function ShowDialogSingleCalendar(caller) {
    SetDate_FromDateCalendars_SingleCalendar_PersonnelSearch(GetDateOfCaller_PersonnelSearch(caller));
    DialogSingleCalendar.set_value(caller);
    DialogSingleCalendar.Show();
}

function tlbItemClear_TlbClear_FromDateCalendars_SingleCalendar_PersonnelSearch_onClick() {
    SetDate_FromDateCalendars_SingleCalendar_PersonnelSearch('');
}

function SetDate_FromDateCalendars_SingleCalendar_PersonnelSearch(date) {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_SingleCalendar_PersonnelSearch').value = date;
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_SingleCalendar_PersonnelSearch_picker').value = date;
            break;
    }
}

function tlbItemClear_TlbClear_FromDateCalendars_DualCalendars_PersonnelSearch_onClick() {
    SetDate_FromDateCalendars_DualCalendars_PersonnelSearch('');
}

function SetDate_FromDateCalendars_DualCalendars_PersonnelSearch(date) {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_DualCalendars_PersonnelSearch').value = date;
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_DualCalendars_PersonnelSearch_picker').value = date;
            break;
    }
}

function tlbItemClear_TlbClear_ToDateCalendars_DualCalendars_PersonnelSearch_onClick() {
    SetDate_ToDateCalendars_DualCalendars_PersonnelSearch('');
}

function SetDate_ToDateCalendars_DualCalendars_PersonnelSearch(date) {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpToDate_DualCalendars_PersonnelSearch').value = date;
            break;
        case 'en-US':
            document.getElementById('gdpToDate_DualCalendars_PersonnelSearch_picker').value = date;
            break;
    }
}

function ResetCalendars_PersonnelSearch() {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_SingleCalendar_PersonnelSearch').value = '';
            document.getElementById('pdpFromDate_DualCalendars_PersonnelSearch').value = '';
            document.getElementById('pdpToDate_DualCalendars_PersonnelSearch').value = '';
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_SingleCalendar_PersonnelSearch_picker').value = '';
            document.getElementById('gdpFromDate_DualCalendars_PersonnelSearch_picker').value = '';
            document.getElementById('gdpToDate_DualCalendars_PersonnelSearch_picker').value = '';
            break;
    }
}

function CollapseControls_PersonnelSearch(exception) {
    if (exception == null || exception != cmbSex_PersonnelSearch)
        cmbSex_PersonnelSearch.collapse();
    if (exception == null || exception != cmbMarriageState_PersonnelSearch)
        cmbMarriageState_PersonnelSearch.collapse();
    if (exception == null || exception != cmbMilitaryState_PersonnelSearch)
        cmbMilitaryState_PersonnelSearch.collapse();
    if (exception == null || exception != cmbDepartment_PersonnelSearch)
        cmbDepartment_PersonnelSearch.collapse();
    if (exception == null || exception != cmbOrganizationPost_PersonnelSearch)
        cmbOrganizationPost_PersonnelSearch.collapse();
    if (exception == null || exception != cmbControlStation_PersonnelSearch)
        cmbControlStation_PersonnelSearch.collapse();
    if (exception == null || exception != cmbWorkGroups_PersonnelSearch)
        cmbWorkGroups_PersonnelSearch.collapse();
    if (exception == null || exception != cmbRuleGroups_PersonnelSearch)
        cmbRuleGroups_PersonnelSearch.collapse();
    if (exception == null || exception != cmbCalculationRange_PersonnelSearch)
        cmbCalculationRange_PersonnelSearch.collapse();
    if (exception == null || exception != cmbEmployType_PersonnelSearch)
        cmbEmployType_PersonnelSearch.collapse();
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpFromDate_DualCalendars_PersonnelSearch');
}















