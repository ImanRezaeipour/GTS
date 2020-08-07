

$(document).ready
        (
            function () {
                document.body.dir = document.KartableFilterForm.dir;
                GetBoxesHeaders_KartableFilter();
                //ViewCurrentLangCalendars_KartableFilter();
                SetButtonImages_TimeSelector_KartableFilter();
                GetCurrentDateTime_KartableFilter();
                SetStrFilterCondition_onLoad();
            }
        );
