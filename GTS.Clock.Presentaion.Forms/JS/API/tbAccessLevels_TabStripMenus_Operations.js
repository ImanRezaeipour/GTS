
function tbAccessLevels_TabStripMenus_onSelect() {
    ChangeColumnHeader_cmbUser_AccessLevels();
    Fill_GridUserGroups_AccessLevels();
}

function ChangeColumnHeader_cmbUser_AccessLevels() {
    if (CurrentLangID == "fa-IR") {
        document.getElementById('clmnPersonnelID_cmbUser_AccessLevels').innerHTML = 'کد پرسنلی';
        document.getElementById('clmnName_cmbUser_AccessLevels').innerHTML = 'نام';
        document.getElementById('clmnFamily_cmbUser_AccessLevels').innerHTML = 'نام خانوادگی';
    }
    if (CurrentLangID == "en-US") {
        document.getElementById('clmnPersonnelID_cmbUser_AccessLevels').innerHTML = 'PersonnelID';
        document.getElementById('clmnName_cmbUser_AccessLevels').innerHTML = 'Name';
        document.getElementById('clmnFamily_cmbUser_AccessLevels').innerHTML = 'Family';
    }
}

function Fill_GridUserGroups_AccessLevels() {
    CallBack_GridUserGroups_AccessLevels.callback();
}


var SelectedItem_GridUserGroups_AccessLevels = null;
function GridUserGroups_AccessLevels_onItemSelect(sender, e) {
    SelectedItem_GridUserGroups_AccessLevels = new Object();
    SelectedItem_GridUserGroups_AccessLevels.ID = e.get_item().getMember('ID').get_text();
    SelectedItem_GridUserGroups_AccessLevels.Name = e.get_item().getMember('Name').get_text();
    SelectedItem_GridUserGroups_AccessLevels.Schema = e.get_item().getMember('Schema').get_text();    
    //SelectedItem_GridUserGroups_AccessLevels = e.get_item().getMember('ID').get_text();
}






 