

$(document).ready
        (
            function() {
                parent.DialogLoading.Close();
                document.body.dir = document.EmployTypesForm.dir;
                SetWrapper_Alert_Box(document.EmployTypesForm.id);
                GetBoxesHeaders_EmployTypes();
                SetActionMode_EmployTypes('View');
                Fill_GridEmployTypes_EmployTypes();
            }
        );
