

$(document).ready
        (
            function() {
                parent.DialogLoading.Close();
                document.body.dir = document.TrafficOperationByOperatorFrom.dir;
                SetWrapper_Alert_Box(document.TrafficOperationByOperatorFrom.id);
                //ViewCurrentLangCalendars_TrafficOperationByOperator();
                GetBoxesHeaders_TrafficOperationByOperator();
            }
        );
