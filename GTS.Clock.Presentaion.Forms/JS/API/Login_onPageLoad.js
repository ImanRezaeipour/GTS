

$(document).ready
        (
            function () {
                document.body.dir = document.LoginForm.dir;
                document.title = document.LoginForm.title;
                initControls_Login();
                CheckReferer();
            }
        );
