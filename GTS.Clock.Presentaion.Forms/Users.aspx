<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.Users" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="UsersForm" runat="server" meta:resourcekey="UsersForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table id="tblUsers_UsersForm" style="width: 97%; height: 400px; font-family: Arial;
        font-size: small">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbUsers" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbUsers" runat="server" ClientSideCommand="tlbItemNew_TlbUsers_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbUsers" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbUsers" runat="server" ClientSideCommand="tlbItemEdit_TlbUsers_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbUsers"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbUsers" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemDelete_TlbUsers_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemDelete_TlbUsers" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbUsers" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbUsers" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbUsers_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbUsers" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemSave_TlbUsers_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemSave_TlbUsers" TextImageSpacing="5" Enabled="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbTlbUsers" runat="server" ClientSideCommand="tlbItemCancel_TlbTlbUsers_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbUsers"
                                        TextImageSpacing="5" Enabled="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDataAccessLevels_TlbUsers" runat="server" ClientSideCommand="tlbItemDataAccessLevels_TlbUsers_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="DataAccessLevels.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDataAccessLevels_TlbUsers"
                                        TextImageSpacing="5"/>
                                    <ComponentArt:ToolBarItem ID="tlbItemExcelExport_TlbUsers" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemExcelExport_TlbUsers_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="Excel.png" ImageWidth="16px" ItemType="Command"
                                        Visible="false" TextImageSpacing="5" meta:resourcekey="tlbItemExcelExport_TlbUsers" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbUsers" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbUsers_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbUsers" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbUsers" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemExit_TlbUsers_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbUsers"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_Users" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%">
                            <table style="width: 99%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="" class="HeaderLabel" style="width: 95%">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td id="header_UsersBox_Users" class="HeaderLabel" style="width: 50%;">
                                                                Users
                                                            </td>
                                                            <td id="loadingPanel_GridUsers_Users" class="HeaderLabel" style="width: 45%">
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
                                        <ComponentArt:CallBack ID="CallBack_GridUsers_Users" OnCallback="CallBack_GridUsers_Users_OnCallBack"
                                            runat="server">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridUsers_Users" runat="server" AllowHorizontalScrolling="true"
                                                    CssClass="Grid" EnableViewState="true" ShowFooter="false" FillContainer="true"
                                                    FooterCssClass="GridFooter" Height="150" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                                    PageSize="6" RunningMode="Client" AllowMultipleSelect="false" AllowColumnResizing="false"
                                                    ScrollBar="Off" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                    ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="200">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                            AllowSorting="false" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                            RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                            DataKeyField="ID" SortImageWidth="9" HoverRowCssClass="HoverRow">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="PersonID" Visible="false" />
                                                                <ComponentArt:GridColumn Align="Center" ColumnType="CheckBox" DataField="Active"
                                                                    DefaultSortDirection="Descending" Width="40" HeadingText="وضعیت" HeadingTextCssClass="HeadingText"
                                                                    meta:resourcekey="clmnState_GridUsers_Users" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="PersonCode" DefaultSortDirection="Descending"
                                                                    Width="110" HeadingText="شماره پرسنلی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPersonnelBarCode_GridUsers_Users" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="PersonName" DefaultSortDirection="Descending"
                                                                    HeadingText="نام و نام خانوادگی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnNameAndFamily_GridUsers_Users"
                                                                    Width="140" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="UserName" DefaultSortDirection="Descending"
                                                                    HeadingText="نام کاربری" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnUserName_GridUsers_Users"
                                                                    Width="100" />
                                                                <ComponentArt:GridColumn DataField="RoleID" Visible="false" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="RoleName" DefaultSortDirection="Descending"
                                                                    Width="100" HeadingText="نقش کاربری" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnUesrRole_GridUsers_Users" />
                                                                <ComponentArt:GridColumn DataField="Password" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="ActiveDirectoryAuthenticate" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="TheDoaminId" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="TheDoaminName" Visible="false" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <ItemSelect EventHandler="GridUsers_Users_onItemSelect" />
                                                        <Load EventHandler="GridUsers_Users_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Users" />
                                                <asp:HiddenField runat="server" ID="hfUsersCount_Users" />
                                                <asp:HiddenField runat="server" ID="hfUsersPageCount_Users" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridUsers_Users_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridUsers_Users_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td runat="server" meta:resourcekey="AlignObj" style="width: 75%;">
                                                    <ComponentArt:ToolBar ID="TlbPaging_GridUsers_Users" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                        Style="direction: ltr" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridUsers_Users" runat="server"
                                                                ClientSideCommand="tlbItemRefresh_TlbPaging_GridUsers_Users_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridUsers_Users"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridUsers_Users" runat="server"
                                                                ClientSideCommand="tlbItemFirst_TlbPaging_GridUsers_Users_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="first.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridUsers_Users"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridUsers_Users" runat="server"
                                                                ClientSideCommand="tlbItemBefore_TlbPaging_GridUsers_Users_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="Before.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridUsers_Users"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridUsers_Users" runat="server"
                                                                ClientSideCommand="tlbItemNext_TlbPaging_GridUsers_Users_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="Next.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridUsers_Users"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridUsers_Users" runat="server"
                                                                ClientSideCommand="tlbItemLast_TlbPaging_GridUsers_Users_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="last.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridUsers_Users"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                                <td id="footer_GridUsers_Users" class="WhiteLabel" style="width: 25%">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 40%" valign="top">
                            <table style="width: 100%; height: 100%;" class="BoxStyle">
                                <tr>
                                    <td class="DetailsBoxHeaderStyle">
                                        <div id="header_SearchBox_Users" class="BoxContainerHeader" runat="server" meta:resourcekey="AlignObj"
                                            style="width: 100%; height: 100%">
                                            Search</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div runat="server" meta:resourcekey="AlignObj" style="width: 100%" class="DropDownHeader">
                                            <img id="imgbox_UserSearch_Users" alt="" runat="server" src="~/Images/Ghadir/arrowDown.jpg"
                                                onclick="imgbox_UserSearch_Users_onClick();" />
                                            <span id="header_UserSearchBox_Users">User Search</span>
                                        </div>
                                        <div class="dhtmlgoodies_contentBox" id="box_UserSearch_Users" style="width: 36.5%;">
                                            <div class="dhtmlgoodies_content" id="subbox_UserSearch_Users">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblSearchField_Users" runat="server" meta:resourcekey="lblSearchField_Users"
                                                                Text=": جستجو بر اساس" CssClass="WhiteLabel"></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbSearchField_Users" OnCallback="CallBack_cmbSearchField_Users_onCallBack"
                                                                            Height="26">
                                                                            <Content>
                                                                                <ComponentArt:ComboBox ID="cmbSearchField_Users" runat="server" AutoComplete="true"
                                                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                    Style="width: 100%" TextBoxCssClass="comboTextBox" TextBoxEnabled="true">
                                                                                    <ClientEvents>
                                                                                        <Expand EventHandler="cmbSearchField_Users_onExpand" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:ComboBox>
                                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_SearchFields" />
                                                                            </Content>
                                                                            <ClientEvents>
                                                                                <BeforeCallback EventHandler="cmbSearchField_Users_onBeforeCallback" />
                                                                                <CallbackComplete EventHandler="cmbSearchField_Users_onCallbackComplete" />
                                                                                <CallbackError EventHandler="cmbSearchField_Users_onCallbackError" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:CallBack>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblSearchTerm_Users" runat="server" meta:resourcekey="lblSearchTerm_Users"
                                                                Text=": عبارت جستجو" CssClass="WhiteLabel"></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input type="text" runat="server" style="width: 95%;" class="TextBoxes" id="txtUserSearch_Users"
                                                                onclick="this.select();" onfocus="this.select();" />
                                                        </td>
                                                        <td>
                                                            <ComponentArt:ToolBar ID="TlbUserSearch_Users" runat="server" CssClass="toolbar"
                                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemUserSearch_TlbUserSearch_Users" runat="server"
                                                                        ClientSideCommand="tlbItemUserSearch_TlbUserSearch_Users_onClick();" DropDownImageHeight="16px"
                                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                                        ItemType="Command" meta:resourcekey="tlbItemUserSearch_TlbUserSearch_Users" TextImageSpacing="5" />
                                                                </Items>
                                                            </ComponentArt:ToolBar>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div runat="server" meta:resourcekey="AlignObj" style="width: 100%" class="DropDownHeader">
                                            <img alt="" runat="server" id="imgbox_SearchByPersonnel_Users" src="~/Images/Ghadir/arrowDown_silver.jpg"
                                                onclick="imgbox_SearchByPersonnel_Users_onClick();" />
                                            <span id="header_SearchByPersonnelBox_Users">Personnel Select</span>
                                        </div>
                                        <div class="dhtmlgoodies_contentBox" id="box_SearchByPersonnel_Users" style="width: 40%;">
                                            <div class="dhtmlgoodies_content" id="subbox_SearchByPersonnel_Users">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 90%">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblPersonnel_Users" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblPersonnel_Users"
                                                                                        Text=": پرسنل"></asp:Label>
                                                                                </td>
                                                                                <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj">
                                                                                    <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_Users" runat="server" CssClass="toolbar"
                                                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                        UseFadeEffect="false" Style="direction: ltr;">
                                                                                        <Items>
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_Users" runat="server"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh_silver.png"
                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_Users"
                                                                                                TextImageSpacing="5" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_Users_onClick();"
                                                                                                Enabled="false" />
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_Users" runat="server"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_Users"
                                                                                                ImageUrl="first.png" TextImageSpacing="5" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_Users_onClick();"
                                                                                                Enabled="false" />
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_Users" runat="server"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_Users"
                                                                                                ImageUrl="Before.png" TextImageSpacing="5" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_Users_onClick();"
                                                                                                Enabled="false" />
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_Users" runat="server"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_Users"
                                                                                                ImageUrl="Next.png" TextImageSpacing="5" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_Users_onClick();"
                                                                                                Enabled="false" />
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_Users" runat="server"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_Users"
                                                                                                ImageUrl="last.png" TextImageSpacing="5" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_Users_onClick();"
                                                                                                Enabled="false" />
                                                                                        </Items>
                                                                                    </ComponentArt:ToolBar>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td style="width: 10%">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 90%">
                                                                        <ComponentArt:CallBack ID="CallBack_cmbPersonnel_Users" runat="server" OnCallback="CallBack_cmbPersonnel_Users_onCallBack"
                                                                            Height="26">
                                                                            <Content>
                                                                                <ComponentArt:ComboBox ID="cmbPersonnel_Users" runat="server" AutoComplete="true"
                                                                                    AutoHighlight="false" CssClass="comboBox" DataFields="BarCode" DataTextField="Name"
                                                                                    DropDownWidth="400" DropDownCssClass="comboDropDown" DropDownHeight="210" DropDownPageSize="7"
                                                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_Users"
                                                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client" TextBoxEnabled="true"
                                                                                    SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                                    <ClientTemplates>
                                                                                        <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_Users">
                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                <tr class="dataRow">
                                                                                                    <td class="dataCell" style="width: 40%">
                                                                                                        ## DataItem.getProperty('Text') ##
                                                                                                    </td>
                                                                                                    <td class="dataCell" style="width: 30%">
                                                                                                        ## DataItem.getProperty('BarCode') ##
                                                                                                    </td>
                                                                                                    <td class="dataCell" style="width: 30%">
                                                                                                        ## DataItem.getProperty('CardNum') ##
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </ComponentArt:ClientTemplate>
                                                                                    </ClientTemplates>
                                                                                    <DropDownHeader>
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="400">
                                                                                            <tr class="headingRow">
                                                                                                <td id="clmnName_cmbPersonnel_Users" class="headingCell" style="width: 40%; text-align: center">
                                                                                                    Name And Family
                                                                                                </td>
                                                                                                <td id="clmnBarCode_cmbPersonnel_Users" class="headingCell" style="width: 30%; text-align: center">
                                                                                                    BarCode
                                                                                                </td>
                                                                                                <td id="clmnCardNum_cmbPersonnel_Users" class="headingCell" style="width: 30%; text-align: center">
                                                                                                    CardNum
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </DropDownHeader>
                                                                                    <ClientEvents>
                                                                                        <Expand EventHandler="cmbPersonnel_Users_onExpand" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:ComboBox>
                                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Personnel_Users" />
                                                                                <asp:HiddenField runat="server" ID="hfPersonnelPageCount_Users" />
                                                                            </Content>
                                                                            <ClientEvents>
                                                                                <BeforeCallback EventHandler="CallBack_cmbPersonnel_Users_onBeforeCallback" />
                                                                                <CallbackComplete EventHandler="CallBack_cmbPersonnel_Users_onCallBackComplete" />
                                                                                <CallbackError EventHandler="CallBack_cmbPersonnel_Users_onCallbackError" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:CallBack>
                                                                    </td>
                                                                    <td style="width: 10%">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 90%">
                                                                        <input runat="server" id="txtPersonnelSearch_Users" class="TextBoxes" type="text"
                                                                            style="width: 95%" onclick="this.select();" onfocus="this.select();" />
                                                                    </td>
                                                                    <td style="width: 10%">
                                                                        <ComponentArt:ToolBar ID="TlbSearchPersonnel_Users" runat="server" CssClass="toolbar"
                                                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_Users" runat="server"
                                                                                    ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_Users_onClick();" DropDownImageHeight="16px"
                                                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search_silver.png" ImageWidth="16px"
                                                                                    ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_Users"
                                                                                    TextImageSpacing="5" />
                                                                            </Items>
                                                                        </ComponentArt:ToolBar>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 90%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="width: 10%">
                                                                        <ComponentArt:ToolBar ID="TlbAdvancedSearch_Users" runat="server" CssClass="toolbar"
                                                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_Users" runat="server"
                                                                                    ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_Users_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses_silver.png"
                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_Users"
                                                                                    TextImageSpacing="5" Enabled="false" />
                                                                            </Items>
                                                                        </ComponentArt:ToolBar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
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
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td class="DetailsBoxHeaderStyle">
                            <div id="header_UserDetailBox_Users" runat="server" meta:resourcekey="AlignObj" class="BoxContainerHeader"
                                style="width: 100%; height: 100%">
                                User Details</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 25%">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 10%">
                                                                <input id="chbActiveUser_Users" type="checkbox" disabled="disabled" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblActiveUser_Users" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblActiveUser_Users"
                                                                    Text="فعال"></asp:Label>
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
                                                                <asp:Label ID="lblUserRole_Users" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblUserRole_Users"
                                                                    Text=": نقش کاربری"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <ComponentArt:CallBack ID="CallBack_cmbUserRole_Users" runat="server" OnCallback="CallBack_cmbUserRole_Users_onCallBack"
                                                                    Height="26">
                                                                    <Content>
                                                                        <ComponentArt:ComboBox ID="cmbUserRole_Users" runat="server" AutoComplete="true"
                                                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                            DropDownHeight="200" DropDownResizingMode="Bottom" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                            DropImageUrl="Images/ComboBox/ddn.png" Enabled="false" ExpandDirection="Up" FocusedCssClass="comboBoxHover"
                                                                            HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                                                            SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox" TextBoxEnabled="true">
                                                                            <DropDownContent>
                                                                                <ComponentArt:TreeView ID="trvUserRole_cmbUserRole_Users" runat="server" CollapseImageUrl="images/TreeView/exp.gif"
                                                                                    CssClass="TreeView" DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                                                    EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                                                    ExpandImageUrl="images/TreeView/col.gif" Height="98%" HoverNodeCssClass="HoverTreeNode"
                                                                                    ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                                                                    LineImageWidth="19" NodeCssClass="ComboTreeNode" NodeEditCssClass="NodeEdit"
                                                                                    NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode"
                                                                                    meta:resourcekey="trvUserRole_cmbUserRole_Users" ShowLines="true" Width="100%">
                                                                                    <ClientEvents>
                                                                                        <NodeSelect EventHandler="trvUserRole_cmbUserRole_Users_onNodeSelect" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:TreeView>
                                                                            </DropDownContent>
                                                                            <ClientEvents>
                                                                                <Expand EventHandler="cmbUserRole_Users_onExpand" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:ComboBox>
                                                                        <asp:HiddenField ID="ErrorHiddenField_UsersRoles" runat="server" />
                                                                    </Content>
                                                                    <ClientEvents>
                                                                        <BeforeCallback EventHandler="cmbUserRole_Users_onBeforeCallback" />
                                                                        <CallbackComplete EventHandler="cmbUserRole_Users_onCallbackComplete" />
                                                                        <CallbackError EventHandler="cmbUserRole_Users_onCallbackError" />
                                                                    </ClientEvents>
                                                                </ComponentArt:CallBack>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 45%">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td id="Td1" runat="server" meta:resourcekey="AlignObj" style="width: 5%">
                                                                <input id="rdbUserNameIntroduction_Users" disabled="disabled" name="Users" onclick="rdbUserNameIntroduction_Users_onClick();"
                                                                    type="radio" value="V1" />
                                                            </td>
                                                            <td id="Td3" runat="server" meta:resourcekey="AlignObj" style="width: 95%">
                                                                <asp:Label ID="lblUserNameIntroduction_Users" runat="server" CssClass="WhiteLabel"
                                                                    meta:resourcekey="lblUserNameIntroduction_Users" Text="تعریف نام کاربری"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 33%">
                                                                <asp:Label ID="lblUserName_Users" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblUserName_Users"
                                                                    Text=": نام کاربری"></asp:Label>
                                                            </td>
                                                            <td style="width: 33%">
                                                                <asp:Label ID="lblPassword_Users" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblPassword_Users"
                                                                    Text=": کلمه عبور"></asp:Label>
                                                            </td>
                                                            <td style="width: 33%">
                                                                <asp:Label ID="lblPasswordRepeat_Users" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblPasswordRepeat_Users"
                                                                    Text=": تکرار کلمه عبور"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <input id="txtUserName_Users" runat="server" class="TextBoxes" disabled="disabled"
                                                                    style="width: 95%;" type="text" onclick="this.select();" onfocus="this.select();" />
                                                            </td>
                                                            <td>
                                                                <input id="txtPassword_Users" runat="server" class="TextBoxes" disabled="disabled"
                                                                    style="width: 95%;" type="password" onclick="this.select();" onfocus="this.select();" />
                                                            </td>
                                                            <td>
                                                                <input id="txtPasswordRepeat_Users" runat="server" class="TextBoxes" disabled="disabled"
                                                                    style="width: 95%;" type="password" onclick="this.select();" onfocus="this.select();" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 30%">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td id="Td4" runat="server" meta:resourcekey="AlignObj" style="width: 5%">
                                                                <input id="rdbActiveDirValidation_Users" disabled="disabled" name="Users" onclick="rdbActiveDirValidation_Users_onClick();"
                                                                    type="radio" value="V1" />
                                                            </td>
                                                            <td id="Td5" runat="server" meta:resourcekey="AlignObj" style="width: 95%">
                                                                <asp:Label ID="lblActiveDirValidation_Users" runat="server" CssClass="WhiteLabel"
                                                                    meta:resourcekey="lblActiveDirValidation_Users" Text="اکتیو دایرکتری"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 50%">
                                                                <asp:Label ID="lblDomainName_Users" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDomainName_Users"
                                                                    Text=": نام دامین"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDomainUserName_Users" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDomainUserName_Users"
                                                                    Text=": نام کاربری دامین"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <ComponentArt:CallBack runat="server" ID="CallBack_cmbDomainName_Users" OnCallback="CallBack_cmbDomainName_Users_onCallBack"
                                                                    Height="26">
                                                                    <Content>
                                                                        <ComponentArt:ComboBox ID="cmbDomainName_Users" runat="server" AutoComplete="true"
                                                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                            ExpandDirection="Up" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                                                                            HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                                                            SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox"
                                                                            DataTextField="Domain" DataValueField="ID" Enabled="false" TextBoxEnabled="true">
                                                                            <ClientEvents>
                                                                                <Expand EventHandler="cmbDomainName_Users_onExpand" />
                                                                                <Collapse EventHandler="cmbDomainName_Users_onCollapse" />
                                                                                <Change EventHandler="cmbDomainName_Users_onChange" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:ComboBox>
                                                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_Domains" />
                                                                    </Content>
                                                                    <ClientEvents>
                                                                        <BeforeCallback EventHandler="CallBack_cmbDomainName_Users_onBeforeCallback" />
                                                                        <CallbackComplete EventHandler="CallBack_cmbDomainName_Users_onCallbackComplete" />
                                                                        <CallbackError EventHandler="CallBack_cmbDomainName_Users_onCallbackError" />
                                                                    </ClientEvents>
                                                                </ComponentArt:CallBack>
                                                            </td>
                                                            <td>
                                                                <ComponentArt:CallBack runat="server" ID="CallBack_cmbDomainUserName_Users" OnCallback="CallBack_cmbDomainUserName_Users_onCallBack"
                                                                    Height="26">
                                                                    <Content>
                                                                        <ComponentArt:ComboBox ID="cmbDomainUserName_Users" runat="server" AutoComplete="true"
                                                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                            Style="width: 100%" TextBoxCssClass="comboTextBox" Enabled="false" ExpandDirection="Up" TextBoxEnabled="true">
                                                                            <ClientEvents>
                                                                                <Expand EventHandler="cmbDomainUserName_Users_onExpand" />
                                                                                <Collapse EventHandler="cmbDomainUserName_Users_onCollapse" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:ComboBox>
                                                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_DomainUsers" />
                                                                    </Content>
                                                                    <ClientEvents>
                                                                        <BeforeCallback EventHandler="CallBack_cmbDomainUserName_Users_onBeforeCallback" />
                                                                        <CallbackComplete EventHandler="CallBack_cmbDomainUserName_Users_onCallbackComplete" />
                                                                        <CallbackError EventHandler="CallBack_cmbDomainUserName_Users_onCallbackError" />
                                                                    </ClientEvents>
                                                                </ComponentArt:CallBack>
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
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="280px">
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
    <iframe id="hidden_IFrame_Users" runat="server" style="width: 0px; height: 0px">
    </iframe>
    <asp:HiddenField runat="server" ID="hfUsersPageSize_Users" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_Users" />
    <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_Users" meta:resourcekey="hfclmnName_cmbPersonnel_Users" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_Users" meta:resourcekey="hfclmnBarCode_cmbPersonnel_Users" />
    <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_Users" meta:resourcekey="hfclmnCardNum_cmbPersonnel_Users" />
    <asp:HiddenField runat="server" ID="hfheader_SearchBox_Users" meta:resourcekey="hfheader_SearchBox_Users" />
    <asp:HiddenField runat="server" ID="hfheader_SearchByPersonnelBox_Users" meta:resourcekey="hfheader_SearchByPersonnelBox_Users" />
    <asp:HiddenField runat="server" ID="hfheader_UserDetailBox_Users" meta:resourcekey="hfheader_UserDetailBox_Users" />
    <asp:HiddenField runat="server" ID="hfheader_UsersBox_Users" meta:resourcekey="hfheader_UsersBox_Users" />
    <asp:HiddenField runat="server" ID="hfheader_UserSearchBox_Users" meta:resourcekey="hfheader_UserSearchBox_Users" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogActiveDirectory" meta:resourcekey="hfTitle_DialogActiveDirectory" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridUsers_Users" meta:resourcekey="hfloadingPanel_GridUsers_Users" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_Users" meta:resourcekey="hfDeleteMessage_Users" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Users" meta:resourcekey="hfCloseMessage_Users" />
    <asp:HiddenField runat="server" ID="hffooter_GridUsers_Users" meta:resourcekey="hffooter_GridUsers_Users" />
    <asp:HiddenField runat="server" ID="hfView_Users" meta:resourcekey="hfView_Users" />
    <asp:HiddenField runat="server" ID="hfAdd_Users" meta:resourcekey="hfAdd_Users" />
    <asp:HiddenField runat="server" ID="hfEdit_Users" meta:resourcekey="hfEdit_Users" />
    <asp:HiddenField runat="server" ID="hfDelete_Users" meta:resourcekey="hfDelete_Users" />
    <asp:HiddenField runat="server" ID="hfErrorType_Users" meta:resourcekey="hfErrorType_Users" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Users" meta:resourcekey="hfConnectionError_Users" />
    <asp:HiddenField runat="server" ID="hfheader_DataAccessLevels_Users" meta:resourcekey="hfheader_DataAccessLevels_Users" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvDataAccessLevels_Users" meta:resourcekey="hfloadingPanel_trvDataAccessLevels_Users" />
    </form>
</body>
</html>
