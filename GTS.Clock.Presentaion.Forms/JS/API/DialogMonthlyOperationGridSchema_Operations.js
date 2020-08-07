
var box_SearchByPersonnel_MasterMonthlyOperation_IsShown = false;
var header_clmnBarCode_cmbPersonnel_MasterMonthlyOperation = '';
var header_clmnName_cmbPersonnel_MasterMonthlyOperation = '';
var SettingsState_MasterMonthlyOperation = 'View';
var BaseCallBackPrefix_GridMasterMonthlyOperation_MasterMonthlyOperation = null;
var isItemExpanded_GridMasterMonthlyOperation_MasterMonthlyOperation = false;

function CallBack_cmbPersonnel_MasterMonthlyOperation_onCallBackComplete(sender, e) {
}

function cmbSearchField_MasterMonthlyOperation_onChange(sender, e) {
}

function SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame() {
    if (parent.CurrentLangID == 'fa-IR')
        parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.scroll(20000, 0);
}

function imgbox_SearchByPersonnel_MasterMonthlyOperation_onClick() {
    if (DialogPersonnelSearch.get_isShowing()) {
        document.getElementById('imgbox_SearchByPersonnel_MasterMonthlyOperation').src = 'Images/Ghadir/arrowDown.jpg';
        DialogPersonnelSearch.Close();
    }
    else {
        DialogPersonnelSearch.Show();
        document.getElementById('imgbox_SearchByPersonnel_MasterMonthlyOperation').src = 'Images/Ghadir/arrowUp.jpg';
    }
}

function ChangeDirection_Mastertbl_MonthlyOperationGridSchemaForm() {
    if (parent.CurrentLangID == 'en-US')
        document.getElementById('Mastertbl_MonthlyOperationGridSchemaForm').dir = document.getElementById('cmbYear_MasterMonthlyOperation_DropDownContent').dir = document.getElementById('tblConfirm_DialogConfirm').dir = 'ltr';
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('Mastertbl_MonthlyOperationGridSchemaForm').dir = document.getElementById('cmbYear_MasterMonthlyOperation_DropDownContent').dir = document.getElementById('tblConfirm_DialogConfirm').dir = 'rtl';
}

function ChangeDirection_cmbMonth_MasterMonthlyOperation() {
    if (parent.CurrentLangID == 'en-US')
        document.getElementById('cmbMonth_MasterMonthlyOperation_DropDownContent').dir = 'ltr';
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('cmbMonth_MasterMonthlyOperation_DropDownContent').dir = 'rtl';
}


function GetBoxesHeaders_MasterMonthlyOperation() {
    header_clmnName_cmbPersonnel_MasterMonthlyOperation = document.getElementById('hfclmnName_cmbPersonnel_MasterMonthlyOperation').value;
    header_clmnBarCode_cmbPersonnel_MasterMonthlyOperation = document.getElementById('hfclmnBarCode_cmbPersonnel_MasterMonthlyOperation').value;
    document.getElementById('header_Summary_MasterMonthlyOperation').innerHTML = document.getElementById('hfheader_Summary_MasterMonthlyOperation').value;
    parent.document.getElementById('Title_DialogMonthlyOperationGridSchema').innerHTML = document.getElementById('hfTitle_DialogMonthlyOperationGridSchema').value;
    document.getElementById('header_GridSettings_MasterMonthlyOperation').innerHTML = document.getElementById('hfheader_GridSettings_MasterMonthlyOperation').value;
    SetHeader_GridMasterMonthlyOperation_MasterMonthlyOperation();
}

function SetHeader_GridMasterMonthlyOperation_MasterMonthlyOperation() {
    var ResultHeader_MonthlyOperation_MasterMonthlyOperation = null;
    var MasterHeader_MonthlyOperation_MasterMonthlyOperation = document.getElementById('hfheader_MonthlyOperation_MasterMonthlyOperation').value;
    var ObjMonthlyOperation_ManagerMasterMonthlyOperation = parent.DialogMonthlyOperationGridSchema.get_value();
    var LoadState = ObjMonthlyOperation_ManagerMasterMonthlyOperation.LoadState;
    var PersonnelName = null;
    switch (LoadState) {
        case 'Personnel':
            var ObjCurrentUser = document.getElementById('hfCurrentUser_MasterMonthlyOperation').value;
            if (ObjCurrentUser != null) {
                var ObjCurrentUser = eval('(' + ObjCurrentUser + ')');
                PersonnelName = ObjCurrentUser.PersonnelName;
            }
            break;
        case 'Manager':
            PersonnelName = ObjMonthlyOperation_ManagerMasterMonthlyOperation.PersonnelName;
            break;
    }
    switch (parent.parent.CurrentLangID) {
        case 'fa-IR':
            ResultHeader_MonthlyOperation_MasterMonthlyOperation = MasterHeader_MonthlyOperation_MasterMonthlyOperation + ' ' + PersonnelName;
            break;
        case 'en-US':
            ResultHeader_MonthlyOperation_MasterMonthlyOperation = PersonnelName + ' ' + MasterHeader_MonthlyOperation_MasterMonthlyOperation;
            break;
    }
    document.getElementById('header_MonthlyOperation_MasterMonthlyOperation').innerHTML = ResultHeader_MonthlyOperation_MasterMonthlyOperation;    
}

function SetHeaders_cmbPersonnel_MasterMonthlyOperation() {
    document.getElementById('clmnName_cmbPersonnel_MasterMonthlyOperation').innerHTML = header_clmnName_cmbPersonnel_MasterMonthlyOperation;
    document.getElementById('clmnBarCode_cmbPersonnel_MasterMonthlyOperation').innerHTML = header_clmnBarCode_cmbPersonnel_MasterMonthlyOperation;
}

function GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick(field) {
    ShowRelativeDialog_MasterMonthlyOperation(field);
}

function ShowRelativeDialog_MasterMonthlyOperation(field) {
    var LoadState = parent.DialogMonthlyOperationGridSchema.get_value().LoadState;
    if (LoadState == 'Manager')
        return;
    var key = GridMasterMonthlyOperation_MasterMonthlyOperation.getSelectedItems()[0].getMember('ID').get_text();
    var ObjRequest = new Object();
    ObjRequest.LoadState = LoadState;
    ObjRequest.Field = field;
    ObjRequest.DateKey = key;
    ObjRequest.RequestCaller = 'Grid';
    ObjRequest.RequestType = '';
    ObjRequest.RequestDateTitle = GridMasterMonthlyOperation_MasterMonthlyOperation.getItemFromKey(0, key).getMember('DayName').get_text() + " " + GridMasterMonthlyOperation_MasterMonthlyOperation.getItemFromKey(0, key).getMember('TheDate').get_text();
    ObjRequest.RequestDate = GridMasterMonthlyOperation_MasterMonthlyOperation.getItemFromKey(0, key).getMember('Date').get_text();
    ObjRequest.RequestUIDate = GridMasterMonthlyOperation_MasterMonthlyOperation.getItemFromKey(0, key).getMember('TheDate').get_text();
    if (field == 'FirstEntrance' || field == 'FirstExit' || field == 'SecondEntrance' || field == 'SecondExit' || field == 'ThirdEntrance' || field == 'ThirdExit' || field == 'FourthEntrance' || field == 'FourthExit' || field == 'FifthEntrance' || field == 'FifthExit' || field == 'LastExit') {
        ObjRequest.RequestType = 'Traffic';
        DialogRequestOnTraffic.set_value(ObjRequest);
        DialogRequestOnTraffic.Show();
    }
    if (field == 'HourlyUnallowableAbsence') {
        ObjRequest.RequestType = 'Hourly';
        DialogHourlyRequestOnAbsence.set_value(ObjRequest);
        DialogHourlyRequestOnAbsence.Show();
    }
    if (field == 'DailyAbsence') {
        ObjRequest.RequestType = 'Daily';
        DialogDailyRequestOnAbsence.set_value(ObjRequest);
        DialogDailyRequestOnAbsence.Show();
    }
    if (field == 'UnallowableOverTime') {
        var unallowableOverTime = GridMasterMonthlyOperation_MasterMonthlyOperation.getItemFromKey(0, key).getMember('UnallowableOverTime').get_text();
        ObjRequest.RequestType = 'OverTime';
        if (CheckUnallowableOverTimeFieldValue_MasterMonthlyOperation(unallowableOverTime)) {
            DialogRequestOnUnallowableOverTime.set_value(ObjRequest);
            DialogRequestOnUnallowableOverTime.Show();
        }
        else {
            DialogOvertimeJustificationRequest.set_value(ObjRequest);
            DialogOvertimeJustificationRequest.Show();
        }
    }
    CollapseControls_MasterMonthlyOperation();
}

function CheckUnallowableOverTimeFieldValue_MasterMonthlyOperation(unallowableOverTime) {
    var State = null;
    var IsContainsNotEmptyChar = false;
    for (var i = 0; i < unallowableOverTime.length; i++) {
        if (unallowableOverTime.charAt(i) != " ") {
            IsContainsNotEmptyChar = true;
            break;
        }
    }
    State = unallowableOverTime != '00:00' && unallowableOverTime != '' && IsContainsNotEmptyChar ? true : false;
    return State;
}

function ShowDialogGridSettings() {
    SettingsState_MasterMonthlyOperation = 'View';
    document.MonthlyOperationGridSchemaForm.chbSelectAll_GridSettings_MasterMonthlyOperation.checked = false;
    var CurrentSettingID_MasterMonthlyOperation = '0';
    CallBack_GridSettings_MasterMonthlyOperation.callback(CreateColumnsArray_GridMasterMonthlyOperation_MasterMonthlyOperation(), 'Get', '', CharToKeyCode_MasterMonthlyOperation(CurrentSettingID_MasterMonthlyOperation), 'None');
    DialogGridSettings.Show();
    CollapseControls_MasterMonthlyOperation();
}

function CharToKeyCode_MasterMonthlyOperation(str) {
    var OutStr = '';
    for (var i = 0; i < str.length; i++) {
        var KeyCode = str.charCodeAt(i);
        var CharKeyCode = '//' + KeyCode;
        OutStr += CharKeyCode;
    }
    return OutStr;
}

function CreateColumnsArray_GridMasterMonthlyOperation_MasterMonthlyOperation() {
    var ArColumns = '';
    var ColumnsCol_GridMasterMonthlyOperation_MasterMonthlyOperation = GridMasterMonthlyOperation_MasterMonthlyOperation.get_table().get_columns();
    for (var i = 1; i < (ColumnsCol_GridMasterMonthlyOperation_MasterMonthlyOperation.length - 15); i++) {
        var Splitter = ":";
        if (i == ColumnsCol_GridMasterMonthlyOperation_MasterMonthlyOperation.length - 16)
            Splitter = '';
        ArColumns = ArColumns + CharToKeyCode_MasterMonthlyOperation(ColumnsCol_GridMasterMonthlyOperation_MasterMonthlyOperation[i].get_dataField()) + "%" + CharToKeyCode_MasterMonthlyOperation(ColumnsCol_GridMasterMonthlyOperation_MasterMonthlyOperation[i].get_headingText()) + Splitter;
    }
    return ArColumns;
}

function GridSettings_MasterMonthlyOperation_onSave() {
    SettingsState_MasterMonthlyOperation = 'Save';
    var ObjDialogMonthlyOperationGridSchema = parent.DialogMonthlyOperationGridSchema.get_value();
    var CurrentSettingID_MasterMonthlyOperation = document.getElementById('hfCurrentSettingID_GridSettings_MasterMonthlyOperation').value;
    CallBack_GridSettings_MasterMonthlyOperation.callback(CreateColumnsArray_GridMasterMonthlyOperation_MasterMonthlyOperation(), 'Set', CreateColumnsArray_GridSettings_MasterMonthlyOperation(), CharToKeyCode_MasterMonthlyOperation(CurrentSettingID_MasterMonthlyOperation), ObjDialogMonthlyOperationGridSchema.LoadState);
}

function CallBack_GridSettings_MasterMonthlyOperation_CallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_GridSettings_MasterMonthlyOperation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2], false, document.getElementById('Mastertbl_MonthlyOperationGridSchemaForm').offsetWidth);
    }
    else {
        if (SettingsState_MasterMonthlyOperation == 'Save') {
            DialogGridSettings.Close();
            parent.DialogMonthlyOperationGridSchema.Close();
            parent.DialogMonthlyOperationGridSchema.Show();
        }
    }
}

function CreateColumnsArray_GridSettings_MasterMonthlyOperation() {
    var ArColumns = '';
    for (var i = 0; i < GridSettings_MasterMonthlyOperation.get_table().getRowCount(); i++) {
        var Splitter = ':';
        if (i == GridSettings_MasterMonthlyOperation.get_table().getRowCount() - 1)
            Splitter = '';
        ArColumns = ArColumns + CharToKeyCode_MasterMonthlyOperation(GridSettings_MasterMonthlyOperation.get_table().getRow(i).getMember('GridColumn').get_text()) + '%' + GridSettings_MasterMonthlyOperation.get_table().getRow(i).getMember('ViewState').get_text() + Splitter;
    }
    return ArColumns;
}

function DialogPersonnelSearch_onClose() {
    document.getElementById('imgbox_SearchByPersonnel_MasterMonthlyOperation').src = 'Images/Ghadir/arrowDown.jpg';
    DialogPersonnelSearch.Close();
}

function chbSelectAll_GridSettings_MasterMonthlyOperation_onClick() {
    var Checked = null;
    if (document.MonthlyOperationGridSchemaForm.chbSelectAll_GridSettings_MasterMonthlyOperation.checked)
        Checked = true;
    else
        Checked = false;

    GridSettings_MasterMonthlyOperation.beginUpdate();
    for (var i = 0; i < GridSettings_MasterMonthlyOperation.get_table().getRowCount(); i++) {
        GridSettings_MasterMonthlyOperation.get_table().getRow(i).setValue(2, Checked, false);
    }
    GridSettings_MasterMonthlyOperation.endUpdate();
}

function SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(Key, ColumnIndex) {
    var Title = '';
    var GCC = GridMasterMonthlyOperation_MasterMonthlyOperation.get_table().get_columns();
    var gridItem = GridMasterMonthlyOperation_MasterMonthlyOperation.getItemFromKey(0, Key);
    Title += gridItem.getMember('DayName').get_text();
    Title += '\n ' + gridItem.getMember('TheDate').get_text();
    Title += '\n ' + GCC[ColumnIndex].get_headingText();
    if (GCC[ColumnIndex].get_dataField() == 'Shift')
        Title += '\n ' + gridItem.getMember('ShiftPairs').get_text();
    return Title;
}


function GridMasterMonthlyOperation_MasterMonthlyOperation_onLoad(sender, e) {
    GridMasterMonthlyOperation_MasterMonthlyOperation_onAfterLoad();
}

function GridMasterMonthlyOperation_MasterMonthlyOperation_onAfterLoad() {
    document.getElementById('loadingPanel_GridMasterMonthlyOperation_MasterMonthlyOperation').innerHTML = '';
    BaseCallBackPrefix_GridMasterMonthlyOperation_MasterMonthlyOperation = GridMasterMonthlyOperation_MasterMonthlyOperation.CallbackPrefix;
}

function GridMasterMonthlyOperation_MasterMonthlyOperation_onBeforeCallback(sender, e) {
    SetCallBackPrefix_GridMasterMonthlyOperation_MasterMonthlyOperation();
    parent.DialogLoading.Show();
}

function GridMasterMonthlyOperation_MasterMonthlyOperation_onRenderComplete(sender, e) {
    parent.DialogLoading.Close(); 
}

function GridMasterMonthlyOperation_MasterMonthlyOperation_onCallbackComplete() {
    parent.DialogLoading.Close();
}

function SetCallBackPrefix_GridMasterMonthlyOperation_MasterMonthlyOperation() {
    var ObjMonthlyOperation_MasterMonthlyOperation = parent.DialogMonthlyOperationGridSchema.get_value();
    var ObjCalculationDateRange_MasterMonthlyOperation = document.getElementById('hfCurrentMonth_MasterMonthlyOperation').value;
    ObjCalculationDateRange_MasterMonthlyOperation = eval('(' + ObjCalculationDateRange_MasterMonthlyOperation + ')');
    var year = document.getElementById('hfCurrentYear_MasterMonthlyOperation').value;
    var month = ObjCalculationDateRange_MasterMonthlyOperation.Order;
    var FromDate = ObjCalculationDateRange_MasterMonthlyOperation.FromDate;
    var ToDate = ObjCalculationDateRange_MasterMonthlyOperation.ToDate;
    GridMasterMonthlyOperation_MasterMonthlyOperation.CallbackPrefix = BaseCallBackPrefix_GridMasterMonthlyOperation_MasterMonthlyOperation + "&PersonnelID=" + CharToKeyCode_MasterMonthlyOperation(ObjMonthlyOperation_MasterMonthlyOperation.PersonnelID) + "&Year=" + CharToKeyCode_MasterMonthlyOperation(year) + "&Month=" + CharToKeyCode_MasterMonthlyOperation(month) + "&FromDate=" + CharToKeyCode_MasterMonthlyOperation(FromDate) + "&ToDate=" + CharToKeyCode_MasterMonthlyOperation(ToDate) + "";
}

function btn_gdpDetailsFromDate_MasterMonthlyOperation_OnMouseUp(event) {
    if (gCalDetailsFromDate_MasterMonthlyOperation.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpDetailsFromDate_MasterMonthlyOperation_OnDateChange(sender, e) {
    var Date = gdpDetailsFromDate_MasterMonthlyOperation.getSelectedDate();
    gCalDetailsFromDate_MasterMonthlyOperation.setSelectedDate(Date);
}

function btn_gdpDetailsFromDate_MasterMonthlyOperation_OnClick(event) {
    if (gCalDetailsFromDate_MasterMonthlyOperation.get_popUpShowing()) {
        gCalDetailsFromDate_MasterMonthlyOperation.hide();
    }
    else {
        gCalDetailsFromDate_MasterMonthlyOperation.setSelectedDate(gdpDetailsFromDate_MasterMonthlyOperation.getSelectedDate());
        gCalDetailsFromDate_MasterMonthlyOperation.show();
    }
}

function gCalDetailsFromDate_MasterMonthlyOperation_OnChange(sender, e) {
    var Date = gCalDetailsFromDate_MasterMonthlyOperation.getSelectedDate();
    gdpDetailsFromDate_MasterMonthlyOperation.setSelectedDate(Date);
}

function gCalDetailsFromDate_MasterMonthlyOperation_onLoad(sender, e) {
    window.gCalDetailsFromDate_MasterMonthlyOperation.PopUpObject.z = 25000000;
}


function btn_gdpDetailsToDate_MasterMonthlyOperation_OnMouseUp(event) {
    if (gCalDetailsFromDate_MasterMonthlyOperation.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpDetailsToDate_MasterMonthlyOperation_OnDateChange(sender, e) {
    var Date = gdpDetailsToDate_MasterMonthlyOperation.getSelectedDate();
    gCalDetailsFromDate_MasterMonthlyOperation.setSelectedDate(Date);
}

function btn_gdpDetailsToDate_MasterMonthlyOperation_OnClick(event) {
    if (gCalDetailsToDate_MasterMonthlyOperation.get_popUpShowing()) {
        gCalDetailsToDate_MasterMonthlyOperation.hide();
    }
    else {
        gCalDetailsToDate_MasterMonthlyOperation.setSelectedDate(gdpDetailsToDate_MasterMonthlyOperation.getSelectedDate());
        gCalDetailsToDate_MasterMonthlyOperation.show();
    }
}

function gCalDetailsToDate_MasterMonthlyOperation_OnChange(sender, e) {
    var Date = gCalDetailsToDate_MasterMonthlyOperation.getSelectedDate();
    gdpDetailsToDate_MasterMonthlyOperation.setSelectedDate(Date);
}

function gCalDetailsToDate_MasterMonthlyOperation_onLoad(sender, e) {
    window.gCalDetailsToDate_MasterMonthlyOperation.PopUpObject.z = 25000000;
}

function tlbItemView_TlbView_MasterMonthlyOperation_onClick() {
    Fill_GridMasterMonthlyOperation_MasterMonthlyOperation();
}

function cmbYear_MasterMonthlyOperation_onChange(sender, e) {
    document.getElementById('hfCurrentYear_MasterMonthlyOperation').value = cmbYear_MasterMonthlyOperation.getSelectedItem().get_value();
    Fill_cmbMonth_MasterMonthlyOperation();
}

function cmbMonth_MasterMonthlyOperation_onChange(sender, e) {
    document.getElementById('hfCurrentMonth_MasterMonthlyOperation').value = cmbMonth_MasterMonthlyOperation.getSelectedItem().get_value();
    NavigateCalculationDateRange_MasterMonthlyOperation();
}

function Fill_GridMasterMonthlyOperation_MasterMonthlyOperation() {
    parent.parent.DialogLoading.Show();
    document.getElementById('loadingPanel_GridMasterMonthlyOperation_MasterMonthlyOperation').innerHTML = document.getElementById('hfloadingPanel_GridMasterMonthlyOperation_MasterMonthlyOperation').value;
    var ObjCalculationDateRange = document.getElementById('hfCurrentMonth_MasterMonthlyOperation').value;
    if (ObjCalculationDateRange != null && ObjCalculationDateRange != undefined && ObjCalculationDateRange != "") {
        ObjCalculationDateRange = eval('(' + ObjCalculationDateRange + ')');
        var year = document.getElementById('hfCurrentYear_MasterMonthlyOperation').value;
        var month = ObjCalculationDateRange.Order;
        var FromDate = ObjCalculationDateRange.FromDate;
        var ToDate = ObjCalculationDateRange.ToDate;
        var PersonnelID = null;
        var LoadState = null;
        var ObjMonthlyOperation_MasterMonthlyOperation = parent.DialogMonthlyOperationGridSchema.get_value();
        if (ObjMonthlyOperation_MasterMonthlyOperation != null && ObjMonthlyOperation_MasterMonthlyOperation != undefined) {
            LoadState = ObjMonthlyOperation_MasterMonthlyOperation.LoadState;
            PersonnelID = ObjMonthlyOperation_MasterMonthlyOperation.PersonnelID;
        }
        CallBack_GridMasterMonthlyOperation_MasterMonthlyOperation.callback(CharToKeyCode_MasterMonthlyOperation(LoadState), CharToKeyCode_MasterMonthlyOperation(PersonnelID), CharToKeyCode_MasterMonthlyOperation(year), CharToKeyCode_MasterMonthlyOperation(month), CharToKeyCode_MasterMonthlyOperation(FromDate), CharToKeyCode_MasterMonthlyOperation(ToDate));
    }
    else {
        parent.parent.DialogLoading.Close();
        SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();
        GridMasterMonthlyOperation_MasterMonthlyOperation_onAfterLoad();
        showDialog(document.getElementById('hfRetErrorType_MasterMonthlyOperation').value, document.getElementById('ErrorHiddenField_CalculationDateRange').value, 'error', false, document.getElementById('Mastertbl_MonthlyOperationGridSchemaForm').offsetWidth);
    }
}

function CallBack_GridMasterMonthlyOperation_MasterMonthlyOperation_onCallbackComplete(sender, e) {
    parent.parent.DialogLoading.Close();
    SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();
    var error = document.getElementById('ErrorHiddenField_MonthlyOperation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2], false, document.getElementById('Mastertbl_MonthlyOperationGridSchemaForm').offsetWidth);
        if (errorParts[3] == 'Reload')
            Fill_GridMasterMonthlyOperation_MasterMonthlyOperation();
    }
    else
        Fill_GridSummaryMonthlyOperation_MasterMonthlyOperation();
}


function GridMasterMonthlyOperation_MasterMonthlyOperation_onCallbackError(sender, e) {
    parent.parent.DialogLoading.Close();
    GridMasterMonthlyOperation_MasterMonthlyOperation.collapseItem(GridMasterMonthlyOperation_MasterMonthlyOperation.getSelectedItems()[0]);
    var RetErrorType = document.getElementById('hfRetErrorType_MasterMonthlyOperation').value;
    var ErrorBody = document.getElementById('hfCallBackError_GridMasterMonthlyOperation_MasterMonthlyOperation').value + ' ' + e.get_errorMessage();
    showDialog(RetErrorType, ErrorBody, 'error', false, document.getElementById('Mastertbl_MonthlyOperationGridSchemaForm').offsetWidth);
}

function Fill_GridSummaryMonthlyOperation_MasterMonthlyOperation() {
    CallBack_GridSummaryMonthlyOperation_MasterMonthlyOperation.callback();
}

function tlbItemUnderManagementPersonnelInformationView_TlbMasterMonthlyOperation_onClick() {
    ViewUnderManagementPersonnelInformation_MasterMonthlyOperation();
}

function ViewUnderManagementPersonnelInformation_MasterMonthlyOperation() {
}

function tlbItemExcelExport_TlbMasterMonthlyOperation_onClick() {
    ExcelExport_GridMasterMonthlyOperation_MasterMonthlyOperation();
}

function ExcelExport_GridMasterMonthlyOperation_MasterMonthlyOperation() {
}

function tlbItemGridSettings_TlbMasterMonthlyOperation_onClick() {
    ShowDialogGridSettings();
}

function tlbItemExit_TlbMasterMonthlyOperation_onClick() {
    ShowDialogConfirm();
}

function CloseDialogMonthlyOperationGridSchema() {
    parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').src = 'WhitePage.aspx';
    parent.DialogMonthlyOperationGridSchema.Close();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDialogMonthlyOperationGridSchema();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_MasterMonthlyOperation').value;
    DialogConfirm.Show();
    CollapseControls_MasterMonthlyOperation();
}

function Fill_cmbMonth_MasterMonthlyOperation() {
    var year = document.getElementById('hfCurrentYear_MasterMonthlyOperation').value;
    var PersonnelID = null;
    var LoadState = null;
    var ObjMonthlyOperation_MasterMonthlyOperation = parent.DialogMonthlyOperationGridSchema.get_value();
    if (ObjMonthlyOperation_MasterMonthlyOperation != null && ObjMonthlyOperation_MasterMonthlyOperation != undefined) {
        LoadState = ObjMonthlyOperation_MasterMonthlyOperation.LoadState;
        PersonnelID = ObjMonthlyOperation_MasterMonthlyOperation.PersonnelID;
    }
    CallBack_cmbMonth_MasterMonthlyOperation.callback(CharToKeyCode_MasterMonthlyOperation(LoadState), CharToKeyCode_MasterMonthlyOperation(year), CharToKeyCode_MasterMonthlyOperation(PersonnelID.toString()));
}

function CallBack_cmbMonth_MasterMonthlyOperation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Months_MasterMonthlyOperation').value;
    if (error != "") {
        parent.parent.DialogLoading.Close();
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2], false, document.getElementById('Mastertbl_MonthlyOperationGridSchemaForm').offsetWidth);
    }
    else {
        ChangeDirection_cmbMonth_MasterMonthlyOperation();
        NavigateCalculationDateRange_MasterMonthlyOperation();
        Fill_GridMasterMonthlyOperation_MasterMonthlyOperation();
    }
}

function NavigateCalculationDateRange_MasterMonthlyOperation() {
    var objCalculationDateRange = document.getElementById('hfCurrentMonth_MasterMonthlyOperation').value;
    if (objCalculationDateRange != null && objCalculationDateRange != undefined && objCalculationDateRange != "") {
        objCalculationDateRange = eval('(' + objCalculationDateRange + ')');
        document.getElementById('txtFromDate_MasterMonthlyOperation').value = objCalculationDateRange.FromDate;
        document.getElementById('txtToDate_MasterMonthlyOperation').value = objCalculationDateRange.ToDate;
    }
}

function CallBack_GridSummaryMonthlyOperation_MasterMonthlyOperation_CallbackComplete(sender, e) {
    var error = document.getElementById('ErroHiddenField_SummaryMonthlyOperation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2], false, document.getElementById('Mastertbl_MonthlyOperationGridSchemaForm').offsetWidth);
    }
    else
        CallBack_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation.callback();
}

function tlbItemSave_TlbGridSettings_onClick() {
    GridSettings_MasterMonthlyOperation_onSave();
}

function tlbItemExit_TlbGridSettings_onClick() {
    DialogGridSettings.Close();
    SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();
}

function tlbItemRefresh_TlbMasterMonthlyOperation_onClick() {
    Fill_GridMasterMonthlyOperation_MasterMonthlyOperation();
}

function CallBack_cmbMonth_MasterMonthlyOperation_onCallbackError(sender, e) {
    ShowConnectionError_MasterMonthlyOperation();
}

function CallBack_GridMasterMonthlyOperation_MasterMonthlyOperation_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridMasterMonthlyOperation_MasterMonthlyOperation').innerHTML = '';
    ShowConnectionError_MasterMonthlyOperation();
}

function CallBack_GridSummaryMonthlyOperation_MasterMonthlyOperation_onCallbackError(sender, e) {
    ShowConnectionError_MasterMonthlyOperation();
}

function CallBack_GridSettings_MasterMonthlyOperation_onCallbackError(sender, e) {
    ShowConnectionError_MasterMonthlyOperation();
}

function ShowConnectionError_MasterMonthlyOperation() {
    var error = document.getElementById('hfErrorType_MasterMonthlyOperation').value;
    var errorBody = document.getElementById('hfConnectionError_MasterMonthlyOperation').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemRequestsView_TlbMasterMonthlyOperation_onClick() {
    ShowDialogRegisteredRequests();
}

function ShowDialogRegisteredRequests() {
    var ObjDialogRegisteredRequests = new Object();
    ObjDialogRegisteredRequests.Caller = 'MonthlyOperationGridSchema';
    parent.DialogRegisteredRequests.set_value(ObjDialogRegisteredRequests);
    parent.DialogRegisteredRequests.Show();
}

function tlbItemDetailsInformation_TlbMasterMonthlyOperation_onClick() {
    var ObjUserInformation = new Object();
    var ObjDialogMonthlyOperationGridSchema = parent.DialogMonthlyOperationGridSchema.get_value();
    ObjUserInformation.CallerSchema = 'Grid';
    ObjUserInformation.LoadState = ObjDialogMonthlyOperationGridSchema.LoadState;
    DialogUserInformation.set_value(ObjUserInformation);
    DialogUserInformation.Show();
}

function CollapseControls_MasterMonthlyOperation() {
    cmbYear_MasterMonthlyOperation.collapse();
    cmbMonth_MasterMonthlyOperation.collapse();
}

function tlbItemFormReconstruction_TlbMasterMonthlyOperation_onClick() {
    var ObjDialogMonthlyOperationGridSchema = parent.DialogMonthlyOperationGridSchema.get_value();
    var loadState = ObjDialogMonthlyOperationGridSchema.LoadState;
    CloseDialogMonthlyOperationGridSchema();
    switch (loadState) {
        case 'Manager':
            parent.document.getElementById('pgvManagerMasterMonthlyOperationReport_iFrame').contentWindow.ShowDialogMonthlyOperationGridSchema();
            break;
        case 'Personnel':
            parent.document.getElementById('pgvPersonnelMasterMonthlyOperationReport_iFrame').contentWindow.ShowDialogMonthlyOperationGridSchema();
            break;
    }
}

function GridMasterMonthlyOperation_MasterMonthlyOperation_onItemCollapse(sender, e) {
    isItemExpanded_GridMasterMonthlyOperation_MasterMonthlyOperation = false;
    if (!isItemExpanded_GridMasterMonthlyOperation_MasterMonthlyOperation) {
        ChangePositionByScroll_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation();
    }
}

function GridMasterMonthlyOperation_MasterMonthlyOperation_onItemExpand(sender, e) {
    GridMasterMonthlyOperation_MasterMonthlyOperation.render();
    parent.DialogLoading.Close();
    isItemExpanded_GridMasterMonthlyOperation_MasterMonthlyOperation = true;
    if (isItemExpanded_GridMasterMonthlyOperation_MasterMonthlyOperation) {
        ChangePositionByScroll_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation();
    }
}

function GetIsItemExpanded_GridMasterMonthlyOperation_MasterMonthlyOperation() {
    return isItemExpanded_GridMasterMonthlyOperation_MasterMonthlyOperation;
}

function ChangeHorizontalPosition_Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation() {
    var CustomPadding = '40px';
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            document.getElementById('Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation').style.paddingRight = CustomPadding;
            break;
        case 'en-US':
            document.getElementById('Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation').style.paddingLeft = CustomPadding;
            break;
    }
}

function tlbItemHelp_TlbMasterMonthlyOperation_onClick() {
    LoadHelpPage('tlbItemHelp_TlbMasterMonthlyOperation');
}
