
var DialogCartableMode = '';

function DialogCartable_onShow(sender, e) {
    CurrentLangID = parent.CurrentLangID;
    var ObjDialogCartable = parent.DialogCartable.get_value();
    var RequestCaller = ObjDialogCartable.RequestCaller;
    var contentUrl_DialogCartable = "Cartable.aspx?RequestCaller=" + CharToKeyCode(RequestCaller);
    DialogCartable.set_contentUrl(contentUrl_DialogCartable);
    document.getElementById('DialogCartable_IFrame').style.display = '';
    document.getElementById('DialogCartable_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogCartable_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogCartable_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogCartable_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogCartable_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogCartable').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogCartable').align = 'right';

    ChangeStyle_DialogCartable();
}

function DialogCartable_onClose(sender, e) {
    document.getElementById('DialogCartable_IFrame').style.display = 'none';
    document.getElementById('DialogCartable_IFrame').style.visibility = 'hidden';
    DialogCartable.set_contentUrl("about:blank");
}

function CharToKeyCode(str) {
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

function ChangeStyle_DialogCartable() {
    document.getElementById('DialogCartable_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById('DialogCartable_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogCartableheader').style.width = document.getElementById('tbl_DialogCartablefooter').style.width = (screen.width - 7).toString() + 'px';
}


