
var CurrentPageState_Operators = 'View';
var CurrentPageIndex_cmbOperators_Operators = 0;
var CurrentPageCombosCallBcakStateObj = new Object();
var LoadState_cmbPersonnel_Operators = 'Normal';
var AdvancedSearchTerm_cmbOperators_Operators = '';
var SelectedPersonnel_Operators = null;
var ConfirmState_Operators = null;
var ObjOperator_Operators = null;

function GetBoxesHeaders_Operators() {
    parent.document.getElementById('Title_DialogOperators').innerHTML = document.getElementById('hfTitle_DialogOperators').value;
    document.getElementById('header_Operators_Operators').innerHTML = document.getElementById('hfheader_Operators_Operators').value;
    document.getElementById('clmnName_cmbPersonnel_Operators').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_Operators').value;
    document.getElementById('clmnBarCode_cmbPersonnel_Operators').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_Operators').value;
    document.getElementById('clmnCardNum_cmbPersonnel_Operators').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_Operators').value;
}

function tlbItemNew_TlbOperators_onClick() {
    ChangePageState_Operators('Add');
    ClearList_Operators();
}

function tlbItemDelete_TlbOperators_onClick() {
    ChangePageState_Operators('Delete');
}

function tlbItemSave_TlbOperators_onClick() {
    Operator_onSave();
}

function tlbItemCancel_TlbOperators_onClick() {
    DialogConfirm.Close();
    ChangePageState_Operators('View');
}

function tlbItemExit_TlbOperators_onClick() {
    ShowDialogConfirm('Exit');
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_Operators_onClick() {
    LoadState_cmbPersonnel_Operators = 'Normal';
    SetPageIndex_cmbOperators_Operators(0);
}

function tlbItemFirst_TlbPaging_PersonnelSearch_Operators_onClick() {
    SetPageIndex_cmbOperators_Operators(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_Operators_onClick() {
    if (CurrentPageIndex_cmbOperators_Operators != 0) {
        CurrentPageIndex_cmbOperators_Operators = CurrentPageIndex_cmbOperators_Operators - 1;
        SetPageIndex_cmbOperators_Operators(CurrentPageIndex_cmbOperators_Operators);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_Operators_onClick() {
    if (CurrentPageIndex_cmbOperators_Operators < parseInt(document.getElementById('hfPersonnelPageCount_Operators').value) - 1) {
        CurrentPageIndex_cmbOperators_Operators = CurrentPageIndex_cmbOperators_Operators + 1;
        SetPageIndex_cmbOperators_Operators(CurrentPageIndex_cmbOperators_Operators);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_Operators_onClick() {
    SetPageIndex_cmbOperators_Operators(parseInt(document.getElementById('hfPersonnelPageCount_Operators').value) - 1);
}

function SetPageIndex_cmbOperators_Operators(pageIndex) {
    CurrentPageIndex_cmbOperators_Operators = pageIndex;
    Fill_cmbOperators_Operators(pageIndex);
}

function Fill_cmbOperators_Operators(pageIndex) {
    var SearchTerm = '';
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_Operators').value);
    switch (LoadState_cmbPersonnel_Operators) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm = document.getElementById('txtPersonnelSearch_Operators').value;
            break;
        case 'AdvancedSearch':
            SearchTerm = AdvancedSearchTerm_cmbOperators_Operators;
            break;
    }
    CallBack_cmbPersonnel_Operators.callback(CharToKeyCode_Operators(LoadState_cmbPersonnel_Operators), CharToKeyCode_Operators(pageSize.toString()), CharToKeyCode_Operators(pageIndex.toString()), CharToKeyCode_Operators(SearchTerm));
}

function CharToKeyCode_Operators(str) {
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

function cmbPersonnel_Operators_onExpand(sender, e) {
    SetPosition_cmbPersonnel_Operators();
    if (cmbPersonnel_Operators.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_Operators == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_Operators = true;
        SetPageIndex_cmbOperators_Operators(0);
    }
}

function cmbPersonnel_Operators_onCollapse(sender, e) {
    if (cmbPersonnel_Operators.getSelectedItem() == undefined && SelectedPersonnel_Operators != null)
        document.getElementById('cmbPersonnel_Operators_Input').value = SelectedPersonnel_Operators.Name;
}

function CallBack_cmbPersonnel_Operators_onBeforeCallback(sender, e) {
    cmbPersonnel_Operators.dispose();
}

function CallBack_cmbPersonnel_Operators_onCallbackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_Operators').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_Operators').value;
    document.getElementById('clmnBarCode_cmbPersonnel_Operators').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_Operators').value;
    document.getElementById('clmnCardNum_cmbPersonnel_Operators').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_Operators').value;

    SetPosition_cmbPersonnel_Operators();

    var error = document.getElementById('ErrorHiddenField_Personnel_Operators').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_Operators_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_Operators_DropImage').mousedown();
        else
            cmbPersonnel_Operators.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_Operators_DropDown').style.display = 'none';
    }
}

function CallBack_cmbPersonnel_Operators_onCallbackError(sender, e) {
    ShowConnectionError_Operators();
}

function tlbItemSearch_TlbSearchPersonnel_Operators_onClick() {
    LoadState_cmbPersonnel_Operators = 'Search';
    CurrentPageIndex_cmbOperators_Operators = 0;
    SetPageIndex_cmbOperators_Operators(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_Operators_onClick() {
    LoadState_cmbPersonnel_Operators = 'AdvancedSearch';
    CurrentPageIndex_cmbPersonnel_Operators = 0;
    ShowDialogPersonnelSearch('Operators');    
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
    CollapseControls_Operators();
}

function Refresh_GridOperators_Operators() {
    LoadState_cmbPersonnel_Operators = 'Normal';
    Fill_GridOperators_Operators();
}

function GridOperators_Operators_onItemSelect(sender, e) {
    if (CurrentPageState_Operators != 'Add')
        NavigateOperator_Operators(e.get_item());
}

function NavigateOperator_Operators(selectedOperatorItem) {
    if (selectedOperatorItem != null) {        
        SelectedPersonnel_Operators = new Object();
        SelectedPersonnel_Operators.ID = selectedOperatorItem.getMember('Person.ID').get_text();
        document.getElementById('cmbPersonnel_Operators_Input').value = SelectedPersonnel_Operators.Name = selectedOperatorItem.getMember('Person.Name').get_text();
        SelectedPersonnel_Operators.BarCode = selectedOperatorItem.getMember('Person.PersonCode').get_text();
        SelectedPersonnel_Operators.OrganizationPostID = selectedOperatorItem.getMember('Person.OrganizationUnit.ID').get_text();
        SelectedPersonnel_Operators.OrganizationPostName = selectedOperatorItem.getMember('Person.OrganizationUnit.Name').get_text();
        document.getElementById('chbActive_Operators').checked = selectedOperatorItem.getMember('Active').get_value();
        document.getElementById('txtDescription_Operators').value = selectedOperatorItem.getMember('Description').get_text();
    }
}

function GridOperators_Operators_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridOperators_Operators').innerHTML = '';        
}

function CallBack_GridOperators_Operators_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Operators').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridOperators_Operators();
    }
}

function CallBack_GridOperators_Operators_onCallbackError(sender, e) {
    ShowConnectionError_Operators();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Operators) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateOperator_Operators();
            break;
        case 'Exit':
            ClearList_Operators();
            Operators_onClose();
            break;
        default:
    }
}

function Operators_onClose() {
    parent.document.getElementById('DialogOperators_IFrame').src = 'WhitePage.aspx';
    parent.DialogOperators.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Operators('View');
}

function SetActionMode_Operators(state) {
    document.getElementById('ActionMode_Operators').innerHTML = document.getElementById("hf" + state + "_Operators").value;
}

function ChangePageState_Operators(state) {
    CurrentPageState_Operators = state;
    SetActionMode_Operators(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbOperators.get_items().getItemById('tlbItemNew_TlbOperators') != null) {
            TlbOperators.get_items().getItemById('tlbItemNew_TlbOperators').set_enabled(false);
            TlbOperators.get_items().getItemById('tlbItemNew_TlbOperators').set_imageUrl('add_silver.png');
        }
        if (TlbOperators.get_items().getItemById('tlbItemEdit_TlbOperators') != null) {
            TlbOperators.get_items().getItemById('tlbItemEdit_TlbOperators').set_enabled(false);
            TlbOperators.get_items().getItemById('tlbItemEdit_TlbOperators').set_imageUrl('edit_silver.png');
        }
        if (TlbOperators.get_items().getItemById('tlbItemDelete_TlbOperators') != null) {
            TlbOperators.get_items().getItemById('tlbItemDelete_TlbOperators').set_enabled(false);
            TlbOperators.get_items().getItemById('tlbItemDelete_TlbOperators').set_imageUrl('remove_silver.png');
        }
        TlbOperators.get_items().getItemById('tlbItemSave_TlbOperators').set_enabled(true);
        TlbOperators.get_items().getItemById('tlbItemSave_TlbOperators').set_imageUrl('save.png');
        TlbOperators.get_items().getItemById('tlbItemCancel_TlbOperators').set_enabled(true);
        TlbOperators.get_items().getItemById('tlbItemCancel_TlbOperators').set_imageUrl('cancel.png');
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemRefresh_TlbPaging_PersonnelSearch_Operators').set_enabled(true);
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemRefresh_TlbPaging_PersonnelSearch_Operators').set_imageUrl('refresh.png');
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemFirst_TlbPaging_PersonnelSearch_Operators').set_enabled(true);
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemFirst_TlbPaging_PersonnelSearch_Operators').set_imageUrl('first.png');
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemBefore_TlbPaging_PersonnelSearch_Operators').set_enabled(true);
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemBefore_TlbPaging_PersonnelSearch_Operators').set_imageUrl('Before.png');
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemNext_TlbPaging_PersonnelSearch_Operators').set_enabled(true);
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemNext_TlbPaging_PersonnelSearch_Operators').set_imageUrl('Next.png');
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemLast_TlbPaging_PersonnelSearch_Operators').set_enabled(true);
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemLast_TlbPaging_PersonnelSearch_Operators').set_imageUrl('last.png');
        TlbSearchPersonnel_Operators.get_items().getItemById('tlbItemSearch_TlbSearchPersonnel_Operators').set_enabled(true);
        TlbSearchPersonnel_Operators.get_items().getItemById('tlbItemSearch_TlbSearchPersonnel_Operators').set_imageUrl('search.png');
        TlbAdvancedSearch_Operators.get_items().getItemById('tlbItemAdvancedSearch_TlbAdvancedSearch_Operators').set_enabled(true);
        TlbAdvancedSearch_Operators.get_items().getItemById('tlbItemAdvancedSearch_TlbAdvancedSearch_Operators').set_imageUrl('eyeglasses.png');
        cmbPersonnel_Operators.enable();
        document.getElementById('txtPersonnelSearch_Operators').disabled = '';
        document.getElementById('chbActive_Operators').disabled = '';
        document.getElementById('txtDescription_Operators').disabled = '';
        if (state == 'Edit')
            NavigateOperator_Operators(GridOperators_Operators.getSelectedItems()[0]);
        if (state == 'Delete')
            Operator_onSave();
    }
    if (state == 'View') {
        if (TlbOperators.get_items().getItemById('tlbItemNew_TlbOperators') != null) {
            TlbOperators.get_items().getItemById('tlbItemNew_TlbOperators').set_enabled(true);
            TlbOperators.get_items().getItemById('tlbItemNew_TlbOperators').set_imageUrl('add.png');
        }
        if (TlbOperators.get_items().getItemById('tlbItemEdit_TlbOperators') != null) {
            TlbOperators.get_items().getItemById('tlbItemEdit_TlbOperators').set_enabled(true);
            TlbOperators.get_items().getItemById('tlbItemEdit_TlbOperators').set_imageUrl('edit.png');
        }
        if (TlbOperators.get_items().getItemById('tlbItemDelete_TlbOperators') != null) {
            TlbOperators.get_items().getItemById('tlbItemDelete_TlbOperators').set_enabled(true);
            TlbOperators.get_items().getItemById('tlbItemDelete_TlbOperators').set_imageUrl('remove.png');
        }
        if (TlbOperators.get_items().getItemById('tlbItemSubstituteSettings_TlbOperators') != null) {
            TlbOperators.get_items().getItemById('tlbItemSubstituteSettings_TlbOperators').set_enabled(true);
            TlbOperators.get_items().getItemById('tlbItemSubstituteSettings_TlbOperators').set_imageUrl('regulation.png');
        }
        TlbOperators.get_items().getItemById('tlbItemSave_TlbOperators').set_enabled(false);
        TlbOperators.get_items().getItemById('tlbItemSave_TlbOperators').set_imageUrl('save_silver.png');
        TlbOperators.get_items().getItemById('tlbItemCancel_TlbOperators').set_enabled(false);
        TlbOperators.get_items().getItemById('tlbItemCancel_TlbOperators').set_imageUrl('cancel_silver.png');
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemRefresh_TlbPaging_PersonnelSearch_Operators').set_enabled(false);
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemRefresh_TlbPaging_PersonnelSearch_Operators').set_imageUrl('refresh_silver.png');
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemFirst_TlbPaging_PersonnelSearch_Operators').set_enabled(false);
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemFirst_TlbPaging_PersonnelSearch_Operators').set_imageUrl('first_silver.png');
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemBefore_TlbPaging_PersonnelSearch_Operators').set_enabled(false);
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemBefore_TlbPaging_PersonnelSearch_Operators').set_imageUrl('Before_silver.png');
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemNext_TlbPaging_PersonnelSearch_Operators').set_enabled(false);
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemNext_TlbPaging_PersonnelSearch_Operators').set_imageUrl('Next_silver.png');
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemLast_TlbPaging_PersonnelSearch_Operators').set_enabled(false);
        TlbPaging_PersonnelSearch_Operators.get_items().getItemById('tlbItemLast_TlbPaging_PersonnelSearch_Operators').set_imageUrl('last_silver.png');
        TlbSearchPersonnel_Operators.get_items().getItemById('tlbItemSearch_TlbSearchPersonnel_Operators').set_enabled(false);
        TlbSearchPersonnel_Operators.get_items().getItemById('tlbItemSearch_TlbSearchPersonnel_Operators').set_imageUrl('search_silver.png');
        TlbAdvancedSearch_Operators.get_items().getItemById('tlbItemAdvancedSearch_TlbAdvancedSearch_Operators').set_enabled(false);
        TlbAdvancedSearch_Operators.get_items().getItemById('tlbItemAdvancedSearch_TlbAdvancedSearch_Operators').set_imageUrl('eyeglasses_silver.png');
        cmbPersonnel_Operators.disable();
        document.getElementById('txtPersonnelSearch_Operators').disabled = 'disabled';
        document.getElementById('chbActive_Operators').disabled = 'disabled';
        document.getElementById('txtDescription_Operators').disabled = 'disabled';
    }
}

function ClearList_Operators() {    
    document.getElementById('chbActive_Operators').checked = true;
    document.getElementById('txtDescription_Operators').value = '';
    document.getElementById('cmbPersonnel_Operators_Input').value = '';
    cmbPersonnel_Operators.unSelect();
}

function Operator_onSave() {
    if (CurrentPageState_Operators != 'Delete')
        UpdateOperator_Operators();
    else
        ShowDialogConfirm('Delete');
}

function UpdateOperator_Operators() {
    ObjOperator_Operators = new Object();
    ObjOperator_Operators.FlowID = '0';
    ObjOperator_Operators.IsActive = false;
    ObjOperator_Operators.ID = '0';
    ObjOperator_Operators.PersonnelID = '0';
    ObjOperator_Operators.PersonnelName = null;
    ObjOperator_Operators.PersonnelBarCode = null;
    ObjOperator_Operators.PersonnelOrganizationPostID = '0';
    ObjOperator_Operators.PersonnelOrganizationPostName = null;
    ObjOperator_Operators.Description = null;

    var SelectedItems_GridOperators_Operators = GridOperators_Operators.getSelectedItems();
    if (SelectedItems_GridOperators_Operators.length > 0)
        ObjOperator_Operators.ID = SelectedItems_GridOperators_Operators[0].getMember('ID').get_text();

    if (CurrentPageState_Operators != 'Delete') {
        var ObjDialogOperators = parent.DialogOperators.get_value();
        ObjOperator_Operators.FlowID = ObjDialogOperators.FlowID;
        if (cmbPersonnel_Operators.getSelectedItem() != undefined) {
            var SelectedPersonnel_Operators = cmbPersonnel_Operators.getSelectedItem();
            ObjOperator_Operators.PersonnelName = SelectedPersonnel_Operators.get_text();
            ObjOperator_Operators.PersonnelBarCode = SelectedPersonnel_Operators.BarCode;
            SelectedPersonnel_Operators = eval('(' + SelectedPersonnel_Operators.get_value() + ')');
            ObjOperator_Operators.PersonnelID = SelectedPersonnel_Operators.ID;
            ObjOperator_Operators.PersonnelOrganizationPostID = SelectedPersonnel_Operators.OrganizationPostID;
            ObjOperator_Operators.PersonnelOrganizationPostName = SelectedPersonnel_Operators.OrganizationPostName;
        }
        else {
            if (SelectedPersonnel_Operators != null) {
                ObjOperator_Operators.PersonnelID = SelectedPersonnel_Operators.ID;
                ObjOperator_Operators.PersonnelName = SelectedPersonnel_Operators.Name;
                ObjOperator_Operators.PersonnelBarCode = SelectedPersonnel_Operators.BarCode;
                ObjOperator_Operators.PersonnelOrganizationPostID = SelectedPersonnel_Operators.OrganizationPostID;
                ObjOperator_Operators.PersonnelOrganizationPostName = SelectedPersonnel_Operators.OrganizationPostName;
            }
        }
        ObjOperator_Operators.IsActive = document.getElementById('chbActive_Operators').checked;
        ObjOperator_Operators.Description = document.getElementById('txtDescription_Operators').value;
    }
    UpdateOperator_OperatorsPage(CharToKeyCode_Operators(CurrentPageState_Operators), CharToKeyCode_Operators(ObjOperator_Operators.ID), CharToKeyCode_Operators(ObjOperator_Operators.FlowID), CharToKeyCode_Operators(ObjOperator_Operators.IsActive.toString()), CharToKeyCode_Operators(ObjOperator_Operators.PersonnelID), CharToKeyCode_Operators(ObjOperator_Operators.Description));
    DialogWaiting.Show();
}

function UpdateOperator_OperatorsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Operators').value;
            Response[1] = document.getElementById('hfConnectionError_Operators').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            Operator_OnAfterUpdate(Response);
            ChangePageState_Operators('View');
            ClearList_Operators();
        }
        else {
            if (CurrentPageState_Operators == 'Delete')
                ChangePageState_Operators('View');
        }
    }
}

function Operator_OnAfterUpdate(Response) {
    if (ObjOperator_Operators != null) {
        var IsActive = ObjOperator_Operators.IsActive;
        var PersonnelID = ObjOperator_Operators.PersonnelID;
        var PersonnelName = ObjOperator_Operators.PersonnelName;
        var PersonnelBarCode = ObjOperator_Operators.PersonnelBarCode;
        var PersonnelOrganizationPostID = ObjOperator_Operators.PersonnelOrganizationPostID;
        var PersonnelOrganizationPostName = ObjOperator_Operators.PersonnelOrganizationPostName;
        var Description = ObjOperator_Operators.Description;

        var OperatorItem = null;
        GridOperators_Operators.beginUpdate();
        switch (CurrentPageState_Operators) {
            case 'Add':
                OperatorItem = GridOperators_Operators.get_table().addEmptyRow(GridOperators_Operators.get_recordCount());
                OperatorItem.setValue(0, Response[3], false);
                GridOperators_Operators.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridOperators_Operators.selectByKey(Response[3], 0, false);
                OperatorItem = GridOperators_Operators.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridOperators_Operators.selectByKey(ObjOperator_Operators.ID, 0, false);
                GridOperators_Operators.deleteSelected();
                break;
        }
        if (CurrentPageState_Operators != 'Delete') {
            OperatorItem.setValue(1, IsActive, false);
            OperatorItem.setValue(2, PersonnelBarCode, false);
            OperatorItem.setValue(3, PersonnelName, false);
            OperatorItem.setValue(4, PersonnelOrganizationPostName, false);
            OperatorItem.setValue(5, Description, false);
            OperatorItem.setValue(6, PersonnelID, false);
            OperatorItem.setValue(7, PersonnelOrganizationPostID, false);
        }
        GridOperators_Operators.endUpdate();
    }
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Operators = confirmState;
    if (CurrentPageState_Operators == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Operators').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Operators').value;
    DialogConfirm.Show();
    CollapseControls_Operators();
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function Fill_GridOperators_Operators() {
    document.getElementById('loadingPanel_GridOperators_Operators').innerHTML = document.getElementById('hfloadingPanel_GridOperators_Operators').value;
    var ObjDialogOperators = parent.DialogOperators.get_value();
    var FlowID = ObjDialogOperators.FlowID;
    CallBack_GridOperators_Operators.callback(CharToKeyCode_Operators(FlowID));
}

function ShowConnectionError_Operators() {
    var error = document.getElementById('hfErrorType_Operators').value;
    var errorBody = document.getElementById('hfConnectionError_Operators').value;
    showDialog(error, errorBody, 'error');
}

function Operators_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_cmbOperators_Operators = SearchTerm;
    SetPageIndex_cmbOperators_Operators(0);
}

function CollapseControls_Operators() {
    cmbPersonnel_Operators.collapse();
}

function tlbItemFormReconstruction_TlbOperators_onClick() {
    Operators_onClose();
    parent.document.getElementById('pgvWorkFlowsView_iFrame').contentWindow.ShowDialogOperators();
}

function tlbItemHelp_TlbOperators_onClick() {
    LoadHelpPage('tlbItemHelp_TlbOperators');
}

function SetPosition_cmbPersonnel_Operators() {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('cmbPersonnel_Operators_DropDown').style.left = document.getElementById('Mastertbl_Operators').clientWidth - 400 + 'px';
    }
    if (parent.CurrentLangID == 'en-US') {
        document.getElementById('cmbPersonnel_Operators_DropDown').style.left = '30px';
    }
}








