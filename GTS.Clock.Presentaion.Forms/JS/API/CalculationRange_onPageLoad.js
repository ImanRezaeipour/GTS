

$(document).ready
(
   function () {
       document.body.dir = document.CalculationRangeForm.dir;
       SetWrapper_Alert_Box(document.CalculationRangeForm.id);
       GetBoxesHeaders_CalculationRange();
       SetActionMode_CalculationRange();
       GetMonthFeatures_CalculationRange();
   }
)
