
var box_RequestType_CollectiveTraffic_IsShown = false;
var CurrentState_DialogCollectiveTraffic = 'View';


function cmbType_CollectiveTraffic_onChange(sender, e) {
    if (cmbType_CollectiveTraffic.getSelectedItem().get_id() == 'Leave')
        ChangeHideElementsState_DialogCollectiveTraffic('Leave', false);
    if (cmbType_CollectiveTraffic.getSelectedItem().get_id() == 'Mission')
        ChangeHideElementsState_DialogCollectiveTraffic('Mission', false);    
}

function gdpFromDate_CollectiveTraffic_OnDateChange(sender, e) {
    var fromDate = gdpFromDate_CollectiveTraffic.getSelectedDate();
    gCalFromDate_CollectiveTraffic.setSelectedDate(fromDate);
}

function btn_gdpFromDate_CollectiveTraffic_OnClick(event) {
    if (gCalFromDate_CollectiveTraffic.get_popUpShowing()) {
        gCalFromDate_CollectiveTraffic.hide();
    }
    else {
        gCalFromDate_CollectiveTraffic.setSelectedDate(gdpFromDate_CollectiveTraffic.getSelectedDate());
        gCalFromDate_CollectiveTraffic.show();
    }
}

function btn_gdpFromDate_CollectiveTraffic_OnMouseUp(event) {
    if (gCalFromDate_CollectiveTraffic.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else
        return true;
}

function gCalFromDate_CollectiveTraffic_OnChange(sender, e) {
    var fromDate = gCalFromDate_CollectiveTraffic.getSelectedDate();
    gdpFromDate_CollectiveTraffic.setSelectedDate(fromDate);
}

function gCalFromDate_CollectiveTraffic_onLoad(sender, e) {
    window.gCalFromDate_CollectiveTraffic.PopUpObject.z = 25000000;
}

function gdpToDate_CollectiveTraffic_OnDateChange(sender, e) {
    var toDate = gdpToDate_CollectiveTraffic.getSelectedDate();
    gCalToDate_CollectiveTraffic.setSelectedDate(toDate);
}

function btn_gdpToDate_CollectiveTraffic_OnClick(event) {
    if (gCalToDate_CollectiveTraffic.get_popUpShowing()) {
        gCalToDate_CollectiveTraffic.hide();
    }
    else {
        gCalToDate_CollectiveTraffic.setSelectedDate(gdpToDate_CollectiveTraffic.getSelectedDate());
        gCalToDate_CollectiveTraffic.show();
    }
}

function btn_gdpToDate_CollectiveTraffic_OnMouseUp(event) {
    if (gCalToDate_CollectiveTraffic.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else
        return true;
}

function gCalToDate_CollectiveTraffic_OnChange(sender, e) {
    var toDate = gCalToDate_CollectiveTraffic.getSelectedDate();
    gdpToDate_CollectiveTraffic.setSelectedDate(toDate);
}

function gCalToDate_CollectiveTraffic_onLoad(sender, e) {
    window.gCalToDate_CollectiveTraffic.PopUpObject.z = 25000000;
}

function trvMissionLocation_CollectiveTraffic_onNodeSelect(sender, e) {
    cmbMissionLocation_CollectiveTraffic.set_text(e.get_node().get_text());
    cmbMissionLocation_CollectiveTraffic.collapse();
}

function CallBack_GridRegisteredRequests_CollectiveTraffic_OnCallbackComplete(sender, e) {
}

function GetBoxesHeaders_CollectiveTraffic() {
    GetBoxesHeaders_CollectiveTrafficPage();
}

function GetBoxesHeaders_CollectiveTrafficPage_onCallBack(Response) {
    parent.parent.document.getElementById('Title_DialogCollectiveTraffic').innerHTML = Response[0];    
    document.getElementById('header_RegisteredRequests_CollectiveTraffic').innerHTML = Response[1];
}

function rdbTraffic_CollectiveTraffic_onClick() {
    ChangeRdbsEnabled_DialogCollectiveTraffic('');
    box_RequestType_CollectiveTraffic_onShowHide();
}

function rdbLeave_CollectiveTraffic_onClick() {
    ChangeRdbsEnabled_DialogCollectiveTraffic('');
    box_RequestType_CollectiveTraffic_onShowHide();
}

function rdbMission_CollectiveTraffic_onClick() {
    ChangeRdbsEnabled_DialogCollectiveTraffic('');
    box_RequestType_CollectiveTraffic_onShowHide();
}

function box_RequestType_CollectiveTraffic_onShowHide() {
    setSlideDownSpeed(20);
    ChangeHideElementsState_DialogCollectiveTraffic('All',true);
    if (box_RequestType_CollectiveTraffic_IsShown)
        slidedown_showHide('box_RequestType_CollectiveTraffic');        
    slidedown_showHide('box_RequestType_CollectiveTraffic');
    if (box_RequestType_CollectiveTraffic_IsShown)
        box_RequestType_CollectiveTraffic_IsShown = false;
    else
        box_RequestType_CollectiveTraffic_IsShown = true;
}

function ChangeHideElementsState_DialogCollectiveTraffic(Type, State) {
    var LeaveVisibility;
    var MissionVisibility;
    switch (Type) {
        case 'Leave':
            if (State)
                LeaveVisibility = 'hidden';
            else
                LeaveVisibility = 'visible';
            MissionVisibility = 'hidden';
            break;
        case 'Mission':
            if (State)
                MissionVisibility = 'hidden';
            else
                MissionVisibility = 'visible';
            LeaveVisibility = 'hidden';
            break;
        case 'All':
            LeaveVisibility = 'hidden';
            MissionVisibility = 'hidden';
            break;
    }
    document.getElementById('cmbIllnessName_CollectiveTraffic').style.visibility = LeaveVisibility;
    document.getElementById('cmbDoctorName_CollectiveTraffic').style.visibility = LeaveVisibility;
    document.getElementById('cmbMissionLocation_CollectiveTraffic').style.visibility = MissionVisibility;
    document.getElementById('lblIllnessName_CollectiveTraffic').style.visibility = LeaveVisibility;
    document.getElementById('lblDoctorName_CollectiveTraffic').style.visibility = LeaveVisibility;
    document.getElementById('lblMissionLocation_CollectiveTraffic').style.visibility = MissionVisibility;

}

function SetPosition_DropDownDives_DialogCollectiveTraffic() {
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('box_RequestType_CollectiveTraffic').style.right = '100px';
    if (parent.CurrentLangID == 'en-US')
        document.getElementById('box_RequestType_CollectiveTraffic').style.left = '100px';
}

function ViewCurrentLangCalendars_DialogCollectiveTraffic() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpFromDate_CollectiveTraffic").parentNode.removeChild(document.getElementById("pdpFromDate_CollectiveTraffic"));
            document.getElementById("pdpFromDate_CollectiveTrafficimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_CollectiveTrafficimgbt"));
            document.getElementById("pdpToDate_CollectiveTraffic").parentNode.removeChild(document.getElementById("pdpToDate_CollectiveTraffic"));
            document.getElementById("pdpToDate_CollectiveTrafficimgbt").parentNode.removeChild(document.getElementById("pdpToDate_CollectiveTrafficimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_FromDateCalendars_CollectiveTraffic").removeChild(document.getElementById("Container_gCalFromDate_CollectiveTraffic"));
            document.getElementById("Container_ToDateCalendars_CollectiveTraffic").removeChild(document.getElementById("Container_gCalToDate_CollectiveTraffic"));
            break;
    }
}

function SetButtonImages_TimeSelectors_DialogCollectiveTraffic() {
    SetButtonImages_TimeSelector_DialogCollectiveTraffic('TimeSelector_FromHour_CollectiveTraffic');
    SetButtonImages_TimeSelector_DialogCollectiveTraffic('TimeSelector_ToHour_CollectiveTraffic');
}

function SetButtonImages_TimeSelector_DialogCollectiveTraffic(TimeSelector) {
    document.getElementById("" + TimeSelector + "_imgUp").src = "images/TimeSelector/CustomUp.gif";
    document.getElementById("" + TimeSelector + "_imgDown").src = "images/TimeSelector/CustomDown.gif";
    document.getElementById("" + TimeSelector + "_imgUp").onmouseover = function () {
        document.getElementById("" + TimeSelector + "_imgUp").src = "images/TimeSelector/oie_CustomUp.gif";
        FocusOnCurrentTimeSelector(TimeSelector);
    }
    document.getElementById(""+TimeSelector+"_imgDown").onmouseover = function () {
        document.getElementById("" + TimeSelector + "_imgDown").src = "images/TimeSelector/oie_CustomDown.gif";
        FocusOnCurrentTimeSelector(TimeSelector);
    }
    document.getElementById(""+TimeSelector+"_imgUp").onmouseout = function () {
        document.getElementById("" + TimeSelector + "_imgUp").src = "images/TimeSelector/CustomUp.gif";
    }
    document.getElementById(""+TimeSelector+"_imgDown").onmouseout = function () {
        document.getElementById("" + TimeSelector + "_imgDown").src = "images/TimeSelector/CustomDown.gif";
    }
}

function FocusOnCurrentTimeSelector(TimeSelector) {
    if (document.activeElement.id != "" + TimeSelector + "_txtHour" && document.activeElement.id != "" + TimeSelector + "_txtMinute" && document.activeElement.id != "" + TimeSelector + "_txtSecond")
        document.getElementById("" + TimeSelector + "_txtHour").focus();
}

function SetBaseState_CollectiveTraffic() {
    HidePdps_DialogCollectiveTraffic();
    HideDives_DialogCollectiveTraffic();
    ChangeRdbsChecked_DialogCollectiveTraffic();
}

function HidePdps_DialogCollectiveTraffic() {
    try 
    {
        updateDateField('pdpFromDate_CollectiveTraffic');
        updateDateField('pdpToDate_CollectiveTraffic');
    }
    catch (error) {
    }
}

function HideDives_DialogCollectiveTraffic() {
    if (box_RequestType_CollectiveTraffic_IsShown)
        box_RequestType_CollectiveTraffic_onShowHide();
}

function CollectiveTraffic_onSave() {
    CurrentState_DialogCollectiveTraffic = 'Save';
    ChangeRdbsEnabled_DialogCollectiveTraffic(CurrentState_DialogCollectiveTraffic);    
    SetBaseState_CollectiveTraffic();
}

function CollectiveTraffic_onCancel() {
    CurrentState_DialogCollectiveTraffic = 'Cancel';
    ChangeRdbsEnabled_DialogCollectiveTraffic(CurrentState_DialogCollectiveTraffic);
    SetBaseState_CollectiveTraffic();
}


function ChangeRdbsChecked_DialogCollectiveTraffic() {
    document.getElementById('rdbTraffic_CollectiveTraffic').checked = false;
    document.getElementById('rdbLeave_CollectiveTraffic').checked = false;
    document.getElementById('rdbMission_CollectiveTraffic').checked = false;
}

function ChangeRdbsEnabled_DialogCollectiveTraffic(State) {
    if (State == 'Save' || State == 'Cancel') {
        document.getElementById('rdbTraffic_CollectiveTraffic').disabled = '';
        document.getElementById('rdbLeave_CollectiveTraffic').disabled = '';
        document.getElementById('rdbMission_CollectiveTraffic').disabled = '';
    }
    else {
        document.getElementById('rdbTraffic_CollectiveTraffic').disabled = 'disabled';
        document.getElementById('rdbLeave_CollectiveTraffic').disabled = 'disabled';
        document.getElementById('rdbMission_CollectiveTraffic').disabled = 'disabled';
    }
}

function CollectiveTraffic_onClose() {
    parent.DialogCollectiveTraffic.Close();
}

function tlbItemHelp_TlbCollectiveTraffic_onClick() {
    LoadHelpPage('tlbItemHelp_TlbCollectiveTraffic');
}







