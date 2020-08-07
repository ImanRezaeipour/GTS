
var ConfirmState_PersonnelOrganizationFeaturesChange = null;
var box_SearchByPersonnel_PersonnelOrganizationFeaturesChange_IsShown = false;
var LoadState_cmbPersonnel_PersonnelOrganizationFeaturesChange = 'Normal';
var CurrentPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange = 0;
var SearchTerm_cmbPersonnel_PersonnelOrganizationFeaturesChange = '';
var CurrentPageCombosCallBcakStateObj = new Object();

function gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange_OnDateChange(sender, eventArgs) {
    var fromDate = gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange.getSelectedDate();
    gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange.setSelectedDate(fromDate);
}

function gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange_OnChange(sender, eventArgs) {
    var fromDate = gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange.getSelectedDate();
    gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange.setSelectedDate(fromDate);
}

function btn_gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange_OnClick(event) {
    if (gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange.get_popUpShowing()) {
        gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange.hide();
    }
    else {
        gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange.setSelectedDate(gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange.getSelectedDate());
        gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange.show();
    }
}

function btn_gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange_OnMouseUp(event) {
    if (gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange_onLoad(sender, e) {
    window.gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange.PopUpObject.z = 25000000;
}

function gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange_OnDateChange(sender, eventArgs) {
    var FromDate = gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange.getSelectedDate();
    gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange.setSelectedDate(FromDate);
}
function gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange_OnChange(sender, eventArgs) {
    var FromDate = gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange.getSelectedDate();
    gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange.setSelectedDate(FromDate);
}
function btn_gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange_OnClick(event) {
    if (gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange.get_popUpShowing()) {
        gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange.hide();
    }
    else {
        gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange.setSelectedDate(gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange.getSelectedDate());
        gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange.show();
    }
}
function btn_gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange_OnMouseUp(event) {
    if (gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange_onLoad(sender, e) {
    window.gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange.PopUpObject.z = 25000000;
}

function gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnDateChange(sender, eventArgs) {
    var FromDate = gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange.getSelectedDate();
    gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange.setSelectedDate(FromDate);
}
function gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnChange(sender, eventArgs) {
    var FromDate = gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange.getSelectedDate();
    gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange.setSelectedDate(FromDate);
}
function btn_gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnClick(event) {
    if (gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange.get_popUpShowing()) {
        gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange.hide();
    }
    else {
        gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange.setSelectedDate(gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange.getSelectedDate());
        gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange.show();
    }
}
function btn_gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnMouseUp(event) {
    if (gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange_onLoad(sender, e) {
    window.gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange.PopUpObject.z = 25000000;
}

function gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnDateChange(sender, eventArgs) {
    var toDate = gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange.getSelectedDate();
    gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange.setSelectedDate(toDate);
}
function gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnChange(sender, eventArgs) {
    var toDate = gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange.getSelectedDate();
    gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange.setSelectedDate(toDate);
}
function btn_gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnClick(event) {
    if (gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange.get_popUpShowing()) {
        gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange.hide();
    }
    else {
        gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange.setSelectedDate(gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange.getSelectedDate());
        gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange.show();
    }
}
function btn_gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnMouseUp(event) {
    if (gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange_onLoad(sender, e) {
    window.gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange.PopUpObject.z = 25000000;
}

function GetBoxesHeaders_PersonnelOrganizationFeatures() {
    document.getElementById('header_SearchByPersonnelBox_PersonnelOrganizationFeaturesChange').innerHTML = document.getElementById('hfheader_SearchByPersonnelBox_PersonnelOrganizationFeaturesChange').value;
    document.getElementById('header_PersonnelProblems_PersonnelOrganizationFeaturesChange').innerHTML = document.getElementById('hfheader_PersonnelProblems_PersonnelOrganizationFeaturesChange').value;
    document.getElementById('clmnName_cmbPersonnel_PersonnelOrganizationFeaturesChange').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_PersonnelOrganizationFeaturesChange').value;
    document.getElementById('clmnBarCode_cmbPersonnel_PersonnelOrganizationFeaturesChange').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_PersonnelOrganizationFeaturesChange').value;
    document.getElementById('clmnCardNum_cmbPersonnel_PersonnelOrganizationFeaturesChange').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_PersonnelOrganizationFeaturesChange').value;
}

function ViewCurrentLangCalendars_PersonnelOrganizationFeaturesChange() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange").parentNode.removeChild(document.getElementById("pdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange"));
            document.getElementById("pdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChangeimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChangeimgbt"));
            document.getElementById("pdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange").parentNode.removeChild(document.getElementById("pdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange"));
            document.getElementById("pdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChangeimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChangeimgbt"));
            document.getElementById("pdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange").parentNode.removeChild(document.getElementById("pdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange"));
            document.getElementById("pdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChangeimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChangeimgbt"));
            document.getElementById("pdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange").parentNode.removeChild(document.getElementById("pdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange"));
            document.getElementById("pdpToDate_RulesGroup_PersonnelOrganizationFeaturesChangeimgbt").parentNode.removeChild(document.getElementById("pdpToDate_RulesGroup_PersonnelOrganizationFeaturesChangeimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_WorkGroupCalendars_PersonnelOrganizationFeaturesChange").removeChild(document.getElementById("Container_gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange"));
            document.getElementById("Container_CalculationRangeCalendars_PersonnelOrganizationFeaturesChange").removeChild(document.getElementById("Container_gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange"));
            document.getElementById("Container_RulesGroupCalendars_FromDate_PersonnelOrganizationFeaturesChange").removeChild(document.getElementById("Container_gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange"));
            document.getElementById("Container_RulesGroupCalendars_ToDate_PersonnelOrganizationFeaturesChange").removeChild(document.getElementById("Container_gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange"));
            break;
    }
}

function SetPosition_PersonnelOrganizationFeatures() {
    var Lag = 50;
    switch (parent.CurrentLangID) {
        case 'en-US':
            document.getElementById('box_SearchByPersonnel_PersonnelOrganizationFeaturesChange').style.left = Lag;
            break;
        case 'fa-IR':
            document.getElementById('box_SearchByPersonnel_PersonnelOrganizationFeaturesChange').style.right = Lag;
            break;
    }
}

function SetPosition_cmbPersonnel_PersonnelOrganizationFeaturesChange() {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('cmbPersonnel_PersonnelOrganizationFeaturesChange_DropDown').style.left = document.getElementById('Mastertbl_PersonnelOrganizationFeaturesChange').clientWidth - 400 + 'px';
    }
    if (parent.CurrentLangID == 'en-US') {
        document.getElementById('cmbPersonnel_PersonnelOrganizationFeaturesChange_DropDown').style.left = '2%';
    }
}


function tlbItemSave_TlbPersonnelOrganizationFeaturesChange_onClick() {
    CollapseControls_PersonnelOrganizationFeaturesChange(null);
    UpdatePersonnelOrganizationFeature_PersonnelOrganizationFeaturesChange();
}

function UpdatePersonnelOrganizationFeature_PersonnelOrganizationFeaturesChange() {
    var DepartmentID = '-1';
    var EmployTypeID = '-1';
    var WorkGroupID = '-1';
    var WorkGroupFromDate = null;
    var CalculationRangeID = '-1';
    var CalculationRangeFromDate = null;
    var RuleGroupID = '-1';
    var RuleGroupFromDate = null;
    var RuleGroupToDate = null;

    var ObjPersonnelOrganizationFeatures = new Object();
    ObjPersonnelOrganizationFeatures.PersonnelLoadState = LoadState_cmbPersonnel_PersonnelOrganizationFeaturesChange;
    ObjPersonnelOrganizationFeatures.StrObjPersonnelOrganizationFeaturesTarget = null;
    ObjPersonnelOrganizationFeatures.PersonnelID = '-1';
    ObjPersonnelOrganizationFeatures.SearchTerm = null;

    ObjPersonnelOrganizationFeatures.SearchTerm = SearchTerm_cmbPersonnel_PersonnelOrganizationFeaturesChange;
    if (cmbPersonnel_PersonnelOrganizationFeaturesChange.getSelectedItem() != undefined)
        ObjPersonnelOrganizationFeatures.PersonnelID = cmbPersonnel_PersonnelOrganizationFeaturesChange.getSelectedItem().get_value();
    if (trvDepartment_PersonnelOrganizationFeaturesChange.get_selectedNode() != undefined)
        DepartmentID = trvDepartment_PersonnelOrganizationFeaturesChange.get_selectedNode().get_id();
    if (cmbEmployType_PersonnelOrganizationFeaturesChange.getSelectedItem() != undefined)
        EmployTypeID = cmbEmployType_PersonnelOrganizationFeaturesChange.getSelectedItem().get_value();
    if (cmbWorkGroup_PersonnelOrganizationFeaturesChange.getSelectedItem() != undefined)
        WorkGroupID = cmbWorkGroup_PersonnelOrganizationFeaturesChange.getSelectedItem().get_value();
    if (cmbCalculationRange_PersonnelOrganizationFeaturesChange.getSelectedItem() != undefined)
        CalculationRangeID = cmbCalculationRange_PersonnelOrganizationFeaturesChange.getSelectedItem().get_value();
    if (cmbRulesGroup_PersonnelOrganizationFeaturesChange.getSelectedItem() != undefined)
        RuleGroupID = cmbRulesGroup_PersonnelOrganizationFeaturesChange.getSelectedItem().get_value();
    switch (parent.SysLangID) {
        case 'fa-IR':
            WorkGroupFromDate = document.getElementById('pdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange').value;
            CalculationRangeFromDate = document.getElementById('pdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange').value;
            RuleGroupFromDate = document.getElementById('pdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange').value;
            RuleGroupToDate = document.getElementById('pdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange').value;
            break;
        case 'en-US':
            WorkGroupFromDate = document.getElementById('gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange').value;
            CalculationRangeFromDate = document.getElementById('gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange').value;
            RuleGroupFromDate = document.getElementById('gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange').value;
            RuleGroupToDate = document.getElementById('gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange').value;            
            break;
    }
    ObjPersonnelOrganizationFeatures.StrObjPersonnelOrganizationFeaturesTarget = '{"DepartmentID":"' + DepartmentID + '","EmployTypeID":"' + EmployTypeID + '","WorkGroupID":"' + WorkGroupID + '","WorkGroupFromDate":"' + WorkGroupFromDate + '","CalculationRangeID":"' + CalculationRangeID + '","CalculationRangeFromDate":"' + CalculationRangeFromDate + '","RuleGroupID":"' + RuleGroupID + '","RuleGroupFromDate":"' + RuleGroupFromDate + '","RuleGroupToDate":"' + RuleGroupToDate + '"}';

    UpdatePersonnelOrganizationFeature_PersonnelOrganizationFeaturesChangePage(CharToKeyCode_PersonnelOrganizationFeaturesChange(ObjPersonnelOrganizationFeatures.PersonnelLoadState), CharToKeyCode_PersonnelOrganizationFeaturesChange(ObjPersonnelOrganizationFeatures.PersonnelID), CharToKeyCode_PersonnelOrganizationFeaturesChange(ObjPersonnelOrganizationFeatures.SearchTerm), CharToKeyCode_PersonnelOrganizationFeaturesChange(ObjPersonnelOrganizationFeatures.StrObjPersonnelOrganizationFeaturesTarget));
    DialogWaiting.Show();
}

function UpdatePersonnelOrganizationFeature_PersonnelOrganizationFeaturesChangePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_PersonnelOrganizationFeaturesChange').value;
            Response[1] = document.getElementById('hfConnectionError_PersonnelOrganizationFeaturesChange').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[3] == 'false')
            Fill_GridPersonnelProblems_PersonnelOrganizationFeaturesChange();
    }
}

function tlbItemHelp_TlbPersonnelOrganizationFeaturesChange_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPersonnelOrganizationFeaturesChange');
}

function tlbItemFormReconstruction_TlbPersonnelOrganizationFeaturesChange_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvPersonnelOrganizationFeaturesChange_iFrame').src = 'PersonnelOrganizationFeaturesChange.aspx';    
}

function tlbItemExit_TlbPersonnelOrganizationFeaturesChange_onClick() {
    ShowDialogConfirm('Exit');    
}

function imgbox_SearchByPersonnel_PersonnelOrganizationFeaturesChange_onClick() {
    CollapseControls_PersonnelOrganizationFeaturesChange(null);
    setSlideDownSpeed(200);

    slidedown_showHide('box_SearchByPersonnel_PersonnelOrganizationFeaturesChange');

    if (box_SearchByPersonnel_PersonnelOrganizationFeaturesChange_IsShown) {
        box_SearchByPersonnel_PersonnelOrganizationFeaturesChange_IsShown = false;
        document.getElementById('imgbox_SearchByPersonnel_PersonnelOrganizationFeaturesChange').src = 'Images/Ghadir/arrowDown.jpg';
    }
    else {
        box_SearchByPersonnel_PersonnelOrganizationFeaturesChange_IsShown = true;
        document.getElementById('imgbox_SearchByPersonnel_PersonnelOrganizationFeaturesChange').src = 'Images/Ghadir/arrowUp.jpg';
    }
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange_onClick() {
    Refresh_cmbPersonnel_PersonnelOrganizationFeaturesChange();
}

function Refresh_cmbPersonnel_PersonnelOrganizationFeaturesChange() {
    LoadState_cmbPersonnel_PersonnelOrganizationFeaturesChange = 'Normal';
    SetPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange(0);
}

function tlbItemFirst_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange_onClick() {
    SetPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange_onClick() {
    if (CurrentPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange != 0) {
        CurrentPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange = CurrentPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange - 1;
        SetPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange(CurrentPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange_onClick() {
    if (CurrentPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange < parseInt(document.getElementById('hfPersonnelPageCount_PersonnelOrganizationFeaturesChange').value) - 1) {
        CurrentPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange = CurrentPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange + 1;
        SetPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange(CurrentPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange_onClick() {
    SetPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange(parseInt(document.getElementById('hfPersonnelPageCount_PersonnelOrganizationFeaturesChange').value) - 1);
}

function SetPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange(pageIndex) {
    CurrentPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange = pageIndex;
    Fill_cmbPersonnel_PersonnelOrganizationFeaturesChange(pageIndex);
}

function Fill_cmbPersonnel_PersonnelOrganizationFeaturesChange(pageIndex) {
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_PersonnelOrganizationFeaturesChange').value);
    switch (LoadState_cmbPersonnel_PersonnelOrganizationFeaturesChange) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm_cmbPersonnel_PersonnelOrganizationFeaturesChange = document.getElementById('txtPersonnelSearch_PersonnelOrganizationFeaturesChange').value;
            break;
        case 'AdvancedSearch':
            break;
    }
    CallBack_cmbPersonnel_PersonnelOrganizationFeaturesChange.callback(CharToKeyCode_PersonnelOrganizationFeaturesChange(LoadState_cmbPersonnel_PersonnelOrganizationFeaturesChange), CharToKeyCode_PersonnelOrganizationFeaturesChange(pageSize.toString()), CharToKeyCode_PersonnelOrganizationFeaturesChange(pageIndex.toString()), CharToKeyCode_PersonnelOrganizationFeaturesChange(SearchTerm_cmbPersonnel_PersonnelOrganizationFeaturesChange));
}

function cmbPersonnel_PersonnelOrganizationFeaturesChange_onExpand(sender, e) {
    CollapseControls_PersonnelOrganizationFeaturesChange(cmbPersonnel_PersonnelOrganizationFeaturesChange);
    SetPosition_cmbPersonnel_PersonnelOrganizationFeaturesChange();
    if (cmbPersonnel_PersonnelOrganizationFeaturesChange.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_PersonnelOrganizationFeaturesChange == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_PersonnelOrganizationFeaturesChange = true;
        SetPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange(0);
    }
}

function CallBack_cmbPersonnel_PersonnelOrganizationFeaturesChange_onBeforeCallback(sender, e) {
    cmbPersonnel_PersonnelOrganizationFeaturesChange.dispose();
}

function CallBack_cmbPersonnel_PersonnelOrganizationFeaturesChange_onCallBackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_PersonnelOrganizationFeaturesChange').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_PersonnelOrganizationFeaturesChange').value;
    document.getElementById('clmnBarCode_cmbPersonnel_PersonnelOrganizationFeaturesChange').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_PersonnelOrganizationFeaturesChange').value;
    document.getElementById('clmnCardNum_cmbPersonnel_PersonnelOrganizationFeaturesChange').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_PersonnelOrganizationFeaturesChange').value;

    var error = document.getElementById('ErrorHiddenField_Personnel_PersonnelOrganizationFeaturesChange').value;
    if (error == "") {        
        document.getElementById('cmbPersonnel_PersonnelOrganizationFeaturesChange_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbPersonnel_PersonnelOrganizationFeaturesChange = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbPersonnel_PersonnelOrganizationFeaturesChange = false;
        cmbPersonnel_PersonnelOrganizationFeaturesChange.expand();
        SetPersonnelCountHeader_PersonnelOrganizationFeaturesChange('CallBack');
        SetPosition_cmbPersonnel_PersonnelOrganizationFeaturesChange();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_PersonnelOrganizationFeaturesChange_DropDown').style.display = 'none';
    }
}

function CallBack_cmbPersonnel_PersonnelOrganizationFeaturesChange_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelOrganizationFeaturesChange();
}

function tlbItemSearch_TlbSearchPersonnel_PersonnelOrganizationFeaturesChange_onClick() {
    LoadState_cmbPersonnel_PersonnelOrganizationFeaturesChange = 'Search';
    SetPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_PersonnelOrganizationFeaturesChange_onClick() {
    LoadState_cmbPersonnel_PersonnelOrganizationFeaturesChange = 'AdvancedSearch';
    ShowDialogPersonnelSearch('PersonnelOrganizationFeaturesChange');
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
    CollapseControls_PersonnelOrganizationFeaturesChange(null);
}

function trvDepartment_PersonnelOrganizationFeaturesChange_onNodeSelect(sender, e) {
    cmbDepartment_PersonnelOrganizationFeaturesChange.set_text(e.get_node().get_text());
    cmbDepartment_PersonnelOrganizationFeaturesChange.collapse();
}

function cmbDepartment_PersonnelOrganizationFeaturesChange_onExpand(sender, e) {
    CollapseControls_PersonnelOrganizationFeaturesChange(cmbDepartment_PersonnelOrganizationFeaturesChange);
    if (trvDepartment_PersonnelOrganizationFeaturesChange.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDepartment_PersonnelOrganizationFeaturesChange == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDepartment_PersonnelOrganizationFeaturesChange = true;
        CallBack_cmbDepartment_PersonnelOrganizationFeaturesChange.callback();
    }
}

function CallBack_cmbDepartment_PersonnelOrganizationFeaturesChange_onBeforeCallback(sender, e) {
    cmbDepartment_PersonnelOrganizationFeaturesChange.collapse();
}

function CallBack_cmbDepartment_PersonnelOrganizationFeaturesChange_onCallbackComplete(sender, e) {
    ChangeComboTreeDirection_PersonnelOrganizationFeaturesChange(parent.CurrentLangID);
    var error = document.getElementById('ErrorHiddenField_Department_PersonnelOrganizationFeaturesChange').value;
    if (error == "") {
        document.getElementById('cmbDepartment_PersonnelOrganizationFeaturesChange_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbDepartment_PersonnelOrganizationFeaturesChange = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbDepartment_PersonnelOrganizationFeaturesChange = false;
        cmbDepartment_PersonnelOrganizationFeaturesChange.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbDepartment_PersonnelOrganizationFeaturesChange_DropDown').style.display = 'none';
    }
}

function CallBack_cmbDepartment_PersonnelOrganizationFeaturesChange_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelOrganizationFeaturesChange();
}

function Refresh_cmbDepartment_PersonnelOrganizationFeaturesChange() {
    CallBack_cmbDepartment_PersonnelOrganizationFeaturesChange.callback();
}

function cmbEmployType_PersonnelOrganizationFeaturesChange_onExpand(sender, e) {
    CollapseControls_PersonnelOrganizationFeaturesChange(cmbEmployType_PersonnelOrganizationFeaturesChange);
    if (cmbEmployType_PersonnelOrganizationFeaturesChange.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbEmployType_PersonnelOrganizationFeaturesChange == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbEmployType_PersonnelOrganizationFeaturesChange = true;
        CallBack_cmbEmployType_PersonnelOrganizationFeaturesChange.callback();
    }
}

function CallBack_cmbEmployType_PersonnelOrganizationFeaturesChange_onBeforeCallback(sender, e) {
    cmbEmployType_PersonnelOrganizationFeaturesChange.dispose();
}

function CallBack_cmbEmployType_PersonnelOrganizationFeaturesChange_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_EmployType_PersonnelOrganizationFeaturesChange').value;
    if (error == "") {
        document.getElementById('cmbEmployType_PersonnelOrganizationFeaturesChange_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbEmployType_PersonnelOrganizationFeaturesChange = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbEmployType_PersonnelOrganizationFeaturesChange = false;
        cmbEmployType_PersonnelOrganizationFeaturesChange.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbEmployType_PersonnelOrganizationFeaturesChange_DropDown').style.display = 'none';
    }
}

function CallBack_cmbEmployType_PersonnelOrganizationFeaturesChange_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelOrganizationFeaturesChange();
}

function Refresh_cmbEmployType_PersonnelOrganizationFeaturesChange() {
    CallBack_cmbEmployType_PersonnelOrganizationFeaturesChange.callback();
}

function cmbWorkGroup_PersonnelOrganizationFeaturesChange_onExpand(sender, e) {
    CollapseControls_PersonnelOrganizationFeaturesChange(cmbWorkGroup_PersonnelOrganizationFeaturesChange);
    if (cmbWorkGroup_PersonnelOrganizationFeaturesChange.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbWorkGroup_PersonnelOrganizationFeaturesChange == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbWorkGroup_PersonnelOrganizationFeaturesChange = true;
        CallBack_cmbWorkGroup_PersonnelOrganizationFeaturesChange.callback();
    }
}

function CallBack_cmbWorkGroup_PersonnelOrganizationFeaturesChange_onBeforeCallback(sender, e) {
    cmbWorkGroup_PersonnelOrganizationFeaturesChange.dispose();
}

function CallBack_cmbWorkGroup_PersonnelOrganizationFeaturesChange_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkGroup_PersonnelOrganizationFeaturesChange').value;
    if (error == "") {
        document.getElementById('cmbWorkGroup_PersonnelOrganizationFeaturesChange_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbWorkGroup_PersonnelOrganizationFeaturesChange = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbWorkGroup_PersonnelOrganizationFeaturesChange = false;
        cmbWorkGroup_PersonnelOrganizationFeaturesChange.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbWorkGroup_PersonnelOrganizationFeaturesChange_DropDown').style.display = 'none';
    }
}

function CallBack_cmbWorkGroup_PersonnelOrganizationFeaturesChange_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelOrganizationFeaturesChange();
}

function Refresh_cmbWorkGroup_PersonnelOrganizationFeaturesChange() {
    CallBack_cmbWorkGroup_PersonnelOrganizationFeaturesChange.callback();
}

function tlbItemClear_TlbClear_WorkGroupCalendars_PersonnelOrganizationFeaturesChange_onClick() {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange_picker').value = "";
            break;
    }
}

function cmbCalculationRange_PersonnelOrganizationFeaturesChange_onExpand(sender, e) {
    CollapseControls_PersonnelOrganizationFeaturesChange(cmbCalculationRange_PersonnelOrganizationFeaturesChange);
    if (cmbCalculationRange_PersonnelOrganizationFeaturesChange.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbCalculationRange_PersonnelOrganizationFeaturesChange == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbCalculationRange_PersonnelOrganizationFeaturesChange = true;
        CallBack_cmbCalculationRange_PersonnelOrganizationFeaturesChange.callback();
    }
}

function CallBack_cmbCalculationRange_PersonnelOrganizationFeaturesChange_onBeforeCallback(sender, e) {
    cmbCalculationRange_PersonnelOrganizationFeaturesChange.dispose();
}

function CallBack_cmbCalculationRange_PersonnelOrganizationFeaturesChange_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_CalculationRange_PersonnelOrganizationFeaturesChange').value;
    if (error == "") {
        document.getElementById('cmbCalculationRange_PersonnelOrganizationFeaturesChange_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbCalculationRange_PersonnelOrganizationFeaturesChange = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbCalculationRange_PersonnelOrganizationFeaturesChange = false;
        cmbCalculationRange_PersonnelOrganizationFeaturesChange.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbCalculationRange_PersonnelOrganizationFeaturesChange_DropDown').style.display = 'none';
    }
}

function CallBack_cmbCalculationRange_PersonnelOrganizationFeaturesChange_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelOrganizationFeaturesChange();
}

function Refresh_cmbCalculationRange_PersonnelOrganizationFeaturesChange() {
    CallBack_cmbCalculationRange_PersonnelOrganizationFeaturesChange.callback();
}

function tlbItemClear_TlbClear_CalculationRangeCalendars_PersonnelOrganizationFeaturesChange_onClick() {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange_picker').value = "";
            break;
    }
}

function cmbRulesGroup_PersonnelOrganizationFeaturesChange_onExpand(sender, e) {
    CollapseControls_PersonnelOrganizationFeaturesChange(cmbRulesGroup_PersonnelOrganizationFeaturesChange);
    if (cmbRulesGroup_PersonnelOrganizationFeaturesChange.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRulesGroup_PersonnelOrganizationFeaturesChange == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRulesGroup_PersonnelOrganizationFeaturesChange = true;
        CallBack_cmbRulesGroup_PersonnelOrganizationFeaturesChange.callback();
    }
}

function CallBack_cmbRulesGroup_PersonnelOrganizationFeaturesChange_onBeforeCallback(sender, e) {
    cmbRulesGroup_PersonnelOrganizationFeaturesChange.dispose();
}

function CallBack_cmbRulesGroup_PersonnelOrganizationFeaturesChange_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RulesGroup_PersonnelOrganizationFeaturesChange').value;
    if (error == "") {
        document.getElementById('cmbRulesGroup_PersonnelOrganizationFeaturesChange_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbRulesGroup_PersonnelOrganizationFeaturesChange = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbRulesGroup_PersonnelOrganizationFeaturesChange = false;
        cmbRulesGroup_PersonnelOrganizationFeaturesChange.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbRulesGroup_PersonnelOrganizationFeaturesChange_DropDown').style.display = 'none';
    }
}

function CallBack_cmbRulesGroup_PersonnelOrganizationFeaturesChange_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelOrganizationFeaturesChange();
}

function Refresh_cmbRulesGroup_PersonnelOrganizationFeaturesChange() {
    CallBack_cmbRulesGroup_PersonnelOrganizationFeaturesChange.callback();    
}

function tlbItemClear_TlbClear_RulesGroupCalendars_FromDate_PersonnelOrganizationFeaturesChange_onClick() {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange_picker').value = "";
            break;
    }
}

function tlbItemClear_TlbClear_RulesGroupCalendars_ToDate_PersonnelOrganizationFeaturesChange_onClick() {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange_picker').value = "";
            break;
    }
}

function Refresh_GridPersonnelProblems_PersonnelOrganizationFeaturesChange() {
    Fill_GridPersonnelProblems_PersonnelOrganizationFeaturesChange();
}

function Fill_GridPersonnelProblems_PersonnelOrganizationFeaturesChange() {
    document.getElementById('loadingPanel_GridPersonnelProblems_PersonnelOrganizationFeaturesChange').innerHTML = document.getElementById('hfloadingPanel_GridPersonnelProblems_PersonnelOrganizationFeaturesChange').value;
    CallBack_GridPersonnelProblems_PersonnelOrganizationFeaturesChange.callback();
}

function GridPersonnelProblems_PersonnelOrganizationFeaturesChange_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridPersonnelProblems_PersonnelOrganizationFeaturesChange').innerHTML = '';
}

function CallBack_GridPersonnelProblems_PersonnelOrganizationFeaturesChange_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_PersonnelProblems_PersonnelOrganizationFeaturesChange').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridPersonnelProblems_PersonnelOrganizationFeaturesChange();
    }
}

function CallBack_GridPersonnelProblems_PersonnelOrganizationFeaturesChange_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelOrganizationFeaturesChange();
}

function CharToKeyCode_PersonnelOrganizationFeaturesChange(str) {
    var OutStr = '';
    if (str != null && str != undefined) {
        for (var i = 0; i < str.length; i++) {
            var KeyCode = str.charCodeAt(i);
            var CharKeyCode = '//' + KeyCode;
            OutStr += CharKeyCode;
        }
    }
    return OutStr;
}

function ShowConnectionError_PersonnelOrganizationFeaturesChange() {
    var error = document.getElementById('hfErrorType_PersonnelOrganizationFeaturesChange').value;
    var errorBody = document.getElementById('hfConnectionError_PersonnelOrganizationFeaturesChange').value;
    showDialog(error, errorBody, 'error');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_PersonnelOrganizationFeaturesChange = confirmState;
    switch (ConfirmState_PersonnelOrganizationFeaturesChange) {
        case 'Exit':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PersonnelOrganizationFeaturesChange').value;
            break;
    }
    DialogConfirm.Show();
    CollapseControls_PersonnelOrganizationFeaturesChange(null);
}

function CollapseControls_PersonnelOrganizationFeaturesChange(exception) {
    if (exception == null || exception != cmbPersonnel_PersonnelOrganizationFeaturesChange)
        cmbPersonnel_PersonnelOrganizationFeaturesChange.collapse();
    if (exception == null || exception != cmbDepartment_PersonnelOrganizationFeaturesChange)
        cmbDepartment_PersonnelOrganizationFeaturesChange.collapse();
    if (exception == null || exception != cmbEmployType_PersonnelOrganizationFeaturesChange)
        cmbEmployType_PersonnelOrganizationFeaturesChange.collapse();
    if (exception == null || exception != cmbWorkGroup_PersonnelOrganizationFeaturesChange)
        cmbWorkGroup_PersonnelOrganizationFeaturesChange.collapse();
    if (exception == null || exception != cmbCalculationRange_PersonnelOrganizationFeaturesChange)
        cmbCalculationRange_PersonnelOrganizationFeaturesChange.collapse();
    if (exception == null || exception != cmbRulesGroup_PersonnelOrganizationFeaturesChange)
        cmbRulesGroup_PersonnelOrganizationFeaturesChange.collapse();
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange');
}

function ChangeComboTreeDirection_PersonnelOrganizationFeaturesChange(CurrentLangID) {
    if (CurrentLangID == 'fa-IR') 
        document.getElementById('cmbDepartment_PersonnelOrganizationFeaturesChange_DropDownContent').style.direction = 'rtl';
    if (CurrentLangID == 'en-US') 
        document.getElementById('cmbDepartment_PersonnelOrganizationFeaturesChange_DropDownContent').style.direction = 'ltr';
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function ResetCalendars_PersonnelOrganizationFeaturesChange() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById('gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange').value = '';
            document.getElementById('gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange').value = '';
            document.getElementById('gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange').value = '';
            document.getElementById('gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange').value = '';
            break;
        case 'fa-IR':
            document.getElementById('pdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange').value = '';
            document.getElementById('pdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange').value = '';
            document.getElementById('pdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange').value = '';
            document.getElementById('pdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange').value = '';
            break;
    }
}

function SetPersonnelCountHeader_PersonnelOrganizationFeaturesChange(state) {
    var tragetPersonnelCount = 0;
    switch (state) {
        case 'CallBack':
            tragetPersonnelCount = document.getElementById('hfPersonnelCount_PersonnelOrganizationFeaturesChange').value;
            break;
        case 'Load':
            tragetPersonnelCount = document.getElementById('hfPersonnelCount_PersonnelOrganizationFeaturesChange').value;
            break;            
        case 'Change':
            tragetPersonnelCount = 1;
            break;
    }
    document.getElementById('tdPersonnelCount_PersonnelOrganizationFeaturesChange').innerHTML = document.getElementById('hfheaderMasterPersonnelCount_PersonnelOrganizationFeaturesChange').value + ' ' + tragetPersonnelCount;
}

function PersonnelOrganizationFeaturesChange_onAfterPersonnelAdvancedSearch(SearchTerm) {
    SearchTerm_cmbPersonnel_PersonnelOrganizationFeaturesChange = SearchTerm;
    SetPageIndex_cmbPersonnel_PersonnelOrganizationFeaturesChange(0);
}

function cmbPersonnel_PersonnelOrganizationFeaturesChange_onChange(sender, e) {
    if (cmbPersonnel_PersonnelOrganizationFeaturesChange != null)
        SetPersonnelCountHeader_PersonnelOrganizationFeaturesChange('Change');
}

function GridPersonnelProblems_PersonnelOrganizationFeaturesChange_onCelldbClick() {
    ShowDialogRequestDescription();
}

function ShowDialogRequestDescription() {
    var SelectedItems_GridPersonnelProblems_PersonnelOrganizationFeaturesChange = GridPersonnelProblems_PersonnelOrganizationFeaturesChange.getSelectedItems();
    if (SelectedItems_GridPersonnelProblems_PersonnelOrganizationFeaturesChange.length > 0) {
        document.getElementById('txtDescription_RequestDescription_PersonnelOrganizationFeaturesChange').value = SelectedItems_GridPersonnelProblems_PersonnelOrganizationFeaturesChange.getMember('Description').get_text();
        DialogRequestDescription.Show();
    }
}

function tlbItemOk_TlbOkConfirm_onClick() {
    parent.CloseCurrentTabOnTabStripMenus();   
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}















