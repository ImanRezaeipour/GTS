
var ConfirmState_MonthlyExceptionShifts = null;
var CurrentPageState_MonthlyExceptionShifts = 'View';
var Basefooter_GridMonthlyExceptionShifts_MonthlyExceptionShifts = null;
var CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts = 0;
var ObjMonthlyExceptionShifts_MonthlyExceptionShifts = null;

function GettBoxesHeaders_MonthlyExceptionShifts() {
    parent.document.getElementById('Title_DialogMonthlyExceptionShifts').innerHTML = document.getElementById('hfTitle_DialogMonthlyExceptionShifts').value;
    document.getElementById('header_MonthlyExceptionShifts_MonthlyExceptionShifts').innerHTML = document.getElementById('hfheader_MonthlyExceptionShifts_MonthlyExceptionShifts').value;
    Basefooter_GridMonthlyExceptionShifts_MonthlyExceptionShifts = document.getElementById('footer_GridMonthlyExceptionShifts_MonthlyExceptionShifts').innerHTML = document.getElementById('hffooter_GridMonthlyExceptionShifts_MonthlyExceptionShifts').value;
}

function tlbItemHelp_TlbMonthlyExceptionShifts_onClick() {
    LoadHelpPage('tlbItemHelp_TlbMonthlyExceptionShifts');
}

function tlbItemFormReconstruction_TlbMonthlyExceptionShifts_onClick() {
    parent.DialogMonthlyExceptionShifts.Close();
    parent.DialogMonthlyExceptionShifts.Show();
}

function tlbItemExit_TlbMonthlyExceptionShifts_onClick() {
    ShowDialogConfirm('Exit');
}

function cmbYear_MonthlyExceptionShifts_onChange(sender, e) {
    document.getElementById('hfCurrentYear_MonthlyExceptionShifts').value = cmbYear_MonthlyExceptionShifts.getSelectedItem().get_value();
}

function cmbMonth_MonthlyExceptionShifts_onChange(sender, e) {
    document.getElementById('hfCurrentMonth_MonthlyExceptionShifts').value = cmbMonth_MonthlyExceptionShifts.getSelectedItem().get_value();
}

function tlbItemView_TlbView_MonthlyExceptionShifts_onClick() {
    SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(0);
}

function GridMonthlyExceptionShifts_MonthlyExceptionShifts_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridMonthlyExceptionShifts_MonthlyExceptionShifts').innerHTML = '';
}

function CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MonthlyExceptionShifts').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else {
        Changefooter_GridMonthlyExceptionShifts_MonthlyExceptionShifts();
    }
}

function CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onCallbackError(sender, e) {
    ShowConnectionError_MonthlyExceptionShifts();
}

function tlbItemRefresh_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick() {
    Refresh_GridMonthlyExceptionShifts_MonthlyExceptionShifts();
}

function tlbItemFirst_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick() {
    SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(0);
}

function tlbItemBefore_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick() {
    if (CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts != 0) {
        CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts = CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts - 1;
        SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts);
    }
}

function tlbItemNext_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick() {
    if (CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts < parseInt(document.getElementById('hfMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts').value) - 1) {
        CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts = CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts + 1;
        SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts);
    }
}

function tlbItemLast_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick() {
    SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(parseInt(document.getElementById('hfMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts').value) - 1);
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_MonthlyExceptionShifts) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateMonthlyExceptionShifts_MonthlyExceptionShifts();
            break;
        case 'Exit':
            CloseDialogMonthlyExceptionShifts();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function EditGridMonthlyExceptionShifts_MonthlyExceptionShifts(rowID) {
    CurrentPageState_MonthlyExceptionShifts = 'Edit';
    GridMonthlyExceptionShifts_MonthlyExceptionShifts.edit(GridMonthlyExceptionShifts_MonthlyExceptionShifts.getItemFromClientId(rowID));
}

function DeleteGridMonthlyExceptionShifts_MonthlyExceptionShifts(rowID) {
    CurrentPageState_MonthlyExceptionShifts = 'Delete';
    ShowDialogConfirm('Delete');
}

function UpdateGridMonthlyExceptionShifts_MonthlyExceptionShifts() {
    GridMonthlyExceptionShifts_MonthlyExceptionShifts.editComplete();
    UpdateMonthlyExceptionShifts_MonthlyExceptionShifts();
}

function UpdateMonthlyExceptionShifts_MonthlyExceptionShifts() {
    var SelectedItems_MasterExceptionShifts_MasterExceptionShifts = GridMonthlyExceptionShifts_MonthlyExceptionShifts.getSelectedItems();
    if (SelectedItems_MasterExceptionShifts_MasterExceptionShifts.length > 0) {
        ObjMonthlyExceptionShifts_MonthlyExceptionShifts = new Object();
        ObjMonthlyExceptionShifts_MonthlyExceptionShifts.ID = SelectedItems_MasterExceptionShifts_MasterExceptionShifts[0].getMember('ID').get_text();
        var PersonnelID = SelectedItems_MasterExceptionShifts_MasterExceptionShifts[0].getMember('PersonID').get_text();
        var Year = document.getElementById('hfCurrentYear_MonthlyExceptionShifts').value;
        var Month = document.getElementById('hfCurrentMonth_MonthlyExceptionShifts').value;
        var StrDaysShiftCol = '';
        var ArDaysShiftCol = {};
        var separator = '&';
        if (CurrentPageState_MonthlyExceptionShifts == 'Edit') {
            var ColumnsCol_MasterExceptionShifts_MasterExceptionShifts = GridMonthlyExceptionShifts_MonthlyExceptionShifts.get_table().get_columns();
            for (var i = 5; i < ColumnsCol_MasterExceptionShifts_MasterExceptionShifts.length - 1; i++) {
                var gridColumn_MasterExceptionShifts_MasterExceptionShifts = ColumnsCol_MasterExceptionShifts_MasterExceptionShifts[i];
                if (gridColumn_MasterExceptionShifts_MasterExceptionShifts.get_visible()) {
                    var DayShiftVal = SelectedItems_MasterExceptionShifts_MasterExceptionShifts[0].getMember(gridColumn_MasterExceptionShifts_MasterExceptionShifts.get_dataField()).get_text();
                    ArDaysShiftCol[ColumnsCol_MasterExceptionShifts_MasterExceptionShifts[i].get_dataField()] = DayShiftVal;
                }
            }
            StrDaysShiftCol = JSON.stringify(ArDaysShiftCol);
        }
        UpdateMonthlyExceptionShifts_MonthlyExceptionShiftsPage(CharToKeyCode_MonthlyExceptionShifts(CurrentPageState_MonthlyExceptionShifts), CharToKeyCode_MonthlyExceptionShifts(PersonnelID), CharToKeyCode_MonthlyExceptionShifts(Year), CharToKeyCode_MonthlyExceptionShifts(Month), CharToKeyCode_MonthlyExceptionShifts(StrDaysShiftCol));
        DialogWaiting.Show();
    }
}

function UpdateMonthlyExceptionShifts_MonthlyExceptionShiftsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (RetMessage[2] == 'success') {
            if (CurrentPageState_MonthlyExceptionShifts == 'Delete')
                DeletePersonnelMonthlyExceptionShifts_GridMonthlyExceptionShifts_MonthlyExceptionShifts();
                CurrentPageState_MonthlyExceptionShifts = 'View';
        }
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function DeletePersonnelMonthlyExceptionShifts_GridMonthlyExceptionShifts_MonthlyExceptionShifts() {
    if (ObjMonthlyExceptionShifts_MonthlyExceptionShifts != null) {
        GridMonthlyExceptionShifts_MonthlyExceptionShifts.beginUpdate();
        MonthlyExceptionShiftsItem = GridMonthlyExceptionShifts_MonthlyExceptionShifts.getItemFromKey(0, ObjMonthlyExceptionShifts_MonthlyExceptionShifts.ID);
        GridMonthlyExceptionShifts_MonthlyExceptionShifts.selectByKey(ObjMonthlyExceptionShifts_MonthlyExceptionShifts.ID, 0, false);
        var ColumnsCol_MasterExceptionShifts_MasterExceptionShifts = GridMonthlyExceptionShifts_MonthlyExceptionShifts.get_table().get_columns();
        for (var i = 5; i < ColumnsCol_MasterExceptionShifts_MasterExceptionShifts.length - 1; i++) {
            var gridColumn_MasterExceptionShifts_MasterExceptionShifts = ColumnsCol_MasterExceptionShifts_MasterExceptionShifts[i];
            if (gridColumn_MasterExceptionShifts_MasterExceptionShifts.get_visible()) 
                MonthlyExceptionShiftsItem.setValue(i, '', false);
        }
        GridMonthlyExceptionShifts_MonthlyExceptionShifts.endUpdate();
    }
}

function SetCellTitle_GridMonthlyExceptionShifts_MonthlyExceptionShifts(state) {
    return document.getElementById('hf' + state + '_MonthlyExceptionShifts').value;
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_MonthlyExceptionShifts = confirmState;
    if (ConfirmState_MonthlyExceptionShifts == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_MonthlyExceptionShifts').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_MonthlyExceptionShifts').value;
    DialogConfirm.Show();
}

function SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(pageIndex) {
    CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts = pageIndex;
    Fill_GridMonthlyExceptionShifts_MonthlyExceptionShifts(pageIndex);
}

function Fill_GridMonthlyExceptionShifts_MonthlyExceptionShifts(pageIndex) {
    document.getElementById('loadingPanel_GridMonthlyExceptionShifts_MonthlyExceptionShifts').innerHTML = document.getElementById('hfloadingPanel_GridMonthlyExceptionShifts_MonthlyExceptionShifts').value;
    var pageSize = parseInt(document.getElementById('hfMonthlyExceptionShiftsPageSize_MonthlyExceptionShifts').value);
    var PersonnelLoadStateConditions = '';
    var ObjDialogMonthlyExceptionShifts = parent.DialogMonthlyExceptionShifts.get_value();
    var PersonnelLoadState = ObjDialogMonthlyExceptionShifts.PersonnelLoadState;
    var PersonnelSearchTerm = ObjDialogMonthlyExceptionShifts.PersonnelSearchTerm;
    var Year = document.getElementById('hfCurrentYear_MonthlyExceptionShifts').value;
    var Month = document.getElementById('hfCurrentMonth_MonthlyExceptionShifts').value;

    CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts.callback(CharToKeyCode_MonthlyExceptionShifts(Year), CharToKeyCode_MonthlyExceptionShifts(Month), CharToKeyCode_MonthlyExceptionShifts(pageSize.toString()), CharToKeyCode_MonthlyExceptionShifts(pageIndex.toString()), CharToKeyCode_MonthlyExceptionShifts(PersonnelLoadState), CharToKeyCode_MonthlyExceptionShifts(PersonnelSearchTerm));
}

function CharToKeyCode_MonthlyExceptionShifts(str) {
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

function Changefooter_GridMonthlyExceptionShifts_MonthlyExceptionShifts() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridMonthlyExceptionShifts_MonthlyExceptionShifts').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts').value) > 0 ? CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridMonthlyExceptionShifts_MonthlyExceptionShifts').innerHTML = retfooterVal;
}

function ShowConnectionError_MonthlyExceptionShifts() {
    var error = document.getElementById('hfErrorType_MonthlyExceptionShifts').value;
    var errorBody = document.getElementById('hfConnectionError_MonthlyExceptionShifts').value;
    showDialog(error, errorBody, 'error');
}

function Refresh_GridMonthlyExceptionShifts_MonthlyExceptionShifts() {
    CurrentPageState_MonthlyExceptionShifts = 'View';
    SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(0);
}

function CloseDialogMonthlyExceptionShifts() {
    parent.document.getElementById('DialogMonthlyExceptionShifts_IFrame').src = 'WhitePage.aspx';
    parent.DialogMonthlyExceptionShifts.Close();
}









