
var CurrentPageState_Physicians = 'View';
var ConfirmState_Physicians = null;
var ObjPhysician_Physicians = null;

function GetBoxesHeaders_Physicians() {
    document.getElementById('header_Physicians_Physicians').innerHTML = document.getElementById('hfheader_Physicians_Physicians').value;
    document.getElementById('header_tblPhysicians_Physicians').innerHTML = document.getElementById('hfheader_tblPhysicians_Physicians').value;
}

function GridPhysicians_Physicians_onItemSelect(sender, e) {
    if (CurrentPageState_Physicians != 'Add')
        NavigatePhysician_Physicians(e.get_item());
}

function NavigatePhysician_Physicians(selectedPhysicianItem) {
    if (selectedPhysicianItem != undefined) {
        document.getElementById('txtName_Physicians').value = selectedPhysicianItem.getMember('FirstName').get_text();
        document.getElementById('txtFamily_Physicians').value = selectedPhysicianItem.getMember('LastName').get_text();
        document.getElementById('txtProficiency_Physicians').value = selectedPhysicianItem.getMember('Takhasos').get_text();
        document.getElementById('txtMedicalAssociation_Physicians').value = selectedPhysicianItem.getMember('Nezampezaeshki').get_text();
        document.getElementById('txtDescription_Physicians').value = selectedPhysicianItem.getMember('Description').get_text();                        
    }
}

function tlbItemNew_TlbPhysicians_onClick() {
    ChangePageState_Physicians('Add');
    ClearList_Physicians();
    FocusOnFirstElement_Physicians();
}

function tlbItemEdit_TlbPhysicians_onClick() {
    ChangePageState_Physicians('Edit');
    FocusOnFirstElement_Physicians();
}

function tlbItemDelete_TlbPhysicians_onClick() {
    ChangePageState_Physicians('Delete');
}

function tlbItemSave_TlbPhysicians_onClick() {
    Physician_onSave();
}

function tlbItemCancel_TlbPhysicians_onClick() {
    ChangePageState_Physicians('View');
    ClearList_Physicians();
}

function tlbItemExit_TlbPhysicians_onClick() {
    ShowDialogConfirm('Exit');
}

function Physician_onSave() {
    if (CurrentPageState_Physicians != 'Delete')
        UpdatePhysician_Physicians();
    else
        ShowDialogConfirm('Delete');
}

function CharToKeyCode_Physicians(str) {
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


function ClearList_Physicians() {
    if (CurrentPageState_Physicians != 'Edit') {
        document.getElementById('txtName_Physicians').value = '';
        document.getElementById('txtFamily_Physicians').value = '';
        document.getElementById('txtProficiency_Physicians').value = '';
        document.getElementById('txtMedicalAssociation_Physicians').value = '';
        document.getElementById('txtDescription_Physicians').value = '';
        GridPhysicians_Physicians.unSelectAll();
    }
}

function FocusOnFirstElement_Physicians() {
    document.getElementById('txtName_Physicians').focus();
}

function ChangePageState_Physicians(state) {
    CurrentPageState_Physicians = state;
    SetActionMode_Physicians(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbPhysicians.get_items().getItemById('tlbItemNew_TlbPhysicians') != null) {
            TlbPhysicians.get_items().getItemById('tlbItemNew_TlbPhysicians').set_enabled(false);
            TlbPhysicians.get_items().getItemById('tlbItemNew_TlbPhysicians').set_imageUrl('add_silver.png');
        }
        if (TlbPhysicians.get_items().getItemById('tlbItemEdit_TlbPhysicians') != null) {
            TlbPhysicians.get_items().getItemById('tlbItemEdit_TlbPhysicians').set_enabled(false);
            TlbPhysicians.get_items().getItemById('tlbItemEdit_TlbPhysicians').set_imageUrl('edit_silver.png');
        }
        if (TlbPhysicians.get_items().getItemById('tlbItemDelete_TlbPhysicians') != null) {
            TlbPhysicians.get_items().getItemById('tlbItemDelete_TlbPhysicians').set_enabled(false);
            TlbPhysicians.get_items().getItemById('tlbItemDelete_TlbPhysicians').set_imageUrl('remove_silver.png');
        }
        TlbPhysicians.get_items().getItemById('tlbItemSave_TlbPhysicians').set_enabled(true);
        TlbPhysicians.get_items().getItemById('tlbItemSave_TlbPhysicians').set_imageUrl('save.png');
        TlbPhysicians.get_items().getItemById('tlbItemCancel_TlbPhysicians').set_enabled(true);
        TlbPhysicians.get_items().getItemById('tlbItemCancel_TlbPhysicians').set_imageUrl('cancel.png');
        document.getElementById('txtName_Physicians').disabled = '';
        document.getElementById('txtFamily_Physicians').disabled = '';
        document.getElementById('txtProficiency_Physicians').disabled = '';
        document.getElementById('txtMedicalAssociation_Physicians').disabled = '';
        document.getElementById('txtDescription_Physicians').disabled = '';
        if (state == 'Edit')
            NavigatePhysician_Physicians(GridPhysicians_Physicians.getSelectedItems()[0]);
        if (state == 'Delete')
            Physician_onSave();
    }
    if (state == 'View') {
        if (TlbPhysicians.get_items().getItemById('tlbItemNew_TlbPhysicians') != null) {
            TlbPhysicians.get_items().getItemById('tlbItemNew_TlbPhysicians').set_enabled(true);
            TlbPhysicians.get_items().getItemById('tlbItemNew_TlbPhysicians').set_imageUrl('add.png');
        }
        if (TlbPhysicians.get_items().getItemById('tlbItemEdit_TlbPhysicians') != null) {
            TlbPhysicians.get_items().getItemById('tlbItemEdit_TlbPhysicians').set_enabled(true);
            TlbPhysicians.get_items().getItemById('tlbItemEdit_TlbPhysicians').set_imageUrl('edit.png');
        }
        if (TlbPhysicians.get_items().getItemById('tlbItemDelete_TlbPhysicians') != null) {
            TlbPhysicians.get_items().getItemById('tlbItemDelete_TlbPhysicians').set_enabled(true);
            TlbPhysicians.get_items().getItemById('tlbItemDelete_TlbPhysicians').set_imageUrl('remove.png');
        }
        TlbPhysicians.get_items().getItemById('tlbItemSave_TlbPhysicians').set_enabled(false);
        TlbPhysicians.get_items().getItemById('tlbItemSave_TlbPhysicians').set_imageUrl('save_silver.png');
        TlbPhysicians.get_items().getItemById('tlbItemCancel_TlbPhysicians').set_enabled(false);
        TlbPhysicians.get_items().getItemById('tlbItemCancel_TlbPhysicians').set_imageUrl('cancel_silver.png');
        document.getElementById('txtName_Physicians').disabled = 'disabled';
        document.getElementById('txtFamily_Physicians').disabled = 'disabled';
        document.getElementById('txtProficiency_Physicians').disabled = 'disabled';
        document.getElementById('txtMedicalAssociation_Physicians').disabled = 'disabled';
        document.getElementById('txtDescription_Physicians').disabled = 'disabled';
    }
}

function SetActionMode_Physicians(state) {
    document.getElementById('ActionMode_Physicians').innerHTML = document.getElementById("hf" + state + "_Physicians").value;
}

function Fill_GridPhysicians_Physicians() {
    document.getElementById('loadingPanel_GridPhysicians_Physicians').innerHTML = document.getElementById('hfloadingPanel_GridPhysicians_Physicians').value;
    CallBack_GridPhysicians_Physicians.callback();
}

function Refresh_GridPhysicians_Physicians() {
    Fill_GridPhysicians_Physicians();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Physicians = confirmState;
    if (CurrentPageState_Physicians == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Physicians').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Physicians').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Physicians) {
        case 'Delete':
            DialogConfirm.Close();
            UpdatePhysician_Physicians();
            break;
        case 'Exit':
            ClearList_Physicians();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Physicians('View');
}

function UpdatePhysician_Physicians() {
    ObjPhysician_Physicians = new Object();
    ObjPhysician_Physicians.FirstName = null;
    ObjPhysician_Physicians.LastName = null;
    ObjPhysician_Physicians.Proficiency = null;
    ObjPhysician_Physicians.MedicalAssociation = null;
    ObjPhysician_Physicians.Description = null;
    ObjPhysician_Physicians.ID = '0';
    var SelectedItems_GridPhysicians_Physicians = GridPhysicians_Physicians.getSelectedItems();
    if (SelectedItems_GridPhysicians_Physicians.length > 0)
        ObjPhysician_Physicians.ID = SelectedItems_GridPhysicians_Physicians[0].getMember("ID").get_text();

    if (CurrentPageState_Physicians != 'Delete') {
        ObjPhysician_Physicians.FirstName = document.getElementById('txtName_Physicians').value;
        ObjPhysician_Physicians.LastName = document.getElementById('txtFamily_Physicians').value;
        ObjPhysician_Physicians.Proficiency = document.getElementById('txtProficiency_Physicians').value;
        ObjPhysician_Physicians.MedicalAssociation = document.getElementById('txtMedicalAssociation_Physicians').value;
        ObjPhysician_Physicians.Description = document.getElementById('txtDescription_Physicians').value;
    }
    UpdatePhysician_PhysiciansPage(CharToKeyCode_Physicians(CurrentPageState_Physicians), CharToKeyCode_Physicians(ObjPhysician_Physicians.ID), CharToKeyCode_Physicians(ObjPhysician_Physicians.FirstName), CharToKeyCode_Physicians(ObjPhysician_Physicians.LastName), CharToKeyCode_Physicians(ObjPhysician_Physicians.Proficiency), CharToKeyCode_Physicians(ObjPhysician_Physicians.MedicalAssociation), CharToKeyCode_Physicians(ObjPhysician_Physicians.Description));
    DialogWaiting.Show();
}

function UpdatePhysician_PhysiciansPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Physicians').value;
            Response[1] = document.getElementById('hfConnectionError_Physicians').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_Physicians();
            Physician_OnAfterUpdate(Response);
            ChangePageState_Physicians('View');
        }
        else {
            if (CurrentPageState_Physicians == 'Delete')
                ChangePageState_Physicians('View');
        }
    }
}

function Physician_OnAfterUpdate(Response) {
    if (ObjPhysician_Physicians != null) {
        var FirstName = ObjPhysician_Physicians.FirstName;
        var LastName = ObjPhysician_Physicians.LastName;
        var Name = FirstName + ' ' + LastName;
        var Proficiency = ObjPhysician_Physicians.Proficiency;
        var MedicalAssociation = ObjPhysician_Physicians.MedicalAssociation;
        var Description = ObjPhysician_Physicians.Description;

        var PhysicianItem = null;
        GridPhysicians_Physicians.beginUpdate();
        switch (CurrentPageState_Physicians) {
            case 'Add':
                PhysicianItem = GridPhysicians_Physicians.get_table().addEmptyRow(GridPhysicians_Physicians.get_recordCount());
                PhysicianItem.setValue(0, Response[3], false);
                GridPhysicians_Physicians.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridPhysicians_Physicians.selectByKey(Response[3], 0, false);
                PhysicianItem = GridPhysicians_Physicians.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridPhysicians_Physicians.selectByKey(ObjPhysician_Physicians.ID, 0, false);
                GridPhysicians_Physicians.deleteSelected();
                break;
        }
        if (CurrentPageState_Physicians != 'Delete') {
            PhysicianItem.setValue(1, FirstName, false);
            PhysicianItem.setValue(2, LastName, false);
            PhysicianItem.setValue(3, Name, false);
            PhysicianItem.setValue(4, Proficiency, false);
            PhysicianItem.setValue(5, MedicalAssociation, false);
            PhysicianItem.setValue(6, Description, false);
        }
        GridPhysicians_Physicians.endUpdate();
    }
}

function CallBack_GridPhysicians_Physicians_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Physicians').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridPhysicians_Physicians();
    }
}

function GridPhysicians_Physicians_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridPhysicians_Physicians').innerHTML = '';
}

function CallBack_GridPhysicians_Physicians_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridPhysicians_Physicians').innerHTML = '';
    ShowConnectionError_Physicians();
}

function ShowConnectionError_Physicians() {
    var error = document.getElementById('hfErrorType_Physicians').value;
    var errorBody = document.getElementById('hfConnectionError_Physicians').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbPhysicians_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvPhysicianIntroduction_iFrame').src = 'Physicians.aspx';
}

function tlbItemHelp_TlbPhysicians_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPhysicians');
}










