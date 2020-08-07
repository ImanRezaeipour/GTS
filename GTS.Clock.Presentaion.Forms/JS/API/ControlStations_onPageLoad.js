

    $(document).ready
        (
            function() {
                parent.DialogLoading.Close();
                document.body.dir = document.ControlStationsForm.dir;
                SetWrapper_Alert_Box(document.ControlStationsForm.id);
                GetBoxesHeaders_ControlStations();
                SetActionMode_ControlStations('View');
                Fill_GridControlStations_ControlStations();

            }
        );
