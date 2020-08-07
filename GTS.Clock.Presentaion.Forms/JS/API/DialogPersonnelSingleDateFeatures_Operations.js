
var CurrentPageState_PersonnelSingleDateFeatures = 'View';
var SelectedSingleDateFeature_PersonnelSingleDateFeatures = null;
var ConfirmState_PersonnelSingleDateFeatures = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures = null;

function ChangePageState_PersonnelSingleDateFeatures(state) {
    SetActionMode_PersonnelSingleDateFeatures(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        CurrentPageState_PersonnelSingleDateFeatures = state;
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemNew_TlbPersonnelSingleDateFeatures').set_enabled(false);
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemNew_TlbPersonnelSingleDateFeatures').set_imageUrl('add_silver.png');
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemEdit_TlbPersonnelSingleDateFeatures').set_enabled(false);
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemEdit_TlbPersonnelSingleDateFeatures').set_imageUrl('edit_silver.png');
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemDelete_TlbPersonnelSingleDateFeatures').set_enabled(false);
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemDelete_TlbPersonnelSingleDateFeatures').set_imageUrl('remove_silver.png');
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemSave_TlbPersonnelSingleDateFeatures').set_enabled(true);
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemSave_TlbPersonnelSingleDateFeatures').set_imageUrl('save.png');
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemCancel_TlbPersonnelSingleDateFeatures').set_enabled(true);
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemCancel_TlbPersonnelSingleDateFeatures').set_imageUrl('cancel.png');
        cmbSingleDateFeatures_PersonnelSingleDateFeatures.enable();
        ChangeCalendarsEnabled_PersonnelSingleDateFeatures('enable');
        if (state == 'Edit')
            NavigatePersonnelSingleDateFeature_PersonnelSingleDateFeatures(GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.getSelectedItems()[0]);
        if (state == 'Delete')
            PersonnelSingleDateFeature_onSave();
    }
    if (state == 'View') {
        CurrentPageState_PersonnelSingleDateFeatures = state;
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemNew_TlbPersonnelSingleDateFeatures').set_enabled(true);
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemNew_TlbPersonnelSingleDateFeatures').set_imageUrl('add.png');
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemEdit_TlbPersonnelSingleDateFeatures').set_enabled(true);
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemEdit_TlbPersonnelSingleDateFeatures').set_imageUrl('edit.png');
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemDelete_TlbPersonnelSingleDateFeatures').set_enabled(true);
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemDelete_TlbPersonnelSingleDateFeatures').set_imageUrl('remove.png');
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemSave_TlbPersonnelSingleDateFeatures').set_enabled(false);
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemSave_TlbPersonnelSingleDateFeatures').set_imageUrl('save_silver.png');
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemCancel_TlbPersonnelSingleDateFeatures').set_enabled(false);
        TlbPersonnelSingleDateFeatures.get_items().getItemById('tlbItemCancel_TlbPersonnelSingleDateFeatures').set_imageUrl('cancel_silver.png');
        ChangeCalendarsEnabled_PersonnelSingleDateFeatures('disable');
        cmbSingleDateFeatures_PersonnelSingleDateFeatures.disable();
    }
}

function PersonnelSingleDateFeature_onSave(){
    if (CurrentPageState_PersonnelSingleDateFeatures != 'Delete')
        UpdatePersonnelSingleDateFeature_PersonnelSingleDateFeatures();
    else
        ShowDialogConfirm('Delete');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_PersonnelSingleDateFeatures = confirmState;
    if (CurrentPageState_PersonnelSingleDateFeatures == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_PersonnelSingleDateFeatures').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PersonnelSingleDateFeatures').value;
    DialogConfirm.Show();
}

function Fill_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures() {
    document.getElementById('header_PersonnelSingleDateFeatures_PersonnelSingleDateFeatures').innerHTML = document.getElementById('hfheader_PersonnelSingleDateFeatures_PersonnelSingleDateFeatures').value;
    var Caller = GetCaller_PersonnelSingleDateFeatures();
    var PersonnelID = parent.parent.DialogPersonnelMainInformation.get_value().PersonnelID;
    CallBack_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.callback(CharToKeyCode_PersonnelSingleDateFeatures(Caller), CharToKeyCode_PersonnelSingleDateFeatures(PersonnelID));
}

function GetCaller_PersonnelSingleDateFeatures() {
    var ObjDialogPersonnelSingleDateFeatures = parent.DialogPersonnelSingleDateFeatures.get_value();
    var Caller = ObjDialogPersonnelSingleDateFeatures.Caller;
    return Caller;
}

function NavigatePersonnelSingleDateFeature_PersonnelSingleDateFeatures(selectedPersonnelSingleDateFeature) {
    if (selectedPersonnelSingleDateFeature != undefined) {
        var Caller = GetCaller_PersonnelSingleDateFeatures();
        var PersonnelSingleDateFeatureIDDataField = null;
        var PersonnelSingleDateFeatureNameDataField = null;
        switch (Caller) {
            case 'WorkGroups':
                PersonnelSingleDateFeatureIDDataField = 'WorkGroup.ID';
                PersonnelSingleDateFeatureNameDataField = 'WorkGroup.Name';
                break;
            case 'CalculationRangesGroups':
                PersonnelSingleDateFeatureIDDataField = 'CalcDateRangeGroup.ID';
                PersonnelSingleDateFeatureNameDataField = 'CalcDateRangeGroup.Name';
                break;
        }
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpFromDate_PersonnelSingleDateFeatures').value = selectedPersonnelSingleDateFeature.getMember('UIFromDate').get_text();
                break;
            case 'en-US':
                var gFromDate = new Date(selectedPersonnelSingleDateFeature.getMember('UIFromDate').get_text());
                gdpFromDate_PersonnelSingleDateFeatures.setSelectedDate(gFromDate);
                gCalFromDate_PersonnelSingleDateFeatures.setSelectedDate(gFromDate);
                break;
        }
        SelectedSingleDateFeature_PersonnelSingleDateFeatures = new Object();
        SelectedSingleDateFeature_PersonnelSingleDateFeatures.ID = selectedPersonnelSingleDateFeature.getMember(PersonnelSingleDateFeatureIDDataField).get_text();
        document.getElementById('cmbSingleDateFeatures_PersonnelSingleDateFeatures_Input').value = SelectedSingleDateFeature_PersonnelSingleDateFeatures.Name = selectedPersonnelSingleDateFeature.getMember(PersonnelSingleDateFeatureNameDataField).get_text();
    }
}

function UpdatePersonnelSingleDateFeature_PersonnelSingleDateFeatures() {
    ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures = new Object();
    ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.Caller = GetCaller_PersonnelSingleDateFeatures();
    ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.ID = '0';
    ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.SingleDateFeatureID = '0';
    ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.SingleDateFeatureName = null;
    ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.PersonnelID = '0';
    ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.FromDate = null;

    var SelectedItems_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures = GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.getSelectedItems();
    if (SelectedItems_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.length > 0)
        ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.ID = SelectedItems_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures[0].getMember("ID").get_text();

    ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.PersonnelID = parent.parent.DialogPersonnelMainInformation.get_value().PersonnelID;
    if (CurrentPageState_PersonnelSingleDateFeatures != 'Delete') {
        if (cmbSingleDateFeatures_PersonnelSingleDateFeatures.getSelectedItem() != undefined) {
            ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.SingleDateFeatureID = cmbSingleDateFeatures_PersonnelSingleDateFeatures.getSelectedItem().get_value();
            ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.SingleDateFeatureName = cmbSingleDateFeatures_PersonnelSingleDateFeatures.getSelectedItem().get_text();
        }
        else {
            if (SelectedSingleDateFeature_PersonnelSingleDateFeatures != null) {
                ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.SingleDateFeatureID = SelectedSingleDateFeature_PersonnelSingleDateFeatures.ID;
                ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.SingleDateFeatureName = SelectedSingleDateFeature_PersonnelSingleDateFeatures.Name;
            }
        }
        var FromDate_PersonnelSingleDateFeatures = null;
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                FromDate_PersonnelSingleDateFeatures = document.getElementById('pdpFromDate_PersonnelSingleDateFeatures').value;
                break;
            case 'en-US':
                FromDate_PersonnelSingleDateFeatures = document.getElementById('gdpFromDate_PersonnelSingleDateFeatures_picker').value;
                break;
        }
        ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.FromDate = FromDate_PersonnelSingleDateFeatures;    
    }
    UpdatePersonnelSingleDateFeature_PersonnelSingleDateFeaturesPage(CharToKeyCode_PersonnelSingleDateFeatures(ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.Caller), CharToKeyCode_PersonnelSingleDateFeatures(CurrentPageState_PersonnelSingleDateFeatures), CharToKeyCode_PersonnelSingleDateFeatures(ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.ID), CharToKeyCode_PersonnelSingleDateFeatures(ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.PersonnelID), CharToKeyCode_PersonnelSingleDateFeatures(ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.SingleDateFeatureID), CharToKeyCode_PersonnelSingleDateFeatures(ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.SingleDateFeatureName), CharToKeyCode_PersonnelSingleDateFeatures(ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.FromDate));
    DialogWaiting.Show();
}

function UpdatePersonnelSingleDateFeature_PersonnelSingleDateFeaturesPage_onCallBack(Response) {
    var RetMessage = Response;
    var Caller = GetCaller_PersonnelSingleDateFeatures();
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_PersonnelSingleDateFeatures();
            PersonnelSingleDateFeature_OnAfterUpdate(Response);
            ChangePageState_PersonnelSingleDateFeatures('View');
            parent.parent.document.getElementById('DialogPersonnelMainInformation_IFrame').contentWindow.ChangePersonnel_onPersonnelSingleDateFeaturesOperationCompleted(Caller, Response[4]);
        }
        else {
            if (CurrentPageState_PersonnelSingleDateFeatures == 'Delete')
                ChangePageState_PersonnelSingleDateFeatures('View');
        }
    }
}

function PersonnelSingleDateFeature_OnAfterUpdate(Response) {
    if (ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures != null) {
        var PersonnelID = ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.PersonnelID;
        var PersonnelSingleDateFeatureID = ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.SingleDateFeatureID;
        var PersonnelSingleDateFeatureName = ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.SingleDateFeatureName;
        var FromDate = ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.FromDate;

        var PersonnelSingleDateFeatureItem = null;
        GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.beginUpdate();
        switch (CurrentPageState_PersonnelSingleDateFeatures) {
            case 'Add':
                PersonnelSingleDateFeatureItem = GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.get_table().addEmptyRow(GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.get_recordCount());
                PersonnelSingleDateFeatureItem.setValue(0, Response[3], false);
                GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.selectByKey(Response[3], 0, false);
                PersonnelSingleDateFeatureItem = GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.selectByKey(ObjPersonnelSingleDateFeature_PersonnelSingleDateFeatures.ID, 0, false);
                GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.deleteSelected();
                break;
        }
        if (CurrentPageState_PersonnelSingleDateFeatures != 'Delete') {
            PersonnelSingleDateFeatureItem.setValue(1, PersonnelSingleDateFeatureID, false);
            PersonnelSingleDateFeatureItem.setValue(2, PersonnelSingleDateFeatureName, false);
            PersonnelSingleDateFeatureItem.setValue(3, FromDate, false);
        }
        GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.endUpdate();
    } 
}

function CharToKeyCode_PersonnelSingleDateFeatures(str) {
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

function tlbItemNew_TlbPersonnelSingleDateFeatures_onClick() {
    ChangePageState_PersonnelSingleDateFeatures('Add');
    ClearList_PersonnelSingleDateFeatures();
}

function tlbItemEdit_TlbPersonnelSingleDateFeatures_onClick() {
    ChangePageState_PersonnelSingleDateFeatures('Edit');
}

function tlbItemDelete_TlbPersonnelSingleDateFeatures_onClick() {
    ChangePageState_PersonnelSingleDateFeatures('Delete');
}

function tlbItemSave_TlbPersonnelSingleDateFeatures_onClick() {
     PersonnelSingleDateFeature_onSave();
}

function tlbItemCancel_TlbPersonnelSingleDateFeatures_onClick() {
    ChangePageState_PersonnelSingleDateFeatures('View');
    ClearList_PersonnelSingleDateFeatures();
}

function ClearList_PersonnelSingleDateFeatures(){
    if (CurrentPageState_PersonnelSingleDateFeatures != 'Edit') {
        document.getElementById('cmbSingleDateFeatures_PersonnelSingleDateFeatures_Input').value = document.getElementById('hfcmbAlarm_PersonnelSingleDateFeatures').value;
        cmbSingleDateFeatures_PersonnelSingleDateFeatures.unSelect();
        SelectedSingleDateFeature_PersonnelSingleDateFeatures = null;
        ResetCalendars_PersonnelSingleDateFeatures();
        GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.unSelectAll();
    }
}

function tlbItemExit_TlbPersonnelSingleDateFeatures_onClick() {
    ShowDialogConfirm('Exit');
}

function Refresh_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures() {
    Fill_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures();
}

function GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures').innerHTML = '';  
}

function GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures_onItemSelect(sender, e) {
    if (CurrentPageState_PersonnelSingleDateFeatures != 'Add')
        NavigatePersonnelSingleDateFeature_PersonnelSingleDateFeatures(e.get_item());
}

function CallBack_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_PersonnelSingleDateFeatures').value;
    if (error != "") {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        if (erroParts[3] == 'Reload')
            Refresh_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures();
    }
    
}

function cmbSingleDateFeatures_PersonnelSingleDateFeatures_onExpand(sender, e) {
    var Caller = GetCaller_PersonnelSingleDateFeatures();
    if (cmbSingleDateFeatures_PersonnelSingleDateFeatures.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSingleDateFeatures_PersonnelSingleDateFeatures == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSingleDateFeatures_PersonnelSingleDateFeatures = true;
        CallBackcmbSingleDateFeatures_PersonnelSingleDateFeatures.callback(CharToKeyCode_PersonnelSingleDateFeatures(Caller));
    }
    if (cmbSingleDateFeatures_PersonnelSingleDateFeatures.getSelectedItem() == undefined && SelectedSingleDateFeature_PersonnelSingleDateFeatures != null)
        document.getElementById('cmbSingleDateFeatures_PersonnelSingleDateFeatures_Input').value = SelectedSingleDateFeature_PersonnelSingleDateFeatures.Name;
}

function CallBackcmbSingleDateFeatures_PersonnelSingleDateFeatures_onBeforeCallback(sender, e) {
    cmbSingleDateFeatures_PersonnelSingleDateFeatures.dispose();
}

function CallBackcmbSingleDateFeatures_PersonnelSingleDateFeatures_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_PersonnelSingleDateFeature_PersonnelSingleDateFeatures').value;
    if (error == "") {
        document.getElementById('cmbSingleDateFeatures_PersonnelSingleDateFeatures_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbSingleDateFeatures_PersonnelSingleDateFeatures_DropImage').mousedown();
        cmbSingleDateFeatures_PersonnelSingleDateFeatures.expand();
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


function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_PersonnelSingleDateFeatures) {
        case 'Delete':
            DialogConfirm.Close();
            UpdatePersonnelSingleDateFeature_PersonnelSingleDateFeatures();
            break;
        case 'Exit':
            ClearList_PersonnelSingleDateFeatures();
            CloseDialogPersonnelSingleDateFeatures();
            break;
    }
}

function CloseDialogPersonnelSingleDateFeatures() {
    parent.document.getElementById('DialogPersonnelSingleDateFeatures_IFrame').src = 'WhitePage.aspx';
    parent.DialogPersonnelSingleDateFeatures.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_PersonnelSingleDateFeatures('View');
}

function btn_gdpFromDate_PersonnelSingleDateFeatures_OnMouseUp(event) {
    if (gCalFromDate_PersonnelSingleDateFeatures.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpFromDate_PersonnelSingleDateFeatures_OnDateChange(sender, e) {
    var FromDate = gdpFromDate_PersonnelSingleDateFeatures.getSelectedDate();
    gCalFromDate_PersonnelSingleDateFeatures.setSelectedDate(FromDate);
}

function btn_gdpFromDate_PersonnelSingleDateFeatures_OnClick(event) {
    if (gCalFromDate_PersonnelSingleDateFeatures.get_popUpShowing()) {
        gCalFromDate_PersonnelSingleDateFeatures.hide();
    }
    else {
        gCalFromDate_PersonnelSingleDateFeatures.setSelectedDate(gdpFromDate_PersonnelSingleDateFeatures.getSelectedDate());
        gCalFromDate_PersonnelSingleDateFeatures.show();
    }
}

function gCalFromDate_PersonnelSingleDateFeatures_OnChange(sender, e) {
    var FromDate = gCalFromDate_PersonnelSingleDateFeatures.getSelectedDate();
    gdpFromDate_PersonnelSingleDateFeatures.setSelectedDate(FromDate);
}

function gCalFromDate_PersonnelSingleDateFeatures_onLoad(sender, e) {
    window.gCalFromDate_PersonnelSingleDateFeatures.PopUpObject.z = 25000000;
}

function GetBoxesHeaders_PersonnelSingleDateFeatures() {
    parent.document.getElementById('Title_DialogPersonnelSingleDateFeatures').innerHTML = document.getElementById('hfTitle_DialogPersonnelSingleDateFeatures').value;
    document.getElementById('header_PersonnelSingleDateFeatures_PersonnelSingleDateFeatures').innerHTML = document.getElementById('hfheader_PersonnelSingleDateFeatures_PersonnelSingleDateFeatures').value;
}

function ViewCurrentLangCalendars_PersonnelSingleDateFeatures() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpFromDate_PersonnelSingleDateFeatures").parentNode.removeChild(document.getElementById("pdpFromDate_PersonnelSingleDateFeatures"));
            document.getElementById("pdpFromDate_PersonnelSingleDateFeaturesimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_PersonnelSingleDateFeaturesimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_FromDateCalendars_PersonnelSingleDateFeatures").removeChild(document.getElementById("Container_gCalFromDate_PersonnelSingleDateFeatures"));
            break;
    }
}

function ResetCalendars_PersonnelSingleDateFeatures() {
    var currentDate_PersonnelSingleDateFeatures = document.getElementById('hfCurrentDate_PersonnelSingleDateFeatures').value;
    switch (parent.parent.SysLangID) {
        case 'en-US':
            currentDate_PersonnelSingleDateFeatures = new Date(currentDate_PersonnelSingleDateFeatures);
            gdpFromDate_PersonnelSingleDateFeatures.setSelectedDate(currentDate_PersonnelSingleDateFeatures);
            gCalFromDate_PersonnelSingleDateFeatures.setSelectedDate(currentDate_PersonnelSingleDateFeatures);
            break;
        case 'fa-IR':
            document.getElementById('pdpFromDate_PersonnelSingleDateFeatures').value = currentDate_PersonnelSingleDateFeatures;
            break;
    }
}

function SetActionMode_PersonnelSingleDateFeatures(state) {
    document.getElementById('ActionMode_PersonnelSingleDateFeatures').innerHTML = document.getElementById('hf' + state + '_PersonnelSingleDateFeatures').value;
}

function ChangeCalendarsEnabled_PersonnelSingleDateFeatures(state) {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ChangeCalendarEnabled_PersonnelSingleDateFeatures('pdpFromDate_PersonnelSingleDateFeatures', state);
            break;
        case 'en-US':
            ChangeCalendarEnabled_PersonnelSingleDateFeatures('gdpFromDate_PersonnelSingleDateFeatures', state);
            break;
    }
}

function ChangeCalendarEnabled_PersonnelSingleDateFeatures(cal, state) {
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
                        CalendarsViewManage_PersonnelSingleDateFeatures(cal);
                    };
                    break;
            }
            break;
    }
}

function CalendarsViewManage_PersonnelSingleDateFeatures(gdpID) {
    var CalID_PersonnelSingleDateFeatures = 'gCal' + gdpID.substr(3, gdpID.length - 3);
    var Cal_PersonnelSingleDateFeatures = eval(CalID_PersonnelSingleDateFeatures);
    if (!Cal_PersonnelSingleDateFeatures.get_popUpShowing())
        Cal_PersonnelSingleDateFeatures.show();
    else
        Cal_PersonnelSingleDateFeatures.hide();
}

function CallBackcmbSingleDateFeatures_PersonnelSingleDateFeatures_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSingleDateFeatures();
}

function CallBack_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSingleDateFeatures();
}

function ShowConnectionError_PersonnelSingleDateFeatures() {
    var error = document.getElementById('hfErrorType_PersonnelSingleDateFeatures').value;
    var errorBody = document.getElementById('hfConnectionError_PersonnelSingleDateFeatures').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemHelp_TlbPersonnelSingleDateFeatures_onClick() {
    var Caller = GetCaller_PersonnelSingleDateFeatures();
    var tlbItemHelp_TlbPersonnelSingleDateFeatures = null;
    switch (Caller) {
       case 'WorkGroups':
          tlbItemHelp_TlbPersonnelSingleDateFeatures = 'tlbItemHelp_TlbPersonnelWorkGroups';
          break;
       case 'CalculationRangesGroups':
          tlbItemHelp_TlbPersonnelSingleDateFeatures = 'tlbItemHelp_TlbPersonnelCalculationRangesGroups';
          break;    
    }
    LoadHelpPage(tlbItemHelp_TlbPersonnelSingleDateFeatures);
}

function tlbItemFormReconstruction_TlbPersonnelSingleDateFeatures_onClick() {
    var Caller = GetCaller_PersonnelSingleDateFeatures();
    CloseDialogPersonnelSingleDateFeatures();
    parent.parent.document.getElementById('DialogPersonnelMainInformation_IFrame').contentWindow.ShowDialogPersonnelSingleDateFeatures(Caller);
}





