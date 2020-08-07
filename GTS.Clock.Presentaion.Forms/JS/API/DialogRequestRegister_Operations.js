
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjRequestRegister = null;
var LoadState_cmbPersonnel_RequestRegister = 'Normal';
var SearchTerm_cmbPersonnel_RequestRegister = '';
var AdvancedSearchTerm_cmbPersonnel_RequestRegister = '';
var CurrentPageIndex_cmbPersonnel_RequestRegister = 0;
var StrUnCollectivePersonnelList_CollectiveTraffic = '';
var StrCollectiveImperativeList_RequestRegister = '';
var CurrentPageIndex_GridPersonnel_CollectiveTraffic = 0;
var RequestPersonnelCountState_RequestRegister = 'Single';
var CollectiveConditions = null;
var zeroTime = '00';
var NullTime_RequestRegister = '';
var PersonnelCount_CollectiveTraffic = 0;
var CurrentPageIndex_GridPersonnel_tbImperative_RequestRegister = 0;
var ObjRequestAttachment_RequestRegister = null;
var ImperativeRequestLoadState_RequestRegister = 'Normal';
var ObjRequestTarget_RequestTarget = null;

function GetBoxesHeaders_RequestRegister() {
    var ObjDialogRequestRegister = parent.DialogRequestRegister.get_value();
    var RequestCaller = ObjDialogRequestRegister.Caller;
    if (RequestCaller == 'Operator') {
        document.getElementById('clmnName_cmbPersonnel_RequestRegister').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_RequestRegister').value;
        document.getElementById('clmnBarCode_cmbPersonnel_RequestRegister').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_RequestRegister').value;
        document.getElementById('clmnCardNum_cmbPersonnel_RequestRegister').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_RequestRegister').value;
    }
    parent.document.getElementById('Title_DialogRequestRegister').innerHTML = document.getElementById('hfTitle_DialogRequestRegister').value;
    document.getElementById('cmbRequestType_tbHourly_RequestRegister_Input').value = document.getElementById('cmbRequestType_tbDaily_RequestRegister_Input').value = document.getElementById('cmbRequestType_tbOverTime_RequestRegister_Input').value = document.getElementById('cmbRequestType_tbImperative_RequestRegister_Input').value = document.getElementById('hfcmbAlarm_RequestRegister').value;
    document.getElementById('header_Personnel_CollectiveTraffic').innerHTML = document.getElementById('hfheader_Personnel_CollectiveTraffic').value;
    document.getElementById('footer_GridPersonnel_CollectiveTraffic').innerHTML = document.getElementById('hffooter_GridPersonnel_CollectiveTraffic').value;
    document.getElementById('header_Personnel_tbImperative_RequestRegister').innerHTML = document.getElementById('hfheader_Personnel_tbImperative_RequestRegister').value;
    document.getElementById('footer_GridPersonnel_tbImperative_RequestRegister').innerHTML = document.getElementById('hffooter_GridPersonnel_tbImperative_RequestRegister').value;
    if (document.getElementById('headerPersonnelCount_RequestRegister') != null)
        document.getElementById('headerPersonnelCount_RequestRegister').innerHTML = document.getElementById('hfSelectedPersonnelCount_RequestRegister').value + '0';
}

function initTimePickers_RequestRegister(pageState) {
    SetButtonImages_TimeSelectors_DialogRequestRegister();
    ChangeTimePickersEnabled_RequestRegister(pageState, 'enable');
    ResetTimepickers_RequestRegister(pageState);
}

function ChangeTimePickersEnabled_RequestRegister(pageState, state) {
    ChangeTimePickerEnabled_RequestRegister(pageState, 'TimeSelector_FromHour_tbHourly_RequestRegister', state);
    ChangeTimePickerEnabled_RequestRegister(pageState, 'TimeSelector_ToHour_tbHourly_RequestRegister', state);
    ChangeTimePickerEnabled_RequestRegister(pageState, 'TimeSelector_FromHour_tbOverTime_RequestRegister', state);
    ChangeTimePickerEnabled_RequestRegister(pageState, 'TimeSelector_ToHour_tbOverTime_RequestRegister', state);
    ChangeTimePickerEnabled_RequestRegister(pageState, 'TimeSelector_Duration_tbOverTime_RequestRegister', state);
}

function ChangeTimePickerEnabled_RequestRegister(pageState, TimeSelector, state) {
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
                CheckTimePickerState_RequestRegister(TimeSelector + '_txtHour');
                CheckTimePickerState_RequestRegister(TimeSelector + '_txtMinute');
                addTime(document.getElementById("" + TimeSelector + "_imgUp"), 24, 1, 1);
            };
            document.getElementById("" + TimeSelector + "_imgDown").onclick = function () {
                CheckTimePickerState_RequestRegister(TimeSelector + '_txtHour');
                CheckTimePickerState_RequestRegister(TimeSelector + '_txtMinute');
                subtractTime(document.getElementById("" + TimeSelector + "_imgDown"), 24, 1, 1);
            };
            document.getElementById("" + TimeSelector + "_txtHour").onchange = function () {
                CheckTimeSelectorPartValue_RequestRegister(pageState, TimeSelector, '_txtHour');
            };
            document.getElementById("" + TimeSelector + "_txtMinute").onchange = function () {
                CheckTimeSelectorPartValue_RequestRegister(pageState, TimeSelector, '_txtMinute');
            };
            break;
    }
    document.getElementById("" + TimeSelector + "_txtHour").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtHour").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtSecond").disabled = disabled;
}

function CheckTimeSelectorPartValue_RequestRegister(pageState, TimeSelectorPartID, identifier) {
    if (document.getElementById(TimeSelectorPartID + identifier).value == "") {
        switch (identifier) {
            case '_txtHour':
                var strTime = zeroTime;
                switch (pageState) {
                    case 'Load':
                        break;
                    case 'Change':
                        if (cmbRequestType_tbHourly_RequestRegister.getSelectedItem() != undefined) {
                            var ObjRequestTargetFeatures = cmbRequestType_tbHourly_RequestRegister.getSelectedItem().get_value();
                            ObjRequestTargetFeatures = eval('(' + ObjRequestTargetFeatures + ')');
                            if (ObjRequestTargetFeatures.IsTraffic)
                                strTime = NullTime_RequestRegister;
                        }
                        break;
                }
                document.getElementById(TimeSelectorPartID + identifier).value = strTime;
                break;
            case '_txtMinute':
                document.getElementById(TimeSelectorPartID + identifier).value = zeroTime;
                intOnly(document.getElementById(TimeSelectorPartID + identifier), 24);
                break;
        }
    }
    else {
        intOnly(document.getElementById(TimeSelectorPartID + identifier), 24);
    }
}

function RequestRegister_onKeyDown(event) {
    var activeID = null;
    if (event.keyCode == 38 || event.keyCode == 40) {
        activeID = document.activeElement.id;
        CheckTimePickerState_RequestRegister(activeID);
    }
}

function CheckTimePickerState_RequestRegister(TimeSelector) {
    if ((TimeSelector == 'TimeSelector_FromHour_tbHourly_RequestRegister_txtHour' && isNaN(document.getElementById(TimeSelector).value)) || (TimeSelector == 'TimeSelector_FromHour_tbHourly_RequestRegister_txtMinute' && (isNaN(document.getElementById(TimeSelector).value) || document.getElementById('TimeSelector_FromHour_tbHourly_RequestRegister_txtHour').value == NullTime_RequestRegister)) || (TimeSelector == 'TimeSelector_FromHour_tbHourly_RequestRegister_txtHour' && parseInt(document.getElementById(TimeSelector).value) < 0))
        document.getElementById('TimeSelector_FromHour_tbHourly_RequestRegister_txtHour').value = zeroTime;
    if ((TimeSelector == 'TimeSelector_ToHour_tbHourly_RequestRegister_txtHour' && isNaN(document.getElementById(TimeSelector).value)) || (TimeSelector == 'TimeSelector_ToHour_tbHourly_RequestRegister_txtMinute' && (isNaN(document.getElementById(TimeSelector).value) || document.getElementById('TimeSelector_ToHour_tbHourly_RequestRegister_txtHour').value == NullTime_RequestRegister)) || (TimeSelector == 'TimeSelector_ToHour_tbHourly_RequestRegister_txtHour' && parseInt(document.getElementById(TimeSelector).value) < 0))
        document.getElementById('TimeSelector_ToHour_tbHourly_RequestRegister_txtHour').value = zeroTime;
    if ((TimeSelector == 'TimeSelector_FromHour_tbOverTime_RequestRegister_txtHour' && isNaN(document.getElementById(TimeSelector).value)) || (TimeSelector == 'TimeSelector_FromHour_tbOverTime_RequestRegister_txtMinute' && (isNaN(document.getElementById(TimeSelector).value) || document.getElementById('TimeSelector_FromHour_tbOverTime_RequestRegister_txtHour').value == NullTime_RequestRegister)) || (TimeSelector == 'TimeSelector_FromHour_tbOverTime_RequestRegister_txtHour' && parseInt(document.getElementById(TimeSelector).value) < 0))
        document.getElementById('TimeSelector_FromHour_tbOverTime_RequestRegister_txtHour').value = zeroTime;
    if ((TimeSelector == 'TimeSelector_ToHour_tbOverTime_RequestRegister_txtHour' && isNaN(document.getElementById(TimeSelector).value)) || (TimeSelector == 'TimeSelector_ToHour_tbOverTime_RequestRegister_txtMinute' && (isNaN(document.getElementById(TimeSelector).value) || document.getElementById('TimeSelector_ToHour_tbOverTime_RequestRegister_txtHour').value == NullTime_RequestRegister)) || (TimeSelector == 'TimeSelector_ToHour_tbOverTime_RequestRegister_txtHour' && parseInt(document.getElementById(TimeSelector).value) < 0))
        document.getElementById('TimeSelector_ToHour_tbOverTime_RequestRegister_txtHour').value = zeroTime;
    if ((TimeSelector == 'TimeSelector_Duration_tbOverTime_RequestRegister_txtHour' && isNaN(document.getElementById(TimeSelector).value)) || (TimeSelector == 'TimeSelector_Duration_tbOverTime_RequestRegister_txtMinute' && (isNaN(document.getElementById(TimeSelector).value) || document.getElementById('TimeSelector_Duration_tbOverTime_RequestRegister_txtHour').value == NullTime_RequestRegister)) || (TimeSelector == 'TimeSelector_Duration_tbOverTime_RequestRegister_txtHour' && parseInt(document.getElementById(TimeSelector).value) < 0))
        document.getElementById('TimeSelector_Duration_tbOverTime_RequestRegister_txtHour').value = zeroTime;
    if ((TimeSelector == 'TimeSelector_FromHour_tbHourly_RequestRegister_txtMinute' || TimeSelector == 'TimeSelector_ToHour_tbHourly_RequestRegister_txtMinute' || TimeSelector == 'TimeSelector_FromHour_tbOverTime_RequestRegister_txtMinute' || TimeSelector == 'TimeSelector_ToHour_tbOverTime_RequestRegister_txtMinute' || TimeSelector == 'TimeSelector_ToHour_tbOverTime_RequestRegister_txtMinute' || TimeSelector == 'TimeSelector_Duration_tbOverTime_RequestRegister_txtMinute') && (isNaN(document.getElementById(TimeSelector).value) || parseInt(document.getElementById(TimeSelector).value) < 0))
        document.getElementById(TimeSelector).value = zeroTime;
}


function ViewCurrentLangCalendars_RequestRegister() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpRequestDate_tbHourly_RequestRegister").parentNode.removeChild(document.getElementById("pdpRequestDate_tbHourly_RequestRegister"));
            document.getElementById("pdpRequestDate_tbHourly_RequestRegisterimgbt").parentNode.removeChild(document.getElementById("pdpRequestDate_tbHourly_RequestRegisterimgbt"));
            document.getElementById("pdpFromDate_tbDaily_RequestRegister").parentNode.removeChild(document.getElementById("pdpFromDate_tbDaily_RequestRegister"));
            document.getElementById("pdpFromDate_tbDaily_RequestRegisterimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_tbDaily_RequestRegisterimgbt"));
            document.getElementById("pdpToDate_tbDaily_RequestRegister").parentNode.removeChild(document.getElementById("pdpToDate_tbDaily_RequestRegister"));
            document.getElementById("pdpToDate_tbDaily_RequestRegisterimgbt").parentNode.removeChild(document.getElementById("pdpToDate_tbDaily_RequestRegisterimgbt"));
            document.getElementById("pdpFromDate_tbOverTime_RequestRegister").parentNode.removeChild(document.getElementById("pdpFromDate_tbOverTime_RequestRegister"));
            document.getElementById("pdpFromDate_tbOverTime_RequestRegisterimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_tbOverTime_RequestRegisterimgbt"));
            document.getElementById("pdpToDate_tbOverTime_RequestRegister").parentNode.removeChild(document.getElementById("pdpToDate_tbOverTime_RequestRegister"));
            document.getElementById("pdpToDate_tbOverTime_RequestRegisterimgbt").parentNode.removeChild(document.getElementById("pdpToDate_tbOverTime_RequestRegisterimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_RequestDateCalendars_tbHourly_RequestRegister").removeChild(document.getElementById("Container_gCalRequestDate_tbHourly_RequestRegister"));
            document.getElementById("Container_FromDateCalendars_tbDaily_RequestRegister").removeChild(document.getElementById("Container_gCalFromDate_tbDaily_RequestRegister"));
            document.getElementById("Container_ToDateCalendars_tbDaily_RequestRegister").removeChild(document.getElementById("Container_gCalToDate_tbDaily_RequestRegister"));
            document.getElementById("Container_FromDateCalendars_tbOverTime_RequestRegister").removeChild(document.getElementById("Container_gCalFromDate_tbOverTime_RequestRegister"));
            document.getElementById("Container_ToDateCalendars_tbOverTime_RequestRegister").removeChild(document.getElementById("Container_gCalToDate_tbOverTime_RequestRegister"));
            break;
    }
}

function SetButtonImages_TimeSelectors_DialogRequestRegister() {
    SetButtonImages_TimeSelector_DialogRequestRegister('TimeSelector_FromHour_tbHourly_RequestRegister');
    SetButtonImages_TimeSelector_DialogRequestRegister('TimeSelector_ToHour_tbHourly_RequestRegister');
    SetButtonImages_TimeSelector_DialogRequestRegister('TimeSelector_FromHour_tbOverTime_RequestRegister');
    SetButtonImages_TimeSelector_DialogRequestRegister('TimeSelector_ToHour_tbOverTime_RequestRegister');
    SetButtonImages_TimeSelector_DialogRequestRegister('TimeSelector_Duration_tbOverTime_RequestRegister');
}

function SetButtonImages_TimeSelector_DialogRequestRegister(TimeSelector) {
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

function btn_gdpRequestDate_tbHourly_RequestRegister_OnMouseUp(event) {
    if (gCalRequestDate_tbHourly_RequestRegister.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function btn_gdpRequestDate_tbHourly_RequestRegister_OnClick(event) {
    if (gCalRequestDate_tbHourly_RequestRegister.get_popUpShowing()) {
        gCalRequestDate_tbHourly_RequestRegister.hide();
    }
    else {
        gCalRequestDate_tbHourly_RequestRegister.setSelectedDate(gdpRequestDate_tbHourly_RequestRegister.getSelectedDate());
        gCalRequestDate_tbHourly_RequestRegister.show();
    }
}

function gdpRequestDate_tbHourly_RequestRegister_OnDateChange(sender, e) {
    var RequestDate = gdpRequestDate_tbHourly_RequestRegister.getSelectedDate();
    gCalRequestDate_tbHourly_RequestRegister.setSelectedDate(RequestDate);
}

function gCalRequestDate_tbHourly_RequestRegister_OnChange(sender, e) {
    var RequestDate = gCalRequestDate_tbHourly_RequestRegister.getSelectedDate();
    gdpRequestDate_tbHourly_RequestRegister.setSelectedDate(RequestDate);
}

function gCalRequestDate_tbHourly_RequestRegister_OnLoad(sender, e) {
    window.gCalRequestDate_tbHourly_RequestRegister.PopUpObject.z = 25000000;
}

function btn_gdpFromDate_tbDaily_RequestRegister_OnMouseUp(event) {
    if (gCalFromDate_tbDaily_RequestRegister.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function btn_gdpFromDate_tbDaily_RequestRegister_OnClick(event) {
    if (gCalFromDate_tbDaily_RequestRegister.get_popUpShowing()) {
        gCalFromDate_tbDaily_RequestRegister.hide();
    }
    else {
        gCalFromDate_tbDaily_RequestRegister.setSelectedDate(gdpFromDate_tbDaily_RequestRegister.getSelectedDate());
        gCalFromDate_tbDaily_RequestRegister.show();
    }
}

function gdpFromDate_tbDaily_RequestRegister_OnDateChange(sender, e) {
    var FromDate = gdpFromDate_tbDaily_RequestRegister.getSelectedDate();
    gCalFromDate_tbDaily_RequestRegister.setSelectedDate(FromDate);
}

function gCalFromDate_tbDaily_RequestRegister_OnChange(sender, e) {
    var FromDate = gCalFromDate_tbDaily_RequestRegister.getSelectedDate();
    gdpFromDate_tbDaily_RequestRegister.setSelectedDate(FromDate);
}

function gCalFromDate_tbDaily_RequestRegister_OnLoad(sender, e) {
    window.gCalFromDate_tbDaily_RequestRegister.PopUpObject.z = 25000000;
}

function btn_gdpToDate_tbDaily_RequestRegister_OnMouseUp(event) {
    if (gCalToDate_tbDaily_RequestRegister.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function btn_gdpToDate_tbDaily_RequestRegister_OnClick(event) {
    if (gCalToDate_tbDaily_RequestRegister.get_popUpShowing()) {
        gCalToDate_tbDaily_RequestRegister.hide();
    }
    else {
        gCalToDate_tbDaily_RequestRegister.setSelectedDate(gdpToDate_tbDaily_RequestRegister.getSelectedDate());
        gCalToDate_tbDaily_RequestRegister.show();
    }
}

function gdpToDate_tbDaily_RequestRegister_OnDateChange(sender, e) {
    var ToDate = gdpToDate_tbDaily_RequestRegister.getSelectedDate();
    gCalToDate_tbDaily_RequestRegister.setSelectedDate(ToDate);
}

function gCalToDate_tbDaily_RequestRegister_OnChange(sender, e) {
    var ToDate = gCalToDate_tbDaily_RequestRegister.getSelectedDate();
    gdpToDate_tbDaily_RequestRegister.setSelectedDate(ToDate);
}

function gCalToDate_tbDaily_RequestRegister_OnLoad(sender, e) {
    window.gCalToDate_tbDaily_RequestRegister.PopUpObject.z = 25000000;
}

function btn_gdpFromDate_tbOverTime_RequestRegister_OnMouseUp(event) {
    if (gCalFromDate_tbOverTime_RequestRegister.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function btn_gdpFromDate_tbOverTime_RequestRegister_OnClick(event) {
    if (gCalFromDate_tbOverTime_RequestRegister.get_popUpShowing()) {
        gCalFromDate_tbOverTime_RequestRegister.hide();
    }
    else {
        gCalFromDate_tbOverTime_RequestRegister.setSelectedDate(gdpFromDate_tbOverTime_RequestRegister.getSelectedDate());
        gCalFromDate_tbOverTime_RequestRegister.show();
    }
}

function gdpFromDate_tbOverTime_RequestRegister_OnDateChange(sender, e) {
    var FromDate = gdpFromDate_tbOverTime_RequestRegister.getSelectedDate();
    gCalFromDate_tbOverTime_RequestRegister.setSelectedDate(FromDate);
}

function gCalFromDate_tbOverTime_RequestRegister_OnChange(sender, e) {
    var FromDate = gCalFromDate_tbOverTime_RequestRegister.getSelectedDate();
    gdpFromDate_tbOverTime_RequestRegister.setSelectedDate(FromDate);
}

function gCalFromDate_tbOverTime_RequestRegister_OnLoad(sender, e) {
    window.gCalFromDate_tbOverTime_RequestRegister.PopUpObject.z = 25000000;
}

function btn_gdpToDate_tbOverTime_RequestRegister_OnMouseUp(event) {
    if (gCalToDate_tbOverTime_RequestRegister.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function btn_gdpToDate_tbOverTime_RequestRegister_OnClick(event) {
    if (gCalToDate_tbOverTime_RequestRegister.get_popUpShowing()) {
        gCalToDate_tbOverTime_RequestRegister.hide();
    }
    else {
        gCalToDate_tbOverTime_RequestRegister.setSelectedDate(gdpToDate_tbOverTime_RequestRegister.getSelectedDate());
        gCalToDate_tbOverTime_RequestRegister.show();
    }

}

function gdpToDate_tbOverTime_RequestRegister_OnDateChange(sender, e) {
    var ToDate = gdpToDate_tbOverTime_RequestRegister.getSelectedDate();
    gCalToDate_tbOverTime_RequestRegister.setSelectedDate(ToDate);
}

function gCalToDate_tbOverTime_RequestRegister_OnChange(sender, e) {
    var ToDate = gCalToDate_tbOverTime_RequestRegister.getSelectedDate();
    gdpToDate_tbOverTime_RequestRegister.setSelectedDate(ToDate);
}

function gCalToDate_tbOverTime_RequestRegister_OnLoad(sender, e) {
    window.gCalToDate_tbOverTime_RequestRegister.PopUpObject.z = 25000000;
}

function tlbItemEndorsement_TlbHourly_onClick() {
    UpdateRequest_RequestRegister();
}

function tlbItemExit_TlbHourly_onClick() {
    ShowDialogConfirm('RequestRegister');
}

function ShowDialogConfirm(caller) {
    switch (caller) {
        case 'RequestRegister':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_RequestRegister').value;
            break;
        case 'CollectiveTraffic':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_CollectiveTraffic').value;
            break;
    }
    DialogConfirm.set_value(caller);
    DialogConfirm.Show();
    CollapseControls_RequestRegister();
}

function cmbIllnesses_tbHourly_RequestRegister_onExpand(sender, e) {
    if (cmbIllnesses_tbHourly_RequestRegister.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbIllnesses_tbHourly_RequestRegister == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbIllnesses_tbHourly_RequestRegister = true;
        CallBack_cmbIllnesses_tbHourly_RequestRegister.callback();
    }
}

function CallBack_cmbIllnesses_tbHourly_RequestRegister_onBeforeCallback(sender, e) {
    cmbIllnesses_tbHourly_RequestRegister.dispose();
}

function CallBack_cmbIllnesses_tbHourly_RequestRegister_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Illnesses_tbHourly_RequestRegister').value;
    if (error == "") {
        document.getElementById('cmbIllnesses_tbHourly_RequestRegister_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbIllnesses_tbHourly_RequestRegister_DropImage').mousedown();
        cmbIllnesses_tbHourly_RequestRegister.expand();
        ChangeControlDirection_RequestRegister('cmbIllnesses_tbHourly_RequestRegister_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbIllnesses_tbHourly_RequestRegister_DropDown').style.display = 'none';
    }
}

function CallBack_cmbIllnesses_tbHourly_RequestRegister_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}


function cmbDoctors_tbHourly_RequestRegister_onExpand(sender, e) {
    if (cmbDoctors_tbHourly_RequestRegister.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDoctors_tbHourly_RequestRegister == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDoctors_tbHourly_RequestRegister = true;
        CallBack_cmbDoctors_tbHourly_RequestRegister.callback();
    }
}

function CallBack_cmbDoctors_tbHourly_RequestRegister_onBeforeCallback(sender, e) {
    cmbDoctors_tbHourly_RequestRegister.dispose();
}

function CallBack_cmbDoctors_tbHourly_RequestRegister_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Doctors_tbHourly_RequestRegister').value;
    if (error == "") {
        document.getElementById('cmbDoctors_tbHourly_RequestRegister_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDoctors_tbHourly_RequestRegister_DropImage').mousedown();
        cmbDoctors_tbHourly_RequestRegister.expand();
        ChangeControlDirection_RequestRegister('cmbDoctors_tbHourly_RequestRegister_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbDoctors_tbHourly_RequestRegister_DropDown').style.display = 'none';
    }
}

function CallBack_cmbDoctors_tbHourly_RequestRegister_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function trvMissionLocation_tbHourly_RequestRegister_onNodeSelect(sender, e) {
    document.getElementById('cmbMissionLocation_tbHourly_RequestRegister_TextBox').innerHTML = e.get_node().get_text();
    cmbMissionLocation_tbHourly_RequestRegister.collapse();
}

function cmbMissionLocation_tbHourly_RequestRegister_onExpand(sender, e) {
    if (trvMissionLocation_tbHourly_RequestRegister.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionLocation_tbHourly_RequestRegister == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionLocation_tbHourly_RequestRegister == true;
        CallBack_cmbMissionLocation_tbHourly_RequestRegister.callback();
    }
}

function CallBack_cmbMissionLocation_tbHourly_RequestRegister_onBeforeCallback(sender, e) {
    cmbMissionLocation_tbHourly_RequestRegister.dispose();
}

function CallBack_cmbMissionLocation_tbHourly_RequestRegister_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Doctors_tbHourly_RequestRegister').value;
    if (error == "") {
        document.getElementById('cmbMissionLocation_tbHourly_RequestRegister_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMissionLocation_tbHourly_RequestRegister_DropImage').mousedown();
        cmbMissionLocation_tbHourly_RequestRegister.expand();
        ChangeControlDirection_RequestRegister('cmbMissionLocation_tbHourly_RequestRegister_DropDownContent');
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbMissionLocation_tbHourly_RequestRegister_DropDown').style.display = 'none';
    }
}

function CallBack_cmbMissionLocation_tbHourly_RequestRegister_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function cmbRequestType_tbHourly_RequestRegister_onChange(sender, e) {
    if (cmbRequestType_tbHourly_RequestRegister.getSelectedItem() != undefined) {
        var ObjRequestTargetFeatures = cmbRequestType_tbHourly_RequestRegister.getSelectedItem().get_value();
        ObjRequestTargetFeatures = eval('(' + ObjRequestTargetFeatures + ')');
        initTimePickers_RequestRegister('Change');
        ChangeHideElementsState_RequestRegister(ObjRequestTargetFeatures.IsSickLeave, ObjRequestTargetFeatures.IsMission, false);
    }
}

function cmbRequestType_tbHourly_RequestRegister_onExpand(sender, e) {
    if (cmbRequestType_tbHourly_RequestRegister.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRequestType_tbHourly_RequestRegister == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRequestType_tbHourly_RequestRegister = true;
        CallBack_cmbRequestType_tbHourly_RequestRegister.callback();
    }
}

function cmbRequestType_tbHourly_RequestRegister_onCollapse(sender, e) {
    if (cmbRequestType_tbHourly_RequestRegister.getSelectedItem() == undefined)
        document.getElementById('cmbRequestType_tbHourly_RequestRegister_TextBox').innerHTML = document.getElementById('hfcmbAlarm_RequestRegister').value;
}

function CallBack_cmbRequestType_tbHourly_RequestRegister_onBeforeCallback(sender, e) {
    cmbRequestType_tbHourly_RequestRegister.dispose();
}

function CallBack_cmbRequestType_tbHourly_RequestRegister_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RequestTypes_tbHourly_RequestRegister').value;
    if (error == "") {
        document.getElementById('cmbRequestType_tbHourly_RequestRegister_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbRequestType_tbHourly_RequestRegister_DropImage').mousedown();
        cmbRequestType_tbHourly_RequestRegister.expand();
        ChangeControlDirection_RequestRegister('cmbRequestType_tbHourly_RequestRegister_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbRequestType_tbHourly_RequestRegister_DropDown').style.display = 'none';
    }
}

function tlbItemEndorsement_TlbDaily_onClick() {
    UpdateRequest_RequestRegister();
}

function tlbItemExit_TlbDaily_onClick() {
    ShowDialogConfirm('RequestRegister');
}

function cmbIllnesses_tbDaily_RequestRegister_onExpand(sender, e) {
    if (cmbIllnesses_tbDaily_RequestRegister.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbIllnesses_tbDaily_RequestRegister == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbIllnesses_tbDaily_RequestRegister = true;
        CallBack_cmbIllnesses_tbDaily_RequestRegister.callback();
    }
}

function CallBack_cmbIllnesses_tbDaily_RequestRegister_onBeforeCallback(sender, e) {
    cmbIllnesses_tbDaily_RequestRegister.dispose();
}

function CallBack_cmbIllnesses_tbDaily_RequestRegister_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Illnesses_tbDaily_RequestRegister').value;
    if (error == "") {
        document.getElementById('cmbIllnesses_tbDaily_RequestRegister_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbIllnesses_tbDaily_RequestRegister_DropImage').mousedown();
        cmbIllnesses_tbDaily_RequestRegister.expand();
        ChangeControlDirection_RequestRegister('cmbIllnesses_tbDaily_RequestRegister_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbIllnesses_tbDaily_RequestRegister_DropDown').style.display = 'none';
    }
}

function CallBack_cmbIllnesses_tbDaily_RequestRegister_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function cmbDoctors_tbDaily_RequestRegister_onExpand(sender, e) {
    if (cmbDoctors_tbDaily_RequestRegister.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDoctors_tbDaily_RequestRegister == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDoctors_tbDaily_RequestRegister = true;
        CallBack_cmbDoctors_tbDaily_RequestRegister.callback();
    }
}

function CallBack_cmbDoctors_tbDaily_RequestRegister_onBeforeCallback(sender, e) {
    cmbDoctors_tbDaily_RequestRegister.dispose();
}

function CallBack_cmbDoctors_tbDaily_RequestRegister_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Doctors_tbDaily_RequestRegister').value;
    if (error == "") {
        document.getElementById('cmbDoctors_tbDaily_RequestRegister_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDoctors_tbDaily_RequestRegister_DropImage').mousedown();
        cmbDoctors_tbDaily_RequestRegister.expand();
        ChangeControlDirection_RequestRegister('cmbDoctors_tbDaily_RequestRegister_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbDoctors_tbDaily_RequestRegister_DropDown').style.display = 'none';
    }
}

function CallBack_cmbDoctors_tbDaily_RequestRegister_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function trvMissionLocation_tbDaily_RequestRegister_onNodeSelect(sender, e) {
    document.getElementById('cmbMissionLocation_tbDaily_RequestRegister_TextBox').innerHTML = e.get_node().get_text();
    cmbMissionLocation_tbDaily_RequestRegister.collapse();
}

function cmbMissionLocation_tbDaily_RequestRegister_onExpand(sender, e) {
    if (trvMissionLocation_tbDaily_RequestRegister.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionLocation_tbDaily_RequestRegister == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionLocation_tbDaily_RequestRegister == true;
        CallBack_cmbMissionLocation_tbDaily_RequestRegister.callback();
    }
}

function CallBack_cmbMissionLocation_tbDaily_RequestRegister_onBeforeCallback(sender, e) {
    cmbMissionLocation_tbDaily_RequestRegister.dispose();
}

function CallBack_cmbMissionLocation_tbDaily_RequestRegister_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MissionLocations_tbDaily_RequestRegister').value;
    if (error == "") {
        document.getElementById('cmbMissionLocation_tbDaily_RequestRegister_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMissionLocation_tbDaily_RequestRegister_DropImage').mousedown();
        cmbMissionLocation_tbDaily_RequestRegister.expand();
        ChangeControlDirection_RequestRegister('cmbMissionLocation_tbDaily_RequestRegister_DropDownContent');
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbMissionLocation_tbDaily_RequestRegister_DropDown').style.display = 'none';
    }
}

function CallBack_cmbMissionLocation_tbDaily_RequestRegister_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function cmbRequestType_tbDaily_RequestRegister_onChange(sender, e) {
    if (cmbRequestType_tbDaily_RequestRegister.getSelectedItem() != undefined) {
        var ObjRequestTargetFeatures = cmbRequestType_tbDaily_RequestRegister.getSelectedItem().get_value();
        ObjRequestTargetFeatures = eval('(' + ObjRequestTargetFeatures + ')');
        ChangeHideElementsState_RequestRegister(ObjRequestTargetFeatures.IsSickLeave, ObjRequestTargetFeatures.IsMission, false);
    }
}

function cmbRequestType_tbDaily_RequestRegister_onExpand(sender, e) {
    if (cmbRequestType_tbDaily_RequestRegister.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRequestType_tbDaily_RequestRegister == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRequestType_tbDaily_RequestRegister = true;
        CallBack_cmbRequestType_tbDaily_RequestRegister.callback();
    }
}

function cmbRequestType_tbDaily_RequestRegister_onCollapse(sender, e) {
    if (cmbRequestType_tbDaily_RequestRegister.getSelectedItem() == undefined)
        document.getElementById('cmbRequestType_tbDaily_RequestRegister_TextBox').innerHTML = document.getElementById('hfcmbAlarm_RequestRegister').value;
}

function CallBack_cmbRequestType_tbDaily_RequestRegister_onBeforeCallback(sender, e) {
    cmbRequestType_tbDaily_RequestRegister.dispose();
}

function CallBack_cmbRequestType_tbDaily_RequestRegister_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RequestTypes_tbDaily_RequestRegister').value;
    if (error == "") {
        document.getElementById('cmbRequestType_tbDaily_RequestRegister_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbRequestType_tbDaily_RequestRegister_DropImage').mousedown();
        cmbRequestType_tbDaily_RequestRegister.expand();
        ChangeControlDirection_RequestRegister('cmbRequestType_tbDaily_RequestRegister_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbRequestType_tbDaily_RequestRegister_DropDown').style.display = 'none';
    }
}

function CallBack_cmbRequestType_tbDaily_RequestRegister_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function tlbItemEndorsement_TlbOverTime_onClick() {
    UpdateRequest_RequestRegister();
}

function tlbItemExit_TlbOverTime_onClick() {
    ShowDialogConfirm('RequestRegister');
}

function cmbRequestType_tbOverTime_RequestRegister_onExpand(sender, e) {
    if (cmbRequestType_tbOverTime_RequestRegister.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRequestType_tbOverTime_RequestRegister == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRequestType_tbOverTime_RequestRegister = true;
        CallBack_cmbRequestType_tbOverTime_RequestRegister.callback();
    }
}

function cmbRequestType_tbOverTime_RequestRegister_onCollapse(sender, e) {
    if (cmbRequestType_tbOverTime_RequestRegister.getSelectedItem() == undefined)
        document.getElementById('cmbRequestType_tbOverTime_RequestRegister_TextBox').innerHTML = document.getElementById('hfcmbAlarm_RequestRegister').value;
}

function cmbRequestType_tbOverTime_RequestRegister_onBeforeCallback(sender, e) {
    cmbRequestType_tbOverTime_RequestRegister.dispose();
}

function cmbRequestType_tbOverTime_RequestRegister_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RequestTypes_tbOverTime_RequestRegister').value;
    if (error == "") {
        document.getElementById('cmbRequestType_tbOverTime_RequestRegister_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbRequestType_tbOverTime_RequestRegister_DropImage').mousedown();
        cmbRequestType_tbOverTime_RequestRegister.expand();
        ChangeControlDirection_RequestRegister('cmbRequestType_tbOverTime_RequestRegister_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbRequestType_tbOverTime_RequestRegister_DropDown').style.display = 'none';
    }
}

function cmbRequestType_tbOverTime_RequestRegister_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    var ConfirmCaller = DialogConfirm.get_value();
    switch (ConfirmCaller) {
        case 'RequestRegister':
            RequestRegister_onClose();
            break;
        case 'CollectiveTraffic':
            CollectiveTraffic_onClose();
            break;
    }
    DialogConfirm.Close();
}

function RequestRegister_onClose() {
    if (ObjRequestRegister != null) {
        parent.parent.document.getElementById('DialogRegisteredRequests_IFrame').contentWindow.RequestRegister_onAfterUpdate(0);
        parent.document.getElementById('DialogRequestRegister_IFrame').src = 'WhitePage.aspx';
        parent.DialogRequestRegister.Close();
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function ShowConnectionError_RequestRegister() {
    var error = document.getElementById('hfErrorType_RequestRegister').value;
    var errorBody = document.getElementById('hfConnectionError_RequestRegister').value;
    showDialog(error, errorBody, 'error');
}

function UpdateRequest_RequestRegister() {
    ObjRequestTarget_RequestTarget = new Object();
    ObjRequestTarget_RequestTarget.RequestCaller = '';
    ObjRequestTarget_RequestTarget.RequestPersonnelCountState = RequestPersonnelCountState_RequestRegister;
    ObjRequestTarget_RequestTarget.SinglePersonnelID = '0';
    ObjRequestTarget_RequestTarget.CollectiveConditionsLoadState = 'Normal';
    ObjRequestTarget_RequestTarget.CollectiveConditions = null;
    ObjRequestTarget_RequestTarget.StrPersonnelList = null;
    ObjRequestTarget_RequestTarget.RequestTarget = GetCurrentRequestTarget_RequestRegister();
    ObjRequestTarget_RequestTarget.Year = null;
    ObjRequestTarget_RequestTarget.Month = null;
    ObjRequestTarget_RequestTarget.PageSize = '0';
    ObjRequestTarget_RequestTarget.ID = '0';
    ObjRequestTarget_RequestTarget.PreCardID = '0';
    ObjRequestTarget_RequestTarget.PreCardTitle = null;
    ObjRequestTarget_RequestTarget.RequestDate = null;
    ObjRequestTarget_RequestTarget.FromDate = null;
    ObjRequestTarget_RequestTarget.ToDate = null;
    ObjRequestTarget_RequestTarget.FromTime = null;
    ObjRequestTarget_RequestTarget.ToTime = null;
    ObjRequestTarget_RequestTarget.IsToTimeInNextDay = false;
    ObjRequestTarget_RequestTarget.Duration = '00:00';
    ObjRequestTarget_RequestTarget.Description = null;
    ObjRequestTarget_RequestTarget.IsSeakLeave = false;
    ObjRequestTarget_RequestTarget.IllnessID = '0';
    ObjRequestTarget_RequestTarget.DoctorID = '0';
    ObjRequestTarget_RequestTarget.IsMission = false;
    ObjRequestTarget_RequestTarget.MissionLocationID = '0';
    ObjRequestTarget_RequestTarget.Value = '0';
    ObjRequestTarget_RequestTarget.AttachmentFile = null;

    var ObjRequestRegister = parent.DialogRequestRegister.get_value();
    ObjRequestTarget_RequestTarget.RequestCaller = ObjRequestRegister.Caller;
    if (ObjRequestTarget_RequestTarget.RequestTarget != 'Imperative') {
        switch (ObjRequestTarget_RequestTarget.RequestCaller) {
            case 'NormalUser':
                break;
            case 'Operator':
                switch (ObjRequestTarget_RequestTarget.RequestPersonnelCountState) {
                    case 'Single':
                        if (cmbPersonnel_RequestRegister.getSelectedItem() != undefined) {
                            var ObjPersonnel = cmbPersonnel_RequestRegister.getSelectedItem().get_value();
                            ObjPersonnel = eval('(' + ObjPersonnel + ')');
                            ObjRequestTarget_RequestTarget.SinglePersonnelID = ObjPersonnel.ID;
                        }
                        break;
                    case 'Collective':
                        ObjRequestTarget_RequestTarget.CollectiveConditionsLoadState = LoadState_cmbPersonnel_RequestRegister;
                        ObjRequestTarget_RequestTarget.CollectiveConditions = CollectiveConditions;
                        ObjRequestTarget_RequestTarget.StrPersonnelList = StrUnCollectivePersonnelList_CollectiveTraffic;
                        break;
                }
                break;
        }
        ObjRequestTarget_RequestTarget.Year = ObjRequestRegister.Year;
        ObjRequestTarget_RequestTarget.Month = ObjRequestRegister.Month;
    }
    else {
        ObjRequestTarget_RequestTarget.CollectiveConditionsLoadState = LoadState_cmbPersonnel_RequestRegister;
        switch (LoadState_cmbPersonnel_RequestRegister) {
            case 'Normal':
                ObjRequestTarget_RequestTarget.CollectiveConditions = '';
                break;
            case 'Search':
                ObjRequestTarget_RequestTarget.CollectiveConditions = SearchTerm_cmbPersonnel_RequestRegister;
                break;
            case 'AdvancedSearch':
                ObjRequestTarget_RequestTarget.CollectiveConditions = AdvancedSearchTerm_cmbPersonnel_RequestRegister;
                break;
        }
    }
    ObjRequestTarget_RequestTarget.PageSize = ObjRequestRegister.PageSize;
    switch (ObjRequestTarget_RequestTarget.RequestTarget) {
        case 'Hourly':
            if (cmbRequestType_tbHourly_RequestRegister.getSelectedItem() != undefined) {
                ObjRequestTarget_RequestTarget.PreCardID = cmbRequestType_tbHourly_RequestRegister.getSelectedItem().get_id();
                ObjRequestTarget_RequestTarget.PreCardTitle = cmbRequestType_tbHourly_RequestRegister.getSelectedItem().get_text();
            }
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    ObjRequestTarget_RequestTarget.RequestDate = document.getElementById('pdpRequestDate_tbHourly_RequestRegister').value;
                    break;
                case 'en-Us':
                    ObjRequestTarget_RequestTarget.RequestDate = document.getElementById('gdpRequestDate_tbHourly_RequestRegister_picker').value;
                    break;
            }
            ObjRequestTarget_RequestTarget.FromTime = GetDuration_TimePicker_RequestRegister('TimeSelector_FromHour_tbHourly_RequestRegister');
            ObjRequestTarget_RequestTarget.ToTime = GetDuration_TimePicker_RequestRegister('TimeSelector_ToHour_tbHourly_RequestRegister');
            if (document.getElementById('chbToHourInNextDay_tbHourly_RequestRegister').checked)
                ObjRequestTarget_RequestTarget.IsToTimeInNextDay = true;
            ObjRequestTarget_RequestTarget.Description = document.getElementById('txtDescription_tbHourly_RequestRegister').value;
            var ObjRequestTargetFeatures = GetRequestTargetFeatures_RequestRegister();
            if (ObjRequestTargetFeatures != null) {
                if (ObjRequestTargetFeatures.IsSickLeave) {
                    ObjRequestTarget_RequestTarget.IsSeakLeave = true;
                    if (cmbIllnesses_tbHourly_RequestRegister.getSelectedItem() != undefined)
                        ObjRequestTarget_RequestTarget.IllnessID = cmbIllnesses_tbHourly_RequestRegister.getSelectedItem().get_value();
                    if (cmbDoctors_tbHourly_RequestRegister.getSelectedItem() != undefined)
                        ObjRequestTarget_RequestTarget.DoctorID = cmbDoctors_tbHourly_RequestRegister.getSelectedItem().get_value();
                }
                if (ObjRequestTargetFeatures.IsMission) {
                    ObjRequestTarget_RequestTarget.IsMission = true;
                    if (trvMissionLocation_tbHourly_RequestRegister.get_selectedNode() != undefined) {
                        if (trvMissionLocation_tbHourly_RequestRegister.get_selectedNode().get_parentNode() != undefined)
                            ObjRequestTarget_RequestTarget.MissionLocationID = trvMissionLocation_tbHourly_RequestRegister.get_selectedNode().get_id();
                    }
                }
            }
            if (ObjRequestAttachment_RequestRegister != null && ObjRequestAttachment_RequestRegister.Hourly != null)
                ObjRequestTarget_RequestTarget.AttachmentFile = ObjRequestAttachment_RequestRegister.Hourly.RequestAttachmentSavedName;
            break;
        case 'Daily':
            if (cmbRequestType_tbDaily_RequestRegister.getSelectedItem() != undefined) {
                ObjRequestTarget_RequestTarget.PreCardID = cmbRequestType_tbDaily_RequestRegister.getSelectedItem().get_id();
                ObjRequestTarget_RequestTarget.PreCardTitle = cmbRequestType_tbDaily_RequestRegister.getSelectedItem().get_text();
            }
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    ObjRequestTarget_RequestTarget.FromDate = document.getElementById('pdpFromDate_tbDaily_RequestRegister').value;
                    ObjRequestTarget_RequestTarget.ToDate = document.getElementById('pdpToDate_tbDaily_RequestRegister').value;
                    break;
                case 'en-Us':
                    ObjRequestTarget_RequestTarget.FromDate = document.getElementById('gdpFromDate_tbDaily_RequestRegister_picker').value;
                    ObjRequestTarget_RequestTarget.ToDate = document.getElementById('gdpToDate_tbDaily_RequestRegister_picker').value;
                    break;
            }
            ObjRequestTarget_RequestTarget.Description = document.getElementById('txtDescription_tbDaily_RequestRegister').value;
            var ObjRequestTargetFeatures = GetRequestTargetFeatures_RequestRegister();
            if (ObjRequestTargetFeatures != null) {
                if (ObjRequestTargetFeatures.IsSickLeave) {
                    ObjRequestTarget_RequestTarget.IsSeakLeave = true;
                    if (cmbIllnesses_tbDaily_RequestRegister.getSelectedItem() != undefined)
                        ObjRequestTarget_RequestTarget.IllnessID = cmbIllnesses_tbDaily_RequestRegister.getSelectedItem().get_value();
                    if (cmbDoctors_tbDaily_RequestRegister.getSelectedItem() != undefined)
                        ObjRequestTarget_RequestTarget.DoctorID = cmbDoctors_tbDaily_RequestRegister.getSelectedItem().get_value();
                }
                if (ObjRequestTargetFeatures.IsMission) {
                    ObjRequestTarget_RequestTarget.IsMission = true;
                    if (trvMissionLocation_tbDaily_RequestRegister.get_selectedNode() != undefined) {
                        if (trvMissionLocation_tbDaily_RequestRegister.get_selectedNode().get_parentNode() != undefined)
                            ObjRequestTarget_RequestTarget.MissionLocationID = trvMissionLocation_tbDaily_RequestRegister.get_selectedNode().get_id();
                    }
                }
            }
            if (ObjRequestAttachment_RequestRegister != null && ObjRequestAttachment_RequestRegister.Daily != null)
                ObjRequestTarget_RequestTarget.AttachmentFile = ObjRequestAttachment_RequestRegister.Daily.RequestAttachmentSavedName;
            break;
        case 'OverTime':
            if (cmbRequestType_tbOverTime_RequestRegister.getSelectedItem() != undefined) {
                ObjRequestTarget_RequestTarget.PreCardID = cmbRequestType_tbOverTime_RequestRegister.getSelectedItem().get_id();
                ObjRequestTarget_RequestTarget.PreCardTitle = cmbRequestType_tbOverTime_RequestRegister.getSelectedItem().get_text();
            }
            ObjRequestTarget_RequestTarget.Description = document.getElementById('txtDescription_tbOverTime_RequestRegister').value;
            var ObjRequestTargetFeatures = GetRequestTargetFeatures_RequestRegister();
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    ObjRequestTarget_RequestTarget.FromDate = document.getElementById('pdpFromDate_tbOverTime_RequestRegister').value;
                    ObjRequestTarget_RequestTarget.ToDate = document.getElementById('pdpToDate_tbOverTime_RequestRegister').value;
                    break;
                case 'en-Us':
                    ObjRequestTarget_RequestTarget.FromDate = document.getElementById('gdpFromDate_tbOverTime_RequestRegister_picker').value;
                    ObjRequestTarget_RequestTarget.ToDate = document.getElementById('gdpToDate_tbOverTime_RequestRegister_picker').value;
                    break;
            }
            ObjRequestTarget_RequestTarget.FromTime = GetDuration_TimePicker_RequestRegister('TimeSelector_FromHour_tbOverTime_RequestRegister');
            ObjRequestTarget_RequestTarget.ToTime = GetDuration_TimePicker_RequestRegister('TimeSelector_ToHour_tbOverTime_RequestRegister');
            ObjRequestTarget_RequestTarget.Duration = GetDuration_TimePicker_RequestRegister('TimeSelector_Duration_tbOverTime_RequestRegister');
            break;
        case 'Imperative':
            if (cmbRequestType_tbImperative_RequestRegister.getSelectedItem() != undefined) {
                ObjRequestTarget_RequestTarget.PreCardID = cmbRequestType_tbImperative_RequestRegister.getSelectedItem().get_id();
                ObjRequestTarget_RequestTarget.PreCardTitle = cmbRequestType_tbImperative_RequestRegister.getSelectedItem().get_text();
            }
            ObjRequestTarget_RequestTarget.Year = document.getElementById('hfCurrentYear_RequestRegister').value;
            ObjRequestTarget_RequestTarget.Month = document.getElementById('hfCurrentMonth_RequestRegister').value;
            if (document.getElementById('txtValue_tbImperative_RequestRegister').value != undefined && document.getElementById('txtValue_tbImperative_RequestRegister').value != null && document.getElementById('txtValue_tbImperative_RequestRegister').value != '')
                ObjRequestTarget_RequestTarget.Value = document.getElementById('txtValue_tbImperative_RequestRegister').value;
            ObjRequestTarget_RequestTarget.Description = document.getElementById('txtDescription_tbImperative_RequestRegister').value;
            ObjRequestTarget_RequestTarget.StrPersonnelList = StrCollectiveImperativeList_RequestRegister;
            break;
    }
    UpdateRequest_RequestRegisterPage(CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.RequestCaller), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.RequestPersonnelCountState), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.SinglePersonnelID), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.CollectiveConditionsLoadState), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.CollectiveConditions), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.StrPersonnelList), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.RequestTarget), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.Year), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.Month), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.PageSize), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.ID), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.PreCardID), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.RequestDate), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.FromDate), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.ToDate), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.FromTime), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.ToTime), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.IsToTimeInNextDay.toString()), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.Duration), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.Description), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.IsSeakLeave.toString()), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.IllnessID), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.DoctorID), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.IsMission.toString()), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.MissionLocationID), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.Value), CharToKeyCode_RequestRegister(ObjRequestTarget_RequestTarget.AttachmentFile));
    DialogWaiting.Show();
}

function UpdateRequest_RequestRegisterPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_RequestRegister').value;
            Response[1] = document.getElementById('hfConnectionError_RequestRegister').value;
        }
        if (RetMessage[2] == 'success') {
            if (ObjRequestRegister != null)
                ObjRequestRegister.PageCount = Response[3];
            ClearList_RequestRegister(RetMessage[4]);
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2], false, document.getElementById('Mastertbl_RequestRegister').offsetWidth);
    }
}

function GetDialogRequestRegisterObjVal_RequestRegister() {
    ObjRequestRegister = parent.DialogRequestRegister.get_value();
}

function CustomizeRequestRegister_RequestRegister() {
    if (ObjRequestRegister != null)
        switch (ObjRequestRegister.Caller) {
            case 'NormalUser':
                document.getElementById("Container_PersonnelSearch_RequestRegister").removeChild(document.getElementById("PersonnelSearchBox_RequestRegister"));
                break;
            case 'Operator':
                break;
        }
}

function ClearList_RequestRegister(requestTarget) {
    var currentDate_RequestRegister = document.getElementById('hfCurrentDate_RequestRegister').value;
    var pageState = 'Change';
    switch (requestTarget) {
        case 'Hourly':
            document.getElementById('cmbRequestType_tbHourly_RequestRegister_Input').value = '';
            cmbRequestType_tbHourly_RequestRegister.unSelect();
            Clear_cmbIllnesses_tbHourly_RequestRegister();
            Clear_cmbDoctors_tbHourly_RequestRegister();
            Clear_cmbMissionLocation_tbHourly_RequestRegister();
            document.getElementById('txtDescription_tbHourly_RequestRegister').value = '';
            ResetTimepicker_RequestRegister(pageState, 'TimeSelector_FromHour_tbHourly_RequestRegister');
            ResetTimepicker_RequestRegister(pageState, 'TimeSelector_ToHour_tbHourly_RequestRegister');
            switch (parent.parent.SysLangID) {
                case 'en-US':
                    currentDate_RequestRegister = new Date(currentDate_RequestRegister);
                    gdpRequestDate_tbHourly_RequestRegister.setSelectedDate(currentDate_RequestRegister);
                    gCalRequestDate_tbHourly_RequestRegister.setSelectedDate(currentDate_RequestRegister);
                    break;
                case 'fa-IR':
                    document.getElementById('pdpRequestDate_tbHourly_RequestRegister').value = currentDate_RequestRegister;
                    break;
            }
            if (ObjRequestAttachment_RequestRegister != null)
                ObjRequestAttachment_RequestRegister.Hourly = null;
            document.getElementById('tdAttachmentName_tbHourly_RequestRegister').innerHTML = '';
            document.getElementById('chbToHourInNextDay_tbHourly_RequestRegister').checked = false;
            break;
        case 'Daily':
            document.getElementById('cmbRequestType_tbDaily_RequestRegister_Input').value = '';
            cmbRequestType_tbDaily_RequestRegister.unSelect();
            Clear_cmbIllnesses_tbDaily_RequestRegister();
            Clear_cmbDoctors_tbDaily_RequestRegister();
            Clear_cmbMissionLocation_tbDaily_RequestRegister();
            switch (parent.parent.SysLangID) {
                case 'en-US':
                    currentDate_RequestRegister = new Date(currentDate_RequestRegister);
                    gdpFromDate_tbDaily_RequestRegister.setSelectedDate(currentDate_RequestRegister);
                    gCalFromDate_tbDaily_RequestRegister.setSelectedDate(currentDate_RequestRegister);
                    gdpToDate_tbDaily_RequestRegister.setSelectedDate(currentDate_RequestRegister);
                    gCalToDate_tbDaily_RequestRegister.setSelectedDate(currentDate_RequestRegister);
                    break;
                case 'fa-IR':
                    document.getElementById('pdpFromDate_tbDaily_RequestRegister').value = currentDate_RequestRegister;
                    document.getElementById('pdpToDate_tbDaily_RequestRegister').value = currentDate_RequestRegister;
                    break;
            }
            document.getElementById('txtDescription_tbDaily_RequestRegister').value = '';
            if (ObjRequestAttachment_RequestRegister != null)
                ObjRequestAttachment_RequestRegister.Daily = null;
            document.getElementById('tdAttachmentName_tbDaily_RequestRegister').innerHTML = '';
            break;
        case 'OverTime':
            document.getElementById('cmbRequestType_tbOverTime_RequestRegister').value = '';
            cmbRequestType_tbOverTime_RequestRegister.unSelect();
            document.getElementById('txtDescription_tbOverTime_RequestRegister').value = '';
            ResetTimepicker_RequestRegister(pageState, 'TimeSelector_FromHour_tbOverTime_RequestRegister');
            ResetTimepicker_RequestRegister(pageState, 'TimeSelector_ToHour_tbOverTime_RequestRegister');
            ResetTimepicker_RequestRegister(pageState, 'TimeSelector_Duration_tbOverTime_RequestRegister');
            switch (parent.parent.SysLangID) {
                case 'en-US':
                    currentDate_RequestRegister = new Date(currentDate_RequestRegister);
                    gdpFromDate_tbOverTime_RequestRegister.setSelectedDate(currentDate_RequestRegister);
                    gCalFromDate_tbOverTime_RequestRegister.setSelectedDate(currentDate_RequestRegister);
                    gdpToDate_tbOverTime_RequestRegister.setSelectedDate(currentDate_RequestRegister);
                    gCalToDate_tbOverTime_RequestRegister.setSelectedDate(currentDate_RequestRegister);
                    break;
                case 'fa-IR':
                    document.getElementById('pdpFromDate_tbOverTime_RequestRegister').value = currentDate_RequestRegister;
                    document.getElementById('pdpToDate_tbOverTime_RequestRegister').value = currentDate_RequestRegister;
                    break;
            }
            break;
        case 'Imperative':
            StrCollectiveImperativeList_RequestRegister = '';
            document.getElementById('txtValue_tbImperative_RequestRegister').value = '';
            document.getElementById('txtDescription_tbImperative_RequestRegister').value = '';
            SetPageIndex_GridPersonnel_tbImperative_RequestRegister(0);
            break;
    }
}

function CharToKeyCode_RequestRegister(str) {
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

function GetDuration_TimePicker_RequestRegister(TimePicker) {
    if (cmbRequestType_tbHourly_RequestRegister.getSelectedItem() != undefined) {
        var ObjRequestTargetFeatures = cmbRequestType_tbHourly_RequestRegister.getSelectedItem().get_value();
        ObjRequestTargetFeatures = eval('(' + ObjRequestTargetFeatures + ')');
        if (ObjRequestTargetFeatures.IsTraffic && document.getElementById(TimePicker + '_txtHour').value == NullTime_RequestRegister)
            return;
    }
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

function ResetCalendars_RequestRegister() {
    var currentDate_RequestRegister = document.getElementById('hfCurrentDate_RequestRegister').value;
    switch (parent.parent.SysLangID) {
        case 'en-US':
            currentDate_RequestRegister = new Date(currentDate_RequestRegister);
            gdpRequestDate_tbHourly_RequestRegister.setSelectedDate(currentDate_RequestRegister);
            gCalRequestDate_tbHourly_RequestRegister.setSelectedDate(currentDate_RequestRegister);
            gdpFromDate_tbDaily_RequestRegister.setSelectedDate(currentDate_RequestRegister);
            gCalFromDate_tbDaily_RequestRegister.setSelectedDate(currentDate_RequestRegister);
            gdpToDate_tbDaily_RequestRegister.setSelectedDate(currentDate_RequestRegister);
            gCalToDate_tbDaily_RequestRegister.setSelectedDate(currentDate_RequestRegister);
            gdpFromDate_tbOverTime_RequestRegister.setSelectedDate(currentDate_RequestRegister);
            gCalFromDate_tbOverTime_RequestRegister.setSelectedDate(currentDate_RequestRegister);
            gdpToDate_tbOverTime_RequestRegister.setSelectedDate(currentDate_RequestRegister);
            gCalToDate_tbOverTime_RequestRegister.setSelectedDate(currentDate_RequestRegister);
            break;
        case 'fa-IR':
            document.getElementById('pdpRequestDate_tbHourly_RequestRegister').value = currentDate_RequestRegister;
            document.getElementById('pdpFromDate_tbDaily_RequestRegister').value = currentDate_RequestRegister;
            document.getElementById('pdpToDate_tbDaily_RequestRegister').value = currentDate_RequestRegister;
            document.getElementById('pdpFromDate_tbOverTime_RequestRegister').value = currentDate_RequestRegister;
            document.getElementById('pdpToDate_tbOverTime_RequestRegister').value = currentDate_RequestRegister;
            break;
    }
}

function ResetTimepickers_RequestRegister(pageState) {
    ResetTimepicker_RequestRegister(pageState, 'TimeSelector_FromHour_tbHourly_RequestRegister');
    ResetTimepicker_RequestRegister(pageState, 'TimeSelector_ToHour_tbHourly_RequestRegister');
    ResetTimepicker_RequestRegister(pageState, 'TimeSelector_FromHour_tbOverTime_RequestRegister');
    ResetTimepicker_RequestRegister(pageState, 'TimeSelector_ToHour_tbOverTime_RequestRegister');
    ResetTimepicker_RequestRegister(pageState, 'TimeSelector_Duration_tbOverTime_RequestRegister');
}

function ResetTimepicker_RequestRegister(pageState, TimePicker) {
    var strTime = zeroTime;
    switch (pageState) {
        case 'Load':
            break;
        case 'Change':
            if (cmbRequestType_tbHourly_RequestRegister.getSelectedItem() != undefined) {
                var ObjRequestTargetFeatures = cmbRequestType_tbHourly_RequestRegister.getSelectedItem().get_value();
                ObjRequestTargetFeatures = eval('(' + ObjRequestTargetFeatures + ')');
                if (ObjRequestTargetFeatures.IsTraffic)
                    strTime = NullTime_RequestRegister;
            }
            break;
    }
    document.getElementById(TimePicker + "_txtHour").value = strTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}

function GetCurrentRequestTarget_RequestRegister() {
    return TabStripRequestRegister.getSelectedTab().get_value();
}

function GetVisibilityProps_RequestRegister(state) {
    var visibility = null;
    if (state)
        visibility = 'visible';
    else
        visibility = 'hidden';
    return visibility;
}

function ChangeHideElementsState_RequestRegister(isSickLeave, isMission, isSwitchAll) {
    var RequestTarget = !isSwitchAll ? GetCurrentRequestTarget_RequestRegister() : 'SwitchAll';
    var IsSickLeave = GetVisibilityProps_RequestRegister(isSickLeave);
    var IsMission = GetVisibilityProps_RequestRegister(isMission);
    switch (RequestTarget) {
        case 'Hourly':
            document.getElementById('lblIllnesses_tbHourly_RequestRegister').style.visibility = IsSickLeave;
            document.getElementById('Container_cmbIllnesses_tbHourly_RequestRegister').style.visibility = IsSickLeave;
            document.getElementById('lblDoctors_tbHourly_RequestRegister').style.visibility = IsSickLeave;
            document.getElementById('Container_cmbDoctors_tbHourly_RequestRegister').style.visibility = IsSickLeave;
            if (!isSickLeave) {
                Clear_cmbIllnesses_tbHourly_RequestRegister();
                Clear_cmbDoctors_tbHourly_RequestRegister();
            }
            document.getElementById('lblMissionLocation_tbHourly_RequestRegister').style.visibility = IsMission;
            document.getElementById('Container_cmbMissionLocation_tbHourly_RequestRegister').style.visibility = IsMission;
            if (!isMission)
                Clear_cmbMissionLocation_tbHourly_RequestRegister();
            break;
        case 'Daily':
            document.getElementById('lblIllnesses_tbDaily_RequestRegister').style.visibility = IsSickLeave;
            document.getElementById('Container_cmbIllnesses_tbDaily_RequestRegister').style.visibility = IsSickLeave;
            document.getElementById('lblDoctors_tbDaily_RequestRegister').style.visibility = IsSickLeave;
            document.getElementById('Container_cmbDoctors_tbDaily_RequestRegister').style.visibility = IsSickLeave;
            if (!isSickLeave) {
                Clear_cmbIllnesses_tbDaily_RequestRegister();
                Clear_cmbDoctors_tbDaily_RequestRegister();
            }
            document.getElementById('lblMissionLocation_tbDaily_RequestRegister').style.visibility = IsMission;
            document.getElementById('Container_cmbMissionLocation_tbDaily_RequestRegister').style.visibility = IsMission;
            if (!isMission)
                Clear_cmbMissionLocation_tbDaily_RequestRegister();
            break;
        case 'OverTime':
            document.getElementById('lblFromDate_tbOverTime_RequestRegister').innerHTML = document.getElementById('hfFromDate_tbOverTime_RequestRegister').value;
            break;
        case 'SwitchAll':
            document.getElementById('lblIllnesses_tbHourly_RequestRegister').style.visibility = IsSickLeave;
            document.getElementById('Container_cmbIllnesses_tbHourly_RequestRegister').style.visibility = IsSickLeave;
            document.getElementById('lblDoctors_tbHourly_RequestRegister').style.visibility = IsSickLeave;
            document.getElementById('Container_cmbDoctors_tbHourly_RequestRegister').style.visibility = IsSickLeave;
            document.getElementById('lblMissionLocation_tbHourly_RequestRegister').style.visibility = IsMission;
            document.getElementById('Container_cmbMissionLocation_tbHourly_RequestRegister').style.visibility = IsMission;
            document.getElementById('lblIllnesses_tbDaily_RequestRegister').style.visibility = IsSickLeave;
            document.getElementById('Container_cmbIllnesses_tbDaily_RequestRegister').style.visibility = IsSickLeave;
            document.getElementById('lblDoctors_tbDaily_RequestRegister').style.visibility = IsSickLeave;
            document.getElementById('Container_cmbDoctors_tbDaily_RequestRegister').style.visibility = IsSickLeave;
            document.getElementById('lblMissionLocation_tbDaily_RequestRegister').style.visibility = IsMission;
            document.getElementById('Container_cmbMissionLocation_tbDaily_RequestRegister').style.visibility = IsMission;
            break;
    }
    if (!isSwitchAll) {
        cmbMissionLocation_tbHourly_RequestRegister.collapse();
        cmbMissionLocation_tbDaily_RequestRegister.collapse();
    }
}

function Clear_cmbIllnesses_tbHourly_RequestRegister() {
    if (cmbIllnesses_tbHourly_RequestRegister.get_itemCount() > 0)
        cmbIllnesses_tbHourly_RequestRegister.selectItemByIndex(0);
}

function Clear_cmbDoctors_tbHourly_RequestRegister() {
    if (cmbDoctors_tbHourly_RequestRegister.get_itemCount() > 0)
        cmbDoctors_tbHourly_RequestRegister.selectItemByIndex(0);
}

function Clear_cmbIllnesses_tbDaily_RequestRegister() {
    if (cmbIllnesses_tbDaily_RequestRegister.get_itemCount() > 0)
        cmbIllnesses_tbDaily_RequestRegister.selectItemByIndex(0);
}

function Clear_cmbDoctors_tbDaily_RequestRegister() {
    if (cmbDoctors_tbDaily_RequestRegister.get_itemCount() > 0)
        cmbDoctors_tbDaily_RequestRegister.selectItemByIndex(0);
}

function Clear_cmbMissionLocation_tbHourly_RequestRegister() {
    if (trvMissionLocation_tbHourly_RequestRegister.get_nodes().get_length() > 0) {
        var NoLocationNode = trvMissionLocation_tbHourly_RequestRegister.get_nodes().getNode(0);
        document.getElementById('cmbMissionLocation_tbHourly_RequestRegister_TextBox').innerHTML = NoLocationNode.get_text();
        trvMissionLocation_tbDaily_RequestRegister.selectNodeById(NoLocationNode.get_id());
    }
}

function Clear_cmbMissionLocation_tbDaily_RequestRegister() {
    if (trvMissionLocation_tbDaily_RequestRegister.get_nodes().get_length() > 0) {
        var NoLocationNode = trvMissionLocation_tbDaily_RequestRegister.get_nodes().getNode(0);
        document.getElementById('cmbMissionLocation_tbDaily_RequestRegister_TextBox').innerHTML = NoLocationNode.get_text();
        trvMissionLocation_tbDaily_RequestRegister.selectNodeById(NoLocationNode.get_id());
    }
}

function GetRequestTargetFeatures_RequestRegister() {
    var RequestTarget = GetCurrentRequestTarget_RequestRegister();
    var ObjRequestTargetFeatures = null;
    switch (RequestTarget) {
        case 'Hourly':
            if (cmbRequestType_tbHourly_RequestRegister.getSelectedItem() != undefined && cmbRequestType_tbHourly_RequestRegister.getSelectedItem() != null)
                ObjRequestTargetFeatures = cmbRequestType_tbHourly_RequestRegister.getSelectedItem().get_value();
            break;
        case 'Daily':
            if (cmbRequestType_tbDaily_RequestRegister.getSelectedItem() != undefined && cmbRequestType_tbDaily_RequestRegister.getSelectedItem() != null)
                ObjRequestTargetFeatures = cmbRequestType_tbDaily_RequestRegister.getSelectedItem().get_value();
            break;
        case 'OverTime':
            if (cmbRequestType_tbOverTime_RequestRegister.getSelectedItem() != undefined && cmbRequestType_tbOverTime_RequestRegister.getSelectedItem() != null)
                ObjRequestTargetFeatures = cmbRequestType_tbOverTime_RequestRegister.getSelectedItem().get_value();
            break;
    }
    ObjRequestTargetFeatures = eval('(' + ObjRequestTargetFeatures + ')');
    return ObjRequestTargetFeatures;
}

function TabStripRequestRegister_onTabSelect(sender, e) {
    CollapseControls_RequestRegister();
}

function CallBack_cmbRequestType_tbHourly_RequestRegister_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_RequestRegister_onClick() {
    Refresh_cmbPersonnel_RequestRegister();
}

function Refresh_cmbPersonnel_RequestRegister() {
    ChangeLoadState_cmbPersonnel_RequestRegister('Normal');
    RequestPersonnelCountState_RequestRegister = 'Single';
    document.getElementById('headerPersonnelCount_RequestRegister').innerHTML = document.getElementById('hfSelectedPersonnelCount_RequestRegister').value + '0';
    StrUnCollectivePersonnelList_CollectiveTraffic = '';
}

function tlbItemFirst_TlbPaging_PersonnelSearch_RequestRegister_onClick() {
    SetPageIndex_cmbPersonnel_RequestRegister(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_RequestRegister_onClick() {
    if (CurrentPageIndex_cmbPersonnel_RequestRegister != 0) {
        CurrentPageIndex_cmbPersonnel_RequestRegister = CurrentPageIndex_cmbPersonnel_RequestRegister - 1;
        SetPageIndex_cmbPersonnel_RequestRegister(CurrentPageIndex_cmbPersonnel_RequestRegister);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_RequestRegister_onClick() {
    if (CurrentPageIndex_cmbPersonnel_RequestRegister < parseInt(document.getElementById('hfPersonnelPageCount_RequestRegister').value) - 1) {
        CurrentPageIndex_cmbPersonnel_RequestRegister = CurrentPageIndex_cmbPersonnel_RequestRegister + 1;
        SetPageIndex_cmbPersonnel_RequestRegister(CurrentPageIndex_cmbPersonnel_RequestRegister);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_RequestRegister_onClick() {
    SetPageIndex_cmbPersonnel_RequestRegister(parseInt(document.getElementById('hfPersonnelPageCount_RequestRegister').value) - 1);
}

function ChangeLoadState_cmbPersonnel_RequestRegister(state) {
    LoadState_cmbPersonnel_RequestRegister = state;
    SetPageIndex_cmbPersonnel_RequestRegister(0);
}

function SetPageIndex_cmbPersonnel_RequestRegister(pageIndex) {
    CurrentPageIndex_cmbPersonnel_RequestRegister = pageIndex;
    Fill_cmbPersonnel_RequestRegister(pageIndex);
}

function Fill_cmbPersonnel_RequestRegister(pageIndex) {
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_RequestRegister').value);
    var SearchTermConditions = '';
    switch (LoadState_cmbPersonnel_RequestRegister) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm_cmbPersonnel_RequestRegister = SearchTermConditions = document.getElementById('txtPersonnelSearch_RequestRegister').value;
            break;
        case 'AdvancedSearch':
            SearchTermConditions = AdvancedSearchTerm_cmbPersonnel_RequestRegister;
            break;
    }
    CallBack_cmbPersonnel_RequestRegister.callback(CharToKeyCode_RequestRegister(LoadState_cmbPersonnel_RequestRegister), CharToKeyCode_RequestRegister(pageSize.toString()), CharToKeyCode_RequestRegister(pageIndex.toString()), CharToKeyCode_RequestRegister(SearchTermConditions));
}

function tlbItemCollectiveTrrafic_TlbPaging_PersonnelSearch_RequestRegister_onClick() {
    RequestPersonnelCountState_RequestRegister = 'Collective';
    ShowDialogCollectiveTraffic();
}

function ShowDialogCollectiveTraffic() {
    var ObjDialogCollectiveTraffic = new Object();
    ObjDialogCollectiveTraffic.CollectiveState = LoadState_cmbPersonnel_RequestRegister;
    switch (LoadState_cmbPersonnel_RequestRegister) {
        case 'Normal':
            CollectiveConditions = '';
            break;
        case 'Search':
            CollectiveConditions = SearchTerm_cmbPersonnel_RequestRegister;
            break;
        case 'AdvancedSearch':
            CollectiveConditions = AdvancedSearchTerm_cmbPersonnel_RequestRegister;
            break;
    }
    ObjDialogCollectiveTraffic.CollectiveConditions = CollectiveConditions;
    SetPersonnelCount_GridPersonnel_CollectiveTraffic('All');
    DialogCollectiveTraffic.set_value(ObjDialogCollectiveTraffic);
    DialogCollectiveTraffic.Show();
    CollapseControls_RequestRegister();
}

function SetPersonnelCount_GridPersonnel_CollectiveTraffic(state) {
    switch (state) {
        case 'All':
            var unCollectivePersonnelCount = 0;
            PersonnelCount_CollectiveTraffic = document.getElementById('hfPersonnelCount_RequestRegister').value != null && document.getElementById('hfPersonnelCount_RequestRegister').value != undefined && document.getElementById('hfPersonnelCount_RequestRegister').value != '' ? parseInt(document.getElementById('hfPersonnelCount_RequestRegister').value) : 0;
            if (StrUnCollectivePersonnelList_CollectiveTraffic != '')
                unCollectivePersonnelCount = StrUnCollectivePersonnelList_CollectiveTraffic.split('#').length - 2;
            PersonnelCount_CollectiveTraffic -= unCollectivePersonnelCount;
            break;
        case 'None':
            PersonnelCount_CollectiveTraffic = 0;
            break;
        case 'Increase':
            PersonnelCount_CollectiveTraffic = PersonnelCount_CollectiveTraffic + 1;
            break;
        case 'Decrease':
            PersonnelCount_CollectiveTraffic = PersonnelCount_CollectiveTraffic - 1;
            break;
    }
    document.getElementById('PersonnelCount_GridPersonnel_CollectiveTraffic').innerHTML = document.getElementById('hfPersonnelCountTitle_GridPersonnel_CollectiveTraffic').value + PersonnelCount_CollectiveTraffic;
}

function cmbPersonnel_RequestRegister_onExpand(sender, e) {
    if (cmbPersonnel_RequestRegister.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_RequestRegister == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_RequestRegister = true;
        SetPageIndex_cmbPersonnel_RequestRegister(0);
    }
}

function CallBack_cmbPersonnel_RequestRegister_onBeforeCallback(sender, e) {
    cmbPersonnel_RequestRegister.dispose();
}

function CallBack_cmbPersonnel_RequestRegister_onCallBackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_RequestRegister').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_RequestRegister').value;
    document.getElementById('clmnBarCode_cmbPersonnel_RequestRegister').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_RequestRegister').value;
    document.getElementById('clmnCardNum_cmbPersonnel_RequestRegister').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_RequestRegister').value;

    var error = document.getElementById('ErrorHiddenField_Personnel_RequestRegister').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_RequestRegister_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_RequestRegister_DropImage').mousedown();
        else
            cmbPersonnel_RequestRegister.expand();
        ChangeControlDirection_RequestRegister('cmbPersonnel_RequestRegister_DropDown');
        var personnelCount = document.getElementById('hfPersonnelCount_RequestRegister').value;
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_RequestRegister_DropDown').style.display = 'none';
    }
}

function CallBack_cmbPersonnel_RequestRegister_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function tlbItemSearch_TlbSearchPersonnel_RequestRegister_onClick() {
    LoadState_cmbPersonnel_RequestRegister = 'Search';
    CurrentPageIndex_cmbPersonnel_RequestRegister = 0;
    StrUnCollectivePersonnelList_CollectiveTraffic = StrCollectiveImperativeList_RequestRegister = '';
    SetPageIndex_cmbPersonnel_RequestRegister(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_RequestRegister_onClick() {
    LoadState_cmbPersonnel_RequestRegister = 'AdvancedSearch';
    CurrentPageIndex_cmbPersonnel_RequestRegister = 0;
    StrUnCollectivePersonnelList_CollectiveTraffic = StrCollectiveImperativeList_RequestRegister = '';
    ShowDialogPersonnelSearch('RequestRegister');
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.parent.DialogPersonnelSearch.Show();
    CollapseControls_RequestRegister();
}

function RequestRegister_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_cmbPersonnel_RequestRegister = SearchTerm;
    SetPageIndex_cmbPersonnel_RequestRegister(0);
}

function tlbItemSave_TlbCollectiveTraffic_onClick() {
    UpdateRequestPersonnelCount_RequestRegister();
    CloseDialogCollectiveTraffic();
}

function CloseDialogCollectiveTraffic() {
    GridPersonnel_CollectiveTraffic.beginUpdate();
    GridPersonnel_CollectiveTraffic.get_table().clearData();
    GridPersonnel_CollectiveTraffic.endUpdate();
    PersonnelCount_CollectiveTraffic = 0;
    document.getElementById('PersonnelCount_GridPersonnel_CollectiveTraffic').innerHTML = document.getElementById('hfPersonnelCountTitle_GridPersonnel_CollectiveTraffic').value + PersonnelCount_CollectiveTraffic;
    document.getElementById('footer_GridPersonnel_CollectiveTraffic').innerHTML = document.getElementById('hffooter_GridPersonnel_CollectiveTraffic').value;
    DialogCollectiveTraffic.Close();
}

function UpdateRequestPersonnelCount_RequestRegister() {
    var personnelCount = parseInt(document.getElementById('hfPersonnelCount_Personnel_CollectiveTraffic').value);
    var unCollectivePersonnelCount = 0;
    if (StrUnCollectivePersonnelList_CollectiveTraffic != '')
        unCollectivePersonnelCount = StrUnCollectivePersonnelList_CollectiveTraffic.split('#').length - 2;
    personnelCount -= unCollectivePersonnelCount;
    document.getElementById('headerPersonnelCount_RequestRegister').innerHTML = document.getElementById('hfSelectedPersonnelCount_RequestRegister').value + personnelCount.toString();
}

function tlbItemExit_TlbCollectiveTraffic_onClick() {
    ShowDialogConfirm('CollectiveTraffic');
}

function CollectiveTraffic_onClose() {
    RequestPersonnelCountState_RequestRegister = 'Single';
    DialogCollectiveTraffic.Close();
}

function GridPersonnel_CollectiveTraffic_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridPersonnel_CollectiveTraffic').innerHTML = '';
}

function GridPersonnel_CollectiveTraffic_onItemCheckChange(sender, e) {
    ChangeStrList_CollectivePersonnel('Personnel', e.get_item(), 'ID', StrUnCollectivePersonnelList_CollectiveTraffic);
}

function ChangeStrList_CollectivePersonnel(state, gridItem, identifierName, StrList) {
    var checked = gridItem.getMember('Select').get_value() ? true : false;
    var CountState_CollectiveTraffic = null;
    var separator = '#';
    var identifier = gridItem.getMember(identifierName).get_text() + separator;
    if (StrList == '')
        StrList = separator;
    if (checked) {
        switch (state) {
            case 'peronnel':
                if (StrList.indexOf(identifier) < 0) {
                    StrList += identifier;
                    CountState_CollectiveTraffic = 'Decrease';
                }
                break;
            case 'Imperative':
                if (StrList.indexOf(identifier) >= 0) {
                    StrList = StrCollectiveImperativeList_RequestRegister.replace(separator + identifier, separator);
                    CountState_CollectiveTraffic = 'Decrease';
                }
                break;
        }
    }
    else {
        switch (state) {
            case 'peronnel':
                if (StrList.indexOf(identifier) >= 0) {
                    StrList = StrUnCollectivePersonnelList_CollectiveTraffic.replace(separator + identifier, separator);
                    CountState_CollectiveTraffic = 'Increase';
                }
                break;
            case 'Imperative':
                if (!gridItem.getMember('IsLockedImperative').get_value()) {
                    if (StrList.indexOf(identifier) < 0) {
                        StrList += identifier;
                        CountState_CollectiveTraffic = 'Increase';
                    }
                }
                else {
                    var separator = '';
                    GridPersonnel_tbImperative_RequestRegister.beginUpdate();
                    gridItem.setValue(2, false, false);
                    GridPersonnel_tbImperative_RequestRegister.endUpdate();
                }
                break;
        }
    }
    switch (state) {
        case 'Personnel':
            StrUnCollectivePersonnelList_CollectiveTraffic = StrList;
            SetPersonnelCount_GridPersonnel_CollectiveTraffic(CountState_CollectiveTraffic);
            break;
        case 'Imperative':
            StrCollectiveImperativeList_RequestRegister = StrList;
            break;
    }
}

function CallBack_GridPersonnel_CollectiveTraffic_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Personnel_CollectiveTraffic').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridPersonnel_CollectiveTraffic(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2], false, document.getElementById('Mastertbl_RegisteredRequestsForm').offsetWidth);
    }
    else {
        Changefooter_GridPersonnel_CollectiveTraffic();
        UpdateGridPersonnel_CollectiveTraffic();
    }
}

function UpdateGridPersonnel_CollectiveTraffic() {
    var separator = '#';
    if (StrUnCollectivePersonnelList_CollectiveTraffic != null) {
        for (var i = 0; i < GridPersonnel_CollectiveTraffic.get_table().getRowCount() ; i++) {
            personnelItem = GridPersonnel_CollectiveTraffic.get_table().getRow(i);
            var personnelID = personnelItem.getMember('ID').get_text();
            GridPersonnel_CollectiveTraffic.beginUpdate();
            if (StrUnCollectivePersonnelList_CollectiveTraffic.indexOf(separator + personnelID + separator) >= 0)
                personnelItem.setValue(1, false, false);
            else
                personnelItem.setValue(1, true, false);
            GridPersonnel_CollectiveTraffic.endUpdate();
        }
    }
}

function Changefooter_GridPersonnel_CollectiveTraffic() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridPersonnel_CollectiveTraffic').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfPersonnelPageCount_Personnel_CollectiveTraffic').value) > 0 ? CurrentPageIndex_GridPersonnel_CollectiveTraffic + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfPersonnelPageCount_Personnel_CollectiveTraffic').value;
        if ((i == 1 || i == 3) && GridPersonnel_CollectiveTraffic.get_table().getRowCount() == 0)
            footerValCol[i] = 0;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridPersonnel_CollectiveTraffic').innerHTML = retfooterVal;
}


function CallBack_GridPersonnel_CollectiveTraffic_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function tlbItemRefresh_TlbPaging_GridPersonnel_CollectiveTraffic_onClick() {
    SetPageIndex_GridPersonnel_CollectiveTraffic(0);
}

function tlbItemFirst_TlbPaging_GridPersonnel_CollectiveTraffic_onClick() {
    SetPageIndex_GridPersonnel_CollectiveTraffic(0);
}

function tlbItemBefore_TlbPaging_GridPersonnel_CollectiveTraffic_onClick() {
    if (CurrentPageIndex_GridPersonnel_CollectiveTraffic != 0) {
        CurrentPageIndex_GridPersonnel_CollectiveTraffic = CurrentPageIndex_GridPersonnel_CollectiveTraffic - 1;
        SetPageIndex_GridPersonnel_CollectiveTraffic(CurrentPageIndex_GridPersonnel_CollectiveTraffic);
    }
}

function tlbItemNext_TlbPaging_GridPersonnel_CollectiveTraffic_onClick() {
    if (CurrentPageIndex_GridPersonnel_CollectiveTraffic < parseInt(document.getElementById('hfPersonnelPageCount_Personnel_CollectiveTraffic').value) - 1) {
        CurrentPageIndex_GridPersonnel_CollectiveTraffic = CurrentPageIndex_GridPersonnel_CollectiveTraffic + 1;
        SetPageIndex_GridPersonnel_CollectiveTraffic(CurrentPageIndex_GridPersonnel_CollectiveTraffic);
    }
}

function tlbItemLast_TlbPaging_GridPersonnel_CollectiveTraffic_onClick() {
    SetPageIndex_GridPersonnel_CollectiveTraffic(parseInt(document.getElementById('hfPersonnelPageCount_Personnel_CollectiveTraffic').value) - 1);
}

function SetPageIndex_GridPersonnel_CollectiveTraffic(pageIndex) {
    CurrentPageIndex_GridPersonnel_CollectiveTraffic = pageIndex;
    Fill_GridPersonnel_CollectiveTraffic(pageIndex);
}

function Fill_GridPersonnel_CollectiveTraffic(pageIndex) {
    document.getElementById('loadingPanel_GridPersonnel_CollectiveTraffic').innerHTML = document.getElementById('hfloadingPanel_GridPersonnel_CollectiveTraffic').value;
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_Personnel_CollectiveTraffic').value);
    var ObjCollectiveTraffic = DialogCollectiveTraffic.get_value();
    var CollectiveState = ObjCollectiveTraffic.CollectiveState;
    var CollectiveConditions = ObjCollectiveTraffic.CollectiveConditions;
    CallBack_GridPersonnel_CollectiveTraffic.callback(CharToKeyCode_RequestRegister(CollectiveState), CharToKeyCode_RequestRegister(pageSize.toString()), CharToKeyCode_RequestRegister(pageIndex.toString()), CharToKeyCode_RequestRegister(CollectiveConditions));
}

function DialogCollectiveTraffic_OnShow(sender, e) {
    Init_DialogCollectiveTraffic();
    SetPageIndex_GridPersonnel_CollectiveTraffic(0);
}

function Init_DialogCollectiveTraffic() {
    document.getElementById('Title_DialogCollectiveTraffic').innerHTML = document.getElementById('hfTitle_DialogCollectiveTraffic').value;
    var CurrentLangID = parent.parent.CurrentLangID;
    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogCollectiveTraffic_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogCollectiveTraffic_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogCollectiveTraffic_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogCollectiveTraffic_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogCollectiveTraffic').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogCollectiveTraffic').align = 'right';

    ChangeControlDirection_RequestRegister('tbl_DialogCollectiveTrafficheader');
    ChangeControlDirection_RequestRegister('tbl_DialogCollectiveTrafficfooter');
}

function ChangeControlDirection_RequestRegister(ctrl) {
    var direction = null;
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    if (ctrl == 'All') {
        if (document.getElementById('cmbPersonnel_RequestRegister_DropDownContent') != null)
            document.getElementById('cmbPersonnel_RequestRegister_DropDownContent').dir = direction;
        document.getElementById('Mastertbl_RequestRegister').dir = document.getElementById('cmbIllnesses_tbHourly_RequestRegister_DropDownContent').dir = document.getElementById('cmbDoctors_tbHourly_RequestRegister_DropDownContent').dir = document.getElementById('cmbMissionLocation_tbHourly_RequestRegister_DropDownContent').dir = document.getElementById('cmbMissionLocation_tbHourly_RequestRegister_DropDownContent').dir = document.getElementById('cmbRequestType_tbHourly_RequestRegister_DropDownContent').dir = document.getElementById('cmbIllnesses_tbDaily_RequestRegister_DropDownContent').dir = document.getElementById('cmbDoctors_tbDaily_RequestRegister_DropDownContent').dir = document.getElementById('cmbMissionLocation_tbDaily_RequestRegister_DropDownContent').dir = document.getElementById('cmbRequestType_tbDaily_RequestRegister_DropDownContent').dir = document.getElementById('cmbRequestType_tbOverTime_RequestRegister_DropDownContent').dir = document.getElementById('cmbRequestType_tbImperative_RequestRegister_DropDownContent').dir = document.getElementById('cmbYear_tbImperative_RequestRegister_DropDownContent').dir = document.getElementById('cmbMonth_tbImperative_RequestRegister_DropDownContent').dir = document.getElementById('tblConfirm_DialogConfirm').dir = document.getElementById('Mastertbl_CollectiveTraffic').dir = document.getElementById('Mastertbl_MonthlyOperationSummary_RequestRegister').dir = direction;
    }
    else
        document.getElementById(ctrl).style.direction = direction;
}

function CollapseControls_RequestRegister() {
    var currentUserState = parent.DialogRequestRegister.get_value();
    switch (currentUserState.Caller) {
        case 'Operator':
            cmbPersonnel_RequestRegister.collapse();
            break;
        case 'NormalUser':
            break;
    }
    cmbIllnesses_tbHourly_RequestRegister.collapse();
    cmbDoctors_tbHourly_RequestRegister.collapse();
    cmbMissionLocation_tbHourly_RequestRegister.collapse();
    cmbRequestType_tbHourly_RequestRegister.collapse();
    cmbIllnesses_tbDaily_RequestRegister.collapse();
    cmbDoctors_tbDaily_RequestRegister.collapse();
    cmbMissionLocation_tbDaily_RequestRegister.collapse();
    cmbRequestType_tbDaily_RequestRegister.collapse();
    cmbRequestType_tbOverTime_RequestRegister.collapse();
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpRequestDate_tbHourly_RequestRegister');
}

function tlbItemFormReconstruction_TlbHourly_onClick() {
    ReconstrucForm_RequestRegister();
}

function tlbItemFormReconstruction_TlbDaily_onClick() {
    ReconstrucForm_RequestRegister();
}

function tlbItemFormReconstruction_TlbOverTime_onClick() {
    ReconstrucForm_RequestRegister();
}

function ReconstrucForm_RequestRegister() {
    var ObjDialogRequestRegister = parent.DialogRequestRegister.get_value();
    var caller = ObjDialogRequestRegister.Caller;
    RequestRegister_onClose();
    parent.parent.document.getElementById('DialogRegisteredRequests_IFrame').contentWindow.ShowDialogRequestRegister(caller);
}

function TimeSelector_MinistryDuration_tbOverTime_RequestRegister_onChange(partID) {
    var id = 'TimeSelector_MinistryDuration_tbOverTime_RequestRegister_' + partID;
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

function cmbRequestType_tbOverTime_RequestRegister_onChange(sender, e) {
    if (cmbRequestType_tbOverTime_RequestRegister.getSelectedItem() != undefined) {
        ChangeHideElementsState_RequestRegister(null, null, false);
    }
}

function tlbItemEndorsement_TlbImperative_onClick() {
    UpdateRequest_RequestRegister();
}

function tlbItemFormReconstruction_TlbImperative_onClick() {
    ReconstrucForm_RequestRegister();
}

function tlbItemExit_TlbImperative_onClick() {
    ShowDialogConfirm('RequestRegister');
}

function cmbRequestType_tbImperative_RequestRegister_onExpand(sender, e) {
    if (cmbRequestType_tbImperative_RequestRegister.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRequestType_tbImperative_RequestRegister == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRequestType_tbImperative_RequestRegister = true;
        CallBack_cmbRequestType_tbImperative_RequestRegister.callback();
    }
}

function cmbRequestType_tbImperative_RequestRegister_onCollapse(sender, e) {
    if (cmbRequestType_tbImperative_RequestRegister.getSelectedItem() == undefined)
        document.getElementById('cmbRequestType_tbImperative_RequestRegister_TextBox').innerHTML = document.getElementById('hfcmbAlarm_RequestRegister').value;
}

function cmbRequestType_tbImperative_RequestRegister_onBeforeCallback(sender, e) {
    cmbRequestType_tbImperative_RequestRegister.dispose();
}

function cmbRequestType_tbImperative_RequestRegister_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RequestTypes_tbImperative_RequestRegister').value;
    if (error == "") {
        document.getElementById('cmbRequestType_tbImperative_RequestRegister_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            cmbRequestType_tbImperative_RequestRegister.expand();
        $('#cmbRequestType_tbImperative_RequestRegister_DropImage').mousedown();
        ChangeControlDirection_RequestRegister('cmbRequestType_tbImperative_RequestRegister_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbRequestType_tbImperative_RequestRegister_DropDown').style.display = 'none';
    }
}

function cmbRequestType_tbImperative_RequestRegister_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function cmbYear_tbImperative_RequestRegister_onChange(sender, e) {
    document.getElementById('hfCurrentYear_RequestRegister').value = cmbYear_tbImperative_RequestRegister.getSelectedItem().get_value();
}

function cmbMonth_tbImperative_RequestRegister_onChange(sender, e) {
    document.getElementById('hfCurrentMonth_RequestRegister').value = cmbMonth_tbImperative_RequestRegister.getSelectedItem().get_value();
}

function GridPersonnel_tbImperative_RequestRegister_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridPersonnel_tbImperative_RequestRegister').innerHTML = '';
}

function CallBack_GridPersonnel_tbImperative_RequestRegister_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_tbImperative_RequestRegister').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridPersonnel_tbImperative_RequestRegister(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else {
        Changefooter_GridPersonnel_tbImperative_RequestRegister();
        document.getElementById('chbAllInThisPage_tbImperative_RequestRegister').checked = false;
        UpdateGridPersonnel_tbImperative_RequestRegister(null);
    }
}

function CallBack_GridPersonnel_tbImperative_RequestRegister_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function tlbItemRefresh_TlbPaging_GridPersonnel_tbImperative_RequestRegister_onClick() {
    ImperativeRequestLoadState_RequestRegister = 'Normal';
    SetPageIndex_GridPersonnel_tbImperative_RequestRegister(0);
}

function tlbItemFirst_TlbPaging_GridPersonnel_tbImperative_RequestRegister_onClick() {
    SetPageIndex_GridPersonnel_tbImperative_RequestRegister(0);
}

function tlbItemBefore_TlbPaging_GridPersonnel_tbImperative_RequestRegister_onClick() {
    if (CurrentPageIndex_GridPersonnel_tbImperative_RequestRegister != 0) {
        CurrentPageIndex_GridPersonnel_tbImperative_RequestRegister = CurrentPageIndex_GridPersonnel_tbImperative_RequestRegister - 1;
        SetPageIndex_GridPersonnel_tbImperative_RequestRegister(CurrentPageIndex_GridPersonnel_tbImperative_RequestRegister);
    }
}

function tlbItemNext_TlbPaging_GridPersonnel_tbImperative_RequestRegister_onClick() {
    if (CurrentPageIndex_GridPersonnel_tbImperative_RequestRegister < parseInt(document.getElementById('hfImperativePageCount_tbImperative_RequestRegister').value) - 1) {
        CurrentPageIndex_GridPersonnel_tbImperative_RequestRegister = CurrentPageIndex_GridPersonnel_tbImperative_RequestRegister + 1;
        SetPageIndex_GridPersonnel_tbImperative_RequestRegister(CurrentPageIndex_GridPersonnel_tbImperative_RequestRegister);
    }
}

function tlbItemLast_TlbPaging_GridPersonnel_tbImperative_RequestRegister_onClick() {
    SetPageIndex_GridPersonnel_tbImperative_RequestRegister(parseInt(document.getElementById('hfImperativePageCount_tbImperative_RequestRegister').value) - 1);
}

function SetPageIndex_GridPersonnel_tbImperative_RequestRegister(pageIndex) {
    CurrentPageIndex_GridPersonnel_tbImperative_RequestRegister = pageIndex;
    Fill_GridPersonnel_tbImperative_RequestRegister(pageIndex);
}

function Fill_GridPersonnel_tbImperative_RequestRegister(pageIndex) {
    document.getElementById('loadingPanel_GridPersonnel_tbImperative_RequestRegister').innerHTML = document.getElementById('hfloadingPanel_GridPersonnel_tbImperative_RequestRegister').value;
    var precardID = '0';
    if (cmbRequestType_tbImperative_RequestRegister.getSelectedItem() != undefined)
        precardID = cmbRequestType_tbImperative_RequestRegister.getSelectedItem().get_id();
    var pageSize = parseInt(document.getElementById('hfImperativePageSize_tbImperative_RequestRegister').value);
    var year = document.getElementById('hfCurrentYear_RequestRegister').value;
    var month = document.getElementById('hfCurrentMonth_RequestRegister').value;
    var isLockedImperative = false;
    if (ImperativeRequestLoadState_RequestRegister != 'Normal') 
            isLockedImperative = true;

    var SearchTermConditions = '';
    switch (LoadState_cmbPersonnel_RequestRegister) {
        case 'Normal':
            break;
        case 'Search':
            SearchTermConditions = document.getElementById('txtPersonnelSearch_RequestRegister').value;
            break;
        case 'AdvancedSearch':
            SearchTermConditions = AdvancedSearchTerm_cmbPersonnel_RequestRegister;
            break;
    }
    CallBack_GridPersonnel_tbImperative_RequestRegister.callback(CharToKeyCode_RequestRegister(LoadState_cmbPersonnel_RequestRegister), CharToKeyCode_RequestRegister(ImperativeRequestLoadState_RequestRegister), CharToKeyCode_RequestRegister(precardID), CharToKeyCode_RequestRegister(year), CharToKeyCode_RequestRegister(month), CharToKeyCode_RequestRegister(isLockedImperative.toString()), CharToKeyCode_RequestRegister(pageSize.toString()), CharToKeyCode_RequestRegister(pageIndex.toString()), CharToKeyCode_RequestRegister(SearchTermConditions));
}

function GridPersonnel_tbImperative_RequestRegister_onItemCheckChange(sender, e) {
    ChangeStrList_CollectivePersonnel('Imperative', e.get_item(), "PersonID", StrCollectiveImperativeList_RequestRegister);
}

function UpdateGridPersonnel_tbImperative_RequestRegister(TargetValue) {
    var separator = '#';
    if (StrCollectiveImperativeList_RequestRegister != null) {
        for (var i = 0; i < GridPersonnel_tbImperative_RequestRegister.get_table().getRowCount() ; i++) {
            personnelItem = GridPersonnel_tbImperative_RequestRegister.get_table().getRow(i);
            var personnelID = personnelItem.getMember('PersonID').get_text();
            GridPersonnel_tbImperative_RequestRegister.beginUpdate();
            if (StrCollectiveImperativeList_RequestRegister.indexOf(separator + personnelID + separator) >= 0) {
                personnelItem.setValue(2, true, false);
                if (TargetValue != null)
                    personnelItem.setValue(5, TargetValue, false);
            }
            else
                personnelItem.setValue(2, false, false);
            GridPersonnel_tbImperative_RequestRegister.endUpdate();
        }
    }
}

function Changefooter_GridPersonnel_tbImperative_RequestRegister() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridPersonnel_tbImperative_RequestRegister').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfImperativePageCount_tbImperative_RequestRegister').value) > 0 ? CurrentPageIndex_GridPersonnel_tbImperative_RequestRegister + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfImperativePageCount_tbImperative_RequestRegister').value;
        if ((i == 1 || i == 3) && GridPersonnel_tbImperative_RequestRegister.get_table().getRowCount() == 0)
            footerValCol[i] = 0;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridPersonnel_tbImperative_RequestRegister').innerHTML = retfooterVal;
}

function txtValue_tbImperative_RequestRegister_onChange() {
    var txtID = 'txtValue_tbImperative_RequestRegister';
    var val = document.getElementById(txtID).value;
    val = !isNaN(parseFloat(val)) ? parseFloat(val) > 0 ? "" + parseFloat(val) + "" : '00' : '00';
    document.getElementById(txtID).value = val.length >= 2 ? val : '0' + val;
}

function tlbItemClose_TlbMonthlyOperationSummary_onClick() {
    CloseDialogMonthlyOperationSummary();
}

function ShowDialogMonthlyOperationSummary() {
    DialogMonthlyOperationSummary.Show();
}

function CloseDialogMonthlyOperationSummary() {
    DialogMonthlyOperationSummary.Close();
}

function DialogMonthlyOperationSummary_OnShow(sender, e) {
    FillMonthlyOperationSummaryList_MonthlyOperationSummary_RequestRegister();
}

function DialogMonthlyOperationSummary_OnClose(sender, e) {
    ClearMonthlyOperationSummaryList_MonthlyOperationSummary_RequestRegister();
}

function FillMonthlyOperationSummaryList_MonthlyOperationSummary_RequestRegister() {
    SelectedItems_GridPersonnel_tbImperative_RequestRegister = GridPersonnel_tbImperative_RequestRegister.getSelectedItems();
    if (SelectedItems_GridPersonnel_tbImperative_RequestRegister.length > 0) {
        document.getElementById('txtPersonnelName_MonthlyOperationSummary_RequestRegister').value = SelectedItems_GridPersonnel_tbImperative_RequestRegister[0].getMember('PersonName').get_text();
        document.getElementById('txtPersonnelCode_MonthlyOperationSummary_RequestRegister').value = SelectedItems_GridPersonnel_tbImperative_RequestRegister[0].getMember('PersonCode').get_text();
        document.getElementById('txtMonthlyOperationSummary_MonthlyOperationSummary_RequestRegister').value = SelectedItems_GridPersonnel_tbImperative_RequestRegister[0].getMember('CalcInfo').get_text();
    }
}

function ClearMonthlyOperationSummaryList_MonthlyOperationSummary_RequestRegister() {
    document.getElementById('txtPersonnelName_MonthlyOperationSummary_RequestRegister').value = '';
    document.getElementById('txtPersonnelCode_MonthlyOperationSummary_RequestRegister').value = '';
    document.getElementById('txtMonthlyOperationSummary_MonthlyOperationSummary_RequestRegister').value = '';
}

function ShowPersonnelMonthlyOperationSummary_GridPersonnel_tbImperative_RequestRegister() {
    ShowDialogMonthlyOperationSummary();
}

function Callback_AttachmentUploader_tbHourly_RequestRegister_onCallBackComplete(sender, e) {
    Subgurim_AttachmentUploader_tbHourly_RequestRegisteradd('1', '4');
}

function Callback_AttachmentUploader_tbHourly_RequestRegister_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function Callback_AttachmentUploader_tbDaily_RequestRegister_onCallBackComplete(sender, e) {
    Subgurim_AttachmentUploader_tbDaily_RequestRegisteradd('1', '4');
}

function Callback_AttachmentUploader_tbDaily_RequestRegister_onCallbackError(sender, e) {
    ShowConnectionError_RequestRegister();
}

function tlbItemDeleteAttachment_TlbDeleteAttachment_tbHourly_RequestRegister_onClick() {
    DeleteRequestAttachment_RequestRegister();
}

function tlbItemDeleteAttachment_TlbDeleteAttachment_tbDaily_RequestRegister_onClick() {
    DeleteRequestAttachment_RequestRegister();
}

function DeleteRequestAttachment_RequestRegister() {
    var requestTarget = GetCurrentRequestTarget_RequestRegister();
    if (ObjRequestAttachment_RequestRegister != null && eval('ObjRequestAttachment_RequestRegister.' + requestTarget) != null && eval('ObjRequestAttachment_RequestRegister.' + requestTarget).RequestAttachmentSavedName != null && eval('ObjRequestAttachment_RequestRegister.' + requestTarget).RequestAttachmentSavedName != '')
        DeleteRequestAttachment_RequestRegisterPage(CharToKeyCode_RequestRegister(requestTarget), CharToKeyCode_RequestRegister(eval('ObjRequestAttachment_RequestRegister.' + requestTarget).RequestAttachmentSavedName));
}

function DeleteRequestAttachment_RequestRegisterPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_RequestRegister').value;
            Response[1] = document.getElementById('hfConnectionError_RequestRegister').value;
        }
        if (RetMessage[2] == 'success') {
            var requestTarget = RetMessage[3];
            switch (requestTarget) {
                case 'Hourly':
                    ObjRequestAttachment_RequestRegister.Hourly = null;
                    break;
                case 'Daily':
                    ObjRequestAttachment_RequestRegister.Daily = null;
                    break;
            }
            document.getElementById('tdAttachmentName_tb' + requestTarget + '_RequestRegister').innerHTML = '';
        }
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function AttachmentUploader_tbHourly_RequestRegister_OnAfterFileUpload(StrRequestAttachment) {
    var message = null;
    if (ObjRequestAttachment_RequestRegister == null)
        ObjRequestAttachment_RequestRegister = new Object();
    ObjRequestAttachment_RequestRegister.Hourly = eval('(' + StrRequestAttachment + ')');
    if (!ObjRequestAttachment_RequestRegister.Hourly.IsErrorOccured)
        message = ObjRequestAttachment_RequestRegister.Hourly.RequestAttachmentRealName;
    else {
        message = ObjRequestAttachment_RequestRegister.Hourly.Message;
        ObjRequestAttachment_RequestRegister.hourly = null;
    }
    document.getElementById('tdAttachmentName_tbHourly_RequestRegister').innerHTML = message;
    Callback_AttachmentUploader_tbHourly_RequestRegister.callback();
}

function AttachmentUploader_tbDaily_RequestRegister_OnAfterFileUpload(StrRequestAttachment) {
    var message = null;
    if (ObjRequestAttachment_RequestRegister == null)
        ObjRequestAttachment_RequestRegister = new Object();
    ObjRequestAttachment_RequestRegister.Daily = eval('(' + StrRequestAttachment + ')');
    if (!ObjRequestAttachment_RequestRegister.Daily.IsErrorOccured)
        message = ObjRequestAttachment_RequestRegister.Daily.RequestAttachmentRealName;
    else {
        message = ObjRequestAttachment_RequestRegister.Daily.Message;
        ObjRequestAttachment_RequestRegister.Daily = null;
    }
    document.getElementById('tdAttachmentName_tbDaily_RequestRegister').innerHTML = message;
    Callback_AttachmentUploader_tbDaily_RequestRegister.callback();
}

function cmbPersonnel_RequestRegister_onChange(sender, e) {
    if (cmbPersonnel_RequestRegister.getSelectedItem() != undefined) {
        RequestPersonnelCountState_RequestRegister = 'Single';
        document.getElementById('headerPersonnelCount_RequestRegister').innerHTML = document.getElementById('hfSelectedPersonnelCount_RequestRegister').value + '1';
    }
}

function tlbItemApply_TlbImperative_onClick() {
    ApplyImperativeRequest_RequestRegister();
}

function ApplyImperativeRequest_RequestRegister() {
    var precardID = '0';
    if (cmbRequestType_tbImperative_RequestRegister.getSelectedItem() != undefined)
        precardID = cmbRequestType_tbImperative_RequestRegister.getSelectedItem().get_id();
    var year = document.getElementById('hfCurrentYear_RequestRegister').value;
    var month = document.getElementById('hfCurrentMonth_RequestRegister').value;
    var imperativeValue = document.getElementById('txtValue_tbImperative_RequestRegister').value;
    if(imperativeValue == null || imperativeValue == '' || imperativeValue == undefined)
        imperativeValue = '0';
    var description = document.getElementById('txtDescription_tbImperative_RequestRegister').value;    
    ApplyImperativeRequest_RequestRegisterPage(CharToKeyCode_RequestRegister(StrCollectiveImperativeList_RequestRegister), CharToKeyCode_RequestRegister(precardID), CharToKeyCode_RequestRegister(year), CharToKeyCode_RequestRegister(month), CharToKeyCode_RequestRegister(imperativeValue), CharToKeyCode_RequestRegister(description));
}

function ApplyImperativeRequest_RequestRegisterPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_RequestRegister').value;
            Response[1] = document.getElementById('hfConnectionError_RequestRegister').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2], false, document.getElementById('Mastertbl_RequestRegister').offsetWidth);
        if (RetMessage[2] == 'success')
            UpdateGridPersonnel_tbImperative_RequestRegister(RetMessage[3]);
    }
}

function tlbItemAppliedRequests_TlbImperativeRequestsFilter_tbImperative_RequestRegister_onClick() {
    ImperativeRequestLoadState_RequestRegister = 'Applied';
    SetPageIndex_GridPersonnel_tbImperative_RequestRegister(0);
}

function tlbItemNotAppliedRequests_TlbImperativeRequestsFilter_tbImperative_RequestRegister_onClick() {
    ImperativeRequestLoadState_RequestRegister = 'NotApplied';
    SetPageIndex_GridPersonnel_tbImperative_RequestRegister(0);
}

function tlbItemView_TlbView_tbImperative_RequestRegister_onClick() {
    ImperativeRequestLoadState_RequestRegister = 'Normal';
    SetPageIndex_GridPersonnel_tbImperative_RequestRegister(0);
}

function SetForeColor_clmnImperativeValue_GridPersonnel_tbImperative_RequestRegister(isLockedImperative) {
    var color = null;
    if (isLockedImperative)
        color = 'green';
    else
        color = 'red';
    return color;
}

function chbAllInThisPage_tbImperative_RequestRegister_onClick() {
    var separator = '#';
    if (StrCollectiveImperativeList_RequestRegister == '')
        StrCollectiveImperativeList_RequestRegister = separator;
    for (var i = 0; i < GridPersonnel_tbImperative_RequestRegister.get_table().getRowCount() ; i++) {
        personnelItem = GridPersonnel_tbImperative_RequestRegister.get_table().getRow(i);
        var personnelID = personnelItem.getMember('PersonID').get_text();
        GridPersonnel_tbImperative_RequestRegister.beginUpdate();
        if (document.getElementById('chbAllInThisPage_tbImperative_RequestRegister').checked) {
            if (StrCollectiveImperativeList_RequestRegister.indexOf(separator + personnelID + separator) < 0 && !personnelItem.getMember('IsLockedImperative').get_value()) {
                StrCollectiveImperativeList_RequestRegister += personnelID + separator;
                personnelItem.setValue(2, true, false);
            }
        }
        else {
            if (StrCollectiveImperativeList_RequestRegister.indexOf(separator + personnelID + separator) >= 0)
                StrCollectiveImperativeList_RequestRegister = StrCollectiveImperativeList_RequestRegister.replace(separator + personnelID + separator, separator);
            personnelItem.setValue(2, false, false);
        }
        GridPersonnel_tbImperative_RequestRegister.endUpdate();
    }
}

function tlbItemClosePicture_TlbPersonnelPicture_onClick() {
    document.getElementById('PersonnelImage_DialogPersonnelImage').src = 'WhitePage.aspx';
    DialogPersonnelImage.Close();
}

function ShowPersonlImage_GridPersonnel_tbImperative_RequestRegister() {
    if (GridPersonnel_tbImperative_RequestRegister.getSelectedItems().length > 0) {
        var dialogPersonnelImageDirection = null;
        switch (parent.parent.CurrentLangID) {
            case 'fa-IR':
                dialogPersonnelImageDirection = 'rtl';
                break;
            case 'en-US':
                dialogPersonnelImageDirection = 'ltr';
                break;
        }
        document.getElementById('Mastertbl_DialogPersonnelImage').dir = dialogPersonnelImageDirection;
        var PersonnelImage = GridPersonnel_tbImperative_RequestRegister.getSelectedItems()[0].getMember('PersonImage').get_text();
        document.getElementById('tdPersonnel_DialogPersonnelImage').innerHTML = GridPersonnel_tbImperative_RequestRegister.getSelectedItems()[0].getMember('PersonName').get_text();
        document.getElementById('PersonnelImage_DialogPersonnelImage').src = "ImageViewer.aspx?reload=" + (new Date()).getTime() + "&AttachmentType=Personnel&ClientAttachment=" + CharToKeyCode_RequestRegister(PersonnelImage);
        DialogPersonnelImage.Show();
    }
}

function GridPersonnel_tbImperative_RequestRegister_onItemSelect(sender, e){
    var imperativeValue = e.get_item().getMember('ImperativeValue').get_text();
    if (imperativeValue != null && imperativeValue != '' && !isNaN(parseInt(imperativeValue)))
        document.getElementById('txtValue_tbImperative_RequestRegister').value = imperativeValue;
}





















