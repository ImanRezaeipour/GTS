
var ConfirmState_PasswordChange = null;

function GetCurrentUser_PasswordChange() { 
    document.getElementById('txtUserName_PasswordChange').value = document.getElementById('hfCurrentUser_PasswordChange').value;
}

function tlbItemEndorsement_TlbPasswordChange_onClick() {
    ShowDialogConfirm('Change');
}

function tlbItemExit_TlbPasswordChange_onClick() {
    ShowDialogConfirm('Exit');    
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_PasswordChange) {
        case 'Change':
            DialogConfirm.Close();
            UpdatePassword_PasswordChange();
            break;
        case 'Exit':
            parent.CloseCurrentTabOnTabStripMenus();            
            break;
    }
}

function UpdatePassword_PasswordChange() {
    var CurrentPassword = document.getElementById('txtPassword_PasswordChange').value;
    var NewPassword = document.getElementById('txtNewPassword_PasswordChange').value;
    var NewPasswordRepeat = document.getElementById('txtNewPasswordRepeat_PasswordChange').value;

    UpdatePassword_PasswordChangePage(CharToKeyCode_PasswordChange(CurrentPassword), CharToKeyCode_PasswordChange(NewPassword), CharToKeyCode_PasswordChange(NewPasswordRepeat));
    DialogWaiting.Show();
}

function UpdatePassword_PasswordChange_onCallBack(Response) { 
        var RetMessage = Response;
        if (RetMessage != null && RetMessage.length > 0) {
            DialogWaiting.Close();
            if (Response[1] == "ConnectionError") {
                Response[0] = document.getElementById('hfErrorType_PasswordChange').value;
                Response[1] = document.getElementById('hfConnectionError_PasswordChange').value;
            }
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
            if (RetMessage[2] == 'success')
                ClearList_PasswordChange();
        }
}

function ClearList_PasswordChange(){
    document.getElementById('txtPassword_PasswordChange').value = '';
    document.getElementById('txtNewPassword_PasswordChange').value = '';
    document.getElementById('txtNewPasswordRepeat_PasswordChange').value = '';
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function CharToKeyCode_PasswordChange(str) {
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

function ShowDialogConfirm(confirmState) {
    ConfirmState_PasswordChange = confirmState;
    switch (confirmState) {
        case 'Change':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfChangeMessage_PasswordChange').value;
            break;
        case 'Exit':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PasswordChange').value;
            break;
    }
    DialogConfirm.Show();
}

function tlbItemFormReconstruction_TlbPasswordChange_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvPasswordChange_iFrame').src = 'PasswordChange.aspx';
}

function tlbItemHelp_TlbPasswordChange_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPasswordChange');
}