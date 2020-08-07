
var FilterValueIsNullMessage = '';
var StrFilterConditions = '';
var CurrentEditingRow_GridCombinationalConditions_CartableFilter = '';
var ArOperators = new Array();
var ConditionLimitationCount_CartableFilter = 10;

function GetBoxesHeaders_CartableFilter() {
    GetBoxesHeaders_CartableFilterPage();
}


function GetBoxesHeaders_CartableFilterPage_onCallBack(Response) {
    parent.document.getElementById('Title_DialogCartableFilter').innerHTML = Response[0];
    document.getElementById('header_CombinationalConditions_CartableFilter').innerHTML = Response[1];
    FilterValueIsNullMessage = Response[2];
    ArOperators[0] = Response[3];
    ArOperators[1] = Response[4];
}

function btn_gdpDate_CartableFilter_OnMouseUp(event) {
    if (gCalDate_CartableFilter.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpDate_CartableFilter_OnDateChange(sender, e) {
    var Date = gdpDate_CartableFilter.getSelectedDate();
    gCalDate_CartableFilter.setSelectedDate(Date);
}

function btn_gdpDate_CartableFilter_OnClick(event) {
    if (gCalDate_CartableFilter.get_popUpShowing()) {
        gCalDate_CartableFilter.hide();
    }
    else {
        gCalDate_CartableFilter.setSelectedDate(gdpDate_CartableFilter.getSelectedDate());
        gCalDate_CartableFilter.show();
    }
}

function gCalDate_CartableFilter_OnChange(sender, e) {
    var Date = gCalDate_CartableFilter.getSelectedDate();
    gdpDate_CartableFilter.setSelectedDate(Date);
}

function gCalDate_CartableFilter_onLoad(sender, e) {
    window.gCalDate_CartableFilter.PopUpObject.z = 25000000;
}

function ViewCurrentLangCalendars_CartableFilter() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpDate_CartableFilter").parentNode.removeChild(document.getElementById("pdpDate_CartableFilter"));
            document.getElementById("pdpDate_CartableFilterimgbt").parentNode.removeChild(document.getElementById("pdpDate_CartableFilterimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_DateCalendars_CartableFilter").removeChild(document.getElementById("Container_gCalDate_CartableFilter"));
            break;
    }
}

function SetButtonImages_TimeSelector_CartableFilter() {
    document.getElementById('TimeSelector_Hour_CartableFilter_imgUp').src = "images/TimeSelector/CustomUp.gif";
    document.getElementById('TimeSelector_Hour_CartableFilter_imgDown').src = "images/TimeSelector/CustomDown.gif";
    document.getElementById('TimeSelector_Hour_CartableFilter_imgUp').onmouseover = function () {
        document.getElementById('TimeSelector_Hour_CartableFilter_imgUp').src = "images/TimeSelector/oie_CustomUp.gif";
        FocusOnCurrentTimeSelector('TimeSelector_Hour_CartableFilter');
    }
    document.getElementById('TimeSelector_Hour_CartableFilter_imgDown').onmouseover = function () {
        document.getElementById('TimeSelector_Hour_CartableFilter_imgDown').src = "images/TimeSelector/oie_CustomDown.gif";
        FocusOnCurrentTimeSelector('TimeSelector_Hour_CartableFilter');
    }
    document.getElementById('TimeSelector_Hour_CartableFilter_imgUp').onmouseout = function () {
        document.getElementById('TimeSelector_Hour_CartableFilter_imgUp').src = "images/TimeSelector/CustomUp.gif";
    }
    document.getElementById('TimeSelector_Hour_CartableFilter_imgDown').onmouseout = function () {
        document.getElementById('TimeSelector_Hour_CartableFilter_imgDown').src = "images/TimeSelector/CustomDown.gif";
    }
}


function FocusOnCurrentTimeSelector(TimeSelector) {
    if (document.activeElement.id != "" + TimeSelector + "_txtHour" && document.activeElement.id != "" + TimeSelector + "_txtMinute" && document.activeElement.id != "" + TimeSelector + "_txtSecond")
        document.getElementById("" + TimeSelector + "_txtHour").focus();
}

function cmbFilterField_CartableFilter_onChange(sender, e) {
    var val = cmbFilterField_CartableFilter.getSelectedItem().get_value();
    CallBack_cmbOperator_CartableFilter.callback(val);
    cmbFilterField_CartableFilter.collapse();
    SelectRelativeTab_TabStripFilterTerms_CartableFilter(val);
}

function SelectRelativeTab_TabStripFilterTerms_CartableFilter(val) {
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

function InterpolationConditions_CartableFilter() {
    if (ValidateFilterValue_CartableFilter()) {
        InsertCondition_GridCombinationalConditions_CartableFilter();
    }
    else {
    }
}

function CheckFilterConditionsCount_CartableFilter() {
    var count = GridCombinationalConditions_CartableFilter.get_table().getRowCount();

    var InterpolationEnabled = '';
    var InterpolationImage = '';

    if (count == ConditionLimitationCount_CartableFilter) {
        InterpolationEnabled = false;
        InterpolationImage = 'add_silver.png';
    }
    else {
        InterpolationEnabled = true;
        InterpolationImage = 'add.png';
    }


    TlbInterpolation_CartableFilter.beginUpdate();
    TlbInterpolation_CartableFilter.get_items().getItemById('tlbItemInterpolation_TlbInterpolation_CartableFilter').set_enabled(InterpolationEnabled);
    TlbInterpolation_CartableFilter.get_items().getItemById('tlbItemInterpolation_TlbInterpolation_CartableFilter').set_imageUrl(InterpolationImage);
    TlbInterpolation_CartableFilter.endUpdate();
}

function InsertCondition_GridCombinationalConditions_CartableFilter() {
    FilterField = cmbFilterField_CartableFilter.getSelectedItem().get_value();
    var FilterOperator = cmbOperator_CartableFilter.getSelectedItem().get_value();
    var FilterCondition = null;
    var ConditionOperator = 'And';
    switch (FilterField) {
        case 'Date':
            switch (parent.parent.CurrentLangID) {
                case 'fa-IR':
                    FilterCondition = document.getElementById('pdpDate_CartableFilter').value;
                    break;
                case 'en-US':
                    break;
            }
            break;
        case 'Selective':
            FilterCondition = cmbSelective_CartableFilter.getSelectedItem().get_text();
            break;
        case 'Time':
            FilterCondition = GetSelectedTime_CartableFilter();
            break;
        case 'String':
            FilterCondition = document.getElementById('txtString_CartableFilter').value;
    }

    var Splitter = null;
    if (StrFilterConditions == '')
        Splitter = '';
    else
        Splitter = '%';
    StrFilterConditions = StrFilterConditions + Splitter + GetConditionID_CartableFilter() + '@' + FilterField + '@' + FilterOperator + '@' + FilterCondition + '@' + ConditionOperator;
    CallBack_GridCombinationalConditions_CartableFilter.callback(StrFilterConditions);
}

function GetConditionID_CartableFilter() { 
    var ID = '';
    if (StrFilterConditions == '')
        ID = 1;
    else {
            var Conditions = StrFilterConditions.split('%');
            var ID = parseInt(Conditions[Conditions.length - 1].split('@')[0]) + 1;
    }
    return ID.toString();
}


function GetSelectedTime_CartableFilter() {
    var TimeSelector = 'TimeSelector_Hour_CartableFilter';
    var SelectedTime_CartableFilter = document.getElementById("" + TimeSelector + "_txtHour").value + ':' + document.getElementById("" + TimeSelector + "_txtMinute").value + ':' + document.getElementById("" + TimeSelector + "_txtSecond").value;
    return SelectedTime_CartableFilter;
}


function GridCombinationalConditions_CartableFilter_onItemDoubleClick(sender, e) {
    var SelectdRowID_GridCombinationalConditions_CartableFilter = e.get_item().getMember("ID").get_text();
    if (SelectdRowID_GridCombinationalConditions_CartableFilter != CurrentEditingRow_GridCombinationalConditions_CartableFilter) {
        CurrentEditingRow_GridCombinationalConditions_CartableFilter = SelectdRowID_GridCombinationalConditions_CartableFilter;
        GridCombinationalConditions_CartableFilter.edit(e.get_item());
    }
    else
        GridCombinationalConditions_CartableFilter.editComplete();
}


function ValidateFilterValue_CartableFilter() {
    var val = cmbFilterField_CartableFilter.getSelectedItem().get_value();
    IsValid = true;
    switch (val) {
        case 'Selective':
            if (cmbSelective_CartableFilter.get_selectedIndex() == -1)
                IsValid = false;
            break;
        case 'String':
            if (document.getElementById('txtString_CartableFilter').value == '')
                IsValid = false;
    }
    return IsValid;
}

function GetCurrentDateTime_CartableFilter() {
    GetCurrentDateTime_CartableFilterPage();
}

function GetCurrentDateTime_CartableFilterPage_onCallBack(Response) {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById("pdpDate_CartableFilter").value = Response;
            break;
    }
}

function getFilter_CartableFilter(DataItem) {
    var cmbInterpolationOperator_CartableFilter = document.getElementById('GridCombinationalConditions_CartableFilter_EditTemplate_0_2_cmbInterpolationOperator_CartableFilter');
    if (cmbInterpolationOperator_CartableFilter.selectedIndex == -1)
        return null;
    UpdateCondition_CartableFilter(DataItem, cmbInterpolationOperator_CartableFilter.value);
    return [cmbInterpolationOperator_CartableFilter.value, cmbInterpolationOperator_CartableFilter.options[cmbInterpolationOperator_CartableFilter.selectedIndex].text];    
}

function setFilter_CartableFilter(DataItem) {
    var cmbInterpolationOperator_CartableFilter = document.getElementById('GridCombinationalConditions_CartableFilter_EditTemplate_0_2_cmbInterpolationOperator_CartableFilter');
    for (var i = 0; i < cmbInterpolationOperator_CartableFilter.length; i++) {
        if (cmbInterpolationOperator_CartableFilter.options[i].text == DataItem.getMember('ConditionOperator').get_text()) {
            cmbInterpolationOperator_CartableFilter.selectedIndex = i;
            break;
        }
    }
}

function UpdateCondition_CartableFilter(DataItem, ConditionOperator) {
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

function ApplyConditions_CartableFilter() {
    if (StrFilterConditions == '')
        InterpolationConditions_CartableFilter();
    PushFilterConditions_CartableFilter();
    parent.DialogCartableFilter.Close();
}

function PushFilterConditions_CartableFilter() {
    parent.parent.document.getElementById('DialogCartable_IFrame').contentWindow.Cartable_OnAfterCustomFilter(StrFilterConditions);
}

function RemoveCondition_CartableFilter(RowID) {
    if (StrFilterConditions != '') {
        if (RowID != 'All') {
            var GridItem = GridCombinationalConditions_CartableFilter.getItemFromClientId(RowID);
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
            GridCombinationalConditions_CartableFilter.beginUpdate();
            GridCombinationalConditions_CartableFilter.deleteItem(GridItem);
            GridCombinationalConditions_CartableFilter.endUpdate();
        }
        else {
            StrFilterConditions = '';
            GridCombinationalConditions_CartableFilter.get_table().clearData();
        }
        CheckFilterConditionsCount_CartableFilter();
        PushFilterConditions_CartableFilter();
    }
}

function CallBack_GridCombinationalConditions_CartableFilter_onCallbackComplete(sender, e) {
    var cmbInterpolationOperator_CartableFilter = document.getElementById('GridCombinationalConditions_CartableFilter_EditTemplate_0_2_cmbInterpolationOperator_CartableFilter');
    if (cmbInterpolationOperator_CartableFilter.options.length > 0) {
        for (var i = 0; i < cmbInterpolationOperator_CartableFilter.options.length; i++) {
            var OperatorProps = ArOperators[i].split(':');
            if (OperatorProps[0] == cmbInterpolationOperator_CartableFilter.options[i].value)
                cmbInterpolationOperator_CartableFilter.options[i].text = OperatorProps[1];
        }
    }
    CheckFilterConditionsCount_CartableFilter();
}

function SetStrFilterCondition_onLoad() {
    StrFilterConditions = parent.parent.DialogCartable.StrFilterConditions;
    if(StrFilterConditions != '')
       CallBack_GridCombinationalConditions_CartableFilter.callback(StrFilterConditions);
}

function CallBack_GridCombinationalConditions_CartableFilter_onCallbackError(sender, e) {
    ShowConnectionError_CartableFilter();
}

function ShowConnectionError_CartableFilter() {
    var error = document.getElementById('hfErrorType_CartableFilter').value;
    var errorBody = document.getElementById('hfConnectionError_CartableFilter').value;
    showDialog(error, errorBody, 'error');
}


























