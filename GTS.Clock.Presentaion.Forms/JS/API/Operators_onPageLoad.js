
$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.OperatorsForm.dir;
                SetWrapper_Alert_Box(document.OperatorsForm.id);
                GetBoxesHeaders_Operators();
                SetActionMode_Operators('View');
                Fill_GridOperators_Operators();
            }
        );
