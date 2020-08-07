
function DialogCalendar_onShow() {
    document.getElementById('DialogCalendar_IFrame').style.display = '';
    document.getElementById('DialogCalendar_IFrame').style.visibility = 'visible';
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('DialogCalendar_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogCalendar_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogCalendar_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogCalendar_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogCalendar').align = 'left';        
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogCalendar').align = 'right';
}

function DialogCalendar_onClose(sender, e) {
    document.getElementById('DialogCalendar_IFrame').style.display = 'none';
    document.getElementById('DialogCalendar_IFrame').style.visibility = 'hidden';
    DialogCalendar.set_contentUrl("about:blank");
}

