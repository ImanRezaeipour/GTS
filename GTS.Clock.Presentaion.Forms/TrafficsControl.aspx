<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrafficsControl.aspx.cs" Inherits="TrafficsControl" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/hierarchicalGridStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="TrafficsControlForm" runat="server" meta:resourcekey="TrafficsControlForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td align="center">
                <ComponentArt:CallBack ID="CallBack_GridTraffics_TrafficsControl" runat="server" OnCallback="CallBack_GridTraffics_TrafficsControl_onCallBack">
                    <Content>
                        <ComponentArt:DataGrid ID="GridTraffics_TrafficsControl" runat="server" AllowColumnResizing="false"
                            AllowHorizontalScrolling="false" CssClass="HGridClass" Height="100%" ImagesBaseUrl="images/Grid/"
                            IndentCellCssClass="HIndentCell" IndentCellWidth="19" meta:resourcekey="GridTraffics_TrafficsControl"
                            AllowMultipleSelect="false" PagerStyle="Numbered" PagerTextCssClass="GridFooterText"
                            PageSize="31" PreloadLevels="false" RunningMode="Callback" ScrollBarCssClass="ScrollBar"
                            ScrollBarWidth="16" ScrollButtonHeight="17" ScrollButtonWidth="16" ScrollGripCssClass="HScrollGrip"
                            ScrollImagesFolderUrl="images/Grid/Scroller/" ScrollTopBottomImageHeight="2"
                            ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageWidth="16" ShowFooter="false"
                            TreeLineImageHeight="20" TreeLineImageWidth="19">
                            <Levels>
                                <ComponentArt:GridLevel AllowReordering="false" AllowSorting="false" AlternatingRowCssClass="HL0AlternatingRowClass"
                                    DataCellCssClass="HDataCell"  DataKeyField="RowID"
                                    GroupHeadingCssClass="HTableHeading" HeadingCellCssClass="HHeadingCellClass"
                                    HeadingRowCssClass="HL0HeadingRowClass" HeadingTextCssClass="HHeadingTextClass"
                                    RowCssClass="HRowClass" SelectedRowCssClass="HSelectedRowClass" SelectorCellCssClass="HL0SelectorCell"
                                    SelectorCellWidth="19" SelectorImageUrl="selector.gif" ShowSelectorCells="true"
                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                    SortImageWidth="9" TableHeadingCssClass="HTableHeading">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="RowID" Visible="false"/>
                                        <ComponentArt:GridColumn Align="Center" DataField="DayName" HeadingText="روز"
                                            meta:resourcekey="clmnDay_GridTraffics_TrafficsControl" Width="200" />
                                        <ComponentArt:GridColumn Align="Center" DataField="TheDate" HeadingText="تاریخ"
                                            meta:resourcekey="clmnDate_GridTraffics_TrafficsControl" Width="200" />
                                        <ComponentArt:GridColumn DataField="Date" Visible="false"/>
                                    </Columns>
                                </ComponentArt:GridLevel>
                                <ComponentArt:GridLevel AllowReordering="false" AllowSorting="false" AlternatingRowCssClass="HL1AlternatingRowClass"
                                    DataCellCssClass="HDataCell" DataKeyField="ID" 
                                    GroupHeadingCssClass="HTableHeading" HeadingCellCssClass="HHeadingCellClass"
                                    HeadingRowCssClass="HL1HeadingRowClass" HeadingTextCssClass="HHeadingTextClass"
                                    RowCssClass="HRowClass" SelectedRowCssClass="HSelectedRowClass" SelectorCellCssClass="HL1SelectorCell"
                                    SelectorCellWidth="19" SelectorImageUrl="selector.gif" ShowSelectorCells="true"
                                    TableHeadingCssClass="HTableHeading">
                                    <Columns>
                                        <ComponentArt:GridColumn Visible="false" DataField="ID" />
                                        <ComponentArt:GridColumn Align="Center" DataField="TheTime" HeadingText="زمان"
                                            meta:resourcekey="clmnTime_GridTraffics_TrafficsControl" Width="70" />
                                        <ComponentArt:GridColumn Align="Center" DataField="PreCardName" HeadingText="نوع تردد"
                                            meta:resourcekey="clmnPreCard_GridTraffics_TrafficsControl" Width="120" />
                                        <ComponentArt:GridColumn Align="Center" DataField="ClockName" HeadingText="دستگاه"
                                            meta:resourcekey="clmnMachine_GridTraffics_TrafficsControl" Width="120" />
                                        <ComponentArt:GridColumn Align="Center" DataField="OpName" HeadingText="اپراتور"
                                            meta:resourcekey="clmnOperator_GridTraffics_TrafficsControl" Width="170" />
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>
                            <ClientEvents>
                                <Load EventHandler="GridTraffics_TrafficsControl_onLoad"/>
                                <ItemExpand EventHandler="GridTraffics_TrafficsControl_onItemExpand" />
                                <BeforeCallback EventHandler="GridTraffics_TrafficsControl_onBeforeCallback"/>
                                <CallbackComplete EventHandler="GridTraffics_TrafficsControl_onCallbackComplete"/>
                                <RenderComplete EventHandler="GridTraffics_TrafficsControl_onRenderComplete"/>
                            </ClientEvents>
                        </ComponentArt:DataGrid>
                        <asp:HiddenField runat="server" ID="ErrorHiddenField_Traffics_TrafficsControl" />
                    </Content>
                    <ClientEvents>
                        <CallbackComplete EventHandler="CallBack_GridTraffics_TrafficsControl_onCallbackComplete" />
                        <CallbackError EventHandler="CallBack_GridTraffics_TrafficsControl_onCallbackError"/>
                    </ClientEvents>
                </ComponentArt:CallBack>
            </td>
        </tr>
    </table>
   <asp:HiddenField runat="server" ID="hfErrorType_TrafficsControl" meta:resourcekey="hfErrorType_TrafficsControl" />
    <asp:HiddenField runat="server" ID="hfConnectionError_TrafficsControl" meta:resourcekey="hfConnectionError_TrafficsControl" />
    </form>
</body>
</html>
