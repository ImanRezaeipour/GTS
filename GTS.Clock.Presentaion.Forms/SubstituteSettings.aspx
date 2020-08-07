<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubstituteSettings.aspx.cs"
    Inherits="SubstituteSettings" %>

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
    <link href="css/menuStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="SubstituteSettingsForm" runat="server" meta:resourcekey="SubstituteSettingsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <div>
        <table style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbSubstituteSettingsSettings" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbSubstituteSettings" runat="server"
                                            ClientSideCommand="tlbItemDelete_TlbSubstituteSettings_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemDelete_TlbSubstituteSettings" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbSubstituteSettings" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbSubstituteSettings" TextImageSpacing="5"
                                            ClientSideCommand="tlbItemHelp_TlbSubstituteSettings_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbSubstituteSettings" runat="server" ClientSideCommand="tlbItemSave_TlbSubstituteSettings_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbSubstituteSettings"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbSubstituteSettings" runat="server"
                                            ClientSideCommand="tlbItemCancel_TlbSubstituteSettings_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemCancel_TlbSubstituteSettings" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbSubstituteSettings" runat="server"
                                            ClientSideCommand="tlbItemFormReconstruction_TlbSubstituteSettings_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbSubstituteSettings"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbSubstituteSettings" runat="server" ClientSideCommand="tlbItemExit_TlbSubstituteSettings_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbSubstituteSettings"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="ActionMode_SubstituteSettings" class="ToolbarMode">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="lblManager_SubstituteSettings" runat="server" Text=": مدیر" class="WhiteLabel"
                                    meta:resourcekey="lblManager_SubstituteSettings"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSubstitute_SubstituteSettings" runat="server" Text=": جانشین" class="WhiteLabel"
                                    meta:resourcekey="lblSubstitute_SubstituteSettings"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input id="txtManager_SubstituteSettings" type="text" style="width: 50%" class="TextBoxes"
                                    readonly="readonly" />
                            </td>
                            <td>
                                <input id="txtSubstitute_SubstituteSettings" type="text" style="width: 50%" class="TextBoxes"
                                    readonly="readonly" />
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
                                        <td id="header_ManagerWorkFlows_SubstituteSettings" class="HeaderLabel" style="width: 50%">
                                            Manager WorkFlows
                                        </td>
                                        <td id="loadingPanel_GridManagerWorkFlows_SubstituteSettings" class="HeaderLabel"
                                            style="width: 45%">
                                        </td>
                                        <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridManagerWorkFlows_SubstituteSettings" runat="server"
                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridManagerWorkFlows_SubstituteSettings"
                                                        runat="server" ClientSideCommand="Refresh_GridManagerWorkFlows_SubstituteSettings();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridManagerWorkFlows_SubstituteSettings"
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
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridManagerWorkFlows_SubstituteSettings"
                                    OnCallback="CallBack_GridManagerWorkFlows_SubstituteSettings_onCallBack">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridManagerWorkFlows_SubstituteSettings" runat="server"
                                            CssClass="Grid" EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter"
                                            ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                            PageSize="5" RunningMode="Client" SearchTextCssClass="GridHeaderText" Width="100%"
                                            AllowMultipleSelect="false" ShowFooter="false" AllowColumnResizing="false" ScrollBar="On"
                                            ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16"
                                            ScrollImagesFolderUrl="images/Grid/scroller/" ScrollButtonWidth="16" ScrollButtonHeight="17"
                                            ScrollBarCssClass="ScrollBar" ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                    SortImageWidth="9">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="IsAssignedToSubstitute" ColumnType="CheckBox"
                                                            AllowEditing="False" HeadingTextCssClass="HeadingText" Width="75" meta:resourcekey="clmnIsAssingned_GridManagerWorkFlows_SubstituteSettings" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="AccessGroup.Name" HeadingText="گروه دسترسی"
                                                            meta:resourcekey="clmnAccessGroup_GridManagerWorkFlows_SubstituteSettings" Width="150"
                                                            HeadingTextCssClass="HeadingText" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="FlowName" DefaultSortDirection="Descending"
                                                            HeadingText="نام جریان" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnWorkFlowName_GridManagerWorkFlows_SubstituteSettings"
                                                            Width="125" />
                                                        <ComponentArt:GridColumn DataField="AccessGroup.ID" Visible="false" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientEvents>
                                                <ItemSelect EventHandler="GridManagerWorkFlows_SubstituteSettings_onItemSelect" />
                                                <Load EventHandler="GridManagerWorkFlows_SubstituteSettings_onLoad" />
                                                <ItemBeforeSelect EventHandler="GridManagerWorkFlows_SubstituteSettings_onItemBeforeSelect" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField ID="ErrorHiddenField_ManagerWorkFlows" runat="server" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridManagerWorkFlows_SubstituteSettings_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridManagerWorkFlows_SubstituteSettings_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 48%">
                                <table style="width: 100%;" class="BoxStyle">
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td id="header_UnderManagementPersonnel_SubstituteSettings" class="HeaderLabel" style="width: 50%">
                                                        UnderManagement Personnel
                                                    </td>
                                                    <td id="loadingPanel_trvUnderManagementPersonnel_SubstituteSettings" class="HeaderLabel"
                                                        style="width: 45%">
                                                    </td>
                                                    <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                        <ComponentArt:ToolBar ID="TlbRefresh_trvUnderManagementPersonnel_SubstituteSettings"
                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvUnderManagementPersonnel_SubstituteSettings"
                                                                    runat="server" ClientSideCommand="Refresh_trvUnderManagementPersonnel_SubstituteSettings();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvUnderManagementPersonnel_SubstituteSettings"
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
                                            <ComponentArt:CallBack runat="server" ID="CallBack_trvUnderManagementPersonnel_SubstituteSettings"
                                                OnCallback="CallBack_trvUnderManagementPersonnel_SubstituteSettings_onCallBack">
                                                <Content>
                                                    <ComponentArt:TreeView ID="trvUnderManagementPersonnel_SubstituteSettings" runat="server"
                                                        CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                        DefaultImageWidth="16" DragAndDropEnabled="true" EnableViewState="false" ExpandCollapseImageHeight="15"
                                                        ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" FillContainer="false"
                                                        Height="230" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true"
                                                        LineImageHeight="20" LineImageWidth="19" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                                        NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                        SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" BorderColor="Black"
                                                        meta:resourcekey="trvUnderManagementPersonnel_SubstituteSettings">
                                                        <ClientEvents>
                                                            <Load EventHandler="trvUnderManagementPersonnel_SubstituteSettings_onLoad" />
                                                            <CallbackComplete EventHandler="trvUnderManagementPersonnel_SubstituteSettings_onCallbackComplete" />
                                                            <NodeBeforeExpand EventHandler="trvUnderManagementPersonnel_SubstituteSettings_onNodeBeforeExpand" />
                                                        </ClientEvents>
                                                    </ComponentArt:TreeView>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_UnderManagementPersonnel" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_trvUnderManagementPersonnel_SubstituteSettings_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_trvUnderManagementPersonnel_SubstituteSettings_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 4%" align="center" valign="middle">
                                &nbsp;
                            </td>
                            <td>
                                <table style="width: 100%;" class="BoxStyle">
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td id="header_WorkFlowAccessLevels_SubstituteSettings" class="HeaderLabel" style="width: 55%">
                                                        Work Flow Access Levels
                                                    </td>
                                                    <td id="loadingPanel_trvWorkFlowAccessLevels_SubstituteSettings" class="HeaderLabel"
                                                        style="width: 40%">
                                                    </td>
                                                    <td id="Td2" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                        <ComponentArt:ToolBar ID="TlbRefresh_trvWorkFlowAccessLevels_SubstituteSettings"
                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvWorkFlowAccessLevels_SubstituteSettings"
                                                                    runat="server" ClientSideCommand="Refresh_trvWorkFlowAccessLevels_SubstituteSettings();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvWorkFlowAccessLevels_SubstituteSettings"
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
                                            <ComponentArt:CallBack runat="server" ID="CallBack_trvWorkFlowAccessLevels_SubstituteSettings"
                                                OnCallback="CallBack_trvWorkFlowAccessLevels_SubstituteSettings_onCallBack">
                                                <Content>
                                                    <ComponentArt:TreeView ID="trvWorkFlowAccessLevels_SubstituteSettings" runat="server"
                                                        CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                        DefaultImageWidth="16" DragAndDropEnabled="true" EnableViewState="false" ExpandCollapseImageHeight="15"
                                                        ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" FillContainer="false"
                                                        Height="230" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true"
                                                        LineImageHeight="20" LineImageWidth="19" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                                        NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                        SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" BorderColor="Black"
                                                        meta:resourcekey="trvWorkFlowAccessLevels_SubstituteSettings">
                                                        <ClientEvents>
                                                            <Load EventHandler="trvWorkFlowAccessLevels_SubstituteSettings_onLoad" />
                                                            <NodeCheckChange EventHandler="trvWorkFlowAccessLevels_SubstituteSettings_onNodeCheckChange" />
                                                        </ClientEvents>
                                                    </ComponentArt:TreeView>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_WorkFlowAccessLevels" />
                                                    <asp:HiddenField runat="server" ID="hfAccessLevelsList_SubstituteSettings" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_trvWorkFlowAccessLevels_SubstituteSettings_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_trvWorkFlowAccessLevels_SubstituteSettings_onCallbackError" />
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
        </table>
    </div>
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
    <asp:HiddenField runat="server" ID="hfTitle_DialogSubstituteSettings" meta:resourcekey="hfTitle_DialogSubstituteSettings" />
    <asp:HiddenField runat="server" ID="hfheader_ManagerWorkFlows_SubstituteSettings"
        meta:resourcekey="hfheader_ManagerWorkFlows_SubstituteSettings" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridManagerWorkFlows_SubstituteSettings"
        meta:resourcekey="hfloadingPanel_GridManagerWorkFlows_SubstituteSettings" />
    <asp:HiddenField runat="server" ID="hfheader_UnderManagementPersonnel_SubstituteSettings"
        meta:resourcekey="hfheader_UnderManagementPersonnel_SubstituteSettings" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvUnderManagementPersonnel_SubstituteSettings"
        meta:resourcekey="hfloadingPanel_trvUnderManagementPersonnel_SubstituteSettings" />
    <asp:HiddenField runat="server" ID="hfheader_WorkFlowAccessLevels_SubstituteSettings"
        meta:resourcekey="hfheader_WorkFlowAccessLevels_SubstituteSettings" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvWorkFlowAccessLevels_SubstituteSettings"
        meta:resourcekey="hfloadingPanel_trvWorkFlowAccessLevels_SubstituteSettings" />
    <asp:HiddenField runat="server" ID="hfView_SubstituteSettings" meta:resourcekey="hfView_SubstituteSettings" />
    <asp:HiddenField runat="server" ID="hfAdd_SubstituteSettings" meta:resourcekey="hfAdd_SubstituteSettings" />
    <asp:HiddenField runat="server" ID="hfEdit_SubstituteSettings" meta:resourcekey="hfEdit_SubstituteSettings" />
    <asp:HiddenField runat="server" ID="hfDelete_SubstituteSettings" meta:resourcekey="hfDelete_SubstituteSettings" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_SubstituteSettings" meta:resourcekey="hfDeleteMessage_SubstituteSettings" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_SubstituteSettings" meta:resourcekey="hfCloseMessage_SubstituteSettings" />
    <asp:HiddenField runat="server" ID="hfErrorType_SubstituteSettings" meta:resourcekey="hfErrorType_SubstituteSettings" />
    <asp:HiddenField runat="server" ID="hfConnectionError_SubstituteSettings" meta:resourcekey="hfConnectionError_SubstituteSettings" />
    </form>
</body>
</html>
