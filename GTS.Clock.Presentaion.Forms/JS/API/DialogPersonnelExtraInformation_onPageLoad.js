
function DialogPersonnelExtraInformation_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var DialogPersonnelExtraInformationValue = DialogPersonnelExtraInformation.get_value();
    DialogPersonnelExtraInformation.set_contentUrl("PersonnelExtraInformation.aspx?PersonnelState=" + CharToKeyCode_PersonnelExtraInformation(DialogPersonnelExtraInformationValue.PageState) + "");
    document.getElementById('DialogPersonnelExtraInformation_IFrame').style.display = '';
    document.getElementById('DialogPersonnelExtraInformation_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogPersonnelExtraInformation_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogPersonnelExtraInformation_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogPersonnelExtraInformation_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogPersonnelExtraInformation_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogPersonnelExtraInformation').align = 'left';
        document.getElementById('tbl_DialogPersonnelExtraInformationheader').dir = 'rtl';
        document.getElementById('tbl_DialogPersonnelExtraInformationfooter').dir = 'rtl';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogPersonnelExtraInformation').align = 'right';
}

function DialogPersonnelExtraInformation_onClose(sender, e) {
    document.getElementById('DialogPersonnelExtraInformation_IFrame').style.display = 'none';
    document.getElementById('DialogPersonnelExtraInformation_IFrame').style.visibility = 'hidden';
    DialogPersonnelExtraInformation.set_contentUrl("about:blank");
}

function CharToKeyCode_PersonnelExtraInformation(str) {
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



