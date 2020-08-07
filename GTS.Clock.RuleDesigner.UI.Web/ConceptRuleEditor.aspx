<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConceptRuleEditor.aspx.cs" Inherits="GTS.Clock.RuleDesigner.UI.Web.ConceptRuleEditor" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link id="Link3" href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link4" href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link5" href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link1" href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link6" href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link7" href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link9" href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <%--<script type="text/javascript" src="JS/json.js"></script>--%>
    <form id="conceptEditorForm" runat="server" meta:resourcekey="conceptEditorForm">
        <div>
            <table style="width: 95%">
                <tbody>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td runat="server" style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbConcepts" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbConceptSave_TlbConcepts" runat="server" ClientSideCommand="tlbConceptSave_TlbConcepts_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                                    ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbConceptSave_TlbConcepts"
                                                    TextImageSpacing="5" />
                                                <%-- <ComponentArt:ToolBarItem ID="tlbConceptSaveAs_TlbConcepts" runat="server" ClientSideCommand="tlbConceptSaveAs_TlbConcepts_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="saveas.png"
                                                ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbConceptSaveAs_TlbConcepts"
                                                TextImageSpacing="5" />--%>
                                                <%-- <ComponentArt:ToolBarItem ID="tlbConceptPattern_TlbConcepts" runat="server" ClientSideCommand="tlbConceptPattern_TlbConcepts_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="template.png"
                                                ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbConceptPattern_TlbConcepts"
                                                TextImageSpacing="5" />--%>
                                                <ComponentArt:ToolBarItem ID="tlbConceptHelp_TlbConcepts" runat="server" ClientSideCommand="tlbConceptHelp_TlbConcepts_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                                    ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbConceptHelp_TlbConcepts"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbConcepts" runat="server" DropDownImageHeight="16px"
                                                    ClientSideCommand="tlbConceptExit_TlbConcepts_onClick();" DropDownImageWidth="16px"
                                                    ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ConceptType="Command"
                                                    meta:resourcekey="tlbConceptExit" TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="ActionMode_Concepts" class="ToolbarMode"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbConceptsExpression" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbConceptExpressionReferesh_TlbConcepts" runat="server"
                                                    ClientSideCommand="tlbConceptExpressionReferesh_TlbConcepts_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ConceptType="Command" meta:resourcekey="tlbConceptExpressionReferesh" TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td></td>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbConceptsDetails" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <%--<ComponentArt:ToolBarItem ID="TlbConceptsDetails_Delete_TlbConcepts" runat="server"
                                                ClientSideCommand="TlbConceptsDetails_Delete_TlbConcepts_onClick();" DropDownImageHeight="16px"
                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                                ConceptType="Command" meta:resourcekey="TlbConceptsDetails_Delete_TlbConcepts" TextImageSpacing="5" />--%>
                                                <ComponentArt:ToolBarItem ID="TlbConceptsDetails_Refresh_JsonObj_TlbConcepts" runat="server"
                                                    ClientSideCommand="TlbConceptsDetails_Refresh_JsonObj_TlbConcepts_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="flow.png"
                                                    ImageWidth="16px" ConceptType="Command" meta:resourcekey="TlbConceptsDetails_Refresh_JsonObj_TlbConcepts"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                                <tr style="width: 100%;">
                                    <td style="width: 45%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_trvConceptExpression_Concept"
                                            OnCallback="CallBack_trvConceptExpression_Concept_onCallBack">
                                            <Content>
                                                <ComponentArt:TreeView ID="trvConceptExpression_Concept" runat="server" ExpandNodeOnSelect="false"
                                                    CollapseNodeOnSelect="false" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                                    DefaultImageHeight="16" HighlightSelectedPath="true" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                    EnableViewState="true" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                    ExpandImageUrl="images/TreeView/col.gif" FillContainer="false" ForceHighlightedNodeID="true"
                                                    Height="330" HoverNodeCssClass="HoverNestingTreeNode" ItemSpacing="2" KeyboardEnabled="true"
                                                    LineImageHeight="20" LineImageWidth="19" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit"
                                                    LoadingFeedbackText="Loading" NodeIndent="17" NodeLabelPadding="3" meta:resourcekey="trvConceptExpression_Concept"
                                                    SelectedNodeCssClass="SelectedTreeNode" ShowLines="true">
                                                    <ClientEvents>
                                                        <NodeSelect EventHandler="trvConceptExpression_Concept_onNodeSelect" />
                                                        <Load EventHandler="trvConceptExpression_Concept_onLoad" />
                                                        <CallbackComplete EventHandler="trvConceptExpression_Concept_onCallbackComplete" />
                                                        <NodeBeforeExpand EventHandler="trvConceptExpression_Concept_onNodeBeforeExpand" />
                                                        <NodeRename EventHandler="trvConceptExpression_Concept_onNodeRename" />
                                                        <NodeMouseDoubleClick EventHandler="trvConceptExpression_Concept_onNodeMouseDoubleClick" />
                                                    </ClientEvents>
                                                </ComponentArt:TreeView>
                                                <asp:HiddenField ID="ErrorHiddenField_trvConceptExpression_Concep" runat="server" />
                                                <asp:HiddenField ID="VariableItemCodeInExpression" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_trvConceptExpression_Concept_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_trvConceptExpression_Concept_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td style="width: 10%;" align="center" valign="middle">
                                        <ComponentArt:ToolBar ID="TlbInterAction_ConceptRuleEditor" runat="server"
                                            CssClass="verticaltoolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            Orientation="Vertical" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item"
                                            DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px"
                                            DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                            ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemAdd_TlbInterAction_ConceptRuleEditor"
                                                    runat="server" ClientSideCommand="tlbItemAdd_TlbInterAction_ConceptRuleEditor_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemAdd_TlbInterAction_ConceptRuleEditor"
                                                    TextImageSpacing="5" Enabled="true" />
                                                <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbInterAction_ConceptRuleEditor"
                                                    runat="server" ClientSideCommand="tlbItemDelete_TlbInterAction_ConceptRuleEditor_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemDelete_TlbInterAction_ConceptRuleEditor"
                                                    TextImageSpacing="5" Enabled="true" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td style="width: 45%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_trvDetails_Concept" OnCallback="CallBack_trvDetails_Concept_onCallBack">
                                            <Content>
                                                <ComponentArt:TreeView ID="trvDetails_Concept" runat="server" ExpandNodeOnSelect="true"
                                                    CollapseNodeOnSelect="false" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                                    DefaultImageHeight="16" HighlightSelectedPath="true" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                    EnableViewState="true" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                    ExpandImageUrl="images/TreeView/col.gif" FillContainer="false" ForceHighlightedNodeID="true"
                                                    Height="330" HoverNodeCssClass="HoverNestingTreeNode" ItemSpacing="2" KeyboardEnabled="true"
                                                    LineImageHeight="20" LineImageWidth="19" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit"
                                                    LoadingFeedbackText="Loading" NodeIndent="17" NodeLabelPadding="3" meta:resourcekey="trvPosts_Post"
                                                    SelectedNodeCssClass="SelectedTreeNode" ShowLines="true">
                                                    <ClientEvents>
                                                        <NodeSelect EventHandler="trvDetails_Concept_onNodeSelect" />
                                                        <Load EventHandler="trvDetails_Concept_onLoad" />
                                                        <CallbackComplete EventHandler="trvDetails_Concept_onCallbackComplete" />
                                                        <NodeBeforeExpand EventHandler="trvDetails_Concept_onNodeBeforeExpand" />
                                                        <NodeBeforeRename EventHandler="trvDetails_Concept_onNodeBeforeRename" />
                                                        <NodeRename EventHandler="trvDetails_Concept_onNodeRename" />
                                                    </ClientEvents>
                                                </ComponentArt:TreeView>
                                                <asp:HiddenField ID="ErrorHiddenField_trvDetails_Concept" runat="server" />
                                                <asp:HiddenField ID="ObjectJsonHiddenField_trDetails_Concept" runat="server" />
                                                <asp:HiddenField ID="hfPageIsLoadedBefore" runat="server" />
                                                <asp:HiddenField ID="hfConceptOrRuleIdentification" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_trvDetails_Concept_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_trvDetails_Concept_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td id="ConceptExpressioncriptFa" class="CellText" style="width: 100%; direction: rtl;"></td>
                    </tr>
                    <tr style="visibility: hidden;">
                        <td id="ConceptExpressioncript" class="CellText" style="width: 100%; direction: ltr;"></td>
                    </tr>

                    <tr style="visibility: hidden;">
                        <td id="ConceptExpressioncriptFull" class="CellText" style="width: 100%; direction: ltr;"></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
            runat="server" Width="280px">
            <Content>
                <table id="tblConfirm_DialogConfirm" style="width: 100%;" class="ConfirmStyle">
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
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancelConfirm"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>
        <asp:HiddenField ID="hfADD_Concepts" runat="server" meta:resourcekey="hfNew_Concepts" />
        <asp:HiddenField ID="hfEdit_Concepts" runat="server" meta:resourcekey="hfEdit_Concepts" />
        <asp:HiddenField ID="hfCloseMessage_Concepts" runat="server" meta:resourcekey="hfCloseMessage_Concepts" />
        <asp:HiddenField ID="hfConnectionError_Concepts" runat="server" meta:resourcekey="hfConnectionError_Concepts" />
        <asp:HiddenField ID="hfDeleteMessage_Concepts" runat="server" meta:resourcekey="hfDeleteMessage_Concepts" />
        <asp:HiddenField ID="hfErrorType_Concepts" runat="server" meta:resourcekey="hfErrorType_Concepts" />
    </form>
</body>
</html>
