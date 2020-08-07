
var CurrentPageState_PreCard = 'View';
var ConfirmState_PreCard = null;
var ObjPreCard_PreCard = null;
var SelectedPreCardType_PreCard = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var chekedList_PrecardAccessLevelsAsign = '';

function GetBoxesHeaders_PreCard() {
    document.getElementById('header_PreCards_PreCard').innerHTML = document.getElementById('hfheader_PreCards_PreCard').value;
    document.getElementById('header_tblPreCards_PreCard').innerHTML = document.getElementById('hfheader_tblPreCards_PreCard').value;
    document.getElementById('header_PrecardAccessLevels_PreCard').innerHTML = document.getElementById('hfheader_PrecardAccessLevels_PreCard').value;
}

function tlbItemNew_TlbPreCard_onClick() {
    ClearList_PreCard();
    ChangePageState_PreCard('Add');
    FocusOnFirstElement_PreCard();
}

function FocusOnFirstElement_PreCard() {
    document.getElementById('chbActivePreCard_PreCard').focus();
}

function SetActionMode_PreCard(state) {
    document.getElementById('ActionMode_PreCard').innerHTML = document.getElementById("hf" + state + "_PreCard").value;
}

function ChangePageState_PreCard(state) {
    CurrentPageState_PreCard = state;
    SetActionMode_PreCard(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbPreCard.get_items().getItemById('tlbItemNew_TlbPreCard') != null) {
            TlbPreCard.get_items().getItemById('tlbItemNew_TlbPreCard').set_enabled(false);
            TlbPreCard.get_items().getItemById('tlbItemNew_TlbPreCard').set_imageUrl('add_silver.png');
        }
        if (TlbPreCard.get_items().getItemById('tlbItemEdit_TlbPreCard') != null) {
            TlbPreCard.get_items().getItemById('tlbItemEdit_TlbPreCard').set_enabled(false);
            TlbPreCard.get_items().getItemById('tlbItemEdit_TlbPreCard').set_imageUrl('edit_silver.png');
        }
        if (TlbPreCard.get_items().getItemById('tlbItemDelete_TlbPreCard') != null) {
            TlbPreCard.get_items().getItemById('tlbItemDelete_TlbPreCard').set_enabled(false);
            TlbPreCard.get_items().getItemById('tlbItemDelete_TlbPreCard').set_imageUrl('remove_silver.png');
        }
        TlbPreCard.get_items().getItemById('tlbItemSave_TlbPreCard').set_enabled(true);
        TlbPreCard.get_items().getItemById('tlbItemSave_TlbPreCard').set_imageUrl('save.png');
        TlbPreCard.get_items().getItemById('tlbItemCancel_TlbPreCard').set_enabled(true);
        TlbPreCard.get_items().getItemById('tlbItemCancel_TlbPreCard').set_imageUrl('cancel.png');
        document.getElementById('chbActivePreCard_PreCard').disabled = '';
        if (state == 'Add')
            document.getElementById('chbActivePreCard_PreCard').checked = true;
        document.getElementById('txtPreCardCode_PreCard').disabled = '';
        document.getElementById('txtPreCardName_PreCard').disabled = '';
        document.getElementById('rdbDaily_PreCard').disabled = '';
        document.getElementById('rdbHourly_PreCard').disabled = '';
        document.getElementById('rdbMonthly_PreCard').disabled = '';
        document.getElementById('chbJustification_PreCard').disabled = '';
        cmbPreCardType_PreCard.enable();
        if (state == 'Edit')
            NavigatePreCard_PreCard(GridPreCards_PreCard.getSelectedItems()[0]);
        if (state == 'Delete')
            PreCard_onSave();
    }
    if (state == 'View') {
        if (TlbPreCard.get_items().getItemById('tlbItemNew_TlbPreCard') != null) {
            TlbPreCard.get_items().getItemById('tlbItemNew_TlbPreCard').set_enabled(true);
            TlbPreCard.get_items().getItemById('tlbItemNew_TlbPreCard').set_imageUrl('add.png');
        }
        if (TlbPreCard.get_items().getItemById('tlbItemEdit_TlbPreCard')) {
            TlbPreCard.get_items().getItemById('tlbItemEdit_TlbPreCard').set_enabled(true);
            TlbPreCard.get_items().getItemById('tlbItemEdit_TlbPreCard').set_imageUrl('edit.png');
        }
        if (TlbPreCard.get_items().getItemById('tlbItemDelete_TlbPreCard') != null) {
            TlbPreCard.get_items().getItemById('tlbItemDelete_TlbPreCard').set_enabled(true);
            TlbPreCard.get_items().getItemById('tlbItemDelete_TlbPreCard').set_imageUrl('remove.png');
        }
        TlbPreCard.get_items().getItemById('tlbItemSave_TlbPreCard').set_enabled(false);
        TlbPreCard.get_items().getItemById('tlbItemSave_TlbPreCard').set_imageUrl('save_silver.png');
        TlbPreCard.get_items().getItemById('tlbItemCancel_TlbPreCard').set_enabled(false);
        TlbPreCard.get_items().getItemById('tlbItemCancel_TlbPreCard').set_imageUrl('cancel_silver.png');
        document.getElementById('chbActivePreCard_PreCard').disabled = 'disabled';
        document.getElementById('txtPreCardCode_PreCard').disabled = 'disabled';
        document.getElementById('txtPreCardName_PreCard').disabled = 'disabled';
        document.getElementById('rdbDaily_PreCard').disabled = 'disabled';
        document.getElementById('rdbHourly_PreCard').disabled = 'disabled';
        document.getElementById('rdbMonthly_PreCard').disabled = 'disabled';
        document.getElementById('chbJustification_PreCard').disabled = 'disabled';
        cmbPreCardType_PreCard.disable();
    }
}

function PreCard_onSave() {
    if (CurrentPageState_PreCard != 'Delete')
        UpdatePreCard_PreCard();
    else
        ShowDialogConfirm('Delete');
}

function UpdatePreCard_PreCard() {
    ObjPreCard_PreCard = new Object();
    ObjPreCard_PreCard.ID = '0';
    ObjPreCard_PreCard.IsActive = false;
    ObjPreCard_PreCard.Code = null;
    ObjPreCard_PreCard.Name = null;
    ObjPreCard_PreCard.PreCardTypeID = '0';
    ObjPreCard_PreCard.PreCardTypeTitle = null;
    ObjPreCard_PreCard.IsDaily = false;
    ObjPreCard_PreCard.IsHourly = false;
    ObjPreCard_PreCard.IsMonthly = false;
    ObjPreCard_PreCard.IsJustification = false;
    var SelectedItems_GridPreCards_PreCard = GridPreCards_PreCard.getSelectedItems();
    if (SelectedItems_GridPreCards_PreCard.length > 0)
        ObjPreCard_PreCard.ID = SelectedItems_GridPreCards_PreCard[0].getMember("ID").get_text();

    if (CurrentPageState_PreCard != 'Delete') {
        ObjPreCard_PreCard.IsActive = document.getElementById('chbActivePreCard_PreCard').checked;
        ObjPreCard_PreCard.Code = document.getElementById('txtPreCardCode_PreCard').value;
        ObjPreCard_PreCard.Name = document.getElementById('txtPreCardName_PreCard').value;
        ObjPreCard_PreCard.IsDaily = document.getElementById('rdbDaily_PreCard').checked;
        ObjPreCard_PreCard.IsHourly = document.getElementById('rdbHourly_PreCard').checked;
        ObjPreCard_PreCard.IsMonthly = document.getElementById('rdbMonthly_PreCard').checked;
        ObjPreCard_PreCard.IsJustification = document.getElementById('chbJustification_PreCard').checked;
        if (cmbPreCardType_PreCard.getSelectedItem() != undefined) {
            ObjPreCard_PreCard.PreCardTypeID = cmbPreCardType_PreCard.getSelectedItem().get_value();
            ObjPreCard_PreCard.PreCardTypeTitle = cmbPreCardType_PreCard.getSelectedItem().get_text();
        }
        else {
            if (SelectedPreCardType_PreCard != null) {
                ObjPreCard_PreCard.PreCardTypeID = SelectedPreCardType_PreCard.ID;
                ObjPreCard_PreCard.PreCardTypeTitle = SelectedPreCardType_PreCard.Name;
            }
        }

    }
    UpdatePreCard_PreCardPage(CharToKeyCode_PreCard(CurrentPageState_PreCard), CharToKeyCode_PreCard(ObjPreCard_PreCard.ID), CharToKeyCode_PreCard(ObjPreCard_PreCard.IsActive.toString()), CharToKeyCode_PreCard(ObjPreCard_PreCard.Code), CharToKeyCode_PreCard(ObjPreCard_PreCard.Name), CharToKeyCode_PreCard(ObjPreCard_PreCard.PreCardTypeID), CharToKeyCode_PreCard(ObjPreCard_PreCard.IsDaily.toString()), CharToKeyCode_PreCard(ObjPreCard_PreCard.IsHourly.toString()), CharToKeyCode_PreCard(ObjPreCard_PreCard.IsMonthly.toString()), CharToKeyCode_PreCard(ObjPreCard_PreCard.IsJustification.toString()));
    DialogWaiting.Show();
}

function UpdatePreCard_PreCardPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_PreCard').value;
            Response[1] = document.getElementById('hfConnectionError_PreCard').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_PreCard();
            PreCard_OnAfterUpdate(Response);
            ChangePageState_PreCard('View');
        }
        else {
            if (CurrentPageState_PreCard == 'Delete')
                ChangePageState_PreCard('View');
        }
    }
}

function PreCard_OnAfterUpdate(Response) {
    if (ObjPreCard_PreCard != null) {
        var IsActive = ObjPreCard_PreCard.IsActive;
        var PreCardCode = ObjPreCard_PreCard.Code;
        var PreCardName = ObjPreCard_PreCard.Name;
        var PreCardTypeID = ObjPreCard_PreCard.PreCardTypeID;
        var PreCardTypeTitle = ObjPreCard_PreCard.PreCardTypeTitle;
        var IsDaily = ObjPreCard_PreCard.IsDaily;
        var IsHourly = ObjPreCard_PreCard.IsHourly;
        var IsMonthly = ObjPreCard_PreCard.IsMonthly;
        var IsJustification = ObjPreCard_PreCard.IsJustification;

        var PreCardItem = null;
        GridPreCards_PreCard.beginUpdate();
        switch (CurrentPageState_PreCard) {
            case 'Add':
                PreCardItem = GridPreCards_PreCard.get_table().addEmptyRow(GridPreCards_PreCard.get_recordCount());
                PreCardItem.setValue(0, Response[3], false);
                GridPreCards_PreCard.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridPreCards_PreCard.selectByKey(Response[3], 0, false);
                PreCardItem = GridPreCards_PreCard.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridPreCards_PreCard.selectByKey(ObjPreCard_PreCard.ID, 0, false);
                GridPreCards_PreCard.deleteSelected();
                break;
        }
        if (CurrentPageState_PreCard != 'Delete') {
            PreCardItem.setValue(1, IsActive, false);
            PreCardItem.setValue(2, IsJustification, false);
            PreCardItem.setValue(3, PreCardCode, false);
            PreCardItem.setValue(4, PreCardName, false);
            PreCardItem.setValue(6, PreCardTypeID, false);
            PreCardItem.setValue(7, PreCardTypeTitle, false);
            PreCardItem.setValue(8, IsDaily, false);
            PreCardItem.setValue(9, IsHourly, false);
            PreCardItem.setValue(10, IsMonthly, false);
        }
        GridPreCards_PreCard.endUpdate();
    }
}

function ClearList_PreCard() {
    document.getElementById('chbActivePreCard_PreCard').checked = false;
    document.getElementById('txtPreCardCode_PreCard').value = '';
    document.getElementById('txtPreCardName_PreCard').value = '';
    document.getElementById('cmbPreCardType_PreCard_Input').value = document.getElementById('hfcmbAlarm_PreCard').value;
    cmbPreCardType_PreCard.unSelect();
    document.getElementById('rdbDaily_PreCard').checked = false;
    document.getElementById('rdbHourly_PreCard').checked = false;
    document.getElementById('chbJustification_PreCard').checked = false;
    SelectedPreCardType_PreCard = null;
}

function CharToKeyCode_PreCard(str) {
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

function tlbItemEdit_TlbPreCard_onClick() {
    ChangePageState_PreCard('Edit');
    FocusOnFirstElement_PreCard();
}

function tlbItemDelete_TlbPreCard_onClick() {
    ChangePageState_PreCard('Delete');
}

function tlbItemSave_TlbPreCard_onClick() {
    CollapseControls_PreCard();
    PreCard_onSave();
}

function tlbItemCancel_TlbPreCard_onClick() {
    ChangePageState_PreCard('View');
    ClearList_PreCard();
}

function tlbItemExit_TlbPreCard_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_PreCard = confirmState;
    if (CurrentPageState_PreCard == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_PreCard').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PreCard').value;
    DialogConfirm.Show();
    CollapseControls_PreCard();
}

function Refresh_GridPreCards_PreCard() {
    Fill_GridPreCards_PreCard();
}

function Fill_GridPreCards_PreCard() {
    document.getElementById('loadingPanel_GridPreCards_PreCard').innerHTML = document.getElementById('hfloadingPanel_GridPreCards_PreCard').value;
    CallBack_GridPreCards_PreCard.callback();
}

function GridPreCards_PreCard_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridPreCards_PreCard').innerHTML = '';
}

function GridPreCards_PreCard_onItemSelect(sender, e) {
    if (CurrentPageState_PreCard != 'Add')
        NavigatePreCard_PreCard(e.get_item());
}

function NavigatePreCard_PreCard(selectedPreCardItem) {
    if (selectedPreCardItem != undefined) {
        document.getElementById('chbActivePreCard_PreCard').checked = selectedPreCardItem.getMember('Active').get_value();
        document.getElementById('txtPreCardCode_PreCard').value = selectedPreCardItem.getMember('Code').get_value();
        document.getElementById('txtPreCardName_PreCard').value = selectedPreCardItem.getMember('Name').get_value();
        document.getElementById('txtPreCardRealName_PreCard').value = selectedPreCardItem.getMember('RealName').get_value();
        SelectedPreCardType_PreCard = new Object();
        SelectedPreCardType_PreCard.ID = selectedPreCardItem.getMember('PrecardGroup.ID').get_text();
        document.getElementById('cmbPreCardType_PreCard_Input').value = SelectedPreCardType_PreCard.Name = selectedPreCardItem.getMember('PrecardGroup.Name').get_text();
        document.getElementById('rdbDaily_PreCard').checked = selectedPreCardItem.getMember('IsDaily').get_value();
        document.getElementById('rdbHourly_PreCard').checked = selectedPreCardItem.getMember('IsHourly').get_value();
        document.getElementById('rdbMonthly_PreCard').checked = selectedPreCardItem.getMember('IsMonthly').get_value();
        document.getElementById('chbJustification_PreCard').checked = selectedPreCardItem.getMember('IsPermit').get_value();
    }
}

function CallBack_GridPreCards_PreCard_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_PreCards').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridPreCards_PreCard();
    }
}

function cmbPreCardType_PreCard_onExpand(sender, e) {
    if (cmbPreCardType_PreCard.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPreCardType_PreCard == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPreCardType_PreCard = true;
        CallBack_cmbPreCardType_PreCard.callback();
    }
}

function cmbPreCardType_PreCard_onCollapse(sender, e) {
    if (cmbPreCardType_PreCard.getSelectedItem() == undefined) {
        if (SelectedPreCardType_PreCard.ID == null || SelectedPreCardType_PreCard.ID == undefined)
            document.getElementById('cmbPreCardType_PreCard_Input').value = document.getElementById('hfcmbAlarm_PreCard').value;
        else
            if (SelectedPreCardType_PreCard.ID != null && SelectedPreCardType_PreCard.ID != undefined)
                document.getElementById('cmbPreCardType_PreCard_Input').value = SelectedPreCardType_PreCard.Name;
    }
}

function CallBack_cmbPreCardType_PreCard_onBeforeCallback(sender, e) {
    cmbPreCardType_PreCard.dispose();
}

function CallBack_cmbPreCardType_PreCard_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_PreCardType').value;
    if (error == "") {
        document.getElementById('cmbPreCardType_PreCard_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPreCardType_PreCard_DropImage').mousedown();
        cmbPreCardType_PreCard.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPreCardType_PreCard_DropDown').style.display = 'none';
    }
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_PreCard) {
        case 'Delete':
            DialogConfirm.Close();
            UpdatePreCard_PreCard();
            break;
        case 'Exit':
            ClearList_PreCard();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_PreCard('View');
}

function CallBack_GridPreCards_PreCard_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridPreCards_PreCard').innerHTML = '';
    ShowConnectionError_PreCard();
}

function ShowConnectionError_PreCard() {
    var error = document.getElementById('hfErrorType_PreCard').value;
    var errorBody = document.getElementById('hfConnectionError_PreCard').value;
    showDialog(error, errorBody, 'error');
}

function CollapseControls_PreCard() {
    cmbPreCardType_PreCard.collapse();
}

function tlbItemFormReconstruction_TlbPreCard_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvPreCardIntroduction_iFrame').src = 'PreCard.aspx';
}

function tlbItemHelp_TlbPreCard_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPreCard');
}

function tlbItemPrecardAccessLevels_TlbPreCard_onClick() {
    ShowDialogPrecardAccessLevels();
}

function ShowDialogPrecardAccessLevels() {
    if (GridPreCards_PreCard.getSelectedItems().length > 0) {
        Fill_trvPrecardAccessLevels_PreCard();
        DialogPrecardAccessLevels.Show();
    }
}

function tlbItemSave_TlbPreacardAccessLevels_onClick() {
    UpdatePrecardAccessLevelsAsign_PreCard();
}

function UpdatePrecardAccessLevelsAsign_PreCard() {
    var precardID = GridPreCards_PreCard.getSelectedItems()[0].getMember('ID').get_text();
    var parentNodesCol = trvPrecardAccessLevels_PreCard.get_nodes();
    for (var i = 0; i < parentNodesCol.get_length() ; i++) {
        var parentNode = parentNodesCol.getNode(i);
        GetChildNodesCheck_trvPrecardAccessLevels_PreCard(parentNode);
    }
    if (chekedList_PrecardAccessLevelsAsign.charAt(chekedList_PrecardAccessLevelsAsign.length - 1) == ',')
        chekedList_PrecardAccessLevelsAsign = chekedList_PrecardAccessLevelsAsign.substring(0, chekedList_PrecardAccessLevelsAsign.length - 1);
    chekedList_PrecardAccessLevelsAsign = '[' + chekedList_PrecardAccessLevelsAsign + ']';
    UpdatePrecardAccessLevelsAsign_PreCardPage(CharToKeyCode_PreCard(precardID), CharToKeyCode_PreCard(chekedList_PrecardAccessLevelsAsign));
    DialogWaiting.Show();
}

function UpdatePrecardAccessLevelsAsign_PreCardPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        chekedList_PrecardAccessLevelsAsign = '';
    }
}

function tlbItemExit_TlbPreacardAccessLevels_onClick() {
    CloseDialogPrecardAccessLevels();
}

function CloseDialogPrecardAccessLevels() {
    ClearNodes_trvPrecardAccessLevels_PreCard();
    DialogPrecardAccessLevels.Close();
}

function ClearNodes_trvPrecardAccessLevels_PreCard() {
    trvPrecardAccessLevels_PreCard.beginUpdate();
    trvPrecardAccessLevels_PreCard.get_nodes().clear();
    trvPrecardAccessLevels_PreCard.endUpdate();
}

function Fill_trvPrecardAccessLevels_PreCard() {
    if (GridPreCards_PreCard.getSelectedItems().length > 0) {
        document.getElementById('loadingPanel_trvPrecardAccessLevels_PreCard').innerHTML = document.getElementById('hfloadingPanel_trvPrecardAccessLevels_PreCard').value;
        var precardID = GridPreCards_PreCard.getSelectedItems()[0].getMember('ID').get_text();
        CallBack_trvPrecardAccessLevels_PreCard.callback(CharToKeyCode_PreCard(precardID));
    }
}

function Refresh_trvPrecardAccessLevels_PreCard() {
    Fill_trvPrecardAccessLevels_PreCard();
}

function trvPrecardAccessLevels_PreCard_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvPrecardAccessLevels_PreCard').innerHTML = '';
}

function CallBack_trvPrecardAccessLevels_PreCard_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_PrecardAccessLevels').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvPrecardAccessLevels_PreCard();
    }
}

function CallBack_trvPrecardAccessLevels_PreCard_onCallbackError(sender, e) {
    ShowConnectionError_PreCard();
}

function GetChildNodesCheck_trvPrecardAccessLevels_PreCard(parentNode) {
    var childNodesCol = parentNode.get_nodes();
    for (var i = 0; i < childNodesCol.get_length() ; i++) {
        var childNode = childNodesCol.getNode(i);
        if (childNode.get_checked())
            chekedList_PrecardAccessLevelsAsign += childNode.get_id() + ',';
        if (childNode != undefined)
            GetChildNodesCheck_trvPrecardAccessLevels_PreCard(childNode);
    }
}

function trvPrecardAccessLevels_PreCard_onNodeCheckChange(sender, e) {
    var parentNode = e.get_node();
    var checked = parentNode.get_checked();
    ChangeChildNodesCheck_trvPrecardAccessLevels_PreCard(parentNode, checked);
}

function ChangeChildNodesCheck_trvPrecardAccessLevels_PreCard(parentNode, checked) {
    var childNodesCol = parentNode.get_nodes();
    for (var i = 0; i < childNodesCol.get_length() ; i++) {
        var childNode = childNodesCol.getNode(i);
        childNode.set_checked(checked);
        ChangeChildNodesCheck_trvPrecardAccessLevels_PreCard(childNode, checked);
    }
}






