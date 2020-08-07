

$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.MasterManagersForm.dir;
                SetWrapper_Alert_Box(document.MasterManagersForm.id);
                GetBoxesHeaders_MasterManagers();
                SetPosition_DropDownDives_MasterManagers();
                SetActionMode_MasterManagers('View');
            }
        );
