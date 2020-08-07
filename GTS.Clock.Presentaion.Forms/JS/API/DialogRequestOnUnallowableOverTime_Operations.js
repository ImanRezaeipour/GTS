
var CurrenPageSate_DialogRequestOnUnallowableOverTime = 'View';
var ConfirmState_RequestOnUnallowableOverTime = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime = null;
var zeroTime = '00';

function initTimePickers_RequestOnUnallowableOverTime() {
    SetButtonImages_TimeSelectors_DialogRequestOnUnallowableOverTime();
    ChangeTimePickerEnabled_RequestOnUnallowableOverTime('TimeSelector_FromHour_RequestOnUnallowableOverTime', 'disable');
    ChangeTimePickerEnabled_RequestOnUnallowableOverTime('TimeSelector_ToHour_RequestOnUnallowableOverTime', 'disable');
    ResetTimepicker_RequestOnUnallowableOverTime('TimeSelector_FromHour_RequestOnUnallowableOverTime');
    ResetTimepicker_RequestOnUnallowableOverTime('TimeSelector_ToHour_RequestOnUnallowableOverTime');
}

function ChangeTimePickerEnabled_RequestOnUnallowableOverTime(TimeSelector, state) {
    var disabled = null;
    switch (state) {
        case 'disable':
            disabled = 'disabled';
            document.getElementById("" + TimeSelector + "_imgUp").onclick = " ";
            document.getElementById("" + TimeSelector + "_imgDown").onclick = " ";
            break;
        case 'enable':
            disabled = '';
            document.getElementById("" + TimeSelector + "_imgUp").onclick = function () {
                CheckTimePickerState_RequestOnUnallowableOverTime(TimeSelector + '_txtHour');
                CheckTimePickerState_RequestOnUnallowableOverTime(TimeSelector + '_txtMinute');
                addTime(document.getElementById("" + TimeSelector + "_imgUp"), 24, 1, 1);
            };
            document.getElementById("" + TimeSelector + "_imgDown").onclick = function () {
                CheckTimePickerState_RequestOnUnallowableOverTime(TimeSelector + '_txtHour');
                CheckTimePickerState_RequestOnUnallowableOverTime(TimeSelector + '_txtMinute');
                subtractTime(document.getElementById("" + TimeSelector + "_imgDown"), 24, 1, 1);
            };
            document.getElementById("" + TimeSelector + "_txtHour").onchange = function () {
                CheckTimeSelectorPartValue_RequestOnUnallowableOverTime(TimeSelector, '_txtHour');
            };
            document.getElementById("" + TimeSelector + "_txtMinute").onchange = function () {
                CheckTimeSelectorPartValue_RequestOnUnallowableOverTime(TimeSelector, '_txtMinute');
            };
            break;
    }
    document.getElementById("" + TimeSelector + "_txtHour").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtHour").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtSecond").disabled = disabled;
}

function CheckTimeSelectorPartValue_RequestOnUnallowableOverTime(TimeSelectorPartID, identifier) {
    if (document.getElementById(TimeSelectorPartID + identifier).value == "") {
        if (document.getElementById(TimeSelectorPartID + identifier).value == "")
            document.getElementById(TimeSelectorPartID + identifier).value = zeroTime;
    }
}

function HideElements_DialogRequestOnUnallowableOverTime() {
    document.getElementById('TimeSelector_FromHour_RequestOnUnallowableOverTime').style.visibility = 'hidden';
    document.getElementById('lblEntrance_RequestOnUnallowableOverTime').style.visibility = 'hidden';
    document.getElementById('TimeSelector_ToHour_RequestOnUnallowableOverTime').style.visibility = 'hidden';
    document.getElementById('lblExit_RequestOnUnallowableOverTime').style.visibility = 'hidden';
}

function GetBoxesHeaders_RequestOnUnallowableOverTime() {
    parent.document.getElementById('Title_DialogRequestOnUnallowableOverTime').innerHTML = document.getElementById('hfTitle_DialogRequestOnUnallowableOverTime').value;
    document.getElementById('header_OverTimeDetails_RequestOnUnallowableOverTime').innerHTML = document.getElementById('hfheader_OverTimeDetails_RequestOnUnallowableOverTime').value;
    document.getElementById('header_RegisteredRequests_RequestOnUnallowableOverTime').innerHTML = document.getElementById('hfheader_RegisteredRequests_RequestOnUnallowableOverTime').value;
    document.getElementById('cmbOverTimeType_RequestOnUnallowableOverTime_Input').value = document.getElementById('hfcmbAlarm_RequestOnUnallowableOverTime').value;
}

function SetButtonImages_TimeSelectors_DialogRequestOnUnallowableOverTime() {
    SetButtonImages_TimeSelector_DialogRequestOnUnallowableOverTime('TimeSelector_FromHour_RequestOnUnallowableOverTime');
    SetButtonImages_TimeSelector_DialogRequestOnUnallowableOverTime('TimeSelector_ToHour_RequestOnUnallowableOverTime');
}

function SetButtonImages_TimeSelector_DialogRequestOnUnallowableOverTime(TimeSelector) {
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
    if (document.activeElement.id != "" + TimeSelector + "_txtHour" && document.activeElement.id != "" + TimeSelector + "_txtMinute" && document.activeElement.id != "" + TimeSelector + "_txtSecond" && !document.getElementById("" + TimeSelector + "_txtHour").disabled)
        document.getElementById("" + TimeSelector + "_txtHour").focus();
}

function Set_SelectedDateTime_RequestOnUnallowableOverTime() {
    document.getElementById('tdSelectedDate_RequestOnUnallowableOverTime').innerHTML = parent.DialogRequestOnUnallowableOverTime.get_value().RequestDateTitle;
}

function tlbItemNew_TlbRequestOnUnallowableOverTime_onClick() {
    ChangePageState_DialogRequestOnUnallowableOverTime('Add');
}

function ChangePageState_DialogRequestOnUnallowableOverTime(state) {
    CurrenPageSate_DialogRequestOnUnallowableOverTime = state;
    SetActionMode_RequestOnUnallowableOverTime(state);
    if (CurrenPageSate_DialogRequestOnUnallowableOverTime == 'Add' || CurrenPageSate_DialogRequestOnUnallowableOverTime == 'Delete') {
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemNew_TlbRequestOnUnallowableOverTime').set_enabled(false);
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemNew_TlbRequestOnUnallowableOverTime').set_imageUrl('Add_silver.png');
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemDelete_TlbRequestOnUnallowableOverTime').set_enabled(false);
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemDelete_TlbRequestOnUnallowableOverTime').set_imageUrl('remove_silver.png');
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemSave_TlbRequestOnUnallowableOverTime').set_enabled(true);
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemSave_TlbRequestOnUnallowableOverTime').set_imageUrl('save.png');
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemCancel_TlbRequestOnUnallowableOverTime').set_enabled(true);
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemCancel_TlbRequestOnUnallowableOverTime').set_imageUrl('cancel.png');
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemExit_TlbRequestOnUnallowableOverTime').set_enabled(false);
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemExit_TlbRequestOnUnallowableOverTime').set_imageUrl('exit_silver.png');
        ChangeTimePickerEnabled_RequestOnUnallowableOverTime('TimeSelector_FromHour_RequestOnUnallowableOverTime', 'enable');
        ChangeTimePickerEnabled_RequestOnUnallowableOverTime('TimeSelector_ToHour_RequestOnUnallowableOverTime', 'enable');
        cmbOverTimeType_RequestOnUnallowableOverTime.enable();
        document.getElementById('txtDescription_RequestOnUnallowableOverTime').disabled = '';
        document.getElementById('chbToHourInNextDay_RequestOnUnallowableOverTime').disabled = '';
        if (state == 'Delete')
            RequestOnUnallowableOverTime_onSave();
    }
    if (CurrenPageSate_DialogRequestOnUnallowableOverTime == 'View') {
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemNew_TlbRequestOnUnallowableOverTime').set_enabled(true);
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemNew_TlbRequestOnUnallowableOverTime').set_imageUrl('Add.png');
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemDelete_TlbRequestOnUnallowableOverTime').set_enabled(true);
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemDelete_TlbRequestOnUnallowableOverTime').set_imageUrl('remove.png');
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemSave_TlbRequestOnUnallowableOverTime').set_enabled(false);
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemSave_TlbRequestOnUnallowableOverTime').set_imageUrl('save_silver.png');
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemCancel_TlbRequestOnUnallowableOverTime').set_enabled(false);
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemCancel_TlbRequestOnUnallowableOverTime').set_imageUrl('cancel_silver.png');
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemExit_TlbRequestOnUnallowableOverTime').set_enabled(true);
        TlbRequestOnUnallowableOverTime.get_items().getItemById('tlbItemExit_TlbRequestOnUnallowableOverTime').set_imageUrl('exit.png');
        cmbOverTimeType_RequestOnUnallowableOverTime.disable();
        ChangeTimePickerEnabled_RequestOnUnallowableOverTime('TimeSelector_FromHour_RequestOnUnallowableOverTime', 'disable');
        ChangeTimePickerEnabled_RequestOnUnallowableOverTime('TimeSelector_ToHour_RequestOnUnallowableOverTime', 'disable');
        document.getElementById('txtDescription_RequestOnUnallowableOverTime').disabled = 'disabled';
        document.getElementById('chbToHourInNextDay_RequestOnUnallowableOverTime').disabled = 'disabled';
    }
}

function RequestOnUnallowableOverTime_onSave() {
    if (CurrenPageSate_DialogRequestOnUnallowableOverTime != 'Delete')
        UpdateRequest_RequestOnUnallowableOverTime();
    else
        ShowDialogConfirm('Delete');
}

function UpdateRequest_RequestOnUnallowableOverTime() {
    ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime = new Object();
    ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.ID = '0';
    ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.PreCardID = '0';
    ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.PreCardTitle = null;
    ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.RequestDate = null;
    ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.FromTime = null;
    ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.ToTime = null;
    ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.IsToTimeInNextDay = false;
    ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.Description = null;

    var ObjDialogRequestOnUnallowableOverTime = parent.DialogRequestOnUnallowableOverTime.get_value();

    var SelectedItems_GridRegisteredRequests_RequestOnUnallowableOverTime = GridRegisteredRequests_RequestOnUnallowableOverTime.getSelectedItems();
    if (SelectedItems_GridRegisteredRequests_RequestOnUnallowableOverTime.length > 0)
        ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.ID = SelectedItems_GridRegisteredRequests_RequestOnUnallowableOverTime[0].getMember("ID").get_text();

    if (CurrenPageSate_DialogRequestOnUnallowableOverTime != 'Delete') {
        ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.RequestDate = parent.DialogRequestOnUnallowableOverTime.get_value().RequestDate;
        if (cmbOverTimeType_RequestOnUnallowableOverTime.getSelectedItem() != undefined) {
            ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.PreCardID = cmbOverTimeType_RequestOnUnallowableOverTime.getSelectedItem().get_value();
            ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.PreCardTitle = cmbOverTimeType_RequestOnUnallowableOverTime.getSelectedItem().get_text();
        }
        ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.FromTime = GetDuration_TimePicker_RequestOnUnallowableOverTime('TimeSelector_FromHour_RequestOnUnallowableOverTime');
        ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.ToTime = GetDuration_TimePicker_RequestOnUnallowableOverTime('TimeSelector_ToHour_RequestOnUnallowableOverTime');
        if (document.getElementById('chbToHourInNextDay_RequestOnUnallowableOverTime').checked)
            ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.IsToTimeInNextDay = true;
        ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.Description = document.getElementById('txtDescription_RequestOnUnallowableOverTime').value;
    }
    UpdateRequest_RequestOnUnallowableOverTimePage(CharToKeyCode_RequestOnUnallowableOverTime(ObjDialogRequestOnUnallowableOverTime.RequestCaller), CharToKeyCode_RequestOnUnallowableOverTime(ObjDialogRequestOnUnallowableOverTime.LoadState), CharToKeyCode_RequestOnUnallowableOverTime(CurrenPageSate_DialogRequestOnUnallowableOverTime), CharToKeyCode_RequestOnUnallowableOverTime(ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.ID), CharToKeyCode_RequestOnUnallowableOverTime(ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.PreCardID), CharToKeyCode_RequestOnUnallowableOverTime(ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.RequestDate), CharToKeyCode_RequestOnUnallowableOverTime(ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.FromTime), CharToKeyCode_RequestOnUnallowableOverTime(ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.ToTime), CharToKeyCode_RequestOnUnallowableOverTime(ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.IsToTimeInNextDay.toString()), CharToKeyCode_RequestOnUnallowableOverTime(ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.Description));
    DialogWaiting.Show();
}

function UpdateRequest_RequestOnUnallowableOverTimePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_RequestOnUnallowableOverTime').value;
            Response[1] = document.getElementById('hfConnectionError_RequestOnUnallowableOverTime').value;
        }
        if (RetMessage[2] == 'success') {
            RequestOnUnallowableOverTime_OnAfterUpdate(Response);
            ClearList_RequestOnUnallowableOverTime();
            ChangePageState_DialogRequestOnUnallowableOverTime('View');
        }
        else {
            if (CurrenPageSate_DialogRequestOnUnallowableOverTime == 'Delete')
                ChangePageState_DialogRequestOnUnallowableOverTime('View');
        }
    }
}

function RequestOnUnallowableOverTime_OnAfterUpdate(Response) {
    if (ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime != null) {
        var PreCardTitle = ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.PreCardTitle;
        var FromTime = ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.FromTime;
        var ToTime = ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.ToTime;
        var RegisterDate = '';
        var RequestState = '';
        var RequestStateTitle = '';

        if (ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.IsToTimeInNextDay)
            ToTime = '+' + ToTime;

        var RegisteredRequestItem = null;

        GridRegisteredRequests_RequestOnUnallowableOverTime.beginUpdate();
        switch (CurrenPageSate_DialogRequestOnUnallowableOverTime) {
            case 'Add':
                RegisteredRequestItem = GridRegisteredRequests_RequestOnUnallowableOverTime.get_table().addEmptyRow(GridRegisteredRequests_RequestOnUnallowableOverTime.get_recordCount());
                RegisteredRequestItem.setValue(0, Response[3], false);
                GridRegisteredRequests_RequestOnUnallowableOverTime.selectByKey(Response[3], 0, false);
                RequestStateTitle = GetRequestStateTitle_RequestOnUnallowableOverTime(Response[4]);
                RequestState = Response[4];
                RegisterDate = Response[5];
                break;
            case 'Delete':
                GridRegisteredRequests_RequestOnUnallowableOverTime.selectByKey(ObjRequestOnUnallowableOverTime_RequestOnUnallowableOverTime.ID, 0, false);
                GridRegisteredRequests_RequestOnUnallowableOverTime.deleteSelected();
                break;
        }
        if (CurrenPageSate_DialogRequestOnUnallowableOverTime != 'Delete') {
            RegisteredRequestItem.setValue(1, PreCardTitle, false);
            RegisteredRequestItem.setValue(2, FromTime, false);
            RegisteredRequestItem.setValue(3, ToTime, false);
            RegisteredRequestItem.setValue(4, RegisterDate, false);
            RegisteredRequestItem.setValue(5, RequestStateTitle, false);
            RegisteredRequestItem.setValue(6, RequestState, false);
        }
        GridRegisteredRequests_RequestOnUnallowableOverTime.endUpdate();
    }
}

function GetDuration_TimePicker_RequestOnUnallowableOverTime(TimePicker) {
    if (document.getElementById(TimePicker + '_txtHour').value.length < 2)
        document.getElementById(TimePicker + '_txtHour').value = '0' + document.getElementById(TimePicker + '_txtHour').value;
    if (document.getElementById(TimePicker + '_txtMinute').value.length < 2)
        document.getElementById(TimePicker + '_txtMinute').value = '0' + document.getElementById(TimePicker + '_txtMinute').value;
    return document.getElementById(TimePicker + '_txtHour').value + ':' + document.getElementById(TimePicker + '_txtMinute').value;
}


function ShowDialogConfirm(confirmState) {
    ConfirmState_RequestOnUnallowableOverTime = confirmState;
    if (CurrenPageSate_DialogRequestOnUnallowableOverTime == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_RequestOnUnallowableOverTime').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_RequestOnUnallowableOverTime').value;
    DialogConfirm.Show();
    CollapseControls_RequestOnUnallowableOverTime();
}

function SetActionMode_RequestOnUnallowableOverTime(state) {
    document.getElementById('ActionMode_RequestOnUnallowableOverTime').innerHTML = document.getElementById("hf" + state + "_RequestOnUnallowableOverTime").value;
}

function tlbItemDelete_TlbRequestOnUnallowableOverTime_onClick() {
    ChangePageState_DialogRequestOnUnallowableOverTime('Delete');
}

function tlbItemSave_TlbRequestOnUnallowableOverTime_onClick() {
    RequestOnUnallowableOverTime_onSave();
}

function tlbItemCancel_TlbRequestOnUnallowableOverTime_onClick() {
    ChangePageState_DialogRequestOnUnallowableOverTime('View');
    ClearList_RequestOnUnallowableOverTime();
}

function ClearList_RequestOnUnallowableOverTime() {
    document.getElementById('chbToHourInNextDay_RequestOnUnallowableOverTime').checked = false;
    document.getElementById('txtDescription_RequestOnUnallowableOverTime').value = '';
    ResetTimepicker_RequestOnUnallowableOverTime('TimeSelector_FromHour_RequestOnUnallowableOverTime');
    ResetTimepicker_RequestOnUnallowableOverTime('TimeSelector_ToHour_RequestOnUnallowableOverTime');
}

function ResetTimepicker_RequestOnUnallowableOverTime(TimePicker) {
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}

function tlbItemExit_TlbRequestOnUnallowableOverTime_onClick() {
    ShowDialogConfirm('Exit');
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_RequestOnUnallowableOverTime) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateRequest_RequestOnUnallowableOverTime();
            break;
        case 'Exit':
            DialogRequestOnUnallowableOverTime_onClose();
            break;
        default:
    }
}

function DialogRequestOnUnallowableOverTime_onClose() {
    parent.document.getElementById('DialogRequestOnUnallowableOverTime_IFrame').src = 'WhitePage.aspx';
    parent.DialogRequestOnUnallowableOverTime.Close();
    var ObjRequest = parent.DialogRequestOnUnallowableOverTime.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    if (RequestCaller == 'Grid')
        parent.parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();
}


function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_DialogRequestOnUnallowableOverTime('View');
}

function Refresh_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime() {
    Fill_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime();
}

function Fill_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime() {
    document.getElementById('loadingPanel_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime').innerHTML = document.getElementById('hfloadingPanel_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime').value;
    var ObjRequest = parent.DialogRequestOnUnallowableOverTime.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    var DateKey = ObjRequest.DateKey;
    var RequestDate = ObjRequest.RequestDate;
    CallBack_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime.callback(CharToKeyCode_RequestOnUnallowableOverTime(RequestCaller), CharToKeyCode_RequestOnUnallowableOverTime(DateKey), CharToKeyCode_RequestOnUnallowableOverTime(RequestDate));
}

function CharToKeyCode_RequestOnUnallowableOverTime(str) {
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

function GridUnallowableOverTimePairs_RequestOnUnallowableOverTime_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime').innerHTML = '';
}

function CallBack_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_UnallowableOverTimePairs').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime();
    }
}

function cmbOverTimeType_RequestOnUnallowableOverTime_onExpand(sender, e) {
    if (cmbOverTimeType_RequestOnUnallowableOverTime.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbOverTimeType_RequestOnUnallowableOverTime == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbOverTimeType_RequestOnUnallowableOverTime = true;
        CallBack_cmbOverTimeType_RequestOnUnallowableOverTime.callback();
    }
}

function cmbOverTimeType_RequestOnUnallowableOverTime_onCollapse(sender, e) {
    if (cmbOverTimeType_RequestOnUnallowableOverTime.getSelectedItem() == undefined)
        document.getElementById('cmbOverTimeType_RequestOnUnallowableOverTime_Input').value = document.getElementById('hfcmbAlarm_RequestOnUnallowableOverTime').value;
}

function CallBack_cmbOverTimeType_RequestOnUnallowableOverTime_onBeforeCallback(sender, e) {
    cmbOverTimeType_RequestOnUnallowableOverTime.dispose();
}

function CallBack_cmbOverTimeType_RequestOnUnallowableOverTime_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_OverTimeTypes').value;
    if (error == "") {
        document.getElementById('cmbOverTimeType_RequestOnUnallowableOverTime_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbOverTimeType_RequestOnUnallowableOverTime_DropImage').mousedown();
        else
            cmbOverTimeType_RequestOnUnallowableOverTime.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbOverTimeType_RequestOnUnallowableOverTime_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function Refresh_GridRegisteredRequests_RequestOnUnallowableOverTime() {
    Fill_GridRegisteredRequests_RequestOnUnallowableOverTime();
}

function Fill_GridRegisteredRequests_RequestOnUnallowableOverTime() {
    document.getElementById('loadingPanel_GridRegisteredRequests_RequestOnUnallowableOverTime').innerHTML = document.getElementById('hfloadingPanel_GridRegisteredRequests_RequestOnUnallowableOverTime').value;
    var ObjRequest = parent.DialogRequestOnUnallowableOverTime.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    var DateKey = ObjRequest.DateKey;
    var RequestDate = ObjRequest.RequestDate;
    CallBack_GridRegisteredRequests_RequestOnUnallowableOverTime.callback(CharToKeyCode_RequestOnUnallowableOverTime(RequestCaller), CharToKeyCode_RequestOnUnallowableOverTime(DateKey), CharToKeyCode_RequestOnUnallowableOverTime(RequestDate));
}

function GridRegisteredRequests_RequestOnUnallowableOverTime_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridRegisteredRequests_RequestOnUnallowableOverTime').innerHTML = '';
}

function CallBack_GridRegisteredRequests_RequestOnUnallowableOverTime_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RegisteredRequests').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridRegisteredRequests_RequestOnUnallowableOverTime();
    }
}

function GetRequestStateTitle_RequestOnUnallowableOverTime(requestState) {
    var RequestStates = document.getElementById('hfRequestStates_RequestOnUnallowableOverTime').value.split('#');
    for (var i = 0; i < RequestStates.length; i++) {
        var requestStateObj = RequestStates[i].split(':');
        if (requestStateObj.length > 1) {
            if (requestStateObj[1] == requestState.toString())
                return requestStateObj[0];
        }
    }
}

function RequestOnUnallowableOverTimeForm_onKeyDown(event) {
    var activeID = null;
    if (event.keyCode == 38 || event.keyCode == 40) {
        activeID = document.activeElement.id;
        CheckTimePickerState_RequestOnUnallowableOverTime(activeID);
    }
}

function CheckTimePickerState_RequestOnUnallowableOverTime(TimeSelector) {
    if (((TimeSelector == 'TimeSelector_FromHour_RequestOnUnallowableOverTime_txtHour' || TimeSelector == 'TimeSelector_ToHour_RequestOnUnallowableOverTime_txtHour') && (document.getElementById(TimeSelector).value == '-1' || isNaN(document.getElementById(TimeSelector).value))) || ((TimeSelector == 'TimeSelector_FromHour_RequestOnUnallowableOverTime_txtMinute' || TimeSelector == 'TimeSelector_ToHour_RequestOnUnallowableOverTime_txtMinute') && isNaN(document.getElementById(TimeSelector).value))) {
        document.getElementById(TimeSelector).value = zeroTime;
        return;
    }
}

function CallBack_cmbOverTimeType_RequestOnUnallowableOverTime_onCallbackError(sender, e) {
    ShowConnectionError_RequestOnUnallowableOverTime();
}

function ShowConnectionError_RequestOnUnallowableOverTime() {
    var error = document.getElementById('hfErrorType_RequestOnUnallowableOverTime').value;
    var errorBody = document.getElementById('hfConnectionError_RequestOnUnallowableOverTime').value;
    showDialog(error, errorBody, 'error');
}

function CallBack_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime_onCallbackError(sender, e) {
    ShowConnectionError_RequestOnUnallowableOverTime();
}

function CallBack_GridRegisteredRequests_RequestOnUnallowableOverTime_onCallbackError(sender, e) {
    ShowConnectionError_RequestOnUnallowableOverTime();
}

function CollapseControls_RequestOnUnallowableOverTime() {
    cmbOverTimeType_RequestOnUnallowableOverTime.collapse();
}

function tlbItemFormReconstruction_TlbRequestOnUnallowableOverTime_onClick() {
    var ObjDialogRequestOnUnallowableOverTime = parent.DialogRequestOnUnallowableOverTime.get_value();
    var field = ObjDialogRequestOnUnallowableOverTime.Field;
    DialogRequestOnUnallowableOverTime_onClose();
    parent.parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.ShowRelativeDialog_MasterMonthlyOperation(field);
}

function GridUnallowableOverTimePairs_RequestOnUnallowableOverTime_onItemSelect(sender, e) {
    NavigateOverTimePairs_RequestOnUnallowableOverTime(e.get_item());
}

function NavigateOverTimePairs_RequestOnUnallowableOverTime(selectedOverTimePairs) {
    if (selectedOverTimePairs != undefined) {
        var fromTime = selectedOverTimePairs.getMember('From').get_text();
        var toTime = selectedOverTimePairs.getMember('To').get_text();
        if (fromTime != '')
            SetDuration_TimePicker_RequestOnUnallowableOverTime('TimeSelector_FromHour_RequestOnUnallowableOverTime', fromTime);
        if (toTime != '')
            SetDuration_TimePicker_RequestOnUnallowableOverTime('TimeSelector_ToHour_RequestOnUnallowableOverTime', toTime);              
    }
}

function SetDuration_TimePicker_RequestOnUnallowableOverTime(TimePicker, strTime) {
    if (strTime == "")
        strTime = '00:00';
    var arTime_Shift = strTime.split(':');
    for (var i = 0; i < 2; i++) {
        if (arTime_Shift[i].length < 2)
            arTime_Shift[i] = '0' + arTime_Shift[i];
    }
    document.getElementById(TimePicker + '_txtHour').value = arTime_Shift[0];
    document.getElementById(TimePicker + '_txtMinute').value = arTime_Shift[1];
}

function tlbItemHelp_TlbRequestOnUnallowableOverTime_onClick() {
    LoadHelpPage('tlbItemHelp_TlbRequestOnUnallowableOverTime');
}












