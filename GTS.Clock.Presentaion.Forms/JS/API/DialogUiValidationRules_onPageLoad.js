function DialogUiValidationRules_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogUiValidationRules.set_contentUrl("UiValidationRules.aspx");
    document.getElementById('DialogUiValidationRules_IFrame').style.display = '';
    document.getElementById('DialogUiValidationRules_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogUiValidationRules_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogUiValidationRules_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogUiValidationRules_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogUiValidationRules_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogUiValidationRules').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogUiValidationRules').align = 'right';
    
}

function DialogUiValidationRules_onClose(sender, e) {
    document.getElementById('DialogUiValidationRules_IFrame').style.display = 'none';
    document.getElementById('DialogUiValidationRules_IFrame').style.visibility = 'hidden';
    DialogUiValidationRules.set_contentUrl("about:blank");
}
