
var CurrentPageState_WorkGroup = 'View';
var ConfirmState_WorkGroup = null;
var ObjWorkGroup_WorkGroup = null;

function tlbItemCalendar_TlbWorkGroups_onClick() { 
    ShowDialogCalendar('Normal');
}

function ShowDialogCalendar(CalViewState) {
    if (CurrentPageState_WorkGroup == 'Edit' && cmbYear_WorkGroup.getSelectedItem() != undefined && GridWorkGroups_WorkGroups.getSelectedItems().length > 0) {
        var ObjStateWorkGroup = new Object();
        ObjStateWorkGroup.Sender = 'WorkGroups';
        ObjStateWorkGroup.PageState = CurrentPageState_WorkGroup;
        ObjStateWorkGroup.CalViewState = CalViewState;
        ObjStateWorkGroup.GroupID = GridWorkGroups_WorkGroups.getSelectedItems()[0].getMember('ID').get_text();
        ObjStateWorkGroup.GroupName = GridWorkGroups_WorkGroups.getSelectedItems()[0].getMember('Name').get_text();
        ObjStateWorkGroup.Year = cmbYear_WorkGroup.getSelectedItem().get_value();
        ObjStateWorkGroup.UIYear = cmbYear_WorkGroup.getSelectedItem().get_text();
        parent.DialogCalendar.set_value(ObjStateWorkGroup);
        parent.DialogCalendar.ContentUrl = 'Calendar.aspx?reload='+(new Date()).getTime()+'&PageCaller=WorkGroups&CalViewState=' + ObjStateWorkGroup.CalViewState + '&GroupID=' + ObjStateWorkGroup.GroupID + '&UIYear=' + ObjStateWorkGroup.UIYear;
        parent.DialogCalendar.Show();
        CollapseControls_WorkGroups();
    }
}

function GetBoxesHeaders_WorkGroup() {
    document.getElementById('header_tblWorkGroupDetails_WorkGroups').innerHTML = document.getElementById('hfheader_tblWorkGroupDetails_WorkGroups').value;
    document.getElementById('header_WorkGroup_Group').innerHTML = document.getElementById('hfheader_WorkGroup_Group').value;
}

function GridWorkGroups_WorkGroups_onItemSelect(sender, e) {
    if (CurrentPageState_WorkGroup != 'Add')
        NavigateWorkGroup_WorkGroup(e.get_item());
}

function NavigateWorkGroup_WorkGroup(selectedWorkGroupItem) {
    if (selectedWorkGroupItem != undefined) {
        document.getElementById('txtGroupCode_WorkGroups').value = selectedWorkGroupItem.getMember('CustomCode').get_text();
        document.getElementById('txtGroupName_WorkGroups').value = selectedWorkGroupItem.getMember('Name').get_text();
    }
}

function tlbItemNew_TlbWorkGroups_onClick() {
    ChangePageState_WorkGroup('Add');
    ClearList_WorkGroup();
    FocusOnFirstElement_WorkGroup();
}

function tlbItemEdit_TlbWorkGroups_onClick() {
    ChangePageState_WorkGroup('Edit');
    FocusOnFirstElement_WorkGroup();
}

function tlbItemDelete_TlbWorkGroups_onClick() {
    ChangePageState_WorkGroup('Delete');
}

function tlbItemSave_TlbWorkGroups_onClick() {
    CollapseControls_WorkGroups();
    WorkGroup_onSave();
}

function tlbItemCancel_TlbWorkGroups_onClick() {
    ChangePageState_WorkGroup('View');
    ClearList_WorkGroup();
}

function tlbItemExit_TlbWorkGroups_onClick() {
    ShowDialogConfirm('Exit');
}

function WorkGroup_onSave() {
    if (CurrentPageState_WorkGroup != 'Delete')
        UpdateWorkGroup_WorkGroup();
    else
        ShowDialogConfirm('Delete');
}

function CharToKeyCode_WorkGroup(str) {
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


function ClearList_WorkGroup() {
    if (CurrentPageState_WorkGroup != 'Edit') {
        document.getElementById('txtGroupCode_WorkGroups').value = '';
        document.getElementById('txtGroupName_WorkGroups').value = '';
    }
}

function FocusOnFirstElement_WorkGroup() {
    document.getElementById('txtGroupCode_WorkGroups').focus();
}

function ChangePageState_WorkGroup(state) {
    CurrentPageState_WorkGroup = state;
    SetActionMode_WorkGroup(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbWorkGroups.get_items().getItemById('tlbItemNew_TlbWorkGroups') != null) {
            TlbWorkGroups.get_items().getItemById('tlbItemNew_TlbWorkGroups').set_enabled(false);
            TlbWorkGroups.get_items().getItemById('tlbItemNew_TlbWorkGroups').set_imageUrl('add_silver.png');
        }
        if (TlbWorkGroups.get_items().getItemById('tlbItemEdit_TlbWorkGroups') != null) {
            TlbWorkGroups.get_items().getItemById('tlbItemEdit_TlbWorkGroups').set_enabled(false);
            TlbWorkGroups.get_items().getItemById('tlbItemEdit_TlbWorkGroups').set_imageUrl('edit_silver.png');
        }
        if (TlbWorkGroups.get_items().getItemById('tlbItemDelete_TlbWorkGroups') != null) {
            TlbWorkGroups.get_items().getItemById('tlbItemDelete_TlbWorkGroups').set_enabled(false);
            TlbWorkGroups.get_items().getItemById('tlbItemDelete_TlbWorkGroups').set_imageUrl('remove_silver.png');
        }
        TlbWorkGroups.get_items().getItemById('tlbItemSave_TlbWorkGroups').set_enabled(true);
        TlbWorkGroups.get_items().getItemById('tlbItemSave_TlbWorkGroups').set_imageUrl('save.png');
        TlbWorkGroups.get_items().getItemById('tlbItemCancel_TlbWorkGroups').set_enabled(true);
        TlbWorkGroups.get_items().getItemById('tlbItemCancel_TlbWorkGroups').set_imageUrl('cancel.png');
        if (state == 'Edit' && TlbWorkGroups.get_items().getItemById('tlbItemCalendar_TlbWorkGroups') != null) {
            TlbWorkGroups.get_items().getItemById('tlbItemCalendar_TlbWorkGroups').set_enabled(true);
            TlbWorkGroups.get_items().getItemById('tlbItemCalendar_TlbWorkGroups').set_imageUrl('Calendar.png');
        }
        document.getElementById('txtGroupCode_WorkGroups').disabled = '';
        document.getElementById('txtGroupName_WorkGroups').disabled = '';
        cmbYear_WorkGroup.enable();
        if (state == 'Edit')
            NavigateWorkGroup_WorkGroup(GridWorkGroups_WorkGroups.getSelectedItems()[0]);
        if (state == 'Delete')
            WorkGroup_onSave();
    }
    if (state == 'View') {
        if (TlbWorkGroups.get_items().getItemById('tlbItemNew_TlbWorkGroups') != null) {
            TlbWorkGroups.get_items().getItemById('tlbItemNew_TlbWorkGroups').set_enabled(true);
            TlbWorkGroups.get_items().getItemById('tlbItemNew_TlbWorkGroups').set_imageUrl('add.png');
        }
        if (TlbWorkGroups.get_items().getItemById('tlbItemEdit_TlbWorkGroups') != null) {
            TlbWorkGroups.get_items().getItemById('tlbItemEdit_TlbWorkGroups').set_enabled(true);
            TlbWorkGroups.get_items().getItemById('tlbItemEdit_TlbWorkGroups').set_imageUrl('edit.png');
        }
        if (TlbWorkGroups.get_items().getItemById('tlbItemDelete_TlbWorkGroups') != null) {
            TlbWorkGroups.get_items().getItemById('tlbItemDelete_TlbWorkGroups').set_enabled(true);
            TlbWorkGroups.get_items().getItemById('tlbItemDelete_TlbWorkGroups').set_imageUrl('remove.png');
        }
        TlbWorkGroups.get_items().getItemById('tlbItemSave_TlbWorkGroups').set_enabled(false);
        TlbWorkGroups.get_items().getItemById('tlbItemSave_TlbWorkGroups').set_imageUrl('save_silver.png');
        TlbWorkGroups.get_items().getItemById('tlbItemCancel_TlbWorkGroups').set_enabled(false);
        TlbWorkGroups.get_items().getItemById('tlbItemCancel_TlbWorkGroups').set_imageUrl('cancel_silver.png');
        if (TlbWorkGroups.get_items().getItemById('tlbItemCalendar_TlbWorkGroups') != null) {
            TlbWorkGroups.get_items().getItemById('tlbItemCalendar_TlbWorkGroups').set_enabled(false);
            TlbWorkGroups.get_items().getItemById('tlbItemCalendar_TlbWorkGroups').set_imageUrl('Calendar_silver.png');
        }
        document.getElementById('txtGroupCode_WorkGroups').disabled = 'disabled';
        document.getElementById('txtGroupName_WorkGroups').disabled = 'disabled';
        cmbYear_WorkGroup.disable();
        var ObjCurrentYear = document.getElementById('hfCurrentYear_WorkGroup').value;
        ObjCurrentYear = eval('(' + ObjCurrentYear + ')');
        cmbYear_WorkGroup.selectItemByIndex(ObjCurrentYear.Index);
    }
}

function SetActionMode_WorkGroup(state) {
    document.getElementById('ActionMode_WorkGroup').innerHTML = document.getElementById("hf" + state + "_WorkGroup").value;
}

function Fill_GridWorkGroups_WorkGroups() {
    document.getElementById('loadingPanel_GridWorkGroup_WorkGroup').innerHTML = document.getElementById('hfloadingPanel_GridWorkGroup_WorkGroup').value;
    CallBack_GridWorkGroups_WorkGroups.callback();
}

function Refresh_GridGroup_Group() {
    Fill_GridWorkGroups_WorkGroups();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_WorkGroup = confirmState;
    if (CurrentPageState_WorkGroup == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_WorkGroup').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_WorkGroup').value;
    DialogConfirm.Show();
    CollapseControls_WorkGroups();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_WorkGroup) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateWorkGroup_WorkGroup();
            break;
        case 'Exit':
            ClearList_WorkGroup();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_WorkGroup('View');
}

function UpdateWorkGroup_WorkGroup() {
    ObjWorkGroup_WorkGroup = new Object();
    ObjWorkGroup_WorkGroup.CustomCode = null;
    ObjWorkGroup_WorkGroup.Name = null;
    ObjWorkGroup_WorkGroup.ID = '0';
    var SelectedItems_GridWorkGroups_WorkGroups = GridWorkGroups_WorkGroups.getSelectedItems();
    if (SelectedItems_GridWorkGroups_WorkGroups.length > 0)
        ObjWorkGroup_WorkGroup.ID = SelectedItems_GridWorkGroups_WorkGroups[0].getMember("ID").get_text();

    if (CurrentPageState_WorkGroup != 'Delete') {
        ObjWorkGroup_WorkGroup.CustomCode = document.getElementById('txtGroupCode_WorkGroups').value;
        ObjWorkGroup_WorkGroup.Name = document.getElementById('txtGroupName_WorkGroups').value;
    }
    UpdateWorkGroup_WorkGroupPage(CharToKeyCode_WorkGroup(CurrentPageState_WorkGroup), CharToKeyCode_WorkGroup(ObjWorkGroup_WorkGroup.ID), CharToKeyCode_WorkGroup(ObjWorkGroup_WorkGroup.CustomCode), CharToKeyCode_WorkGroup(ObjWorkGroup_WorkGroup.Name));
    DialogWaiting.Show();
}

function UpdateWorkGroup_WorkGroupPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_WorkGroup').value;
            Response[1] = document.getElementById('hfConnectionError_WorkGroup').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_WorkGroup();
            WorkGroup_OnAfterUpdate(Response);
            ChangePageState_WorkGroup('View');
        }
        else {
            if (CurrentPageState_WorkGroup == 'Delete')
                ChangePageState_WorkGroup('View');
        }
    }
}

function WorkGroup_OnAfterUpdate(Response) {
    if (ObjWorkGroup_WorkGroup != null) {
        var WorkGroupCode = ObjWorkGroup_WorkGroup.CustomCode;
        var WorkGroupName = ObjWorkGroup_WorkGroup.Name;

        var WorkGroupItem = null;
        GridWorkGroups_WorkGroups.beginUpdate();
        switch (CurrentPageState_WorkGroup) {
            case 'Add':
                WorkGroupItem = GridWorkGroups_WorkGroups.get_table().addEmptyRow(GridWorkGroups_WorkGroups.get_recordCount());
                WorkGroupItem.setValue(0, Response[3], false);
                GridWorkGroups_WorkGroups.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridWorkGroups_WorkGroups.selectByKey(Response[3], 0, false);
                WorkGroupItem = GridWorkGroups_WorkGroups.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridWorkGroups_WorkGroups.selectByKey(ObjWorkGroup_WorkGroup.ID, 0, false);
                GridWorkGroups_WorkGroups.deleteSelected();
                break;
        }
        if (CurrentPageState_WorkGroup != 'Delete') {
            WorkGroupItem.setValue(1, WorkGroupCode, false);
            WorkGroupItem.setValue(2, WorkGroupName, false);
        }
        GridWorkGroups_WorkGroups.endUpdate();
    }
}

function CallBack_GridWorkGroups_WorkGroups_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkGroups').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridWorkGroups_WorkGroups();
    }
}

function GridWorkGroups_WorkGroups_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridWorkGroup_WorkGroup').innerHTML = '';
}

function CallBack_GridWorkGroups_WorkGroups_onCallbackError() {
    document.getElementById('loadingPanel_GridWorkGroup_WorkGroup').innerHTML = '';
    ShowConnectionError_WorkGroups();
}

function ShowConnectionError_WorkGroups() {
    var error = document.getElementById('hfErrorType_WorkGroup').value;
    var errorBody = document.getElementById('hfConnectionError_WorkGroup').value;
    showDialog(error, errorBody, 'error');
}

function CollapseControls_WorkGroups() {
    cmbYear_WorkGroup.collapse();
}

function tlbItemFormReconstruction_TlbWorkGroups_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvWorkGroupsIntroduction_iFrame').src = 'WorkGroups.aspx';
}

function tlbItemHelp_TlbWorkGroups_onClick() {
    LoadHelpPage('tlbItemHelp_TlbWorkGroups');
}










