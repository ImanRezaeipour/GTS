
var box_MissionRequest_HourlyRequestOnAbsence_IsShown = false;
var box_LeaveRequest_HourlyRequestOnAbsence_IsShown = false;
var CurrenPageSate_DialogHourlyRequestOnAbsence = 'View';
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence = null;
var CurrentRequestState_HourlyRequestOnAbsence = null;
var ConfirmState_HourlyRequestOnAbsence = null;
var zeroTime = '00';
var ObjRequestAttachment_HourlyRequestOnAbsence = null;

function initTimePickers_HourlyRequestOnAbsence() {
    SetButtonImages_TimeSelectors_DialogHourlyRequestOnAbsence();
    ChangeTimePickerActions_DialogHourlyRequestOnAbsence('TimeSelector_FromHour_Leave_HourlyRequestOnAbsence');
    ChangeTimePickerActions_DialogHourlyRequestOnAbsence('TimeSelector_ToHour_Leave_HourlyRequestOnAbsence');
    ChangeTimePickerActions_DialogHourlyRequestOnAbsence('TimeSelector_FromHour_Mission_HourlyRequestOnAbsence');
    ChangeTimePickerActions_DialogHourlyRequestOnAbsence('TimeSelector_ToHour_Mission_HourlyRequestOnAbsence');
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_FromHour_Leave_HourlyRequestOnAbsence');
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_ToHour_Leave_HourlyRequestOnAbsence');
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_FromHour_Mission_HourlyRequestOnAbsence');
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_ToHour_Mission_HourlyRequestOnAbsence');
}

function ResetTimepicker_HourlyRequestOnAbsence(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function Set_SelectedDateTime_HourlyRequestOnAbsence() {
    document.getElementById('tdSelectedDate_HourlyRequestOnAbsence').innerHTML = parent.DialogHourlyRequestOnAbsence.get_value().RequestDateTitle;
}

function GetBoxesHeaders_HourlyRequestOnAbsence() {
    parent.document.getElementById('Title_DialogHourlyRequestOnAbsence').innerHTML = document.getElementById('hfTitle_DialogHourlyRequestOnAbsence').value;
    document.getElementById('header_AbsenceDetails_HourlyRequestOnAbsence').innerHTML = document.getElementById('hfheader_AbsenceDetails_HourlyRequestOnAbsence').value;
    document.getElementById('header_RegisteredRequests_HourlyRequestOnAbsence').innerHTML = document.getElementById('hfheader_RegisteredRequests_HourlyRequestOnAbsence').value;
    document.getElementById('cmbLeaveType_HourlyRequestOnAbsence_Input').value = document.getElementById('cmbMissionType_HourlyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_HourlyRequestOnAbsence').value;
}

function SetButtonImages_TimeSelectors_DialogHourlyRequestOnAbsence() {
    SetButtonImages_TimeSelector_DialogHourlyRequestOnAbsence('TimeSelector_FromHour_Leave_HourlyRequestOnAbsence');
    SetButtonImages_TimeSelector_DialogHourlyRequestOnAbsence('TimeSelector_ToHour_Leave_HourlyRequestOnAbsence');
    SetButtonImages_TimeSelector_DialogHourlyRequestOnAbsence('TimeSelector_FromHour_Mission_HourlyRequestOnAbsence');
    SetButtonImages_TimeSelector_DialogHourlyRequestOnAbsence('TimeSelector_ToHour_Mission_HourlyRequestOnAbsence');
}

function ChangeTimePickerActions_DialogHourlyRequestOnAbsence(TimeSelector) {
    document.getElementById("" + TimeSelector + "_imgUp").onclick = function () {
        CheckTimePickerState_HourlyRequestOnAbsence(TimeSelector + '_txtHour');
        CheckTimePickerState_HourlyRequestOnAbsence(TimeSelector + '_txtMinute');
        addTime(document.getElementById("" + TimeSelector + "_imgUp"), 24, 1, 1);
    };
    document.getElementById("" + TimeSelector + "_imgDown").onclick = function () {
        CheckTimePickerState_HourlyRequestOnAbsence(TimeSelector + '_txtHour');
        CheckTimePickerState_HourlyRequestOnAbsence(TimeSelector + '_txtMinute');
        subtractTime(document.getElementById("" + TimeSelector + "_imgDown"), 24, 1, 1);
    };
//    document.getElementById("" + TimeSelector + "_txtHour").onchange = function () {
//        CheckTimeSelectorPartValue_HourlyRequestOnAbsence(TimeSelector, '_txtHour');
//    }
//    document.getElementById("" + TimeSelector + "_txtMinute").onchange = function () {
//        CheckTimeSelectorPartValue_HourlyRequestOnAbsence(TimeSelector, '_txtMinute');
//    }
}

function CheckTimeSelectorPartValue_HourlyRequestOnAbsence(TimeSelectorPartID, identifier) {
    if (document.getElementById(TimeSelectorPartID + identifier).value == "") {
        if (document.getElementById(TimeSelectorPartID + identifier).value == "")
            document.getElementById(TimeSelectorPartID + identifier).value = zeroTime;
    }
}

function SetButtonImages_TimeSelector_DialogHourlyRequestOnAbsence(TimeSelector) {
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

function rdbLeaveRequest_HourlyRequestOnAbsence_onClick() {
    HourlyRequestOnAbsence_onInsert();
    box_LeaveRequest_HourlyRequestOnAbsence_onShowHide();
}

function rdbMissionRequest_HourlyRequestOnAbsence_onClick() {
    HourlyRequestOnAbsence_onInsert();
    box_MissionRequest_HourlyRequestOnAbsence_onShowHide();
}

function box_MissionRequest_HourlyRequestOnAbsence_onShowHide() {
    CollapseControls_HourlyRequestOnAbsence();
    setSlideDownSpeed(200);
    slidedown_showHide('box_MissionRequest_HourlyRequestOnAbsence');
    if (box_MissionRequest_HourlyRequestOnAbsence_IsShown) {
        box_MissionRequest_HourlyRequestOnAbsence_IsShown = false;
        ClearMissionRequestList_HourlyRequestOnAbsence();
    }
    else {
        box_MissionRequest_HourlyRequestOnAbsence_IsShown = true;
        CurrentRequestState_HourlyRequestOnAbsence = 'Mission';
    }
}

function ClearMissionRequestList_HourlyRequestOnAbsence() {
    cmbMissionType_HourlyRequestOnAbsence.unSelect();
    document.getElementById('cmbMissionType_HourlyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_HourlyRequestOnAbsence').value;
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_FromHour_Mission_HourlyRequestOnAbsence');
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_ToHour_Mission_HourlyRequestOnAbsence');
    document.getElementById('txtDescription_Mission_HourlyRequestOnAbsence').value = '';
    var trvNodeNotDetermined = trvMissionLocation_HourlyRequestOnAbsence.findNodeById('-1');
    if (trvNodeNotDetermined != undefined) {
        trvNodeNotDetermined.select();
        cmbMissionLocation_HourlyRequestOnAbsence.set_text(trvNodeNotDetermined.get_text());
    }
    cmbMissionType_HourlyRequestOnAbsence.collapse();
    cmbMissionLocation_HourlyRequestOnAbsence.collapse();
    ObjRequestAttachment_HourlyRequestOnAbsence = null;
    document.getElementById('tdAttachmentName_Mission_HourlyRequestOnAbsence').innerHTML = '';
    document.getElementById('chbToHourInNextDay_Mission_HourlyRequestOnAbsence').checked = false;
}

function box_LeaveRequest_HourlyRequestOnAbsence_onShowHide() {
    CollapseControls_HourlyRequestOnAbsence();
    setSlideDownSpeed(200);
    ChangeHideElementsState_DialogHourlyRequestOnAbsence(true);
    slidedown_showHide('box_LeaveRequest_HourlyRequestOnAbsence');
    if (box_LeaveRequest_HourlyRequestOnAbsence_IsShown) {
        box_LeaveRequest_HourlyRequestOnAbsence_IsShown = false;
        ClearLeaveRequestList_HourlyRequestOnAbsence();
    }
    else {
        box_LeaveRequest_HourlyRequestOnAbsence_IsShown = true;
        CurrentRequestState_HourlyRequestOnAbsence = 'Leave';
    }
}

function ClearLeaveRequestList_HourlyRequestOnAbsence() {
    cmbLeaveType_HourlyRequestOnAbsence.unSelect();
    document.getElementById('cmbLeaveType_HourlyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_HourlyRequestOnAbsence').value;
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_FromHour_Leave_HourlyRequestOnAbsence');
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_ToHour_Leave_HourlyRequestOnAbsence');
    document.getElementById('txtDescription_Leave_HourlyRequestOnAbsence').value = '';
    cmbDoctorName_HourlyRequestOnAbsence.selectItemByIndex(0);
    cmbIllnessName_HourlyRequestOnAbsence.selectItemByIndex(0);
    cmbLeaveType_HourlyRequestOnAbsence.collapse();
    cmbDoctorName_HourlyRequestOnAbsence.collapse();
    cmbIllnessName_HourlyRequestOnAbsence.collapse();
    ObjRequestAttachment_HourlyRequestOnAbsence = null;
    document.getElementById('tdAttachmentName_Leave_HourlyRequestOnAbsence').innerHTML = '';
    document.getElementById('chbToHourInNextDay_Leave_HourlyRequestOnAbsence').checked = false;
}

function ChangeHideElementsState_DialogHourlyRequestOnAbsence(State) {
    var visibility;
    if (State)
        visibility = 'hidden';
    else
        visibility = 'visible';
    document.getElementById('cmbDoctorName_HourlyRequestOnAbsence').style.visibility = visibility;
    document.getElementById('cmbIllnessName_HourlyRequestOnAbsence').style.visibility = visibility;
    document.getElementById('lblDoctorName_HourlyRequestOnAbsence').style.visibility = visibility;
    document.getElementById('lblIllnessName_HourlyRequestOnAbsence').style.visibility = visibility;
}

function ChangePageState_DialogHourlyRequestOnAbsence(state) {
    CurrenPageSate_DialogHourlyRequestOnAbsence = state;
    SetActionMode_HourlyRequestOnAbsence(state);
    if (CurrenPageSate_DialogHourlyRequestOnAbsence == 'Add' || CurrenPageSate_DialogHourlyRequestOnAbsence == 'Delete') {
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemDelete_TlbHourlyRequestOnAbsence').set_enabled(false);
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemDelete_TlbHourlyRequestOnAbsence').set_imageUrl('remove_silver.png');
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemSave_TlbHourlyRequestOnAbsence').set_enabled(true);
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemSave_TlbHourlyRequestOnAbsence').set_imageUrl('save.png');
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemCancel_TlbHourlyRequestOnAbsence').set_enabled(true);
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemCancel_TlbHourlyRequestOnAbsence').set_imageUrl('cancel.png');
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemExit_TlbHourlyRequestOnAbsence').set_enabled(false);
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemExit_TlbHourlyRequestOnAbsence').set_imageUrl('exit_silver.png');
        document.getElementById('rdbLeaveRequest_HourlyRequestOnAbsence').disabled = true;
        document.getElementById('rdbMissionRequest_HourlyRequestOnAbsence').disabled = true;
        if (state == 'Add') {
            var SelectedItems_GridAbsencePairs_RequestOnAbsence = GridAbsencePairs_RequestOnAbsence.getSelectedItems();
            if (SelectedItems_GridAbsencePairs_RequestOnAbsence.length > 0)                
                NavigateAbsensePairs_RequestOnAbsence(SelectedItems_GridAbsencePairs_RequestOnAbsence[0]);
        }
        if (state == 'Delete')
            HourlyRequestOnAbsence_onSave();
    }
    if (CurrenPageSate_DialogHourlyRequestOnAbsence == 'View') {
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemDelete_TlbHourlyRequestOnAbsence').set_enabled(true);
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemDelete_TlbHourlyRequestOnAbsence').set_imageUrl('remove.png');
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemSave_TlbHourlyRequestOnAbsence').set_enabled(false);
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemSave_TlbHourlyRequestOnAbsence').set_imageUrl('save_silver.png');
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemCancel_TlbHourlyRequestOnAbsence').set_enabled(false);
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemCancel_TlbHourlyRequestOnAbsence').set_imageUrl('cancel_silver.png');
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemExit_TlbHourlyRequestOnAbsence').set_enabled(true);
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemExit_TlbHourlyRequestOnAbsence').set_imageUrl('exit.png');
        document.getElementById('rdbLeaveRequest_HourlyRequestOnAbsence').disabled = false;
        document.getElementById('rdbMissionRequest_HourlyRequestOnAbsence').disabled = false;
        document.getElementById('rdbLeaveRequest_HourlyRequestOnAbsence').checked = false;
        document.getElementById('rdbMissionRequest_HourlyRequestOnAbsence').checked = false;
    }
}

function HourlyRequestOnAbsence_onInsert() {
    ChangePageState_DialogHourlyRequestOnAbsence('Add');
}

function HourlyRequestOnAbsence_onSave() {
    if (CurrenPageSate_DialogHourlyRequestOnAbsence != 'Delete')
        UpdateRequest_HourlyRequestOnAbsence();
    else
        ShowDialogConfirm('Delete');
}

function UpdateRequest_HourlyRequestOnAbsence() {
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence = new Object();
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ID = '0';
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.RequestState = null;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardID = '0';
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardTitle = null;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.RequestDate = null;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.FromTime = null;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ToTime = null;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsToTimeInNextDay = false;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.Description = null;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsSeakLeave = 'false';
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.DoctorID = '-1';
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IllnessID = '-1';
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.MissionLocationID = '-1';
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.AttachmentFile = null;

    var ObjDialogHourlyRequestOnAbsence = parent.DialogHourlyRequestOnAbsence.get_value();

    var SelectedItems_GridRegisteredRequests_HourlyRequestOnAbsence = GridRegisteredRequests_HourlyRequestOnAbsence.getSelectedItems();
    if (SelectedItems_GridRegisteredRequests_HourlyRequestOnAbsence.length > 0)
        ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ID = SelectedItems_GridRegisteredRequests_HourlyRequestOnAbsence[0].getMember("ID").get_text();

    if (CurrenPageSate_DialogHourlyRequestOnAbsence != 'Delete') {
        ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.RequestState = CurrentRequestState_HourlyRequestOnAbsence;
        ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.RequestDate = parent.DialogHourlyRequestOnAbsence.get_value().RequestDate;
        switch (CurrentRequestState_HourlyRequestOnAbsence) {
            case 'Leave':
                if (cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem() != undefined) {
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardID = cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem().get_id();
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardTitle = cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem().get_text();
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.FromTime = GetDuration_TimePicker_HourlyRequestOnAbsence('TimeSelector_FromHour_Leave_HourlyRequestOnAbsence');
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ToTime = GetDuration_TimePicker_HourlyRequestOnAbsence('TimeSelector_ToHour_Leave_HourlyRequestOnAbsence');
                    if (document.getElementById('chbToHourInNextDay_Leave_HourlyRequestOnAbsence').checked)
                        ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsToTimeInNextDay = true;
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.Description = document.getElementById('txtDescription_Leave_HourlyRequestOnAbsence').value;
                    if (ObjRequestAttachment_HourlyRequestOnAbsence != null)
                        ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.AttachmentFile = ObjRequestAttachment_HourlyRequestOnAbsence.RequestAttachmentSavedName;
                    if (cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem().get_value() == 'true') {
                        ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsSeakLeave = 'true';
                        if (cmbDoctorName_HourlyRequestOnAbsence.getSelectedItem() != undefined)
                            ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.DoctorID = cmbDoctorName_HourlyRequestOnAbsence.getSelectedItem().get_value();
                        if (cmbIllnessName_HourlyRequestOnAbsence.getSelectedItem() != undefined)
                            ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IllnessID = cmbIllnessName_HourlyRequestOnAbsence.getSelectedItem().get_value();
                    }
                    else
                        ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsSeakLeave = 'false';
                }
                break;
            case 'Mission':
                if (cmbMissionType_HourlyRequestOnAbsence.getSelectedItem() != undefined) {
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardID = cmbMissionType_HourlyRequestOnAbsence.getSelectedItem().get_value();
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardTitle = cmbMissionType_HourlyRequestOnAbsence.getSelectedItem().get_text();
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.FromTime = GetDuration_TimePicker_HourlyRequestOnAbsence('TimeSelector_FromHour_Mission_HourlyRequestOnAbsence');
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ToTime = GetDuration_TimePicker_HourlyRequestOnAbsence('TimeSelector_ToHour_Mission_HourlyRequestOnAbsence');
                    if (document.getElementById('chbToHourInNextDay_Mission_HourlyRequestOnAbsence').checked)
                        ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsToTimeInNextDay = true;
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.Description = document.getElementById('txtDescription_Mission_HourlyRequestOnAbsence').value;
                    if (ObjRequestAttachment_HourlyRequestOnAbsence != null)
                        ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.AttachmentFile = ObjRequestAttachment_HourlyRequestOnAbsence.RequestAttachmentSavedName;
                    if (trvMissionLocation_HourlyRequestOnAbsence.get_selectedNode() != undefined) {
                        if (trvMissionLocation_HourlyRequestOnAbsence.get_selectedNode().get_parentNode() != undefined)
                            ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.MissionLocationID = trvMissionLocation_HourlyRequestOnAbsence.get_selectedNode().get_id();
                    }
                }
                break;
        }
    }
    UpdateRequest_HourlyRequestOnAbsencePage(CharToKeyCode_HourlyRequestOnAbsence(ObjDialogHourlyRequestOnAbsence.RequestCaller), CharToKeyCode_HourlyRequestOnAbsence(ObjDialogHourlyRequestOnAbsence.LoadState), CharToKeyCode_HourlyRequestOnAbsence(CurrenPageSate_DialogHourlyRequestOnAbsence), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ID), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.RequestState), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardID), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.RequestDate), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.FromTime), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ToTime), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsToTimeInNextDay.toString()), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.Description), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsSeakLeave), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.DoctorID), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IllnessID), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.MissionLocationID), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.AttachmentFile));
    DialogWaiting.Show();
}

function UpdateRequest_HourlyRequestOnAbsencePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_HourlyRequestOnAbsence').value;
            Response[1] = document.getElementById('hfConnectionError_HourlyRequestOnAbsence').value;
        }
        if (RetMessage[2] == 'success') {
            HourlyRequest_OnAfterUpdate(Response);
            SetBaseState_HourlyRequestOnAbsence();
        }
        else {
            if (CurrenPageSate_DialogHourlyRequestOnAbsence == 'Delete')
                ChangePageState_DialogHourlyRequestOnAbsence('View');
        }
    }
}

function HourlyRequest_OnAfterUpdate(Response) {
    if (ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence != null) {
        var PreCardTitle = ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardTitle;
        var FromTime = ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.FromTime;
        var ToTime = ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ToTime;
        var RegisterDate = '';
        var RequestState = '';
        var RequestStateTitle = '';

        if (ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsToTimeInNextDay)
            ToTime = '+' + ToTime;

        ObjRequestAttachment_HourlyRequestOnAbsence = null;

        var RegisteredRequestItem = null;
        GridRegisteredRequests_HourlyRequestOnAbsence.beginUpdate();
        switch (CurrenPageSate_DialogHourlyRequestOnAbsence) {
            case 'Add':
                RegisteredRequestItem = GridRegisteredRequests_HourlyRequestOnAbsence.get_table().addEmptyRow(GridRegisteredRequests_HourlyRequestOnAbsence.get_recordCount());
                RegisteredRequestItem.setValue(0, Response[3], false);
                GridRegisteredRequests_HourlyRequestOnAbsence.selectByKey(Response[3], 0, false);
                RequestStateTitle = GetRequestStateTitle_HourlyRequestOnAbsence(Response[4]);
                RequestState = Response[4];
                RegisterDate = Response[5];
                break;
            case 'Delete':
                GridRegisteredRequests_HourlyRequestOnAbsence.selectByKey(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ID, 0, false);
                GridRegisteredRequests_HourlyRequestOnAbsence.deleteSelected();
                break;
        }
        if (CurrenPageSate_DialogHourlyRequestOnAbsence != 'Delete') {
            RegisteredRequestItem.setValue(1, PreCardTitle, false);
            RegisteredRequestItem.setValue(2, FromTime, false);
            RegisteredRequestItem.setValue(3, ToTime, false);
            RegisteredRequestItem.setValue(4, RegisterDate, false);
            RegisteredRequestItem.setValue(5, RequestStateTitle, false);
            RegisteredRequestItem.setValue(6, RequestState, false);
        }
        GridRegisteredRequests_HourlyRequestOnAbsence.endUpdate();
    }
}

function GetDuration_TimePicker_HourlyRequestOnAbsence(TimePicker) {
    var hour = document.getElementById(TimePicker + '_txtHour').value;
    var minute = document.getElementById(TimePicker + '_txtMinute').value;
    if (hour == '' || parseFloat(hour) < 0)
        document.getElementById(TimePicker + '_txtHour').value = hour = '00';
    if (minute == '' || parseFloat(minute) < 0)
        document.getElementById(TimePicker + '_txtMinute').value = minute = '00';
    if (document.getElementById(TimePicker + '_txtHour').value.length < 2)
        document.getElementById(TimePicker + '_txtHour').value = '0' + document.getElementById(TimePicker + '_txtHour').value;
    if (document.getElementById(TimePicker + '_txtMinute').value.length < 2)
        document.getElementById(TimePicker + '_txtMinute').value = '0' + document.getElementById(TimePicker + '_txtMinute').value;
    return document.getElementById(TimePicker + '_txtHour').value + ':' + document.getElementById(TimePicker + '_txtMinute').value;
}

function HourlyRequestOnAbsence_onCancel() {
    CurrenPageSate_DialogHourlyRequestOnAbsence = 'Cancel';
    SetBaseState_HourlyRequestOnAbsence();
}

function SetBaseState_HourlyRequestOnAbsence() {
    HideDives_HourlyRequestOnAbsence();
    ChangePageState_DialogHourlyRequestOnAbsence('View');
}

function HideDives_HourlyRequestOnAbsence() {
    if (box_LeaveRequest_HourlyRequestOnAbsence_IsShown)
        box_LeaveRequest_HourlyRequestOnAbsence_onShowHide();
    if (box_MissionRequest_HourlyRequestOnAbsence_IsShown)
        box_MissionRequest_HourlyRequestOnAbsence_onShowHide();
}

function DialogHourlyRequestOnAbsence_onClose() {
    parent.document.getElementById('DialogHourlyRequestOnAbsence_IFrame').src = 'WhitePage.aspx';
    parent.DialogHourlyRequestOnAbsence.Close();
    var ObjRequest = parent.DialogHourlyRequestOnAbsence.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    if(RequestCaller == 'Grid')
       parent.parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();
}

function SetPosition_DropDownDives_DialogHourlyRequestOnAbsence() {
    if (parent.parent.CurrentLangID == 'fa-IR') {
        document.getElementById('box_LeaveRequest_HourlyRequestOnAbsence').style.right = '144px';
        document.getElementById('box_MissionRequest_HourlyRequestOnAbsence').style.right = '144px';
    }
    if (parent.parent.CurrentLangID == 'en-US') {
        document.getElementById('box_LeaveRequest_HourlyRequestOnAbsence').style.left = '144px';
        document.getElementById('box_MissionRequest_HourlyRequestOnAbsence').style.left = '144px';
    }
}

function tlbItemDelete_TlbHourlyRequestOnAbsence_onClick() {
    ChangePageState_DialogHourlyRequestOnAbsence('Delete');
}

function tlbItemSave_TlbHourlyRequestOnAbsence_onClick() {
    HourlyRequestOnAbsence_onSave();
}

function tlbItemCancel_TlbHourlyRequestOnAbsence_onClick() {
    HourlyRequestOnAbsence_onCancel();
}

function GridAbsencePairs_RequestOnAbsence_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridAbsencePairs_RequestOnAbsence').innerHTML = '';
}

function Fill_GridAbsencePairs_RequestOnAbsence() {
    document.getElementById('loadingPanel_GridAbsencePairs_RequestOnAbsence').innerHTML = document.getElementById('hfloadingPanel_GridAbsencePairs_RequestOnAbsence').value;
    var ObjRequest = parent.DialogHourlyRequestOnAbsence.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    var DateKey = ObjRequest.DateKey;
    var RequestDate = ObjRequest.RequestDate;
    CallBack_GridAbsencePairs_RequestOnAbsence.callback(CharToKeyCode_HourlyRequestOnAbsence(RequestCaller), CharToKeyCode_HourlyRequestOnAbsence(DateKey), CharToKeyCode_HourlyRequestOnAbsence(RequestDate));
}

function CallBack_GridAbsencePairs_RequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_AbsencePairs').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridAbsencePairs_RequestOnAbsence();
    }
}

function Refresh_GridAbsencePairs_RequestOnAbsence() {
    Fill_GridAbsencePairs_RequestOnAbsence();
}

function GridRegisteredRequests_HourlyRequestOnAbsence_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridRegisteredRequests_HourlyRequestOnAbsence').innerHTML = '';
}

function Fill_GridRegisteredRequests_HourlyRequestOnAbsence() {
    document.getElementById('loadingPanel_GridRegisteredRequests_HourlyRequestOnAbsence').innerHTML = document.getElementById('hfloadingPanel_GridRegisteredRequests_HourlyRequestOnAbsence').value;
    var ObjRequest = parent.DialogHourlyRequestOnAbsence.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    var DateKey = ObjRequest.DateKey;
    var RequestDate = ObjRequest.RequestDate;
    CallBack_GridRegisteredRequests_HourlyRequestOnAbsence.callback(CharToKeyCode_HourlyRequestOnAbsence(RequestCaller), CharToKeyCode_HourlyRequestOnAbsence(DateKey), CharToKeyCode_HourlyRequestOnAbsence(RequestDate));
}

function CallBack_GridRegisteredRequests_HourlyRequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RegisteredRequests').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridRegisteredRequests_HourlyRequestOnAbsence();
    }
}

function Refresh_GridRegisteredRequests_HourlyRequestOnAbsence() {
    Fill_GridRegisteredRequests_HourlyRequestOnAbsence();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_HourlyRequestOnAbsence) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateRequest_HourlyRequestOnAbsence();
            break;
        case 'Exit':
            DialogHourlyRequestOnAbsence_onClose();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_DialogHourlyRequestOnAbsence('View');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_HourlyRequestOnAbsence = confirmState;
    if (CurrenPageSate_DialogHourlyRequestOnAbsence == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_HourlyRequestOnAbsence').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_HourlyRequestOnAbsence').value;
    DialogConfirm.Show();
    CollapseControls_HourlyRequestOnAbsence();
}

function CharToKeyCode_HourlyRequestOnAbsence(str) {
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

function cmbLeaveType_HourlyRequestOnAbsence_onChange(sender, e) {
    if (cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem() != undefined) {
        if (cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem().get_value() == 'true')
            ChangeHideElementsState_DialogHourlyRequestOnAbsence(false);
        else
            ChangeHideElementsState_DialogHourlyRequestOnAbsence(true);
    }
}

function cmbLeaveType_HourlyRequestOnAbsence_onExpand(sender, e) {
    if (cmbLeaveType_HourlyRequestOnAbsence.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbLeaveType_HourlyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbLeaveType_HourlyRequestOnAbsence = true;
        CallBack_cmbLeaveType_HourlyRequestOnAbsence.callback();
    }
}

function cmbLeaveType_HourlyRequestOnAbsence_onCollapse(sender, e) {
    if (cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem() == undefined)
        document.getElementById('cmbLeaveType_HourlyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_HourlyRequestOnAbsence').value;
}

function CallBack_cmbLeaveType_HourlyRequestOnAbsence_onBeforeCallback(sender, e) {
    cmbLeaveType_HourlyRequestOnAbsence.dispose();
}

function CallBack_cmbLeaveType_HourlyRequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_LeaveTypes').value;
    if (error == "") {
        document.getElementById('cmbLeaveType_HourlyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbLeaveType_HourlyRequestOnAbsence_DropImage').mousedown();
        cmbLeaveType_HourlyRequestOnAbsence.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbLeaveType_HourlyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function cmbMissionType_HourlyRequestOnAbsence_onExpand(sender, e) {
    if (cmbMissionType_HourlyRequestOnAbsence.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionType_HourlyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionType_HourlyRequestOnAbsence = true;
        CallBack_cmbMissionType_HourlyRequestOnAbsence.callback();
    }
}

function cmbMissionType_HourlyRequestOnAbsence_onCollapse(sender, e) {
    if (cmbMissionType_HourlyRequestOnAbsence.getSelectedItem() == undefined)
        document.getElementById('cmbMissionType_HourlyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_HourlyRequestOnAbsence').value;
}

function CallBack_cmbMissionType_HourlyRequestOnAbsence_onBeforeCallback(sender, e) {
    cmbMissionType_HourlyRequestOnAbsence.dispose();
}

function CallBack_cmbMissionType_HourlyRequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MissionTypes').value;
    if (error == "") {
        document.getElementById('cmbMissionType_HourlyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMissionType_HourlyRequestOnAbsence_DropImage').mousedown();
        cmbMissionType_HourlyRequestOnAbsence.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbMissionType_HourlyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function trvMissionLocation_HourlyRequestOnAbsence_onNodeSelect(sender, e) {
    document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_TextBox').innerHTML = e.get_node().get_text();
    cmbMissionLocation_HourlyRequestOnAbsence.collapse();
}

function cmbMissionLocation_HourlyRequestOnAbsence_onExpand(sender, e) {
    if (trvMissionLocation_HourlyRequestOnAbsence.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionLocation_HourlyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionLocation_HourlyRequestOnAbsence = true;
        CallBack_cmbMissionLocation_HourlyRequestOnAbsence.callback();
    }
}

function CallBack_cmbMissionLocation_HourlyRequestOnAbsence_onBeforeCallback(sender, e) {
    cmbMissionLocation_HourlyRequestOnAbsence.dispose();
}

function CallBack_cmbMissionLocation_HourlyRequestOnAbsence_CallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MissionLocations').value;
    if (error == "") {
        document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMissionLocation_HourlyRequestOnAbsence_DropImage').mousedown();
        cmbMissionLocation_HourlyRequestOnAbsence.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function cmbDoctorName_HourlyRequestOnAbsence_onExpand(sender, e) {
    if (cmbDoctorName_HourlyRequestOnAbsence.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDoctorName_HourlyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDoctorName_HourlyRequestOnAbsence = true;
        CallBack_cmbDoctorName_HourlyRequestOnAbsence.callback();
    }
}

function CallBack_cmbDoctorName_HourlyRequestOnAbsence_onBeforeCallback(sender, e) {
    cmbDoctorName_HourlyRequestOnAbsence.dispose();
}

function CallBack_cmbDoctorName_HourlyRequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Doctors').value;
    if (error == "") {
        document.getElementById('cmbDoctorName_HourlyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDoctorName_HourlyRequestOnAbsence_DropImage').mousedown();
        cmbDoctorName_HourlyRequestOnAbsence.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbDoctorName_HourlyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function cmbIllnessName_HourlyRequestOnAbsence_onExpand(sender, e) {
    if (cmbIllnessName_HourlyRequestOnAbsence.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbIllnessName_HourlyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbIllnessName_HourlyRequestOnAbsence = true;
        CallBack_cmbIllnessName_HourlyRequestOnAbsence.callback();
    }
}

function CallBack_cmbIllnessName_HourlyRequestOnAbsence_onBeforeCallback(sender, e) {
    cmbIllnessName_HourlyRequestOnAbsence.dispose();
}

function CallBack_cmbIllnessName_HourlyRequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Illnesses').value;
    if (error == "") {
        document.getElementById('cmbIllnessName_HourlyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbIllnessName_HourlyRequestOnAbsence_DropImage').mousedown();
        cmbIllnessName_HourlyRequestOnAbsence.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbIllnessName_HourlyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function GetRequestStateTitle_HourlyRequestOnAbsence(requestState) {
    var RequestStates = document.getElementById('hfRequestStates_HourlyRequestOnAbsence').value.split('#');
    for (var i = 0; i < RequestStates.length; i++) {
        var requestStateObj = RequestStates[i].split(':');
        if (requestStateObj.length > 1) {
            if (requestStateObj[1] == requestState.toString())
                return requestStateObj[0];
        }
    }
}

function tlbItemExit_TlbHourlyRequestOnAbsence_onClick() {
    ShowDialogConfirm('Exit');
}

function SetActionMode_HourlyRequestOnAbsence(state) {
    document.getElementById('ActionMode_HourlyRequestOnAbsence').innerHTML = document.getElementById("hf" + state + "_HourlyRequestOnAbsence").value;
}

function HourlyRequestOnAbsenceForm_onKeyDown(event) {
    var activeID = null;
    if (event.keyCode == 38 || event.keyCode == 40) {
        activeID = document.activeElement.id;
        CheckTimePickerState_HourlyRequestOnAbsence(activeID);
    }
}

function CheckTimePickerState_HourlyRequestOnAbsence(TimeSelector) {
    if (((TimeSelector == 'TimeSelector_FromHour_Leave_HourlyRequestOnAbsence_txtHour' || TimeSelector == 'TimeSelector_ToHour_Leave_HourlyRequestOnAbsence_txtHour' || TimeSelector == 'TimeSelector_FromHour_Mission_HourlyRequestOnAbsence_txtHour' || TimeSelector == 'TimeSelector_ToHour_Mission_HourlyRequestOnAbsence_txtHour') && (document.getElementById(TimeSelector).value == '-1' || isNaN(document.getElementById(TimeSelector).value))) || ((TimeSelector == 'TimeSelector_FromHour_Leave_HourlyRequestOnAbsence_txtMinute' || TimeSelector == 'TimeSelector_ToHour_Leave_HourlyRequestOnAbsence_txtMinute' || TimeSelector == 'TimeSelector_FromHour_Mission_HourlyRequestOnAbsence_txtMinute' || TimeSelector == 'TimeSelector_ToHour_Mission_HourlyRequestOnAbsence_txtMinute') && isNaN(document.getElementById(TimeSelector).value))) {
        document.getElementById(TimeSelector).value = zeroTime;
        return;
    }
}

function CallBack_cmbLeaveType_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function CallBack_cmbDoctorName_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function CallBack_cmbIllnessName_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function CallBack_cmbMissionType_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function CallBack_cmbMissionLocation_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function ShowConnectionError_HourlyRequestOnAbsence() {
    var error = document.getElementById('hfErrorType_HourlyRequestOnAbsence').value;
    var errorBody = document.getElementById('hfConnectionError_HourlyRequestOnAbsence').value;
    showDialog(error, errorBody, 'error');
}

function CallBack_GridAbsencePairs_RequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function CallBack_GridRegisteredRequests_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function CollapseControls_HourlyRequestOnAbsence() {
    cmbLeaveType_HourlyRequestOnAbsence.collapse();
    cmbDoctorName_HourlyRequestOnAbsence.collapse();
    cmbIllnessName_HourlyRequestOnAbsence.collapse();
    cmbMissionType_HourlyRequestOnAbsence.collapse();
    cmbMissionLocation_HourlyRequestOnAbsence.collapse();
}

function tlbItemFormReconstruction_TlbHourlyRequestOnAbsence_onClick() {
    var ObjDialogHourlyRequestOnAbsence = parent.DialogHourlyRequestOnAbsence.get_value();
    var field = ObjDialogHourlyRequestOnAbsence.Field;
    DialogHourlyRequestOnAbsence_onClose();
    parent.parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.ShowRelativeDialog_MasterMonthlyOperation(field);
}

function GridAbsencePairs_RequestOnAbsence_onItemSelect(sender, e) {
    NavigateAbsensePairs_RequestOnAbsence(e.get_item());
}

function NavigateAbsensePairs_RequestOnAbsence(selectedAbsensePairsItem) {
    if (selectedAbsensePairsItem != undefined) {
        var fromTime = selectedAbsensePairsItem.getMember('From').get_text();
        var toTime = selectedAbsensePairsItem.getMember('To').get_text();
        if (fromTime != '') {
            switch (CurrentRequestState_HourlyRequestOnAbsence) {
                case 'Leave':
                    SetDuration_TimePicker_RequestOnAbsence('TimeSelector_FromHour_Leave_HourlyRequestOnAbsence', fromTime);
                    break;
                case 'Mission':
                    SetDuration_TimePicker_RequestOnAbsence('TimeSelector_FromHour_Mission_HourlyRequestOnAbsence', fromTime);
                    break;
            }
        }
        if (toTime != '') {
            switch (CurrentRequestState_HourlyRequestOnAbsence) {
                case 'Leave':
                    SetDuration_TimePicker_RequestOnAbsence('TimeSelector_ToHour_Leave_HourlyRequestOnAbsence', toTime);
                    break;
                case 'Mission':
                    SetDuration_TimePicker_RequestOnAbsence('TimeSelector_ToHour_Mission_HourlyRequestOnAbsence', toTime);
                    break;
            }
        }

    }
}

function SetDuration_TimePicker_RequestOnAbsence(TimePicker, strTime) {
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

function tlbItemHelp_TlbHourlyRequestOnAbsence_onClick() {
    LoadHelpPage('tlbItemHelp_TlbHourlyRequestOnAbsence');    
}

function AttachmentUploader_Leave_HourlyRequestOnAbsence_OnAfterFileUpload(StrRequestAttachment) {
    var message = null;
    if (ObjRequestAttachment_HourlyRequestOnAbsence == null)
        ObjRequestAttachment_HourlyRequestOnAbsence = new Object();
    ObjRequestAttachment_HourlyRequestOnAbsence = eval('(' + StrRequestAttachment + ')');
    if (!ObjRequestAttachment_HourlyRequestOnAbsence.IsErrorOccured)
        message = ObjRequestAttachment_HourlyRequestOnAbsence.RequestAttachmentRealName;
    else {
        message = ObjRequestAttachment_HourlyRequestOnAbsence.Message;
        ObjRequestAttachment_HourlyRequestOnAbsence = null;
    }
    document.getElementById('tdAttachmentName_Leave_HourlyRequestOnAbsence').innerHTML = message;
    Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence.callback();
}

function AttachmentUploader_Mission_HourlyRequestOnAbsence_OnAfterFileUpload(StrRequestAttachment) {
    var message = null;
    if (ObjRequestAttachment_HourlyRequestOnAbsence == null)
        ObjRequestAttachment_HourlyRequestOnAbsence = new Object();
    ObjRequestAttachment_HourlyRequestOnAbsence = eval('(' + StrRequestAttachment + ')');
    if (!ObjRequestAttachment_HourlyRequestOnAbsence.IsErrorOccured)
        message = ObjRequestAttachment_HourlyRequestOnAbsence.RequestAttachmentRealName;
    else {
        message = ObjRequestAttachment_HourlyRequestOnAbsence.Message;
        ObjRequestAttachment_HourlyRequestOnAbsence = null;
    }
    document.getElementById('tdAttachmentName_Mission_HourlyRequestOnAbsence').innerHTML = message;
    Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence.callback();
}

function Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence_onCallBackComplete(sender, e) {
    Subgurim_AttachmentUploader_Leave_HourlyRequestOnAbsenceadd('1', '4');
}

function Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence_onCallBackComplete(sender, e) {
    Subgurim_AttachmentUploader_Mission_HourlyRequestOnAbsenceadd('1', '4');
}

function Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function tlbItemDeleteAttachment_TlbDeleteAttachment_Leave_HourlyRequestOnAbsence_onClick() {
    DeleteRequestAttachment_HourlyRequestOnAbsence();
}

function DeleteRequestAttachment_HourlyRequestOnAbsence() {
    if (ObjRequestAttachment_HourlyRequestOnAbsence != null && ObjRequestAttachment_HourlyRequestOnAbsence.RequestAttachmentSavedName != null && ObjRequestAttachment_HourlyRequestOnAbsence.RequestAttachmentSavedName != '')
        DeleteRequestAttachment_HourlyRequestOnAbsencePage(CharToKeyCode_HourlyRequestOnAbsence(ObjRequestAttachment_HourlyRequestOnAbsence.RequestAttachmentSavedName));
}


function DeleteRequestAttachment_HourlyRequestOnAbsencePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_HourlyRequestOnAbsence').value;
            Response[1] = document.getElementById('hfConnectionError_HourlyRequestOnAbsence').value;
        }
        if (RetMessage[2] == 'success') {
            ObjRequestAttachment_HourlyRequestOnAbsence = null;
            switch (CurrentRequestState_HourlyRequestOnAbsence) {
                case 'Leave':
                    document.getElementById('tdAttachmentName_Leave_HourlyRequestOnAbsence').innerHTML = '';
                    break;
                case 'Mission':
                    document.getElementById('tdAttachmentName_Mission_HourlyRequestOnAbsence').innerHTML = '';
                    break;
            }
        }
        else 
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}



























