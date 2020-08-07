<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterUnderManagementPersonnelExeptionAccessView.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.MasterUnderManagementPersonnelExeptionAccessView" %>

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
    <form id="MasterUnderManagementPersonnelExeptionAccessViewForm" runat="server" meta:resourcekey="MasterUnderManagementPersonnelExeptionAccessViewForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; background-image: url('Images/Ghadir/bg-body.jpg'); font-family: Arial;
        font-size: small">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr align="center">
                        <td>
                            <asp:Label ID="lblManager_MasterUnderManagementPersonnelExeptionAccessView" runat="server"
                                Text=": مدیر" meta:resourcekey="lblManager_MasterUnderManagementPersonnelExeptionAccessView"
                                CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblAccessGroup_MasterUnderManagementPersonnelExeptionAccessView" runat="server"
                                Text=": گروه دسترسی" meta:resourcekey="lblAccessGroup_MasterUnderManagementPersonnelExeptionAccessView"
                                CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <input id="txtManager_MasterUnderManagementPersonnelExeptionAccessView" type="text"
                                disabled="disabled" style="background-color: #DCDCDC" />
                        </td>
                        <td>
                            <input id="txtAccessGroup_MasterUnderManagementPersonnelExeptionAccessView" type="text"
                                disabled="disabled" style="background-color: #DCDCDC" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="background-image: url('Images/Ghadir/bg-body.jpg'); background-repeat: repeat;
                    border: outset 1px black; width: 100%">
                    <tr>
                        <td id="header_BoxUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView"
                            style="color: White">
                            استثنائات
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <iframe allowtransparency="true" runat="server" id="MasterUnderManagementPersonnelExeptionAccessView_iFrame"
                                src="UnderManagementPersonnelExeptionAccessView.aspx" class="MasterUnderManagementPersonnelExeptionAccessView_iFrame"
                                frameborder="0" align="middle" name="I1"></iframe>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table style="width: 95%;">
                                <tr>
                                    <td runat="server" meta:resourcekey="AlignObj" style="width: 85%">
                                        <ComponentArt:ToolBar ID="TlbPaging_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView"
                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView"
                                                    runat="server" ClientSideCommand="" DropDownImageHeight="16px" DropDownImageWidth="16px"
                                                    Enabled="true" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command"
                                                    meta:resourcekey="tlbItemRefresh_TlbPaging_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView"
                                                    runat="server" ClientSideCommand="" DropDownImageHeight="16px" DropDownImageWidth="16px"
                                                    Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView"
                                                    runat="server" ClientSideCommand="SetPageIndex_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView('1');"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView"
                                                    runat="server" ClientSideCommand="SetPageIndex_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView('0');"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView"
                                                    runat="server" ClientSideCommand="" DropDownImageHeight="16px" DropDownImageWidth="16px"
                                                    Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="footer_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView"
                                        style="width: 15%" runat="server" meta:resourcekey="InverseAlignObj">
                                        Page 0 of 0
                                    </td>
                                </tr>
                            </table>
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
    </form>
</body>
</html>
