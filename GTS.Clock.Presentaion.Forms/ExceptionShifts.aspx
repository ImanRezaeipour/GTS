<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.ExceptionShifts"
    CodeFile="ExceptionShifts.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="ExceptionShiftsForm" runat="server" meta:resourcekey="ExceptionShiftsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table id="Mastertbl_ExceptionShifts" style="font-size: small; background-color: White;
        font-family: Arial;" class="BoxStyle">
        <tr>
            <td valign="top">
                <ComponentArt:TabStrip ID="TabStripExceptionShifts" runat="server" DefaultGroupTabSpacing="1"
                    DefaultItemLookId="DefaultTabLook" DefaultSelectedItemLookId="SelectedTabLook"
                    ImagesBaseUrl="images/TabStrip" MultiPageId="MultiPageExceptionShifts" ScrollLeftLookId="ScrollItem"
                    ScrollRightLookId="ScrollItem">
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
                        <ComponentArt:TabStripTab ID="tbDetails_TabStripShiftDetails" meta:resourcekey="tbDetails_TabStripShiftDetails"
                            Text="جزئیات">
                        </ComponentArt:TabStripTab>
                        <ComponentArt:TabStripTab ID="tbTwoDayReplacement_TabStripShiftDetails" meta:resourcekey="tbTwoDayReplacement_TabStripShiftDetails"
                            Text="جابجایی دو روز با هم">
                        </ComponentArt:TabStripTab>
                        <ComponentArt:TabStripTab ID="tbTwoPersonnelReplacement_TabStripShiftDetails" meta:resourcekey="tbTwoPersonnelReplacement_TabStripShiftDetails"
                            Text="جابجایی دو نفر با هم">
                        </ComponentArt:TabStripTab>
                    </Tabs>
                </ComponentArt:TabStrip>
                <ComponentArt:MultiPage ID="MultiPageExceptionShifts" runat="server" CssClass="MultiPage"
                    Width="700">
                    <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvDetails_DialogExceptionShifts"
                        Visible="true">
                        <table style="width: 100%; height: 200px; font-family: Arial; font-size: small" class="BoxStyle">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 70%">
                                                            <ComponentArt:ToolBar ID="TlbExceptionShiftsDetails" runat="server" CssClass="toolbar"
                                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemEndorsement_TlbExceptionShiftsDetails" runat="server"
                                                                        ClientSideCommand="tlbItemEndorsement_TlbExceptionShiftsDetails_onClick();" DropDownImageHeight="16px"
                                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                                                        ItemType="Command" meta:resourcekey="tlbItemEndorsement_TlbExceptionShiftsDetails"
                                                                        TextImageSpacing="5" />
                                                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbExceptionShiftsDetails"
                                                                        runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbExceptionShiftsDetails_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbExceptionShiftsDetails"
                                                                        TextImageSpacing="5" />
                                                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbExceptionShiftsDetails" runat="server"
                                                                        ClientSideCommand="tlbItemExit_TlbExceptionShiftsDetails_onClick();" DropDownImageHeight="16px"
                                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbExceptionShiftsDetails" TextImageSpacing="5" />
                                                                </Items>
                                                            </ComponentArt:ToolBar>
                                                        </td>
                                                        <td id="ActionMode_Details_ExceptionShifts" class="ToolbarMode">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr id="Tr9" runat="server">
                                                        <td id="Td1" runat="server" meta:resourcekey="VAlignObj">
                                                            <asp:Label ID="lblDetailsFromDate_ExceptionShifts" runat="server" CssClass="WhiteLabel"
                                                                meta:resourcekey="lblFromDate_ExceptionShifts" Text="از تاریخ :"></asp:Label>
                                                        </td>
                                                        <td id="Td2" runat="server" meta:resourcekey="VAlignObj">
                                                            <asp:Label ID="lblDetailsToDate_ExceptionShifts" runat="server" CssClass="WhiteLabel"
                                                                meta:resourcekey="lblToDate_ExceptionShifts" Text="تا تاریخ :"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr10" runat="server">
                                                        <td id="Container_DetailsFromDateCalendars_ExceptionShifts" runat="server" meta:resourcekey="VAlignObj">
                                                            <table runat="server" id="Container_pdpDetailsFromDate_ExceptionShifts" visible="false"
                                                                style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <pcal:PersianDatePickup ID="pdpDetailsFromDate_ExceptionShifts" runat="server" CssClass="PersianDatePicker"
                                                                            ReadOnly="true"></pcal:PersianDatePickup>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table runat="server" id="Container_gdpDetailsFromDate_ExceptionShifts" visible="false"
                                                                style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <table id="Container_gCalDetailsFromDate_ExceptionShifts" border="0" cellpadding="0"
                                                                            cellspacing="0">
                                                                            <tr>
                                                                                <td onmouseup="btn_gdpDetailsFromDate_ExceptionShifts_OnMouseUp(event)">
                                                                                    <ComponentArt:Calendar ID="gdpDetailsFromDate_ExceptionShifts" runat="server" ControlType="Picker"
                                                                                        PickerCssClass="picker" MaxDate="2122-1-1" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                        SelectedDate="2008-1-1">
                                                                                        <ClientEvents>
                                                                                            <SelectionChanged EventHandler="gdpDetailsFromDate_ExceptionShifts_OnDateChange" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:Calendar>
                                                                                </td>
                                                                                <td style="font-size: 10px;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <img id="btn_gdpDetailsFromDate_ExceptionShifts" alt="" class="calendar_button" onclick="btn_gdpDetailsFromDate_ExceptionShifts_OnClick(event)"
                                                                                        onmouseup="btn_gdpDetailsFromDate_ExceptionShifts_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <ComponentArt:Calendar ID="gCalDetailsFromDate_ExceptionShifts" runat="server" AllowMonthSelection="false"
                                                                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                            CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                            DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                            MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                            OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpDetailsFromDate_ExceptionShifts"
                                                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                                            <ClientEvents>
                                                                                <SelectionChanged EventHandler="gCalDetailsFromDate_ExceptionShifts_OnChange" />
                                                                                <Load EventHandler="gCalDetailsFromDate_ExceptionShifts_onLoad" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:Calendar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td id="Container_DetailsToDateCalendars_ExceptionShifts" runat="server" meta:resourcekey="VAlignObj">
                                                            <table runat="server" id="Container_pdpDetailsToDate_ExceptionShifts" visible="false"
                                                                style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <pcal:PersianDatePickup ID="pdpDetailsToDate_ExceptionShifts" runat="server" CssClass="PersianDatePicker"
                                                                            ReadOnly="true"></pcal:PersianDatePickup>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table runat="server" id="Container_gdpDetailsToDate_ExceptionShifts" visible="false"
                                                                style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <table id="Container_gCalDetailsToDate_ExceptionShifts" border="0" cellpadding="0"
                                                                            cellspacing="0">
                                                                            <tr>
                                                                                <td onmouseup="btn_gdpDetailsToDate_ExceptionShifts_OnMouseUp(event)">
                                                                                    <ComponentArt:Calendar ID="gdpDetailsToDate_ExceptionShifts" runat="server" ControlType="Picker"
                                                                                        PickerCssClass="picker" MaxDate="2122-1-1" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                        SelectedDate="2008-1-1">
                                                                                        <ClientEvents>
                                                                                            <SelectionChanged EventHandler="gdpDetailsToDate_ExceptionShifts_OnDateChange" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:Calendar>
                                                                                </td>
                                                                                <td style="font-size: 10px;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <img id="btn_gdpDetailsToDate_ExceptionShifts" alt="" class="calendar_button" onclick="btn_gdpDetailsToDate_ExceptionShifts_OnClick(event)"
                                                                                        onmouseup="btn_gdpDetailsToDate_ExceptionShifts_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <ComponentArt:Calendar ID="gCalDetailsToDate_ExceptionShifts" runat="server" AllowMonthSelection="false"
                                                                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                            CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                            DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                            MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                            OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpDetailsToDate_ExceptionShifts"
                                                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                                            <ClientEvents>
                                                                                <SelectionChanged EventHandler="gCalDetailsToDate_ExceptionShifts_OnChange" />
                                                                                <Load EventHandler="gCalDetailsToDate_ExceptionShifts_onLoad" />
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
                                        <tr id="Tr11" runat="server">
                                            <td>
                                                <asp:Label ID="lblShift_ExceptionShifts" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblShift_ExceptionShifts"
                                                    Text="شیفت :"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="Tr12" runat="server">
                                            <td>
                                                <div style="width: 100%">
                                                    <ComponentArt:CallBack runat="server" ID="CallBack_cmbShift_ExceptionShifts" OnCallback="CallBack_cmbShift_ExceptionShifts_onCallBack">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbShift_ExceptionShifts" runat="server" AutoComplete="true"
                                                                AutoHighlight="false" CssClass="comboBox" DataTextField="Name" DataValueField="ID"
                                                                DropDownCssClass="comboDropDown" DropDownHeight="150" DropDownPageSize="10" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client" TextBoxEnabled="true"
                                                                SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbShift_ExceptionShifts_onExpand" />
                                                                    <Collapse EventHandler="cmbShift_ExceptionShifts_onCollapse" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_Shifts" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbShift_ExceptionShifts_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbShift_ExceptionShifts_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbShift_ExceptionShifts_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ComponentArt:PageView>
                    <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvTwoDayReplacement_TabStripShiftDetails"
                        Visible="true">
                        <table style="width: 100%; height: 200px; font-family: Arial; font-size: small" class="BoxStyle">
                            <tr>
                                <td style="height: 10%">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <ComponentArt:ToolBar ID="TlbTwoDayReplacement" runat="server" CssClass="toolbar"
                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                    UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemEndorsement_TlbTwoDayReplacement" runat="server"
                                                            ClientSideCommand="tlbItemEndorsement_TlbTwoDayReplacement_onClick();" DropDownImageHeight="16px"
                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                                            ItemType="Command" meta:resourcekey="tlbItemEndorsement_TlbTwoDayReplacement"
                                                            TextImageSpacing="5" />
                                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbTwoDayReplacement" runat="server"
                                                            ClientSideCommand="tlbItemFormReconstruction_TlbTwoDayReplacement_onClick();"
                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbTwoDayReplacement"
                                                            TextImageSpacing="5" />
                                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbTwoDayReplacement" runat="server" ClientSideCommand="tlbItemExit_TlbTwoDayReplacement_onClick();"
                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbTwoDayReplacement"
                                                            TextImageSpacing="5" />
                                                    </Items>
                                                </ComponentArt:ToolBar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 30%">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%;" class="BoxStyle">
                                                    <tr>
                                                        <td style="width: 20%" valign="top">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 5%">
                                                                        <input id="rdbWorkGroup_TwoDayReplacement_ExceptionShifts" name="TwoDayReplacement_ExceptionShifts"
                                                                            onclick="rdbWorkGroup_TwoDayReplacement_ExceptionShifts_onClick();" type="radio"
                                                                            value="V1" />
                                                                    </td>
                                                                    <td style="width: 95%">
                                                                        <asp:Label ID="lblWorkGroupchb_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                            CssClass="WhiteLabel" meta:resourcekey="lblWorkGroupchb_TwoDayReplacement_ExceptionShifts"
                                                                            Text="گروه کاری :"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblWorkGroup_TwoDayReplacement_ExceptionShifts" runat="server" CssClass="WhiteLabel"
                                                                            meta:resourcekey="lblWorkGroup_TwoDayReplacement_ExceptionShifts" Text="گروه کاری :"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbWorkGroup_TwoDayReplacement_ExceptionShifts"
                                                                            OnCallback="CallBack_cmbWorkGroup_TwoDayReplacement_ExceptionShifts_onCallBack">
                                                                            <Content>
                                                                                <ComponentArt:ComboBox ID="cmbWorkGroup_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                                    AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                    DataTextField="Name" DataValueField="ID" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                                                    TextBoxEnabled="true" Width="50%" Enabled="false">
                                                                                    <ClientEvents>
                                                                                        <Expand EventHandler="cmbWorkGroup_TwoDayReplacement_ExceptionShifts_onExpand" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:ComboBox>
                                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_WorkGroups_TwoDayReplacement_ExceptionShifts" />
                                                                            </Content>
                                                                            <ClientEvents>
                                                                                <BeforeCallback EventHandler="CallBack_cmbWorkGroup_TwoDayReplacement_ExceptionShifts_onBeforeCallback" />
                                                                                <CallbackComplete EventHandler="CallBack_cmbWorkGroup_TwoDayReplacement_ExceptionShifts_onCallbackComplete" />
                                                                                <CallbackError EventHandler="CallBack_cmbWorkGroup_TwoDayReplacement_ExceptionShifts_onCallbackError" />
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
                                        <tr>
                                            <td>
                                                <table style="width: 100%;" class="BoxStyle">
                                                    <tr>
                                                        <td id="Container_rdbPersonnel_TwoDayReplacement_ExceptionShifts" style="width: 20%"
                                                            valign="top">
                                                            <table id="Table1" runat="server" meta:resourcekey="tbl_rdbPersonnel_TwoDayReplacement_ExceptionShifts">
                                                                <tr>
                                                                    <td style="width: 5%">
                                                                        <input id="rdbPersonnel_TwoDayReplacement_ExceptionShifts" checked="checked" name="TwoDayReplacement_ExceptionShifts"
                                                                            onclick="rdbPersonnel_TwoDayReplacement_ExceptionShifts_onClick();" type="radio"
                                                                            value="V1" />
                                                                    </td>
                                                                    <td id="Td4" style="width: 95%">
                                                                        <asp:Label ID="lblPersonnelchb_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                            CssClass="WhiteLabel" meta:resourcekey="lblPersonnelchb_TwoDayReplacement_ExceptionShifts"
                                                                            Text="پرسنل :"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table style="width: 50%">
                                                                <tr>
                                                                    <td style="width: 50%">
                                                                        <div id="Div2" runat="server" class="DropDownHeader" meta:resourcekey="AlignObj"
                                                                            style="width: 100%">
                                                                            <img id="imgbox_SearchByPersonnel_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                                alt="" onclick="imgbox_SearchByPersonnel_TwoDayReplacement_ExceptionShifts_onClick();"
                                                                                src="Images/Ghadir/arrowDown.jpg" />
                                                                            <span id="header_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts">انتخاب پرسنل</span>
                                                                        </div>
                                                                    </td>
                                                                    <td align="center" style="color: White" id="tdSelectedPersonnel_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div id="box_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts" class="dhtmlgoodies_contentBox"
                                                                style="width: 50%;">
                                                                <div id="subbox_SearchByPersonnel_TwoDayReplacement_ExceptionShifts" class="dhtmlgoodies_content">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <table style="width: 100%;">
                                                                                    <tr>
                                                                                        <td style="width: 90%">
                                                                                            <table style="width: 100%;">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblPersonnel_TwoDayReplacement_ExceptionShifts" runat="server" CssClass="WhiteLabel"
                                                                                                            meta:resourcekey="lblPersonnel_TwoDayReplacement_ExceptionShifts" Text=": پرسنل"></asp:Label>
                                                                                                    </td>
                                                                                                    <td id="Td3" runat="server" meta:resourcekey="InverseAlignObj">
                                                                                                        <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts"
                                                                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                                            UseFadeEffect="false" Style="direction: ltr">
                                                                                                            <Items>
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" />
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="first.png"
                                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" />
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Before.png"
                                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" />
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Next.png"
                                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" />
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="last.png"
                                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_TwoDayReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" />
                                                                                                            </Items>
                                                                                                        </ComponentArt:ToolBar>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 90%">
                                                                                            <ComponentArt:CallBack ID="CallBack_cmbPersonnel_TwoDayReplacement_ExceptionShifts"
                                                                                                OnCallback="CallBack_cmbPersonnel_TwoDayReplacement_ExceptionShifts_onCallBack"
                                                                                                runat="server" Height="26">
                                                                                                <Content>
                                                                                                    <ComponentArt:ComboBox ID="cmbPersonnel_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                                                        AutoComplete="true" AutoHighlight="false" CssClass="comboBox" DataFields="BarCode"
                                                                                                        DataTextField="Name" DropDownCssClass="comboDropDown" DropDownHeight="220" DropDownPageSize="7"
                                                                                                        DropDownWidth="400" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_TwoDayReplacement_ExceptionShifts"
                                                                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client" TextBoxEnabled="true"
                                                                                                        SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                                                        <ClientTemplates>
                                                                                                            <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_TwoDayReplacement_ExceptionShifts">
                                                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                    <tr class="dataRow">
                                                                                                                        <td class="dataCell" style="width: 40%">
                                                                                                                            ## DataItem.getProperty('Text') ##
                                                                                                                        </td>
                                                                                                                        <td class="dataCell" style="width: 30%">
                                                                                                                            ## DataItem.getProperty('BarCode') ##
                                                                                                                        </td>
                                                                                                                        <td class="dataCell" style="width: 30%">
                                                                                                                            ## DataItem.getProperty('CardNum') ##
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </ComponentArt:ClientTemplate>
                                                                                                        </ClientTemplates>
                                                                                                        <DropDownHeader>
                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="400">
                                                                                                                <tr class="headingRow">
                                                                                                                    <td id="clmnName_cmbPersonnel_TwoDayReplacement_ExceptionShifts" class="headingCell"
                                                                                                                        style="width: 40%; text-align: center">
                                                                                                                        Name And Family
                                                                                                                    </td>
                                                                                                                    <td id="clmnBarCode_cmbPersonnel_TwoDayReplacement_ExceptionShifts" class="headingCell"
                                                                                                                        style="width: 30%; text-align: center">
                                                                                                                        BarCode
                                                                                                                    </td>
                                                                                                                    <td id="clmnCardNum_cmbPersonnel_TwoDayReplacement_ExceptionShifts" class="headingCell"
                                                                                                                        style="width: 30%; text-align: center">
                                                                                                                        CardNum
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </DropDownHeader>
                                                                                                        <ClientEvents>
                                                                                                            <Expand EventHandler="cmbPersonnel_TwoDayReplacement_ExceptionShifts_onExpand" />
                                                                                                            <Change EventHandler="cmbPersonnel_TwoDayReplacement_ExceptionShifts_onChange" />
                                                                                                        </ClientEvents>
                                                                                                    </ComponentArt:ComboBox>
                                                                                                    <asp:HiddenField ID="ErrorHiddenField_TwoDayReplacement_ExceptionShifts" runat="server" />
                                                                                                    <asp:HiddenField ID="hfPersonnelCount_TwoDayReplacement_ExceptionShifts" runat="server" />
                                                                                                    <asp:HiddenField ID="hfPersonnelPageCount_TwoDayReplacement_ExceptionShifts" runat="server" />
                                                                                                </Content>
                                                                                                <ClientEvents>
                                                                                                    <BeforeCallback EventHandler="CallBack_cmbPersonnel_TwoDayReplacement_ExceptionShifts_onBeforeCallback" />
                                                                                                    <CallbackComplete EventHandler="CallBack_cmbPersonnel_TwoDayReplacement_ExceptionShifts_onCallbackComplete" />
                                                                                                    <CallbackError EventHandler="CallBack_cmbPersonnel_TwoDayReplacement_ExceptionShifts_onCallbackError" />
                                                                                                </ClientEvents>
                                                                                            </ComponentArt:CallBack>
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 90%">
                                                                                            <input id="txtPersonnelSearch_TwoDayReplacement_ExceptionShifts" runat="server" class="TextBoxes"
                                                                                                style="width: 95%" type="text" />
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            <ComponentArt:ToolBar ID="TlbSearchPersonnel_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                                <Items>
                                                                                                    <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_TwoDayReplacement_ExceptionShifts"
                                                                                                        runat="server" ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_TwoDayReplacement_ExceptionShifts_onClick();"
                                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_TwoDayReplacement_ExceptionShifts"
                                                                                                        TextImageSpacing="5" />
                                                                                                </Items>
                                                                                            </ComponentArt:ToolBar>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 90%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            <ComponentArt:ToolBar ID="TlbAdvancedSearch_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                                <Items>
                                                                                                    <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_TwoDayReplacement_ExceptionShifts"
                                                                                                        runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_TwoDayReplacement_ExceptionShifts_onClick();"
                                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_TwoDayReplacement_ExceptionShifts"
                                                                                                        TextImageSpacing="5" />
                                                                                                </Items>
                                                                                            </ComponentArt:ToolBar>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="Tr1" runat="server">
                                            <td id="Td5" runat="server">
                                                <asp:Label ID="lblDates_TwoDayReplacementDialog_ExceptionShifts" runat="server" CssClass="WhiteLabel"
                                                    meta:resourcekey="lblDates_TwoDayReplacementDialog_ExceptionShifts" Text="تاریخ های"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="Tr2" runat="server">
                                            <td id="Td6" runat="server">
                                                <table style="width: 100%;" class="BoxStyle">
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%;">
                                                                <tr id="Tr3" runat="server">
                                                                    <td id="Container_FromDateCalendars_TwoDayReplacement_ExceptionShifts">
                                                                        <table runat="server" id="Container_pdpFromDate_TwoDayReplacement_ExceptionShifts"
                                                                            visible="false" style="width: 100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <pcal:PersianDatePickup ID="pdpFromDate_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                                        CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table runat="server" id="Container_gdpFromDate_TwoDayReplacement_ExceptionShifts"
                                                                            visible="false" style="width: 100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <table id="Container_gCalFromDate_TwoDayReplacement_ExceptionShifts" border="0" cellpadding="0"
                                                                                        cellspacing="0">
                                                                                        <tr>
                                                                                            <td onmouseup="btn_gdpFromDate_TwoDayReplacement_ExceptionShifts_OnMouseUp(event)">
                                                                                                <ComponentArt:Calendar ID="gdpFromDate_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                                                    ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                                    PickerFormat="Custom" SelectedDate="2008-1-1" Width="115px">
                                                                                                    <ClientEvents>
                                                                                                        <SelectionChanged EventHandler="gdpFromDate_TwoDayReplacement_ExceptionShifts_OnDateChange" />
                                                                                                    </ClientEvents>
                                                                                                </ComponentArt:Calendar>
                                                                                            </td>
                                                                                            <td style="font-size: 10px;">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td>
                                                                                                <img id="btn_gdpFromDate_TwoDayReplacement_ExceptionShifts" alt="" class="calendar_button"
                                                                                                    onclick="btn_gdpFromDate_TwoDayReplacement_ExceptionShifts_OnClick(event)" onmouseup="btn_gdpFromDate_TwoDayReplacement_ExceptionShifts_OnMouseUp(event)"
                                                                                                    src="Images/Calendar/btn_calendar.gif" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <ComponentArt:Calendar ID="gCalFromDate_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                                        AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                                        CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                                        DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                                        ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                                        NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                                        PopUpExpandControlId="btn_gdpFromDate_TwoDayReplacement_ExceptionShifts" PrevImageUrl="cal_prevMonth.gif"
                                                                                        SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                                                        SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                                        <ClientEvents>
                                                                                            <SelectionChanged EventHandler="gCalFromDate_TwoDayReplacement_ExceptionShifts_OnChange" />
                                                                                            <Load EventHandler="gCalFromDate_TwoDayReplacement_ExceptionShifts_onLoad" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:Calendar>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr4" runat="server">
                                                                    <td>
                                                                        <ComponentArt:ToolBar ID="TlbFirstDateView_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                            UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemView_TlbFirstDateView_TwoDayReplacement_ExceptionShifts"
                                                                                    runat="server" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                                                                    ClientSideCommand="tlbItemView_TlbFirstDateView_TwoDayReplacement_ExceptionShifts_onClick();"
                                                                                    ImageUrl="eyeglasses.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemView_TlbFirstDateView_TwoDayReplacement_ExceptionShifts"
                                                                                    TextImageSpacing="5" />
                                                                            </Items>
                                                                        </ComponentArt:ToolBar>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr5" runat="server">
                                                                    <td>
                                                                        <input id="txtFirstDateShiftName_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                            class="TextBoxes" readonly="readonly" style="width: 60%;" type="text" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="color: White; width: 5%">
                                                            &lt;&lt;&gt;&gt;
                                                        </td>
                                                        <td>
                                                            <table style="width: 100%;">
                                                                <tr id="Tr6" runat="server">
                                                                    <td id="Container_ToDateCalendars_TwoDayReplacement_ExceptionShifts">
                                                                        <table runat="server" id="Container_pdpToDate_TwoDayReplacement_ExceptionShifts"
                                                                            visible="false" style="width: 100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <pcal:PersianDatePickup ID="pdpToDate_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                                        CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table runat="server" id="Container_gdpToDate_TwoDayReplacement_ExceptionShifts"
                                                                            visible="false" style="width: 100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <table id="Container_gCalToDate_TwoDayReplacement_ExceptionShifts" border="0" cellpadding="0"
                                                                                        cellspacing="0">
                                                                                        <tr>
                                                                                            <td onmouseup="btn_gdpToDate_TwoDayReplacement_ExceptionShifts_OnMouseUp(event)">
                                                                                                <ComponentArt:Calendar ID="gdpToDate_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                                                    ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                                    PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                                                    <ClientEvents>
                                                                                                        <SelectionChanged EventHandler="gdpToDate_TwoDayReplacement_ExceptionShifts_OnDateChange" />
                                                                                                    </ClientEvents>
                                                                                                </ComponentArt:Calendar>
                                                                                            </td>
                                                                                            <td style="font-size: 10px;">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td>
                                                                                                <img id="btn_gdpToDate_TwoDayReplacement_ExceptionShifts" alt="" class="calendar_button"
                                                                                                    onclick="btn_gdpToDate_TwoDayReplacement_ExceptionShifts_OnClick(event)" onmouseup="btn_gdpToDate_TwoDayReplacement_ExceptionShifts_OnMouseUp(event)"
                                                                                                    src="Images/Calendar/btn_calendar.gif" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <ComponentArt:Calendar ID="gCalToDate_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                                        AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                                        CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                                        DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                                        ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                                        NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                                        PopUpExpandControlId="btn_gdpToDate_TwoDayReplacement_ExceptionShifts" PrevImageUrl="cal_prevMonth.gif"
                                                                                        SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                                                        SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                                                        <ClientEvents>
                                                                                            <SelectionChanged EventHandler="gCalToDate_TwoDayReplacement_ExceptionShifts_OnChange" />
                                                                                            <Load EventHandler="gCalToDate_TwoDayReplacement_ExceptionShifts_onLoad" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:Calendar>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr7" runat="server">
                                                                    <td>
                                                                        <ComponentArt:ToolBar ID="TlbSecondDateView_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                            UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemView_TlbSecondDateView_TwoDayReplacement_ExceptionShifts"
                                                                                    runat="server" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                                                                    ClientSideCommand="tlbItemView_TlbSecondDateView_TwoDayReplacement_ExceptionShifts_onClick();"
                                                                                    ImageUrl="eyeglasses.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemView_TlbSecondDateView_TwoDayReplacement_ExceptionShifts"
                                                                                    TextImageSpacing="5" />
                                                                            </Items>
                                                                        </ComponentArt:ToolBar>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr8" runat="server">
                                                                    <td>
                                                                        <input id="txtSecondDateShiftName_TwoDayReplacement_ExceptionShifts" runat="server"
                                                                            class="TextBoxes" readonly="readonly" style="width: 60%;" type="text" />
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
                    </ComponentArt:PageView>
                    <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvTwoPersonnelReplacement_TabStripShiftDetails"
                        Visible="true">
                        <table class="BoxStyle" style="width: 100%; font-family: Arial; font-size: small">
                            <tr>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbTwoPersonnelReplacement" runat="server" CssClass="toolbar"
                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                        UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemEndorsement_TlbTwoPersonnelReplacement" runat="server"
                                                ClientSideCommand="tlbItemEndorsement_TlbTwoPersonnelReplacement_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEndorsement_TlbTwoPersonnelReplacement"
                                                TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbTwoPersonnelReplacement"
                                                runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbTwoPersonnelReplacement_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbTwoPersonnelReplacement"
                                                TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbTwoPersonnelReplacement" runat="server"
                                                ClientSideCommand="tlbItemExit_TlbTwoPersonnelReplacement_onClick();" DropDownImageHeight="16px"
                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                                ItemType="Command" meta:resourcekey="tlbItemExit_TlbTwoPersonnelReplacement"
                                                TextImageSpacing="5" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="BoxStyle" style="width: 100%;">
                                        <tr>
                                            <td style="width: 15%" valign="top">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 5%">
                                                            <input id="rdbWorkGroup_TwoPersonnelReplacement_ExceptionShifts" name="TwoPersonnelReplacement_ExceptionShifts"
                                                                onclick="rdbWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onClick();" type="radio"
                                                                value="V1" />
                                                        </td>
                                                        <td id="Td8" runat="server" style="width: 95%">
                                                            <asp:Label ID="lblWorkGroupchb_TwoPersonnelReplacement_ExceptionShifts" runat="server"
                                                                CssClass="WhiteLabel" meta:resourcekey="lblWorkGroupchb_TwoPersonnelReplacement_ExceptionShifts"
                                                                Text="گروه کاری :"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 50%">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts"
                                                                            runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts"
                                                                            Text="گروه کاری 1:"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <ComponentArt:CallBack ID="CallBack_cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts"
                                                                            runat="server" OnCallback="CallBack_cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onCallBack">
                                                                            <Content>
                                                                                <ComponentArt:ComboBox ID="cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts"
                                                                                    runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                    DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                    TextBoxCssClass="comboTextBox" Width="98%" Enabled="false" DataTextField="Name"
                                                                                    DataValueField="ID" TextBoxEnabled="true">
                                                                                    <ClientEvents>
                                                                                        <Expand EventHandler="cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onExpand" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:ComboBox>
                                                                                <asp:HiddenField ID="ErrorHiddenField_FirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts"
                                                                                    runat="server" />
                                                                            </Content>
                                                                            <ClientEvents>
                                                                                <BeforeCallback EventHandler="CallBack_cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onBeforeCallback" />
                                                                                <CallbackComplete EventHandler="CallBack_cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onCallbackComplete" />
                                                                                <CallbackError EventHandler="CallBack_cmbFirstPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onCallbackError" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:CallBack>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts"
                                                                            runat="server" CssClass="WhiteLabel" meta:resourcekey="lblSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts"
                                                                            Text="گروه کاری 2:"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <ComponentArt:CallBack ID="CallBack_cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts"
                                                                            runat="server" OnCallback="CallBack_cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onCallBack">
                                                                            <Content>
                                                                                <ComponentArt:ComboBox ID="cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts"
                                                                                    runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                    DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                    TextBoxCssClass="comboTextBox" Width="98%" Enabled="false" DataTextField="Name"
                                                                                    DataValueField="ID" TextBoxEnabled="true">
                                                                                    <ClientEvents>
                                                                                        <Expand EventHandler="cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onExpand" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:ComboBox>
                                                                                <asp:HiddenField ID="ErrorHiddenField_SecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts"
                                                                                    runat="server" />
                                                                            </Content>
                                                                            <ClientEvents>
                                                                                <BeforeCallback EventHandler="CallBack_cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onBeforeCallback" />
                                                                                <CallbackComplete EventHandler="CallBack_cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onCallbackComplete" />
                                                                                <CallbackError EventHandler="CallBack_cmbSecondPersonnelWorkGroup_TwoPersonnelReplacement_ExceptionShifts_onCallbackError" />
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
                            <tr>
                                <td>
                                    <table class="BoxStyle" style="width: 100%;">
                                        <tr>
                                            <td id="Container_rdbPersonnel_TwoPersonnelReplacement_ExceptionShifts" style="width: 15%"
                                                valign="top">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 5%">
                                                            <input id="rdbPersonnel_TwoPersonnelReplacement_ExceptionShifts" checked="checked"
                                                                name="TwoPersonnelReplacement_ExceptionShifts" onclick="rdbPersonnel_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                type="radio" value="V1" />
                                                        </td>
                                                        <td id="Td9" runat="server" style="width: 95%">
                                                            <asp:Label ID="lblPersonnelchb_TwoPersonnelReplacement_ExceptionShifts" runat="server"
                                                                CssClass="WhiteLabel" meta:resourcekey="lblPersonnelchb_TwoPersonnelReplacement_ExceptionShifts"
                                                                Text="پرسنل :"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 50%">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 50%">
                                                                        <div id="Div1" runat="server" class="DropDownHeader" meta:resourcekey="AlignObj"
                                                                            style="width: 100%">
                                                                            <img id="imgbox_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts" runat="server"
                                                                                alt="" onclick="imgbox_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                src="Images/Ghadir/arrowDown.jpg" />
                                                                            <span id="header_SearchByPersonnelBox1_TwoPersonnelReplacement_ExceptionShifts">Personnel1
                                                                                Select</span>
                                                                        </div>
                                                                    </td>
                                                                    <td align="center" style="color: White" id="tdSelectedPersonnel_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div id="box_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts" class="dhtmlgoodies_contentBox"
                                                                style="width: 36.5%;">
                                                                <div id="subbox_SearchByPersonnel1_TwoPersonnelReplacement_ExceptionShifts" class="dhtmlgoodies_content">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <table style="width: 100%;">
                                                                                    <tr>
                                                                                        <td style="width: 90%">
                                                                                            <table style="width: 100%;">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblPersonnel1_TwoPersonnelReplacement_ExceptionShifts" runat="server"
                                                                                                            CssClass="WhiteLabel" meta:resourcekey="lblPersonnel1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                            Text=": پرسنل"></asp:Label>
                                                                                                    </td>
                                                                                                    <td runat="server" meta:resourcekey="InverseAlignObj">
                                                                                                        <ComponentArt:ToolBar ID="TlbPaging1_PersonnelSearch1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                                            Style="direction: ltr" UseFadeEffect="false">
                                                                                                            <Items>
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging1_PersonnelSearch1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging1_PersonnelSearch1_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging1_PersonnelSearch1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" />
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemFirst_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                    ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" ImageUrl="first.png" />
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemBefore_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                    ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" ImageUrl="Before.png" />
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemNext_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                    ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" ImageUrl="Next.png" />
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemLast_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                    ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging1_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" ImageUrl="last.png" />
                                                                                                            </Items>
                                                                                                        </ComponentArt:ToolBar>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 90%">
                                                                                            <ComponentArt:CallBack ID="CallBack_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                runat="server" Height="26" OnCallback="CallBack_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onCallBack">
                                                                                                <Content>
                                                                                                    <ComponentArt:ComboBox ID="cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        runat="server" AutoComplete="true" AutoHighlight="false" CssClass="comboBox"
                                                                                                        DataFields="BarCode" DataTextField="Name" DropDownCssClass="comboDropDown" DropDownHeight="220"
                                                                                                        DropDownPageSize="7" DropDownWidth="400" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                                        ItemClientTemplateId="ItemTemplate_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client" TextBoxEnabled="true"
                                                                                                        SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                                                        <ClientTemplates>
                                                                                                            <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts">
                                                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                    <tr class="dataRow">
                                                                                                                        <td class="dataCell" style="width: 40%">
                                                                                                                            ## DataItem.getProperty('Text') ##
                                                                                                                        </td>
                                                                                                                        <td class="dataCell" style="width: 30%">
                                                                                                                            ## DataItem.getProperty('BarCode') ##
                                                                                                                        </td>
                                                                                                                        <td class="dataCell" style="width: 30%">
                                                                                                                            ## DataItem.getProperty('CardNum') ##
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </ComponentArt:ClientTemplate>
                                                                                                        </ClientTemplates>
                                                                                                        <DropDownHeader>
                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="400">
                                                                                                                <tr class="headingRow">
                                                                                                                    <td id="clmnName_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts" class="headingCell"
                                                                                                                        style="width: 40%; text-align: center">
                                                                                                                        Name And Family
                                                                                                                    </td>
                                                                                                                    <td id="clmnBarCode_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts" class="headingCell"
                                                                                                                        style="width: 30%; text-align: center">
                                                                                                                        BarCode
                                                                                                                    </td>
                                                                                                                    <td id="clmnCardNum_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts" class="headingCell"
                                                                                                                        style="width: 30%; text-align: center">
                                                                                                                        CardNum
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </DropDownHeader>
                                                                                                        <ClientEvents>
                                                                                                            <Expand EventHandler="cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onExpand" />
                                                                                                            <Change EventHandler="cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onChange" />
                                                                                                        </ClientEvents>
                                                                                                    </ComponentArt:ComboBox>
                                                                                                    <asp:HiddenField ID="ErrorHiddenField_Personnel1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        runat="server" />
                                                                                                    <asp:HiddenField ID="hfPersonnelCount_Personnel1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        runat="server" />
                                                                                                    <asp:HiddenField ID="hfPersonnelPageCount_Personnel1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        runat="server" />
                                                                                                </Content>
                                                                                                <ClientEvents>
                                                                                                    <BeforeCallback EventHandler="CallBack_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onBeforeCallback" />
                                                                                                    <CallbackComplete EventHandler="CallBack_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onCallbackComplete" />
                                                                                                    <CallbackError EventHandler="CallBack_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onCallbackError" />
                                                                                                </ClientEvents>
                                                                                            </ComponentArt:CallBack>
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 90%">
                                                                                            <input id="txtPersonnelSearch1_TwoPersonnelReplacement_ExceptionShifts" runat="server"
                                                                                                class="TextBoxes" style="width: 95%" type="text" />
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            <ComponentArt:ToolBar ID="TlbSearchPersonnel1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                                <Items>
                                                                                                    <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        runat="server" ClientSideCommand="tlbItemSearch_TlbSearchPersonnel1_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        TextImageSpacing="5" />
                                                                                                </Items>
                                                                                            </ComponentArt:ToolBar>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 90%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            <ComponentArt:ToolBar ID="TlbAdvancedSearch1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                                <Items>
                                                                                                    <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch1_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch1_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        TextImageSpacing="5" />
                                                                                                </Items>
                                                                                            </ComponentArt:ToolBar>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 50%">
                                                                        <div id="Div3" runat="server" class="DropDownHeader" meta:resourcekey="AlignObj"
                                                                            style="width: 100%">
                                                                            <img id="imgbox_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts" runat="server"
                                                                                alt="" onclick="imgbox_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                src="Images/Ghadir/arrowDown.jpg" />
                                                                            <span id="header_SearchByPersonnelBox2_TwoPersonnelReplacement_ExceptionShifts">Personnel2
                                                                                Select</span>
                                                                        </div>
                                                                    </td>
                                                                    <td align="center" style="color: White" id="tdSelectedPersonnel_SearchByPersonnelBox2_TwoPersonnelReplacement_ExceptionShifts">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div id="box_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts" class="dhtmlgoodies_contentBox"
                                                                style="width: 36.5%;">
                                                                <div id="subbox_SearchByPersonnel2_TwoPersonnelReplacement_ExceptionShifts" class="dhtmlgoodies_content">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <table style="width: 100%;">
                                                                                    <tr>
                                                                                        <td style="width: 90%">
                                                                                            <table style="width: 100%;">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblPersonnel2_TwoPersonnelReplacement_ExceptionShifts" runat="server"
                                                                                                            CssClass="WhiteLabel" meta:resourcekey="lblPersonnel2_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                            Text=": پرسنل"></asp:Label>
                                                                                                    </td>
                                                                                                    <td id="Td10" runat="server" meta:resourcekey="InverseAlignObj">
                                                                                                        <ComponentArt:ToolBar ID="TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                                            UseFadeEffect="false" Style="direction: ltr">
                                                                                                            <Items>
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" />
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemFirst_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                    ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" ImageUrl="first.png" />
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemBefore_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                    ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" ImageUrl="Before.png" />
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemNext_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                    ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" ImageUrl="Next.png" />
                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    runat="server" ClientSideCommand="tlbItemLast_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                    ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging2_PersonnelSearch_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                                    TextImageSpacing="5" ImageUrl="last.png" />
                                                                                                            </Items>
                                                                                                        </ComponentArt:ToolBar>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 90%">
                                                                                            <ComponentArt:CallBack ID="CallBack_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                runat="server" Height="26" OnCallback="CallBack_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onCallBack">
                                                                                                <Content>
                                                                                                    <ComponentArt:ComboBox ID="cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        runat="server" AutoComplete="true" AutoHighlight="false" CssClass="comboBox"
                                                                                                        DataFields="BarCode" DataTextField="Name" DropDownCssClass="comboDropDown" DropDownHeight="220"
                                                                                                        DropDownPageSize="7" DropDownWidth="400" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                                        ItemClientTemplateId="ItemTemplate_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client" TextBoxEnabled="true"
                                                                                                        SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                                                        <ClientTemplates>
                                                                                                            <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts">
                                                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                    <tr class="dataRow">
                                                                                                                        <td class="dataCell" style="width: 40%">
                                                                                                                            ## DataItem.getProperty('Text') ##
                                                                                                                        </td>
                                                                                                                        <td class="dataCell" style="width: 30%">
                                                                                                                            ## DataItem.getProperty('BarCode') ##
                                                                                                                        </td>
                                                                                                                        <td class="dataCell" style="width: 30%">
                                                                                                                            ## DataItem.getProperty('CardNum') ##
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </ComponentArt:ClientTemplate>
                                                                                                        </ClientTemplates>
                                                                                                        <DropDownHeader>
                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                <tr class="headingRow">
                                                                                                                    <td id="clmnName_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts" class="headingCell"
                                                                                                                        style="width: 35%; text-align: center">
                                                                                                                        Name And Family
                                                                                                                    </td>
                                                                                                                    <td id="clmnBarCode_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts" class="headingCell"
                                                                                                                        style="width: 30%; text-align: center">
                                                                                                                        BarCode
                                                                                                                    </td>
                                                                                                                    <td id="clmnCardNum_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts" class="headingCell"
                                                                                                                        style="width: 30%; text-align: center">
                                                                                                                        CardNum
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </DropDownHeader>
                                                                                                        <ClientEvents>
                                                                                                            <Expand EventHandler="cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onExpand" />
                                                                                                            <Change EventHandler="cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onChange" />
                                                                                                        </ClientEvents>
                                                                                                    </ComponentArt:ComboBox>
                                                                                                    <asp:HiddenField ID="ErrorHiddenField_Personnel2_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        runat="server" />
                                                                                                    <asp:HiddenField ID="hfPersonnelCount_Personnel2_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        runat="server" />
                                                                                                    <asp:HiddenField ID="hfPersonnelPageCount_Personnel2_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        runat="server" />
                                                                                                </Content>
                                                                                                <ClientEvents>
                                                                                                    <BeforeCallback EventHandler="CallBack_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onBeforeCallback" />
                                                                                                    <CallbackComplete EventHandler="CallBack_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onCallbackComplete" />
                                                                                                    <CallbackError EventHandler="CallBack_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onCallbackError" />
                                                                                                </ClientEvents>
                                                                                            </ComponentArt:CallBack>
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 90%">
                                                                                            <input id="txtPersonnelSearch2_TwoPersonnelReplacement_ExceptionShifts" runat="server"
                                                                                                class="TextBoxes" style="width: 95%" type="text" />
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            <ComponentArt:ToolBar ID="TlbSearchPersonnel2_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                                <Items>
                                                                                                    <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel2_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        runat="server" ClientSideCommand="tlbItemSearch_TlbSearchPersonnel2_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel2_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        TextImageSpacing="5" />
                                                                                                </Items>
                                                                                            </ComponentArt:ToolBar>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 90%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            <ComponentArt:ToolBar ID="TlbAdvancedSearch2_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                                <Items>
                                                                                                    <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch2_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch2_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch2_TwoPersonnelReplacement_ExceptionShifts"
                                                                                                        TextImageSpacing="5" />
                                                                                                </Items>
                                                                                            </ComponentArt:ToolBar>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td runat="server">
                                    <asp:Label ID="lblDates_TwoPersonnelReplacementDialog_ExceptionShifts" runat="server"
                                        CssClass="WhiteLabel" meta:resourcekey="lblDates_TwoPersonnelReplacementDialog_ExceptionShifts"
                                        Text="تاریخ های"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="BoxStyle" style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr id="Tr15" runat="server">
                                                        <td id="Container_TwoPersonnelReplacementCalendars1_ExceptionShifts">
                                                            <table runat="server" id="Container_pdpTwoPersonnelReplacement1_ExceptionShifts"
                                                                visible="false" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <pcal:PersianDatePickup ID="pdpTwoPersonnelReplacement1_ExceptionShifts" runat="server"
                                                                            CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table runat="server" id="Container_gdpTwoPersonnelReplacement1_ExceptionShifts"
                                                                visible="false" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <table id="Container_gCalTwoPersonnelReplacement1_ExceptionShifts" border="0" cellpadding="0"
                                                                            cellspacing="0">
                                                                            <tr>
                                                                                <td onmouseup="btn_gdpTwoPersonnelReplacement1_ExceptionShifts_OnMouseUp(event)">
                                                                                    <ComponentArt:Calendar ID="gdpTwoPersonnelReplacement1_ExceptionShifts" runat="server"
                                                                                        ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                        PickerFormat="Custom" SelectedDate="2008-1-1" Width="115px">
                                                                                        <ClientEvents>
                                                                                            <SelectionChanged EventHandler="gdpTwoPersonnelReplacement1_ExceptionShifts_OnDateChange" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:Calendar>
                                                                                </td>
                                                                                <td style="font-size: 10px;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <img id="btn_gdpTwoPersonnelReplacement1_ExceptionShifts" alt="" class="calendar_button"
                                                                                        onclick="btn_gdpTwoPersonnelReplacement1_ExceptionShifts_OnClick(event)" onmouseup="btn_gdpTwoPersonnelReplacement1_ExceptionShifts_OnMouseUp(event)"
                                                                                        src="Images/Calendar/btn_calendar.gif" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <ComponentArt:Calendar ID="gCalTwoPersonnelReplacement1_ExceptionShifts" runat="server"
                                                                            AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                            CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                            DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                            ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                            NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                            PopUpExpandControlId="btn_gdpTwoPersonnelReplacement1_ExceptionShifts" PrevImageUrl="cal_prevMonth.gif"
                                                                            SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                                            SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                            <ClientEvents>
                                                                                <SelectionChanged EventHandler="gCalTwoPersonnelReplacement1_ExceptionShifts_OnChange" />
                                                                                <Load EventHandler="gCalTwoPersonnelReplacement1_ExceptionShifts_onLoad" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:Calendar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="txtFirstDateShiftName_TwoPersonnelReplacement_ExceptionShifts" runat="server"
                                                                class="TextBoxes" readonly="readonly" style="width: 98%;" type="text" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server">
                                                            <ComponentArt:ToolBar ID="TlbFirstDateView_TwoPersonnelReplacement_ExceptionShifts"
                                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemView_TlbFirstDateView_TwoPersonnelReplacement_ExceptionShifts"
                                                                        runat="server" ClientSideCommand="tlbItemView_TlbFirstDateView_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemView_TlbFirstDateView_TwoPersonnelReplacement_ExceptionShifts"
                                                                        TextImageSpacing="5" />
                                                                </Items>
                                                            </ComponentArt:ToolBar>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="center" style="color: White; width: 5%">
                                            </td>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr id="Tr13" runat="server">
                                                        <td id="Container_TwoPersonnelReplacementCalendars2_ExceptionShifts">
                                                            <table runat="server" id="Container_pdpTwoPersonnelReplacement2_ExceptionShifts"
                                                                visible="false" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <pcal:PersianDatePickup ID="pdpTwoPersonnelReplacement2_ExceptionShifts" runat="server"
                                                                            CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table runat="server" id="Container_gdpTwoPersonnelReplacement2_ExceptionShifts"
                                                                visible="false" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <table id="Container_gCalTwoPersonnelReplacement2_ExceptionShifts" border="0" cellpadding="0"
                                                                            cellspacing="0">
                                                                            <tr>
                                                                                <td onmouseup="btn_gdpTwoPersonnelReplacement2_ExceptionShifts_OnMouseUp(event)">
                                                                                    <ComponentArt:Calendar ID="gdpTwoPersonnelReplacement2_ExceptionShifts" runat="server"
                                                                                        ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                        PickerFormat="Custom" SelectedDate="2008-1-1" Width="115px">
                                                                                        <ClientEvents>
                                                                                            <SelectionChanged EventHandler="gdpTwoPersonnelReplacement2_ExceptionShifts_OnDateChange" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:Calendar>
                                                                                </td>
                                                                                <td style="font-size: 10px;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <img id="Img1" alt="" class="calendar_button" onclick="btn_gdpTwoPersonnelReplacement2_ExceptionShifts_OnClick(event)"
                                                                                        onmouseup="btn_gdpTwoPersonnelReplacement2_ExceptionShifts_OnMouseUp(event)"
                                                                                        src="Images/Calendar/btn_calendar.gif" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <ComponentArt:Calendar ID="gCalTwoPersonnelReplacement2_ExceptionShifts" runat="server"
                                                                            AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                            CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                            DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                            ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                            NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                            PopUpExpandControlId="btn_gdpTwoPersonnelReplacement2_ExceptionShifts" PrevImageUrl="cal_prevMonth.gif"
                                                                            SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                                            SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                            <ClientEvents>
                                                                                <SelectionChanged EventHandler="gCalTwoPersonnelReplacement2_ExceptionShifts_OnChange" />
                                                                                <Load EventHandler="gCalTwoPersonnelReplacement2_ExceptionShifts_onLoad" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:Calendar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="txtSecondDateShiftName_TwoPersonnelReplacement_ExceptionShifts" runat="server"
                                                                class="TextBoxes" readonly="readonly" style="width: 98%;" type="text" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server">
                                                            <ComponentArt:ToolBar ID="TlbSecondDateView_TwoPersonnelReplacement_ExceptionShifts"
                                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemView_TlbSecondDateView_TwoPersonnelReplacement_ExceptionShifts"
                                                                        runat="server" ClientSideCommand="tlbItemView_TlbSecondDateView_TwoPersonnelReplacement_ExceptionShifts_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemView_TlbSecondDateView_TwoPersonnelReplacement_ExceptionShifts"
                                                                        TextImageSpacing="5" />
                                                                </Items>
                                                            </ComponentArt:ToolBar>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ComponentArt:PageView>
                </ComponentArt:MultiPage>
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
                        <img id="Img2" runat="server" alt="" src="~/Images/Dialog/Waiting.gif" />
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogWaiting_onShow" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <asp:HiddenField runat="server" ID="hfTitle_DialogExceptionShifts" meta:resourcekey="hfTitle_DialogExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfAdd_ExceptionShifts" meta:resourcekey="hfAdd_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfEdit_ExceptionShifts" meta:resourcekey="hfEdit_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfheader_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts"
        meta:resourcekey="hfheader_SearchByPersonnelBox_TwoDayReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfheader_SearchByPersonnelBox1_TwoPersonnelReplacement_ExceptionShifts"
        meta:resourcekey="hfheader_SearchByPersonnelBox1_TwoPersonnelReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfheader_SearchByPersonnelBox2_TwoPersonnelReplacement_ExceptionShifts"
        meta:resourcekey="hfheader_SearchByPersonnelBox2_TwoPersonnelReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfCurrentDate_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_ExceptionShifts" meta:resourcekey="hfCloseMessage_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfErrorType_ExceptionShifts" meta:resourcekey="hfErrorType_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfConnectionError_ExceptionShifts" meta:resourcekey="hfConnectionError_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_TwoDayReplacement_ExceptionShifts"
        meta:resourcekey="hfclmnName_cmbPersonnel_TwoDayReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_TwoDayReplacement_ExceptionShifts"
        meta:resourcekey="hfclmnBarCode_cmbPersonnel_TwoDayReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_TwoDayReplacement_ExceptionShifts"
        meta:resourcekey="hfclmnCardNum_cmbPersonnel_TwoDayReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_TwoDayReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts"
        meta:resourcekey="hfclmnName_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts"
        meta:resourcekey="hfclmnBarCode_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts"
        meta:resourcekey="hfclmnCardNum_cmbPersonnel1_TwoPersonnelReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_Personnel1_TwoPersonnelReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts"
        meta:resourcekey="hfclmnName_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts"
        meta:resourcekey="hfclmnBarCode_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts"
        meta:resourcekey="hfclmnCardNum_cmbPersonnel2_TwoPersonnelReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_Personnel2_TwoPersonnelReplacement_ExceptionShifts" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_ExceptionShifts" meta:resourcekey="hfcmbAlarm_ExceptionShifts" />
    </form>
</body>
</html>
