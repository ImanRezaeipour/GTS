$(document).ready
        (
            function () {
                document.body.dir = document.Station_Clock_Date_ReportParameterForm.dir;
                SetWrapper_Alert_Box(document.Station_Clock_Date_ReportParameterForm.id);
                //ViewCurrentLangCalendars_RuleParameters();
                SetReportParameterObj_Station_Clock_Date_ReportParameter();

            }
        );