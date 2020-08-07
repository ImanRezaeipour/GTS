
function DialogEndorsementFlowState_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogEndorsementFlowState = DialogEndorsementFlowState.get_value();
    var ManagerFlowID = ObjDialogEndorsementFlowState.ManagerFlowID;
    DialogEndorsementFlowState.set_contentUrl("EndorsementFlowState.aspx?ManagerFlowID=" + CharToKeyCode_EndorsementFlowState(ManagerFlowID) + "");
    document.getElementById('DialogEndorsementFlowState_IFrame').style.display = '';
    document.getElementById('DialogEndorsementFlowState_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogEndorsementFlowState_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogEndorsementFlowState_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogEndorsementFlowState_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogEndorsementFlowState_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogEndorsementFlowState').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogEndorsementFlowState').align = 'right';
}

function DialogEndorsementFlowState_onClose(sender, e) {
    document.getElementById('DialogEndorsementFlowState_IFrame').style.display = 'none';
    document.getElementById('DialogEndorsementFlowState_IFrame').style.visibility = 'hidden';
    DialogEndorsementFlowState.set_contentUrl("about:blank");
}

function CharToKeyCode_EndorsementFlowState(str) {
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

