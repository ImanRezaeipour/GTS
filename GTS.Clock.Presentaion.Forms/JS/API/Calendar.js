
var CurrentGregorianYear =  2026;
var CurrentPersianYear = 1404 ;
var CurrentCalendarType = "Persian";
var GREGORIAN_EPOCH = 1721425.5;
var PERSIAN_EPOCH = 1948320.5;

function IsLeapYear(year) {
    var yearIsLeap = false;
//    if (status == "Persian") {
//        yRemain = year % 33;
//        if (yRemain == 1 || yRemain == 5 || yRemain == 9 || yRemain == 13 || yRemain == 17 || yRemain == 22 || yRemain == 26 || yRemain == 30)
//            yearIsLeap = true;
//    }
//    if (status == "Gregorian") {
        if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
            yearIsLeap = true;
        //   }
        return yearIsLeap; 
}

function GetCurrentGregorianMonthDayCount(Year,Month)
{
      DayCount = null;
      if (Month == 0 || Month == 2 || Month == 0 || Month == 4 || Month == 6 || Month == 7 || Month == 9 || Month == 11)
         DayCount = 31;
     if (Month == 3 || Month == 5 || Month == 8 || Month == 10)
         DayCount = 30;
     if (Month == 1) {
         var LeapYear = IsLeapYear(Year);
          if (LeapYear)
            DayCount = 29;
         else
            DayCount = 28;
      }
      return DayCount;
}


function FillPersianCal() {
    var DayCount;
    var PersianDate = null;
    var CurrentOperationYear;
    var DayNumber;

    while (PersianDate == null || PersianDate[0] == CurrentPersianYear) {
    
        if (PersianDate == null)
            CurrentOperationYear = CurrentGregorianYear - 1;
        else
            CurrentOperationYear = CurrentGregorianYear;
            
        for (var i = 0; i < 12; i++) {
            
            DayCount = GetCurrentGregorianMonthDayCount(CurrentOperationYear,i);
            for (var j = 1; j <= DayCount; j++) {

                PersianDate = CalculateGregorian(CurrentOperationYear, i, j);
                
                if(j == 1)
                {
                   var date = new Date(CurrentOperationYear, i, j);
                   DayNumber = date.getDay();
                   InitializePersianDayNumber(DayNumber,"set");
                }
                else
                   DayNumber = InitializePersianDayNumber(null,"get");
                   
                if (DayNumber == 0)
                    DayNumber = 7;

//                if (i == 1 && j == 1)
//                    alert(""+DayCount+""+CurrentOperationYear+"");
                  //alert("" + PersianDate[0] + "" + PersianDate[1] + "" + PersianDate[2] + "---"+DayNumber+"");

                    
                if (PersianDate[0] == CurrentPersianYear)
                        FillMonthRelativeTable(PersianDate[1], PersianDate[2], DayNumber);
                    if (PersianDate[0] == CurrentPersianYear + 1)
                        return;
                }
        }
    }
}
var NextPersianDayNumber;
function InitializePersianDayNumber(FirstDayofMonthNumber,state)
{
    var StartofMonth;
    if(state == "set")
    {
        StartofMonth = FirstDayofMonthNumber;
        NextPersianDayNumber = StartofMonth;
    }
    if(state == "get")
    {
        if(NextPersianDayNumber == 6)
           {
              NextPersianDayNumber = 0;
              return NextPersianDayNumber;
           }
        NextPersianDayNumber++;
        return NextPersianDayNumber;
    }
}

function SetPersianMonthRowPointer(DayNumber) {
    var PersianMonthRowPointer;
   if (DayNumber <= 5)
       PersianMonthRowPointer = DayNumber + 2;
   else
       PersianMonthRowPointer = DayNumber - 5;
   return PersianMonthRowPointer;   
}

var PersianMonthColPointer;
var bm;
function FillMonthRelativeTable(Month, Day, DayNumber) {
        if (bm < Month || bm == null)
            PersianMonthColPointer = 1;
        bm = Month;

        var PersianMonthRowPointer = SetPersianMonthRowPointer(DayNumber);
        //alert("tdcal" + Month + "" + PersianMonthColPointer + "" + PersianMonthRowPointer + "="+Day+"");
        document.getElementById("tdcal" + Month + "" + PersianMonthColPointer + "" + PersianMonthRowPointer + "").innerHTML = Day;
        if (DayNumber == 5)
            PersianMonthColPointer++;
}

function CalculateGregorian(year, Month, Day) {
    var j = GregorianDate_to_JulianDate(year, Month + 1, Day) + (Math.floor(0 + 60 * (0 + 60 * 0) + 0.5) / 86400.0);

    var PersianDate = JulianDate_to_PersianDate(j);
    return new Array(PersianDate[0], PersianDate[1], PersianDate[2]);    
}

function GregorianDate_to_JulianDate(year, Month, Day) {
    return (GREGORIAN_EPOCH - 1) +
           (365 * (year - 1)) +
           Math.floor((year - 1) / 4) +
           (-Math.floor((year - 1) / 100)) +
           Math.floor((year - 1) / 400) +
           Math.floor((((367 * Month) - 362) / 12) +
           ((Month <= 2) ? 0 :
                               (IsLeapYear(year) ? -1 : -2)
           ) +
           Day);
}

function JulianDate_to_PersianDate(jd) {
    var year, month, day, depoch, cycle, cyear, ycycle,
        aux1, aux2, yday;
        
    jd = Math.floor(jd) + 0.5;

    depoch = jd - PersianDate_to_JulianDate(475, 1, 1);
    cycle = Math.floor(depoch / 1029983);
    cyear = mod(depoch, 1029983);
    if (cyear == 1029982) {
        ycycle = 2820;
    } else {
        aux1 = Math.floor(cyear / 366);
        aux2 = mod(cyear, 366);
        ycycle = Math.floor(((2134 * aux1) + (2816 * aux2) + 2815) / 1028522) +
                    aux1 + 1;
    }
    year = ycycle + (2820 * cycle) + 474;
    if (year <= 0) {
        year--;
    }
    yday = (jd - PersianDate_to_JulianDate(year, 1, 1)) + 1;
    month = (yday <= 186) ? Math.ceil(yday / 31) : Math.ceil((yday - 6) / 30);
    day = (jd - PersianDate_to_JulianDate(year, month, 1)) + 1;
    return new Array(year, month, day);
}


function PersianDate_to_JulianDate(year, month, day) {
    var epbase, epyear = null;
     
    epbase = year - ((year >= 0) ? 474 : 473);
    epyear = 474 + mod(epbase, 2820);

    return day +
            ((month <= 7) ?
                ((month - 1) * 31) :
                (((month - 1) * 30) + 6)
            ) +
            Math.floor(((epyear * 682) - 110) / 2816) +
            (epyear - 1) * 365 +
            Math.floor(epbase / 2820) * 1029983 +
            (PERSIAN_EPOCH - 1);
}

function mod(a, b) {
    return a - (b * Math.floor(a / b));
}








