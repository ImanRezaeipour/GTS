
function GetBoxesHeaders_UserInformation() {
    parent.document.getElementById('Title_DialogUserInformation').innerHTML = document.getElementById('hfTitle_DialogUserInformation').value;
}

function CallBack_BListUserInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_UserInformation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}

function CallBack_BListUserInformation_onCallbackError(sender, e) {
    ShowConnectionError_UserInformation();
}

function ShowConnectionError_UserInformation() {
    var error = document.getElementById('hfErrorType_UserInformation').value;
    var errorBody = document.getElementById('hfConnectionError_UserInformation').value;
    showDialog(error, errorBody, 'error');
}

function GetUserInformation_UserInformation() {
    var ObjDialogUserInformation = parent.DialogUserInformation.get_value();
    var LoadState = null;
    var PersonnelID = null;
    switch (ObjDialogUserInformation.CallerSchema) {
        case 'Grid':
            var ObjDialogMonthlyOperationGridSchema = parent.parent.DialogMonthlyOperationGridSchema.get_value();
            LoadState = ObjDialogMonthlyOperationGridSchema.LoadState;
            PersonnelID = ObjDialogMonthlyOperationGridSchema.PersonnelID;
            break;
        case 'GanttChart':
            var ObjDialogMonthlyOperationGanttChartSchema = parent.parent.DialogMonthlyOperationGanttChartSchema.get_value();
            LoadState = ObjDialogMonthlyOperationGanttChartSchema.LoadState;
            PersonnelID = ObjDialogMonthlyOperationGanttChartSchema.PersonnelID;
            break;
    }
    CallBack_BListUserInformation.callback(CharToKeyCode_UserInformation(LoadState), CharToKeyCode_UserInformation(PersonnelID));
}

function CharToKeyCode_UserInformation(str) {
    var OutStr = '';
    if (str != null) {
        for (var i = 0; i < str.length; i++) {
            var KeyCode = str.charCodeAt(i);
            var CharKeyCode = '//' + KeyCode;
            OutStr += CharKeyCode;
        }
    }
    return OutStr;
}

