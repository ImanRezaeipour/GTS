<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CultureConditionalDateTime.aspx.cs" Inherits="CultureConditionalDateTime" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="CultureConditionalDateTimeForm" runat="server" meta:resourcekey="CultureConditionalDateTimeForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <div style="height: 290px;" class="BoxStyle">
            <table style="width: 70%; font-family: Arial; font-size: small">
                <tr>
                    <td style="width: 50%">
                        <asp:Label ID="lblFromDate_CultureConditionalDateTime" runat="server" Text=": از تاریخ" CssClass="WhiteLabel"
                            meta:resourcekey="lblFromDate_CultureConditionalDateTime"></asp:Label>
                    </td>
                    <td id="Container_fromDateCalendars_CultureConditionalDateTime">
                        <table runat="server" id="Container_pdpfromDate_CultureConditionalDateTime" visible="false" style="width: 100%">
                            <tr>
                                <td>
                                    <pcal:PersianDatePickup ID="pdpfromDate_CultureConditionalDateTime" runat="server" CssClass="PersianDatePicker"
                                        ReadOnly="true"></pcal:PersianDatePickup>
                                </td>
                            </tr>
                        </table>
                        <table runat="server" id="Container_gdpfromDate_CultureConditionalDateTime" visible="false" style="width: 100%">
                            <tr>
                                <td>
                                    <table id="Container_gCalfromDate_CultureConditionalDateTime" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td onmouseup="btn_gdpfromDate_CultureConditionalDateTime_OnMouseUp(event)">
                                                <ComponentArt:Calendar ID="gdpfromDate_CultureConditionalDateTime" runat="server" ControlType="Picker"
                                                    PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                    SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                    <ClientEvents>
                                                        <SelectionChanged EventHandler="gdpfromDate_CultureConditionalDateTime_OnDateChange" />
                                                    </ClientEvents>
                                                </ComponentArt:Calendar>
                                            </td>
                                            <td style="font-size: 10px;">&nbsp;
                                            </td>
                                            <td>
                                                <img id="btn_gdpfromDate_CultureConditionalDateTime" alt="" class="calendar_button" onclick="btn_gdpfromDate_CultureConditionalDateTime_OnClick(event)"
                                                    onmouseup="btn_gdpfromDate_CultureConditionalDateTime_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                            </td>
                                        </tr>
                                    </table>
                                    <ComponentArt:Calendar ID="gCalfromDate_CultureConditionalDateTime" runat="server" AllowMonthSelection="false"
                                        AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                        CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                        DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                        MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                        OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpfromDate_CultureConditionalDateTime"
                                        PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                        SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                        <ClientEvents>
                                            <SelectionChanged EventHandler="gCalfromDate_CultureConditionalDateTime_OnChange" />
                                            <Load EventHandler="gCalfromDate_CultureConditionalDateTime_onLoad" />
                                        </ClientEvents>
                                    </ComponentArt:Calendar>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblToDate_CultureConditionalDateTime" runat="server" Text=": تا تاریخ" CssClass="WhiteLabel"
                            meta:resourcekey="lblToDate_CultureConditionalDateTime"></asp:Label>
                    </td>
                    <td id="Container_toDateCalendars_CultureConditionalDateTime">
                        <table runat="server" id="Container_pdptoDate_CultureConditionalDateTime" visible="false" style="width: 100%">
                            <tr>
                                <td>
                                    <pcal:PersianDatePickup ID="pdptoDate_CultureConditionalDateTime" runat="server" CssClass="PersianDatePicker"
                                        ReadOnly="true"></pcal:PersianDatePickup>
                                </td>
                            </tr>
                        </table>
                        <table runat="server" id="Container_gdptoDate_CultureConditionalDateTime" visible="false" style="width: 100%">
                            <tr>
                                <td>
                                    <table id="Container_gCaltoDate_CultureConditionalDateTime" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td onmouseup="btn_gdptoDate_CultureConditionalDateTime_OnMouseUp(event)">
                                                <ComponentArt:Calendar ID="gdptoDate_CultureConditionalDateTime" runat="server" ControlType="Picker"
                                                    PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                    SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                    <ClientEvents>
                                                        <SelectionChanged EventHandler="gdptoDate_CultureConditionalDateTime_OnDateChange" />
                                                    </ClientEvents>
                                                </ComponentArt:Calendar>
                                            </td>
                                            <td style="font-size: 10px;">&nbsp;
                                            </td>
                                            <td>
                                                <img id="btn_gdptoDate_CultureConditionalDateTime" alt="" class="calendar_button" onclick="btn_gdptoDate_CultureConditionalDateTime_OnClick(event)"
                                                    onmouseup="btn_gdptoDate_CultureConditionalDateTime_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                            </td>
                                        </tr>
                                    </table>
                                    <ComponentArt:Calendar ID="gCaltoDate_CultureConditionalDateTime" runat="server" AllowMonthSelection="false"
                                        AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                        CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                        DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                        MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                        OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdptoDate_CultureConditionalDateTime"
                                        PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                        SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                        <ClientEvents>
                                            <SelectionChanged EventHandler="gCaltoDate_CultureConditionalDateTime_OnChange" />
                                            <Load EventHandler="gCaltoDate_CultureConditionalDateTime_onLoad" />
                                        </ClientEvents>
                                    </ComponentArt:Calendar>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTime_CultureConditionalDateTime" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTime_CultureConditionalDateTime"
                                        Text=": زمان"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 70%">
                                                <table dir="ltr" style="width: 100%">
                                                    <tr>
                                                        <td align="center" style="width: 48%">
                                                            <input id="TimeSelector_Hour_CultureConditionalDateTime_txtHour" onchange="TimeSelector_Hour_CultureConditionalDateTime_onChange('txtHour')"
                                                                style="width: 70%; text-align: center" type="text" />
                                                        </td>
                                                        <td align="center">:
                                                        </td>
                                                        <td align="center" style="width: 48%">
                                                            <input id="TimeSelector_Hour_CultureConditionalDateTime_txtMinute" onchange="TimeSelector_Hour_CultureConditionalDateTime_onChange('txtMinute')"
                                                                style="width: 70%; text-align: center" type="text" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <ComponentArt:ToolBar ID="TlbRegister_CultureConditionalDateTime" runat="server" CssClass="toolbar"
                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemRegister_TlbRegister_CultureConditionalDateTime" runat="server"
                                                            ClientSideCommand="tlbItemRegister_TlbRegister_CultureConditionalDateTime_onClick();" DropDownImageHeight="16px"
                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                                            ItemType="Command" meta:resourcekey="tlbItemRegister_TlbRegister_CultureConditionalDateTime"
                                                            TextImageSpacing="5" Enabled="true" />
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
        </div>
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
        <asp:HiddenField runat="server" ID="hfCurrentDate_CultureConditionalDateTime" />
        <asp:HiddenField runat="server" ID="hfConnectionError_CultureConditionalDateTime" meta:resourcekey="hfConnectionError_CultureConditionalDateTime" />
        <asp:HiddenField runat="server" ID="hfErrorType_CultureConditionalDateTime" meta:resourcekey="hfErrorType_CultureConditionalDateTime" />
        <asp:HiddenField runat="server" ID="ReportParameterID" />
    </form>
</body>
</html>
