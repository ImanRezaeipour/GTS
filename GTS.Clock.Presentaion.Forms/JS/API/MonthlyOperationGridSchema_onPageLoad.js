
$(document).ready
        (
            function () {
                parent.DialogLoading.Show();
                document.body.dir = document.MonthlyOperationGridSchemaForm.dir;
                SetWrapper_Alert_Box(document.MonthlyOperationGridSchemaForm.id);
                SetDirection_Alert_Box(parent.document.MainForm.dir);
                ChangeDirection_Mastertbl_MonthlyOperationGridSchemaForm();
                GetBoxesHeaders_MasterMonthlyOperation();
                Fill_cmbMonth_MasterMonthlyOperation();
                ChangeHorizontalPosition_Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation();

                var scrolladY;
                var scrolladNextObjectY;
                var windowY;
                scrolladY = $("#Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation").offset().top;
                scrolladNextObjectY = $("#Container_GridMasterMonthlyOperation_MasterMonthlyOperation").offset().top + 20;
                $(window).scroll(function () {
                    var expanded = GetIsItemExpanded_GridMasterMonthlyOperation_MasterMonthlyOperation();
                    //if (expanded == 0) {

                    if ($.browser.msie) {
                        windowY = $(window).scrollTop();
                    }
                    else {
                        windowY = $(window).attr("scrollY");
                    }
                    if (windowY > scrolladNextObjectY) {
                        var offset = windowY - scrolladY;
                        document.getElementById('Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation').style.visibility = "visible";
                        document.getElementById('Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation').style.height = document.getElementById('tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation').offsetHeight;
                        var offsetLag = 0;
                        if (navigator.userAgent.indexOf('MSIE') >= 0)
                            offsetLag = 10;
                        else
                            offsetLag = 28;
                        $("#Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation").animate({ top: offset + offsetLag }, { queue: false, duration: 500 });
                    }
                    else {
                        $("#Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation").animate({ top: 0 }, { queue: false, duration: 500 });
                        document.getElementById('Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation').style.visibility = "hidden";
                        document.getElementById('Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation').style.height = 0;
                    }
                    //}
                });
            });

function ChangePositionByScroll_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation() {
    var scrollY;
    var winY;
    var scrollNextObjectY;
    scrollY = $("#Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation").offset().top;
    scrollNextObjectY = $("#Container_GridMasterMonthlyOperation_MasterMonthlyOperation").offset().top + 20;
    if ($.browser.msie) {
        winY = $(window).scrollTop();
    }
    else {
        winY = $(window).attr("scrollY");
    }

    if (winY > scrollNextObjectY) {
        var offs = winY - scrollY;
        document.getElementById('Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation').style.visibility = "visible";
        $("#Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation").animate({ top: offs + 45 }, { queue: false, duration: 500 });
    }
    else {
        $("#Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation").animate({ top: 0 }, { queue: false, duration: 500 });
        document.getElementById('Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation').style.visibility = "hidden";
    }
}

        
