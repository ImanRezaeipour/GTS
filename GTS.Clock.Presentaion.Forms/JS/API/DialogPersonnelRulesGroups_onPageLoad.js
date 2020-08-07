
function DialogPersonnelRulesGroups_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogPersonnelRulesGroups.set_contentUrl("PersonnelRulesGroups.aspx");
    document.getElementById('DialogPersonnelRulesGroups_IFrame').style.display = '';
    document.getElementById('DialogPersonnelRulesGroups_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogPersonnelRulesGroups_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogPersonnelRulesGroups_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogPersonnelRulesGroups_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogPersonnelRulesGroups_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogPersonnelRulesGroups').align = 'left';
        document.getElementById('tbl_DialogPersonnelRulesGroupsheader').dir = 'rtl';
        document.getElementById('tbl_DialogPersonnelRulesGroupsfooter').dir = 'rtl';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogPersonnelRulesGroups').align = 'right';
}

function DialogPersonnelRulesGroups_onClose(sender, e) {
    document.getElementById('DialogPersonnelRulesGroups_IFrame').style.display = 'none';
    document.getElementById('DialogPersonnelRulesGroups_IFrame').style.visibility = 'hidden';
    DialogPersonnelRulesGroups.set_contentUrl("about:blank");
}
