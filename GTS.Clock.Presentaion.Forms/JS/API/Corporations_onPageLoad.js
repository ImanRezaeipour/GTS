


$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.CorporationsForm.dir;
                SetWrapper_Alert_Box(document.CorporationsForm.id);
                GetBoxesHeaders_Corporations();
                SetActionMode_Corporations('View');
                Fill_GridCorporations_Corporations();

            }
        );
