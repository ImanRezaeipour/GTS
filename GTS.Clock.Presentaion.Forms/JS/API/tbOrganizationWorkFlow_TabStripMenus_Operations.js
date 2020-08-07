
var CurrentPageState_OrganizationWorkFlow = 'View';
var ConfirmState_OrganizationWorkFlow = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var LoadState_Managers = 'Normal';
var ObjexpandingDepPersonnelNode_OrganizationWorkFlow = null;
var box_WorkFlowSearch_OrganizationWorkFlow_IsShown = false;

function GetBoxesHeaders_OrganizationWorkFlow() {
    document.getElementById('header_WorkFlowSearch_OrganizationWorkFlow').innerHTML = document.getElementById('hfheader_WorkFlowSearch_OrganizationWorkFlow').value;
    document.getElementById('header_WorkFlows_OrganizationWorkFlow').innerHTML = document.getElementById('hfheader_WorkFlows_OrganizationWorkFlow').value;
    document.getElementById('header_UnderManagementPersonnel_OrganizationWorkFlow').innerHTML = document.getElementById('hfheader_UnderManagementPersonnel_OrganizationWorkFlow').value;
}

function SetPosition_DropDownDives_OrganizationWorkFlow() {
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('box_WorkFlowSearch_OrganizationWorkFlow').style.right = '10px';
    if (parent.CurrentLangID == 'en-US')
        document.getElementById('box_WorkFlowSearch_OrganizationWorkFlow').style.left = '10px';
}

function imgbox_WorkFlowSearch_OrganizationWorkFlow_onClick() {
    CollapseControls_OrganizationWorkFlow();
    setSlideDownSpeed(200);
    slidedown_showHide('box_WorkFlowSearch_OrganizationWorkFlow');

    if (box_WorkFlowSearch_OrganizationWorkFlow_IsShown) {
        box_WorkFlowSearch_OrganizationWorkFlow_IsShown = false;
        document.getElementById('imgbox_WorkFlowSearch_OrganizationWorkFlow').src = 'Images/Ghadir/arrowDown.jpg';
    }
    else {
        box_WorkFlowSearch_OrganizationWorkFlow_IsShown = true;
        document.getElementById('imgbox_WorkFlowSearch_OrganizationWorkFlow').src = 'Images/Ghadir/arrowUp.jpg';
    }
}

function tlbItemNew_TlbOrganizationWorkFlow_onClick() {
    CurrentPageState_OrganizationWorkFlow = 'Add';
    ShowDialogUnderManagementPersonnel();
}

function tlbItemEdit_TlbOrganizationWorkFlow_onClick() {
    CurrentPageState_OrganizationWorkFlow = 'Edit';
    ShowDialogUnderManagementPersonnel();
}

function tlbItemDelete_TlbOrganizationWorkFlow_onClick() {
    CurrentPageState_OrganizationWorkFlow = 'Delete';
    ShowDialogConfirm('Delete');
}

function tlbItemExit_TlbOrganizationWorkFlow_onClick() {
    ShowDialogConfirm('Exit');
}

function cmbSearchField_OrganizationWorkFlow_onExpand(sender, e) {
    if (cmbSearchField_OrganizationWorkFlow.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSearchField_OrganizationWorkFlow == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSearchField_OrganizationWorkFlow = true;
        CallBack_cmbSearchField_OrganizationWorkFlow.callback();
    }
}

function cmbSearchField_OrganizationWorkFlow_onCollapse(sender, e) {
    if (CurrentPageCombosCallBcakStateObj.cmbSearchField_OrganizationWorkFlow && cmbSearchField_OrganizationWorkFlow.getSelectedItem() == undefined) {
        CurrentPageCombosCallBcakStateObj.cmbSearchField_OrganizationWorkFlow = false;
        cmbSearchField_OrganizationWorkFlow.expand();
    }
}

function CallBack_cmbSearchField_OrganizationWorkFlow_onBeforeCallback(sender, e) {
    cmbSearchField_OrganizationWorkFlow.dispose();
}

function CallBack_cmbSearchField_OrganizationWorkFlow_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkFlowSearch').value;
    if (error == "") {
        document.getElementById('cmbSearchField_OrganizationWorkFlow_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbSearchField_OrganizationWorkFlow = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbSearchField_OrganizationWorkFlow = false;
        cmbSearchField_OrganizationWorkFlow.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbSearchField_OrganizationWorkFlow_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function Fill_GridWorkFlows_OrganizationWorkFlow(LoadState) {
    var SearchField = 'NotSpec';
    var SearchTerm = '';

    document.getElementById('loadingPanel_GridWorkFlows_OrganizationWorkFlow').innerHTML = document.getElementById('hfloadingPanel_GridWorkFlows_OrganizationWorkFlow').value;
    switch (LoadState) {
        case 'Normal':
            break;
        case 'Search':
            selectedSearchField = cmbSearchField_OrganizationWorkFlow.getSelectedItem();
            if (selectedSearchField != undefined && selectedSearchField != null) 
                SearchField = selectedSearchField.get_value();
            SearchTerm = document.getElementById('txtSearchTerm_OrganizationWorkFlow').value;
            break;
    }
    CallBack_GridWorkFlows_OrganizationWorkFlow.callback(CharToKeyCode_OrganizationWorkFlow(LoadState), CharToKeyCode_OrganizationWorkFlow(SearchField), CharToKeyCode_OrganizationWorkFlow(SearchTerm));
}


function CallBack_GridWorkFlows_OrganizationWorkFlow_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkFlows_OrganizationWorkFlow').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Refresh_GridWorkFlows_OrganizationWorkFlow();
    }
}

function GridWorkFlows_OrganizationWorkFlow_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridWorkFlows_OrganizationWorkFlow').innerHTML = '';
}

function tlbItemApplyConditions_TlbApplyConditions_OrganizationWorkFlow_onClick() {
    Fill_GridWorkFlows_OrganizationWorkFlow('Search');
}

function tlbItemWorkFlowView_TlbInterAction_OrganizationWorkFlow_onClick() {
    DrawFlow_OrganizationWorkFlow('View');
}

function DrawFlow_OrganizationWorkFlow(state) {
    var FlowID_OrganizationWorkFlow = null;
    switch (state) {
        case 'View':
            var SelectedItems_GridWorkFlows_OrganizationWorkFlow = GridWorkFlows_OrganizationWorkFlow.getSelectedItems();
            if (SelectedItems_GridWorkFlows_OrganizationWorkFlow.length > 0 && SelectedItems_GridWorkFlows_OrganizationWorkFlow != null)
                FlowID_OrganizationWorkFlow = SelectedItems_GridWorkFlows_OrganizationWorkFlow[0].getMember('ID').get_text();
            break;
        case 'Refresh':
            FlowID_OrganizationWorkFlow = '0';
            break;
    }
    CallBack_WorkFlow_OrganizationWorkFlow.callback(CharToKeyCode_OrganizationWorkFlow(FlowID_OrganizationWorkFlow));
}

function CallBack_WorkFlow_OrganizationWorkFlow_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkFlow_OrganizationWorkFlow').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}


function tlbItemUnderManagementPersonnel_TlbInterAction_OrganizationWorkFlow_onClick() {
    FillByFlowID_trvUnderManagementPersonnel_OrganizationWorkFlow();
}

function FillByFlowID_trvUnderManagementPersonnel_OrganizationWorkFlow() {
    var SelectedItems_GridWorkFlows_OrganizationWorkFlow = GridWorkFlows_OrganizationWorkFlow.getSelectedItems();
    if (SelectedItems_GridWorkFlows_OrganizationWorkFlow.length > 0 && SelectedItems_GridWorkFlows_OrganizationWorkFlow != null) {
        var flowID = SelectedItems_GridWorkFlows_OrganizationWorkFlow[0].getMember('ID').get_text();
        Fill_trvUnderManagementPersonnel_OrganizationWorkFlow(flowID);
    }
    else {
        if (trvUnderManagementPersonnel_OrganizationWorkFlow.get_nodes().get_nodeArray().length > 0) {
            trvUnderManagementPersonnel_OrganizationWorkFlow.beginUpdate();
            trvUnderManagementPersonnel_OrganizationWorkFlow.get_nodes().clear();
            trvUnderManagementPersonnel_OrganizationWorkFlow.endUpdate();
        }
    }
}


function Fill_trvUnderManagementPersonnel_OrganizationWorkFlow(flowID) {
    document.getElementById('loadingPanel_trvUnderManagementPersonnel_OrganizationWorkFlow').innerHTML = document.getElementById('hfloadingPanel_trvUnderManagementPersonnel_OrganizationWorkFlow').value;
    ObjexpandingDepPersonnelNode_OrganizationWorkFlow = null;
    CallBack_trvUnderManagementPersonnel_OrganizationWorkFlow.callback(CharToKeyCode_OrganizationWorkFlow(flowID));
}

function Refresh_GridWorkFlows_OrganizationWorkFlow() {
    ClearSearchDetails_OrganizationWorkFlow();
    Clear_trvUnderManagementPersonnel_OrganizationWorkFlow();
    Fill_GridWorkFlows_OrganizationWorkFlow('Normal');
    DrawFlow_OrganizationWorkFlow('Refresh');
}

function Clear_trvUnderManagementPersonnel_OrganizationWorkFlow() {
    trvUnderManagementPersonnel_OrganizationWorkFlow.beginUpdate();
    trvUnderManagementPersonnel_OrganizationWorkFlow.get_nodes().clear();
    trvUnderManagementPersonnel_OrganizationWorkFlow.endUpdate();
}

function ClearSearchDetails_OrganizationWorkFlow() {
    document.getElementById('txtSearchTerm_OrganizationWorkFlow').value = '';
    document.getElementById('cmbSearchField_OrganizationWorkFlow_Input').value = '';
    cmbSearchField_OrganizationWorkFlow.unSelect();
}

function CallBack_trvUnderManagementPersonnel_OrganizationWorkFlow_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_UnderManagementPersonnel_OrganizationWorkFlow').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            FillByFlowID_trvUnderManagementPersonnel_OrganizationWorkFlow();
    }
}

function trvUnderManagementPersonnel_OrganizationWorkFlow_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvUnderManagementPersonnel_OrganizationWorkFlow').innerHTML = '';
}

function trvUnderManagementPersonnel_OrganizationWorkFlow_onCallbackComplete(sender, e) {
    if (ObjexpandingDepPersonnelNode_OrganizationWorkFlow != null) {
        if (ObjexpandingDepPersonnelNode_OrganizationWorkFlow.Node.get_nodes().get_length() == 0 && ObjexpandingDepPersonnelNode_OrganizationWorkFlow.HasChild) {
            ObjexpandingDepPersonnelNode_OrganizationWorkFlow = null;
            GetLoadonDemandError_DepartmetsPersonnel_UnderManagementPersonnelPage();
        }
        else
            ObjexpandingDepPersonnelNode_OrganizationWorkFlow = null;
    }
}

function GetLoadonDemandError_DepartmetsPersonnel_UnderManagementPersonnelPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function trvUnderManagementPersonnel_OrganizationWorkFlow_onNodeBeforeExpand(sender, e) {
    if (ObjexpandingDepPersonnelNode_OrganizationWorkFlow != null)
        ObjexpandingDepPersonnelNode_OrganizationWorkFlow = null;
    ObjexpandingDepPersonnelNode_OrganizationWorkFlow = new Object();
    ObjexpandingDepPersonnelNode_OrganizationWorkFlow.Node = e.get_node();
    if (e.get_node().get_nodes().get_length() == 1 && (e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined || e.get_node().get_nodes().get_nodeArray()[0].get_id() == '')) {
        ObjexpandingDepPersonnelNode_OrganizationWorkFlow.HasChild = true;
        trvUnderManagementPersonnel_OrganizationWorkFlow.beginUpdate();
        ObjexpandingDepPersonnelNode_OrganizationWorkFlow.Node.get_nodes().remove(0);
        trvUnderManagementPersonnel_OrganizationWorkFlow.endUpdate();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            ObjexpandingDepPersonnelNode_OrganizationWorkFlow.HasChild = false;
        else
            ObjexpandingDepPersonnelNode_OrganizationWorkFlow.HasChild = true;
    }
}

function Refresh_trvUnderManagementPersonnel_OrganizationWorkFlow() {
    FillByFlowID_trvUnderManagementPersonnel_OrganizationWorkFlow();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_OrganizationWorkFlow) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateWorkFow_OrganizationWorkFlow();
            break;
        case 'Exit':
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    CurrentPageState_OrganizationWorkFlow = 'View';
}

function ShowDialogUnderManagementPersonnel() {
    var ObjPageState_OrganizationWorkFlow = new Object();
    switch (CurrentPageState_OrganizationWorkFlow) {
        case 'Add':
            ObjPageState_OrganizationWorkFlow.PageState = CurrentPageState_OrganizationWorkFlow;
            parent.DialogUnderManagementPersonnel.set_value(ObjPageState_OrganizationWorkFlow);
            parent.DialogUnderManagementPersonnel.Show();
            break;
        case 'Edit':
            selectedItems_GridWorkFlows_OrganizationWorkFlow = GridWorkFlows_OrganizationWorkFlow.getSelectedItems();
            if (selectedItems_GridWorkFlows_OrganizationWorkFlow.length > 0) {
                ObjPageState_OrganizationWorkFlow.PageState = CurrentPageState_OrganizationWorkFlow;
                ObjPageState_OrganizationWorkFlow.FlowID = selectedItems_GridWorkFlows_OrganizationWorkFlow[0].getMember('ID').get_text();
                ObjPageState_OrganizationWorkFlow.FlowName = selectedItems_GridWorkFlows_OrganizationWorkFlow[0].getMember('FlowName').get_text();
                ObjPageState_OrganizationWorkFlow.IsActiveFlow = selectedItems_GridWorkFlows_OrganizationWorkFlow[0].getMember('ActiveFlow').get_value();
                ObjPageState_OrganizationWorkFlow.IsMainFlow = selectedItems_GridWorkFlows_OrganizationWorkFlow[0].getMember('MainFlow').get_value();
                ObjPageState_OrganizationWorkFlow.AccessGroupID = selectedItems_GridWorkFlows_OrganizationWorkFlow[0].getMember('AccessGroup.ID').get_text();
                ObjPageState_OrganizationWorkFlow.AccessGroupName = selectedItems_GridWorkFlows_OrganizationWorkFlow[0].getMember('AccessGroup.Name').get_text();
                parent.DialogUnderManagementPersonnel.set_value(ObjPageState_OrganizationWorkFlow);
                parent.DialogUnderManagementPersonnel.Show();
            }
            break;
    }
    CollapseControls_OrganizationWorkFlow();
}

function CharToKeyCode_OrganizationWorkFlow(str) {
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

function UpdateWorkFow_OrganizationWorkFlow() {
    var selectedFlowID_OrganizationWorkFlow = '0';
    if (GridWorkFlows_OrganizationWorkFlow.getSelectedItems().length > 0)
        selectedFlowID_OrganizationWorkFlow = GridWorkFlows_OrganizationWorkFlow.getSelectedItems()[0].getMember('ID').get_text();
    UpdateWorkFow_OrganizationWorkFlowPage(CharToKeyCode_OrganizationWorkFlow(CurrentPageState_OrganizationWorkFlow), CharToKeyCode_OrganizationWorkFlow(selectedFlowID_OrganizationWorkFlow));
    DialogWaiting.Show();
}

function UpdateWorkFow_OrganizationWorkFlowPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearSearchDetails_OrganizationWorkFlow();
            var ObjUnderManagement = new Object();
            ObjUnderManagement.FlowID = RetMessage[3];
            OrganizationWorkFlow_onAfterUpdate(CurrentPageState_OrganizationWorkFlow, ObjUnderManagement);
            CurrentPageState_OrganizationWorkFlow = 'View';
        }
        else {
            if (CurrentPageState_OrganizationWorkFlow == 'Delete')
                CurrentPageState_OrganizationWorkFlow = 'View';
        }
    }
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_OrganizationWorkFlow = confirmState;
    if (CurrentPageState_OrganizationWorkFlow == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_OrganizationWorkFlow').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_OrganizationWorkFlow').value;
    DialogConfirm.Show();
    CollapseControls_OrganizationWorkFlow();
}

function SetActionMode_OrganizationWorkFlow(state) {
    document.getElementById('ActionMode_OrganizationWorkFlow').innerHTML = document.getElementById('hf' + state + '_OrganizationWorkFlow').value;
}

function UnderManagement_onAfterUpdate(pageState, ObjUnderManagement) {
    OrganizationWorkFlow_onAfterUpdate(pageState, ObjUnderManagement);
}

function OrganizationWorkFlow_onAfterUpdate(pageState, ObjUnderManagement) {
    if (ObjUnderManagement != null) {
        var AccessGroupID = ObjUnderManagement.AccessGroupID;
        var AccessGroupName = ObjUnderManagement.AccessGroupName;
        var FlowID = ObjUnderManagement.FlowID;
        var FlowName = ObjUnderManagement.FlowName;
        var ActiveFlow = true;

        GridWorkFlows_OrganizationWorkFlow.beginUpdate();
        switch (pageState) {
            case 'Add':
                OrganizationFlowItem = GridWorkFlows_OrganizationWorkFlow.get_table().addEmptyRow(GridWorkFlows_OrganizationWorkFlow.get_recordCount());
                OrganizationFlowItem.setValue(0, FlowID, false);
                GridWorkFlows_OrganizationWorkFlow.selectByKey(FlowID, 0, false);
                break;
            case 'Edit':
                GridWorkFlows_OrganizationWorkFlow.selectByKey(FlowID, 0, false);
                OrganizationFlowItem = GridWorkFlows_OrganizationWorkFlow.getItemFromKey(0, FlowID);
                break;
            case 'Delete':
                GridWorkFlows_OrganizationWorkFlow.selectByKey(FlowID, 0, false);
                GridWorkFlows_OrganizationWorkFlow.deleteSelected();
                break;
        }
        if (pageState != 'Delete') {
            OrganizationFlowItem.setValue(1, AccessGroupID, false);
            OrganizationFlowItem.setValue(2, AccessGroupName, false);
            OrganizationFlowItem.setValue(3, FlowName, false);
            OrganizationFlowItem.setValue(4, ActiveFlow, false);
        }
        GridWorkFlows_OrganizationWorkFlow.endUpdate();
        if (pageState == 'Edit' || pageState == 'Delete')
            Refresh_trvUnderManagementPersonnel_OrganizationWorkFlow();
        DrawFlow_OrganizationWorkFlow('Refresh');
    }
}

function ManagerWorkFlow_onAfterUpdate(FlowID, IsActiveFlow, IsMainFlow) {
    GridWorkFlows_OrganizationWorkFlow.beginUpdate();
    GridWorkFlows_OrganizationWorkFlow.selectByKey(FlowID, 0, false);
    OrganizationFlowItem = GridWorkFlows_OrganizationWorkFlow.getItemFromKey(0, FlowID);
    OrganizationFlowItem.setValue(4, IsActiveFlow, false);
    OrganizationFlowItem.setValue(5, IsMainFlow, false);
    GridWorkFlows_OrganizationWorkFlow.endUpdate();
    DrawFlow_OrganizationWorkFlow('Refresh');
}

function CallBack_cmbSearchField_OrganizationWorkFlow_onCallbackError(sender, e) {
    ShowConnectionError_OrganizationWorkFlow();
}

function CallBack_GridWorkFlows_OrganizationWorkFlow_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridWorkFlows_OrganizationWorkFlow').innerHTML = '';
    ShowConnectionError_OrganizationWorkFlow();
}

function CallBack_trvUnderManagementPersonnel_OrganizationWorkFlow_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvUnderManagementPersonnel_OrganizationWorkFlow').innerHTML = '';
    ShowConnectionError_OrganizationWorkFlow();
}

function CallBack_WorkFlow_OrganizationWorkFlow_onCallbackError(sender, e) {
    ShowConnectionError_OrganizationWorkFlow();
}

function ShowConnectionError_OrganizationWorkFlow() {
    var error = document.getElementById('hfErrorType_OrganizationWorkFlow').value;
    var errorBody = document.getElementById('hfConnectionError_OrganizationWorkFlow').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemWorkFlowOperators_TlbOrganizationWorkFlow_onClick() {
    ShowDialogOperators();
}

function ShowDialogOperators() {
    var selectedItems_GridWorkFlows_OrganizationWorkFlow = GridWorkFlows_OrganizationWorkFlow.getSelectedItems();
    if (selectedItems_GridWorkFlows_OrganizationWorkFlow.length > 0) {
        var ObjDialogOperators = new Object();
        ObjDialogOperators.FlowID = selectedItems_GridWorkFlows_OrganizationWorkFlow[0].getMember('ID').get_text();
        ObjDialogOperators.FlowName = selectedItems_GridWorkFlows_OrganizationWorkFlow[0].getMember('FlowName').get_text();
        ObjDialogOperators.AccessGroupID = selectedItems_GridWorkFlows_OrganizationWorkFlow[0].getMember('AccessGroup.ID').get_text();
        ObjDialogOperators.AccessGroupName = selectedItems_GridWorkFlows_OrganizationWorkFlow[0].getMember('AccessGroup.Name').get_text();
        parent.DialogOperators.set_value(ObjDialogOperators);
        parent.DialogOperators.Show();
    }
}

function CollapseControls_OrganizationWorkFlow() {
    cmbSearchField_OrganizationWorkFlow.collapse();
}

function tlbItemFormReconstruction_TlbOrganizationWorkFlow_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvWorkFlowsView_iFrame').src = 'OrganizationWorkFlow.aspx';
}

function tlbItemHelp_TlbOrganizationWorkFlow_onClick() {
    LoadHelpPage('tlbItemHelp_TlbOrganizationWorkFlow');
}














