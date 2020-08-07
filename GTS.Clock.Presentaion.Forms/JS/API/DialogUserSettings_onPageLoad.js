
var CurrentLangID = null;
function DialogUserSettings_onShow(sender, e) {
    CurrentLangID = parent.CurrentLangID;
    var ObjDialogUserSettings = DialogUserSettings.get_value();
    var Caller = ObjDialogUserSettings.Caller;
    DialogUserSettings.set_contentUrl("UserSettings.aspx?Caller="+CharToKeyCode_UserSettings(Caller)+"");
    document.getElementById('DialogUserSettings_IFrame').style.display = '';
    document.getElementById('DialogUserSettings_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogUserSettings_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogUserSettings_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogUserSettings_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogUserSettings_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogUserSettings').align = 'left';
        document.getElementById('tbl_DialogUserSettingsheader').dir = 'rtl';
        document.getElementById('tbl_DialogUserSettingsfooter').dir = 'rtl';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogUserSettings').align = 'right';
}

function DialogUserSettings_onClose(sender, e) {
    document.getElementById('DialogUserSettings_IFrame').style.display = 'none';
    document.getElementById('DialogUserSettings_IFrame').style.visibility = 'hidden';
    DialogUserSettings.set_contentUrl("about:blank");
}

function CharToKeyCode_UserSettings(str) {
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


