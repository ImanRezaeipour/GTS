<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalculationRange.aspx.cs"
    Inherits="CalculationRange" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/numericUpDown.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="CalculationRangeForm" runat="server" meta:resourcekey="CalculationRangeForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%;" class="BoxStyle">
        <tr>
            <td>
                <table style="width: 60%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbCalculationRange" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemEndorsement_TlbCalculationRange" runat="server"
                                        ClientSideCommand="tlbItemEndorsement_TlbCalculationRange_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemEndorsement_TlbCalculationRange"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDecreaseAll_TlbCalculationRange" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="2dowarrow.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDecreaseAll_TlbCalculationRange"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemDecreaseAll_TlbCalculationRange_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemIncreaseAll_TlbCalculationRange" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="2uparrow.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemIncreaseAll_TlbCalculationRange"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemIncreaseAll_TlbCalculationRange_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbCalculationRange" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbCalculationRange_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbCalculationRange"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbCalculationRange" runat="server" ClientSideCommand="tlbItemExit_TlbCalculationRange_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbCalculationRange"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_CalculationRange" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%" valign="top">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_CalculationRange_CalculationRange" class="HeaderLabel" style="width: 35%">
                                                    Calculation Ranges
                                                </td>
                                                <td id="loadingPanel_GridConcepts_CalculationRange" class="HeaderLabel" style="width: 35%">
                                                </td>
                                                <td class="HeaderLabel" style="width: 25%">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 5%">
                                                                <input id="chbSelectAll_CalculationRange" type="checkbox" onclick="chbSelectAll_CalculationRange_onClick();" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="SelectAll_CalculationRange" runat="server" CssClass="WhiteLabel" Style="font-weight: normal"
                                                                    Text="انتخاب همه" meta:resourcekey="SelectAll_CalculationRange"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridConcepts_CalculationRange" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridConcepts_CalculationRange"
                                                                runat="server" ClientSideCommand="Refresh_GridConcepts_CalculationRange();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridConcepts_CalculationRange"
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
                                        <ComponentArt:CallBack ID="CallBack_GridConcepts_CalculationRange" runat="server"
                                            OnCallback="CallBack_GridConcepts_CalculationRange_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridConcepts_CalculationRange" runat="server" CssClass="Grid"
                                                    EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                    PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="5" RunningMode="Client"
                                                    SearchTextCssClass="GridHeaderText" Width="100%" AllowMultipleSelect="false"
                                                    ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                    ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AllowSorting="false" AlternatingRowCssClass="AlternatingRow"
                                                            DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                            HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                            SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                            SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="IsUsedByDateRange" DefaultSortDirection="Descending"
                                                                    ColumnType="CheckBox" HeadingText="انتخاب" HeadingTextCssClass="HeadingText"
                                                                    meta:resourcekey="clmnSelect_GridConcepts_CalculationRange" AllowEditing="True"
                                                                    Width="40" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام مفهوم" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnConceptName_GridConcepts_CalculationRange"
                                                                    Width="200" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridConcepts_CalculationRange_onLoad" />
                                                        <ItemSelect EventHandler="GridConcepts_CalculationRange_onItemSelect" />
                                                        <ItemCheckChange EventHandler="GridConcepts_CalculationRange_onItemCheckChange" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_CalculationRange" />
                                                <asp:HiddenField runat="server" ID="CheckListHiddenField_CalculationRange" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridConcepts_CalculationRange_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridConcepts_CalculationRange_onCallbackError" />
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
                                        <div id="header_CalculationRangeDetails_CalculationRange" runat="server" style="color: White;
                                            width: 100%; height: 100%" class="BoxContainerHeader">
                                            Calculation Range Details</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCalculationRangeName_CalculationRange" runat="server" CssClass="WhiteLabel"
                                            Text=": نام محدوده محاسبات" meta:resourcekey="lblCalculationRangeName_CalculationRange"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input id="txtCalculationRangeName_CalculationRange" type="text" style="width: 98%"
                                            class="TextBoxes" onclick="this.select();" onfocus="this.select();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDescription_CalculationRange" runat="server" CssClass="WhiteLabel"
                                            Text=": توضیحات" meta:resourcekey="lblDescription_CalculationRange"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <textarea id="txtDescription_CalculationRange" cols="20" rows="2" style="width: 98%;
                                            height: 70px;" class="TextBoxes" onclick="this.select();" onfocus="this.select();"></textarea>
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
                <table style="width: 100%" class="BoxStyle">
                    <tr>
                        <td>
                            <table style="width: 100%; border: 1px outset black">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMonth1_CalculationRange" CssClass="WhiteLabel" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label CssClass="WhiteLabel" runat="server" Text="از" meta:resourcekey="lblFrom_CalculationRange"></asp:Label>
                                                </td>
                                                <td id="tdNudFd1_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label CssClass="WhiteLabel" runat="server" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudFm1_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label CssClass="WhiteLabel" runat="server" Text="تا" meta:resourcekey="lblTo_CalculationRange"></asp:Label>
                                                </td>
                                                <td id="tdNudTd1_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label CssClass="WhiteLabel" runat="server" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudTm1_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%; border: 1px outset black">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMonth2_CalculationRange" CssClass="WhiteLabel" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFrom_CalculationRange"
                                                        Text="از"></asp:Label>
                                                </td>
                                                <td id="tdNudFd2_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudFm2_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTo_CalculationRange"
                                                        Text="تا"></asp:Label>
                                                </td>
                                                <td id="tdNudTd2_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudTm2_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%; border: 1px outset black">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMonth3_CalculationRange" CssClass="WhiteLabel" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFrom_CalculationRange"
                                                        Text="از"></asp:Label>
                                                </td>
                                                <td id="tdNudFd3_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudFm3_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTo_CalculationRange"
                                                        Text="تا"></asp:Label>
                                                </td>
                                                <td id="tdNudTd3_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudTm3_CalculationRange" dir="ltr">
                                                    &nbsp;
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
                                    <td align="center">
                                        <asp:Label ID="lblMonth4_CalculationRange" CssClass="WhiteLabel" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFrom_CalculationRange"
                                                        Text="از"></asp:Label>
                                                </td>
                                                <td id="tdNudFd4_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudFm4_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTo_CalculationRange"
                                                        Text="تا"></asp:Label>
                                                </td>
                                                <td id="tdNudTd4_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudTm4_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%; border: 1px outset black">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMonth5_CalculationRange" CssClass="WhiteLabel" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFrom_CalculationRange"
                                                        Text="از"></asp:Label>
                                                </td>
                                                <td id="tdNudFd5_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudFm5_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTo_CalculationRange"
                                                        Text="تا"></asp:Label>
                                                </td>
                                                <td id="tdNudTd5_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudTm5_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%; border: 1px outset black">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMonth6_CalculationRange" CssClass="WhiteLabel" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFrom_CalculationRange"
                                                        Text="از"></asp:Label>
                                                </td>
                                                <td id="tdNudFd6_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudFm6_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTo_CalculationRange"
                                                        Text="تا"></asp:Label>
                                                </td>
                                                <td id="tdNudTd6_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudTm6_CalculationRange" dir="ltr">
                                                    &nbsp;
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
                                    <td align="center">
                                        <asp:Label ID="lblMonth7_CalculationRange" CssClass="WhiteLabel" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFrom_CalculationRange"
                                                        Text="از"></asp:Label>
                                                </td>
                                                <td id="tdNudFd7_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudFm7_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTo_CalculationRange"
                                                        Text="تا"></asp:Label>
                                                </td>
                                                <td id="tdNudTd7_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudTm7_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%; border: 1px outset black">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMonth8_CalculationRange" CssClass="WhiteLabel" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFrom_CalculationRange"
                                                        Text="از"></asp:Label>
                                                </td>
                                                <td id="tdNudFd8_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudFm8_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTo_CalculationRange"
                                                        Text="تا"></asp:Label>
                                                </td>
                                                <td id="tdNudTd8_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudTm8_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%; border: 1px outset black">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMonth9_CalculationRange" CssClass="WhiteLabel" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFrom_CalculationRange"
                                                        Text="از"></asp:Label>
                                                </td>
                                                <td id="tdNudFd9_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudFm9_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTo_CalculationRange"
                                                        Text="تا"></asp:Label>
                                                </td>
                                                <td id="tdNudTd9_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudTm9_CalculationRange" dir="ltr">
                                                    &nbsp;
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
                                    <td align="center">
                                        <asp:Label ID="lblMonth10_CalculationRange" CssClass="WhiteLabel" runat="server"
                                            Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFrom_CalculationRange"
                                                        Text="از"></asp:Label>
                                                </td>
                                                <td id="tdNudFd10_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudFm10_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTo_CalculationRange"
                                                        Text="تا"></asp:Label>
                                                </td>
                                                <td id="tdNudTd10_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudTm10_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%; border: 1px outset black">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMonth11_CalculationRange" CssClass="WhiteLabel" runat="server"
                                            Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFrom_CalculationRange"
                                                        Text="از"></asp:Label>
                                                </td>
                                                <td id="tdNudFd11_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudFm11_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTo_CalculationRange"
                                                        Text="تا"></asp:Label>
                                                </td>
                                                <td id="tdNudTd11_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudTm11_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%; border: 1px outset black">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMonth12_CalculationRange" CssClass="WhiteLabel" runat="server"
                                            Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFrom_CalculationRange"
                                                        Text="از"></asp:Label>
                                                </td>
                                                <td id="tdNudFd12_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudFm12_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 13%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTo_CalculationRange"
                                                        Text="تا"></asp:Label>
                                                </td>
                                                <td id="tdNudTd12_CalculationRange" dir="ltr">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%" valign="bottom" align="center">
                                                    <asp:Label runat="server" CssClass="WhiteLabel" Text="\"></asp:Label>
                                                </td>
                                                <td id="tdNudTm12_CalculationRange" dir="ltr">
                                                    &nbsp;
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
    <asp:HiddenField runat="server" ID="hfheader_CalculationRange_CalculationRange" meta:resourcekey="hfheader_CalculationRange_CalculationRange" />
    <asp:HiddenField runat="server" ID="hfheader_CalculationRangeDetails_CalculationRange"
        meta:resourcekey="hfheader_CalculationRangeDetails_CalculationRange" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_CalculationRange" meta:resourcekey="hfCloseMessage_CalculationRange" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridConcepts_CalculationRange"
        meta:resourcekey="hfloadingPanel_GridConcepts_CalculationRange" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogCalculationRange" meta:resourcekey="hfTitle_DialogCalculationRange" />
    <asp:HiddenField runat="server" ID="hfAdd_CalculationRange" meta:resourcekey="hfAdd_CalculationRange" />
    <asp:HiddenField runat="server" ID="hfEdit_CalculationRange" meta:resourcekey="hfEdit_CalculationRange" />
    <asp:HiddenField runat="server" ID="hfErrorType_CalculationRange" meta:resourcekey="hfErrorType_CalculationRange" />
    <asp:HiddenField runat="server" ID="hfConnectionError_CalculationRange" meta:resourcekey="hfConnectionError_CalculationRange" />
    </form>
</body>
</html>
