function DialogConceptRuleEditor_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogConceptRuleEditor.set_contentUrl("ConceptRuleEditor.aspx");
    document.getElementById('DialogConceptRuleEditor_IFrame').style.display = '';
    document.getElementById('DialogConceptRuleEditor_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogConceptRuleEditor_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogConceptRuleEditor_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogConceptRuleEditor_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogConceptRuleEditor_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogConceptRuleEditor').align = 'left';
    }
    if (CurrentLangID == 'en-US') {
        document.getElementById('CloseButton_DialogConceptRuleEditor').align = 'right';
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
    document.getElementById('tbl_DialogConceptRuleEditorheader').dir = document.getElementById('tbl_DialogConceptRuleEditorfooter').dir = direction;

    ChangeStyle_DialogConceptRuleEditor();
}

function DialogConceptRuleEditor_onClose(sender, e) {
    document.getElementById('DialogConceptRuleEditor_IFrame').style.display = 'none';
    document.getElementById('DialogConceptRuleEditor_IFrame').style.visibility = 'hidden';
    DialogConceptRuleEditor.set_contentUrl("about:blank");
}

function ChangeStyle_DialogConceptRuleEditor() {
    document.getElementById('DialogConceptRuleEditor_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById('DialogConceptRuleEditor_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('DialogConceptRuleEditor_IFrame').style.left = '0px';
    document.getElementById('tbl_DialogConceptRuleEditorheader').style.width = document.getElementById('tbl_DialogConceptRuleEditorfooter').style.width = (screen.width - 7).toString() + 'px';
    document.getElementById('tbl_DialogConceptRuleEditorheader').style.left = document.getElementById('tbl_DialogConceptRuleEditorfooter').style.left = '0px';
}