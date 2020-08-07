$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.IllnessesForm.dir;
                SetWrapper_Alert_Box(document.IllnessesForm.id);
                GetBoxesHeaders_Illness();
                SetActionMode_Illness('View');
                Fill_GridIllness_Illness();
            }
        );
