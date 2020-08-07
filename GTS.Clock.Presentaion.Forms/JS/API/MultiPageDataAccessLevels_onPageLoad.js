
$(document).ready
(
   function () {
       parent.parent.DialogLoading.Close();
       document.body.dir = document.MultiPageDataAccessLevelsForm.dir;
       SetWrapper_Alert_Box(document.MultiPageDataAccessLevelsForm.id);
       GetObjDataAccessLevel_MultiPageDataAccessLevels();
       ChangeTargetQuickSearchEnabled_MultiPageDataAccessLevels();
       GetBoxesHeaders_MultiPageDataAccessLevels();
       SetPageIndex_GridDataAccessLevelsSource_MultiPageDataAccessLevels(0);
       SetPageIndex_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(0);
   }
)
