
var CurrentPageState_RegisteredRequests = 'View';
var ConfirmState_RegisteredRequests = null;
var ObjRegisteredRequest_RegisteredRequests = new Object();
var CurrentPageIndex_GridRegisteredRequests_RegisteredRequests = 0;
var LoadState_RegisteredRequests = '';
var CurrentPageCombosCallBcakStateObj = new Object();
var LoadState_cmbPersonnel_RegisteredRequests = 'Normal';
var CurrentPageIndex_cmbPersonnel_RegisteredRequests = 0;
var SearchTerm_cmbPersonnel_RegisteredRequests = '';
var AdvancedSearchTerm_cmbPersonnel_RegisteredRequests = '';

function GetBoxesHeaders_RegisteredRequests() {
    parent.document.getElementById('Title_DialogRegisteredRequests').innerHTML = '&nbsp;&nbsp;&nbsp;' + document.getElementById('hfTitle_DialogRegisteredRequests').value;
    document.getElementById('header_RegisteredRequests_RegisteredRequests').innerHTML = document.getElementById('hfheader_RegisteredRequests_RegisteredRequests').value;
    document.getElementById('header_Filter_RegisteredRequests').innerHTML = document.getElementById('hfheader_Filter_RegisteredRequests').value;
    document.getElementById('beginfooter_GridRegisteredRequests_RegisteredRequests').innerHTML = document.getElementById('endfooter_GridRegisteredRequests_RegisteredRequests').innerHTML = document.getElementById('hffooter_GridRegisteredRequests_RegisteredRequests').value;
    document.getElementById('clmnName_cmbPersonnel_RegisteredRequests').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_RegisteredRequests').value;
    document.getElementById('clmnBarCode_cmbPersonnel_RegisteredRequests').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_RegisteredRequests').value;
    document.getElementById('clmnCardNum_cmbPersonnel_RegisteredRequests').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_RegisteredRequests').value;
}

function CustomizeRegisteredRequestsFilter_RegisteredRequests() {
    var currentUserState = document.getElementById('CurrentUserState_RegisteredRequests').value;    
    switch (currentUserState) {
        case 'NormalUser':
            document.getElementById("Container_PersonnelSearchBox_RegisteredRequests").removeChild(document.getElementById("PersonnelSearchBox_RegisteredRequests"));    
            break;
        case 'Operator':
            break;
    }
}

function ResetCalendars_RegisteredRequests() {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_RegisteredRequests').value = '';
            document.getElementById('pdpToDate_RegisteredRequests').value = '';
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_RegisteredRequests_picker').value = '';
            document.getElementById('gdpToDate_RegisteredRequests_picker').value = '';
            break;
    }
}

function ChangeDirection_Mastertbl_RegisteredRequestsForm() {
    var direction = null;
    if (parent.CurrentLangID == 'en-US')
        direction = 'ltr';
    if (parent.CurrentLangID == 'fa-IR')
        direction = 'rtl';
    document.getElementById('Mastertbl_RegisteredRequestsForm').dir = document.getElementById('cmbYear_RegisteredRequests_DropDownContent').dir = document.getElementById('cmbMonth_RegisteredRequests_DropDownContent').dir = direction;
}

function SetHorizontalScrollingDirection_GridRegisteredRequests_RegisteredRequests_Opera() {
    if (navigator.userAgent.indexOf('Opera') != -1 && parent.CurrentLangID == "fa-IR")
        document.getElementById('GridRegisteredRequests_RegisteredRequests').style.direction = "ltr";
}

function ViewCurrentLangCalendars_RegisteredRequests() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpFromDate_RegisteredRequests").parentNode.removeChild(document.getElementById("pdpFromDate_RegisteredRequests"));
            document.getElementById("pdpFromDate_RegisteredRequestsimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_RegisteredRequestsimgbt"));
            document.getElementById("pdpToDate_RegisteredRequests").parentNode.removeChild(document.getElementById("pdpToDate_RegisteredRequests"));
            document.getElementById("pdpToDate_RegisteredRequestsimgbt").parentNode.removeChild(document.getElementById("pdpToDate_RegisteredRequestsimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_FromDateCalendars_RegisteredRequests").removeChild(document.getElementById("Container_gCalFromDate_RegisteredRequests"));
            document.getElementById("Container_ToDateCalendars_RegisteredRequests").removeChild(document.getElementById("Container_gCalToDate_RegisteredRequests"));
            break;
    }
}


///////////////////// gdpFromDate & gCalFromDate ////////////////////////
function gdpFromDate_RegisteredRequests_OnDateChange(sender, eventArgs) {
    var fromDate = gdpFromDate_RegisteredRequests.getSelectedDate();
    gCalFromDate_RegisteredRequests.setSelectedDate(fromDate);
}
function gCalFromDate_RegisteredRequests_OnChange(sender, eventArgs) {
    var fromDate = gCalFromDate_RegisteredRequests.getSelectedDate();
    gdpFromDate_RegisteredRequests.setSelectedDate(fromDate);
}
function btn_gdpFromDate_RegisteredRequests_OnClick(event) {
    if (gCalFromDate_RegisteredRequests.get_popUpShowing()) {
        gCalFromDate_RegisteredRequests.hide();
    }
    else {
        gCalFromDate_RegisteredRequests.setSelectedDate(gdpFromDate_RegisteredRequests.getSelectedDate());
        gCalFromDate_RegisteredRequests.show();
    }
}
function btn_gdpFromDate_RegisteredRequests_OnMouseUp(event) {
    if (gCalFromDate_RegisteredRequests.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalFromDate_RegisteredRequests_onLoad(sender, e) {
    window.gCalFromDate_RegisteredRequests.PopUpObject.z = 25000000;
}

///////////////////// gdpToDate & gCalToDate ////////////////////////
function gdpToDate_RegisteredRequests_OnDateChange(sender, eventArgs) {
    var toDate = gdpToDate_RegisteredRequests.getSelectedDate();
    gCalToDate_RegisteredRequests.setSelectedDate(toDate);
}
function gCalToDate_RegisteredRequests_OnChange(sender, eventArgs) {
    var toDate = gCalToDate_RegisteredRequests.getSelectedDate();
    gdpToDate_RegisteredRequests.setSelectedDate(toDate);
}
function btn_gdpToDate_RegisteredRequests_OnClick(event) {
    if (gCalToDate_RegisteredRequests.get_popUpShowing()) {
        gCalToDate_RegisteredRequests.hide();
    }
    else {
        gCalToDate_RegisteredRequests.setSelectedDate(gdpToDate_RegisteredRequests.getSelectedDate());
        gCalToDate_RegisteredRequests.show();
    }
}
function btn_gdpToDate_RegisteredRequests_OnMouseUp(event) {
    if (gCalToDate_RegisteredRequests.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalToDate_RegisteredRequests_onLoad(sender, e) {
    window.gCalToDate_RegisteredRequests.PopUpObject.z = 25000000;
}

function DialogRegisteredRequestsFilter_OnShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR') {
        var direction = 'rtl';
        document.getElementById('tbl_RegisteredRequestsFilter_RegisteredRequests').dir = document.getElementById('cmbRequestType_RegisteredRequests_DropDownContent').dir = document.getElementById('cmbExporter_RegisteredRequests_DropDownContent').dir = direction;
        if (document.getElementById('cmbPersonnel_RegisteredRequests_DropDownContent') != null)
            document.getElementById('cmbPersonnel_RegisteredRequests_DropDownContent').dir = direction;
    }
}

function ShowDialogRequestRegister(caller) {
    var ObjRequestRegister = new Object();
    ObjRequestRegister.Caller = caller;
    ObjRequestRegister.Year = document.getElementById('hfCurrentYear_RegisteredRequests').value;
    ObjRequestRegister.Month = document.getElementById('hfCurrentMonth_RegisteredRequests').value;
    ObjRequestRegister.PageSize = document.getElementById('hfRegisteredRequestsPageSize_RegisteredRequests').value;
    ObjRequestRegister.PageCount = document.getElementById('hfRegisteredRequestsPageCount_RegisteredRequests').value;
    DialogRequestRegister.set_value(ObjRequestRegister);
    DialogRequestRegister.Show();
    CollapseControls_RegisteredRequests();
}

function tlbItemInsert_TlbRegisteredRequests_onClick() {
    ShowDialogRequestRegister('NormalUser');
}

function tlbItemDelete_TlbRegisteredRequests_onClick() {
    CurrentPageState_RegisteredRequests = 'Delete';
    ShowDialogConfirm('Delete');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_RegisteredRequests = confirmState;
    if (CurrentPageState_RegisteredRequests == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_RegisteredRequests').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_RegisteredRequests').value;
    DialogConfirm.Show();
    CollapseControls_RegisteredRequests();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_RegisteredRequests) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateRegisteredRequest_RegisteredRequests();
            break;
        case 'Exit':
            RegisteredRequests_onClose();
            break;
    }
}

function RegisteredRequests_onClose() {
    parent.document.getElementById('DialogRegisteredRequests_IFrame').src = 'WhitePage.aspx';
    parent.DialogRegisteredRequests.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    CurrentPageState_RegisteredRequests = 'View';
}

function UpdateRegisteredRequest_RegisteredRequests() {
    ObjRegisteredRequest_RegisteredRequests = new Object();
    ObjRegisteredRequest_RegisteredRequests.ID = '0';
    ObjRegisteredRequest_RegisteredRequests.AttachmentFile = null;
    var SelectedRegisteredRequest_RegisteredRequests = GridRegisteredRequests_RegisteredRequests.getSelectedItems();
    if (SelectedRegisteredRequest_RegisteredRequests.length > 0) {
        ObjRegisteredRequest_RegisteredRequests.ID = SelectedRegisteredRequest_RegisteredRequests[0].getMember('ID').get_text();
        ObjRegisteredRequest_RegisteredRequests.AttachmentFile = SelectedRegisteredRequest_RegisteredRequests[0].getMember('AttachmentFile').get_text();
    }
    UpdateRegisteredRequest_RegisteredRequestsPage(CharToKeyCode_RegisteredRequests(CurrentPageState_RegisteredRequests), CharToKeyCode_RegisteredRequests(ObjRegisteredRequest_RegisteredRequests.ID), CharToKeyCode_RegisteredRequests(ObjRegisteredRequest_RegisteredRequests.AttachmentFile));
    DialogWaiting.Show();
}

function UpdateRegisteredRequest_RegisteredRequestsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_RegisteredRequests').value;
            Response[1] = document.getElementById('hfConnectionError_RegisteredRequests').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2], false, document.getElementById('Mastertbl_RegisteredRequestsForm').offsetWidth);
        if (RetMessage[2] == 'success') {
            //RegisteredRequest_OnAfterUpdate(Response);
            //UpdateFeatures_GridRegisteredRequests_RegisteredRequests();
            ChangeLoadState_GridRegisteredRequests_RegisteredRequests('UnKnown');
            Fill_GridRegisteredRequests_RegisteredRequests(0);
        }
    }
}

function RegisteredRequest_OnAfterUpdate(Response) {
    if (ObjRegisteredRequest_RegisteredRequests != null) {
        GridRegisteredRequests_RegisteredRequests.beginUpdate();
        switch (CurrentPageState_RegisteredRequests) {
            case 'Delete':
                GridRegisteredRequests_RegisteredRequests.selectByKey(ObjRegisteredRequest_RegisteredRequests.ID, 0, false);
                GridRegisteredRequests_RegisteredRequests.deleteSelected();
                UpdateFeatures_GridRegisteredRequests_RegisteredRequests();
                break;
        }
        GridRegisteredRequests_RegisteredRequests.endUpdate();
    }
}

function UpdateFeatures_GridRegisteredRequests_RegisteredRequests() {
    var RegisteredRequestsCount = parseInt(document.getElementById('hfRegisteredRequestsCount_RegisteredRequests').value);
    var RegisteredRequestsPageCount = parseInt(document.getElementById('hfRegisteredRequestsPageCount_RegisteredRequests').value);
    var RegisteredRequestsPageSize = parseInt(document.getElementById('hfRegisteredRequestsPageSize_RegisteredRequests').value);
    if (RegisteredRequestsCount > 0) {
        RegisteredRequestsCount = RegisteredRequestsCount - 1;
        var divRem = mod(RegisteredRequestsCount, RegisteredRequestsPageSize);
        if (divRem == 0) {
            RegisteredRequestsPageCount = RegisteredRequestsPageCount - 1;
            if (CurrentPageIndex_GridRegisteredRequests_RegisteredRequests == RegisteredRequestsPageCount)
                CurrentPageIndex_GridRegisteredRequests_RegisteredRequests = CurrentPageIndex_GridRegisteredRequests_RegisteredRequests - 1 >= 0 ? CurrentPageIndex_GridRegisteredRequests_RegisteredRequests - 1 : 0;
        }
        SetPageIndex_GridRegisteredRequests_RegisteredRequests(CurrentPageIndex_GridRegisteredRequests_RegisteredRequests);
        document.getElementById('hfRegisteredRequestsCount_RegisteredRequests').value = RegisteredRequestsCount.toString();
        document.getElementById('hfRegisteredRequestsPageCount_RegisteredRequests').value = RegisteredRequestsPageCount.toString();
        Changefooter_GridRegisteredRequests_RegisteredRequests();
    }
}

function mod(a, b) {
    return a - (b * Math.floor(a / b));
}

function CharToKeyCode_RegisteredRequests(str) {
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

function tlbItemFilter_TlbRegisteredRequests_onClick() {
    DialogRegisteredRequestsFilter.Show();
}

function tlbItemExit_TlbRegisteredRequests_onClick() {
    ShowDialogConfirm('Exit');
}

function tlbItemView_TlbView_RegisteredRequests_onClick() {
    ChangeLoadState_GridRegisteredRequests_RegisteredRequests('UnKnown');
}

function tlbItemConfirmedRequests_TlbRegisteredRequestsFilter_RegisteredRequests_onClick() {
    ChangeLoadState_GridRegisteredRequests_RegisteredRequests('Confirmed');
}

function tlbItemPendingRequests_TlbRegisteredRequestsFilter_RegisteredRequests_onClick() {
    ChangeLoadState_GridRegisteredRequests_RegisteredRequests('UnderReview');    
}

function tlbItemRejectedRequests_TlbRegisteredRequestsFilter_RegisteredRequests_onClick() {
    ChangeLoadState_GridRegisteredRequests_RegisteredRequests('Unconfirmed');
}

function tlbItemDeletedRequests_TlbRegisteredRequestsFilter_RegisteredRequests_onClick() {
    ChangeLoadState_GridRegisteredRequests_RegisteredRequests('Deleted');
}

function cmbYear_RegisteredRequests_onChange(sender, e) {
    document.getElementById('hfCurrentYear_RegisteredRequests').value = cmbYear_RegisteredRequests.getSelectedItem().get_value();
}

function cmbMonth_RegisteredRequests_onChange(sender, e) {
    document.getElementById('hfCurrentMonth_RegisteredRequests').value = cmbMonth_RegisteredRequests.getSelectedItem().get_value();
}

function GridRegisteredRequests_RegisteredRequests_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridRegisteredRequests_RegisteredRequests').innerHTML = '';
}

function CallBack_GridRegisteredRequests_RegisteredRequests_onCallbackComplete(sender, e) {
    SetHorizontalScrollingDirection_GridRegisteredRequests_RegisteredRequests_Opera();
    GridRegisteredRequests_RegisteredRequests.render();
    var error = document.getElementById('ErrorHiddenField_RegisteredRequests').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridRegisteredRequests_RegisteredRequests(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2], false, document.getElementById('Mastertbl_RegisteredRequestsForm').offsetWidth);
    }
    else
        Changefooter_GridRegisteredRequests_RegisteredRequests();
}

function Changefooter_GridRegisteredRequests_RegisteredRequests() {
    var retfooterVal = '';
    var footerVal = document.getElementById('beginfooter_GridRegisteredRequests_RegisteredRequests').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfRegisteredRequestsPageCount_RegisteredRequests').value) > 0 ? CurrentPageIndex_GridRegisteredRequests_RegisteredRequests + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfRegisteredRequestsPageCount_RegisteredRequests').value;
        if ((i == 1 || i == 3) && GridRegisteredRequests_RegisteredRequests.get_table().getRowCount() == 0)
            footerValCol[i] = 0;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('beginfooter_GridRegisteredRequests_RegisteredRequests').innerHTML = document.getElementById('endfooter_GridRegisteredRequests_RegisteredRequests').innerHTML = retfooterVal;
}

function CallBack_GridRegisteredRequests_RegisteredRequests_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridRegisteredRequests_RegisteredRequests').innerHTML = '';
    ShowConnectionError_RegisteredRequests();
}

function ShowConnectionError_RegisteredRequests() {
    var error = document.getElementById('hfErrorType_RegisteredRequests').value;
    var errorBody = document.getElementById('hfConnectionError_RegisteredRequests').value;
    showDialog(error, errorBody, 'error');
}

function SetClmnImage_GridRegisteredRequests_RegisteredRequests(dataField, Key) {
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
    }
    return cellImage;
}

function SetCellTitle_GridRegisteredRequests_RegisteredRequests(dataField, Key) {
    var elementID = null;
    switch (dataField) {
        case 'FlowStatus':
            elementID = 'hfRequestStates_RegisteredRequests';
            break;
        case 'RequestType':
            elementID = 'hfRequestTypes_RegisteredRequests';
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

function GetRequestFlowLevel_GridRegisteredRequests_RegisteredRequests() {
    ShowDialogEndorsementFlowState_RegisteredRequests();
}

function ShowDialogEndorsementFlowState_RegisteredRequests() {
    if (GridRegisteredRequests_RegisteredRequests.getSelectedItems().length > 0) {
        var ObjEndorsementFlowState = new Object();
        ObjEndorsementFlowState.ManagerFlowID = GridRegisteredRequests_RegisteredRequests.getSelectedItems()[0].getMember('ManagerFlowID').get_text();
        ObjEndorsementFlowState.PersonnelID = GridRegisteredRequests_RegisteredRequests.getSelectedItems()[0].getMember('PersonId').get_text();
        ObjEndorsementFlowState.RequestID = GridRegisteredRequests_RegisteredRequests.getSelectedItems()[0].getMember('RequestID').get_text();
        parent.DialogEndorsementFlowState.set_value(ObjEndorsementFlowState);
        parent.DialogEndorsementFlowState.Show();
        CollapseControls_RegisteredRequests();
    }
}

function ShowDescription_GridRegisteredRequests_RegisteredRequests(DescriptionType) {
    if (GridRegisteredRequests_RegisteredRequests.getSelectedItems().length > 0) {
        var DescriptionTitle = null;
        switch (DescriptionType) {
            case 'Description':
                DescriptionTitle = document.getElementById('hfRequestDescription_RegisteredRequests').value;
                break;
            case 'ManagerDescription':
                DescriptionTitle = document.getElementById('hfManagerDescription_RegisteredRequests').value;
                break;
        }
        document.getElementById('txtDescription_RequestDescription_RegisteredRequests').value = GridRegisteredRequests_RegisteredRequests.getSelectedItems()[0].getMember(DescriptionType).get_text();
        document.getElementById('lblDescription_RequestDescription_RegisteredRequests').innerHTML = DescriptionTitle;
        DialogRequestDescription.Show();
        CollapseControls_RegisteredRequests();
    }
}

function tlbItemExit_tlbExit_RequestDescription_RegisteredRequests_onClick() {
    DialogRequestDescription.Close();
}

function ChangeLoadState_GridRegisteredRequests_RegisteredRequests(state) {
    LoadState_RegisteredRequests = state;
    SetPageIndex_GridRegisteredRequests_RegisteredRequests(0);
}

function RequestRegister_onAfterUpdate(pageIndex) {
    LoadState_RegisteredRequests = 'UnKnown';
    SetPageIndex_GridRegisteredRequests_RegisteredRequests(pageIndex);
}

function tlbItemRefresh_TlbPaging_GridRegisteredRequests_RegisteredRequests_onClick() {
    ChangeLoadState_GridRegisteredRequests_RegisteredRequests('UnKnown');
}

function tlbItemFirst_TlbPaging_GridRegisteredRequests_RegisteredRequests_onClick() {
    SetPageIndex_GridRegisteredRequests_RegisteredRequests(0);
}

function tlbItemBefore_TlbPaging_GridRegisteredRequests_RegisteredRequests_onClick() {
    if (CurrentPageIndex_GridRegisteredRequests_RegisteredRequests != 0) {
        CurrentPageIndex_GridRegisteredRequests_RegisteredRequests = CurrentPageIndex_GridRegisteredRequests_RegisteredRequests - 1;
        SetPageIndex_GridRegisteredRequests_RegisteredRequests(CurrentPageIndex_GridRegisteredRequests_RegisteredRequests);
    }
}

function tlbItemNext_TlbPaging_GridRegisteredRequests_RegisteredRequests_onClick() {
    if (CurrentPageIndex_GridRegisteredRequests_RegisteredRequests < parseInt(document.getElementById('hfRegisteredRequestsPageCount_RegisteredRequests').value) - 1) {
        CurrentPageIndex_GridRegisteredRequests_RegisteredRequests = CurrentPageIndex_GridRegisteredRequests_RegisteredRequests + 1;
        SetPageIndex_GridRegisteredRequests_RegisteredRequests(CurrentPageIndex_GridRegisteredRequests_RegisteredRequests);
    }
}

function tlbItemLast_TlbPaging_GridRegisteredRequests_RegisteredRequests_onClick() {
    SetPageIndex_GridRegisteredRequests_RegisteredRequests(parseInt(document.getElementById('hfRegisteredRequestsPageCount_RegisteredRequests').value) - 1);
}

function SetPageIndex_GridRegisteredRequests_RegisteredRequests(pageIndex) {
    CurrentPageIndex_GridRegisteredRequests_RegisteredRequests = pageIndex;
    Fill_GridRegisteredRequests_RegisteredRequests(pageIndex);
}

function Fill_GridRegisteredRequests_RegisteredRequests(pageIndex) {
    document.getElementById('loadingPanel_GridRegisteredRequests_RegisteredRequests').innerHTML = document.getElementById('hfloadingPanel_GridRegisteredRequests_RegisteredRequests').value;
    var currentUserState = document.getElementById('CurrentUserState_RegisteredRequests').value;
    var pageSize = parseInt(document.getElementById('hfRegisteredRequestsPageSize_RegisteredRequests').value);
    var year = document.getElementById('hfCurrentYear_RegisteredRequests').value;
    var month = document.getElementById('hfCurrentMonth_RegisteredRequests').value;
    var StrFilterConditions = GetFilterConditions_RegisteredRequests();    
    CallBack_GridRegisteredRequests_RegisteredRequests.callback(CharToKeyCode_RegisteredRequests(currentUserState), CharToKeyCode_RegisteredRequests(LoadState_RegisteredRequests), CharToKeyCode_RegisteredRequests(year), CharToKeyCode_RegisteredRequests(month), CharToKeyCode_RegisteredRequests(StrFilterConditions), CharToKeyCode_RegisteredRequests(pageSize.toString()), CharToKeyCode_RegisteredRequests(pageIndex.toString()));
}

function tlbItemApplyConditions_TlbApplyFilterConditions_RegisteredRequests_onClick() {
    ChangeLoadState_GridRegisteredRequests_RegisteredRequests('CustomFilter');
    DialogRegisteredRequestsFilter.Close();
}

function GetFilterConditions_RegisteredRequests() {
    var StrFilterConditions = '';
    var currentUserState = document.getElementById('CurrentUserState_RegisteredRequests').value;
    var personnelID = '0';
    var requestTypeID = '-1';
    var requestExporter = '-1';
    var fromDate = '';
    var toDate = '';
    if (LoadState_RegisteredRequests == 'CustomFilter') {        
        if (currentUserState == 'Operator' && cmbPersonnel_RegisteredRequests.getSelectedItem() != undefined) {
            var ObjPersonnelDetails = cmbPersonnel_RegisteredRequests.getSelectedItem().get_value();
            ObjPersonnelDetails = eval('('+ObjPersonnelDetails+')');
            personnelID = ObjPersonnelDetails.ID;
        }
        if (cmbRequestType_RegisteredRequests.getSelectedItem() != undefined)
            requestTypeID = cmbRequestType_RegisteredRequests.getSelectedItem().get_id();
        if (cmbExporter_RegisteredRequests.getSelectedItem() != undefined)
            requestExporter = cmbExporter_RegisteredRequests.getSelectedItem().get_id();
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                fromDate = document.getElementById('pdpFromDate_RegisteredRequests').value;
                toDate = document.getElementById('pdpToDate_RegisteredRequests').value;
                break;
            case 'en-US':
                fromDate = document.getElementById('gdpFromDate_RegisteredRequests_picker').value;
                toDate = document.getElementById('gdpToDate_RegisteredRequests_picker');
                break;
        }
        StrFilterConditions = '{"currentUserState":"' + currentUserState + '","personnelID":"' + personnelID + '","requestTypeID":"' + requestTypeID + '","requestExporter":"' + requestExporter + '","fromDate":"' + fromDate + '","toDate":"' + toDate + '"}';
    }
    return StrFilterConditions;
}

function tlbItemExit_tlbExit_RegisterteredRequests_onClick() {
    DialogRegisteredRequestsFilter.Close();
    ClearCustomFilterConditions_RegisteresRequests();
    CollapseControls_RegisteredRequests();
}

function ClearCustomFilterConditions_RegisteresRequests() {
    Clean_cmbRequestType_RegisteredRequests();
    Clean_cmbExporter_RegisteredRequests();
    Clean_FromDateCalendars_RegisteredRequests();
    Clean_ToDateCalendars_RegisteredRequests();
}

function cmbRequestType_RegisteredRequests_onExpand(sender, e) {
    if (cmbRequestType_RegisteredRequests.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRequestType_RegisteredRequests == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRequestType_RegisteredRequests = true;
        CallBack_cmbRequestType_RegisteredRequests.callback();
    }
}

function CallBack_cmbRequestType_RegisteredRequests_onBeforeCallback(sender, e) {
    cmbRequestType_RegisteredRequests.dispose();
}

function CallBack_cmbRequestType_RegisteredRequests_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RequestsTypes').value;
    ChangeComboContentDirection_RegisteredRequests('cmbRequestType_RegisteredRequests');
    if (error == "") {
        document.getElementById('cmbRequestType_RegisteredRequests_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbRequestType_RegisteredRequests_DropImage').mousedown();
        cmbRequestType_RegisteredRequests.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbRequestType_RegisteredRequests_DropDown').style.display = 'none';
    }
}

function ChangeComboContentDirection_RegisteredRequests(cmbID) {
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById(cmbID + '_DropDownContent').dir = 'rtl';
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function cmbExporter_RegisteredRequests_onExpand(sender, e) {
    if (cmbExporter_RegisteredRequests.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbExporter_RegisteredRequests == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbExporter_RegisteredRequests = true;
        CallBack_cmbExporter_RegisteredRequests.callback();
    }
}

function CallBack_cmbExporter_RegisteredRequests_onBeforeCallback(sender, e) {
    cmbExporter_RegisteredRequests.dispose();
}

function CallBack_cmbExporter_RegisteredRequests_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Exporters').value;
    ChangeComboContentDirection_RegisteredRequests('cmbExporter_RegisteredRequests');
    if (error == "") {
        document.getElementById('cmbExporter_RegisteredRequests_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbExporter_RegisteredRequests_DropImage').mousedown();
        cmbExporter_RegisteredRequests.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbExporter_RegisteredRequests_DropDown').style.display = 'none';
    }
}

function tlbItemClean_TlbClean_cmbRequestType_RegisteredRequests_onClick() {
    Clean_cmbRequestType_RegisteredRequests();
}

function Clean_cmbRequestType_RegisteredRequests() {
    document.getElementById('cmbRequestType_RegisteredRequests').value = '';
    cmbRequestType_RegisteredRequests.unSelect();
}

function tlbItemClean_TlbClean_cmbExporter_RegisteredRequests_onClick() {
    Clean_cmbExporter_RegisteredRequests();
}

function Clean_cmbExporter_RegisteredRequests() {
    document.getElementById('cmbExporter_RegisteredRequests').value = '';
    cmbExporter_RegisteredRequests.unSelect();
}

function tlbItemClean_TlbClean_FromDateCalendars_RegisteredRequests_onClick() {
    Clean_FromDateCalendars_RegisteredRequests();
}

function Clean_FromDateCalendars_RegisteredRequests() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_RegisteredRequests').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_RegisteredRequests').value = "";
            break;
    }
}

function TlbItemClean_TlbClean_ToDateCalendars_RegisteredRequests_onClick() {
    Clean_ToDateCalendars_RegisteredRequests();
}

function Clean_ToDateCalendars_RegisteredRequests() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpToDate_RegisteredRequests').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpToDate_RegisteredRequests').value = "";
            break;
    }
}

function DialogRequestDescription_onShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('tbl_DialogRequestDescription_RegisteredRequests').value = '';
        document.getElementById('tbl_DialogRequestDescription_RegisteredRequests').style.direction = 'rtl';
    }
}

function DialogConfirm_OnShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('tblConfirm_DialogConfirm').style.direction = 'rtl';
}

function CallBack_cmbRequestType_RegisteredRequests_onCallbackError(sender, e) {
    ShowConnectionError_RegisteredRequests();
}

function CallBack_cmbExporter_RegisteredRequests_onCallbackError(sender, e) {
    ShowConnectionError_RegisteredRequests();
}

function tlbItemRequestByOperator_TlbRegisteredRequests_onClick() {
    ShowDialogRequestRegister('Operator');
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_RegisteredRequests_onClick() {
    Refresh_cmbPersonnel_RegisteredRequests();
}

function Refresh_cmbPersonnel_RegisteredRequests() {
    LoadState_cmbPersonnel_RegisteredRequests = 'Normal';
    RequestPersonnelCountState_RequestRegister = 'Single';
    StrUnCollectivePersonnelList_CollectiveTraffic = '';
    SetPageIndex_cmbPersonnel_RegisteredRequests(0);
}

function tlbItemFirst_TlbPaging_PersonnelSearch_RegisteredRequests_onClick() {
    SetPageIndex_cmbPersonnel_RegisteredRequests(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_RegisteredRequests_onClick() {
    if (CurrentPageIndex_cmbPersonnel_RegisteredRequests != 0) {
        CurrentPageIndex_cmbPersonnel_RegisteredRequests = CurrentPageIndex_cmbPersonnel_RegisteredRequests - 1;
        SetPageIndex_cmbPersonnel_RegisteredRequests(CurrentPageIndex_cmbPersonnel_RegisteredRequests);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_RegisteredRequests_onClick() {
    if (CurrentPageIndex_cmbPersonnel_RegisteredRequests < parseInt(document.getElementById('hfPersonnelPageCount_RegisteredRequests').value) - 1) {
        CurrentPageIndex_cmbPersonnel_RegisteredRequests = CurrentPageIndex_cmbPersonnel_RegisteredRequests + 1;
        SetPageIndex_cmbPersonnel_RegisteredRequests(CurrentPageIndex_cmbPersonnel_RegisteredRequests);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_RegisteredRequests_onClick() {
    SetPageIndex_cmbPersonnel_RegisteredRequests(parseInt(document.getElementById('hfPersonnelPageCount_RequestRegister').value) - 1);
}

function SetPageIndex_cmbPersonnel_RegisteredRequests(pageIndex) {
    CurrentPageIndex_cmbPersonnel_RegisteredRequests = pageIndex;
    Fill_cmbPersonnel_RegisteredRequests(pageIndex);
}

function tlbItemSearch_TlbSearchPersonnel_RegisteredRequests_onClick() {
    LoadState_cmbPersonnel_RegisteredRequests = 'Search';
    CurrentPageIndex_cmbPersonnel_RegisteredRequests = 0;
    SetPageIndex_cmbPersonnel_RegisteredRequests(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_RegisteredRequests_onClick() {
    LoadState_cmbPersonnel_RegisteredRequests = 'AdvancedSearch';
    CurrentPageIndex_cmbPersonnel_RegisteredRequests = 0;
    ShowDialogPersonnelSearch('RegisteredRequests');
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
    CollapseControls_RegisteredRequests();
}

function RegisteredRequests_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_cmbPersonnel_RegisteredRequests = SearchTerm;
    SetPageIndex_cmbPersonnel_RegisteredRequests(0);
}

function Fill_cmbPersonnel_RegisteredRequests(pageIndex) {
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_RegisteredRequests').value);
    var SearchTermConditions = '';
    switch (LoadState_cmbPersonnel_RegisteredRequests) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm_cmbPersonnel_RegisteredRequests = SearchTermConditions = document.getElementById('txtPersonnelSearch_RegisteredRequests').value;
            break;
        case 'AdvancedSearch':
            SearchTermConditions = AdvancedSearchTerm_cmbPersonnel_RegisteredRequests;
            break;
    }
    CallBack_cmbPersonnel_RegisteredRequests.callback(CharToKeyCode_RegisteredRequests(LoadState_cmbPersonnel_RegisteredRequests), CharToKeyCode_RegisteredRequests(pageSize.toString()), CharToKeyCode_RegisteredRequests(pageIndex.toString()), CharToKeyCode_RegisteredRequests(SearchTermConditions));
}

function cmbPersonnel_RegisteredRequests_onExpand(sender, e) {
    if (cmbPersonnel_RegisteredRequests.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_RegisteredRequests == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_RegisteredRequests = true;
        SetPageIndex_cmbPersonnel_RegisteredRequests(0);
    }
}

function CallBack_cmbPersonnel_RegisteredRequests_onBeforeCallback(sender, e) {
    cmbPersonnel_RegisteredRequests.dispose();
}

function CallBack_cmbPersonnel_RegisteredRequests_onCallBackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_RegisteredRequests').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_RegisteredRequests').value;
    document.getElementById('clmnBarCode_cmbPersonnel_RegisteredRequests').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_RegisteredRequests').value;
    document.getElementById('clmnCardNum_cmbPersonnel_RegisteredRequests').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_RegisteredRequests').value;
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('tblDropDownContent_cmbPersonnel_RegisteredRequests').dir = document.getElementById('cmbPersonnel_RegisteredRequests_DropDownContent').dir = 'rtl';

    var error = document.getElementById('ErrorHiddenField_Personnel_RegisteredRequests').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_RegisteredRequests_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_RegisteredRequests_DropImage').mousedown();
        else
            cmbPersonnel_RegisteredRequests.expand();

        var personnelCount = document.getElementById('hfPersonnelCount_RegisteredRequests').value;
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_RegisteredRequests_DropDown').style.display = 'none';
    }
}

function CallBack_cmbPersonnel_RegisteredRequests_onCallbackError(sender, e) {
    ShowConnectionError_RegisteredRequests();
}

function CollapseControls_RegisteredRequests() {
    var currentUserState = document.getElementById('CurrentUserState_RegisteredRequests').value;
    switch (currentUserState) {
        case 'Operator':
            cmbPersonnel_RegisteredRequests.collapse();
            break;
        case 'NormalUser':
            break;
    }
    cmbYear_RegisteredRequests.collapse();
    cmbMonth_RegisteredRequests.collapse();
    cmbRequestType_RegisteredRequests.collapse();
    cmbExporter_RegisteredRequests.collapse();
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpFromDate_RegisteredRequests');
}

function tlbItemFormReconstruction_TlbRegisteredRequests_onClick() {
    var ObjDialogRegisteredRequests = parent.DialogRegisteredRequests.get_value();
    var caller = ObjDialogRegisteredRequests.Caller;
    RegisteredRequests_onClose();    
    switch (caller) {
        case 'MainPage':
            parent.DialogRegisteredRequests.set_value(ObjDialogRegisteredRequests);
            parent.DialogRegisteredRequests.Show();
            break;
        case 'MonthlyOperationGridSchema':
            parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.ShowDialogRegisteredRequests();
            break;
        case 'MonthlyOperationGanttChartSchema':
            parent.document.getElementById('DialogMonthlyOperationGanttChartSchema_IFrame').contentWindow.ShowDialogRegisteredRequests();
            break;
    }
}

function tlbItemHelp_TlbRegisteredRequests_onClick() {
    LoadHelpPage('tlbItemHelp_TlbRegisteredRequests');    
}

function ShowAttachmentFile_GridRegisteredRequests_RegisteredRequests() {
    var SelectedItems_GridRegisteredRequests_RegisteredRequests = GridRegisteredRequests_RegisteredRequests.getSelectedItems();
    if (SelectedItems_GridRegisteredRequests_RegisteredRequests.length > 0 && SelectedItems_GridRegisteredRequests_RegisteredRequests[0].getMember('AttachmentFile').get_text() != '') {
        var AttachmentFile = SelectedItems_GridRegisteredRequests_RegisteredRequests[0].getMember('AttachmentFile').get_text();
        window.open("ClientAttachmentViewer.aspx?AttachmentType=Request&ClientAttachment=" + CharToKeyCode_RegisteredRequests(AttachmentFile) + "", "ClientAttachmentViewer" + (new Date()).getTime() + "", "width=" + screen.width + ",height=" + screen.height + ",toolbar=yes,location=yes,directories=yes,status=yes,menubar=yes,scrollbars=yes,copyhistory=yes,resizable=yes");
    }
}

function SetAttachmentFileImage_GridRegisteredRequests_RegisteredRequests(attachmentFile) {
    var innerHTML = '';
    if (attachmentFile != undefined && attachmentFile != null && attachmentFile != '')
        innerHTML = '<img src="Images/Grid/attachment.png" alt="" />';
    return innerHTML;
}






