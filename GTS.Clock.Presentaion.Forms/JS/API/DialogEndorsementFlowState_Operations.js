

function GetBoxesHeaders_EndorsementFlowState() {
    parent.parent.document.getElementById('Title_DialogEndorsementFlowState').innerHTML = document.getElementById('hfTitle_DialogEndorsementFlowState').value;
    document.getElementById('header_EndorsementFlowState_EndorsementFlowState').innerHTML = document.getElementById('hfheader_EndorsementFlowState_EndorsementFlowState').value;
}

function Initialize_EndorsementFlowState() {
    var ObjEndorsementFlowState = parent.parent.DialogEndorsementFlowState.get_value();
    var ManagerFlowID = ObjEndorsementFlowState.ManagerFlowID;
    if (ManagerFlowID != 0)
        document.getElementById('Container_txtEndorsementFlowState_EndorsementFlowState').parentNode.removeChild(document.getElementById('Container_txtEndorsementFlowState_EndorsementFlowState'));
    else
        document.getElementById('Container_GridEndorsementFlowState_EndorsementFlowState').parentNode.removeChild(document.getElementById('Container_GridEndorsementFlowState_EndorsementFlowState'));       
}

function Fill_GridEndorsementFlowState_EndorsementFlowState() {
    document.getElementById('loadingPanel_GridEndorsementFlowState_EndorsementFlowState').innerHTML = document.getElementById('hfloadingPanel_GridEndorsementFlowState_EndorsementFlowState').value;
    var ObjEndorsementFlowState = parent.parent.DialogEndorsementFlowState.get_value();
    var ManagerFlowID = ObjEndorsementFlowState.ManagerFlowID;
    var RequestID = ObjEndorsementFlowState.RequestID;
    CallBack_GridEndorsementFlowState_EndorsementFlowState.callback(CharToKeyCode_EndorsementFlowState(ManagerFlowID), CharToKeyCode_EndorsementFlowState(RequestID));
}

function GridEndorsementFlowState_EndorsementFlowState_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridEndorsementFlowState_EndorsementFlowState').innerHTML = '';
}

function CallBack_GridEndorsementFlowState_EndorsementFlowState_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_InFlow_EndorsementFlowState').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridEndorsementFlowState_EndorsementFlowState();
    }
}

function CharToKeyCode_EndorsementFlowState(str) {
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

function SetCellTitle_GridEndorsementFlowState_EndorsementFlowState(Key) {
    strListObj = document.getElementById('hfRequestStates_EndorsementFlowState').value.split('#');
    for (var i = 0; i < strListObj.length; i++) {
        strListItemObj = strListObj[i].split(':');
        if (strListItemObj.length > 1) {
            if (strListItemObj[1] == Key.toString())
                return strListItemObj[0];
        }
    }
}

function SetClmnImage_GridEndorsementFlowState_EndorsementFlowState(Key) {
    switch (Key.toString()) {
        case '1':
            cellImage = 'Images/Grid/save.png';
            break;
        case '2':
            cellImage = 'Images/Grid/cancel.png';
            break;
        case '3':
            cellImage = 'Images/Grid/waiting_flow.png';
            break;
        case '4':
            cellImage = 'Images/Grid/remove.png';
            break;
    }
    return cellImage;
}

function Refresh_GridEndorsementFlowState_EndorsementFlowState() {
    GetEndorsementFlowStates_EndorsementFlowState();
}

function CallBack_GridEndorsementFlowState_EndorsementFlowState_onCallbackError(sender, e) {
    ShowConnectionError_EndorsementFlowState();
}

function ShowConnectionError_EndorsementFlowState() {
    var error = document.getElementById('hfErrorType_EndorsementFlowState').value;
    var errorBody = document.getElementById('hfConnectionError_EndorsementFlowState').value;
    showDialog(error, errorBody, 'error');
}

function CallBack_txtEndorsementFlowState_EndorsementFlowState_onCallbackComplete(sender, e) {
    document.getElementById('loadingPanel_GridEndorsementFlowState_EndorsementFlowState').innerHTML = '';
    var error = document.getElementById('ErrorHiddenField_PendingFlow_EndorsementFlowState').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_txtEndorsementFlowState_EndorsementFlowState();
    }
}

function CallBack_txtEndorsementFlowState_EndorsementFlowState_onCallbackError(sender, e) {
    ShowConnectionError_EndorsementFlowState();
}

function Fill_txtEndorsementFlowState_EndorsementFlowState() {
    document.getElementById('loadingPanel_GridEndorsementFlowState_EndorsementFlowState').innerHTML = document.getElementById('hfloadingPanel_GridEndorsementFlowState_EndorsementFlowState').value;
    var ObjEndorsementFlowState = parent.parent.DialogEndorsementFlowState.get_value();
    var PersonnelID = ObjEndorsementFlowState.PersonnelID;
    var RequestID = ObjEndorsementFlowState.RequestID;
    CallBack_txtEndorsementFlowState_EndorsementFlowState.callback(CharToKeyCode_EndorsementFlowState(PersonnelID), CharToKeyCode_EndorsementFlowState(RequestID));
}

function GetEndorsementFlowStates_EndorsementFlowState() {
    var ObjEndorsementFlowState = parent.parent.DialogEndorsementFlowState.get_value();
    var ManagerFlowID = ObjEndorsementFlowState.ManagerFlowID;
    if (ManagerFlowID != 0)
        Fill_GridEndorsementFlowState_EndorsementFlowState();
    else
        Fill_txtEndorsementFlowState_EndorsementFlowState();
}









