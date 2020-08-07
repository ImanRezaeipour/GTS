
$(document).ready
(
   function () {
       parent.DialogLoading.Close();
       document.body.dir = document.MasterCalculationRangeForm.dir;
       SetWrapper_Alert_Box(document.MasterCalculationRangeForm.id);
       GetBoxesHeaders_MasterCalculationRange();
       SetActionMode_MasterCalculationRange('View');
       Fill_GridCalculationRange_MasterCalculationRange();

   }
)
