
var CurrentPageState_MissionLocations = 'View';
var ConfirmState_MissionLocations = null;
var ObjMissionLocation_MissionLocations = null;

function GetBoxesHeaders_MissionLocations() {
    document.getElementById('header_MissionLocations_MissionLocationsIntroduction').innerHTML = document.getElementById('hfheader_MissionLocations_MissionLocationsIntroduction').value;
    document.getElementById('header_MissionLocationsDetails_MissionLocationIntroduction').innerHTML = document.getElementById('hfheader_MissionLocationsDetails_MissionLocationIntroduction').value;
}

function tlbItemNew_TlbMissionOverallLocation_onClick() {
    ChangePageState_MissionLocations('Add');
    ClearList_MissionLocations();
    FocusOnFirstElement_MissionLocations();
}

function tlbItemEdit_TlbMissionOverallLocation_onClick() {
    ChangePageState_MissionLocations('Edit');
    FocusOnFirstElement_MissionLocations();
}

function tlbItemDelete_TlbMissionOverallLocation() {
    ChangePageState_MissionLocations('Delete');
}

function tlbItemSave_TlbMissionOverallLocation_onClick() {
    MissionLocation_onSave();
}

function tlbItemCancel_TlbMissionOverallLocation_onClick() {
    DialogConfirm.Close();
    ChangePageState_MissionLocations('View');
}

function ChangePageState_MissionLocations(state) {
    CurrentPageState_MissionLocations = state;
    SetActionMode_MissionLocations(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbMissionOverallLocation.get_items().getItemById('tlbItemNew_TlbMissionOverallLocation') != null) {
            TlbMissionOverallLocation.get_items().getItemById('tlbItemNew_TlbMissionOverallLocation').set_enabled(false);
            TlbMissionOverallLocation.get_items().getItemById('tlbItemNew_TlbMissionOverallLocation').set_imageUrl('add_silver.png');
        }
        if (TlbMissionOverallLocation.get_items().getItemById('tlbItemEdit_TlbMissionOverallLocation') != null) {
            TlbMissionOverallLocation.get_items().getItemById('tlbItemEdit_TlbMissionOverallLocation').set_enabled(false);
            TlbMissionOverallLocation.get_items().getItemById('tlbItemEdit_TlbMissionOverallLocation').set_imageUrl('edit_silver.png');
        }
        if (TlbMissionOverallLocation.get_items().getItemById('tlbItemDelete_TlbMissionOverallLocation') != null) {
            TlbMissionOverallLocation.get_items().getItemById('tlbItemDelete_TlbMissionOverallLocation').set_enabled(false);
            TlbMissionOverallLocation.get_items().getItemById('tlbItemDelete_TlbMissionOverallLocation').set_imageUrl('remove_silver.png');
        }
        TlbMissionOverallLocation.get_items().getItemById('tlbItemSave_TlbMissionOverallLocation').set_enabled(true);
        TlbMissionOverallLocation.get_items().getItemById('tlbItemSave_TlbMissionOverallLocation').set_imageUrl('save.png');
        TlbMissionOverallLocation.get_items().getItemById('tlbItemCancel_TlbMissionOverallLocation').set_enabled(true);
        TlbMissionOverallLocation.get_items().getItemById('tlbItemCancel_TlbMissionOverallLocation').set_imageUrl('cancel.png');
        document.getElementById('txtMissionLocationCode_MissionLocationIntroduction').disabled = '';
        document.getElementById('txtMissionLocationName_MissionLocationIntroduction').disabled = '';
        if (state == 'Edit')
            NavigateMissionLocation_MissionLocations(trvMissionLocationsIntroduction_MissionLocationsIntroduction.get_selectedNode());
        if (state == 'Delete')
            MissionLocation_onSave();
    }
    if (state == 'View') {
        if (TlbMissionOverallLocation.get_items().getItemById('tlbItemNew_TlbMissionOverallLocation') != null) {
            TlbMissionOverallLocation.get_items().getItemById('tlbItemNew_TlbMissionOverallLocation').set_enabled(true);
            TlbMissionOverallLocation.get_items().getItemById('tlbItemNew_TlbMissionOverallLocation').set_imageUrl('add.png');
        }
        if (TlbMissionOverallLocation.get_items().getItemById('tlbItemEdit_TlbMissionOverallLocation') != null) {
            TlbMissionOverallLocation.get_items().getItemById('tlbItemEdit_TlbMissionOverallLocation').set_enabled(true);
            TlbMissionOverallLocation.get_items().getItemById('tlbItemEdit_TlbMissionOverallLocation').set_imageUrl('edit.png');
        }
        if (TlbMissionOverallLocation.get_items().getItemById('tlbItemDelete_TlbMissionOverallLocation') != null) {
            TlbMissionOverallLocation.get_items().getItemById('tlbItemDelete_TlbMissionOverallLocation').set_enabled(true);
            TlbMissionOverallLocation.get_items().getItemById('tlbItemDelete_TlbMissionOverallLocation').set_imageUrl('remove.png');
        }
        TlbMissionOverallLocation.get_items().getItemById('tlbItemSave_TlbMissionOverallLocation').set_enabled(false);
        TlbMissionOverallLocation.get_items().getItemById('tlbItemSave_TlbMissionOverallLocation').set_imageUrl('save_silver.png');
        TlbMissionOverallLocation.get_items().getItemById('tlbItemCancel_TlbMissionOverallLocation').set_enabled(false);
        TlbMissionOverallLocation.get_items().getItemById('tlbItemCancel_TlbMissionOverallLocation').set_imageUrl('cancel_silver.png');
        document.getElementById('txtMissionLocationCode_MissionLocationIntroduction').disabled = 'disabled';
        document.getElementById('txtMissionLocationName_MissionLocationIntroduction').disabled = 'disabled';
    }
}

function SetActionMode_MissionLocations(state) {
    document.getElementById('ActionMode_MissionOverallLocationForm').innerHTML = document.getElementById("hf" + state + "_MissionLocations").value;
}

function ClearList_MissionLocations() {
    if (CurrentPageState_MissionLocations != 'Edit') {
        document.getElementById('txtMissionLocationCode_MissionLocationIntroduction').value = '';
        document.getElementById('txtMissionLocationName_MissionLocationIntroduction').value = '';
    }
}

function FocusOnFirstElement_MissionLocations() {
    document.getElementById('txtMissionLocationCode_MissionLocationIntroduction').focus();
}

function tlbItemExit_TlbMissionOverallLocation_onClick() {
    ShowDialogConfirm('Exit');
}

function MissionLocation_onSave() {
    if (CurrentPageState_MissionLocations != 'Delete')
        UpdateMissionLocation_MissionLocations();
    else
        ShowDialogConfirm('Delete');
}

function UpdateMissionLocation_MissionLocations() {
    ObjMissionLocation_MissionLocations = new Object();
    ObjMissionLocation_MissionLocations.CustomCode = null;
    ObjMissionLocation_MissionLocations.Name = null;
    ObjMissionLocation_MissionLocations.SelectedID = '0';
    ObjMissionLocation_MissionLocations.ParentID = '0';
    var SelectedMissionLocationNode_MissionLocations = trvMissionLocationsIntroduction_MissionLocationsIntroduction.get_selectedNode();
    if (SelectedMissionLocationNode_MissionLocations != undefined) {
        ObjMissionLocation_MissionLocations.SelectedID = SelectedMissionLocationNode_MissionLocations.get_id();
        if (SelectedMissionLocationNode_MissionLocations.get_parentNode() != undefined)
            ObjMissionLocation_MissionLocations.ParentID = SelectedMissionLocationNode_MissionLocations.get_parentNode().get_id();
    }
    if (CurrentPageState_MissionLocations != 'Delete') {
        ObjMissionLocation_MissionLocations.CustomCode = document.getElementById('txtMissionLocationCode_MissionLocationIntroduction').value;
        ObjMissionLocation_MissionLocations.Name = document.getElementById('txtMissionLocationName_MissionLocationIntroduction').value;
    }
    UpdateMissionLocation_MissionLocationsPage(CharToKeyCode_MissionLocations(CurrentPageState_MissionLocations), CharToKeyCode_MissionLocations(ObjMissionLocation_MissionLocations.ParentID), CharToKeyCode_MissionLocations(ObjMissionLocation_MissionLocations.SelectedID), CharToKeyCode_MissionLocations(ObjMissionLocation_MissionLocations.CustomCode), CharToKeyCode_MissionLocations(ObjMissionLocation_MissionLocations.Name));
    DialogWaiting.Show();
}

function UpdateMissionLocation_MissionLocationsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_MissionLocations').value;
            Response[1] = document.getElementById('hfConnectionError_MissionLocations').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            MissionLocation_OnAfterUpdate(Response);
            ClearList_MissionLocations();
            ChangePageState_MissionLocations('View');
        }
        else {
            if (CurrentPageState_MissionLocations == 'Delete')
                ChangePageState_MissionLocations('View');
        }
    }
}

function MissionLocation_OnAfterUpdate(Response) {
    var MissionLocationNodeText = ObjMissionLocation_MissionLocations.Name;
    var MissionLocationNodeValue = ObjMissionLocation_MissionLocations.CustomCode;

    trvMissionLocationsIntroduction_MissionLocationsIntroduction.beginUpdate();
    switch (CurrentPageState_MissionLocations) {
        case 'Add':
            var newMissionLocationNode = new ComponentArt.Web.UI.TreeViewNode();
            newMissionLocationNode.set_text(MissionLocationNodeText);
            newMissionLocationNode.set_value(MissionLocationNodeValue);
            newMissionLocationNode.set_id(Response[3]);
            newMissionLocationNode.set_imageUrl('Images/TreeView/folder.gif');
            trvMissionLocationsIntroduction_MissionLocationsIntroduction.findNodeById(ObjMissionLocation_MissionLocations.SelectedID).get_nodes().add(newMissionLocationNode);
            trvMissionLocationsIntroduction_MissionLocationsIntroduction.selectNodeById(ObjMissionLocation_MissionLocations.SelectedID);
            break;
        case 'Edit':
            var selectedMissionLocationNode = trvMissionLocationsIntroduction_MissionLocationsIntroduction.findNodeById(Response[3]);
            selectedMissionLocationNode.set_text(MissionLocationNodeText);
            selectedMissionLocationNode.set_value(MissionLocationNodeValue);
            trvMissionLocationsIntroduction_MissionLocationsIntroduction.selectNodeById(Response[3]);
            break;
        case 'Delete':
            trvMissionLocationsIntroduction_MissionLocationsIntroduction.findNodeById(ObjMissionLocation_MissionLocations.SelectedID).remove();
            break;
    }
    trvMissionLocationsIntroduction_MissionLocationsIntroduction.endUpdate();
    if (CurrentPageState_MissionLocations == 'Add')
        trvMissionLocationsIntroduction_MissionLocationsIntroduction.get_selectedNode().expand();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_MissionLocations = confirmState;
    if (CurrentPageState_MissionLocations == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_MissionLocations').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_MissionLocations').value;
    DialogConfirm.Show();
}


function Refresh_trvMissionLocationsIntroduction_MissionLocationsIntroduction() {
    Fill_trvMissionLocationsIntroduction_MissionLocationsIntroduction();
}

function Fill_trvMissionLocationsIntroduction_MissionLocationsIntroduction() {
    document.getElementById('loadingPanel_trvMissionLocationsIntroduction_MissionLocationsIntroduction').innerHTML = document.getElementById('hfloadingPanel_trvMissionLocationsIntroduction_MissionLocationsIntroduction').value;
    CallBack_trvMissionLocationsIntroduction_MissionLocationsIntroduction.callback();
}

function trvMissionLocationsIntroduction_MissionLocationsIntroduction_onNodeSelect(sender, e) {
    if (CurrentPageState_MissionLocations != 'Add')
        NavigateMissionLocation_MissionLocations(e.get_node());
}

function NavigateMissionLocation_MissionLocations(selectedMissionLocationNode) {
    if (selectedMissionLocationNode != undefined) {
        document.getElementById('txtMissionLocationCode_MissionLocationIntroduction').value = selectedMissionLocationNode.get_value();
        document.getElementById('txtMissionLocationName_MissionLocationIntroduction').value = selectedMissionLocationNode.get_text();
    }
}

function trvMissionLocationsIntroduction_MissionLocationsIntroduction_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvMissionLocationsIntroduction_MissionLocationsIntroduction').innerHTML = "";
}

function CallBack_trvMissionLocationsIntroduction_MissionLocationsIntroduction_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MissionLocations').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvMissionLocationsIntroduction_MissionLocationsIntroduction();
    }
}

function CallBack_trvMissionLocationsIntroduction_MissionLocationsIntroduction_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvMissionLocationsIntroduction_MissionLocationsIntroduction').innerHTML = '';
    ShowConnectionError_MissionLocationsIntroduction();
}

function CharToKeyCode_MissionLocations(str) {
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

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_MissionLocations) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateMissionLocation_MissionLocations();
            break;
        case 'Exit':
            ClearList_MissionLocations();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_MissionLocations('View');
}

function tlbItemFormReconstruction_TlbMissionOverallLocation_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvMissionLocationsIntroduction_iFrame').src = 'MissionLocations.aspx';
}

function tlbItemHelp_TlbMissionOverallLocation_onClick() {
    LoadHelpPage('tlbItemHelp_TlbMissionOverallLocation');
}

 