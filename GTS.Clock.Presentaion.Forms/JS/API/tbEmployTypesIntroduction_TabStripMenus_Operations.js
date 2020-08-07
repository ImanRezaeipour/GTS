
var CurrentPageState_EmployTypes = 'View';
var ConfirmState_EmployTypes = null;
var ObjEmployType_EmployTypes = null;

function GetBoxesHeaders_EmployTypes() {
    document.getElementById('header_tblEmployTypeDetails_EmployTypes').innerHTML = document.getElementById('hfheader_tblEmployTypeDetails_EmployTypes').value;
    document.getElementById('header_EmployTypes_EmployTypes').innerHTML = document.getElementById('hfheader_EmployTypes_EmployTypes').value;
}

function GridEmployTypes_EmployTypes_onItemSelect(sender, e) {
    if (CurrentPageState_EmployTypes != 'Add')
        NavigateEmployType_EmployTypes(e.get_item());
}

function NavigateEmployType_EmployTypes(selectedEmployTypeItem) {
    if (selectedEmployTypeItem != undefined) {
        document.getElementById('txtEmployCode_EmployTypesIntroduction').value = selectedEmployTypeItem.getMember('CustomCode').get_text();
        document.getElementById('txtEmployType_EmployTypesIntroduction').value = selectedEmployTypeItem.getMember('Name').get_text();
    }
}


function tlbItemNew_TlbEmployTypesIntroduction_onClick() {
    ChangePageState_EmployTypes('Add');
    ClearList_EmployTypes();
    FocusOnFirstElement_EmployTypes();
}

function tlbItemEdit_TlbEmployTypesIntroduction_onClick() {
    ChangePageState_EmployTypes('Edit');
    FocusOnFirstElement_EmployTypes();
}

function tlbItemDelete_TlbEmployTypesIntroduction_onClick() {
    ChangePageState_EmployTypes('Delete');
}

function tlbItemSave_TlbEmployTypesIntroduction_onClick() {
    EmployType_onSave();
}

function tlbItemCancel_TlbEmployTypesIntroduction_onClick() {
    ChangePageState_EmployTypes('View');
    ClearList_EmployTypes();
}

function tlbItemExit_TlbEmployTypesIntroduction_onClick() {
    ShowDialogConfirm('Exit');
}

function EmployType_onSave() {
    if (CurrentPageState_EmployTypes != 'Delete')
        UpdateEmployType_EmployTypes();
    else
        ShowDialogConfirm('Delete');
}

function CharToKeyCode_EmployTypes(str) {
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


function ClearList_EmployTypes() {
    if (CurrentPageState_EmployTypes != 'Edit') {
        document.getElementById('txtEmployCode_EmployTypesIntroduction').value = '';
        document.getElementById('txtEmployType_EmployTypesIntroduction').value = '';
        GridEmployTypes_EmployTypes.unSelectAll();
    }
}

function FocusOnFirstElement_EmployTypes() {
    document.getElementById('txtEmployCode_EmployTypesIntroduction').focus();
}

function ChangePageState_EmployTypes(state) {
    CurrentPageState_EmployTypes = state;
    SetActionMode_EmployTypes(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbEmployTypesIntroduction.get_items().getItemById('tlbItemNew_TlbEmployTypesIntroduction') != null) {
            TlbEmployTypesIntroduction.get_items().getItemById('tlbItemNew_TlbEmployTypesIntroduction').set_enabled(false);
            TlbEmployTypesIntroduction.get_items().getItemById('tlbItemNew_TlbEmployTypesIntroduction').set_imageUrl('add_silver.png');
        }
        if (TlbEmployTypesIntroduction.get_items().getItemById('tlbItemEdit_TlbEmployTypesIntroduction') != null) {
            TlbEmployTypesIntroduction.get_items().getItemById('tlbItemEdit_TlbEmployTypesIntroduction').set_enabled(false);
            TlbEmployTypesIntroduction.get_items().getItemById('tlbItemEdit_TlbEmployTypesIntroduction').set_imageUrl('edit_silver.png');
        }
        if (TlbEmployTypesIntroduction.get_items().getItemById('tlbItemDelete_TlbEmployTypesIntroduction') != null) {
            TlbEmployTypesIntroduction.get_items().getItemById('tlbItemDelete_TlbEmployTypesIntroduction').set_enabled(false);
            TlbEmployTypesIntroduction.get_items().getItemById('tlbItemDelete_TlbEmployTypesIntroduction').set_imageUrl('remove_silver.png');
        }
        TlbEmployTypesIntroduction.get_items().getItemById('tlbItemSave_TlbEmployTypesIntroduction').set_enabled(true);
        TlbEmployTypesIntroduction.get_items().getItemById('tlbItemSave_TlbEmployTypesIntroduction').set_imageUrl('save.png');
        TlbEmployTypesIntroduction.get_items().getItemById('tlbItemCancel_TlbEmployTypesIntroduction').set_enabled(true);
        TlbEmployTypesIntroduction.get_items().getItemById('tlbItemCancel_TlbEmployTypesIntroduction').set_imageUrl('cancel.png');
        document.getElementById('txtEmployCode_EmployTypesIntroduction').disabled = '';
        document.getElementById('txtEmployType_EmployTypesIntroduction').disabled = '';
        if (state == 'Edit')
            NavigateEmployType_EmployTypes(GridEmployTypes_EmployTypes.getSelectedItems()[0]);
        if (state == 'Delete')
            EmployType_onSave();
    }
    if (state == 'View') {
        if (TlbEmployTypesIntroduction.get_items().getItemById('tlbItemNew_TlbEmployTypesIntroduction') != null) {
            TlbEmployTypesIntroduction.get_items().getItemById('tlbItemNew_TlbEmployTypesIntroduction').set_enabled(true);
            TlbEmployTypesIntroduction.get_items().getItemById('tlbItemNew_TlbEmployTypesIntroduction').set_imageUrl('add.png');
        }
        if (TlbEmployTypesIntroduction.get_items().getItemById('tlbItemEdit_TlbEmployTypesIntroduction') != null) {
            TlbEmployTypesIntroduction.get_items().getItemById('tlbItemEdit_TlbEmployTypesIntroduction').set_enabled(true);
            TlbEmployTypesIntroduction.get_items().getItemById('tlbItemEdit_TlbEmployTypesIntroduction').set_imageUrl('edit.png');
        }
        if (TlbEmployTypesIntroduction.get_items().getItemById('tlbItemDelete_TlbEmployTypesIntroduction') != null) {
            TlbEmployTypesIntroduction.get_items().getItemById('tlbItemDelete_TlbEmployTypesIntroduction').set_enabled(true);
            TlbEmployTypesIntroduction.get_items().getItemById('tlbItemDelete_TlbEmployTypesIntroduction').set_imageUrl('remove.png');
        }
        TlbEmployTypesIntroduction.get_items().getItemById('tlbItemSave_TlbEmployTypesIntroduction').set_enabled(false);
        TlbEmployTypesIntroduction.get_items().getItemById('tlbItemSave_TlbEmployTypesIntroduction').set_imageUrl('save_silver.png');
        TlbEmployTypesIntroduction.get_items().getItemById('tlbItemCancel_TlbEmployTypesIntroduction').set_enabled(false);
        TlbEmployTypesIntroduction.get_items().getItemById('tlbItemCancel_TlbEmployTypesIntroduction').set_imageUrl('cancel_silver.png');
        document.getElementById('txtEmployCode_EmployTypesIntroduction').disabled = 'disabled';
        document.getElementById('txtEmployType_EmployTypesIntroduction').disabled = 'disabled';
    }
}

function SetActionMode_EmployTypes(state) {
    document.getElementById('ActionMode_EmployTypes').innerHTML = document.getElementById("hf" + state + "_EmployType").value;
}

function Fill_GridEmployTypes_EmployTypes() {
    document.getElementById('loadingPanel_GridEmployTypes_EmployTypes').innerHTML = document.getElementById('hfloadingPanel_GridEmployTypes_EmployTypes').value;
    CallBack_GridEmployTypes_EmployTypes.callback();
}

function Refresh_GridEmployTypes_EmployTypes() {
    Fill_GridEmployTypes_EmployTypes();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_EmployTypes = confirmState;
    if (CurrentPageState_EmployTypes == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_EmployType').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_EmployType').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_EmployTypes) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateEmployType_EmployTypes();
            break;
        case 'Exit':
            ClearList_EmployTypes();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_EmployTypes('View');
}

function UpdateEmployType_EmployTypes() {
    ObjEmployType_EmployTypes = new Object();
    ObjEmployType_EmployTypes.CustomCode = null;
    ObjEmployType_EmployTypes.Name = null;
    ObjEmployType_EmployTypes.ID = '0';
    var SelectedItems_GridEmployTypes_EmployTypes = GridEmployTypes_EmployTypes.getSelectedItems();
    if (SelectedItems_GridEmployTypes_EmployTypes.length > 0)
        ObjEmployType_EmployTypes.ID = SelectedItems_GridEmployTypes_EmployTypes[0].getMember("ID").get_text();

    if (CurrentPageState_EmployTypes != 'Delete') {
        ObjEmployType_EmployTypes.CustomCode = document.getElementById('txtEmployCode_EmployTypesIntroduction').value;
        ObjEmployType_EmployTypes.Name = document.getElementById('txtEmployType_EmployTypesIntroduction').value;
    }
    UpdateEmploy_EmployTypesPage(CharToKeyCode_EmployTypes(CurrentPageState_EmployTypes), CharToKeyCode_EmployTypes(ObjEmployType_EmployTypes.ID), CharToKeyCode_EmployTypes(ObjEmployType_EmployTypes.CustomCode), CharToKeyCode_EmployTypes(ObjEmployType_EmployTypes.Name));
    DialogWaiting.Show();
}

function UpdateEmploy_EmployTypesPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_EmployTypes').value;
            Response[1] = document.getElementById('hfConnectionError_EmployTypes').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_EmployTypes();
            EmployType_OnAfterUpdate(Response);
            ChangePageState_EmployTypes('View');
        }
        else {
            if (CurrentPageState_EmployTypes == 'Delete')
                ChangePageState_EmployTypes('View');
        }
    }
}

function EmployType_OnAfterUpdate(Response) {
    if (ObjEmployType_EmployTypes != null) {
        var EmployTypeCode = ObjEmployType_EmployTypes.CustomCode;
        var EmployTypeName = ObjEmployType_EmployTypes.Name;

        var EmployTypeItem = null;
        GridEmployTypes_EmployTypes.beginUpdate();
        switch (CurrentPageState_EmployTypes) {
            case 'Add':
                EmployTypeItem = GridEmployTypes_EmployTypes.get_table().addEmptyRow(GridEmployTypes_EmployTypes.get_recordCount());
                EmployTypeItem.setValue(0, Response[3], false);
                GridEmployTypes_EmployTypes.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridEmployTypes_EmployTypes.selectByKey(Response[3], 0, false);
                EmployTypeItem = GridEmployTypes_EmployTypes.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridEmployTypes_EmployTypes.selectByKey(ObjEmployType_EmployTypes.ID, 0, false);
                GridEmployTypes_EmployTypes.deleteSelected();
                break;
        }
        if (CurrentPageState_EmployTypes != 'Delete') {
            EmployTypeItem.setValue(1, EmployTypeCode, false);
            EmployTypeItem.setValue(2, EmployTypeName, false);
        }
        GridEmployTypes_EmployTypes.endUpdate();
    }
}

function CallBack_GridEmployTypes_EmployTypes_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_EmployTypes').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridEmployTypes_EmployTypes();
    }
}

function GridEmployTypes_EmployTypes_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridEmployTypes_EmployTypes').innerHTML = '';
}

function CallBack_GridEmployTypes_EmployTypes_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridEmployTypes_EmployTypes').innerHTML = '';
    ShowConnectionError_EmployTypes();
}

function ShowConnectionError_EmployTypes() {
    var error = document.getElementById('hfErrorType_EmployTypes').value;
    var errorBody = document.getElementById('hfConnectionError_EmployTypes').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbEmployTypesIntroduction_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvEmployTypesIntroduction_iFrame').src = 'EmployTypes.aspx';
}

function tlbItemHelp_TlbEmployTypesIntroduction_onClick() {
    LoadHelpPage('tlbItemHelp_TlbEmployTypesIntroduction');    
}











