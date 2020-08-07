

function DialogHourlyRequestOnAbsence_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogHourlyRequestOnAbsence = DialogHourlyRequestOnAbsence.get_value();
    DialogHourlyRequestOnAbsence.set_contentUrl("HourlyRequestOnAbsence.aspx?RC=" + CharToKeyCode_HourlyRequestOnAbsence(ObjDialogHourlyRequestOnAbsence.RequestCaller) + "&RLS=" + CharToKeyCode_HourlyRequestOnAbsence(ObjDialogHourlyRequestOnAbsence.LoadState) + "");
    document.getElementById('DialogHourlyRequestOnAbsence_IFrame').style.display = '';
    document.getElementById('DialogHourlyRequestOnAbsence_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogHourlyRequestOnAbsence_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogHourlyRequestOnAbsence_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogHourlyRequestOnAbsence_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogHourlyRequestOnAbsence_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogHourlyRequestOnAbsence').align = 'left';
        document.getElementById('tbl_DialogHourlyRequestOnAbsence').dir = 'rtl';
        document.getElementById('tbl_DialogHourlyRequestOnAbsencefooter').dir = 'rtl';        
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogHourlyRequestOnAbsence').align = 'right';
}

function DialogHourlyRequestOnAbsence_onClose(sender, e) {
    document.getElementById('DialogHourlyRequestOnAbsence_IFrame').style.display = 'none';
    document.getElementById('DialogHourlyRequestOnAbsence_IFrame').style.visibility = 'hidden';
    DialogHourlyRequestOnAbsence.set_contentUrl("about:blank");
}

function CharToKeyCode_HourlyRequestOnAbsence(str) {
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

