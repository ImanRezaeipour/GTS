

$(document).ready
        (
            function () {
                document.body.dir = document.RuleParametersForm.dir;
                SetWrapper_Alert_Box(document.RuleParametersForm.id);
                GetBoxesHeaders_RuleParameters();
                //ViewCurrentLangCalendars_RuleParameters();
                //initTimePicker_RuleParameters();
                ResetCalendars_RuleParameters();
                SetActionMode_RuleParameters('View');
                ChangeCalendarsEnabled_RuleParameters('disable');
                NavigateRuleFeatures_RuleParameters();
                Fill_GridRuleDateRanges_RuleParameters();
                Fill_GridRuleParameters_RuleParameters(null);
            }
        );
