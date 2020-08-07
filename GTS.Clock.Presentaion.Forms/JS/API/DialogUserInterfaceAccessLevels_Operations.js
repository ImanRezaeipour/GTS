
var chekedList_AccessLevelsAsign = '';

function GetBoxesHeaders_AccessLevelsAsign() {
    parent.document.getElementById('Title_DialogUserInterfaceAccessLevels').innerHTML = document.getElementById('hfTitle_DialogUserInterfaceAccessLevels').value;
    document.getElementById('header_AccessLevelsAsign_AccessLevelsAsign').innerHTML = document.getElementById('hfheader_AccessLevelsAsign_AccessLevelsAsign').value;
}

function tlbItemEndorsement_TlbAccessLevelsAsign_onClick() {
    UpdateAccessLevelsAsign_AccessLevelsAsign();
}

function tlbItemExit_TlbAccessLevelsAsign_onClick() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_AccessLevelsAsign').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDialogUserInterfaceAccessLevels();
    DialogConfirm.Close();
}

function CloseDialogUserInterfaceAccessLevels() {
    parent.document.getElementById('DialogUserInterfaceAccessLevels_IFrame').src = 'WhitePage.aspx';
    parent.DialogUserInterfaceAccessLevels.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function Refresh_trvAccessLevelsAsign_AccessLevelsAsign() {
    Fill_trvAccessLevelsAsign_AccessLevelsAsign();
}

function SetCurrentRoleName_AccessLevelsAsign() {
    var ObjDialogUserInterfaceAccessLevels = parent.DialogUserInterfaceAccessLevels.get_value();
    var roleName = ObjDialogUserInterfaceAccessLevels.RoleName;
    document.getElementById('txtUserRoleName_AccessLevelsAsign').value = roleName;
}

function Fill_trvAccessLevelsAsign_AccessLevelsAsign() {
    document.getElementById('loadingPanel_trvAccessLevelsAsign_AccessLevelsAsign').innerHTML = document.getElementById('hfloadingPanel_trvAccessLevelsAsign_AccessLevelsAsign').value;
    var ObjDialogUserInterfaceAccessLevels = parent.DialogUserInterfaceAccessLevels.get_value();
    var roleID = ObjDialogUserInterfaceAccessLevels.RoleID;
    CallBack_trvAccessLevelsAsign_AccessLevelsAsign.callback(CharToKeyCode_AccessLevelsAsign(roleID));
}

function trvAccessLevelsAsign_AccessLevelsAsign_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvAccessLevelsAsign_AccessLevelsAsign').innerHTML = '';
}

function CallBack_trvAccessLevelsAsign_AccessLevelsAsign_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_AccessLevelsAsign').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvAccessLevelsAsign_AccessLevelsAsign();
    }
}

function trvAccessLevelsAsign_AccessLevelsAsign_onNodeCheckChange(sender, e) {
    var parentNode = e.get_node();
    var checked = parentNode.get_checked();
    ChangeChildNodesCheck_trvAccessLevelsAsign_AccessLevelsAsign(parentNode, checked);    
}

function ChangeChildNodesCheck_trvAccessLevelsAsign_AccessLevelsAsign(parentNode, checked) {
    var childNodesCol = parentNode.get_nodes();
    for (var i = 0; i < childNodesCol.get_length(); i++) {
        var childNode = childNodesCol.getNode(i);
        childNode.set_checked(checked);
        ChangeChildNodesCheck_trvAccessLevelsAsign_AccessLevelsAsign(childNode, checked);
    }
}

function UpdateAccessLevelsAsign_AccessLevelsAsign() {
    var roleID = parent.DialogUserInterfaceAccessLevels.get_value().RoleID;
    var parentNodesCol = trvAccessLevelsAsign_AccessLevelsAsign.get_nodes();
    for (var i = 0; i < parentNodesCol.get_length(); i++) {
        var parentNode = parentNodesCol.getNode(i);
        GetChildNodesCheck_trvAccessLevelsAsign_AccessLevelsAsign(parentNode);
    }
    if (chekedList_AccessLevelsAsign.charAt(chekedList_AccessLevelsAsign.length - 1) == ',')
        chekedList_AccessLevelsAsign = chekedList_AccessLevelsAsign.substring(0, chekedList_AccessLevelsAsign.length - 1);
    chekedList_AccessLevelsAsign = '[' + chekedList_AccessLevelsAsign + ']';
    UpdateAccessLevelsAsign_AccessLevelsAsignPage(CharToKeyCode_AccessLevelsAsign(roleID), CharToKeyCode_AccessLevelsAsign(chekedList_AccessLevelsAsign));
    DialogWaiting.Show();
}

function UpdateAccessLevelsAsign_AccessLevelsAsignPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        chekedList_AccessLevelsAsign = '';
    }
}

function GetChildNodesCheck_trvAccessLevelsAsign_AccessLevelsAsign(parentNode) {
    var childNodesCol = parentNode.get_nodes();
    for (var i = 0; i < childNodesCol.get_length(); i++) {
        var childNode = childNodesCol.getNode(i);
        if (childNode.get_checked())
            chekedList_AccessLevelsAsign += childNode.get_id() + ',';
        if(childNode != undefined)
           GetChildNodesCheck_trvAccessLevelsAsign_AccessLevelsAsign(childNode);
    }
}

function CharToKeyCode_AccessLevelsAsign(str) {
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

function CallBack_trvAccessLevelsAsign_AccessLevelsAsign_onCallbackError(sender, e) {
     ShowConnectionError_AccessLevelsAsign();
}

function ShowConnectionError_AccessLevelsAsign() {
    var error = document.getElementById('hfErrorType_AccessLevelsAsign').value;
    var errorBody = document.getElementById('hfConnectionError_AccessLevelsAsign').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbAccessLevelsAsign_onClick() {
    CloseDialogUserInterfaceAccessLevels();
    parent.document.getElementById('pgvRolesIntroduction_iFrame').contentWindow.ShowDialogAccessLevels();
}







