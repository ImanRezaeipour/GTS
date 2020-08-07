

$(document).ready
        (
            function () {
                document.body.dir = document.CartableForm.dir;
                SetWrapper_Alert_Box(document.CartableForm.id);
                SetDirection_Alert_Box(parent.document.MainForm.dir);
                //ViewCurrentLangCalendars_Calendar();
                ChangeDateControlContainersWidth_Cartable();
                SetCurrentDate_Cartable();
                GetBoxesHeaders_Cartable();
                ChangeDirection_cmbControls_Cartable();
                ChangeDirection_Mastertbl_CartableForm();
                SetHorizontalScrollingDirection_GridCartable_Cartable_Opera();
                GetDefaultLoadState_Cartable();
                SetPageIndex_GridCartable_Cartable(0);
            }
        );
