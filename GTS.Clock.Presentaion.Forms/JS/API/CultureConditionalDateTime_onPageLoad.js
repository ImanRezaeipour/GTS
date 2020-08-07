
$(document).ready(function () {
    document.body.dir = document.CultureConditionalDateTimeForm.dir;
    SetWrapper_Alert_Box(document.CultureConditionalDateTimeForm.id);
    SetCurrentDate_CultureConditionalDateTime();
    initTimePicker_CultureConditionalDateTime();
    SetReportParameterObj_CultureConditionalDateTime();
})