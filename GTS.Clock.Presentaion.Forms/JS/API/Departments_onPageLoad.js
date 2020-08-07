

$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.DepartmentForm.dir;
                SetWrapper_Alert_Box(document.DepartmentForm.id);
                GetBoxesHeaders_Departments();
                SetActionMode_Departments('View');
                Fill_trvDepartmentsIntroduction_DepartmentIntroduction();
            }
        );


