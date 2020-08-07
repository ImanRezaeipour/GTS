

$(document).ready
        (
            function () {
                document.body.dir = document.AccessLevelsForm.dir;
                SetWrapper_Alert_Box(document.AccessLevelsForm.id);
                GetBoxesHeaders_AccessLevelsAsign();
                SetCurrentRoleName_AccessLevelsAsign();
                Fill_trvAccessLevelsAsign_AccessLevelsAsign();
            }
        );
