

$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.MasterLeaveRemainsForm.dir;
                SetWrapper_Alert_Box(document.MasterLeaveRemainsForm.id);
                GetBoxesHeaders_MasterLeaveRemains();
                SetActionMode_MasterLeaveRemains('View');
                Init_TimeSelectors_MasterLeaveRemains();
                SetPosition_cmbPersonnel_MasterLeaveRemains();
                SetPageIndex_GridMasterLeaveRemains_MasterLeaveRemains(0);
            }
        );
