
function GetBoxesHeaders_SystemMessage() {
    parent.document.getElementById('Title_DialogSystemMessage').innerHTML = document.getElementById('hfTitle_DialogSystemMessage').value;
}

function GetMessage_SystemMessage() {
    var RetMessage = document.getElementById('hfMessage_SystemMessage').value;
    if (RetMessage != null && RetMessage.length > 0) {
        RetMessage = eval('(' + RetMessage + ')');
        showDialog(RetMessage[0], RetMessage[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_SystemMessage();
            parent.parent.document.getElementById('pgvPrivateMessage_iFrame').contentWindow.RefreshGridPrivateMessageSend_PrivateMessage();
        }
    }
}

function ClearList_SystemMessage() {
    document.getElementById('txtSubject_SystemMessage').value = '';
    document.getElementById('txtMessage_SystemMessage').value = '';
}
