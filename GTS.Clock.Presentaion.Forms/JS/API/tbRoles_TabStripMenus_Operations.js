
var CurrentPageState_Roles = 'View';
var ConfirmState_Roles = null;
var ObjRoles_Roles = null;

function GetBoxesHeaders_Roles() {
    document.getElementById('header_Roles_Roles').innerHTML = document.getElementById('hfheader_Roles_Roles').value;
    document.getElementById('header_RoleDetails_Roles').innerHTML = document.getElementById('hfheader_RoleDetails_Roles').value;
}

function trvRoles_Roles_onNodeSelect(sender, e) {
    if (CurrentPageState_Roles != 'Add')
        NavigateRole_Roles(e.get_node());
}

function NavigateRole_Roles(selectedRoleNode) {
    if (selectedRoleNode != undefined) {
        document.getElementById('txtRoleCode_Roles').value = selectedRoleNode.get_value();
        document.getElementById('txtRoleName_Roles').value = selectedRoleNode.get_text();
    }
}

function tlbItemNew_TlbRoles_onClick() {
    ChangePageState_Roles('Add');
    ClearList_Roles();
    FocusOnFirstElement_Roles();
}

function tlbItemEdit_TlbRoles_onClick() {
    ChangePageState_Roles('Edit');
    FocusOnFirstElement_Roles();
}

function tlbItemDelete_TlbRoles_onClick() {
    ChangePageState_Roles('Delete');
}

function tlbItemSave_TlbRoles_onClick() {
    Roles_onSave();
}

function Roles_onSave() {
    if (CurrentPageState_Roles != 'Delete')
        UpdateRoles_Roles();
    else
        ShowDialogConfirm('Delete');
}

function UpdateRoles_Roles() {
    ObjRoles_Roles = new Object();
    ObjRoles_Roles.CustomCode = null;
    ObjRoles_Roles.Name = null;
    ObjRoles_Roles.SelectedID = '0';
    var selectedRoleNode_Roles = trvRoles_Roles.get_selectedNode();
    if (selectedRoleNode_Roles != undefined)
        ObjRoles_Roles.SelectedID = selectedRoleNode_Roles.get_id();
    if (CurrentPageState_Roles != 'Delete') {
        ObjRoles_Roles.CustomCode = document.getElementById('txtRoleCode_Roles').value;
        ObjRoles_Roles.Name = document.getElementById('txtRoleName_Roles').value;
    }
    UpdateRoles_RolesPage(CharToKeyCode_Roles(CurrentPageState_Roles), CharToKeyCode_Roles(ObjRoles_Roles.SelectedID), CharToKeyCode_Roles(ObjRoles_Roles.CustomCode), CharToKeyCode_Roles(ObjRoles_Roles.Name));
    DialogWaiting.Show();
}

function UpdateRoles_RolesPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Roles').value;
            Response[1] = document.getElementById('hfConnectionError_Roles').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            Roles_OnAfterUpdate(Response);
            ClearList_Roles();
            ChangePageState_Roles('View');
        }
        else {
            if (CurrentPageState_Roles == 'Delete')
                ChangePageState_Roles('View');
        }
    }
}

function Roles_OnAfterUpdate(Response) {
    var RolesNodeText = ObjRoles_Roles.Name;
    var RolesNodeValue = ObjRoles_Roles.CustomCode;

    trvRoles_Roles.beginUpdate();
    switch (CurrentPageState_Roles) {
        case 'Add':
            var newRolesNode = new ComponentArt.Web.UI.TreeViewNode();
            newRolesNode.set_text(RolesNodeText);
            newRolesNode.set_value(RolesNodeValue);
            newRolesNode.set_id(Response[3]);
            newRolesNode.set_imageUrl('Images/TreeView/folder.gif');
            trvRoles_Roles.findNodeById(ObjRoles_Roles.SelectedID).get_nodes().add(newRolesNode);
            trvRoles_Roles.selectNodeById(ObjRoles_Roles.SelectedID);
            break;
        case 'Edit':
            var selectedRoleNode = trvRoles_Roles.findNodeById(Response[3]);
            selectedRoleNode.set_text(RolesNodeText);
            selectedRoleNode.set_value(RolesNodeValue);
            trvRoles_Roles.selectNodeById(Response[3]);
            break;
        case 'Delete':
            trvRoles_Roles.findNodeById(ObjRoles_Roles.SelectedID).remove();
            break;

    }
    trvRoles_Roles.endUpdate();
    if (CurrentPageState_Roles == 'Add')
        trvRoles_Roles.get_selectedNode().expand();
}

function tlbItemCancel_TlbRoles_onClick() {
    ChangePageState_Roles('View');
    ClearList_Roles();
}

function tlbItemExit_TlbRoles_onClick() {
    ShowDialogConfirm('Exit');
}

function ChangePageState_Roles(state) {
    CurrentPageState_Roles = state;
    SetActionMode_Roles(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbRoles.get_items().getItemById('tlbItemNew_TlbRoles') != null) {
            TlbRoles.get_items().getItemById('tlbItemNew_TlbRoles').set_enabled(false);
            TlbRoles.get_items().getItemById('tlbItemNew_TlbRoles').set_imageUrl('add_silver.png');
        }
        if (TlbRoles.get_items().getItemById('tlbItemEdit_TlbRoles') != null) {
            TlbRoles.get_items().getItemById('tlbItemEdit_TlbRoles').set_enabled(false);
            TlbRoles.get_items().getItemById('tlbItemEdit_TlbRoles').set_imageUrl('edit_silver.png');
        }
        if (TlbRoles.get_items().getItemById('tlbItemDelete_TlbRoles') != null) {
            TlbRoles.get_items().getItemById('tlbItemDelete_TlbRoles').set_enabled(false);
            TlbRoles.get_items().getItemById('tlbItemDelete_TlbRoles').set_imageUrl('remove_silver.png');
        }
        TlbRoles.get_items().getItemById('tlbItemSave_TlbRoles').set_enabled(true);
        TlbRoles.get_items().getItemById('tlbItemSave_TlbRoles').set_imageUrl('save.png');
        TlbRoles.get_items().getItemById('tlbItemCancel_TlbRoles').set_enabled(true);
        TlbRoles.get_items().getItemById('tlbItemCancel_TlbRoles').set_imageUrl('cancel.png');
        document.getElementById('txtRoleCode_Roles').disabled = '';
        document.getElementById('txtRoleName_Roles').disabled = '';
        if (TlbRoles.get_items().getItemById('tlbItemUserInterfaceAccessLevels_TlbRoles') != null && state == 'Edit') {            
            TlbRoles.get_items().getItemById('tlbItemUserInterfaceAccessLevels_TlbRoles').set_enabled(true);
            TlbRoles.get_items().getItemById('tlbItemUserInterfaceAccessLevels_TlbRoles').set_imageUrl('access.png');
            NavigateRole_Roles(trvRoles_Roles.get_selectedNode());
        }
        if (state == 'Delete')
            Roles_onSave();
    }
    if (state == 'View') {
        if (TlbRoles.get_items().getItemById('tlbItemNew_TlbRoles') != null) {
            TlbRoles.get_items().getItemById('tlbItemNew_TlbRoles').set_enabled(true);
            TlbRoles.get_items().getItemById('tlbItemNew_TlbRoles').set_imageUrl('add.png');
        }
        if (TlbRoles.get_items().getItemById('tlbItemEdit_TlbRoles') != null) {
            TlbRoles.get_items().getItemById('tlbItemEdit_TlbRoles').set_enabled(true);
            TlbRoles.get_items().getItemById('tlbItemEdit_TlbRoles').set_imageUrl('edit.png');
        }
        if (TlbRoles.get_items().getItemById('tlbItemDelete_TlbRoles') != null) {
            TlbRoles.get_items().getItemById('tlbItemDelete_TlbRoles').set_enabled(true);
            TlbRoles.get_items().getItemById('tlbItemDelete_TlbRoles').set_imageUrl('remove.png');
        }
        TlbRoles.get_items().getItemById('tlbItemSave_TlbRoles').set_enabled(false);
        TlbRoles.get_items().getItemById('tlbItemSave_TlbRoles').set_imageUrl('save_silver.png');
        TlbRoles.get_items().getItemById('tlbItemCancel_TlbRoles').set_enabled(false);
        TlbRoles.get_items().getItemById('tlbItemCancel_TlbRoles').set_imageUrl('cancel_silver.png');
        if (TlbRoles.get_items().getItemById('tlbItemUserInterfaceAccessLevels_TlbRoles') != null) {
            TlbRoles.get_items().getItemById('tlbItemUserInterfaceAccessLevels_TlbRoles').set_enabled(false);
            TlbRoles.get_items().getItemById('tlbItemUserInterfaceAccessLevels_TlbRoles').set_imageUrl('access_silver.png');
        }
        document.getElementById('txtRoleCode_Roles').disabled = 'disabled';
        document.getElementById('txtRoleName_Roles').disabled = 'disabled';
    }
}

function SetActionMode_Roles(state) {
    document.getElementById('ActionMode_RolesForm').innerHTML = document.getElementById("hf" + state + "_Roles").value;
}

function ClearList_Roles() {
    if (CurrentPageState_Roles != 'Edit') {
        document.getElementById('txtRoleCode_Roles').value = '';
        document.getElementById('txtRoleName_Roles').value = '';
    }
}

function FocusOnFirstElement_Roles() {
    document.getElementById('txtRoleCode_Roles').focus();
}

function CharToKeyCode_Roles(str) {
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

function Fill_trvRoles_Roles() {
    document.getElementById('loadingPanel_trvRoles_Roles').innerHTML = document.getElementById('hfloadingPanel_trvRoles_Roles').value;
    CallBack_trvRoles_Roles.callback();
}

function CallBack_trvRoles_Roles_CallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Roles').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvRoles_Roles();
    }
}

function Refresh_trvRoles_Roles() {
    Fill_trvRoles_Roles();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Roles = confirmState;
    if (CurrentPageState_Roles == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Roles').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Roles').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Roles) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateRoles_Roles();
            break;
        case 'Exit':
            ClearList_Roles();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Roles('View');
}

function trvRoles_Roles_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvRoles_Roles').innerHTML = "";
}

function tlbItemUserInterfaceAccessLevels_TlbRoles_onClick() {
    ShowDialogAccessLevels();
}

function ShowDialogAccessLevels() {
    var selectedRoleNode_Roles = trvRoles_Roles.get_selectedNode();
    if (selectedRoleNode_Roles != undefined) {
        var ObjDialogUserInterfaceAccessLevels = new Object();
        ObjDialogUserInterfaceAccessLevels.RoleID = selectedRoleNode_Roles.get_id();
        ObjDialogUserInterfaceAccessLevels.RoleName = selectedRoleNode_Roles.get_text();
        parent.DialogUserInterfaceAccessLevels.set_value(ObjDialogUserInterfaceAccessLevels);
        parent.DialogUserInterfaceAccessLevels.Show();
    }
}

function CallBack_trvRoles_Roles_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvRoles_Roles').innerHTML = '';
    ShowConnectionError_Roles();
}

function ShowConnectionError_Roles() {
    var error = document.getElementById('hfErrorType_Roles').value;
    var errorBody = document.getElementById('hfConnectionError_Roles').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbRoles_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvRolesIntroduction_iFrame').src = 'Roles.aspx';
}

function tlbItemHelp_TlbRoles_onClick() {
    LoadHelpPage('tlbItemHelp_TlbRoles');    
}



