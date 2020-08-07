
var ConfirmState_LeaveBudget = null;

function GetBoxesHeaders_LeaveBudget() {
    parent.document.getElementById('Title_DialogLeaveBudget').innerHTML = document.getElementById('Title_DialogLeaveBudget').value;
}

function tlbItemEndorsement_TlbLeaveBudget_LeaveBudget_onClick() {
    CollapseControls_LeaveBudget();
    UpdateLeaveBudget_LeaveBudget();
}

function tlbItemExit_TlbLeaveBudget_LeaveBudget_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_LeaveBudget = confirmState;
    switch (ConfirmState_LeaveBudget) {
        case 'Exit':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_LeaveBudget').value;
            break;
    }
    DialogConfirm.Show();
    CollapseControls_LeaveBudget();
}

function LeaveBudget_onClose() {
    parent.document.getElementById('DialogLeaveBudget_IFrame').src = 'WhitePage.aspx';
    parent.DialogLeaveBudget.Close();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_LeaveBudget) {
        case 'Exit':
            LeaveBudget_onClose();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function GetAxises_LeaveBudget() {
    var error = document.getElementById('ErrorHiddenField_BudgetAxises_LeaveBudget').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else {
        var MonthsAxises = null;
        var DaysAxises = null;
        var SplitedObjs = document.getElementById('hfBudgetAxises_LeaveBudget').value;
        MonthsAxises = SplitedObjs.split('@');
        for (var i = 0; i < MonthsAxises.length; i++) {
            document.getElementById("lblMonth" + (i + 1) + "_LeaveBudget").innerHTML = MonthsAxises[i];
        }
    }
}

function CharToKeyCode_LeaveBudget(str) {
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

function Init_TimeSelectors_LeaveBudget() {
    FetchRelativeOperation_TimePickers_LeaveBudget('Reset');
    //FetchRelativeOperation_TimePickers_LeaveBudget('ChangeFloat');
    FetchRelativeOperation_TimePickers_LeaveBudget('ChangeButtonImage');
    FetchRelativeOperation_TimePickers_LeaveBudget('ChangeAction');
}

function FetchRelativeOperation_TimePickers_LeaveBudget(ActionType) {
    var RelativeOperation = null;
    switch (ActionType) {
        case 'Reset':
            RelativeOperation = 'ResetTimepicker_LeaveBudget';
            break;
        case 'ChangeFloat':
            RelativeOperation = 'ChangeFloat_TimeSelector_LeaveBudget';
            break;
        case 'ChangeButtonImage':
            RelativeOperation = 'SetButtonImages_TimeSelector_LeaveBudget';
            break;
        case 'ChangeAction':
            RelativeOperation = 'ChangeTimePickerActions_TimeSelector_LeaveBudget';
            break;
        case 'ChangeEnabled':
            RelativeOperation = 'ChangeTimePickerEnabled_LeaveBudget';
            break;
    }
    eval(RelativeOperation + '("TimeSelector_Hour_LeaveBudget")');
    for (var i = 1; i < 13; i++) {
        eval(RelativeOperation + '("TimeSelector_Month'+i.toString()+'_LeaveBudget")');        
    }
}

function Init_DayBoxes_LeaveBudget() {
    ChangeDayBoxEnabled_LeaveBudget('txtYearBudgetDay_LeaveBudget', 'disable');
    for (var i = 1; i < 13; i++) {
        ChangeDayBoxEnabled_LeaveBudget('txtMonth' + i.toString() + '_LeaveBudget', 'disable');
    }
}

function ChangeDayBoxEnabled_LeaveBudget(dayBoxID, state) {
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

function ChangeTimePickerEnabled_LeaveBudget(TimeSelector, state) {
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
                CheckTimePickerState_Shift(TimeSelector + '_txtHour');
                CheckTimePickerState_Shift(TimeSelector + '_txtMinute');
                addTime(document.getElementById("" + TimeSelector + "_imgUp"), 24, 1, 1);
            };
            document.getElementById("" + TimeSelector + "_imgDown").onclick = function () {
                CheckTimePickerState_Shift(TimeSelector + '_txtHour');
                CheckTimePickerState_Shift(TimeSelector + '_txtMinute');
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


function ResetTimepicker_LeaveBudget(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}

function ChangeTimePickerActions_TimeSelector_LeaveBudget(TimeSelector) {
    document.getElementById("" + TimeSelector + "_txtHour").onchange = function () {
        TimeSelector_LeaveBudget_onChange(TimeSelector, '_txtHour');
    };
    document.getElementById("" + TimeSelector + "_txtMinute").onchange = function () {
        TimeSelector_LeaveBudget_onChange(TimeSelector, '_txtMinute');
    };
}

function TimeSelector_LeaveBudget_onChange(TimeSelector, partID) {
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

function DayBox_LeaveBudget_onChange(dayBoxID) {
    var val = document.getElementById(dayBoxID).value;
    val = !isNaN(parseFloat(val)) ? parseFloat(val) > 0 ? "" + parseFloat(val) + "" : '0' : '0';
    document.getElementById(dayBoxID).value = val;
}

function ChangeFloat_TimeSelector_LeaveBudget(TimeSelector) {
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

function SetButtonImages_TimeSelector_LeaveBudget(TimeSelector) {
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

function tlbItemView_TlbView_LeaveBudget_onClick() {
    var ObjDialogLeaveBudget = parent.DialogLeaveBudget.get_value();
    var RuleGroupID = ObjDialogLeaveBudget.RuleGroupID;
    var Year = document.getElementById('hfCurrentYear_LeaveBudget').value;
    GetLeaveBudget_LeaveBudgetPage(CharToKeyCode_LeaveBudget(Year), CharToKeyCode_LeaveBudget(RuleGroupID));
    DialogWaiting.Show();
}

function GetLeaveBudget_LeaveBudgetPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_LeaveBudget').value;
            Response[1] = document.getElementById('hfConnectionError_LeaveBudget').value;
        }
        if (Response[2] != 'error') {
            var ObjLeaveBudget = Response[3];
            if (ObjLeaveBudget != null && ObjLeaveBudget != '' && ObjLeaveBudget != undefined) {
                ObjLeaveBudget = eval('(' + ObjLeaveBudget + ')');
                FillLeaveBudgetList_LeaveBudget(ObjLeaveBudget);
            }
        }
        else
            showDialog(Response[0], Response[1], Response[2]);
    }
}

function cmbYear_LeaveBudget_onChange(sender, e) {
    document.getElementById('hfCurrentYear_LeaveBudget').value = cmbYear_LeaveBudget.getSelectedItem().get_value();
}

function SetCurrentLeaveBudget_LeaveBudget() {
    var error = document.getElementById('ErrorHiddenField_LeaveBudget').value;
    if (error == "") {
        var ObjLeaveBudget = document.getElementById('hfLeaveBudget_LeaveBudget').value;
        if (ObjLeaveBudget != null && ObjLeaveBudget != '' && ObjLeaveBudget != undefined) {
            ObjLeaveBudget = eval('(' + ObjLeaveBudget + ')');
            FillLeaveBudgetList_LeaveBudget(ObjLeaveBudget);
        }
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}

function FillLeaveBudgetList_LeaveBudget(ObjLeaveBudget) {
    var LeaveBudgetType = ObjLeaveBudget.LeaveBudgetType;
    document.getElementById('txtDescription_LeaveBudget').value = ObjLeaveBudget.Description != null ? ObjLeaveBudget.Description : '';
    switch (LeaveBudgetType) {
        case 'Usual':
            document.getElementById('rdbYearBudget_LeaveBudget').checked = true;
            document.getElementById('rdbSpecialCase_LeaveBudget').checked = false;
            document.getElementById('txtYearBudgetDay_LeaveBudget').value = ObjLeaveBudget.YD != null ? ObjLeaveBudget.YD : '';
            SetDuration_TimePicker_LeaveBudget('TimeSelector_Hour_LeaveBudget', ObjLeaveBudget.YH);
            break;
        case 'PerMonth':
            document.getElementById('rdbSpecialCase_LeaveBudget').checked = true;
            document.getElementById('rdbYearBudget_LeaveBudget').checked = false;
            for (var i = 1; i < 13; i++) {
                var day = eval('ObjLeaveBudget.MD' + i.toString() + '');
                var hour = eval('ObjLeaveBudget.MH' + i.toString() + '');
                document.getElementById('txtMonth' + i.toString() + '_LeaveBudget').value = day != null ? day : '';
                SetDuration_TimePicker_LeaveBudget('TimeSelector_Month' + i.toString() + '_LeaveBudget', hour);
            }
            break;
    }
    ClearRelativeList_LeaveBudget(LeaveBudgetType);
    ChangeControlsEnabled_LeaveBudget(LeaveBudgetType);
}

function ClearRelativeList_LeaveBudget(state) {
    switch (state) {
        case 'Usual':
            for (var i = 1; i < 13; i++) {
                document.getElementById('txtMonth' + i.toString() + '_LeaveBudget').value = '';
                ResetTimepicker_LeaveBudget('TimeSelector_Month' + i.toString() + '_LeaveBudget');
            }
            break;
        case 'PerMonth':
            document.getElementById('txtYearBudgetDay_LeaveBudget').value = '';
            ResetTimepicker_LeaveBudget('TimeSelector_Hour_LeaveBudget');
            break;
    }
}

function SetDuration_TimePicker_LeaveBudget(TimePicker, strTime) {
    if (strTime == "" || strTime == null)
        strTime = '00:00';
    var arTime_LeaveBudget = strTime.split(':');
    for (var i = 0; i < 2; i++) {
        if (arTime_LeaveBudget[i].length < 2)
            arTime_LeaveBudget[i] = '0' + arTime_LeaveBudget[i];
    }
    document.getElementById(TimePicker + '_txtHour').value = arTime_LeaveBudget[0];
    document.getElementById(TimePicker + '_txtMinute').value = arTime_LeaveBudget[1];
}

function UpdateLeaveBudget_LeaveBudget() {
    var ObjDialogLeaveBudget = parent.DialogLeaveBudget.get_value();
    var RuleGroupID = ObjDialogLeaveBudget.RuleGroupID;
    var Year = document.getElementById('hfCurrentYear_LeaveBudget').value;
    var LeaveBudgetType = LeaveBudget_onBeforeUpdate();
    var StrObjLeaveBudget = CreateLeaveBudgetList_LeaveBudget(LeaveBudgetType);
    var Description = document.getElementById('txtDescription_LeaveBudget').value;
    UpdateLeaveBudget_LeaveBudgetPage(CharToKeyCode_LeaveBudget(RuleGroupID), CharToKeyCode_LeaveBudget(Year), CharToKeyCode_LeaveBudget(StrObjLeaveBudget));
    DialogWaiting.Show();
}

function UpdateLeaveBudget_LeaveBudgetPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_LeaveBudget').value;
            Response[1] = document.getElementById('hfConnectionError_LeaveBudget').value;
        }
        if (Response[2] == 'success') {
            var leaveBudgetType = Response[3];
            ClearRelativeList_LeaveBudget(leaveBudgetType);
        }
        showDialog(Response[0], Response[1], Response[2]);
    }
}

function LeaveBudget_onBeforeUpdate() {
    var LeaveBudgetType = null;
    if (document.getElementById('rdbYearBudget_LeaveBudget').checked) {
        document.getElementById('rdbSpecialCase_LeaveBudget').checked = false;
        LeaveBudgetType = 'Usual';
    }
    if (document.getElementById('rdbSpecialCase_LeaveBudget').checked) {
        document.getElementById('rdbYearBudget_LeaveBudget').checked = false;
        LeaveBudgetType = 'PerMonth';
    }
    if (LeaveBudgetType == null) {
        document.getElementById('rdbYearBudget_LeaveBudget').checked = true;
        LeaveBudgetType = 'Usual';
    }
    return LeaveBudgetType;
}

function CreateLeaveBudgetList_LeaveBudget(LeaveBudgetType) {
    var StrObjLeaveBudget = null;
    var description = document.getElementById('txtDescription_LeaveBudget').value;
    switch (LeaveBudgetType) {
        case 'Usual':
            var day = document.getElementById('txtYearBudgetDay_LeaveBudget').value;
            var hour = GetDuration_TimePicker_LeaveBudget('TimeSelector_Hour_LeaveBudget');
            StrObjLeaveBudget = '"LeaveBudgetType":"' + LeaveBudgetType + '","YD":"' + day + '","YH":"' + hour + '","Description":"' + description + '"';
            break;
        case 'PerMonth':
            StrObjLeaveBudget = '"LeaveBudgetType":"' + LeaveBudgetType + '"';
            for (var i = 1; i < 13; i++) {
                StrObjLeaveBudget += ',"MD' + i.toString() + '":"' + document.getElementById('txtMonth' + i.toString() + '_LeaveBudget').value + '","MH' + i.toString() + '":"' + GetDuration_TimePicker_LeaveBudget('TimeSelector_Month' + i.toString() + '_LeaveBudget') + '"';
            }
            StrObjLeaveBudget += ',"Description":"' + description + '"';
            break;
    }
    StrObjLeaveBudget = '{' + StrObjLeaveBudget + '}';
    return StrObjLeaveBudget;
}

function GetDuration_TimePicker_LeaveBudget(TimePicker) {
    if (document.getElementById(TimePicker + '_txtHour').value.length < 2)
        document.getElementById(TimePicker + '_txtHour').value = '0' + document.getElementById(TimePicker + '_txtHour').value;
    if (document.getElementById(TimePicker + '_txtMinute').value.length < 2)
        document.getElementById(TimePicker + '_txtMinute').value = '0' + document.getElementById(TimePicker + '_txtMinute').value;
    return document.getElementById(TimePicker + '_txtHour').value + ':' + document.getElementById(TimePicker + '_txtMinute').value;
}

function rdbYearBudget_LeaveBudget_onClick() {
    ChangeControlsEnabled_LeaveBudget('Usual');
}

function rdbSpecialCase_LeaveBudget_onClick() {
    ChangeControlsEnabled_LeaveBudget('PerMonth');
}

function ChangeControlsEnabled_LeaveBudget(state) {
    var usualEnabled = null;
    var perMonthEnabled = null;
    switch (state) {
        case 'Usual':
            usualEnabled = 'enable';
            perMonthEnabled = 'disable';
            break;
        case 'PerMonth':
            usualEnabled = 'disable';
            perMonthEnabled = 'enable';
            break;
    }
    ChangeDayBoxEnabled_LeaveBudget('txtYearBudgetDay_LeaveBudget', usualEnabled);
    ChangeTimePickerEnabled_LeaveBudget('TimeSelector_Hour_LeaveBudget', usualEnabled);
    for (var i = 1; i < 13; i++) {
        ChangeDayBoxEnabled_LeaveBudget('txtMonth' + i.toString() + '_LeaveBudget', perMonthEnabled);
        ChangeTimePickerEnabled_LeaveBudget('TimeSelector_Month' + i.toString() + '_LeaveBudget', perMonthEnabled);
    }
}

function CollapseControls_LeaveBudget() {
    cmbYear_LeaveBudget.collapse();
}

function tlbItemFormReconstruction_TlbLeaveBudget_LeaveBudget_onClick() {
    LeaveBudget_onClose();
    parent.document.getElementById('pgvRulesGroupIntroduction_iFrame').contentWindow.ShowDialogLeaveBudget();    
}







