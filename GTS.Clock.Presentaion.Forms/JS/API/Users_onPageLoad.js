

$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.UsersForm.dir;
                SetWrapper_Alert_Box(document.UsersForm.id);
                GetBoxesHeaders_Users();
                SetPosition_dropdowndive_Users();
                ChangeEnabled_DropDownDive_Users('imgbox_SearchByPersonnel_Users', 'disabled');
                SetActionMode_Users('View');
                SetPageIndex_GridUsers_Users(0);
            }
        );






