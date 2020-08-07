

$(document).ready
 (
    function () {
        document.body.dir = document.PersonnelInformationSummaryForm.dir;
        SetWrapper_Alert_Box(document.PersonnelInformationSummaryForm.id);
        GetBoxesHeaders_PersonnelInformationSummary();
        ShowCurrentPersonnelImage_PersonnelInformationSummary();
        GetErrorMessage_PersonnelInformationSummary();
    }
 )
