<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UiValidation.aspx.cs" Inherits="UiValidation" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="UiValidationForm" runat="server" meta:resourcekey="UiValidationForm">
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
                            <ComponentArt:ToolBar ID="TlbUiValidationIntroduction" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbUiValidationIntroduction" runat="server"
                                        ClientSideCommand="tlbItemNew_TlbUiValidation_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemNew_TlbUiValidationIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbUiValidationIntroduction" runat="server"
                                        ClientSideCommand="tlbItemEdit_TlbUiValidation_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemEdit_TlbUiValidationIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbUiValidationIntroduction" runat="server"
                                        ClientSideCommand="tlbItemDelete_TlbUiValidation_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemDelete_TlbUiValidationIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSetLaw_TlbUiValidationIntroduction" runat="server"
                                        ClientSideCommand="tlbItemSetLaw_TlbUiValidation_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="regulation.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSetLaw_TlbUiValidationIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbUiValidationIntroduction" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbUiValidationIntroduction"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbUiValidationIntroduction_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbUiValidationIntroduction" runat="server"
                                        ClientSideCommand="tlbItemSave_TlbUiValidation_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbUiValidationIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbUiValidationIntroduction" runat="server"
                                        DropDownImageHeight="16px" ClientSideCommand="tlbItemCancel_TlbUiValidation_onClick();"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemCancel_TlbUiValidationIntroduction"
                                        TextImageSpacing="5" Visible="true" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbUiValidationIntroduction"
                                        runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbUiValidationIntroduction_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbUiValidationIntroduction"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbUiValidationIntroduction" runat="server"
                                        ClientSideCommand="tlbItemExit_TlbUiValidation_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbUiValidationIntroduction"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_UiValidation" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 60%">
                            <table style="width: 100%" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_UiValidation_UiValidation" class="HeaderLabel" style="width: 50%">
                                                    UiValidation
                                                </td>
                                                <td id="loadingPanel_GridUiValidation_UiValidation" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridUiValidation_UiValidation" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridUiValidation_UiValidation"
                                                                runat="server" ClientSideCommand="Refresh_GridUiValidation_UiValidation();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridUiValidation_UiValidation"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridUiValidation_UiValidation"
                                            OnCallback="CallBack_GridUiValidation_UiValidation_Callback">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridUiValidation_UiValidation" runat="server" CssClass="Grid"
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
                                                                    HeadingText="کد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnUiValidationCode_GridUiValidation"
                                                                    Width="30" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnUiValidationName_GridUiValidation"
                                                                    Width="200" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <ItemSelect EventHandler="GridUiValidation_UiValidation_onItemSelect" />
                                                        <Load EventHandler="GridUiValidation_UiValidation_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField ID="ErrorHiddenField_UiValidation" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridUiValidation_UiValidation_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridUiValidation_UiValidation_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%;" id="tblUiValidation_UiValidationIntroduction" class="BoxStyle">
                                <tr id="Tr1" runat="server" meta:resourcekey="AlignObj">
                                    <td class="DetailsBoxHeaderStyle">
                                        <div id="header_tblUiValidation_UiValidationIntroduction" runat="server" class="BoxContainerHeader"
                                            meta:resourcekey="AlignObj" style="width: 100%; height: 100%">
                                            UiValidation Details</div>
                                    </td>
                                </tr>
                                <tr id="Tr4" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblUiValidationCode_UiValidationIntroduction" runat="server" meta:resourcekey="lblUiValidationCode_UiValidationIntroduction"
                                            Text="کد :" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr5" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtUiValidationCode_UiValidationIntroduction"
                                            disabled="disabled" onfocus="this.select();" onclick="this.select();" />
                                    </td>
                                </tr>
                                <tr id="Tr6" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblUiValidationName_UiValidationIntroduction" runat="server" meta:resourcekey="lblUiValidationName_UiValidationIntroduction"
                                            Text="نام واسط کاربری :" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr7" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtUiValidationName_UiValidationIntroduction"
                                            disabled="disabled" onfocus="this.select();" onclick="this.select();" />
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
    <asp:HiddenField runat="server" ID="hfheader_tblUiValidationDetails_UiValidationIntroduction"
        meta:resourcekey="hfheader_tblUiValidationDetails_UiValidationIntroduction" />
    <asp:HiddenField runat="server" ID="hfheader_UiValidation_UiValidation" meta:resourcekey="hfheader_UiValidation_UiValidation" />
    <asp:HiddenField runat="server" ID="hfView_UiValidation" meta:resourcekey="hfView_UiValidation" />
    <asp:HiddenField runat="server" ID="hfAdd_UiValidation" meta:resourcekey="hfAdd_UiValidation" />
    <asp:HiddenField runat="server" ID="hfEdit_UiValidation" meta:resourcekey="hfEdit_UiValidation" />
    <asp:HiddenField runat="server" ID="hfDelete_UiValidation" meta:resourcekey="hfDelete_UiValidation" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_UiValidation" meta:resourcekey="hfDeleteMessage_UiValidation" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_UiValidation" meta:resourcekey="hfCloseMessage_UiValidation" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridUiValidation_UiValidation"
        meta:resourcekey="hfloadingPanel_GridUiValidation_UiValidation" />
    <asp:HiddenField runat="server" ID="hfErrorType_UiValidation" meta:resourcekey="hfErrorType_UiValidation" />
    <asp:HiddenField runat="server" ID="hfConnectionError_UiValidation" meta:resourcekey="hfConnectionError_UiValidation" />
    </form>
</body>
</html>
