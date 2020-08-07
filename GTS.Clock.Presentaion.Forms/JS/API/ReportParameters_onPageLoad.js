
$(document).ready
(
    function () {
        document.body.dir = document.ReportParametersForm.dir;
        SetWrapper_Alert_Box(document.ReportParametersForm.id);
        GetBoxesHeaders_ReportParameters();
        ResetCalendars_ReportParameters();
        //ViewCurrentLangCalendars_ReportParameters();
        Fill_GridReportParameters_ReportParameters();
    }
)