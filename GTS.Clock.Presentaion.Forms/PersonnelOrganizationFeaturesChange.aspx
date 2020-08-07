<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonnelOrganizationFeaturesChange.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.PersonnelOrganizationFeaturesChange" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="FUA" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
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
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="PersonnelOrganizationFeaturesChangeForm" runat="server" meta:resourcekey="PersonnelOrganizationFeaturesChangeForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table id="Mastertbl_PersonnelOrganizationFeaturesChange" style="width: 100%; font-family: Arial;
        font-size: small" class="BoxStyle">
        <tr>
            <td>
                <ComponentArt:ToolBar ID="TlbPersonnelOrganizationFeaturesChange" runat="server"
                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                    UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbPersonnelOrganizationFeaturesChange"
                            runat="server" ClientSideCommand="tlbItemSave_TlbPersonnelOrganizationFeaturesChange_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                            ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbPersonnelOrganizationFeaturesChange"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbPersonnelOrganizationFeaturesChange"
                            runat="server" ClientSideCommand="tlbItemHelp_TlbPersonnelOrganizationFeaturesChange_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbPersonnelOrganizationFeaturesChange"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbPersonnelOrganizationFeaturesChange"
                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbPersonnelOrganizationFeaturesChange_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbPersonnelOrganizationFeaturesChange"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPersonnelOrganizationFeaturesChange"
                            runat="server" ClientSideCommand="tlbItemExit_TlbPersonnelOrganizationFeaturesChange_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbPersonnelOrganizationFeaturesChange"
                            TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblAllPersonnelsWithThisCondition_PersonnelOrganizationFeaturesChange"
                                runat="server" Text="کلیه پرسنل با این شرایط" class="HeaderLabel" meta:resourcekey="lblAllPersonnelsWithThisCondition_PersonnelOrganizationFeaturesChange"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 80%">
                            <div runat="server" class="DropDownHeader" meta:resourcekey="AlignObj" style="width: 100%">
                                <img id="imgbox_SearchByPersonnel_PersonnelOrganizationFeaturesChange" runat="server"
                                    alt="" onclick="imgbox_SearchByPersonnel_PersonnelOrganizationFeaturesChange_onClick();"
                                    src="~/Images/Ghadir/arrowDown.jpg" />
                                <span id="header_SearchByPersonnelBox_PersonnelOrganizationFeaturesChange">Personnel
                                    Select</span>
                            </div>
                            <div id="box_SearchByPersonnel_PersonnelOrganizationFeaturesChange" class="dhtmlgoodies_contentBox"
                                style="width: 50%;">
                                <div id="subbox_SearchByPersonnel_PersonnelOrganizationFeaturesChange" class="dhtmlgoodies_content">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 90%">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblPersonnel_PersonnelOrganizationFeaturesChange" runat="server" CssClass="WhiteLabel"
                                                                            meta:resourcekey="lblPersonnel_PersonnelOrganizationFeaturesChange" Text=": پرسنل"></asp:Label>
                                                                    </td>
                                                                    <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj">
                                                                        <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange"
                                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                            Style="direction: ltr;" UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange"
                                                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange"
                                                                                    TextImageSpacing="5" />
                                                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange"
                                                                                    runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="first.png"
                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange"
                                                                                    TextImageSpacing="5" />
                                                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange"
                                                                                    runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Before.png"
                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange"
                                                                                    TextImageSpacing="5" />
                                                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange"
                                                                                    runat="server" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Next.png"
                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange"
                                                                                    TextImageSpacing="5" />
                                                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange"
                                                                                    runat="server" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="last.png"
                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_PersonnelOrganizationFeaturesChange"
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
                                                            <ComponentArt:CallBack ID="CallBack_cmbPersonnel_PersonnelOrganizationFeaturesChange"
                                                                runat="server" Height="26" OnCallback="CallBack_cmbPersonnel_PersonnelOrganizationFeaturesChange_onCallBack">
                                                                <Content>
                                                                    <ComponentArt:ComboBox ID="cmbPersonnel_PersonnelOrganizationFeaturesChange" runat="server"
                                                                        AutoComplete="true" AutoHighlight="false" CssClass="comboBox" DataFields="BarCode"
                                                                        DataTextField="Name" DropDownCssClass="comboDropDown" DropDownHeight="210" DropDownPageSize="7"
                                                                        DropDownWidth="390" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_PersonnelOrganizationFeaturesChange"
                                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client"
                                                                        SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                        <ClientTemplates>
                                                                            <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_PersonnelOrganizationFeaturesChange">
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
                                                                            <table id="tblItemTemplate_cmbPersonnel_PersonnelOrganizationFeaturesChange" border="0"
                                                                                cellpadding="0" cellspacing="0" width="390">
                                                                                <tr class="headingRow">
                                                                                    <td id="clmnName_cmbPersonnel_PersonnelOrganizationFeaturesChange" class="headingCell"
                                                                                        style="width: 40%; text-align: center">
                                                                                        Name And Family
                                                                                    </td>
                                                                                    <td id="clmnBarCode_cmbPersonnel_PersonnelOrganizationFeaturesChange" class="headingCell"
                                                                                        style="width: 30%; text-align: center">
                                                                                        BarCode
                                                                                    </td>
                                                                                    <td id="clmnCardNum_cmbPersonnel_PersonnelOrganizationFeaturesChange" class="headingCell"
                                                                                        style="width: 30%; text-align: center">
                                                                                        CardNum
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </DropDownHeader>
                                                                        <ClientEvents>
                                                                            <Change EventHandler="cmbPersonnel_PersonnelOrganizationFeaturesChange_onChange" />
                                                                            <Expand EventHandler="cmbPersonnel_PersonnelOrganizationFeaturesChange_onExpand" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:ComboBox>
                                                                    <asp:HiddenField ID="ErrorHiddenField_Personnel_PersonnelOrganizationFeaturesChange"
                                                                        runat="server" />
                                                                    <asp:HiddenField ID="hfPersonnelPageCount_PersonnelOrganizationFeaturesChange" runat="server" />
                                                                    <asp:HiddenField ID="hfPersonnelCount_PersonnelOrganizationFeaturesChange" runat="server" />
                                                                </Content>
                                                                <ClientEvents>
                                                                    <BeforeCallback EventHandler="CallBack_cmbPersonnel_PersonnelOrganizationFeaturesChange_onBeforeCallback" />
                                                                    <CallbackComplete EventHandler="CallBack_cmbPersonnel_PersonnelOrganizationFeaturesChange_onCallBackComplete" />
                                                                    <CallbackError EventHandler="CallBack_cmbPersonnel_PersonnelOrganizationFeaturesChange_onCallbackError" />
                                                                </ClientEvents>
                                                            </ComponentArt:CallBack>
                                                        </td>
                                                        <td style="width: 10%">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 90%">
                                                            <input id="txtPersonnelSearch_PersonnelOrganizationFeaturesChange" runat="server"
                                                                class="TextBoxes" onclick="this.select();" onfocus="this.select();" style="width: 95%"
                                                                type="text" />
                                                        </td>
                                                        <td style="width: 10%">
                                                            <ComponentArt:ToolBar ID="TlbSearchPersonnel_PersonnelOrganizationFeaturesChange"
                                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_PersonnelOrganizationFeaturesChange"
                                                                        runat="server" ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_PersonnelOrganizationFeaturesChange_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_PersonnelOrganizationFeaturesChange"
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
                                                            <ComponentArt:ToolBar ID="TlbAdvancedSearch_PersonnelOrganizationFeaturesChange"
                                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_PersonnelOrganizationFeaturesChange"
                                                                        runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_PersonnelOrganizationFeaturesChange_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_PersonnelOrganizationFeaturesChange"
                                                                        TextImageSpacing="5" />
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
                        <td id="tdPersonnelCount_PersonnelOrganizationFeaturesChange">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td>
                            <asp:Label ID="lblChangeToThisForm_PersonnelOrganizationFeaturesChange" runat="server"
                                class="HeaderLabel" Text="به شکل زیر تغییر یابند" meta:resourcekey="lblChangeToThisForm_PersonnelOrganizationFeaturesChange"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Label ID="lblRulesGroup_PersonnelOrganizationFeaturesChange" runat="server"
                                            Text=": به گروه قانون" CssClass="WhiteLabel" meta:resourcekey="lblRulesGroup_PersonnelOrganizationFeaturesChange"></asp:Label>
                                    </td>
                                    <td style="width: 35%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <ComponentArt:CallBack runat="server" ID="CallBack_cmbRulesGroup_PersonnelOrganizationFeaturesChange"
                                                        OnCallback="CallBack_cmbRulesGroup_PersonnelOrganizationFeaturesChange_onCallBack"
                                                        Height="26">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbRulesGroup_PersonnelOrganizationFeaturesChange" runat="server"
                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                TextBoxCssClass="comboTextBox" Width="100%" ExpandDirection="Down" DataTextField="Name"
                                                                DataValueField="ID">
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbRulesGroup_PersonnelOrganizationFeaturesChange_onExpand" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_RulesGroup_PersonnelOrganizationFeaturesChange" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbRulesGroup_PersonnelOrganizationFeaturesChange_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbRulesGroup_PersonnelOrganizationFeaturesChange_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbRulesGroup_PersonnelOrganizationFeaturesChange_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                <td style="width: 5%">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_cmbRulesGroup_PersonnelOrganizationFeaturesChange"
                                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbRulesGroup_PersonnelOrganizationFeaturesChange"
                                                                runat="server" ClientSideCommand="Refresh_cmbRulesGroup_PersonnelOrganizationFeaturesChange();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbRulesGroup_PersonnelOrganizationFeaturesChange"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 25%">
                                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblFromDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                        runat="server" CssClass="WhiteLabel" Text=": از تاریخ" meta:resourcekey="lblFromDate_RulesGroup_PersonnelOrganizationFeaturesChange"></asp:Label>
                                                </td>
                                                <td style="width: 80%">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td id="Container_RulesGroupCalendars_FromDate_PersonnelOrganizationFeaturesChange"
                                                                style="width: 60%">
                                                                <table runat="server" id="Container_pdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                                    visible="false" style="width: 100%">
                                                                    <tr>
                                                                        <td>
                                                                            <pcal:PersianDatePickup ID="pdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                                                runat="server" CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table runat="server" id="Container_gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                                    visible="false" style="width: 100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table id="Container_gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                                                border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td onmouseup="btn_gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnMouseUp(event)">
                                                                                        <ComponentArt:Calendar ID="gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                                                            runat="server" ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker"
                                                                                            PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom">
                                                                                            <ClientEvents>
                                                                                                <SelectionChanged EventHandler="gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnDateChange" />
                                                                                            </ClientEvents>
                                                                                        </ComponentArt:Calendar>
                                                                                    </td>
                                                                                    <td style="font-size: 10px;">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td>
                                                                                        <img id="btn_gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange" alt="" class="calendar_button"
                                                                                            onclick="btn_gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnClick(event)"
                                                                                            onmouseup="btn_gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnMouseUp(event)"
                                                                                            src="Images/Calendar/btn_calendar.gif" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <ComponentArt:Calendar ID="gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                                                runat="server" AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                                CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                                DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                                ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                                NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                                PopUpExpandControlId="btn_gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                                                PrevImageUrl="cal_prevMonth.gif" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                                                SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnChange" />
                                                                                    <Load EventHandler="gCalFromDate_RulesGroup_PersonnelOrganizationFeaturesChange_onLoad" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top" style="width: 3%">
                                                                <ComponentArt:ToolBar ID="TlbClear_RulesGroupCalendars_FromDate_PersonnelOrganizationFeaturesChange"
                                                                    runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_RulesGroupCalendars_FromDate_PersonnelOrganizationFeaturesChange"
                                                                            runat="server" ClientSideCommand="tlbItemClear_TlbClear_RulesGroupCalendars_FromDate_PersonnelOrganizationFeaturesChange_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Clean.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_RulesGroupCalendars_FromDate_PersonnelOrganizationFeaturesChange"
                                                                            TextImageSpacing="5" />
                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%">
                                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblToDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                        runat="server" Text=":تا تاریخ" CssClass="WhiteLabel" meta:resourcekey="lblToDate_RulesGroup_PersonnelOrganizationFeaturesChange"></asp:Label>
                                                </td>
                                                <td style="width: 85%">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td id="Container_RulesGroupCalendars_ToDate_PersonnelOrganizationFeaturesChange"
                                                                style="width: 60%">
                                                                <table runat="server" id="Container_pdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                                    visible="false" style="width: 100%">
                                                                    <tr>
                                                                        <td>
                                                                            <pcal:PersianDatePickup ID="pdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                                                runat="server" CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table runat="server" id="Container_gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                                    visible="false" style="width: 100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table id="Container_gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange" border="0"
                                                                                cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td onmouseup="btn_gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnMouseUp(event)">
                                                                                        <ComponentArt:Calendar ID="gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                                                            runat="server" ControlType="Picker" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                            PickerFormat="Custom" SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                                            <ClientEvents>
                                                                                                <SelectionChanged EventHandler="gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnDateChange" />
                                                                                            </ClientEvents>
                                                                                        </ComponentArt:Calendar>
                                                                                    </td>
                                                                                    <td style="font-size: 10px;">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td>
                                                                                        <img id="btn_gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange" alt="" class="calendar_button"
                                                                                            onclick="btn_gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnClick(event)"
                                                                                            onmouseup="btn_gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnMouseUp(event)"
                                                                                            src="Images/Calendar/btn_calendar.gif" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <ComponentArt:Calendar ID="gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                                                runat="server" AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                                CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                                DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                                ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                                NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                                PopUpExpandControlId="btn_gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange"
                                                                                PrevImageUrl="cal_prevMonth.gif" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                                                SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange_OnChange" />
                                                                                    <Load EventHandler="gCalToDate_RulesGroup_PersonnelOrganizationFeaturesChange_onLoad" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top" style="width: 3%">
                                                                <ComponentArt:ToolBar ID="TlbClear_RulesGroupCalendars_ToDate_PersonnelOrganizationFeaturesChange"
                                                                    runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_RulesGroupCalendars_ToDate_PersonnelOrganizationFeaturesChange"
                                                                            runat="server" ClientSideCommand="tlbItemClear_TlbClear_RulesGroupCalendars_ToDate_PersonnelOrganizationFeaturesChange_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Clean.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_RulesGroupCalendars_ToDate_PersonnelOrganizationFeaturesChange"
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
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Label ID="lblWorkGroup_PersonnelOrganizationFeaturesChange" runat="server" CssClass="WhiteLabel"
                                            Text=": به گروه کاری" meta:resourcekey="lblWorkGroup_PersonnelOrganizationFeaturesChange"></asp:Label>
                                    </td>
                                    <td style="width: 35%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 90%">
                                                    <ComponentArt:CallBack runat="server" ID="CallBack_cmbWorkGroup_PersonnelOrganizationFeaturesChange"
                                                        Height="26" OnCallback="CallBack_cmbWorkGroup_PersonnelOrganizationFeaturesChange_onCallBack">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbWorkGroup_PersonnelOrganizationFeaturesChange" runat="server"
                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                TextBoxCssClass="comboTextBox" Width="100%" DataTextField="Name" DataValueField="ID">
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbWorkGroup_PersonnelOrganizationFeaturesChange_onExpand" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_WorkGroup_PersonnelOrganizationFeaturesChange" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbWorkGroup_PersonnelOrganizationFeaturesChange_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbWorkGroup_PersonnelOrganizationFeaturesChange_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbWorkGroup_PersonnelOrganizationFeaturesChange_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                <td style="width: 10%">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_cmbWorkGroup_PersonnelOrganizationFeaturesChange"
                                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbWorkGroup_PersonnelOrganizationFeaturesChange"
                                                                runat="server" ClientSideCommand="Refresh_cmbWorkGroup_PersonnelOrganizationFeaturesChange();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbWorkGroup_PersonnelOrganizationFeaturesChange"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 14%">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFromDate_WorkGroup_PersonnelOrganizationFeaturesChange"
                                            runat="server" CssClass="WhiteLabel" Text=": از تاریخ" meta:resourcekey="lblFromDate_WorkGroup_PersonnelOrganizationFeaturesChange"></asp:Label>
                                    </td>
                                    <td style="width: 36%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="Container_WorkGroupCalendars_PersonnelOrganizationFeaturesChange">
                                                    <table runat="server" id="Container_pdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange"
                                                        visible="false" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <pcal:PersianDatePickup ID="pdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange"
                                                                    runat="server" CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table runat="server" id="Container_gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange"
                                                        visible="false" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange">
                                                                    <tr>
                                                                        <td onmouseup="btn_gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange_OnMouseUp(event)">
                                                                            <ComponentArt:Calendar ID="gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange"
                                                                                runat="server" ControlType="Picker" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                PickerFormat="Custom" SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange_OnDateChange" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                        <td style="font-size: 10px;">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <img id="btn_gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange" alt="" class="calendar_button"
                                                                                onclick="btn_gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange_OnClick(event)"
                                                                                onmouseup="btn_gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange_OnMouseUp(event)"
                                                                                src="Images/Calendar/btn_calendar.gif" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <ComponentArt:Calendar ID="gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange"
                                                                    runat="server" AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                    CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                    DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                    ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                    NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                    PopUpExpandControlId="btn_gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange"
                                                                    PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                    SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                                    <ClientEvents>
                                                                        <SelectionChanged EventHandler="gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange_OnChange" />
                                                                        <Load EventHandler="gCalFromDate_WorkGroup_PersonnelOrganizationFeaturesChange_onLoad" />
                                                                    </ClientEvents>
                                                                </ComponentArt:Calendar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="top" style="width: 3%">
                                                    <ComponentArt:ToolBar ID="TlbClear_WorkGroupCalendars_PersonnelOrganizationFeaturesChange"
                                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_WorkGroupCalendars_PersonnelOrganizationFeaturesChange"
                                                                runat="server" ClientSideCommand="tlbItemClear_TlbClear_WorkGroupCalendars_PersonnelOrganizationFeaturesChange_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Clean.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_WorkGroupCalendars_PersonnelOrganizationFeaturesChange"
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
                        <td>
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Label ID="lblCalculationRange_PersonnelOrganizationFeaturesChange" runat="server"
                                            Text=": به محدوده محاسبات" CssClass="WhiteLabel" meta:resourcekey="lblCalculationRange_PersonnelOrganizationFeaturesChange"></asp:Label>
                                    </td>
                                    <td style="width: 35%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <ComponentArt:CallBack runat="server" ID="CallBack_cmbCalculationRange_PersonnelOrganizationFeaturesChange"
                                                        OnCallback="CallBack_cmbCalculationRange_PersonnelOrganizationFeaturesChange_onCallBack"
                                                        Height="26">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbCalculationRange_PersonnelOrganizationFeaturesChange"
                                                                runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                TextBoxCssClass="comboTextBox" Width="100%" ExpandDirection="Down" DataTextField="Name"
                                                                DataValueField="ID">
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbCalculationRange_PersonnelOrganizationFeaturesChange_onExpand" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_CalculationRange_PersonnelOrganizationFeaturesChange" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbCalculationRange_PersonnelOrganizationFeaturesChange_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbCalculationRange_PersonnelOrganizationFeaturesChange_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbCalculationRange_PersonnelOrganizationFeaturesChange_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                <td style="width: 5%">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_cmbCalculationRange_PersonnelOrganizationFeaturesChange"
                                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbCalculationRange_PersonnelOrganizationFeaturesChange"
                                                                runat="server" ClientSideCommand="Refresh_cmbCalculationRange_PersonnelOrganizationFeaturesChange();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbCalculationRange_PersonnelOrganizationFeaturesChange"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 14%">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFromDate_CalculationRange_PersonnelOrganizationFeaturesChange"
                                            runat="server" Text=": از تاریخ" CssClass="WhiteLabel" meta:resourcekey="lblFromDate_CalculationRange_PersonnelOrganizationFeaturesChange"></asp:Label>
                                    </td>
                                    <td style="width: 36%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="Container_CalculationRangeCalendars_PersonnelOrganizationFeaturesChange">
                                                    <table runat="server" id="Container_pdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange"
                                                        visible="false" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <pcal:PersianDatePickup ID="pdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange"
                                                                    runat="server" CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table runat="server" id="Container_gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange"
                                                        visible="false" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange">
                                                                    <tr>
                                                                        <td onmouseup="btn_gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange_OnMouseUp(event)">
                                                                            <ComponentArt:Calendar ID="gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange"
                                                                                runat="server" ControlType="Picker" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                PickerFormat="Custom" SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange_OnDateChange" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                        <td style="font-size: 10px;">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <img id="btn_gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange" alt=""
                                                                                class="calendar_button" onclick="btn_gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange_OnClick(event)"
                                                                                onmouseup="btn_gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange_OnMouseUp(event)"
                                                                                src="Images/Calendar/btn_calendar.gif" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <ComponentArt:Calendar ID="gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange"
                                                                    runat="server" AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                    CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                    DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                    ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                    NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                    PopUpExpandControlId="btn_gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange"
                                                                    PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                    SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                                    <ClientEvents>
                                                                        <SelectionChanged EventHandler="gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange_OnChange" />
                                                                        <Load EventHandler="gCalFromDate_CalculationRange_PersonnelOrganizationFeaturesChange_onLoad" />
                                                                    </ClientEvents>
                                                                </ComponentArt:Calendar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="top" style="width: 3%">
                                                    <ComponentArt:ToolBar ID="TlbClear_CalculationRangeCalendars_PersonnelOrganizationFeaturesChange"
                                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_CalculationRangeCalendars_PersonnelOrganizationFeaturesChange"
                                                                runat="server" ClientSideCommand="tlbItemClear_TlbClear_CalculationRangeCalendars_PersonnelOrganizationFeaturesChange_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Clean.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_CalculationRangeCalendars_PersonnelOrganizationFeaturesChange"
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
                        <td>
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Label ID="lblDepartment_PersonnelOrganizationFeaturesChange" runat="server"
                                            CssClass="WhiteLabel" Text=": به بخش" meta:resourcekey="lblDepartment_PersonnelOrganizationFeaturesChange"></asp:Label>
                                    </td>
                                    <td style="width: 35%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <ComponentArt:CallBack runat="server" ID="CallBack_cmbDepartment_PersonnelOrganizationFeaturesChange"
                                                        OnCallback="CallBack_cmbDepartment_PersonnelOrganizationFeaturesChange_onCallBack"
                                                        Height="26">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbDepartment_PersonnelOrganizationFeaturesChange" runat="server"
                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                DropDownCssClass="comboDropDown" DropDownHeight="160" DropDownResizingMode="Corner"
                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                                Width="100%" ExpandDirection="Down">
                                                                <DropDownContent>
                                                                    <ComponentArt:TreeView ID="trvDepartment_PersonnelOrganizationFeaturesChange" runat="server"
                                                                        CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                                        DefaultImageWidth="16" DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15"
                                                                        ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" Height="98%"
                                                                        HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20"
                                                                        LineImagesFolderUrl="Images/TreeView/LeftLines" LineImageWidth="19" NodeCssClass="TreeNode"
                                                                        NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode"
                                                                        ShowLines="true" Width="100%" meta:resourcekey="trvDepartment_PersonnelOrganizationFeaturesChange">
                                                                        <ClientEvents>
                                                                            <NodeSelect EventHandler="trvDepartment_PersonnelOrganizationFeaturesChange_onNodeSelect" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:TreeView>
                                                                </DropDownContent>
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbDepartment_PersonnelOrganizationFeaturesChange_onExpand" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_Department_PersonnelOrganizationFeaturesChange" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbDepartment_PersonnelOrganizationFeaturesChange_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbDepartment_PersonnelOrganizationFeaturesChange_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbDepartment_PersonnelOrganizationFeaturesChange_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                <td style="width: 5%">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_cmbDepartment_PersonnelOrganizationFeaturesChange"
                                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbDepartment_PersonnelOrganizationFeaturesChange"
                                                                runat="server" ClientSideCommand="Refresh_cmbDepartment_PersonnelOrganizationFeaturesChange();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbDepartment_PersonnelOrganizationFeaturesChange"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 20%">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEmployType_PersonnelOrganizationFeaturesChange"
                                            runat="server" CssClass="WhiteLabel" Text=": به نوع استخدام" meta:resourcekey="lblEmployType_PersonnelOrganizationFeaturesChange"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <ComponentArt:CallBack runat="server" ID="CallBack_cmbEmployType_PersonnelOrganizationFeaturesChange"
                                                        OnCallback="CallBack_cmbEmployType_PersonnelOrganizationFeaturesChange_onCallBack"
                                                        Height="26">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbEmployType_PersonnelOrganizationFeaturesChange" runat="server"
                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                DataTextField="Name" DataValueField="ID" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                                Width="100%" ExpandDirection="Down">
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbEmployType_PersonnelOrganizationFeaturesChange_onExpand" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_EmployType_PersonnelOrganizationFeaturesChange" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbEmployType_PersonnelOrganizationFeaturesChange_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbEmployType_PersonnelOrganizationFeaturesChange_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbEmployType_PersonnelOrganizationFeaturesChange_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                <td style="width: 5%">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_cmbEmployType_PersonnelOrganizationFeaturesChange"
                                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbEmployType_PersonnelOrganizationFeaturesChange"
                                                                runat="server" ClientSideCommand="Refresh_cmbEmployType_PersonnelOrganizationFeaturesChange();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbEmployType_PersonnelOrganizationFeaturesChange"
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
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td id="header_PersonnelProblems_PersonnelOrganizationFeaturesChange" class="HeaderLabel"
                                        style="width: 50%">
                                        Personnel with Problems
                                    </td>
                                    <td id="loadingPanel_GridPersonnelProblems_PersonnelOrganizationFeaturesChange" class="HeaderLabel"
                                        style="width: 45%">
                                    </td>
                                    <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbRefresh_GridPersonnelProblems_PersonnelOrganizationFeaturesChange"
                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridPersonnelProblems_PersonnelOrganizationFeaturesChange"
                                                    runat="server" ClientSideCommand="Refresh_GridPersonnelProblems_PersonnelOrganizationFeaturesChange();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridPersonnelProblems_PersonnelOrganizationFeaturesChange"
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
                            <ComponentArt:CallBack runat="server" ID="CallBack_GridPersonnelProblems_PersonnelOrganizationFeaturesChange"
                                OnCallback="CallBack_GridPersonnelProblems_PersonnelOrganizationFeaturesChange_onCallBack">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridPersonnelProblems_PersonnelOrganizationFeaturesChange"
                                        runat="server" CssClass="Grid" EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter"
                                        ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                        PageSize="10" RunningMode="Client" SearchTextCssClass="GridHeaderText" Width="100%"
                                        AllowMultipleSelect="false" ShowFooter="false" AllowColumnResizing="false" ScrollBar="On"
                                        ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16"
                                        ScrollImagesFolderUrl="images/Grid/scroller/" ScrollButtonWidth="16" ScrollButtonHeight="17"
                                        ScrollBarCssClass="ScrollBar" ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                        <Levels>
                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                SortImageWidth="9">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Code" DefaultSortDirection="Descending"
                                                        HeadingTextCssClass="HeadingText" meta:resourcekey="clmnCode_GridPersonnelProblems_PersonnelOrganizationFeaturesChange" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                        HeadingTextCssClass="HeadingText" meta:resourcekey="clmnName_GridPersonnelProblems_PersonnelOrganizationFeaturesChange" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                        DataCellClientTemplateId="clmnDescription_GridPersonnelProblems_PersonnelOrganizationFeaturesChange"
                                                        HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDescription_GridPersonnelProblems_PersonnelOrganizationFeaturesChange" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientTemplates>
                                            <ComponentArt:ClientTemplate ID="clmnDescription_GridPersonnelProblems_PersonnelOrganizationFeaturesChange">
                                                <table style="width: 100%; cursor: crosshair" ondblclick="GridPersonnelProblems_PersonnelOrganizationFeaturesChange_onCelldbClick();">
                                                    <tr>
                                                        <td align="center">
                                                            ##DataItem.GetMember('Description').Value##
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                        </ClientTemplates>
                                        <ClientEvents>
                                            <Load EventHandler="GridPersonnelProblems_PersonnelOrganizationFeaturesChange_onLoad" />
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_PersonnelProblems_PersonnelOrganizationFeaturesChange" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridPersonnelProblems_PersonnelOrganizationFeaturesChange_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridPersonnelProblems_PersonnelOrganizationFeaturesChange_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogRequestDescription"
        runat="server" Width="400px">
        <Content>
            <table id="tbl_DialogRequestDescription_PersonnelOrganizationFeaturesChange" runat="server"
                class="BoxStyle" style="width: 100%; font-family: Arial; font-size: small">
                <tr>
                    <td style="width: 98%">
                        &nbsp;
                    </td>
                    <td id="Td3" runat="server" meta:resourcekey="InverseAlignObj" style="width: 2%">
                        <ComponentArt:ToolBar ID="tlbExit_RequestDescription_PersonnelOrganizationFeaturesChange"
                            runat="server" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                            UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemExit_tlbExit_RequestDescription_PersonnelOrganizationFeaturesChange"
                                    runat="server" ClientSideCommand="tlbItemExit_tlbExit_RequestDescription_PersonnelOrganizationFeaturesChange_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="close-down.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_tlbExit_RequestDescription_PersonnelOrganizationFeaturesChange"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%">
                        <asp:Label ID="lblDescription_RequestDescription_PersonnelOrganizationFeaturesChange"
                            runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDescription_RequestDescription_PersonnelOrganizationFeaturesChange"
                            Text=": توضیحات درخواست"></asp:Label>
                    </td>
                    <td id="Td4" runat="server" meta:resourcekey="InverseAlignObj" style="width: 2%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="txtDescription_RequestDescription_PersonnelOrganizationFeaturesChange"
                            cols="5" name="S1" rows="12" style="width: 99%; height: 100px" class="TextBoxes"
                            readonly="readonly"></textarea>
                    </td>
                </tr>
            </table>
        </Content>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="350px">
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
                        <img id="Img1" runat="server" alt="" src="~/Images/Dialog/Waiting.gif"  />
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogWaiting_onShow" />
        </ClientEvents>
  </ComponentArt:Dialog>
    <asp:HiddenField runat="server" ID="hfheader_SearchByPersonnelBox_PersonnelOrganizationFeaturesChange"
        meta:resourcekey="hfheader_SearchByPersonnelBox_PersonnelOrganizationFeaturesChange" />
    <asp:HiddenField runat="server" ID="hfheader_PersonnelProblems_PersonnelOrganizationFeaturesChange"
        meta:resourcekey="hfheader_PersonnelProblems_PersonnelOrganizationFeaturesChange" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridPersonnelProblems_PersonnelOrganizationFeaturesChange"
        meta:resourcekey="hfloadingPanel_GridPersonnelProblems_PersonnelOrganizationFeaturesChange" />
    <asp:HiddenField runat="server" ID="hfErrorType_PersonnelOrganizationFeaturesChange"
        meta:resourcekey="hfErrorType_PersonnelOrganizationFeaturesChange" />
    <asp:HiddenField runat="server" ID="hfConnectionError_PersonnelOrganizationFeaturesChange"
        meta:resourcekey="hfConnectionError_PersonnelOrganizationFeaturesChange" />
    <asp:HiddenField runat="server" ID="hfOperation_PersonnelOrganizationFeaturesChange"
        meta:resourcekey="hfOperation_PersonnelOrganizationFeaturesChange" />
    <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_PersonnelOrganizationFeaturesChange"
        meta:resourcekey="hfclmnName_cmbPersonnel_PersonnelOrganizationFeaturesChange" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_PersonnelOrganizationFeaturesChange"
        meta:resourcekey="hfclmnBarCode_cmbPersonnel_PersonnelOrganizationFeaturesChange" />
    <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_PersonnelOrganizationFeaturesChange"
        meta:resourcekey="hfclmnCardNum_cmbPersonnel_PersonnelOrganizationFeaturesChange" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_PersonnelOrganizationFeaturesChange"
        meta:resourcekey="hfCloseMessage_PersonnelOrganizationFeaturesChange" />
    <asp:HiddenField runat="server" ID="hfheaderMasterPersonnelCount_PersonnelOrganizationFeaturesChange"
        meta:resourcekey="hfheaderMasterPersonnelCount_PersonnelOrganizationFeaturesChange" />
    <asp:HiddenField runat="server" ID="hfCurrentDate_PersonnelOrganizationFeaturesChange" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_PersonnelOrganizationFeaturesChange" />
    </form>
</body>
</html>
