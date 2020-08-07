
var BaseCallBackPrefix_GridShiftsView_ShiftsView = null;

function GetBoxesHeaders_ShiftsView() {
    parent.document.getElementById('Title_DialogShiftsView').innerHTML = document.getElementById('hfTitle_DialogShiftsView').value;
    document.getElementById('header_Shifts_ShiftsView').innerHTML = document.getElementById('hfheader_Shifts_ShiftsView').value;
}

function GridShiftsView_ShiftsView_onItemExpand(sender, e) {
    GridShiftsView_ShiftsView.render();
}

function GridShiftsView_ShiftsView_onRenderComplete(sender, e) {
    if (parent.parent.CurrentLangID == 'fa-IR') {
        parent.document.getElementById('DialogShiftsView_topLeftImage').src = 'Images/Dialog/top_right.gif';
        parent.document.getElementById('DialogShiftsView_topRightImage').src = 'Images/Dialog/top_left.gif';
        parent.document.getElementById('DialogShiftsView_downLeftImage').src = 'Images/Dialog/down_right.gif';
        parent.document.getElementById('DialogShiftsView_downRightImage').src = 'Images/Dialog/down_left.gif';
        parent.document.getElementById('CloseButton_DialogShiftsView').align = 'left';
        parent.document.getElementById('tbl_DialogShiftsViewheader').dir = 'rtl';
        parent.document.getElementById('tbl_DialogShiftsViewfooter').dir = 'rtl';
    }
    if (parent.parent.CurrentLangID == 'en-US')
        parent.document.getElementById('CloseButton_DialogShiftsView').align = 'right';
}

function Fill_GridShiftsView_ShiftsView() {
    document.getElementById('loadingPanel_GridShiftsView_ShiftsView').innerHTML = document.getElementById('hfloadingPanel_GridShiftsView_ShiftsView').value;    
    var RequestDate = parent.DialogShiftsView.get_value().RequestDate;
    CallBack_GridShiftsView_ShiftsView.callback(CharToKeyCode_ShiftsView(RequestDate));
}

function CallBack_GridShiftsView_ShiftsView_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErroHiddenField_ShiftsView').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridShiftsView_ShiftsView();
    }
}

function CharToKeyCode_ShiftsView(str) {
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

function GridShiftsView_ShiftsView_onLoad() {
    document.getElementById('loadingPanel_GridShiftsView_ShiftsView').innerHTML = '';
    BaseCallBackPrefix_GridShiftsView_ShiftsView = GridShiftsView_ShiftsView.CallbackPrefix;
}

function Refresh_GridShiftsView_ShiftsView() {
    Fill_GridShiftsView_ShiftsView();
}

function GridShiftsView_ShiftsView_onBeforeCallback(sender, e) {
    SetCallBackPrefix_GridMasterMonthlyOperation_MasterMonthlyOperation();
}

function SetCallBackPrefix_GridMasterMonthlyOperation_MasterMonthlyOperation() {
    var RequestDate = parent.DialogShiftsView.get_value().RequestDate;
    GridShiftsView_ShiftsView.CallbackPrefix = BaseCallBackPrefix_GridShiftsView_ShiftsView + '&RequestDate=' + CharToKeyCode_ShiftsView(RequestDate);
}

function CallBack_GridShiftsView_ShiftsView_onCallbackError(sender, e) {
    ShowConnectionError_ShiftsView();
}

function ShowConnectionError_ShiftsView() {
    var error = document.getElementById('hfErrorType_ShiftsView').value;
    var errorBody = document.getElementById('hfConnectionError_ShiftsView').value;
    showDialog(error, errorBody, 'error');
}


