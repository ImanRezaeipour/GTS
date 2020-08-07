<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInterfaceAccessLevels.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.UserInterfaceAccessLevels" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="FUA" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
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
    <form id="AccessLevelsForm" runat="server" meta:resourcekey="AccessLevelsForm" onsubmit="return false;">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="font-size: small; font-family: Arial;" width="100%" class="BoxStyle">
        <tr>
            <td>
                <ComponentArt:ToolBar ID="TlbAccessLevelsAsign" runat="server" CssClass="toolbar"
                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                    UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemEndorsement_TlbAccessLevelsAsign" runat="server"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEndorsement_TlbAccessLevelsAsign"
                            TextImageSpacing="5" ClientSideCommand="tlbItemEndorsement_TlbAccessLevelsAsign_onClick();" />
                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbAccessLevelsAsign" runat="server"
                            ClientSideCommand="tlbItemFormReconstruction_TlbAccessLevelsAsign_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbAccessLevelsAsign"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbAccessLevelsAsign" runat="server" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                            ClientSideCommand="tlbItemExit_TlbAccessLevelsAsign_onClick();" ItemType="Command"
                            meta:resourcekey="tlbItemExit_TlbAccessLevelsAsign" TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Label ID="lblUserRoleName_AccessLevelsAsign" runat="server" Text=": نام نقش کاربری"
                                meta:resourcekey="lblUserRoleName_AccessLevelsAsign" CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtUserRoleName_AccessLevelsAsign"
                                readonly="readonly" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="60%">
                <table style="width: 100%" class="BoxStyle">
                    <tr>
                        <td id="header_AccessLevelsAsign_AccessLevelsAsign" class="HeaderLabel" style="width: 50%">
                            Access Levels Asignment
                        </td>
                        <td id="loadingPanel_trvAccessLevelsAsign_AccessLevelsAsign" class="HeaderLabel"
                            style="width: 45%">
                        </td>
                        <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj" style="width: 5%">
                            <ComponentArt:ToolBar ID="TlbRefresh_trvAccessLevelsAsign_AccessLevelsAsign" runat="server"
                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvAccessLevelsAsign_AccessLevelsAsign"
                                        runat="server" ClientSideCommand="Refresh_trvAccessLevelsAsign_AccessLevelsAsign();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvAccessLevelsAsign_AccessLevelsAsign"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <ComponentArt:CallBack ID="CallBack_trvAccessLevelsAsign_AccessLevelsAsign" runat="server"
                                OnCallback="CallBack_trvAccessLevelsAsign_AccessLevelsAsign_onCallBack">
                                <Content>
                                    <ComponentArt:TreeView ID="trvAccessLevelsAsign_AccessLevelsAsign" runat="server"
                                        CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                        DefaultImageWidth="16" DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15"
                                        ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" Height="340px"
                                        HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20"
                                        LineImageWidth="19" NodeCssClass="TreeNode" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                        NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode"
                                        ShowLines="true" Width="99%" meta:resourcekey="trvAccessLevelsAsign_AccessLevelsAsign">
                                        <ClientEvents>
                                            <Load EventHandler="trvAccessLevelsAsign_AccessLevelsAsign_onLoad" />
                                            <NodeCheckChange EventHandler="trvAccessLevelsAsign_AccessLevelsAsign_onNodeCheckChange" />
                                        </ClientEvents>
                                    </ComponentArt:TreeView>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_AccessLevelsAsign" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_trvAccessLevelsAsign_AccessLevelsAsign_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_trvAccessLevelsAsign_AccessLevelsAsign_onCallbackError" />
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
    <asp:HiddenField runat="server" ID="hfTitle_DialogUserInterfaceAccessLevels" meta:resourcekey="hfTitle_DialogUserInterfaceAccessLevels" />
    <asp:HiddenField runat="server" ID="hfheader_AccessLevelsAsign_AccessLevelsAsign"
        meta:resourcekey="hfheader_AccessLevelsAsign_AccessLevelsAsign" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvAccessLevelsAsign_AccessLevelsAsign"
        meta:resourcekey="hfloadingPanel_trvAccessLevelsAsign_AccessLevelsAsign" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_AccessLevelsAsign" meta:resourcekey="hfCloseMessage_AccessLevelsAsign" />
    <asp:HiddenField runat="server" ID="hfErrorType_AccessLevelsAsign" meta:resourcekey="hfErrorType_AccessLevelsAsign" />
    <asp:HiddenField runat="server" ID="hfConnectionError_AccessLevelsAsign" meta:resourcekey="hfConnectionError_AccessLevelsAsign" />
    </form>
</body>
</html>
