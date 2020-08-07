
var CurrentPageCombosCallBcakStateObj = new Object();

function GetBoxesHeaders_MasterDataAccessLevels() {
    parent.document.getElementById('Title_DialogMasterDataAccessLevels').innerHTML = document.getElementById('hfTitle_DialogMasterDataAccessLevels').value;
    document.getElementById('cmbDataAccessLevels_MasterDataAccessLevels_Input').value = document.getElementById('hfcmbAlarm_MasterDataAccessLevels').value;
}

function cmbDataAccessLevels_MasterDataAccessLevels_onExpand(sender, e) {
    if (cmbDataAccessLevels_MasterDataAccessLevels.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDataAccessLevels_MasterDataAccessLevels == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDataAccessLevels_MasterDataAccessLevels = true;
        CallBack_cmbDataAccessLevels_MasterDataAccessLevels.callback();
    }
}

function cmbDataAccessLevels_MasterDataAccessLevels_onCollapse(sender, e) {
    if (cmbDataAccessLevels_MasterDataAccessLevels.getSelectedItem() == undefined)
        document.getElementById('cmbDataAccessLevels_MasterDataAccessLevels_Input').value = document.getElementById('hfcmbAlarm_MasterDataAccessLevels').value;
}

function cmbDataAccessLevels_MasterDataAccessLevels_onBeforeCallback(sender, e) {
    cmbDataAccessLevels_MasterDataAccessLevels.dispose();
}

function cmbDataAccessLevels_MasterDataAccessLevels_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DataAccessLevels').value;
    if (error == "") {
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDataAccessLevels_MasterDataAccessLevels_DropImage').mousedown();
        cmbDataAccessLevels_MasterDataAccessLevels.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbDataAccessLevels_MasterDataAccessLevels_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function cmbDataAccessLevels_MasterDataAccessLevels_onCallbackError(sender, e) {
    ShowConnectionError_MasterDataAccessLevels();
}

function ShowConnectionError_MasterDataAccessLevels() {
    var error = document.getElementById('hfErrorType_MasterDataAccessLevels').value;
    var errorBody = document.getElementById('hfConnectionError_MasterDataAccessLevels').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemExit_TlbMasterDataAccessLevels_onClick() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_MasterDataAccessLevels').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDialogMasterDataAccessLevels();
}

function CloseDialogMasterDataAccessLevels() {
    parent.document.getElementById('DialogMasterDataAccessLevels_IFrame').src = 'WhitePage.aspx';
    parent.DialogMasterDataAccessLevels.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function tlbItemSettings_TlbSettings_MasterDataAccessLevels_onClick() {
    if (cmbDataAccessLevels_MasterDataAccessLevels.getSelectedItem() != undefined) {
        parent.DialogLoading.Show();
        var ObjDataAccessLevel = cmbDataAccessLevels_MasterDataAccessLevels.getSelectedItem().get_value();
        ObjDataAccessLevel = eval('(' + ObjDataAccessLevel + ')');
        var DataAccessLevelsPart = ObjDataAccessLevel.Key;
        var ContainerIFrameSource_MasterDataAccessLevels = null;
        var ContainerIFrameSourcePart_MasterDataAccessLevels = null;
        if (DataAccessLevelsPart == 'Shift' || DataAccessLevelsPart == 'WorkGroup' || DataAccessLevelsPart == 'Precard' || DataAccessLevelsPart == 'ControlStation' || DataAccessLevelsPart == 'Doctor' || DataAccessLevelsPart == 'Flow' || DataAccessLevelsPart == 'Corporation')
            ContainerIFrameSource_MasterDataAccessLevels = 'SinglePageDataAccessLevels.aspx';
        if (DataAccessLevelsPart == 'Manager') {
            var ObjDailogMasterDataAccessLevels = parent.DialogMasterDataAccessLevels.get_value();
            var UserID = ObjDailogMasterDataAccessLevels.UserID;
            ContainerIFrameSource_MasterDataAccessLevels = 'MultiPageDataAccessLevels.aspx';
            ContainerIFrameSourcePart_MasterDataAccessLevels = '&UID=' + CharToKeyCode_MasterDataAccessLevels(UserID) + '&Dalk=' + CharToKeyCode_MasterDataAccessLevels(DataAccessLevelsPart) + ''
        }
        if (DataAccessLevelsPart == 'Department' || DataAccessLevelsPart == 'RuleGroup' || DataAccessLevelsPart == 'Report' || DataAccessLevelsPart == 'OrganizationUnit')
            ContainerIFrameSource_MasterDataAccessLevels = 'MultiLevelsDataAccessLevels.aspx';

        document.getElementById('ContainerIFrame_MasterDataAccessLevels').src = ContainerIFrameSource_MasterDataAccessLevels + '?DT=' + (new Date()).getTime() + ContainerIFrameSourcePart_MasterDataAccessLevels + '&PageCaller=' + DataAccessLevelsPart;
    }
}

function GetObjDataAccessLevel_MasterDataAccessLevels() {
    var ObjDataAccessLevel = null;
    if (cmbDataAccessLevels_MasterDataAccessLevels.getSelectedItem() != undefined) {
        ObjDataAccessLevel = cmbDataAccessLevels_MasterDataAccessLevels.getSelectedItem().get_value();
        ObjDataAccessLevel = eval('(' + ObjDataAccessLevel + ')');
        return ObjDataAccessLevel;
    }
}

function CharToKeyCode_MasterDataAccessLevels(str) {
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

function tlbItemFormReconstruction_TlbMasterDataAccessLevels_onClick() {
    CloseDialogMasterDataAccessLevels();
    parent.document.getElementById('pgvUsersIntroduction_iFrame').contentWindow.ShowDialogDataAccessLevels();
}

function tlbItemHelp_TlbMasterDataAccessLevels_onClick() {
    LoadHelpPage('tlbItemHelp_TlbMasterDataAccessLevels');
}

function SetUserCount_MasterDataAccessLevels() {
    var ObjDialogMasterDataAccessLevels = parent.DialogMasterDataAccessLevels.get_value();
    document.getElementById('tdUserCount_MasterDataAccessLevels').innerHTML = document.getElementById('hfUserCount_MasterDataAccessLevels').value + ObjDialogMasterDataAccessLevels.UserCount;
}


