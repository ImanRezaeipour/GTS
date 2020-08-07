
var CurrentPageState_LeaveReserve = 'View';
var ConfirmState_LeaveReserve = null;
var ObjLeaveReserve_LeaveReserve = new Object();
var CurrentPageCombosCallBcakStateObj = new Object();

function GetBoxesHeaders_LeaveReserve() {
    parent.document.getElementById('Title_DialogLeaveReserve').innerHTML = document.getElementById('hfTitle_DialogLeaveReserve').value;
    document.getElementById('header_LeaveReserve_LeaveReserve').innerHTML = document.getElementById('hfheader_LeaveReserve_LeaveReserve').value;
    document.getElementById('cmbOperationType_LeaveReserve_Input').value = document.getElementById('hfcmbAlarm_LeaveReserve').value;
}

function ViewCurrentLangCalendars_LeaveReserve() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpDate_LeaveReserve").parentNode.removeChild(document.getElementById("pdpDate_LeaveReserve"));
            document.getElementById("pdpDate_LeaveReserveimgbt").parentNode.removeChild(document.getElementById("pdpDate_LeaveReserveimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_DateCalendars_LeaveReserve").removeChild(document.getElementById("Container_gCalDate_LeaveReserve"));
            break;
    }
}

function btn_gdpDate_LeaveReserve_OnMouseUp(event) {
    if (gCalDate_LeaveReserve.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function btn_gdpDate_LeaveReserve_OnClick(event) {
    if (gCalDate_LeaveReserve.get_popUpShowing()) {
        gCalDate_LeaveReserve.hide();
    }
    else {
        gCalDate_LeaveReserve.setSelectedDate(gdpDate_LeaveReserve.getSelectedDate());
        gCalDate_LeaveReserve.show();
    }
}

function gdpDate_LeaveReserve_OnDateChange(sender, e) {
    var Date = gdpDate_LeaveReserve.getSelectedDate();
    gCalDate_LeaveReserve.setSelectedDate(Date);
}

function gCalDate_LeaveReserve_OnChange(sender, e) {
    var Date = gCalDate_LeaveReserve.getSelectedDate();
    gdpDate_LeaveReserve.setSelectedDate(Date);
}

function gCalDate_LeaveReserve_OnLoad(sender, e) {
    window.gCalDate_LeaveReserve.PopUpObject.z = 25000000;
}

function SetActionMode_LeaveReserve(state) {
    document.getElementById('ActionMode_LeaveReserve').innerHTML = document.getElementById("hf" + state + "_LeaveReserve").value;
}

function ClearList_LeaveReserve() {
    cmbOperationType_LeaveReserve.unSelect();
    document.getElementById('cmbOperationType_LeaveReserve_Input').value = '';
    document.getElementById('txtDay_LeaveReserve').value = '';
    ResetTimepicker_LeaveReserve('TimeSelector_Hour_LeaveReserve');
    ResetCalendars_LeaveReserve();
}

function ResetCalendars_LeaveReserve() {
    var currentDate_LeaveReserve = document.getElementById('hfCurrentDate_LeaveReserve').value;
    switch (parent.parent.SysLangID) {
        case 'en-US':
            currentDate_LeaveReserve = new Date(currentDate_LeaveReserve);
            gdpDate_LeaveReserve.setSelectedDate(currentDate_LeaveReserve);
            gCalDate_LeaveReserve.setSelectedDate(currentDate_LeaveReserve);
            break;
        case 'fa-IR':
            document.getElementById('pdpDate_LeaveReserve').value = currentDate_LeaveReserve;
            break;
    }
}

function tlbItemNew_TlbLeaveReserve_onClick() {
    ChangePageState_LeaveReserve('Add');
    ClearList_LeaveReserve();
}

function tlbItemDelete_TlbLeaveReserve_onClick() {
    ChangePageState_LeaveReserve('Delete');
}

function tlbItemSave_TlbLeaveReserve_onClick() {
    CollapseControls_LeaveReserve();
    LeaveReserve_onSave();
}

function tlbItemCancel_TlbLeaveReserve_onClick() {
    ChangePageState_LeaveReserve('View');
    ClearList_LeaveReserve();
}

function tlbItemExit_TlbLeaveReserve_onClick() {
    ShowDialogConfirm('Exit');
}

function GridLeaveReserve_LeaveReserve_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridLeaveReserve_LeaveReserve').innerHTML = '';
}

function CallBack_GridLeaveReserve_LeaveReserve_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_LeaveReserve').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridLeaveReserve_LeaveReserve();
    }
}

function Fill_GridLeaveReserve_LeaveReserve() {
    document.getElementById('loadingPanel_GridLeaveReserve_LeaveReserve').innerHTML = document.getElementById('hfloadingPanel_GridLeaveReserve_LeaveReserve').value;
    var ObjDialogLeaveReserve = parent.DialogLeaveReserve.get_value();
    var personnelID = ObjDialogLeaveReserve.PersonnelID;
    CallBack_GridLeaveReserve_LeaveReserve.callback(CharToKeyCode_LeaveReserve(personnelID));
}

function CallBack_GridLeaveReserve_LeaveReserve_onCallbackError(sender, e) {
    ShowConnectionError_LeaveReserve();
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CharToKeyCode_LeaveReserve(str) {
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

function ShowConnectionError_LeaveReserve() {
    var error = document.getElementById('hfErrorType_LeaveReserve').value;
    var errorBody = document.getElementById('hfConnectionError_LeaveReserve').value;
    showDialog(error, errorBody, 'error');
}

function Init_TimeSelectors_LeaveReserve() {
    FetchRelativeOperation_TimePickers_LeaveReserve('Reset');
    FetchRelativeOperation_TimePickers_LeaveReserve('ChangeFloat');
    FetchRelativeOperation_TimePickers_LeaveReserve('ChangeButtonImage');
    FetchRelativeOperation_TimePickers_LeaveReserve('ChangeAction');
}

function FetchRelativeOperation_TimePickers_LeaveReserve(ActionType) {
    var RelativeOperation = null;
    switch (ActionType) {
        case 'Reset':
            RelativeOperation = 'ResetTimepicker_LeaveReserve';
            break;
        case 'ChangeFloat':
            RelativeOperation = 'ChangeFloat_TimeSelector_LeaveReserve';
            break;
        case 'ChangeButtonImage':
            RelativeOperation = 'SetButtonImages_TimeSelector_LeaveReserve';
            break;
        case 'ChangeAction':
            RelativeOperation = 'ChangeTimePickerActions_TimeSelector_LeaveReserve';
            break;
        case 'ChangeEnabled':
            RelativeOperation = 'ChangeTimePickerEnabled_LeaveReserve';
            break;
    }
    eval(RelativeOperation + '("TimeSelector_Hour_LeaveReserve")');
}

function Init_DayBoxes_LeaveReserve() {
    ChangeDayBoxEnabled_LeaveReserve('txtDay_LeaveReserve', 'disable');
}

function ChangeDayBoxEnabled_LeaveReserve(dayBoxID, state) {
    var disabled = null;
    switch (state) {
        case 'disable':
            disabled = 'disabled';
            break;
        case 'enable':
            disabled = '';
            break;
    }
    document.getElementById(dayBoxID).disabled = disabled;
}

function ChangeTimePickerEnabled_LeaveReserve(TimeSelector, state) {
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
                CheckTimePickerState_LeaveReserve(TimeSelector + '_txtHour');
                CheckTimePickerState_LeaveReserve(TimeSelector + '_txtMinute');
                addTime(document.getElementById("" + TimeSelector + "_imgUp"), 24, 1, 1);
            };
            document.getElementById("" + TimeSelector + "_imgDown").onclick = function () {
                CheckTimePickerState_LeaveReserve(TimeSelector + '_txtHour');
                CheckTimePickerState_LeaveReserve(TimeSelector + '_txtMinute');
                subtractTime(document.getElementById("" + TimeSelector + "_imgDown"), 24, 1, 1);
            };
            break;

    }
    document.getElementById("" + TimeSelector + "_txtHour").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtHour").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtSecond").disabled = disabled;
}

function CheckTimePickerState_LeaveReserve(TimeSelector) {
    if ((TimeSelector == 'TimeSelector_Hour_LeaveReserve_txtHour' && (document.getElementById(TimeSelector).value == '-1' || isNaN(document.getElementById(TimeSelector).value))) || (TimeSelector == 'TimeSelector_Hour_LeaveReserve_txtMinute' && isNaN(document.getElementById(TimeSelector).value))) {
        document.getElementById(TimeSelector).value = zeroTime;
        return;
    }
}

function ResetTimepicker_LeaveReserve(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}

function ChangeTimePickerActions_TimeSelector_LeaveReserve(TimeSelector) {
    document.getElementById("" + TimeSelector + "_txtHour").onchange = function () {
        TimeSelector_LeaveReserve_onChange(TimeSelector, '_txtHour');
    };
    document.getElementById("" + TimeSelector + "_txtMinute").onchange = function () {
        TimeSelector_LeaveReserve_onChange(TimeSelector, '_txtMinute');
    };
}

function TimeSelector_LeaveReserve_onChange(TimeSelector, partID) {
    var id = TimeSelector + partID;
    var val = document.getElementById(id).value;
    val = !isNaN(parseFloat(val)) ? parseFloat(val) > 0 ? "" + parseFloat(val) + "" : '00' : '00';
    switch (partID) {
        case 'txtHour':
            break;
        case 'txtMinute':
            val = parseFloat(val) < 60 ? val : '59';
            break;
    }
    document.getElementById(id).value = val.length == 2 ? val : '0' + val;
}

function DayBox_LeaveReserve_onChange(dayBoxID) {
    var val = document.getElementById(dayBoxID).value;
    val = !isNaN(parseFloat(val)) ? parseFloat(val) > 0 ? "" + parseFloat(val) + "" : '0' : '0';
    document.getElementById(dayBoxID).value = val;
}

function ChangeFloat_TimeSelector_LeaveReserve(TimeSelector) {
    var align = null;
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            align = 'right';
            break;
        case 'en-US':
            align = 'left';
            break;
    }
    document.getElementById(TimeSelector).style.styleFloat = align;
    document.getElementById(TimeSelector).style.cssFloat = align;
}

function SetButtonImages_TimeSelector_LeaveReserve(TimeSelector) {
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
    try {
        if (document.activeElement.id != "" + TimeSelector + "_txtHour" && document.activeElement.id != "" + TimeSelector + "_txtMinute" && document.activeElement.id != "" + TimeSelector + "_txtSecond")
            document.getElementById("" + TimeSelector + "_txtHour").focus();
    }
    catch (error) {
    }
}

function ChangeCalendarsEnabled_LeaveReserve(state) {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ChangeCalendarEnabled_LeaveReserve('pdpDate_LeaveReserve', state);
            break;
        case 'en-US':
            ChangeCalendarEnabled_LeaveReserve('gdpDate_LeaveReserve', state);
            break;
    }
}

function ChangeCalendarEnabled_LeaveReserve(cal, state) {
    var disabled = null;
    switch (state) {
        case 'disable':
            disabled = 'disabled';
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    document.getElementById(cal).onclick = " ";
                    document.getElementById(cal + 'imgbt').onclick = " ";
                    break;
                case 'en-US':
                    document.getElementById('btn_' + cal).onclick = " ";
                    break;
            }
            break;
        case 'enable':
            disabled = '';
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    document.getElementById(cal).onclick = function () {
                        displayDatePicker(cal);
                    };
                    document.getElementById(cal + 'imgbt').onclick = function () {
                        displayDatePicker(cal);
                    };
                    break;
                case 'en-US':
                    document.getElementById('btn_' + cal).onclick = function () {
                        CalendarsViewManage_LeaveReserve(cal);
                    };
                    break;
            }
            break;
    }
}

function CalendarsViewManage_LeaveReserve(gdpID) {
    var CalID_LeaveReserve = 'gCal' + gdpID.substr(3, gdpID.length - 3);
    var Cal_LeaveReserve = eval(CalID_LeaveReserve);
    if (!Cal_LeaveReserve.get_popUpShowing())
        Cal_LeaveReserve.show();
    else
        Cal_LeaveReserve.hide();
}

function ChangePageState_LeaveReserve(state) {
    CurrentPageState_LeaveReserve = state;
    SetActionMode_LeaveReserve(state);
    if (state == 'Add' || state == 'Delete') {
        if (TlbLeaveReserve.get_items().getItemById('tlbItemNew_TlbLeaveReserve') != null) {
            TlbLeaveReserve.get_items().getItemById('tlbItemNew_TlbLeaveReserve').set_enabled(false);
            TlbLeaveReserve.get_items().getItemById('tlbItemNew_TlbLeaveReserve').set_imageUrl('add_silver.png');
        }
        if (TlbLeaveReserve.get_items().getItemById('tlbItemDelete_TlbLeaveReserve') != null) {
            TlbLeaveReserve.get_items().getItemById('tlbItemDelete_TlbLeaveReserve').set_enabled(false);
            TlbLeaveReserve.get_items().getItemById('tlbItemDelete_TlbLeaveReserve').set_imageUrl('remove_silver.png');
        }
        TlbLeaveReserve.get_items().getItemById('tlbItemSave_TlbLeaveReserve').set_enabled(true);
        TlbLeaveReserve.get_items().getItemById('tlbItemSave_TlbLeaveReserve').set_imageUrl('save.png');
        TlbLeaveReserve.get_items().getItemById('tlbItemCancel_TlbLeaveReserve').set_enabled(true);
        TlbLeaveReserve.get_items().getItemById('tlbItemCancel_TlbLeaveReserve').set_imageUrl('cancel.png');
        cmbOperationType_LeaveReserve.enable();
        document.getElementById('txtDay_LeaveReserve').disabled = '';
        ChangeTimePickerEnabled_LeaveReserve('TimeSelector_Hour_LeaveReserve', 'enable');
        ChangeCalendarsEnabled_LeaveReserve('enable');
        if (state == 'Delete')
            LeaveReserve_onSave();
    }
    if (state == 'View') {
        if (TlbLeaveReserve.get_items().getItemById('tlbItemNew_TlbLeaveReserve') != null) {
            TlbLeaveReserve.get_items().getItemById('tlbItemNew_TlbLeaveReserve').set_enabled(true);
            TlbLeaveReserve.get_items().getItemById('tlbItemNew_TlbLeaveReserve').set_imageUrl('add.png');
        }
        if (TlbLeaveReserve.get_items().getItemById('tlbItemDelete_TlbLeaveReserve') != null) {
            TlbLeaveReserve.get_items().getItemById('tlbItemDelete_TlbLeaveReserve').set_enabled(true);
            TlbLeaveReserve.get_items().getItemById('tlbItemDelete_TlbLeaveReserve').set_imageUrl('remove.png');
        }
        TlbLeaveReserve.get_items().getItemById('tlbItemSave_TlbLeaveReserve').set_enabled(false);
        TlbLeaveReserve.get_items().getItemById('tlbItemSave_TlbLeaveReserve').set_imageUrl('save_silver.png');
        TlbLeaveReserve.get_items().getItemById('tlbItemCancel_TlbLeaveReserve').set_enabled(false);
        TlbLeaveReserve.get_items().getItemById('tlbItemCancel_TlbLeaveReserve').set_imageUrl('cancel_silver.png');
        cmbOperationType_LeaveReserve.disable();
        document.getElementById('txtDay_LeaveReserve').disabled = 'disabled';
        ChangeTimePickerEnabled_LeaveReserve('TimeSelector_Hour_LeaveReserve', 'disable');
        ChangeCalendarsEnabled_LeaveReserve('disable');
    }
}

function LeaveReserve_onSave() {
    if (CurrentPageState_LeaveReserve != 'Delete')
        UpdateLeaveReserve_LeaveReserve();
    else
        ShowDialogConfirm('Delete');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_LeaveReserve = confirmState;
    if (CurrentPageState_LeaveReserve == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_LeaveReserve').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_LeaveReserve').value;
    DialogConfirm.Show();
    CollapseControls_LeaveReserve();
}

function GetDuration_TimePicker_LeaveReserve(TimePicker) {
    if (document.getElementById(TimePicker + '_txtHour').value.length < 2)
        document.getElementById(TimePicker + '_txtHour').value = '0' + document.getElementById(TimePicker + '_txtHour').value;
    if (document.getElementById(TimePicker + '_txtMinute').value.length < 2)
        document.getElementById(TimePicker + '_txtMinute').value = '0' + document.getElementById(TimePicker + '_txtMinute').value;
    return document.getElementById(TimePicker + '_txtHour').value + ':' + document.getElementById(TimePicker + '_txtMinute').value;
}

function UpdateLeaveReserve_LeaveReserve() {
    ObjLeaveReserve_LeaveReserve = new Object();
    ObjLeaveReserve_LeaveReserve.LeaveRemainID = '0';
    ObjLeaveReserve_LeaveReserve.PersonnelID = '0';
    ObjLeaveReserve_LeaveReserve.OperationType = '-1';
    ObjLeaveReserve_LeaveReserve.OperationTypeTitle = null;
    ObjLeaveReserve_LeaveReserve.Day = null;
    ObjLeaveReserve_LeaveReserve.Hour = null;
    ObjLeaveReserve_LeaveReserve.Date = null;
    ObjLeaveReserve_LeaveReserve.Description = null;
    ObjLeaveReserve_LeaveReserve.ID = '0';
    var SelectedItems_GridLeaveReserve_LeaveReserve = GridLeaveReserve_LeaveReserve.getSelectedItems();
    if (SelectedItems_GridLeaveReserve_LeaveReserve.length > 0)
        ObjLeaveReserve_LeaveReserve.ID = SelectedItems_GridLeaveReserve_LeaveReserve[0].getMember("ID").get_text();

    if (CurrentPageState_LeaveReserve != 'Delete') {
        var ObjDialogLeaveReserve = parent.DialogLeaveReserve.get_value();
        ObjLeaveReserve_LeaveReserve.LeaveRemainID = ObjDialogLeaveReserve.LeaveRemainID;
        ObjLeaveReserve_LeaveReserve.PersonnelID = ObjDialogLeaveReserve.PersonnelID;
        if (cmbOperationType_LeaveReserve.getSelectedItem() != undefined) {
            ObjLeaveReserve_LeaveReserve.OperationType = cmbOperationType_LeaveReserve.getSelectedItem().get_id();
            ObjLeaveReserve_LeaveReserve.OperationTypeTitle = cmbOperationType_LeaveReserve.getSelectedItem().get_text();
        }
        ObjLeaveReserve_LeaveReserve.Day = document.getElementById('txtDay_LeaveReserve').value;
        ObjLeaveReserve_LeaveReserve.Hour = GetDuration_TimePicker_LeaveReserve('TimeSelector_Hour_LeaveReserve');
        ObjLeaveReserve_LeaveReserve.Description = document.getElementById('txtDescription_LeaveReserve').value;
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                ObjLeaveReserve_LeaveReserve.Date = document.getElementById('pdpDate_LeaveReserve').value;
                break;
            case 'en-US':
                ObjLeaveReserve_LeaveReserve.Date = document.getElementById('gdpDate_LeaveReserve_picker').value;
                break;
        }
    }
    UpdateLeaveReserve_LeaveReservePage(CharToKeyCode_LeaveReserve(CurrentPageState_LeaveReserve), CharToKeyCode_LeaveReserve(ObjLeaveReserve_LeaveReserve.LeaveRemainID), CharToKeyCode_LeaveReserve(ObjLeaveReserve_LeaveReserve.PersonnelID), CharToKeyCode_LeaveReserve(ObjLeaveReserve_LeaveReserve.ID), CharToKeyCode_LeaveReserve(ObjLeaveReserve_LeaveReserve.OperationType), CharToKeyCode_LeaveReserve(ObjLeaveReserve_LeaveReserve.Day), CharToKeyCode_LeaveReserve(ObjLeaveReserve_LeaveReserve.Hour), CharToKeyCode_LeaveReserve(ObjLeaveReserve_LeaveReserve.Date), CharToKeyCode_LeaveReserve(ObjLeaveReserve_LeaveReserve.Description));
    DialogWaiting.Show();
}

function UpdateLeaveReserve_LeaveReservePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_LeaveReserve').value;
            Response[1] = document.getElementById('hfConnectionError_LeaveReserve').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_LeaveReserve();
            LeaveReserve_OnAfterUpdate(Response);
            ChangePageState_LeaveReserve('View');
        }
        else {
            if (CurrentPageState_LeaveReserve == 'Delete')
                ChangePageState_LeaveReserve('View');
        }
    }
}

function LeaveReserve_OnAfterUpdate(Response) {
    if (ObjLeaveReserve_LeaveReserve != null) {
        var LeaveReserveOperationType = ObjLeaveReserve_LeaveReserve.OperationType;
        var LeaveReserveOperationTypeTitle = ObjLeaveReserve_LeaveReserve.OperationTypeTitle;
        var LeaveReserveDay = ObjLeaveReserve_LeaveReserve.Day;
        var LeaveReserveHour = ObjLeaveReserve_LeaveReserve.Hour;
        var LeaveReserveDate = ObjLeaveReserve_LeaveReserve.Date;
        var LeaveReserveDescription = ObjLeaveReserve_LeaveReserve.Description;

        var LeaveReserveItem = null;
        GridLeaveReserve_LeaveReserve.beginUpdate();
        switch (CurrentPageState_LeaveReserve) {
            case 'Add':
                LeaveReserveItem = GridLeaveReserve_LeaveReserve.get_table().addEmptyRow(GridLeaveReserve_LeaveReserve.get_recordCount());
                LeaveReserveItem.setValue(0, Response[3], false);
                GridLeaveReserve_LeaveReserve.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridLeaveReserve_LeaveReserve.selectByKey(Response[3], 0, false);
                LeaveReserveItem = GridLeaveReserve_LeaveReserve.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridLeaveReserve_LeaveReserve.selectByKey(ObjLeaveReserve_LeaveReserve.ID, 0, false);
                GridLeaveReserve_LeaveReserve.deleteSelected();
                break;
        }
        if (CurrentPageState_LeaveReserve != 'Delete') {
            LeaveReserveItem.setValue(1, LeaveReserveDate, false);
            LeaveReserveItem.setValue(2, LeaveReserveOperationTypeTitle, false);
            LeaveReserveItem.setValue(3, LeaveReserveDay, false);
            LeaveReserveItem.setValue(4, LeaveReserveHour, false);
            LeaveReserveItem.setValue(5, LeaveReserveDescription, false);
            LeaveReserveItem.setValue(6, LeaveReserveOperationType, false);
        }
        GridLeaveReserve_LeaveReserve.endUpdate();
    }
}

function GetOperationType_LeaveReserve(OperationTypeID) {
    var OperationTypesList = document.getElementById('hfOperationTypes_LeaveReserve').value;
    var OperationTypesListParts = OperationTypesList.split('#');
    for (var i = 0; i < OperationTypesListParts.length; i++) {
        if (OperationTypesListParts[i] != '') {
            var OperationTypesPartObj = OperationTypesListParts[i].split(':');
            if (OperationTypeID == OperationTypesPartObj[1])
                return OperationTypesPartObj[0];
        }
    }
}

function GetRelativePersonnel_LeaveReserve() {
    var ObjDialogLeaveReserve = parent.DialogLeaveReserve.get_value();
    document.getElementById('headerRelativePersonnel_LeaveReserve').innerHTML = ObjDialogLeaveReserve.PersonnelName;
}

function cmbOperationType_LeaveReserve_onExpand(sender, e) {
    if (cmbOperationType_LeaveReserve.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbOperationType_LeaveReserve == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbOperationType_LeaveReserve = true;
        CallBack_cmbOperationType_LeaveReserve.callback();
    }
}

function cmbOperationType_LeaveReserve_onCollapse(sender, e) {
    if (cmbOperationType_LeaveReserve.getSelectedItem() == undefined)
        document.getElementById('cmbOperationType_LeaveReserve_Input').value = document.getElementById('hfcmbAlarm_LeaveReserve').value;
}

function CallBack_cmbOperationType_LeaveReserve_onBeforeCallback(sender, e) {
    cmbOperationType_LeaveReserve.dispose();
}

function CallBack_cmbOperationType_LeaveReserve_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_LeaveReserve').value;
    if (error == "") {
        document.getElementById('cmbOperationType_LeaveReserve_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbOperationType_LeaveReserve_DropImage').mousedown();
        cmbOperationType_LeaveReserve.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbOperationType_LeaveReserve_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function GridLeaveReserve_LeaveReserve_onItemSelect(sender, e) {
    if (CurrentPageState_LeaveReserve != 'Add')
        NavigateLeaveReserve_LeaveReserve(e.get_item());
}

function NavigateLeaveReserve_LeaveReserve(selectedLeaveReserveItem) {
    if (selectedLeaveReserveItem != undefined) {
        document.getElementById('cmbOperationType_LeaveReserve_Input').value = GetOperationType_LeaveReserve(selectedLeaveReserveItem.getMember('Action').get_text());
        document.getElementById('txtDay_LeaveReserve').value = selectedLeaveReserveItem.getMember('Day').get_text();
        SetDuration_TimePicker_LeaveReserve('TimeSelector_Hour_LeaveReserve', selectedLeaveReserveItem.getMember('Hour').get_text());
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpDate_LeaveReserve').value = selectedLeaveReserveItem.getMember('Date').get_text();
                break;
            case 'en-US':
                var gDate = new Date(selectedLeaveReserveItem.getMember('Date').get_text());
                gdpDate_LeaveReserve.setSelectedDate(gDate);
                gdpDate_LeaveReserve.setSelectedDate(gDate);
                break;
        }
    }
}

function SetDuration_TimePicker_LeaveReserve(TimePicker, strTime) {
    if (strTime == "" || strTime == null)
        strTime = '00:00';
    var arTime_LeaveReserve = strTime.split(':');
    for (var i = 0; i < 2; i++) {
        if (arTime_LeaveReserve[i].length < 2)
            arTime_LeaveReserve[i] = '0' + arTime_LeaveReserve[i];
    }
    document.getElementById(TimePicker + '_txtHour').value = arTime_LeaveReserve[0];
    document.getElementById(TimePicker + '_txtMinute').value = arTime_LeaveReserve[1];
}

function CollapseControls_LeaveReserve() {
    cmbOperationType_LeaveReserve.collapse();
}

function Refresh_GridLeaveReserve_LeaveReserve() {
    Fill_GridLeaveReserve_LeaveReserve();
}

function tlbItemFormReconstruction_TlbLeaveReserve_onClick() {
    parent.DialogLeaveReserve.Close();
    parent.document.getElementById('pgvMasterLeaveRemains_iFrame').contentWindow.ShowDialogLeaveReserve();
}

function tlbItemHelp_TlbLeaveReserve_onClick() {
    LoadHelpPage('tlbItemHelp_TlbLeaveReserve');
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_LeaveReserve) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateLeaveReserve_LeaveReserve();
            break;
        case 'Exit':
            ClearList_LeaveReserve();
            CloseDialogLeaveReserve();
            break;
    }
}

function CloseDialogLeaveReserve() {
    parent.document.getElementById('DialogLeaveReserve_IFrame').src = 'WhitePage.aspx';
    parent.DialogLeaveReserve.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}
















