

$(document).ready
(
    function () {
        parent.DialogLoading.Close();
        document.body.dir = document.ConceptRuleMasterOperationForm.dir;
        SetWrapper_Alert_Box(document.ConceptRuleMasterOperationForm.id);
    }
);