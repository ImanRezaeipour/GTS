
var box_OvertimeJustificationRequest_OvertimeJustificationRequest_IsShown = false;
var zeroTime = '00';
var CurrenPageSate_OvertimeJustificationRequest = 'View';
var ConfirmState_OvertimeJustificationRequest = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjOvertimeJustificationRequest_OvertimeJustificationRequest = null;

function InitTimePickers_OvertimeJustificationRequest() {
    SetButtonImages_TimeSelectors_DialogOvertimeJustificationRequest();
    ChangeEnabled_TimeSelectors_OvertimeJustificationRequest(false);
    ResetTimepicker_OvertimeJustificationRequest('TimeSelector_FromHour_OvertimeJustificationRequest');
    ResetTimepicker_OvertimeJustificationRequest('TimeSelector_ToHour_OvertimeJustificationRequest');
    ResetTimepicker_OvertimeJustificationRequest('TimeSelector_TimeDuration_OvertimeJustificationRequest');
}

function Set_SelectedDateTime_OvertimeJustificationRequest() {
    document.getElementById('tdSelectedDate_OvertimeJustificationRequest').innerHTML = parent.DialogOvertimeJustificationRequest.get_value().RequestDateTitle;
}

function GetBoxesHeaders_OvertimeJustificationRequest() {
    parent.document.getElementById('Title_DialogOvertimeJustificationRequest').innerHTML = document.getElementById('hfTitle_DialogOvertimeJustificationRequest').value;
    document.getElementById('header_imgbox_OvertimeJustificationRequest_OvertimeJustificationRequest').innerHTML = document.getElementById('hfheader_imgbox_OvertimeJustificationRequest_OvertimeJustificationRequest').value;
    document.getElementById('header_RegisteredRequests_OvertimeJustificationRequest').innerHTML = document.getElementById('hfheader_RegisteredRequests_OvertimeJustificationRequest').value;
    document.getElementById('cmbOverTimeType_OvertimeJustificationRequest_Input').value = document.getElementById('hfcmbAlarm_OvertimeJustificationRequest').value;
}

function ViewCurrentLangCalendars_DialogOvertimeJustificationRequest() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpFromDate_OvertimeJustificationRequest").parentNode.removeChild(document.getElementById("pdpFromDate_OvertimeJustificationRequest"));
            document.getElementById("pdpFromDate_OvertimeJustificationRequestimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_OvertimeJustificationRequestimgbt"));
            document.getElementById("pdpToDate_OvertimeJustificationRequest").parentNode.removeChild(document.getElementById("pdpToDate_OvertimeJustificationRequest"));
            document.getElementById("pdpToDate_OvertimeJustificationRequestimgbt").parentNode.removeChild(document.getElementById("pdpToDate_OvertimeJustificationRequestimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_FromDateCalendars_OvertimeJustificationRequest").removeChild(document.getElementById("Container_gCalFromDate_OvertimeJustificationRequest"));
            document.getElementById("Container_ToDateCalendars_OvertimeJustificationRequest").removeChild(document.getElementById("Container_gCalToDate_OvertimeJustificationRequest"));
            break;
    }
}

function gdpFromDate_OvertimeJustificationRequest_OnDateChange(sender, e) {
    var fromDate = gdpFromDate_OvertimeJustificationRequest.getSelectedDate();
    gCalFromDate_OvertimeJustificationRequest.setSelectedDate(fromDate);
}

function btn_gdpFromDate_OvertimeJustificationRequest_OnClick(event) {
    if (gCalFromDate_OvertimeJustificationRequest.get_popUpShowing()) {
        gCalFromDate_OvertimeJustificationRequest.hide();
    }
    else {
        gCalFromDate_OvertimeJustificationRequest.setSelectedDate(gdpFromDate_OvertimeJustificationRequest.getSelectedDate());
        gCalFromDate_OvertimeJustificationRequest.show();
    }
}

function btn_gdpFromDate_OvertimeJustificationRequest_OnMouseUp(event) {
    if (gCalFromDate_OvertimeJustificationRequest.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else
        return true;
}

function gCalFromDate_OvertimeJustificationRequest_OnChange(sender, e) {
    var fromDate = gCalFromDate_OvertimeJustificationRequest.getSelectedDate();
    gdpFromDate_OvertimeJustificationRequest.setSelectedDate(fromDate);
}

function gCalFromDate_OvertimeJustificationRequest_onLoad(sender, e) {
    window.gCalFromDate_OvertimeJustificationRequest.PopUpObject.z = 25000000;
}

function gdpToDate_OvertimeJustificationRequest_OnDateChange(sender, e) {
    var fromDate = gdpToDate_OvertimeJustificationRequest.getSelectedDate();
    gCalToDate_OvertimeJustificationRequest.setSelectedDate(fromDate);
}

function btn_gdpToDate_OvertimeJustificationRequest_OnClick(event) {
    if (gCalToDate_OvertimeJustificationRequest.get_popUpShowing()) {
        gCalToDate_OvertimeJustificationRequest.hide();
    }
    else {
        gCalToDate_OvertimeJustificationRequest.setSelectedDate(gdpToDate_OvertimeJustificationRequest.getSelectedDate());
        gCalToDate_OvertimeJustificationRequest.show();
    }
}

function btn_gdpToDate_OvertimeJustificationRequest_OnMouseUp(event) {
    if (gCalToDate_OvertimeJustificationRequest.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else
        return true;
}

function gCalToDate_OvertimeJustificationRequest_OnChange(sender, e) {
    var fromDate = gCalToDate_OvertimeJustificationRequest.getSelectedDate();
    gdpToDate_OvertimeJustificationRequest.setSelectedDate(fromDate);
}

function gCalToDate_OvertimeJustificationRequest_OnLoad(sender, e) {
    window.gCalToDate_OvertimeJustificationRequest.PopUpObject.z = 25000000;
}

function SetButtonImages_TimeSelectors_DialogOvertimeJustificationRequest() {
    SetButtonImages_TimeSelector_DialogOvertimeJustificationRequest('TimeSelector_FromHour_OvertimeJustificationRequest');
    SetButtonImages_TimeSelector_DialogOvertimeJustificationRequest('TimeSelector_ToHour_OvertimeJustificationRequest');
    SetButtonImages_TimeSelector_DialogOvertimeJustificationRequest('TimeSelector_TimeDuration_OvertimeJustificationRequest');
}

function SetButtonImages_TimeSelector_DialogOvertimeJustificationRequest(TimeSelector) {
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

function ChangeEnabled_TimeSelector_OvertimeJustificationRequest(TimeSelector, State) {
    var disabled;
    if (State)
        disabled = '';
    else {
        disabled = 'disabled';
        document.getElementById("" + TimeSelector + "_imgUp").onclick = " ";
        document.getElementById("" + TimeSelector + "_imgDown").onclick = " ";
    }

    document.getElementById("" + TimeSelector + "_txtHour").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtHour").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtSecond").disabled = disabled;

    if (State) {
        document.getElementById("" + TimeSelector + "_imgUp").onclick = function () {
            addTime(document.getElementById("" + TimeSelector + "_imgUp"), 24, 1, 1);
        };
        document.getElementById("" + TimeSelector + "_imgDown").onclick = function () {
            subtractTime(document.getElementById("" + TimeSelector + "_imgDown"), 24, 1, 1);
        };
        document.getElementById("" + TimeSelector + "_txtHour").onchange = function () {
            CheckTimeSelectorPartValue_OvertimeJustificationRequest(TimeSelector, '_txtHour');
        };
        document.getElementById("" + TimeSelector + "_txtMinute").onchange = function () {
            CheckTimeSelectorPartValue_OvertimeJustificationRequest(TimeSelector, '_txtMinute');
        };
    }
}

function OvertimeJustificationRequest_onKeyDown(event) {
    var activeID = null;
    if (event.keyCode == 38 || event.keyCode == 40) {
        activeID = document.activeElement.id;
        CheckTimePickerState_OvertimeJustificationRequest(activeID);
    }
}

function CheckTimePickerState_OvertimeJustificationRequest(TimeSelector) {
    if (((TimeSelector == 'TimeSelector_FromHour_OvertimeJustificationRequest_txtHour' || TimeSelector == 'TimeSelector_ToHour_OvertimeJustificationRequest_txtHour' && TimeSelector == 'TimeSelector_TimeDuration_OvertimeJustificationRequest_txtHour') && (document.getElementById(TimeSelector).value == '-1' || isNaN(document.getElementById(TimeSelector).value))) || ((TimeSelector == 'TimeSelector_FromHour_OvertimeJustificationRequest_txtMinute' || TimeSelector == 'TimeSelector_ToHour_OvertimeJustificationRequest_txtMinute' && TimeSelector == 'TimeSelector_TimeDuration_OvertimeJustificationRequest_txtMinute') && isNaN(document.getElementById(TimeSelector).value))) {
        document.getElementById(TimeSelector).value = zeroTime;
        return;
    }
}

function CheckTimeSelectorPartValue_OvertimeJustificationRequest(TimeSelectorPartID, identifier) {
    if (document.getElementById(TimeSelectorPartID + identifier).value == "") {
        if (document.getElementById(TimeSelectorPartID + identifier).value == "")
            document.getElementById(TimeSelectorPartID + identifier).value = zeroTime;
    }
}

function imgbox_OvertimeJustificationRequest_OvertimeJustificationRequest_onClick() {
    if (!box_OvertimeJustificationRequest_OvertimeJustificationRequest_IsShown)
        OvertimeJustificationRequest_onInsert();
    else
        OvertimeJustificationRequest_onCancel();
}

function OvertimeJustificationRequest_onInsert() {
    ChangePageState_DialogOvertimeJustificationRequest('Add');
    box_OvertimeJustificationRequest_OvertimeJustificationRequest_onShowHide();
}

function box_OvertimeJustificationRequest_OvertimeJustificationRequest_onShowHide() {
    setSlideDownSpeed(200);
    slidedown_showHide('box_OvertimeJustificationRequest_OvertimeJustificationRequest');

    if (box_OvertimeJustificationRequest_OvertimeJustificationRequest_IsShown) {
        box_OvertimeJustificationRequest_OvertimeJustificationRequest_IsShown = false;
        ClearList_OvertimeJustificationRequest();
        document.getElementById('imgbox_OvertimeJustificationRequest_OvertimeJustificationRequest').src = 'Images/Ghadir/arrowDown.jpg';
        try {
            updateDateField('pdpFromDate_OvertimeJustificationRequest');
            updateDateField('pdpToDate_OvertimeJustificationRequest');
        }
        catch (error) {
        }
    }
    else {
        box_OvertimeJustificationRequest_OvertimeJustificationRequest_IsShown = true;
        document.getElementById('imgbox_OvertimeJustificationRequest_OvertimeJustificationRequest').src = 'Images/Ghadir/arrowUp.jpg';
    }
}

function ClearList_OvertimeJustificationRequest() {
    cmbOverTimeType_OvertimeJustificationRequest.unSelect();
    document.getElementById('cmbOverTimeType_OvertimeJustificationRequest_Input').value = document.getElementById('hfcmbAlarm_OvertimeJustificationRequest').value;
    document.getElementById('txtDescription_OvertimeJustificationRequest').value = '';
    ResetCalendars_OvertimeJustificationRequest();
    ResetTimepicker_OvertimeJustificationRequest('TimeSelector_FromHour_OvertimeJustificationRequest');
    ResetTimepicker_OvertimeJustificationRequest('TimeSelector_ToHour_OvertimeJustificationRequest');
    ResetTimepicker_OvertimeJustificationRequest('TimeSelector_TimeDuration_OvertimeJustificationRequest');
}

function ResetTimepicker_OvertimeJustificationRequest(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}

function ResetCalendars_OvertimeJustificationRequest() {
    var ObjRequest = parent.DialogOvertimeJustificationRequest.get_value();
    var currentDate_OvertimeJustificationRequest = ObjRequest.RequestUIDate;
    switch (parent.parent.SysLangID) {
        case 'en-US':
            currentDate_OvertimeJustificationRequest = new Date(currentDate_OvertimeJustificationRequest);
            gdpFromDate_OvertimeJustificationRequest.setSelectedDate(currentDate_OvertimeJustificationRequest);
            gCalFromDate_OvertimeJustificationRequest.setSelectedDate(currentDate_OvertimeJustificationRequest);
            gdpToDate_OvertimeJustificationRequest.setSelectedDate(currentDate_OvertimeJustificationRequest);
            gCalToDate_OvertimeJustificationRequest.setSelectedDate(currentDate_OvertimeJustificationRequest);
            break;
        case 'fa-IR':
            document.getElementById('pdpFromDate_OvertimeJustificationRequest').value = currentDate_OvertimeJustificationRequest;
            document.getElementById('pdpToDate_OvertimeJustificationRequest').value = currentDate_OvertimeJustificationRequest;
            break;
    }
}

function SetPosition_DropDownDives_DialogOvertimeJustificationRequest() {
    if (parent.parent.CurrentLangID == 'fa-IR')
        document.getElementById('box_OvertimeJustificationRequest_OvertimeJustificationRequest').style.right = '144px';
    if (parent.parent.CurrentLangID == 'en-US')
        document.getElementById('box_OvertimeJustificationRequest_OvertimeJustificationRequest').style.left = '144px';
}

function chbTimePeriod_OvertimeJustificationRequest_onClick() {
    var state;
    if (document.getElementById('chbTimePeriod_OvertimeJustificationRequest').checked) {
        state = true;
        if (document.getElementById('chbTimeDuration_OvertimeJustificationRequest').checked)
            ChangeEnabled_TimeChbs_OvertimeJustificationRequest('disabled');
    }
    else {
        state = false;
        if (document.getElementById('chbTimeDuration_OvertimeJustificationRequest').checked)
            ChangeEnabled_TimeChbs_OvertimeJustificationRequest('');
    }
    ChangeEnabled_TimeSelector_OvertimeJustificationRequest('TimeSelector_FromHour_OvertimeJustificationRequest', state);
    ChangeEnabled_TimeSelector_OvertimeJustificationRequest('TimeSelector_ToHour_OvertimeJustificationRequest', state);
}

function chbTimeDuration_OvertimeJustificationRequest_onClick() {
    var state;
    var TimeChbsEnabledState;
    if (document.getElementById('chbTimeDuration_OvertimeJustificationRequest').checked) {
        state = true;
        if (document.getElementById('chbTimePeriod_OvertimeJustificationRequest').checked)
            TimeChbsEnabledState = 'disabled';
        else
            TimeChbsEnabledState = '';
    }
    else {
        state = false;
        TimeChbsEnabledState = 'disabled';
    }
    ChangeEnabled_TimeSelector_OvertimeJustificationRequest('TimeSelector_TimeDuration_OvertimeJustificationRequest', state);
    ChangeEnabled_TimeChbs_OvertimeJustificationRequest(TimeChbsEnabledState);
}

function ChangeEnabled_TimeChbs_OvertimeJustificationRequest(State) {
    document.getElementById('chbBeforeTime_OvertimeJustificationRequest').disabled = State;
    document.getElementById('chbMiddleTime_OvertimeJustificationRequest').disabled = State;
    document.getElementById('chblAfterTime_OvertimeJustificationRequest').disabled = State;
}

function ChangeEnabled_TimeSelectors_OvertimeJustificationRequest(state) {
    ChangeEnabled_TimeSelector_OvertimeJustificationRequest('TimeSelector_FromHour_OvertimeJustificationRequest', state);
    ChangeEnabled_TimeSelector_OvertimeJustificationRequest('TimeSelector_ToHour_OvertimeJustificationRequest', state);
    ChangeEnabled_TimeSelector_OvertimeJustificationRequest('TimeSelector_TimeDuration_OvertimeJustificationRequest', state);
}

function ShowDialogShiftsView() {
    var ObjDialogOvertimeJustificationRequest = parent.DialogOvertimeJustificationRequest.get_value();
    var ObjDialogShiftsView = new Object();
    ObjDialogShiftsView.RequestDate = ObjDialogOvertimeJustificationRequest.RequestDate;
    ObjDialogShiftsView.RequestCaller = ObjDialogOvertimeJustificationRequest.RequestCaller;
    ObjDialogShiftsView.LoadState = ObjDialogOvertimeJustificationRequest.LoadState;
    DialogShiftsView.set_value(ObjDialogShiftsView);
    DialogShiftsView.Show();
    CollapseControls_OvertimeJustificationRequest();
}

function tlbItemSave_TlbOvertimeJustificationRequest_onClick() {
    OvertimeJustificationRequest_onSave();
}

function tlbItemCancel_TlbOvertimeJustificationRequest_onClick() {
    OvertimeJustificationRequest_onCancel();
}

function OvertimeJustificationRequest_onCancel() {
    SetBaseState_OvertimeJustificationRequest();
}

function SetBaseState_OvertimeJustificationRequest() {
    if (CurrenPageSate_OvertimeJustificationRequest != 'Delete')
        box_OvertimeJustificationRequest_OvertimeJustificationRequest_onShowHide();
    ChangePageState_DialogOvertimeJustificationRequest('View');
}

function OvertimeJustificationRequest_onSave() {
    if (CurrenPageSate_OvertimeJustificationRequest != 'Delete')
        UpdateRequest_OvertimeJustificationRequest();
    else
        ShowDialogConfirm('Delete');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_OvertimeJustificationRequest = confirmState;
    if (CurrenPageSate_OvertimeJustificationRequest == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_OvertimeJustificationRequest').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_OvertimeJustificationRequest').value;
    DialogConfirm.Show();
    CollapseControls_OvertimeJustificationRequest();
}

function UpdateRequest_OvertimeJustificationRequest() {
    ObjOvertimeJustificationRequest_OvertimeJustificationRequest = new Object();
    ObjOvertimeJustificationRequest_OvertimeJustificationRequest.ID = '0';
    ObjOvertimeJustificationRequest_OvertimeJustificationRequest.PreCardID = '0';
    ObjOvertimeJustificationRequest_OvertimeJustificationRequest.PreCardTitle = null;
    ObjOvertimeJustificationRequest_OvertimeJustificationRequest.RequestDate = null;
    ObjOvertimeJustificationRequest_OvertimeJustificationRequest.FromDate = null;
    ObjOvertimeJustificationRequest_OvertimeJustificationRequest.ToDate = null;
    ObjOvertimeJustificationRequest_OvertimeJustificationRequest.FromTime = null;
    ObjOvertimeJustificationRequest_OvertimeJustificationRequest.ToTime = null;
    ObjOvertimeJustificationRequest_OvertimeJustificationRequest.Duration = null;
    ObjOvertimeJustificationRequest_OvertimeJustificationRequest.Description = null;

    var ObjDialogOvertimeJustificationRequest = parent.DialogOvertimeJustificationRequest.get_value();

    var SelectedItems_GridRegisteredRequests_OvertimeJustificationRequest = GridRegisteredRequests_OvertimeJustificationRequest.getSelectedItems();
    if (SelectedItems_GridRegisteredRequests_OvertimeJustificationRequest.length > 0)
        ObjOvertimeJustificationRequest_OvertimeJustificationRequest.ID = SelectedItems_GridRegisteredRequests_OvertimeJustificationRequest[0].getMember("ID").get_text();

    if (CurrenPageSate_OvertimeJustificationRequest != 'Delete') {
        ObjOvertimeJustificationRequest_OvertimeJustificationRequest.RequestDate = parent.DialogOvertimeJustificationRequest.get_value().RequestDate;
        if (cmbOverTimeType_OvertimeJustificationRequest.getSelectedItem() != undefined) {
            ObjOvertimeJustificationRequest_OvertimeJustificationRequest.PreCardID = cmbOverTimeType_OvertimeJustificationRequest.getSelectedItem().get_value();
            ObjOvertimeJustificationRequest_OvertimeJustificationRequest.PreCardTitle = cmbOverTimeType_OvertimeJustificationRequest.getSelectedItem().get_text();
        }
        ObjOvertimeJustificationRequest_OvertimeJustificationRequest.FromTime = GetDuration_TimePicker_OvertimeJustificationRequest('TimeSelector_FromHour_OvertimeJustificationRequest');
        ObjOvertimeJustificationRequest_OvertimeJustificationRequest.ToTime = GetDuration_TimePicker_OvertimeJustificationRequest('TimeSelector_ToHour_OvertimeJustificationRequest');
        ObjOvertimeJustificationRequest_OvertimeJustificationRequest.Duration = GetDuration_TimePicker_OvertimeJustificationRequest('TimeSelector_TimeDuration_OvertimeJustificationRequest');
        ObjOvertimeJustificationRequest_OvertimeJustificationRequest.Description = document.getElementById('txtDescription_OvertimeJustificationRequest').value;

        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                ObjOvertimeJustificationRequest_OvertimeJustificationRequest.FromDate = document.getElementById('pdpFromDate_OvertimeJustificationRequest').value;
                ObjOvertimeJustificationRequest_OvertimeJustificationRequest.ToDate = document.getElementById('pdpToDate_OvertimeJustificationRequest').value;
                break;
            case 'en-US':
                ObjOvertimeJustificationRequest_OvertimeJustificationRequest.FromDate = document.getElementById('gdpFromDate_OvertimeJustificationRequest_picker').value;
                ObjOvertimeJustificationRequest_OvertimeJustificationRequest.ToDate = document.getElementById('gdpToDate_OvertimeJustificationRequest_picker').value;
                break;
        }

    }
    UpdateRequest_OvertimeJustificationRequestPage(CharToKeyCode_OvertimeJustificationRequest(ObjDialogOvertimeJustificationRequest.RequestCaller), CharToKeyCode_OvertimeJustificationRequest(ObjDialogOvertimeJustificationRequest.LoadState), CharToKeyCode_OvertimeJustificationRequest(CurrenPageSate_OvertimeJustificationRequest), CharToKeyCode_OvertimeJustificationRequest(ObjOvertimeJustificationRequest_OvertimeJustificationRequest.ID), CharToKeyCode_OvertimeJustificationRequest(ObjOvertimeJustificationRequest_OvertimeJustificationRequest.PreCardID), CharToKeyCode_OvertimeJustificationRequest(ObjOvertimeJustificationRequest_OvertimeJustificationRequest.RequestDate), CharToKeyCode_OvertimeJustificationRequest(ObjOvertimeJustificationRequest_OvertimeJustificationRequest.FromDate), CharToKeyCode_OvertimeJustificationRequest(ObjOvertimeJustificationRequest_OvertimeJustificationRequest.ToDate), CharToKeyCode_OvertimeJustificationRequest(ObjOvertimeJustificationRequest_OvertimeJustificationRequest.FromTime), CharToKeyCode_OvertimeJustificationRequest(ObjOvertimeJustificationRequest_OvertimeJustificationRequest.ToTime), CharToKeyCode_OvertimeJustificationRequest(ObjOvertimeJustificationRequest_OvertimeJustificationRequest.Duration), CharToKeyCode_OvertimeJustificationRequest(ObjOvertimeJustificationRequest_OvertimeJustificationRequest.Description));
    DialogWaiting.Show();
}

function UpdateRequest_OvertimeJustificationRequestPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_OvertimeJustificationRequest').value;
            Response[1] = document.getElementById('hfConnectionError_OvertimeJustificationRequest').value;
        }
        if (RetMessage[2] == 'success') {
            OvertimeJustificationRequest_OnAfterUpdate(Response);
            ClearList_OvertimeJustificationRequest();
            SetBaseState_OvertimeJustificationRequest();
        }
        else {
            if (CurrenPageSate_OvertimeJustificationRequest == 'Delete')
                ChangePageState_DialogOvertimeJustificationRequest('View');
        }
    }
}

function OvertimeJustificationRequest_OnAfterUpdate(Response) {
    if (ObjOvertimeJustificationRequest_OvertimeJustificationRequest != null) {
        var PreCardTitle = ObjOvertimeJustificationRequest_OvertimeJustificationRequest.PreCardTitle;
        var FromDate = ObjOvertimeJustificationRequest_OvertimeJustificationRequest.FromDate;
        var ToDate = ObjOvertimeJustificationRequest_OvertimeJustificationRequest.ToDate;
        var FromTime = ObjOvertimeJustificationRequest_OvertimeJustificationRequest.FromTime;
        var ToTime = ObjOvertimeJustificationRequest_OvertimeJustificationRequest.ToTime;
        var Duration = ObjOvertimeJustificationRequest_OvertimeJustificationRequest.Duration;
        var RegisterDate = '';
        var RequestState = '';
        var RequestStateTitle = '';

        var RegisteredRequestItem = null;

        GridRegisteredRequests_OvertimeJustificationRequest.beginUpdate();
        switch (CurrenPageSate_OvertimeJustificationRequest) {
            case 'Add':
                RegisteredRequestItem = GridRegisteredRequests_OvertimeJustificationRequest.get_table().addEmptyRow(GridRegisteredRequests_OvertimeJustificationRequest.get_recordCount());
                RegisteredRequestItem.setValue(0, Response[3], false);
                GridRegisteredRequests_OvertimeJustificationRequest.selectByKey(Response[3], 0, false);
                RequestStateTitle = GetRequestStateTitle_OvertimeJustificationRequest(Response[4]);
                RequestState = Response[4];
                RegisterDate = Response[5];
                break;
            case 'Delete':
                GridRegisteredRequests_OvertimeJustificationRequest.selectByKey(ObjOvertimeJustificationRequest_OvertimeJustificationRequest.ID, 0, false);
                GridRegisteredRequests_OvertimeJustificationRequest.deleteSelected();
                break;
        }
        if (CurrenPageSate_OvertimeJustificationRequest != 'Delete') {
            RegisteredRequestItem.setValue(1, PreCardTitle, false);
            RegisteredRequestItem.setValue(2, FromDate, false);
            RegisteredRequestItem.setValue(3, ToDate, false);
            RegisteredRequestItem.setValue(4, FromTime, false);
            RegisteredRequestItem.setValue(5, ToTime, false);
            RegisteredRequestItem.setValue(6, Duration, false);
            RegisteredRequestItem.setValue(7, RegisterDate, false);
            RegisteredRequestItem.setValue(8, RequestStateTitle, false);
            RegisteredRequestItem.setValue(9, RequestState, false);
        }
        GridRegisteredRequests_OvertimeJustificationRequest.endUpdate();
    }
}

function GetDuration_TimePicker_OvertimeJustificationRequest(TimePicker) {
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


function tlbItemDelete_TlbOvertimeJustificationRequest_onClick() {
    ChangePageState_DialogOvertimeJustificationRequest('Delete');
}

function ChangePageState_DialogOvertimeJustificationRequest(state) {
    CurrenPageSate_OvertimeJustificationRequest = state;
    SetActionMode_OvertimeJustificationRequest(state);
    if (CurrenPageSate_OvertimeJustificationRequest == 'Add' || CurrenPageSate_OvertimeJustificationRequest == 'Delete') {
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemDelete_TlbOvertimeJustificationRequest').set_enabled(false);
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemDelete_TlbOvertimeJustificationRequest').set_imageUrl('remove_silver.png');
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemSave_TlbOvertimeJustificationRequest').set_enabled(true);
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemSave_TlbOvertimeJustificationRequest').set_imageUrl('save.png');
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemCancel_TlbOvertimeJustificationRequest').set_enabled(true);
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemCancel_TlbOvertimeJustificationRequest').set_imageUrl('cancel.png');
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemExit_TlbOvertimeJustificationRequest').set_enabled(false);
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemExit_TlbOvertimeJustificationRequest').set_imageUrl('exit_silver.png');
        ChangeEnabled_TimeSelectors_OvertimeJustificationRequest(true);
        if (state == 'Delete')
            OvertimeJustificationRequest_onSave();
    }
    if (CurrenPageSate_OvertimeJustificationRequest == 'View') {
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemDelete_TlbOvertimeJustificationRequest').set_enabled(true);
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemDelete_TlbOvertimeJustificationRequest').set_imageUrl('remove.png');
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemSave_TlbOvertimeJustificationRequest').set_enabled(false);
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemSave_TlbOvertimeJustificationRequest').set_imageUrl('save_silver.png');
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemCancel_TlbOvertimeJustificationRequest').set_enabled(false);
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemCancel_TlbOvertimeJustificationRequest').set_imageUrl('cancel_silver.png');
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemExit_TlbOvertimeJustificationRequest').set_enabled(true);
        TlbOvertimeJustificationRequest.get_items().getItemById('tlbItemExit_TlbOvertimeJustificationRequest').set_imageUrl('exit.png');
        ChangeEnabled_TimeSelectors_OvertimeJustificationRequest(false);
    }
}

function tlbItemShiftView_TlbOvertimeJustificationRequest_onClick() {
    ShowDialogShiftsView();
}

function tlbItemExit_TlbOvertimeJustificationRequest_onClick() {
    ShowDialogConfirm('Exit');
}

function cmbOverTimeType_OvertimeJustificationRequest_onExpand(sender, e) {
    if (cmbOverTimeType_OvertimeJustificationRequest.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbOverTimeType_OvertimeJustificationRequest == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbOverTimeType_OvertimeJustificationRequest = true;
        CallBack_cmbOverTimeType_OvertimeJustificationRequest.callback();
    }
}

function cmbOverTimeType_OvertimeJustificationRequest_onCollapse(sender, e) {
    if (cmbOverTimeType_OvertimeJustificationRequest.getSelectedItem() == undefined)
        document.getElementById('cmbOverTimeType_OvertimeJustificationRequest_Input').value = document.getElementById('hfcmbAlarm_OvertimeJustificationRequest').value;
}

function CallBack_cmbOverTimeType_OvertimeJustificationRequest_onBeforeCallback(sender, e) {
    cmbOverTimeType_OvertimeJustificationRequest.dispose();
}

function CallBack_cmbOverTimeType_OvertimeJustificationRequest_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_OverTimeTypes').value;
    if (error == "") {
        document.getElementById('cmbOverTimeType_OvertimeJustificationRequest_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbOverTimeType_OvertimeJustificationRequest_DropImage').mousedown();
        cmbOverTimeType_OvertimeJustificationRequest.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbOverTimeType_OvertimeJustificationRequest_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function Refresh_GridRegisteredRequests_OvertimeJustificationRequest() {
    Fill_GridRegisteredRequests_OvertimeJustificationRequest();
}

function Fill_GridRegisteredRequests_OvertimeJustificationRequest() {
    document.getElementById('loadingPanel_GridRegisteredRequests_OvertimeJustificationRequest').innerHTML = document.getElementById('hfloadingPanel_GridRegisteredRequests_OvertimeJustificationRequest').value;
    var ObjRequest = parent.DialogOvertimeJustificationRequest.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    var DateKey = ObjRequest.DateKey;
    var RequestDate = ObjRequest.RequestDate;
    CallBack_GridRegisteredRequests_OvertimeJustificationRequest.callback(CharToKeyCode_OvertimeJustificationRequest(RequestCaller), CharToKeyCode_OvertimeJustificationRequest(DateKey), CharToKeyCode_OvertimeJustificationRequest(RequestDate));
}

function CharToKeyCode_OvertimeJustificationRequest(str) {
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

function GridRegisteredRequests_OvertimeJustificationRequest_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridRegisteredRequests_OvertimeJustificationRequest').innerHTML = '';
}

function CallBack_GridRegisteredRequests_OvertimeJustificationRequest_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RegisteredRequests').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridRegisteredRequests_OvertimeJustificationRequest();
    }
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_OvertimeJustificationRequest) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateRequest_OvertimeJustificationRequest();
            break;
        case 'Exit':
            DialogOvertimeJustificationRequest_onClose();
            break;
        default:
    }
}

function DialogOvertimeJustificationRequest_onClose() {
    parent.document.getElementById('DialogOvertimeJustificationRequest_IFrame').src = 'WhitePage.aspx';
    parent.DialogOvertimeJustificationRequest.Close();
    var ObjRequest = parent.DialogOvertimeJustificationRequest.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    if(RequestCaller == 'Grid')
       parent.parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_DialogOvertimeJustificationRequest('View');
}

function GetRequestStateTitle_OvertimeJustificationRequest(requestState) {
    var RequestStates = document.getElementById('hfRequestStates_OvertimeJustificationRequest').value.split('#');
    for (var i = 0; i < RequestStates.length; i++) {
        var requestStateObj = RequestStates[i].split(':');
        if (requestStateObj.length > 1) {
            if (requestStateObj[1] == requestState.toString())
                return requestStateObj[0];
        }
    }
}

function SetActionMode_OvertimeJustificationRequest(state) {
    document.getElementById('ActionMode_OvertimeJustificationRequest').innerHTML = document.getElementById("hf" + state + "_OvertimeJustificationRequest").value;
}

function CallBack_cmbOverTimeType_OvertimeJustificationRequest_onCallbackError(sender, e) {
    ShowConnectionError_OvertimeJustificationRequest();
}

function ShowConnectionError_OvertimeJustificationRequest() {
    var error = document.getElementById('hfErrorType_OvertimeJustificationRequest').value;
    var errorBody = document.getElementById('hfConnectionError_OvertimeJustificationRequest').value;
    showDialog(error, errorBody, 'error');
}

function CallBack_GridRegisteredRequests_OvertimeJustificationRequest_onCallbackError(sender, e) {
    ShowConnectionError_OvertimeJustificationRequest();
}

function CollapseControls_OvertimeJustificationRequest() {
    cmbOverTimeType_OvertimeJustificationRequest.collapse();
}

function tlbItemFormReconstruction_TlbOvertimeJustificationRequest_onClick() {
    var ObjDialogOvertimeJustificationRequest = parent.DialogOvertimeJustificationRequest.get_value();
    var field = ObjDialogOvertimeJustificationRequest.Field;
    DialogOvertimeJustificationRequest_onClose();
    parent.parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.ShowRelativeDialog_MasterMonthlyOperation(field);
}

function tlbItemHelp_TlbOvertimeJustificationRequest_onClick() {
    LoadHelpPage('tlbItemHelp_TlbOvertimeJustificationRequest');
}

















