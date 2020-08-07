var CurrentTabIndex_ExceptionShifts = '';
var CurrentPageCombosCallBcakStateObj = new Object();
var ConfirmState_ExceptionShifts = null;
var LoadState_cmbPersonnel_TwoDayReplacement_ExceptionShifts = 'Normal';
var CurrentPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts = 0;
var SearchTerm_cmbPersonnel_TwoDayReplacement_ExceptionShifts = '';
var AdvancedSearchTerm_cmbPersonnel_TwoDayReplacement_ExceptionShifts = '';
var LoadState_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts = 'Normal';
var CurrentPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts = 0;
var SearchTerm_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts = '';
var AdvancedSearchTerm_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts = '';
var LoadState_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts = 'Normal';
var SearchTerm_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts = '';
var AdvancedSearchTerm_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts = '';
var CurrentPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts = 0;
var CurrentAdvancedSearchState_TwoPersonnelReplacement_ExceptionShifts = null;
var CurrentReplacementState_ExceptionShifs = 'Personnel';
var CurrentShiftViewState_ExceptionShifts = 'NotView';
var ObjExceptionShift_ExceptionShifts = null;
var box_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts_IsShown = false;
var box_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts_IsShown = false;


///////////////////// gdpDetailsFromDate & gCalDetailsFromDate ////////////////////////
function gdpDetailsFromDate_ExceptionShifts_OnDateChange(sender, eventArgs) {
    var fromDate = gdpDetailsFromDate_ExceptionShifts.getSelectedDate();
    gCalDetailsFromDate_ExceptionShifts.setSelectedDate(fromDate);
}
function gCalDetailsFromDate_ExceptionShifts_OnChange(sender, eventArgs) {
    var fromDate = gCalDetailsFromDate_ExceptionShifts.getSelectedDate();
    gdpDetailsFromDate_ExceptionShifts.setSelectedDate(fromDate);
}
function btn_gdpDetailsFromDate_ExceptionShifts_OnClick(event) {
    if (gCalDetailsFromDate_ExceptionShifts.get_popUpShowing()) {
        gCalDetailsFromDate_ExceptionShifts.hide();
    }
    else {
        gCalDetailsFromDate_ExceptionShifts.setSelectedDate(gdpDetailsFromDate_ExceptionShifts.getSelectedDate());
        gCalDetailsFromDate_ExceptionShifts.show();
    }
}
function btn_gdpDetailsFromDate_ExceptionShifts_OnMouseUp(event) {
    if (gCalDetailsFromDate_ExceptionShifts.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalDetailsFromDate_ExceptionShifts_onLoad(sender, e) {
    window.gCalDetailsFromDate_ExceptionShifts.PopUpObject.z = 25000000;
}

///////////////////// gdpDetailsToDate & gCalDetailsToDate ////////////////////////
function gdpDetailsToDate_ExceptionShifts_OnDateChange(sender, eventArgs) {
    var toDate = gdpDetailsToDate_ExceptionShifts.getSelectedDate();
    gCalDetailsToDate_ExceptionShifts.setSelectedDate(toDate);
}
function gCalDetailsToDate_ExceptionShifts_OnChange(sender, eventArgs) {
    var toDate = gCalDetailsToDate_ExceptionShifts.getSelectedDate();
    gdpDetailsToDate_ExceptionShifts.setSelectedDate(toDate);
}
function btn_gdpDetailsToDate_ExceptionShifts_OnClick(event) {
    if (gCalDetailsToDate_ExceptionShifts.get_popUpShowing()) {
        gCalDetailsToDate_ExceptionShifts.hide();
    }
    else {
        gCalDetailsToDate_ExceptionShifts.setSelectedDate(gdpDetailsToDate_ExceptionShifts.getSelectedDate());
        gCalDetailsToDate_ExceptionShifts.show();
    }
}
function btn_gdpDetailsToDate_ExceptionShifts_OnMouseUp(event) {
    if (gCalDetailsToDate_ExceptionShifts.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalDetailsToDate_ExceptionShifts_onLoad(sender, e) {
    window.gCalDetailsToDate_ExceptionShifts.PopUpObject.z = 25000000;
}


///////////////////// gdpFromDate_TwoDayReplacement_ExceptionShifts & gCalFromDate_TwoDayReplacement_ExceptionShifts ////////////////////////
function gdpFromDate_TwoDayReplacement_ExceptionShifts_OnDateChange(sender, eventArgs) {
    var fromDate = gdpFromDate_TwoDayReplacement_ExceptionShifts.getSelectedDate();
    gCalFromDate_TwoDayReplacement_ExceptionShifts.setSelectedDate(fromDate);
}
function gCalFromDate_TwoDayReplacement_ExceptionShifts_OnChange(sender, eventArgs) {
    var fromDate = gCalFromDate_TwoDayReplacement_ExceptionShifts.getSelectedDate();
    gdpFromDate_TwoDayReplacement_ExceptionShifts.setSelectedDate(fromDate);
}
function btn_gdpFromDate_TwoDayReplacement_ExceptionShifts_OnClick(event) {
    if (gCalFromDate_TwoDayReplacement_ExceptionShifts.get_popUpShowing()) {
        gCalFromDate_TwoDayReplacement_ExceptionShifts.hide();
    }
    else {
        gCalFromDate_TwoDayReplacement_ExceptionShifts.setSelectedDate(gdpFromDate_TwoDayReplacement_ExceptionShifts.getSelectedDate());
        gCalFromDate_TwoDayReplacement_ExceptionShifts.show();
    }
}
function btn_gdpFromDate_TwoDayReplacement_ExceptionShifts_OnMouseUp(event) {
    if (gCalFromDate_TwoDayReplacement_ExceptionShifts.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalFromDate_TwoDayReplacement_ExceptionShifts_onLoad(sender, e) {
    window.gCalFromDate_TwoDayReplacement_ExceptionShifts.PopUpObject.z = 25000000;
}

///////////////////// gdpToDate_TwoDayReplacement_ExceptionShifts & gCalToDate_TwoDayReplacement_ExceptionShifts  ////////////////////////
function gdpToDate_TwoDayReplacement_ExceptionShifts_OnDateChange(sender, eventArgs) {
    var ToDate = gdpToDate_TwoDayReplacement_ExceptionShifts.getSelectedDate();
    gCalToDate_TwoDayReplacement_ExceptionShifts.setSelectedDate(ToDate);
}
function gCalToDate_TwoDayReplacement_ExceptionShifts_OnChange(sender, eventArgs) {
    var ToDate = gCalToDate_TwoDayReplacement_ExceptionShifts.getSelectedDate();
    gdpToDate_TwoDayReplacement_ExceptionShifts.setSelectedDate(ToDate);
}
function btn_gdpToDate_TwoDayReplacement_ExceptionShifts_OnClick(event) {
    if (gCalToDate_TwoDayReplacement_ExceptionShifts.get_popUpShowing()) {
        gCalToDate_TwoDayReplacement_ExceptionShifts.hide();
    }
    else {
        gCalToDate_TwoDayReplacement_ExceptionShifts.setSelectedDate(gdpToDate_TwoDayReplacement_ExceptionShifts.getSelectedDate());
        gCalToDate_TwoDayReplacement_ExceptionShifts.show();
    }
}
function btn_gdpToDate_TwoDayReplacement_ExceptionShifts_OnMouseUp(event) {
    if (gCalToDate_TwoDayReplacement_ExceptionShifts.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalToDate_TwoDayReplacement_ExceptionShifts_onLoad(sender, e) {
    window.gCalToDate_TwoDayReplacement_ExceptionShifts.PopUpObject.z = 25000000;
}

///////////////////// gdpTwoPersonnelReplacement1_ExceptionShifts & gCalTwoPersonnelReplacement1_ExceptionShifts ////////////////////////
function gdpTwoPersonnelReplacement1_ExceptionShifts_OnDateChange(sender, eventArgs) {
    var FromDate = gdpTwoPersonnelReplacement1_ExceptionShifts.getSelectedDate();
    gCalTwoPersonnelReplacement1_ExceptionShifts.setSelectedDate(FromDate);
}
function gCalTwoPersonnelReplacement1_ExceptionShifts_OnChange(sender, eventArgs) {
    var FromDate = gCalTwoPersonnelReplacement1_ExceptionShifts.getSelectedDate();
    gdpTwoPersonnelReplacement1_ExceptionShifts.setSelectedDate(FromDate);
}
function btn_gdpTwoPersonnelReplacement1_ExceptionShifts_OnClick(event) {
    if (gCalTwoPersonnelReplacement1_ExceptionShifts.get_popUpShowing()) {
        gCalTwoPersonnelReplacement1_ExceptionShifts.hide();
    }
    else {
        gCalTwoPersonnelReplacement1_ExceptionShifts.setSelectedDate(gdpTwoPersonnelReplacement1_ExceptionShifts.getSelectedDate());
        gCalTwoPersonnelReplacement1_ExceptionShifts.show();
    }
}
function btn_gdpTwoPersonnelReplacement1_ExceptionShifts_OnMouseUp(event) {
    if (gCalTwoPersonnelReplacement1_ExceptionShifts.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalTwoPersonnelReplacement1_ExceptionShifts_onLoad(sender, e) {
    window.gCalTwoPersonnelReplacement1_ExceptionShifts.PopUpObject.z = 25000000;
}

///////////////////// gdpTwoPersonnelReplacement2_ExceptionShifts & gCalTwoPersonnelReplacement2_ExceptionShifts ////////////////////////
function gdpTwoPersonnelReplacement2_ExceptionShifts_OnDateChange(sender, eventArgs) {
    var ToDate = gdpTwoPersonnelReplacement2_ExceptionShifts.getSelectedDate();
    gCalTwoPersonnelReplacement2_ExceptionShifts.setSelectedDate(ToDate);
}
function gCalTwoPersonnelReplacement2_ExceptionShifts_OnChange(sender, eventArgs) {
    var ToDate = gCalTwoPersonnelReplacement2_ExceptionShifts.getSelectedDate();
    gdpTwoPersonnelReplacement2_ExceptionShifts.setSelectedDate(ToDate);
}
function btn_gdpTwoPersonnelReplacement2_ExceptionShifts_OnClick(event) {
    if (gCalTwoPersonnelReplacement2_ExceptionShifts.get_popUpShowing()) {
        gCalTwoPersonnelReplacement2_ExceptionShifts.hide();
    }
    else {
        gCalTwoPersonnelReplacement2_ExceptionShifts.setSelectedDate(gdpTwoPersonnelReplacement1_ExceptionShifts.getSelectedDate());
        gCalTwoPersonnelReplacement2_ExceptionShifts.show();
    }
}
function btn_gdpTwoPersonnelReplacement2_ExceptionShifts_OnMouseUp(event) {
    if (gCalTwoPersonnelReplacement2_ExceptionShifts.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalTwoPersonnelReplacement2_ExceptionShifts_onLoad(sender, e) {
    window.gCalTwoPersonnelReplacement2_ExceptionShifts.PopUpObject.z = 25000000;
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////

function init_ExceptionShifts() {
    var TabID = parent.ActiveTabID_ExceptionShifts;
    CurrentLangID = parent.CurrentLangID;

    TabStripExceptionShifts_onTabsEnabledChange(TabID);
    ViewCurrentLangCalendars_TabStripExceptionShifts(parent.SysLangID, TabID);
    ChangeComboColumnHeader_DialogExceptionShifts(CurrentLangID, TabID);
}

function TabStripExceptionShifts_onTabsEnabledChange() {
    var ObjDialogExceptionShifts = parent.DialogExceptionShifts.get_value();
    var TabID = ObjDialogExceptionShifts.ActiveTabID;
    var TabsCol = TabStripExceptionShifts.get_tabs();
    for (var i = 0; i < TabsCol.get_length(); i++) {
        {
            var tab = TabsCol.getTab(i);
            if (tab.get_id() == TabID) {
                tab.set_enabled(true);
                CurrentTabIndex_ExceptionShifts = i;
                TabStripExceptionShifts.selectTabById(TabID);
                MultiPageExceptionShifts.getPage(i).show();
            }
            else
                tab.set_enabled(false);                                
        }
    }
}

function ViewCurrentLangCalendars_TabStripExceptionShifts() {
    var ObjDialogExceptionShifts = parent.DialogExceptionShifts.get_value();
    var TabID = ObjDialogExceptionShifts.ActiveTabID;
    switch (TabID) {
        case 'tbDetails_TabStripShiftDetails':
            switch (parent.SysLangID) {
                case 'en-US':
                    document.getElementById("pdpDetailsFromDate_ExceptionShifts").parentNode.removeChild(document.getElementById("pdpDetailsFromDate_ExceptionShifts"));
                    document.getElementById("pdpDetailsFromDate_ExceptionShiftsimgbt").parentNode.removeChild(document.getElementById("pdpDetailsFromDate_ExceptionShiftsimgbt"));
                    document.getElementById("pdpDetailsToDate_ExceptionShifts").parentNode.removeChild(document.getElementById("pdpDetailsToDate_ExceptionShifts"));
                    document.getElementById("pdpDetailsToDate_ExceptionShiftsimgbt").parentNode.removeChild(document.getElementById("pdpDetailsToDate_ExceptionShiftsimgbt"));
                    break;
                case 'fa-IR':
                    document.getElementById("Container_DetailsFromDateCalendars_ExceptionShifts").removeChild(document.getElementById("Container_gCalDetailsFromDate_ExceptionShifts"));
                    document.getElementById("Container_DetailsToDateCalendars_ExceptionShifts").removeChild(document.getElementById("Container_gCalDetailsToDate_ExceptionShifts"));
                    break;
            }
            break;
        case 'tbTwoDayReplacement_TabStripShiftDetails':
            switch (parent.SysLangID) {
                case 'en-US':
                    document.getElementById("pdpFromDate_TwoDayReplacement_ExceptionShifts").parentNode.removeChild(document.getElementById("pdpFromDate_TwoDayReplacement_ExceptionShifts"));
                    document.getElementById("pdpFromDate_TwoDayReplacement_ExceptionShiftsimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_TwoDayReplacement_ExceptionShiftsimgbt"));
                    document.getElementById("pdpToDate_TwoDayReplacement_ExceptionShifts").parentNode.removeChild(document.getElementById("pdpToDate_TwoDayReplacement_ExceptionShifts"));
                    document.getElementById("pdpToDate_TwoDayReplacement_ExceptionShiftsimgbt").parentNode.removeChild(document.getElementById("pdpToDate_TwoDayReplacement_ExceptionShiftsimgbt"));
                    break;
                case 'fa-IR':
                    document.getElementById("Container_FromDateCalendars_TwoDayReplacement_ExceptionShifts").removeChild(document.getElementById("Container_gCalFromDate_TwoDayReplacement_ExceptionShifts"));
                    document.getElementById("Container_ToDateCalendars_TwoDayReplacement_ExceptionShifts").removeChild(document.getElementById("Container_gCalToDate_TwoDayReplacement_ExceptionShifts"));
                    break;
            }
            break;
        case 'tbTwoPersonnelReplacement_TabStripShiftDetails':
            switch (parent.SysLangID) {
                case 'en-US':
                    document.getElementById("pdpTwoPersonnelReplacement_ExceptionShifts").parentNode.removeChild(document.getElementById("pdpTwoPersonnelReplacement_ExceptionShifts"));
                    document.getElementById("pdpTwoPersonnelReplacement_ExceptionShiftsimgbt").parentNode.removeChild(document.getElementById("pdpTwoPersonnelReplacement_ExceptionShiftsimgbt"));
                    break;
                case 'fa-IR':
                    document.getElementById("Container_TwoPersonnelReplacementCalendars1_ExceptionShifts").removeChild(document.getElementById("Container_gCalTwoPersonnelReplacement1_ExceptionShifts"));
                    document.getElementById("Container_TwoPersonnelReplacementCalendars2_ExceptionShifts").removeChild(document.getElementById("Container_gCalTwoPersonnelReplacement2_ExceptionShifts"));
                    break;
            }
            break;
    }
}

function CollapseControls_ExceptionShifts(exception) {
    var ObjDialogExceptionShifts = parent.DialogExceptionShifts.get_value();
    var State = ObjDialogExceptionShifts.State;
    if (State == 'Add' || State == 'Edit') {
        if (exception == null || exception != cmbShift_ExceptionShifts)
            cmbShift_ExceptionShifts.collapse();
        if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
            displayDatePicker('pdpDetailsFromDate_ExceptionShifts');
    }
    if (State == 'TwoDayReplacement') {
        if (exception == null || exception != cmbWorkGroup_TwoDayReplacement_ExceptionShifts)
            cmbWorkGroup_TwoDayReplacement_ExceptionShifts.collapse();
        if (exception == null || exception != cmbPersonnel_TwoDayReplacement_ExceptionShifts)
            cmbPersonnel_TwoDayReplacement_ExceptionShifts.collapse();
        if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
            displayDatePicker('pdpFromDate_TwoDayReplacement_ExceptionShifts');
    }
    if (State == 'TwoPersonnelReplacement') {
        if (exception == null || exception != cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts)
            cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.collapse();
        if (exception == null || exception != cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts)
            cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.collapse();
        if (exception == null || exception != cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts)
            cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts.collapse();
        if (exception == null || exception != cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts)
            cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts.collapse();
        if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
            displayDatePicker('pdpTwoPersonnelReplacement1_ExceptionShifts');        
    }
}

function imgbox_SearchByPersonnel_TwoDayReplacement_ExceptionShifts_onClick() {
    SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts_onShowHide();
}

function SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts_onShowHide() {
    CollapseControls_ExceptionShifts(null);
    setSlideDownSpeed(200);
    slidedown_showHide('box_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts');

    if (box_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts_IsShown) {
        box_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts_IsShown = false;
        document.getElementById('imgbox_SearchByPersonnel_TwoDayReplacement_ExceptionShifts').src = 'Images/Ghadir/arrowDown.jpg';
    }
    else {
        box_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts_IsShown = true;
        document.getElementById('imgbox_SearchByPersonnel_TwoDayReplacement_ExceptionShifts').src = 'Images/Ghadir/arrowUp.jpg';
    }
}

function imgbox_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    SearchByPersonnel1Box_TwoPersonnelReplacement_ExceptionShifts_onShowHide();
}

function SearchByPersonnel1Box_TwoPersonnelReplacement_ExceptionShifts_onShowHide() {
    CollapseControls_ExceptionShifts(null);
    setSlideDownSpeed(200);
    if (box_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts_IsShown)
        SearchByPersonnel2Box_TwoPersonnelReplacement_ExceptionShifts_onShowHide();    
    slidedown_showHide('box_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts');

    if (box_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts_IsShown) {
        box_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts_IsShown = false;
        document.getElementById('imgbox_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts').src = 'Images/Ghadir/arrowDown.jpg';
    }
    else {
        box_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts_IsShown = true;
        document.getElementById('imgbox_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts').src = 'Images/Ghadir/arrowUp.jpg';
    }
}

function imgbox_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    SearchByPersonnel2Box_TwoPersonnelReplacement_ExceptionShifts_onShowHide();
}

var box_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts_IsShown = false;
function SearchByPersonnel2Box_TwoPersonnelReplacement_ExceptionShifts_onShowHide() {
    CollapseControls_ExceptionShifts(null);
    setSlideDownSpeed(200);
    if (box_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts_IsShown)
        SearchByPersonnel1Box_TwoPersonnelReplacement_ExceptionShifts_onShowHide();
    slidedown_showHide('box_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts');

    if (box_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts_IsShown) {
        box_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts_IsShown = false;
        document.getElementById('imgbox_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts').src = 'Images/Ghadir/arrowDown.jpg';
    }
    else {
        box_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts_IsShown = true;
        document.getElementById('imgbox_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts').src = 'Images/Ghadir/arrowUp.jpg';
    }
}

function SetPosition_DropDownDives_DialogExceptionShifts() {
    switch (parent.CurrentLangID) {
        case "fa-IR":
            document.getElementById('box_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts').style.right = '150px';
            document.getElementById('box_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts').style.right = '120px';
            document.getElementById('box_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts').style.right = '400px';
            break;
        case "en-US":
            document.getElementById('box_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts').style.left = '150px';
            document.getElementById('box_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts').style.left = '120px';
            document.getElementById('box_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts').style.left = '400px';
            break;
    }
}

function GetBoxesHeaders_ExceptionShifts() {
    var ObjDialogExceptionShifts = parent.DialogExceptionShifts.get_value();
    var State = ObjDialogExceptionShifts.State;
    parent.document.getElementById('Title_DialogExceptionShifts').innerHTML = document.getElementById('hfTitle_DialogExceptionShifts').value;
    switch (State) {
        case 'Add':
            document.getElementById('cmbShift_ExceptionShifts_Input').value = document.getElementById('hfcmbAlarm_ExceptionShifts').value;
            break;
        case 'Edit':
            document.getElementById('cmbShift_ExceptionShifts_Input').value = document.getElementById('hfcmbAlarm_ExceptionShifts').value;
            break;
        case 'TwoDayReplacement':
            document.getElementById('header_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts').innerHTML = document.getElementById('hfheader_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts').value;
            document.getElementById('clmnName_cmbPersonnel_TwoDayReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_TwoDayReplacement_ExceptionShifts').value;
            document.getElementById('clmnBarCode_cmbPersonnel_TwoDayReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_TwoDayReplacement_ExceptionShifts').value;
            document.getElementById('clmnCardNum_cmbPersonnel_TwoDayReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_TwoDayReplacement_ExceptionShifts').value;
            break;
        case 'TwoPersonnelReplacement':
            document.getElementById('header_SearchByPersonnelBox1_TwoPersonnelReplacement_ExceptionShifts').innerHTML = document.getElementById('hfheader_SearchByPersonnelBox1_TwoPersonnelReplacement_ExceptionShifts').value;
            document.getElementById('header_SearchByPersonnelBox2_TwoPersonnelReplacement_ExceptionShifts').innerHTML = document.getElementById('hfheader_SearchByPersonnelBox2_TwoPersonnelReplacement_ExceptionShifts').value;
            document.getElementById('clmnName_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnName_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts').value;
            document.getElementById('clmnBarCode_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts').value;
            document.getElementById('clmnCardNum_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts').value;
            document.getElementById('clmnName_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnName_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts').value;
            document.getElementById('clmnBarCode_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts').value;
            document.getElementById('clmnCardNum_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts').value;
            break;            
    }
}

function tlbItemEndorsement_TlbExceptionShiftsDetails_onClick() {
    ResetShiftViewState_ExceptionShifts();
    UpdateExceptionShift_ExceptionShifts();
}

function tlbItemExit_TlbExceptionShiftsDetails_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_ExceptionShifts = confirmState;
    switch (confirmState) {
        case 'Exit':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_ExceptionShifts').value;
            break;
    }
    DialogConfirm.Show();
    CollapseControls_ExceptionShifts(null);
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_ExceptionShifts) {
        case 'Exit':
            CloseDialogExceptionShifts();
            break;
    }
}

function CloseDialogExceptionShifts() {
    parent.document.getElementById('DialogExceptionShifts_IFrame').src = 'WhitePage.aspx';
    parent.DialogExceptionShifts.Close();
    parent.document.getElementById('pgvExceptionShiftsIntroduction_iFrame').contentWindow.ChangePageState_MasterExceptionShifts('View');
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function cmbShift_ExceptionShifts_onExpand(sender, e) {
    CollapseControls_ExceptionShifts(cmbShift_ExceptionShifts);
    if (cmbShift_ExceptionShifts.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbShift_ExceptionShifts == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbShift_ExceptionShifts = true;
        CallBack_cmbShift_ExceptionShifts.callback();
    }
}

function cmbShift_ExceptionShifts_onCollapse(sender, e) {
    if (cmbShift_ExceptionShifts.getSelectedItem() == undefined) {
        var ObjDialogExceptionShifts = parent.DialogExceptionShifts.get_value();
        if (ObjDialogExceptionShifts.State == 'Edit' && ObjDialogExceptionShifts.ShiftID != undefined)
            document.getElementById('cmbShift_ExceptionShifts_Input').value = ObjDialogExceptionShifts.ShiftName;
    }
}

function CallBack_cmbShift_ExceptionShifts_onBeforeCallback(sender, e) {
    cmbShift_ExceptionShifts.dispose();
}

function CallBack_cmbShift_ExceptionShifts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Shifts').value;
    if (error == "") {
        document.getElementById('cmbShift_ExceptionShifts_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbShift_ExceptionShifts_DropImage').mousedown();
        cmbShift_ExceptionShifts.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbShift_ExceptionShifts_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CallBack_cmbShift_ExceptionShifts_onCallbackError(sender, e) {
    ShowConnectionError_ExceptionShifts();
}

function ShowConnectionError_ExceptionShifts() {
    var error = document.getElementById('hfErrorType_ExceptionShifts').value;
    var errorBody = document.getElementById('hfConnectionError_ExceptionShifts').value;
    showDialog(error, errorBody, 'error');
}

function cmbWorkGroup_TwoDayReplacement_ExceptionShifts_onExpand(sender, e) {
    CollapseControls_ExceptionShifts(cmbWorkGroup_TwoDayReplacement_ExceptionShifts);
    if (cmbWorkGroup_TwoDayReplacement_ExceptionShifts.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbWorkGroup_TwoDayReplacement_ExceptionShifts == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbWorkGroup_TwoDayReplacement_ExceptionShifts = true;
        CallBack_cmbWorkGroup_TwoDayReplacement_ExceptionShifts.callback();
    }
}

function CallBack_cmbWorkGroup_TwoDayReplacement_ExceptionShifts_onBeforeCallback(sender, e) {
    cmbWorkGroup_TwoDayReplacement_ExceptionShifts.dispose();
}

function CallBack_cmbWorkGroup_TwoDayReplacement_ExceptionShifts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkGroups_TwoDayReplacement_ExceptionShifts').value;
    if (error == "") {
        document.getElementById('cmbWorkGroup_TwoDayReplacement_ExceptionShifts_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbWorkGroup_TwoDayReplacement_ExceptionShifts_DropImage').mousedown();
        cmbWorkGroup_TwoDayReplacement_ExceptionShifts.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbWorkGroup_TwoDayReplacement_ExceptionShifts_DropDown').style.display = 'none';
    }
}

function CallBack_cmbWorkGroup_TwoDayReplacement_ExceptionShifts_onCallbackError(sender, e) {
    ShowConnectionError_ExceptionShifts();
}

function tlbItemEndorsement_TlbTwoDayReplacement_onClick() {
    ResetShiftViewState_ExceptionShifts();
    UpdateExceptionShift_ExceptionShifts();
}

function ResetShiftViewState_ExceptionShifts() {
    CurrentShiftViewState_ExceptionShifts = 'NotView';
}

function tlbItemExit_TlbTwoDayReplacement_onClick() {
    ShowDialogConfirm('Exit');
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts_onClick() {
    Refresh_cmbPersonnel_TwoDayReplacement_ExceptionShifts();
}

function Refresh_cmbPersonnel_TwoDayReplacement_ExceptionShifts() {
    LoadState_cmbPersonnel_TwoDayReplacement_ExceptionShifts = 'Normal';
    SetPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts(0);
}

function tlbItemFirst_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts_onClick() {
    SetPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts_onClick() {
    if (CurrentPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts != 0) {
        CurrentPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts = CurrentPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts - 1;
        SetPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts(CurrentPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts_onClick() {
    if (CurrentPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts < parseInt(document.getElementById('hfPersonnelPageCount_TwoDayReplacement_ExceptionShifts').value) - 1) {
        CurrentPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts = CurrentPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts + 1;
        SetPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts(CurrentPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts_onClick() {
    SetPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts(parseInt(document.getElementById('hfPersonnelPageCount_TwoDayReplacement_ExceptionShifts').value) - 1);
}

function SetPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts(pageIndex) {
    CurrentPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts = pageIndex;
    Fill_cmbPersonnel_TwoDayReplacement_ExceptionShifts(pageIndex);
}

function Fill_cmbPersonnel_TwoDayReplacement_ExceptionShifts(pageIndex) {
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_TwoDayReplacement_ExceptionShifts').value);
    var SearchTermConditions = '';
    switch (LoadState_cmbPersonnel_TwoDayReplacement_ExceptionShifts) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm_cmbPersonnel_TwoDayReplacement_ExceptionShifts = SearchTermConditions = document.getElementById('txtPersonnelSearch_TwoDayReplacement_ExceptionShifts').value;
            break;
        case 'AdvancedSearch':
            SearchTermConditions = AdvancedSearchTerm_cmbPersonnel_TwoDayReplacement_ExceptionShifts;
            break;
    }
    CallBack_cmbPersonnel_TwoDayReplacement_ExceptionShifts.callback(CharToKeyCode_ExceptionShifts(LoadState_cmbPersonnel_TwoDayReplacement_ExceptionShifts), CharToKeyCode_ExceptionShifts(pageSize.toString()), CharToKeyCode_ExceptionShifts(pageIndex.toString()), CharToKeyCode_ExceptionShifts(SearchTermConditions));
}

function CharToKeyCode_ExceptionShifts(str) {
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

function cmbPersonnel_TwoDayReplacement_ExceptionShifts_onExpand(sender, e) {
    CollapseControls_ExceptionShifts(cmbPersonnel_TwoDayReplacement_ExceptionShifts);
    SetPosition_cmbPersonnelControls_ExceptionShifts();
    if (cmbPersonnel_TwoDayReplacement_ExceptionShifts.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_TwoDayReplacement_ExceptionShifts == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_TwoDayReplacement_ExceptionShifts = true;
        SetPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts(0);
    }
}

function CallBack_cmbPersonnel_TwoDayReplacement_ExceptionShifts_onBeforeCallback(sender, e) {
    cmbPersonnel_TwoDayReplacement_ExceptionShifts.dispose();
}

function CallBack_cmbPersonnel_TwoDayReplacement_ExceptionShifts_onCallbackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_TwoDayReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_TwoDayReplacement_ExceptionShifts').value;
    document.getElementById('clmnBarCode_cmbPersonnel_TwoDayReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_TwoDayReplacement_ExceptionShifts').value;
    document.getElementById('clmnCardNum_cmbPersonnel_TwoDayReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_TwoDayReplacement_ExceptionShifts').value;

    SetPosition_cmbPersonnelControls_ExceptionShifts();

    var error = document.getElementById('ErrorHiddenField_TwoDayReplacement_ExceptionShifts').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_TwoDayReplacement_ExceptionShifts_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_TwoDayReplacement_ExceptionShifts_DropImage').mousedown();
        else 
            cmbPersonnel_TwoDayReplacement_ExceptionShifts.expand();        
        var personnelCount = document.getElementById('hfPersonnelCount_TwoDayReplacement_ExceptionShifts').value;
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_TwoDayReplacement_ExceptionShifts_DropDown').style.display = 'none';
    }
}

function SetPosition_cmbPersonnelControls_ExceptionShifts() {
    var ObjDialogExceptionShifts = parent.DialogExceptionShifts.get_value();
    var State = ObjDialogExceptionShifts.State;    
    switch (State) {
        case 'TwoDayReplacement':
            switch (parent.parent.CurrentLangID) {
                case 'fa-IR':
                    document.getElementById('cmbPersonnel_TwoDayReplacement_ExceptionShifts_DropDown').style.left = document.getElementById('Mastertbl_ExceptionShifts').clientWidth - (document.getElementById('Container_rdbPersonnel_TwoDayReplacement_ExceptionShifts').clientWidth + 400) + 'px';
                    break;
                case 'en-US':
                    document.getElementById('cmbPersonnel_TwoDayReplacement_ExceptionShifts_DropDown').style.left = '150px';
                    break;
            }
            break;
        case 'TwoPersonnelReplacement':
            switch (parent.parent.CurrentLangID) {
                case 'fa-IR':
                    document.getElementById('cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_DropDown').style.left = document.getElementById('Mastertbl_ExceptionShifts').clientWidth -(document.getElementById('Container_rdbPersonnel_TwoPersonnelReplacement_ExceptionShifts').clientWidth + 400) + 'px';
                    break;
                case 'en-US':
                    document.getElementById('cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_DropDown').style.left = '120px';
                    break;
            }
            break;
    }
}

function CallBack_cmbPersonnel_TwoDayReplacement_ExceptionShifts_onCallbackError(sender, e) {
    ShowConnectionError_ExceptionShifts();
}

function tlbItemSearch_TlbSearchPersonnel_TwoDayReplacement_ExceptionShifts_onClick() {
    LoadState_cmbPersonnel_TwoDayReplacement_ExceptionShifts = 'Search';
    SetPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_TwoDayReplacement_ExceptionShifts_onClick() {
    LoadState_cmbPersonnel_TwoDayReplacement_ExceptionShifts = 'AdvancedSearch';
    CurrentAdvancedSearchState_TwoPersonnelReplacement_ExceptionShifts = 'First';
    ShowDialogPersonnelSearch('ExceptionShifts');
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
    CollapseControls_ExceptionShifts(null);
}

function ExceptionShifts_onAfterPersonnelAdvancedSearch(SearchTerm) {
    var ObjDialogExceptionShifts = parent.DialogExceptionShifts.get_value();
    var State = ObjDialogExceptionShifts.State;
    switch (State) {
        case 'TwoDayReplacement':
            AdvancedSearchTerm_cmbPersonnel_TwoDayReplacement_ExceptionShifts = SearchTerm;
            SetPageIndex_cmbPersonnel_TwoDayReplacement_ExceptionShifts(0);
            break;
        case 'TwoPersonnelReplacement':
            switch (CurrentAdvancedSearchState_TwoPersonnelReplacement_ExceptionShifts) {
                case 'First':
                    AdvancedSearchTerm_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts = SearchTerm;
                    SetPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts(0);
                    break;
                case 'Second':
                    AdvancedSearchTerm_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts = SearchTerm;
                    SetPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts(0);
                    break;
            }
            break;
    }
}

function tlbItemView_TlbFirstDateView_TwoDayReplacement_ExceptionShifts_onClick() {
    CurrentShiftViewState_ExceptionShifts = 'First';
    UpdateExceptionShift_ExceptionShifts();
}

function tlbItemView_TlbSecondDateView_TwoDayReplacement_ExceptionShifts_onClick() {
    CurrentShiftViewState_ExceptionShifts = 'Second';
    UpdateExceptionShift_ExceptionShifts();
}

function tlbItemEndorsement_TlbTwoPersonnelReplacement_onClick() {
    ResetShiftViewState_ExceptionShifts();
    UpdateExceptionShift_ExceptionShifts();
}

function tlbItemExit_TlbTwoPersonnelReplacement_onClick() {
    ShowDialogConfirm('Exit');
}

function ResetCalendars_ExceptionShifts() {
    var ObjDialogExceptionShifts = parent.DialogExceptionShifts.get_value();
    var State = ObjDialogExceptionShifts.State;
    var currentDate_ExceptionShifts = document.getElementById('hfCurrentDate_ExceptionShifts').value;
    switch (State) {
        case 'TwoDayReplacement':
            switch (parent.SysLangID) {
                case 'fa-IR':
                    document.getElementById('pdpFromDate_TwoDayReplacement_ExceptionShifts').value = currentDate_ExceptionShifts;
                    document.getElementById('pdpToDate_TwoDayReplacement_ExceptionShifts').value = currentDate_ExceptionShifts;
                    break;
                case 'en-US':
                    currentDate_ExceptionShifts = new Date(currentDate_ExceptionShifts);                    
                    gdpFromDate_TwoDayReplacement_ExceptionShifts.setSelectedDate(currentDate_ExceptionShifts);
                    gCalFromDate_TwoDayReplacement_ExceptionShifts.setSelectedDate(currentDate_ExceptionShifts);
                    gdpToDate_TwoDayReplacement_ExceptionShifts.setSelectedDate(currentDate_ExceptionShifts);
                    gCalToDate_TwoDayReplacement_ExceptionShifts.setSelectedDate(currentDate_ExceptionShifts);
                    break;
            }
            break;
        case 'TwoPersonnelReplacement':
            switch (parent.SysLangID) {
                case 'fa-IR':
                    document.getElementById('pdpTwoPersonnelReplacement1_ExceptionShifts').value = currentDate_ExceptionShifts;
                    document.getElementById('pdpTwoPersonnelReplacement2_ExceptionShifts').value = currentDate_ExceptionShifts;            
                    break;
                case 'en-US':
                    currentDate_ExceptionShifts = new Date(currentDate_ExceptionShifts);
                    gdpTwoPersonnelReplacement1_ExceptionShifts.setSelectedDate(currentDate_ExceptionShifts);
                    gCalTwoPersonnelReplacement1_ExceptionShifts.setSelectedDate(currentDate_ExceptionShifts);
                    gdpTwoPersonnelReplacement2_ExceptionShifts.setSelectedDate(currentDate_ExceptionShifts);
                    gCalTwoPersonnelReplacement2_ExceptionShifts.setSelectedDate(currentDate_ExceptionShifts);
                    break;
            }
            break;
        default:
            switch (parent.SysLangID) {
                case 'fa-IR':
                    document.getElementById('pdpDetailsFromDate_ExceptionShifts').value = currentDate_ExceptionShifts;
                    document.getElementById('pdpDetailsToDate_ExceptionShifts').value = currentDate_ExceptionShifts;
                    break;
                case 'en-US':
                    currentDate_ExceptionShifts = new Date(currentDate_ExceptionShifts);
                    gdpDetailsFromDate_ExceptionShifts.setSelectedDate(currentDate_ExceptionShifts);
                    gCalDetailsFromDate_ExceptionShifts.setSelectedDate(currentDate_ExceptionShifts);
                    gdpDetailsToDate_ExceptionShifts.setSelectedDate(currentDate_ExceptionShifts);
                    gCalDetailsFromDate_ExceptionShifts.setSelectedDate(currentDate_ExceptionShifts);
                    break;
            }
            break;
    }
}

function cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onExpand(sender, e) {
    CollapseControls_ExceptionShifts(cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts);
    if (cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts = true;
        CallBack_cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.callback();
    }
}

function CallBack_cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onBeforeCallback(sender, e) {
    cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.dispose();
}

function CallBack_cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_FirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts').value;
    if (error == "") {
        document.getElementById('cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_DropImage').mousedown();
        cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_DropDown').style.display = 'none';
    }
}

function CallBack_cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onCallbackError(sender, e) {
    ShowConnectionError_ExceptionShifts();
}

function cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onExpand(sender, e) {
    CollapseControls_ExceptionShifts(cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts);
    if (cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts = true;
        CallBack_cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.callback();
    }
}

function CallBack_cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onBeforeCallback(sender, e) {
    cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.dispose();
}

function CallBack_cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_SecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts').value;
    if (error == "") {
        document.getElementById('cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_DropImage').mousedown();
        cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_DropDown').style.display = 'none';
    }
}

function CallBack_cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onCallbackError(sender, e) {
    ShowConnectionError_ExceptionShifts();
}

function tlbItemRefresh_TlbPaging1_PersonnelSearch1_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    Refresh_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts();
}

function Refresh_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts() {
    LoadState_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts = 'Normal';
    SetPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts(0);
}


function tlbItemFirst_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    SetPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts(0);
}

function tlbItemBefore_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    if (CurrentPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts != 0) {
        CurrentPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts = CurrentPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts - 1;
        SetPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts(CurrentPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts);
    }
}

function tlbItemNext_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    if (CurrentPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts < parseInt(document.getElementById('hfPersonnelPageCount_Personnel1_TwoPersonnelReplacement_ExceptionShifts').value) - 1) {
        CurrentPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts = CurrentPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts + 1;
        SetPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts(CurrentPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts);
    }
}

function tlbItemLast_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    SetPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts(parseInt(document.getElementById('hfPersonnelPageCount_Personnel1_TwoPersonnelReplacement_ExceptionShifts').value) - 1);
}

function SetPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts(pageIndex) {
    CurrentPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts = pageIndex;
    Fill_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts(pageIndex);
}

function Fill_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts(pageIndex) {
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_Personnel1_TwoPersonnelReplacement_ExceptionShifts').value);
    var SearchTermConditions = '';
    switch (LoadState_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts = SearchTermConditions = document.getElementById('txtPersonnelSearch1_TwoPersonnelReplacement_ExceptionShifts').value;
            break;
        case 'AdvancedSearch':
            SearchTermConditions = AdvancedSearchTerm_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts;
            break;
    }
    CallBack_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts.callback(CharToKeyCode_ExceptionShifts(LoadState_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts), CharToKeyCode_ExceptionShifts(pageSize.toString()), CharToKeyCode_ExceptionShifts(pageIndex.toString()), CharToKeyCode_ExceptionShifts(SearchTermConditions));
}

function cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onExpand(sender, e) {
    CollapseControls_ExceptionShifts(cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts);
    SetPosition_cmbPersonnelControls_ExceptionShifts();
    if (cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts = true;
        SetPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts(0);
    }
}

function CallBack_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onBeforeCallback() {
    cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts.dispose();
}

function CallBack_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onCallbackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnName_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts').value;
    document.getElementById('clmnBarCode_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts').value;
    document.getElementById('clmnCardNum_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts').value;

    SetPosition_cmbPersonnelControls_ExceptionShifts();

    var error = document.getElementById('ErrorHiddenField_Personnel1_TwoPersonnelReplacement_ExceptionShifts').value;
    if (error == "") {
        document.getElementById('cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_DropImage').mousedown();
        else
            cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_DropDown').style.display = 'none';
    }
}

function CallBack_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onCallbackError(sender, e) {
    ShowConnectionError_ExceptionShifts();
}

function tlbItemSearch_TlbSearchPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    LoadState_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts = 'Search';
    SetPageIndex_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch1_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    LoadState_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts = 'AdvancedSearch';
    CurrentAdvancedSearchState_TwoPersonnelReplacement_ExceptionShifts = 'First';
    ShowDialogPersonnelSearch('ExceptionShifts');
}

function tlbItemRefresh_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    Refresh_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts();
}

function Refresh_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts() {
    LoadState_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts = 'Normal';
    SetPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts(0);
}

function tlbItemFirst_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    SetPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts(0);
}

function tlbItemBefore_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    if (CurrentPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts != 0) {
        CurrentPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts = CurrentPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts - 1;
        SetPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts(CurrentPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts);
    }
}

function tlbItemNext_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    if (CurrentPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts < parseInt(document.getElementById('hfPersonnelPageCount_Personnel2_TwoPersonnelReplacement_ExceptionShifts').value) - 1) {
        CurrentPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts = CurrentPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts + 1;
        SetPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts(CurrentPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts);
    }
}

function tlbItemLast_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    SetPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts(parseInt(document.getElementById('hfPersonnelPageCount_Personnel2_TwoPersonnelReplacement_ExceptionShifts').value) - 1);
}

function SetPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts(pageIndex) {
    CurrentPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts = pageIndex;
    Fill_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts(pageIndex);
}

function Fill_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts(pageIndex) {
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_Personnel2_TwoPersonnelReplacement_ExceptionShifts').value);
    var SearchTermConditions = '';
    switch (LoadState_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts = SearchTermConditions = document.getElementById('txtPersonnelSearch2_TwoPersonnelReplacement_ExceptionShifts').value;
            break;
        case 'AdvancedSearch':
            SearchTermConditions = AdvancedSearchTerm_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts;
            break;
    }
    CallBack_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts.callback(CharToKeyCode_ExceptionShifts(LoadState_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts), CharToKeyCode_ExceptionShifts(pageSize.toString()), CharToKeyCode_ExceptionShifts(pageIndex.toString()), CharToKeyCode_ExceptionShifts(SearchTermConditions));
}

function cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onExpand(sender, e) {
    CollapseControls_ExceptionShifts(cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts);
    if (cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts = true;
        SetPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts(0);
    }
}

function CallBack_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onBeforeCallback(sender, e) {
    cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts.dispose();
}

function CallBack_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onCallbackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnName_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts').value;
    document.getElementById('clmnBarCode_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts').value;
    document.getElementById('clmnCardNum_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts').value;

    SetPosition_cmbPersonnelControls_ExceptionShifts();

    var error = document.getElementById('ErrorHiddenField_Personnel2_TwoPersonnelReplacement_ExceptionShifts').value;
    if (error == "") {
        document.getElementById('cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_DropImage').mousedown();
        else
            cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts.expand();
        var personnelCount = document.getElementById('hfPersonnelCount_Personnel2_TwoPersonnelReplacement_ExceptionShifts').value;
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_DropDown').style.display = 'none';
    }
}

function CallBack_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onCallbackError(sender, e) {
    ShowConnectionError_ExceptionShifts();
}

function tlbItemSearch_TlbSearchPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    LoadState_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts = 'Search';
    SetPageIndex_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch2_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    LoadState_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts = 'AdvancedSearch';
    CurrentAdvancedSearchState_TwoPersonnelReplacement_ExceptionShifts = 'Second';
    ShowDialogPersonnelSearch('ExceptionShifts');
}

function tlbItemView_TlbFirstDateView_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    CurrentShiftViewState_ExceptionShifts = 'First';
    UpdateExceptionShift_ExceptionShifts();
}

function tlbItemView_TlbSecondDateView_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    CurrentShiftViewState_ExceptionShifts = 'Second';
    UpdateExceptionShift_ExceptionShifts();
}

function rdbWorkGroup_TwoDayReplacement_ExceptionShifts_onClick() {
    if (document.getElementById('rdbWorkGroup_TwoDayReplacement_ExceptionShifts').checked) {
        CurrentReplacementState_ExceptionShifs = 'WorkGroup';
        if(box_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts_IsShown)
           SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts_onShowHide();
        ChangeControlsEnabled_ExceptionShifts('WorkGroup');
    }
}

function rdbPersonnel_TwoDayReplacement_ExceptionShifts_onClick() {
    if (document.getElementById('rdbPersonnel_TwoDayReplacement_ExceptionShifts').checked) {
        CurrentReplacementState_ExceptionShifs = 'Personnel';
        ChangeControlsEnabled_ExceptionShifts('Personnel');
    }
}

function rdbWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    if (document.getElementById('rdbWorkGroup_TwoPersonnelReplacement_ExceptionShifts').checked) {
        CurrentReplacementState_ExceptionShifs = 'WorkGroup';
        if(box_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts_IsShown)
           SearchByPersonnel1Box_TwoPersonnelReplacement_ExceptionShifts_onShowHide();
        if(box_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts_IsShown)
           SearchByPersonnel2Box_TwoPersonnelReplacement_ExceptionShifts_onShowHide();
        ChangeControlsEnabled_ExceptionShifts('WorkGroup');
    }
}

function rdbPersonnel_TwoPersonnelReplacement_ExceptionShifts_onClick() {
    if (document.getElementById('rdbPersonnel_TwoPersonnelReplacement_ExceptionShifts').checked) {
        CurrentReplacementState_ExceptionShifs = 'Personnel';
        ChangeControlsEnabled_ExceptionShifts('Personnel');
    }
}

function ChangeControlsEnabled_ExceptionShifts(state) {
    var ObjDialogExceptionShifts = parent.DialogExceptionShifts.get_value();
    var formState = ObjDialogExceptionShifts.State;
    switch (formState) {
        case 'TwoDayReplacement':
            switch (state) {
                case 'WorkGroup':
                    cmbWorkGroup_TwoDayReplacement_ExceptionShifts.enable();
                    document.getElementById('imgbox_SearchByPersonnel_TwoDayReplacement_ExceptionShifts').onclick = '';
                    break;
                case 'Personnel':
                    cmbWorkGroup_TwoDayReplacement_ExceptionShifts.disable();
                    document.getElementById('imgbox_SearchByPersonnel_TwoDayReplacement_ExceptionShifts').onclick = function () {
                       imgbox_SearchByPersonnel_TwoDayReplacement_ExceptionShifts_onClick();
                    };
                    break;
            }
            break;
        case 'TwoPersonnelReplacement':
            switch (state) {
                case 'WorkGroup':
                    cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.enable();
                    cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.enable();
                    document.getElementById('imgbox_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts').onclick = '';
                    document.getElementById('imgbox_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts').onclick = '';
                    break;
                case 'Personnel':
                    cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.disable();
                    cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.disable();
                    document.getElementById('imgbox_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts').onclick = function () {
                        imgbox_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onClick();
                    };
                    document.getElementById('imgbox_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts').onclick = function () {
                        imgbox_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onClick();
                    };
                    break;
            }
            break;
    }
}

function UpdateExceptionShift_ExceptionShifts() {
    var ObjDialogExceptionShifts = parent.DialogExceptionShifts.get_value();
    ObjExceptionShift_ExceptionShifts = new Object();
    ObjExceptionShift_ExceptionShifts.State = ObjDialogExceptionShifts.State;
    ObjExceptionShift_ExceptionShifts.ShiftID = '0';
    ObjExceptionShift_ExceptionShifts.FirstPersonnelID = '0';
    ObjExceptionShift_ExceptionShifts.SecondPersonnelID = '0';
    ObjExceptionShift_ExceptionShifts.FirstWorkGroupID = '0';
    ObjExceptionShift_ExceptionShifts.SecondWorkGroupID = '0';
    ObjExceptionShift_ExceptionShifts.FirstDate = null;
    ObjExceptionShift_ExceptionShifts.SecondDate = null;
    ObjExceptionShift_ExceptionShifts.ReplacementState = 'NotReplacement';
    ObjExceptionShift_ExceptionShifts.ShiftViewState = 'NotView';
    switch (ObjExceptionShift_ExceptionShifts.State) {
        case 'TwoDayReplacement':
            if (CurrentReplacementState_ExceptionShifs == 'WorkGroup' && cmbWorkGroup_TwoDayReplacement_ExceptionShifts.getSelectedItem() != undefined)
                ObjExceptionShift_ExceptionShifts.FirstWorkGroupID = cmbWorkGroup_TwoDayReplacement_ExceptionShifts.getSelectedItem().get_value();
            if (CurrentReplacementState_ExceptionShifs == 'Personnel' && cmbPersonnel_TwoDayReplacement_ExceptionShifts.getSelectedItem() != undefined)
                ObjExceptionShift_ExceptionShifts.FirstPersonnelID = cmbPersonnel_TwoDayReplacement_ExceptionShifts.getSelectedItem().get_id();
            switch (parent.SysLangID) {
                case 'fa-IR':
                    ObjExceptionShift_ExceptionShifts.FirstDate = document.getElementById('pdpFromDate_TwoDayReplacement_ExceptionShifts').value;
                    ObjExceptionShift_ExceptionShifts.SecondDate = document.getElementById('pdpToDate_TwoDayReplacement_ExceptionShifts').value;
                    break;
                case 'en-US':
                    ObjExceptionShift_ExceptionShifts.FirstDate = document.getElementById('gdpFromDate_TwoDayReplacement_ExceptionShifts_Input').value;
                    ObjExceptionShift_ExceptionShifts.SecondDate = document.getElementById('gdpToDate_TwoDayReplacement_ExceptionShifts_Input').value;
                    break;
            }
            ObjExceptionShift_ExceptionShifts.ReplacementState = CurrentReplacementState_ExceptionShifs;
            ObjExceptionShift_ExceptionShifts.ShiftViewState = CurrentShiftViewState_ExceptionShifts;
            break;
        case 'TwoPersonnelReplacement':
            if (CurrentReplacementState_ExceptionShifs == 'WorkGroup') {
                if (cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.getSelectedItem() != undefined)
                    ObjExceptionShift_ExceptionShifts.FirstWorkGroupID = cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.getSelectedItem().get_value();
                if (cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.getSelectedItem() != undefined)
                    ObjExceptionShift_ExceptionShifts.SecondWorkGroupID = cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts.getSelectedItem().get_value();
            }
            if (CurrentReplacementState_ExceptionShifs == 'Personnel') {
                if (cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts.getSelectedItem() != undefined)
                    ObjExceptionShift_ExceptionShifts.FirstPersonnelID = cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts.getSelectedItem().get_id();
                if (cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts.getSelectedItem() != undefined)
                    ObjExceptionShift_ExceptionShifts.SecondPersonnelID = cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts.getSelectedItem().get_id();
            }
            switch (parent.SysLangID) {
                case 'fa-IR':
                    ObjExceptionShift_ExceptionShifts.FirstDate = document.getElementById('pdpTwoPersonnelReplacement1_ExceptionShifts').value;
                    ObjExceptionShift_ExceptionShifts.SecondDate = document.getElementById('pdpTwoPersonnelReplacement2_ExceptionShifts').value;
                    break;
                case 'en-US':
                    ObjExceptionShift_ExceptionShifts.FirstDate = document.getElementById('gdpTwoPersonnelReplacement1_ExceptionShifts_Input').value;
                    ObjExceptionShift_ExceptionShifts.SecondDate = document.getElementById('gdpTwoPersonnelReplacement2_ExceptionShifts').value;
                    break;
            }
            ObjExceptionShift_ExceptionShifts.ReplacementState = CurrentReplacementState_ExceptionShifs;
            ObjExceptionShift_ExceptionShifts.ShiftViewState = CurrentShiftViewState_ExceptionShifts;
            break;
        default:
            ObjExceptionShift_ExceptionShifts.FirstPersonnelID = ObjDialogExceptionShifts.PersonnelID != undefined ? ObjDialogExceptionShifts.PersonnelID : '0';
            if (cmbShift_ExceptionShifts.getSelectedItem() != undefined)
                ObjExceptionShift_ExceptionShifts.ShiftID = cmbShift_ExceptionShifts.getSelectedItem().get_value();
            else
                ObjExceptionShift_ExceptionShifts.ShiftID = ObjDialogExceptionShifts.ShiftID != undefined ? ObjDialogExceptionShifts.ShiftID : '0';
            switch (parent.SysLangID) {
                case 'fa-IR':
                    ObjExceptionShift_ExceptionShifts.FirstDate = document.getElementById('pdpDetailsFromDate_ExceptionShifts').value;
                    ObjExceptionShift_ExceptionShifts.SecondDate = document.getElementById('pdpDetailsToDate_ExceptionShifts').value;
                    break;
                case 'en-US':
                    ObjExceptionShift_ExceptionShifts.FirstDate = document.getElementById('gdpDetailsFromDate_ExceptionShifts_Input').value;
                    ObjExceptionShift_ExceptionShifts.SecondDate = document.getElementById('gdpDetailsToDate_ExceptionShifts_Input').value;
                    break;
            }
            break;
    }
    UpdateExceptionShift_ExceptionShiftsPage(CharToKeyCode_ExceptionShifts(ObjExceptionShift_ExceptionShifts.State), CharToKeyCode_ExceptionShifts(ObjExceptionShift_ExceptionShifts.ShiftID), CharToKeyCode_ExceptionShifts(ObjExceptionShift_ExceptionShifts.FirstPersonnelID), CharToKeyCode_ExceptionShifts(ObjExceptionShift_ExceptionShifts.SecondPersonnelID), CharToKeyCode_ExceptionShifts(ObjExceptionShift_ExceptionShifts.FirstWorkGroupID), CharToKeyCode_ExceptionShifts(ObjExceptionShift_ExceptionShifts.SecondWorkGroupID), CharToKeyCode_ExceptionShifts(ObjExceptionShift_ExceptionShifts.FirstDate), CharToKeyCode_ExceptionShifts(ObjExceptionShift_ExceptionShifts.SecondDate), CharToKeyCode_ExceptionShifts(ObjExceptionShift_ExceptionShifts.ReplacementState), CharToKeyCode_ExceptionShifts(ObjExceptionShift_ExceptionShifts.ShiftViewState));
    DialogWaiting.Show();
}

function UpdateExceptionShift_ExceptionShiftsPage_onCallBack(Response) { 
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_ExceptionShifts').value;
            Response[1] = document.getElementById('hfConnectionError_ExceptionShifts').value;
        }
    }
    if (RetMessage[2] == 'success') {
        if (ObjExceptionShift_ExceptionShifts != null && ObjExceptionShift_ExceptionShifts.ReplacementState != 'NotReplacement' && ObjExceptionShift_ExceptionShifts.ShiftViewState != 'NotView') {
            var ShiftName = Response[3];
            SetRelativeShift_ExceptionShifts(ShiftName);
            return;
        }
        parent.document.getElementById('pgvExceptionShiftsIntroduction_iFrame').contentWindow.ExceptionShift_OnAfterUpdate(RetMessage);
        CloseDialogExceptionShifts();
    }
    else
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
}

function SetRelativeShift_ExceptionShifts(ShiftName) {
    var State = ObjExceptionShift_ExceptionShifts.State;
    var ShiftViewState = ObjExceptionShift_ExceptionShifts.ShiftViewState;
    var TargetShiftBoxID = null;
    switch (State) {
        case 'TwoDayReplacement':
            switch (ShiftViewState) {
                case 'First':
                    TargetShiftBoxID = 'txtFirstDateShiftName_TwoDayReplacement_ExceptionShifts';
                    break;
                case 'Second':
                    TargetShiftBoxID = 'txtSecondDateShiftName_TwoDayReplacement_ExceptionShifts';
                    break;
            }
            break;
        case 'TwoPersonnelReplacement':
            switch (ShiftViewState) {
                case 'First':
                    TargetShiftBoxID = 'txtFirstDateShiftName_TwoPersonnelReplacement_ExceptionShifts';
                    break;
                case 'Second':
                    TargetShiftBoxID = 'txtSecondDateShiftName_TwoPersonnelReplacement_ExceptionShifts';
                    break;
            }
            break;
    }
    document.getElementById(TargetShiftBoxID).value = ShiftName;
}

function NavigateExceptionShift_ExceptionShifts() {
    var ObjDialogExceptionShifts = parent.DialogExceptionShifts.get_value();
    var State = ObjDialogExceptionShifts.State;
    if (State == 'Edit') {
        if (ObjDialogExceptionShifts.ShiftID != undefined && ObjDialogExceptionShifts.ShiftName != undefined)
            document.getElementById('cmbShift_ExceptionShifts_Input').value = ObjDialogExceptionShifts.ShiftName;
        switch (parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpDetailsFromDate_ExceptionShifts').value = document.getElementById('pdpDetailsToDate_ExceptionShifts').value = ObjDialogExceptionShifts.Date;
                break;
            case 'en-US':
                var date = new Date(ObjDialogExceptionShifts.Date);
                gdpDetailsFromDate_ExceptionShifts.setSelectedDate(date);
                gCalDetailsFromDate_ExceptionShifts.setSelectedDate(date);
                gdpDetailsToDate_ExceptionShifts.setSelectedDate(date);
                gCalDetailsToDate_ExceptionShifts.setSelectedDate(date);
                break;
        }
    }
}

function SetActionMode_ExceptionShifts() {
    var ObjDialogExceptionShifts = parent.DialogExceptionShifts.get_value();
    var state = ObjDialogExceptionShifts.State;
    if(state == 'Add' || state == 'Edit')
        document.getElementById('ActionMode_Details_ExceptionShifts').innerHTML = document.getElementById("hf" + state + "_ExceptionShifts").value;
}

function tlbItemFormReconstruction_TlbExceptionShiftsDetails_onClick() {
    ReconstructFrom_ExceptionShifts();
}

function tlbItemFormReconstruction_TlbTwoDayReplacement_onClick() {
    ReconstructFrom_ExceptionShifts();
}

function tlbItemFormReconstruction_TlbTwoPersonnelReplacement_onClick() {
    ReconstructFrom_ExceptionShifts();
}

function ReconstructFrom_ExceptionShifts() {
    var ObjDialogExceptionShifts = parent.DialogExceptionShifts.get_value();
    var State = ObjDialogExceptionShifts.State;
    CloseDialogExceptionShifts();
    parent.document.getElementById('pgvExceptionShiftsIntroduction_iFrame').contentWindow.ChangePageState_MasterExceptionShifts(State);
}

function cmbPersonnel_TwoDayReplacement_ExceptionShifts_onChange(sender, e) {
    if (cmbPersonnel_TwoDayReplacement_ExceptionShifts.getSelectedItem() != undefined)
        document.getElementById('tdSelectedPersonnel_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts').innerHTML = cmbPersonnel_TwoDayReplacement_ExceptionShifts.getSelectedItem().get_text();
}

function cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onChange(sender, e) {
    if (cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts.getSelectedItem() != undefined)
        document.getElementById('tdSelectedPersonnel_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts').innerHTML = cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts.getSelectedItem().get_text();
}

function cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onChange(sender, e) {
    if (cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts.getSelectedItem() != undefined)
        document.getElementById('tdSelectedPersonnel_SearchByPersonnelBox2_TwoPersonnelReplacement_ExceptionShifts').innerHTML = cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts.getSelectedItem().get_text();
}















