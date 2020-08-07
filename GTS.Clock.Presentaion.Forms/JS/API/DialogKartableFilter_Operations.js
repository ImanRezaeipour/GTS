
var FilterValueIsNullMessage = '';
var StrFilterConditions = '';
var CurrentEditingRow_GridCombinationalConditions_KartableFilter = '';
var ArOperators = new Array();
var ConditionLimitationCount_KartableFilter = 10;

function GetBoxesHeaders_KartableFilter() {
    GetBoxesHeaders_KartableFilterPage();
    DialogWaiting.Show();
}


function GetBoxesHeaders_KartableFilterPage_onCallBack(Response) {
    DialogWaiting.Close();
    parent.document.getElementById('Title_DialogKartableFilter').innerHTML = Response[0];
    document.getElementById('header_CombinationalConditions_KartableFilter').innerHTML = Response[1];
    FilterValueIsNullMessage = Response[2];
    ArOperators[0] = Response[3];
    ArOperators[1] = Response[4];
}

function btn_gdpDate_KartableFilter_OnMouseUp(event) {
    if (gCalDate_KartableFilter.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpDate_KartableFilter_OnDateChange(sender, e) {
    var Date = gdpDate_KartableFilter.getSelectedDate();
    gCalDate_KartableFilter.setSelectedDate(Date);
}

function btn_gdpDate_KartableFilter_OnClick(event) {
    if (gCalDate_KartableFilter.get_popUpShowing()) {
        gCalDate_KartableFilter.hide();
    }
    else {
        gCalDate_KartableFilter.setSelectedDate(gdpDate_KartableFilter.getSelectedDate());
        gCalDate_KartableFilter.show();
    }
}

function gCalDate_KartableFilter_OnChange(sender, e) {
    var Date = gCalDate_KartableFilter.getSelectedDate();
    gdpDate_KartableFilter.setSelectedDate(Date);
}

function gCalDate_KartableFilter_onLoad(sender, e) {
    window.gCalDate_KartableFilter.PopUpObject.z = 25000000;
}

function ViewCurrentLangCalendars_KartableFilter() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpDate_KartableFilter").parentNode.removeChild(document.getElementById("pdpDate_KartableFilter"));
            document.getElementById("pdpDate_KartableFilterimgbt").parentNode.removeChild(document.getElementById("pdpDate_KartableFilterimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_DateCalendars_KartableFilter").removeChild(document.getElementById("Container_gCalDate_KartableFilter"));
            break;
    }
}

function SetButtonImages_TimeSelector_KartableFilter() {
    document.getElementById('TimeSelector_Hour_KartableFilter_imgUp').src = "images/TimeSelector/CustomUp.gif";
    document.getElementById('TimeSelector_Hour_KartableFilter_imgDown').src = "images/TimeSelector/CustomDown.gif";
    document.getElementById('TimeSelector_Hour_KartableFilter_imgUp').onmouseover = function () {
        document.getElementById('TimeSelector_Hour_KartableFilter_imgUp').src = "images/TimeSelector/oie_CustomUp.gif";
        FocusOnCurrentTimeSelector('TimeSelector_Hour_KartableFilter');
    }
    document.getElementById('TimeSelector_Hour_KartableFilter_imgDown').onmouseover = function () {
        document.getElementById('TimeSelector_Hour_KartableFilter_imgDown').src = "images/TimeSelector/oie_CustomDown.gif";
        FocusOnCurrentTimeSelector('TimeSelector_Hour_KartableFilter');
    }
    document.getElementById('TimeSelector_Hour_KartableFilter_imgUp').onmouseout = function () {
        document.getElementById('TimeSelector_Hour_KartableFilter_imgUp').src = "images/TimeSelector/CustomUp.gif";
    }
    document.getElementById('TimeSelector_Hour_KartableFilter_imgDown').onmouseout = function () {
        document.getElementById('TimeSelector_Hour_KartableFilter_imgDown').src = "images/TimeSelector/CustomDown.gif";
    }
}


function FocusOnCurrentTimeSelector(TimeSelector) {
    if (document.activeElement.id != "" + TimeSelector + "_txtHour" && document.activeElement.id != "" + TimeSelector + "_txtMinute" && document.activeElement.id != "" + TimeSelector + "_txtSecond")
        document.getElementById("" + TimeSelector + "_txtHour").focus();
}

function cmbFilterField_KartableFilter_onChange(sender, e) {
    var val = cmbFilterField_KartableFilter.getSelectedItem().get_value();
    CallBack_cmbOperator_KartableFilter.callback(val);
    cmbFilterField_KartableFilter.collapse();
    SelectRelativeTab_TabStripFilterTerms_KartableFilter(val);
}

function SelectRelativeTab_TabStripFilterTerms_KartableFilter(val) {
    var tbID = "tb" + val + "_TabStripFilterTerms";
    var tbCollection_TabStripFilterTerms = TabStripFilterTerms.get_tabs();
    tbCollection_TabStripFilterTerms.getTabById(tbID).select();
    var tbColArray_TabStripFilterTerms = tbCollection_TabStripFilterTerms.get_tabArray();
    for (var i = 0; i < tbColArray_TabStripFilterTerms.length; i++) {
        var tb = tbColArray_TabStripFilterTerms[i];
       var tbEnabled = true;
       if (tb.get_id() != tbID)
           tbEnabled = false;
       tb.set_enabled(tbEnabled);
   }}

function InterpolationConditions_KartableFilter() {
    if (ValidateFilterValue_KartableFilter()) {
        InsertCondition_GridCombinationalConditions_KartableFilter();
    }
    else {
    }
}

function CheckFilterConditionsCount_KartableFilter() {
    var count = GridCombinationalConditions_KartableFilter.get_table().getRowCount();

    var InterpolationEnabled = '';
    var InterpolationImage = '';

    if (count == ConditionLimitationCount_KartableFilter) {
        InterpolationEnabled = false;
        InterpolationImage = 'add_silver.png';
    }
    else {
        InterpolationEnabled = true;
        InterpolationImage = 'add.png';
    }


    TlbInterpolation_KartableFilter.beginUpdate();
    TlbInterpolation_KartableFilter.get_items().getItemById('tlbItemInterpolation_TlbInterpolation_KartableFilter').set_enabled(InterpolationEnabled);
    TlbInterpolation_KartableFilter.get_items().getItemById('tlbItemInterpolation_TlbInterpolation_KartableFilter').set_imageUrl(InterpolationImage);
    TlbInterpolation_KartableFilter.endUpdate();
}

function InsertCondition_GridCombinationalConditions_KartableFilter() {
    FilterField = cmbFilterField_KartableFilter.getSelectedItem().get_value();
    var FilterOperator = cmbOperator_KartableFilter.getSelectedItem().get_value();
    var FilterCondition = null;
    var ConditionOperator = 'And';
    switch (FilterField) {
        case 'Date':
            switch (parent.parent.CurrentLangID) {
                case 'fa-IR':
                    FilterCondition = document.getElementById('pdpDate_KartableFilter').value;
                    break;
                case 'en-US':
                    break;
            }
            break;
        case 'Selective':
            FilterCondition = cmbSelective_KartableFilter.getSelectedItem().get_text();
            break;
        case 'Time':
            FilterCondition = GetSelectedTime_KartableFilter();
            break;
        case 'String':
            FilterCondition = document.getElementById('txtString_KartableFilter').value;
    }

    var Splitter = null;
    if (StrFilterConditions == '')
        Splitter = '';
    else
        Splitter = '%';
    StrFilterConditions = StrFilterConditions + Splitter + GetConditionID_KartableFilter() + '@' + FilterField + '@' + FilterOperator + '@' + FilterCondition + '@' + ConditionOperator;
    CallBack_GridCombinationalConditions_KartableFilter.callback(StrFilterConditions);
}

function GetConditionID_KartableFilter() { 
    var ID = '';
    if (StrFilterConditions == '')
        ID = 1;
    else {
            var Conditions = StrFilterConditions.split('%');
            var ID = parseInt(Conditions[Conditions.length - 1].split('@')[0]) + 1;
    }
    return ID.toString();
}


function GetSelectedTime_KartableFilter() {
    var TimeSelector = 'TimeSelector_Hour_KartableFilter';
    var SelectedTime_KartableFilter = document.getElementById("" + TimeSelector + "_txtHour").value + ':' + document.getElementById("" + TimeSelector + "_txtMinute").value + ':' + document.getElementById("" + TimeSelector + "_txtSecond").value;
    return SelectedTime_KartableFilter;
}


function GridCombinationalConditions_KartableFilter_onItemDoubleClick(sender, e) {
    var SelectdRowID_GridCombinationalConditions_KartableFilter = e.get_item().getMember("ID").get_text();
    if (SelectdRowID_GridCombinationalConditions_KartableFilter != CurrentEditingRow_GridCombinationalConditions_KartableFilter) {
        CurrentEditingRow_GridCombinationalConditions_KartableFilter = SelectdRowID_GridCombinationalConditions_KartableFilter;
        GridCombinationalConditions_KartableFilter.edit(e.get_item());
    }
    else
        GridCombinationalConditions_KartableFilter.editComplete();
}


function ValidateFilterValue_KartableFilter() {
    var val = cmbFilterField_KartableFilter.getSelectedItem().get_value();
    IsValid = true;
    switch (val) {
        case 'Selective':
            if (cmbSelective_KartableFilter.get_selectedIndex() == -1)
                IsValid = false;
            break;
        case 'String':
            if (document.getElementById('txtString_KartableFilter').value == '')
                IsValid = false;
    }
    return IsValid;
}

function GetCurrentDateTime_KartableFilter() {
    GetCurrentDateTime_KartableFilterPage();
    DialogWaiting.Show();
}

function GetCurrentDateTime_KartableFilterPage_onCallBack(Response) {
    DialogWaiting.Close();
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById("pdpDate_KartableFilter").value = Response;
            break;
    }
}

function getFilter_KartableFilter(DataItem) {
    var cmbInterpolationOperator_KartableFilter = document.getElementById('GridCombinationalConditions_KartableFilter_EditTemplate_0_2_cmbInterpolationOperator_KartableFilter');
    if (cmbInterpolationOperator_KartableFilter.selectedIndex == -1)
        return null;
    UpdateCondition_KartableFilter(DataItem, cmbInterpolationOperator_KartableFilter.value);
    return [cmbInterpolationOperator_KartableFilter.value, cmbInterpolationOperator_KartableFilter.options[cmbInterpolationOperator_KartableFilter.selectedIndex].text];    
}

function setFilter_KartableFilter(DataItem) {
    var cmbInterpolationOperator_KartableFilter = document.getElementById('GridCombinationalConditions_KartableFilter_EditTemplate_0_2_cmbInterpolationOperator_KartableFilter');
    for (var i = 0; i < cmbInterpolationOperator_KartableFilter.length; i++) {
        if (cmbInterpolationOperator_KartableFilter.options[i].text == DataItem.getMember('ConditionOperator').get_text()) {
            cmbInterpolationOperator_KartableFilter.selectedIndex = i;
            break;
        }
    }
}

function UpdateCondition_KartableFilter(DataItem, ConditionOperator) {
    if (StrFilterConditions != '') {
        var RetStrFilterConditions = '';        
        var ID = DataItem.getMember("ID").get_text();
        var Conditions = StrFilterConditions.split('%');
        for (var i = 0; i < Conditions.length; i++) {
            var Splitter = '';
            var ConditionProps = Conditions[i].split('@');
            if (ConditionProps[0] == ID) {
                ConditionProps[4] = ConditionOperator;
                var RetConditionProps = '';
                for (var j = 0; j < ConditionProps.length; j++) {
                    var ConditionSplitter = '';
                    if (j == ConditionProps.length - 1)
                        ConditionSplitter = '';
                    else
                        ConditionSplitter = '@';
                    RetConditionProps = RetConditionProps + ConditionProps[j] + ConditionSplitter;
                }
                Conditions[i] = RetConditionProps;
            }
            if (i == 0)
                Splitter = '';
            else
                Splitter = '%';
            RetStrFilterConditions = RetStrFilterConditions + Splitter + Conditions[i];
        }
        StrFilterConditions = RetStrFilterConditions;
    }
}

function ApplyConditions_KartableFilter() {
    if (StrFilterConditions == '')
        InterpolationConditions_KartableFilter();
    PushFilterConditions_KartableFilter();
    parent.DialogKartableFilter.Close();
}

function PushFilterConditions_KartableFilter() {
    parent.parent.document.getElementById('DialogKartable_IFrame').contentWindow.Kartable_OnAfterCustomFilter(StrFilterConditions);
}

function RemoveCondition_KartableFilter(RowID) {
    if (StrFilterConditions != '') {
        if (RowID != 'All') {
            var GridItem = GridCombinationalConditions_KartableFilter.getItemFromClientId(RowID);
            var ID = GridItem.getMember("ID").get_text();
            var RetStrFilterConditions = '';
            var Conditions = StrFilterConditions.split('%');
            for (var i = 0; i < Conditions.length; i++) {
                if (parseInt(Conditions[i].split('@')[0]) != ID) {
                    var Splitter = '';
                    if (i == 0 || RetStrFilterConditions == '')
                        Splitter = '';
                    else {
                        Splitter = '%';
                    }
                    RetStrFilterConditions = RetStrFilterConditions + Splitter + Conditions[i];
                }
            }
            StrFilterConditions = RetStrFilterConditions;
            GridCombinationalConditions_KartableFilter.beginUpdate();
            GridCombinationalConditions_KartableFilter.deleteItem(GridItem);
            GridCombinationalConditions_KartableFilter.endUpdate();
        }
        else {
            StrFilterConditions = '';
            GridCombinationalConditions_KartableFilter.get_table().clearData();
        }
        CheckFilterConditionsCount_KartableFilter();
        PushFilterConditions_KartableFilter();
    }
}

function CallBack_GridCombinationalConditions_KartableFilter_onCallbackComplete(sender, e) {
    var cmbInterpolationOperator_KartableFilter = document.getElementById('GridCombinationalConditions_KartableFilter_EditTemplate_0_2_cmbInterpolationOperator_KartableFilter');
    if (cmbInterpolationOperator_KartableFilter.options.length > 0) {
        for (var i = 0; i < cmbInterpolationOperator_KartableFilter.options.length; i++) {
            var OperatorProps = ArOperators[i].split(':');
            if (OperatorProps[0] == cmbInterpolationOperator_KartableFilter.options[i].value)
                cmbInterpolationOperator_KartableFilter.options[i].text = OperatorProps[1];
        }
    }
    CheckFilterConditionsCount_KartableFilter();
}

function SetStrFilterCondition_onLoad() {
    StrFilterConditions = parent.parent.DialogKartable.StrFilterConditions;
    if(StrFilterConditions != '')
       CallBack_GridCombinationalConditions_KartableFilter.callback(StrFilterConditions);
}

function CallBack_GridCombinationalConditions_KartableFilter_onCallbackError(sender, e) {
    ShowConnectionError_KartableFilter();
}

function ShowConnectionError_KartableFilter() {
    var error = document.getElementById('hfErrorType_KartableFilter').value;
    var errorBody = document.getElementById('hfConnectionError_KartableFilter').value;
    showDialog(error, errorBody, 'error');
}


























