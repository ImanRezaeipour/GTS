<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccessGroups.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.AccessGroups" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="AccessGroupsForm" runat="server" meta:resourcekey="AccessGroupsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small" class="BoxStyle">
        <tr>
            <td>
                <ComponentArt:ToolBar ID="TlbAccessGroups" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbAccessGroups" runat="server" ClientSideCommand="tlbItemNew_TlbAccessGroups_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbAccessGroups"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbAccessGroups" runat="server" ClientSideCommand="tlbItemEdit_TlbAccessGroups_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbAccessGroups"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbAccessGroups" runat="server" DropDownImageHeight="16px"
                            ClientSideCommand="tlbItemDelete_TlbAccessGroups_onClick();" DropDownImageWidth="16px"
                            ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                            meta:resourcekey="tlbItemDelete_TlbAccessGroups" TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbAccessGroups" runat="server" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbAccessGroups" TextImageSpacing="5"
                            ClientSideCommand="tlbItemHelp_TlbAccessGroups_onClick();" />
                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbAccessGroups" runat="server" DropDownImageHeight="16px"
                            ClientSideCommand="tlbItemSave_TlbAccessGroups_onClick();" DropDownImageWidth="16px"
                            ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px" ItemType="Command"
                            meta:resourcekey="tlbItemSave_TlbAccessGroups" TextImageSpacing="5" Enabled="false" />
                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbAccessGroups" runat="server" ClientSideCommand="tlbItemCancel_TlbAccessGroups_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbAccessGroups"
                            TextImageSpacing="5" Enabled="false" />
                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbAccessGroups" runat="server"
                            ClientSideCommand="tlbItemFormReconstruction_TlbAccessGroups_onClick();" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbAccessGroups"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbAccessGroups" runat="server" DropDownImageHeight="16px"
                            ClientSideCommand="tlbItemExit_TlbAccessGroups_onClick();" DropDownImageWidth="16px"
                            ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbAccessGroups"
                            TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
            <td id="ActionMode_AccessGroups" class="ToolbarMode">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_AccessGroupsBox_AccessGroups" class="HeaderLabel" style="width: 50%">
                                                    Access Groups
                                                </td>
                                                <td id="loadingPanel_GridAccessGroups_AccessGroups" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridAccessGroups_AccessGroups" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridAccessGroups_AccessGroups"
                                                                runat="server" ClientSideCommand="Refresh_GridAccessGroups_AccessGroups();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridAccessGroups_AccessGroups"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridAccessGroups_AccessGroups"
                                            OnCallback="CallBack_GridAccessGroups_AccessGroups_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridAccessGroups_AccessGroups" runat="server" CssClass="Grid"
                                                    EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                    PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="14" RunningMode="Client"
                                                    SearchTextCssClass="GridHeaderText" Width="100%" AllowMultipleSelect="false"
                                                    ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                    ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                            DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                            RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                            SortImageWidth="9">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام گروه دسترسی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnAccessGroupName_GridAccessGroups_AccessGroups"
                                                                    Width="250" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                                    Width="150" HeadingText="توضیحات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDescription_GridAccessGroups_AccessGroups" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridAccessGroups_AccessGroups_onLoad" />
                                                        <ItemSelect EventHandler="GridAccessGroups_AccessGroups_onItemSelect" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_AccessGroups" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridAccessGroups_AccessGroups_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridAccessGroups_AccessGroups_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td class="DetailsBoxHeaderStyle">
                                        <div id="header_AccessGroupsDetailsBox_AccessGroups" runat="server" meta:resourcekey="AlignObj"
                                            style="color: White; width: 100%;height: 100%" class="BoxContainerHeader">                                            
                                            جزییات گروه دسترسی</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAccessGroupName_AccessGroups" runat="server" Text=": نام گروه دسترسی"
                                            CssClass="WhiteLabel" meta:resourcekey="lblAccessGroupName_AccessGroups"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input id="txtAccessGroupName_AccessGroups" type="text" style="width: 97%;" class="TextBoxes"
                                            disabled="disabled" tabindex="1" onclick="this.select();" onfocus="this.select();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDescription_AccessGroups" runat="server" Text=": توضیحات" CssClass="WhiteLabel"
                                            meta:resourcekey="lblDescription_AccessGroups"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <textarea id="txtDescription_AccessGroups" cols="20" name="S1" rows="4" style="width: 97%"
                                            class="TextBoxes" disabled="disabled" tabindex="2" onclick="this.select();" onfocus="this.select();"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbAccessLevel_AccessGroups" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            Orientation="Vertical" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item"
                                            DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px"
                                            DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                            ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemAccessLevel_TlbAccessLevel_AccessGroups" runat="server"
                                                    ClientSideCommand="tlbItemAccessLevel_TlbAccessLevel_AccessGroups_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="access_silver.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAccessLevel_TlbAccessLevel_AccessGroups"
                                                    TextImageSpacing="5" Enabled="false" />
                                            </Items>
                                        </ComponentArt:ToolBar>
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
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogAccessLevel"
        runat="server" Width="400px" HeaderClientTemplateId="DialogAccessLevelheader"
        FooterClientTemplateId="DialogAccessLevelfooter">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogAccessLevelheader">
                <table style="width: 401px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogAccessLevel.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogAccessLevel_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogAccessLevel" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogAccessLevel" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="DialogAccessLevel.Close('cancelled')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogAccessLevel_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogAccessLevelfooter">
                <table id="tbl_DialogAccessLevelfooter" style="width: 401px" cellpadding="0" cellspacing="0"
                    border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogAccessLevel_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogAccessLevel_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <Content>
            <table id="Table1" runat="server" class="BodyStyle" style="width: 100%; font-family: Arial;
                font-size: small">
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbAccessLevel" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                        DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                        DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                        DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemSave_TlbAccessLevel" runat="server" ClientSideCommand="tlbItemSave_TlbAccessLevel_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbAccessLevel"
                                                TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbAccessLevel" runat="server" ClientSideCommand="tlbItemExit_TlbAccessLevel_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_AccessLevel"
                                                TextImageSpacing="5" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                                <td id="ActionMode_AccessLevel" class="ToolbarMode">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAccessGroupName_AccessLevel_AccessGroups" runat="server" CssClass="WhiteLabel"
                            meta:resourcekey="lblAccessGroupName_AccessLevel_AccessGroups" Text=": نام گروه دسترسی"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="txtAccessGroupName_AccessLevel_AccessGroups" class="TextBoxes" readonly="readonly"
                            type="text" style="width: 99%" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="BoxStyle" style="width: 100%;">
                            <tr>
                                <td id="" style="color: White; font-weight: bold; font-family: Arial; width: 100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td id="header_AccessLevelBox_AccessGroups" class="HeaderLabel" style="width: 50%">
                                                Access Levels
                                            </td>
                                            <td id="loadingPanel_trvAccessLevel_AccessGroups" class="HeaderLabel" style="width: 45%">
                                            </td>
                                            <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj" style="width: 5%">
                                                <ComponentArt:ToolBar ID="TlbRefresh_trvAccessLevel_AccessGroups" runat="server"
                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvAccessLevel_AccessGroups"
                                                            runat="server" ClientSideCommand="Refresh_trvAccessLevel_AccessGroups();" DropDownImageHeight="16px"
                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                            ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvAccessLevel_AccessGroups"
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
                                    <ComponentArt:CallBack ID="CallBack_trvAccessLevel_AccessGroups" runat="server" OnCallback="CallBack_trvAccessLevel_AccessGroups_onCallBack">
                                        <Content>
                                            <ComponentArt:TreeView ID="trvAccessLevel_AccessGroups" runat="server" ExpandNodeOnSelect="true"
                                                CollapseNodeOnSelect="false" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                                DefaultImageHeight="16" HighlightSelectedPath="true" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                EnableViewState="false" ExpandCollapseImageHeight="15" LoadingFeedbackText="loading......."
                                                ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" FillContainer="false"
                                                ForceHighlightedNodeID="true" HoverNodeCssClass="HoverNestingTreeNode" ItemSpacing="2"
                                                KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19" NodeCssClass="TreeNode"
                                                NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode"
                                                ShowLines="true" BorderColor="Black" Height="290" meta:resourcekey="trvAccessLevel_AccessGroups">
                                                <ClientEvents>
                                                    <NodeCheckChange EventHandler="trvAccessLevel_AccessGroups_onNodeCheckChange" />
                                                    <Load EventHandler="trvAccessLevel_AccessGroups_onLoad" />
                                                </ClientEvents>
                                            </ComponentArt:TreeView>
                                            <asp:HiddenField ID="hfAccessLevelsList_AccessGroups" runat="server" Value="null" />
                                            <asp:HiddenField ID="ErrorHiddenField_AccessLevel" runat="server" />
                                        </Content>
                                        <ClientEvents>
                                            <CallbackComplete EventHandler="CallBack_trvAccessLevel_AccessGroups_onCallbackComplete" />
                                            <CallbackError EventHandler="CallBack_trvAccessLevel_AccessGroups_onCallbackError" />
                                        </ClientEvents>
                                    </ComponentArt:CallBack>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogAccessLevel_OnShow" />
        </ClientEvents>
    </ComponentArt:Dialog>
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
                        <img id="Img1" runat="server" alt="" src="~/Images/Dialog/Waiting.gif"  />
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogWaiting_onShow" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <asp:HiddenField runat="server" ID="hfTitle_DialogAccessGroups" meta:resourcekey="hfTitle_DialogAccessGroups" />
    <asp:HiddenField runat="server" ID="hfheader_AccessGroupsBox_AccessGroups" meta:resourcekey="hfheader_AccessGroupsBox_AccessGroups" />
    <asp:HiddenField runat="server" ID="hfheader_AccessGroupsDetailsBox_AccessGroups"
        meta:resourcekey="hfheader_AccessGroupsDetailsBox_AccessGroups" />
    <asp:HiddenField runat="server" ID="hfView_AccessGroups" meta:resourcekey="hfView_AccessGroups" />
    <asp:HiddenField runat="server" ID="hfAdd_AccessGroups" meta:resourcekey="hfAdd_AccessGroups" />
    <asp:HiddenField runat="server" ID="hfEdit_AccessGroups" meta:resourcekey="hfEdit_AccessGroups" />
    <asp:HiddenField runat="server" ID="hfDelete_AccessGroups" meta:resourcekey="hfDelete_AccessGroups" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_AccessGroups" meta:resourcekey="hfDeleteMessage_AccessGroups" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_AccessGroups" meta:resourcekey="hfCloseMessage_AccessGroups" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridAccessGroups_AccessGroups"
        meta:resourcekey="hfloadingPanel_GridAccessGroups_AccessGroups" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogAccessLevel" meta:resourcekey="hfTitle_DialogAccessLevel" />
    <asp:HiddenField runat="server" ID="hfheader_AccessLevelBox_AccessGroups" meta:resourcekey="hfheader_AccessLevelBox_AccessGroups" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvAccessLevel_AccessGroups" meta:resourcekey="hfloadingPanel_trvAccessLevel_AccessGroups" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_AccessLevels" meta:resourcekey="hfCloseMessage_AccessLevels" />
    <asp:HiddenField runat="server" ID="hfErrorType_AccessLevels" meta:resourcekey="hfErrorType_AccessLevels" />
    <asp:HiddenField runat="server" ID="hfConnectionError_AccessLevels" meta:resourcekey="hfConnectionError_AccessLevels" />
    </form>
</body>
</html>
