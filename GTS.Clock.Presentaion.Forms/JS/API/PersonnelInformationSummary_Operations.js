
function GetBoxesHeaders_PersonnelInformationSummary() {
    var boxHeader = document.getElementById('hfheader_PersonnelInformationSummary').value;
    if (this.frameElement.id != 'MainViewMaximizedPartIFrame_MainView')
        parent.document.getElementById('header_' + this.frameElement.id).innerHTML = document.getElementById('hfheader_PersonnelInformationSummary').value;
    else
        parent.document.getElementById('Title_DialogMainViewMaximizedPart').innerHTML = boxHeader;
}

function GetErrorMessage_PersonnelInformationSummary() {
    var errorMessage = document.getElementById('ErrorHiddenField_PersonnelInformationSummary').value;
    if (errorMessage != '' && errorMessage != undefined) {
        errorMessage = eval('(' + errorMessage + ')');
        if (errorMessage[2] != 'success')
            showDialog(errorMessage[0], errorMessage[1], errorMessage[2]);
    }
}

function ShowCurrentPersonnelImage_PersonnelInformationSummary() {
    //var CurrentPersonnelID = document.getElementById('CurrentPersonnelID_PersonnelInformationSummary').value;
    //if (CurrentPersonnelID != null && parseInt(CurrentPersonnelID) > 0)
    //   document.getElementById('CurrentPersonnelImage_PersonnelInformationSummary').src = 'ImageViewer.aspx?reload=""' + (new Date()).getTime() + '"&PersonnelID="' + CurrentPersonnelID + '"';

    var imageFile = document.getElementById('hfCurrentPersonnelImage_PersonnelInformationSummary').value;
    document.getElementById('CurrentPersonnelImage_PersonnelInformationSummary').src = "ImageViewer.aspx?reload=" + (new Date()).getTime() + "&AttachmentType=Personnel&ClientAttachment=" + CharToKeyCode_PersonnelInformationSummary(imageFile);
}

function CharToKeyCode_PersonnelInformationSummary(str) {
    var OutStr = '';
    if (str != null && str != undefined) {
        for (var i = 0; i < str.length; i++) {
            var KeyCode = str.charCodeAt(i);
            var CharKeyCode = '//' + KeyCode;
            OutStr += CharKeyCode;
        }
    }
    return OutStr;
}
