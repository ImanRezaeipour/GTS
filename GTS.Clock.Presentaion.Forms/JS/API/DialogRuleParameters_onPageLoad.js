
function DialogRuleParameters_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogRuleParameters.set_contentUrl("RuleParameters.aspx");
    document.getElementById('DialogRuleParameters_IFrame').style.display = '';
    document.getElementById('DialogRuleParameters_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogRuleParameters_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogRuleParameters_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogRuleParameters_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogRuleParameters_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogRuleParameters').align = 'left';
        document.getElementById('tbl_DialogRuleParametersheader').dir = 'rtl';
        document.getElementById('tbl_DialogRuleParametersfooter').dir = 'rtl';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogRuleParameters').align = 'right';
}

function DialogRuleParameters_onClose(sender, e) {
    document.getElementById('DialogRuleParameters_IFrame').style.display = 'none';
    document.getElementById('DialogRuleParameters_IFrame').style.visibility = 'hidden';
    DialogRuleParameters.set_contentUrl("about:blank");
}
