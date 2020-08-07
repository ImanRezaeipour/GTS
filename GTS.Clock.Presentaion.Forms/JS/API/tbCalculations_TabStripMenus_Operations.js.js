
var CurrentPageCombosCallBcakStateObj = new Object();
var LoadState_cmbPersonnel_Calculations = 'Normal';
var SearchTerm_cmbPersonnel_Calculations = '';
var AdvancedSearchTerm_cmbPersonnel_Calculations = '';
var CurrentPageIndex_cmbPersonnel_Calculations = 0;
var CalculatePersonnelCountState_Calculations = 'Single';
var ConfirmState_Calculations = null;
var CalculationsProgress = null;


function GetBoxesHeaders_CalculationsForm() {
    document.getElementById('clmnName_cmbPersonnel_Calculations').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_Calculations').value;
    document.getElementById('clmnBarCode_cmbPersonnel_Calculations').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_Calculations').value;
    document.getElementById('clmnCardNum_cmbPersonnel_Calculations').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_Calculations').value;
    document.getElementById('lblSelectedPersonnelCount_Calculations').innerHTML = document.getElementById('hfheaderPersonnelCount_Calculations').value + +document.getElementById('hfPersonnelSelectedCount_Calculations').value;
}

function btn_gdpFromDate_tbDaily_Calculations_OnMouseUp(event) {
    if (gCalFromDate_tbDaily_Calculations.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function btn_gdpFromDate_tbDaily_Calculations_OnClick(event) {
    if (gCalFromDate_tbDaily_Calculations.get_popUpShowing()) {
        gCalFromDate_tbDaily_Calculations.hide();
    }
    else {
        gCalFromDate_tbDaily_Calculations.setSelectedDate(gdpFromDate_tbDaily_Calculations.getSelectedDate());
        gCalFromDate_tbDaily_Calculations.show();
    }
}

function gdpFromDate_tbDaily_Calculations_OnDateChange(sender, e) {
    var FromDate = gdpFromDate_tbDaily_Calculations.getSelectedDate();
    gCalFromDate_tbDaily_Calculations.setSelectedDate(FromDate);
}

function gCalFromDate_tbDaily_Calculations_OnChange(sender, e) {
    var FromDate = gCalFromDate_tbDaily_Calculations.getSelectedDate();
    gdpFromDate_tbDaily_Calculations.setSelectedDate(FromDate);
}

function gCalFromDate_tbDaily_Calculations_OnLoad(sender, e) {
    window.gCalFromDate_tbDaily_Calculations.PopUpObject.z = 25000000;
}

function btn_gdpToDate_tbDaily_Calculations_OnMouseUp(event) {
    if (gCalToDate_tbDaily_Calculations.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function btn_gdpToDate_tbDaily_Calculations_OnClick(event) {
    if (gCalToDate_tbDaily_Calculations.get_popUpShowing()) {
        gCalToDate_tbDaily_Calculations.hide();
    }
    else {
        gCalToDate_tbDaily_Calculations.setSelectedDate(gdpToDate_tbDaily_Calculations.getSelectedDate());
        gCalToDate_tbDaily_Calculations.show();
    }
}

function gdpToDate_tbDaily_Calculations_OnDateChange(sender, e) {
    var ToDate = gdpToDate_tbDaily_Calculations.getSelectedDate();
    gCalToDate_tbDaily_Calculations.setSelectedDate(ToDate);
}

function gCalToDate_tbDaily_Calculations_OnChange(sender, e) {
    var ToDate = gCalToDate_tbDaily_Calculations.getSelectedDate();
    gdpToDate_tbDaily_Calculations.setSelectedDate(ToDate);
}

function gCalToDate_tbDaily_Calculations_OnLoad(sender, e) {
    window.gCalToDate_tbDaily_Calculations.PopUpObject.z = 25000000;
}

function tlbItemExit_TlbHourly_onClick() {
    ShowDialogConfirm('Calculations');
}

function ShowDialogConfirm(caller) {
    switch (caller) {
        case 'Calculations':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Calculations').value;
            break;
    }
    DialogConfirm.set_value(caller);
    DialogConfirm.Show();
    CollapseControls_Calculations();
}

function CallBack_cmbPersonnel_Calculations_onBeforeCallback(sender, e) {
    cmbPersonnel_Calculations.dispose();
}

function cmbPersonnel_Calculations_onChange(sender, e) {
    SetSelectedPersonnelCount_Calculations();
}

function CallBack_cmbPersonnel_Calculations_onCallBackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_Calculations').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_Calculations').value;
    document.getElementById('clmnBarCode_cmbPersonnel_Calculations').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_Calculations').value;
    document.getElementById('clmnCardNum_cmbPersonnel_Calculations').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_Calculations').value;
    SetSelectedPersonnelCount_Calculations();
    SetPosition_cmbPersonnel_Calculations();
    var error = document.getElementById('ErrorHiddenField_Personnel_Calculations').value;
    if (error == "") {
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbPersonnel_Calculations = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbPersonnel_Calculations = false;
        document.getElementById('cmbPersonnel_Calculations_DropDown').style.display = 'none';
        cmbPersonnel_Calculations.expand();
        ChangeControlDirection_Calculations('cmbPersonnel_Calculations_DropDown');

        var personnelCount = document.getElementById('hfPersonnelCount_Calculations').value;
        ChangeRequestPersonnelCount_Calculations(personnelCount);
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_Calculations_DropDown').style.display = 'none';
    }
}

function CallBack_cmbPersonnel_Calculations_onCallbackError(sender, e) {
    ShowConnectionError_Calculations();
}

function ShowConnectionError_Calculations() {
    var error = document.getElementById('hfErrorType_Calculations').value;
    var errorBody = document.getElementById('hfConnectionError_Calculations').value;
    showDialog(error, errorBody, 'error');
}


function ViewCurrentLangCalendars_Calculations() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpFromDate_tbDaily_Calculations").parentNode.removeChild(document.getElementById("pdpFromDate_tbDaily_Calculations"));
            document.getElementById("pdpFromDate_tbDaily_Calculationsimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_tbDaily_Calculationsimgbt"));
            document.getElementById("pdpToDate_tbDaily_Calculations").parentNode.removeChild(document.getElementById("pdpToDate_tbDaily_Calculations"));
            document.getElementById("pdpToDate_tbDaily_Calculationsimgbt").parentNode.removeChild(document.getElementById("pdpToDate_tbDaily_Calculationsimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_FromDateCalendars_tbDaily_Calculations").removeChild(document.getElementById("Container_gCalFromDate_tbDaily_Calculations"));
            document.getElementById("Container_ToDateCalendars_tbDaily_Calculations").removeChild(document.getElementById("Container_gCalToDate_tbDaily_Calculations"));
            break;
    }
}

function ResetCalendars_Calculations() {
    var currentDate_Calculations = document.getElementById('hfCurrentDate_Calculations').value;
    switch (parent.parent.SysLangID) {
        case 'en-US':
            currentDate_Calculations = new Date(currentDate_Calculations);
            gdpFromDate_tbDaily_Calculations.setSelectedDate(currentDate_Calculations);
            gCalFromDate_tbDaily_Calculations.setSelectedDate(currentDate_Calculations);
            gdpToDate_tbDaily_Calculations.setSelectedDate(currentDate_Calculations);
            gCalToDate_tbDaily_Calculations.setSelectedDate(currentDate_Calculations);
            break;
        case 'fa-IR':
            document.getElementById('pdpFromDate_tbDaily_Calculations').value = currentDate_Calculations;
            document.getElementById('pdpToDate_tbDaily_Calculations').value = currentDate_Calculations;
            break;
    }
}

function ChangeControlDirection_Calculations(ctrl) {
    var direction = null;
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    if (ctrl == 'All') {
        if (document.getElementById('cmbPersonnel_Calculations_DropDownContent') != null)
            document.getElementById('cmbPersonnel_Calculations_DropDownContent').dir = direction;
    }
    else
        document.getElementById(ctrl).style.direction = direction;
}

function cmbPersonnel_Calculations_onExpand(sender, e) {
    SetPosition_cmbPersonnel_Calculations();
    if (cmbPersonnel_Calculations.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_Calculations == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_Calculations = true;
        SetPageIndex_cmbPersonnel_Calculations(0);
    }
}

function cmbPersonnel_Calculations_onCollapse(sender, e) {    
    if (CurrentPageCombosCallBcakStateObj.cmbPersonnel_Calculations) {
        CurrentPageCombosCallBcakStateObj.cmbPersonnel_Calculations = false;
        cmbPersonnel_Calculations.expand();
    }
}

function SetPageIndex_cmbPersonnel_Calculations(pageIndex) {
    CurrentPageIndex_cmbPersonnel_Calculations = pageIndex;
    Fill_cmbPersonnel_Calculations(pageIndex);
}

function Fill_cmbPersonnel_Calculations(pageIndex) {
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_Calculations').value);
    var SearchTermConditions = '';
    switch (LoadState_cmbPersonnel_Calculations) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm_cmbPersonnel_Calculations = SearchTermConditions = document.getElementById('txtPersonnelSearch_Calculations').value;
            break;
        case 'AdvancedSearch':
            SearchTermConditions = AdvancedSearchTerm_cmbPersonnel_Calculations;
            break;
    }
    CallBack_cmbPersonnel_Calculations.callback(CharToKeyCode_Calculations(LoadState_cmbPersonnel_Calculations), CharToKeyCode_Calculations(pageSize.toString()), CharToKeyCode_Calculations(pageIndex.toString()), CharToKeyCode_Calculations(SearchTermConditions));
}

function CharToKeyCode_Calculations(str) {
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

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function ChangeRequestPersonnelCount_Calculations(personnelCount) {
    var countVal = document.getElementById('headerPersonnelCount_Calculations').innerHTML;
    var countValCol = countVal.split(':');
    countVal = countValCol[0] + ':' + personnelCount;
}

function tlbItemSearch_TlbSearchPersonnel_Calculations_onClick() {
    LoadState_cmbPersonnel_Calculations = 'Search';
    CurrentPageIndex_cmbPersonnel_Calculations = 0;
    SetPageIndex_cmbPersonnel_Calculations(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_Calculations_onClick() {
    LoadState_cmbPersonnel_Calculations = 'AdvancedSearch';
    CurrentPageIndex_cmbPersonnel_Calculations = 0;
    ShowDialogPersonnelSearch('Calculations');
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
    CollapseControls_Calculations();
}

function CollapseControls_Calculations() {
    cmbPersonnel_Calculations.collapse();
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpFromDate_tbDaily_Calculations');
}

function Calculations_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_cmbPersonnel_Calculations = SearchTerm;
    SetPageIndex_cmbPersonnel_Calculations(0);
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_Calculations_onClick() {
    Refresh_cmbPersonnel_Calculations();
}

function Refresh_cmbPersonnel_Calculations() {
    ChangeLoadState_cmbPersonnel_Calculations('Normal');
}

function tlbItemFirst_TlbPaging_PersonnelSearch_Calculations_onClick() {
    SetPageIndex_cmbPersonnel_Calculations(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_Calculations_onClick() {
    if (CurrentPageIndex_cmbPersonnel_Calculations != 0) {
        CurrentPageIndex_cmbPersonnel_Calculations = CurrentPageIndex_cmbPersonnel_Calculations - 1;
        SetPageIndex_cmbPersonnel_Calculations(CurrentPageIndex_cmbPersonnel_Calculations);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_Calculations_onClick() {
    if (CurrentPageIndex_cmbPersonnel_Calculations < parseInt(document.getElementById('hfPersonnelPageCount_Calculations').value) - 1) {
        CurrentPageIndex_cmbPersonnel_Calculations = CurrentPageIndex_cmbPersonnel_Calculations + 1;
        SetPageIndex_cmbPersonnel_Calculations(CurrentPageIndex_cmbPersonnel_Calculations);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_Calculations_onClick() {
    SetPageIndex_cmbPersonnel_Calculations(parseInt(document.getElementById('hfPersonnelPageCount_Calculations').value) - 1);
}

function ChangeLoadState_cmbPersonnel_Calculations(state) {
    LoadState_cmbPersonnel_Calculations = state;
    SetPageIndex_cmbPersonnel_Calculations(0);
}

function SetPosition_cmbPersonnel_Calculations() {
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('cmbPersonnel_Calculations_DropDown').style.left = document.getElementById('Mastertbl_Calculations').clientWidth - 400 + 'px';
    if (parent.CurrentLangID == 'en-US')
        document.getElementById('cmbPersonnel_Calculations_DropDown').style.left = '10px';
}

function tlbItemFormReconstruction_TlbCalculationsIntroduction_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvCalculations_iFrame').src = 'Calculations.aspx';
}

function ChangeCalculatePersonnelCountState_Calculations(state) {
    switch (state) {
        case "Single":
            document.getElementById('RdbSinglePersonel_Calculations').checked = true;
            document.getElementById('RdbGroupPersonel_Calculations').checked = false;
            CalculatePersonnelCountState_Calculations = 'Single';
            break;
        case "Group":
            document.getElementById('RdbSinglePersonel_Calculations').checked = false;
            document.getElementById('RdbGroupPersonel_Calculations').checked = true;
            CalculatePersonnelCountState_Calculations = 'Group';
            break;
    }
    SetSelectedPersonnelCount_Calculations();
}

function tlbItemCalculate_TlbCalculations_onClick() {
    Calculte_Calculations();
}

function Calculte_Calculations() {
    var fromDate_Calculations = '';
    var toDate_Calculations = '';
    var PersonnelID = '0';
    var SearchTermConditions = '';

    ClearCalculationsProgressFeatures_Calculations();
    switch (LoadState_cmbPersonnel_Calculations) {
        case 'Normal':
            break;
        case 'Search':
            SearchTermConditions = document.getElementById('txtPersonnelSearch_Calculations').value;
            break;
        case 'AdvancedSearch':
            SearchTermConditions = AdvancedSearchTerm_cmbPersonnel_Calculations;
            break;
    }
    if (cmbPersonnel_Calculations.getSelectedItem() != undefined)
        PersonnelID = cmbPersonnel_Calculations.getSelectedItem().get_id();

    switch (parent.SysLangID) {
        case 'fa-IR':
            fromDate_Calculations = document.getElementById('pdpFromDate_tbDaily_Calculations').value;
            toDate_Calculations = document.getElementById('pdpToDate_tbDaily_Calculations').value;
            break;
        case 'en-US':
            fromDate_Calculations = document.getElementById('gdpFromDate_tbDaily_Calculations_picker').value;
            toDate_Calculations = document.getElementById('gdpToDate_tbDaily_Calculations_picker').value;
            break;
    }

    Calculate_CalculationsPage(CharToKeyCode_Calculations(LoadState_cmbPersonnel_Calculations), CharToKeyCode_Calculations(SearchTermConditions), CharToKeyCode_Calculations(fromDate_Calculations), CharToKeyCode_Calculations(toDate_Calculations), CharToKeyCode_Calculations(PersonnelID), CharToKeyCode_Calculations(CalculatePersonnelCountState_Calculations));
}

function Calculate_CalculationsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Calculations').value;
            Response[1] = document.getElementById('hfConnectionError_Calculations').value;
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
        }
    }
    if (RetMessage[2] == 'error')
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
    if (RetMessage[2] == 'success')
        SetProgressbarPercentage_Calculations();
}

function SetProgressbarPercentage_Calculations() {
    GetCalculationsPercentage_CalculationsPage();
}

function GetCalculationsPercentage_CalculationsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Calculations').value;
            Response[1] = document.getElementById('hfConnectionError_Calculations').value;
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
        }
        if (RetMessage[2] == 'success') {
            CalculationsProgress = RetMessage[3];
            CalculationsProgress = eval('(' + CalculationsProgress + ')');
            var ProgressPercent = CalculationsProgress.Progress;
            if (ProgressPercent > 0) {
                SetCalculationsProgressFeatures_Calculations(CalculationsProgress);
                if (ProgressPercent < 100)
                    setTimeout('SetProgressbarPercentage_Calculations();', 10000);
                else
                    if (ProgressPercent == 100)
                        showDialog(RetMessage[0], Response[1], RetMessage[2]);
            }
        }
        else
            if (RetMessage[2] != '')
                showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function SetCalculationsProgressFeatures_Calculations(CalculationsProgress) {
    var ProgressPercent = CalculationsProgress.Progress;
    var AllPersonnelCount = CalculationsProgress.AllPersonnelCount;
    var CalculatedPersonnelCount = CalculationsProgress.CalculatedPersonnelCount;

    document.getElementById('Container_CalculationProgressFeatures').style.visibility = 'visible';
    for (var i = 1; i <= ProgressPercent; i++) {
        document.getElementById('p' + i + '_Progressbar_Calculations').style.backgroundColor = '#4B0082';
    }
    document.getElementById('tdAllPersonnelCount_Calculations').innerHTML = document.getElementById('hfAllPersonnelCount_Calculations').value + ' ' + AllPersonnelCount;
    document.getElementById('tdCalculatedPersonnelCount_Calculations').innerHTML = document.getElementById('hfCalculatedPersonnelCount_Calculations').value + ' ' + CalculatedPersonnelCount;
    document.getElementById('tdProgressPercentage_Calculations').innerHTML = document.getElementById('hfProgressPercentage_Calculations').value + ' ' + ProgressPercent + '%';
}

function ClearCalculationsProgressFeatures_Calculations() {
    CalculationsProgress = null;
    for (var i = 1; i <= 100; i++) {
        document.getElementById('p' + i + '_Progressbar_Calculations').style.backgroundColor = '';
    }
    document.getElementById('tdAllPersonnelCount_Calculations').innerHTML = '';
    document.getElementById('tdCalculatedPersonnelCount_Calculations').innerHTML = '';
    document.getElementById('Container_CalculationProgressFeatures').style.visibility = 'hidden';
}

function SetSelectedPersonnelCount_Calculations() {
    switch (CalculatePersonnelCountState_Calculations) {
        case 'Single':
            if (cmbPersonnel_Calculations.get_selectedIndex() >= 0)
                document.getElementById('lblSelectedPersonnelCount_Calculations').innerHTML = document.getElementById('hfheaderPersonnelCount_Calculations').value + 1;
            else
                document.getElementById('lblSelectedPersonnelCount_Calculations').innerHTML = document.getElementById('hfheaderPersonnelCount_Calculations').value + 0;
            break;
        case 'Group':
            document.getElementById('lblSelectedPersonnelCount_Calculations').innerHTML = document.getElementById('hfheaderPersonnelCount_Calculations').value + document.getElementById('hfPersonnelSelectedCount_Calculations').value;
            break;
    }
}

function tlbItemExit_TlbCalculations_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    CollapseControls_Calculations();
    ConfirmState_Calculations = confirmState;
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Calculations').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Calculations) {
        case 'Exit':
            parent.CloseCurrentTabOnTabStripMenus();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function tlbItemHelp_TlbCalculationsIntroduction_onClick() {
    LoadHelpPage('tlbItemHelp_TlbCalculationsIntroduction');
}



