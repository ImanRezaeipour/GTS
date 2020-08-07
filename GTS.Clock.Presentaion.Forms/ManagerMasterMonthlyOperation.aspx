<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagerMasterMonthlyOperation.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.ManagerMasterMonthlyOperation" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="ManagerMasterMonthlyOperationForm" runat="server" meta:resourcekey="ManagerMasterMonthlyOperationForm"
    onsubmit="return false;">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table id="Mastertbl_ManagerMasterMonthlyOperation" style="width: 100%; font-family: Arial;
        font-size: small">
        <tr>
            <td>
                <ComponentArt:ToolBar ID="TlbManagerMasterMonthlyOperation" runat="server" CssClass="toolbar"
                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                    UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemGridSettings_TlbManagerMasterMonthlyOperation"
                            runat="server" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                            ImageUrl="package_settings.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemGridSettings_TlbManagerMasterMonthlyOperation"
                            TextImageSpacing="5" ClientSideCommand="tlbItemGridSettings_TlbManagerMasterMonthlyOperation_onClick();" />
                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbManagerMasterMonthlyOperation" runat="server"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbManagerMasterMonthlyOperation"
                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbManagerMasterMonthlyOperation_onClick();" />
                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbManagerMasterMonthlyOperation"
                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbManagerMasterMonthlyOperation_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbManagerMasterMonthlyOperation"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbManagerMasterMonthlyOperation" runat="server"
                            DropDownImageHeight="16px" ClientSideCommand="tlbItemExit_TlbManagerMasterMonthlyOperation();"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbManagerMasterMonthlyOperation"
                            TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td align="center">
                            <table>
                                <tr>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbGridSchema_ManagerMasterMonthlyOperation" runat="server"
                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="64px"
                                            DefaultItemImageWidth="64px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                            ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemGridSchema_TlbGridSchema_ManagerMasterMonthlyOperation"
                                                    runat="server" ClientSideCommand="tlbItemGridSchema_TlbGridSchema_ManagerMasterMonthlyOperation_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="64px" ImageUrl="GridSchema.png"
                                                    ImageWidth="64px" ItemType="Command" meta:resourcekey="tlbItemGridSchema_TlbGridSchema_ManagerMasterMonthlyOperation"
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
                                        <ComponentArt:ToolBar ID="TlbGraphicalSchema_ManagerMasterMonthlyOperation" runat="server"
                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="64px"
                                            DefaultItemImageWidth="64px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                            ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemGraphicalSchema_TlbGraphicalSchema_ManagerMasterMonthlyOperation"
                                                    runat="server" ClientSideCommand="tlbItemGraphicalSchema_TlbGraphicalSchema_ManagerMasterMonthlyOperation_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="64px" ImageUrl="GraphicalSchema.png"
                                                    ImageWidth="64px" ItemType="Command" meta:resourcekey="tlbItemGraphicalSchema_TlbGraphicalSchema_ManagerMasterMonthlyOperation"
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
        <tr>
            <td style="width: 25%" valign="top">
                &nbsp;
            </td>
            <td style="width: 75%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%">
                            <div runat="server" meta:resourcekey="AlignObj" style="font-weight: bold; width: 100%"
                                class="DropDownHeader">
                                <img id="imgbox_Conditions_ManagerMasterMonthlyOperation" runat="server" alt="" onclick="imgbox_Conditions_ManagerMasterMonthlyOperation_onClick();"
                                    src="~/Images/Ghadir/arrowDown.jpg" />
                                <span id="header_Conditions_ManagerMasterMonthlyOperation">Conditions</span>
                            </div>
                            <div id="box_Conditions_ManagerMasterMonthlyOperation" class="dhtmlgoodies_contentBox"
                                style="width: 40%;">
                                <div id="subbox_UserSearch_Users" class="dhtmlgoodies_content">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 50%">
                                                <table style="width: 95%; border: 1px outset black">
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 5%">
                                                                        <input id="chbFilter_ManagerMasterMonthlyOperation" type="checkbox" onclick="chbFilter_ManagerMasterMonthlyOperation_onClick();" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblFilter_ManagerMasterMonthlyOperation" runat="server" CssClass="WhiteLabel"
                                                                            meta:resourcekey="lblFilter_ManagerMasterMonthlyOperation" Text=": فیلتر"></asp:Label>
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
                                                                        <asp:Label ID="lblFilterBy_ManagerMasterMonthlyOperation" runat="server" CssClass="WhiteLabel"
                                                                            meta:resourcekey="lblFilterBy_ManagerMasterMonthlyOperation" Text=": فیلتر بر اساس"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <ComponentArt:CallBack ID="CallBack_cmbFilterBy_ManagerMasterMonthlyOperation" runat="server"
                                                                            OnCallback="CallBack_cmbFilterBy_ManagerMasterMonthlyOperation_onCallBack" Height="26">
                                                                            <Content>
                                                                                <ComponentArt:ComboBox ID="cmbFilterBy_ManagerMasterMonthlyOperation" runat="server"
                                                                                    AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                    DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                    ExpandDirection="Up" DropImageUrl="Images/ComboBox/ddn.png" Enabled="false" FocusedCssClass="comboBoxHover"
                                                                                    HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                                                                    SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                                </ComponentArt:ComboBox>
                                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_FilterBy_ManagerMasterMonthlyOperation" />
                                                                            </Content>
                                                                            <ClientEvents>
                                                                                <BeforeCallback EventHandler="CallBack_cmbFilterBy_ManagerMasterMonthlyOperation_onBeforeCallback"/>
                                                                                <CallbackComplete EventHandler="CallBack_cmbFilterBy_ManagerMasterMonthlyOperation_onCallbackComplete" />
                                                                                <CallbackError EventHandler="CallBack_cmbFilterBy_ManagerMasterMonthlyOperation_onCallbackError" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:CallBack>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblFilterTerm_ManagerMasterMonthlyOperation" runat="server" CssClass="WhiteLabel"
                                                                            meta:resourcekey="lblFilterTerm_ManagerMasterMonthlyOperation" Text="عبارت فیلتر"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <input id="txtFilterTerm_ManagerMasterMonthlyOperation" class="TextBoxes" disabled="disabled"
                                                                            style="width: 97%" type="text" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table style="width: 95%; border: 1px outset black">
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 5%">
                                                                        <input id="chbSort_ManagerMasterMonthlyOperation" type="checkbox" onclick="chbSort_ManagerMasterMonthlyOperation_onClick();" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblSort_ManagerMasterMonthlyOperation" runat="server" CssClass="WhiteLabel"
                                                                            meta:resourcekey="lblSort_ManagerMasterMonthlyOperation" Text=": مرتب سازی"></asp:Label>
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
                                                                        <asp:Label ID="lblSortBy_ManagerMasterMonthlyOperation" runat="server" CssClass="WhiteLabel"
                                                                            meta:resourcekey="lblSortBy_ManagerMasterMonthlyOperation" Text=": مرتب سازی بر اساس"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <ComponentArt:CallBack ID="CallBack_cmbSortBy_ManagerMasterMonthlyOperation" runat="server"
                                                                            OnCallback="CallBack_cmbSortBy_ManagerMasterMonthlyOperation_onCallBack" Height="26">
                                                                            <Content>
                                                                                <ComponentArt:ComboBox ID="cmbSortBy_ManagerMasterMonthlyOperation" runat="server"
                                                                                    AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                    DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                    DropImageUrl="Images/ComboBox/ddn.png" Enabled="false" FocusedCssClass="comboBoxHover"
                                                                                    ExpandDirection="Up" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                                                                    SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                                </ComponentArt:ComboBox>
                                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_SortBy_ManagerMasterMonthlyOperation" />
                                                                            </Content>
                                                                            <ClientEvents>
                                                                                <BeforeCallback EventHandler="CallBack_cmbSortBy_ManagerMasterMonthlyOperation_onBeforeCallback"/>
                                                                                <CallbackComplete EventHandler="CallBack_cmbSortBy_ManagerMasterMonthlyOperation_onCallbackComplete" />
                                                                                <CallbackError EventHandler="CallBack_cmbSortBy_ManagerMasterMonthlyOperation_onCallbackError" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:CallBack>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblSortDirection_ManagerMasterMonthlyOperation" runat="server" CssClass="WhiteLabel"
                                                                            meta:resourcekey="lblSortDirection_ManagerMasterMonthlyOperation" Text=": جهت مرتب سازی"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <ComponentArt:ComboBox ID="cmbSortDirection_ManagerMasterMonthlyOperation" runat="server"
                                                                            AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                            DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                            DropImageUrl="Images/ComboBox/ddn.png" Enabled="false" FocusedCssClass="comboBoxHover"
                                                                            HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                                                            SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox"
                                                                            DropDownHeight="50">
                                                                            <Items>
                                                                                <ComponentArt:ComboBoxItem runat="server" Id="cmbItemAscending_cmbSortDirection_ManagerMasterMonthlyOperation"
                                                                                    Value="asc" meta:resourcekey="cmbItemAscending_cmbSortDirection_ManagerMasterMonthlyOperation" />
                                                                                <ComponentArt:ComboBoxItem runat="server" Id="cmbItemDescending_cmbSortDirection_ManagerMasterMonthlyOperation"
                                                                                    Value="desc" meta:resourcekey="cmbItemDescending_cmbSortDirection_ManagerMasterMonthlyOperation" />
                                                                            </Items>
                                                                        </ComponentArt:ComboBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <ComponentArt:ToolBar ID="TlbApplyConditions_ManagerMasterMonthlyOperation" runat="server"
                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemApplyConditions_TlbApplyConditions_ManagerMasterMonthlyOperation"
                                                            runat="server" ClientSideCommand="tlbItemApplyConditions_TlbApplyConditions_ManagerMasterMonthlyOperation_onClick();"
                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemApplyConditions_TlbApplyConditions_ManagerMasterMonthlyOperation"
                                                            TextImageSpacing="5" />
                                                    </Items>
                                                </ComponentArt:ToolBar>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                        <td style="width: 30%">
                            <table>
                                <tr>
                                    <td style="width: 30%">
                                        <asp:Label ID="lblMonth_ManagerMasterMonthlyOperation" runat="server" CssClass="WhiteLabel"
                                            meta:resourcekey="lblMonth_ManagerMasterMonthlyOperation" Text=": ماه"></asp:Label>
                                    </td>
                                    <td style="width: 70%">
                                        <ComponentArt:ComboBox ID="cmbMonth_ManagerMasterMonthlyOperation" runat="server"
                                            AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                            DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" Enabled="true" FocusedCssClass="comboBoxHover"
                                            HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                            SelectedItemCssClass="comboItemHover" Width="100" TextBoxCssClass="comboTextBox"
                                            DropDownHeight="270">
                                            <ClientEvents>
                                                <Change EventHandler="cmbMonth_ManagerMasterMonthlyOperation_onChange" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%">
                            <table class="BoxStyle" style="width: 100%;">
                                <tr>
                                    <td style="width: 80%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 30%; font-size: small; font-weight: normal;">
                                                    <asp:Label ID="lblQuickSearch_ManagerMasterMonthlyOperation" runat="server" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblQuickSearch_ManagerMasterMonthlyOperation" Text=": جستجوی سریع"></asp:Label>
                                                </td>
                                                <td>
                                                    <input id="txtSerchTerm_ManagerMasterMonthlyOperation" runat="server" class="TextBoxes"
                                                        style="width: 99%;" type="text" />
                                                </td>
                                                <td style="width: 5%">
                                                    <ComponentArt:ToolBar ID="TlbManagerMasterMonthlyOperationQuickSearch" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                        UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbManagerMasterMonthlyOperationQuickSearch"
                                                                runat="server" ClientSideCommand="tlbItemSearch_TlbManagerMasterMonthlyOperationQuickSearch_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbManagerMasterMonthlyOperationQuickSearch"
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
            </td>
        </tr>
        <tr>
            <td style="width: 20%" valign="top">
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td id="header_trvDepartments_ManagerMasterMonthlyOperation" class="HeaderLabel"
                                        style="width: 45%">
                                        Departments
                                    </td>
                                    <td id="loadingPanel_trvDepartments_ManagerMasterMonthlyOperation" class="HeaderLabel"
                                        style="width: 50%">
                                    </td>
                                    <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbRefresh_trvDepartments_ManagerMasterMonthlyOperation"
                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvDepartments_ManagerMasterMonthlyOperation"
                                                    runat="server" ClientSideCommand="Refresh_trvDepartments_ManagerMasterMonthlyOperation();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvDepartments_ManagerMasterMonthlyOperation"
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
                            <ComponentArt:CallBack runat="server" ID="CallBack_trvDepartments_ManagerMasterMonthlyOperation"
                                OnCallback="CallBack_trvDepartments_ManagerMasterMonthlyOperation_onCallBack">
                                <Content>
                                    <ComponentArt:TreeView ID="trvDepartments_ManagerMasterMonthlyOperation" runat="server"
                                        BorderColor="Black" CollapseImageUrl="images/TreeView/exp.gif" CollapseNodeOnSelect="false"
                                        CssClass="TreeView" DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="false"
                                        EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                        ExpandImageUrl="images/TreeView/col.gif" ExpandNodeOnSelect="true" FillContainer="false"
                                        ForceHighlightedNodeID="true" HighlightSelectedPath="true" HoverNodeCssClass="HoverNestingTreeNode"
                                        ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19"
                                        Height="250" meta:resourcekey="trvDepartments_ManagerMasterMonthlyOperation"
                                        NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                        SelectedNodeCssClass="SelectedTreeNode" ShowLines="true">
                                        <ClientEvents>
                                            <Load EventHandler="trvDepartments_ManagerMasterMonthlyOperation_onLoad" />
                                            <NodeSelect EventHandler="trvDepartments_ManagerMasterMonthlyOperation_onNodeSelect" />
                                        </ClientEvents>
                                    </ComponentArt:TreeView>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_Departments_ManagerMasterMonthlyOperation" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_trvDepartments_ManagerMasterMonthlyOperation_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_trvDepartments_ManagerMasterMonthlyOperation_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td id="header_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation" class="HeaderLabel"
                                        style="width: 80%;">
                                        Personnel Monthly Operation Summary
                                    </td>
                                    <td id="loadingPanel_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation" class="HeaderLabel"
                                        style="width: 20%">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ComponentArt:CallBack ID="CallBack_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                runat="server" OnCallback="CallBack_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onCallBack">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                        runat="server" AllowHorizontalScrolling="true" CssClass="Grid" EnableViewState="true"
                                        ShowFooter="false" FillContainer="true" FooterCssClass="GridFooter" Height="100%"
                                        ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PageSize="8" RunningMode="Client"
                                        Width="100%" AllowMultipleSelect="false" AllowColumnResizing="false" ScrollBar="Off"
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
                                                    <ComponentArt:GridColumn Align="Center" DataField="BarCode" HeadingText="کد پرسنلی"
                                                        meta:resourcekey="clmnPersonnelBarcode_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="70" />
                                                    <ComponentArt:GridColumn Visible="false" DataField="PersonId" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="PersonName" HeadingText="نام و نام خانوادگی"
                                                        meta:resourcekey="clmnNameAndFamily_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        TextWrap="true" Width="150" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="DepartmentName" HeadingText="بخش"
                                                        meta:resourcekey="clmnDepartment_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="150" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="FirstEntrance" HeadingText="ورود اول"
                                                        meta:resourcekey="clmnFirstEntrance_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="80" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="FirstExit" HeadingText="خروج اول"
                                                        meta:resourcekey="clmnFirstExit_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="50" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="SecondEntrance" HeadingText="ورود دوم"
                                                        meta:resourcekey="clmnSecondEntrance_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="95" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="SecondExit" HeadingText="خروج دوم"
                                                        meta:resourcekey="clmnSecondExit_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="70" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="ThirdEntrance" HeadingText="ورود سوم"
                                                        meta:resourcekey="clmnThirdEntrance_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="85" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="ThirdExit" HeadingText="خروج سوم"
                                                        meta:resourcekey="clmnThirdExit_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="55" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="FourthEntrance" HeadingText="ورود چهارم"
                                                        meta:resourcekey="clmnFourthEntrance_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="85" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="FourthExit" HeadingText="خروج چهارم"
                                                        meta:resourcekey="clmnFourthExit_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="55" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="FifthEntrance" HeadingText="ورود پنجم"
                                                        meta:resourcekey="clmnFifthEntrance_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="85" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="FifthExit" HeadingText="خروج پنجم"
                                                        meta:resourcekey="clmnFifthExit_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="55" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="LastExit" HeadingText="خروج آخر"
                                                        meta:resourcekey="clmnLastExit_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="50" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="NecessaryOperation" HeadingText="کارکرد لازم"
                                                        meta:resourcekey="clmnNecessaryOperation_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="125" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="PresenceDuration" HeadingText="مدت حضور"
                                                        meta:resourcekey="clmnPresenceDuration_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="120" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="HourlyPureOperation" HeadingText="کارکرد خالص ساعتی"
                                                        meta:resourcekey="clmnHourlyPureOperation_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="130" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="DailyPureOperation" HeadingText="کارکرد خالص روزانه"
                                                        meta:resourcekey="clmnDailyPureOperation_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="120" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="ImpureOperation" HeadingText="کارکرد ناخالص "
                                                        meta:resourcekey="clmnImpureOperation_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="135" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="AllowableOverTime" HeadingText="اضافه کار مجاز"
                                                        meta:resourcekey="clmnAllowableOverTime_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="115" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="UnallowableOverTime" HeadingText="اضافه کار غیر مجاز"
                                                        meta:resourcekey="clmnUnallowableOverTime_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="130" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="HourlyAllowableAbsence" HeadingText="غیبت مجاز ساعتی"
                                                        meta:resourcekey="clmnHourlyAllowableAbsence_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="110" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="HourlyUnallowableAbsence" HeadingText="غیبت غیر مجاز ساعتی"
                                                        meta:resourcekey="clmnHourlyUnallowableAbsence_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="160" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="DailyAbsence" HeadingText="غیبت روزانه"
                                                        meta:resourcekey="clmnUnallowableAbsence_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="145" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="HourlyMission" HeadingText="ماموریت ساعتی"
                                                        meta:resourcekey="clmnHourlyMission_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="85" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="DailyMission" HeadingText="ماموریت روزانه"
                                                        meta:resourcekey="clmnDailyMission_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="75" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="HostelryMission" HeadingText="ماموریت شبانه روزی"
                                                        meta:resourcekey="clmnHostelryMission_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="95" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="HourlySickLeave" HeadingText="مرخصی استعلاجی ساعتی"
                                                        meta:resourcekey="clmnHourlySickLeave_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="105" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="DailySickLeave" HeadingText="مرخصی استعلاجی روزانه"
                                                        meta:resourcekey="clmnDailySickLeave_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="95" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="HourlyMeritoriouslyLeave" HeadingText="مرخصی استحقاقی ساعتی"
                                                        meta:resourcekey="clmnHourlyMeritoriouslyLeave_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="155" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="DailyMeritoriouslyLeave" HeadingText="مرخصی استحقاقی روزانه"
                                                        meta:resourcekey="clmnDailyMeritoriouslyLeave_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="145" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="HourlyWithoutPayLeave" HeadingText="مرخصی بی حقوق ساعتی"
                                                        meta:resourcekey="clmnHourlyWithoutPayLeave_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="150" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="DailyWithoutPayLeave" HeadingText="مرخصی بی حقوق روزانه"
                                                        meta:resourcekey="clmnDailyWithoutPayLeave_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="150" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="HourlyWithPayLeave" HeadingText="مرخصی با حقوق ساعتی"
                                                        meta:resourcekey="clmnHourlyWithPayLeave_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="150" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="DailyWithPayLeave" HeadingText="مرخصی با حقوق روزانه"
                                                        meta:resourcekey="clmnDailyWithPayLeave_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="150" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="ShiftName" HeadingText="شیفت"
                                                        meta:resourcekey="clmnShift_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="150" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RemainLeaveToMonthEnd" HeadingText="مرخصی باقیمانده تا پایان ماه"
                                                        meta:resourcekey="clmnRemainLeaveToMonthEnd_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="200" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RemainLeaveToYearEnd" HeadingText="مرخصی باقیمانده تا پایان سال"
                                                        meta:resourcekey="clmnRemainLeaveToYearEnd_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                        Width="200" />
                                                    <ComponentArt:GridColumn DataField="ReserveField1" HeadingText="فیلد رزرو 1" Width="150"
                                                        Align="Center" meta:resourcekey="ReserveField1" />
                                                    <ComponentArt:GridColumn DataField="ReserveField2" HeadingText="فیلد رزرو 2" Width="150"
                                                        Align="Center" meta:resourcekey="ReserveField2" />
                                                    <ComponentArt:GridColumn DataField="ReserveField3" HeadingText="فیلد رزرو 3" Width="150"
                                                        Align="Center" meta:resourcekey="ReserveField3" />
                                                    <ComponentArt:GridColumn DataField="ReserveField4" HeadingText="فیلد رزرو 4" Width="150"
                                                        Align="Center" meta:resourcekey="ReserveField4" />
                                                    <ComponentArt:GridColumn DataField="ReserveField5" HeadingText="فیلد رزرو 5" Width="150"
                                                        Align="Center" meta:resourcekey="ReserveField5" />
                                                    <ComponentArt:GridColumn DataField="ReserveField6" HeadingText="فیلد رزرو 6" Width="150"
                                                        Align="Center" meta:resourcekey="ReserveField6" />
                                                    <ComponentArt:GridColumn DataField="ReserveField7" HeadingText="فیلد رزرو 7" Width="150"
                                                        Align="Center" meta:resourcekey="ReserveField7" />
                                                    <ComponentArt:GridColumn DataField="ReserveField8" HeadingText="فیلد رزرو 8" Width="150"
                                                        Align="Center" meta:resourcekey="ReserveField8" />
                                                    <ComponentArt:GridColumn DataField="ReserveField9" HeadingText="فیلد رزرو 9" Width="150"
                                                        Align="Center" meta:resourcekey="ReserveField9" />
                                                    <ComponentArt:GridColumn DataField="ReserveField10" HeadingText="فیلد رزرو 10" Width="150"
                                                        Align="Center" meta:resourcekey="ReserveField10" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientEvents>
                                            <Load EventHandler="GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onLoad" />
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_MonthlyOperationSummary" />
                                    <asp:HiddenField runat="server" ID="hfMonthlyOperationSummaryCount_ManagerMasterMonthlyOperation" />
                                    <asp:HiddenField runat="server" ID="hfMonthlyOperationSummaryPageCount_ManagerMasterMonthlyOperation" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_CallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td runat="server" meta:resourcekey="AlignObj" style="width: 80%;">
                                        <ComponentArt:ToolBar ID="TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            Style="direction: ltr" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                    runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                    runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                    runat="server" ClientSideCommand="tlbItemNext_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                    runat="server" ClientSideCommand="tlbItemLast_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="footer_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation" style="width: 20%"
                                        runat="server">
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
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogGridSummarySettings"
        runat="server" Width="270px">
        <Content>
            <table runat="server" style="width: 100%;" class="SettingStyle" meta:resourcekey="tblGridSummarySettings_DialogGridSummarySettings">
                <tr>
                    <td>
                        <ComponentArt:ToolBar ID="TlbGridSummarySettings" runat="server" CssClass="toolbar"
                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                            UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbGridSummarySettings" runat="server"
                                    DropDownImageHeight="16px" ClientSideCommand="GridSummarySettings_ManagerMasterMonthlyOperation_onSave();"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemSave_TlbGridSummarySettings" TextImageSpacing="5"
                                    Enabled="true" />
                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbGridSummarySettings" runat="server"
                                    DropDownImageHeight="16px" ClientSideCommand="DialogGridSummarySettings.Close();"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemExit_TlbGridSummarySettings" TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table style="width: 100%;" class="BoxStyle">
                            <tr>
                                <td id="header_GridSummarySettings_ManagerMasterMonthlyOperation" style="color: White;
                                    font-weight: bold; font-family: Arial; width: 100%">
                                    Grid Settings
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 1%">
                                                <input runat="server" id="chbRecreateColumns_GridSummarySettings_ManagerMasterMonthlyOperation"
                                                    type="checkbox" />
                                            </td>
                                            <td style="width: 63%">
                                                <asp:Label runat="server" ID="lblRecreateColumns_GridSummarySettings_ManagerMasterMonthlyOperation"
                                                    meta:resourcekey="lblRecreateColumns_GridSummarySettings_ManagerMasterMonthlyOperation"
                                                    CssClass="WhiteLabel"></asp:Label>
                                            </td>
                                            <td style="width: 1%">
                                                <input runat="server" id="chbSelectAll_GridSummarySettings_ManagerMasterMonthlyOperation"
                                                    type="checkbox" onclick="chbSelectAll_GridSummarySettings_ManagerMasterMonthlyOperation_onClick();" />
                                            </td>
                                            <td style="width: 35%">
                                                <asp:Label runat="server" ID="lblSelectAll_GridSummarySettings_ManagerMasterMonthlyOperation"
                                                    meta:resourcekey="lblSelectAll_GridSummarySettings_ManagerMasterMonthlyOperation"
                                                    CssClass="WhiteLabel"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <ComponentArt:CallBack runat="server" ID="CallBack_GridSummarySettings_ManagerMasterMonthlyOperation"
                                        OnCallback="CallBack_GridSummarySettings_ManagerMasterMonthlyOperation_onCallBack">
                                        <Content>
                                            <ComponentArt:DataGrid ID="GridSummarySettings_ManagerMasterMonthlyOperation" runat="server"
                                                AllowMultipleSelect="false" EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter"
                                                Height="400" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerStyle="Numbered"
                                                ShowFooter="false" PagerTextCssClass="GridFooterText" PageSize="14" RunningMode="Client"
                                                SearchTextCssClass="GridHeaderText" TabIndex="0" Width="180" AllowColumnResizing="false"
                                                ScrollBar="Auto" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                <Levels>
                                                    <ComponentArt:GridLevel AllowSorting="false" AlternatingRowCssClass="AlternatingRow"
                                                        DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                        HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                        SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                        SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false">
                                                        <Columns>
                                                            <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="GridColumn" DefaultSortDirection="Descending"
                                                                HeadingText="ستون گرید" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnGridColumn_GridSummarySettings_ManagerMasterMonthlyOperation" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="ViewState" DefaultSortDirection="Descending"
                                                                HeadingText="نمایش" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnView_GridSummarySettings_ManagerMasterMonthlyOperation"
                                                                ColumnType="CheckBox" AllowEditing="True" />
                                                        </Columns>
                                                    </ComponentArt:GridLevel>
                                                </Levels>
                                            </ComponentArt:DataGrid>
                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_GridSummarySettings_ManagerMasterMonthlyOperation" />
                                            <asp:HiddenField runat="server" ID="hfCurrentSettingID_GridSummarySettings_ManagerMasterMonthlyOperation" />
                                        </Content>
                                        <ClientEvents>
                                            <CallbackComplete EventHandler="CallBack_GridSummarySettings_ManagerMasterMonthlyOperation_CallbackComplete" />
                                            <CallbackError EventHandler="CallBack_GridSummarySettings_ManagerMasterMonthlyOperation_onCallbackError" />
                                        </ClientEvents>
                                    </ComponentArt:CallBack>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </Content>
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
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancelConfirm"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
            </table>
        </Content>
    </ComponentArt:Dialog>
    <asp:HiddenField runat="server" ID="hfCloseMessage_ManagerMasterMonthlyOperation"
        meta:resourcekey="hfCloseMessage_ManagerMasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfheader_Conditions_ManagerMasterMonthlyOperation"
        meta:resourcekey="hfheader_Conditions_ManagerMasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfheader_trvDepartments_ManagerMasterMonthlyOperation"
        meta:resourcekey="hfheader_trvDepartments_ManagerMasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfheader_GridSummarySettings_ManagerMasterMonthlyOperation"
        meta:resourcekey="hfheader_GridSummarySettings_ManagerMasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hffooter_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
        meta:resourcekey="hffooter_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
        meta:resourcekey="hfloadingPanel_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfColumnsRecreation_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
        meta:resourcekey="hfColumnsRecreation_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvDepartments_ManagerMasterMonthlyOperation"
        meta:resourcekey="hfloadingPanel_trvDepartments_ManagerMasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="ErrorHiddenField_Months_ManagerMasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfMonthlyOperationSummaryPageSize_ManagerMasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfheader_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation"
        meta:resourcekey="hfheader_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfCurrentMonth_ManagerMasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfErrorType_ManagerMasterMonthlyOperation" meta:resourcekey="hfErrorType_ManagerMasterMonthlyOperation" />
    <asp:HiddenField runat="server" ID="hfConnectionError_ManagerMasterMonthlyOperation"
        meta:resourcekey="hfConnectionError_ManagerMasterMonthlyOperation" />
    </form>
</body>
</html>
