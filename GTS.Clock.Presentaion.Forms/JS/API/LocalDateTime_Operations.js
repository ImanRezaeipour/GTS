
function GetBoxesHeaders_LocalDateTime() {
    var boxHeader = document.getElementById('hfheader_LocalDateTime').value;
    if (this.frameElement.id != 'MainViewMaximizedPartIFrame_MainView')
        parent.document.getElementById('header_' + this.frameElement.id).innerHTML = document.getElementById('hfheader_LocalDateTime').value;
    else
        parent.document.getElementById('Title_DialogMainViewMaximizedPart').innerHTML = boxHeader;
}


