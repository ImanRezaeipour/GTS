
function DialogCollectiveTraffic_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogCollectiveTraffic.set_contentUrl("CollectiveTraffic.aspx");
    document.getElementById('DialogCollectiveTraffic_IFrame').style.display = '';
    document.getElementById('DialogCollectiveTraffic_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogCollectiveTraffic_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogCollectiveTraffic_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogCollectiveTraffic_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogCollectiveTraffic_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogCollectiveTraffic').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogCollectiveTraffic').align = 'right';
}

function DialogCollectiveTraffic_onClose(sender, e) {
    document.getElementById('DialogCollectiveTraffic_IFrame').style.display = 'none';
    document.getElementById('DialogCollectiveTraffic_IFrame').style.visibility = 'hidden';
    DialogCollectiveTraffic.set_contentUrl("about:blank");
}
