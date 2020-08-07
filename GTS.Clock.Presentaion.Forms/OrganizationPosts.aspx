<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrganizationPosts.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.OrganizationPosts" %>

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
    <form id="OrganizationPostsForm" runat="server" meta:resourcekey="OrganizationPostsForm">
    <%--<asp:ScriptManager ID="ScriptManager" runat="server">
        <Services>
            <asp:ServiceReference Path="~/Service/OrganizationPostsServive.svc/json" />
        </Services>
    </asp:ScriptManager>--%>
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
                            <ComponentArt:ToolBar ID="TlbPosts" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbPosts" runat="server" ClientSideCommand="tlbItemNew_TlbPosts_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbPosts" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbPosts" runat="server" ClientSideCommand="tlbItemEdit_TlbPosts_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbPosts"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbPosts" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemDelete_TlbPosts" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemDelete_TlbPosts_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbPosts" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbPosts" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbPosts_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbPosts" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbPosts" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemSave_TlbPosts_onClick();" Enabled="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbPosts" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemCancel_TlbPosts" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemCancel_TlbPosts_onClick();" Enabled="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbPosts" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbPosts_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbPosts" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPosts" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemExit_TlbPosts_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbPosts"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_Posts" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%" valign="top">
                            <table style="width: 100%" class="BoxStyle">
                                <tr>
                                    <td style="color: White; width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_Posts_Posts" class="HeaderLabel" style="width: 45%">
                                                    Organization Posts
                                                </td>
                                                <td id="loadingPanel_trvPosts_Post" class="HeaderLabel" style="width: 50%">
                                                </td>
                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_trvPosts_Post" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvPosts_Post" runat="server"
                                                                ClientSideCommand="Refresh_trvPosts_Post();" DropDownImageHeight="16px" DropDownImageWidth="16px"
                                                                ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command"
                                                                meta:resourcekey="tlbItemRefresh_TlbRefresh_trvPosts_Post" TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_trvPosts_Post" OnCallback="CallBack_trvPosts_Post_onCallBack">
                                            <Content>
                                                <ComponentArt:TreeView ID="trvPosts_Post" runat="server" ExpandNodeOnSelect="true"
                                                    CollapseNodeOnSelect="false" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                                    DefaultImageHeight="16" HighlightSelectedPath="true" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                    EnableViewState="true" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                    ExpandImageUrl="images/TreeView/col.gif" FillContainer="false" ForceHighlightedNodeID="true"
                                                    Height="330" HoverNodeCssClass="HoverNestingTreeNode" ItemSpacing="2" KeyboardEnabled="true"
                                                    LineImageHeight="20" LineImageWidth="19" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit"
                                                    LoadingFeedbackText="Loading" NodeIndent="17" NodeLabelPadding="3" meta:resourcekey="trvPosts_Post"
                                                    SelectedNodeCssClass="SelectedTreeNode" ShowLines="true">
                                                    <ClientEvents>
                                                        <NodeSelect EventHandler="trvPosts_Post_onNodeSelect" />
                                                        <Load EventHandler="trvPosts_Post_onLoad" />
                                                        <CallbackComplete EventHandler="trvPosts_Post_onCallbackComplete" />
                                                        <NodeBeforeExpand EventHandler="trvPosts_Post_onNodeBeforeExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:TreeView>
                                                <asp:HiddenField ID="ErrorHiddenField_Posts" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_trvPosts_Post_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_trvPosts_Post_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" valign="middle">
                            <table style="width: 100%;" id="tblOrgPosts_Posts" class="BoxStyle">
                                <tr id="Tr1" runat="server" meta:resourcekey="AlignObj">
                                    <td id="Td7" runat="server" valign="middle">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="DetailsBoxHeaderStyle">
                                                    <div id="header_tblPostsDetails_Posts" runat="server" meta:resourcekey="AlignObj"
                                                        style="color: White; width: 100%; height: 100%" class="BoxContainerHeader">
                                                        Organization Post Details</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblOrgPostCode_Posts" runat="server" meta:resourcekey="lblOrgPostCode_Posts"
                                                        Text=": کد پست" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtOrgPostCode_Posts"
                                                        onclick="this.select();" onfocus="this.select();" disabled="disabled" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblOrgPostName_Posts" runat="server" meta:resourcekey="lblOrgPostName_Posts"
                                                        Text=": نام پست" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtOrgPostName_Posts"
                                                        onclick="this.select();" onfocus="this.select();" disabled="disabled" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPersonnelName_Posts" runat="server" meta:resourcekey="lblPersonnelName_Posts"
                                                        Text=": نام و نام خانوادگی " CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtPersonnelName_Posts"
                                                        onclick="this.select();" onfocus="this.select();" readonly="readonly" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPersonnelCode_Posts" runat="server" meta:resourcekey="lblPersonnelCode_Posts"
                                                        Text=": شماره پرسنلی" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtPersonnelCode_Posts"
                                                        onclick="this.select();" onfocus="this.select();" readonly="readonly" /></td>
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
    <asp:HiddenField runat="server" ID="hfheader_Posts_Posts" meta:resourcekey="hfheader_Posts_Posts" />
    <asp:HiddenField runat="server" ID="hfheader_tblPostsDetails_Posts" meta:resourcekey="hfheader_tblPostsDetails_Posts" />
    <asp:HiddenField runat="server" ID="hfView_Posts" meta:resourcekey="hfView_Posts" />
    <asp:HiddenField runat="server" ID="hfAdd_Posts" meta:resourcekey="hfAdd_Posts" />
    <asp:HiddenField runat="server" ID="hfEdit_Posts" meta:resourcekey="hfEdit_Posts" />
    <asp:HiddenField runat="server" ID="hfDelete_Posts" meta:resourcekey="hfDelete_Posts" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_Posts" meta:resourcekey="hfDeleteMessage_Posts" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Posts" meta:resourcekey="hfCloseMessage_Posts" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvPosts_Post" meta:resourcekey="hfloadingPanel_trvPosts_Post" />
    <asp:HiddenField runat="server" ID="hfErrorType_Posts" meta:resourcekey="hfErrorType_Posts" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Posts" meta:resourcekey="hfConnectionError_Posts" />
    </form>
</body>
</html>
