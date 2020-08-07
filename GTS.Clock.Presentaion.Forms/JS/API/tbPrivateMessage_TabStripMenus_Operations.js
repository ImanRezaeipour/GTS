var CurrentPageState_PrivateMessage = 'View';
var ConfirmState_PrivateMessage = null;
var ObjPrivateMessage_PrivateMessage = null;
var CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage = 0;
var CurrentPageIndexSend_GridPrivateMessage_PrivateMessage = 0;
var CurrentPageState_PrivateMessage = 'View';
var ObjPrivateMessageReceive_PrivateMessage = null;
var ObjPrivateMessageSend_PrivateMessage = null;
var SelectedGrid_PrivateMessage = null;
var SelectedGridRow_PrivareMessage = null;

function CallBack_GridPrivateMessageReceive_PrivateMessage_onCallbackComplete(sender, e) {
    var errorReceive = document.getElementById('errorHiddenFieldReceive_PrivateMessage').value;
    GridPrivateMessageReceive_PrivateMessage.render();
    if (errorReceive != "") {
        var errorParts = eval('(' + errorReceive + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridPrivateMessageReceive_PrivateMessage();
    }
    Changefooter_GridPrivateMessageReceive_PrivateMessage();

}

function CallBack_GridPrivateMessageSend_PrivateMessage_onCallbackComplete(sender, e) {
    var errorSend = document.getElementById('errorHiddenFieldSend_PrivateMessage').value;
    GridPrivateMessageSend_PrivateMessage.render();
    if (errorSend != "") {
        var errorParts = eval('(' + errorSend + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridPrivateMessageSend_PrivateMessage();
    }
    Changefooter_GridPrivateMessageSend_PrivateMessage();
}

function CallBack_GridPrivateMessageReceive_PrivateMessage_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridPrivateMessageReceive_PrivateMessage').innerHTML = '';
    ShowConnectionError_PrivateMessage();
}

function CallBack_GridPrivateMessageSend_PrivateMessage_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridPrivateMessageSend_PrivateMessage').innerHTML = '';
    ShowConnectionError_PrivateMessage();
}

function ShowConnectionError_PrivateMessage() {
    var error = document.getElementById('hfErrorType_PrivateMessage').value;
    var errorBody = document.getElementById('hfConnectionError_PrivateMessage').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemDelete_TlbPrivateMessage_onClick() {
    ChangePageState_PrivateMessage('Delete');
}

function tlbItemSend_TlbPrivateMessage_onClick() {
    ShowDialogSendPrivateMessage('Direct');
}

function tlbItemReplyMessage_PrivateMessageIntroduction_onClick() {
    ShowDialogSendPrivateMessage('Reply');
}

function ShowDialogSendPrivateMessage(state) {
    var ObjDialogSendPrivateMessage = new Object();
    switch (state) {
        case 'Direct':
            ObjDialogSendPrivateMessage.State = state;
            parent.DialogSendPrivateMessage.set_value(ObjDialogSendPrivateMessage);
            parent.DialogSendPrivateMessage.show();
            break;
        case 'Reply':
            if (GridPrivateMessageReceive_PrivateMessage.getSelectedItems().length > 0) {
                ObjDialogSendPrivateMessage.RecieveMessageID = GridPrivateMessageReceive_PrivateMessage.getSelectedItems()[0].getMember('ID').get_text();
                ObjDialogSendPrivateMessage.ReceveMessageTarget = GridPrivateMessageReceive_PrivateMessage.getSelectedItems()[0].getMember('FromPerson.Name').get_text();
                ObjDialogSendPrivateMessage.ReceveMessageTargetID = GridPrivateMessageReceive_PrivateMessage.getSelectedItems()[0].getMember('FromPersonID').get_text();
                ObjDialogSendPrivateMessage.ReceveMessageSubject = GridPrivateMessageReceive_PrivateMessage.getSelectedItems()[0].getMember('Subject').get_text();
                ObjDialogSendPrivateMessage.State = state;
                parent.DialogSendPrivateMessage.set_value(ObjDialogSendPrivateMessage);
                parent.DialogSendPrivateMessage.show();
            }
            break;
    }
}

function tlbItemExit_TlbPrivateMessage_onClick() {
    ShowDialogConfirm('Exit');
}

function GridPrivateMessageReceive_PrivateMessage_onItemSelect(sender, e) {
    NavigateMasterPrivateMessageReceive_PrivateMessage(e.get_item());
    SelectedGrid_PrivateMessage = "Receive";
}

function GridPrivateMessageSend_PrivateMessage_onItemSelect(sender, e) {
    NavigateMasterPrivateMessageSend_PrivateMessage(e.get_item());
    SelectedGrid_PrivateMessage = "Send";
}

function NavigateMasterPrivateMessageReceive_PrivateMessage(selectedPrivateMessageReceive) {
    if (selectedPrivateMessageReceive != undefined) {
        document.getElementById('txtPrivateMessageReceiveFrom_PrivateMessageIntroduction').value = selectedPrivateMessageReceive.getMember('FromPerson.Name').get_text();
        document.getElementById('txtPrivateMessageReceiveSubject_PrivateMessageIntroduction').value = selectedPrivateMessageReceive.getMember('Subject').get_text();
        document.getElementById('txtPrivateMessageReceiveMessage_PrivateMessageIntroduction').value = selectedPrivateMessageReceive.getMember('Message').get_text();
        SelectedGridRow_PrivareMessage = selectedPrivateMessageReceive;
        NavigateRead_PrivateMessageReceive_PrivateMessagePage(CharToKeyCode_PrivateMessage(selectedPrivateMessageReceive.getMember('ID').get_text()));
        DialogWaiting.Show();
    }
}

function NavigateRead_PrivateMessageReceive_PrivateMessagePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_PrivateMessage').value;
            Response[1] = document.getElementById('hfConnectionError_PrivateMessage').value;
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
        }
        if (RetMessage[2] == 'success') {
            PrivateMessageReceive_OnAfterNavigate();
        }
    }
}

function PrivateMessageReceive_OnAfterNavigate() {
    GridPrivateMessageReceive_PrivateMessage.beginUpdate();
    SelectedGridRow_PrivareMessage.SetValue(3, true);
    SetImage_clmnPrivateMessageStatus_GridPrivateMessage(true);
    GridPrivateMessageReceive_PrivateMessage.endUpdate();
}

function NavigateMasterPrivateMessageSend_PrivateMessage(selectedPrivateMessageSend) {
    if (selectedPrivateMessageSend != undefined) {
        document.getElementById('txtPrivateMessageSendTo_PrivateMessageIntroduction').value = selectedPrivateMessageSend.getMember('ToPerson.Name').get_text();
        document.getElementById('txtPrivateMessageSendSubject_PrivateMessageIntroduction').value = selectedPrivateMessageSend.getMember('Subject').get_text();
        document.getElementById('txtPrivateMessageSendMessage_PrivateMessageIntroduction').value = selectedPrivateMessageSend.getMember('Message').get_text();
        SelectedGridRow_PrivareMessage = selectedPrivateMessageSend;
    }
}

function NavigateRead_PrivateMessageSend_PrivateMessagePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_PrivateMessage').value;
            Response[1] = document.getElementById('hfConnectionError_PrivateMessage').value;
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
        }
        if (RetMessage[2] == 'success')
            PrivateMessageSend_OnAfterNavigate();
    }
}

function PrivateMessageSend_OnAfterNavigate() {
    GridPrivateMessageSend_PrivateMessage.beginUpdate();
    SetImage_clmnPrivateMessageStatus_GridPrivateMessage(true);
    GridPrivateMessageSend_PrivateMessage.endUpdate();
}

function TabStripPrivateMessageMenus_onTabSelect(sender, e) {
    if (e.get_tab().get_id() == "tbReceive_TabStripMenus")
        MultiPagePrivateMessageMenus.findPageById('pgvPrivateMessageReceive').Show();
    if (e.get_tab().get_id() == "tbSend_TabStripMenus")
        MultiPagePrivateMessageMenus.findPageById('pgvPrivateMessageSend').Show();
}

function GridPrivateMessageReceive_PrivateMessage_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridPrivateMessageReceive_PrivateMessage').innerHTML = '';
}

function GridPrivateMessageSend_PrivateMessage_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridPrivateMessageSend_PrivateMessage').innerHTML = '';
}

function ClearListReceive_PrivateMessage() {
    if (CurrentPageState_PrivateMessage != 'Edit') {
        document.getElementById('txtPrivateMessageReceiveFrom_PrivateMessageIntroduction').value = '';
        document.getElementById('txtPrivateMessageReceiveSubject_PrivateMessageIntroduction').value = '';
        document.getElementById('txtPrivateMessageReceiveMessage_PrivateMessageIntroduction').value = '';
        GridPrivateMessageReceive_PrivateMessage.unSelectAll();
        GridPrivateMessageSend_PrivateMessage.unSelectAll();
    }
}

function ClearListSend_PrivateMessage() {
    if (CurrentPageState_PrivateMessage != 'Edit') {
        document.getElementById('txtPrivateMessageSendTo_PrivateMessageIntroduction').value = '';
        document.getElementById('txtPrivateMessageSendSubject_PrivateMessageIntroduction').value = '';
        document.getElementById('txtPrivateMessageSendMessage_PrivateMessageIntroduction').value = '';
        GridPrivateMessageReceive_PrivateMessage.unSelectAll();
        GridPrivateMessageSend_PrivateMessage.unSelectAll();
    }
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_PrivateMessage = confirmState;
    if (CurrentPageState_PrivateMessage == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_PrivateMessage').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PrivateMessage').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_PrivateMessage) {
        case 'Delete':
            DialogConfirm.Close();
            if (SelectedGrid_PrivateMessage == "Receive") {
                UpdatePrivateMessageReceive_PrivateMessage();
            }
            if (SelectedGrid_PrivateMessage == "Send") {
                UpdatePrivateMessageSend_PrivateMessage();
            }
            break;
        case 'Exit':
            ClearListReceive_PrivateMessage();
            ClearListSend_PrivateMessage();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
    }
}
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_PrivateMessage('View');
}
function tlbItemFormReconstruction_TlbPrivateMessageIntroduction_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvPrivateMessage_iFrame').src = 'PrivateMessage.aspx';
}

function GetBoxesHeadersReceive_PrivateMessage() {
    document.getElementById('header_tblPrivateMessageReceiveDetails_PrivateMessageIntroduction').innerHTML = document.getElementById('hfheader_tblPrivateMessageDetails_PrivateMessageIntroduction').value;
    document.getElementById('header_PrivateMessage_PrivateMessage').innerHTML = document.getElementById('hfheader_PrivateMessage_PrivateMessage').value;
    document.getElementById('footer_GridPrivateMessageReceive_PrivateMessage').innerHTML = document.getElementById('hffooter_GridUsers_Users').value;
}

function GetBoxesHeadersSend_PrivateMessage() {
    document.getElementById('header_tblPrivateMessageSendDetails_PrivateMessageIntroduction').innerHTML = document.getElementById('hfheader_tblPrivateMessageDetails_PrivateMessageIntroduction').value;
    document.getElementById('header_PrivateMessage_PrivateMessage').innerHTML = document.getElementById('hfheader_PrivateMessage_PrivateMessage').value;
    document.getElementById('footer_GridPrivateMessageSend_PrivateMessage').innerHTML = document.getElementById('hffooter_GridUsers_Users').value;
}

function PrivateMessage_onSave() {
    ShowDialogConfirm('Delete');
}

function ChangePageState_PrivateMessage(state) {
    CurrentPageState_PrivateMessage = state;
    SetActionMode_PrivateMessage(state);
    if (state == 'Delete') {
        PrivateMessage_onSave();
    }
    if (state == 'View') {
        if (TlbPrivateMessageIntroduction.get_items().getItemById('tlbItemDelete_TlbPrivateMessageIntroduction') != null) {
            TlbPrivateMessageIntroduction.get_items().getItemById('tlbItemDelete_TlbPrivateMessageIntroduction').set_enabled(true);
            TlbPrivateMessageIntroduction.get_items().getItemById('tlbItemDelete_TlbPrivateMessageIntroduction').set_imageUrl('remove.png');
        }
        document.getElementById('txtPrivateMessageSendTo_PrivateMessageIntroduction').disabled = 'disabled';
        document.getElementById('txtPrivateMessageSendSubject_PrivateMessageIntroduction').disabled = 'disabled';
        document.getElementById('txtPrivateMessageSendMessage_PrivateMessageIntroduction').disabled = 'disabled';
        document.getElementById('txtPrivateMessageReceiveFrom_PrivateMessageIntroduction').disabled = 'disabled';
        document.getElementById('txtPrivateMessageReceiveSubject_PrivateMessageIntroduction').disabled = 'disabled';
        document.getElementById('txtPrivateMessageReceiveMessage_PrivateMessageIntroduction').disabled = 'disabled';
    }
}

function GetBoxesHeadersReceive_PrivateMessage() {
    document.getElementById('header_tblPrivateMessageReceiveDetails_PrivateMessageIntroduction').innerHTML = document.getElementById('hfheader_tblPrivateMessageDetails_PrivateMessageIntroduction').value;
    document.getElementById('footer_GridPrivateMessageReceive_PrivateMessage').innerHTML = document.getElementById('hffooter_GridPrivateMessageReceive_PrivateMessage').value;
}

function GetBoxesHeadersSend_PrivateMessage() {
    document.getElementById('header_tblPrivateMessageSendDetails_PrivateMessageIntroduction').innerHTML = document.getElementById('hfheader_tblPrivateMessageDetails_PrivateMessageIntroduction').value;
    document.getElementById('footer_GridPrivateMessageSend_PrivateMessage').innerHTML = document.getElementById('hffooter_GridPrivateMessageSend_PrivateMessage').value;
}

function SetActionMode_PrivateMessage(state) {
    document.getElementById('ActionMode_PrivateMessage').innerHTML = document.getElementById("hf" + state + "_PrivateMessage").value;
}

function SetPageIndexReceive_PrivateMessage(pageIndex) {
    CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage = pageIndex;
    Fill_cmbPersonnel_PrivateMessage(pageIndex);
}

function CheckOut_chbCheckAll_PrivateMessageReceive() {
    document.getElementById('chbSelectAllPrivateMessageReceive_PrivateMessage').checked = false;
}

function CheckOut_chbCheckAll_PrivateMessageSend() {
    document.getElementById('chbSelectAllPrivateMessageSend_PrivateMessage').checked = false;
}

function tlbItemRefresh_TlbPaging_GridPrivateMessageReceive_PrivateMessage_onClick() {
    ChangeLoadState_GridPrivateMessageReceive_PrivateMessage('Normal');
    CheckOut_chbCheckAll_PrivateMessageReceive();
}

function tlbItemFirst_TlbPaging_GridPrivateMessageReceive_PrivateMessage_onClick() {
    SetPageIndex_GridPrivateMessageReceive_PrivateMessage(0);
    CheckOut_chbCheckAll_PrivateMessageReceive();
}

function tlbItemBefore_TlbPaging_GridPrivateMessageReceive_PrivateMessage_onClick() {
    if (CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage != 0) {
        CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage = CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage - 1;
        SetPageIndex_GridPrivateMessageReceive_PrivateMessage(CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage);
    }
    CheckOut_chbCheckAll_PrivateMessageReceive();
}

function tlbItemNext_TlbPaging_GridPrivateMessageReceive_PrivateMessage_onClick() {
    if (CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage < parseInt(document.getElementById('hfPrivateMessageReceivePageCount_PrivateMessage').value) - 1) {
        CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage = CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage + 1;
        SetPageIndex_GridPrivateMessageReceive_PrivateMessage(CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage);
    }
    CheckOut_chbCheckAll_PrivateMessageReceive();
}

function tlbItemLast_TlbPaging_GridPrivateMessageReceive_PrivateMessage_onClick() {
    SetPageIndex_GridPrivateMessageReceive_PrivateMessage(parseInt(document.getElementById('hfPrivateMessageReceivePageCount_PrivateMessage').value) - 1);
    CheckOut_chbCheckAll_PrivateMessageReceive();
}

function tlbItemRefresh_TlbPaging_GridPrivateMessageSend_PrivateMessage_onClick() {
    RefreshGridPrivateMessageSend_PrivateMessage();
}

function RefreshGridPrivateMessageSend_PrivateMessage() {
    ChangeLoadState_GridPrivateMessageSend_PrivateMessage('Normal');
    CheckOut_chbCheckAll_PrivateMessageSend();
}

function tlbItemFirst_TlbPaging_GridPrivateMessageSend_PrivateMessage_onClick() {
    SetPageIndex_GridPrivateMessageSend_PrivateMessage(0);
    CheckOut_chbCheckAll_PrivateMessageSend();
}

function tlbItemBefore_TlbPaging_GridPrivateMessageSend_PrivateMessage_onClick() {
    if (CurrentPageIndexSend_GridPrivateMessage_PrivateMessage != 0) {
        CurrentPageIndexSend_GridPrivateMessage_PrivateMessage = CurrentPageIndexSend_GridPrivateMessage_PrivateMessage - 1;
        SetPageIndex_GridPrivateMessageSend_PrivateMessage(CurrentPageIndexSend_GridPrivateMessage_PrivateMessage);
    }
    CheckOut_chbCheckAll_PrivateMessageSend();
}

function tlbItemNext_TlbPaging_GridPrivateMessageSend_PrivateMessage_onClick() {
    if (CurrentPageIndexSend_GridPrivateMessage_PrivateMessage < parseInt(document.getElementById('hfPrivateMessageSendPageCount_PrivateMessage').value) - 1) {
        CurrentPageIndexSend_GridPrivateMessage_PrivateMessage = CurrentPageIndexSend_GridPrivateMessage_PrivateMessage + 1;
        SetPageIndex_GridPrivateMessageSend_PrivateMessage(CurrentPageIndexSend_GridPrivateMessage_PrivateMessage);
    }
    CheckOut_chbCheckAll_PrivateMessageSend();
}

function tlbItemLast_TlbPaging_GridPrivateMessageSend_PrivateMessage_onClick() {
    SetPageIndex_GridPrivateMessageSend_PrivateMessage(parseInt(document.getElementById('hfPrivateMessageSendPageCount_PrivateMessage').value) - 1);
    CheckOut_chbCheckAll_PrivateMessageSend();
}

function ChangeLoadState_GridPrivateMessageReceive_PrivateMessage(state) {
    LoadState_Users = state;
    SetPageIndex_GridPrivateMessageReceive_PrivateMessage(0);
}

function ChangeLoadState_GridPrivateMessageSend_PrivateMessage(state) {
    LoadState_Users = state;
    SetPageIndex_GridPrivateMessageSend_PrivateMessage(0);
}

function SetPageIndex_GridPrivateMessageSend_PrivateMessage(pageIndex) {
    CurrentPageIndexSend_GridPrivateMessage_PrivateMessage = pageIndex;
    Fill_GridPrivateMessageSend_PrivateMessage(pageIndex);
}

function Fill_GridPrivateMessageSend_PrivateMessage(pageIndex) {
    document.getElementById('loadingPanel_GridPrivateMessageSend_PrivateMessage').innerHTML = document.getElementById('hfloadingPanel_GridPrivateMessage_PrivateMessage').value;
    var pageSize = parseInt(document.getElementById('hfPrivateMessageSendPageSize_PrivateMessage').value);
    CallBack_GridPrivateMessageSend_PrivateMessage.callback(CharToKeyCode_PrivateMessage(pageSize.toString()), CharToKeyCode_PrivateMessage(pageIndex.toString()));
}

function SelectTabDefault() {
    TabStripPrivateMessageMenus.selectTabById('tbReceive_TabStripMenus');
}

function SetPageIndex_GridPrivateMessageReceive_PrivateMessage(pageIndex) {
    CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage = pageIndex;
    Fill_GridPrivateMessageReceive_PrivateMessage(pageIndex);
}

function Fill_GridPrivateMessageReceive_PrivateMessage(pageIndex) {
    document.getElementById('loadingPanel_GridPrivateMessageReceive_PrivateMessage').innerHTML = document.getElementById('hfloadingPanel_GridPrivateMessage_PrivateMessage').value;
    var pageSize = parseInt(document.getElementById('hfPrivateMessageReceivePageSize_PrivateMessage').value);

    CallBack_GridPrivateMessageReceive_PrivateMessage.callback(CharToKeyCode_PrivateMessage(pageSize.toString()), CharToKeyCode_PrivateMessage(pageIndex.toString()));
}

function CharToKeyCode_PrivateMessage(str) {
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

function selectAllItem_GridPrivateMessageReceive_PrivateMessage() {
    var checkAll = document.getElementById('chbSelectAllPrivateMessageReceive_PrivateMessage').checked;
    var gridItem;
    var itemIndex = 0;
    GridPrivateMessageReceive_PrivateMessage.beginUpdate();
    while (gridItem = GridPrivateMessageReceive_PrivateMessage.get_table().getRow(itemIndex)) {
        gridItem.SetValue(2, checkAll);
        itemIndex++;
    }
    GridPrivateMessageReceive_PrivateMessage.endUpdate();
}

function selectAllItem_GridPrivateMessageSend_PrivateMessage() {
    var checkAll = document.getElementById('chbSelectAllPrivateMessageSend_PrivateMessage').checked;
    var gridItem;
    var itemIndex = 0;
    GridPrivateMessageSend_PrivateMessage.beginUpdate();
    while (gridItem = GridPrivateMessageSend_PrivateMessage.get_table().getRow(itemIndex)) {
        gridItem.SetValue(2, checkAll);
        itemIndex++;
    }
    GridPrivateMessageSend_PrivateMessage.endUpdate();
}

function SetImage_clmnPrivateMessageStatus_GridPrivateMessage(type) {
    var nodeTypeImage = '';
    switch (type) {
        case true:
            nodeTypeImage = 'Images/Grid/mail_Read.png';
            break;
        case false:
            nodeTypeImage = 'Images/Grid/mail_UnRead.png';
            break;
    }
    return nodeTypeImage;
}

function Changefooter_GridPrivateMessageReceive_PrivateMessage() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridPrivateMessageReceive_PrivateMessage').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfPrivateMessageReceiveCount_PrivateMessage').value) > 0 ? CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfPrivateMessageReceivePageCount_PrivateMessage').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridPrivateMessageReceive_PrivateMessage').innerHTML = retfooterVal;
}

function mod(a, b) {
    return a - (b * Math.floor(a / b));
}

function UpdatePrivateMessageReceive_PrivateMessage() {
    ObjPrivateMessageReceive_PrivateMessage = new Array();
    var gridItem;
    var itemGridIndex = 0;
    var itemArrayIndex = 0;
    while (gridItem = GridPrivateMessageReceive_PrivateMessage.get_table().getRow(itemGridIndex)) {
        if (gridItem.getMember('Select').get_value() == true) {
            ObjPrivateMessageReceive_PrivateMessage[itemArrayIndex] = gridItem.getMember('ID').get_value();
            itemArrayIndex++;
        }
        itemGridIndex++;
    }
    var stringArrayItem = ObjPrivateMessageReceive_PrivateMessage.join(',');
    UpdatePrivateMessageReceive_PrivateMessagePage(CharToKeyCode_PrivateMessage(CurrentPageState_PrivateMessage), CharToKeyCode_PrivateMessage(stringArrayItem));
    DialogWaiting.Show();
}

function UpdatePrivateMessageReceive_PrivateMessagePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_PrivateMessage').value;
            Response[1] = document.getElementById('hfConnectionError_PrivateMessage').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearPrivateMessageReceiveList_PrivateMessage();
            PrivateMessageReceive_OnAfterUpdate(Response);
            ChangePageState_PrivateMessage('View');
        }
        else {
            if (CurrentPageState_PrivateMessage == 'Delete') {
                ChangePageState_PrivateMessage('View');
            }
        }
    }
    CheckOut_chbCheckAll_PrivateMessageReceive();
}

function ClearPrivateMessageReceiveList_PrivateMessage() {
    document.getElementById('txtPrivateMessageReceiveFrom_PrivateMessageIntroduction').value = '';
    document.getElementById('txtPrivateMessageReceiveSubject_PrivateMessageIntroduction').value = '';
    document.getElementById('txtPrivateMessageReceiveMessage_PrivateMessageIntroduction').value = '';
}

function PrivateMessageReceive_OnAfterUpdate(Response) {
    if (ObjPrivateMessageReceive_PrivateMessage != null) {
        GridPrivateMessageReceive_PrivateMessage.beginUpdate();
        switch (CurrentPageState_PrivateMessage) {
            case 'Delete':
                for (var i = 0; i < ObjPrivateMessageReceive_PrivateMessage.length; i++) {
                    GridPrivateMessageReceive_PrivateMessage.selectByKey(ObjPrivateMessageReceive_PrivateMessage[i], 0, false);
                    GridPrivateMessageReceive_PrivateMessage.deleteSelected();
                }
                UpdateFeatures_GridPrivateMessageReceive_PrivateMessage();
                break;
        }
        GridPrivateMessageReceive_PrivateMessage.endUpdate();
    }
}

function UpdateFeatures_GridPrivateMessageReceive_PrivateMessage() {
    var PrivateMessageCount = parseInt(document.getElementById('hfPrivateMessageReceiveCount_PrivateMessage').value);
    var PrivateMessagePageCount = parseInt(document.getElementById('hfPrivateMessageReceivePageCount_PrivateMessage').value);
    var PrivateMessagePageSize = parseInt(document.getElementById('hfPrivateMessageReceivePageSize_PrivateMessage').value);
    var Lag = 0;
    switch (CurrentPageState_PrivateMessage) {
        case 'Delete':
            Lag = Lag - 1;
            break;
    }
    if ((PrivateMessageCount > 0 && CurrentPageState_PrivateMessage == 'Delete')) {
        PrivateMessageCount = PrivateMessageCount + Lag;
        var divRem = mod(PrivateMessageCount, PrivateMessagePageSize);
        switch (CurrentPageState_PrivateMessage) {
            case 'Delete':
                if (divRem == 0) {
                    UsersPageCount = UsersPageCount + Lag;
                    if (CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage == PrivateMessagePageCount) 
                        CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage = CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage + Lag >= 0 ? CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage + Lag : 0;
                }
                break;
        }
        SetPageIndex_GridPrivateMessageReceive_PrivateMessage(CurrentPageIndexReceive_GridPrivateMessage_PrivateMessage);
        document.getElementById('hfPrivateMessageReceiveCount_PrivateMessage').value = PrivateMessageCount.toString();
        document.getElementById('hfPrivateMessageReceivePageCount_PrivateMessage').value = PrivateMessagePageCount.toString();
        Changefooter_GridPrivateMessageReceive_PrivateMessage();
    }
}

function Changefooter_GridPrivateMessageSend_PrivateMessage() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridPrivateMessageSend_PrivateMessage').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfPrivateMessageSendCount_PrivateMessage').value) > 0 ? CurrentPageIndexSend_GridPrivateMessage_PrivateMessage + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfPrivateMessageSendPageCount_PrivateMessage').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridPrivateMessageSend_PrivateMessage').innerHTML = retfooterVal;
}

function UpdatePrivateMessageSend_PrivateMessage() {
    ObjPrivateMessageSend_PrivateMessage = new Array();
    var gridItem;
    var itemGridIndex = 0;
    var itemArrayIndex = 0;
    while (gridItem = GridPrivateMessageSend_PrivateMessage.get_table().getRow(itemGridIndex)) {
        if (gridItem.getMember('Select').get_value() == true) {
            ObjPrivateMessageSend_PrivateMessage[itemArrayIndex] = gridItem.getMember('ID').get_value();
            itemArrayIndex++;
        }
        itemGridIndex++;
    }
    var stringArrayItem = ObjPrivateMessageSend_PrivateMessage.join(',');
    UpdatePrivateMessageSend_PrivateMessagePage(CharToKeyCode_PrivateMessage(CurrentPageState_PrivateMessage), CharToKeyCode_PrivateMessage(stringArrayItem));
    DialogWaiting.Show();
}

function UpdatePrivateMessageSend_PrivateMessagePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_PrivateMessage').value;
            Response[1] = document.getElementById('hfConnectionError_PrivateMessage').value;
        }
        showDialog(RetMessage[0], RetMessage[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearPrivateMessageSendList_PrivateMessage();
            PrivateMessageSend_OnAfterUpdate(Response);
            ChangePageState_PrivateMessage('View');

        }

    }
    CheckOut_chbCheckAll_PrivateMessageSend();
}

function ClearPrivateMessageSendList_PrivateMessage() {
    document.getElementById('txtPrivateMessageSendTo_PrivateMessageIntroduction').value = '';
    document.getElementById('txtPrivateMessageSendSubject_PrivateMessageIntroduction').value = '';
    document.getElementById('txtPrivateMessageSendMessage_PrivateMessageIntroduction').value = '';
}

function PrivateMessageSend_OnAfterUpdate(Response) {
    if (ObjPrivateMessageSend_PrivateMessage != null) {
        GridPrivateMessageSend_PrivateMessage.beginUpdate();
        switch (CurrentPageState_PrivateMessage) {
            case 'Delete':
                for (var i = 0; i < ObjPrivateMessageSend_PrivateMessage.length; i++) {
                    GridPrivateMessageSend_PrivateMessage.selectByKey(ObjPrivateMessageSend_PrivateMessage[i], 0, false);
                    GridPrivateMessageSend_PrivateMessage.deleteSelected();
                }
                UpdateFeatures_GridPrivateMessageSend_PrivateMessage();
                break;
        }
        GridPrivateMessageSend_PrivateMessage.endUpdate();
    }
}

function UpdateFeatures_GridPrivateMessageSend_PrivateMessage() {
    var PrivateMessageCount = parseInt(document.getElementById('hfPrivateMessageSendCount_PrivateMessage').value);
    var PrivateMessagePageCount = parseInt(document.getElementById('hfPrivateMessageSendPageCount_PrivateMessage').value);
    var PrivateMessagePageSize = parseInt(document.getElementById('hfPrivateMessageSendPageSize_PrivateMessage').value);
    var Lag = 0;
    switch (CurrentPageState_PrivateMessage) {
        case 'Delete':
            Lag = Lag - 1;
            break;
    }
    if ((PrivateMessageCount > 0 && CurrentPageState_PrivateMessage == 'Delete')) {
        PrivateMessageCount = PrivateMessageCount + Lag;
        var divRem = mod(PrivateMessageCount, PrivateMessagePageSize);
        switch (CurrentPageState_PrivateMessage) {
            case 'Delete':
                if (divRem == 0) {
                    UsersPageCount = UsersPageCount + Lag;
                    if (CurrentPageIndexSend_GridPrivateMessage_PrivateMessage == PrivateMessagePageCount) {
                        CurrentPageIndexSend_GridPrivateMessage_PrivateMessage = CurrentPageIndexSend_GridPrivateMessage_PrivateMessage + Lag >= 0 ? CurrentPageIndexSend_GridPrivateMessage_PrivateMessage + Lag : 0;
                    }
                }
                break;
        }
        SetPageIndex_GridPrivateMessageSend_PrivateMessage(CurrentPageIndexSend_GridPrivateMessage_PrivateMessage);
        document.getElementById('hfPrivateMessageSendCount_PrivateMessage').value = PrivateMessageCount.toString();
        document.getElementById('hfPrivateMessageSendPageCount_PrivateMessage').value = PrivateMessagePageCount.toString();
        Changefooter_GridPrivateMessageSend_PrivateMessage();
    }
}

function tlbItemHelp_TlbPrivateMessageIntroduction_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPrivateMessageIntroduction');       
}

function tlbItemSystemMessage_TlbPrivateMessageIntroduction_onClick() {
    ShowDialogSystemMessage();
}

function ShowDialogSystemMessage() {
    DialogSystemMessage.Show();
}
   