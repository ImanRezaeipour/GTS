
function DialogUserInformation_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogUserInformation = DialogUserInformation.get_value();
    DialogUserInformation.set_contentUrl("UserInformation.aspx?CallerSchema=" + CharToKeyCode_UserInformation(ObjDialogUserInformation.CallerSchema) + "&LoadState=" + CharToKeyCode_UserInformation(ObjDialogUserInformation.LoadState) + "");
    document.getElementById('DialogUserInformation_IFrame').style.display = '';
    document.getElementById('DialogUserInformation_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogUserInformation_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogUserInformation_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogUserInformation_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogUserInformation_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogUserInformation').align = 'left';
        document.getElementById('tbl_DialogUserInformationheader').dir = 'rtl';
        document.getElementById('tbl_DialogUserInformationfooter').dir = 'rtl';        
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogUserInformation').align = 'right';
}

function DialogUserInformation_onClose(sender, e) {
    document.getElementById('DialogUserInformation_IFrame').style.display = 'none';
    document.getElementById('DialogUserInformation_IFrame').style.visibility = 'hidden';
    DialogUserInformation.set_contentUrl("about:blank");
}

function CharToKeyCode_UserInformation(str) {
    var OutStr = '';
    if (str != null) {
        for (var i = 0; i < str.length; i++) {
            var KeyCode = str.charCodeAt(i);
            var CharKeyCode = '//' + KeyCode;
            OutStr += CharKeyCode;
        }
    }
    return OutStr;
}



