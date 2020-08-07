
$(document).ready
        (
            function () {
                document.body.dir = document.UserSettingsForm.dir;
                SetWrapper_Alert_Box(document.UserSettingsForm.id);
                GetBoxesHeaders_UserSettings();
                GetSettings_UserSettings('Load');
                ChangeDirection_Mastertbl_UserSettings();
            }
        );
