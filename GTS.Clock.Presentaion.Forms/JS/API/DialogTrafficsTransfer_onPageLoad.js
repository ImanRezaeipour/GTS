

function DialogTrafficsTransfer_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogTrafficsTransfer.set_contentUrl("TrafficsTransfer.aspx");
    document.getElementById('DialogTrafficsTransfer_IFrame').style.display = '';
    document.getElementById('DialogTrafficsTransfer_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogTrafficsTransfer_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogTrafficsTransfer_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogTrafficsTransfer_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogTrafficsTransfer_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogTrafficsTransfer').align = 'left';
    }
    if (CurrentLangID == 'en-US') {
        document.getElementById('CloseButton_DialogTrafficsTransfer').align = 'right';
    }

    var direction = null;
    switch (CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    document.getElementById('tbl_DialogTrafficsTransferheader').dir = document.getElementById('tbl_DialogTrafficsTransferfooter').dir = direction; 
}

function DialogTrafficsTransfer_onClose(sender, e) {
    document.getElementById('DialogTrafficsTransfer_IFrame').style.display = 'none';
    document.getElementById('DialogTrafficsTransfer_IFrame').style.visibility = 'hidden';
    DialogTrafficsTransfer.set_contentUrl("about:blank");
}



