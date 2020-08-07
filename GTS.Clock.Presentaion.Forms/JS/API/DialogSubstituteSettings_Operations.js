
var ObjexpandingDepPersonnelNode_SubstituteSettings = null;
var CurrentPageState_SubstituteSettings = 'View';
var ConfirmState_SubstituteSettings = null;
var ObjSubstituteSettings = null;

function GetBoxesHeaders_SubstituteSettings() {
    parent.document.getElementById('Title_DialogSubstituteSettings').innerHTML = document.getElementById('hfTitle_DialogSubstituteSettings').value;
    document.getElementById('header_ManagerWorkFlows_SubstituteSettings').innerHTML = document.getElementById('hfheader_ManagerWorkFlows_SubstituteSettings').value;
    document.getElementById('header_UnderManagementPersonnel_SubstituteSettings').innerHTML = document.getElementById('hfheader_UnderManagementPersonnel_SubstituteSettings').value;
    document.getElementById('header_WorkFlowAccessLevels_SubstituteSettings').innerHTML = document.getElementById('hfheader_WorkFlowAccessLevels_SubstituteSettings').value;
}

function SetSubstituteInfo_SubstituteSettings() {
    var ObjDialogSubstituteSettings = parent.DialogSubstituteSettings.get_value();
    document.getElementById('txtManager_SubstituteSettings').value = ObjDialogSubstituteSettings.ManagerName;
    document.getElementById('txtSubstitute_SubstituteSettings').value = ObjDialogSubstituteSettings.PersonnelName;
}

function tlbItemDelete_TlbSubstituteSettings_onClick() {
    CurrentPageState_SubstituteSettings = 'Delete';
    SubstituteSettings_onSave();
}

function tlbItemSave_TlbSubstituteSettings_onClick() {
    SubstituteSettings_onSave();
}

function SubstituteSettings_onSave() {
    if (CurrentPageState_SubstituteSettings != 'Delete')
        UpdateSubstituteSetting_SubstituteSettings();
    else
        ShowDialogConfirm('Delete');
}


function tlbItemCancel_TlbSubstituteSettings_onClick() {
    DialogConfirm.Close();
}

function tlbItemExit_TlbSubstituteSettings_onClick() {
    ShowDialogConfirm('Exit');
}

function SubstituteSettings_onClose() {
    parent.document.getElementById('DialogSubstituteSettings_IFrame').src = 'WhitePage.aspx';
    parent.DialogSubstituteSettings.Close();
}

function Refresh_GridManagerWorkFlows_SubstituteSettings() {
    Fill_GridManagerWorkFlows_SubstituteSettings();
}

function Fill_GridManagerWorkFlows_SubstituteSettings() {
    document.getElementById('loadingPanel_GridManagerWorkFlows_SubstituteSettings').innerHTML = document.getElementById('hfloadingPanel_GridManagerWorkFlows_SubstituteSettings').value;
    var ObjDialogSubstituteSettings = parent.DialogSubstituteSettings.get_value();
    var managerPersonnelID = ObjDialogSubstituteSettings.ManagerPersonnelID;
    var substituteID = ObjDialogSubstituteSettings.SubstituteID;
    CallBack_GridManagerWorkFlows_SubstituteSettings.callback(CharToKeyCode_SubstituteSettings(substituteID), CharToKeyCode_SubstituteSettings(managerPersonnelID));
}

function GridManagerWorkFlows_SubstituteSettings_onItemSelect(sender, e) {
    Fill_trvUnderManagementPersonnel_SubstituteSettings();
    Fill_trvWorkFlowAccessLevels_SubstituteSettings();
}

function GridManagerWorkFlows_SubstituteSettings_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridManagerWorkFlows_SubstituteSettings').innerHTML = '';
}

function CallBack_GridManagerWorkFlows_SubstituteSettings_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ManagerWorkFlows').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridManagerWorkFlows_SubstituteSettings();
    }
}

function CallBack_GridManagerWorkFlows_SubstituteSettings_onCallbackError(sender, e) {
    ShowConnectionError_SubstituteSettings();
}

function Refresh_trvUnderManagementPersonnel_SubstituteSettings() {
    Fill_trvUnderManagementPersonnel_SubstituteSettings();
}

function Fill_trvUnderManagementPersonnel_SubstituteSettings() {
    document.getElementById('loadingPanel_trvUnderManagementPersonnel_SubstituteSettings').innerHTML = document.getElementById('hfloadingPanel_trvUnderManagementPersonnel_SubstituteSettings').value;
    var SelectedItems_GridManagerWorkFlows_SubstituteSettings = GridManagerWorkFlows_SubstituteSettings.getSelectedItems();
    if (SelectedItems_GridManagerWorkFlows_SubstituteSettings.length > 0) {
        var managerFlowID = SelectedItems_GridManagerWorkFlows_SubstituteSettings[0].getMember('ID').get_text();
        CallBack_trvUnderManagementPersonnel_SubstituteSettings.callback(CharToKeyCode_SubstituteSettings(managerFlowID));
    }
}

function trvUnderManagementPersonnel_SubstituteSettings_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvUnderManagementPersonnel_SubstituteSettings').innerHTML = '';
}

function trvUnderManagementPersonnel_SubstituteSettings_onCallbackComplete(sender, e) {
    if (ObjexpandingDepPersonnelNode_SubstituteSettings != null) {
        if (ObjexpandingDepPersonnelNode_SubstituteSettings.Node.get_nodes().get_length() == 0 && ObjexpandingDepPersonnelNode_SubstituteSettings.HasChild) {
            ObjexpandingDepPersonnelNode_SubstituteSettings = null;
            GetLoadonDemandError_DepartmetsPersonnel_SubstituteSettingsPage();
        }
        else
            ObjexpandingDepPersonnelNode_SubstituteSettings = null;
    }
}

function GetLoadonDemandError_DepartmetsPersonnel_SubstituteSettingsPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function trvUnderManagementPersonnel_SubstituteSettings_onNodeBeforeExpand(sender, e) {
    if (ObjexpandingDepPersonnelNode_SubstituteSettings != null)
        ObjexpandingDepPersonnelNode_SubstituteSettings = null;
    ObjexpandingDepPersonnelNode_SubstituteSettings = new Object();
    ObjexpandingDepPersonnelNode_SubstituteSettings.Node = e.get_node();
    if (e.get_node().get_nodes().get_length() == 1 && (e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined || e.get_node().get_nodes().get_nodeArray()[0].get_id() == '')) {
        ObjexpandingDepPersonnelNode_SubstituteSettings.HasChild = true;
        trvUnderManagementPersonnel_SubstituteSettings.beginUpdate();
        ObjexpandingDepPersonnelNode_SubstituteSettings.Node.get_nodes().remove(0);
        trvUnderManagementPersonnel_SubstituteSettings.endUpdate();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            ObjexpandingDepPersonnelNode_SubstituteSettings.HasChild = false;
        else
            ObjexpandingDepPersonnelNode_SubstituteSettings.HasChild = true;
    }
}

function CallBack_trvUnderManagementPersonnel_SubstituteSettings_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_UnderManagementPersonnel').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvUnderManagementPersonnel_SubstituteSettings();
    }
}

function CallBack_trvUnderManagementPersonnel_SubstituteSettings_onCallbackError(sender, e) {
    ShowConnectionError_SubstituteSettings();
}

function Refresh_trvWorkFlowAccessLevels_SubstituteSettings() {
    Fill_trvWorkFlowAccessLevels_SubstituteSettings();
}

function Fill_trvWorkFlowAccessLevels_SubstituteSettings() {
    document.getElementById('loadingPanel_trvWorkFlowAccessLevels_SubstituteSettings').innerHTML = document.getElementById('hfloadingPanel_trvWorkFlowAccessLevels_SubstituteSettings').value;
    var SelectedItems_GridManagerWorkFlows_SubstituteSettings = GridManagerWorkFlows_SubstituteSettings.getSelectedItems();
    if (SelectedItems_GridManagerWorkFlows_SubstituteSettings.length > 0) {
        var ObjDialogSubstituteSettings = parent.DialogSubstituteSettings.get_value();
        var managerFlowID = SelectedItems_GridManagerWorkFlows_SubstituteSettings[0].getMember('ID').get_text();
        var substituteID = ObjDialogSubstituteSettings.SubstituteID;
        CallBack_trvWorkFlowAccessLevels_SubstituteSettings.callback(CharToKeyCode_SubstituteSettings(substituteID), CharToKeyCode_SubstituteSettings(managerFlowID));
    }
}

function trvWorkFlowAccessLevels_SubstituteSettings_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvWorkFlowAccessLevels_SubstituteSettings').innerHTML = '';
}

function CallBack_trvWorkFlowAccessLevels_SubstituteSettings_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkFlowAccessLevels').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvWorkFlowAccessLevels_SubstituteSettings();
    }
}

function CallBack_trvWorkFlowAccessLevels_SubstituteSettings_onCallbackError(sender, e) {
    ShowConnectionError_SubstituteSettings();
}

function trvWorkFlowAccessLevels_SubstituteSettings_onNodeCheckChange(sender, e) {
    var currentNode_trvWorkFlowAccessLevels_SubstituteSettings = e.get_node();
    var checked = false;
    if (currentNode_trvWorkFlowAccessLevels_SubstituteSettings.get_checked())
        checked = true;
    if (currentNode_trvWorkFlowAccessLevels_SubstituteSettings.get_parentNode() == undefined || currentNode_trvWorkFlowAccessLevels_SubstituteSettings.get_parentNode() == null) {
        trvWorkFlowAccessLevels_SubstituteSettings.beginUpdate();
        switch (checked) {
            case true:
                currentNode_trvWorkFlowAccessLevels_SubstituteSettings.checkAll();
                break;
            case false:
                currentNode_trvWorkFlowAccessLevels_SubstituteSettings.unCheckAll();
                break;
        }
        trvWorkFlowAccessLevels_SubstituteSettings.endUpdate();
    }
    CreateAccessLevelsList_SubstituteSettings(currentNode_trvWorkFlowAccessLevels_SubstituteSettings, checked);
}

function CreateAccessLevelsList_SubstituteSettings(accessLevelNode, checkeState) {
    var AccessLevelsList_SubstituteSettings = document.getElementById('hfAccessLevelsList_SubstituteSettings').value;
    var parentID = undefined;
    if (accessLevelNode.get_parentNode() != undefined)
        parentID = accessLevelNode.get_parentNode().get_id();
    var CurrentListPart = 'ID=' + accessLevelNode.get_id() + '%Ch=' + checkeState.toString() + '%P=' + parentID;
    var BeforeListPart = 'ID=' + accessLevelNode.get_id() + '%Ch=' + (!checkeState).toString() + '%P=' + parentID;
    if (parentID == undefined) {
        switch (checkeState) {
            case true:
                AccessLevelsList_SubstituteSettings = AccessLevelsList_SubstituteSettings.replace(new RegExp('P=' + accessLevelNode.get_id(), 'g'), 'Delete');
                AccessLevelsList_SubstituteSettings += CurrentListPart + '#';
                break;
            case false:
                AccessLevelsList_SubstituteSettings = AccessLevelsList_SubstituteSettings.replace(BeforeListPart + '#', '');
                AccessLevelsList_SubstituteSettings = AccessLevelsList_SubstituteSettings.replace(new RegExp('P=' + accessLevelNode.get_id(), 'g'), 'Delete');
                break;
        }
    }
    else {
        var NodeChecked = accessLevelNode.get_parentNode().get_checked();
        if (NodeChecked == true || NodeChecked == 1) {
            switch (checkeState) {
                case true:
                    AccessLevelsList_SubstituteSettings = AccessLevelsList_SubstituteSettings.replace(BeforeListPart + '#', '');
                    break;
                case false:
                    AccessLevelsList_SubstituteSettings += CurrentListPart + '#';
                    break;
            }
        }
        else {
            if (NodeChecked == false || NodeChecked == 0) {
                switch (checkeState) {
                    case true:
                        AccessLevelsList_SubstituteSettings = AccessLevelsList_SubstituteSettings.replace('ID=' + accessLevelNode.get_id() + '%Ch=' + true.toString() + '%Delete', '');
                        AccessLevelsList_SubstituteSettings += CurrentListPart + '#';
                        break;
                    case false:
                        AccessLevelsList_SubstituteSettings = AccessLevelsList_SubstituteSettings.replace(BeforeListPart + '#', '');
                        break;
                }
            }
            else {
                if (NodeChecked == undefined) {
                    switch (checkeState) {
                        case true:
                            AccessLevelsList_SubstituteSettings += CurrentListPart + '#';
                            break;
                        case false:
                            AccessLevelsList_SubstituteSettings = AccessLevelsList_SubstituteSettings.replace(BeforeListPart + '#', '');
                            break;
                    }
                }
            }
        }
    }
    document.getElementById('hfAccessLevelsList_SubstituteSettings').value = AccessLevelsList_SubstituteSettings;
}

function CallBack_trvWorkFlowAccessLevels_SubstituteSettings_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkFlowAccessLevels').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvWorkFlowAccessLevels_SubstituteSettings();
    }
}

function CallBack_trvWorkFlowAccessLevels_SubstituteSettings_onCallbackError(sender, e) {
    ShowConnectionError_SubstituteSettings();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_SubstituteSettings) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateSubstituteSetting_SubstituteSettings();
            break;
        case 'Exit':
            SubstituteSettings_onClose();
            break;
        default:
    }
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_SubstituteSettings = confirmState;
    if (CurrentPageState_SubstituteSettings == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_SubstituteSettings').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_SubstituteSettings').value;
    DialogConfirm.Show();
}

function UpdateSubstituteSetting_SubstituteSettings() {
    ObjSubstituteSettings = new Object();
    ObjSubstituteSettings.ManagerFlowID = '0';
    ObjSubstituteSettings.SubstituteID = '0';
    ObjSubstituteSettings.AccessLevelsList = 'null';

    var ObjDialogSubstituteSettings = parent.DialogSubstituteSettings.get_value();
    ObjSubstituteSettings.SubstituteID = ObjDialogSubstituteSettings.SubstituteID;
    var SelectedItem_GridManagerWorkFlows_SubstituteSettings = GridManagerWorkFlows_SubstituteSettings.getSelectedItems();
    if (SelectedItem_GridManagerWorkFlows_SubstituteSettings.length > 0)
        ObjSubstituteSettings.ManagerFlowID = SelectedItem_GridManagerWorkFlows_SubstituteSettings[0].getMember('ID').get_text();
    if (CurrentPageState_SubstituteSettings != 'Delete')
        ObjSubstituteSettings.AccessLevelsList = document.getElementById('hfAccessLevelsList_SubstituteSettings').value;
    UpdateSubstituteSetting_SubstituteSettingsPage(CharToKeyCode_SubstituteSettings(CurrentPageState_SubstituteSettings), CharToKeyCode_SubstituteSettings(ObjSubstituteSettings.SubstituteID), CharToKeyCode_SubstituteSettings(ObjSubstituteSettings.ManagerFlowID), CharToKeyCode_SubstituteSettings(ObjSubstituteSettings.AccessLevelsList));
    DialogWaiting.Show();
}

function UpdateSubstituteSetting_SubstituteSettingsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_SubstituteSettings').value;
            Response[1] = document.getElementById('hfConnectionError_SubstituteSettings').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            SubstituteSettings_OnAfterUpdate(Response);
            CurrentPageState_SubstituteSettings = 'View';
        }
        else {
            if (CurrentPageState_SubstituteSettings == 'Delete')
                CurrentPageState_SubstituteSettings = 'View';
        }
    }
}

function SubstituteSettings_OnAfterUpdate(Response) {
    var IsFlowAssigned = Response[3] == 'false' ? false : true;
    GridManagerWorkFlows_SubstituteSettings.beginUpdate();
    GridManagerWorkFlows_SubstituteSettings.selectByKey(ObjSubstituteSettings.ManagerFlowID, 0, false);
    ManagerWorkFlowItem = GridManagerWorkFlows_SubstituteSettings.getItemFromKey(0, ObjSubstituteSettings.ManagerFlowID);
    ManagerWorkFlowItem.setValue(1, IsFlowAssigned, false);
    GridManagerWorkFlows_SubstituteSettings.endUpdate();

    if (CurrentPageState_SubstituteSettings == 'Delete') {
        trvWorkFlowAccessLevels_SubstituteSettings.beginUpdate();
        trvWorkFlowAccessLevels_SubstituteSettings.unCheckAll();
        trvWorkFlowAccessLevels_SubstituteSettings.endUpdate();
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function SetActionMode_SubstituteSettings(state) {
    document.getElementById('ActionMode_SubstituteSettings').innerHTML = document.getElementById("hf" + state + "_SubstituteSettings").value;
}

function CharToKeyCode_SubstituteSettings(str) {
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

function ShowConnectionError_SubstituteSettings() {
    var error = document.getElementById('hfErrorType_SubstituteSettings').value;
    var errorBody = document.getElementById('hfConnectionError_SubstituteSettings').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbSubstituteSettings_onClick() {
    SubstituteSettings_onClose();
    parent.document.getElementById('pgvSubstituteIntroduction_iFrame').contentWindow.ShowDialogSubstituteSettings();
}

function tlbItemHelp_TlbSubstituteSettings_onClick() {
    LoadHelpPage('tlbItemHelp_TlbSubstituteSettings');
}

function GridManagerWorkFlows_SubstituteSettings_onItemBeforeSelect(sender, e) {
    GridManagerWorkFlows_SubstituteSettings.unSelectAll();
}

