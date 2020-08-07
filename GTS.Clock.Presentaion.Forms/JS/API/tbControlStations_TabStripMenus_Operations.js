var CurrentPageState_ControlStations = 'View';
var ConfirmState_ControlStations = null;
var ObjControlStation_ControlStationss = null;

function GetBoxesHeaders_ControlStations() {
    document.getElementById('header_ControlStationDetails_ControlStations').innerHTML = document.getElementById('hfheader_ControlStationDetails_ControlStations').value;
    document.getElementById('header_ControlStations_ControlStations').innerHTML = document.getElementById('hfheader_ControlStations_ControlStations').value;
}


function tlbItemNew_TlbControlStation_onClick() {
    ChangePageState_ControlStations('Add');
    ClearList_ControlStations();
    FocusOnFirstElement_ControlStations();
}

function tlbItemEdit_TlbControlStation_onClick() {
    ChangePageState_ControlStations('Edit');
    FocusOnFirstElement_ControlStations();
}

function tlbItemDelete_TlbControlStation_onClick() {
    ChangePageState_ControlStations('Delete');
    ChangePageState_ControlStations('Delete');
}

function tlbItemSave_TlbControlStation_onClick() {
    ControlStation_onSave();
}

function tlbItemCancel_TlbControlStation_onClick() {
    ChangePageState_ControlStations('View');
    ClearList_ControlStations();
}

function tlbItemExit_TlbControlStation_onClick() {
    ShowDialogConfirm('Exit');
}

function Refresh_GridControlStations_ControlStations() {
    Fill_GridControlStations_ControlStations();
}

function GridControlStations_ControlStations_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridControlStations_ControlStations').innerHTML = '';
}

function GridControlStations_ControlStations_onItemSelect(sender, e) {
    if (CurrentPageState_ControlStations != 'Add')
        NavigateControlStation_ControlStations(e.get_item());
}

function CallBack_GridControlStations_ControlStations_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ControlStations').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridControlStations_ControlStations();
    }
}

function CallBack_GridControlStations_ControlStations_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridControlStations_ControlStations').innerHTML = '';
    ShowConnectionError_ControlStations();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_ControlStations) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateControlStation_ControlStations();
            break;
        case 'Exit':
            ClearList_ControlStations();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_ControlStations('View');
}

function NavigateControlStation_ControlStations(selectedControlStationItem) {
    if (selectedControlStationItem != undefined) {
        document.getElementById('txtControlStationCode_ControlStations').value = selectedControlStationItem.getMember('CustomCode').get_text();
        document.getElementById('txtControlStationName_ControlStations').value = selectedControlStationItem.getMember('Name').get_text();
    }
}

function ControlStation_onSave() {
    if (CurrentPageState_ControlStations != 'Delete')
        UpdateControlStation_ControlStations();
    else
        ShowDialogConfirm('Delete');
}

function CharToKeyCode_ControlStations(str) {
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


function ClearList_ControlStations() {
    if (CurrentPageState_ControlStations != 'Edit') {
        document.getElementById('txtControlStationCode_ControlStations').value = '';
        document.getElementById('txtControlStationName_ControlStations').value = '';
        GridControlStations_ControlStations.unSelectAll();
    }
}

function FocusOnFirstElement_ControlStations() {
    document.getElementById('txtControlStationCode_ControlStations').focus();
}

function ChangePageState_ControlStations(state) {
    CurrentPageState_ControlStations = state;
    SetActionMode_ControlStations(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbControlStation.get_items().getItemById('tlbItemNew_TlbControlStation') != null) {
            TlbControlStation.get_items().getItemById('tlbItemNew_TlbControlStation').set_enabled(false);
            TlbControlStation.get_items().getItemById('tlbItemNew_TlbControlStation').set_imageUrl('add_silver.png');
        }
        if (TlbControlStation.get_items().getItemById('tlbItemEdit_TlbControlStation') != null) {
            TlbControlStation.get_items().getItemById('tlbItemEdit_TlbControlStation').set_enabled(false);
            TlbControlStation.get_items().getItemById('tlbItemEdit_TlbControlStation').set_imageUrl('edit_silver.png');
        }
        if (TlbControlStation.get_items().getItemById('tlbItemDelete_TlbControlStation') != null) {
            TlbControlStation.get_items().getItemById('tlbItemDelete_TlbControlStation').set_enabled(false);
            TlbControlStation.get_items().getItemById('tlbItemDelete_TlbControlStation').set_imageUrl('remove_silver.png');
        }
        TlbControlStation.get_items().getItemById('tlbItemSave_TlbControlStation').set_enabled(true);
        TlbControlStation.get_items().getItemById('tlbItemSave_TlbControlStation').set_imageUrl('save.png');
        TlbControlStation.get_items().getItemById('tlbItemCancel_TlbControlStation').set_enabled(true);
        TlbControlStation.get_items().getItemById('tlbItemCancel_TlbControlStation').set_imageUrl('cancel.png');
        document.getElementById('txtControlStationCode_ControlStations').disabled = '';
        document.getElementById('txtControlStationName_ControlStations').disabled = '';
        if (state == 'Edit')
            NavigateControlStation_ControlStations(GridControlStations_ControlStations.getSelectedItems()[0]);
        if (state == 'Delete')
            ControlStation_onSave();
    }
    if (state == 'View') {
        if (TlbControlStation.get_items().getItemById('tlbItemNew_TlbControlStation') != null) {
            TlbControlStation.get_items().getItemById('tlbItemNew_TlbControlStation').set_enabled(true);
            TlbControlStation.get_items().getItemById('tlbItemNew_TlbControlStation').set_imageUrl('add.png');
        }
        if (TlbControlStation.get_items().getItemById('tlbItemEdit_TlbControlStation') != null) {
            TlbControlStation.get_items().getItemById('tlbItemEdit_TlbControlStation').set_enabled(true);
            TlbControlStation.get_items().getItemById('tlbItemEdit_TlbControlStation').set_imageUrl('edit.png');
        }
        if (TlbControlStation.get_items().getItemById('tlbItemDelete_TlbControlStation') != null) {
            TlbControlStation.get_items().getItemById('tlbItemDelete_TlbControlStation').set_enabled(true);
            TlbControlStation.get_items().getItemById('tlbItemDelete_TlbControlStation').set_imageUrl('remove.png');
        }
        TlbControlStation.get_items().getItemById('tlbItemSave_TlbControlStation').set_enabled(false);
        TlbControlStation.get_items().getItemById('tlbItemSave_TlbControlStation').set_imageUrl('save_silver.png');
        TlbControlStation.get_items().getItemById('tlbItemCancel_TlbControlStation').set_enabled(false);
        TlbControlStation.get_items().getItemById('tlbItemCancel_TlbControlStation').set_imageUrl('cancel_silver.png');
        document.getElementById('txtControlStationCode_ControlStations').disabled = 'disabled';
        document.getElementById('txtControlStationName_ControlStations').disabled = 'disabled';
    }
}

function SetActionMode_ControlStations(state) {
    document.getElementById('ActionMode_ControlStations').innerHTML = document.getElementById("hf" + state + "_ControlStations").value;
}

function Fill_GridControlStations_ControlStations() {
    document.getElementById('loadingPanel_GridControlStations_ControlStations').innerHTML = document.getElementById('hfloadingPanel_GridControlStations_ControlStations').value;
    CallBack_GridControlStations_ControlStations.callback();
}

function Refresh_GridControlStations_ControlStationss() {
    Fill_GridControlStations_ControlStationss();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_ControlStations = confirmState;
    if (CurrentPageState_ControlStations == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_ControlStations').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_ControlStations').value;
    DialogConfirm.Show();
}


function UpdateControlStation_ControlStations() {
    ObjControlStation_ControlStations = new Object();
    ObjControlStation_ControlStations.CustomCode = null;
    ObjControlStation_ControlStations.Name = null;
    ObjControlStation_ControlStations.Description = null;
    ObjControlStation_ControlStations.ID = '0';
    var SelectedItems_GridControlStations_ControlStations = GridControlStations_ControlStations.getSelectedItems();
    if (SelectedItems_GridControlStations_ControlStations.length > 0)
        ObjControlStation_ControlStations.ID = SelectedItems_GridControlStations_ControlStations[0].getMember("ID").get_text();

    if (CurrentPageState_ControlStations != 'Delete') {
        ObjControlStation_ControlStations.CustomCode = document.getElementById('txtControlStationCode_ControlStations').value;
        ObjControlStation_ControlStations.Name = document.getElementById('txtControlStationName_ControlStations').value;
    }
    UpdateControlStation_ControlStationsPage(CharToKeyCode_ControlStations(CurrentPageState_ControlStations), CharToKeyCode_ControlStations(ObjControlStation_ControlStations.ID), CharToKeyCode_ControlStations(ObjControlStation_ControlStations.CustomCode), CharToKeyCode_ControlStations(ObjControlStation_ControlStations.Name));
    DialogWaiting.Show();
}

function UpdateControlStation_ControlStationsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_ControlStations').value;
            Response[1] = document.getElementById('hfConnectionError_ControlStations').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_ControlStations();
            ControlStation_OnAfterUpdate(Response);
            ChangePageState_ControlStations('View');
        }
        else {
            if (CurrentPageState_ControlStations == 'Delete')
                ChangePageState_ControlStations('View');
        }
    }
}

function ControlStation_OnAfterUpdate(Response) {
    if (ObjControlStation_ControlStations != null) {
        var ControlStationCode = ObjControlStation_ControlStations.CustomCode;
        var ControlStationName = ObjControlStation_ControlStations.Name;
        var ControlStationDescription = ObjControlStation_ControlStations.Description;

        var ControlStationItem = null;
        GridControlStations_ControlStations.beginUpdate();
        switch (CurrentPageState_ControlStations) {
            case 'Add':
                ControlStationItem = GridControlStations_ControlStations.get_table().addEmptyRow(GridControlStations_ControlStations.get_recordCount());
                ControlStationItem.setValue(0, Response[3], false);
                GridControlStations_ControlStations.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridControlStations_ControlStations.selectByKey(Response[3], 0, false);
                ControlStationItem = GridControlStations_ControlStations.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridControlStations_ControlStations.selectByKey(ObjControlStation_ControlStations.ID, 0, false);
                GridControlStations_ControlStations.deleteSelected();
                break;
        }
        if (CurrentPageState_ControlStations != 'Delete') {
            ControlStationItem.setValue(1, ControlStationCode, false);
            ControlStationItem.setValue(2, ControlStationName, false);
            ControlStationItem.setValue(3, ControlStationDescription, false);
        }
        GridControlStations_ControlStations.endUpdate();
    }
}

function ShowConnectionError_ControlStation() {
    var error = document.getElementById('hfErrorType_ControlStation').value;
    var errorBody = document.getElementById('hfConnectionError_ControlStation').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbControlStation_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvControlStationIntroduction_iFrame').src = 'ControlStations.aspx';
}

function tlbItemHelp_TlbControlStation_onClick() {
    LoadHelpPage('tlbItemHelp_TlbControlStation');
}


