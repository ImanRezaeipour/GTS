
var CurrentPageState_WorkHeat = 'View';
var ConfirmState_WorkHeat = null;
var ObjWorkHeat_WorkHeat = null;

function GetBoxesHeaders_WorkHeat() {
    document.getElementById('header_tblWorkHeatDetails_WorkHeatIntroduction').innerHTML = document.getElementById('hfheader_tblWorkHeatDetails_WorkHeatIntroduction').value;
    document.getElementById('header_WorkHeat_WorkHeat').innerHTML = document.getElementById('hfheader_WorkHeat_WorkHeat').value;
}

function GridWorkHeat_WorkHeat_onItemSelect(sender, e) {
    if (CurrentPageState_WorkHeat != 'Add')
        NavigateWorkHeat_WorkHeat(e.get_item());
}

function NavigateWorkHeat_WorkHeat(selectedWorkHeatItem) {
    if (selectedWorkHeatItem != undefined) {
        document.getElementById('txtWorkHeatCode_WorkHeatIntroduction').value = selectedWorkHeatItem.getMember('CustomCode').get_text() ;
        document.getElementById('txtWorkHeatName_WorkHeatIntroduction').value = selectedWorkHeatItem.getMember('Name').get_text();
        document.getElementById('txtWorkHeatDescription_WorkHeatIntroduction').value = selectedWorkHeatItem.getMember('Description').get_text();
    }
}

function tlbItemNew_TlbWorkHeatIntroduction_onClick() {
    ChangePageState_WorkHeat('Add');
    ClearList_WorkHeat();
    FocusOnFirstElement_WorkHeat();
}

function tlbItemEdit_TlbWorkHeatIntroduction_onClick() {
    ChangePageState_WorkHeat('Edit');
    FocusOnFirstElement_WorkHeat();
}

function tlbItemDelete_TlbWorkHeatIntroduction_onClick() {
    ChangePageState_WorkHeat('Delete');
}

function tlbItemSave_TlbWorkHeatIntroduction_onClick() {
    WorkHeat_onSave();
}

function tlbItemCancel_TlbWorkHeatIntroduction_onClick() {
    ChangePageState_WorkHeat('View');
    ClearList_WorkHeat();
}

function tlbItemExit_TlbWorkHeatIntroduction_onClick() {
    ShowDialogConfirm('Exit');
}

function WorkHeat_onSave() {
    if (CurrentPageState_WorkHeat != 'Delete')
        UpdateWorkHeat_WorkHeat();
    else
        ShowDialogConfirm('Delete');    
}

function CharToKeyCode_WorkHeat(str) {
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


function ClearList_WorkHeat() {
    if (CurrentPageState_WorkHeat != 'Edit') {
        document.getElementById('txtWorkHeatCode_WorkHeatIntroduction').value = '';
        document.getElementById('txtWorkHeatName_WorkHeatIntroduction').value = '';
        document.getElementById('txtWorkHeatDescription_WorkHeatIntroduction').value = '';
        GridWorkHeat_WorkHeat.unSelectAll();
    }
}

function FocusOnFirstElement_WorkHeat() {
    document.getElementById('txtWorkHeatCode_WorkHeatIntroduction').focus();
}

function ChangePageState_WorkHeat(state) {
    CurrentPageState_WorkHeat = state;
    SetActionMode_WorkHeat(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbWorkHeatIntroduction.get_items().getItemById('tlbItemNew_TlbWorkHeatIntroduction') != null) {
            TlbWorkHeatIntroduction.get_items().getItemById('tlbItemNew_TlbWorkHeatIntroduction').set_enabled(false);
            TlbWorkHeatIntroduction.get_items().getItemById('tlbItemNew_TlbWorkHeatIntroduction').set_imageUrl('add_silver.png');
        }
        if (TlbWorkHeatIntroduction.get_items().getItemById('tlbItemEdit_TlbWorkHeatIntroduction') != null) {
            TlbWorkHeatIntroduction.get_items().getItemById('tlbItemEdit_TlbWorkHeatIntroduction').set_enabled(false);
            TlbWorkHeatIntroduction.get_items().getItemById('tlbItemEdit_TlbWorkHeatIntroduction').set_imageUrl('edit_silver.png');
        }
        if (TlbWorkHeatIntroduction.get_items().getItemById('tlbItemDelete_TlbWorkHeatIntroduction') != null) {
            TlbWorkHeatIntroduction.get_items().getItemById('tlbItemDelete_TlbWorkHeatIntroduction').set_enabled(false);
            TlbWorkHeatIntroduction.get_items().getItemById('tlbItemDelete_TlbWorkHeatIntroduction').set_imageUrl('remove_silver.png');
        }
        TlbWorkHeatIntroduction.get_items().getItemById('tlbItemSave_TlbWorkHeatIntroduction').set_enabled(true);
        TlbWorkHeatIntroduction.get_items().getItemById('tlbItemSave_TlbWorkHeatIntroduction').set_imageUrl('save.png');
        TlbWorkHeatIntroduction.get_items().getItemById('tlbItemCancel_TlbWorkHeatIntroduction').set_enabled(true);
        TlbWorkHeatIntroduction.get_items().getItemById('tlbItemCancel_TlbWorkHeatIntroduction').set_imageUrl('cancel.png');
        document.getElementById('txtWorkHeatCode_WorkHeatIntroduction').disabled = '';
        document.getElementById('txtWorkHeatName_WorkHeatIntroduction').disabled = '';
        document.getElementById('txtWorkHeatDescription_WorkHeatIntroduction').disabled = '';
        if (state == 'Edit')
            NavigateWorkHeat_WorkHeat(GridWorkHeat_WorkHeat.getSelectedItems()[0]);
        if (state == 'Delete')
            WorkHeat_onSave();
    }
    if (state == 'View') {
        if (TlbWorkHeatIntroduction.get_items().getItemById('tlbItemNew_TlbWorkHeatIntroduction') != null) {
            TlbWorkHeatIntroduction.get_items().getItemById('tlbItemNew_TlbWorkHeatIntroduction').set_enabled(true);
            TlbWorkHeatIntroduction.get_items().getItemById('tlbItemNew_TlbWorkHeatIntroduction').set_imageUrl('add.png');
        }
        if (TlbWorkHeatIntroduction.get_items().getItemById('tlbItemEdit_TlbWorkHeatIntroduction') != null) {
            TlbWorkHeatIntroduction.get_items().getItemById('tlbItemEdit_TlbWorkHeatIntroduction').set_enabled(true);
            TlbWorkHeatIntroduction.get_items().getItemById('tlbItemEdit_TlbWorkHeatIntroduction').set_imageUrl('edit.png');
        }
        if (TlbWorkHeatIntroduction.get_items().getItemById('tlbItemDelete_TlbWorkHeatIntroduction') != null) {
            TlbWorkHeatIntroduction.get_items().getItemById('tlbItemDelete_TlbWorkHeatIntroduction').set_enabled(true);
            TlbWorkHeatIntroduction.get_items().getItemById('tlbItemDelete_TlbWorkHeatIntroduction').set_imageUrl('remove.png');
        }
        TlbWorkHeatIntroduction.get_items().getItemById('tlbItemSave_TlbWorkHeatIntroduction').set_enabled(false);
        TlbWorkHeatIntroduction.get_items().getItemById('tlbItemSave_TlbWorkHeatIntroduction').set_imageUrl('save_silver.png');
        TlbWorkHeatIntroduction.get_items().getItemById('tlbItemCancel_TlbWorkHeatIntroduction').set_enabled(false);
        TlbWorkHeatIntroduction.get_items().getItemById('tlbItemCancel_TlbWorkHeatIntroduction').set_imageUrl('cancel_silver.png');
        document.getElementById('txtWorkHeatCode_WorkHeatIntroduction').disabled = 'disabled';
        document.getElementById('txtWorkHeatName_WorkHeatIntroduction').disabled = 'disabled';
        document.getElementById('txtWorkHeatDescription_WorkHeatIntroduction').disabled = 'disabled';
    }
}

function SetActionMode_WorkHeat(state) {
    document.getElementById('ActionMode_WorkHeat').innerHTML = document.getElementById("hf" + state + "_WorkHeat").value;
}

function Fill_GridWorkHeat_WorkHeat() {
    document.getElementById('loadingPanel_GridWorkHeat_WorkHeat').innerHTML = document.getElementById('hfloadingPanel_GridWorkHeat_WorkHeat').value;
    CallBack_GridWorkHeat_WorkHeat.callback();
}

function Refresh_GridWorkHeat_WorkHeat() {
    Fill_GridWorkHeat_WorkHeat();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_WorkHeat = confirmState;
    if (CurrentPageState_WorkHeat == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_WorkHeat').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_WorkHeat').value;
    DialogConfirm.Show();    
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_WorkHeat) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateWorkHeat_WorkHeat();
            break;
        case 'Exit':
            ClearList_WorkHeat();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_WorkHeat('View');
}

function UpdateWorkHeat_WorkHeat() {
    ObjWorkHeat_WorkHeat = new Object();
    ObjWorkHeat_WorkHeat.CustomCode = null;
    ObjWorkHeat_WorkHeat.Name = null;
    ObjWorkHeat_WorkHeat.Description = null;
    ObjWorkHeat_WorkHeat.ID = '0';
    var SelectedItems_GridWorkHeat_WorkHeat = GridWorkHeat_WorkHeat.getSelectedItems();
    if (SelectedItems_GridWorkHeat_WorkHeat.length > 0) 
        ObjWorkHeat_WorkHeat.ID = SelectedItems_GridWorkHeat_WorkHeat[0].getMember("ID").get_text();
    
    if (CurrentPageState_WorkHeat != 'Delete') {
        ObjWorkHeat_WorkHeat.CustomCode = document.getElementById('txtWorkHeatCode_WorkHeatIntroduction').value;
        ObjWorkHeat_WorkHeat.Name = document.getElementById('txtWorkHeatName_WorkHeatIntroduction').value;
        ObjWorkHeat_WorkHeat.Description = document.getElementById('txtWorkHeatDescription_WorkHeatIntroduction').value;
    }
    UpdateWorkHeat_WorkHeatPage(CharToKeyCode_WorkHeat(CurrentPageState_WorkHeat), CharToKeyCode_WorkHeat(ObjWorkHeat_WorkHeat.ID), CharToKeyCode_WorkHeat(ObjWorkHeat_WorkHeat.CustomCode), CharToKeyCode_WorkHeat(ObjWorkHeat_WorkHeat.Name), CharToKeyCode_WorkHeat(ObjWorkHeat_WorkHeat.Description));
    DialogWaiting.Show();
}

function UpdateWorkHeat_WorkHeatPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_WorkHeat').value;
            Response[1] = document.getElementById('hfConnectionError_WorkHeat').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_WorkHeat();            
            WorkHeat_OnAfterUpdate(Response);
            ChangePageState_WorkHeat('View');
        }
        else { 
           if(CurrentPageState_WorkHeat == 'Delete')
              ChangePageState_WorkHeat('View');              
        }
    }
}

function WorkHeat_OnAfterUpdate(Response) {
    if (ObjWorkHeat_WorkHeat != null) {
        var WorkHeatCode = ObjWorkHeat_WorkHeat.CustomCode;
        var WorkHeatName = ObjWorkHeat_WorkHeat.Name;
        var WorkHeatDescription = ObjWorkHeat_WorkHeat.Description;

        var WorkHeatItem = null;
        GridWorkHeat_WorkHeat.beginUpdate();
        switch (CurrentPageState_WorkHeat) {
            case 'Add':
                WorkHeatItem = GridWorkHeat_WorkHeat.get_table().addEmptyRow(GridWorkHeat_WorkHeat.get_recordCount());
                WorkHeatItem.setValue(0, Response[3], false);
                GridWorkHeat_WorkHeat.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridWorkHeat_WorkHeat.selectByKey(Response[3], 0, false);
                WorkHeatItem = GridWorkHeat_WorkHeat.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridWorkHeat_WorkHeat.selectByKey(ObjWorkHeat_WorkHeat.ID, 0, false);
                GridWorkHeat_WorkHeat.deleteSelected();
                break;
        }
        if (CurrentPageState_WorkHeat != 'Delete') {
            WorkHeatItem.setValue(1, WorkHeatCode, false);
            WorkHeatItem.setValue(2, WorkHeatName, false);
            WorkHeatItem.setValue(3, WorkHeatDescription, false);
        }
        GridWorkHeat_WorkHeat.endUpdate();
    }
}

function CallBack_GridWorkHeat_WorkHeat_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkHeat').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridWorkHeat_WorkHeat();
    }
}

function GridWorkHeat_WorkHeat_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridWorkHeat_WorkHeat').innerHTML = '';
}

function CallBack_GridWorkHeat_WorkHeat_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridWorkHeat_WorkHeat').innerHTML = '';
    ShowConnectionError_WorkHeat();
}

function ShowConnectionError_WorkHeat() {
    var error = document.getElementById('hfErrorType_WorkHeat').value;
    var errorBody = document.getElementById('hfConnectionError_WorkHeat').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbWorkHeatIntroduction_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvWorkHeatIntroduction_iFrame').src = 'WorkHeat.aspx';
}

function tlbItemHelp_TlbWorkHeatIntroduction_onClick() {
    LoadHelpPage('tlbItemHelp_TlbWorkHeatIntroduction');
}










