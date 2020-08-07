<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RulesGroupUpdate.aspx.cs"
    Inherits="RulesGroupUpdate" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/menuStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="RulesGroupUpdateForm" runat="server" meta:resourcekey="RulesGroupUpdateForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <ComponentArt:Menu ID="contextMenu_trvRulesTemplates_RulesGroupUpdate" Orientation="Vertical"
        DefaultGroupCssClass="Group" DefaultItemLookId="DefaultItemLook" DefaultGroupItemSpacing="1"
        ImagesBaseUrl="images/Menu" EnableViewState="false" ContextMenu="Custom" runat="server">
        <Items>
            <ComponentArt:MenuItem ID="AddItem_contextMenu_trvRulesTemplates_RulesGroupUpdate"
                Look-LeftIconHeight="16px" Look-LeftIconWidth="16px" Look-RightIconWidth="16px"
                Look-RightIconHeight="16px" runat="server" meta:resourcekey="AddItem_contextMenu_trvRulesTemplates_RulesGroupUpdate">
            </ComponentArt:MenuItem>
        </Items>
        <ClientEvents>
            <ItemSelect EventHandler="contextMenu_trvRulesTemplates_RulesGroupUpdate_onItemSelect" />
        </ClientEvents>
        <ItemLooks>
            <ComponentArt:ItemLook LookId="DefaultItemLook" CssClass="MenuItem" HoverCssClass="ItemHover"
                ExpandedCssClass="ItemHover" LeftIconWidth="20" LeftIconHeight="18" LabelPaddingLeft="10"
                LabelPaddingRight="10" LabelPaddingTop="3" LabelPaddingBottom="4" />
            <ComponentArt:ItemLook LookId="BreakItem" CssClass="MenuBreak" />
        </ItemLooks>
    </ComponentArt:Menu>
    <ComponentArt:Menu ID="contextMenu_trvRules_RulesGroupUpdate" Orientation="Vertical"
        DefaultGroupCssClass="Group" DefaultItemLookId="DefaultItemLook" DefaultGroupItemSpacing="1"
        ImagesBaseUrl="images/Menu" EnableViewState="false" ContextMenu="Custom" runat="server">
        <Items>
            <ComponentArt:MenuItem ID="DeleteItem_contextMenu_trvRules_RulesGroupUpdate" Look-LeftIconHeight="16px"
                Look-LeftIconWidth="16px" Look-RightIconWidth="16px" Look-RightIconHeight="16px"
                runat="server" meta:resourcekey="DeleteItem_contextMenu_trvRules_RulesGroupUpdate">
            </ComponentArt:MenuItem>
        </Items>
        <ClientEvents>
            <ItemSelect EventHandler="contextMenu_trvRules_RulesGroupUpdate_onItemSelect" />
        </ClientEvents>
        <ItemLooks>
            <ComponentArt:ItemLook LookId="DefaultItemLook" CssClass="MenuItem" HoverCssClass="ItemHover"
                ExpandedCssClass="ItemHover" LeftIconWidth="20" LeftIconHeight="18" LabelPaddingLeft="10"
                LabelPaddingRight="10" LabelPaddingTop="3" LabelPaddingBottom="4" />
            <ComponentArt:ItemLook LookId="BreakItem" CssClass="MenuBreak" />
        </ItemLooks>
    </ComponentArt:Menu>
    <table style="width: 100%;" class="BoxStyle">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbRulesGroupUpdate" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbRulesGroupUpdate" runat="server" ClientSideCommand="tlbItemSave_TlbRulesGroupUpdate_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                        ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbRulesGroupUpdate"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbRulesGroupUpdate" runat="server" ClientSideCommand="tlbItemCancel_TlbRulesGroupUpdate_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                        ImageUrl="cancel.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbRulesGroupUpdate"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemRuleParameteresRegulation_TlbRulesGroupUpdate"
                                        runat="server" ClientSideCommand="tlbItemRuleParameteresRegulation_TlbRulesGroupUpdate_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="false" ImageHeight="16px"
                                        ImageUrl="regulation_silver.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRuleParameteresRegulation_TlbRulesGroupUpdate"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemRuleGroupRulesParametersValidation_TlbRulesGroupUpdate"
                                        runat="server" ClientSideCommand="tlbItemRuleGroupRulesParametersValidation_TlbRulesGroupUpdate_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                        ImageUrl="RuleGroupRulesParametersValidation.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemRuleGroupRulesParametersValidation_TlbRulesGroupUpdate"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbRulesGroupUpdate" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbRulesGroupUpdate" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbRulesGroupUpdate_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbRulesGroupUpdate" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbRulesGroupUpdate_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbRulesGroupUpdate"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbRulesGroupUpdate" runat="server" ClientSideCommand="tlbItemExit_TlbRulesGroupUpdate_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbRulesGroupUpdate"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_RulesGroupUpdate" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 45%">
                            <asp:Label ID="lblRuleGroupName_RulesGroupUpdate" runat="server" Text=": نام گروه قانون"
                                meta:resourcekey="lblRuleGroupName_RulesGroupUpdate" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td style="width: 55%">
                            <asp:Label ID="lblRuleGroupDescriptions_RulesGroupUpdate" runat="server" Text=": توضیحات گروه قانون"
                                meta:resourcekey="lblRuleGroupDescriptions_RulesGroupUpdate" CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input id="txtRuleGroupName_RulesGroupUpdate" type="text" runat="server" style="width: 99%"
                                class="TextBoxes" />
                        </td>
                        <td>
                            <input id="txtRuleGroupDescriptions_RulesGroupUpdate" type="text" style="width: 99%"
                                class="TextBoxes" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td style="width: 45%">
                            <table style="width: 100%" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_RulesTemplates_RulesGroupUpdate" class="HeaderLabel" style="width: 35%">
                                                    Rules Templates
                                                </td>
                                                <td id="loadingPanel_trvRulesTemplates_RulesGroupUpdate" class="HeaderLabel" style="width: 60%">
                                                </td>
                                                <td id="Td2" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_trvRulesTemplates_RulesGroupUpdate" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvRulesTemplates_RulesGroupUpdate"
                                                                runat="server" ClientSideCommand="Refresh_trvRulesTemplates_RulesGroupUpdate();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvRulesTemplates_RulesGroupUpdate"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_trvRulesTemplates_RulesGroupUpdate"
                                            OnCallback="CallBack_trvRulesTemplates_RulesGroupUpdate_onCallBack">
                                            <Content>
                                                <ComponentArt:TreeView ID="trvRulesTemplates_RulesGroupUpdate" runat="server" BorderColor="Black"
                                                    CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                    DefaultImageWidth="16" DragAndDropEnabled="true" EnableViewState="false" ExpandCollapseImageHeight="15"
                                                    ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" FillContainer="false"
                                                    Height="300" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true"
                                                    LineImageHeight="20" LineImageWidth="19" meta:resourcekey="trvRulesTemplates_RulesGroupUpdate"
                                                    NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                    SelectedNodeCssClass="SelectedTreeNode" ShowLines="true">
                                                    <ClientEvents>
                                                        <Load EventHandler="trvRulesTemplates_RulesGroupUpdate_onLoad" />
                                                        <ContextMenu EventHandler="trvRulesTemplates_RulesGroupUpdate_onContextMenu" />
                                                        <NodeSelect EventHandler="trvRulesTemplates_RulesGroupUpdate_onNodeSelect" />
                                                    </ClientEvents>
                                                </ComponentArt:TreeView>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_RulesTemplates_RulesGroupUpdate" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_trvRulesTemplates_RulesGroupUpdate_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_trvRulesTemplates_RulesGroupUpdate_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 10%" align="center">
                            <ComponentArt:ToolBar ID="TlbInterAction_RulesGroupUpdate" runat="server" CssClass="verticaltoolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" Orientation="Vertical" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemAdd_TlbInterAction_RulesGroupUpdate" runat="server"
                                        ClientSideCommand="tlbItemAdd_TlbInterAction_RulesGroupUpdate_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemAdd_TlbInterAction_RulesGroupUpdate"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbInterAction_RulesGroupUpdate" runat="server"
                                        ClientSideCommand="tlbItemDelete_TlbInterAction_RulesGroupUpdate_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDelete_TlbInterAction_RulesGroupUpdate"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td style="width: 45%">
                            <table style="width: 100%" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_Rules_RulesGroupUpdate" class="HeaderLabel" style="width: 45%">
                                                    Rules
                                                </td>
                                                <td id="loadingPanel_trvRules_RulesGroupUpdate" class="HeaderLabel" style="width: 50%">
                                                </td>
                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_trvRules_RulesGroupUpdate" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvRules_RulesGroupUpdate"
                                                                runat="server" ClientSideCommand="Refresh_trvRules_RulesGroupUpdate();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvRules_RulesGroupUpdate"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_trvRules_RulesGroupUpdate" OnCallback="CallBack_trvRules_RulesGroupUpdate_onCallBack">
                                            <Content>
                                                <ComponentArt:TreeView ID="trvRules_RulesGroupUpdate" runat="server" BorderColor="Black"
                                                    CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                    DefaultImageWidth="16" DragAndDropEnabled="true" EnableViewState="false" ExpandCollapseImageHeight="15"
                                                    ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" FillContainer="false"
                                                    Height="300" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true"
                                                    LineImageHeight="20" LineImagesFolderUrl="Images/TreeView/LeftLines" LineImageWidth="19"
                                                    meta:resourcekey="trvRules_RulesGroupUpdate" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit"
                                                    NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode"
                                                    ShowLines="true">
                                                    <ClientEvents>
                                                        <Load EventHandler="trvRules_RulesGroupUpdate_onLoad" />
                                                        <ContextMenu EventHandler="trvRules_RulesGroupUpdate_onContextMenu" />
                                                        <NodeSelect EventHandler="trvRules_RulesGroupUpdate_onNodeSelect" />
                                                    </ClientEvents>
                                                </ComponentArt:TreeView>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Rules_RulesGroupUpdate" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_trvRules_RulesGroupUpdate_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_trvRules_RulesGroupUpdate_onCallbackError" />
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
                                    <td>
                                        <asp:Label ID="lblRuleTemplateDescription_RulesGroupUpdate" runat="server" Text=": شرح الگوی قانون"
                                            meta:resourcekey="lblRuleTemplateDescription_RulesGroupUpdate" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <textarea id="txtRuleTemplateDescription_RulesGroupUpdate" cols="20" name="S1" rows="3"
                                            class="TextBoxes" style="width: 99%; height: 75px" readonly="readonly"></textarea>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRuleDescription_RulesGroupUpdate" runat="server" Text=": شرح قانون"
                                            meta:resourcekey="lblRuleDescription_RulesGroupUpdate" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <textarea id="txtRuleDescription_RulesGroupUpdate" cols="20" name="S2" rows="3" class="TextBoxes"
                                            style="width: 99%; height: 75px" readonly="readonly"></textarea>
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
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogRuleParameters"
        HeaderClientTemplateId="DialogRuleParametersheader" FooterClientTemplateId="DialogRuleParametersfooter"
        runat="server" PreloadContentUrl="false" ContentUrl="RuleParameters.aspx" IFrameCssClass="RuleParameters_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogRuleParametersheader">
                <table id="tbl_DialogRuleParametersheader" style="width: 703px;" cellpadding="0"
                    cellspacing="0" border="0" onmousedown="DialogRuleParameters.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogRuleParameters_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogRuleParameters" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold;">
                                    </td>
                                    <td id="CloseButton_DialogRuleParameters" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogRuleParameters_IFrame').src = 'WhitePage.aspx'; DialogRuleParameters.Close('cancelled');" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogRuleParameters_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogRuleParametersfooter">
                <table id="tbl_DialogRuleParametersfooter" style="width: 703px" cellpadding="0" cellspacing="0"
                    border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogRuleParameters_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogRuleParameters_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogRuleParameters_onShow" />
            <OnClose EventHandler="DialogRuleParameters_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
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
                        <img runat="server" alt="" src="~/Images/Dialog/Waiting.gif" />
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogWaiting_onShow" />
        </ClientEvents>
    </ComponentArt:Dialog>    
    <asp:HiddenField runat="server" ID="hfheader_RulesTemplates_RulesGroupUpdate" meta:resourcekey="hfheader_RulesTemplates_RulesGroupUpdate" />
    <asp:HiddenField runat="server" ID="hfheader_Rules_RulesGroupUpdate" meta:resourcekey="hfheader_Rules_RulesGroupUpdate" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvRulesTemplates_RulesGroupUpdate"
        meta:resourcekey="hfloadingPanel_trvRulesTemplates_RulesGroupUpdate" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvRules_RulesGroupUpdate" meta:resourcekey="hfloadingPanel_trvRules_RulesGroupUpdate" />
    <asp:HiddenField runat="server" ID="hfView_RulesGroupUpdate" meta:resourcekey="hfView_RulesGroupUpdate" />
    <asp:HiddenField runat="server" ID="hfAdd_RulesGroupUpdate" meta:resourcekey="hfAdd_RulesGroupUpdate" />
    <asp:HiddenField runat="server" ID="hfEdit_RulesGroupUpdate" meta:resourcekey="hfEdit_RulesGroupUpdate" />
    <asp:HiddenField runat="server" ID="hfDelete_RulesGroupUpdate" meta:resourcekey="hfDelete_RulesGroupUpdate" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_RulesGroupUpdate" meta:resourcekey="hfDeleteMessage_RulesGroupUpdate" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_RulesGroupUpdate" meta:resourcekey="hfCloseMessage_RulesGroupUpdate" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogRulesGroupUpdate" meta:resourcekey="hfTitle_DialogRulesGroupUpdate" />
    <asp:HiddenField runat="server" ID="hfErrorType_DialogRulesGroupUpdate" meta:resourcekey="hfErrorType_DialogRulesGroupUpdate" />
    <asp:HiddenField runat="server" ID="hfConnectionError_DialogRulesGroupUpdate" meta:resourcekey="hfConnectionError_DialogRulesGroupUpdate" />
    </form>
</body>
</html>
