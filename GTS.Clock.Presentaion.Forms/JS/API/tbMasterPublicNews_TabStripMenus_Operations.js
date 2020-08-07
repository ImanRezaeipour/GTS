
var CurrentPageState_MasterPublicNews = 'View';
var ConfirmState_MasterPublicNews = null;
var ObjMasterPublicNews_MasterPublicNews = null;

function tlbItemNew_TlbMasterPublicNews_onClick() {
    ChangePageState_MasterPublicNews('Add');
    ClearList_MasterPublicNews();
    FocusOnFirstElement_MasterPublicNews();
}

function tlbItemEdit_TlbMasterPublicNews_onClick() {
    ChangePageState_MasterPublicNews('Edit');
    FocusOnFirstElement_MasterPublicNews();
}

function tlbItemDelete_TlbMasterPublicNews_onClick() {
    ChangePageState_MasterPublicNews('Delete');
}

function tlbItemSave_TlbMasterPublicNews_onClick() {
    MasterPublicNews_onSave();
}

function tlbItemCancel_TlbMasterPublicNews_onClick() {
    ChangePageState_MasterPublicNews('View');
    ClearList_MasterPublicNews();
}

function tlbItemExit_TlbMasterPublicNews_onClick() {
    ShowDialogConfirm('Exit');
}

function CallBack_GridMasterPublicNews_MasterPublicNews_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MasterPublicNews').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridMasterPublicNews_MasterPublicNews();
    }
}
function ShowConnectionError_MasterPublicNews() {
    var error = document.getElementById('hfErrorType_MasterPublicNews').value;
    var errorBody = document.getElementById('hfConnectionError_MasterPublicNews').value;
    showDialog(error, errorBody, 'error');
}
function CallBack_GridMasterPublicNews_MasterPublicNews_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridMasterPublicNews_MasterPublicNews').innerHTML = '';
    ShowConnectionError_MasterPublicNews();
}
function GetBoxesHeaders_MasterPublicNews() {
    document.getElementById('header_tblMasterPublicNewsDetails_MasterPublicNewsIntroduction').innerHTML = document.getElementById('hfheader_tblMasterPublicNewsDetails_MasterPublicNewsIntroduction').value;
    document.getElementById('header_MasterPublicNews_MasterPublicNews').innerHTML = document.getElementById('hfheader_MasterPublicNews_MasterPublicNews').value;
}

function SetActionMode_MasterPublicNews(state) {
    document.getElementById('ActionMode_MasterPublicNews').innerHTML = document.getElementById("hf" + state + "_MasterPublicNews").value;
}
function Refresh_GridMasterPublicNews_MasterPublicNews() {
    Fill_GridMasterPublicNews_MasterPublicNews();
}

function Fill_GridMasterPublicNews_MasterPublicNews() {
    document.getElementById('loadingPanel_GridMasterPublicNews_MasterPublicNews').innerHTML = document.getElementById('hfloadingPanel_GridMasterPublicNews_MasterPublicNews').value;
    CallBack_GridMasterPublicNews_MasterPublicNews.callback();
}
function GridMasterPublicNews_MasterPublicNews_onItemSelect(sender, e) {
    if (CurrentPageState_MasterPublicNews != 'Add')
        NavigateMasterPublicNews_MasterPublicNews(e.get_item());
}
function GridMasterPublicNews_MasterPublicNews_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridMasterPublicNews_MasterPublicNews').innerHTML = '';
}
function ChangePageState_MasterPublicNews(state) {
    CurrentPageState_MasterPublicNews = state;
    SetActionMode_MasterPublicNews(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemNew_TlbMasterPublicNewsIntroduction') != null) {
            TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemNew_TlbMasterPublicNewsIntroduction').set_enabled(false);
            TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemNew_TlbMasterPublicNewsIntroduction').set_imageUrl('add_silver.png');
        }
        if (TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemEdit_TlbMasterPublicNewsIntroduction') != null) {
            TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemEdit_TlbMasterPublicNewsIntroduction').set_enabled(false);
            TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemEdit_TlbMasterPublicNewsIntroduction').set_imageUrl('edit_silver.png');
        }
        if (TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemDelete_TlbMasterPublicNewsIntroduction') != null) {
            TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemDelete_TlbMasterPublicNewsIntroduction').set_enabled(false);
            TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemDelete_TlbMasterPublicNewsIntroduction').set_imageUrl('remove_silver.png');
        }
        TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemSave_TlbMasterPublicNewsIntroduction').set_enabled(true);
        TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemSave_TlbMasterPublicNewsIntroduction').set_imageUrl('save.png');
        TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemCancel_TlbMasterPublicNewsIntroduction').set_enabled(true);
        TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemCancel_TlbMasterPublicNewsIntroduction').set_imageUrl('cancel.png');
        document.getElementById('chbMasterPublicNewsActive_MasterPublicNewsIntroduction').disabled = '';
        document.getElementById('txtMasterPublicNewsSubject_MasterPublicNewsIntroduction').disabled = '';
        document.getElementById('txtMasterPublicNewsMessage_MasterPublicNewsIntroduction').disabled = '';
        if (state == 'Edit')
            NavigateMasterPublicNews_MasterPublicNews(GridMasterPublicNews_MasterPublicNews.getSelectedItems()[0]);
        if (state == 'Delete')
            MasterPublicNews_onSave();
    }
    if (state == 'View') {
        if (TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemNew_TlbMasterPublicNewsIntroduction') != null) {
            TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemNew_TlbMasterPublicNewsIntroduction').set_enabled(true);
            TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemNew_TlbMasterPublicNewsIntroduction').set_imageUrl('add.png');
        }
        if (TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemEdit_TlbMasterPublicNewsIntroduction') != null) {
            TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemEdit_TlbMasterPublicNewsIntroduction').set_enabled(true);
            TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemEdit_TlbMasterPublicNewsIntroduction').set_imageUrl('edit.png');
        }
        if (TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemDelete_TlbMasterPublicNewsIntroduction') != null) {
            TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemDelete_TlbMasterPublicNewsIntroduction').set_enabled(true);
            TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemDelete_TlbMasterPublicNewsIntroduction').set_imageUrl('remove.png');
        }
        TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemSave_TlbMasterPublicNewsIntroduction').set_enabled(false);
        TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemSave_TlbMasterPublicNewsIntroduction').set_imageUrl('save_silver.png');
        TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemCancel_TlbMasterPublicNewsIntroduction').set_enabled(false);
        TlbMasterPublicNewsIntroduction.get_items().getItemById('tlbItemCancel_TlbMasterPublicNewsIntroduction').set_imageUrl('cancel_silver.png');
        document.getElementById('chbMasterPublicNewsActive_MasterPublicNewsIntroduction').disabled = 'disabled';
        document.getElementById('txtMasterPublicNewsSubject_MasterPublicNewsIntroduction').disabled = 'disabled';
        document.getElementById('txtMasterPublicNewsMessage_MasterPublicNewsIntroduction').disabled = 'disabled';
    }
}
function ClearList_MasterPublicNews() {
    if (CurrentPageState_MasterPublicNews != 'Edit') {
        document.getElementById('chbMasterPublicNewsActive_MasterPublicNewsIntroduction').checked = false;
        document.getElementById('txtMasterPublicNewsSubject_MasterPublicNewsIntroduction').value = '';
        document.getElementById('txtMasterPublicNewsMessage_MasterPublicNewsIntroduction').value = '';
        GridMasterPublicNews_MasterPublicNews.unSelectAll();
    }
}
function FocusOnFirstElement_MasterPublicNews() {
    document.getElementById('chbMasterPublicNewsActive_MasterPublicNewsIntroduction').focus();
}
function ShowDialogConfirm(confirmState) {
    ConfirmState_MasterPublicNews = confirmState;
    if (CurrentPageState_MasterPublicNews == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_MasterPublicNews').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_MasterPublicNews').value;
    DialogConfirm.Show();
}
function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_MasterPublicNews) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateMasterPublicNews_MasterPublicNews();
            break;
        case 'Exit':
            ClearList_MasterPublicNews();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_MasterPublicNews('View');
}
function tlbItemFormReconstruction_TlbMasterPublicNewsIntroduction_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvMasterPublicNews_iFrame').src = 'MasterPublicNews.aspx';
}
function NavigateMasterPublicNews_MasterPublicNews(selectedMasterPublicNewsItem) {
    if (selectedMasterPublicNewsItem != undefined) {
        document.getElementById('chbMasterPublicNewsActive_MasterPublicNewsIntroduction').checked = selectedMasterPublicNewsItem.getMember('Active').get_value();
        document.getElementById('txtMasterPublicNewsSubject_MasterPublicNewsIntroduction').value = selectedMasterPublicNewsItem.getMember('Subject').get_text();
        document.getElementById('txtMasterPublicNewsMessage_MasterPublicNewsIntroduction').value = selectedMasterPublicNewsItem.getMember('Message').get_text();
    }
}
function MasterPublicNews_onSave() {
    if (CurrentPageState_MasterPublicNews != 'Delete')
        UpdateMasterPublicNews_MasterPublicNews();
    else
        ShowDialogConfirm('Delete');
}
function MasterPublicNews_OnAfterUpdate(Response) {
    if (ObjMasterPublicNews_MasterPublicNews != null) {
        var MasterPublicNewsActive = ObjMasterPublicNews_MasterPublicNews.Active;
        var MasterPublicNewsSubject = ObjMasterPublicNews_MasterPublicNews.Subject;
        var MasterPublicNewsMessage = ObjMasterPublicNews_MasterPublicNews.Message;

        var MasterPublicNewsItem = null;
        GridMasterPublicNews_MasterPublicNews.beginUpdate();
        switch (CurrentPageState_MasterPublicNews) {
            case 'Add':
                MasterPublicNewsItem = GridMasterPublicNews_MasterPublicNews.get_table().addEmptyRow(GridMasterPublicNews_MasterPublicNews.get_recordCount());
                MasterPublicNewsItem.setValue(0, Response[3], false);
                GridMasterPublicNews_MasterPublicNews.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridMasterPublicNews_MasterPublicNews.selectByKey(Response[3], 0, false);
                MasterPublicNewsItem = GridMasterPublicNews_MasterPublicNews.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridMasterPublicNews_MasterPublicNews.selectByKey(ObjMasterPublicNews_MasterPublicNews.ID, 0, false);
                GridMasterPublicNews_MasterPublicNews.deleteSelected();
                break;
        }
        if (CurrentPageState_MasterPublicNews != 'Delete') {
            MasterPublicNewsItem.setValue(1, MasterPublicNewsActive, false);
            MasterPublicNewsItem.setValue(2, MasterPublicNewsSubject, false);
            MasterPublicNewsItem.setValue(3, MasterPublicNewsMessage, false);
        }
        GridMasterPublicNews_MasterPublicNews.endUpdate();
    }
}
function UpdateMasterPublicNews_MasterPublicNews() {
    ObjMasterPublicNews_MasterPublicNews = new Object();
    ObjMasterPublicNews_MasterPublicNews.Active = false;
    ObjMasterPublicNews_MasterPublicNews.Subject = null;
    ObjMasterPublicNews_MasterPublicNews.Message = null;
    ObjMasterPublicNews_MasterPublicNews.ID = '0';
    var SelectedItems_GridMasterPublicNews_MasterPublicNews = GridMasterPublicNews_MasterPublicNews.getSelectedItems();
    if (SelectedItems_GridMasterPublicNews_MasterPublicNews.length > 0)
        ObjMasterPublicNews_MasterPublicNews.ID = SelectedItems_GridMasterPublicNews_MasterPublicNews[0].getMember("ID").get_text();

    if (CurrentPageState_MasterPublicNews != 'Delete') {
        ObjMasterPublicNews_MasterPublicNews.Active = document.getElementById('chbMasterPublicNewsActive_MasterPublicNewsIntroduction').checked;
        ObjMasterPublicNews_MasterPublicNews.Subject = document.getElementById('txtMasterPublicNewsSubject_MasterPublicNewsIntroduction').value;
        ObjMasterPublicNews_MasterPublicNews.Message = document.getElementById('txtMasterPublicNewsMessage_MasterPublicNewsIntroduction').value;
    }
    UpdateMasterPublicNews_MasterPublicNewsPage(CharToKeyCode_MasterPublicNews(CurrentPageState_MasterPublicNews), CharToKeyCode_MasterPublicNews(ObjMasterPublicNews_MasterPublicNews.ID), CharToKeyCode_MasterPublicNews(ObjMasterPublicNews_MasterPublicNews.Active.toString()), CharToKeyCode_MasterPublicNews(ObjMasterPublicNews_MasterPublicNews.Subject), CharToKeyCode_MasterPublicNews(ObjMasterPublicNews_MasterPublicNews.Message));
    DialogWaiting.Show();
}
function UpdateMasterPublicNews_MasterPublicNewsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_MasterPublicNews').value;
            Response[1] = document.getElementById('hfConnectionError_MasterPublicNews').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_MasterPublicNews();
            MasterPublicNews_OnAfterUpdate(Response);
            ChangePageState_MasterPublicNews('View');
        }
        else {
            if (CurrentPageState_MasterPublicNews == 'Delete')
                ChangePageState_MasterPublicNews('View');
        }
    }
}
function CharToKeyCode_MasterPublicNews(str) {
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

function tlbItemHelp_TlbMasterPublicNewsIntroduction_onClick() {
    LoadHelpPage('tlbItemHelp_TlbMasterPublicNewsIntroduction');    
    
}
