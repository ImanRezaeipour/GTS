

function DialogRequestOnUnallowableOverTime_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogRequestOnUnallowableOverTime = DialogRequestOnUnallowableOverTime.get_value();
    DialogRequestOnUnallowableOverTime.set_contentUrl("RequestOnUnallowableOverTime.aspx?RC=" + CharToKeyCode_RequestOnUnallowableOverTime(ObjDialogRequestOnUnallowableOverTime.RequestCaller) + "&RLS=" + CharToKeyCode_RequestOnUnallowableOverTime(ObjDialogRequestOnUnallowableOverTime.LoadState) + "");
    document.getElementById('DialogRequestOnUnallowableOverTime_IFrame').style.display = '';
    document.getElementById('DialogRequestOnUnallowableOverTime_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogRequestOnUnallowableOverTime_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogRequestOnUnallowableOverTime_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogRequestOnUnallowableOverTime_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogRequestOnUnallowableOverTime_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogRequestOnUnallowableOverTime').align = 'left';
        document.getElementById('tbl_DialogRequestOnUnallowableOverTimeheader').dir = 'rtl';
        document.getElementById('tbl_DialogRequestOnUnallowableOverTimefooter').dir = 'rtl';        
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogRequestOnUnallowableOverTime').align = 'right';
}

function DialogRequestOnUnallowableOverTime_onClose(sender, e) {
    document.getElementById('DialogRequestOnUnallowableOverTime_IFrame').style.display = 'none';
    document.getElementById('DialogRequestOnUnallowableOverTime_IFrame').style.visibility = 'hidden';
    DialogRequestOnUnallowableOverTime.set_contentUrl("about:blank");
}

function CharToKeyCode_RequestOnUnallowableOverTime(str) {
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

