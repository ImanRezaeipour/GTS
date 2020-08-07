<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Shifts.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.Shifts" %>

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
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/colorPickerStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body onkeydown="ShiftsForm_onKeydown(event);">
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="ShiftsForm" runat="server" meta:resourcekey="ShiftsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; height: 400px; font-family: Arial; font-size: small">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table style="width: 60%;">
                                <tr>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbShift" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemNew_TlbShift" runat="server" DropDownImageHeight="16px"
                                                    ClientSideCommand="tlbItemNew_TlbShift_onClick();" DropDownImageWidth="16px"
                                                    ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbShift"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbShift" runat="server" DropDownImageHeight="16px"
                                                    ClientSideCommand="tlbItemEdit_TlbShift_onClick();" DropDownImageWidth="16px"
                                                    ImageHeight="16px" ImageUrl="edit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbShift"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbShift" runat="server" DropDownImageHeight="16px"
                                                    ClientSideCommand="tlbItemDelete_TlbShift_onClick();" DropDownImageWidth="16px"
                                                    ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                                    meta:resourcekey="tlbItemDelete_TlbShift" TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbShift" runat="server" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemHelp_TlbShift" TextImageSpacing="5"
                                                    ClientSideCommand="tlbItemHelp_TlbShift_onClick();" />
                                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbShift" runat="server" DropDownImageHeight="16px"
                                                    ClientSideCommand="tlbItemSave_TlbShift_onClick();" DropDownImageWidth="16px"
                                                    ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px" ItemType="Command"
                                                    meta:resourcekey="tlbItemSave_TlbShift" TextImageSpacing="5" Enabled="false" />
                                                <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbShift" runat="server" Enabled="false"
                                                    ClientSideCommand="tlbItemCancel_TlbShift_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemCancel_TlbShift" TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbShift" runat="server"
                                                    ClientSideCommand="tlbItemFormReconstruction_TlbShift_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbShift" TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbShift" runat="server" DropDownImageHeight="16px"
                                                    ClientSideCommand="tlbItemExit_TlbShift_onClick();" DropDownImageWidth="16px"
                                                    ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbShift"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="ActionMode_Shift" class="ToolbarMode">
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
                        <td style="width: 60%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="color: White; width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_Shifts_Shift" class="HeaderLabel" style="width: 50%">
                                                    Shifts
                                                </td>
                                                <td id="loadingPanel_GridShift_Shift" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridShift_Shift" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridShift_Shift" runat="server"
                                                                ClientSideCommand="Refresh_GridShift_Shift();" DropDownImageHeight="16px" DropDownImageWidth="16px"
                                                                ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command"
                                                                meta:resourcekey="tlbItemRefresh_TlbRefresh_GridShift_Shift" TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ComponentArt:CallBack ID="CallBack_GridShift_Shift" runat="server" OnCallback="CallBack_GridShift_Shift_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridShift_Shift" runat="server" AllowHorizontalScrolling="true"
                                                    CssClass="Grid" EnableViewState="true" ShowFooter="false" FillContainer="true"
                                                    FooterCssClass="GridFooter" Height="100%" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                                    PageSize="10" RunningMode="Client" Width="744px" AllowMultipleSelect="false"
                                                    AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
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
                                                                <ComponentArt:GridColumn Align="Center" DataField="CustomCode" DefaultSortDirection="Descending"
                                                                    HeadingText="کد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnShiftCode_GridShift_Shift"
                                                                    Width="130" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام شیفت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnShiftName_GridShift_Shift"
                                                                    Width="280" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="ShiftTypeTitle" DefaultSortDirection="Descending"
                                                                    HeadingText="نوع شیفت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnShiftType_GridShift_Shift"
                                                                    Width="100" DataCellClientTemplateId="DataCellClientTemplate_clmnShiftTypeTitle_GridShift_Shift" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="NobatKari.Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نوبت کاری" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnWorkHeat_GridShift_Shift"
                                                                    Width="100" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="MinNobatKariTime" DefaultSortDirection="Descending"
                                                                    HeadingText="حد نصاب نوبت کاری" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnWorkHeatMin_GridShift_Shift"
                                                                    Width="100" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Breakfast" DefaultSortDirection="Descending"
                                                                    ColumnType="CheckBox" HeadingText="صبحانه" HeadingTextCssClass="HeadingText"
                                                                    meta:resourcekey="clmnBraekfast_GridShift_Shift" Width="60" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Lunch" DefaultSortDirection="Descending"
                                                                    ColumnType="CheckBox" HeadingText="ناهار" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnLunch_GridShift_Shift"
                                                                    Width="40" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Dinner" DefaultSortDirection="Descending"
                                                                    ColumnType="CheckBox" HeadingText="شام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDinner_GridShift_Shift"
                                                                    Width="40" />
                                                                <ComponentArt:GridColumn DataField="Color" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="ShiftType" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="NobatKariID" Visible="false" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientTemplates>
                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnShiftTypeTitle_GridShift_Shift">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td align="center">
                                                                        ##GetShiftTypeTitle_Shift(DataItem.GetMember('ShiftType').Value)##
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ComponentArt:ClientTemplate>
                                                    </ClientTemplates>
                                                    <ClientEvents>
                                                        <ItemSelect EventHandler="GridShift_Shift_onItemSelect" />
                                                        <Load EventHandler="GridShift_Shift_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField ID="ErrorHiddenField_Shift" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridShift_Shift_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridShift_Shift_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 40%" valign="top">
                            <table style="width: 100%;" class="BoxStyle" id="tblShifts_Shift">
                                <tr>
                                    <td class="DetailsBoxHeaderStyle">
                                        <div id="header_ShiftDetails_Shift" runat="server" meta:resourcekey="AlignObj" style="width: 100%;
                                            height: 100%" class="BoxContainerHeader">
                                            Shift Details</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblShiftCode_Shift" runat="server" meta:resourcekey="lblShiftCode_Shift"
                                            Text=": کد شیفت" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtShiftCode_Shift"
                                            disabled="disabled" onfocus="this.select();" onclick="this.select();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblShiftName_Shift" runat="server" meta:resourcekey="lblShiftName_Shift"
                                            Text=": نام شیفت" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtShiftName_Shift"
                                            disabled="disabled" onfocus="this.select();" onclick="this.select();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblShiftType_Shift" runat="server" meta:resourcekey="lblShiftType_Shift"
                                            Text=": نوع شیفت" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="test">
                                        <div style="width: 99%">
                                            <ComponentArt:CallBack runat="server" ID="CallBackcmbShiftType_Shift" OnCallback="CallBackcmbShiftType_Shift_onCallBack"
                                                Height="26">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbShiftType_Shift" runat="server" AutoComplete="true"
                                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                        Style="width: 100%" TextBoxCssClass="comboTextBox" Enabled="false" ExpandDirection="Up" TextBoxEnabled="true">
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbShiftType_Shift_onExpand" />
                                                            <Collapse EventHandler="cmbShiftType_Shift_onCollapse" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_ShiftType" />
                                                </Content>
                                                <ClientEvents>
                                                    <BeforeCallback EventHandler="CallBackcmbShiftType_Shift_onBeforeCallback" />
                                                    <CallbackComplete EventHandler="CallBackcmbShiftType_Shift_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBackcmbShiftType_Shift_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWorkHeat_Shift" runat="server" meta:resourcekey="lblWorkHeat_Shift"
                                            Text=": نوبت کاری" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 60%">
                                                    <ComponentArt:CallBack ID="CallBack_cmbWorkHeat_Shift" runat="server" OnCallback="CallBackcmbWorkHeat_Shift_onCallBack"
                                                        Height="26">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbWorkHeat_Shift" runat="server" AutoComplete="true"
                                                                DataTextField="Name" DataValueField="ID" AutoFilter="true" AutoHighlight="false"
                                                                CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" Style="width: 100%"
                                                                TextBoxCssClass="comboTextBox" Enabled="false">
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbWorkHeat_Shift_onExpand" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField ID="ErrorHiddenField_WorkHeat" runat="server" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbWorkHeat_Shift_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbWorkHeat_Shift_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbWorkHeat_Shift_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                <td style="width: 5%">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_cmbWorkHeat_Shift" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbWorkHeat_Shift" runat="server"
                                                                ClientSideCommand="Refresh_cmbWorkHeat_Shift();" DropDownImageHeight="16px" DropDownImageWidth="16px"
                                                                ImageHeight="16px" ImageUrl="refresh_silver.png" ImageWidth="16px" ItemType="Command"
                                                                meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbWorkHeat_Shift" TextImageSpacing="5" Enabled="false"/>
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                                <td style="width: 35%">
                                                    <ComponentArt:ToolBar ID="TlbWorkHeat_Shift" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                                        DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                                        DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                                        DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                                        ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemWorkHeats_TlbWorkHeat_Shift" runat="server"
                                                                ClientSideCommand="tlbItemWorkHeats_TlbWorkHeat_Shift_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses_silver.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemWorkHeats_TlbWorkHeat_Shift"
                                                                TextImageSpacing="5" Enabled="false" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
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
                                                    <asp:Label ID="lblWorkHeatMin_Shift" runat="server" meta:resourcekey="lblWorkHeatMin_Shift"
                                                        Text=": حد نصاب نوبت کاری" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblColorSelect_Shift" runat="server" meta:resourcekey="lblColorSelect_Shift"
                                                        Text=": انتخاب رنگ" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <MKB:TimeSelector ID="TimeSelector_WorkHeatMin_Shift" runat="server" DisplaySeconds="true"
                                                        MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;">
                                                    </MKB:TimeSelector>
                                                </td>
                                                <td>
                                                    <div>
                                                        <div>
                                                            <span class="lbl" style="line-height: 24px;"></span><a id="a_ColorPicker" href="javascript:void(0);"
                                                                class="choose"><span id="clr_ColorPicker"></span><span class="lbl">...</span>
                                                            </a>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 65%;">
                                            <tr>
                                                <td style="width: 5%">
                                                    <input id="chbBreakfast_Shift" type="checkbox" disabled="disabled" />
                                                </td>
                                                <td style="width: 28%">
                                                    <asp:Label ID="lblBreakfast_Shift" runat="server" meta:resourcekey="lblBreakfast_Shift"
                                                        Text=": صبحانه" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td style="width: 5%">
                                                    <input id="chbLunch_Shift" type="checkbox" disabled="disabled" />
                                                </td>
                                                <td style="width: 28%">
                                                    <asp:Label ID="lblLunch_Shift" runat="server" meta:resourcekey="lblLunch_Shift" Text=": ناهار"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td style="width: 5%">
                                                    <input id="chbDinner_Shift" type="checkbox" disabled="disabled" />
                                                </td>
                                                <td style="width: 28%">
                                                    <asp:Label ID="lblDinner_Shift" runat="server" meta:resourcekey="lblDinner_Shift"
                                                        Text=": شام" CssClass="WhiteLabel"></asp:Label>
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
                <table style="width: 60%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbShiftPairs" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbShiftPairs" runat="server" ClientSideCommand="tlbItemNew_TlbShiftPairs_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbShiftPairs"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbShiftPairs" runat="server" ClientSideCommand="tlbItemEdit_TlbShiftPairs_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbShiftPairs"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbShiftPairs" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemDelete_TlbShiftPairs_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemDelete_TlbShiftPairs" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbShiftPairs" runat="server" ClientSideCommand="tlbItemSave_TlbShiftPairs_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbShiftPairs"
                                        TextImageSpacing="5" Enabled="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbShiftPairs" runat="server" Enabled="false"
                                        ClientSideCommand="tlbItemCancel_TlbShiftPairs_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemCancel_TlbShiftPairs" TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_ShiftPair" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 60%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="color: White; width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_ShiftPairs_Shift" class="HeaderLabel" style="width: 50%">
                                                    ShiftPairs
                                                </td>
                                                <td id="loadingPanel_GridShiftPairs_Shift" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridShiftPairs_Shift" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridShiftPairs_Shift" runat="server"
                                                                ClientSideCommand="Refresh_GridShiftPairs_Shift();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridShiftPairs_Shift"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridShiftPairs_Shift" OnCallback="CallBack_GridShiftPairs_Shift_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridShiftPairs_Shift" runat="server" CssClass="Grid" EnableViewState="true"
                                                    FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                    PagePaddingEnabled="true" PageSize="5" RunningMode="Client" Width="100%" AllowMultipleSelect="false"
                                                    ScrollBar="On" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                    ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ShowFooter="false" ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                            DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                            RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                            SortImageWidth="9">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="FromTime" DefaultSortDirection="Descending"
                                                                    HeadingText="از" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnShiftPairFrom_GridShiftPairs_Shift"
                                                                    Width="25" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="ToTime" DefaultSortDirection="Descending"
                                                                    HeadingText="تا" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnShiftPairTo_GridShiftPairs_Shift"
                                                                    Width="25" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="BeforeToleranceTime" DefaultSortDirection="Descending"
                                                                    HeadingText="از تلرانس" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnShiftPairAfterTolerance_GridShiftPairs_Shift"
                                                                    Width="25" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="AfterToleranceTime" DefaultSortDirection="Descending"
                                                                    HeadingText="تا تلرانس" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnShiftPairBeforeTolerance_GridShiftPairs_Shift"
                                                                    Width="25" />
                                                                <ComponentArt:GridColumn DataField="NextDayContinual" Visible="false" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridShiftPairs_Shift_onLoad" />
                                                        <ItemSelect EventHandler="GridShiftPairs_Shift_onItemSelect" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField ID="ErrorHiddenField_ShiftPairs" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridShiftPairs_Shift_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridShiftPairs_Shift_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 35%" valign="top">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td colspan="2" class="DetailsBoxHeaderStyle">
                                        <div id="header_ShiftPairsDetails_Shift" runat="server" meta:resourcekey="AlignObj"
                                            class="BoxContainerHeader" style="color: White; width: 100%; height: 100%">
                                            Shift Pair Details</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblShiftPairFrom_ShiftPairs" runat="server" meta:resourcekey="lblShiftPairFrom_Shift"
                                            Text=": از" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblShiftPairTo_ShiftPairs" runat="server" meta:resourcekey="lblShiftPairTo_Shift"
                                            Text=": تا" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <MKB:TimeSelector ID="TimeSelector_From_ShiftPairs" runat="server" DisplaySeconds="true"
                                            MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                        </MKB:TimeSelector>
                                    </td>
                                    <td>
                                        <MKB:TimeSelector ID="TimeSelector_To_ShiftPairs" runat="server" DisplaySeconds="true"
                                            MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                        </MKB:TimeSelector>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblShiftPairFromTolerance_Shift" runat="server" meta:resourcekey="lblShiftPairFromTolerance_Shift"
                                            Text=": از تلرانس" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblShiftPairToTolerance_Shift" runat="server" meta:resourcekey="lblShiftPairToTolerance_Shift"
                                            Text=": تا تلرانس" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <MKB:TimeSelector ID="TimeSelector_FromTolerance_ShiftPairs" runat="server" DisplaySeconds="true"
                                            MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                        </MKB:TimeSelector>
                                    </td>
                                    <td>
                                        <MKB:TimeSelector ID="TimeSelector_ToTolerance_ShiftPairs" runat="server" DisplaySeconds="true"
                                            MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                        </MKB:TimeSelector>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 5%">
                                                    <input id="chbContinueInNextDay_ShiftPairs" type="checkbox" disabled="disabled" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblContinueInNextDay_ShiftPairs" runat="server" Text="ادامه در روز بعد"
                                                        CssClass="WhiteLabel" meta:resourcekey="lblContinueInNextDay_ShiftPairs"></asp:Label>
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
    <ComponentArt:Dialog ID="DialogColors" runat="server" AllowDrag="true" AllowResize="false"
        Alignment="MiddleCentre" Width="325" Height="363" ContentCssClass="dlg">
        <Content>
            <div class="ttl" onmousedown="DialogColors.StartDrag(event);">
                <a class="close" href="javascript:void(0);" onclick="DialogColors.close();this.blur();return false;">
                    <span>X</span></a>
            </div>
            <div class="con">
                <ComponentArt:MultiPage ID="DialogPages" runat="server">
                    <ComponentArt:PageView ID="GridPage">
                        <div class="pg">
                            <ComponentArt:ColorPicker ID="ColorGrid" GridColumns="12" Mode="Grid" CssClass="grid"
                                ColorCssClass="swatch" ColorHoverCssClass="swatch-h" ColorActiveCssClass="swatch-a"
                                ColorGridCssClass="swatches" GridCellSpacing="1" runat="server">
                                <ClientEvents>
                                    <ColorChanged EventHandler="color_changed" />
                                </ClientEvents>
                                <Colors>
                                    <ComponentArt:ColorPickerColor Hex="#ff0000" />
                                    <ComponentArt:ColorPickerColor Hex="#ff8f00" />
                                    <ComponentArt:ColorPickerColor Hex="#ffff00" />
                                    <ComponentArt:ColorPickerColor Hex="#c5ff00" />
                                    <ComponentArt:ColorPickerColor Hex="#00ff00" />
                                    <ComponentArt:ColorPickerColor Hex="#00ff98" />
                                    <ComponentArt:ColorPickerColor Hex="#00ffff" />
                                    <ComponentArt:ColorPickerColor Hex="#00c1ff" />
                                    <ComponentArt:ColorPickerColor Hex="#0000ff" />
                                    <ComponentArt:ColorPickerColor Hex="#9400ff" />
                                    <ComponentArt:ColorPickerColor Hex="#ff00ff" />
                                    <ComponentArt:ColorPickerColor Hex="#ff0058" />
                                    <ComponentArt:ColorPickerColor Hex="#e51919" />
                                    <ComponentArt:ColorPickerColor Hex="#e58b19" />
                                    <ComponentArt:ColorPickerColor Hex="#e4e519" />
                                    <ComponentArt:ColorPickerColor Hex="#b6e519" />
                                    <ComponentArt:ColorPickerColor Hex="#19e519" />
                                    <ComponentArt:ColorPickerColor Hex="#19e592" />
                                    <ComponentArt:ColorPickerColor Hex="#19e4e5" />
                                    <ComponentArt:ColorPickerColor Hex="#19b3e5" />
                                    <ComponentArt:ColorPickerColor Hex="#1919e5" />
                                    <ComponentArt:ColorPickerColor Hex="#8f19e5" />
                                    <ComponentArt:ColorPickerColor Hex="#e519e4" />
                                    <ComponentArt:ColorPickerColor Hex="#e5195f" />
                                    <ComponentArt:ColorPickerColor Hex="#a33636" />
                                    <ComponentArt:ColorPickerColor Hex="#a37336" />
                                    <ComponentArt:ColorPickerColor Hex="#a3a336" />
                                    <ComponentArt:ColorPickerColor Hex="#8aa336" />
                                    <ComponentArt:ColorPickerColor Hex="#36a336" />
                                    <ComponentArt:ColorPickerColor Hex="#36a376" />
                                    <ComponentArt:ColorPickerColor Hex="#36a3a3" />
                                    <ComponentArt:ColorPickerColor Hex="#3688a3" />
                                    <ComponentArt:ColorPickerColor Hex="#3636a3" />
                                    <ComponentArt:ColorPickerColor Hex="#7536a3" />
                                    <ComponentArt:ColorPickerColor Hex="#a336a3" />
                                    <ComponentArt:ColorPickerColor Hex="#a3365c" />
                                    <ComponentArt:ColorPickerColor Hex="#602020" />
                                    <ComponentArt:ColorPickerColor Hex="#604420" />
                                    <ComponentArt:ColorPickerColor Hex="#606020" />
                                    <ComponentArt:ColorPickerColor Hex="#516020" />
                                    <ComponentArt:ColorPickerColor Hex="#206020" />
                                    <ComponentArt:ColorPickerColor Hex="#206046" />
                                    <ComponentArt:ColorPickerColor Hex="#206060" />
                                    <ComponentArt:ColorPickerColor Hex="#205060" />
                                    <ComponentArt:ColorPickerColor Hex="#202060" />
                                    <ComponentArt:ColorPickerColor Hex="#452060" />
                                    <ComponentArt:ColorPickerColor Hex="#602060" />
                                    <ComponentArt:ColorPickerColor Hex="#602036" />
                                    <ComponentArt:ColorPickerColor Hex="#ff9999" />
                                    <ComponentArt:ColorPickerColor Hex="#ffd299" />
                                    <ComponentArt:ColorPickerColor Hex="#ffff99" />
                                    <ComponentArt:ColorPickerColor Hex="#e8ff99" />
                                    <ComponentArt:ColorPickerColor Hex="#99ff99" />
                                    <ComponentArt:ColorPickerColor Hex="#99ffd6" />
                                    <ComponentArt:ColorPickerColor Hex="#99ffff" />
                                    <ComponentArt:ColorPickerColor Hex="#99e6ff" />
                                    <ComponentArt:ColorPickerColor Hex="#9999ff" />
                                    <ComponentArt:ColorPickerColor Hex="#d499ff" />
                                    <ComponentArt:ColorPickerColor Hex="#ff99ff" />
                                    <ComponentArt:ColorPickerColor Hex="#ff99bc" />
                                    <ComponentArt:ColorPickerColor Hex="#ffe0e0" />
                                    <ComponentArt:ColorPickerColor Hex="#fff1e0" />
                                    <ComponentArt:ColorPickerColor Hex="#ffffe0" />
                                    <ComponentArt:ColorPickerColor Hex="#f8ffe0" />
                                    <ComponentArt:ColorPickerColor Hex="#e0ffe0" />
                                    <ComponentArt:ColorPickerColor Hex="#e0fff3" />
                                    <ComponentArt:ColorPickerColor Hex="#e0ffff" />
                                    <ComponentArt:ColorPickerColor Hex="#e0f7ff" />
                                    <ComponentArt:ColorPickerColor Hex="#e0e0ff" />
                                    <ComponentArt:ColorPickerColor Hex="#f2e0ff" />
                                    <ComponentArt:ColorPickerColor Hex="#ffe0ff" />
                                    <ComponentArt:ColorPickerColor Hex="#ffe0eb" />
                                    <ComponentArt:ColorPickerColor Hex="#ffffff" />
                                    <ComponentArt:ColorPickerColor Hex="#e2e2e2" />
                                    <ComponentArt:ColorPickerColor Hex="#d7d7d7" />
                                    <ComponentArt:ColorPickerColor Hex="#cdcdcd" />
                                    <ComponentArt:ColorPickerColor Hex="#b7b7b7" />
                                    <ComponentArt:ColorPickerColor Hex="#898989" />
                                    <ComponentArt:ColorPickerColor Hex="#707070" />
                                    <ComponentArt:ColorPickerColor Hex="#555555" />
                                    <ComponentArt:ColorPickerColor Hex="#464646" />
                                    <ComponentArt:ColorPickerColor Hex="#252525" />
                                    <ComponentArt:ColorPickerColor Hex="#111111" />
                                    <ComponentArt:ColorPickerColor Hex="#000000" />
                                </Colors>
                            </ComponentArt:ColorPicker>
                        </div>
                    </ComponentArt:PageView>
                </ComponentArt:MultiPage>
            </div>
            <div class="ftr">
                <div id="chip">
                </div>
                <div id="hex">
                </div>
            </div>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogColors_OnShow" />
            <OnClose EventHandler="DialogColors_OnClose" />
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
    <asp:HiddenField runat="server" ID="hfheader_Shifts_Shift" meta:resourcekey="hfheader_Shifts_Shift" />
    <asp:HiddenField runat="server" ID="hfheader_ShiftDetails_Shift" meta:resourcekey="hfheader_ShiftDetails_Shift" />
    <asp:HiddenField runat="server" ID="hfheader_ShiftPairs_Shift" meta:resourcekey="hfheader_ShiftPairs_Shift" />
    <asp:HiddenField runat="server" ID="hfheader_ShiftPairsDetails_Shift" meta:resourcekey="hfheader_ShiftPairsDetails_Shift" />
    <asp:HiddenField runat="server" ID="hfView_Shift" meta:resourcekey="hfView_Shift" />
    <asp:HiddenField runat="server" ID="hfAdd_Shift" meta:resourcekey="hfAdd_Shift" />
    <asp:HiddenField runat="server" ID="hfEdit_Shift" meta:resourcekey="hfEdit_Shift" />
    <asp:HiddenField runat="server" ID="hfDelete_Shift" meta:resourcekey="hfDelete_Shift" />
    <asp:HiddenField runat="server" ID="hfView_ShiftPair" meta:resourcekey="hfView_ShiftPair" />
    <asp:HiddenField runat="server" ID="hfAdd_ShiftPair" meta:resourcekey="hfAdd_ShiftPair" />
    <asp:HiddenField runat="server" ID="hfDelete_ShiftPair" meta:resourcekey="hfDelete_ShiftPair" />
    <asp:HiddenField runat="server" ID="hfEdit_ShiftPair" meta:resourcekey="hfEdit_ShiftPair" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_Shift" meta:resourcekey="hfDeleteMessage_Shift" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_ShiftPair" meta:resourcekey="hfDeleteMessage_ShiftPair" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Shift" meta:resourcekey="hfCloseMessage_Shift" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridShift_Shift" meta:resourcekey="hfloadingPanel_GridShift_Shift" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridShiftPairs_Shift" meta:resourcekey="hfloadingPanel_GridShiftPairs_Shift" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_Shift" meta:resourcekey="hfcmbAlarm_Shift" />
    <asp:HiddenField runat="server" ID="hfShiftTypes_Shift" />
    <asp:HiddenField runat="server" ID="hfErrorType_Shift" meta:resourcekey="hfErrorType_Shift" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Shift" meta:resourcekey="hfConnectionError_Shift" />
    </form>
</body>
</html>
