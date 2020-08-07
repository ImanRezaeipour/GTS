
$(document).ready
        (
            function () {
                document.body.dir = document.PeriodRepeatForm.dir;
                SetWrapper_Alert_Box(document.PeriodRepeatForm.id);
                GetBoxesHeaders_PeriodRepeat();
                ChangeControlDirection_PeriodRepeat('All');
            }
        );
