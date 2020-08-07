<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OvertimeJustificationRequest.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.OvertimeJustificationRequest" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
</head>
<body onkeydown="OvertimeJustificationRequest_onKeyDown(event);">
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="OvertimeJustificationRequestForm" runat="server" meta:resourcekey="OvertimeJustificationRequestForm"
    onsubmit="return false;">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
        <tr style="height: 5%">
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbOvertimeJustificationRequest" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbOvertimeJustificationRequest" runat="server"
                                        ClientSideCommand="tlbItemSave_TlbOvertimeJustificationRequest_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbOvertimeJustificationRequest"
                                        TextImageSpacing="5" Enabled="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbOvertimeJustificationRequest" runat="server"
                                        ClientSideCommand="tlbItemCancel_TlbOvertimeJustificationRequest_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbOvertimeJustificationRequest"
                                        TextImageSpacing="5" Enabled="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbOvertimeJustificationRequest" runat="server"
                                        ClientSideCommand="tlbItemDelete_TlbOvertimeJustificationRequest_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDelete_TlbOvertimeJustificationRequest"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemShiftView_TlbOvertimeJustificationRequest" runat="server"
                                        ClientSideCommand="tlbItemShiftView_TlbOvertimeJustificationRequest_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemShiftView_TlbOvertimeJustificationRequest"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbOvertimeJustificationRequest" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbOvertimeJustificationRequest"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbOvertimeJustificationRequest_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbOvertimeJustificationRequest"
                                        runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbOvertimeJustificationRequest_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbOvertimeJustificationRequest"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbOvertimeJustificationRequest" runat="server"
                                        DropDownImageHeight="16px" ClientSideCommand="tlbItemExit_TlbOvertimeJustificationRequest_onClick();"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbOvertimeJustificationRequest"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="tdSelectedDate_OvertimeJustificationRequest" class="HeaderLabel" style="width: 30%">
                            &nbsp;
                        </td>
                        <td runat="server" id="ActionMode_OvertimeJustificationRequest" style="width: 10%"
                            class="ToolbarMode" meta:resourcekey="InverseAlignObj">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 5%">
            <td>
                <div runat="server" meta:resourcekey="AlignObj" style="width: 80%" class="DropDownHeader">
                    <img alt="" runat="server" id="imgbox_OvertimeJustificationRequest_OvertimeJustificationRequest"
                        src="~/Images/Ghadir/arrowDown.jpg" onclick="imgbox_OvertimeJustificationRequest_OvertimeJustificationRequest_onClick();" />
                    <span id="header_imgbox_OvertimeJustificationRequest_OvertimeJustificationRequest">درخواست
                        مجوز اضافه کاری</span>
                </div>
                <div class="dhtmlgoodies_contentBox" id="box_OvertimeJustificationRequest_OvertimeJustificationRequest"
                    style="width: 70%;">
                    <div class="dhtmlgoodies_content" id="subbox_OvertimeJustificationRequest_OvertimeJustificationRequest">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblOverTimeType_OvertimeJustificationRequest" runat="server" CssClass="WhiteLabel"
                                        meta:resourcekey="lblOverTimeType_OvertimeJustificationRequest" Text=": نوع اضافه کاری"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <ComponentArt:CallBack ID="CallBack_cmbOverTimeType_OvertimeJustificationRequest"
                                        runat="server" OnCallback="CallBack_cmbOverTimeType_OvertimeJustificationRequest_onCallBack"
                                        Height="26">
                                        <Content>
                                            <ComponentArt:ComboBox ID="cmbOverTimeType_OvertimeJustificationRequest" runat="server"
                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                DataTextField="Name" DataValueField="ID" DropDownCssClass="comboDropDown" DropDownHeight="70"
                                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100%" >
                                                <ClientEvents>
                                                    <Expand EventHandler="cmbOverTimeType_OvertimeJustificationRequest_onExpand" />
                                                    <Collapse EventHandler="cmbOverTimeType_OvertimeJustificationRequest_onCollapse" />
                                                </ClientEvents>
                                            </ComponentArt:ComboBox>
                                            <asp:HiddenField ID="ErrorHiddenField_OverTimeTypes" runat="server" />
                                        </Content>
                                        <ClientEvents>
                                            <BeforeCallback EventHandler="CallBack_cmbOverTimeType_OvertimeJustificationRequest_onBeforeCallback" />
                                            <CallbackComplete EventHandler="CallBack_cmbOverTimeType_OvertimeJustificationRequest_onCallbackComplete" />
                                            <CallbackError EventHandler="CallBack_cmbOverTimeType_OvertimeJustificationRequest_onCallbackError" />
                                        </ClientEvents>
                                    </ComponentArt:CallBack>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFromDate_OvertimeJustificationRequest" runat="server" CssClass="WhiteLabel"
                                        meta:resourcekey="lblFromDate_OvertimeJustificationRequest" Text=": از تاریخ"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblToDate_OvertimeJustificationRequest" runat="server" CssClass="WhiteLabel"
                                        meta:resourcekey="lblToDate_OvertimeJustificationRequest" Text=": تا تاریخ"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td id="Container_FromDateCalendars_OvertimeJustificationRequest">
                                    <table runat="server" id="Container_pdpFromDate_OvertimeJustificationRequest" visible="false"
                                        style="width: 100%">
                                        <tr>
                                            <td>
                                                <pcal:PersianDatePickup ID="pdpFromDate_OvertimeJustificationRequest" runat="server"
                                                    CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="Container_gdpFromDate_OvertimeJustificationRequest" visible="false"
                                        style="width: 100%">
                                        <tr>
                                            <td>
                                                <table id="Container_gCalFromDate_OvertimeJustificationRequest" border="0" cellpadding="0"
                                                    cellspacing="0">
                                                    <tr>
                                                        <td onmouseup="btn_gdpFromDate_OvertimeJustificationRequest_OnMouseUp(event)">
                                                            <ComponentArt:Calendar ID="gdpFromDate_OvertimeJustificationRequest" runat="server"
                                                                ControlType="Picker" MaxDate="2021-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                <ClientEvents>
                                                                    <SelectionChanged EventHandler="gdpFromDate_OvertimeJustificationRequest_OnDateChange" />
                                                                </ClientEvents>
                                                            </ComponentArt:Calendar>
                                                        </td>
                                                        <td style="font-size: 10px;">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <img id="btn_gdpFromDate_OvertimeJustificationRequest" alt="" class="calendar_button"
                                                                onclick="btn_gdpFromDate_OvertimeJustificationRequest_OnClick(event)" onmouseup="btn_gdpFromDate_OvertimeJustificationRequest_OnMouseUp(event)"
                                                                src="Images/Calendar/btn_calendar.gif" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <ComponentArt:Calendar ID="gCalFromDate_OvertimeJustificationRequest" runat="server"
                                                    AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                    CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                    DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                    ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                    NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                    PopUpExpandControlId="btn_gdpFromDate_OvertimeJustificationRequest" PrevImageUrl="cal_prevMonth.gif"
                                                    SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                    SwapSlide="Linear" VisibleDate="2008-1-1">
                                                    <ClientEvents>
                                                        <SelectionChanged EventHandler="gCalFromDate_OvertimeJustificationRequest_OnChange" />
                                                        <Load EventHandler="gCalFromDate_OvertimeJustificationRequest_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:Calendar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="Container_ToDateCalendars_OvertimeJustificationRequest">
                                    <table runat="server" id="Container_pdpToDate_OvertimeJustificationRequest" visible="false"
                                        style="width: 100%">
                                        <tr>
                                            <td>
                                                <pcal:PersianDatePickup ID="pdpToDate_OvertimeJustificationRequest" runat="server"
                                                    CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="Container_gdpToDate_OvertimeJustificationRequest" visible="false"
                                        style="width: 100%">
                                        <tr>
                                            <td>
                                                <table id="Container_gCalToDate_OvertimeJustificationRequest" border="0" cellpadding="0"
                                                    cellspacing="0">
                                                    <tr>
                                                        <td onmouseup="btn_gdpToDate_OvertimeJustificationRequest_OnMouseUp(event)">
                                                            <ComponentArt:Calendar ID="gdpToDate_OvertimeJustificationRequest" runat="server"
                                                                ControlType="Picker" MaxDate="2021-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                <ClientEvents>
                                                                    <SelectionChanged EventHandler="gdpToDate_OvertimeJustificationRequest_OnDateChange" />
                                                                </ClientEvents>
                                                            </ComponentArt:Calendar>
                                                        </td>
                                                        <td style="font-size: 10px;">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <img id="btn_gdpToDate_OvertimeJustificationRequest" alt="" class="calendar_button"
                                                                onclick="btn_gdpToDate_OvertimeJustificationRequest_OnClick(event)" onmouseup="btn_gdpToDate_OvertimeJustificationRequest_OnMouseUp(event)"
                                                                src="Images/Calendar/btn_calendar.gif" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <ComponentArt:Calendar ID="gCalToDate_OvertimeJustificationRequest" runat="server"
                                                    AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                    CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                    DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                    ImagesBaseUrl="Images/Calendar" MaxDate="2021-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                    NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                    PopUpExpandControlId="btn_gdpToDate_OvertimeJustificationRequest" PrevImageUrl="cal_prevMonth.gif"
                                                    SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                    SwapSlide="Linear" VisibleDate="2008-1-1">
                                                    <ClientEvents>
                                                        <SelectionChanged EventHandler="gCalToDate_OvertimeJustificationRequest_OnChange" />
                                                        <Load EventHandler="gCalToDate_OvertimeJustificationRequest_OnLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:Calendar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table class="BoxStyle" style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFromHour_OvertimeJustificationRequest" runat="server" CssClass="WhiteLabel"
                                                    meta:resourcekey="lblFromHour_OvertimeJustificationRequest" Text=": از ساعت"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblToHour_OvertimeJustificationRequest" runat="server" CssClass="WhiteLabel"
                                                    meta:resourcekey="lblToHour_OvertimeJustificationRequest" Text=": تا ساعت"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTimeDuration_OvertimeJustificationRequest" runat="server" CssClass="WhiteLabel"
                                                    meta:resourcekey="lblTimeDuration_OvertimeJustificationRequest" Text=": مدت زمان"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td1" runat="server" meta:resourcekey="AlignObj">
                                                <MKB:TimeSelector ID="TimeSelector_FromHour_OvertimeJustificationRequest" runat="server"
                                                    DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;"
                                                    Visible="true">
                                                </MKB:TimeSelector>
                                            </td>
                                            <td id="Td2" runat="server" meta:resourcekey="AlignObj">
                                                <MKB:TimeSelector ID="TimeSelector_ToHour_OvertimeJustificationRequest" runat="server"
                                                    DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;"
                                                    Visible="true">
                                                </MKB:TimeSelector>
                                            </td>
                                            <td id="Td3" runat="server" meta:resourcekey="AlignObj">
                                                <MKB:TimeSelector ID="TimeSelector_TimeDuration_OvertimeJustificationRequest" runat="server"
                                                    DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;"
                                                    Visible="true">
                                                </MKB:TimeSelector>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDescription_OvertimeJustificationRequest" runat="server" CssClass="WhiteLabel"
                                        meta:resourcekey="lblDescription_OvertimeJustificationRequest" Text=": توضیحات"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <textarea id="txtDescription_OvertimeJustificationRequest" cols="20" name="S1" rows="2"
                                        style="width: 97%" class="TextBoxes"></textarea>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr style="height: 90%">
            <td valign="top">
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td id="header_RegisteredRequests_OvertimeJustificationRequest" class="HeaderLabel"
                                        style="width: 50%">
                                        Registered Requests
                                    </td>
                                    <td id="loadingPanel_GridRegisteredRequests_OvertimeJustificationRequest" class="HeaderLabel"
                                        style="width: 45%">
                                    </td>
                                    <td id="Td6" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbRefresh_GridRegisteredRequests_OvertimeJustificationRequest"
                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRegisteredRequests_OvertimeJustificationRequest"
                                                    runat="server" ClientSideCommand="Refresh_GridRegisteredRequests_OvertimeJustificationRequest();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRegisteredRequests_OvertimeJustificationRequest"
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
                            <ComponentArt:CallBack ID="CallBack_GridRegisteredRequests_OvertimeJustificationRequest"
                                runat="server" OnCallback="CallBack_GridRegisteredRequests_OvertimeJustificationRequest_onCallBack"
                                Width="680">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridRegisteredRequests_OvertimeJustificationRequest" runat="server"
                                        AllowHorizontalScrolling="true" CssClass="Grid" EnableViewState="true" ShowFooter="false"
                                        FillContainer="true" FooterCssClass="GridFooter" Height="150" ImagesBaseUrl="images/Grid/"
                                        PagePaddingEnabled="true" PageSize="14" RunningMode="Client" Width="590" AllowMultipleSelect="false"
                                        AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                        ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                        ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                        ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                        <Levels>
                                            <ComponentArt:GridLevel AllowReordering="false" AllowSorting="false" AlternatingRowCssClass="AlternatingRow"
                                                DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Title" DefaultSortDirection="Descending"
                                                        HeadingText="نوع درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestType_GridRegisteredRequests_OvertimeJustificationRequest" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheFromDate" DefaultSortDirection="Descending"
                                                        HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromDate_GridRegisteredRequests_OvertimeJustificationRequest" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheToDate" DefaultSortDirection="Descending"
                                                        HeadingText="تا تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToDate_GridRegisteredRequests_OvertimeJustificationRequest" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheFromTime" DefaultSortDirection="Descending"
                                                        HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromHour_GridRegisteredRequests_OvertimeJustificationRequest" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheFromTime" DefaultSortDirection="Descending"
                                                        HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToHour_GridRegisteredRequests_OvertimeJustificationRequest" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheTimeDuration" DefaultSortDirection="Descending"
                                                        HeadingText="مدت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmDuration_GridRegisteredRequests_OvertimeJustificationRequest" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RegistrationDate" DefaultSortDirection="Descending"
                                                        HeadingText="تاربخ ثبت درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestDate_GridRegisteredRequests_OvertimeJustificationRequest" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="StatusTitle" DefaultSortDirection="Descending"
                                                        DataCellClientTemplateId="DataCellClientTemplate_clmnState_GridRegisteredRequests_OvertimeJustificationRequest"
                                                        HeadingText="وضعیت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnState_GridRegisteredRequests_OvertimeJustificationRequest" />
                                                    <ComponentArt:GridColumn DataField="Status" Visible="false" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientTemplates>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnState_GridRegisteredRequests_OvertimeJustificationRequest">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center">
                                                            ##GetRequestStateTitle_OvertimeJustificationRequest(DataItem.GetMember('Status').Value)##
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                        </ClientTemplates>
                                        <ClientEvents>
                                            <Load EventHandler="GridRegisteredRequests_OvertimeJustificationRequest_onLoad" />
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_RegisteredRequests" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridRegisteredRequests_OvertimeJustificationRequest_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridRegisteredRequests_OvertimeJustificationRequest_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogShiftsView"
        HeaderClientTemplateId="DialogShiftsViewheader" FooterClientTemplateId="DialogShiftsViewfooter"
        runat="server" PreloadContentUrl="false" ContentUrl="ShiftsView.aspx" IFrameCssClass="ShiftsView_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogShiftsViewheader">
                <table id="tbl_DialogShiftsViewheader" style="width: 402px;" cellpadding="0" cellspacing="0"
                    border="0" onmousedown="DialogShiftsView.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogShiftsView_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px;">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogShiftsView" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold;">
                                    </td>
                                    <td id="CloseButton_DialogShiftsView" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogShiftsView_IFrame').src = 'WhitePage.aspx'; DialogShiftsView.Close('cancelled');" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogShiftsView_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogShiftsViewfooter">
                <table id="tbl_DialogShiftsViewfooter" style="width: 402px" cellpadding="0" cellspacing="0"
                    border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogShiftsView_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogShiftsView_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogShiftsView_onShow" />
            <OnClose EventHandler="DialogShiftsView_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="300px">
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
    <asp:HiddenField runat="server" ID="hfView_OvertimeJustificationRequest" meta:resourcekey="hfView_OvertimeJustificationRequest" />
    <asp:HiddenField runat="server" ID="hfAdd_OvertimeJustificationRequest" meta:resourcekey="hfAdd_OvertimeJustificationRequest" />
    <asp:HiddenField runat="server" ID="hfDelete_OvertimeJustificationRequest" meta:resourcekey="hfDelete_OvertimeJustificationRequest" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_OvertimeJustificationRequest" meta:resourcekey="hfcmbAlarm_OvertimeJustificationRequest" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridRegisteredRequests_OvertimeJustificationRequest"
        meta:resourcekey="hfloadingPanel_GridRegisteredRequests_OvertimeJustificationRequest" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogOvertimeJustificationRequest" meta:resourcekey="hfTitle_DialogOvertimeJustificationRequest" />
    <asp:HiddenField runat="server" ID="hfheader_RegisteredRequests_OvertimeJustificationRequest"
        meta:resourcekey="hfheader_RegisteredRequests_OvertimeJustificationRequest" />
    <asp:HiddenField runat="server" ID="hfRequestStates_OvertimeJustificationRequest" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_OvertimeJustificationRequest"
        meta:resourcekey="hfDeleteMessage_OvertimeJustificationRequest" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_OvertimeJustificationRequest"
        meta:resourcekey="hfCloseMessage_OvertimeJustificationRequest" />
    <asp:HiddenField runat="server" ID="hfheader_imgbox_OvertimeJustificationRequest_OvertimeJustificationRequest"
        meta:resourcekey="hfheader_imgbox_OvertimeJustificationRequest_OvertimeJustificationRequest" />
    <asp:HiddenField runat="server" ID="hfCurrentDate_OvertimeJustificationRequest" />
    <asp:HiddenField runat="server" ID="hfErrorType_OvertimeJustificationRequest" meta:resourcekey="hfErrorType_OvertimeJustificationRequest" />
    <asp:HiddenField runat="server" ID="hfConnectionError_OvertimeJustificationRequest"
        meta:resourcekey="hfConnectionError_OvertimeJustificationRequest" />
    </form>
</body>
</html>
