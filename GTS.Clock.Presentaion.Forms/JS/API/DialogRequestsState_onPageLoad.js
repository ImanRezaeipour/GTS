
function DialogRequestsState_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogRequestsState.set_contentUrl("RequestsState.aspx");
    document.getElementById('DialogRequestsState_IFrame').style.display = '';
    document.getElementById('DialogRequestsState_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogRequestsState_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogRequestsState_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogRequestsState_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogRequestsState_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogRequestsState').align = 'left';
        document.getElementById('tbl_DialogRequestsStateheader').dir = 'rtl';
        document.getElementById('tbl_DialogRequestsStatefooter').dir = 'rtl';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogRequestsState').align = 'right';
}

function DialogRequestsState_onClose(sender, e) {
    document.getElementById('DialogRequestsState_IFrame').style.display = 'none';
    document.getElementById('DialogRequestsState_IFrame').style.visibility = 'hidden';
    DialogRequestsState.set_contentUrl("about:blank");
}
