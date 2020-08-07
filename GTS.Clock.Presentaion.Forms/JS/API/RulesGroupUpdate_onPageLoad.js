
$(document).ready
        (
            function () {
                document.body.dir = document.RulesGroupUpdateForm.dir;
                SetWrapper_Alert_Box(document.RulesGroupUpdateForm.id);
                SetActionMode_RulesGroupUpdate();
                GetBoxesHeaders_RulesGroupUpdate();
                Fill_trvRulesTemplates_RulesGroupUpdate();
                Fill_trvRules_RulesGroupUpdate();
            }
        );
