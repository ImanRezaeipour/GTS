

function DialogMonthlyOperationGanttChartSchema_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogMonthlyOperationGanttChartSchema = DialogMonthlyOperationGanttChartSchema.get_value();
    var LoadState = ObjDialogMonthlyOperationGanttChartSchema.LoadState;
    var PersonnelID = ObjDialogMonthlyOperationGanttChartSchema.PersonnelID;
    DialogMonthlyOperationGanttChartSchema.set_contentUrl("MonthlyOperationGanttChartSchema.aspx?reload=" + (new Date()).getTime() + "&LoadState=" + CharToKeyCode_MonthlyOperationGanttChartSchema(LoadState) + "&PID=" + CharToKeyCode_MonthlyOperationGanttChartSchema(PersonnelID) + "");
    document.getElementById('DialogMonthlyOperationGanttChartSchema_IFrame').style.display = '';
    document.getElementById('DialogMonthlyOperationGanttChartSchema_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogMonthlyOperationGanttChartSchema_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogMonthlyOperationGanttChartSchema_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogMonthlyOperationGanttChartSchema_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogMonthlyOperationGanttChartSchema_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogMonthlyOperationGanttChartSchema').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogMonthlyOperationGanttChartSchema').align = 'right';

    ChangeStyle_DialogMonthlyOperationGanttChartSchema();
}

function DialogMonthlyOperationGanttChartSchema_onClose(sender, e) {
    document.getElementById('DialogMonthlyOperationGanttChartSchema_IFrame').style.display = 'none';
    document.getElementById('DialogMonthlyOperationGanttChartSchema_IFrame').style.visibility = 'hidden';
    DialogMonthlyOperationGanttChartSchema.set_contentUrl("about:blank");
}

function ChangeStyle_DialogMonthlyOperationGanttChartSchema() {
    document.getElementById('DialogMonthlyOperationGanttChartSchema_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById('DialogMonthlyOperationGanttChartSchema_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogMonthlyOperationGanttChartSchemaheader').style.width = document.getElementById('tbl_DialogMonthlyOperationGanttChartSchemafooter').style.width = (screen.width - 7).toString() + 'px';
}


function CharToKeyCode_MonthlyOperationGanttChartSchema(str) {
    var OutStr = '';
    for (var i = 0; i < str.length; i++) {
        var KeyCode = str.charCodeAt(i);
        var CharKeyCode = '//' + KeyCode;
        OutStr += CharKeyCode;
    }
    return OutStr;
}







