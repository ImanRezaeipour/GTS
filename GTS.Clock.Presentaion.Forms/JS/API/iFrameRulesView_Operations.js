
var BaseCallBackPrefix_GridRules_RulesView = null;

function Fill_GridRules_RulesView() {
    parent.document.getElementById('loadingPanel_GridRules_MasterRulesView').innerHTML = parent.document.getElementById('hfloadingPanel_GridRules_MasterRulesView').value;
    var ObjDialogMasterRulesView = parent.parent.DialogMasterRulesView.get_value();
    var RuleGroupID = ObjDialogMasterRulesView.RuleGroupID;
    CallBack_GridRules_RulesView.callback(CharToKeyCode_RulesView(RuleGroupID));
}

function GridRules_RulesView_onBeforeCallback(sender, e) {
    SetCallBackPrefix_GridRules_RulesView();
    parent.parent.DialogLoading.Show();
}

function SetCallBackPrefix_GridRules_RulesView() {
    var ObjDialogMasterRulesView = parent.parent.DialogMasterRulesView.get_value();
    var RuleGroupID = ObjDialogMasterRulesView.RuleGroupID;
    GridRules_RulesView.CallbackPrefix = BaseCallBackPrefix_GridRules_RulesView + '&RuleGroupID=' + CharToKeyCode_RulesView(RuleGroupID);
}

function GridRules_RulesView_onCallbackComplete(sender, e) {
    parent.parent.DialogLoading.Show();
}

function GridRules_RulesView_onLoad(sender, e) {
    parent.document.getElementById('loadingPanel_GridRules_MasterRulesView').innerHTML = '';
    BaseCallBackPrefix_GridRules_RulesView = GridRules_RulesView.CallbackPrefix;
}

function GridRules_RulesView_onItemSelect(sender, e) {
    if (e.get_item().get_table().get_level() != 0)
        parent.parent.document.getElementById('DialogMasterRulesView_IFrame').contentWindow.NavigateRule_MasterRulesView(e.get_item());
}

function GridRules_RulesView_onItemExpand(sender, e) {
    GridRules_RulesView.render();
    parent.parent.DialogLoading.Close();
}

function GridRules_RulesView_onRenderComplete(sender, e) {
    parent.parent.DialogLoading.Close();
}

function CallBack_GridRules_RulesView_onCallbackComplete(sender, e) {
    parent.parent.DialogLoading.Close();
    var error = document.getElementById('ErrorHiddenField_RulesView').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        parent.parent.document.getElementById('DialogMasterRulesView_IFrame').contentWindow.ShowError_GridRules_RulesView_onCallBackCompleted(errorParts);
        if (errorParts[3] == 'Reload')
            Fill_GridRules_RulesView();
    }
}

function CallBack_GridRules_RulesView_onCallbackError(sender, e) {
    ShowConnectionError_RulesView();
}

function ShowConnectionError_RulesView() {
    var error = document.getElementById('hfErrorType_RulesView').value;
    var errorBody = document.getElementById('hfConnectionError_RulesView').value;
    showDialog(error, errorBody, 'error');
}

function CharToKeyCode_RulesView(str) {
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

function GetSelectedRuleItem_RulesView() {
    return GridRules_RulesView.getSelectedItems();
}





