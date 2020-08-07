

$(document).ready
        (
            function () {
                document.body.dir = document.UnderManagementPersonnelForm.dir;
                SetWrapper_Alert_Box(document.UnderManagementPersonnelForm.id);
                GetBoxesHeaders_UnderManagementPersonnel();
                SetActionMode_UnderManagementPersonnel();
                SetPosition_DropDownDives_UnderManagementPersonnel();
                Fill_trvOrganizationPersonnel_UnderManagementPersonnel(true);
                Fill_GridUnderManagementPersonnel_UnderManagementPersonnel();
            }
        );
