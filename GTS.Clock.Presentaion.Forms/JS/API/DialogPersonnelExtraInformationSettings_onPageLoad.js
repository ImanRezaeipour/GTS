

function DialogPersonnelExtraInformationSettings_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogPersonnelExtraInformationSettings.set_contentUrl("PersonnelExtraInformationSettings.aspx");
    document.getElementById('DialogPersonnelExtraInformationSettings_IFrame').style.display = '';
    document.getElementById('DialogPersonnelExtraInformationSettings_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogPersonnelExtraInformationSettings_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogPersonnelExtraInformationSettings_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogPersonnelExtraInformationSettings_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogPersonnelExtraInformationSettings_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogPersonnelExtraInformationSettings').align = 'left';
    }
    if (CurrentLangID == 'en-US') {
        document.getElementById('CloseButton_DialogPersonnelExtraInformationSettings').align = 'right';
    }
}


function DialogPersonnelExtraInformationSettings_onClose(sender, e) {
    document.getElementById('DialogPersonnelExtraInformationSettings_IFrame').style.display = 'none';
    document.getElementById('DialogPersonnelExtraInformationSettings_IFrame').style.visibility = 'hidden';
    DialogPersonnelExtraInformationSettings.set_contentUrl("about:blank");
}





