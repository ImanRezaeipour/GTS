

$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.WorkGroupsForm.dir;
                SetWrapper_Alert_Box(document.WorkGroupsForm.id);
                GetBoxesHeaders_WorkGroup();
                SetActionMode_WorkGroup('View');
                Fill_GridWorkGroups_WorkGroups();                
            }
        );
