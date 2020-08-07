<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterManagers.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.MasterManagers" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/hierarchicalGridStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/siteMapStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body onload="MasterManagersForm_onPageLoad();">
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="MasterManagersForm" runat="server" meta:resourcekey="MasterManagersForm" onsubmit="return false;">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small">
        <tr>
            <td>
                <ComponentArt:ToolBar ID="TlbMasterManagers" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemWorkFlowView_TlbMasterManagers" runat="server"
                            ClientSideCommand="tlbItemWorkFlowView_TlbMasterManagers_onClick();" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="flow.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemWorkFlowView_TlbMasterManagers" TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbMasterManagers" runat="server" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbMasterManagers" TextImageSpacing="5"
                            ClientSideCommand="tlbItemHelp_TlbMasterManagers_onClick();" />
                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbMasterManagers" runat="server"
                            ClientSideCommand="tlbItemFormReconstruction_TlbMasterManagers_onClick();" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbMasterManagers"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbMasterManagers" runat="server" ClientSideCommand="tlbItemExit_TlbMasterManagers_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbMasterManagers"
                            TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
            <td id="ActionMode_MasterManagers" class="ToolbarMode">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div runat="server" meta:resourcekey="AlignObj" style="white; width: 100%" class="DropDownHeader">
                    <img id="imgbox_ManagersDetails_MasterManagers" runat="server" alt="" onclick="imgbox_ManagersDetails_MasterManagers_onClick();"
                        src="~/Images/Ghadir/arrowDown.jpg" />
                    <span id="header_ManagersDetails_MasterManagers">Managers Details</span>
                </div>
                <div id="box_ManagersDetails_MasterManagers" class="dhtmlgoodies_contentBox" style="width: 36.5%;">
                    <div id="subbox_ManagersDetails_MasterManagers" class="dhtmlgoodies_content">
                        <table style="width: 100%;" class="BoxStyle">
                            <tr>
                                <td>
                                    <table style="width: 100%; border: outset 1px black;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 5%">
                                                            <input id="rdbManagerFilter_MasterManagers" name="ManagersDeatails" type="radio"
                                                                runat="server" onclick="rdbManagerFilter_MasterManagers_onClick();" />
                                                        </td>
                                                        <td style="width: 95%">
                                                            <asp:Label ID="lblManagerFilter_MasterManagers" runat="server" Text="فیلتر مدیر"
                                                                meta:resourcekey="lblManagerFilter_MasterManagers" CssClass="WhiteLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblAccessGroup_MasterManagers" runat="server" Text=": نوع فیلتر" meta:resourcekey="lblAccessGroup_MasterManagers"
                                                    CssClass="WhiteLabel"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <ComponentArt:CallBack runat="server" ID="CallBack_cmbAccessGroup_MasterManagers"
                                                    OnCallback="CallBack_cmbAccessGroup_MasterManagers_onCallBack" Height="26">
                                                    <Content>
                                                        <ComponentArt:ComboBox ID="cmbAccessGroup_MasterManagers" runat="server" AutoComplete="true"
                                                            DataTextField="Name" DataValueField="ID" AutoFilter="true" AutoHighlight="false"
                                                            CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                            DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                            FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                            ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                            TextBoxEnabled="true" Enabled="false" Style="width: 60%">
                                                            <ClientEvents>
                                                                <Expand EventHandler="cmbAccessGroup_MasterManagers_onExpand" />
                                                            </ClientEvents>
                                                        </ComponentArt:ComboBox>
                                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_Filter" />
                                                    </Content>
                                                    <ClientEvents>
                                                        <BeforeCallback EventHandler="CallBack_cmbAccessGroup_MasterManagers_onBeforeCallback" />
                                                        <CallbackComplete EventHandler="CallBack_cmbAccessGroup_MasterManagers_onCallbackComplete" />
                                                        <CallbackError EventHandler="CallBack_cmbAccessGroup_MasterManagers_onCallbackError" />
                                                    </ClientEvents>
                                                </ComponentArt:CallBack>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%; border: outset 1px black;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 5%">
                                                            <input id="rdbManagerSearch_MasterManagers" name="ManagersDeatails" type="radio"
                                                                runat="server" onclick="rdbManagerSearch_MasterManagers_onClick();" />
                                                        </td>
                                                        <td style="width: 95%">
                                                            <asp:Label ID="lblManagerSearch_MasterManagers" runat="server" Text=": جستجوی مدیر"
                                                                meta:resourcekey="lblManagerSearch_MasterManagers" CssClass="WhiteLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSearchField_MasterManagers" runat="server" Text=": فیلد جستجو"
                                                    meta:resourcekey="lblSearchField_MasterManagers" CssClass="WhiteLabel"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <ComponentArt:CallBack runat="server" ID="CallBack_cmbSearchField_MasterManagers"
                                                    OnCallback="CallBack_cmbSearchField_MasterManagers_onCallBack" Height="26">
                                                    <Content>
                                                        <ComponentArt:ComboBox ID="cmbSearchField_MasterManagers" runat="server" AutoComplete="true"
                                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                            DropDownHeight="70" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                            TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Style="width: 60%" Enabled="false"
                                                            ExpandDirection="Up">
                                                            <ClientEvents>
                                                                <Expand EventHandler="cmbSearchField_MasterManagers_onExpand" />
                                                            </ClientEvents>
                                                        </ComponentArt:ComboBox>
                                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_Search" />
                                                    </Content>
                                                    <ClientEvents>
                                                        <BeforeCallback EventHandler="CallBack_cmbSearchField_MasterManagers_onBeforeCallback" />
                                                        <CallbackComplete EventHandler="CallBack_cmbSearchField_MasterManagers_CallbackComplete" />
                                                        <CallbackError EventHandler="CallBack_cmbSearchField_MasterManagers_onCallbackError" />
                                                    </ClientEvents>
                                                </ComponentArt:CallBack>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSearchTerm_MasterManagers" runat="server" Text=": عبارت جستجو"
                                                    meta:resourcekey="lblSearchTerm_MasterManagers" CssClass="WhiteLabel"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input id="txtSearchTerm_MasterManagers" type="text" disabled="disabled" class="TextBoxes"
                                                    style="width: 60%" onclick="this.select();" onfocus="this.select();" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbApplyConditions_MasterManagers" runat="server" CssClass="toolbar"
                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemApplyConditions_TlbApplyConditions_MasterManagers"
                                                runat="server" ClientSideCommand="tlbItemApplyConditions_TlbApplyConditions_MasterManagers_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemApplyConditions_TlbApplyConditions_MasterManagers"
                                                TextImageSpacing="5" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%" class="BoxStyle">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td id="header_Managers_MasterManagers" class="HeaderLabel" style="width: 50%;">
                                        Managers
                                    </td>
                                    <td id="loadingPanel_GridManagers_MasterManagers" class="HeaderLabel" style="width: 45%">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <iframe allowtransparency="true" runat="server" id="ManagersIntroduction_iFrame"
                                src="Managers.aspx" class="ManagersIntroduction_iFrame" frameborder="0" align="middle">
                            </iframe>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table style="width: 540px;">
                                <tr>
                                    <td style="width: 85%;" runat="server" meta:resourcekey="AlignObj">
                                        <ComponentArt:ToolBar ID="TlbPaging_GridManagers_MasterManagers" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            Style="direction: ltr" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridManagers_MasterManagers"
                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridManagers_MasterManagers_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridManagers_MasterManagers"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridManagers_MasterManagers"
                                                    runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_GridManagers_MasterManagers_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridManagers_MasterManagers"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridManagers_MasterManagers"
                                                    runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_GridManagers_MasterManagers_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridManagers_MasterManagers"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridManagers_MasterManagers"
                                                    runat="server" ClientSideCommand="tlbItemNext_TlbPaging_GridManagers_MasterManagers_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridManagers_MasterManagers"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridManagers_MasterManagers"
                                                    runat="server" ClientSideCommand="tlbItemLast_TlbPaging_GridManagers_MasterManagers_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridManagers_MasterManagers"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="footer_GridManagers_MasterManagers" style="width: 15%">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="SiteMapContainer">
                <ComponentArt:CallBack runat="server" ID="CallBackWorkFlow_MasterManagers" OnCallback="CallBackWorkFlow_MasterManagers_onCallBack">
                    <Content>
                        <ComponentArt:SiteMap ID="smpWorkFlow_MasterManagers" Width="100%" CssClass="SiteMap"
                            SiteMapLayout="Breadcrumbs" RootNodeCssClass="BreadcrumbsNode" ParentNodeCssClass="BreadcrumbsNode"
                            LeafNodeCssClass="BreadcrumbsNode" BreadcrumbsSeparatorString='&nbsp;<img src="images/SiteMap/MMX ARROW RIGHT - Copy.png" alt="" style="vertical-align:bottom;" />&nbsp;'
                            runat="server">
                        </ComponentArt:SiteMap>
                        <asp:HiddenField runat="server" ID="ErrorHiddenField_WorkFlow" />
                    </Content>
                    <ClientEvents>
                        <CallbackComplete EventHandler="CallBackWorkFlow_MasterManagers_onCallbackComplete" />
                        <CallbackError EventHandler="CallBackWorkFlow_MasterManagers_onCallbackError" />
                    </ClientEvents>
                </ComponentArt:CallBack>
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
    <asp:HiddenField runat="server" ID="hfheader_ManagersDetails_MasterManagers" meta:resourcekey="hfheader_ManagersDetails_MasterManagers" />
    <asp:HiddenField runat="server" ID="hfheader_Managers_MasterManagers" meta:resourcekey="hfheader_Managers_MasterManagers" />
    <asp:HiddenField runat="server" ID="hffooter_GridManagers_MasterManagers" meta:resourcekey="hffooter_GridManagers_MasterManagers" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridManagers_MasterManagers" meta:resourcekey="hfloadingPanel_GridManagers_MasterManagers" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_MasterManagers" meta:resourcekey="hfDeleteMessage_MasterManagers" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_MasterManagers" meta:resourcekey="hfCloseMessage_MasterManagers" />
    <asp:HiddenField runat="server" ID="hfView_MasterManagers" meta:resourcekey="hfView_MasterManagers" />
    <asp:HiddenField runat="server" ID="hfDelete_MasterManagers" meta:resourcekey="hfDelete_MasterManagers" />
    <asp:HiddenField runat="server" ID="hfErrorType_MasterManagers" meta:resourcekey="hfErrorType_MasterManagers" />
    <asp:HiddenField runat="server" ID="hfConnectionError_MasterManagers" meta:resourcekey="hfConnectionError_MasterManagers" />
    </form>
</body>
</html>
