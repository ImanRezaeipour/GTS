<%@ Page AutoEventWireup="true" Inherits="GTS.Clock.RuleDesigner.UI.Web.ConceptRuleMasterOperation" CodeBehind="ConceptRuleMasterOperation.aspx.cs" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="ConceptRuleMasterOperationForm" runat="server" meta:resourcekey="ConceptRuleMasterOperationForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 100%; font-family: Arial; font-size: small;">
            <tr>
                <td>
                    <ComponentArt:ToolBar ID="TlbConceptRuleMasterOperation" runat="server" CssClass="toolbar"
                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                        UseFadeEffect="false">
                        <Items>
                            <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbConceptRuleMasterOperation" runat="server"
                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbConceptRuleMasterOperation"
                                TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbConceptRuleMasterOperation_onClick();" />
                            <ComponentArt:ToolBarItem ID="tlbItemCompileConceptAndRule_TlbConceptRuleMasterOperation"
                                runat="server" ClientSideCommand="tlbItemCompileConceptAndRule_TlbConceptRuleMasterOperation_onClick();"
                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCompileConceptAndRule_TlbConceptRuleMasterOperation"
                                TextImageSpacing="5" />
                            <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbConceptRuleMasterOperation"
                                runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbConceptRuleMasterOperation_onClick();"
                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbConceptRuleMasterOperation"
                                TextImageSpacing="5" />
                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbConceptRuleMasterOperation" runat="server"
                                DropDownImageHeight="16px" ClientSideCommand="tlbItemExit_TlbConceptRuleMasterOperation();"
                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                ItemType="Command" meta:resourcekey="tlbItemExit_TlbConceptRuleMasterOperation"
                                TextImageSpacing="5" />
                        </Items>
                    </ComponentArt:ToolBar>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; height: 280px">
                        <tr>
                            <td align="center">
                                <table>
                                    <tr>
                                        <td>
                                            <ComponentArt:ToolBar ID="TlbConcepts_ConceptRuleMasterOperation" runat="server"
                                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="64px"
                                                DefaultItemImageWidth="64px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                                ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemConcepts_TlbConcepts_ConceptRuleMasterOperation"
                                                        runat="server" ClientSideCommand="tlbItemConcepts_TlbConcepts_ConceptRuleMasterOperation_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="64px" ImageUrl="Concepts.png"
                                                        ImageWidth="64px" ItemType="Command" meta:resourcekey="tlbItemConcepts_TlbConcepts_ConceptRuleMasterOperation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="center">
                                <table>
                                    <tr>
                                        <td>
                                            <ComponentArt:ToolBar ID="TlbRules_ConceptRuleMasterOperation" runat="server"
                                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="64px"
                                                DefaultItemImageWidth="64px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                                ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRules_TlbRules_ConceptRuleMasterOperation"
                                                        runat="server" ClientSideCommand="tlbItemRules_TlbRules_ConceptRuleMasterOperation_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="64px" ImageUrl="Rules.png"
                                                        ImageWidth="64px" ItemType="Command" meta:resourcekey="tlbItemRules_TlbRules_ConceptRuleMasterOperation"
                                                        TextImageSpacing="5" />
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
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancelConfirm"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfCloseMessage_ConceptRuleMasterOperation"
            meta:resourcekey="hfCloseMessage_ConceptRuleMasterOperation" />
    </form>
</body>
</html>
