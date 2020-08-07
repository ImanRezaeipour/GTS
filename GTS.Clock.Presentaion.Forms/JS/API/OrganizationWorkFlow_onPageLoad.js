

$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.OrganizationWorkFlowForm.dir;
                SetWrapper_Alert_Box(document.OrganizationWorkFlowForm.id);
                GetBoxesHeaders_OrganizationWorkFlow();
                SetPosition_DropDownDives_OrganizationWorkFlow();
                SetActionMode_OrganizationWorkFlow('View');
                Fill_GridWorkFlows_OrganizationWorkFlow('Normal');
            }
        );
