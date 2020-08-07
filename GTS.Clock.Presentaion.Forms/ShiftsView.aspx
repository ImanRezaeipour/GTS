<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShiftsView.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.ShiftsView" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/hierarchicalGridStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="ShiftsViewForm" runat="server" meta:resourcekey="ShiftsViewForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; height: 320px; font-family: Arial; font-size: small" class="BoxStyle">
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td id="header_Shifts_ShiftsView" class="HeaderLabel" style="width: 50%">
                            Shifts
                        </td>
                        <td id="loadingPanel_GridShiftsView_ShiftsView" class="HeaderLabel" style="width: 45%">
                        </td>
                        <td id="Td6" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                            <ComponentArt:ToolBar ID="TlbRefresh_GridShiftsView_ShiftsView" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridShiftsView_ShiftsView"
                                        runat="server" ClientSideCommand="Refresh_GridShiftsView_ShiftsView();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridShiftsView_ShiftsView"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 95%" valign="top">
                <ComponentArt:CallBack ID="CallBack_GridShiftsView_ShiftsView" runat="server" OnCallback="CallBack_GridShiftsView_ShiftsView_onCallBack">
                    <Content>
                        <ComponentArt:DataGrid ID="GridShiftsView_ShiftsView" PreloadLevels="false" RunningMode="Callback"
                            CssClass="HGridClass" ShowFooter="false" IndentCellWidth="19" AllowHorizontalScrolling="false"
                            IndentCellCssClass="HIndentCell" TreeLineImageWidth="19" TreeLineImageHeight="20" 
                            ImagesBaseUrl="images/Grid/" PagerTextCssClass="HGridFooterText" ScrollTopBottomImagesEnabled="true"
                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" PageSize="7" ScrollImagesFolderUrl="images/Grid/Scroller/"
                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                            ScrollGripCssClass="HScrollGrip" ScrollBarWidth="16" Height="300" runat="server"
                            meta:resourcekey="GridShiftsView_ShiftsView" AllowColumnResizing="false">
                            <Levels>
                                <ComponentArt:GridLevel DataKeyField="ID" SelectedRowCssClass="HSelectedRowClass"
                                    HeadingTextCssClass="HHeadingTextClass" HeadingCellCssClass="HHeadingCellClass"
                                    AlternatingRowCssClass="HL0AlternatingRowClass" RowCssClass="HRowClass" HeadingRowCssClass="HL0HeadingRowClass"
                                    TableHeadingCssClass="HTableHeading" GroupHeadingCssClass="HTableHeading" SortDescendingImageUrl="desc.gif"
                                    SortAscendingImageUrl="asc.gif" SortImageHeight="5" SortImageWidth="9" SelectorCellCssClass="HL0SelectorCell"
                                    DataCellCssClass="HDataCell" SelectorImageUrl="selector.gif" SelectorCellWidth="19"
                                    ShowSelectorCells="true" AllowSorting="false" AllowReordering="false">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                        <ComponentArt:GridColumn DataField="ShiftID" Visible="false" />
                                        <ComponentArt:GridColumn Align="Center" DataField="DayName" HeadingText="روز" meta:resourcekey="clmnDay_GridShiftsView_ShiftsView"
                                            Width="60" />
                                        <ComponentArt:GridColumn Align="Center" DataField="Date" HeadingText="تاریخ" meta:resourcekey="clmnDate_GridShiftsView_ShiftsView"
                                            Width="60" />
                                        <ComponentArt:GridColumn Align="Center" DataField="ShiftName" HeadingText="نام شیفت"
                                            meta:resourcekey="clmnShiftName_GridShiftsView_ShiftsView" Width="180" />
                                    </Columns>
                                </ComponentArt:GridLevel>
                                <ComponentArt:GridLevel AllowReordering="false" AllowSorting="false" AlternatingRowCssClass="HL1AlternatingRowClass"
                                    DataCellCssClass="HDataCell" DataMember="DetailedMonthlyOperation" GroupHeadingCssClass="HTableHeading"
                                    HeadingCellCssClass="HHeadingCellClass" HeadingRowCssClass="HL1HeadingRowClass"
                                    HeadingTextCssClass="HHeadingTextClass" RowCssClass="HRowClass" SelectedRowCssClass="HSelectedRowClass"
                                    SelectorCellCssClass="HL1SelectorCell" SelectorCellWidth="19" SelectorImageUrl="selector.gif"
                                    ShowSelectorCells="true" TableHeadingCssClass="HTableHeading">
                                    <Columns>
                                        <ComponentArt:GridColumn Align="Center" DataField="From" HeadingText="از ساعت" meta:resourcekey="clmnFromHour_GridShiftsView_ShiftsView"
                                            Width="150" />
                                        <ComponentArt:GridColumn Align="Center" DataField="To" HeadingText="تا ساعت" meta:resourcekey="clmnToHour_GridShiftsView_ShiftsView" />
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>
                            <ClientEvents>
                                <Load EventHandler="GridShiftsView_ShiftsView_onLoad" />
                                <ItemExpand EventHandler="GridShiftsView_ShiftsView_onItemExpand" />
                                <RenderComplete EventHandler="GridShiftsView_ShiftsView_onRenderComplete" />
                                <BeforeCallback EventHandler="GridShiftsView_ShiftsView_onBeforeCallback" />
                            </ClientEvents>
                        </ComponentArt:DataGrid>
                        <asp:HiddenField runat="server" ID="ErroHiddenField_ShiftsView" />
                    </Content>
                    <ClientEvents>
                        <CallbackComplete EventHandler="CallBack_GridShiftsView_ShiftsView_onCallbackComplete" />
                        <CallbackError EventHandler="CallBack_GridShiftsView_ShiftsView_onCallbackError"/>
                    </ClientEvents>
                </ComponentArt:CallBack>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hfTitle_DialogShiftsView" meta:resourcekey="hfTitle_DialogShiftsView" />
    <asp:HiddenField runat="server" ID="hfheader_Shifts_ShiftsView" meta:resourcekey="hfheader_Shifts_ShiftsView" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridShiftsView_ShiftsView" meta:resourcekey="hfloadingPanel_GridShiftsView_ShiftsView" />
    <asp:HiddenField runat="server" ID="hfErrorType_ShiftsView" meta:resourcekey="hfErrorType_ShiftsView"/>
    <asp:HiddenField runat="server" ID="hfConnectionError_ShiftsView" meta:resourcekey="hfConnectionError_ShiftsView"/>
    </form>
</body>
</html>
