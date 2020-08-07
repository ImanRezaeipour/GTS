
function DialogRulesManagement_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogRulesManagement.set_contentUrl("RulesManagement.aspx");
    document.getElementById('DialogRulesManagement_IFrame').style.display = '';
    document.getElementById('DialogRulesManagement_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogRulesManagement_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogRulesManagement_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogRulesManagement_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogRulesManagement_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogRulesManagement').align = 'left';
    }
    if (CurrentLangID == 'en-US') {
        document.getElementById('CloseButton_DialogRulesManagement').align = 'right';
    }

    var direction = null;
    switch (CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    document.getElementById('tbl_DialogRulesManagementheader').dir = document.getElementById('tbl_DialogRulesManagementfooter').dir = direction;

    ChangeStyle_DialogRulesManagement();
}

function DialogRulesManagement_onClose(sender, e) {
    document.getElementById('DialogRulesManagement_IFrame').style.display = 'none';
    document.getElementById('DialogRulesManagement_IFrame').style.visibility = 'hidden';
    DialogRulesManagement.set_contentUrl("about:blank");
}

function ChangeStyle_DialogRulesManagement() {
    document.getElementById('DialogRulesManagement_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById('DialogRulesManagement_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogRulesManagementheader').style.width = document.getElementById('tbl_DialogRulesManagementfooter').style.width = (screen.width - 7).toString() + 'px';
}
