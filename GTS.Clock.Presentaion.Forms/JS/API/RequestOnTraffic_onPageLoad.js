

$(document).ready
        (
            function () {
                SetWrapper_Alert_Box(document.RequestOnTrafficForm.id);
                SetDirection_Alert_Box(document.RequestOnTrafficForm.dir);
                Set_SelectedDateTime_RequestOnTraffic();
                GetBoxesHeaders_RequestOnTraffic();
                ChangeControlsDirection_DialogRequestOnTraffic();
                initTimePickers_RequestOnTraffic();
                SetActionMode_RequestOnTraffic('Add');
                Fill_GridTrafficPairs_RequestOnTraffic();
                Fill_GridRegisteredRequests_RequestOnTraffic();
            }
        );
