
var SettingsState_MonthlyOperationGanttChartSchema = 'View';
var CurrentDataPointFeaturesObj = null;

function GetBoxesHeaders_MonthlyOperationGanttChartSchema() {
    SetHeader_MonthlyOperation_MonthlyOperationGanttChartSchema();
    parent.document.getElementById('Title_DialogMonthlyOperationGanttChartSchema').innerHTML = document.getElementById('hfTitle_DialogMonthlyOperationGanttChartSchema').value;
    document.getElementById('header_GridSettings_MonthlyOperationGanttChartSchema').innerHTML = document.getElementById('hfheader_GridSettings_MonthlyOperationGanttChartSchema').value;
}

function SetHeader_MonthlyOperation_MonthlyOperationGanttChartSchema() {
    var ResultHeader_MonthlyOperation_MonthlyOperationGanttChartSchema = null;
    var MasterHeader_MonthlyOperation_MonthlyOperationGanttChartSchema = document.getElementById('hfheader_MonthlyOperation_MonthlyOperationGanttChartSchema').value;
    var ObjMonthlyOperation_ManagerMonthlyOperationGanttChartSchema = parent.DialogMonthlyOperationGanttChartSchema.get_value();
    var LoadState = ObjMonthlyOperation_ManagerMonthlyOperationGanttChartSchema.LoadState;
    var PersonnelName = null;
    switch (LoadState) {
        case 'Personnel':
            var ObjCurrentUser = document.getElementById('hfCurrentUser_MonthlyOperationGanttChartSchema').value;
            if (ObjCurrentUser != null) {
                var ObjCurrentUser = eval('(' + ObjCurrentUser + ')');
                PersonnelName = ObjCurrentUser.PersonnelName;
            }
            break;
        case 'Manager':
            PersonnelName = ObjMonthlyOperation_ManagerMonthlyOperationGanttChartSchema.PersonnelName;
            break;
    }
    switch (parent.parent.CurrentLangID) {
        case 'fa-IR':
            ResultHeader_MonthlyOperation_MonthlyOperationGanttChartSchema = MasterHeader_MonthlyOperation_MonthlyOperationGanttChartSchema + ' ' + PersonnelName;
            break;
        case 'en-US':
            ResultHeader_MonthlyOperation_MonthlyOperationGanttChartSchema = PersonnelName + ' ' + MasterHeader_MonthlyOperation_MonthlyOperationGanttChartSchema;
            break;
    }
    document.getElementById('header_MonthlyOperation_MonthlyOperationGanttChartSchema').innerHTML = ResultHeader_MonthlyOperation_MonthlyOperationGanttChartSchema;    

}

function DataPointCollection_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema_onMouseOver(Serie, Index) {
}

function DataPointCollection_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema_onDblClick(Serie, Index) {
    CurrentDataPointFeaturesObj = GetRelatedDataPointFeaturesObj_MonthlyOperationGanttChartSchema(Serie, Index);
    ShowRelativeDialog_MonthlyOperationGanttChartSchema(CurrentDataPointFeaturesObj.Type);
}

function GetRelatedDataPointFeaturesObj_MonthlyOperationGanttChartSchema(Serie, Index) {
    var DataPointFeaturesObj = null;
    var Key = Serie + "%" + Index;
    var RetPointFeaturesCol = document.getElementById('hfTaskFeatures_MonthlyOperationGanttChartSchema').value;
    RetPointFeaturesCol = eval('(' + RetPointFeaturesCol + ')');
    if (RetPointFeaturesCol != null && RetPointFeaturesCol.length > 0) {
        for (var i = 0; i < RetPointFeaturesCol.length; i++) {
            if (RetPointFeaturesCol[i].Key == Key) {
                DataPointFeaturesObj = RetPointFeaturesCol[i];
                break;
            }
        }
    }
    return DataPointFeaturesObj;
}

function ShowRelativeDialog_MonthlyOperationGanttChartSchema(field) {
    if (CurrentDataPointFeaturesObj != null) {
        var LoadState = parent.DialogMonthlyOperationGanttChartSchema.get_value().LoadState;
        if (LoadState == 'Manager')
            return;
        var ObjRequest = new Object();
        ObjRequest.LoadState = LoadState;
        ObjRequest.Field = field;
        ObjRequest.DateKey = CurrentDataPointFeaturesObj.DateKey;
        ObjRequest.RequestCaller = 'GanttChart';
        ObjRequest.RequestType = '';
        ObjRequest.RequestDateTitle = CurrentDataPointFeaturesObj.DayName + " " + CurrentDataPointFeaturesObj.UIDate;
        ObjRequest.RequestDate = CurrentDataPointFeaturesObj.Date;
        ObjRequest.RequestUIDate = CurrentDataPointFeaturesObj.UIDate;
        if (field == 'PairlyHourlyUnallowableAbsence') {
            ObjRequest.RequestType = 'Hourly';
            DialogHourlyRequestOnAbsence.set_value(ObjRequest);
            DialogHourlyRequestOnAbsence.Show();
        }
        if (field == 'PairlyDailyAbsence') {
            ObjRequest.RequestType = 'Daily';
            DialogDailyRequestOnAbsence.set_value(ObjRequest);
            DialogDailyRequestOnAbsence.Show();
        }
        if (field == 'PairlyUnallowableOverTime') {
            ObjRequest.RequestType = 'OverTime';
            DialogRequestOnUnallowableOverTime.set_value(ObjRequest);
            DialogRequestOnUnallowableOverTime.Show();
        }
        CollapseControls_MonthlyOperationGanttChartSchema();
    }
}

function CallBack_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema_onCallBackComplete(sender, e) {
    parent.parent.DialogLoading.Close();
    var error = document.getElementById('ErrorHiddenField_MonthlyOperationGanttChartSchema').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema();
    }
}

function CallBack_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema_onCallbackError(sender, e) {
    ShowConnectionError_MonthlyOperationGanttChartSchema();
}

function cmbYear_MonthlyOperationGanttChartSchema_onChange(sender, e) {
    document.getElementById('hfCurrentYear_MonthlyOperationGanttChartSchema').value = cmbYear_MonthlyOperationGanttChartSchema.getSelectedItem().get_value();
    Fill_cmbMonth_MonthlyOperationGanttChartSchema();
}

function Fill_cmbMonth_MonthlyOperationGanttChartSchema() {
    var year = document.getElementById('hfCurrentYear_MonthlyOperationGanttChartSchema').value;
    var PersonnelID = null;
    var LoadState = null;
    var ObjMonthlyOperation_MonthlyOperationGanttChartSchema = parent.DialogMonthlyOperationGanttChartSchema.get_value();
    if (ObjMonthlyOperation_MonthlyOperationGanttChartSchema != null && ObjMonthlyOperation_MonthlyOperationGanttChartSchema != undefined) {
        LoadState = ObjMonthlyOperation_MonthlyOperationGanttChartSchema.LoadState;
        PersonnelID = ObjMonthlyOperation_MonthlyOperationGanttChartSchema.PersonnelID;
    }
    CallBack_cmbMonth_MonthlyOperationGanttChartSchema.callback(CharToKeyCode_MonthlyOperationGanttChartSchema(LoadState), CharToKeyCode_MonthlyOperationGanttChartSchema(year), CharToKeyCode_MonthlyOperationGanttChartSchema(PersonnelID.toString()));
}

function cmbMonth_MonthlyOperationGanttChartSchema_onChange(sender, e) {
    document.getElementById('hfCurrentMonth_MonthlyOperationGanttChartSchema').value = cmbMonth_MonthlyOperationGanttChartSchema.getSelectedItem().get_value();
    NavigateCalculationDateRange_MonthlyOperationGanttChartSchema();
}

function NavigateCalculationDateRange_MonthlyOperationGanttChartSchema() {
    var objCalculationDateRange = document.getElementById('hfCurrentMonth_MonthlyOperationGanttChartSchema').value;
    if (objCalculationDateRange != null && objCalculationDateRange != undefined && objCalculationDateRange != "") {
        objCalculationDateRange = eval('(' + objCalculationDateRange + ')');
        document.getElementById('txtFromDate_MonthlyOperationGanttChartSchema').value = objCalculationDateRange.FromDate;
        document.getElementById('txtToDate_MonthlyOperationGanttChartSchema').value = objCalculationDateRange.ToDate;
    }
}

function CallBack_cmbMonth_MonthlyOperationGanttChartSchema_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Months_MonthlyOperationGanttChartSchema').value;
    if (error != "") {
        parent.parent.DialogLoading.Close();
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else {
        NavigateCalculationDateRange_MonthlyOperationGanttChartSchema();
        Fill_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema();
    }
}

function Fill_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema() {
    parent.parent.DialogLoading.Show();
    var ObjCalculationDateRange = document.getElementById('hfCurrentMonth_MonthlyOperationGanttChartSchema').value;
    if (ObjCalculationDateRange != null && ObjCalculationDateRange != undefined && ObjCalculationDateRange != "") {
        var GanttChartWidth = (screen.width - 50).toString();
        var GanttChartHeight = (0.60 * screen.height).toString();
        ObjCalculationDateRange = eval('(' + ObjCalculationDateRange + ')');
        var year = document.getElementById('hfCurrentYear_MonthlyOperationGanttChartSchema').value;
        var month = ObjCalculationDateRange.Order;
        var FromDate = ObjCalculationDateRange.FromDate;
        var ToDate = ObjCalculationDateRange.ToDate;
        var PersonnelID = null;
        var LoadState = null;
        var ObjMonthlyOperation_MonthlyOperationGanttChartSchema = parent.DialogMonthlyOperationGanttChartSchema.get_value();
        if (ObjMonthlyOperation_MonthlyOperationGanttChartSchema != null && ObjMonthlyOperation_MonthlyOperationGanttChartSchema != undefined) {
            LoadState = ObjMonthlyOperation_MonthlyOperationGanttChartSchema.LoadState;
            PersonnelID = ObjMonthlyOperation_MonthlyOperationGanttChartSchema.PersonnelID;
        }
        CallBack_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.callback(CharToKeyCode_MonthlyOperationGanttChartSchema(LoadState), CharToKeyCode_MonthlyOperationGanttChartSchema(PersonnelID), CharToKeyCode_MonthlyOperationGanttChartSchema(year), CharToKeyCode_MonthlyOperationGanttChartSchema(month), CharToKeyCode_MonthlyOperationGanttChartSchema(FromDate), CharToKeyCode_MonthlyOperationGanttChartSchema(ToDate), CharToKeyCode_MonthlyOperationGanttChartSchema(GanttChartWidth), CharToKeyCode_MonthlyOperationGanttChartSchema(GanttChartHeight));
    }
    else {
        parent.parent.DialogLoading.Close();
        showDialog(document.getElementById('hfRetErrorType_MonthlyOperationGanttChartSchema').value, document.getElementById('ErrorHiddenField_CalculationDateRange').value, 'error', false, document.getElementById('Mastertbl_MonthlyOperationGridSchemaForm').offsetWidth);
    }
}

function CallBack_cmbMonth_MonthlyOperationGanttChartSchema_onCallbackError(sender, e) {
    ShowConnectionError_MonthlyOperationGanttChartSchema();
}

function tlbItemView_TlbView_MonthlyOperationGanttChartSchema_onClick() {
    Fill_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema();
}

function tlbItemRequestsView_TlbMonthlyOperation_onClick() {
    ShowDialogRegisteredRequests();
}

function ShowDialogRegisteredRequests() {
    var ObjDialogRegisteredRequests = new Object();
    ObjDialogRegisteredRequests.Caller = 'MonthlyOperationGanttChartSchema';
    parent.DialogRegisteredRequests.set_value(ObjDialogRegisteredRequests);
    parent.DialogRegisteredRequests.Show();
}

function tlbItemRefresh_TlbMonthlyOperation_onClick() {
    Fill_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema();
}

function tlbItemDetailsInformation_TlbMonthlyOperation_onClick() {
    var ObjUserInformation = new Object();
    var ObjDialogMonthlyOperationGridSchema = parent.DialogMonthlyOperationGridSchema.get_value();
    ObjUserInformation.CallerSchema = 'GanttChart';
    ObjUserInformation.LoadState = ObjDialogMonthlyOperationGridSchema.LoadState;
    DialogUserInformation.set_value(ObjUserInformation);
    DialogUserInformation.Show();
}

function tlbItemFormReconstruction_TlbMonthlyOperation_onClick() {
    var ObjDialogMonthlyOperationGanttChartSchema = parent.DialogMonthlyOperationGanttChartSchema.get_value();
    var loadState = ObjDialogMonthlyOperationGanttChartSchema.LoadState;
    CloseDialogMonthlyOperationGanttChartSchema();
    switch (loadState) {
        case 'Manager':
            parent.document.getElementById('pgvManagerMasterMonthlyOperationReport_iFrame').contentWindow.ShowDialogMonthlyOperationGanttChartSchema();
            break;
        case 'Personnel':
            parent.document.getElementById('pgvPersonnelMasterMonthlyOperationReport_iFrame').contentWindow.ShowDialogMonthlyOperationGanttChartSchema();
            break;
    }
}

function CloseDialogMonthlyOperationGanttChartSchema() {
    parent.document.getElementById('DialogMonthlyOperationGanttChartSchema_IFrame').src = 'WhitePage.aspx';
    parent.DialogMonthlyOperationGanttChartSchema.Close();
}


function tlbItemGanttChartSettings_TlbMonthlyOperation_onClick() {
    ShowDialogGanttChartSettings();
}

function ShowDialogGanttChartSettings() {
    SettingsState_MonthlyOperationGanttChartSchema = 'View';
    document.MonthlyOperationGanttChartSchemaForm.chbSelectAll_GridSettings_MonthlyOperationGanttChartSchema.checked = false;
    var CurrentSettingID_MonthlyOperationGanttChartSchema = '0';
    CallBack_GridSettings_MonthlyOperationGanttChartSchema.callback('Get', '', CharToKeyCode_MonthlyOperationGanttChartSchema(CurrentSettingID_MonthlyOperationGanttChartSchema), 'None');
    DialogGanttChartSettings.Show();
    CollapseControls_MonthlyOperationGanttChartSchema();
}

function CollapseControls_MonthlyOperationGanttChartSchema() {
    cmbYear_MonthlyOperationGanttChartSchema.collapse();
    cmbMonth_MonthlyOperationGanttChartSchema.collapse();
}

function tlbItemHelp_TlbMonthlyOperation_onClick() {
    LoadHelpPage('tlbItemHelp_TlbMonthlyOperation');
}

function tlbItemExit_TlbMonthlyOperation_onClick() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_MonthlyOperationGanttChartSchema').value;
    DialogConfirm.Show();
    CollapseControls_MonthlyOperationGanttChartSchema();
}

function CharToKeyCode_MonthlyOperationGanttChartSchema(str) {
    var OutStr = '';
    for (var i = 0; i < str.length; i++) {
        var KeyCode = str.charCodeAt(i);
        var CharKeyCode = '//' + KeyCode;
        OutStr += CharKeyCode;
    }
    return OutStr;
}

function tlbItemSave_TlbGanttChartSettings_onClick() {
    GridSettings_MonthlyOperationGanttChartSchema_onSave();
}

function GridSettings_MonthlyOperationGanttChartSchema_onSave() {
    SettingsState_MonthlyOperationGanttChartSchema = 'Save';
    var ObjDialogMonthlyOperationGanttChartSchema = parent.DialogMonthlyOperationGanttChartSchema.get_value();
    var CurrentSettingID_MonthlyOperationGanttChartSchema = document.getElementById('hfCurrentSettingID_GridSettings_MonthlyOperationGanttChartSchema').value;
    CallBack_GridSettings_MonthlyOperationGanttChartSchema.callback('Set', CharToKeyCode_MonthlyOperationGanttChartSchema(CreateTasksObj_GridSettings_MonthlyOperationGanttChartSchema()), CharToKeyCode_MonthlyOperationGanttChartSchema(CurrentSettingID_MonthlyOperationGanttChartSchema), ObjDialogMonthlyOperationGanttChartSchema.LoadState);
}

function CreateTasksObj_GridSettings_MonthlyOperationGanttChartSchema() {
    var ObjTasksCol = '';
    for (var i = 0; i < GridSettings_MonthlyOperationGanttChartSchema.get_table().getRowCount(); i++) {
        var Splitter = ',';
        if (i == GridSettings_MonthlyOperationGanttChartSchema.get_table().getRowCount() - 1)
            Splitter = '';

        var ObjTask = new Object();
        ObjTask.ConceptTitle = GridSettings_MonthlyOperationGanttChartSchema.get_table().getRow(i).getMember('ConceptTitle').get_text();
        ObjTask.ConceptCaption = GridSettings_MonthlyOperationGanttChartSchema.get_table().getRow(i).getMember('ConceptCaption').get_text();
        ObjTask.ViewState = GridSettings_MonthlyOperationGanttChartSchema.get_table().getRow(i).getMember('ViewState').get_text();

        ObjTasksCol = ObjTasksCol + '{"ConceptTitle":"' + ObjTask.ConceptTitle + '","ConceptCaption":"' + ObjTask.ConceptCaption + '","ViewState":"' + ObjTask.ViewState + '"}' + Splitter;
    }
    ObjTasksCol = '[' + ObjTasksCol + ']';
    return ObjTasksCol;
}

function tlbItemExit_TlbGanttChartSettings_onClick() {
    DialogGanttChartSettings.Close();
}

function chbSelectAll_GridSettings_MonthlyOperationGanttChartSchema_onClick() {
    var Checked = null;
    if (document.MonthlyOperationGanttChartSchemaForm.chbSelectAll_GridSettings_MonthlyOperationGanttChartSchema.checked)
        Checked = true;
    else
        Checked = false;

    GridSettings_MonthlyOperationGanttChartSchema.beginUpdate();
    for (var i = 0; i < GridSettings_MonthlyOperationGanttChartSchema.get_table().getRowCount(); i++) {
        GridSettings_MonthlyOperationGanttChartSchema.get_table().getRow(i).setValue(2, Checked, false);
    }
    GridSettings_MonthlyOperationGanttChartSchema.endUpdate();
}

function CallBack_GridSettings_MonthlyOperationGanttChartSchema_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_GridSettings_MonthlyOperationGanttChartSchema').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else {
        if (SettingsState_MonthlyOperationGanttChartSchema == 'Save') {
            DialogGanttChartSettings.Close();
            Fill_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema();
        }
    }
}

function CallBack_GridSettings_MonthlyOperationGanttChartSchema_onCallbackError(sender, e) {
    ShowConnectionError_MonthlyOperationGanttChartSchema();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDialogMonthlyOperationGanttChartSchema();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function ShowConnectionError_MonthlyOperationGanttChartSchema() {
    var error = document.getElementById('hfErrorType_MonthlyOperationGanttChartSchema').value;
    var errorBody = document.getElementById('hfConnectionError_MonthlyOperationGanttChartSchema').value;
    showDialog(error, errorBody, 'error');
}
















