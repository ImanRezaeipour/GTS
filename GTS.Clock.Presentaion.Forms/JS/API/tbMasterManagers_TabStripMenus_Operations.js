
var LoadState_Managers = 'Normal';
var CurrentPageIndex_GridManagers_MasterManagers = 0;
var CurrentPageCombosCallBcakStateObj = new Object();
var ConfirmState_Manager = null;
var ObjManagerDetails_MasterManagers = new Object();
var CurrentPageState_MasterManagers = 'View';
var box_ManagersDetails_MasterManagers_IsShown = false;


function GetBoxesHeaders_MasterManagers() {
    document.getElementById('header_ManagersDetails_MasterManagers').innerHTML = document.getElementById('hfheader_ManagersDetails_MasterManagers').value;
    document.getElementById('header_Managers_MasterManagers').innerHTML = document.getElementById('hfheader_Managers_MasterManagers').value;
    document.getElementById('footer_GridManagers_MasterManagers').innerHTML = document.getElementById('hffooter_GridManagers_MasterManagers').value;
}

function imgbox_ManagersDetails_MasterManagers_onClick() {
    CollapseControls_MasterManagers();
    setSlideDownSpeed(200);
    slidedown_showHide('box_ManagersDetails_MasterManagers');

    if (box_ManagersDetails_MasterManagers_IsShown) {
        box_ManagersDetails_MasterManagers_IsShown = false;
        document.getElementById('imgbox_ManagersDetails_MasterManagers').src = 'Images/Ghadir/arrowDown.jpg';
    }
    else {
        box_ManagersDetails_MasterManagers_IsShown = true;
        document.getElementById('imgbox_ManagersDetails_MasterManagers').src = 'Images/Ghadir/arrowUp.jpg';
    }
}

function rdbManagerFilter_MasterManagers_onClick() {
    cmbAccessGroup_MasterManagers.enable();
    cmbSearchField_MasterManagers.disable();
    document.MasterManagersForm.txtSearchTerm_MasterManagers.disabled = 'disabled';
}

function rdbManagerSearch_MasterManagers_onClick() {
    cmbAccessGroup_MasterManagers.disable();
    cmbSearchField_MasterManagers.enable();
    document.MasterManagersForm.txtSearchTerm_MasterManagers.disabled = '';
}

function MasterManagersForm_onPageLoad() {
    SetPageIndex_GridManagers_MasterManagers(0);
}

function SetPageIndex_GridManagers_MasterManagers(pageIndex) {
    CurrentPageIndex_GridManagers_MasterManagers = pageIndex;
    document.getElementById('ManagersIntroduction_iFrame').contentWindow.Fill_GridManagers_Managers(LoadState_Managers, pageIndex);
}

function tlbItemApplyConditions_TlbApplyConditions_MasterManagers_onClick() {
    if (document.getElementById('rdbManagerFilter_MasterManagers').checked)
        LoadState_Managers = 'Filter';
    if (document.getElementById('rdbManagerSearch_MasterManagers').checked)
        LoadState_Managers = 'Search';
    SetObjManagerDetails_MasterManagers();
    SetPageIndex_GridManagers_MasterManagers(0);
    imgbox_ManagersDetails_MasterManagers_onClick();
    ClearManagerDetails_MasterManagers();
}

function SetObjManagerDetails_MasterManagers() {
    ObjManagerDetails_MasterManagers.AccessGroup = cmbAccessGroup_MasterManagers.getSelectedItem();
    ObjManagerDetails_MasterManagers.SearchField = cmbSearchField_MasterManagers.getSelectedItem();
    ObjManagerDetails_MasterManagers.SearchTerm = document.getElementById('txtSearchTerm_MasterManagers').value;
}

function ClearManagerDetails_MasterManagers() {
    document.getElementById('rdbManagerFilter_MasterManagers').checked = false;
    document.getElementById('rdbManagerSearch_MasterManagers').checked = false;
    document.getElementById('txtSearchTerm_MasterManagers').value = '';
    document.getElementById('cmbAccessGroup_MasterManagers_Input').value = '';
    document.getElementById('cmbSearchField_MasterManagers_Input').value = '';
    cmbAccessGroup_MasterManagers.unSelect();
    cmbSearchField_MasterManagers.unSelect();
}


function SetPosition_DropDownDives_MasterManagers() {
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('box_ManagersDetails_MasterManagers').style.right = '10px';
    if (parent.CurrentLangID == 'en-US')
        document.getElementById('box_ManagersDetails_MasterManagers').style.left = '10px';
}

function tlbItemExit_TlbMasterManagers_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Manager = confirmState;
    if (CurrentPageState_MasterManagers == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_MasterManagers').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_MasterManagers').value;
    DialogConfirm.Show();
    CollapseControls_MasterManagers();
}

function tlbItemRefresh_TlbPaging_GridManagers_MasterManagers_onClick() {
    Refresh_GridManagers_MasterManagers();
    ClearManagerDetails_MasterManagers();
    ViewWorkFlow_MasterManagers('Refresh');
}

function Refresh_GridManagers_MasterManagers() {
    LoadState_Managers = 'Normal';
    SetPageIndex_GridManagers_MasterManagers(0);
}


function tlbItemFirst_TlbPaging_GridManagers_MasterManagers_onClick() {
    SetPageIndex_GridManagers_MasterManagers(0);
}

function tlbItemBefore_TlbPaging_GridManagers_MasterManagers_onClick() {
    if (CurrentPageIndex_GridManagers_MasterManagers != 0) {
        CurrentPageIndex_GridManagers_MasterManagers = CurrentPageIndex_GridManagers_MasterManagers - 1;
        SetPageIndex_GridManagers_MasterManagers(CurrentPageIndex_GridManagers_MasterManagers);
    }
}

function tlbItemNext_TlbPaging_GridManagers_MasterManagers_onClick() {
    if (CurrentPageIndex_GridManagers_MasterManagers < document.getElementById('ManagersIntroduction_iFrame').contentWindow.GetManagersPageCount_Managers() - 1) {
        CurrentPageIndex_GridManagers_MasterManagers = CurrentPageIndex_GridManagers_MasterManagers + 1;
        SetPageIndex_GridManagers_MasterManagers(CurrentPageIndex_GridManagers_MasterManagers);
    }
}

function tlbItemLast_TlbPaging_GridManagers_MasterManagers_onClick() {
    SetPageIndex_GridManagers_MasterManagers(document.getElementById('ManagersIntroduction_iFrame').contentWindow.GetManagersPageCount_Managers() - 1);
}

function GetSelectedItem_cmbAccessGroup_MasterManagers() {
    return ObjManagerDetails_MasterManagers.AccessGroup;
}

function GetSearchObj_MasterManagers() {
    var serachObj_MasterManagers = new Object();
    serachObj_MasterManagers.SearchField = ObjManagerDetails_MasterManagers.SearchField;
    serachObj_MasterManagers.SearchTerm = ObjManagerDetails_MasterManagers.SearchTerm;
    return serachObj_MasterManagers;
}

function SetLoadingPanel_GridManagers_Managers() {
    document.getElementById('loadingPanel_GridManagers_MasterManagers').innerHTML = document.getElementById('hfloadingPanel_GridManagers_MasterManagers').value;
}

function cmbAccessGroup_MasterManagers_onExpand(sender, e) {
    if (cmbAccessGroup_MasterManagers.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbAccessGroup_MasterManagers == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbAccessGroup_MasterManagers = true;
        CallBack_cmbAccessGroup_MasterManagers.callback();
    }
}

function CallBack_cmbAccessGroup_MasterManagers_onBeforeCallback(sender, e) {
    cmbAccessGroup_MasterManagers.dispose();
}

function CallBack_cmbAccessGroup_MasterManagers_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Filter').value;
    if (error == "") {
        document.getElementById('cmbAccessGroup_MasterManagers_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbAccessGroup_MasterManagers_DropImage').mousedown();
        cmbAccessGroup_MasterManagers.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbAccessGroup_MasterManagers_DropDown').style.display = 'none';
    }
}

function cmbSearchField_MasterManagers_onExpand(sender, e) {
    if (cmbSearchField_MasterManagers.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSearchField_MasterManagers == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSearchField_MasterManagers = true;
        CallBack_cmbSearchField_MasterManagers.callback();
    }
}

function CallBack_cmbSearchField_MasterManagers_onBeforeCallback(sender, e) {
    cmbSearchField_MasterManagers.dispose();
}

function CallBack_cmbSearchField_MasterManagers_CallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Search').value;
    if (error == "") {
        document.getElementById('cmbSearchField_MasterManagers_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbSearchField_MasterManagers_DropImage').mousedown();
        cmbSearchField_MasterManagers.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbSearchField_MasterManagers_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function Changefooter_GridManagers_Managers(pageCount) {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridManagers_MasterManagers').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = CurrentPageIndex_GridManagers_MasterManagers + 1;
        if (i == 3)
            footerValCol[i] = pageCount;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridManagers_MasterManagers').innerHTML = retfooterVal;
}

function Changeheader_GridManagers_Managers() {
    document.getElementById('loadingPanel_GridManagers_MasterManagers').innerHTML = '';
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Manager) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateManager_Managers();
            break;
        case 'Exit':
            parent.CloseCurrentTabOnTabStripMenus();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    CurrentPageState_MasterManagers = 'View';
}

function UpdateManager_Managers() {
}

function SetActionMode_MasterManagers(state) {
    document.getElementById('ActionMode_MasterManagers').innerHTML = document.getElementById('hf' + state + '_MasterManagers').value;
}

function tlbItemWorkFlowView_TlbMasterManagers_onClick() {
    ViewWorkFlow_MasterManagers('View');
}

function ViewWorkFlow_MasterManagers(state) {
    var FlowID_MasterManagers = null;
    switch (state) {
        case 'View':
            var SelectedItems_GridManages = document.getElementById('ManagersIntroduction_iFrame').contentWindow.GetSelectedManagerItem_GridManagers_Managers();
            if (SelectedItems_GridManages != null)
                FlowID_MasterManagers = SelectedItems_GridManages.getMember('ID').get_text();
            break;
        case 'Refresh':
            FlowID_MasterManagers = '0';
            break
    }
    CallBackWorkFlow_MasterManagers.callback(CharToKeyCode_MasterManagers(FlowID_MasterManagers));
}

function CallBackWorkFlow_MasterManagers_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkFlow').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}

function CharToKeyCode_MasterManagers(str) {
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

function ShowError_GridManagers_Managers_onCallBackCompleted(errorParts) {
    showDialog(errorParts[0], errorParts[1], errorParts[2]);
}

function tlbItemNew_TlbMasterManagers_onClick() {
    CurrentPageState_MasterManagers = 'Add';
    ShowDialogUnderManagementPersonnel();
}

function tlbItemEdit_TlbMasterManagers_onClick() {
    CurrentPageState_MasterManagers = 'Edit';
    ShowDialogUnderManagementPersonnel();
}

function ShowDialogUnderManagementPersonnel() {
    var ObjPageState_MasterManagers = new Object();
    switch (CurrentPageState_MasterManagers) {
        case 'Add':
            ObjPageState_MasterManagers.PageState = CurrentPageState_MasterManagers;
            parent.DialogUnderManagementPersonnel.set_value(ObjPageState_MasterManagers);
            parent.DialogUnderManagementPersonnel.Show();
            break;
        case 'Edit':
            selectedItems_GridManagers_Managers = document.getElementById('ManagersIntroduction_iFrame').contentWindow.GetSelectedManagerItem_GridManagers_Managers();
            if (selectedItems_GridManagers_Managers != null) {
                ObjPageState_MasterManagers.PageState = CurrentPageState_MasterManagers;
                ObjPageState_MasterManagers.FlowID = selectedItems_GridManagers_Managers.getMember('ID').get_text();
                ObjPageState_MasterManagers.FlowName = selectedItems_GridManagers_Managers.getMember('FlowName').get_text();
                ObjPageState_MasterManagers.AccessGroupID = selectedItems_GridManagers_Managers.getMember('AccessGroup.ID').get_text();
                ObjPageState_MasterManagers.AccessGroupName = selectedItems_GridManagers_Managers.getMember('AccessGroup.Name').get_text();
                parent.DialogUnderManagementPersonnel.set_value(ObjPageState_MasterManagers);
                parent.DialogUnderManagementPersonnel.Show();
            }
            break;
    }
}

function UnderManagement_onAfterUpdate() {
    Refresh_GridManagers_MasterManagers();
    ClearManagerDetails_MasterManagers();
}

function CallBack_cmbAccessGroup_MasterManagers_onCallbackError(sender, e) {
    ShowConnectionError_MasterManagers();
}

function CallBackWorkFlow_MasterManagers_onCallbackError(sender, e) {
    ShowConnectionError_MasterManagers();
}

function ShowConnectionError_MasterManagers() {
    var error = document.getElementById('hfErrorType_MasterManagers').value;
    var errorBody = document.getElementById('hfConnectionError_MasterManagers').value;
    showDialog(error, errorBody, 'error');
}

function CollapseControls_MasterManagers() {
    cmbAccessGroup_MasterManagers.collapse();
    cmbSearchField_MasterManagers.collapse();
}

function CallBack_cmbSearchField_MasterManagers_onCallbackError(sender, e) {
    ShowConnectionError_MasterManagers();
}

function tlbItemFormReconstruction_TlbMasterManagers_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvMasterManagersIntroduction_iFrame').src = 'MasterManagers.aspx';
}

function tlbItemHelp_TlbMasterManagers_onClick() {
    LoadHelpPage('tlbItemHelp_TlbMasterManagers');
}










