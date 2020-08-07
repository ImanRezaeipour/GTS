
var ObjCultureYearMonth_CultureYearMonth = null;

function cmbYear_CultureYearMonth_onChange(sender, e) {
    document.getElementById('hfCurrentYear_CultureYearMonth').value = cmbYear_CultureYearMonth.getSelectedItem().get_value();
}

function cmbMonth_CultureYearMonth_onChange(sender, e) {
    document.getElementById('hfCurrentMonth_CultureYearMonth').value = cmbMonth_CultureYearMonth.getSelectedItem().get_value();
}

function tlbItemRegister_TlbRegister_CultureYearMonth_onClick() {
    Register_CultureYearMonth();
}

function Register_CultureYearMonth() {        
    ObjCultureYearMonth_CultureYearMonth.Year = document.getElementById('hfCurrentYear_CultureYearMonth').value;
    ObjCultureYearMonth_CultureYearMonth.Month = document.getElementById('hfCurrentMonth_CultureYearMonth').value;
    Register_CultureYearMonthPage(CharToKeyCode_CultureYearMonth(ObjCultureYearMonth_CultureYearMonth.ReportParameterID), CharToKeyCode_CultureYearMonth(ObjCultureYearMonth_CultureYearMonth.ReportParameterActionID), CharToKeyCode_CultureYearMonth(ObjCultureYearMonth_CultureYearMonth.ReportFileID), CharToKeyCode_CultureYearMonth(ObjCultureYearMonth_CultureYearMonth.Year), CharToKeyCode_CultureYearMonth(ObjCultureYearMonth_CultureYearMonth.Month));
    DialogWaiting.Show();
}

function Register_CultureYearMonthPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_CultureYearMonth').value;
            Response[1] = document.getElementById('hfConnectionError_CultureYearMonth').value;
        }
        if (RetMessage[2] == 'success')
            parent.parent.document.getElementById('DialogReportParameters_IFrame').contentWindow.ReportParametres_OnAfterUpdate(Response, ObjCultureYearMonth_CultureYearMonth.ReportParameterID);
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);           
    }
}

function CharToKeyCode_CultureYearMonth(str) {
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

function SetReportParameterObj_CultureYearMonth() {
    var SelectedItems_GridReportParameters_ReportParameters = parent.GridReportParameters_ReportParameters.getSelectedItems();
    if (SelectedItems_GridReportParameters_ReportParameters.length > 0) {
        ObjCultureYearMonth_CultureYearMonth = new Object();
        ObjCultureYearMonth_CultureYearMonth.ReportParameterID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ID').get_text();
        ObjCultureYearMonth_CultureYearMonth.ReportParameterActionID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ActionId').get_text();
        ObjCultureYearMonth_CultureYearMonth.ReportFileID = parent.parent.DialogReportParameters.get_value().ReportFileID;
    }
}

