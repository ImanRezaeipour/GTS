
var CurrentPageState_YearlyHolidays = 'View';
var ObjYearlyHolidaysGroup_YearlyHolidays = null;
var ConfirmState_YearlyHolidays = null;

function GetBoxesHeaders_YearlyHolidays() {
    document.getElementById('header_YearlyHolidays_YearlyHolidays').innerHTML = document.getElementById('hfheader_YearlyHolidays_YearlyHolidays').value;
    document.getElementById('header_YearlyHolidayDetails_YearlyHolidays').innerHTML = document.getElementById('hfheader_YearlyHolidayDetails_YearlyHolidays').value;
}

function ShowDialogCalendar() {
    if (CurrentPageState_YearlyHolidays == 'Edit' && cmbYear_YearlyHolidays.getSelectedItem() != undefined && GridYearlyHolidays_YearlyHolidays.getSelectedItems().length > 0) {
        var ObjStateYearlyHolidaysGroup = new Object();
        ObjStateYearlyHolidaysGroup.Sender = 'Holidays';
        ObjStateYearlyHolidaysGroup.PageState = CurrentPageState_YearlyHolidays;
        ObjStateYearlyHolidaysGroup.CalViewState = 'Normal';
        ObjStateYearlyHolidaysGroup.GroupID = GridYearlyHolidays_YearlyHolidays.getSelectedItems()[0].getMember('ID').get_text();
        ObjStateYearlyHolidaysGroup.GroupName = GridYearlyHolidays_YearlyHolidays.getSelectedItems()[0].getMember('Name').get_text();
        ObjStateYearlyHolidaysGroup.Year = cmbYear_YearlyHolidays.getSelectedItem().get_value();
        ObjStateYearlyHolidaysGroup.UIYear = cmbYear_YearlyHolidays.getSelectedItem().get_text();
        parent.DialogCalendar.ContentUrl = 'Calendar.aspx?reload=' + (new Date()).getTime() + '&PageCaller=Holidays&CalViewState=Normal&GroupID=' + ObjStateYearlyHolidaysGroup.GroupID + '&UIYear=' + ObjStateYearlyHolidaysGroup.UIYear;
        parent.DialogCalendar.set_value(ObjStateYearlyHolidaysGroup);
        parent.DialogCalendar.Show();
        CollapseControls_YearlyHolidays();
    }
}

function tlbItemNew_TlbYearlyHolidays_onClick() {
    ChangePageState_YearlyHolidays('Add');
    ClearList_YearlyHolidays();
    FocusOnFirstElement_YearlyHolidays();
}

function ClearList_YearlyHolidays() {
    if (CurrentPageState_YearlyHolidays != 'Edit') {
        document.getElementById('txtHolidayGroupCustomCode_YearlyHolidays').value = '';
        document.getElementById('txtHolidayGroupTitle_YearlyHolidays').value = '';
        document.getElementById('txtDescription_YearlyHolidays').value = '';
    }
}

function FocusOnFirstElement_YearlyHolidays() {
    document.getElementById('txtHolidayGroupCustomCode_YearlyHolidays').focus();
}

function ChangePageState_YearlyHolidays(state) {
    CurrentPageState_YearlyHolidays = state;
    SetActionMode_YearlyHolidays(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbYearlyHolidays.get_items().getItemById('tlbItemNew_TlbYearlyHolidays') != null) {
            TlbYearlyHolidays.get_items().getItemById('tlbItemNew_TlbYearlyHolidays').set_enabled(false);
            TlbYearlyHolidays.get_items().getItemById('tlbItemNew_TlbYearlyHolidays').set_imageUrl('add_silver.png');
        }
        if (TlbYearlyHolidays.get_items().getItemById('tlbItemEdit_TlbYearlyHolidays') != null) {
            TlbYearlyHolidays.get_items().getItemById('tlbItemEdit_TlbYearlyHolidays').set_enabled(false);
            TlbYearlyHolidays.get_items().getItemById('tlbItemEdit_TlbYearlyHolidays').set_imageUrl('edit_silver.png');
        }
        if (TlbYearlyHolidays.get_items().getItemById('tlbItemDelete_TlbYearlyHolidays') != null) {
            TlbYearlyHolidays.get_items().getItemById('tlbItemDelete_TlbYearlyHolidays').set_enabled(false);
            TlbYearlyHolidays.get_items().getItemById('tlbItemDelete_TlbYearlyHolidays').set_imageUrl('remove_silver.png');
        }
        TlbYearlyHolidays.get_items().getItemById('tlbItemSave_TlbYearlyHolidays').set_enabled(true);
        TlbYearlyHolidays.get_items().getItemById('tlbItemSave_TlbYearlyHolidays').set_imageUrl('save.png');
        TlbYearlyHolidays.get_items().getItemById('tlbItemCancel_TlbYearlyHolidays').set_enabled(true);
        TlbYearlyHolidays.get_items().getItemById('tlbItemCancel_TlbYearlyHolidays').set_imageUrl('cancel.png');
        if (state == 'Edit' && TlbYearlyHolidays.get_items().getItemById('tlbItemHolidaysRegulation_TlbYearlyHolidays') != null) {
            TlbYearlyHolidays.get_items().getItemById('tlbItemHolidaysRegulation_TlbYearlyHolidays').set_enabled(true);
            TlbYearlyHolidays.get_items().getItemById('tlbItemHolidaysRegulation_TlbYearlyHolidays').set_imageUrl('BallClockRed.png');
        }
        document.getElementById('txtHolidayGroupTitle_YearlyHolidays').disabled = '';
        document.getElementById('txtDescription_YearlyHolidays').disabled = '';
        document.getElementById('txtHolidayGroupCustomCode_YearlyHolidays').disabled = '';
        cmbYear_YearlyHolidays.enable();
        if (state == 'Edit')
            NavigateYearlyHolidays_YearlyHolidays(GridYearlyHolidays_YearlyHolidays.getSelectedItems()[0]);
        if (state == 'Delete')
            YearlyHolidaysGroup_onSave();
    }
    if (state == 'View') {
        if (TlbYearlyHolidays.get_items().getItemById('tlbItemNew_TlbYearlyHolidays') != null) {
            TlbYearlyHolidays.get_items().getItemById('tlbItemNew_TlbYearlyHolidays').set_enabled(true);
            TlbYearlyHolidays.get_items().getItemById('tlbItemNew_TlbYearlyHolidays').set_imageUrl('add.png');
        }
        if (TlbYearlyHolidays.get_items().getItemById('tlbItemEdit_TlbYearlyHolidays') != null) {
            TlbYearlyHolidays.get_items().getItemById('tlbItemEdit_TlbYearlyHolidays').set_enabled(true);
            TlbYearlyHolidays.get_items().getItemById('tlbItemEdit_TlbYearlyHolidays').set_imageUrl('edit.png');
        }
        if (TlbYearlyHolidays.get_items().getItemById('tlbItemDelete_TlbYearlyHolidays') != null) {
            TlbYearlyHolidays.get_items().getItemById('tlbItemDelete_TlbYearlyHolidays').set_enabled(true);
            TlbYearlyHolidays.get_items().getItemById('tlbItemDelete_TlbYearlyHolidays').set_imageUrl('remove.png');
        }
        TlbYearlyHolidays.get_items().getItemById('tlbItemSave_TlbYearlyHolidays').set_enabled(false);
        TlbYearlyHolidays.get_items().getItemById('tlbItemSave_TlbYearlyHolidays').set_imageUrl('save_silver.png');
        TlbYearlyHolidays.get_items().getItemById('tlbItemCancel_TlbYearlyHolidays').set_enabled(false);
        TlbYearlyHolidays.get_items().getItemById('tlbItemCancel_TlbYearlyHolidays').set_imageUrl('cancel_silver.png');
        if (TlbYearlyHolidays.get_items().getItemById('tlbItemHolidaysRegulation_TlbYearlyHolidays') != null) {
            TlbYearlyHolidays.get_items().getItemById('tlbItemHolidaysRegulation_TlbYearlyHolidays').set_enabled(false);
            TlbYearlyHolidays.get_items().getItemById('tlbItemHolidaysRegulation_TlbYearlyHolidays').set_imageUrl('BallClockRed_silver.png');
        }
        document.getElementById('txtHolidayGroupTitle_YearlyHolidays').disabled = 'disabled';
        document.getElementById('txtDescription_YearlyHolidays').disabled = 'disabled';
        document.getElementById('txtHolidayGroupCustomCode_YearlyHolidays').disabled = 'disabled';
        cmbYear_YearlyHolidays.disable();
        var ObjCurrentYear = document.getElementById('hfCurrentYear_YearlyHolidays').value;
        ObjCurrentYear = eval('(' + ObjCurrentYear + ')');
        cmbYear_YearlyHolidays.selectItemByIndex(ObjCurrentYear.Index);
    }
}

function NavigateYearlyHolidays_YearlyHolidays(selectedYearlyHolidaysGroupItem) {
    if (selectedYearlyHolidaysGroupItem != undefined) {
        document.getElementById('txtHolidayGroupCustomCode_YearlyHolidays').value = selectedYearlyHolidaysGroupItem.getMember('CustomCode').get_text();
        document.getElementById('txtHolidayGroupTitle_YearlyHolidays').value = selectedYearlyHolidaysGroupItem.getMember('Name').get_text();
        document.getElementById('txtDescription_YearlyHolidays').value = selectedYearlyHolidaysGroupItem.getMember('Description').get_text();
    }
}

function YearlyHolidaysGroup_onSave() {
    if (CurrentPageState_YearlyHolidays != 'Delete')
        UpdateYearlyHolidaysGroup_YearlyHolidays();
    else
        ShowDialogConfirm('Delete');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_YearlyHolidays = confirmState;
    if (CurrentPageState_YearlyHolidays == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_YearlyHolidays').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_YearlyHolidays').value;
    DialogConfirm.Show();
    CollapseControls_YearlyHolidays();
}

function UpdateYearlyHolidaysGroup_YearlyHolidays() {
    ObjYearlyHolidaysGroup_YearlyHolidays = new Object();
    ObjYearlyHolidaysGroup_YearlyHolidays.CustomCode = null;
    ObjYearlyHolidaysGroup_YearlyHolidays.Title = null;
    ObjYearlyHolidaysGroup_YearlyHolidays.Description = null;
    ObjYearlyHolidaysGroup_YearlyHolidays.ID = '0';
    var SelectedItems_GridYearlyHolidays_YearlyHolidays = GridYearlyHolidays_YearlyHolidays.getSelectedItems();
    if (SelectedItems_GridYearlyHolidays_YearlyHolidays.length > 0)
        ObjYearlyHolidaysGroup_YearlyHolidays.ID = SelectedItems_GridYearlyHolidays_YearlyHolidays[0].getMember("ID").get_text();

    if (CurrentPageState_YearlyHolidays != 'Delete') {
        ObjYearlyHolidaysGroup_YearlyHolidays.CustomCode = document.getElementById('txtHolidayGroupCustomCode_YearlyHolidays').value;
        ObjYearlyHolidaysGroup_YearlyHolidays.Title = document.getElementById('txtHolidayGroupTitle_YearlyHolidays').value;
        ObjYearlyHolidaysGroup_YearlyHolidays.Description = document.getElementById('txtDescription_YearlyHolidays').value;
    }
    UpdateYearlyHolidaysGroup_YearlyHolidaysPage(CharToKeyCode_YearlyHolidays(CurrentPageState_YearlyHolidays), CharToKeyCode_YearlyHolidays(ObjYearlyHolidaysGroup_YearlyHolidays.ID), CharToKeyCode_YearlyHolidays(ObjYearlyHolidaysGroup_YearlyHolidays.CustomCode), CharToKeyCode_YearlyHolidays(ObjYearlyHolidaysGroup_YearlyHolidays.Title), CharToKeyCode_YearlyHolidays(ObjYearlyHolidaysGroup_YearlyHolidays.Description));
    DialogWaiting.Show();
}

function UpdateYearlyHolidaysGroup_YearlyHolidaysPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_YearlyHolidays').value;
            Response[1] = document.getElementById('hfConnectionError_YearlyHolidays').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_YearlyHolidays();
            YearlyHolidaysGroup_OnAfterUpdate(Response);
            ChangePageState_YearlyHolidays('View');
        }
        else {
            if (CurrentPageState_YearlyHolidays == 'Delete')
                ChangePageState_YearlyHolidays('View');
        }
    }
}

function YearlyHolidaysGroup_OnAfterUpdate(Response) {
    if (ObjYearlyHolidaysGroup_YearlyHolidays != null) {
        var YearlyHolidaysGroupCustomCode = ObjYearlyHolidaysGroup_YearlyHolidays.CustomCode;
        var YearlyHolidaysGroupTitle = ObjYearlyHolidaysGroup_YearlyHolidays.Title;
        var YearlyHolidaysGroupDescription = ObjYearlyHolidaysGroup_YearlyHolidays.Description;

        var YearlyHolidaysGroupItem = null;
        GridYearlyHolidays_YearlyHolidays.beginUpdate();
        switch (CurrentPageState_YearlyHolidays) {
            case 'Add':
                YearlyHolidaysGroupItem = GridYearlyHolidays_YearlyHolidays.get_table().addEmptyRow(GridYearlyHolidays_YearlyHolidays.get_recordCount());
                YearlyHolidaysGroupItem.setValue(0, Response[3], false);
                GridYearlyHolidays_YearlyHolidays.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridYearlyHolidays_YearlyHolidays.selectByKey(Response[3], 0, false);
                YearlyHolidaysGroupItem = GridYearlyHolidays_YearlyHolidays.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridYearlyHolidays_YearlyHolidays.selectByKey(ObjYearlyHolidaysGroup_YearlyHolidays.ID, 0, false);
                GridYearlyHolidays_YearlyHolidays.deleteSelected();
                break;
        }
        if (CurrentPageState_YearlyHolidays != 'Delete') {
            YearlyHolidaysGroupItem.setValue(1, YearlyHolidaysGroupCustomCode, false);
            YearlyHolidaysGroupItem.setValue(2, YearlyHolidaysGroupTitle, false);
            YearlyHolidaysGroupItem.setValue(3, YearlyHolidaysGroupDescription, false);
        }
        GridYearlyHolidays_YearlyHolidays.endUpdate();
    }
}

function CharToKeyCode_YearlyHolidays(str) {
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

function SetActionMode_YearlyHolidays(state) {
    document.getElementById('ActionMode_YearlyHolidays').innerHTML = document.getElementById("hf" + state + "_YearlyHolidays").value;
}

function tlbItemEdit_TlbYearlyHolidays_onClick() {
    ChangePageState_YearlyHolidays('Edit');
    FocusOnFirstElement_YearlyHolidays();
}

function tlbItemDelete_TlbYearlyHolidays_onClick() {
    ChangePageState_YearlyHolidays('Delete');
}

function tlbItemHolidaysRegulation_TlbYearlyHolidays_onClick() {
    ShowDialogCalendar();
}

function tlbItemSave_TlbYearlyHolidays_onClick() {
    CollapseControls_YearlyHolidays();
    YearlyHolidaysGroup_onSave();
}

function tlbItemCancel_TlbYearlyHolidays_onClick() {
    ChangePageState_YearlyHolidays('View');
    ClearList_YearlyHolidays();
}

function tlbItemExit_TlbYearlyHolidays_onClick() {
    ShowDialogConfirm('Exit');
}

function Refresh_YearlyHolidays_YearlyHolidays() {
    Fill_GridYearlyHolidays_YearlyHolidays();
}

function Fill_GridYearlyHolidays_YearlyHolidays() {
    document.getElementById('loadingPanel_GridYearlyHolidays_YearlyHolidays').innerHTML = document.getElementById('hfloadingPanel_GridYearlyHolidays_YearlyHolidays').value;
    CallBack_GridYearlyHolidays_YearlyHolidays.callback();
}

function GridYearlyHolidays_YearlyHolidays_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridYearlyHolidays_YearlyHolidays').innerHTML = '';
}

function CallBack_GridYearlyHolidays_YearlyHolidays_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_YearlyHolidays').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridYearlyHolidays_YearlyHolidays();
    }
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_YearlyHolidays) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateYearlyHolidaysGroup_YearlyHolidays();
            break;
        case 'Exit':
            ClearList_YearlyHolidays();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_YearlyHolidays('View');
}

function CallBack_GridYearlyHolidays_YearlyHolidays_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridYearlyHolidays_YearlyHolidays').innerHTML = '';
    ShowConnectionError_YearlyHolidays();
}

function ShowConnectionError_YearlyHolidays() {
    var error = document.getElementById('hfErrorType_YearlyHolidays').value;
    var errorBody = document.getElementById('hfConnectionError_YearlyHolidays').value;
    showDialog(error, errorBody, 'error');
}

function CollapseControls_YearlyHolidays() {
    cmbYear_YearlyHolidays.collapse();
}

function tlbItemFormReconstruction_TlbYearlyHolidays_onClick() {
    parent.DialogLoading.Show();    
    parent.document.getElementById('pgvYearlyHolidaysIntroduction_iFrame').src = 'YearlyHolidays.aspx';
}

function tlbItemHelp_TlbYearlyHolidays_onClick() {
    LoadHelpPage('tlbItemHelp_TlbYearlyHolidays');
}





