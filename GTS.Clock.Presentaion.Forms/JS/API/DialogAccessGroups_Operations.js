
var CurrentPageState_AccessGroups = 'View';
var ConfirmState_AccessGroups = null;
var ObjAccessGroup_AccessGroups = null;

function GetBoxesHeaders_AccessGroups() {
    parent.document.getElementById('Title_DialogAccessGroups').innerHTML = document.getElementById('hfTitle_DialogAccessGroups').value;    
    document.getElementById('header_AccessGroupsBox_AccessGroups').innerHTML = document.getElementById('hfheader_AccessGroupsBox_AccessGroups').value;
    document.getElementById('header_AccessGroupsDetailsBox_AccessGroups').innerHTML = document.getElementById('hfheader_AccessGroupsDetailsBox_AccessGroups').value;
    document.getElementById('header_AccessLevelBox_AccessGroups').innerHTML = document.getElementById('hfheader_AccessLevelBox_AccessGroups').value;
}

function GridAccessGroups_AccessGroups_onItemSelect(sender, e) {
    if (CurrentPageState_AccessGroups != 'Add')
        NavigateAccessGroup_AccessGroups(e.get_item());
    document.getElementById('hfAccessLevelsList_AccessGroups').value = 'null';
}

function NavigateAccessGroup_AccessGroups(selectedAccessGroupItem) {
    if (selectedAccessGroupItem != undefined) {
        document.getElementById('txtAccessGroupName_AccessGroups').value = selectedAccessGroupItem.getMember('Name').get_text();
        document.getElementById('txtDescription_AccessGroups').value = selectedAccessGroupItem.getMember('Description').get_text();
    }
}

function tlbItemNew_TlbAccessGroups_onClick() {
    ChangePageState_AccessGroups('Add');
    ClearList_AccessGroups();
    FocusOnFirstElement_AccessGroups();
}

function tlbItemEdit_TlbAccessGroups_onClick() {
    ChangePageState_AccessGroups('Edit');
    FocusOnFirstElement_AccessGroups();
}

function tlbItemDelete_TlbAccessGroups_onClick() {
    ChangePageState_AccessGroups('Delete');
}

function tlbItemSave_TlbAccessGroups_onClick() {
    AccessGroup_onSave();
}

function tlbItemCancel_TlbAccessGroups_onClick() {
    ChangePageState_AccessGroups('View');
    ClearList_AccessGroups();
}

function tlbItemExit_TlbAccessGroups_onClick() {
    ShowDialogConfirm('Exit');
}

function AccessGroup_onSave() {
    if (CurrentPageState_AccessGroups != 'Delete')
        UpdateAccessGroup_AccessGroups();
    else
        ShowDialogConfirm('Delete');
}

function CharToKeyCode_AccessGroups(str) {
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


function ClearList_AccessGroups() {
    if (CurrentPageState_AccessGroups != 'Edit') {
        document.getElementById('txtAccessGroupName_AccessGroups').value = '';
        document.getElementById('txtDescription_AccessGroups').value = '';
        GridAccessGroups_AccessGroups.unSelectAll();
    }
}

function FocusOnFirstElement_AccessGroups() {
    document.getElementById('txtAccessGroupName_AccessGroups').focus();
}

function ChangePageState_AccessGroups(state) {
    CurrentPageState_AccessGroups = state;
    SetActionMode_AccessGroups(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbAccessGroups.get_items().getItemById('tlbItemNew_TlbAccessGroups') != null) {
            TlbAccessGroups.get_items().getItemById('tlbItemNew_TlbAccessGroups').set_enabled(false);
            TlbAccessGroups.get_items().getItemById('tlbItemNew_TlbAccessGroups').set_imageUrl('add_silver.png');
        }
        if (TlbAccessGroups.get_items().getItemById('tlbItemEdit_TlbAccessGroups') != null) {
            TlbAccessGroups.get_items().getItemById('tlbItemEdit_TlbAccessGroups').set_enabled(false);
            TlbAccessGroups.get_items().getItemById('tlbItemEdit_TlbAccessGroups').set_imageUrl('edit_silver.png');
        }
        if (TlbAccessGroups.get_items().getItemById('tlbItemDelete_TlbAccessGroups') != null) {
            TlbAccessGroups.get_items().getItemById('tlbItemDelete_TlbAccessGroups').set_enabled(false);
            TlbAccessGroups.get_items().getItemById('tlbItemDelete_TlbAccessGroups').set_imageUrl('remove_silver.png');
        }
        TlbAccessGroups.get_items().getItemById('tlbItemSave_TlbAccessGroups').set_enabled(true);
        TlbAccessGroups.get_items().getItemById('tlbItemSave_TlbAccessGroups').set_imageUrl('save.png');
        TlbAccessGroups.get_items().getItemById('tlbItemCancel_TlbAccessGroups').set_enabled(true);
        TlbAccessGroups.get_items().getItemById('tlbItemCancel_TlbAccessGroups').set_imageUrl('cancel.png');
        if (state != 'Delete' && TlbAccessLevel_AccessGroups.get_items().getItemById('tlbItemAccessLevel_TlbAccessLevel_AccessGroups') != null) {
            TlbAccessLevel_AccessGroups.get_items().getItemById('tlbItemAccessLevel_TlbAccessLevel_AccessGroups').set_enabled(true);
            TlbAccessLevel_AccessGroups.get_items().getItemById('tlbItemAccessLevel_TlbAccessLevel_AccessGroups').set_imageUrl('access.png')
        }
        document.getElementById('txtAccessGroupName_AccessGroups').disabled = '';
        document.getElementById('txtDescription_AccessGroups').disabled = '';
        if (state == 'Edit')
            NavigateAccessGroup_AccessGroups(GridAccessGroups_AccessGroups.getSelectedItems()[0]);
        if (state == 'Delete')
            AccessGroup_onSave();
    }
    if (state == 'View') {
        if (TlbAccessGroups.get_items().getItemById('tlbItemNew_TlbAccessGroups') != null) {
            TlbAccessGroups.get_items().getItemById('tlbItemNew_TlbAccessGroups').set_enabled(true);
            TlbAccessGroups.get_items().getItemById('tlbItemNew_TlbAccessGroups').set_imageUrl('add.png');
        }
        if (TlbAccessGroups.get_items().getItemById('tlbItemEdit_TlbAccessGroups') != null) {
            TlbAccessGroups.get_items().getItemById('tlbItemEdit_TlbAccessGroups').set_enabled(true);
            TlbAccessGroups.get_items().getItemById('tlbItemEdit_TlbAccessGroups').set_imageUrl('edit.png');
        }
        if (TlbAccessGroups.get_items().getItemById('tlbItemDelete_TlbAccessGroups') != null) {
            TlbAccessGroups.get_items().getItemById('tlbItemDelete_TlbAccessGroups').set_enabled(true);
            TlbAccessGroups.get_items().getItemById('tlbItemDelete_TlbAccessGroups').set_imageUrl('remove.png');
        }
        TlbAccessGroups.get_items().getItemById('tlbItemSave_TlbAccessGroups').set_enabled(false);
        TlbAccessGroups.get_items().getItemById('tlbItemSave_TlbAccessGroups').set_imageUrl('save_silver.png');
        TlbAccessGroups.get_items().getItemById('tlbItemCancel_TlbAccessGroups').set_enabled(false);
        TlbAccessGroups.get_items().getItemById('tlbItemCancel_TlbAccessGroups').set_imageUrl('cancel_silver.png');
        if (TlbAccessLevel_AccessGroups.get_items().getItemById('tlbItemAccessLevel_TlbAccessLevel_AccessGroups') != null) {
            TlbAccessLevel_AccessGroups.get_items().getItemById('tlbItemAccessLevel_TlbAccessLevel_AccessGroups').set_enabled(false);
            TlbAccessLevel_AccessGroups.get_items().getItemById('tlbItemAccessLevel_TlbAccessLevel_AccessGroups').set_imageUrl('access_silver.png')
        }
        document.getElementById('txtAccessGroupName_AccessGroups').disabled = 'disabled';
        document.getElementById('txtDescription_AccessGroups').disabled = 'disabled';
    }
}

function SetActionMode_AccessGroups(state) {
    document.getElementById('ActionMode_AccessLevel').innerHTML = document.getElementById('ActionMode_AccessGroups').innerHTML = document.getElementById("hf" + state + "_AccessGroups").value;
}

function Fill_GridAccessGroups_AccessGroups() {
    document.getElementById('loadingPanel_GridAccessGroups_AccessGroups').innerHTML = document.getElementById('hfloadingPanel_GridAccessGroups_AccessGroups').value;
    CallBack_GridAccessGroups_AccessGroups.callback();
}

function Refresh_GridAccessGroups_AccessGroups() {
    Fill_GridAccessGroups_AccessGroups();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_AccessGroups = confirmState;
    if (CurrentPageState_AccessGroups == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_AccessGroups').value;
    else {
        if (DialogAccessLevel.get_isShowing())
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_AccessLevels').value;
        else
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_AccessGroups').value;
    }
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_AccessGroups) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateAccessGroup_AccessGroups();
            break;
        case 'Exit':
            if (DialogAccessLevel.get_isShowing()) {
                DialogConfirm.Close();
                CloseDialogAccessLevel();
            }
            else {
                ClearList_AccessGroups();
                CloseDialogAccessGroups();
            }
            break;
        default:
    }
}

function CloseDialogAccessGroups() {
    parent.document.getElementById('DialogAccessGroups_IFrame').src = 'WhitePage.aspx';
    parent.parent.document.getElementById('DialogUnderManagementPersonnel_IFrame').contentWindow.UpdateAccessGroups_onAfterAccessGroupsChange();
    parent.DialogAccessGroups.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    if (!DialogAccessLevel.get_isShowing())
         ChangePageState_AccessGroups('View');
}

function UpdateAccessGroup_AccessGroups() {
    var ObjDialogAccessGroups = parent.DialogAccessGroups.get_value();
    ObjAccessGroup_AccessGroups = new Object();
    ObjAccessGroup_AccessGroups.Name = null;
    ObjAccessGroup_AccessGroups.Description = null;
    ObjAccessGroup_AccessGroups.AccessLevelsList = null;
    ObjAccessGroup_AccessGroups.ID = '0';
    var SelectedItems_GridAccessGroups_AccessGroups = GridAccessGroups_AccessGroups.getSelectedItems();
    if (SelectedItems_GridAccessGroups_AccessGroups.length > 0)
        ObjAccessGroup_AccessGroups.ID = SelectedItems_GridAccessGroups_AccessGroups[0].getMember("ID").get_text();

    if (CurrentPageState_AccessGroups != 'Delete') {
        ObjAccessGroup_AccessGroups.Name = document.getElementById('txtAccessGroupName_AccessGroups').value;
        ObjAccessGroup_AccessGroups.Description = document.getElementById('txtDescription_AccessGroups').value;
        ObjAccessGroup_AccessGroups.AccessLevelsList = document.getElementById('hfAccessLevelsList_AccessGroups').value;
    }
    UpdateAccessGroup_AccessGroupsPage(CharToKeyCode_AccessGroups(CurrentPageState_AccessGroups), CharToKeyCode_AccessGroups(ObjDialogAccessGroups.FlowState), CharToKeyCode_AccessGroups(ObjAccessGroup_AccessGroups.ID), CharToKeyCode_AccessGroups(ObjAccessGroup_AccessGroups.Name), CharToKeyCode_AccessGroups(ObjAccessGroup_AccessGroups.Description), CharToKeyCode_AccessGroups(ObjAccessGroup_AccessGroups.AccessLevelsList));
    DialogWaiting.Show();
}

function UpdateAccessGroup_AccessGroupsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_AccessLevels').value;
            Response[1] = document.getElementById('hfConnectionError_AccessLevels').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_AccessGroups();
            AccessGroup_OnAfterUpdate(Response);
            ChangePageState_AccessGroups('View');
        }
        else {
            if (CurrentPageState_AccessGroups == 'Delete')
                ChangePageState_AccessGroups('View');
        }
    }
}

function AccessGroup_OnAfterUpdate(Response) {
    if (ObjAccessGroup_AccessGroups != null) {
        var AccessGroupName = ObjAccessGroup_AccessGroups.Name;
        var AccessGroupDescription = ObjAccessGroup_AccessGroups.Description;

        var AccessGroupItem = null;
        GridAccessGroups_AccessGroups.beginUpdate();
        switch (CurrentPageState_AccessGroups) {
            case 'Add':
                AccessGroupItem = GridAccessGroups_AccessGroups.get_table().addEmptyRow(GridAccessGroups_AccessGroups.get_recordCount());
                AccessGroupItem.setValue(0, Response[3], false);
                GridAccessGroups_AccessGroups.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridAccessGroups_AccessGroups.selectByKey(Response[3], 0, false);
                AccessGroupItem = GridAccessGroups_AccessGroups.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridAccessGroups_AccessGroups.selectByKey(ObjAccessGroup_AccessGroups.ID, 0, false);
                GridAccessGroups_AccessGroups.deleteSelected();
                break;
        }
        if (CurrentPageState_AccessGroups != 'Delete') {
            AccessGroupItem.setValue(1, AccessGroupName, false);
            AccessGroupItem.setValue(2, AccessGroupDescription, false);
        }
        GridAccessGroups_AccessGroups.endUpdate();
    }
}

function CallBack_GridAccessGroups_AccessGroups_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_AccessGroups').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridAccessGroups_AccessGroups();
    }
}

function GridAccessGroups_AccessGroups_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridAccessGroups_AccessGroups').innerHTML = '';
}

function tlbItemSave_TlbAccessLevel_onClick() {
    CloseDialogAccessLevel();
}

function CloseDialogAccessLevel() {
    ClearList_AccessLevel_AccessGroups();
    DialogAccessLevel.Close();
}

function ClearList_AccessLevel_AccessGroups() {
    document.getElementById('txtAccessGroupName_AccessLevel_AccessGroups').value = '';
    trvAccessLevel_AccessGroups.unCheckAll();
}

function tlbItemExit_TlbAccessLevel_onClick() {
    document.getElementById('hfAccessLevelsList_AccessGroups').value = 'null';
    ShowDialogConfirm('Exit');
}

function Refresh_trvAccessLevel_AccessGroups() {
    Fill_trvAccessLevel_AccessGroups();
}

function trvAccessLevel_AccessGroups_onNodeCheckChange(sender, e) {
   var currentNode_trvAccessLevel_AccessGroups = e.get_node();
   var checked = false;
   if (currentNode_trvAccessLevel_AccessGroups.get_checked())
       checked = true;
   if (currentNode_trvAccessLevel_AccessGroups.get_parentNode() == undefined || currentNode_trvAccessLevel_AccessGroups.get_parentNode() == null) {
       trvAccessLevel_AccessGroups.beginUpdate();
       switch (checked) {
           case true:
               currentNode_trvAccessLevel_AccessGroups.checkAll();
               break;
           case false:
               currentNode_trvAccessLevel_AccessGroups.unCheckAll();
               break;
       }
       trvAccessLevel_AccessGroups.endUpdate();
   }
   CreateAccessLevelsList_AccessGroups(currentNode_trvAccessLevel_AccessGroups, checked);     
}

function CreateAccessLevelsList_AccessGroups(accessLevelNode, checkeState) {
    var AccessLevelsList_AccessGroups = document.getElementById('hfAccessLevelsList_AccessGroups').value;
    var parentID = undefined;
    if (accessLevelNode.get_parentNode() != undefined)
        parentID = accessLevelNode.get_parentNode().get_id();
    var CurrentListPart = 'ID=' + accessLevelNode.get_id() + '%Ch=' + checkeState.toString() + '%P=' + parentID;
    var BeforeListPart = 'ID=' + accessLevelNode.get_id() + '%Ch=' + (!checkeState).toString() + '%P=' + parentID;
    if (parentID == undefined) {
        switch (checkeState) {
            case true:
                AccessLevelsList_AccessGroups = AccessLevelsList_AccessGroups.replace(new RegExp('P=' + accessLevelNode.get_id(), 'g'), 'Delete');
                AccessLevelsList_AccessGroups += CurrentListPart + '#';
                break;
            case false:
                AccessLevelsList_AccessGroups = AccessLevelsList_AccessGroups.replace(BeforeListPart + '#', '');
                AccessLevelsList_AccessGroups = AccessLevelsList_AccessGroups.replace(new RegExp('P=' + accessLevelNode.get_id(), 'g'), 'Delete');
                break;
        }
    }
    else {
        var NodeChecked = accessLevelNode.get_parentNode().get_checked();
        if (NodeChecked == true || NodeChecked == 1) {
            switch (checkeState) {
                case true:
                    AccessLevelsList_AccessGroups = AccessLevelsList_AccessGroups.replace(BeforeListPart + '#', '');
                    break;
                case false:
                    AccessLevelsList_AccessGroups += CurrentListPart + '#';
                    break;
            }
        }
        else {
            if (NodeChecked == false || NodeChecked == 0) {
                switch (checkeState) {
                    case true:
                        AccessLevelsList_AccessGroups = AccessLevelsList_AccessGroups.replace('ID=' + accessLevelNode.get_id() + '%Ch=' + true.toString() + '%Delete', '');
                        AccessLevelsList_AccessGroups += CurrentListPart + '#';
                        break;
                    case false:
                        AccessLevelsList_AccessGroups = AccessLevelsList_AccessGroups.replace(BeforeListPart + '#', '');
                        break;
                }
            }
            else {
                if (NodeChecked == undefined) {
                    switch (checkeState) {
                        case true:
                            AccessLevelsList_AccessGroups += CurrentListPart + '#';
                            break;
                        case false:
                            AccessLevelsList_AccessGroups = AccessLevelsList_AccessGroups.replace(BeforeListPart + '#', '');
                            break;
                    }
                }
            }
        }
    }
    document.getElementById('hfAccessLevelsList_AccessGroups').value = AccessLevelsList_AccessGroups;
}

function trvAccessLevel_AccessGroups_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvAccessLevel_AccessGroups').innerHTML = '';
}

function CallBack_trvAccessLevel_AccessGroups_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_AccessLevel').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvAccessLevel_AccessGroups();
    }
}

function Fill_trvAccessLevel_AccessGroups() {
    var AccessGroupID = '0';
    document.getElementById('loadingPanel_trvAccessLevel_AccessGroups').innerHTML = document.getElementById('hfloadingPanel_trvAccessLevel_AccessGroups').value;
    var selectedItems_GridAccessGroups_AccessGroups = GridAccessGroups_AccessGroups.getSelectedItems();
    if (CurrentPageState_AccessGroups == 'Edit')
        AccessGroupID = selectedItems_GridAccessGroups_AccessGroups[0].getMember('ID').get_text();
    CallBack_trvAccessLevel_AccessGroups.callback(CharToKeyCode_AccessGroups(AccessGroupID));
}

function DialogAccessLevel_OnShow(sender, e) {
    document.getElementById('Title_DialogAccessLevel').innerHTML = document.getElementById('hfTitle_DialogAccessLevel').value;
    SetAccessGroupTitle_AccessGroups();
    var CurrentLangID = parent.parent.CurrentLangID;
    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogAccessLevel_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogAccessLevel_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogAccessLevel_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogAccessLevel_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogAccessLevel').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogAccessLevel').align = 'right';
    Fill_trvAccessLevel_AccessGroups();
}

function tlbItemAccessLevel_TlbAccessLevel_AccessGroups_onClick() {
    if (CurrentPageState_AccessGroups == 'Add' || (CurrentPageState_AccessGroups == 'Edit' && GridAccessGroups_AccessGroups.getSelectedItems().length > 0))
        ShowDialogAccessLevel();
}

function ShowDialogAccessLevel() {
    DialogAccessLevel.Show();
}

function SetAccessGroupTitle_AccessGroups() { 
    var AccessGroupTitle = '';
    switch (CurrentPageState_AccessGroups) {
        case 'Add':
            if (document.getElementById('txtAccessGroupName_AccessGroups').value != '')
                AccessGroupTitle = document.getElementById('txtAccessGroupName_AccessGroups').value;
            break;
        case 'Edit':
            if (GridAccessGroups_AccessGroups.getSelectedItems().length > 0)
                AccessGroupTitle = GridAccessGroups_AccessGroups.getSelectedItems()[0].getMember('Name').get_text();
            break;
    }
    document.getElementById('txtAccessGroupName_AccessLevel_AccessGroups').value = AccessGroupTitle;
}

function CallBack_GridAccessGroups_AccessGroups_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridAccessGroups_AccessGroups').innerHTML = '';
    ShowConnectionError_AccessLevels();
}

function ShowConnectionError_AccessLevels() {
    var error = document.getElementById('hfErrorType_AccessLevels').value;
    var errorBody = document.getElementById('hfConnectionError_AccessLevels').value;
    showDialog(error, errorBody, 'error');
}

function CallBack_trvAccessLevel_AccessGroups_onCallbackError(sender, e) {
    ShowConnectionError_AccessLevels();
}

function tlbItemFormReconstruction_TlbAccessGroups_onClick() {
    CloseDialogAccessGroups();
    parent.parent.document.getElementById('DialogUnderManagementPersonnel_IFrame').contentWindow.CollapseControls_UnderManagementPersonnel();
    parent.parent.document.getElementById('DialogUnderManagementPersonnel_IFrame').contentWindow.ShowDialogAccessGroups();
}

function tlbItemHelp_TlbAccessGroups_onClick() {
    LoadHelpPage('tlbItemHelp_TlbAccessGroups');
}















