

var CurrentPageState_Corporations = 'View';
var ConfirmState_Corporations = null;
var ObjCorporation_Corporations = null;

function GetBoxesHeaders_Corporations() {
    document.getElementById('header_Corporations_Corporations').innerHTML = document.getElementById('hfheader_Corporations_Corporations').value;
    document.getElementById('header_tblCorporations_Corporations').innerHTML = document.getElementById('hfheader_tblCorporations_Corporations').value;
}

function GridCorporations_Corporations_onItemSelect(sender, e) {
    if (CurrentPageState_Corporations != 'Add')
        NavigateCorporation_Corporations(e.get_item());
}

function NavigateCorporation_Corporations(selectedCorporationItem) {
    if (selectedCorporationItem != undefined) {
        document.getElementById('txtCorporationCode_Corporations').value = selectedCorporationItem.getMember('Code').get_text();
        document.getElementById('txtCorporationName_Corporations').value = selectedCorporationItem.getMember('Name').get_text();
        document.getElementById('txtEconomicCode_Corporations').value = selectedCorporationItem.getMember('EconomicCode').get_text();
        document.getElementById('txtTelNumber_Corporations').value = selectedCorporationItem.getMember('Phone').get_text();
        document.getElementById('txtFax_Corporations').value = selectedCorporationItem.getMember('Fax').get_text();
        document.getElementById('txtAddress_Corporations').value = selectedCorporationItem.getMember('Address').get_text();
        document.getElementById('txtDescription_Corporations').value = selectedCorporationItem.getMember('Description').get_text();
    }
}

function tlbItemNew_TlbCorporations_onClick() {
    ChangePageState_Corporations('Add');
    ClearList_Corporations();
    FocusOnFirstElement_Corporations();
}

function tlbItemEdit_TlbCorporations_onClick() {
    ChangePageState_Corporations('Edit');
    FocusOnFirstElement_Corporations();
}

function tlbItemDelete_TlbCorporations_onClick() {
    ChangePageState_Corporations('Delete');
}

function tlbItemSave_TlbCorporations_onClick() {
    Corporation_onSave();
}

function tlbItemCancel_TlbCorporations_onClick() {
    ChangePageState_Corporations('View');
    ClearList_Corporations();
}

function tlbItemExit_TlbCorporations_onClick() {
    ShowDialogConfirm('Exit');
}

function Corporation_onSave() {
    if (CurrentPageState_Corporations != 'Delete')
        UpdateCorporation_Corporations();
    else
        ShowDialogConfirm('Delete');
}

function CharToKeyCode_Corporations(str) {
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


function ClearList_Corporations() {
    if (CurrentPageState_Corporations != 'Edit') {
        document.getElementById('txtCorporationCode_Corporations').value = '';
        document.getElementById('txtCorporationName_Corporations').value = '';
        document.getElementById('txtEconomicCode_Corporations').value = '';
        document.getElementById('txtTelNumber_Corporations').value = '';
        document.getElementById('txtFax_Corporations').value = '';
        document.getElementById('txtAddress_Corporations').value = '';
        document.getElementById('txtDescription_Corporations').value = '';
        GridCorporations_Corporations.unSelectAll();
    }
}

function FocusOnFirstElement_Corporations() {
    document.getElementById('txtCorporationName_Corporations').focus();
}

function ChangePageState_Corporations(state) {
    CurrentPageState_Corporations = state;
    SetActionMode_Corporations(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbCorporations.get_items().getItemById('tlbItemNew_TlbCorporations') != null) {
            TlbCorporations.get_items().getItemById('tlbItemNew_TlbCorporations').set_enabled(false);
            TlbCorporations.get_items().getItemById('tlbItemNew_TlbCorporations').set_imageUrl('add_silver.png');
        }
        if (TlbCorporations.get_items().getItemById('tlbItemEdit_TlbCorporations') != null) {
            TlbCorporations.get_items().getItemById('tlbItemEdit_TlbCorporations').set_enabled(false);
            TlbCorporations.get_items().getItemById('tlbItemEdit_TlbCorporations').set_imageUrl('edit_silver.png');
        }
        if (TlbCorporations.get_items().getItemById('tlbItemDelete_TlbCorporations') != null) {
            TlbCorporations.get_items().getItemById('tlbItemDelete_TlbCorporations').set_enabled(false);
            TlbCorporations.get_items().getItemById('tlbItemDelete_TlbCorporations').set_imageUrl('remove_silver.png');
        }
        TlbCorporations.get_items().getItemById('tlbItemSave_TlbCorporations').set_enabled(true);
        TlbCorporations.get_items().getItemById('tlbItemSave_TlbCorporations').set_imageUrl('save.png');
        TlbCorporations.get_items().getItemById('tlbItemCancel_TlbCorporations').set_enabled(true);
        TlbCorporations.get_items().getItemById('tlbItemCancel_TlbCorporations').set_imageUrl('cancel.png');
        document.getElementById('txtCorporationCode_Corporations').disabled = '';
        document.getElementById('txtCorporationName_Corporations').disabled = '';
        document.getElementById('txtEconomicCode_Corporations').disabled = '';
        document.getElementById('txtTelNumber_Corporations').disabled = '';
        document.getElementById('txtFax_Corporations').disabled = '';
        document.getElementById('txtAddress_Corporations').disabled = '';
        document.getElementById('txtDescription_Corporations').disabled = '';
        if (state == 'Edit')
            NavigateCorporation_Corporations(GridCorporations_Corporations.getSelectedItems()[0]);
        if (state == 'Delete')
            Corporation_onSave();
    }
    if (state == 'View') {
        if (TlbCorporations.get_items().getItemById('tlbItemNew_TlbCorporations') != null) {
            TlbCorporations.get_items().getItemById('tlbItemNew_TlbCorporations').set_enabled(true);
            TlbCorporations.get_items().getItemById('tlbItemNew_TlbCorporations').set_imageUrl('add.png');
        }
        if (TlbCorporations.get_items().getItemById('tlbItemEdit_TlbCorporations') != null) {
            TlbCorporations.get_items().getItemById('tlbItemEdit_TlbCorporations').set_enabled(true);
            TlbCorporations.get_items().getItemById('tlbItemEdit_TlbCorporations').set_imageUrl('edit.png');
        }
        if (TlbCorporations.get_items().getItemById('tlbItemDelete_TlbCorporations') != null) {
            TlbCorporations.get_items().getItemById('tlbItemDelete_TlbCorporations').set_enabled(true);
            TlbCorporations.get_items().getItemById('tlbItemDelete_TlbCorporations').set_imageUrl('remove.png');
        }
        TlbCorporations.get_items().getItemById('tlbItemSave_TlbCorporations').set_enabled(false);
        TlbCorporations.get_items().getItemById('tlbItemSave_TlbCorporations').set_imageUrl('save_silver.png');
        TlbCorporations.get_items().getItemById('tlbItemCancel_TlbCorporations').set_enabled(false);
        TlbCorporations.get_items().getItemById('tlbItemCancel_TlbCorporations').set_imageUrl('cancel_silver.png');
        document.getElementById('txtCorporationCode_Corporations').disabled = 'disabled';
        document.getElementById('txtCorporationName_Corporations').disabled = 'disabled';
        document.getElementById('txtEconomicCode_Corporations').disabled = 'disabled';
        document.getElementById('txtTelNumber_Corporations').disabled = 'disabled';
        document.getElementById('txtFax_Corporations').disabled = 'disabled';
        document.getElementById('txtAddress_Corporations').disabled = 'disabled';
        document.getElementById('txtDescription_Corporations').disabled = 'disabled';
    }
}

function SetActionMode_Corporations(state) {
    document.getElementById('ActionMode_Corporations').innerHTML = document.getElementById("hf" + state + "_Corporations").value;
}

function Fill_GridCorporations_Corporations() {
    document.getElementById('loadingPanel_GridCorporations_Corporations').innerHTML = document.getElementById('hfloadingPanel_GridCorporations_Corporations').value;
    CallBack_GridCorporations_Corporations.callback();
}

function Refresh_GridCorporations_Corporations() {
    Fill_GridCorporations_Corporations();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Corporations = confirmState;
    if (CurrentPageState_Corporations == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Corporations').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Corporations').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Corporations) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateCorporation_Corporations();
            break;
        case 'Exit':
            ClearList_Corporations();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Corporations('View');
}

function UpdateCorporation_Corporations() {
    ObjCorporation_Corporations = new Object();
    ObjCorporation_Corporations.Code = null;
    ObjCorporation_Corporations.Name = null;
    ObjCorporation_Corporations.EconomicCode = null;
    ObjCorporation_Corporations.Phone = null;
    ObjCorporation_Corporations.Fax = null;
    ObjCorporation_Corporations.Address = null;
    ObjCorporation_Corporations.Description = null;
    ObjCorporation_Corporations.ID = '0';
    var SelectedItems_GridCorporations_Corporations = GridCorporations_Corporations.getSelectedItems();
    if (SelectedItems_GridCorporations_Corporations.length > 0)
        ObjCorporation_Corporations.ID = SelectedItems_GridCorporations_Corporations[0].getMember("ID").get_text();

    if (CurrentPageState_Corporations != 'Delete') {
        ObjCorporation_Corporations.Code = document.getElementById('txtCorporationCode_Corporations').value;
        ObjCorporation_Corporations.Name = document.getElementById('txtCorporationName_Corporations').value;
        ObjCorporation_Corporations.EconomicCode = document.getElementById('txtEconomicCode_Corporations').value;
        ObjCorporation_Corporations.Phone = document.getElementById('txtTelNumber_Corporations').value;
        ObjCorporation_Corporations.Fax = document.getElementById('txtFax_Corporations').value;
        ObjCorporation_Corporations.Address = document.getElementById('txtAddress_Corporations').value;
        ObjCorporation_Corporations.Description = document.getElementById('txtDescription_Corporations').value;
    }
    UpdateCorporation_CorporationsPage(CharToKeyCode_Corporations(CurrentPageState_Corporations), CharToKeyCode_Corporations(ObjCorporation_Corporations.ID), CharToKeyCode_Corporations(ObjCorporation_Corporations.Name), CharToKeyCode_Corporations(ObjCorporation_Corporations.Code), CharToKeyCode_Corporations(ObjCorporation_Corporations.EconomicCode), CharToKeyCode_Corporations(ObjCorporation_Corporations.Phone), CharToKeyCode_Corporations(ObjCorporation_Corporations.Fax), CharToKeyCode_Corporations(ObjCorporation_Corporations.Address), CharToKeyCode_Corporations(ObjCorporation_Corporations.Description));
    DialogWaiting.Show();
}

function UpdateCorporation_CorporationsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Corporations').value;
            Response[1] = document.getElementById('hfConnectionError_Corporations').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_Corporations();
            Corporation_OnAfterUpdate(Response);
            ChangePageState_Corporations('View');
        }
        else {
            if (CurrentPageState_Corporations == 'Delete')
                ChangePageState_Corporations('View');
        }
    }
}

function Corporation_OnAfterUpdate(Response) {
    if (ObjCorporation_Corporations != null) {
        var Name = ObjCorporation_Corporations.Name;
        var Code = ObjCorporation_Corporations.Code;
        var EconomicCode = ObjCorporation_Corporations.EconomicCode;
        var Phone = ObjCorporation_Corporations.Phone;
        var Fax = ObjCorporation_Corporations.Fax;
        var Address = ObjCorporation_Corporations.Address;
        var Description = ObjCorporation_Corporations.Description;

        var CorporationItem = null;
        GridCorporations_Corporations.beginUpdate();
        switch (CurrentPageState_Corporations) {
            case 'Add':
                CorporationItem = GridCorporations_Corporations.get_table().addEmptyRow(GridCorporations_Corporations.get_recordCount());
                CorporationItem.setValue(0, Response[3], false);
                GridCorporations_Corporations.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridCorporations_Corporations.selectByKey(Response[3], 0, false);
                CorporationItem = GridCorporations_Corporations.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridCorporations_Corporations.selectByKey(ObjCorporation_Corporations.ID, 0, false);
                GridCorporations_Corporations.deleteSelected();
                break;
        }
        if (CurrentPageState_Corporations != 'Delete') {
            CorporationItem.setValue(1, Code, false);
            CorporationItem.setValue(2, Name, false);
            CorporationItem.setValue(3, Description, false);
            CorporationItem.setValue(4, EconomicCode, false);
            CorporationItem.setValue(5, Phone, false);
            CorporationItem.setValue(6, Fax, false);
            CorporationItem.setValue(7, Address, false);
        }
        GridCorporations_Corporations.endUpdate();
    }
}

function CallBack_GridCorporations_Corporations_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Corporations').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridCorporations_Corporations();
    }
}

function GridCorporations_Corporations_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridCorporations_Corporations').innerHTML = '';
}

function CallBack_GridCorporations_Corporations_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridCorporations_Corporations').innerHTML = '';
    ShowConnectionError_Corporations();
}

function ShowConnectionError_Corporations() {
    var error = document.getElementById('hfErrorType_Corporations').value;
    var errorBody = document.getElementById('hfConnectionError_Corporations').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbCorporations_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvCorporationsIntroduction_iFrame').src = 'Corporations.aspx';
}

function tlbItemHelp_TlbCorporations_onClick() {
    LoadHelpPage('tlbItemHelp_TlbCorporations');
}










