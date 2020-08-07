<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Roles.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.Roles" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="RolesForm" runat="server" meta:resourcekey="RolesForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 90%; height: 400px; font-family: Arial; font-size: small;">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbRoles" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbRoles" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ClientSideCommand="tlbItemNew_TlbRoles_onClick();"
                                        ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbRoles"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbRoles" runat="server" ClientSideCommand="tlbItemEdit_TlbRoles_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbRoles"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbRoles" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ClientSideCommand="tlbItemDelete_TlbRoles_onClick();"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDelete_TlbRoles"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbRoles" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbRoles" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbRoles_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbRoles" runat="server" ClientSideCommand="tlbItemSave_TlbRoles_onClick();"
                                        Enabled="false" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                        ImageUrl="save_silver.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbRoles"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbRoles" runat="server" Enabled="false"
                                        ClientSideCommand="tlbItemCancel_TlbRoles_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemCancel_TlbRoles" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemUserInterfaceAccessLevels_TlbRoles" runat="server"
                                        ClientSideCommand="tlbItemUserInterfaceAccessLevels_TlbRoles_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="access.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemUserInterfaceAccessLevels_TlbRoles"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbRoles" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbRoles_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbRoles" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbRoles" runat="server" ClientSideCommand="tlbItemExit_TlbRoles_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbRoles"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_RolesForm" class="ToolbarMode" style="width: 40%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_Roles_Roles" class="HeaderLabel" style="width: 50%">
                                                    Roles
                                                </td>
                                                <td id="loadingPanel_trvRoles_Roles" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td2" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_trvRoles_Roles" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvRoles_Roles" runat="server"
                                                                ClientSideCommand="Refresh_trvRoles_Roles();" DropDownImageHeight="16px" DropDownImageWidth="16px"
                                                                ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command"
                                                                meta:resourcekey="tlbItemRefresh_TlbRefresh_trvRoles_Roles" TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_trvRoles_Roles" OnCallback="CallBack_trvRoles_Roles_onCallBack">
                                            <Content>
                                                <ComponentArt:TreeView ID="trvRoles_Roles" runat="server" ExpandNodeOnSelect="true"
                                                    CollapseNodeOnSelect="false" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                                    DefaultImageHeight="16" HighlightSelectedPath="true" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                    EnableViewState="false" ExpandCollapseImageHeight="15" LoadingFeedbackText="loading......."
                                                    ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" FillContainer="false"
                                                    ForceHighlightedNodeID="true" Height="330" HoverNodeCssClass="HoverNestingTreeNode"
                                                    ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19"
                                                    NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                    SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" meta:resourcekey="trvRoles_Roles"
                                                    BorderColor="Black">
                                                    <ClientEvents>
                                                        <NodeSelect EventHandler="trvRoles_Roles_onNodeSelect" />
                                                        <Load EventHandler="trvRoles_Roles_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:TreeView>
                                                <asp:HiddenField ID="ErrorHiddenField_Roles" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_trvRoles_Roles_CallbackComplete" />
                                                <CallbackError EventHandler="CallBack_trvRoles_Roles_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="middle" align="center">
                            <table style="width: 80%;" class="BoxStyle" id="tblRoles_Roles">
                                <tr id="Tr1" runat="server" meta:resourcekey="AlignObj">
                                    <td class="DetailsBoxHeaderStyle">
                                        <div id="header_RoleDetails_Roles" runat="server" class="BoxContainerHeader" meta:resourcekey="AlignObj"
                                            style="color: White; width: 100%; height: 100%">
                                            Role Details</div>
                                    </td>
                                </tr>
                                <tr id="Tr3" runat="server" meta:resourcekey="AlignObj">
                                    <td id="Td1" runat="server">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblRoleCode_Roles" runat="server" meta:resourcekey="lblRoleCode_Roles"
                                                        Text=": کد نقش" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtRoleCode_Roles"
                                                        disabled="disabled" onfocus="this.select();" onclick="this.select();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblRoleName_Roles" runat="server" meta:resourcekey="lblRoleName_Roles"
                                                        Text=": نام نقش" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtRoleName_Roles"
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
            </td>
        </tr>
        <tr>
            <td>
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
    <asp:HiddenField runat="server" ID="hfheader_Roles_Roles" meta:resourcekey="hfheader_Roles_Roles" />
    <asp:HiddenField runat="server" ID="hfheader_RoleDetails_Roles" meta:resourcekey="hfheader_RoleDetails_Roles" />
    <asp:HiddenField runat="server" ID="hfView_Roles" meta:resourcekey="hfView_Roles" />
    <asp:HiddenField runat="server" ID="hfAdd_Roles" meta:resourcekey="hfAdd_Roles" />
    <asp:HiddenField runat="server" ID="hfEdit_Roles" meta:resourcekey="hfEdit_Roles" />
    <asp:HiddenField runat="server" ID="hfDelete_Roles" meta:resourcekey="hfDelete_Roles" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_Roles" meta:resourcekey="hfDeleteMessage_Roles" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Roles" meta:resourcekey="hfCloseMessage_Roles" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvRoles_Roles" meta:resourcekey="hfloadingPanel_trvRoles_Roles" />
    <asp:HiddenField runat="server" ID="hfAccessor_trvRoles" />
    <asp:HiddenField runat="server" ID="hfErrorType_Roles" meta:resourcekey="hfErrorType_Roles" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Roles" meta:resourcekey="hfConnectionError_Roles" />
    </form>
</body>
</html>
