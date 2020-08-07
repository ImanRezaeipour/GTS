
function DialogHistory_onShow(sender, e) {
    CurrentLangID = parent.CurrentLangID;
    var ObjHistory = DialogHistory.get_value();
    var RequestID = ObjHistory.RequestID;
    var contentUrl_DialogHistory = "History.aspx?RequestID=" + CharToKeyCode_History(RequestID);
    DialogHistory.set_contentUrl(contentUrl_DialogHistory);
    document.getElementById('DialogHistory_IFrame').style.display = '';
    document.getElementById('DialogHistory_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogHistory_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogHistory_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogHistory_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogHistory_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogHistory').align = 'left';
        document.getElementById('tbl_DialogHistoryheader').dir = 'rtl';
        document.getElementById('tbl_DialogHistoryfooter').dir = 'rtl';        
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogHistory').align = 'right';
}

function DialogHistory_onClose(sender, e) {
    document.getElementById('DialogHistory_IFrame').style.display = 'none';
    document.getElementById('DialogHistory_IFrame').style.visibility = 'hidden';
    DialogHistory.set_contentUrl("about:blank");
}

function CharToKeyCode_History(str) {
    var OutStr = '';
    if (str != null && str != undefined) {
        for (var i = 0; i < str.length; i++) {
            var KeyCode = str.charCodeAt(i);
            var CharKeyCode = '//' + KeyCode;
            OutStr += CharKeyCode;
        }
    }
    return OutStr;
}
