

$(document).ready
        (
            function () {
                document.body.dir = document.KartableForm.dir;
                SetWrapper_Alert_Box(document.KartableForm.id);
                SetDirection_Alert_Box(parent.document.MainForm.dir);
                //ViewCurrentLangCalendars_Calendar();
                ChangeDateControlContainersWidth_Kartable();
                SetCurrentDate_Kartable();
                GetBoxesHeaders_Kartable();
                ChangeDirection_cmbControls_Kartable();
                ChangeDirection_Mastertbl_KartableForm();
                SetHorizontalScrollingDirection_GridKartable_Kartable_Opera();
                GetDefaultLoadState_Kartable();
                SetPageIndex_GridKartable_Kartable(0);
            }
        );
