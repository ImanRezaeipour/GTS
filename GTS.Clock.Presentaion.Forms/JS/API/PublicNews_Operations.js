
function GetBoxesHeaders_PublicNews() {
    var boxHeader = document.getElementById('hfheader_PublicNews').value;
    if (this.frameElement.id != 'MainViewMaximizedPartIFrame_MainView')
        parent.document.getElementById('header_' + this.frameElement.id).innerHTML = boxHeader;
    else
        parent.document.getElementById('Title_DialogMainViewMaximizedPart').innerHTML = boxHeader;
}

function GetErrorMessage_PublicNews() {
    var errorMessage = document.getElementById('ErrorHiddenField_PublicNews').value;
    if (errorMessage != '' && errorMessage != undefined) {
        errorMessage = eval('(' + errorMessage + ')');
        if (errorMessage[2] != 'success')
            showDialog(errorMessage[0], errorMessage[1], errorMessage[2]);
    }
}

