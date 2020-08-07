
var CurrenPageSate_DialogRequestOnTraffic = 'Add';
var ConfirmState_RequestOnTraffic = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjRequestOnTraffic_RequestOnTraffic = null;
var NullTime_RequestOnTraffic = '';
var zeroTime = '00';

function initTimePickers_RequestOnTraffic() {
    SetButtonImages_TimeSelectors_DialogRequestOnTraffic();
    ChangeTimePickerEnabled_RequestOnTraffic('TimeSelector_Entrance_RequestOnTraffic', 'enable');
    ChangeTimePickerEnabled_RequestOnTraffic('TimeSelector_Exit_RequestOnTraffic', 'enable');
    ResetTimepicker_RequestOnTraffic('TimeSelector_Entrance_RequestOnTraffic');
    ResetTimepicker_RequestOnTraffic('TimeSelector_Exit_RequestOnTraffic');
}

function ChangeTimePickerEnabled_RequestOnTraffic(TimeSelector, state) {
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
                CheckTimePickerState_RequestOnTraffic(TimeSelector + '_txtHour');
                CheckTimePickerState_RequestOnTraffic(TimeSelector + '_txtMinute');
                addTime(document.getElementById("" + TimeSelector + "_imgUp"), 24, 1, 1);
            };
            document.getElementById("" + TimeSelector + "_imgDown").onclick = function () {
                CheckTimePickerState_RequestOnTraffic(TimeSelector + '_txtHour');
                CheckTimePickerState_RequestOnTraffic(TimeSelector + '_txtMinute');
                subtractTime(document.getElementById("" + TimeSelector + "_imgDown"), 24, 1, 1);
            };
            document.getElementById("" + TimeSelector + "_txtHour").onchange = function () {
                CheckTimeSelectorPartValue_RequestOnTraffic(TimeSelector, '_txtHour');
            };
            document.getElementById("" + TimeSelector + "_txtMinute").onchange = function () {
                CheckTimeSelectorPartValue_RequestOnTraffic(TimeSelector, '_txtMinute');
            };
            break;
    }
    document.getElementById("" + TimeSelector + "_txtHour").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtHour").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtSecond").disabled = disabled;
}

function CheckTimeSelectorPartValue_RequestOnTraffic(TimeSelectorPartID, identifier) {
    if (document.getElementById(TimeSelectorPartID + identifier).value == "") {
        switch (identifier) {
            case '_txtHour':
                document.getElementById(TimeSelectorPartID + identifier).value = NullTime_RequestOnTraffic;
                break;
            case '_txtMinute':
                document.getElementById(TimeSelectorPartID + identifier).value = zeroTime;
                intOnly(document.getElementById(TimeSelectorPartID + identifier), 24);
                break;
        }
    }
    else {
        intOnly(document.getElementById(TimeSelectorPartID + identifier), 24);
    }
}


function HideElements_DialogRequestOnTraffic() {
    document.getElementById('TimeSelector_Entrance_RequestOnTraffic').style.visibility = 'hidden';
    document.getElementById('lblEntrance_RequestOnTraffic').style.visibility = 'hidden';
    document.getElementById('TimeSelector_Exit_RequestOnTraffic').style.visibility = 'hidden';
    document.getElementById('lblExit_RequestOnTraffic').style.visibility = 'hidden';
}

function GetBoxesHeaders_RequestOnTraffic() {
    parent.document.getElementById('Title_DialogRequestOnTraffic').innerHTML = document.getElementById('hfTitle_DialogRequestOnTraffic').value;
    document.getElementById('header_TrafficDetails_RequestOnTraffic').innerHTML = document.getElementById('hfheader_TrafficDetails_RequestOnTraffic').value;
    document.getElementById('header_RegisteredRequests_RequestOnTraffic').innerHTML = document.getElementById('hfheader_RegisteredRequests_RequestOnTraffic').value;
    document.getElementById('cmbTrafficType_RequestOnTraffic_Input').value = document.getElementById('hfcmbAlarm_RequestOnTraffic').value;
}

function ChangeControlsDirection_DialogRequestOnTraffic() {
    var direction = null;
    switch (parent.parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    document.getElementById('tblPairsContainer_RequestOnTraffics').dir = direction;
}

function SetButtonImages_TimeSelectors_DialogRequestOnTraffic() {
    SetButtonImages_TimeSelector_DialogRequestOnTraffic('TimeSelector_Entrance_RequestOnTraffic');
    SetButtonImages_TimeSelector_DialogRequestOnTraffic('TimeSelector_Exit_RequestOnTraffic');
}

function SetButtonImages_TimeSelector_DialogRequestOnTraffic(TimeSelector) {
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

function Set_SelectedDateTime_RequestOnTraffic() {
    document.getElementById('tdSelectedDate_RequestOnTraffic').innerHTML = parent.DialogRequestOnTraffic.get_value().RequestDateTitle;
}

function tlbItemNew_TlbRequestOnTraffic_onClick() {
    ChangePageState_DialogRequestOnTraffic('Add');
}

function ChangePageState_DialogRequestOnTraffic(state) {
    CurrenPageSate_DialogRequestOnTraffic = state;
    SetActionMode_RequestOnTraffic(state);
    if (CurrenPageSate_DialogRequestOnTraffic == 'Add' || CurrenPageSate_DialogRequestOnTraffic == 'Delete') {
        TlbRequestOnTraffic.get_items().getItemById('tlbItemNew_TlbRequestOnTraffic').set_enabled(false);
        TlbRequestOnTraffic.get_items().getItemById('tlbItemNew_TlbRequestOnTraffic').set_imageUrl('Add_silver.png');
        TlbRequestOnTraffic.get_items().getItemById('tlbItemDelete_TlbRequestOnTraffic').set_enabled(false);
        TlbRequestOnTraffic.get_items().getItemById('tlbItemDelete_TlbRequestOnTraffic').set_imageUrl('remove_silver.png');
        TlbRequestOnTraffic.get_items().getItemById('tlbItemSave_TlbRequestOnTraffic').set_enabled(true);
        TlbRequestOnTraffic.get_items().getItemById('tlbItemSave_TlbRequestOnTraffic').set_imageUrl('save.png');
        TlbRequestOnTraffic.get_items().getItemById('tlbItemCancel_TlbRequestOnTraffic').set_enabled(true);
        TlbRequestOnTraffic.get_items().getItemById('tlbItemCancel_TlbRequestOnTraffic').set_imageUrl('cancel.png');
        TlbRequestOnTraffic.get_items().getItemById('tlbItemExit_TlbRequestOnTraffic').set_enabled(false);
        TlbRequestOnTraffic.get_items().getItemById('tlbItemExit_TlbRequestOnTraffic').set_imageUrl('exit_silver.png');
        ChangeTimePickerEnabled_RequestOnTraffic('TimeSelector_Entrance_RequestOnTraffic', 'enable');
        ChangeTimePickerEnabled_RequestOnTraffic('TimeSelector_Exit_RequestOnTraffic', 'enable');
        cmbTrafficType_RequestOnTraffic.enable();
        document.getElementById('chbExitInNextDay_RequestOnTraffic').disabled = '';
        document.getElementById('txtDescription_RequestOnTraffic').disabled = '';
        if (state == 'Delete')
            RequestOnTraffic_onSave();
    }
    if (CurrenPageSate_DialogRequestOnTraffic == 'View') {
        TlbRequestOnTraffic.get_items().getItemById('tlbItemNew_TlbRequestOnTraffic').set_enabled(true);
        TlbRequestOnTraffic.get_items().getItemById('tlbItemNew_TlbRequestOnTraffic').set_imageUrl('Add.png');
        TlbRequestOnTraffic.get_items().getItemById('tlbItemDelete_TlbRequestOnTraffic').set_enabled(true);
        TlbRequestOnTraffic.get_items().getItemById('tlbItemDelete_TlbRequestOnTraffic').set_imageUrl('remove.png');
        TlbRequestOnTraffic.get_items().getItemById('tlbItemSave_TlbRequestOnTraffic').set_enabled(false);
        TlbRequestOnTraffic.get_items().getItemById('tlbItemSave_TlbRequestOnTraffic').set_imageUrl('save_silver.png');
        TlbRequestOnTraffic.get_items().getItemById('tlbItemCancel_TlbRequestOnTraffic').set_enabled(false);
        TlbRequestOnTraffic.get_items().getItemById('tlbItemCancel_TlbRequestOnTraffic').set_imageUrl('cancel_silver.png');
        TlbRequestOnTraffic.get_items().getItemById('tlbItemExit_TlbRequestOnTraffic').set_enabled(true);
        TlbRequestOnTraffic.get_items().getItemById('tlbItemExit_TlbRequestOnTraffic').set_imageUrl('exit.png');
        cmbTrafficType_RequestOnTraffic.disable();
        ChangeTimePickerEnabled_RequestOnTraffic('TimeSelector_Entrance_RequestOnTraffic', 'disable');
        ChangeTimePickerEnabled_RequestOnTraffic('TimeSelector_Exit_RequestOnTraffic', 'disable');
        document.getElementById('chbExitInNextDay_RequestOnTraffic').disabled = 'disabled';
        document.getElementById('txtDescription_RequestOnTraffic').disabled = 'disabled';
    }
}

function RequestOnTraffic_onSave() {
    if (CurrenPageSate_DialogRequestOnTraffic != 'Delete')
        UpdateRequest_RequestOnTraffic();
    else
        ShowDialogConfirm('Delete');
}

function UpdateRequest_RequestOnTraffic() {
    ObjRequestOnTraffic_RequestOnTraffic = new Object();
    ObjRequestOnTraffic_RequestOnTraffic.ID = '0';
    ObjRequestOnTraffic_RequestOnTraffic.PreCardID = '0';
    ObjRequestOnTraffic_RequestOnTraffic.PreCardTitle = null;
    ObjRequestOnTraffic_RequestOnTraffic.RequestDate = null;
    ObjRequestOnTraffic_RequestOnTraffic.FromTime = null;
    ObjRequestOnTraffic_RequestOnTraffic.ToTime = null;
    ObjRequestOnTraffic_RequestOnTraffic.IsToTimeInNextDay = false;
    ObjRequestOnTraffic_RequestOnTraffic.Description = null;

    var ObjDialogRequestOnTraffic = parent.DialogRequestOnTraffic.get_value();

    var SelectedItems_GridRegisteredRequests_RequestOnTraffic = GridRegisteredRequests_RequestOnTraffic.getSelectedItems();
    if (SelectedItems_GridRegisteredRequests_RequestOnTraffic.length > 0)
        ObjRequestOnTraffic_RequestOnTraffic.ID = SelectedItems_GridRegisteredRequests_RequestOnTraffic[0].getMember("ID").get_text();

    if (CurrenPageSate_DialogRequestOnTraffic != 'Delete') {
        ObjRequestOnTraffic_RequestOnTraffic.RequestDate = parent.DialogRequestOnTraffic.get_value().RequestDate;
        if (cmbTrafficType_RequestOnTraffic.getSelectedItem() != undefined) {
            ObjRequestOnTraffic_RequestOnTraffic.PreCardID = cmbTrafficType_RequestOnTraffic.getSelectedItem().get_value();
            ObjRequestOnTraffic_RequestOnTraffic.PreCardTitle = cmbTrafficType_RequestOnTraffic.getSelectedItem().get_text();
        }
        else {
            ObjRequestOnTraffic_RequestOnTraffic.PreCardID = '-1';
            ObjRequestOnTraffic_RequestOnTraffic.PreCardTitle = document.getElementById('hfcmbAlarm_RequestOnTraffic').value;
        }
        ObjRequestOnTraffic_RequestOnTraffic.FromTime = GetDuration_TimePicker_RequestOnTraffic('TimeSelector_Entrance_RequestOnTraffic');
        ObjRequestOnTraffic_RequestOnTraffic.ToTime = GetDuration_TimePicker_RequestOnTraffic('TimeSelector_Exit_RequestOnTraffic');
        if (document.getElementById('chbExitInNextDay_RequestOnTraffic').checked)
            ObjRequestOnTraffic_RequestOnTraffic.IsToTimeInNextDay = true;
        ObjRequestOnTraffic_RequestOnTraffic.Description = document.getElementById('txtDescription_RequestOnTraffic').value;
    }
    UpdateRequest_RequestOnTrafficPage(CharToKeyCode_RequestOnTraffic(ObjDialogRequestOnTraffic.RequestCaller), CharToKeyCode_RequestOnTraffic(ObjDialogRequestOnTraffic.LoadState), CharToKeyCode_RequestOnTraffic(CurrenPageSate_DialogRequestOnTraffic), CharToKeyCode_RequestOnTraffic(ObjRequestOnTraffic_RequestOnTraffic.ID), CharToKeyCode_RequestOnTraffic(ObjRequestOnTraffic_RequestOnTraffic.PreCardID), CharToKeyCode_RequestOnTraffic(ObjRequestOnTraffic_RequestOnTraffic.RequestDate), CharToKeyCode_RequestOnTraffic(ObjRequestOnTraffic_RequestOnTraffic.FromTime), CharToKeyCode_RequestOnTraffic(ObjRequestOnTraffic_RequestOnTraffic.ToTime), CharToKeyCode_RequestOnTraffic(ObjRequestOnTraffic_RequestOnTraffic.IsToTimeInNextDay.toString()), CharToKeyCode_RequestOnTraffic(ObjRequestOnTraffic_RequestOnTraffic.Description));
    DialogWaiting.Show();
}

function UpdateRequest_RequestOnTrafficPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_RequestOnTraffic').value;
            Response[1] = document.getElementById('hfConnectionError_RequestOnTraffic').value;
        }
        if (RetMessage[2] == 'success') {
            RequestOnTraffic_OnAfterUpdate(Response);
            ClearList_RequestOnTraffic();
            ChangePageState_DialogRequestOnTraffic('View');
        }
        else {
            if (CurrenPageSate_DialogRequestOnTraffic == 'Delete')
                ChangePageState_DialogRequestOnTraffic('View');
        }
    }
}

function RequestOnTraffic_OnAfterUpdate(Response) {
    if (ObjRequestOnTraffic_RequestOnTraffic != null) {
        var PreCardTitle = ObjRequestOnTraffic_RequestOnTraffic.PreCardTitle;
        var FromTime = ObjRequestOnTraffic_RequestOnTraffic.FromTime;
        var ToTime = ObjRequestOnTraffic_RequestOnTraffic.ToTime;
        var RegisterDate = '';
        var RequestState = '';
        var RequestStateTitle = '';

        if (ObjRequestOnTraffic_RequestOnTraffic.IsToTimeInNextDay)
            ToTime = '+' + ToTime;

        var RegisteredRequestItem = null;

        GridRegisteredRequests_RequestOnTraffic.beginUpdate();
        switch (CurrenPageSate_DialogRequestOnTraffic) {
            case 'Add':
                RegisteredRequestItem = GridRegisteredRequests_RequestOnTraffic.get_table().addEmptyRow(GridRegisteredRequests_RequestOnTraffic.get_recordCount());
                RegisteredRequestItem.setValue(0, Response[3], false);
                GridRegisteredRequests_RequestOnTraffic.selectByKey(Response[3], 0, false);
                RequestStateTitle = GetRequestStateTitle_RequestOnTraffic(Response[4]);
                RequestState = Response[4];
                RegisterDate = Response[5];
                break;
            case 'Delete':
                GridRegisteredRequests_RequestOnTraffic.selectByKey(ObjRequestOnTraffic_RequestOnTraffic.ID, 0, false);
                GridRegisteredRequests_RequestOnTraffic.deleteSelected();
                break;
        }
        if (CurrenPageSate_DialogRequestOnTraffic != 'Delete') {
            RegisteredRequestItem.setValue(1, PreCardTitle, false);
            RegisteredRequestItem.setValue(2, FromTime, false);
            RegisteredRequestItem.setValue(3, ToTime, false);
            RegisteredRequestItem.setValue(4, RegisterDate, false);
            RegisteredRequestItem.setValue(5, RequestStateTitle, false);
            RegisteredRequestItem.setValue(6, RequestState, false);
        }
        GridRegisteredRequests_RequestOnTraffic.endUpdate();
    }
}

function GetDuration_TimePicker_RequestOnTraffic(TimePicker) {
    if (document.getElementById(TimePicker + '_txtHour').value == NullTime_RequestOnTraffic)
        return "";
    var hour = document.getElementById(TimePicker + '_txtHour').value;
    var minute = document.getElementById(TimePicker + '_txtMinute').value;
    if (parseFloat(hour) < 0)
        document.getElementById(TimePicker + '_txtHour').value = zeroTime;
    if (parseFloat(minute) < 0)
        document.getElementById(TimePicker + '_txtMinute').value = zeroTime;
    if (document.getElementById(TimePicker + '_txtHour').value.length < 2)
        document.getElementById(TimePicker + '_txtHour').value = '0' + document.getElementById(TimePicker + '_txtHour').value;
    if (document.getElementById(TimePicker + '_txtMinute').value.length < 2)
        document.getElementById(TimePicker + '_txtMinute').value = '0' + document.getElementById(TimePicker + '_txtMinute').value;
    return document.getElementById(TimePicker + '_txtHour').value + ':' + document.getElementById(TimePicker + '_txtMinute').value;
}


function ShowDialogConfirm(confirmState) {
    ConfirmState_RequestOnTraffic = confirmState;
    if (CurrenPageSate_DialogRequestOnTraffic == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_RequestOnTraffic').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_RequestOnTraffic').value;
    DialogConfirm.Show();
    CollapseControls_RequestOnTraffic();
}

function SetActionMode_RequestOnTraffic(state) {
    document.getElementById('ActionMode_RequestOnTraffic').innerHTML = document.getElementById("hf" + state + "_RequestOnTraffic").value;
}

function tlbItemDelete_TlbRequestOnTraffic_onClick() {
    ChangePageState_DialogRequestOnTraffic('Delete');
}

function tlbItemSave_TlbRequestOnTraffic_onClick() {
    RequestOnTraffic_onSave();
}

function tlbItemCancel_TlbRequestOnTraffic_onClick() {
    ChangePageState_DialogRequestOnTraffic('View');
    ClearList_RequestOnTraffic();
}

function ClearList_RequestOnTraffic() {
    document.getElementById('chbExitInNextDay_RequestOnTraffic').checked = false;
    document.getElementById('txtDescription_RequestOnTraffic').value = '';
    ResetTimepicker_RequestOnTraffic('TimeSelector_Entrance_RequestOnTraffic');
    ResetTimepicker_RequestOnTraffic('TimeSelector_Exit_RequestOnTraffic');
}

function ResetTimepicker_RequestOnTraffic(TimePicker) {
    document.getElementById(TimePicker + "_txtHour").value = NullTime_RequestOnTraffic;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}

function tlbItemExit_TlbRequestOnTraffic_onClick() {
    ShowDialogConfirm('Exit');
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_RequestOnTraffic) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateRequest_RequestOnTraffic();
            break;
        case 'Exit':
            DialogRequestOnTraffic_onClose();
            break;
        default:
    }
}

function DialogRequestOnTraffic_onClose() {
    parent.document.getElementById('DialogRequestOnTraffic_IFrame').src = 'WhitePage.aspx';
    parent.DialogRequestOnTraffic.Close();
    var ObjRequest = parent.DialogRequestOnTraffic.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    if (RequestCaller == 'Grid')
        parent.parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();
}


function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_DialogRequestOnTraffic('View');
}

function Refresh_GridTrafficPairs_RequestOnTraffic() {
    Fill_GridTrafficPairs_RequestOnTraffic();
}

function Fill_GridTrafficPairs_RequestOnTraffic() {
    document.getElementById('loadingPanel_GridTrafficPairs_RequestOnTraffic').innerHTML = document.getElementById('hfloadingPanel_GridTrafficPairs_RequestOnTraffic').value;
    var ObjRequest = parent.DialogRequestOnTraffic.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    var DateKey = ObjRequest.DateKey;
    var RequestDate = ObjRequest.RequestDate;
    CallBack_GridTrafficPairs_RequestOnTraffic.callback(CharToKeyCode_RequestOnTraffic(RequestCaller), CharToKeyCode_RequestOnTraffic(DateKey), CharToKeyCode_RequestOnTraffic(RequestDate));
}

function CharToKeyCode_RequestOnTraffic(str) {
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

function GridTrafficPairs_RequestOnTraffic_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridTrafficPairs_RequestOnTraffic').innerHTML = '';
}

function CallBack_GridTrafficPairs_RequestOnTraffic_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_TrafficPairs').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridTrafficPairs_RequestOnTraffic();
    }
}

function cmbTrafficType_RequestOnTraffic_onChange(sender, e) {
    //    if (cmbTrafficType_RequestOnTraffic.getSelectedItem() != undefined) {
    //        switch (cmbTrafficType_RequestOnTraffic.getSelectedItem().get_id()) {
    //            case '0':
    //                document.getElementById('TimeSelector_Entrance_RequestOnTraffic').style.visibility = 'visible';
    //                document.getElementById('lblEntrance_RequestOnTraffic').style.visibility = 'visible';
    //                document.getElementById('TimeSelector_Exit_RequestOnTraffic').style.visibility = 'hidden';
    //                document.getElementById('lblExit_RequestOnTraffic').style.visibility = 'hidden';
    //                break;
    //            case '1':
    //                document.getElementById('TimeSelector_Entrance_RequestOnTraffic').style.visibility = 'hidden';
    //                document.getElementById('lblEntrance_RequestOnTraffic').style.visibility = 'hidden';
    //                document.getElementById('TimeSelector_Exit_RequestOnTraffic').style.visibility = 'visible';
    //                document.getElementById('lblExit_RequestOnTraffic').style.visibility = 'visible';
    //                break;
    //            case '2':
    //                document.getElementById('TimeSelector_Entrance_RequestOnTraffic').style.visibility = 'visible';
    //                document.getElementById('lblEntrance_RequestOnTraffic').style.visibility = 'visible';
    //                document.getElementById('TimeSelector_Exit_RequestOnTraffic').style.visibility = 'visible';
    //                document.getElementById('lblExit_RequestOnTraffic').style.visibility = 'visible';
    //                break;
    //        }
    //    }
}

function cmbTrafficType_RequestOnTraffic_onExpand(sender, e) {
    if (cmbTrafficType_RequestOnTraffic.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbTrafficType_RequestOnTraffic == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbTrafficType_RequestOnTraffic = true;
        CallBack_cmbTrafficType_RequestOnTraffic.callback();
    }
}

function cmbTrafficType_RequestOnTraffic_onCollapse(sender, e) {
    if (cmbTrafficType_RequestOnTraffic.getSelectedItem() == undefined)
        document.getElementById('cmbTrafficType_RequestOnTraffic_Input').value = document.getElementById('hfcmbAlarm_RequestOnTraffic').value;
}

function CallBack_cmbTrafficType_RequestOnTraffic_onBeforeCallback(sender, e) {
    cmbTrafficType_RequestOnTraffic.dispose();
}

function CallBack_cmbTrafficType_RequestOnTraffic_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_TrafficTypes').value;
    if (error == "") {
        document.getElementById('cmbTrafficType_RequestOnTraffic_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbTrafficType_RequestOnTraffic_DropImage').mousedown();
        cmbTrafficType_RequestOnTraffic.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbTrafficType_RequestOnTraffic_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function Refresh_GridRegisteredRequests_RequestOnTraffic() {
    Fill_GridRegisteredRequests_RequestOnTraffic();
}

function Fill_GridRegisteredRequests_RequestOnTraffic() {
    document.getElementById('loadingPanel_GridRegisteredRequests_RequestOnTraffic').innerHTML = document.getElementById('hfloadingPanel_GridRegisteredRequests_RequestOnTraffic').value;
    var ObjRequest = parent.DialogRequestOnTraffic.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    var DateKey = ObjRequest.DateKey;
    var RequestDate = ObjRequest.RequestDate;
    CallBack_GridRegisteredRequests_RequestOnTraffic.callback(CharToKeyCode_RequestOnTraffic(RequestCaller), CharToKeyCode_RequestOnTraffic(DateKey), CharToKeyCode_RequestOnTraffic(RequestDate));
}

function GridRegisteredRequests_RequestOnTraffic_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridRegisteredRequests_RequestOnTraffic').innerHTML = '';
}

function CallBack_GridRegisteredRequests_RequestOnTraffic_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RegisteredRequests').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridRegisteredRequests_RequestOnTraffic();
    }
}

function GetRequestStateTitle_RequestOnTraffic(requestState) {
    var RequestStates = document.getElementById('hfRequestStates_RequestOnTraffic').value.split('#');
    for (var i = 0; i < RequestStates.length; i++) {
        var requestStateObj = RequestStates[i].split(':');
        if (requestStateObj.length > 1) {
            if (requestStateObj[1] == requestState.toString())
                return requestStateObj[0];
        }
    }
}

function RequestOnTrafficForm_onKeyDown(event) {
    var activeID = null;
    if (event.keyCode == 38 || event.keyCode == 40) {
        activeID = document.activeElement.id;
        CheckTimePickerState_RequestOnTraffic(activeID);
    }
}

function CheckTimePickerState_RequestOnTraffic(TimeSelector) {
    if ((TimeSelector == 'TimeSelector_Entrance_RequestOnTraffic_txtHour' && isNaN(document.getElementById(TimeSelector).value)) || (TimeSelector == 'TimeSelector_Entrance_RequestOnTraffic_txtMinute' && (isNaN(document.getElementById(TimeSelector).value) || document.getElementById('TimeSelector_Entrance_RequestOnTraffic_txtHour').value == NullTime_RequestOnTraffic)) || (TimeSelector == 'TimeSelector_Entrance_RequestOnTraffic_txtHour' && document.getElementById(TimeSelector).value == '-1'))
        document.getElementById('TimeSelector_Entrance_RequestOnTraffic_txtHour').value = zeroTime;
    if ((TimeSelector == 'TimeSelector_Exit_RequestOnTraffic_txtHour' && isNaN(document.getElementById(TimeSelector).value)) || (TimeSelector == 'TimeSelector_Exit_RequestOnTraffic_txtMinute' && (isNaN(document.getElementById(TimeSelector).value) || document.getElementById('TimeSelector_Exit_RequestOnTraffic_txtHour').value == NullTime_RequestOnTraffic)) || (TimeSelector == 'TimeSelector_Entrance_RequestOnTraffic_txtHour' && document.getElementById(TimeSelector).value == '-1'))
        document.getElementById('TimeSelector_Exit_RequestOnTraffic_txtHour').value = zeroTime;
    if ((TimeSelector == 'TimeSelector_Entrance_RequestOnTraffic_txtMinute' || TimeSelector == 'TimeSelector_Exit_RequestOnTraffic_txtMinute') && isNaN(document.getElementById(TimeSelector).value)) 
        document.getElementById(TimeSelector).value = zeroTime;
}

function CallBack_cmbTrafficType_RequestOnTraffic_onCallbackError(sender, e) {
    ShowConnectionError_RequestOnTraffic();
}

function ShowConnectionError_RequestOnTraffic() {
    var error = document.getElementById('hfErrorType_RequestOnTraffic').value;
    var errorBody = document.getElementById('hfConnectionError_RequestOnTraffic').value;
    showDialog(error, errorBody, 'error');
}

function CallBack_GridTrafficPairs_RequestOnTraffic_onCallbackError(sender, e) {
    ShowConnectionError_RequestOnTraffic();
}

function CallBack_GridRegisteredRequests_RequestOnTraffic_onCallbackError(sender, e) {
    ShowConnectionError_RequestOnTraffic();
}

function CollapseControls_RequestOnTraffic() {
    cmbTrafficType_RequestOnTraffic.collapse();
}

function tlbItemFormReconstruction_TlbRequestOnTraffic_onClick() {
    var ObjDialogRequestOnTraffic = parent.DialogRequestOnTraffic.get_value();
    var field = ObjDialogRequestOnTraffic.Field;
    DialogRequestOnTraffic_onClose();
    parent.parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.ShowRelativeDialog_MasterMonthlyOperation(field);
}

function GridTrafficPairs_RequestOnTraffic_onItemSelect(sender, e) {
    NavigateTrafficPairs_RequestOnTraffic(e.get_item());
}

function NavigateTrafficPairs_RequestOnTraffic(selectedTrafiicPairsItem) {
    if (selectedTrafiicPairsItem != undefined) {
        var fromTime = selectedTrafiicPairsItem.getMember('From').get_text();
        var toTime = selectedTrafiicPairsItem.getMember('To').get_text();
        if (fromTime != '')
            SetDuration_TimePicker_RequestOnTraffic('TimeSelector_Entrance_RequestOnTraffic', fromTime);
        if (toTime != '')
            SetDuration_TimePicker_RequestOnTraffic('TimeSelector_Exit_RequestOnTraffic', toTime);
    }
}

function SetDuration_TimePicker_RequestOnTraffic(TimePicker, strTime) {
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

function tlbItemHelp_TlbRequestOnTraffic_onClick() {
    LoadHelpPage('tlbItemHelp_TlbRequestOnTraffic');
}











