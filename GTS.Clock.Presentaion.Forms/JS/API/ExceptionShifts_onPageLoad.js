

$(document).ready
        (
            function () {
                document.body.dir = document.ExceptionShiftsForm.dir;
                SetWrapper_Alert_Box(document.ExceptionShiftsForm.id);
                CurrentLangID = parent.CurrentLangID;
                TabStripExceptionShifts_onTabsEnabledChange();
                //ViewCurrentLangCalendars_TabStripExceptionShifts();
                SetPosition_DropDownDives_DialogExceptionShifts();
                SetPosition_cmbPersonnelControls_ExceptionShifts();
                GetBoxesHeaders_ExceptionShifts();
                SetActionMode_ExceptionShifts();
                ResetCalendars_ExceptionShifts();
                NavigateExceptionShift_ExceptionShifts();
            }
        );
