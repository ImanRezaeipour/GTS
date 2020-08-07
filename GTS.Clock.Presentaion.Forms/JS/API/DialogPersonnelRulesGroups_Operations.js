
var CurrentPageState_PersonnelRulesGroups = 'View';
var SelectedRuleGroup_PersonnelRulesGroups = null;
var ConfirmState_PersonnelRulesGroups = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjPersonnelRuleGroup_PersonnelRulesGroups = null;


function GetBoxesHeaders_PersonnelRulesGroups() {
    parent.document.getElementById('Title_DialogPersonnelRulesGroups').innerHTML = document.getElementById('hfTitle_DialogPersonnelRulesGroups').value;
    document.getElementById('header_PersonnelRulesGroups_PersonnelRulesGroups').innerHTML = document.getElementById('hfheader_PersonnelRulesGroups_PersonnelRulesGroups').value;
}

function ViewCurrentLangCalendars_PersonnelRulesGroups() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpFromDate_PersonnelRulesGroups").parentNode.removeChild(document.getElementById("pdpFromDate_PersonnelRulesGroups"));
            document.getElementById("pdpFromDate_PersonnelRulesGroupsimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_PersonnelRulesGroupsimgbt"));
            document.getElementById("pdpToDate_PersonnelRulesGroups").parentNode.removeChild(document.getElementById("pdpToDate_PersonnelRulesGroups"));
            document.getElementById("pdpToDate_PersonnelRulesGroupsimgbt").parentNode.removeChild(document.getElementById("pdpToDate_PersonnelRulesGroupsimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_FromDateCalendars_PersonnelRulesGroups").removeChild(document.getElementById("Container_gCalFromDate_PersonnelRulesGroups"));
            document.getElementById("Container_ToDateCalendars_PersonnelRulesGroups").removeChild(document.getElementById("Container_gCalToDate_PersonnelRulesGroups"));
            break;
    }
}

function ResetCalendars_PersonnelRulesGroups() {
    var currentDate_PersonnelRulesGroups = document.getElementById('hfCurrentDate_PersonnelRulesGroups').value;
    switch (parent.parent.SysLangID) {
        case 'en-US':
            currentDate_PersonnelRulesGroups = new Date(currentDate_PersonnelRulesGroups);
            gdpFromDate_PersonnelRulesGroups.setSelectedDate(currentDate_PersonnelRulesGroups);
            gCalFromDate_PersonnelRulesGroups.setSelectedDate(currentDate_PersonnelRulesGroups);
            gdpToDate_PersonnelRulesGroups.setSelectedDate(currentDate_PersonnelRulesGroups);
            gCalToDate_PersonnelRulesGroups.setSelectedDate(currentDate_PersonnelRulesGroups);
            break;
        case 'fa-IR':
            document.getElementById('pdpFromDate_PersonnelRulesGroups').value = currentDate_PersonnelRulesGroups;
            document.getElementById('pdpToDate_PersonnelRulesGroups').value = currentDate_PersonnelRulesGroups;
            break;
    }
}

function SetActionMode_PersonnelRulesGroups(state) {
    document.getElementById('ActionMode_PersonnelRulesGroups').innerHTML = document.getElementById('hf' + state + '_PersonnelRulesGroups').value;
}

function ChangeCalendarsEnabled_PersonnelRulesGroups(state) {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ChangeCalendarEnabled_PersonnelRulesGroups('pdpFromDate_PersonnelRulesGroups', state);
            ChangeCalendarEnabled_PersonnelRulesGroups('pdpToDate_PersonnelRulesGroups', state);
            break;
        case 'en-US':
            ChangeCalendarEnabled_PersonnelRulesGroups('gdpFromDate_PersonnelRulesGroups', state);
            ChangeCalendarEnabled_PersonnelRulesGroups('gdpToDate_PersonnelRulesGroups', state);
            break;
    }
}

function ChangeCalendarEnabled_PersonnelRulesGroups(cal, state) {
    var disabled = null;
    switch (state) {
        case 'disable':
            disabled = 'disabled';
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    document.getElementById(cal).onclick = " ";
                    document.getElementById(cal + 'imgbt').onclick = " ";
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
                        CalendarsViewManage_PersonnelRulesGroups(cal);
                    };
                    break;
            }
            break;
    }
}

function CalendarsViewManage_PersonnelRulesGroups(gdpID) {
    var CalID_PersonnelRulesGroups = 'gCal' + gdpID.substr(3, gdpID.length - 3);
    var Cal_PersonnelRulesGroups = eval(CalID_PersonnelRulesGroups);
    if (!Cal_PersonnelRulesGroups.get_popUpShowing())
        Cal_PersonnelRulesGroups.show();
    else
        Cal_PersonnelRulesGroups.hide();
}


function Fill_GridPersonnelRulesGroups_PersonnelRulesGroups() {
    document.getElementById('header_PersonnelRulesGroups_PersonnelRulesGroups').innerHTML = document.getElementById('hfheader_PersonnelRulesGroups_PersonnelRulesGroups').value;
    var PersonnelID = parent.parent.DialogPersonnelMainInformation.get_value().PersonnelID;
    CallBack_GridPersonnelRulesGroups_PersonnelRulesGroups.callback(CharToKeyCode_PersonnelRulesGroups(PersonnelID));
}

function CharToKeyCode_PersonnelRulesGroups(str) {
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

function NavigatePersonnelRuleGroup_PersonnelRulesGroups(selectedPersonnelRuleGroup) {
    if (selectedPersonnelRuleGroup != undefined) {
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpFromDate_PersonnelRulesGroups').value = selectedPersonnelRuleGroup.getMember('UIFromDate').get_text();
                document.getElementById('pdpToDate_PersonnelRulesGroups').value = selectedPersonnelRuleGroup.getMember('UIToDate').get_text();
                break;
            case 'en-US':
                var gFromDate = new Date(selectedPersonnelRuleGroup.getMember('UIFromDate').get_text());
                gdpFromDate_PersonnelRulesGroups.setSelectedDate(gFromDate);
                gdpFromDate_PersonnelRulesGroups.setSelectedDate(gFromDate);
                var gToDate = new Date(selectedPersonnelRuleGroup.getMember('UIToDate').get_text());
                gdpToDate_PersonnelRulesGroups.setSelectedDate(gToDate);
                gdpToDate_PersonnelRulesGroups.setSelectedDate(gToDate);
                break;
        }
        SelectedRuleGroup_PersonnelRulesGroups = new Object();
        SelectedRuleGroup_PersonnelRulesGroups.ID = selectedPersonnelRuleGroup.getMember('RuleCategory.ID').get_text();
        document.getElementById('cmbRulesGroups_PersonnelRulesGroups_Input').value = SelectedRuleGroup_PersonnelRulesGroups.Name = selectedPersonnelRuleGroup.getMember('RuleCategory.Name').get_text();
    }
}

function ChangePageState_PersonnelRuleGroups(state) {
    SetActionMode_PersonnelRulesGroups(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        CurrentPageState_PersonnelRulesGroups = state;
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemNew_TlbPersonnelRulesGroups').set_enabled(false);
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemNew_TlbPersonnelRulesGroups').set_imageUrl('add_silver.png');
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemEdit_TlbPersonnelRulesGroups').set_enabled(false);
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemEdit_TlbPersonnelRulesGroups').set_imageUrl('edit_silver.png');
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemDelete_TlbPersonnelRulesGroups').set_enabled(false);
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemDelete_TlbPersonnelRulesGroups').set_imageUrl('remove_silver.png');
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemSave_TlbPersonnelRulesGroups').set_enabled(true);
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemSave_TlbPersonnelRulesGroups').set_imageUrl('save.png');
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemCancel_TlbPersonnelRulesGroups').set_enabled(true);
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemCancel_TlbPersonnelRulesGroups').set_imageUrl('cancel.png');
        cmbRulesGroups_PersonnelRulesGroups.enable();
        ChangeCalendarsEnabled_PersonnelRulesGroups('enable');
        if (state == 'Edit')
            NavigatePersonnelRuleGroup_PersonnelRulesGroups(GridPersonnelRulesGroups_PersonnelRulesGroups.getSelectedItems()[0]);
        if (state == 'Delete')
            PersonnelRuleGroup_onSave();
    }
    if (state == 'View') {
        CurrentPageState_PersonnelRulesGroups = state;
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemNew_TlbPersonnelRulesGroups').set_enabled(true);
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemNew_TlbPersonnelRulesGroups').set_imageUrl('add.png');
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemEdit_TlbPersonnelRulesGroups').set_enabled(true);
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemEdit_TlbPersonnelRulesGroups').set_imageUrl('edit.png');
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemDelete_TlbPersonnelRulesGroups').set_enabled(true);
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemDelete_TlbPersonnelRulesGroups').set_imageUrl('remove.png');
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemSave_TlbPersonnelRulesGroups').set_enabled(false);
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemSave_TlbPersonnelRulesGroups').set_imageUrl('save_silver.png');
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemCancel_TlbPersonnelRulesGroups').set_enabled(false);
        TlbPersonnelRulesGroups.get_items().getItemById('tlbItemCancel_TlbPersonnelRulesGroups').set_imageUrl('cancel_silver.png');
        ChangeCalendarsEnabled_PersonnelRulesGroups('disable');
        cmbRulesGroups_PersonnelRulesGroups.disable();
    }
}

function PersonnelRuleGroup_onSave() {
    if (CurrentPageState_PersonnelRulesGroups != 'Delete')
        UpdatePersonnelRuleGroup_PersonnelRulesGroups();
    else
        ShowDialogConfirm('Delete');
}

function UpdatePersonnelRuleGroup_PersonnelRulesGroups() {
    ObjPersonnelRuleGroup_PersonnelRulesGroups = new Object();
    ObjPersonnelRuleGroup_PersonnelRulesGroups.ID = '0';
    ObjPersonnelRuleGroup_PersonnelRulesGroups.RuleGroupID = '0';
    ObjPersonnelRuleGroup_PersonnelRulesGroups.RuleGroupName = null;
    ObjPersonnelRuleGroup_PersonnelRulesGroups.PersonnelID = '0';
    ObjPersonnelRuleGroup_PersonnelRulesGroups.FromDate = null;
    ObjPersonnelRuleGroup_PersonnelRulesGroups.ToDate = null;

    var SelectedItems_GridPersonnelRulesGroups_PersonnelRulesGroups = GridPersonnelRulesGroups_PersonnelRulesGroups.getSelectedItems();
    if (SelectedItems_GridPersonnelRulesGroups_PersonnelRulesGroups.length > 0)
        ObjPersonnelRuleGroup_PersonnelRulesGroups.ID = SelectedItems_GridPersonnelRulesGroups_PersonnelRulesGroups[0].getMember("ID").get_text();

    ObjPersonnelRuleGroup_PersonnelRulesGroups.PersonnelID = parent.parent.DialogPersonnelMainInformation.get_value().PersonnelID;
    if (CurrentPageState_PersonnelRulesGroups != 'Delete') {
        if (cmbRulesGroups_PersonnelRulesGroups.getSelectedItem() != undefined) {
            ObjPersonnelRuleGroup_PersonnelRulesGroups.RuleGroupID = cmbRulesGroups_PersonnelRulesGroups.getSelectedItem().get_value();
            ObjPersonnelRuleGroup_PersonnelRulesGroups.RuleGroupName = cmbRulesGroups_PersonnelRulesGroups.getSelectedItem().get_text();
        }
        else {
            if (SelectedRuleGroup_PersonnelRulesGroups != null) {
                ObjPersonnelRuleGroup_PersonnelRulesGroups.RuleGroupID = SelectedRuleGroup_PersonnelRulesGroups.ID;
                ObjPersonnelRuleGroup_PersonnelRulesGroups.RuleGroupName = SelectedRuleGroup_PersonnelRulesGroups.Name;
            }
        }

        var FromDate_PersonnelRuleGroup = null;
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                FromDate_PersonnelRuleGroup = document.getElementById('pdpFromDate_PersonnelRulesGroups').value;
                break;
            case 'en-US':
                FromDate_PersonnelRuleGroup = document.getElementById('gdpFromDate_PersonnelRulesGroups_picker').value;
                break;
        }
        ObjPersonnelRuleGroup_PersonnelRulesGroups.FromDate = FromDate_PersonnelRuleGroup;

        var ToDate_PersonnelRuleGroup = null;
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                ToDate_PersonnelRuleGroup = document.getElementById('pdpToDate_PersonnelRulesGroups').value;
                break;
            case 'en-US':
                ToDate_PersonnelRuleGroup = document.getElementById('gdpToDate_PersonnelRulesGroups_picker').value;
                break;
        }
        ObjPersonnelRuleGroup_PersonnelRulesGroups.ToDate = ToDate_PersonnelRuleGroup;

    }
    UpdatePersonnelRuleGroup_PersonnelRulesGroupsPage(CharToKeyCode_PersonnelRulesGroups(CurrentPageState_PersonnelRulesGroups), CharToKeyCode_PersonnelRulesGroups(ObjPersonnelRuleGroup_PersonnelRulesGroups.ID), CharToKeyCode_PersonnelRulesGroups(ObjPersonnelRuleGroup_PersonnelRulesGroups.PersonnelID), CharToKeyCode_PersonnelRulesGroups(ObjPersonnelRuleGroup_PersonnelRulesGroups.RuleGroupID), CharToKeyCode_PersonnelRulesGroups(ObjPersonnelRuleGroup_PersonnelRulesGroups.RuleGroupName), CharToKeyCode_PersonnelRulesGroups(ObjPersonnelRuleGroup_PersonnelRulesGroups.FromDate), CharToKeyCode_PersonnelRulesGroups(ObjPersonnelRuleGroup_PersonnelRulesGroups.ToDate));
    DialogWaiting.Show();
}

function UpdatePersonnelRuleGroup_PersonnelRulesGroupsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_PersonnelRulesGroups();
            PersonnelRulesGroups_OnAfterUpdate(Response);
            ChangePageState_PersonnelRuleGroups('View');
            parent.parent.document.getElementById('DialogPersonnelMainInformation_IFrame').contentWindow.ChangePersonnel_onPersonnelRulesGroupsOperationCompleted(Response[4]);
        }
        else {
            if (CurrentPageState_PersonnelRulesGroups == 'Delete')
                ChangePageState_PersonnelRuleGroups('View');
        }
    }
}

function PersonnelRulesGroups_OnAfterUpdate(Response) {
    if (ObjPersonnelRuleGroup_PersonnelRulesGroups != null) {
        var PersonnelID = ObjPersonnelRuleGroup_PersonnelRulesGroups.PersonnelID;
        var RuleGroupID = ObjPersonnelRuleGroup_PersonnelRulesGroups.RuleGroupID;
        var RuleGroupName = ObjPersonnelRuleGroup_PersonnelRulesGroups.RuleGroupName;
        var FromDate = ObjPersonnelRuleGroup_PersonnelRulesGroups.FromDate;
        var ToDate = ObjPersonnelRuleGroup_PersonnelRulesGroups.ToDate;

        var PersonnelRuleGroupItem = null;
        GridPersonnelRulesGroups_PersonnelRulesGroups.beginUpdate();
        switch (CurrentPageState_PersonnelRulesGroups) {
            case 'Add':
                PersonnelRuleGroupItem = GridPersonnelRulesGroups_PersonnelRulesGroups.get_table().addEmptyRow(GridPersonnelRulesGroups_PersonnelRulesGroups.get_recordCount());
                PersonnelRuleGroupItem.setValue(0, Response[3], false);
                GridPersonnelRulesGroups_PersonnelRulesGroups.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridPersonnelRulesGroups_PersonnelRulesGroups.selectByKey(Response[3], 0, false);
                PersonnelRuleGroupItem = GridPersonnelRulesGroups_PersonnelRulesGroups.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridPersonnelRulesGroups_PersonnelRulesGroups.selectByKey(ObjPersonnelRuleGroup_PersonnelRulesGroups.ID, 0, false);
                GridPersonnelRulesGroups_PersonnelRulesGroups.deleteSelected();
                break;
        }
        if (CurrentPageState_PersonnelRulesGroups != 'Delete') {
            PersonnelRuleGroupItem.setValue(1, RuleGroupID, false);
            PersonnelRuleGroupItem.setValue(2, RuleGroupName, false);
            PersonnelRuleGroupItem.setValue(3, FromDate, false);
            PersonnelRuleGroupItem.setValue(4, ToDate, false);
        }
        GridPersonnelRulesGroups_PersonnelRulesGroups.endUpdate();
    }
}


function ShowDialogConfirm(confirmState) {
    ConfirmState_PersonnelRulesGroups = confirmState;
    if (CurrentPageState_PersonnelRulesGroups == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_PersonnelRulesGroups').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PersonnelRulesGroups').value;
    DialogConfirm.Show();
}

function ClearList_PersonnelRulesGroups()
{
    if (CurrentPageState_PersonnelRulesGroups != 'Edit') {
        document.getElementById('cmbRulesGroups_PersonnelRulesGroups_Input').value = document.getElementById('hfcmbAlarm_PersonnelRulesGroups').value;
        cmbRulesGroups_PersonnelRulesGroups.unSelect();
        SelectedRuleGroup_PersonnelRulesGroups = null;
        ResetCalendars_PersonnelRulesGroups();
        GridPersonnelRulesGroups_PersonnelRulesGroups.unSelectAll();
    }
}


function tlbItemNew_TlbPersonnelRulesGroups_onClick() {
    ChangePageState_PersonnelRuleGroups('Add');
    ClearList_PersonnelRulesGroups();
}

function tlbItemEdit_TlbPersonnelRulesGroups_onClick() {
    ChangePageState_PersonnelRuleGroups('Edit');
}

function tlbItemDelete_TlbPersonnelRulesGroups_onClick() {
    ChangePageState_PersonnelRuleGroups('Delete');
}

function tlbItemSave_TlbPersonnelRulesGroups_onClick() {
    PersonnelRuleGroup_onSave();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_PersonnelRulesGroups) {
        case 'Delete':
            DialogConfirm.Close();
            UpdatePersonnelRuleGroup_PersonnelRulesGroups();
            break;
        case 'Exit':
            ClearList_PersonnelRulesGroups();
            CloseDialogPersonnelRulesGroups();
            break;
    }
}

function CloseDialogPersonnelRulesGroups() {
    parent.document.getElementById('DialogPersonnelRulesGroups_IFrame').src = 'WhitePage.aspx';
    parent.DialogPersonnelRulesGroups.Close();
}


function tlbItemCancel_TlbPersonnelRulesGroups_onClick() {
    ChangePageState_PersonnelRuleGroups('View');
    ClearList_PersonnelRulesGroups();
}

function tlbItemExit_TlbPersonnelRulesGroups_onClick() {
    ShowDialogConfirm('Exit');
}

function cmbRulesGroups_PersonnelRulesGroups_onExpand(sender, e) {
    if (cmbRulesGroups_PersonnelRulesGroups.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRulesGroups_PersonnelRulesGroups == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRulesGroups_PersonnelRulesGroups = true;
        CallBackcmbRulesGroups_PersonnelRulesGroups.callback();
    }
    if (cmbRulesGroups_PersonnelRulesGroups.getSelectedItem() == undefined && SelectedRuleGroup_PersonnelRulesGroups != null)
        document.getElementById('cmbRulesGroups_PersonnelRulesGroups_Input').value = SelectedRuleGroup_PersonnelRulesGroups.Name;
}

function CallBackcmbRulesGroups_PersonnelRulesGroups_onBeforeCallback(sender, e) {
    cmbRulesGroups_PersonnelRulesGroups.dispose();
}

function CallBackcmbRulesGroups_PersonnelRulesGroups_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RulesGroups_PersonnelRulesGroups').value;
    if (error == "") {
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbRulesGroups_PersonnelRulesGroups_DropImage').mousedown();
        cmbRulesGroups_PersonnelRulesGroups.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function btn_gdpFromDate_PersonnelRulesGroups_OnMouseUp(event) {
    if (gCalFromDate_PersonnelRulesGroups.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpFromDate_PersonnelRulesGroups_OnDateChange(sender, e) {
    var FromDate = gdpFromDate_PersonnelRulesGroups.getSelectedDate();
    gCalFromDate_PersonnelRulesGroups.setSelectedDate(FromDate);
}

function btn_gdpFromDate_PersonnelRulesGroups_OnClick(event) {
    if (gCalFromDate_PersonnelRulesGroups.get_popUpShowing()) {
        gCalFromDate_PersonnelRulesGroups.hide();
    }
    else {
        gCalFromDate_PersonnelRulesGroups.setSelectedDate(gdpFromDate_PersonnelRulesGroups.getSelectedDate());
        gCalFromDate_PersonnelRulesGroups.show();
    }
}

function gCalFromDate_PersonnelRulesGroups_OnChange(sender, e) {
    var FromDate = gCalFromDate_PersonnelRulesGroups.getSelectedDate();
    gdpFromDate_PersonnelRulesGroups.setSelectedDate(FromDate);
}

function gCalFromDate_PersonnelRulesGroups_onLoad(sender, e) {
    window.gCalFromDate_PersonnelRulesGroups.PopUpObject.z = 25000000;
}

function btn_gdpToDate_PersonnelRulesGroups_OnMouseUp(event) {
    if (gCalToDate_PersonnelRulesGroups.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpToDate_PersonnelRulesGroups_OnDateChange(sender, e) {
    var ToDate = gdpToDate_PersonnelRulesGroups.getSelectedDate();
    gCalToDate_PersonnelRulesGroups.setSelectedDate(ToDate);
}

function btn_gdpToDate_PersonnelRulesGroups_OnClick(event) {
    if (gCalToDate_PersonnelRulesGroups.get_popUpShowing()) {
        gCalToDate_PersonnelRulesGroups.hide();
    }
    else {
        gCalToDate_PersonnelRulesGroups.setSelectedDate(gdpToDate_PersonnelRulesGroups.getSelectedDate());
        gCalToDate_PersonnelRulesGroups.show();
    }
}

function gCalToDate_PersonnelRulesGroups_OnChange(sender, e) {
    var ToDate = gCalToDate_PersonnelRulesGroups.getSelectedDate();
    gdpToDate_PersonnelRulesGroups.setSelectedDate(ToDate);
}

function gCalToDate_PersonnelRulesGroups_onLoad(sender, e) {
    window.gCalToDate_PersonnelRulesGroups.PopUpObject.z = 25000000;
}

function Refresh_GridPersonnelRulesGroups_PersonnelRulesGroups() {
    Fill_GridPersonnelRulesGroups_PersonnelRulesGroups();
}

function GridPersonnelRulesGroups_PersonnelRulesGroups_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridPersonnelRulesGroups_PersonnelRulesGroups').value = '';
}

function GridPersonnelRulesGroups_PersonnelRulesGroups_onItemSelect(sender, e) {
    if (CurrentPageState_PersonnelRulesGroups != 'Add')
        NavigatePersonnelRuleGroup_PersonnelRulesGroups(e.get_item());
}

function CallBack_GridPersonnelRulesGroups_PersonnelRulesGroups_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_PersonnelRulesGroups_PersonnelRulesGroups').value;
    if (error != "") {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        if (erroParts[3] == 'Reload')
            Refresh_GridPersonnelRulesGroups_PersonnelRulesGroups();
    }
}

function CallBackcmbRulesGroups_PersonnelRulesGroups_onCallbackError() {
    ShowConnectionError_hfErrorType_PersonnelRulesGroups();
}

function CallBack_GridPersonnelRulesGroups_PersonnelRulesGroups_onCallbackError(sender, e) {
    ShowConnectionError_hfErrorType_PersonnelRulesGroups();
}

function ShowConnectionError_hfErrorType_PersonnelRulesGroups() {
    var error = document.getElementById('hfErrorType_hfErrorType_PersonnelRulesGroups').value;
    var errorBody = document.getElementById('hfConnectionError_hfErrorType_PersonnelRulesGroups').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemHelp_TlbRulesGroup_onClick() {
    LoadHelpPage('tlbItemHelp_TlbRulesGroup');
}

function tlbItemFormReconstruction_TlbRulesGroup_onClick() {
    CloseDialogPersonnelRulesGroups();
    parent.parent.document.getElementById('DialogPersonnelMainInformation_IFrame').contentWindow.ShowDialogPersonnelRulesGroups();    
}

