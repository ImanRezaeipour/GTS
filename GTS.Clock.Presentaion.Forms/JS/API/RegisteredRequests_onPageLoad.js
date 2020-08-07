
$(document).ready
        (
            function () {
                document.body.dir = document.RegisteredRequestsForm.dir;
                GetBoxesHeaders_RegisteredRequests();
                SetWrapper_Alert_Box(document.RegisteredRequestsForm.id);
                SetDirection_Alert_Box(parent.document.MainForm.dir);
                ChangeDirection_Mastertbl_RegisteredRequestsForm();
                SetHorizontalScrollingDirection_GridRegisteredRequests_RegisteredRequests_Opera();
                ChangeLoadState_GridRegisteredRequests_RegisteredRequests('UnKnown');
                //ViewCurrentLangCalendars_RegisteredRequests();
                ResetCalendars_RegisteredRequests();
                CustomizeRegisteredRequestsFilter_RegisteredRequests();
            }
        );
