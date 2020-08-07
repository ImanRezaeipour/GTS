<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ControlStations.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.ControlStations" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="FUA" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="ControlStationsForm" runat="server" meta:resourcekey="ControlStationsForm">
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
                            <ComponentArt:ToolBar ID="TlbControlStation" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbControlStation" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemNew_TlbControlStation_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbControlStation"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbControlStation" runat="server" ClientSideCommand="tlbItemEdit_TlbControlStation_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbControlStation"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbControlStation" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemDelete_TlbControlStation_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemDelete_TlbControlStation" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbControlStation" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidthhiftint="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbControlStation" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbControlStation_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbControlStation" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemSave_TlbControlStation_onClick();" DropDownImageWidth="16px"
                                        Enabled="false" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbControlStation" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbControlStation" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                        Enabled="false" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbControlStation"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemCancel_TlbControlStation_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbControlStation" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbControlStation_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbControlStation"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbControlStation" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbControlStation" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemExit_TlbControlStation_onClick();" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_ControlStations" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="color: White; width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_ControlStations_ControlStations" class="HeaderLabel" style="width: 50%">
                                                    Control Stations
                                                </td>
                                                <td id="loadingPanel_GridControlStations_ControlStations" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridControlStations_ControlStations" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridControlStations_ControlStations"
                                                                runat="server" ClientSideCommand="Refresh_GridControlStations_ControlStations();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridControlStations_ControlStations"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridControlStations_ControlStations"
                                            OnCallback="CallBack_GridControlStations_ControlStations_onCallback">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridControlStations_ControlStations" runat="server" CssClass="Grid"
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
                                                                <ComponentArt:GridColumn Align="Center" DataField="CustomCode" DefaultSortDirection="Descending"
                                                                    HeadingText="کد ایستگاه" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnControlStationCode_GridControlStation" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام ایستگاه" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnControlStationName_GridControlStation" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridControlStations_ControlStations_onLoad" />
                                                        <ItemSelect EventHandler="GridControlStations_ControlStations_onItemSelect" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_ControlStations" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridControlStations_ControlStations_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridControlStations_ControlStations_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="middle" align="center">
                            <table style="width: 100%;" class="BoxStyle" id="tblControlStations_ControlStations">
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td runat="server">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="DetailsBoxHeaderStyle">
                                                    <div id="header_ControlStationDetails_ControlStations" runat="server" meta:resourcekey="AlignObj"
                                                        style="color: White; width: 100%; height: 100%" class="BoxContainerHeader">
                                                        Control Station Details</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblControlStationCode_ControlStation" runat="server" meta:resourcekey="lblControlStationCode_ControlStation"
                                                        Text=": کد ایستگاه" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtControlStationCode_ControlStations"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblControlStationName_ControlStation" runat="server" meta:resourcekey="lblControlStationName_ControlStation"
                                                        Text=": نام ایستگاه" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtControlStationName_ControlStations"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
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
    <asp:HiddenField runat="server" ID="hfheader_ControlStationDetails_ControlStations"
        meta:resourcekey="hfheader_ControlStationDetails_ControlStations" />
    <asp:HiddenField runat="server" ID="hfheader_ControlStations_ControlStations" meta:resourcekey="hfheader_ControlStations_ControlStations" />
    <asp:HiddenField runat="server" ID="hfView_ControlStations" meta:resourcekey="hfView_ControlStations" />
    <asp:HiddenField runat="server" ID="hfAdd_ControlStations" meta:resourcekey="hfAdd_ControlStations" />
    <asp:HiddenField runat="server" ID="hfEdit_ControlStations" meta:resourcekey="hfEdit_ControlStations" />
    <asp:HiddenField runat="server" ID="hfDelete_ControlStations" meta:resourcekey="hfDelete_ControlStations" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_ControlStations" meta:resourcekey="hfDeleteMessage_ControlStations" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_ControlStations" meta:resourcekey="hfCloseMessage_ControlStations" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridControlStations_ControlStations"
        meta:resourcekey="hfloadingPanel_GridControlStations_ControlStations" />
    <asp:HiddenField runat="server" ID="hfErrorType_ControlStations" meta:resourcekey="hfErrorType_ControlStations" />
    <asp:HiddenField runat="server" ID="hfConnectionError_ControlStations" meta:resourcekey="hfConnectionError_ControlStations" />
    </form>
</body>
</html>
