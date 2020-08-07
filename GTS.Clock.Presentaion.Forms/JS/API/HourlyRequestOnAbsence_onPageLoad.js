

$(document).ready
        (
            function () {
                SetWrapper_Alert_Box(document.HourlyRequestOnAbsenceForm.id);
                SetDirection_Alert_Box(document.HourlyRequestOnAbsenceForm.dir);
                Set_SelectedDateTime_HourlyRequestOnAbsence();
                GetBoxesHeaders_HourlyRequestOnAbsence();
                initTimePickers_HourlyRequestOnAbsence();
                SetPosition_DropDownDives_DialogHourlyRequestOnAbsence();
                SetActionMode_HourlyRequestOnAbsence('View');
                Fill_GridAbsencePairs_RequestOnAbsence();
                Fill_GridRegisteredRequests_HourlyRequestOnAbsence();
            }
        );
