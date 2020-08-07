
$(document).ready
        (
            function () {
                document.body.dir = document.MonthlyExceptionShiftsForm.dir;
                SetWrapper_Alert_Box(document.MonthlyExceptionShiftsForm.id);
                GettBoxesHeaders_MonthlyExceptionShifts();
                SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(0);
            }
        );
