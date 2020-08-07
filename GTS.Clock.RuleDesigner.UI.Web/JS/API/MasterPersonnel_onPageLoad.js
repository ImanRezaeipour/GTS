

$(document).ready
        (
            function() {
                parent.DialogLoading.Close();            
                document.body.dir = document.MasterPersonnelMainInformationForm.dir;
                SetHorizontalScrollingDirection_GridPersonnel_Personnel_Opera();
                SetWrapper_Alert_Box(document.MasterPersonnelMainInformationForm.id);
                GetBoxesHeaders_Personnel();
                SetActionMode_Personnel('View');
                SetPageIndex_GridPersonnel_Personnel(0);
            }
        );
