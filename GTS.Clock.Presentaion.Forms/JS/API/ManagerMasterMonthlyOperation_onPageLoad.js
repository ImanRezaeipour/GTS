

$(document).ready
(
    function () {
        parent.DialogLoading.Close();
        document.body.dir = document.ManagerMasterMonthlyOperationForm.dir;
        //ChangeDirection_Mastertbl_ManagerMasterMonthlyOperationForm();
        SetWrapper_Alert_Box(document.ManagerMasterMonthlyOperationForm.id);
        GetBoxesHeaders_ManagerMasterMonthlyOperation();
        SetHorizontalScrollingDirection_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_Opera();
        SetPosition_DropDownDives_ManagerMasterMonthlyOperation();
        SetPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(0);
        Fill_trvDepartments_ManagerMasterMonthlyOperation();
    }
);