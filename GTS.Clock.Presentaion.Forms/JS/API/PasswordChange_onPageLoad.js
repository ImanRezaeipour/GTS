

$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.PasswordChangeForm.dir;
                SetWrapper_Alert_Box(document.PasswordChangeForm.id);
                GetCurrentUser_PasswordChange();
            }
        );
