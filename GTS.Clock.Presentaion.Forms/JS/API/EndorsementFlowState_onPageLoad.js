
$(document).ready
        (
            function () {
                document.body.dir = document.EndorsementFlowStateForm.dir;
                SetWrapper_Alert_Box(document.EndorsementFlowStateForm.id);
                GetBoxesHeaders_EndorsementFlowState();
                Initialize_EndorsementFlowState();
                GetEndorsementFlowStates_EndorsementFlowState();
            }
        );
