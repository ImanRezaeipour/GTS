

$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.MasterExceptionShiftsForm.dir;
                SetWrapper_Alert_Box(document.MasterExceptionShiftsForm.id);
                SetActionMode_MasterExceptionShifts('View');
                GetBoxesHeaders_MasterExceptionShifts();
                SetPosition_DropDownDives_MasterExceptionShifts();
                //ViewCurrentLangCalendars_tbMasterExceptionShifts_TabStripMenus();
                ResetCalendars_MasterExceptionShifts();
                SetPosition_cmbPersonnel_MasterExceptionShifts();
            }
        );
