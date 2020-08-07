$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.CalculationsForm.dir;
                SetWrapper_Alert_Box(document.CalculationsForm.id);
                GetBoxesHeaders_CalculationsForm();
                ResetCalendars_Calculations();
                ChangeControlDirection_Calculations('All');
                SetPosition_cmbPersonnel_Calculations();
                SetProgressbarPercentage_Calculations();
            }
        );

