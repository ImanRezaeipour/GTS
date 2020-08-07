<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterExceptionShifts.aspx.cs" Inherits="GTS.Clock.RuleDesigner.UI.Web.MasterExceptionShifts" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link id="Link1" href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link2" href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link3" href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link4" href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link5" href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link6" href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link7" href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link8" href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link9" href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link10" href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link11" href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link12" href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link13" href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="MasterExceptionShiftsForm" runat="server" meta:resourcekey="MasterExceptionShiftsForm"
        onsubmit="return false;">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table id="Mastertbl_MasterExceptionShifts" style="width: 100%; height: 400px; font-family: Arial; font-size: small"
            class="BodyStyle">
            <tr>
                <td style="height: 10%">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbMasterExceptionShifts" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbMasterExceptionShifts" runat="server"
                                            ClientSideCommand="tlbItemNew_TlbMasterExceptionShifts_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemNew_TlbMasterExceptionShifts" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbMasterExceptionShifts" runat="server"
                                            ClientSideCommand="tlbItemEdit_TlbMasterExceptionShifts_onClick()" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemEdit_TlbMasterExceptionShifts" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbMasterExceptionShifts" runat="server"
                                            ClientSideCommand="tlbItemDelete_TlbMasterExceptionShifts_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemDelete_TlbMasterExceptionShifts"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemTwoDayReplacement_TlbMasterExceptionShifts"
                                            runat="server" ClientSideCommand="tlbItemTwoDayReplacement_TlbMasterExceptionShifts_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exchange.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemTwoDayReplacement_TlbMasterExceptionShifts"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemTwoPersonnelReplacement_TlbMasterExceptionShifts"
                                            runat="server" ClientSideCommand="tlbItemTwoPersonnelReplacement_TlbMasterExceptionShifts_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="role.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemTwoPersonnelReplacement_TlbMasterExceptionShifts"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemMonthlyExceptionShifts_TlbMasterExceptionShifts"
                                            runat="server" ClientSideCommand="tlbItemMonthlyExceptionShifts_TlbMasterExceptionShifts_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="monthlyExceptionShifts.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemMonthlyExceptionShifts_TlbMasterExceptionShifts"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbMasterExceptionShifts" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbMasterExceptionShifts"
                                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbMasterExceptionShifts_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbMasterExceptionShifts"
                                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbMasterExceptionShifts_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbMasterExceptionShifts"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbMasterExceptionShifts" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbMasterExceptionShifts"
                                            TextImageSpacing="5" ClientSideCommand="tlbItemExit_TlbMasterExceptionShifts_onClick();" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="ActionMode_MasterExceptionShifts" class="ToolbarMode" style="width: 20%"></td>
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
                                    <tr>
                                        <td style="width: 20%" valign="middle">
                                            <div id="Div1" runat="server" class="DropDownHeader" meta:resourcekey="AlignObj" style="width: 100%">
                                                <img id="imgbox_SearchByPersonnel_MasterExceptionShifts" runat="server" alt="" onclick="imgbox_SearchByPersonnel_MasterExceptionShifts_onClick();"
                                                    src="~/Images/Ghadir/arrowDown.jpg" />
                                                <span id="header_SearchByPersonnelBox_MasterExceptionShifts">انتخاب پرسنل</span>
                                            </div>
                                            <div id="box_SearchByPersonnel_MasterExceptionShifts" class="dhtmlgoodies_contentBox"
                                                style="width: 36.5%;">
                                                <div id="subbox_SearchByPersonnel_MasterExceptionShifts" class="dhtmlgoodies_content">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 90%">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblPersonnel_MasterExceptionShifts" runat="server" CssClass="ContentLabel"
                                                                                            meta:resourcekey="lblPersonnel_MasterExceptionShifts" Text=": پرسنل"></asp:Label>
                                                                                    </td>
                                                                                    <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj">
                                                                                        <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_MasterExceptionShifts" runat="server"
                                                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                            UseFadeEffect="false" Style="direction: ltr">
                                                                                            <Items>
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_MasterExceptionShifts"
                                                                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_MasterExceptionShifts_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_MasterExceptionShifts"
                                                                                                    TextImageSpacing="5" />
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_MasterExceptionShifts"
                                                                                                    runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_MasterExceptionShifts_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                    ImageUrl="first.png" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_MasterExceptionShifts"
                                                                                                    TextImageSpacing="5" />
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_MasterExceptionShifts"
                                                                                                    runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_MasterExceptionShifts_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                    ImageUrl="Before.png" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_MasterExceptionShifts"
                                                                                                    TextImageSpacing="5" />
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_MasterExceptionShifts"
                                                                                                    runat="server" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_MasterExceptionShifts_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                    ImageUrl="Next.png" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_MasterExceptionShifts"
                                                                                                    TextImageSpacing="5" />
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_MasterExceptionShifts"
                                                                                                    runat="server" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_MasterExceptionShifts_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                    ImageUrl="last.png" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_MasterExceptionShifts"
                                                                                                    TextImageSpacing="5" />
                                                                                            </Items>
                                                                                        </ComponentArt:ToolBar>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="width: 10%">&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 90%">
                                                                            <ComponentArt:CallBack ID="CallBack_cmbPersonnel_MasterExceptionShifts" runat="server"
                                                                                Height="26" OnCallback="CallBack_cmbPersonnel_MasterExceptionShifts_onCallBack">
                                                                                <Content>
                                                                                    <ComponentArt:ComboBox ID="cmbPersonnel_MasterExceptionShifts" runat="server" AutoComplete="true"
                                                                                        AutoHighlight="false" CssClass="comboBox" DataFields="BarCode" DataTextField="Name"
                                                                                        DropDownCssClass="comboDropDown" DropDownWidth="390" DropDownHeight="250" DropDownPageSize="8"
                                                                                        DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_MasterExceptionShifts"
                                                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client"
                                                                                        SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                                        <ClientTemplates>
                                                                                            <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_MasterExceptionShifts">
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
                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="390">
                                                                                                <tr class="headingRow">
                                                                                                    <td id="clmnName_cmbPersonnel_MasterExceptionShifts" class="headingCell" style="width: 40%; text-align: center">Name And Family
                                                                                                    </td>
                                                                                                    <td id="clmnBarCode_cmbPersonnel_MasterExceptionShifts" class="headingCell" style="width: 30%; text-align: center">BarCode
                                                                                                    </td>
                                                                                                    <td id="clmnCardNum_cmbPersonnel_MasterExceptionShifts" class="headingCell" style="width: 30%; text-align: center">CardNum
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </DropDownHeader>
                                                                                        <ClientEvents>
                                                                                            <Expand EventHandler="cmbPersonnel_MasterExceptionShifts_onExpand" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:ComboBox>
                                                                                    <asp:HiddenField ID="ErrorHiddenField_Personnel_MasterExceptionShifts" runat="server" />
                                                                                    <asp:HiddenField ID="hfPersonnelPageCount_MasterExceptionShifts" runat="server" />
                                                                                    <asp:HiddenField ID="hfPersonnelCount_MasterExceptionShifts" runat="server" />
                                                                                </Content>
                                                                                <ClientEvents>
                                                                                    <BeforeCallback EventHandler="CallBack_cmbPersonnel_MasterExceptionShifts_onBeforeCallback" />
                                                                                    <CallbackComplete EventHandler="CallBack_cmbPersonnel_MasterExceptionShifts_onCallBackComplete" />
                                                                                    <CallbackError EventHandler="CallBack_cmbPersonnel_MasterExceptionShifts_onCallbackError" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:CallBack>
                                                                        </td>
                                                                        <td style="width: 10%">&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 90%">
                                                                            <input id="txtPersonnelSearch_MasterExceptionShifts" runat="server" class="TextBoxes"
                                                                                style="width: 95%" type="text" />
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                            <ComponentArt:ToolBar ID="TlbSearchPersonnel_MasterExceptionShifts" runat="server"
                                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_MasterExceptionShifts"
                                                                                        runat="server" ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_MasterExceptionShifts_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_MasterExceptionShifts"
                                                                                        TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 90%">&nbsp;
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                            <ComponentArt:ToolBar ID="TlbAdvancedSearch_MasterExceptionShifts" runat="server"
                                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_MasterExceptionShifts"
                                                                                        runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_MasterExceptionShifts_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_MasterExceptionShifts"
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
                                        <td style="width: 70%">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 10%" align="center">
                                                        <asp:Label ID="lblPMasterFromDate_MasterExceptionShifts" runat="server" meta:resourcekey="lblFromDate_MasterExceptionShifts"
                                                            Text="از:" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                    <td style="width: 40%" id="Container_FromDateCalendars_MasterExceptionShifts">
                                                        <table runat="server" id="Container_pdpMasterFromDate_MasterExceptionShifts" visible="false"
                                                            style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <pcal:PersianDatePickup ID="pdpMasterFromDate_MasterExceptionShifts" runat="server"
                                                                        CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table runat="server" id="Container_gdpMasterFromDate_MasterExceptionShifts" visible="false"
                                                            style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalMasterFromDate_MasterExceptionShifts">
                                                                        <tr>
                                                                            <td onmouseup="btn_gdpMasterFromDate_MasterExceptionShifts_OnMouseUp(event)">
                                                                                <ComponentArt:Calendar ID="gdpMasterFromDate_MasterExceptionShifts" runat="server"
                                                                                    ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                    PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                                    <ClientEvents>
                                                                                        <SelectionChanged EventHandler="gdpMasterFromDate_MasterExceptionShifts_OnDateChange" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:Calendar>
                                                                            </td>
                                                                            <td style="font-size: 10px;">&nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <img id="btn_gdpMasterFromDate_MasterExceptionShifts" alt="" class="calendar_button"
                                                                                    onclick="btn_gdpMasterFromDate_MasterExceptionShifts_OnClick(event)" onmouseup="btn_gdpMasterFromDate_MasterExceptionShifts_OnMouseUp(event)"
                                                                                    src="Images/Calendar/btn_calendar.gif" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <ComponentArt:Calendar ID="gCalMasterFromDate_MasterExceptionShifts" runat="server"
                                                                        AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                        CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                        MaxDate="2122-1-1" DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover"
                                                                        DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar" MonthCssClass="month"
                                                                        NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday"
                                                                        PopUp="Custom" PopUpExpandControlId="btn_gdpMasterFromDate_MasterExceptionShifts"
                                                                        PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                        SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gCalMasterFromDate_MasterExceptionShifts_OnChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 10%" align="center">
                                                        <asp:Label ID="lblMasterToDate_MasterExceptionShifts" runat="server" meta:resourcekey="lblToDate_MasterExceptionShifts"
                                                            Text="تا :" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                    <td id="Container_ToDateCalendars_MasterExceptionShifts" style="width: 40%">
                                                        <table runat="server" id="Container_pdpMasterToDate_MasterExceptionShifts" visible="false"
                                                            style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <pcal:PersianDatePickup ID="pdpMasterToDate_MasterExceptionShifts" runat="server"
                                                                        CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table id="Container_gdpMasterToDate_MasterExceptionShifts" runat="server" visible="false"
                                                            style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <table id="Container_gCalMasterToDate_MasterExceptionShifts" border="0" cellpadding="0"
                                                                        cellspacing="0">
                                                                        <tr>
                                                                            <td onmouseup="btn_gdpMasterToDate_MasterExceptionShifts_OnMouseUp(event)">
                                                                                <ComponentArt:Calendar ID="gdpMasterToDate_MasterExceptionShifts" runat="server"
                                                                                    ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                    PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                                    <ClientEvents>
                                                                                        <SelectionChanged EventHandler="gdpMasterToDate_MasterExceptionShifts_OnDateChange" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:Calendar>
                                                                            </td>
                                                                            <td style="font-size: 10px;">&nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <img id="btn_gdpMasterToDate_MasterExceptionShifts" alt="" class="calendar_button"
                                                                                    onclick="btn_gdpMasterToDate_MasterExceptionShifts_OnClick(event)" onmouseup="btn_gdpMasterToDate_MasterExceptionShifts_OnMouseUp(event)"
                                                                                    src="Images/Calendar/btn_calendar.gif" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <ComponentArt:Calendar ID="gCalMasterToDate_MasterExceptionShifts" runat="server"
                                                                        AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                        CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                        DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                        ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                        NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                        PopUpExpandControlId="btn_gdpMasterToDate_MasterExceptionShifts" PrevImageUrl="cal_prevMonth.gif"
                                                                        SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                                        SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gCalMasterToDate_MasterExceptionShifts_OnChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <ComponentArt:ToolBar ID="TlbView_MasterExceptionShifts" runat="server" CssClass="toolbar"
                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemView_TlbView_MasterExceptionShifts" runat="server"
                                                        ClientSideCommand="tlbItemView_TlbView_MasterExceptionShifts_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemView_TlbView_MasterExceptionShifts"
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
            <tr>
                <td style="height: 50%" valign="top">
                    <table style="width: 100%;" class="BoxStyle">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td id="header_ExceptionShifts_MasterExceptionShifts" class="HeaderLabel" style="width: 50%">شیفت های استثناء
                                        </td>
                                        <td id="loadingPanel_GridExceptionShifts_MastExceptionShifts" class="HeaderLabel"
                                            style="width: 45%"></td>
                                        <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridExceptionShifts_ExceptionShifts" runat="server"
                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridExceptionShifts_ExceptionShifts"
                                                        runat="server" ClientSideCommand="Refresh_GridExceptionShifts_ExceptionShifts();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridExceptionShifts_ExceptionShifts"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridMasterExceptionShifts_MasterExceptionShifts"
                                    OnCallback="CallBack_GridMasterExceptionShifts_MasterExceptionShifts_onCallBack">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridMasterExceptionShifts_MasterExceptionShifts" runat="server"
                                            CssClass="Grid" EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter"
                                            ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                            PageSize="12" RunningMode="Client" SearchTextCssClass="GridHeaderText" Width="100%"
                                            AllowMultipleSelect="false" ShowFooter="false" AllowColumnResizing="false" ScrollBar="On"
                                            ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16"
                                            ScrollImagesFolderUrl="images/Grid/scroller/" ScrollButtonWidth="16" ScrollButtonHeight="17"
                                            ScrollBarCssClass="ScrollBar" ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                                                    DataKeyField="ID" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                    SortImageWidth="9">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheDate" DefaultSortDirection="Descending"
                                                            HeadingText="تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDate_GridMasterExceptionShifts_MasterExceptionShifts" />
                                                        <ComponentArt:GridColumn DataField="Shift.ID" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Shift.Name" DefaultSortDirection="Descending"
                                                            HeadingText="نام شیفت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnShiftName_GridMasterExceptionShifts_MasterExceptionShifts" />
                                                        <ComponentArt:GridColumn DataField="Person.ID" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Person.Name" DefaultSortDirection="Descending"
                                                            HeadingText="نام و نام خانوادگی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPersonnelName_GridMasterExceptionShifts_MasterExceptionShifts" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientEvents>
                                                <Load EventHandler="GridMasterExceptionShifts_MasterExceptionShifts_onLoad" />
                                                <ItemSelect EventHandler="GridMasterExceptionShifts_MasterExceptionShifts_onItemSelect" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_ExceptionShifts" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridMasterExceptionShifts_MasterExceptionShifts_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridMasterExceptionShifts_MasterExceptionShifts_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                    </table>
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
        <asp:HiddenField runat="server" ID="hfheader_SearchByPersonnelBox_MasterExceptionShifts"
            meta:resourcekey="hfheader_SearchByPersonnelBox_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfheader_ExceptionShifts_MasterExceptionShifts"
            meta:resourcekey="hfheader_ExceptionShifts_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfView_MasterExceptionShifts" meta:resourcekey="hfView_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfAdd_MasterExceptionShifts" meta:resourcekey="hfAdd_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfEdit_MasterExceptionShifts" meta:resourcekey="hfEdit_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfDelete_MasterExceptionShifts" meta:resourcekey="hfDelete_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfTwoDayReplacement_MasterExceptionShifts" meta:resourcekey="hfTwoDayReplacement_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfTwoPersonnelReplacement_MasterExceptionShifts"
            meta:resourcekey="hfTwoPersonnelReplacement_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_MasterExceptionShifts" meta:resourcekey="hfDeleteMessage_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_MasterExceptionShifts" meta:resourcekey="hfCloseMessage_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridExceptionShifts_MasterExceptionShifts"
            meta:resourcekey="hfloadingPanel_GridExceptionShifts_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfErrorType_MasterExceptionShifts" meta:resourcekey="hfErrorType_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfConnectionError_MasterExceptionShifts" meta:resourcekey="hfConnectionError_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_MasterExceptionShifts"
            meta:resourcekey="hfclmnName_cmbPersonnel_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_MasterExceptionShifts"
            meta:resourcekey="hfclmnBarCode_cmbPersonnel_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_MasterExceptionShifts"
            meta:resourcekey="hfclmnCardNum_cmbPersonnel_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfPersonnelPageSize_MasterExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfCurrentDate_MasterExceptionShifts" />
    </form>
</body>
</html>
