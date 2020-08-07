

function DialogAccessGroups_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogAccessGroups = DialogAccessGroups.get_value();
    DialogAccessGroups.set_contentUrl("AccessGroups.aspx?FlowState=" + CharToKeyCode_AccessGroups(ObjDialogAccessGroups.FlowState) + "");
    document.getElementById('DialogAccessGroups_IFrame').style.display = '';
    document.getElementById('DialogAccessGroups_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogAccessGroups_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogAccessGroups_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogAccessGroups_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogAccessGroups_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogAccessGroups').align = 'left';
    }
    if (CurrentLangID == 'en-US') 
        document.getElementById('CloseButton_DialogAccessGroups').align = 'right';
}

function DialogAccessGroups_onClose(sender, e) {
    document.getElementById('DialogAccessGroups_IFrame').style.display = 'none';
    document.getElementById('DialogAccessGroups_IFrame').style.visibility = 'hidden';
    DialogAccessGroups.set_contentUrl("about:blank");
}

function CharToKeyCode_AccessGroups(str) {
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

