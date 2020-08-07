
/* -------------------------------------------------------------------------- */
/* ---------------------------------- Rule ---------------------------------- */
/* -------------------------------------------------------------------------- */


/* Variables */
var CurrentPageIndex_GridRules_Rules = 0;
var RuleSearchBoxIsShownRule_Rules = false;
var LoadStateRule_Rules = 'Normal';
var CombosCallBackCurrentStateRule_Rules = new Object();
var CurrentPageStateRule_Rules = 'View';
var SelectedRules_Rule = new Object();
var ConfirmStateRule_Rules = null;
/* /Variables */

/*  UUU  */

function ChangePageStateRule_Rules(State) {
    CurrentPageStateRule_Rules = State;
    SetActionModeRule_Rules(State);

    if (State == 'Add' || State == 'Edit' || State == 'Delete') {
        if (TlbRules.get_items().getItemById('tlbItemNew_TlbRules') != null) {
            TlbRules.get_items().getItemById('tlbItemNew_TlbRules').set_enabled(false);
            TlbRules.get_items().getItemById('tlbItemNew_TlbRules').set_imageUrl('add_silver.png');
        }
        if (TlbRules.get_items().getItemById('tlbItemEdit_TlbRules') != null) {
            TlbRules.get_items().getItemById('tlbItemEdit_TlbRules').set_enabled(false);
            TlbRules.get_items().getItemById('tlbItemEdit_TlbRules').set_imageUrl('edit_silver.png');
        }
        if (TlbRules.get_items().getItemById('tlbItemDelete_TlbRules') != null) {
            TlbRules.get_items().getItemById('tlbItemDelete_TlbRules').set_enabled(false);
            TlbRules.get_items().getItemById('tlbItemDelete_TlbRules').set_imageUrl('remove_silver.png');
        }
        TlbRules.get_items().getItemById('tlbItemSave_TlbRules').set_enabled(true);
        TlbRules.get_items().getItemById('tlbItemSave_TlbRules').set_imageUrl('save.png');
        TlbRules.get_items().getItemById('tlbItemCancel_TlbTlbRules').set_enabled(true);
        TlbRules.get_items().getItemById('tlbItemCancel_TlbTlbRules').set_imageUrl('cancel.png');
        TlbRules.get_items().getItemById('tlbItemDefine_TlbTlbRules').set_enabled(true);
        TlbRules.get_items().getItemById('tlbItemDefine_TlbTlbRules').set_imageUrl('view_detailed.png');

        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemRefresh_TlbPaging_GridRules_Rules').set_enabled(false);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemRefresh_TlbPaging_GridRules_Rules').set_imageUrl('refresh_silver.png');
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemLast_TlbPaging_GridRules_Rules').set_enabled(false);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemLast_TlbPaging_GridRules_Rules').set_imageUrl('last_silver.png');
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemNext_TlbPaging_GridRules_Rules').set_enabled(false);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemNext_TlbPaging_GridRules_Rules').set_imageUrl('Next_silver.png');
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemBefore_TlbPaging_GridRules_Rules').set_enabled(false);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemBefore_TlbPaging_GridRules_Rules').set_imageUrl('Before_silver.png');
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemFirst_TlbPaging_GridRules_Rules').set_enabled(false);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemFirst_TlbPaging_GridRules_Rules').set_imageUrl('first_silver.png');
        if (tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbRuleQuickSearch') != null) {
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbRuleQuickSearch').set_enabled(false);
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbRuleQuickSearch').set_imageUrl('search_silver.png');
        }
        document.getElementById('txtSerchTerm_Rule').disabled = true;
        document.getElementById('txtRuleName').disabled = false;
        document.getElementById('txtRuleCode').disabled = false;

        cmbRuleType_Rules.enable();

        if (State == 'Edit')
            NavigateRule_Rules(GridRules_Rules.getSelectedItems()[0]);
        if (State == 'Delete')
            onSaveRule_Rules();
    }

    if (State == 'View') {
        if (TlbRules.get_items().getItemById('tlbItemNew_TlbRules') != null) {
            TlbRules.get_items().getItemById('tlbItemNew_TlbRules').set_enabled(true);
            TlbRules.get_items().getItemById('tlbItemNew_TlbRules').set_imageUrl('add.png');
        }
        if (TlbRules.get_items().getItemById('tlbItemEdit_TlbRules') != null) {
            TlbRules.get_items().getItemById('tlbItemEdit_TlbRules').set_enabled(true);
            TlbRules.get_items().getItemById('tlbItemEdit_TlbRules').set_imageUrl('edit.png');
        }
        if (TlbRules.get_items().getItemById('tlbItemDelete_TlbRules') != null) {
            TlbRules.get_items().getItemById('tlbItemDelete_TlbRules').set_enabled(true);
            TlbRules.get_items().getItemById('tlbItemDelete_TlbRules').set_imageUrl('remove.png');
        }
        TlbRules.get_items().getItemById('tlbItemSave_TlbRules').set_enabled(false);
        TlbRules.get_items().getItemById('tlbItemSave_TlbRules').set_imageUrl('save_silver.png');
        TlbRules.get_items().getItemById('tlbItemCancel_TlbTlbRules').set_enabled(false);
        TlbRules.get_items().getItemById('tlbItemCancel_TlbTlbRules').set_imageUrl('cancel_silver.png');
        TlbRules.get_items().getItemById('tlbItemDefine_TlbTlbRules').set_enabled(false);
        TlbRules.get_items().getItemById('tlbItemDefine_TlbTlbRules').set_imageUrl('view_detailed_silver.png');

        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemRefresh_TlbPaging_GridRules_Rules').set_enabled(true);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemRefresh_TlbPaging_GridRules_Rules').set_imageUrl('refresh.png');
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemLast_TlbPaging_GridRules_Rules').set_enabled(true);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemLast_TlbPaging_GridRules_Rules').set_imageUrl("last.png");
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemNext_TlbPaging_GridRules_Rules').set_enabled(true);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemNext_TlbPaging_GridRules_Rules').set_imageUrl("Next.png");
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemBefore_TlbPaging_GridRules_Rules').set_enabled(true);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemBefore_TlbPaging_GridRules_Rules').set_imageUrl("Before.png");
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemFirst_TlbPaging_GridRules_Rules').set_enabled(true);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemFirst_TlbPaging_GridRules_Rules').set_imageUrl("first.png");
        if (tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbRuleQuickSearch') != null) {
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbRuleQuickSearch').set_enabled(true);
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbRuleQuickSearch').set_imageUrl('search.png');
        }

        document.getElementById('txtSerchTerm_Rule').disabled = false;
        document.getElementById('txtRuleName').disabled = true;
        document.getElementById('txtRuleCode').disabled = true;
        cmbRuleType_Rules.disable();

    }

}

function GetBoxesHeaders_Rules() {
    document.getElementById('header_RulesBox_Rules').innerHTML = document.getElementById('hfHeaderRule_Rules').value;
    document.getElementById('header_RuleParameters_Rule').innerHTML = document.getElementById('hfHeaderRuleParameter_Rules').value;
}

/* /UUU  */

/* GridRules Field */
function SetPageIndex_GridRules_Rules(pageIndex) {
    CurrentPageIndex_GridRules_Rules = pageIndex;
    Fill_GridRules_Rules(pageIndex);
}
function Fill_GridRules_Rules(pageIndex) {
    document.getElementById('loadingPanel_GridRules_Rules').innerHTML = document.getElementById('hfloadingPanel_GridRules_Rules').value;
    var pageSize = parseInt(document.getElementById('hfRulesPageSize_Rules').value);
    var searchKey = 'NotSpecified';
    var searchTerm = '';
    if (RuleSearchBoxIsShownRule_Rules) {
        searchTerm = document.getElementById('txtSerchTerm_Rule').value;
    }
    CallBack_GridRules_Rule.callback(CharToKeyCode_Rules(LoadStateRule_Rules), CharToKeyCode_Rules(pageSize.toString()), CharToKeyCode_Rules(pageIndex.toString()), CharToKeyCode_Rules(searchKey), CharToKeyCode_Rules(searchTerm));
}
function tlbItemSearch_TlbRuleQuickSearch_onClick(sender, args) {
    RuleSearchBoxIsShownRule_Rules = true;
    LoadStateRule_Rules = 'Search';
    SetPageIndex_GridRules_Rules(0);
}

function GridRules_Rules_onLoad(sender, args) {
    document.getElementById('loadingPanel_GridRules_Rules').innerHTML = '';
}
function GridRules_Rules_onItemSelect(sender, args) {
    if (CurrentPageStateRule_Rules != 'Add')
        NavigateRule_Rules(args.get_item());
}
function CallBack_GridRules_Rule_OnCallbackComplete(sender, args) {
    var test = 'test';
}
function CallBack_GridRules_Rule_onCallbackError(sender, args) {
    var test = 'test';
}
/* /GridRules Field */

/* Grid Toolbar Button Events */
function tlbRuleRefresh_TlbPaging_GridRules_Rules_onClick(sender, args) {
    ChangeLoadState_GridRules_Rules('Normal');
}
function ChangeLoadState_GridRules_Rules(state) {
    LoadStateRule_Rules = state;
    SetPageIndex_GridRules_Rules(0);
}
function tlbRuleFirst_TlbPaging_GridRules_Rules_onClick(sender, args) {
    SetPageIndex_GridRules_Rules(0);
}
function tlbRuleBefore_TlbPaging_GridRules_Rules_onClick(sender, args) {
    if (CurrentPageIndex_GridRules_Rules != 0) {
        CurrentPageIndex_GridRules_Rules = CurrentPageIndex_GridRules_Rules - 1;
        SetPageIndex_GridRules_Rules(CurrentPageIndex_GridRules_Rules);
    }
}
function tlbRuleNext_TlbPaging_GridRules_Rules_onClick(sender, args) {
    if (CurrentPageIndex_GridRules_Rules < parseInt(document.getElementById('hfRulesPageCount_Rules').value) - 1) {
        CurrentPageIndex_GridRules_Rules = CurrentPageIndex_GridRules_Rules + 1;
        SetPageIndex_GridRules_Rules(CurrentPageIndex_GridRules_Rules);
    }
}
function tlbRuleLast_TlbPaging_GridRules_Rules_onClick(sender, args) {
    SetPageIndex_GridRules_Rules(parseInt(document.getElementById('hfRulesPageCount_Rules').value) - 1);
}
/* /Grid Toolbar Button Events */

/* Combobox RuleType */
function cmbRuleType_Rules_onExpand(sender, args) {
    if (cmbRuleType_Rules.get_itemCount() == 0 && CombosCallBackCurrentStateRule_Rules.IsExpandOccured_cmbRuleType_Rules == undefined) {
        CombosCallBackCurrentStateRule_Rules.cmbRuleType_Rules_Text = document.getElementById('cmbRuleType_Rules_Input').value;
        CombosCallBackCurrentStateRule_Rules.IsExpandOccured_cmbRuleType_Rules = true;
        CallBack_cmbRuleType_Rules.callback();
    } else {
        document.getElementById('cmbRuleType_Rules_Input').value = CombosCallBackCurrentStateRule_Rules.cmbRuleType_Rules_Text;
    }
}
function cmbRuleType_Rules_onBeforeCallback(sender, args) {
    cmbRuleType_Rules.dispose();
}
function cmbRuleType_Rules_onCallbackComplete(sender, args) {
    var error = document.getElementById('ErrorHiddenField_TypeFields').value;
    if (error == "") {
        document.getElementById('cmbRuleType_Rules_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompletedRule_Rules())
            $('#cmbRuleType_Rules_DropImage').mousedown();
        cmbRuleType_Rules.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbRuleType_Rules_DropDown').style.display = 'none';
    }
}
function cmbRuleType_Rules_onCallbackError(sender, args) {
    document.getElementById('loadingPanel_GridRules_Rules').innerHTML = '';
    ShowConnectionError_Rules();
}
/* /Combobox RuleType */


/* SelectedRules_Rule and it's functions */

function ClearControlsRule_Rules() {

    SelectedRules_Rule.Script = '';
    SelectedRules_Rule.CSharpCode = '';

    SelectedRules_Rule.CustomCategoryCode = '';
    SelectedRules_Rule.JsonObject = '';

    document.getElementById('txtRuleCode').value = '';
    document.getElementById('txtRuleName').value = '';


    document.getElementById('cmbRuleType_Rules_Input').value = '';
    if (cmbRuleType_Rules.getSelectedItem() != undefined)
        cmbRuleType_Rules.unSelect();

}

function NavigateRule_Rules(selectedRule) {
    if (selectedRule == undefined) {
        return;
    }

    /*
    var typeTitle = selectedRule.getMember('TypeTitle').get_text();
    var periodicTypeTitle = selectedRule.getMember('PeriodicTypeTitle').get_text();
    var calcSituationTypeTitle = selectedRule.getMember('CalcSituationTypeTitle').get_text();
    var persistSituationTypeTitle = selectedRule.getMember('PersistSituationTypeTitle').get_text();
    */
    RefreshRule_Rules();

    GridRuleParametersCallCallBack_Rules(selectedRule.getMember('ID').get_text());

    FillSelectedRule_Rules(
        selectedRule.getMember('ID').get_text(),
        selectedRule.getMember('IdentifierCode').get_text(),
        selectedRule.getMember('Name').get_text(),
        selectedRule.getMember('TypeId').get_text(),
        selectedRule.getMember('Type').get_text(),
        selectedRule.getMember('UserDefined').get_text(),
        selectedRule.getMember('Script').get_text(),
        selectedRule.getMember('CSharpCode').get_text(),
        selectedRule.getMember('JsonObject').get_text(),
        selectedRule.getMember('CustomCategoryCode').get_text()
    );

    FillSelectedFieldsRule_Rules();
}

function onSaveRule_Rules() {
    if (CurrentPageStateRule_Rules != 'Delete')
        UpdateRule_Rules();
    else
        ShowDialogConfirm('Delete', 'Rule');
}
function OnCancelRule_Rules() {
    ChangePageStateRule_Rules('View');
    ClearControlsRule_Rules();
}

function RefreshRule_Rules() {
    FillSelectedRule_Rules(0, 0, null, 0, null, null, null, null, null);
}
function FillSelectedRule_Rules(id, identifierCode, name, typeId, type, userDefined, script, cSharpCode, jsonObject, customCategoryCode) {
    SelectedRules_Rule = new Object();
    SelectedRules_Rule.ID = id;
    SelectedRules_Rule.IdentifierCode = identifierCode;
    SelectedRules_Rule.Name = name;

    SelectedRules_Rule.TypeId = typeId;
    SelectedRules_Rule.Type = type;

    SelectedRules_Rule.UserDefined = userDefined;

    if (jsonObject != undefined && jsonObject != "") {
        SelectedRules_Rule.Script = script;
        SelectedRules_Rule.CSharpCode = cSharpCode;

        SelectedRules_Rule.CustomCategoryCode = customCategoryCode;
        SelectedRules_Rule.JsonObject = JSON.parse(jsonObject);
    }

}
function FillSelectedFieldsRule_Rules() {
    document.getElementById('txtRuleCode').value = SelectedRules_Rule.IdentifierCode;
    document.getElementById('txtRuleName').value = SelectedRules_Rule.Name;
    document.getElementById('cmbRuleType_Rules_Input').value = SelectedRules_Rule.Type;
}
/* /SelectedRules_Rule and it's functions */

function SetActionModeRule_Rules(state) {
    document.getElementById('ActionMode_Rules').innerHTML = document.getElementById("hfState" + state + "_Rules").value;
}

/* Utilities */

function CharToKeyCode_Rules(str) {
    if (str == null) return '';

    str = str.toString();

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
function ClearListRule_Rules() {
    if (CurrentPageStateRule_Rules != 'Edit') {
        RefreshRule_Rules();
    }
}
function CheckNavigator_onCmbCallBackCompletedRule_Rules() {
    if (navigator.userAgent.indexOf('Safari') != 1 || navigator.userAgent.indexOf('Chrome') != 1)
        return true;
    return false;
}

function CloseDialogRulesManagement() {
    parent.document.getElementById('DialogRulesManagement_IFrame').src = 'WhitePage.aspx';
    parent.DialogRulesManagement.Close();
}
function ShowDialogConfirm(confirmState, objectType) {

    if (objectType == 'Rule') {
        ConfirmStateRule_Rules = confirmState;
        if (CurrentPageStateRule_Rules == 'Delete')
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessageRule_Rules').value;
        else
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Rules').value;
    }
    if (objectType == 'RuleParameter') {
        ConfirmStateRuleParameter_Rules = confirmState;
        if (ConfirmStateRuleParameter_Rules == 'Delete')
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessageRuleParam_Rules').value;
        else
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Rules').value;
    }

    DialogConfirm.set_value(objectType);
    DialogConfirm.Show();
}
/* /Utilities */

/* Error */
function ShowConnectionError_Rules() {
    var error = document.getElementById('hfErrorType_Rules').value;
    var errorBody = document.getElementById('hfConnectionError_Rules').value;
    showDialog(error, errorBody, 'error');
}
/* /Error */

/* Toolbar Item Clicks */
function tlbRuleNew_TlbRules_onClick() {
    ClearControlsRule_Rules();
    ChangePageStateRule_Rules('Add');
}
function tlbRuleEdit_TlbRules_onClick(sender, args) {
    ChangePageStateRule_Rules('Edit');
}
function tlbRuleDelete_TlbRules_onClick(sender, args) {
    ChangePageStateRule_Rules('Delete');
}
//function tlbRuleHelp_TlbRules_onClick(sender, args) {

//}
function tlbRuleSave_TlbRules_onClick(sender, args) {
    onSaveRule_Rules();
}
function tlbRuleCancel_TlbTlbRules_onClick(sender, args) {
    OnCancelRule_Rules();
}
function tlbRuleDefine_TlbTlbRules_onClick(sender, args) {

    var RuleJsonObjectEditor = new Object();
    RuleJsonObjectEditor.ID = SelectedRules_Rule.ID;
    RuleJsonObjectEditor.DetailsJsonObject = SelectedRules_Rule.JsonObject;
    RuleJsonObjectEditor.ScriptEn = SelectedRules_Rule.CSharpCode;
    RuleJsonObjectEditor.ScriptFa = SelectedRules_Rule.Script;

    RuleJsonObjectEditor.CallerDialog = "RuleManagement";

    parent.DialogConceptRuleEditor.set_value(RuleJsonObjectEditor);
    parent.DialogConceptRuleEditor.Show();
}
function tlbItemFormReconstruction_TlbRule_onClick(sender, args) {
    CloseDialogRulesManagement();
    parent.DialogRulesManagement.Show();
}
function tlbRuleExit_TlbRules_onClick(sender, args) {
    ShowDialogConfirm('Exit');
}

function Apply_Object_CSharp_Script_FromRuleRuleEditor(recivedObject) {

    SelectedRules_Rule.Script = recivedObject.ScriptFa;
    SelectedRules_Rule.CSharpCode = recivedObject.ScriptEn;
    SelectedRules_Rule.JsonObject = recivedObject.DetailsJsonObject;
}

/* /Toolbar Item Clicks */


/* Rule Ajax functions */
function UpdateRule_Rules() {

    ObjRule_Rules = new Object();

    ObjRule_Rules.ID = 0;
    ObjRule_Rules.IdentifierCode = "";
    ObjRule_Rules.Name = "";
    ObjRule_Rules.CustomCategoryCode = null;
    ObjRule_Rules.TypeId = 0;
    ObjRule_Rules.Type = null;
    ObjRule_Rules.UserDefined = true;
    ObjRule_Rules.Script = "";
    ObjRule_Rules.CSharpCode = "";
    ObjRule_Rules.JsonObject = "";
    ObjRule_Rules.CustomCategoryCode = 0;

    var SelectedItems_GridRules_Rules = GridRules_Rules.getSelectedItems();
    if (SelectedItems_GridRules_Rules.length > 0)
        ObjRule_Rules.ID = SelectedItems_GridRules_Rules[0].getMember("ID").get_text();
    else ObjRule_Rules.ID = 0;

    if (CurrentPageStateRule_Rules != 'Delete') {

        ObjRule_Rules.IdentifierCode = document.getElementById('txtRuleCode').value;
        ObjRule_Rules.Name = document.getElementById('txtRuleName').value;

        if (SelectedRules_Rule.CustomCategoryCode != undefined && SelectedRules_Rule.CustomCategoryCode != "") {
            ObjRule_Rules.CustomCategoryCode = SelectedRules_Rule.CustomCategoryCode;
        } else {
            ObjRule_Rules.CustomCategoryCode = SelectedRules_Rule.IdentifierCode;
        }


        if (cmbRuleType_Rules.getSelectedItem() != undefined) {
            ObjRule_Rules.TypeId = parseInt(cmbRuleType_Rules.getSelectedItem().Value);
        } else if (SelectedRules_Rule.TypeId != undefined) {
            ObjRule_Rules.TypeId = SelectedRules_Rule.TypeId;
        }

        if (SelectedRules_Rule.UserDefined != undefined)
            ObjRule_Rules.UserDefined = SelectedRules_Rule.UserDefined;
        else SelectedRules_Rule.UserDefined = true;

        if (SelectedRules_Rule.Script != undefined)
            ObjRule_Rules.Script = SelectedRules_Rule.Script;
        else ObjRule_Rules.Script = '';

        if (SelectedRules_Rule.CSharpCode != undefined)
            ObjRule_Rules.CSharpCode = SelectedRules_Rule.CSharpCode;
        else ObjRule_Rules.CSharpCode = '';

        ObjRule_Rules.JsonObject = JSON.stringify(SelectedRules_Rule.JsonObject);

    }

    // Using Ajax Dll
    UpdateRule_RulePage_Call(ObjRule_Rules);

    // Using ComponentArt CallBack Parameter
    //CallBackSaveRules_Rules_Call(ObjRule_Rules);
}

function UpdateRule_RulePage_Call(objRuleRules) {

    //Using Ajax Dll
    UpdateRule_RulesPage(
        CharToKeyCode_Rules(objRuleRules.ID),
        CharToKeyCode_Rules(objRuleRules.IdentifierCode),
        CharToKeyCode_Rules(objRuleRules.Name),
        CharToKeyCode_Rules(objRuleRules.CustomCategoryCode),
        CharToKeyCode_Rules(objRuleRules.TypeId),
        CharToKeyCode_Rules(objRuleRules.UserDefined),
        CharToKeyCode_Rules(objRuleRules.Script),
        CharToKeyCode_Rules(objRuleRules.CSharpCode),
        /*CharToKeyCode_Rules(*/objRuleRules.JsonObject/*)*/,
        CharToKeyCode_Rules(CurrentPageStateRule_Rules)
    );
}
function UpdateRule_RulesPage_onCallBack(Response) {
    //Console.log(JSON.stringify(Response));

    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Rules').value;
            Response[1] = document.getElementById('hfConnectionError_Rules').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            Fill_GridRules_Rules(CurrentPageIndex_GridRules_Rules);
            ClearListRule_Rules();
            RefreshRule_Rules();
            ClearControlsRule_Rules();
            ChangePageStateRule_Rules('View');
        }
        else {
            if (CurrentPageStateRule_Rules == 'Delete')
                ChangePageStateRule_Rules('View');
        }
    }
}

function CallBackSaveRules_Rules_Call(objRuleRules) {
    
    CallBackSaveRules_Rules.callback(
        CharToKeyCode_Rules(objRuleRules.ID),
        CharToKeyCode_Rules(objRuleRules.IdentifierCode),
        CharToKeyCode_Rules(objRuleRules.Name),
        CharToKeyCode_Rules(objRuleRules.CustomCategoryCode),
        CharToKeyCode_Rules(objRuleRules.TypeId),
        CharToKeyCode_Rules(objRuleRules.UserDefined),
        CharToKeyCode_Rules(objRuleRules.Script),
        CharToKeyCode_Rules(objRuleRules.CSharpCode),
        /*CharToKeyCode_Rules(*/objRuleRules.JsonObject/*)*/,
        CharToKeyCode_Rules(CurrentPageStateRule_Rules)
    );
}
function CallBackSaveRules_Rules_onCallbackComplete(sender, e) {

    var strCallBackComplete = document.getElementById('hfCallBackDataSaveRules_Rules').value;

    if (strCallBackComplete != "") {
        var RetMessage = JSON.parse(strCallBackComplete);// eval('(' + strCallBackComplete + ')');

        if (RetMessage != null && RetMessage.length > 0) {
            if (RetMessage[1] == "ConnectionError") {
                RetMessage[0] = document.getElementById('hfErrorType_Rules').value;
                RetMessage[1] = document.getElementById('hfConnectionError_Rules').value;
            }
            showDialog(RetMessage[0], RetMessage[1], RetMessage[2]);
            if (RetMessage[2] == 'success') {
                Fill_GridRules_Rules(CurrentPageIndex_GridRules_Rules);
                ClearListRule_Rules();
                RefreshRule_Rules();
                ClearControlsRule_Rules();
                ChangePageStateRule_Rules('View');
            }
            else {
                if (CurrentPageStateRule_Rules == 'Delete')
                    ChangePageStateRule_Rules('View');
            }
        }
    }
}
function CallBackSaveRules_Rules_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridRules_Rules').innerHTML = '';
    ShowConnectionError_Rules();
}

/* Rule Ajax functions */


/* Conform Dialog Exit | Delete functions */
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageStateRule_Rules('View');
}
function tlbItemOk_TlbOkConfirm_onClick() {

    var objType = DialogConfirm.get_value();

    if (objType == 'Rule') {
        switch (ConfirmStateRule_Rules) {
            case 'Delete':

                DialogConfirm.Close();
                UpdateRule_Rules();

                break;
            case 'Exit':

                RefreshRule_Rules();
                CloseDialogRulesManagement();

                break;
            default:
        }
    }

    if (objType == 'RuleParameter') {
        switch (ConfirmStateRuleParameter_Rules) {
            case 'Delete':

                DialogConfirm.Close();
                UpdateRuleParameter_Rules();

                break;
            case 'Exit':

                RefreshRuleParameter_Rules();
                CloseDialogRulesManagement();

                break;
            default:
        }
    }



}
/* /Conform Dialog Exit | Delete functions */

/* -------------------------------------------------------------------------- */
/* ---------------------------------- /Rule --------------------------------- */
/* -------------------------------------------------------------------------- */



/* --------------------------------------------------------------------------- */
/* ------------------------------ Rule Parameter ----------------------------- */
/* --------------------------------------------------------------------------- */

var CurrentPageStateRuleParameter_Rules = 'View';
var SelectedRuleParameter_Rules = new Object();
var ConfirmStateRuleParameter_Rules = null;
var CombosCallBackCurrentStateRuleParameter_Rules = new Object();

var EnumRuleParameterTypeRuleParameter_Rules = null;
function SetEnumTypes() {
    EnumRuleParameterTypeRuleParameter_Rules = JSON.parse($('#hfJsonRuleParameterTypeEnum').val());
}
function GetRuleTempParamTypeTitle_Rules(enumId) {
    return EnumRuleParameterTypeRuleParameter_Rules[enumId];
}


/* Grid Rule Parameter events */
function GridRuleParameters_Rules_onLoad(sender, args) {
    document.getElementById('loadingPanel_GridRuleParameters_Rule').innerHTML = '';
}
function GridRuleParameters_Rules_onItemSelect(sender, args) {
    if (CurrentPageStateRuleParameter_Rules != 'Add')
        NavigateRuleParameter_Rules(args.get_item());
}
function CallBack_GridRuleParameters_Rule_OnCallbackComplete(sender, args) {

}
function CallBack_GridRuleParameters_Rule_onCallbackError(sender, args) {

}
function GridRuleParametersCallCallBack_Rules(SelectedRuleId) {
    CallBack_GridRuleParameters_Rule.callback(CharToKeyCode_Rules(SelectedRuleId));
}

/* Grid Rule Parameter events */


/* ComboBox RuleParameter */
function cmbRuleParameterType_Rules_onBeforeCallback(sender, args) {
    cmbRuleParameterType_Rules.dispose();
}
function cmbRuleParameterType_Rules_onCallbackComplete(sender, args) {
    var error = document.getElementById('ErrorHiddenField_RuleParameterTypeFields').value;
    if (error == "") {
        document.getElementById('cmbRuleParameterType_Rules_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompletedRule_Rules())
            $('#cmbRuleParameterType_Rules_DropImage').mousedown();
        cmbRuleParameterType_Rules.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbRuleParameterType_Rules_DropDown').style.display = 'none';
    }
}
function cmbRuleParameterType_Rules_onExpand(sender, args) {
    if (cmbRuleParameterType_Rules.get_itemCount() == 0 && CombosCallBackCurrentStateRuleParameter_Rules.IsExpandOccured_cmbRuleParameterType_Rules == undefined) {
        CombosCallBackCurrentStateRuleParameter_Rules.cmbRuleParameterType_Rules_Text = document.getElementById('cmbRuleParameterType_Rules_Input').value;
        CombosCallBackCurrentStateRuleParameter_Rules.IsExpandOccured_cmbRuleParameterType_Rules = true;
        CallBack_cmbRuleParameterType_Rules.callback();
    } else {
        document.getElementById('cmbRuleParameterType_Rules_Input').value = CombosCallBackCurrentStateRuleParameter_Rules.cmbRuleParameterType_Rules_Text;
    }
}
function cmbRuleParameterType_Rules_onCallbackError(sender, args) {
    document.getElementById('loadingPanel_GridRules_Rules').innerHTML = '';
    ShowConnectionError_Rules();
}
/* /ComboBox RuleParameter */

/* Rule Parameter Toolbar Item Clicks */
function tlbItemNew_TlbRuleParameters_onClick(sender, args) {
    ClearControlsRuleTempParam_Rules();
    ChangePageStateRuleParameter_Rules('Add');
}
function tlbItemEdit_TlbRuleParameters_onClick(sender, args) {
    ChangePageStateRuleParameter_Rules('Edit');
}
function tlbItemDelete_TlbRuleParameters_onClick(sender, args) {
    ChangePageStateRuleParameter_Rules('Delete');
}
function tlbItemSave_TlbRuleParameters_onClick(sender, args) {
    RuleParameters_onSave();
}
function tlbItemCancel_TlbRuleParameters_onClick(sender, args) {
    RuleParameters_onCancel();
}
/* Rule Parameter Toolbar Item Clicks */

function Refresh_GridRuleParameters_Rules(sender, args) {
    if (GridRules_Rules.getSelectedItems()[0] != undefined) {
        RefreshRuleParameter_Rules();
        ClearControlsRuleTempParam_Rules();
        GridRuleParametersCallCallBack_Rules(GridRules_Rules.getSelectedItems()[0].getMember('ID').get_text());
    }


}

function ChangePageStateRuleParameter_Rules(State) {
    CurrentPageStateRuleParameter_Rules = State;
    SetActionModeRuleParameter_Rules(State);

    if (State == 'Add' || State == 'Edit' || State == 'Delete') {
        if (TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters').set_enabled(false);
            TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters').set_imageUrl('add_silver.png');
        }
        if (TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters').set_enabled(false);
            TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters').set_imageUrl('edit_silver.png');
        }
        if (TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters').set_enabled(false);
            TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters').set_imageUrl('remove_silver.png');
        }

        TlbRuleParameters.get_items().getItemById('tlbItemSave_TlbRuleParameters').set_enabled(true);
        TlbRuleParameters.get_items().getItemById('tlbItemSave_TlbRuleParameters').set_imageUrl('save.png');

        TlbRuleParameters.get_items().getItemById('tlbItemCancel_TlbRuleParameters').set_enabled(true);
        TlbRuleParameters.get_items().getItemById('tlbItemCancel_TlbRuleParameters').set_imageUrl('cancel.png');

        document.getElementById('txtRuleParameterTitle_Rule').disabled = false;
        document.getElementById('txtRuleParameterName_Rule').disabled = false;
        cmbRuleParameterType_Rules.enable();

        if (State == 'Edit')
            NavigateRuleParameter_Rules(GridRuleParameters_Rules.getSelectedItems()[0]);
        if (State == 'Delete')
            RuleParameters_onSave();
    }

    if (State == 'View') {
        if (TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters').set_enabled(true);
            TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters').set_imageUrl('add.png');
        }
        if (TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters').set_enabled(true);
            TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters').set_imageUrl('edit.png');
        }
        if (TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters').set_enabled(true);
            TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters').set_imageUrl('remove.png');
        }

        TlbRuleParameters.get_items().getItemById('tlbItemSave_TlbRuleParameters').set_enabled(false);
        TlbRuleParameters.get_items().getItemById('tlbItemSave_TlbRuleParameters').set_imageUrl('save_silver.png');

        TlbRuleParameters.get_items().getItemById('tlbItemCancel_TlbRuleParameters').set_enabled(false);
        TlbRuleParameters.get_items().getItemById('tlbItemCancel_TlbRuleParameters').set_imageUrl('cancel_silver.png');

        document.getElementById('txtRuleParameterTitle_Rule').disabled = true;
        document.getElementById('txtRuleParameterName_Rule').disabled = true;
        cmbRuleParameterType_Rules.disable();

    }

}

function NavigateRuleParameter_Rules(RuleParameter) {

    ClearControlsRuleTempParam_Rules();

    if (RuleParameter == undefined) {
        return;
    }

    SelectedRuleParameter_Rules = new Object();

    FillSelectedRuleTempParam_Rules(
        RuleParameter.getMember('ID').get_text(),
        RuleParameter.getMember('Name').get_text(),
        RuleParameter.getMember('Title').get_text(),
        RuleParameter.getMember('Type').get_text(),
        RuleParameter.getMember('RuleTemplateId').get_text()
    );

    FillFieldsSelectedRuleTempParam_Rules();

}

function ClearListRuleParameter_Rules() {
    if (CurrentPageStateRuleParameter_Rules != 'Edit') {
        ClearControlsRuleTempParam_Rules();
    }
}
function ClearControlsRuleTempParam_Rules() {

    document.getElementById('txtRuleParameterTitle_Rule').value = '';
    document.getElementById('txtRuleParameterName_Rule').value = '';

    document.getElementById('cmbRuleParameterType_Rules_Input').value = '';
    if (cmbRuleParameterType_Rules.getSelectedItem() != undefined)
        cmbRuleParameterType_Rules.unSelect();

}

function RuleParameters_onSave() {
    if (CurrentPageStateRuleParameter_Rules != 'Delete')
        UpdateRuleParameter_Rules();
    else
        ShowDialogConfirm('Delete', 'RuleParameter');
}

function RuleParameters_onCancel() {
    ChangePageStateRuleParameter_Rules('View');
    ClearControlsRuleTempParam_Rules();
}

function RefreshRuleParameter_Rules() {
    FillSelectedRuleTempParam_Rules(0, null, null, null, 0);
}

function FillSelectedRuleTempParam_Rules(id, name, title, type, ruleId) {
    SelectedRuleParameter_Rules.RuleTempParamId = id;
    SelectedRuleParameter_Rules.RuleParameterName = name;
    SelectedRuleParameter_Rules.RuleParameterTitle = title;
    SelectedRuleParameter_Rules.RuleParameterType = type;
    SelectedRuleParameter_Rules.RuleTempParamRuleId = ruleId;
}
function FillFieldsSelectedRuleTempParam_Rules() {
    document.getElementById('txtRuleParameterTitle_Rule').value = SelectedRuleParameter_Rules.RuleParameterTitle;
    document.getElementById('txtRuleParameterName_Rule').value = SelectedRuleParameter_Rules.RuleParameterName;
    document.getElementById('cmbRuleParameterType_Rules_Input').value = GetRuleTempParamTypeTitle_Rules(SelectedRuleParameter_Rules.RuleParameterType);
}

function SetActionModeRuleParameter_Rules(state) {
    document.getElementById('ActionMode_RuleParameter_Rules').innerHTML = document.getElementById("hfState" + state + "_Rules").value;
}

function UpdateRuleParameter_Rules() {

    ObjRuleParameter_Rules = new Object();

    ObjRuleParameter_Rules.ID = 0;
    ObjRuleParameter_Rules.RuleId = 0;
    ObjRuleParameter_Rules.Title = "";
    ObjRuleParameter_Rules.Name = "";
    ObjRuleParameter_Rules.TypeId = 0;
    ObjRuleParameter_Rules.Type = null;

    var SelectedItems_GridRuleParameters_Rules = GridRuleParameters_Rules.getSelectedItems();
    if (SelectedItems_GridRuleParameters_Rules.length > 0)
        ObjRuleParameter_Rules.ID = SelectedItems_GridRuleParameters_Rules[0].getMember("ID").get_text();
    else ObjRuleParameter_Rules.ID = 0;

    var SelectedItems_GridRules_Rules = GridRules_Rules.getSelectedItems();
    if (SelectedItems_GridRules_Rules.length > 0)
        ObjRuleParameter_Rules.RuleId = SelectedItems_GridRules_Rules[0].getMember("ID").get_text();
    else ObjRuleParameter_Rules.RuleId = 0;

    if (CurrentPageStateRuleParameter_Rules != 'Delete') {

        ObjRuleParameter_Rules.Title = document.getElementById('txtRuleParameterTitle_Rule').value;
        ObjRuleParameter_Rules.Name = document.getElementById('txtRuleParameterName_Rule').value;

        if (cmbRuleParameterType_Rules.getSelectedItem() != undefined) {
            ObjRuleParameter_Rules.TypeId = parseInt(cmbRuleParameterType_Rules.getSelectedItem().Value);
        } else if (SelectedRuleParameter_Rules.TypeId != undefined) {
            ObjRuleParameter_Rules.TypeId = SelectedRuleParameter_Rules.TypeId;
        }
    }

    UpdateRuleParameter_RulesPage(
        CharToKeyCode_Rules(ObjRuleParameter_Rules.ID),
        CharToKeyCode_Rules(ObjRuleParameter_Rules.RuleId),
        CharToKeyCode_Rules(ObjRuleParameter_Rules.Title),
        CharToKeyCode_Rules(ObjRuleParameter_Rules.Name),
        CharToKeyCode_Rules(ObjRuleParameter_Rules.TypeId),
        CharToKeyCode_Rules(CurrentPageStateRuleParameter_Rules)
    );
}
function UpdateRuleParameter_RulesPage_onCallBack(Response) {
    //Console.log(JSON.stringify(Response));

    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Rules').value;
            Response[1] = document.getElementById('hfConnectionError_Rules').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {

            var SelectedItems_GridRule_Rules = GridRules_Rules.getSelectedItems();
            if (SelectedItems_GridRule_Rules.length > 0) {

                var se1 = SelectedItems_GridRule_Rules[0];
                var se2 = se1.getMember("ID");
                var se3 = se2.get_text();

                GridRuleParametersCallCallBack_Rules(se3);
            }

            ClearListRuleParameter_Rules();
            RefreshRuleParameter_Rules();
            ClearControlsRuleTempParam_Rules();
            ChangePageStateRuleParameter_Rules('View');
        }
        else {
            if (CurrentPageStateRuleParameter_Rules == 'Delete')
                ChangePageStateRuleParameter_Rules('View');
        }
    }
}




/* ---------------------------------------------------------------------------- */
/* ------------------------------ /Rule Parameter ----------------------------- */
/* ---------------------------------------------------------------------------- */
