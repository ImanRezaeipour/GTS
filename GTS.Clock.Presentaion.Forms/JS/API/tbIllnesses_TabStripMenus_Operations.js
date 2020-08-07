
var CurrentPageState_Illness = 'View';
var ConfirmState_Illness = null;
var ObjIllness_Illness = null;

function GetBoxesHeaders_Illness() {
    document.getElementById('header_Illnesses_Illness').innerHTML = document.getElementById('hfheader_Illnesses_Illness').value;
    document.getElementById('header_tblIllness_Illness').innerHTML = document.getElementById('hfheader_tblIllness_Illness').value;
}

function GridIllness_Illness_onItemSelect(sender, e) {
    if (CurrentPageState_Illness != 'Add')
        NavigateIllness_Illness(e.get_item());
}

function NavigateIllness_Illness(selectedIllnessItem) {
    if (selectedIllnessItem != undefined) {
        document.getElementById('txtIllnessName_Illness').value = selectedIllnessItem.getMember('Name').get_text();
        document.getElementById('txtDescription_Illness').value = selectedIllnessItem.getMember('Description').get_text();
    }
}


function tlbItemNew_TlbIllness_onClick() {
    ChangePageState_Illness('Add');
    ClearList_Illness();
    FocusOnFirstElement_Illness();
}

function tlbItemEdit_TlbIllness_onClick() {
    ChangePageState_Illness('Edit');
    FocusOnFirstElement_Illness();
}

function tlbItemDelete_TlbIllness_onClick() {
    ChangePageState_Illness('Delete');
}

function tlbItemSave_TlbIllness_onClick() {
    Illness_onSave();
}

function tlbItemCancel_TlbIllness_onClick() {
    ChangePageState_Illness('View');
    ClearList_Illness();
}

function tlbItemExit_TlbIllness_onClick() {
    ShowDialogConfirm('Exit');
}

function Illness_onSave() {
    if (CurrentPageState_Illness != 'Delete')
        UpdateIllness_Illness();
    else
        ShowDialogConfirm('Delete');
}

function CharToKeyCode_Illness(str) {
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


function ClearList_Illness() {
    if (CurrentPageState_Illness != 'Edit') {
        document.getElementById('txtIllnessName_Illness').value = '';
        document.getElementById('txtDescription_Illness').value = '';
        GridIllness_Illness.unSelectAll();
    }
}

function FocusOnFirstElement_Illness() {
    document.getElementById('txtIllnessName_Illness').focus();
}

function ChangePageState_Illness(state) {
    CurrentPageState_Illness = state;
    SetActionMode_Illness(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbIllness.get_items().getItemById('tlbItemNew_TlbIllness') != null) {
            TlbIllness.get_items().getItemById('tlbItemNew_TlbIllness').set_enabled(false);
            TlbIllness.get_items().getItemById('tlbItemNew_TlbIllness').set_imageUrl('add_silver.png');
        }
        if (TlbIllness.get_items().getItemById('tlbItemEdit_TlbIllness') != null) {
            TlbIllness.get_items().getItemById('tlbItemEdit_TlbIllness').set_enabled(false);
            TlbIllness.get_items().getItemById('tlbItemEdit_TlbIllness').set_imageUrl('edit_silver.png');
        }
        if (TlbIllness.get_items().getItemById('tlbItemDelete_TlbIllness') != null) {
            TlbIllness.get_items().getItemById('tlbItemDelete_TlbIllness').set_enabled(false);
            TlbIllness.get_items().getItemById('tlbItemDelete_TlbIllness').set_imageUrl('remove_silver.png');
        }
        TlbIllness.get_items().getItemById('tlbItemSave_TlbIllness').set_enabled(true);
        TlbIllness.get_items().getItemById('tlbItemSave_TlbIllness').set_imageUrl('save.png');
        TlbIllness.get_items().getItemById('tlbItemCancel_TlbIllness').set_enabled(true);
        TlbIllness.get_items().getItemById('tlbItemCancel_TlbIllness').set_imageUrl('cancel.png');
        document.getElementById('txtIllnessName_Illness').disabled = '';
        document.getElementById('txtDescription_Illness').disabled = '';
        if (state == 'Edit')
            NavigateIllness_Illness(GridIllness_Illness.getSelectedItems()[0]);
        if (state == 'Delete')
            Illness_onSave();
    }
    if (state == 'View') {
        if (TlbIllness.get_items().getItemById('tlbItemNew_TlbIllness') != null) {
            TlbIllness.get_items().getItemById('tlbItemNew_TlbIllness').set_enabled(true);
            TlbIllness.get_items().getItemById('tlbItemNew_TlbIllness').set_imageUrl('add.png');
        }
        if (TlbIllness.get_items().getItemById('tlbItemEdit_TlbIllness') != null) {
            TlbIllness.get_items().getItemById('tlbItemEdit_TlbIllness').set_enabled(true);
            TlbIllness.get_items().getItemById('tlbItemEdit_TlbIllness').set_imageUrl('edit.png');
        }
        if (TlbIllness.get_items().getItemById('tlbItemDelete_TlbIllness') != null) {
            TlbIllness.get_items().getItemById('tlbItemDelete_TlbIllness').set_enabled(true);
            TlbIllness.get_items().getItemById('tlbItemDelete_TlbIllness').set_imageUrl('remove.png');
        }
        TlbIllness.get_items().getItemById('tlbItemSave_TlbIllness').set_enabled(false);
        TlbIllness.get_items().getItemById('tlbItemSave_TlbIllness').set_imageUrl('save_silver.png');
        TlbIllness.get_items().getItemById('tlbItemCancel_TlbIllness').set_enabled(false);
        TlbIllness.get_items().getItemById('tlbItemCancel_TlbIllness').set_imageUrl('cancel_silver.png');
        document.getElementById('txtIllnessName_Illness').disabled = 'disabled';
        document.getElementById('txtDescription_Illness').disabled = 'disabled';
    }
}

function SetActionMode_Illness(state) {
    document.getElementById('ActionMode_Illness').innerHTML = document.getElementById("hf" + state + "_Illness").value;
}

function Fill_GridIllness_Illness() {
    document.getElementById('loadingPanel_GridIllness_Illness').innerHTML = document.getElementById('hfloadingPanel_GridIllness_Illness').value;
    CallBack_GridIllness_Illness.callback();
}

function Refresh_GridIllness_Illness() {
    Fill_GridIllness_Illness();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Illness = confirmState;
    if (CurrentPageState_Illness == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Illness').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Illness').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Illness) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateIllness_Illness();
            break;
        case 'Exit':
            ClearList_Illness();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Illness('View');
}

function UpdateIllness_Illness() {
    ObjIllness_Illness = new Object();
    ObjIllness_Illness.Name = null;
    ObjIllness_Illness.Description = null;
    ObjIllness_Illness.ID = '0';
    var SelectedItems_GridIllness_Illness = GridIllness_Illness.getSelectedItems();
    if (SelectedItems_GridIllness_Illness.length > 0)
        ObjIllness_Illness.ID = SelectedItems_GridIllness_Illness[0].getMember("ID").get_text();

    if (CurrentPageState_Illness != 'Delete') {
        ObjIllness_Illness.Name = document.getElementById('txtIllnessName_Illness').value;
        ObjIllness_Illness.Description = document.getElementById('txtDescription_Illness').value;
    }
    UpdateIllness_IllnessPage(CharToKeyCode_Illness(CurrentPageState_Illness), CharToKeyCode_Illness(ObjIllness_Illness.ID), CharToKeyCode_Illness(ObjIllness_Illness.Name), CharToKeyCode_Illness(ObjIllness_Illness.Description));
    DialogWaiting.Show();
}

function UpdateIllness_IllnessPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Illness').value;
            Response[1] = document.getElementById('hfConnectionError_Illness').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_Illness();
            Illness_OnAfterUpdate(Response);
            ChangePageState_Illness('View');
        }
        else {
            if (CurrentPageState_Illness == 'Delete')
                ChangePageState_Illness('View');
        }
    }
}

function Illness_OnAfterUpdate(Response) {
    if (ObjIllness_Illness != null) {
        var IllnessName = ObjIllness_Illness.Name;
        var IllnessDescription = ObjIllness_Illness.Description;

        var IllnessItem = null;
        GridIllness_Illness.beginUpdate();
        switch (CurrentPageState_Illness) {
            case 'Add':
                IllnessItem = GridIllness_Illness.get_table().addEmptyRow(GridIllness_Illness.get_recordCount());
                IllnessItem.setValue(0, Response[3], false);
                GridIllness_Illness.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridIllness_Illness.selectByKey(Response[3], 0, false);
                IllnessItem = GridIllness_Illness.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridIllness_Illness.selectByKey(ObjIllness_Illness.ID, 0, false);
                GridIllness_Illness.deleteSelected();
                break;
        }
        if (CurrentPageState_Illness != 'Delete') {
            IllnessItem.setValue(1, IllnessName, false);
            IllnessItem.setValue(2, IllnessDescription, false);
        }
        GridIllness_Illness.endUpdate();
    }
}

function CallBack_GridIllness_Illness_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Illness').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridIllness_Illness();
    }
}

function GridIllness_Illness_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridIllness_Illness').innerHTML = '';
}

function CallBack_GridIllness_Illness_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridIllness_Illness').innerHTML = '';
    ShowConnectionError_Illness();
}

function ShowConnectionError_Illness() {
    var error = document.getElementById('hfErrorType_Illness').value;
    var errorBody = document.getElementById('hfConnectionError_Illness').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbIllness_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvIllnessIntroduction_iFrame').src = 'Illnesses.aspx';
}

function tlbItemHelp_TlbIllness_onClick() {
    LoadHelpPage('tlbItemHelp_TlbIllness');    
}












