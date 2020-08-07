<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Managers.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.Managers" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/hierarchicalGridStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <form id="ManagersForm" runat="server" meta:resourcekey="ManagersForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td align="center">
                <ComponentArt:CallBack ID="CallBack_GridManagers_Managers" runat="server" OnCallback="CallBack_GridManagers_Managers_onCallBack">
                    <Content>
                        <ComponentArt:DataGrid ID="GridManagers_Managers" runat="server" AllowColumnResizing="false"
                            AllowHorizontalScrolling="false" CssClass="HGridClass" Height="100%" ImagesBaseUrl="images/Grid/"
                            IndentCellCssClass="HIndentCell" IndentCellWidth="19" meta:resourcekey="GridManagers_Managers"
                            AllowMultipleSelect="false" PagerStyle="Numbered" PagerTextCssClass="GridFooterText"
                            PageSize="10" PreloadLevels="false" RunningMode="Callback" ScrollBarCssClass="ScrollBar"
                            ScrollBarWidth="16" ScrollButtonHeight="17" ScrollButtonWidth="16" ScrollGripCssClass="HScrollGrip"
                            ScrollImagesFolderUrl="images/Grid/Scroller/" ScrollTopBottomImageHeight="2"
                            ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageWidth="16" ShowFooter="false"
                            TreeLineImageHeight="20" TreeLineImageWidth="19">
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
                                        <ComponentArt:GridColumn Align="Center" DataField="ThePerson.BarCode" HeadingText="بارکد"
                                            meta:resourcekey="clmnBarCode_GridManagers_Managers" Width="200" />
                                        <ComponentArt:GridColumn Align="Center" DataField="ThePerson.Name" HeadingText="نام و نام خانوادگی"
                                            meta:resourcekey="clmnName_GridManagers_Managers" Width="220" />
                                        <ComponentArt:GridColumn Visible="false" DataField="TheOrganizationUnit.ID" />
                                        <ComponentArt:GridColumn Align="Center" DataField="TheOrganizationUnit.Name" HeadingText="پست سازمانی"
                                            meta:resourcekey="clmnOrganizationPostName_GridManagers_Managers" Width="220" />
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
                                        <ComponentArt:GridColumn Visible="false" DataField="ID" />
                                        <ComponentArt:GridColumn Visible="false" DataField="AccessGroup.ID" />
                                        <ComponentArt:GridColumn Align="Center" DataField="AccessGroup.Name" HeadingText="گروه دسترسی"
                                            meta:resourcekey="clmnAccessGroup_GridManagers_Managers" Width="200" />
                                        <ComponentArt:GridColumn Align="Center" DataField="DepartmentCount" HeadingText="بخش تحت مدیریت"
                                            meta:resourcekey="clmnUnderManagementDepartment_GridManagers_Managers" Width="70" />
                                        <ComponentArt:GridColumn Align="Center" DataField="PersonCount" HeadingText="پرسنل تحت مدیریت"
                                            meta:resourcekey="clmnUnderManagementPersonnel_GridManagers_Managers" Width="70" />
                                        <ComponentArt:GridColumn Align="Center" DataField="WorkFlow" HeadingText="جریان کار"
                                            meta:resourcekey="clmnWorkFlow_GridManagers_Managers" Width="50" ColumnType="CheckBox" />
                                        <ComponentArt:GridColumn Align="Center" DataField="ActiveFlow" HeadingText="جریان فعال"
                                            meta:resourcekey="clmnActiveFlow_GridManagers_Managers" ColumnType="CheckBox"
                                            Width="56" />
                                        <ComponentArt:GridColumn Align="Center" DataField="FlowName" HeadingText="نام جریان"
                                            meta:resourcekey="clmnFlowName_GridManagers_Managers" />
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>
                            <ClientEvents>
                                <ItemSelect EventHandler="GridManagers_Managers_onItemSelect" />
                                <ItemExpand EventHandler="GridManagers_Managers_onItemExpand" />
                                <ItemCollapse EventHandler="GridManagers_Managers_onItemCollapse" />
                            </ClientEvents>
                        </ComponentArt:DataGrid>
                        <asp:HiddenField runat="server" ID="ErrorHiddenField_Managers_Managers" />
                        <asp:HiddenField runat="server" ID="hfManagersPageCount_Managers" />
                    </Content>
                    <ClientEvents>
                        <Load EventHandler="CallBack_GridManagers_Managers_onLoad" />
                        <CallbackComplete EventHandler="CallBack_GridManagers_Managers_onCallbackComplete" />
                        <CallbackError EventHandler="CallBack_GridManagers_Managers_onCallbackError"/>
                    </ClientEvents>
                </ComponentArt:CallBack>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hfManagesPageSize_Managers" />
    <asp:HiddenField runat="server" ID="hfErrorType_Managers" meta:resourcekey="hfErrorType_Managers" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Managers" meta:resourcekey="hfConnectionError_Managers" />
    </form>
</body>
</html>
