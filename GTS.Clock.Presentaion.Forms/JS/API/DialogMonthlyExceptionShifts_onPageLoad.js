

function DialogMonthlyExceptionShifts_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogMonthlyExceptionShifts.set_contentUrl("MonthlyExceptionShifts.aspx?reload=" + (new Date()).getTime() + "");
    document.getElementById('DialogMonthlyExceptionShifts_IFrame').style.display = '';
    document.getElementById('DialogMonthlyExceptionShifts_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogMonthlyExceptionShifts_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogMonthlyExceptionShifts_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogMonthlyExceptionShifts_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogMonthlyExceptionShifts_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogMonthlyExceptionShifts').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogMonthlyExceptionShifts').align = 'right';

    ChangeStyle_DialogMonthlyExceptionShifts();
}

function DialogMonthlyExceptionShifts_onClose(sender, e) {
    document.getElementById('DialogMonthlyExceptionShifts_IFrame').style.display = 'none';
    document.getElementById('DialogMonthlyExceptionShifts_IFrame').style.visibility = 'hidden';
    DialogMonthlyExceptionShifts.set_contentUrl("about:blank");
}

function ChangeStyle_DialogMonthlyExceptionShifts() {
    document.getElementById('DialogMonthlyExceptionShifts_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById('DialogMonthlyExceptionShifts_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogMonthlyExceptionShiftsheader').style.width = document.getElementById('tbl_DialogMonthlyExceptionShiftsfooter').style.width = (screen.width - 7).toString() + 'px';
}

function CharToKeyCode_MonthlyExceptionShifts(str) {
    var OutStr = '';
    for (var i = 0; i < str.length; i++) {
        var KeyCode = str.charCodeAt(i);
        var CharKeyCode = '//' + KeyCode;
        OutStr += CharKeyCode;
    }
    return OutStr;
}








