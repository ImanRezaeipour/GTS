
var box_Conditions_ManagerMasterMonthlyOperation_IsShown = false;
var SettingsState_SummaryMonthlyOperation = 'View';
var CurrentPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation = 0;
var LoadState_ManagerMasterMonthlyOperation = 'Normal';
var DataCustomizeState_ManagerMasterMonthlyOperation = null;

function GridSchema_ManagerMasterMonthlyOperation_onMouseOver() {
    document.getElementById('imgGridSchema_ManagerMasterMonthlyOperation').src = 'Images/Ghadir/LargeGridSchema.png';
    document.getElementById('lblGridSchema_ManagerMasterMonthlyOperation').style.fontSize = 'large';
}

function GridSchema_ManagerMasterMonthlyOperation_onMouseOut() {
    document.getElementById('imgGridSchema_ManagerMasterMonthlyOperation').src = 'Images/Ghadir/GridSchema.png';
    document.getElementById('lblGridSchema_ManagerMasterMonthlyOperation').style.fontSize = 'small';
}

function GraphicalSchema_ManagerMasterMonthlyOperation_onMouseOver() {
    document.getElementById('imgGraphicalSchema_ManagerMasterMonthlyOperation').src = 'Images/Ghadir/LargeGraphicalSchema.png';
    document.getElementById('lblGraphicalSchema_ManagerMasterMonthlyOperation').style.fontSize = 'large';
}

function GraphicalSchema_ManagerMasterMonthlyOperation_onMouseOut() {
    document.getElementById('imgGraphicalSchema_ManagerMasterMonthlyOperation').src = 'Images/Ghadir/GraphicalSchema.png';
    document.getElementById('lblGraphicalSchema_ManagerMasterMonthlyOperation').style.fontSize = 'small';
}

function ChangeDirection_Mastertbl_ManagerMasterMonthlyOperationForm() {
    if (parent.CurrentLangID == 'en-US')
        document.getElementById('Mastertbl_ManagerMasterMonthlyOperationForm').dir = 'ltr';
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('Mastertbl_ManagerMasterMonthlyOperationForm').dir = 'rtl';
}

function GetBoxesHeaders_ManagerMasterMonthlyOperation() {
    document.getElementById('header_Conditions_ManagerMasterMonthlyOperation').innerHTML = document.getElementById('hfheader_Conditions_ManagerMasterMonthlyOperation').value;
    document.getElementById('header_trvDepartments_ManagerMasterMonthlyOperation').innerHTML = document.getElementById('hfheader_trvDepartments_ManagerMasterMonthlyOperation').value;
    document.getElementById('header_GridSummarySettings_ManagerMasterMonthlyOperation').innerHTML = document.getElementById('hfheader_GridSummarySettings_ManagerMasterMonthlyOperation').value;
    document.getElementById('header_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').innerHTML = document.getElementById('hfheader_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').value;
    document.getElementById('footer_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').innerHTML = document.getElementById('hffooter_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').value;
    var RetError_cmbMonth_ManagerMasterMonthlyOperation = document.getElementById('ErrorHiddenField_Months_ManagerMasterMonthlyOperation').value;
    if (RetError_cmbMonth_ManagerMasterMonthlyOperation != "")
        cmbMonth_ManagerMasterMonthlyOperation_onDataFillErrorOcured(RetError_cmbMonth_ManagerMasterMonthlyOperation);
}

function cmbMonth_ManagerMasterMonthlyOperation_onDataFillErrorOcured(RetError) {
    if (RetError != "") {
        var errorParts = eval('(' + RetError + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}

function imgbox_Conditions_ManagerMasterMonthlyOperation_onClick() {
    setSlideDownSpeed(200);
    slidedown_showHide('box_Conditions_ManagerMasterMonthlyOperation');

    if (box_Conditions_ManagerMasterMonthlyOperation_IsShown) {
        box_Conditions_ManagerMasterMonthlyOperation_IsShown = false;
        document.getElementById('imgbox_Conditions_ManagerMasterMonthlyOperation').src = 'Images/Ghadir/arrowDown.jpg';
    }
    else {
        box_Conditions_ManagerMasterMonthlyOperation_IsShown = true;
        document.getElementById('imgbox_Conditions_ManagerMasterMonthlyOperation').src = 'Images/Ghadir/arrowUp.jpg';
        Fill_ConditionControls_ManagerMasterMonthlyOperation();
    }
}

function SetHorizontalScrollingDirection_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_Opera() {
    if (navigator.userAgent.indexOf('Opera') != -1 && parent.CurrentLangID == "fa-IR")
        document.getElementById('GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').style.direction = "ltr";
}

function ShowDialogGridSammarySettings() {
    SettingsState_SummaryMonthlyOperation = 'View';
    CurrentSettingID_SummaryMonthlyOperation = '0';
    document.ManagerMasterMonthlyOperationForm.chbSelectAll_GridSummarySettings_ManagerMasterMonthlyOperation.checked = false;
    document.ManagerMasterMonthlyOperationForm.chbRecreateColumns_GridSummarySettings_ManagerMasterMonthlyOperation.checked = false;
    Fill_GridSummarySettings_ManagerMasterMonthlyOperation(CreateColumnsArray_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(false), '', 'Get', document.getElementById('chbRecreateColumns_GridSummarySettings_ManagerMasterMonthlyOperation').checked, CharToKeyCode_ManagerMasterMonthlyOperation(CurrentSettingID_SummaryMonthlyOperation));
    DialogGridSummarySettings.Show();
    CollapseControls_ManagerMasterMonthlyOperation();
}


function CreateColumnsArray_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(CheckColumnVisible) {
    var ArColumns = '';
    var ColumnsCol_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation = GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.get_table().get_columns();
    for (var i = 1; i < (ColumnsCol_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.length); i++) {
        if ((CheckColumnVisible && ColumnsCol_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation[i].get_visible()) || !CheckColumnVisible) {
            var Splitter = ":";
            if (i == ColumnsCol_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.length - 1)
                Splitter = '';
            ArColumns = ArColumns + CharToKeyCode_ManagerMasterMonthlyOperation(ColumnsCol_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation[i].get_dataField()) + "%" + CharToKeyCode_ManagerMasterMonthlyOperation(ColumnsCol_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation[i].get_headingText()) + Splitter;
        }
    }
    if (ArColumns.charAt(ArColumns.length - 1) == ':')
        ArColumns = ArColumns.substring(0, ArColumns.length - 1);
    return ArColumns;
}

function CharToKeyCode_ManagerMasterMonthlyOperation(str) {
    var OutStr = '';
    for (var i = 0; i < str.length; i++) {
        var KeyCode = str.charCodeAt(i);
        var CharKeyCode = '//' + KeyCode;
        OutStr += CharKeyCode;
    }
    return OutStr;
}

function GridSummarySettings_ManagerMasterMonthlyOperation_onSave() {
    SettingsState_SummaryMonthlyOperation = 'Save';
    CurrentSettingID_SummaryMonthlyOperation = document.getElementById('hfCurrentSettingID_GridSummarySettings_ManagerMasterMonthlyOperation').value;
    ChangeConditionUsing_ManagerMasterMonthlyOperation();
    Fill_GridSummarySettings_ManagerMasterMonthlyOperation(CreateColumnsArray_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(false), CreateColumnsArray_GridSummarySettings_ManagerMasterMonthlyOperation(), 'Set', document.getElementById('chbRecreateColumns_GridSummarySettings_ManagerMasterMonthlyOperation').checked, CharToKeyCode_ManagerMasterMonthlyOperation(CurrentSettingID_SummaryMonthlyOperation));
    SetHeader_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onChangeSettings();
}

function SetHeader_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onChangeSettings() {
    var header = null;
    if (document.getElementById('chbRecreateColumns_GridSummarySettings_ManagerMasterMonthlyOperation').checked)
        header = document.getElementById('hfColumnsRecreation_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').value;
    else
        header = document.getElementById('hfloadingPanel_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').value;
    document.getElementById('loadingPanel_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').innerHTML = header;
}

function Fill_GridSummarySettings_ManagerMasterMonthlyOperation(gridMonthlyOperationSummaryColumnsArray, gridSummarySettingsColumnsArray, state, IsRecreateColumns, currentSettingID) {
    CallBack_GridSummarySettings_ManagerMasterMonthlyOperation.callback(gridMonthlyOperationSummaryColumnsArray, state, gridSummarySettingsColumnsArray, IsRecreateColumns.toString(), currentSettingID);
}

function CallBack_GridSummarySettings_ManagerMasterMonthlyOperation_CallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_GridSummarySettings_ManagerMasterMonthlyOperation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        DialogGridSummarySettings.Close();
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else {
        if (SettingsState_SummaryMonthlyOperation == 'Save') {
            DialogGridSummarySettings.Close();
            SetPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(0);
        }
    }
}

function CreateColumnsArray_GridSummarySettings_ManagerMasterMonthlyOperation() {
    var ArColumns = '';
    for (var i = 0; i < GridSummarySettings_ManagerMasterMonthlyOperation.get_table().getRowCount(); i++) {
        var Splitter = ':';
        if (i == GridSummarySettings_ManagerMasterMonthlyOperation.get_table().getRowCount() - 1)
            Splitter = '';
        ArColumns = ArColumns + CharToKeyCode_ManagerMasterMonthlyOperation(GridSummarySettings_ManagerMasterMonthlyOperation.get_table().getRow(i).getMember('GridColumn').get_text()) + '%' + GridSummarySettings_ManagerMasterMonthlyOperation.get_table().getRow(i).getMember('ViewState').get_text() + Splitter;
    }
    return ArColumns;
}

function chbSelectAll_GridSummarySettings_ManagerMasterMonthlyOperation_onClick() {
    var Checked = null;
    if (document.ManagerMasterMonthlyOperationForm.chbSelectAll_GridSummarySettings_ManagerMasterMonthlyOperation.checked)
        Checked = true;
    else
        Checked = false;

    GridSummarySettings_ManagerMasterMonthlyOperation.beginUpdate();
    for (var i = 0; i < GridSummarySettings_ManagerMasterMonthlyOperation.get_table().getRowCount(); i++) {
        GridSummarySettings_ManagerMasterMonthlyOperation.get_table().getRow(i).setValue(2, Checked, false);
    }
    GridSummarySettings_ManagerMasterMonthlyOperation.endUpdate();
}

function chbFilter_ManagerMasterMonthlyOperation_onClick() {
    if (document.ManagerMasterMonthlyOperationForm.chbFilter_ManagerMasterMonthlyOperation.checked) {
        cmbFilterBy_ManagerMasterMonthlyOperation.enable();
        document.getElementById('txtFilterTerm_ManagerMasterMonthlyOperation').disabled = '';
    }
    else {
        cmbFilterBy_ManagerMasterMonthlyOperation.disable();
        document.getElementById('txtFilterTerm_ManagerMasterMonthlyOperation').disabled = 'disabled';
        document.getElementById('txtFilterTerm_ManagerMasterMonthlyOperation').value = '';
    }
}

function Fill_ConditionControls_ManagerMasterMonthlyOperation() {
    var ArColumns = CreateColumnsArray_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(true);
    var selectedIndex_cmbFilterBy_ManagerMasterMonthlyOperation = '-1';
    var selectedIndex_cmbSortBy_ManagerMasterMonthlyOperation = '-1';
    if (LoadState_ManagerMasterMonthlyOperation == 'Search') {
        if (cmbFilterBy_ManagerMasterMonthlyOperation.getSelectedItem() != undefined)
            selectedIndex_cmbFilterBy_ManagerMasterMonthlyOperation = cmbFilterBy_ManagerMasterMonthlyOperation.get_selectedIndex();
    }
    if (document.getElementById('chbSort_ManagerMasterMonthlyOperation').checked) {
        if (cmbSortBy_ManagerMasterMonthlyOperation.getSelectedItem() != undefined)
            selectedIndex_cmbSortBy_ManagerMasterMonthlyOperation = cmbSortBy_ManagerMasterMonthlyOperation.get_selectedIndex();
    }
    if (cmbFilterBy_ManagerMasterMonthlyOperation.get_itemCount() == 0)
        CallBack_cmbFilterBy_ManagerMasterMonthlyOperation.callback(ArColumns, CharToKeyCode_ManagerMasterMonthlyOperation(selectedIndex_cmbFilterBy_ManagerMasterMonthlyOperation.toString()));
    if (cmbSortBy_ManagerMasterMonthlyOperation.get_itemCount() == 0)
        CallBack_cmbSortBy_ManagerMasterMonthlyOperation.callback(ArColumns, CharToKeyCode_ManagerMasterMonthlyOperation(selectedIndex_cmbSortBy_ManagerMasterMonthlyOperation.toString()));
}

function chbSort_ManagerMasterMonthlyOperation_onClick() {
    if (document.ManagerMasterMonthlyOperationForm.chbSort_ManagerMasterMonthlyOperation.checked) {
        cmbSortBy_ManagerMasterMonthlyOperation.enable();
        cmbSortDirection_ManagerMasterMonthlyOperation.enable();
    }
    else {
        cmbSortBy_ManagerMasterMonthlyOperation.disable();
        cmbSortDirection_ManagerMasterMonthlyOperation.disable();
        document.getElementById('cmbSortDirection_ManagerMasterMonthlyOperation_Input').value = '';
    }
}

function ChangeConditionUsing_ManagerMasterMonthlyOperation() {
    document.ManagerMasterMonthlyOperationForm.chbFilter_ManagerMasterMonthlyOperation.checked = false;
    chbFilter_ManagerMasterMonthlyOperation_onClick();
    document.ManagerMasterMonthlyOperationForm.chbSort_ManagerMasterMonthlyOperation.checked = false;
    chbSort_ManagerMasterMonthlyOperation_onClick();
    if (box_Conditions_ManagerMasterMonthlyOperation_IsShown)
        imgbox_Conditions_ManagerMasterMonthlyOperation_onClick();
}

function SetPosition_DropDownDives_ManagerMasterMonthlyOperation() {
    imgbox_Conditions_ManagerMasterMonthlyOperation_onClick
    switch (parent.CurrentLangID) {
        case "fa-IR":
            document.getElementById('box_Conditions_ManagerMasterMonthlyOperation').style.right = '260px';
            break;
        case "en-US":
            document.getElementById('box_Conditions_ManagerMasterMonthlyOperation').style.left = '260px';
            break;
    }
}

function tlbItemOk_TlbOkConfirm_onClick() {
    parent.CloseCurrentTabOnTabStripMenus();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function tlbItemGridSettings_TlbManagerMasterMonthlyOperation_onClick() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_ManagerMasterMonthlyOperation').value;
    ShowDialogGridSammarySettings();
}

function tlbItemExit_TlbManagerMasterMonthlyOperation() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_ManagerMasterMonthlyOperation').value;
    DialogConfirm.Show();
    CollapseControls_ManagerMasterMonthlyOperation();
}

function tlbItemApplyConditions_TlbApplyConditions_ManagerMasterMonthlyOperation_onClick() {
    DataCustomizeState_ManagerMasterMonthlyOperation = 'FilterAndSort';
    SetPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(0);
    imgbox_Conditions_ManagerMasterMonthlyOperation_onClick();
}

function tlbItemRefresh_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onClick() {
    Refresh_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation();
}

function Refresh_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation() {
    ChangeConditionUsing_ManagerMasterMonthlyOperation();
    LoadState_ManagerMasterMonthlyOperation = 'Normal';
    DataCustomizeState_ManagerMasterMonthlyOperation = null;
    SetPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(0);
}

function tlbItemFirst_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onClick() {
    SetPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(0);
}

function tlbItemBefore_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onClick() {
    if (CurrentPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation != 0) {
        CurrentPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation = CurrentPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation - 1;
        SetPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(CurrentPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation);
    }
}

function tlbItemNext_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onClick() {
    if (CurrentPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation < parseInt(document.getElementById('hfMonthlyOperationSummaryPageCount_ManagerMasterMonthlyOperation').value) - 1) {
        CurrentPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation = CurrentPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation + 1;
        SetPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(CurrentPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation);
    }
}

function tlbItemLast_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onClick() {
    SetPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(parseInt(document.getElementById('hfMonthlyOperationSummaryPageCount_ManagerMasterMonthlyOperation').value) - 1);
}

function SetPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(pageIndex) {
    CurrentPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation = pageIndex;
    Fill_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(pageIndex);
}

function Fill_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(pageIndex) {
    document.getElementById('loadingPanel_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').innerHTML = document.getElementById('hfloadingPanel_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').value;
    var departmentID = '0';
    if (trvDepartments_ManagerMasterMonthlyOperation.get_selectedNode() != undefined)
        var departmentID = trvDepartments_ManagerMasterMonthlyOperation.get_selectedNode().get_id();
    var month = document.getElementById('hfCurrentMonth_ManagerMasterMonthlyOperation').value;
    var pageSize = parseInt(document.getElementById('hfMonthlyOperationSummaryPageSize_ManagerMasterMonthlyOperation').value);
    var orderBy = 'NONE';
    var orderByDirection = 'desc';
    var searchKey = 'NONE';
    var searchTerm = '';
    switch (DataCustomizeState_ManagerMasterMonthlyOperation) {
        case 'FilterAndSort':
            if (document.getElementById('chbSort_ManagerMasterMonthlyOperation').checked) {
                var selectedItem_cmbSortBy_ManagerMasterMonthlyOperation = cmbSortBy_ManagerMasterMonthlyOperation.getSelectedItem();
                if (selectedItem_cmbSortBy_ManagerMasterMonthlyOperation != undefined)
                    orderBy = selectedItem_cmbSortBy_ManagerMasterMonthlyOperation.get_value();
                var selectedItem_cmbSortDirection_ManagerMasterMonthlyOperation = cmbSortDirection_ManagerMasterMonthlyOperation.getSelectedItem();
                if (selectedItem_cmbSortDirection_ManagerMasterMonthlyOperation != undefined)
                    orderByDirection = selectedItem_cmbSortDirection_ManagerMasterMonthlyOperation.get_value();
            }
            if (document.getElementById('chbFilter_ManagerMasterMonthlyOperation').checked) {
                LoadState_ManagerMasterMonthlyOperation = 'Search';
                selectedItem_cmbFilterBy_ManagerMasterMonthlyOperation = cmbFilterBy_ManagerMasterMonthlyOperation.getSelectedItem();
                if (selectedItem_cmbFilterBy_ManagerMasterMonthlyOperation != undefined)
                    searchKey = selectedItem_cmbFilterBy_ManagerMasterMonthlyOperation.get_value();
                searchTerm = document.getElementById('txtFilterTerm_ManagerMasterMonthlyOperation').value;
            }
            break;
        case 'QuickSearch':
            searchTerm = document.getElementById('txtSerchTerm_ManagerMasterMonthlyOperation').value;
            break;
    }
    CallBack_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.callback(CharToKeyCode_ManagerMasterMonthlyOperation(LoadState_ManagerMasterMonthlyOperation), CharToKeyCode_ManagerMasterMonthlyOperation(departmentID), CharToKeyCode_ManagerMasterMonthlyOperation(pageSize.toString()), CharToKeyCode_ManagerMasterMonthlyOperation(pageIndex.toString()), CharToKeyCode_ManagerMasterMonthlyOperation(orderBy), CharToKeyCode_ManagerMasterMonthlyOperation(orderByDirection), CharToKeyCode_ManagerMasterMonthlyOperation(searchKey), CharToKeyCode_ManagerMasterMonthlyOperation(searchTerm), CharToKeyCode_ManagerMasterMonthlyOperation(month));
}

function Refresh_trvDepartments_ManagerMasterMonthlyOperation() {
    Fill_trvDepartments_ManagerMasterMonthlyOperation();
}

function Fill_trvDepartments_ManagerMasterMonthlyOperation() {
    document.getElementById('loadingPanel_trvDepartments_ManagerMasterMonthlyOperation').innerHTML = document.getElementById('hfloadingPanel_trvDepartments_ManagerMasterMonthlyOperation').value;
    CallBack_trvDepartments_ManagerMasterMonthlyOperation.callback();
}

function trvDepartments_ManagerMasterMonthlyOperation_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvDepartments_ManagerMasterMonthlyOperation').innerHTML = '';
}

function CallBack_trvDepartments_ManagerMasterMonthlyOperation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Departments_ManagerMasterMonthlyOperation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvDepartments_ManagerMasterMonthlyOperation();
    }
    else {
        try {
            if (GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.get_recordCount() > 0)
                Refresh_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation();
        } catch (e) {
        }
    }
}

function CallBack_cmbFilterBy_ManagerMasterMonthlyOperation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_FilterBy_ManagerMasterMonthlyOperation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}

function Changefooter_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfMonthlyOperationSummaryPageCount_ManagerMasterMonthlyOperation').value) > 0 ? CurrentPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfMonthlyOperationSummaryPageCount_ManagerMasterMonthlyOperation').value;
        if ((i == 1 || i == 3) && GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.get_table().getRowCount() == 0)
            footerValCol[i] = 0;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').innerHTML = retfooterVal;
}


function CallBack_cmbSortBy_ManagerMasterMonthlyOperation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_SortBy_ManagerMasterMonthlyOperation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}

function GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').innerHTML = '';
}

function CallBack_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_CallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MonthlyOperationSummary').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(0);
    }
    else
        if ((trvDepartments_ManagerMasterMonthlyOperation.get_selectedNode() != undefined && LoadState_ManagerMasterMonthlyOperation == 'Normal') || (LoadState_ManagerMasterMonthlyOperation == 'Search' && GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.get_recordCount() > 0))
            Changefooter_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation();
        else
            document.getElementById('footer_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').innerHTML = document.getElementById('hffooter_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').value;
}

function trvDepartments_ManagerMasterMonthlyOperation_onNodeSelect(sender, e) {
    Refresh_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation();
}

function cmbMonth_ManagerMasterMonthlyOperation_onChange(sender, e) {
    document.getElementById('hfCurrentMonth_ManagerMasterMonthlyOperation').value = cmbMonth_ManagerMasterMonthlyOperation.getSelectedItem().get_value();
}

function tlbItemGridSchema_TlbGridSchema_ManagerMasterMonthlyOperation_onClick() {
    ShowDialogMonthlyOperationGridSchema();
}

function ShowDialogMonthlyOperationGridSchema() {
    var selectedItems_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation = GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.getSelectedItems();
    if (selectedItems_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.length > 0) {
        var ObjMonthlyOperation_ManagerMasterMonthlyOperation = new Object();
        ObjMonthlyOperation_ManagerMasterMonthlyOperation.LoadState = 'Manager';
        ObjMonthlyOperation_ManagerMasterMonthlyOperation.PersonnelID = selectedItems_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation[0].getMember('PersonId').get_text();
        ObjMonthlyOperation_ManagerMasterMonthlyOperation.PersonnelName = selectedItems_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation[0].getMember('PersonName').get_text();
        parent.DialogMonthlyOperationGridSchema.set_value(ObjMonthlyOperation_ManagerMasterMonthlyOperation);
        parent.DialogMonthlyOperationGridSchema.Show();
    }
}

function tlbItemGraphicalSchema_TlbGraphicalSchema_ManagerMasterMonthlyOperation_onClick() {
    ShowDialogMonthlyOperationGanttChartSchema();
}

function ShowDialogMonthlyOperationGanttChartSchema() {
    var selectedItems_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation = GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.getSelectedItems();
    if (selectedItems_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.length > 0) {
        var ObjMonthlyOperation_ManagerMasterMonthlyOperation = new Object();
        ObjMonthlyOperation_ManagerMasterMonthlyOperation.LoadState = 'Manager';
        ObjMonthlyOperation_ManagerMasterMonthlyOperation.PersonnelID = selectedItems_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation[0].getMember('PersonId').get_text();
        ObjMonthlyOperation_ManagerMasterMonthlyOperation.PersonnelName = selectedItems_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation[0].getMember('PersonName').get_text();
        parent.DialogMonthlyOperationGanttChartSchema.set_value(ObjMonthlyOperation_ManagerMasterMonthlyOperation);
        parent.DialogMonthlyOperationGanttChartSchema.Show();
    }
}

function CallBack_cmbFilterBy_ManagerMasterMonthlyOperation_onCallbackError(sender, e) {
    ShowConnectionError_ManagerMasterMonthlyOperation();
}

function CallBack_cmbSortBy_ManagerMasterMonthlyOperation_onCallbackError(sender, e) {
    ShowConnectionError_ManagerMasterMonthlyOperation();
}

function CallBack_trvDepartments_ManagerMasterMonthlyOperation_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvDepartments_ManagerMasterMonthlyOperation').innerHTML = '';
    ShowConnectionError_ManagerMasterMonthlyOperation();
}

function CallBack_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation').innerHTML = '';
    ShowConnectionError_ManagerMasterMonthlyOperation();
}

function CallBack_GridSummarySettings_ManagerMasterMonthlyOperation_onCallbackError(sender, e) {
    ShowConnectionError_ManagerMasterMonthlyOperation();
}

function ShowConnectionError_ManagerMasterMonthlyOperation() {
    var error = document.getElementById('hfErrorType_ManagerMasterMonthlyOperation').value;
    var errorBody = document.getElementById('hfConnectionError_ManagerMasterMonthlyOperation').value;
    showDialog(error, errorBody, 'error');
}

function CollapseControls_ManagerMasterMonthlyOperation() {
    cmbMonth_ManagerMasterMonthlyOperation.collapse();
    cmbFilterBy_ManagerMasterMonthlyOperation.collapse();
    cmbSortBy_ManagerMasterMonthlyOperation.collapse();
    cmbSortDirection_ManagerMasterMonthlyOperation.collapse();
}

function tlbItemFormReconstruction_TlbManagerMasterMonthlyOperation_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvManagerMasterMonthlyOperationReport_iFrame').src = 'ManagerMasterMonthlyOperation.aspx';
}

function tlbItemHelp_TlbManagerMasterMonthlyOperation_onClick() {
    LoadHelpPage('tlbItemHelp_TlbManagerMasterMonthlyOperation');
}

function tlbItemSearch_TlbManagerMasterMonthlyOperationQuickSearch_onClick() {
    DataCustomizeState_ManagerMasterMonthlyOperation = 'QuickSearch';
    LoadState_ManagerMasterMonthlyOperation = 'Search';
    SetPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(0);
}

function CallBack_cmbFilterBy_ManagerMasterMonthlyOperation_onBeforeCallback(sender, e) {
    cmbFilterBy_ManagerMasterMonthlyOperation.dispose();
}

function CallBack_cmbSortBy_ManagerMasterMonthlyOperation_onBeforeCallback(sender, e) {
    cmbSortBy_ManagerMasterMonthlyOperation.dispose();
}




















