<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CollectiveTraffic.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.CollectiveTraffic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/navStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" type="text/css" rel="stylesheet" />
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
    <link href="css/PersianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="CollectiveTrafficForm" runat="server" meta:resourcekey="CollectiveTrafficForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; height: 460px; background-repeat: repeat; font-family: Arial; font-size: small" class="BodyStyle">        
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbCollectiveTraffic" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbCollectiveTraffic" runat="server" ClientSideCommand="CollectiveTraffic_onSave();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbCollectiveTraffic"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbCollectiveTraffic" runat="server"
                                        ClientSideCommand="CollectiveTraffic_onCancel();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCollectiveTraffic" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbCollectiveTraffic" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbCollectiveTraffic" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbCollectiveTraffic_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbCollectiveTraffic" runat="server" ClientSideCommand="CollectiveTraffic_onClose();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbCollectiveTraffic"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td>
                            <asp:ValidationSummary ID="CollectiveTrafficValidationSummary" runat="server" ValidationGroup="CollectiveTrafficGroup" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table style="width: 50%;">
                    <tr>
                        <td runat="server" meta:resourcekey="AlignObj">
                            <asp:Label ID="lblBarCode_CollectiveTraffic" runat="server" Text=": بار کد" CssClass="WhiteLabel"
                                meta:resourcekey="lblBarCode_CollectiveTraffic"></asp:Label>
                        </td>
                        <td runat="server" meta:resourcekey="AlignObj">
                            <asp:Label ID="lblName_CollectiveTraffic" runat="server" Text=": نام" CssClass="WhiteLabel"
                                meta:resourcekey="lblName_CollectiveTraffic"></asp:Label>
                        </td>
                        <td runat="server" meta:resourcekey="AlignObj">
                            <asp:Label ID="lblFamily_CollectiveTraffic" runat="server" Text=": نام خانوادگی"
                                CssClass="WhiteLabel" meta:resourcekey="lblFamily_CollectiveTraffic"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input runat="server" type="text" id="txtBarcode_CollectiveTraffic" disabled="disabled" />
                        </td>
                        <td>
                            <input runat="server" type="text" id="txtName_CollectiveTraffic" disabled="disabled" />
                        </td>
                        <td>
                            <input runat="server" type="text" id="txtFamily_CollectiveTraffic" disabled="disabled" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table style="width: 40%;">
                    <tr>
                        <td style="width: 5%" runat="server" meta:resourcekey="AlignObj">
                            <input runat="server" type="radio" id="rdbTraffic_CollectiveTraffic" name="CollectiveTraffic"
                                onclick="rdbTraffic_CollectiveTraffic_onClick();" />
                        </td>
                        <td style="width: 28%" runat="server" meta:resourcekey="AlignObj">
                            <asp:Label ID="lblTraffic_CollectiveTraffic" runat="server" Text="تردد" CssClass="WhiteLabel"
                                meta:resourcekey="lblTraffic_CollectiveTraffic"></asp:Label>
                        </td>
                        <td style="width: 5%" runat="server" meta:resourcekey="AlignObj">
                            <input runat="server" type="radio" id="rdbLeave_CollectiveTraffic" name="CollectiveTraffic"
                                onclick="rdbLeave_CollectiveTraffic_onClick();" />
                        </td>
                        <td style="width: 28%" runat="server" meta:resourcekey="AlignObj">
                            <asp:Label ID="lblLeave_CollectiveTraffic" runat="server" Text="مرخصی" CssClass="WhiteLabel"
                                meta:resourcekey="lblLeave_CollectiveTraffic"></asp:Label>
                        </td>
                        <td style="width: 5%" runat="server" meta:resourcekey="AlignObj">
                            <input runat="server" type="radio" id="rdbMission_CollectiveTraffic" name="CollectiveTraffic"
                                onclick="rdbMission_CollectiveTraffic_onClick();" />
                        </td>
                        <td style="width: 28%" runat="server" meta:resourcekey="AlignObj">
                            <asp:Label ID="lblMission_CollectiveTraffic" runat="server" Text="ماموریت" CssClass="WhiteLabel"
                                meta:resourcekey="lblMission_CollectiveTraffic"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td style="width: 95%">
                            <div class="dhtmlgoodies_contentBox" id="box_RequestType_CollectiveTraffic" style="width: 82%;">
                                <div class="dhtmlgoodies_content" id="subbox_RequestType_CollectiveTraffic">
                                    <table style="width: 100%; background-image: url('Images/Ghadir/bg-body.jpg'); background-repeat: repeat">
                                        <tr>
                                            <td style="width: 33%">
                                                <asp:Label ID="lblType_CollectiveTraffic" runat="server" Text=": نوع" CssClass="WhiteLabel"
                                                    meta:resourcekey="lblType_CollectiveTraffic"></asp:Label>
                                            </td>
                                            <td style="width: 33%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 33%" rowspan="6" valign="top">
                                                <table style="width: 100%; border: 1px outset black">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblIllnessName_CollectiveTraffic" runat="server" Text=": نام بیماری"
                                                                CssClass="WhiteLabel" meta:resourcekey="lblIllnessName_CollectiveTraffic"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <ComponentArt:ComboBox ID="cmbIllnessName_CollectiveTraffic" runat="server" AutoComplete="true"
                                                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                DropDownHeight="175" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Down" FocusedCssClass="comboBoxHover"
                                                                HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                                                SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" Width="95%">
                                                            </ComponentArt:ComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDoctorName_CollectiveTraffic" runat="server" Text=": نام دکتر"
                                                                CssClass="WhiteLabel" meta:resourcekey="lblDoctorName_CollectiveTraffic"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <ComponentArt:ComboBox ID="cmbDoctorName_CollectiveTraffic" runat="server" AutoComplete="true"
                                                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                DropDownHeight="175" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Down" FocusedCssClass="comboBoxHover"
                                                                HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                                                SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" Width="95%">
                                                            </ComponentArt:ComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblMissionLocation_CollectiveTraffic" runat="server" Text=": محل ماموریت"
                                                                CssClass="WhiteLabel" meta:resourcekey="lblMissionLocation_CollectiveTraffic"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <ComponentArt:ComboBox ID="cmbMissionLocation_CollectiveTraffic" runat="server" AutoComplete="true"
                                                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                DropDownHeight="200" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Up" FocusedCssClass="comboBoxHover"
                                                                HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                                                DropDownWidth="300" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                                Width="95%" meta:resourcekey="cmbMissionLocation_CollectiveTraffic">
                                                                <DropDownContent>
                                                                    <ComponentArt:TreeView ID="trvMissionLocation_CollectiveTraffic" runat="server" CollapseImageUrl="images/TreeView/exp.gif"
                                                                        CssClass="TreeView" DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                                        EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                                        ExpandImageUrl="images/TreeView/col.gif" Height="95%" HoverNodeCssClass="HoverTreeNode"
                                                                        ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19"
                                                                        NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                                        SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" SiteMapXmlFile="XML/treeData.xml"
                                                                        Width="100%" meta:resourcekey="trvMissionLocation_CollectiveTraffic">
                                                                        <ClientEvents>
                                                                            <NodeSelect EventHandler="trvMissionLocation_CollectiveTraffic_onNodeSelect" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:TreeView>
                                                                </DropDownContent>
                                                            </ComponentArt:ComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <ComponentArt:ComboBox ID="cmbType_CollectiveTraffic" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownHeight="175" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Down" FocusedCssClass="comboBoxHover"
                                                    HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                                    SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" Width="100%">
                                                    <Items>
                                                        <ComponentArt:ComboBoxItem Id="Leave" Text="تست مرخصی" />
                                                        <ComponentArt:ComboBoxItem Id="Mission" Text="تست ماموریت" />
                                                    </Items>
                                                    <ClientEvents>
                                                        <Change EventHandler="cmbType_CollectiveTraffic_onChange" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFromDate_CollectiveTraffic" runat="server" Text=": از تاریخ" CssClass="WhiteLabel"
                                                    meta:resourcekey="lblFromDate_CollectiveTraffic"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblToDate_CollectiveTraffic" runat="server" Text=": تا تاریخ" CssClass="WhiteLabel"
                                                    meta:resourcekey="lblToDate_CollectiveTraffic"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Container_FromDateCalendars_CollectiveTraffic">
                                                <table runat="server" id="Container_pdpFromDate_CollectiveTraffic" visible="false"
                                                    style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <pcal:PersianDatePickup ID="pdpFromDate_CollectiveTraffic" runat="server" CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table runat="server" id="Container_gdpFromDate_CollectiveTraffic" visible="false"
                                                    style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalFromDate_CollectiveTraffic">
                                                                <tr>
                                                                    <td onmouseup="btn_gdpFromDate_CollectiveTraffic_OnMouseUp(event)">
                                                                        <ComponentArt:Calendar ID="gdpFromDate_CollectiveTraffic" runat="server" ControlType="Picker"
                                                                            PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                            SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                            <ClientEvents>
                                                                                <SelectionChanged EventHandler="gdpFromDate_CollectiveTraffic_OnDateChange" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:Calendar>
                                                                    </td>
                                                                    <td style="font-size: 10px;">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <img id="btn_gdpFromDate_CollectiveTraffic" alt="" class="calendar_button" onclick="btn_gdpFromDate_CollectiveTraffic_OnClick(event)"
                                                                            onmouseup="btn_gdpFromDate_CollectiveTraffic_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <ComponentArt:Calendar ID="gCalFromDate_CollectiveTraffic" runat="server" AllowMonthSelection="false"
                                                                AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpFromDate_CollectiveTraffic"
                                                                PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                                <ClientEvents>
                                                                    <SelectionChanged EventHandler="gCalFromDate_CollectiveTraffic_OnChange" />
                                                                    <Load EventHandler="gCalFromDate_CollectiveTraffic_onLoad" />
                                                                </ClientEvents>
                                                            </ComponentArt:Calendar>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td id="Container_ToDateCalendars_CollectiveTraffic">
                                                <table runat="server" id="Container_pdpToDate_CollectiveTraffic" visible="false"
                                                    style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <pcal:PersianDatePickup ID="pdpToDate_CollectiveTraffic" runat="server" CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table runat="server" id="Container_gdpToDate_CollectiveTraffic" visible="false" style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalToDate_CollectiveTraffic">
                                                                <tr>
                                                                    <td onmouseup="btn_gdpToDate_CollectiveTraffic_OnMouseUp(event)">
                                                                        <ComponentArt:Calendar ID="gdpToDate_CollectiveTraffic" runat="server" ControlType="Picker"
                                                                            PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                            SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                            <ClientEvents>
                                                                                <SelectionChanged EventHandler="gdpToDate_CollectiveTraffic_OnDateChange" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:Calendar>
                                                                    </td>
                                                                    <td style="font-size: 10px;">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <img id="btn_gdpToDate_CollectiveTraffic" alt="" class="calendar_button" onclick="btn_gdpToDate_CollectiveTraffic_OnClick(event)"
                                                                            onmouseup="btn_gdpToDate_CollectiveTraffic_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <ComponentArt:Calendar ID="gCalToDate_CollectiveTraffic" runat="server" AllowMonthSelection="false"
                                                                AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpToDate_CollectiveTraffic"
                                                                PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                                <ClientEvents>
                                                                    <SelectionChanged EventHandler="gCalToDate_CollectiveTraffic_OnChange" />
                                                                    <Load EventHandler="gCalToDate_CollectiveTraffic_onLoad" />
                                                                </ClientEvents>
                                                            </ComponentArt:Calendar>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFromHour_CollectiveTraffic" runat="server" Text=": از ساعت" CssClass="WhiteLabel"
                                                    meta:resourcekey="lblFromHour_CollectiveTraffic"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblToHour_CollectiveTraffic" runat="server" Text=": تا ساعت" CssClass="WhiteLabel"
                                                    meta:resourcekey="lblToHour_CollectiveTraffic"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td1" runat="server" meta:resourcekey="AlignObj">
                                                <MKB:TimeSelector ID="TimeSelector_FromHour_CollectiveTraffic" runat="server" DisplaySeconds="true"
                                                    MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                                </MKB:TimeSelector>
                                            </td>
                                            <td id="Td2" runat="server" meta:resourcekey="AlignObj">
                                                <MKB:TimeSelector ID="TimeSelector_ToHour_CollectiveTraffic" runat="server" DisplaySeconds="true"
                                                    MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                                </MKB:TimeSelector>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDescription_CollectiveTraffic" runat="server" Text=": توضیحات"
                                                    CssClass="WhiteLabel" meta:resourcekey="lblDescription_CollectiveTraffic"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <textarea id="txtDescription_CollectiveTraffic" cols="20" name="S1" rows="2" style="width: 97%"></textarea>
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
        <tr>
            <td style="height: 70%" align="center">
                <table style="width: 91%; background-image: url('Images/Ghadir/bg-body.jpg'); border: outset 1px black;
                    background-repeat: repeat">
                    <tr>
                        <td runat="server" id="header_RegisteredRequests_CollectiveTraffic" style="color: White;
                            font-weight: bold; font-family: Arial; width: 100%" meta:resourcekey="AlignObj">
                            Registered Requests
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ComponentArt:CallBack ID="CallBack_GridRegisteredRequests_CollectiveTraffic" runat="server"
                                OnCallback="CallBack_GridRegisteredRequests_CollectiveTraffic_OnCallBack">
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridRegisteredRequests_CollectiveTraffic_OnCallbackComplete" />
                                </ClientEvents>
                                <Content>
                                    <ComponentArt:DataGrid ID="GridRegisteredRequests_CollectiveTraffic" runat="server"
                                        AllowMultipleSelect="false" EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter"
                                        Height="150" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerStyle="Numbered"
                                        ShowFooter="false" PagerTextCssClass="GridFooterText" PageSize="14" RunningMode="Client"
                                        SearchTextCssClass="GridHeaderText" TabIndex="0" Width="100%" AllowColumnResizing="false"
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
                                                    <ComponentArt:GridColumn Align="Center" DataField="Day" DefaultSortDirection="Descending"
                                                        HeadingText="روز" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay_GridRegisteredRequests_CollectiveTraffic" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Date" DefaultSortDirection="Descending"
                                                        HeadingText="تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDate_GridRegisteredRequests_CollectiveTraffic" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="FromHour" DefaultSortDirection="Descending"
                                                        HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromHour_GridRegisteredRequests_CollectiveTraffic" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="ToHour" DefaultSortDirection="Descending"
                                                        HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToHour_GridRegisteredRequests_CollectiveTraffic" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TrafficType" DefaultSortDirection="Descending"
                                                        HeadingText="نوع تردد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnTrafficType_GridRegisteredRequests_CollectiveTraffic" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="State" DefaultSortDirection="Descending"
                                                        HeadingText="وضعیت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnState_GridRegisteredRequests_CollectiveTraffic" />
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
