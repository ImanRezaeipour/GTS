
var CurrentPageIndex_cmbPersonnel_UnderManagementPersonnel = 0;
var CurrentPageCombosCallBcakStateObj = new Object();
var LoadState_cmbPersonnel_UnderManagementPersonnel = 'Normal';
var ObjexpandingOrgPostNode_UnderManagementPersonnel = null;
var ObjexpandingDepPersonnelNode_UnderManagementPersonnel = null;
var CurrentPageState_UnderManagementPersonnel = 'View';
var box_ManagersSearch_UnderManagementPersonnel_IsShown = false;
var CurrentManagerCreator_UnderManagementPersonnel = null;
var box_postsSearch_UnderManagementPersonnel_IsShown = false;
var ObjUnderManagement_UnderManagementPersonnel = null;
var AdvancedSearchTerm_cmbPersonnel_UnderManagementPersonnel = '';


function GetBoxesHeaders_UnderManagementPersonnel() {
    parent.document.getElementById('Title_DialogUnderManagementPersonnel').innerHTML = document.getElementById('hfTitle_DialogUnderManagementPersonnel').value;
    document.getElementById('header_ManagerFeturesBox_UnderManagementPersonnel').innerHTML = document.getElementById('hfheader_ManagerFeturesBox_UnderManagementPersonnel').value;
    document.getElementById('header_OrganizationPersonnelBox_UnderManagementPersonnel').innerHTML = document.getElementById('hfheader_OrganizationPersonnelBox_UnderManagementPersonnel').value;
    document.getElementById('header_UnderManagementPersonnelBox_UnderManagementPersonnel').innerHTML = document.getElementById('hfheader_UnderManagementPersonnelBox_UnderManagementPersonnel').value;
    document.getElementById('header_OrganizationPosts_UnderManagementPersonnel').innerHTML = document.getElementById('hfheader_OrganizationPosts_UnderManagementPersonnel').value;
    document.getElementById('header_OrganizationPostSearch_UnderManagementPersonnel').innerHTML = document.getElementById('hfheader_OrganizationPostSearch_UnderManagementPersonnel').value;
    document.getElementById('clmnName_cmbPersonnel_UnderManagementPersonnel').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_UnderManagementPersonnel').value;
    document.getElementById('clmnBarCode_cmbPersonnel_UnderManagementPersonnel').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_UnderManagementPersonnel').value;
    document.getElementById('clmnCardNum_cmbPersonnel_UnderManagementPersonnel').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_UnderManagementPersonnel').value;
    document.getElementById('cmbAccessGroup_UnderManagementPersonnel_TextBox').innerHTML = document.getElementById('hfcmbAlarm_UnderManagementPersonnel').value;
}

function SetPosition_DropDownDives_UnderManagementPersonnel() {
    if (CurrentPageState_UnderManagementPersonnel == 'Add') {
        if (parent.CurrentLangID == 'fa-IR') {
            document.getElementById('box_ManagersSearch_UnderManagementPersonnel').style.right = '30px';
            document.getElementById('box_postsSearch_UnderManagementPersonnel').style.right = '440px';
        }
        if (parent.CurrentLangID == 'en-US') {
            document.getElementById('box_ManagersSearch_UnderManagementPersonnel').style.left = '30px';
            document.getElementById('box_postsSearch_UnderManagementPersonnel').style.left = '440px';
        }
    }
}

function rdbManagersSearch_UnderManagementPersonnel_onClick() {
    CollapseControls_UnderManagementPersonnel();
    CurrentManagerCreator_UnderManagementPersonnel = 'Personnel';
    setSlideDownSpeed(200);
    if (box_postsSearch_UnderManagementPersonnel_IsShown)
        rdbPostsSearch_UnderManagementPersonnel_onClick();
    slidedown_showHide('box_ManagersSearch_UnderManagementPersonnel');

    if (box_ManagersSearch_UnderManagementPersonnel_IsShown) {
        box_ManagersSearch_UnderManagementPersonnel_IsShown = false;
        document.getElementById('rdbManagersSearch_UnderManagementPersonnel').checked = false;
    }
    else
        box_ManagersSearch_UnderManagementPersonnel_IsShown = true;
}

function rdbPostsSearch_UnderManagementPersonnel_onClick() {
    CollapseControls_UnderManagementPersonnel();
    CurrentManagerCreator_UnderManagementPersonnel = "OrganizationPost";
    setSlideDownSpeed(200);
    if (box_ManagersSearch_UnderManagementPersonnel_IsShown)
        rdbManagersSearch_UnderManagementPersonnel_onClick();
    slidedown_showHide_surplus('box_postsSearch_UnderManagementPersonnel');

    if (box_postsSearch_UnderManagementPersonnel_IsShown) {
        box_postsSearch_UnderManagementPersonnel_IsShown = false;
        document.getElementById('rdbPostsSearch_UnderManagementPersonnel').checked = false;
    }
    else
        box_postsSearch_UnderManagementPersonnel_IsShown = true;

    Fill_trvOrganizationPosts_UnderManagementPersonnel(false);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_UnderManagementPersonnel_onClick() {
    LoadState_cmbPersonnel_UnderManagementPersonnel = 'AdvancedSearch';
    CurrentPageIndex_cmbPersonnel_UnderManagementPersonnel = 0;
    ShowDialogPersonnelSearch('UnderManagementPersonnel');
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
    CollapseControls_UnderManagementPersonnel();
}

function ShowDialogUnderManagementPersonnelExeptionAccessView() {
    DialogUnderManagementPersonnelExeptionAccessView.Show();
    CollapseControls_UnderManagementPersonnel();
}

function ShowDialogAccessGroups() {
    var ObjDialogUnderManagementPersonnel = parent.DialogUnderManagementPersonnel.get_value();
    var ObjDialogAccessGroups = new Object();
    ObjDialogAccessGroups.FlowState = ObjDialogUnderManagementPersonnel.PageState;
    DialogAccessGroups.set_value(ObjDialogAccessGroups);
    DialogAccessGroups.Show();
    CollapseControls_UnderManagementPersonnel();
}

function ShowDialogManagersWorkFlow() {
    var flowID = null;
    var obj = parent.DialogUnderManagementPersonnel.get_value();
    if (ObjUnderManagement_UnderManagementPersonnel == null) {
        ObjUnderManagement_UnderManagementPersonnel = new Object();
        flowID = ObjUnderManagement_UnderManagementPersonnel.FlowID = obj.FlowID;
        ObjUnderManagement_UnderManagementPersonnel.IsActiveFlow = obj.IsActiveFlow;
        ObjUnderManagement_UnderManagementPersonnel.IsMainFlow = obj.IsMainFlow;
    }
    else {
        flowID = ObjUnderManagement_UnderManagementPersonnel.FlowID;
        var IsActiveFlow = false;
        if (obj.IsActiveFlow != undefined)
            IsActiveFlow = obj.IsActiveFlow;
        else
            IsActiveFlow = true;
        ObjUnderManagement_UnderManagementPersonnel.IsActiveFlow = IsActiveFlow;
        var IsMainFlow = false;
        if (obj.IsMainFlow != undefined)
            IsMainFlow = obj.IsMainFlow;
        else
            IsMainFlow = true;
        ObjUnderManagement_UnderManagementPersonnel.IsMainFlow = IsMainFlow;
    }
    ObjUnderManagement_UnderManagementPersonnel.FlowState = obj.PageState;

    if (flowID != null && flowID != undefined && flowID != '0') {
        DialogManagersWorkFlow.set_value(ObjUnderManagement_UnderManagementPersonnel);
        DialogManagersWorkFlow.Show();
    }
    CollapseControls_UnderManagementPersonnel();
}

function ManagerWorkFlow_onAfterUpdate(IsActiveFlow, IsMainFlow) {
    var obj = parent.DialogUnderManagementPersonnel.get_value();
    if (obj.IsActiveFlow != undefined)
        obj.IsActiveFlow = IsActiveFlow;
    if (obj.IsMainFlow != undefined)
        obj.IsMainFlow = IsMainFlow;

}

function ShowDialogUnderManagementPersonnelExeptionAccessCreation() {
    DialogUnderManagementPersonnelExeptionAccessCreation.Show();
    CollapseControls_UnderManagementPersonnel();
}

function tlbItemSearch_TlbSearchPersonnel_UnderManagementPersonnel_onClick() {
    LoadState_cmbPersonnel_UnderManagementPersonnel = 'Search';
    CurrentPageIndex_cmbPersonnel_UnderManagementPersonnel = 0;
    SetPageIndex_cmbPersonnel_UnderManagementPersonnel(0);
}

function tlbItemAccessGroup_TlbAccessGroup_UnderManagementPersonnel_onClick() {
    ShowDialogAccessGroups();
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_UnderManagementPersonnel_onClick() {
    Refresh_cmbPersonnel_UnderManagementPersonnel();
    ClearManagerDetails_MasterManagers();
}

function ClearManagerDetails_MasterManagers() {
    document.getElementById('txtPersonnelSearch_UnderManagementPersonnel').value = '';
    document.getElementById('cmbPersonnel_UnderManagementPersonnel_Input').value = '';
    cmbPersonnel_UnderManagementPersonnel.unSelect();
}

function Refresh_cmbPersonnel_UnderManagementPersonnel() {
    LoadState_cmbPersonnel_UnderManagementPersonnel = 'Normal';
    SetPageIndex_cmbPersonnel_UnderManagementPersonnel(0);
}

function tlbItemFirst_TlbPaging_PersonnelSearch_UnderManagementPersonnel_onClick() {
    SetPageIndex_cmbPersonnel_UnderManagementPersonnel(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_UnderManagementPersonnel_onClick() {
    if (CurrentPageIndex_cmbPersonnel_UnderManagementPersonnel != 0) {
        CurrentPageIndex_cmbPersonnel_UnderManagementPersonnel = CurrentPageIndex_cmbPersonnel_UnderManagementPersonnel - 1;
        SetPageIndex_cmbPersonnel_UnderManagementPersonnel(CurrentPageIndex_cmbPersonnel_UnderManagementPersonnel);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_UnderManagementPersonnel_onClick() {
    if (CurrentPageIndex_cmbPersonnel_UnderManagementPersonnel < parseInt(document.getElementById('hfPersonnelPageCount_UnderManagementPersonnel').value) - 1) {
        CurrentPageIndex_cmbPersonnel_UnderManagementPersonnel = CurrentPageIndex_cmbPersonnel_UnderManagementPersonnel + 1;
        SetPageIndex_cmbPersonnel_UnderManagementPersonnel(CurrentPageIndex_cmbPersonnel_UnderManagementPersonnel);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_UnderManagementPersonnel_onClick() {
    SetPageIndex_cmbPersonnel_UnderManagementPersonnel(parseInt(document.getElementById('hfPersonnelPageCount_UnderManagementPersonnel').value) - 1);
}

function SetPageIndex_cmbPersonnel_UnderManagementPersonnel(pageIndex) {
    CurrentPageIndex_cmbPersonnel_UnderManagementPersonnel = pageIndex;
    Fill_cmbPersonnel_UnderManagementPersonnel(pageIndex);
}

function Fill_cmbPersonnel_UnderManagementPersonnel(pageIndex) {
    var SearchTerm = '';
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_UnderManagementPersonnel').value);
    switch (LoadState_cmbPersonnel_UnderManagementPersonnel) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm = document.getElementById('txtPersonnelSearch_UnderManagementPersonnel').value;
            break;
        case 'AdvancedSearch':
            SearchTerm = AdvancedSearchTerm_cmbPersonnel_UnderManagementPersonnel;
            break;
    }
    CallBack_cmbPersonnel_UnderManagementPersonnel.callback(CharToKeyCode_UnderManagementPersonnel(LoadState_cmbPersonnel_UnderManagementPersonnel), CharToKeyCode_UnderManagementPersonnel(pageSize.toString()), CharToKeyCode_UnderManagementPersonnel(pageIndex.toString()), CharToKeyCode_UnderManagementPersonnel(SearchTerm));
}

function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDilaogUnderManagementPersonnel();
}

function CloseDilaogUnderManagementPersonnel() {
    parent.document.getElementById('DialogUnderManagementPersonnel_IFrame').src = 'WhitePage.aspx';
    parent.DialogUnderManagementPersonnel.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function cmbPersonnel_UnderManagementPersonnel_onExpand(sender, e) {
    SetPosition_cmbPersonnel_UnderManagementPersonnel();
    if (cmbPersonnel_UnderManagementPersonnel.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_UnderManagementPersonnel == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_UnderManagementPersonnel = true;
        SetPageIndex_cmbPersonnel_UnderManagementPersonnel(0);
    }
}

function cmbPersonnel_UnderManagementPersonnel_onChange(sender, e) {
    NavigateManagerDetails_UnderManagementPersonnel();
}

function NavigateManagerDetails_UnderManagementPersonnel() {
    var selectedItem_cmbPersonnel_UnderManagementPersonnel = cmbPersonnel_UnderManagementPersonnel.getSelectedItem();
    if (selectedItem_cmbPersonnel_UnderManagementPersonnel != undefined && selectedItem_cmbPersonnel_UnderManagementPersonnel != null) {
        document.getElementById('txtManagerName_UnderManagementPersonnel').value = selectedItem_cmbPersonnel_UnderManagementPersonnel.get_text();
        document.getElementById('txtManagerBarcode_UnderManagementPersonnel').value = selectedItem_cmbPersonnel_UnderManagementPersonnel.BarCode;
        var personnelDetails = selectedItem_cmbPersonnel_UnderManagementPersonnel.get_value();
        if (personnelDetails != "") {
            personnelDetails = eval('(' + personnelDetails + ')');
            personnelDetails.OrganizationPostName != null ? document.getElementById('txtManagerOrganizationPost_UnderManagementPersonnel').value = document.getElementById('txtOrganizationPost_UnderManagementPersonnel').value = personnelDetails.OrganizationPostName : document.getElementById('txtManagerOrganizationPost_UnderManagementPersonnel').value = document.getElementById('txtOrganizationPost_UnderManagementPersonnel').value = '';
            personnelDetails.RoleName != null ? document.getElementById('txtManagerOrganizationRole_UnderManagementPersonnel').value = personnelDetails.RoleName : document.getElementById('txtManagerOrganizationRole_UnderManagementPersonnel').value = '';
        }
    }
}

function CallBack_cmbPersonnel_UnderManagementPersonnel_onBeforeCallback(sender, e) {
    cmbPersonnel_UnderManagementPersonnel.dispose();
}

function CallBack_cmbPersonnel_UnderManagementPersonnel_onCallbackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_UnderManagementPersonnel').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_UnderManagementPersonnel').value;
    document.getElementById('clmnBarCode_cmbPersonnel_UnderManagementPersonnel').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_UnderManagementPersonnel').value;
    document.getElementById('clmnCardNum_cmbPersonnel_UnderManagementPersonnel').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_UnderManagementPersonnel').value;

    SetPosition_cmbPersonnel_UnderManagementPersonnel();

    var error = document.getElementById('ErrorHiddenField_Personnel_UnderManagementPersonnel').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_UnderManagementPersonnel_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_UnderManagementPersonnel_DropImage').mousedown();
        else
            cmbPersonnel_UnderManagementPersonnel.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_UnderManagementPersonnel_DropDown').style.display = 'none';
    }
}

function SetPosition_cmbPersonnel_UnderManagementPersonnel() {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('cmbPersonnel_UnderManagementPersonnel_DropDown').style.left = document.getElementById('Mastertbl_UnderManagementPersonnel').clientWidth - 400 + 'px';
    }
    if (parent.CurrentLangID == 'en-US') {
        document.getElementById('cmbPersonnel_UnderManagementPersonnel_DropDown').style.left = '30px';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CharToKeyCode_UnderManagementPersonnel(str) {
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

function tlbItemExit_TlbUnderManagementPersonnel_onClick() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_UnderManagementPersonnel').value;
    DialogConfirm.Show();
    CollapseControls_UnderManagementPersonnel();
}

function Fill_trvOrganizationPosts_UnderManagementPersonnel(isRefresh) {
    if (trvOrganizationPosts_UnderManagementPersonnel.get_nodes().get_length() == 0 || isRefresh) {
        document.getElementById('loadingPanel_trvOrganizationPosts_UnderManagementPersonnel').innerHTML = document.getElementById('hfloadingPanel_trvOrganizationPosts_UnderManagementPersonnel').value;
        ObjexpandingOrgPostNode_UnderManagementPersonnel = null;
        CallBack_trvOrganizationPosts_UnderManagementPersonnel.callback();
    }
}

function trvOrganizationPosts_UnderManagementPersonnel_onNodeSelect(sender, e) {
    NavigateOrganizationPost_UnderManagementPersonnel(e.get_node());
}

function NavigateOrganizationPost_UnderManagementPersonnel(selectedOrgPostNode) {
    document.getElementById('txtManagerOrganizationPost_UnderManagementPersonnel').value = selectedOrgPostNode.get_text();
    document.getElementById('txtManagerName_UnderManagementPersonnel').value = '';
    document.getElementById('txtManagerBarcode_UnderManagementPersonnel').value = '';
    document.getElementById('txtManagerOrganizationRole_UnderManagementPersonnel').value = '';
}

function trvOrganizationPosts_UnderManagementPersonnel_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvOrganizationPosts_UnderManagementPersonnel').innerHTML = '';
}

function trvOrganizationPosts_UnderManagementPersonnel_onCallbackComplete(sender, e) {
    if (ObjexpandingOrgPostNode_UnderManagementPersonnel != null) {
        if (ObjexpandingOrgPostNode_UnderManagementPersonnel.Node.get_nodes().get_length() == 0 && ObjexpandingOrgPostNode_UnderManagementPersonnel.HasChild) {
            ObjexpandingOrgPostNode_UnderManagementPersonnel = null;
            GetLoadonDemandError_OrganizationPosts_UnderManagementPersonnelPage();
        }
        else
            ObjexpandingOrgPostNode_UnderManagementPersonnel = null;
    }
}

function trvOrganizationPosts_UnderManagementPersonnel_onNodeBeforeExpand(sender, e) {
    if (ObjexpandingOrgPostNode_UnderManagementPersonnel != null)
        ObjexpandingOrgPostNode_UnderManagementPersonnel = null;
    ObjexpandingOrgPostNode_UnderManagementPersonnel = new Object();
    ObjexpandingOrgPostNode_UnderManagementPersonnel.Node = e.get_node();
    if (e.get_node().get_nodes().get_length() == 1 && (e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined || e.get_node().get_nodes().get_nodeArray()[0].get_id() == '')) {
        ObjexpandingOrgPostNode_UnderManagementPersonnel.HasChild = true;
        trvOrganizationPosts_UnderManagementPersonnel.beginUpdate();
        ObjexpandingOrgPostNode_UnderManagementPersonnel.Node.get_nodes().remove(0);
        trvOrganizationPosts_UnderManagementPersonnel.endUpdate();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            ObjexpandingOrgPostNode_UnderManagementPersonnel.HasChild = false;
        else
            ObjexpandingOrgPostNode_UnderManagementPersonnel.HasChild = true;
    }
}

function CallBack_trvOrganizationPosts_UnderManagementPersonnel_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_OrganizationPosts_UnderManagementPersonnel').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvOrganizationPosts_UnderManagementPersonnel(false);
    }
}

function Refresh_trvOrganizationPosts_UnderManagementPersonnel() {
    Fill_trvOrganizationPosts_UnderManagementPersonnel(true)
}

function GetLoadonDemandError_OrganizationPosts_UnderManagementPersonnelPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function tlbItemPostSearch_TlbPostSearch_UnderManagementPersonnel_onClick() {
    Fill_cmbPostSearchResult_UnderManagementPersonnel();
}

function Fill_cmbPostSearchResult_UnderManagementPersonnel() {
    ClearOrganizationPostDetails_UnderManagementPersonnel();
    var SearchTerm = document.getElementById('txtSearchTerm_UnderManagementPersonnel').value;
    CallBack_cmbPostSearchResult_UnderManagementPersonnel.callback(CharToKeyCode_UnderManagementPersonnel(SearchTerm));
}

function cmbPostSearchResult_UnderManagementPersonnel_onChange(sender, e) {
    NavigateOrganizationPostDetails_UnderManagementPersonnel();
}

function NavigateOrganizationPostDetails_UnderManagementPersonnel() {
    var selectedItem_cmbPostSearchResult_UnderManagementPersonnel = cmbPostSearchResult_UnderManagementPersonnel.getSelectedItem();
    if (selectedItem_cmbPostSearchResult_UnderManagementPersonnel != undefined && selectedItem_cmbPostSearchResult_UnderManagementPersonnel != null) {
        document.getElementById('txtManagerOrganizationPost_UnderManagementPersonnel').value = selectedItem_cmbPostSearchResult_UnderManagementPersonnel.get_text();
        var organizationPostDetails = selectedItem_cmbPostSearchResult_UnderManagementPersonnel.get_value();
        if (organizationPostDetails != "") {
            organizationPostDetails = eval('(' + organizationPostDetails + ')');
            organizationPostDetails.PersonnelBarCode != null ? document.getElementById('txtManagerBarcode_UnderManagementPersonnel').value = organizationPostDetails.PersonnelBarCode : document.getElementById('txtManagerBarcode_UnderManagementPersonnel').value = '';
            organizationPostDetails.PersonnelName != null ? document.getElementById('txtManagerName_UnderManagementPersonnel').value = document.getElementById('txtPersonnel_UnderManagementPersonnel').value = organizationPostDetails.PersonnelName : document.getElementById('txtManagerName_UnderManagementPersonnel').value = document.getElementById('txtPersonnel_UnderManagementPersonnel').value = '';
            organizationPostDetails.RoleName != null ? document.getElementById('txtManagerOrganizationRole_UnderManagementPersonnel').value = organizationPostDetails.RoleName : document.getElementById('txtManagerOrganizationRole_UnderManagementPersonnel').value = '';
        }
    }
}

function ClearOrganizationPostDetails_UnderManagementPersonnel() {
    document.getElementById('txtPersonnel_UnderManagementPersonnel').value = '';
    document.getElementById('cmbPostSearchResult_UnderManagementPersonnel_Input').value = '';
    cmbPostSearchResult_UnderManagementPersonnel.unSelect();
}

function CallBack_cmbPostSearchResult_UnderManagementPersonnel_onBeforeCallback(sender, e) {
    cmbPostSearchResult_UnderManagementPersonnel.dispose();
}

function CallBack_cmbPostSearchResult_UnderManagementPersonnel_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_PostSearchResult_UnderManagementPersonnel').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_cmbPostSearchResult_UnderManagementPersonnel();
    }
    else
        cmbPostSearchResult_UnderManagementPersonnel.expand();
}

function cmbAccessGroup_UnderManagementPersonnel_onExpand(sender, e) {
    if (cmbAccessGroup_UnderManagementPersonnel.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbAccessGroup_UnderManagementPersonnel == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbAccessGroup_UnderManagementPersonnel = true;
        Fill_cmbAccessGroup_UnderManagementPersonnel();
    }
}

function Fill_cmbAccessGroup_UnderManagementPersonnel() {
    CallBack_cmbAccessGroup_UnderManagementPersonnel.callback()
}

function cmbAccessGroup_UnderManagementPersonnel_onCollapse() {
    if (cmbAccessGroup_UnderManagementPersonnel.getSelectedItem() == undefined)
        document.getElementById('cmbAccessGroup_UnderManagementPersonnel_Input').value = document.getElementById('hfcmbAlarm_UnderManagementPersonnel').value;
}

function CallBack_cmbAccessGroup_UnderManagementPersonnel_onBeforeCallback(sender, e) {
    cmbAccessGroup_UnderManagementPersonnel.dispose();
}

function CallBack_cmbAccessGroup_UnderManagementPersonnel_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_AccessGroup_UnderManagementPersonnel').value;
    if (error == "") {
        document.getElementById('cmbAccessGroup_UnderManagementPersonnel_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbAccessGroup_UnderManagementPersonnel_DropImage').mousedown();
        cmbAccessGroup_UnderManagementPersonnel.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbAccessGroup_UnderManagementPersonnel_DropDown').style.display = 'none';
    }
}

function Refresh_trvOrganizationPersonnel_UnderManagementPersonnel() {
    Fill_trvOrganizationPersonnel_UnderManagementPersonnel(true);
}

function trvOrganizationPersonnel_UnderManagementPersonnel_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvOrganizationPersonnel_UnderManagementPersonnel').innerHTML = '';
}

function trvOrganizationPersonnel_UnderManagementPersonnel_onCallbackComplete(sender, e) {
    if (ObjexpandingDepPersonnelNode_UnderManagementPersonnel != null) {
        if (ObjexpandingDepPersonnelNode_UnderManagementPersonnel.Node.get_nodes().get_length() == 0 && ObjexpandingDepPersonnelNode_UnderManagementPersonnel.HasChild) {
            ObjexpandingDepPersonnelNode_UnderManagementPersonnel = null;
            GetLoadonDemandError_DepartmetsPersonnel_OrganizationWorkFlowPage();
        }
        else
            ObjexpandingDepPersonnelNode_UnderManagementPersonnel = null;
    }
}

function GetLoadonDemandError_DepartmetsPersonnel_OrganizationWorkFlowPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function trvOrganizationPersonnel_UnderManagementPersonnel_onNodeBeforeExpand(sender, e) {
    if (ObjexpandingDepPersonnelNode_UnderManagementPersonnel != null)
        ObjexpandingDepPersonnelNode_UnderManagementPersonnel = null;
    ObjexpandingDepPersonnelNode_UnderManagementPersonnel = new Object();
    ObjexpandingDepPersonnelNode_UnderManagementPersonnel.Node = e.get_node();
    if (e.get_node().get_nodes().get_length() == 1 && (e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined || e.get_node().get_nodes().get_nodeArray()[0].get_id() == '')) {
        ObjexpandingDepPersonnelNode_UnderManagementPersonnel.HasChild = true;
        trvOrganizationPersonnel_UnderManagementPersonnel.beginUpdate();
        ObjexpandingDepPersonnelNode_UnderManagementPersonnel.Node.get_nodes().remove(0);
        trvOrganizationPersonnel_UnderManagementPersonnel.endUpdate();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            ObjexpandingDepPersonnelNode_UnderManagementPersonnel.HasChild = false;
        else
            ObjexpandingDepPersonnelNode_UnderManagementPersonnel.HasChild = true;
    }
}

function CallBack_trvOrganizationPersonnel_UnderManagementPersonnel_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_OrganizationPersonnel_UnderManagementPersonnel').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvOrganizationPersonnel_UnderManagementPersonnel(true);
    }
}

function Fill_trvOrganizationPersonnel_UnderManagementPersonnel(isRefresh) {
    if (trvOrganizationPersonnel_UnderManagementPersonnel.get_nodes().get_length() == 0 || isRefresh) {
        document.getElementById('loadingPanel_trvOrganizationPersonnel_UnderManagementPersonnel').innerHTML = document.getElementById('hfloadingPanel_trvOrganizationPersonnel_UnderManagementPersonnel').value;
        ObjexpandingDepPersonnelNode_UnderManagementPersonnel = null;
        CallBack_trvOrganizationPersonnel_UnderManagementPersonnel.callback();
    }
}

function GridUnderManagementPersonnel_UnderManagementPersonnel_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridUnderManagementPersonnel_UnderManagementPersonnel').innerHTML = '';
}

function Fill_GridUnderManagementPersonnel_UnderManagementPersonnel() {
    document.getElementById('loadingPanel_GridUnderManagementPersonnel_UnderManagementPersonnel').innerHTML = document.getElementById('hfloadingPanel_GridUnderManagementPersonnel_UnderManagementPersonnel').value;
    switch (CurrentPageState_UnderManagementPersonnel) {
        case 'Add':
            CallBack_GridUnderManagementPersonnel_UnderManagementPersonnel.callback(CharToKeyCode_UnderManagementPersonnel(CurrentPageState_UnderManagementPersonnel));
            break;
        case 'Edit':
            var flowID = parent.DialogUnderManagementPersonnel.get_value().FlowID;
            CallBack_GridUnderManagementPersonnel_UnderManagementPersonnel.callback(CharToKeyCode_UnderManagementPersonnel(CurrentPageState_UnderManagementPersonnel), CharToKeyCode_UnderManagementPersonnel(flowID));
            break;
    }
}

function Refresh_GridUnderManagementPersonnel_UnderManagementPersonnel() {
    Fill_GridUnderManagementPersonnel_UnderManagementPersonnel();
}

function CallBack_GridUnderManagementPersonnel_UnderManagementPersonnel_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_UnderManagementPersonnel_UnderManagementPersonnel').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridUnderManagementPersonnel_UnderManagementPersonnel();
    }
}

function tlbItemAdd_TlbInterAction_UnderManagementPersonnel_onClick() {
    InsertUnderManagement_UnderManagementPersonnel();
}

function InsertUnderManagement_UnderManagementPersonnel() {
    var UnderManagementPersonnelList_UnderManagementPersonnel = document.getElementById('hfUnderManagementPersonnelList_UnderManagementPersonnel').value;
    selectedNode_trvOrganizationPersonnel_UnderManagementPersonnel = trvOrganizationPersonnel_UnderManagementPersonnel.get_selectedNode();
    if (selectedNode_trvOrganizationPersonnel_UnderManagementPersonnel != undefined && selectedNode_trvOrganizationPersonnel_UnderManagementPersonnel.get_parentNode() != null) {
        var nodeID = selectedNode_trvOrganizationPersonnel_UnderManagementPersonnel.get_id();
        var nodeName = selectedNode_trvOrganizationPersonnel_UnderManagementPersonnel.get_text();
        var parentNodeID = selectedNode_trvOrganizationPersonnel_UnderManagementPersonnel.get_parentNode().get_id();
        var nodeType = selectedNode_trvOrganizationPersonnel_UnderManagementPersonnel.get_value();
        var hasAccess = false;
        var hasSubDep = false;
        var ListIdentifier = '';
        var rowID = Math.random().toString();

        switch (nodeType) {
            case '0':
                ListIdentifier = 'dep' + nodeID + 'prs0';
                break;
            case '1':
                ListIdentifier = 'dep' + parentNodeID + 'prs' + nodeID;
                break;
        }
        if (UnderManagementPersonnelList_UnderManagementPersonnel.indexOf(ListIdentifier) == -1) {
            var UnderManagementPersonnelListPart = 'Key=' + ListIdentifier + '%Type=' + nodeType + '%Access=' + hasAccess.toString() + '%SubDep=' + hasSubDep.toString();
            UnderManagementPersonnelList_UnderManagementPersonnel += UnderManagementPersonnelListPart + '#';
            GridUnderManagementPersonnel_UnderManagementPersonnel.beginUpdate();
            newUMPItem = GridUnderManagementPersonnel_UnderManagementPersonnel.get_table().addEmptyRow(GridUnderManagementPersonnel_UnderManagementPersonnel.get_recordCount());
            newUMPItem.setValue(0, rowID, false);
            newUMPItem.setValue(1, hasAccess, false);
            newUMPItem.setValue(2, nodeType, false);
            newUMPItem.setValue(3, nodeName, false);
            newUMPItem.setValue(4, hasSubDep, false);
            newUMPItem.setValue(5, ListIdentifier, false);
            GridUnderManagementPersonnel_UnderManagementPersonnel.selectByKey(rowID, 0, false);
            GridUnderManagementPersonnel_UnderManagementPersonnel.endUpdate();
        }
        document.getElementById('hfUnderManagementPersonnelList_UnderManagementPersonnel').value = UnderManagementPersonnelList_UnderManagementPersonnel;
    }
}

function GetUndermanagementType_UnderManagementPersonnel(typeKey) {
    var UnderManagementTypeList_UnderManagementPersonnel = document.getElementById('hfUndermanagementTypesList_UnderManagementPersonnel').value;
    ListParts = UnderManagementTypeList_UnderManagementPersonnel.split('#');
    for (var i = 0; i < ListParts.length; i++) {
        if (ListParts[i] != '') {
            var PartsSection = ListParts[i].split(':');
            if (PartsSection[0] == typeKey)
                return PartsSection[1];
        }
    }
}

function tlbItemDelete_TlbInterAction_UnderManagementPersonnel_onClick() {
    DeleteUnderManagement_UnderManagementPersonnel();
}

function DeleteUnderManagement_UnderManagementPersonnel() {
    var UnderManagementPersonnelList_UnderManagementPersonnel = document.getElementById('hfUnderManagementPersonnelList_UnderManagementPersonnel').value;
    selectedUnderManagement = GridUnderManagementPersonnel_UnderManagementPersonnel.getSelectedItems();
    if (selectedUnderManagement.length > 0) {
        ListIdentifier = 'Key=' + selectedUnderManagement[0].getMember('KeyID').get_text();
        var ListIdentifierIndex = UnderManagementPersonnelList_UnderManagementPersonnel.indexOf(ListIdentifier);
        if (ListIdentifierIndex >= 0) {
            var ListPart = ListIdentifier + '%Type=' + selectedUnderManagement[0].getMember('Type').get_text() + '%Access=' + selectedUnderManagement[0].getMember('Contains').get_text() + '%SubDep=' + selectedUnderManagement[0].getMember('ContainInnerChilds').get_text() + '#';
            UnderManagementPersonnelList_UnderManagementPersonnel = UnderManagementPersonnelList_UnderManagementPersonnel.replace(ListPart, '');

            GridUnderManagementPersonnel_UnderManagementPersonnel.beginUpdate();
            GridUnderManagementPersonnel_UnderManagementPersonnel.deleteSelected();
            GridUnderManagementPersonnel_UnderManagementPersonnel.endUpdate();
        }
    }
    document.getElementById('hfUnderManagementPersonnelList_UnderManagementPersonnel').value = UnderManagementPersonnelList_UnderManagementPersonnel;
}

function SetActionMode_UnderManagementPersonnel() {
    CurrentPageState_UnderManagementPersonnel = parent.DialogUnderManagementPersonnel.get_value().PageState;
    document.getElementById('ActionMode_UnderManagementPersonnel').innerHTML = document.getElementById('hf' + CurrentPageState_UnderManagementPersonnel + '_UnderManagementPersonnel').value;
    ChangePageState_UnderManagementPersonnel(CurrentPageState_UnderManagementPersonnel);
}

function contextMenu_trvOrganizationPersonnel_UnderManagementPersonnel_onItemSelect(sender, e) {
    var OrganizationPartItem_UnderManagementPersonnel = e.get_item();
    var OrganizationPartNode_UnderManagementPersonnel = OrganizationPartItem_UnderManagementPersonnel.get_parentMenu().get_contextData();
    trvOrganizationPersonnel_UnderManagementPersonnel.selectNodeById(OrganizationPartNode_UnderManagementPersonnel.get_id());
    InsertUnderManagement_UnderManagementPersonnel();
}

function trvOrganizationPersonnel_UnderManagementPersonnel_onContextMenu(sender, e) {
    if (TlbInterAction_UnderManagementPersonnel.get_items().getItemById('tlbItemAdd_TlbInterAction_UnderManagementPersonnel') != null)
        contextMenu_trvOrganizationPersonnel_UnderManagementPersonnel.showContextMenuAtEvent(e.get_event(), e.get_node());
}

function contextMenu_GridUnderManagementPersonnel_UnderManagementPersonnel_onItemSelect(sender, e) {
    DeleteUnderManagement_UnderManagementPersonnel();
}

function GridUnderManagementPersonnel_UnderManagementPersonnel_onContextMenu(sender, e) {
    if (TlbInterAction_UnderManagementPersonnel.get_items().getItemById('tlbItemDelete_TlbInterAction_UnderManagementPersonnel') != null) {
        GridUnderManagementPersonnel_UnderManagementPersonnel.select(e.get_item());
        contextMenu_GridUnderManagementPersonnel_UnderManagementPersonnel.showContextMenuAtEvent(e.get_event());
        contextMenu_GridUnderManagementPersonnel_UnderManagementPersonnel.set_contextData(e.get_item());
    }
}

function GridUnderManagementPersonnel_UnderManagementPersonnel_onItemBeforeCheckChange(sender, e) {
    var UnderManagementPersonnelList_UnderManagementPersonnel = document.getElementById('hfUnderManagementPersonnelList_UnderManagementPersonnel').value;
    var itemKey = e.get_item().getMember('KeyID').get_text();
    var itemType = e.get_item().getMember('Type').get_text();
    var itemAccess = e.get_item().getMember('Contains').get_value();
    var itemSubDep = e.get_item().getMember('ContainInnerChilds').get_value();
    if (CheckSubDepCheckChange_onItemBeforeCheckChange(e)) {
        var beforeChangeListPart = 'Key=' + itemKey + '%Type=' + itemType + '%Access=' + itemAccess.toString() + '%SubDep=' + itemSubDep.toString();
        if (UnderManagementPersonnelList_UnderManagementPersonnel.indexOf(beforeChangeListPart) >= 0) {
            switch (e.get_columnIndex()) {
                case 1:
                    itemAccess = !itemAccess;
                    break;
                case 4:
                    itemSubDep = !itemSubDep;
                    break;
            }
        }
        var afterChangeListPart = 'Key=' + itemKey + '%Type=' + itemType + '%Access=' + itemAccess.toString() + '%SubDep=' + itemSubDep.toString();
        UnderManagementPersonnelList_UnderManagementPersonnel = UnderManagementPersonnelList_UnderManagementPersonnel.replace(beforeChangeListPart, afterChangeListPart);
        document.getElementById('hfUnderManagementPersonnelList_UnderManagementPersonnel').value = UnderManagementPersonnelList_UnderManagementPersonnel;
    }
}

function CheckSubDepCheckChange_onItemBeforeCheckChange(e) {
    var changeIsAllowd = true;
    var dataField = GridUnderManagementPersonnel_UnderManagementPersonnel.get_table().get_columns()[e.get_columnIndex()].get_dataField();
    if (dataField == 'ContainInnerChilds' && e.get_item().getMember('Type').get_text() != '0') {
        var itemSubDep = e.get_item().getMember(dataField).get_value();
        if (!itemSubDep)
            changeIsAllowd = false;
    }
    return changeIsAllowd;
}

function GridUnderManagementPersonnel_UnderManagementPersonnel_onItemCheckChange(sender, e) {
    var itemSubDep = e.get_item().getMember('ContainInnerChilds').get_value();
    if (!itemSubDep && e.get_item().getMember('Type').get_text() != '0') {
        GridUnderManagementPersonnel_UnderManagementPersonnel.beginUpdate();
        e.get_item().setValue(4, false, false);
        GridUnderManagementPersonnel_UnderManagementPersonnel.endUpdate();
    }
}

function SetImage_clmnType_GridUnderManagementPersonnel_UnderManagementPersonnel(type) {
    var nodeTypeImage = '';
    switch (type.toString()) {
        case '0':
            nodeTypeImage = 'Images/Grid/folder.gif';
            break;
        case '1':
            nodeTypeImage = 'Images/Grid/user.png';
            break;
    }
    return nodeTypeImage;
}

function tlbItemSave_TlbUnderManagementPersonnel_onClick() {
    CollapseControls_UnderManagementPersonnel();
    UpdateUnderManagement_UnderManagementPersonnel();
}

function UpdateUnderManagement_UnderManagementPersonnel() {
    ObjUnderManagement_UnderManagementPersonnel = new Object();
    ObjUnderManagement_UnderManagementPersonnel.SelectedManagerID = '0';
    ObjUnderManagement_UnderManagementPersonnel.ManagerCreator = 'None';
    ObjUnderManagement_UnderManagementPersonnel.PersonnelID = '0';
    ObjUnderManagement_UnderManagementPersonnel.OrganizationPostID = '0';
    ObjUnderManagement_UnderManagementPersonnel.AccessGroupID = '0';
    ObjUnderManagement_UnderManagementPersonnel.AccessGroupName = '';
    ObjUnderManagement_UnderManagementPersonnel.FlowID = '0';
    ObjUnderManagement_UnderManagementPersonnel.FlowName = null;
    strUnderManagementList = document.getElementById('hfUnderManagementPersonnelList_UnderManagementPersonnel').value;
    ObjPageState_UnderManagementPersonnel = parent.DialogUnderManagementPersonnel.get_value();

    switch (CurrentPageState_UnderManagementPersonnel) {
        case 'Add':
            switch (CurrentManagerCreator_UnderManagementPersonnel) {
                case "OrganizationPost":
                    ObjUnderManagement_UnderManagementPersonnel.ManagerCreator = 'OrganizationPost';
                    var selectedOrganization_UnderManagementPersonnel = cmbPostSearchResult_UnderManagementPersonnel.getSelectedItem();
                    if (selectedOrganization_UnderManagementPersonnel != undefined && selectedOrganization_UnderManagementPersonnel != null) {
                        selectedOrganization_UnderManagementPersonnel = eval('(' + selectedOrganization_UnderManagementPersonnel.get_value() + ')');
                        ObjUnderManagement_UnderManagementPersonnel.OrganizationPostID = selectedOrganization_UnderManagementPersonnel.ID;
                    }
                    else {
                        selectedOrganization_UnderManagementPersonnel = trvOrganizationPosts_UnderManagementPersonnel.get_selectedNode();
                        if (selectedOrganization_UnderManagementPersonnel != undefined && selectedOrganization_UnderManagementPersonnel != null)
                            ObjUnderManagement_UnderManagementPersonnel.OrganizationPostID = selectedOrganization_UnderManagementPersonnel.get_id();
                    }
                    break;
                case 'Personnel':
                    ObjUnderManagement_UnderManagementPersonnel.ManagerCreator = 'Personnel';
                    var selectedPersonnel_UnderManagementPersonnel = cmbPersonnel_UnderManagementPersonnel.getSelectedItem();
                    if (selectedPersonnel_UnderManagementPersonnel != undefined && selectedPersonnel_UnderManagementPersonnel != null) {
                        selectedPersonnel_UnderManagementPersonnel = eval('(' + selectedPersonnel_UnderManagementPersonnel.get_value() + ')');
                        ObjUnderManagement_UnderManagementPersonnel.PersonnelID = selectedPersonnel_UnderManagementPersonnel.ID;
                    }
                    break;
            }
            break;
        case 'Edit':
            ObjUnderManagement_UnderManagementPersonnel.FlowID = ObjPageState_UnderManagementPersonnel.FlowID;
            break;
    }
    selectedAccessGroup_UnderManagementPersonnel = cmbAccessGroup_UnderManagementPersonnel.getSelectedItem();
    if (selectedAccessGroup_UnderManagementPersonnel != undefined && selectedAccessGroup_UnderManagementPersonnel != null) {
        ObjUnderManagement_UnderManagementPersonnel.AccessGroupID = selectedAccessGroup_UnderManagementPersonnel.get_value();
        ObjUnderManagement_UnderManagementPersonnel.AccessGroupName = selectedAccessGroup_UnderManagementPersonnel.get_text();
    }
    else {
        if (ObjPageState_UnderManagementPersonnel.AccessGroupID != undefined && ObjPageState_UnderManagementPersonnel.AccessGroupID != null) {
            ObjUnderManagement_UnderManagementPersonnel.AccessGroupID = ObjPageState_UnderManagementPersonnel.AccessGroupID;
            ObjUnderManagement_UnderManagementPersonnel.AccessGroupName = ObjPageState_UnderManagementPersonnel.AccessGroupName;
        }
    }
    ObjUnderManagement_UnderManagementPersonnel.FlowName = document.getElementById('txtFlowName_UnderManagementPersonnel').value;

    UpdateUnderManagement_UnderManagementPersonnelPage(CharToKeyCode_UnderManagementPersonnel(CurrentPageState_UnderManagementPersonnel), CharToKeyCode_UnderManagementPersonnel(ObjUnderManagement_UnderManagementPersonnel.SelectedManagerID), CharToKeyCode_UnderManagementPersonnel(ObjUnderManagement_UnderManagementPersonnel.ManagerCreator), CharToKeyCode_UnderManagementPersonnel(ObjUnderManagement_UnderManagementPersonnel.PersonnelID), CharToKeyCode_UnderManagementPersonnel(ObjUnderManagement_UnderManagementPersonnel.OrganizationPostID), CharToKeyCode_UnderManagementPersonnel(ObjUnderManagement_UnderManagementPersonnel.AccessGroupID), CharToKeyCode_UnderManagementPersonnel(ObjUnderManagement_UnderManagementPersonnel.FlowID), CharToKeyCode_UnderManagementPersonnel(ObjUnderManagement_UnderManagementPersonnel.FlowName), CharToKeyCode_UnderManagementPersonnel(strUnderManagementList));
    DialogWaiting.Show();
}

function UpdateUnderManagement_UnderManagementPersonnelPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_UnderManagementPersonnel').value;
            Response[1] = document.getElementById('hfConnectionError_UnderManagementPersonnel').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success' && RetMessage[3] != "" && RetMessage[3] != '0') {
            var obj = parent.DialogUnderManagementPersonnel.get_value();
            ObjUnderManagement_UnderManagementPersonnel.FlowID = RetMessage[3];
            if (CurrentPageState_UnderManagementPersonnel == 'Add')
                ChangePageState_UnderManagementPersonnel('AfterAdd');
            parent.document.getElementById('pgvWorkFlowsView_iFrame').contentWindow.UnderManagement_onAfterUpdate(CurrentPageState_UnderManagementPersonnel, ObjUnderManagement_UnderManagementPersonnel);
        }
    }
}

function ChangePageState_UnderManagementPersonnel(state) {
    switch (state) {
        case 'Add':
            if (TlbUnderManagementPersonnel.get_items().getItemById('tlbItemExeptionAccessCreation_TlbUnderManagementPersonnel') != null) {
                TlbUnderManagementPersonnel.get_items().getItemById('tlbItemExeptionAccessCreation_TlbUnderManagementPersonnel').set_enabled(false);
                TlbUnderManagementPersonnel.get_items().getItemById('tlbItemExeptionAccessCreation_TlbUnderManagementPersonnel').set_imageUrl('access_silver.png');
            }
            if (TlbUnderManagementPersonnel.get_items().getItemById('tlbItemExeptionAccessView_TlbUnderManagementPersonnel') != null) {
                TlbUnderManagementPersonnel.get_items().getItemById('tlbItemExeptionAccessView_TlbUnderManagementPersonnel').set_enabled(false);
                TlbUnderManagementPersonnel.get_items().getItemById('tlbItemExeptionAccessView_TlbUnderManagementPersonnel').set_imageUrl('exceptions_silver.png');
            }
            if (TlbUnderManagementPersonnel.get_items().getItemById('tlbItemWorkFlow_TlbUnderManagementPersonnel') != null) {
                TlbUnderManagementPersonnel.get_items().getItemById('tlbItemWorkFlow_TlbUnderManagementPersonnel').set_enabled(false);
                TlbUnderManagementPersonnel.get_items().getItemById('tlbItemWorkFlow_TlbUnderManagementPersonnel').set_imageUrl('flowCreate_silver.png');
            }
            break;
        case 'AfterAdd':
            TlbUnderManagementPersonnel.get_items().getItemById('tlbItemSave_TlbUnderManagementPersonnel').set_enabled(false);
            TlbUnderManagementPersonnel.get_items().getItemById('tlbItemSave_TlbUnderManagementPersonnel').set_imageUrl('save_silver.png');
            if (TlbUnderManagementPersonnel.get_items().getItemById('tlbItemExeptionAccessCreation_TlbUnderManagementPersonnel') != null) {
                TlbUnderManagementPersonnel.get_items().getItemById('tlbItemExeptionAccessCreation_TlbUnderManagementPersonnel').set_enabled(true);
                TlbUnderManagementPersonnel.get_items().getItemById('tlbItemExeptionAccessCreation_TlbUnderManagementPersonnel').set_imageUrl('access.png');
            }
            if (TlbUnderManagementPersonnel.get_items().getItemById('tlbItemExeptionAccessView_TlbUnderManagementPersonnel') != null) {
                TlbUnderManagementPersonnel.get_items().getItemById('tlbItemExeptionAccessView_TlbUnderManagementPersonnel').set_enabled(true);
                TlbUnderManagementPersonnel.get_items().getItemById('tlbItemExeptionAccessView_TlbUnderManagementPersonnel').set_imageUrl('exceptions.png');
            }
            if (TlbUnderManagementPersonnel.get_items().getItemById('tlbItemWorkFlow_TlbUnderManagementPersonnel') != null) {
                TlbUnderManagementPersonnel.get_items().getItemById('tlbItemWorkFlow_TlbUnderManagementPersonnel').set_enabled(true);
                TlbUnderManagementPersonnel.get_items().getItemById('tlbItemWorkFlow_TlbUnderManagementPersonnel').set_imageUrl('flowCreate.png');
            }
            break;
        case 'Edit':
            document.getElementById('Container_headerBox_UnderManagementPersonnel').removeChild(document.getElementById('headerBox_UnderManagementPersonnel'));
            var ObjPageState_UnderManagementPersonnel = parent.DialogUnderManagementPersonnel.get_value();
            document.getElementById('cmbAccessGroup_UnderManagementPersonnel_TextBox').innerHTML = ObjPageState_UnderManagementPersonnel.AccessGroupName;
            document.getElementById('txtFlowName_UnderManagementPersonnel').value = ObjPageState_UnderManagementPersonnel.FlowName;
            break;
    }
}

function tlbItemExeptionAccessCreation_TlbUnderManagementPersonnel_onClick() {
}

function tlbItemExeptionAccessView_TlbUnderManagementPersonnel_onClick() {
}

function tlbItemWorkFlow_TlbUnderManagementPersonnel_onClick() {
    ShowDialogManagersWorkFlow();
}

function CallBack_cmbPersonnel_UnderManagementPersonnel_onCallbackError(sender, e) {
    ShowConnectionError_UnderManagementPersonnel();
}

function CallBack_trvOrganizationPosts_UnderManagementPersonnel_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvOrganizationPosts_UnderManagementPersonnel').innerHTML = '';
    ShowConnectionError_UnderManagementPersonnel();
}

function CallBack_cmbPostSearchResult_UnderManagementPersonnel_onCallbackError(sender, e) {
    ShowConnectionError_UnderManagementPersonnel();
}

function CallBack_cmbAccessGroup_UnderManagementPersonnel_onCallbackError(sender, e) {
    ShowConnectionError_UnderManagementPersonnel();
}

function CallBack_trvOrganizationPersonnel_UnderManagementPersonnel_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvOrganizationPersonnel_UnderManagementPersonnel').innerHTML = '';
    ShowConnectionError_UnderManagementPersonnel();
}

function CallBack_GridUnderManagementPersonnel_UnderManagementPersonnel_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridUnderManagementPersonnel_UnderManagementPersonnel').innerHTML = '';
    ShowConnectionError_UnderManagementPersonnel();
}

function ShowConnectionError_UnderManagementPersonnel() {
    var error = document.getElementById('hfErrorType_UnderManagementPersonnel').value;
    var errorBody = document.getElementById('hfConnectionError_UnderManagementPersonnel').value;
    showDialog(error, errorBody, 'error');
}

function UpdateAccessGroups_onAfterAccessGroupsChange() {
    Fill_cmbAccessGroup_UnderManagementPersonnel();
}

function CollapseControls_UnderManagementPersonnel() {
    try {
        cmbPersonnel_UnderManagementPersonnel.collapse();
        cmbPostSearchResult_UnderManagementPersonnel.collapse();
    } catch (e) {
    }
    cmbAccessGroup_UnderManagementPersonnel.collapse();
}

function UnderManagementPersonnel_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_cmbPersonnel_UnderManagementPersonnel = SearchTerm;
    SetPageIndex_cmbPersonnel_UnderManagementPersonnel(0);
}

function tlbItemFormReconstruction_TlbUnderManagementPersonnel_onClick() {
    CloseDilaogUnderManagementPersonnel();
    parent.document.getElementById('pgvWorkFlowsView_iFrame').contentWindow.ShowDialogUnderManagementPersonnel();
}

function tlbItemHelp_TlbUnderManagementPersonnel_onClick() {
    LoadHelpPage('tlbItemHelp_TlbUnderManagementPersonnel');
}






















