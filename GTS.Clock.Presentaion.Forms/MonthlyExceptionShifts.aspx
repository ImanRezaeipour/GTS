<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthlyExceptionShifts.aspx.cs" Inherits="MonthlyExceptionShifts" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="MonthlyExceptionShiftsForm" runat="server" meta:resourcekey="MonthlyExceptionShiftsForm">
        <table id="Mastertbl_MonthlyExceptionShifts" style="width: 99%; height: 100%; font-family: Arial; font-size: small;"
            class="BoxStyle">
            <tr>
                <td>
                    <ComponentArt:ToolBar ID="TlbMonthlyExceptionShifts" runat="server" CssClass="toolbar"
                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" class="BoxStyle">
                        <Items>
                            <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbMonthlyExceptionShifts" runat="server" DropDownImageHeight="16px"
                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                ItemType="Command" meta:resourcekey="tlbItemHelp_TlbMonthlyExceptionShifts" TextImageSpacing="5"
                                ClientSideCommand="tlbItemHelp_TlbMonthlyExceptionShifts_onClick();" />
                            <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbMonthlyExceptionShifts" runat="server"
                                ClientSideCommand="tlbItemFormReconstruction_TlbMonthlyExceptionShifts_onClick();" DropDownImageHeight="16px"
                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbMonthlyExceptionShifts" TextImageSpacing="5" />
                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbMonthlyExceptionShifts" runat="server" DropDownImageHeight="16px"
                                ClientSideCommand="tlbItemExit_TlbMonthlyExceptionShifts_onClick();" DropDownImageWidth="16px"
                                ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbMonthlyExceptionShifts"
                                TextImageSpacing="5" />

                        </Items>
                    </ComponentArt:ToolBar>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <table style="width: 30%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblYear_MonthlyExceptionShifts" runat="server" Text=": سال" CssClass="WhiteLabel"
                                                meta:resourcekey="lblYear_MonthlyExceptionShifts"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMonth_MonthlyExceptionShifts" runat="server" Text=": ماه" CssClass="WhiteLabel"
                                                meta:resourcekey="lblMonth_MonthlyExceptionShifts"></asp:Label>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ComponentArt:ComboBox ID="cmbYear_MonthlyExceptionShifts" runat="server" AutoComplete="true"
                                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100">
                                                <ClientEvents>
                                                    <Change EventHandler="cmbYear_MonthlyExceptionShifts_onChange" />
                                                </ClientEvents>
                                            </ComponentArt:ComboBox>
                                        </td>
                                        <td>
                                            <ComponentArt:ComboBox ID="cmbMonth_MonthlyExceptionShifts" runat="server" AutoComplete="true"
                                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100" DropDownHeight="280">
                                                <ClientEvents>
                                                    <Change EventHandler="cmbMonth_MonthlyExceptionShifts_onChange" />
                                                </ClientEvents>
                                            </ComponentArt:ComboBox>
                                        </td>
                                        <td>
                                            <ComponentArt:ToolBar ID="TlbView_MonthlyExceptionShifts" runat="server" CssClass="toolbar"
                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemView_TlbView_MonthlyExceptionShifts" runat="server"
                                                        ClientSideCommand="tlbItemView_TlbView_MonthlyExceptionShifts_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemView_TlbView_MonthlyExceptionShifts"
                                                        TextImageSpacing="5" Enabled="true" />
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
                <td style="vertical-align: top">
                    <table style="width: 100%; height: 300px; border: outset 1px black;" class="BoxStyle">
                        <tr>
                            <td style="height: 5%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td id="header_MonthlyExceptionShifts_MonthlyExceptionShifts" class="HeaderLabel" style="width: 50%;">Monthly Exception Shifts
                                        </td>
                                        <td id="loadingPanel_GridMonthlyExceptionShifts_MonthlyExceptionShifts" class="HeaderLabel"
                                            style="width: 45%"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                    OnCallback="CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onCallBack">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridMonthlyExceptionShifts_MonthlyExceptionShifts" runat="server" AllowEditing="true"
                                            AllowHorizontalScrolling="true" CssClass="Grid" EnableViewState="false" ShowFooter="false"
                                            FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/" EditOnClickSelectedItem="false"
                                            PagePaddingEnabled="true" PageSize="16" RunningMode="Client" AllowMultipleSelect="false"
                                            AllowColumnResizing="false" ScrollBar="Off" ScrollTopBottomImagesEnabled="true"
                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="960">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                    SortImageWidth="9" EditCommandClientTemplateId="EditCommandTemplate">
                                                    <Columns>
                                                        <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="EditTemplate" EditControlType="EditCommand" Width="50" Align="Center" />
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                        <ComponentArt:GridColumn DataField="PersonID" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="PersonName" DefaultSortDirection="Descending" AllowEditing="False"
                                                            HeadingText="نام و نام خانوادگی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPersonName_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="140" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="PersonCode" DefaultSortDirection="Descending" AllowEditing="False"
                                                            HeadingText="شماره پرسنلی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPersonCode_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="110" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day1" DefaultSortDirection="Descending"
                                                            HeadingText="روز 1" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay1_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day2" DefaultSortDirection="Descending"
                                                            HeadingText="روز 2" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay2_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day3" DefaultSortDirection="Descending"
                                                            HeadingText="روز 3" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay3_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day4" DefaultSortDirection="Descending"
                                                            HeadingText="روز 4" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay4_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day5" DefaultSortDirection="Descending"
                                                            HeadingText="روز 5" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay5_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day6" DefaultSortDirection="Descending"
                                                            HeadingText="روز 6" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay6_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day7" DefaultSortDirection="Descending"
                                                            HeadingText="روز 7" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay7_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day8" DefaultSortDirection="Descending"
                                                            HeadingText="روز 8" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay8_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day9" DefaultSortDirection="Descending"
                                                            HeadingText="روز 9" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay9_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day10" DefaultSortDirection="Descending"
                                                            HeadingText="روز 10" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay10_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day11" DefaultSortDirection="Descending"
                                                            HeadingText="روز 11" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay11_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day12" DefaultSortDirection="Descending"
                                                            HeadingText="روز 12" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay12_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day13" DefaultSortDirection="Descending"
                                                            HeadingText="روز 13" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay13_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day14" DefaultSortDirection="Descending"
                                                            HeadingText="روز 14" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay14_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day15" DefaultSortDirection="Descending"
                                                            HeadingText="روز 15" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay15_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day16" DefaultSortDirection="Descending"
                                                            HeadingText="روز 16" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay16_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day17" DefaultSortDirection="Descending"
                                                            HeadingText="روز 17" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay17_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day18" DefaultSortDirection="Descending"
                                                            HeadingText="روز 18" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay18_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day19" DefaultSortDirection="Descending"
                                                            HeadingText="روز 19" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay19_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day20" DefaultSortDirection="Descending"
                                                            HeadingText="روز 20" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay20_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day21" DefaultSortDirection="Descending"
                                                            HeadingText="2روز 1" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay21_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day22" DefaultSortDirection="Descending"
                                                            HeadingText="روز 22" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay22_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day23" DefaultSortDirection="Descending"
                                                            HeadingText="روز 23" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay23_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day24" DefaultSortDirection="Descending"
                                                            HeadingText="روز 24" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay24_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day25" DefaultSortDirection="Descending"
                                                            HeadingText="روز 25" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay25_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day26" DefaultSortDirection="Descending"
                                                            HeadingText="روز 26" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay26_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day27" DefaultSortDirection="Descending"
                                                            HeadingText="روز 27" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay27_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day28" DefaultSortDirection="Descending"
                                                            HeadingText="روز 28" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay28_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day29" DefaultSortDirection="Descending"
                                                            HeadingText="روز 29" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay29_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day30" DefaultSortDirection="Descending"
                                                            HeadingText="روز 30" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay30_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Day31" DefaultSortDirection="Descending"
                                                            HeadingText="3روز 1" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDay31_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                            Width="40" />
                                                        <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="EditTemplate" EditControlType="EditCommand" Width="50" Align="Center" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="EditTemplate">
                                                    <a>
                                                        <img src="Images/ToolBar/edit.png" onclick="javascript:EditGridMonthlyExceptionShifts_MonthlyExceptionShifts('## DataItem.ClientId ##');" title="##SetCellTitle_GridMonthlyExceptionShifts_MonthlyExceptionShifts('Edit')##"/></a>&nbsp;<a>
                                                            <img src="Images/ToolBar/remove.png" onclick="javascript:DeleteGridMonthlyExceptionShifts_MonthlyExceptionShifts('## DataItem.ClientId ##')" title="##SetCellTitle_GridMonthlyExceptionShifts_MonthlyExceptionShifts('Delete')##"/></a>
                                                </ComponentArt:ClientTemplate>
                                                <ComponentArt:ClientTemplate ID="EditCommandTemplate">
                                                    <a>
                                                        <img src="Images/ToolBar/save.png" onclick="javascript:UpdateGridMonthlyExceptionShifts_MonthlyExceptionShifts();" title="##SetCellTitle_GridMonthlyExceptionShifts_MonthlyExceptionShifts('Save')##"/></a>&nbsp;<a>
                                                            <img src="Images/ToolBar/cancel.png" onclick="javascript:GridMonthlyExceptionShifts_MonthlyExceptionShifts.EditCancel();" title="##SetCellTitle_GridMonthlyExceptionShifts_MonthlyExceptionShifts('Cancel')##"/></a>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                            <ClientEvents>
                                                <Load EventHandler="GridMonthlyExceptionShifts_MonthlyExceptionShifts_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_MonthlyExceptionShifts" />
                                        <asp:HiddenField runat="server" ID="hfMonthlyExceptionShiftsCount_MonthlyExceptionShifts" />
                                        <asp:HiddenField runat="server" ID="hfMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td id="Td1" runat="server" meta:resourcekey="AlignObj" style="width: 10%;">
                                            <ComponentArt:ToolBar ID="TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts" runat="server"
                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                Style="direction: ltr" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        runat="server" ClientSideCommand="tlbItemNext_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        runat="server" ClientSideCommand="tlbItemLast_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                        <td id="footer_GridMonthlyExceptionShifts_MonthlyExceptionShifts" runat="server" class="WhiteLabel"
                                            meta:resourcekey="InverseAlignObj" style="width: 45%"></td>
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
            runat="server" Width="420px">
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
        <asp:HiddenField runat="server" ID="hfTitle_DialogMonthlyExceptionShifts" meta:resourcekey="hfTitle_DialogMonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfheader_MonthlyExceptionShifts_MonthlyExceptionShifts" meta:resourcekey="hfheader_MonthlyExceptionShifts_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_MonthlyExceptionShifts" meta:resourcekey="hfDeleteMessage_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_MonthlyExceptionShifts" meta:resourcekey="hfCloseMessage_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfCurrentYear_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfCurrentMonth_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfMonthlyExceptionShiftsPageSize_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hffooter_GridMonthlyExceptionShifts_MonthlyExceptionShifts" meta:resourcekey="hffooter_GridMonthlyExceptionShifts_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfErrorType_MonthlyExceptionShifts" meta:resourcekey="hfErrorType_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfConnectionError_MonthlyExceptionShifts" meta:resourcekey="hfConnectionError_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridMonthlyExceptionShifts_MonthlyExceptionShifts" meta:resourcekey="hfloadingPanel_GridMonthlyExceptionShifts_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfEdit_MonthlyExceptionShifts" meta:resourcekey="hfEdit_MonthlyExceptionShifts"/>
        <asp:HiddenField runat="server" ID="hfDelete_MonthlyExceptionShifts" meta:resourcekey="hfDelete_MonthlyExceptionShifts"/>
        <asp:HiddenField runat="server" ID="hfSave_MonthlyExceptionShifts" meta:resourcekey="hfSave_MonthlyExceptionShifts"/>
        <asp:HiddenField runat="server" ID="hfCancel_MonthlyExceptionShifts" meta:resourcekey="hfCancel_MonthlyExceptionShifts"/>
    </form>
</body>
</html>
