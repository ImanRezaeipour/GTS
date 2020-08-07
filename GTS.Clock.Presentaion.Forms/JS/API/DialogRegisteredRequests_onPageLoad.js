
function DialogRegisteredRequests_onShow(sender, e) {
    CurrentLangID = parent.CurrentLangID;
    var ObjDialogRegisteredRequests = DialogRegisteredRequests.get_value();
    DialogRegisteredRequests.set_contentUrl("RegisteredRequests.aspx?Caller=" + CharToKeyCode_RegisteredRequests(ObjDialogRegisteredRequests.Caller) + "");
    document.getElementById('DialogRegisteredRequests_IFrame').style.display = '';
    document.getElementById('DialogRegisteredRequests_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogRegisteredRequests_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogRegisteredRequests_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogRegisteredRequests_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogRegisteredRequests_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogRegisteredRequests').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogRegisteredRequests').align = 'right';

    ChangeStyle_DialogRegisteredRequests();
}

function DialogRegisteredRequests_onClose(sender, e) {
    document.getElementById('DialogRegisteredRequests_IFrame').style.display = 'none';
    document.getElementById('DialogRegisteredRequests_IFrame').style.visibility = 'hidden';
    DialogRegisteredRequests.set_contentUrl("about:blank");
}

function ChangeStyle_DialogRegisteredRequests() {
    document.getElementById('DialogRegisteredRequests_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById('DialogRegisteredRequests_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogRegisteredRequestsheader').style.width = document.getElementById('tbl_DialogRegisteredRequestsfooter').style.width = (screen.width - 7).toString() + 'px';
}

function CharToKeyCode_RegisteredRequests(str) {
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

