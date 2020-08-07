<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrafficOperationByOperator.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.TrafficOperationByOperator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/navStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/upload.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/rotator.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/colorPickerStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="TrafficOperationByOperatorFrom" runat="server" meta:resourcekey="TrafficOperationByOperatorFrom"
    onsubmit="return false;">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 90%; font-family: Arial; font-size: small">
        <tr>
            <td>
                <ComponentArt:ToolBar ID="TlbTrafficOperationByOperator" runat="server" CssClass="toolbar"
                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                    UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemNew_TrafficOperationByOperator" runat="server"
                            ClientSideCommand="" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                            ImageUrl="add.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TrafficOperationByOperator"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemEdit_TrafficOperationByOperator" runat="server"
                            ClientSideCommand="" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                            ImageUrl="edit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TrafficOperationByOperator"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TrafficOperationByOperator" runat="server"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDelete_TrafficOperationByOperator"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TrafficOperationByOperator" runat="server"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TrafficOperationByOperator"
                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TrafficOperationByOperator_onClick();" />
                        <ComponentArt:ToolBarItem ID="tlbItemCollectiveTraffic_TrafficOperationByOperator"
                            runat="server" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                            ClientSideCommand="ShowDialogCollectiveTraffic_TrafficOperationByOperator();"
                            ImageUrl="collection.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCollectiveTraffic_TrafficOperationByOperator"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TrafficOperationByOperator" runat="server"
                            DropDownImageHeight="16px" ClientSideCommand="tbTrafficOperationByOperator_TabStripMenus_onClose();"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemExit_TrafficOperationByOperator"
                            TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
            <td>
                <asp:ValidationSummary ID="TrafficOperationByOperatorValidationSummary" runat="server"
                    ValidationGroup="TrafficOperationByOperatorGroup" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td valign="middle" style="width: 60%" class="DetailsBoxHeaderStyle">
                                        <table style="width: 90%; background-image: url('Images/Ghadir/bg-body.jpg'); background-repeat: repeat">
                                            <tr>
                                                <td colspan="3" class="DetailsBoxHeaderStyle">
                                                    <div id="header_PersonnelSearchBox_TrafficOperationByOperator" runat="server" class="BoxContainerHeader"
                                                        meta:resourcekey="AlignObj" style="width: 100%; height: 100%">
                                                        Personnel Search</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 80%">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td id="Td2" runat="server" meta:resourcekey="AlignObj">
                                                                <asp:Label ID="lblPersonnel_TrafficOperationByOperator" runat="server" CssClass="WhiteLabel"
                                                                    meta:resourcekey="lblPersonnel_TrafficOperationByOperator" Text=": پرسنل"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 10%">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 10%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <table id="Table1" runat="server" meta:resourcekey="AlignObj" style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 80%">
                                                                <ComponentArt:CallBack ID="CallBack_cmbPersonnel_TrafficOperationByOperator" runat="server"
                                                                    OnCallback="CallBack_cmbPersonnel_TrafficOperationByOperator_onCallBack" Height="26">
                                                                    <Content>
                                                                        <ComponentArt:ComboBox ID="cmbPersonnel_TrafficOperationByOperator" runat="server"
                                                                            AutoComplete="true" AutoHighlight="false" CssClass="comboBox" DataFields="BarCode"
                                                                            DataTextField="Name" DropDownCssClass="comboDropDown" DropDownHeight="200" DropDownPageSize="10"
                                                                            DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                            FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_Users"
                                                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client"
                                                                            SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                            <ClientTemplates>
                                                                                <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_TrafficOperationByOperator">
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr class="dataRow">
                                                                                            <td class="dataCell" style="width: 50%">
                                                                                                ## DataItem.getProperty(&#39;Text&#39;) ##
                                                                                            </td>
                                                                                            <td class="dataCell" style="width: 50%">
                                                                                                ## DataItem.getProperty(&#39;BarCode&#39;) ##
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ComponentArt:ClientTemplate>
                                                                            </ClientTemplates>
                                                                            <DropDownHeader>
                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr class="headingRow">
                                                                                        <td id="clmnName_cmbPersonnel_TrafficOperationByOperator" class="headingCell" style="width: 35%;
                                                                                            text-align: center">
                                                                                            Name And Family
                                                                                        </td>
                                                                                        <td id="clmnBarCode_cmbPersonnel_TrafficOperationByOperator" class="headingCell"
                                                                                            style="width: 30%; text-align: center">
                                                                                            BarCode
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </DropDownHeader>
                                                                        </ComponentArt:ComboBox>
                                                                    </Content>
                                                                    <ClientEvents>
                                                                        <CallbackComplete EventHandler="CallBack_cmbPersonnel_TrafficOperationByOperator_onCallBackComplete" />
                                                                    </ClientEvents>
                                                                </ComponentArt:CallBack>
                                                            </td>
                                                            <td style="width: 20%">
                                                                <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_TrafficOperationByOperator" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                    UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_TrafficOperationByOperator"
                                                                            runat="server" ClientSideCommand="Fill_cmbPersonnel_Users('Refresh')" DropDownImageHeight="16px"
                                                                            DropDownImageWidth="16px" Enabled="false" ImageHeight="16px" ImageUrl="refresh.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_TrafficOperationByOperator"
                                                                            TextImageSpacing="5" />
                                                                        <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_TrafficOperationByOperator"
                                                                            runat="server" ClientSideCommand="Fill_cmbPersonnel_Users('Last')" DropDownImageHeight="16px"
                                                                            DropDownImageWidth="16px" Enabled="false" ImageHeight="16px" ImageWidth="16px"
                                                                            ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_TrafficOperationByOperator"
                                                                            TextImageSpacing="5" />
                                                                        <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_TrafficOperationByOperator"
                                                                            runat="server" ClientSideCommand="Fill_cmbPersonnel_Users('Next')" DropDownImageHeight="16px"
                                                                            DropDownImageWidth="16px" Enabled="false" ImageHeight="16px" ImageWidth="16px"
                                                                            ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_TrafficOperationByOperator"
                                                                            TextImageSpacing="5" />
                                                                        <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_TrafficOperationByOperator"
                                                                            runat="server" ClientSideCommand="Fill_cmbPersonnel_Users('Before')" DropDownImageHeight="16px"
                                                                            DropDownImageWidth="16px" Enabled="false" ImageHeight="16px" ImageWidth="16px"
                                                                            ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_TrafficOperationByOperator"
                                                                            TextImageSpacing="5" />
                                                                        <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_TrafficOperationByOperator"
                                                                            runat="server" ClientSideCommand="Fill_cmbPersonnel_Users('First')" DropDownImageHeight="16px"
                                                                            DropDownImageWidth="16px" Enabled="false" ImageHeight="16px" ImageWidth="16px"
                                                                            ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_TrafficOperationByOperator"
                                                                            TextImageSpacing="5" />
                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <table id="Table2" runat="server" meta:resourcekey="AlignObj" style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 70%">
                                                                <input id="txtPersonnelSearch_TrafficOperationByOperator" runat="server" class="TextBoxes"
                                                                    style="width: 98%" type="text" />
                                                            </td>
                                                            <td style="width: 15%">
                                                                <ComponentArt:ToolBar ID="TlbSearchPersonnel_TrafficOperationByOperator" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_TrafficOperationByOperator"
                                                                            runat="server" ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_TrafficOperationByOperator_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="false" ImageHeight="16px"
                                                                            ImageUrl="search.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_TrafficOperationByOperator"
                                                                            TextImageSpacing="5" />
                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                            <td style="width: 15%">
                                                                <ComponentArt:ToolBar ID="TlbَAdvancedSearch_TrafficOperationByOperator" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_TrafficOperationByOperator"
                                                                            runat="server" ClientSideCommand="ShowDialogPersonnelSearch_TrafficOperationByOperator();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="false" ImageHeight="16px"
                                                                            ImageUrl="eyeglasses.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_TrafficOperationByOperator"
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
                                    <td valign="top">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFromDate_TrafficOperationByOperator" runat="server" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblFromDate_TrafficOperationByOperator" Text=": از تاریخ"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Container_FromDateCalendars_TrafficOperationByOperator">
                                                    <table runat="server" id="Container_pdpFromDate_TrafficOperationByOperator" visible="false"
                                                        style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <pcal:PersianDatePickup ID="pdpFromDate_TrafficOperationByOperator" runat="server"
                                                                    CssClass="WhiteLabel" ReadOnly="true"></pcal:PersianDatePickup>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table runat="server" id="Container_gdpFromDate_TrafficOperationByOperator" visible="false"
                                                        style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalFromDate_TrafficOperationByOperator">
                                                                    <tr>
                                                                        <td onmouseup="btn_gdpFromDate_TrafficOperationByOperator_OnMouseUp(event)">
                                                                            <ComponentArt:Calendar ID="gdpFromDate_TrafficOperationByOperator" runat="server"
                                                                                ControlType="Picker" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                PickerFormat="Custom" SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gdpFromDate_TrafficOperationByOperator_OnDateChange" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                        <td style="font-size: 10px;">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <img id="btn_gdpFromDate_TrafficOperationByOperator" alt="" class="calendar_button"
                                                                                onclick="btn_gdpFromDate_TrafficOperationByOperator_OnClick(event)" onmouseup="btn_gdpFromDate_TrafficOperationByOperator_OnMouseUp(event)"
                                                                                src="Images/Calendar/btn_calendar.gif" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <ComponentArt:Calendar ID="gCalFromDate_TrafficOperationByOperator" runat="server"
                                                                    AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                    CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                    DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                    ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                    NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                    PopUpExpandControlId="btn_gdpFromDate_TrafficOperationByOperator" PrevImageUrl="cal_prevMonth.gif"
                                                                    SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                                    SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                                    <ClientEvents>
                                                                        <SelectionChanged EventHandler="gCalFromDate_TrafficOperationByOperator_OnChange" />
                                                                    </ClientEvents>
                                                                </ComponentArt:Calendar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblToDate_TrafficOperationByOperator" runat="server" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblToDate_TrafficOperationByOperator" Text=": تا تاریخ"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Container_ToDateCalendars_TrafficOperationByOperator">
                                                    <table runat="server" id="Container_pdpToDate_TrafficOperationByOperator" visible="false"
                                                        style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <pcal:PersianDatePickup ID="pdpToDate_TrafficOperationByOperator" runat="server"
                                                                    CssClass="WhiteLabel" ReadOnly="true"></pcal:PersianDatePickup>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table runat="server" id="Container_gdpToDate_TrafficOperationByOperator" visible="false"
                                                        style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalToDate_TrafficOperationByOperator">
                                                                    <tr>
                                                                        <td onmouseup="btn_gdpToDate_TrafficOperationByOperator_OnMouseUp(event)">
                                                                            <ComponentArt:Calendar ID="gdpToDate_TrafficOperationByOperator" runat="server" ControlType="Picker"
                                                                                PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gdpToDate_TrafficOperationByOperator_OnDateChange" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                        <td style="font-size: 10px;">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <img id="btn_gdpToDate_TrafficOperationByOperator" alt="" class="calendar_button"
                                                                                onclick="btn_gdpToDate_TrafficOperationByOperator_OnClick(event)" onmouseup="btn_gdpToDate_TrafficOperationByOperator_OnMouseUp(event)"
                                                                                src="Images/Calendar/btn_calendar.gif" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <ComponentArt:Calendar ID="gCalToDate_TrafficOperationByOperator" runat="server"
                                                                    AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                    CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                    DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                    ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                    NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                    PopUpExpandControlId="btn_gdpToDate_TrafficOperationByOperator" PrevImageUrl="cal_prevMonth.gif"
                                                                    SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                                    SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                    <ClientEvents>
                                                                        <SelectionChanged EventHandler="gCalToDate_TrafficOperationByOperator_OnChange" />
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
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%; background-image: url('Images/Ghadir/bg-body.jpg'); background-repeat: repeat;
                    border: outset 1px black;">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td id="header_TrafficDetails_TrafficOperationByOperator" class="HeaderLabel" style="width: 95%">
                                        TrafficDetails
                                    </td>
                                    <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbOperations_GridTrafficDetails_TrafficOperationByOperator"
                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemTrafficStateView_TlbOperations_GridTrafficDetails_TrafficOperationByOperator"
                                                    runat="server" ClientSideCommand="tlbItemTrafficStateView_TlbOperations_GridTrafficDetails_TrafficOperationByOperator_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="false" ImageHeight="16px"
                                                    ImageUrl="view.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemTrafficStateView_TlbOperations_GridTrafficDetails_TrafficOperationByOperator"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbOperations_GridTrafficDetails_TrafficOperationByOperator"
                                                    runat="server" ClientSideCommand="Refresh_GridTrafficDetails_TrafficOperationByOperator();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbOperations_GridTrafficDetails_TrafficOperationByOperator"
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
                            <ComponentArt:CallBack ID="CallBack_GridTrafficDetails_TrafficOperationByOperator"
                                runat="server" OnCallback="CallBack_GridTrafficDetails_TrafficOperationByOperator_onCallBack">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridTrafficDetails_TrafficOperationByOperator" runat="server"
                                        AllowColumnResizing="false" AllowMultipleSelect="false" EnableViewState="false"
                                        FillContainer="true" FooterCssClass="GridFooter" Height="150" ImagesBaseUrl="images/Grid/"
                                        PagePaddingEnabled="true" PagerStyle="Numbered" PagerTextCssClass="GridFooterText"
                                        PageSize="12" RunningMode="Client" ScrollBar="Auto" ScrollBarCssClass="ScrollBar"
                                        ScrollBarWidth="16" ScrollButtonHeight="17" ScrollButtonWidth="16" ScrollGripCssClass="ScrollGrip"
                                        ScrollImagesFolderUrl="images/Grid/scroller/" ScrollTopBottomImageHeight="2"
                                        ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageWidth="16" SearchTextCssClass="GridHeaderText"
                                        ShowFooter="false" TabIndex="0" Width="100%">
                                        <Levels>
                                            <ComponentArt:GridLevel AllowReordering="false" AllowSorting="false" AlternatingRowCssClass="AlternatingRow"
                                                DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TrafficType" DefaultSortDirection="Descending"
                                                        HeadingText="روز" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay_GridTrafficDetails_TrafficOperationByOperator" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TrafficTime" DefaultSortDirection="Descending"
                                                        HeadingText="تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDate_GridTrafficDetails_TrafficOperationByOperator" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RequestDate" DefaultSortDirection="Descending"
                                                        HeadingText="زمان" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnTime_GridTrafficDetails_TrafficOperationByOperator" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RequestDate" DefaultSortDirection="Descending"
                                                        HeadingText="نوع تردد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnTrafficType_GridTrafficDetails_TrafficOperationByOperator" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="State" DefaultSortDirection="Descending"
                                                        HeadingText="تغییر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnChange_GridTrafficDetails_TrafficOperationByOperator" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="State" DefaultSortDirection="Descending"
                                                        HeadingText="دستگاه" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMachine_GridTrafficDetails_TrafficOperationByOperator" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="State" DefaultSortDirection="Descending"
                                                        HeadingText="اپراتور" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnOperator_GridTrafficDetails_TrafficOperationByOperator" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                    </ComponentArt:DataGrid>
                                </Content>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
