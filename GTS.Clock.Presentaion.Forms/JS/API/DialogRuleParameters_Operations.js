
var CurrentPageState_RuleParameters = 'View';
var ConfirmState_RuleParameters = null;
var ObjDateRangeParametersValue_RuleParameters = '';
var selectedDataRangeID_RuleParameters = '-1';
var ObjRuleParameter_RuleParameters = null;

function GetBoxesHeaders_RuleParameters() {
    parent.document.getElementById('Title_DialogRuleParameters').innerHTML = document.getElementById('hfTitle_DialogRuleParameters').value;
    document.getElementById('header_RuleDateRanges_RuleParameters').innerHTML = document.getElementById('hfheader_RuleDateRanges_RuleParameters').value;
    document.getElementById('header_RuleParameters_RuleParameters').innerHTML = document.getElementById('hfheader_RuleParameters_RuleParameters').value;
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

function gdpDate_RuleParameters_OnDateChange(sender, e) {
    var Date = gdpDate_RuleParameters.getSelectedDate();
    gCalDate_RuleParameters.setSelectedDate(Date);
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

function gCalDate_RuleParameters_OnChange(sender, e) {
    var Date = gCalDate_RuleParameters.getSelectedDate();
    gdpDate_RuleParameters.setSelectedDate(Date);
}

function gCalDate_RuleParameters_onLoad(sender, e) {
    window.gCalDate_RuleParameters.PopUpObject.z = 25000000;
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

function SetButtonImages_TimeSelector_RuleParameters() {
    document.getElementById('TimeSelector_Hour_RuleParameters_imgUp').src = "images/TimeSelector/CustomUp.gif";
    document.getElementById('TimeSelector_Hour_RuleParameters_imgDown').src = "images/TimeSelector/CustomDown.gif";
    document.getElementById('TimeSelector_Hour_RuleParameters_imgUp').onmouseover = function () {
        document.getElementById('TimeSelector_Hour_RuleParameters_imgUp').src = "images/TimeSelector/oie_CustomUp.gif";
        FocusOnCurrentTimeSelector('TimeSelector_Hour_RuleParameters');
    };
    document.getElementById('TimeSelector_Hour_RuleParameters_imgDown').onmouseover = function () {
        document.getElementById('TimeSelector_Hour_RuleParameters_imgDown').src = "images/TimeSelector/oie_CustomDown.gif";
        FocusOnCurrentTimeSelector('TimeSelector_Hour_RuleParameters');
    };
    document.getElementById('TimeSelector_Hour_RuleParameters_imgUp').onmouseout = function () {
        document.getElementById('TimeSelector_Hour_RuleParameters_imgUp').src = "images/TimeSelector/CustomUp.gif";
    };
    document.getElementById('TimeSelector_Hour_RuleParameters_imgDown').onmouseout = function () {
        document.getElementById('TimeSelector_Hour_RuleParameters_imgDown').src = "images/TimeSelector/CustomDown.gif";
    };
}   


function FocusOnCurrentTimeSelector(TimeSelector) {
    if (document.activeElement.id != "" + TimeSelector + "_txtHour" && document.activeElement.id != "" + TimeSelector + "_txtMinute" && document.activeElement.id != "" + TimeSelector + "_txtSecond" && !document.getElementById("" + TimeSelector + "_txtHour").disabled)
        document.getElementById("" + TimeSelector + "_txtHour").focus();
}

function btn_gdpFromDate_RuleParameters_OnMouseUp(event) {
    if (gCalFromDate_RuleParameters.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpFromDate_RuleParameters_OnDateChange(sender, e) {
    var FromDate = gdpFromDate_RuleParameters.getSelectedDate();
    gCalFromDate_RuleParameters.setSelectedDate(FromDate);
}

function btn_gdpFromDate_RuleParameters_OnClick(event) {
    if (gCalFromDate_RuleParameters.get_popUpShowing()) {
        gCalFromDate_RuleParameters.hide();
    }
    else {
        gCalFromDate_RuleParameters.setSelectedDate(gdpFromDate_RuleParameters.getSelectedDate());
        gCalFromDate_RuleParameters.show();
    }
}

function gCalFromDate_RuleParameters_OnChange(sender, e) {
    var FromDate = gCalFromDate_RuleParameters.getSelectedDate();
    gdpFromDate_RuleParameters.setSelectedDate(FromDate);
}

function gCalFromDate_RuleParameters_onLoad(sender, e) {
    window.gCalFromDate_RuleParameters.PopUpObject.z = 25000000;
}

function btn_gdpToDate_RuleParameters_OnMouseUp(event) {
    if (gCalToDate_RuleParameters.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpToDate_RuleParameters_OnDateChange(sender, e) {
    var ToDate = gdpToDate_RuleParameters.getSelectedDate();
    gCalToDate_RuleParameters.setSelectedDate(ToDate);
}

function btn_gdpToDate_RuleParameters_OnClick(event) {
    if (gCalToDate_RuleParameters.get_popUpShowing()) {
        gCalToDate_RuleParameters.hide();
    }
    else {
        gCalToDate_RuleParameters.setSelectedDate(gdpToDate_RuleParameters.getSelectedDate());
        gCalToDate_RuleParameters.show();
    }
}

function gCalToDate_RuleParameters_OnChange(sender, e) {
    var ToDate = gCalToDate_RuleParameters.getSelectedDate();
    gdpToDate_RuleParameters.setSelectedDate(ToDate);
}

function gCalToDate_RuleParameters_onLoad(sender, e) {
    window.gCalToDate_RuleParameters.PopUpObject.z = 25000000;
}

function tlbItemNew_TlbRuleParameters_onClick() {
    ChangePageState_RuleParameters('Add');
    ClearList_RuleParameters();
}

function ClearList_RuleParameters() {
    ResetCalendars_RuleParameters();
    ResetTimepicker_RuleParameters('TimeSelector_Hour_RuleParameters');
    document.getElementById('txtNumeric_RuleParameters').value = '';
    ObjDateRangeParametersValue_RuleParameters = '';
    GridRuleDateRanges_RuleParameters.unSelectAll();
    GridRuleParameters_RuleParameters.unSelectAll();
}

function ResetTimepicker_RuleParameters(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    //document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}


function ChangePageState_RuleParameters(state) {
    SetActionMode_RuleParameters(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        CurrentPageState_RuleParameters = state;
        if (TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters').set_enabled(false);
            TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters').set_imageUrl('add_silver.png');
        }
        if (TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters').set_enabled(false);
            TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters').set_imageUrl('edit_silver.png');
        }
        if (TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters').set_enabled(false);
            TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters').set_imageUrl('remove_silver.png');
        }
        TlbRuleParameters.get_items().getItemById('tlbItemSave_TlbRuleParameters').set_enabled(true);
        TlbRuleParameters.get_items().getItemById('tlbItemSave_TlbRuleParameters').set_imageUrl('save.png');
        TlbRuleParameters.get_items().getItemById('tlbItemCancel_TlbRuleParameters').set_enabled(true);
        TlbRuleParameters.get_items().getItemById('tlbItemCancel_TlbRuleParameters').set_imageUrl('cancel.png');
        TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms').set_enabled(true);
        TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms').set_imageUrl('save.png');
        TlbConfirm_pgvTime_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms').set_enabled(true);
        TlbConfirm_pgvTime_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms').set_imageUrl('save.png');
        TlbConfirm_pgvDate_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms').set_enabled(true);
        TlbConfirm_pgvDate_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms').set_imageUrl('save.png');
        document.getElementById('txtNumeric_RuleParameters').disabled = '';
        ChangeCalendarsEnabled_RuleParameters('enable');
        ChangeTimePickerEnabled_RuleParameters('TimeSelector_Hour_RuleParameters', 'enable');
        if (state == 'Add')
            Fill_GridRuleParameters_RuleParameters(null);
        if (state == 'Edit')
            NavigateRuleDateRange_RuleParameters(GridRuleDateRanges_RuleParameters.getSelectedItems()[0]);
        if (state == 'Delete')
            RuleParameter_onSave();
    }
    if (state == 'View') {
        if(CurrentPageState_RuleParameters == 'Delete')
            Fill_GridRuleParameters_RuleParameters(null);
        CurrentPageState_RuleParameters = state;
        if (TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters').set_enabled(true);
            TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters').set_imageUrl('add.png');
        }
        if (TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters').set_enabled(true);
            TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters').set_imageUrl('edit.png');
        }
        if (TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters').set_enabled(true);
            TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters').set_imageUrl('remove.png');
        }
        TlbRuleParameters.get_items().getItemById('tlbItemSave_TlbRuleParameters').set_enabled(false);
        TlbRuleParameters.get_items().getItemById('tlbItemSave_TlbRuleParameters').set_imageUrl('save_silver.png');
        TlbRuleParameters.get_items().getItemById('tlbItemCancel_TlbRuleParameters').set_enabled(false);
        TlbRuleParameters.get_items().getItemById('tlbItemCancel_TlbRuleParameters').set_imageUrl('cancel_silver.png');
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

function RuleParameter_onSave() {
    if (CurrentPageState_RuleParameters != 'Delete')
        UpdateRuleParameter_RuleParameters();
    else
        ShowDialogConfirm('Delete');
}

function UpdateRuleParameter_RuleParameters() {
    ObjRuleParameter_RuleParameters = new Object();
    ObjRuleParameter_RuleParameters.ID = '0';
    ObjRuleParameter_RuleParameters.FromDate = null;
    ObjRuleParameter_RuleParameters.ToDate = null;
    var ObjDialogRuleParameters = parent.DialogRuleParameters.get_value();        

    var SelectedItems_GridRuleDateRanges_RuleParameters = GridRuleDateRanges_RuleParameters.getSelectedItems();
    if (SelectedItems_GridRuleDateRanges_RuleParameters.length > 0)
        ObjRuleParameter_RuleParameters.ID = SelectedItems_GridRuleDateRanges_RuleParameters[0].getMember("ID").get_text();
    var FromDate_RuleParameters = null;
    var ToDate_RuleParameters = null;
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            FromDate_RuleParameters = document.getElementById('pdpFromDate_RuleParameters').value;
            ToDate_RuleParameters = document.getElementById('pdpToDate_RuleParameters').value;
            break;
        case 'en-US':
            FromDate_RuleParameters = document.getElementById('gdpFromDate_RuleParameters_picker').value;
            ToDate_RuleParameters = document.getElementById('gdpToDate_RuleParameters_picker').value;
            break;
    }
    ObjRuleParameter_RuleParameters.FromDate = FromDate_RuleParameters;
    ObjRuleParameter_RuleParameters.ToDate = ToDate_RuleParameters;
    UpdateRuleParameter_RuleParametersPage(CharToKeyCode_RuleParameters(CurrentPageState_RuleParameters), CharToKeyCode_RuleParameters(ObjDialogRuleParameters.State), CharToKeyCode_RuleParameters(ObjDialogRuleParameters.RuleGroupID), CharToKeyCode_RuleParameters(ObjDialogRuleParameters.RuleID), CharToKeyCode_RuleParameters(ObjRuleParameter_RuleParameters.ID), CharToKeyCode_RuleParameters(ObjRuleParameter_RuleParameters.FromDate), CharToKeyCode_RuleParameters(ObjRuleParameter_RuleParameters.ToDate), CharToKeyCode_RuleParameters('[' + ObjDateRangeParametersValue_RuleParameters + ']'));
    DialogWaiting.Show();
}

function UpdateRuleParameter_RuleParametersPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_RuleParameters').value;
            Response[1] = document.getElementById('hfConnectionError_RuleParameters').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            RuleDateRange_OnAfterUpdate(Response);
            ClearList_RuleParameters();
            ChangePageState_RuleParameters('View');
        }
        else {
            if (CurrentPageState_RuleParameters == 'Delete')
                ChangePageState_RuleParameters('View');
        }
    }
}

function RuleParameter_onAfterUpdate(Response) {
    var RetMessage = Response;
    if (RetMessage[2] == 'success') {
        if (CurrentPageState_RuleParameters == 'Delete') {
            GridRuleParameters_RuleParameters.beginUpdate();
            GridRuleParameters_RuleParameters.get_table().clearData();
            GridRuleParameters_RuleParameters.endUpdate();
        }
    }

}

function RuleDateRange_OnAfterUpdate(Response) {
    if (ObjRuleParameter_RuleParameters != null) {
        var FromDate = ObjRuleParameter_RuleParameters.FromDate;
        var ToDate = ObjRuleParameter_RuleParameters.ToDate;

        var RuleDateRangeItem = null;

        GridRuleDateRanges_RuleParameters.beginUpdate();
        switch (CurrentPageState_RuleParameters) {
            case 'Add':
                RuleDateRangeItem = GridRuleDateRanges_RuleParameters.get_table().addEmptyRow(GridRuleDateRanges_RuleParameters.get_recordCount());
                RuleDateRangeItem.setValue(0, Response[3], false);
                GridRuleDateRanges_RuleParameters.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridRuleDateRanges_RuleParameters.selectByKey(Response[3], 0, false);
                RuleDateRangeItem = GridRuleDateRanges_RuleParameters.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridRuleDateRanges_RuleParameters.selectByKey(ObjRuleParameter_RuleParameters.ID, 0, false);
                GridRuleDateRanges_RuleParameters.deleteSelected();
                break;
        }
        if (CurrentPageState_RuleParameters != 'Delete') {
            RuleDateRangeItem.setValue(1, FromDate, false);
            RuleDateRangeItem.setValue(2, ToDate, false);
        }
        GridRuleDateRanges_RuleParameters.endUpdate();
    }
}

function NavigateRuleDateRange_RuleParameters(selectedRuleDateRange) {
    if (selectedRuleDateRange != undefined) {
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpFromDate_RuleParameters').value = selectedRuleDateRange.getMember('FromDate').get_text();
                document.getElementById('pdpToDate_RuleParameters').value = selectedRuleDateRange.getMember('ToDate').get_text();
                break;
            case 'en-US':
                var gFromDate = new Date(selectedRuleDateRange.getMember('FromDate').get_text());
                gdpFromDate_RuleParameters.setSelectedDate(gFromDate);
                gCalFromDate_RuleParameters.setSelectedDate(gFromDate);
                var gToDate = new Date(selectedRuleDateRange.getMember('ToDate').get_text());
                gdpToDate_RuleParameters.setSelectedDate(gToDate);
                gCalToDate_RuleParameters.setSelectedDate(gToDate);
                break;
        }
        Fill_GridRuleParameters_RuleParameters(selectedRuleDateRange.getMember('ID').get_text());
    }
}

function ChangeTimePickerEnabled_RuleParameters(TimeSelector, state) {
    var disabled = null;
    switch (state) {
        case 'disable':
            disabled = 'disabled';
//            document.getElementById("" + TimeSelector + "_imgUp").onclick = " ";
//            document.getElementById("" + TimeSelector + "_imgDown").onclick = " ";
            break;
        case 'enable':
            disabled = '';
//            document.getElementById("" + TimeSelector + "_imgUp").onclick = function () {
//                addTime(document.getElementById("" + TimeSelector + "_imgUp"), 24, 1, 1);
//            }
//            document.getElementById("" + TimeSelector + "_imgDown").onclick = function () {
//                subtractTime(document.getElementById("" + TimeSelector + "_imgDown"), 24, 1, 1);
//            }
            break;
    }
    document.getElementById("" + TimeSelector + "_txtHour").disabled = disabled;
    //document.getElementById("" + TimeSelector + "_txtHour").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").disabled = disabled;
    //document.getElementById("" + TimeSelector + "_txtMinute").nextSibling.disabled = disabled;
    //document.getElementById("" + TimeSelector + "_txtSecond").disabled = disabled;
}


function ChangeCalendarsEnabled_RuleParameters(state) {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ChangeCalendarEnabled_RuleParameters('pdpFromDate_RuleParameters', state);
            ChangeCalendarEnabled_RuleParameters('pdpToDate_RuleParameters', state);
            ChangeCalendarEnabled_RuleParameters('pdpDate_RuleParameters', state);
            break;
        case 'en-US':
            ChangeCalendarEnabled_RuleParameters('gdpFromDate_RuleParameters', state);
            ChangeCalendarEnabled_RuleParameters('gdpToDate_RuleParameters', state);
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

function CalendarsViewManage_RuleParameters(gdpID) {
    var CalID_RuleParameters = 'gCal' + gdpID.substr(3, gdpID.length - 3);
    var Cal_RuleParameters = eval(CalID_RuleParameters);
    if (!Cal_RuleParameters.get_popUpShowing())
        Cal_RuleParameters.show();
    else
        Cal_RuleParameters.hide();
}

function tlbItemEdit_TlbRuleParameters_onClick() {
    ChangePageState_RuleParameters('Edit');
}

function tlbItemDelete_TlbRuleParameters_onClick() {
    ChangePageState_RuleParameters('Delete');
}

function tlbItemSave_TlbRuleParameters_onClick() {
    RuleParameter_onSave();
}

function tlbItemCancel_TlbRuleParameters_onClick() {
    ChangePageState_RuleParameters('View');
    ClearList_RuleParameters();
}

function tlbItemExit_TlbRuleParameters_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_RuleParameters = confirmState;
    if (CurrentPageState_RuleParameters == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_RuleParameters').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_RuleParameters').value;
    DialogConfirm.Show();
}


function CharToKeyCode_RuleParameters(str) {
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

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_RuleParameters) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateRuleParameter_RuleParameters();
            break;
        case 'Exit':
            ClearList_RuleParameters();
            CloseDialogRuleParameters();
            break;
    }
}

function CloseDialogRuleParameters() {
    parent.document.getElementById('DialogRuleParameters_IFrame').src = "WhitePage.aspx";
    parent.DialogRuleParameters.Close();
}


function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_RuleParameters('View');
}

function GridRuleDateRanges_RuleParameters_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridRuleDateRanges_RuleParameters').innerHTML = '';
}

function GridRuleDateRanges_RuleParameters_onItemSelect(sender, e) {
    var ruleDateRangeID = e.get_item().getMember('ID').get_text();
    if (selectedDataRangeID_RuleParameters != ruleDateRangeID) {
        ClearParametersList_RuleParameters();
        selectedDataRangeID_RuleParameters = ruleDateRangeID;
    }
    if (CurrentPageState_RuleParameters != 'Add')
        NavigateRuleDateRange_RuleParameters(e.get_item());
}

function CallBack_GridRuleDateRanges_RuleParameters_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RuleDateRanges_RuleParameters').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridRuleDateRanges_RuleParameters();
    }
}

function Fill_GridRuleDateRanges_RuleParameters() {
    var ObjDialogRuleParameters = parent.DialogRuleParameters.get_value();
    document.getElementById('loadingPanel_GridRuleDateRanges_RuleParameters').innerHTML = document.getElementById('hfloadingPanel_GridRuleDateRanges_RuleParameters').value;
    CallBack_GridRuleDateRanges_RuleParameters.callback(ObjDialogRuleParameters.State, ObjDialogRuleParameters.RuleGroupID, ObjDialogRuleParameters.RuleID);
}

function Refresh_GridRuleDateRanges_RuleParameters() {
    Fill_GridRuleDateRanges_RuleParameters();
}

function GridRuleParameters_RuleParameters_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridRuleParameters_RuleParameters').value = '';
}

function GridRuleParameters_RuleParameters_onItemSelect(sender, e) {
    NavligateRuleParameter_RuleParameters(e.get_item());
}

function NavligateRuleParameter_RuleParameters(selectedRuleParameter) {
    if (selectedRuleParameter != undefined) {
        document.getElementById('txtParameterTitle_RuleParameters').value = selectedRuleParameter.getMember('Title').get_text();
        var ruleParameterValue = selectedRuleParameter.getMember('Value').get_text();
        switch (selectedRuleParameter.getMember('Type').get_text()) {
            case '0':
                TabStripRuleParametersTerms.selectTabById('tbNumeric_TabStripRuleParametersTerms');
                if(CurrentPageState_RuleParameters != 'Add')
                   document.getElementById('txtNumeric_RuleParameters').value = ruleParameterValue;
                break;
            case '1':
                TabStripRuleParametersTerms.selectTabById('tbTime_TabStripRuleParametersTerms');
                var TimeValue = '';
                if (CurrentPageState_RuleParameters != 'Add') {
                    var IsParameterValueInNextDay = selectedRuleParameter.getMember('IsInNextDay').get_value();
                    if (IsParameterValueInNextDay) 
                        TimeValue = ruleParameterValue.replace('+', '');
                    else 
                        TimeValue = ruleParameterValue;
                    document.getElementById('chbNextDay_RuleParameters').checked = IsParameterValueInNextDay;
                    SetDuration_TimePicker_RuleParameters('TimeSelector_Hour_RuleParameters', TimeValue);
                }
                break;
            case '2':
                TabStripRuleParametersTerms.selectTabById('tbDate_TabStripRuleParametersTerms');
                ruleParameterValue = new Date(ruleParameterValue);
                if (CurrentPageState_RuleParameters != 'Add') {
                    gdpDate_RuleParameters.setSelectedDate(ruleParameterValue);
                    gCalDate_RuleParameters.setSelectedDate(ruleParameterValue);
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


function CallBack_GridRuleParameters_RuleParameters_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RuleParameters_RuleParameters').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Refresh_GridRuleParameters_RuleParameters();
    }
    else {
        if (GridRuleDateRanges_RuleParameters.getSelectedItems().length > 0 && GridRuleParameters_RuleParameters.get_table().getRowCount() > 0) {
            var ruleParameterItem = GridRuleParameters_RuleParameters.get_table().getRow(0);
            GridRuleParameters_RuleParameters.selectByKey(ruleParameterItem.getMember('ID').get_text(), 0, false);
            NavligateRuleParameter_RuleParameters(ruleParameterItem);
        }
           
    }
}

function Refresh_GridRuleParameters_RuleParameters() {
    var ruleDateRangeID = null;
    if (GridRuleDateRanges_RuleParameters.get_table().getRowCount() > 0) {
        if (GridRuleDateRanges_RuleParameters.getSelectedItems().length  > 0)
            ruleDateRangeID = GridRuleDateRanges_RuleParameters.getSelectedItems()[0].getMember('ID').get_text();
        else
            ruleDateRangeID = GridRuleDateRanges_RuleParameters.get_table().getRow(0).getMember('ID').get_text();
        Fill_GridRuleParameters_RuleParameters(ruleDateRangeID);
    }
}

function tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms_onClick() {
    if (GridRuleParameters_RuleParameters.getSelectedItems().length > 0) {
        var NumericValue_RuleParameters = document.getElementById('txtNumeric_RuleParameters').value;
        if (!isNaN(parseInt(NumericValue_RuleParameters)))
            UpdateDateRangeRuleParametersList_RuleParameters(NumericValue_RuleParameters);
    }
}

function tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms_onClick() {
    if (GridRuleParameters_RuleParameters.getSelectedItems().length > 0) {
        var TimeValue_RuleParametes = GetDuration_TimePicker_RuleParameters('TimeSelector_Hour_RuleParameters');
        if (document.getElementById('chbNextDay_RuleParameters').checked)
            TimeValue_RuleParametes = '+' + TimeValue_RuleParametes;
        UpdateDateRangeRuleParametersList_RuleParameters(TimeValue_RuleParametes);
    }
}

function tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms_onClick() {
    if (GridRuleParameters_RuleParameters.getSelectedItems().length > 0) {
        var DateValue_RuleParametes = document.getElementById('gdpDate_RuleParameters_picker').value;
        UpdateDateRangeRuleParametersList_RuleParameters(DateValue_RuleParametes);
    }
}

function UpdateDateRangeRuleParametersList_RuleParameters(ruleParameterValue) {
    var selectedRuleParameterID = GridRuleParameters_RuleParameters.getSelectedItems()[0].getMember('ID').get_text();
    var selectedRuleParameterName = GridRuleParameters_RuleParameters.getSelectedItems()[0].getMember('Name').get_text();
    var selectedRuleParameterTitle = GridRuleParameters_RuleParameters.getSelectedItems()[0].getMember('Title').get_text();
    var selectedRuleParameterType = GridRuleParameters_RuleParameters.getSelectedItems()[0].getMember('Type').get_text();
    var selectedRuleParameterValue = GridRuleParameters_RuleParameters.getSelectedItems()[0].getMember('Value').get_text();
    var NewParameterValueMember_RuleParameters = '{"ID":"' + selectedRuleParameterID + '","Name":"' + selectedRuleParameterName + '","Title":"' + selectedRuleParameterTitle + '","Type":"' + selectedRuleParameterType + '","Value":"' + ruleParameterValue + '"}';
    var OldParameterValueMember_RuleParameters = '{"ID":"' + selectedRuleParameterID + '","Name":"' + selectedRuleParameterName + '","Title":"' + selectedRuleParameterTitle + '","Type":"' + selectedRuleParameterType + '","Value":"' + selectedRuleParameterValue + '"}';
    if (ObjDateRangeParametersValue_RuleParameters != '' && ObjDateRangeParametersValue_RuleParameters.indexOf(OldParameterValueMember_RuleParameters) >= 0)
        ObjDateRangeParametersValue_RuleParameters = ObjDateRangeParametersValue_RuleParameters.replace(OldParameterValueMember_RuleParameters, '');
    var splitter = '';
    if (ObjDateRangeParametersValue_RuleParameters != '')
        splitter = ',';
    ObjDateRangeParametersValue_RuleParameters += splitter + NewParameterValueMember_RuleParameters;
    if (ObjDateRangeParametersValue_RuleParameters[0] == ',')
        ObjDateRangeParametersValue_RuleParameters = ObjDateRangeParametersValue_RuleParameters.substring(1, ObjDateRangeParametersValue_RuleParameters.length);
    if (ObjDateRangeParametersValue_RuleParameters[ObjDateRangeParametersValue_RuleParameters.length - 1] == ',')
        ObjDateRangeParametersValue_RuleParameters = ObjDateRangeParametersValue_RuleParameters.substring(0, ObjDateRangeParametersValue_RuleParameters.length - 1);
    UpdateGridItem_GridRuleParameters_RuleParameters(ruleParameterValue);
}

function UpdateGridItem_GridRuleParameters_RuleParameters(newRuleParameterVal) {
    GridRuleParameters_RuleParameters.beginUpdate();
    GridRuleParameters_RuleParameters.getSelectedItems()[0].setValue(2, newRuleParameterVal, false);
    GridRuleParameters_RuleParameters.endUpdate();
}

function ClearParametersList_RuleParameters() {
    var currentDate_RuleParameters = document.getElementById('hfCurrentDate_RuleParameters').value;
    ResetTimepicker_RuleParameters('TimeSelector_Hour_RuleParameters');
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpDate_RuleParameters').value = currentDate_RuleParameters;
            break;
        case 'en-US':
            currentDate_RuleParameters = new Date(currentDate_RuleParameters);
            gdpDate_RuleParameters.setSelectedDate(currentDate_RuleParameters);
            gCalDate_RuleParameters.setSelectedDate(currentDate_RuleParameters);
            break;
    }
    document.getElementById('txtNumeric_RuleParameters').value = '';
    ObjDateRangeParametersValue_RuleParameters = '' ;
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

function Fill_GridRuleParameters_RuleParameters(ruleDateRangeID) {
    document.getElementById('header_RuleParameters_RuleParameters').innerHTML = document.getElementById('hfheader_RuleParameters_RuleParameters').value;
    var ObjDialogRuleParameters = parent.DialogRuleParameters.get_value();
    if (ruleDateRangeID == null)
        CallBack_GridRuleParameters_RuleParameters.callback(ObjDialogRuleParameters.RuleGroupID, ObjDialogRuleParameters.RuleID);
    else
        CallBack_GridRuleParameters_RuleParameters.callback(ObjDialogRuleParameters.RuleGroupID, ObjDialogRuleParameters.RuleID, ruleDateRangeID);
}

function ResetCalendars_RuleParameters() {
    var currentDate_RuleParameters = document.getElementById('hfCurrentDate_RuleParameters').value;
    switch (parent.parent.SysLangID) {
        case 'en-US':
            currentDate_RuleParameters = new Date(currentDate_RuleParameters);
            gdpFromDate_RuleParameters.setSelectedDate(currentDate_RuleParameters);
            gCalFromDate_RuleParameters.setSelectedDate(currentDate_RuleParameters);
            gdpToDate_RuleParameters.setSelectedDate(currentDate_RuleParameters);
            gCalToDate_RuleParameters.setSelectedDate(currentDate_RuleParameters);
            gdpDate_RuleParameters.setSelectedDate(currentDate_RuleParameters);
            gCalDate_RuleParameters.setSelectedDate(currentDate_RuleParameters);
            break;
        case 'fa-IR':
            document.getElementById('pdpFromDate_RuleParameters').value = currentDate_RuleParameters;
            document.getElementById('pdpToDate_RuleParameters').value = currentDate_RuleParameters;
            document.getElementById('pdpDate_RuleParameters').value = currentDate_RuleParameters;
            break;
    }
}

function SetActionMode_RuleParameters(state) {
    document.getElementById('ActionMode_RuleParameters').innerHTML = document.getElementById("hf" + state + "_RuleParameters").value;
}

function initTimePicker_RuleParameters() {
    var disable = '';
    SetButtonImages_TimeSelector_RuleParameters();
    ChangeFloat_TimeSelectors_RuleParameters();
    ChangeTimePickerEnabled_RuleParameters('TimeSelector_Hour_RuleParameters', 'disable');
    ResetTimepicker_RuleParameters('TimeSelector_Hour_RuleParameters');
}

function ChangeFloat_TimeSelectors_RuleParameters() {
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            document.getElementById('TimeSelector_Hour_RuleParameters').style.styleFloat = 'right';
            document.getElementById('TimeSelector_Hour_RuleParameters').style.cssFloat = 'right';
            break;
        case 'en-US':
            document.getElementById('TimeSelector_Hour_RuleParameters').style.styleFloat = 'left';
            document.getElementById('TimeSelector_Hour_RuleParameters').style.cssFloat = 'left';
            break;
    }
}

function CallBack_GridRuleDateRanges_RuleParameters_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridRuleDateRanges_RuleParameters').innerHTML = '';
    ShowConnectionError_RuleParameters();
}

function ShowConnectionError_RuleParameters() {
    var error = document.getElementById('hfErrorType_RuleParameters').value;
    var errorBody = document.getElementById('hfConnectionError_RuleParameters').value;
    showDialog(error, errorBody, 'error');
}

function TimeSelector_Hour_RuleParameters_onChange(partID) {
    var id = 'TimeSelector_Hour_RuleParameters_' + partID;
    var val = document.getElementById(id).value;
    val = !isNaN(parseFloat(val)) ? parseFloat(val) > 0 ? ""+parseFloat(val)+"" : '00' : '00';
    switch (partID) {
        case 'txtHour':
            break;
        case 'txtMinute':
            val = parseFloat(val) < 60 ? val : '59';
            break;
    }
    document.getElementById(id).value = val.length >= 2 ? val : '0' + val;
}

function CallBack_GridRuleParameters_RuleParameters_onCallbackError(sender, e) {
    ShowConnectionError_RuleParameters();
}

function tlbItemFormReconstruction_TlbRuleParameters_onClick() {
    CloseDialogRuleParameters();
    parent.parent.document.getElementById('DialogRulesGroupUpdate_IFrame').contentWindow.ShowDialogRuleParameters();
}

function tlbItemHelp_TlbRuleParameters_onClick() {
    LoadHelpPage('tlbItemHelp_TlbRuleParameters');
}

function NavigateRuleFeatures_RuleParameters() {
    var ObjDialogRuleParameters = parent.DialogRuleParameters.get_value();
    document.getElementById('txtRuleTitle_RuleParameters').value = ObjDialogRuleParameters.RuleTitle;
}





