

function DialogUnderManagementPersonnel_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogUnderManagementPersonnel.set_contentUrl("UnderManagementPersonnel.aspx");
    document.getElementById('DialogUnderManagementPersonnel_IFrame').style.display = '';
    document.getElementById('DialogUnderManagementPersonnel_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogUnderManagementPersonnel_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogUnderManagementPersonnel_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogUnderManagementPersonnel_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogUnderManagementPersonnel_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogUnderManagementPersonnel').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogUnderManagementPersonnel').align = 'right';
}

function DialogUnderManagementPersonnel_onClose(sender, e) {
    document.getElementById('DialogUnderManagementPersonnel_IFrame').style.display = 'none';
    document.getElementById('DialogUnderManagementPersonnel_IFrame').style.visibility = 'hidden';
    DialogUnderManagementPersonnel.set_contentUrl("about:blank");
}
