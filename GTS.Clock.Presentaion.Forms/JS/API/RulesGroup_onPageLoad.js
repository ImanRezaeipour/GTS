
$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.RulesGroupForm.dir;
                SetWrapper_Alert_Box(document.RulesGroupForm.id);
                SetActionMode_RulesGroup('View');
                GetBoxesHeaders_RulesGroup();
                Fill_trvRulesGroup_RulesGroup();
            }
        );
