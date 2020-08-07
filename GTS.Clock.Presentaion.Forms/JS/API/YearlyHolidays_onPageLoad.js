

$(document).ready
(
   function () {
       parent.DialogLoading.Close();
       document.body.dir = document.YearlyHolidaysForm.dir;
       SetWrapper_Alert_Box(document.YearlyHolidaysForm.id);
       GetBoxesHeaders_YearlyHolidays();
       SetActionMode_YearlyHolidays('View');
       Fill_GridYearlyHolidays_YearlyHolidays();                
   }
)
