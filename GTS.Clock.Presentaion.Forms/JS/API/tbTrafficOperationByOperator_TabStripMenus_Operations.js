
function GetBoxesHeaders_TrafficOperationByOperator() {
    GetBoxesHeaders_TrafficOperationByOperatorPage();
}

function GetBoxesHeaders_TrafficOperationByOperatorPage_onCallBack(Response) {
    document.getElementById('header_PersonnelSearchBox_TrafficOperationByOperator').innerHTML = Response[0];
    document.getElementById('header_TrafficDetails_TrafficOperationByOperator').innerHTML = Response[1];
    document.getElementById('clmnName_cmbPersonnel_TrafficOperationByOperator').innerHTML = Response[2];
    document.getElementById('clmnBarCode_cmbPersonnel_TrafficOperationByOperator').innerHTML = Response[3];
}

function ViewCurrentLangCalendars_TrafficOperationByOperator() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpFromDate_TrafficOperationByOperator").parentNode.removeChild(document.getElementById("pdpFromDate_TrafficOperationByOperator"));
            document.getElementById("pdpFromDate_TrafficOperationByOperatorimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_TrafficOperationByOperatorimgbt"));
            document.getElementById("pdpToDate_TrafficOperationByOperator").parentNode.removeChild(document.getElementById("pdpToDate_TrafficOperationByOperator"));
            document.getElementById("pdpToDate_TrafficOperationByOperatorimgbt").parentNode.removeChild(document.getElementById("pdpToDate_TrafficOperationByOperatorimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_FromDateCalendars_TrafficOperationByOperator").removeChild(document.getElementById("Container_gCalFromDate_TrafficOperationByOperator"));
            document.getElementById("Container_ToDateCalendars_TrafficOperationByOperator").removeChild(document.getElementById("Container_gCalToDate_TrafficOperationByOperator"));
            break;
    }
}


function CallBack_cmbPersonnel_TrafficOperationByOperator_onCallBackComplete(sender, e) {
}


function gdpFromDate_TrafficOperationByOperator_OnDateChange(sender, e) {
    var fromDate = gdpFromDate_TrafficOperationByOperator.getSelectedDate();
    gCalFromDate_TrafficOperationByOperator.setSelectedDate(fromDate);
}

function btn_gdpFromDate_TrafficOperationByOperator_OnClick(event) {
    if (gCalFromDate_TrafficOperationByOperator.get_popUpShowing()) {
        gCalFromDate_TrafficOperationByOperator.hide();
    }
    else {
        gCalFromDate_TrafficOperationByOperator.setSelectedDate(gdpFromDate_TrafficOperationByOperator.getSelectedDate());
        gCalFromDate_TrafficOperationByOperator.show();
    }
}

function btn_gdpFromDate_TrafficOperationByOperator_OnMouseUp(event) {
    if (gCalFromDate_TrafficOperationByOperator.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else
        return true;
}

function gCalFromDate_TrafficOperationByOperator_OnChange(sender, e) {
    var fromDate = gCalFromDate_TrafficOperationByOperator.getSelectedDate();
    gdpFromDate_TrafficOperationByOperator.setSelectedDate(fromDate);
}


function gdpToDate_TrafficOperationByOperator_OnDateChange(sender, e) {
    var toDate = gdpToDate_TrafficOperationByOperator.getSelectedDate();
    gCalToDate_TrafficOperationByOperator.setSelectedDate(toDate);
}

function btn_gdpToDate_TrafficOperationByOperator_OnClick(event) {
    if (gCalToDate_TrafficOperationByOperator.get_popUpShowing()) {
        gCalToDate_TrafficOperationByOperator.hide();
    }
    else {
        gCalToDate_TrafficOperationByOperator.setSelectedDate(gdpToDate_TrafficOperationByOperator.getSelectedDate());
        gCalToDate_TrafficOperationByOperator.show();
    }
}

function btn_gdpToDate_TrafficOperationByOperator_OnMouseUp(event) {
    if (gCalToDate_TrafficOperationByOperator.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else
        return true;
}

function gCalToDate_TrafficOperationByOperator_OnChange(sender, e) {
    var toDate = gCalToDate_TrafficOperationByOperator.getSelectedDate();
    gdpToDate_TrafficOperationByOperator.setSelectedDate(toDate);
}


function tbTrafficOperationByOperator_TabStripMenus_onClose() {
    parent.CloseCurrentTabOnTabStripMenus();
}

function ShowDialogCollectiveTraffic_TrafficOperationByOperator() {
    parent.DialogCollectiveTraffic.Show();
}

function tlbItemHelp_TrafficOperationByOperator_onClick() {
    LoadHelpPage('tlbItemHelp_TrafficOperationByOperator');    
}
