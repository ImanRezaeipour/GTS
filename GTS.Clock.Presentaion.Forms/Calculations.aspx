<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calculations.aspx.cs" Inherits="Calculations" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="CalculationsForm" runat="server" meta:resourcekey="CalculationsForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table id="Mastertbl_Calculations" style="width: 100%; height: 400px; font-family: Arial; font-size: small">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 60%">
                                <ComponentArt:ToolBar ID="TlbCalculationsIntroduction" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemCalculate_TlbCalculationsIntroduction" runat="server"
                                            ClientSideCommand="tlbItemCalculate_TlbCalculations_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemCalculate_TlbCalculationsIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemForcibleCalculate_TlbCalculationsIntroduction" runat="server"
                                            ClientSideCommand="tlbItemForcibleCalculate_TlbCalculationsIntroduction_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_red.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemForcibleCalculate_TlbCalculationsIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbCalculationsIntroduction" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbCalculationsIntroduction"
                                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbCalculationsIntroduction_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbCalculationsIntroduction"
                                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbCalculationsIntroduction_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbCalculationsIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TTlbCalculationsIntroduction" runat="server"
                                            ClientSideCommand="tlbItemExit_TlbCalculations_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbCalculationsIntroduction"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top" id="Container_PersonnelSearch_Calculations">
                    <table runat="server" style="width: 100%;">
                        <tr>
                            <td style="width: 50%" valign="top">
                                <table style="width: 80%;" id="PersonnelSearchBox_Calculations" class="BoxStyle">
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td colspan="2">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td style="width: 5%">
                                                                                <input id="RdbSinglePersonel_Calculations" type="radio" value="V1" checked="checked"
                                                                                    onclick="ChangeCalculatePersonnelCountState_Calculations('Single');" name="Personnel" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblOnePersonel_Calculations" CssClass="WhiteLabel" runat="server" Text="فردی" meta:resourcekey="lblOnePersonel_Calculations">
                                                                                </asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td style="width: 5%">
                                                                                <input id="RdbGroupPersonel_Calculations" type="radio" value="V1" onclick="ChangeCalculatePersonnelCountState_Calculations('Group');"
                                                                                    name="Personnel" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblGroupPersonel_Calculations" runat="server" CssClass="WhiteLabel" Text="گروهی" meta:resourcekey="lblGroupPersonel_Calculations">
                                                                                </asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 78%">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblPersonnel_Calculations" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblPersonnel_Calculations"
                                                                        Text=": پرسنل"></asp:Label>
                                                                </td>
                                                                <td id="Td4" runat="server" meta:resourcekey="InverseAlignObj">
                                                                    <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_Calculations" runat="server"
                                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                        Style="direction: ltr;" UseFadeEffect="false">
                                                                        <Items>
                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_Calculations"
                                                                                runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_Calculations_onClick();"
                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_Calculations"
                                                                                TextImageSpacing="5" />
                                                                            <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_Calculations"
                                                                                runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_Calculations_onClick();"
                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="first.png"
                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_Calculations"
                                                                                TextImageSpacing="5" />
                                                                            <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_Calculations"
                                                                                runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_Calculations_onClick();"
                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Before.png"
                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_Calculations"
                                                                                TextImageSpacing="5" />
                                                                            <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_Calculations"
                                                                                runat="server" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_Calculations_onClick();"
                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Next.png"
                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_Calculations"
                                                                                TextImageSpacing="5" />
                                                                            <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_Calculations"
                                                                                runat="server" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_Calculations_onClick();"
                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="last.png"
                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_Calculations"
                                                                                TextImageSpacing="5" />
                                                                        </Items>
                                                                    </ComponentArt:ToolBar>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td id="headerPersonnelCount_Calculations" style="width: 22%"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ComponentArt:CallBack ID="CallBack_cmbPersonnel_Calculations" runat="server" OnCallback="CallBack_cmbPersonnel_Calculations_Callback"
                                                            Height="26">
                                                            <Content>
                                                                <ComponentArt:ComboBox ID="cmbPersonnel_Calculations" runat="server" AutoComplete="true"
                                                                    AutoHighlight="false" CssClass="comboBox" DataFields="BarCode" DataTextField="Name"
                                                                    DropDownCssClass="comboDropDown" DropDownHeight="220" DropDownPageSize="7" DropDownWidth="400"
                                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_Calculations"
                                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client"
                                                                    SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox" TextBoxEnabled="true">
                                                                    <ClientTemplates>
                                                                        <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_Calculations">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr class="dataRow">
                                                                                    <td class="dataCell" style="width: 40%">## DataItem.getProperty('Text') ##
                                                                                    </td>
                                                                                    <td class="dataCell" style="width: 30%">## DataItem.getProperty('BarCode') ##
                                                                                    </td>
                                                                                    <td class="dataCell" style="width: 30%">## DataItem.getProperty('CardNum') ##
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ComponentArt:ClientTemplate>
                                                                    </ClientTemplates>
                                                                    <DropDownHeader>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="400">
                                                                            <tr class="headingRow">
                                                                                <td id="clmnName_cmbPersonnel_Calculations" class="headingCell" style="width: 40%; text-align: center">Name And Family
                                                                                </td>
                                                                                <td id="clmnBarCode_cmbPersonnel_Calculations" class="headingCell" style="width: 30%; text-align: center">BarCode
                                                                                </td>
                                                                                <td id="clmnCardNum_cmbPersonnel_Calculations" class="headingCell" style="width: 30%; text-align: center">CardNum
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </DropDownHeader>
                                                                    <ClientEvents>
                                                                        <Expand EventHandler="cmbPersonnel_Calculations_onExpand" />
                                                                        <Change EventHandler="cmbPersonnel_Calculations_onChange" />
                                                                    </ClientEvents>
                                                                </ComponentArt:ComboBox>
                                                                <asp:HiddenField ID="ErrorHiddenField_Personnel_Calculations" runat="server" />
                                                                <asp:HiddenField ID="hfPersonnelPageCount_Calculations" runat="server" />
                                                                <asp:HiddenField ID="hfPersonnelCount_Calculations" runat="server" />
                                                                <asp:HiddenField runat="server" ID="hfPersonnelSelectedCount_Calculations" Value="0" />
                                                            </Content>
                                                            <ClientEvents>
                                                                <BeforeCallback EventHandler="CallBack_cmbPersonnel_Calculations_onBeforeCallback" />
                                                                <CallbackComplete EventHandler="CallBack_cmbPersonnel_Calculations_onCallBackComplete" />
                                                                <CallbackError EventHandler="CallBack_cmbPersonnel_Calculations_onCallbackError" />
                                                            </ClientEvents>
                                                        </ComponentArt:CallBack>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input id="txtPersonnelSearch_Calculations" runat="server" class="TextBoxes" onclick="this.select();"
                                                            onfocus="this.select();" style="width: 98%" type="text" />
                                                    </td>
                                                    <td>
                                                        <ComponentArt:ToolBar ID="TlbSearchPersonnel_Calculations" runat="server" CssClass="toolbar"
                                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_Calculations" runat="server"
                                                                    ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_Calculations_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_Calculations"
                                                                    TextImageSpacing="5" />
                                                            </Items>
                                                        </ComponentArt:ToolBar>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSelectedPersonnelCount_Calculations" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td>
                                                        <ComponentArt:ToolBar ID="TlbAdvancedSearch_Calculations" runat="server" CssClass="toolbar"
                                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_Calculations"
                                                                    runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_Calculations_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_Calculations"
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
                            <td style="width: 50%" valign="top">
                                <table style="width: 100%;">
                                    <tr runat="server" meta:resourcekey="AlignObj">
                                        <td style="width: 50%">
                                            <asp:Label ID="lblFromDate_tbDaily_Calculations" runat="server" CssClass="WhiteLabel"
                                                meta:resourcekey="lblFromDate_tbDaily_Calculations" Text=": از تاریخ"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblToDate_tbDaily_Calculations" runat="server" CssClass="WhiteLabel"
                                                meta:resourcekey="lblToDate_tbDaily_Calculations" Text=": تا تاریخ"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" meta:resourcekey="AlignObj">
                                        <td id="Container_FromDateCalendars_tbDaily_Calculations">
                                            <table runat="server" id="Container_pdpFromDate_tbDaily_Calculations" visible="false"
                                                style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <pcal:PersianDatePickup ID="pdpFromDate_tbDaily_Calculations" runat="server" CssClass="PersianDatePicker"
                                                            ReadOnly="true" Style="margin: 0 40 0 0"></pcal:PersianDatePickup>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table runat="server" id="Container_gdpFromDate_tbDaily_Calculations" visible="false"
                                                style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <table id="Container_gCalFromDate_tbDaily_Calculations" border="0" cellpadding="0"
                                                            cellspacing="0">
                                                            <tr>
                                                                <td onmouseup="btn_gdpFromDate_tbDaily_Calculations_OnMouseUp(event)">
                                                                    <ComponentArt:Calendar ID="gdpFromDate_tbDaily_Calculations" runat="server" ControlType="Picker"
                                                                        MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                        SelectedDate="2008-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gdpFromDate_tbDaily_Calculations_OnDateChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                                <td style="font-size: 10px;"></td>
                                                                <td>
                                                                    <img id="btn_gdpFromDate_tbDaily_Calculations" alt="" class="calendar_button" onclick="btn_gdpFromDate_tbDaily_Calculations_OnClick(event)"
                                                                        onmouseup="btn_gdpFromDate_tbDaily_Calculations_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <ComponentArt:Calendar ID="gCalFromDate_tbDaily_Calculations" runat="server" AllowMonthSelection="false"
                                                AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpFromDate_tbDaily_Calculations"
                                                PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                <ClientEvents>
                                                    <SelectionChanged EventHandler="gCalFromDate_tbDaily_Calculations_OnChange" />
                                                    <Load EventHandler="gCalFromDate_tbDaily_Calculations_OnLoad" />
                                                </ClientEvents>
                                            </ComponentArt:Calendar>
                                        </td>
                                        <td id="Container_ToDateCalendars_tbDaily_Calculations">
                                            <table runat="server" id="Container_pdpToDate_tbDaily_Calculations" visible="false"
                                                style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <pcal:PersianDatePickup ID="pdpToDate_tbDaily_Calculations" runat="server" CssClass="PersianDatePicker"
                                                            ReadOnly="true"></pcal:PersianDatePickup>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table runat="server" id="Container_gdpToDate_tbDaily_Calculations" visible="false"
                                                style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <table id="Container_gCalToDate_tbDaily_Calculations" border="0" cellpadding="0"
                                                            cellspacing="0">
                                                            <tr>
                                                                <td onmouseup="btn_gdpToDate_tbDaily_Calculations_OnMouseUp(event)">
                                                                    <ComponentArt:Calendar ID="gdpToDate_tbDaily_Calculations" runat="server" ControlType="Picker"
                                                                        MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                        SelectedDate="2008-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gdpToDate_tbDaily_Calculations_OnDateChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                                <td style="font-size: 10px;"></td>
                                                                <td>
                                                                    <img id="btn_gdpToDate_tbDaily_Calculations" alt="" class="calendar_button" onclick="btn_gdpToDate_tbDaily_Calculations_OnClick(event)"
                                                                        onmouseup="btn_gdpToDate_tbDaily_Calculations_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <ComponentArt:Calendar ID="gCalToDate_tbDaily_Calculations" runat="server" AllowMonthSelection="false"
                                                AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpToDate_tbDaily_Calculations"
                                                PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                <ClientEvents>
                                                    <SelectionChanged EventHandler="gCalToDate_tbDaily_Calculations_OnChange" />
                                                    <Load EventHandler="gCalToDate_tbDaily_Calculations_OnLoad" />
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
                <td>
                    <ComponentArt:CallBack runat="server" ID="CallBack_Container_CalculationProgressFeatures" OnCallback="CallBack_Container_CalculationProgressFeatures_onCallBack">
                        <Content>
                            <table runat="server" id="Container_CalculationProgressFeatures" align="center" style="width: 99%" visible="false">
                                <tr>
                                    <td>
                                        <div runat="server" id="Progressbar_Calculations" style="font-size: 8pt; padding: 2px; border: solid black 1px; width: 100%">
                                            <table runat="server" id="tblProgressbar_Calculations" style="width: 100%">
                                                <tr>
                                                    <td runat="server" style="width: 1%" id="p1_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p2_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p3_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p4_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p5_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p6_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p7_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p8_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p9_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p10_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p11_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p12_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p13_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p14_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p15_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p16_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p17_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p18_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p19_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p20_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p21_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p22_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p23_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p24_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p25_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p26_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p27_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p28_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p29_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p30_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p31_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p32_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p33_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p34_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p35_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p36_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p37_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p38_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p39_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p40_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p41_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p42_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p43_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p44_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p45_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p46_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p47_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p48_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p49_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p50_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p51_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p52_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p53_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p54_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p55_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p56_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p57_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p58_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p59_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p60_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p61_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p62_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p63_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p64_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p65_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p66_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p67_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p68_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p69_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p70_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p71_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p72_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p73_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p74_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p75_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p76_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p77_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p78_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p79_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p80_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p81_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p82_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p83_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p84_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p85_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p86_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p87_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p88_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p89_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p90_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p91_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p92_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p93_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p94_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p95_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p96_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p97_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p98_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p99_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                    <td runat="server" style="width: 1%" id="p100_Progressbar_Calculations">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 33%" id="tdCalculatedPersonnelCount_Calculations">
                                                    <asp:Label runat="server" ID="lblCalculatedPersonnelCount_Calculations" Text="" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td style="width: 33%" id="tdAllPersonnelCount_Calculations">
                                                    <asp:Label runat="server" ID="lblAllPersonnelCount_Calculations" Text="" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td style="width: 33%" id="tdProgressPercentage_Calculations">
                                                    <asp:Label runat="server" ID="lblProgressPercentage_Calculations" Text="" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField runat="server" ID="hfCalculationProgress_Calculations"/>
                            <asp:HiddenField runat="server" ID="ErrorHiddenField_Calculations"/>
                        </Content>
                        <ClientEvents>
                            <CallbackComplete EventHandler="CallBack_Container_CalculationProgressFeatures_onCallbackComplete"/>
                            <CallbackError EventHandler="CallBack_Container_CalculationProgressFeatures_onCallbackError"/>
                        </ClientEvents>
                    </ComponentArt:CallBack>
                </td>
            </tr>
        </table>
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
        <asp:HiddenField runat="server" ID="hfheaderPersonnelCount_Calculations" meta:resourcekey="hfheaderPersonnelCount_Calculations" />
        <asp:HiddenField runat="server" ID="hfPersonnelPageSize_Calculations" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_Calculations" meta:resourcekey="hfCloseMessage_Calculations" />
        <asp:HiddenField runat="server" ID="hfCurrentDate_Calculations" />
        <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_Calculations" meta:resourcekey="hfclmnName_cmbPersonnel_Calculations" />
        <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_Calculations" meta:resourcekey="hfclmnBarCode_cmbPersonnel_Calculations" />
        <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_Calculations" meta:resourcekey="hfclmnCardNum_cmbPersonnel_Calculations" />
        <asp:HiddenField runat="server" ID="hfErrorType_Calculations" meta:resourcekey="hfErrorType_Calculations" />
        <asp:HiddenField runat="server" ID="hfConnectionError_Calculations" meta:resourcekey="hfConnectionError_Calculations" />
        <asp:HiddenField runat="server" ID="hfRetSuccessType_Calculations" meta:resourcekey="hfRetSuccessType_Calculations"/>
        <asp:HiddenField runat="server" ID="hfCalculationsCompletedSuccessfully_Calculations" meta:resourcekey="hfCalculationsCompletedSuccessfully_Calculations"/>
    </form>
</body>
</html>
