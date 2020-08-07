
var BaseCallBackPrefix_GridTraffics_TrafficsControl = null;

function Fill_GridTraffics_TrafficsControl() {
    ChangeLoadingPannelContent_GridTraffics_TrafficsControl('Fill');
    var TrafficsRelativeObj = GetTrafficsRelativeObj_TrafficsControl();
    var PersonnelID = TrafficsRelativeObj.PersonnelID;
    var Year = TrafficsRelativeObj.Year;
    var Month = TrafficsRelativeObj.Month;
    CallBack_GridTraffics_TrafficsControl.callback(CharToKeyCode_TrafficsControl(PersonnelID), CharToKeyCode_TrafficsControl(Year), CharToKeyCode_TrafficsControl(Month));
}

function GetTrafficsRelativeObj_TrafficsControl() {
    return parent.parent.document.getElementById('pgvTrafficsControl_iFrame').contentWindow.GetTrafficsRelativeObj_MasterTrafficsControl();
}

function GridTraffics_TrafficsControl_onLoad(sender, e) {
    ChangeLoadingPannelContent_GridTraffics_TrafficsControl('Load');
    BaseCallBackPrefix_GridTraffics_TrafficsControl = GridTraffics_TrafficsControl.CallbackPrefix;
}

function ChangeLoadingPannelContent_GridTraffics_TrafficsControl(state) {
    parent.parent.document.getElementById('pgvTrafficsControl_iFrame').contentWindow.ChangloadingPanelContent_GridTraffics_MasterTrafficsControl(state);
}

function GridTraffics_TrafficsControl_onItemExpand(sender, e) {
    GridTraffics_TrafficsControl.render();
}

function GridTraffics_TrafficsControl_onBeforeCallback(sender, e) {
    SetCallBackPrefix_GridTraffics_TrafficsControl();
    parent.parent.DialogLoading.Show();
}

function SetCallBackPrefix_GridTraffics_TrafficsControl() {
    var TrafficsRelativeObj = GetTrafficsRelativeObj_TrafficsControl();
    var PersonnelID = TrafficsRelativeObj.PersonnelID;
    var Year = TrafficsRelativeObj.Year;
    var Month = TrafficsRelativeObj.Month;
    GridTraffics_TrafficsControl.CallbackPrefix = BaseCallBackPrefix_GridTraffics_TrafficsControl + '&PersonnelID=' + CharToKeyCode_TrafficsControl(PersonnelID) + '&Year=' + CharToKeyCode_TrafficsControl(Year) + '&Month=' + CharToKeyCode_TrafficsControl(Month);        
}

function GridTraffics_TrafficsControl_onCallbackComplete(sender, e) {
    parent.parent.DialogLoading.Show();
}

function GridTraffics_TrafficsControl_onRenderComplete(sender, e) {
    parent.parent.DialogLoading.Close();
}

function SetBaseCallbackPrefix_GridTraffics_TrafficsControl() {
    BaseCallbackPrefix_GridTraffics_TrafficsControl = CallBack_GridTraffics_TrafficsControl.CallbackPrefix;
}

function CallBack_GridTraffics_TrafficsControl_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Traffics_TrafficsControl').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        parent.parent.document.getElementById('pgvTrafficsControl_iFrame').contentWindow.ShowError_GridTraffics_TrafficsControl_onCallBackCompleted(errorParts);
        if (errorParts[3] == 'Reload')
            Fill_GridTraffics_TrafficsControl();
    }
}

function CallBack_GridTraffics_TrafficsControl_onCallbackError(sender, e) {
    ShowConnectionError_TrafficsControl();
}

function GetSelectedItemObj_GridTraffics_TrafficsControl() {
    var SelectedItemObj_GridTraffics_TrafficsControl = undefined;
    if (GridTraffics_TrafficsControl.getSelectedItems().length > 0) {
        SelectedItemObj_GridTraffics_TrafficsControl = new Object();
        SelectedItemObj_GridTraffics_TrafficsControl.Obj = GridTraffics_TrafficsControl.getSelectedItems()[0];
        SelectedItemObj_GridTraffics_TrafficsControl.Level = GridTraffics_TrafficsControl.getSelectedItems()[0].get_table().get_level();
    }
    return SelectedItemObj_GridTraffics_TrafficsControl;
}

function CharToKeyCode_TrafficsControl(str) {
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

function ShowConnectionError_TrafficsControl() {
    var error = document.getElementById('hfErrorType_TrafficsControl').value;
    var errorBody = document.getElementById('hfConnectionError_TrafficsControl').value;
    showDialog(error, errorBody, 'error');
}





