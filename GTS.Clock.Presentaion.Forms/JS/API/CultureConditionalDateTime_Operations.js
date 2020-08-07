

var ObjCultureConditionalDateTime_CultureConditionalDateTime = null;

function gdpfromDate_CultureConditionalDateTime_OnDateChange(sender, e) {
    var Date = gdpfromDate_CultureConditionalDateTime.getSelectedDate();
    gCalfromDate_CultureConditionalDateTime.setSelectedDate(Date);
}
function gCalfromDate_CultureConditionalDateTime_OnChange(sender, e) {
    var Date = gCalfromDate_CultureConditionalDateTime.getSelectedDate();
    gdpfromDate_CultureConditionalDateTime.setSelectedDate(Date);

}
function gCalfromDate_CultureConditionalDateTime_onLoad(sender, e) {
    window.gCalfromDate_CultureConditionalDateTime.PopUpObject.z = 25000000;
}
function gdptoDate_CultureConditionalDateTime_OnDateChange(sender, e) {
    var Date = gdptoDate_CultureConditionalDateTime.getSelectedDate();
    gCaltoDate_CultureConditionalDateTime.setSelectedDate(Date);

}
function gCaltoDate_CultureConditionalDateTime_OnChange(sender, e) {
    var Date = gCaltoDate_CultureConditionalDateTime.getSelectedDate();
    gdptoDate_CultureConditionalDateTime.setSelectedDate(Date);

}
function gCaltoDate_CultureConditionalDateTime_onLoad(sender, e) {
    window.gCaltoDate_CultureConditionalDateTime.PopUpObject.z = 25000000;
}
function btn_gdpfromDate_CultureConditionalDateTime_OnMouseUp(event) {
    if (gCalfromDate_CultureConditionalDateTime.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}
function btn_gdptoDate_CultureConditionalDateTime_OnMouseUp(event) {
    if (gCaltoDate_CultureConditionalDateTime.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}
function btn_gdpfromDate_CultureConditionalDateTime_OnClick(event) {
    if (gCalfromDate_CultureConditionalDateTime.get_popUpShowing()) {
        gCalfromDate_CultureConditionalDateTime.hide();
    }
    else {
        gCalfromDate_CultureConditionalDateTime.setSelectedDate(gdpfromDate_CultureConditionalDateTime.getSelectedDate());
        gCalfromDate_CultureConditionalDateTime.show();
    }
}
function btn_gdptoDate_CultureConditionalDateTime_OnClick(event) {
    if (gCaltoDate_CultureConditionalDateTime.get_popUpShowing()) {
        gCaltoDate_CultureConditionalDateTime.hide();
    }
    else {
        gCaltoDate_CultureConditionalDateTime.setSelectedDate(gdptoDate_CultureConditionalDateTime.getSelectedDate());
        gCaltoDate_CultureConditionalDateTime.show();
    }
}

function SetCurrentDate_CultureConditionalDateTime() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpfromDate_CultureConditionalDateTime').value = document.getElementById('hfCurrentDate_CultureConditionalDateTime').value;
            document.getElementById('pdptoDate_CultureConditionalDateTime').value = document.getElementById('hfCurrentDate_CultureConditionalDateTime').value;
            break;
        case 'en-US':
            currentDate_PersonnelMainInformation = new Date(document.getElementById('hfCurrentDate_CultureConditionalDateTime').value);
            gdpfromDate_CultureConditionalDateTime.setSelectedDate(currentDate_PersonnelMainInformation);
            gCalfromDate_CultureConditionalDateTime.setSelectedDate(currentDate_PersonnelMainInformation);
            gdptoDate_CultureConditionalDateTime.setSelectedDate(currentDate_PersonnelMainInformation);
            gCaltoDate_CultureConditionalDateTime.setSelectedDate(currentDate_PersonnelMainInformation);
            break;
    }
}

function CharToKeyCode_CultureConditionalDateTime(str) {
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

function TimeSelector_Hour_CultureConditionalDateTime_onChange(partID) {
    var id = 'TimeSelector_Hour_CultureConditionalDateTime_' + partID;
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

function initTimePicker_CultureConditionalDateTime() {
    ResetTimepicker_CultureConditionalDateTime('TimeSelector_Hour_CultureConditionalDateTime');
}

function ResetTimepicker_CultureConditionalDateTime(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
}

function SetReportParameterObj_CultureConditionalDateTime() {
    var SelectedItems_GridReportParameters_ReportParameters = parent.GridReportParameters_ReportParameters.getSelectedItems();
    if (SelectedItems_GridReportParameters_ReportParameters.length > 0) {
        ObjCultureConditionalDateTime_CultureConditionalDateTime = new Object();
        ObjCultureConditionalDateTime_CultureConditionalDateTime.ReportParameterID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ID').get_text();
        ObjCultureConditionalDateTime_CultureConditionalDateTime.ReportParameterActionID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ActionId').get_text();
        ObjCultureConditionalDateTime_CultureConditionalDateTime.ReportFileID = parent.parent.DialogReportParameters.get_value().ReportFileID;
    }
}

function tlbItemRegister_TlbRegister_CultureConditionalDateTime_onClick() {
    Register_CultureConditionalDateTime();
}

function Register_CultureConditionalDateTime() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ObjCultureConditionalDateTime_CultureConditionalDateTime.from = document.getElementById('pdpfromDate_CultureConditionalDateTime').value;
            ObjCultureConditionalDateTime_CultureConditionalDateTime.to = document.getElementById('pdptoDate_CultureConditionalDateTime').value;
            break;
        case 'en-US':
            ObjCultureConditionalDateTime_CultureConditionalDateTime.from = document.getElementById('gdpfromDate_CultureConditionalDateTime_picker').value;
            ObjCultureConditionalDateTime_CultureConditionalDateTime.to = document.getElementById('gdptoDate_CultureConditionalDateTime_picker').value;
            break;
    }
    var ObjTime = GetTime_CultureConditionalDateTime();
    Register_CultureConditionalDateTimePage(CharToKeyCode_CultureConditionalDateTime(ObjCultureConditionalDateTime_CultureConditionalDateTime.ReportParameterID), CharToKeyCode_CultureConditionalDateTime(ObjCultureConditionalDateTime_CultureConditionalDateTime.ReportParameterActionID), CharToKeyCode_CultureConditionalDateTime(ObjCultureConditionalDateTime_CultureConditionalDateTime.ReportFileID), CharToKeyCode_CultureConditionalDateTime(ObjCultureConditionalDateTime_CultureConditionalDateTime.from), CharToKeyCode_CultureConditionalDateTime(ObjCultureConditionalDateTime_CultureConditionalDateTime.to), CharToKeyCode_CultureConditionalDateTime(ObjTime.Hour), CharToKeyCode_CultureConditionalDateTime(ObjTime.Minute));
    DialogWaiting.Show();
}

function Register_CultureConditionalDateTimePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_CultureConditionalDateTime').value;
            Response[1] = document.getElementById('hfConnectionError_CultureConditionalDateTime').value;
        }
        if (RetMessage[2] == 'success')
            parent.parent.document.getElementById('DialogReportParameters_IFrame').contentWindow.ReportParametres_OnAfterUpdate(Response, ObjCultureConditionalDateTime_CultureConditionalDateTime.ReportParameterID);
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function GetTime_CultureConditionalDateTime() {
    var ObjTime = new Object();
    ObjTime.Hour = document.getElementById('TimeSelector_Hour_CultureConditionalDateTime_txtHour').value;
    ObjTime.Minute = document.getElementById('TimeSelector_Hour_CultureConditionalDateTime_txtMinute').value;
    return ObjTime;
}



