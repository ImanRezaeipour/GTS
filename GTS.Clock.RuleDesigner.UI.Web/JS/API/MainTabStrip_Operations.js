

var ObjTabTabStripTabDirectClose = null;

function CloseCurrentTabOnTabStripMenus() {
    if (TabStripMenus.getSelectedTab().get_id() != "tbWelcome_TabStripMenus") {
        TabStripMenus.beginUpdate();
        var DeletedTabIndex = 0;
        var DeletedTabID = -1;
        var pgvID = TabStripMenus.getSelectedTab().get_pageViewId();
        for (var j = 0; j < TabStripMenus.get_tabs().get_length() ; j++) {
            if (TabStripMenus.get_tabs().getTab(j).get_id() == TabStripMenus.getSelectedTab().get_id()) {
                DeletedTabIndex = j;
                break;
            }
        }
        var tbArray = TabStripMenus.get_tabs();
        var newSelectedtb = null;
        if (ObjTabTabStripTabDirectClose == null || !ObjTabTabStripTabDirectClose.IsTabStripTabDirectClose || tbArray.get_length() == 2) {
            for (var i = 0; i < tbArray.get_length() ; i++) {
                if (tbArray.getTab(i).get_index() + 1 == DeletedTabIndex) {
                    newSelectedtb = tbArray.getTab(i);
                    tbArray.getTab(i).select();
                    //SelectNavBarItem_onTabSelect(tbArray.getTab(i).get_id());
                    break;
                }
            }
        }
        else {
            if (ObjTabTabStripTabDirectClose != null && ObjTabTabStripTabDirectClose.ActiveTabBeforeTabDirectClose != undefined) {
                TabStripMenus.setSelectedTab(ObjTabTabStripTabDirectClose.ActiveTabBeforeTabDirectClose);
                //SelectNavBarItem_onTabSelect(ObjTabTabStripTabDirectClose.ActiveTabBeforeTabDirectClose.get_id());
                ObjTabTabStripTabDirectClose = null;
            }
        }

        document.getElementById(pgvID + "_iFrame").src = "about:blank";
        TabStripMenus.get_tabs().remove(DeletedTabIndex);
        if (newSelectedtb != null && newSelectedtb.get_id() == "tbWelcome_TabStripMenus")
            MultiPageMenus.findPageById("pgvWelcome").Show();
        TabStripMenus.endUpdate();
    }
}


function TabStripMenus_onTabSelect(sender, e) {
    if (e.get_tab().get_id() == "tbWelcome_TabStripMenus")
        MultiPageMenus.findPageById("pgvWelcome").Show();
    //    else
    //        SelectNavBarItem_onTabSelect(e.get_tab().get_id());        
}

function imgClose_TabStripTab_TabStripMenus_onclick(tabID) {
    ObjTabTabStripTabDirectClose = new Object();
    ObjTabTabStripTabDirectClose.ActiveTabBeforeTabDirectClose = TabStripMenus.getSelectedTab();
    ObjTabTabStripTabDirectClose.IsTabStripTabDirectClose = TabStripMenus.getSelectedTab().get_id() != tabID ? true : false;
    TabStripMenus.setSelectedTab(TabStripMenus.findTabById(tabID));
    CloseCurrentTabOnTabStripMenus();
}



