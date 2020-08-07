

function DialogMainViewMaximizedPart_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogMainViewMaximizedPart_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogMainViewMaximizedPart_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogMainViewMaximizedPart_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogMainViewMaximizedPart_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogMainViewMaximizedPart').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogMainViewMaximizedPart').align = 'right';
}

function DialogMainViewMaximizedPart_onClose(sender, e) {
    DialogMainViewMaximizedPart.set_contentUrl("about:blank");
}










