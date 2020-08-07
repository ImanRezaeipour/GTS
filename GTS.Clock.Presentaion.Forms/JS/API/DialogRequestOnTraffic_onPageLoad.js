

function DialogRequestOnTraffic_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogRequestOnTraffic = DialogRequestOnTraffic.get_value();
    DialogRequestOnTraffic.set_contentUrl("RequestOnTraffic.aspx?RC=" + CharToKeyCode_RequestOnTraffic(ObjDialogRequestOnTraffic.RequestCaller) + "&RLS=" + CharToKeyCode_RequestOnTraffic(ObjDialogRequestOnTraffic.LoadState) + "");
    document.getElementById('DialogRequestOnTraffic_IFrame').style.display = '';
    document.getElementById('DialogRequestOnTraffic_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogRequestOnTraffic_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogRequestOnTraffic_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogRequestOnTraffic_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogRequestOnTraffic_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogRequestOnTraffic').align = 'left';
        document.getElementById('tbl_DialogRequestOnTrafficheader').dir = 'rtl';
        document.getElementById('tbl_DialogRequestOnTrafficfooter').dir = 'rtl';        
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogRequestOnTraffic').align = 'right';
}

function DialogRequestOnTraffic_onClose(sender, e) {
    document.getElementById('DialogRequestOnTraffic_IFrame').style.display = 'none';
    document.getElementById('DialogRequestOnTraffic_IFrame').style.visibility = 'hidden';
    DialogRequestOnTraffic.set_contentUrl("about:blank");
}

function CharToKeyCode_RequestOnTraffic(str) {
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

