
var CurrentPageState_MasterTrafficsControl = 'View';
var ConfirmState_MasterTrafficsControl = null;
var LoadState_cmbPersonnel_MasterTrafficsControl = 'Normal';
var AdvancedSearchTerm_cmbPersonnel_MasterTrafficsControl = '';
var CurrentPageCombosCallBcakStateObj = new Object();
var CurrentPageIndex_cmbPersonnel_MasterTrafficsControl = 0;
var ObjTraffic_MasterTrafficsControl = null;
var zeroTime = '00';
var NullTime_RequestRegister = '';

function btn_gdpDate_MasterTrafficsControl_OnMouseUp(event) {
    if (gCalDate_MasterTrafficsControl.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function btn_gdpDate_MasterTrafficsControl_OnClick(event) {
    if (gCalDate_MasterTrafficsControl.get_popUpShowing()) {
        gCalDate_MasterTrafficsControl.hide();
    }
    else {
        gCalDate_MasterTrafficsControl.setSelectedDate(gdpDate_MasterTrafficsControl.getSelectedDate());
        gCalDate_MasterTrafficsControl.show();
    }
}

function gdpDate_MasterTrafficsControl_OnDateChange(sender, e) {
    var Date = gdpDate_MasterTrafficsControl.getSelectedDate();
    gCalDate_MasterTrafficsControl.setSelectedDate(Date);
}

function gCalDate_MasterTrafficsControl_OnChange(sender, e) {
    var Date = gCalDate_MasterTrafficsControl.getSelectedDate();
    gdpDate_MasterTrafficsControl.setSelectedDate(Date);
}

function gCalDate_MasterTrafficsControl_OnLoad(sender, e) {
    window.gCalDate_MasterTrafficsControl.PopUpObject.z = 25000000;
}

function GetBoxesHeaders_MasterTrafficsControl() {
    document.getElementById('header_Traffics_MasterTrafficsControl').innerHTML = document.getElementById('hfheader_Traffics_MasterTrafficsControl').value;
    document.getElementById('clmnName_cmbPersonnel_MasterTrafficsControl').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_MasterTrafficsControl').value;
    document.getElementById('clmnBarCode_cmbPersonnel_MasterTrafficsControl').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_MasterTrafficsControl').value;
    document.getElementById('clmnCardNum_cmbPersonnel_MasterTrafficsControl').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_MasterTrafficsControl').value;
    document.getElementById('cmbPrecards_MasterTrafficsControl_Input').value = document.getElementById('hfcmbAlarm_cmbPrecards_MasterTrafficsControl').value;
}

function ViewCurrentLangCalendars_MasterTrafficsControl() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpDate_MasterTrafficsControl").parentNode.removeChild(document.getElementById("pdpDate_MasterTrafficsControl"));
            document.getElementById("pdpDate_MasterTrafficsControlimgbt").parentNode.removeChild(document.getElementById("pdpDate_MasterTrafficsControlimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_DateCalendars_MasterTrafficsControl").removeChild(document.getElementById("Container_gCalDate_MasterTrafficsControl"));
            break;
    }
}

function ResetCalendar_MasterTrafficsControl() {
    var currentDate_MasterTrafficsControl = document.getElementById('hfCurrentDate_MasterTrafficsControl').value;
    switch (parent.parent.SysLangID) {
        case 'en-US':
            currentDate_MasterTrafficsControl = new Date(currentDate_MasterTrafficsControl);
            gdpDate_MasterTrafficsControl.setSelectedDate(currentDate_MasterTrafficsControl);
            gCalDate_MasterTrafficsControl.setSelectedDate(currentDate_MasterTrafficsControl);
            break;
        case 'fa-IR':
            document.getElementById('pdpDate_MasterTrafficsControl').value = currentDate_MasterTrafficsControl;
            break;
    }
}

function ViewCurrentLangCalendars_MasterTrafficsControl() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpDate_MasterTrafficsControl").parentNode.removeChild(document.getElementById("pdpDate_MasterTrafficsControl"));
            document.getElementById("pdpDate_MasterTrafficsControlimgbt").parentNode.removeChild(document.getElementById("pdpDate_MasterTrafficsControlimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_DateCalendars_MasterTrafficsControl").removeChild(document.getElementById("Container_gCalDate_MasterTrafficsControl"));
            break;
    }
}

function Init_TimeSelectors_MasterTrafficsControl() {
    FetchRelativeOperation_TimePickers_MasterTrafficsControl('Reset');
    FetchRelativeOperation_TimePickers_MasterTrafficsControl('ChangeFloat');
    FetchRelativeOperation_TimePickers_MasterTrafficsControl('ChangeButtonImage');
    FetchRelativeOperation_TimePickers_MasterTrafficsControl('ChangeAction');
}

function FetchRelativeOperation_TimePickers_MasterTrafficsControl(ActionType) {
    var RelativeOperation = null;
    switch (ActionType) {
        case 'Reset':
            RelativeOperation = 'ResetTimepicker_MasterTrafficsControl';
            break;
        case 'ChangeFloat':
            RelativeOperation = 'ChangeFloat_TimeSelector_MasterTrafficsControl';
            break;
        case 'ChangeButtonImage':
            RelativeOperation = 'SetButtonImages_TimeSelector_MasterTrafficsControl';
            break;
        case 'ChangeAction':
            RelativeOperation = 'ChangeTimePickerActions_TimeSelector_MasterTrafficsControl';
            break;
    }
    eval(RelativeOperation + '("TimeSelector_Time_MasterTrafficsControl")');
}

function ChangeTimePickerActions_TimeSelector_MasterTrafficsControl(TimeSelector) {
    document.getElementById("" + TimeSelector + "_txtHour").onchange = function () {
        TimeSelector_MasterTrafficsControl_onChange(TimeSelector, '_txtHour');
    };
    document.getElementById("" + TimeSelector + "_txtMinute").onchange = function () {
        TimeSelector_MasterTrafficsControl_onChange(TimeSelector, '_txtMinute');
    };
}

function TimeSelector_MasterTrafficsControl_onChange(TimeSelector, partID) {
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

function ResetTimepicker_MasterTrafficsControl(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}

function ChangeFloat_TimeSelector_MasterTrafficsControl(TimeSelector) {
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

function SetButtonImages_TimeSelector_MasterTrafficsControl(TimeSelector) {
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

function tlbItemNew_TlbMasterTrafficsControl_onClick() {
    ChangePageState_MasterTrafficsControl('Add');
    ShowDialogTrafficsControl();
}

function tlbItemDelete_TlbMasterTrafficsControl_onClick() {
    ChangePageState_MasterTrafficsControl('Delete');
}

function ShowDialogTrafficsControl() {
    CollapseControls_MasterTrafficsControl();
    DialogTrafficsControl.Show();
}

function ClearList_MasterTrafficsControl() {
    cmbPrecards_MasterTrafficsControl.unSelect();
    document.getElementById('cmbPrecards_MasterTrafficsControl_Input').value = document.getElementById('hfcmbAlarm_cmbPrecards_MasterTrafficsControl').value;
    ResetCalendar_MasterTrafficsControl();
    ResetTimepicker_MasterTrafficsControl('TimeSelector_Time_MasterTrafficsControl');
    document.getElementById('txtDescription_MasterTrafficsControl').value = '';
}

function ChangePageState_MasterTrafficsControl(state) {
    CurrentPageState_MasterTrafficsControl = state;
    SetActionMode_MasterTrafficsControl(state);
    if (state == 'Add' || state == 'Delete') {
        if (state == 'Delete')
            Traffic_onSave();
    }
}

function Traffic_onSave() {
    if (CurrentPageState_MasterTrafficsControl != 'Delete')
        UpdateTraffic_MasterTrafficsControl();
    else
        ShowDialogConfirm('Delete');    
}

function GetSelectedTraffic_MasterTrafficsControl() {
}

function SetActionMode_MasterTrafficsControl(state) {
    document.getElementById('ActionMode_MasterTrafficsControl').innerHTML = document.getElementById("hf" + state + "_MasterTrafficsControl").value;
}

function tlbItemHelp_TlbMasterTrafficsControl_onClick() {
    LoadHelpPage('tlbItemHelp_TlbMasterTrafficsControl');
}

function tlbItemCalculation_TlbTlbMasterTrafficsControl_onClick() {
    Calculate_MasterTrafficsControl();
}

function CallBack_bulletedListCalculation_MasterTrafficsControl_onCallbackComplete(sender, e) {
    document.getElementById('tdCalculationing_MasterTrafficsControl').innerHTML = '';
    var error = document.getElementById('ErrorHiddenField_CalculationResult').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}

function CallBack_bulletedListCalculation_MasterTrafficsControl_onCallbackError(sender, e) {
    ShowConnectionError_MasterTrafficsControl();
}

function Calculate_MasterTrafficsControl() {
    document.getElementById('tdCalculationing_MasterTrafficsControl').innerHTML = document.getElementById('hfCalculationing_MasterTrafficsControl').value;
    var SelectedItem_GridTraffics_MasterTrafficsControl = document.getElementById('Traffics_iFrame').contentWindow.GetSelectedItemObj_GridTraffics_TrafficsControl();
    if (SelectedItem_GridTraffics_MasterTrafficsControl != undefined && SelectedItem_GridTraffics_MasterTrafficsControl.Level == 0) {
        var personnelID = '0';
        if (cmbPersonnel_MasterTrafficsControl.getSelectedItem() != undefined)
            personnelID = cmbPersonnel_MasterTrafficsControl.getSelectedItem().get_id();
        var date = SelectedItem_GridTraffics_MasterTrafficsControl.Obj.getMember('Date').get_text();
        CallBack_bulletedListCalculation_MasterTrafficsControl.callback(CharToKeyCode_MasterTrafficsControl(personnelID), CharToKeyCode_MasterTrafficsControl(date));
    }
}

function tlbItemFormReconstruction_TlbMasterTrafficsControl_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvTrafficsControl_iFrame').src = 'MasterTrafficsControl.aspx';
}

function tlbItemExit_TlbMasterTrafficsControl_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_MasterTrafficsControl = confirmState;
    if (CurrentPageState_MasterTrafficsControl == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_MasterTrafficsControl').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_MasterTrafficsControl').value;
    DialogConfirm.Show();
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_MasterTrafficsControl_onClick() {
    LoadState_cmbPersonnel_MasterTrafficsControl = 'Normal';
    SetPageIndex_cmbPersonnel_MasterTrafficsControl(0);
}

function tlbItemFirst_TlbPaging_PersonnelSearch_MasterTrafficsControl_onClick() {
    SetPageIndex_cmbPersonnel_MasterTrafficsControl(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_MasterTrafficsControl_onClick() {
    if (CurrentPageIndex_cmbPersonnel_MasterTrafficsControl != 0) {
        CurrentPageIndex_cmbPersonnel_MasterTrafficsControl = CurrentPageIndex_cmbPersonnel_MasterTrafficsControl - 1;
        SetPageIndex_cmbPersonnel_MasterTrafficsControl(CurrentPageIndex_cmbPersonnel_MasterTrafficsControl);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_MasterTrafficsControl_onClick() {
    if (CurrentPageIndex_cmbPersonnel_MasterTrafficsControl < parseInt(document.getElementById('hfPersonnelPageCount_MasterTrafficsControl').value) - 1) {
        CurrentPageIndex_cmbPersonnel_MasterTrafficsControl = CurrentPageIndex_cmbPersonnel_MasterTrafficsControl + 1;
        SetPageIndex_cmbPersonnel_MasterTrafficsControl(CurrentPageIndex_cmbPersonnel_MasterTrafficsControl);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_MasterTrafficsControl_onClick() {
    SetPageIndex_cmbPersonnel_MasterTrafficsControl(parseInt(document.getElementById('hfPersonnelPageCount_MasterTrafficsControl').value) - 1);
}

function SetPageIndex_cmbPersonnel_MasterTrafficsControl(pageIndex) {
    CurrentPageIndex_cmbPersonnel_MasterTrafficsControl = pageIndex;
    Fill_cmbPersonnel_MasterTrafficsControl(pageIndex);
}

function Fill_cmbPersonnel_MasterTrafficsControl(pageIndex) {
    var SearchTerm = '';
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_MasterTrafficsControl').value);
    switch (LoadState_cmbPersonnel_MasterTrafficsControl) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm = document.getElementById('txtPersonnelSearch_MasterTrafficsControl').value;
            break;
        case 'AdvancedSearch':
            SearchTerm = AdvancedSearchTerm_cmbPersonnel_MasterTrafficsControl;
            break;
    }
    CallBack_cmbPersonnel_MasterTrafficsControl.callback(CharToKeyCode_MasterTrafficsControl(LoadState_cmbPersonnel_MasterTrafficsControl), CharToKeyCode_MasterTrafficsControl(pageSize.toString()), CharToKeyCode_MasterTrafficsControl(pageIndex.toString()), CharToKeyCode_MasterTrafficsControl(SearchTerm));
}

function MasterTrafficsControl_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_cmbPersonnel_MasterTrafficsControl = SearchTerm;
    SetPageIndex_cmbPersonnel_MasterTrafficsControl(0);
}

function cmbPersonnel_MasterTrafficsControl_onExpand(sender, e) {
    SetPosition_cmbPersonnel_MasterTrafficsControl();
    if (cmbPersonnel_MasterTrafficsControl.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_MasterTrafficsControl == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_MasterTrafficsControl = true;
        SetPageIndex_cmbPersonnel_MasterTrafficsControl(0);
    }
}

function CallBack_cmbPersonnel_MasterTrafficsControl_onBeforeCallback(sender, e) {
    cmbPersonnel_MasterTrafficsControl.dispose();
}

function CallBack_cmbPersonnel_MasterTrafficsControl_onCallBackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_MasterTrafficsControl').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_MasterTrafficsControl').value;
    document.getElementById('clmnBarCode_cmbPersonnel_MasterTrafficsControl').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_MasterTrafficsControl').value;
    document.getElementById('clmnCardNum_cmbPersonnel_MasterTrafficsControl').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_MasterTrafficsControl').value;

    ChangeDirection_cmbPersonnel_MasterTrafficsControl();

    var error = document.getElementById('ErrorHiddenField_Personnel_MasterTrafficsControl').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_MasterTrafficsControl_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_MasterTrafficsControl_DropImage').mousedown();
        else
            cmbPersonnel_MasterTrafficsControl.expand();
        SetPosition_cmbPersonnel_MasterTrafficsControl();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_MasterTrafficsControl_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CallBack_cmbPersonnel_MasterTrafficsControl_onCallbackError(sender, e) {
    ShowConnectionError_MasterTrafficsControl();
}

function tlbItemSearch_TlbSearchPersonnel_MasterTrafficsControl_onClick() {
    LoadState_cmbPersonnel_MasterTrafficsControl = 'Search';
    CurrentPageIndex_cmbPersonnel_MasterTrafficsControl = 0;
    SetPageIndex_cmbPersonnel_MasterTrafficsControl(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_MasterTrafficsControl_onClick() {
    LoadState_cmbPersonnel_MasterTrafficsControl = 'AdvancedSearch';
    CurrentPageIndex_cmbPersonnel_MasterTrafficsControl = 0;
    ShowDialogPersonnelSearch('MasterTrafficsControl');
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
}


function tlbItemView_TlbView_MasterTrafficsControl_onClick() {
    GetPersonnelTraffics_MasterTrafficsControl();
}

function cmbYear_MasterTrafficsControl_onChange(sender, e) {
    if (cmbYear_MasterTrafficsControl.getSelectedItem() != undefined)
        document.getElementById('hfCurrentYear_MasterTrafficsControl').value = cmbYear_MasterTrafficsControl.getSelectedItem().get_value();
}

function cmbMonth_MasterTrafficsControl_onChange(sender, e) {
    if (cmbMonth_MasterTrafficsControl.getSelectedItem() != undefined)
        document.getElementById('hfCurrentMonth_MasterTrafficsControl').value = cmbMonth_MasterTrafficsControl.getSelectedItem().get_value();
}

function GetPersonnelTraffics_MasterTrafficsControl() {
    document.getElementById('Traffics_iFrame').contentWindow.Fill_GridTraffics_TrafficsControl();
}

function Refresh_GridTraffics_MasterTrafficsControl() {
    GetPersonnelTraffics_MasterTrafficsControl();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    var role = DialogConfirm.get_value();
    switch (ConfirmState_MasterTrafficsControl) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateTraffic_MasterTrafficsControl();
            break;
        case 'Exit':
            ClearList_MasterTrafficsControl();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function UpdateTraffic_MasterTrafficsControl() {
    ObjTraffic_MasterTrafficsControl = new Object();
    ObjTraffic_MasterTrafficsControl.ID = '0';
    ObjTraffic_MasterTrafficsControl.PersonnelID = '0';
    ObjTraffic_MasterTrafficsControl.PracardID = '0';
    ObjTraffic_MasterTrafficsControl.Date = null;
    ObjTraffic_MasterTrafficsControl.Time = null;
    ObjTraffic_MasterTrafficsControl.Description = null;
    var SelectedItem_GridTraffics_MasterTrafficsControl = document.getElementById('Traffics_iFrame').contentWindow.GetSelectedItemObj_GridTraffics_TrafficsControl();
    if (SelectedItem_GridTraffics_MasterTrafficsControl != undefined && SelectedItem_GridTraffics_MasterTrafficsControl.Level == 1)
        ObjTraffic_MasterTrafficsControl.ID = SelectedItem_GridTraffics_MasterTrafficsControl.Obj.getMember("ID").get_text();

    if (CurrentPageState_MasterTrafficsControl != 'Delete') {
        if (cmbPersonnel_MasterTrafficsControl.getSelectedItem() != undefined)
            ObjTraffic_MasterTrafficsControl.PersonnelID = cmbPersonnel_MasterTrafficsControl.getSelectedItem().get_id();
        if (cmbPrecards_MasterTrafficsControl.getSelectedItem() != undefined)
            ObjTraffic_MasterTrafficsControl.PracardID = cmbPrecards_MasterTrafficsControl.getSelectedItem().get_value();
        ObjTraffic_MasterTrafficsControl.Time = GetDuration_TimePicker_MasterTrafficsControl('TimeSelector_Time_MasterTrafficsControl');
        ObjTraffic_MasterTrafficsControl.Description = document.getElementById('txtDescription_MasterTrafficsControl').value;
        switch (parent.SysLangID) {
            case 'fa-IR':
                ObjTraffic_MasterTrafficsControl.Date = document.getElementById('pdpDate_MasterTrafficsControl').value;
                break;
            case 'en-US':
                ObjTraffic_MasterTrafficsControl.Date = document.getElementById('gdpDate_MasterTrafficsControl_picker').value;
                break;
        }
    }
    UpdateTraffic_MasterTrafficsControlPage(CharToKeyCode_MasterTrafficsControl(CurrentPageState_MasterTrafficsControl), CharToKeyCode_MasterTrafficsControl(ObjTraffic_MasterTrafficsControl.ID), CharToKeyCode_MasterTrafficsControl(ObjTraffic_MasterTrafficsControl.PersonnelID), CharToKeyCode_MasterTrafficsControl(ObjTraffic_MasterTrafficsControl.PracardID), CharToKeyCode_MasterTrafficsControl(ObjTraffic_MasterTrafficsControl.Time), CharToKeyCode_MasterTrafficsControl(ObjTraffic_MasterTrafficsControl.Date), CharToKeyCode_MasterTrafficsControl(ObjTraffic_MasterTrafficsControl.Description));
    DialogWaiting.Show();
}

function UpdateTraffic_MasterTrafficsControlPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_MasterTrafficsControl').value;
            Response[1] = document.getElementById('hfConnectionError_MasterTrafficsControl').value;
        }
        showDialog(RetMessage[0], RetMessage[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_MasterTrafficsControl();
            GetPersonnelTraffics_MasterTrafficsControl();            
            if(CurrentPageState_MasterTrafficsControl != 'Add')
               ChangePageState_MasterTrafficsControl('View');
        }
        else {
            if (CurrentPageState_MasterTrafficsControl == 'Delete')
                ChangePageState_MasterTrafficsControl('View');
        }
    }
}

function GetTrafficsRelativeObj_MasterTrafficsControl() {
    var TrafficsRelativeObj = new Object();
    TrafficsRelativeObj.PersonnelID = '0';
    if (cmbPersonnel_MasterTrafficsControl.getSelectedItem() != undefined)
        TrafficsRelativeObj.PersonnelID = cmbPersonnel_MasterTrafficsControl.getSelectedItem().get_id();
    TrafficsRelativeObj.Year = document.getElementById('hfCurrentYear_MasterTrafficsControl').value;
    TrafficsRelativeObj.Month = document.getElementById('hfCurrentMonth_MasterTrafficsControl').value;
    return TrafficsRelativeObj;
}

function GetDuration_TimePicker_MasterTrafficsControl(TimePicker) {
    if (document.getElementById(TimePicker + '_txtHour').value.length < 2)
        document.getElementById(TimePicker + '_txtHour').value = '0' + document.getElementById(TimePicker + '_txtHour').value;
    if (document.getElementById(TimePicker + '_txtMinute').value.length < 2)
        document.getElementById(TimePicker + '_txtMinute').value = '0' + document.getElementById(TimePicker + '_txtMinute').value;
    return document.getElementById(TimePicker + '_txtHour').value + ':' + document.getElementById(TimePicker + '_txtMinute').value;
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_MasterTrafficsControl('View');
}

function tlbItemSave_TlbTrafficsControl_DialogTrafficsControl_MasterTrafficsControl_onClick() {
    CollapseControls_MasterTrafficsControl();
    Traffic_onSave();
}

function CollapseControls_MasterTrafficsControl() {
    cmbPersonnel_MasterTrafficsControl.collapse();
    cmbYear_MasterTrafficsControl.collapse();
    cmbMonth_MasterTrafficsControl.collapse();
    cmbPrecards_MasterTrafficsControl.collapse();
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpDate_MasterTrafficsControl');
}

function tlbItemCancel_TlbTrafficsControl_DialogTrafficsControl_MasterTrafficsControl_onClick() {
    Traffic_onCancel();
    CloseDialogTrafficsControl();
}

function CloseDialogTrafficsControl() {
    CollapseControls_MasterTrafficsControl();
    DialogTrafficsControl.Close();
}

function Traffic_onCancel() {
    ChangePageState_MasterTrafficsControl('View');
    ClearList_MasterTrafficsControl();
}

function cmbPrecards_MasterTrafficsControl_onExpand(sender, e) {
    if (cmbPrecards_MasterTrafficsControl.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPrecards_MasterTrafficsControl == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPrecards_MasterTrafficsControl = true;
        CallBack_cmbPrecards_MasterTrafficsControl.callback();
    }
}

function cmbPrecards_MasterTrafficsControl_onCollapse(sender, e) {
    if (cmbPrecards_MasterTrafficsControl.getSelectedItem() == undefined)
        document.getElementById('cmbPrecards_MasterTrafficsControl_Input').value = document.getElementById('hfcmbAlarm_cmbPrecards_MasterTrafficsControl').value;
}

function CallBack_cmbPrecards_MasterTrafficsControl_onBeforeCallback(sender, e) {
    cmbPrecards_MasterTrafficsControl.dispose();
}

function CallBack_cmbPrecards_MasterTrafficsControl_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_TrafficType').value;
    ChangeDirection_cmbPrecards_MasterTrafficsControl();
    if (error == "") {
        document.getElementById('cmbPrecards_MasterTrafficsControl_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPrecards_MasterTrafficsControl_DropImage').mousedown();
        cmbPrecards_MasterTrafficsControl.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPrecards_MasterTrafficsControl_DropDown').style.display = 'none';
    }
}

function CallBack_cmbPrecards_MasterTrafficsControl_onCallbackError(sender, e) {
    ShowConnectionError_MasterTrafficsControl();
}

function ShowError_GridTraffics_TrafficsControl_onCallBackCompleted(errorParts) {
    showDialog(errorParts[0], errorParts[1], errorParts[2]);
}

function ChangloadingPanelContent_GridTraffics_MasterTrafficsControl(state) {
    var contentText = null;
    switch (state) {
        case 'Fill':
            contentText = document.getElementById('hfloadingPanel_GridTraffics_MasterTrafficsControl').value;
            break;
        case 'Load':
            contentText = '';
            break;
    }
    document.getElementById('loadingPanel_GridTraffics_MasterTrafficsControl').innerHTML = contentText;
}

function SetPosition_cmbPersonnel_MasterTrafficsControl() {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('cmbPersonnel_MasterTrafficsControl_DropDown').style.left = document.getElementById('Mastertbl_MasterTrafficsControl').clientWidth - 400 + 'px';
    }
    if (parent.CurrentLangID == 'en-US') {
        document.getElementById('cmbPersonnel_MasterTrafficsControl_DropDown').style.left = '10px';
    }
}

function ChangeDirection_MasterTrafficsControl() {
    var direction = null;
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    document.getElementById('Mastertbl_MasterTrafficsControl').dir = document.getElementById('Mastertbl_DialogTrafficsControl_MasterTrafficsControl').dir = document.getElementById('cmbYear_MasterTrafficsControl_DropDown').dir = document.getElementById('cmbMonth_MasterTrafficsControl_DropDown').dir = document.getElementById('tblConfirm_DialogConfirm').dir = direction;
    document.getElementById('pdpDate_MasterTrafficsControl').parentNode.dir = direction;
    ChangeDirection_cmbPersonnel_MasterTrafficsControl();
    ChangeDirection_cmbPrecards_MasterTrafficsControl();
}

function ChangeDirection_cmbPersonnel_MasterTrafficsControl() {
    var direction = null;
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    document.getElementById('cmbPersonnel_MasterTrafficsControl_DropDown').dir = direction;
}

function ChangeDirection_cmbPrecards_MasterTrafficsControl() {
    var direction = null;
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    document.getElementById('cmbPrecards_MasterTrafficsControl_DropDown').dir = direction;    
}

function MasterTrafficsControl_onKeyDown() {
    var activeID = null;
    if (event.keyCode == 38 || event.keyCode == 40) {
        activeID = document.activeElement.id;
        CheckTimePickerState_MasterTrafficsControl(activeID);
    }
}

function CheckTimePickerState_MasterTrafficsControl(TimeSelector) {
    if ((TimeSelector == 'TimeSelector_Time_MasterTrafficsControl_txtHour' && isNaN(document.getElementById(TimeSelector).value)) || (TimeSelector == 'TimeSelector_Time_MasterTrafficsControl_txtMinute' && (isNaN(document.getElementById(TimeSelector).value) || document.getElementById('TimeSelector_Time_MasterTrafficsControl_txtHour').value == NullTime_RequestRegister)) || (TimeSelector == 'TimeSelector_Time_MasterTrafficsControl_txtHour' && document.getElementById(TimeSelector).value == '-1'))
        document.getElementById('TimeSelector_Time_MasterTrafficsControl_txtHour').value = zeroTime;
    if (TimeSelector == 'TimeSelector_Time_MasterTrafficsControl_txtMinute' && (isNaN(document.getElementById(TimeSelector).value) || parseInt(document.getElementById(TimeSelector).value) < 0))
        document.getElementById(TimeSelector).value = zeroTime;
}

function CharToKeyCode_MasterTrafficsControl(str) {
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

function ShowConnectionError_MasterTrafficsControl() {
    var error = document.getElementById('hfErrorType_MasterTrafficsControl').value;
    var errorBody = document.getElementById('hfConnectionError_MasterTrafficsControl').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemTrafficTransfer_TlbTlbMasterTrafficsControl_onClick() {
    ShowDialogTrafficsTransfer();
}

function ShowDialogTrafficsTransfer() {
    CollapseControls_MasterTrafficsControl();
    parent.DialogTrafficsTransfer.Show();
}






