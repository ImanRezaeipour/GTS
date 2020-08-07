<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnderManagementPersonnelExeptionAccessView.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.UnderManagementPersonnelExeptionAccessView" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/navStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/upload.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/hierarchicalGridStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <form id="UnderManagementPersonnelExeptionAccessViewForm" runat="server" meta:resourcekey="UnderManagementPersonnelExeptionAccessViewForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width:100%">
        <tr>
            <td align="center">
                <ComponentArt:CallBack ID="CallBack_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView" runat="server" OnCallback="CallBack_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView_onCallBack">
                    <Content>
                        <ComponentArt:DataGrid ID="GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView" runat="server" AllowColumnResizing="false"
                            AllowHorizontalScrolling="false" CssClass="HGridClass" Height="100%" ImagesBaseUrl="images/Grid/"
                            IndentCellCssClass="HIndentCell" IndentCellWidth="19" meta:resourcekey="GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView"
                            PagerStyle="Numbered" PagerTextCssClass="GridFooterText" PageSize="10" PreloadLevels="false"
                            RunningMode="Callback" ScrollBarCssClass="ScrollBar" ScrollBarWidth="16" ScrollButtonHeight="17"
                            ScrollButtonWidth="16" ScrollGripCssClass="HScrollGrip" ScrollImagesFolderUrl="images/Grid/Scroller/"
                            ScrollTopBottomImageHeight="2" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageWidth="16"

                            ShowFooter="false" TreeLineImageHeight="20" TreeLineImageWidth="19">
                            <Levels>
                                <ComponentArt:GridLevel AllowReordering="false" AllowSorting="false" AlternatingRowCssClass="HL0AlternatingRowClass"
                                    DataCellCssClass="HDataCell" DataKeyField="ID" DataMember="MasterMonthlyOperation"
                                    GroupHeadingCssClass="HTableHeading" HeadingCellCssClass="HHeadingCellClass"
                                    HeadingRowCssClass="HL0HeadingRowClass" HeadingTextCssClass="HHeadingTextClass"
                                    RowCssClass="HRowClass" SelectedRowCssClass="HSelectedRowClass" SelectorCellCssClass="HL0SelectorCell"
                                    SelectorCellWidth="19" SelectorImageUrl="selector.gif" ShowSelectorCells="true"
                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                    SortImageWidth="9" TableHeadingCssClass="HTableHeading">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                        <ComponentArt:GridColumn Align="Center" DataField="BarCode" HeadingText="بارکد" meta:resourcekey="clmnBarCode_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView"
                                            Width="160" />
                                        <ComponentArt:GridColumn Align="Center" DataField="Name" HeadingText="نام و نام خانوادگی" meta:resourcekey="clmnName_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView"
                                            Width="320" />
                                    </Columns>
                                </ComponentArt:GridLevel>
                                <ComponentArt:GridLevel AllowReordering="false" AllowSorting="false" AlternatingRowCssClass="HL1AlternatingRowClass"
                                    DataCellCssClass="HDataCell" DataKeyField="ID" DataMember="DetailedMonthlyOperation"
                                    GroupHeadingCssClass="HTableHeading" HeadingCellCssClass="HHeadingCellClass"
                                    HeadingRowCssClass="HL1HeadingRowClass" HeadingTextCssClass="HHeadingTextClass"
                                    RowCssClass="HRowClass" SelectedRowCssClass="HSelectedRowClass" SelectorCellCssClass="HL1SelectorCell"
                                    SelectorCellWidth="19" SelectorImageUrl="selector.gif" ShowSelectorCells="true"
                                    TableHeadingCssClass="HTableHeading">
                                    <Columns>
                                        <ComponentArt:GridColumn Align="Center" DataField="ExeptionAccess" HeadingText="دسترسی استثناء"
                                            meta:resourcekey="clmnExeptionAccess_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView" Width="90" />
                                        <ComponentArt:GridColumn Align="Center" DataField="PersonnelAccess" HeadingText="دسترسی پرسنل"
                                            meta:resourcekey="clmnPersonnelAccess_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView" Width="210" ColumnType="CheckBox"/>
                                        <ComponentArt:GridColumn Align="Center" DataField="GroupAccess" HeadingText="دسترسی گروه"
                                            meta:resourcekey="clmnGroupAccess_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView" ColumnType="CheckBox"/>
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>                            
                            <ClientEvents>
                            <ItemSelect EventHandler="GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView_onItemSelect"/>
                            <ItemExpand EventHandler="GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView_onItemExpand"/>
                            <ItemCollapse EventHandler="GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView_onItemCollapse"/>
                            </ClientEvents>
                        </ComponentArt:DataGrid>
                    </Content>
                </ComponentArt:CallBack>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
