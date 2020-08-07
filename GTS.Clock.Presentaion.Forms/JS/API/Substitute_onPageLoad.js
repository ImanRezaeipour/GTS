

$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.SubstituteForm.dir;
                SetWrapper_Alert_Box(document.SubstituteForm.id);
                //ViewCurrentLangCalendars_tbSubstitute_TabStripMenus();
                ChangeCalendarsEnabled_Substitute('disable');
                SetPosition_DropDownDives_Substitute();
                GetBoxesHeaders_Substitute();
                ResetCalendars_Substitute();
                SetActionMode_Substitute('View');
            }
        );
