<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Substitute.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.Substitute" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="SubstituteForm" runat="server" meta:resourcekey="SubstituteForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table id="Mastertbl_Substitute" style="width: 100%; font-family: Arial; font-size: small;">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbSubstitute" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbSubstitute" runat="server" ClientSideCommand="tlbItemNew_TlbSubstitute_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbSubstitute"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbSubstitute" runat="server" ClientSideCommand="tlbItemEdit_TlbSubstitute_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbSubstitute"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbSubstitute" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemDelete_TlbSubstitute_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemDelete_TlbSubstitute" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSubstituteSettings_TlbSubstitute" runat="server"
                                        DropDownImageHeight="16px" ClientSideCommand="tlbItemSubstituteSettings_TlbSubstitute_onClick();"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="regulation.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSubstituteSettings_TlbSubstitute"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbSubstitute" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbSubstitute" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbSubstitute_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbSubstitute" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemSave_TlbSubstitute_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemSave_TlbSubstitute" TextImageSpacing="5" Enabled="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbSubstitute" runat="server" ClientSideCommand="tlbItemCancel_TlbSubstitute_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbSubstitute"
                                        TextImageSpacing="5" Enabled="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbSubstitute" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbSubstitute_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbSubstitute"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbSubstitute" runat="server" ClientSideCommand="tlbItemExit_TlbSubstitute_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbSubstitute"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_Substitute" class="ToolbarMode">
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
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td valign="top" style="width: 35%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 20%">
                                                    <ComponentArt:ToolBar ID="TlbManagerSelect_Substitute" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemManagerSelect_TlbManagerSelect_Substitute" runat="server"
                                                                ClientSideCommand="tlbItemManagerSelect_TlbManagerSelect_Substitute_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="false" ImageHeight="16px"
                                                                ImageUrl="arrowDown_silver.jpg" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemManagerSelect_TlbManagerSelect_Substitute"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                    <div id="box_ManagersSelect_Substitute" class="dhtmlgoodies_contentBox" style="width: 40%;">
                                                        <div id="subbox_ManagersSelect_Substitute" class="dhtmlgoodies_content">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 90%">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblManager_ManagersSelect_Substitute" runat="server" CssClass="WhiteLabel"
                                                                                        meta:resourcekey="lblManager_ManagersSelect_Substitute" Text=": مدیر"></asp:Label>
                                                                                </td>
                                                                                <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj">
                                                                                    <ComponentArt:ToolBar ID="TlbPaging_ManagersSelect_Substitute" runat="server" CssClass="toolbar"
                                                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                        UseFadeEffect="false" Style="direction: ltr">
                                                                                        <Items>
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_ManagersSelect_Substitute"
                                                                                                runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_ManagersSelect_Substitute_onClick();"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_ManagersSelect_Substitute"
                                                                                                TextImageSpacing="5" />
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_ManagersSelect_Substitute" runat="server"
                                                                                                ClientSideCommand="tlbItemFirst_TlbPaging_ManagersSelect_Substitute_onClick();"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                ImageUrl="first.png" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_ManagersSelect_Substitute"
                                                                                                TextImageSpacing="5" />
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_ManagersSelect_Substitute"
                                                                                                runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_ManagersSelect_Substitute_onClick();"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                ImageUrl="Before.png" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_ManagersSelect_Substitute"
                                                                                                TextImageSpacing="5" />
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_ManagersSelect_Substitute" runat="server"
                                                                                                ClientSideCommand="tlbItemNext_TlbPaging_ManagersSelect_Substitute_onClick();"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                ImageUrl="Next.png" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_ManagersSelect_Substitute"
                                                                                                TextImageSpacing="5" />
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_ManagersSelect_Substitute" runat="server"
                                                                                                ClientSideCommand="tlbItemLast_TlbPaging_ManagersSelect_Substitute_onClick();"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                ImageUrl="last.png" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_ManagersSelect_Substitute"
                                                                                                TextImageSpacing="5" />
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
                                                                        <ComponentArt:CallBack ID="CallBack_cmbManagers_Substitute" runat="server" OnCallback="CallBack_cmbManagers_Substitute_onCallBack"
                                                                            Height="26">
                                                                            <Content>
                                                                                <ComponentArt:ComboBox ID="cmbManagers_Substitute" runat="server" AutoComplete="true"
                                                                                    AutoHighlight="false" CssClass="comboBox" DataFields="BarCode,CardNum" DataTextField="Name"
                                                                                    DropDownCssClass="comboDropDown" DropDownHeight="210" DropDownWidth="400" DropDownPageSize="7"
                                                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbManagers_Substitute"
                                                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client"
                                                                                    SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox"
                                                                                    meta:resourcekey="cmbManagers_Substitute" TextBoxEnabled="true">
                                                                                    <ClientTemplates>
                                                                                        <ComponentArt:ClientTemplate ID="ItemTemplate_cmbManagers_Substitute">
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
                                                                                                <td id="clmnName_cmbManagers_Substitute" class="headingCell" style="width: 40%; text-align: center">
                                                                                                    Name And Family
                                                                                                </td>
                                                                                                <td id="clmnBarCode_cmbManagers_Substitute" class="headingCell" style="width: 30%;
                                                                                                    text-align: center">
                                                                                                    BarCode
                                                                                                </td>
                                                                                                <td id="clmnCardNum_cmbManagers_Substitute" class="headingCell" style="width: 30%;
                                                                                                    text-align: center">
                                                                                                    CardNum
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </DropDownHeader>
                                                                                    <ClientEvents>
                                                                                        <Expand EventHandler="cmbManagers_Substitute_onExpand" />
                                                                                        <Change EventHandler="cmbManagers_Substitute_onChange" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:ComboBox>
                                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Managers_Substitute" />
                                                                                <asp:HiddenField runat="server" ID="hfManagerPageCount_Substitute" />
                                                                            </Content>
                                                                            <ClientEvents>
                                                                                <BeforeCallback EventHandler="CallBack_cmbManagers_Substitute_onBeforeCallback" />
                                                                                <CallbackComplete EventHandler="CallBack_cmbManagers_Substitute_onCallbackComplete" />
                                                                                <CallbackError EventHandler="CallBack_cmbManagers_Substitute_onCallbackError" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:CallBack>
                                                                    </td>
                                                                    <td style="width: 10%">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 90%">
                                                                        <input id="txtManagerSearch_Substitute" runat="server" class="TextBoxes" style="width: 95%"
                                                                            type="text" />
                                                                    </td>
                                                                    <td style="width: 10%">
                                                                        <ComponentArt:ToolBar ID="TlbManagerSearch_Substitute" runat="server" CssClass="toolbar"
                                                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbManagerSearch_Substitute" runat="server"
                                                                                    ClientSideCommand="tlbItemSearch_TlbManagerSearch_Substitute_onClick();" DropDownImageHeight="16px"
                                                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                                                    ItemType="Command" meta:resourcekey="tlbItemSearch_TlbManagerSearch_Substitute"
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
                                                                        <ComponentArt:ToolBar ID="TlbAdvancedManagerSearch_Substitute" runat="server" CssClass="toolbar"
                                                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedManagerSearch_Substitute"
                                                                                    runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedManagerSearch_Substitute_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedManagerSearch_Substitute"
                                                                                    TextImageSpacing="5" />
                                                                            </Items>
                                                                        </ComponentArt:ToolBar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td id="tdManagerInfo_Substitute" class="WhiteLabel">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" style="width: 35%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 20%">
                                                    <ComponentArt:ToolBar ID="TlbSubstituteSelect_Substitute" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemSubstituteSelect_TlbSubstituteSelect_Substitute"
                                                                runat="server" ClientSideCommand="tlbItemSubstituteSelect_TlbSubstituteSelect_Substitute_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="false" ImageHeight="16px"
                                                                ImageUrl="arrowDown_silver.jpg" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSubstituteSelect_TlbSubstituteSelect_Substitute"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                    <div id="box_SubstitutesSelect_Substitute" class="dhtmlgoodies_contentBox" style="width: 40%;">
                                                        <div id="subbox_SubstitutesSelect_Substitute" class="dhtmlgoodies_content">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 90%">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblSubstitute_SubstitutesSelect_Substitute" runat="server" CssClass="WhiteLabel"
                                                                                        meta:resourcekey="lblSubstitute_SubstitutesSelect_Substitute" Text=": جانشین"></asp:Label>
                                                                                </td>
                                                                                <td id="Td1" runat="server" meta:resourcekey="InverseAlignObj">
                                                                                    <ComponentArt:ToolBar ID="TlbPaging_SubstitutesSelect_Substitute" runat="server"
                                                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                        UseFadeEffect="false" Style="direction: ltr">
                                                                                        <Items>
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_SubstitutesSelect_Substitute"
                                                                                                runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_SubstitutesSelect_Substitute_onClick();"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_SubstitutesSelect_Substitute"
                                                                                                TextImageSpacing="5" />
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_SubstitutesSelect_Substitute"
                                                                                                runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_SubstitutesSelect_Substitute_onClick();"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                ImageUrl="first.png" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_SubstitutesSelect_Substitute"
                                                                                                TextImageSpacing="5" />
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_SubstitutesSelect_Substitute"
                                                                                                runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_SubstitutesSelect_Substitute_onClick();"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                ImageUrl="Before.png" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_SubstitutesSelect_Substitute"
                                                                                                TextImageSpacing="5" />
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_SubstitutesSelect_Substitute"
                                                                                                runat="server" ClientSideCommand="tlbItemNext_TlbPaging_SubstitutesSelect_Substitute_onClick();"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                ImageUrl="Next.png" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_SubstitutesSelect_Substitute"
                                                                                                TextImageSpacing="5" />
                                                                                            <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_SubstitutesSelect_Substitute"
                                                                                                runat="server" ClientSideCommand="tlbItemLast_TlbPaging_SubstitutesSelect_Substitute_onClick();"
                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                ImageUrl="last.png" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_SubstitutesSelect_Substitute"
                                                                                                TextImageSpacing="5" />
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
                                                                        <ComponentArt:CallBack ID="CallBack_cmbPersonnel_Substitute" runat="server" OnCallback="CallBack_cmbPersonnel_Substitute_onCallBack"
                                                                            Height="26">
                                                                            <Content>
                                                                                <ComponentArt:ComboBox ID="cmbPersonnel_Substitute" runat="server" AutoComplete="true"
                                                                                    AutoHighlight="false" CssClass="comboBox" DataFields="BarCode,CardNum" DataTextField="Name"
                                                                                    DropDownCssClass="comboDropDown" DropDownHeight="210" DropDownWidth="400" DropDownPageSize="7"
                                                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_Substitute"
                                                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client"
                                                                                    SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox"
                                                                                    meta:resourcekey="cmbPersonnel_Substitute" TextBoxEnabled="true">
                                                                                    <ClientTemplates>
                                                                                        <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_Substitute">
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
                                                                                                <td id="clmnName_cmbPersonnel_Substitute" class="headingCell" style="width: 40%;
                                                                                                    text-align: center">
                                                                                                    Name And Family
                                                                                                </td>
                                                                                                <td id="clmnBarCode_cmbPersonnel_Substitute" class="headingCell" style="width: 30%;
                                                                                                    text-align: center">
                                                                                                    BarCode
                                                                                                </td>
                                                                                                <td id="clmnCardNum_cmbPersonnel_Substitute" class="headingCell" style="width: 30%;
                                                                                                    text-align: center">
                                                                                                    CardNum
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </DropDownHeader>
                                                                                    <ClientEvents>
                                                                                        <Expand EventHandler="cmbPersonnel_Substitute_onExpand" />
                                                                                        <Change EventHandler="cmbPersonnel_Substitute_onChange" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:ComboBox>
                                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Personnel_Substitute" />
                                                                                <asp:HiddenField runat="server" ID="hfPersonnelPageCount_Substitute" />
                                                                            </Content>
                                                                            <ClientEvents>
                                                                                <BeforeCallback EventHandler="CallBack_cmbPersonnel_Substitute_onBeforeCallback" />
                                                                                <CallbackComplete EventHandler="CallBack_cmbPersonnel_Substitute_onCallbackComplete" />
                                                                                <CallbackError EventHandler="CallBack_cmbPersonnel_Substitute_onCallbackError" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:CallBack>
                                                                    </td>
                                                                    <td style="width: 10%">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 90%">
                                                                        <input id="txtSubstituteSearch_Substitute" runat="server" class="TextBoxes" style="width: 95%"
                                                                            type="text" />
                                                                    </td>
                                                                    <td style="width: 10%">
                                                                        <ComponentArt:ToolBar ID="TlbSubstituteSearch_Substitute" runat="server" CssClass="toolbar"
                                                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSubstituteSearch_Substitute" runat="server"
                                                                                    ClientSideCommand="tlbItemSearch_TlbSubstituteSearch_Substitute_onClick();" DropDownImageHeight="16px"
                                                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                                                    ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSubstituteSearch_Substitute"
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
                                                                        <ComponentArt:ToolBar ID="TlbAdvancedSubstituteSearch_Substitute" runat="server"
                                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSubstituteSearch_Substitute"
                                                                                    runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSubstituteSearch_Substitute_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSubstituteSearch_Substitute"
                                                                                    TextImageSpacing="5" />
                                                                            </Items>
                                                                        </ComponentArt:ToolBar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td id="tdPersonnelInfo_Substitute" class="WhiteLabel">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFromDate_Substitute" CssClass="WhiteLabel" meta:resourcekey="lblFromDate_Substitute"
                                                        runat="server" Text=": از تاریخ"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Container_MasterFromDateCalendars_Substitute">
                                                    <table runat="server" id="Container_pdpMasterFromDate_Substitute" visible="false"
                                                        style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <pcal:PersianDatePickup ID="pdpMasterFromDate_Substitute" runat="server" CssClass="PersianDatePicker"
                                                                    ReadOnly="true"></pcal:PersianDatePickup>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table runat="server" id="Container_gdpMasterFromDate_Substitute" visible="false"
                                                        style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalMasterFromDate_Substitute">
                                                                    <tr>
                                                                        <td onmouseup="btn_gdpMasterFromDate_Substitute_OnMouseUp(event)">
                                                                            <ComponentArt:Calendar ID="gdpMasterFromDate_Substitute" runat="server" ControlType="Picker"
                                                                                PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gdpMasterFromDate_Substitute_OnDateChange" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                        <td style="font-size: 10px;">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <img id="btn_gdpMasterFromDate_Substitute" alt="" class="calendar_button" onclick="btn_gdpMasterFromDate_Substitute_OnClick(event)"
                                                                                onmouseup="btn_gdpMasterFromDate_Substitute_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <ComponentArt:Calendar ID="gCalMasterFromDate_Substitute" runat="server" AllowMonthSelection="false"
                                                                    AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                    CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                    DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                    MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                    OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpMasterFromDate_Substitute"
                                                                    PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                    SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                                    <ClientEvents>
                                                                        <SelectionChanged EventHandler="gCalMasterFromDate_Substitute_OnChange" />
                                                                    </ClientEvents>
                                                                </ComponentArt:Calendar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblToDate_Substitute" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblToDate_Substitute"
                                                        Text=": تا تاریخ"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Container_MasterToDateCalendars_Substitute">
                                                    <table runat="server" id="Container_pdpMasterToDate_Substitute" visible="false" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <pcal:PersianDatePickup ID="pdpMasterToDate_Substitute" runat="server" CssClass="PersianDatePicker"
                                                                    ReadOnly="true"></pcal:PersianDatePickup>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table runat="server" id="Container_gdpMasterToDate_Substitute" visible="false" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalMasterToDate_Substitute">
                                                                    <tr>
                                                                        <td onmouseup="btn_gdpMasterToDate_Substitute_OnMouseUp(event)">
                                                                            <ComponentArt:Calendar ID="gdpMasterToDate_Substitute" runat="server" ControlType="Picker"
                                                                                PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gdpMasterToDate_Substitute_OnDateChange" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                        <td style="font-size: 10px;">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <img id="btn_gdpMasterToDate_Substitute" alt="" class="calendar_button" onclick="btn_gdpMasterToDate_Substitute_OnClick(event)"
                                                                                onmouseup="btn_gdpMasterToDate_Substitute_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <ComponentArt:Calendar ID="gCalMasterToDate_Substitute" runat="server" AllowMonthSelection="false"
                                                                    AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                    CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                    DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                    MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                    OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpMasterToDate_Substitute"
                                                                    PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                    SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                                    <ClientEvents>
                                                                        <SelectionChanged EventHandler="gCalMasterToDate_Substitute_OnChange" />
                                                                    </ClientEvents>
                                                                </ComponentArt:Calendar>
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
                <table style="width: 100%; border: outset 1px black" class="BoxStyle">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="3">
                                        <table style="width: 45%;" class="BoxStyle">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblQuickSerch_Substitute" runat="server" meta:resourcekey="lblQuickSerch_Substitute"
                                                        Text=": جستجوی سریع" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 80%">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtSerchTerm_Substitute" />
                                                            </td>
                                                            <td style="width: 5%">
                                                                <ComponentArt:ToolBar ID="TlbSubstituteQuickSearch" runat="server" CssClass="toolbar"
                                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                    UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSubstituteQuickSearch" runat="server"
                                                                            ClientSideCommand="tlbItemSearch_TlbSubstituteQuickSearch_onClick();" DropDownImageHeight="16px"
                                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                                            ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSubstituteQuickSearch"
                                                                            TextImageSpacing="5" />
                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="header_Substitutes_Substitute" class="HeaderLabel" style="width: 50%">
                                        Substitutes
                                    </td>
                                    <td id="loadingPanel_GridSubstitutes_Substitute" class="HeaderLabel" style="width: 45%">
                                    </td>
                                    <td id="Td3" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbRefresh_GridSubstitutes_Substitute" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridSubstitutes_Substitute"
                                                    runat="server" ClientSideCommand="Refresh_GridSubstitutes_Substitute();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridSubstitutes_Substitute"
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
                            <ComponentArt:CallBack runat="server" ID="CallBack_GridSubstitutes_Substitute" OnCallback="CallBack_GridSubstitutes_Substitute_onCallback">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridSubstitutes_Substitute" runat="server" CssClass="Grid"
                                        EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                        PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="7" RunningMode="Client"
                                        SearchTextCssClass="GridHeaderText" Width="100%" AllowMultipleSelect="false"
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
                                                    <ComponentArt:GridColumn Align="Center" DataField="Person.PersonCode" DefaultSortDirection="Descending"
                                                        HeadingText="بارکد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnBarCode_Substitute" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Person.Name" DefaultSortDirection="Descending"
                                                        HeadingText="نام و نام خانوادگی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnName_Substitute" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Person.OrganizationUnit.Name"
                                                        DefaultSortDirection="Descending" HeadingText="پست سازمانی" HeadingTextCssClass="HeadingText" 
                                                        meta:resourcekey="clmnOrganizationPost_Substitute" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheFromDate" DefaultSortDirection="Descending"
                                                        HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromDate_Substitute" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="TheToDate" DefaultSortDirection="Descending"
                                                        HeadingText="تا تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToDate_Substitute" />
                                                    <ComponentArt:GridColumn DataField="Person.ID" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="Person.OrganizationUnit.ID" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="Manager.ThePerson.ID" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="Manager.ThePerson.Name" Visible="false" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientEvents>
                                            <ItemSelect EventHandler="GridSubstitutes_Substitute_onItemSelect" />
                                            <Load EventHandler="GridSubstitutes_Substitute_onLoad" />
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_Substitute" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridSubstitutes_Substitute_onCallbackComplete" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
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
    <asp:HiddenField runat="server" ID="hfheader_Substitutes_Substitute" meta:resourcekey="hfheader_Substitutes_Substitute" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridSubstitutes_Substitute" meta:resourcekey="hfloadingPanel_GridSubstitutes_Substitute" />
    <asp:HiddenField runat="server" ID="hfView_Substitute" meta:resourcekey="hfView_Substitute" />
    <asp:HiddenField runat="server" ID="hfAdd_Substitute" meta:resourcekey="hfAdd_Substitute" />
    <asp:HiddenField runat="server" ID="hfEdit_Substitute" meta:resourcekey="hfEdit_Substitute" />
    <asp:HiddenField runat="server" ID="hfDelete_Substitute" meta:resourcekey="hfDelete_Substitute" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_Substitute" meta:resourcekey="hfDeleteMessage_Substitute" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Substitute" meta:resourcekey="hfCloseMessage_Substitute" />
    <asp:HiddenField runat="server" ID="hfErrorType_Substitute" meta:resourcekey="hfErrorType_Substitute" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Substitute" meta:resourcekey="hfConnectionError_Substitute" />
    <asp:HiddenField runat="server" ID="hfclmnName_cmbManagers_Substitute" meta:resourcekey="hfclmnName_cmbManagers_Substitute" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbManagers_Substitute" meta:resourcekey="hfclmnBarCode_cmbManagers_Substitute" />
    <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbManagers_Substitute" meta:resourcekey="hfclmnCardNum_cmbManagers_Substitute" />
    <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_Substitute" meta:resourcekey="hfclmnName_cmbPersonnel_Substitute" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_Substitute" meta:resourcekey="hfclmnBarCode_cmbPersonnel_Substitute" />
    <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_Substitute" meta:resourcekey="hfclmnCardNum_cmbPersonnel_Substitute" />
    <asp:HiddenField runat="server" ID="hfManagerPageSize_Substitute" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_Substitute" />
    <asp:HiddenField runat="server" ID="hfCurrentDate_Substitute" />
    </form>
</body>
</html>
