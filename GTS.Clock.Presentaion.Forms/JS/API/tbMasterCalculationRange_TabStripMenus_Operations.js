
var CurrentPageState_MasterCalculationRange = 'View';
var ConfirmState_MasterCalculationRange = null;
var ObjCalculationRange_CalculationRange = null;

function GetBoxesHeaders_MasterCalculationRange() {
    document.getElementById("header_CalculationRange_MasterCalculationRange").innerHTML = document.getElementById("hfheader_CalculationRange_MasterCalculationRange").value;
}

function ShowDialogCalculationRange() {
    var CurrentStateObj_MasterCalculationRange = null;
    switch (CurrentPageState_MasterCalculationRange) {
        case 'Add':
            CurrentStateObj_MasterCalculationRange = { "State": "" + CurrentPageState_MasterCalculationRange + "", "CalculationRangeID": "" };
            break;
        case 'Edit':
            var CalculationRangeName_MasterCalculationRange = '';
            var CalculationRangeDescription_MasterCalculationRange = '';
            var selectedCalculationRanges_MasterCalculationRange = GridCalculationRange_MasterCalculationRange.getSelectedItems()[0];
            if (selectedCalculationRanges_MasterCalculationRange.getMember('Name').get_text() != undefined)
                CalculationRangeName_MasterCalculationRange = selectedCalculationRanges_MasterCalculationRange.getMember('Name').get_text();
            if (selectedCalculationRanges_MasterCalculationRange.getMember('Description').get_text() != undefined)
                CalculationRangeDescription_MasterCalculationRange = selectedCalculationRanges_MasterCalculationRange.getMember('Description').get_text();
            CurrentStateObj_MasterCalculationRange = { "State": "" + CurrentPageState_MasterCalculationRange + "", "CalculationRangeID": "" + selectedCalculationRanges_MasterCalculationRange.getMember('ID').get_text() + "", "CalculationRangeName": "" + CalculationRangeName_MasterCalculationRange + "", "CalculationRangeDescription": "" + CalculationRangeDescription_MasterCalculationRange + "" };
    }
    parent.DialogCalculationRange.set_value(CurrentStateObj_MasterCalculationRange);
    parent.DialogCalculationRange.Show();
}

function tlbItemNew_TlbCalculationRange_onClick() {
    CurrentPageState_MasterCalculationRange = 'Add';
    ShowDialogCalculationRange();
}

function tlbItemEdit_TlbCalculationRange_onClick() {
    if (GridCalculationRange_MasterCalculationRange.getSelectedItems().length > 0) {
        CurrentPageState_MasterCalculationRange = 'Edit';
        ShowDialogCalculationRange();
    }
}

function tlbItemDelete_TlbCalculationRange_onClick() {
    CurrentPageState_MasterCalculationRange = 'Delete';
    ShowDialogConfirm('Delete');
}

function tlbItemCopy_TlbCalculationRange_onClick() {
    var selectedCalculationRangeID = '0';
    var selectedCalculationRange_MasterCalculationRange = GridCalculationRange_MasterCalculationRange.getSelectedItems();
    if (selectedCalculationRange_MasterCalculationRange.length > 0)
        selectedCalculationRangeID = selectedCalculationRange_MasterCalculationRange[0].getMember('ID').get_text();
    CopyCalculationRange_MasterCalculationRangePage(CharToKeyCode_MasterCalculationRange(selectedCalculationRangeID));
    DialogWaiting.Show();
}

function CopyCalculationRange_MasterCalculationRangePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success')
            Refresh_GridCalculationRange_MasterCalculationRange();
    }
}

function tlbItemExit_TlbCalculationRange_onClick() {
    ShowDialogConfirm('Exit');
}

function Fill_GridCalculationRange_MasterCalculationRange() {
    document.getElementById('loadingPanel_GridMasterCalculationRange_MasterCalculationRange').innerHTML = document.getElementById('hfloadingPanel_GridMasterCalculationRange_MasterCalculationRange').value;
    CallBack_GridCalculationRange_MasterCalculationRange.callback();
}

function Refresh_GridCalculationRange_MasterCalculationRange() {
    Fill_GridCalculationRange_MasterCalculationRange();
}

function GridCalculationRange_MasterCalculationRange_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridMasterCalculationRange_MasterCalculationRange').innerHTML = '';
}

function CallBack_GridCalculationRange_MasterCalculationRange_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MasterCalculationRange').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridCalculationRange_MasterCalculationRange();
    }
}

function UpdateCalculationRange_MasterCalculationRange() {
    ObjCalculationRange_CalculationRange = new Object();
    ObjCalculationRange_CalculationRange.ID = '0';
    var SelectedCalculationRange_MasterCalculationRange = GridCalculationRange_MasterCalculationRange.getSelectedItems();
    if (SelectedCalculationRange_MasterCalculationRange.length > 0)
        ObjCalculationRange_CalculationRange.ID = SelectedCalculationRange_MasterCalculationRange[0].getMember('ID').get_text();
    UpdateCalculationRange_MasterCalculationRangePage(CharToKeyCode_MasterCalculationRange(CurrentPageState_MasterCalculationRange), CharToKeyCode_MasterCalculationRange(ObjCalculationRange_CalculationRange.ID));
    DialogWaiting.Show();
}

function UpdateCalculationRange_MasterCalculationRangePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_MasterCalculationRange').value;
            Response[1] = document.getElementById('hfConnectionError_MasterCalculationRange').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            CalculationRange_OnAfterUpdate(Response);
            CurrentPageState_MasterCalculationRange = 'View';
        }
        else {
            if (CurrentPageState_MasterCalculationRange == 'Delete')
                CurrentPageState_MasterCalculationRange = 'View';
        }
    }
}

function CalculationRange_OnAfterUpdate() {
    if (ObjCalculationRange_CalculationRange != null) {
        GridCalculationRange_MasterCalculationRange.beginUpdate();
        switch (CurrentPageState_MasterCalculationRange) {
            case 'Delete':
                GridCalculationRange_MasterCalculationRange.selectByKey(ObjCalculationRange_CalculationRange.ID, 0, false);
                GridCalculationRange_MasterCalculationRange.deleteSelected();
                break;
        }
        GridCalculationRange_MasterCalculationRange.endUpdate();
    }

}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_MasterCalculationRange) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateCalculationRange_MasterCalculationRange();
            break;
        case 'Exit':
            parent.CloseCurrentTabOnTabStripMenus();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    CurrentPageState_MasterCalculationRange = 'View';
}

function SetActionMode_MasterCalculationRange(state) {
    document.getElementById('ActionMode_MasterCalculationRange').innerHTML = document.getElementById("hf" + state + "_MasterCalculationRange").value;
}

function CharToKeyCode_MasterCalculationRange(str) {
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

function ShowDialogConfirm(confirmState) {
    ConfirmState_MasterCalculationRange = confirmState;
    if (CurrentPageState_MasterCalculationRange == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_MasterCalculationRange').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_MasterCalculationRange').value;
    DialogConfirm.Show();
}

function CalculationRange_onOperationComplete(message, objCalculationRange) {
    CurrentPageState_MasterCalculationRange = 'View';
    showDialog(message[0], message[1], message[2]);
    var CalculationRangeItem = null;
    GridCalculationRange_MasterCalculationRange.beginUpdate();
    switch (objCalculationRange.State) {
        case 'Add':
            CalculationRangeItem = GridCalculationRange_MasterCalculationRange.get_table().addEmptyRow(GridCalculationRange_MasterCalculationRange.get_recordCount());
            CalculationRangeItem.setValue(0, objCalculationRange.ID, false);
            GridCalculationRange_MasterCalculationRange.selectByKey(objCalculationRange.ID, 0, false);
            break;
        case 'Edit':
            GridCalculationRange_MasterCalculationRange.selectByKey(objCalculationRange.ID, 0, false);
            CalculationRangeItem = GridCalculationRange_MasterCalculationRange.getItemFromKey(0, objCalculationRange.ID);
            break;
    }
    if (CurrentPageState_MasterCalculationRange != 'Delete') {
        CalculationRangeItem.setValue(1, objCalculationRange.Name, false);
        CalculationRangeItem.setValue(2, objCalculationRange.Description, false);
    }
    GridCalculationRange_MasterCalculationRange.endUpdate();
}

function CallBack_GridCalculationRange_MasterCalculationRange_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridMasterCalculationRange_MasterCalculationRange').innerHTML = '';
    ShowConnectionError_MasterCalculationRange();
}

function ShowConnectionError_MasterCalculationRange() {
    var error = document.getElementById('hfErrorType_MasterCalculationRange').value;
    var errorBody = document.getElementById('hfConnectionError_MasterCalculationRange').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbCalculationRange_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvCalculationRangeDefinition_iFrame').src = 'MasterCalculationRange.aspx';
}

function tlbItemHelp_TlbCalculationRange_onClick() {
    LoadHelpPage('tlbItemHelp_TlbCalculationRange');    
}

