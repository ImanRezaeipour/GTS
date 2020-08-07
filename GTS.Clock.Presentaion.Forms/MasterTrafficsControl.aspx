<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterTrafficsControl.aspx.cs"
    Inherits="MasterTrafficsControl" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
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
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body onkeydown="MasterTrafficsControl_onKeyDown(event)">
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="MasterTrafficsControlForm" runat="server" meta:resourcekey="MasterTrafficsControlForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table id="Mastertbl_MasterTrafficsControl" style="width: 100%; font-family: Arial;
        font-size: small" class="BodyStyle">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbMasterTrafficsControl" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbMasterTrafficsControl" runat="server"
                                        ClientSideCommand="tlbItemNew_TlbMasterTrafficsControl_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemNew_TlbMasterTrafficsControl" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbMasterTrafficsControl" runat="server"
                                        ClientSideCommand="tlbItemDelete_TlbMasterTrafficsControl_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemDelete_TlbMasterTrafficsControl"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbMasterTrafficsControl" runat="server"
                                        ClientSideCommand="tlbItemHelp_TlbMasterTrafficsControl_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbMasterTrafficsControl" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCalculation_TlbTlbMasterTrafficsControl" runat="server"
                                        ClientSideCommand="tlbItemCalculation_TlbTlbMasterTrafficsControl_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="calc.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCalculation_TlbTlbMasterTrafficsControl"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemTrafficTransfer_TlbTlbMasterTrafficsControl"
                                        runat="server" ClientSideCommand="tlbItemTrafficTransfer_TlbTlbMasterTrafficsControl_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Transfer.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemTrafficTransfer_TlbTlbMasterTrafficsControl"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbMasterTrafficsControl"
                                        runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbMasterTrafficsControl_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbMasterTrafficsControl"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbMasterTrafficsControl" runat="server"
                                        ClientSideCommand="tlbItemExit_TlbMasterTrafficsControl_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbMasterTrafficsControl" TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_MasterTrafficsControl" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 75%">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <table style="width: 80%;" class="BoxStyle">
                                                        <tr>
                                                            <td style="width: 90%">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblPersonnel_MasterTrafficsControl" runat="server" CssClass="WhiteLabel"
                                                                                meta:resourcekey="lblPersonnel_MasterTrafficsControl" Text=": پرسنل"></asp:Label>
                                                                        </td>
                                                                        <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj">
                                                                            <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_MasterTrafficsControl" runat="server"
                                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                Style="direction: ltr;" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_MasterTrafficsControl"
                                                                                        runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_MasterTrafficsControl_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_MasterTrafficsControl"
                                                                                        TextImageSpacing="5" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_MasterTrafficsControl"
                                                                                        runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_MasterTrafficsControl_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="first.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_MasterTrafficsControl"
                                                                                        TextImageSpacing="5" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_MasterTrafficsControl"
                                                                                        runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_MasterTrafficsControl_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Before.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_MasterTrafficsControl"
                                                                                        TextImageSpacing="5" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_MasterTrafficsControl"
                                                                                        runat="server" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_MasterTrafficsControl_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Next.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_MasterTrafficsControl"
                                                                                        TextImageSpacing="5" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_MasterTrafficsControl"
                                                                                        runat="server" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_MasterTrafficsControl_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="last.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_MasterTrafficsControl"
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
                                                                <ComponentArt:CallBack ID="CallBack_cmbPersonnel_MasterTrafficsControl" runat="server"
                                                                    Height="26" OnCallback="CallBack_cmbPersonnel_MasterTrafficsControl_onCallBack">
                                                                    <Content>
                                                                        <ComponentArt:ComboBox ID="cmbPersonnel_MasterTrafficsControl" runat="server" AutoComplete="true"
                                                                            AutoHighlight="false" CssClass="comboBox" DataFields="BarCode" DataTextField="Name"
                                                                            DropDownCssClass="comboDropDown" DropDownHeight="210" DropDownPageSize="7" DropDownWidth="390"
                                                                            DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                            FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_MasterTrafficsControl"
                                                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client" TextBoxEnabled="true"
                                                                            SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                            <ClientTemplates>
                                                                                <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_MasterTrafficsControl">
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
                                                                                <table border="0" cellpadding="0" cellspacing="0" width="390">
                                                                                    <tr class="headingRow">
                                                                                        <td id="clmnName_cmbPersonnel_MasterTrafficsControl" class="headingCell" style="width: 40%;
                                                                                            text-align: center">
                                                                                            Name And Family
                                                                                        </td>
                                                                                        <td id="clmnBarCode_cmbPersonnel_MasterTrafficsControl" class="headingCell" style="width: 30%;
                                                                                            text-align: center">
                                                                                            BarCode
                                                                                        </td>
                                                                                        <td id="clmnCardNum_cmbPersonnel_MasterTrafficsControl" class="headingCell" style="width: 30%;
                                                                                            text-align: center">
                                                                                            CardNum
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </DropDownHeader>
                                                                            <ClientEvents>
                                                                                <Expand EventHandler="cmbPersonnel_MasterTrafficsControl_onExpand" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:ComboBox>
                                                                        <asp:HiddenField ID="ErrorHiddenField_Personnel_MasterTrafficsControl" runat="server" />
                                                                        <asp:HiddenField ID="hfPersonnelCount_MasterTrafficsControl" runat="server" />
                                                                        <asp:HiddenField ID="hfPersonnelPageCount_MasterTrafficsControl" runat="server" />
                                                                    </Content>
                                                                    <ClientEvents>
                                                                        <BeforeCallback EventHandler="CallBack_cmbPersonnel_MasterTrafficsControl_onBeforeCallback" />
                                                                        <CallbackComplete EventHandler="CallBack_cmbPersonnel_MasterTrafficsControl_onCallBackComplete" />
                                                                        <CallbackError EventHandler="CallBack_cmbPersonnel_MasterTrafficsControl_onCallbackError" />
                                                                    </ClientEvents>
                                                                </ComponentArt:CallBack>
                                                            </td>
                                                            <td style="width: 10%">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 90%">
                                                                <input id="txtPersonnelSearch_MasterTrafficsControl" runat="server" class="TextBoxes"
                                                                    onclick="this.select();" onfocus="this.select();" style="width: 95%" type="text" />
                                                            </td>
                                                            <td style="width: 10%">
                                                                <ComponentArt:ToolBar ID="TlbSearchPersonnel_MasterTrafficsControl" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_MasterTrafficsControl"
                                                                            runat="server" ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_MasterTrafficsControl_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_MasterTrafficsControl"
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
                                                                <ComponentArt:ToolBar ID="TlbAdvancedSearch_MasterTrafficsControl" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_MasterTrafficsControl"
                                                                            runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_MasterTrafficsControl_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_MasterTrafficsControl"
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
                        <td style="width: 25%">
                            <table style="width: 30%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblYear_MasterTrafficsControl" runat="server" Text=": سال" CssClass="WhiteLabel"
                                            meta:resourcekey="lblYear_MasterTrafficsControl"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMonth_MasterTrafficsControl" runat="server" Text=": ماه" CssClass="WhiteLabel"
                                            meta:resourcekey="lblMonth_MasterTrafficsControl"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:ComboBox ID="cmbYear_MasterTrafficsControl" runat="server" AutoComplete="true"
                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                            TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100">
                                            <ClientEvents>
                                                <Change EventHandler="cmbYear_MasterTrafficsControl_onChange" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                    </td>
                                    <td>
                                        <ComponentArt:ComboBox ID="cmbMonth_MasterTrafficsControl" runat="server" AutoComplete="true"
                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                            TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100" DropDownHeight="280">
                                            <ClientEvents>
                                                <Change EventHandler="cmbMonth_MasterTrafficsControl_onChange" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                    </td>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbView_MasterTrafficsControl" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemView_TlbView_MasterTrafficsControl" runat="server"
                                                    ClientSideCommand="tlbItemView_TlbView_MasterTrafficsControl_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemView_TlbView_MasterTrafficsControl"
                                                    TextImageSpacing="5" Enabled="true" />
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
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 73%" valign="top">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_Traffics_MasterTrafficsControl" class="HeaderLabel" style="width: 50%">
                                                    Traffics
                                                </td>
                                                <td id="loadingPanel_GridTraffics_MasterTrafficsControl" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridTraffics_MasterTrafficsControl" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridTraffics_MasterTrafficsControl"
                                                                runat="server" ClientSideCommand="Refresh_GridTraffics_MasterTrafficsControl();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridTraffics_MasterTrafficsControl"
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
                                        <iframe id="Traffics_iFrame" class="Traffics_iFrame" src="TrafficsControl.aspx">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 40%">
                                                    <asp:Label ID="lblCalculation_MasterTrafficsControl" runat="server" Text=": محاسبات"
                                                        meta:resourcekey="lblCalculation_MasterTrafficsControl" class="WhiteLabel"></asp:Label>
                                                </td>
                                                <td id="tdCalculationing_MasterTrafficsControl" class="HeaderLabel">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="overflow: auto; width: 300px; height: 230px">
                                            <ComponentArt:CallBack runat="server" ID="CallBack_bulletedListCalculation_MasterTrafficsControl"
                                                OnCallback="CallBack_bulletedListCalculation_MasterTrafficsControl_onCallBack">
                                                <Content>
                                                    <asp:BulletedList runat="server" ID="bulletedListCalculation_MasterTrafficsControl">
                                                    </asp:BulletedList>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_CalculationResult" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_bulletedListCalculation_MasterTrafficsControl_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_bulletedListCalculation_MasterTrafficsControl_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
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
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogTrafficsControl"
        runat="server" Width="500px">
        <Content>
            <table id="Mastertbl_DialogTrafficsControl_MasterTrafficsControl" style="width: 100%;"
                class="BodyStyle">
                <tr>
                    <td>
                        <ComponentArt:ToolBar ID="TlbTrafficsControl_DialogTrafficsControl_MasterTrafficsControl"
                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                            UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbTrafficsControl_DialogTrafficsControl_MasterTrafficsControl"
                                    runat="server" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                    ImageUrl="save.png" ClientSideCommand="tlbItemSave_TlbTrafficsControl_DialogTrafficsControl_MasterTrafficsControl_onClick();"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbTrafficsControl_DialogTrafficsControl_MasterTrafficsControl"
                                    TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbTrafficsControl_DialogTrafficsControl_MasterTrafficsControl"
                                    runat="server" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                    ImageUrl="cancel.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbTrafficsControl_DialogTrafficsControl_MasterTrafficsControl"
                                    TextImageSpacing="5" ClientSideCommand="tlbItemCancel_TlbTrafficsControl_DialogTrafficsControl_MasterTrafficsControl_onClick();" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Label ID="lblTrafficType_MasterTrafficsControl" runat="server" Text=": نوع تردد"
                                        meta:resourcekey="lblTrafficType_MasterTrafficsControl" class="WhiteLabel"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ComponentArt:CallBack runat="server" ID="CallBack_cmbPrecards_MasterTrafficsControl"
                                        OnCallback="CallBack_cmbPrecards_MasterTrafficsControl_onCallBack" Height="26">
                                        <Content>
                                            <ComponentArt:ComboBox ID="cmbPrecards_MasterTrafficsControl" runat="server" AutoComplete="true"
                                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                ExpandDirection="Down" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                                                HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                                SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox"
                                                DataTextField="Name" DataValueField="ID" TextBoxEnabled="true">
                                                <ClientEvents>
                                                    <Expand EventHandler="cmbPrecards_MasterTrafficsControl_onExpand" />
                                                    <Collapse EventHandler="cmbPrecards_MasterTrafficsControl_onCollapse" />
                                                </ClientEvents>
                                            </ComponentArt:ComboBox>
                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_TrafficType" />
                                        </Content>
                                        <ClientEvents>
                                            <BeforeCallback EventHandler="CallBack_cmbPrecards_MasterTrafficsControl_onBeforeCallback" />
                                            <CallbackComplete EventHandler="CallBack_cmbPrecards_MasterTrafficsControl_onCallbackComplete" />
                                            <CallbackError EventHandler="CallBack_cmbPrecards_MasterTrafficsControl_onCallbackError" />
                                        </ClientEvents>
                                    </ComponentArt:CallBack>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTime_MasterTrafficsControl" runat="server" Text=": زمان" class="WhiteLabel"
                                        meta:resourcekey="lblTime_MasterTrafficsControl"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDate_MasterTrafficsControl" runat="server" Text=": تاریخ" class="WhiteLabel"
                                        meta:resourcekey="lblDate_MasterTrafficsControl"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <MKB:TimeSelector ID="TimeSelector_Time_MasterTrafficsControl" runat="server" DisplaySeconds="true"
                                        MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                    </MKB:TimeSelector>
                                </td>
                                <td id="Container_DateCalendars_MasterTrafficsControl">
                                    <table runat="server" id="Container_pdpDate_MasterTrafficsControl" visible="false"
                                        style="width: 100%">
                                        <tr>
                                            <td>
                                                <pcal:PersianDatePickup ID="pdpDate_MasterTrafficsControl" runat="server" CssClass="PersianDatePicker"
                                                    ReadOnly="true"></pcal:PersianDatePickup>
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="Container_gdpDate_MasterTrafficsControl" visible="false"
                                        style="width: 100%">
                                        <tr>
                                            <td>
                                                <table id="Container_gCalDate_MasterTrafficsControl" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td onmouseup="btn_gdpDate_MasterTrafficsControl_OnMouseUp(event)">
                                                            <ComponentArt:Calendar ID="gdpDate_MasterTrafficsControl" runat="server" ControlType="Picker"
                                                                MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                SelectedDate="2008-1-1">
                                                                <ClientEvents>
                                                                    <SelectionChanged EventHandler="gdpDate_MasterTrafficsControl_OnDateChange" />
                                                                </ClientEvents>
                                                            </ComponentArt:Calendar>
                                                        </td>
                                                        <td style="font-size: 10px;">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <img id="btn_gdpDate_MasterTrafficsControl" alt="" class="calendar_button" onclick="btn_gdpDate_MasterTrafficsControl_OnClick(event)"
                                                                onmouseup="btn_gdpDate_MasterTrafficsControl_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <ComponentArt:Calendar ID="gCalDate_MasterTrafficsControl" runat="server" AllowMonthSelection="false"
                                                    AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                    CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                    DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                    MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                    OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpDate_MasterTrafficsControl"
                                                    PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                    SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                    <ClientEvents>
                                                        <SelectionChanged EventHandler="gCalDate_MasterTrafficsControl_OnChange" />
                                                        <Load EventHandler="gCalDate_MasterTrafficsControl_OnLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:Calendar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDescription_MasterTrafficsControl" runat="server" class="WhiteLabel"
                                        Text=": توضیحات" meta:resourcekey="lblDescription_MasterTrafficsControl"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <textarea id="txtDescription_MasterTrafficsControl" cols="20" name="S1" rows="2"
                                        style="width: 100%; height: 50px" onfocus="this.select();" onclick="this.select();"></textarea>
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
    <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_MasterTrafficsControl"
        meta:resourcekey="hfclmnName_cmbPersonnel_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_MasterTrafficsControl"
        meta:resourcekey="hfclmnBarCode_cmbPersonnel_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_MasterTrafficsControl"
        meta:resourcekey="hfclmnCardNum_cmbPersonnel_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogMasterTrafficsControl" meta:resourcekey="hfTitle_DialogMasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfheader_Traffics_MasterTrafficsControl" meta:resourcekey="hfheader_Traffics_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridTraffics_MasterTrafficsControl"
        meta:resourcekey="hfloadingPanel_GridTraffics_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_MasterTrafficsControl" meta:resourcekey="hfDeleteMessage_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_MasterTrafficsControl" meta:resourcekey="hfCloseMessage_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfView_MasterTrafficsControl" meta:resourcekey="hfView_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfAdd_MasterTrafficsControl" meta:resourcekey="hfAdd_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfDelete_MasterTrafficsControl" meta:resourcekey="hfDelete_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfErrorType_MasterTrafficsControl" meta:resourcekey="hfErrorType_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfConnectionError_MasterTrafficsControl" meta:resourcekey="hfConnectionError_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfCurrentDate_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfCurrentYear_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfCurrentMonth_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_cmbPrecards_MasterTrafficsControl"
        meta:resourcekey="hfcmbAlarm_cmbPrecards_MasterTrafficsControl" />
    <asp:HiddenField runat="server" ID="hfCalculationing_MasterTrafficsControl" meta:resourcekey="hfCalculationing_MasterTrafficsControl" />
    </form>
</body>
</html>
