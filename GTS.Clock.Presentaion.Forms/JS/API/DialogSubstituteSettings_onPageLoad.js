


function DialogSubstituteSettings_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogSubstituteSettings.set_contentUrl("SubstituteSettings.aspx");
    document.getElementById('DialogSubstituteSettings_IFrame').style.display = '';
    document.getElementById('DialogSubstituteSettings_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogSubstituteSettings_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogSubstituteSettings_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogSubstituteSettings_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogSubstituteSettings_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogSubstituteSettings').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogSubstituteSettings').align = 'right';
}

function DialogSubstituteSettings_onClose(sender, e) {
    document.getElementById('DialogSubstituteSettings_IFrame').style.display = 'none';
    document.getElementById('DialogSubstituteSettings_IFrame').style.visibility = 'hidden';
    DialogSubstituteSettings.set_contentUrl("about:blank");
}
