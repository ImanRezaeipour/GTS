
var CurrentPageState_Departments = 'View';
var ConfirmState_Departments = null;
var ObjDepartment_Departments = null;

function GetBoxesHeaders_Departments() {
    document.getElementById('header_Departments_DepartmentIntroduction').innerHTML = document.getElementById('hfheader_Departments_DepartmentIntroduction').value;
    document.getElementById('header_DepartmentDetails_DepartmentIntroduction').innerHTML = document.getElementById('hfheader_DepartmentDetails_DepartmentIntroduction').value;
}

function trvDepartmentsIntroduction_DepartmentIntroduction_onNodeSelect(sender, e) {
    if (CurrentPageState_Departments != 'Add')
        NavigateDepartment_DepartmentIntroduction(e.get_node());
}

function NavigateDepartment_DepartmentIntroduction(selectedDepartmentNode) {
    if (selectedDepartmentNode != undefined) {
        var departmentObj = selectedDepartmentNode.get_value();
        departmentObj = eval('(' + departmentObj + ')');
        document.getElementById('txtDepartmentCode_DepartmentIntroduction').value = departmentObj.CustomCode;
        document.getElementById('txtDepartmentName_DepartmentIntroduction').value = departmentObj.Name;
    }
}

function chbDepartmentCodeView_DepartmentIntroduction_onclick() {
    CallBack_trvDepartmentsIntroduction_DepartmentIntroduction.callback('DepartmentCodeView', "" + document.getElementById('chbDepartmentCodeView_DepartmentIntroduction').checked + "");
}

function tlbItemNew_TlbDepartmentsIntroduction_onClick() {
    ChangePageState_Departments('Add');
    ClearList_Departments();
    FocusOnFirstElement_Departments();
}

function tlbItemHelp_TlbDepartmentsIntroduction_onClick() {
    LoadHelpPage('tlbItemHelp_TlbDepartmentsIntroduction');
}

function tlbItemEdit_TlbDepartmentsIntroduction_onClick() {
    ChangePageState_Departments('Edit');
    FocusOnFirstElement_Departments();
}

function tlbItemDelete_TlbDepartmentsIntroduction_onClick() {
    ChangePageState_Departments('Delete');
}

function tlbItemSave_TlbDepartmentsIntroduction_onClick() {
    Department_onSave();
}

function Department_onSave() {
    if (CurrentPageState_Departments != 'Delete')
        UpdateDepartment_Departments();
    else
        ShowDialogConfirm('Delete');
}

function UpdateDepartment_Departments() {
    ObjDepartment_Departments = new Object();
    ObjDepartment_Departments.CustomCode = null;
    ObjDepartment_Departments.Name = null;
    ObjDepartment_Departments.SelectedID = '0';
    var SelectedDepartmentNode_Departments = trvDepartmentsIntroduction_DepartmentIntroduction.get_selectedNode();
    if (SelectedDepartmentNode_Departments != undefined)
        ObjDepartment_Departments.SelectedID = SelectedDepartmentNode_Departments.get_id();
    if (CurrentPageState_Departments != 'Delete') {
        ObjDepartment_Departments.CustomCode = document.getElementById('txtDepartmentCode_DepartmentIntroduction').value;
        ObjDepartment_Departments.Name = document.getElementById('txtDepartmentName_DepartmentIntroduction').value;
    }
    UpdateDepartment_DepartmentsPage(CharToKeyCode_Departments(CurrentPageState_Departments), CharToKeyCode_Departments(ObjDepartment_Departments.SelectedID), CharToKeyCode_Departments(ObjDepartment_Departments.CustomCode), CharToKeyCode_Departments(ObjDepartment_Departments.Name));
    DialogWaiting.Show();
}

function Department_OnAfterUpdate(Response) {
    var DepartmentNodeText = ObjDepartment_Departments.Name;
    var DepartmentNodeValue = '{"Name":"' + ObjDepartment_Departments.Name + '","CustomCode":"' + ObjDepartment_Departments.CustomCode + '"}';
    if (document.getElementById('chbDepartmentCodeView_DepartmentIntroduction').checked)
        DepartmentNodeText = ObjDepartment_Departments.CustomCode + ' - ' + DepartmentNodeText;

    trvDepartmentsIntroduction_DepartmentIntroduction.beginUpdate();
    switch (CurrentPageState_Departments) {
        case 'Add':
            var newDepartmentNode = new ComponentArt.Web.UI.TreeViewNode();
            newDepartmentNode.set_text(DepartmentNodeText);
            newDepartmentNode.set_value(DepartmentNodeValue);
            newDepartmentNode.set_id(Response[3]);
            newDepartmentNode.set_imageUrl('Images/TreeView/folder.gif');
            trvDepartmentsIntroduction_DepartmentIntroduction.findNodeById(ObjDepartment_Departments.SelectedID).get_nodes().add(newDepartmentNode);
            trvDepartmentsIntroduction_DepartmentIntroduction.selectNodeById(ObjDepartment_Departments.SelectedID);
            break;
        case 'Edit':
            var selectedDepartmentNode = trvDepartmentsIntroduction_DepartmentIntroduction.findNodeById(Response[3]);
            selectedDepartmentNode.set_text(DepartmentNodeText);
            selectedDepartmentNode.set_value(DepartmentNodeValue);
            trvDepartmentsIntroduction_DepartmentIntroduction.selectNodeById(Response[3]);
            break;
        case 'Delete':
            trvDepartmentsIntroduction_DepartmentIntroduction.findNodeById(ObjDepartment_Departments.SelectedID).remove();
            break;

    }
    trvDepartmentsIntroduction_DepartmentIntroduction.endUpdate();
    if (CurrentPageState_Departments == 'Add')
        trvDepartmentsIntroduction_DepartmentIntroduction.get_selectedNode().expand();
}

function tlbItemCancel_TlbDepartmentsIntroduction_onClick() {
    ChangePageState_Departments('View');
    ClearList_Departments();
}

function tlbItemExit_TlbDepartmentsIntroduction_onClick() {
    ShowDialogConfirm('Exit');
}

function ChangePageState_Departments(state) {
    CurrentPageState_Departments = state;
    SetActionMode_Departments(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbDepartmentsIntroduction.get_items().getItemById('tlbItemNew_TlbDepartmentsIntroduction') != null) {
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemNew_TlbDepartmentsIntroduction').set_enabled(false);
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemNew_TlbDepartmentsIntroduction').set_imageUrl('add_silver.png');
        }
        if (TlbDepartmentsIntroduction.get_items().getItemById('tlbItemEdit_TlbDepartmentsIntroduction') != null) {
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemEdit_TlbDepartmentsIntroduction').set_enabled(false);
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemEdit_TlbDepartmentsIntroduction').set_imageUrl('edit_silver.png');
        }
        if (TlbDepartmentsIntroduction.get_items().getItemById('tlbItemDelete_TlbDepartmentsIntroduction') != null) {
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemDelete_TlbDepartmentsIntroduction').set_enabled(false);
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemDelete_TlbDepartmentsIntroduction').set_imageUrl('remove_silver.png');
        }
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemSave_TlbDepartmentsIntroduction').set_enabled(true);
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemSave_TlbDepartmentsIntroduction').set_imageUrl('save.png');
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemCancel_TlbDepartmentsIntroduction').set_enabled(true);
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemCancel_TlbDepartmentsIntroduction').set_imageUrl('cancel.png');
        document.getElementById('txtDepartmentCode_DepartmentIntroduction').disabled = '';
        document.getElementById('txtDepartmentName_DepartmentIntroduction').disabled = '';
        if (state == 'Edit')
            NavigateDepartment_DepartmentIntroduction(trvDepartmentsIntroduction_DepartmentIntroduction.get_selectedNode());
        if (state == 'Delete')
            Department_onSave();
    }
    if (state == 'View') {
        if (TlbDepartmentsIntroduction.get_items().getItemById('tlbItemNew_TlbDepartmentsIntroduction') != null) {
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemNew_TlbDepartmentsIntroduction').set_enabled(true);
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemNew_TlbDepartmentsIntroduction').set_imageUrl('add.png');
        }
        if (TlbDepartmentsIntroduction.get_items().getItemById('tlbItemEdit_TlbDepartmentsIntroduction') != null) {
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemEdit_TlbDepartmentsIntroduction').set_enabled(true);
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemEdit_TlbDepartmentsIntroduction').set_imageUrl('edit.png');
        }
        if (TlbDepartmentsIntroduction.get_items().getItemById('tlbItemDelete_TlbDepartmentsIntroduction') != null) {
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemDelete_TlbDepartmentsIntroduction').set_enabled(true);
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemDelete_TlbDepartmentsIntroduction').set_imageUrl('remove.png');
        }
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemSave_TlbDepartmentsIntroduction').set_enabled(false);
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemSave_TlbDepartmentsIntroduction').set_imageUrl('save_silver.png');
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemCancel_TlbDepartmentsIntroduction').set_enabled(false);
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemCancel_TlbDepartmentsIntroduction').set_imageUrl('cancel_silver.png');
        document.getElementById('txtDepartmentCode_DepartmentIntroduction').disabled = 'disabled';
        document.getElementById('txtDepartmentName_DepartmentIntroduction').disabled = 'disabled';
    }
}

function SetActionMode_Departments(state) {
    document.getElementById('ActionMode_DepartmentForm').innerHTML = document.getElementById("hf" + state + "_Departments").value;
}

function ClearList_Departments() {
    if (CurrentPageState_Departments != 'Edit') {
        document.getElementById('txtDepartmentCode_DepartmentIntroduction').value = '';
        document.getElementById('txtDepartmentName_DepartmentIntroduction').value = '';
    }
}

function FocusOnFirstElement_Departments() {
    document.getElementById('txtDepartmentCode_DepartmentIntroduction').focus();
}

function CharToKeyCode_Departments(str) {
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

function Fill_trvDepartmentsIntroduction_DepartmentIntroduction() {
    document.getElementById('loadingPanel_trvDepartmentsIntroduction_DepartmentIntroduction').innerHTML = document.getElementById('hfloadingPanel_trvDepartmentsIntroduction_DepartmentIntroduction').value;
    CallBack_trvDepartmentsIntroduction_DepartmentIntroduction.callback('DepartmentCodeView', "" + document.getElementById('chbDepartmentCodeView_DepartmentIntroduction').checked + "");
}

function CallBack_trvDepartmentsIntroduction_DepartmentIntroduction_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Departments').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvDepartmentsIntroduction_DepartmentIntroduction();
    }
}

function Refresh_trvDepartmentsIntroduction_DepartmentIntroduction() {
    Fill_trvDepartmentsIntroduction_DepartmentIntroduction();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Departments = confirmState;
    if (CurrentPageState_Departments == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Departments').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Departments').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Departments) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateDepartment_Departments();
            break;
        case 'Exit':
            ClearList_Departments();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Departments('View');
}

function trvDepartmentsIntroduction_DepartmentIntroduction_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvDepartmentsIntroduction_DepartmentIntroduction').innerHTML = "";
}

function UpdateDepartment_DepartmentsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Departments').value;
            Response[1] = document.getElementById('hfConnectionError_Departments').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            Department_OnAfterUpdate(Response);
            ClearList_Departments();
            ChangePageState_Departments('View');
        }
        else {
            if (CurrentPageState_Departments == 'Delete')
                ChangePageState_Departments('View');
        }
    }
}


function CallBack_trvDepartmentsIntroduction_DepartmentIntroduction_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvDepartmentsIntroduction_DepartmentIntroduction').innerHTML = '';
    ShowConnectionError_DepartmentIntroduction();
}

function ShowConnectionError_DepartmentIntroduction() {
    var error = document.getElementById('hfErrorType_Departments').value;
    var errorBody = document.getElementById('hfConnectionError_Departments').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbDepartmentsIntroduction_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvDepartmentsIntroduction_iFrame').src = 'Departments.aspx';
}


