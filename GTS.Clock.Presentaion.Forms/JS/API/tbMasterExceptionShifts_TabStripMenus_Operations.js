
var CurrentPageState_MasterExceptionShifts = 'View';
var LoadState_cmbPersonnel_MasterExceptionShifts = 'Normal';
var CurrentPageCombosCallBcakStateObj = new Object();
var CurrentPageIndex_cmbPersonnel_MasterExceptionShifts = 0;
var SearchTerm_cmbPersonnel_MasterExceptionShifts = '';
var AdvancedSearchTerm_cmbPersonnel_MasterExceptionShifts = '';
var ConfirmState_MasterExceptionShifts = null;
var ObjMasterExceptionShift_MasterExceptionShifts = null;

///////////////////// gdpMasterFromDate & gCalMasterFromDate ////////////////////////
function gdpMasterFromDate_MasterExceptionShifts_OnDateChange(sender, eventArgs) {
    var fromDate = gdpMasterFromDate_MasterExceptionShifts.getSelectedDate();
    gCalMasterFromDate_MasterExceptionShifts.setSelectedDate(fromDate);
}
function gCalMasterFromDate_MasterExceptionShifts_OnChange(sender, eventArgs) {
    var fromDate = gCalMasterFromDate_MasterExceptionShifts.getSelectedDate();
    gdpMasterFromDate_MasterExceptionShifts.setSelectedDate(fromDate);
}
function btn_gdpMasterFromDate_MasterExceptionShifts_OnClick(event) {
    if (gCalMasterFromDate_MasterExceptionShifts.get_popUpShowing()) {
        gCalMasterFromDate_MasterExceptionShifts.hide();
    }
    else {
        gCalMasterFromDate_MasterExceptionShifts.setSelectedDate(gdpMasterFromDate_MasterExceptionShifts.getSelectedDate());
        gCalMasterFromDate_MasterExceptionShifts.show();
    }
}
function btn_gdpMasterFromDate_MasterExceptionShifts_OnMouseUp(event) {
    if (gCalMasterFromDate_MasterExceptionShifts.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

///////////////////// gdpMasterToDate & gCalMasterToDate ////////////////////////
function gdpMasterToDate_MasterExceptionShifts_OnDateChange(sender, eventArgs) {
    var toDate = gdpMasterToDate_MasterExceptionShifts.getSelectedDate();
    gCalMasterToDate_MasterExceptionShifts.setSelectedDate(toDate);
}
function gCalMasterToDate_MasterExceptionShifts_OnChange(sender, eventArgs) {
    var toDate = gCalMasterToDate_MasterExceptionShifts.getSelectedDate();
    gdpMasterToDate_MasterExceptionShifts.setSelectedDate(toDate);
}
function btn_gdpMasterToDate_MasterExceptionShifts_OnClick(event) {
    if (gCalMasterToDate_MasterExceptionShifts.get_popUpShowing()) {
        gCalMasterToDate_MasterExceptionShifts.hide();
    }
    else {
        gCalMasterToDate_MasterExceptionShifts.setSelectedDate(gdpMasterToDate_MasterExceptionShifts.getSelectedDate());
        gCalMasterToDate_MasterExceptionShifts.show();
    }
}

function btn_gdpMasterToDate_MasterExceptionShifts_OnMouseUp(event) {
    if (gCalMasterToDate_MasterExceptionShifts.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function GetBoxesHeaders_MasterExceptionShifts() {
    document.getElementById('header_SearchByPersonnelBox_MasterExceptionShifts').innerHTML = document.getElementById('hfheader_SearchByPersonnelBox_MasterExceptionShifts').value;
    document.getElementById('header_ExceptionShifts_MasterExceptionShifts').innerHTML = document.getElementById('hfheader_ExceptionShifts_MasterExceptionShifts').value;
    document.getElementById('clmnName_cmbPersonnel_MasterExceptionShifts').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_MasterExceptionShifts').value;
    document.getElementById('clmnBarCode_cmbPersonnel_MasterExceptionShifts').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_MasterExceptionShifts').value;
    document.getElementById('clmnCardNum_cmbPersonnel_MasterExceptionShifts').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_MasterExceptionShifts').value;
}

function ViewCurrentLangCalendars_tbMasterExceptionShifts_TabStripMenus() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpMasterFromDate_MasterExceptionShifts").parentNode.removeChild(document.getElementById("pdpMasterFromDate_MasterExceptionShifts"));
            document.getElementById("pdpMasterFromDate_MasterExceptionShiftsimgbt").parentNode.removeChild(document.getElementById("pdpMasterFromDate_MasterExceptionShiftsimgbt"));
            document.getElementById("pdpMasterToDate_MasterExceptionShifts").parentNode.removeChild(document.getElementById("pdpMasterToDate_MasterExceptionShifts"));
            document.getElementById("pdpMasterToDate_MasterExceptionShiftsimgbt").parentNode.removeChild(document.getElementById("pdpMasterToDate_MasterExceptionShiftsimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_FromDateCalendars_MasterExceptionShifts").removeChild(document.getElementById("Container_gCalMasterFromDate_MasterExceptionShifts"));
            document.getElementById("Container_ToDateCalendars_MasterExceptionShifts").removeChild(document.getElementById("Container_gCalMasterToDate_MasterExceptionShifts"));
            break;
    }
}

function SetPosition_cmbPersonnel_MasterExceptionShifts() {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('cmbPersonnel_MasterExceptionShifts_DropDown').style.left = document.getElementById('Mastertbl_MasterExceptionShifts').clientWidth - 400 + 'px';
    }
    if (parent.CurrentLangID == 'en-US') {
        document.getElementById('cmbPersonnel_MasterExceptionShifts_DropDown').style.left = '30px';
    }
}

var box_SearchByPersonnel_MasterExceptionShifts_IsShown = false;
function imgbox_SearchByPersonnel_MasterExceptionShifts_onClick() {
    CollapseControls_MasterExceptionShifts(null);
    setSlideDownSpeed(200);
    slidedown_showHide('box_SearchByPersonnel_MasterExceptionShifts');

    if (box_SearchByPersonnel_MasterExceptionShifts_IsShown) {
        box_SearchByPersonnel_MasterExceptionShifts_IsShown = false;
        document.getElementById('imgbox_SearchByPersonnel_MasterExceptionShifts').src = 'Images/Ghadir/arrowDown.jpg';
    }
    else {
        box_SearchByPersonnel_MasterExceptionShifts_IsShown = true;
        document.getElementById('imgbox_SearchByPersonnel_MasterExceptionShifts').src = 'Images/Ghadir/arrowUp.jpg';
    }
}

function SetPosition_DropDownDives_MasterExceptionShifts() {
    switch (parent.CurrentLangID) {
        case "fa-IR":
            document.getElementById('box_SearchByPersonnel_MasterExceptionShifts').style.right = '20px';
            break;
        case "en-US":
            document.getElementById('box_SearchByPersonnel_MasterExceptionShifts').style.left = '20px';
            break;
    }

}

function tlbItemNew_TlbMasterExceptionShifts_onClick() {
    if(cmbPersonnel_MasterExceptionShifts.getSelectedItem() != undefined)
       ChangePageState_MasterExceptionShifts('Add');
}

function tlbItemEdit_TlbMasterExceptionShifts_onClick() {
    if (GridMasterExceptionShifts_MasterExceptionShifts.getSelectedItems().length > 0)
        ChangePageState_MasterExceptionShifts('Edit');
}

function tlbItemDelete_TlbMasterExceptionShifts_onClick() {
    ChangePageState_MasterExceptionShifts('Delete');
}

function tlbItemTwoDayReplacement_TlbMasterExceptionShifts_onClick() {
    ChangePageState_MasterExceptionShifts('TwoDayReplacement');
}

function tlbItemTwoPersonnelReplacement_TlbMasterExceptionShifts_onClick() {
    ChangePageState_MasterExceptionShifts('TwoPersonnelReplacement');
}

function ChangePageState_MasterExceptionShifts(state) {
    CurrentPageState_MasterExceptionShifts = state;
    SetActionMode_MasterExceptionShifts(state);
    if (CurrentPageState_MasterExceptionShifts != 'Delete' && CurrentPageState_MasterExceptionShifts != 'View') {
        ShowDialogExceptionShifts(state);
        if (CurrentPageState_MasterExceptionShifts == 'Edit')
            NavigateExceptionShift_MasterExceptionShifts(GridMasterExceptionShifts_MasterExceptionShifts.getSelectedItems()[0]);
    }
    if(CurrentPageState_MasterExceptionShifts == 'Delete')
        ShowDialogConfirm('Delete');
}

function SetActionMode_MasterExceptionShifts(state) {
       document.getElementById('ActionMode_MasterExceptionShifts').innerHTML = document.getElementById("hf" + state + "_MasterExceptionShifts").value;
}

function ShowDialogExceptionShifts() {
    var ObjDialogExceptionShifts = new Object();
    ObjDialogExceptionShifts.State = CurrentPageState_MasterExceptionShifts;
    ObjDialogExceptionShifts.PersonnelID = undefined;
    ObjDialogExceptionShifts.PersonnelName = undefined;
    ObjDialogExceptionShifts.ShiftID = undefined;
    ObjDialogExceptionShifts.ShiftName = undefined;
    ObjDialogExceptionShifts.Date = undefined;
    switch (CurrentPageState_MasterExceptionShifts) {
        case 'Add':
            ObjDialogExceptionShifts.ActiveTabID = 'tbDetails_TabStripShiftDetails';
            if (cmbPersonnel_MasterExceptionShifts.getSelectedItem() != undefined) {
                ObjDialogExceptionShifts.PersonnelID = cmbPersonnel_MasterExceptionShifts.getSelectedItem().get_id();
                ObjDialogExceptionShifts.PersonnelName = cmbPersonnel_MasterExceptionShifts.getSelectedItem().get_text();
            }
            break;
        case 'Edit':
            ObjDialogExceptionShifts.ActiveTabID = 'tbDetails_TabStripShiftDetails';
            if (GridMasterExceptionShifts_MasterExceptionShifts.getSelectedItems().length > 0) {
                ObjDialogExceptionShifts.PersonnelID = GridMasterExceptionShifts_MasterExceptionShifts.getSelectedItems()[0].getMember('Person.ID').get_text();
                ObjDialogExceptionShifts.PersonnelName = GridMasterExceptionShifts_MasterExceptionShifts.getSelectedItems()[0].getMember('Person.Name').get_text();
                ObjDialogExceptionShifts.ShiftID = GridMasterExceptionShifts_MasterExceptionShifts.getSelectedItems()[0].getMember('Shift.ID').get_text();
                ObjDialogExceptionShifts.ShiftName = GridMasterExceptionShifts_MasterExceptionShifts.getSelectedItems()[0].getMember('Shift.Name').get_text();
                ObjDialogExceptionShifts.Date = GridMasterExceptionShifts_MasterExceptionShifts.getSelectedItems()[0].getMember('TheDate').get_text();
            }
            break;
        case 'TwoDayReplacement':
            ObjDialogExceptionShifts.ActiveTabID = 'tbTwoDayReplacement_TabStripShiftDetails';
            break;
        case 'TwoPersonnelReplacement':
            ObjDialogExceptionShifts.ActiveTabID = 'tbTwoPersonnelReplacement_TabStripShiftDetails';
            break;
    }
    parent.DialogExceptionShifts.set_value(ObjDialogExceptionShifts);
    parent.DialogExceptionShifts.Show();
}

function tlbItemExit_TlbMasterExceptionShifts_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_MasterExceptionShifts = confirmState;
    if (CurrentPageState_MasterExceptionShifts == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_MasterExceptionShifts').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_MasterExceptionShifts').value;
    DialogConfirm.Show();
    CollapseControls_MasterExceptionShifts(null);
}

function tlbItemHelp_TlbMasterExceptionShifts_onClick() {
    LoadHelpPage('tlbItemHelp_TlbMasterExceptionShifts');
}

function GridMasterExceptionShifts_MasterExceptionShifts_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridExceptionShifts_MastExceptionShifts').innerHTML = '';    
}

function CallBack_GridMasterExceptionShifts_MasterExceptionShifts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ExceptionShifts').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridMasterExceptionShifts_MasterExceptionShifts();
    }
}

function Fill_GridMasterExceptionShifts_MasterExceptionShifts() {
    document.getElementById('loadingPanel_GridExceptionShifts_MastExceptionShifts').innerHTML = document.getElementById('hfloadingPanel_GridExceptionShifts_MasterExceptionShifts').value;
    var PersonnelID = '0';
    var FromDate = null;
    var ToDate = null;
    if (cmbPersonnel_MasterExceptionShifts.getSelectedItem() != undefined)
        PersonnelID = cmbPersonnel_MasterExceptionShifts.getSelectedItem().get_id();
    switch (parent.SysLangID) {
        case 'fa-IR':
            FromDate = document.getElementById('pdpMasterFromDate_MasterExceptionShifts').value;
            ToDate = document.getElementById('pdpMasterToDate_MasterExceptionShifts').value;
            break;
        case 'en-Us':
            FromDate = document.getElementById('gdpMasterFromDate_MasterExceptionShifts_picker').value;
            ToDate = document.getElementById('gdpMasterToDate_MasterExceptionShifts_picker').value;
            break;
    }
    CallBack_GridMasterExceptionShifts_MasterExceptionShifts.callback(CharToKeyCode_MasterExceptionShifts(PersonnelID), CharToKeyCode_MasterExceptionShifts(FromDate), CharToKeyCode_MasterExceptionShifts(ToDate));
}

function CallBack_GridMasterExceptionShifts_MasterExceptionShifts_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridExceptionShifts_MastExceptionShifts').innerHTML = '';
    ShowConnectionError_MasterExceptionShifts();
}

function ShowConnectionError_MasterExceptionShifts() {
    var error = document.getElementById('hfErrorType_MasterExceptionShifts').value;
    var errorBody = document.getElementById('hfConnectionError_MasterExceptionShifts').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_MasterExceptionShifts_onClick() {
    Refresh_cmbPersonnel_MasterExceptionShifts();
}

function Refresh_cmbPersonnel_MasterExceptionShifts() {
    LoadState_cmbPersonnel_MasterExceptionShifts = 'Normal';
    SetPageIndex_cmbPersonnel_MasterExceptionShifts(0);
}

function tlbItemFirst_TlbPaging_PersonnelSearch_MasterExceptionShifts_onClick() {
    SetPageIndex_cmbPersonnel_MasterExceptionShifts(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_MasterExceptionShifts_onClick() {
    if (CurrentPageIndex_cmbPersonnel_MasterExceptionShifts != 0) {
        CurrentPageIndex_cmbPersonnel_MasterExceptionShifts = CurrentPageIndex_cmbPersonnel_MasterExceptionShifts - 1;
        SetPageIndex_cmbPersonnel_MasterExceptionShifts(CurrentPageIndex_cmbPersonnel_MasterExceptionShifts);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_MasterExceptionShifts_onClick() {
    if (CurrentPageIndex_cmbPersonnel_MasterExceptionShifts < parseInt(document.getElementById('hfPersonnelPageCount_MasterExceptionShifts').value) - 1) {
        CurrentPageIndex_cmbPersonnel_MasterExceptionShifts = CurrentPageIndex_cmbPersonnel_MasterExceptionShifts + 1;
        SetPageIndex_cmbPersonnel_MasterExceptionShifts(CurrentPageIndex_cmbPersonnel_MasterExceptionShifts);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_MasterExceptionShifts_onClick() {
    SetPageIndex_cmbPersonnel_MasterExceptionShifts(parseInt(document.getElementById('hfPersonnelPageCount_MasterExceptionShifts').value) - 1);
}

function SetPageIndex_cmbPersonnel_MasterExceptionShifts(pageIndex) {
    CurrentPageIndex_cmbPersonnel_MasterExceptionShifts = pageIndex;
    Fill_cmbPersonnel_MasterExceptionShifts(pageIndex);
}

function Fill_cmbPersonnel_MasterExceptionShifts(pageIndex) {
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_MasterExceptionShifts').value);
    var SearchTermConditions = '';
    switch (LoadState_cmbPersonnel_MasterExceptionShifts) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm_cmbPersonnel_MasterExceptionShifts = SearchTermConditions = document.getElementById('txtPersonnelSearch_MasterExceptionShifts').value;
            break;
        case 'AdvancedSearch':
            SearchTermConditions = AdvancedSearchTerm_cmbPersonnel_MasterExceptionShifts;
            break;
    }
    CallBack_cmbPersonnel_MasterExceptionShifts.callback(CharToKeyCode_MasterExceptionShifts(LoadState_cmbPersonnel_MasterExceptionShifts), CharToKeyCode_MasterExceptionShifts(pageSize.toString()), CharToKeyCode_MasterExceptionShifts(pageIndex.toString()), CharToKeyCode_MasterExceptionShifts(SearchTermConditions));
}

function CharToKeyCode_MasterExceptionShifts(str) {
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

function cmbPersonnel_MasterExceptionShifts_onExpand(sender, e) {
    CollapseControls_MasterExceptionShifts(cmbPersonnel_MasterExceptionShifts);
    SetPosition_cmbPersonnel_MasterExceptionShifts();
    if (cmbPersonnel_MasterExceptionShifts.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_MasterExceptionShifts == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_MasterExceptionShifts = true;
        SetPageIndex_cmbPersonnel_MasterExceptionShifts(0);
    }
}

function CallBack_cmbPersonnel_MasterExceptionShifts_onBeforeCallback(sender, e) {
    cmbPersonnel_MasterExceptionShifts.dispose();
}

function CallBack_cmbPersonnel_MasterExceptionShifts_onCallBackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_MasterExceptionShifts').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_MasterExceptionShifts').value;
    document.getElementById('clmnBarCode_cmbPersonnel_MasterExceptionShifts').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_MasterExceptionShifts').value;
    document.getElementById('clmnCardNum_cmbPersonnel_MasterExceptionShifts').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_MasterExceptionShifts').value;

    SetPosition_cmbPersonnel_MasterExceptionShifts();

    var error = document.getElementById('ErrorHiddenField_Personnel_MasterExceptionShifts').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_MasterExceptionShifts_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_MasterExceptionShifts_DropImage').mousedown();
        else
            cmbPersonnel_MasterExceptionShifts.expand();
        var personnelCount = document.getElementById('hfPersonnelCount_MasterExceptionShifts').value;
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_MasterExceptionShifts_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CallBack_cmbPersonnel_MasterExceptionShifts_onCallbackError(sender, e) {
    ShowConnectionError_MasterExceptionShifts();
}

function tlbItemSearch_TlbSearchPersonnel_MasterExceptionShifts_onClick() {
    LoadState_cmbPersonnel_MasterExceptionShifts = 'Search';
    SetPageIndex_cmbPersonnel_MasterExceptionShifts(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_MasterExceptionShifts_onClick() {
    LoadState_cmbPersonnel_MasterExceptionShifts = 'AdvancedSearch';
    ShowDialogPersonnelSearch('MasterExceptionShifts');
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
    CollapseControls_MasterExceptionShifts(null);
}

function MasterExceptionShifts_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_cmbPersonnel_MasterExceptionShifts = SearchTerm;
    SetPageIndex_cmbPersonnel_MasterExceptionShifts(0);
}

function tlbItemView_TlbView_MasterExceptionShifts_onClick() {
    Fill_GridMasterExceptionShifts_MasterExceptionShifts();
}

function CollapseControls_MasterExceptionShifts(exception) {
    if (exception == null || exception != cmbPersonnel_MasterExceptionShifts)
        cmbPersonnel_MasterExceptionShifts.collapse();
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpMasterFromDate_MasterExceptionShifts');
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_MasterExceptionShifts) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateExceptionShift_MasterExceptionShifts();
            break;
        case 'Exit':
            parent.CloseCurrentTabOnTabStripMenus();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_MasterExceptionShifts('View');
}

function UpdateExceptionShift_MasterExceptionShifts() {
    ObjMasterExceptionShift_MasterExceptionShifts = new Object();
    ObjMasterExceptionShift_MasterExceptionShifts.ID = '0';
    var SelectedItems_GridMasterExceptionShifts_MasterExceptionShifts = GridMasterExceptionShifts_MasterExceptionShifts.getSelectedItems();
    if (SelectedItems_GridMasterExceptionShifts_MasterExceptionShifts.length > 0)
        ObjMasterExceptionShift_MasterExceptionShifts.ID = SelectedItems_GridMasterExceptionShifts_MasterExceptionShifts[0].getMember("ID").get_text();

    UpdateExceptionShift_MasterExceptionShiftsPage(CharToKeyCode_MasterExceptionShifts(CurrentPageState_MasterExceptionShifts), CharToKeyCode_MasterExceptionShifts(ObjMasterExceptionShift_MasterExceptionShifts.ID));
    DialogWaiting.Show();
}

function UpdateExceptionShift_MasterExceptionShiftsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_MasterExceptionShifts').value;
            Response[1] = document.getElementById('hfConnectionError_MasterExceptionShifts').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            MasterExceptionShift_OnAfterUpdate(Response);
            ChangePageState_MasterExceptionShifts('View');
        }
        else {
            if (CurrentPageState_MasterExceptionShifts == 'Delete')
                ChangePageState_MasterExceptionShifts('View');
        }
    }
}

function MasterExceptionShift_OnAfterUpdate(Response) {
    if (ObjMasterExceptionShift_MasterExceptionShifts != null) {
        var MasterExceptionShiftItem = null;
        GridMasterExceptionShifts_MasterExceptionShifts.beginUpdate();
        switch (CurrentPageState_MasterExceptionShifts) {
            case 'Add':
                break;
            case 'Edit':
                break;
            case 'Delete':
                GridMasterExceptionShifts_MasterExceptionShifts.selectByKey(ObjMasterExceptionShift_MasterExceptionShifts.ID, 0, false);
                GridMasterExceptionShifts_MasterExceptionShifts.deleteSelected();
                break;
        }
        GridMasterExceptionShifts_MasterExceptionShifts.endUpdate();
    }
}

function ExceptionShift_OnAfterUpdate(Response) {
    var RetMessage = Response;
    showDialog(RetMessage[0], Response[1], RetMessage[2]);
    Fill_GridMasterExceptionShifts_MasterExceptionShifts();
}

function ResetCalendars_MasterExceptionShifts() {
    var currentDate_MasterExceptionShifts = document.getElementById('hfCurrentDate_MasterExceptionShifts').value;
    switch (parent.SysLangID) {
        case 'en-US':
            currentDate_MasterExceptionShifts = new Date(currentDate_MasterExceptionShifts);
            gdpMasterFromDate_MasterExceptionShifts.setSelectedDate(currentDate_MasterExceptionShifts);
            gCalMasterFromDate_MasterExceptionShifts.setSelectedDate(currentDate_MasterExceptionShifts);
            gdpMasterToDate_MasterExceptionShifts.setSelectedDate(currentDate_MasterExceptionShifts);
            gCalMasterToDate_MasterExceptionShifts.setSelectedDate(currentDate_MasterExceptionShifts);
            break;
        case 'fa-IR':
            document.getElementById('pdpMasterFromDate_MasterExceptionShifts').value = currentDate_MasterExceptionShifts;
            document.getElementById('pdpMasterToDate_MasterExceptionShifts').value = currentDate_MasterExceptionShifts;
            break;
    }
}

function tlbItemFormReconstruction_TlbMasterExceptionShifts_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvExceptionShiftsIntroduction_iFrame').src = 'MasterExceptionShifts.aspx';
}

function Refresh_GridExceptionShifts_ExceptionShifts() {
    Fill_GridMasterExceptionShifts_MasterExceptionShifts();
}

function GridMasterExceptionShifts_MasterExceptionShifts_onItemSelect(sender, e) {
    if (CurrentPageState_MasterExceptionShifts != 'Add')
        NavigateExceptionShift_MasterExceptionShifts(e.get_item());
}

function NavigateExceptionShift_MasterExceptionShifts(selectedExceptionShiftItem) {
    if (selectedExceptionShiftItem != undefined) {
        document.getElementById('cmbPersonnel_MasterExceptionShifts').value = selectedExceptionShiftItem.getMember('Person.Name').get_text();
        switch (parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpMasterFromDate_MasterExceptionShifts').value = document.getElementById('pdpMasterToDate_MasterExceptionShifts').value = selectedExceptionShiftItem.getMember('TheDate').get_text();
                break;
            case 'en-US':
                var date = new Date(selectedExceptionShiftItem.getMember('TheDate').get_text());
                gdpMasterFromDate_MasterExceptionShifts.setSelectedDate(date);
                gCalMasterFromDate_MasterExceptionShifts.setSelectedDate(date);
                gdpMasterToDate_MasterExceptionShifts.setSelectedDate(date);
                gCalMasterToDate_MasterExceptionShifts.setSelectedDate(date);
                break;
        }
    }
}

function tlbItemMonthlyExceptionShifts_TlbMasterExceptionShifts_onClick() {
    ShowDialogMonthlyExceptionShifts();
}

function ShowDialogMonthlyExceptionShifts() {
    var ObjDialogMonthlyExceptionShifts = new Object();
    ObjDialogMonthlyExceptionShifts.PersonnelLoadState = LoadState_cmbPersonnel_MasterExceptionShifts;
    ObjDialogMonthlyExceptionShifts.PersonnelSearchTerm = null;
    switch (LoadState_cmbPersonnel_MasterExceptionShifts) {
        case 'Normal':
            break;
        case 'Search':
            ObjDialogMonthlyExceptionShifts.PersonnelSearchTerm = document.getElementById('txtPersonnelSearch_MasterExceptionShifts').value;
            break;
        case 'AdvancedSearch':
            ObjDialogMonthlyExceptionShifts.PersonnelSearchTerm = AdvancedSearchTerm_cmbPersonnel_MasterExceptionShifts;
            break;
    }
    if (LoadState_cmbPersonnel_MasterExceptionShifts == 'Normal' || ((LoadState_cmbPersonnel_MasterExceptionShifts == 'Search' || LoadState_cmbPersonnel_MasterExceptionShifts == 'AdvancedSearch') && ObjDialogMonthlyExceptionShifts.PersonnelSearchTerm != null)) {
        parent.DialogMonthlyExceptionShifts.set_value(ObjDialogMonthlyExceptionShifts);
        parent.DialogMonthlyExceptionShifts.Show();
    }
}

















    
