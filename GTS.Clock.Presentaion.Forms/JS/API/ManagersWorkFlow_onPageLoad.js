
$(document).ready
        (
            function () {
                document.body.dir = document.ManagersWorkFlowForm.dir;
                SetWrapper_Alert_Box(document.ManagersWorkFlowForm.id);
                GetBoxesHeaders_ManagersWorkFlow();
                CheckFlowIsActive_ManagersWorkFlow();
                CheckFlowIsMain_ManagersWorkFlow();
                Fill_GridManagersWorkFlow_ManagersWorkFlow();
                Fill_trvOrganizationPosts_ManagersWorkFlow(true);
            }
        );
