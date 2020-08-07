
$(document).ready
(
   function () {
       document.body.dir = document.MasterDataAccessLevelsForm.dir;
       SetWrapper_Alert_Box(document.MasterDataAccessLevelsForm.id);
       GetBoxesHeaders_MasterDataAccessLevels();
       SetUserCount_MasterDataAccessLevels();
   }
)

