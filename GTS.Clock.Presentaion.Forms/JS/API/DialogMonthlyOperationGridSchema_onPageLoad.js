
function DialogMonthlyOperationGridSchema_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogMonthlyOperationGridSchema = DialogMonthlyOperationGridSchema.get_value();
    var LoadState = ObjDialogMonthlyOperationGridSchema.LoadState;
    var PersonnelID = ObjDialogMonthlyOperationGridSchema.PersonnelID;
    DialogMonthlyOperationGridSchema.set_contentUrl("MonthlyOperationGridSchema.aspx?reload=" + (new Date()).getTime() + "&LoadState=" + CharToKeyCode_MonthlyOperationGridSchema(LoadState) + "&PID=" + CharToKeyCode_MonthlyOperationGridSchema(PersonnelID) + "");
    document.getElementById('DialogMonthlyOperationGridSchema_IFrame').style.display = '';
    document.getElementById('DialogMonthlyOperationGridSchema_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogMonthlyOperationGridSchema_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogMonthlyOperationGridSchema_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogMonthlyOperationGridSchema_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogMonthlyOperationGridSchema_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogMonthlyOperationGridSchema').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogMonthlyOperationGridSchema').align = 'right';

    ChangeStyle_DialogMonthlyOperationGridSchema();
}

function DialogMonthlyOperationGridSchema_onClose(sender, e) {
    document.getElementById('DialogMonthlyOperationGridSchema_IFrame').style.display = 'none';
    document.getElementById('DialogMonthlyOperationGridSchema_IFrame').style.visibility = 'hidden';
    DialogMonthlyOperationGridSchema.set_contentUrl("about:blank");
}

function ChangeStyle_DialogMonthlyOperationGridSchema() {
    document.getElementById('DialogMonthlyOperationGridSchema_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById('DialogMonthlyOperationGridSchema_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogMonthlyOperationGridSchemaheader').style.width = document.getElementById('tbl_DialogMonthlyOperationGridSchemafooter').style.width = (screen.width - 7).toString() + 'px';
}

function CharToKeyCode_MonthlyOperationGridSchema(str) {
    var OutStr = '';
    for (var i = 0; i < str.length; i++) {
        var KeyCode = str.charCodeAt(i);
        var CharKeyCode = '//' + KeyCode;
        OutStr += CharKeyCode;
    }
    return OutStr;
}








