<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterDataAccessLevels.aspx.cs"
    Inherits="MasterDataAccessLevels" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="MasterDataAccessLevelsForm" runat="server" meta:resourcekey="MasterDataAccessLevelsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
        <tr>
            <td>
                <ComponentArt:ToolBar ID="TlbMasterDataAccessLevels" runat="server" CssClass="toolbar"
                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                    UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbMasterDataAccessLevels" runat="server"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbMasterDataAccessLevels"
                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbMasterDataAccessLevels_onClick();" />
                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbMasterDataAccessLevels"
                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbMasterDataAccessLevels_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbMasterDataAccessLevels"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbMasterDataAccessLevels" runat="server"
                            ClientSideCommand="tlbItemExit_TlbMasterDataAccessLevels_onClick();" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbMasterDataAccessLevels" TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td style="width:60%">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 25%">
                            <asp:Label ID="lblDataAccessLevels_MasterDataAccessLevels" runat="server" Text=": سطوح دسترسی داده"
                                meta:resourcekey="lblDataAccessLevels_MasterDataAccessLevels"></asp:Label>
                        </td>
                        <td style="width: 50%">
                            <ComponentArt:CallBack ID="CallBack_cmbDataAccessLevels_MasterDataAccessLevels" runat="server"
                                Height="26" OnCallback="CallBack_cmbDataAccessLevels_MasterDataAccessLevels_onCallBack">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbDataAccessLevels_MasterDataAccessLevels" runat="server"
                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                        DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                        Style="width: 100%" TextBoxCssClass="comboTextBox" DropDownHeight="270" TextBoxEnabled="true">
                                        <ClientEvents>
                                            <Expand EventHandler="cmbDataAccessLevels_MasterDataAccessLevels_onExpand" />
                                            <Collapse EventHandler="cmbDataAccessLevels_MasterDataAccessLevels_onCollapse" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField ID="ErrorHiddenField_DataAccessLevels" runat="server" />
                                </Content>
                                <ClientEvents>
                                    <BeforeCallback EventHandler="cmbDataAccessLevels_MasterDataAccessLevels_onBeforeCallback" />
                                    <CallbackComplete EventHandler="cmbDataAccessLevels_MasterDataAccessLevels_onCallbackComplete" />
                                    <CallbackError EventHandler="cmbDataAccessLevels_MasterDataAccessLevels_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                        <td style="width: 25%">
                            <ComponentArt:ToolBar ID="TlbSettings_MasterDataAccessLevels" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSettings_TlbSettings_MasterDataAccessLevels"
                                        runat="server" ClientSideCommand="tlbItemSettings_TlbSettings_MasterDataAccessLevels_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="regulation.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSettings_TlbSettings_MasterDataAccessLevels"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
                        </td>
                        <td id="tdUserCount_MasterDataAccessLevels">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <iframe id="ContainerIFrame_MasterDataAccessLevels" style="width: 100%; height: 450px;">
                </iframe>
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
    <asp:HiddenField runat="server" ID="hfTitle_DialogMasterDataAccessLevels" meta:resourcekey="hfTitle_DialogMasterDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_MasterDataAccessLevels" meta:resourcekey="hfcmbAlarm_MasterDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfErrorType_MasterDataAccessLevels" meta:resourcekey="hfErrorType_MasterDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfConnectionError_MasterDataAccessLevels" meta:resourcekey="hfConnectionError_MasterDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_MasterDataAccessLevels" meta:resourcekey="hfCloseMessage_MasterDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfUserCount_MasterDataAccessLevels" meta:resourcekey="hfUserCount_MasterDataAccessLevels"/>
    </form>
</body>
</html>
