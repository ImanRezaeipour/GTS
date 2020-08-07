<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterLeaveRemains.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.MasterLeaveRemains" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="FUA" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
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
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/numericUpDown.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="MasterLeaveRemainsForm" runat="server" meta:resourcekey="MasterLeaveRemainsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table id="Mastertbl_MasterLeaveRemains" style="width: 100%; font-family: Arial; font-size: small">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbMasterLeaveRemains" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbMasterLeaveRemains" runat="server" ClientSideCommand="tlbItemNew_TlbMasterLeaveRemains_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbMasterLeaveRemains"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbMasterLeaveRemains" runat="server" ClientSideCommand="tlbItemEdit_TlbMasterLeaveRemains_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbMasterLeaveRemains"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemLeaveRemainsTransfer_TlbMasterLeaveRemains"
                                        runat="server" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                        ImageUrl="LeaveTransfer.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLeaveRemainsTransfer_TlbMasterLeaveRemains"
                                        ClientSideCommand="tlbItemLeaveRemainsTransfer_TlbMasterLeaveRemains_onClick();"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemLeaveReserve_TlbMasterLeaveRemains" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="LeaveReserve.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLeaveReserve_TlbMasterLeaveRemains"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemLeaveReserve_TlbMasterLeaveRemains_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbMasterLeaveRemains" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbMasterLeaveRemains" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbMasterLeaveRemains_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbMasterLeaveRemains" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbMasterLeaveRemains_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbMasterLeaveRemains"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbMasterLeaveRemains" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbMasterLeaveRemains" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemExit_TlbMasterLeaveRemains_onClick();" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_MasterLeaveRemains" class="ToolbarMode">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="width: 90%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPersonnel_PersonnelSearch_MasterLeaveRemains" runat="server" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblPersonnel_PersonnelSearch_MasterLeaveRemains" Text=": پرسنل"></asp:Label>
                                                </td>
                                                <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_MasterLeaveRemains" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                        Style="direction: ltr" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_MasterLeaveRemains"
                                                                runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_MasterLeaveRemains_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_MasterLeaveRemains"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_MasterLeaveRemains"
                                                                runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_MasterLeaveRemains_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="first.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_MasterLeaveRemains"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_MasterLeaveRemains"
                                                                runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_MasterLeaveRemains_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Before.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_MasterLeaveRemains"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_MasterLeaveRemains"
                                                                runat="server" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_MasterLeaveRemains_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Next.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_MasterLeaveRemains"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_MasterLeaveRemains"
                                                                runat="server" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_MasterLeaveRemains_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="last.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_MasterLeaveRemains"
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
                                        <ComponentArt:CallBack ID="CallBack_cmbPersonnel_MasterLeaveRemains" runat="server"
                                            OnCallback="CallBack_cmbPersonnel_MasterLeaveRemains_onCallBack" Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbPersonnel_MasterLeaveRemains" runat="server" AutoComplete="true"
                                                    AutoHighlight="false" CssClass="comboBox" DataFields="BarCode,CardNum" DataTextField="FirstNameAndLastName"
                                                    DropDownCssClass="comboDropDown" DropDownHeight="210" DropDownPageSize="7" DropDownWidth="390"
                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_MasterLeaveRemains"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" meta:resourcekey="cmbPersonnel_MasterLeaveRemains"
                                                    RunningMode="Client" SelectedItemCssClass="comboItemHover" Style="width: 100%"
                                                    TextBoxCssClass="comboTextBox" TextBoxEnabled="true">
                                                    <ClientTemplates>
                                                        <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_MasterLeaveRemains">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="390">
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
                                                                <td id="clmnName_cmbPersonnel_MasterLeaveRemains" class="headingCell" style="width: 40%;
                                                                    text-align: center">
                                                                    Name And Family
                                                                </td>
                                                                <td id="clmnBarCode_cmbPersonnel_MasterLeaveRemains" class="headingCell" style="width: 30%;
                                                                    text-align: center">
                                                                    BarCode
                                                                </td>
                                                                <td id="clmnCardNum_cmbPersonnel_MasterLeaveRemains" class="headingCell" style="width: 30%;
                                                                    text-align: center">
                                                                    CardNum
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </DropDownHeader>
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbPersonnel_MasterLeaveRemains_onExpand" />
                                                        <Collapse EventHandler="cmbPersonnel_MasterLeaveRemains_onCollapse" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField ID="ErrorHiddenField_Personnel_MasterLeaveRemains" runat="server" />
                                                <asp:HiddenField ID="hfPersonnelCount_MasterLeaveRemains" runat="server" />
                                                <asp:HiddenField ID="hfPersonnelPageCount_MasterLeaveRemains" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbPersonnel_MasterLeaveRemains_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbPersonnel_MasterLeaveRemains_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbPersonnel_MasterLeaveRemains_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td style="width: 10%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 90%">
                                        <input id="txtPersonnelSearch_MasterLeaveRemains" runat="server" class="TextBoxes"
                                            onclick="this.select();" onfocus="this.select();" style="width: 95%" type="text" />
                                    </td>
                                    <td style="width: 10%">
                                        <ComponentArt:ToolBar ID="TlbSearchPersonnel_MasterLeaveRemains" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_MasterLeaveRemains"
                                                    runat="server" ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_MasterLeaveRemains_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_MasterLeaveRemains"
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
                                        <ComponentArt:ToolBar ID="TlbAdvancedSearch_MasterLeaveRemains" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_MasterLeaveRemains"
                                                    runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_MasterLeaveRemains_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_MasterLeaveRemains"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="bottom">
                            <table style="width: 95%;">
                                <tr>
                                    <td style="width: 33%">
                                        <asp:Label ID="lblFromYear_MasterLeaveRemains" runat="server" Text=": از سال" class="WhiteLabel"
                                            meta:resourcekey="lblFromYear_MasterLeaveRemains"></asp:Label>
                                    </td>
                                    <td style="width: 33%">
                                        <asp:Label ID="lblToYear_MasterLeaveRemains" runat="server" Text=": تا سال" class="WhiteLabel"
                                            meta:resourcekey="lblToYear_MasterLeaveRemains"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:ComboBox ID="cmbFromYear_MasterMonthlyOperation" runat="server" AutoComplete="true"
                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                            TextBoxCssClass="comboTextBox" Style="width: 98%;" DropDownHeight="280">
                                            <ClientEvents>
                                                <Change EventHandler="cmbFromYear_MasterMonthlyOperation_onChange" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                    </td>
                                    <td>
                                        <ComponentArt:ComboBox ID="cmbToYear_MasterMonthlyOperation" runat="server" AutoComplete="true"
                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                            TextBoxCssClass="comboTextBox" Style="width: 98%;" DropDownHeight="280">
                                            <ClientEvents>
                                                <Change EventHandler="cmbToYear_MasterMonthlyOperation_onChange" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                    </td>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbView_MasterLeaveRemains" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemView_TlbView_MasterLeaveRemains" runat="server"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="NormalView.png"
                                                    ClientSideCommand="tlbItemView_TlbView_MasterLeaveRemains_onClick();" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemView_TlbView_MasterLeaveRemains"
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
            <td style="height: 60%" valign="top">
                <table style="width: 98%;" class="BoxStyle">
                    <tr>
                        <td style="width: 100%">
                            <table style="width: 100%;">
                                <tr>
                                    <td id="header_LeaveRemains_LeaveRemains" style="width: 66%" class="HeaderLabel">
                                        Leave Remains
                                    </td>
                                    <td id="loadingPanel_GridMasterLeaveRemains_MasterLeaveRemains" class="HeaderLabel"
                                        style="width: 35%">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <ComponentArt:CallBack runat="server" ID="CallBack_GridMasterLeaveRemains_MasterLeaveRemains"
                                OnCallback="CallBack_GridMasterLeaveRemains_MasterLeaveRemains_onCallback">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridMasterLeaveRemains_MasterLeaveRemains" runat="server"
                                        AllowColumnResizing="false" AllowHorizontalScrolling="true" AllowMultipleSelect="false"
                                        CssClass="Grid" EnableViewState="true" FillContainer="true" FooterCssClass="GridFooter"
                                        ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                        PageSize="7" RunningMode="Client" ScrollBar="Off" ScrollBarCssClass="ScrollBar"
                                        ScrollBarWidth="16" ScrollButtonHeight="17" ScrollButtonWidth="16" ScrollGripCssClass="ScrollGrip"
                                        ScrollImagesFolderUrl="images/Grid/scroller/" ScrollTopBottomImageHeight="2"
                                        ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageWidth="16" SearchTextCssClass="GridHeaderText"
                                        ShowFooter="false">
                                        <Levels>
                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                                                SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="Person.ID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Person.PersonCode" DefaultSortDirection="Descending"
                                                        HeadingText="شماره پرسنلی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPersonnelID_GridMasterLeaveRemains_MasterLeaveRemains" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Person.Name" DefaultSortDirection="Descending"
                                                        HeadingText="نام و نام خانوادگی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnName_GridMasterLeaveRemains_MasterLeaveRemains" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Year" DefaultSortDirection="Descending"
                                                        HeadingText="سال" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnYear_GridMasterLeaveRemains_MasterLeaveRemains" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="ConfirmedDay" DefaultSortDirection="Descending"
                                                        HeadingText="روز تائید شده" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnConfirmedDay_GridMasterLeaveRemains_MasterLeaveRemains" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="ConfirmedHour" DefaultSortDirection="Descending"
                                                        HeadingText="ساعت تائید شده" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnConfirmedHour_GridMasterLeaveRemains_MasterLeaveRemains" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RealDay" DefaultSortDirection="Descending"
                                                        HeadingText="روز واقعی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRealDay_GridMasterLeaveRemains_MasterLeaveRemains" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="RealHour" DefaultSortDirection="Descending"
                                                        HeadingText="ساعت واقعی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRealHour_GridMasterLeaveRemains_MasterLeaveRemains" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientEvents>
                                            <Load EventHandler="GridMasterLeaveRemains_MasterLeaveRemains_onLoad" />
                                            <ItemSelect EventHandler="GridMasterLeaveRemains_MasterLeaveRemains_onItemSelect" />
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField runat="server" ID="hfLeaveRemainsPageCount_MasterLeaveRemains" />
                                    <asp:HiddenField runat="server" ID="hfLeaveRemainsCount_LeaveRemains" />
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_MasterLeaveRemains" />
                                </Content>
                                <ClientEvents>
                                    <CallbackError EventHandler="CallBack_GridMasterLeaveRemains_MasterLeaveRemains_onCallbackError" />
                                    <CallbackComplete EventHandler="CallBack_GridMasterLeaveRemains_MasterLeaveRemains_onCallbackComplete" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <table style="width: 100%;">
                                <tr>
                                    <td id="Td5" runat="server" meta:resourcekey="AlignObj">
                                        <ComponentArt:ToolBar ID="TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            Style="direction: ltr" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains"
                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains"
                                                    runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains"
                                                    runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains"
                                                    runat="server" ClientSideCommand="tlbItemNext_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains"
                                                    runat="server" ClientSideCommand="tlbItemLast_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridMasterLeaveRemains_MasterLeaveRemains"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="footer_GridMasterLeaveRemains_MasterLeaveRemains" class="WhiteLabel" style="width: 25%">
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
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogLeaveRemains"
        runat="server" Width="500px">
        <Content>
            <table style="width: 100%;" class="BodyStyle">
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 20%">
                                    <ComponentArt:ToolBar ID="TlbLeaveRemains" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                        DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                        DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                        DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemSave_TlbLeaveRemains" runat="server" ClientSideCommand="tlbItemSave_TlbLeaveRemains_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbLeaveRemains"
                                                TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbLeaveRemains" runat="server" ClientSideCommand="tlbItemCancel_TlbLeaveRemains_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png"
                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbLeaveRemains"
                                                TextImageSpacing="5" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 10%">
                                                <asp:Label ID="lblYear_MasterLeaveRemains" runat="server" class="WhiteLabel" Text=": سال"
                                                    meta:resourcekey="lblYear_MasterLeaveRemains"></asp:Label>
                                            </td>
                                            <td>
                                                <input type="text" id="txtYear_MasterLeaveRemains" class="TextBoxes" style="width: 20%;
                                                    text-align: center" onchange="DayBox_MasterLeaveRemains_onChange" />
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
                                        <tr align="center">
                                            <td id="Td1" runat="server" meta:resourcekey="AlignObj" style="width: 20%">
                                                <asp:Label ID="lblRealRemains_MasterLeaveRemains" runat="server" meta:resourcekey="lblRealRemains_MasterLeaveRemains"
                                                    Text=": مانده واقعی" class="WhiteLabel"></asp:Label>
                                            </td>
                                            <td style="width: 30%">
                                                <input type="text" runat="server" style="width: 50%; text-align: center" class="TextBoxes"
                                                    id="txtRealRemainsDay_MasterLeaveRemains" disabled="disabled" />
                                            </td>
                                            <td style="width: 10%">
                                                <asp:Label ID="lblRealRemainsDay_MasterLeaveRemains" runat="server" meta:resourcekey="lblRealRemainsDay_MasterLeaveRemains"
                                                    Text="روز و" class="WhiteLabel"></asp:Label>
                                            </td>
                                            <td style="width: 30%">
                                                <input type="text" runat="server" style="width: 50%; text-align: center" class="TextBoxes"
                                                    id="txtRealRemainsHour_MasterLeaveRemains" disabled="disabled" />
                                            </td>
                                            <td style="width: 10%">
                                                <asp:Label ID="lblRealRemainsHour_MasterLeaveRemains" runat="server" meta:resourcekey="lblRealRemainsHour_MasterLeaveRemains"
                                                    Text="ساعت" class="WhiteLabel"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td id="Td3" runat="server" meta:resourcekey="AlignObj">
                                                <asp:Label ID="lblConfirmedRemains_MasterLeaveRemains" runat="server" meta:resourcekey="lblConfirmedRemains_MasterLeaveRemains"
                                                    Text=": مانده تایید شده " class="WhiteLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <input type="text" runat="server" style="width: 50%; text-align: center" class="TextBoxes"
                                                    id="txtConfirmedRemainsDay_MasterLeaveRemains" onchange="DayBox_MasterLeaveRemains_onChange('txtConfirmedRemainsDay_MasterLeaveRemains')" />
                                            </td>
                                            <td style="width: 5%">
                                                <asp:Label ID="lblConfirmedRemainsDay_MasterLeaveRemains" runat="server" meta:resourcekey="lblConfirmedRemainsDay_MasterLeaveRemains"
                                                    Text="روز و" class="WhiteLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <MKB:TimeSelector ID="TimeSelector_ConfirmedRemainsHour_MasterLeaveRemains" runat="server"
                                                    DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                                </MKB:TimeSelector>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblConfirmedRemainsHour_MasterLeaveRemains" runat="server" meta:resourcekey="lblConfirmedRemainsHour_MasterLeaveRemains"
                                                    Text="ساعت" class="WhiteLabel"></asp:Label>
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
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogLeaveRemainsTransfer"
        runat="server" Width="300px">
        <Content>
            <table style="width: 100%;" class="BodyStyle">
                <tr>
                    <td>
                        <ComponentArt:ToolBar ID="TlbLeaveRemainsTransfer_MasterLeaveRemains" runat="server"
                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                            UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbLeaveRemainsTransfer_MasterLeaveRemains"
                                    runat="server" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                    ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbLeaveRemainsTransfer_MasterLeaveRemains"
                                    ClientSideCommand="tlbItemSave_TlbLeaveRemainsTransfer_MasterLeaveRemains_onClick();"
                                    TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbLeaveRemainsTransfer_MasterLeaveRemains"
                                    runat="server" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                    ImageUrl="cancel.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbLeaveRemainsTransfer_MasterLeaveRemains"
                                    TextImageSpacing="5" ClientSideCommand="tlbItemCancel_TlbLeaveRemainsTransfer_MasterLeaveRemains_onClick();" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;" class="BoxStyle">
                            <tr>
                                <td>
                                    <asp:Label ID="lblLeaveRemainsTransfer_MasterLeaveRemains" runat="server" CssClass="HeaderLabel"
                                        Text=": انتقال مانده مرخصی" meta:resourcekey="lblLeaveRemainsTransfer_MasterLeaveRemains"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 25%">
                                                <asp:Label ID="lblTransferFromYear_MasterLeaveRemains" runat="server" Text=": از سال"
                                                    CssClass="WhiteLabel" meta:resourcekey="lblTransferFromYear_MasterLeaveRemains"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <ComponentArt:ComboBox ID="cmbTransferFromYear_MasterLeaveRemains" runat="server"
                                                    AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                    DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                    TextBoxCssClass="comboTextBox" Style="width: 70px;" DropDownHeight="50">
                                                </ComponentArt:ComboBox>
                                            </td>
                                            <td style="width: 25%">
                                                &nbsp;&nbsp;<asp:Label ID="lblTransferToYear_MasterLeaveRemains" runat="server" Text=": به سال"
                                                    CssClass="WhiteLabel" meta:resourcekey="lblTransferToYear_MasterLeaveRemains"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <ComponentArt:ComboBox ID="cmbTransferToYear_MasterLeaveRemains" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                    TextBoxCssClass="comboTextBox" Style="width: 70px;" DropDownHeight="50">
                                                </ComponentArt:ComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 80%;">
                                        <tr>
                                            <td style="width: 50%">
                                                <asp:Label ID="lblPersonnelCount_MasterLeaveRemains" runat="server" Text=": تعداد پرسنل"
                                                    meta:resourcekey="lblPersonnelCount_MasterLeaveRemains"></asp:Label>
                                            </td>
                                            <td>
                                                <input id="txtPersonnelCount_MasterLeaveRemains" class="TextBoxes" style="width: 42%;
                                                    text-align: center" type="text" readonly="readonly"/>
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
        runat="server" Width="320px">
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
    <asp:HiddenField runat="server" ID="hfheader_LeaveRemains_LeaveRemains" meta:resourcekey="hfheader_LeaveRemains_LeaveRemains" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_MasterLeaveRemains" meta:resourcekey="hfCloseMessage_MasterLeaveRemains" />
    <asp:HiddenField runat="server" ID="hfErrorType_MasterLeaveRemains" meta:resourcekey="hfErrorType_MasterLeaveRemains" />
    <asp:HiddenField runat="server" ID="hfConnectionError_MasterLeaveRemains" meta:resourcekey="hfConnectionError_MasterLeaveRemains" />
    <asp:HiddenField runat="server" ID="hfView_MasterLeaveRemains" meta:resourcekey="hfView_MasterLeaveRemains" />
    <asp:HiddenField runat="server" ID="hfAdd_MasterLeaveRemains" meta:resourcekey="hfAdd_MasterLeaveRemains" />
    <asp:HiddenField runat="server" ID="hfEdit_MasterLeaveRemains" meta:resourcekey="hfEdit_MasterLeaveRemains" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridMasterLeaveRemains_MasterLeaveRemains"
        meta:resourcekey="hfloadingPanel_GridMasterLeaveRemains_MasterLeaveRemains" />
    <asp:HiddenField runat="server" ID="hffooter_GridMasterLeaveRemains_MasterLeaveRemains"
        meta:resourcekey="hffooter_GridMasterLeaveRemains_MasterLeaveRemains" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_MasterLeaveRemains" />
    <asp:HiddenField runat="server" ID="hfLeaveRemainsPageSize_MasterLeaveRemains" />
    <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_MasterLeaveRemains" meta:resourcekey="hfclmnName_cmbPersonnel_MasterLeaveRemains" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_MasterLeaveRemains"
        meta:resourcekey="hfclmnBarCode_cmbPersonnel_MasterLeaveRemains" />
    <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_MasterLeaveRemains"
        meta:resourcekey="hfclmnCardNum_cmbPersonnel_MasterLeaveRemains" />
    <asp:HiddenField runat="server" ID="hfFromYear_MasterLeaveRemains" />
    <asp:HiddenField runat="server" ID="hfToYear_MasterLeaveRemains" />
    </form>
</body>
</html>
