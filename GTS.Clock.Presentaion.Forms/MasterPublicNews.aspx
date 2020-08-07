<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterPublicNews.aspx.cs"
    Inherits="MasterPublicNews" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="MasterPublicNewsForm" runat="server" meta:resourcekey="MasterPublicNewsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; height: 400px; font-family: Arial; font-size: small">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%">
                            <ComponentArt:ToolBar ID="TlbMasterPublicNewsIntroduction" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbMasterPublicNewsIntroduction" runat="server"
                                        ClientSideCommand="tlbItemNew_TlbMasterPublicNews_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemNew_TlbMasterPublicNewsIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbMasterPublicNewsIntroduction" runat="server"
                                        ClientSideCommand="tlbItemEdit_TlbMasterPublicNews_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemEdit_TlbMasterPublicNewsIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbMasterPublicNewsIntroduction" runat="server"
                                        ClientSideCommand="tlbItemDelete_TlbMasterPublicNews_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemDelete_TlbMasterPublicNewsIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbMasterPublicNewsIntroduction" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbMasterPublicNewsIntroduction"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbMasterPublicNewsIntroduction_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbMasterPublicNewsIntroduction" runat="server"
                                        ClientSideCommand="tlbItemSave_TlbMasterPublicNews_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbMasterPublicNewsIntroduction"
                                        TextImageSpacing="5" Enabled="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbMasterPublicNewsIntroduction" runat="server"
                                        Enabled="false" ClientSideCommand="tlbItemCancel_TlbMasterPublicNews_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbMasterPublicNewsIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbMasterPublicNewsIntroduction"
                                        runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbMasterPublicNewsIntroduction_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbMasterPublicNewsIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TTlbMasterPublicNewsIntroduction" runat="server"
                                        ClientSideCommand="tlbItemExit_TlbMasterPublicNews_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbMasterPublicNewsIntroduction"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_MasterPublicNews" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="70%">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td style="width: 60%">
                            <table style="width: 100%" class="BoxStyle">
                                <tr>
                                    <td style="width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_MasterPublicNews_MasterPublicNews" class="HeaderLabel" style="width: 50%">
                                                    Master Public News
                                                </td>
                                                <td id="loadingPanel_GridMasterPublicNews_MasterPublicNews" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridMasterPublicNews_MasterPublicNews" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridMasterPublicNews_MasterPublicNews"
                                                                runat="server" ClientSideCommand="Refresh_GridMasterPublicNews_MasterPublicNews();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridMasterPublicNews_MasterPublicNews"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridMasterPublicNews_MasterPublicNews"
                                            OnCallback="CallBack_GridMasterPublicNews_MasterPublicNews_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridMasterPublicNews_MasterPublicNews" runat="server"
                                                    CssClass="Grid" EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter"
                                                    ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                                    PageSize="14" RunningMode="Client" SearchTextCssClass="GridHeaderText" Width="100%"
                                                    AllowMultipleSelect="false" ShowFooter="false" AllowColumnResizing="false" ScrollBar="On"
                                                    ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16"
                                                    ScrollImagesFolderUrl="images/Grid/scroller/" ScrollButtonWidth="16" ScrollButtonHeight="17"
                                                    ScrollBarCssClass="ScrollBar" ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                            DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                            RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                            SortImageWidth="9">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Active" DefaultSortDirection="Descending"
                                                                    HeadingText="فعال" ColumnType="CheckBox" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMasterPublicNewsActive_GridMasterPublicNews" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Subject" DefaultSortDirection="Descending"
                                                                    HeadingText="عنوان" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMasterPublicNewsSubject_GridMasterPublicNews" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Message" DefaultSortDirection="Descending"
                                                                    HeadingText="متن پیام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMasterPublicNewsMessage_GridMasterPublicNews" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <ItemSelect EventHandler="GridMasterPublicNews_MasterPublicNews_onItemSelect" />
                                                        <Load EventHandler="GridMasterPublicNews_MasterPublicNews_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField ID="ErrorHiddenField_MasterPublicNews" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridMasterPublicNews_MasterPublicNews_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridMasterPublicNews_MasterPublicNews_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 40%" align="center">
                            <table style="width: 100%;" id="tblMasterPublicNews_MasterPublicNewsIntroduction"
                                class="BoxStyle">
                                <tr id="Tr1" runat="server" meta:resourcekey="AlignObj">
                                    <td class="DetailsBoxHeaderStyle">
                                        <div id="header_tblMasterPublicNewsDetails_MasterPublicNewsIntroduction" runat="server"
                                            class="BoxContainerHeader" meta:resourcekey="AlignObj" style="width: 100%; height: 100%">
                                            Master Public News Details</div>
                                    </td>
                                </tr>
                                <tr id="Tr2" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <input type="checkbox" runat="server" class="checkboxes" meta:resourcekey="chbMasterPublicNewsActive_MasterPublicNewsIntroduction"
                                                        id="chbMasterPublicNewsActive_MasterPublicNewsIntroduction" disabled="disabled"
                                                        onfocus="this.select();" onclick="this.select();" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMasterPublicNewsActive_MasterPublicNewsIntroduction" runat="server"
                                                        meta:resourcekey="lblMasterPublicNewsActive_MasterPublicNewsIntroduction" Text="فعال"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="Tr3" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                    </td>
                                </tr>
                                <tr id="Tr4" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblMasterPublicNewsSubject_MasterPublicNewsIntroduction" runat="server"
                                            meta:resourcekey="lblMasterPublicNewsSubject_MasterPublicNewsIntroduction" Text=":عنوان"
                                            CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr5" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtMasterPublicNewsSubject_MasterPublicNewsIntroduction"
                                            disabled="disabled" onfocus="this.select();" onclick="this.select();" />
                                    </td>
                                </tr>
                                <tr id="Tr6" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblMasterPublicNewsMessage_MasterPublicNewsIntroduction" runat="server"
                                            meta:resourcekey="lblMasterPublicNewsMessage_MasterPublicNewsIntroduction" Text=": متن پیام"
                                            CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr7" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <textarea id="txtMasterPublicNewsMessage_MasterPublicNewsIntroduction" cols="20"
                                            rows="6" style="width: 98%; height: 80px;" disabled="disabled" class="TextBoxes"
                                            onfocus="this.select();" onclick="this.select();"></textarea>
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
    <asp:HiddenField runat="server" ID="hfheader_tblMasterPublicNewsDetails_MasterPublicNewsIntroduction"
        meta:resourcekey="hfheader_tblMasterPublicNewsDetails_MasterPublicNewsIntroduction" />
    <asp:HiddenField runat="server" ID="hfheader_MasterPublicNews_MasterPublicNews" meta:resourcekey="hfheader_MasterPublicNews_MasterPublicNews" />
    <asp:HiddenField runat="server" ID="hfView_MasterPublicNews" meta:resourcekey="hfView_MasterPublicNews" />
    <asp:HiddenField runat="server" ID="hfAdd_MasterPublicNews" meta:resourcekey="hfAdd_MasterPublicNews" />
    <asp:HiddenField runat="server" ID="hfEdit_MasterPublicNews" meta:resourcekey="hfEdit_MasterPublicNews" />
    <asp:HiddenField runat="server" ID="hfDelete_MasterPublicNews" meta:resourcekey="hfDelete_MasterPublicNews" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_MasterPublicNews" meta:resourcekey="hfDeleteMessage_MasterPublicNews" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_MasterPublicNews" meta:resourcekey="hfCloseMessage_MasterPublicNews" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridMasterPublicNews_MasterPublicNews"
        meta:resourcekey="hfloadingPanel_GridMasterPublicNews_MasterPublicNews" />
    <asp:HiddenField runat="server" ID="hfErrorType_MasterPublicNews" meta:resourcekey="hfErrorType_MasterPublicNews" />
    <asp:HiddenField runat="server" ID="hfConnectionError_MasterPublicNews" meta:resourcekey="hfConnectionError_MasterPublicNews" />
    </form>
</body>
</html>
