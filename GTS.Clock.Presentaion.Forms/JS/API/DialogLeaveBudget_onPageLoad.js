

function DialogLeaveBudget_onShow(sender, e) {
    var ObjDialogLeaveBudget = DialogLeaveBudget.get_value();
    var RuleGroupID = ObjDialogLeaveBudget.RuleGroupID;
    DialogLeaveBudget.set_contentUrl("LeaveBudget.aspx?RuleGroupID="+CharToKeyCode_DialogLeaveBudget(RuleGroupID)+"");
    document.getElementById('DialogLeaveBudget_IFrame').style.display = '';
    document.getElementById('DialogLeaveBudget_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogLeaveBudget_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogLeaveBudget_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogLeaveBudget_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogLeaveBudget_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogLeaveBudget').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogLeaveBudget').align = 'right';
}

function DialogLeaveBudget_onClose(sender, e) {
    document.getElementById('DialogLeaveBudget_IFrame').style.display = 'none';
    document.getElementById('DialogLeaveBudget_IFrame').style.visibility = 'hidden';
    DialogLeaveBudget.set_contentUrl("about:blank");
}

function CharToKeyCode_DialogLeaveBudget(str) {
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

