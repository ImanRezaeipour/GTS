$(document).ready
        (
            function () {
                document.body.dir = document.FirstDayYear_ToDate_ReportParameterForm.dir;
                SetWrapper_Alert_Box(document.FirstDayYear_ToDate_ReportParameterForm.id);
                //ViewCurrentLangCalendars_RuleParameters();
                ResetCalendar_FirstDayYear_ToDate_ReportParameter();
                SetReportParameterObj_FirstDayYear_ToDate_ReportParameter();
            }
        );