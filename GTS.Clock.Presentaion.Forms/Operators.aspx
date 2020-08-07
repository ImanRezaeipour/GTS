<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Operators.aspx.cs" Inherits="Operators" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <form id="OperatorsForm" runat="server" meta:resourcekey="OperatorsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <div>
        <table id="Mastertbl_Operators" style="width: 100%; font-family: Arial; font-size: small"
            class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbOperators" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbOperators" runat="server" ClientSideCommand="tlbItemNew_TlbOperators_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbOperators"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbOperators" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemDelete_TlbOperators_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                            meta:resourcekey="tlbItemDelete_TlbOperators" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbOperators" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbOperators" TextImageSpacing="5"
                                            ClientSideCommand="tlbItemHelp_TlbOperators_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbOperators" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemSave_TlbOperators_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px" ItemType="Command"
                                            meta:resourcekey="tlbItemSave_TlbOperators" TextImageSpacing="5" Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbOperators" runat="server" ClientSideCommand="tlbItemCancel_TlbOperators_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbOperators"
                                            TextImageSpacing="5" Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbOperators" runat="server"
                                            ClientSideCommand="tlbItemFormReconstruction_TlbOperators_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbOperators"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbOperators" runat="server" ClientSideCommand="tlbItemExit_TlbOperators_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbOperators"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="ActionMode_Operators" class="ToolbarMode">
                                &nbsp;
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
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 90%">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblPersonnel_PersonnelSearch_Operators" runat="server" CssClass="WhiteLabel"
                                                            meta:resourcekey="lblPersonnel_PersonnelSearch_Operators" Text=": پرسنل"></asp:Label>
                                                    </td>
                                                    <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj">
                                                        <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_Operators" runat="server" CssClass="toolbar"
                                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                            Style="direction: ltr" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_Operators"
                                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_Operators_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh_silver.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_Operators"
                                                                    TextImageSpacing="5" Enabled="false" />
                                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_Operators" runat="server"
                                                                    ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_Operators_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="first_silver.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_Operators"
                                                                    TextImageSpacing="5" Enabled="false" />
                                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_Operators"
                                                                    runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_Operators_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Before_silver.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_Operators"
                                                                    TextImageSpacing="5" Enabled="false" />
                                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_Operators" runat="server"
                                                                    ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_Operators_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Next_silver.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_Operators"
                                                                    TextImageSpacing="5" Enabled="false" />
                                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_Operators" runat="server"
                                                                    ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_Operators_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="last_silver.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_Operators"
                                                                    TextImageSpacing="5" Enabled="false" />
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
                                            <ComponentArt:CallBack ID="CallBack_cmbPersonnel_Operators" runat="server" OnCallback="CallBack_cmbPersonnel_Operators_onCallBack"
                                                Height="26">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbPersonnel_Operators" runat="server" AutoComplete="true"
                                                        AutoHighlight="false" CssClass="comboBox" DataFields="BarCode,CardNum" DataTextField="FirstNameAndLastName"
                                                        DropDownCssClass="comboDropDown" DropDownHeight="210" DropDownPageSize="7" DropDownWidth="400"
                                                        DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_Operators"
                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" meta:resourcekey="cmbPersonnel_Operators"
                                                        RunningMode="Client" SelectedItemCssClass="comboItemHover" Style="width: 100%"
                                                        TextBoxCssClass="comboTextBox" Enabled="false" TextBoxEnabled="true">
                                                        <ClientTemplates>
                                                            <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_Operators">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="400">
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
                                                                    <td id="clmnName_cmbPersonnel_Operators" class="headingCell" style="width: 40%; text-align: center">
                                                                        Name And Family
                                                                    </td>
                                                                    <td id="clmnBarCode_cmbPersonnel_Operators" class="headingCell" style="width: 30%;
                                                                        text-align: center">
                                                                        BarCode
                                                                    </td>
                                                                    <td id="clmnCardNum_cmbPersonnel_Operators" class="headingCell" style="width: 30%;
                                                                        text-align: center">
                                                                        CardNum
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DropDownHeader>
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbPersonnel_Operators_onExpand" />
                                                            <Collapse EventHandler="cmbPersonnel_Operators_onCollapse" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField ID="ErrorHiddenField_Personnel_Operators" runat="server" />
                                                    <asp:HiddenField ID="hfPersonnelPageCount_Operators" runat="server" />
                                                </Content>
                                                <ClientEvents>
                                                    <BeforeCallback EventHandler="CallBack_cmbPersonnel_Operators_onBeforeCallback" />
                                                    <CallbackComplete EventHandler="CallBack_cmbPersonnel_Operators_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_cmbPersonnel_Operators_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                        <td style="width: 10%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90%">
                                            <input id="txtPersonnelSearch_Operators" runat="server" class="TextBoxes" onclick="this.select();"
                                                onfocus="this.select();" style="width: 95%" type="text" />
                                        </td>
                                        <td style="width: 10%">
                                            <ComponentArt:ToolBar ID="TlbSearchPersonnel_Operators" runat="server" CssClass="toolbar"
                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_Operators" runat="server"
                                                        ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_Operators_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search_silver.png" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_Operators"
                                                        TextImageSpacing="5" Enabled="false" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 10%">
                                            <ComponentArt:ToolBar ID="TlbAdvancedSearch_Operators" runat="server" CssClass="toolbar"
                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_Operators"
                                                        runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_Operators_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses_silver.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_Operators"
                                                        TextImageSpacing="5" Enabled="false" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 5%">
                                                        <input id="chbActive_Operators" type="checkbox" checked="checked" disabled="disabled" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblActive_Operators" runat="server" Text="فعال" meta:resourcekey="lblActive_Operators"
                                                            CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDescription_Operators" runat="server" Text=": توضیحات" meta:resourcekey="lblDescription_Operators"
                                                CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <textarea id="txtDescription_Operators" cols="20" name="S1" rows="5" style="width: 99%;
                                                height: 63px" class="TextBoxes" disabled="disabled"></textarea>
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
                            <td style="width: 100%">
                                <table style="width: 100%">
                                    <tr>
                                        <td id="header_Operators_Operators" class="HeaderLabel" style="width: 50%">
                                            Operators
                                        </td>
                                        <td id="loadingPanel_GridOperators_Operators" class="HeaderLabel" style="width: 45%">
                                        </td>
                                        <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridOperators_Operators" runat="server" CssClass="toolbar"
                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridOperators_Operators"
                                                        runat="server" ClientSideCommand="Refresh_GridOperators_Operators();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridOperators_Operators"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridOperators_Operators" OnCallback="CallBack_GridOperators_Operators_onCallBack">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridOperators_Operators" runat="server" CssClass="Grid"
                                            EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                            PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="14" RunningMode="Client"
                                            SearchTextCssClass="GridHeaderText" Width="100%" AllowMultipleSelect="false"
                                            ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                    SortImageWidth="9">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Active" HeadingTextCssClass="HeadingText"
                                                            ColumnType="CheckBox" meta:resourcekey="clmnActive_GridOperators_Operators" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Person.PersonCode" DefaultSortDirection="Descending"
                                                            HeadingText="کد پرسنلی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPersonnelCode_GridOperators_Operators" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Person.Name" DefaultSortDirection="Descending"
                                                            HeadingText="نام اپراتور" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnOperatorName_GridOperators_Operators" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Person.OrganizationUnit.Name"
                                                            DefaultSortDirection="Descending" HeadingText="پست سازمانی" HeadingTextCssClass="HeadingText"
                                                            meta:resourcekey="clmnOrganizationPOst_GridOperators_Operators" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                            HeadingText="توضیحات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDescription_GridOperators_Operators" />
                                                        <ComponentArt:GridColumn DataField="Person.ID" Visible="false" />
                                                        <ComponentArt:GridColumn DataField="Person.OrganizationUnit.ID" Visible="false" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientEvents>
                                                <ItemSelect EventHandler="GridOperators_Operators_onItemSelect" />
                                                <Load EventHandler="GridOperators_Operators_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField ID="ErrorHiddenField_Operators" runat="server" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridOperators_Operators_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridOperators_Operators_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
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
                        <img id="Img1" runat="server" alt="" src="~/Images/Dialog/Waiting.gif" />
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogWaiting_onShow" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <asp:HiddenField runat="server" ID="hfTitle_DialogOperators" meta:resourcekey="hfTitle_DialogOperators" />
    <asp:HiddenField runat="server" ID="hfheader_Operators_Operators" meta:resourcekey="hfheader_Operators_Operators" />
    <asp:HiddenField runat="server" ID="hfView_Operators" meta:resourcekey="hfView_Operators" />
    <asp:HiddenField runat="server" ID="hfAdd_Operators" meta:resourcekey="hfAdd_Operators" />
    <asp:HiddenField runat="server" ID="hfDelete_Operators" meta:resourcekey="hfDelete_Operators" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_Operators" meta:resourcekey="hfDeleteMessage_Operators" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Operators" meta:resourcekey="hfCloseMessage_Operators" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridOperators_Operators" meta:resourcekey="hfloadingPanel_GridOperators_Operators" />
    <asp:HiddenField runat="server" ID="hfErrorType_Operators" meta:resourcekey="hfErrorType_Operators" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Operators" meta:resourcekey="hfConnectionError_Operators" />
    <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_Operators" meta:resourcekey="hfclmnName_cmbPersonnel_Operators" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_Operators" meta:resourcekey="hfclmnBarCode_cmbPersonnel_Operators" />
    <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_Operators" meta:resourcekey="hfclmnCardNum_cmbPersonnel_Operators" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_Operators" />
    </form>
</body>
</html>
