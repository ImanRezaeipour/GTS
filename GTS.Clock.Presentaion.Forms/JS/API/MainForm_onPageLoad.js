
$(document).ready(
    function () {
        document.body.dir = document.MainForm.dir;
        document.title = document.MainForm.title;
        document.MainForm.title = "";
        SetCurrentCulture();
        SetNavBarHeight();
        InitializeQuickLaunch_MainForm();
        $('imgHeaderLogo').imgscale();
    }
)

















