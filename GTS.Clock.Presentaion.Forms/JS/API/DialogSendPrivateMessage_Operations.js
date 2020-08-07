var ObjexpandingOrgPostNode_SendPrivateMessage = null;
var ObjexpandingDepPersonnelNode_SendPrivateMessage = null;
var CurrentPageState_SendPrivateMessage = 'Add';
var CurrentManagerCreator_SendPrivateMessage = null;
var ObjUnderManagement_SendPrivateMessage = null;

function CustomizeForm_SendPrivateMessage() {
    var ObjDialogSendPrivateMessage = parent.DialogSendPrivateMessage.get_value();
    var State = ObjDialogSendPrivateMessage.State;
    switch (State) {
        case 'Direct':
            Fill_trvOrganizationPersonnel_SendPrivateMessage(true);
            Fill_GridUnderManagementPersonnel_SendPrivateMessage();
            break;
        case 'Reply':
            document.getElementById('tblAdvancedBox_SendPrivateMessage').parentNode.removeChild(document.getElementById('tblAdvancedBox_SendPrivateMessage'));
            document.getElementById('txtSendPrivateMessageSubject_SendPrivateMessageIntroduction').disabled = true;
            document.getElementById('txtSendPrivateMessageSubject_SendPrivateMessageIntroduction').value = ObjDialogSendPrivateMessage.ReceveMessageSubject;
            document.getElementById('lblInformationSenderPerson').innerHTML = document.getElementById('hfSendTo_tblPrivateMessageDetails_PrivateMessageIntroduction').value + ObjDialogSendPrivateMessage.ReceveMessageTarget;
            document.getElementById('hfReplyPersonID_SendPrivateMessage').value = ObjDialogSendPrivateMessage.ReceveMessageTargetID;
            document.getElementById('hfReplyMessageID_SendPrivateMessage').value = ObjDialogSendPrivateMessage.RecieveMessageID;
            break;
    }
}
function tlbItemFormReconstruction_TlbSendPrivateMessage_onClick() {
    CloseDilaogSendPrivateMessage();
    var ObjDialogSendPrivateMessage = parent.DialogSendPrivateMessage.get_value();

    parent.document.getElementById('pgvPrivateMessage_iFrame').contentWindow.ShowDialogSendPrivateMessage(ObjDialogSendPrivateMessage.State);
}
function GetBoxesHeaders_SendPrivateMessage() {
    parent.document.getElementById('Title_DialogSendPrivateMessage').innerHTML = document.getElementById('hfTitle_DialogSendPrivateMessage').value;
    document.getElementById('header_OrganizationPersonnelBox_SendPrivateMessage').innerHTML = document.getElementById('hfheader_OrganizationPersonnelBox_SendPrivateMessage').value;
    document.getElementById('header_UnderManagementPersonnelBox_SendPrivateMessage').innerHTML = document.getElementById('hfheader_SendPrivateMessageBox_SendPrivateMessage').value;
    document.getElementById('header_tblSendPrivateMessageDetails_SendPrivateMessageIntroduction').innerHTML = document.getElementById('hfheader_tblPrivateMessageDetails_PrivateMessageIntroduction').value;
}

function CharToKeyCode_SendPrivateMessage(str) {
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

function tlbItemExit_TlbSendPrivateMessage_onClick() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_SendPrivateMessage').value;
    DialogConfirm.Show();

}

function Refresh_trvOrganizationPersonnel_SendPrivateMessage() {
    Fill_trvOrganizationPersonnel_SendPrivateMessage(true);
}

function trvOrganizationPersonnel_SendPrivateMessage_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvOrganizationPersonnel_SendPrivateMessage').innerHTML = '';
}

function trvOrganizationPersonnel_SendPrivateMessage_onCallbackComplete(sender, e) {
    if (ObjexpandingDepPersonnelNode_SendPrivateMessage != null) {
        if (ObjexpandingDepPersonnelNode_SendPrivateMessage.Node.get_nodes().get_length() == 0 && ObjexpandingDepPersonnelNode_SendPrivateMessage.HasChild) {
            ObjexpandingDepPersonnelNode_SendPrivateMessage = null;
            GetLoadonDemandError_DepartmetsPersonnel_SendPrivateMessagePage();
        }
        else
            ObjexpandingDepPersonnelNode_SendPrivateMessage = null;
    }
}

function GetLoadonDemandError_DepartmetsPersonnel_SendPrivateMessagePage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function trvOrganizationPersonnel_SendPrivateMessage_onNodeBeforeExpand(sender, e) {
    if (ObjexpandingDepPersonnelNode_SendPrivateMessage != null)
        ObjexpandingDepPersonnelNode_SendPrivateMessage = null;
    ObjexpandingDepPersonnelNode_SendPrivateMessage = new Object();
    ObjexpandingDepPersonnelNode_SendPrivateMessage.Node = e.get_node();
    if (e.get_node().get_nodes().get_length() == 1 && (e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined || e.get_node().get_nodes().get_nodeArray()[0].get_id() == '')) {
        ObjexpandingDepPersonnelNode_SendPrivateMessage.HasChild = true;
        trvOrganizationPersonnel_SendPrivateMessage.beginUpdate();
        ObjexpandingDepPersonnelNode_SendPrivateMessage.Node.get_nodes().remove(0);
        trvOrganizationPersonnel_SendPrivateMessage.endUpdate();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            ObjexpandingDepPersonnelNode_SendPrivateMessage.HasChild = false;
        else
            ObjexpandingDepPersonnelNode_SendPrivateMessage.HasChild = true;
    }
}

function CallBack_trvOrganizationPersonnel_SendPrivateMessage_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_OrganizationPersonnel_SendPrivateMessage').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvOrganizationPersonnel_SendPrivateMessage(true);
    }
}

function Fill_trvOrganizationPersonnel_SendPrivateMessage(isRefresh) {
    if (trvOrganizationPersonnel_SendPrivateMessage.get_nodes().get_length() == 0 || isRefresh) {
        document.getElementById('loadingPanel_trvOrganizationPersonnel_SendPrivateMessage').innerHTML = document.getElementById('hfloadingPanel_trvOrganizationPersonnel_SendPrivateMessage').value;
        ObjexpandingDepPersonnelNode_SendPrivateMessage = null;
        CallBack_trvOrganizationPersonnel_SendPrivateMessage.callback();
    }
}

function GridUnderManagementPersonnel_SendPrivateMessage_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridUnderManagementPersonnel_SendPrivateMessage').innerHTML = '';
}

function Fill_GridSendPrivateMessage_SendPrivateMessage() {
    document.getElementById('loadingPanel_GridSendPrivateMessage_SendPrivateMessage').innerHTML = document.getElementById('hfloadingPanel_GridSendPrivateMessage_SendPrivateMessage').value;
    switch (CurrentPageState_SendPrivateMessage) {
        case 'Add':
            CallBack_GridSendPrivateMessage_SendPrivateMessage.callback(CharToKeyCode_UnderManagementPersonnel(CurrentPageState_SendPrivateMessage));
            break;
        case 'Edit':
            var flowID = parent.DialogSendPrivateMessage.get_value().FlowID;
            CallBack_GridSendPrivateMessage_SendPrivateMessage.callback(CharToKeyCode_SendPrivateMessage(CurrentPageState_SendPrivateMessage), CharToKeyCode_SendPrivateMessage(flowID));
            break;
    }
}

function CallBack_GridSendPrivateMessage_SendPrivateMessage_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_SendPrivateMessage_SendPrivateMessage').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridSendPrivateMessage_SendPrivateMessage();
    }
}

function tlbItemsend_TlbSendPrivateMessage_onClick() {
    UpdateUnderManagement_SendPrivateMessage();

}

function GetUndermanagementType_SendPrivateMessage(typeKey) {
    var UnderManagementTypeList_SendPrivateMessage = document.getElementById('hfUndermanagementTypesList_SendPrivateMessage').value;
    ListParts = UnderManagementTypeList_SendPrivateMessage.split('#');
    for (var i = 0; i < ListParts.length; i++) {
        if (ListParts[i] != '') {
            var PartsSection = ListParts[i].split(':');
            if (PartsSection[0] == typeKey)
                return PartsSection[1];
        }
    }
}

function tlbItemDelete_TlbInterAction_SendPrivateMessage_onClick() {
    DeleteUnderManagement_SendPrivateMessage();
}

function DeleteUnderManagement_SendPrivateMessage() {
    var UnderManagementPersonnelList_SendPrivateMessage = document.getElementById('hfUnderManagementPersonnelList_SendPrivateMessage').value;
    selectedUnderManagement = GridUnderManagementPersonnel_SendPrivateMessage.getSelectedItems();
    if (selectedUnderManagement.length > 0) {
        ListIdentifier = 'Key=' + selectedUnderManagement[0].getMember('KeyID').get_text();
        var ListIdentifierIndex = UnderManagementPersonnelList_SendPrivateMessage.indexOf(ListIdentifier);
        if (ListIdentifierIndex >= 0) {
            var ListPart = ListIdentifier + '%Type=' + selectedUnderManagement[0].getMember('Type').get_text() + '%Access=' + selectedUnderManagement[0].getMember('Contains').get_text() + '%SubDep=' + selectedUnderManagement[0].getMember('ContainInnerChilds').get_text() + '#';
            UnderManagementPersonnelList_SendPrivateMessage = UnderManagementPersonnelList_SendPrivateMessage.replace(ListPart, '');

            GridUnderManagementPersonnel_SendPrivateMessage.beginUpdate();
            GridUnderManagementPersonnel_SendPrivateMessage.deleteSelected();
            GridUnderManagementPersonnel_SendPrivateMessage.endUpdate();
        }
    }
    document.getElementById('hfUnderManagementPersonnelList_SendPrivateMessage').value = UnderManagementPersonnelList_SendPrivateMessage;
}

function SetActionMode_SendPrivateMessage() {
    CurrentPageState_SendPrivateMessage = parent.DialogSendPrivateMessage.get_value().PageState;
    document.getElementById('ActionMode_SendPrivateMessage').innerHTML = document.getElementById('hf' + CurrentPageState_SendPrivateMessage + '_SendPrivateMessage').value;
    ChangePageState_SendPrivateMessage(CurrentPageState_SendPrivateMessage);
}

function contextMenu_trvOrganizationPersonnel_SendPrivateMessage_onItemSelect(sender, e) {
    var OrganizationPartItem_SendPrivateMessage = e.get_item();
    var OrganizationPartNode_SendPrivateMessage = OrganizationPartItem_SendPrivateMessage.get_parentMenu().get_contextData();
    trvOrganizationPersonnel_SendPrivateMessage.selectNodeById(OrganizationPartNode_SendPrivateMessage.get_id());
    InsertUnderManagement_SendPrivateMessage();
}
function trvOrganizationPersonnel_SendPrivateMessage_onContextMenu(sender, e) {
    if (TlbInterAction_SendPrivateMessage.get_items().getItemById('tlbItemAdd_TlbInterAction_SendPrivateMessage') != null)
        contextMenu_trvOrganizationPersonnel_UnderManagementPersonnel.showContextMenuAtEvent(e.get_event(), e.get_node());
}

function contextMenu_GridUnderManagementPersonnel_SendPrivateMessage_onItemSelect(sender, e) {
    DeleteUnderManagement_SendPrivateMessage();
}
function GridUnderManagementPersonnel_SendPrivateMessage_onContextMenu(sender, e) {
    if (TlbInterAction_SendPrivateMessage.get_items().getItemById('tlbItemDelete_TlbInterAction_SendPrivateMessage') != null) {
        GridUnderManagementPersonnel_SendPrivateMessage.select(e.get_item());
        contextMenu_GridUnderManagementPersonnel_SendPrivateMessage.showContextMenuAtEvent(e.get_event());
        contextMenu_GridUnderManagementPersonnel_SendPrivateMessage.set_contextData(e.get_item());
    }
}

function GridUnderManagementPersonnel_SendPrivateMessage_onItemBeforeCheckChange(sender, e) {
    var UnderManagementPersonnelList_SendPrivateMessage = document.getElementById('hfUnderManagementPersonnelList_SendPrivateMessage').value;
    var itemKey = e.get_item().getMember('KeyID').get_text();
    var itemType = e.get_item().getMember('Type').get_text();
    var itemAccess = e.get_item().getMember('Contains').get_value();
    var itemSubDep = e.get_item().getMember('ContainInnerChilds').get_value();
    if (CheckSubDepCheckChange_onItemBeforeCheckChange(e)) {
        var beforeChangeListPart = 'Key=' + itemKey + '%Type=' + itemType + '%Access=' + itemAccess.toString() + '%SubDep=' + itemSubDep.toString();
        if (UnderManagementPersonnelList_SendPrivateMessage.indexOf(beforeChangeListPart) >= 0) {
            switch (e.get_columnIndex()) {
                case 1:
                    itemAccess = !itemAccess;
                    break;
                case 4:
                    itemSubDep = !itemSubDep;
                    break;
            }
        }
        var afterChangeListPart = 'Key=' + itemKey + '%Type=' + itemType + '%Access=' + itemAccess.toString() + '%SubDep=' + itemSubDep.toString();
        UnderManagementPersonnelList_SendPrivateMessage = UnderManagementPersonnelList_SendPrivateMessage.replace(beforeChangeListPart, afterChangeListPart);
        document.getElementById('hfUnderManagementPersonnelList_SendPrivateMessage').value = UnderManagementPersonnelList_SendPrivateMessage;
    }
}

function CheckSubDepCheckChange_onItemBeforeCheckChange(e) {
    var changeIsAllowd = true;
    var dataField = GridUnderManagementPersonnel_SendPrivateMessage.get_table().get_columns()[e.get_columnIndex()].get_dataField();
    if (dataField == 'ContainInnerChilds' && e.get_item().getMember('Type').get_text() != '0') {
        var itemSubDep = e.get_item().getMember(dataField).get_value();
        if (!itemSubDep)
            changeIsAllowd = false;
    }
    return changeIsAllowd;
}
function GridUnderManagementPersonnel_SendPrivateMessage_onItemCheckChange(sender, e) {
    var itemSubDep = e.get_item().getMember('ContainInnerChilds').get_value();
    if (!itemSubDep && e.get_item().getMember('Type').get_text() != '0') {
        GridUnderManagementPersonnel_SendPrivateMessage.beginUpdate();
        e.get_item().setValue(4, false, false);
        GridUnderManagementPersonnel_SendPrivateMessage.endUpdate();
    }
}


function SetImage_clmnType_GridUnderManagementPersonnel_SendPrivateMessage(type) {
    var nodeTypeImage = '';
    switch (type.toString()) {
        case '0':
            nodeTypeImage = 'Images/Grid/folder.gif';
            break;
        case '1':
            nodeTypeImage = 'Images/Grid/user.png';
            break;
    }
    return nodeTypeImage;
}

function ChangePageState_SendPrivateMessage(state) {
    switch (state) {
        case 'Add':
            if (TlbSendPrivateMessage.get_items().getItemById('tlbItemExeptionAccessCreation_TlbSendPrivateMessage') != null) {
                TlbSendPrivateMessage.get_items().getItemById('tlbItemExeptionAccessCreation_TlbSendPrivateMessage').set_enabled(false);
                TlbSendPrivateMessage.get_items().getItemById('tlbItemExeptionAccessCreation_TlbSendPrivateMessage').set_imageUrl('access_silver.png');
            }
            if (TlbSendPrivateMessage.get_items().getItemById('tlbItemExeptionAccessView_TlbSendPrivateMessage') != null) {
                TlbSendPrivateMessage.get_items().getItemById('tlbItemExeptionAccessView_TlbSendPrivateMessage').set_enabled(false);
                TlbSendPrivateMessage.get_items().getItemById('tlbItemExeptionAccessView_TlbSendPrivateMessage').set_imageUrl('exceptions_silver.png');
            }
            if (TlbSendPrivateMessage.get_items().getItemById('tlbItemWorkFlow_TlbSendPrivateMessage') != null) {
                TlbSendPrivateMessage.get_items().getItemById('tlbItemWorkFlow_TlbSendPrivateMessage').set_enabled(false);
                TlbSendPrivateMessage.get_items().getItemById('tlbItemWorkFlow_TlbSendPrivateMessage').set_imageUrl('flowCreate_silver.png');
            }
            break;

     
    }
}


function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDilaogSendPrivateMessage();
}

function CloseDilaogSendPrivateMessage() {
    parent.document.getElementById('DialogSendPrivateMessage_IFrame').src = 'WhitePage.aspx';
    parent.DialogSendPrivateMessage.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}
function ShowConnectionError_SendPrivateMessage() {
    var error = document.getElementById('hfErrorType_SendPrivateMessage').value;
    var errorBody = document.getElementById('hfConnectionError_SendPrivateMessage').value;
    showDialog(error, errorBody, 'error');
}

function CallBack_GridUnderManagementPersonnel_SendPrivateMessage_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_SendPrivateMessage_SendPrivateMessage').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridUnderManagementPersonnel_SendPrivateMessage();
    }
}
function Fill_GridUnderManagementPersonnel_SendPrivateMessage() {
    document.getElementById('loadingPanel_GridUnderManagementPersonnel_SendPrivateMessage').innerHTML = document.getElementById('hfloadingPanel_GridUnderManagementPersonnel_SendPrivateMessage').value;
    switch (CurrentPageState_SendPrivateMessage) {
        case 'Add':
            CallBack_GridUnderManagementPersonnel_SendPrivateMessage.callback(CharToKeyCode_SendPrivateMessage(CurrentPageState_SendPrivateMessage));
            break;

    }
}
function CallBack_trvOrganizationPersonnel_SendPrivateMessage_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvOrganizationPersonnel_SendPrivateMessage').innerHTML = '';
    ShowConnectionError_SendPrivateMessage();
}

function CallBack_GridUnderManagementPersonnel_SendPrivateMessage_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridUnderManagementPersonnel_SendPrivateMessage').innerHTML = '';
    ShowConnectionError_SendPrivateMessage();
}

function ShowConnectionError_SendPrivateMessage() {
    var error = document.getElementById('hfErrorType_SendPrivateMessage').value;
    var errorBody = document.getElementById('hfConnectionError_SendPrivateMessage').value;
    showDialog(error, errorBody, 'error');
}
function tlbItemAdd_TlbInterAction_SendPrivateMessage_onClick() {
    InsertUnderManagement_SendPrivateMessage();
}

function InsertUnderManagement_SendPrivateMessage() {
    var UnderManagementPersonnelList_SendPrivateMessage = document.getElementById('hfUnderManagementPersonnelList_SendPrivateMessage').value;
    selectedNode_trvOrganizationPersonnel_SendPrivateMessage = trvOrganizationPersonnel_SendPrivateMessage.get_selectedNode();
    if (selectedNode_trvOrganizationPersonnel_SendPrivateMessage != undefined) {
        var nodeID = selectedNode_trvOrganizationPersonnel_SendPrivateMessage.get_id();
        var nodeName = selectedNode_trvOrganizationPersonnel_SendPrivateMessage.get_text();
        var parentNodeID = selectedNode_trvOrganizationPersonnel_SendPrivateMessage.get_parentNode().get_id();
        var nodeType = selectedNode_trvOrganizationPersonnel_SendPrivateMessage.get_value();
        var hasAccess = false;
        var hasSubDep = false;
        var ListIdentifier = '';
        var rowID = Math.random().toString();

        switch (nodeType) {
            case '0':
                ListIdentifier = 'dep' + nodeID + 'prs0';
                break;
            case '1':
                ListIdentifier = 'dep' + parentNodeID + 'prs' + nodeID;
                break;
        }
        if (UnderManagementPersonnelList_SendPrivateMessage.indexOf(ListIdentifier) == -1) {
            var UnderManagementPersonnelListPart = 'Key=' + ListIdentifier + '%Type=' + nodeType + '%Access=' + hasAccess.toString() + '%SubDep=' + hasSubDep.toString();
            UnderManagementPersonnelList_SendPrivateMessage += UnderManagementPersonnelListPart + '#';
            GridUnderManagementPersonnel_SendPrivateMessage.beginUpdate();
            newUMPItem = GridUnderManagementPersonnel_SendPrivateMessage.get_table().addEmptyRow(GridUnderManagementPersonnel_SendPrivateMessage.get_recordCount());
            newUMPItem.setValue(0, rowID, false);
            newUMPItem.setValue(1, hasAccess, false);
            newUMPItem.setValue(2, nodeType, false);
            newUMPItem.setValue(3, nodeName, false);
            newUMPItem.setValue(4, hasSubDep, false);
            newUMPItem.setValue(5, ListIdentifier, false);
            GridUnderManagementPersonnel_SendPrivateMessage.selectByKey(rowID, 0, false);
            GridUnderManagementPersonnel_SendPrivateMessage.endUpdate();
        }
        document.getElementById('hfUnderManagementPersonnelList_SendPrivateMessage').value = UnderManagementPersonnelList_SendPrivateMessage;
    }
}
function tlbItemDelete_TlbInterAction_SendPrivateMessage_onClick() {
    DeleteUnderManagement_SendPrivateMessage();
}

function DeleteUnderManagement_SendPrivateMessage() {
    var UnderManagementPersonnelList_SendPrivateMessage = document.getElementById('hfUnderManagementPersonnelList_SendPrivateMessage').value;
    selectedUnderManagement = GridUnderManagementPersonnel_SendPrivateMessage.getSelectedItems();
    if (selectedUnderManagement.length > 0) {
        ListIdentifier = 'Key=' + selectedUnderManagement[0].getMember('KeyID').get_text();
        var ListIdentifierIndex = UnderManagementPersonnelList_SendPrivateMessage.indexOf(ListIdentifier);
        if (ListIdentifierIndex >= 0) {
            var ListPart = ListIdentifier + '%Type=' + selectedUnderManagement[0].getMember('Type').get_text() + '%Access=' + selectedUnderManagement[0].getMember('Contains').get_text() + '%SubDep=' + selectedUnderManagement[0].getMember('ContainInnerChilds').get_text() + '#';
            UnderManagementPersonnelList_SendPrivateMessage = UnderManagementPersonnelList_SendPrivateMessage.replace(ListPart, '');

            GridUnderManagementPersonnel_SendPrivateMessage.beginUpdate();
            GridUnderManagementPersonnel_SendPrivateMessage.deleteSelected();
            GridUnderManagementPersonnel_SendPrivateMessage.endUpdate();
        }
    }
    document.getElementById('hfUnderManagementPersonnelList_SendPrivateMessage').value = UnderManagementPersonnelList_SendPrivateMessage;
}

function UpdateUnderManagement_SendPrivateMessage() {
    var ObjDialogSendPrivateMessage = parent.DialogSendPrivateMessage.get_value();
    var message_SendPrivateMessage = document.getElementById('txtSendPrivateMessageMessage_SendPrivateMessageIntroduction').value;
    var subject_SendPrivateMessage = document.getElementById('txtSendPrivateMessageSubject_SendPrivateMessageIntroduction').value;
    switch (ObjDialogSendPrivateMessage.State) {
        case 'Reply':
            UpdateUnderManagementReply_SendPrivateMessagePage(CharToKeyCode_SendPrivateMessage(ObjDialogSendPrivateMessage.RecieveMessageID), CharToKeyCode_SendPrivateMessage(message_SendPrivateMessage), CharToKeyCode_SendPrivateMessage(subject_SendPrivateMessage));
            DialogWaiting.Show();
            break;
        case 'Direct':
            ObjPageState_SendPrivateMessage = parent.DialogSendPrivateMessage.get_value();
            strUnderManagementList = document.getElementById('hfUnderManagementPersonnelList_SendPrivateMessage').value;
            UpdateUnderManagement_SendPrivateMessagePage(CharToKeyCode_SendPrivateMessage(strUnderManagementList), CharToKeyCode_SendPrivateMessage(message_SendPrivateMessage), CharToKeyCode_SendPrivateMessage(subject_SendPrivateMessage));
            DialogWaiting.Show();
            break;
    }

}

function UpdateUnderManagement_SendPrivateMessagePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_SendPrivateMessage').value;
            Response[1] = document.getElementById('hfConnectionError_SendPrivateMessage').value;
        }
        if (RetMessage[2] == 'success') {
            var obj = parent.DialogSendPrivateMessage.get_value();
            parent.document.getElementById('pgvPrivateMessage_iFrame').contentWindow.SetPageIndex_GridPrivateMessageSend_PrivateMessage(0);
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}
function UpdateUnderManagementReply_SendPrivateMessagePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_SendPrivateMessage').value;
            Response[1] = document.getElementById('hfConnectionError_SendPrivateMessage').value;
        }
        if (RetMessage[2] == 'success') {
            var obj = parent.DialogSendPrivateMessage.get_value();
            parent.document.getElementById('pgvPrivateMessage_iFrame').contentWindow.SetPageIndex_GridPrivateMessageSend_PrivateMessage(0);
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}
