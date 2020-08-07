

$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.OrganizationPostsForm.dir;
                SetWrapper_Alert_Box(document.OrganizationPostsForm.id);
                GetBoxesHeaders_Posts();
                SetActionMode_Posts('View');
                Fill_trvPosts_Posts();
            }
        );
