

function DialogUnderManagementPersonnelExeptionAccessView_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogUnderManagementPersonnelExeptionAccessView.set_contentUrl("MasterUnderManagementPersonnelExeptionAccessView.aspx");
    document.getElementById('DialogUnderManagementPersonnelExeptionAccessView_IFrame').style.display = '';
    document.getElementById('DialogUnderManagementPersonnelExeptionAccessView_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogUnderManagementPersonnelExeptionAccessView_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogUnderManagementPersonnelExeptionAccessView_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogUnderManagementPersonnelExeptionAccessView_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogUnderManagementPersonnelExeptionAccessView_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogUnderManagementPersonnelExeptionAccessView').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogUnderManagementPersonnelExeptionAccessView').align = 'right';
}

function DialogUnderManagementPersonnelExeptionAccessView_onClose(sender, e) {
    document.getElementById('DialogUnderManagementPersonnelExeptionAccessView_IFrame').style.display = 'none';
    document.getElementById('DialogUnderManagementPersonnelExeptionAccessView_IFrame').style.visibility = 'hidden';
    DialogUnderManagementPersonnelExeptionAccessView.set_contentUrl("about:blank");
}
