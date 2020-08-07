
var CurrentPageState_Substitute = 'View';
var box_ManagersSelect_Substitute_IsShown = false;
var box_SubstitutesSelect_Substitute_IsShown = false;
var SelectedManager_Substitute = null;
var SelectedPersonnel_Substitute = null;
var LoadState_cmbManagers_Substitute = 'Normal';
var LoadState_cmbPersonnel_Substitute = 'Normal';
var LoadState_GridSubstitutes_Substitute = 'Normal';
var CurrentPageIndex_cmbManagers_Substitute = 0;
var CurrentPageIndex_cmbPersonnel_Substitute = 0;
var AdvancedSearchTerm_cmbManagers_Substitute = '';
var AdvancedSearchTerm_cmbPersonnel_Substitute = '';
var CurrentPageCombosCallBcakStateObj = new Object();
var ConfirmState_Substitute = null;
var ObjSubstitute_Substitute = null;

function GetBoxesHeaders_Substitute() {
    document.getElementById('header_Substitutes_Substitute').innerHTML = document.getElementById('hfheader_Substitutes_Substitute').value;
    document.getElementById('clmnName_cmbManagers_Substitute').innerHTML = document.getElementById('hfclmnName_cmbManagers_Substitute').value;
    document.getElementById('clmnBarCode_cmbManagers_Substitute').innerHTML = document.getElementById('hfclmnBarCode_cmbManagers_Substitute').value;
    document.getElementById('clmnCardNum_cmbManagers_Substitute').innerHTML = document.getElementById('hfclmnCardNum_cmbManagers_Substitute').value;
    document.getElementById('clmnName_cmbPersonnel_Substitute').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_Substitute').value;
    document.getElementById('clmnBarCode_cmbPersonnel_Substitute').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_Substitute').value;
    document.getElementById('clmnCardNum_cmbPersonnel_Substitute').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_Substitute').value;
}

function ResetCalendars_Substitute() {
    var currentDate_Substitute = document.getElementById('hfCurrentDate_Substitute').value;
    switch (parent.SysLangID) {
        case 'en-US':
            currentDate_Substitute = new Date(currentDate_Substitute);
            gdpMasterFromDate_Substitute.setSelectedDate(currentDate_Substitute);
            gdpMasterToDate_Substitute.setSelectedDate(currentDate_Substitute);
            break;
        case 'fa-IR':
            document.getElementById('pdpMasterFromDate_Substitute').value = currentDate_Substitute;
            document.getElementById('pdpMasterToDate_Substitute').value = currentDate_Substitute;
            break;
    }
}


//////////////////////////////////// gdpMasterFromDate & gCalMasterFromDate ///////////////////////////////////////////
function btn_gdpMasterFromDate_Substitute_OnMouseUp(event) {
    if (gCalMasterFromDate_Substitute.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpMasterFromDate_Substitute_OnDateChange(sender, e) {
    var fromDate = gdpMasterFromDate_Substitute.getSelectedDate();
    gCalMasterFromDate_Substitute.setSelectedDate(fromDate);
}

function btn_gdpMasterFromDate_Substitute_OnClick(event) {
    if (gCalMasterFromDate_Substitute.get_popUpShowing()) {
        gCalMasterFromDate_Substitute.hide();
    }
    else {
        gCalMasterFromDate_Substitute.setSelectedDate(gdpMasterFromDate_Substitute.getSelectedDate());
        gCalMasterFromDate_Substitute.show();
    }
}

function btn_gdpMasterFromDate_Substitute_OnMouseUp(event) {
    if (gCalMasterFromDate_Substitute.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalMasterFromDate_Substitute_OnChange(sender, e) {
    var fromDate = gCalMasterFromDate_Substitute.getSelectedDate();
    gdpMasterFromDate_Substitute.setSelectedDate(fromDate);
}

////////////////////////////////// gdpMasterToDate & gCalMasterToDate /////////////////////////////////////////////////
function btn_gdpMasterToDate_Substitute_OnMouseUp(event) {
    if (gCalMasterToDate_Substitute.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpMasterToDate_Substitute_OnDateChange(sender, e) {
    var toDate = gdpMasterToDate_Substitute.getSelectedDate();
    gCalMasterToDate_Substitute.setSelectedDate(toDate);
}

function btn_gdpMasterToDate_Substitute_OnClick(event) {
    if (gCalMasterToDate_Substitute.get_popUpShowing()) {
        gCalMasterToDate_Substitute.hide();
    }
    else {
        gCalMasterToDate_Substitute.setSelectedDate(gdpMasterToDate_Substitute.getSelectedDate());
        gCalMasterToDate_Substitute.show();
    }
}

function btn_gdpMasterToDate_Substitute_OnMouseUp(event) {
    if (gCalMasterToDate_Substitute.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalMasterToDate_Substitute_OnChange(event) {
    var toDate = gCalMasterToDate_Substitute.getSelectedDate();
    gdpMasterToDate_Substitute.setSelectedDate(toDate);
}

function ChangeCalendarsEnabled_Substitute(state) {
    switch (parent.SysLangID) {
        case 'fa-IR':
            ChangeCalendarEnabled_Substitute('pdpMasterFromDate_Substitute', state);
            ChangeCalendarEnabled_Substitute('pdpMasterToDate_Substitute', state);
            break;
        case 'en-US':
            ChangeCalendarEnabled_Substitute('gdpMasterFromDate_Substitute', state);
            ChangeCalendarEnabled_Substitute('gdpMasterToDate_Substitute', state);
            break;
    }
}

function ChangeCalendarEnabled_Substitute(cal, state) {
    var disabled = null;
    switch (state) {
        case 'disable':
            disabled = 'disabled';
            switch (parent.SysLangID) {
                case 'fa-IR':
                    document.getElementById(cal).onclick = " ";
                    document.getElementById(cal + 'imgbt').onclick = " ";
                    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible') 
                        displayDatePicker(cal);
                    break;
                case 'en-US':
                    document.getElementById('btn_' + cal).onclick = " ";
                    break;
            }
            break;
        case 'enable':
            disabled = '';
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    document.getElementById(cal).onclick = function () {
                        displayDatePicker(cal);
                    };
                    document.getElementById(cal + 'imgbt').onclick = function () {
                        displayDatePicker(cal);
                    };
                    break;
                case 'en-US':
                    document.getElementById('btn_' + cal).onclick = function () {
                        CalendarsViewManage_Substitute(cal);
                    };
                    break;
            }
            break;
    }
}

function CalendarsViewManage_Substitute(gdpID) {
    var CalID_Substitute = 'gCal' + gdpID.substr(3, gdpID.length - 3);
    var Cal_Substitute = eval(CalID_Substitute);
    if (!Cal_Substitute.get_popUpShowing())
        Cal_Substitute.show();
    else
        Cal_Substitute.hide();
}


function SetPosition_DropDownDives_Substitute() {
    switch (parent.CurrentLangID) {
        case "fa-IR":
            document.getElementById('box_ManagersSelect_Substitute').style.right = '20px';
            document.getElementById('box_SubstitutesSelect_Substitute').style.right = '350px';
            break;
        case "en-US":
            document.getElementById('box_ManagersSelect_Substitute').style.left = '20px';
            document.getElementById('box_SubstitutesSelect_Substitute').style.left = '350px';
            break;
    }

}

function ViewCurrentLangCalendars_tbSubstitute_TabStripMenus() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpMasterFromDate_Substitute").parentNode.removeChild(document.getElementById("pdpMasterFromDate_Substitute"));
            document.getElementById("pdpMasterFromDate_Substituteimgbt").parentNode.removeChild(document.getElementById("pdpMasterFromDate_Substituteimgbt"));
            document.getElementById("pdpMasterToDate_Substitute").parentNode.removeChild(document.getElementById("pdpMasterToDate_Substitute"));
            document.getElementById("pdpMasterToDate_Substituteimgbt").parentNode.removeChild(document.getElementById("pdpMasterToDate_Substituteimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_MasterFromDateCalendars_Substitute").removeChild(document.getElementById("Container_gCalMasterFromDate_Substitute"));
            document.getElementById("Container_MasterToDateCalendars_Substitute").removeChild(document.getElementById("Container_gCalMasterToDate_Substitute"));
            break
    }
}

function ManagersSelectBox_Substitute_onDropDownDiveStateChange() {
    CollapseControls_Substitute(null);
    setSlideDownSpeed(200);

    if (box_SubstitutesSelect_Substitute_IsShown)
        SubstituteSelectBox_Substitute_onDropDownDiveStateChange();
    slidedown_showHide('box_ManagersSelect_Substitute');

    if (box_ManagersSelect_Substitute_IsShown) {
        box_ManagersSelect_Substitute_IsShown = false;
        TlbManagerSelect_Substitute.get_items().getItemById('tlbItemManagerSelect_TlbManagerSelect_Substitute').set_imageUrl('arrowDown.jpg');
        cmbManagers_Substitute.collapse();
    }
    else {
        box_ManagersSelect_Substitute_IsShown = true;
        TlbManagerSelect_Substitute.get_items().getItemById('tlbItemManagerSelect_TlbManagerSelect_Substitute').set_imageUrl('arrowUp.jpg');
    }
}

function SubstituteSelectBox_Substitute_onDropDownDiveStateChange() {
    CollapseControls_Substitute(null);
    setSlideDownSpeed(200);

    if (box_ManagersSelect_Substitute_IsShown)
        ManagersSelectBox_Substitute_onDropDownDiveStateChange();
    slidedown_showHide('box_SubstitutesSelect_Substitute');

    if (box_SubstitutesSelect_Substitute_IsShown) {
        box_SubstitutesSelect_Substitute_IsShown = false;
        TlbSubstituteSelect_Substitute.get_items().getItemById('tlbItemSubstituteSelect_TlbSubstituteSelect_Substitute').set_imageUrl('arrowDown.jpg');
        cmbPersonnel_Substitute.collapse();
    }
    else {
        box_SubstitutesSelect_Substitute_IsShown = true;
        TlbSubstituteSelect_Substitute.get_items().getItemById('tlbItemSubstituteSelect_TlbSubstituteSelect_Substitute').set_imageUrl('arrowUp.jpg');
    }
}

function ChangePageState_Substitute(state) {
    CurrentPageState_Substitute = state;
    SetActionMode_Substitute(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbSubstitute.get_items().getItemById('tlbItemNew_TlbSubstitute') != null) {
            TlbSubstitute.get_items().getItemById('tlbItemNew_TlbSubstitute').set_enabled(false);
            TlbSubstitute.get_items().getItemById('tlbItemNew_TlbSubstitute').set_imageUrl('add_silver.png');
        }
        if (TlbSubstitute.get_items().getItemById('tlbItemEdit_TlbSubstitute') != null) {
            TlbSubstitute.get_items().getItemById('tlbItemEdit_TlbSubstitute').set_enabled(false);
            TlbSubstitute.get_items().getItemById('tlbItemEdit_TlbSubstitute').set_imageUrl('edit_silver.png');
        }
        if (TlbSubstitute.get_items().getItemById('tlbItemDelete_TlbSubstitute') != null) {
            TlbSubstitute.get_items().getItemById('tlbItemDelete_TlbSubstitute').set_enabled(false);
            TlbSubstitute.get_items().getItemById('tlbItemDelete_TlbSubstitute').set_imageUrl('remove_silver.png');
        }
        TlbSubstitute.get_items().getItemById('tlbItemSave_TlbSubstitute').set_enabled(true);
        TlbSubstitute.get_items().getItemById('tlbItemSave_TlbSubstitute').set_imageUrl('save.png');
        TlbSubstitute.get_items().getItemById('tlbItemCancel_TlbSubstitute').set_enabled(true);
        TlbSubstitute.get_items().getItemById('tlbItemCancel_TlbSubstitute').set_imageUrl('cancel.png');
        if (TlbSubstitute.get_items().getItemById('tlbItemSubstituteSettings_TlbSubstitute') != null && CurrentPageState_Substitute != 'Edit') {
            TlbSubstitute.get_items().getItemById('tlbItemSubstituteSettings_TlbSubstitute').set_enabled(false);
            TlbSubstitute.get_items().getItemById('tlbItemSubstituteSettings_TlbSubstitute').set_imageUrl('regulation_silver.png');
        }
        if (CurrentPageState_Substitute != 'Delete') {
            if (TlbSubstituteSelect_Substitute.get_items().getItemById('tlbItemSubstituteSelect_TlbSubstituteSelect_Substitute') != null) {
                TlbSubstituteSelect_Substitute.get_items().getItemById('tlbItemSubstituteSelect_TlbSubstituteSelect_Substitute').set_enabled(true);
                TlbSubstituteSelect_Substitute.get_items().getItemById('tlbItemSubstituteSelect_TlbSubstituteSelect_Substitute').set_imageUrl('arrowDown.jpg');
            }
            ChangeCalendarsEnabled_Substitute('enable');
        }
        if (TlbManagerSelect_Substitute.get_items().getItemById('tlbItemManagerSelect_TlbManagerSelect_Substitute') != null) {
            TlbManagerSelect_Substitute.get_items().getItemById('tlbItemManagerSelect_TlbManagerSelect_Substitute').set_enabled(true);
            TlbManagerSelect_Substitute.get_items().getItemById('tlbItemManagerSelect_TlbManagerSelect_Substitute').set_imageUrl('arrowDown.jpg');
        }
        if (state == 'Edit')
            NavigateSubstitute_Substitute(GridSubstitutes_Substitute.getSelectedItems()[0]);
        if (state == 'Delete')
            Substitute_onSave();
    }
    if (state == 'View') {
        if (TlbSubstitute.get_items().getItemById('tlbItemNew_TlbSubstitute') != null) {
            TlbSubstitute.get_items().getItemById('tlbItemNew_TlbSubstitute').set_enabled(true);
            TlbSubstitute.get_items().getItemById('tlbItemNew_TlbSubstitute').set_imageUrl('add.png');
        }
        if (TlbSubstitute.get_items().getItemById('tlbItemEdit_TlbSubstitute') != null) {
            TlbSubstitute.get_items().getItemById('tlbItemEdit_TlbSubstitute').set_enabled(true);
            TlbSubstitute.get_items().getItemById('tlbItemEdit_TlbSubstitute').set_imageUrl('edit.png');
        }
        if (TlbSubstitute.get_items().getItemById('tlbItemDelete_TlbSubstitute') != null) {
            TlbSubstitute.get_items().getItemById('tlbItemDelete_TlbSubstitute').set_enabled(true);
            TlbSubstitute.get_items().getItemById('tlbItemDelete_TlbSubstitute').set_imageUrl('remove.png');
        }
        if (TlbSubstitute.get_items().getItemById('tlbItemSubstituteSettings_TlbSubstitute') != null) {
            TlbSubstitute.get_items().getItemById('tlbItemSubstituteSettings_TlbSubstitute').set_enabled(true);
            TlbSubstitute.get_items().getItemById('tlbItemSubstituteSettings_TlbSubstitute').set_imageUrl('regulation.png');
        }
        TlbSubstitute.get_items().getItemById('tlbItemSave_TlbSubstitute').set_enabled(false);
        TlbSubstitute.get_items().getItemById('tlbItemSave_TlbSubstitute').set_imageUrl('save_silver.png');
        TlbSubstitute.get_items().getItemById('tlbItemCancel_TlbSubstitute').set_enabled(false);
        TlbSubstitute.get_items().getItemById('tlbItemCancel_TlbSubstitute').set_imageUrl('cancel_silver.png');
        if (box_ManagersSelect_Substitute_IsShown)
            ManagersSelectBox_Substitute_onDropDownDiveStateChange();
        if (box_SubstitutesSelect_Substitute_IsShown)
            SubstituteSelectBox_Substitute_onDropDownDiveStateChange();
        if (TlbManagerSelect_Substitute.get_items().getItemById('tlbItemManagerSelect_TlbManagerSelect_Substitute') != null) {
            TlbManagerSelect_Substitute.get_items().getItemById('tlbItemManagerSelect_TlbManagerSelect_Substitute').set_enabled(false);
            TlbManagerSelect_Substitute.get_items().getItemById('tlbItemManagerSelect_TlbManagerSelect_Substitute').set_imageUrl('arrowDown_silver.jpg');
        }
        if (TlbSubstituteSelect_Substitute.get_items().getItemById('tlbItemSubstituteSelect_TlbSubstituteSelect_Substitute') != null) {
            TlbSubstituteSelect_Substitute.get_items().getItemById('tlbItemSubstituteSelect_TlbSubstituteSelect_Substitute').set_enabled(false);
            TlbSubstituteSelect_Substitute.get_items().getItemById('tlbItemSubstituteSelect_TlbSubstituteSelect_Substitute').set_imageUrl('arrowDown_silver.jpg');
        }
        ChangeCalendarsEnabled_Substitute('disable');
    }
}

function NavigateSubstitute_Substitute(selectedSubstituteItem) {
    if (selectedSubstituteItem != null) {
        SelectedManager_Substitute = new Object();
        SelectedManager_Substitute.ID = selectedSubstituteItem.getMember('Manager.ThePerson.ID').get_text();
        document.getElementById('tdManagerInfo_Substitute').innerHTML = document.getElementById('cmbManagers_Substitute_Input').value = SelectedManager_Substitute.Name = selectedSubstituteItem.getMember('Manager.ThePerson.Name').get_text();
        SelectedPersonnel_Substitute = new Object();
        SelectedPersonnel_Substitute.ID = selectedSubstituteItem.getMember('Person.ID').get_text();
        document.getElementById('tdPersonnelInfo_Substitute').innerHTML = document.getElementById('cmbPersonnel_Substitute_Input').value = SelectedPersonnel_Substitute.Name = selectedSubstituteItem.getMember('Person.Name').get_text();
        SelectedPersonnel_Substitute.BarCode = selectedSubstituteItem.getMember('Person.PersonCode').get_text();
        SelectedPersonnel_Substitute.OrganizationPostID = selectedSubstituteItem.getMember('Person.OrganizationUnit.ID').get_text();
        SelectedPersonnel_Substitute.OrganizationPostName = selectedSubstituteItem.getMember('Person.OrganizationUnit.Name').get_text();
        switch (parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpMasterFromDate_Substitute').value = selectedSubstituteItem.getMember('TheFromDate').get_text();
                document.getElementById('pdpMasterToDate_Substitute').value = selectedSubstituteItem.getMember('TheToDate').get_text();
                break;
            case 'en-US':
                document.getElementById('gdpMasterFromDate_Substitute_picker').value = selectedSubstituteItem.getMember('TheFromDate').get_text();
                document.getElementById('gdpMasterToDate_Substitute_picker').value = selectedSubstituteItem.getMember('TheToDate').get_text();
                break;
        }
    }
}

function SetActionMode_Substitute(state) {
    document.getElementById('ActionMode_Substitute').innerHTML = document.getElementById("hf" + state + "_Substitute").value;
}

function tlbItemNew_TlbSubstitute_onClick() {
    ChangePageState_Substitute('Add');
    ClearList_Substitute();
}

function tlbItemEdit_TlbSubstitute_onClick() {
    ChangePageState_Substitute('Edit');
}

function ClearList_Substitute() {
    cmbManagers_Substitute.unSelect();
    document.getElementById('cmbManagers_Substitute_Input').value = '';
    cmbPersonnel_Substitute.unSelect();
    document.getElementById('cmbPersonnel_Substitute_Input').value = '';
    ResetCalendars_Substitute();
    document.getElementById('tdManagerInfo_Substitute').innerHTML = '';
    document.getElementById('tdPersonnelInfo_Substitute').innerHTML = '';
}

function tlbItemDelete_TlbSubstitute_onClick() {
    ChangePageState_Substitute('Delete');
}

function tlbItemSubstituteSettings_TlbSubstitute_onClick() {
    ShowDialogSubstituteSettings();
}

function ShowDialogSubstituteSettings() {
    var SelectedItems_GridSubstitutes_Substitute = GridSubstitutes_Substitute.getSelectedItems();
    if (SelectedItems_GridSubstitutes_Substitute.length > 0) {
        var ObjDialogSubstituteSettings = new Object();
        ObjDialogSubstituteSettings.SubstituteID = SelectedItems_GridSubstitutes_Substitute[0].getMember('ID').get_text();
        ObjDialogSubstituteSettings.ManagerPersonnelID = SelectedItems_GridSubstitutes_Substitute[0].getMember('Manager.ThePerson.ID').get_text();
        ObjDialogSubstituteSettings.ManagerName = SelectedItems_GridSubstitutes_Substitute[0].getMember('Manager.ThePerson.Name').get_text();
        ObjDialogSubstituteSettings.PersonnelID = SelectedItems_GridSubstitutes_Substitute[0].getMember('Person.ID').get_text();
        ObjDialogSubstituteSettings.PersonnelName = SelectedItems_GridSubstitutes_Substitute[0].getMember('Person.Name').get_text();
        parent.DialogSubstituteSettings.set_value(ObjDialogSubstituteSettings);
        parent.DialogSubstituteSettings.Show();
        CollapseControls_Substitute(null);
    }
}

function tlbItemSave_TlbSubstitute_onClick() {
    CollapseControls_Substitute(null);
    Substitute_onSave();
}

function Substitute_onSave() {
    if (CurrentPageState_Substitute != 'Delete')
        UpdateSubstitute_Substitute();
    else
        ShowDialogConfirm('Delete');
}

function tlbItemCancel_TlbSubstitute_onClick() {
    ChangePageState_Substitute('View');
    ClearList_Substitute();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Substitute = confirmState;
    if (CurrentPageState_Substitute == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Substitute').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Substitute').value;
    DialogConfirm.Show();
    CollapseControls_Substitute(null);
}

function tlbItemExit_TlbSubstitute_onClick() {
    ShowDialogConfirm('Exit');
}

function UpdateSubstitute_Substitute() {
    ObjSubstitute_Substitute = new Object();
    ObjSubstitute_Substitute.ID = '0';
    ObjSubstitute_Substitute.ManagerPersonnelID = '0';
    ObjSubstitute_Substitute.ManagerName = null;
    ObjSubstitute_Substitute.PersonnelID = '0';
    ObjSubstitute_Substitute.PersonnelName = null;
    ObjSubstitute_Substitute.PersonnelBarCode = null;
    ObjSubstitute_Substitute.PersonnelOrganizationPostID = '0';
    ObjSubstitute_Substitute.PersonnelOrganizationPostName = null;
    ObjSubstitute_Substitute.FromDate = null;
    ObjSubstitute_Substitute.ToDate = null;

    var SelectedItems_GridSubstitutes_Substitute = GridSubstitutes_Substitute.getSelectedItems();
    if (SelectedItems_GridSubstitutes_Substitute.length > 0)
        ObjSubstitute_Substitute.ID = SelectedItems_GridSubstitutes_Substitute[0].getMember('ID').get_text();

    if(CurrentPageState_Substitute != 'Delete')
    {
        if (cmbManagers_Substitute.getSelectedItem() != undefined) {
            var selectedManager_Substitute = cmbManagers_Substitute.getSelectedItem().get_value();
            selectedManager_Substitute = eval('(' + selectedManager_Substitute + ')');
            ObjSubstitute_Substitute.ManagerPersonnelID = selectedManager_Substitute.ID;
            ObjSubstitute_Substitute.ManagerName = cmbManagers_Substitute.getSelectedItem().get_text();
        }
        else {
            if (SelectedManager_Substitute != null) {
                ObjSubstitute_Substitute.ManagerPersonnelID = SelectedManager_Substitute.ID;
                ObjSubstitute_Substitute.ManagerName = SelectedManager_Substitute.Name;
            }
        }
        if (cmbPersonnel_Substitute.getSelectedItem() != undefined) {
            var selectedPersonnel_Substitute = cmbPersonnel_Substitute.getSelectedItem();
            ObjSubstitute_Substitute.PersonnelName = selectedPersonnel_Substitute.get_text();
            ObjSubstitute_Substitute.PersonnelBarCode = selectedPersonnel_Substitute.BarCode;
            selectedPersonnel_Substitute = eval('(' + selectedPersonnel_Substitute.get_value() + ')');
            ObjSubstitute_Substitute.PersonnelID = selectedPersonnel_Substitute.ID;
            ObjSubstitute_Substitute.PersonnelOrganizationPostID = selectedPersonnel_Substitute.OrganizationPostID;
            ObjSubstitute_Substitute.PersonnelOrganizationPostName = selectedPersonnel_Substitute.OrganizationPostName;
        }
        else {
            if (SelectedPersonnel_Substitute != null) {
                ObjSubstitute_Substitute.PersonnelID = SelectedPersonnel_Substitute.ID;
                ObjSubstitute_Substitute.PersonnelName = SelectedPersonnel_Substitute.Name;
                ObjSubstitute_Substitute.PersonnelBarCode = SelectedPersonnel_Substitute.BarCode;
                ObjSubstitute_Substitute.PersonnelOrganizationPostID = SelectedPersonnel_Substitute.OrganizationPostID;
                ObjSubstitute_Substitute.PersonnelOrganizationPostName = SelectedPersonnel_Substitute.OrganizationPostName;
            }
        }
        switch (parent.SysLangID) {
            case 'fa-IR':
                ObjSubstitute_Substitute.FromDate = document.getElementById('pdpMasterFromDate_Substitute').value;
                ObjSubstitute_Substitute.ToDate= document.getElementById('pdpMasterToDate_Substitute').value;
                break;
            case 'en-US':
                ObjSubstitute_Substitute.FromDate = document.getElementById('gdpMasterFromDate_Substitute_picker').value;
                ObjSubstitute_Substitute.ToDate = document.getElementById('gdpMasterToDate_Substitute_picker').value;
                break;
        }
    }
    UpdateSubstitute_SubstitutePage(CharToKeyCode_Substitute(CurrentPageState_Substitute), CharToKeyCode_Substitute(ObjSubstitute_Substitute.ID), CharToKeyCode_Substitute(ObjSubstitute_Substitute.ManagerPersonnelID), CharToKeyCode_Substitute(ObjSubstitute_Substitute.PersonnelID), CharToKeyCode_Substitute(ObjSubstitute_Substitute.FromDate), CharToKeyCode_Substitute(ObjSubstitute_Substitute.ToDate));
    DialogWaiting.Show();
}

function UpdateSubstitute_SubstitutePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Substitute').value;
            Response[1] = document.getElementById('hfConnectionError_Substitute').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            Substitute_OnAfterUpdate(Response);
            ChangePageState_Substitute('View');
            ClearList_Substitute();
        }
        else {
            if (CurrentPageState_Substitute == 'Delete')
                ChangePageState_Substitute('View');
        }
    }
}

function Substitute_OnAfterUpdate(Response) {
    if (ObjSubstitute_Substitute != null) {
        var ManagerPersonnelID = ObjSubstitute_Substitute.ManagerPersonnelID;
        var ManagerName = ObjSubstitute_Substitute.ManagerName;
        var PersonnelID = ObjSubstitute_Substitute.PersonnelID;
        var PersonnelName = ObjSubstitute_Substitute.PersonnelName;
        var PersonnelBarCode = ObjSubstitute_Substitute.PersonnelBarCode;
        var PersonnelOrganizationPostID = ObjSubstitute_Substitute.PersonnelOrganizationPostID;
        var PersonnelOrganizationPostName = ObjSubstitute_Substitute.PersonnelOrganizationPostName;
        var FromDate = ObjSubstitute_Substitute.FromDate;
        var ToDate = ObjSubstitute_Substitute.ToDate;

        var SubstituteItem = null;
        GridSubstitutes_Substitute.beginUpdate();
        switch (CurrentPageState_Substitute) {
            case 'Add':
                SubstituteItem = GridSubstitutes_Substitute.get_table().addEmptyRow(GridSubstitutes_Substitute.get_recordCount());
                SubstituteItem.setValue(0, Response[3], false);
                GridSubstitutes_Substitute.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridSubstitutes_Substitute.selectByKey(Response[3], 0, false);
                SubstituteItem = GridSubstitutes_Substitute.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridSubstitutes_Substitute.selectByKey(ObjSubstitute_Substitute.ID, 0, false);
                GridSubstitutes_Substitute.deleteSelected();
                break;
        }
        if (CurrentPageState_Substitute != 'Delete') {
            SubstituteItem.setValue(1, PersonnelBarCode, false);
            SubstituteItem.setValue(2, PersonnelName, false);
            SubstituteItem.setValue(3, PersonnelOrganizationPostName, false);
            SubstituteItem.setValue(4, FromDate, false);
            SubstituteItem.setValue(5, ToDate, false);
            SubstituteItem.setValue(6, PersonnelID, false);
            SubstituteItem.setValue(7, PersonnelOrganizationPostID, false);
            SubstituteItem.setValue(8, ManagerPersonnelID, false);
            SubstituteItem.setValue(9, ManagerName, false);
        }
        GridSubstitutes_Substitute.endUpdate();
    }
}

function tlbItemManagerSelect_TlbManagerSelect_Substitute_onClick() {
    ManagersSelectBox_Substitute_onDropDownDiveStateChange();
}

function tlbItemRefresh_TlbPaging_ManagersSelect_Substitute_onClick() {
    LoadState_cmbManagers_Substitute = 'Normal';
    ChangePage_lookUpBox_Substitute('Manager', 'Refresh');
}

function tlbItemFirst_TlbPaging_ManagersSelect_Substitute_onClick() {
    ChangePage_lookUpBox_Substitute('Manager', 'First');
}

function tlbItemBefore_TlbPaging_ManagersSelect_Substitute_onClick() {
    ChangePage_lookUpBox_Substitute('Manager', 'Before');
}

function tlbItemNext_TlbPaging_ManagersSelect_Substitute_onClick() {
    ChangePage_lookUpBox_Substitute('Manager', 'Next');
}

function tlbItemLast_TlbPaging_ManagersSelect_Substitute_onClick() {
    ChangePage_lookUpBox_Substitute('Manager', 'Last');
}

function ChangePage_lookUpBox_Substitute(lookUptype, pageState) {
    var currentPageIndex = 0;
    var pageCount = 0;
    switch (lookUptype) {
        case 'Manager':
            currentPageIndex = CurrentPageIndex_cmbManagers_Substitute;
            pageCount = document.getElementById('hfManagerPageCount_Substitute').value;
            break;
        case 'Substitute':
            currentPageIndex = CurrentPageIndex_cmbPersonnel_Substitute;
            pageCount = document.getElementById('hfPersonnelPageCount_Substitute').value;
            break;
    }
    switch (pageState) {
        case 'First':
            SetPageIndex_lookUpBox_Substitute(lookUptype, 0);
            break;
        case 'Before':
            if (currentPageIndex != 0) {
                currentPageIndex = currentPageIndex - 1;
                SetPageIndex_lookUpBox_Substitute(lookUptype, currentPageIndex);
            }
            break;
        case 'Next':
            if (currentPageIndex < parseInt(pageCount) - 1) {
                currentPageIndex = currentPageIndex + 1;
                SetPageIndex_lookUpBox_Substitute(lookUptype, currentPageIndex);
            }
            break;
        case 'Last':
            SetPageIndex_lookUpBox_Substitute(lookUptype, parseInt(pageCount) - 1);
            break;
        case 'Refresh':
            SetPageIndex_lookUpBox_Substitute(lookUptype, 0);
            break;
    }
    switch (lookUptype) {
        case 'Manager':
            CurrentPageIndex_cmbManagers_Substitute = currentPageIndex;
            break;
        case 'Substitute':
            CurrentPageIndex_cmbPersonnel_Substitute = currentPageIndex;
            break;
    }
}

function SetPageIndex_lookUpBox_Substitute(lookUpType, pageIndex) {
    switch (lookUpType) {
        case 'Manager':
            CurrentPageIndex_cmbManagers_Substitute = pageIndex;
            Fill_cmbManagers_Substitute(pageIndex);
            break;
        case 'Substitute':
            CurrentPageIndex_cmbPersonnel_Substitute = pageIndex;
            Fill_cmbPersonnel_Substitute(pageIndex);
            break;
    }
}

function Substitute_onAfterPersonnelAdvancedSearch(lookUpType, SearchTerm) {
    switch (lookUpType) {
        case 'Manager':
            AdvancedSearchTerm_cmbManagers_Substitute = SearchTerm;
            break;
        case 'Substitute':
            AdvancedSearchTerm_cmbPersonnel_Substitute = SearchTerm;
            break;
    }
    ChangePage_lookUpBox_Substitute(lookUpType,'First');
}

function Fill_cmbManagers_Substitute(pageIndex) {
    var SearchTerm = '';
    var pageSize = parseInt(document.getElementById('hfManagerPageSize_Substitute').value);
    switch (LoadState_cmbManagers_Substitute) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm = document.getElementById('txtManagerSearch_Substitute').value;
            break;
        case 'AdvancedSearch':
            SearchTerm = AdvancedSearchTerm_cmbManagers_Substitute;
            break;
    }
    CallBack_cmbManagers_Substitute.callback(CharToKeyCode_Substitute(LoadState_cmbManagers_Substitute), CharToKeyCode_Substitute(pageSize.toString()), CharToKeyCode_Substitute(pageIndex.toString()), CharToKeyCode_Substitute(SearchTerm));
}

function CharToKeyCode_Substitute(str) {
    var OutStr = '';
    if (str != null && str != undefined) {
        for (var i = 0; i < str.length; i++) {
            var KeyCode = str.charCodeAt(i);
            var CharKeyCode = '//' + KeyCode;
            OutStr += CharKeyCode;
        }
    }
    return OutStr;
}

function Fill_cmbPersonnel_Substitute(pageIndex) {
    var SearchTerm = '';
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_Substitute').value);
    switch (LoadState_cmbPersonnel_Substitute) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm = document.getElementById('txtSubstituteSearch_Substitute').value;
            break;
        case 'AdvancedSearch':
            SearchTerm = AdvancedSearchTerm_cmbPersonnel_Substitute;
            break;
    }
    CallBack_cmbPersonnel_Substitute.callback(CharToKeyCode_Substitute(LoadState_cmbPersonnel_Substitute), CharToKeyCode_Substitute(pageSize.toString()), CharToKeyCode_Substitute(pageIndex.toString()), CharToKeyCode_Substitute(SearchTerm));
}

function cmbManagers_Substitute_onExpand(sender, e) {
    CollapseControls_Substitute(cmbManagers_Substitute);
    SetPosition_cmbManagers_Substitute();
    if (cmbManagers_Substitute.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbManagers_Substitute == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbManagers_Substitute = true;
        ChangePage_lookUpBox_Substitute('Manager', 'First');
    }
    if (cmbManagers_Substitute.getSelectedItem() == undefined && SelectedManager_Substitute != null)
        document.getElementById('cmbManagers_Substitute_Input').value = SelectedManager_Substitute.Name;
}

function CallBack_cmbManagers_Substitute_onBeforeCallback(sender, e) {
    cmbManagers_Substitute.dispose();
}

function CallBack_cmbManagers_Substitute_onCallbackComplete(sender, e) {
    document.getElementById('clmnName_cmbManagers_Substitute').innerHTML = document.getElementById('hfclmnName_cmbManagers_Substitute').value;
    document.getElementById('clmnBarCode_cmbManagers_Substitute').innerHTML = document.getElementById('hfclmnBarCode_cmbManagers_Substitute').value;
    document.getElementById('clmnCardNum_cmbManagers_Substitute').innerHTML = document.getElementById('hfclmnCardNum_cmbManagers_Substitute').value;

    SetPosition_cmbManagers_Substitute();

    var error = document.getElementById('ErrorHiddenField_Managers_Substitute').value;
    if (error == "") {
        document.getElementById('cmbManagers_Substitute_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbManagers_Substitute_DropImage').mousedown();
        else
            cmbManagers_Substitute.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbManagers_Substitute_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CallBack_cmbManagers_Substitute_onCallbackError(sender, e) {
    ShowConnectionError_Substitutes();
}

function ShowConnectionError_Substitutes() {
    var error = document.getElementById('hfErrorType_Substitute').value;
    var errorBody = document.getElementById('hfConnectionError_Substitute').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemSearch_TlbManagerSearch_Substitute_onClick() {
    LoadState_cmbManagers_Substitute = 'Search';
    CurrentPageIndex_cmbManagers_Substitute = 0;
    CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbManagers_Substitute = true;
    ChangePage_lookUpBox_Substitute('Manager', 'First');
}

function tlbItemAdvancedSearch_TlbAdvancedManagerSearch_Substitute_onClick() {
    LoadState_cmbManagers_Substitute = 'AdvancedSearch';
    CurrentPageIndex_cmbManagers_Substitute = 0;
    CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbManagers_Substitute = true;
    ShowDialogPersonnelSearch('Manager_Substitute');    
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
    CollapseControls_Substitute(null);
}

function tlbItemSubstituteSelect_TlbSubstituteSelect_Substitute_onClick() {
    SubstituteSelectBox_Substitute_onDropDownDiveStateChange();
}

function tlbItemRefresh_TlbPaging_SubstitutesSelect_Substitute_onClick() {
    LoadState_cmbPersonnel_Substitute = 'Normal';
    ChangePage_lookUpBox_Substitute('Substitute', 'Refresh');
}

function tlbItemFirst_TlbPaging_SubstitutesSelect_Substitute_onClick() {
    ChangePage_lookUpBox_Substitute('Substitute', 'First');
}

function tlbItemBefore_TlbPaging_SubstitutesSelect_Substitute_onClick() {
    ChangePage_lookUpBox_Substitute('Substitute', 'Before');
}

function tlbItemNext_TlbPaging_SubstitutesSelect_Substitute_onClick() {
    ChangePage_lookUpBox_Substitute('Substitute', 'Next');
}

function tlbItemLast_TlbPaging_SubstitutesSelect_Substitute_onClick() {
    ChangePage_lookUpBox_Substitute('Substitute', 'Last');
}

function cmbPersonnel_Substitute_onExpand(sender, e) {
    CollapseControls_Substitute(cmbPersonnel_Substitute);
    if (cmbPersonnel_Substitute.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_Substitute == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_Substitute = true;
        ChangePage_lookUpBox_Substitute('Substitute', 'First');
    }
    if (cmbPersonnel_Substitute.getSelectedItem() == undefined && SelectedPersonnel_Substitute != null)
        document.getElementById('cmbPersonnel_Substitute_Input').value = SelectedPersonnel_Substitute.Name;
}

function CallBack_cmbPersonnel_Substitute_onBeforeCallback(sender, e) {
    cmbPersonnel_Substitute.dispose();
}

function CallBack_cmbPersonnel_Substitute_onCallbackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_Substitute').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_Substitute').value;
    document.getElementById('clmnBarCode_cmbPersonnel_Substitute').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_Substitute').value;
    document.getElementById('clmnCardNum_cmbPersonnel_Substitute').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_Substitute').value;

    var error = document.getElementById('ErrorHiddenField_Personnel_Substitute').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_Substitute_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_Substitute_DropImage').mousedown();
        else
            cmbPersonnel_Substitute.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_Substitute_DropDown').style.display = 'none';
    }
}

function CallBack_cmbPersonnel_Substitute_onCallbackError(sender, e) {
    ShowConnectionError_Substitutes();
}

function tlbItemSearch_TlbSubstituteSearch_Substitute_onClick() {
    LoadState_cmbPersonnel_Substitute = 'Search';
    CurrentPageIndex_cmbPersonnel_Substitute = 0;
    ChangePage_lookUpBox_Substitute('Substitute', 'First');
}

function tlbItemAdvancedSearch_TlbAdvancedSubstituteSearch_Substitute_onClick() {
    LoadState_cmbPersonnel_Substitute = 'AdvancedSearch';
    CurrentPageIndex_cmbPersonnel_Substitute = 0;
    ShowDialogPersonnelSearch('Personnel_Substitute');
}

function Refresh_GridSubstitutes_Substitute() {
    LoadState_GridSubstitutes_Substitute = 'Normal';
    Fill_GridSubstitutes_Substitute();
}

function tlbItemSearch_TlbSubstituteQuickSearch_onClick() {
    LoadState_GridSubstitutes_Substitute = 'Search';
    Fill_GridSubstitutes_Substitute();
}

function Fill_GridSubstitutes_Substitute() {
    var ManagerPersonnelID = null;
    if (cmbManagers_Substitute.getSelectedItem() != null) {
        ManagerItemValue = cmbManagers_Substitute.getSelectedItem().get_value();
        ManagerItemValue = eval('(' + ManagerItemValue + ')');
        ManagerPersonnelID = ManagerItemValue.ID;
    }
    else {
        if (SelectedManager_Substitute != null)
            ManagerPersonnelID = SelectedManager_Substitute.ID;
    }
    if (ManagerPersonnelID != null) {
        document.getElementById('loadingPanel_GridSubstitutes_Substitute').innerHTML = document.getElementById('hfloadingPanel_GridSubstitutes_Substitute').value;
        var SearchTerm = null;
        switch (LoadState_GridSubstitutes_Substitute) {
            case 'Normal':
                SearchTerm = '';
                break;
            case 'Search':
                SearchTerm = document.getElementById('txtSerchTerm_Substitute').value;
                break;
        }
        CallBack_GridSubstitutes_Substitute.callback(CharToKeyCode_Substitute(LoadState_GridSubstitutes_Substitute), CharToKeyCode_Substitute(ManagerPersonnelID), CharToKeyCode_Substitute(SearchTerm));
    }
}

function GridSubstitutes_Substitute_onItemSelect(sender, e) {
    if (CurrentPageState_Substitute != 'Add')
        NavigateSubstitute_Substitute(e.get_item());
}

function GridSubstitutes_Substitute_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridSubstitutes_Substitute').innerHTML = '';        
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Substitute) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateSubstitute_Substitute();
            break;
        case 'Exit':
            ClearList_Substitute();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Substitute('View');
}

function cmbManagers_Substitute_onChange(sender, e) {
    if (CurrentPageState_Substitute != 'View') {
        SetManagerInfo_Substitute();
        Fill_GridSubstitutes_Substitute();
    }
}

function SetManagerInfo_Substitute() {
    if (cmbManagers_Substitute.getSelectedItem() != undefined)
        document.getElementById('tdManagerInfo_Substitute').innerHTML = cmbManagers_Substitute.getSelectedItem().get_text();
}

function cmbPersonnel_Substitute_onChange(sender, e) {
    if(CurrentPageState_Substitute != 'View')
       SetPersonnelInfo_Substitute();
}

function SetPersonnelInfo_Substitute() {
    if (cmbPersonnel_Substitute.getSelectedItem() != undefined)
        document.getElementById('tdPersonnelInfo_Substitute').innerHTML = cmbPersonnel_Substitute.getSelectedItem().get_text();
}

function CallBack_GridSubstitutes_Substitute_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Substitute').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridSubstitutes_Substitute();
    }
}

function CollapseControls_Substitute(exception) {
    if (exception == null || exception != cmbManagers_Substitute)
        cmbManagers_Substitute.collapse();
    if (exception == null || exception != cmbPersonnel_Substitute)
        cmbPersonnel_Substitute.collapse();
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpMasterFromDate_Substitute');
}

function tlbItemFormReconstruction_TlbSubstitute_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvSubstituteIntroduction_iFrame').src = 'Substitute.aspx';
}

function tlbItemHelp_TlbSubstitute_onClick() {
    LoadHelpPage('tlbItemHelp_TlbSubstitute');
}

function SetPosition_cmbManagers_Substitute() {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('cmbManagers_Substitute_DropDown').style.left = document.getElementById('Mastertbl_Substitute').clientWidth - 400 + 'px';
    }
    if (parent.CurrentLangID == 'en-US') {
        document.getElementById('cmbManagers_Substitute_DropDown').style.left = '30px';
    }
}






