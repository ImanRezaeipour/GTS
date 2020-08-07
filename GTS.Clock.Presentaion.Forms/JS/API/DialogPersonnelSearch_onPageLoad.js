

function DialogPersonnelSearch_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogPersonnelSearch.set_contentUrl("PersonnelSearch.aspx");
    document.getElementById('DialogPersonnelSearch_IFrame').style.display = '';
    document.getElementById('DialogPersonnelSearch_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogPersonnelSearch_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogPersonnelSearch_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogPersonnelSearch_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogPersonnelSearch_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogPersonnelSearch').align = 'left';
    }
    if (CurrentLangID == 'en-US') {
        document.getElementById('CloseButton_DialogPersonnelSearch').align = 'right';
    }

    ChangeStyle_DialogPersonnelSearch();
}

function DialogPersonnelSearch_onClose(sender, e) {
    document.getElementById('DialogPersonnelSearch_IFrame').style.display = 'none';
    document.getElementById('DialogPersonnelSearch_IFrame').style.visibility = 'hidden';
    DialogPersonnelSearch.set_contentUrl("about:blank");
}

function ChangeStyle_DialogPersonnelSearch() {
    document.getElementById('DialogPersonnelSearch_IFrame').style.width = (screen.width - 50).toString() + 'px';
    document.getElementById('DialogPersonnelSearch_IFrame').style.height = (0.66 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogPersonnelSearchheader').style.width = document.getElementById('tbl_DialogPersonnelSearchfooter').style.width = (screen.width - 47).toString() + 'px';
}

