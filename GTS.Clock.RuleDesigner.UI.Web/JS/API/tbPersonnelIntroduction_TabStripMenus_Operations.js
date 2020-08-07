
var CurrentPageState_Personnel = 'View';
var ConfirmState_Personnel = null;
var ObjPersonnel_Personnel = null;
var CurrentPageIndex_GridPersonnel_Personnel = 0;
var OriginalText_lblPersonnelCount_Personnel = null;
var LoadState_Personnel = 'Normal';
var AdvancedSearchTerm_Personnel = '';

function SetHorizontalScrollingDirection_GridPersonnel_Personnel_Opera() {
    if (navigator.userAgent.indexOf('Opera') != -1 && parent.CurrentLangID == "fa-IR") 
        document.getElementById('GridPersonnel_Personnel').style.direction = "ltr";
}

function ShowDialogPersonnelMainInformation() {
    var CurrentStateObj_Personnel = new Object();
    switch (CurrentPageState_Personnel) {
        case 'Add':
            CurrentStateObj_Personnel.PageState = CurrentPageState_Personnel;
            CurrentStateObj_Personnel.PageSize = document.getElementById('hfPersonnelPageSize_Personnel').value;
            CurrentStateObj_Personnel.PersonnelID = '0';
            parent.DialogPersonnelMainInformation.set_value(CurrentStateObj_Personnel);
            parent.DialogPersonnelMainInformation.Show();
            break;
        case 'Edit':
            var selectedPersonnel_Personnel = GridPersonnel_Personnel.getSelectedItems()[0];
            if (selectedPersonnel_Personnel != undefined) {
                CurrentStateObj_Personnel.PageState = CurrentPageState_Personnel;
                CurrentStateObj_Personnel.PageSize = document.getElementById('hfPersonnelPageSize_Personnel').value;
                CurrentStateObj_Personnel.PersonnelID = selectedPersonnel_Personnel.getMember('ID').get_text();
                CurrentStateObj_Personnel.IsActive = selectedPersonnel_Personnel.getMember('Active').get_text();
                CurrentStateObj_Personnel.FirstName = selectedPersonnel_Personnel.getMember('FirstName').get_text();
                CurrentStateObj_Personnel.LastName = selectedPersonnel_Personnel.getMember('LastName').get_text();
                CurrentStateObj_Personnel.SexID = selectedPersonnel_Personnel.getMember('Sex').get_text();
                CurrentStateObj_Personnel.SexTitle = selectedPersonnel_Personnel.getMember('SexTitle').get_text();
                CurrentStateObj_Personnel.FatherName = selectedPersonnel_Personnel.getMember('PersonDetail.FatherName').get_text();
                CurrentStateObj_Personnel.NationalCode = selectedPersonnel_Personnel.getMember('PersonDetail.MeliCode').get_text();
                CurrentStateObj_Personnel.MilitaryState = selectedPersonnel_Personnel.getMember('PersonDetail.MilitaryStatus').get_text();
                CurrentStateObj_Personnel.MilitaryStateTitle = selectedPersonnel_Personnel.getMember('PersonDetail.MilitaryStatusTitle').get_text();
                CurrentStateObj_Personnel.IdentityCertificate = selectedPersonnel_Personnel.getMember('PersonDetail.BirthCertificate').get_text();
                CurrentStateObj_Personnel.IssuanceLocation = selectedPersonnel_Personnel.getMember('PersonDetail.PlaceIssued').get_text();
                CurrentStateObj_Personnel.Education = selectedPersonnel_Personnel.getMember('Education').get_text();
                CurrentStateObj_Personnel.MarriageStateID = selectedPersonnel_Personnel.getMember('MaritalStatus').get_text();
                CurrentStateObj_Personnel.MarriageStateTitle = selectedPersonnel_Personnel.getMember('MaritalStatusTitle').get_text();
                CurrentStateObj_Personnel.State = selectedPersonnel_Personnel.getMember('PersonDetail.Status').get_text();
                CurrentStateObj_Personnel.Tel = selectedPersonnel_Personnel.getMember('PersonDetail.Tel').get_text();
                CurrentStateObj_Personnel.Address = selectedPersonnel_Personnel.getMember('PersonDetail.Address').get_text();
                CurrentStateObj_Personnel.BirthLocation = selectedPersonnel_Personnel.getMember('PersonDetail.BirthPlace').get_text();
                CurrentStateObj_Personnel.BirthDate = CheckDate_PersonnelMainInformation(selectedPersonnel_Personnel.getMember('PersonDetail.UIBirthDate').get_text());
                CurrentStateObj_Personnel.PersonnelNumber = selectedPersonnel_Personnel.getMember('PersonCode').get_text();
                CurrentStateObj_Personnel.CardNumber = selectedPersonnel_Personnel.getMember('CardNum').get_text();
                CurrentStateObj_Personnel.DepartmentID = selectedPersonnel_Personnel.getMember('Department.ID').get_text();
                CurrentStateObj_Personnel.DepartmentName = selectedPersonnel_Personnel.getMember('Department.Name').get_text();
                CurrentStateObj_Personnel.OrganizationPostID = selectedPersonnel_Personnel.getMember('OrganizationUnit.ID').get_text();
                CurrentStateObj_Personnel.OrganizationPostName = selectedPersonnel_Personnel.getMember('OrganizationUnit.Name').get_text();
                CurrentStateObj_Personnel.OrganizationPostCustomCode = selectedPersonnel_Personnel.getMember('OrganizationUnit.CustomCode').get_text();
                CurrentStateObj_Personnel.ParentOrganizationPostID = selectedPersonnel_Personnel.getMember('OrganizationUnit.ParentID').get_text();
                CurrentStateObj_Personnel.CurrentActiveWorkGroup = selectedPersonnel_Personnel.getMember('CurrentActiveWorkGroup').get_text();
                CurrentStateObj_Personnel.CurrentActiveRuleGroup = selectedPersonnel_Personnel.getMember('CurrentActiveRuleGroup').get_text();
                CurrentStateObj_Personnel.ControlStationID = selectedPersonnel_Personnel.getMember('ControlStation.ID').get_text();
                CurrentStateObj_Personnel.ControlStationName = selectedPersonnel_Personnel.getMember('ControlStation.Name').get_text();
                CurrentStateObj_Personnel.EmployNumber = selectedPersonnel_Personnel.getMember('EmploymentNum').get_text();
                CurrentStateObj_Personnel.EmployTypeID = selectedPersonnel_Personnel.getMember('EmploymentType.ID').get_text();
                CurrentStateObj_Personnel.EmployTypeName = selectedPersonnel_Personnel.getMember('EmploymentType.Name').get_text();
                CurrentStateObj_Personnel.EmployDate = CheckDate_PersonnelMainInformation(selectedPersonnel_Personnel.getMember('UIEmploymentDate').get_text());
                CurrentStateObj_Personnel.EmployEndDate = CheckDate_PersonnelMainInformation(selectedPersonnel_Personnel.getMember('UIEndEmploymentDate').get_text());
                CurrentStateObj_Personnel.CalculationRangeID = selectedPersonnel_Personnel.getMember('CurrentRangeAssignment.CalcDateRangeGroup.ID').get_text();
                CurrentStateObj_Personnel.CalculationRangeName = selectedPersonnel_Personnel.getMember('CurrentRangeAssignment.CalcDateRangeGroup.Name').get_text();
                CurrentStateObj_Personnel.RunFromDate = selectedPersonnel_Personnel.getMember('CurrentRangeAssignment.UIFromDate').get_text();
                CurrentStateObj_Personnel.UserInterfaceRuleGroupID = selectedPersonnel_Personnel.getMember('UIValidationGroup.ID').get_text();
                CurrentStateObj_Personnel.UserInterfaceRuleGroupName = selectedPersonnel_Personnel.getMember('UIValidationGroup.Name').get_text();
                CurrentStateObj_Personnel.Reserve1 = selectedPersonnel_Personnel.getMember('PersonDetail.R1').get_text();
                CurrentStateObj_Personnel.Reserve1Title = selectedPersonnel_Personnel.getMember('PersonDetail.R1Title').get_text();
                CurrentStateObj_Personnel.Reserve2 = selectedPersonnel_Personnel.getMember('PersonDetail.R2').get_text();
                CurrentStateObj_Personnel.Reserve2Title = selectedPersonnel_Personnel.getMember('PersonDetail.R2Title').get_text();
                CurrentStateObj_Personnel.Reserve3 = selectedPersonnel_Personnel.getMember('PersonDetail.R3').get_text();
                CurrentStateObj_Personnel.Reserve3Title = selectedPersonnel_Personnel.getMember('PersonDetail.R3Title').get_text();
                CurrentStateObj_Personnel.Reserve4 = selectedPersonnel_Personnel.getMember('PersonDetail.R4').get_text();
                CurrentStateObj_Personnel.Reserve4Title = selectedPersonnel_Personnel.getMember('PersonDetail.R4Title').get_text();
                CurrentStateObj_Personnel.Reserve5 = selectedPersonnel_Personnel.getMember('PersonDetail.R5').get_text();
                CurrentStateObj_Personnel.Reserve5Title = selectedPersonnel_Personnel.getMember('PersonDetail.R5Title').get_text();
                CurrentStateObj_Personnel.Reserve6 = selectedPersonnel_Personnel.getMember('PersonDetail.R6').get_text();
                CurrentStateObj_Personnel.Reserve6Title = selectedPersonnel_Personnel.getMember('PersonDetail.R6Title').get_text();
                CurrentStateObj_Personnel.Reserve7 = selectedPersonnel_Personnel.getMember('PersonDetail.R7').get_text();
                CurrentStateObj_Personnel.Reserve7Title = selectedPersonnel_Personnel.getMember('PersonDetail.R7Title').get_text();
                CurrentStateObj_Personnel.Reserve8 = selectedPersonnel_Personnel.getMember('PersonDetail.R8').get_text();
                CurrentStateObj_Personnel.Reserve8Title = selectedPersonnel_Personnel.getMember('PersonDetail.R8Title').get_text();
                CurrentStateObj_Personnel.Reserve9 = selectedPersonnel_Personnel.getMember('PersonDetail.R9').get_text();
                CurrentStateObj_Personnel.Reserve9Title = selectedPersonnel_Personnel.getMember('PersonDetail.R9Title').get_text();
                CurrentStateObj_Personnel.Reserve10 = selectedPersonnel_Personnel.getMember('PersonDetail.R10').get_text();
                CurrentStateObj_Personnel.Reserve10Title = selectedPersonnel_Personnel.getMember('PersonDetail.R10Title').get_text();
                CurrentStateObj_Personnel.Reserve11 = selectedPersonnel_Personnel.getMember('PersonDetail.R11').get_text();
                CurrentStateObj_Personnel.Reserve11Title = selectedPersonnel_Personnel.getMember('PersonDetail.R11Title').get_text();
                CurrentStateObj_Personnel.Reserve12 = selectedPersonnel_Personnel.getMember('PersonDetail.R12').get_text();
                CurrentStateObj_Personnel.Reserve12Title = selectedPersonnel_Personnel.getMember('PersonDetail.R12Title').get_text();
                CurrentStateObj_Personnel.Reserve13 = selectedPersonnel_Personnel.getMember('PersonDetail.R13').get_text();
                CurrentStateObj_Personnel.Reserve13Title = selectedPersonnel_Personnel.getMember('PersonDetail.R13Title').get_text();
                CurrentStateObj_Personnel.Reserve14 = selectedPersonnel_Personnel.getMember('PersonDetail.R14').get_text();
                CurrentStateObj_Personnel.Reserve14Title = selectedPersonnel_Personnel.getMember('PersonDetail.R14Title').get_text();
                CurrentStateObj_Personnel.Reserve15 = selectedPersonnel_Personnel.getMember('PersonDetail.R15').get_text();
                CurrentStateObj_Personnel.Reserve15Title = selectedPersonnel_Personnel.getMember('PersonDetail.R15Title').get_text();
                CurrentStateObj_Personnel.Reserve16 = selectedPersonnel_Personnel.getMember('PersonDetail.R16').get_text();
                CurrentStateObj_Personnel.Reserve16Title = selectedPersonnel_Personnel.getMember('PersonDetail.R16Title').get_text();
                CurrentStateObj_Personnel.Reserve17 = selectedPersonnel_Personnel.getMember('PersonDetail.R17').get_text();
                CurrentStateObj_Personnel.Reserve17Title = selectedPersonnel_Personnel.getMember('PersonDetail.R17Title').get_text();
                CurrentStateObj_Personnel.Reserve18 = selectedPersonnel_Personnel.getMember('PersonDetail.R18').get_text();
                CurrentStateObj_Personnel.Reserve18Title = selectedPersonnel_Personnel.getMember('PersonDetail.R18Title').get_text();
                CurrentStateObj_Personnel.Reserve19 = selectedPersonnel_Personnel.getMember('PersonDetail.R19').get_text();
                CurrentStateObj_Personnel.Reserve19Title = selectedPersonnel_Personnel.getMember('PersonDetail.R19Title').get_text();
                CurrentStateObj_Personnel.Reserve20 = selectedPersonnel_Personnel.getMember('PersonDetail.R20').get_text();
                CurrentStateObj_Personnel.Reserve20Title = selectedPersonnel_Personnel.getMember('PersonDetail.R20Title').get_text();

                parent.DialogPersonnelMainInformation.set_value(CurrentStateObj_Personnel);
                parent.DialogPersonnelMainInformation.Show();
            }
    }
}

function CheckDate_PersonnelMainInformation(date) {
    if (document.getElementById('hfBaseDateString_Personnel').value != date)
        return date;
    else
        return "";
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
}

function tlbItemNew_TlbPersonnel_onClick() {
    CurrentPageState_Personnel = 'Add';
    ShowDialogPersonnelMainInformation();
}

function tlbItemEdit_TlbPersonnel_onClick() {
    CurrentPageState_Personnel = 'Edit';
    ShowDialogPersonnelMainInformation();
}

function tlbItemDelete_TlbPersonnel_onClick() {
    CurrentPageState_Personnel = 'Delete';
    ShowDialogConfirm('Delete');
}

function tlbItemExit_TlbPersonnel_onClick() {
    ShowDialogConfirm('Exit');
}

function tlbItemSearch_TlbPersonnel_onClick() {
    LoadState_Personnel = 'AdvancedSearch';
    ShowDialogPersonnelSearch('MasterPersonnel');
}

function tlbItemSearch_TlbPersonnelQuickSearch_onClick() {
    LoadState_Personnel = 'Search';
    SetPageIndex_GridPersonnel_Personnel(0);
}

function Refresh_GridPersonnel_Personnel() {
    LoadState_Personnel = 'Normal';
    SetPageIndex_GridPersonnel_Personnel(0);
}

function Fill_GridPersonnel_Personnel(pageIndex) {
    document.getElementById('loadingPanel_GridPersonnel_Personnel').innerHTML = document.getElementById('hfloadingPanel_GridPersonnel_Personnel').value;
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_Personnel').value);
    switch (LoadState_Personnel) {
        case 'Normal':
            CallBack_GridPersonnel_Personnel.callback(CharToKeyCode_Personnel(LoadState_Personnel), CharToKeyCode_Personnel(pageSize.toString()), CharToKeyCode_Personnel(pageIndex.toString()));            
            break;
        case 'Search':
            var seachTerm = document.getElementById('txtSerchTerm_Personnel').value;
            CallBack_GridPersonnel_Personnel.callback(CharToKeyCode_Personnel(LoadState_Personnel), CharToKeyCode_Personnel(pageSize.toString()), CharToKeyCode_Personnel(pageIndex.toString()), CharToKeyCode_Personnel(seachTerm));
            break;
        case 'AdvancedSearch':
            var seachTerm = AdvancedSearchTerm_Personnel;
            CallBack_GridPersonnel_Personnel.callback(CharToKeyCode_Personnel(LoadState_Personnel), CharToKeyCode_Personnel(pageSize.toString()), CharToKeyCode_Personnel(pageIndex.toString()), CharToKeyCode_Personnel(seachTerm));
            break;
    }
}

function Fill_GridPersonnel_Personnel_onPersonnelOperationCompleted(pageState, RetMessage) {
    switch (pageState) {
        case 'Add':
            var pageCount = parseInt(RetMessage[4]);
            SetPageIndex_GridPersonnel_Personnel(pageCount - 1);
            break;
        case 'Edit':
            SetPageIndex_GridPersonnel_Personnel(CurrentPageIndex_GridPersonnel_Personnel);
            break;
    }
    showDialog(RetMessage[0], RetMessage[1], RetMessage[2]);            
}

function GridPersonnel_Personnel_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridPersonnel_Personnel').innerHTML = '';
}

function CallBack_GridPersonnel_Personnel_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Personnel').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridPersonnel_Personnel(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);            
    }
    else 
        Changefooter_GridPersonnel_Personnel();
}

function Changefooter_GridPersonnel_Personnel() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridPersonnel_Personnel').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfPersonnelPageCount_Personnel').value) > 0 ? CurrentPageIndex_GridPersonnel_Personnel + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfPersonnelPageCount_Personnel').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridPersonnel_Personnel').innerHTML = retfooterVal;
    document.getElementById('lblPersonnelCount_Personnel').innerHTML = OriginalText_lblPersonnelCount_Personnel + ' ' + document.getElementById('hfPersonnelCount_Personnel').value;
}

function GetSexTitle_Personnel(sex) {
    var SexList = document.getElementById('hfSexList_Personnel').value.split('#');
    for (var i = 0; i < SexList.length; i++) {
        var sexObj = SexList[i].split(':');
        if (sexObj.length > 1) {
            if (sexObj[1] == sex.toString())
                return sexObj[0];
        }
    }
}

function GetMilitaryStatusTitle_Personnel(MilitaryStatus) {
    var MilitaryStatusList = document.getElementById('hfMilitaryStatusList_Personnel').value.split('#');
    for (var i = 0; i < MilitaryStatusList.length; i++) {
        var militaryStatusObj = MilitaryStatusList[i].split(':');
        if (militaryStatusObj.length > 1) {
            if (militaryStatusObj[1] == MilitaryStatus.toString())
                return militaryStatusObj[0];
        }
    }
}

function tlbItemRefresh_TlbPaging_GridPersonnel_Personnel_onClick() {
    Refresh_GridPersonnel_Personnel();
}

function tlbItemFirst_TlbPaging_GridPersonnel_Personnel_onClick() {
    SetPageIndex_GridPersonnel_Personnel(0);
}

function tlbItemBefore_TlbPaging_GridPersonnel_Personnel_onClick() {
    if (CurrentPageIndex_GridPersonnel_Personnel != 0) {
        CurrentPageIndex_GridPersonnel_Personnel = CurrentPageIndex_GridPersonnel_Personnel - 1;
        SetPageIndex_GridPersonnel_Personnel(CurrentPageIndex_GridPersonnel_Personnel);
    }
}

function tlbItemNext_TlbPaging_GridPersonnel_Personnel_onClick() {
    if (CurrentPageIndex_GridPersonnel_Personnel < parseInt(document.getElementById('hfPersonnelPageCount_Personnel').value) - 1) {
        CurrentPageIndex_GridPersonnel_Personnel = CurrentPageIndex_GridPersonnel_Personnel + 1;
        SetPageIndex_GridPersonnel_Personnel(CurrentPageIndex_GridPersonnel_Personnel);
    }
}

function tlbItemLast_TlbPaging_GridPersonnel_Personnel_onClick() {
    SetPageIndex_GridPersonnel_Personnel(parseInt(document.getElementById('hfPersonnelPageCount_Personnel').value) - 1);
}

function SetPageIndex_GridPersonnel_Personnel(pageIndex) {
    CurrentPageIndex_GridPersonnel_Personnel = pageIndex;
    Fill_GridPersonnel_Personnel(pageIndex);
}

function GetMaritalStatusTitle_Personnel(MaritalStatus) {
    var MaritalStatusList = document.getElementById('hfMaritalStatusList_Personnel').value.split('#');
    for (var i = 0; i < MaritalStatusList.length; i++) {
        var maritalStatusObj = MaritalStatusList[i].split(':');
        if (maritalStatusObj.length > 1) {
            if (maritalStatusObj[1] == MaritalStatus.toString())
                return maritalStatusObj[0];
        }
    }
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Personnel) {
        case 'Delete':
            DialogConfirm.Close();
            UpdatePersonnel_Personnel();
            break;
        case 'Exit':
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    CurrentPageState_Personnel = 'View';
}

function CharToKeyCode_Personnel(str) {
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


function UpdatePersonnel_Personnel() {
    ObjPersonnel_Personnel = new Object();
    ObjPersonnel_Personnel.ID = '0';
    var SelectedPersonnel_Personnel = GridPersonnel_Personnel.getSelectedItems();
    if (SelectedPersonnel_Personnel.length > 0)
        ObjPersonnel_Personnel.ID = SelectedPersonnel_Personnel[0].getMember('ID').get_text();
    UpdatePersonnel_PersonnelPage(CharToKeyCode_Personnel(CurrentPageState_Personnel), CharToKeyCode_Personnel(ObjPersonnel_Personnel.ID));
}

function UpdatePersonnel_PersonnelPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Personnel').value;
            Response[1] = document.getElementById('hfConnectionError_Personnel').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            Personnel_OnAfterUpdate(Response);
            CurrentPageState_Personnel = 'View';
        }
        else {
            if (CurrentPageState_Personnel == 'Delete')
                CurrentPageState_Personnel = 'View';
        }
    }
}

function mod(a, b) {
    return a - (b * Math.floor(a / b));
}

function Personnel_OnAfterUpdate(Response) {
    if (ObjPersonnel_Personnel != null) {
        GridPersonnel_Personnel.beginUpdate();
        switch (CurrentPageState_Personnel) {
            case 'Delete':
                GridPersonnel_Personnel.selectByKey(ObjPersonnel_Personnel.ID, 0, false);
                GridPersonnel_Personnel.deleteSelected();
                UpdateFeatures_GridPersonnel_Personnel();
                break;
        }
        GridPersonnel_Personnel.endUpdate();
    }
}

function UpdateFeatures_GridPersonnel_Personnel() {
    var personnelCount = parseInt(document.getElementById('hfPersonnelCount_Personnel').value);
    var PersonnelPageCount = parseInt(document.getElementById('hfPersonnelPageCount_Personnel').value);
    var PersonnelPageSize = parseInt(document.getElementById('hfPersonnelPageSize_Personnel').value);
    if (personnelCount > 0) {
        personnelCount = personnelCount - 1;
        var divRem = mod(personnelCount, PersonnelPageSize);
        if (divRem == 0) {
            PersonnelPageCount = PersonnelPageCount - 1;
            if (CurrentPageIndex_GridPersonnel_Personnel == PersonnelPageCount)
                CurrentPageIndex_GridPersonnel_Personnel = CurrentPageIndex_GridPersonnel_Personnel - 1 >= 0 ? CurrentPageIndex_GridPersonnel_Personnel - 1 : 0;
        }
        SetPageIndex_GridPersonnel_Personnel(CurrentPageIndex_GridPersonnel_Personnel);
        document.getElementById('hfPersonnelCount_Personnel').value = personnelCount.toString();
        document.getElementById('hfPersonnelPageCount_Personnel').value = PersonnelPageCount.toString();
        Changefooter_GridPersonnel_Personnel();
    }
}

function GetBoxesHeaders_Personnel() {
    document.getElementById('header_Personnel_Personnel').innerHTML = document.getElementById('hfheader_Personnel_Personnel').value;
    document.getElementById('footer_GridPersonnel_Personnel').innerHTML = document.getElementById('hffooter_GridPersonnel_Personnel').value;
    OriginalText_lblPersonnelCount_Personnel = document.getElementById('lblPersonnelCount_Personnel').innerHTML;
}

function SetActionMode_Personnel(state) {
    document.getElementById('ActionMode_Personnel').innerHTML = document.getElementById('hf' + state + '_Personnel').value;
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Personnel = confirmState;
    if (CurrentPageState_Personnel == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Personnel').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Personnel').value;
    DialogConfirm.Show();
}

function CallBack_GridPersonnel_Personnel_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridPersonnel_Personnel').innerHTML = '';
    ShowConnectionError_Personnel();
}

function ShowConnectionError_Personnel() {
    var error = document.getElementById('hfErrorType_Personnel').value;
    var errorBody = document.getElementById('hfConnectionError_Personnel').value;
    showDialog(error, errorBody, 'error');
}


function MasterPersonnelMainInformation_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_Personnel = SearchTerm;
    SetPageIndex_GridPersonnel_Personnel(0);
}

function tlbItemFormReconstruction_TlbPersonnel_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvPersonnelIntroduction_iFrame').src = 'MasterPersonnel.aspx';
}

function tlbItemHelp_TlbPersonnel_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPersonnel');
}




