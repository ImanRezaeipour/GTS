<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultiLevelsDataAccessLevels.aspx.cs"
    Inherits="MultiLevelsDataAccessLevels" %>

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
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="MultiLevelsDataAccessLevelsForm" runat="server" meta:resourcekey="MultiLevelsDataAccessLevelsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
        <tr>
            <td style="width: 48%" class="BoxStyle">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td id="header_DataAccessLevelsSource_MultiLevelsDataAccessLevels" style="width: 40%"
                                        class="HeaderLabel">
                                    </td>
                                    <td id="loadingPanel_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels" style="width: 24%"
                                        class="HeaderLabel">
                                        &nbsp;
                                    </td>
                                    <td style="width: 32%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 5%">
                                                    <input id="chbSelectAll_MultiLevelsDataAccessLevels" type="checkbox" class="WhiteLabel" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSelectAll_MultiLevelsDataAccessLevels" runat="server" Text="همه"
                                                        meta:resourcekey="lblSelectAll_MultiLevelsDataAccessLevels"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td id="Td1" runat="server" style="width: 4%" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbRefresh_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels"
                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels"
                                                    runat="server" ClientSideCommand="Refresh_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels"
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
                            <ComponentArt:CallBack ID="CallBack_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels"
                                runat="server" OnCallback="CallBack_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels_onCallBack">
                                <Content>
                                    <ComponentArt:TreeView ID="trvDataAccessLevelsSource_MultiLevelsDataAccessLevels"
                                        runat="server" ExpandNodeOnSelect="true" CollapseNodeOnSelect="false" CollapseImageUrl="images/TreeView/exp.gif"
                                        CssClass="TreeView" DefaultImageHeight="16" HighlightSelectedPath="true" DefaultImageWidth="16"
                                        DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15"
                                        LoadingFeedbackText="loading......." ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif"
                                        FillContainer="false" ForceHighlightedNodeID="true" Height="385" HoverNodeCssClass="HoverNestingTreeNode"
                                        ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19"
                                        NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                        SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" meta:resourcekey="trvDataAccessLevelsSource_MultiLevelsDataAccessLevels"
                                        BorderColor="Black">
                                        <ClientEvents>
                                            <Load EventHandler="trvDataAccessLevelsSource_MultiLevelsDataAccessLevels_onLoad" />
                                            <NodeBeforeExpand EventHandler="trvDataAccessLevelsSource_MultiLevelsDataAccessLevels_onNodeBeforeExpand" />
                                            <CallbackComplete EventHandler="trvDataAccessLevelsSource_MultiLevelsDataAccessLevels_onCallbackComplete" />
                                        </ClientEvents>
                                    </ComponentArt:TreeView>
                                    <asp:HiddenField ID="ErrorHiddenField_DataAccessLevelsSource" runat="server" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="center" valign="middle" style="width: 4%">
                <ComponentArt:ToolBar ID="TlbInterAction_MultiLevelsDataAccessLevels" runat="server"
                    CssClass="verticaltoolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" Orientation="Vertical" UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemAdd_TlbInterAction_MultiLevelsDataAccessLevels"
                            runat="server" ClientSideCommand="tlbItemAdd_TlbInterAction_MultiLevelsDataAccessLevels_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdd_TlbInterAction_MultiLevelsDataAccessLevels"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbInterAction_MultiLevelsDataAccessLevels"
                            runat="server" ClientSideCommand="tlbItemDelete_TlbInterAction_MultiLevelsDataAccessLevels_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDelete_TlbInterAction_MultiLevelsDataAccessLevels"
                            TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
            <td class="BoxStyle">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td id="header_DataAccessLevelsTarget_MultiLevelsDataAccessLevels" style="width: 60%"
                                        class="HeaderLabel">
                                        &nbsp;
                                    </td>
                                    <td id="loadingPanel_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels" style="width: 36%"
                                        class="HeaderLabel">
                                        &nbsp;
                                    </td>
                                    <td style="width: 4%" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbRefresh_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels"
                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels"
                                                    runat="server" ClientSideCommand="Refresh_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels"
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
                            <ComponentArt:CallBack ID="CallBack_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels"
                                runat="server" OnCallback="CallBack_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onCallBack">
                                <Content>
                                    <ComponentArt:TreeView ID="trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels"
                                        runat="server" ExpandNodeOnSelect="true" CollapseNodeOnSelect="false" CollapseImageUrl="images/TreeView/exp.gif"
                                        CssClass="TreeView" DefaultImageHeight="16" HighlightSelectedPath="true" DefaultImageWidth="16"
                                        DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15"
                                        LoadingFeedbackText="loading......." ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif"
                                        FillContainer="false" ForceHighlightedNodeID="true" Height="385" HoverNodeCssClass="HoverNestingTreeNode"
                                        ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19"
                                        NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                        SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" meta:resourcekey="trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels"
                                        BorderColor="Black">
                                        <ClientEvents>
                                            <Load EventHandler="trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onLoad" />
                                            <NodeBeforeExpand EventHandler="trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onNodeBeforeExpand" />
                                            <CallbackComplete EventHandler="trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onCallbackComplete" />
                                            <NodeSelect EventHandler="trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onNodeSelect" />
                                        </ClientEvents>
                                    </ComponentArt:TreeView>
                                    <asp:HiddenField ID="ErrorHiddenField_DataAccessLevelsTarget" runat="server" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
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
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels"
        meta:resourcekey="hfloadingPanel_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels"
        meta:resourcekey="hfloadingPanel_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfErrorType_MultiLevelsDataAccessLevels" meta:resourcekey="hfErrorType_MultiLevelsDataAccessLevels" />
    <asp:HiddenField runat="server" ID="hfConnectionError_MultiLevelsDataAccessLevels"
        meta:resourcekey="hfConnectionError_MultiLevelsDataAccessLevels" />
    </form>
</body>
</html>
