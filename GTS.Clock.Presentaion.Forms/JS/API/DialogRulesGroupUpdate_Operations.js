
var CurrentPageState_RulesGroupUpdate = null;
var CurrentPageStateObjBatch_RulesGroupUpdate = null;
var RulesList_RulesGroupUpdate = "";
var ConfirmState_RulesGroupUpdate = null;
var ObjRuleGroup_RulesGroupUpdate = null;
var IsUpdateCompleted = false;

function GetBoxesHeaders_RulesGroupUpdate() {
    parent.document.getElementById('Title_DialogRulesGroupUpdate').innerHTML = document.getElementById('hfTitle_DialogRulesGroupUpdate').value;
    document.getElementById('header_RulesTemplates_RulesGroupUpdate').innerHTML = document.getElementById('hfheader_RulesTemplates_RulesGroupUpdate').value;
    document.getElementById('header_Rules_RulesGroupUpdate').innerHTML = document.getElementById('hfheader_Rules_RulesGroupUpdate').value;
}

function ShowDialogRuleParameters() {
    DialogRuleParameters.Show();
}

function tlbItemSave_TlbRulesGroupUpdate_onClick() {
    UpdateRuleGroup_RulesGroupUpdate();
}

function UpdateRuleGroup_RulesGroupUpdate() {
    ObjRuleGroup_RulesGroupUpdate = new Object();
    ObjRuleGroup_RulesGroupUpdate.Name = null;
    ObjRuleGroup_RulesGroupUpdate.Description = null;
    ObjRuleGroup_RulesGroupUpdate.ID = '0';
    ObjRuleGroup_RulesGroupUpdate.State = CurrentPageState_RulesGroupUpdate;

    ObjRuleGroup_RulesGroupUpdate.Name = document.getElementById('txtRuleGroupName_RulesGroupUpdate').value;
    ObjRuleGroup_RulesGroupUpdate.Description = document.getElementById('txtRuleGroupDescriptions_RulesGroupUpdate').value;
    if (CurrentPageState_RulesGroupUpdate != 'Add')
        ObjRuleGroup_RulesGroupUpdate.ID = CurrentPageStateObjBatch_RulesGroupUpdate.RuleGroupID;
    UpdateRuleGroup_RulesGroupUpdatePage(CharToKeyCode_RulesGroupUpdate(CurrentPageState_RulesGroupUpdate), CharToKeyCode_RulesGroupUpdate(ObjRuleGroup_RulesGroupUpdate.ID), CharToKeyCode_RulesGroupUpdate(ObjRuleGroup_RulesGroupUpdate.Name), CharToKeyCode_RulesGroupUpdate(ObjRuleGroup_RulesGroupUpdate.Description), CharToKeyCode_RulesGroupUpdate(RulesList_RulesGroupUpdate));
    DialogWaiting.Show();
}

function UpdateRuleGroup_RulesGroupUpdatePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();        
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_DialogRulesGroupUpdate').value;
            Response[1] = document.getElementById('hfConnectionError_DialogRulesGroupUpdate').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            RulesGroupUpdate_onAfterUpdate(RetMessage[2]);
            ClearList_RulesGroupUpdate();
            ChangePageState_RulesGroupUpdate();
            CurrentPageStateObjBatch_RulesGroupUpdate.RuleGroupID = ObjRuleGroup_RulesGroupUpdate.ID = Response[3];
            parent.document.getElementById('pgvRulesGroupIntroduction_iFrame').contentWindow.RuleGroup_onOperationComplete(ObjRuleGroup_RulesGroupUpdate);
        }
    }
}

function RulesGroupUpdate_onAfterUpdate(ruleGroupID) {
    CurrentPageStateObjBatch_RulesGroupUpdate.RuleGroupID = ruleGroupID;
    IsUpdateCompleted = true;
}

function ChangePageState_RulesGroupUpdate() {
    document.getElementById('ActionMode_RulesGroupUpdate').innerHTML = document.getElementById("hfView_RulesGroupUpdate").value;
    document.getElementById('txtRuleGroupName_RulesGroupUpdate').disabled = 'disabled';
    document.getElementById('txtRuleGroupDescriptions_RulesGroupUpdate').disabled = 'disabled';
    TlbRulesGroupUpdate.get_items().getItemById('tlbItemSave_TlbRulesGroupUpdate').set_enabled(false);
    TlbRulesGroupUpdate.get_items().getItemById('tlbItemSave_TlbRulesGroupUpdate').set_imageUrl('save_silver.png');
    TlbRulesGroupUpdate.get_items().getItemById('tlbItemCancel_TlbRulesGroupUpdate').set_enabled(false);
    TlbRulesGroupUpdate.get_items().getItemById('tlbItemCancel_TlbRulesGroupUpdate').set_imageUrl('cancel_silver.png');
    ChangeEnabled_tlbItems_TlbInterAction_RulesGroupUpdate(false, false);
}

function ChangeEnabled_tlbItems_TlbInterAction_RulesGroupUpdate(enabled, IsForcibleChecking) {
    var tlbItemAdd_TlbInterAction_RulesGroupUpdate_imgUrl = null;
    var tlbItemDelete_TlbInterAction_RulesGroupUpdate_imgUrl = null;
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            switch (enabled) {
                case true:
                    tlbItemAdd_TlbInterAction_RulesGroupUpdate_imgUrl = 'arrow-left.png';
                    tlbItemDelete_TlbInterAction_RulesGroupUpdate_imgUrl = 'arrow-right.png';
                    break;
                case false:
                    tlbItemAdd_TlbInterAction_RulesGroupUpdate_imgUrl = 'arrow-left_silver.png';
                    tlbItemDelete_TlbInterAction_RulesGroupUpdate_imgUrl = 'arrow-right_silver.png';
                    break;
            }
            break;
        case 'en-US':
            switch (enabled) {
                case true:
                    tlbItemAdd_TlbInterAction_RulesGroupUpdate_imgUrl = 'arrow-right.png';
                    tlbItemDelete_TlbInterAction_RulesGroupUpdate_imgUrl = 'arrow-left.png';
                    break;
                case false:
                    tlbItemAdd_TlbInterAction_RulesGroupUpdate_imgUrl = 'arrow-right_silver.png';
                    tlbItemDelete_TlbInterAction_RulesGroupUpdate_imgUrl = 'arrow-left_silver.png';
                    break;
            }
            break;
    }
    if (TlbInterAction_RulesGroupUpdate.get_items().getItemById('tlbItemAdd_TlbInterAction_RulesGroupUpdate') != null && !IsForcibleChecking) {
        TlbInterAction_RulesGroupUpdate.get_items().getItemById('tlbItemAdd_TlbInterAction_RulesGroupUpdate').set_enabled(enabled);
        TlbInterAction_RulesGroupUpdate.get_items().getItemById('tlbItemAdd_TlbInterAction_RulesGroupUpdate').set_imageUrl(tlbItemAdd_TlbInterAction_RulesGroupUpdate_imgUrl);
    }
    if (TlbInterAction_RulesGroupUpdate.get_items().getItemById('tlbItemDelete_TlbInterAction_RulesGroupUpdate') != null) {
        TlbInterAction_RulesGroupUpdate.get_items().getItemById('tlbItemDelete_TlbInterAction_RulesGroupUpdate').set_enabled(enabled);
        TlbInterAction_RulesGroupUpdate.get_items().getItemById('tlbItemDelete_TlbInterAction_RulesGroupUpdate').set_imageUrl(tlbItemDelete_TlbInterAction_RulesGroupUpdate_imgUrl);
    }
}

function ClearList_RulesGroupUpdate() {
    document.getElementById('txtRuleGroupName_RulesGroupUpdate').value = '';
    document.getElementById('txtRuleGroupDescriptions_RulesGroupUpdate').value = '';
    document.getElementById('txtRuleTemplateDescription_RulesGroupUpdate').value = '';
    document.getElementById('txtRuleDescription_RulesGroupUpdate').value = '';
    RulesList_RulesGroupUpdate = '';
}

function tlbItemCancel_TlbRulesGroupUpdate_onClick() {
    parent.DialogRulesGroupUpdate.Close();
}

function tlbItemRuleParameteresRegulation_TlbRulesGroupUpdate_onClick() {
    var selectedNode_trvRules = trvRules_RulesGroupUpdate.get_selectedNode();
    if (selectedNode_trvRules != undefined && eval('(' + selectedNode_trvRules.get_value() + ')').HasParameter) {
        var ruleID = selectedNode_trvRules.get_id();
        var ObjDialogRuleParametersValue = new Object();
        ObjDialogRuleParametersValue.State = parent.DialogRulesGroupUpdate.get_value().State;
        ObjDialogRuleParametersValue.RuleGroupID = CurrentPageStateObjBatch_RulesGroupUpdate.RuleGroupID;
        ObjDialogRuleParametersValue.RuleID = ruleID;
        ObjDialogRuleParametersValue.RuleTitle = selectedNode_trvRules.get_text();
        DialogRuleParameters.set_value(ObjDialogRuleParametersValue);
        ShowDialogRuleParameters();
    }
}

function tlbItemExit_TlbRulesGroupUpdate_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_RulesGroupUpdate = confirmState;
    if (CurrentPageState_RulesGroupUpdate == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_RulesGroupUpdate').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_RulesGroupUpdate').value;
    DialogConfirm.Show();
}


function Refresh_trvRulesTemplates_RulesGroupUpdate() {
    Fill_trvRulesTemplates_RulesGroupUpdate();
}

function Refresh_trvRules_RulesGroupUpdate() {
    RulesList_RulesGroupUpdate = "";
    Fill_trvRules_RulesGroupUpdate();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_RulesGroupUpdate) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateRuleGroup_RulesGroupUpdate();
            break;
        case 'Exit':
            ClearList_RulesGroupUpdate();
            CloseDialogRulesGroupUpdate();
            break;
    }
}

function CloseDialogRulesGroupUpdate() {
    parent.document.getElementById('DialogRulesGroupUpdate_IFrame').src = "WhitePage.aspx";
    parent.DialogRulesGroupUpdate.Close();   
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function SetActionMode_RulesGroupUpdate() {
    CurrentPageStateObjBatch_RulesGroupUpdate = parent.DialogRulesGroupUpdate.get_value();
    if (CurrentPageStateObjBatch_RulesGroupUpdate != undefined && CurrentPageStateObjBatch_RulesGroupUpdate != null) {
        CurrentPageState_RulesGroupUpdate = CurrentPageStateObjBatch_RulesGroupUpdate.State;
        document.getElementById('ActionMode_RulesGroupUpdate').innerHTML = document.getElementById("hf" + CurrentPageState_RulesGroupUpdate + "_RulesGroupUpdate").value;
        if (CurrentPageState_RulesGroupUpdate == 'Edit')
            NavigateRuleGroup_RuleGroupUpdate();
    }
}

function NavigateRuleGroup_RuleGroupUpdate() {
    document.getElementById('txtRuleGroupName_RulesGroupUpdate').value = CurrentPageStateObjBatch_RulesGroupUpdate.RuleGroupName;
    document.getElementById('txtRuleGroupDescriptions_RulesGroupUpdate').value = CurrentPageStateObjBatch_RulesGroupUpdate.RuleGroupDescription;
}

function Fill_trvRulesTemplates_RulesGroupUpdate() {
    document.getElementById('loadingPanel_trvRulesTemplates_RulesGroupUpdate').innerHTML = document.getElementById('hfloadingPanel_trvRulesTemplates_RulesGroupUpdate').value;
    CallBack_trvRulesTemplates_RulesGroupUpdate.callback();
}

function Fill_trvRules_RulesGroupUpdate() {
    document.getElementById('loadingPanel_trvRules_RulesGroupUpdate').innerHTML = document.getElementById('hfloadingPanel_trvRules_RulesGroupUpdate').value;
    if(CurrentPageState_RulesGroupUpdate == 'Add')
        CallBack_trvRules_RulesGroupUpdate.callback(CharToKeyCode_RulesGroupUpdate(CurrentPageState_RulesGroupUpdate));
    else
        CallBack_trvRules_RulesGroupUpdate.callback(CharToKeyCode_RulesGroupUpdate(CurrentPageState_RulesGroupUpdate), CharToKeyCode_RulesGroupUpdate(CurrentPageStateObjBatch_RulesGroupUpdate.RuleGroupID));
}

function trvRulesTemplates_RulesGroupUpdate_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvRulesTemplates_RulesGroupUpdate').innerHTML = '';
}

function trvRules_RulesGroupUpdate_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvRules_RulesGroupUpdate').innerHTML = '';
}

function CallBack_trvRulesTemplates_RulesGroupUpdate_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RulesTemplates_RulesGroupUpdate').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvRulesTemplates_RulesGroupUpdate();
    }
}

function CallBack_trvRules_RulesGroupUpdate_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Rules_RulesGroupUpdate').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvRules_RulesGroupUpdate();
    }
}

function CharToKeyCode_RulesGroupUpdate(str) {
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

function contextMenu_trvRulesTemplates_RulesGroupUpdate_onItemSelect(sender, e) {
    var RuleTemplateItem_RulesGroupUpdate = e.get_item();
    var RuleTemplateNode_RulesGroupUpdate = RuleTemplateItem_RulesGroupUpdate.get_parentMenu().get_contextData();
    trvRulesTemplates_RulesGroupUpdate.selectNodeById(RuleTemplateNode_RulesGroupUpdate.get_id());
    InsertRule_RulesGroupUpdate();
}

function trvRulesTemplates_RulesGroupUpdate_onContextMenu(sender, e) {
    if (TlbInterAction_RulesGroupUpdate.get_items().getItemById('tlbItemAdd_TlbInterAction_RulesGroupUpdate') != null && TlbInterAction_RulesGroupUpdate.get_items().getItemById('tlbItemAdd_TlbInterAction_RulesGroupUpdate').get_enabled() && trvRulesTemplates_RulesGroupUpdate.get_selectedNode() != undefined && trvRulesTemplates_RulesGroupUpdate.get_selectedNode().get_id() == e.get_node().get_id())
        contextMenu_trvRulesTemplates_RulesGroupUpdate.showContextMenuAtEvent(e.get_event(), e.get_node());
}

function contextMenu_trvRules_RulesGroupUpdate_onItemSelect(sender, e) {
    var RuleItem_RulesGroupUpdate = e.get_item();
    var RuleNode_RulesGroupUpdate = RuleItem_RulesGroupUpdate.get_parentMenu().get_contextData();
    trvRules_RulesGroupUpdate.selectNodeById(RuleNode_RulesGroupUpdate.get_id());
    RemoveRule_RulesGroupUpdate();
}

function trvRules_RulesGroupUpdate_onContextMenu(sender, e) {
    if (TlbInterAction_RulesGroupUpdate.get_items().getItemById('tlbItemDelete_TlbInterAction_RulesGroupUpdate') != null && TlbInterAction_RulesGroupUpdate.get_items().getItemById('tlbItemDelete_TlbInterAction_RulesGroupUpdate').get_enabled() && trvRules_RulesGroupUpdate.get_selectedNode() != undefined && trvRules_RulesGroupUpdate.get_selectedNode().get_id() == e.get_node().get_id())    
        contextMenu_trvRules_RulesGroupUpdate.showContextMenuAtEvent(e.get_event(), e.get_node()); 
}

function tlbItemAdd_TlbInterAction_RulesGroupUpdate_onClick() {
   InsertRule_RulesGroupUpdate();
}

function InsertRule_RulesGroupUpdate()
{
    var selectedNode_trvRulesTemplates_RulesGroupUpdate = trvRulesTemplates_RulesGroupUpdate.get_selectedNode();
    if (selectedNode_trvRulesTemplates_RulesGroupUpdate != undefined && selectedNode_trvRulesTemplates_RulesGroupUpdate.get_nodes().get_length() == 0) {
        if (trvRules_RulesGroupUpdate.findNodeById(selectedNode_trvRulesTemplates_RulesGroupUpdate.get_id()) == undefined || trvRules_RulesGroupUpdate.findNodeById(selectedNode_trvRulesTemplates_RulesGroupUpdate.get_id()).get_value() == 'removed') {
            if (RulesList_RulesGroupUpdate.indexOf('Delete%' + selectedNode_trvRulesTemplates_RulesGroupUpdate.get_id() + '#') >= 0) 
                RulesList_RulesGroupUpdate = RulesList_RulesGroupUpdate.replace('Delete%' + selectedNode_trvRulesTemplates_RulesGroupUpdate.get_id() + '#', '');
            if (trvRules_RulesGroupUpdate.findNodeById(selectedNode_trvRulesTemplates_RulesGroupUpdate.get_parentNode().get_id()).get_nodes().getNodeById(selectedNode_trvRulesTemplates_RulesGroupUpdate.get_id()) == null) {
                var RuleMember_RulesGroupUpdate = 'Add%' + selectedNode_trvRulesTemplates_RulesGroupUpdate.get_id() + '#';
                RulesList_RulesGroupUpdate += RuleMember_RulesGroupUpdate;

                trvRules_RulesGroupUpdate.beginUpdate();
                var newRuleNode = new ComponentArt.Web.UI.TreeViewNode();
                newRuleNode.set_text(selectedNode_trvRulesTemplates_RulesGroupUpdate.get_text());
                var ruleTemplateNodeValue = selectedNode_trvRulesTemplates_RulesGroupUpdate.get_value();
                newRuleNode.set_value(ruleTemplateNodeValue);
                newRuleNode.set_id(selectedNode_trvRulesTemplates_RulesGroupUpdate.get_id());
                if (eval('(' + ruleTemplateNodeValue + ')').HasParameter)
                    newRuleNode.set_imageUrl('Images/TreeView/folder_blue.gif');
                else
                    newRuleNode.set_imageUrl('Images/TreeView/folder.gif');
                var ParentNode_trvRules_RulesGroupUpdate = trvRules_RulesGroupUpdate.findNodeById(selectedNode_trvRulesTemplates_RulesGroupUpdate.get_parentNode().get_id());
                ParentNode_trvRules_RulesGroupUpdate.get_nodes().add(newRuleNode);
                trvRules_RulesGroupUpdate.endUpdate();
                ParentNode_trvRules_RulesGroupUpdate.expand();
            }
        }
    }
}

function tlbItemDelete_TlbInterAction_RulesGroupUpdate_onClick() {
    RemoveRule_RulesGroupUpdate();
}

function RemoveRule_RulesGroupUpdate() {
    var selectedNode_trvRules_RulesGroupUpdate = trvRules_RulesGroupUpdate.get_selectedNode();
    if (selectedNode_trvRules_RulesGroupUpdate != undefined && selectedNode_trvRules_RulesGroupUpdate.get_nodes().get_length() == 0 && !eval('(' + selectedNode_trvRules_RulesGroupUpdate.get_value() + ')').IsForcible) {
        var RuleMember_RulesGroupUpdate = 'Add%' + selectedNode_trvRules_RulesGroupUpdate.get_id() + '#';
        if (RulesList_RulesGroupUpdate.indexOf(RuleMember_RulesGroupUpdate) >= 0) {
            RulesList_RulesGroupUpdate = RulesList_RulesGroupUpdate.replace(RuleMember_RulesGroupUpdate, '');
        }
        else {
            RuleMember_RulesGroupUpdate = 'Delete%' + selectedNode_trvRules_RulesGroupUpdate.get_id() + '#';
            RulesList_RulesGroupUpdate += RuleMember_RulesGroupUpdate;
        }
        trvRules_RulesGroupUpdate.beginUpdate();
        selectedNode_trvRules_RulesGroupUpdate.set_value('removed');
        selectedNode_trvRules_RulesGroupUpdate.remove();
        trvRules_RulesGroupUpdate.endUpdate();
    }
}

function trvRulesTemplates_RulesGroupUpdate_onNodeSelect(sender, e) {
    if (e.get_node().get_nodes().get_length() == 0) {
        ruleTemplateNodeValue = eval('(' + e.get_node().get_value() + ')');
        if(ruleTemplateNodeValue != undefined)
            document.getElementById('txtRuleTemplateDescription_RulesGroupUpdate').value = ruleTemplateNodeValue.Script;
    }
}

function trvRules_RulesGroupUpdate_onNodeSelect(sender, e) {
    if (e.get_node().get_nodes().get_length() == 0) {
        var ruleNodeValue = eval('(' + e.get_node().get_value() + ')');
        var HasParameters = false;
        var IsForcible = false;
        if (ruleNodeValue != undefined) {
            document.getElementById('txtRuleDescription_RulesGroupUpdate').value = ruleNodeValue.Script;
            HasParameters = ruleNodeValue.HasParameter;
            IsForcible = ruleNodeValue.IsForcible;
        }
        ChnageEnabled_tlbItemRuleParameteresRegulation_TlbRulesGroupUpdate(e.get_node().get_id(), HasParameters);

        if (IsForcible)
            ChangeEnabled_tlbItems_TlbInterAction_RulesGroupUpdate(false, true);
        else
            if (!IsUpdateCompleted)
                ChangeEnabled_tlbItems_TlbInterAction_RulesGroupUpdate(true, true);
            else
                ChangeEnabled_tlbItems_TlbInterAction_RulesGroupUpdate(false, true);
    }
}

function ChnageEnabled_tlbItemRuleParameteresRegulation_TlbRulesGroupUpdate(ruleNodeID, HasParameter) {
    if (HasParameter && RulesList_RulesGroupUpdate.indexOf('Add%' + ruleNodeID) == -1) {
        if (TlbRulesGroupUpdate.get_items().getItemById('tlbItemRuleParameteresRegulation_TlbRulesGroupUpdate') != null) {
            TlbRulesGroupUpdate.get_items().getItemById('tlbItemRuleParameteresRegulation_TlbRulesGroupUpdate').set_enabled(true);
            TlbRulesGroupUpdate.get_items().getItemById('tlbItemRuleParameteresRegulation_TlbRulesGroupUpdate').set_imageUrl('regulation.png');
        }
    }
    else {
        if (TlbRulesGroupUpdate.get_items().getItemById('tlbItemRuleParameteresRegulation_TlbRulesGroupUpdate') != null) {
            TlbRulesGroupUpdate.get_items().getItemById('tlbItemRuleParameteresRegulation_TlbRulesGroupUpdate').set_enabled(false);
            TlbRulesGroupUpdate.get_items().getItemById('tlbItemRuleParameteresRegulation_TlbRulesGroupUpdate').set_imageUrl('regulation_silver.png');
        }
    }
}

function CallBack_trvRulesTemplates_RulesGroupUpdate_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvRulesTemplates_RulesGroupUpdate').innerHTML = '';
    ShowConnectionError_RulesGroupUpdate();
}

function CallBack_trvRules_RulesGroupUpdate_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvRules_RulesGroupUpdate').innerHTML = '';
    ShowConnectionError_RulesGroupUpdate();
}

function ShowConnectionError_RulesGroupUpdate() {
    var error = document.getElementById('hfErrorType_DialogRulesGroupUpdate').value;
    var errorBody = document.getElementById('hfConnectionError_DialogRulesGroupUpdate').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbRulesGroupUpdate_onClick() {
    var ObjDialogRulesGroupUpdate = parent.DialogRulesGroupUpdate.get_value();
    CloseDialogRulesGroupUpdate();
    parent.document.getElementById('pgvRulesGroupIntroduction_iFrame').contentWindow.ShowDialogRulesGroupUpdate(ObjDialogRulesGroupUpdate.State);
}

function tlbItemHelp_TlbRulesGroupUpdate_onClick() {
    LoadHelpPage('tlbItemHelp_TlbRulesGroupUpdate');
}

function tlbItemRuleGroupRulesParametersValidation_TlbRulesGroupUpdate_onClick() {
    ValidateRuleGroupRuleParameters_RulesGroupUpdate();
}

function ValidateRuleGroupRuleParameters_RulesGroupUpdate(){
    var ObjDialogRulesGroupUpdate = parent.DialogRulesGroupUpdate.get_value();
    var RuleGroupID = ObjDialogRulesGroupUpdate.RuleGroupID;
    var State = ObjDialogRulesGroupUpdate.State;
    ValidateRuleGroupRuleParameters_RulesGroupUpdatePage(CharToKeyCode_RulesGroupUpdate(State), CharToKeyCode_RulesGroupUpdate(RuleGroupID));
    DialogWaiting.Show();
}

function ValidateRuleGroupRuleParameters_RulesGroupUpdatePage_onCallBack(Response) { 
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_DialogRulesGroupUpdate').value;
            Response[1] = document.getElementById('hfConnectionError_DialogRulesGroupUpdate').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}












