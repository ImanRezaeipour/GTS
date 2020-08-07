
var CurrentLangID = 'fa-IR';
var SysLangID = 'fa-IR';

function SetCurrentCulture() {
    document.body.dir = document.MainForm.dir;
    document.title = document.MainForm.title;
    document.MainForm.title = "";
}

function PageLoad() {
    SetNavBarHeight();
    SetCurrentCulture();
    MainPage_GetCurrentLanguages();
    initQuickLaunch_MainForm();
}

function MainPage_GetCurrentLanguages_onCallBack(Response) {
    if (Response != null) {
        var languages = eval('(' + Response + ')');
        CurrentLangID = languages.CurrentLanguage;
        SysLangID = languages.CurrentSysLanguage;
    }
}

function SetNavBarHeight() {
    document.getElementById('NavBarMain_tr').style.height = parseInt(screen.height - 440) + "px";
    //document.getElementById('NavBarMain_tr').style.height = (parseInt(screen.height))*0.4818 + "px";
}

















