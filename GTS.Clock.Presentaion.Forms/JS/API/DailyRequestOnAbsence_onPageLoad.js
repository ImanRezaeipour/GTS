

$(document).ready
        (
            function () {
                SetWrapper_Alert_Box(document.DailyRequestOnAbsenceForm.id);
                SetDirection_Alert_Box(document.DailyRequestOnAbsenceForm.dir);
                Set_SelectedDateTime_DailyRequestOnAbsence();
                ResetCalendars_DailyRequestOnAbsence();
                GetBoxesHeaders_DailyRequestOnAbsence();
                //ViewCurrentLangCalendars_DialogDailyRequestOnAbsence();
                SetPosition_DropDownDives_DialogDailyRequestOnAbsence();
                SetActionMode_DailyRequestOnAbsence('View');
                Fill_GridRegisteredRequests_DailyRequestOnAbsence();
            }
        );

