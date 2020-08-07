
$(document).ready
        (
            function () {
                SetWrapper_Alert_Box(document.OvertimeJustificationRequestForm.id);
                SetDirection_Alert_Box(document.OvertimeJustificationRequestForm.dir);
                Set_SelectedDateTime_OvertimeJustificationRequest();
                GetBoxesHeaders_OvertimeJustificationRequest();
                SetActionMode_OvertimeJustificationRequest('View');
                //ViewCurrentLangCalendars_DialogOvertimeJustificationRequest();
                SetPosition_DropDownDives_DialogOvertimeJustificationRequest();
                InitTimePickers_OvertimeJustificationRequest();
                ResetCalendars_OvertimeJustificationRequest();
                Fill_GridRegisteredRequests_OvertimeJustificationRequest();
            }
        );
