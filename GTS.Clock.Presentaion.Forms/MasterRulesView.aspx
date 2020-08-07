<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterRulesView.aspx.cs"
    Inherits="MasterRulesView" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="MasterRulesViewForm" runat="server" meta:resourcekey="MasterRulesViewForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 99%; font-family: Arial; font-size: small;" class="BoxStyle">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table style="width: 100%; border: outset 1px black;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <ComponentArt:ToolBar ID="TlbMasterRulesView" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                                        DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                                        DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                                        DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbMasterRulesView" runat="server"
                                                                ClientSideCommand="tlbItemFormReconstruction_TlbMasterRulesView_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbMasterRulesView"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbMasterRulesView" runat="server" ClientSideCommand="tlbItemExit_TlbMasterRulesView_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbMasterRulesView"
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
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td id="header_GridRules_MasterRulesView" class="HeaderLabel" style="width: 50%">
                                                                Rules
                                                            </td>
                                                            <td id="loadingPanel_GridRules_MasterRulesView" class="HeaderLabel" style="width: 45%">
                                                            </td>
                                                            <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                                <ComponentArt:ToolBar ID="TlbRefresh_GridRules_MasterRulesView" runat="server" CssClass="toolbar"
                                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRules_MasterRulesView"
                                                                            runat="server" ClientSideCommand="Refresh_GridRules_MasterRulesView();" DropDownImageHeight="16px"
                                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                            ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRules_MasterRulesView"
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
                                                    <iframe id="RulesView_iFrame" runat="server" align="middle" allowtransparency="true" class="RulesView_iFrame"
                                                        frameborder="0" src="RulesView.aspx" name="I1"></iframe>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 49%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_RuleDateRanges_MasterRulesView" class="HeaderLabel" style="width: 50%">
                                                    Rule Date Ranges
                                                </td>
                                                <td id="loadingPanel_GridRuleDateRanges_MasterRulesView" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridRuleDateRanges_MasterRulesView" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRuleDateRanges_MasterRulesView"
                                                                runat="server" ClientSideCommand="Refresh_GridRuleDateRanges_MasterRulesView();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRuleDateRanges_MasterRulesView"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridRuleDateRanges_MasterRulesView"
                                            OnCallback="CallBack_GridRuleDateRanges_MasterRulesView_onCallback">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridRuleDateRanges_MasterRulesView" runat="server" CssClass="Grid"
                                                    EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                    PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="4" RunningMode="Client"
                                                    SearchTextCssClass="GridHeaderText" Width="100%" AllowMultipleSelect="false"
                                                    ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                    ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                            HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                                                            SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                            SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="TheFromDate" DefaultSortDirection="Descending"
                                                                    HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromDate_GridRuleDateRanges_MasterRulesView"
                                                                    Visible="true" Width="75" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="TheToDate" DefaultSortDirection="Descending"
                                                                    HeadingText="تا تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToDate_GridRuleDateRanges_MasterRulesView"
                                                                    Visible="true" Width="75" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridRuleDateRanges_MasterRulesView_onLoad" />
                                                        <ItemSelect EventHandler="GridRuleDateRanges_MasterRulesView_onItemSelect" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_RuleDateRanges" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridRuleDateRanges_MasterRulesView_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridRuleDateRanges_MasterRulesView_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 2%">
                            &nbsp; &nbsp;
                        </td>
                        <td style="width: 49%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_RuleParameters_MasterRulesView" class="HeaderLabel" style="width: 50%">
                                                    Rule Parameters
                                                </td>
                                                <td id="loadingPanel_GridRuleParameters_MasterRulesView" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridRuleParameters_MasterRulesView" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRuleParameters_MasterRulesView"
                                                                runat="server" ClientSideCommand="Refresh_GridRuleParameters_MasterRulesView();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRuleParameters_MasterRulesView"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridRuleParameters_MasterRulesView"
                                            OnCallback="CallBack_GridRuleParameters_MasterRulesView_onCallback">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridRuleParameters_MasterRulesView" runat="server" CssClass="Grid"
                                                    EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                    PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="4" RunningMode="Client"
                                                    SearchTextCssClass="GridHeaderText" Width="100%" AllowMultipleSelect="false"
                                                    ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                    ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                            HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                                                            SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                            SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام پارامتر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnParameterName_GridRuleParameters_MasterRulesView"
                                                                    Visible="true" Width="75" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Value" DefaultSortDirection="Descending"
                                                                    HeadingText="مقدار پارامتر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnParameterValue_GridRuleParameters_MasterRulesView"
                                                                    Visible="true" Width="75" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridRuleParameters_MasterRulesView_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_RuleParameters" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridRuleParameters_MasterRulesView_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridRuleParameters_MasterRulesView_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblRuleSubject_MasterRulesView" runat="server" Text=": عنوان قانون" meta:resourcekey="lblRuleSubject_MasterRulesView"
                                CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <textarea id="txtRuleSubject_MasterRulesView" cols="20" name="S2" rows="1" style="width: 99%;"
                                readonly="readonly" class="TextBoxes"></textarea></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblRuleText_MasterRulesView" runat="server" Text=": متن قانون" meta:resourcekey="lblRuleText_MasterRulesView"
                                CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <textarea id="txtRulesText_MasterRulesView" cols="20" name="S1" rows="4" style="width: 99%;
                                height: 70px" readonly="readonly" class="TextBoxes"></textarea>
                        </td>
                    </tr>
                </table>
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
    <asp:HiddenField runat="server" ID="hfTitle_DialogMasterRulesView" meta:resourcekey="hfTitle_DialogMasterRulesView" />
    <asp:HiddenField runat="server" ID="hfheader_RuleDateRanges_MasterRulesView" meta:resourcekey="hfheader_RuleDateRanges_MasterRulesView" />
    <asp:HiddenField runat="server" ID="hfheader_RuleParameters_MasterRulesView" meta:resourcekey="hfheader_RuleParameters_MasterRulesView" />
    <asp:HiddenField runat="server" ID="hfErrorType_MasterRulesView" meta:resourcekey="hfErrorType_MasterRulesView" />
    <asp:HiddenField runat="server" ID="hfConnectionError_MasterRulesView" meta:resourcekey="hfConnectionError_MasterRulesView" />
    <asp:HiddenField runat="server" ID="hfheader_GridRules_MasterRulesView" meta:resourcekey="hfheader_GridRules_MasterRulesView" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridRuleDateRanges_MasterRulesView"
        meta:resourcekey="hfloadingPanel_GridRuleDateRanges_MasterRulesView" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridRuleParameters_MasterRulesView"
        meta:resourcekey="hfloadingPanel_GridRuleParameters_MasterRulesView" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_MasterRulesView" meta:resourcekey="hfCloseMessage_MasterRulesView" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridRules_MasterRulesView" meta:resourcekey="hfloadingPanel_GridRules_MasterRulesView" />
    </form>
</body>
</html>
