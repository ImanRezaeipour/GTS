
function DialogMasterDataAccessLevels_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogMasterDataAccessLevels.set_contentUrl("MasterDataAccessLevels.aspx?reload=" + (new Date()).getTime() + "");
    document.getElementById('DialogMasterDataAccessLevels_IFrame').style.display = '';
    document.getElementById('DialogMasterDataAccessLevels_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogMasterDataAccessLevels_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogMasterDataAccessLevels_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogMasterDataAccessLevels_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogMasterDataAccessLevels_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogMasterDataAccessLevels').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogMasterDataAccessLevels').align = 'right';
}

function DialogMasterDataAccessLevels_onClose(sender, e) {
    document.getElementById('DialogMasterDataAccessLevels_IFrame').style.display = 'none';
    document.getElementById('DialogMasterDataAccessLevels_IFrame').style.visibility = 'hidden';
    DialogMasterDataAccessLevels.set_contentUrl("about:blank");
}








