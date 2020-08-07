<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestsState.aspx.cs" Inherits="RequestsState" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="RequestsStateForm" runat="server" meta:resourcekey="RequestsStateForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family:Arial; font-size:small; background-image:url('Images/Ghadir/bg-body.jpg'); background-repeat:repeat" >
        <tr>
            <td>
                <ComponentArt:DataGrid ID="GridRequestsState_RequestsState" runat="server" AllowHorizontalScrolling="true"
                    CallbackCacheSize="20000" CallbackCachingEnabled="true" CssClass="Grid" EnableViewState="true"
                    FillContainer="true" FooterCssClass="GridFooter" Height="100%" ImagesBaseUrl="images/Grid/"
                    LoadingPanelClientTemplateId="GridRequestsState_RequestsState_LoadingFeedbackTemplate"
                    LoadingPanelFadeDuration="1000" LoadingPanelFadeMaximumOpacity="60" LoadingPanelPosition="MiddleCenter"
                    ManualPaging="true" PagePaddingEnabled="true" PagerStyle="Numbered" PagerTextCssClass="GridFooterText"
                    PageSize="20" RunningMode="Client" SearchTextCssClass="GridHeaderText" Width="320">
                    <Levels>
                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                            HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                            SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                            SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9">
                            <Columns>
                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                <ComponentArt:GridColumn Align="Center" DataField="Manager" DefaultSortDirection="Descending"
                                    HeadingText="نام مدیر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnManagerName_GridRequestsState_RequestsState"
                                    Width="100" />
                                <ComponentArt:GridColumn Align="Center" DataField="State" DefaultSortDirection="Descending"
                                    HeadingText="وضعیت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnState_GridRequestsState_RequestsState"
                                    Width="40" />                                                                
                                <ComponentArt:GridColumn Align="Center" DataField="DateAndTime" DefaultSortDirection="Descending"
                                    HeadingText="تاریخ و ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDateAndTime_GridRequestsState_RequestsState"
                                    Width="60" />
                                <ComponentArt:GridColumn Align="Center" DataField="Descriptions" DefaultSortDirection="Descending"
                                    HeadingText="توضیحات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDescriptions_GridRequestsState_RequestsState"
                                    Width="120" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="GridRequestsState_RequestsState_LoadingFeedbackTemplate">
                            <table bgcolor="#e0e0e0" style="width: 100; height: 50">
                                <tr>
                                    <td align="center" valign="middle">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="font-size: 10px; font-family: Verdana;">
                                                    Loading...&nbsp;
                                                </td>
                                                <td>
                                                    <img alt="" border="0" height="16" src="images/Grid/spinner.gif" width="16" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:DataGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
