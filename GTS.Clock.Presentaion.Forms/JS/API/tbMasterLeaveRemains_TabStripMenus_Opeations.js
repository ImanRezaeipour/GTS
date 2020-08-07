
var CurrentPageState_MasterLeaveRemains = 'View';
var ConfirmState_MasterLeaveRemains = null;
var LoadState_MasterLeaveRemains = 'Normal';
var CurrentPageIndex_cmbPersonnel_MasterLeaveRemains = 0;
var SearchTerm_MasterLeaveRemains = '';
var AdvancedSearchTerm_cmbPersonnel_MasterLeaveRemains = '';
var CurrentPageCombosCallBcakStateObj = new Object();
var SelectedPersonnel_MasterLeaveRemains = null;
var CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains = 0;
var ObjLeaveRemain_MasterLeaveRemains = null;
var SelectedPersonnelID_MasterLeaveRemains = '-1';
var MasterLoadState_MasterLeaveRemains = 'Normal';

function GetBoxesHeaders_MasterLeaveRemains() {
    document.getElementById('header_LeaveRemains_LeaveRemains').innerHTML = document.getElementById('hfheader_LeaveRemains_LeaveRemains').value;
    document.getElementById('clmnName_cmbPersonnel_MasterLeaveRemains').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_MasterLeaveRemains').value;
    document.getElementById('clmnBarCode_cmbPersonnel_MasterLeaveRemains').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_MasterLeaveRemains').value;
    document.getElementById('clmnCardNum_cmbPersonnel_MasterLeaveRemains').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_MasterLeaveRemains').value;
    document.getElementById('footer_GridMasterLeaveRemains_MasterLeaveRemains').innerHTML = document.getElementById('hffooter_GridMasterLeaveRemains_MasterLeaveRemains').value;
}

function Init_TimeSelectors_MasterLeaveRemains() {
    FetchRelativeOperation_TimePickers_MasterLeaveRemains('Reset');
    //FetchRelativeOperation_TimePickers_MasterLeaveRemains('ChangeFloat');
    FetchRelativeOperation_TimePickers_MasterLeaveRemains('ChangeButtonImage');
    FetchRelativeOperation_TimePickers_MasterLeaveRemains('ChangeAction');
}

function FetchRelativeOperation_TimePickers_MasterLeaveRemains(ActionType) {
    var RelativeOperation = null;
    switch (ActionType) {
        case 'Reset':
            RelativeOperation = 'ResetTimepicker_MasterLeaveRemains';
            break;
        case 'ChangeFloat':
            RelativeOperation = 'ChangeFloat_TimeSelector_MasterLeaveRemains';
            break;
        case 'ChangeButtonImage':
            RelativeOperation = 'SetButtonImages_TimeSelector_MasterLeaveRemains';
            break;
        case 'ChangeAction':
            RelativeOperation = 'ChangeTimePickerActions_TimeSelector_MasterLeaveRemains';
            break;
    }
    eval(RelativeOperation + '("TimeSelector_ConfirmedRemainsHour_MasterLeaveRemains")');
}

function ResetTimepicker_MasterLeaveRemains(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}

function ChangeTimePickerActions_TimeSelector_MasterLeaveRemains(TimeSelector) {
    document.getElementById("" + TimeSelector + "_txtHour").onchange = function () {
        TimeSelector_MasterLeaveRemains_onChange(TimeSelector, '_txtHour');
    };
    document.getElementById("" + TimeSelector + "_txtMinute").onchange = function () {
        TimeSelector_MasterLeaveRemains_onChange(TimeSelector, '_txtMinute');
    };
}

function TimeSelector_MasterLeaveRemains_onChange(TimeSelector, partID) {
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

function ChangeFloat_TimeSelector_MasterLeaveRemains(TimeSelector) {
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

function SetButtonImages_TimeSelector_MasterLeaveRemains(TimeSelector) {
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

function DayBox_MasterLeaveRemains_onChange(dayBoxID) {
    var val = document.getElementById(dayBoxID).value;
    val = !isNaN(parseFloat(val)) ? parseFloat(val) > 0 ? "" + parseFloat(val) + "" : '0' : '0';
    if (dayBoxID == 'txtYear_MasterLeaveRemains' && val == '0')
        val == document.getElementById('hfToYear_MasterLeaveRemains').value;
    document.getElementById(dayBoxID).value = val;
}

function SetActionMode_MasterLeaveRemains(state) {
    document.getElementById('ActionMode_MasterLeaveRemains').innerHTML = document.getElementById("hf" + state + "_MasterLeaveRemains").value;
}

function tlbItemNew_TlbMasterLeaveRemains_onClick() {
    ChangePageState_MasterLeaveRemains('Add');
    ShowDialogLeaveRemains();
}

function tlbItemEdit_TlbMasterLeaveRemains_onClick() {
    ChangePageState_MasterLeaveRemains('Edit');
    ShowDialogLeaveRemains();
}

function ChangePageState_MasterLeaveRemains(state) {
    CurrentPageState_MasterLeaveRemains = state;
    ClearList_MasterLeaveRemains();
    SetActionMode_MasterLeaveRemains(state);
    switch (state) {
        case 'Add':
            document.getElementById('txtYear_MasterLeaveRemains').disabled = '';
            break;
        case 'Edit':
            document.getElementById('txtYear_MasterLeaveRemains').disabled = 'disabled';
            NavigateLeaveRemain_MasterLeaveRemains(GridMasterLeaveRemains_MasterLeaveRemains.getSelectedItems()[0]);
            break;
        case 'View':
            break;
    }
}

function ClearList_MasterLeaveRemains() {
    document.getElementById('txtYear_MasterLeaveRemains').value = document.getElementById('hfToYear_MasterLeaveRemains').value;
    document.getElementById('txtRealRemainsDay_MasterLeaveRemains').value = '';
    document.getElementById('txtRealRemainsHour_MasterLeaveRemains').value = '';
    document.getElementById('txtConfirmedRemainsDay_MasterLeaveRemains').value = '';
    ResetTimepicker_MasterLeaveRemains('TimeSelector_ConfirmedRemainsHour_MasterLeaveRemains');
}

function ResetTimepicker_MasterLeaveRemains(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}

function tlbItemLeaveRemainsTransfer_TlbMasterLeaveRemains_onClick() {
    CurrentPageState_MasterLeaveRemains = 'Transfer';
    ShowDialogLeaveRemainsTransfer();
}

function ShowDialogLeaveRemainsTransfer() {
    SetPersonnelCount_MasterLeaveRemains();
    DialogLeaveRemainsTransfer.Show();
    CollapseControls_MasterLeaveRemains();
}

function tlbItemLeaveReserve_TlbMasterLeaveRemains_onClick() {
    ShowDialogLeaveReserve();
}

function tlbItemExit_TlbMasterLeaveRemains_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_MasterLeaveRemains = confirmState;
    switch (ConfirmState_MasterLeaveRemains) {
        case 'Exit':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_MasterLeaveRemains').value;
            break;
    }
    DialogConfirm.Show();
    CollapseControls_MasterLeaveRemains();
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_MasterLeaveRemains_onClick() {
    LoadState_MasterLeaveRemains = 'Normal';
    SelectedPersonnel_MasterLeaveRemains = null;
    SelectedPersonnelID_MasterLeaveRemains = '-1';
    SetPageIndex_cmbPersonnel_MasterLeaveRemains(0);
}

function tlbItemFirst_TlbPaging_PersonnelSearch_MasterLeaveRemains_onClick() {
    SetPageIndex_cmbPersonnel_MasterLeaveRemains(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_MasterLeaveRemains_onClick() {
    if (CurrentPageIndex_cmbPersonnel_MasterLeaveRemains != 0) {
        CurrentPageIndex_cmbPersonnel_MasterLeaveRemains = CurrentPageIndex_cmbPersonnel_MasterLeaveRemains - 1;
        SetPageIndex_cmbPersonnel_MasterLeaveRemains(CurrentPageIndex_cmbPersonnel_MasterLeaveRemains);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_MasterLeaveRemains_onClick() {
    if (CurrentPageIndex_cmbPersonnel_MasterLeaveRemains < parseInt(document.getElementById('hfPersonnelPageCount_MasterLeaveRemains').value) - 1) {
        CurrentPageIndex_cmbPersonnel_MasterLeaveRemains = CurrentPageIndex_cmbPersonnel_MasterLeaveRemains + 1;
        SetPageIndex_cmbPersonnel_MasterLeaveRemains(CurrentPageIndex_cmbPersonnel_MasterLeaveRemains);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_MasterLeaveRemains_onClick() {
    SetPageIndex_cmbPersonnel_MasterLeaveRemains(parseInt(document.getElementById('hfPersonnelPageCount_MasterLeaveRemains').value) - 1);
}

function SetPageIndex_cmbPersonnel_MasterLeaveRemains(pageIndex) {
    CurrentPageIndex_cmbPersonnel_MasterLeaveRemains = pageIndex;
    Fill_cmbPersonnel_MasterLeaveRemains(pageIndex);
}

function Fill_cmbPersonnel_MasterLeaveRemains(pageIndex) {
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_MasterLeaveRemains').value);
    switch (LoadState_MasterLeaveRemains) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm_MasterLeaveRemains = document.getElementById('txtPersonnelSearch_MasterLeaveRemains').value;
            break;
        case 'AdvancedSearch':
            SearchTerm_MasterLeaveRemains = AdvancedSearchTerm_cmbPersonnel_MasterLeaveRemains;
            break;
    }
    CallBack_cmbPersonnel_MasterLeaveRemains.callback(CharToKeyCode_MasterLeaveRemains(LoadState_MasterLeaveRemains), CharToKeyCode_MasterLeaveRemains(pageSize.toString()), CharToKeyCode_MasterLeaveRemains(pageIndex.toString()), CharToKeyCode_MasterLeaveRemains(SearchTerm_MasterLeaveRemains));
}

function cmbPersonnel_MasterLeaveRemains_onExpand(sender, e) {
    SetPosition_cmbPersonnel_MasterLeaveRemains();
    if (cmbPersonnel_MasterLeaveRemains.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_MasterLeaveRemains == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_MasterLeaveRemains = true;
        SetPageIndex_cmbPersonnel_MasterLeaveRemains(0);
    }
}

function cmbPersonnel_MasterLeaveRemains_onCollapse(sender, e) {
    if (cmbPersonnel_MasterLeaveRemains.getSelectedItem() == undefined && SelectedPersonnel_MasterLeaveRemains != null)
        document.getElementById('cmbPersonnel_MasterLeaveRemains_Input').value = SelectedPersonnel_MasterLeaveRemains.Name;
}

function CallBack_cmbPersonnel_MasterLeaveRemains_onBeforeCallback(sender, e) {
    cmbPersonnel_MasterLeaveRemains.dispose();
}

function CallBack_cmbPersonnel_MasterLeaveRemains_onCallbackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_MasterLeaveRemains').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_MasterLeaveRemains').value;
    document.getElementById('clmnBarCode_cmbPersonnel_MasterLeaveRemains').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_MasterLeaveRemains').value;
    document.getElementById('clmnCardNum_cmbPersonnel_MasterLeaveRemains').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_MasterLeaveRemains').value;

    var error = document.getElementById('ErrorHiddenField_Personnel_MasterLeaveRemains').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_MasterLeaveRemains_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_MasterLeaveRemains_DropImage').mousedown();
        else
            cmbPersonnel_MasterLeaveRemains.expand();
        SetPosition_cmbPersonnel_MasterLeaveRemains();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_MasterLeaveRemains_DropDown').style.display = 'none';
    }
}

function SetPersonnelCount_MasterLeaveRemains() {   
    document.getElementById('txtPersonnelCount_MasterLeaveRemains').value = cmbPersonnel_MasterLeaveRemains.getSelectedItem() == undefined && SelectedPersonnel_MasterLeaveRemains == null ? document.getElementById('hfPersonnelCount_MasterLeaveRemains').value : '1';
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CallBack_cmbPersonnel_MasterLeaveRemains_onCallbackError(sender, e) {
    ShowConnectionError_MasterLeaveRemains;
}

function tlbItemSearch_TlbSearchPersonnel_MasterLeaveRemains_onClick() {
    LoadState_MasterLeaveRemains = 'Search';
    CurrentPageIndex_cmbPersonnel_MasterLeaveRemains = 0;
    SelectedPersonnel_MasterLeaveRemains = null;
    SelectedPersonnelID_MasterLeaveRemains = '-1';
    SetPageIndex_cmbPersonnel_MasterLeaveRemains(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_MasterLeaveRemains_onClick() {
    LoadState_MasterLeaveRemains = 'AdvancedSearch';
    CurrentPageIndex_cmbPersonnel_MasterLeaveRemains = 0;
    SelectedPersonnel_MasterLeaveRemains = null;
    SelectedPersonnelID_MasterLeaveRemains = '-1';
    ShowDialogPersonnelSearch('LeaveRemains');
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
    CollapseControls_MasterLeaveRemains();
}

function tlbItemView_TlbView_MasterLeaveRemains_onClick() {
    MasterLoadState_MasterLeaveRemains = LoadState_MasterLeaveRemains;
    GetCurrentPersonnel_MasterLeaveRemains();
    SetPageIndex_GridMasterLeaveRemains_MasterLeaveRemains(0);
}

function GetCurrentPersonnel_MasterLeaveRemains() {
    if (cmbPersonnel_MasterLeaveRemains.getSelectedItem() != undefined)
        SelectedPersonnelID_MasterLeaveRemains = cmbPersonnel_MasterLeaveRemains.getSelectedItem().get_id();
}

function GridMasterLeaveRemains_MasterLeaveRemains_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridMasterLeaveRemains_MasterLeaveRemains').innerHTML = '';
}

function CallBack_GridMasterLeaveRemains_MasterLeaveRemains_onCallbackError(sender, e) {
    ShowConnectionError_MasterLeaveRemains();
}

function ShowConnectionError_MasterLeaveRemains() {
    var error = document.getElementById('hfErrorType_MasterLeaveRemains').value;
    var errorBody = document.getElementById('hfConnectionError_MasterLeaveRemains').value;
    showDialog(error, errorBody, 'error');
}

function CallBack_GridMasterLeaveRemains_MasterLeaveRemains_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MasterLeaveRemains').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridMasterLeaveRemains_MasterLeaveRemains(0);
    }
    else
        Changefooter_GridMasterLeaveRemains_MasterLeaveRemains();
}

function tlbItemRefresh_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains_onClick() {
    SetPageIndex_GridMasterLeaveRemains_MasterLeaveRemains(0);
}

function tlbItemFirst_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains_onClick() {
    SetPageIndex_GridMasterLeaveRemains_MasterLeaveRemains(0);
}

function tlbItemBefore_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains_onClick() {
    if (CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains != 0) {
        CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains = CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains - 1;
        SetPageIndex_GridMasterLeaveRemains_MasterLeaveRemains(CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains);
    }
}

function tlbItemNext_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains_onClick() {
    if (CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains < parseInt(document.getElementById('hfLeaveRemainsPageCount_MasterLeaveRemains').value) - 1) {
        CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains = CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains + 1;
        SetPageIndex_GridMasterLeaveRemains_MasterLeaveRemains(CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains);
    }
}

function tlbItemLast_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains_onClick() {
    SetPageIndex_GridMasterLeaveRemains_MasterLeaveRemains(parseInt(document.getElementById('hfLeaveRemainsPageCount_MasterLeaveRemains').value) - 1);
}

function SetPageIndex_GridMasterLeaveRemains_MasterLeaveRemains(pageIndex) {
    CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains = pageIndex;
    Fill_GridMasterLeaveRemains_MasterLeaveRemains(pageIndex);
}

function Fill_GridMasterLeaveRemains_MasterLeaveRemains(pageIndex) {
    document.getElementById('loadingPanel_GridMasterLeaveRemains_MasterLeaveRemains').innerHTML = document.getElementById('hfloadingPanel_GridMasterLeaveRemains_MasterLeaveRemains').value;
    var pageSize = parseInt(document.getElementById('hfLeaveRemainsPageSize_MasterLeaveRemains').value);
    var FromYear = document.getElementById('hfFromYear_MasterLeaveRemains').value;
    var ToYear = document.getElementById('hfToYear_MasterLeaveRemains').value;
    CallBack_GridMasterLeaveRemains_MasterLeaveRemains.callback(CharToKeyCode_MasterLeaveRemains(MasterLoadState_MasterLeaveRemains), CharToKeyCode_MasterLeaveRemains(pageSize.toString()), CharToKeyCode_MasterLeaveRemains(pageIndex.toString()), CharToKeyCode_MasterLeaveRemains(SelectedPersonnelID_MasterLeaveRemains), CharToKeyCode_MasterLeaveRemains(SearchTerm_MasterLeaveRemains), CharToKeyCode_MasterLeaveRemains(FromYear), CharToKeyCode_MasterLeaveRemains(ToYear));
}

function GridMasterLeaveRemains_MasterLeaveRemains_onItemSelect(sender, e) {
    if (CurrentPageState_MasterLeaveRemains != 'Add')
        NavigateLeaveRemain_MasterLeaveRemains(e.get_item());
}

function NavigateLeaveRemain_MasterLeaveRemains(selectedLeaveRemainItem) {
    if (selectedLeaveRemainItem != undefined) {
        SelectedPersonnel_MasterLeaveRemains = new Object();
        SelectedPersonnel_MasterLeaveRemains.ID = selectedLeaveRemainItem.getMember('Person.ID').get_text();
        document.getElementById('cmbPersonnel_MasterLeaveRemains_Input').value = SelectedPersonnel_MasterLeaveRemains.Name = selectedLeaveRemainItem.getMember('Person.Name').get_text();
        SelectedPersonnel_MasterLeaveRemains.Code = selectedLeaveRemainItem.getMember('Person.PersonCode').get_text();
        document.getElementById('txtYear_MasterLeaveRemains').value = selectedLeaveRemainItem.getMember('Year').get_text();
        document.getElementById('txtRealRemainsDay_MasterLeaveRemains').value = selectedLeaveRemainItem.getMember('RealDay').get_text();
        document.getElementById('txtRealRemainsHour_MasterLeaveRemains').value = selectedLeaveRemainItem.getMember('RealHour').get_text();
        document.getElementById('txtConfirmedRemainsDay_MasterLeaveRemains').value = selectedLeaveRemainItem.getMember('ConfirmedDay').get_text();
        SetDuration_TimePicker_MasterLeaveRemains('TimeSelector_ConfirmedRemainsHour_MasterLeaveRemains', selectedLeaveRemainItem.getMember('ConfirmedHour').get_text());
    }
}

function SetDuration_TimePicker_MasterLeaveRemains(TimePicker, strTime) {
    if (strTime == "")
        strTime = '00:00';
    var arTime_MasterLeaveRemains = strTime.split(':');
    for (var i = 0; i < 2; i++) {
        if (arTime_MasterLeaveRemains[i].length < 2)
            arTime_MasterLeaveRemains[i] = '0' + arTime_MasterLeaveRemains[i];
    }
    document.getElementById(TimePicker + '_txtHour').value = arTime_MasterLeaveRemains[0];
    document.getElementById(TimePicker + '_txtMinute').value = arTime_MasterLeaveRemains[1];
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_MasterLeaveRemains) {
        case 'Exit':
            parent.CloseCurrentTabOnTabStripMenus();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function tlbItemSave_TlbLeaveRemains_onClick() {
    CollapseControls_MasterLeaveRemains();
    UpdateLeaveRemain_MasterLeaveRemains();
}

function GetDuration_TimePicker_MasterLeaveRemains(TimePicker) {
    if (document.getElementById(TimePicker + '_txtHour').value.length < 2)
        document.getElementById(TimePicker + '_txtHour').value = '0' + document.getElementById(TimePicker + '_txtHour').value;
    if (document.getElementById(TimePicker + '_txtMinute').value.length < 2)
        document.getElementById(TimePicker + '_txtMinute').value = '0' + document.getElementById(TimePicker + '_txtMinute').value;
    return document.getElementById(TimePicker + '_txtHour').value + ':' + document.getElementById(TimePicker + '_txtMinute').value;
}

function UpdateLeaveRemain_MasterLeaveRemains() {
    ObjLeaveRemain_MasterLeaveRemains = new Object();
    ObjLeaveRemain_MasterLeaveRemains.ID = '0';
    ObjLeaveRemain_MasterLeaveRemains.PersonnelID = '-1';
    ObjLeaveRemain_MasterLeaveRemains.PersonnelCode = null;
    ObjLeaveRemain_MasterLeaveRemains.PersonnelName = null;
    ObjLeaveRemain_MasterLeaveRemains.Year = '0';
    ObjLeaveRemain_MasterLeaveRemains.RealDay = null;
    ObjLeaveRemain_MasterLeaveRemains.RealHour = null;
    ObjLeaveRemain_MasterLeaveRemains.ConfirmedDay = null;
    ObjLeaveRemain_MasterLeaveRemains.ConfirmedHour = null;
    ObjLeaveRemain_MasterLeaveRemains.PersonnelLoadState = LoadState_MasterLeaveRemains;
    ObjLeaveRemain_MasterLeaveRemains.TransferFromYear = '0';
    ObjLeaveRemain_MasterLeaveRemains.TransferToYear = '0';
    ObjLeaveRemain_MasterLeaveRemains.PersonnelSearchTerm = SearchTerm_MasterLeaveRemains;

    var SelectedItems_GridMasterLeaveRemains_MasterLeaveRemains = GridMasterLeaveRemains_MasterLeaveRemains.getSelectedItems();
    if (SelectedItems_GridMasterLeaveRemains_MasterLeaveRemains.length > 0)
        ObjLeaveRemain_MasterLeaveRemains.ID = SelectedItems_GridMasterLeaveRemains_MasterLeaveRemains[0].getMember("ID").get_text();

    if (cmbPersonnel_MasterLeaveRemains.getSelectedItem() != undefined) {
        ObjLeaveRemain_MasterLeaveRemains.PersonnelID = cmbPersonnel_MasterLeaveRemains.getSelectedItem().get_id();
        ObjLeaveRemain_MasterLeaveRemains.PersonnelCode = cmbPersonnel_MasterLeaveRemains.getSelectedItem().BarCode;
        ObjLeaveRemain_MasterLeaveRemains.PersonnelName = cmbPersonnel_MasterLeaveRemains.getSelectedItem().get_text();
    }
    else {
        if (SelectedPersonnel_MasterLeaveRemains != null) {
            ObjLeaveRemain_MasterLeaveRemains.PersonnelID = SelectedPersonnel_MasterLeaveRemains.ID;
            ObjLeaveRemain_MasterLeaveRemains.PersonnelCode = SelectedPersonnel_MasterLeaveRemains.Code;
            ObjLeaveRemain_MasterLeaveRemains.PersonnelName = SelectedPersonnel_MasterLeaveRemains.Name;
        }
    }
    ObjLeaveRemain_MasterLeaveRemains.Year = document.getElementById('txtYear_MasterLeaveRemains').value != '' ? document.getElementById('txtYear_MasterLeaveRemains').value : '0';
    ObjLeaveRemain_MasterLeaveRemains.RealDay = document.getElementById('txtRealRemainsDay_MasterLeaveRemains').value;
    ObjLeaveRemain_MasterLeaveRemains.RealHour = document.getElementById('txtRealRemainsHour_MasterLeaveRemains').value;
    ObjLeaveRemain_MasterLeaveRemains.ConfirmedDay = document.getElementById('txtConfirmedRemainsDay_MasterLeaveRemains').value;
    ObjLeaveRemain_MasterLeaveRemains.ConfirmedHour = GetDuration_TimePicker_MasterLeaveRemains('TimeSelector_ConfirmedRemainsHour_MasterLeaveRemains');
    ObjLeaveRemain_MasterLeaveRemains.TransferFromYear = cmbTransferFromYear_MasterLeaveRemains.getSelectedItem().get_value();
    ObjLeaveRemain_MasterLeaveRemains.TransferToYear = cmbTransferToYear_MasterLeaveRemains.getSelectedItem().get_value();
    UpdateLeaveRemain_MasterLeaveRemainsPage(CharToKeyCode_MasterLeaveRemains(CurrentPageState_MasterLeaveRemains), CharToKeyCode_MasterLeaveRemains(ObjLeaveRemain_MasterLeaveRemains.ID), CharToKeyCode_MasterLeaveRemains(ObjLeaveRemain_MasterLeaveRemains.PersonnelID), CharToKeyCode_MasterLeaveRemains(ObjLeaveRemain_MasterLeaveRemains.Year), CharToKeyCode_MasterLeaveRemains(ObjLeaveRemain_MasterLeaveRemains.RealDay), CharToKeyCode_MasterLeaveRemains(ObjLeaveRemain_MasterLeaveRemains.RealHour), CharToKeyCode_MasterLeaveRemains(ObjLeaveRemain_MasterLeaveRemains.ConfirmedDay), CharToKeyCode_MasterLeaveRemains(ObjLeaveRemain_MasterLeaveRemains.ConfirmedHour), CharToKeyCode_MasterLeaveRemains(ObjLeaveRemain_MasterLeaveRemains.PersonnelLoadState), CharToKeyCode_MasterLeaveRemains(ObjLeaveRemain_MasterLeaveRemains.TransferFromYear), CharToKeyCode_MasterLeaveRemains(ObjLeaveRemain_MasterLeaveRemains.TransferToYear), CharToKeyCode_MasterLeaveRemains(ObjLeaveRemain_MasterLeaveRemains.PersonnelSearchTerm));
    DialogWaiting.Show();
}

function UpdateLeaveRemain_MasterLeaveRemainsPage_onCallBcak(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();        
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_MasterLeaveRemains').value;
            Response[1] = document.getElementById('hfConnectionError_MasterLeaveRemains').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            CloseRelativeDialog_MasterLeaveRemains();
            CurrentPageState_MasterLeaveRemains = 'View';
            LoadState_MasterLeaveRemains = 'Normal';
            ClearLeaveRemainsTransferList_MasterLeaveRemains();
            SetPageIndex_GridMasterLeaveRemains_MasterLeaveRemains(0);
        }
        else {
            if (CurrentPageState_MasterLeaveRemains == 'Delete')
                CurrentPageState_MasterLeaveRemains = 'View';
        }
    }
}

function CloseRelativeDialog_MasterLeaveRemains() {
    switch (CurrentPageState_MasterLeaveRemains) {
        case 'Add':
            DialogLeaveRemains.Close();
            break;
        case 'Edit':
            DialogLeaveRemains.Close();            
            break;
        case 'Transfer':
            DialogLeaveRemainsTransfer.Close();            
            break;
    }
}

function LeaveRemain_OnAfterUpdate(Response) {
    if (ObjLeaveRemain_MasterLeaveRemains != null && CurrentPageState_MasterLeaveRemains != 'Transfer') {
        var PersonnelID = ObjLeaveRemain_MasterLeaveRemains.PersonnelID;
        var PersonnelCode = ObjLeaveRemain_MasterLeaveRemains.PersonnelCode;
        var PersonnelName = ObjLeaveRemain_MasterLeaveRemains.PersonnelName;
        var Year = ObjLeaveRemain_MasterLeaveRemains.Year;
        var RealDay = ObjLeaveRemain_MasterLeaveRemains.RealDay;
        var RealHour = ObjLeaveRemain_MasterLeaveRemains.RealHour;
        var ConfirmedDay = ObjLeaveRemain_MasterLeaveRemains.ConfirmedDay;
        var ConfirmedHour = ObjLeaveRemain_MasterLeaveRemains.ConfirmedHour;

        LeaveRemainItem = null;
        GridMasterLeaveRemains_MasterLeaveRemains.beginUpdate();
        switch (CurrentPageState_MasterLeaveRemains) {
            case 'Add':                
                LeaveRemainItem = GridMasterLeaveRemains_MasterLeaveRemains.get_table().addEmptyRow(GridMasterLeaveRemains_MasterLeaveRemains.get_recordCount());
                LeaveRemainItem.setValue(0, Response[3], false);
                GridMasterLeaveRemains_MasterLeaveRemains.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridMasterLeaveRemains_MasterLeaveRemains.selectByKey(Response[3], 0, false);
                LeaveRemainItem = GridMasterLeaveRemains_MasterLeaveRemains.getItemFromKey(0, Response[3]);
                break;
        }
        LeaveRemainItem.setValue(1, PersonnelID, false);
        LeaveRemainItem.setValue(2, PersonnelCode, false);
        LeaveRemainItem.setValue(3, PersonnelName, false);
        LeaveRemainItem.setValue(4, Year, false);
        LeaveRemainItem.setValue(1, ConfirmedDay, false);
        LeaveRemainItem.setValue(1, ConfirmedHour, false);
        LeaveRemainItem.setValue(1, RealDay, false);
        LeaveRemainItem.setValue(1, RealHour, false);

        GridMasterLeaveRemains_MasterLeaveRemains.endUpdate();
    }
}

function UpdateFeatures_GridMasterLeaveRemains_MasterLeaveRemains() {
    var LeaveRemainsCount = parseInt(document.getElementById('hfLeaveRemainsCount_LeaveRemains').value);
    var LeaveRemainsPageCount = parseInt(document.getElementById('hfLeaveRemainsPageCount_MasterLeaveRemains').value);
    var LeaveRemainsPageSize = parseInt(document.getElementById('hfLeaveRemainsPageSize_MasterLeaveRemains').value);
    if (LeaveRemainsCount > 0) {
        LeaveRemainsCount = LeaveRemainsCount - 1;
        var divRem = mod(LeaveRemainsCount, LeaveRemainsPageSize);
        if (divRem == 0) {
            LeaveRemainsPageCount = LeaveRemainsPageCount - 1;
            if (CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains == LeaveRemainsPageCount)
                CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains = CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains - 1 >= 0 ? CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains - 1 : 0;
        }
        SetPageIndex_GridMasterLeaveRemains_MasterLeaveRemains(CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains);
        document.getElementById('hfLeaveRemainsCount_LeaveRemains').value = LeaveRemainsCount.toString();
        document.getElementById('hfLeaveRemainsPageCount_MasterLeaveRemains').value = LeaveRemainsPageCount.toString();
        Changefooter_GridMasterLeaveRemains_MasterLeaveRemains();
    }
}

function Changefooter_GridMasterLeaveRemains_MasterLeaveRemains() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridMasterLeaveRemains_MasterLeaveRemains').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfLeaveRemainsPageCount_MasterLeaveRemains').value) > 0 ? CurrentPageIndex_GridMasterLeaveRemains_MasterLeaveRemains + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfLeaveRemainsPageCount_MasterLeaveRemains').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridMasterLeaveRemains_MasterLeaveRemains').innerHTML = retfooterVal;
}

function mod(a, b) {
    return a - (b * Math.floor(a / b));
}

function tlbItemCancel_TlbLeaveRemains_onClick() {
    ChangePageState_MasterLeaveRemains('View');
    DialogLeaveRemains.Close();
}

function ShowDialogLeaveRemains() {
    DialogLeaveRemains.Show();
    CollapseControls_MasterLeaveRemains();
}

function ShowDialogLeaveReserve() {
    var selectedItems_GridMasterLeaveRemains_MasterLeaveRemains = GridMasterLeaveRemains_MasterLeaveRemains.getSelectedItems();
    if (cmbPersonnel_MasterLeaveRemains.getSelectedItem() != undefined || selectedItems_GridMasterLeaveRemains_MasterLeaveRemains.length > 0) {
        var ObjDialogLeaveReserve = new Object();
        ObjDialogLeaveReserve.LeaveRemainID = selectedItems_GridMasterLeaveRemains_MasterLeaveRemains[0].getMember('ID').get_text();
        if (cmbPersonnel_MasterLeaveRemains.getSelectedItem() != undefined)
            ObjDialogLeaveReserve.PersonnelID = cmbPersonnel_MasterLeaveRemains.getSelectedItem().get_id();
        else {
            if (selectedItems_GridMasterLeaveRemains_MasterLeaveRemains.length > 0)
                ObjDialogLeaveReserve.PersonnelID = selectedItems_GridMasterLeaveRemains_MasterLeaveRemains[0].getMember('Person.ID').get_text();
        }
        ObjDialogLeaveReserve.PersonnelName = selectedItems_GridMasterLeaveRemains_MasterLeaveRemains[0].getMember('Person.Name').get_text();
        ObjDialogLeaveReserve.Year = selectedItems_GridMasterLeaveRemains_MasterLeaveRemains[0].getMember('Year').get_text();
        ObjDialogLeaveReserve.ConfirmedDay = selectedItems_GridMasterLeaveRemains_MasterLeaveRemains[0].getMember('ConfirmedDay').get_text();
        ObjDialogLeaveReserve.ConfirmedHour = selectedItems_GridMasterLeaveRemains_MasterLeaveRemains[0].getMember('ConfirmedHour').get_text();
        ObjDialogLeaveReserve.RealDay = selectedItems_GridMasterLeaveRemains_MasterLeaveRemains[0].getMember('RealDay').get_text();
        ObjDialogLeaveReserve.RealHour = selectedItems_GridMasterLeaveRemains_MasterLeaveRemains[0].getMember('RealHour').get_text();
        parent.DialogLeaveReserve.set_value(ObjDialogLeaveReserve);
        parent.DialogLeaveReserve.Show();
        CollapseControls_MasterLeaveRemains();
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CharToKeyCode_MasterLeaveRemains(str) {
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

function LeaveRemains_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_cmbPersonnel_MasterLeaveRemains = SearchTerm;
    SetPageIndex_cmbPersonnel_MasterLeaveRemains(0);
}

function cmbFromYear_MasterMonthlyOperation_onChange(sender, e) {
    if (cmbFromYear_MasterMonthlyOperation.getSelectedItem() != undefined)
        document.getElementById('hfFromYear_MasterLeaveRemains').value = cmbFromYear_MasterMonthlyOperation.getSelectedItem().get_value();
}

function cmbToYear_MasterMonthlyOperation_onChange(sender, e) {
    if (cmbToYear_MasterMonthlyOperation.getSelectedItem() != undefined)
        document.getElementById('hfToYear_MasterLeaveRemains').value = cmbToYear_MasterMonthlyOperation.getSelectedItem().get_value();
}

function SetPosition_cmbPersonnel_MasterLeaveRemains() {
    if (parent.CurrentLangID == 'fa-IR') {
        if (400 >= document.getElementById('cmbPersonnel_MasterLeaveRemains').parentNode.parentNode.clientWidth)
           document.getElementById('cmbPersonnel_MasterLeaveRemains_DropDown').style.left = document.getElementById('Mastertbl_MasterLeaveRemains').clientWidth - 400 + 'px';
    }
    if (parent.CurrentLangID == 'en-US') {
        document.getElementById('cmbPersonnel_MasterLeaveRemains_DropDown').style.left = '10px';
    }
}

function CollapseControls_MasterLeaveRemains() {
    cmbPersonnel_MasterLeaveRemains.collapse();
    cmbFromYear_MasterMonthlyOperation.collapse();
    cmbToYear_MasterMonthlyOperation.collapse();
}

function tlbItemSave_TlbLeaveRemainsTransfer_MasterLeaveRemains_onClick() {
    CollapseControls_MasterLeaveRemains();
    UpdateLeaveRemain_MasterLeaveRemains();
}

function tlbItemCancel_TlbLeaveRemainsTransfer_MasterLeaveRemains_onClick() {
    CurrentPageState_MasterLeaveRemains = 'View';
    ClearLeaveRemainsTransferList_MasterLeaveRemains();
    DialogLeaveRemainsTransfer.Close();
}

function ClearLeaveRemainsTransferList_MasterLeaveRemains() {
    document.getElementById('cmbTransferFromYear_MasterLeaveRemains_Input').value = cmbTransferFromYear_MasterLeaveRemains.getItem(1).get_value();
    cmbTransferFromYear_MasterLeaveRemains.selectItemByIndex(1);
    document.getElementById('cmbTransferToYear_MasterLeaveRemains_Input').value = cmbTransferToYear_MasterLeaveRemains.getItem(1).get_value();
    cmbTransferToYear_MasterLeaveRemains.selectItemByIndex(1);
    document.getElementById('txtPersonnelCount_MasterLeaveRemains').value = '';
}

function tlbItemFormReconstruction_TlbMasterLeaveRemains_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvMasterLeaveRemains_iFrame').src = 'MasterLeaveRemains.aspx';
}

function tlbItemHelp_TlbMasterLeaveRemains_onClick() {
    LoadHelpPage('tlbItemHelp_TlbMasterLeaveRemains');    
}




