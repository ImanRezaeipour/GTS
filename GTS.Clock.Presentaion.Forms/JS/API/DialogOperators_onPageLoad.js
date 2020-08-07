

function DialogOperators_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogOperators.set_contentUrl("Operators.aspx");
    document.getElementById('DialogOperators_IFrame').style.display = '';
    document.getElementById('DialogOperators_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogOperators_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogOperators_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogOperators_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogOperators_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogOperators').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogOperators').align = 'right';
}

function DialogOperators_onClose(sender, e) {
    document.getElementById('DialogOperators_IFrame').style.display = 'none';
    document.getElementById('DialogOperators_IFrame').style.visibility = 'hidden';
    DialogOperators.set_contentUrl("about:blank");
}
