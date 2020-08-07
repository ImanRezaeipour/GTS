$(document).ready
        (
            function () {
                document.body.dir = document.DefineUiValidationForm.dir;
                SetWrapper_Alert_Box(document.DefineUiValidationForm.id);
                GetBoxesHeaders_DefineUiValidation();
                Fill_ObjectDefineUiValidation_DefineUiValidation();
                Fill_GridDefineUiValidation_DefineUiValidation();
                ViewCurrentLangCalendars_RuleParameters();
                ClearList_RuleParameters();
            }
        );