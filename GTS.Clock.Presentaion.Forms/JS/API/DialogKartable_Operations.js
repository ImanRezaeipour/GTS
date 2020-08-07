
var LastPageIndex_GridKartable_Kartable = 0;
var CurrentPageIndex_GridKartable_Kartable = 0;
var LoadState_Kartable = null;
var ConfirmState_Kartable = null;
var CurrentPageState_Kartable = 'View';
var StrSelectedRequests_Kartable = '';
var ObjKartable_Kartable = null;
var StrFilterConditions_Kartable = '';

function GetDefaultLoadState_Kartable() {
    var ObjDialogKartable_Kartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable_Kartable.RequestCaller;
    switch (RequestCaller) {
        case 'Kartable':
            LoadState_Kartable = 'None';
            break;
        case 'Survey':
            LoadState_Kartable = 'UnKnown';
            break;
        case 'Sentry':
            LoadState_Kartable = 'None';
            break;
    }
    return LoadState_Kartable;
}

function GetBoxesHeaders_Kartable() {
    var TitleDialog_Kartable = '';
    var HeaderGrid_Kartable = '';
    var RequestCaller = parent.DialogKartable.get_value().RequestCaller;
    switch (RequestCaller) {
        case 'Kartable':
            TitleDialog_Kartable = document.getElementById('hfTitle_DialogKartable').value;
            HeaderGrid_Kartable = document.getElementById('hfheader_Kartable_Kartable').value;
            break;
        case 'Survey':
            TitleDialog_Kartable = document.getElementById('hfTitle_DialogSurveyedRequests').value;
            HeaderGrid_Kartable = document.getElementById('hfheader_SurveyedRequests_Kartable').value;
            break;
        case 'Sentry':
            TitleDialog_Kartable = document.getElementById('hfTitle_DialogSentry').value;
            HeaderGrid_Kartable = document.getElementById('hfheader_SentryKartable_Kartable').value;
            break;
    }
    parent.document.getElementById('Title_DialogKartable').innerHTML = TitleDialog_Kartable;
    document.getElementById('header_Kartable_Kartable').innerHTML = HeaderGrid_Kartable;
    document.getElementById('beginfooter_GridKartable_Kartable').innerHTML = document.getElementById('endfooter_GridKartable_Kartable').innerHTML = document.getElementById('hffooter_GridKartable_Kartable').value;
}

function ChangeDirection_cmbControls_Kartable() {
    var RequestCaller = parent.DialogKartable.get_value().RequestCaller;
    if (RequestCaller == 'Kartable' || RequestCaller == 'Survey') {
        ChangeComboDirection_MasterMonthlyOperation('cmbYear_Kartable');
        ChangeComboDirection_MasterMonthlyOperation('cmbMonth_Kartable');
    }
    if (RequestCaller == 'Sentry')
        ChangeComboDirection_MasterMonthlyOperation('cmbSortBy_Kartable');
}

function ChangeDateControlContainersWidth_Kartable() {
    var RequestCaller = parent.DialogKartable.get_value().RequestCaller;
    if (RequestCaller == 'Kartable' || RequestCaller == 'Survey') {
        document.getElementById('Container_DateCalendars_RequestRegister').style.width = '0px';
    }
    if (RequestCaller == 'Sentry') {
    }
}

function ShowDialogKartableFilter_Kartable() {
    DialogKartableFilter.Show();
    CollapseControls_Kartable();
}

function ShowDialogHistory_Kartable() {
    if (GridKartable_Kartable.getSelectedItems().length > 0) {
        var ObjHistory = new Object();
        ObjHistory.RequestID = GridKartable_Kartable.getSelectedItems()[0].getMember('RequestID').get_text();
        ObjHistory.RequestIssuer = GridKartable_Kartable.getSelectedItems()[0].getMember('Applicant').get_text();
        ObjHistory.RequestTitle = GridKartable_Kartable.getSelectedItems()[0].getMember('RequestTitle').get_text();
        DialogHistory.set_value(ObjHistory);
        DialogHistory.Show();
        CollapseControls_Kartable();
    }
}

function ShowDialogEndorsementFlowState_Kartable() {
    if (GridKartable_Kartable.getSelectedItems().length > 0) {
        var ObjEndorsementFlowState = new Object();
        ObjEndorsementFlowState.ManagerFlowID = GridKartable_Kartable.getSelectedItems()[0].getMember('ManagerFlowID').get_text();
        ObjEndorsementFlowState.RequestID = GridKartable_Kartable.getSelectedItems()[0].getMember('RequestID').get_text();
        parent.DialogEndorsementFlowState.set_value(ObjEndorsementFlowState);
        parent.DialogEndorsementFlowState.Show();
        CollapseControls_Kartable();
    }
}

function ShowDialogRequestRejectDescription_Kartable(state) {
    var description = null;
    switch (state) {
        case 'Reject':
            description = document.getElementById('hfRequestRejectDescription_Kartable').value;
            break;
        case 'Delete':
            description = document.getElementById('hfRequestDeleteDescription_Kartable').value;
            break;
    }
    document.getElementById('hfDescription_RequestReject_Kartable').innerHTML = description;
    DialogRequestRejectDescription.Show();
    CollapseControls_Kartable();
}

function SetHorizontalScrollingDirection_GridKartable_Kartable_Opera() {
    if (navigator.userAgent.indexOf('Opera') != -1 && parent.CurrentLangID == "fa-IR")
        document.getElementById('GridKartable_Kartable').style.direction = "ltr";
}

function ChangeDirection_Mastertbl_KartableForm() {
    if (parent.CurrentLangID == 'en-US')
        document.getElementById('Mastertbl_KartableForm').dir = 'ltr';
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('Mastertbl_KartableForm').dir = 'rtl';
    }
}

function DialogRequestRejectDescription_onShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('tbl_RequestRejectDescription_Kartable').dir = 'rtl';
    }
}

function ShowDialogRequestsState() {
    DialogRequestsState.Show();
    CollapseControls_Kartable();
}

function GridKartable_Kartable_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridKartable_Kartable').innerHTML = '';
    BaseCallBackPrefix_GridKartable_Kartable = GridKartable_Kartable.CallbackPrefix;
}

function CallBack_GridKartable_Kartable_onCallbackComplete(sender, e) {
    SetHorizontalScrollingDirection_GridKartable_Kartable_Opera();
    GridKartable_Kartable.render();
    parent.DialogLoading.Close();
    var error = document.getElementById('ErrorHiddenField_Kartable').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridKartable_Kartable(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2], false, document.getElementById('Mastertbl_KartableForm').offsetWidth);
    }
    else
        Changefooter_GridKartable_Kartable();
}

function Changefooter_GridKartable_Kartable() {
    var retfooterVal = '';
    var footerVal = document.getElementById('beginfooter_GridKartable_Kartable').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfKartablePageCount_Kartable').value) > 0 ? CurrentPageIndex_GridKartable_Kartable + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfKartablePageCount_Kartable').value;
        if ((i == 1 || i == 3) && GridKartable_Kartable.get_table().getRowCount() == 0)
            footerValCol[i] = 0;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('beginfooter_GridKartable_Kartable').innerHTML = document.getElementById('endfooter_GridKartable_Kartable').innerHTML = retfooterVal;
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Kartable) {
        case 'PageChange':
            StrSelectedRequests_Kartable = '';
            Fill_GridKartable_Kartable(CurrentPageIndex_GridKartable_Kartable);
            break;
        case 'Reject':
            DialogConfirm.Close();
            ShowDialogRequestRejectDescription_Kartable('Reject');
            break;
        case 'Exit':
            DialogKartable_onClose();
            break;
        default:
    }
    DialogConfirm.Close();
}

function DialogKartable_onClose() {
    parent.document.getElementById('DialogKartable_IFrame').src = 'WhitePage.aspx';
    parent.DialogKartable.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function tlbItemEndorsement_TlbKartable_onClick() {
    UpdateKartable_Kartable("Confirmed");
}

function tlbItemReject_TlbKartable_onClick() {
    ShowDialogRequestRejectDescription_Kartable('Reject');
}

function tlbItemDelete_TlbKartable_onClick() {
    ShowDialogRequestRejectDescription_Kartable('Delete');
}

function UpdateKartable_Kartable(CurrentPageState) {
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    ObjKartable_Kartable = new Object();
    ObjKartable_Kartable.ManagerFlowID = '0';
    if (GridKartable_Kartable.getSelectedItems().length > 0)
        ObjKartable_Kartable.ManagerFlowID = GridKartable_Kartable.getSelectedItems()[0].getMember('ManagerFlowID').get_text();
    switch (CurrentPageState) {
        case 'Confirmed':
            ObjKartable_Kartable.ActionDescription = "";
            break;
        case 'Unconfirmed':
            ObjKartable_Kartable.ActionDescription = document.getElementById('txtDescription_RequestReject_Kartable').value;
            break;
        case 'Deleted':
            ObjKartable_Kartable.ActionDescription = document.getElementById('txtDescription_RequestReject_Kartable').value;
            var SelectedItems_GridKartable_Kartable = GridKartable_Kartable.getSelectedItems();
            if (SelectedItems_GridKartable_Kartable.length > 0) {
                if (StrSelectedRequests_Kartable != '')
                    StrSelectedRequests_Kartable = '';
                ChangeRequestsList_Kartable(SelectedItems_GridKartable_Kartable[0]);
            }
    }
    ObjKartable_Kartable.StrSelectedRequests = StrSelectedRequests_Kartable;
    UpdateKartable_KartablePage(CharToKeyCode_Kartable(RequestCaller), CharToKeyCode_Kartable(CurrentPageState), CharToKeyCode_Kartable(ObjKartable_Kartable.StrSelectedRequests), CharToKeyCode_Kartable(ObjKartable_Kartable.ActionDescription));
    DialogWaiting.Show();
}

function UpdateKartable_KartablePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();        
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Kartable').value;
            Response[1] = document.getElementById('hfConnectionError_Kartable').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2], false, document.getElementById('Mastertbl_KartableForm').offsetWidth);
        if (RetMessage[2] == 'success') {
            //Kartable_OnAfterUpdate(Response);
            //UpdateFeatures_GridKartable_Kartable();
            Kartable_OnAfterUpdate();
        }
    }
}

function Kartable_OnAfterUpdate() {         
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    ClearList_Kartable(RequestCaller);
    CurrentPageIndex_GridKartable_Kartable = 0;
    Fill_GridKartable_Kartable(0);
}

//function Kartable_OnAfterUpdate(Response) {
//    var ObjDialogKartable = parent.DialogKartable.get_value();
//    var RequestCaller = ObjDialogKartable.RequestCaller;
//    switch (RequestCaller) {
//        case 'Kartable':
//            var strSelectedRequests = ObjKartable_Kartable.StrSelectedRequests;
//            GridKartable_Kartable.beginUpdate();
//            for (var i = 0; i < GridKartable_Kartable.get_table().getRowCount(); i++) {
//                var gridItem = GridKartable_Kartable.get_table().getRow(i);
//                requestID = gridItem.getMember('RequestID').get_text();
//                if (strSelectedRequests.indexOf('#' + requestID + '#') >= 0)
//                    GridKartable_Kartable.deleteItem(gridItem);
//            }
//            GridKartable_Kartable.endUpdate();
//            break;
//        case 'Survey':
//            break;
//        case 'Sentry':
//            break;
//    }
//    ClearList_Kartable(RequestCaller);
//}

function ClearList_Kartable(RequestCaller) {
    StrSelectedRequests_Kartable = '';
    document.getElementById('txtDescription_RequestReject_Kartable').value = '';
    if (RequestCaller == 'Kartable')
        document.getElementById('chbSelectAllinthisPage_Kartable').checked = false;
}

function UpdateFeatures_GridKartable_Kartable() {
    var KartableCount = parseInt(document.getElementById('hfKartableCount_Kartable').value);
    var KartablePageCount = parseInt(document.getElementById('hfKartablePageCount_Kartable').value);
    var KartablePageSize = parseInt(document.getElementById('hfKartablePageSize_Kartable').value);
    if (KartableCount > 0) {
        KartableCount = KartableCount - 1;
        var divRem = mod(KartableCount, KartablePageSize);
        if (divRem == 0) {
            KartablePageCount = KartablePageCount - 1;
            if (CurrentPageIndex_GridKartable_Kartable == KartablePageCount)
                CurrentPageIndex_GridKartable_Kartable = CurrentPageIndex_GridKartable_Kartable - 1;
        }
        SetPageIndex_GridKartable_Kartable(CurrentPageIndex_GridKartable_Kartable);
        document.getElementById('hfKartableCount_Kartable').value = KartableCount.toString();
        document.getElementById('hfKartablePageCount_Kartable').value = KartablePageCount.toString();
        Changefooter_GridKartable_Kartable();
    }
}

function mod(a, b) {
    return a - (b * Math.floor(a / b));
}

function tlbItemHistory_TlbKartable_onClick() {
    ShowDialogHistory_Kartable();
}

function tlbItemFilter_TlbKartable_onClick() {
    ShowDialogKartableFilter_Kartable();
}

function Kartable_OnAfterCustomFilter(StrCustomFilter) {
    StrFilterConditions_Kartable = StrCustomFilter;
    ChangeLoadState_GridKartable_Kartable('CustomFilter');
}

function tlbItemSearch_TlbKartableQuickSearch_onClick() {
    StrFilterConditions_Kartable = document.getElementById('txtSerchTerm_Kartable').value;
    ChangeLoadState_GridKartable_Kartable('Search');
}


function tlbItemExit_TlbKartable_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Kartable = confirmState;
    switch (confirmState) {
        case 'Reject':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfRejectMessage_Kartable').value;
            break;
        case 'PageChange':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfPageChange_Kartable').value;
            break;
        case 'Exit':
            var ObjDialogKartable_Kartable = parent.DialogKartable.get_value();
            var RequestCaller = ObjDialogKartable_Kartable.RequestCaller;
            var CloseMessage_DialogKartable = null;
            switch (RequestCaller) {
                case 'Kartable':
                    CloseMessage_DialogKartable = document.getElementById('hfKartableCloseMessage_Kartable').value;
                    break;
                case 'Survey':
                    CloseMessage_DialogKartable = document.getElementById('hfSurveyCloseMessage_Kartable').value;
                    break;
                case 'Sentry':
                    CloseMessage_DialogKartable = document.getElementById('hfSentryCloseMessage_Kartable').value;
                    break;
            }
            document.getElementById('lblConfirm').innerHTML = CloseMessage_DialogKartable;
            break;
    }
    DialogConfirm.Show();
    CollapseControls_Kartable();
}

function tlbItemAllRequests_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable(GetDefaultLoadState_Kartable());
}

function tlbItemDailyRequests_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('Daily');
}

function tlbItemHourlyRequests_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('Hourly');
}

function tlbItemOverTimeJustification_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('OverWork');
}

function tlbItemImperative_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('Imperative');
}

function tlbItemConfirmedRequests_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('Confirmed');
}

function tlbItemRejectedRequests_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('Unconfirmed');
}

function tlbItemDeletedRequests_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('Deleted');
}

function tlbItemRefresh_TlbPaging_GridKartable_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable(GetDefaultLoadState_Kartable());
}

function ChangeLoadState_GridKartable_Kartable(state) {
    LoadState_Kartable = state;
    SetPageIndex_GridKartable_Kartable(0);
}

function tlbItemFirst_TlbPaging_GridKartable_Kartable_onClick() {
    SetPageIndex_GridKartable_Kartable(0);
}

function tlbItemBefore_TlbPaging_GridKartable_Kartable_onClick() {
    if (CurrentPageIndex_GridKartable_Kartable != 0) {
        CurrentPageIndex_GridKartable_Kartable = CurrentPageIndex_GridKartable_Kartable - 1;
        SetPageIndex_GridKartable_Kartable(CurrentPageIndex_GridKartable_Kartable);
    }
}

function tlbItemNext_TlbPaging_GridKartable_Kartable_onClick() {
    if (CurrentPageIndex_GridKartable_Kartable < parseInt(document.getElementById('hfKartablePageCount_Kartable').value) - 1) {
        CurrentPageIndex_GridKartable_Kartable = CurrentPageIndex_GridKartable_Kartable + 1;
        SetPageIndex_GridKartable_Kartable(CurrentPageIndex_GridKartable_Kartable);
    }
}

function tlbItemLast_TlbPaging_GridKartable_Kartable_onClick() {
    SetPageIndex_GridKartable_Kartable(parseInt(document.getElementById('hfKartablePageCount_Kartable').value) - 1);
}

function SetPageIndex_GridKartable_Kartable(pageIndex) {
    CurrentPageIndex_GridKartable_Kartable = pageIndex;
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    switch (RequestCaller) {
        case 'Kartable':
            if (StrSelectedRequests_Kartable == '')
                Fill_GridKartable_Kartable(pageIndex);
            else
                ShowDialogConfirm('PageChange');
            break;
        case 'Survey':
            Fill_GridKartable_Kartable(pageIndex);
            break;
        case 'Sentry':
            Fill_GridKartable_Kartable(pageIndex);
            break;
    }
}

function ConfirmPageChange_GridKartable_Kartable() { 
}

function cmbYear_Kartable_onChange(sender, e) {
    document.getElementById('hfCurrentYear_Kartable').value = cmbYear_Kartable.getSelectedItem().get_value();
}

function cmbMonth_Kartable_onChange(sender, e) {
    document.getElementById('hfCurrentMonth_Kartable').value = cmbMonth_Kartable.getSelectedItem().get_value();
}

function Fill_GridKartable_Kartable(pageIndex) {
    document.getElementById('loadingPanel_GridKartable_Kartable').innerHTML = document.getElementById('hfloadingPanel_GridKartable_Kartable').value;
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    var pageSize = parseInt(document.getElementById('hfKartablePageSize_Kartable').value);
    var sortBy = document.getElementById('hfCurrentSortBy_Kartable').value;
    var year = '0';
    var month = '0';
    var date = null;
    if (RequestCaller == 'Kartable' || RequestCaller == 'Survey') {
        year = document.getElementById('hfCurrentYear_Kartable').value;
        month = document.getElementById('hfCurrentMonth_Kartable').value;
    }
    if (RequestCaller == 'Sentry') {
        switch (parent.SysLangID) {
            case 'fa-IR':
                date = document.getElementById('pdpDate_Kartable').value;
                break;
            case 'en-US':
                date = document.getElementById('gdpDate_Kartable').value;
                break;
        }
    }
    CallBack_GridKartable_Kartable.callback(CharToKeyCode_Kartable(RequestCaller), CharToKeyCode_Kartable(LoadState_Kartable), CharToKeyCode_Kartable(year), CharToKeyCode_Kartable(month), CharToKeyCode_Kartable(date), CharToKeyCode_Kartable(StrFilterConditions_Kartable), CharToKeyCode_Kartable(sortBy), CharToKeyCode_Kartable(pageSize.toString()), CharToKeyCode_Kartable(pageIndex.toString()));
    parent.DialogLoading.Show();
}

function CustomizeTlbKartable_Kartable() {
    var RequestCaller = parent.DialogKartable.get_value().RequestCaller;
    CallBackTlbKartable_Kartable.callback(CharToKeyCode_Kartable(RequestCaller));
}

function CustomizeTlbKartableFilter_Kartable() {
    var RequestCaller = parent.DialogKartable.get_value().RequestCaller;
    CallBackTlbKartableFilter_Kartable.callback(CharToKeyCode_Kartable(RequestCaller));
}

function CharToKeyCode_Kartable(str) {
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

function tlbItemView_TlbView_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable(GetDefaultLoadState_Kartable());
}

function tlbItemEndorsement_TlbRequestReject_Kartable_onClick() {
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    var RequestState = null;
    switch (RequestCaller) {
        case 'Kartable':
            RequestState = 'Unconfirmed';
            break;
        case 'Survey':
            RequestState = 'Deleted';
            break;
    }
    DialogRequestRejectDescription.Close();
    UpdateKartable_Kartable(RequestState);
}

function tlbItemCancel_TlbRequestReject_Kartable_onClick() {
    DialogRequestRejectDescription.Close();
    document.getElementById('txtDescription_RequestReject_Kartable').value = '';
}

function SetClmnImage_GridKartable_Kartable(dataField, Key) {
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
                    cellImage = 'Images/Grid/monthly.png';
                    break;
                case '4':
                    cellImage = 'Images/Grid/Permission.png';
                    break;
                case '5':
                    cellImage = 'Images/Grid/imperative.png';
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

function GetRequestFlowLevel_GridKartable_Kartable() {
    ShowDialogEndorsementFlowState_Kartable();
}

function SetCellTitle_GridKartable_Kartable(dataField, Key) {
    var elementID = null;
    switch (dataField) {
        case 'FlowStatus':
            elementID = 'hfRequestStates_Kartable';
            break;
        case 'RequestType':
            elementID = 'hfRequestTypes_Kartable';
            break;
        case 'RequestSource':
            elementID = 'hfRequestSources_Kartable';
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

function GridKartable_Kartable_onItemCheckChange(sender, e) {
    ChangeRequestsList_Kartable(e.get_item());
}

function ChangeRequestsList_Kartable(RequestItem) {
    var requestID = RequestItem.getMember('RequestID').get_text();
    var managerFlowID = RequestItem.getMember('ManagerFlowID').get_text();
    if (StrSelectedRequests_Kartable != null && StrSelectedRequests_Kartable.indexOf('#RID=' + requestID + '%MFID=' + managerFlowID + '#') >= 0)
        StrSelectedRequests_Kartable = StrSelectedRequests_Kartable.replace('#RID=' + requestID + '%MFID=' + managerFlowID + '#', '#');
    else
        StrSelectedRequests_Kartable += 'RID=' + requestID + '%MFID=' + managerFlowID + '#';
    if (StrSelectedRequests_Kartable.charAt(0) != '#')
        StrSelectedRequests_Kartable = '#' + StrSelectedRequests_Kartable;
}

function chbSelectAllinthisPage_Kartable_onClick() {
    StrSelectedRequests_Kartable = '';
    var checked = false;
    if (document.getElementById('chbSelectAllinthisPage_Kartable').checked)
        checked = true;
    GridKartable_Kartable.beginUpdate();
    for (var i = 0; i < GridKartable_Kartable.get_table().getRowCount(); i++) {
        var gridItem = GridKartable_Kartable.get_table().getRow(i);
        gridItem.setValue(6, checked, false);
        if (checked) {
            var requestID = gridItem.getMember('RequestID').get_value();
            var managerFlowID = gridItem.getMember('ManagerFlowID').get_text();
            StrSelectedRequests_Kartable += 'RID=' + requestID + '%MFID=' + managerFlowID + '#';
        }
    }
    StrSelectedRequests_Kartable = StrSelectedRequests_Kartable != '' ? '#' + StrSelectedRequests_Kartable : '';
    GridKartable_Kartable.endUpdate();
}

function DialogRequestDescription_onShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('tbl_DialogRequestDescription_Kartable').value = '';
        document.getElementById('tbl_DialogRequestDescription_Kartable').style.direction = 'rtl';
    }
}

function ShowRequestDescription_GridKartable_Kartable() {
    if (GridKartable_Kartable.getSelectedItems().length > 0) {
        document.getElementById('txtDescription_RequestDescription_Kartable').value = GridKartable_Kartable.getSelectedItems()[0].getMember('Description').get_text();
        DialogRequestDescription.Show();
    }
}

function tlbItemExit_tlbExit_RequestDescription_Kartable_onClick() {
    DialogRequestDescription.Close();
}

function tlbItemExit_tlbExit_RequestReject_Kartable_onClick() {
    DialogRequestRejectDescription.Close();
}

function DialogConfirm_OnShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('tblConfirm_DialogConfirm').style.direction = 'rtl';
}

function CallBack_GridKartable_Kartable_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridKartable_Kartable').innerHTML = '';
    ShowConnectionError_Kartable();
}

function ShowConnectionError_Kartable() {
    var error = document.getElementById('hfErrorType_Kartable').value;
    var errorBody = document.getElementById('hfConnectionError_Kartable').value;
    showDialog(error, errorBody, 'error');
}

function CollapseControls_Kartable() {
    cmbYear_Kartable.collapse();
    cmbMonth_Kartable.collapse();
}

function tlbItemFormReconstruction_TlbKartable_onClick() {
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    DialogKartable_onClose();
    parent.DialogKartable.set_value(ObjDialogKartable);
    parent.DialogKartable.Show();
}

function tlbItemHelp_TlbKartable_onClick() {
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    var helpID = null;
    switch (RequestCaller) {
        case 'Kartable':
            helpID = 'tlbItemHelp_TlbKartable';
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

function cmbSortBy_Kartable_onChange(sender, e) {
    document.getElementById('hfCurrentSortBy_Kartable').value = cmbSortBy_Kartable.getSelectedItem().get_value();
}

function ChangeComboDirection_MasterMonthlyOperation(cmbID) {
    if (parent.CurrentLangID == 'en-US')
        document.getElementById(cmbID + '_DropDownContent').dir = 'ltr';
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById(cmbID + '_DropDownContent').dir = 'rtl';
}

function btn_gdpDate_Kartable_OnMouseUp(event) {
    if (gCalDate_Kartable.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function btn_gdpDate_Kartable_OnClick(event) {
    if (gCalDate_Kartable.get_popUpShowing()) {
        gCalDate_Kartable.hide();
    }
    else {
        gCalDate_Kartable.setSelectedDate(gdpDate_Kartable.getSelectedDate());
        gCalDate_Kartable.show();
    }
}

function gdpDate_Kartable_OnDateChange(sender, e) {
    var RequestDate = gdpDate_Kartable.getSelectedDate();
    gCalDate_Kartable.setSelectedDate(RequestDate);
}

function gCalDate_Kartable_OnChange(sender, e) {
    var RequestDate = gCalDate_Kartable.getSelectedDate();
    gdpDate_Kartable.setSelectedDate(RequestDate);
}

function gCalDate_Kartable_OnLoad(sender, e) {
    window.gCalDate_Kartable.PopUpObject.z = 25000000;
}

function ViewCurrentLangCalendars_Calendar() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpDate_Kartable").parentNode.removeChild(document.getElementById("pdpDate_Kartable"));
            break;
        case 'fa-IR':
            document.getElementById("Container_DateCalendars_RequestRegister").removeChild(document.getElementById("Container_gCalDate_Kartable"));
            break;
    }
}

function SetCurrentDate_Kartable() {
    var ObjDialogKartable_Kartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable_Kartable.RequestCaller;
    if (RequestCaller == 'Sentry') {
        switch (parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpDate_Kartable').value = document.getElementById('hfCurrentDate_Kartable').value;
                break;
            case 'en-US':
                currentDate_Kartable = new Date(document.getElementById('hfCurrentDate_Kartable').value);
                gdpDate_Kartable.setSelectedDate(currentDate_Kartable);
                gCalDate_Kartable.setSelectedDate(currentDate_Kartable);
                break;
        }
    }
}

function tlbItemClosePicture_TlbApplicantPicture_onClick() {
    document.getElementById('ApplicantImage_DialogApplicantImage').src = 'WhitePage.aspx';
    DialogApplicantImage.Close();
}

function ShowApplicantImage_GridKartable_Kartable() {
    if (GridKartable_Kartable.getSelectedItems().length > 0) {
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
        var PersonnelImage = GridKartable_Kartable.getSelectedItems()[0].getMember('PersonImage').get_text();
        document.getElementById('tdCurrentApplicant_DialogApplicantImage').innerHTML = GridKartable_Kartable.getSelectedItems()[0].getMember('Applicant').get_text();
        document.getElementById('ApplicantImage_DialogApplicantImage').src = "ImageViewer.aspx?reload=" + (new Date()).getTime() + "&AttachmentType=Personnel&ClientAttachment=" + CharToKeyCode_Kartable(PersonnelImage);
        DialogApplicantImage.Show();
    }
}

function ShowAttachmentFile_GridKartable_Kartable() {
    var SelectedItems_GridKartable_Kartable = GridKartable_Kartable.getSelectedItems();
    if (SelectedItems_GridKartable_Kartable.length > 0 && SelectedItems_GridKartable_Kartable[0].getMember('AttachmentFile').get_text() != '') {
        var AttachmentFile = SelectedItems_GridKartable_Kartable[0].getMember('AttachmentFile').get_text();
        window.open("ClientAttachmentViewer.aspx?AttachmentType=Request&ClientAttachment=" + CharToKeyCode_Kartable(AttachmentFile) + "", "ClientAttachmentViewer" + (new Date()).getTime() + "", "width=" + screen.width + ",height=" + screen.height + ",toolbar=yes,location=yes,directories=yes,status=yes,menubar=yes,scrollbars=yes,copyhistory=yes,resizable=yes");
    }
}

function SetAttachmentFileImage_GridKartable_Kartable(attachmentFile) {
    var innerHTML = '';
    if (attachmentFile != undefined && attachmentFile != null && attachmentFile != '')
        innerHTML = '<img src="Images/Grid/attachment.png" alt="" />';
    return innerHTML;
}


























