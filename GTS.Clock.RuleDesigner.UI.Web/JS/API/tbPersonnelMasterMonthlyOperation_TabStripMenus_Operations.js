
function tlbItemExit_TlbPersonnelMasterMonthlyOperation() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PersonnelMasterMonthlyOperation').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    parent.CloseCurrentTabOnTabStripMenus();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function tlbItemGridSchema_TlbGridSchema_PersonnelMasterMonthlyOperation_onClick() {
    ShowDialogMonthlyOperationGridSchema();
}

function ShowDialogMonthlyOperationGridSchema() {
    var ObjMonthlyOperation_PersonnelMasterMonthlyOperation = new Object();
    ObjMonthlyOperation_PersonnelMasterMonthlyOperation.LoadState = 'Personnel';
    ObjMonthlyOperation_PersonnelMasterMonthlyOperation.PersonnelID = '-1';
    ObjMonthlyOperation_PersonnelMasterMonthlyOperation.PersonnelName = null;
    parent.DialogMonthlyOperationGridSchema.set_value(ObjMonthlyOperation_PersonnelMasterMonthlyOperation);
    parent.DialogMonthlyOperationGridSchema.Show();
}

function tlbItemGraphicalSchema_TlbGraphicalSchema_PersonnelMasterMonthlyOperation_onClick() {
    ShowDialogMonthlyOperationGanttChartSchema();
}

function ShowDialogMonthlyOperationGanttChartSchema() {
    var ObjMonthlyOperation_PersonnelMasterMonthlyOperation = new Object();
    ObjMonthlyOperation_PersonnelMasterMonthlyOperation.LoadState = 'Personnel';
    ObjMonthlyOperation_PersonnelMasterMonthlyOperation.PersonnelID = '-1';
    ObjMonthlyOperation_PersonnelMasterMonthlyOperation.PersonnelName = null;
    parent.DialogMonthlyOperationGanttChartSchema.set_value(ObjMonthlyOperation_PersonnelMasterMonthlyOperation);
    parent.DialogMonthlyOperationGanttChartSchema.Show();
}

function tlbItemFormReconstruction_TlbPersonnelMasterMonthlyOperation_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvPersonnelMasterMonthlyOperationReport_iFrame').src = 'PersonnelMasterMonthlyOperation.aspx';
}

function tlbItemHelp_TlbPersonnelMasterMonthlyOperation_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPersonnelMasterMonthlyOperation');
}
