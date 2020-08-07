<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Machines.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.Machines" %>

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
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="MachinesForm" runat="server" meta:resourcekey="MachinesForm">
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
                            <ComponentArt:ToolBar ID="TlbMachine" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbMachine" runat="server" ClientSideCommand="tlbItemNew_TlbMachine_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbMachine"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbMachine" runat="server" ClientSideCommand="tlbItemEdit_TlbMachine_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbMachine"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbMachine" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemDelete_TlbMachine_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemDelete_TlbMachine" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbMachine" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbMachine" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbMachine_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbMachine" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemSave_TlbMachine_onClick();" DropDownImageWidth="16px"
                                        Enabled="false" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbMachine" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbMachine" runat="server" ClientSideCommand="tlbItemCancel_TlbMachine_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbMachine"
                                        Enabled="false" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbMachine" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbMachine_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbMachine" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbMachine" runat="server" ClientSideCommand="tlbItemExit_TlbMachine_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbMachine"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_Machines" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="55%">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 65%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="color: White; width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_Machines_Machines" class="HeaderLabel" style="width: 50%">
                                                    Machines
                                                </td>
                                                <td id="loadingPanel_GridMachines_Machines" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridMachines_Machines" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridMachines_Machines" runat="server"
                                                                ClientSideCommand="Refresh_GridMachines_Machines();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridMachines_Machines"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridMachines_Machines" OnCallback="CallBack_GridMachines_Machines_onCallback">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridMachines_Machines" runat="server" CssClass="Grid"
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
                                                                <ComponentArt:GridColumn Align="Center" DataField="CustomCode" DefaultSortDirection="Descending"
                                                                    HeadingText="کد دستگاه" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMachineCode_GridMachine" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام دستگاه" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMachineName_GridMachine" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="ClockType.Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نوع دستگاه" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMachineType_GridMachine"
                                                                    DataCellClientTemplateId="DataCellClientTemplate_clmnMachineTypeTitle_GridMachines_Machines" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Station.Name" DefaultSortDirection="Descending"
                                                                    HeadingText="ایستگاه کنترل" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnControlStation_GridMachine" />
                                                                <ComponentArt:GridColumn Align="Center" ColumnType="CheckBox" DataField="Active"
                                                                    DefaultSortDirection="Descending" HeadingText="وضعیت" HeadingTextCssClass="HeadingText"
                                                                    meta:resourcekey="clmnSituation_GridMachine" />
                                                                <ComponentArt:GridColumn DataField="ClockType.ID" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="Station.ID" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="Tel" Visible="false" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridMachines_Machines_onLoad" />
                                                        <ItemSelect EventHandler="GridMachines_Machines_onItemSelect" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Machines" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridMachines_Machines_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridMachines_Machines_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 35%">
                            <table style="width: 90%;" class="BoxStyle" id="tblMachines_Machines">
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td class="DetailsBoxHeaderStyle">
                                        <div id="header_MachineDetails_Machines" runat="server" meta:resourcekey="AlignObj"
                                            style="color: White; width: 100%; height: 100%" class="BoxContainerHeader">
                                            Machine Details</div>
                                    </td>
                                </tr>
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblMachineCode_Machine" runat="server" meta:resourcekey="lblMachineCode_Machine"
                                            Text=": کد دستگاه" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtMachineCode_Machines"
                                            disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                    </td>
                                </tr>
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblMachineName_Machine" runat="server" meta:resourcekey="lblMachineName_Machine"
                                            Text=": نام دستگاه" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtMachineName_Machines"
                                            disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                    </td>
                                </tr>
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblMachineType_Machine" runat="server" meta:resourcekey="lblMachineType_Machine"
                                            Text=": نوع دستگاه" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbMachineTypes_Machines" OnCallback="CallBack_cmbMachineTypes_Machines_onCallback"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbMachineTypes_Machines" runat="server" AutoComplete="true"
                                                    DataTextField="Name" DataValueField="ID" AutoFilter="true" AutoHighlight="false"
                                                    CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" Style="width: 100%"
                                                    TextBoxCssClass="comboTextBox" Enabled="false" TextBoxEnabled="true">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbMachineTypes_Machines_onExpand" />
                                                        <Collapse EventHandler="cmbMachineTypes_Machines_onCollapse" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_MachineTypes" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbMachineTypes_Machines_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbMachineTypes_Machines_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbMachineTypes_Machines_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblControlStation_Machine" runat="server" meta:resourcekey="lblControlStation_Machine"
                                            Text=": ایستگاه کنترل" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBcak_cmbControlStations_Machines" OnCallback="CallBcak_cmbControlStations_Machines_onCallback"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbControlStation_Machines" runat="server" AutoComplete="true"
                                                    DataTextField="Name" DataValueField="ID" AutoFilter="true" AutoHighlight="false"
                                                    CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" Style="width: 100%"
                                                    TextBoxCssClass="comboTextBox" Enabled="false" TextBoxEnabled="true">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbControlStation_Machines_onExpand" />
                                                        <Collapse EventHandler="cmbControlStation_Machines_onCollapse" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_ControlStations" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBcak_cmbControlStations_Machines_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBcak_cmbControlStations_Machines_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBcak_cmbControlStations_Machines_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                                <tr id="Tr9" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblMachineConnectionTelNumber_Machine" runat="server" meta:resourcekey="lblMachineConnectionTelNumber_Machine"
                                            Text=": شماره تلفن خط متصل به دستگاه" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr10" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtMachineConnectionTelNumber_Machines"
                                            disabled="disabled" onclick="this.select();" onfocus="this.select();" />
                                    </td>
                                </tr>
                                <tr id="Tr12" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Label runat="server" ID="lblState_Machines" meta:resourcekey="lblState_Machines"
                                                        CssClass="WhiteLabel" Text=": وضعیت"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 5%">
                                                    <input id="rdbActive_Machines" name="Machines" type="radio" value="V1" checked="checked"
                                                        disabled="disabled" />
                                                </td>
                                                <td style="width: 45%">
                                                    <asp:Label ID="lblActive_Machine" runat="server" meta:resourcekey="lblActive_Machine"
                                                        Text=": فعال" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td style="width: 5%">
                                                    <input id="rdbInActive_Machines" name="Machines" type="radio" value="V1" disabled="disabled" />
                                                </td>
                                                <td style="width: 45%">
                                                    <asp:Label ID="lblInActive_Machine" runat="server" meta:resourcekey="lblInActive_Machine"
                                                        Text=": غیر فعال" CssClass="WhiteLabel"></asp:Label>
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
    <asp:HiddenField runat="server" ID="hfheader_MachineDetails_Machines" meta:resourcekey="hfheader_MachineDetails_Machines" />
    <asp:HiddenField runat="server" ID="hfheader_Machines_Machines" meta:resourcekey="hfheader_Machines_Machines" />
    <asp:HiddenField runat="server" ID="hfView_Machines" meta:resourcekey="hfView_Machines" />
    <asp:HiddenField runat="server" ID="hfAdd_Machines" meta:resourcekey="hfAdd_Machines" />
    <asp:HiddenField runat="server" ID="hfEdit_Machines" meta:resourcekey="hfEdit_Machines" />
    <asp:HiddenField runat="server" ID="hfDelete_Machines" meta:resourcekey="hfDelete_Machines" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_Machines" meta:resourcekey="hfDeleteMessage_Machines" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Machines" meta:resourcekey="hfCloseMessage_Machines" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridMachines_Machines" meta:resourcekey="hfloadingPanel_GridMachines_Machines" />
    <asp:HiddenField runat="server" ID="hfErrorType_Machines" meta:resourcekey="hfErrorType_Machines" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Machines" meta:resourcekey="hfConnectionError_Machines" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_Machines" meta:resourcekey="hfcmbAlarm_Machines" />
    <asp:HiddenField runat="server" ID="hfMachineTypes_Machines" />
    </form>
</body>
</html>
