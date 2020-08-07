
var CurrentPageState_Machines = 'View';
var ConfirmState_Machines = null;
var ObjMachine_Machines = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var SelectedMachineType_Machines = null;
var SelectedControlStation_Machines = null;

function GetBoxesHeaders_Machines() {
    document.getElementById('header_MachineDetails_Machines').innerHTML = document.getElementById('hfheader_MachineDetails_Machines').value;
    document.getElementById('header_Machines_Machines').innerHTML = document.getElementById('hfheader_Machines_Machines').value;
}

function tlbItemNew_TlbMachine_onClick() {
    ChangePageState_Machines('Add');
    ClearList_Machines();
    FocusOnFirstElement_Machines();
}

function tlbItemEdit_TlbMachine_onClick() {
    ChangePageState_Machines('Edit');
    FocusOnFirstElement_Machines();
}

function tlbItemDelete_TlbMachine_onClick() {
    ChangePageState_Machines('Delete');
}

function tlbItemSave_TlbMachine_onClick() {
    CollapseControls_Machines();
    Machine_onSave();
}

function tlbItemCancel_TlbMachine_onClick() {
    ChangePageState_Machines('View');
    ClearList_Machines();
}

function tlbItemExit_TlbMachine_onClick() {
    ShowDialogConfirm('Exit');
}

function Refresh_GridMachines_Machines() {
    Fill_GridMachines_Machines();

}

function GridMachines_Machines_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridMachines_Machines').innerHTML = '';
}

function GridMachines_Machines_onItemSelect(sender, e) {
    if (CurrentPageState_Machines != 'Add')
        NavigateMachine_Machines(e.get_item());
}

function CallBack_GridMachines_Machines_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Machines').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridMachines_Machines();
    }
}

function CallBack_GridMachines_Machines_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridMachines_Machines').innerHTML = '';
    ShowConnectionError_Machines();
}

function cmbControlStation_Machines_onExpand(sender, e) {
    if (cmbControlStation_Machines.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbControlStation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbControlStation = true;
        CallBcak_cmbControlStations_Machines.callback();
    }
}

function cmbControlStation_Machines_onCollapse(sender, e) {
    if (cmbControlStation_Machines.getSelectedItem() == undefined) {
        if (SelectedControlStation_Machines.ID == null || SelectedControlStation_Machines.ID == undefined)
            document.getElementById('cmbControlStation_Machines_Input').value = document.getElementById('hfcmbAlarm_Machines').value;
        else {
            if (SelectedControlStation_Machines.ID != null && SelectedControlStation_Machines.ID != undefined)
                document.getElementById('cmbControlStation_Machines_Input').value = SelectedControlStation_Machines.Name;
        }
    }
}

function CallBcak_cmbControlStations_Machines_onBeforeCallback(sender, e) {
    cmbControlStation_Machines.dispose();
}

function CallBcak_cmbControlStations_Machines_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ControlStations').value;
    if (error == "") {
        document.getElementById('cmbControlStation_Machines_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbControlStation_Machines_DropImage').mousedown();
        cmbControlStation_Machines.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbControlStation_DropDown').style.display = 'none';
    }
}


function cmbMachineTypes_Machines_onExpand(sender, e) {
    if (cmbMachineTypes_Machines.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMachineTypes_Machines == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMachineTypes_Machines = true;
        CallBack_cmbMachineTypes_Machines.callback();
    }
}

function cmbMachineTypes_Machines_onCollapse(sender, e) {
    if (cmbMachineTypes_Machines.getSelectedItem() == undefined) {
        if (SelectedMachineType_Machines.ID == null || SelectedMachineType_Machines.ID == undefined)
            document.getElementById('cmbMachineTypes_Machines_Input').value = document.getElementById('hfcmbAlarm_Machines').value;
        else {
            if (SelectedMachineType_Machines.ID != null && SelectedMachineType_Machines.ID != undefined)
                document.getElementById('cmbMachineTypes_Machines_Input').value = SelectedMachineType_Machines.Name;
        }
    }
}

function CallBack_cmbMachineTypes_Machines_onBeforeCallback(sender, e) {
    cmbMachineTypes_Machines.dispose();
}

function CallBack_cmbMachineTypes_Machines_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MachineTypes').value;
    if (error == "") {
        document.getElementById('cmbMachineTypes_Machines_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMachineTypes_Machines_DropImage').mousedown();
        cmbMachineTypes_Machines.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbMachineTypes_Machines_DropDown').style.display = 'none';
    }
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Machines) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateMachine_Machines();
            break;
        case 'Exit':
            ClearList_Machines();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Machines('View');
}

function NavigateMachine_Machines(selectedMachineItem) {
    if (selectedMachineItem != undefined) {
        document.getElementById('txtMachineCode_Machines').value = selectedMachineItem.getMember('CustomCode').get_text();
        document.getElementById('txtMachineName_Machines').value = selectedMachineItem.getMember('Name').get_text();
        SelectedMachineType_Machines = new Object();
        SelectedMachineType_Machines.ID = selectedMachineItem.getMember('ClockType.ID').get_text();
        document.getElementById('cmbMachineTypes_Machines_Input').value = SelectedMachineType_Machines.Name = selectedMachineItem.getMember('ClockType.Name').get_text();
        SelectedControlStation_Machines = new Object();
        SelectedControlStation_Machines.ID = selectedMachineItem.getMember('Station.ID').get_text();
        document.getElementById('cmbControlStation_Machines_Input').value = SelectedControlStation_Machines.Name = selectedMachineItem.getMember('Station.Name').get_text();
        document.getElementById('txtMachineConnectionTelNumber_Machines').value = selectedMachineItem.getMember('Tel').get_text();
        selectedMachineItem.getMember('Active').get_value() == true ? document.getElementById('rdbActive_Machines').checked = true : document.getElementById('rdbInActive_Machines').checked = true;
    }
}

function Machine_onSave() {
    if (CurrentPageState_Machines != 'Delete')
        UpdateMachine_Machines();
    else
        ShowDialogConfirm('Delete');
}

function CharToKeyCode_Machines(str) {
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

function ClearList_Machines() {
    if (CurrentPageState_Machines != 'Edit') {
        document.getElementById('txtMachineCode_Machines').value = '';
        document.getElementById('txtMachineName_Machines').value = '';
        document.getElementById('cmbMachineTypes_Machines_Input').value = document.getElementById('hfcmbAlarm_Machines').value;
        cmbMachineTypes_Machines.unSelect();
        document.getElementById('cmbControlStation_Machines_Input').value = document.getElementById('hfcmbAlarm_Machines').value;
        cmbControlStation_Machines.unSelect();
        document.getElementById('txtMachineConnectionTelNumber_Machines').value = '';
        document.getElementById('rdbActive_Machines').checked = true;
        document.getElementById('rdbInActive_Machines').checked = false;
        GridMachines_Machines.unSelectAll();
        SelectedMachineType_Machines = null;
        SelectedControlStation_Machines = null;
    }
}

function FocusOnFirstElement_Machines() {
    document.getElementById('txtMachineCode_Machines').focus();
}

function ChangePageState_Machines(state) {
    CurrentPageState_Machines = state;
    SetActionMode_Machines(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbMachine.get_items().getItemById('tlbItemNew_TlbMachine') != null) {
            TlbMachine.get_items().getItemById('tlbItemNew_TlbMachine').set_enabled(false);
            TlbMachine.get_items().getItemById('tlbItemNew_TlbMachine').set_imageUrl('add_silver.png');
        }
        if (TlbMachine.get_items().getItemById('tlbItemEdit_TlbMachine') != null) {
            TlbMachine.get_items().getItemById('tlbItemEdit_TlbMachine').set_enabled(false);
            TlbMachine.get_items().getItemById('tlbItemEdit_TlbMachine').set_imageUrl('edit_silver.png');
        }
        if (TlbMachine.get_items().getItemById('tlbItemDelete_TlbMachine') != null) {
            TlbMachine.get_items().getItemById('tlbItemDelete_TlbMachine').set_enabled(false);
            TlbMachine.get_items().getItemById('tlbItemDelete_TlbMachine').set_imageUrl('remove_silver.png');
        }
        TlbMachine.get_items().getItemById('tlbItemSave_TlbMachine').set_enabled(true);
        TlbMachine.get_items().getItemById('tlbItemSave_TlbMachine').set_imageUrl('save.png');
        TlbMachine.get_items().getItemById('tlbItemCancel_TlbMachine').set_enabled(true);
        TlbMachine.get_items().getItemById('tlbItemCancel_TlbMachine').set_imageUrl('cancel.png');
        document.getElementById('txtMachineCode_Machines').disabled = '';
        document.getElementById('txtMachineName_Machines').disabled = '';
        document.getElementById('txtMachineConnectionTelNumber_Machines').disabled = '';
        document.getElementById('rdbActive_Machines').disabled = '';
        document.getElementById('rdbInActive_Machines').disabled = '';
        cmbMachineTypes_Machines.enable();
        cmbControlStation_Machines.enable();
        if (state == 'Edit')
            NavigateMachine_Machines(GridMachines_Machines.getSelectedItems()[0]);
        if (state == 'Delete')
            Machine_onSave();
    }
    if (state == 'View') {
        if (TlbMachine.get_items().getItemById('tlbItemNew_TlbMachine') != null) {
            TlbMachine.get_items().getItemById('tlbItemNew_TlbMachine').set_enabled(true);
            TlbMachine.get_items().getItemById('tlbItemNew_TlbMachine').set_imageUrl('add.png');
        }
        if (TlbMachine.get_items().getItemById('tlbItemEdit_TlbMachine') != null) {
            TlbMachine.get_items().getItemById('tlbItemEdit_TlbMachine').set_enabled(true);
            TlbMachine.get_items().getItemById('tlbItemEdit_TlbMachine').set_imageUrl('edit.png');
        }
        if (TlbMachine.get_items().getItemById('tlbItemDelete_TlbMachine') != null) {
            TlbMachine.get_items().getItemById('tlbItemDelete_TlbMachine').set_enabled(true);
            TlbMachine.get_items().getItemById('tlbItemDelete_TlbMachine').set_imageUrl('remove.png');
        }
        TlbMachine.get_items().getItemById('tlbItemSave_TlbMachine').set_enabled(false);
        TlbMachine.get_items().getItemById('tlbItemSave_TlbMachine').set_imageUrl('save_silver.png');
        TlbMachine.get_items().getItemById('tlbItemCancel_TlbMachine').set_enabled(false);
        TlbMachine.get_items().getItemById('tlbItemCancel_TlbMachine').set_imageUrl('cancel_silver.png');
        document.getElementById('txtMachineCode_Machines').disabled = 'disabled';
        document.getElementById('txtMachineName_Machines').disabled = 'disabled';
        document.getElementById('txtMachineConnectionTelNumber_Machines').disabled = 'disabled';
        document.getElementById('rdbActive_Machines').disabled = 'disabled';
        document.getElementById('rdbInActive_Machines').disabled = 'disabled';
        cmbMachineTypes_Machines.disable();
        cmbControlStation_Machines.disable();
    }
}

function SetActionMode_Machines(state) {
    document.getElementById('ActionMode_Machines').innerHTML = document.getElementById("hf" + state + "_Machines").value;
}

function Fill_GridMachines_Machines() {
    document.getElementById('loadingPanel_GridMachines_Machines').innerHTML = document.getElementById('hfloadingPanel_GridMachines_Machines').value;
    CallBack_GridMachines_Machines.callback();
}

function Refresh_GridMachines_Machines() {
    Fill_GridMachines_Machines();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Machines = confirmState;
    if (CurrentPageState_Machines == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Machines').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Machines').value;
    DialogConfirm.Show();
    CollapseControls_Machines();
}


function UpdateMachine_Machines() {
    ObjMachine_Machines = new Object();
    ObjMachine_Machines.CustomCode = null;
    ObjMachine_Machines.Name = null;
    ObjMachine_Machines.MachineTypeID = '0';
    ObjMachine_Machines.MachineTypeName = null;
    ObjMachine_Machines.ControlStationID = '0';
    ObjMachine_Machines.ControlStationName = '0';
    ObjMachine_Machines.MachineConnectionTel = null;
    ObjMachine_Machines.IsActive = false;
    ObjMachine_Machines.ID = '0';
    var SelectedItems_GridMachines_Machines = GridMachines_Machines.getSelectedItems();
    if (SelectedItems_GridMachines_Machines.length > 0)
        ObjMachine_Machines.ID = SelectedItems_GridMachines_Machines[0].getMember("ID").get_text();

    if (CurrentPageState_Machines != 'Delete') {
        ObjMachine_Machines.CustomCode = document.getElementById('txtMachineCode_Machines').value;
        ObjMachine_Machines.Name = document.getElementById('txtMachineName_Machines').value;
        if (cmbMachineTypes_Machines.getSelectedItem() != undefined) {
            ObjMachine_Machines.MachineTypeID = cmbMachineTypes_Machines.getSelectedItem().get_value();
            ObjMachine_Machines.MachineTypeName = cmbMachineTypes_Machines.getSelectedItem().get_text();
        }
        else {
            if (SelectedMachineType_Machines != null) {
                ObjMachine_Machines.MachineTypeID = SelectedMachineType_Machines.ID;
                ObjMachine_Machines.MachineTypeName = SelectedMachineType_Machines.Name;
            }
        }
        if (cmbControlStation_Machines.getSelectedItem() != undefined) {
            ObjMachine_Machines.ControlStationID = cmbControlStation_Machines.getSelectedItem().get_value();
            ObjMachine_Machines.ControlStationName = cmbControlStation_Machines.getSelectedItem().get_text();
        }
        else {
            if (SelectedControlStation_Machines != null) {
                ObjMachine_Machines.ControlStationID = SelectedControlStation_Machines.ID;
                ObjMachine_Machines.ControlStationName = SelectedControlStation_Machines.Name;
            }
        }
        ObjMachine_Machines.MachineConnectionTel = document.getElementById('txtMachineConnectionTelNumber_Machines').value;
        ObjMachine_Machines.IsActive = document.getElementById('rdbActive_Machines').checked;
    }
    UpdateMachine_MachinesPage(CharToKeyCode_Machines(CurrentPageState_Machines), CharToKeyCode_Machines(ObjMachine_Machines.ID), CharToKeyCode_Machines(ObjMachine_Machines.CustomCode), CharToKeyCode_Machines(ObjMachine_Machines.Name), CharToKeyCode_Machines(ObjMachine_Machines.MachineTypeID), CharToKeyCode_Machines(ObjMachine_Machines.ControlStationID), CharToKeyCode_Machines(ObjMachine_Machines.MachineConnectionTel), CharToKeyCode_Machines(ObjMachine_Machines.IsActive.toString()));
    DialogWaiting.Show();
}

function UpdateMachine_MachinesPage_onCallBack(Response) {
    var RetMesage = Response;
    if (RetMesage != null && RetMesage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Machines').value;
            Response[1] = document.getElementById('hfConnectionError_Machines').value;
        }
        showDialog(RetMesage[0], Response[1], RetMesage[2]);
        if (RetMesage[2] == 'success') {
            ClearList_Machines();
            ControlStation_OnAfterUpdate(Response);
            ChangePageState_Machines('View');
        }
        else {
            if (CurrentPageState_Machines == 'Delete')
                ChangePageState_Machines('View');
        }
    }
}

function ControlStation_OnAfterUpdate(Response) {
    if (ObjMachine_Machines != null) {
        var MachineCode = ObjMachine_Machines.CustomCode;
        var MachineName = ObjMachine_Machines.Name;
        var MachineTypeID = ObjMachine_Machines.MachineTypeID;
        var MachineTypeName = ObjMachine_Machines.MachineTypeName;
        var ControlStationID = ObjMachine_Machines.ControlStationID;
        var ControlStationName = ObjMachine_Machines.ControlStationName;
        var MachineConnectionTel = ObjMachine_Machines.MachineConnectionTel;
        var IsActive = ObjMachine_Machines.IsActive;

        var MachineItem = null;
        GridMachines_Machines.beginUpdate();
        switch (CurrentPageState_Machines) {
            case 'Add':
                MachineItem = GridMachines_Machines.get_table().addEmptyRow(GridMachines_Machines.get_recordCount());
                MachineItem.setValue(0, Response[3], false);
                GridMachines_Machines.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridMachines_Machines.selectByKey(Response[3], 0, false);
                MachineItem = GridMachines_Machines.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridMachines_Machines.selectByKey(ObjMachine_Machines.ID, 0, false);
                GridMachines_Machines.deleteSelected();
                break;
        }
        if (CurrentPageState_Machines != 'Delete') {
            MachineItem.setValue(1, MachineCode, false);
            MachineItem.setValue(2, MachineName, false);
            MachineItem.setValue(3, MachineTypeName, false);
            MachineItem.setValue(4, ControlStationName, false);
            MachineItem.setValue(5, IsActive, false);
            MachineItem.setValue(6, MachineTypeID, false);
            MachineItem.setValue(7, ControlStationID, false);
            MachineItem.setValue(8, MachineConnectionTel, false);
        }
        GridMachines_Machines.endUpdate();
    }
}

function CallBack_cmbMachineTypes_Machines_onCallbackError(sender, e) {
    ShowConnectionError_ControlStation();
}

function CallBcak_cmbControlStations_Machines_onCallbackError(sender, e) {
    ShowConnectionError_Machines();
}

function ShowConnectionError_Machines() {
    var error = document.getElementById('hfErrorType_Machines').value;
    var errorBody = document.getElementById('hfConnectionError_Machines').value;
    showDialog(error, errorBody, 'error');
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CollapseControls_Machines() {
    cmbMachineTypes_Machines.collapse();
    cmbControlStation_Machines.collapse();
}

function tlbItemFormReconstruction_TlbMachine_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvMachineIntroduction_iFrame').src = 'Machines.aspx';
}

function tlbItemHelp_TlbMachine_onClick() {
    LoadHelpPage('tlbItemHelp_TlbMachine');    
}






