<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainView.aspx.cs" Inherits="MainView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/toolbar.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="MainViewForm" runat="server" meta:resourcekey="MainViewForm">
    <table id="Mastertbl_MainView" style="width: 100%; height: 400px; font-family: Arial;
        font-size: small;" class="BoxStyle">
        <tr>
            <td style="width: 50%" valign="top">
                <table style="width: 100%;" class="MainViewPartStyle">
                    <tr>
                        <td>
                            <table style="width: 100%;" class="ContainerHeader">
                                <tr>
                                    <td valign="top">
                                        <div id="header_MainViewPart1_MainView" runat="server" class="HeaderLabel" meta:resourcekey="AlignObj"
                                            style="color: White; width: 100%; height: 100%">
                                        </div>
                                    </td>
                                    <td style="width: 3%; cursor: pointer">
                                        <ComponentArt:ToolBar ID="TlbMaximize_MainViewPart1_MainView" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemMaximize_TlbMaximize_MainViewPart1_MainView"
                                                    runat="server" ClientSideCommand="Maximize_MainViewPart1_MainView();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="maximizeIcon.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemMaximize_TlbMaximize_MainViewPart1_MainView"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td style="width: 3%; cursor: pointer">
                                        <ComponentArt:ToolBar ID="TlbRefresh_MainViewPart1_MainView" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_MainViewPart1_MainView" runat="server"
                                                    ClientSideCommand="Refresh_MainViewPart1_MainView();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_MainViewPart1_MainView"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <iframe id="MainViewPart1_MainView" allowtransparency="true" src="about:blank" frameborder="0"
                                class="MainViewPart1_iFrame" name="MainViewPart1_MainView"></iframe>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table style="width: 100%;" class="MainViewPartStyle">
                    <tr>
                        <td>
                            <table style="width: 100%;" class="ContainerHeader">
                                <tr>
                                    <td valign="top">
                                        <div id="header_MainViewPart2_MainView" runat="server" class="HeaderLabel" meta:resourcekey="AlignObj"
                                            style="color: White; width: 100%; height: 100%">
                                        </div>
                                    </td>
                                    <td style="width: 3%; cursor: pointer">
                                        <ComponentArt:ToolBar ID="TlbMaximize_MainViewPart2_MainView" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemMaximize_TlbMaximize_MainViewPart2_MainView"
                                                    runat="server" ClientSideCommand="Maximize_MainViewPart2_MainView();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="maximizeIcon.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemMaximize_TlbMaximize_MainViewPart2_MainView"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td style="width: 3%; cursor: pointer">
                                        <ComponentArt:ToolBar ID="TlbRefresh_MainViewPart2_MainView" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_MainViewPart2_MainView" runat="server"
                                                    ClientSideCommand="Refresh_MainViewPart2_MainView();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_MainViewPart2_MainView"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <iframe id="MainViewPart2_MainView" allowtransparency="true" src="about:blank" frameborder="0"
                                class="MainViewPart2_iFrame" name="MainViewPart2_MainView"></iframe>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table style="width: 100%;" class="MainViewPartStyle">
                    <tr>
                        <td>
                            <table style="width: 100%;" class="ContainerHeader">
                                <tr>
                                    <td valign="top">
                                        <div id="header_MainViewPart3_MainView" runat="server" class="HeaderLabel" meta:resourcekey="AlignObj"
                                            style="color: White; width: 100%; height: 100%">
                                        </div>
                                    </td>
                                    <td style="width: 3%; cursor: pointer">
                                        <ComponentArt:ToolBar ID="TlbMaximize_MainViewPart3_MainView" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemMaximize_TlbMaximize_MainViewPart3_MainView"
                                                    runat="server" ClientSideCommand="Maximize_MainViewPart3_MainView();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="maximizeIcon.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemMaximize_TlbMaximize_MainViewPart3_MainView"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td style="width: 3%; cursor: pointer">
                                        <ComponentArt:ToolBar ID="TlbRefresh_MainViewPart3_MainView" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_MainViewPart3_MainView" runat="server"
                                                    ClientSideCommand="Refresh_MainViewPart3_MainView();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_MainViewPart3_MainView"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <iframe id="MainViewPart3_MainView" allowtransparency="true" src="about:blank" frameborder="0"
                                class="MainViewPart3_iFrame" name="MainViewPart3_MainView"></iframe>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table style="width: 100%;" class="MainViewPartStyle">
                    <tr>
                        <td>
                            <table style="width: 100%;" class="ContainerHeader">
                                <tr>
                                    <td valign="top">
                                        <div id="header_MainViewPart4_MainView" runat="server" class="HeaderLabel" meta:resourcekey="AlignObj"
                                            style="color: White; width: 100%; height: 100%">
                                        </div>
                                    </td>
                                    <td style="width: 3%; cursor: pointer">
                                        <ComponentArt:ToolBar ID="TlbMaximize_MainViewPart4_MainView" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemMaximize_TlbMaximize_MainViewPart4_MainView"
                                                    runat="server" ClientSideCommand="Maximize_MainViewPart4_MainView();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="maximizeIcon.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemMaximize_TlbMaximize_MainViewPart4_MainView"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td style="width: 3%; cursor: pointer">
                                        <ComponentArt:ToolBar ID="TlbRefresh_MainViewPart4_MainView" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_MainViewPart4_MainView" runat="server"
                                                    ClientSideCommand="Refresh_MainViewPart4_MainView();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_MainViewPart4_MainView"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <iframe id="MainViewPart4_MainView" allowtransparency="true" src="about:blank" frameborder="0"
                                class="MainViewPart4_iFrame" name="MainViewPart4_MainView"></iframe>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogMainViewMaximizedPart"
        HeaderClientTemplateId="DialogMainViewMaximizedPartheader" FooterClientTemplateId="DialogMainViewMaximizedPartfooter"
        runat="server">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogMainViewMaximizedPartheader">
                <table id="tbl_DialogMainViewMaximizedPartheader" style="width: 730px;" cellpadding="0"
                    cellspacing="0" border="0" onmousedown="DialogMainViewMaximizedPart.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogMainViewMaximizedPart_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogMainViewMaximizedPart" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold;">
                                    </td>
                                    <td id="CloseButton_DialogMainViewMaximizedPart" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('MainViewMaximizedPartIFrame_MainView').src = 'about:blank'; DialogMainViewMaximizedPart.Close('cancelled');" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogMainViewMaximizedPart_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogMainViewMaximizedPartfooter">
                <table id="tbl_DialogMainViewMaximizedPartfooter" style="width: 730px" cellpadding="0"
                    cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogMainViewMaximizedPart_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogMainViewMaximizedPart_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <Content>
            <table style="width: 100%;" class="BoxStyle">
                <tr>
                    <td>
                        <iframe id="MainViewMaximizedPartIFrame_MainView" allowtransparency="true" src="about:blank"
                            frameborder="0" class="MainViewMaximizedPart_iFrame" name="MainViewMaximizedPartIFrame_MainView">
                        </iframe>
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogMainViewMaximizedPart_onShow" />
        </ClientEvents>
    </ComponentArt:Dialog>
    </form>
</body>
</html>
