
$(document).ready
(
    function () {
        parent.DialogLoading.Close();
        document.body.dir = document.PersonnelMasterMonthlyOperationForm.dir;
        SetWrapper_Alert_Box(document.PersonnelMasterMonthlyOperationForm.id);
    }
);