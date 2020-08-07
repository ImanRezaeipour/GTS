<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrganizationWorkFlow.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.OrganizationWorkFlow" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/siteMapStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="OrganizationWorkFlowForm" runat="server" meta:resourcekey="OrganizationWorkFlowForm"
    onsubmit="return false;">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbOrganizationWorkFlow" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbOrganizationWorkFlow" runat="server"
                                        ClientSideCommand="tlbItemNew_TlbOrganizationWorkFlow_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemNew_TlbOrganizationWorkFlow" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbOrganizationWorkFlow" runat="server"
                                        ClientSideCommand="tlbItemEdit_TlbOrganizationWorkFlow_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemEdit_TlbOrganizationWorkFlow" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbOrganizationWorkFlow" runat="server"
                                        ClientSideCommand="tlbItemDelete_TlbOrganizationWorkFlow_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemDelete_TlbOrganizationWorkFlow" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemWorkFlowOperators_TlbOrganizationWorkFlow" runat="server"
                                        ClientSideCommand="tlbItemWorkFlowOperators_TlbOrganizationWorkFlow_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="operator.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemWorkFlowOperators_TlbOrganizationWorkFlow"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbOrganizationWorkFlow" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbOrganizationWorkFlow"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbOrganizationWorkFlow_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbOrganizationWorkFlow"
                                        runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbOrganizationWorkFlow_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbOrganizationWorkFlow"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbOrganizationWorkFlow" runat="server"
                                        ClientSideCommand="tlbItemExit_TlbOrganizationWorkFlow_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbOrganizationWorkFlow" TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_OrganizationWorkFlow" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div runat="server" meta:resourcekey="AlignObj" style="width: 100%" class="DropDownHeader">
                    <img id="imgbox_WorkFlowSearch_OrganizationWorkFlow" runat="server" alt="" onclick="imgbox_WorkFlowSearch_OrganizationWorkFlow_onClick();"
                        src="~/Images/Ghadir/arrowDown.jpg" />
                    <span id="header_WorkFlowSearch_OrganizationWorkFlow">جستجوی جریان کاری</span>
                </div>
                <div id="box_WorkFlowSearch_OrganizationWorkFlow" class="dhtmlgoodies_contentBox"
                    style="width: 36.5%;">
                    <div id="subbox_WorkFlowSearch_OrganizationWorkFlow" class="dhtmlgoodies_content">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="margin-left: 40px">
                                                <asp:Label ID="lblSearchField_OrganizationWorkFlow" runat="server" Text=": جستجو بر اساس"
                                                    CssClass="WhiteLabel" meta:resourcekey="lblSearchField_OrganizationWorkFlow"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSearchTerm_OrganizationWorkFlow" runat="server" Text="Label" CssClass="WhiteLabel"
                                                    meta:resourcekey="lblSearchTerm_OrganizationWorkFlow"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <ComponentArt:CallBack runat="server" ID="CallBack_cmbSearchField_OrganizationWorkFlow"
                                                    OnCallback="CallBack_cmbSearchField_OrganizationWorkFlow_onCallBack" Height="26">
                                                    <Content>
                                                        <ComponentArt:ComboBox ID="cmbSearchField_OrganizationWorkFlow" runat="server" AutoComplete="true"
                                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                            DropDownHeight="70" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                                                            HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                                            SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" Width="150"
                                                            TextBoxEnabled="true">
                                                            <ClientEvents>
                                                                <Expand EventHandler="cmbSearchField_OrganizationWorkFlow_onExpand" />
                                                                <Collapse EventHandler="cmbSearchField_OrganizationWorkFlow_onCollapse" />
                                                            </ClientEvents>
                                                        </ComponentArt:ComboBox>
                                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_WorkFlowSearch" />
                                                    </Content>
                                                    <ClientEvents>
                                                        <BeforeCallback EventHandler="CallBack_cmbSearchField_OrganizationWorkFlow_onBeforeCallback" />
                                                        <CallbackComplete EventHandler="CallBack_cmbSearchField_OrganizationWorkFlow_onCallbackComplete" />
                                                        <CallbackError EventHandler="CallBack_cmbSearchField_OrganizationWorkFlow_onCallbackError" />
                                                    </ClientEvents>
                                                </ComponentArt:CallBack>
                                            </td>
                                            <td>
                                                <input id="txtSearchTerm_OrganizationWorkFlow" type="text" style="width: 95%" class="TextBoxes"
                                                    onfocus="this.select();" onclick="this.select();" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbApplyConditions_OrganizationWorkFlow" runat="server"
                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                        Orientation="Vertical" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item"
                                        DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px"
                                        DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                        ItemSpacing="1px" UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemApplyConditions_TlbApplyConditions_OrganizationWorkFlow"
                                                runat="server" ClientSideCommand="tlbItemApplyConditions_TlbApplyConditions_OrganizationWorkFlow_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemApplyConditions_TlbApplyConditions_OrganizationWorkFlow"
                                                TextImageSpacing="5" Enabled="true" />
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
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_WorkFlows_OrganizationWorkFlow" class="HeaderLabel" style="width: 50%">
                                                    Work Flows
                                                </td>
                                                <td id="loadingPanel_GridWorkFlows_OrganizationWorkFlow" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridWorkFlows_OrganizationWorkFlow" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridWorkFlows_OrganizationWorkFlow"
                                                                runat="server" ClientSideCommand="Refresh_GridWorkFlows_OrganizationWorkFlow();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridWorkFlows_OrganizationWorkFlow"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridWorkFlows_OrganizationWorkFlow"
                                            OnCallback="CallBack_GridWorkFlows_OrganizationWorkFlow_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridWorkFlows_OrganizationWorkFlow" runat="server" CssClass="Grid"
                                                    EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                    PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="13" RunningMode="Client"
                                                    SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false" ShowFooter="false"
                                                    AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                    Height="150" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow"
                                                            DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                            HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                            SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                            SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="AccessGroup.ID" Visible="false" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="AccessGroup.Name" HeadingText="گروه دسترسی"
                                                                    meta:resourcekey="clmnAccessGroup_GridWorkFlows_OrganizationWorkFlow" Width="150"
                                                                    HeadingTextCssClass="HeadingText" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="FlowName" DefaultSortDirection="Descending"
                                                                    HeadingText="نام جریان" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnWorkFlowName_GridWorkFlows_OrganizationWorkFlow"
                                                                    Width="125" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="ActiveFlow" DefaultSortDirection="Descending"
                                                                    ColumnType="CheckBox" HeadingText="جریان فعال" HeadingTextCssClass="HeadingText"
                                                                    meta:resourcekey="clmnWorkFlowActive_GridWorkFlows_OrganizationWorkFlow" Width="65" />
                                                                <ComponentArt:GridColumn DataField="MainFlow" Visible="false" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridWorkFlows_OrganizationWorkFlow_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_WorkFlows_OrganizationWorkFlow" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridWorkFlows_OrganizationWorkFlow_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridWorkFlows_OrganizationWorkFlow_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" valign="middle" style="width: 2%">
                            <ComponentArt:ToolBar ID="TlbInterAction_OrganizationWorkFlow" runat="server" CssClass="verticaltoolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" Orientation="Vertical" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemWorkFlowView_TlbInterAction_OrganizationWorkFlow"
                                        runat="server" ClientSideCommand="tlbItemWorkFlowView_TlbInterAction_OrganizationWorkFlow_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                        ImageUrl="flow.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemWorkFlowView_TlbInterAction_OrganizationWorkFlow"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemUnderManagementPersonnel_TlbInterAction_OrganizationWorkFlow"
                                        runat="server" ClientSideCommand="tlbItemUnderManagementPersonnel_TlbInterAction_OrganizationWorkFlow_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                        ImageUrl="dap.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemUnderManagementPersonnel_TlbInterAction_OrganizationWorkFlow"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td style="width: 50%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_UnderManagementPersonnel_OrganizationWorkFlow" class="HeaderLabel"
                                                    style="width: 65%">
                                                    UnderManagement Personnel
                                                </td>
                                                <td id="loadingPanel_trvUnderManagementPersonnel_OrganizationWorkFlow" class="HeaderLabel"
                                                    style="width: 30%">
                                                </td>
                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_trvUnderManagementPersonnel_OrganizationWorkFlow"
                                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvUnderManagementPersonnel_OrganizationWorkFlow"
                                                                runat="server" ClientSideCommand="Refresh_trvUnderManagementPersonnel_OrganizationWorkFlow();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvUnderManagementPersonnel_OrganizationWorkFlow"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_trvUnderManagementPersonnel_OrganizationWorkFlow"
                                            OnCallback="CallBack_trvUnderManagementPersonnel_OrganizationWorkFlow_onCallBack">
                                            <Content>
                                                <ComponentArt:TreeView ID="trvUnderManagementPersonnel_OrganizationWorkFlow" runat="server"
                                                    CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                    ExpandNodeOnSelect="false" DefaultImageWidth="16" DragAndDropEnabled="true" EnableViewState="false"
                                                    ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif"
                                                    FillContainer="false" Height="290" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2"
                                                    KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                                    NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                    SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" BorderColor="Black"
                                                    meta:resourcekey="trvUnderManagementPersonnel_OrganizationWorkFlow">
                                                    <ClientEvents>
                                                        <Load EventHandler="trvUnderManagementPersonnel_OrganizationWorkFlow_onLoad" />
                                                        <CallbackComplete EventHandler="trvUnderManagementPersonnel_OrganizationWorkFlow_onCallbackComplete" />
                                                        <NodeBeforeExpand EventHandler="trvUnderManagementPersonnel_OrganizationWorkFlow_onNodeBeforeExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:TreeView>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_UnderManagementPersonnel_OrganizationWorkFlow" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_trvUnderManagementPersonnel_OrganizationWorkFlow_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_trvUnderManagementPersonnel_OrganizationWorkFlow_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="SiteMapContainer">
                <ComponentArt:CallBack ID="CallBack_WorkFlow_OrganizationWorkFlow" runat="server"
                    OnCallback="CallBack_WorkFlow_OrganizationWorkFlow_onCallBack">
                    <Content>
                        <ComponentArt:SiteMap ID="smpWorkFlow_OrganizationWorkFlow" runat="server" BreadcrumbsSeparatorString='&nbsp;<img src="images/SiteMap/MMX ARROW RIGHT - Copy.png" alt="" style="vertical-align:bottom;" />&nbsp;'
                            CssClass="SiteMap" LeafNodeCssClass="BreadcrumbsNode" ParentNodeCssClass="BreadcrumbsNode"
                            RootNodeCssClass="BreadcrumbsNode" SiteMapLayout="Breadcrumbs" Width="100%">
                        </ComponentArt:SiteMap>
                        <asp:HiddenField runat="server" ID="ErrorHiddenField_WorkFlow_OrganizationWorkFlow" />
                    </Content>
                    <ClientEvents>
                        <CallbackComplete EventHandler="CallBack_WorkFlow_OrganizationWorkFlow_onCallbackComplete" />
                        <CallbackError EventHandler="CallBack_WorkFlow_OrganizationWorkFlow_onCallbackError" />
                    </ClientEvents>
                </ComponentArt:CallBack>
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="320px" Style="top: 157px; left: 0px">
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
    <asp:HiddenField runat="server" ID="hfheader_WorkFlowSearch_OrganizationWorkFlow"
        meta:resourcekey="hfheader_WorkFlowSearch_OrganizationWorkFlow" />
    <asp:HiddenField runat="server" ID="hfheader_WorkFlows_OrganizationWorkFlow" meta:resourcekey="hfheader_WorkFlows_OrganizationWorkFlow" />
    <asp:HiddenField runat="server" ID="hfheader_UnderManagementPersonnel_OrganizationWorkFlow"
        meta:resourcekey="hfheader_UnderManagementPersonnel_OrganizationWorkFlow" />
    <asp:HiddenField runat="server" ID="hfView_OrganizationWorkFlow" meta:resourcekey="hfView_OrganizationWorkFlow" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridWorkFlows_OrganizationWorkFlow"
        meta:resourcekey="hfloadingPanel_GridWorkFlows_OrganizationWorkFlow" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvUnderManagementPersonnel_OrganizationWorkFlow"
        meta:resourcekey="hfloadingPanel_trvUnderManagementPersonnel_OrganizationWorkFlow" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_OrganizationWorkFlow" meta:resourcekey="hfDeleteMessage_OrganizationWorkFlow" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_OrganizationWorkFlow" meta:resourcekey="hfCloseMessage_OrganizationWorkFlow" />
    <asp:HiddenField runat="server" ID="hfErrorType_OrganizationWorkFlow" meta:resourcekey="hfErrorType_OrganizationWorkFlow" />
    <asp:HiddenField runat="server" ID="hfConnectionError_OrganizationWorkFlow" meta:resourcekey="hfConnectionError_OrganizationWorkFlow" />
    </form>
</body>
</html>
