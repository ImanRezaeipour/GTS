

function DialogActiveDirectory_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogActiveDirectory.set_contentUrl("ActiveDirectory.aspx");
    document.getElementById('DialogActiveDirectory_IFrame').style.display = '';
    document.getElementById('DialogActiveDirectory_IFrame').style.visibility = 'visible';
    document.getElementById('Title_DialogActiveDirectory').innerHTML = Title_DialogActiveDirectory;

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogActiveDirectory_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogActiveDirectory_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogActiveDirectory_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogActiveDirectory_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogActiveDirectory').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogActiveDirectory').align = 'right';
}

function DialogActiveDirectory_onClose(sender, e) {
    document.getElementById('DialogActiveDirectory_IFrame').style.display = 'none';
    document.getElementById('DialogActiveDirectory_IFrame').style.visibility = 'hidden';
    DialogActiveDirectory.set_contentUrl("about:blank");
}
