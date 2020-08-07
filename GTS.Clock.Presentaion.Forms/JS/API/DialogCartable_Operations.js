
var LastPageIndex_GridCartable_Cartable = 0;
var CurrentPageIndex_GridCartable_Cartable = 0;
var LoadState_Cartable = null;
var ConfirmState_Cartable = null;
var CurrentPageState_Cartable = 'View';
var StrSelectedRequests_Cartable = '';
var ObjCartable_Cartable = null;
var StrFilterConditions_Cartable = '';

function GetDefaultLoadState_Cartable() {
    var ObjDialogCartable_Cartable = parent.DialogCartable.get_value();
    var RequestCaller = ObjDialogCartable_Cartable.RequestCaller;
    switch (RequestCaller) {
        case 'Cartable':
            LoadState_Cartable = 'None';
            break;
        case 'Survey':
            LoadState_Cartable = 'UnKnown';
            break;
        case 'Sentry':
            LoadState_Cartable = 'None';
            break;
    }
    return LoadState_Cartable;
}

function GetBoxesHeaders_Cartable() {
    var TitleDialog_Cartable = '';
    var HeaderGrid_Cartable = '';
    var RequestCaller = parent.DialogCartable.get_value().RequestCaller;
    switch (RequestCaller) {
        case 'Cartable':
            TitleDialog_Cartable = document.getElementById('hfTitle_DialogCartable').value;
            HeaderGrid_Cartable = document.getElementById('hfheader_Cartable_Cartable').value;
            break;
        case 'Survey':
            TitleDialog_Cartable = document.getElementById('hfTitle_DialogSurveyedRequests').value;
            HeaderGrid_Cartable = document.getElementById('hfheader_SurveyedRequests_Cartable').value;
            break;
        case 'Sentry':
            TitleDialog_Cartable = document.getElementById('hfTitle_DialogSentry').value;
            HeaderGrid_Cartable = document.getElementById('hfheader_SentryCartable_Cartable').value;
            break;
    }
    parent.document.getElementById('Title_DialogCartable').innerHTML = TitleDialog_Cartable;
    document.getElementById('header_Cartable_Cartable').innerHTML = HeaderGrid_Cartable;
    document.getElementById('beginfooter_GridCartable_Cartable').innerHTML = document.getElementById('endfooter_GridCartable_Cartable').innerHTML = document.getElementById('hffooter_GridCartable_Cartable').value;
}

function ChangeDirection_cmbControls_Cartable() {
    var RequestCaller = parent.DialogCartable.get_value().RequestCaller;
    if (RequestCaller == 'Cartable' || RequestCaller == 'Survey') {
        ChangeComboDirection_MasterMonthlyOperation('cmbYear_Cartable');
        ChangeComboDirection_MasterMonthlyOperation('cmbMonth_Cartable');
    }
    if (RequestCaller == 'Sentry')
        ChangeComboDirection_MasterMonthlyOperation('cmbSortBy_Cartable');
}

function ChangeDateControlContainersWidth_Cartable() {
    var RequestCaller = parent.DialogCartable.get_value().RequestCaller;
    if (RequestCaller == 'Cartable' || RequestCaller == 'Survey') {
        document.getElementById('Container_DateCalendars_RequestRegister').style.width = '0px';
    }
    if (RequestCaller == 'Sentry') {
    }
}

function ShowDialogCartableFilter_Cartable() {
    DialogCartableFilter.Show();
    CollapseControls_Cartable();
}

function ShowDialogHistory_Cartable() {
    if (GridCartable_Cartable.getSelectedItems().length > 0) {
        var ObjHistory = new Object();
        ObjHistory.RequestID = GridCartable_Cartable.getSelectedItems()[0].getMember('RequestID').get_text();
        ObjHistory.RequestIssuer = GridCartable_Cartable.getSelectedItems()[0].getMember('Applicant').get_text();
        ObjHistory.RequestTitle = GridCartable_Cartable.getSelectedItems()[0].getMember('RequestTitle').get_text();
        DialogHistory.set_value(ObjHistory);
        DialogHistory.Show();
        CollapseControls_Cartable();
    }
}

function ShowDialogEndorsementFlowState_Cartable() {
    if (GridCartable_Cartable.getSelectedItems().length > 0) {
        var ObjEndorsementFlowState = new Object();
        ObjEndorsementFlowState.ManagerFlowID = GridCartable_Cartable.getSelectedItems()[0].getMember('ManagerFlowID').get_text();
        ObjEndorsementFlowState.RequestID = GridCartable_Cartable.getSelectedItems()[0].getMember('RequestID').get_text();
        parent.DialogEndorsementFlowState.set_value(ObjEndorsementFlowState);
        parent.DialogEndorsementFlowState.Show();
        CollapseControls_Cartable();
    }
}

function ShowDialogRequestRejectDescription_Cartable(state) {
    var description = null;
    switch (state) {
        case 'Reject':
            description = document.getElementById('hfRequestRejectDescription_Cartable').value;
            break;
        case 'Delete':
            description = document.getElementById('hfRequestDeleteDescription_Cartable').value;
            break;
    }
    document.getElementById('hfDescription_RequestReject_Cartable').innerHTML = description;
    DialogRequestRejectDescription.Show();
    CollapseControls_Cartable();
}

function SetHorizontalScrollingDirection_GridCartable_Cartable_Opera() {
    if (navigator.userAgent.indexOf('Opera') != -1 && parent.CurrentLangID == "fa-IR")
        document.getElementById('GridCartable_Cartable').style.direction = "ltr";
}

function ChangeDirection_Mastertbl_CartableForm() {
    if (parent.CurrentLangID == 'en-US')
        document.getElementById('Mastertbl_CartableForm').dir = 'ltr';
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('Mastertbl_CartableForm').dir = 'rtl';
    }
}

function DialogRequestRejectDescription_onShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('tbl_RequestRejectDescription_Cartable').dir = 'rtl';
    }
}

function ShowDialogRequestsState() {
    DialogRequestsState.Show();
    CollapseControls_Cartable();
}

function GridCartable_Cartable_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridCartable_Cartable').innerHTML = '';
    BaseCallBackPrefix_GridCartable_Cartable = GridCartable_Cartable.CallbackPrefix;
}

function CallBack_GridCartable_Cartable_onCallbackComplete(sender, e) {
    SetHorizontalScrollingDirection_GridCartable_Cartable_Opera();
    GridCartable_Cartable.render();
    var error = document.getElementById('ErrorHiddenField_Cartable').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridCartable_Cartable(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2], false, document.getElementById('Mastertbl_CartableForm').offsetWidth);
    }
    else
        Changefooter_GridCartable_Cartable();
}

function Changefooter_GridCartable_Cartable() {
    var retfooterVal = '';
    var footerVal = document.getElementById('beginfooter_GridCartable_Cartable').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfCartablePageCount_Cartable').value) > 0 ? CurrentPageIndex_GridCartable_Cartable + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfCartablePageCount_Cartable').value;
        if ((i == 1 || i == 3) && GridCartable_Cartable.get_table().getRowCount() == 0)
            footerValCol[i] = 0;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('beginfooter_GridCartable_Cartable').innerHTML = document.getElementById('endfooter_GridCartable_Cartable').innerHTML = retfooterVal;
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Cartable) {
        case 'PageChange':
            Fill_GridCartable_Cartable(CurrentPageIndex_GridCartable_Cartable);
            break;
        case 'Reject':
            DialogConfirm.Close();
            ShowDialogRequestRejectDescription_Cartable('Reject');
            break;
        case 'Exit':
            DialogCartable_onClose();
            break;
        default:
    }
    DialogConfirm.Close();
}

function DialogCartable_onClose() {
    parent.document.getElementById('DialogCartable_IFrame').src = 'WhitePage.aspx';
    parent.DialogCartable.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function tlbItemEndorsement_TlbCartable_onClick() {
    UpdateCartable_Cartable("Confirmed");
}

function tlbItemReject_TlbCartable_onClick() {
    ShowDialogRequestRejectDescription_Cartable('Reject');
}

function tlbItemDelete_TlbCartable_onClick() {
    ShowDialogRequestRejectDescription_Cartable('Delete');
}

function UpdateCartable_Cartable(CurrentPageState) {
    var ObjDialogCartable = parent.DialogCartable.get_value();
    var RequestCaller = ObjDialogCartable.RequestCaller;
    ObjCartable_Cartable = new Object();
    ObjCartable_Cartable.ManagerFlowID = '0';
    if (GridCartable_Cartable.getSelectedItems().length > 0)
        ObjCartable_Cartable.ManagerFlowID = GridCartable_Cartable.getSelectedItems()[0].getMember('ManagerFlowID').get_text();
    switch (CurrentPageState) {
        case 'Confirmed':
            ObjCartable_Cartable.ActionDescription = "";
            break;
        case 'Unconfirmed':
            ObjCartable_Cartable.ActionDescription = document.getElementById('txtDescription_RequestReject_Cartable').value;
            break;
        case 'Deleted':
            ObjCartable_Cartable.ActionDescription = document.getElementById('txtDescription_RequestReject_Cartable').value;
            var SelectedItems_GridCartable_Cartable = GridCartable_Cartable.getSelectedItems();
            if (SelectedItems_GridCartable_Cartable.length > 0)
                ChangeRequestsList_Cartable(SelectedItems_GridCartable_Cartable[0]);
    }
    ObjCartable_Cartable.StrSelectedRequests = StrSelectedRequests_Cartable;
    UpdateCartable_CartablePage(CharToKeyCode_Cartable(RequestCaller), CharToKeyCode_Cartable(CurrentPageState), CharToKeyCode_Cartable(ObjCartable_Cartable.StrSelectedRequests), CharToKeyCode_Cartable(ObjCartable_Cartable.ActionDescription));
}

function UpdateCartable_CartablePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Cartable').value;
            Response[1] = document.getElementById('hfConnectionError_Cartable').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2], false, document.getElementById('Mastertbl_CartableForm').offsetWidth);
        if (RetMessage[2] == 'success') {
            //Cartable_OnAfterUpdate(Response);
            //UpdateFeatures_GridCartable_Cartable();
            Cartable_OnAfterUpdate();
        }
    }
}

function Cartable_OnAfterUpdate() {         
    var ObjDialogCartable = parent.DialogCartable.get_value();
    var RequestCaller = ObjDialogCartable.RequestCaller;
    ClearList_Cartable(RequestCaller);
    CurrentPageIndex_GridCartable_Cartable = 0;
    Fill_GridCartable_Cartable(0);
}

//function Cartable_OnAfterUpdate(Response) {
//    var ObjDialogCartable = parent.DialogCartable.get_value();
//    var RequestCaller = ObjDialogCartable.RequestCaller;
//    switch (RequestCaller) {
//        case 'Cartable':
//            var strSelectedRequests = ObjCartable_Cartable.StrSelectedRequests;
//            GridCartable_Cartable.beginUpdate();
//            for (var i = 0; i < GridCartable_Cartable.get_table().getRowCount(); i++) {
//                var gridItem = GridCartable_Cartable.get_table().getRow(i);
//                requestID = gridItem.getMember('RequestID').get_text();
//                if (strSelectedRequests.indexOf('#' + requestID + '#') >= 0)
//                    GridCartable_Cartable.deleteItem(gridItem);
//            }
//            GridCartable_Cartable.endUpdate();
//            break;
//        case 'Survey':
//            break;
//        case 'Sentry':
//            break;
//    }
//    ClearList_Cartable(RequestCaller);
//}

function ClearList_Cartable(RequestCaller) {
    StrSelectedRequests_Cartable = '';
    document.getElementById('txtDescription_RequestReject_Cartable').value = '';
    if (RequestCaller == 'Cartable')
        document.getElementById('chbSelectAllinthisPage_Cartable').checked = false;
}

function UpdateFeatures_GridCartable_Cartable() {
    var CartableCount = parseInt(document.getElementById('hfCartableCount_Cartable').value);
    var CartablePageCount = parseInt(document.getElementById('hfCartablePageCount_Cartable').value);
    var CartablePageSize = parseInt(document.getElementById('hfCartablePageSize_Cartable').value);
    if (CartableCount > 0) {
        CartableCount = CartableCount - 1;
        var divRem = mod(CartableCount, CartablePageSize);
        if (divRem == 0) {
            CartablePageCount = CartablePageCount - 1;
            if (CurrentPageIndex_GridCartable_Cartable == CartablePageCount)
                CurrentPageIndex_GridCartable_Cartable = CurrentPageIndex_GridCartable_Cartable - 1;
        }
        SetPageIndex_GridCartable_Cartable(CurrentPageIndex_GridCartable_Cartable);
        document.getElementById('hfCartableCount_Cartable').value = CartableCount.toString();
        document.getElementById('hfCartablePageCount_Cartable').value = CartablePageCount.toString();
        Changefooter_GridCartable_Cartable();
    }
}

function mod(a, b) {
    return a - (b * Math.floor(a / b));
}

function tlbItemHistory_TlbCartable_onClick() {
    ShowDialogHistory_Cartable();
}

function tlbItemFilter_TlbCartable_onClick() {
    ShowDialogCartableFilter_Cartable();
}

function Cartable_OnAfterCustomFilter(StrCustomFilter) {
    StrFilterConditions_Cartable = StrCustomFilter;
    ChangeLoadState_GridCartable_Cartable('CustomFilter');
}

function tlbItemSearch_TlbCartableQuickSearch_onClick() {
    StrFilterConditions_Cartable = document.getElementById('txtSerchTerm_Cartable').value;
    ChangeLoadState_GridCartable_Cartable('Search');
}


function tlbItemExit_TlbCartable_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Cartable = confirmState;
    switch (confirmState) {
        case 'Reject':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfRejectMessage_Cartable').value;
            break;
        case 'PageChange':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfPageChange_Cartable').value;
            break;
        case 'Exit':
            var ObjDialogCartable_Cartable = parent.DialogCartable.get_value();
            var RequestCaller = ObjDialogCartable_Cartable.RequestCaller;
            var CloseMessage_DialogCartable = null;
            switch (RequestCaller) {
                case 'Cartable':
                    CloseMessage_DialogCartable = document.getElementById('hfCartableCloseMessage_Cartable').value;
                    break;
                case 'Survey':
                    CloseMessage_DialogCartable = document.getElementById('hfSurveyCloseMessage_Cartable').value;
                    break;
                case 'Sentry':
                    CloseMessage_DialogCartable = document.getElementById('hfSentryCloseMessage_Cartable').value;
                    break;
            }
            document.getElementById('lblConfirm').innerHTML = CloseMessage_DialogCartable;
            break;
    }
    DialogConfirm.Show();
    CollapseControls_Cartable();
}

function tlbItemAllRequests_TlbCartableFilter_Cartable_onClick() {
    ChangeLoadState_GridCartable_Cartable(GetDefaultLoadState_Cartable());
}

function tlbItemDailyRequests_TlbCartableFilter_Cartable_onClick() {
    ChangeLoadState_GridCartable_Cartable('Daily');
}

function tlbItemHourlyRequests_TlbCartableFilter_Cartable_onClick() {
    ChangeLoadState_GridCartable_Cartable('Hourly');
}

function tlbItemOverTimeJustification_TlbCartableFilter_Cartable_onClick() {
    ChangeLoadState_GridCartable_Cartable('OverWork');
}

function tlbItemConfirmedRequests_TlbCartableFilter_Cartable_onClick() {
    ChangeLoadState_GridCartable_Cartable('Confirmed');
}

function tlbItemRejectedRequests_TlbCartableFilter_Cartable_onClick() {
    ChangeLoadState_GridCartable_Cartable('Unconfirmed');
}

function tlbItemDeletedRequests_TlbCartableFilter_Cartable_onClick() {
    ChangeLoadState_GridCartable_Cartable('Deleted');
}

function tlbItemRefresh_TlbPaging_GridCartable_Cartable_onClick() {
    ChangeLoadState_GridCartable_Cartable(GetDefaultLoadState_Cartable());
}

function ChangeLoadState_GridCartable_Cartable(state) {
    LoadState_Cartable = state;
    SetPageIndex_GridCartable_Cartable(0);
}

function tlbItemFirst_TlbPaging_GridCartable_Cartable_onClick() {
    SetPageIndex_GridCartable_Cartable(0);
}

function tlbItemBefore_TlbPaging_GridCartable_Cartable_onClick() {
    if (CurrentPageIndex_GridCartable_Cartable != 0) {
        CurrentPageIndex_GridCartable_Cartable = CurrentPageIndex_GridCartable_Cartable - 1;
        SetPageIndex_GridCartable_Cartable(CurrentPageIndex_GridCartable_Cartable);
    }
}

function tlbItemNext_TlbPaging_GridCartable_Cartable_onClick() {
    if (CurrentPageIndex_GridCartable_Cartable < parseInt(document.getElementById('hfCartablePageCount_Cartable').value) - 1) {
        CurrentPageIndex_GridCartable_Cartable = CurrentPageIndex_GridCartable_Cartable + 1;
        SetPageIndex_GridCartable_Cartable(CurrentPageIndex_GridCartable_Cartable);
    }
}

function tlbItemLast_TlbPaging_GridCartable_Cartable_onClick() {
    SetPageIndex_GridCartable_Cartable(parseInt(document.getElementById('hfCartablePageCount_Cartable').value) - 1);
}

function SetPageIndex_GridCartable_Cartable(pageIndex) {
    CurrentPageIndex_GridCartable_Cartable = pageIndex;
    var ObjDialogCartable = parent.DialogCartable.get_value();
    var RequestCaller = ObjDialogCartable.RequestCaller;
    switch (RequestCaller) {
        case 'Cartable':
            if (StrSelectedRequests_Cartable == '')
                Fill_GridCartable_Cartable(pageIndex);
            else
                ShowDialogConfirm('PageChange');
            break;
        case 'Survey':
            Fill_GridCartable_Cartable(pageIndex);
            break;
        case 'Sentry':
            Fill_GridCartable_Cartable(pageIndex);
            break;
    }
}

function ConfirmPageChange_GridCartable_Cartable() { 
}

function cmbYear_Cartable_onChange(sender, e) {
    document.getElementById('hfCurrentYear_Cartable').value = cmbYear_Cartable.getSelectedItem().get_value();
}

function cmbMonth_Cartable_onChange(sender, e) {
    document.getElementById('hfCurrentMonth_Cartable').value = cmbMonth_Cartable.getSelectedItem().get_value();
}

function Fill_GridCartable_Cartable(pageIndex) {
    document.getElementById('loadingPanel_GridCartable_Cartable').innerHTML = document.getElementById('hfloadingPanel_GridCartable_Cartable').value;
    var ObjDialogCartable = parent.DialogCartable.get_value();
    var RequestCaller = ObjDialogCartable.RequestCaller;
    var pageSize = parseInt(document.getElementById('hfCartablePageSize_Cartable').value);
    var sortBy = document.getElementById('hfCurrentSortBy_Cartable').value;
    var year = '0';
    var month = '0';
    var date = null;
    if (RequestCaller == 'Cartable' || RequestCaller == 'Survey') {
        year = document.getElementById('hfCurrentYear_Cartable').value;
        month = document.getElementById('hfCurrentMonth_Cartable').value;
    }
    if (RequestCaller == 'Sentry') {
        switch (parent.SysLangID) {
            case 'fa-IR':
                date = document.getElementById('pdpDate_Cartable').value;
                break;
            case 'en-US':
                date = document.getElementById('gdpDate_Cartable').value;
                break;
        }
    }
    CallBack_GridCartable_Cartable.callback(CharToKeyCode_Cartable(RequestCaller), CharToKeyCode_Cartable(LoadState_Cartable), CharToKeyCode_Cartable(year), CharToKeyCode_Cartable(month), CharToKeyCode_Cartable(date), CharToKeyCode_Cartable(StrFilterConditions_Cartable), CharToKeyCode_Cartable(sortBy), CharToKeyCode_Cartable(pageSize.toString()), CharToKeyCode_Cartable(pageIndex.toString()));
}

function CustomizeTlbCartable_Cartable() {
    var RequestCaller = parent.DialogCartable.get_value().RequestCaller;
    CallBackTlbCartable_Cartable.callback(CharToKeyCode_Cartable(RequestCaller));
}

function CustomizeTlbCartableFilter_Cartable() {
    var RequestCaller = parent.DialogCartable.get_value().RequestCaller;
    CallBackTlbCartableFilter_Cartable.callback(CharToKeyCode_Cartable(RequestCaller));
}

function CharToKeyCode_Cartable(str) {
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

function tlbItemView_TlbView_Cartable_onClick() {
    ChangeLoadState_GridCartable_Cartable(GetDefaultLoadState_Cartable());
}

function tlbItemEndorsement_TlbRequestReject_Cartable_onClick() {
    var ObjDialogCartable = parent.DialogCartable.get_value();
    var RequestCaller = ObjDialogCartable.RequestCaller;
    var RequestState = null;
    switch (RequestCaller) {
        case 'Cartable':
            RequestState = 'Unconfirmed';
            break;
        case 'Survey':
            RequestState = 'Deleted';
            break;
    }
    DialogRequestRejectDescription.Close();
    UpdateCartable_Cartable(RequestState);
}

function tlbItemCancel_TlbRequestReject_Cartable_onClick() {
    DialogRequestRejectDescription.Close();
    document.getElementById('txtDescription_RequestReject_Cartable').value = '';
}

function SetClmnImage_GridCartable_Cartable(dataField, Key) {
    var cellImage = '';
    switch (dataField) {
        case 'FlowStatus':
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
            break;
        case 'FlowLevels':
            cellImage = 'Images/Grid/info.png';
            break;
        case 'RequestType':
            switch (Key.toString()) {
                case '0':
                    cellImage = 'Images/Grid/all.png';
                    break;
                case '1':
                    cellImage = 'Images/Grid/clock.png';
                    break;
                case '2':
                    cellImage = 'Images/Grid/day.png';
                    break;
                case '3':
                    cellImage = 'Images/Grid/Permission.png';
                    break;
            }
            break;
        case 'RequestSource':
            switch (Key.toString()) {
                case '0':
                    cellImage = 'Images/Grid/user.png';
                    break;
                case '1':
                    cellImage = 'Images/Grid/role.png';
                    break;
            }
            break;
    }
    return cellImage;
}

function GetRequestFlowLevel_GridCartable_Cartable() {
    ShowDialogEndorsementFlowState_Cartable();
}

function SetCellTitle_GridCartable_Cartable(dataField, Key) {
    var elementID = null;
    switch (dataField) {
        case 'FlowStatus':
            elementID = 'hfRequestStates_Cartable';
            break;
        case 'RequestType':
            elementID = 'hfRequestTypes_Cartable';
            break;
        case 'RequestSource':
            elementID = 'hfRequestSources_Cartable';
            break;
    }
    strListObj = document.getElementById(elementID).value.split('#');
    for (var i = 0; i < strListObj.length; i++) {
        strListItemObj = strListObj[i].split(':');
        if (strListItemObj.length > 1) {
            if (strListItemObj[1] == Key.toString())
                return strListItemObj[0];
        }
    }
}

function GetShiftTypeTitle_Shift(shiftType) {
    var ShiftTypes = document.getElementById('hfShiftTypes_Shift').value.split('#');
    for (var i = 0; i < ShiftTypes.length; i++) {
        var shiftTypeObj = ShiftTypes[i].split(':');
        if (shiftTypeObj.length > 1) {
            if (shiftTypeObj[1] == shiftType.toString())
                return shiftTypeObj[0];
        }
    }
}

function GridCartable_Cartable_onItemCheckChange(sender, e) {
    ChangeRequestsList_Cartable(e.get_item());
}

function ChangeRequestsList_Cartable(RequestItem) {
    var requestID = RequestItem.getMember('RequestID').get_text();
    var managerFlowID = RequestItem.getMember('ManagerFlowID').get_text();
    if (StrSelectedRequests_Cartable != null && StrSelectedRequests_Cartable.indexOf('#RID=' + requestID + '%MFID=' + managerFlowID + '#') >= 0)
        StrSelectedRequests_Cartable = StrSelectedRequests_Cartable.replace('#RID=' + requestID + '%MFID=' + managerFlowID + '#', '#');
    else
        StrSelectedRequests_Cartable += 'RID=' + requestID + '%MFID=' + managerFlowID + '#';
    if (StrSelectedRequests_Cartable.charAt(0) != '#')
        StrSelectedRequests_Cartable = '#' + StrSelectedRequests_Cartable;
}

function chbSelectAllinthisPage_Cartable_onClick() {
    StrSelectedRequests_Cartable = '';
    var checked = false;
    if (document.getElementById('chbSelectAllinthisPage_Cartable').checked)
        checked = true;
    GridCartable_Cartable.beginUpdate();
    for (var i = 0; i < GridCartable_Cartable.get_table().getRowCount(); i++) {
        var gridItem = GridCartable_Cartable.get_table().getRow(i);
        gridItem.setValue(6, checked, false);
        if (checked) {
            var requestID = gridItem.getMember('RequestID').get_value();
            var managerFlowID = gridItem.getMember('ManagerFlowID').get_text();
            StrSelectedRequests_Cartable += 'RID=' + requestID + '%MFID=' + managerFlowID + '#';
        }
    }
    StrSelectedRequests_Cartable = StrSelectedRequests_Cartable != '' ? '#' + StrSelectedRequests_Cartable : '';
    GridCartable_Cartable.endUpdate();
}

function DialogRequestDescription_onShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('tbl_DialogRequestDescription_Cartable').value = '';
        document.getElementById('tbl_DialogRequestDescription_Cartable').style.direction = 'rtl';
    }
}

function ShowRequestDescription_GridCartable_Cartable() {
    if (GridCartable_Cartable.getSelectedItems().length > 0) {
        document.getElementById('txtDescription_RequestDescription_Cartable').value = GridCartable_Cartable.getSelectedItems()[0].getMember('Description').get_text();
        DialogRequestDescription.Show();
    }
}

function tlbItemExit_tlbExit_RequestDescription_Cartable_onClick() {
    DialogRequestDescription.Close();
}

function tlbItemExit_tlbExit_RequestReject_Cartable_onClick() {
    DialogRequestRejectDescription.Close();
}

function DialogConfirm_OnShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('tblConfirm_DialogConfirm').style.direction = 'rtl';
}

function CallBack_GridCartable_Cartable_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridCartable_Cartable').innerHTML = '';
    ShowConnectionError_Cartable();
}

function ShowConnectionError_Cartable() {
    var error = document.getElementById('hfErrorType_Cartable').value;
    var errorBody = document.getElementById('hfConnectionError_Cartable').value;
    showDialog(error, errorBody, 'error');
}

function CollapseControls_Cartable() {
    cmbYear_Cartable.collapse();
    cmbMonth_Cartable.collapse();
}

function tlbItemFormReconstruction_TlbCartable_onClick() {
    var ObjDialogCartable = parent.DialogCartable.get_value();
    var RequestCaller = ObjDialogCartable.RequestCaller;
    DialogCartable_onClose();
    parent.DialogCartable.set_value(ObjDialogCartable);
    parent.DialogCartable.Show();
}

function tlbItemHelp_TlbCartable_onClick() {
    var ObjDialogCartable = parent.DialogCartable.get_value();
    var RequestCaller = ObjDialogCartable.RequestCaller;
    var helpID = null;
    switch (RequestCaller) {
        case 'Cartable':
            helpID = 'tlbItemHelp_TlbCartable';
            break;
        case 'Survey':
            helpID = 'tlbItemHelp_TlbSurvey';
            break;
        case 'Sentry':
            helpID = 'tlbItemHelp_TlbSentry';
            break;
    }
    LoadHelpPage(helpID);
}

function cmbSortBy_Cartable_onChange(sender, e) {
    document.getElementById('hfCurrentSortBy_Cartable').value = cmbSortBy_Cartable.getSelectedItem().get_value();
}

function ChangeComboDirection_MasterMonthlyOperation(cmbID) {
    if (parent.CurrentLangID == 'en-US')
        document.getElementById(cmbID + '_DropDownContent').dir = 'ltr';
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById(cmbID + '_DropDownContent').dir = 'rtl';
}

function btn_gdpDate_Cartable_OnMouseUp(event) {
    if (gCalDate_Cartable.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function btn_gdpDate_Cartable_OnClick(event) {
    if (gCalDate_Cartable.get_popUpShowing()) {
        gCalDate_Cartable.hide();
    }
    else {
        gCalDate_Cartable.setSelectedDate(gdpDate_Cartable.getSelectedDate());
        gCalDate_Cartable.show();
    }
}

function gdpDate_Cartable_OnDateChange(sender, e) {
    var RequestDate = gdpDate_Cartable.getSelectedDate();
    gCalDate_Cartable.setSelectedDate(RequestDate);
}

function gCalDate_Cartable_OnChange(sender, e) {
    var RequestDate = gCalDate_Cartable.getSelectedDate();
    gdpDate_Cartable.setSelectedDate(RequestDate);
}

function gCalDate_Cartable_OnLoad(sender, e) {
    window.gCalDate_Cartable.PopUpObject.z = 25000000;
}

function ViewCurrentLangCalendars_Calendar() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpDate_Cartable").parentNode.removeChild(document.getElementById("pdpDate_Cartable"));
            break;
        case 'fa-IR':
            document.getElementById("Container_DateCalendars_RequestRegister").removeChild(document.getElementById("Container_gCalDate_Cartable"));
            break;
    }
}

function SetCurrentDate_Cartable() {
    var ObjDialogCartable_Cartable = parent.DialogCartable.get_value();
    var RequestCaller = ObjDialogCartable_Cartable.RequestCaller;
    if (RequestCaller == 'Sentry') {
        switch (parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpDate_Cartable').value = document.getElementById('hfCurrentDate_Cartable').value;
                break;
            case 'en-US':
                currentDate_Cartable = new Date(document.getElementById('hfCurrentDate_Cartable').value);
                gdpDate_Cartable.setSelectedDate(currentDate_Cartable);
                gCalDate_Cartable.setSelectedDate(currentDate_Cartable);
                break;
        }
    }
}

function tlbItemClosePicture_TlbApplicantPicture_onClick() {
    document.getElementById('ApplicantImage_DialogPersonnelMainInformation').src = 'WhitePage.aspx';
    DialogApplicantImage.Close();
}

function ShowApplicantImage_GridCartable_Cartable() {
    if (GridCartable_Cartable.getSelectedItems().length > 0) {
        var dialogApplicantImageDirection = null;
        switch (parent.CurrentLangID) {
            case 'fa-IR':
                dialogApplicantImageDirection = 'rtl';
                break;
            case 'en-US':
                dialogApplicantImageDirection = 'ltr';
                break;
        }
        document.getElementById('Mastertbl_DialogApplicantImage').dir = dialogApplicantImageDirection;
        var PersonnelID = GridCartable_Cartable.getSelectedItems()[0].getMember('PersonId').get_text();
        document.getElementById('tdCurrentApplicant_DialogApplicantImage').innerHTML = GridCartable_Cartable.getSelectedItems()[0].getMember('Applicant').get_text();
        document.getElementById('ApplicantImage_DialogPersonnelMainInformation').src = 'ImageViewer.aspx?reload=""' + (new Date()).getTime() + '"&PersonnelID="' + PersonnelID + '"';
        DialogApplicantImage.Show();
    }
}

























