
$(document).ready
        (
            function () {
                document.body.dir = document.LeaveReserveForm.dir;
                SetWrapper_Alert_Box(document.LeaveReserveForm.id);
                GetBoxesHeaders_LeaveReserve();
                Init_DayBoxes_LeaveReserve();
                Init_TimeSelectors_LeaveReserve();
                //ViewCurrentLangCalendars_LeaveReserve();
                ChangeCalendarsEnabled_LeaveReserve('disable');
                ChangeTimePickerEnabled_LeaveReserve('TimeSelector_Hour_LeaveReserve', 'disable');
                SetActionMode_LeaveReserve('View');
                GetRelativePersonnel_LeaveReserve();
                Fill_GridLeaveReserve_LeaveReserve();
            }
        );
