

$(document).ready
        (
            function () {
                document.body.dir = document.CartableFilterForm.dir;
                GetBoxesHeaders_CartableFilter();
                //ViewCurrentLangCalendars_CartableFilter();
                SetButtonImages_TimeSelector_CartableFilter();
                GetCurrentDateTime_CartableFilter();
                SetStrFilterCondition_onLoad();
            }
        );
