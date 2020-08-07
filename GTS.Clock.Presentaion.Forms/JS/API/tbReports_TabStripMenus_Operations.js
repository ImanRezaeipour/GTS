
var CurrentPageState_Reports = 'View';
var ConfirmState_Reports = null;
var ObjReport_Reports = null;
var CurrentPageMode_Reports = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var SelectedReportFile_Reports = null;

function GetBoxesHeaders_Reports() {
    document.getElementById('header_Reports_Reports').innerHTML = document.getElementById('hfheader_Reports_Reports').value;
    document.getElementById('header_ReportDetails_Reports').innerHTML = document.getElementById('hfheader_ReportDetails_Reports').value;
    document.getElementById('header_ReportGroupDetails_Reports').innerHTML = document.getElementById('hfheader_ReportGroupDetails_Reports').value;
    document.getElementById('cmbReportFiles_Reports_Input').value = document.getElementById('hfcmbAlarm_Reports').value;
    document.getElementById('clmnFileName_cmbReportFiles_Reports').innerHTML = document.getElementById('hfclmnFileName_cmbReportFiles_Reports').value;
    document.getElementById('clmnDescription_cmbReportFiles_Reports').innerHTML = document.getElementById('hfclmnDescription_cmbReportFiles_Reports').value;
}

function tlbItemNewGroup_TlbReports_onClick() {
    if (!CheckIsReport_Reports()) {
        CurrentPageMode_Reports = 'ReportGroup';
        ChangePageState_Reports('Add');
        ClearList_Reports();
    }
}

function tlbItemNewReport_TlbReports_onClick() {
    if (!CheckIsReport_Reports()) {
        CurrentPageMode_Reports = 'Report';
        ChangePageState_Reports('Add');
        ClearList_Reports();
    }
}

function CheckIsReport_Reports() {
    var isReport = false;
    var selectedNode_trvReports_Reports = trvReports_Reports.get_selectedNode();
    if (selectedNode_trvReports_Reports != undefined) {
        var TargetType = selectedNode_trvReports_Reports.get_value();
        TargetType = eval('(' + TargetType + ')').TargetType;
        switch (TargetType) {
            case 'Report':
                isReport = true;
                break;
            case 'ReportGroup':
                break;
        }
    }
    return isReport;
}

function tlbItemEdit_TlbReports_onClick() {
    ChangePageState_Reports('Edit');
}

function tlbItemDelete_TlbReports_onClick() {
    ChangePageState_Reports('Delete');
}

function tlbItemSave_TlbReports_onClick() {
    Report_onSave();
}

function tlbItemCancel_TlbReports_onClick() {
    DialogConfirm.Close();
    ChangePageState_Reports('View');
}

function tlbItemExit_TlbReports_onClick() {
    ShowDialogConfirm('Exit');
}

function Refresh_trvReports_Reports() {
    Fill_trvReports_Reports();
}

function trvReports_Reports_onNodeSelect(sender, e) {
    if (CurrentPageState_Reports != 'Add')
        NavigateReport_Reports(e.get_node());
    else {
        var TargetType = eval('('+e.get_node().get_value()+')').TargetType;
        switch (TargetType) {
            case 'Report':
                ChangePageState_Reports('View');
                break;
            case 'ReportGroup':
                break;
        }
    }
    CheckType_Reports(e.get_node());
}

function trvReports_Reports_NodeMouseDoubleClick(sender, e) {
    var trvNodeVal = eval('(' + e.get_node().get_value() + ')');
    var TargetType = trvNodeVal.TargetType;
    switch (TargetType) {
        case 'Report':
            if (trvNodeVal.HasParameter) {
                if (TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports') != null)
                    ShowDialogReportParameters_Reports();
            }
            else
                if (TlbReports.get_items().getItemById('tlbItemReportView_TlbReports') != null)
                    GetReport_Reports();
            break;
        case 'ReportGroup':
            break;
    }
}

function CheckType_Reports(trvNode) {
    var trvNodeVal = eval('(' + trvNode.get_value() + ')');
    switch (trvNodeVal.TargetType) {
        case 'Report':
            var IsReportHasParameter = trvNodeVal.HasParameter;
            if (IsReportHasParameter) {
                if (TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports') != null) {
                    TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports').set_enabled(true);
                    TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports').set_imageUrl('regulation.png');
                }
                if (TlbReports.get_items().getItemById('tlbItemReportView_TlbReports') != null) {
                    TlbReports.get_items().getItemById('tlbItemReportView_TlbReports').set_enabled(false);
                    TlbReports.get_items().getItemById('tlbItemReportView_TlbReports').set_imageUrl('Report_silver.png');
                }
            }
            else {
                if (TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports') != null) {
                    TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports').set_enabled(false);
                    TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports').set_imageUrl('regulation_silver.png');
                }
                if (TlbReports.get_items().getItemById('tlbItemReportView_TlbReports') != null) {
                    TlbReports.get_items().getItemById('tlbItemReportView_TlbReports').set_enabled(true);
                    TlbReports.get_items().getItemById('tlbItemReportView_TlbReports').set_imageUrl('Report.png');
                }
            }
            break;
        case 'ReportGroup':
            if (TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports') != null) {
                TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports').set_enabled(false);
                TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports').set_imageUrl('regulation_silver.png');
            }
            if (TlbReports.get_items().getItemById('tlbItemReportView_TlbReports') != null) {
                TlbReports.get_items().getItemById('tlbItemReportView_TlbReports').set_enabled(false);
                TlbReports.get_items().getItemById('tlbItemReportView_TlbReports').set_imageUrl('Report_silver.png');
            }
            break;
    }
}

function trvReports_Reports_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvReports_Reports').innerHTML = '';
}

function CallBack_trvReports_Reports_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Reports').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvReports_Reports();
    }
}

function CallBack_trvReports_Reports_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvReports_Reports').innerHTML = '';
    ShowConnectionError_Reports();
}

function cmbReportFiles_Reports_onExpand(sender, e) {
    if (cmbReportFiles_Reports.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbReportFiles_Reports == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbReportFiles_Reports = true;
        CallBack_cmbReportFiles_Reports.callback();
    }
}

function cmbReportFiles_Reports_onCollapse(sender, e) {
    if (cmbReportFiles_Reports.getSelectedItem() == undefined || SelectedReportFile_Reports == null)
        document.getElementById('cmbReportFiles_Reports_Input').value = document.getElementById('hfcmbAlarm_Reports').value;
    else {
        if (cmbReportFiles_Reports.getSelectedItem() != undefined && SelectedReportFile_Reports != null)
            document.getElementById('cmbReportFiles_Reports_Input').value = SelectedReportFile_Reports.Description;
    }

}

function CallBack_cmbReportFiles_Reports_onBeforeCallback(sender, e) {
    cmbReportFiles_Reports.dispose();
}

function CallBack_cmbReportFiles_Reports_onCallbackComplete(sender, e) {
    document.getElementById('clmnFileName_cmbReportFiles_Reports').innerHTML = document.getElementById('hfclmnFileName_cmbReportFiles_Reports').value;
    document.getElementById('clmnDescription_cmbReportFiles_Reports').innerHTML = document.getElementById('hfclmnDescription_cmbReportFiles_Reports').value;

    var error = document.getElementById('ErrorHiddenField_ReportFiles_Reports').value;
    if (error == "") {
        document.getElementById('cmbReportFiles_Reports_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbReportFiles_Reports_DropImage').mousedown();
        else
            cmbReportFiles_Reports.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbReportFiles_Reports_DropDown').style.display = 'none';
    }
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Reports) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateReport_Reports();
            break;
        case 'Exit':
            ClearList_Reports();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Reports('View');
}

function ChangePageState_Reports(state) {
    CurrentPageState_Reports = state;
    SetActionMode_Reports(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbReports.get_items().getItemById('tlbItemNewGroup_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemNewGroup_TlbReports').set_enabled(false);
            TlbReports.get_items().getItemById('tlbItemNewGroup_TlbReports').set_imageUrl('group_silver.png');
        }
        if (TlbReports.get_items().getItemById('tlbItemNewReport_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemNewReport_TlbReports').set_enabled(false);
            TlbReports.get_items().getItemById('tlbItemNewReport_TlbReports').set_imageUrl('newReport_silver.png');
        }
        if (TlbReports.get_items().getItemById('tlbItemEdit_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemEdit_TlbReports').set_enabled(false);
            TlbReports.get_items().getItemById('tlbItemEdit_TlbReports').set_imageUrl('edit_silver.png');
        }
        if (TlbReports.get_items().getItemById('tlbItemDelete_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemDelete_TlbReports').set_enabled(false);
            TlbReports.get_items().getItemById('tlbItemDelete_TlbReports').set_imageUrl('remove_silver.png');
        }
        TlbReports.get_items().getItemById('tlbItemSave_TlbReports').set_enabled(true);
        TlbReports.get_items().getItemById('tlbItemSave_TlbReports').set_imageUrl('save.png');
        TlbReports.get_items().getItemById('tlbItemCancel_TlbReports').set_enabled(true);
        TlbReports.get_items().getItemById('tlbItemCancel_TlbReports').set_imageUrl('cancel.png');
        switch (CurrentPageMode_Reports) {
            case 'ReportGroup':
                document.getElementById('txtReportGroupName_Reports').disabled = '';
                break;
            case 'Report':
                document.getElementById('txtReportName_Reports').disabled = '';
                cmbReportFiles_Reports.enable();
                break;
        }
        if (state == 'Edit')
            NavigateReport_Reports(trvReports_Reports.get_selectedNode());
        if (state == 'Delete')
            Report_onSave();
    }
    if (state == 'View') {
        if (TlbReports.get_items().getItemById('tlbItemNewGroup_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemNewGroup_TlbReports').set_enabled(true);
            TlbReports.get_items().getItemById('tlbItemNewGroup_TlbReports').set_imageUrl('group.png');
        }
        if (TlbReports.get_items().getItemById('tlbItemNewReport_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemNewReport_TlbReports').set_enabled(true);
            TlbReports.get_items().getItemById('tlbItemNewReport_TlbReports').set_imageUrl('newReport.png');
        }
        if (TlbReports.get_items().getItemById('tlbItemEdit_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemEdit_TlbReports').set_enabled(true);
            TlbReports.get_items().getItemById('tlbItemEdit_TlbReports').set_imageUrl('edit.png');
        }
        if (TlbReports.get_items().getItemById('tlbItemDelete_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemDelete_TlbReports').set_enabled(true);
            TlbReports.get_items().getItemById('tlbItemDelete_TlbReports').set_imageUrl('remove.png');
        }
        TlbReports.get_items().getItemById('tlbItemSave_TlbReports').set_enabled(false);
        TlbReports.get_items().getItemById('tlbItemSave_TlbReports').set_imageUrl('save_silver.png');
        TlbReports.get_items().getItemById('tlbItemCancel_TlbReports').set_enabled(false);
        TlbReports.get_items().getItemById('tlbItemCancel_TlbReports').set_imageUrl('cancel_silver.png');
        document.getElementById('txtReportGroupName_Reports').disabled = 'disabled';
        document.getElementById('txtReportName_Reports').disabled = 'disabled';
        cmbReportFiles_Reports.disable();
    }
}

function SetActionMode_Reports(state) {    
    document.getElementById('ActionMode_Reports').innerHTML = document.getElementById('hf' + state + '_Reports').value + (CurrentPageState_Reports != 'View' ? ' ' + document.getElementById('hf' + CurrentPageMode_Reports + '_Reports').value : '');
}

function NavigateReport_Reports(selectedReportNode) {
    if (selectedReportNode != undefined) {
        ObjTarget_Reports = selectedReportNode.get_value();
        ObjTarget_Reports = eval('(' + ObjTarget_Reports + ')');
        CurrentPageMode_Reports = ObjTarget_Reports.TargetType;
        switch (CurrentPageMode_Reports) {
            case 'ReportGroup':
                document.getElementById('txtReportGroupName_Reports').value = selectedReportNode.get_text();
                break;
            case 'Report':
                SelectedReportFile_Reports = new Object();
                SelectedReportFile_Reports.ID = selectedReportNode.get_id();
                document.getElementById('txtReportName_Reports').value = selectedReportNode.get_text();
                document.getElementById('cmbReportFiles_Reports_Input').value = SelectedReportFile_Reports.Description = ObjTarget_Reports.Description;
                SelectedReportFile_Reports.FileID = ObjTarget_Reports.FileID;
                SelectedReportFile_Reports.FileName = ObjTarget_Reports.FileName;
                break;
        }
    }
}

function Report_onSave() {
    if (CurrentPageState_Reports != 'Delete')
        UpdateReport_Reports();
    else
        ShowDialogConfirm('Delete');
}

function UpdateReport_Reports() {
    ObjReport_Reports = new Object();
    ObjReport_Reports.TargetType = null;
    ObjReport_Reports.ParentID = '0';
    ObjReport_Reports.SelectedID = '0';
    ObjReport_Reports.Name = null;
    ObjReport_Reports.ReportFileID = '0';
    ObjReport_Reports.ReportFileName = null;
    ObjReport_Reports.ReportFileDescription = null;

    ObjReport_Reports.TargetType = CurrentPageMode_Reports;
    var SelectedReportNode_Reports = trvReports_Reports.get_selectedNode();
    if (SelectedReportNode_Reports != undefined) {
        ObjReport_Reports.SelectedID = SelectedReportNode_Reports.get_id();
        if (SelectedReportNode_Reports.get_parentNode() != undefined)
            ObjReport_Reports.ParentID = SelectedReportNode_Reports.get_parentNode().get_id();
    }
    if (CurrentPageState_Reports != 'Delete') {
        switch (CurrentPageMode_Reports) {
            case 'ReportGroup':
                ObjReport_Reports.Name = document.getElementById('txtReportGroupName_Reports').value;
                break;
            case 'Report':
                ObjReport_Reports.Name = document.getElementById('txtReportName_Reports').value;
                var SelectedItem_cmbReportFiles_Reports = cmbReportFiles_Reports.getSelectedItem();
                if (SelectedItem_cmbReportFiles_Reports != undefined) {
                    ObjReport_Reports.ReportFileID = SelectedItem_cmbReportFiles_Reports.get_value();
                    ObjReport_Reports.ReportFileName = SelectedItem_cmbReportFiles_Reports.Name;
                    ObjReport_Reports.ReportFileDescription = SelectedItem_cmbReportFiles_Reports.get_text();
                }
                else {
                    if (SelectedReportFile_Reports != null) {
                        ObjReport_Reports.ReportFileID = SelectedReportFile_Reports.FileID;
                        ObjReport_Reports.ReportFileName = SelectedReportFile_Reports.FileName;
                        ObjReport_Reports.ReportFileDescription = SelectedReportFile_Reports.Description;
                    }
                }
                break;
        }
    }
    UpdateReport_ReportsPage(CharToKeyCode_Reports(CurrentPageState_Reports), CharToKeyCode_Reports(ObjReport_Reports.TargetType), CharToKeyCode_Reports(ObjReport_Reports.ParentID), CharToKeyCode_Reports(ObjReport_Reports.SelectedID), CharToKeyCode_Reports(ObjReport_Reports.Name), CharToKeyCode_Reports(ObjReport_Reports.ReportFileID));
    DialogWaiting.Show();
}

function UpdateReport_ReportsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Reports').value;
            Response[1] = document.getElementById('hfConnectionError_Reports').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_Reports();
            Report_OnAfterUpdate(Response);
            ChangePageState_Reports('View');
        }
        else {
            if (CurrentPageState_Reports == 'Delete')
                ChangePageState_Reports('View');
        }
    }
}

function Report_OnAfterUpdate(Response) {
    var ReportNodeText = ObjReport_Reports.Name;
    var ReportNodeValue = '{"TargetType":"' + ObjReport_Reports.TargetType + '","FileID":"' + ObjReport_Reports.ReportFileID + '","FileName":"' + ObjReport_Reports.ReportFileName + '","Description":"' + ObjReport_Reports.ReportFileDescription + '"}';

    trvReports_Reports.beginUpdate();
    switch (CurrentPageState_Reports) {
        case 'Add':
            var newReportNode = new ComponentArt.Web.UI.TreeViewNode();
            newReportNode.set_text(ReportNodeText);
            newReportNode.set_value(ReportNodeValue);
            newReportNode.set_id(Response[3]);
            var imageName = null;
            switch (ObjReport_Reports.TargetType) {
                case 'ReportGroup':
                    imageName = 'group.png';
                    break;
                case 'Report':
                    imageName = 'report.png';
                    break;
            }
            newReportNode.set_imageUrl('Images/TreeView/' + imageName);
            trvReports_Reports.findNodeById(ObjReport_Reports.SelectedID).get_nodes().add(newReportNode);
            trvReports_Reports.selectNodeById(ObjReport_Reports.SelectedID);
            break;
        case 'Edit':
            var selectedReportNode = trvReports_Reports.findNodeById(Response[3]);
            selectedReportNode.set_text(ReportNodeText);
            selectedReportNode.set_value(ReportNodeValue);
            trvReports_Reports.selectNodeById(Response[3]);
            break;
        case 'Delete':
            trvReports_Reports.findNodeById(ObjReport_Reports.SelectedID).remove();
            break;
    }
    trvReports_Reports.endUpdate();
    if (CurrentPageState_Reports == 'Add')
        trvReports_Reports.get_selectedNode().expand();
}

function CharToKeyCode_Reports(str) {
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

function ShowDialogConfirm(confirmState) {
    ConfirmState_Reports = confirmState;
    if (CurrentPageState_Reports == 'Delete') {
        var confirmMessage = null;
        switch (CurrentPageMode_Reports) {
            case 'ReportGroup':
                confirmMessage = document.getElementById('hfReportGroupDeleteMessage_Reports').value;
                break;
            case 'Report':
                confirmMessage = document.getElementById('hfReportDeleteMessage_Reports').value;
                break;
        }
        document.getElementById('lblConfirm').innerHTML = confirmMessage;
    }
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Reports').value;
    DialogConfirm.Show();
    CollapseControls_Reports();
}

function ClearList_Reports() {
    document.getElementById('txtReportGroupName_Reports').value = '';
    document.getElementById('txtReportName_Reports').value = '';
    document.getElementById('cmbReportFiles_Reports_Input').value = document.getElementById('hfcmbAlarm_Reports').value;
    cmbReportFiles_Reports.unSelect();
}

function Fill_trvReports_Reports() {
    document.getElementById('loadingPanel_trvReports_Reports').innerHTML = document.getElementById('hfloadingPanel_trvReports_Reports').value;
    CallBack_trvReports_Reports.callback();
}

function ShowConnectionError_Reports() {
    var error = document.getElementById('hfErrorType_Reports').value;
    var errorBody = document.getElementById('hfConnectionError_Reports').value;
    showDialog(error, errorBody, 'error');
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function tlbItemReportsParametersRegulation_TlbReports_onClick() {
    ShowDialogReportParameters_Reports();
}

function tlbItemReportView_TlbReports_onClick() {
    GetReport_Reports();
}

function GetReport_Reports() {
    if (trvReports_Reports.get_selectedNode() != undefined) {
        var reportObj = trvReports_Reports.get_selectedNode().get_value();
        reportObj = eval('(' + reportObj + ')');
        if (reportObj.TargetType == 'Report' && !reportObj.HasParameter) {
            ObjReport_Reports = new Object();
            ObjReport_Reports.ReportFileID = reportObj.FileID;
            ObjReport_Reports.ReportName = trvReports_Reports.get_selectedNode().get_text();

            GetReport_ReportsPage(CharToKeyCode_Reports(ObjReport_Reports.ReportFileID));
            DialogWaiting.Show();
        }
    }
}

function GetReport_ReportsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Reports').value;
            Response[1] = document.getElementById('hfConnectionError_Reports').value;
        }
        if (RetMessage[2] == 'success')
            ShowReport_Reports(Response);
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function ShowReport_Reports(Response) {
    var stiReportGUID = Response[3];
    var reportName = null;
    if (ObjReport_Reports != null)
        reportName = ObjReport_Reports.ReportName;
    var NewReportWindow = window.open("ReportViewer.aspx?ReportGUID=" + stiReportGUID + "&ReportTitle=" + CharToKeyCode_Reports(reportName) + "", "ReportViewer" + (new Date()).getTime() + "", "width=" + screen.width + ",height=" + screen.height + ",toolbar=yes,location=yes,directories=yes,status=yes,menubar=yes,scrollbars=yes,copyhistory=yes,resizable=yes");
}

function ShowDialogReportParameters_Reports() {
    if (trvReports_Reports.get_selectedNode() != undefined) {
        var reportObj = trvReports_Reports.get_selectedNode().get_value();
        reportObj = eval('(' + reportObj + ')');
        switch (reportObj.TargetType) {
            case 'Report':
                var ObjReportParameters = new Object();
                ObjReportParameters.ReportID = trvReports_Reports.get_selectedNode().get_id();
                ObjReportParameters.ReportFileID = reportObj.FileID;
                ObjReportParameters.ReportName = trvReports_Reports.get_selectedNode().get_text();
                parent.DialogReportParameters.set_value(ObjReportParameters);
                parent.DialogReportParameters.Show();
                break;
            case 'ReportGroup':
                break;
        }
        CollapseControls_Reports();
    }
}

function CallBack_cmbReportFiles_Reports_onCallbackError(sender, e) {
    ShowConnectionError_Reports();
}

function CollapseControls_Reports() {
    cmbReportFiles_Reports.collapse();
}

function tlbItemFormReconstruction_TlbReports_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvReportsIntroduction_iFrame').src = 'Reports.aspx';
}

function tlbItemHelp_TlbReports_onClick() {
    LoadHelpPage('tlbItemHelp_TlbReports');
}







