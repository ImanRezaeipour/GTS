<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Corporations.aspx.cs" Inherits="Corporations" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="CorporationsForm" runat="server" meta:resourcekey="CorporationsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small">
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbCorporations" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemNew_TlbCorporations" runat="server" ClientSideCommand="tlbItemNew_TlbCorporations_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbCorporations"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbCorporations" runat="server" ClientSideCommand="tlbItemEdit_TlbCorporations_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbCorporations"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbCorporations" runat="server" DropDownImageHeight="16px"
                                                    ClientSideCommand="tlbItemDelete_TlbCorporations_onClick();" DropDownImageWidth="16px"
                                                    ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                                    meta:resourcekey="tlbItemDelete_TlbCorporations" TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbCorporations" runat="server" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemHelp_TlbCorporations" TextImageSpacing="5"
                                                    ClientSideCommand="tlbItemHelp_TlbCorporations_onClick();" />
                                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbCorporations" runat="server" ClientSideCommand="tlbItemSave_TlbCorporations_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="false" ImageHeight="16px"
                                                    ImageUrl="save_silver.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbCorporations"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbCorporations" runat="server" ClientSideCommand="tlbItemCancel_TlbCorporations_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="false" ImageHeight="16px"
                                                    ImageUrl="cancel_silver.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCorporations"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbCorporations" runat="server"
                                                    ClientSideCommand="tlbItemFormReconstruction_TlbCorporations_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbCorporations"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbCorporations" runat="server" ClientSideCommand="tlbItemExit_TlbCorporations_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbCorporations"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="ActionMode_Corporations" class="ToolbarMode">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td valign="top" style="width: 60%">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td id="header_Corporations_Corporations" class="HeaderLabel" style="width: 50%">
                                                                Corporations
                                                            </td>
                                                            <td id="loadingPanel_GridCorporations_Corporations" class="HeaderLabel" style="width: 45%">
                                                            </td>
                                                            <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                                <ComponentArt:ToolBar ID="TlbRefresh_GridCorporations_Corporations" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridCorporations_Corporations"
                                                                            runat="server" ClientSideCommand="Refresh_GridCorporations_Corporations();" DropDownImageHeight="16px"
                                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                            ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridCorporations_Corporations"
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
                                                    <ComponentArt:CallBack runat="server" ID="CallBack_GridCorporations_Corporations"
                                                        OnCallback="CallBack_GridCorporations_Corporations_onCallBack">
                                                        <Content>
                                                            <ComponentArt:DataGrid ID="GridCorporations_Corporations" runat="server" CssClass="Grid"
                                                                EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                                PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="15" RunningMode="Client"
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
                                                                            <ComponentArt:GridColumn Align="Center" DataField="Code" DefaultSortDirection="Descending"
                                                                                HeadingText="کد شرکت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnCode_GridCorporations_Corporations" />
                                                                            <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                                HeadingText="نام شرکت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnName_GridCorporations_Corporations" />
                                                                            <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                                                HeadingText="توضیحات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDescription_GridCorporations_Corporations" />
                                                                            <ComponentArt:GridColumn DataField="EconomicCode" Visible="false" />
                                                                            <ComponentArt:GridColumn DataField="Phone" Visible="false" />
                                                                            <ComponentArt:GridColumn DataField="Fax" Visible="false" />
                                                                            <ComponentArt:GridColumn DataField="Address" Visible="false" />
                                                                        </Columns>
                                                                    </ComponentArt:GridLevel>
                                                                </Levels>
                                                                <ClientEvents>
                                                                    <ItemSelect EventHandler="GridCorporations_Corporations_onItemSelect" />
                                                                    <Load EventHandler="GridCorporations_Corporations_onLoad" />
                                                                </ClientEvents>
                                                            </ComponentArt:DataGrid>
                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_Corporations" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <CallbackComplete EventHandler="CallBack_GridCorporations_Corporations_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_GridCorporations_Corporations_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="middle">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td class="DetailsBoxHeaderStyle">
                                                    <div id="header_tblCorporations_Corporations" runat="server" style="color: White;
                                                        width: 100%; height: 100%" class="BoxContainerHeader">
                                                        Corporation Details</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCorporationName_Corporations" runat="server" CssClass="WhiteLabel"
                                                        Text=": نام شرکت" meta:resourcekey="lblCorporationName_Corporations"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtCorporationName_Corporations" type="text" style="width: 97%" class="TextBoxes"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCorporationCode_Corporations" runat="server" CssClass="WhiteLabel"
                                                        Text=": کد شرکت" meta:resourcekey="lblCorporationCode_Corporations"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtCorporationCode_Corporations" type="text" style="width: 97%" class="TextBoxes"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEconomicCode_Corporations" runat="server" CssClass="WhiteLabel"
                                                        Text=": کد اقتصادی" meta:resourcekey="lblEconomicCode_Corporations"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtEconomicCode_Corporations" type="text" style="width: 97%" class="TextBoxes"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTelNumber_Corporations" runat="server" CssClass="WhiteLabel" Text=": تلفن"
                                                        meta:resourcekey="lblTelNumber_Corporations"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtTelNumber_Corporations" type="text" style="width: 97%" class="TextBoxes"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFax_Corporations" runat="server" CssClass="WhiteLabel" Text=": فکس"
                                                        meta:resourcekey="lblFax_Corporations"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtFax_Corporations" type="text" style="width: 97%" class="TextBoxes"
                                                        disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAddress_Corporations" runat="server" CssClass="WhiteLabel" Text=": آدرس"
                                                        meta:resourcekey="lblAddress_Corporations"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <textarea id="txtAddress_Corporations" rows="7" cols="20" style="width: 97%; height: 35px"
                                                        class="TextBoxes" disabled="disabled" onclick="this.select();" onfocus="this.select();"></textarea>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDescription_Corporations" runat="server" CssClass="WhiteLabel"
                                                        Text=": توضیحات" meta:resourcekey="lblDescription_Corporations"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <textarea id="txtDescription_Corporations" cols="20" rows="7" style="width: 97%;
                                                        height: 35px" class="TextBoxes" disabled="disabled" onclick="this.select();"
                                                        onfocus="this.select();"></textarea>
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
    <asp:HiddenField runat="server" ID="hfheader_Corporations_Corporations" meta:resourcekey="hfheader_Corporations_Corporations" />
    <asp:HiddenField runat="server" ID="hfheader_tblCorporations_Corporations" meta:resourcekey="hfheader_tblCorporations_Corporations" />
    <asp:HiddenField runat="server" ID="hfView_Corporations" meta:resourcekey="hfView_Corporations" />
    <asp:HiddenField runat="server" ID="hfAdd_Corporations" meta:resourcekey="hfAdd_Corporations" />
    <asp:HiddenField runat="server" ID="hfEdit_Corporations" meta:resourcekey="hfEdit_Corporations" />
    <asp:HiddenField runat="server" ID="hfDelete_Corporations" meta:resourcekey="hfDelete_Corporations" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_Corporations" meta:resourcekey="hfDeleteMessage_Corporations" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Corporations" meta:resourcekey="hfCloseMessage_Corporations" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridCorporations_Corporations"
        meta:resourcekey="hfloadingPanel_GridCorporations_Corporations" />
    <asp:HiddenField runat="server" ID="hfErrorType_Corporations" meta:resourcekey="hfErrorType_Corporations" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Corporations" meta:resourcekey="hfConnectionError_Corporations" />
    </form>
</body>
</html>
