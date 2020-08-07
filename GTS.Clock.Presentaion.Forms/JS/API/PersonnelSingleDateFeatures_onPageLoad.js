

$(document).ready
        (
            function () {
                document.body.dir = document.PersonnelSingleDateFeaturesFrom.dir;
                SetWrapper_Alert_Box(document.PersonnelSingleDateFeaturesFrom.id);
                GetBoxesHeaders_PersonnelSingleDateFeatures();
                //ViewCurrentLangCalendars_PersonnelSingleDateFeatures();
                ResetCalendars_PersonnelSingleDateFeatures();
                SetActionMode_PersonnelSingleDateFeatures('View');
                ChangeCalendarsEnabled_PersonnelSingleDateFeatures('disable');
                Fill_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures();
            }
        );
