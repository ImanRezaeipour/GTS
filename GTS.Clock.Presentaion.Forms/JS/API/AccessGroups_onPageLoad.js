

$(document).ready
        (
            function () {
                document.body.dir = document.AccessGroupsForm.dir;
                SetWrapper_Alert_Box(document.AccessGroupsForm.id);
                GetBoxesHeaders_AccessGroups();
                SetActionMode_AccessGroups('View');
                Fill_GridAccessGroups_AccessGroups();
            }
        );
