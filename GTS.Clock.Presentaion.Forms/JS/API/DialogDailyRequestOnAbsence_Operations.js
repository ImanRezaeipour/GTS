
var box_LeaveRequest_DailyRequestOnAbsence_IsShown = false;
var box_MissionRequest_DailyRequestOnAbsence_IsShown = false;
var CurrentPageState_DialogDailyRequestOnAbsence = 'View';
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjDailyRequestOnAbsence_DailyRequestOnAbsence = null;
var CurrentRequestState_DailyRequestOnAbsence = null;
var ConfirmState_DailyRequestOnAbsence = null;
var ObjRequestAttachment_DailyRequestOnAbsence = null;


function GetBoxesHeaders_DailyRequestOnAbsence() {
    parent.document.getElementById('Title_DialogDailyRequestOnAbsence').innerHTML = document.getElementById('hfTitle_DialogDailyRequestOnAbsence').value;
    document.getElementById('header_RegisteredRequests_DailyRequestOnAbsence').innerHTML = document.getElementById('hfheader_RegisteredRequests_DailyRequestOnAbsence').value;
    document.getElementById('cmbLeaveType_DailyRequestOnAbsence_Input').value = document.getElementById('cmbMissionType_DailyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_DailyRequestOnAbsence').value;
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function gdpFromDate_Leave_DailyRequestOnAbsence_OnDateChange(sender, e) {
    var fromDate = gdpFromDate_Leave_DailyRequestOnAbsence.getSelectedDate();
    gCalFromDate_Leave_DailyRequestOnAbsence.setSelectedDate(fromDate);
}

function gCalFromDate_Leave_DailyRequestOnAbsence_OnChange(sender, e) {
    var fromDate = gCalFromDate_Leave_DailyRequestOnAbsence.getSelectedDate();
    gdpFromDate_Leave_DailyRequestOnAbsence.setSelectedDate(fromDate);
}

function gCalFromDate_Leave_DailyRequestOnAbsence_onLoad(sender, e) {
    window.gCalFromDate_Leave_DailyRequestOnAbsence.PopUpObject.z = 25000000;
}

function gdpToDate_Leave_DailyRequestOnAbsence_OnDateChange(sender, e) {
    var toDate = gdpToDate_Leave_DailyRequestOnAbsence.getSelectedDate();
    gCalToDate_Leave_DailyRequestOnAbsence.setSelectedDate(toDate);
}

function gCalToDate_Leave_DailyRequestOnAbsence_OnChange(sender, e) {
    var toDate = gCalToDate_Leave_DailyRequestOnAbsence.getSelectedDate();
    gdpToDate_Leave_DailyRequestOnAbsence.setSelectedDate(toDate);
}

function gCalToDate_Leave_DailyRequestOnAbsence_OnLoad(sender, e) {
    window.gCalToDate_Leave_DailyRequestOnAbsence.PopUpObject.z = 25000000;
}

function gdpFromDate_Mission_DailyRequestOnAbsence_OnDateChange(sender, e) {
    var fromDate = gdpFromDate_Mission_DailyRequestOnAbsence.getSelectedDate();
    gCalFromDate_Mission_DailyRequestOnAbsence.setSelectedDate(fromDate);
}

function gCalFromDate_Mission_DailyRequestOnAbsence_OnChange(sender, e) {
    var fromDate = gCalFromDate_Mission_DailyRequestOnAbsence.getSelectedDate();
    gdpFromDate_Mission_DailyRequestOnAbsence.setSelectedDate(fromDate);
}

function gCalFromDate_Mission_DailyRequestOnAbsence_onLoad(sender, e) {
    window.gCalFromDate_Mission_DailyRequestOnAbsence.PopUpObject.z = 25000000;
}

function gdpToDate_Mission_DailyRequestOnAbsence_OnDateChange(sender, e) {
    var toDate = gdpToDate_Mission_DailyRequestOnAbsence.getSelectedDate();
    gCalToDate_Mission_DailyRequestOnAbsence.setSelectedDate(toDate);
}

function gCalToDate_Mission_DailyRequestOnAbsence_OnChange(sender, e) {
    var toDate = gCalToDate_Mission_DailyRequestOnAbsence.getSelectedDate();
    gdpToDate_Mission_DailyRequestOnAbsence.setSelectedDate(toDate);
}

function gCalToDate_Mission_DailyRequestOnAbsence_onLoad(sender, e) {
    window.gCalToDate_Mission_DailyRequestOnAbsence.PopUpObject.z = 25000000;
}

function btn_gdpFromDate_Leave_DailyRequestOnAbsence_OnMouseUp(event) {
    if (gCalFromDate_Leave_DailyRequestOnAbsence.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else
        return true;
}

function btn_gdpToDate_Leave_DailyRequestOnAbsence_OnMouseUp(event) {
    if (gCalToDate_Leave_DailyRequestOnAbsence.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else
        return true;
}

function btn_gdpFromDate_Mission_DailyRequestOnAbsence_OnMouseUp(event) {
    if (gCalFromDate_Mission_DailyRequestOnAbsence.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else
        return true;
}

function btn_gdpToDate_Mission_DailyRequestOnAbsence_OnMouseUp(event) {
    if (gCalToDate_Mission_DailyRequestOnAbsence.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else
        return true;
}

function trvMissionLocation_DailyRequestOnAbsence_onNodeSelect(sender, e) {
    document.getElementById('cmbMissionLocation_DailyRequestOnAbsence_TextBox').innerHTML = e.get_node().get_text();
    cmbMissionLocation_DailyRequestOnAbsence.collapse();
}

function Set_SelectedDateTime_DailyRequestOnAbsence() {
    document.getElementById('tdSelectedDate_DailyRequestOnAbsence').innerHTML = parent.DialogDailyRequestOnAbsence.get_value().RequestDateTitle;
}

function rdbLeaveRequest_DailyRequestOnAbsence_onClick() {
    DailyRequestOnAbsence_onInsert();
    box_LeaveRequest_DailyRequestOnAbsence_onShowHide();
}

function rdbMissionRequest_DailyRequestOnAbsence_onClick() {
    DailyRequestOnAbsence_onInsert();
    box_MissionRequest_DailyRequestOnAbsence_onShowHide();
}

function box_LeaveRequest_DailyRequestOnAbsence_onShowHide() {
    CollapseControls_DailyRequestOnAbsence();
    setSlideDownSpeed(200);
    ChangeHideElementsState_DialogDailyRequestOnAbsence(true);
    slidedown_showHide('box_LeaveRequest_DailyRequestOnAbsence');
    if (box_LeaveRequest_DailyRequestOnAbsence_IsShown) {
        box_LeaveRequest_DailyRequestOnAbsence_IsShown = false;
        ClearLeaveRequestList_DailyRequestOnAbsence();
    }
    else {
        box_LeaveRequest_DailyRequestOnAbsence_IsShown = true;
        CurrentRequestState_DailyRequestOnAbsence = 'Leave';
    }
}

function ClearLeaveRequestList_DailyRequestOnAbsence() {
    cmbLeaveType_DailyRequestOnAbsence.unSelect();
    document.getElementById('cmbLeaveType_DailyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_DailyRequestOnAbsence').value;
    document.getElementById('txtDescription_Leave_DailyRequestOnAbsence').value = '';
    cmbDoctorName_DailyRequestOnAbsence.selectItemByIndex(0);
    cmbIllnessName_DailyRequestOnAbsence.selectItemByIndex(0);
    cmbLeaveType_DailyRequestOnAbsence.collapse();
    cmbDoctorName_DailyRequestOnAbsence.collapse();
    cmbIllnessName_DailyRequestOnAbsence.collapse();
    ResetCalendars_DailyRequestOnAbsence();
    ObjRequestAttachment_DailyRequestOnAbsence = null;
    document.getElementById('tdAttachmentName_Leave_DailyRequestOnAbsence').innerHTML = '';
}

function ResetCalendars_DailyRequestOnAbsence() {
    var ObjRequest = parent.DialogDailyRequestOnAbsence.get_value();
    var currentDate_DailyRequestOnAbsence = ObjRequest.RequestUIDate;
    switch (parent.parent.SysLangID) {
        case 'en-US':
            currentDate_DailyRequestOnAbsence = new Date(currentDate_DailyRequestOnAbsence);
            gdpFromDate_Leave_DailyRequestOnAbsence.setSelectedDate(currentDate_DailyRequestOnAbsence);
            gCalFromDate_Leave_DailyRequestOnAbsence.setSelectedDate(currentDate_DailyRequestOnAbsence);
            gdpToDate_Leave_DailyRequestOnAbsence.setSelectedDate(currentDate_DailyRequestOnAbsence);
            gCalToDate_Leave_DailyRequestOnAbsence.setSelectedDate(currentDate_DailyRequestOnAbsence);
            gdpFromDate_Mission_DailyRequestOnAbsence.setSelectedDate(currentDate_DailyRequestOnAbsence);
            gCalFromDate_Mission_DailyRequestOnAbsence.setSelectedDate(currentDate_DailyRequestOnAbsence);
            gdpToDate_Mission_DailyRequestOnAbsence.setSelectedDate(currentDate_DailyRequestOnAbsence);
            gCalToDate_Mission_DailyRequestOnAbsence.setSelectedDate(currentDate_DailyRequestOnAbsence);
            break;
        case 'fa-IR':
            document.getElementById('pdpFromDate_Leave_DailyRequestOnAbsence').value = currentDate_DailyRequestOnAbsence;
            document.getElementById('pdpToDate_Leave_DailyRequestOnAbsence').value = currentDate_DailyRequestOnAbsence;
            document.getElementById('pdpFromDate_Mission_DailyRequestOnAbsence').value = currentDate_DailyRequestOnAbsence;
            document.getElementById('pdpToDate_Mission_DailyRequestOnAbsence').value = currentDate_DailyRequestOnAbsence;
            break;
    }
}

function box_MissionRequest_DailyRequestOnAbsence_onShowHide() {
    CollapseControls_DailyRequestOnAbsence();
    setSlideDownSpeed(200);
    slidedown_showHide('box_MissionRequest_DailyRequestOnAbsence');
    if (box_MissionRequest_DailyRequestOnAbsence_IsShown) {
        box_MissionRequest_DailyRequestOnAbsence_IsShown = false;
        ClearMissionRequestList_DailyRequestOnAbsence();
    }
    else {
        box_MissionRequest_DailyRequestOnAbsence_IsShown = true;
        CurrentRequestState_DailyRequestOnAbsence = 'Mission';
    }
}

function ClearMissionRequestList_DailyRequestOnAbsence() {
    cmbMissionType_DailyRequestOnAbsence.unSelect();
    document.getElementById('cmbMissionType_DailyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_DailyRequestOnAbsence').value;
    document.getElementById('txtDescription_Mission_DailyRequestOnAbsence').value = '';
    var trvNodeNotDetermined = trvMissionLocation_DailyRequestOnAbsence.findNodeById('-1');
    if (trvNodeNotDetermined != undefined) {
        trvNodeNotDetermined.select();
        cmbMissionLocation_DailyRequestOnAbsence.set_text(trvNodeNotDetermined.get_text());
    }
    cmbMissionType_DailyRequestOnAbsence.collapse();
    cmbMissionLocation_DailyRequestOnAbsence.collapse();
    ObjRequestAttachment_DailyRequestOnAbsence = null;
    document.getElementById('tdAttachmentName_Mission_DailyRequestOnAbsence').innerHTML = '';
}


function DailyRequestOnAbsence_onInsert() {
    ChangePageState_DialogDailyRequestOnAbsence('Add');
}

function DailyRequestOnAbsence_onSave() {
    if (CurrentPageState_DialogDailyRequestOnAbsence != 'Delete')
        UpdateRequest_DailyRequestOnAbsence();
    else
        ShowDialogConfirm('Delete');
}

function UpdateRequest_DailyRequestOnAbsence() {
    ObjDailyRequestOnAbsence_DailyRequestOnAbsence = new Object();
    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.ID = '0';
    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.RequestState = null;
    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.PreCardID = '0';
    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.PreCardTitle = null;
    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.RequestDate = null;
    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.FromDate = null;
    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.ToDate = null;
    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.Description = null;
    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.IsSeakLeave = 'false';
    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.DoctorID = '-1';
    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.IllnessID = '-1';
    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.MissionLocationID = '-1';
    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.AttachmentFile = null;

    var ObjDialogDailyRequestOnAbsence = parent.DialogDailyRequestOnAbsence.get_value();

    var SelectedItems_GridRegisteredRequests_DailyRequestOnAbsence = GridRegisteredRequests_DailyRequestOnAbsence.getSelectedItems();
    if (SelectedItems_GridRegisteredRequests_DailyRequestOnAbsence.length > 0)
        ObjDailyRequestOnAbsence_DailyRequestOnAbsence.ID = SelectedItems_GridRegisteredRequests_DailyRequestOnAbsence[0].getMember("ID").get_text();

    if (CurrentPageState_DialogDailyRequestOnAbsence != 'Delete') {
        ObjDailyRequestOnAbsence_DailyRequestOnAbsence.RequestState = CurrentRequestState_DailyRequestOnAbsence;
        ObjDailyRequestOnAbsence_DailyRequestOnAbsence.RequestDate = parent.DialogDailyRequestOnAbsence.get_value().RequestDate;
        switch (CurrentRequestState_DailyRequestOnAbsence) {
            case 'Leave':
                if (cmbLeaveType_DailyRequestOnAbsence.getSelectedItem() != undefined) {
                    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.PreCardID = cmbLeaveType_DailyRequestOnAbsence.getSelectedItem().get_id();
                    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.PreCardTitle = cmbLeaveType_DailyRequestOnAbsence.getSelectedItem().get_text();
                    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.Description = document.getElementById('txtDescription_Leave_DailyRequestOnAbsence').value;
                    if (ObjRequestAttachment_DailyRequestOnAbsence != null)
                        ObjDailyRequestOnAbsence_DailyRequestOnAbsence.AttachmentFile = ObjRequestAttachment_DailyRequestOnAbsence.RequestAttachmentSavedName;
                    if (cmbLeaveType_DailyRequestOnAbsence.getSelectedItem().get_value() == 'true') {
                        ObjDailyRequestOnAbsence_DailyRequestOnAbsence.IsSeakLeave = 'true';
                        if (cmbDoctorName_DailyRequestOnAbsence.getSelectedItem() != undefined)
                            ObjDailyRequestOnAbsence_DailyRequestOnAbsence.DoctorID = cmbDoctorName_DailyRequestOnAbsence.getSelectedItem().get_value();
                        if (cmbIllnessName_DailyRequestOnAbsence.getSelectedItem() != undefined)
                            ObjDailyRequestOnAbsence_DailyRequestOnAbsence.IllnessID = cmbIllnessName_DailyRequestOnAbsence.getSelectedItem().get_value();
                    }
                    else
                        ObjDailyRequestOnAbsence_DailyRequestOnAbsence.IsSeakLeave = 'false';

                    switch (parent.parent.SysLangID) {
                        case 'fa-IR':
                            ObjDailyRequestOnAbsence_DailyRequestOnAbsence.FromDate = document.getElementById('pdpFromDate_Leave_DailyRequestOnAbsence').value;
                            ObjDailyRequestOnAbsence_DailyRequestOnAbsence.ToDate = document.getElementById('pdpToDate_Leave_DailyRequestOnAbsence').value;
                            break;
                        case 'en-US':
                            ObjDailyRequestOnAbsence_DailyRequestOnAbsence.FromDate = document.getElementById('gdpFromDate_Leave_DailyRequestOnAbsence_picker').value;
                            ObjDailyRequestOnAbsence_DailyRequestOnAbsence.ToDate = document.getElementById('gdpToDate_Leave_DailyRequestOnAbsence_picker').value;
                            break;
                    }
                }
                break;
            case 'Mission':
                if (cmbMissionType_DailyRequestOnAbsence.getSelectedItem() != undefined) {
                    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.PreCardID = cmbMissionType_DailyRequestOnAbsence.getSelectedItem().get_value();
                    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.PreCardTitle = cmbMissionType_DailyRequestOnAbsence.getSelectedItem().get_text();
                    ObjDailyRequestOnAbsence_DailyRequestOnAbsence.Description = document.getElementById('txtDescription_Mission_DailyRequestOnAbsence').value;
                    if (trvMissionLocation_DailyRequestOnAbsence.get_selectedNode() != undefined) {
                        if (trvMissionLocation_DailyRequestOnAbsence.get_selectedNode().get_parentNode() != undefined)
                            ObjDailyRequestOnAbsence_DailyRequestOnAbsence.MissionLocationID = trvMissionLocation_DailyRequestOnAbsence.get_selectedNode().get_id();
                    }
                    if (ObjRequestAttachment_DailyRequestOnAbsence != null)
                        ObjDailyRequestOnAbsence_DailyRequestOnAbsence.AttachmentFile = ObjRequestAttachment_DailyRequestOnAbsence.RequestAttachmentSavedName;
                    switch (parent.parent.SysLangID) {
                        case 'fa-IR':
                            ObjDailyRequestOnAbsence_DailyRequestOnAbsence.FromDate = document.getElementById('pdpFromDate_Mission_DailyRequestOnAbsence').value;
                            ObjDailyRequestOnAbsence_DailyRequestOnAbsence.ToDate = document.getElementById('pdpToDate_Mission_DailyRequestOnAbsence').value;
                            break;
                        case 'en-US':
                            ObjDailyRequestOnAbsence_DailyRequestOnAbsence.FromDate = document.getElementById('gdpFromDate_Mission_DailyRequestOnAbsence_picker').value;
                            ObjDailyRequestOnAbsence_DailyRequestOnAbsence.ToDate = document.getElementById('gdpToDate_Mission_DailyRequestOnAbsence_picker').value;
                            break;
                    }
                }
                break;
        }
    }
    UpdateRequest_DailyRequestOnAbsencePage(CharToKeyCode_DailyRequestOnAbsence(ObjDialogDailyRequestOnAbsence.RequestCaller), CharToKeyCode_DailyRequestOnAbsence(ObjDialogDailyRequestOnAbsence.LoadState), CharToKeyCode_DailyRequestOnAbsence(CurrentPageState_DialogDailyRequestOnAbsence), CharToKeyCode_DailyRequestOnAbsence(ObjDailyRequestOnAbsence_DailyRequestOnAbsence.ID), CharToKeyCode_DailyRequestOnAbsence(ObjDailyRequestOnAbsence_DailyRequestOnAbsence.RequestState), CharToKeyCode_DailyRequestOnAbsence(ObjDailyRequestOnAbsence_DailyRequestOnAbsence.PreCardID), CharToKeyCode_DailyRequestOnAbsence(ObjDailyRequestOnAbsence_DailyRequestOnAbsence.RequestDate), CharToKeyCode_DailyRequestOnAbsence(ObjDailyRequestOnAbsence_DailyRequestOnAbsence.FromDate), CharToKeyCode_DailyRequestOnAbsence(ObjDailyRequestOnAbsence_DailyRequestOnAbsence.ToDate), CharToKeyCode_DailyRequestOnAbsence(ObjDailyRequestOnAbsence_DailyRequestOnAbsence.Description), CharToKeyCode_DailyRequestOnAbsence(ObjDailyRequestOnAbsence_DailyRequestOnAbsence.IsSeakLeave), CharToKeyCode_DailyRequestOnAbsence(ObjDailyRequestOnAbsence_DailyRequestOnAbsence.DoctorID), CharToKeyCode_DailyRequestOnAbsence(ObjDailyRequestOnAbsence_DailyRequestOnAbsence.IllnessID), CharToKeyCode_DailyRequestOnAbsence(ObjDailyRequestOnAbsence_DailyRequestOnAbsence.MissionLocationID), CharToKeyCode_DailyRequestOnAbsence(ObjDailyRequestOnAbsence_DailyRequestOnAbsence.AttachmentFile));
    DialogWaiting.Show();
}

function UpdateRequest_DailyRequestOnAbsencePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_DailyRequestOnAbsence').value;
            Response[1] = document.getElementById('hfConnectionError_DailyRequestOnAbsence').value;
        }
        if (RetMessage[2] == 'success') {
            DailyRequest_OnAfterUpdate(Response);
            SetBaseState_DailyRequestOnAbsence();
        }
        else {
            if (CurrentPageState_DialogDailyRequestOnAbsence == 'Delete')
                ChangePageState_DialogDailyRequestOnAbsence('View');
        }
    }
}

function DailyRequest_OnAfterUpdate(Response) {
    if (ObjDailyRequestOnAbsence_DailyRequestOnAbsence != null) {
        var PreCardTitle = ObjDailyRequestOnAbsence_DailyRequestOnAbsence.PreCardTitle;
        var FromDate = ObjDailyRequestOnAbsence_DailyRequestOnAbsence.FromDate;
        var ToDate = ObjDailyRequestOnAbsence_DailyRequestOnAbsence.ToDate;
        var RegisterDate = '';
        var RequestState = '';
        var RequestStateTitle = '';
        var IsContainsInRequestDateRange = '';

        IsContainsInRequestDateRange = Response[6];
        var RegisteredRequestItem = null;

        ObjRequestAttachment_DailyRequestOnAbsence = null;

        GridRegisteredRequests_DailyRequestOnAbsence.beginUpdate();
        switch (CurrentPageState_DialogDailyRequestOnAbsence) {
            case 'Add':
                if (IsContainsInRequestDateRange == 'true') {
                    RegisteredRequestItem = GridRegisteredRequests_DailyRequestOnAbsence.get_table().addEmptyRow(GridRegisteredRequests_DailyRequestOnAbsence.get_recordCount());
                    RegisteredRequestItem.setValue(0, Response[3], false);
                    GridRegisteredRequests_DailyRequestOnAbsence.selectByKey(Response[3], 0, false);
                    RequestStateTitle = GetRequestStateTitle_DailyRequestOnAbsence(Response[4]);
                    RequestState = Response[4];
                    RegisterDate = Response[5];
                }
                break;
            case 'Delete':
                GridRegisteredRequests_DailyRequestOnAbsence.selectByKey(ObjDailyRequestOnAbsence_DailyRequestOnAbsence.ID, 0, false);
                GridRegisteredRequests_DailyRequestOnAbsence.deleteSelected();
                break;
        }
        if (CurrentPageState_DialogDailyRequestOnAbsence != 'Delete' && IsContainsInRequestDateRange == 'true') {
            RegisteredRequestItem.setValue(1, PreCardTitle, false);
            RegisteredRequestItem.setValue(2, FromDate, false);
            RegisteredRequestItem.setValue(3, ToDate, false);
            RegisteredRequestItem.setValue(4, RegisterDate, false);
            RegisteredRequestItem.setValue(5, RequestStateTitle, false);
            RegisteredRequestItem.setValue(6, RequestState, false);
        }
        GridRegisteredRequests_DailyRequestOnAbsence.endUpdate();
    }
}


function ShowDialogConfirm(confirmState) {
    ConfirmState_DailyRequestOnAbsence = confirmState;
    if (CurrentPageState_DialogDailyRequestOnAbsence == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_DailyRequestOnAbsence').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_DailyRequestOnAbsence').value;
    DialogConfirm.Show();
    CollapseControls_DailyRequestOnAbsence();
}

function CharToKeyCode_DailyRequestOnAbsence(str) {
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

function DailyRequestOnAbsence_onCancel() {
    SetBaseState_DailyRequestOnAbsence();
}

function SetBaseState_DailyRequestOnAbsence() {
    HidePdps_DailyRequestOnAbsence();
    HideDives_DailyRequestOnAbsence();
    ChangePageState_DialogDailyRequestOnAbsence('View');
}

function HidePdps_DailyRequestOnAbsence() {
    try {
        if (document.getElementById('rdbLeaveRequest_DailyRequestOnAbsence').checked) {
            updateDateField('pdpFromDate_Leave_DailyRequestOnAbsence');
            updateDateField('pdpToDate_Leave_DailyRequestOnAbsence');
        }
        if (document.getElementById('rdbMissionRequest_DailyRequestOnAbsence').checked) {
            updateDateField('pdpFromDate_Mission_DailyRequestOnAbsence');
            updateDateField('pdpToDate_Mission_DailyRequestOnAbsence');
        }
    }
    catch (error) {
    }
}

function HideDives_DailyRequestOnAbsence() {
    if (box_LeaveRequest_DailyRequestOnAbsence_IsShown)
        box_LeaveRequest_DailyRequestOnAbsence_onShowHide();
    if (box_MissionRequest_DailyRequestOnAbsence_IsShown)
        box_MissionRequest_DailyRequestOnAbsence_onShowHide();
}

function DialogDailyRequestOnAbsence_onClose() {
    parent.document.getElementById('DialogDailyRequestOnAbsence_IFrame').src = 'WhitePage.aspx';
    parent.DialogDailyRequestOnAbsence.Close();
    var ObjRequest = parent.DialogDailyRequestOnAbsence.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    if(RequestCaller == 'Grid')
       parent.parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();
}

function ChangePageState_DialogDailyRequestOnAbsence(state) {
    CurrentPageState_DialogDailyRequestOnAbsence = state;
    SetActionMode_DailyRequestOnAbsence(state);
    if (CurrentPageState_DialogDailyRequestOnAbsence == 'Add' || CurrentPageState_DialogDailyRequestOnAbsence == 'Delete') {
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemDelete_TlbDailyRequestOnAbsence').set_enabled(false);
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemDelete_TlbDailyRequestOnAbsence').set_imageUrl('remove_silver.png');
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemSave_TlbDailyRequestOnAbsence').set_enabled(true);
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemSave_TlbDailyRequestOnAbsence').set_imageUrl('save.png');
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemCancel_TlbDailyRequestOnAbsence').set_enabled(true);
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemCancel_TlbDailyRequestOnAbsence').set_imageUrl('cancel.png');
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemExit_TlbDailyRequestOnAbsence').set_enabled(false);
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemExit_TlbDailyRequestOnAbsence').set_imageUrl('exit_silver.png');
        document.getElementById('rdbLeaveRequest_DailyRequestOnAbsence').disabled = true;
        document.getElementById('rdbMissionRequest_DailyRequestOnAbsence').disabled = true;
        if (state == 'Delete')
            DailyRequestOnAbsence_onSave();
    }
    if (CurrentPageState_DialogDailyRequestOnAbsence == 'View') {
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemDelete_TlbDailyRequestOnAbsence').set_enabled(true);
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemDelete_TlbDailyRequestOnAbsence').set_imageUrl('remove.png');
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemSave_TlbDailyRequestOnAbsence').set_enabled(false);
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemSave_TlbDailyRequestOnAbsence').set_imageUrl('save_silver.png');
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemCancel_TlbDailyRequestOnAbsence').set_enabled(false);
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemCancel_TlbDailyRequestOnAbsence').set_imageUrl('cancel_silver.png');
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemExit_TlbDailyRequestOnAbsence').set_enabled(true);
        TlbDailyRequestOnAbsence.get_items().getItemById('tlbItemExit_TlbDailyRequestOnAbsence').set_imageUrl('exit.png');
        document.getElementById('rdbLeaveRequest_DailyRequestOnAbsence').disabled = false;
        document.getElementById('rdbMissionRequest_DailyRequestOnAbsence').disabled = false;
        document.getElementById('rdbLeaveRequest_DailyRequestOnAbsence').checked = false;
        document.getElementById('rdbMissionRequest_DailyRequestOnAbsence').checked = false;
    }

}

function ViewCurrentLangCalendars_DialogDailyRequestOnAbsence() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpFromDate_Leave_DailyRequestOnAbsence").parentNode.removeChild(document.getElementById("pdpFromDate_Leave_DailyRequestOnAbsence"));
            document.getElementById("pdpFromDate_Leave_DailyRequestOnAbsenceimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_Leave_DailyRequestOnAbsenceimgbt"));
            document.getElementById("pdpFromDate_Mission_DailyRequestOnAbsence").parentNode.removeChild(document.getElementById("pdpFromDate_Mission_DailyRequestOnAbsence"));
            document.getElementById("pdpFromDate_Mission_DailyRequestOnAbsenceimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_Mission_DailyRequestOnAbsenceimgbt"));
            document.getElementById("pdpToDate_Leave_DailyRequestOnAbsence").parentNode.removeChild(document.getElementById("pdpToDate_Leave_DailyRequestOnAbsence"));
            document.getElementById("pdpToDate_Leave_DailyRequestOnAbsenceimgbt").parentNode.removeChild(document.getElementById("pdpToDate_Leave_DailyRequestOnAbsenceimgbt"));
            document.getElementById("pdpToDate_Mission_DailyRequestOnAbsence").parentNode.removeChild(document.getElementById("pdpToDate_Mission_DailyRequestOnAbsence"));
            document.getElementById("pdpToDate_Mission_DailyRequestOnAbsenceimgbt").parentNode.removeChild(document.getElementById("pdpToDate_Mission_DailyRequestOnAbsenceimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_LeaveFromDateCalendars_DailyRequestOnAbsence").removeChild(document.getElementById("Container_gCalFromDate_Leave_DailyRequestOnAbsence"));
            document.getElementById("Container_ToDateLeaveCalendars_DailyRequestOnAbsence").removeChild(document.getElementById("Container_gCalToDate_Leave_DailyRequestOnAbsence"));
            document.getElementById("Container_FromDateMissionCalendars_DailyRequestOnAbsence").removeChild(document.getElementById("Container_gCalFromDate_Mission_DailyRequestOnAbsence"));
            document.getElementById("Container_ToDateMissionCalendars_DailyRequestOnAbsence").removeChild(document.getElementById("Container_gCalToDate_Mission_DailyRequestOnAbsence"));
            break;
    }
}


function btn_gdpFromDate_Leave_DailyRequestOnAbsence_OnClick(event) {
    if (gCalFromDate_Leave_DailyRequestOnAbsence.get_popUpShowing()) {
        gCalFromDate_Leave_DailyRequestOnAbsence.hide();
    }
    else {
        gCalFromDate_Leave_DailyRequestOnAbsence.setSelectedDate(gdpFromDate_Leave_DailyRequestOnAbsence.getSelectedDate());
        gCalFromDate_Leave_DailyRequestOnAbsence.show();
    }
}

function btn_gdpToDate_Leave_DailyRequestOnAbsence_OnClick(event) {
    if (gCalToDate_Leave_DailyRequestOnAbsence.get_popUpShowing()) {
        gCalToDate_Leave_DailyRequestOnAbsence.hide();
    }
    else {
        gCalToDate_Leave_DailyRequestOnAbsence.setSelectedDate(gdpToDate_Leave_DailyRequestOnAbsence.getSelectedDate());
        gCalToDate_Leave_DailyRequestOnAbsence.show();
    }
}

function btn_gdpFromDate_Mission_DailyRequestOnAbsence_OnClick(event) {
    if (gCalFromDate_Mission_DailyRequestOnAbsence.get_popUpShowing()) {
        gCalFromDate_Mission_DailyRequestOnAbsence.hide();
    }
    else {
        gCalFromDate_Mission_DailyRequestOnAbsence.setSelectedDate(gdpFromDate_Mission_DailyRequestOnAbsence.getSelectedDate());
        gCalFromDate_Mission_DailyRequestOnAbsence.show();
    }
}

function btn_gdpToDate_Mission_DailyRequestOnAbsence_OnClick(event) {
    if (gCalToDate_Mission_DailyRequestOnAbsence.get_popUpShowing()) {
        gCalToDate_Mission_DailyRequestOnAbsence.hide();
    }
    else {
        gCalToDate_Mission_DailyRequestOnAbsence.setSelectedDate(gdpToDate_Mission_DailyRequestOnAbsence.getSelectedDate());
        gCalToDate_Mission_DailyRequestOnAbsence.show();
    }
}

function ChangeHideElementsState_DialogDailyRequestOnAbsence(State) {
    var visibility;
    if (State)
        visibility = 'hidden';
    else
        visibility = 'visible';
    document.getElementById('cmbDoctorName_DailyRequestOnAbsence').style.visibility = visibility;
    document.getElementById('cmbIllnessName_DailyRequestOnAbsence').style.visibility = visibility;
    document.getElementById('lblDoctorName_DailyRequestOnAbsence').style.visibility = visibility;
    document.getElementById('lblIllnessName_DailyRequestOnAbsence').style.visibility = visibility;
}

function SetPosition_DropDownDives_DialogDailyRequestOnAbsence() {
    if (parent.parent.CurrentLangID == 'fa-IR') {
        document.getElementById('box_LeaveRequest_DailyRequestOnAbsence').style.right = '144px';
        document.getElementById('box_MissionRequest_DailyRequestOnAbsence').style.right = '144px';
    }
    if (parent.parent.CurrentLangID == 'en-US') {
        document.getElementById('box_LeaveRequest_DailyRequestOnAbsence').style.left = '144px';
        document.getElementById('box_MissionRequest_DailyRequestOnAbsence').style.left = '144px';
    }
}

function tlbItemDelete_TlbDailyRequestOnAbsence_onClick() {
    ChangePageState_DialogDailyRequestOnAbsence('Delete');
}

function tlbItemSave_TlbDailyRequestOnAbsence_onClick() {
    DailyRequestOnAbsence_onSave();
}

function tlbItemCancel_TlbDailyRequestOnAbsence_onClick() {
    DailyRequestOnAbsence_onCancel();
}

function tlbItemExit_TlbDailyRequestOnAbsence_onClick() {
    ShowDialogConfirm('Exit');
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_DailyRequestOnAbsence) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateRequest_DailyRequestOnAbsence();
            break;
        case 'Exit':
            DialogDailyRequestOnAbsence_onClose();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_DialogDailyRequestOnAbsence('View');
}

function cmbLeaveType_DailyRequestOnAbsence_onChange(sender, e) {
    if (cmbLeaveType_DailyRequestOnAbsence.getSelectedItem() != undefined) {
        if (cmbLeaveType_DailyRequestOnAbsence.getSelectedItem().get_value() == 'true')
            ChangeHideElementsState_DialogDailyRequestOnAbsence(false);
        else
            ChangeHideElementsState_DialogDailyRequestOnAbsence(true);
    }
}

function cmbLeaveType_DailyRequestOnAbsence_onExpand(sender, e) {
    if (cmbLeaveType_DailyRequestOnAbsence.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbLeaveType_DailyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbLeaveType_DailyRequestOnAbsence = true;
        CallBack_cmbLeaveType_DailyRequestOnAbsence.callback();
    }
}

function cmbLeaveType_DailyRequestOnAbsence_onCollapse(sender, e) {
    if (cmbLeaveType_DailyRequestOnAbsence.getSelectedItem() == undefined)
        document.getElementById('cmbLeaveType_DailyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_DailyRequestOnAbsence').value;
}

function CallBack_cmbLeaveType_DailyRequestOnAbsence_onBeforeCallback(sender, e) {
    cmbLeaveType_DailyRequestOnAbsence.dispose();
}

function CallBack_cmbLeaveType_DailyRequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_LeaveTypes').value;
    if (error == "") {
        document.getElementById('cmbLeaveType_DailyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbLeaveType_DailyRequestOnAbsence_DropImage').mousedown();
        cmbLeaveType_DailyRequestOnAbsence.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbLeaveType_DailyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function cmbDoctorName_DailyRequestOnAbsence_onExpand(sender, e) {
    if (cmbDoctorName_DailyRequestOnAbsence.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDoctorName_DailyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDoctorName_DailyRequestOnAbsence = true;
        CallBack_cmbDoctorName_DailyRequestOnAbsence.callback();
    }
}

function CallBack_cmbDoctorName_DailyRequestOnAbsence_BeforeCallback(sender, e) {
    cmbDoctorName_DailyRequestOnAbsence.dispose();
}

function CallBack_cmbDoctorName_DailyRequestOnAbsence_CallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Doctors').value;
    if (error == "") {
        document.getElementById('cmbDoctorName_DailyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDoctorName_DailyRequestOnAbsence_DropImage').mousedown();
        cmbDoctorName_DailyRequestOnAbsence.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbDoctorName_DailyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function cmbIllnessName_DailyRequestOnAbsence_onExpand(sender, e) {
    if (cmbIllnessName_DailyRequestOnAbsence.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbIllnessName_DailyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbIllnessName_DailyRequestOnAbsence = true;
        CallBack_cmbIllnessName_DailyRequestOnAbsence.callback();
    }
}

function CallBack_cmbIllnessName_DailyRequestOnAbsence_BeforeCallback(sender, e) {
    cmbIllnessName_DailyRequestOnAbsence.dispose();
}

function CallBack_cmbIllnessName_DailyRequestOnAbsence_CallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Illnesses').value;
    if (error == "") {
        document.getElementById('cmbIllnessName_DailyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbIllnessName_DailyRequestOnAbsence_DropImage').mousedown();
        cmbIllnessName_DailyRequestOnAbsence.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbIllnessName_DailyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function cmbMissionType_DailyRequestOnAbsence_onExpand(sender, e) {
    if (cmbMissionType_DailyRequestOnAbsence.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionType_DailyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionType_DailyRequestOnAbsence = true;
        CallBack_cmbMissionType_DailyRequestOnAbsence.callback();
    }
}

function cmbMissionType_DailyRequestOnAbsence_onCollapse(sender, e) {
    if (cmbMissionType_DailyRequestOnAbsence.getSelectedItem() == undefined)
        document.getElementById('cmbMissionType_DailyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_DailyRequestOnAbsence').value;
}

function CallBack_cmbMissionType_DailyRequestOnAbsence_BeforeCallback(sender, e) {
    cmbMissionType_DailyRequestOnAbsence.dispose();
}

function CallBack_cmbMissionType_DailyRequestOnAbsence_CallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MissionTypes').value;
    if (error == "") {
        document.getElementById('cmbMissionType_DailyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMissionType_DailyRequestOnAbsence_DropImage').mousedown();
        cmbMissionType_DailyRequestOnAbsence.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbMissionType_DailyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function cmbMissionLocation_DailyRequestOnAbsence_onExpand(sender, e) {
    if (trvMissionLocation_DailyRequestOnAbsence.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionLocation_DailyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionLocation_DailyRequestOnAbsence == true;
        CallBack_cmbMissionLocation_DailyRequestOnAbsence.callback();
    }
}

function CallBack_cmbMissionLocation_DailyRequestOnAbsence_onBeforeCallback(sender, e) {
    cmbMissionLocation_DailyRequestOnAbsence.dispose();
}

function CallBack_cmbMissionLocation_DailyRequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MissionLocations').value;
    if (error == "") {
        document.getElementById('cmbMissionLocation_DailyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
           $('#cmbMissionLocation_DailyRequestOnAbsence_DropImage').mousedown();
        cmbMissionLocation_DailyRequestOnAbsence.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbMissionLocation_DailyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function tlbItemRefresh_TlbRefresh_GridRegisteredRequests_DailyRequestOnAbsence_onClick() {
    Refresh_GridRegisteredRequests_DailyRequestOnAbsence();
}

function Refresh_GridRegisteredRequests_DailyRequestOnAbsence() {
    Fill_GridRegisteredRequests_DailyRequestOnAbsence();
}

function Fill_GridRegisteredRequests_DailyRequestOnAbsence() {
    document.getElementById('loadingPanel_GridRegisteredRequests_DailyRequestOnAbsence').innerHTML = document.getElementById('hfloadingPanel_GridRegisteredRequests_DailyRequestOnAbsence').value;
    var ObjRequest = parent.DialogDailyRequestOnAbsence.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    var DateKey = ObjRequest.DateKey;
    var RequestDate = ObjRequest.RequestDate;
    CallBack_GridRegisteredRequests_DailyRequestOnAbsence.callback(CharToKeyCode_DailyRequestOnAbsence(RequestCaller), CharToKeyCode_DailyRequestOnAbsence(DateKey), CharToKeyCode_DailyRequestOnAbsence(RequestDate));
}

function GridRegisteredRequests_DailyRequestOnAbsence_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridRegisteredRequests_DailyRequestOnAbsence').innerHTML = '';
}

function CallBack_GridRegisteredRequests_DailyRequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RegisteredRequests').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridRegisteredRequests_DailyRequestOnAbsence();
    }
}

function GetRequestStateTitle_DailyRequestOnAbsence(requestState) {
    var RequestStates = document.getElementById('hfRequestStates_DailyRequestOnAbsence').value.split('#');
    for (var i = 0; i < RequestStates.length; i++) {
        var requestStateObj = RequestStates[i].split(':');
        if (requestStateObj.length > 1) {
            if (requestStateObj[1] == requestState.toString())
                return requestStateObj[0];
        }
    }
}

function SetActionMode_DailyRequestOnAbsence(state) {
    document.getElementById('ActionMode_DailyRequestOnAbsence').innerHTML = document.getElementById("hf" + state + "_DailyRequestOnAbsence").value;
}

function CallBack_cmbLeaveType_DailyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_DailyRequestOnAbsence();
}

function CallBack_cmbDoctorName_DailyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_DailyRequestOnAbsence();
}

function CallBack_cmbIllnessName_DailyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_DailyRequestOnAbsence();
}

function CallBack_cmbMissionType_DailyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_DailyRequestOnAbsence();
}

function CallBack_cmbMissionLocation_DailyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_DailyRequestOnAbsence();
}

function ShowConnectionError_DailyRequestOnAbsence() {
    var error = document.getElementById('hfErrorType_DailyRequestOnAbsence').value;
    var errorBody = document.getElementById('hfConnectionError_DailyRequestOnAbsence').value;
    showDialog(error, errorBody, 'error');
}

function CallBack_GridRegisteredRequests_DailyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_DailyRequestOnAbsence();
}

function CollapseControls_DailyRequestOnAbsence() {
    cmbLeaveType_DailyRequestOnAbsence.collapse();
    cmbDoctorName_DailyRequestOnAbsence.collapse();
    cmbIllnessName_DailyRequestOnAbsence.collapse();
    cmbMissionType_DailyRequestOnAbsence.collapse();
    cmbMissionLocation_DailyRequestOnAbsence.collapse();
}

function tlbItemFormReconstruction_TlbDailyRequestOnAbsence_onClick() {
    var ObjDialogDailyRequestOnAbsence = parent.DialogDailyRequestOnAbsence.get_value();
    var field = ObjDialogDailyRequestOnAbsence.Field;
    DialogDailyRequestOnAbsence_onClose();
    parent.parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.ShowRelativeDialog_MasterMonthlyOperation(field);
}

function tlbItemHelp_TlbDailyRequestOnAbsence_onClick() {
    LoadHelpPage('tlbItemHelp_TlbDailyRequestOnAbsence');
}

function AttachmentUploader_Leave_DailyRequestOnAbsence_OnAfterFileUpload(StrRequestAttachment) {
    var message = null;
    if (ObjRequestAttachment_DailyRequestOnAbsence == null)
        ObjRequestAttachment_DailyRequestOnAbsence = new Object();
    ObjRequestAttachment_DailyRequestOnAbsence = eval('(' + StrRequestAttachment + ')');
    if (!ObjRequestAttachment_DailyRequestOnAbsence.IsErrorOccured)
        message = ObjRequestAttachment_DailyRequestOnAbsence.RequestAttachmentRealName;
    else {
        message = ObjRequestAttachment_DailyRequestOnAbsence.Message;
        ObjRequestAttachment_DailyRequestOnAbsence = null;
    }
    document.getElementById('tdAttachmentName_Leave_DailyRequestOnAbsence').innerHTML = message;
    Callback_AttachmentUploader_Leave_DailyRequestOnAbsence.callback();
}

function AttachmentUploader_Mission_DailyRequestOnAbsence_OnAfterFileUpload(StrRequestAttachment) {
    var message = null;
    if (ObjRequestAttachment_DailyRequestOnAbsence == null)
        ObjRequestAttachment_DailyRequestOnAbsence = new Object();
    ObjRequestAttachment_DailyRequestOnAbsence = eval('(' + StrRequestAttachment + ')');
    if (!ObjRequestAttachment_DailyRequestOnAbsence.IsErrorOccured)
        message = ObjRequestAttachment_DailyRequestOnAbsence.RequestAttachmentRealName;
    else {
        message = ObjRequestAttachment_DailyRequestOnAbsence.Message;
        ObjRequestAttachment_DailyRequestOnAbsence = null;
    }
    document.getElementById('tdAttachmentName_Mission_DailyRequestOnAbsence').innerHTML = message;
    Callback_AttachmentUploader_Mission_DailyRequestOnAbsence.callback();
}

function Callback_AttachmentUploader_Leave_DailyRequestOnAbsence_onCallBackComplete(sender, e) {
    Subgurim_AttachmentUploader_Leave_DailyRequestOnAbsenceadd('1', '4');
}

function Callback_AttachmentUploader_Leave_DailyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_DailyRequestOnAbsence();
}

function tlbItemDeleteAttachment_TlbDeleteAttachment_Leave_DailyRequestOnAbsence_onClick() {
    DeleteRequestAttachment_DailyRequestOnAbsence();
}

function Callback_AttachmentUploader_Mission_DailyRequestOnAbsence_onCallBackComplete(sender, e) {
    Subgurim_AttachmentUploader_Mission_DailyRequestOnAbsenceadd('1', '4');
}

function Callback_AttachmentUploader_Mission_DailyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_DailyRequestOnAbsence();
}

function tlbItemDeleteAttachment_TlbDeleteAttachment_Mission_DailyRequestOnAbsence_onClick() {
    DeleteRequestAttachment_DailyRequestOnAbsence();
}

function DeleteRequestAttachment_DailyRequestOnAbsence() {
    if (ObjRequestAttachment_DailyRequestOnAbsence != null && ObjRequestAttachment_DailyRequestOnAbsence.RequestAttachmentSavedName != null && ObjRequestAttachment_DailyRequestOnAbsence.RequestAttachmentSavedName != '')
        DeleteRequestAttachment_DailyRequestOnAbsencePage(CharToKeyCode_DailyRequestOnAbsence(ObjRequestAttachment_DailyRequestOnAbsence.RequestAttachmentSavedName));
}

function DeleteRequestAttachment_DailyRequestOnAbsencePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_DailyRequestOnAbsence').value;
            Response[1] = document.getElementById('hfConnectionError_DailyRequestOnAbsence').value;
        }
        if (RetMessage[2] == 'success') {
            ObjRequestAttachment_DailyRequestOnAbsence = null;
            switch (CurrentRequestState_DailyRequestOnAbsence) {
                case 'Leave':
                    document.getElementById('tdAttachmentName_Leave_DailyRequestOnAbsence').innerHTML = '';
                    break;
                case 'Mission':
                    document.getElementById('tdAttachmentName_Mission_DailyRequestOnAbsence').innerHTML = '';
                    break;
            }
        }
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}       






















