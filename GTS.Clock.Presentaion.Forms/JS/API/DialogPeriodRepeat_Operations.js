
var CurrentPageCombosCallBcakStateObj = new Object();
var HolidaysList_PeriodRepeat = '';

function GetBoxesHeaders_PeriodRepeat() {
    parent.document.getElementById('Title_DialogPeriodRepeat').innerHTML = document.getElementById('hfTitle_DialogPeriodRepeat').value;
}

function tlbItemEndorsement_TlbPeriodRepeat_onClick() {
    UpdatePeriodRepeat_PeriodRepeat();
}

function UpdatePeriodRepeat_PeriodRepeat() {
    var ObjDialogPeriodRepeat = parent.DialogPeriodRepeat.get_value();
    ObjDialogPeriodRepeat.FromMonth = ObjDialogPeriodRepeat.FromDay = ObjDialogPeriodRepeat.ToMonth = ObjDialogPeriodRepeat.ToDay = '1';
    if (cmbFromMonth_PeriodRepeat.getSelectedItem() != undefined)
        ObjDialogPeriodRepeat.FromMonth = cmbFromMonth_PeriodRepeat.getSelectedItem().get_value();
    if (cmbFromDay_PeriodRepeat.getSelectedItem() != undefined)
        ObjDialogPeriodRepeat.FromDay = cmbFromDay_PeriodRepeat.getSelectedItem().get_value();
    if (cmbToMonth_PeriodRepeat.getSelectedItem() != undefined)
        ObjDialogPeriodRepeat.ToMonth = cmbToMonth_PeriodRepeat.getSelectedItem().get_value();
    if (cmbToDay_PeriodRepeat.getSelectedItem() != undefined)
        ObjDialogPeriodRepeat.ToDay = cmbToDay_PeriodRepeat.getSelectedItem().get_value();
    ObjDialogPeriodRepeat.StrHolidaysList = HolidaysList_PeriodRepeat;
    parent.parent.document.getElementById('DialogCalendar_IFrame').contentWindow.UpdatePeriodRepeat_Calendar(ObjDialogPeriodRepeat);
}

function tlbItemFormReconstruction_TlbPeriodRepeat_onClick() {
    CloseDialogPeriodRepeat();
    parent.parent.document.getElementById('DialogCalendar_IFrame').contentWindow.ShowDialogPeriodRepeat_DialogCalendar();
}

function tlbItemExit_TlbPeriodRepeat_onClick() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PeriodRepeat').value;
    DialogConfirm.Show();
    CollapseControls_PeriodRepeat(null);
}

function cmbFromMonth_PeriodRepeat_onChange(sender, e) {
    if (cmbFromMonth_PeriodRepeat.getSelectedItem() != undefined) {
        var month = cmbFromMonth_PeriodRepeat.getSelectedItem().get_value();
        Fill_cmbFromDay_PeriodRepeat(month);
    }
}

function Fill_cmbFromDay_PeriodRepeat(month) {
    var ObjDialogCalendar = parent.parent.DialogCalendar.get_value();
    var Year = ObjDialogCalendar.Year;
    CallBack_cmbFromDay_PeriodRepeat.callback(CharToKeyCode_PeriodRepeat(Year), CharToKeyCode_PeriodRepeat(month));
}

function cmbToMonth_PeriodRepeat_onChange(sender, e) {
    if (cmbToMonth_PeriodRepeat.getSelectedItem() != undefined) {
        var month = cmbToMonth_PeriodRepeat.getSelectedItem().get_value();
        Fill_cmbToDay_PeriodRepeat(month);
    }
}

function Fill_cmbToDay_PeriodRepeat(month) {
    var ObjDialogCalendar = parent.parent.DialogCalendar.get_value();
    var Year = ObjDialogCalendar.Year;
    CallBack_cmbToDay_PeriodRepeat.callback(CharToKeyCode_PeriodRepeat(Year), CharToKeyCode_PeriodRepeat(month));
}

function trvHolidays_PeriodRepeat_onNodeCheckChange(sender, e) {
    var holidayID = e.get_node().get_value();
    if (HolidaysList_PeriodRepeat.indexOf('#' + holidayID + '#') < 0) 
        HolidaysList_PeriodRepeat += holidayID + '#';
    else
        HolidaysList_PeriodRepeat = HolidaysList_PeriodRepeat.replace('#' + holidayID + '#', '#');

    if (HolidaysList_PeriodRepeat.charAt(0) != '#')
        HolidaysList_PeriodRepeat = '#' + HolidaysList_PeriodRepeat;
    if (HolidaysList_PeriodRepeat.length == 1 && HolidaysList_PeriodRepeat.charAt(0) == '#')
        HolidaysList_PeriodRepeat = '';    
}

function cmbHolidays_PeriodRepeat_onCallbackError(sender, e) {
    ShowConnectionError_PeriodRepeat();
}

function CallBack_cmbFromDay_PeriodRepeat_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_FromDay_PeriodRepeat').value;
    if (error == "") {
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbFromDay_PeriodRepeat = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbFromDay_PeriodRepeat = false;
        document.getElementById('cmbFromDay_PeriodRepeat_DropDown').style.display = 'none';
        cmbFromDay_PeriodRepeat.expand();
        ChangeControlDirection_PeriodRepeat('cmbFromDay_PeriodRepeat_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbFromDay_PeriodRepeat_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CallBack_cmbFromDay_PeriodRepeat_onCallbackError(sender, e) {
    ShowConnectionError_PeriodRepeat();
}

function CallBack_cmbToDay_PeriodRepeat_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ToDay_PeriodRepeat').value;
    if (error == "") {
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbToDay_PeriodRepeat = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbToDay_PeriodRepeat = false;
        document.getElementById('cmbToDay_PeriodRepeat_DropDown').style.display = 'none';
        cmbToDay_PeriodRepeat.expand();
        ChangeControlDirection_PeriodRepeat('cmbToDay_PeriodRepeat_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbToDay_PeriodRepeat_DropDown').style.display = 'none';
    }
}

function CallBack_cmbToDay_PeriodRepeat_onCallbackError(sender, e) {
    ShowConnectionError_PeriodRepeat();
}

function cmbHolidays_PeriodRepeat_onExpand(sender, e) {
    CollapseControls_PeriodRepeat(cmbHolidays_PeriodRepeat);
    if (cmbHolidays_PeriodRepeat.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbHolidays_PeriodRepeat == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbHolidays_PeriodRepeat = true;
        CallBack_cmbHolidays_PeriodRepeat.callback();
    }
}

function cmbHolidays_PeriodRepeat_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Holidays').value;
    if (error == "") {
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbHolidays_PeriodRepeat_DropImage').mousedown();
        cmbHolidays_PeriodRepeat.expand();
        ChangeControlDirection_PeriodRepeat('cmbHolidays_PeriodRepeat_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbHolidays_PeriodRepeat_DropDown').style.display = 'none';
    }
}

function cmbHolidays_PeriodRepeat_onBeforeCallback(sender, e) {
    cmbHolidays_PeriodRepeat.dispose();
}

function Refresh_cmbHolidays_PeriodRepeat() {
    CallBack_cmbHolidays_PeriodRepeat.callback();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDialogPeriodRepeat();
}

function CloseDialogPeriodRepeat() {
    parent.document.getElementById('DialogPeriodRepeat_IFrame').src = 'WhitePage.aspx';
    parent.DialogPeriodRepeat.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function CharToKeyCode_PeriodRepeat(str) {
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

function ShowConnectionError_PeriodRepeat() {
    var error = document.getElementById('hfErrorType_PeriodRepeat').value;
    var errorBody = document.getElementById('hfConnectionError_PeriodRepeat').value;
    showDialog(error, errorBody, 'error');
}

function cmbFromMonth_PeriodRepeat_onExpand(sender, e) {
    CollapseControls_PeriodRepeat(cmbFromMonth_PeriodRepeat);
}

function cmbFromDay_PeriodRepeat_onExpand(sender, e) {
    CollapseControls_PeriodRepeat(cmbFromDay_PeriodRepeat);
}

function cmbToMonth_PeriodRepeat_onExpand(sender, e) {
    CollapseControls_PeriodRepeat(cmbToMonth_PeriodRepeat);
}

function cmbToDay_PeriodRepeat_onExpand(sender, e) {
    CollapseControls_PeriodRepeat(cmbToDay_PeriodRepeat);
}

function CollapseControls_PeriodRepeat(exception) {
    if (exception == null || exception != cmbFromMonth_PeriodRepeat)
        cmbFromMonth_PeriodRepeat.collapse();
    if (exception == null || exception != cmbFromDay_PeriodRepeat)
        cmbFromDay_PeriodRepeat.collapse();
    if (exception == null || exception != cmbToMonth_PeriodRepeat)
        cmbToMonth_PeriodRepeat.collapse();
    if (exception == null || exception != cmbToDay_PeriodRepeat)
        cmbToDay_PeriodRepeat.collapse();
    if (exception == null || exception != cmbHolidays_PeriodRepeat)
        cmbHolidays_PeriodRepeat.collapse();
}

function ChangeControlDirection_PeriodRepeat(ctrl) {
    var direction = null;
    switch (parent.parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    if (ctrl == 'All') {
        document.getElementById('Mastertbl_PeriodRepeat').dir = document.getElementById('cmbFromMonth_PeriodRepeat_DropDownContent').dir = document.getElementById('cmbFromDay_PeriodRepeat_DropDownContent').dir = document.getElementById('cmbFromMonth_PeriodRepeat_DropDownContent').dir = document.getElementById('cmbToDay_PeriodRepeat_DropDownContent').dir = document.getElementById('cmbHolidays_PeriodRepeat_DropDownContent').dir = direction;
    }
    else
        document.getElementById(ctrl).style.direction = direction;
}







