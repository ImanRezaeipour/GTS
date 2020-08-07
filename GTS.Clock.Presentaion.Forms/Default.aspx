<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="FUA" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link id="Link1" href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link2" href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link3" href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link4" href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link5" href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link6" href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link7" href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link8" href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link9" href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link10" href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link11" href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link12" href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link13" href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link14" href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link15" href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link16" href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <%--<table id="Mastertbl_PersonnelSelect" class="BoxStyle" style="width: 100%; background-color: White">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbPersonnelSelect" runat="server" class="BoxStyle" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbPersonnelSelect" runat="server" ClientSideCommand="tlbItemSave_TlbPersonnelSelect_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbPersonnelSelect"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPersonnelSelect" runat="server" ClientSideCommand="tlbItemExit_TlbPersonnelSelect_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbPersonnelSelect"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="BoxStyle" style="width: 100%;">
                                <tr>
                                    <td style="color: White; width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_Personnel_PersonnelSelect" class="HeaderLabel" style="width: 30%;">Personnel
                                                </td>
                                                <td id="loadingPanel_GridPersonnel_PersonnelSelect" class="HeaderLabel" style="width: 35%">
                                                    &nbsp;</td>
                                                <td style="width:35%">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 5%">
                                                                <input id="chbAllInThisPage_PersonnelSelect" onclick="chbAllInThisPage_PersonnelSelect_onClick();" type="checkbox" /></td>
                                                            <td>
                                                                <asp:Label ID="lblAllInThisPage_PersonnelSelect" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblAllInThisPage_PersonnelSelect" Text="همه در این صفحه"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ComponentArt:CallBack ID="CallBack_GridPersonnel_PersonnelSelect" runat="server"
                                            OnCallback="CallBack_GridPersonnel_PersonnelSelect_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridPersonnel_PersonnelSelect" runat="server" AllowColumnResizing="false"
                                                    AllowHorizontalScrolling="true" AllowMultipleSelect="false" CssClass="Grid" EnableViewState="true"
                                                    FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                    PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="10" RunningMode="Client"
                                                    ScrollBar="Off" ScrollBarCssClass="ScrollBar" ScrollBarWidth="16" ScrollButtonHeight="17"
                                                    ScrollButtonWidth="16" ScrollGripCssClass="ScrollGrip" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollTopBottomImageHeight="2" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageWidth="16"
                                                    SearchTextCssClass="GridHeaderText" ShowFooter="false" Width="600px">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                            DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                            RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                            SortImageWidth="9">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                <ComponentArt:GridColumn Align="Center" ColumnType="CheckBox" DataField="Select"
                                                                    HeadingText="انتخاب" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnSelect_GridPersonnel_PersonnelSelect"
                                                                    Width="50" AllowEditing="True" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="PersonCode" DefaultSortDirection="Descending"
                                                                    HeadingText="شماره پرسنلی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPersonnelNumber_GridPersonnel_PersonnelSelect"
                                                                    Width="125" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام و نام خانوادگی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnName_GridPersonnel_PersonnelSelect"
                                                                    Width="175" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Department.Name" DefaultSortDirection="Descending"
                                                                    HeadingText="بخش" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDepartment_GridPersonnel_PersonnelSelect"
                                                                    Width="175" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="OrganizationUnit.Name" DefaultSortDirection="Descending"
                                                                    HeadingText="پست سازمانی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnOrganizationPost_GridPersonnel_PersonnelSelect"
                                                                    Width="175" />
                                                                <ComponentArt:GridColumn DataField="Department.ID" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="OrganizationUnit.ID" Visible="false" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridPersonnel_PersonnelSelect_onLoad" />
                                                        <ItemCheckChange EventHandler="GridPersonnel_PersonnelSelect_onItemCheckChange" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField ID="ErrorHiddenField_Personnel_PersonnelSelect" runat="server" />
                                                <asp:HiddenField ID="hfPersonnelCount_Personnel_PersonnelSelect" runat="server" />
                                                <asp:HiddenField ID="hfPersonnelPageCount_Personnel_PersonnelSelect" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridPersonnel_PersonnelSelect_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridPersonnel_PersonnelSelect_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td id="Td5" runat="server" meta:resourcekey="AlignObj" style="width: 50%;">
                                                    <ComponentArt:ToolBar ID="TlbPaging_GridPersonnel_PersonnelSelect" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                        Style="direction: ltr" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridPersonnel_PersonnelSelect_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_GridPersonnel_PersonnelSelect_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_GridPersonnel_PersonnelSelect_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                runat="server" ClientSideCommand="tlbItemNext_TlbPaging_GridPersonnel_PersonnelSelect_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                runat="server" ClientSideCommand="tlbItemLast_TlbPaging_GridPersonnel_PersonnelSelect_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                                <td id="PersonnelCount_GridPersonnel_PersonnelSelect" class="WhiteLabel" style="width: 25%"></td>
                                                <td id="footer_GridPersonnel_PersonnelSelect" class="WhiteLabel" style="width: 25%"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>--%>
    </form>
</body>
</html>


