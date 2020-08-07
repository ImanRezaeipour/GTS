
var ObjCultureDateTime_CultureDateTime = null;

function cmbYear_CultureDateTime_onChange(sender, e) {
    document.getElementById('hfCurrentYear_CultureDateTime').value = cmbYear_CultureDateTime.getSelectedItem().get_value();
}

function cmbMonth_CultureDateTime_onChange(sender, e) {
    document.getElementById('hfCurrentMonth_CultureDateTime').value = cmbMonth_CultureDateTime.getSelectedItem().get_value();
}

function tlbItemRegister_TlbRegister_CultureDateTime_onClick() {
    Register_CultureDateTime();
}

function Register_CultureDateTime() {
    ObjCultureDateTime_CultureDateTime.Year = document.getElementById('hfCurrentYear_CultureDateTime').value;
    ObjCultureDateTime_CultureDateTime.Month = document.getElementById('hfCurrentMonth_CultureDateTime').value;
    var ObjTime = GetTime_CultureDateTime();
    Register_CultureDateTimePage(CharToKeyCode_CultureDateTime(ObjCultureDateTime_CultureDateTime.ReportParameterID), CharToKeyCode_CultureDateTime(ObjCultureDateTime_CultureDateTime.ReportParameterActionID), CharToKeyCode_CultureDateTime(ObjCultureDateTime_CultureDateTime.ReportFileID), CharToKeyCode_CultureDateTime(ObjCultureDateTime_CultureDateTime.Year), CharToKeyCode_CultureDateTime(ObjCultureDateTime_CultureDateTime.Month), CharToKeyCode_CultureDateTime(ObjTime.Hour), CharToKeyCode_CultureDateTime(ObjTime.Minute));
    DialogWaiting.Show();
}

function GetTime_CultureDateTime() {
    var ObjTime = new Object();
    ObjTime.Hour = document.getElementById('TimeSelector_Hour_CultureDateTime_txtHour').value;
    ObjTime.Minute = document.getElementById('TimeSelector_Hour_CultureDateTime_txtMinute').value; 
    return ObjTime;
}

function Register_CultureDateTimePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_CultureDateTime').value;
            Response[1] = document.getElementById('hfConnectionError_CultureDateTime').value;
        }
        if (RetMessage[2] == 'success')
            parent.parent.document.getElementById('DialogReportParameters_IFrame').contentWindow.ReportParametres_OnAfterUpdate(Response, ObjCultureDateTime_CultureDateTime.ReportParameterID);
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function CharToKeyCode_CultureDateTime(str) {
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

function TimeSelector_Hour_CultureDateTime_onChange(partID) {
    var id = 'TimeSelector_Hour_CultureDateTime_' + partID;
    var val = document.getElementById(id).value;
    val = !isNaN(parseFloat(val)) ? parseFloat(val) > 0 ? "" + parseFloat(val) + "" : '00' : '00';
    switch (partID) {
        case 'txtHour':
            break;
        case 'txtMinute':
            val = parseFloat(val) < 60 ? val : '59';
            break;
    }
    document.getElementById(id).value = val.length >= 2 ? val : '0' + val;
}

function initTimePicker_CultureDateTime() {
    ResetTimepicker_CultureDateTime('TimeSelector_Hour_CultureDateTime');
}

function ResetTimepicker_CultureDateTime(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
}

function SetReportParameterObj_CultureDateTime() {
    var SelectedItems_GridReportParameters_ReportParameters = parent.GridReportParameters_ReportParameters.getSelectedItems();
    if (SelectedItems_GridReportParameters_ReportParameters.length > 0) {
        ObjCultureDateTime_CultureDateTime = new Object();
        ObjCultureDateTime_CultureDateTime.ReportParameterID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ID').get_text();
        ObjCultureDateTime_CultureDateTime.ReportParameterActionID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ActionId').get_text();
        ObjCultureDateTime_CultureDateTime.ReportFileID = parent.parent.DialogReportParameters.get_value().ReportFileID;
    }
}

