function DialogSystemMessage_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogSystemMessage.set_contentUrl("SystemMessage.aspx");
    document.getElementById('DialogSystemMessage_IFrame').style.display = '';
    document.getElementById('DialogSystemMessage_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogSystemMessage_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogSystemMessage_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogSystemMessage_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogSystemMessage_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogSystemMessage').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogSystemMessage').align = 'right';
    
}

function DialogSystemMessage_onClose(sender, e) {
    document.getElementById('DialogSystemMessage_IFrame').style.display = 'none';
    document.getElementById('DialogSystemMessage_IFrame').style.visibility = 'hidden';
    DialogSystemMessage.set_contentUrl("about:blank");
}
