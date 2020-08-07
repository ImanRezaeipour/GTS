
function DialogConceptsManagement_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogConceptsManagement.set_contentUrl("ConceptsManagement.aspx");
    document.getElementById('DialogConceptsManagement_IFrame').style.display = '';
    document.getElementById('DialogConceptsManagement_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogConceptsManagement_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogConceptsManagement_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogConceptsManagement_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogConceptsManagement_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogConceptsManagement').align = 'left';
    }
    if (CurrentLangID == 'en-US') {
        document.getElementById('CloseButton_DialogConceptsManagement').align = 'right';
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
    document.getElementById('tbl_DialogConceptsManagementheader').dir = document.getElementById('tbl_DialogConceptsManagementfooter').dir = direction;

    ChangeStyle_DialogConceptsManagement();
}

function DialogConceptsManagement_onClose(sender, e) {
    document.getElementById('DialogConceptsManagement_IFrame').style.display = 'none';
    document.getElementById('DialogConceptsManagement_IFrame').style.visibility = 'hidden';
    DialogConceptsManagement.set_contentUrl("about:blank");
}

function ChangeStyle_DialogConceptsManagement() {
    document.getElementById('DialogConceptsManagement_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById('DialogConceptsManagement_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogConceptsManagementheader').style.width = document.getElementById('tbl_DialogConceptsManagementfooter').style.width = (screen.width - 7).toString() + 'px';
}
