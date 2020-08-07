

function CalendarForm_onKeyDown(event) {
    //    var keyCode = (window.Event) ? event.which : event.keyCode;
    if (IsDialogCalendarFocused) {
        switch (event.keyCode) {
            case 40:
                DialogCalendar_onDownArrowKey();
                break;
            case 38:
                DialogCalendar_onUpArrowKey();
                break;
            case 39:
                if (CurrentLangID == "fa-IR")
                    DialogCalendar_onRightArrowKey();
                else
                    DialogCalendar_onLeftArrowKey();
                break;
            case 37:
                if (CurrentLangID == "fa-IR")
                    DialogCalendar_onLeftArrowKey();
                else
                    DialogCalendar_onRightArrowKey();                    
                break;
            case 33:
                DialogCalendar_onPgUp();
                break;
            case 34:
                DialogCalendar_onPgDn();
                break;                                
        }
        if ((event.keyCode > 47 && event.keyCode < 58))
            SetTypeforCurrentActivetxtCal(event.keyCode);
        else
            if (event.keyCode > 95 && event.keyCode < 106) {
                event.keyCode -= 48;
                SetTypeforCurrentActivetxtCal(event.keyCode);
            }

    }
}


