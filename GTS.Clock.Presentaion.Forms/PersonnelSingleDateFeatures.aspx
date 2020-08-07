<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonnelSingleDateFeatures.aspx.cs"
    Inherits="PersonnelSingleDateFeatures" %>

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
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="PersonnelSingleDateFeaturesFrom" runat="server" meta:resourcekey="PersonnelSingleDateFeaturesFrom">
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
                            <ComponentArt:ToolBar ID="TlbPersonnelSingleDateFeatures" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbPersonnelSingleDateFeatures" runat="server"
                                        ClientSideCommand="tlbItemNew_TlbPersonnelSingleDateFeatures_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemNew_TlbPersonnelSingleDateFeatures"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbPersonnelSingleDateFeatures" runat="server"
                                        ClientSideCommand="tlbItemEdit_TlbPersonnelSingleDateFeatures_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemEdit_TlbPersonnelSingleDateFeatures"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbPersonnelSingleDateFeatures" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png"
                                        ImageWidth="16px" ClientSideCommand="tlbItemDelete_TlbPersonnelSingleDateFeatures_onClick();"
                                        ItemType="Command" meta:resourcekey="tlbItemDelete_TlbPersonnelSingleDateFeatures"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbPersonnelSingleDateFeatures" runat="server"
                                        ClientSideCommand="tlbItemSave_TlbPersonnelSingleDateFeatures_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" Enabled="false" ImageHeight="16px" ImageUrl="save_silver.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbPersonnelSingleDateFeatures"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbPersonnelSingleDateFeatures" runat="server"
                                        ClientSideCommand="tlbItemCancel_TlbPersonnelSingleDateFeatures_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" Enabled="false" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbPersonnelSingleDateFeatures"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbPersonnelSingleDateFeatures" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbPersonnelSingleDateFeatures"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbPersonnelSingleDateFeatures_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbPersonnelSingleDateFeatures"
                                        runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbPersonnelSingleDateFeatures_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbPersonnelSingleDateFeatures"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPersonnelSingleDateFeatures" runat="server"
                                        ClientSideCommand="tlbItemExit_TlbPersonnelSingleDateFeatures_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbPersonnelSingleDateFeatures"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_PersonnelSingleDateFeatures" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 80%;">
                    <tr>
                        <td style="width: 50%">
                            <asp:Label ID="lblPersonnelSingleDateFeatureName_PersonnelSingleDateFeatures" runat="server"
                                Text="" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td style="width: 50%">
                            <asp:Label ID="lblFromDate_PersonnelSingleDateFeatures" runat="server" Text=": از تاریخ"
                                CssClass="WhiteLabel" meta:resourcekey="lblFromDate_PersonnelSingleDateFeatures"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="bottom">
                            <ComponentArt:CallBack ID="CallBackcmbSingleDateFeatures_PersonnelSingleDateFeatures"
                                runat="server" OnCallback="CallBackcmbSingleDateFeatures_PersonnelSingleDateFeatures_onCallBack"
                                Height="26">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbSingleDateFeatures_PersonnelSingleDateFeatures" runat="server"
                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                        DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                        DropImageUrl="Images/ComboBox/ddn.png" Enabled="false" FocusedCssClass="comboBoxHover"
                                        HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                        SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox"
                                        DataTextField="Name" DataValueField="ID" TextBoxEnabled="true">
                                        <ClientEvents>
                                            <Expand EventHandler="cmbSingleDateFeatures_PersonnelSingleDateFeatures_onExpand" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_PersonnelSingleDateFeature_PersonnelSingleDateFeatures" />
                                </Content>
                                <ClientEvents>
                                    <BeforeCallback EventHandler="CallBackcmbSingleDateFeatures_PersonnelSingleDateFeatures_onBeforeCallback" />
                                    <CallbackComplete EventHandler="CallBackcmbSingleDateFeatures_PersonnelSingleDateFeatures_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBackcmbSingleDateFeatures_PersonnelSingleDateFeatures_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                        <td valign="top" id="Container_FromDateCalendars_PersonnelSingleDateFeatures">
                            <table runat="server" id="Container_pdpFromDate_PersonnelSingleDateFeatures" visible="false"
                                style="width: 100%">
                                <tr>
                                    <td>
                                        <pcal:PersianDatePickup ID="pdpFromDate_PersonnelSingleDateFeatures" runat="server"
                                            CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                    </td>
                                </tr>
                            </table>
                            <table runat="server" id="Container_gdpFromDate_PersonnelSingleDateFeatures" visible="false"
                                style="width: 100%">
                                <tr>
                                    <td>
                                        <table id="Container_gCalFromDate_PersonnelSingleDateFeatures" border="0" cellpadding="0"
                                            cellspacing="0">
                                            <tr>
                                                <td onmouseup="btn_gdpFromDate_PersonnelSingleDateFeatures_OnMouseUp(event)">
                                                    <ComponentArt:Calendar ID="gdpFromDate_PersonnelSingleDateFeatures" runat="server"
                                                        ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                        PickerFormat="Custom" SelectedDate="2008-1-1">
                                                        <ClientEvents>
                                                            <SelectionChanged EventHandler="gdpFromDate_PersonnelSingleDateFeatures_OnDateChange" />
                                                        </ClientEvents>
                                                    </ComponentArt:Calendar>
                                                </td>
                                                <td style="font-size: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <img id="btn_gdpFromDate_PersonnelSingleDateFeatures" alt="" class="calendar_button"
                                                        onclick="btn_gdpFromDate_PersonnelSingleDateFeatures_OnClick(event)" onmouseup="btn_gdpFromDate_PersonnelSingleDateFeatures_OnMouseUp(event)"
                                                        src="Images/Calendar/btn_calendar.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                        <ComponentArt:Calendar ID="gCalFromDate_PersonnelSingleDateFeatures" runat="server"
                                            AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                            CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                            DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                            ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                            NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                            PopUpExpandControlId="btn_gdpFromDate_PersonnelSingleDateFeatures" PrevImageUrl="cal_prevMonth.gif"
                                            SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                            SwapSlide="Linear" VisibleDate="2008-1-1">
                                            <ClientEvents>
                                                <SelectionChanged EventHandler="gCalFromDate_PersonnelSingleDateFeatures_OnChange" />
                                                <Load EventHandler="gCalFromDate_PersonnelSingleDateFeatures_onLoad" />
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
                                    <td id="header_PersonnelSingleDateFeatures_PersonnelSingleDateFeatures" class="HeaderLabel"
                                        style="width: 50%">
                                        Personnel Work Groups
                                    </td>
                                    <td id="loadingPanel_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures"
                                        class="HeaderLabel" style="width: 45%">
                                    </td>
                                    <td id="Td1" runat="server" meta:resourcekey="InverseAlignObj" style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbRefresh_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures"
                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures"
                                                    runat="server" ClientSideCommand="Refresh_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures"
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
                            <ComponentArt:CallBack runat="server" ID="CallBack_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures"
                                OnCallback="CallBack_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures_onCallBack">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures"
                                        runat="server" CssClass="Grid" EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter"
                                        ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                        PageSize="11" RunningMode="Client" SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false"
                                        ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
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
                                                    <ComponentArt:GridColumn DataField="" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="" DefaultSortDirection="Descending"
                                                        HeadingText="نام" HeadingTextCssClass="HeadingText" Visible="true" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="UIFromDate" DefaultSortDirection="Descending"
                                                        HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" Visible="true" meta:resourcekey="clmnUIFromDate_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientEvents>
                                            <Load EventHandler="GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures_onLoad" />
                                            <ItemSelect EventHandler="GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures_onItemSelect" />
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_PersonnelSingleDateFeatures" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures_onCallbackError" />
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
    <asp:HiddenField runat="server" ID="hfheader_PersonnelSingleDateFeatures_PersonnelSingleDateFeatures" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogPersonnelSingleDateFeatures" />
    <asp:HiddenField runat="server" ID="hfView_PersonnelSingleDateFeatures" meta:resourcekey="hfView_PersonnelSingleDateFeatures" />
    <asp:HiddenField runat="server" ID="hfAdd_PersonnelSingleDateFeatures" meta:resourcekey="hfAdd_PersonnelSingleDateFeatures" />
    <asp:HiddenField runat="server" ID="hfEdit_PersonnelSingleDateFeatures" meta:resourcekey="hfEdit_PersonnelSingleDateFeatures" />
    <asp:HiddenField runat="server" ID="hfDelete_PersonnelSingleDateFeatures" meta:resourcekey="hfDelete_PersonnelSingleDateFeatures" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_PersonnelSingleDateFeatures" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_PersonnelSingleDateFeatures" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures"
        meta:resourcekey="hfloadingPanel_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_PersonnelSingleDateFeatures" meta:resourcekey="hfcmbAlarm_PersonnelSingleDateFeatures" />
    <asp:HiddenField runat="server" ID="hfCurrentDate_PersonnelSingleDateFeatures" meta:resourcekey="hfCurrentDate_PersonnelSingleDateFeatures" />
    <asp:HiddenField runat="server" ID="hfErrorType_PersonnelSingleDateFeatures" meta:resourcekey="hfErrorType_PersonnelSingleDateFeatures" />
    <asp:HiddenField runat="server" ID="hfConnectionError_PersonnelSingleDateFeatures"
        meta:resourcekey="hfConnectionError_PersonnelSingleDateFeatures" />
    </form>
</body>
</html>
