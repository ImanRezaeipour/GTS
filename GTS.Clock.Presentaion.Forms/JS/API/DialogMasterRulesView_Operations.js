
var ConfirmState_MasterRulesView = null;

function GetBoxesHeaders_MasterRulesView() {
    parent.document.getElementById('Title_DialogMasterRulesView').innerHTML = document.getElementById('hfTitle_DialogMasterRulesView').value;
    document.getElementById('header_GridRules_MasterRulesView').innerHTML = document.getElementById('hfheader_GridRules_MasterRulesView').value;
    document.getElementById('header_RuleDateRanges_MasterRulesView').innerHTML = document.getElementById('hfheader_RuleDateRanges_MasterRulesView').value;
    document.getElementById('header_RuleParameters_MasterRulesView').innerHTML = document.getElementById('hfheader_RuleParameters_MasterRulesView').value;
}

function Fill_GridRuleDateRanges_MasterRulesView() {
    document.getElementById('loadingPanel_GridRuleDateRanges_MasterRulesView').innerHTML = document.getElementById('hfloadingPanel_GridRuleDateRanges_MasterRulesView').value;
    SelectedItems_GridRuleView_MasterRulesView = document.getElementById('RulesView_iFrame').contentWindow.GetSelectedRuleItem_RulesView();
    if (SelectedItems_GridRuleView_MasterRulesView.length > 0) {
        var ruleGroupID = GetRuleGroupID_MasterRulesView();
        var ruleID = SelectedItems_GridRuleView_MasterRulesView[0].getMember('ID').get_text();
        CallBack_GridRuleDateRanges_MasterRulesView.callback(CharToKeyCode_MasterRulesView(ruleGroupID), CharToKeyCode_MasterRulesView(ruleID));
    }
}

function GetRuleGroupID_MasterRulesView() {
    var ObjDialogMasterRulesView = parent.DialogMasterRulesView.get_value();
    var RuleGroupID = ObjDialogMasterRulesView.RuleGroupID;
    return RuleGroupID;
}

function GridRuleDateRanges_MasterRulesView_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridRuleDateRanges_MasterRulesView').innerHTML = '';
}

function GridRuleDateRanges_MasterRulesView_onItemSelect(sender, e) {
    Fill_GridRuleParameters_MasterRulesView();
}

function CallBack_GridRuleDateRanges_MasterRulesView_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RuleDateRanges').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload') 
            Fill_GridRuleDateRanges_MasterRulesView();
    }
}

function CallBack_GridRuleDateRanges_MasterRulesView_onCallbackError(sender, e) {
    ShowConnectionError_MasterRulesView();
}

function Fill_GridRuleParameters_MasterRulesView() {
    document.getElementById('loadingPanel_GridRuleParameters_MasterRulesView').innerHTML = document.getElementById('hfloadingPanel_GridRuleParameters_MasterRulesView').value;
    var SelectedItems_GridRuleDateRanges_MasterRulesView = GridRuleDateRanges_MasterRulesView.getSelectedItems();
    if (SelectedItems_GridRuleDateRanges_MasterRulesView.length > 0) {
        var ruleGroupID = GetRuleGroupID_MasterRulesView();
        var ruleDataRangeID = SelectedItems_GridRuleDateRanges_MasterRulesView[0].getMember('ID').get_text();
        CallBack_GridRuleParameters_MasterRulesView.callback(CharToKeyCode_MasterRulesView(ruleGroupID), CharToKeyCode_MasterRulesView(ruleDataRangeID));
    }
}

function GridRuleParameters_MasterRulesView_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridRuleParameters_MasterRulesView').innerHTML = '';
}

function CallBack_GridRuleParameters_MasterRulesView_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RuleParameters').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload') {
           Fill_GridRuleParameters_MasterRulesView();
        }
    }
}

function CallBack_GridRuleParameters_MasterRulesView_onCallbackError(sender, e) {
    ShowConnectionError_MasterRulesView();
}

function ShowError_GridRules_RulesView_onCallBackCompleted(errorParts) {
    showDialog(errorParts[0], errorParts[1], errorParts[2]);
}

function Refresh_GridRules_MasterRulesView() {
    document.getElementById('RulesView_iFrame').contentWindow.Fill_GridRules_RulesView();
}

function Refresh_GridRuleDateRanges_MasterRulesView() {
    Fill_GridRuleDateRanges_MasterRulesView();
}

function Refresh_GridRuleParameters_MasterRulesView() {
    Fill_GridRuleParameters_MasterRulesView();
}

function tlbItemFormReconstruction_TlbMasterRulesView_onClick() {
    CloseDialogMasterRulesView();
    parent.document.getElementById('pgvRulesGroupIntroduction_iFrame').contentWindow.ShowDialogMasterRulesView();
}

function tlbItemExit_TlbMasterRulesView_onClick() {
    ShowDialogConfirm('Exit');
}

function CloseDialogMasterRulesView() {
    parent.document.getElementById('DialogMasterRulesView_IFrame').src = 'WhitePage.aspx';
    parent.DialogMasterRulesView.Close();
}

function NavigateRule_MasterRulesView(RuleGridItem) {
    document.getElementById('txtRuleSubject_MasterRulesView').value = RuleGridItem.getMember('Name').get_text();
    document.getElementById('txtRulesText_MasterRulesView').value = RuleGridItem.getMember('Script').get_text();
    Fill_GridRuleDateRanges_MasterRulesView(RuleGridItem);
    ClearItems_GridRuleParameters_MasterRulesView();
}

function ClearItems_GridRuleParameters_MasterRulesView() {
    GridRuleParameters_MasterRulesView.beginUpdate();
    GridRuleParameters_MasterRulesView.get_table().clearData();
    GridRuleParameters_MasterRulesView.endUpdate();
}

function CharToKeyCode_MasterRulesView(str) {
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

function ShowConnectionError_MasterRulesView() {
    var error = document.getElementById('hfErrorType_MasterRulesView').value;
    var errorBody = document.getElementById('hfConnectionError_MasterRulesView').value;
    showDialog(error, errorBody, 'error');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_MasterRulesView = confirmState;
    switch (confirmState) {
        case 'Exit':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_MasterRulesView').value;
            break;
    }
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_MasterRulesView) {
        case 'Exit':
            DialogConfirm.Close();
            CloseDialogMasterRulesView();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

