<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RulesGroup.aspx.cs" Inherits="RulesGroup" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="RulesGroupForm" runat="server" meta:resourcekey="RulesGroupForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 50%; font-family: Arial; font-size: small;" class="BoxStyle">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbRulesGroup" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbRulesGroup" runat="server" ClientSideCommand="tlbItemNew_TlbRulesGroup_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbRulesGroup"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbRulesGroup" runat="server" ClientSideCommand="tlbItemEdit_TlbRulesGroup_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbRulesGroup"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbRulesGroup" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemDelete_TlbRulesGroup_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemDelete_TlbRulesGroup" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemRulesView_TlbRulesGroup" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png" ImageWidth="16px"
                                        ClientSideCommand="tlbItemRulesView_TlbRulesGroup_onClick();" ItemType="Command"
                                        meta:resourcekey="tlbItemRulesView_TlbRulesGroup" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemLeaveBudget_TlbRulesGroup" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="LeaveBudget.png" ImageWidth="16px"
                                        ClientSideCommand="tlbItemLeaveBudget_TlbRulesGroup_onClick();" ItemType="Command"
                                        meta:resourcekey="tlbItemLeaveBudget_TlbRulesGroup" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemRuleGroupCopy_TlbRulesGroup" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="copy.png"
                                        ImageWidth="16px" ClientSideCommand="tlbItemRuleGroupCopy_TlbRulesGroup_onClick();"
                                        ItemType="Command" meta:resourcekey="tlbItemRuleGroupCopy_TlbRulesGroup" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbRulesGroup" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbRulesGroup" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbRulesGroup_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbRulesGroup" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbRulesGroup_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbRulesGroup"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbRulesGroup" runat="server" ClientSideCommand="tlbItemExit_TlbRulesGroup_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbRulesGroup"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_RulesGroup" class="ToolbarMode">
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
                                    <td id="header_RulesGroup_RulesGroup" class="HeaderLabel" style="width: 45%">
                                        Rules Group
                                    </td>
                                    <td id="loadingPanel_trvRulesGroup_RulesGroup" class="HeaderLabel" style="width: 50%">
                                    </td>
                                    <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbRefresh_trvRulesGroup_RulesGroup" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvRulesGroup_RulesGroup"
                                                    runat="server" ClientSideCommand="Refresh_trvRulesGroup_RulesGroup();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvRulesGroup_RulesGroup"
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
                            <ComponentArt:CallBack runat="server" ID="CallBack_trvRulesGroup_RulesGroup" OnCallback="CallBack_trvRulesGroup_RulesGroup_onCallBack">
                                <Content>
                                    <ComponentArt:TreeView ID="trvRulesGroup_RulesGroup" runat="server" BorderColor="Black"
                                        CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                        DefaultImageWidth="16" DragAndDropEnabled="true" EnableViewState="false" ExpandCollapseImageHeight="15"
                                        ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" FillContainer="false"
                                        Height="300" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true"
                                        LineImageHeight="20" LineImageWidth="19" meta:resourcekey="trvRulesGroup_RulesGroup"
                                        NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                        SelectedNodeCssClass="SelectedTreeNode" ShowLines="true">
                                        <ClientEvents>
                                            <NodeMouseDoubleClick EventHandler="trvRulesGroup_RulesGroup_onNodeMouseDoubleClick" />
                                            <Load EventHandler="trvRulesGroup_RulesGroup_onLoad" />
                                        </ClientEvents>
                                    </ComponentArt:TreeView>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_RulesGroup" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="trvRulesGroup_RulesGroup_onCallbackComplete" />
                                    <CallbackError EventHandler="trvRulesGroup_RulesGroup_onCallbackError" />
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
    <asp:HiddenField runat="server" ID="hfheader_RulesGroup_RulesGroup" meta:resourcekey="hfheader_RulesGroup_RulesGroup" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvRulesGroup_RulesGroup" meta:resourcekey="hfloadingPanel_trvRulesGroup_RulesGroup" />
    <asp:HiddenField runat="server" ID="hfView_RulesGroup" meta:resourcekey="hfView_RulesGroup" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_RulesGroup" meta:resourcekey="hfCloseMessage_RulesGroup" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_RulesGroup" meta:resourcekey="hfDeleteMessage_RulesGroup" />
    <asp:HiddenField runat="server" ID="hfErrorType_RulesGroup" meta:resourcekey="hfErrorType_RulesGroup" />
    <asp:HiddenField runat="server" ID="hfConnectionError_RulesGroup" meta:resourcekey="hfConnectionError_RulesGroup" />
    </form>
</body>
</html>
