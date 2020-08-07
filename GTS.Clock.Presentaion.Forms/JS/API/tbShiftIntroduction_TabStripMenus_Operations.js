
var CurrentPageState_Shift = 'View';
var ConfirmState_Shift = null;
var ObjShift_Shift = null;
var CurrentPageState_ShiftPair = 'View';
var SelectedShiftType_Shift = null;
var SelectedWorkHeat_Shift = null;
var ObjShift_Shift = null;
var ObjShiftPair_Shift = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var zeroTime = '00';


function SetButtonImages_TimeSelectors_tbShiftIntroduction_TabStripMenus() {
    SetButtonImages_TimeSelector_tbShiftIntroduction_TabStripMenus('TimeSelector_WorkHeatMin_Shift');
    SetButtonImages_TimeSelector_tbShiftIntroduction_TabStripMenus('TimeSelector_From_ShiftPairs');
    SetButtonImages_TimeSelector_tbShiftIntroduction_TabStripMenus('TimeSelector_To_ShiftPairs');
    SetButtonImages_TimeSelector_tbShiftIntroduction_TabStripMenus('TimeSelector_FromTolerance_ShiftPairs');
    SetButtonImages_TimeSelector_tbShiftIntroduction_TabStripMenus('TimeSelector_ToTolerance_ShiftPairs');
}

function SetButtonImages_TimeSelector_tbShiftIntroduction_TabStripMenus(TimeSelector) {
    document.getElementById("" + TimeSelector + "_imgUp").src = "images/TimeSelector/CustomUp.gif";
    document.getElementById("" + TimeSelector + "_imgDown").src = "images/TimeSelector/CustomDown.gif";
    document.getElementById("" + TimeSelector + "_imgUp").onmouseover = function () {
        document.getElementById("" + TimeSelector + "_imgUp").src = "images/TimeSelector/oie_CustomUp.gif";
        FocusOnCurrentTimeSelector(TimeSelector);
    };
    document.getElementById("" + TimeSelector + "_imgDown").onmouseover = function () {
        document.getElementById("" + TimeSelector + "_imgDown").src = "images/TimeSelector/oie_CustomDown.gif";
        FocusOnCurrentTimeSelector(TimeSelector);
    };
    document.getElementById("" + TimeSelector + "_imgUp").onmouseout = function () {
        document.getElementById("" + TimeSelector + "_imgUp").src = "images/TimeSelector/CustomUp.gif";
    };
    document.getElementById("" + TimeSelector + "_imgDown").onmouseout = function () {
        document.getElementById("" + TimeSelector + "_imgDown").src = "images/TimeSelector/CustomDown.gif";
    };
}



function SetHorizontalScrollingDirection_GridShift_Shift_Opera() {
    if (navigator.userAgent.indexOf('Opera') != -1 && parent.CurrentLangID == "fa-IR")
        document.getElementById('GridShift_Shift').style.direction = "ltr";
}

function FixSize_GridShiftPairs_Shift() {
    GridShiftPairs_Shift.render();
}

function FixSize_GridShift_Shift() {
    GridShift_Shift.render();
}

function color_changed(sender, args) {
    if (sender.get_selectedColor() && sender.get_selectedColor().get_hex()) {
        var c = "#" + sender.get_selectedColor().get_hex();
        document.getElementById("chip").style.backgroundColor = c;
        document.getElementById("hex").innerHTML = c;
        document.getElementById("clr_ColorPicker").style.backgroundColor = c;
    }
}

function DialogColors_OnClose(sender, e) {
    document.getElementById("chip").style.backgroundColor = '';
    document.getElementById("hex").innerHTML = '';
}

function ChangeFloat_TimeSelectors_Shift() {
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            document.getElementById('TimeSelector_WorkHeatMin_Shift').style.styleFloat = 'right';
            document.getElementById('TimeSelector_WorkHeatMin_Shift').style.cssFloat = 'right';
            document.getElementById('TimeSelector_From_ShiftPairs').style.styleFloat = 'right';
            document.getElementById('TimeSelector_From_ShiftPairs').style.cssFloat = 'right';
            document.getElementById('TimeSelector_To_ShiftPairs').style.styleFloat = 'right';
            document.getElementById('TimeSelector_To_ShiftPairs').style.cssFloat = 'right';
            document.getElementById('TimeSelector_FromTolerance_ShiftPairs').style.styleFloat = 'right';
            document.getElementById('TimeSelector_FromTolerance_ShiftPairs').style.cssFloat = 'right';
            document.getElementById('TimeSelector_ToTolerance_ShiftPairs').style.styleFloat = 'right';
            document.getElementById('TimeSelector_ToTolerance_ShiftPairs').style.cssFloat = 'right';
            break;
        case 'en-US':
            document.getElementById('TimeSelector_WorkHeatMin_Shift').style.styleFloat = 'left';
            document.getElementById('TimeSelector_WorkHeatMin_Shift').style.cssFloat = 'left';
            document.getElementById('TimeSelector_From_ShiftPairs').style.styleFloat = 'left';
            document.getElementById('TimeSelector_From_ShiftPairs').style.cssFloat = 'left';
            document.getElementById('TimeSelector_To_ShiftPairs').style.styleFloat = 'left';
            document.getElementById('TimeSelector_To_ShiftPairs').style.cssFloat = 'left';
            document.getElementById('TimeSelector_FromTolerance_ShiftPairs').style.styleFloat = 'left';
            document.getElementById('TimeSelector_FromTolerance_ShiftPairs').style.cssFloat = 'left';
            document.getElementById('TimeSelector_ToTolerance_ShiftPairs').style.styleFloat = 'left';
            document.getElementById('TimeSelector_ToTolerance_ShiftPairs').style.cssFloat = 'left';
            break;
    }
}

function FocusOnCurrentTimeSelector(TimeSelector) {
    if (document.activeElement.id != "" + TimeSelector + "_txtHour" && document.activeElement.id != "" + TimeSelector + "_txtMinute" && document.activeElement.id != "" + TimeSelector + "_txtSecond" && !document.getElementById("" + TimeSelector + "_txtHour").disabled)
        document.getElementById("" + TimeSelector + "_txtHour").focus();
}

function GetBoxesHeaders_Shift() {
    document.getElementById('header_Shifts_Shift').innerHTML = document.getElementById('hfheader_Shifts_Shift').value;
    document.getElementById('header_ShiftDetails_Shift').innerHTML = document.getElementById('hfheader_ShiftDetails_Shift').value;
    document.getElementById('header_ShiftPairs_Shift').innerHTML = document.getElementById('hfheader_ShiftPairs_Shift').value;
    document.getElementById('header_ShiftPairsDetails_Shift').innerHTML = document.getElementById('hfheader_ShiftPairsDetails_Shift').value;
}

function SetActionMode_Shift(contentParent, state) {
    if (contentParent == 'Form' || contentParent == 'Shift')
        document.getElementById('ActionMode_Shift').innerHTML = document.getElementById("hf" + state + "_Shift").value;
    if (contentParent == 'Form' || contentParent == 'ShiftPair')
        document.getElementById('ActionMode_ShiftPair').innerHTML = document.getElementById("hf" + state + "_ShiftPair").value;
}

function ChangeColorPickerEnabled_Shift(state) {
    switch (state) {
        case 'disable':
            document.getElementById('a_ColorPicker').onclick = " ";
            break;
        case 'enable':
            document.getElementById('a_ColorPicker').onclick = function () {
                DialogColors.Show();
            };
            break;
    }
}

function DialogColors_OnShow(sender, e) {
    document.getElementById('DialogColors').style.zIndex = 25000000;
}

function ChangeTimePickerEnabled_Shift(TimeSelector, state) {
    var disabled = null;
    switch (state) {
        case 'disable':
            disabled = 'disabled';
            document.getElementById("" + TimeSelector + "_imgUp").onclick = " ";
            document.getElementById("" + TimeSelector + "_imgDown").onclick = " ";
            break;
        case 'enable':
            disabled = '';
            document.getElementById("" + TimeSelector + "_imgUp").onclick = function () {
                CheckTimePickerState_Shift(TimeSelector + '_txtHour');
                CheckTimePickerState_Shift(TimeSelector + '_txtMinute');
                addTime(document.getElementById("" + TimeSelector + "_imgUp"), 24, 1, 1);
            };
            document.getElementById("" + TimeSelector + "_imgDown").onclick = function () {
                CheckTimePickerState_Shift(TimeSelector + '_txtHour');
                CheckTimePickerState_Shift(TimeSelector + '_txtMinute');
                subtractTime(document.getElementById("" + TimeSelector + "_imgDown"), 24, 1, 1);
            };
            //            document.getElementById("" + TimeSelector + "_txtHour").onchange = function () {
            //                CheckTimeSelectorPartValue_Shift(TimeSelector, '_txtHour');
            //            }
            //            document.getElementById("" + TimeSelector + "_txtMinute").onchange = function () {
            //                CheckTimeSelectorPartValue_Shift(TimeSelector, '_txtMinute');
            //            }
            break;

    }
    document.getElementById("" + TimeSelector + "_txtHour").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtHour").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtSecond").disabled = disabled;
}

function CheckTimeSelectorPartValue_Shift(TimeSelectorPartID, identifier) {
    if (document.getElementById(TimeSelectorPartID + identifier).value == "") {
        if (document.getElementById(TimeSelectorPartID + identifier).value == "")
            document.getElementById(TimeSelectorPartID + identifier).value = zeroTime;
    }
}

function initTimePickers_Shift() {
    var disable = '';
    SetButtonImages_TimeSelectors_tbShiftIntroduction_TabStripMenus();
    ChangeFloat_TimeSelectors_Shift();

    ChangeTimePickerEnabled_Shift('TimeSelector_WorkHeatMin_Shift', 'disable');
    ChangeTimePickerEnabled_Shift('TimeSelector_From_ShiftPairs', 'disable');
    ChangeTimePickerEnabled_Shift('TimeSelector_To_ShiftPairs', 'disable');
    ChangeTimePickerEnabled_Shift('TimeSelector_FromTolerance_ShiftPairs', 'disable');
    ChangeTimePickerEnabled_Shift('TimeSelector_ToTolerance_ShiftPairs', 'disable');
    ResetTimepicker_Shift('TimeSelector_WorkHeatMin_Shift');
    ResetTimepicker_Shift('TimeSelector_From_ShiftPairs');
    ResetTimepicker_Shift('TimeSelector_To_ShiftPairs');
    ResetTimepicker_Shift('TimeSelector_FromTolerance_ShiftPairs');
    ResetTimepicker_Shift('TimeSelector_ToTolerance_ShiftPairs');
}

function CheckTimePickerState_Shift(TimeSelector) {
    if (((TimeSelector == 'TimeSelector_WorkHeatMin_Shift_txtHour' || TimeSelector == 'TimeSelector_From_ShiftPairs_txtHour' || TimeSelector == 'TimeSelector_To_ShiftPairs_txtHour' || TimeSelector == 'TimeSelector_FromTolerance_ShiftPairs_txtHour' || TimeSelector == 'TimeSelector_ToTolerance_ShiftPairs_txtHour') && (document.getElementById(TimeSelector).value == '-1' || isNaN(document.getElementById(TimeSelector).value))) || ((TimeSelector == 'TimeSelector_WorkHeatMin_Shift_txtMinute' || TimeSelector == 'TimeSelector_From_ShiftPairs_txtMinute' || TimeSelector == 'TimeSelector_To_ShiftPairs_txtMinute' || TimeSelector == 'TimeSelector_FromTolerance_ShiftPairs_txtMinute' || TimeSelector == 'TimeSelector_ToTolerance_ShiftPairs_txtMinute') && isNaN(document.getElementById(TimeSelector).value))) {
        document.getElementById(TimeSelector).value = zeroTime;
        return;
    }
}

function ShiftsForm_onKeydown(event) {
    var activeID = null;
    if (event.keyCode == 38 || event.keyCode == 40) {
        activeID = document.activeElement.id;
        CheckTimePickerState_Shift(activeID);
    }
}

function Fill_GridShift_Shift() {
    document.getElementById('loadingPanel_GridShift_Shift').innerHTML = document.getElementById('hfloadingPanel_GridShift_Shift').value;
    CallBack_GridShift_Shift.callback();
}

function Fill_GridShiftPairs_Shift(shiftID) {
    document.getElementById('loadingPanel_GridShiftPairs_Shift').innerHTML = document.getElementById('hfloadingPanel_GridShiftPairs_Shift').value;
    CallBack_GridShiftPairs_Shift.callback(shiftID);
}

function GridShift_Shift_onItemSelect(sender, e) {
    if (CurrentPageState_Shift != 'Add')
        NavigateShift_Shift(e.get_item());
}

function NavigateShift_Shift(selectedShiftItem) {
    if (selectedShiftItem != undefined) {
        document.getElementById('txtShiftCode_Shift').value = selectedShiftItem.getMember('CustomCode').get_text();
        document.getElementById('txtShiftName_Shift').value = selectedShiftItem.getMember('Name').get_text();
        SelectedShiftType_Shift = new Object();
        SelectedShiftType_Shift.ID = selectedShiftItem.getMember('ShiftType').get_text();
        document.getElementById('cmbShiftType_Shift_Input').value = SelectedShiftType_Shift.Name = GetShiftType_Shift(SelectedShiftType_Shift.ID);
        SelectedWorkHeat_Shift = new Object();
        SelectedWorkHeat_Shift.ID = selectedShiftItem.getMember('NobatKariID').get_text();
        document.getElementById('cmbWorkHeat_Shift_Input').value = SelectedWorkHeat_Shift.Name = selectedShiftItem.getMember('NobatKari.Name').get_text();
        SetDuration_TimePicker_Shift('TimeSelector_WorkHeatMin_Shift', selectedShiftItem.getMember('MinNobatKariTime').get_text());
        document.getElementById("clr_ColorPicker").style.backgroundColor = selectedShiftItem.getMember('Color').get_text();
        document.getElementById('chbBreakfast_Shift').checked = GetCheckedProp_Shift(selectedShiftItem.getMember('Breakfast').get_text());
        document.getElementById('chbLunch_Shift').checked = GetCheckedProp_Shift(selectedShiftItem.getMember('Lunch').get_text());
        document.getElementById('chbDinner_Shift').checked = GetCheckedProp_Shift(selectedShiftItem.getMember('Dinner').get_text());
        Fill_GridShiftPairs_Shift(selectedShiftItem.getMember('ID').get_text());
    }
}

function GetShiftType_Shift(shiftTypeID) {
    var shiftTypesList = document.getElementById('hfShiftTypes_Shift').value;
    var shiftTypesListParts = shiftTypesList.split('#');
    for (var i = 0; i < shiftTypesListParts.length; i++) {
        if (shiftTypesListParts[i] != '') {
            var shiftTypesPartObj = shiftTypesListParts[i].split(':');
            if (shiftTypeID == shiftTypesPartObj[1])
                return shiftTypesPartObj[0];
        }
    }
}

function NavigateShiftPair_Shift(selectedShiftPairItem) {
    if (selectedShiftPairItem != undefined) {
        SetDuration_TimePicker_Shift('TimeSelector_From_ShiftPairs', selectedShiftPairItem.getMember('FromTime').get_text());
        SetDuration_TimePicker_Shift('TimeSelector_To_ShiftPairs', selectedShiftPairItem.getMember('ToTime').get_text());
        SetDuration_TimePicker_Shift('TimeSelector_FromTolerance_ShiftPairs', selectedShiftPairItem.getMember('BeforeToleranceTime').get_text());
        SetDuration_TimePicker_Shift('TimeSelector_ToTolerance_ShiftPairs', selectedShiftPairItem.getMember('AfterToleranceTime').get_text());
        document.getElementById('chbContinueInNextDay_ShiftPairs').checked = selectedShiftPairItem.getMember('NextDayContinual').get_value();
    }
}

function GetCheckedProp_Shift(checkedStr) {
    var checked = null;
    switch (checkedStr) {
        case 'true':
            checked = true;
            break;
        case 'false':
            checked = false;
            break;
    }
    return checked;
}

function SetDuration_TimePicker_Shift(TimePicker, strTime) {
    if (strTime.indexOf('+') >= 0)
        strTime = strTime.replace('+', '');    
    if (strTime == "")
        strTime = '00:00';
    var arTime_Shift = strTime.split(':');
    for (var i = 0; i < 2; i++) {
        if (arTime_Shift[i].length < 2)
            arTime_Shift[i] = '0' + arTime_Shift[i];
    }
    document.getElementById(TimePicker + '_txtHour').value = arTime_Shift[0];
    document.getElementById(TimePicker + '_txtMinute').value = arTime_Shift[1];
}

function GetDuration_TimePicker_Shift(TimePicker) {
    if (document.getElementById(TimePicker + '_txtHour').value.length < 2)
        document.getElementById(TimePicker + '_txtHour').value = '0' + document.getElementById(TimePicker + '_txtHour').value;
    if (document.getElementById(TimePicker + '_txtMinute').value.length < 2)
        document.getElementById(TimePicker + '_txtMinute').value = '0' + document.getElementById(TimePicker + '_txtMinute').value;
    return document.getElementById(TimePicker + '_txtHour').value + ':' + document.getElementById(TimePicker + '_txtMinute').value;
}

function tlbItemNew_TlbShift_onClick() {
    ChangePageState_Shift('Add');
    ClearList_Shift();
    FocusOnFirstElement_Shift();
}

function ClearList_Shift() {
    if (CurrentPageState_Shift != 'Edit') {
        document.getElementById('txtShiftCode_Shift').value = '';
        document.getElementById('txtShiftName_Shift').value = '';
        document.getElementById('cmbShiftType_Shift_Input').value = document.getElementById('hfcmbAlarm_Shift').value;
        cmbShiftType_Shift.unSelect();
        document.getElementById('cmbWorkHeat_Shift_Input').value = '';
        cmbWorkHeat_Shift.unSelect();
        ResetTimepicker_Shift('TimeSelector_WorkHeatMin_Shift');
        document.getElementById("clr_ColorPicker").style.backgroundColor = '';
        document.getElementById('chbBreakfast_Shift').checked = false;
        document.getElementById('chbLunch_Shift').checked = false;
        document.getElementById('chbDinner_Shift').checked = false;
        SelectedShiftType_Shift = null;
        SelectedWorkHeat_Shift = null;
        GridShift_Shift.unSelectAll();
    }
}

function ClearList_ShiftPair() {
    if (CurrentPageState_ShiftPair != 'Edit') {
        ResetTimepicker_Shift('TimeSelector_From_ShiftPairs');
        ResetTimepicker_Shift('TimeSelector_To_ShiftPairs');
        ResetTimepicker_Shift('TimeSelector_FromTolerance_ShiftPairs');
        ResetTimepicker_Shift('TimeSelector_ToTolerance_ShiftPairs');
        document.getElementById('chbContinueInNextDay_ShiftPairs').checked = false;
        GridShiftPairs_Shift.unSelectAll();
    }
}

function FocusOnFirstElement_Shift() {
    document.getElementById('txtShiftCode_Shift').focus();
}

function FocusOnFirstElement_ShiftPair() {
    FocusOnCurrentTimeSelector('TimeSelector_From_ShiftPairs');
}

function ResetTimepicker_Shift(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}

function tlbItemEdit_TlbShift_onClick() {
    ChangePageState_Shift('Edit');
    FocusOnFirstElement_Shift();
}

function tlbItemDelete_TlbShift_onClick() {
    ChangePageState_Shift('Delete');
}

function tlbItemSave_TlbShift_onClick() {
    CollapseControls_Shift();
    Shift_onSave();
}

function tlbItemCancel_TlbShift_onClick() {
    ChangePageState_Shift('View');
    ClearList_Shift();
}

function tlbItemExit_TlbShift_onClick() {
    ShowDialogConfirm(null, 'Exit', null);
}

function Shift_onSave() {
    if (CurrentPageState_Shift != 'Delete')
        UpdateShift_Shift();
    else
        ShowDialogConfirm('Shift', 'Delete', CurrentPageState_Shift);
}

function ShiftPair_onSave() {
    if (CurrentPageState_ShiftPair != 'Delete')
        UpdateShiftPair_Shift();
    else
        ShowDialogConfirm('ShiftPair', 'Delete', CurrentPageState_ShiftPair);
}

function ShowDialogConfirm(role, confirmState, currentPageState) {
    ConfirmState_Shift = confirmState;
    if (currentPageState == 'Delete') {
        var ConfirmMessage = null;
        switch (role) {
            case 'Shift':
                ConfirmMessage = document.getElementById('hfDeleteMessage_Shift').value;
                break;
            case 'ShiftPair':
                ConfirmMessage = document.getElementById('hfDeleteMessage_ShiftPair').value;
                break;
        }
        document.getElementById('lblConfirm').innerHTML = ConfirmMessage;
    }
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Shift').value;
    DialogConfirm.set_value(role);
    DialogConfirm.Show();
    CollapseControls_Shift();
}

function UpdateShift_Shift() {
    ObjShift_Shift = new Object();
    ObjShift_Shift.ID = '0';
    ObjShift_Shift.CustomCode = null;
    ObjShift_Shift.Name = null;
    ObjShift_Shift.ShiftType = '-1';
    ObjShift_Shift.ShiftTypeTitle = null;
    ObjShift_Shift.WorkHeatName = null;
    ObjShift_Shift.WorkHeatID = '0';
    ObjShift_Shift.WorkHeatMin = '0';
    ObjShift_Shift.Breakfast = false;
    ObjShift_Shift.Lunch = false;
    ObjShift_Shift.Dinner = false;
    ObjShift_Shift.Color = null;

    var SelectedItems_GridShift_Shift = GridShift_Shift.getSelectedItems();
    if (SelectedItems_GridShift_Shift.length > 0)
        ObjShift_Shift.ID = SelectedItems_GridShift_Shift[0].getMember("ID").get_text();
        
    if (CurrentPageState_Shift != 'Delete') {
        ObjShift_Shift.CustomCode = document.getElementById('txtShiftCode_Shift').value;
        ObjShift_Shift.Name = document.getElementById('txtShiftName_Shift').value;
        if (cmbShiftType_Shift.getSelectedItem() != undefined) {
            ObjShift_Shift.ShiftType = cmbShiftType_Shift.getSelectedItem().get_id();
            ObjShift_Shift.ShiftTypeTitle = cmbShiftType_Shift.getSelectedItem().get_text();
        }
        else {
            if (SelectedShiftType_Shift != null) {
                ObjShift_Shift.ShiftType = SelectedShiftType_Shift.ID;
                ObjShift_Shift.ShiftTypeTitle = SelectedShiftType_Shift.Name;
            }
        }
        if (cmbWorkHeat_Shift.getSelectedItem() != undefined) {
            ObjShift_Shift.WorkHeatID = cmbWorkHeat_Shift.getSelectedItem().get_value();
            ObjShift_Shift.WorkHeatName = cmbWorkHeat_Shift.getSelectedItem().get_text();
        }
        else {
            if (SelectedWorkHeat_Shift != null) {
                ObjShift_Shift.WorkHeatID = SelectedWorkHeat_Shift.ID;
                ObjShift_Shift.WorkHeatName = SelectedWorkHeat_Shift.Name;
            }
        }
        ObjShift_Shift.Color = document.getElementById("clr_ColorPicker").style.backgroundColor;
        ObjShift_Shift.WorkHeatMin = GetDuration_TimePicker_Shift('TimeSelector_WorkHeatMin_Shift');
        ObjShift_Shift.Breakfast = document.getElementById('chbBreakfast_Shift').checked;
        ObjShift_Shift.Lunch = document.getElementById('chbLunch_Shift').checked;
        ObjShift_Shift.Dinner = document.getElementById('chbDinner_Shift').checked;
    }
    UpdateShift_ShiftPage(CharToKeyCode_Shift(CurrentPageState_Shift), CharToKeyCode_Shift(ObjShift_Shift.ID), CharToKeyCode_Shift(ObjShift_Shift.CustomCode), CharToKeyCode_Shift(ObjShift_Shift.Name), CharToKeyCode_Shift(ObjShift_Shift.ShiftType), CharToKeyCode_Shift(ObjShift_Shift.WorkHeatID), CharToKeyCode_Shift(ObjShift_Shift.WorkHeatMin), CharToKeyCode_Shift(ObjShift_Shift.Color), CharToKeyCode_Shift(ObjShift_Shift.Breakfast.toString()), CharToKeyCode_Shift(ObjShift_Shift.Lunch.toString()), CharToKeyCode_Shift(ObjShift_Shift.Dinner.toString()));
    DialogWaiting.Show();
}

function UpdateShiftPair_Shift() {
    ObjShiftPair_Shift = new Object();
    ObjShiftPair_Shift.ID = '0';
    ObjShiftPair_Shift.ShiftID = '0';
    ObjShiftPair_Shift.From = '0';
    ObjShiftPair_Shift.To = '0';
    ObjShiftPair_Shift.BeforeTolerance = '0';
    ObjShiftPair_Shift.AfterTolerance = '0';
    ObjShiftPair_Shift.ContinueInNextDay = false;

    var SelectedItems_GridShift_Shift = GridShift_Shift.getSelectedItems();
    if (SelectedItems_GridShift_Shift.length > 0)
        ObjShiftPair_Shift.ShiftID = SelectedItems_GridShift_Shift[0].getMember("ID").get_text();

    var SelectedItems_GridShiftPairs_Shift = GridShiftPairs_Shift.getSelectedItems();
    if (SelectedItems_GridShiftPairs_Shift.length > 0)
        ObjShiftPair_Shift.ID = SelectedItems_GridShiftPairs_Shift[0].getMember("ID").get_text();

    if (CurrentPageState_ShiftPair != 'Delete') {
        ObjShiftPair_Shift.From = GetDuration_TimePicker_Shift('TimeSelector_From_ShiftPairs');
        ObjShiftPair_Shift.To = GetDuration_TimePicker_Shift('TimeSelector_To_ShiftPairs');
        ObjShiftPair_Shift.BeforeTolerance = GetDuration_TimePicker_Shift('TimeSelector_FromTolerance_ShiftPairs');
        ObjShiftPair_Shift.AfterTolerance = GetDuration_TimePicker_Shift('TimeSelector_ToTolerance_ShiftPairs');
        ObjShiftPair_Shift.ContinueInNextDay = document.getElementById('chbContinueInNextDay_ShiftPairs').checked;
    }
    UpdateShiftPair_ShiftPage(CharToKeyCode_Shift(CurrentPageState_ShiftPair), CharToKeyCode_Shift(ObjShiftPair_Shift.ShiftID), CharToKeyCode_Shift(ObjShiftPair_Shift.ID), CharToKeyCode_Shift(ObjShiftPair_Shift.From), CharToKeyCode_Shift(ObjShiftPair_Shift.To), CharToKeyCode_Shift(ObjShiftPair_Shift.BeforeTolerance), CharToKeyCode_Shift(ObjShiftPair_Shift.AfterTolerance), CharToKeyCode_Shift(ObjShiftPair_Shift.ContinueInNextDay.toString()));
    DialogWaiting.Show();
}

function UpdateShiftPair_ShiftPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Shift').value;
            Response[1] = document.getElementById('hfConnectionError_Shift').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_ShiftPair();
            ShiftPair_OnAfterUpdate(Response);
            ChangePageState_ShiftPair('View');
        }
        else {
            if (CurrentPageState_ShiftPair == 'Delete') {
                ChangePageState_ShiftPair('View');
            }
            else
                ChangeRelativeToolBarEnabled_Shift('ShiftPair', 'End');
        }
    }
}

function ShiftPair_OnAfterUpdate(Response) {
    if (ObjShiftPair_Shift != null) {
        var From = ObjShiftPair_Shift.From;
        var To = ObjShiftPair_Shift.To;
        var FromTolerance = ObjShiftPair_Shift.BeforeTolerance;
        var ToTolerance = ObjShiftPair_Shift.AfterTolerance;
        var ContinueInNextDay = ObjShiftPair_Shift.ContinueInNextDay;

        if (ContinueInNextDay && To.indexOf('+') < 0)
            To = '+' + To;
        else {
            if (To.indexOf('+') >= 0)
                To = To.replace('+', '');
        }
                      
        var ShiftPairItem = null;
        GridShiftPairs_Shift.beginUpdate();
        switch (CurrentPageState_ShiftPair) {
            case 'Add':
                ShiftPairItem = GridShiftPairs_Shift.get_table().addEmptyRow(GridShiftPairs_Shift.get_recordCount());
                ShiftPairItem.setValue(0, Response[3], false);
                GridShiftPairs_Shift.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridShiftPairs_Shift.selectByKey(Response[3], 0, false);
                ShiftPairItem = GridShiftPairs_Shift.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridShiftPairs_Shift.selectByKey(ObjShiftPair_Shift.ID, 0, false);
                GridShiftPairs_Shift.deleteSelected();
                break;
        }
        if (CurrentPageState_ShiftPair != 'Delete') {
            ShiftPairItem.setValue(1, From, false);
            ShiftPairItem.setValue(2, To, false);
            ShiftPairItem.setValue(3, FromTolerance, false);
            ShiftPairItem.setValue(4, ToTolerance, false);
            ShiftPairItem.setValue(5, ContinueInNextDay, false);
        }
        GridShiftPairs_Shift.endUpdate();
    }
}

function UpdateShift_ShiftPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Shift').value;
            Response[1] = document.getElementById('hfConnectionError_Shift').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_Shift();
            Shift_OnAfterUpdate(Response);
            ClearData_GridShiftPairs_Shift();
            ChangePageState_Shift('View');
        }
        else {
            if (CurrentPageState_Shift == 'Delete')
                ChangePageState_Shift('View');
            else
                ChangeRelativeToolBarEnabled_Shift('Shift', 'End');
        }
    }
}

function ClearData_GridShiftPairs_Shift() {
    if (CurrentPageState_Shift == 'Add' || CurrentPageState_Shift == 'Delete') {
        GridShiftPairs_Shift.get_table().clearData();
        GridShiftPairs_Shift.render();
    }
}

function Shift_OnAfterUpdate(Response) {
    if (ObjShift_Shift != null) {
        var ShiftCode = ObjShift_Shift.CustomCode;
        var ShiftName = ObjShift_Shift.Name;
        var ShiftType = ObjShift_Shift.ShiftType;
        var ShiftTypeTitle = ObjShift_Shift.ShiftTypeTitle;
        var WorkHeatName = ObjShift_Shift.WorkHeatName;
        var WorkHeatID = ObjShift_Shift.WorkHeatID;
        var WorkHeatMin = ObjShift_Shift.WorkHeatMin;
        var Breakfast = ObjShift_Shift.Breakfast;
        var Lunch = ObjShift_Shift.Lunch;
        var Dinner = ObjShift_Shift.Dinner;
        var ShiftColor = ObjShift_Shift.Color;

        var ShiftItem = null;

        GridShift_Shift.beginUpdate();
        switch (CurrentPageState_Shift) {
            case 'Add':
                ShiftItem = GridShift_Shift.get_table().addEmptyRow(GridShift_Shift.get_recordCount());
                ShiftItem.setValue(0, Response[3], false);
                GridShift_Shift.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridShift_Shift.selectByKey(Response[3], 0, false);
                ShiftItem = GridShift_Shift.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridShift_Shift.selectByKey(ObjShift_Shift.ID, 0, false);
                GridShift_Shift.deleteSelected();
                break;
        }
        if (CurrentPageState_Shift != 'Delete') {
            ShiftItem.setValue(1, ShiftCode, false);
            ShiftItem.setValue(2, ShiftName, false);
            ShiftItem.setValue(3, ShiftTypeTitle, false);
            ShiftItem.setValue(4, WorkHeatName, false);
            ShiftItem.setValue(5, WorkHeatMin, false);
            ShiftItem.setValue(6, Breakfast, false);
            ShiftItem.setValue(7, Lunch, false);
            ShiftItem.setValue(8, Dinner, false);
            ShiftItem.setValue(9, ShiftColor, false);
            ShiftItem.setValue(10, ShiftType, false);
            ShiftItem.setValue(11, WorkHeatID, false);
        }
        GridShift_Shift.endUpdate();        
    }
}

function ChangePageState_Shift(state) {
    CurrentPageState_Shift = state;
    SetActionMode_Shift('Shift', state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbShift.get_items().getItemById('tlbItemNew_TlbShift') != null) {
            TlbShift.get_items().getItemById('tlbItemNew_TlbShift').set_enabled(false);
            TlbShift.get_items().getItemById('tlbItemNew_TlbShift').set_imageUrl('add_silver.png');
        }
        if (TlbShift.get_items().getItemById('tlbItemEdit_TlbShift') != null) {
            TlbShift.get_items().getItemById('tlbItemEdit_TlbShift').set_enabled(false);
            TlbShift.get_items().getItemById('tlbItemEdit_TlbShift').set_imageUrl('edit_silver.png');
        }
        if (TlbShift.get_items().getItemById('tlbItemDelete_TlbShift') != null) {
            TlbShift.get_items().getItemById('tlbItemDelete_TlbShift').set_enabled(false);
            TlbShift.get_items().getItemById('tlbItemDelete_TlbShift').set_imageUrl('remove_silver.png');
        }
        TlbShift.get_items().getItemById('tlbItemSave_TlbShift').set_enabled(true);
        TlbShift.get_items().getItemById('tlbItemSave_TlbShift').set_imageUrl('save.png');
        TlbShift.get_items().getItemById('tlbItemCancel_TlbShift').set_enabled(true);
        TlbShift.get_items().getItemById('tlbItemCancel_TlbShift').set_imageUrl('cancel.png');
        TlbWorkHeat_Shift.get_items().getItemById('tlbItemWorkHeats_TlbWorkHeat_Shift').set_enabled(true);
        TlbWorkHeat_Shift.get_items().getItemById('tlbItemWorkHeats_TlbWorkHeat_Shift').set_imageUrl('eyeglasses.png');
        TlbRefresh_cmbWorkHeat_Shift.get_items().getItemById('tlbItemRefresh_TlbRefresh_cmbWorkHeat_Shift').set_enabled(true);
        TlbRefresh_cmbWorkHeat_Shift.get_items().getItemById('tlbItemRefresh_TlbRefresh_cmbWorkHeat_Shift').set_imageUrl('refresh.png');
        ChangeRelativeToolBarEnabled_Shift('Shift', 'Start');
        document.getElementById('txtShiftCode_Shift').disabled = '';
        document.getElementById('txtShiftName_Shift').disabled = '';
        cmbShiftType_Shift.enable();
        cmbWorkHeat_Shift.enable();
        ChangeTimePickerEnabled_Shift('TimeSelector_WorkHeatMin_Shift', 'enable');
        ChangeColorPickerEnabled_Shift('enable');
        document.getElementById('chbBreakfast_Shift').disabled = '';
        document.getElementById('chbLunch_Shift').disabled = '';
        document.getElementById('chbDinner_Shift').disabled = '';
        if (state == 'Edit')
            NavigateShift_Shift(GridShift_Shift.getSelectedItems()[0]);
        if (state == 'Delete')
            Shift_onSave();
    }
    if (state == 'View') {
        if (TlbShift.get_items().getItemById('tlbItemNew_TlbShift') != null) {
            TlbShift.get_items().getItemById('tlbItemNew_TlbShift').set_enabled(true);
            TlbShift.get_items().getItemById('tlbItemNew_TlbShift').set_imageUrl('add.png');
        }
        if (TlbShift.get_items().getItemById('tlbItemEdit_TlbShift') != null) {
            TlbShift.get_items().getItemById('tlbItemEdit_TlbShift').set_enabled(true);
            TlbShift.get_items().getItemById('tlbItemEdit_TlbShift').set_imageUrl('edit.png');
        }
        if (TlbShift.get_items().getItemById('tlbItemDelete_TlbShift') != null) {
            TlbShift.get_items().getItemById('tlbItemDelete_TlbShift').set_enabled(true);
            TlbShift.get_items().getItemById('tlbItemDelete_TlbShift').set_imageUrl('remove.png');
        }
        TlbShift.get_items().getItemById('tlbItemSave_TlbShift').set_enabled(false);
        TlbShift.get_items().getItemById('tlbItemSave_TlbShift').set_imageUrl('save_silver.png');
        TlbShift.get_items().getItemById('tlbItemCancel_TlbShift').set_enabled(false);
        TlbShift.get_items().getItemById('tlbItemCancel_TlbShift').set_imageUrl('cancel_silver.png');
        TlbWorkHeat_Shift.get_items().getItemById('tlbItemWorkHeats_TlbWorkHeat_Shift').set_enabled(true);
        TlbWorkHeat_Shift.get_items().getItemById('tlbItemWorkHeats_TlbWorkHeat_Shift').set_imageUrl('eyeglasses.png');
        TlbRefresh_cmbWorkHeat_Shift.get_items().getItemById('tlbItemRefresh_TlbRefresh_cmbWorkHeat_Shift').set_enabled(false);
        TlbRefresh_cmbWorkHeat_Shift.get_items().getItemById('tlbItemRefresh_TlbRefresh_cmbWorkHeat_Shift').set_imageUrl('refresh_silver.png');
        ChangeRelativeToolBarEnabled_Shift('Shift', 'End');
        document.getElementById('txtShiftCode_Shift').disabled = 'disabled';
        document.getElementById('txtShiftName_Shift').disabled = 'disabled';
        cmbShiftType_Shift.disable();
        cmbWorkHeat_Shift.disable();
        ChangeTimePickerEnabled_Shift('TimeSelector_WorkHeatMin_Shift', 'disable');
        ChangeColorPickerEnabled_Shift('disable');
        document.getElementById('chbBreakfast_Shift').disabled = 'disabled';
        document.getElementById('chbLunch_Shift').disabled = 'disabled';
        document.getElementById('chbDinner_Shift').disabled = 'disabled';
        if (DialogColors.get_isShowing())
            DialogColors.Close();
    }
}

function ChangePageState_ShiftPair(state) {
    CurrentPageState_ShiftPair = state;
    SetActionMode_Shift('ShiftPair', state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbShiftPairs.get_items().getItemById('tlbItemNew_TlbShiftPairs') != null) {
            TlbShiftPairs.get_items().getItemById('tlbItemNew_TlbShiftPairs').set_enabled(false);
            TlbShiftPairs.get_items().getItemById('tlbItemNew_TlbShiftPairs').set_imageUrl('add_silver.png');
        }
        if (TlbShiftPairs.get_items().getItemById('tlbItemEdit_TlbShiftPairs') != null) {
            TlbShiftPairs.get_items().getItemById('tlbItemEdit_TlbShiftPairs').set_enabled(false);
            TlbShiftPairs.get_items().getItemById('tlbItemEdit_TlbShiftPairs').set_imageUrl('edit_silver.png');
        }
        if (TlbShiftPairs.get_items().getItemById('tlbItemDelete_TlbShiftPairs') != null) {
            TlbShiftPairs.get_items().getItemById('tlbItemDelete_TlbShiftPairs').set_enabled(false);
            TlbShiftPairs.get_items().getItemById('tlbItemDelete_TlbShiftPairs').set_imageUrl('remove_silver.png');
        }
        TlbShiftPairs.get_items().getItemById('tlbItemSave_TlbShiftPairs').set_enabled(true);
        TlbShiftPairs.get_items().getItemById('tlbItemSave_TlbShiftPairs').set_imageUrl('save.png');
        TlbShiftPairs.get_items().getItemById('tlbItemCancel_TlbShiftPairs').set_enabled(true);
        TlbShiftPairs.get_items().getItemById('tlbItemCancel_TlbShiftPairs').set_imageUrl('cancel.png');
        ChangeRelativeToolBarEnabled_Shift('ShiftPair', 'Start');
        ChangeTimePickerEnabled_Shift('TimeSelector_From_ShiftPairs', 'enable');
        ChangeTimePickerEnabled_Shift('TimeSelector_To_ShiftPairs', 'enable');
        ChangeTimePickerEnabled_Shift('TimeSelector_FromTolerance_ShiftPairs', 'enable');
        ChangeTimePickerEnabled_Shift('TimeSelector_ToTolerance_ShiftPairs', 'enable');
        document.getElementById('chbContinueInNextDay_ShiftPairs').disabled = '';
        if (state == 'Edit')
            NavigateShiftPair_Shift(GridShiftPairs_Shift.getSelectedItems()[0]);
        if (state == 'Delete')
            ShiftPair_onSave();
    }
    if (state == 'View') {
        if (TlbShiftPairs.get_items().getItemById('tlbItemNew_TlbShiftPairs') != null) {
            TlbShiftPairs.get_items().getItemById('tlbItemNew_TlbShiftPairs').set_enabled(true);
            TlbShiftPairs.get_items().getItemById('tlbItemNew_TlbShiftPairs').set_imageUrl('add.png');
        }
        if (TlbShiftPairs.get_items().getItemById('tlbItemEdit_TlbShiftPairs') != null) {
            TlbShiftPairs.get_items().getItemById('tlbItemEdit_TlbShiftPairs').set_enabled(true);
            TlbShiftPairs.get_items().getItemById('tlbItemEdit_TlbShiftPairs').set_imageUrl('edit.png');
        }
        if (TlbShiftPairs.get_items().getItemById('tlbItemDelete_TlbShiftPairs') != null) {
            TlbShiftPairs.get_items().getItemById('tlbItemDelete_TlbShiftPairs').set_enabled(true);
            TlbShiftPairs.get_items().getItemById('tlbItemDelete_TlbShiftPairs').set_imageUrl('remove.png');
        }
        TlbShiftPairs.get_items().getItemById('tlbItemSave_TlbShiftPairs').set_enabled(false);
        TlbShiftPairs.get_items().getItemById('tlbItemSave_TlbShiftPairs').set_imageUrl('save_silver.png');
        TlbShiftPairs.get_items().getItemById('tlbItemCancel_TlbShiftPairs').set_enabled(false);
        TlbShiftPairs.get_items().getItemById('tlbItemCancel_TlbShiftPairs').set_imageUrl('cancel_silver.png');
        ChangeRelativeToolBarEnabled_Shift('ShiftPair', 'End');
        ChangeTimePickerEnabled_Shift('TimeSelector_From_ShiftPairs', 'disable');
        ChangeTimePickerEnabled_Shift('TimeSelector_To_ShiftPairs', 'disable');
        ChangeTimePickerEnabled_Shift('TimeSelector_FromTolerance_ShiftPairs', 'disable');
        ChangeTimePickerEnabled_Shift('TimeSelector_ToTolerance_ShiftPairs', 'disable');
        document.getElementById('chbContinueInNextDay_ShiftPairs').disabled = 'disabled';
    }
}

function ChangeRelativeToolBarEnabled_Shift(master, operationState) {
    switch (master) {
        case 'Shift':
            switch (operationState) {
                case 'Start':
                    TlbShiftPairs.get_items().getItemById('tlbItemNew_TlbShiftPairs').set_enabled(false);
                    TlbShiftPairs.get_items().getItemById('tlbItemNew_TlbShiftPairs').set_imageUrl('add_silver.png');
                    TlbShiftPairs.get_items().getItemById('tlbItemEdit_TlbShiftPairs').set_enabled(false);
                    TlbShiftPairs.get_items().getItemById('tlbItemEdit_TlbShiftPairs').set_imageUrl('edit_silver.png');
                    TlbShiftPairs.get_items().getItemById('tlbItemDelete_TlbShiftPairs').set_enabled(false);
                    TlbShiftPairs.get_items().getItemById('tlbItemDelete_TlbShiftPairs').set_imageUrl('remove_silver.png');
                    TlbShiftPairs.get_items().getItemById('tlbItemSave_TlbShiftPairs').set_enabled(false);
                    TlbShiftPairs.get_items().getItemById('tlbItemSave_TlbShiftPairs').set_imageUrl('save_silver.png');
                    TlbShiftPairs.get_items().getItemById('tlbItemCancel_TlbShiftPairs').set_enabled(false);
                    TlbShiftPairs.get_items().getItemById('tlbItemCancel_TlbShiftPairs').set_imageUrl('cancel_silver.png');
                    break;
                case 'End':
                    if (CurrentPageState_ShiftPair == 'Add' || CurrentPageState_ShiftPair == 'Edit' || CurrentPageState_ShiftPair == 'Delete') {
                        TlbShiftPairs.get_items().getItemById('tlbItemNew_TlbShiftPairs').set_enabled(false);
                        TlbShiftPairs.get_items().getItemById('tlbItemNew_TlbShiftPairs').set_imageUrl('add_silver.png');
                        TlbShiftPairs.get_items().getItemById('tlbItemEdit_TlbShiftPairs').set_enabled(false);
                        TlbShiftPairs.get_items().getItemById('tlbItemEdit_TlbShiftPairs').set_imageUrl('edit_silver.png');
                        TlbShiftPairs.get_items().getItemById('tlbItemDelete_TlbShiftPairs').set_enabled(false);
                        TlbShiftPairs.get_items().getItemById('tlbItemDelete_TlbShiftPairs').set_imageUrl('remove_silver.png');
                        TlbShiftPairs.get_items().getItemById('tlbItemSave_TlbShiftPairs').set_enabled(true);
                        TlbShiftPairs.get_items().getItemById('tlbItemSave_TlbShiftPairs').set_imageUrl('save.png');
                        TlbShiftPairs.get_items().getItemById('tlbItemCancel_TlbShiftPairs').set_enabled(true);
                        TlbShiftPairs.get_items().getItemById('tlbItemCancel_TlbShiftPairs').set_imageUrl('cancel.png');
                    }
                    if (CurrentPageState_ShiftPair == 'View') {
                        TlbShiftPairs.get_items().getItemById('tlbItemNew_TlbShiftPairs').set_enabled(true);
                        TlbShiftPairs.get_items().getItemById('tlbItemNew_TlbShiftPairs').set_imageUrl('add.png');
                        TlbShiftPairs.get_items().getItemById('tlbItemEdit_TlbShiftPairs').set_enabled(true);
                        TlbShiftPairs.get_items().getItemById('tlbItemEdit_TlbShiftPairs').set_imageUrl('edit.png');
                        TlbShiftPairs.get_items().getItemById('tlbItemDelete_TlbShiftPairs').set_enabled(true);
                        TlbShiftPairs.get_items().getItemById('tlbItemDelete_TlbShiftPairs').set_imageUrl('remove.png');
                        TlbShiftPairs.get_items().getItemById('tlbItemSave_TlbShiftPairs').set_enabled(false);
                        TlbShiftPairs.get_items().getItemById('tlbItemSave_TlbShiftPairs').set_imageUrl('save_silver.png');
                        TlbShiftPairs.get_items().getItemById('tlbItemCancel_TlbShiftPairs').set_enabled(false);
                        TlbShiftPairs.get_items().getItemById('tlbItemCancel_TlbShiftPairs').set_imageUrl('cancel_silver.png');
                    }
                    break;
            }
            break;
        case 'ShiftPair':
            switch (operationState) {
                case 'Start':
                    TlbShift.get_items().getItemById('tlbItemNew_TlbShift').set_enabled(false);
                    TlbShift.get_items().getItemById('tlbItemNew_TlbShift').set_imageUrl('add_silver.png');
                    TlbShift.get_items().getItemById('tlbItemEdit_TlbShift').set_enabled(false);
                    TlbShift.get_items().getItemById('tlbItemEdit_TlbShift').set_imageUrl('edit_silver.png');
                    TlbShift.get_items().getItemById('tlbItemDelete_TlbShift').set_enabled(false);
                    TlbShift.get_items().getItemById('tlbItemDelete_TlbShift').set_imageUrl('remove_silver.png');
                    TlbShift.get_items().getItemById('tlbItemSave_TlbShift').set_enabled(false);
                    TlbShift.get_items().getItemById('tlbItemSave_TlbShift').set_imageUrl('save_silver.png');
                    TlbShift.get_items().getItemById('tlbItemCancel_TlbShift').set_enabled(false);
                    TlbShift.get_items().getItemById('tlbItemCancel_TlbShift').set_imageUrl('cancel_silver.png');
                    TlbWorkHeat_Shift.get_items().getItemById('tlbItemWorkHeats_TlbWorkHeat_Shift').set_enabled(false);
                    TlbWorkHeat_Shift.get_items().getItemById('tlbItemWorkHeats_TlbWorkHeat_Shift').set_imageUrl('eyeglasses_silver.png');
                    break;
                case 'End':
                    if (CurrentPageState_Shift == 'Add' || CurrentPageState_Shift == 'Edit' || CurrentPageState_Shift == 'Delete') {
                        TlbShift.get_items().getItemById('tlbItemNew_TlbShift').set_enabled(false);
                        TlbShift.get_items().getItemById('tlbItemNew_TlbShift').set_imageUrl('add_silver.png');
                        TlbShift.get_items().getItemById('tlbItemEdit_TlbShift').set_enabled(false);
                        TlbShift.get_items().getItemById('tlbItemEdit_TlbShift').set_imageUrl('edit_silver.png');
                        TlbShift.get_items().getItemById('tlbItemDelete_TlbShift').set_enabled(false);
                        TlbShift.get_items().getItemById('tlbItemDelete_TlbShift').set_imageUrl('remove_silver.png');
                        TlbShift.get_items().getItemById('tlbItemSave_TlbShift').set_enabled(true);
                        TlbShift.get_items().getItemById('tlbItemSave_TlbShift').set_imageUrl('save.png');
                        TlbShift.get_items().getItemById('tlbItemCancel_TlbShift').set_enabled(true);
                        TlbShift.get_items().getItemById('tlbItemCancel_TlbShift').set_imageUrl('cancel.png');
                        TlbWorkHeat_Shift.get_items().getItemById('tlbItemWorkHeats_TlbWorkHeat_Shift').set_enabled(false);
                        TlbWorkHeat_Shift.get_items().getItemById('tlbItemWorkHeats_TlbWorkHeat_Shift').set_imageUrl('eyeglasses_silver.png');
                    }
                    if (CurrentPageState_Shift == 'View') {
                        TlbShift.get_items().getItemById('tlbItemNew_TlbShift').set_enabled(true);
                        TlbShift.get_items().getItemById('tlbItemNew_TlbShift').set_imageUrl('add.png');
                        TlbShift.get_items().getItemById('tlbItemEdit_TlbShift').set_enabled(true);
                        TlbShift.get_items().getItemById('tlbItemEdit_TlbShift').set_imageUrl('edit.png');
                        TlbShift.get_items().getItemById('tlbItemDelete_TlbShift').set_enabled(true);
                        TlbShift.get_items().getItemById('tlbItemDelete_TlbShift').set_imageUrl('remove.png');
                        TlbShift.get_items().getItemById('tlbItemSave_TlbShift').set_enabled(false);
                        TlbShift.get_items().getItemById('tlbItemSave_TlbShift').set_imageUrl('save_silver.png');
                        TlbShift.get_items().getItemById('tlbItemCancel_TlbShift').set_enabled(false);
                        TlbShift.get_items().getItemById('tlbItemCancel_TlbShift').set_imageUrl('cancel_silver.png');
                        TlbWorkHeat_Shift.get_items().getItemById('tlbItemWorkHeats_TlbWorkHeat_Shift').set_enabled(true);
                        TlbWorkHeat_Shift.get_items().getItemById('tlbItemWorkHeats_TlbWorkHeat_Shift').set_imageUrl('eyeglasses.png');
                    }
                    break
            }
    }
}

function GridShift_Shift_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridShift_Shift').innerHTML = '';
}

function CallBack_GridShift_Shift_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Shift').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridShift_Shift();
    }
    else {
        if (GridShift_Shift.get_table().getRowCount() > 0) {
            var ShiftID = GridShift_Shift.get_table().getRow(0).getMember('ID').get_text();
            GridShift_Shift.selectByKey(ShiftID, 0, false);
            Fill_GridShiftPairs_Shift(ShiftID);
        }
    }
}

function Refresh_GridShift_Shift() {
    Fill_GridShift_Shift();
}

function tlbItemWorkHeats_TlbWorkHeat_Shift_onClick() {
    parent.NavBarMain_onItemSelect_Operations(parent.NavBarMain.findItemById('nvbItemWorkHeatIntroduction_NavBarMain'));
}

function tlbItemOk_TlbOkConfirm_onClick() {
    var role = DialogConfirm.get_value();
    switch (ConfirmState_Shift) {
        case 'Delete':
            switch (role) {
                case 'Shift':
                    DialogConfirm.Close();
                    UpdateShift_Shift();
                    break;
                case 'ShiftPair':
                    DialogConfirm.Close();
                    UpdateShiftPair_Shift();
                    break;
            }
            break;
        case 'Exit':
            ClearList_Shift();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    var role = DialogConfirm.get_value();
    switch (role) {
        case 'Shift':
            ChangePageState_Shift('View');
            break;
        case 'ShiftPair':
            ChangePageState_ShiftPair('View');
            break;
    }
}

function cmbShiftType_Shift_onExpand(sender, e) {
    if (cmbShiftType_Shift.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbShiftType_Shift == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbShiftType_Shift = true;
        CallBackcmbShiftType_Shift.callback();
    }
}

function cmbShiftType_Shift_onCollapse(sender, e) {
    if(cmbShiftType_Shift.getSelectedItem() == undefined ){
        if (SelectedShiftType_Shift.ID == undefined || SelectedShiftType_Shift.ID == null)
            document.getElementById('cmbShiftType_Shift_Input').value = document.getElementById('hfcmbAlarm_Shift').value;
        else {
            if (SelectedShiftType_Shift.ID != undefined && SelectedShiftType_Shift.ID != null)
                    document.getElementById('cmbShiftType_Shift_Input').value = SelectedShiftType_Shift.Name;
        }
    }
}

function CallBackcmbShiftType_Shift_onBeforeCallback(sender, e) {
    cmbShiftType_Shift.dispose();
}

function CallBackcmbShiftType_Shift_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ShiftType').value;
    if (error == "") {
        document.getElementById('cmbShiftType_Shift_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbShiftType_Shift_DropImage').mousedown();
        cmbShiftType_Shift.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbShiftType_Shift_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CharToKeyCode_Shift(str) {
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

function GetShiftTypeTitle_Shift(shiftType) {
    var ShiftTypes = document.getElementById('hfShiftTypes_Shift').value.split('#');
    for (var i = 0; i < ShiftTypes.length; i++) {
        var shiftTypeObj = ShiftTypes[i].split(':');
        if (shiftTypeObj.length > 1) {
            if (shiftTypeObj[1] == shiftType.toString())
                return shiftTypeObj[0];
        }
    }
}

function tlbItemNew_TlbShiftPairs_onClick() {
    ChangePageState_ShiftPair('Add');
    ClearList_ShiftPair();
    FocusOnFirstElement_ShiftPair();
}

function tlbItemEdit_TlbShiftPairs_onClick() {
    ChangePageState_ShiftPair('Edit');
    FocusOnFirstElement_ShiftPair();
}

function tlbItemDelete_TlbShiftPairs_onClick() {
    ChangePageState_ShiftPair('Delete');
}

function tlbItemSave_TlbShiftPairs_onClick() {
    CollapseControls_Shift();
    ShiftPair_onSave();
}

function tlbItemCancel_TlbShiftPairs_onClick() {
    ChangePageState_ShiftPair('View');
    ClearList_ShiftPair();
}

function Refresh_GridShiftPairs_Shift() {
    var shiftID = null;
    if (GridShift_Shift.get_table().getRowCount() > 0) {
        if (GridShift_Shift.getSelectedItems() != undefined)
            shiftID = GridShift_Shift.getSelectedItems()[0].getMember('ID').get_text();
        else
            shiftID = GridShift_Shift.get_table().getRow(0).getMember('ID').get_text();
    }
    Fill_GridShiftPairs_Shift(shiftID);
}

function GridShiftPairs_Shift_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridShiftPairs_Shift').innerHTML = '';
}

function GridShiftPairs_Shift_onItemSelect(sender, e) {
    if (CurrentPageState_ShiftPair != 'Add')
        NavigateShiftPair_Shift(e.get_item());
}

function CallBack_GridShiftPairs_Shift_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ShiftPairs').value;
    if (error != "") {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        if (erroParts[3] == 'Reload')
            Refresh_GridShiftPairs_Shift();
    }
}

function cmbWorkHeat_Shift_onExpand(sender, e) {
    if (cmbWorkHeat_Shift.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbWorkHeat_Shift == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbWorkHeat_Shift = true;
        CallBack_cmbWorkHeat_Shift.callback();
    }
}

function CallBack_cmbWorkHeat_Shift_onBeforeCallback(sender, e) {
    cmbWorkHeat_Shift.dispose();
}

function CallBack_cmbWorkHeat_Shift_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkHeat').value;
    if (error == "") {
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbWorkHeat_Shift_DropImage').mousedown();
        cmbWorkHeat_Shift.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbWorkHeat_Shift_DropDown').style.display = 'none';
    }
}

function Refresh_cmbWorkHeat_Shift() {
    CallBack_cmbWorkHeat_Shift.callback();
}

function CallBack_GridShift_Shift_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridShift_Shift').innerHTML = '';
    ShowConnectionError_Shift();
}

function CallBack_GridShiftPairs_Shift_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridShiftPairs_Shift').innerHTML = '';
    ShowConnectionError_Shift();
}

function ShowConnectionError_Shift() {
    var error = document.getElementById('hfErrorType_Shift').value;
    var errorBody = document.getElementById('hfConnectionError_Shift').value;
    showDialog(error, errorBody, 'error');
}

function CallBackcmbShiftType_Shift_onCallbackError(sender, e) {
    ShowConnectionError_Shift();
}

function CallBack_cmbWorkHeat_Shift_onCallbackError(sender, e) {
    ShowConnectionError_Shift();
}

function CollapseControls_Shift() {
    cmbShiftType_Shift.collapse();
    cmbWorkHeat_Shift.collapse();
}

function tlbItemFormReconstruction_TlbShift_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvShiftIntroduction_iFrame').src = 'Shifts.aspx';
}

function tlbItemHelp_TlbShift_onClick() {
    LoadHelpPage('tlbItemHelp_TlbShift');    
}







