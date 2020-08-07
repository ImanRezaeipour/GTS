

$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.PhysiciansForm.dir;
                SetWrapper_Alert_Box(document.PhysiciansForm.id);
                GetBoxesHeaders_Physicians();
                SetActionMode_Physicians('View');
                Fill_GridPhysicians_Physicians();
            }
        );
