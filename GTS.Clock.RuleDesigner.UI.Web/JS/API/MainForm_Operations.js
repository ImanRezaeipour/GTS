

function initQuickLaunch_MainForm() {
    document.getElementById('dmItemPersonnelMasterMonthlyOperationReport_DockMenu').title = document.getElementById('hfdmItemPersonnelMasterMonthlyOperationReport_DockMenu').value;
    document.getElementById('dmItemManagerMasterMonthlyOperationReport_DockMenu').title = document.getElementById('hfdmItemManagerMasterMonthlyOperationReport_DockMenu').value;
    document.getElementById('dmItemPersonnelIntroduction_DockMenu').title = document.getElementById('hfdmItemPersonnelIntroduction_DockMenu').value;
    document.getElementById('dmItemReportsIntroduction_DockMenu').title = document.getElementById('hfdmItemReportsIntroduction_DockMenu').value;
    document.getElementById('dmItemCartable_DockMenu').title = document.getElementById('hfdmItemCartable_DockMenu').value;
    document.getElementById('dmItemSurveyedRequests_DockMenu').title = document.getElementById('hfdmItemSurveyedRequests_DockMenu').value;
    document.getElementById('dmItemRegisteredRequests_DockMenu').title = document.getElementById('hfdmItemRegisteredRequests_DockMenu').value;
    document.getElementById('dmItemTrafficOperationByOperator_DockMenu').title = document.getElementById('hfdmItemTrafficOperationByOperator_DockMenu').value;
    document.getElementById('dmItemWorkFlowsView_DockMenu').title = document.getElementById('hfdmItemWorkFlowsView_DockMenu').value;
    document.getElementById('dmItemWelcome_DockMenu').title = document.getElementById('hfdmItemWelcome_DockMenu').value;
}


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
            return isExist
        }
    }
}