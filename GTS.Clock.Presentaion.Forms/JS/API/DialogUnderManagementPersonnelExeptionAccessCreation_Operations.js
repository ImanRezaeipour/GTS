
function GetBoxesHeaders_UnderManagementPersonnelExeptionAccessCreation() {
    GetBoxesHeaders_UnderManagementPersonnelExeptionAccessCreationPage();
}

function GetBoxesHeaders_UnderManagementPersonnelExeptionAccessCreationPage_onCallBack(Response) {
    parent.document.getElementById('Title_DialogUnderManagementPersonnelExeptionAccessCreation').innerHTML = Response[0];
    document.getElementById('header_UnderManagemetPersonnelBox_UnderManagementPersonnelExeptionAccessCreation').innerHTML = Response[1];
    document.getElementById('header_AccessLevelBox_UnderManagementPersonnelExeptionAccessCreation').innerHTML = Response[2];
}