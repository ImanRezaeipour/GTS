

function DialogPeriodRepeat_onShow(sender, e) {
    CurrentLangID = parent.CurrentLangID;
    document.getElementById('DialogPeriodRepeat_IFrame').style.display = '';
    document.getElementById('DialogPeriodRepeat_IFrame').style.visibility = 'visible';
    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogPeriodRepeat_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogPeriodRepeat_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogPeriodRepeat_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogPeriodRepeat_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogPeriodRepeat').align = 'left';
        document.getElementById('tbl_DialogPeriodRepeatheader').dir = 'rtl';
        document.getElementById('tbl_DialogPeriodRepeatfooter').dir = 'rtl';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogPeriodRepeat').align = 'right';
}

function DialogPeriodRepeat_onClose(sender, e) {
    document.getElementById('DialogPeriodRepeat_IFrame').style.display = 'none';
    document.getElementById('DialogPeriodRepeat_IFrame').style.visibility = 'hidden';
    DialogPeriodRepeat.set_contentUrl("about:blank");
}
