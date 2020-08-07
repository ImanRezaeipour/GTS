
var DialogKartableMode = '';

function DialogKartable_onShow(sender, e) {
    CurrentLangID = parent.CurrentLangID;
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    var contentUrl_DialogKartable = "Kartable.aspx?RequestCaller=" + CharToKeyCode(RequestCaller);
    DialogKartable.set_contentUrl(contentUrl_DialogKartable);
    document.getElementById('DialogKartable_IFrame').style.display = '';
    document.getElementById('DialogKartable_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogKartable_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogKartable_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogKartable_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogKartable_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogKartable').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogKartable').align = 'right';

    ChangeStyle_DialogKartable();
}

function DialogKartable_onClose(sender, e) {
    document.getElementById('DialogKartable_IFrame').style.display = 'none';
    document.getElementById('DialogKartable_IFrame').style.visibility = 'hidden';
    DialogKartable.set_contentUrl("about:blank");
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

function ChangeStyle_DialogKartable() {
    document.getElementById('DialogKartable_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById('DialogKartable_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogKartableheader').style.width = document.getElementById('tbl_DialogKartablefooter').style.width = (screen.width - 7).toString() + 'px';
}


