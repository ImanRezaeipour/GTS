

function DialogUserInterfaceAccessLevels_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogUserInterfaceAccessLevels.set_contentUrl("UserInterfaceAccessLevels.aspx");
    document.getElementById('DialogUserInterfaceAccessLevels_IFrame').style.display = '';
    document.getElementById('DialogUserInterfaceAccessLevels_IFrame').style.visibility = 'visible';
    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogUserInterfaceAccessLevels_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogUserInterfaceAccessLevels_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogUserInterfaceAccessLevels_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogUserInterfaceAccessLevels_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('Title_DialogUserInterfaceAccessLevels').innerHTML = 'تخصیص سطوح دسترسی واسط کاربری';
        document.getElementById('CloseButton_DialogUserInterfaceAccessLevels').align = 'left';
    }
    if (CurrentLangID == 'en-US') {
        document.getElementById('Title_DialogUserInterfaceAccessLevels').innerHTML = 'User Interface Access Levels Asignment';
        document.getElementById('CloseButton_DialogUserInterfaceAccessLevels').align = 'right';
    }
}

function DialogUserInterfaceAccessLevels_onClose(sender, e) {
    document.getElementById('DialogUserInterfaceAccessLevels_IFrame').style.display = 'none';
    document.getElementById('DialogUserInterfaceAccessLevels_IFrame').style.visibility = 'hidden';
    DialogUserInterfaceAccessLevels.set_contentUrl("about:blank");
}
