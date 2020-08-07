
function DialogMasterRulesView_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogMasterRulesView.set_contentUrl("MasterRulesView.aspx");
    document.getElementById('DialogMasterRulesView_IFrame').style.display = '';
    document.getElementById('DialogMasterRulesView_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogMasterRulesView_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogMasterRulesView_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogMasterRulesView_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogMasterRulesView_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogMasterRulesView').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogMasterRulesView').align = 'right';
}

function DialogMasterRulesView_onClose(sender, e) {
    document.getElementById('DialogMasterRulesView_IFrame').style.display = 'none';
    document.getElementById('DialogMasterRulesView_IFrame').style.visibility = 'hidden';
    DialogMasterRulesView.set_contentUrl("about:blank");
}
