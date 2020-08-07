
$(document).ready
        (
            function () {
                document.body.dir = document.CultureDateTimeForm.dir;
                SetWrapper_Alert_Box(document.CultureDateTimeForm.id);
                initTimePicker_CultureDateTime();
                SetReportParameterObj_CultureDateTime();
            }
        );
