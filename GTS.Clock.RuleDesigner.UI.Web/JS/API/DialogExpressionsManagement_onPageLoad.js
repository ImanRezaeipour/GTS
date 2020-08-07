
function DialogExpressionsManagement_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogExpressionsManagement.set_contentUrl("ExpressionsManagement.aspx");
    document.getElementById('DialogExpressionsManagement_IFrame').style.display = '';
    document.getElementById('DialogExpressionsManagement_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogExpressionsManagement_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogExpressionsManagement_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogExpressionsManagement_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogExpressionsManagement_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogExpressionsManagement').align = 'left';
    }
    if (CurrentLangID == 'en-US') {
        document.getElementById('CloseButton_DialogExpressionsManagement').align = 'right';
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
    document.getElementById('tbl_DialogExpressionsManagementheader').dir = document.getElementById('tbl_DialogExpressionsManagementfooter').dir = direction;

    ChangeStyle_DialogExpressionsManagement();
}

function DialogExpressionsManagement_onClose(sender, e) {
    document.getElementById('DialogExpressionsManagement_IFrame').style.display = 'none';
    document.getElementById('DialogExpressionsManagement_IFrame').style.visibility = 'hidden';
    DialogExpressionsManagement.set_contentUrl("about:blank");
}

function ChangeStyle_DialogExpressionsManagement() {
    document.getElementById('DialogExpressionsManagement_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById('DialogExpressionsManagement_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogExpressionsManagementheader').style.width = document.getElementById('tbl_DialogExpressionsManagementfooter').style.width = (screen.width - 7).toString() + 'px';
}
