$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.UiValidationForm.dir;
                SetWrapper_Alert_Box(document.UiValidationForm.id);
                GetBoxesHeaders_UiValidation();
                SetActionMode_UiValidation('View');
                Fill_GridUiValidation_UiValidation();
            }
        );