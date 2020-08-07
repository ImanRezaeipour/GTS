<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagersWorkFlow.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.ManagersWorkFlow" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="ManagersWorkFlowForm" runat="server" meta:resourcekey="ManagersWorkFlowForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table id="Mastertbl_ManagersWorkFlow" style="width: 100%; font-family: Arial;" class="BoxStyle">
        <tr>
            <td>
                <ComponentArt:ToolBar ID="TlbManagersWorkFlow" runat="server" CssClass="toolbar"
                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                    UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbManagersWorkFlow" runat="server" ClientSideCommand="tlbItemNew_TlbManagersWorkFlow_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbManagersWorkFlow"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbManagersWorkFlow" runat="server" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                            ClientSideCommand="tlbItemDelete_TlbManagersWorkFlow_onClick();" ItemType="Command"
                            meta:resourcekey="tlbItemDelete_TlbManagersWorkFlow" TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbManagersWorkFlow" runat="server" DropDownImageHeight="16px"
                            ClientSideCommand="tlbItemSave_TlbManagersWorkFlow_onClick();" DropDownImageWidth="16px"
                            ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbManagersWorkFlow"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbManagersWorkFlow" runat="server" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbManagersWorkFlow" TextImageSpacing="5"
                            ClientSideCommand="tlbItemHelp_TlbManagersWorkFlow_onClick();" />
                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbManagersWorkFlow" runat="server"
                            ClientSideCommand="tlbItemFormReconstruction_TlbManagersWorkFlow_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbManagersWorkFlow"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbManagersWorkFlow" runat="server" ClientSideCommand="tlbItemExit_TlbManagersWorkFlow_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbManagersWorkFlow"
                            TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 5%">
                                        <input id="rdbManagersSearch_ManagersWorkFlow" type="radio" name="SwitchSearch_ManagersWorkFlow"
                                            onclick="rdbManagersSearch_ManagersWorkFlow_onClick();" disabled="disabled" />
                                    </td>
                                    <td style="width: 95%">
                                        <asp:Label ID="lblManagersSearch_ManagersWorkFlow" runat="server" CssClass="WhiteLabel"
                                            meta:resourcekey="lblManagersSearch_ManagersWorkFlow" Text="جستجوی مدیر"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 5%">
                                        <input id="rdbPostsSearch_ManagersWorkFlow" type="radio" name="SwitchSearch_ManagersWorkFlow"
                                            onclick="rdbPostsSearch_ManagersWorkFlow_onClick();" disabled="disabled" />
                                    </td>
                                    <td style="width: 95%">
                                        <asp:Label ID="lblPostsSearch_ManagersWorkFlow" runat="server" Text="جستجوی پست"
                                            meta:resourcekey="lblPostsSearch_ManagersWorkFlow" CssClass="WhiteLabel"></asp:Label>
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
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 2%">
                                        <input id="chbActiveFlow_ManagersWorkFlow" type="checkbox" />
                                    </td>
                                    <td style="width: 98%">
                                        <asp:Label ID="lblActiveFlow_ManagersWorkFlow" runat="server" Text="جریان فعال" CssClass="WhiteLabel"
                                            meta:resourcekey="lblActiveFlow_ManagersWorkFlow"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 2%">
                                        <input id="chbMainFlow_ManagersWorkFlow" type="checkbox" />
                                    </td>
                                    <td style="width: 98%">
                                        <asp:Label ID="lblMainFlow_ManagersWorkFlow" runat="server" Text="جریان اصلی" CssClass="WhiteLabel"
                                            meta:resourcekey="lblMainFlow_ManagersWorkFlow"></asp:Label>
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
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 95%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_ManagersWorkFlow_ManagersWorkFlow" class="HeaderLabel" style="width: 45%">
                                                    Managers Flow
                                                </td>
                                                <td id="loadingPanel_GridManagersWorkFlow_ManagersWorkFlow" class="HeaderLabel" style="width: 50%">
                                                </td>
                                                <td id="Td5" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridManagersWorkFlow_ManagersWorkFlow" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridManagersWorkFlow_ManagersWorkFlow"
                                                                runat="server" ClientSideCommand="Refresh_GridManagersWorkFlow_ManagersWorkFlow();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridManagersWorkFlow_ManagersWorkFlow"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridManagersWorkFlow_ManagersWorkFlow"
                                            OnCallback="CallBack_GridManagersWorkFlow_ManagersWorkFlow_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridManagersWorkFlow_ManagersWorkFlow" runat="server"
                                                    CssClass="Grid" EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter"
                                                    ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                                    PageSize="10" RunningMode="Client" SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false"
                                                    ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                    ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                            HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                                                            SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                            SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" DataKeyField="ID">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                <ComponentArt:GridColumn Visible="false" DataField="OwnerID" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="ManagerType" DefaultSortDirection="Descending"
                                                                    DataCellClientTemplateId="DataCellClientTemplateId_clmnType_GridManagersWorkFlow_ManagersWorkFlow"
                                                                    HeadingText="نوع" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnType_GridManagersWorkFlow_ManagersWorkFlow"
                                                                    Width="70" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnName_GridManagersWorkFlow_ManagersWorkFlow" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientTemplates>
                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnType_GridManagersWorkFlow_ManagersWorkFlow">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td align="center" style="font-family: Verdana; font-size: 10px;">
                                                                        <img src="##SetImage_clmnType_GridManagersWorkFlow_ManagersWorkFlow(DataItem.GetMember('ManagerType').Value)##"
                                                                            alt="" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ComponentArt:ClientTemplate>
                                                    </ClientTemplates>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridManagersWorkFlow_ManagersWorkFlow_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_ManagersWorkFlow" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridManagersWorkFlow_ManagersWorkFlow_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridManagersWorkFlow_ManagersWorkFlow_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 5%" align="center" valign="middle">
                            <ComponentArt:ToolBar ID="TlbInterAction_ManagersWorkFlow" runat="server" CssClass="verticaltoolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" Orientation="Vertical" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemUp_TlbInterAction_ManagersWorkFlow" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                        ImageUrl="arrow-up.png" ImageWidth="16px" ItemType="Command" ClientSideCommand="tlbItemUp_TlbInterAction_ManagersWorkFlow_onClick();"
                                        meta:resourcekey="tlbItemUp_TlbInterAction_ManagersWorkFlow" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDown_TlbInterAction_ManagersWorkFlow" runat="server"
                                        ClientSideCommand="tlbItemDown_TlbInterAction_ManagersWorkFlow_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="arrow-down.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDown_TlbInterAction_ManagersWorkFlow"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogManagerSearch"
        runat="server" Width="350px">
        <Content>
            <table style="width: 100%;" class="BodyStyle">
                <tr>
                    <td id="Td1" runat="server" colspan="2">
                        <ComponentArt:ToolBar ID="tlbPersonnelSearch" runat="server" DefaultItemActiveCssClass="itemActive"
                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                            CssClass="toolbar" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                            UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemSave_tlbPersonnelSearch" runat="server" DropDownImageHeight="16px"
                                    ClientSideCommand="tlbItemSave_tlbPersonnelSearch_onClick();" DropDownImageWidth="16px"
                                    ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_tlbPersonnelSearch"
                                    TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemCancel_tlbPersonnelSearch" runat="server" DropDownImageHeight="16px"
                                    ClientSideCommand="tlbItemCancel_tlbPersonnelSearch_onClick();" DropDownImageWidth="16px"
                                    ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_tlbPersonnelSearch"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td style="width: 90%">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblPersonnel_ManagersSearch_ManagersWorkFlow" runat="server" CssClass="WhiteLabel"
                                        meta:resourcekey="lblPersonnel_ManagersSearch_ManagersWorkFlow" Text=": پرسنل"></asp:Label>
                                </td>
                                <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj">
                                    <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_ManagersWorkFlow" runat="server"
                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                        UseFadeEffect="false" Style="direction: ltr">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_ManagersWorkFlow"
                                                runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_ManagersWorkFlow_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_ManagersWorkFlow"
                                                TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_ManagersWorkFlow"
                                                runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_ManagersWorkFlow_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_ManagersWorkFlow"
                                                TextImageSpacing="5" ImageUrl="first.png" />
                                            <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_ManagersWorkFlow"
                                                runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_ManagersWorkFlow_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_ManagersWorkFlow"
                                                TextImageSpacing="5" ImageUrl="Before.png" />
                                            <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_ManagersWorkFlow"
                                                runat="server" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_ManagersWorkFlow_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_ManagersWorkFlow"
                                                TextImageSpacing="5" ImageUrl="Next.png" />
                                            <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_ManagersWorkFlow"
                                                runat="server" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_ManagersWorkFlow_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_ManagersWorkFlow"
                                                TextImageSpacing="5" ImageUrl="last.png" />
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
                        <ComponentArt:CallBack ID="CallBack_cmbPersonnel_ManagersWorkFlow" runat="server"
                            OnCallback="CallBack_cmbPersonnel_ManagersWorkFlow_onCallBack" Height="26">
                            <Content>
                                <ComponentArt:ComboBox ID="cmbPersonnel_ManagersWorkFlow" runat="server" AutoComplete="true"
                                    AutoHighlight="false" CssClass="comboBox" DataTextField="FirstNameAndLastName"
                                    DataFields="BarCode,CardNum" DropDownCssClass="comboDropDown" DropDownHeight="190"
                                    DropDownPageSize="6" DropDownWidth="400" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                    ItemClientTemplateId="ItemTemplate_cmbPersonnel_ManagersWorkFlow" ItemCssClass="comboItem"
                                    ItemHoverCssClass="comboItemHover" RunningMode="Client" SelectedItemCssClass="comboItemHover"
                                    Style="width: 100%" TextBoxCssClass="comboTextBox" meta:resourcekey="cmbPersonnel_ManagersWorkFlow" TextBoxEnabled="true">
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_ManagersWorkFlow">
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
                                                <td id="clmnName_cmbPersonnel_ManagersWorkFlow" class="headingCell" style="width: 40%;
                                                    text-align: center">
                                                    Name And Family
                                                </td>
                                                <td id="clmnBarCode_cmbPersonnel_ManagersWorkFlow" class="headingCell" style="width: 30%;
                                                    text-align: center">
                                                    BarCode
                                                </td>
                                                <td id="clmnCardNum_cmbPersonnel_ManagersWorkFlow" class="headingCell" style="width: 30%;
                                                    text-align: center">
                                                    BarCode
                                                </td>
                                            </tr>
                                        </table>
                                    </DropDownHeader>
                                    <ClientEvents>
                                        <Expand EventHandler="cmbPersonnel_ManagersWorkFlow_onExpand" />
                                    </ClientEvents>
                                </ComponentArt:ComboBox>
                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Personnel_ManagersWorkFlow" />
                                <asp:HiddenField runat="server" ID="hfPersonnelPageCount_ManagersWorkFlow" />
                            </Content>
                            <ClientEvents>
                                <BeforeCallback EventHandler="CallBack_cmbPersonnel_ManagersWorkFlow_onBeforeCallback" />
                                <CallbackComplete EventHandler="CallBack_cmbPersonnel_ManagersWorkFlow_onCallbackComplete" />
                                <CallbackError EventHandler="CallBack_cmbPersonnel_ManagersWorkFlow_onCallbackError" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 90%">
                        <input id="txtPersonnelSearch_ManagersWorkFlow" runat="server" class="TextBoxes"
                            style="width: 95%" type="text" onclick="this.select();" onfocus="this.select();" />
                    </td>
                    <td style="width: 10%">
                        <ComponentArt:ToolBar ID="TlbSearchPersonnel_ManagersWorkFlow" runat="server" CssClass="toolbar"
                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_ManagersWorkFlow"
                                    runat="server" ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_ManagersWorkFlow_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_ManagersWorkFlow"
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
                        <ComponentArt:ToolBar ID="TlbAdvancedSearch_ManagersWorkFlow" runat="server" CssClass="toolbar"
                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_ManagersWorkFlow"
                                    runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_ManagersWorkFlow_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_ManagersWorkFlow"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td style="width: 90%">
                        <asp:Label ID="lblOrganizationPost_ManagersSearch_ManagersWorkFlow" runat="server"
                            CssClass="WhiteLabel" meta:resourcekey="lblOrganizationPost_ManagersSearch_ManagersWorkFlow"
                            Text=": پست سازمانی"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 90%">
                        <input id="txtOrganizationPost_ManagersWorkFlow" class="TextBoxes" style="width: 99%"
                            onclick="this.select();" onfocus="this.select();" readonly="readonly" />
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </Content>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogOrganizationPostSearch"
        runat="server" Width="350px">
        <Content>
            <table style="width: 100%;" class="BodyStyle">
                <tr>
                    <td id="Td4" runat="server">
                        <ComponentArt:ToolBar ID="TlbOrganizationPostSearch" runat="server" DefaultItemActiveCssClass="itemActive"
                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                            CssClass="toolbar" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                            UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbOrganizationPostSearch" runat="server"
                                    DropDownImageHeight="16px" ClientSideCommand="tlbItemSave_TlbOrganizationPostSearch_onClick();"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemSave_TlbOrganizationPostSearch" TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbOrganizationPostSearch" runat="server"
                                    DropDownImageHeight="16px" ClientSideCommand="tlbItemCancel_TlbOrganizationPostSearch_onClick();"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemCancel_TlbOrganizationPostSearch"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;" class="BoxStyle">
                            <tr>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td id="header_OrganizationPosts_ManagersWorkFlow" class="HeaderLabel" style="width: 55%">
                                            </td>
                                            <td id="loadingPanel_trvOrganizationPosts_ManagersWorkFlow" class="HeaderLabel" style="width: 40%">
                                            </td>
                                            <td>
                                                <ComponentArt:ToolBar ID="TlbRefresh_trvOrganizationPosts_ManagersWorkFlow" runat="server"
                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvOrganizationPosts_ManagersWorkFlow"
                                                            runat="server" ClientSideCommand="Refresh_trvOrganizationPosts_ManagersWorkFlow();"
                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvOrganizationPosts_ManagersWorkFlow"
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
                                    <ComponentArt:CallBack runat="server" ID="CallBack_trvOrganizationPosts_ManagersWorkFlow"
                                        OnCallback="CallBack_trvOrganizationPosts_ManagersWorkFlow_onCallBack">
                                        <Content>
                                            <ComponentArt:TreeView ID="trvOrganizationPosts_ManagersWorkFlow" runat="server"
                                                BorderColor="Black" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                                DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="true" EnableViewState="false"
                                                ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif"
                                                FillContainer="false" Height="180" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2"
                                                KeyboardEnabled="true" LineImageHeight="20" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                                LineImageWidth="19" meta:resourcekey="trvOrganizationPost_ManagersWorkFlow" NodeCssClass="TreeNode"
                                                NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode"
                                                ShowLines="true">
                                                <ClientEvents>
                                                    <Load EventHandler="trvOrganizationPosts_ManagersWorkFlow_onLoad" />
                                                    <CallbackComplete EventHandler="trvOrganizationPosts_ManagersWorkFlow_onCallbackComplete" />
                                                    <NodeBeforeExpand EventHandler="trvOrganizationPosts_ManagersWorkFlow_onNodeBeforeExpand" />
                                                </ClientEvents>
                                            </ComponentArt:TreeView>
                                            <asp:HiddenField ID="ErrorHiddenField_OrganizationPosts_ManagersWorkFlow" runat="server" />
                                        </Content>
                                        <ClientEvents>
                                            <CallbackComplete EventHandler="CallBack_trvOrganizationPosts_ManagersWorkFlow_onCallbackComplete" />
                                            <CallbackError EventHandler="CallBack_trvOrganizationPosts_ManagersWorkFlow_onCallbackError" />
                                        </ClientEvents>
                                    </ComponentArt:CallBack>
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
                                    <table style="width: 100%;" class="BoxStyle">
                                        <tr>
                                            <td class="DetailsBoxHeaderStyle">
                                                <div id="header_OrganizationPostSearch_ManagersWorkFlow" runat="server" meta:resourcekey="AlignObj"
                                                    style="color: White; width: 100%; height: 100%" class="BoxContainerHeader">
                                                    جستجوی پست سازمانی</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSearchTerm_ManagersWorkFlow" runat="server" Text=": جستجوی پست سازمانی"
                                                    meta:resourcekey="lblSearchTerm_ManagersWorkFlow" CssClass="WhiteLabel"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td colspan="2">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 50%">
                                                                        <input id="txtSearchTerm_ManagersWorkFlow" type="text" class="TextBoxes" onclick="this.select();"
                                                                            style="width: 97%" onfocus="this.select();" />
                                                                    </td>
                                                                    <td>
                                                                        <ComponentArt:ToolBar ID="TlbPostSearch_ManagersWorkFlow" runat="server" CssClass="toolbar"
                                                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemPostSearch_TlbPostSearch_ManagersWorkFlow" runat="server"
                                                                                    ClientSideCommand="tlbItemPostSearch_TlbPostSearch_ManagersWorkFlow_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemPostSearch_TlbPostSearch_ManagersWorkFlow"
                                                                                    TextImageSpacing="5" Enabled="true" />
                                                                            </Items>
                                                                        </ComponentArt:ToolBar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%">
                                                            <asp:Label ID="lblPostSearchResult_ManagersWorkFlow" runat="server" Text=": نتایج جستجوی پست"
                                                                meta:resourcekey="lblPostSearchResult_ManagersWorkFlow" CssClass="WhiteLabel"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPersonnel_ManagersWorkFlow" runat="server" Text=": پرسنل" meta:resourcekey="lblPersonnel_ManagersWorkFlow"
                                                                CssClass="WhiteLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbPostSearchResult_ManagersWorkFlow"
                                                                OnCallback="CallBack_cmbPostSearchResult_ManagersWorkFlow_onCallBack" Height="26">
                                                                <Content>
                                                                    <ComponentArt:ComboBox ID="cmbPostSearchResult_ManagersWorkFlow" runat="server" AutoComplete="true"
                                                                        AutoHighlight="false" CssClass="comboBox" DataFields="BarCode" ExpandDirection="Up"
                                                                        DataTextField="Name" DropDownCssClass="comboDropDown" DropDownHeight="150" DropDownPageSize="10"
                                                                        DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                        ItemHoverCssClass="comboItemHover" RunningMode="Client" SelectedItemCssClass="comboItemHover"
                                                                        Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                    </ComponentArt:ComboBox>
                                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_PostSearchResult_ManagersWorkFlow" />
                                                                </Content>
                                                                <ClientEvents>
                                                                    <BeforeCallback EventHandler="CallBack_cmbPostSearchResult_ManagersWorkFlow_onBeforeCallback" />
                                                                    <CallbackComplete EventHandler="CallBack_cmbPostSearchResult_ManagersWorkFlow_onCallbackComplete" />
                                                                    <CallbackError EventHandler="CallBack_cmbPostSearchResult_ManagersWorkFlow_onCallbackError" />
                                                                </ClientEvents>
                                                            </ComponentArt:CallBack>
                                                        </td>
                                                        <td>
                                                            <input id="txtPersonnel_ManagersWorkFlow" type="text" class="TextBoxes" style="width: 97%"
                                                                onclick="this.select();" onfocus="this.select();" readonly="readonly" />
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
        </Content>
    </ComponentArt:Dialog>
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
    <asp:HiddenField runat="server" ID="hfheader_ManagersWorkFlow_ManagersWorkFlow" meta:resourcekey="hfheader_ManagersWorkFlow_ManagersWorkFlow" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridManagersWorkFlow_ManagersWorkFlow"
        meta:resourcekey="hfloadingPanel_GridManagersWorkFlow_ManagersWorkFlow" />
    <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_ManagersWorkFlow" meta:resourcekey="hfclmnName_cmbPersonnel_ManagersWorkFlow" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_ManagersWorkFlow"
        meta:resourcekey="hfclmnBarCode_cmbPersonnel_ManagersWorkFlow" />
    <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_ManagersWorkFlow"
        meta:resourcekey="hfclmnCardNum_cmbPersonnel_ManagersWorkFlow" />
    <asp:HiddenField runat="server" ID="hfheader_tblOrganizationPostsSearch_DialogOrganizationPostSearch"
        meta:resourcekey="hfheader_tblOrganizationPostsSearch_DialogOrganizationPostSearch" />
    <asp:HiddenField runat="server" ID="hfheader_OrganizationPosts_ManagersWorkFlow"
        meta:resourcekey="hfheader_OrganizationPosts_ManagersWorkFlow" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvOrganizationPosts_ManagersWorkFlow"
        meta:resourcekey="hfloadingPanel_trvOrganizationPosts_ManagersWorkFlow" />
    <asp:HiddenField runat="server" ID="hfheader_OrganizationPostSearch_ManagersWorkFlow"
        meta:resourcekey="hfheader_OrganizationPostSearch_ManagersWorkFlow" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_ManagersWorkFlow" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_ManagersWorkFlow" meta:resourcekey="hfDeleteMessage_ManagersWorkFlow" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_ManagersWorkFlow" meta:resourcekey="hfCloseMessage_ManagersWorkFlow" />
    <asp:HiddenField runat="server" ID="hfFlowPartsRange_ManagersWorkFlow" meta:resourcekey="hfFlowPartsRange_ManagersWorkFlow" />
    <asp:HiddenField runat="server" ID="hfRetErrorType_ManagersWorkFlow" meta:resourcekey="hfRetErrorType_ManagersWorkFlow" />
    <asp:HiddenField runat="server" ID="hfErrorType_ManagersWorkFlow" meta:resourcekey="hfErrorType_ManagersWorkFlow" />
    <asp:HiddenField runat="server" ID="hfConnectionError_ManagersWorkFlow" meta:resourcekey="hfConnectionError_ManagersWorkFlow" />
    </form>
</body>
</html>
