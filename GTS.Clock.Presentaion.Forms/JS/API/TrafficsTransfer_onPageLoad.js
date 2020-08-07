
$(document).ready
        (
            function () {
                document.body.dir = document.TrafficsTransferForm.dir;
                SetWrapper_Alert_Box(document.TrafficsTransferForm.id);
                SetBoxesHeaders_TrafficsTransfer();
                initTimePickers_TrafficsTransfer();
                ResetCalendars_TrafficsTransfer();
            }
        );
