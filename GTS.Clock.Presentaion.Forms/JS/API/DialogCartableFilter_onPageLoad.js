

function DialogCartableFilter_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogCartableFilter.set_contentUrl("CartableFilter.aspx");
    document.getElementById('DialogCartableFilter_IFrame').style.display = '';
    document.getElementById('DialogCartableFilter_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogCartableFilter_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogCartableFilter_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogCartableFilter_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogCartableFilter_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogCartableFilter').align = 'left';
        document.getElementById('tbl_DialogCartableFilterheader').dir = 'rtl';
        document.getElementById('tbl_DialogCartableFilterfooter').dir = 'rtl';        
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogCartableFilter').align = 'right';
}

function DialogCartableFilter_onClose(sender, e) {
    document.getElementById('DialogCartableFilter_IFrame').style.display = 'none';
    document.getElementById('DialogCartableFilter_IFrame').style.visibility = 'hidden';
    DialogCartableFilter.set_contentUrl("about:blank");
}
