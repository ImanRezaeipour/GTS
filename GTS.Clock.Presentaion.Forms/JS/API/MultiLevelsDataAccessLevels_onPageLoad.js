
$(document).ready
(
   function () {
       parent.parent.DialogLoading.Close();
       document.body.dir = document.MultiLevelsDataAccessLevelsForm.dir;
       SetWrapper_Alert_Box(document.MultiLevelsDataAccessLevelsForm.id);
       GetObjDataAccessLevel_MultiLevelsDataAccessLevels();
       GetBoxesHeaders_MultiLevelsDataAccessLevels();
       Fill_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels();
       Fill_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels();
   }
)
