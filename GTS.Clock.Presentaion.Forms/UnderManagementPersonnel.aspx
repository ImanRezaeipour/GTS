<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnderManagementPersonnel.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.UnderManagementPersonnel" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/menuStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="UnderManagementPersonnelForm" runat="server" meta:resourcekey="UnderManagementPersonnelForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <ComponentArt:Menu ID="contextMenu_trvOrganizationPersonnel_UnderManagementPersonnel"
        Orientation="Vertical" DefaultGroupCssClass="Group" DefaultItemLookId="DefaultItemLook"
        DefaultGroupItemSpacing="1" ImagesBaseUrl="images/Menu" EnableViewState="false"
        ContextMenu="Custom" runat="server">
        <Items>
            <ComponentArt:MenuItem ID="AddItem_contextMenu_trvOrganizationPersonnel_UnderManagementPersonnel"
                Look-LeftIconHeight="16px" Look-LeftIconWidth="16px" Look-RightIconWidth="16px"
                Look-RightIconHeight="16px" runat="server" meta:resourcekey="AddItem_contextMenu_trvOrganizationPersonnel_UnderManagementPersonnel">
            </ComponentArt:MenuItem>
        </Items>
        <ClientEvents>
            <ItemSelect EventHandler="contextMenu_trvOrganizationPersonnel_UnderManagementPersonnel_onItemSelect" />
        </ClientEvents>
        <ItemLooks>
            <ComponentArt:ItemLook LookId="DefaultItemLook" CssClass="Item" HoverCssClass="ItemHover"
                ExpandedCssClass="ItemHover" LeftIconWidth="20" LeftIconHeight="18" LabelPaddingLeft="10"
                LabelPaddingRight="10" LabelPaddingTop="3" LabelPaddingBottom="4" />
        </ItemLooks>
    </ComponentArt:Menu>
    <ComponentArt:Menu ID="contextMenu_GridUnderManagementPersonnel_UnderManagementPersonnel"
        Orientation="Vertical" DefaultGroupCssClass="Group" DefaultItemLookId="DefaultItemLook"
        DefaultGroupItemSpacing="1" ImagesBaseUrl="images/Menu" EnableViewState="false"
        ContextMenu="Custom" runat="server">
        <Items>
            <ComponentArt:MenuItem ID="DeleteItem_contextMenu_GridUnderManagementPersonnel_UnderManagementPersonnel"
                Look-LeftIconHeight="16px" Look-LeftIconWidth="16px" Look-RightIconWidth="16px"
                Look-RightIconHeight="16px" runat="server" meta:resourcekey="DeleteItem_contextMenu_GridUnderManagementPersonnel_UnderManagementPersonnel">
            </ComponentArt:MenuItem>
        </Items>
        <ClientEvents>
            <ItemSelect EventHandler="contextMenu_GridUnderManagementPersonnel_UnderManagementPersonnel_onItemSelect" />
        </ClientEvents>
        <ItemLooks>
            <ComponentArt:ItemLook LookId="DefaultItemLook" CssClass="Item" HoverCssClass="ItemHover"
                ExpandedCssClass="ItemHover" LeftIconWidth="20" LeftIconHeight="18" LabelPaddingLeft="10"
                LabelPaddingRight="10" LabelPaddingTop="3" LabelPaddingBottom="4" />
        </ItemLooks>
    </ComponentArt:Menu>
    <div>
        <table id="Mastertbl_UnderManagementPersonnel" style="width: 100%; font-family: Arial;
            font-size: small;" class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbUnderManagementPersonnel" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbUnderManagementPersonnel" runat="server"
                                            ClientSideCommand="tlbItemSave_TlbUnderManagementPersonnel_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemSave_TlbUnderManagementPersonnel"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExeptionAccessCreation_TlbUnderManagementPersonnel"
                                            runat="server" ClientSideCommand="tlbItemExeptionAccessCreation_TlbUnderManagementPersonnel_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="access.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExeptionAccessCreation_TlbUnderManagementPersonnel"
                                            TextImageSpacing="5" Visible="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExeptionAccessView_TlbUnderManagementPersonnel"
                                            runat="server" ClientSideCommand="tlbItemExeptionAccessView_TlbUnderManagementPersonnel_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exceptions.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExeptionAccessView_TlbUnderManagementPersonnel"
                                            TextImageSpacing="5" Visible="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemWorkFlow_TlbUnderManagementPersonnel" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="flowCreate.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemWorkFlow_TlbUnderManagementPersonnel"
                                            TextImageSpacing="5" ClientSideCommand="tlbItemWorkFlow_TlbUnderManagementPersonnel_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbUnderManagementPersonnel" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbUnderManagementPersonnel"
                                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbUnderManagementPersonnel_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbUnderManagementPersonnel"
                                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbUnderManagementPersonnel_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbUnderManagementPersonnel"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbUnderManagementPersonnel" runat="server"
                                            ClientSideCommand="tlbItemExit_TlbUnderManagementPersonnel_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbUnderManagementPersonnel"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="ActionMode_UnderManagementPersonnel" class="ToolbarMode">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="Container_headerBox_UnderManagementPersonnel">
                    <table id="headerBox_UnderManagementPersonnel" style="width: 100%;" class="BoxStyle">
                        <tr>
                            <td id="header_ManagerFeturesBox_UnderManagementPersonnel" style="color: White; font-size: 15px"
                                class="HeaderLabel">
                                مدیر
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%; border: outset 1px black;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblManagerBarcode_UnderManagementPersonnel" runat="server" Text=": بارکد"
                                                meta:resourcekey="lblManagerBarcode_UnderManagementPersonnel" CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblManagerName_UnderManagementPersonnel" runat="server" Text=": نام"
                                                meta:resourcekey="lblManagerName_UnderManagementPersonnel" CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblManagerOrganizationRole_UnderManagementPersonnel" runat="server"
                                                Text=": نقش سازمانی" meta:resourcekey="lblManagerOrganizationRole_UnderManagementPersonnel"
                                                CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblManagerOrganizationPost_UnderManagementPersonnel" runat="server"
                                                Text=": پست سازمانی" meta:resourcekey="lblManagerOrganizationPost_UnderManagementPersonnel"
                                                CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input id="txtManagerBarcode_UnderManagementPersonnel" type="text" readonly="readonly"
                                                class="TextBoxes" onclick="this.select();" onfocus="this.select();" />
                                        </td>
                                        <td>
                                            <input id="txtManagerName_UnderManagementPersonnel" type="text" readonly="readonly"
                                                class="TextBoxes" onclick="this.select();" onfocus="this.select();" />
                                        </td>
                                        <td>
                                            <input id="txtManagerOrganizationRole_UnderManagementPersonnel" type="text" readonly="readonly"
                                                class="TextBoxes" onclick="this.select();" onfocus="this.select();" />
                                        </td>
                                        <td>
                                            <input id="txtManagerOrganizationPost_UnderManagementPersonnel" type="text" readonly="readonly"
                                                class="TextBoxes" onclick="this.select();" onfocus="this.select();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 50%">
                                                        <table style="width: 100%;" id="Container_rdbManagersSearch_UnderManagementPersonnel">
                                                            <tr>
                                                                <td style="width: 5%">
                                                                    <input id="rdbManagersSearch_UnderManagementPersonnel" type="radio" name="SwitchSearch_UnderManagementPersonnel"
                                                                        onclick="rdbManagersSearch_UnderManagementPersonnel_onClick();" />
                                                                </td>
                                                                <td style="width: 95%">
                                                                    <asp:Label ID="lblManagersSearch_UnderManagementPersonnel" runat="server" CssClass="WhiteLabel"
                                                                        meta:resourcekey="lblManagersSearch_UnderManagementPersonnel" Text="جستجوی مدیر"></asp:Label>
                                                                    <div id="box_ManagersSearch_UnderManagementPersonnel" class="dhtmlgoodies_contentBox"
                                                                        style="width: 40%;">
                                                                        <div id="subbox_ManagersSearch_UnderManagementPersonnel" class="dhtmlgoodies_content">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="width: 90%">
                                                                                        <table style="width: 100%;">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblPersonnel_ManagersSearch_UnderManagementPersonnel" runat="server"
                                                                                                        CssClass="WhiteLabel" meta:resourcekey="lblPersonnel_ManagersSearch_UnderManagementPersonnel"
                                                                                                        Text=": پرسنل"></asp:Label>
                                                                                                </td>
                                                                                                <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj">
                                                                                                    <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_UnderManagementPersonnel" runat="server"
                                                                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                                        UseFadeEffect="false" Style="direction: ltr">
                                                                                                        <Items>
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_UnderManagementPersonnel"
                                                                                                                runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_UnderManagementPersonnel_onClick();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_UnderManagementPersonnel"
                                                                                                                TextImageSpacing="5" />
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_UnderManagementPersonnel"
                                                                                                                runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_UnderManagementPersonnel_onClick();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_UnderManagementPersonnel"
                                                                                                                TextImageSpacing="5" ImageUrl="first.png" />
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_UnderManagementPersonnel"
                                                                                                                runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_UnderManagementPersonnel_onClick();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_UnderManagementPersonnel"
                                                                                                                TextImageSpacing="5" ImageUrl="Before.png" />
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_UnderManagementPersonnel"
                                                                                                                runat="server" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_UnderManagementPersonnel_onClick();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_UnderManagementPersonnel"
                                                                                                                TextImageSpacing="5" ImageUrl="Next.png" />
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_UnderManagementPersonnel"
                                                                                                                runat="server" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_UnderManagementPersonnel_onClick();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_UnderManagementPersonnel"
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
                                                                                        <ComponentArt:CallBack ID="CallBack_cmbPersonnel_UnderManagementPersonnel" runat="server"
                                                                                            OnCallback="CallBack_cmbPersonnel_UnderManagementPersonnel_onCallBack" Height="26">
                                                                                            <Content>
                                                                                                <ComponentArt:ComboBox ID="cmbPersonnel_UnderManagementPersonnel" runat="server"
                                                                                                    AutoComplete="true" AutoHighlight="false" CssClass="comboBox" DataTextField="FirstNameAndLastName"
                                                                                                    DataFields="BarCode,CardNum" DropDownCssClass="comboDropDown" DropDownHeight="210"
                                                                                                    DropDownPageSize="7" DropDownWidth="400" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                                    ItemClientTemplateId="ItemTemplate_cmbPersonnel_UnderManagementPersonnel" ItemCssClass="comboItem"
                                                                                                    ItemHoverCssClass="comboItemHover" RunningMode="Client" SelectedItemCssClass="comboItemHover" TextBoxEnabled="true"
                                                                                                    Style="width: 100%" TextBoxCssClass="comboTextBox" meta:resourcekey="cmbPersonnel_UnderManagementPersonnel">
                                                                                                    <ClientTemplates>
                                                                                                        <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_UnderManagementPersonnel">
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
                                                                                                                <td id="clmnName_cmbPersonnel_UnderManagementPersonnel" class="headingCell" style="width: 40%;
                                                                                                                    text-align: center">
                                                                                                                    Name And Family
                                                                                                                </td>
                                                                                                                <td id="clmnBarCode_cmbPersonnel_UnderManagementPersonnel" class="headingCell" style="width: 30%;
                                                                                                                    text-align: center">
                                                                                                                    BarCode
                                                                                                                </td>
                                                                                                                <td id="clmnCardNum_cmbPersonnel_UnderManagementPersonnel" class="headingCell" style="width: 30%;
                                                                                                                    text-align: center">
                                                                                                                    CardNum
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </DropDownHeader>
                                                                                                    <ClientEvents>
                                                                                                        <Change EventHandler="cmbPersonnel_UnderManagementPersonnel_onChange" />
                                                                                                        <Expand EventHandler="cmbPersonnel_UnderManagementPersonnel_onExpand" />
                                                                                                    </ClientEvents>
                                                                                                </ComponentArt:ComboBox>
                                                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Personnel_UnderManagementPersonnel" />
                                                                                                <asp:HiddenField runat="server" ID="hfPersonnelPageCount_UnderManagementPersonnel" />
                                                                                            </Content>
                                                                                            <ClientEvents>
                                                                                                <BeforeCallback EventHandler="CallBack_cmbPersonnel_UnderManagementPersonnel_onBeforeCallback" />
                                                                                                <CallbackComplete EventHandler="CallBack_cmbPersonnel_UnderManagementPersonnel_onCallbackComplete" />
                                                                                                <CallbackError EventHandler="CallBack_cmbPersonnel_UnderManagementPersonnel_onCallbackError" />
                                                                                            </ClientEvents>
                                                                                        </ComponentArt:CallBack>
                                                                                    </td>
                                                                                    <td style="width: 10%">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 90%">
                                                                                        <input id="txtPersonnelSearch_UnderManagementPersonnel" runat="server" class="TextBoxes"
                                                                                            style="width: 95%" type="text" onclick="this.select();" onfocus="this.select();" />
                                                                                    </td>
                                                                                    <td style="width: 10%">
                                                                                        <ComponentArt:ToolBar ID="TlbSearchPersonnel_UnderManagementPersonnel" runat="server"
                                                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                            <Items>
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_UnderManagementPersonnel"
                                                                                                    runat="server" ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_UnderManagementPersonnel_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_UnderManagementPersonnel"
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
                                                                                        <ComponentArt:ToolBar ID="TlbAdvancedSearch_UnderManagementPersonnel" runat="server"
                                                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                            <Items>
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_UnderManagementPersonnel"
                                                                                                    runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_UnderManagementPersonnel_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_UnderManagementPersonnel"
                                                                                                    TextImageSpacing="5" />
                                                                                            </Items>
                                                                                        </ComponentArt:ToolBar>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 90%">
                                                                                        <asp:Label ID="lblOrganizationPost_ManagersSearch_UnderManagementPersonnel" runat="server"
                                                                                            CssClass="WhiteLabel" meta:resourcekey="lblOrganizationPost_ManagersSearch_UnderManagementPersonnel"
                                                                                            Text=": پست سازمانی"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 10%">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 90%">
                                                                                        <input id="txtOrganizationPost_UnderManagementPersonnel" class="TextBoxes" style="width: 99%"
                                                                                            onclick="this.select();" onfocus="this.select();" readonly="readonly" />
                                                                                    </td>
                                                                                    <td style="width: 10%">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 50%">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 5%">
                                                                    <input id="rdbPostsSearch_UnderManagementPersonnel" type="radio" name="SwitchSearch_UnderManagementPersonnel"
                                                                        onclick="rdbPostsSearch_UnderManagementPersonnel_onClick();" />
                                                                </td>
                                                                <td style="width: 95%">
                                                                    <asp:Label ID="lblPostsSearch_UnderManagementPersonnel" runat="server" Text="جستجوی پست"
                                                                        meta:resourcekey="lblPostsSearch_UnderManagementPersonnel" CssClass="WhiteLabel"></asp:Label>
                                                                    <div id="box_postsSearch_UnderManagementPersonnel" class="dhtmlgoodies_contentBox"
                                                                        style="width: 40%;">
                                                                        <div id="subbox_PostsSearch_UnderManagementPersonnel" class="dhtmlgoodies_content">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <table style="width: 100%;" class="BoxStyle">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table style="width: 100%">
                                                                                                        <tr>
                                                                                                            <td id="header_OrganizationPosts_UnderManagementPersonnel" class="HeaderLabel" style="width: 55%">
                                                                                                            </td>
                                                                                                            <td id="loadingPanel_trvOrganizationPosts_UnderManagementPersonnel" class="HeaderLabel"
                                                                                                                style="width: 40%">
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <ComponentArt:ToolBar ID="TlbRefresh_trvOrganizationPosts_UnderManagementPersonnel"
                                                                                                                    runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                                                    <Items>
                                                                                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvOrganizationPosts_UnderManagementPersonnel"
                                                                                                                            runat="server" ClientSideCommand="Refresh_trvOrganizationPosts_UnderManagementPersonnel();"
                                                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvOrganizationPosts_UnderManagementPersonnel"
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
                                                                                                    <ComponentArt:CallBack runat="server" ID="CallBack_trvOrganizationPosts_UnderManagementPersonnel"
                                                                                                        OnCallback="CallBack_trvOrganizationPosts_UnderManagementPersonnel_onCallBack">
                                                                                                        <Content>
                                                                                                            <ComponentArt:TreeView ID="trvOrganizationPosts_UnderManagementPersonnel" runat="server"
                                                                                                                BorderColor="Black" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                                                                                                DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="true" EnableViewState="false"
                                                                                                                ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif"
                                                                                                                FillContainer="false" Height="180" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2"
                                                                                                                KeyboardEnabled="true" LineImageHeight="20" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                                                                                                LineImageWidth="19" meta:resourcekey="trvOrganizationPost_UnderManagementPersonnel"
                                                                                                                NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                                                                                SelectedNodeCssClass="SelectedTreeNode" ShowLines="true">
                                                                                                                <ClientEvents>
                                                                                                                    <NodeSelect EventHandler="trvOrganizationPosts_UnderManagementPersonnel_onNodeSelect" />
                                                                                                                    <Load EventHandler="trvOrganizationPosts_UnderManagementPersonnel_onLoad" />
                                                                                                                    <CallbackComplete EventHandler="trvOrganizationPosts_UnderManagementPersonnel_onCallbackComplete" />
                                                                                                                    <NodeBeforeExpand EventHandler="trvOrganizationPosts_UnderManagementPersonnel_onNodeBeforeExpand" />
                                                                                                                </ClientEvents>
                                                                                                            </ComponentArt:TreeView>
                                                                                                            <asp:HiddenField ID="ErrorHiddenField_OrganizationPosts_UnderManagementPersonnel"
                                                                                                                runat="server" />
                                                                                                        </Content>
                                                                                                        <ClientEvents>
                                                                                                            <CallbackComplete EventHandler="CallBack_trvOrganizationPosts_UnderManagementPersonnel_onCallbackComplete" />
                                                                                                            <CallbackError EventHandler="CallBack_trvOrganizationPosts_UnderManagementPersonnel_onCallbackError" />
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
                                                                                                                <div id="header_OrganizationPostSearch_UnderManagementPersonnel" runat="server" meta:resourcekey="AlignObj"
                                                                                                                    style="color: White; width: 100%; height: 100%" class="BoxContainerHeader">
                                                                                                                    جستجوی پست سازمانی</div>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblSearchTerm_UnderManagementPersonnel" runat="server" Text=": جستجوی پست سازمانی"
                                                                                                                    meta:resourcekey="lblSearchTerm_UnderManagementPersonnel" CssClass="WhiteLabel"></asp:Label>
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
                                                                                                                                        <input id="txtSearchTerm_UnderManagementPersonnel" type="text" class="TextBoxes"
                                                                                                                                            onclick="this.select();" style="width: 97%" onfocus="this.select();" />
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <ComponentArt:ToolBar ID="TlbPostSearch_UnderManagementPersonnel" runat="server"
                                                                                                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                                                                            <Items>
                                                                                                                                                <ComponentArt:ToolBarItem ID="tlbItemPostSearch_TlbPostSearch_UnderManagementPersonnel"
                                                                                                                                                    runat="server" ClientSideCommand="tlbItemPostSearch_TlbPostSearch_UnderManagementPersonnel_onClick();"
                                                                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemPostSearch_TlbPostSearch_UnderManagementPersonnel"
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
                                                                                                                            <asp:Label ID="lblPostSearchResult_UnderManagementPersonnel" runat="server" Text=": نتایج جستجوی پست"
                                                                                                                                meta:resourcekey="lblPostSearchResult_UnderManagementPersonnel" CssClass="WhiteLabel"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblPersonnel_UnderManagementPersonnel" runat="server" Text=": پرسنل"
                                                                                                                                meta:resourcekey="lblPersonnel_UnderManagementPersonnel" CssClass="WhiteLabel"></asp:Label>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbPostSearchResult_UnderManagementPersonnel"
                                                                                                                                OnCallback="CallBack_cmbPostSearchResult_UnderManagementPersonnel_onCallBack"
                                                                                                                                Height="26">
                                                                                                                                <Content>
                                                                                                                                    <ComponentArt:ComboBox ID="cmbPostSearchResult_UnderManagementPersonnel" runat="server"
                                                                                                                                        AutoComplete="true" AutoHighlight="false" CssClass="comboBox" DataFields="BarCode"
                                                                                                                                        ExpandDirection="Up" DataTextField="Name" DropDownCssClass="comboDropDown" DropDownHeight="150"
                                                                                                                                        DropDownPageSize="10" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                                                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                                                                                        ItemHoverCssClass="comboItemHover" RunningMode="Client" SelectedItemCssClass="comboItemHover"
                                                                                                                                        Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                                                                                        <ClientEvents>
                                                                                                                                            <Change EventHandler="cmbPostSearchResult_UnderManagementPersonnel_onChange" />
                                                                                                                                        </ClientEvents>
                                                                                                                                    </ComponentArt:ComboBox>
                                                                                                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_PostSearchResult_UnderManagementPersonnel" />
                                                                                                                                </Content>
                                                                                                                                <ClientEvents>
                                                                                                                                    <BeforeCallback EventHandler="CallBack_cmbPostSearchResult_UnderManagementPersonnel_onBeforeCallback" />
                                                                                                                                    <CallbackComplete EventHandler="CallBack_cmbPostSearchResult_UnderManagementPersonnel_onCallbackComplete" />
                                                                                                                                    <CallbackError EventHandler="CallBack_cmbPostSearchResult_UnderManagementPersonnel_onCallbackError" />
                                                                                                                                </ClientEvents>
                                                                                                                            </ComponentArt:CallBack>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <input id="txtPersonnel_UnderManagementPersonnel" type="text" class="TextBoxes" style="width: 97%"
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
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="lblFlowName_UnderManagementPersonnel" runat="server" Text=": نام جریان"
                                                meta:resourcekey="lblFlowName_UnderManagementPersonnel" CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;<input type="text" id="txtFlowName_UnderManagementPersonnel" style="width: 99%"
                                                class="TextBoxes" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="center" valign="middle" style="width: 4%">
                                &nbsp;
                            </td>
                            <td style="width: 56%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 95%">
                                            <asp:Label ID="lblAccessGroup_UnderManagementPersonnel" runat="server" Text=": گروه دسترسی"
                                                meta:resourcekey="lblAccessGroup_UnderManagementPersonnel" CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                        <td style="width: 5%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbAccessGroup_UnderManagementPersonnel"
                                                OnCallback="CallBack_cmbAccessGroup_UnderManagementPersonnel_OnCallback" Height="26">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbAccessGroup_UnderManagementPersonnel" runat="server"
                                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                        DataTextField="Name" DataValueField="ID" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                        DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                        ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" Style="width: 100%"
                                                        TextBoxCssClass="comboTextBox" TextBoxEnabled="true">
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbAccessGroup_UnderManagementPersonnel_onExpand" />
                                                            <Collapse EventHandler="cmbAccessGroup_UnderManagementPersonnel_onCollapse" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_AccessGroup_UnderManagementPersonnel" />
                                                </Content>
                                                <ClientEvents>
                                                    <BeforeCallback EventHandler="CallBack_cmbAccessGroup_UnderManagementPersonnel_onBeforeCallback" />
                                                    <CallbackComplete EventHandler="CallBack_cmbAccessGroup_UnderManagementPersonnel_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_cmbAccessGroup_UnderManagementPersonnel_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                        <td>
                                            <ComponentArt:ToolBar ID="TlbAccessGroup_UnderManagementPersonnel" runat="server"
                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemAccessGroup_TlbAccessGroup_UnderManagementPersonnel"
                                                        runat="server" ClientSideCommand="tlbItemAccessGroup_TlbAccessGroup_UnderManagementPersonnel_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="accessGroup.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAccessGroup_TlbAccessGroup_UnderManagementPersonnel"
                                                        TextImageSpacing="5" Enabled="true" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%">
                                <table style="width: 100%;" class="BoxStyle">
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td id="header_OrganizationPersonnelBox_UnderManagementPersonnel" class="HeaderLabel"
                                                        style="width: 55%">
                                                        Organization Personnel
                                                    </td>
                                                    <td id="loadingPanel_trvOrganizationPersonnel_UnderManagementPersonnel" class="HeaderLabel"
                                                        style="width: 40%">
                                                    </td>
                                                    <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                        <ComponentArt:ToolBar ID="TlbRefresh_trvOrganizationPersonnel_UnderManagementPersonnel"
                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvOrganizationPersonnel_UnderManagementPersonnel"
                                                                    runat="server" ClientSideCommand="Refresh_trvOrganizationPersonnel_UnderManagementPersonnel();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvOrganizationPersonnel_UnderManagementPersonnel"
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
                                            <ComponentArt:CallBack runat="server" ID="CallBack_trvOrganizationPersonnel_UnderManagementPersonnel"
                                                OnCallback="CallBack_trvOrganizationPersonnel_UnderManagementPersonnel_onCallBack">
                                                <Content>
                                                    <ComponentArt:TreeView ID="trvOrganizationPersonnel_UnderManagementPersonnel" runat="server"
                                                        CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                        DefaultImageWidth="16" DragAndDropEnabled="true" EnableViewState="false" ExpandCollapseImageHeight="15"
                                                        ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" FillContainer="false"
                                                        Height="280" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true"
                                                        LineImageHeight="20" LineImageWidth="19" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                                        NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                        SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" BorderColor="Black"
                                                        meta:resourcekey="trvOrganizationPersonnel_UnderManagementPersonnel">
                                                        <ClientEvents>
                                                            <Load EventHandler="trvOrganizationPersonnel_UnderManagementPersonnel_onLoad" />
                                                            <CallbackComplete EventHandler="trvOrganizationPersonnel_UnderManagementPersonnel_onCallbackComplete" />
                                                            <NodeBeforeExpand EventHandler="trvOrganizationPersonnel_UnderManagementPersonnel_onNodeBeforeExpand" />
                                                            <ContextMenu EventHandler="trvOrganizationPersonnel_UnderManagementPersonnel_onContextMenu" />
                                                        </ClientEvents>
                                                    </ComponentArt:TreeView>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_OrganizationPersonnel_UnderManagementPersonnel" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_trvOrganizationPersonnel_UnderManagementPersonnel_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_trvOrganizationPersonnel_UnderManagementPersonnel_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="center" valign="middle" style="width: 4%">
                                <ComponentArt:ToolBar ID="TlbInterAction_UnderManagementPersonnel" runat="server"
                                    CssClass="verticaltoolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    Orientation="Vertical" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item"
                                    DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px"
                                    DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                    ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemAdd_TlbInterAction_UnderManagementPersonnel"
                                            runat="server" ClientSideCommand="tlbItemAdd_TlbInterAction_UnderManagementPersonnel_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemAdd_TlbInterAction_UnderManagementPersonnel"
                                            TextImageSpacing="5" Enabled="true" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbInterAction_UnderManagementPersonnel"
                                            runat="server" ClientSideCommand="tlbItemDelete_TlbInterAction_UnderManagementPersonnel_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemDelete_TlbInterAction_UnderManagementPersonnel"
                                            TextImageSpacing="5" Enabled="true" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td style="width: 48%" valign="top">
                                <table style="width: 100%;" class="BoxStyle">
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td id="header_UnderManagementPersonnelBox_UnderManagementPersonnel" class="HeaderLabel"
                                                        style="width: 45%">
                                                        UnderManagement Personnel
                                                    </td>
                                                    <td id="loadingPanel_GridUnderManagementPersonnel_UnderManagementPersonnel" class="HeaderLabel"
                                                        style="width: 50%">
                                                    </td>
                                                    <td id="Td5" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                        <ComponentArt:ToolBar ID="TlbRefresh_GridUnderManagementPersonnel_UnderManagementPersonnel"
                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridUnderManagementPersonnel_UnderManagementPersonnel"
                                                                    runat="server" ClientSideCommand="Refresh_GridUnderManagementPersonnel_UnderManagementPersonnel();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridUnderManagementPersonnel_UnderManagementPersonnel"
                                                                    TextImageSpacing="5" />
                                                            </Items>
                                                        </ComponentArt:ToolBar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;">
                                            <ComponentArt:CallBack runat="server" ID="CallBack_GridUnderManagementPersonnel_UnderManagementPersonnel"
                                                OnCallback="CallBack_GridUnderManagementPersonnel_UnderManagementPersonnel_onCallBack">
                                                <Content>
                                                    <ComponentArt:DataGrid ID="GridUnderManagementPersonnel_UnderManagementPersonnel"
                                                        runat="server" CssClass="Grid" EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter"
                                                        ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                                        PageSize="12" RunningMode="Client" SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false"
                                                        ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                        Height="330" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
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
                                                                    <ComponentArt:GridColumn Align="Center" DataField="Contains" DefaultSortDirection="Descending"
                                                                        ColumnType="CheckBox" HeadingText="دسترسی" HeadingTextCssClass="HeadingText"
                                                                        AllowEditing="True" Width="56" meta:resourcekey="clmnAccess_GridUnderManagementPersonnel_UnderManagementPersonnel" />
                                                                    <ComponentArt:GridColumn Align="Center" DataField="Type" DefaultSortDirection="Descending"
                                                                        DataCellClientTemplateId="DataCellClientTemplateId_clmnType_GridUnderManagementPersonnel_UnderManagementPersonnel"
                                                                        Width="70" HeadingText="نوع" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnType_GridUnderManagementPersonnel_UnderManagementPersonnel" />
                                                                    <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                        Width="150" HeadingText="نام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnName_GridUnderManagementPersonnel_UnderManagementPersonnel" />
                                                                    <ComponentArt:GridColumn Align="Center" DataField="ContainInnerChilds" DefaultSortDirection="Descending"
                                                                        ColumnType="CheckBox" Width="136" HeadingText="زیربخش" HeadingTextCssClass="HeadingText"
                                                                        AllowEditing="True" meta:resourcekey="clmnSubDepartment_GridUnderManagementPersonnel_UnderManagementPersonnel" />
                                                                    <ComponentArt:GridColumn DataField="KeyID" Visible="false" />
                                                                </Columns>
                                                            </ComponentArt:GridLevel>
                                                        </Levels>
                                                        <ClientTemplates>
                                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnType_GridUnderManagementPersonnel_UnderManagementPersonnel">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td align="center" style="font-family: Verdana; font-size: 10px;">
                                                                            <img src="##SetImage_clmnType_GridUnderManagementPersonnel_UnderManagementPersonnel(DataItem.GetMember('Type').Value)##"
                                                                                alt="" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ComponentArt:ClientTemplate>
                                                        </ClientTemplates>
                                                        <ClientEvents>
                                                            <Load EventHandler="GridUnderManagementPersonnel_UnderManagementPersonnel_onLoad" />
                                                            <ContextMenu EventHandler="GridUnderManagementPersonnel_UnderManagementPersonnel_onContextMenu" />
                                                            <ItemBeforeCheckChange EventHandler="GridUnderManagementPersonnel_UnderManagementPersonnel_onItemBeforeCheckChange" />
                                                            <ItemCheckChange EventHandler="GridUnderManagementPersonnel_UnderManagementPersonnel_onItemCheckChange" />
                                                        </ClientEvents>
                                                    </ComponentArt:DataGrid>
                                                    <asp:HiddenField runat="server" ID="hfUnderManagementPersonnelList_UnderManagementPersonnel" />
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_UnderManagementPersonnel_UnderManagementPersonnel" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_GridUnderManagementPersonnel_UnderManagementPersonnel_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_GridUnderManagementPersonnel_UnderManagementPersonnel_onCallbackError" />
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
    </div>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        HeaderClientTemplateId="DialogUnderManagementPersonnelExeptionAccessViewheader"
        FooterClientTemplateId="DialogUnderManagementPersonnelExeptionAccessViewfooter"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogUnderManagementPersonnelExeptionAccessView"
        runat="server" PreloadContentUrl="false" ContentUrl="MasterUnderManagementPersonnelExeptionAccessView.aspx"
        IFrameCssClass="UnderManagementPersonnelExeptionAccessView_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogUnderManagementPersonnelExeptionAccessViewheader">
                <table style="width: 653px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogUnderManagementPersonnelExeptionAccessView.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogUnderManagementPersonnelExeptionAccessView_topLeftImage" style="display: block;"
                                src="Images/Dialog/top_left.gif" alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogUnderManagementPersonnelExeptionAccessView" valign="bottom" style="color: White;
                                        font-size: 13px; font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogUnderManagementPersonnelExeptionAccessView" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="DialogUnderManagementPersonnelExeptionAccessView.Close('cancelled')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogUnderManagementPersonnelExeptionAccessView_topRightImage" style="display: block;"
                                src="Images/Dialog/top_right.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogUnderManagementPersonnelExeptionAccessViewfooter">
                <table id="tbl_DialogUnderManagementPersonnelExeptionAccessViewfooter" style="width: 653px"
                    cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogUnderManagementPersonnelExeptionAccessView_downLeftImage" style="display: block;"
                                src="Images/Dialog/down_left.gif" alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogUnderManagementPersonnelExeptionAccessView_downRightImage" style="display: block;"
                                src="Images/Dialog/down_right.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogUnderManagementPersonnelExeptionAccessView_onShow" />
            <OnClose EventHandler="DialogUnderManagementPersonnelExeptionAccessView_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        HeaderClientTemplateId="DialogUnderManagementPersonnelExeptionAccessCreationheader"
        FooterClientTemplateId="DialogUnderManagementPersonnelExeptionAccessCreationfooter"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogUnderManagementPersonnelExeptionAccessCreation"
        runat="server" PreloadContentUrl="false" ContentUrl="UnderManagementPersonnelExeptionAccessCreation.aspx"
        IFrameCssClass="UnderManagementPersonnelExeptionAccessCreation_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogUnderManagementPersonnelExeptionAccessCreationheader">
                <table style="width: 753px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogUnderManagementPersonnelExeptionAccessCreation.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogUnderManagementPersonnelExeptionAccessCreation_topLeftImage" style="display: block;"
                                src="Images/Dialog/top_left.gif" alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogUnderManagementPersonnelExeptionAccessCreation" valign="bottom"
                                        style="color: White; font-size: 13px; font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogUnderManagementPersonnelExeptionAccessCreation" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="DialogUnderManagementPersonnelExeptionAccessCreation.Close('cancelled')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogUnderManagementPersonnelExeptionAccessCreation_topRightImage" style="display: block;"
                                src="Images/Dialog/top_right.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogUnderManagementPersonnelExeptionAccessCreationfooter">
                <table id="tbl_DialogUnderManagementPersonnelExeptionAccessCreationfooter" style="width: 753px"
                    cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogUnderManagementPersonnelExeptionAccessCreation_downLeftImage" style="display: block;"
                                src="Images/Dialog/down_left.gif" alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogUnderManagementPersonnelExeptionAccessCreation_downRightImage" style="display: block;"
                                src="Images/Dialog/down_right.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogUnderManagementPersonnelExeptionAccessCreation_onShow" />
            <OnClose EventHandler="DialogUnderManagementPersonnelExeptionAccessCreation_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        HeaderClientTemplateId="DialogAccessGroupsheader" FooterClientTemplateId="DialogAccessGroupsfooter"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogAccessGroups"
        runat="server" PreloadContentUrl="false" ContentUrl="AccessGroups.aspx" IFrameCssClass="AccessGroups_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogAccessGroupsheader">
                <table style="width: 753px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogAccessGroups.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogAccessGroups_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogAccessGroups" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogAccessGroups" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogAccessGroups_IFrame').src = 'WhitePage.aspx'; parent.document.getElementById('DialogUnderManagementPersonnel_IFrame').contentWindow.UpdateAccessGroups_onAfterAccessGroupsChange(); DialogAccessGroups.Close('cancelled')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogAccessGroups_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogAccessGroupsfooter">
                <table id="tbl_DialogAccessGroupsfooter" style="width: 753px" cellpadding="0" cellspacing="0"
                    border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogAccessGroups_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogAccessGroups_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogAccessGroups_onShow" />
            <OnClose EventHandler="DialogAccessGroups_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        HeaderClientTemplateId="DialogManagersWorkFlowheader" FooterClientTemplateId="DialogManagersWorkFlowfooter"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogManagersWorkFlow"
        runat="server" PreloadContentUrl="false" ContentUrl="ManagersWorkFlow.aspx" IFrameCssClass="ManagersWorkFlow_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogManagersWorkFlowheader">
                <table style="width: 503px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogManagersWorkFlow.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogManagersWorkFlow_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogManagersWorkFlow" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogManagersWorkFlow" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogManagersWorkFlow_IFrame').src = 'WhitePage.aspx'; DialogManagersWorkFlow.Close('cancelled');" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogManagersWorkFlow_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogManagersWorkFlowfooter">
                <table id="tbl_DialogManagersWorkFlowfooter" style="width: 503px" cellpadding="0"
                    cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogManagersWorkFlow_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogManagersWorkFlow_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogManagersWorkFlow_onShow" />
            <OnClose EventHandler="DialogManagersWorkFlow_onClose" />
        </ClientEvents>
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
    <asp:HiddenField runat="server" ID="hfTitle_DialogUnderManagementPersonnel" meta:resourcekey="hfTitle_DialogUnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfheader_ManagerFeturesBox_UnderManagementPersonnel"
        meta:resourcekey="hfheader_ManagerFeturesBox_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfheader_OrganizationPersonnelBox_UnderManagementPersonnel"
        meta:resourcekey="hfheader_OrganizationPersonnelBox_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfheader_UnderManagementPersonnelBox_UnderManagementPersonnel"
        meta:resourcekey="hfheader_UnderManagementPersonnelBox_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfheader_OrganizationPosts_UnderManagementPersonnel"
        meta:resourcekey="hfheader_OrganizationPosts_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfheader_OrganizationPostSearch_UnderManagementPersonnel"
        meta:resourcekey="hfheader_OrganizationPostSearch_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_UnderManagementPersonnel"
        meta:resourcekey="hfclmnName_cmbPersonnel_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_UnderManagementPersonnel"
        meta:resourcekey="hfclmnBarCode_cmbPersonnel_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_UnderManagementPersonnel"
        meta:resourcekey="hfclmnCardNum_cmbPersonnel_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_UnderManagementPersonnel" meta:resourcekey="hfCloseMessage_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvOrganizationPosts_UnderManagementPersonnel"
        meta:resourcekey="hfloadingPanel_trvOrganizationPosts_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvOrganizationPersonnel_UnderManagementPersonnel"
        meta:resourcekey="hfloadingPanel_trvOrganizationPersonnel_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_UnderManagementPersonnel" meta:resourcekey="hfcmbAlarm_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridUnderManagementPersonnel_UnderManagementPersonnel"
        meta:resourcekey="hfloadingPanel_GridUnderManagementPersonnel_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfAdd_UnderManagementPersonnel" meta:resourcekey="hfAdd_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfEdit_UnderManagementPersonnel" meta:resourcekey="hfEdit_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfUndermanagementTypesList_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfErrorType_UnderManagementPersonnel" meta:resourcekey="hfErrorType_UnderManagementPersonnel" />
    <asp:HiddenField runat="server" ID="hfConnectionError_UnderManagementPersonnel" meta:resourcekey="hfConnectionError_UnderManagementPersonnel" />
    </form>
</body>
</html>
