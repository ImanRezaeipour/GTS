
var BaseCallbackPrefix_GridManagers_Managers = null;

function SetBaseCallbackPrefix_GridManagers_Managers() {
    BaseCallbackPrefix_GridManagers_Managers = CallBack_GridManagers_Managers.CallbackPrefix;
}

function GridManagers_Managers_onItemSelect(sender, e) {
}

function GridManagers_Managers_onItemExpand(sender, e) {
    GridManagers_Managers.render();
}

function GridManagers_Managers_onItemCollapse(sender, e) {
}

function Fill_GridManagers_Managers(LoadState, pageIndex) {
    var AccessGroupID = '0';
    var SearchField = '';
    var SearchTerm = '';

    parent.parent.document.getElementById('pgvMasterManagersIntroduction_iFrame').contentWindow.SetLoadingPanel_GridManagers_Managers();
    var pageSize = parseInt(document.getElementById('hfManagesPageSize_Managers').value);
    switch (LoadState) {
        case 'Normal':
            break;
        case 'Filter':
            var selectedFilterItem = parent.parent.document.getElementById('pgvMasterManagersIntroduction_iFrame').contentWindow.GetSelectedItem_cmbAccessGroup_MasterManagers();
            if (selectedFilterItem != undefined)
                AccessGroupID = selectedFilterItem.get_value();
            break;
        case 'Search':
            var searchObj = parent.parent.document.getElementById('pgvMasterManagersIntroduction_iFrame').contentWindow.GetSearchObj_MasterManagers();
            var selectedSearchField = searchObj.SearchField;
            if (selectedSearchField != undefined)
                SearchField = selectedSearchField.get_value();
            else
                SearchField = 'NotSpecified';
            SearchTerm = searchObj.SearchTerm;
            break;
    }
    CallBack_GridManagers_Managers.CallbackPrefix = BaseCallbackPrefix_GridManagers_Managers + '?LoadState=' + CharToKeyCode_Managers(LoadState) + '&PageIndex=' + CharToKeyCode_Managers(pageIndex.toString()) + '&FilterBy=' + CharToKeyCode_Managers(AccessGroupID) + '&SearchField=' + CharToKeyCode_Managers(SearchField) + '&SearchTerm=' + CharToKeyCode_Managers(SearchTerm);
    CallBack_GridManagers_Managers.callback();
}

function CharToKeyCode_Managers(str) {
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


function CallBack_GridManagers_Managers_onLoad(sender, e) {
    SetBaseCallbackPrefix_GridManagers_Managers();
}

function GetManagersPageCount_Managers() {
    return parseInt(document.getElementById('hfManagersPageCount_Managers').value);
}

function CallBack_GridManagers_Managers_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Managers_Managers').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        parent.parent.document.getElementById('pgvMasterManagersIntroduction_iFrame').contentWindow.ShowError_GridManagers_Managers_onCallBackCompleted(errorParts);
        if (errorParts[3] == 'Reload')
            document.getElementById('pgvMasterManagersIntroduction_iFrame').contentWindow.SetPageIndex_GridManagers_MasterManagers(0);
    }
    else {
        var managerPageCount = document.getElementById('hfManagersPageCount_Managers').value;
        parent.parent.document.getElementById('pgvMasterManagersIntroduction_iFrame').contentWindow.Changeheader_GridManagers_Managers();
        parent.parent.document.getElementById('pgvMasterManagersIntroduction_iFrame').contentWindow.Changefooter_GridManagers_Managers(managerPageCount);
    }
}

function GetSelectedManagerItem_GridManagers_Managers() {
    var SelectedItems_GridManagers_Managers = GridManagers_Managers.getSelectedItems();
    if (SelectedItems_GridManagers_Managers.length > 0) {
        if (SelectedItems_GridManagers_Managers[0].get_table().get_level() > 0)
            return SelectedItems_GridManagers_Managers[0];
        else
            return null;
    }
    else
        return null;
}

function CallBack_GridManagers_Managers_onCallbackError(sender, e) {
    parent.document.getElementById('loadingPanel_GridManagers_MasterManagers').innerHTML = '';
    ShowConnectionError_Managers();
}

function ShowConnectionError_Managers() {
    var error = document.getElementById('hfErrorType_Managers').value;
    var errorBody = document.getElementById('hfConnectionError_Managers').value;
    showDialog(error, errorBody, 'error');
}




