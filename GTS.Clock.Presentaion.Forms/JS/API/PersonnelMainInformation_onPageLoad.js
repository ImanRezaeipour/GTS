

$(document).ready
        (
            function () {
                document.body.dir = document.PersonnelMainInformationForm.dir;
                init_DialogPersonnelMainInformation();
                SetWrapper_Alert_Box(document.PersonnelMainInformationForm.id);
                ResetCalendars_DialogPersonnelMainInformation();
                GetBoxesHeaders_DialogPersonnelMainInformation();
                GetWorkingPersonnelID_PersonnelMainInformation();
            }
        );



