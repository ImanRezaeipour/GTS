$(document).ready
        (
            function () {
                document.body.dir = document.UiValidationRulesForm.dir;
                SetWrapper_Alert_Box(document.UiValidationRulesForm.id);
                GetBoxesHeaders_UiValidationRules();
                Fill_ObjectUiValidationRules_UiValidationRules();
                Fill_GridUiValidationRules_UiValidationRules();
                //ViewCurrentLangCalendars_RuleParameters();
                ClearList_RuleParameters();
            }
        );