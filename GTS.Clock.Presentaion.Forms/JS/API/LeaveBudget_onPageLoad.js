

$(document).ready
        (
            function () {
                document.body.dir = document.LeaveBudgetForm.dir;
                SetWrapper_Alert_Box(document.LeaveBudgetForm.id);
                GetBoxesHeaders_LeaveBudget();
                GetAxises_LeaveBudget();
                Init_DayBoxes_LeaveBudget();
                Init_TimeSelectors_LeaveBudget();
                SetCurrentLeaveBudget_LeaveBudget();
            }
        );
