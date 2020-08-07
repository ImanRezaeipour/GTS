

$(document).ready
        (
            function() {
               parent.DialogLoading.Close();
               document.body.dir = document.WorkHeatForm.dir;
               SetWrapper_Alert_Box(document.WorkHeatForm.id);
               GetBoxesHeaders_WorkHeat();
               SetActionMode_WorkHeat('View');
               Fill_GridWorkHeat_WorkHeat();
            }
        );
