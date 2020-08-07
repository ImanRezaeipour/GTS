
var ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter = null;
function gdpfromDate_RuleParameters_OnDateChange(sender, e) {
    var Date = gdpfromDate_RuleParameters.getSelectedDate();
    gCalfromDate_RuleParameters.setSelectedDate(Date);
}
function gCalfromDate_RuleParameters_OnChange(sender, e) {
    var Date = gCalfromDate_RuleParameters.getSelectedDate();
    gdpfromDate_RuleParameters.setSelectedDate(Date);
   
}
function gCalfromDate_RuleParameters_onLoad(sender, e) {
    window.gCalfromDate_RuleParameters.PopUpObject.z = 25000000;
}
function gdptoDate_RuleParameters_OnDateChange(sender, e) {
    var Date = gdptoDate_RuleParameters.getSelectedDate();
    gCaltoDate_RuleParameters.setSelectedDate(Date);
    
}
function gCaltoDate_RuleParameters_OnChange(sender, e) {
    var Date = gCaltoDate_RuleParameters.getSelectedDate();
    gdptoDate_RuleParameters.setSelectedDate(Date);
   
}
function gCaltoDate_RuleParameters_onLoad(sender, e) {
    window.gCaltoDate_RuleParameters.PopUpObject.z = 25000000;
}
function btn_gdpfromDate_RuleParameters_OnMouseUp(event) {
    if (gCalfromDate_RuleParameters.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}
function btn_gdptoDate_RuleParameters_OnMouseUp(event) {
    if (gCaltoDate_RuleParameters.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}
function btn_gdpfromDate_RuleParameters_OnClick(event) {
    if (gCalfromDate_RuleParameters.get_popUpShowing()) {
        gCalfromDate_RuleParameters.hide();
    }
    else {
        gCalfromDate_RuleParameters.setSelectedDate(gdpfromDate_RuleParameters.getSelectedDate());
        gCalfromDate_RuleParameters.show();
    }
}
function btn_gdptoDate_RuleParameters_OnClick(event) {
    if (gCaltoDate_RuleParameters.get_popUpShowing()) {
        gCaltoDate_RuleParameters.hide();
    }
    else {
        gCaltoDate_RuleParameters.setSelectedDate(gdptoDate_RuleParameters.getSelectedDate());
        gCaltoDate_RuleParameters.show();
    }
}

//نمایش تقویم بر اساس زبان انتخاب شده در نرم افزار
function ViewCurrentLangCalendars_FromDate_ToDate_ReportParameter() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpfromDate_RuleParameters").parentNode.removeChild(document.getElementById("pdpfromDate_RuleParameters"));
            document.getElementById("pdpfromDate_RuleParametersimgbt").parentNode.removeChild(document.getElementById("pdpfromDate_RuleParametersimgbt"));
            document.getElementById("pdptoDate_RuleParameters").parentNode.removeChild(document.getElementById("pdptoDate_RuleParameters"));
            document.getElementById("pdptoDate_RuleParametersimgbt").parentNode.removeChild(document.getElementById("pdptoDate_RuleParametersimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_fromDateCalendars_RuleParameters").removeChild(document.getElementById("Container_gCalfromDate_RuleParameters"));
            document.getElementById("Container_toDateCalendars_RuleParameters").removeChild(document.getElementById("Container_gCaltoDate_RuleParameters"));
            break;
    }
    setCurrentDateCalendar();
}

// تنظیم تقویم های شمسی و میلادی به تاریخ روز بر اساس زبان نرم افزار
function SetCurrentDate_FromDate_ToDate_ReportParameter() {
  
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpfromDate_RuleParameters').value = document.getElementById('hfCurrentfromDate_RuleParameters').value;
            document.getElementById('pdptoDate_RuleParameters').value = document.getElementById('hfCurrentfromDate_RuleParameters').value;
            break;
        case 'en-US':
            currentDate_PersonnelMainInformation = new Date(document.getElementById('hfCurrentfromDate_RuleParameters').value);
            gdpfromDate_RuleParameters.setSelectedDate(currentDate_PersonnelMainInformation);
            gCalfromDate_RuleParameters.setSelectedDate(currentDate_PersonnelMainInformation);
            gdptoDate_RuleParameters.setSelectedDate(currentDate_PersonnelMainInformation);
            gCaltoDate_RuleParameters.setSelectedDate(currentDate_PersonnelMainInformation);                      
            break;
    }
}

function ResetCalendars_FromDate_ToDate_ReportParameter() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpfromDate_RuleParameters').value = '';
            document.getElementById('pdptoDate_RuleParameters').value = '';            
            break;
        case 'en-US':
            document.getElementById('gdpfromDate_RuleParameters_picker').value = '';
            document.getElementById('gdptoDate_RuleParameters_picker').value = '';
            break;
    }
}

function tlbItemRegister_TlbRegister_FromDate_ToDate_ReportParameter_onClick() {
    Register_FromDate_ToDate_ReportParameter();
}
// ارسال اطلاعات پارامتر ها به سرور 
function Register_FromDate_ToDate_ReportParameter() {

    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter.from = document.getElementById('pdpfromDate_RuleParameters').value;
            ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter.to = document.getElementById('pdptoDate_RuleParameters').value;
            break;
        case 'en-US':
            ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter.from = document.getElementById('gdpfromDate_RuleParameters_picker').value;
            ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter.to = document.getElementById('gdptoDate_RuleParameters_picker').value;
            break;
    }

    Register_FromDate_ToDate_ReportParameterPage(CharToKeyCode_FromDate_ToDate_ReportParameter(ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter.ReportParameterID), CharToKeyCode_FromDate_ToDate_ReportParameter(ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter.ReportParameterActionID), CharToKeyCode_FromDate_ToDate_ReportParameter(ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter.ReportFileID), CharToKeyCode_FromDate_ToDate_ReportParameter(ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter.from), CharToKeyCode_FromDate_ToDate_ReportParameter(ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter.to));
    DialogWaiting.Show();
}

function Register_FromDate_ToDate_ReportParameterPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_FromDate_ToDate_ReportParameter').value;
            Response[1] = document.getElementById('hfConnectionError_FromDate_ToDate_ReportParameter').value;
        }
        if (RetMessage[2] == 'success')
            parent.parent.document.getElementById('DialogReportParameters_IFrame').contentWindow.ReportParametres_OnAfterUpdate(Response, ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter.ReportParameterID);
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function CharToKeyCode_FromDate_ToDate_ReportParameter(str) {
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
//در یافت اطلاعات پارامتر انتخاب شده و نگهداری در آبجکت  برای ارسال مجدد به سرور 
function SetReportParameterObj_FromDate_ToDate_ReportParameter() {
    var SelectedItems_GridReportParameters_ReportParameters = parent.GridReportParameters_ReportParameters.getSelectedItems();
    if (SelectedItems_GridReportParameters_ReportParameters.length > 0) {
        ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter = new Object();
      ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter.ReportParameterID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ID').get_text();
       ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter.ReportParameterActionID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ActionId').get_text();
       ObjFromDate_ToDate_ReportParameter_FromDate_ToDate_ReportParameter.ReportFileID = parent.parent.DialogReportParameters.get_value().ReportFileID;
   }
}

