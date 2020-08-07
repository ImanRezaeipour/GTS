

function GetBoxesHeaders_History() {
    parent.document.getElementById('Title_DialogHistory').innerHTML = document.getElementById('hfTitle_DialogHistory').value;
}

function SetRequestHistory_History() {
    errorMessage = document.getElementById('ErrorHiddenField_History').value;
    if (errorMessage == "") {
        var RequestHistoryObj = document.getElementById('hfHistory_History').value;
        RequestHistoryObj = eval('(' + RequestHistoryObj + ')');
        var IsLeave = RequestHistoryObj.IsLeave;
        var From = RequestHistoryObj.From;
        var To = RequestHistoryObj.To;
        var UesedInMonth = RequestHistoryObj.UesedInMonth;
        var UesedInYear = RequestHistoryObj.UesedInYear;
        var RemainLeaveInMonth = RequestHistoryObj.RemainLeaveInMonth;
        var RemainLeaveInYear = RequestHistoryObj.RemainLeaveInYear;

        var ObjDialogHistory = parent.DialogHistory.get_value();
        var RequestTitle = ObjDialogHistory.RequestTitle;
        var RequestIssuer = ObjDialogHistory.RequestIssuer;

        document.getElementById('lblRequestTopic_History').innerHTML += ' ' + RequestTitle;
        document.getElementById('lblRequestIssuer_History').innerHTML += ' ' + RequestIssuer;
        document.getElementById('txtFrom_History').value = From;
        document.getElementById('txtTo_History').value = To;
        document.getElementById('txtInMonth_Consumed_History').value = UesedInMonth;
        document.getElementById('txtInYear_Consumed_History').value = UesedInYear;
        if (!IsLeave)
            document.getElementById("Container_MeritLeaveRemain_History").removeChild(document.getElementById("MeritLeaveRemainBox_History"));
        else {
            document.getElementById('txtInMonth_MeritLeaveRemain_History').value = RemainLeaveInMonth;
            document.getElementById('txtInYear_MeritLeaveRemain_History').value = RemainLeaveInYear;
        }
    }
    else {
        var RetMessage = eval('(' + errorMessage + ')');
        showDialog(RetMessage[0], Response[1], RetMessage[2], false);
    }
}

