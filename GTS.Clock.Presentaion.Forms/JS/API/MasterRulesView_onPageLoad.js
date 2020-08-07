
$(document).ready
        (
            function () {
                document.body.dir = document.MasterRulesViewForm.dir;
                SetWrapper_Alert_Box(document.MasterRulesViewForm.id);
                GetBoxesHeaders_MasterRulesView();
            }
        );
