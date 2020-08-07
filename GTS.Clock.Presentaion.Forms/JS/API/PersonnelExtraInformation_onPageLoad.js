
$(document).ready
        (
            function () {
                document.body.dir = document.PersonnelExtraInformationForm.dir;
                SetWrapper_Alert_Box(document.PersonnelExtraInformationForm.id);
                GetBoxesHeaders_PersonnelExtraInformation();
                SetActionMode_PersonnelExtraInformation();
                CheckPersonnelReserveFieldLoadAccess_PersonnelExtraInformation();
            }
        );
