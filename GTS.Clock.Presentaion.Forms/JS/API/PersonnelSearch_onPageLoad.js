

$(document).ready
        (
            function () {
                document.body.dir = document.PersonnelSearchForm.dir;
                SetWrapper_Alert_Box(document.PersonnelSearchForm.id);
                //ViewCurrentLangCalendars_PersonnelSearch();
                //ChangeComboTreeDirection_PersonnelSearch();
                GetBoxesHeaders_Substitute();
                ResetCalendars_PersonnelSearch();
            }
        );
