<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthlyOperationGridSchema.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.MonthlyOperationGridSchema" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/hierarchicalGridStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/headerGridSyle.css" runat="server" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="MonthlyOperationGridSchemaForm" runat="server" meta:resourcekey="MonthlyOperationGridSchemaForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table id="Mastertbl_MonthlyOperationGridSchemaForm" style="width: 99%; height: 550px;
        font-family: Arial; font-size: small;" class="BoxStyle">
        <tr>
            <td>
                <ComponentArt:ToolBar ID="TlbMasterMonthlyOperation" runat="server" CssClass="toolbar"
                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                    UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemRequestsView_TlbMasterMonthlyOperation" runat="server"
                            ClientSideCommand="tlbItemRequestsView_TlbMasterMonthlyOperation_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="registeredRequests.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRequestsView_TlbMasterMonthlyOperation"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbMasterMonthlyOperation" runat="server"
                            ClientSideCommand="tlbItemRefresh_TlbMasterMonthlyOperation_onClick();" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbMasterMonthlyOperation"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemDetailsInformation_TlbMasterMonthlyOperation"
                            runat="server" ClientSideCommand="tlbItemDetailsInformation_TlbMasterMonthlyOperation_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="view_detailed.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDetailsInformation_TlbMasterMonthlyOperation"
                            TextImageSpacing="5" />                        
                        <ComponentArt:ToolBarItem ID="tlbItemGridSettings_TlbMasterMonthlyOperation" runat="server"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="package_settings.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemGridSettings_TlbMasterMonthlyOperation"
                            TextImageSpacing="5" ClientSideCommand="tlbItemGridSettings_TlbMasterMonthlyOperation_onClick();" />
                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbMasterMonthlyOperation"
                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbMasterMonthlyOperation_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="symbolRefresh.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbMasterMonthlyOperation"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbMasterMonthlyOperation" runat="server"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbMasterMonthlyOperation"
                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbMasterMonthlyOperation_onClick();" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbMasterMonthlyOperation" runat="server"
                            DropDownImageHeight="16px" ClientSideCommand="tlbItemExit_TlbMasterMonthlyOperation_onClick();"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbMasterMonthlyOperation" TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table style="width: 950px;">
                                <tr>
                                    <td style="width: 45%">
                                        <table style="width: 95%">
                                            <tr align="center">
                                                <td style="width: 18%">
                                                    <asp:Label ID="lblYear_MasterMonthlyOperation" runat="server" Text=": سال" meta:resourcekey="lblYear_MasterMonthlyOperation"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td style="width: 29%">
                                                    <ComponentArt:ComboBox ID="cmbYear_MasterMonthlyOperation" runat="server" AutoComplete="true"
                                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                        TextBoxCssClass="comboTextBox" Style="width: 98%;" DropDownHeight="280">
                                                        <ClientEvents>
                                                            <Change EventHandler="cmbYear_MasterMonthlyOperation_onChange" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                </td>
                                                <td style="width: 18%;">
                                                    <asp:Label ID="lblMonth_MasterMonthlyOperation" runat="server" Text=": ماه" meta:resourcekey="lblMonth_MasterMonthlyOperation"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td style="width: 29%;">
                                                    <ComponentArt:CallBack runat="server" ID="CallBack_cmbMonth_MasterMonthlyOperation"
                                                        OnCallback="CallBack_cmbMonth_MasterMonthlyOperation_onCallBack" Height="26">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbMonth_MasterMonthlyOperation" runat="server" AutoComplete="true"
                                                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                TextBoxCssClass="comboTextBox" Style="width: 98%;" DropDownHeight="280">
                                                                <ClientEvents>
                                                                    <Change EventHandler="cmbMonth_MasterMonthlyOperation_onChange" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField runat="server" ID="hfCurrentMonth_MasterMonthlyOperation" />
                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_Months_MasterMonthlyOperation" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <CallbackComplete EventHandler="CallBack_cmbMonth_MasterMonthlyOperation_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbMonth_MasterMonthlyOperation_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                <td>
                                                    <ComponentArt:ToolBar ID="TlbView_MasterMonthlyOperation" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemView_TlbView_MasterMonthlyOperation" runat="server"
                                                                ClientSideCommand="tlbItemView_TlbView_MasterMonthlyOperation_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemView_TlbView_MasterMonthlyOperation"
                                                                TextImageSpacing="5" Enabled="true" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 10%" align="center">
                                        <asp:Label ID="lblFromDate_MasterMonthlyOperation" runat="server" Text=": از تاریخ"
                                            meta:resourcekey="lblFromDate_MasterMonthlyOperation" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                    <td style="width: 17%" align="center">
                                        <input id="txtFromDate_MasterMonthlyOperation" type="text" class="TextBoxes" readonly="readonly"
                                            style="width: 95%; text-align: center" />
                                    </td>
                                    <td style="width: 10%" align="center">
                                        <asp:Label ID="lblToDate_MasterMonthlyOperation" runat="server" Text=": تا تاریخ"
                                            meta:resourcekey="lblToDate_MasterMonthlyOperation" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                    <td style="width: 17%" align="center">
                                        <input id="txtToDate_MasterMonthlyOperation" type="text" class="TextBoxes" readonly="readonly"
                                            style="width: 95%; text-align: center" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 70%">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td style="height: 90%" valign="top">
                            <table style="height: 100%">
                                <tr>
                                    <td style="height: 100%" valign="top">
                                        <table style="width: 100%; height: 100%" class="BoxStyle">
                                            <tr>
                                                <td style="height: 5%">
                                                    <table style="width: 700px">
                                                        <tr>
                                                            <td id="header_MonthlyOperation_MasterMonthlyOperation" class="HeaderLabel" style="width: 50%">
                                                                Monthly Operation Report
                                                            </td>
                                                            <td id="loadingPanel_GridMasterMonthlyOperation_MasterMonthlyOperation" class="HeaderLabel"
                                                                style="width: 50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" valign="top">
                                                    <div runat="server" id="Container_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                        style="position: relative;
                                                        visibility: hidden" class="HHeadingFloatContainerClass">
                                                        <ComponentArt:CallBack ID="CallBack_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                            runat="server" OnCallback="CallBack_tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation_onCallback">
                                                            <Content>
                                                                <asp:Table ID="tblFloatHeader_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                    runat="server" CssClass="HHeadingFloatTableClass" Width="100%">
                                                                    <asp:TableRow>
                                                                        <asp:TableCell ID="ID" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="DayName" meta:resourcekey="clmnHeaderDay_GridMasterMonthlyOperation_MasterMonthlyOperation">روز</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="Date" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="TheDate" meta:resourcekey="clmnHeaderDate_GridMasterMonthlyOperation_MasterMonthlyOperation">تاریخ</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="FirstEntrance" meta:resourcekey="clmnHeaderFirstEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation">ورود اول</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="FirstExit" meta:resourcekey="clmnHeaderFirstExit_GridMasterMonthlyOperation_MasterMonthlyOperation">خروج اول</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="SecondEntrance" meta:resourcekey="clmnHeaderSecondEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation">ورود دوم</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="SecondExit" meta:resourcekey="clmnHeaderSecondExit_GridMasterMonthlyOperation_MasterMonthlyOperation">خروج دوم</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ThirdEntrance" meta:resourcekey="clmnHeaderThirdEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation">ورود سوم</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ThirdExit" meta:resourcekey="clmnHeaderThirdExit_GridMasterMonthlyOperation_MasterMonthlyOperation">خروج سوم</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="FourthEntrance" meta:resourcekey="clmnHeaderFourthEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation">ورود چهارم</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="FourthExit" meta:resourcekey="clmnHeaderFourthExit_GridMasterMonthlyOperation_MasterMonthlyOperation">خروج چهارم</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="FifthEntrance" meta:resourcekey="clmnHeaderFifthEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation">ورود پنجم</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="FifthExit" meta:resourcekey="clmnHeaderFifthExit_GridMasterMonthlyOperation_MasterMonthlyOperation">خروج پنجم</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="LastExit" meta:resourcekey="clmnHeaderLastExit_GridMasterMonthlyOperation_MasterMonthlyOperation">خروج آخر</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="NecessaryOperation" meta:resourcekey="clmnHeaderNecessaryOperation_GridMasterMonthlyOperation_MasterMonthlyOperation">کارکرد لازم</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="PresenceDuration" meta:resourcekey="clmnHeaderPresenceDuration_GridMasterMonthlyOperation_MasterMonthlyOperation">مدت حضور</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="HourlyPureOperation" meta:resourcekey="clmnHeaderHourlyPureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation">کارکرد خالص ساعتی</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="DailyPureOperation" meta:resourcekey="clmnHeaderDailyPureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation">کارکرد خالص روزانه</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ImpureOperation" meta:resourcekey="clmnHeaderImpureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation">کارکرد ناخالص</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="AllowableOverTime" meta:resourcekey="clmnHeaderAllowableOverTime_GridMasterMonthlyOperation_MasterMonthlyOperation">اضافه کار مجاز</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="UnallowableOverTime" meta:resourcekey="clmnHeaderUnallowableOverTime_GridMasterMonthlyOperation_MasterMonthlyOperation">اضافه کار غیر مجاز</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="HourlyAllowableAbsence" meta:resourcekey="clmnHeaderHourlyAllowableAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation">غیبت مجاز ساعتی</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="HourlyUnallowableAbsence" meta:resourcekey="clmnHeaderHourlyUnallowableAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation">غیبت غیر مجاز ساعتی</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="DailyAbsence" meta:resourcekey="clmnHeaderDailyAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation">غیبت روزانه</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="HourlyMission" meta:resourcekey="clmnHeaderHourlyMission_GridMasterMonthlyOperation_MasterMonthlyOperation">ماموریت ساعتی</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="DailyMission" meta:resourcekey="clmnHeaderDailyMission_GridMasterMonthlyOperation_MasterMonthlyOperation">ماموریت روزانه</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="HostelryMission" meta:resourcekey="clmnHeaderHostelryMission_GridMasterMonthlyOperation_MasterMonthlyOperation">ماموریت شبانه روزی</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="HourlySickLeave" meta:resourcekey="clmnHeaderHourlySickLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">مرخصی استعلاجی ساعتی</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="DailySickLeave" meta:resourcekey="clmnHeaderDailySickLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">مرخصی استعلاجی روزانه</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="HourlyMeritoriouslyLeave" meta:resourcekey="clmnHeaderHourlyMeritoriouslyLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">مرخصی استحقاقی ساعتی</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="DailyMeritoriouslyLeave" meta:resourcekey="clmnHeaderDailyMeritoriouslyLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">مرخصی استحقاقی روزانه</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="HourlyWithoutPayLeave" meta:resourcekey="clmnHeaderHourlyWithoutPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">مرخصی بی حقوق ساعتی</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="DailyWithoutPayLeave" meta:resourcekey="clmnHeaderDailyWithoutPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">مرخصی بی حقوق روزانه</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="HourlyWithPayLeave" meta:resourcekey="clmnHeaderHourlyWithPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">مرخصی با حقوق ساعتی</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="DailyWithPayLeave" meta:resourcekey="clmnHeaderDailyWithPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">مرخصی با حقوق روزانه</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="Shift" meta:resourcekey="clmnHeaderShift_GridMasterMonthlyOperation_MasterMonthlyOperation">شیفت</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ShiftPairs" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ReserveField1" meta:resourcekey="clmnHeaderReserveField1_GridMasterMonthlyOperation_MasterMonthlyOperation">فیلد رزرو 1</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ReserveField2" meta:resourcekey="clmnHeaderReserveField2_GridMasterMonthlyOperation_MasterMonthlyOperation">فیلد رزرو 2</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ReserveField3" meta:resourcekey="clmnHeaderReserveField3_GridMasterMonthlyOperation_MasterMonthlyOperation">فیلد رزرو 3</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ReserveField4" meta:resourcekey="clmnHeaderReserveField4_GridMasterMonthlyOperation_MasterMonthlyOperation">فیلد رزرو 4</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ReserveField5" meta:resourcekey="clmnHeaderReserveField5_GridMasterMonthlyOperation_MasterMonthlyOperation">فیلد رزرو 5</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ReserveField6" meta:resourcekey="clmnHeaderReserveField6_GridMasterMonthlyOperation_MasterMonthlyOperation">فیلد رزرو 6</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ReserveField7" meta:resourcekey="clmnHeaderReserveField7_GridMasterMonthlyOperation_MasterMonthlyOperation">فیلد رزرو 7</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ReserveField8" meta:resourcekey="clmnHeaderReserveField8_GridMasterMonthlyOperation_MasterMonthlyOperation">فیلد رزرو 8</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ReserveField9" meta:resourcekey="clmnHeaderReserveField9_GridMasterMonthlyOperation_MasterMonthlyOperation">فیلد رزرو 9</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ReserveField10" meta:resourcekey="clmnHeaderReserveField10_GridMasterMonthlyOperation_MasterMonthlyOperation">فیلد رزرو 10</asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="FirstEntrance_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="FirstExit_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="SecondEntrance_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="SecondExit_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ThirdEntrance_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="ThirdExit_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="FourthEntrance_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="FourthExit_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="FifthEntrance_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="FifthExit_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="LastExit_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="HourlyUnallowableAbsence_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="DailyAbsence_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="DayHolidayState_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="DayState_BC" Visible="false"></asp:TableCell>
                                                                        <asp:TableCell runat="server" ID="DayStateTitle" Visible="false"></asp:TableCell>
                                                                    </asp:TableRow>
                                                                </asp:Table>
                                                            </Content>
                                                        </ComponentArt:CallBack>
                                                    </div>
                                                    <div id="Container_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                        <ComponentArt:CallBack ID="CallBack_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                            runat="server" OnCallback="CallBack_GridMasterMonthlyOperation_MasterMonthlyOperation_onCallBack">
                                                            <Content>
                                                                <ComponentArt:DataGrid ID="GridMasterMonthlyOperation_MasterMonthlyOperation" PreloadLevels="false"
                                                                    RunningMode="Callback" CssClass="HGridClass" ShowFooter="false" IndentCellWidth="19"
                                                                    AllowHorizontalScrolling="false" IndentCellCssClass="HIndentCell" TreeLineImageWidth="19"
                                                                    TreeLineImageHeight="20" ImagesBaseUrl="images/Grid/" PagerTextCssClass="HGridFooterText"
                                                                    ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16"
                                                                    PageSize="40" ScrollImagesFolderUrl="images/Grid/Scroller/" ScrollButtonWidth="16"
                                                                    ScrollBar="Off" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar" ScrollGripCssClass="HScrollGrip"
                                                                    ScrollBarWidth="16" Height="300" runat="server" meta:resourcekey="GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                    AllowColumnResizing="false">
                                                                    <Levels>
                                                                        <ComponentArt:GridLevel DataMember="MasterMonthlyOperation" DataKeyField="ID" SelectedRowCssClass="HSelectedRowClass"
                                                                            HeadingTextCssClass="HHeadingTextClass" HeadingCellCssClass="HHeadingCellClass"
                                                                            AlternatingRowCssClass="HL0AlternatingRowClass" RowCssClass="HRowClass" HeadingRowCssClass="HL0HeadingRowClass"
                                                                            TableHeadingCssClass="HTableHeading" GroupHeadingCssClass="HTableHeading" SortDescendingImageUrl="desc.gif"
                                                                            SortAscendingImageUrl="asc.gif" SortImageHeight="5" SortImageWidth="9" SelectorCellCssClass="HL0SelectorCell"
                                                                            DataCellCssClass="HDataCell" SelectorImageUrl="selector.gif" SelectorCellWidth="19"
                                                                            ShowSelectorCells="true" AllowSorting="false" AllowReordering="false" ShowHeadingCells="true">
                                                                            <Columns>
                                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="DayName" HeadingText="روز" meta:resourcekey="clmnDay_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="70" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_DayName_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="Date" Visible="false" FormatString="yyyy/MM/dd"/>
                                                                                <ComponentArt:GridColumn DataField="TheDate" HeadingText="تاریخ" meta:resourcekey="clmnDate_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="80" Align="Center" />
                                                                                <ComponentArt:GridColumn DataField="FirstEntrance" HeadingText="ورود اول" meta:resourcekey="clmnFirstEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="80" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnFirstEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="FirstExit" HeadingText="خروج اول" meta:resourcekey="clmnFirstExit_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="50" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnFirstExit_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="SecondEntrance" HeadingText="ورود دوم" meta:resourcekey="clmnSecondEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="95" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnSecondEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="SecondExit" HeadingText="خروج دوم" meta:resourcekey="clmnSecondExit_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="70" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnSecondExit_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="ThirdEntrance" HeadingText="ورود سوم" meta:resourcekey="clmnThirdEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="85" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnThirdEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="ThirdExit" HeadingText="خروج سوم" meta:resourcekey="clmnThirdExit_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="55" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnThirdExit_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="FourthEntrance" HeadingText="ورود چهارم" meta:resourcekey="clmnFourthEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="85" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnFourthEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="FourthExit" HeadingText="خروج چهارم" meta:resourcekey="clmnFourthExit_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="55" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnFourthExit_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="FifthEntrance" HeadingText="ورود پنجم" meta:resourcekey="clmnFifthEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="85" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnFifthEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="FifthExit" HeadingText="خروج پنجم" meta:resourcekey="clmnFifthExit_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="55" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnFifthExit_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="LastExit" HeadingText="خروج آخر" meta:resourcekey="clmnLastExit_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="50" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnLastExit_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="NecessaryOperation" HeadingText="کارکرد لازم"
                                                                                    meta:resourcekey="clmnNecessaryOperation_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="125" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnNecessaryOperation_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="PresenceDuration" HeadingText="مدت حضور" meta:resourcekey="clmnPresenceDuration_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="120" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnPresenceDuration_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="HourlyPureOperation" HeadingText="کارکرد خالص ساعتی"
                                                                                    meta:resourcekey="clmnHourlyPureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="130" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnHourlyPureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="DailyPureOperation" HeadingText="کارکرد خالص روزانه"
                                                                                    meta:resourcekey="clmnDailyPureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="120" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnDailyPureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="ImpureOperation" HeadingText="کارکرد ناخالص "
                                                                                    meta:resourcekey="clmnImpureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="135" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnImpureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="AllowableOverTime" HeadingText="اضافه کار مجاز"
                                                                                    meta:resourcekey="clmnAllowableOverTime_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="115" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnAllowableOverTime_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="UnallowableOverTime" HeadingText="اضافه کار غیر مجاز"
                                                                                    meta:resourcekey="clmnUnallowableOverTime_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="130" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnUnallowableOverTime_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="HourlyAllowableAbsence" HeadingText="غیبت مجاز ساعتی"
                                                                                    meta:resourcekey="clmnHourlyAllowableAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="110" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnHourlyAllowableAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="HourlyUnallowableAbsence" HeadingText="غیبت غیر مجاز ساعتی"
                                                                                    meta:resourcekey="clmnHourlyUnallowableAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="160" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnHourlyUnallowableAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="DailyAbsence" HeadingText="غیبت روزانه" meta:resourcekey="clmnDailyAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="145" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnDailyAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="HourlyMission" HeadingText="ماموریت ساعتی" meta:resourcekey="clmnHourlyMission_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="85" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnHourlyMission_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="DailyMission" HeadingText="ماموریت روزانه" meta:resourcekey="clmnDailyMission_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="75" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnDailyMission_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="HostelryMission" HeadingText="ماموریت شبانه روزی"
                                                                                    meta:resourcekey="clmnHostelryMission_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="95" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnHostelryMission_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="HourlySickLeave" HeadingText="مرخصی استعلاجی ساعتی"
                                                                                    meta:resourcekey="clmnHourlySickLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="105" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnHourlySickLeave_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="DailySickLeave" HeadingText="مرخصی استعلاجی روزانه"
                                                                                    meta:resourcekey="clmnDailySickLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="95" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnDailySickLeave_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="HourlyMeritoriouslyLeave" HeadingText="مرخصی استحقاقی ساعتی"
                                                                                    meta:resourcekey="clmnHourlyMeritoriouslyLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="155" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnHourlyMeritoriouslyLeave_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="DailyMeritoriouslyLeave" HeadingText="مرخصی استحقاقی روزانه"
                                                                                    meta:resourcekey="clmnDailyMeritoriouslyLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="145" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnDailyMeritoriouslyLeave_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="HourlyWithoutPayLeave" HeadingText="مرخصی بی حقوق ساعتی"
                                                                                    meta:resourcekey="clmnHourlyWithoutPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="150" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnHourlyWithoutPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="DailyWithoutPayLeave" HeadingText="مرخصی بی حقوق روزانه"
                                                                                    meta:resourcekey="clmnDailyWithoutPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="150" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnDailyWithoutPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="HourlyWithPayLeave" HeadingText="مرخصی با حقوق ساعتی"
                                                                                    meta:resourcekey="clmnHourlyWithPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="150" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnHourlyWithPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="DailyWithPayLeave" HeadingText="مرخصی با حقوق روزانه"
                                                                                    meta:resourcekey="clmnDailyWithPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="150" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnDailyWithPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="Shift" HeadingText="شیفت" meta:resourcekey="clmnShift_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="150" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnShift_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="ShiftPairs" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="ReserveField1" HeadingText="فیلد رزرو 1" Width="150"
                                                                                    Align="Center" meta:resourcekey="clmnReserveField1_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="ReserveField2" HeadingText="فیلد رزرو 2" Width="150"
                                                                                    Align="Center" meta:resourcekey="clmnReserveField2_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="ReserveField3" HeadingText="فیلد رزرو 3" Width="150"
                                                                                    Align="Center" meta:resourcekey="clmnReserveField3_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="ReserveField4" HeadingText="فیلد رزرو 4" Width="150"
                                                                                    Align="Center" meta:resourcekey="clmnReserveField4_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="ReserveField5" HeadingText="فیلد رزرو 5" Width="150"
                                                                                    Align="Center" meta:resourcekey="clmnReserveField5_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="ReserveField6" HeadingText="فیلد رزرو 6" Width="150"
                                                                                    Align="Center" meta:resourcekey="clmnReserveField6_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="ReserveField7" HeadingText="فیلد رزرو 7" Width="150"
                                                                                    Align="Center" meta:resourcekey="clmnReserveField7_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="ReserveField8" HeadingText="فیلد رزرو 8" Width="150"
                                                                                    Align="Center" meta:resourcekey="clmnReserveField8_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="ReserveField9" HeadingText="فیلد رزرو 9" Width="150"
                                                                                    Align="Center" meta:resourcekey="clmnReserveField9_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="ReserveField10" HeadingText="فیلد رزرو 10" Width="150"
                                                                                    Align="Center" meta:resourcekey="clmnReserveField10_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="FirstEntrance_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="FirstExit_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="SecondEntrance_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="SecondExit_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="ThirdEntrance_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="ThirdExit_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="FourthEntrance_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="FourthExit_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="FifthEntrance_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="FifthExit_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="LastExit_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="HourlyUnallowableAbsence_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="DailyAbsence_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="DayHolidayState_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="DayState_BC" Visible="false" />
                                                                                <ComponentArt:GridColumn DataField="DayStateTitle" Visible="false" />
                                                                            </Columns>
                                                                        </ComponentArt:GridLevel>
                                                                        <ComponentArt:GridLevel DataMember="DetailedMonthlyOperation" DataKeyField="ID" SelectedRowCssClass="HSelectedRowClass"
                                                                            HeadingTextCssClass="HHeadingTextClass" HeadingCellCssClass="HHeadingCellClass"
                                                                            AlternatingRowCssClass="HL1AlternatingRowClass" RowCssClass="HRowClass" HeadingRowCssClass="HL1HeadingRowClass"
                                                                            TableHeadingCssClass="HTableHeading" GroupHeadingCssClass="HTableHeading" SelectorCellCssClass="HL1SelectorCell"
                                                                            DataCellCssClass="HDataCell" SelectorImageUrl="selector.gif" SelectorCellWidth="19"
                                                                            ShowSelectorCells="true" AllowSorting="false" AllowReordering="false">
                                                                            <Columns>
                                                                                <ComponentArt:GridColumn DataField="Name" HeadingText="عنوان" meta:resourcekey="clmnTitle_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="200" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnTitle_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="From" HeadingText="از ساعت" meta:resourcekey="clmnFromHour_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="60" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnFromHour_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="To" HeadingText="تا ساعت" meta:resourcekey="clmnToHour_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    Width="60" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnToHour_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="Description" HeadingText="توضیحات" meta:resourcekey="clmnDescription_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                    DataCellClientTemplateId="DataCellClientTemplate_clmnDescription_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                                <ComponentArt:GridColumn DataField="Color" Visible="false" />
                                                                            </Columns>
                                                                        </ComponentArt:GridLevel>
                                                                    </Levels>
                                                                    <ClientTemplates>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_DayName_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%">
                                                                                <tr>
                                                                                    <td title="##DataItem.GetMember('DayStateTitle').Value##" style="width: 20%; border: 1px outset lavender;
                                                                                        background-color: ##DataItem.GetMember('DayState_BC').Value##">
                                                                                    </td>
                                                                                    <td style="width: 80%; font-weight: bold; color: ##DataItem.GetMember('DayHolidayState_BC').Value##">
                                                                                        ##DataItem.GetMember('DayName').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnFirstEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; cursor: crosshair; background-color: ##DataItem.GetMember('FirstEntrance_BC').Value##"
                                                                                title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,4);##"
                                                                                ondblclick="GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick('FirstEntrance');">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('FirstEntrance').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnFirstExit_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; cursor: crosshair; background-color: ##DataItem.GetMember('FirstExit_BC').Value##"
                                                                                title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,5);##"
                                                                                ondblclick="GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick('FirstExit');">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('FirstExit').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnSecondEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; cursor: crosshair; background-color: ##DataItem.GetMember('SecondEntrance_BC').Value##"
                                                                                title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,6);##"
                                                                                ondblclick="GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick('SecondEntrance');">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('SecondEntrance').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnSecondExit_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; cursor: crosshair; background-color: ##DataItem.GetMember('SecondExit_BC').Value##"
                                                                                title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,7);##"
                                                                                ondblclick="GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick('SecondExit');">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('SecondExit').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnThirdEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; cursor: crosshair; background-color: ##DataItem.GetMember('ThirdEntrance_BC').Value##"
                                                                                title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,8);##"
                                                                                ondblclick="GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick('ThirdEntrance');">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('ThirdEntrance').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnThirdExit_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; cursor: crosshair; background-color: ##DataItem.GetMember('ThirdExit_BC').Value##"
                                                                                title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,9);##"
                                                                                ondblclick="GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick('ThirdExit');">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('ThirdExit').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnFourthEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; cursor: crosshair; background-color: ##DataItem.GetMember('FourthEntrance_BC').Value##"
                                                                                title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,10);##"
                                                                                ondblclick="GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick('FourthEntrance');">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('FourthEntrance').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnFourthExit_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; cursor: crosshair; background-color: ##DataItem.GetMember('FourthExit_BC').Value##"
                                                                                title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,11);##"
                                                                                ondblclick="GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick('FourthExit');">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('FourthExit').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnFifthEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; cursor: crosshair; background-color: ##DataItem.GetMember('FifthEntrance_BC').Value##"
                                                                                title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,12);##"
                                                                                ondblclick="GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick('FifthEntrance');">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('FifthEntrance').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnFifthExit_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; cursor: crosshair; background-color: ##DataItem.GetMember('FifthExit_BC').Value##"
                                                                                title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,13);##"
                                                                                ondblclick="GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick('FifthExit');">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('FifthExit').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnLastExit_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; cursor: crosshair; background-color: ##DataItem.GetMember('LastExit_BC').Value##"
                                                                                title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,14);##"
                                                                                ondblclick="GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick('LastExit');">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('LastExit').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnNecessaryOperation_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,15);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('NecessaryOperation').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnPresenceDuration_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,16);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('PresenceDuration').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnHourlyPureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,17);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('HourlyPureOperation').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnDailyPureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,18);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('DailyPureOperation').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnImpureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,19);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('ImpureOperation').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnAllowableOverTime_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,20);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('AllowableOverTime').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnUnallowableOverTime_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; cursor: crosshair;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,21);##"
                                                                                ondblclick="GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick('UnallowableOverTime')">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('UnallowableOverTime').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnHourlyAllowableAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,22);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('HourlyAllowableAbsence').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnHourlyUnallowableAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; cursor: crosshair; background-color: ##DataItem.GetMember('HourlyUnallowableAbsence_BC').Value##"
                                                                                title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,23);##"
                                                                                ondblclick="GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick('HourlyUnallowableAbsence');">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('HourlyUnallowableAbsence').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnDailyAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; cursor: crosshair; background-color: ##DataItem.GetMember('DailyAbsence_BC').Value##"
                                                                                title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,24);##"
                                                                                ondblclick="GridMasterMonthlyOperation_MasterMonthlyOperation_onCelldbClick('DailyAbsence');">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('DailyAbsence').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnHourlyMission_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,25);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('HourlyMission').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnDailyMission_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,26);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('DailyMission').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnHostelryMission_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,27);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('HostelryMission').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnHourlySickLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,28);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('HourlySickLeave').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnDailySickLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,29);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('DailySickLeave').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnHourlyMeritoriouslyLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,30);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('HourlyMeritoriouslyLeave').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnDailyMeritoriouslyLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,31);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('DailyMeritoriouslyLeave').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnHourlyWithoutPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,32);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('HourlyWithoutPayLeave').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnDailyWithoutPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,33);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('DailyWithoutPayLeave').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnHourlyWithPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,34);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('HourlyWithPayLeave').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnDailyWithPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,35);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('DailyWithPayLeave').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnShift_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%;" title="##SetCellTitle_GridMasterMonthlyOperation_MasterMonthlyOperation(DataItem.GetMember('ID').Value,36);##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('Shift').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnTitle_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; background-color: ##DataItem.GetMember('Color').Value##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('Name').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnFromHour_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; background-color: ##DataItem.GetMember('Color').Value##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('From').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnToHour_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; background-color: ##DataItem.GetMember('Color').Value##">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        ##DataItem.GetMember('To').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnDescription_GridMasterMonthlyOperation_MasterMonthlyOperation">
                                                                            <table style="width: 100%; background-color: ##DataItem.GetMember('Color').Value##">
                                                                                <tr>
                                                                                    <td>
                                                                                        ##DataItem.GetMember('Description').Value##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                    </ClientTemplates>
                                                                    <ClientEvents>
                                                                        <ItemCollapse EventHandler="GridMasterMonthlyOperation_MasterMonthlyOperation_onItemCollapse" />
                                                                        <ItemExpand EventHandler="GridMasterMonthlyOperation_MasterMonthlyOperation_onItemExpand" />
                                                                        <BeforeCallback EventHandler="GridMasterMonthlyOperation_MasterMonthlyOperation_onBeforeCallback" />
                                                                        <Load EventHandler="GridMasterMonthlyOperation_MasterMonthlyOperation_onLoad" />
                                                                        <CallbackError EventHandler="GridMasterMonthlyOperation_MasterMonthlyOperation_onCallbackError" />
                                                                        <CallbackComplete EventHandler="GridMasterMonthlyOperation_MasterMonthlyOperation_onCallbackComplete" />
                                                                        <RenderComplete EventHandler="GridMasterMonthlyOperation_MasterMonthlyOperation_onRenderComplete" />
                                                                    </ClientEvents>
                                                                </ComponentArt:DataGrid>
                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_MonthlyOperation" />
                                                            </Content>
                                                            <ClientEvents>
                                                                <CallbackComplete EventHandler="CallBack_GridMasterMonthlyOperation_MasterMonthlyOperation_onCallbackComplete" />
                                                                <CallbackError EventHandler="CallBack_GridMasterMonthlyOperation_MasterMonthlyOperation_onCallbackError" />
                                                            </ClientEvents>
                                                        </ComponentArt:CallBack>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td id="header_Summary_MasterMonthlyOperation" style="color: White; font-weight: bold;
                                                    font-family: Arial; width: 100%">
                                                    Total
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%">
                                                    <ComponentArt:CallBack ID="CallBack_GridSummaryMonthlyOperation_MasterMonthlyOperation"
                                                        runat="server" OnCallback="CallBack_GridSummaryMonthlyOperation_MasterMonthlyOperation_onCallBack">
                                                        <Content>
                                                            <ComponentArt:DataGrid ID="GridSummaryMonthlyOperation_MasterMonthlyOperation" RunningMode="Callback"
                                                                CssClass="HGridClass" ShowFooter="false" AllowHorizontalScrolling="false" IndentCellCssClass="HIndentCell"
                                                                TreeLineImageWidth="19" TreeLineImageHeight="20" PageSize="1" ImagesBaseUrl="images/Grid/"
                                                                TreeLineImagesFolderUrl="images/Grid/LeftLines/" PagerStyle="Numbered" PagerTextCssClass="HGridFooterText"
                                                                Height="100%" runat="server" AllowColumnResizing="false">
                                                                <Levels>
                                                                    <ComponentArt:GridLevel DataMember="Categories" DataKeyField="CategoryId" SelectedRowCssClass="HSelectedRowClass"
                                                                        HeadingTextCssClass="HHeadingTextClass" HeadingCellCssClass="HHeadingCellClass"
                                                                        AlternatingRowCssClass="HAlternatingRowClass" RowCssClass="HRowClass" HeadingRowCssClass="HL0HeadingRowClass"
                                                                        TableHeadingCssClass="HTableHeading" GroupHeadingCssClass="HTableHeading" SortDescendingImageUrl="desc.gif"
                                                                        SortAscendingImageUrl="asc.gif" SortImageHeight="5" SortImageWidth="9" DataCellCssClass="HDataCell"
                                                                        ShowSelectorCells="false" AllowSorting="false" AllowReordering="false">
                                                                        <Columns>
                                                                            <ComponentArt:GridColumn Visible="true" HeadingText=" " Width="24" />
                                                                            <ComponentArt:GridColumn DataField="DayName" HeadingText="روز" meta:resourcekey="clmnDay_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="70" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnEmpty_GridSummaryMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="TheDate" HeadingText="تاریخ" meta:resourcekey="clmnDate_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="80" TextWrap="true" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnEmpty_GridSummaryMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="FirstEntrance" HeadingText="ورود اول" meta:resourcekey="clmnFirstEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="80" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnEmpty_GridSummaryMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="FirstExit" HeadingText="خروج اول" meta:resourcekey="clmnFirstExit_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="50" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnEmpty_GridSummaryMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="SecondEntrance" HeadingText="ورود دوم" meta:resourcekey="clmnSecondEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="95" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnEmpty_GridSummaryMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="SecondExit" HeadingText="خروج دوم" meta:resourcekey="clmnSecondExit_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="70" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnEmpty_GridSummaryMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="ThirdEntrance" HeadingText="ورود سوم" meta:resourcekey="clmnThirdEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="85" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnEmpty_GridSummaryMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="ThirdExit" HeadingText="خروج سوم" meta:resourcekey="clmnThirdExit_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="55" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnEmpty_GridSummaryMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="FourthEntrance" HeadingText="ورود چهارم" meta:resourcekey="clmnFourthEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="85" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnEmpty_GridSummaryMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="FourthExit" HeadingText="خروج چهارم" meta:resourcekey="clmnFourthExit_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="55" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnEmpty_GridSummaryMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="FifthEntrance" HeadingText="ورود پنجم" meta:resourcekey="clmnFifthEntrance_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="85" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnEmpty_GridSummaryMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="FifthExit" HeadingText="خروج پنجم" meta:resourcekey="clmnFifthExit_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="55" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnEmpty_GridSummaryMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="LastExit" HeadingText="خروج آخر" meta:resourcekey="clmnLastExit_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="50" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnEmpty_GridSummaryMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="NecessaryOperation" HeadingText="کارکرد لازم"
                                                                                meta:resourcekey="clmnNecessaryOperation_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="125" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="PresenceDuration" HeadingText="مدت حضور" meta:resourcekey="clmnPresenceDuration_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="120" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="HourlyPureOperation" HeadingText="کارکرد خالص ساعتی"
                                                                                meta:resourcekey="clmnHourlyPureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="130" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="DailyPureOperation" HeadingText="کارکرد خالص روزانه"
                                                                                meta:resourcekey="clmnDailyPureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="120" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="ImpureOperation" HeadingText="کارکرد ناخالص "
                                                                                meta:resourcekey="clmnImpureOperation_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="135" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="AllowableOverTime" HeadingText="اضافه کار مجاز"
                                                                                meta:resourcekey="clmnAllowableOverTime_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="115" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="UnallowableOverTime" HeadingText="اضافه کار غیر مجاز"
                                                                                meta:resourcekey="clmnUnallowableOverTime_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="130" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="HourlyAllowableAbsence" HeadingText="غیبت مجاز ساعتی"
                                                                                meta:resourcekey="clmnHourlyAllowableAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="110" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="HourlyUnallowableAbsence" HeadingText="غیبت غیر مجاز ساعتی"
                                                                                meta:resourcekey="clmnHourlyUnallowableAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="160" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="DailyAbsence" HeadingText="غیبت روزانه" meta:resourcekey="clmnDailyAbsence_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="145" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="HourlyMission" HeadingText="ماموریت ساعتی" meta:resourcekey="clmnHourlyMission_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="85" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="DailyMission" HeadingText="ماموریت روزانه" meta:resourcekey="clmnDailyMission_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="75" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="HostelryMission" HeadingText="ماموریت شبانه روزی"
                                                                                meta:resourcekey="clmnHostelryMission_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="95" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="HourlySickLeave" HeadingText="مرخصی استعلاجی ساعتی"
                                                                                meta:resourcekey="clmnHourlySickLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="105" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="DailySickLeave" HeadingText="مرخصی استعلاجی روزانه"
                                                                                meta:resourcekey="clmnDailySickLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="95" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="HourlyMeritoriouslyLeave" HeadingText="مرخصی استحقاقی ساعتی"
                                                                                meta:resourcekey="clmnHourlyMeritoriouslyLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="155" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="DailyMeritoriouslyLeave" HeadingText="مرخصی استحقاقی روزانه"
                                                                                meta:resourcekey="clmnDailyMeritoriouslyLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="145" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="HourlyWithoutPayLeave" HeadingText="مرخصی بی حقوق ساعتی"
                                                                                meta:resourcekey="clmnHourlyWithoutPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="150" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="DailyWithoutPayLeave" HeadingText="مرخصی بی حقوق روزانه"
                                                                                meta:resourcekey="clmnDailyWithoutPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="150" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="HourlyWithPayLeave" HeadingText="مرخصی با حقوق ساعتی"
                                                                                meta:resourcekey="clmnHourlyWithPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="150" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="DailyWithPayLeave" HeadingText="مرخصی با حقوق روزانه"
                                                                                meta:resourcekey="clmnDailyWithPayLeave_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="150" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="Shift" HeadingText="شیفت" meta:resourcekey="clmnShift_GridMasterMonthlyOperation_MasterMonthlyOperation"
                                                                                Width="150" Align="Center" />
                                                                            <ComponentArt:GridColumn DataField="ReserveField1" HeadingText="فیلد رزرو 1" Width="150"
                                                                                Align="Center" meta:resourcekey="clmnReserveField1_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="ReserveField2" HeadingText="فیلد رزرو 2" Width="150"
                                                                                Align="Center" meta:resourcekey="clmnReserveField2_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="ReserveField3" HeadingText="فیلد رزرو 3" Width="150"
                                                                                Align="Center" meta:resourcekey="clmnReserveField3_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="ReserveField4" HeadingText="فیلد رزرو 4" Width="150"
                                                                                Align="Center" meta:resourcekey="clmnReserveField4_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="ReserveField5" HeadingText="فیلد رزرو 5" Width="150"
                                                                                Align="Center" meta:resourcekey="clmnReserveField5_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="ReserveField6" HeadingText="فیلد رزرو 6" Width="150"
                                                                                Align="Center" meta:resourcekey="clmnReserveField6_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="ReserveField7" HeadingText="فیلد رزرو 7" Width="150"
                                                                                Align="Center" meta:resourcekey="clmnReserveField7_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="ReserveField8" HeadingText="فیلد رزرو 8" Width="150"
                                                                                Align="Center" meta:resourcekey="clmnReserveField8_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="ReserveField9" HeadingText="فیلد رزرو 9" Width="150"
                                                                                Align="Center" meta:resourcekey="clmnReserveField9_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                            <ComponentArt:GridColumn DataField="ReserveField10" HeadingText="فیلد رزرو 10" Width="150"
                                                                                Align="Center" meta:resourcekey="clmnReserveField10_GridMasterMonthlyOperation_MasterMonthlyOperation" />
                                                                        </Columns>
                                                                    </ComponentArt:GridLevel>
                                                                </Levels>
                                                                <ClientTemplates>
                                                                    <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnEmpty_GridSummaryMonthlyOperation_MasterMonthlyOperation">
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td align="center">
                                                                                    ---
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ComponentArt:ClientTemplate>
                                                                </ClientTemplates>
                                                            </ComponentArt:DataGrid>
                                                            <asp:HiddenField runat="server" ID="ErroHiddenField_SummaryMonthlyOperation" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <CallbackComplete EventHandler="CallBack_GridSummaryMonthlyOperation_MasterMonthlyOperation_CallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_GridSummaryMonthlyOperation_MasterMonthlyOperation_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogGridSettings"
        runat="server" Width="200px">
        <Content>
            <table runat="server" style="font-family: Arial; border-top: gray 1px double;
                border-right: black 1px double; font-size: small; border-left: black 1px double;
                border-bottom: gray 1px double; width: 100%;" meta:resourcekey="tblGridSettings_DialogGridSettings"
                class="BodyStyle">
                <tr>
                    <td>
                        <ComponentArt:ToolBar ID="TlbGridSettings" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbGridSettings" runat="server" DropDownImageHeight="16px"
                                    ClientSideCommand="tlbItemSave_TlbGridSettings_onClick();" DropDownImageWidth="16px"
                                    ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbGridSettings"
                                    TextImageSpacing="5" Enabled="true" />
                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbGridSettings" runat="server" DropDownImageHeight="16px"
                                    ClientSideCommand="tlbItemExit_TlbGridSettings_onClick();" DropDownImageWidth="16px"
                                    ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbGridSettings"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table style="width: 100%;" class="BoxStyle">
                            <tr>
                                <td id="header_GridSettings_MasterMonthlyOperation" style="color: White; font-weight: bold;
                                    font-family: Arial; width: 100%">
                                    Grid Settings
                                </td>
                            </tr>
                            <tr id="Tr1" runat="server" meta:resourcekey="InverseAlignObj">
                                <td id="Td1" runat="server" meta:resourcekey="InverseAlignObj">
                                    <table style="width: 60%">
                                        <tr align="center">
                                            <td style="width: 30%">
                                                <asp:Label runat="server" ID="lblSelectAll_GridSettings_MasterMonthlyOperation" meta:resourcekey="lblSelectAll_GridSettings_MasterMonthlyOperation"
                                                    CssClass="WhiteLabel"></asp:Label>
                                            </td>
                                            <td style="width: 10%">
                                                <input runat="server" id="chbSelectAll_GridSettings_MasterMonthlyOperation" type="checkbox"
                                                    onclick="chbSelectAll_GridSettings_MasterMonthlyOperation_onClick();" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <ComponentArt:CallBack runat="server" ID="CallBack_GridSettings_MasterMonthlyOperation"
                                        OnCallback="CallBack_GridSettings_MasterMonthlyOperation_onCallBack">
                                        <Content>
                                            <ComponentArt:DataGrid ID="GridSettings_MasterMonthlyOperation" runat="server" AllowMultipleSelect="false"
                                                EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter" Height="495"
                                                ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerStyle="Numbered"
                                                ShowFooter="false" PagerTextCssClass="GridFooterText" PageSize="17" RunningMode="Client"
                                                SearchTextCssClass="GridHeaderText" TabIndex="0" Width="180" AllowColumnResizing="false"
                                                ScrollBar="Auto" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                <Levels>
                                                    <ComponentArt:GridLevel AllowSorting="false" AlternatingRowCssClass="AlternatingRow"
                                                        DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                        HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                        SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                        SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false">
                                                        <Columns>
                                                            <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="GridColumn" DefaultSortDirection="Descending"
                                                                HeadingText="ستون گرید" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnGridColumn_GridSettings_MasterMonthlyOperation" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="ViewState" DefaultSortDirection="Descending"
                                                                HeadingText="نمایش" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnView_GridSettings_MasterMonthlyOperation"
                                                                ColumnType="CheckBox" AllowEditing="True" />
                                                        </Columns>
                                                    </ComponentArt:GridLevel>
                                                </Levels>
                                            </ComponentArt:DataGrid>
                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_GridSettings_MasterMonthlyOperation" />
                                            <asp:HiddenField runat="server" ID="hfCurrentSettingID_GridSettings_MasterMonthlyOperation" />
                                        </Content>
                                        <ClientEvents>
                                            <CallbackComplete EventHandler="CallBack_GridSettings_MasterMonthlyOperation_CallbackComplete" />
                                            <CallbackError EventHandler="CallBack_GridSettings_MasterMonthlyOperation_onCallbackError" />
                                        </ClientEvents>
                                    </ComponentArt:CallBack>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </Content>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogRequestOnTraffic"
        HeaderClientTemplateId="DialogRequestOnTrafficheader" FooterClientTemplateId="DialogRequestOnTrafficfooter"
        runat="server" PreloadContentUrl="false" ContentUrl="RequestOnTraffic.aspx" IFrameCssClass="RequestOnTraffic_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogRequestOnTrafficheader">
                <table id="tbl_DialogRequestOnTrafficheader" style="width: 653px" cellpadding="0"
                    cellspacing="0" border="0" onmousedown="DialogRequestOnTraffic.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogRequestOnTraffic_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogRequestOnTraffic" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogRequestOnTraffic" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogRequestOnTraffic_IFrame').src = 'WhitePage.aspx'; DialogRequestOnTraffic.Close('cancelled'); parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogRequestOnTraffic_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogRequestOnTrafficfooter">
                <table id="tbl_DialogRequestOnTrafficfooter" style="width: 653px" cellpadding="0"
                    cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogRequestOnTraffic_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogRequestOnTraffic_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogRequestOnTraffic_onShow" />
            <OnClose EventHandler="DialogRequestOnTraffic_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogHourlyRequestOnAbsence"
        HeaderClientTemplateId="DialogHourlyRequestOnAbsenceheader" FooterClientTemplateId="DialogHourlyRequestOnAbsencefooter"
        runat="server" PreloadContentUrl="false" ContentUrl="HourlyRequestOnAbsence.aspx"
        IFrameCssClass="HourlyRequestOnAbsence_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogHourlyRequestOnAbsenceheader">
                <table id="tbl_DialogHourlyRequestOnAbsence" style="width: 653px" cellpadding="0"
                    cellspacing="0" border="0" onmousedown="DialogHourlyRequestOnAbsence.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogHourlyRequestOnAbsence_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogHourlyRequestOnAbsence" valign="bottom" style="color: White;
                                        font-size: 13px; font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogHourlyRequestOnAbsence" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogHourlyRequestOnAbsence_IFrame').src = 'WhitePage.aspx'; DialogHourlyRequestOnAbsence.Close('cancelled'); parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogHourlyRequestOnAbsence_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogHourlyRequestOnAbsencefooter">
                <table id="tbl_DialogHourlyRequestOnAbsencefooter" style="width: 653px" cellpadding="0"
                    cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogHourlyRequestOnAbsence_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogHourlyRequestOnAbsence_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogHourlyRequestOnAbsence_onShow" />
            <OnClose EventHandler="DialogHourlyRequestOnAbsence_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogDailyRequestOnAbsence"
        HeaderClientTemplateId="DialogDailyRequestOnAbsenceheader" FooterClientTemplateId="DialogDailyRequestOnAbsencefooter"
        runat="server" PreloadContentUrl="false" ContentUrl="DailyRequestOnAbsence.aspx"
        IFrameCssClass="DailyRequestOnAbsence_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogDailyRequestOnAbsenceheader">
                <table id="tbl_DialogDailyRequestOnAbsenceheader" style="width: 653px" cellpadding="0"
                    cellspacing="0" border="0" onmousedown="DialogDailyRequestOnAbsence.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogDailyRequestOnAbsence_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogDailyRequestOnAbsence" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogDailyRequestOnAbsence" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogDailyRequestOnAbsence_IFrame').src = 'WhitePage.aspx'; DialogDailyRequestOnAbsence.Close('cancelled'); parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogDailyRequestOnAbsence_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogDailyRequestOnAbsencefooter">
                <table id="tbl_DialogDailyRequestOnAbsencefooter" style="width: 653px" cellpadding="0"
                    cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogDailyRequestOnAbsence_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogDailyRequestOnAbsence_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogDailyRequestOnAbsence_onShow" />
            <OnClose EventHandler="DialogDailyRequestOnAbsence_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogRequestOnUnallowableOverTime"
        HeaderClientTemplateId="DialogRequestOnUnallowableOverTimeheader" FooterClientTemplateId="DialogRequestOnUnallowableOverTimefooter"
        runat="server" PreloadContentUrl="false" ContentUrl="RequestOnUnallowableOverTime.aspx"
        IFrameCssClass="RequestOnUnallowableOverTime_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogRequestOnUnallowableOverTimeheader">
                <table id="tbl_DialogRequestOnUnallowableOverTimeheader" style="width: 653px;" cellpadding="0"
                    cellspacing="0" border="0" onmousedown="DialogRequestOnUnallowableOverTime.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogRequestOnUnallowableOverTime_topLeftImage" style="display: block;"
                                src="Images/Dialog/top_left.gif" alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogRequestOnUnallowableOverTime" valign="bottom" style="color: White;
                                        font-size: 13px; font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogRequestOnUnallowableOverTime" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogRequestOnUnallowableOverTime_IFrame').src = 'WhitePage.aspx'; DialogRequestOnUnallowableOverTime.Close('cancelled'); parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogRequestOnUnallowableOverTime_topRightImage" style="display: block;"
                                src="Images/Dialog/top_right.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogRequestOnUnallowableOverTimefooter">
                <table id="tbl_DialogRequestOnUnallowableOverTimefooter" style="width: 653px" cellpadding="0"
                    cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogRequestOnUnallowableOverTime_downLeftImage" style="display: block;"
                                src="Images/Dialog/down_left.gif" alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogRequestOnUnallowableOverTime_downRightImage" style="display: block;"
                                src="Images/Dialog/down_right.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogRequestOnUnallowableOverTime_onShow" />
            <OnClose EventHandler="DialogRequestOnUnallowableOverTime_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogOvertimeJustificationRequest"
        HeaderClientTemplateId="DialogOvertimeJustificationRequestheader" FooterClientTemplateId="DialogOvertimeJustificationRequestfooter"
        runat="server" PreloadContentUrl="false" ContentUrl="OvertimeJustificationRequest.aspx"
        IFrameCssClass="OvertimeJustificationRequest_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogOvertimeJustificationRequestheader">
                <table id="tbl_DialogOvertimeJustificationRequestheader" style="width: 703px;" cellpadding="0"
                    cellspacing="0" border="0" onmousedown="DialogOvertimeJustificationRequest.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogOvertimeJustificationRequest_topLeftImage" style="display: block;"
                                src="Images/Dialog/top_left.gif" alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogOvertimeJustificationRequest" valign="bottom" style="color: White;
                                        font-size: 13px; font-family: Arial; font-weight: bold;">
                                    </td>
                                    <td id="CloseButton_DialogOvertimeJustificationRequest" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogOvertimeJustificationRequest_IFrame').src = 'WhitePage.aspx'; DialogOvertimeJustificationRequest.Close('cancelled'); parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogOvertimeJustificationRequest_topRightImage" style="display: block;"
                                src="Images/Dialog/top_right.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogOvertimeJustificationRequestfooter">
                <table id="tbl_DialogOvertimeJustificationRequestfooter" style="width: 703px" cellpadding="0"
                    cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogOvertimeJustificationRequest_downLeftImage" style="display: block;"
                                src="Images/Dialog/down_left.gif" alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogOvertimeJustificationRequest_downRightImage" style="display: block;"
                                src="Images/Dialog/down_right.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogOvertimeJustificationRequest_onShow" />
            <OnClose EventHandler="DialogOvertimeJustificationRequest_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        HeaderClientTemplateId="DialogUserInformationheader" FooterClientTemplateId="DialogUserInformationfooter"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogUserInformation"
        runat="server" PreloadContentUrl="false" ContentUrl="UserInformation.aspx" IFrameCssClass="UserInformation_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogUserInformationheader">
                <table id="tbl_DialogUserInformationheader" style="width: 603px" cellpadding="0"
                    cellspacing="0" border="0" onmousedown="DialogUserInformation.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogUserInformation_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogUserInformation" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogUserInformation" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogUserInformation_IFrame').src = 'WhitePage.aspx'; DialogUserInformation.Close('cancelled'); parent.document.getElementById('DialogMonthlyOperationGridSchema_IFrame').contentWindow.SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogUserInformation_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogUserInformationfooter">
                <table id="tbl_DialogUserInformationfooter" style="width: 603px" cellpadding="0"
                    cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogUserInformation_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogUserInformation_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogUserInformation_onShow" />
            <OnClose EventHandler="DialogUserInformation_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="300px">
        <Content>
            <table id="tblConfirm_DialogConfirm" style="width: 100%;" class="ConfirmStyle">
                <tr align="center">
                    <td colspan="2">
                        <asp:Label ID="lblConfirm" runat="server" CssClass="WhiteLabel"></asp:Label>
                    </td>
                </tr>
                <tr align="center">
                    <td style="width: 50%">
                        <ComponentArt:ToolBar ID="TlbOkConfirm" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                            ItemSpacing="1px" UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemOk_TlbOkConfirm" runat="server" ClientSideCommand="tlbItemOk_TlbOkConfirm_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemOk_TlbOkConfirm"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                    <td>
                        <ComponentArt:ToolBar ID="TlbCancelConfirm" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                            ItemSpacing="1px" UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbCancelConfirm" runat="server" ClientSideCommand="tlbItemCancel_TlbCancelConfirm_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancelConfirm"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
            </table>
        </Content>
    </ComponentArt:Dialog>
    <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_MasterMonthlyOperation"
        meta:resourcekey="hfclmnName_cmbPersonnel_MasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_MasterMonthlyOperation"
        meta:resourcekey="hfclmnBarCode_cmbPersonnel_MasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfheader_SearchByPersonnelBox_MasterMonthlyOperation"
        meta:resourcekey="hfheader_SearchByPersonnelBox_MasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfheader_MonthlyOperation_MasterMonthlyOperation"
        meta:resourcekey="hfheader_MonthlyOperation_MasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfheader_Summary_MasterMonthlyOperation" meta:resourcekey="hfheader_Summary_MasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogMonthlyOperationGridSchema" meta:resourcekey="hfTitle_DialogMonthlyOperationGridSchema" />
    <asp:HiddenField runat="server" ID="hfheader_GridSettings_MasterMonthlyOperation"
        meta:resourcekey="hfheader_GridSettings_MasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfheader_tblPersonnelSearch_DialogPersonnelSearch"
        meta:resourcekey="hfheader_tblPersonnelSearch_DialogPersonnelSearch" />
    <asp:HiddenField runat="server" ID="hfCurrentYear_MasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfRetErrorType_MasterMonthlyOperation" meta:resourcekey="hfRetErrorType_MasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfCallBackError_GridMasterMonthlyOperation_MasterMonthlyOperation"
        meta:resourcekey="hfCallBackError_GridMasterMonthlyOperation_MasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_MasterMonthlyOperation" meta:resourcekey="hfCloseMessage_MasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridMasterMonthlyOperation_MasterMonthlyOperation"
        meta:resourcekey="hfloadingPanel_GridMasterMonthlyOperation_MasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="ErrorHiddenField_CalculationDateRange" meta:resourcekey="ErrorHiddenField_CalculationDateRange" />
    <asp:HiddenField runat="server" ID="hfErrorType_MasterMonthlyOperation" meta:resourcekey="hfErrorType_MasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfConnectionError_MasterMonthlyOperation" meta:resourcekey="hfConnectionError_MasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfCurrentUser_MasterMonthlyOperation" />
    </form>
</body>
</html>
