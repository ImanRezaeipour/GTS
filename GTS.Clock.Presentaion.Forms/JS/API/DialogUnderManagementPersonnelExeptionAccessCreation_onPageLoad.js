

function DialogUnderManagementPersonnelExeptionAccessCreation_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogUnderManagementPersonnelExeptionAccessCreation.set_contentUrl("UnderManagementPersonnelExeptionAccessCreation.aspx");
    document.getElementById('DialogUnderManagementPersonnelExeptionAccessCreation_IFrame').style.display = '';
    document.getElementById('DialogUnderManagementPersonnelExeptionAccessCreation_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogUnderManagementPersonnelExeptionAccessCreation_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogUnderManagementPersonnelExeptionAccessCreation_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogUnderManagementPersonnelExeptionAccessCreation_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogUnderManagementPersonnelExeptionAccessCreation_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogUnderManagementPersonnelExeptionAccessCreation').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogUnderManagementPersonnelExeptionAccessCreation').align = 'right';
}

function DialogUnderManagementPersonnelExeptionAccessCreation_onClose(sender, e) {
    document.getElementById('DialogUnderManagementPersonnelExeptionAccessCreation_IFrame').style.display = 'none';
    document.getElementById('DialogUnderManagementPersonnelExeptionAccessCreation_IFrame').style.visibility = 'hidden';
    DialogUnderManagementPersonnelExeptionAccessCreation.set_contentUrl("about:blank");
}
