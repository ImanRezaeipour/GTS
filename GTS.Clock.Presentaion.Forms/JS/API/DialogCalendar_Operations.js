
var CurrentActivetxtCal = null;
var IsDialogCalendarFocused = true;
var RowCounter = null;
var ColCounter = null;
var MonthCounter = null;
var BorderStyle_onFocus = "red 2px solid";
var BorderStyle_onBlur = "gray 1px double";
var DaysInYear = null;
var CurrentLangID = parent.CurrentLangID;
var StrWorkGroupShifts_Calendar = '';
var baseList_Calndar = '';

function SplitCurrentActivetxtCal()
{
   if (CurrentActivetxtCal.length == 9) {
       RowCounter = CurrentActivetxtCal.substring(8, 9);
       ColCounter = CurrentActivetxtCal.substring(7, 8);
       MonthCounter = CurrentActivetxtCal.substring(6, 7);
   }
   if (CurrentActivetxtCal.length == 10) {
       RowCounter = CurrentActivetxtCal.substring(9, 10);
       ColCounter = CurrentActivetxtCal.substring(8, 9);
       MonthCounter = CurrentActivetxtCal.substring(6, 8);
   }}

function GetBoxesHeaders_Calendar() {
    var DialogCalendarTitle = '';
    var calendarTitle = document.getElementById('hfTitle_DialogCalendar').value;
    var sender = parent.DialogCalendar.get_value().Sender;
    var titlePart = parent.DialogCalendar.get_value().GroupName;
    var year = parent.DialogCalendar.get_value().UIYear;
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            DialogCalendarTitle = calendarTitle + ' ' + titlePart + ' ' + year;
            break;
        case 'en-US':
            DialogCalendarTitle = year + ' ' + titlePart + ' ' + calendarTitle;
            break;
    }
    parent.document.getElementById('Title_DialogCalendar').innerHTML = DialogCalendarTitle;
}

function GetAxises_Calendar() {
    var error = document.getElementById('ErrorHiddenField_CalAxises_Calendar').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else {
        var MonthsAxises = null;
        var DaysAxises = null;
        var SplitedObjs = document.getElementById('hfCalAxises_Calendar').value.split('$');
        MonthsAxises = SplitedObjs[0].split('@');
        DaysAxises = SplitedObjs[1].split('@');
        for (var i = 0; i < MonthsAxises.length; i++) {
            document.getElementById("lblMonth" + (i + 1) + "").innerHTML = MonthsAxises[i];
        }
        for (var j = 0; j < DaysAxises.length; j++) {
            for (var k = 0; k < 3; k++) {
                document.getElementById("lblDay" + (j + 1) + "" + (k + 1) + "").innerHTML = DaysAxises[j];
            }
        }
    }
}

function CalendarPage_FillCal_onCallBack(Response) {
    arCal = Response;
    if (arCal != null && typeof (arCal) == "object" && arCal.length > 0) {
        DialogWaiting.Close();
            for (var i = 0; i < arCal.length; i++) {
                var objs = arCal[i].split("=");
                if (document.getElementById("" + objs[0] + "") != null)
                    document.getElementById("" + objs[0] + "").value = objs[1];
                else {
                    if(DaysInYear != 365 && DaysInYear != 366)
                       DaysInYear = i;
                }                
            }
            for (var j = 1; j <= 12; j++) {
                for (var m = 1; m <= 6; m++) {
                    for (var n = 1; n <= 7; n++) {
                        if (document.getElementById("txtcal" + j + "" + m + "" + n + "").value == "")
                            document.getElementById("txtcal" + j + "" + m + "" + n + "").style.visibility = "hidden";
                    }
                }
            }
            SwitchtoFirstActiveCellofMonth(1);
    }
    }

function DialogCalendar_onDownArrowKey() {
    if (CurrentActivetxtCal == null)
        SwitchtoFirstActiveCellofMonth(1);
    else {
        if (document.activeElement.id.indexOf('txtcal') == -1)
            return;

        SplitCurrentActivetxtCal();
           
        if (RowCounter != 7) {
            if (document.getElementById("txtcal" + MonthCounter + "" + ColCounter + "" + (parseInt(RowCounter) + 1) + "") != null && document.getElementById("txtcal" + MonthCounter + "" + ColCounter + "" + (parseInt(RowCounter) + 1) + "").style.visibility != "hidden") 
                document.getElementById("txtcal" + MonthCounter + "" + ColCounter + "" + (parseInt(RowCounter) + 1) + "").focus();
            else
                SwitchtoFirstActiveCellofMonth(parseInt(MonthCounter) + 1);
        }
        else {
            if (document.getElementById("txtcal" + MonthCounter + "" + (parseInt(ColCounter) + 1) + "1") != null && document.getElementById("txtcal" + MonthCounter + "" + (parseInt(ColCounter) + 1) + "1").style.visibility != "hidden") 
                document.getElementById("txtcal" + MonthCounter + "" + (parseInt(ColCounter) + 1) + "1").focus();
            else
                SwitchtoFirstActiveCellofMonth(parseInt(MonthCounter) + 1);
        }
    }
}

function DialogCalendar_onLeftArrowKey() {
    if (CurrentActivetxtCal == null) 
        SwitchtoFirstActiveCellofMonth(1);
    else {
        if (document.activeElement.id.indexOf('txtcal') == -1)
            return;

        SplitCurrentActivetxtCal();
        SwitchtoNextCurrentDayNumberofYear();
    }
}

function SwitchtoNextCurrentDayNumberofYear() {
    if (ColCounter == 6) {
        MonthCounter = parseInt(MonthCounter) + 1;
        ColCounter = 0;
    }
    if (MonthCounter == 13)
        MonthCounter = 1;

    if (document.getElementById("txtcal" + MonthCounter + "" + (parseInt(ColCounter) + 1) + "" + RowCounter + "").value != "") 
        document.getElementById("txtcal" + MonthCounter + "" + (parseInt(ColCounter) + 1) + "" + RowCounter + "").focus();
    else {
        if (parseInt(ColCounter) != 5) {
            if (document.getElementById("txtcal" + MonthCounter + "" + (parseInt(ColCounter) + 2) + "" + RowCounter + "").value == "") {
                MonthCounter = parseInt(MonthCounter) + 1;
                ColCounter = 0;
                SwitchtoNextCurrentDayNumberofYear();
            }
            else 
                document.getElementById("txtcal" + MonthCounter + "" + (parseInt(ColCounter) + 2) + "" + RowCounter + "").focus();
        }
        else {
            MonthCounter = parseInt(MonthCounter) + 1;
            ColCounter = 0;
            SwitchtoNextCurrentDayNumberofYear();
        }
    }
}

function DialogCalendar_onRightArrowKey() {
    if (CurrentActivetxtCal == null) {
        SwitchtoLastActiveCellofMonth(12);
    }
    else {
        if (document.activeElement.id.indexOf('txtcal') == -1)
            return;

        SplitCurrentActivetxtCal();
        SwitchtoBeforeCurrentDayNumberofYear();
    }
}

function SwitchtoBeforeCurrentDayNumberofYear() {
    if (ColCounter == 1) {
        MonthCounter = parseInt(MonthCounter) - 1;
        ColCounter = 6;
    }
    if (MonthCounter == 0)
        MonthCounter = 12;

    if (document.getElementById("txtcal" + MonthCounter + "" + (parseInt(ColCounter) - 1) + "" + RowCounter + "").value != "") 
        document.getElementById("txtcal" + MonthCounter + "" + (parseInt(ColCounter) - 1) + "" + RowCounter + "").focus();
    else {
        if (parseInt(ColCounter) != 2) {
            if (document.getElementById("txtcal" + MonthCounter + "" + (parseInt(ColCounter) - 2) + "" + RowCounter + "").value == "") {
                MonthCounter = parseInt(MonthCounter) - 1;
                ColCounter = 6;
                SwitchtoBeforeCurrentDayNumberofYear();
            }
            else 
                document.getElementById("txtcal" + MonthCounter + "" + (parseInt(ColCounter) - 2) + "" + RowCounter + "").focus();
        }
        else {
            MonthCounter = parseInt(MonthCounter) - 1;
            ColCounter = 6;
            SwitchtoBeforeCurrentDayNumberofYear();
        }
    }
}
  
function DialogCalendar_onUpArrowKey() {
    if (CurrentActivetxtCal == null)
        SwitchtoLastActiveCellofMonth(12);
    else {
        if (document.activeElement.id.indexOf('txtcal') == -1)
            return;

        SplitCurrentActivetxtCal();

        if (RowCounter != 1) {
            if (document.getElementById("txtcal" + MonthCounter + "" + ColCounter + "" + (parseInt(RowCounter) - 1) + "") != null && document.getElementById("txtcal" + MonthCounter + "" + ColCounter + "" + (parseInt(RowCounter) - 1) + "").style.visibility != "hidden") 
                document.getElementById("txtcal" + MonthCounter + "" + ColCounter + "" + (parseInt(RowCounter) - 1) + "").focus();
            else
                SwitchtoLastActiveCellofMonth(parseInt(MonthCounter) - 1);
        }
        else {
            if (document.getElementById("txtcal" + MonthCounter + "" + (parseInt(ColCounter) - 1) + "7") != null && document.getElementById("txtcal" + MonthCounter + "" + (parseInt(ColCounter) - 1) + "7").style.visibility != "hidden") 
                document.getElementById("txtcal" + MonthCounter + "" + (parseInt(ColCounter) - 1) + "7").focus();
            else
                SwitchtoLastActiveCellofMonth(parseInt(MonthCounter) - 1);
        }
    }
}

function SwitchtoLastActiveCellofMonth(Month) {
    if (Month == 0)
        Month = 12;
    for (var i = 6; i > 0; i--) {
        for (var j = 7; j > 0; j--) {
            if (document.getElementById("txtcal" + Month + "" + i + "" + j + "").value != "") {
                document.getElementById("txtcal" + Month + "" + i + "" + j + "").focus();
                i = 0;
                break;
            } 
        }
    }
}

function SwitchtoFirstActiveCellofMonth(Month) {
    if (Month == 13)
        Month = 1;
         for (var i = 1; i <= 7; i++) {
             if (document.getElementById("txtcal" + Month + "1" + i + "").value != "") {
                 document.getElementById("txtcal" + Month + "1" + i + "").focus();
                 if (Month == 1)
                     FirstDayofYearCell = "txtcal" + Month + "1" + i + "";
                     break;
              }
        }
 }

 function txtCalInDialogCalendar_onFocus(x) {
       CurrentActivetxtCal = x;
       document.getElementById(x).style.border = BorderStyle_onFocus;
 }

 function txtCalInDialogCalendar_onBlur() {
     if (document.activeElement.id.indexOf('txtcal') != -1 || (document.activeElement.id == '' && document.activeElement.innerHTML.indexOf('cmbTypes_DialogCalendar') == -1) || document.activeElement.nodeName == 'HTML' || ((navigator.userAgent.indexOf('Firefox') != -1 || navigator.userAgent.indexOf('Safari') != -1) && document.activeElement.nodeName == 'BODY')) {
         document.getElementById(CurrentActivetxtCal).style.border = BorderStyle_onBlur;
         //ResetShiftSelection_Calendar();
     }
 }

 function DialogCalendar_onPgDn() {
     if (document.activeElement.id.indexOf('txtcal') == -1)
         return;
     var CurrentDate = document.getElementById(CurrentActivetxtCal).value;
     SplitCurrentActivetxtCal();
     if (MonthCounter == 6 && CurrentDate == 31)
         CurrentDate = 30;
     if (MonthCounter == 11 && CurrentDate == 30 && DaysInYear == 365)
         CurrentDate = 29;
     if (MonthCounter == 12)
         MonthCounter = 0;
     for (var i = 1; i <= 6; i++) {
         for (var j = 1; j <= 7; j++) {
             if (document.getElementById("txtcal" + (parseInt(MonthCounter) + 1) + "" + i + "" + j + "")) {
                 if (document.getElementById("txtcal" + (parseInt(MonthCounter) + 1) + "" + i + "" + j + "").value == CurrentDate) {
                     document.getElementById("txtcal" + (parseInt(MonthCounter) + 1) + "" + i + "" + j + "").focus();
                     i = 7;
                     break;
                 }
             }
         }
     }
 }

 function DialogCalendar_onPgUp() {
     if (document.activeElement.id.indexOf('txtcal') == -1)
         return;
     var CurrentDate = document.getElementById(CurrentActivetxtCal).value;
     SplitCurrentActivetxtCal();
     if (MonthCounter == 1)
         MonthCounter = 13;
     for (var i = 1; i <= 6; i++) {
         for (var j = 1; j <= 7; j++) {
             if (document.getElementById("txtcal" + (parseInt(MonthCounter) - 1) + "" + i + "" + j + "") != null) {
                 if (document.getElementById("txtcal" + (parseInt(MonthCounter) - 1) + "" + i + "" + j + "").value == CurrentDate) {
                     document.getElementById("txtcal" + (parseInt(MonthCounter) - 1) + "" + i + "" + j + "").focus();
                     i = 7;
                     break;
                 }
             }
         }
     }
 }

 function Calendar_FillCal() {
     var CalendarYear = parent.DialogCalendar.get_value().Year;
     CalendarPage_FillCal(CalendarYear);
     DialogWaiting.Show();
 }

 function txtCalInDialogCalendar_ondblclick() {
     ChangeType_DialogCalendar();
 }

 function cmbTypes_DialogCalendar_onChange(sender, e) {
     ChangeType_DialogCalendar();
 }

 function ChangeType_DialogCalendar() {
     if (CurrentActivetxtCal != null) {
         if (cmbTypes_DialogCalendar.getSelectedItem() != undefined && cmbTypes_DialogCalendar.getSelectedItem() != null) {
             document.getElementById(CurrentActivetxtCal).style.backgroundColor = cmbTypes_DialogCalendar.getSelectedItem().get_value();
             document.getElementById(CurrentActivetxtCal).focus();
             ChangeGroupListStr_Calendar(cmbTypes_DialogCalendar.getSelectedItem().get_id());
         }
     }
 }


 function CreateBasePanel_Calendar() {
    var error = document.getElementById('ErrorHiddenField_BasePanel_Calendar').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else {
        baseList_Calndar = document.getElementById('hfBaseLists_Calendar').value;
        if (baseList_Calndar != "") {
            baseList_Calndar = eval('(' + baseList_Calndar + ')');
            for (var i = 0; i < baseList_Calndar.length; i++) {
                var panelPartID = 'tdShift' + i + 'backgroundColor';
                document.getElementById(panelPartID).innerHTML = i.toString();
                document.getElementById(panelPartID).style.backgroundColor = baseList_Calndar[i].Color;
                document.getElementById(panelPartID).title = baseList_Calndar[i].Name;
            }
        }
    }
}

function GetCalData_Calendar() {
    var error = document.getElementById('ErrorHiddenField_CalData_Calendar').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else {
        var CalDataViewList = document.getElementById('hfCalDataView_Calendar').value;
        if (CalDataViewList != "")
            CalDataViewList = eval('(' + CalDataViewList + ')');
        for (var i = 0; i < CalDataViewList.length; i++) {
            var txtCalID = 'txtcal' + CalDataViewList[i].M + '' + CalDataViewList[i].CPointer + '' + CalDataViewList[i].RPointer + '';
            document.getElementById(txtCalID).style.backgroundColor = CalDataViewList[i].SColor;
            document.getElementById(txtCalID).title = CalDataViewList[i].Title;
        }
    }
}

function tlbItemRgister_DialogCalendar_onClick() {
    CollapseControls_Calendar();
    UpdateCalendar_Calendar();   
}

function UpdateCalendar_Calendar() {
    var GroupID = parent.DialogCalendar.get_value().GroupID;
    var year = parent.DialogCalendar.get_value().UIYear;
    var sender = parent.DialogCalendar.get_value().Sender;
    var StrCalData = document.getElementById('hfCalData_Calendar').value;
    UpdateCalendar_CalendarPage(CharToKeyCode_Calendar(sender), CharToKeyCode_Calendar(GroupID), CharToKeyCode_Calendar(year), CharToKeyCode_Calendar(StrCalData));
    DialogWaiting.Show();
}

function UpdateCalendar_CalendarPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function CharToKeyCode_Calendar(str) {
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

function GetCurrentMonthNumber_Calendar() {
     monthNumber = CurrentActivetxtCal.substring(6, CurrentActivetxtCal.length - 2);
     return monthNumber;
}

function tlbItemExit_DialogCalendar_onClick() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Calendar').value;
    DialogConfirm.Show();
}

function CloseDialogCalendar() {
    parent.document.getElementById('DialogCalendar_IFrame').src = 'WhitePage.aspx';
    parent.DialogCalendar.Close();
}

function tlbItemRepeat_DialogCalendar_onClick() { 
}

function tlbItemHoliday_DialogCalendar_onClick() {
    cmbTypes_DialogCalendar.selectItemByIndex(1);
}

function tlbItemNotHoliday_DialogCalendar_onClick() {
    cmbTypes_DialogCalendar.selectItemByIndex(0);
}

function tlbItemOk_TlbOkConfirm_onClick() {    
    CloseDialogCalendar();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function SetTypeforCurrentActivetxtCal(Code) {
    if (CurrentActivetxtCal != null) {
        var strkey = String.fromCharCode(Code);
        if (parseInt(strkey) < baseList_Calndar.length) {
            cmbTypes_DialogCalendar.unSelect();
            cmbTypes_DialogCalendar.selectItemByIndex(parseInt(strkey));
        }
    }
}

function ChangeGroupListStr_Calendar(typeID) {
    var BaseStrPart = 'M=' + GetCurrentMonthNumber_Calendar() + '#D=' + document.getElementById(CurrentActivetxtCal).value;
    var StrCalData = document.getElementById('hfCalData_Calendar').value;
    if (StrCalData.indexOf(BaseStrPart) != -1) {
        StrCalData = StrCalData.replace(BaseStrPart + '#SID=', '');
        if (typeID != '0')
            StrCalData += BaseStrPart + '#SID=' + typeID + '%';
    }
    else {
        if (typeID != '0')
            StrCalData += BaseStrPart + '#SID=' + typeID + '%';
    }
    document.getElementById('hfCalData_Calendar').value = StrCalData;
}

function ResetShiftSelection_Calendar() {
    cmbTypes_DialogCalendar.unSelect();
}

function CollapseControls_Calendar() {
    cmbTypes_DialogCalendar.collapse();
}

function tlbItemFormReconstruction_tlbItemNotHoliday_DialogCalendar_onClick() {
    var ObjDialogCalendar = parent.DialogCalendar.get_value();
    var sender = ObjDialogCalendar.Sender;
    CloseDialogCalendar();
    switch (sender) {
        case 'WorkGroups':
            parent.document.getElementById('pgvWorkGroupsIntroduction_iFrame').contentWindow.ShowDialogCalendar('Normal');
            break;
        case 'Holidays':
            parent.document.getElementById('pgvYearlyHolidaysIntroduction_iFrame').contentWindow.ShowDialogCalendar();
            break;
    }
}

function ChangeDirection_Mastertbl_Calendar() {
    if (parent.CurrentLangID == 'en-US')
        document.getElementById('Mastertbl_Calendar').dir = document.getElementById('cmbTypes_DialogCalendar_DropDownContent').dir = document.getElementById('tblConfirm_DialogConfirm').dir = 'ltr';
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('Mastertbl_Calendar').dir = document.getElementById('cmbTypes_DialogCalendar_DropDownContent').dir = document.getElementById('tblConfirm_DialogConfirm').dir = 'rtl';
}

function tlbItemPeriodRepeat_DialogCalendar_onClick() {
    ShowDialogPeriodRepeat_DialogCalendar();
}

function ShowDialogPeriodRepeat_DialogCalendar() {
    var ObjDialogCalendar = parent.DialogCalendar.get_value();
    var ObjDialogPeriodRepeat = new Object();
    ObjDialogPeriodRepeat.Sender = ObjDialogCalendar.Sender;
    ObjDialogPeriodRepeat.Year = ObjDialogCalendar.Year;
    ObjDialogPeriodRepeat.UIYear = ObjDialogCalendar.UIYear;
    ObjDialogPeriodRepeat.GroupID = ObjDialogCalendar.GroupID;
    ObjDialogPeriodRepeat.CalData = document.getElementById('hfCalData_Calendar').value;
    DialogPeriodRepeat.set_value(ObjDialogPeriodRepeat);
    DialogPeriodRepeat.ContentUrl = 'PeriodRepeat.aspx?reload=' + (new Date()).getTime() + '&Year=' + CharToKeyCode_Calendar(ObjDialogCalendar.Year);
    DialogPeriodRepeat.Show();
}

function UpdatePeriodRepeat_Calendar(ObjDialogPeriodRepeat) {
    if (ObjDialogPeriodRepeat != null && ObjDialogPeriodRepeat != undefined) {
        var Sender = ObjDialogPeriodRepeat.Sender;
        var UIYear = ObjDialogPeriodRepeat.UIYear;
        var GroupID = ObjDialogPeriodRepeat.GroupID;
        var CalData = ObjDialogPeriodRepeat.CalData;
        var FromMonth = ObjDialogPeriodRepeat.FromMonth;
        var FromDay = ObjDialogPeriodRepeat.FromDay;
        var ToMonth = ObjDialogPeriodRepeat.ToMonth;
        var ToDay = ObjDialogPeriodRepeat.ToDay;
        var StrHolidaysList = ObjDialogPeriodRepeat.StrHolidaysList;
        UpdatePeriodRepeat_CalendarPage(CharToKeyCode_Calendar(Sender), CharToKeyCode_Calendar(UIYear), CharToKeyCode_Calendar(GroupID), CharToKeyCode_Calendar(CalData), CharToKeyCode_Calendar(FromMonth), CharToKeyCode_Calendar(FromDay), CharToKeyCode_Calendar(ToMonth), CharToKeyCode_Calendar(ToDay), CharToKeyCode_Calendar(StrHolidaysList));
        DialogWaiting.Show();
    }
}

function UpdatePeriodRepeat_CalendarPage_onCallBack(Response) {
    var RetMessage = Response;
    DialogWaiting.Close();
    if (RetMessage != null && RetMessage.length > 0 && RetMessage[2] == 'error') {
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
    else {
        CloseDialogCalendar();
        parent.document.getElementById('pgvWorkGroupsIntroduction_iFrame').contentWindow.ShowDialogCalendar('PeriodRepeat');
    }
}










