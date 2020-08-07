<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnderManagementPersonnelExeptionAccessCreation.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.UnderManagementPersonnelExeptionAccessCreation" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/navStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/upload.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/hierarchicalGridStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="UnderManagementPersonnelExeptionAccessCreationForm" runat="server" meta:resourcekey="UnderManagementPersonnelExeptionAccessCreationForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small; background-image: url('Images/Ghadir/bg-body.jpg');
        background-repeat: repeat">
        <tr>
            <td>
                <ComponentArt:ToolBar ID="TlbUnderManagementPersonnelExeptionAccessCreation" runat="server"
                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbUnderManagementPersonnelExeptionAccessCreation"
                            runat="server" ClientSideCommand="" DropDownImageHeight="16px" DropDownImageWidth="16px"
                            Enabled="true" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px" ItemType="Command"
                            meta:resourcekey="tlbItemSave_TlbUnderManagementPersonnelExeptionAccessCreation"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbUnderManagementPersonnelExeptionAccessCreation" runat="server" ClientSideCommand=""
                            DropDownImageHeight="16px" ImageUrl="cancel.png" DropDownImageWidth="16px" Enabled="true"
                            ImageHeight="16px" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbUnderManagementPersonnelExeptionAccessCreation"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbUnderManagementPersonnelExeptionAccessCreation"
                            runat="server" ClientSideCommand="parent.DialogUnderManagementPersonnelExeptionAccessCreation.Close();"
                            DropDownImageHeight="16px" ImageUrl="exit.png" DropDownImageWidth="16px" Enabled="true"
                            ImageHeight="16px" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbUnderManagementPersonnelExeptionAccessCreation"
                            TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 48%">
                            <table style="width: 100%; background-image: url('Images/Ghadir/bg-body.jpg'); border: outset 1px black;
                                background-repeat: repeat">
                                <tr>
                                    <td id="header_UnderManagemetPersonnelBox_UnderManagementPersonnelExeptionAccessCreation"
                                        style="color: White; width: 100%">
                                        پرسنل تحت مدیریت
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ComponentArt:TreeView ID="trvUnderManagemetPersonnel_UnderManagementPersonnelExeptionAccessCreation"
                                            runat="server" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                            DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="true" EnableViewState="false"
                                            ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif"
                                            FillContainer="false" Height="375" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2"
                                            KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                            NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                            SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" SiteMapXmlFile="XML/treeData.xml"
                                            BorderColor="Black" meta:resourcekey="trvUnderManagemetPersonnel_UnderManagementPersonnelExeptionAccessCreation">
                                        </ComponentArt:TreeView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" valign="middle" style="width: 4%">
                            <ComponentArt:ToolBar ID="TlbAccessLevelView_UnderManagementPersonnelExeptionAccessCreation"
                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                Orientation="Vertical" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item"
                                DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px"
                                DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemAccessLevelView_TlbAccessLevelView_UnderManagementPersonnelExeptionAccessCreation"
                                        runat="server" ClientSideCommand="" DropDownImageHeight="16px" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="eyeglasses.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemAccessLevelView_TlbAccessLevelView_UnderManagementPersonnelExeptionAccessCreation"
                                        TextImageSpacing="5" Enabled="true" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td style="width: 48%">
                            <table style="width: 100%; background-image: url('Images/Ghadir/bg-body.jpg'); border: outset 1px black;
                                background-repeat: repeat">
                                <tr>
                                    <td id="header_AccessLevelBox_UnderManagementPersonnelExeptionAccessCreation" style="color: White;
                                        width: 100%">
                                        سطح دسترسی
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;">
                                        <ComponentArt:TreeView ID="trvAccessLevel_UnderManagementPersonnelExeptionAccessCreation"
                                            runat="server" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                            DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="true" EnableViewState="false"
                                            ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif"
                                            FillContainer="false" Height="375" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2"
                                            KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                            NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                            SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" SiteMapXmlFile="XML/treeData.xml"
                                            BorderColor="Black" meta:resourcekey="trvAccessLevel_UnderManagementPersonnelExeptionAccessCreation">
                                        </ComponentArt:TreeView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
