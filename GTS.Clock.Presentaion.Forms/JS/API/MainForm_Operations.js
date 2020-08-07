var CurrentLangID = null;
var SysLangID = null;
var HelpRefererWindow = null;

function DockMenuItem_onClick(dmItemID) {
    var dmItemBaseText = dmItemID.substring(6, dmItemID.length - 9);
    var nvbItem = 'nvbItem' + dmItemBaseText + '_NavBarMain';
    if (dmItemID != 'dmItemWelcome_DockMenu') {
        if (CheckExistingNavBarItem_MainPage(nvbItem))
            NavBarMain_onItemSelect_Operations(NavBarMain.findItemById(nvbItem));
    }
    else {
        TabStripMenus.get_tabs().getTabById("tb" + dmItemBaseText + "_TabStripMenus").select();
        MultiPageMenus.findPageById('pgv' + dmItemBaseText).Show();
    }
}

function CheckExistingNavBarItem_MainPage(nvbItem) {
    var isExist = false;
    for (var i = 0; i < NavBarMain.get_items().get_length(); i++) {
        var parentNavbarItem = NavBarMain.get_items().get_itemArray()[i];
        if (parentNavbarItem.get_items().getItemById(nvbItem) != null) {
            isExist = true;
            return isExist;
        }
    }
}

function SetNavBarHeight() {
    document.getElementById('NavBarMain_tr').style.height = parseInt(screen.height - 440) + "px";
    //document.getElementById('NavBarMain_tr').style.height = (parseInt(screen.height))*0.4818 + "px";
}

function SetCurrentCulture() {
    CurrentLangID = document.getElementById('hfCurrentUILangID').value;
    SysLangID = document.getElementById('hfCurrentSysLangID').value
}

function InitializeQuickLaunch_MainForm() {
    document.getElementById('qlItemHome').alt = document.getElementById('qlItemHome').title = document.getElementById('hfTitle_qlItemHome').value;
    if (CheckDockMenuItemIsAccessible_MainForm('qlItemWorkFlowsView'))
        document.getElementById('qlItemWorkFlowsView').alt = document.getElementById('qlItemWorkFlowsView').title = document.getElementById('hfTitle_qlItemWorkFlowsView').value;
    if (CheckDockMenuItemIsAccessible_MainForm('qlItemTrafficsControl'))
        document.getElementById('qlItemTrafficsControl').alt = document.getElementById('qlItemTrafficsControl').title = document.getElementById('hfTitle_qlItemTrafficsControl').value;
    if (CheckDockMenuItemIsAccessible_MainForm('qlItemRegisteredRequests'))
        document.getElementById('qlItemRegisteredRequests').alt = document.getElementById('qlItemRegisteredRequests').title = document.getElementById('hfTitle_qlItemRegisteredRequests').value;
    if (CheckDockMenuItemIsAccessible_MainForm('qlItemSurveyedRequests'))
        document.getElementById('qlItemSurveyedRequests').alt = document.getElementById('qlItemSurveyedRequests').title = document.getElementById('hfTitle_qlItemSurveyedRequests').value;
    if (CheckDockMenuItemIsAccessible_MainForm('qlItemKartable'))
        document.getElementById('qlItemKartable').alt = document.getElementById('qlItemKartable').title = document.getElementById('hfTitle_qlItemKartable').value;
    if (CheckDockMenuItemIsAccessible_MainForm('qlItemReportsIntroduction'))
        document.getElementById('qlItemReportsIntroduction').alt = document.getElementById('qlItemReportsIntroduction').title = document.getElementById('hfTitle_qlItemReportsIntroduction').value;
    if (CheckDockMenuItemIsAccessible_MainForm('qlItemPersonnelIntroduction'))
        document.getElementById('qlItemPersonnelIntroduction').alt = document.getElementById('qlItemPersonnelIntroduction').title = document.getElementById('hfTitle_qlItemPersonnelIntroduction').value;
    if (CheckDockMenuItemIsAccessible_MainForm('qlItemManagerMasterMonthlyOperationReport'))
        document.getElementById('qlItemManagerMasterMonthlyOperationReport').alt = document.getElementById('qlItemManagerMasterMonthlyOperationReport').title = document.getElementById('hfTitle_qlItemManagerMasterMonthlyOperationReport').value;
    if (CheckDockMenuItemIsAccessible_MainForm('qlItemPersonnelMasterMonthlyOperationReport'))
        document.getElementById('qlItemPersonnelMasterMonthlyOperationReport').alt = document.getElementById('qlItemPersonnelMasterMonthlyOperationReport').title = document.getElementById('hfTitle_qlItemPersonnelMasterMonthlyOperationReport').value;
}

function CheckDockMenuItemIsAccessible_MainForm(dmItemID) {
    var IsAccessible = true;
    var dmItemBaseText = (dmItemID + '_DockMenu').substring(6, (dmItemID + '_DockMenu').length - 9);
    var nvbItem = 'nvbItem' + dmItemBaseText + '_NavBarMain';
    var AccessNotAllowedNavBarItemsList = document.getElementById('hfAccessNoAllowdNavBarItemsList').value;
    var AccessNotAllowedNavBarItemsList = eval('(' + AccessNotAllowedNavBarItemsList + ')');
    for (var i = 0; i < AccessNotAllowedNavBarItemsList.length; i++) {
        if (AccessNotAllowedNavBarItemsList[i] == nvbItem) {
            IsAccessible = false;
            break;
        }
    }
    if (!IsAccessible)
        document.getElementById(dmItemID).parentNode.parentNode.removeChild(document.getElementById(dmItemID).parentNode);
    return IsAccessible;    
}

function imgbtnPersian_onClick() {
    MainFrom_onBeforePostBack();
}

function ImgbtnEnglish_onClick() {
    MainFrom_onBeforePostBack();
}

function imgbtnLogOut_onClick() {
    MainFrom_onBeforePostBack();
}

function MainFrom_onBeforePostBack() {
    TabStripMenus.dispose();
}





