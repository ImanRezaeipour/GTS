var CurrentPageState_UiValidation = 'View';
var ConfirmState_UiValidation = null;
var ObjUiValidation_UiValidation = null;

function CallBack_GridUiValidation_UiValidation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_UiValidation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridUiValidation_UiValidation();
    }
}
function ShowConnectionError_UiValidation() {
    var error = document.getElementById('hfErrorType_UiValidation').value;
    var errorBody = document.getElementById('hfConnectionError_UiValidation').value;
    showDialog(error, errorBody, 'error');
}
function CallBack_GridUiValidation_UiValidation_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridUiValidation_UiValidation').innerHTML = '';
    ShowConnectionError_UiValidation();
}
function GetBoxesHeaders_UiValidation() {

    document.getElementById('header_UiValidation_UiValidation').innerHTML = document.getElementById('hfheader_UiValidation_UiValidation').value;
    document.getElementById('header_tblUiValidation_UiValidationIntroduction').innerHTML = document.getElementById('hfheader_tblUiValidationDetails_UiValidationIntroduction').value;
}
function SetActionMode_UiValidation(state) {
    document.getElementById('ActionMode_UiValidation').innerHTML = document.getElementById("hf" + state + "_UiValidation").value;
}
function Refresh_GridUiValidation_UiValidation() {
    Fill_GridUiValidation_UiValidation();
}
function GridUiValidation_UiValidation_onItemSelect(sender, e) {
    if (CurrentPageState_UiValidation != 'Add')
        NavigateUiValidation_UiValidation(e.get_item());
}
function GridUiValidation_UiValidation_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridUiValidation_UiValidation').innerHTML = '';
}
function ShowDialogUiValidationRules() {
    var ObjPageState_UiValidation = new Object();
    selectedItems_GridUiValidation_UiValidation = GridUiValidation_UiValidation.getSelectedItems();
            if (selectedItems_GridUiValidation_UiValidation.length > 0) {
                ObjPageState_UiValidation.PageState = CurrentPageState_UiValidation;
                ObjPageState_UiValidation.GroupID = selectedItems_GridUiValidation_UiValidation[0].getMember('ID').get_text();
                ObjPageState_UiValidation.Name = selectedItems_GridUiValidation_UiValidation[0].getMember('Name').get_text();
                parent.DialogUiValidationRules.set_value(ObjPageState_UiValidation);
                parent.DialogUiValidationRules.Show();
            }

    
}
function tlbItemNew_TlbUiValidation_onClick() {
    ChangePageState_UiValidation('Add');
    ClearList_UiValidation();
    FocusOnFirstElement_UiValidation();
    
    
}

function tlbItemEdit_TlbUiValidation_onClick() {
    ChangePageState_UiValidation('Edit');
    FocusOnFirstElement_UiValidation();
}

function tlbItemDelete_TlbUiValidation_onClick() {
    ChangePageState_UiValidation('Delete');
}
function tlbItemCancel_TlbUiValidation_onClick() {
    ChangePageState_UiValidation('View');
    ClearList_UiValidation();
}
function tlbItemSetLaw_TlbUiValidation_onClick() {
    var SelectedItems_GridUiValidation_UiValidation = GridUiValidation_UiValidation.getSelectedItems();
    if (SelectedItems_GridUiValidation_UiValidation.length > 0) {
        ShowDialogUiValidationRules();
    }
}
function tlbItemExit_TlbUiValidation_onClick() {
    ShowDialogConfirm('Exit');
}
function ClearList_UiValidation() {
    if (CurrentPageState_UiValidation != 'Edit') {

        document.getElementById('txtUiValidationCode_UiValidationIntroduction').value = '';
        document.getElementById('txtUiValidationName_UiValidationIntroduction').value = '';
        GridUiValidation_UiValidation.unSelectAll();
    }
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_UiValidation = confirmState;
    if (CurrentPageState_UiValidation == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_UiValidation').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_UiValidation').value;
    DialogConfirm.Show();
}
function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_UiValidation) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateUiValidation_UiValidation();
            break;
        case 'Exit':
            ClearList_UiValidation();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function FocusOnFirstElement_UiValidation() {
    document.getElementById('txtUiValidationCode_UiValidationIntroduction').focus();
}
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_UiValidation('View');
}
function tlbItemFormReconstruction_TlbUiValidationIntroduction_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvUiValidation_iFrame').src = 'UiValidation.aspx';
}
function tlbItemCancel_TlbUiValidation_onClick() {
    ChangePageState_UiValidation('View');
    ClearList_UiValidation();
}
function tlbItemSave_TlbUiValidation_onClick() {
    UiValidation_onSave();
}
function ChangePageState_UiValidation(state) {
    CurrentPageState_UiValidation = state;
    SetActionMode_UiValidation(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbUiValidationIntroduction.get_items().getItemById('tlbItemNew_TlbUiValidationIntroduction') != null) {
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemNew_TlbUiValidationIntroduction').set_enabled(false);
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemNew_TlbUiValidationIntroduction').set_imageUrl('add_silver.png');
        }
        if (TlbUiValidationIntroduction.get_items().getItemById('tlbItemEdit_TlbUiValidationIntroduction') != null) {
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemEdit_TlbUiValidationIntroduction').set_enabled(false);
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemEdit_TlbUiValidationIntroduction').set_imageUrl('edit_silver.png');
        }
        if (TlbUiValidationIntroduction.get_items().getItemById('tlbItemDelete_TlbUiValidationIntroduction') != null) {
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemDelete_TlbUiValidationIntroduction').set_enabled(false);
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemDelete_TlbUiValidationIntroduction').set_imageUrl('remove_silver.png');
        }
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemSave_TlbUiValidationIntroduction').set_enabled(true);
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemSave_TlbUiValidationIntroduction').set_imageUrl('save.png');
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemCancel_TlbUiValidationIntroduction').set_enabled(true);
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemCancel_TlbUiValidationIntroduction').set_imageUrl('cancel.png');
        document.getElementById('txtUiValidationCode_UiValidationIntroduction').disabled = '';
        document.getElementById('txtUiValidationName_UiValidationIntroduction').disabled = '';
        if (state == 'Edit')
            NavigateUiValidation_UiValidation(GridUiValidation_UiValidation.getSelectedItems()[0]);
        if (state == 'Delete')
            UiValidation_onSave();
    }
    if (state == 'View') {
        if (TlbUiValidationIntroduction.get_items().getItemById('tlbItemNew_TlbUiValidationIntroduction') != null) {
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemNew_TlbUiValidationIntroduction').set_enabled(true);
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemNew_TlbUiValidationIntroduction').set_imageUrl('add.png');
        }
        if (TlbUiValidationIntroduction.get_items().getItemById('tlbItemEdit_TlbUiValidationIntroduction') != null) {
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemEdit_TlbUiValidationIntroduction').set_enabled(true);
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemEdit_TlbUiValidationIntroduction').set_imageUrl('edit.png');
        }
        if (TlbUiValidationIntroduction.get_items().getItemById('tlbItemDelete_TlbUiValidationIntroduction') != null) {
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemDelete_TlbUiValidationIntroduction').set_enabled(true);
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemDelete_TlbUiValidationIntroduction').set_imageUrl('remove.png');
        }
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemSave_TlbUiValidationIntroduction').set_enabled(false);
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemSave_TlbUiValidationIntroduction').set_imageUrl('save_silver.png');
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemCancel_TlbUiValidationIntroduction').set_enabled(false);
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemCancel_TlbUiValidationIntroduction').set_imageUrl('cancel_silver.png');
        document.getElementById('txtUiValidationCode_UiValidationIntroduction').disabled = 'disabled';
        document.getElementById('txtUiValidationName_UiValidationIntroduction').disabled = 'disabled';
    }
}
function Fill_GridUiValidation_UiValidation() {
    document.getElementById('loadingPanel_GridUiValidation_UiValidation').innerHTML = document.getElementById('hfloadingPanel_GridUiValidation_UiValidation').value;
    CallBack_GridUiValidation_UiValidation.callback();
}
function CallBack_GridUiValidation_UiValidation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_UiValidation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridUiValidation_UiValidation();
    }
}
function UpdateUiValidation_UiValidation() {
    ObjUiValidation_UiValidation = new Object();
    ObjUiValidation_UiValidation.Code = null;
    ObjUiValidation_UiValidation.Name = null;
    ObjUiValidation_UiValidation.Description = null;
    ObjUiValidation_UiValidation.ID = '0';
    var SelectedItems_GridUiValidation_UiValidation = GridUiValidation_UiValidation.getSelectedItems();
    if (SelectedItems_GridUiValidation_UiValidation.length > 0)
        ObjUiValidation_UiValidation.ID = SelectedItems_GridUiValidation_UiValidation[0].getMember("ID").get_text();

    if (CurrentPageState_UiValidation != 'Delete') {
        ObjUiValidation_UiValidation.Code= document.getElementById('txtUiValidationCode_UiValidationIntroduction').value;
        ObjUiValidation_UiValidation.Name = document.getElementById('txtUiValidationName_UiValidationIntroduction').value;
        
    }
    UpdateUiValidation_UiValidationPage(CharToKeyCode_UiValidation(CurrentPageState_UiValidation), CharToKeyCode_UiValidation(ObjUiValidation_UiValidation.ID), CharToKeyCode_UiValidation(ObjUiValidation_UiValidation.Code), CharToKeyCode_UiValidation(ObjUiValidation_UiValidation.Name));
    DialogWaiting.Show();
}

function UpdateUiValidation_UiValidationPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_UiValidation').value;
            Response[1] = document.getElementById('hfConnectionError_UiValidation').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_UiValidation();
            UiValidation_OnAfterUpdate(Response);
            ChangePageState_UiValidation('View');
        }
        else {
            if (CurrentPageState_UiValidation == 'Delete')
                ChangePageState_UiValidation('View');
        }
    }
}

function UiValidation_OnAfterUpdate(Response) {
    if (ObjUiValidation_UiValidation != null) {
        var UiValidationCode = ObjUiValidation_UiValidation.Code;
        var UiValidationName = ObjUiValidation_UiValidation.Name;


        var UiValidationItem = null;
        GridUiValidation_UiValidation.beginUpdate();
        switch (CurrentPageState_UiValidation) {
            case 'Add':
                UiValidationItem = GridUiValidation_UiValidation.get_table().addEmptyRow(GridUiValidation_UiValidation.get_recordCount());
                UiValidationItem.setValue(0, Response[3], false);
                GridUiValidation_UiValidation.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridUiValidation_UiValidation.selectByKey(Response[3], 0, false);
                UiValidationItem = GridUiValidation_UiValidation.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridUiValidation_UiValidation.selectByKey(ObjUiValidation_UiValidation.ID, 0, false);
                GridUiValidation_UiValidation.deleteSelected();
                break;
        }
        if (CurrentPageState_UiValidation != 'Delete') {
            UiValidationItem.setValue(1, UiValidationCode, false);
            UiValidationItem.setValue(2, UiValidationName, false);
            
        }
        GridUiValidation_UiValidation.endUpdate();
    }
}
function NavigateUiValidation_UiValidation(selectedUiValidationItem) {
    if (selectedUiValidationItem != undefined) {
        document.getElementById('txtUiValidationCode_UiValidationIntroduction').value = selectedUiValidationItem.getMember('CustomCode').get_text();
        document.getElementById('txtUiValidationName_UiValidationIntroduction').value = selectedUiValidationItem.getMember('Name').get_text();
       
    }
}
function UiValidation_onSave() {
    if (CurrentPageState_UiValidation != 'Delete')
        UpdateUiValidation_UiValidation();
    else
        ShowDialogConfirm('Delete');
}
function CharToKeyCode_UiValidation(str) {
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

function tlbItemHelp_TlbUiValidationIntroduction_onClick() {
    LoadHelpPage('tlbItemHelp_TlbUiValidationIntroduction');    
}