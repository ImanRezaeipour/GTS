<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RuleParameters.aspx.cs" Inherits="RuleParameters" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="RuleParametersForm" runat="server" meta:resourcekey="RuleParametersForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 100%;" class="BoxStyle">
            <tr>
                <td colspan="2">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbRuleParameters" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbRuleParameters" runat="server" ClientSideCommand="tlbItemNew_TlbRuleParameters_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbRuleParameters"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbRuleParameters" runat="server" ClientSideCommand="tlbItemEdit_TlbRuleParameters_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbRuleParameters"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbRuleParameters" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                            ClientSideCommand="tlbItemDelete_TlbRuleParameters_onClick();" ItemType="Command"
                                            meta:resourcekey="tlbItemDelete_TlbRuleParameters" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbRuleParameters" runat="server" ClientSideCommand="tlbItemSave_TlbRuleParameters_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="false" ImageHeight="16px"
                                            ImageUrl="save_silver.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbRuleParameters"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbRuleParameters" runat="server" ClientSideCommand="tlbItemCancel_TlbRuleParameters_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="false" ImageHeight="16px"
                                            ImageUrl="cancel_silver.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbRuleParameters"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbRuleParameters" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbRuleParameters" TextImageSpacing="5"
                                            ClientSideCommand="tlbItemHelp_TlbRuleParameters_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbRuleParameters" runat="server"
                                            ClientSideCommand="tlbItemFormReconstruction_TlbRuleParameters_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbRuleParameters"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbRuleParameters" runat="server" ClientSideCommand="tlbItemExit_TlbRuleParameters_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbRuleParameters"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="ActionMode_RuleParameters" class="ToolbarMode">&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" style="width: 50%">
                    <table style="width: 100%;" class="BoxStyle">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td id="header_RuleDateRanges_RuleParameters" class="HeaderLabel" style="width: 65%">Rule Date Ranges
                                        </td>
                                        <td id="loadingPanel_GridRuleDateRanges_RuleParameters" class="HeaderLabel" style="width: 30%"></td>
                                        <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridRuleDateRanges_RuleParameters" runat="server"
                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRuleDateRanges_RuleParameters"
                                                        runat="server" ClientSideCommand="Refresh_GridRuleDateRanges_RuleParameters();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRuleDateRanges_RuleParameters"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridRuleDateRanges_RuleParameters"
                                    OnCallback="CallBack_GridRuleDateRanges_RuleParameters_onCallBack">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridRuleDateRanges_RuleParameters" runat="server" CssClass="Grid"
                                            EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                            PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="5" RunningMode="Client"
                                            SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false" ShowFooter="false"
                                            AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="150">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                    SortImageWidth="9">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="FromDate" DefaultSortDirection="Descending"
                                                            HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromDate_GridRuleDateRanges_RuleParameters"
                                                            Visible="true" Width="75" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="ToDate" DefaultSortDirection="Descending"
                                                            HeadingText="تا تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToDate_GridRuleDateRanges_RuleParameters"
                                                            Visible="true" Width="75" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientEvents>
                                                <Load EventHandler="GridRuleDateRanges_RuleParameters_onLoad" />
                                                <ItemSelect EventHandler="GridRuleDateRanges_RuleParameters_onItemSelect" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_RuleDateRanges_RuleParameters" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridRuleDateRanges_RuleParameters_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridRuleDateRanges_RuleParameters_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Label ID="lblFromDate_RuleParameters" runat="server" Text=": از تاریخ" meta:resourcekey="lblFromDate_RuleParameters"
                                    CssClass="WhiteLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td id="Container_FromDateCalendars_RuleParameters">
                                <table runat="server" id="Container_pdpFromDate_RuleParameters" visible="false" style="width: 100%">
                                    <tr>
                                        <td>
                                            <pcal:PersianDatePickup ID="pdpFromDate_RuleParameters" runat="server" CssClass="PersianDatePicker"
                                                ReadOnly="true"></pcal:PersianDatePickup>
                                        </td>
                                    </tr>
                                </table>
                                <table runat="server" id="Container_gdpFromDate_RuleParameters" visible="false" style="width: 100%">
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalFromDate_RuleParameters">
                                                <tr>
                                                    <td onmouseup="btn_gdpFromDate_RuleParameters_OnMouseUp(event)">
                                                        <ComponentArt:Calendar ID="gdpFromDate_RuleParameters" runat="server" ControlType="Picker"
                                                            PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                            SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                            <ClientEvents>
                                                                <SelectionChanged EventHandler="gdpFromDate_RuleParameters_OnDateChange" />
                                                            </ClientEvents>
                                                        </ComponentArt:Calendar>
                                                    </td>
                                                    <td style="font-size: 10px;">&nbsp;
                                                    </td>
                                                    <td>
                                                        <img id="btn_gdpFromDate_RuleParameters" alt="" class="calendar_button" onclick="btn_gdpFromDate_RuleParameters_OnClick(event)"
                                                            onmouseup="btn_gdpFromDate_RuleParameters_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <ComponentArt:Calendar ID="gCalFromDate_RuleParameters" runat="server" AllowMonthSelection="false"
                                                AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpFromDate_RuleParameters"
                                                PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                <ClientEvents>
                                                    <SelectionChanged EventHandler="gCalFromDate_RuleParameters_OnChange" />
                                                    <Load EventHandler="gCalFromDate_RuleParameters_onLoad" />
                                                </ClientEvents>
                                            </ComponentArt:Calendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblToDate_RuleParameters" runat="server" Text=": تا تاریخ" meta:resourcekey="lblToDate_RuleParameters"
                                    CssClass="WhiteLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td id="Container_ToDateCalendars_RuleParameters">
                                <table runat="server" id="Container_pdpToDate_RuleParameters" visible="false" style="width: 100%">
                                    <tr>
                                        <td>
                                            <pcal:PersianDatePickup ID="pdpToDate_RuleParameters" runat="server" CssClass="PersianDatePicker"
                                                ReadOnly="true"></pcal:PersianDatePickup>
                                        </td>
                                    </tr>
                                </table>
                                <table runat="server" id="Container_gdpToDate_RuleParameters" visible="false" style="width: 100%">
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalToDate_RuleParameters">
                                                <tr>
                                                    <td onmouseup="btn_gdpToDate_RuleParameters_OnMouseUp(event)">
                                                        <ComponentArt:Calendar ID="gdpToDate_RuleParameters" runat="server" ControlType="Picker"
                                                            PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                            SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                            <ClientEvents>
                                                                <SelectionChanged EventHandler="gdpToDate_RuleParameters_OnDateChange" />
                                                            </ClientEvents>
                                                        </ComponentArt:Calendar>
                                                    </td>
                                                    <td style="font-size: 10px;">&nbsp;
                                                    </td>
                                                    <td>
                                                        <img id="btn_gdpToDate_RuleParameters" alt="" class="calendar_button" onclick="btn_gdpToDate_RuleParameters_OnClick(event)"
                                                            onmouseup="btn_gdpToDate_RuleParameters_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <ComponentArt:Calendar ID="gCalToDate_RuleParameters" runat="server" AllowMonthSelection="false"
                                                AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpToDate_RuleParameters"
                                                PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                <ClientEvents>
                                                    <SelectionChanged EventHandler="gCalToDate_RuleParameters_OnChange" />
                                                    <Load EventHandler="gCalToDate_RuleParameters_onLoad" />
                                                </ClientEvents>
                                            </ComponentArt:Calendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" style="width: 50%">
                    <table style="width: 100%;" class="BoxStyle">
                        <tr>
                            <td id="" class="HeaderLabel">
                                <table style="width: 100%">
                                    <tr>
                                        <td id="header_RuleParameters_RuleParameters" class="HeaderLabel" style="width: 50%">Rule Parameteres
                                        </td>
                                        <td id="loadingPanel_GridRuleParameters_RuleParameters" class="HeaderLabel" style="width: 45%"></td>
                                        <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridRuleParameters_RuleParameters" runat="server"
                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRuleParameters_RuleParameters"
                                                        runat="server" ClientSideCommand="Refresh_GridRuleParameters_RuleParameters();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRuleParameters_RuleParameters"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridRuleParameters_RuleParameters"
                                    OnCallback="CallBack_GridRuleParameters_RuleParameters_onCallBack">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridRuleParameters_RuleParameters" runat="server" CssClass="Grid"
                                            EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                            PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="4" RunningMode="Client"
                                            SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false" ShowFooter="false"
                                            AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="150px">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                    SortImageWidth="9">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Title" DefaultSortDirection="Descending"
                                                            HeadingText="نام پارامتر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnParameterName_GridRuleParameters_RuleParameters"
                                                            Visible="true" Width="75" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Value" DefaultSortDirection="Descending"
                                                            HeadingText="مقدار پارامتر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnParameterValue_GridRuleParameters_RuleParameters"
                                                            Visible="true" Width="75" />
                                                        <ComponentArt:GridColumn DataField="Name" Visible="false" />
                                                        <ComponentArt:GridColumn DataField="Type" Visible="false" />
                                                        <ComponentArt:GridColumn DataField="IsInNextDay" Visible="false" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientEvents>
                                                <Load EventHandler="GridRuleParameters_RuleParameters_onLoad" />
                                                <ItemSelect EventHandler="GridRuleParameters_RuleParameters_onItemSelect" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_RuleParameters_RuleParameters" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridRuleParameters_RuleParameters_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridRuleParameters_RuleParameters_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <div style="width: 97%" class="TabStripContainer">
                                    <ComponentArt:TabStrip ID="TabStripRuleParametersTerms" runat="server" DefaultGroupTabSpacing="1"
                                        DefaultItemLookId="DefaultTabLook" DefaultSelectedItemLookId="SelectedTabLook"
                                        ImagesBaseUrl="images/TabStrip" MultiPageId="MultiPageRuleParametersTerms" ScrollLeftLookId="ScrollItem"
                                        ScrollRightLookId="ScrollItem" Width="100%">
                                        <ItemLooks>
                                            <ComponentArt:ItemLook CssClass="DefaultTab" HoverCssClass="DefaultTabHover" LabelPaddingBottom="4"
                                                LabelPaddingLeft="15" LabelPaddingRight="15" LabelPaddingTop="4" LeftIconHeight="22"
                                                LeftIconUrl="tab_left_icon.gif" LeftIconWidth="13" LookId="DefaultTabLook" meta:resourcekey="DefaultTabLook"
                                                RightIconHeight="22" RightIconUrl="tab_right_icon.gif" RightIconWidth="13" />
                                            <ComponentArt:ItemLook CssClass="SelectedTab" LabelPaddingBottom="4" LabelPaddingLeft="15"
                                                LabelPaddingRight="15" LabelPaddingTop="4" LeftIconHeight="22" LeftIconUrl="selected_tab_left_icon.gif"
                                                LeftIconWidth="13" LookId="SelectedTabLook" meta:resourcekey="SelectedTabLook"
                                                RightIconHeight="22" RightIconUrl="selected_tab_right_icon.gif" RightIconWidth="13" />
                                            <ComponentArt:ItemLook CssClass="ScrollItem" HoverCssClass="ScrollItemHover" LabelPaddingBottom="0"
                                                LabelPaddingLeft="5" LabelPaddingRight="5" LabelPaddingTop="0" LookId="ScrollItem" />
                                        </ItemLooks>
                                        <Tabs>
                                            <ComponentArt:TabStripTab ID="tbNumeric_TabStripRuleParametersTerms" meta:resourcekey="tbNumeric_TabStripRuleParametersTerms"
                                                Text="عددی" Value="Numeric" Enabled="false">
                                            </ComponentArt:TabStripTab>
                                            <ComponentArt:TabStripTab ID="tbTime_TabStripRuleParametersTerms" meta:resourcekey="tbTime_TabStripRuleParametersTerms"
                                                Text="زمان" Value="Time" Enabled="false">
                                            </ComponentArt:TabStripTab>
                                            <ComponentArt:TabStripTab ID="tbDate_TabStripRuleParametersTerms" meta:resourcekey="tbDate_TabStripRuleParametersTerms"
                                                Text="تاریخ" Value="Date" Enabled="false">
                                            </ComponentArt:TabStripTab>
                                        </Tabs>
                                    </ComponentArt:TabStrip>
                                </div>
                                <ComponentArt:MultiPage ID="MultiPageRuleParametersTerms" runat="server" CssClass="MultiPage"
                                    Width="300">
                                    <ComponentArt:PageView ID="pgvNumeric_MultiPageRuleParametersTerms" runat="server"
                                        Width="100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNumeric_RuleParameters" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblNumeric_RuleParameters"
                                                        Text=": عددی"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <input id="txtNumeric_RuleParameters" runat="server" type="text" class="TextBoxes"
                                                                    style="width: 95%" disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                            </td>
                                                            <td align="center">
                                                                <ComponentArt:ToolBar ID="TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms"
                                                                            runat="server" ClientSideCommand="tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png"
                                                                            Enabled="false" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms"
                                                                            TextImageSpacing="5" />
                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView ID="pgvTime_MultiPageRuleParametersTerms" runat="server" Width="100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTime_RuleParameters" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTime_RuleParameters"
                                                        Text=": زمان"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 90%">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 60%">
                                                                            <table dir="ltr" style="width: 100%">
                                                                                <tr>
                                                                                    <td align="center" style="width: 40%">
                                                                                        <input type="text" id="TimeSelector_Hour_RuleParameters_txtHour" style="width: 70%; text-align: center"
                                                                                            onchange="TimeSelector_Hour_RuleParameters_onChange('txtHour')" />
                                                                                    </td>
                                                                                    <td align="center">:
                                                                                    </td>
                                                                                    <td align="center" style="width: 40%">
                                                                                        <input type="text" id="TimeSelector_Hour_RuleParameters_txtMinute" style="width: 70%; text-align: center"
                                                                                            onchange="TimeSelector_Hour_RuleParameters_onChange('txtMinute')" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="width: 10%">
                                                                                        <input id="chbNextDay_RuleParameters" type="checkbox" /></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblNextDay_RuleParameters" runat="server" Text="روز بعد" CssClass="WhiteLabel" meta:resourcekey="lblNextDay_RuleParameters"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td align="center">
                                                                <ComponentArt:ToolBar ID="TlbConfirm_pgvTime_MultiPageRuleParametersTerms" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms"
                                                                            runat="server" ClientSideCommand="tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png"
                                                                            Enabled="false" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms"
                                                                            TextImageSpacing="5" />
                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView ID="pgvDate_MultiPageRuleParametersTerms" runat="server" Width="100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDate_RuleParameters" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDate_RuleParameters"
                                                        Text=": تاریخ"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Container_DateCalendars_RuleParameters">
                                                    <table runat="server" id="Container_pdpDate_RuleParameters" visible="false" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <pcal:PersianDatePickup ID="pdpDate_RuleParameters" runat="server" CssClass="PersianDatePicker"
                                                                    ReadOnly="true"></pcal:PersianDatePickup>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table runat="server" id="Container_gdpDate_RuleParameters" visible="false" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <table id="Container_gCalDate_RuleParameters" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td onmouseup="btn_gdpDate_RuleParameters_OnMouseUp(event)">
                                                                            <ComponentArt:Calendar ID="gdpDate_RuleParameters" runat="server" ControlType="Picker"
                                                                                PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                SelectedDate="2008-1-1">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gdpDate_RuleParameters_OnDateChange" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                        <td style="font-size: 10px;">&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <img id="btn_gdpDate_RuleParameters" alt="" class="calendar_button" onclick="btn_gdpDate_RuleParameters_OnClick(event)"
                                                                                onmouseup="btn_gdpDate_RuleParameters_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <ComponentArt:Calendar ID="gCalDate_RuleParameters" runat="server" AllowMonthSelection="false"
                                                                    AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                    CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                    DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                    MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                    OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpDate_RuleParameters"
                                                                    PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                    SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                    <ClientEvents>
                                                                        <SelectionChanged EventHandler="gCalDate_RuleParameters_OnChange" />
                                                                        <Load EventHandler="gCalDate_RuleParameters_onLoad" />
                                                                    </ClientEvents>
                                                                </ComponentArt:Calendar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <ComponentArt:ToolBar ID="TlbConfirm_pgvDate_MultiPageRuleParametersTerms" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms"
                                                                runat="server" ClientSideCommand="tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png"
                                                                Enabled="false" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                    </ComponentArt:PageView>
                                </ComponentArt:MultiPage>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <textarea id="txtParameterTitle_RuleParameters" style="width: 100%; height: 40px;"
                                    cols="20" class="TextBoxes" name="S2" rows="2" readonly="readonly"></textarea>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="2">
                    <asp:Label ID="lblRuleTitle_RuleParameters" runat="server" Text=": نام قانون" CssClass="WhiteLabel"
                        meta:resourcekey="lblRuleTitle_RuleParameters"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="2">
                    <textarea id="txtRuleTitle_RuleParameters" cols="20" name="S1" rows="3" style="height: 60px; width: 99%"
                        class="TextBoxes" readonly="readonly"></textarea>
                </td>
            </tr>
        </table>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
            runat="server" Width="280px">
            <Content>
                <table style="width: 100%;" class="ConfirmStyle">
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
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancel"
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
        <asp:HiddenField runat="server" ID="hfheader_RuleDateRanges_RuleParameters" meta:resourcekey="hfheader_RuleDateRanges_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfheader_RuleParameters_RuleParameters" meta:resourcekey="hfheader_RuleParameters_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfView_RuleParameters" meta:resourcekey="hfView_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfAdd_RuleParameters" meta:resourcekey="hfAdd_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfEdit_RuleParameters" meta:resourcekey="hfEdit_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfDelete_RuleParameters" meta:resourcekey="hfDelete_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_RuleParameters" meta:resourcekey="hfDeleteMessage_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_RuleParameters" meta:resourcekey="hfCloseMessage_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridRuleDateRanges_RuleParameters"
            meta:resourcekey="hfloadingPanel_GridRuleDateRanges_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridRuleParameters_RuleParameters"
            meta:resourcekey="hfloadingPanel_GridRuleParameters_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogRuleParameters" meta:resourcekey="hfTitle_DialogRuleParameters" />
        <asp:HiddenField runat="server" ID="hfCurrentDate_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfErrorType_RuleParameters" meta:resourcekey="hfErrorType_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfConnectionError_RuleParameters" meta:resourcekey="hfConnectionError_RuleParameters" />
    </form>
</body>
</html>
