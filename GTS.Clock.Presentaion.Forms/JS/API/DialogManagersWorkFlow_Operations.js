
var CurrentPageIndex_cmbPersonnel_ManagersWorkFlow = 0;
var LoadState_cmbPersonnel_ManagersWorkFlow = 'Normal';
var CurrentPageCombosCallBcakStateObj = new Object();
var CurrentPageState_ManagersWorkFlow = 'View';
var ObjexpandingOrgPostNode_ManagersWorkFlow = null;
var CurrentFlowCreator_ManagersWorkFlow = null;
var ConfirmState_ManagersWorkFlow = null;
var ObjManagerWorkFlow_ManagerWorkFlow = null;
var AdvancedSearchTerm_cmbPersonnel_ManagersWorkFlow = '';


function GetBoxesHeaders_ManagersWorkFlow() {
    parent.document.getElementById('Title_DialogManagersWorkFlow').innerHTML = document.getElementById('hfheader_ManagersWorkFlow_ManagersWorkFlow').value;
    document.getElementById('header_ManagersWorkFlow_ManagersWorkFlow').innerHTML = document.getElementById('hfheader_ManagersWorkFlow_ManagersWorkFlow').value;
    document.getElementById('header_OrganizationPosts_ManagersWorkFlow').innerHTML = document.getElementById('hfheader_OrganizationPosts_ManagersWorkFlow').value;
    document.getElementById('header_OrganizationPostSearch_ManagersWorkFlow').innerHTML = document.getElementById('hfheader_OrganizationPostSearch_ManagersWorkFlow').value;
    document.getElementById('clmnName_cmbPersonnel_ManagersWorkFlow').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_ManagersWorkFlow').value;
    document.getElementById('clmnBarCode_cmbPersonnel_ManagersWorkFlow').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_ManagersWorkFlow').value;
    document.getElementById('clmnCardNum_cmbPersonnel_ManagersWorkFlow').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_ManagersWorkFlow').value;
}

function tlbItemNew_TlbManagersWorkFlow_onClick() {
    ChangePageState_ManagersWorkFlow('Add');
}

function CheckFlowIsActive_ManagersWorkFlow() {
    var ObjDialogManagersWorkFlow = parent.DialogManagersWorkFlow.get_value();
    document.getElementById('chbActiveFlow_ManagersWorkFlow').checked = ObjDialogManagersWorkFlow.IsActiveFlow;
}

function CheckFlowIsMain_ManagersWorkFlow() {
    var ObjDialogManagersWorkFlow = parent.DialogManagersWorkFlow.get_value();
    document.getElementById('chbMainFlow_ManagersWorkFlow').checked = ObjDialogManagersWorkFlow.IsMainFlow;
}

function ChangePageState_ManagersWorkFlow(state) {
    var disabled = '';
    CurrentPageState_ManagersWorkFlow = state;

    if (state == 'Add') {
        disabled = '';
        if (TlbManagersWorkFlow.get_items().getItemById('tlbItemNew_TlbManagersWorkFlow') != null) {
            TlbManagersWorkFlow.get_items().getItemById('tlbItemNew_TlbManagersWorkFlow').set_enabled(false);
            TlbManagersWorkFlow.get_items().getItemById('tlbItemNew_TlbManagersWorkFlow').set_imageUrl('add_silver.png');
        }
        if (TlbManagersWorkFlow.get_items().getItemById('tlbItemDelete_TlbManagersWorkFlow') != null) {
            TlbManagersWorkFlow.get_items().getItemById('tlbItemDelete_TlbManagersWorkFlow').set_enabled(false);
            TlbManagersWorkFlow.get_items().getItemById('tlbItemDelete_TlbManagersWorkFlow').set_imageUrl('remove_silver.png');
        }
    }
    if (state == 'View') {
        disabled = 'disabled';
        if (TlbManagersWorkFlow.get_items().getItemById('tlbItemNew_TlbManagersWorkFlow') != null) {
            TlbManagersWorkFlow.get_items().getItemById('tlbItemNew_TlbManagersWorkFlow').set_enabled(true);
            TlbManagersWorkFlow.get_items().getItemById('tlbItemNew_TlbManagersWorkFlow').set_imageUrl('add.png');
        }
        if (TlbManagersWorkFlow.get_items().getItemById('tlbItemDelete_TlbManagersWorkFlow') != null) {
            TlbManagersWorkFlow.get_items().getItemById('tlbItemDelete_TlbManagersWorkFlow').set_enabled(true);
            TlbManagersWorkFlow.get_items().getItemById('tlbItemDelete_TlbManagersWorkFlow').set_imageUrl('remove.png');
        }
    }

    document.getElementById('rdbManagersSearch_ManagersWorkFlow').disabled = disabled;
    document.getElementById('rdbPostsSearch_ManagersWorkFlow').disabled = disabled;
    document.getElementById('rdbManagersSearch_ManagersWorkFlow').checked = false;
    document.getElementById('rdbPostsSearch_ManagersWorkFlow').checked = false;
}

function tlbItemDelete_TlbManagersWorkFlow_onClick() {
    CurrentPageState_ManagersWorkFlow = 'Delete';
    ShowDialogConfirm('Delete');
}

function tlbItemSave_TlbManagersWorkFlow_onClick() {
    UpdateManagersWorkFlow_ManagersWorkFlow();
}

function UpdateManagersWorkFlow_ManagersWorkFlow() {
    var FlowObject = parent.DialogManagersWorkFlow.get_value();
    ObjManagerWorkFlow_ManagerWorkFlow = new Object();
    ObjManagerWorkFlow_ManagerWorkFlow.FlowID = FlowObject.FlowID.toString();
    var isActiveFlow = false;
    if (document.getElementById('chbActiveFlow_ManagersWorkFlow').checked)
        isActiveFlow = true;
    ObjManagerWorkFlow_ManagerWorkFlow.IsActiveFlow = isActiveFlow;
    var isMainFlow = false;
    if (document.getElementById('chbMainFlow_ManagersWorkFlow').checked)
        isMainFlow = true;
    ObjManagerWorkFlow_ManagerWorkFlow.IsMainFlow = isMainFlow;
    var FlowPartsList = '';
    var recordCount = GridManagersWorkFlow_ManagersWorkFlow.get_recordCount();
    for (var i = 0; i < recordCount; i++) {
        var splitter = ',';
        if (i == recordCount - 1)
            splitter = '';
        var FlowItem = GridManagersWorkFlow_ManagersWorkFlow.get_table().getRow(i);
        var Type = FlowItem.getMember('ManagerType').get_text();
        var TypeID = FlowItem.getMember('OwnerID').get_text();
        var FlowPartItem = '{"Type":"' + Type + '","TypeID":"' + TypeID + '","Level":"' + (i + 1) + '"}';
        FlowPartsList += FlowPartItem + splitter;
    }
    ObjManagerWorkFlow_ManagerWorkFlow.FlowPartsList = FlowPartsList = '[' + FlowPartsList + ']';
    UpdateManagersWorkFlow_ManagersWorkFlowPage(CharToKeyCode_ManagersWorkFlow(FlowObject.FlowState), CharToKeyCode_ManagersWorkFlow(ObjManagerWorkFlow_ManagerWorkFlow.FlowID), CharToKeyCode_ManagersWorkFlow(ObjManagerWorkFlow_ManagerWorkFlow.IsActiveFlow.toString()), CharToKeyCode_ManagersWorkFlow(ObjManagerWorkFlow_ManagerWorkFlow.IsMainFlow.toString()), CharToKeyCode_ManagersWorkFlow(ObjManagerWorkFlow_ManagerWorkFlow.FlowPartsList));
    DialogWaiting.Show();
}

function UpdateManagersWorkFlow_ManagersWorkFlowPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_ManagersWorkFlow').value;
            Response[2] = document.getElementById('hfConnectionError_ManagersWorkFlow').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (Response[2] == 'success')
            ManagersWorkFlow_onAfterUpdate();
    }
}

function ManagersWorkFlow_onAfterUpdate() {
    if (ObjManagerWorkFlow_ManagerWorkFlow != null) {
        parent.parent.document.getElementById('pgvWorkFlowsView_iFrame').contentWindow.ManagerWorkFlow_onAfterUpdate(ObjManagerWorkFlow_ManagerWorkFlow.FlowID, ObjManagerWorkFlow_ManagerWorkFlow.IsActiveFlow, ObjManagerWorkFlow_ManagerWorkFlow.IsMainFlow);
        parent.parent.document.getElementById('DialogUnderManagementPersonnel_IFrame').contentWindow.ManagerWorkFlow_onAfterUpdate(ObjManagerWorkFlow_ManagerWorkFlow.IsActiveFlow, ObjManagerWorkFlow_ManagerWorkFlow.IsMainFlow);
    }
}

function tlbItemExit_TlbManagersWorkFlow_onClick() {
    ShowDialogConfirm('Exit');
}

function CloseDialogManagersWorkFlow() {
    parent.document.getElementById('DialogManagersWorkFlow_IFrame').src = 'WhitePage.aspx';
    parent.DialogManagersWorkFlow.Close();
}

function rdbManagersSearch_ManagersWorkFlow_onClick() {
    CurrentFlowCreator_ManagersWorkFlow = 'Personnel';
    ShowDialogManagerSearch();
}

function ShowDialogManagerSearch() {
    DialogManagerSearch.Show();
    CollapseControls_ManagersWorkFlow();
}

function rdbPostsSearch_ManagersWorkFlow_onClick() {
    CurrentFlowCreator_ManagersWorkFlow = 'OrganizationPost';
    ShowDialogOrganizationPostSearch();
}

function ShowDialogOrganizationPostSearch() {
    DialogOrganizationPostSearch.Show();
    CollapseControls_ManagersWorkFlow();
}

function SetImage_clmnType_GridManagersWorkFlow_ManagersWorkFlow(type) {
    var nodeTypeImage = '';
    switch (type.toString()) {
        case '1':
            nodeTypeImage = 'Images/Grid/user.png';
            break;
        case '2':
            nodeTypeImage = 'Images/Grid/folder.gif';
            break;
    }
    return nodeTypeImage;
}

function Fill_GridManagersWorkFlow_ManagersWorkFlow() {
    document.getElementById('loadingPanel_GridManagersWorkFlow_ManagersWorkFlow').innerHTML = document.getElementById('hfloadingPanel_GridManagersWorkFlow_ManagersWorkFlow').value;
    var flowID = parent.DialogManagersWorkFlow.get_value().FlowID;
    CallBack_GridManagersWorkFlow_ManagersWorkFlow.callback(CharToKeyCode_ManagersWorkFlow(flowID));
}

function Refresh_GridManagersWorkFlow_ManagersWorkFlow() {
    Fill_GridManagersWorkFlow_ManagersWorkFlow();
}

function GridManagersWorkFlow_ManagersWorkFlow_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridManagersWorkFlow_ManagersWorkFlow').innerHTML = '';
}

function CallBack_GridManagersWorkFlow_ManagersWorkFlow_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ManagersWorkFlow').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridManagersWorkFlow_ManagersWorkFlow();
    }
}

function tlbItemUp_TlbInterAction_ManagersWorkFlow_onClick() {
    ChangeItemPriority_GridManagersWorkFlow_ManagersWorkFlow('Up');
}

function tlbItemDown_TlbInterAction_ManagersWorkFlow_onClick() {
    ChangeItemPriority_GridManagersWorkFlow_ManagersWorkFlow('Down');
}

function ChangeItemPriority_GridManagersWorkFlow_ManagersWorkFlow(state) {
    if (GridManagersWorkFlow_ManagersWorkFlow.getSelectedItems().length > 0) {
        var tempItem = null;
        var targetItem = null;
        var selectedItem = tempItem = GridManagersWorkFlow_ManagersWorkFlow.getSelectedItems()[0];
        switch (state) {
            case 'Up':
                if (selectedItem.get_index() > 0) {
                    targetItem = GridManagersWorkFlow_ManagersWorkFlow.get_table().getRow(selectedItem.get_index() - 1);
                }
                break;
            case 'Down':
                if (selectedItem.get_index() < 9)
                    targetItem = GridManagersWorkFlow_ManagersWorkFlow.get_table().getRow(selectedItem.get_index() + 1);
                break;
        }
        if (targetItem != null) {
            GridManagersWorkFlow_ManagersWorkFlow.beginUpdate();
            selectedItem.setValue(0, targetItem.getMember('ID').get_text(), false);
            selectedItem.setValue(1, targetItem.getMember('OwnerID').get_text(), false);
            selectedItem.setValue(2, targetItem.getMember('ManagerType').get_text(), false);
            selectedItem.setValue(3, targetItem.getMember('Name').get_text(), false);
            targetItem.setValue(0, tempItem.getMember('ID').get_text(), false);
            targetItem.setValue(1, tempItem.getMember('OwnerID').get_text(), false);
            targetItem.setValue(2, tempItem.getMember('ManagerType').get_text(), false);
            targetItem.setValue(3, tempItem.getMember('Name').get_text(), false);
            GridManagersWorkFlow_ManagersWorkFlow.endUpdate();
        }
    }
}

function tlbItemSave_tlbPersonnelSearch_onClick() {
    InsertFlowPart_ManagersWorkFlow();
    CloseDialogManagerSearch();
}

function InsertFlowPart_ManagersWorkFlow() {
    switch (CurrentFlowCreator_ManagersWorkFlow) {
        case 'Personnel':
            var selectedPersonnel_ManagersWorkFlow = cmbPersonnel_ManagersWorkFlow.getSelectedItem();
            if (selectedPersonnel_ManagersWorkFlow != undefined && selectedPersonnel_ManagersWorkFlow != null) {
                var personnelDetails = eval('(' + selectedPersonnel_ManagersWorkFlow.get_value() + ')');
                InsertFlowPartItem_GridManagersWorkFlow_ManagersWorkFlow('1', personnelDetails.ID.toString(), selectedPersonnel_ManagersWorkFlow.get_text());
            }
            break;
        case 'OrganizationPost':
            var selectedOrganizationPost_ManagersWorkFlow = null;
            var typeID = '0';
            var typeName = null;
            selectedOrganizationPost_ManagersWorkFlow = cmbPostSearchResult_ManagersWorkFlow.getSelectedItem();
            if (selectedOrganizationPost_ManagersWorkFlow != undefined && selectedOrganizationPost_ManagersWorkFlow != null) {
                var OrgPostDetails = eval('(' + selectedOrganizationPost_ManagersWorkFlow.get_value() + ')');
                typeID = OrgPostDetails.ID.toString();
                typeName = selectedOrganizationPost_ManagersWorkFlow.get_text();
            }
            else {
                selectedOrganizationPost_ManagersWorkFlow = trvOrganizationPosts_ManagersWorkFlow.get_selectedNode();
                if (selectedOrganizationPost_ManagersWorkFlow != undefined && selectedOrganizationPost_ManagersWorkFlow != null) {
                    typeID = selectedOrganizationPost_ManagersWorkFlow.get_id();
                    typeName = selectedOrganizationPost_ManagersWorkFlow.get_text();
                }
            }
            if (selectedOrganizationPost_ManagersWorkFlow != null)
                InsertFlowPartItem_GridManagersWorkFlow_ManagersWorkFlow('2', typeID, typeName);
            break;
    }
}

function DeleteFlowPart_ManagersWorkFlow() {
    if (GridManagersWorkFlow_ManagersWorkFlow.getSelectedItems().length > 0) {
        if (GridManagersWorkFlow_ManagersWorkFlow.get_recordCount() > 1)
            GridManagersWorkFlow_ManagersWorkFlow.deleteSelected();
        else
            showDialog(document.getElementById('hfRetErrorType_ManagersWorkFlow').value, document.getElementById('hfFlowPartsRange_ManagersWorkFlow').value, 'error');
    }
}

function InsertFlowPartItem_GridManagersWorkFlow_ManagersWorkFlow(type, typeID, typeName) {
    if (!CheckItemExisting_ManagersWorkFlow(typeID)) {
        if (GridManagersWorkFlow_ManagersWorkFlow.get_recordCount() < 10) {
            GridManagersWorkFlow_ManagersWorkFlow.beginUpdate();
            var rowID = Math.random().toString();
            newFPItem = GridManagersWorkFlow_ManagersWorkFlow.get_table().addEmptyRow(GridManagersWorkFlow_ManagersWorkFlow.get_recordCount());
            newFPItem.setValue(0, rowID, false);
            newFPItem.setValue(1, typeID, false);
            newFPItem.setValue(2, type, false);
            newFPItem.setValue(3, typeName, false);
            GridManagersWorkFlow_ManagersWorkFlow.selectByKey(rowID, 0, false);
            GridManagersWorkFlow_ManagersWorkFlow.endUpdate();
        }
        else
            showDialog(document.getElementById('hfRetErrorType_ManagersWorkFlow').value, document.getElementById('hfFlowPartsRange_ManagersWorkFlow').value, 'error');
    }
}

function CheckItemExisting_ManagersWorkFlow(selectedItemID) {
    var IsExisting = false;
    var type = null;
    switch (CurrentFlowCreator_ManagersWorkFlow) {
        case 'Personnel':
            type = 1;
            break;
        case 'OrganizationPost':
            type = 2;
            break;
    }
    for (var i = 0; i < GridManagersWorkFlow_ManagersWorkFlow.get_recordCount() ; i++) {
        var selectedFlowPartItem = GridManagersWorkFlow_ManagersWorkFlow.get_table().getRow(i);
        if (selectedFlowPartItem.getMember('OwnerID').get_text() == selectedItemID && selectedFlowPartItem.getMember('ManagerType').get_text() == type) {
            IsExisting = true;
            break;
        }
    }
    return IsExisting;
}

function tlbItemCancel_tlbPersonnelSearch_onClick() {
    CloseDialogManagerSearch();
}

function CloseDialogManagerSearch() {
    ClearManagerDetails_ManagersWorkFlow();
    DialogManagerSearch.Close();
    ChangePageState_ManagersWorkFlow('View');
    CollapseControls_ManagersWorkFlow();
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_ManagersWorkFlow_onClick() {
    Refresh_cmbPersonnel_ManagersWorkFlow();
    ClearManagerDetails_ManagersWorkFlow();
}

function Refresh_cmbPersonnel_ManagersWorkFlow() {
    LoadState_cmbPersonnel_ManagersWorkFlow = 'Normal';
    SetPageIndex_cmbPersonnel_ManagersWorkFlow(0);
}

function ClearManagerDetails_ManagersWorkFlow() {
    document.getElementById('txtPersonnelSearch_ManagersWorkFlow').value = '';
    document.getElementById('cmbPersonnel_ManagersWorkFlow_Input').value = '';
    cmbPersonnel_ManagersWorkFlow.unSelect();
}


function tlbItemFirst_TlbPaging_PersonnelSearch_ManagersWorkFlow_onClick() {
    SetPageIndex_cmbPersonnel_ManagersWorkFlow(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_ManagersWorkFlow_onClick() {
    if (CurrentPageIndex_cmbPersonnel_ManagersWorkFlow != 0) {
        CurrentPageIndex_cmbPersonnel_ManagersWorkFlow = CurrentPageIndex_cmbPersonnel_ManagersWorkFlow - 1;
        SetPageIndex_cmbPersonnel_ManagersWorkFlow(CurrentPageIndex_cmbPersonnel_ManagersWorkFlow);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_ManagersWorkFlow_onClick() {
    if (CurrentPageIndex_cmbPersonnel_ManagersWorkFlow < parseInt(document.getElementById('hfPersonnelPageCount_ManagersWorkFlow').value) - 1) {
        CurrentPageIndex_cmbPersonnel_ManagersWorkFlow = CurrentPageIndex_cmbPersonnel_ManagersWorkFlow + 1;
        SetPageIndex_cmbPersonnel_ManagersWorkFlow(CurrentPageIndex_cmbPersonnel_ManagersWorkFlow);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_ManagersWorkFlow_onClick() {
    SetPageIndex_cmbPersonnel_ManagersWorkFlow(parseInt(document.getElementById('hfPersonnelPageCount_ManagersWorkFlow').value) - 1);
}

function cmbPersonnel_ManagersWorkFlow_onExpand(sender, e) {
    SetPosition_cmbPersonnel_ManagersWorkFlow();
    if (cmbPersonnel_ManagersWorkFlow.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_ManagersWorkFlow == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_ManagersWorkFlow = true;
        SetPageIndex_cmbPersonnel_ManagersWorkFlow(0);
    }
}

function CallBack_cmbPersonnel_ManagersWorkFlow_onBeforeCallback(sender, e) {
    cmbPersonnel_ManagersWorkFlow.dispose();
}

function CallBack_cmbPersonnel_ManagersWorkFlow_onCallbackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_ManagersWorkFlow').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_ManagersWorkFlow').value;
    document.getElementById('clmnBarCode_cmbPersonnel_ManagersWorkFlow').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_ManagersWorkFlow').value;
    document.getElementById('clmnCardNum_cmbPersonnel_ManagersWorkFlow').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_ManagersWorkFlow').value;

    SetPosition_cmbPersonnel_ManagersWorkFlow();

    var error = document.getElementById('ErrorHiddenField_Personnel_ManagersWorkFlow').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_ManagersWorkFlow_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_ManagersWorkFlow_DropImage').mousedown();
        else
            cmbPersonnel_ManagersWorkFlow.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_ManagersWorkFlow_DropDown').style.display = 'none';
        CloseDialogManagerSearch();
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function SetPageIndex_cmbPersonnel_ManagersWorkFlow(pageIndex) {
    CurrentPageIndex_cmbPersonnel_ManagersWorkFlow = pageIndex;
    Fill_cmbPersonnel_ManagersWorkFlow(pageIndex);
}

function Fill_cmbPersonnel_ManagersWorkFlow(pageIndex) {
    var SearchTerm = '';
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_ManagersWorkFlow').value);
    switch (LoadState_cmbPersonnel_ManagersWorkFlow) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm = document.getElementById('txtPersonnelSearch_ManagersWorkFlow').value;
            break;
        case 'AdvancedSearch':
            SearchTerm = AdvancedSearchTerm_cmbPersonnel_ManagersWorkFlow;
            break;
    }
    CallBack_cmbPersonnel_ManagersWorkFlow.callback(CharToKeyCode_ManagersWorkFlow(LoadState_cmbPersonnel_ManagersWorkFlow), CharToKeyCode_ManagersWorkFlow(pageSize.toString()), CharToKeyCode_ManagersWorkFlow(pageIndex.toString()), CharToKeyCode_ManagersWorkFlow(SearchTerm));
}

function CharToKeyCode_ManagersWorkFlow(str) {
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

function tlbItemSearch_TlbSearchPersonnel_ManagersWorkFlow_onClick() {
    LoadState_cmbPersonnel_ManagersWorkFlow = 'Search';
    SetPageIndex_cmbPersonnel_ManagersWorkFlow(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_ManagersWorkFlow_onClick() {
    LoadState_cmbPersonnel_ManagersWorkFlow = 'AdvancedSearch';
    ShowDialogPersonnelSearch();
}

function ShowDialogPersonnelSearch() {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = 'ManagersWorkFlow';
    parent.parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.parent.DialogPersonnelSearch.Show();
}

function tlbItemSave_TlbOrganizationPostSearch_onClick() {
    InsertFlowPart_ManagersWorkFlow();
    CloseDialogOrganizationPostSearch();
}

function tlbItemCancel_TlbOrganizationPostSearch_onClick() {
    CloseDialogOrganizationPostSearch();
}

function CloseDialogOrganizationPostSearch() {
    ClearOrganizationPostDetails_ManagersWorkFlow();
    DialogOrganizationPostSearch.Close();
    ChangePageState_ManagersWorkFlow('View');
    CollapseControls_ManagersWorkFlow();
}

function Refresh_trvOrganizationPosts_ManagersWorkFlow() {
    Fill_trvOrganizationPosts_ManagersWorkFlow(true);
}

function Fill_trvOrganizationPosts_ManagersWorkFlow(isRefresh) {
    if (trvOrganizationPosts_ManagersWorkFlow.get_nodes().get_length() == 0 || isRefresh) {
        document.getElementById('loadingPanel_trvOrganizationPosts_ManagersWorkFlow').innerHTML = document.getElementById('hfloadingPanel_trvOrganizationPosts_ManagersWorkFlow').value;
        ObjexpandingOrgPostNode_ManagersWorkFlow = null;
        CallBack_trvOrganizationPosts_ManagersWorkFlow.callback();
    }
}

function trvOrganizationPosts_ManagersWorkFlow_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvOrganizationPosts_ManagersWorkFlow').innerHTML = '';
}

function trvOrganizationPosts_ManagersWorkFlow_onCallbackComplete(sender, e) {
    if (ObjexpandingOrgPostNode_ManagersWorkFlow != null) {
        if (ObjexpandingOrgPostNode_ManagersWorkFlow.Node.get_nodes().get_length() == 0 && ObjexpandingOrgPostNode_ManagersWorkFlow.HasChild) {
            ObjexpandingOrgPostNode_ManagersWorkFlow = null;
            GetLoadonDemandError_OrganizationPosts_ManagersWorkFlowPage();
        }
        else
            ObjexpandingOrgPostNode_ManagersWorkFlow = null;
    }
}

function GetLoadonDemandError_OrganizationPosts_ManagersWorkFlowPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function trvOrganizationPosts_ManagersWorkFlow_onNodeBeforeExpand(sender, e) {
    if (ObjexpandingOrgPostNode_ManagersWorkFlow != null)
        ObjexpandingOrgPostNode_ManagersWorkFlow = null;
    ObjexpandingOrgPostNode_ManagersWorkFlow = new Object();
    ObjexpandingOrgPostNode_ManagersWorkFlow.Node = e.get_node();
    if (e.get_node().get_nodes().get_length() == 1 && (e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined || e.get_node().get_nodes().get_nodeArray()[0].get_id() == '')) {
        ObjexpandingOrgPostNode_ManagersWorkFlow.HasChild = true;
        trvOrganizationPosts_ManagersWorkFlow.beginUpdate();
        ObjexpandingOrgPostNode_ManagersWorkFlow.Node.get_nodes().remove(0);
        trvOrganizationPosts_ManagersWorkFlow.endUpdate();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            ObjexpandingOrgPostNode_ManagersWorkFlow.HasChild = false;
        else
            ObjexpandingOrgPostNode_ManagersWorkFlow.HasChild = true;
    }
}

function CallBack_trvOrganizationPosts_ManagersWorkFlow_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_OrganizationPosts_ManagersWorkFlow').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvOrganizationPosts_ManagersWorkFlow(false);
        CloseDialogOrganizationPostSearch();
    }
}

function tlbItemPostSearch_TlbPostSearch_ManagersWorkFlow_onClick() {
    Fill_cmbPostSearchResult_ManagersWorkFlow();
}

function Fill_cmbPostSearchResult_ManagersWorkFlow() {
    ClearOrganizationPostDetails_ManagersWorkFlow();
    var SearchTerm = document.getElementById('txtSearchTerm_ManagersWorkFlow').value;
    CallBack_cmbPostSearchResult_ManagersWorkFlow.callback(CharToKeyCode_ManagersWorkFlow(SearchTerm));
}

function ClearOrganizationPostDetails_ManagersWorkFlow() {
    document.getElementById('txtPersonnel_ManagersWorkFlow').value = '';
    document.getElementById('cmbPostSearchResult_ManagersWorkFlow_Input').value = '';
    cmbPostSearchResult_ManagersWorkFlow.unSelect();
}


function CallBack_cmbPostSearchResult_ManagersWorkFlow_onBeforeCallback(sender, e) {
    cmbPostSearchResult_ManagersWorkFlow.dispose();
}

function CallBack_cmbPostSearchResult_ManagersWorkFlow_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_PostSearchResult_ManagersWorkFlow').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_cmbPostSearchResult_ManagersWorkFlow();
        CloseDialogOrganizationPostSearch();
    }
    else
        cmbPostSearchResult_ManagersWorkFlow.expand();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_ManagersWorkFlow = confirmState;
    if (CurrentPageState_ManagersWorkFlow == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_ManagersWorkFlow').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_ManagersWorkFlow').value;
    DialogConfirm.Show();
    CollapseControls_ManagersWorkFlow();
}


function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_ManagersWorkFlow) {
        case 'Delete':
            DialogConfirm.Close();
            DeleteFlowPart_ManagersWorkFlow();
            break;
        case 'Exit':
            CloseDialogManagersWorkFlow();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    CurrentPageState_ManagersWorkFlow = 'View';
}

function CallBack_GridManagersWorkFlow_ManagersWorkFlow_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridManagersWorkFlow_ManagersWorkFlow').innerHTML = '';
    ShowConnectionError_ManagersWorkFlow();
}

function CallBack_cmbPersonnel_ManagersWorkFlow_onCallbackError(sender, e) {
    ShowConnectionError_ManagersWorkFlow();
}

function CallBack_trvOrganizationPosts_ManagersWorkFlow_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvOrganizationPosts_ManagersWorkFlow').innerHTML = '';
    ShowConnectionError_ManagersWorkFlow();
}

function CallBack_cmbPostSearchResult_ManagersWorkFlow_onCallbackError(sender, e) {
    ShowConnectionError_ManagersWorkFlow();
}

function ShowConnectionError_ManagersWorkFlow() {
    var error = document.getElementById('hfErrorType_ManagersWorkFlow').value;
    var errorBody = document.getElementById('hfConnectionError_ManagersWorkFlow').value;
    showDialog(error, errorBody, 'error');
}

function CollapseControls_ManagersWorkFlow() {
    cmbPersonnel_ManagersWorkFlow.collapse();
    cmbPostSearchResult_ManagersWorkFlow.collapse();
}

function tlbItemFormReconstruction_TlbManagersWorkFlow_onClick() {
    CloseDialogManagersWorkFlow();
    parent.parent.document.getElementById('DialogUnderManagementPersonnel_IFrame').contentWindow.ShowDialogManagersWorkFlow();
}

function tlbItemHelp_TlbManagersWorkFlow_onClick() {
    LoadHelpPage('tlbItemHelp_TlbManagersWorkFlow');
}

function SetPosition_cmbPersonnel_ManagersWorkFlow() {
    if (parent.parent.CurrentLangID == 'fa-IR') {
        document.getElementById('cmbPersonnel_ManagersWorkFlow_DropDown').style.left = document.getElementById('Mastertbl_ManagersWorkFlow').clientWidth - 400 + 'px';
    }
    if (parent.CurrentLangID == 'en-US') {
        document.getElementById('cmbPersonnel_ManagersWorkFlow_DropDown').style.left = '30px';
    }
}

function ManagersWorkFlow_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_cmbPersonnel_ManagersWorkFlow = SearchTerm;
    SetPageIndex_cmbPersonnel_ManagersWorkFlow(0);
}













