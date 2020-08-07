
$(document).ready
(
   function () {
       parent.parent.DialogLoading.Close();
       document.body.dir = document.SinglePageDataAccessLevelsForm.dir;
       SetWrapper_Alert_Box(document.SinglePageDataAccessLevelsForm.id);
       GetObjDataAccessLevel_SinglePageDataAccessLevels();
       GetBoxesHeaders_SinglePageDataAccessLevels();
       Fill_GridDataAccessLevelsSource_SinglePageDataAccessLevels();
       Fill_GridDataAccessLevelsTarget_SinglePageDataAccessLevels();
   }
)

