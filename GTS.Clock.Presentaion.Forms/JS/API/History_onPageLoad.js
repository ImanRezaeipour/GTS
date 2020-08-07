

$(document).ready
        (
            function () {
                document.body.dir = document.HistoryForm.dir;
                SetWrapper_Alert_Box(document.HistoryForm.id);
                GetBoxesHeaders_History();
                SetRequestHistory_History();
            }
        );
