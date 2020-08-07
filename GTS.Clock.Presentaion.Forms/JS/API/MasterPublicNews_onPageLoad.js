
$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.MasterPublicNewsForm.dir;
                SetWrapper_Alert_Box(document.MasterPublicNewsForm.id);
                GetBoxesHeaders_MasterPublicNews();
                SetActionMode_MasterPublicNews('View');
                Fill_GridMasterPublicNews_MasterPublicNews();
            }
        );