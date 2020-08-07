
var CurrentPageState_PersonnelExtraInformationSettings = 'View';
var ConfirmState_PersonnelExtraInformationSettings = null;
var ObjPersonnelExtraInformationSettings_PersonnelExtraInformationSettings = null;
var ObjReserveFieldItem_PersonnelExtraInformationSettings = null;
var CurrentPageCombosCallBcakStateObj = new Object();

function GetBoxesHeaders_PersonnelExtraInformationSettings() {
    parent.document.getElementById('Title_DialogPersonnelExtraInformationSettings').innerHTML = document.getElementById('hfTitle_PersonnelExtraInformationSettings').value;
    document.getElementById('headerReserveFieldItems_PersonnelExtraInformationSettings').innerHTML = document.getElementById('hfheaderReserveFieldItems_PersonnelExtraInformationSettings').value;
    document.getElementById('headerItems_ReserveFieldItems_PersonnelExtraInformationSettings').innerHTML = document.getElementById('hfheaderItems_ReserveFieldItems_PersonnelExtraInformationSettings').value;
    document.getElementById('cmbReserveFields_PersonnelExtraInformationSettings').value = document.getElementById('hfcmbAlarm_DialogPersonnelExtraInformationSettings').value;
}

function tlbItemSave_TlbPersonnelExtraInformationSettings_onClick() {
    CurrentPageState_PersonnelExtraInformationSettings = 'EditPersonnelExtraInformationSettings';
    UpdatePersonnelExtraInformationSettings_PersonnelExtraInformationSettings();
}

function UpdatePersonnelExtraInformationSettings_PersonnelExtraInformationSettings() {
    ObjPersonnelExtraInformationSettings_PersonnelExtraInformationSettings = new Object();
    ObjPersonnelExtraInformationSettings_PersonnelExtraInformationSettings.ReserveFieldID = '0';
    ObjPersonnelExtraInformationSettings_PersonnelExtraInformationSettings.ReserveFieldName = null;

    var ObjDialogPersonnelExtraInformationSettings = parent.DialogPersonnelExtraInformationSettings.get_value();

    var SelectedItem_cmbReserveFields_PersonnelExtraInformationSettings = cmbReserveFields_PersonnelExtraInformationSettings.getSelectedItem();
    if (SelectedItem_cmbReserveFields_PersonnelExtraInformationSettings != undefined) {
        var ReserveFieldID = SelectedItem_cmbReserveFields_PersonnelExtraInformationSettings.get_id();
        ObjPersonnelExtraInformationSettings_PersonnelExtraInformationSettings.ReserveFieldID = ReserveFieldID;
    }
    ObjPersonnelExtraInformationSettings_PersonnelExtraInformationSettings.ReserveFieldName = document.getElementById('txtReserveFieldName_PersonnelExtraInformationSettings').value;

    UpdatePersonnelExtraInformationSetting_PersonnelExtraInformationSettingsPage(CharToKeyCode_PersonnelExtraInformationSettings(ObjDialogPersonnelExtraInformationSettings.PersonnelState), CharToKeyCode_PersonnelExtraInformationSettings(ObjPersonnelExtraInformationSettings_PersonnelExtraInformationSettings.ReserveFieldID), CharToKeyCode_PersonnelExtraInformationSettings(ObjPersonnelExtraInformationSettings_PersonnelExtraInformationSettings.ReserveFieldName));
    DialogWaiting.Show();
}

function UpdatePersonnelExtraInformationSetting_PersonnelExtraInformationSettingsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_PersonnelExtraInformationSettings').value;
            Response[1] = document.getElementById('hfConnectionError_PersonnelExtraInformationSettings').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_PersonnelExtraInformationSettings();
            PersonnelExtraInformationSetting_OnAfterUpdate(Response);
            ChangePageState_PersonnelExtraInformationSettings('View');
        }
    }
}

function PersonnelExtraInformationSetting_OnAfterUpdate() {
    if (ObjPersonnelExtraInformationSettings_PersonnelExtraInformationSettings != null) {
        var ReserveFieldID = ObjPersonnelExtraInformationSettings_PersonnelExtraInformationSettings.ReserveFieldID;
        var ReserveFieldName = ObjPersonnelExtraInformationSettings_PersonnelExtraInformationSettings.ReserveFieldName;

        ReserveFieldItem = cmbReserveFields_PersonnelExtraInformationSettings.findItemByProperty("Id", ReserveFieldID);
        if (ReserveFieldItem != undefined) {
            cmbReserveFields_PersonnelExtraInformationSettings.beginUpdate();
            ReserveFieldItem.set_text(ReserveFieldName);
            cmbReserveFields_PersonnelExtraInformationSettings.endUpdate();
        }
    }
}

function ClearList_PersonnelExtraInformationSettings() {
    if (CurrentPageState_PersonnelExtraInformationSettings == 'EditPersonnelExtraInformationSettings') {
        document.getElementById('txtReserveFieldName_PersonnelExtraInformationSettings').value = '';
        document.getElementById('cmbReserveFields_PersonnelExtraInformationSettings_Input').value = document.getElementById('hfcmbAlarm_DialogPersonnelExtraInformationSettings').value;
        cmbReserveFields_PersonnelExtraInformationSettings.unSelect();
        ChangeControlsEnabled_PersonnelExtraInformationSettings('TextValue', null);
    }
    if (CurrentPageState_PersonnelExtraInformationSettings == 'AddReserveFieldItem' || CurrentPageState_PersonnelExtraInformationSettings == 'EditReserveFieldItem' || CurrentPageState_PersonnelExtraInformationSettings == 'DeleteReserveFieldItem') {
        document.getElementById('txtItemName_PersonnelExtraInformationSettings').value = '';
        document.getElementById('txtlItemAlias_PersonnelExtraInformationSettings').value = '';
    }
}


function tlbItemHelp_TlbPersonnelExtraInformationSettings_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPersonnelExtraInformationSettings');
}

function tlbItemFormReconstruction_TlbPersonnelExtraInformationSettings_onClick() {
    parent.parent.document.getElementById('DialogPersonnelExtraInformation_IFrame').contentWindow.ShowDialogPersonnelExtraInformationSettings();
}

function CloseDialogPersonnelExtraInformationSettings() {
    parent.parent.document.getElementById('DialogPersonnelExtraInformation_IFrame').contentWindow.ReconstructForm_PersonnelExtraInformation();
    parent.document.getElementById('DialogPersonnelExtraInformationSettings_IFrame').src = 'WhitePage.aspx';
    parent.DialogPersonnelExtraInformationSettings.Close();
}

function tlbItemExit_TlbPersonnelExtraInformationSettings_onClick() {
    ShowDialogConfirm('Exit');
}

function cmbReserveFields_PersonnelExtraInformationSettings_onExpand(sender, e) {
    CollapseControls_PersonnelExtraInformationSettings(cmbReserveFields_PersonnelExtraInformationSettings);
    if (cmbReserveFields_PersonnelExtraInformationSettings.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbReserveFields_PersonnelExtraInformationSettings == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbReserveFields_PersonnelExtraInformationSettings = true;
        CallBack_cmbReserveFields_PersonnelExtraInformationSettings.callback();
    }
}

function cmbReserveFields_PersonnelExtraInformationSettings_onCollapse(sender, e) {
    if (cmbReserveFields_PersonnelExtraInformationSettings.getSelectedItem() == undefined)
        document.getElementById('cmbReserveFields_PersonnelExtraInformationSettings_Input').value = document.getElementById('hfcmbAlarm_DialogPersonnelExtraInformationSettings').value;
}

function CallBack_cmbReserveFields_PersonnelExtraInformationSettings_onBeforeCallback(sender, e) {
    cmbReserveFields_PersonnelExtraInformationSettings.dispose();
}

function CallBack_cmbReserveFields_PersonnelExtraInformationSettings_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ReserveFields_PersonnelExtraInformationSettings').value;
    if (error == "") {
        document.getElementById('cmbReserveFields_PersonnelExtraInformationSettings_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbReserveFields_PersonnelExtraInformationSettings_DropImage').mousedown();
        cmbReserveFields_PersonnelExtraInformationSettings.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbReserveFields_PersonnelExtraInformationSettings_DropDown').style.display = 'none';
    }
}

function CallBack_cmbReserveFields_PersonnelExtraInformationSettings_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelExtraInformationSettings();
}

function tlbItemNew_TlbReserveFieldsItems_onClick() {
    ChangePageState_PersonnelExtraInformationSettings('AddReserveFieldItem');
    ReserveFieldItem_onSave();
}

function tlbItemEdit_TlbReserveFieldsItems_onClick() {
    ChangePageState_PersonnelExtraInformationSettings('EditReserveFieldItem');
    ReserveFieldItem_onSave();
}

function tlbItemDelete_TlbReserveFieldsItems_onClick() {
    ChangePageState_PersonnelExtraInformationSettings('DeleteReserveFieldItem');
    ReserveFieldItem_onSave();
}

function ChangePageState_PersonnelExtraInformationSettings(state) {
    CurrentPageState_PersonnelExtraInformationSettings = state;
}

function ReserveFieldItem_onSave() {
    if (CurrentPageState_PersonnelExtraInformationSettings != 'DeleteReserveFieldItem')
        UpdateReserveFieldItem_PersonnelExtraInformationSettings();
    else
        ShowDialogConfirm('DeleteReserveFieldItem');
}

function UpdateReserveFieldItem_PersonnelExtraInformationSettings() {
    var ObjDialogPersonnelExtraInformationSettings = parent.DialogPersonnelExtraInformationSettings.get_value();    
    var SelectedItem_cmbReserveFields_PersonnelExtraInformationSettings = cmbReserveFields_PersonnelExtraInformationSettings.getSelectedItem();
    var ReserveFieldVal = GetReserveFieldVal_PersonnelExtraInformationSettings();
    ObjReserveFieldItem_PersonnelExtraInformationSettings.ControlType = ReserveFieldVal.ControlType;
    ObjReserveFieldItem_PersonnelExtraInformationSettings.ReserveFieldItemID = '0';
    var SelectedItems_GridReserveFieldItems_PersonnelExtraInformationSettings = GridReserveFieldItems_PersonnelExtraInformationSettings.getSelectedItems();
    if (SelectedItems_GridReserveFieldItems_PersonnelExtraInformationSettings.length > 0)
        ObjReserveFieldItem_PersonnelExtraInformationSettings.ReserveFieldItemID = SelectedItems_GridReserveFieldItems_PersonnelExtraInformationSettings[0].getMember('ID').get_text();
    switch (ObjReserveFieldItem_PersonnelExtraInformationSettings.ControlType) {
        case 'ComboValue':
            ObjReserveFieldItem_PersonnelExtraInformationSettings.ReserveFieldItemName = document.getElementById('txtItemName_PersonnelExtraInformationSettings').value;
            ObjReserveFieldItem_PersonnelExtraInformationSettings.ReserveFieldItemAlias = document.getElementById('txtlItemAlias_PersonnelExtraInformationSettings').value;

            UpdateReserveFieldItem_PersonnelExtraInformationSettingsPage(CharToKeyCode_PersonnelExtraInformationSettings(CurrentPageState_PersonnelExtraInformationSettings), CharToKeyCode_PersonnelExtraInformationSettings(ObjDialogPersonnelExtraInformationSettings.PersonnelState), CharToKeyCode_PersonnelExtraInformationSettings(ObjReserveFieldItem_PersonnelExtraInformationSettings.ControlType), CharToKeyCode_PersonnelExtraInformationSettings(ObjReserveFieldItem_PersonnelExtraInformationSettings.ReserveFieldID), CharToKeyCode_PersonnelExtraInformationSettings(ObjReserveFieldItem_PersonnelExtraInformationSettings.ReserveFieldItemID), CharToKeyCode_PersonnelExtraInformationSettings(ObjReserveFieldItem_PersonnelExtraInformationSettings.ReserveFieldItemName), CharToKeyCode_PersonnelExtraInformationSettings(ObjReserveFieldItem_PersonnelExtraInformationSettings.ReserveFieldItemAlias));
            DialogWaiting.Show();
            break;
    }
}

function UpdateReserveFieldItem_PersonnelExtraInformationSettingsPage_onCallback(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_PersonnelExtraInformationSettings').value;
            Response[1] = document.getElementById('hfConnectionError_PersonnelExtraInformationSettings').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_PersonnelExtraInformationSettings();
            ReserveFieldItem_onAfterUpdate(Response);
            ChangePageState_PersonnelExtraInformationSettings('View');
        }
    }
}

function ReserveFieldItem_onAfterUpdate(Response) {
    if (ObjReserveFieldItem_PersonnelExtraInformationSettings != null) {
        var ControlType = ObjReserveFieldItem_PersonnelExtraInformationSettings.ControlType;
        switch (ControlType) {
            case 'ComboValue':
                Refresh_GridReserveFieldItems_PersonnelExtraInformationSettings();
                break;
        }
    }
}

function Refresh_GridReserveFieldItems_PersonnelExtraInformationSettings() {
    Fill_GridReserveFieldItems_PersonnelExtraInformationSettings();
}

function Fill_GridReserveFieldItems_PersonnelExtraInformationSettings() {
    var selectedItem_cmbReserveFields_PersonnelExtraInformationSettings = cmbReserveFields_PersonnelExtraInformationSettings.getSelectedItem();
    if (selectedItem_cmbReserveFields_PersonnelExtraInformationSettings != undefined) {
        document.getElementById('loadingPanel_ReserveFieldItems_PersonnelExtraInformationSettings').innerHTML = document.getElementById('hfloadingPanel_ReserveFieldItems_PersonnelExtraInformationSettings').value;
        var ReserveFieldID = selectedItem_cmbReserveFields_PersonnelExtraInformationSettings.get_id();
        CallBack_GridReserveFieldItems_PersonnelExtraInformationSettings.callback(CharToKeyCode_PersonnelExtraInformationSettings(ReserveFieldID));
    }
}

function GetReserveFieldVal_PersonnelExtraInformationSettings() {
    var selectedItem_cmbReserveFields_PersonnelExtraInformationSettings = cmbReserveFields_PersonnelExtraInformationSettings.getSelectedItem();
    var ReserveFieldVal = selectedItem_cmbReserveFields_PersonnelExtraInformationSettings.get_value();
    ReserveFieldVal = eval('(' + ReserveFieldVal + ')');
    return ReserveFieldVal;
}

function GridReserveFieldItems_PersonnelExtraInformationSettings_onItemSelect(sender, e) {
    if (CurrentPageState_PersonnelExtraInformationSettings != 'Add')
        NavigateReserveFieldItem_PersonnelExtraInformationSettings(e.get_item());
}

function NavigateReserveFieldItem_PersonnelExtraInformationSettings(selectedReserveFieldItem) {
    if (selectedReserveFieldItem != undefined) {
        document.getElementById('txtItemName_PersonnelExtraInformationSettings').value = selectedReserveFieldItem.getMember('ComboText').get_text();
        document.getElementById('txtlItemAlias_PersonnelExtraInformationSettings').value = selectedReserveFieldItem.getMember('ComboValue').get_text();
    }
}

function GridReserveFieldItems_PersonnelExtraInformationSettings_onLoad(sender, e) {
    document.getElementById('loadingPanel_ReserveFieldItems_PersonnelExtraInformationSettings').innerHTML = '';
}

function CallBack_GridReserveFieldItems_PersonnelExtraInformationSettings_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ReserveFieldItems_PersonnelExtraInformationSettings').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridReserveFieldItems_PersonnelExtraInformationSettings();
    }
}

function CallBack_GridReserveFieldItems_PersonnelExtraInformationSettings_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelExtraInformationSettings();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_PersonnelExtraInformationSettings) {
        case 'DeleteReserveFieldItem':
            DialogConfirm.Close();
            UpdateReserveFieldItem_PersonnelExtraInformationSettings();
            break;
        case 'Exit':
            ClearList_PersonnelExtraInformationSettings();
            CloseDialogPersonnelExtraInformationSettings();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_PersonnelExtraInformationSettings('View');
}

function CollapseControls_PersonnelExtraInformationSettings(exception) {
    if (exception == null || exception != cmbReserveFields_PersonnelExtraInformationSettings)
        cmbReserveFields_PersonnelExtraInformationSettings.collapse();
}

function ShowConnectionError_PersonnelExtraInformationSettings() {
    var error = document.getElementById('hfErrorType_PersonnelExtraInformationSettings').value;
    var errorBody = document.getElementById('hfConnectionError_PersonnelExtraInformationSettings').value;
    showDialog(error, errorBody, 'error');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_PersonnelExtraInformationSettings = confirmState;
    switch (confirmState) {
        case 'DeleteReserveFieldItem':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_PersonnelExtraInformationSettings').value;
            break;
        case 'Exit':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PersonnelExtraInformationSettings').value;
            break;
    }
    DialogConfirm.Show();
    CollapseControls_PersonnelExtraInformationSettings();
}

function cmbReserveFields_PersonnelExtraInformationSettings_onChange(sender, e) {
    if (cmbReserveFields_PersonnelExtraInformationSettings.getSelectedItem() != undefined) {
        document.getElementById('txtReserveFieldName_PersonnelExtraInformationSettings').value = cmbReserveFields_PersonnelExtraInformationSettings.getSelectedItem().get_text();
        var ReserveFieldVal = GetReserveFieldVal_PersonnelExtraInformationSettings();
        var ControlType = ReserveFieldVal.ControlType;
        ChangeControlsEnabled_PersonnelExtraInformationSettings(ControlType, 'onIndexChange');
        ClearItems_GridReserveFieldItems_PersonnelExtraInformationSettings();
    }
}

function ClearItems_GridReserveFieldItems_PersonnelExtraInformationSettings() {
    GridReserveFieldItems_PersonnelExtraInformationSettings.beginUpdate();
    GridReserveFieldItems_PersonnelExtraInformationSettings.get_table().clearData();
    GridReserveFieldItems_PersonnelExtraInformationSettings.endUpdate();
}

function tlbItemItemsEdit_TlbPersonnelExtraInformationSettings_onClick() {
    var ReserveFieldVal = GetReserveFieldVal_PersonnelExtraInformationSettings();
    var ControlType = ReserveFieldVal.ControlType;
    ChangeControlsEnabled_PersonnelExtraInformationSettings(ControlType, 'onClick');
    SetCruentReserveFieldID_PersonnelExtraInformationSettings();
    SetItemsHeader_GridReserveFieldItems_PersonnelExtraInformationSettings();
    Fill_GridReserveFieldItems_PersonnelExtraInformationSettings();
}

function SetCruentReserveFieldID_PersonnelExtraInformationSettings() {
    var SelectedItem_cmbReserveFields_PersonnelExtraInformationSettings = cmbReserveFields_PersonnelExtraInformationSettings.getSelectedItem();
    var ReserveFieldID = SelectedItem_cmbReserveFields_PersonnelExtraInformationSettings.get_id();
    ObjReserveFieldItem_PersonnelExtraInformationSettings = new Object();
    ObjReserveFieldItem_PersonnelExtraInformationSettings.ReserveFieldID = ReserveFieldID;
}

function SetItemsHeader_GridReserveFieldItems_PersonnelExtraInformationSettings() {
    var SelectedItem_cmbReserveFields_PersonnelExtraInformationSettings = cmbReserveFields_PersonnelExtraInformationSettings.getSelectedItem();
    if (SelectedItem_cmbReserveFields_PersonnelExtraInformationSettings != undefined) {
        var ReserveFieldName = SelectedItem_cmbReserveFields_PersonnelExtraInformationSettings.get_text();
        var BaseItemsHeader_GridReserveFieldItems_PersonnelExtraInformationSettings = document.getElementById('hfheaderItems_ReserveFieldItems_PersonnelExtraInformationSettings').value;
        var headerItems_ReserveFieldItems_PersonnelExtraInformationSettings = null;
        switch (parent.parent.parent.CurrentLangID) {
            case 'fa-IR':
                headerItems_ReserveFieldItems_PersonnelExtraInformationSettings = BaseItemsHeader_GridReserveFieldItems_PersonnelExtraInformationSettings + ' ' + ReserveFieldName;
                break;
            case 'en-US':
                headerItems_ReserveFieldItems_PersonnelExtraInformationSettings = ReserveFieldName + ' ' + BaseItemsHeader_GridReserveFieldItems_PersonnelExtraInformationSettings;
                break;
        }
    }
}

function ChangeItemsDivesVisible_PersonnelExtraInformationSettings(visible) {
    if (cmbReserveFields_PersonnelExtraInformationSettings.detSelectedItem() != undefined) {
        var visibility = null;
        if (visibility)
            visibility = 'Visible';
        else
            visibility = 'Hidden';

        var ReserveFieldVal = GetReserveFieldVal_PersonnelExtraInformationSettings();
        var ControlType = ReserveFieldVal.ControlType;
        switch (ControlType) {
            case 'ComboValue':
                document.getElementById('ContainerReserveFieldItems_PersonnelExtraInformationSettings').style.visibility = visibility;
                break;
        }
    }
}

function ChangeControlsEnabled_PersonnelExtraInformationSettings(ControlType, state) {
    switch (ControlType) {
        case 'ComboValue':
            switch (state) {
                case 'onIndexChange':
                    if (TlbPersonnelExtraInformationSettings.get_items().getItemById('tlbItemItemsEdit_TlbPersonnelExtraInformationSettings') != null) {
                        TlbPersonnelExtraInformationSettings.get_items().getItemById('tlbItemItemsEdit_TlbPersonnelExtraInformationSettings').set_enabled(true);
                        TlbPersonnelExtraInformationSettings.get_items().getItemById('tlbItemItemsEdit_TlbPersonnelExtraInformationSettings').set_imageUrl('edit.png');
                    }
                    break;
                case 'onClick':
                    if (TlbReserveFieldsItems.get_items().getItemById('tlbItemNew_TlbReserveFieldsItems') != null) {
                        TlbReserveFieldsItems.get_items().getItemById('tlbItemNew_TlbReserveFieldsItems').set_enabled(true);
                        TlbReserveFieldsItems.get_items().getItemById('tlbItemNew_TlbReserveFieldsItems').set_imageUrl('add.png');
                    }
                    if (TlbReserveFieldsItems.get_items().getItemById('tlbItemEdit_TlbReserveFieldsItems') != null) {
                        TlbReserveFieldsItems.get_items().getItemById('tlbItemEdit_TlbReserveFieldsItems').set_enabled(true);
                        TlbReserveFieldsItems.get_items().getItemById('tlbItemEdit_TlbReserveFieldsItems').set_imageUrl('edit.png');
                    }
                    if (TlbReserveFieldsItems.get_items().getItemById('tlbItemDelete_TlbReserveFieldsItems') != null) {
                        TlbReserveFieldsItems.get_items().getItemById('tlbItemDelete_TlbReserveFieldsItems').set_enabled(true);
                        TlbReserveFieldsItems.get_items().getItemById('tlbItemDelete_TlbReserveFieldsItems').set_imageUrl('remove.png');
                    }
                    if (TlbRefresh_GridReserveFieldItems_PersonnelExtraInformationSettings.get_items().getItemById('tlbItemRefresh_TlbRefresh_GridReserveFieldItems_PersonnelExtraInformationSettings') != null) {
                        TlbRefresh_GridReserveFieldItems_PersonnelExtraInformationSettings.get_items().getItemById('tlbItemRefresh_TlbRefresh_GridReserveFieldItems_PersonnelExtraInformationSettings').set_enabled(true);
                        TlbRefresh_GridReserveFieldItems_PersonnelExtraInformationSettings.get_items().getItemById('tlbItemRefresh_TlbRefresh_GridReserveFieldItems_PersonnelExtraInformationSettings').set_imageUrl('refresh.png');
                    }
                    document.getElementById('txtItemName_PersonnelExtraInformationSettings').disabled = '';
                    document.getElementById('txtlItemAlias_PersonnelExtraInformationSettings').disabled = '';
                    break;
            }
            break;
        case 'TextValue':
            if (TlbPersonnelExtraInformationSettings.get_items().getItemById('tlbItemItemsEdit_TlbPersonnelExtraInformationSettings') != null) {
                TlbPersonnelExtraInformationSettings.get_items().getItemById('tlbItemItemsEdit_TlbPersonnelExtraInformationSettings').set_enabled(false);
                TlbPersonnelExtraInformationSettings.get_items().getItemById('tlbItemItemsEdit_TlbPersonnelExtraInformationSettings').set_imageUrl('edit_silver.png');
            }
            if (TlbReserveFieldsItems.get_items().getItemById('tlbItemNew_TlbReserveFieldsItems') != null) {
                TlbReserveFieldsItems.get_items().getItemById('tlbItemNew_TlbReserveFieldsItems').set_enabled(false);
                TlbReserveFieldsItems.get_items().getItemById('tlbItemNew_TlbReserveFieldsItems').set_imageUrl('add_silver.png');
            }
            if (TlbReserveFieldsItems.get_items().getItemById('tlbItemEdit_TlbReserveFieldsItems') != null) {
                TlbReserveFieldsItems.get_items().getItemById('tlbItemEdit_TlbReserveFieldsItems').set_enabled(false);
                TlbReserveFieldsItems.get_items().getItemById('tlbItemEdit_TlbReserveFieldsItems').set_imageUrl('edit_silver.png');
            }
            if (TlbReserveFieldsItems.get_items().getItemById('tlbItemDelete_TlbReserveFieldsItems') != null) {
                TlbReserveFieldsItems.get_items().getItemById('tlbItemDelete_TlbReserveFieldsItems').set_enabled(false);
                TlbReserveFieldsItems.get_items().getItemById('tlbItemDelete_TlbReserveFieldsItems').set_imageUrl('remove_silver.png');
            }
            if (TlbRefresh_GridReserveFieldItems_PersonnelExtraInformationSettings.get_items().getItemById('tlbItemRefresh_TlbRefresh_GridReserveFieldItems_PersonnelExtraInformationSettings') != null) {
                TlbRefresh_GridReserveFieldItems_PersonnelExtraInformationSettings.get_items().getItemById('tlbItemRefresh_TlbRefresh_GridReserveFieldItems_PersonnelExtraInformationSettings').set_enabled(false);
                TlbRefresh_GridReserveFieldItems_PersonnelExtraInformationSettings.get_items().getItemById('tlbItemRefresh_TlbRefresh_GridReserveFieldItems_PersonnelExtraInformationSettings').set_imageUrl('refresh_silver.png');
            }
            document.getElementById('txtItemName_PersonnelExtraInformationSettings').disabled = 'disabled';
            document.getElementById('txtlItemAlias_PersonnelExtraInformationSettings').disabled = 'disabled';
            break;
    }
}

function CharToKeyCode_PersonnelExtraInformationSettings(str) {
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

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}





