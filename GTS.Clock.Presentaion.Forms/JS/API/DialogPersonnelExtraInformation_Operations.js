
var CurrentPageState_PersonnelExtraInformation = 'View';
var CurrentPageCombosCallBcakStateObj = new Object();

function GetBoxesHeaders_PersonnelExtraInformation() {
    parent.document.getElementById('Title_DialogPersonnelExtraInformation').innerHTML = document.getElementById('hfTitle_DialogPersonnelExtraInformation').value;
    GetReserveFieldHeaders_PersonnelExtraInformation();
}

function GetReserveFieldHeaders_PersonnelExtraInformation() {
    GetReserveFieldHeaders_PersonnelExtraInformationPage("");
    DialogWaiting.Show();
}

function GetReserveFieldHeaders_PersonnelExtraInformationPage_onCallback(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_PersonnelExtraInformationSettings').value;
            Response[1] = document.getElementById('hfConnectionError_PersonnelExtraInformationSettings').value;
        }
        if (RetMessage[2] == 'success')
            SetReserveFieldHeaders_PersonnelExtraInformation(RetMessage[3]);
        else
            if (RetMessage[2] == 'error')
                showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function SetReserveFieldHeaders_PersonnelExtraInformation(ReserveFieldsCol) {
    ReserveFieldsCol = eval('(' + ReserveFieldsCol + ')');
    for (var i = 0; i < ReserveFieldsCol.length; i++) {
        if (ReserveFieldsCol[i].Value != '')
            document.getElementById('lblTitle_' + ReserveFieldsCol[i].Name + '_DialogPersonnelExtraInformation').innerHTML = ReserveFieldsCol[i].Value;
    }
}

function SetActionMode_PersonnelExtraInformation() {
    var CurrentStateObj_PersonnelExtraInformation = parent.DialogPersonnelExtraInformation.get_value();
    CurrentPageState_PersonnelExtraInformation = CurrentStateObj_PersonnelExtraInformation.PageState;
    document.getElementById('ActionMode_PersonnelExtraInformation').innerHTML = document.getElementById('hf' + CurrentPageState_PersonnelExtraInformation + '_DialogPersonnelExtraInformation').value;
    NavigatePersonnel_DialogPersonnelExtraInformation(CurrentStateObj_PersonnelExtraInformation);
}

function NavigatePersonnel_DialogPersonnelExtraInformation(CurrentStateObj_PersonnelExtraInformation) {
    switch (CurrentPageState_PersonnelExtraInformation) {
        case 'Add':
            if (CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation != '') {

                for (var i = 1; i <= 15; i++) {
                    document.getElementById('txtValue_R' + i + '_DialogPersonnelExtraInformation').value = (eval(CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation)[i - 1]).Value;
                }
                for (var j = 16; j <= 20; j++) {
                    document.getElementById('cmbValue_R' + j + '_DialogPersonnelExtraInformation_Input').value = (eval(CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation)[j - 1]).Text;
                }
            }
            break;
        case 'Edit':
            for (var i = 1; i <= 15; i++) {
                document.getElementById('txtValue_R' + i + '_DialogPersonnelExtraInformation').value = CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation != '' ? (eval(CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation)[i - 1]).Value : eval('CurrentStateObj_PersonnelExtraInformation.Reserve' + i + '') != '' ? eval('CurrentStateObj_PersonnelExtraInformation.Reserve' + i + '') : '';
            }
            for (var j = 16; j <= 20; j++) {
                document.getElementById('cmbValue_R' + j + '_DialogPersonnelExtraInformation_Input').value = CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation != '' ? (eval(CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation)[j - 1]).Text : eval('CurrentStateObj_PersonnelExtraInformation.Reserve' + j + 'Text') != '' ? eval('CurrentStateObj_PersonnelExtraInformation.Reserve' + j + 'Text') : '';
            }
            break;
    }
}

function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDialogPersonnelExtraInformation();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function tlbItemSave_TlbPersonnelExtraInformation_onClick() {
    UpdatePersonnelExtraInformation_PersonnelExtraInformation();
    CloseDialogPersonnelExtraInformation();
}

function UpdatePersonnelExtraInformation_PersonnelExtraInformation() {
    var CurrentStateObj_PersonnelExtraInformation = parent.DialogPersonnelExtraInformation.get_value();
    var StrPersonnelExtraInformation = '';
    var splitter = ',';
    for (var i = 1; i <= 15; i++) {
        StrPersonnelExtraInformation += '{"Name":"R' + i + '","Text":"","Value":"' + document.getElementById('txtValue_R' + i + '_DialogPersonnelExtraInformation').value + '"}' + splitter + '';
    }
    for (var j = 16; j <= 20; j++) {
        var cmb = eval('cmbValue_R' + j + '_DialogPersonnelExtraInformation');
        var reserveFieldVal = '0';
        var reserveFieldText = '';
        if (cmb.getSelectedItem() != undefined) {
            reserveFieldVal = cmb.getSelectedItem().get_id();
            reserveFieldText = cmb.getSelectedItem().get_text();
        }
        else {
            var cachedReserveFieldVal = '';
            var cachedReserveFieldText = '';
            switch (CurrentPageState_PersonnelExtraInformation) {
                case 'Add':
                    if (CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation != '') {
                        cachedReserveFieldVal = (eval(CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation)[j - 1]).Value;
                        cachedReserveFieldText = (eval(CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation)[j - 1]).Text;
                    }
                    break;
                case 'Edit':
                    cachedReserveFieldVal = CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation != '' ? (eval(CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation)[j - 1]).Value : eval('CurrentStateObj_PersonnelExtraInformation.Reserve' + j + '') != '' ? eval('CurrentStateObj_PersonnelExtraInformation.Reserve' + j + '') : '';
                    cachedReserveFieldText = CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation != '' ? (eval(CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation)[j - 1]).Text : eval('CurrentStateObj_PersonnelExtraInformation.Reserve' + j + 'Text') != '' ? eval('CurrentStateObj_PersonnelExtraInformation.Reserve' + j + 'Text') : '';
                    break;
            }
            if (cachedReserveFieldVal != '')
                reserveFieldVal = cachedReserveFieldVal;
            if (cachedReserveFieldText != '')
                reserveFieldText = cachedReserveFieldText;
        }
        if (j == 20)
            splitter = '';
        StrPersonnelExtraInformation += '{"Name":"R' + j + '","Text":"' + reserveFieldText + '","Value":"' + reserveFieldVal + '"}' + splitter + '';
    }
    StrPersonnelExtraInformation = '[' + StrPersonnelExtraInformation + ']';
    parent.parent.document.getElementById('DialogPersonnelMainInformation_IFrame').contentWindow.UpdatePersonnel_onAfterPersonnelExtraInformation(StrPersonnelExtraInformation);
}

function tlbItemSettings_TlbPersonnelExtraInformation_onClick() {
    ShowDialogPersonnelExtraInformationSettings();
}

function ShowDialogPersonnelExtraInformationSettings() {
    if (DialogPersonnelExtraInformationSettings.get_isShowing())
        DialogPersonnelExtraInformationSettings.Close();
    var ObjDialogPersonnelExtraInformation = parent.DialogPersonnelExtraInformation.get_value();
    var ObjDialogPersonnelExtraInformationSettings = new Object();
    ObjDialogPersonnelExtraInformationSettings.PersonnelState = ObjDialogPersonnelExtraInformation.PageState;
    DialogPersonnelExtraInformationSettings.set_value(ObjDialogPersonnelExtraInformationSettings);
    DialogPersonnelExtraInformationSettings.Show();
}

function tlbItemExit_TlbPersonnelExtraInformation_onClick() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_DialogPersonnelExtraInformation').value;
    DialogConfirm.Show();
}

function tlbItemHelp_TlbPersonnelExtraInformation_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPersonnelExtraInformation');
}

function tlbItemFormReconstruction_TlbPersonnelExtraInformation_onClick() {
    ReconstructForm_PersonnelExtraInformation();
}

function ReconstructForm_PersonnelExtraInformation() {
    CloseDialogPersonnelExtraInformation();
    parent.parent.document.getElementById('DialogPersonnelMainInformation_IFrame').contentWindow.ShowDialogPersonnelExtraInformation();
}

function CloseDialogPersonnelExtraInformation() {
    parent.document.getElementById('DialogPersonnelExtraInformation_IFrame').src = 'WhitePage.aspx';
    parent.DialogPersonnelExtraInformation.Close();
}

function CharToKeyCode_PersonnelExtraInformation(str) {
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

function cmbValue_R16_DialogPersonnelExtraInformation_onExpand(sender, e) {
    CollapseControls_PersonnelExtraInformation(cmbValue_R16_DialogPersonnelExtraInformation);
    if (cmbValue_R16_DialogPersonnelExtraInformation.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbValue_R16_DialogPersonnelExtraInformation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbValue_R16_DialogPersonnelExtraInformation = true;
        CallBack_cmbValue_R16_DialogPersonnelExtraInformation.callback();
    }
}

function cmbValue_R16_DialogPersonnelExtraInformation_onCollapse(sender, e) {
    if (cmbValue_R16_DialogPersonnelExtraInformation.getSelectedItem() == undefined)
        CheckText_cmbsControls_DialogPersonnelExtraInformation_onCollapse(16);
}

function CallBack_cmbValue_R16_DialogPersonnelExtraInformation_onBeforeCallback(sender, e) {
    cmbValue_R16_DialogPersonnelExtraInformation.dispose();
}

function CallBack_cmbValue_R16_DialogPersonnelExtraInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_R16_DialogPersonnelExtraInformation').value;
    if (error == "") {
        document.getElementById('cmbValue_R16_DialogPersonnelExtraInformation_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbValue_R16_DialogPersonnelExtraInformation_DropImage').mousedown();
        cmbValue_R16_DialogPersonnelExtraInformation.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbValue_R16_DialogPersonnelExtraInformation_DropDown').style.display = 'none';
    }
}

function CallBack_cmbValue_R16_DialogPersonnelExtraInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelExtraInformation();
}

function cmbValue_R17_DialogPersonnelExtraInformation_onExpand(sender, e) {
    CollapseControls_PersonnelExtraInformation(cmbValue_R17_DialogPersonnelExtraInformation);
    if (cmbValue_R17_DialogPersonnelExtraInformation.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbValue_R17_DialogPersonnelExtraInformation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbValue_R17_DialogPersonnelExtraInformation = true;
        CallBack_cmbValue_R17_DialogPersonnelExtraInformation.callback();
    }
}

function cmbValue_R17_DialogPersonnelExtraInformation_onCollapse(sender, e) {
    if (cmbValue_R17_DialogPersonnelExtraInformation.getSelectedItem() == undefined)
        CheckText_cmbsControls_DialogPersonnelExtraInformation_onCollapse(17);
}

function CallBack_cmbValue_R17_DialogPersonnelExtraInformation_onBeforeCallback(sender, e) {
    cmbValue_R17_DialogPersonnelExtraInformation.dispose();
}

function CallBack_cmbValue_R17_DialogPersonnelExtraInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_R17_DialogPersonnelExtraInformation').value;
    if (error == "") {
        document.getElementById('cmbValue_R17_DialogPersonnelExtraInformation_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbValue_R17_DialogPersonnelExtraInformation_DropImage').mousedown();
        cmbValue_R17_DialogPersonnelExtraInformation.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbValue_R17_DialogPersonnelExtraInformation_DropDown').style.display = 'none';
    }
}

function CallBack_cmbValue_R17_DialogPersonnelExtraInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelExtraInformation();
}

function cmbValue_R18_DialogPersonnelExtraInformation_onExpand(sender, e) {
    CollapseControls_PersonnelExtraInformation(cmbValue_R18_DialogPersonnelExtraInformation);
    if (cmbValue_R18_DialogPersonnelExtraInformation.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbValue_R18_DialogPersonnelExtraInformation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbValue_R18_DialogPersonnelExtraInformation = true;
        CallBack_cmbValue_R18_DialogPersonnelExtraInformation.callback();
    }
}

function cmbValue_R18_DialogPersonnelExtraInformation_onCollapse(sender, e) {
    if (cmbValue_R18_DialogPersonnelExtraInformation.getSelectedItem() == undefined)
        CheckText_cmbsControls_DialogPersonnelExtraInformation_onCollapse(18);
}

function CallBack_cmbValue_R18_DialogPersonnelExtraInformation_onBeforeCallback(sender, e) {
    cmbValue_R18_DialogPersonnelExtraInformation.dispose();
}

function CallBack_cmbValue_R18_DialogPersonnelExtraInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_R18_DialogPersonnelExtraInformation').value;
    if (error == "") {
        document.getElementById('cmbValue_R18_DialogPersonnelExtraInformation_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbValue_R18_DialogPersonnelExtraInformation_DropImage').mousedown();
        cmbValue_R18_DialogPersonnelExtraInformation.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbValue_R18_DialogPersonnelExtraInformation_DropDown').style.display = 'none';
    }
}

function CallBack_cmbValue_R18_DialogPersonnelExtraInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelExtraInformation();
}

function cmbValue_R19_DialogPersonnelExtraInformation_onExpand(sender, e) {
    CollapseControls_PersonnelExtraInformation(cmbValue_R19_DialogPersonnelExtraInformation);
    if (cmbValue_R19_DialogPersonnelExtraInformation.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbValue_R19_DialogPersonnelExtraInformation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbValue_R19_DialogPersonnelExtraInformation = true;
        CallBack_cmbValue_R19_DialogPersonnelExtraInformation.callback();
    }
}

function cmbValue_R19_DialogPersonnelExtraInformation_onCollapse(sender, e) {
    if (cmbValue_R19_DialogPersonnelExtraInformation.getSelectedItem() == undefined)
        CheckText_cmbsControls_DialogPersonnelExtraInformation_onCollapse(19);
}

function CallBack_cmbValue_R19_DialogPersonnelExtraInformation_onBeforeCallback(sender, e) {
    cmbValue_R19_DialogPersonnelExtraInformation.dispose();
}

function CallBack_cmbValue_R19_DialogPersonnelExtraInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_R19_DialogPersonnelExtraInformation').value;
    if (error == "") {
        document.getElementById('cmbValue_R19_DialogPersonnelExtraInformation_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbValue_R19_DialogPersonnelExtraInformation_DropImage').mousedown();
        cmbValue_R19_DialogPersonnelExtraInformation.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbValue_R19_DialogPersonnelExtraInformation_DropDown').style.display = 'none';
    }
}

function CallBack_cmbValue_R19_DialogPersonnelExtraInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelExtraInformation();
}

function cmbValue_R20_DialogPersonnelExtraInformation_onExpand(sender, e) {
    CollapseControls_PersonnelExtraInformation(cmbValue_R20_DialogPersonnelExtraInformation);
    if (cmbValue_R20_DialogPersonnelExtraInformation.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbValue_R20_DialogPersonnelExtraInformation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbValue_R20_DialogPersonnelExtraInformation = true;
        CallBack_cmbValue_R20_DialogPersonnelExtraInformation.callback();
    }
}

function cmbValue_R20_DialogPersonnelExtraInformation_onCollapse(sender, e) {
    if (cmbValue_R20_DialogPersonnelExtraInformation.getSelectedItem() == undefined)
        CheckText_cmbsControls_DialogPersonnelExtraInformation_onCollapse(20);
}

function CallBack_cmbValue_R20_DialogPersonnelExtraInformation_onBeforeCallback(sender, e) {
    cmbValue_R20_DialogPersonnelExtraInformation.dispose();
}

function CallBack_cmbValue_R20_DialogPersonnelExtraInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_R20_DialogPersonnelExtraInformation').value;
    if (error == "") {
        document.getElementById('cmbValue_R20_DialogPersonnelExtraInformation_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbValue_R20_DialogPersonnelExtraInformation_DropImage').mousedown();
        cmbValue_R20_DialogPersonnelExtraInformation.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbValue_R20_DialogPersonnelExtraInformation_DropDown').style.display = 'none';
    }
}

function CallBack_cmbValue_R20_DialogPersonnelExtraInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelExtraInformation();
}

function ShowConnectionError_PersonnelExtraInformation() {
    var error = document.getElementById('hfErrorType_PersonnelExtraInformation').value;
    var errorBody = document.getElementById('hfConnectionError_PersonnelExtraInformation').value;
    showDialog(error, errorBody, 'error');
}

function CollapseControls_PersonnelExtraInformation(exception) {
    if (exception == null || exception != cmbValue_R16_DialogPersonnelExtraInformation)
        cmbValue_R16_DialogPersonnelExtraInformation.collapse();
    if (exception == null || exception != cmbValue_R17_DialogPersonnelExtraInformation)
        cmbValue_R17_DialogPersonnelExtraInformation.collapse();
    if (exception == null || exception != cmbValue_R18_DialogPersonnelExtraInformation)
        cmbValue_R18_DialogPersonnelExtraInformation.collapse();
    if (exception == null || exception != cmbValue_R19_DialogPersonnelExtraInformation)
        cmbValue_R19_DialogPersonnelExtraInformation.collapse();
    if (exception == null || exception != cmbValue_R20_DialogPersonnelExtraInformation)
        cmbValue_R20_DialogPersonnelExtraInformation.collapse();

}

function CheckText_cmbsControls_DialogPersonnelExtraInformation_onCollapse(index) {
    var CurrentStateObj_PersonnelExtraInformation = parent.DialogPersonnelExtraInformation.get_value();
    switch (CurrentPageState_PersonnelExtraInformation) {
        case 'Add':
            if (CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation != '') {
                if ((eval(CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation)[index - 1]).Value != '' && (eval(CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation)[index - 1]).Value != undefined)
                    document.getElementById('cmbValue_R' + index + '_DialogPersonnelExtraInformation_Input').value = (eval(CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation)[index - 1]).Text;
            }
            break;
        case 'Edit':
              document.getElementById('cmbValue_R' + index + '_DialogPersonnelExtraInformation_Input').value = CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation != '' ? (eval(CurrentStateObj_PersonnelExtraInformation.StrPersonnelExtraInformation)[index - 1]).Text : eval('CurrentStateObj_PersonnelExtraInformation.Reserve' + index + 'Text');
            break;
    }
}

function CheckPersonnelReserveFieldLoadAccess_PersonnelExtraInformation() {
    var error = document.getElementById('hfLoadException_PersonnelExtraInformation').value;
    if (error != "") {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
    }
}








