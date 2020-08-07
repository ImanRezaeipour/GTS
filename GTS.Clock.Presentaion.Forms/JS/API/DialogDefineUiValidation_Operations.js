var ObjDialogDefineUiValidation;
var ObjDateRangeParametersValue_RuleParameters = '';
var selectedDataRangeID_RuleParameters = '-1';
var ObjRuleParameter_RuleParameters = null;
var CurrentPageState_RuleParameters = 'View';
var CurrentPageState_Rule = 'View';

function CallBack_GridDefineUiValidation_DefineUiValidation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DefineUiValidation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridDefineUiValidation_DefineUiValidation();
    }
}
function ShowConnectionError_DefineUiValidation() {
    var error = document.getElementById('hfErrorType_DefineUiValidation').value;
    var errorBody = document.getElementById('hfConnectionError_DefineUiValidation').value;
    showDialog(error, errorBody, 'error');
}
function CallBack_GridDefineUiValidation_DefineUiValidation_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridDefineUiValidation_DefineUiValidation').innerHTML = '';
    ShowConnectionError_UiValidation();
}
function GetBoxesHeaders_DefineUiValidation() {
    parent.document.getElementById('Title_DialogDefineUiValidation').innerHTML = document.getElementById('hfTitle_DialogDefineUiValidation').value;
    document.getElementById('header_DefineUiValidation_DefineUiValidation').innerHTML = document.getElementById('hfheader_DefineUiValidation_DefineUiValidation').value;
    document.getElementById('header_ParameterDefineUiValidation_DefineUiValidation').innerHTML = document.getElementById('hfheader_ParameterDefineUiValidation_DefineUiValidation').value;
}
function SetActionMode_DefineUiValidation(state) {
    document.getElementById('ActionMode_DefineUiValidation').innerHTML = document.getElementById("hf" + state + "_DefineUiValidation").value;
}
function Refresh_GridDefineUiValidation_DefineUiValidation() {
    Fill_GridDefineUiValidation_DefineUiValidation();
}
function GridDefineUiValidation_DefineUiValidation_onItemSelect(sender, e) {
    NavigateUiValidation_UiValidation(e.get_item());
    CurrentPageState_RuleParameters = 'View';
    ChangePageState_RuleParameters();
    ClearList_RuleParameters();

}
function GridDefineUiValidation_DefineUiValidation_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridDefineUiValidation_DefineUiValidation').innerHTML = '';
}

function NavigateUiValidation_UiValidation(selectedGridDefineUiValidation) {
    var selectedID = selectedGridDefineUiValidation.getMember('ID').get_text();
    var selectedRuleID = selectedGridDefineUiValidation.getMember('RuleID').get_text();
    Fill_GridParameterDefineUiValidation_DefineUiValidation(selectedID, selectedRuleID);
}


function CallBack_GridParameterDefineUiValidation_DefineUiValidation_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridParameterDefineUiValidation_DefineUiValidation').innerHTML = '';
    ShowConnectionError_DefineUiValidation();
}
//function Refresh_GridParameterDefineUiValidation_DefineUiValidation() {
//    Fill_GridParameterDefineUiValidation_DefineUiValidation();
//}
function GridParameterDefineUiValidation_DefineUiValidation_onItemSelect(sender, e) {
    NavligateRuleParameter_RuleParameters(e.get_item());
}
function GridParameterDefineUiValidation_DefineUiValidation_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridParameterDefineUiValidation_DefineUiValidation').innerHTML = '';
}
function tlbItemExit_TlbDefineUiValidation_onClick() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_DefineUiValidation').value;
    DialogConfirm.Show();

}
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}
function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDilaogDefineUiValidation();
}
function CloseDilaogDefineUiValidation() {
    parent.document.getElementById('DialogDefineUiValidation_IFrame').src = 'WhitePage.aspx';
    parent.DialogDefineUiValidation.Close();
}
function tlbItemFormReconstruction_TlbDefineUiValidation_onClick() {
    CloseDilaogDefineUiValidation();
    var ObjDialogDefineUiValidation = parent.DialogDefineUiValidation.get_value();
    parent.document.getElementById('pgvUiValidation_iFrame').contentWindow.ShowDialogDefineUiValidation(ObjDialogDefineUiValidation.State);
}


function ViewCurrentLangCalendars_RuleParameters() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpDate_RuleParameters").parentNode.removeChild(document.getElementById("pdpDate_RuleParameters"));
            document.getElementById("pdpDate_RuleParametersimgbt").parentNode.removeChild(document.getElementById("pdpDate_RuleParametersimgbt"));
            document.getElementById("pdpFromDate_RuleParameters").parentNode.removeChild(document.getElementById("pdpFromDate_RuleParameters"));
            document.getElementById("pdpFromDate_RuleParametersimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_RuleParametersimgbt"));
            document.getElementById("pdpToDate_RuleParameters").parentNode.removeChild(document.getElementById("pdpToDate_RuleParameters"));
            document.getElementById("pdpToDate_RuleParametersimgbt").parentNode.removeChild(document.getElementById("pdpToDate_RuleParametersimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_DateCalendars_RuleParameters").removeChild(document.getElementById("Container_gCalDate_RuleParameters"));
            document.getElementById("Container_FromDateCalendars_RuleParameters").removeChild(document.getElementById("Container_gCalFromDate_RuleParameters"));
            document.getElementById("Container_ToDateCalendars_RuleParameters").removeChild(document.getElementById("Container_gCalToDate_RuleParameters"));
            break;
    }
}
function Fill_ObjectDefineUiValidation_DefineUiValidation() {
    ObjDialogDefineUiValidation = parent.DialogDefineUiValidation.get_value();
    document.getElementById("txtNameDefineUiValidation_DefineUiValidation").value = ObjDialogDefineUiValidation.Name;
}
function Fill_GridDefineUiValidation_DefineUiValidation() {
    document.getElementById('loadingPanel_GridDefineUiValidation_DefineUiValidation').innerHTML = document.getElementById('hfloadingPanel_GridDefineUiValidation_DefineUiValidation').value;
    CallBack_GridDefineUiValidation_DefineUiValidation.callback(CharToKeyCode_DefineUiValidation(ObjDialogDefineUiValidation.GroupID));
}
function CharToKeyCode_DefineUiValidation(str) {
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
function NavigateDefineUiValidation_DefineUiValidation(selectedDefineUiValidationItem) {
    if (selectedUiValidationItem != undefined) {
        var selectedRuleID = selectedDefineUiValidationItem.getMember('ID').get_text();
        Fill_GridParameterDefineUiValidation_DefineUiValidation(selectedRuleID);

    }
}
function Fill_GridParameterDefineUiValidation_DefineUiValidation(ID, RuleID) {
    var selectedID = ID;
    var selectedRuleID = RuleID;
    document.getElementById('loadingPanel_GridParameterDefineUiValidation_DefineUiValidation').innerHTML = document.getElementById('hfloadingPanel_GridParameterDefineUiValidation_DefineUiValidation').value;
    CallBack_GridParameterDefineUiValidation_DefineUiValidation.callback(CharToKeyCode_DefineUiValidation(ID), CharToKeyCode_DefineUiValidation(selectedRuleID));
}
function CallBack_GridParameterDefineUiValidation_DefineUiValidation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ParameterDefineUiValidation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridParameterDefineUiValidation_DefineUiValidation();
    }
}
function NavligateRuleParameter_RuleParameters(selectedRuleParameter) {
    if (selectedRuleParameter != undefined) {
        CurrentPageState_RuleParameters = 'Edit';
        ChangePageState_RuleParameters();
        var ruleParameterValue = selectedRuleParameter.getMember('TheValue').get_text();
        switch (selectedRuleParameter.getMember('Type').get_text()) {
            case '0':
                TabStripRuleParametersTerms.selectTabById('tbNumeric_TabStripRuleParametersTerms');
                document.getElementById('txtNumeric_RuleParameters').value = ruleParameterValue;
                break;
            case '1':
                TabStripRuleParametersTerms.selectTabById('tbTime_TabStripRuleParametersTerms');
                if (ruleParameterValue != '') {
                    SetDuration_TimePicker_RuleParameters('TimeSelector_Hour_RuleParameters', ruleParameterValue);
                }
                break;
            case '2':
                TabStripRuleParametersTerms.selectTabById('tbDate_TabStripRuleParametersTerms');
                if (ruleParameterValue != '') {
                    switch (parent.parent.SysLangID) {
                        case 'fa-IR':
                            document.getElementById('pdpDate_RuleParameters').value = ruleParameterValue;
                            break;
                        case 'en-US':
                            ruleparametervalue = new date(ruleparametervalue);
                            gCalDate_RuleParameters.setSelectedDate(ruleParameterValue);
                            gdpDate_RuleParameters.setSelectedDate(ruleParameterValue);
                            break;
                    }
                }
                break;
        }
    }
}
function SetDuration_TimePicker_RuleParameters(TimePicker, strTime) {
    var arTime_RuleParameter = strTime.split(':');
    for (var i = 0; i < 2; i++) {
        if (arTime_RuleParameter[i].length < 2)
            arTime_RuleParameter[i] = '0' + arTime_RuleParameter[i];
    }
    document.getElementById(TimePicker + '_txtHour').value = arTime_RuleParameter[0];
    document.getElementById(TimePicker + '_txtMinute').value = arTime_RuleParameter[1];
}
function gdpDate_RuleParameters_OnDateChange(sender, e) {
    var Date = gdpDate_RuleParameters.getSelectedDate();
    gCalDate_RuleParameters.setSelectedDate(Date);
}
function gCalDate_RuleParameters_OnChange(sender, e) {
    var Date = gCalDate_RuleParameters.getSelectedDate();
    gdpDate_RuleParameters.setSelectedDate(Date);
}
function gCalDate_RuleParameters_onLoad(sender, e) {
    window.gCalDate_RuleParameters.PopUpObject.z = 25000000;
}
function btn_gdpDate_RuleParameters_OnClick(event) {
    if (gCalDate_RuleParameters.get_popUpShowing()) {
        gCalDate_RuleParameters.hide();
    }
    else {
        gCalDate_RuleParameters.setSelectedDate(gdpDate_RuleParameters.getSelectedDate());
        gCalDate_RuleParameters.show();
    }
}

function ChangePageState_RuleParameters() {
    state = CurrentPageState_RuleParameters;
    if (state == 'Edit') {
        TlbParameterDefineUiValidation.get_items().getItemById('tlbItemSave_TlbParameterDefineUiValidation').set_enabled(true);
        TlbParameterDefineUiValidation.get_items().getItemById('tlbItemSave_TlbParameterDefineUiValidation').set_imageUrl('save.png');
        TlbParameterDefineUiValidation.get_items().getItemById('tlbItemCancel_TlbParameterDefineUiValidation').set_enabled(true);
        TlbParameterDefineUiValidation.get_items().getItemById('tlbItemCancel_TlbParameterDefineUiValidation').set_imageUrl('cancel.png');
        TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms').set_enabled(true);
        TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms').set_imageUrl('save.png');
        TlbConfirm_pgvTime_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms').set_enabled(true);
        TlbConfirm_pgvTime_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms').set_imageUrl('save.png');
        TlbConfirm_pgvDate_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms').set_enabled(true);
        TlbConfirm_pgvDate_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms').set_imageUrl('save.png');
        document.getElementById('txtNumeric_RuleParameters').disabled = '';
        ChangeCalendarsEnabled_RuleParameters('enable');
        ChangeTimePickerEnabled_RuleParameters('TimeSelector_Hour_RuleParameters', 'enable');

    }
    if (state == 'View') {
        TlbParameterDefineUiValidation.get_items().getItemById('tlbItemSave_TlbParameterDefineUiValidation').set_enabled(false);
        TlbParameterDefineUiValidation.get_items().getItemById('tlbItemSave_TlbParameterDefineUiValidation').set_imageUrl('save_silver.png');
        TlbParameterDefineUiValidation.get_items().getItemById('tlbItemCancel_TlbParameterDefineUiValidation').set_enabled(false);
        TlbParameterDefineUiValidation.get_items().getItemById('tlbItemCancel_TlbParameterDefineUiValidation').set_imageUrl('cancel_silver.png');
        TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms').set_enabled(false);
        TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms').set_imageUrl('save_silver.png');
        TlbConfirm_pgvTime_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms').set_enabled(false);
        TlbConfirm_pgvTime_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms').set_imageUrl('save_silver.png');
        TlbConfirm_pgvDate_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms').set_enabled(false);
        TlbConfirm_pgvDate_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms').set_imageUrl('save_silver.png');
        document.getElementById('txtNumeric_RuleParameters').disabled = 'disabled';
        ChangeCalendarsEnabled_RuleParameters('disable');
        ChangeTimePickerEnabled_RuleParameters('TimeSelector_Hour_RuleParameters', 'disable');
    }
}
function ChangeTimePickerEnabled_RuleParameters(TimeSelector, state) {
    var disabled = null;
    switch (state) {
        case 'disable':
            disabled = 'disabled';
            break;
        case 'enable':
            disabled = '';
            break;
    }
    document.getElementById("" + TimeSelector + "_txtHour").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").disabled = disabled;
}
function ChangeCalendarsEnabled_RuleParameters(state) {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ChangeCalendarEnabled_RuleParameters('pdpDate_RuleParameters', state);
            break;
        case 'en-US':
            ChangeCalendarEnabled_RuleParameters('gdpDate_RuleParameters', state);
            break;
    }
}

function ChangeCalendarEnabled_RuleParameters(cal, state) {
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
                        CalendarsViewManage_RuleParameters(cal);
                    };
                    break;
            }
            break;
    }
}
function ClearList_RuleParameters() {
    ResetCalendars_RuleParameters();
    ResetTimepicker_RuleParameters('TimeSelector_Hour_RuleParameters');
    document.getElementById('txtNumeric_RuleParameters').value = '';

}
function ResetCalendars_RuleParameters() {
    var currentDate_RuleParameters = document.getElementById('hfCurrentDate_RuleParameters').value;
    switch (parent.parent.SysLangID) {
        case 'en-US':
            currentDate_RuleParameters = new Date(currentDate_RuleParameters);
            gdpDate_RuleParameters.setSelectedDate(currentDate_RuleParameters);
            gCalDate_RuleParameters.setSelectedDate(currentDate_RuleParameters);
            break;
        case 'fa-IR':
            document.getElementById('pdpDate_RuleParameters').value = currentDate_RuleParameters;
            break;
    }
}
function ResetTimepicker_RuleParameters(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
}

function btn_gdpDate_RuleParameters_OnClick(event) {
    if (gCalDate_RuleParameters.get_popUpShowing()) {
        gCalDate_RuleParameters.hide();
    }
    else {
        gCalDate_RuleParameters.setSelectedDate(gdpDate_RuleParameters.getSelectedDate());
        gCalDate_RuleParameters.show();
    }
}
function btn_gdpDate_RuleParameters_OnMouseUp(event) {
    if (gCalDate_RuleParameters.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}


function FocusOnCurrentTimeSelector(TimeSelector) {
    if (document.activeElement.id != "" + TimeSelector + "_txtHour" && document.activeElement.id != "" + TimeSelector + "_txtMinute" && document.activeElement.id != "" + TimeSelector + "_txtSecond" && !document.getElementById("" + TimeSelector + "_txtHour").disabled)
        document.getElementById("" + TimeSelector + "_txtHour").focus();
}

function ViewCurrentLangCalendars_RuleParameters() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpDate_RuleParameters").parentNode.removeChild(document.getElementById("pdpDate_RuleParameters"));
            document.getElementById("pdpDate_RuleParametersimgbt").parentNode.removeChild(document.getElementById("pdpDate_RuleParametersimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_DateCalendars_RuleParameters").removeChild(document.getElementById("Container_gCalDate_RuleParameters"));
            break;
    }
}
function GetDuration_TimePicker_RuleParameters(TimePicker) {
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
function tlbItemCancel_TlbDefineUiValidation_onClick() {
    CurrentPageState_RuleParameters = 'View';
    ChangePageState_RuleParameters();
    ClearList_RuleParameters();
    GridParameterDefineUiValidation_DefineUiValidation.unSelectAll();
}


function UpdateDefineUiValidation_DefineUiValidation() {
    CurrentPageState_Rule = 'Edit';

    var gridItem;
    var itemIndex = 0;
    var strListRowValue = '';
    strListRowValue += '[';
    while (gridItem = GridDefineUiValidation_DefineUiValidation.get_table().getRow(itemIndex)) {
        strListRowValue += '{"uvID":"' + gridItem.getMember("ID").get_text() + '","IsA":"' + gridItem.getMember("Active").get_text() + '","IsO":"' + gridItem.getMember("OpratorRestriction").get_text() + '","GID":"' + ObjDialogDefineUiValidation.GroupID + '","RID":"' + gridItem.getMember("RuleID").get_text() + '"},';
        itemIndex++;
    }
    strListRowValue = strListRowValue.substring(0, strListRowValue.length - 1);
    strListRowValue += ']';

    UpdateDefineUiValidation_DefineUiValidationPage(CharToKeyCode_DefineUiValidation(strListRowValue), CharToKeyCode_DefineUiValidation(CurrentPageState_Rule));
}
function UpdateDefineUiValidation_DefineUiValidationPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_UiValidation').value;
            Response[1] = document.getElementById('hfConnectionError_UiValidation').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            CurrentPageState_RuleParameters = 'View';
            CurrentPageState_Rule = 'View';
            ChangePageState_RuleParameters();
        }
    }
}

function tlbItemSave_TlbDefineUiValidation_onClick() {
    UpdateDefineUiValidation_DefineUiValidation();

}
function tlbItemSave_TlbParameterDefineUiValidation_onClick() {
    UpdateParameterDefineUiValidation_DefineUiValidation();
}

function UpdateParameterDefineUiValidation_DefineUiValidation() {
    var gridItem;
    var itemIndex = 0;
    var strListRowValue = '';
    strListRowValue += '[';
    while (gridItem = GridParameterDefineUiValidation_DefineUiValidation.get_table().getRow(itemIndex)) {
        strListRowValue += '{"PID":"' + gridItem.getMember("ID").get_text() + '","Name":"' + gridItem.getMember("Name").get_text() + '","Val":"' + gridItem.getMember("TheValue").get_text() + '","Type":"' + gridItem.getMember("Type").get_text() + '","KN":"' + gridItem.getMember("KeyName").get_text() + '"},';
        itemIndex++;
    }
    strListRowValue = strListRowValue.substring(0, strListRowValue.length - 1);
    strListRowValue += ']';

    UpdateParameterDefineUiValidation_DefineUiValidationPage(CharToKeyCode_DefineUiValidation(strListRowValue), CharToKeyCode_DefineUiValidation(CurrentPageState_RuleParameters));
}
function UpdateParameterDefineUiValidation_DefineUiValidationPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_UiValidation').value;
            Response[1] = document.getElementById('hfConnectionError_UiValidation').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_RuleParameters();
            CurrentPageState_RuleParameters = 'View';
            CurrentPageState_Rule = 'View';
            ChangePageState_RuleParameters();
            GridParameterDefineUiValidation_DefineUiValidation.unSelectAll();

        }

    }
}

function tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms_onClick() {
    if (GridParameterDefineUiValidation_DefineUiValidation.getSelectedItems().length > 0) {
        var NumericValue_RuleParameters = document.getElementById('txtNumeric_RuleParameters').value;
        if (!isNaN(parseInt(NumericValue_RuleParameters)))
            UpadateGridParameterDefineUiValidation_DefineUiValidation(NumericValue_RuleParameters);
    }
}

function tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms_onClick() {
    if (GridParameterDefineUiValidation_DefineUiValidation.getSelectedItems().length > 0) {
        var TimeValue_RuleParametes = GetDuration_TimePicker_RuleParameters('TimeSelector_Hour_RuleParameters');
        UpadateGridParameterDefineUiValidation_DefineUiValidation(TimeValue_RuleParametes);
    }
}
function tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms_onClick() {
    if (GridParameterDefineUiValidation_DefineUiValidation.getSelectedItems().length > 0) {
        var DateValue_RuleParametes = null;
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                DateValue_RuleParametes = document.getElementById('pdpDate_RuleParameters').value;
                break;
            case 'en-US':
                DateValue_RuleParametes = document.getElementById('gdpDate_RuleParameters_picker').value;
                break;
        }
        UpadateGridParameterDefineUiValidation_DefineUiValidation(DateValue_RuleParametes);
    }
}

function UpadateGridParameterDefineUiValidation_DefineUiValidation(ruleParameterValue) {
    GridParameterDefineUiValidation_DefineUiValidation.beginUpdate();
    GridParameterDefineUiValidation_DefineUiValidation.getSelectedItems()[0].setValue(4, ruleParameterValue, false);
    GridParameterDefineUiValidation_DefineUiValidation.endUpdate();
}

function TimeSelector_Hour_RuleParameters_onChange(partID) {
    var id = 'TimeSelector_Hour_RuleParameters_' + partID;
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