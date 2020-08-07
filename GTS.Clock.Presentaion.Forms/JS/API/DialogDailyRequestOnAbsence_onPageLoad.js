

function DialogDailyRequestOnAbsence_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogDailyRequestOnAbsence = DialogDailyRequestOnAbsence.get_value();
    DialogDailyRequestOnAbsence.set_contentUrl("DailyRequestOnAbsence.aspx?RC=" + CharToKeyCode_DailyRequestOnAbsence(ObjDialogDailyRequestOnAbsence.RequestCaller) + "&RLS=" + CharToKeyCode_DailyRequestOnAbsence(ObjDialogDailyRequestOnAbsence.LoadState) + "");
    document.getElementById('DialogDailyRequestOnAbsence_IFrame').style.display = '';
    document.getElementById('DialogDailyRequestOnAbsence_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogDailyRequestOnAbsence_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogDailyRequestOnAbsence_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogDailyRequestOnAbsence_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogDailyRequestOnAbsence_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogDailyRequestOnAbsence').align = 'left';
        document.getElementById('tbl_DialogDailyRequestOnAbsenceheader').dir = 'rtl';
        document.getElementById('tbl_DialogDailyRequestOnAbsencefooter').dir = 'rtl';        
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogDailyRequestOnAbsence').align = 'right';
}

function DialogDailyRequestOnAbsence_onClose(sender, e) {
    document.getElementById('DialogDailyRequestOnAbsence_IFrame').style.display = 'none';
    document.getElementById('DialogDailyRequestOnAbsence_IFrame').style.visibility = 'hidden';
    DialogDailyRequestOnAbsence.set_contentUrl("about:blank");
}

function CharToKeyCode_DailyRequestOnAbsence(str) {
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

