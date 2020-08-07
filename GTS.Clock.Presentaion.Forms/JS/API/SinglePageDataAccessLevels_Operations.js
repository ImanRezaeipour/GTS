
var ObjDataAccessLevel_SinglePageDataAccessLevels = null;
var CurrentPageState_SinglePageDataAccessLevels = 'View';
var OperationState_SinglePageDataAccessLevels = 'Before';

function GetObjDataAccessLevel_SinglePageDataAccessLevels(){
    ObjDataAccessLevel_SinglePageDataAccessLevels = parent.GetObjDataAccessLevel_MasterDataAccessLevels();
}

function GetBoxesHeaders_SinglePageDataAccessLevels() {
    if (ObjDataAccessLevel_SinglePageDataAccessLevels != null) {
        document.getElementById('header_DataAccessLevelsSource_SinglePageDataAccessLevels').innerHTML = ObjDataAccessLevel_SinglePageDataAccessLevels.Source;
        document.getElementById('header_DataAccessLevelsTarget_SinglePageDataAccessLevels').innerHTML = ObjDataAccessLevel_SinglePageDataAccessLevels.Target;
    }
}

function Refresh_GridDataAccessLevelsSource_SinglePageDataAccessLevels() {
    Fill_GridDataAccessLevelsSource_SinglePageDataAccessLevels();
}

function Fill_GridDataAccessLevelsSource_SinglePageDataAccessLevels() {
    document.getElementById('loadingPanel_GridDataAccessLevelsSource_SinglePageDataAccessLevels').innerHTML = document.getElementById('hfloadingPanel_GridDataAccessLevelsSource_SinglePageDataAccessLevels').value;
    var DataAccessLevelKey = ObjDataAccessLevel_SinglePageDataAccessLevels != null ? ObjDataAccessLevel_SinglePageDataAccessLevels.Key : '';
    CallBack_GridDataAccessLevelsSource_SinglePageDataAccessLevels.callback(CharToKeyCode_SinglePageDataAccessLevels(DataAccessLevelKey));
}

function GridDataAccessLevelsSource_SinglePageDataAccessLevels_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridDataAccessLevelsSource_SinglePageDataAccessLevels').innerHTML = '';
}

function CallBack_GridDataAccessLevelsSource_SinglePageDataAccessLevels_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DataAccessLevelsSource').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            Fill_GridDataAccessLevelsSource_SinglePageDataAccessLevels();
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}

function CallBack_GridDataAccessLevelsSource_SinglePageDataAccessLevels_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridDataAccessLevelsSource_SinglePageDataAccessLevels').innerHTML = '';
    ShowConnectionError_SinglePageDataAccessLevels();
}

function tlbItemAdd_TlbInterAction_SinglePageDataAccessLevels_onClick() {
    CurrentPageState_SinglePageDataAccessLevels = 'Add';
    UpdateDataAccessLevels_SinglePageDataAccessLevels();
}

function tlbItemDelete_TlbInterAction_SinglePageDataAccessLevels_onClick() {
    CurrentPageState_SinglePageDataAccessLevels = 'Delete';
    UpdateDataAccessLevels_SinglePageDataAccessLevels();
}

function UpdateDataAccessLevels_SinglePageDataAccessLevels() {
    var ObjDialogMasterDataAccessLevels = parent.parent.DialogMasterDataAccessLevels.get_value();
    var DataAccessLevelOperationType = ObjDialogMasterDataAccessLevels.DataAccessLevelOperationType;
    var UserID = ObjDialogMasterDataAccessLevels.UserID;
    var SearchKey = ObjDialogMasterDataAccessLevels.SearchKey;
    var SearchTerm = ObjDialogMasterDataAccessLevels.SearchTerm;
    var DataAccessLevelKey = ObjDataAccessLevel_SinglePageDataAccessLevels != null ? ObjDataAccessLevel_SinglePageDataAccessLevels.Key : '';
    var DataAccessLevelSourceID = '-1';
    var DataAccessLevelTargetID = '-1';
    switch (CurrentPageState_SinglePageDataAccessLevels) {
        case 'Add':
          if(document.getElementById('chbSelectAll_SinglePageDataAccessLevels') != null && document.getElementById('chbSelectAll_SinglePageDataAccessLevels').checked)
             DataAccessLevelSourceID = '0';
          else
          {
             var SelectedItems_GridDataAccessLevelsSource_SinglePageDataAccessLevels = GridDataAccessLevelsSource_SinglePageDataAccessLevels.getSelectedItems();             
             if(SelectedItems_GridDataAccessLevelsSource_SinglePageDataAccessLevels.length > 0)
                DataAccessLevelSourceID = SelectedItems_GridDataAccessLevelsSource_SinglePageDataAccessLevels[0].getMember('ID').get_text();
          }
          break;            
        case 'Delete':
             var SelectedItems_GridDataAccessLevelsTarget_SinglePageDataAccessLevels = GridDataAccessLevelsTarget_SinglePageDataAccessLevels.getSelectedItems();             
             if(SelectedItems_GridDataAccessLevelsTarget_SinglePageDataAccessLevels.length > 0)
                DataAccessLevelTargetID = SelectedItems_GridDataAccessLevelsTarget_SinglePageDataAccessLevels[0].getMember('ID').get_text();          
          break;
    }
    UpdateDataAccessLevels_SinglePageDataAccessLevelsPage(CharToKeyCode_SinglePageDataAccessLevels(CurrentPageState_SinglePageDataAccessLevels), CharToKeyCode_SinglePageDataAccessLevels(DataAccessLevelKey), CharToKeyCode_SinglePageDataAccessLevels(DataAccessLevelOperationType), CharToKeyCode_SinglePageDataAccessLevels(UserID), CharToKeyCode_SinglePageDataAccessLevels(SearchKey), CharToKeyCode_SinglePageDataAccessLevels(SearchTerm), CharToKeyCode_SinglePageDataAccessLevels(DataAccessLevelSourceID), CharToKeyCode_SinglePageDataAccessLevels(DataAccessLevelTargetID));
   DialogWaiting.Show();
}

function UpdateDataAccessLevels_SinglePageDataAccessLevelsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_SinglePageDataAccessLevels').value;
            Response[1] = document.getElementById('hfConnectionError_SinglePageDataAccessLevels').value;
        }
        if (RetMessage[2] == 'success') {
            CurrentPageState_SinglePageDataAccessLevels = 'View';
            OperationState_SinglePageDataAccessLevels = 'After';
            if (document.getElementById('chbSelectAll_SinglePageDataAccessLevels') != null && document.getElementById('chbSelectAll_SinglePageDataAccessLevels') != undefined)
                document.getElementById('chbSelectAll_SinglePageDataAccessLevels').checked = false;
            Fill_GridDataAccessLevelsTarget_SinglePageDataAccessLevels();
        }
        else 
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function Refresh_GridDataAccessLevelsTarget_SinglePageDataAccessLevels() {
    Fill_GridDataAccessLevelsTarget_SinglePageDataAccessLevels();
}

function Fill_GridDataAccessLevelsTarget_SinglePageDataAccessLevels() {
    document.getElementById('loadingPanel_GridDataAccessLevelsTarget_SinglePageDataAccessLevels').innerHTML = document.getElementById('hfloadingPanel_GridDataAccessLevelsTarget_SinglePageDataAccessLevels').value;
    var ObjDialogMasterDataAccessLevels = parent.parent.DialogMasterDataAccessLevels.get_value();
    var UserID = ObjDialogMasterDataAccessLevels.UserID;
    var SearchKey = ObjDialogMasterDataAccessLevels.SearchKey;
    var SearchTerm = ObjDialogMasterDataAccessLevels.SearchTerm;
    var DataAccessLevelKey = ObjDataAccessLevel_SinglePageDataAccessLevels != null ? ObjDataAccessLevel_SinglePageDataAccessLevels.Key : '';
    var DataAccessLevelOperationType = ObjDialogMasterDataAccessLevels.DataAccessLevelOperationType;
    CallBack_GridDataAccessLevelsTarget_SinglePageDataAccessLevels.callback(CharToKeyCode_SinglePageDataAccessLevels(UserID), CharToKeyCode_SinglePageDataAccessLevels(DataAccessLevelKey), CharToKeyCode_SinglePageDataAccessLevels(DataAccessLevelOperationType), CharToKeyCode_SinglePageDataAccessLevels(OperationState_SinglePageDataAccessLevels), CharToKeyCode_SinglePageDataAccessLevels(SearchKey), CharToKeyCode_SinglePageDataAccessLevels(SearchTerm));
}

function GridDataAccessLevelsTarget_SinglePageDataAccessLevels_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridDataAccessLevelsTarget_SinglePageDataAccessLevels').innerHTML = '';
}

function CallBack_GridDataAccessLevelsTarget_SinglePageDataAccessLevels_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DataAccessLevelsTarget').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            Fill_GridDataAccessLevelsTarget_SinglePageDataAccessLevels();
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}

function CallBack_GridDataAccessLevelsTarget_SinglePageDataAccessLevels_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridDataAccessLevelsTarget_SinglePageDataAccessLevels').innerHTML = '';
    ShowConnectionError_SinglePageDataAccessLevels();
}

function CharToKeyCode_SinglePageDataAccessLevels(str) {
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

function ShowConnectionError_SinglePageDataAccessLevels() {
    var error = document.getElementById('hfErrorType_SinglePageDataAccessLevels').value;
    var errorBody = document.getElementById('hfConnectionError_SinglePageDataAccessLevels').value;
    showDialog(error, errorBody, 'error');
}

function GridDataAccessLevelsTarget_SinglePageDataAccessLevels_onItemSelect(sender, e) {
    var ObjDialogMasterDataAccessLevels = parent.parent.DialogMasterDataAccessLevels.get_value();
    var DataAccessLevelOperationType = ObjDialogMasterDataAccessLevels.DataAccessLevelOperationType;
        if (DataAccessLevelOperationType == 'Single') {
        switch (parent.parent.CurrentLangID) {
            case 'fa-IR':
                ImageUrl = 'arrow-right.png';
                break;
            case 'en-US':
                ImageUrl = 'arrow-left.png';
                break;
        }
        enabled = true;
    }
    else {
        switch (parent.parent.CurrentLangID) {
            case 'fa-IR':
                ImageUrl = 'arrow-right_silver.png';
                break;
            case 'en-US':
                ImageUrl = 'arrow-left_silver.png';
                break;
        }
        enabled = false;
    }
    TlbInterAction_SinglePageDataAccessLevels.beginUpdate();
    TlbInterAction_SinglePageDataAccessLevels.get_items().getItemById('tlbItemDelete_TlbInterAction_SinglePageDataAccessLevels').set_enabled(enabled);
    TlbInterAction_SinglePageDataAccessLevels.get_items().getItemById('tlbItemDelete_TlbInterAction_SinglePageDataAccessLevels').set_imageUrl(ImageUrl);
    TlbInterAction_SinglePageDataAccessLevels.endUpdate();
}



