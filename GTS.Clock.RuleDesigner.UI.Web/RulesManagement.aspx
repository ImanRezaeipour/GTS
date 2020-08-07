<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RulesManagement.aspx.cs" Inherits="GTS.Clock.RuleDesigner.UI.Web.RulesManagement" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link id="Link1" href="css/colorPickerStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link2" href="css/combobox.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link3" href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link4" href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link5" href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link6" href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link7" href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link8" href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link9" href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <%--    <script src="JS/API/RulesManagement_onPageLoad.js" type="text/javascript"></script>
    <script src="JS/API/DialogRulesManagement_Operations.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="JS/API/Alert_Box.js"></script>
    <script src="JS/API/ColorPicker.js" type="text/javascript"></script>
    <form id="RulesManagementForm" runat="server" dir="rtl">
        <table style="width: 97%; height: 400px; font-family: Arial; font-size: small; text-align: right;" class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 85%;">
                                            <ComponentArt:ToolBar ID="TlbRules" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbRules" runat="server" ClientSideCommand="tlbRuleNew_TlbRules_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                                        ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbRuleNew_TlbRules"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbRules" runat="server" ClientSideCommand="tlbRuleEdit_TlbRules_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                                        ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbRuleEdit_TlbRules"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbRules" runat="server" DropDownImageHeight="16px"
                                                        ClientSideCommand="tlbRuleDelete_TlbRules_onClick();" DropDownImageWidth="16px"
                                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" RuleType="Command"
                                                        meta:resourcekey="tlbRuleDelete" TextImageSpacing="5" />
                                                    <%-- <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbRules" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                            RuleType="Command" meta:resourcekey="tlbRuleHelp" TextImageSpacing="5"
                                            ClientSideCommand="tlbRuleHelp_TlbRules_onClick();" />--%>
                                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbRules" runat="server" DropDownImageHeight="16px"
                                                        ClientSideCommand="tlbRuleSave_TlbRules_onClick();" DropDownImageWidth="16px"
                                                        ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px" RuleType="Command"
                                                        meta:resourcekey="tlbRuleSave" TextImageSpacing="5" Enabled="false" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbTlbRules" runat="server" ClientSideCommand="tlbRuleCancel_TlbTlbRules_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                                        ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbRuleCancel" TextImageSpacing="5"
                                                        Enabled="false" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemDefine_TlbTlbRules" runat="server" ClientSideCommand="tlbRuleDefine_TlbTlbRules_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="view_detailed.png"
                                                        ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbRuleDefine" TextImageSpacing="5"
                                                        Enabled="false" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbRule" runat="server"
                                                        ClientSideCommand="tlbItemFormReconstruction_TlbRule_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbRule" TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbRules" runat="server" DropDownImageHeight="16px"
                                                        ClientSideCommand="tlbRuleExit_TlbRules_onClick();" DropDownImageWidth="16px"
                                                        ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" RuleType="Command"
                                                        meta:resourcekey="tlbRuleExit" TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                        <td style="width: 15%;" class="ToolbarMode">
                                            <span id="ActionMode_Rules"></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table style="width: 99%;" class="BoxStyle">
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    <asp:Label ID="lblQuickSerch_Rule" runat="server" meta:resourcekey="lblQuickSerch_Rule" Text=": جستجوی سریع" CssClass="WhiteLabel"></asp:Label>
                                                                </td>
                                                                <td style="width: 80%">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtSerchTerm_Rule" />
                                                                            </td>
                                                                            <td style="width: 5%">
                                                                                <ComponentArt:ToolBar ID="tlbItemQuickSearch" runat="server" CssClass="toolbar"
                                                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                    UseFadeEffect="false">
                                                                                    <Items>
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbRuleQuickSearch" runat="server"
                                                                                            ClientSideCommand="tlbItemSearch_TlbRuleQuickSearch_onClick();" DropDownImageHeight="16px"
                                                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                                                            ItemType="Command" meta:resourcekey="tlbItemSearch_TlbRuleQuickSearch" TextImageSpacing="5" />
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
                                                <tr>
                                                    <td style="width: 60%">
                                                        <table style="width: 99%;" class="BoxStyle">
                                                            <tr>
                                                                <td>
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td id="" class="HeaderLabel" style="width: 95%">
                                                                                <table style="width: 100%;">
                                                                                    <tr>
                                                                                        <td id="header_RulesBox_Rules" class="HeaderLabel" style="width: 50%;">Rules
                                                                                        </td>
                                                                                        <td id="loadingPanel_GridRules_Rules" class="HeaderLabel" style="width: 45%"></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%">
                                                                    <ComponentArt:CallBack ID="CallBack_GridRules_Rule" OnCallback="CallBack_GridRules_Rule_OnCallBack"
                                                                        runat="server">
                                                                        <Content>
                                                                            <ComponentArt:DataGrid ID="GridRules_Rules" runat="server" AllowHorizontalScrolling="true"
                                                                                CssClass="Grid" EnableViewState="true" ShowFooter="false" FillContainer="true"
                                                                                FooterCssClass="GridFooter" Height="150" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                                                                PageSize="5" RunningMode="Client" AllowMultipleSelect="false" AllowColumnResizing="false"
                                                                                ScrollBar="Off" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                                                ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                                                ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                                                ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="200">
                                                                                <Levels>
                                                                                    <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                                                        AllowSorting="false" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                                                        RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                                                        SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                                                        DataKeyField="ID" SortImageWidth="9" HoverRowCssClass="HoverRow">
                                                                                        <Columns>
                                                                                            <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                                            <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending" Width="110" meta:resourcekey="GridColumnRuleName" HeadingTextCssClass="HeadingText" />
                                                                                            <%--<ComponentArt:GridColumn Align="Center" DataField="Script" DefaultSortDirection="Descending" Width="110" meta:resourcekey="GridColumnRuleScript" HeadingTextCssClass="HeadingText" />--%>
                                                                                            <ComponentArt:GridColumn Align="Center" DataField="IdentifierCode" DefaultSortDirection="Descending" Width="110" meta:resourcekey="GridColumnRuleIdentifierCode" HeadingTextCssClass="HeadingText" />
                                                                                            <ComponentArt:GridColumn DataField="TypeId" Visible="false" />
                                                                                            <ComponentArt:GridColumn Align="Center" DataField="Type" DefaultSortDirection="Descending" Width="110" meta:resourcekey="GridColumnRuleType" HeadingTextCssClass="HeadingText" />
                                                                                            <ComponentArt:GridColumn DataField="UserDefined" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="Script" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="CSharpCode" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="JsonObject" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="CustomCategoryCode" Visible="false" />
                                                                                        </Columns>
                                                                                    </ComponentArt:GridLevel>
                                                                                </Levels>
                                                                                <ClientEvents>
                                                                                    <ItemSelect EventHandler="GridRules_Rules_onItemSelect" />
                                                                                    <Load EventHandler="GridRules_Rules_onLoad" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:DataGrid>
                                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_Rules" />
                                                                            <asp:HiddenField runat="server" ID="hfRulesCount_Rules" />
                                                                            <asp:HiddenField runat="server" ID="hfRulesPageCount_Rules" />
                                                                        </Content>
                                                                        <ClientEvents>
                                                                            <CallbackComplete EventHandler="CallBack_GridRules_Rule_OnCallbackComplete" />
                                                                            <CallbackError EventHandler="CallBack_GridRules_Rule_onCallbackError" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:CallBack>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td id="Td1" runat="server" meta:resourcekey="AlignObj" style="width: 75%;">
                                                                                <ComponentArt:ToolBar runat="server" ID="TlbPaging_GridRules_Rules" CssClass="toolbar"
                                                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                    Style="direction: ltr" UseFadeEffect="false">
                                                                                    <Items>
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridRules_Rules"
                                                                                            runat="server" ClientSideCommand="tlbRuleRefresh_TlbPaging_GridRules_Rules_onClick();"
                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                            ImageUrl="refresh.png" ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbRuleRefresh_TlbPaging_GridRules_Rules"
                                                                                            TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridRules_Rules" runat="server"
                                                                                            ClientSideCommand="tlbRuleFirst_TlbPaging_GridRules_Rules_onClick();"
                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                            ImageUrl="first.png" ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbRuleFirst_TlbPaging_GridRules_Rules"
                                                                                            TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridRules_Rules" runat="server"
                                                                                            ClientSideCommand="tlbRuleBefore_TlbPaging_GridRules_Rules_onClick();"
                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                            ImageUrl="Before.png" ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbRuleBefore_TlbPaging_GridRules_Rules"
                                                                                            TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridRules_Rules" runat="server"
                                                                                            ClientSideCommand="tlbRuleNext_TlbPaging_GridRules_Rules_onClick();"
                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                            ImageUrl="Next.png" ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbRuleNext_TlbPaging_GridRules_Rules"
                                                                                            TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridRules_Rules" runat="server"
                                                                                            ClientSideCommand="tlbRuleLast_TlbPaging_GridRules_Rules_onClick();"
                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                            ImageUrl="last.png" ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbRuleLast_TlbPaging_GridRules_Rules"
                                                                                            TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                                    </Items>
                                                                                </ComponentArt:ToolBar>
                                                                            </td>
                                                                            <td id="footer_GridRules_Rules" class="WhiteLabel" style="width: 25%"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 40%">
                                                        <table style="width: 99%;" class="BoxStyle">
                                                            <tr>
                                                                <td class="DetailsBoxHeaderStyle" colspan="3">
                                                                    <div id="Div1" runat="server" meta:resourcekey="AlignObj"
                                                                        class="BoxContainerHeader" style="color: White; width: 100%; height: 100%">
                                                                        جزئیات قانون
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="WhiteLabel">
                                                                    <asp:Label ID="lblRuleName" runat="server" meta:resourcekey="lblRuleName"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input id="txtRuleName" type="text" runat="server" style="width: 99%;" class="TextBoxes" disabled="disabled"
                                                                        onclick="this.select();" onfocus="this.select();" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="WhiteLabel">
                                                                    <asp:Label ID="lblRuleCode" runat="server" meta:resourcekey="lblRuleCode"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input id="txtRuleCode" type="text" runat="server" style="width: 99%;" class="TextBoxes" disabled="disabled"
                                                                        onclick="this.select();" onfocus="this.select();" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="WhiteLabel">
                                                                    <asp:Label ID="lblRuleType" runat="server" meta:resourcekey="lblRuleType"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td>
                                                                                <ComponentArt:CallBack runat="server" ID="CallBack_cmbRuleType_Rules" OnCallback="CallBack_cmbRuleType_Rules_onCallBack"
                                                                                    Height="26">
                                                                                    <Content>
                                                                                        <ComponentArt:ComboBox ID="cmbRuleType_Rules" runat="server" AutoComplete="true"
                                                                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                            ItemCssClass="comboitem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                            Style="width: 99%" TextBoxCssClass="comboTextBox" Enabled="false">
                                                                                            <ClientEvents>
                                                                                                <Expand EventHandler="cmbRuleType_Rules_onExpand" />
                                                                                            </ClientEvents>
                                                                                        </ComponentArt:ComboBox>
                                                                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_TypeFields" />
                                                                                    </Content>
                                                                                    <ClientEvents>
                                                                                        <BeforeCallback EventHandler="cmbRuleType_Rules_onBeforeCallback" />
                                                                                        <CallbackComplete EventHandler="cmbRuleType_Rules_onCallbackComplete" />
                                                                                        <CallbackError EventHandler="cmbRuleType_Rules_onCallbackError" />
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
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 85%;">
                                <ComponentArt:ToolBar ID="TlbRuleParameters" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbRuleParameters" runat="server" ClientSideCommand="tlbItemNew_TlbRuleParameters_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbRuleParameters"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbRuleParameters" runat="server" ClientSideCommand="tlbItemEdit_TlbRuleParameters_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbRuleParameters"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbRuleParameters" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemDelete_TlbRuleParameters_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                            meta:resourcekey="tlbItemDelete_TlbRuleParameters" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbRuleParameters" runat="server" ClientSideCommand="tlbItemSave_TlbRuleParameters_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbRuleParameters"
                                            TextImageSpacing="5" Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbRuleParameters" runat="server" Enabled="false"
                                            ClientSideCommand="tlbItemCancel_TlbRuleParameters_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemCancel_TlbRuleParameters" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRuleParameters_Rule" runat="server"
                                            ClientSideCommand="Refresh_GridRuleParameters_Rules();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRuleParameters_Rule"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>

                            <td style="width: 15%;" class="ToolbarMode">
                                <span id="ActionMode_RuleParameter_Rules"></span>
                            </td>

                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 60%">
                                <table style="width: 100%;" class="BoxStyle">
                                    <tr>
                                        <td style="color: White; width: 100%">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td id="header_RuleParameters_Rule" class="HeaderLabel" style="width: 50%">Rule Parameters
                                                    </td>
                                                    <td id="loadingPanel_GridRuleParameters_Rule" class="HeaderLabel" style="width: 45%"></td>
                                                    <td id="Td2" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <ComponentArt:CallBack ID="CallBack_GridRuleParameters_Rule" OnCallback="CallBack_GridRuleParameters_Rule_OnCallBack"
                                                runat="server">
                                                <Content>
                                                    <ComponentArt:DataGrid ID="GridRuleParameters_Rules" runat="server" AllowHorizontalScrolling="false"
                                                        CssClass="Grid" EnableViewState="true" ShowFooter="false" FillContainer="true"
                                                        FooterCssClass="GridFooter" Height="150" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                                        PageSize="5" RunningMode="Client" AllowMultipleSelect="false" AllowColumnResizing="false"
                                                        ScrollBar="On" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                        ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                        ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                        ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="100%">
                                                        <Levels>
                                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                                AllowSorting="false" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                                DataKeyField="ID" SortImageWidth="9" HoverRowCssClass="HoverRow">
                                                                <Columns>
                                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                    <ComponentArt:GridColumn DataField="RuleTemplateId" Visible="false" />
                                                                    <ComponentArt:GridColumn Align="Center" DataField="Title" DefaultSortDirection="Descending" TextWrap="True" meta:resourcekey="GridColumnRuleParameterTitle" HeadingTextCssClass="HeadingText" />
                                                                    <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending" TextWrap="True" meta:resourcekey="GridColumnRuleParameterName" HeadingTextCssClass="HeadingText" />
                                                                    <ComponentArt:GridColumn
                                                                        DataField="TypeTitle"
                                                                        Align="Center"
                                                                        DataCellClientTemplateId="DataCellClientTemplate_clmnTypeTitle_GridRuleParameter_Rules"
                                                                        DefaultSortDirection="Descending"
                                                                        TextWrap="True"
                                                                        meta:resourcekey="GridColumnRuleParameterType"
                                                                        HeadingTextCssClass="HeadingText" />
                                                                    <ComponentArt:GridColumn Visible="false" Align="Center" DataField="Type" DefaultSortDirection="Descending" TextWrap="True" meta:resourcekey="GridColumnRuleParameterType" HeadingTextCssClass="HeadingText" />
                                                                    <ComponentArt:GridColumn DataField="RuleTemplateId" Visible="false" />
                                                                </Columns>
                                                            </ComponentArt:GridLevel>
                                                        </Levels>
                                                        <ClientTemplates>
                                                            <ComponentArt:ClientTemplate runat="server" ID="DataCellClientTemplate_clmnTypeTitle_GridRuleParameter_Rules">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td align="center">##GetRuleTempParamTypeTitle_Rules(DataItem.GetMember('Type').Value)##
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ComponentArt:ClientTemplate>
                                                        </ClientTemplates>
                                                        <ClientEvents>
                                                            <ItemSelect EventHandler="GridRuleParameters_Rules_onItemSelect" />
                                                            <Load EventHandler="GridRuleParameters_Rules_onLoad" />
                                                        </ClientEvents>
                                                    </ComponentArt:DataGrid>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenFieldRuleParameter_Rules" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_GridRuleParameters_Rule_OnCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_GridRuleParameters_Rule_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 40%" valign="top">
                                <table style="width: 100%;" class="BoxStyle">
                                    <tr>
                                        <td class="DetailsBoxHeaderStyle">
                                            <div id="header_RuleParametersDetails_Rule" runat="server" meta:resourcekey="AlignObj"
                                                class="BoxContainerHeader" style="color: White; width: 100%; height: 100%">
                                                جزئیات پارامتر قانون
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblRuleParameterTitle_Rule" runat="server" meta:resourcekey="lblRuleParameterTitle_Rule"
                                                Text=": عنوان پارامتر" CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtRuleParameterTitle_Rule"
                                                disabled="disabled" onfocus="this.select();" onclick="this.select();" /></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lblRuleParameterName_Rule" runat="server" meta:resourcekey="lblRuleParameterName_Rule"
                                                Text=": نام پارامتر " CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtRuleParameterName_Rule"
                                                disabled="disabled" onfocus="this.select();" onclick="this.select();" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblRuleParameterType_Rule" runat="server" meta:resourcekey="lblRuleParameterType_Rule"
                                                Text=": نوع پارامتر" CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbRuleParameterType_Rules" OnCallback="CallBack_cmbRuleParameterType_Rules_onCallBack"
                                                            Height="26">
                                                            <Content>
                                                                <ComponentArt:ComboBox ID="cmbRuleParameterType_Rules" runat="server" AutoComplete="true"
                                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                    ItemCssClass="comboitem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                    Style="width: 99%" TextBoxCssClass="comboTextBox" Enabled="false">
                                                                    <ClientEvents>
                                                                        <Expand EventHandler="cmbRuleParameterType_Rules_onExpand" />
                                                                    </ClientEvents>
                                                                </ComponentArt:ComboBox>
                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_RuleParameterTypeFields" />
                                                            </Content>
                                                            <ClientEvents>
                                                                <BeforeCallback EventHandler="cmbRuleParameterType_Rules_onBeforeCallback" />
                                                                <CallbackComplete EventHandler="cmbRuleParameterType_Rules_onCallbackComplete" />
                                                                <CallbackError EventHandler="cmbRuleParameterType_Rules_onCallbackError" />
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
                </td>
            </tr>
        </table>

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
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancel"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="Dialog1"
            runat="server" Width="280px">
            <Content>
                <table style="width: 100%;" class="ConfirmStyle">
                    <tr align="center">
                        <td colspan="2">
                            <asp:Label ID="Label1" runat="server" CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td style="width: 50%">
                            <ComponentArt:ToolBar ID="ToolBar1" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="ToolBarItem1" runat="server" ClientSideCommand="tlbItemOk_TlbOkConfirm_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemOk_TlbOkConfirm"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td>
                            <ComponentArt:ToolBar ID="ToolBar2" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="ToolBarItem2" runat="server" ClientSideCommand="tlbItemCancel_TlbCancelConfirm_onClick();"
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
        <ComponentArt:CallBack runat="server" ID="CallBackSaveRules_Rules" OnCallback="CallBackSaveRules_Rules_OnCallBack">
            <Content>
                <asp:HiddenField runat="server" ID="hfCallBackDataSaveRules_Rules" />
            </Content>
            <ClientEvents>
                <CallbackComplete EventHandler="CallBackSaveRules_Rules_onCallbackComplete" />
                <CallbackError EventHandler="CallBackSaveRules_Rules_onCallbackError" />
            </ClientEvents>
        </ComponentArt:CallBack>
        <iframe id="hidden_IFrame_Rules" runat="server" style="width: 0px; height: 0px"></iframe>
        <!-- Titles -->
        <asp:HiddenField runat="server" ID="hfHeaderRule_Rules" meta:resourcekey="hfRule_Rules" />
        <asp:HiddenField runat="server" ID="hfHeaderRuleParameter_Rules" meta:resourcekey="hfRuleParameter_Rules" />

        <!-- /Titles -->
        <!-- State -->
        <asp:HiddenField runat="server" ID="hfStateView_Rules" meta:resourcekey="hfView_Rules" />
        <asp:HiddenField runat="server" ID="hfStateAdd_Rules" meta:resourcekey="hfAdd_Rules" />
        <asp:HiddenField runat="server" ID="hfStateEdit_Rules" meta:resourcekey="hfEdit_Rules" />
        <asp:HiddenField runat="server" ID="hfStateDelete_Rules" meta:resourcekey="hfDelete_Rules" />
        <asp:HiddenField runat="server" ID="hfStateErrorType_Rules" meta:resourcekey="hfErrorType_Rules" />
        <asp:HiddenField runat="server" ID="hfStateConnectionError_Rules" meta:resourcekey="hfConnectionError_Rules" />
        <asp:HiddenField runat="server" ID="hfDeleteMessageRule_Rules" meta:resourcekey="hfDeleteMessage_Rules" />
        <asp:HiddenField runat="server" ID="hfDeleteMessageRuleParam_Rules" meta:resourcekey="hfDeleteMessageRuleParam_Rules" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_Rules" meta:resourcekey="hfCloseMessage_Rules" />

        <!-- /State -->
        <asp:HiddenField runat="server" ID="hfRulesPageSize_Rules" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridRules_Rules" meta:resourcekey="hfloadingPanel_GridRules_Rules" />
        <!-- Enums ErrorHiddenField_TypeFields -->
        <asp:HiddenField runat="server" ID="hfJsonEnum_Type" />
        <asp:HiddenField runat="server" ID="hfJsonRuleParameterTypeEnum" />
    </form>
</body>
</html>

