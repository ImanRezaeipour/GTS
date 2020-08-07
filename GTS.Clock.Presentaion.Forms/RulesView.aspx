<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RulesView.aspx.cs" Inherits="RulesView" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/hierarchicalGridStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>    
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="RulesViewForm" runat="server" meta:resourcekey="RulesViewForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td align="center">
                <ComponentArt:CallBack ID="CallBack_GridRules_RulesView" runat="server" OnCallback="CallBack_GridRules_RulesView_onCallBack">
                    <Content>
                        <ComponentArt:DataGrid ID="GridRules_RulesView" runat="server" AllowColumnResizing="false"
                            AllowHorizontalScrolling="false" CssClass="HGridClass" Height="100%" ImagesBaseUrl="images/Grid/"
                            IndentCellCssClass="HIndentCell" IndentCellWidth="19" meta:resourcekey="GridRules_RulesView"
                            PagerStyle="Numbered" PagerTextCssClass="GridFooterText" PageSize="10" PreloadLevels="false"
                            RunningMode="Callback" ScrollBarCssClass="ScrollBar" ScrollBarWidth="16" ScrollButtonHeight="17"
                            ScrollButtonWidth="16" ScrollGripCssClass="HScrollGrip" ScrollImagesFolderUrl="images/Grid/Scroller/"
                            ScrollTopBottomImageHeight="2" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageWidth="16"
                            ShowFooter="false" TreeLineImageHeight="20" TreeLineImageWidth="19">
                            <Levels>
                                <ComponentArt:GridLevel AllowReordering="false" AllowSorting="false" AlternatingRowCssClass="HL0AlternatingRowClass"
                                    DataCellCssClass="HDataCell" DataKeyField="ID"
                                    GroupHeadingCssClass="HTableHeading" HeadingCellCssClass="HHeadingCellClass"
                                    HeadingRowCssClass="HL0HeadingRowClass" HeadingTextCssClass="HHeadingTextClass"
                                    RowCssClass="HRowClass" SelectedRowCssClass="HSelectedRowClass" SelectorCellCssClass="HL0SelectorCell"
                                    SelectorCellWidth="19" SelectorImageUrl="selector.gif" ShowSelectorCells="true"
                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                    SortImageWidth="9" TableHeadingCssClass="HTableHeading">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                        <ComponentArt:GridColumn Align="Center" DataField="Name" HeadingText="عنوان دسته قانون"
                                            meta:resourcekey="clmnRuleBatchTitle_GridRules_RulesView" Width="570" />
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
                                        <ComponentArt:GridColumn DataField="ID" Visible="false"/>
                                        <ComponentArt:GridColumn Align="Center" DataField="IdentifierCode" HeadingText="کد قانون"
                                            meta:resourcekey="clmnRuleCode_GridRules_RulesView" Width="120" />
                                        <ComponentArt:GridColumn Align="Center" DataField="Name" HeadingText="عنوان قانون"
                                            meta:resourcekey="clmnRuleTitle_GridRules_RulesView" Width="264" />
                                        <ComponentArt:GridColumn Align="Center" DataField="Order" HeadingText="اولویت"
                                            meta:resourcekey="clmnRulePriority_GridRules_RulesView" Width="70" />
                                        <ComponentArt:GridColumn DataField="Script" Visible="false"/>
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>
                            <ClientEvents>
                                <Load EventHandler="GridRules_RulesView_onLoad"/>
                                <ItemSelect EventHandler="GridRules_RulesView_onItemSelect" />
                                <ItemExpand EventHandler="GridRules_RulesView_onItemExpand" />
                                <BeforeCallback EventHandler="GridRules_RulesView_onBeforeCallback"/>
                                <CallbackComplete EventHandler="GridRules_RulesView_onCallbackComplete"/>
                                <RenderComplete EventHandler="GridRules_RulesView_onRenderComplete"/>
                            </ClientEvents>
                        </ComponentArt:DataGrid>
                        <asp:HiddenField runat="server" ID="ErrorHiddenField_RulesView"/>
                    </Content>
                    <ClientEvents>
                    <CallbackComplete EventHandler="CallBack_GridRules_RulesView_onCallbackComplete"/>
                    <CallbackError EventHandler="CallBack_GridRules_RulesView_onCallbackError"/>
                    </ClientEvents>
                </ComponentArt:CallBack>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
