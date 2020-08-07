
function DialogReportParameters_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogReportParameters.set_contentUrl("ReportParameters.aspx?reload=" + (new Date()).getTime() + "");
    document.getElementById('DialogReportParameters_IFrame').style.display = '';
    document.getElementById('DialogReportParameters_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogReportParameters_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogReportParameters_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogReportParameters_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogReportParameters_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogReportParameters').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogReportParameters').align = 'right';
}

function DialogReportParameters_onClose(sender, e) {
    document.getElementById('DialogReportParameters_IFrame').style.display = 'none';
    document.getElementById('DialogReportParameters_IFrame').style.visibility = 'hidden';
    DialogReportParameters.set_contentUrl("about:blank");
}

function ChangeStyle_DialogReportParameters() {
    document.getElementById('DialogReportParameters_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById('DialogReportParameters_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogReportParametersheader').style.width = document.getElementById('tbl_DialogReportParametersfooter').style.width = (screen.width - 7).toString() + 'px';
}

