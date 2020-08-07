
var CurrentPageIndex_cmbPersonnel_PersonalFilter_ReportParameters = '0';
var LoadState_cmbPersonnel_PersonalFilter_ReportParameters = 'Normal';
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjexpandingOrgPostNode_ReportParameters = null;
var ObjReportParameters_ReportParameters = null;
var PersonnelCount_PersonnelSelect = 0;
var StrSelectivePersonnelList_PersonnelSelect = '';
var CachedPersonnelFilterObj = '';
var CurrentPageIndex_GridPersonnel_PersonnelSelect = 0;


function ResetCalendars_ReportParameters() {
    var currentDate_ReportParameters = document.getElementById('hfCurrentDate_ReportParameters').value;
    switch (parent.SysLangID) {
        case 'en-US':
            //            currentDate_ReportParameters = new Date(currentDate_ReportParameters);
            //            gdpWorkGroup_GroupFilter_ReportParameters.setSelectedDate(currentDate_ReportParameters);
            //            gdpFromDate_RuleGroup_GroupFilter_ReportParameters.setSelectedDate(currentDate_ReportParameters);
            //            gdpToDate_RuleGroup_GroupFilter_ReportParameters.setSelectedDate(currentDate_ReportParameters);
            //            gdpRunFromDate_GroupFilter_ReportParameters.setSelectedDate(currentDate_ReportParameters);
            break;
        case 'fa-IR':
            currentDate_ReportParameters = '';
            document.getElementById('pdpWorkGroup_GroupFilter_ReportParameters').value = currentDate_ReportParameters;
            document.getElementById('pdpFromDate_RuleGroup_GroupFilter_ReportParameters').value = currentDate_ReportParameters;
            document.getElementById('pdpToDate_RuleGroup_GroupFilter_ReportParameters').value = currentDate_ReportParameters;
            document.getElementById('pdpRunFromDate_GroupFilter_ReportParameters').value = currentDate_ReportParameters;
            break;
    }
}

function GetBoxesHeaders_ReportParameters() {
    SetTitle_DialogReportParameters_ReportPrameters();
    document.getElementById('headerPersonnelFilter_PersonalFilter_ReportParameters').innerHTML = document.getElementById('hfheaderPersonnelFilter_PersonalFilter_ReportParameters').value;
    document.getElementById('headerPersonnelFilter_GroupFilter_ReportParameters').innerHTML = document.getElementById('hfheaderPersonnelFilter_GroupFilter_ReportParameters').value;
    document.getElementById('headerReportParameters_ReportParameters').innerHTML = document.getElementById('hfheaderReportParameters_ReportParameters').value;
    document.getElementById('clmnName_cmbPersonnel_PersonalFilter_ReportParameters').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_PersonalFilter_ReportParameters').value;
    document.getElementById('clmnBarCode_cmbPersonnel_PersonalFilter_ReportParameters').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_PersonalFilter_ReportParameters').value;
    document.getElementById('clmnCardNum_cmbPersonnel_PersonalFilter_ReportParameters').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_PersonalFilter_ReportParameters').value;
}

function SetTitle_DialogReportParameters_ReportPrameters() {
    var ObjReportParameters = parent.DialogReportParameters.get_value();
    var ReportName = ObjReportParameters.ReportName;
    var DialogReportParametersTitle = null;
    switch (parent.SysLangID) {
        case 'fa-IR':
            DialogReportParametersTitle = document.getElementById('hfTitle_DialogReportParameters').value + ' ' + ReportName;
            break;
        case 'en-US':
            DialogReportParametersTitle = ReportName + ' ' + document.getElementById('hfTitle_DialogReportParameters').value;
            break;
    }
    parent.document.getElementById('Title_DialogReportParameters').innerHTML = DialogReportParametersTitle;
}

function ViewCurrentLangCalendars_ReportParameters() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpWorkGroup_GroupFilter_ReportParameters").parentNode.removeChild(document.getElementById("pdpWorkGroup_GroupFilter_ReportParameters"));
            document.getElementById("pdpWorkGroup_GroupFilter_ReportParametersimgbt").parentNode.removeChild(document.getElementById("pdpWorkGroup_GroupFilter_ReportParametersimgbt"));
            document.getElementById("pdpFromDate_RuleGroup_GroupFilter_ReportParameters").parentNode.removeChild(document.getElementById("pdpFromDate_RuleGroup_GroupFilter_ReportParameters"));
            document.getElementById("pdpFromDate_RuleGroup_GroupFilter_ReportParametersimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_RuleGroup_GroupFilter_ReportParametersimgbt"));
            document.getElementById("pdpToDate_RuleGroup_GroupFilter_ReportParameters").parentNode.removeChild(document.getElementById("pdpToDate_RuleGroup_GroupFilter_ReportParameters"));
            document.getElementById("pdpToDate_RuleGroup_GroupFilter_ReportParametersimgbt").parentNode.removeChild(document.getElementById("pdpToDate_RuleGroup_GroupFilter_ReportParametersimgbt"));
            document.getElementById("pdpRunFromDate_GroupFilter_ReportParameters").parentNode.removeChild(document.getElementById("pdpRunFromDate_GroupFilter_ReportParameters"));
            document.getElementById("pdpRunFromDate_GroupFilter_ReportParametersimgbt").parentNode.removeChild(document.getElementById("pdpRunFromDate_GroupFilter_ReportParametersimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_WorkGroupCalendars_GroupFilter_ReportParameters").removeChild(document.getElementById("Container_gCalWorkGroup_GroupFilter_ReportParameters"));
            document.getElementById("Container_FromDateCalendars_RuleGroup_GroupFilter_ReportParameters").removeChild(document.getElementById("Container_gCalFromDate_RuleGroup_GroupFilter_ReportParameters"));
            document.getElementById("Container_ToDateCalendars_RuleGroup_GroupFilter_ReportParameters").removeChild(document.getElementById("Container_gCalToDate_RuleGroup_GroupFilter_ReportParameters"));
            document.getElementById("Container_RunFromDateCalndars_GroupFilter_ReportParameters").removeChild(document.getElementById("Container_gCalRunFromDate_GroupFilter_ReportParameters"));
            break;
    }
}

function gdpWorkGroup_GroupFilter_ReportParameters_OnDateChange(sender, eventArgs) {
    var BirthDate = gdpWorkGroup_GroupFilter_ReportParameters.getSelectedDate();
    gCalWorkGroup_GroupFilter_ReportParameters.setSelectedDate(BirthDate);
}

function gCalWorkGroup_GroupFilter_ReportParameters_OnChange(sender, eventArgs) {
    var BirthDate = gCalWorkGroup_GroupFilter_ReportParameters.getSelectedDate();
    gdpWorkGroup_GroupFilter_ReportParameters.setSelectedDate(BirthDate);
}

function btn_gdpWorkGroup_GroupFilter_ReportParameters_OnClick(event) {
    if (gCalWorkGroup_GroupFilter_ReportParameters.get_popUpShowing()) {
        gCalWorkGroup_GroupFilter_ReportParameters.hide();
    }
    else {
        gCalWorkGroup_GroupFilter_ReportParameters.setSelectedDate(gdpWorkGroup_GroupFilter_ReportParameters.getSelectedDate());
        gCalWorkGroup_GroupFilter_ReportParameters.show();
    }
}

function btn_gdpWorkGroup_GroupFilter_ReportParameters_OnMouseUp(event) {
    if (gCalWorkGroup_GroupFilter_ReportParameters.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalWorkGroup_GroupFilter_ReportParameters_onLoad(sender, e) {
    window.gCalWorkGroup_GroupFilter_ReportParameters.PopUpObject.z = 25000000;
}

function TlbItemClean_TlbClean_WorkGroupCalendars_GroupFilter_ReportParameters_onClick() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpWorkGroup_GroupFilter_ReportParameters').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpWorkGroup_GroupFilter_ReportParameters_picker').value = "";
            break;
    }
}

function gdpFromDate_RuleGroup_GroupFilter_ReportParameters_OnDateChange(sender, eventArgs) {
    var BirthDate = gdpFromDate_RuleGroup_GroupFilter_ReportParameters.getSelectedDate();
    gCalFromDate_RuleGroup_GroupFilter_ReportParameters.setSelectedDate(BirthDate);
}
function gCalFromDate_RuleGroup_GroupFilter_ReportParameters_OnChange(sender, eventArgs) {
    var BirthDate = gCalFromDate_RuleGroup_GroupFilter_ReportParameters.getSelectedDate();
    gdpFromDate_RuleGroup_GroupFilter_ReportParameters.setSelectedDate(BirthDate);
}
function btn_gdpFromDate_RuleGroup_GroupFilter_ReportParameters_OnClick(event) {
    if (gCalFromDate_RuleGroup_GroupFilter_ReportParameters.get_popUpShowing()) {
        gCalFromDate_RuleGroup_GroupFilter_ReportParameters.hide();
    }
    else {
        gCalFromDate_RuleGroup_GroupFilter_ReportParameters.setSelectedDate(gdpFromDate_RuleGroup_GroupFilter_ReportParameters.getSelectedDate());
        gCalFromDate_RuleGroup_GroupFilter_ReportParameters.show();
    }
}
function btn_gdpFromDate_RuleGroup_GroupFilter_ReportParameters_OnMouseUp(event) {
    if (gCalFromDate_RuleGroup_GroupFilter_ReportParameters.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalFromDate_RuleGroup_GroupFilter_ReportParameters_onLoad(sender, e) {
    window.gCalFromDate_RuleGroup_GroupFilter_ReportParameters.PopUpObject.z = 25000000;
}

function tlbItemClean_TlbClean_FromDateCalendars_RuleGroup_GroupFilter_ReportParameters_onClick() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_RuleGroup_GroupFilter_ReportParameters').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_RuleGroup_GroupFilter_ReportParameters_picker').value = "";
            break;
    }
}

function gdpToDate_RuleGroup_GroupFilter_ReportParameters_OnDateChange(sender, eventArgs) {
    var BirthDate = gdpToDate_RuleGroup_GroupFilter_ReportParameters.getSelectedDate();
    gCalToDate_RuleGroup_GroupFilter_ReportParameters.setSelectedDate(BirthDate);
}
function gCalToDate_RuleGroup_GroupFilter_ReportParameters_OnChange(sender, eventArgs) {
    var BirthDate = gCalToDate_RuleGroup_GroupFilter_ReportParameters.getSelectedDate();
    gdpToDate_RuleGroup_GroupFilter_ReportParameters.setSelectedDate(BirthDate);
}
function btn_gdpToDate_RuleGroup_GroupFilter_ReportParameters_OnClick(event) {
    if (gCalToDate_RuleGroup_GroupFilter_ReportParameters.get_popUpShowing()) {
        gCalToDate_RuleGroup_GroupFilter_ReportParameters.hide();
    }
    else {
        gCalToDate_RuleGroup_GroupFilter_ReportParameters.setSelectedDate(gdpToDate_RuleGroup_GroupFilter_ReportParameters.getSelectedDate());
        gCalToDate_RuleGroup_GroupFilter_ReportParameters.show();
    }
}
function btn_gdpToDate_RuleGroup_GroupFilter_ReportParameters_OnMouseUp(event) {
    if (gCalToDate_RuleGroup_GroupFilter_ReportParameters.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalToDate_RuleGroup_GroupFilter_ReportParameters_onLoad(sender, e) {
    window.gCalToDate_RuleGroup_GroupFilter_ReportParameters.PopUpObject.z = 25000000;
}

function tlbItemClean_TlbClean_ToDateCalendars_RuleGroup_GroupFilter_ReportParameters_onClick() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpToDate_RuleGroup_GroupFilter_ReportParameters').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpToDate_RuleGroup_GroupFilter_ReportParameters_picker').value = "";
            break;
    }
}

function tlbItemClean_TlbClean_CalculationRangeCalendars_GroupFilter_ReportParameters_onClick() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpRunFromDate_GroupFilter_ReportParameters').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpRunFromDate_GroupFilter_ReportParameters_picker').value = "";
            break;
    }
}

function gdpRunFromDate_GroupFilter_ReportParameters_OnDateChange(sender, eventArgs) {
    var BirthDate = gdpRunFromDate_GroupFilter_ReportParameters.getSelectedDate();
    gCalRunFromDate_GroupFilter_ReportParameters.setSelectedDate(BirthDate);
}
function gCalRunFromDate_GroupFilter_ReportParameters_OnChange(sender, eventArgs) {
    var BirthDate = gCalRunFromDate_GroupFilter_ReportParameters.getSelectedDate();
    gdpRunFromDate_GroupFilter_ReportParameters.setSelectedDate(BirthDate);
}
function btn_gdpRunFromDate_GroupFilter_ReportParameters_OnClick(event) {
    if (gCalRunFromDate_GroupFilter_ReportParameters.get_popUpShowing()) {
        gCalRunFromDate_GroupFilter_ReportParameters.hide();
    }
    else {
        gCalRunFromDate_GroupFilter_ReportParameters.setSelectedDate(gdpRunFromDate_GroupFilter_ReportParameters.getSelectedDate());
        gCalRunFromDate_GroupFilter_ReportParameters.show();
    }
}
function btn_gdpRunFromDate_GroupFilter_ReportParameters_OnMouseUp(event) {
    if (gCalRunFromDate_GroupFilter_ReportParameters.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalRunFromDate_GroupFilter_ReportParameters_onLoad(sender, e) {
    window.gCalRunFromDate_GroupFilter_ReportParameters.PopUpObject.z = 25000000;
}

function tlbItemClean_TlbClean_RunFromDateCalendars_RuleGroup_GroupFilter_ReportParameters_onClick() {
}

function tlbItemReportView_TlbPersoanlFilter_PersonalFilter_ReportParameters_onClick() {
    GetReport_ReportParameters('Personal');
}

function tlbItemExit_TlbPersoanlFilter_PersonalFilter_ReportParameters_onClick() {
    ShowDialogConfirm('ReportParameters');
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters_onClick() {
    Refresh_cmbPersonnel_PersonalFilter_ReportParameters();
}

function Refresh_cmbPersonnel_PersonalFilter_ReportParameters() {
    LoadState_cmbPersonnel_PersonalFilter_ReportParameters = 'Normal';
    SetPageIndex_cmbPersonnel_PersonalFilter_ReportParameters(0);
}

function tlbItemFirst_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters_onClick() {
    SetPageIndex_cmbPersonnel_PersonalFilter_ReportParameters(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters_onClick() {
    if (CurrentPageIndex_cmbPersonnel_PersonalFilter_ReportParameters != 0) {
        CurrentPageIndex_cmbPersonnel_PersonalFilter_ReportParameters = CurrentPageIndex_cmbPersonnel_PersonalFilter_ReportParameters - 1;
        SetPageIndex_cmbPersonnel_PersonalFilter_ReportParameters(CurrentPageIndex_cmbPersonnel_PersonalFilter_ReportParameters);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters_onClick() {
    if (CurrentPageIndex_cmbPersonnel_PersonalFilter_ReportParameters < parseInt(document.getElementById('hfPersonnelPageCount_PersonalFilter_ReportParameters').value) - 1) {
        CurrentPageIndex_cmbPersonnel_PersonalFilter_ReportParameters = CurrentPageIndex_cmbPersonnel_PersonalFilter_ReportParameters + 1;
        SetPageIndex_cmbPersonnel_PersonalFilter_ReportParameters(CurrentPageIndex_cmbPersonnel_PersonalFilter_ReportParameters);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters_onClick() {
    SetPageIndex_cmbPersonnel_PersonalFilter_ReportParameters(parseInt(document.getElementById('hfPersonnelPageCount_PersonalFilter_ReportParameters').value) - 1);
}

function SetPageIndex_cmbPersonnel_PersonalFilter_ReportParameters(pageIndex) {
    CurrentPageIndex_cmbPersonnel_PersonalFilter_ReportParameters = pageIndex;
    Fill_cmbPersonnel_PersonalFilter_ReportParameters(pageIndex);
}

function Fill_cmbPersonnel_PersonalFilter_ReportParameters(pageIndex) {
    var SearchTerm = '';
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_ReportParameters').value);
    switch (LoadState_cmbPersonnel_PersonalFilter_ReportParameters) {
        case 'Normal':
            break;
        case 'Search':
            var PersonnelFirstName = document.getElementById('txtFirstName_PersonalFilter_ReportParameters').value;
            var PersonnelLastName = document.getElementById('txtLastName_PersonalFilter_ReportParameters').value;
            var PersonnelFatherName = document.getElementById('txtFatherName_PersonalFilter_ReportParameters').value;
            var PersonnelNumber = document.getElementById('txtPersonnelNumber_PersonalFilter_ReportParameters').value;
            var PersonnelCardNumber = document.getElementById('txtCardNumber_PersonalFilter_ReportParameters').value;
            var PersonnelOrganizationPostID = '0';
            if (trvOrganizationPost_PersonalFilter_ReportParameters.get_selectedNode() != undefined) {
                PersonnelOrganizationPostID = trvOrganizationPost_PersonalFilter_ReportParameters.get_selectedNode().get_id();
            }
            SearchTerm = '{"FirstName":"' + PersonnelFirstName + '","LastName":"' + PersonnelLastName + '","FatherName":"' + PersonnelFatherName + '","PersonnelNumber":"' + PersonnelNumber + '","CardNumber":"' + PersonnelCardNumber + '","OrganizationPostID":"' + PersonnelOrganizationPostID + '"}';
            break;
    }
    CallBack_cmbPersonnel_PersonalFilter_ReportParameters.callback(CharToKeyCode_ReportParameters(LoadState_cmbPersonnel_PersonalFilter_ReportParameters), CharToKeyCode_ReportParameters(pageSize.toString()), CharToKeyCode_ReportParameters(pageIndex.toString()), CharToKeyCode_ReportParameters(SearchTerm));
}

function CharToKeyCode_ReportParameters(str) {
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

function tlbItemFilter_TlbFilter_PersonalFilter_ReportParameters_onClick() {
    LoadState_cmbPersonnel_PersonalFilter_ReportParameters = 'Search';
    SetPageIndex_cmbPersonnel_PersonalFilter_ReportParameters(0);
}

function Refresh_cmbOrganizationPost_PersonalFilter_ReportParameters() {
    Fill_cmbOrganizationPost_PersonalFilter_ReportParameters();
}

function tlbItemClean_TlbRefresh_cmbOrganizationPost_PersonalFilter_ReportParameters_onClick() {
    document.getElementById('cmbOrganizationPost_PersonalFilter_ReportParameters_Input').value = '';
    cmbOrganizationPost_PersonalFilter_ReportParameters.unSelect();
    trvOrganizationPost_PersonalFilter_ReportParameters.SelectedNode = undefined;
}

function tlbItemReportView_TlbPersonalFilter_GroupFilter_ReportParameters_onClick() {
    if (document.getElementById('chbAllPersonWithThisConditions_ReportParameters').checked)
        GetReport_ReportParameters('Group');
    else
        GetReport_ReportParameters('SelectInGroup');
}

function tlbItemExit_TlbPersonalFilter_GroupFilter_ReportParameters_onClick() {
    ShowDialogConfirm('ReportParameters');
}

function Refresh_cmbDepartment_GroupFilter_ReportParameters() {
    Fill_cmbDepartment_GroupFilter_ReportParameters();
}

function tlbItemClean_TlbRefresh_cmbDepartment_GroupFilter_ReportParameters_onClick() {
    document.getElementById('cmbDepartment_GroupFilter_ReportParameters_Input').value = '';
    cmbDepartment_GroupFilter_ReportParameters.unSelect();
    trvDepartment_GroupFilter_ReportParameters.SelectedNode = undefined;
}

function Refresh_cmbWorkGroup_GroupFilter_ReportParameters() {
    Fill_cmbWorkGroup_GroupFilter_ReportParameters();
}

function tlbItemClean_TlbRefresh_cmbWorkGroup_GroupFilter_ReportParameters_onClick() {
    document.getElementById('cmbWorkGroup_GroupFilter_ReportParameters_Input').value = '';
    cmbWorkGroup_GroupFilter_ReportParameters.unSelect();
}

function Refresh_cmbCalculationRange_GroupFilter_ReportParameters() {
    Fill_cmbCalculationRange_GroupFilter_ReportParameters();
}

function tlbItemClean_TlbRefresh_cmbCalculationRange_GroupFilter_ReportParameters_onClick() {
    document.getElementById('cmbCalculationRange_GroupFilter_ReportParameters_Input').value = '';
    cmbCalculationRange_GroupFilter_ReportParameters.unSelect();
}

function Refresh_cmbControlStation_GroupFilter_ReportParameters() {
    Fill_cmbControlStation_GroupFilter_ReportParameters();
}

function tlbItemClean_TlbRefresh_cmbControlStation_GroupFilter_ReportParameters_onClick() {
    document.getElementById('cmbControlStation_GroupFilter_ReportParameters_Input').value = '';
    cmbControlStation_GroupFilter_ReportParameters.unSelect();
}

function Refresh_cmbEmployType_GroupFilter_ReportParameters() {
    Fill_cmbEmployType_GroupFilter_ReportParameters();
}

function tlbItemClean_TlbRefresh_cmbEmployType_GroupFilter_ReportParameters_onClick() {
    document.getElementById('cmbEmployType_GroupFilter_ReportParameters_Input').value = '';
    cmbEmployType_GroupFilter_ReportParameters.unSelect();
}

function tlbItemEdit_TlbReportParameters_ReportParameters_onClick() {
    LoadCustomControls_ReportParameters();
}

function GridReportParameters_ReportParameters_onItemDoubleClick(sender, e) {
    if (TlbReportParameters_ReportParameters.get_items().getItemById('tlbItemEdit_TlbReportParameters_ReportParameters') != null)
        LoadCustomControls_ReportParameters();
}

function LoadCustomControls_ReportParameters() {
    var SelectedItems_GridReportParameters_ReportParameters = GridReportParameters_ReportParameters.getSelectedItems();
    if (SelectedItems_GridReportParameters_ReportParameters.length > 0) {
        var source = SelectedItems_GridReportParameters_ReportParameters[0].getMember('Key').get_text();
        var reportParameterID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ID').get_text();
        ShowReportParametersCustomControls_ReportParameters(source, reportParameterID);
    }
}

function tlbItemRefresh_TlbReportParameters_ReportParameters_onClick() {
    ShowReportParametersCustomControls_ReportParameters('WhitePage.aspx', CharToKeyCode_ReportParameters('-1'));
    Fill_GridReportParameters_ReportParameters();
}

function cmbPersonnel_PersonalFilter_ReportParameters_onExpand(sender, e) {
    CollapseControls_ReportParameters(cmbPersonnel_PersonalFilter_ReportParameters);
    if (cmbPersonnel_PersonalFilter_ReportParameters.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_PersonalFilter_ReportParameters == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_PersonalFilter_ReportParameters = true;
        SetPageIndex_cmbPersonnel_PersonalFilter_ReportParameters(0);
    }
}

function CallBack_cmbPersonnel_PersonalFilter_ReportParameters_onBeforeCallback(sender, e) {
    cmbPersonnel_PersonalFilter_ReportParameters.dispose();
}

function CallBack_cmbPersonnel_PersonalFilter_ReportParameters_onCallbackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_PersonalFilter_ReportParameters').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_PersonalFilter_ReportParameters').value;
    document.getElementById('clmnBarCode_cmbPersonnel_PersonalFilter_ReportParameters').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_PersonalFilter_ReportParameters').value;
    document.getElementById('clmnCardNum_cmbPersonnel_PersonalFilter_ReportParameters').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_PersonalFilter_ReportParameters').value;

    var error = document.getElementById('ErrorHiddenField_Personnel_PersonalFilter_ReportParameters').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_PersonalFilter_ReportParameters_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_PersonalFilter_ReportParameters_DropImage').mousedown();
        else
            cmbPersonnel_PersonalFilter_ReportParameters.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_PersonalFilter_ReportParameters_DropDown').style.display = 'none';
    }
}

function CallBack_cmbPersonnel_PersonalFilter_ReportParameters_onCallbackError(sender, e) {
    ShowConnectionError_ReportParameters();
}

function ShowConnectionError_ReportParameters() {
    var error = document.getElementById('hfErrorType_ReportParameters').value;
    var errorBody = document.getElementById('hfConnectionError_ReportParameters').value;
    showDialog(error, errorBody, 'error');
}

function trvOrganizationPost_PersonalFilter_ReportParameters_onNodeSelect(sender, e) {
    document.getElementById('cmbOrganizationPost_PersonalFilter_ReportParameters_TextBox').innerHTML = e.get_node().get_text();
    cmbOrganizationPost_PersonalFilter_ReportParameters.collapse();
}

function trvOrganizationPost_PersonalFilter_ReportParameters_onCallbackComplete(sender, e) {
    if (ObjexpandingOrgPostNode_ReportParameters != null) {
        if (ObjexpandingOrgPostNode_ReportParameters.Node.get_nodes().get_length() == 0 && ObjexpandingOrgPostNode_ReportParameters.HasChild) {
            ObjexpandingOrgPostNode_ReportParameters = null;
            GetLoadonDemandError_ReportParametersPage();
        }
        else
            ObjexpandingOrgPostNode_ReportParameters = null;
    }
}

function GetLoadonDemandError_ReportParametersPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function trvOrganizationPost_PersonalFilter_ReportParameters_onNodeBeforeExpand(sender, e) {
    if (ObjexpandingOrgPostNode_ReportParameters != null)
        ObjexpandingOrgPostNode_ReportParameters = null;
    ObjexpandingOrgPostNode_ReportParameters = new Object();
    ObjexpandingOrgPostNode_ReportParameters.Node = e.get_node();
    if (e.get_node().get_nodes().get_length() == 1 && (e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined || e.get_node().get_nodes().get_nodeArray()[0].get_id() == '')) {
        ObjexpandingOrgPostNode_ReportParameters.HasChild = true;
        trvOrganizationPost_PersonalFilter_ReportParameters.beginUpdate();
        ObjexpandingOrgPostNode_ReportParameters.Node.get_nodes().remove(0);
        trvOrganizationPost_PersonalFilter_ReportParameters.endUpdate();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            ObjexpandingOrgPostNode_ReportParameters.HasChild = false;
        else
            ObjexpandingOrgPostNode_ReportParameters.HasChild = true;
    }
}

function cmbOrganizationPost_PersonalFilter_ReportParameters_onExpand(sender, e) {
    if (trvOrganizationPost_PersonalFilter_ReportParameters.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbOrganizationPost_PersonalFilter_ReportParameters == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbOrganizationPost_PersonalFilter_ReportParameters = true;
        ObjexpandingOrgPostNode_ReportParameters = null;
        Fill_cmbOrganizationPost_PersonalFilter_ReportParameters();
    }
}

function Fill_cmbOrganizationPost_PersonalFilter_ReportParameters() {
    CallBack_cmbOrganizationPost_PersonalFilter_ReportParameters.callback();
}

function CallBack_cmbOrganizationPost_PersonalFilter_ReportParameters_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_OrganizationPost_PersonalFilter_ReportParameters').value;
    if (error == "") {
        document.getElementById('cmbOrganizationPost_PersonalFilter_ReportParameters_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbOrganizationPost_PersonalFilter_ReportParameters_DropImage').mousedown();
        cmbOrganizationPost_PersonalFilter_ReportParameters.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbOrganizationPost_PersonalFilter_ReportParameters_DropDown').style.display = 'none';
    }
}

function CallBack_cmbOrganizationPost_PersonalFilter_ReportParameters_onCallbackError(sender, e) {
    ShowConnectionError_ReportParameters();
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function tlbItemClean_TlbClean_cmbSex_GroupFilter_ReportParameters_onClick() {
    document.getElementById('cmbSex_GroupFilter_ReportParameters_Input').value = '';
    cmbSex_GroupFilter_ReportParameters.unSelect();
}

function cmbSex_GroupFilter_ReportParameters_onExpand(sender, e) {
    CollapseControls_ReportParameters(cmbSex_GroupFilter_ReportParameters);
    if (cmbSex_GroupFilter_ReportParameters.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSex_GroupFilter_ReportParameters == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSex_GroupFilter_ReportParameters = true;
        CallBack_cmbSex_GroupFilter_ReportParameters.callback();
    }
}

function CallBack_cmbSex_GroupFilter_ReportParameters_onBeforeCallback(sender, e) {
    cmbSex_GroupFilter_ReportParameters.dispose();
}

function CallBack_cmbSex_GroupFilter_ReportParameters_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Sex_GroupFilter_ReportParameters').value;
    if (error == "") {
        document.getElementById('cmbSex_GroupFilter_ReportParameters_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbSex_GroupFilter_ReportParameters_DropImage').mousedown();
        cmbSex_GroupFilter_ReportParameters.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbSex_GroupFilter_ReportParameters_DropDown').style.display = 'none';
    }
}

function CallBack_cmbSex_GroupFilter_ReportParameters_onCallbackError(sender, e) {
    ShowConnectionError_ReportParameters();
}

function tlbItemClean_TlbClean_cmbMilitaryState_GroupFilter_ReportParameters_onClick() {
    document.getElementById('cmbMilitaryState_GroupFilter_ReportParameters_Input').value = '';
    cmbMilitaryState_GroupFilter_ReportParameters.unSelect();
}

function cmbMilitaryState_GroupFilter_ReportParameters_onExpand(sender, e) {
    CollapseControls_ReportParameters(cmbMilitaryState_GroupFilter_ReportParameters);
    if (cmbMilitaryState_GroupFilter_ReportParameters.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMilitaryState_GroupFilter_ReportParameters == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMilitaryState_GroupFilter_ReportParameters = true;
        CallBack_cmbMilitaryState_GroupFilter_ReportParameters.callback();
    }
}

function CallBack_cmbMilitaryState_GroupFilter_ReportParameters_onBeforeCallback(sender, e) {
    cmbMilitaryState_GroupFilter_ReportParameters.dispose();
}

function CallBack_cmbMilitaryState_GroupFilter_ReportParameters_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MilitaryState_GroupFilter_ReportParameters').value;
    if (error == "") {
        document.getElementById('cmbMilitaryState_GroupFilter_ReportParameters_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMilitaryState_GroupFilter_ReportParameters_DropImage').mousedown();
        cmbMilitaryState_GroupFilter_ReportParameters.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbMilitaryState_GroupFilter_ReportParameters_DropDown').style.display = 'none';
    }
}

function CallBack_cmbMilitaryState_GroupFilter_ReportParameters_onCallbackError(sender, e) {
    ShowConnectionError_ReportParameters();
}

function tlbItemClean_TlbClean_cmbMarriageState_GroupFilter_ReportParameters_onClick() {
    document.getElementById('cmbMarriageState_GroupFilter_ReportParameters_Input').value = '';
    cmbMarriageState_GroupFilter_ReportParameters.unSelect();
}

function cmbMarriageState_GroupFilter_ReportParameters_onExpand(sender, e) {
    CollapseControls_ReportParameters(cmbMarriageState_GroupFilter_ReportParameters);
    if (cmbMarriageState_GroupFilter_ReportParameters.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMarriageState_GroupFilter_ReportParameters == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMarriageState_GroupFilter_ReportParameters = true;
        CallBack_cmbMarriageState_GroupFilter_ReportParameters.callback();
    }
}

function CallBack_cmbMarriageState_GroupFilter_ReportParameters_onBeforeCallback(sender, e) {
    cmbMarriageState_GroupFilter_ReportParameters.dispose();
}

function CallBack_cmbMarriageState_GroupFilter_ReportParameters_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MarriageState_GroupFilter_ReportParameters').value;
    if (error == "") {
        document.getElementById('cmbMarriageState_GroupFilter_ReportParameters_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMarriageState_GroupFilter_ReportParameters_DropImage').mousedown();
        cmbMarriageState_GroupFilter_ReportParameters.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbMarriageState_GroupFilter_ReportParameters_DropDown').style.display = 'none';
    }
}

function CallBack_cmbMarriageState_GroupFilter_ReportParameters_onCallbackError(sender, e) {
    ShowConnectionError_ReportParameters();
}

function trvDepartment_GroupFilter_ReportParameters_onNodeSelect(sender, e) {
    document.getElementById('cmbDepartment_GroupFilter_ReportParameters_TextBox').innerHTML = e.get_node().get_text();
    cmbDepartment_GroupFilter_ReportParameters.collapse();
}

function cmbDepartment_GroupFilter_ReportParameters_onExpand(sender, e) {
    if (trvDepartment_GroupFilter_ReportParameters.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDepartment_GroupFilter_ReportParameters == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDepartment_GroupFilter_ReportParameters = true;
        Fill_cmbDepartment_GroupFilter_ReportParameters();
    }
}

function Fill_cmbDepartment_GroupFilter_ReportParameters() {
    CallBack_cmbDepartment_GroupFilter_ReportParameters.callback();
}

function CallBack_cmbDepartment_GroupFilter_ReportParameters_onBeforeCallback(sender, e) {
    cmbDepartment_GroupFilter_ReportParameters.dispose();
}

function CallBack_cmbDepartment_GroupFilter_ReportParameters_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Department_GroupFilter_ReportParameters').value;
    if (error == "") {
        document.getElementById('cmbDepartment_GroupFilter_ReportParameters_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDepartment_GroupFilter_ReportParameters_DropImage').mousedown();
        cmbDepartment_GroupFilter_ReportParameters.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbDepartment_GroupFilter_ReportParameters_DropDown').style.display = 'none';
    }
}

function CallBack_cmbDepartment_GroupFilter_ReportParameters_onCallbackError(sender, e) {
    ShowConnectionError_ReportParameters();
}

function cmbWorkGroup_GroupFilter_ReportParameters_onExpand(sender, e) {
    CollapseControls_ReportParameters(cmbWorkGroup_GroupFilter_ReportParameters);
    if (cmbWorkGroup_GroupFilter_ReportParameters.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbWorkGroup_GroupFilter_ReportParameters == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbWorkGroup_GroupFilter_ReportParameters = true;
        Fill_cmbWorkGroup_GroupFilter_ReportParameters();
    }
}

function Fill_cmbWorkGroup_GroupFilter_ReportParameters() {
    CallBack_cmbWorkGroup_GroupFilter_ReportParameters.callback();
}

function CallBack_cmbWorkGroup_GroupFilter_ReportParameters_onBeforeCallback(sender, e) {
    cmbWorkGroup_GroupFilter_ReportParameters.dispose();
}

function CallBack_cmbWorkGroup_GroupFilter_ReportParameters_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkGroup_GroupFilter_ReportParameters').value;
    if (error == "") {
        document.getElementById('cmbDepartment_GroupFilter_ReportParameters_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbWorkGroup_GroupFilter_ReportParameters_DropImage').mousedown();
        cmbWorkGroup_GroupFilter_ReportParameters.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbWorkGroup_GroupFilter_ReportParameters_DropDown').style.display = 'none';
    }
}

function CallBack_cmbWorkGroup_GroupFilter_ReportParameters_onCallbackError(sender, e) {
    ShowConnectionError_ReportParameters();
}

function Refresh_cmbRuleGroup_GroupFilter_ReportParameters() {
    Fill_cmbRuleGroup_GroupFilter_ReportParameters();
}

function tlbItemClean_TlbRefresh_cmbRuleGroup_GroupFilter_ReportParameters_onClick() {
    document.getElementById('cmbRuleGroup_GroupFilter_ReportParameters_Input').value = '';
    cmbRuleGroup_GroupFilter_ReportParameters.unSelect();
}

function cmbRuleGroup_GroupFilter_ReportParameters_onExpand(sender, e) {
    CollapseControls_ReportParameters(cmbRuleGroup_GroupFilter_ReportParameters);
    if (cmbRuleGroup_GroupFilter_ReportParameters.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRuleGroup_GroupFilter_ReportParameters == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRuleGroup_GroupFilter_ReportParameters = true;
        Fill_cmbRuleGroup_GroupFilter_ReportParameters();
    }
}

function Fill_cmbRuleGroup_GroupFilter_ReportParameters() {
    CallBack_cmbRuleGroup_GroupFilter_ReportParameters.callback();
}

function CallBack_cmbRuleGroup_GroupFilter_ReportParameters_onBeforeCallback(sender, e) {
    cmbRuleGroup_GroupFilter_ReportParameters.dispose();
}

function CallBack_cmbRuleGroup_GroupFilter_ReportParameters_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RuleGroup_GroupFilter_ReportParameters').value;
    if (error == "") {
        document.getElementById('cmbRuleGroup_GroupFilter_ReportParameters_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbRuleGroup_GroupFilter_ReportParameters_DropImage').mousedown();
        cmbRuleGroup_GroupFilter_ReportParameters.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbRuleGroup_GroupFilter_ReportParameters_DropDown').style.display = 'none';
    }
}

function CallBack_cmbRuleGroup_GroupFilter_ReportParameters_onCallbackError(sender, e) {
    ShowConnectionError_ReportParameters();
}

function cmbCalculationRange_GroupFilter_ReportParameters_onExpand(sender, e) {
    CollapseControls_ReportParameters(cmbCalculationRange_GroupFilter_ReportParameters);
    if (cmbCalculationRange_GroupFilter_ReportParameters.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbCalculationRange_GroupFilter_ReportParameters == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbCalculationRange_GroupFilter_ReportParameters = true;
        Fill_cmbCalculationRange_GroupFilter_ReportParameters();
    }
}

function Fill_cmbCalculationRange_GroupFilter_ReportParameters() {
    CallBack_cmbCalculationRange_GroupFilter_ReportParameters.callback();
}

function CallBack_cmbCalculationRange_GroupFilter_ReportParameters_onBeforeCallback(sender, e) {
    cmbCalculationRange_GroupFilter_ReportParameters.dispose();
}

function CallBack_cmbCalculationRange_GroupFilter_ReportParameters_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_CalculationRange_GroupFilter_ReportParameters').value;
    if (error == "") {
        document.getElementById('cmbCalculationRange_GroupFilter_ReportParameters_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbCalculationRange_GroupFilter_ReportParameters_DropImage').mousedown();
        cmbCalculationRange_GroupFilter_ReportParameters.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbCalculationRange_GroupFilter_ReportParameters_DropDown').style.display = 'none';
    }
}

function cmbControlStation_GroupFilter_ReportParameters_onExpand(sender, e) {
    CollapseControls_ReportParameters(cmbControlStation_GroupFilter_ReportParameters);
    if (cmbControlStation_GroupFilter_ReportParameters.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbControlStation_GroupFilter_ReportParameters == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbControlStation_GroupFilter_ReportParameters = true;
        Fill_cmbControlStation_GroupFilter_ReportParameters();
    }
}

function Fill_cmbControlStation_GroupFilter_ReportParameters() {
    CallBack_cmbControlStation_GroupFilter_ReportParameters.callback();
}

function CallBack_cmbControlStation_GroupFilter_ReportParameters_onBeforeCallback(sender, e) {
    cmbControlStation_GroupFilter_ReportParameters.dispose();
}

function CallBack_cmbControlStation_GroupFilter_ReportParameters_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_CalculationRange_GroupFilter_ReportParameters').value;
    if (error == "") {
        document.getElementById('cmbControlStation_GroupFilter_ReportParameters_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbControlStation_GroupFilter_ReportParameters_DropImage').mousedown();
        cmbControlStation_GroupFilter_ReportParameters.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbControlStation_GroupFilter_ReportParameters_DropDown').style.display = 'none';
    }
}

function CallBack_cmbControlStation_GroupFilter_ReportParameters_onCallbackError() {
    ShowConnectionError_ReportParameters();
}

function cmbEmployType_GroupFilter_ReportParameters_onExpand(sender, e) {
    CollapseControls_ReportParameters(cmbEmployType_GroupFilter_ReportParameters);
    if (cmbEmployType_GroupFilter_ReportParameters.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbEmployType_GroupFilter_ReportParameters == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbEmployType_GroupFilter_ReportParameters = true;
        Fill_cmbEmployType_GroupFilter_ReportParameters();
    }
}

function Fill_cmbEmployType_GroupFilter_ReportParameters() {
    CallBack_cmbEmployType_GroupFilter_ReportParameters.callback();
}

function CallBack_cmbEmployType_GroupFilter_ReportParameters_onBeforeCallback(sender, e) {
    cmbEmployType_GroupFilter_ReportParameters.dispose();
}

function CallBack_cmbEmployType_GroupFilter_ReportParameters_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_CalculationRange_GroupFilter_ReportParameters').value;
    if (error == "") {
        document.getElementById('cmbEmployType_GroupFilter_ReportParameters_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbEmployType_GroupFilter_ReportParameters_DropImage').mousedown();
        cmbEmployType_GroupFilter_ReportParameters.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbEmployType_GroupFilter_ReportParameters_DropDown').style.display = 'none';
    }
}

function CallBack_cmbEmployType_GroupFilter_ReportParameters_onCallbackError(sender, e) {
    ShowConnectionError_ReportParameters();
}

function Fill_GridReportParameters_ReportParameters() {
    document.getElementById('loadingPanel_GridReportParameters_ReportParameters').innerHTML = document.getElementById('hfloadingPanel_GridReportParameters_ReportParameters').value;
    var reportObj = parent.DialogReportParameters.get_value();
    CallBack_GridReportParameters_ReportParameters.callback(CharToKeyCode_ReportParameters(reportObj.ReportFileID));
}

function GridReportParameters_ReportParameters_onLoad() {
    document.getElementById('loadingPanel_GridReportParameters_ReportParameters').innerHTML = '';
}

function CallBack_GridReportParameters_ReportParameters_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErroHiddenField_ReportParameters_ReportParameters').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridReportParameters_ReportParameters();
    }
}

function CallBack_GridReportParameters_ReportParameters_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridReportParameters_ReportParameters').innerHTML = '';
    ShowConnectionError_ReportParameters();
}

function ShowReportParametersCustomControls_ReportParameters(source, reportParameterID) {
    document.getElementById('IFrameReportParameters_ReportParameters').src = source + '?ReportParameterID=' + CharToKeyCode_ReportParameters(reportParameterID) + '&dt=' + (new Date()).getTime();
}

function TabStripReportParameters_onTabSelect(sender, e) {
    CollapseControls_ReportParameters();
}

function GetReport_ReportParameters(personnelFilterType) {
    ObjReportParameters_ReportParameters = new Object();
    var ObjReport_ReportParameters = parent.DialogReportParameters.get_value();
    ObjReportParameters_ReportParameters.ReportFileID = ObjReport_ReportParameters.ReportFileID;
    ObjReportParameters_ReportParameters.PersonnelFilterType = personnelFilterType;
    ObjReportParameters_ReportParameters.PersonnelFilterObj = '';
    ObjReportParameters_ReportParameters.ReportParametersList = '';
    if (personnelFilterType == 'Personal') {
        var personnelID = '0';
        if (cmbPersonnel_PersonalFilter_ReportParameters.getSelectedItem() != undefined) {
            var personnelObj = cmbPersonnel_PersonalFilter_ReportParameters.getSelectedItem().get_value();
            personnelObj = eval('(' + personnelObj + ')');
            personnelID = personnelObj.ID;
        }
        ObjReportParameters_ReportParameters.PersonnelFilterObj = '{"PersonnelID":"' + personnelID + '"}';
    }
    else
        if (personnelFilterType == 'Group' || personnelFilterType == 'SelectInGroup') {
            var active = '';
            var sexID = '-1';
            var education = '';
            var militaryStateID = '-1';
            var marriageStateID = '-1';
            var departmentID = '0';
            var subDepartment = 'false';
            var workGroupID = '0';
            var workGroupFromDate = '';
            var ruleGroupID = '0';
            var ruleGroupFromDate = '';
            var ruleGroupToDate = '';
            var calculationRangeID = '0';
            var calculationRangeFromDate = '';
            var controlStationID = '0';
            var employTypeID = '0';
            if (document.getElementById('rdbAllPersonnel_GroupFilter_ReportParameters').checked)
                active = '';
            else
                if (document.getElementById('rdbActive_GroupFilter_ReportParameters').checked)
                    active = true;
                else
                    if (document.getElementById('rdbDeactive_GroupFilter_ReportParameters').checked)
                        active = false;
            if (cmbSex_GroupFilter_ReportParameters.getSelectedItem() != undefined)
                sexID = cmbSex_GroupFilter_ReportParameters.getSelectedItem().get_id();
            if (document.getElementById('txtEducation_GroupFilter_ReportParameters').value != '' && document.getElementById('txtEducation_GroupFilter_ReportParameters').value != undefined)
                education = document.getElementById('txtEducation_GroupFilter_ReportParameters').value;
            if (cmbMilitaryState_GroupFilter_ReportParameters.getSelectedItem() != undefined)
                militaryStateID = cmbMilitaryState_GroupFilter_ReportParameters.getSelectedItem().get_id();
            if (cmbMarriageState_GroupFilter_ReportParameters.getSelectedItem() != undefined)
                marriageStateID = cmbMarriageState_GroupFilter_ReportParameters.getSelectedItem().get_id();
            if (trvDepartment_GroupFilter_ReportParameters.get_selectedNode() != undefined)
                departmentID = trvDepartment_GroupFilter_ReportParameters.get_selectedNode().get_id();
            subDepartment = document.getElementById('chbSubDepartment_GroupFilter_ReportParameters').checked;
            if (cmbWorkGroup_GroupFilter_ReportParameters.getSelectedItem() != undefined)
                workGroupID = cmbWorkGroup_GroupFilter_ReportParameters.getSelectedItem().get_value();
            if (cmbRuleGroup_GroupFilter_ReportParameters.getSelectedItem() != undefined)
                ruleGroupID = cmbRuleGroup_GroupFilter_ReportParameters.getSelectedItem().get_value();
            if (cmbCalculationRange_GroupFilter_ReportParameters.getSelectedItem() != undefined)
                calculationRangeID = cmbCalculationRange_GroupFilter_ReportParameters.getSelectedItem().get_value();
            if (cmbControlStation_GroupFilter_ReportParameters.getSelectedItem() != undefined)
                controlStationID = cmbControlStation_GroupFilter_ReportParameters.getSelectedItem().get_value();
            if (cmbEmployType_GroupFilter_ReportParameters.getSelectedItem() != undefined)
                employTypeID = cmbEmployType_GroupFilter_ReportParameters.getSelectedItem().get_value();
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    workGroupFromDate = document.getElementById('pdpWorkGroup_GroupFilter_ReportParameters').value;
                    ruleGroupFromDate = document.getElementById('pdpFromDate_RuleGroup_GroupFilter_ReportParameters').value;
                    ruleGroupToDate = document.getElementById('pdpToDate_RuleGroup_GroupFilter_ReportParameters').value;
                    calculationRangeFromDate = document.getElementById('pdpRunFromDate_GroupFilter_ReportParameters').value;
                    break;
                case 'en-US':
                    workGroupFromDate = document.getElementById('gdpWorkGroup_GroupFilter_ReportParameters_picker').value;
                    ruleGroupFromDate = document.getElementById('gdpFromDate_RuleGroup_GroupFilter_ReportParameters_picker').value;
                    ruleGroupToDate = document.getElementById('gdpToDate_RuleGroup_GroupFilter_ReportParameters_picker').value;
                    calculationRangeFromDate = document.getElementById('gdpRunFromDate_GroupFilter_ReportParameters_picker').value;
                    break;
            }
            ObjReportParameters_ReportParameters.PersonnelFilterObj = '{"Active":"' + active + '","Sex":"' + sexID + '","Education":"' + education + '","MilitaryState":"' + militaryStateID + '","MarriageState":"' + marriageStateID + '","DepartmentID":"' + departmentID + '","IsContainsSubDepartment":"' + subDepartment + '","WorkGroupID":"' + workGroupID + '","WorkGroupFromDate":"' + workGroupFromDate + '","RuleGroupID":"' + ruleGroupID + '","RuleGroupFromDate":"' + ruleGroupFromDate + '","RuleGroupToDate":"' + ruleGroupToDate + '","CalculationRangeID":"' + calculationRangeID + '","CalculationRangeFromDate":"' + calculationRangeFromDate + '","ControlStationID":"' + controlStationID + '","EmployTypeID":"' + employTypeID + '"}';
        }
    ObjReportParameters_ReportParameters.ReportParametersList = CreateReportsParametersList_ReportParameters();
    if (personnelFilterType == 'Personal' || personnelFilterType == 'Group') {
        GetReport_ReportParametersPage(CharToKeyCode_ReportParameters(ObjReportParameters_ReportParameters.ReportFileID), CharToKeyCode_ReportParameters(ObjReportParameters_ReportParameters.PersonnelFilterType), CharToKeyCode_ReportParameters(ObjReportParameters_ReportParameters.PersonnelFilterObj), CharToKeyCode_ReportParameters(ObjReportParameters_ReportParameters.ReportParametersList));
        DialogWaiting.Show();
    }
    else
        if (personnelFilterType == 'SelectInGroup') {
            if (CachedPersonnelFilterObj != '' && CachedPersonnelFilterObj != ObjReportParameters_ReportParameters.PersonnelFilterObj)
                StrSelectivePersonnelList_PersonnelSelect = '';
            CachedPersonnelFilterObj = ObjReportParameters_ReportParameters.PersonnelFilterObj;
            ShowDialogPersonnelSelect(ObjReportParameters_ReportParameters);
        }
}

function GetReport_ReportParametersPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_ReportParameters').value;
            Response[1] = document.getElementById('hfConnectionError_ReportParameters').value;
        }
        if (RetMessage[2] == 'success')
            ShowReport_ReportParameters(Response);
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function ShowReport_ReportParameters(Response) {
    var stiReportGUID = Response[3];
    var reportName = parent.DialogReportParameters.get_value().ReportName;
    var NewReportWindow = window.open("ReportViewer.aspx?ReportGUID=" + stiReportGUID + "&ReportTitle=" + CharToKeyCode_ReportParameters(reportName) + "", "ReportViewer" + (new Date()).getTime() + "", "width=" + screen.width + ",height=" + screen.height + ",toolbar=yes,location=yes,directories=yes,status=yes,menubar=yes,scrollbars=yes,copyhistory=yes,resizable=yes");
}

function CreateReportsParametersList_ReportParameters() {
    var ObjReportFeatures_ReportParameters = '';
    var splitter = ',';
    for (var i = 0; i < GridReportParameters_ReportParameters.get_recordCount() ; i++) {
        if (i == GridReportParameters_ReportParameters.get_recordCount() - 1)
            splitter = '';
        var gridItem_GridReportParameters_ReportParameters = GridReportParameters_ReportParameters.get_table().getRow(i);
        var reportParameterID = gridItem_GridReportParameters_ReportParameters.getMember('ID').get_text();
        var reportParameterValue = gridItem_GridReportParameters_ReportParameters.getMember('Value').get_text();
        var reportActionID = gridItem_GridReportParameters_ReportParameters.getMember('ActionId').get_text();
        ObjReportFeatures_ReportParameters += '{"ID":"' + reportParameterID + '","Value":"' + reportParameterValue + '","ActionID":"' + reportActionID + '"}' + splitter;
    }
    ObjReportFeatures_ReportParameters = '[' + ObjReportFeatures_ReportParameters + ']';
    return ObjReportFeatures_ReportParameters;
}

function ReportParametres_OnAfterUpdate(Response, reportParameterID) {
    var ReportParameterValue = Response[3];
    GridReportParameters_ReportParameters.beginUpdate();
    ReportParameterItem = GridReportParameters_ReportParameters.getItemFromKey(0, reportParameterID);
    ReportParameterItem.setValue(2, ReportParameterValue, false);
    GridReportParameters_ReportParameters.endUpdate();
}

function CallBack_cmbCalculationRange_GroupFilter_ReportParameters_onCallbackError(sender, e) {
    ShowConnectionError_ReportParameters();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    var caller = DialogConfirm.get_value();
    switch (caller) {
        case 'ReportParameters':
            ReportParameters_onClose();
            break;
        case 'PersonnelSelect':
            CloseDialogPersonnelSelect();
            break;
    }
    DialogConfirm.Close();
}

function ShowDialogConfirm(caller) {
    switch (caller) {
        case 'ReportParameters':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_ReportParameters').value;
            break;
        case 'PersonnelSelect':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PersonnelSelect').value;
            break;
    }
    DialogConfirm.set_value(caller);
    DialogConfirm.Show();
    CollapseControls_ReportParameters();
}

function ReportParameters_onClose() {
    parent.document.getElementById("DialogReportParameters_IFrame").src = 'WhitePage.aspx';
    parent.DialogReportParameters.Close();
}


function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function tlbItemReportView_TlbReportParameters_ReportParameters_onClick() {
    var selectedTabID_ReportParameters = TabStripReportParameters.getSelectedTab().get_id();
    switch (selectedTabID_ReportParameters) {
        case 'tbPersoanlFilter_TabStripReportParameters':
            if (TlbPersonalFilter_PersonalFilter_ReportParameters.get_items().getItemById('tlbItemReportView_TlbPersoanlFilter_PersonalFilter_ReportParameters') != null)
                GetReport_ReportParameters('Personal');
            break;
        case 'tbGroupFilter_TabStripReportParameters':
            if (TlbPersonalFilter_GroupFilter_ReportParameters.get_items().getItemById('tlbItemReportView_TlbPersonalFilter_GroupFilter_ReportParameters') != null)
                if (document.getElementById('chbAllPersonWithThisConditions_ReportParameters').checked)
                    GetReport_ReportParameters('Group');
                else
                    GetReport_ReportParameters('SelectInGroup');
            break;
    }
}

function CollapseControls_ReportParameters(cmbException) {
    if (cmbException == null || cmbException != cmbPersonnel_PersonalFilter_ReportParameters)
        cmbPersonnel_PersonalFilter_ReportParameters.collapse();
    if (cmbException == null || cmbException != cmbOrganizationPost_PersonalFilter_ReportParameters)
        cmbOrganizationPost_PersonalFilter_ReportParameters.collapse();
    if (cmbException == null || cmbException != cmbSex_GroupFilter_ReportParameters)
        cmbSex_GroupFilter_ReportParameters.collapse();
    if (cmbException == null || cmbException != cmbMilitaryState_GroupFilter_ReportParameters)
        cmbMilitaryState_GroupFilter_ReportParameters.collapse();
    if (cmbException == null || cmbException != cmbMarriageState_GroupFilter_ReportParameters)
        cmbMarriageState_GroupFilter_ReportParameters.collapse();
    if (cmbException == null || cmbException != cmbDepartment_GroupFilter_ReportParameters)
        cmbDepartment_GroupFilter_ReportParameters.collapse();
    if (cmbException == null || cmbException != cmbWorkGroup_GroupFilter_ReportParameters)
        cmbWorkGroup_GroupFilter_ReportParameters.collapse();
    if (cmbException == null || cmbException != cmbRuleGroup_GroupFilter_ReportParameters)
        cmbRuleGroup_GroupFilter_ReportParameters.collapse();
    if (cmbException == null || cmbException != cmbCalculationRange_GroupFilter_ReportParameters)
        cmbCalculationRange_GroupFilter_ReportParameters.collapse();
    if (cmbException == null || cmbException != cmbControlStation_GroupFilter_ReportParameters)
        cmbControlStation_GroupFilter_ReportParameters.collapse();
    if (cmbException == null || cmbException != cmbEmployType_GroupFilter_ReportParameters)
        cmbEmployType_GroupFilter_ReportParameters.collapse();
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpWorkGroup_GroupFilter_ReportParameters');
}

function tlbItemFormReconstruction_TlbPersoanlFilter_PersonalFilter_ReportParameters_onClick() {
    ReconstructForm_ReportParameters();
}

function tlbItemFormReconstruction_TlbPersonalFilter_GroupFilter_ReportParameters_onClick() {
    ReconstructForm_ReportParameters();
}

function ReconstructForm_ReportParameters() {
    ReportParameters_onClose();
    parent.document.getElementById('pgvReportsIntroduction_iFrame').contentWindow.ShowDialogReportParameters_Reports();
}

function tlbItemHelp_TlbPersoanlFilter_PersonalFilter_ReportParameters_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPersoanlFilter_PersonalFilter_ReportParameters');
}

function tlbItemHelp_TlbPersonalFilter_GroupFilter_ReportParameters_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPersonalFilter_GroupFilter_ReportParameters');
}

function ShowDialogPersonnelSelect(ObjReportParameters) {
    var ObjDialogPersonnelSelect = new Object();
    SetPersonnelCount_GridPersonnel_PersonnelSelect('All');
    DialogPersonnelSelect.set_value(ObjReportParameters);
    DialogPersonnelSelect.Show();
    CollapseControls_ReportParameters();
}

function CloseDialogPersonnelSelect() {
    GridPersonnel_PersonnelSelect.beginUpdate();
    GridPersonnel_PersonnelSelect.get_table().clearData();
    GridPersonnel_PersonnelSelect.endUpdate();
    PersonnelCount_PersonnelSelect = 0;
    document.getElementById('PersonnelCount_GridPersonnel_PersonnelSelect').innerHTML = document.getElementById('hfPersonnelCountTitle_GridPersonnel_PersonnelSelect').value + PersonnelCount_PersonnelSelect;
    document.getElementById('footer_GridPersonnel_PersonnelSelect').innerHTML = document.getElementById('hffooter_GridPersonnel_PersonnelSelect').value;
    DialogPersonnelSelect.Close();
}

function tlbItemSave_TlbPersonnelSelect_onClick() {
    var ObjDialogPersonnelSelect = DialogPersonnelSelect.get_value();
    var PersonnelIDList = '{"PersonnelIDList":"' + StrSelectivePersonnelList_PersonnelSelect + '"}';
    CloseDialogPersonnelSelect();
    GetReport_ReportParametersPage(CharToKeyCode_ReportParameters(ObjDialogPersonnelSelect.ReportFileID), CharToKeyCode_ReportParameters(ObjDialogPersonnelSelect.PersonnelFilterType), CharToKeyCode_ReportParameters(PersonnelIDList), CharToKeyCode_ReportParameters(ObjDialogPersonnelSelect.ReportParametersList));
    DialogWaiting.Show();
}

function tlbItemExit_TlbPersonnelSelect_onClick() {
    ShowDialogConfirm('PersonnelSelect');
}

function GridPersonnel_PersonnelSelect_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridPersonnel_PersonnelSelect').innerHTML = '';
}

function GridPersonnel_PersonnelSelect_onItemCheckChange(sender, e) {
    ChangeStrList_PersonnelSelect(e.get_item(), 'ID', StrSelectivePersonnelList_PersonnelSelect);
}

function ChangeStrList_PersonnelSelect(gridItem, identifierName, StrList) {
    var checked = gridItem.getMember('Select').get_value() ? true : false;
    var CountState_PersonnelSelect = null;
    var separator = '#';
    var identifier = gridItem.getMember(identifierName).get_text() + separator;
    if (StrList == '')
        StrList = separator;
    if (checked) {
        if (StrList.indexOf(identifier) >= 0) {
            StrList = StrSelectivePersonnelList_PersonnelSelect.replace(separator + identifier, separator);
            CountState_PersonnelSelect = 'Decrease';
        }
    }
    else {
        if (StrList.indexOf(identifier) < 0) {
            StrList += identifier;
            CountState_PersonnelSelect = 'Increase';
        }
    }
    StrSelectivePersonnelList_PersonnelSelect = StrList;
    SetPersonnelCount_GridPersonnel_PersonnelSelect(CountState_PersonnelSelect);
}

function SetPersonnelCount_GridPersonnel_PersonnelSelect(state) {
    switch (state) {
        case 'All':
            var SelectivePersonnelCount = 0;
            if (StrSelectivePersonnelList_PersonnelSelect != '')
                SelectivePersonnelCount = StrSelectivePersonnelList_PersonnelSelect.split('#').length - 2;
            PersonnelCount_PersonnelSelect += SelectivePersonnelCount;
            break;
        case 'None':
            PersonnelCount_PersonnelSelect = 0;
            break;
        case 'Increase':
            PersonnelCount_PersonnelSelect = PersonnelCount_PersonnelSelect + 1;
            break;
        case 'Decrease':
            PersonnelCount_PersonnelSelect = PersonnelCount_PersonnelSelect - 1;
            break;
    }
    document.getElementById('PersonnelCount_GridPersonnel_PersonnelSelect').innerHTML = document.getElementById('hfPersonnelCountTitle_GridPersonnel_PersonnelSelect').value + PersonnelCount_PersonnelSelect;
}

function CallBack_GridPersonnel_PersonnelSelect_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Personnel_PersonnelSelect').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridPersonnel_PersonnelSelect(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2], false, document.getElementById('Mastertbl_RegisteredRequestsForm').offsetWidth);
    }
    else {
        document.getElementById('chbAllInThisPage_PersonnelSelect').checked = false;
        Changefooter_GridPersonnel_PersonnelSelect();
        UpdateGridPersonnel_PersonnelSelect();
    }
}

function Changefooter_GridPersonnel_PersonnelSelect() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridPersonnel_PersonnelSelect').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfPersonnelPageCount_Personnel_PersonnelSelect').value) > 0 ? CurrentPageIndex_GridPersonnel_PersonnelSelect + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfPersonnelPageCount_Personnel_PersonnelSelect').value;
        if ((i == 1 || i == 3) && GridPersonnel_PersonnelSelect.get_table().getRowCount() == 0)
            footerValCol[i] = 0;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridPersonnel_PersonnelSelect').innerHTML = retfooterVal;
}

function UpdateGridPersonnel_PersonnelSelect() {
    var separator = '#';
    if (StrSelectivePersonnelList_PersonnelSelect != null) {
        for (var i = 0; i < GridPersonnel_PersonnelSelect.get_table().getRowCount() ; i++) {
            personnelItem = GridPersonnel_PersonnelSelect.get_table().getRow(i);
            var personnelID = personnelItem.getMember('ID').get_text();
            GridPersonnel_PersonnelSelect.beginUpdate();
            if (StrSelectivePersonnelList_PersonnelSelect.indexOf(separator + personnelID + separator) >= 0)
                personnelItem.setValue(1, true, false);
            else
                personnelItem.setValue(1, false, false);
            GridPersonnel_PersonnelSelect.endUpdate();
        }
    }
}

function CallBack_GridPersonnel_PersonnelSelect_onCallbackError(sender, e) {
    ShowConnectionError_ReportParameters();
}

function tlbItemRefresh_TlbPaging_GridPersonnel_PersonnelSelect_onClick() {
    SetPageIndex_GridPersonnel_PersonnelSelect(0);
}

function tlbItemFirst_TlbPaging_GridPersonnel_PersonnelSelect_onClick() {
    SetPageIndex_GridPersonnel_PersonnelSelect(0);
}

function tlbItemBefore_TlbPaging_GridPersonnel_PersonnelSelect_onClick() {
    if (CurrentPageIndex_GridPersonnel_PersonnelSelect != 0) {
        CurrentPageIndex_GridPersonnel_PersonnelSelect = CurrentPageIndex_GridPersonnel_PersonnelSelect - 1;
        SetPageIndex_GridPersonnel_PersonnelSelect(CurrentPageIndex_GridPersonnel_PersonnelSelect);
    }
}

function tlbItemNext_TlbPaging_GridPersonnel_PersonnelSelect_onClick() {
    if (CurrentPageIndex_GridPersonnel_PersonnelSelect < parseInt(document.getElementById('hfPersonnelPageCount_Personnel_PersonnelSelect').value) - 1) {
        CurrentPageIndex_GridPersonnel_PersonnelSelect = CurrentPageIndex_GridPersonnel_PersonnelSelect + 1;
        SetPageIndex_GridPersonnel_PersonnelSelect(CurrentPageIndex_GridPersonnel_PersonnelSelect);
    }
}

function tlbItemLast_TlbPaging_GridPersonnel_PersonnelSelect_onClick() {
    SetPageIndex_GridPersonnel_PersonnelSelect(parseInt(document.getElementById('hfPersonnelPageCount_Personnel_PersonnelSelect').value) - 1);
}

function SetPageIndex_GridPersonnel_PersonnelSelect(pageIndex) {
    CurrentPageIndex_GridPersonnel_PersonnelSelect = pageIndex;
    Fill_GridPersonnel_PersonnelSelect(pageIndex);
}

function Fill_GridPersonnel_PersonnelSelect(pageIndex) {
    document.getElementById('loadingPanel_GridPersonnel_PersonnelSelect').innerHTML = document.getElementById('hfloadingPanel_GridPersonnel_PersonnelSelect').value;
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_Personnel_PersonnelSelect').value);
    var ObjPersonnelSelect = DialogPersonnelSelect.get_value();
    var PersonnelFilterObj = ObjPersonnelSelect.PersonnelFilterObj;
    CallBack_GridPersonnel_PersonnelSelect.callback(CharToKeyCode_ReportParameters(pageSize.toString()), CharToKeyCode_ReportParameters(pageIndex.toString()), CharToKeyCode_ReportParameters(PersonnelFilterObj));
}

function DialogPersonnelSelect_OnShow() {
    Init_DialogPersonnelSelect();
    SetPageIndex_GridPersonnel_PersonnelSelect(0);
}

function Init_DialogPersonnelSelect() {
    document.getElementById('Title_DialogPersonnelSelect').innerHTML = document.getElementById('hfTitle_DialogPersonnelSelect').value;
    document.getElementById('header_Personnel_PersonnelSelect').innerHTML = document.getElementById('hfheader_Personnel_PersonnelSelect').value;
    document.getElementById('footer_GridPersonnel_PersonnelSelect').innerHTML = document.getElementById('hffooter_GridPersonnel_PersonnelSelect').value;
    var CurrentLangID = parent.parent.CurrentLangID;
    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogPersonnelSelect_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogPersonnelSelect_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogPersonnelSelect_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogPersonnelSelect_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogPersonnelSelect').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogPersonnelSelect').align = 'right';
}

function chbAllInThisPage_PersonnelSelect_onClick() {
    var separator = '#';
    if (StrSelectivePersonnelList_PersonnelSelect == '')
        StrSelectivePersonnelList_PersonnelSelect = separator;
    for (var i = 0; i < GridPersonnel_PersonnelSelect.get_table().getRowCount() ; i++) {
        personnelItem = GridPersonnel_PersonnelSelect.get_table().getRow(i);
        var personnelID = personnelItem.getMember('ID').get_text();
        GridPersonnel_PersonnelSelect.beginUpdate();
        if (document.getElementById('chbAllInThisPage_PersonnelSelect').checked) {
            if (StrSelectivePersonnelList_PersonnelSelect.indexOf(separator + personnelID + separator) < 0) {
                StrSelectivePersonnelList_PersonnelSelect += personnelID + separator;
                personnelItem.setValue(1, true, false);
                SetPersonnelCount_GridPersonnel_PersonnelSelect('Increase');
            }
        }
        else {
            if (StrSelectivePersonnelList_PersonnelSelect.indexOf(separator + personnelID + separator) >= 0) {
                StrSelectivePersonnelList_PersonnelSelect = StrSelectivePersonnelList_PersonnelSelect.replace(separator + personnelID + separator, separator);
                SetPersonnelCount_GridPersonnel_PersonnelSelect('Decrease');
            }
            personnelItem.setValue(1, false, false);
        }
        GridPersonnel_PersonnelSelect.endUpdate();
    }
}






