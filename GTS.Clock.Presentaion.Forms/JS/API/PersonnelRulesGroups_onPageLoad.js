
$(document).ready
        (
            function () {
                document.body.dir = document.PersonnelRulesGroupsForm.dir;
                SetWrapper_Alert_Box(document.PersonnelRulesGroupsForm.id);
                GetBoxesHeaders_PersonnelRulesGroups();
                //ViewCurrentLangCalendars_PersonnelRulesGroups();
                ResetCalendars_PersonnelRulesGroups();
                SetActionMode_PersonnelRulesGroups('View');
                ChangeCalendarsEnabled_PersonnelRulesGroups('disable');
                Fill_GridPersonnelRulesGroups_PersonnelRulesGroups();
            }
        );
