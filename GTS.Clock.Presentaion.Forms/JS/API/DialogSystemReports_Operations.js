
var ConfirmState_SystemReports = null;
var CurrentPageState_SystemReports = 'View';
var CurrentPageCombosCallBcakStateObj = new Object();
var CurrentPageIndex_GridSystemReportType_SystemReports = 0;
var ObjCurrentSystemReportFilterConditions_SystemReports = null;
var Basefooter_GridSystemReportType_SystemReports = null;

function GetBoxesHeaders_SystemReports() {
    parent.document.getElementById('Title_DialogSystemReports').innerHTML = document.getElementById('hfTitle_DialogSystemReports').value;
    document.getElementById('header_GridSystemReportType_SystemReports').innerHTML = document.getElementById('hfheader_GridSystemReportType_SystemReports').value;
    document.getElementById('header_SystemReportTypeFeature_SystemReports').innerHTML = document.getElementById('hfheader_SystemReportTypeFeature_SystemReports').value;
    Basefooter_GridSystemReportType_SystemReports = document.getElementById('footer_GridSystemReportType_SystemReports').innerHTML = document.getElementById('hffooter_GridSystemReportType_SystemReports').value;
}

function tlbItemDeleteAll_TlbSystemReports_onClick() {
    CurrentPageState_SystemReports = 'DeleteAll';
    ShowDialogConfirm('DeleteAll');
}

function UpdateSystemReport_SystemReports() {
    var SelectedItem_cmbSystemReportType_SystemReports = cmbSystemReportType_SystemReports.getSelectedItem();
    if (SelectedItem_cmbSystemReportType_SystemReports != null && SelectedItem_cmbSystemReportType_SystemReports != undefined) {
        var SelectedSystemReportType = SelectedItem_cmbSystemReportType_SystemReports.get_value();
        UpdateSystemReport_SystemReportsPage(CharToKeyCode_SystemReports(CurrentPageState_SystemReports), CharToKeyCode_SystemReports(SelectedSystemReportType));
        DialogWaiting.Show();
    }
}

function GetCurrentSystemReportType_SystemReports() {
    var ObjCurrentSystemReportType = eval('(' + document.getElementById('hfCurrentSystemReportType_SystemReports').value + ')');
    return ObjCurrentSystemReportType;
}

function UpdateSystemReport_SystemReportsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (RetMessage[2] == 'success') {
            if (CurrentPageState_SystemReports == 'DeleteAll') {
                DeleteAllItems_GridSystemReportType_SystemReports();
                document.getElementById('footer_GridSystemReportType_SystemReports').innerHTML = Basefooter_GridSystemReportType_SystemReports;
                CurrentPageState_SystemReports = 'View';
            }
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function DeleteAllItems_GridSystemReportType_SystemReports() {
    var GridSystemReportType = GetGridSystemReportType_SystemReports();
    GridSystemReportType.beginUpdate();
    GridSystemReportType.get_table().clearData();
    GridSystemReportType.endUpdate();
}

function GetGridSystemReportType_SystemReports() {
    var GridSystemReportType = null;
    GridSystemReportType = 'Grid' + GetCurrentSystemReportType_SystemReports().Value + '_SystemReports';
    GridSystemReportType = eval(GridSystemReportType);
    return GridSystemReportType;
}

function tlbItemHelp_TlbSystemReports_onClick() {
    LoadHelpPage('tlbItemHelp_TlbSystemReports');
}

function tlbItemFormReconstruction_TlbSystemReports_onClick() {
    parent.DialogSystemReports.Close();
    parent.DialogSystemReports.Show();
}

function tlbItemExit_TlbSystemReports_onClick() {
    ShowDialogConfirm('Exit');
}

function ChangeCurrentSystemReportType_SystemReports() {
    if (cmbSystemReportType_SystemReports.getSelectedItem() != undefined && cmbSystemReportType_SystemReports.getSelectedItem() != null)
    {
        var ObjCurrentSystemReportType = '{"Text":"' + cmbSystemReportType_SystemReports.getSelectedItem().get_text() + '","Value":"' + cmbSystemReportType_SystemReports.getSelectedItem().get_value() + '"}';
        document.getElementById('hfCurrentSystemReportType_SystemReports').value = ObjCurrentSystemReportType;
    }
}

function ResetSystemReportTypeFeature_SystemReports() {
    document.getElementById('header_SystemReportTypeFeature_SystemReports').innerHTML = document.getElementById('hfheader_SystemReportTypeFeature_SystemReports').value;
    document.getElementById('txtSystemReportTypeFeature_SystemReports').value = '';
}

function gdpFromDate_SystemReports_OnDateChange(sender, eventArgs) {
    var FromDate = gdpFromDate_SystemReports.getSelectedDate();
    gCalFromDate_SystemReports.setSelectedDate(FromDate);
}

function gCalFromDate_SystemReports_OnChange(sender, eventArgs) {
    var FromDate = gCalFromDate_SystemReports.getSelectedDate();
    gdpFromDate_SystemReports.setSelectedDate(FromDate);
}

function btn_gdpFromDate_SystemReports_OnClick(event) {
    if (gCalFromDate_SystemReports.get_popUpShowing()) {
        gCalFromDate_SystemReports.hide();
    }
    else {
        gCalFromDate_SystemReports.setSelectedDate(gdpFromDate_SystemReports.getSelectedDate());
        gCalFromDate_SystemReports.show();
    }
}

function btn_gdpFromDate_SystemReports_OnMouseUp(event) {
    if (gCalFromDate_SystemReports.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalFromDate_SystemReports_onLoad(sender, e) {
    window.gCalFromDate_SystemReports.PopUpObject.z = 25000000;
}

function gdpToDate_SystemReports_OnDateChange(sender, eventArgs) {
    var ToDate = gdpToDate_SystemReports.getSelectedDate();
    gCalToDate_SystemReports.setSelectedDate(ToDate);
}
function gCalToDate_SystemReports_OnChange(sender, eventArgs) {
    var ToDate = gCalToDate_SystemReports.getSelectedDate();
    gdpToDate_SystemReports.setSelectedDate(ToDate);
}

function btn_gdpToDate_SystemReports_OnClick(event) {
    if (gCalToDate_SystemReports.get_popUpShowing()) {
        gCalToDate_SystemReports.hide();
    }
    else {
        gCalToDate_SystemReports.setSelectedDate(gdpToDate_SystemReports.getSelectedDate());
        gCalToDate_SystemReports.show();
    }
}

function btn_gdpToDate_SystemReports_OnMouseUp(event) {
    if (gCalToDate_SystemReports.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalToDate_SystemReports_onLoad(sender, e) {
    window.gCalToDate_SystemReports.PopUpObject.z = 25000000;
}

function tlbItemClear_TlbClear_FromDateCalendars_SystemReports_onClick() {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_SystemReports').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_SystemReports_picker').value = "";
            break;
    }
}

function tlbItemClear_TlbClear_ToDateCalendars_SystemReports_onClick() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpToDate_SystemReports').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpToDate_SystemReports_picker').value = "";
            break;
    }
}

function tlbItemResultsView_TlbResultView_onClick() {
    ChangeCurrentSystemReportType_SystemReports();
    Changeheader_GridSystemReportType_SystemReports(false);
    CreateSystemReportsFilterConditions_SystemReports();
    ResetSystemReportTypeFeature_SystemReports();
    SetPageIndex_GridSystemReportType_SystemReports(0);
}

function CreateSystemReportsFilterConditions_SystemReports() {
    ObjCurrentSystemReportFilterConditions_SystemReports = new Object();
    var SearchTerm = document.getElementById('txtSearchTerm_SystemReports').value;
    var FromDate = null;
    var ToDate = null;
    switch (parent.SysLangID) {
        case 'fa-IR':
            FromDate = document.getElementById('pdpFromDate_SystemReports').value;
            ToDate = document.getElementById('pdpToDate_SystemReports').value;
            break;
        case 'en-US':
            FromDate = document.getElementById('gdpFromDate_SystemReports_picker').value;
            ToDate = document.getElementById('gdpToDate_SystemReports_picker').value;
            break;
    }
    ObjCurrentSystemReportFilterConditions_SystemReports.SearchTerm = SearchTerm;
    ObjCurrentSystemReportFilterConditions_SystemReports.FromDate = FromDate;
    ObjCurrentSystemReportFilterConditions_SystemReports.ToDate = ToDate;
}

function SetPageIndex_GridSystemReportType_SystemReports(pageIndex) {
    CurrentPageIndex_GridSystemReportType_SystemReports = pageIndex;
    Fill_GridSystemReportType_SystemReports(pageIndex);
}

function Fill_GridSystemReportType_SystemReports(pageIndex) {
    document.getElementById('loadingPanel_GridSystemReportType_SystemReports').innerHTML = document.getElementById('hfloadingPanel_GridSystemReportType_SystemReports').value;
    var pageSize = parseInt(document.getElementById('hfSystemReportTypePageSize_SystemReports').value);
    var FilterConditions = '';
    var SearchTerm = '';
    var FromDate = '';
    var ToDate = '';
    if (ObjCurrentSystemReportFilterConditions_SystemReports != null) {
        var SearchTerm = ObjCurrentSystemReportFilterConditions_SystemReports.SearchTerm;
        var FromDate = ObjCurrentSystemReportFilterConditions_SystemReports.FromDate;
        var ToDate = ObjCurrentSystemReportFilterConditions_SystemReports.ToDate;
    }
    FilterConditions = '{"SearchTerm":"' + SearchTerm + '","FromDate":"' + FromDate + '","ToDate":"' + ToDate + '"}';

    CallBack_GridSystemReportType_SystemReports.callback(CharToKeyCode_SystemReports(GetCurrentSystemReportType_SystemReports().Value), CharToKeyCode_SystemReports(pageSize.toString()), CharToKeyCode_SystemReports(pageIndex.toString()), CharToKeyCode_SystemReports(FilterConditions));
}

function NavigateSystemReportTypeFeature_SystemReports_onCelldbClick(systemReportTypeFeature) {
    var GridSystemReportType = GetGridSystemReportType_SystemReports();
    document.getElementById('txtSystemReportTypeFeature_SystemReports').value = GridSystemReportType.getSelectedItems()[0].getMember(systemReportTypeFeature).get_value();
    Changeheader_SystemReportTypeFeature_SystemReports(systemReportTypeFeature);
}

function Changeheader_SystemReportTypeFeature_SystemReports(systemReportTypeFeature) {
    var GridSystemReportType = GetGridSystemReportType_SystemReports();
    var GridColumnsCol = GridSystemReportType.get_table().get_columns();
    for (var i = 0; i < GridColumnsCol.length; i++) {
        if (GridColumnsCol[i].get_dataField() == systemReportTypeFeature)
        {
            document.getElementById('header_SystemReportTypeFeature_SystemReports').innerHTML = GridColumnsCol[i].get_headingText();
            break;
        }
    }
}

function GridSystemBusinessReport_SystemReports_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridSystemReportType_SystemReports').innerHTML = '';
}

function GridSystemEngineReport_SystemReports_onLoad(sender, e) {
    ResetLoadingPanel_SystemReports();
}

function GridSystemWindowsServiceReport_SystemReports_onLoad(sender, e) {
    ResetLoadingPanel_SystemReports();
}

function GridSystemUserActionReport_SystemReports_onLoad(sender, e) {
    ResetLoadingPanel_SystemReports();
}

function CallBack_GridSystemReportType_SystemReports_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_GridSystemReportType_SystemReports').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridSystemReportType_SystemReports(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else {
        Changefooter_GridSystemReportType_SystemReports();
        Changeheader_GridSystemReportType_SystemReports(false);
    }
}

function Changeheader_GridSystemReportType_SystemReports(IsReset) {
    var SelectedItem_cmbSystemReportType_SystemReports = cmbSystemReportType_SystemReports.getSelectedItem();
    if (SelectedItem_cmbSystemReportType_SystemReports != undefined && SelectedItem_cmbSystemReportType_SystemReports != null) 
        document.getElementById('header_GridSystemReportType_SystemReports').innerHTML = GetCurrentSystemReportType_SystemReports().Text;
}

function CallBack_GridSystemReportType_SystemReports_onCallbackError(sender, e) {
    ResetLoadingPanel_SystemReports();
    ShowConnectionError_SystemReports();
}

function tlbItemRefresh_TlbPaging_GridSystemReportType_SystemReports_onClick() {
    Refresh_GridSystemReportType_SystemReports();
}

function tlbItemFirst_TlbPaging_GridSystemReportType_SystemReports_onClick() {
    SetPageIndex_GridSystemReportType_SystemReports(0);
}

function tlbItemBefore_TlbPaging_GridSystemReportType_SystemReports_onClick() {
    if (CurrentPageIndex_GridSystemReportType_SystemReports != 0) {
        CurrentPageIndex_GridSystemReportType_SystemReports = CurrentPageIndex_GridSystemReportType_SystemReports - 1;
        SetPageIndex_GridSystemReportType_SystemReports(CurrentPageIndex_GridSystemReportType_SystemReports);
    }
}

function tlbItemNext_TlbPaging_GridSystemReportType_SystemReports_onClick() {
    if (CurrentPageIndex_GridSystemReportType_SystemReports < parseInt(document.getElementById('hfSystemReportTypePageCount_SystemReports').value) - 1) {
        CurrentPageIndex_GridSystemReportType_SystemReports = CurrentPageIndex_GridSystemReportType_SystemReports + 1;
        SetPageIndex_GridSystemReportType_SystemReports(CurrentPageIndex_GridSystemReportType_SystemReports);
    }
}

function tlbItemLast_TlbPaging_GridSystemReportType_SystemReports_onClick() {
    SetPageIndex_GridSystemReportType_SystemReports(parseInt(document.getElementById('hfSystemReportTypePageCount_SystemReports').value) - 1);
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_SystemReports) {
        case 'DeleteAll':
            DialogConfirm.Close();
            UpdateSystemReport_SystemReports();
            break;
        case 'Exit':
            CloseDialogSystemReports();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function CharToKeyCode_SystemReports(str) {
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

function ShowDialogConfirm(confirmState) {
    ConfirmState_SystemReports = confirmState;
    if (ConfirmState_SystemReports == 'DeleteAll')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_SystemReports').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_SystemReports').value;
    DialogConfirm.Show();
}

function ShowConnectionError_SystemReports() {
    var error = document.getElementById('hfErrorType_SystemReports').value;
    var errorBody = document.getElementById('hfConnectionError_SystemReports').value;
    showDialog(error, errorBody, 'error');
}

function ResetLoadingPanel_SystemReports() {
    document.getElementById('loadingPanel_GridSystemReportType_SystemReports').innerHTML = '';
}

function Changefooter_GridSystemReportType_SystemReports() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridSystemReportType_SystemReports').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfSystemReportTypePageCount_SystemReports').value) > 0 ? CurrentPageIndex_GridSystemReportType_SystemReports + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfSystemReportTypePageCount_SystemReports').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridSystemReportType_SystemReports').innerHTML = retfooterVal;
}

function Refresh_GridSystemReportType_SystemReports() {
    CurrentPageState_SystemReports = 'View';
    ObjCurrentSystemReportFilterConditions_SystemReports = null;
    SetPageIndex_GridSystemReportType_SystemReports(0);
}

function CloseDialogSystemReports() {
    parent.document.getElementById('DialogSystemReports_IFrame').src = 'WhitePage.aspx';
    parent.DialogSystemReports.Close();
}

function CollapseControls_SystemReports(exception) {
    if (exception == null || exception != cmbSystemReportType_SystemReports)
        cmbSystemReportType_SystemReports.collapse();
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpFromDate_SystemReports');
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}










