

$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.RolesForm.dir;
                SetWrapper_Alert_Box(document.RolesForm.id);
                GetBoxesHeaders_Roles();
                SetActionMode_Roles('View');
                Fill_trvRoles_Roles();
            }
        );
