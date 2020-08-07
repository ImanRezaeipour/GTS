
var CurrentPageState_RulesGroup = 'View';
var ConfirmState_RulesGroup = null;
var ObjRuleGroup_RulesGroup = null;

function GetBoxesHeaders_RulesGroup() {
    document.getElementById('header_RulesGroup_RulesGroup').innerHTML = document.getElementById('hfheader_RulesGroup_RulesGroup').value;
    document.getElementById('header_RulesGroup_RulesGroup').innerHTML = document.getElementById('hfheader_RulesGroup_RulesGroup').value;
}

function ShowDialogRulesGroupUpdate(state) {
    var CurrentStateObj_RulesGroup = null;
    switch (state) {
        case 'Add':
            CurrentStateObj_RulesGroup = { "State": "" + CurrentPageState_RulesGroup + "", "RuleGroupID": "" };
            break;
        case 'Edit':
            var RuleGroupDescription_RulesGroup = '';
            if (!isNaN(trvRulesGroup_RulesGroup.get_selectedNode().get_value()))
                RuleGroupDescription_RulesGroup = trvRulesGroup_RulesGroup.get_selectedNode().get_value();
            CurrentStateObj_RulesGroup = { "State": "" + CurrentPageState_RulesGroup + "", "RuleGroupID": "" + trvRulesGroup_RulesGroup.get_selectedNode().get_id() + "", "RuleGroupName": "" + trvRulesGroup_RulesGroup.get_selectedNode().get_text() + "", "RuleGroupDescription": "" + RuleGroupDescription_RulesGroup +"" };
    }
    parent.DialogRulesGroupUpdate.set_value(CurrentStateObj_RulesGroup);
    parent.DialogRulesGroupUpdate.Show();
}

function ShowDialogMasterRulesView() {
    if (trvRulesGroup_RulesGroup.get_selectedNode() != undefined) {
        var ObjDialogMasterRulesView = new Object();
        ObjDialogMasterRulesView.RuleGroupID = trvRulesGroup_RulesGroup.get_selectedNode().get_id();
        parent.DialogMasterRulesView.set_value(ObjDialogMasterRulesView);
        parent.DialogMasterRulesView.Show();
    }
}

function trvRulesGroup_RulesGroup_onNodeMouseDoubleClick() {
    ShowDialogMasterRulesView();
}

function RuleGroup_onOperationComplete(objRuleGroup) {
    trvRulesGroup_RulesGroup.beginUpdate();
    switch (objRuleGroup.State) {
        case 'Add':
            var newRuleGroupNode = new ComponentArt.Web.UI.TreeViewNode();
            newRuleGroupNode.set_text(objRuleGroup.Name);
            newRuleGroupNode.set_value(objRuleGroup.Description);
            newRuleGroupNode.set_id(objRuleGroup.ID);
            newRuleGroupNode.set_imageUrl('Images/TreeView/folder.gif');
            trvRulesGroup_RulesGroup.get_nodes().getNode(0).get_nodes().add(newRuleGroupNode);
            trvRulesGroup_RulesGroup.selectNodeById(objRuleGroup.ID);
            break;
        case 'Edit':
            var selectedRuleGroupNode = trvRulesGroup_RulesGroup.findNodeById(objRuleGroup.ID);
            selectedRuleGroupNode.set_text(objRuleGroup.Name);
            selectedRuleGroupNode.set_value(objRuleGroup.Description);
            trvRulesGroup_RulesGroup.selectNodeById(objRuleGroup.ID);
            break;
    }
    trvRulesGroup_RulesGroup.endUpdate();
}

function Refresh_trvRulesGroup_RulesGroup() {
    Fill_trvRulesGroup_RulesGroup();
}

function SetActionMode_RulesGroup(state) {
    document.getElementById('ActionMode_RulesGroup').innerHTML = document.getElementById("hf" + state + "_RulesGroup").value;
}

function Fill_trvRulesGroup_RulesGroup() {
    document.getElementById('loadingPanel_trvRulesGroup_RulesGroup').innerHTML = document.getElementById('hfloadingPanel_trvRulesGroup_RulesGroup').value;
    CallBack_trvRulesGroup_RulesGroup.callback();
}

function tlbItemNew_TlbRulesGroup_onClick() {
    CurrentPageState_RulesGroup = 'Add';
    ShowDialogRulesGroupUpdate(CurrentPageState_RulesGroup);
}

function tlbItemEdit_TlbRulesGroup_onClick() {
    var selectedNode_trvRulesGroup_RulesGroup = trvRulesGroup_RulesGroup.get_selectedNode();
    if (selectedNode_trvRulesGroup_RulesGroup != undefined && selectedNode_trvRulesGroup_RulesGroup.get_nodes().get_length() == 0) {
        CurrentPageState_RulesGroup = 'Edit';
        ShowDialogRulesGroupUpdate(CurrentPageState_RulesGroup);
    }
}

function tlbItemDelete_TlbRulesGroup_onClick() {
    var selectedNode_trvRulesGroup_RulesGroup = trvRulesGroup_RulesGroup.get_selectedNode();
    if (selectedNode_trvRulesGroup_RulesGroup != undefined && selectedNode_trvRulesGroup_RulesGroup.get_nodes().get_length() == 0) {
        CurrentPageState_RulesGroup = 'Delete';
        ShowDialogConfirm('Delete');
    }
}

function tlbItemRulesView_TlbRulesGroup_onClick() {
    ShowDialogMasterRulesView();
}

function tlbItemExit_TlbRulesGroup_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_RulesGroup = confirmState;
    if (CurrentPageState_RulesGroup == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_RulesGroup').value;
    else        
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_RulesGroup').value;
    DialogConfirm.Show();
}

function UpdateRuleGroup_RulesGroup() {
    var selectedRuleGroupID = '0';
    if (trvRulesGroup_RulesGroup.get_selectedNode() != undefined)
        selectedRuleGroupID = trvRulesGroup_RulesGroup.get_selectedNode().get_id();
    ObjRuleGroup_RulesGroup = new Object();
    ObjRuleGroup_RulesGroup.SelectedID = selectedRuleGroupID;
    ObjRuleGroup_RulesGroup.Description = trvRulesGroup_RulesGroup.get_selectedNode().get_value();
    UpdateRuleGroup_RulesGroupPage(CharToKeyCode_RulesGroup(CurrentPageState_RulesGroup), CharToKeyCode_RulesGroup(selectedRuleGroupID));
    DialogWaiting.Show();
}

function UpdateRuleGroup_RulesGroupPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            RuleGroup_OnAfterUpdate(Response[3]);
            CurrentPageState_RulesGroup = 'View';
        }
        else {
            if (CurrentPageState_RulesGroup == 'Delete')
                CurrentPageState_RulesGroup = 'View';
        }
    }
}

function RuleGroup_OnAfterUpdate(StrObjRuleGroup) {
    var ObjRuleGroup = eval('(' + StrObjRuleGroup + ')');
    trvRulesGroup_RulesGroup.beginUpdate();
    switch (CurrentPageState_RulesGroup) {
        case 'Delete':
            trvRulesGroup_RulesGroup.findNodeById(ObjRuleGroup.ID).remove();
            break;
        case 'Copy':
            var newRuleGroupNode = new ComponentArt.Web.UI.TreeViewNode();
            newRuleGroupNode.set_text(ObjRuleGroup.Name);
            newRuleGroupNode.set_value(ObjRuleGroup_RulesGroup.Description);
            newRuleGroupNode.set_id(ObjRuleGroup.ID);
            newRuleGroupNode.set_imageUrl('Images/TreeView/folder.gif');
            trvRulesGroup_RulesGroup.get_nodes().getNode(0).get_nodes().add(newRuleGroupNode);
            trvRulesGroup_RulesGroup.selectNodeById(ObjRuleGroup.ID);
            break;
    }
    trvRulesGroup_RulesGroup.endUpdate();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_RulesGroup) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateRuleGroup_RulesGroup();
            break;
        case 'Exit':
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    CurrentPageState_RulesGroup = 'View';
}

function trvRulesGroup_RulesGroup_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvRulesGroup_RulesGroup').innerHTML = "";
}

function trvRulesGroup_RulesGroup_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RulesGroup').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvRulesGroup_RulesGroup();
    }
}

function SetWrapper_Alert_Box_RulesGroup(title, message, type) {
    SetWrapper_Alert_Box(document.RulesGroupForm.id);
    showDialog(title, message, type);
}

function CharToKeyCode_RulesGroup(str) {
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

function trvRulesGroup_RulesGroup_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvRulesGroup_RulesGroup').innerHTML = '';
    ShowConnectionError_RulesGroup();
}

function ShowConnectionError_RulesGroup() {
    var error = document.getElementById('hfErrorType_RulesGroup').value;
    var errorBody = document.getElementById('hfConnectionError_RulesGroup').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemLeaveBudget_TlbRulesGroup_onClick() {
    ShowDialogLeaveBudget();
}

function ShowDialogLeaveBudget() {
    if (trvRulesGroup_RulesGroup.get_selectedNode() != undefined) {
        var ObjDialogLeaveBudget = new Object();
        ObjDialogLeaveBudget.RuleGroupID = trvRulesGroup_RulesGroup.get_selectedNode().get_id();
        parent.DialogLeaveBudget.set_value(ObjDialogLeaveBudget);
        parent.DialogLeaveBudget.Show();
    }
}

function tlbItemFormReconstruction_TlbRulesGroup_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvRulesGroupIntroduction_iFrame').src = 'RulesGroup.aspx';
}

function tlbItemRuleGroupCopy_TlbRulesGroup_onClick() {    
    CurrentPageState_RulesGroup = 'Copy';
    UpdateRuleGroup_RulesGroup();
}

function tlbItemHelp_TlbRulesGroup_onClick() {
    LoadHelpPage('tlbItemHelp_TlbRulesGroup');
}





