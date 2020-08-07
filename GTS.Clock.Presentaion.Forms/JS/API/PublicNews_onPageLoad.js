
$(document).ready
(
   function () {
       document.body.dir = document.PublicNewsForm.dir;
       SetWrapper_Alert_Box(document.PublicNewsForm.id);
       GetBoxesHeaders_PublicNews();
       GetErrorMessage_PublicNews();
   }
)