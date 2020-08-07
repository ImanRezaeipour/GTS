<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemReports.aspx.cs" Inherits="SystemReports" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="SystemReportsForm" runat="server" meta:resourcekey="SystemReportsForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 99%; font-family: Arial; font-size: small;" class="BoxStyle">

            <tr>
                <td>
                    <ComponentArt:ToolBar ID="TlbSystemReports" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                        <Items>
                            <ComponentArt:ToolBarItem ID="tlbItemDeleteAll_TlbSystemReports" runat="server" ClientSideCommand="tlbItemDeleteAll_TlbSystemReports_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDeleteAll_TlbSystemReports" TextImageSpacing="5" />
                            <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbSystemReports" runat="server" ClientSideCommand="tlbItemHelp_TlbSystemReports_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbSystemReports" TextImageSpacing="5" />
                            <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbSystemReports" runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbSystemReports_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbSystemReports" TextImageSpacing="5" />
                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbSystemReports" runat="server" ClientSideCommand="tlbItemExit_TlbSystemReports_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbSystemReports" TextImageSpacing="5" />
                        </Items>
                    </ComponentArt:ToolBar>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;" class="BoxStyle">
                        <tr>
                            <td style="width: 28%">
                                <asp:Label ID="lblSystemReportType_SystemReports" runat="server" CssClass="WhiteLabel" Text=": نوع گزارش سیستمی" meta:resourcekey="lblSystemReportType_SystemReports"></asp:Label>
                            </td>
                            <td style="width: 28%"></td>
                            <td style="width: 28%"></td>
                            <td style="width: 16%"></td>
                        </tr>
                        <tr>
                            <td>
                                <ComponentArt:ComboBox ID="cmbSystemReportType_SystemReports" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" Width="80%">
                                </ComponentArt:ComboBox>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSearchTerm_SystemReports" runat="server" CssClass="WhiteLabel" Text=": عبارت جستجو" meta:resourcekey="lblSearchTerm_SystemReports"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFromDate_SystemReports" runat="server" CssClass="WhiteLabel" Text=": از تاریخ" meta:resourcekey="lblFromDate_SystemReports"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblToDate_SystemReports" runat="server" CssClass="WhiteLabel" Text=": تا تاریخ" meta:resourcekey="lblToDate_SystemReports"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                <input id="txtSearchTerm_SystemReports" class="TextBoxes" type="text" onclick="this.select();" onfocus="this.select();" style="width: 80%" /></td>
                            <td style="vertical-align: top">
                                <table>
                                    <tr>
                                        <td id="Container_FromDateCalendars_SystemReports">
                                            <table id="Container_pdpFromDate_SystemReports" runat="server" style="width: 100%" visible="false">
                                                <tr>
                                                    <td>
                                                        <pcal:PersianDatePickup ID="pdpFromDate_SystemReports" runat="server" CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="Container_gdpFromDate_SystemReports" runat="server" style="width: 100%" visible="false">
                                                <tr>
                                                    <td>
                                                        <table id="Container_gCalFromDate_SystemReports" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td onmouseup="btn_gdpFromDate_SystemReports_OnMouseUp(event)">
                                                                    <ComponentArt:Calendar ID="gdpFromDate_SystemReports" runat="server" ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gdpFromDate_SystemReports_OnDateChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                                <td style="font-size: 10px;">&nbsp; </td>
                                                                <td>
                                                                    <img id="btn_gdpFromDate_SystemReports" alt="" class="calendar_button" onclick="btn_gdpFromDate_SystemReports_OnClick(event)" onmouseup="btn_gdpFromDate_SystemReports_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <ComponentArt:Calendar ID="gCalFromDate_SystemReports" runat="server" AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpFromDate_SystemReports" PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                            <ClientEvents>
                                                                <SelectionChanged EventHandler="gCalFromDate_SystemReports_OnChange" />
                                                                <Load EventHandler="gCalFromDate_SystemReports_onLoad" />
                                                            </ClientEvents>
                                                        </ComponentArt:Calendar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top">
                                            <ComponentArt:ToolBar ID="TlbClear_FromDateCalendars_SystemReports" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_FromDateCalendars_SystemReports" runat="server" ClientSideCommand="tlbItemClear_TlbClear_FromDateCalendars_SystemReports_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_FromDateCalendars_SystemReports" TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <table>
                                    <tr>
                                        <td id="Container_ToDateCalendars_SystemReports">
                                            <table id="Container_pdpToDate_SystemReports" runat="server" style="width: 100%" visible="false">
                                                <tr>
                                                    <td>
                                                        <pcal:PersianDatePickup ID="pdpToDate_SystemReports" runat="server" CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="Container_gdpToDate_SystemReports" runat="server" style="width: 100%" visible="false">
                                                <tr>
                                                    <td>
                                                        <table id="Container_gCalToDate_SystemReports" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td onmouseup="btn_gdpToDate_SystemReports_OnMouseUp(event)">
                                                                    <ComponentArt:Calendar ID="gdpToDate_SystemReports" runat="server" ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gdpToDate_SystemReports_OnDateChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                                <td style="font-size: 10px;">&nbsp; </td>
                                                                <td>
                                                                    <img id="btn_gdpToDate_SystemReports" alt="" class="calendar_button" onclick="btn_gdpToDate_SystemReports_OnClick(event)" onmouseup="btn_gdpToDate_SystemReports_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <ComponentArt:Calendar ID="gCalToDate_SystemReports" runat="server" AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpToDate_SystemReports" PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                            <ClientEvents>
                                                                <SelectionChanged EventHandler="gCalToDate_SystemReports_OnChange" />
                                                                <Load EventHandler="gCalToDate_SystemReports_onLoad" />
                                                            </ClientEvents>
                                                        </ComponentArt:Calendar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top">
                                            <ComponentArt:ToolBar ID="TlbClear_ToDateCalendars_SystemReports" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_ToDateCalendars_SystemReports" runat="server" ClientSideCommand="tlbItemClear_TlbClear_ToDateCalendars_SystemReports_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_ToDateCalendars_SystemReports" TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td runat="server" style="vertical-align: top" meta:resourcekey="InverseAlignObj">
                                <ComponentArt:ToolBar ID="TlbResultsView" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemResultsView_TlbResultView" runat="server"
                                            ClientSideCommand="tlbItemResultsView_TlbResultView_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemResultsView_TlbResultView" TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;" class="BoxStyle">
                        <tr>
                            <td style="color: White; width: 100%">
                                <table style="width: 100%">
                                    <tr>
                                        <td id="header_GridSystemReportType_SystemReports" class="HeaderLabel" style="width: 50%;">System Report Type
                                        </td>
                                        <td id="loadingPanel_GridSystemReportType_SystemReports" class="HeaderLabel" style="width: 45%"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <ComponentArt:CallBack ID="CallBack_GridSystemReportType_SystemReports" runat="server" OnCallback="CallBack_GridSystemReportType_SystemReports_onCallBack">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridSystemBusinessReport_SystemReports" runat="server" AllowHorizontalScrolling="false"
                                            CssClass="Grid" EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter"
                                            ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                            PageSize="7" RunningMode="Client" SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false"
                                            ShowFooter="false" AllowColumnResizing="false" ScrollBar="Off" ScrollTopBottomImagesEnabled="true"
                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="960px" Visible="false">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                    SortImageWidth="9">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                        <ComponentArt:GridColumn DataField="Date" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="UIDate" DefaultSortDirection="Descending"
                                                            HeadingText="تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDate_GridSystemBusinessReport_SystemReports"
                                                            Width="70" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Username" DefaultSortDirection="Descending"
                                                            HeadingText="نام کاربری" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnUsername_GridSystemBusinessReport_SystemReports"
                                                            Width="100" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="IPAddress" DefaultSortDirection="Descending"
                                                            HeadingText="ادرس آی پی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnIPAddress_GridSystemBusinessReport_SystemReports"
                                                            Width="120" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="ClassName" DefaultSortDirection="Descending"
                                                            HeadingText="نام کلاس" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnClassName_GridSystemBusinessReport_SystemReports"
                                                            Width="120" DataCellClientTemplateId="DataCellClientTemplate_clmnClassName_GridSystemBusinessReport_SystemReports" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="MethodName" DefaultSortDirection="Descending"
                                                            HeadingText="نام متد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMethodName_GridSystemBusinessReport_SystemReports"
                                                            Width="120" DataCellClientTemplateId="DataCellClientTemplate_clmnMethodName_GridSystemBusinessReport_SystemReports" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Message" DefaultSortDirection="Descending"
                                                            HeadingText="پیغام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMessage_GridSystemBusinessReport_SystemReports"
                                                            Width="120" DataCellClientTemplateId="DataCellClientTemplate_clmnMessage_GridSystemBusinessReport_SystemReports" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Level" DefaultSortDirection="Descending"
                                                            HeadingText="سطح" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnLevel_GridSystemBusinessReport_SystemReports"
                                                            Width="70" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Exception" DefaultSortDirection="Descending"
                                                            HeadingText="خطا" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnException_GridSystemBusinessReport_SystemReports"
                                                            Width="120" DataCellClientTemplateId="DataCellClientTemplateId_clmnException_GridSystemBusinessReport_SystemReports" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="ExceptionSource" DefaultSortDirection="Descending"
                                                            HeadingText="منبع خطا" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnExceptionSource_GridSystemBusinessReport_SystemReports"
                                                            Width="120" DataCellClientTemplateId="DataCellClientTemplateId_clmnExceptionSource_GridSystemBusinessReport_SystemReports" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>                                                
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnClassName_GridSystemBusinessReport_SystemReports">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; font-family: Verdana; font-size: 10px; cursor: crosshair;" ondblclick="NavigateSystemReportTypeFeature_SystemReports_onCelldbClick('ClassName');">##DataItem.GetMember('ClassName').Value##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnMethodName_GridSystemBusinessReport_SystemReports">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; font-family: Verdana; font-size: 10px; cursor: crosshair;" ondblclick="NavigateSystemReportTypeFeature_SystemReports_onCelldbClick('MethodName');">##DataItem.GetMember('MethodName').Value##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnMessage_GridSystemBusinessReport_SystemReports">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; font-family: Verdana; font-size: 10px; cursor: crosshair;" ondblclick="NavigateSystemReportTypeFeature_SystemReports_onCelldbClick('Message');">##DataItem.GetMember('Message').Value##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnException_GridSystemBusinessReport_SystemReports">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; font-family: Verdana; font-size: 10px; cursor: crosshair;" ondblclick="NavigateSystemReportTypeFeature_SystemReports_onCelldbClick('Exception');">##DataItem.GetMember('Exception').Value##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnExceptionSource_GridSystemBusinessReport_SystemReports">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; font-family: Verdana; font-size: 10px; cursor: crosshair;" ondblclick="NavigateSystemReportTypeFeature_SystemReports_onCelldbClick('ExceptionSource');">##DataItem.GetMember('ExceptionSource').Value##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                            <ClientEvents>
                                                <Load EventHandler="GridSystemBusinessReport_SystemReports_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <ComponentArt:DataGrid ID="GridSystemEngineReport_SystemReports" runat="server" AllowHorizontalScrolling="false"
                                            CssClass="Grid" EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter"
                                            ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                            PageSize="7" RunningMode="Client" SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false"
                                            ShowFooter="false" AllowColumnResizing="false" ScrollBar="Off" ScrollTopBottomImagesEnabled="true"
                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="450px" Visible="false">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                    SortImageWidth="9">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                        <ComponentArt:GridColumn DataField="Date" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="UIDate" DefaultSortDirection="Descending"
                                                            HeadingText="تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDate_GridSystemEngineReport_SystemReports"
                                                            Width="70" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="PersonBarcode" DefaultSortDirection="Descending"
                                                            HeadingText="شماره پرسنلی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnBarcode_GridSystemEngineReport_SystemReports"
                                                            Width="125" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Level" DefaultSortDirection="Descending"
                                                            HeadingText="سطح" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnLevel_GridSystemEngineReport_SystemReports"
                                                            Width="70" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Message" DefaultSortDirection="Descending"
                                                            HeadingText="پیغام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMessage_GridSystemEngineReport_SystemReports"
                                                            Width="120" DataCellClientTemplateId="DataCellClientTemplate_clmnMessage_GridSystemEngineReport_SystemReports" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Exception" DefaultSortDirection="Descending"
                                                            HeadingText="خطا" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnException_GridSystemEngineReport_SystemReports"
                                                            Width="120" DataCellClientTemplateId="DataCellClientTemplateId_clmnException_GridSystemEngineReport_SystemReports" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnMessage_GridSystemEngineReport_SystemReports">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; font-family: Verdana; font-size: 10px; cursor: crosshair;" ondblclick="NavigateSystemReportTypeFeature_SystemReports_onCelldbClick('Message');">##DataItem.GetMember('Message').Value##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnException_GridSystemEngineReport_SystemReports">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; font-family: Verdana; font-size: 10px; cursor: crosshair;" ondblclick="NavigateSystemReportTypeFeature_SystemReports_onCelldbClick('Exception');">##DataItem.GetMember('Exception').Value##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                            <ClientEvents>
                                                <Load EventHandler="GridSystemEngineReport_SystemReports_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <ComponentArt:DataGrid ID="GridSystemWindowsServiceReport_SystemReports" runat="server" AllowHorizontalScrolling="false"
                                            CssClass="Grid" EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter"
                                            ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                            PageSize="7" RunningMode="Client" SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false"
                                            ShowFooter="false" AllowColumnResizing="false" ScrollBar="Off" ScrollTopBottomImagesEnabled="true"
                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="380px" Visible="false">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                    SortImageWidth="9">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                        <ComponentArt:GridColumn DataField="Date" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="UIDate" DefaultSortDirection="Descending"
                                                            HeadingText="تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDate_GridSystemWindowsServiceReport_SystemReports"
                                                            Width="70" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Level" DefaultSortDirection="Descending"
                                                            HeadingText="سطح" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnLevel_GridSystemWindowsServiceReport_SystemReports"
                                                            Width="70" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Message" DefaultSortDirection="Descending"
                                                            HeadingText="پیغام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMessage_GridSystemWindowsServiceReport_SystemReports"
                                                            Width="120" DataCellClientTemplateId="DataCellClientTemplate_clmnMessage_GridSystemWindowsServiceReport_SystemReports" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Exception" DefaultSortDirection="Descending"
                                                            HeadingText="خطا" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnException_GridSystemWindowsServiceReport_SystemReports"
                                                            Width="120" DataCellClientTemplateId="DataCellClientTemplateId_clmnException_GridSystemWindowsServiceReport_SystemReports" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnMessage_GridSystemWindowsServiceReport_SystemReports">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; font-family: Verdana; font-size: 10px; cursor: crosshair;" ondblclick="NavigateSystemReportTypeFeature_SystemReports_onCelldbClick('Message');">##DataItem.GetMember('Message').Value##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnException_GridSystemWindowsServiceReport_SystemReports">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; font-family: Verdana; font-size: 10px; cursor: crosshair;" ondblclick="NavigateSystemReportTypeFeature_SystemReports_onCelldbClick('Exception');">##DataItem.GetMember('Exception').Value##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                            <ClientEvents>
                                                <Load EventHandler="GridSystemWindowsServiceReport_SystemReports_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <ComponentArt:DataGrid ID="GridSystemUserActionReport_SystemReports" runat="server" AllowHorizontalScrolling="false"
                                            CssClass="Grid" EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter"
                                            ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                            PageSize="7" RunningMode="Client" SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false"
                                            ShowFooter="false" AllowColumnResizing="false" ScrollBar="Off" ScrollTopBottomImagesEnabled="true"
                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="960px" Visible="false">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                    SortImageWidth="9">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                        <ComponentArt:GridColumn DataField="NullableDate" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="UIDate" DefaultSortDirection="Descending"
                                                            HeadingText="تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDate_GridSystemUserActionReport_SystemReports"
                                                            Width="70" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Username" DefaultSortDirection="Descending"
                                                            HeadingText="نام کاربری" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnUsername_GridSystemUserActionReport_SystemReports"
                                                            Width="100" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="IPAddress" DefaultSortDirection="Descending"
                                                            HeadingText="ادرس آی پی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnIPAddress_GridSystemUserActionReport_SystemReports"
                                                            Width="120" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="PageID" DefaultSortDirection="Descending"
                                                            HeadingText="شناسه صفحه" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPageID_GridSystemBusinessReport_SystemReports"
                                                            Width="120" DataCellClientTemplateId="DataCellClientTemplateId_clmnPageID_GridSystemBusinessReport_SystemReports" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="ClassName" DefaultSortDirection="Descending"
                                                            HeadingText="نام کلاس" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnClassName_GridSystemUserActionReport_SystemReports"
                                                            Width="120" DataCellClientTemplateId="DataCellClientTemplate_clmnClassName_GridSystemUserActionReport_SystemReports" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="MethodName" DefaultSortDirection="Descending"
                                                            HeadingText="نام متد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMethodName_GridSystemUserActionReport_SystemReports"
                                                            Width="120" DataCellClientTemplateId="DataCellClientTemplate_clmnMethodName_GridSystemUserActionReport_SystemReports" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Action" DefaultSortDirection="Descending"
                                                            HeadingText="عملیات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnAction_GridSystemUserActionReport_SystemReports"
                                                            Width="100" DataCellClientTemplateId="DataCellClientTemplate_clmnAction_GridSystemUserActionReport_SystemReports" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="ObjectInformation" DefaultSortDirection="Descending"
                                                            HeadingText="اطلاعات شیئ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnObjectInformation_GridSystemUserActionReport_SystemReports"
                                                            Width="150" DataCellClientTemplateId="DataCellClientTemplateId_clmnObjectInformation_GridSystemUserActionReport_SystemReports" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnPageID_GridSystemBusinessReport_SystemReports">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; font-family: Verdana; font-size: 10px; cursor: crosshair;" ondblclick="NavigateSystemReportTypeFeature_SystemReports_onCelldbClick('PageID');">##DataItem.GetMember('PageID').Value##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnClassName_GridSystemUserActionReport_SystemReports">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; font-family: Verdana; font-size: 10px; cursor: crosshair;" ondblclick="NavigateSystemReportTypeFeature_SystemReports_onCelldbClick('ClassName');">##DataItem.GetMember('ClassName').Value##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnMethodName_GridSystemUserActionReport_SystemReports">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; font-family: Verdana; font-size: 10px; cursor: crosshair;" ondblclick="NavigateSystemReportTypeFeature_SystemReports_onCelldbClick('MethodName');">##DataItem.GetMember('MethodName').Value##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnAction_GridSystemUserActionReport_SystemReports">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; font-family: Verdana; font-size: 10px; cursor: crosshair;" ondblclick="NavigateSystemReportTypeFeature_SystemReports_onCelldbClick('Action');">##DataItem.GetMember('Action').Value##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnObjectInformation_GridSystemUserActionReport_SystemReports">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; font-family: Verdana; font-size: 10px; cursor: crosshair;" ondblclick="NavigateSystemReportTypeFeature_SystemReports_onCelldbClick('ObjectInformation');">##DataItem.GetMember('ObjectInformation').Value##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                            <ClientEvents>
                                                <Load EventHandler="GridSystemUserActionReport_SystemReports_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_GridSystemReportType_SystemReports" />
                                        <asp:HiddenField runat="server" ID="hfSystemReportTypePageCount_SystemReports" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridSystemReportType_SystemReports_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridSystemReportType_SystemReports_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 75%;" runat="server" meta:resourcekey="AlignObj">
                                            <ComponentArt:ToolBar ID="TlbPaging_GridSystemReportType_SystemReports" runat="server" CssClass="toolbar"
                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                Style="direction: ltr" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridSystemReportType_SystemReports" runat="server"
                                                        ClientSideCommand="tlbItemRefresh_TlbPaging_GridSystemReportType_SystemReports_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridSystemReportType_SystemReports"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridSystemReportType_SystemReports" runat="server"
                                                        ClientSideCommand="tlbItemFirst_TlbPaging_GridSystemReportType_SystemReports_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridSystemReportType_SystemReports"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridSystemReportType_SystemReports" runat="server"
                                                        ClientSideCommand="tlbItemBefore_TlbPaging_GridSystemReportType_SystemReports_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridSystemReportType_SystemReports"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridSystemReportType_SystemReports" runat="server"
                                                        ClientSideCommand="tlbItemNext_TlbPaging_GridSystemReportType_SystemReports_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridSystemReportType_SystemReports"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridSystemReportType_SystemReports" runat="server"
                                                        ClientSideCommand="tlbItemLast_TlbPaging_GridSystemReportType_SystemReports_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridSystemReportType_SystemReports"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                        <td id="footer_GridSystemReportType_SystemReports" style="width: 10%" class="WhiteLabel"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;" class="BoxStyle">
                        <tr>
                            <td id="header_SystemReportTypeFeature_SystemReports" class="HeaderLabel">System Report Type Feature
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <textarea id="txtSystemReportTypeFeature_SystemReports" readonly="readonly" cols="20" name="S1" rows="2" style="width: 100%; height: 110px; direction:ltr;" class="TextBoxes"></textarea></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
            runat="server" Width="350px">
            <Content>
                <table style="width: 100%;" class="ConfirmStyle">
                    <tr style="text-align: center">
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
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" Modal="true" AllowResize="false"
            runat="server" AllowDrag="false" Alignment="MiddleCentre" ID="DialogWaiting">
            <Content>
                <table>
                    <tr>
                        <td>
                            <img id="Img1" runat="server" alt="" src="~/Images/Dialog/Waiting.gif" />
                        </td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogWaiting_onShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfTitle_DialogSystemReports" meta:resourcekey="hfTitle_DialogSystemReports" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_SystemReports" meta:resourcekey="hfDeleteMessage_SystemReports" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_SystemReports" meta:resourcekey="hfCloseMessage_SystemReports" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridSystemReportType_SystemReports" meta:resourcekey="hfloadingPanel_GridSystemReportType_SystemReports" />
        <asp:HiddenField runat="server" ID="hffooter_GridSystemBusinessReport_SystemReports" meta:resourcekey="hffooter_GridSystemBusinessReport_SystemReports" />
        <asp:HiddenField runat="server" ID="hfheader_GridSystemReportType_SystemReports" meta:resourcekey="hfheader_GridSystemReportType_SystemReports" />
        <asp:HiddenField runat="server" ID="hfheader_SystemReportTypeFeature_SystemReports" meta:resourcekey="hfheader_SystemReportTypeFeature_SystemReports" />
        <asp:HiddenField runat="server" ID="hfErrorType_SystemReports" meta:resourcekey="hfErrorType_SystemReports" />
        <asp:HiddenField runat="server" ID="hfConnectionError_SystemReports" meta:resourcekey="hfConnectionError_SystemReports" />
        <asp:HiddenField runat="server" ID="hffooter_GridSystemReportType_SystemReports" meta:resourcekey="hffooter_GridSystemReportType_SystemReports" />
        <asp:HiddenField runat="server" ID="hfSystemReportTypePageSize_SystemReports" />
        <asp:HiddenField ID="hfCurrentSystemReportType_SystemReports" runat="server" />
    </form>
</body>
</html>
