<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YearlyHolidays.aspx.cs" Inherits="YearlyHolidays" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="YearlyHolidaysForm" runat="server" meta:resourcekey="YearlyHolidaysForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 90%; height: 400px; font-family: Arial; font-size: small">
        <tr>
            <td>
                &nbsp;
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbYearlyHolidays" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbYearlyHolidays" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                        ClientSideCommand="tlbItemNew_TlbYearlyHolidays_onClick();" ItemType="Command"
                                        meta:resourcekey="tlbItemNew_TlbYearlyHolidays" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbYearlyHolidays" runat="server" ClientSideCommand="tlbItemEdit_TlbYearlyHolidays_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbTlbYearlyHolidays"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbYearlyHolidays" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                        ClientSideCommand="tlbItemDelete_TlbYearlyHolidays_onClick();" ItemType="Command"
                                        meta:resourcekey="tlbItemDelete_TlbYearlyHolidays" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHolidaysRegulation_TlbYearlyHolidays" runat="server"
                                        ClientSideCommand="tlbItemHolidaysRegulation_TlbYearlyHolidays_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="BallClockRed_silver.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHolidaysRegulation_TlbYearlyHolidays"
                                        TextImageSpacing="5" Enabled="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbYearlyHolidays" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbYearlyHolidays" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbYearlyHolidays_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbYearlyHolidays" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        Enabled="false" ClientSideCommand="tlbItemSave_TlbYearlyHolidays_onClick();"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbYearlyHolidays" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbYearlyHolidays" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                        Enabled="false" ClientSideCommand="tlbItemCancel_TlbYearlyHolidays_onClick();"
                                        ItemType="Command" meta:resourcekey="tlbItemCancel_TlbYearlyHolidays" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbYearlyHolidays" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbYearlyHolidays_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbYearlyHolidays"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbYearlyHolidays" runat="server" ClientSideCommand="tlbItemExit_TlbYearlyHolidays_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbYearlyHolidays"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_YearlyHolidays" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%" valign="top">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_YearlyHolidays_YearlyHolidays" class="HeaderLabel" style="width: 60%">
                                                    Yearly Holidays
                                                </td>
                                                <td id="loadingPanel_GridYearlyHolidays_YearlyHolidays" class="HeaderLabel" style="width: 35%">
                                                </td>
                                                <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridYearlyHolidays_YearlyHolidays" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridYearlyHolidays_YearlyHolidays"
                                                                runat="server" ClientSideCommand="Refresh_YearlyHolidays_YearlyHolidays();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridYearlyHolidays_YearlyHolidays"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridYearlyHolidays_YearlyHolidays"
                                            OnCallback="CallBack_GridYearlyHolidays_YearlyHolidays_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridYearlyHolidays_YearlyHolidays" runat="server" ShowFooter="false"
                                                    CssClass="Grid" EnableViewState="true" FillContainer="false" ImagesBaseUrl="images/Grid/"
                                                    AllowMultipleSelect="false" PagePaddingEnabled="true" PageSize="14" RunningMode="Client"
                                                    SearchTextCssClass="GridHeaderText" Width="100%" AllowColumnResizing="false"
                                                    ScrollBar="On" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                    ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                            HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                                                            DataKeyField="ID" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                            SortImageWidth="9">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="CustomCode" DefaultSortDirection="Descending"
                                                                    HeadingText="کد تعطیلات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnHolidayCustomCode_YearlyHolidays" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="عنوان تعطیلات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnHolidayTitle_YearlyHolidays" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                                    HeadingText="توضیحات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDescription_YearlyHolidays" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridYearlyHolidays_YearlyHolidays_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_YearlyHolidays" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridYearlyHolidays_YearlyHolidays_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridYearlyHolidays_YearlyHolidays_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="middle" align="center">
                            <table id="tbl_YearlyHolidayDetails_YearlyHolidays" style="width: 90%;" class="BoxStyle">
                                <tr id="Tr4" runat="server" meta:resourcekey="AlignObj">
                                    <td id="Td9" runat="server">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="DetailsBoxHeaderStyle">
                                                    <div id="header_YearlyHolidayDetails_YearlyHolidays" runat="server" meta:resourcekey="AlignObj"
                                                        style="color: White; width: 100%; height: 100%" class="BoxContainerHeader">
                                                        Yearly Holiday Details</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblHolidayGroupCustomCode_YearlyHolidays" runat="server" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblHolidayGroupCustomCode_YearlyHolidays" Text=": کد تعطیلات"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtHolidayGroupCustomCode_YearlyHolidays" runat="server" class="TextBoxes"
                                                        style="width: 98%;" type="text" disabled="disabled" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblHolidayTitle_YearlyHolidays" runat="server" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblHolidayTitle_YearlyHolidays" Text=": عنوان تعطیلات"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtHolidayGroupTitle_YearlyHolidays" runat="server" class="TextBoxes"
                                                        style="width: 98%;" type="text" disabled="disabled" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDescription_YearlyHolidays" runat="server" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblDescription_YearlyHolidays" Text=": توضیحات"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <textarea id="txtDescription_YearlyHolidays" cols="20" name="S1" rows="3" style="width: 98%;
                                                        height: 35px" disabled="disabled" class="TextBoxes"></textarea>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblYear_YearlyHolidays" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblYear_YearlyHolidays"
                                                        Text=": سال"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <ComponentArt:ComboBox ID="cmbYear_YearlyHolidays" runat="server" AutoComplete="true"
                                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                        TextBoxCssClass="comboTextBox" Width="100%" SelectedIndex="0" Enabled="false"
                                                        DropDownHeight="90">
                                                    </ComponentArt:ComboBox>
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
    <asp:HiddenField runat="server" ID="hfheader_YearlyHolidayDetails_YearlyHolidays"
        meta:resourcekey="hfheader_YearlyHolidayDetails_YearlyHolidays" />
    <asp:HiddenField runat="server" ID="hfheader_YearlyHolidays_YearlyHolidays" meta:resourcekey="hfheader_YearlyHolidays_YearlyHolidays" />
    <asp:HiddenField runat="server" ID="hfView_YearlyHolidays" meta:resourcekey="hfView_YearlyHolidays" />
    <asp:HiddenField runat="server" ID="hfAdd_YearlyHolidays" meta:resourcekey="hfAdd_YearlyHolidays" />
    <asp:HiddenField runat="server" ID="hfEdit_YearlyHolidays" meta:resourcekey="hfEdit_YearlyHolidays" />
    <asp:HiddenField runat="server" ID="hfDelete_YearlyHolidays" meta:resourcekey="hfDelete_YearlyHolidays" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_YearlyHolidays" meta:resourcekey="hfDeleteMessage_YearlyHolidays" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_YearlyHolidays" meta:resourcekey="hfCloseMessage_YearlyHolidays" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridYearlyHolidays_YearlyHolidays"
        meta:resourcekey="hfloadingPanel_GridYearlyHolidays_YearlyHolidays" />
    <asp:HiddenField runat="server" ID="hfErrorType_YearlyHolidays" meta:resourcekey="hfErrorType_YearlyHolidays" />
    <asp:HiddenField runat="server" ID="hfConnectionError_YearlyHolidays" meta:resourcekey="hfConnectionError_YearlyHolidays" />
    <asp:HiddenField runat="server" ID="hfCurrentYear_YearlyHolidays" />
    </form>
</body>
</html>
