
function GetBoxesHeaders_RequestsStatePage_onCallBack(Response) {
    parent.document.getElementById('Title_DialogRequestsState').innerHTML = Response[0];    
}