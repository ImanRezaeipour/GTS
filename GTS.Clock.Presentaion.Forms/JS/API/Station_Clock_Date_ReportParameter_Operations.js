
var ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter = null;
var CurrentPageCombosCallBcakStateObj = new Object();

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
function btn_gdpfromDate_RuleParameters_OnClick(event) {
    if (gCalfromDate_RuleParameters.get_popUpShowing()) {
        gCalfromDate_RuleParameters.hide();
    }
    else {
        gCalfromDate_RuleParameters.setSelectedDate(gdpfromDate_RuleParameters.getSelectedDate());
        gCalfromDate_RuleParameters.show();
    }
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
//  
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
function setCurrentDateCalendar() {
//
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpfromDate_RuleParameters').value = document.getElementById('hfCurrentfromDate_RuleParameters').value;
            document.getElementById('pdptoDate_RuleParameters').value = document.getElementById('hfCurrenttoDate_RuleParameters').value;
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

function tlbItemRegister_TlbRegister_Station_Clock_Date_ReportParameter_onClick() {
    Register_Station_Clock_Date_ReportParameter();
}
// ارسال اطلاعات پارامتر ها به سرور 
function Register_Station_Clock_Date_ReportParameter() {
    if (cmbStation_Station_Clock_Date_ReportParameter.getSelectedItem() != undefined)
        ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.Station = cmbStation_Station_Clock_Date_ReportParameter.getSelectedItem().get_value();
    else
        ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.Station = '0';
    if (cmbClock_Station_Clock_Date_ReportParameter.getSelectedItem() != undefined)
        ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.Clock = cmbClock_Station_Clock_Date_ReportParameter.getSelectedItem().get_value();
    else
        ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.Clock = '0';
  
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.from = document.getElementById('pdpfromDate_RuleParameters').value;
            ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.to = document.getElementById('pdptoDate_RuleParameters').value;
            break;
        case 'en-US':
            ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.from = document.getElementById('gdpfromDate_RuleParameters_picker').value;
            ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.to = document.getElementById('gdptoDate_RuleParameters_picker').value;
            break;
    }

    Register_Station_Clock_Date_ReportParameterPage(CharToKeyCode_Station_Clock_Date_ReportParameter(ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.ReportParameterID), CharToKeyCode_Station_Clock_Date_ReportParameter(ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.ReportParameterActionID), CharToKeyCode_Station_Clock_Date_ReportParameter(ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.ReportFileID), CharToKeyCode_Station_Clock_Date_ReportParameter(ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.from), CharToKeyCode_Station_Clock_Date_ReportParameter(ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.to), CharToKeyCode_Station_Clock_Date_ReportParameter(ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.Station), CharToKeyCode_Station_Clock_Date_ReportParameter(ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.Clock));
    DialogWaiting.Show();
}

function Register_Station_Clock_Date_ReportParameterPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Station_Clock_Date_ReportParameter').value;
            Response[1] = document.getElementById('hfConnectionError_Station_Clock_Date_ReportParameter').value;
        }
        if (RetMessage[2] == 'success')
            parent.parent.document.getElementById('DialogReportParameters_IFrame').contentWindow.ReportParametres_OnAfterUpdate(Response, ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.ReportParameterID);
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function CharToKeyCode_Station_Clock_Date_ReportParameter(str) {
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
function SetReportParameterObj_Station_Clock_Date_ReportParameter() {
    var SelectedItems_GridReportParameters_ReportParameters = parent.GridReportParameters_ReportParameters.getSelectedItems();
    if (SelectedItems_GridReportParameters_ReportParameters.length > 0) {
    ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter = new Object();
        ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.ReportParameterID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ID').get_text();
        ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.ReportParameterActionID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ActionId').get_text();
        ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter.ReportFileID = parent.parent.DialogReportParameters.get_value().ReportFileID;
    }
}
//******************************
function CallBack_cmbStation_Station_Clock_Date_ReportParameter_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Station_Station_Clock_Date_ReportParameter').value;
    if (error == "") {
        document.getElementById('Station_Clock_Date_ReportParameter_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbStation_Station_Clock_Date_ReportParameter = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbStation_Station_Clock_Date_ReportParameter = false;
        Station_Clock_Date_ReportParameter.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbStation_Station_Clock_Date_ReportParameter_DropDown').style.display = 'none';
    }
}
function cmbStation_Station_Clock_Date_ReportParameter_onExpand(sender, e) {
    if (cmbStation_Station_Clock_Date_ReportParameter.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbStation_Station_Clock_Date_ReportParameter == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbStation_Station_Clock_Date_ReportParameter = true;
        Fill_cmbStation_Station_Clock_Date_ReportParameter();
    }
}
function cmbStation_Station_Clock_Date_ReportParameter_onChange(sender, e) {
    CurrentPageCombosCallBcakStateObj.IsChangeOccured_cmbStation_Station_Clock_Date_ReportParameter = true;
}
function Fill_cmbStation_Station_Clock_Date_ReportParameter() {
    CallBack_cmbStation_Station_Clock_Date_ReportParameter.callback();
}

function CallBack_cmbStation_Station_Clock_Date_ReportParameter_onBeforeCallback(sender, e) {
    cmbStation_Station_Clock_Date_ReportParameter.dispose();
}

function CallBack_cmbStation_Station_Clock_Date_ReportParameter_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Station_Station_Clock_Date_ReportParameter').value;
    if (error == "") {
        document.getElementById('cmbStation_Station_Clock_Date_ReportParameter_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbStation_Station_Clock_Date_ReportParameter_DropImage').mousedown();
        cmbStation_Station_Clock_Date_ReportParameter.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbStation_Station_Clock_Date_ReportParameter_DropDown').style.display = 'none';
    }
}

function CallBack_cmbStation_Station_Clock_Date_ReportParameter_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

//*****************************
function CallBack_cmbClock_Station_Clock_Date_ReportParameter_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Clock_Station_Clock_Date_ReportParameter').value;
    if (error == "") {
        document.getElementById('Station_Clock_Date_ReportParameter_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbClock_Station_Clock_Date_ReportParameter = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbClock_Station_Clock_Date_ReportParameter = false;
        Station_Clock_Date_ReportParameter.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbClock_Station_Clock_Date_ReportParameter_DropDown').style.display = 'none';
    }
}
function cmbClock_Station_Clock_Date_ReportParameter_onExpand(sender, e) {
    if ( CurrentPageCombosCallBcakStateObj.IsChangeOccured_cmbStation_Station_Clock_Date_ReportParameter==true) {
        {
            CurrentPageCombosCallBcakStateObj.IsChangeOccured_cmbStation_Station_Clock_Date_ReportParameter = false;
            Fill_cmbClock_Station_Clock_Date_ReportParameter();
        }
        
    }
}

function Fill_cmbClock_Station_Clock_Date_ReportParameter() {
    if (cmbStation_Station_Clock_Date_ReportParameter.getSelectedItem() != undefined)
        CallBack_cmbClock_Station_Clock_Date_ReportParameter.callback(cmbStation_Station_Clock_Date_ReportParameter.getSelectedItem().get_value());
}

function CallBack_cmbClock_Station_Clock_Date_ReportParameter_onBeforeCallback(sender, e) {
    cmbClock_Station_Clock_Date_ReportParameter.dispose();
}

function CallBack_cmbClock_Station_Clock_Date_ReportParameter_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Clock_Station_Clock_Date_ReportParameter').value;
    if (error == "") {
        document.getElementById('cmbClock_Station_Clock_Date_ReportParameter_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbClock_Station_Clock_Date_ReportParameter_DropImage').mousedown();
        cmbClock_Station_Clock_Date_ReportParameter.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbClock_Station_Clock_Date_ReportParameter_DropDown').style.display = 'none';
    }
}
function CallBack_cmbClock_Station_Clock_Date_ReportParameter_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

//**********************************
function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}