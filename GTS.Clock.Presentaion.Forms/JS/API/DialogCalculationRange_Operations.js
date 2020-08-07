
var CurrentPageState_CalculationRange = null;
var ConceptsListObj_CalculationRange = "";
var ObjCalculationRange_CalculationRange = null;
var DefaultCalculationRangesList_CalculationRange = "";
var MonthsDayCol_CalculationRange = null;
var FirstExceptionNudObj_CalculationRange = null;
var LastExceptionNudObj_CalculationRange = null;
var NudCreator_CalculationRange = null;



function GetBoxesHeaders_CalculationRange() {
    parent.document.getElementById('Title_DialogCalculationRange').innerHTML = document.getElementById('hfTitle_DialogCalculationRange').value;
    document.getElementById('header_CalculationRange_CalculationRange').innerHTML = document.getElementById('hfheader_CalculationRange_CalculationRange').value;
    document.getElementById('header_CalculationRangeDetails_CalculationRange').innerHTML = document.getElementById('hfheader_CalculationRangeDetails_CalculationRange').value;
}

function GetMonthFeatures_CalculationRange() {
    GetMonthFeatures_CalculationRangePage("");
}

function GetMonthFeatures_CalculationRangePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (RetMessage[2] == 'success') {
            var MonthAxises = RetMessage[3];
            MonthsAxises = MonthAxises.split('@');
            for (var i = 0; i < MonthsAxises.length; i++) {
                document.getElementById("lblMonth" + (i + 1) + "_CalculationRange").innerHTML = MonthsAxises[i];
            }

            MonthsDayCol_CalculationRange = eval('(' + RetMessage[4] + ')');
            CreateNumericUpDownControls_CalculationRange(MonthsDayCol_CalculationRange);
            GetDefaultCalculationRangesList_CalculationRange();
        }
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function CreateNumericUpDownControls_CalculationRange(monthsDayCol) {
    var ArObj = new Array();
    ArObj[0] = 'Fm';
    ArObj[1] = 'Fd';
    ArObj[2] = 'Tm';
    ArObj[3] = 'Td';
    for (var i = 1; i <= 12; i++) {
        CreateNumericUpDown_CalculationRange(i, monthsDayCol[i - 1], ArObj);
    }
}

function CreateNumericUpDown_CalculationRange(month, monthDayCount, ArObj) {
    for (var i = 0; i < ArObj.length; i++) {
        var NudID = "Nud" + ArObj[i] + "" + month + "";
        var Nud = new SpinControl(NudID);
        document.getElementById("tdNud" + ArObj[i] + "" + month + "_CalculationRange").appendChild(Nud.GetContainer());
        AddNudFeatures_CalculationRange(Nud, month, monthDayCount, i, GetCreator_NumericUpDown_CalculationRange(), GetCreatorMeter_NumericUpDown_CalculationRange(i));
        Nud.StartListening();
    }
}

function GetCreator_NumericUpDown_CalculationRange() {
    if (NudCreator_CalculationRange == null)
        NudCreator_CalculationRange = 'm';
    else {
        if (NudCreator_CalculationRange == 'm')
            NudCreator_CalculationRange = 'd';
        else
            NudCreator_CalculationRange = 'm';
    }
    return NudCreator_CalculationRange;
}

function GetCreatorMeter_NumericUpDown_CalculationRange(index) {
    var NudcreatorMeter_CalculationRange = null;
    if (index < 2)
        NudcreatorMeter_CalculationRange = 'F';
    else
        NudcreatorMeter_CalculationRange = 'T';
    return NudcreatorMeter_CalculationRange;
}

function AddNudFeatures_CalculationRange(Nud, month, monthDayCount, index, creator, creatorMeter) {
    if (month == 1 && index == 0) {
        Nud.SetFirstExceptionOccured(true);
        FirstExceptionNudObj_CalculationRange = new Object();
        FirstExceptionNudObj_CalculationRange.ID = "Nud" + creatorMeter + "" + creator + "" + month + "";
        FirstExceptionNudObj_CalculationRange.Obj = Nud;
    }
    if (month == 12 && index == 2) {
        Nud.SetLastExceptionOccured(true);
        LastExceptionNudObj_CalculationRange = new Object();
        LastExceptionNudObj_CalculationRange.ID = "Nud" + creatorMeter + "" + creator + "" + month + "";
        LastExceptionNudObj_CalculationRange.Obj = Nud;
    }

    var MinVal = null;
    var MaxVal = null;
    var CurrentVal = null;
    switch (creator) {
        case 'm':
            switch (creatorMeter) {
                case 'F':
                    MinVal = month - 1;
                    MaxVal = month;
                    break;
                case 'T':
                    MinVal = month;
                    MaxVal = month + 1;
                    break;
            }
            CurrentVal = month;
            break;
        case 'd':
            switch (creatorMeter) {
                case 'F':
                    CurrentVal = 1;
                    break;
                case 'T':
                    CurrentVal = monthDayCount;
                    break;
            }
            MinVal = 1;
            MaxVal = monthDayCount;
            break;
    }
    Nud.SetMinValue("" + MinVal + "");
    Nud.SetMaxValue("" + MaxVal + "");
    Nud.SetCurrentValue("" + CurrentVal + "");
}

function IncreaseAll_CalculationRange() {
    for (var i = 1; i <= 12; i++) {
        if (document.getElementById("NudFm" + i + "").value == "" + (i - 1) + "" || (i == 1 && document.getElementById("NudFm" + i + "").value == '12')) {
            var MaxFirstMonth_CalculationRange = null;
            if (i == 1 && document.getElementById("NudFm" + i + "").value == '12')
                MaxFirstMonth_CalculationRange = parseInt(MonthsDayCol_CalculationRange[MonthsDayCol_CalculationRange - 1]);
            else
                MaxFirstMonth_CalculationRange = parseInt(MonthsDayCol_CalculationRange[i - 2]);
            if (parseInt(document.getElementById("NudFd" + i + "").value) < MaxFirstMonth_CalculationRange) {
                document.getElementById("NudFd" + i + "").value = "" + (parseInt(document.getElementById("NudFd" + i + "").value) + 1) + "";
                if (parseInt(document.getElementById("NudTd" + i + "").value) < parseInt(MonthsDayCol_CalculationRange[i - 1]))
                    document.getElementById("NudTd" + i + "").value = "" + (parseInt(document.getElementById("NudTd" + i + "").value) + 1) + "";
            }
            else {
                document.getElementById("NudFd" + i + "").value = '1';
                document.getElementById("NudFm" + i + "").value = "" + i + "";
                if (parseInt(document.getElementById("NudTd" + i + "").value) < parseInt(MonthsDayCol_CalculationRange[i - 1]))
                    document.getElementById("NudTd" + i + "").value = "" + (parseInt(document.getElementById("NudTd" + i + "").value) + 1) + "";
                else {
                    document.getElementById("NudTd" + i + "").value = '1';
                    document.getElementById("NudTm" + i + "").value = "" + (i + 1) + "";
                }
            }
        }
        else {
            if (parseInt(document.getElementById("NudFd" + i + "").value) < parseInt(MonthsDayCol_CalculationRange[i - 1]))
                document.getElementById("NudFd" + i + "").value = "" + (parseInt(document.getElementById("NudFd" + i + "").value) + 1) + "";
            if (parseInt(document.getElementById("NudTm" + i + "").value) == i) {
                if (parseInt(document.getElementById("NudTd" + i + "").value) < parseInt(MonthsDayCol_CalculationRange[i - 1]))
                    document.getElementById("NudTd" + i + "").value = "" + (parseInt(document.getElementById("NudTd" + i + "").value) + 1) + "";
                else {
                    document.getElementById("NudTd" + i + "").value = '1';
                    var LastMonth_CalculationRange = i + 1;
                    if (LastMonth_CalculationRange > 12) {
                        LastMonth_CalculationRange = 1;
                        Nud_OnExceptionOccured(LastExceptionNudObj_CalculationRange.Obj, 1);
                    }
                    document.getElementById("NudTm" + i + "").value = "" + LastMonth_CalculationRange + "";
                }
            }
            else {
                var MaxLastMonth_CalculationRange = null;
                if (i < 12)
                    MaxLastMonth_CalculationRange = parseInt(MonthsDayCol_CalculationRange[i]);
                else {
                    MaxLastMonth_CalculationRange = parseInt(MonthsDayCol_CalculationRange[0]);
                    Nud_OnExceptionOccured(LastExceptionNudObj_CalculationRange.Obj, 1);
                }
                if (parseInt(document.getElementById("NudTd" + i + "").value) < MaxLastMonth_CalculationRange) {
                    if (i < 12) {
                        if (parseInt(document.getElementById("NudFd" + (i + 1) + "").value) < MaxLastMonth_CalculationRange)
                            document.getElementById("NudTd" + i + "").value = "" + (parseInt(document.getElementById("NudTd" + i + "").value) + 1) + "";
                    }
                    else
                        document.getElementById("NudTd" + i + "").value = "" + (parseInt(document.getElementById("NudTd" + i + "").value) + 1) + "";
                }
            }
        }
    }
}

function DecreaseAll_CalculationRange() {
    for (var i = 1; i <= 12; i++) {
        if (parseInt(document.getElementById("NudTm" + i + "").value) > i || (i == 12 && document.getElementById("NudTm" + i + "").value == 1)) {
            if (parseInt(document.getElementById("NudTd" + i + "").value) > 1) {
                document.getElementById("NudTd" + i + "").value = "" + (parseInt(document.getElementById("NudTd" + i + "").value) - 1) + "";
                if (parseInt(document.getElementById("NudFd" + i + "").value) > 1)
                    document.getElementById("NudFd" + i + "").value = "" + (parseInt(document.getElementById("NudFd" + i + "").value) - 1) + "";
            }
            else {
                var BeforeMonth_CalculationRange = null;
                if ((i == 12 && document.getElementById("NudTm" + i + "").value == '1')) {
                    BeforeMonth_CalculationRange = '12';
                    Nud_OnExceptionOccured(LastExceptionNudObj_CalculationRange.Obj, -1);
                }
                else
                    BeforeMonth_CalculationRange = "" + (parseInt(document.getElementById("NudTm" + i + "").value - 1)) + "";
                document.getElementById("NudTm" + i + "").value = BeforeMonth_CalculationRange;
                document.getElementById("NudTd" + i + "").value = MonthsDayCol_CalculationRange[i - 1];
                if (parseInt(document.getElementById("NudFd" + i + "").value) > 1)
                    document.getElementById("NudFd" + i + "").value = "" + (parseInt(document.getElementById("NudFd" + i + "").value) - 1) + "";
                else {
                    if (i == 1)
                        document.getElementById("NudFm" + i + "").value = '12';
                    else
                        document.getElementById("NudFm" + i + "").value = "" + (parseInt(document.getElementById("NudFm" + i + "").value) - 1) + "";
                    document.getElementById("NudFd" + i + "").value = MonthsDayCol_CalculationRange[parseInt(document.getElementById("NudFm" + i + "").value) - 1];
                }
            }
        }
        else {
            if (parseInt(document.getElementById("NudTd" + i + "").value) > 1) {
                document.getElementById("NudTd" + i + "").value = "" + (parseInt(document.getElementById("NudTd" + i + "").value) - 1) + "";
                if (parseInt(document.getElementById("NudFd" + i + "").value) > 1) {
                    var Blocked_Decrease = false;
                    if (i > 1) {
                        if (document.getElementById("NudFm" + i + "").value == document.getElementById("NudTm" + (i - 1) + "").value && document.getElementById("NudFd" + i + "").value == "" + (parseInt(document.getElementById("NudTd" + (i - 1) + "").value) + 1) + "")
                            Blocked_Decrease = true;
                    }
                    if (!Blocked_Decrease)
                        document.getElementById("NudFd" + i + "").value = "" + (parseInt(document.getElementById("NudFd" + i + "").value) - 1) + "";
                }
                else {
                    var BeforeMonth_CalculationRange = null;
                    if (i == 1)
                        BeforeMonth_CalculationRange = '12';
                    else
                        BeforeMonth_CalculationRange = "" + (parseInt(document.getElementById("NudFm" + i + "").value) - 1) + "";
                    document.getElementById("NudFm" + i + "").value = BeforeMonth_CalculationRange;
                    document.getElementById("NudFd" + i + "").value = MonthsDayCol_CalculationRange[parseInt(document.getElementById("NudFm" + i + "").value) - 1];
                }
            }
            else {
                if (document.getElementById("NudTm" + i + "").value == "" + i + "")
                    if (parseInt(document.getElementById("NudFd" + i + "").value) > 1) {
                        var Blocked_Decrease = false;
                        if (i > 1) {
                            if (document.getElementById("NudFm" + i + "").value == document.getElementById("NudTm" + (i - 1) + "").value && document.getElementById("NudFd" + i + "").value == "" + (parseInt(document.getElementById("NudTd" + (i - 1) + "").value) + 1) + "")
                                Blocked_Decrease = true;
                        }
                        if (!Blocked_Decrease)
                            document.getElementById("NudFd" + i + "").value = "" + (parseInt(document.getElementById("NudFd" + i + "").value) - 1) + "";
                    }
            }
        }
    }
}

function tlbItemEndorsement_TlbCalculationRange_onClick() {
    UpdateCaculationRange_CalculationRange();
}

function UpdateCaculationRange_CalculationRange() {
    ObjCalculationRange_CalculationRange = new Object();
    ObjCalculationRange_CalculationRange.Name = null;
    ObjCalculationRange_CalculationRange.Description = null;
    ObjCalculationRange_CalculationRange.ID = '0';
    ObjCalculationRange_CalculationRange.State = CurrentPageState_CalculationRange;
    var SelectedCalculationRangeID_CalculationRange = parent.DialogCalculationRange.get_value().CalculationRangeID;
    if (SelectedCalculationRangeID_CalculationRange != "")
        ObjCalculationRange_CalculationRange.ID = SelectedCalculationRangeID_CalculationRange;

    ObjCalculationRange_CalculationRange.Name = document.getElementById('txtCalculationRangeName_CalculationRange').value;
    ObjCalculationRange_CalculationRange.Description = document.getElementById('txtDescription_CalculationRange').value;

    UpdateCaculationRange_CalculationRangePage(CharToKeyCode_CalculationRange(CurrentPageState_CalculationRange), CharToKeyCode_CalculationRange(ObjCalculationRange_CalculationRange.ID), CharToKeyCode_CalculationRange(ObjCalculationRange_CalculationRange.Name), CharToKeyCode_CalculationRange(ObjCalculationRange_CalculationRange.Description), CharToKeyCode_CalculationRange(ConceptsListObj_CalculationRange), CharToKeyCode_CalculationRange(DefaultCalculationRangesList_CalculationRange), CharToKeyCode_CalculationRange(GetCalculationRangesList_CalculationRange()));
    DialogWaiting.Show();
}

function UpdateCaculationRange_CalculationRangePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();        
        if (RetMessage[2] == 'success') {
            ObjCalculationRange_CalculationRange.ID = Response[3];
            parent.document.getElementById('pgvCalculationRangeDefinition_iFrame').contentWindow.CalculationRange_onOperationComplete(Response, ObjCalculationRange_CalculationRange);
            CloseDialogCalculationRange();
        }
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function GetDefaultCalculationRangesList_CalculationRange() {
    DefaultCalculationRangesList_CalculationRange = GetCalculationRangesList_CalculationRange();
}

function GetCalculationRangesList_CalculationRange() {
    var CalculationRangesList_CalculationRange = "";
    for (var i = 1; i < 13; i++) {
        var splitter = ',';
        if (i == 12)
            splitter = '';
        var CalculationRangesListPart_CalculationRange = '{"M":"Month' + i + '","Fm":"' + document.getElementById('NudFm' + i + '').value + '","Fd":"' + document.getElementById('NudFd' + i + '').value + '","Tm":"' + document.getElementById('NudTm' + i + '').value + '","Td":"' + document.getElementById('NudTd' + i + '').value + '"}';
        CalculationRangesList_CalculationRange += CalculationRangesListPart_CalculationRange + splitter;
    }
    CalculationRangesList_CalculationRange = '[' + CalculationRangesList_CalculationRange + ']';
    return CalculationRangesList_CalculationRange;
}

function GridConcepts_CalculationRange_onItemCheckChange(sender, e) {
    conceptItem_GridConcepts_CalculationRange = e.get_item();
    var conceptItemChecked_CalculationRange = conceptItem_GridConcepts_CalculationRange.getMember('IsUsedByDateRange').get_text();
    var conceptItemID_CalculationRange = conceptItem_GridConcepts_CalculationRange.getMember('ID').get_text();
    switch (conceptItemChecked_CalculationRange) {
        case 'true':
            if (ConceptsListObj_CalculationRange.indexOf('' + conceptItemID_CalculationRange + '#') >= 0)
                ConceptsListObj_CalculationRange = ConceptsListObj_CalculationRange.replace('' + conceptItemID_CalculationRange + '#', '');
            break;
        case 'false':
            if (ConceptsListObj_CalculationRange.indexOf('' + conceptItemID_CalculationRange + '#') == -1)
                ConceptsListObj_CalculationRange += '' + conceptItemID_CalculationRange + '#';
            break;
        default:
            if (ConceptsListObj_CalculationRange.indexOf('' + conceptItemID_CalculationRange + '#') == -1)
                ConceptsListObj_CalculationRange += '' + conceptItemID_CalculationRange + '#';
            break;
    }
}

function tlbItemExit_TlbCalculationRange_onClick() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_CalculationRange').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDialogCalculationRange();
}

function CloseDialogCalculationRange() {
    parent.document.getElementById('DialogCalculationRange_IFrame').src = "WhitePage.aspx";
    parent.DialogCalculationRange.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function GridConcepts_CalculationRange_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridConcepts_CalculationRange').innerHTML = '';
}

function SetActionMode_CalculationRange() {
    var CurrentStateObj_CalculationRange = parent.DialogCalculationRange.get_value();
    CurrentPageState_CalculationRange = CurrentStateObj_CalculationRange.State;
    document.getElementById('ActionMode_CalculationRange').innerHTML = document.getElementById('hf' + CurrentPageState_CalculationRange + '_CalculationRange').value;
    if (CurrentStateObj_CalculationRange.State == 'Edit')
        NavigateCalculationRange_CalculationRange(CurrentStateObj_CalculationRange);
    Fill_GridConcepts_CalculationRange(CurrentStateObj_CalculationRange.State);
}

function NavigateCalculationRange_CalculationRange(CurrentStateObj_CalculationRange) {
    document.getElementById('txtCalculationRangeName_CalculationRange').value = CurrentStateObj_CalculationRange.CalculationRangeName;
    var CalculationRangeDescription_CalculationRange = "";
    if (CurrentStateObj_CalculationRange.CalculationRangeDescription != undefined)
        document.getElementById('txtDescription_CalculationRange').value = CurrentStateObj_CalculationRange.CalculationRangeDescription;
}

function chbSelectAll_CalculationRange_onClick() {
    var checkState = null;
    if (document.getElementById('chbSelectAll_CalculationRange').checked)
        checkState = 'CheckAll';
    else
        checkState = 'UnCheckAll';
    Fill_GridConcepts_CalculationRange(checkState);
}

function Fill_GridConcepts_CalculationRange(state) {
    ConceptsListObj_CalculationRange = "";    
    document.getElementById('loadingPanel_GridConcepts_CalculationRange').innerHTML = document.getElementById('hfloadingPanel_GridConcepts_CalculationRange').value;
    CallBack_GridConcepts_CalculationRange.callback(state);
}

function CallBack_GridConcepts_CalculationRange_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_CalculationRange').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridConcepts_CalculationRange();
    }
    else {
        var checkList = document.getElementById('CheckListHiddenField_CalculationRange').value;
        if (checkList != "")
            ConceptsListObj_CalculationRange = checkList;
    }
}

function CharToKeyCode_CalculationRange(str) {
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
    ConfirmState_CalculationRange = confirmState;
    if (CurrentPageState_MasterCalculationRange == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_CalculationRange').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_CalculationRange').value;
    DialogConfirm.Show();
}

function Refresh_GridConcepts_CalculationRange() {
    Fill_GridConcepts_CalculationRange(parent.DialogCalculationRange.get_value().State);
}   

function GridConcepts_CalculationRange_onItemSelect(sender, e) {
    if (CurrentPageState_CalculationRange != 'Add')
        NavigateCalculationRanges_CalculationRange(e.get_item());
}

function NavigateCalculationRanges_CalculationRange(conceptItem) { 
    if(conceptItem != undefined){
        GetCalculationRangesOnDemand_CalculationRangePage(CharToKeyCode_CalculationRange(parent.DialogCalculationRange.get_value().CalculationRangeID), CharToKeyCode_CalculationRange(conceptItem.getMember('ID').get_text()));
        DialogWaiting.Show();
    }
}

function GetCalculationRangesOnDemand_CalculationRangePage_onCallBack(Response) {
    var strCalculationRangeList = Response;
    if (Response != null) {
        DialogWaiting.Close();
        var CalculationRangeList = eval('(' + strCalculationRangeList + ')');
        for (var i = 0; i < CalculationRangeList.length; i++) {
            var monthIndex = CalculationRangeList[i].M;
            document.getElementById('NudFm' + monthIndex + '').value = CalculationRangeList[i].Fm;
            document.getElementById('NudFd' + monthIndex + '').value = CalculationRangeList[i].Fd;
            document.getElementById('NudTm' + monthIndex + '').value = CalculationRangeList[i].Tm;
            document.getElementById('NudTd' + monthIndex + '').value = CalculationRangeList[i].Td;
        }
    }
 }

 function CallBack_GridConcepts_CalculationRange_onCallbackError(sender, e) {
     ShowConnectionError_CalculationRange();
 }

 function ShowConnectionError_CalculationRange() {
     var error = document.getElementById('hfErrorType_CalculationRange').value;
     var errorBody = document.getElementById('hfConnectionError_CalculationRange').value;
     showDialog(error, errorBody, 'error');
 }

 function tlbItemDecreaseAll_TlbCalculationRange_onClick() {
     DecreaseAll_CalculationRange();
 }

 function tlbItemIncreaseAll_TlbCalculationRange_onClick() {
     IncreaseAll_CalculationRange();
 }

 function tlbItemFormReconstruction_TlbCalculationRange_onClick() {
     CloseDialogCalculationRange();
     parent.document.getElementById('pgvCalculationRangeDefinition_iFrame').contentWindow.ShowDialogCalculationRange();
 }








