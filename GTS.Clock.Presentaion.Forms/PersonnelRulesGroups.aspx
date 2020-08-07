<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonnelRulesGroups.aspx.cs"
    Inherits="PersonnelRulesGroups" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/PersianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="PersonnelRulesGroupsForm" runat="server" meta:resourcekey="PersonnelRulesGroupsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%;" class="BoxStyle">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbPersonnelRulesGroups" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbPersonnelRulesGroups" runat="server"
                                        ClientSideCommand="tlbItemNew_TlbPersonnelRulesGroups_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemNew_TlbPersonnelRulesGroups" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbPersonnelRulesGroups" runat="server"
                                        ClientSideCommand="tlbItemEdit_TlbPersonnelRulesGroups_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemEdit_TlbPersonnelRulesGroups" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbPersonnelRulesGroups" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png"
                                        ImageWidth="16px" ClientSideCommand="tlbItemDelete_TlbPersonnelRulesGroups_onClick();"
                                        ItemType="Command" meta:resourcekey="tlbItemDelete_TlbPersonnelRulesGroups" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbPersonnelRulesGroups" runat="server"
                                        ClientSideCommand="tlbItemSave_TlbPersonnelRulesGroups_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" Enabled="false" ImageHeight="16px" ImageUrl="save_silver.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbPersonnelRulesGroups"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbPersonnelRulesGroups" runat="server"
                                        ClientSideCommand="tlbItemCancel_TlbPersonnelRulesGroups_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" Enabled="false" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbPersonnelRulesGroups"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbRulesGroup" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbPersonnelRulesGroups" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbRulesGroup_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbRulesGroup" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbRulesGroup_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbRulesGroup"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPersonnelRulesGroups" runat="server"
                                        ClientSideCommand="tlbItemExit_TlbPersonnelRulesGroups_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbPersonnelRulesGroups" TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_PersonnelRulesGroups" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="lblRuleGroupName_PersonnelRulesGroups" runat="server" Text=": نام گروه قانون"
                                CssClass="WhiteLabel" meta:resourcekey="lblRuleGroupName_PersonnelRulesGroups"></asp:Label>
                        </td>
                        <td style="width: 30%">
                            <asp:Label ID="lblFromDate_PersonnelRulesGroups" runat="server" Text=": از تاریخ"
                                CssClass="WhiteLabel" meta:resourcekey="lblFromDate_PersonnelRulesGroups"></asp:Label>
                        </td>
                        <td style="width: 30%">
                            <asp:Label ID="lblToDate_PersonnelRulesGroups" runat="server" Text=": تا تاریخ" CssClass="WhiteLabel"
                                meta:resourcekey="lblToDate_PersonnelRulesGroups"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="bottom">
                            <ComponentArt:CallBack ID="CallBackcmbRulesGroups_PersonnelRulesGroups" runat="server"
                                OnCallback="CallBackcmbRulesGroups_PersonnelRulesGroups_onCallBack" Height="26">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbRulesGroups_PersonnelRulesGroups" runat="server" AutoComplete="true"
                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                        DropImageUrl="Images/ComboBox/ddn.png" Enabled="false" FocusedCssClass="comboBoxHover"
                                        HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                        SelectedItemCssClass="comboItemHover" Style="width: 90%" TextBoxCssClass="comboTextBox"
                                        DataTextField="Name" DataValueField="ID" TextBoxEnabled="true">
                                        <ClientEvents>
                                            <Expand EventHandler="cmbRulesGroups_PersonnelRulesGroups_onExpand" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_RulesGroups_PersonnelRulesGroups" />
                                </Content>
                                <ClientEvents>
                                    <BeforeCallback EventHandler="CallBackcmbRulesGroups_PersonnelRulesGroups_onBeforeCallback" />
                                    <CallbackComplete EventHandler="CallBackcmbRulesGroups_PersonnelRulesGroups_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBackcmbRulesGroups_PersonnelRulesGroups_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                        <td valign="top" id="Container_FromDateCalendars_PersonnelRulesGroups">
                            <table runat="server" id="Container_pdpFromDate_PersonnelRulesGroups" visible="false"
                                style="width: 100%">
                                <tr>
                                    <td>
                                        <pcal:PersianDatePickup ID="pdpFromDate_PersonnelRulesGroups" runat="server" CssClass="PersianDatePicker"
                                            ReadOnly="true"></pcal:PersianDatePickup>
                                    </td>
                                </tr>
                            </table>
                            <table runat="server" id="Container_gdpFromDate_PersonnelRulesGroups" visible="false"
                                style="width: 100%">
                                <tr>
                                    <td>
                                        <table id="Container_gCalFromDate_PersonnelRulesGroups" border="0" cellpadding="0"
                                            cellspacing="0">
                                            <tr>
                                                <td onmouseup="btn_gdpFromDate_PersonnelRulesGroups_OnMouseUp(event)">
                                                    <ComponentArt:Calendar ID="gdpFromDate_PersonnelRulesGroups" runat="server" ControlType="Picker"
                                                        MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                        SelectedDate="2008-1-1">
                                                        <ClientEvents>
                                                            <SelectionChanged EventHandler="gdpFromDate_PersonnelRulesGroups_OnDateChange" />
                                                        </ClientEvents>
                                                    </ComponentArt:Calendar>
                                                </td>
                                                <td style="font-size: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <img id="btn_gdpFromDate_PersonnelRulesGroups" alt="" class="calendar_button" onclick="btn_gdpFromDate_PersonnelRulesGroups_OnClick(event)"
                                                        onmouseup="btn_gdpFromDate_PersonnelRulesGroups_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                        <ComponentArt:Calendar ID="gCalFromDate_PersonnelRulesGroups" runat="server" AllowMonthSelection="false"
                                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                            CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                            DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                            MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                            OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpFromDate_PersonnelRulesGroups"
                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                            <ClientEvents>
                                                <SelectionChanged EventHandler="gCalFromDate_PersonnelRulesGroups_OnChange" />
                                                <Load EventHandler="gCalFromDate_PersonnelRulesGroups_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:Calendar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" id="Container_ToDateCalendars_PersonnelRulesGroups">
                            <table runat="server" id="Container_pdpToDate_PersonnelRulesGroups" visible="false"
                                style="width: 100%">
                                <tr>
                                    <td>
                                        <pcal:PersianDatePickup ID="pdpToDate_PersonnelRulesGroups" runat="server" CssClass="PersianDatePicker"
                                            ReadOnly="true"></pcal:PersianDatePickup>
                                    </td>
                                </tr>
                            </table>
                            <table runat="server" id="Container_gdpToDate_PersonnelRulesGroups" visible="false"
                                style="width: 100%">
                                <tr>
                                    <td>
                                        <table id="Container_gCalToDate_PersonnelRulesGroups" border="0" cellpadding="0"
                                            cellspacing="0">
                                            <tr>
                                                <td onmouseup="btn_gdpToDate_PersonnelRulesGroups_OnMouseUp(event)">
                                                    <ComponentArt:Calendar ID="gdpToDate_PersonnelRulesGroups" runat="server" ControlType="Picker"
                                                        MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                        SelectedDate="2008-1-1">
                                                        <ClientEvents>
                                                            <SelectionChanged EventHandler="gdpToDate_PersonnelRulesGroups_OnDateChange" />
                                                        </ClientEvents>
                                                    </ComponentArt:Calendar>
                                                </td>
                                                <td style="font-size: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <img id="btn_gdpToDate_PersonnelRulesGroups" alt="" class="calendar_button" onclick="btn_gdpToDate_PersonnelRulesGroups_OnClick(event)"
                                                        onmouseup="btn_gdpToDate_PersonnelRulesGroups_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                        <ComponentArt:Calendar ID="gCalToDate_PersonnelRulesGroups" runat="server" AllowMonthSelection="false"
                                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                            CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                            DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                            MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                            OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpToDate_PersonnelRulesGroups"
                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                            <ClientEvents>
                                                <SelectionChanged EventHandler="gCalToDate_PersonnelRulesGroups_OnChange" />
                                                <Load EventHandler="gCalToDate_PersonnelRulesGroups_onLoad" />
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
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td id="header_PersonnelRulesGroups_PersonnelRulesGroups" class="HeaderLabel" style="width: 50%">
                                        Personnel Rules Groups
                                    </td>
                                    <td id="loadingPanel_GridPersonnelRulesGroups_PersonnelRulesGroups" class="HeaderLabel"
                                        style="width: 45%">
                                    </td>
                                    <td id="Td1" runat="server" meta:resourcekey="InverseAlignObj" style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbRefresh_GridPersonnelRulesGroups_PersonnelRulesGroups"
                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridPersonnelRulesGroups_PersonnelRulesGroups"
                                                    runat="server" ClientSideCommand="Refresh_GridPersonnelRulesGroups_PersonnelRulesGroups();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridPersonnelRulesGroups_PersonnelRulesGroups"
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
                            <ComponentArt:CallBack runat="server" ID="CallBack_GridPersonnelRulesGroups_PersonnelRulesGroups"
                                OnCallback="CallBack_GridPersonnelRulesGroups_PersonnelRulesGroups_onCallBack">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridPersonnelRulesGroups_PersonnelRulesGroups" runat="server"
                                        CssClass="Grid" EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter"
                                        ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                        PageSize="11" RunningMode="Client" SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false"
                                        ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                        ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                        ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                        ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="240">
                                        <Levels>
                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                SortImageWidth="9">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="RuleCategory.ID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RuleCategory.Name" DefaultSortDirection="Descending"
                                                        HeadingText="نام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnName_GridPersonnelRulesGroups_PersonnelRulesGroups"
                                                        Visible="true" Width="80" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="UIFromDate" DefaultSortDirection="Descending"
                                                        HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromDate_GridPersonnelRulesGroups_PersonnelRulesGroups"
                                                        Visible="true" Width="80" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="UIToDate" DefaultSortDirection="Descending"
                                                        HeadingText="تا تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToDate_GridPersonnelRulesGroups_PersonnelRulesGroups"
                                                        Visible="true" Width="80" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientEvents>
                                            <Load EventHandler="GridPersonnelRulesGroups_PersonnelRulesGroups_onLoad" />
                                            <ItemSelect EventHandler="GridPersonnelRulesGroups_PersonnelRulesGroups_onItemSelect" />
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_PersonnelRulesGroups_PersonnelRulesGroups" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridPersonnelRulesGroups_PersonnelRulesGroups_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridPersonnelRulesGroups_PersonnelRulesGroups_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="320px">
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
    <asp:HiddenField runat="server" ID="hfheader_PersonnelRulesGroups_PersonnelRulesGroups"
        meta:resourcekey="hfheader_PersonnelRulesGroups_PersonnelRulesGroups" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogPersonnelRulesGroups" meta:resourcekey="hfTitle_DialogPersonnelRulesGroups" />
    <asp:HiddenField runat="server" ID="hfView_PersonnelRulesGroups" meta:resourcekey="hfView_PersonnelRulesGroups" />
    <asp:HiddenField runat="server" ID="hfAdd_PersonnelRulesGroups" meta:resourcekey="hfAdd_PersonnelRulesGroups" />
    <asp:HiddenField runat="server" ID="hfEdit_PersonnelRulesGroups" meta:resourcekey="hfEdit_PersonnelRulesGroups" />
    <asp:HiddenField runat="server" ID="hfDelete_PersonnelRulesGroups" meta:resourcekey="hfDelete_PersonnelRulesGroups" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_PersonnelRulesGroups" meta:resourcekey="hfDeleteMessage_PersonnelRulesGroups" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_PersonnelRulesGroups" meta:resourcekey="hfCloseMessage_PersonnelRulesGroups" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridPersonnelRulesGroups_PersonnelRulesGroups"
        meta:resourcekey="hfloadingPanel_GridPersonnelRulesGroups_PersonnelRulesGroups" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_PersonnelRulesGroups" meta:resourcekey="hfcmbAlarm_PersonnelRulesGroups" />
    <asp:HiddenField runat="server" ID="hfCurrentDate_PersonnelRulesGroups" />
    <asp:HiddenField runat="server" ID="hfErrorType_PersonnelRulesGroups" meta:resourcekey="hfErrorType_PersonnelRulesGroups" />
    <asp:HiddenField runat="server" ID="hfConnectionError_PersonnelRulesGroups" meta:resourcekey="hfConnectionError_PersonnelRulesGroups" />
    </form>
</body>
</html>
