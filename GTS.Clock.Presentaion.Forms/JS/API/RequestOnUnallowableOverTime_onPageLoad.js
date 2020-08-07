

$(document).ready
        (
            function () {
                SetWrapper_Alert_Box(document.RequestOnUnallowableOverTimeForm.id);
                SetDirection_Alert_Box(document.RequestOnUnallowableOverTimeForm.dir);
                Set_SelectedDateTime_RequestOnUnallowableOverTime();
                GetBoxesHeaders_RequestOnUnallowableOverTime();
                initTimePickers_RequestOnUnallowableOverTime();
                SetActionMode_RequestOnUnallowableOverTime('View');
                Fill_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime();
                Fill_GridRegisteredRequests_RequestOnUnallowableOverTime();
            }
        );
