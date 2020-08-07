
function DialogRequestRegister_onShow(sender, e) {
    CurrentLangID = parent.CurrentLangID;
    var ObjDialogRequestRegister = DialogRequestRegister.get_value();
    var RequestCaller = ObjDialogRequestRegister.Caller;
    DialogRequestRegister.set_contentUrl("RequestRegister.aspx?RequestCaller=" + CharToKeyCode_RequestRegister(RequestCaller) + "");
    document.getElementById('DialogRequestRegister_IFrame').style.display = '';
    document.getElementById('DialogRequestRegister_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogRequestRegister_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogRequestRegister_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogRequestRegister_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogRequestRegister_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogRequestRegister').align = 'left';
        document.getElementById('tbl_DialogRequestRegisterheader').dir = 'rtl';
        document.getElementById('tbl_DialogRequestRegisterfooter').dir = 'rtl';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogRequestRegister').align = 'right';
}

function DialogRequestRegister_onClose(sender, e) {
    document.getElementById('DialogRequestRegister_IFrame').style.display = 'none';
    document.getElementById('DialogRequestRegister_IFrame').style.visibility = 'hidden';
    DialogRequestRegister.set_contentUrl("about:blank");
}

function CharToKeyCode_RequestRegister(str) {
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

