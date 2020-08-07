
function initializeParts_MainView() {
    document.getElementById('MainViewPart1_MainView').src = 'PrivateNews.aspx?' + (new Date()).getDate();
    document.getElementById('MainViewPart2_MainView').src = 'PersonnelInformationSummary.aspx?' + (new Date()).getDate();
    document.getElementById('MainViewPart3_MainView').src = 'PublicNews.aspx?' + (new Date()).getDate();
    document.getElementById('MainViewPart4_MainView').src = 'LocalDateTime.aspx?' + (new Date()).getDate();
}

function Refresh_MainViewPart1_MainView() {
    document.getElementById('MainViewPart1_MainView').src = 'PrivateNews.aspx?' + (new Date()).getDate();
}

function Refresh_MainViewPart2_MainView() {
    document.getElementById('MainViewPart2_MainView').src = 'PersonnelInformationSummary.aspx?' + (new Date()).getDate();
}

function Refresh_MainViewPart3_MainView() {
    document.getElementById('MainViewPart3_MainView').src = 'PublicNews.aspx?' + (new Date()).getDate();
}

function Refresh_MainViewPart4_MainView() {
    document.getElementById('MainViewPart4_MainView').src = 'LocalDateTime.aspx?' + (new Date()).getDate();
}

function Maximize_MainViewPart1_MainView() {
    ShowDialogMainViewMaximizedPart("PrivateNews.aspx");
}

function Maximize_MainViewPart2_MainView() {
    ShowDialogMainViewMaximizedPart("PersonnelInformationSummary.aspx");
}

function Maximize_MainViewPart3_MainView() {
    ShowDialogMainViewMaximizedPart("PublicNews.aspx");
}

function Maximize_MainViewPart4_MainView() {
    ShowDialogMainViewMaximizedPart("LocalDateTime.aspx");
}

function ShowDialogMainViewMaximizedPart(Caller) {
    document.getElementById('MainViewMaximizedPartIFrame_MainView').src = Caller;
    DialogMainViewMaximizedPart.Show();
}

