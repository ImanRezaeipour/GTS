

$(document).ready
        (
            function () {
                document.body.dir = document.MonthlyOperationGanttChartSchemaForm.dir;
                SetWrapper_Alert_Box(document.MonthlyOperationGanttChartSchemaForm.id);
                GetBoxesHeaders_MonthlyOperationGanttChartSchema();
                Fill_cmbMonth_MonthlyOperationGanttChartSchema();
            }
        );
        
        
        
