

function GetBoxesHeaders_MasterUnderManagementPersonnelExeptionAccessView() {
    GetBoxesHeaders_MasterUnderManagementPersonnelExeptionAccessViewPage();
    DialogWaiting.Show();
}

function GetBoxesHeaders_MasterUnderManagementPersonnelExeptionAccessViewPage_onCallBack(Response) {
    DialogWaiting.Close();
    parent.document.getElementById('Title_DialogUnderManagementPersonnelExeptionAccessView').innerHTML = Response[0];
    document.getElementById('header_BoxUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView').innerHTML = Response[1];
}

function SetPageIndex_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView(index){
    SetPageIndex_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessViewPage(index);
    DialogWaiting.Show();
}

function SetPageIndex_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView_onCallBack(Response) {
    DialogWaiting.Close();
    document.getElementById('MasterUnderManagementPersonnelExeptionAccessView_iFrame').contentWindow.CallBack_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView.callback();
}



