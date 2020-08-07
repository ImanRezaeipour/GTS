
var CurrentPageCombosCallBcakStateObj = new Object();

function gdpFromDate_TrafficsTransfer_OnDateChange(sender, eventArgs) {
    var FromDate = gdpFromDate_TrafficsTransfer.getSelectedDate();
    gCalFromDate_TrafficsTransfer.setSelectedDate(FromDate);
}

function gCalFromDate_TrafficsTransfer_OnChange(sender, eventArgs) {
    var FromDate = gCalFromDate_TrafficsTransfer.getSelectedDate();
    gdpFromDate_TrafficsTransfer.setSelectedDate(FromDate);
}

function btn_gdpFromDate_TrafficsTransfer_OnClick(event) {
    if (gCalFromDate_TrafficsTransfer.get_popUpShowing()) {
        gCalFromDate_TrafficsTransfer.hide();
    }
    else {
        gCalFromDate_TrafficsTransfer.setSelectedDate(gdpFromDate_TrafficsTransfer.getSelectedDate());
        gCalFromDate_TrafficsTransfer.show();
    }
}

function btn_gdpFromDate_TrafficsTransfer_OnMouseUp(event) {
    if (gCalFromDate_TrafficsTransfer.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalFromDate_TrafficsTransfer_onLoad(sender, e) {
    window.gCalFromDate_TrafficsTransfer.PopUpObject.z = 25000000;
}

function gdpToDate_TrafficsTransfer_OnDateChange(sender, eventArgs) {
    var ToDate = gdpToDate_TrafficsTransfer.getSelectedDate();
    gCalToDate_TrafficsTransfer.setSelectedDate(ToDate);
}

function gCalToDate_TrafficsTransfer_OnChange(sender, eventArgs) {
    var ToDate = gCalToDate_TrafficsTransfer.getSelectedDate();
    gdpToDate_TrafficsTransfer.setSelectedDate(ToDate);
}

function btn_gdpToDate_TrafficsTransfer_OnClick(event) {
    if (gCalToDate_TrafficsTransfer.get_popUpShowing()) {
        gCalToDate_TrafficsTransfer.hide();
    }
    else {
        gCalToDate_TrafficsTransfer.setSelectedDate(gdpToDate_TrafficsTransfer.getSelectedDate());
        gCalToDate_TrafficsTransfer.show();
    }
}

function btn_gdpToDate_TrafficsTransfer_OnMouseUp(event) {
    if (gCalToDate_TrafficsTransfer.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalToDate_TrafficsTransfer_onLoad(sender, e) {
    window.gCalToDate_TrafficsTransfer.PopUpObject.z = 25000000;
}

function SetBoxesHeaders_TrafficsTransfer() {
    parent.document.getElementById('Title_DialogTrafficsTransfer').innerHTML = document.getElementById('hfTitle_DialogTrafficsTransfer').value;
}

function tlbItemSave_TlbTrafficsTransfer_onClick() {
    TrafficsTransferByConditions_TrafficsTransfer();
}

function TrafficsTransferByConditions_TrafficsTransfer() {
    var TrafficTransferMode = null;
    var TrafficTransferType = 'Backward';
    var MachineID = '0';
    var FromDate = null;
    var ToDate = null;
    var FromTime = null;
    var ToTime = null;
    var FromRecord = '0';
    var ToRecord = '0';
    var FromIdentifier = '0';
    var ToIdentifier = '0';
    var TransferDay = '0';
    var TransferTime = null;
    var TransferHour = '0';
    var TransferMinute = '0';
    var IsIntegralConditions = false;

    TrafficTransferMode = GetTrafficTransferMode_TrafficsTransfer();
    switch (TrafficTransferMode) {
        case 'RecordBase':
            IsIntegralConditions = document.getElementById('chbIntegratedConditionsOverRecordBase_TrafficsTransfer').checked;
            break;
        case 'IdentifierBase':
            IsIntegralConditions = document.getElementById('chbIntegratedConditionsOverIdentifierBase_TrafficsTransfer').checked;
            break;
    }
    if (cmbTrafficTransferType_TrafficsTransfer.getSelectedItem() != undefined)
        TrafficTransferType = cmbTrafficTransferType_TrafficsTransfer.getSelectedItem().get_value();
    if (cmbMachines_TrafficsTransfer.getSelectedItem() != undefined)
        MachineID = cmbMachines_TrafficsTransfer.getSelectedItem().get_id();
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            FromDate = document.getElementById('pdpFromDate_TrafficsTransfer').value;
            ToDate = document.getElementById('pdpToDate_TrafficsTransfer').value;
            break;
        case 'en-US':
            FromDate = document.getElementById('gdpFromDate_TrafficsTransfer_picker').value;
            ToDate = document.getElementById('gdpToDate_TrafficsTransfer_picker').value;
            break;
    }
    FromTime = GetDuration_TimePicker_TrafficsTransfer('TimeSelector_FromHour_TrafficsTransfer');
    ToTime = GetDuration_TimePicker_TrafficsTransfer('TimeSelector_ToHour_TrafficsTransfer');
    FromRecord = document.getElementById('txtFromRecord_TrafficsTransfer').value != null && document.getElementById('txtFromRecord_TrafficsTransfer').value != '' ? document.getElementById('txtFromRecord_TrafficsTransfer').value : '0';
    ToRecord = document.getElementById('txtToRecord_TrafficsTransfer').value != null && document.getElementById('txtToRecord_TrafficsTransfer').value != '' ? document.getElementById('txtToRecord_TrafficsTransfer').value : '0';
    FromIdentifier = document.getElementById('txtFromIdentifier_TrafficsTransfer').value != null && document.getElementById('txtFromIdentifier_TrafficsTransfer').value != '' ? document.getElementById('txtFromIdentifier_TrafficsTransfer').value : '0';
    ToIdentifier = document.getElementById('txtToIdentifier_TrafficsTransfer').value != null && document.getElementById('txtToIdentifier_TrafficsTransfer').value != '' ? document.getElementById('txtToIdentifier_TrafficsTransfer').value : '0';
    TransferDay = document.getElementById('txtDay_TrafficsTransfer').value != null && document.getElementById('txtDay_TrafficsTransfer').value != '' ? document.getElementById('txtDay_TrafficsTransfer').value : '00';
    TransferHour = document.getElementById('txtHour_TrafficsTransfer').value != null && document.getElementById('txtHour_TrafficsTransfer').value != '' ? document.getElementById('txtHour_TrafficsTransfer').value : '00';
    TransferMinute = document.getElementById('txtMinute_TrafficsTransfer').value != null && document.getElementById('txtMinute_TrafficsTransfer').value != '' ? document.getElementById('txtMinute_TrafficsTransfer').value : '00';
    TransferTime = TransferHour + ':' + TransferMinute;

    TrafficsTransferByConditions_TrafficsTransferPage(CharToKeyCode_TrafficsTransfer(TrafficTransferMode), CharToKeyCode_TrafficsTransfer(TrafficTransferType), CharToKeyCode_TrafficsTransfer(MachineID), CharToKeyCode_TrafficsTransfer(FromDate), CharToKeyCode_TrafficsTransfer(ToDate), CharToKeyCode_TrafficsTransfer(FromTime), CharToKeyCode_TrafficsTransfer(ToTime), CharToKeyCode_TrafficsTransfer(FromRecord), CharToKeyCode_TrafficsTransfer(ToRecord), CharToKeyCode_TrafficsTransfer(FromIdentifier), CharToKeyCode_TrafficsTransfer(ToIdentifier), CharToKeyCode_TrafficsTransfer(TransferDay), CharToKeyCode_TrafficsTransfer(TransferTime), CharToKeyCode_TrafficsTransfer(IsIntegralConditions.toString()));
    DialogWaiting.Show();
}

function TrafficsTransferByConditions_TrafficsTransferPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();        
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_TrafficsTransfer').value;
            Response[1] = document.getElementById('hfConnectionError_TrafficsTransfer').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function tlbItemExit_TlbTrafficsTransfer_onClick() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_TrafficsTransfer').value;
    DialogConfirm.Show();
}

function cmbMachines_TrafficsTransfer_onExpand(sender, e) {
    CollapseControls_TrafficsTransfer(cmbMachines_TrafficsTransfer);
    if (cmbMachines_TrafficsTransfer.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMachines_TrafficsTransfer == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMachines_TrafficsTransfer = true;
        CallBack_cmbMachines_TrafficsTransfer.callback();
    }
}

function cmbMachines_TrafficsTransfer_onCollapse(sender, e) {
    if (cmbMachines_TrafficsTransfer.getSelectedItem() == undefined)
        document.getElementById('cmbMachines_TrafficsTransfer_Input').value = document.getElementById('hfcmbAlarm_TrafficsTransfer').value;
}

function CallBack_cmbMachines_TrafficsTransfer_onBeforeCallback(sender, e) {
    cmbMachines_TrafficsTransfer.dispose();
}

function CallBack_cmbMachines_TrafficsTransfer_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Machines_TrafficsTransfer').value;
    if (error == "") {
        document.getElementById('cmbMachines_TrafficsTransfer_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMachines_TrafficsTransfer_DropImage').mousedown();
        cmbMachines_TrafficsTransfer.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbMachines_TrafficsTransfer_DropDown').style.display = 'none';
    }
}

function CallBack_cmbMachines_TrafficsTransfer_onCallbackError(sender, e) {
    ShowConnectionError_TrafficsTransfer();
}

function cmbTrafficTransferType_TrafficsTransfer_onExpand(sender, e) {
    CollapseControls_TrafficsTransfer(cmbTrafficTransferType_TrafficsTransfer);
    if (cmbTrafficTransferType_TrafficsTransfer.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbTrafficTransferType_TrafficsTransfer == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbTrafficTransferType_TrafficsTransfer = true;
        CallBack_cmbTrafficTransferType_TrafficsTransfer.callback();
    }
}

function cmbTrafficTransferType_TrafficsTransfer_onCollapse(sender, e) {
    if (cmbTrafficTransferType_TrafficsTransfer.getSelectedItem() == undefined) {
        var CurrentStateObj_TrafficsTransferMainInformation = parent.TrafficsTransfer.get_value();
        if (CurrentStateObj_TrafficsTransferMainInformation.MilitaryState != null && CurrentStateObj_TrafficsTransferMainInformation.MilitaryState != undefined)
            document.getElementById('cmbTrafficTransferType_TrafficsTransfer_Input').value = GetMilitaryState_TrafficsTransfer(CurrentStateObj_TrafficsTransferMainInformation.MilitaryState);
    }
}

function CallBack_cmbTrafficTransferType_TrafficsTransfer_onBeforeCallback(sender, e) {
    cmbTrafficTransferType_TrafficsTransfer.dispose();
}

function CallBack_cmbTrafficTransferType_TrafficsTransfer_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_TrafficTransferType_TrafficsTransfer').value;
    if (error == "") {
        document.getElementById('cmbTrafficTransferType_TrafficsTransfer_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbTrafficTransferType_TrafficsTransfer_DropImage').mousedown();
        cmbTrafficTransferType_TrafficsTransfer.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbTrafficTransferType_TrafficsTransfer_DropDown').style.display = 'none';
    }
}

function CallBack_cmbTrafficTransferType_TrafficsTransfer_onCallbackError(sender, e) {
    ShowConnectionError_TrafficsTransfer();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDialogTrafficsTransfer();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function ShowConnectionError_TrafficsTransfer() {
    var error = document.getElementById('hfErrorType_TrafficsTransfer').value;
    var errorBody = document.getElementById('hfConnectionError_TrafficsTransfer').value;
    showDialog(error, errorBody, 'error');
}

function CollapseControls_TrafficsTransfer(exception) {
    if (exception == null || exception != cmbMachines_TrafficsTransfer)
        cmbMachines_TrafficsTransfer.collapse();
    if (exception == null || exception != cmbTrafficTransferType_TrafficsTransfer)
        cmbTrafficTransferType_TrafficsTransfer.collapse();
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpFromDate_TrafficsTransfer');
}

function CloseDialogTrafficsTransfer() {
    parent.document.getElementById('DialogTrafficsTransfer_IFrame').src = 'WhitePage.aspx';
    parent.DialogTrafficsTransfer.Close();
}

function initTimePickers_TrafficsTransfer() {
    SetButtonImages_TimeSelectors_TrafficsTransfer();
    ChangeFloat_TimeSelectors_TrafficsTransfer();

    ChangeTimePickerFeatures_TrafficsTransfer('TimeSelector_FromHour_TrafficsTransfer');
    ChangeTimePickerFeatures_TrafficsTransfer('TimeSelector_ToHour_TrafficsTransfer');
    ResetTimepicker_TrafficsTransfer('TimeSelector_FromHour_TrafficsTransfer');
    ResetTimepicker_TrafficsTransfer('TimeSelector_ToHour_TrafficsTransfer');
}

function SetButtonImages_TimeSelectors_TrafficsTransfer() {
    SetButtonImages_TimeSelector_TrafficsTransfer('TimeSelector_FromHour_TrafficsTransfer');
    SetButtonImages_TimeSelector_TrafficsTransfer('TimeSelector_ToHour_TrafficsTransfer');
}

function SetButtonImages_TimeSelector_TrafficsTransfer(TimeSelector) {
    document.getElementById("" + TimeSelector + "_imgUp").src = "images/TimeSelector/CustomUp.gif";
    document.getElementById("" + TimeSelector + "_imgDown").src = "images/TimeSelector/CustomDown.gif";
    document.getElementById("" + TimeSelector + "_imgUp").onmouseover = function () {
        document.getElementById("" + TimeSelector + "_imgUp").src = "images/TimeSelector/oie_CustomUp.gif";
        FocusOnCurrentTimeSelector(TimeSelector);
    };
    document.getElementById("" + TimeSelector + "_imgDown").onmouseover = function () {
        document.getElementById("" + TimeSelector + "_imgDown").src = "images/TimeSelector/oie_CustomDown.gif";
        FocusOnCurrentTimeSelector(TimeSelector);
    };
    document.getElementById("" + TimeSelector + "_imgUp").onmouseout = function () {
        document.getElementById("" + TimeSelector + "_imgUp").src = "images/TimeSelector/CustomUp.gif";
    };
    document.getElementById("" + TimeSelector + "_imgDown").onmouseout = function () {
        document.getElementById("" + TimeSelector + "_imgDown").src = "images/TimeSelector/CustomDown.gif";
    };
}

function FocusOnCurrentTimeSelector(TimeSelector) {
    if (document.activeElement.id != "" + TimeSelector + "_txtHour" && document.activeElement.id != "" + TimeSelector + "_txtMinute" && document.activeElement.id != "" + TimeSelector + "_txtSecond")
        document.getElementById("" + TimeSelector + "_txtHour").focus();
}

function ChangeFloat_TimeSelectors_TrafficsTransfer() {
    switch (parent.parent.CurrentLangID) {
        case 'fa-IR':
            document.getElementById('TimeSelector_FromHour_TrafficsTransfer').style.styleFloat = 'right';
            document.getElementById('TimeSelector_FromHour_TrafficsTransfer').style.cssFloat = 'right';
            document.getElementById('TimeSelector_ToHour_TrafficsTransfer').style.styleFloat = 'right';
            document.getElementById('TimeSelector_ToHour_TrafficsTransfer').style.cssFloat = 'right';
            break;
        case 'en-US':
            document.getElementById('TimeSelector_FromHour_TrafficsTransfer').style.styleFloat = 'left';
            document.getElementById('TimeSelector_FromHour_TrafficsTransfer').style.cssFloat = 'left';
            document.getElementById('TimeSelector_ToHour_TrafficsTransfer').style.styleFloat = 'left';
            document.getElementById('TimeSelector_ToHour_TrafficsTransfer').style.cssFloat = 'left';
            break;
    }
}

function ChangeTimePickerFeatures_TrafficsTransfer(TimeSelector) {
    document.getElementById("" + TimeSelector + "_imgUp").onclick = function () {
        CheckTimePickerState_TrafficsTransfer(TimeSelector + '_txtHour');
        CheckTimePickerState_TrafficsTransfer(TimeSelector + '_txtMinute');
        addTime(document.getElementById("" + TimeSelector + "_imgUp"), 24, 1, 1);
    };
    document.getElementById("" + TimeSelector + "_imgDown").onclick = function () {
        CheckTimePickerState_TrafficsTransfer(TimeSelector + '_txtHour');
        CheckTimePickerState_TrafficsTransfer(TimeSelector + '_txtMinute');
        subtractTime(document.getElementById("" + TimeSelector + "_imgDown"), 24, 1, 1);
    };
}

function CheckTimePickerState_TrafficsTransfer(TimeSelector) {
    if (((TimeSelector == 'TimeSelector_WorkHeatMin_TrafficsTransfer_txtHour' || TimeSelector == 'TimeSelector_From_TrafficsTransferPairs_txtHour') && (document.getElementById(TimeSelector).value == '-1' || isNaN(document.getElementById(TimeSelector).value))) || ((TimeSelector == 'TimeSelector_WorkHeatMin_TrafficsTransfer_txtMinute' || TimeSelector == 'TimeSelector_From_TrafficsTransferPairs_txtMinute') && isNaN(document.getElementById(TimeSelector).value))) {
        document.getElementById(TimeSelector).value = zeroTime;
        return;
    }
}

function ResetTimepicker_TrafficsTransfer(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}

function txtRecordControl_TrafficsTransfer_onChange(Type) {
    var id = 'txt' + Type + 'Record_TrafficsTransfer';
    var val = document.getElementById(id).value;
    val = !isNaN(parseFloat(val)) ? parseFloat(val) > 0 ? "" + parseFloat(val) + "" : '00' : '00';
    document.getElementById(id).value = val.length >= 2 ? val : '0' + val;
}

function txtIdentifierControl_TrafficsTransfer_onChange(Type) {
    var id = 'txt' + Type + 'Identifier_TrafficsTransfer';
    var val = document.getElementById(id).value;
    val = !isNaN(parseFloat(val)) ? parseFloat(val) > 0 ? "" + parseFloat(val) + "" : '0' : '0';
}

function TransferFeaturesControl_TrafficsTransfer_onChange(Type) {
    var id = 'txt' + Type + '_TrafficsTransfer';
    var val = document.getElementById(id).value;
    val = !isNaN(parseFloat(val)) ? parseFloat(val) > 0 ? "" + parseFloat(val) + "" : '00' : '00';
    switch (Type) {
        case 'Day':
            break;
        case 'Hour':
            val = parseFloat(val) < 24 ? val : '23';
            break;
        case 'Minute':
            val = parseFloat(val) < 60 ? val : '59';
            break;
    }
    document.getElementById(id).value = val.length >= 2 ? val : '0' + val;
}

function rdbNormalTransfer_TrafficsTransfer_onClick() {
    ChangeTransferTrafficsModeControlsEnabled_TrafficsTransfer('Normal');
}

function rdbRecordeBaseTransfer_TrafficsTransfer_onClick() {
    ChangeTransferTrafficsModeControlsEnabled_TrafficsTransfer('RecordBase');
}

function rdbIdentifierBaseTransfer_TrafficsTransfer_onClick() {
    ChangeTransferTrafficsModeControlsEnabled_TrafficsTransfer('IdentifierBase');
}

function ChangeTransferTrafficsModeControlsEnabled_TrafficsTransfer(Type, State) {
    var RecordBaseDisabled = null;
    var IdentifierBaseDisabled = null;
    switch (Type) {
        case 'Normal':
            RecordBaseDisabled = true;
            IdentifierBaseDisabled = true;
            break;
        case 'RecordBase':
            RecordBaseDisabled = false;
            IdentifierBaseDisabled = true;
            break;
        case 'IdentifierBase':
            RecordBaseDisabled = true;
            IdentifierBaseDisabled = false;
            break;
    }
    document.getElementById('chbIntegratedConditionsOverRecordBase_TrafficsTransfer').disabled = RecordBaseDisabled;
    document.getElementById('txtFromRecord_TrafficsTransfer').disabled = RecordBaseDisabled;
    document.getElementById('txtToRecord_TrafficsTransfer').disabled = RecordBaseDisabled;
    document.getElementById('chbIntegratedConditionsOverIdentifierBase_TrafficsTransfer').disabled = IdentifierBaseDisabled;
    document.getElementById('txtFromIdentifier_TrafficsTransfer').disabled = IdentifierBaseDisabled;
    document.getElementById('txtToIdentifier_TrafficsTransfer').disabled = IdentifierBaseDisabled;
}

function ResetCalendars_TrafficsTransfer() {
    var strCrurrentDate = document.getElementById('hfCurrentDate_TrafficsTransfer').value;
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_TrafficsTransfer').value = document.getElementById('pdpToDate_TrafficsTransfer').value = strCrurrentDate;
            break;
        case 'en-US':
            currentDate_TrafficsTransfer = new Date(strCrurrentDate);
            gdpFromDate_TrafficsTransfer.setSelectedDate(currentDate_TrafficsTransfer);
            gdpToDate_TrafficsTransfer.setSelectedDate(currentDate_TrafficsTransfer);
            break;
    }
}

function tlbItemHelp_TlbTrafficsTransfer_onClick() {
    LoadHelpPage('tlbItemHelp_TlbTrafficsTransfer');
}

function GetTrafficTransferMode_TrafficsTransfer() {
    var TrafficTransferMode = null;
    if (document.getElementById("rdbNormalTransfer_TrafficsTransfer").checked)
        TrafficTransferMode = 'Normal';
    if (document.getElementById('rdbRecordeBaseTransfer_TrafficsTransfer').checked)
        TrafficTransferMode = 'RecordBase';
    if (document.getElementById('rdbIdentifierBaseTransfer_TrafficsTransfer').checked)
        TrafficTransferMode = 'IdentifierBase';
    return TrafficTransferMode;
}

function CharToKeyCode_TrafficsTransfer(str) {
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

function GetDuration_TimePicker_TrafficsTransfer(TimePicker) {
    if (document.getElementById(TimePicker + '_txtHour').value.length < 2)
        document.getElementById(TimePicker + '_txtHour').value = '0' + document.getElementById(TimePicker + '_txtHour').value;
    if (document.getElementById(TimePicker + '_txtMinute').value.length < 2)
        document.getElementById(TimePicker + '_txtMinute').value = '0' + document.getElementById(TimePicker + '_txtMinute').value;
    return document.getElementById(TimePicker + '_txtHour').value + ':' + document.getElementById(TimePicker + '_txtMinute').value;
}

function tlbItemFormReconstruction_TlbTrafficsTransfer_onClick() {
    CloseDialogTrafficsTransfer();
    parent.document.getElementById('pgvTrafficsControl_iFrame').contentWindow.ShowDialogTrafficsTransfer();
}

















