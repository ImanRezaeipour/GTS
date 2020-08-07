

function DialogManagersWorkFlow_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogManagersWorkFlow = DialogManagersWorkFlow.get_value();
    DialogManagersWorkFlow.set_contentUrl("ManagersWorkFlow.aspx");
    document.getElementById('DialogManagersWorkFlow_IFrame').style.display = '';
    document.getElementById('DialogManagersWorkFlow_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogManagersWorkFlow_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogManagersWorkFlow_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogManagersWorkFlow_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogManagersWorkFlow_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogManagersWorkFlow').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogManagersWorkFlow').align = 'right';
}

function DialogManagersWorkFlow_onClose(sender, e) {
    document.getElementById('DialogManagersWorkFlow_IFrame').style.display = 'none';
    document.getElementById('DialogManagersWorkFlow_IFrame').style.visibility = 'hidden';
    DialogManagersWorkFlow.set_contentUrl("about:blank");
}


