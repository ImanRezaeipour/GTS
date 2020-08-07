
function DialogPersonnelSingleDateFeatures_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogPersonnelSingleDateFeatures = DialogPersonnelSingleDateFeatures.get_value();
    var Caller = ObjDialogPersonnelSingleDateFeatures.Caller;
    DialogPersonnelSingleDateFeatures.set_contentUrl('PersonnelSingleDateFeatures.aspx?Caller=' + CharToKeyCode_PersonnelSingleDateFeatures(Caller));
    document.getElementById('DialogPersonnelSingleDateFeatures_IFrame').style.display = '';
    document.getElementById('DialogPersonnelSingleDateFeatures_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogPersonnelSingleDateFeatures_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogPersonnelSingleDateFeatures_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogPersonnelSingleDateFeatures_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogPersonnelSingleDateFeatures_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogPersonnelSingleDateFeatures').align = 'left';
        document.getElementById('tbl_DialogPersonnelSingleDateFeaturesheader').dir = 'rtl';
        document.getElementById('tbl_DialogPersonnelSingleDateFeaturesfooter').dir = 'rtl';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogPersonnelSingleDateFeatures').align = 'right';
}

function DialogPersonnelSingleDateFeatures_onClose(sender, e) {
    document.getElementById('DialogPersonnelSingleDateFeatures_IFrame').style.display = 'none';
    document.getElementById('DialogPersonnelSingleDateFeatures_IFrame').style.visibility = 'hidden';
    DialogPersonnelSingleDateFeatures.set_contentUrl("about:blank");
}

function CharToKeyCode_PersonnelSingleDateFeatures(str) {
    var OutStr = '';
    if (str != null && str != undefined) {
        for (var i = 0; i < str.length; i++) {
            var KeyCode = str.charCodeAt(i);
            var CharKeyCode = '//' + KeyCode;
            OutStr += CharKeyCode;
        }
    }
    return OutStr;
}

