
var ObjFirstDayYear_ToDate_ReportParameter_FirstDayYear_ToDate_ReportParameter = null;


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
function ViewCurrentLangCalendars_RuleParameters() {
    switch (parent.parent.SysLangID) {
        case 'en-US':

            document.getElementById("pdptoDate_RuleParameters").parentNode.removeChild(document.getElementById("pdptoDate_RuleParameters"));
            document.getElementById("pdptoDate_RuleParametersimgbt").parentNode.removeChild(document.getElementById("pdptoDate_RuleParametersimgbt"));
            break;
        case 'fa-IR':
 
            document.getElementById("Container_toDateCalendars_RuleParameters").removeChild(document.getElementById("Container_gCaltoDate_RuleParameters"));
            break;
    }
    setCurrentDateCalendar();
}

// تنظیم تقویم های شمسی و میلادی به تاریخ روز بر اساس زبان نرم افزار
function setCurrentDateCalendar() {

    switch (parent.parent.SysLangID) {
        case 'fa-IR':

            document.getElementById('pdptoDate_RuleParameters').value = document.getElementById('hfCurrenttoDate_RuleParameters').value;
            break;
        case 'en-US':
            currentDate_PersonnelMainInformation = new Date(document.getElementById('hfCurrenttoDate_RuleParameters').value);
            
            gdptoDate_RuleParameters.setSelectedDate(currentDate_PersonnelMainInformation);
            gCaltoDate_RuleParameters.setSelectedDate(currentDate_PersonnelMainInformation);


            break;
    }
}

function tlbItemRegister_TlbRegister_FirstDayYear_ToDate_ReportParameter_onClick() {
    Register_FirstDayYear_ToDate_ReportParameter();
}
// ارسال اطلاعات پارامتر ها به سرور 
function Register_FirstDayYear_ToDate_ReportParameter() {

    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            
            ObjFirstDayYear_ToDate_ReportParameter_FirstDayYear_ToDate_ReportParameter.to = document.getElementById('pdptoDate_RuleParameters').value;
            break;
        case 'en-US':
            
            ObjFirstDayYear_ToDate_ReportParameter_FirstDayYear_ToDate_ReportParameter.to = document.getElementById('gdptoDate_RuleParameters_picker').value;
            break;
    }

    Register_FirstDayYear_ToDate_ReportParameterPage(CharToKeyCode_FirstDayYear_ToDate_ReportParameter(ObjFirstDayYear_ToDate_ReportParameter_FirstDayYear_ToDate_ReportParameter.ReportParameterID), CharToKeyCode_FirstDayYear_ToDate_ReportParameter(ObjFirstDayYear_ToDate_ReportParameter_FirstDayYear_ToDate_ReportParameter.ReportParameterActionID), CharToKeyCode_FirstDayYear_ToDate_ReportParameter(ObjFirstDayYear_ToDate_ReportParameter_FirstDayYear_ToDate_ReportParameter.ReportFileID), CharToKeyCode_FirstDayYear_ToDate_ReportParameter(ObjFirstDayYear_ToDate_ReportParameter_FirstDayYear_ToDate_ReportParameter.to));
    DialogWaiting.Show();
}

function Register_FirstDayYear_ToDate_ReportParameterPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_FirstDayYear_ToDate_ReportParameter').value;
            Response[1] = document.getElementById('hfConnectionError_FirstDayYear_ToDate_ReportParameter').value;
        }
        if (RetMessage[2] == 'success')
            parent.parent.document.getElementById('DialogReportParameters_IFrame').contentWindow.ReportParametres_OnAfterUpdate(Response, ObjFirstDayYear_ToDate_ReportParameter_FirstDayYear_ToDate_ReportParameter.ReportParameterID);
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function CharToKeyCode_FirstDayYear_ToDate_ReportParameter(str) {
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
function SetReportParameterObj_FirstDayYear_ToDate_ReportParameter() {
        var SelectedItems_GridReportParameters_ReportParameters = parent.GridReportParameters_ReportParameters.getSelectedItems();
        if (SelectedItems_GridReportParameters_ReportParameters.length > 0) {
   ObjFirstDayYear_ToDate_ReportParameter_FirstDayYear_ToDate_ReportParameter = new Object();
          ObjFirstDayYear_ToDate_ReportParameter_FirstDayYear_ToDate_ReportParameter.ReportParameterID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ID').get_text();
           ObjFirstDayYear_ToDate_ReportParameter_FirstDayYear_ToDate_ReportParameter.ReportParameterActionID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ActionId').get_text();
           ObjFirstDayYear_ToDate_ReportParameter_FirstDayYear_ToDate_ReportParameter.ReportFileID = parent.parent.DialogReportParameters.get_value().ReportFileID;
     }
   }

   function ResetCalendar_FirstDayYear_ToDate_ReportParameter() {
       switch (parent.parent.SysLangID) {
           case 'fa-IR':
               document.getElementById('pdptoDate_RuleParameters').value = '';
               break;
           case 'en-US':
               document.getElementById('gdptoDate_RuleParameters').value = '';
               break;
       }
   }

