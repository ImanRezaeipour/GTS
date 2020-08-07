

$(document).ready
        (
            function() {
               parent.DialogLoading.Close();
               document.body.dir = document.MachinesForm.dir;
               SetWrapper_Alert_Box(document.MachinesForm.id);
               GetBoxesHeaders_Machines();
               SetActionMode_Machines('View');
               Fill_GridMachines_Machines();
            }
        );
