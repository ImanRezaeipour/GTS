
var ObjDataAccessLevel_MultiPageDataAccessLevels = null;
var CurrentPageState_MultiPageDataAccessLevels = 'View';
var LoadState_DataAccessLevelsSource_MultiPageDataAccessLevels = 'Normal';
var LoadState_DataAccessLevelsTarget_MultiPageDataAccessLevels = 'Normal';
var CurrentPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels = 0;
var CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels = 0;
var OperationState_MultiPageDataAccessLevels = 'Before';

function GetObjDataAccessLevel_MultiPageDataAccessLevels() {
    ObjDataAccessLevel_MultiPageDataAccessLevels = parent.GetObjDataAccessLevel_MasterDataAccessLevels();
}

function ChangeTargetQuickSearchEnabled_MultiPageDataAccessLevels() {
    var ObjDialogMasterDataAccessLevels = parent.parent.DialogMasterDataAccessLevels.get_value();
    var DataAccessLevelOperationType = ObjDialogMasterDataAccessLevels.DataAccessLevelOperationType;
    if (DataAccessLevelOperationType == 'Single') {
        switch (parent.parent.CurrentLangID) {
            case 'fa-IR':
                ImageUrl = 'search.png';
                break;
            case 'en-US':
                ImageUrl = 'search.png';
                break;
        }
        enabled = true;
    }
    else {
        switch (parent.parent.CurrentLangID) {
            case 'fa-IR':
                ImageUrl = 'search_silver.png';
                break;
            case 'en-US':
                ImageUrl = 'search_silver.png';
                break;
        }
        enabled = false;
    }
    TlbDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels.beginUpdate();
    TlbDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels.get_items().getItemById('tlbItemSearch_TlbDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels').set_enabled(enabled);
    TlbDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels.get_items().getItemById('tlbItemSearch_TlbDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels').set_imageUrl(ImageUrl);
    TlbDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels.endUpdate();
}

function GetBoxesHeaders_MultiPageDataAccessLevels() {
    if (ObjDataAccessLevel_MultiPageDataAccessLevels != null) {
        document.getElementById('header_DataAccessLevelsSource_MultiPageDataAccessLevels').innerHTML = ObjDataAccessLevel_MultiPageDataAccessLevels.Source;
        document.getElementById('header_DataAccessLevelsTarget_MultiPageDataAccessLevels').innerHTML = ObjDataAccessLevel_MultiPageDataAccessLevels.Target;
    }
    document.getElementById('footer_GridDataAccessLevelsSource_MultiPageDataAccessLevels').innerHTML = document.getElementById('hffooter_GridDataAccessLevelsSource_MultiPageDataAccessLevels').value;
    document.getElementById('footer_GridDataAccessLevelsTarget_MultiPageDataAccessLevels').innerHTML = document.getElementById('hffooter_GridDataAccessLevelsTarget_MultiPageDataAccessLevels').value;
}

function tlbItemSearch_TlbDataAccessLevelsSourceQuickSearch_MultiPageDataAccessLevels_onClick() {
    LoadState_DataAccessLevelsSource_MultiPageDataAccessLevels = 'Search';
    SetPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels(0);
}

function Refresh_GridDataAccessLevelsSource_MultiPageDataAccessLevels() {
    LoadState_DataAccessLevelsSource_MultiPageDataAccessLevels = 'Normal';
    SetPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels(0);
}

function CallBack_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DataAccessLevelsSource').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else
        Changefooter_GridDataAccessLevelsSource_MultiPageDataAccessLevels();
}

function CallBack_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onCallbackError(sender, e) {
    ShowConnectionError_MultiPageDataAccessLevels();
}

function tlbItemRefresh_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onClick() {
    Refresh_GridDataAccessLevelsSource_MultiPageDataAccessLevels();
}

function tlbItemFirst_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onClick() {
    SetPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels(0);
}

function tlbItemBefore_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onClick() {
    if (CurrentPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels != 0) {
        CurrentPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels = CurrentPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels - 1;
        SetPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels(CurrentPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels);
    }
}

function tlbItemNext_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onClick() {
    if (CurrentPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels < parseInt(document.getElementById('hfDataAccessLevelsSourcePageCount_MultiPageDataAccessLevels').value) - 1) {
        CurrentPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels = CurrentPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels + 1;
        SetPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels(CurrentPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels);
    }
}

function tlbItemLast_TlbPaging_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onClick() {
    SetPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels(parseInt(document.getElementById('hfDataAccessLevelsSourcePageCount_MultiPageDataAccessLevels').value) - 1);
}

function SetPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels(pageIndex) {
    CurrentPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels = pageIndex;
    Fill_GridDataAccessLevelsSource_MultiPageDataAccessLevels(pageIndex);
}

function Fill_GridDataAccessLevelsSource_MultiPageDataAccessLevels(pageIndex) {
    document.getElementById('loadingPanel_GridDataAccessLevelsSource_MultiPageDataAccessLevels').innerHTML = document.getElementById('hfloadingPanel_GridDataAccessLevelsSource_MultiPageDataAccessLevels').value;
    var DataAccessLevelKey = ObjDataAccessLevel_MultiPageDataAccessLevels != null ? ObjDataAccessLevel_MultiPageDataAccessLevels.Key : '';
    var pageSize = parseInt(document.getElementById('hfDataAccessLevelsSourcePageSize_MultiPageDataAccessLevels').value);
    switch (LoadState_DataAccessLevelsSource_MultiPageDataAccessLevels) {
        case 'Normal':
            seachTerm = '';
            break;
        case 'Search':
            var seachTerm = document.getElementById('txtDataAccessLevelsSourceQuickSearch_MultiPageDataAccessLevels').value;
            break;
    }
    CallBack_GridDataAccessLevelsSource_MultiPageDataAccessLevels.callback(CharToKeyCode_MultiPageDataAccessLevels(DataAccessLevelKey), CharToKeyCode_MultiPageDataAccessLevels(LoadState_DataAccessLevelsSource_MultiPageDataAccessLevels), CharToKeyCode_MultiPageDataAccessLevels(pageSize.toString()), CharToKeyCode_MultiPageDataAccessLevels(pageIndex.toString()), CharToKeyCode_MultiPageDataAccessLevels(seachTerm));
}

function tlbItemAdd_TlbInterAction_MultiPageDataAccessLevels_onClick() {
    CurrentPageState_MultiPageDataAccessLevels = 'Add';
    UpdateDataAccessLevels_MultiPageDataAccessLevels();
}

function tlbItemDelete_TlbInterAction_MultiPageDataAccessLevels_onClick() {
    CurrentPageState_MultiPageDataAccessLevels = 'Delete';
    UpdateDataAccessLevels_MultiPageDataAccessLevels();
}

function UpdateDataAccessLevels_MultiPageDataAccessLevels() {
    var ObjDialogMasterDataAccessLevels = parent.parent.DialogMasterDataAccessLevels.get_value();
    var DataAccessLevelOperationType = ObjDialogMasterDataAccessLevels.DataAccessLevelOperationType;
    var UserID = ObjDialogMasterDataAccessLevels.UserID;
    var SearchKey = ObjDialogMasterDataAccessLevels.SearchKey;
    var SearchTerm = ObjDialogMasterDataAccessLevels.SearchTerm;
    var DataAccessLevelKey = ObjDataAccessLevel_MultiPageDataAccessLevels != null ? ObjDataAccessLevel_MultiPageDataAccessLevels.Key : '';
    var DataAccessLevelSourceID = '-1';
    var DataAccessLevelTargetID = '-1';
    switch (CurrentPageState_MultiPageDataAccessLevels) {
        case 'Add':
          if(document.getElementById('chbSelectAll_MultiPageDataAccessLevels').checked)
             DataAccessLevelSourceID = '0';
          else
          {
             var SelectedItems_GridDataAccessLevelsSource_MultiPageDataAccessLevels = GridDataAccessLevelsSource_MultiPageDataAccessLevels.getSelectedItems();             
             if(SelectedItems_GridDataAccessLevelsSource_MultiPageDataAccessLevels.length > 0)
                DataAccessLevelSourceID = SelectedItems_GridDataAccessLevelsSource_MultiPageDataAccessLevels[0].getMember('ID').get_text();
          }
          break;            
        case 'Delete':
             var SelectedItems_GridDataAccessLevelsTarget_MultiPageDataAccessLevels = GridDataAccessLevelsTarget_MultiPageDataAccessLevels.getSelectedItems();             
             if(SelectedItems_GridDataAccessLevelsTarget_MultiPageDataAccessLevels.length > 0)
                DataAccessLevelTargetID = SelectedItems_GridDataAccessLevelsTarget_MultiPageDataAccessLevels[0].getMember('ID').get_text();          
          break;
    }
    UpdateDataAccessLevels_MultiPageDataAccessLevelsPage(CharToKeyCode_MultiPageDataAccessLevels(CurrentPageState_MultiPageDataAccessLevels), CharToKeyCode_MultiPageDataAccessLevels(DataAccessLevelKey), CharToKeyCode_MultiPageDataAccessLevels(DataAccessLevelOperationType), CharToKeyCode_MultiPageDataAccessLevels(UserID), CharToKeyCode_MultiPageDataAccessLevels(SearchKey), CharToKeyCode_MultiPageDataAccessLevels(SearchTerm), CharToKeyCode_MultiPageDataAccessLevels(DataAccessLevelSourceID), CharToKeyCode_MultiPageDataAccessLevels(DataAccessLevelTargetID));
    DialogWaiting.Show();
}

function UpdateDataAccessLevels_MultiPageDataAccessLevelsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_MultiPageDataAccessLevels').value;
            Response[1] = document.getElementById('hfConnectionError_MultiPageDataAccessLevels').value;
        }
        if (RetMessage[2] == 'success') {
            OperationState_MultiPageDataAccessLevels = 'After';
            UpdateFeatures_GridDataAccessLevelsTarget_MultiPageDataAccessLevels();
            CurrentPageState_MultiPageDataAccessLevels = 'View';
            document.getElementById('chbSelectAll_MultiPageDataAccessLevels').checked = false;
        }
        else 
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function UpdateFeatures_GridDataAccessLevelsTarget_MultiPageDataAccessLevels() {
    var DataAccessLevelsTargetCount = parseInt(document.getElementById('hfDataAccessLevelsTargetCount_MultiPageDataAccessLevels').value);
    var DataAccessLevelsTargetPageCount = parseInt(document.getElementById('hfDataAccessLevelsTargetPageCount_MultiPageDataAccessLevels').value);
    var DataAccessLevelsTargetPageSize = parseInt(document.getElementById('hfDataAccessLevelsTargetPageSize_MultiPageDataAccessLevels').value);
    var Lag = 0;
    switch (CurrentPageState_MultiPageDataAccessLevels) {
        case 'Add':
            Lag = Lag + 1;
            break;
        case 'Delete':
            Lag = Lag - 1;
            break;
    }
    if ((DataAccessLevelsTargetCount > 0 && CurrentPageState_MultiPageDataAccessLevels == 'Delete') || CurrentPageState_MultiPageDataAccessLevels == 'Add') {
        DataAccessLevelsTargetCount = DataAccessLevelsTargetCount + Lag;
        var divRem = mod(DataAccessLevelsTargetCount, DataAccessLevelsTargetPageSize);
        switch (CurrentPageState_MultiPageDataAccessLevels) {
            case 'Add':
                if (GridDataAccessLevelsTarget_MultiPageDataAccessLevels.get_table().getRowCount() > DataAccessLevelsTargetPageSize) {
                    DataAccessLevelsTargetPageCount = DataAccessLevelsTargetPageCount + Lag;
                    CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels = CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels + Lag;
                }
                else
                    CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels = 0;
                break;
            case 'Delete':
                if (divRem == 0) {
                    DataAccessLevelsTargetPageCount = DataAccessLevelsTargetPageCount + Lag;
                    if (CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels == DataAccessLevelsTargetPageCount) {
                        CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels = CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels + Lag >= 0 ? CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels + Lag : 0;
                    }
                }
                break;
        }
        SetPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels);
        document.getElementById('hfDataAccessLevelsTargetCount_MultiPageDataAccessLevels').value = DataAccessLevelsTargetCount.toString();
        document.getElementById('hfDataAccessLevelsTargetPageCount_MultiPageDataAccessLevels').value = DataAccessLevelsTargetPageCount.toString();
        Changefooter_GridDataAccessLevelsTarget_MultiPageDataAccessLevels();
    }
}

function mod(a, b) {
    return a - (b * Math.floor(a / b));
}

function Changefooter_GridDataAccessLevelsSource_MultiPageDataAccessLevels() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridDataAccessLevelsSource_MultiPageDataAccessLevels').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfDataAccessLevelsSourcePageCount_MultiPageDataAccessLevels').value) > 0 ? CurrentPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfDataAccessLevelsSourcePageCount_MultiPageDataAccessLevels').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridDataAccessLevelsSource_MultiPageDataAccessLevels').innerHTML = retfooterVal;
}


function Changefooter_GridDataAccessLevelsTarget_MultiPageDataAccessLevels() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridDataAccessLevelsTarget_MultiPageDataAccessLevels').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfDataAccessLevelsTargetPageCount_MultiPageDataAccessLevels').value) > 0 ? CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfDataAccessLevelsTargetPageCount_MultiPageDataAccessLevels').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridDataAccessLevelsTarget_MultiPageDataAccessLevels').innerHTML = retfooterVal;
}


function tlbItemSearch_TlbDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels_onClick() {
    LoadState_DataAccessLevelsTarget_MultiPageDataAccessLevels = 'Search';
    SetPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(0);
}

function Refresh_GridDataAccessLevelsTarget_MultiPageDataAccessLevels() {
    LoadState_DataAccessLevelsTarget_MultiPageDataAccessLevels = 'Normal';
    SetPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(0);
}

function CallBack_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onCallbackComplete(sender, e) {
        var error = document.getElementById('ErrorHiddenField_DataAccessLevelsTarget').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else
        Changefooter_GridDataAccessLevelsTarget_MultiPageDataAccessLevels();
}

function CallBack_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onCallbackError(sender, e) {
    ShowConnectionError_MultiPageDataAccessLevels();
}

function tlbItemRefresh_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onClick() {
    Refresh_GridDataAccessLevelsTarget_MultiPageDataAccessLevels();
}

function tlbItemFirst_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onClick() {
    SetPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(0);
}

function tlbItemBefore_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onClick() {
    if (CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels != 0) {
        CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels = CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels - 1;
        SetPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels);
    }
}

function tlbItemNext_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onClick() {
    if (CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels < parseInt(document.getElementById('hfDataAccessLevelsTargetPageCount_MultiPageDataAccessLevels').value) - 1) {
        CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels = CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels + 1;
        SetPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels);
    }
}

function tlbItemLast_TlbPaging_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onClick() {
    SetPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(parseInt(document.getElementById('hfDataAccessLevelsTargetPageCount_MultiPageDataAccessLevels').value) - 1);    
}

function SetPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(pageIndex) {
    CurrentPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels = pageIndex;
    Fill_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(pageIndex);
}

function Fill_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(pageIndex) {
    document.getElementById('loadingPanel_GridDataAccessLevelsTarget_MultiPageDataAccessLevels').innerHTML = document.getElementById('hfloadingPanel_GridDataAccessLevelsTarget_MultiPageDataAccessLevels').value;
    var ObjDialogMasterDataAccessLevels = parent.parent.DialogMasterDataAccessLevels.get_value();
    var UserID = ObjDialogMasterDataAccessLevels.UserID;
    var UserSearchKey = ObjDialogMasterDataAccessLevels.SearchKey;
    var UserSearchTerm = ObjDialogMasterDataAccessLevels.SearchTerm;
    var DataAccessLevelKey = ObjDataAccessLevel_MultiPageDataAccessLevels != null ? ObjDataAccessLevel_MultiPageDataAccessLevels.Key : '';
    var DataAccessLevelOperationType = ObjDialogMasterDataAccessLevels.DataAccessLevelOperationType;
    var pageSize = parseInt(document.getElementById('hfDataAccessLevelsTargetPageSize_MultiPageDataAccessLevels').value);
    switch (LoadState_DataAccessLevelsTarget_MultiPageDataAccessLevels) {
        case 'Normal':
            seachTerm = '';
            break;
        case 'Search':
            var seachTerm = document.getElementById('txtDataAccessLevelsTargetQuickSearch_MultiPageDataAccessLevels').value;
            break;
    }
    CallBack_GridDataAccessLevelsTarget_MultiPageDataAccessLevels.callback(CharToKeyCode_MultiPageDataAccessLevels(UserID), CharToKeyCode_MultiPageDataAccessLevels(DataAccessLevelKey), CharToKeyCode_MultiPageDataAccessLevels(DataAccessLevelOperationType), CharToKeyCode_MultiPageDataAccessLevels(LoadState_DataAccessLevelsTarget_MultiPageDataAccessLevels), CharToKeyCode_MultiPageDataAccessLevels(pageSize.toString()), CharToKeyCode_MultiPageDataAccessLevels(pageIndex.toString()), CharToKeyCode_MultiPageDataAccessLevels(seachTerm), CharToKeyCode_MultiPageDataAccessLevels(OperationState_MultiPageDataAccessLevels), CharToKeyCode_MultiPageDataAccessLevels(UserSearchKey), CharToKeyCode_MultiPageDataAccessLevels(UserSearchTerm));
}

function CharToKeyCode_MultiPageDataAccessLevels(str) {
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

function ShowConnectionError_MultiPageDataAccessLevels() {
    var error = document.getElementById('hfErrorType_MultiPageDataAccessLevels').value;
    var errorBody = document.getElementById('hfConnectionError_MultiPageDataAccessLevels').value;
    showDialog(error, errorBody, 'error');
}

function GridDataAccessLevelsSource_MultiPageDataAccessLevels_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridDataAccessLevelsSource_MultiPageDataAccessLevels').innerHTML = '';
}

function GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridDataAccessLevelsTarget_MultiPageDataAccessLevels').innerHTML = '';
}

function GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onItemSelect(sender, e) {
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
    TlbInterAction_MultiPageDataAccessLevels.beginUpdate();
    TlbInterAction_MultiPageDataAccessLevels.get_items().getItemById('tlbItemDelete_TlbInterAction_MultiPageDataAccessLevels').set_enabled(enabled);
    TlbInterAction_MultiPageDataAccessLevels.get_items().getItemById('tlbItemDelete_TlbInterAction_MultiPageDataAccessLevels').set_imageUrl(ImageUrl);
    TlbInterAction_MultiPageDataAccessLevels.endUpdate();
}










