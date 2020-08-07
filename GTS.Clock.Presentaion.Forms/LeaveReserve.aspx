<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveReserve.aspx.cs" Inherits="LeaveReserve" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/numericUpDown.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="LeaveReserveForm" runat="server" meta:resourcekey="LeaveReserveForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small" class="BoxStyle">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbLeaveReserve" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbLeaveReserve" runat="server" ClientSideCommand="tlbItemNew_TlbLeaveReserve_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbLeaveReserve"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbLeaveReserve" runat="server" ClientSideCommand="tlbItemDelete_TlbLeaveReserve_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDelete_TlbLeaveReserve"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbLeaveReserve" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        Enabled="false" ItemType="Command" meta:resourcekey="tlbItemSave_TlbLeaveReserve"
                                        ClientSideCommand="tlbItemSave_TlbLeaveReserve_onClick();" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbLeaveReserve" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                        Enabled="false" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbLeaveReserve"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemCancel_TlbLeaveReserve_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbLeaveReserve" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbLeaveReserve" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbLeaveReserve_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbLeaveReserve" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbLeaveReserve_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbLeaveReserve"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbLeaveReserve" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbLeaveReserve" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemExit_TlbLeaveReserve_onClick();" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="headerRelativePersonnel_LeaveReserve" style="width: 30%">
                        </td>
                        <td id="ActionMode_LeaveReserve" class="ToolbarMode" style="width: 10%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" class="BoxStyle">
                    <tr align="center">
                        <td style="width: 10%">
                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbOperationType_LeaveReserve"
                                OnCallback="CallBack_cmbOperationType_LeaveReserve_onCallBack" Height="26">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbOperationType_LeaveReserve" runat="server" AutoComplete="true"
                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                        TextBoxCssClass="comboTextBox" Style="width: 98%;" DropDownHeight="100" SelectedIndex="0"
                                        Enabled="false" TextBoxEnabled="true">
                                        <ClientEvents>
                                            <Expand EventHandler="cmbOperationType_LeaveReserve_onExpand" />
                                            <Collapse EventHandler="cmbOperationType_LeaveReserve_onCollapse" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_OperationTypes" />
                                </Content>
                                <ClientEvents>
                                    <BeforeCallback EventHandler="CallBack_cmbOperationType_LeaveReserve_onBeforeCallback" />
                                    <CallbackComplete EventHandler="CallBack_cmbOperationType_LeaveReserve_onCallbackComplete" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblLeaveReserveDuration_LeaveReserve" class="WhiteLabel" runat="server"
                                Text="مدت ذخیره مرخصی" meta:resourcekey="lblLeaveReserveDuration_LeaveReserve"></asp:Label>
                        </td>
                        <td style="width: 10%">
                            <input id="txtDay_LeaveReserve" type="text" class="TextBoxes" style="width: 80%;
                                text-align: center;" disabled="disabled" />
                        </td>
                        <td style="width: 8%">
                            <asp:Label ID="lblDay_LeaveReserve" runat="server" class="WhiteLabel" Text="روز و"
                                meta:resourcekey="lblDay_LeaveReserve"></asp:Label>
                        </td>
                        <td style="width: 12%">
                            <MKB:TimeSelector ID="TimeSelector_Hour_LeaveReserve" runat="server" DisplaySeconds="true"
                                MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                            </MKB:TimeSelector>
                        </td>
                        <td style="width: 12%">
                            <asp:Label ID="lblHourInDate_LeaveReserve" runat="server" class="WhiteLabel" Text="ساعت در تاریخ"
                                meta:resourcekey="lblHourInDate_LeaveReserve"></asp:Label>
                        </td>
                        <td id="Container_DateCalendars_LeaveReserve" runat="server" meta:resourcekey="AlignObj">
                            <table runat="server" id="Container_pdpDate_LeaveReserve" visible="false" style="width: 100%">
                                <tr>
                                    <td>
                                        <pcal:PersianDatePickup ID="pdpDate_LeaveReserve" runat="server" CssClass="PersianDatePicker"
                                            ReadOnly="true"></pcal:PersianDatePickup>
                                    </td>
                                </tr>
                            </table>
                            <table runat="server" id="Container_gdpDate_LeaveReserve" visible="false" style="width: 100%">
                                <tr>
                                    <td>
                                        <table id="Container_gCalDate_LeaveReserve" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td onmouseup="btn_gdpDate_LeaveReserve_OnMouseUp(event)">
                                                    <ComponentArt:Calendar ID="gdpDate_LeaveReserve" runat="server" ControlType="Picker"
                                                        MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                        SelectedDate="2008-1-1">
                                                        <ClientEvents>
                                                            <SelectionChanged EventHandler="gdpDate_LeaveReserve_OnDateChange" />
                                                        </ClientEvents>
                                                    </ComponentArt:Calendar>
                                                </td>
                                                <td style="font-size: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <img id="btn_gdpDate_LeaveReserve" alt="" class="calendar_button" onclick="btn_gdpDate_LeaveReserve_OnClick(event)"
                                                        onmouseup="btn_gdpDate_LeaveReserve_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                        <ComponentArt:Calendar ID="gCalDate_LeaveReserve" runat="server" AllowMonthSelection="false"
                                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                            CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                            DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                            MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                            OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpDate_LeaveReserve"
                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                            <ClientEvents>
                                                <SelectionChanged EventHandler="gCalDate_LeaveReserve_OnChange" />
                                                <Load EventHandler="gCalDate_LeaveReserve_OnLoad" />
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
                <asp:Label ID="lblDescription_LeaveReserve" runat="server" class="WhiteLabel" Text=": توضیحات"
                    meta:resourcekey="lblDescription_LeaveReserve"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <textarea id="txtDescription_LeaveReserve" cols="20" name="S1" rows="2" class="TextBoxes"
                    style="width: 99%; height: 40px;"></textarea>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%" class="BoxStyle">
                    <tr>
                        <td style="width: 100%">
                            <table style="width: 100%">
                                <tr>
                                    <td id="header_LeaveReserve_LeaveReserve" class="HeaderLabel" style="width: 50%">
                                        Leave Reserve
                                    </td>
                                    <td id="loadingPanel_GridLeaveReserve_LeaveReserve" class="HeaderLabel" style="width: 45%">
                                    </td>
                                    <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbRefresh_GridLeaveReserve_LeaveReserve" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridLeaveReserve_LeaveReserve"
                                                    runat="server" ClientSideCommand="Refresh_GridLeaveReserve_LeaveReserve();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridLeaveReserve_LeaveReserve"
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
                            <ComponentArt:CallBack runat="server" ID="CallBack_GridLeaveReserve_LeaveReserve"
                                OnCallback="CallBack_GridLeaveReserve_LeaveReserve_onCallBack">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridLeaveReserve_LeaveReserve" runat="server" CssClass="Grid"
                                        EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                        PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="14" RunningMode="Client"
                                        SearchTextCssClass="GridHeaderText" Width="100%" AllowMultipleSelect="false"
                                        ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                        ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                        ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                        ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                        <Levels>
                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                SortImageWidth="9">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Date" DefaultSortDirection="Descending"
                                                        HeadingText="تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDate_GridLeaveReserve_LeaveReserve" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="ActionTitle" DefaultSortDirection="Descending"
                                                        DataCellClientTemplateId="DataCellClientTemplate_clmnOperationTypeTitle_GridLeaveReserve_LeaveReserve"
                                                        HeadingText="نوع عملیات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnOperationType_GridLeaveReserve_LeaveReserve" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Day" DefaultSortDirection="Descending"
                                                        HeadingText="روز" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay_GridLeaveReserve_LeaveReserve" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Hour" DefaultSortDirection="Descending"
                                                        HeadingText="ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnHour_GridLeaveReserve_LeaveReserve" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                        HeadingText="توضیحات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDescription_GridLeaveReserve_LeaveReserve" />
                                                    <ComponentArt:GridColumn DataField="Action" Visible="false" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientTemplates>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnOperationTypeTitle_GridLeaveReserve_LeaveReserve">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center">
                                                            ##GetOperationType_LeaveReserve(DataItem.GetMember('Action').Value)##
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                        </ClientTemplates>
                                        <ClientEvents>
                                            <Load EventHandler="GridLeaveReserve_LeaveReserve_onLoad" />
                                            <ItemSelect EventHandler="GridLeaveReserve_LeaveReserve_onItemSelect" />
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField ID="ErrorHiddenField_LeaveReserve" runat="server" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridLeaveReserve_LeaveReserve_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridLeaveReserve_LeaveReserve_onCallbackError" />
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
    <asp:HiddenField runat="server" ID="hfTitle_DialogLeaveReserve" meta:resourcekey="hfTitle_DialogLeaveReserve" />
    <asp:HiddenField runat="server" ID="hfView_LeaveReserve" meta:resourcekey="hfView_LeaveReserve" />
    <asp:HiddenField runat="server" ID="hfAdd_LeaveReserve" meta:resourcekey="hfAdd_LeaveReserve" />
    <asp:HiddenField runat="server" ID="hfDelete_LeaveReserve" meta:resourcekey="hfDelete_LeaveReserve" />
    <asp:HiddenField runat="server" ID="hfErrorType_LeaveReserve" meta:resourcekey="hfErrorType_LeaveReserve" />
    <asp:HiddenField runat="server" ID="hfConnectionError_LeaveReserve" meta:resourcekey="hfConnectionError_LeaveReserve" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridLeaveReserve_LeaveReserve"
        meta:resourcekey="hfloadingPanel_GridLeaveReserve_LeaveReserve" />
    <asp:HiddenField runat="server" ID="hfheader_LeaveReserve_LeaveReserve" meta:resourcekey="hfheader_LeaveReserve_LeaveReserve" />
    <asp:HiddenField runat="server" ID="hfCurrentDate_LeaveReserve" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_LeaveReserve" meta:resourcekey="hfDeleteMessage_LeaveReserve" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_LeaveReserve" meta:resourcekey="hfCloseMessage_LeaveReserve" />
    <asp:HiddenField runat="server" ID="hfOperationTypes_LeaveReserve" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_LeaveReserve" meta:resourcekey="hfcmbAlarm_LeaveReserve" />
    </form>
</body>
</html>
